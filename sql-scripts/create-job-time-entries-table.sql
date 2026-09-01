-- Ticket time tracking (direct feedback, 2026-09-01): "a start stop auto add time to
-- notes and billing was a major sore point" of the legacy app. Concurrent by design - a
-- running timer is just a row with end_time IS NULL, so any number of jobs can each have
-- their own running timer at once (a tech bouncing between an install, a screen swap, and
-- a new build) with no separate "active timer" state to keep in sync. Billing integration
-- (turning tracked hours into an invoice/labour-charge line) is deliberately not wired up
-- yet - this table only tracks the time itself.

CREATE TABLE IF NOT EXISTS job_time_entries (
    entry_id SERIAL PRIMARY KEY,
    job_id INTEGER NOT NULL REFERENCES jobs(job_id) ON DELETE CASCADE,
    staff_id INTEGER REFERENCES staff(staff_id) ON DELETE SET NULL,
    staff_name VARCHAR(50) NOT NULL DEFAULT '',
    start_time TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    end_time TIMESTAMP, -- NULL = still running
    description TEXT NOT NULL DEFAULT '',
    billable BOOLEAN NOT NULL DEFAULT true,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_job_time_entries_job ON job_time_entries(job_id);

-- Fast "every currently running timer" lookup for the status-bar indicator and its
-- filtered ticket list - a partial index since running rows are always a tiny minority.
CREATE INDEX IF NOT EXISTS idx_job_time_entries_running ON job_time_entries(job_id) WHERE end_time IS NULL;
