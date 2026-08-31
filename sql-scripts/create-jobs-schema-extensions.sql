-- JobMatix Jobs schema extensions
-- Adds foreign keys (previously zero in this database), extends existing tables
-- to preserve full fidelity of legacy MSSQL data, and creates new tables for
-- legacy concepts with no Postgres equivalent yet (quote job parts, model
-- checklist lookup, RA attachments).
-- Run against: jobmatix_pos (see create-jobs-schema-postgresql.sql header -
-- this used to target a separate jobmatix_jobs database, now merged into
-- jobmatix_pos as JobMatix's single unified database)

BEGIN;

-- ==================== Extend existing tables ====================

ALTER TABLE tasks
  ADD COLUMN IF NOT EXISTS task_type_id INTEGER REFERENCES tasktypes(tasktype_id),
  ADD COLUMN IF NOT EXISTS performed_by_staff_id INTEGER,
  ADD COLUMN IF NOT EXISTS performed_by_staff_name VARCHAR(50) NOT NULL DEFAULT '';

ALTER TABLE parts
  ADD COLUMN IF NOT EXISTS stock_id INTEGER,
  ADD COLUMN IF NOT EXISTS long_description VARCHAR(600) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS cat1 VARCHAR(16) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS cat2 VARCHAR(16) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS cat3 VARCHAR(16) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS is_warranty_part BOOLEAN NOT NULL DEFAULT false,
  ADD COLUMN IF NOT EXISTS warranty_part_no VARCHAR(24) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS serviced_by_staff_id INTEGER,
  ADD COLUMN IF NOT EXISTS serviced_by_staff_name VARCHAR(50) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS serial_number VARCHAR(40) NOT NULL DEFAULT '';

ALTER TABLE jobchecklists
  ADD COLUMN IF NOT EXISTS comments VARCHAR(250) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS staff_id INTEGER,
  ADD COLUMN IF NOT EXISTS staff_name VARCHAR(50) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS date_updated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP;

ALTER TABLE jobother
  ADD COLUMN IF NOT EXISTS staff_name VARCHAR(50) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS barcode VARCHAR(50) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS integer_data1 INTEGER NOT NULL DEFAULT -1,
  ADD COLUMN IF NOT EXISTS integer_data2 INTEGER NOT NULL DEFAULT -1,
  ADD COLUMN IF NOT EXISTS text_data2 VARCHAR(4000) NOT NULL DEFAULT '';

ALTER TABLE documents
  ADD COLUMN IF NOT EXISTS party_info VARCHAR(1000) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS staff_id INTEGER,
  ADD COLUMN IF NOT EXISTS staff_name VARCHAR(100) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS is_image BOOLEAN NOT NULL DEFAULT false;

ALTER TABLE servicemodelchecklists
  ADD COLUMN IF NOT EXISTS rm_stock_id INTEGER;

ALTER TABLE returnauthorizations
  ADD COLUMN IF NOT EXISTS record_locked BOOLEAN NOT NULL DEFAULT false,
  ADD COLUMN IF NOT EXISTS serial_number VARCHAR(40) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS origin VARCHAR(24) NOT NULL DEFAULT 'Counter',
  ADD COLUMN IF NOT EXISTS rm_customer_id INTEGER,
  ADD COLUMN IF NOT EXISTS customer_company VARCHAR(50) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS customer_phone VARCHAR(20) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS customer_mobile VARCHAR(20) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS rm_stock_id INTEGER,
  ADD COLUMN IF NOT EXISTS item_supplier_code VARCHAR(15) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS item_barcode VARCHAR(40) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS item_cat1 VARCHAR(6) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS item_cat2 VARCHAR(6) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS item_cat3 VARCHAR(6) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS item_sell_ex DECIMAL(19,4) NOT NULL DEFAULT 0,
  ADD COLUMN IF NOT EXISTS goods_id INTEGER,
  ADD COLUMN IF NOT EXISTS invoice_no VARCHAR(20) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS invoice_date TIMESTAMP,
  ADD COLUMN IF NOT EXISTS goods_date TIMESTAMP,
  ADD COLUMN IF NOT EXISTS order_no VARCHAR(20) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS order_id INTEGER,
  ADD COLUMN IF NOT EXISTS supplier_id INTEGER,
  ADD COLUMN IF NOT EXISTS supplier_barcode VARCHAR(15) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS supplier_phone VARCHAR(20) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS supplier_fax VARCHAR(20) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS supplier_email VARCHAR(250) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS supplier_address VARCHAR(500) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS date_rma_requested TIMESTAMP,
  ADD COLUMN IF NOT EXISTS date_rma_response TIMESTAMP,
  ADD COLUMN IF NOT EXISTS rma_granted VARCHAR(1) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS supplier_rma_no VARCHAR(48) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS courier_barcode VARCHAR(32) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS date_goods_sent_back TIMESTAMP,
  ADD COLUMN IF NOT EXISTS return_result VARCHAR(15) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS staff_id_created INTEGER,
  ADD COLUMN IF NOT EXISTS staff_name_created VARCHAR(50) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS staff_id_updated INTEGER,
  ADD COLUMN IF NOT EXISTS staff_name_updated VARCHAR(50) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS date_updated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  ADD COLUMN IF NOT EXISTS rma_request_notes VARCHAR(2040) NOT NULL DEFAULT '',
  ADD COLUMN IF NOT EXISTS serial_audit_id INTEGER;

-- ==================== New tables ====================

CREATE TABLE IF NOT EXISTS quote_job_parts (
  quotepart_id INTEGER PRIMARY KEY,
  quotepart_jobid INTEGER REFERENCES jobs(job_id),
  quotepart_orderid INTEGER,
  quotepart_barcode VARCHAR(40) NOT NULL DEFAULT '',
  quotepart_description VARCHAR(50) NOT NULL DEFAULT '',
  quotepart_cat1 VARCHAR(6) NOT NULL DEFAULT '',
  quotepart_cat2 VARCHAR(6) NOT NULL DEFAULT '',
  quotepart_orderqty INTEGER NOT NULL DEFAULT 0,
  quotepart_stockid INTEGER,
  quotepart_sell_inc DECIMAL(19,4) NOT NULL DEFAULT 0,
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS model_checklist (
  checklist_id INTEGER PRIMARY KEY,
  checklist_description VARCHAR(50) NOT NULL DEFAULT '',
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS ra_attachments (
  doc_id INTEGER PRIMARY KEY,
  doc_ra_id INTEGER REFERENCES returnauthorizations(ra_id),
  doc_party_info VARCHAR(1000) NOT NULL DEFAULT '',
  doc_staff_id INTEGER,
  doc_staff_name VARCHAR(100) NOT NULL DEFAULT '',
  doc_file_format VARCHAR(30) NOT NULL DEFAULT '',
  doc_file_title VARCHAR(400) NOT NULL DEFAULT '',
  doc_file_is_image BOOLEAN NOT NULL DEFAULT false,
  doc_file_size INTEGER NOT NULL DEFAULT 0,
  doc_file_content BYTEA,
  doc_file_comments VARCHAR(2000) NOT NULL DEFAULT '',
  date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- ==================== Foreign keys on existing tables (previously zero) ====================

ALTER TABLE tasks ADD CONSTRAINT fk_tasks_job FOREIGN KEY (job_id) REFERENCES jobs(job_id);
ALTER TABLE parts ADD CONSTRAINT fk_parts_job FOREIGN KEY (job_id) REFERENCES jobs(job_id);
ALTER TABLE jobchecklists ADD CONSTRAINT fk_jobchecklists_job FOREIGN KEY (job_id) REFERENCES jobs(job_id);
ALTER TABLE jobother ADD CONSTRAINT fk_jobother_job FOREIGN KEY (job_id) REFERENCES jobs(job_id);
ALTER TABLE documents ADD CONSTRAINT fk_documents_job FOREIGN KEY (job_id) REFERENCES jobs(job_id);
ALTER TABLE returnauthorizations ADD CONSTRAINT fk_ra_job FOREIGN KEY (job_id) REFERENCES jobs(job_id);

CREATE INDEX IF NOT EXISTS idx_qjp_job ON quote_job_parts(quotepart_jobid);
CREATE INDEX IF NOT EXISTS idx_ra_attach_ra ON ra_attachments(doc_ra_id);

COMMIT;

-- Added post-hoc: JobServiceCheckLists (job-instance results) is distinct from
-- JobsCheckLists (generic checklist) and ServiceModelChecklists (the lookup template)
CREATE TABLE IF NOT EXISTS job_service_checklists (
  jobchecklist_id INTEGER PRIMARY KEY,
  job_id INTEGER REFERENCES jobs(job_id),
  rm_stock_id INTEGER,
  sequence INTEGER NOT NULL DEFAULT -1,
  task_description VARCHAR(80) NOT NULL DEFAULT '',
  status VARCHAR(32) NOT NULL DEFAULT '',
  comments VARCHAR(255) NOT NULL DEFAULT '',
  staff_id INTEGER,
  staff_name VARCHAR(50) NOT NULL DEFAULT '',
  date_updated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
CREATE INDEX IF NOT EXISTS idx_jsc_job ON job_service_checklists(job_id);

-- Widened post-hoc: legacy RA data exceeded these column widths
ALTER TABLE returnauthorizations ALTER COLUMN rastatus TYPE VARCHAR(30);
ALTER TABLE returnauthorizations ALTER COLUMN ranumber TYPE VARCHAR(60);

-- Fixed post-hoc: same systemic gap Phase 2 hit in jobmatix_pos (see
-- create-pos-schema-extensions.sql) - these 4 tables were declared with
-- INTEGER PRIMARY KEY and no SERIAL default, so they were only insertable
-- with an explicit id (fine for the legacy-data import, broken for the new
-- app inserting fresh rows). Added sequences synced past the current max id.
DO $$
DECLARE
    tbl RECORD;
BEGIN
    FOR tbl IN
        SELECT * FROM (VALUES
            ('quote_job_parts', 'quotepart_id'),
            ('model_checklist', 'checklist_id'),
            ('ra_attachments', 'doc_id'),
            ('job_service_checklists', 'jobchecklist_id')
        ) AS t(table_name, pk_column)
    LOOP
        EXECUTE format('CREATE SEQUENCE IF NOT EXISTS %I_%I_seq OWNED BY %I.%I',
            tbl.table_name, tbl.pk_column, tbl.table_name, tbl.pk_column);
        EXECUTE format('SELECT setval(''%I_%I_seq'', COALESCE((SELECT MAX(%I) FROM %I), 0) + 1, false)',
            tbl.table_name, tbl.pk_column, tbl.pk_column, tbl.table_name);
        EXECUTE format('ALTER TABLE %I ALTER COLUMN %I SET DEFAULT nextval(''%I_%I_seq'')',
            tbl.table_name, tbl.pk_column, tbl.table_name, tbl.pk_column);
    END LOOP;
END $$;

-- Added post-merge (2026-08-31): real cross-domain FKs, only possible now that jobs/RA
-- tables and POS tables (customer/staff/stock/supplier) live in the same database. These
-- columns previously held legacy numeric IDs with no referential integrity (cross-database
-- FKs aren't possible in Postgres) - some historical rows have stale/orphaned values (an
-- ID-scheme drift from the original legacy migration, not something to guess-fix), so
-- invalid values are nulled out before adding each FK rather than left to violate it.
-- goods_id/order_id on returnauthorizations are deliberately NOT given FKs here: ~74% of
-- historical RA rows predate the POS goods-received integration (added late in the legacy
-- product's life) and have no valid reference at all - not worth forcing.
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'fk_jobs_customer') THEN
        ALTER TABLE jobs ALTER COLUMN rmcustomer_id DROP NOT NULL;
        ALTER TABLE jobs ALTER COLUMN rcvdrmstaff_id DROP NOT NULL;
        ALTER TABLE jobs ALTER COLUMN techrmstaff_id DROP NOT NULL;
        ALTER TABLE jobs ALTER COLUMN deliveredrmstaff_id DROP NOT NULL;

        UPDATE jobs SET rmcustomer_id = NULL WHERE rmcustomer_id = -1 OR NOT EXISTS (SELECT 1 FROM customer c WHERE c.customer_id = jobs.rmcustomer_id);
        UPDATE jobs SET rcvdrmstaff_id = NULL WHERE rcvdrmstaff_id = -1 OR NOT EXISTS (SELECT 1 FROM staff s WHERE s.staff_id = jobs.rcvdrmstaff_id);
        UPDATE jobs SET techrmstaff_id = NULL WHERE techrmstaff_id = -1 OR NOT EXISTS (SELECT 1 FROM staff s WHERE s.staff_id = jobs.techrmstaff_id);
        UPDATE jobs SET deliveredrmstaff_id = NULL WHERE deliveredrmstaff_id = -1 OR NOT EXISTS (SELECT 1 FROM staff s WHERE s.staff_id = jobs.deliveredrmstaff_id);
        UPDATE parts SET stock_id = NULL WHERE stock_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM stock st WHERE st.stock_id = parts.stock_id);
        UPDATE parts SET serviced_by_staff_id = NULL WHERE serviced_by_staff_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM staff s WHERE s.staff_id = parts.serviced_by_staff_id);
        UPDATE tasks SET performed_by_staff_id = NULL WHERE performed_by_staff_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM staff s WHERE s.staff_id = tasks.performed_by_staff_id);
        UPDATE returnauthorizations SET rm_stock_id = NULL WHERE rm_stock_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM stock st WHERE st.stock_id = returnauthorizations.rm_stock_id);
        UPDATE returnauthorizations SET supplier_id = NULL WHERE supplier_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM supplier su WHERE su.supplier_id = returnauthorizations.supplier_id);
        UPDATE returnauthorizations SET staff_id_created = NULL WHERE staff_id_created IS NOT NULL AND NOT EXISTS (SELECT 1 FROM staff s WHERE s.staff_id = returnauthorizations.staff_id_created);
        UPDATE documents SET staff_id = NULL WHERE staff_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM staff s WHERE s.staff_id = documents.staff_id);
        UPDATE jobchecklists SET staff_id = NULL WHERE staff_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM staff s WHERE s.staff_id = jobchecklists.staff_id);
        UPDATE servicemodelchecklists SET rm_stock_id = NULL WHERE rm_stock_id IS NOT NULL AND NOT EXISTS (SELECT 1 FROM stock st WHERE st.stock_id = servicemodelchecklists.rm_stock_id);

        ALTER TABLE jobs ADD CONSTRAINT fk_jobs_customer FOREIGN KEY (rmcustomer_id) REFERENCES customer(customer_id) ON DELETE SET NULL;
        ALTER TABLE jobs ADD CONSTRAINT fk_jobs_rcvdstaff FOREIGN KEY (rcvdrmstaff_id) REFERENCES staff(staff_id) ON DELETE SET NULL;
        ALTER TABLE jobs ADD CONSTRAINT fk_jobs_techstaff FOREIGN KEY (techrmstaff_id) REFERENCES staff(staff_id) ON DELETE SET NULL;
        ALTER TABLE jobs ADD CONSTRAINT fk_jobs_deliveredstaff FOREIGN KEY (deliveredrmstaff_id) REFERENCES staff(staff_id) ON DELETE SET NULL;
        ALTER TABLE parts ADD CONSTRAINT fk_parts_stock FOREIGN KEY (stock_id) REFERENCES stock(stock_id) ON DELETE SET NULL;
        ALTER TABLE parts ADD CONSTRAINT fk_parts_servicedbystaff FOREIGN KEY (serviced_by_staff_id) REFERENCES staff(staff_id) ON DELETE SET NULL;
        ALTER TABLE tasks ADD CONSTRAINT fk_tasks_performedbystaff FOREIGN KEY (performed_by_staff_id) REFERENCES staff(staff_id) ON DELETE SET NULL;
        ALTER TABLE returnauthorizations ADD CONSTRAINT fk_ra_stock FOREIGN KEY (rm_stock_id) REFERENCES stock(stock_id) ON DELETE SET NULL;
        ALTER TABLE returnauthorizations ADD CONSTRAINT fk_ra_supplier FOREIGN KEY (supplier_id) REFERENCES supplier(supplier_id) ON DELETE SET NULL;
        ALTER TABLE returnauthorizations ADD CONSTRAINT fk_ra_staffcreated FOREIGN KEY (staff_id_created) REFERENCES staff(staff_id) ON DELETE SET NULL;
        ALTER TABLE returnauthorizations ADD CONSTRAINT fk_ra_staffupdated FOREIGN KEY (staff_id_updated) REFERENCES staff(staff_id) ON DELETE SET NULL;
        ALTER TABLE documents ADD CONSTRAINT fk_documents_staff FOREIGN KEY (staff_id) REFERENCES staff(staff_id) ON DELETE SET NULL;
        ALTER TABLE jobchecklists ADD CONSTRAINT fk_jobchecklists_staff FOREIGN KEY (staff_id) REFERENCES staff(staff_id) ON DELETE SET NULL;
        ALTER TABLE servicemodelchecklists ADD CONSTRAINT fk_smc_stock FOREIGN KEY (rm_stock_id) REFERENCES stock(stock_id) ON DELETE SET NULL;
    END IF;
END $$;

-- Added post-merge (2026-08-31): 3 legacy RAItems columns that weren't carried over when
-- this table was first ported, found while scoping the RA feature build (ROADMAP.md
-- Phase 0.4). Also fixes rastatus's stale default ('Open') to match the real 7-state
-- vocabulary the legacy app actually uses (10-Created/20-RMA-Requested/30-RMA-Granted/
-- 50-GoodsSentToSupplier/70-GoodsCompleted/95-RMA-Refused/97-RMA-Cancelled).
ALTER TABLE returnauthorizations ADD COLUMN IF NOT EXISTS ra_symptoms VARCHAR(511) NOT NULL DEFAULT '';
ALTER TABLE returnauthorizations ADD COLUMN IF NOT EXISTS date_goods_received_back TIMESTAMP;
ALTER TABLE returnauthorizations ADD COLUMN IF NOT EXISTS return_result_comment VARCHAR(64) NOT NULL DEFAULT '';
ALTER TABLE returnauthorizations ALTER COLUMN rastatus SET DEFAULT '10-Created';
