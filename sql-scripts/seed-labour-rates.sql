-- Job reporting (ROADMAP.md Phase 3 - job reporting) needs a per-priority
-- labour hourly rate to compute LabourCharge, the same way the legacy
-- app's Jobs/Staff reports did. Real historical values recovered from the
-- restored legacy SQL Server database (JobTracking.dbo.SystemInfo on the
-- jobmatix-mssql-restore container), not invented placeholders.
-- ON CONFLICT DO NOTHING - safe to re-run, never overwrites a value
-- someone has since changed via the app's own systeminfo editor.

INSERT INTO systeminfo (info_key, info_value) VALUES
    ('LabourHourlyRatePriority1', '110.00'),
    ('LabourHourlyRatePriority2', '135.00'),
    ('LabourHourlyRatePriority3', '185.00')
ON CONFLICT (info_key) DO NOTHING;
