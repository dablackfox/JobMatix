using System;
using System.Collections.Generic;
using System.Globalization;

namespace JMxPOS8.Services;

// Port of the legacy app's own session-log parsing (JMxJT620.NET
// ucChildJobReports42.vb's gCurComputeChargeableHours, and
// modReportSubs.vb's gbQueryWorkSessions per-line dissection) - the real
// "SHAPE query" work for job reporting (ROADMAP.md Phase 3/job reporting)
// turned out to already live in application code, not the SQL Server
// SHAPE/scalar-function side; that side only ever called back into a
// dynamically-created T-SQL function doing the same string parsing.
//
// jobs.sessiontimes is a free-text log, one line per work session,
// CRLF-separated: "dd/MMM/yy: StaffName  +H.HH" with an optional "-NC"
// suffix on the hours marking a non-chargeable session. Verified against
// 17,382 real migrated jobs (2,295 of which have at least one -NC line).
public static class SessionTimesParser
{
    public record SessionEntry(DateTime? Date, string StaffName, decimal HoursChargeable, decimal HoursNonChargeable);

    private static readonly string[] DateFormats = { "dd/MMM/yy", "d/MMM/yy" };

    public static List<SessionEntry> Parse(string? sessionTimes)
    {
        var entries = new List<SessionEntry>();
        if (string.IsNullOrWhiteSpace(sessionTimes))
            return entries;

        var lines = sessionTimes.Replace("\r\n", "\n").Split('\n');
        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();
            if (line.Length == 0)
                continue;

            var colonPos = line.IndexOf(':');
            if (colonPos <= 0)
                continue;

            var datePart = line.Substring(0, colonPos).Trim();
            var rest = line.Substring(colonPos + 1).Trim();

            DateTime? date = DateTime.TryParseExact(datePart, DateFormats, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var parsedDate)
                ? parsedDate
                : null;

            var plusPos = rest.IndexOf('+');
            if (plusPos < 0)
                continue;

            // Legacy default for a blank name (ucChildJobReports42.vb line 532) - kept
            // faithfully so totals match the legacy report exactly rather than silently
            // dropping these lines.
            var staffName = rest.Substring(0, plusPos).Trim();
            if (staffName.Length == 0)
                staffName = "YY_UNKNOWN";

            var timePart = rest.Substring(plusPos + 1).Trim().ToUpperInvariant();
            var chargeable = true;
            const string noChargeMarker = "-NC";
            if (timePart.Contains(noChargeMarker))
            {
                chargeable = false;
                timePart = timePart.Replace(noChargeMarker, "").Trim();
            }

            if (!decimal.TryParse(timePart, NumberStyles.Number, CultureInfo.InvariantCulture, out var hours))
                continue;

            entries.Add(new SessionEntry(
                date,
                staffName,
                HoursChargeable: chargeable ? hours : 0m,
                HoursNonChargeable: chargeable ? 0m : hours));
        }

        return entries;
    }

    // Matches gCurComputeChargeableHours exactly - the sum used for a job's total
    // chargeable labour hours (Jobs/Staff reports' ChargeableTime/LabourCharge).
    public static decimal ComputeChargeableHours(string? sessionTimes)
    {
        decimal total = 0;
        foreach (var entry in Parse(sessionTimes))
            total += entry.HoursChargeable;
        return total;
    }
}
