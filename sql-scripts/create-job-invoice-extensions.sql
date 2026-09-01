-- Job -> Invoice generation on completion (direct feedback, 2026-09-01 - Quote -> Job ->
-- Invoice pipeline, Phase 4). JobService.CompleteJobAndInvoiceAsync reuses SaleService.
-- CommitSaleAsync's proven transaction shape rather than reinventing tax/serial/stock
-- logic - see that method and ROADMAP.md for the full design.

-- Idempotency: invoice.job_number already existed (create-pos-schema-extensions.sql),
-- unused by any C# code until now - repurposed as the Job->Invoice link rather than adding
-- a new jobs.invoice_id column, which would point the wrong direction relative to every
-- other FK in this schema (invoice_lines/payments both point *at* invoice, not the other
-- way around).
--
-- A partial unique index on job_number was the original plan (the real concurrency safety
-- net for a double-click on Complete) but real migrated data blocks it: job #18380
-- genuinely has two real historical 2019 invoices sharing job_number=18380 (confirmed live -
-- not a migration artifact worth "fixing" unilaterally, since altering real financial
-- records isn't this session's call to make). Every other one of the 5,711 non-null
-- job_number rows is unique, so this is a single known exception, not a systemic problem -
-- but it means a hard DB constraint isn't available here. JobService.
-- CompleteJobAndInvoiceAsync's in-transaction "does an invoice already reference this job"
-- check is therefore the *only* protection against a double-complete, not a courtesy on top
-- of a DB guarantee - a near-simultaneous double-click could theoretically still race past
-- it. Accepted as a known, low-likelihood limitation (small team, single-till workflow)
-- rather than forcing a schema change that requires touching real financial data.

-- Two non-stock "line item" placeholders, needed because invoice_lines.stock_id is
-- NOT NULL: one shared "Labour" row (the dollar amount is computed dynamically per job
-- from billable job_time_entries hours x the job's priority rate - this row's own
-- sellprice/costprice are never read), and one "Non-Stock/Miscellaneous Part" row for the
-- rare case of a hand-typed job part with no matched stock_id (parts.stock_id is nullable,
-- unlike a Sale line, which always resolves a real StockItem first). supplier_id 0 is the
-- existing "<Default>" placeholder supplier already used elsewhere in this data.
INSERT INTO stock (supplier_id, barcode, description, category, stockcode, is_non_stock_item, taxcode, costprice, sellprice)
SELECT 0, 'LABOUR-SVC', 'Labour', 'SERVICE', 'LABOUR-SVC', true, 'GST', 0, 0
WHERE NOT EXISTS (SELECT 1 FROM stock WHERE barcode = 'LABOUR-SVC');

INSERT INTO stock (supplier_id, barcode, description, category, stockcode, is_non_stock_item, taxcode, costprice, sellprice)
SELECT 0, 'NONSTOCK-MISC', 'Non-Stock/Miscellaneous Part', 'SERVICE', 'NONSTOCK-MISC', true, 'GST', 0, 0
WHERE NOT EXISTS (SELECT 1 FROM stock WHERE barcode = 'NONSTOCK-MISC');
