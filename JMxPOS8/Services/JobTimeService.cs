using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services;

// Ticket time tracking (ROADMAP.md - direct feedback, 2026-09-01). Concurrent by design:
// a running timer is just a job_time_entries row with end_time IS NULL, so any number of
// jobs can each have their own running timer at the same time - no separate "active
// timer" state to keep in sync, and it survives an app restart since it's a real DB row,
// not in-memory state (unlike the single-timer, memory-only pattern found in the
// rmm-psa-dashboard sibling project, which doesn't solve the same problem).
public class JobTimeService
{
    private readonly DatabaseService _db;

    public JobTimeService(DatabaseService db)
    {
        _db = db;
    }

    // Idempotent - if this exact job already has a running timer, returns it instead of
    // starting a second one on the same ticket. Deliberately does NOT check across other
    // jobs; a staff member having several running timers at once across different jobs is
    // the whole point.
    public async Task<JobTimeEntry> StartTimerAsync(int jobId, int? staffId, string staffName)
    {
        var existing = await GetRunningTimerForJobAsync(jobId);
        if (existing != null)
            return existing;

        // start_time is compared against DateTime.Now (local) to compute elapsed, so it
        // must be stamped with app-local time too - the DB container's clock runs UTC
        // (see StocktakeService's own @now override for the same reasoning), and letting
        // start_time default to the DB's CURRENT_TIMESTAMP would make every timer appear
        // to have already been running for the local UTC offset the moment it starts.
        var startTime = DateTime.Now;
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO job_time_entries (job_id, staff_id, staff_name, start_time)
            VALUES (@jobId, @staffId, @staffName, @startTime)
            RETURNING entry_id, start_time";
        AddParam(cmd, "@jobId", jobId);
        AddParam(cmd, "@staffId", (object?)staffId ?? DBNull.Value);
        AddParam(cmd, "@staffName", staffName);
        AddParam(cmd, "@startTime", startTime);

        using var reader = await Task.Run(() => cmd.ExecuteReader());
        await Task.Run(() => reader.Read());
        return new JobTimeEntry
        {
            EntryId = reader.GetInt32(0),
            JobId = jobId,
            StaffId = staffId,
            StaffName = staffName,
            StartTime = reader.GetDateTime(1)
        };
    }

    public async Task StopTimerAsync(int entryId, string description, bool billable)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            UPDATE job_time_entries
            SET end_time = @endTime, description = @description, billable = @billable
            WHERE entry_id = @entryId";
        AddParam(cmd, "@entryId", entryId);
        AddParam(cmd, "@endTime", DateTime.Now);
        AddParam(cmd, "@description", description);
        AddParam(cmd, "@billable", billable);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    public async Task<JobTimeEntry?> GetRunningTimerForJobAsync(int jobId)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT entry_id, job_id, staff_id, staff_name, start_time, end_time, description, billable
            FROM job_time_entries
            WHERE job_id = @jobId AND end_time IS NULL
            LIMIT 1";
        AddParam(cmd, "@jobId", jobId);

        using var reader = await Task.Run(() => cmd.ExecuteReader());
        if (!await Task.Run(() => reader.Read()))
            return null;
        return ReadEntry(reader);
    }

    public async Task<List<JobTimeEntry>> GetTimeEntriesForJobAsync(int jobId)
    {
        var results = new List<JobTimeEntry>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT entry_id, job_id, staff_id, staff_name, start_time, end_time, description, billable
            FROM job_time_entries
            WHERE job_id = @jobId
            ORDER BY start_time DESC";
        AddParam(cmd, "@jobId", jobId);

        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            results.Add(ReadEntry(reader));
        return results;
    }

    // Backs the status-bar "N running" indicator and its click-through filtered list -
    // every currently-running timer, across every job, joined back to enough job info to
    // show something recognizable in that list.
    public async Task<List<RunningTimerSummary>> GetRunningTimersAsync()
    {
        var results = new List<RunningTimerSummary>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT te.entry_id, te.job_id, te.staff_name, te.start_time,
                   CASE WHEN j.customercompany IN ('N/A','--','') THEN j.customername ELSE j.customercompany END AS customer,
                   j.problemshort, j.problemlong, j.problemsymptoms
            FROM job_time_entries te
            JOIN jobs j ON j.job_id = te.job_id
            WHERE te.end_time IS NULL
            ORDER BY te.start_time ASC";

        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            results.Add(new RunningTimerSummary
            {
                EntryId = reader.GetInt32(0),
                JobId = reader.GetInt32(1),
                StaffName = reader.GetString(2),
                StartTime = reader.GetDateTime(3),
                CustomerName = reader.GetString(4),
                ProblemSummary = ProblemDescriptionHelper.Summarize(
                    reader.GetString(5), reader.GetString(6), reader.GetString(7))
            });
        }
        return results;
    }

    private static JobTimeEntry ReadEntry(System.Data.IDataReader reader) => new()
    {
        EntryId = reader.GetInt32(0),
        JobId = reader.GetInt32(1),
        StaffId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
        StaffName = reader.GetString(3),
        StartTime = reader.GetDateTime(4),
        EndTime = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
        Description = reader.GetString(6),
        Billable = reader.GetBoolean(7)
    };

    private static void AddParam(System.Data.IDbCommand cmd, string name, object value)
    {
        var param = cmd.CreateParameter();
        param.ParameterName = name;
        param.Value = value;
        cmd.Parameters.Add(param);
    }
}
