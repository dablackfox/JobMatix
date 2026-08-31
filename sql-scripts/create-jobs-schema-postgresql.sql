-- =========================================================================
-- JobMatix Jobs Tracking Database Schema - PostgreSQL Version
-- Converted from modCreateJobs3.vb
-- Date: 2026-01-15
-- Target: jobmatix_jobs database
-- =========================================================================

-- =========================================================================
-- TABLE: Jobs (Main job tracking table)
-- =========================================================================
CREATE TABLE IF NOT EXISTS Jobs (
    Job_Id SERIAL PRIMARY KEY,
    CustomerBarcode VARCHAR(25) NOT NULL DEFAULT '',
    RMCustomer_Id INTEGER NOT NULL DEFAULT -1,
    CustomerCompany VARCHAR(50) NOT NULL DEFAULT 'N/A',
    CustomerName VARCHAR(50) NOT NULL DEFAULT 'N/A',
    CustomerPhone VARCHAR(20) NOT NULL DEFAULT 'N/A',
    CustomerMobile VARCHAR(20) NOT NULL DEFAULT 'N/A',
    Priority VARCHAR(1) NOT NULL DEFAULT 'H',
    NominatedTech VARCHAR(50) NOT NULL DEFAULT 'N/A',
    JobStatus VARCHAR(16) NOT NULL DEFAULT '10-Created',
    GoodsInCare VARCHAR(250) NOT NULL DEFAULT 'N/A',
    GoodsOther VARCHAR(250) NOT NULL DEFAULT 'N/A',
    GoodsBrand VARCHAR(50) NOT NULL DEFAULT 'N/A',
    GoodsModel VARCHAR(50) NOT NULL DEFAULT 'N/A',
    GoodsExtras VARCHAR(250) NOT NULL DEFAULT 'N/A',
    MultiAccounts VARCHAR(1) DEFAULT 'N' NOT NULL,
    Username VARCHAR(32) NOT NULL DEFAULT '',
    UserPassword VARCHAR(32) NOT NULL DEFAULT '',
    DataBackupReqd VARCHAR(1) DEFAULT 'N' NOT NULL,
    DataDiskReqd VARCHAR(1) DEFAULT 'N' NOT NULL,
    ProblemShort VARCHAR(250) NOT NULL DEFAULT '',
    ProblemLong TEXT NOT NULL DEFAULT '',
    ProblemSymptoms VARCHAR(250) NOT NULL DEFAULT '',
    JobReturned VARCHAR(1) NOT NULL DEFAULT 'N',
    SystemUnderWarranty BOOLEAN NOT NULL DEFAULT FALSE,
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DatePromised TIMESTAMP NOT NULL DEFAULT '2020-12-25',
    RcvdStaffName VARCHAR(50) NOT NULL DEFAULT 'N/A',
    RcvdRMStaff_Id INTEGER NOT NULL DEFAULT -1,
    Diagnosis VARCHAR(550) NOT NULL DEFAULT '',
    ServiceNotes TEXT NOT NULL DEFAULT '',
    SessionTimes TEXT NOT NULL DEFAULT '',
    TotalServiceTime DECIMAL(6,2) NOT NULL DEFAULT 0,
    DateCompleted TIMESTAMP NULL,
    TechStaffName VARCHAR(50) NOT NULL DEFAULT 'N/A',
    TechRMStaff_Id INTEGER NOT NULL DEFAULT -1,
    Notifications TEXT NOT NULL DEFAULT '',
    DateDelivered TIMESTAMP NULL,
    DeliveredStaffName VARCHAR(50) NOT NULL DEFAULT 'N/A',
    DeliveredRMStaff_Id INTEGER NOT NULL DEFAULT -1,
    DateUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_jobs_customercompany_status ON Jobs(CustomerCompany, JobStatus);
CREATE INDEX IF NOT EXISTS idx_jobs_customername_status ON Jobs(CustomerName, JobStatus);
CREATE INDEX IF NOT EXISTS idx_jobs_rcvdstaff_status ON Jobs(RcvdStaffName, JobStatus);
CREATE INDEX IF NOT EXISTS idx_jobs_status_name ON Jobs(JobStatus, CustomerName);
CREATE INDEX IF NOT EXISTS idx_jobs_created_name_status ON Jobs(DateCreated, CustomerName, JobStatus);
CREATE INDEX IF NOT EXISTS idx_jobs_priority_name_status ON Jobs(Priority, CustomerName, JobStatus);
CREATE INDEX IF NOT EXISTS idx_jobs_techstaff_name_status ON Jobs(TechStaffName, CustomerName, JobStatus);

COMMENT ON TABLE Jobs IS 'Main job tracking and service repair jobs';

-- =========================================================================
-- TABLE: GoodsTypes (Reference table for goods types)
-- =========================================================================
CREATE TABLE IF NOT EXISTS GoodsTypes (
    GoodsType_Id SERIAL PRIMARY KEY,
    GoodsTypeDescription VARCHAR(50) NOT NULL DEFAULT 'N/A',
    GoodsTypeCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_goodstypes_description ON GoodsTypes(GoodsTypeDescription);

COMMENT ON TABLE GoodsTypes IS 'Reference table for types of goods serviced';

-- =========================================================================
-- TABLE: TaskTypes (Reference table for task types)
-- =========================================================================
CREATE TABLE IF NOT EXISTS TaskTypes (
    TaskType_Id SERIAL PRIMARY KEY,
    TaskDescription VARCHAR(50) NOT NULL DEFAULT 'N/A',
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_tasktypes_description ON TaskTypes(TaskDescription);

COMMENT ON TABLE TaskTypes IS 'Reference table for types of service tasks';

-- =========================================================================
-- TABLE: Brands (Reference table for brands)
-- =========================================================================
CREATE TABLE IF NOT EXISTS Brands (
    Brand_Id SERIAL PRIMARY KEY,
    BrandDescr VARCHAR(50) NOT NULL DEFAULT 'N/A',
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_brands_descr ON Brands(BrandDescr);

COMMENT ON TABLE Brands IS 'Reference table for equipment brands';

-- =========================================================================
-- TABLE: Symptoms (Reference table for symptoms)
-- =========================================================================
CREATE TABLE IF NOT EXISTS Symptoms (
    Symptom_Id SERIAL PRIMARY KEY,
    SymptomDescr VARCHAR(50) NOT NULL DEFAULT 'N/A',
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_symptoms_descr ON Symptoms(SymptomDescr);

COMMENT ON TABLE Symptoms IS 'Reference table for problem symptoms';

-- =========================================================================
-- TABLE: Tasks (Tasks performed on jobs)
-- =========================================================================
CREATE TABLE IF NOT EXISTS Tasks (
    Task_Id SERIAL PRIMARY KEY,
    Job_Id INTEGER NOT NULL,
    TaskDescr VARCHAR(50) NOT NULL DEFAULT 'N/A',
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_tasks_job ON Tasks(Job_Id);

COMMENT ON TABLE Tasks IS 'Tasks performed on specific jobs';

-- =========================================================================
-- TABLE: Parts (Parts used in jobs)
-- =========================================================================
CREATE TABLE IF NOT EXISTS Parts (
    Part_Id SERIAL PRIMARY KEY,
    Job_Id INTEGER NOT NULL,
    PartCode VARCHAR(40) NOT NULL DEFAULT '',
    PartDescr VARCHAR(250) NOT NULL DEFAULT 'N/A',
    Quantity DECIMAL(10,2) NOT NULL DEFAULT 1,
    CostPrice DECIMAL(19,4) NOT NULL DEFAULT 0,
    SellPrice DECIMAL(19,4) NOT NULL DEFAULT 0,
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_parts_job ON Parts(Job_Id);
CREATE INDEX IF NOT EXISTS idx_parts_code ON Parts(PartCode);

COMMENT ON TABLE Parts IS 'Parts used in job repairs';

-- =========================================================================
-- TABLE: ServiceModelCheckLists (Checklist for service models)
-- =========================================================================
CREATE TABLE IF NOT EXISTS ServiceModelCheckLists (
    ModelCheckList_Id SERIAL PRIMARY KEY,
    ModelName VARCHAR(50) NOT NULL DEFAULT 'N/A',
    CheckListItem VARCHAR(250) NOT NULL DEFAULT '',
    ItemOrder INTEGER NOT NULL DEFAULT 0,
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_servicemodelchecklist_model ON ServiceModelCheckLists(ModelName);

COMMENT ON TABLE ServiceModelCheckLists IS 'Checklists for servicing specific models';

-- =========================================================================
-- TABLE: JobCheckLists (Checklist items completed for jobs)
-- =========================================================================
CREATE TABLE IF NOT EXISTS JobCheckLists (
    JobCheckList_Id SERIAL PRIMARY KEY,
    Job_Id INTEGER NOT NULL,
    CheckListItem VARCHAR(250) NOT NULL DEFAULT '',
    IsCompleted BOOLEAN NOT NULL DEFAULT FALSE,
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DateCompleted TIMESTAMP NULL
);

CREATE INDEX IF NOT EXISTS idx_jobchecklists_job ON JobCheckLists(Job_Id);

COMMENT ON TABLE JobCheckLists IS 'Checklist items and completion status for jobs';

-- =========================================================================
-- TABLE: JobOther (Additional information for jobs)
-- =========================================================================
CREATE TABLE IF NOT EXISTS JobOther (
    JobOther_Id SERIAL PRIMARY KEY,
    Job_Id INTEGER NOT NULL,
    FieldName VARCHAR(50) NOT NULL DEFAULT '',
    FieldValue TEXT NOT NULL DEFAULT '',
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_jobother_job ON JobOther(Job_Id);

COMMENT ON TABLE JobOther IS 'Additional flexible fields for jobs';

-- =========================================================================
-- TABLE: ReturnAuthorizations (Return/Warranty tracking)
-- =========================================================================
CREATE TABLE IF NOT EXISTS ReturnAuthorizations (
    RA_Id SERIAL PRIMARY KEY,
    Job_Id INTEGER NULL,
    CustomerBarcode VARCHAR(25) NOT NULL DEFAULT '',
    CustomerName VARCHAR(50) NOT NULL DEFAULT '',
    SupplierName VARCHAR(50) NOT NULL DEFAULT '',
    RANumber VARCHAR(30) NOT NULL DEFAULT '',
    RADate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    RAStatus VARCHAR(20) NOT NULL DEFAULT 'Open',
    ItemDescription TEXT NOT NULL DEFAULT '',
    ProblemDescription TEXT NOT NULL DEFAULT '',
    Resolution TEXT NOT NULL DEFAULT '',
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DateCompleted TIMESTAMP NULL
);

CREATE INDEX IF NOT EXISTS idx_ra_job ON ReturnAuthorizations(Job_Id);
CREATE INDEX IF NOT EXISTS idx_ra_number ON ReturnAuthorizations(RANumber);
CREATE INDEX IF NOT EXISTS idx_ra_status ON ReturnAuthorizations(RAStatus);

COMMENT ON TABLE ReturnAuthorizations IS 'Return authorizations and warranty claims';

-- =========================================================================
-- TABLE: Documents (Document attachments for jobs)
-- =========================================================================
CREATE TABLE IF NOT EXISTS Documents (
    doc_id SERIAL PRIMARY KEY,
    Job_Id INTEGER NOT NULL,
    doc_filename VARCHAR(250) NOT NULL DEFAULT '',
    doc_description VARCHAR(250) NOT NULL DEFAULT '',
    doc_data BYTEA NULL,
    doc_type VARCHAR(50) NOT NULL DEFAULT '',
    doc_size INTEGER NOT NULL DEFAULT 0,
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_documents_job ON Documents(Job_Id);

COMMENT ON TABLE Documents IS 'File attachments associated with jobs';

-- =========================================================================
-- TABLE: SystemInfo (Configuration and metadata)
-- =========================================================================
CREATE TABLE IF NOT EXISTS SystemInfo (
    SystemKey VARCHAR(48) PRIMARY KEY,
    SystemValue VARCHAR(4000) NOT NULL DEFAULT '',
    DateCreated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    DateUpdated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

COMMENT ON TABLE SystemInfo IS 'System configuration and metadata for jobs database';

-- Insert initial system info
INSERT INTO SystemInfo (SystemKey, SystemValue) VALUES
    ('database_version', '6.2.0'),
    ('schema_created', CURRENT_TIMESTAMP::TEXT),
    ('database_type', 'PostgreSQL'),
    ('migration_date', '2026-01-15')
ON CONFLICT (SystemKey) DO UPDATE 
    SET SystemValue = EXCLUDED.SystemValue,
        DateUpdated = CURRENT_TIMESTAMP;

-- =========================================================================
-- FUNCTIONS: Trigger to update DateUpdated
-- =========================================================================
CREATE OR REPLACE FUNCTION update_dateupdated_timestamp()
RETURNS TRIGGER AS $$
BEGIN
    NEW.DateUpdated = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Apply trigger to Jobs table
CREATE TRIGGER trg_jobs_updated
    BEFORE UPDATE ON Jobs
    FOR EACH ROW
    EXECUTE FUNCTION update_dateupdated_timestamp();

-- =========================================================================
-- Sample Data (Optional - for testing)
-- =========================================================================

-- Insert some common goods types
INSERT INTO GoodsTypes (GoodsTypeDescription) VALUES
    ('Computer - Desktop'),
    ('Computer - Laptop'),
    ('Computer - Tablet'),
    ('Phone - Mobile'),
    ('Printer'),
    ('Monitor'),
    ('Hard Drive'),
    ('Other')
ON CONFLICT DO NOTHING;

-- Insert some common task types
INSERT INTO TaskTypes (TaskDescription) VALUES
    ('Virus Removal'),
    ('Data Recovery'),
    ('Hardware Repair'),
    ('Software Installation'),
    ('Upgrade RAM'),
    ('Upgrade HDD/SSD'),
    ('Screen Replacement'),
    ('Diagnostic')
ON CONFLICT DO NOTHING;

-- Insert some common brands
INSERT INTO Brands (BrandDescr) VALUES
    ('Dell'),
    ('HP'),
    ('Lenovo'),
    ('Apple'),
    ('ASUS'),
    ('Acer'),
    ('Samsung'),
    ('Microsoft'),
    ('Other')
ON CONFLICT DO NOTHING;

-- Insert some common symptoms
INSERT INTO Symptoms (SymptomDescr) VALUES
    ('Won''t Turn On'),
    ('Slow Performance'),
    ('Blue Screen'),
    ('No Display'),
    ('Won''t Boot'),
    ('Overheating'),
    ('Strange Noise'),
    ('Virus/Malware')
ON CONFLICT DO NOTHING;

-- =========================================================================
-- GRANTS
-- =========================================================================
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO jobmatix_user;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO jobmatix_user;
GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO jobmatix_user;

-- =========================================================================
-- Verification Queries
-- =========================================================================
-- SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' ORDER BY table_name;
-- SELECT * FROM SystemInfo;

-- =========================================================================
-- End of Jobs Database Schema
-- =========================================================================
