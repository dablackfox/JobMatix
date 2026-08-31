-- =========================================================================
-- JobMatix POS Database Schema - PostgreSQL Version
-- Converted from modCreatePOSdb.vb
-- Date: 2026-01-15
-- Target: jobmatix_pos database
-- =========================================================================

-- Drop existing tables if they exist (for clean reinstall)
-- Uncomment these if you want to recreate from scratch
-- DROP TABLE IF EXISTS Invoice_Lines CASCADE;
-- DROP TABLE IF EXISTS Invoice CASCADE;
-- DROP TABLE IF EXISTS Stock CASCADE;
-- DROP TABLE IF EXISTS Staff CASCADE;
-- DROP TABLE IF EXISTS Supplier CASCADE;
-- DROP TABLE IF EXISTS Customer CASCADE;

-- =========================================================================
-- TABLE: Staff
-- =========================================================================
CREATE TABLE IF NOT EXISTS Staff (
    staff_id SERIAL PRIMARY KEY,
    barcode VARCHAR(15) NOT NULL UNIQUE,
    lastName VARCHAR(50) NOT NULL,
    firstName VARCHAR(50) NOT NULL,
    docket_name VARCHAR(50) NOT NULL,
    position VARCHAR(50) NOT NULL DEFAULT '',
    isAdministrator BOOLEAN NOT NULL DEFAULT FALSE,
    inactive BOOLEAN NOT NULL DEFAULT FALSE,
    dateOfBirth TIMESTAMP NOT NULL,
    address TEXT NOT NULL DEFAULT '',
    suburb VARCHAR(40) NOT NULL DEFAULT '',
    state VARCHAR(30) NOT NULL DEFAULT '',
    postcode VARCHAR(10) NOT NULL DEFAULT '',
    homePhone VARCHAR(20) NOT NULL DEFAULT '',
    mobile VARCHAR(20) NOT NULL DEFAULT '',
    emailAddress VARCHAR(250) NOT NULL DEFAULT '',
    status VARCHAR(15) NOT NULL DEFAULT '',
    password VARCHAR(80) NOT NULL DEFAULT '',
    passwordHint VARCHAR(250) NOT NULL DEFAULT '',
    staffPicture BYTEA NULL,
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_staff_lastname ON Staff(lastName);

COMMENT ON TABLE Staff IS 'Staff members who can use the POS system';

-- =========================================================================
-- TABLE: Supplier
-- =========================================================================
CREATE TABLE IF NOT EXISTS Supplier (
    supplier_id SERIAL PRIMARY KEY,
    barcode VARCHAR(15) NOT NULL UNIQUE,
    supplierName VARCHAR(50) NOT NULL DEFAULT '',
    grade VARCHAR(15) NOT NULL DEFAULT '',
    inactive BOOLEAN NOT NULL DEFAULT FALSE,
    contactName VARCHAR(50) NOT NULL DEFAULT '',
    contactPosition VARCHAR(50) NOT NULL DEFAULT '',
    address TEXT NOT NULL DEFAULT '',
    suburb VARCHAR(40) NOT NULL DEFAULT '',
    state VARCHAR(30) NOT NULL DEFAULT '',
    postcode VARCHAR(10) NOT NULL DEFAULT '',
    country VARCHAR(30) NOT NULL DEFAULT '',
    businessPhone VARCHAR(20) NOT NULL DEFAULT '',
    homePhone VARCHAR(20) NOT NULL DEFAULT '',
    fax VARCHAR(20) NOT NULL DEFAULT '',
    mobile VARCHAR(20) NOT NULL DEFAULT '',
    emailAddress VARCHAR(250) NOT NULL DEFAULT '',
    website VARCHAR(250) NOT NULL DEFAULT '',
    abn VARCHAR(15) NOT NULL DEFAULT '',
    taxCode VARCHAR(15) NOT NULL DEFAULT '',
    notes TEXT NOT NULL DEFAULT '',
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_supplier_name ON Supplier(supplierName);
CREATE INDEX IF NOT EXISTS idx_supplier_barcode ON Supplier(barcode);

COMMENT ON TABLE Supplier IS 'Suppliers of stock items';

-- =========================================================================
-- TABLE: Customer
-- =========================================================================
CREATE TABLE IF NOT EXISTS Customer (
    customer_id SERIAL PRIMARY KEY,
    barcode VARCHAR(15) NOT NULL UNIQUE,
    customerName VARCHAR(100) NOT NULL DEFAULT '',
    companyName VARCHAR(100) NOT NULL DEFAULT '',
    grade VARCHAR(15) NOT NULL DEFAULT '',
    inactive BOOLEAN NOT NULL DEFAULT FALSE,
    contactName VARCHAR(50) NOT NULL DEFAULT '',
    contactPosition VARCHAR(50) NOT NULL DEFAULT '',
    address TEXT NOT NULL DEFAULT '',
    suburb VARCHAR(40) NOT NULL DEFAULT '',
    state VARCHAR(30) NOT NULL DEFAULT '',
    postcode VARCHAR(10) NOT NULL DEFAULT '',
    country VARCHAR(30) NOT NULL DEFAULT '',
    businessPhone VARCHAR(20) NOT NULL DEFAULT '',
    homePhone VARCHAR(20) NOT NULL DEFAULT '',
    fax VARCHAR(20) NOT NULL DEFAULT '',
    mobile VARCHAR(20) NOT NULL DEFAULT '',
    emailAddress VARCHAR(250) NOT NULL DEFAULT '',
    website VARCHAR(250) NOT NULL DEFAULT '',
    abn VARCHAR(15) NOT NULL DEFAULT '',
    taxCode VARCHAR(15) NOT NULL DEFAULT '',
    isAccount BOOLEAN NOT NULL DEFAULT FALSE,
    accountBalance DECIMAL(19,4) NOT NULL DEFAULT 0,
    creditLimit DECIMAL(19,4) NOT NULL DEFAULT 0,
    notes TEXT NOT NULL DEFAULT '',
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_customer_name ON Customer(customerName);
CREATE INDEX IF NOT EXISTS idx_customer_barcode ON Customer(barcode);
CREATE INDEX IF NOT EXISTS idx_customer_company ON Customer(companyName);

COMMENT ON TABLE Customer IS 'Customers who purchase from POS';

-- =========================================================================
-- TABLE: Stock
-- =========================================================================
CREATE TABLE IF NOT EXISTS Stock (
    stock_id SERIAL PRIMARY KEY,
    supplier_id INTEGER NOT NULL REFERENCES Supplier(supplier_id),
    barcode VARCHAR(40) NOT NULL UNIQUE,
    description VARCHAR(250) NOT NULL DEFAULT '',
    category VARCHAR(50) NOT NULL DEFAULT '',
    stockCode VARCHAR(40) NOT NULL DEFAULT '',
    supplierCode VARCHAR(40) NOT NULL DEFAULT '',
    inactive BOOLEAN NOT NULL DEFAULT FALSE,
    quantityInStock DECIMAL(10,2) NOT NULL DEFAULT 0,
    minStockLevel DECIMAL(10,2) NOT NULL DEFAULT 0,
    maxStockLevel DECIMAL(10,2) NOT NULL DEFAULT 0,
    reorderQuantity DECIMAL(10,2) NOT NULL DEFAULT 0,
    costPrice DECIMAL(19,4) NOT NULL DEFAULT 0,
    sellPrice DECIMAL(19,4) NOT NULL DEFAULT 0,
    taxCode VARCHAR(15) NOT NULL DEFAULT '',
    taxRate DECIMAL(5,2) NOT NULL DEFAULT 0,
    unit_of_measure VARCHAR(20) NOT NULL DEFAULT '',
    notes TEXT NOT NULL DEFAULT '',
    stockImage BYTEA NULL,
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_stock_barcode ON Stock(barcode);
CREATE INDEX IF NOT EXISTS idx_stock_supplier ON Stock(supplier_id);
CREATE INDEX IF NOT EXISTS idx_stock_description ON Stock(description);
CREATE INDEX IF NOT EXISTS idx_stock_category ON Stock(category);

COMMENT ON TABLE Stock IS 'Stock items available for sale';

-- =========================================================================
-- TABLE: Invoice
-- =========================================================================
CREATE TABLE IF NOT EXISTS Invoice (
    invoice_id SERIAL PRIMARY KEY,
    staff_id INTEGER NOT NULL REFERENCES Staff(staff_id),
    customer_id INTEGER NOT NULL REFERENCES Customer(customer_id),
    transactionType VARCHAR(15) NOT NULL DEFAULT '',
    invoiceNumber VARCHAR(20) NOT NULL UNIQUE,
    invoiceDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    dueDate TIMESTAMP NULL,
    status VARCHAR(15) NOT NULL DEFAULT '',
    subtotal DECIMAL(19,4) NOT NULL DEFAULT 0,
    taxAmount DECIMAL(19,4) NOT NULL DEFAULT 0,
    totalAmount DECIMAL(19,4) NOT NULL DEFAULT 0,
    amountPaid DECIMAL(19,4) NOT NULL DEFAULT 0,
    amountDue DECIMAL(19,4) NOT NULL DEFAULT 0,
    paymentMethod VARCHAR(20) NOT NULL DEFAULT '',
    paymentReference VARCHAR(50) NOT NULL DEFAULT '',
    notes TEXT NOT NULL DEFAULT '',
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_invoice_customer ON Invoice(customer_id);
CREATE INDEX IF NOT EXISTS idx_invoice_staff ON Invoice(staff_id);
CREATE INDEX IF NOT EXISTS idx_invoice_number ON Invoice(invoiceNumber);
CREATE INDEX IF NOT EXISTS idx_invoice_date ON Invoice(invoiceDate);
CREATE INDEX IF NOT EXISTS idx_invoice_status ON Invoice(status);

COMMENT ON TABLE Invoice IS 'Sales invoices and transactions';

-- =========================================================================
-- TABLE: Invoice_Lines
-- =========================================================================
CREATE TABLE IF NOT EXISTS Invoice_Lines (
    line_id SERIAL PRIMARY KEY,
    invoice_id INTEGER NOT NULL REFERENCES Invoice(invoice_id) ON DELETE CASCADE,
    stock_id INTEGER NOT NULL REFERENCES Stock(stock_id),
    lineNumber INTEGER NOT NULL DEFAULT 0,
    description VARCHAR(250) NOT NULL DEFAULT '',
    quantity DECIMAL(10,2) NOT NULL DEFAULT 1,
    unitPrice DECIMAL(19,4) NOT NULL DEFAULT 0,
    discount DECIMAL(5,2) NOT NULL DEFAULT 0,
    taxCode VARCHAR(15) NOT NULL DEFAULT '',
    taxRate DECIMAL(5,2) NOT NULL DEFAULT 0,
    taxAmount DECIMAL(19,4) NOT NULL DEFAULT 0,
    lineTotal DECIMAL(19,4) NOT NULL DEFAULT 0,
    notes TEXT NOT NULL DEFAULT '',
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_invoicelines_invoice ON Invoice_Lines(invoice_id);
CREATE INDEX IF NOT EXISTS idx_invoicelines_stock ON Invoice_Lines(stock_id);

COMMENT ON TABLE Invoice_Lines IS 'Line items for each invoice';

-- =========================================================================
-- TABLE: Payments
-- =========================================================================
CREATE TABLE IF NOT EXISTS Payments (
    payment_id SERIAL PRIMARY KEY,
    staff_id INTEGER NOT NULL REFERENCES Staff(staff_id),
    customer_id INTEGER NOT NULL REFERENCES Customer(customer_id),
    invoice_id INTEGER NOT NULL DEFAULT -1,
    transactionType VARCHAR(15) NOT NULL DEFAULT '',
    paymentDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    paymentMethod VARCHAR(20) NOT NULL DEFAULT '',
    paymentReference VARCHAR(50) NOT NULL DEFAULT '',
    amount DECIMAL(19,4) NOT NULL DEFAULT 0,
    notes TEXT NOT NULL DEFAULT '',
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX IF NOT EXISTS idx_payments_customer ON Payments(customer_id);
CREATE INDEX IF NOT EXISTS idx_payments_invoice ON Payments(invoice_id);
CREATE INDEX IF NOT EXISTS idx_payments_staff ON Payments(staff_id);
CREATE INDEX IF NOT EXISTS idx_payments_date ON Payments(paymentDate);

COMMENT ON TABLE Payments IS 'Payment transactions';

-- =========================================================================
-- TABLE: SystemInfo (Configuration and metadata)
-- =========================================================================
CREATE TABLE IF NOT EXISTS SystemInfo (
    info_key VARCHAR(100) PRIMARY KEY,
    info_value TEXT NOT NULL DEFAULT '',
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    date_updated TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

COMMENT ON TABLE SystemInfo IS 'System configuration and metadata';

-- Insert initial system info
INSERT INTO SystemInfo (info_key, info_value) VALUES
    ('database_version', '6.2.0'),
    ('schema_created', CURRENT_TIMESTAMP::TEXT),
    ('database_type', 'PostgreSQL'),
    ('migration_date', '2026-01-15')
ON CONFLICT (info_key) DO UPDATE 
    SET info_value = EXCLUDED.info_value,
        date_updated = CURRENT_TIMESTAMP;

-- =========================================================================
-- FUNCTIONS: Trigger to update date_modified
-- =========================================================================
CREATE OR REPLACE FUNCTION update_modified_timestamp()
RETURNS TRIGGER AS $$
BEGIN
    NEW.date_modified = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Apply triggers to tables with date_modified
CREATE TRIGGER trg_staff_modified
    BEFORE UPDATE ON Staff
    FOR EACH ROW
    EXECUTE FUNCTION update_modified_timestamp();

CREATE TRIGGER trg_supplier_modified
    BEFORE UPDATE ON Supplier
    FOR EACH ROW
    EXECUTE FUNCTION update_modified_timestamp();

CREATE TRIGGER trg_customer_modified
    BEFORE UPDATE ON Customer
    FOR EACH ROW
    EXECUTE FUNCTION update_modified_timestamp();

CREATE TRIGGER trg_stock_modified
    BEFORE UPDATE ON Stock
    FOR EACH ROW
    EXECUTE FUNCTION update_modified_timestamp();

CREATE TRIGGER trg_invoice_modified
    BEFORE UPDATE ON Invoice
    FOR EACH ROW
    EXECUTE FUNCTION update_modified_timestamp();

-- =========================================================================
-- Sample Data (Optional - for testing)
-- =========================================================================

-- Insert a default admin staff member
INSERT INTO Staff (barcode, lastName, firstName, docket_name, position, isAdministrator, dateOfBirth, password)
VALUES ('ADMIN001', 'Admin', 'System', 'Admin', 'Administrator', TRUE, '1980-01-01', 'admin123')
ON CONFLICT (barcode) DO NOTHING;

-- Insert a default customer
INSERT INTO Customer (barcode, customerName, companyName, grade)
VALUES ('CUST001', 'Walk-In Customer', 'Cash Sales', 'Standard')
ON CONFLICT (barcode) DO NOTHING;

-- Insert a default supplier
INSERT INTO Supplier (barcode, supplierName, grade)
VALUES ('SUPP001', 'Default Supplier', 'Standard')
ON CONFLICT (barcode) DO NOTHING;

-- =========================================================================
-- GRANTS (adjust as needed for your user)
-- =========================================================================
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO jobmatix_user;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO jobmatix_user;
GRANT EXECUTE ON ALL FUNCTIONS IN SCHEMA public TO jobmatix_user;

-- =========================================================================
-- Verification Queries
-- =========================================================================
-- Uncomment to run after schema creation:
-- SELECT table_name FROM information_schema.tables WHERE table_schema = 'public' ORDER BY table_name;
-- SELECT * FROM SystemInfo;

-- =========================================================================
-- End of POS Database Schema
-- =========================================================================
