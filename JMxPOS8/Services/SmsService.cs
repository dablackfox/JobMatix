using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JMxPOS8.Services;

public enum SmsGateway { SmsBoss, SmsBroadcast, SmsGlobal, DirectSms }

public class SmsGatewaySettings
{
    public SmsGateway Gateway { get; set; } = SmsGateway.SmsBoss;
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

// Ports the legacy SMS notification feature (modInetSubs.vb/gbSendSMS, ROADMAP.md Phase 3) -
// 4 plain HTTP(S) Australian SMS gateways, no modem/GSM hardware dependency. Settings are
// stored in the generic systeminfo key/value table, matching how the legacy app stored
// gateway credentials (clsSystemInfo), rather than a dedicated settings table.
public class SmsService
{
    private const string KeyGateway = "sms_gateway";
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
        cmd.CommandText = "SELECT info_key, info_value FROM systeminfo WHERE info_key IN (@k1, @k2, @k3, @k4)";
        AddParam(cmd, "@k1", KeyGateway);
        AddParam(cmd, "@k2", KeyUsername);
        AddParam(cmd, "@k3", KeyPassword);
        AddParam(cmd, "@k4", KeyFromNumber);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            values[reader.GetString(0)] = reader.GetString(1);

        return new SmsGatewaySettings
        {
            Gateway = values.TryGetValue(KeyGateway, out var g) && Enum.TryParse<SmsGateway>(g, out var parsed) ? parsed : SmsGateway.SmsBoss,
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
            (KeyGateway, settings.Gateway.ToString()),
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
            return settings.Gateway switch
            {
                SmsGateway.SmsBoss => await SendViaSmsBossAsync(settings, toMobile, message),
                SmsGateway.SmsBroadcast => await SendViaQueryStringGatewayAsync(
                    "https://api.smsbroadcast.com.au/api-adv.php",
                    new Dictionary<string, string> { ["username"] = settings.Username, ["password"] = settings.Password, ["to"] = toMobile, ["from"] = settings.FromNumber, ["message"] = message },
                    "ok:"),
                SmsGateway.SmsGlobal => await SendViaQueryStringGatewayAsync(
                    "https://www.smsglobal.com/http-api.php",
                    new Dictionary<string, string> { ["action"] = "sendsms", ["user"] = settings.Username, ["password"] = settings.Password, ["to"] = toMobile, ["from"] = settings.FromNumber, ["text"] = message },
                    "ok:"),
                SmsGateway.DirectSms => await SendViaQueryStringGatewayAsync(
                    "https://api.directsms.com.au/s3/http/send_message",
                    new Dictionary<string, string> { ["Username"] = settings.Username, ["Password"] = settings.Password, ["Recipients"] = toMobile, ["Message"] = message },
                    "id:"),
                _ => new SmsSendResult { Success = false, ErrorMessage = "Unknown gateway" }
            };
        }
        catch (Exception ex)
        {
            return new SmsSendResult { Success = false, ErrorMessage = $"Gateway request failed: {ex.Message}" };
        }
    }

    private async Task<SmsSendResult> SendViaSmsBossAsync(SmsGatewaySettings settings, string toMobile, string message)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", settings.Username),
            new KeyValuePair<string, string>("password", settings.Password),
            new KeyValuePair<string, string>("from", settings.FromNumber),
            new KeyValuePair<string, string>("to", toMobile),
            new KeyValuePair<string, string>("message", message)
        });
        var response = await _http.PostAsync("http://www.smsboss.com.au/api/sms.asmx/SendSMS", content);
        var text = await response.Content.ReadAsStringAsync();
        bool success = response.IsSuccessStatusCode && text.Contains("<Status>OK</Status>", StringComparison.OrdinalIgnoreCase);
        return new SmsSendResult { Success = success, RawResponse = text };
    }

    // SmsBroadcast/SmsGlobal/DirectSms all build the whole request as a query string and
    // POST an empty body, rather than a form-encoded body - matches the legacy inetWebRequest
    // usage confirmed in modInetSubs.vb (ROADMAP.md Phase 3 audit). Success is judged by a
    // gateway-specific substring in the response body rather than a status code, since none
    // of the three return a structured error status.
    private async Task<SmsSendResult> SendViaQueryStringGatewayAsync(string baseUrl, Dictionary<string, string> queryParams, string successSubstring)
    {
        var query = string.Join("&", queryParams.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));
        var response = await _http.PostAsync($"{baseUrl}?{query}", new StringContent(""));
        var text = await response.Content.ReadAsStringAsync();
        bool success = text.Contains(successSubstring, StringComparison.OrdinalIgnoreCase);
        return new SmsSendResult { Success = success, RawResponse = text };
    }

    private static void AddParam(System.Data.IDbCommand cmd, string name, object value)
    {
        var param = cmd.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        cmd.Parameters.Add(param);
    }
}
