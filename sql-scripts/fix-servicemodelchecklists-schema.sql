-- ROADMAP.md Phase 0.4 / "What Changed" #9: servicemodelchecklists was ported with the
-- wrong shape - invented modelname/itemorder columns instead of the real
-- RMStockId/TaskDescription link the legacy app (frmModelEdit3.vb) actually keys by.
-- rm_stock_id was already added later (create-jobs-schema-extensions.sql) and all 21
-- real rows already have it populated - modelname is unused (always blank) and itemorder
-- is unused (always 0). checklistitem holds the real task description text.
--
-- This just corrects the column names/shape to match what the legacy code actually
-- reads/writes - it does NOT build the checklist-template editing UI or the per-job
-- completion gate that depends on it (both real, separate, not-yet-scheduled work).
ALTER TABLE servicemodelchecklists RENAME COLUMN checklistitem TO task_description;
ALTER TABLE servicemodelchecklists ALTER COLUMN rm_stock_id SET NOT NULL;
ALTER TABLE servicemodelchecklists DROP COLUMN modelname;
ALTER TABLE servicemodelchecklists DROP COLUMN itemorder;
