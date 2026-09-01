-- Ticket notes (public/private), per direct feedback (2026-09-01): there was nowhere
-- to enter new notes on a ticket, and no way to distinguish an internal-only note from
-- one meant to be customer-facing. A running log (many notes over time), not a single
-- overwritten field like the existing jobs.servicenotes/diagnosis columns.

CREATE TABLE IF NOT EXISTS job_notes (
    note_id SERIAL PRIMARY KEY,
    job_id INTEGER NOT NULL REFERENCES jobs(job_id) ON DELETE CASCADE,
    note_text TEXT NOT NULL,
    is_private BOOLEAN NOT NULL DEFAULT true,
    staff_name VARCHAR(50) NOT NULL DEFAULT '',
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_job_notes_job ON job_notes(job_id, date_created DESC);
