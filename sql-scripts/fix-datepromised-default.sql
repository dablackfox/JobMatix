-- ROADMAP.md Phase 1, still-open item: "Fix Jobs.DatePromised hardcoded default
-- (2020-12-25 literal) - should be null or computed." Fixed 2026-09-02 while wiring up
-- the on-site job list + staff SMS reminder feature, which needed DatePromised to
-- actually mean something for newly created jobs.
--
-- Only changes the column's own constraint/default going forward - deliberately does NOT
-- touch the ~22,783 existing rows already sitting at the old 2020-12-25/2050-12-25
-- sentinel defaults (mix of this port's own bad default and the legacy app's own "no date
-- set" value). That's real migrated history; JobRecord.DatePromisedSentinels/
-- HasRealDatePromised guards against treating those values as real dates in any query
-- that reads this column, so a backfill isn't needed for correctness.
ALTER TABLE jobs ALTER COLUMN datepromised DROP NOT NULL;
ALTER TABLE jobs ALTER COLUMN datepromised DROP DEFAULT;
