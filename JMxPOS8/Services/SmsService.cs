using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JMxPOS8.Services;

public class SmsGatewaySettings
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string FromNumber { get; set; } = "";

    public bool IsConfigured => !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
}

public class SmsSendResult
{
    public bool Success { get; set; }
    public string RawResponse { get; set; } = "";
    public string ErrorMessage { get; set; } = "";
}

// Ports the legacy SMS notification feature (modInetSubs.vb/gbSendSMS, ROADMAP.md Phase 3).
// The legacy code supported 4 gateways, but the real historical SystemInfo config (checked
// directly against the restored legacy SQL Server database, JobTracking.SystemInfo) shows
// only one was ever actually configured/used in production: DirectSMS
// (SmsGatewayHostName='directSMS'). The other 3 adapters were never live, so this only
// ports DirectSMS rather than carrying dead options forward. Settings are stored in the
// generic systeminfo key/value table, matching how the legacy app stored gateway
// credentials (clsSystemInfo), rather than a dedicated settings table.
public class SmsService
{
    private const string KeyUsername = "sms_username";
    private const string KeyPassword = "sms_password";
    private const string KeyFromNumber = "sms_from_number";

    private readonly DatabaseService _db;
    private readonly HttpClient _http;

    // Accepts an HttpClient so tests can substitute a fake handler instead of making real
    // network calls to a live SMS gateway (which would cost real money and send a real
    // message to whatever number is dialled in).
    public SmsService(DatabaseService db, HttpClient? httpClient = null)
    {
        _db = db;
        _http = httpClient ?? new HttpClient();
    }

    public async Task<SmsGatewaySettings> GetSettingsAsync()
    {
        var values = new Dictionary<string, string>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT info_key, info_value FROM systeminfo WHERE info_key IN (@k1, @k2, @k3)";
        AddParam(cmd, "@k1", KeyUsername);
        AddParam(cmd, "@k2", KeyPassword);
        AddParam(cmd, "@k3", KeyFromNumber);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            values[reader.GetString(0)] = reader.GetString(1);

        return new SmsGatewaySettings
        {
            Username = values.GetValueOrDefault(KeyUsername, ""),
            Password = values.GetValueOrDefault(KeyPassword, ""),
            FromNumber = values.GetValueOrDefault(KeyFromNumber, "")
        };
    }

    public async Task SaveSettingsAsync(SmsGatewaySettings settings)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        foreach (var (key, value) in new[]
        {
            (KeyUsername, settings.Username),
            (KeyPassword, settings.Password),
            (KeyFromNumber, settings.FromNumber)
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

    public async Task<SmsSendResult> SendSmsAsync(string toMobile, string message)
    {
        if (string.IsNullOrWhiteSpace(toMobile))
            return new SmsSendResult { Success = false, ErrorMessage = "No mobile number on file for this customer" };

        var settings = await GetSettingsAsync();
        if (!settings.IsConfigured)
            return new SmsSendResult { Success = false, ErrorMessage = "SMS gateway is not configured (Staff > SMS Settings)" };

        try
        {
            // Builds the whole request as a query string and POSTs an empty body, matching
            // the legacy inetWebRequest usage confirmed in modInetSubs.vb - DirectSMS has no
            // structured error status, so success is judged by an "id:" substring in the
            // response body instead.
            var queryParams = new Dictionary<string, string>
            {
                ["Username"] = settings.Username,
                ["Password"] = settings.Password,
                ["Recipients"] = toMobile,
                ["Message"] = message
            };
            var query = string.Join("&", queryParams.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));
            var response = await _http.PostAsync($"https://api.directsms.com.au/s3/http/send_message?{query}", new StringContent(""));
            var text = await response.Content.ReadAsStringAsync();
            bool success = text.Contains("id:", StringComparison.OrdinalIgnoreCase);
            return new SmsSendResult { Success = success, RawResponse = text };
        }
        catch (Exception ex)
        {
            return new SmsSendResult { Success = false, ErrorMessage = $"Gateway request failed: {ex.Message}" };
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
