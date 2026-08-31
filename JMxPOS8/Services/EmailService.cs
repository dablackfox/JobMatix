using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JMxPOS8.Services;

public class EmailSettings
{
    public string Host { get; set; } = "";
    public int Port { get; set; } = 587;
    public bool UseSsl { get; set; } = true;
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string FromAddress { get; set; } = "";

    public bool IsConfigured => !string.IsNullOrWhiteSpace(Host) && !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
}

public class EmailSendResult
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; } = "";
}

// Sends a real SMTP message given settings - split out from EmailService so tests can
// substitute a no-op fake instead of opening a real connection to a live mail relay (a real
// SMTP send costs nothing to attempt, unlike SMS, but still shouldn't fire during automated
// verification against a real customer's inbox or a real third-party account).
public interface IEmailTransport
{
    Task SendAsync(EmailSettings settings, MailMessage message);
}

public class SmtpEmailTransport : IEmailTransport
{
    public async Task SendAsync(EmailSettings settings, MailMessage message)
    {
        using var client = new SmtpClient(settings.Host, settings.Port)
        {
            EnableSsl = settings.UseSsl,
            Credentials = new NetworkCredential(settings.Username, settings.Password)
        };
        await client.SendMailAsync(message);
    }
}

// Ports the legacy email notification feature (frmNotifyCust22.vb's email tab,
// ROADMAP.md Phase 3) - plain SMTP via System.Net.Mail, matching the legacy
// System.Net.Mail.SmtpClient usage exactly rather than introducing a new mail library.
// Settings live in the generic systeminfo key/value table alongside SMS settings.
public class EmailService
{
    private const string KeyHost = "smtp_host";
    private const string KeyPort = "smtp_port";
    private const string KeyUseSsl = "smtp_use_ssl";
    private const string KeyUsername = "smtp_username";
    private const string KeyPassword = "smtp_password";
    private const string KeyFromAddress = "smtp_from_address";

    private readonly DatabaseService _db;
    private readonly IEmailTransport _transport;

    public EmailService(DatabaseService db, IEmailTransport? transport = null)
    {
        _db = db;
        _transport = transport ?? new SmtpEmailTransport();
    }

    public async Task<EmailSettings> GetSettingsAsync()
    {
        var values = new Dictionary<string, string>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT info_key, info_value FROM systeminfo WHERE info_key IN (@k1, @k2, @k3, @k4, @k5, @k6)";
        AddParam(cmd, "@k1", KeyHost);
        AddParam(cmd, "@k2", KeyPort);
        AddParam(cmd, "@k3", KeyUseSsl);
        AddParam(cmd, "@k4", KeyUsername);
        AddParam(cmd, "@k5", KeyPassword);
        AddParam(cmd, "@k6", KeyFromAddress);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            values[reader.GetString(0)] = reader.GetString(1);

        return new EmailSettings
        {
            Host = values.GetValueOrDefault(KeyHost, ""),
            Port = int.TryParse(values.GetValueOrDefault(KeyPort, "587"), out var p) ? p : 587,
            UseSsl = values.GetValueOrDefault(KeyUseSsl, "Y") == "Y",
            Username = values.GetValueOrDefault(KeyUsername, ""),
            Password = values.GetValueOrDefault(KeyPassword, ""),
            FromAddress = values.GetValueOrDefault(KeyFromAddress, "")
        };
    }

    public async Task SaveSettingsAsync(EmailSettings settings)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        foreach (var (key, value) in new[]
        {
            (KeyHost, settings.Host),
            (KeyPort, settings.Port.ToString()),
            (KeyUseSsl, settings.UseSsl ? "Y" : "N"),
            (KeyUsername, settings.Username),
            (KeyPassword, settings.Password),
            (KeyFromAddress, settings.FromAddress)
        })
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO systeminfo (info_key, info_value)
                VALUES (@key, @value)
                ON CONFLICT (info_key) DO UPDATE SET info_value = @value, date_updated = CURRENT_TIMESTAMP";
            AddParam(cmd, "@key", key);
            AddParam(cmd, "@value", value);
            await Task.Run(() => cmd.ExecuteNonQuery());
        }
    }

    public async Task<EmailSendResult> SendEmailAsync(string toAddress, string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(toAddress))
            return new EmailSendResult { Success = false, ErrorMessage = "No email address on file for this customer" };

        var settings = await GetSettingsAsync();
        if (!settings.IsConfigured)
            return new EmailSendResult { Success = false, ErrorMessage = "Email (SMTP) is not configured (Staff > Email Settings)" };

        try
        {
            using var message = new MailMessage(settings.FromAddress, toAddress, subject, body);
            await _transport.SendAsync(settings, message);
            return new EmailSendResult { Success = true };
        }
        catch (Exception ex)
        {
            return new EmailSendResult { Success = false, ErrorMessage = $"Send failed: {ex.Message}" };
        }
    }

    private static void AddParam(System.Data.IDbCommand cmd, string name, object value)
    {
        var param = cmd.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        cmd.Parameters.Add(param);
    }
}
