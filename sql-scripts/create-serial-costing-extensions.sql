-- Phase 6.1 (ROADMAP.md): per-unit cost tracking for serialized items, so COGS/margin
-- reporting can use what a specific unit actually cost instead of stock.costprice's
-- "latest cost wins" convention.

-- Each serial now remembers its own landed cost and which goods-received line it came
-- in on, instead of only ever being able to look up the SKU's current (possibly since
-- overwritten) cost price.
ALTER TABLE serial_audit
    ADD COLUMN IF NOT EXISTS unit_cost NUMERIC(19,4) NOT NULL DEFAULT 0,
    ADD COLUMN IF NOT EXISTS received_line_id INTEGER REFERENCES goods_received_line(line_id);

CREATE INDEX IF NOT EXISTS idx_serial_audit_received_line ON serial_audit(received_line_id);

-- invoice_lines.serial_audit_id already exists from the legacy port (15,619 historical
-- rows have it populated) but was never constrained. 1,018 of those (6.5%, consistent
-- with the ~5% orphan rate seen in the 2026-08-31 cross-database FK merge) don't resolve
-- to a real serial_audit row - null them out before adding the FK, same pattern as that
-- merge used.
UPDATE invoice_lines
SET serial_audit_id = NULL
WHERE serial_audit_id IS NOT NULL
  AND NOT EXISTS (SELECT 1 FROM serial_audit sa WHERE sa.serial_id = invoice_lines.serial_audit_id);

ALTER TABLE invoice_lines
    ADD CONSTRAINT invoice_lines_serial_audit_id_fkey
    FOREIGN KEY (serial_audit_id) REFERENCES serial_audit(serial_id) ON DELETE SET NULL;
