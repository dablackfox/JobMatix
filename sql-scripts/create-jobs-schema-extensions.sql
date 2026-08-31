-- JobMatix Jobs schema extensions
-- Adds foreign keys (previously zero in this database), extends existing tables
-- to preserve full fidelity of legacy MSSQL data, and creates new tables for
-- legacy concepts with no Postgres equivalent yet (quote job parts, model
-- checklist lookup, RA attachments).
-- Run against: jobmatix_jobs

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
