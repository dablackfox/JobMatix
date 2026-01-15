# JobMatix MSSQL to PostgreSQL Migration Guide

**Date:** January 15, 2026  
**Project:** JobMatix 6.2  
**Current Database:** Microsoft SQL Server (via OleDb/SQLOLEDB)  
**Target Database:** PostgreSQL 12+

---

## Table of Contents
1. [Executive Summary](#executive-summary)
2. [Prerequisites](#prerequisites)
3. [Database Architecture Differences](#database-architecture-differences)
4. [Code Changes Required](#code-changes-required)
5. [Step-by-Step Migration Plan](#step-by-step-migration-plan)
6. [SQL Syntax Conversions](#sql-syntax-conversions)
7. [Connection String Changes](#connection-string-changes)
8. [Estimated Effort](#estimated-effort)
9. [Risks and Mitigation](#risks-and-mitigation)
10. [Testing Strategy](#testing-strategy)

---

## Executive Summary

This document outlines the complete migration path for converting the JobMatix 6.2 application suite from Microsoft SQL Server to PostgreSQL. The project consists of 6 main applications:

- **JobMatix62.Net** - Main job tracking application
- **JMxJT620.NET** - Job tracking module
- **JMxPOS620.Net** - Point of Sale system
- **JMxRAs62.Net** - Return Authorization system
- **JMxRetailHost620.Net** - Retail host integration
- **JMxBackupAgent** - Backup utility

**Scope:** 500+ VB.NET files, 14 connection points, hundreds of SQL queries

**Estimated Duration:** 3-4 weeks full-time development + 2 weeks testing

**Complexity Level:** High (due to MSSQL-specific features and stored procedures)

---

## Prerequisites

### Software Requirements

1. **PostgreSQL Server**
   - Version: 12.x or higher (preferably 14+ for better performance)
   - Extensions needed: None initially
   - Character set: UTF8

2. **.NET Dependencies**
   ```
   Npgsql (NuGet Package)
   - Version: 6.x or 7.x (latest stable)
   - Package: Npgsql
   ```

3. **Development Tools**
   - Visual Studio 2019 or later
   - pgAdmin 4 (for database management)
   - VS Code with PostgreSQL extension (optional)

### Knowledge Requirements

- Understanding of PostgreSQL administration
- VB.NET/ADO.NET programming
- SQL differences between MSSQL and PostgreSQL
- Connection pooling and transaction management

---

## Database Architecture Differences

### Key Differences Between MSSQL and PostgreSQL

| Feature | MSSQL (Current) | PostgreSQL (Target) | Impact |
|---------|----------------|---------------------|--------|
| **Provider** | System.Data.OleDb | Npgsql | High - All connection code |
| **Auto-increment** | IDENTITY(1,1) | SERIAL or GENERATED ALWAYS | High - 50+ tables |
| **String Type** | nvarchar(max) | TEXT | Medium - 200+ columns |
| **Money Type** | MONEY | DECIMAL(19,4) or NUMERIC | Medium - 100+ columns |
| **Boolean** | BIT | BOOLEAN | Medium - 50+ columns |
| **Date Functions** | GETDATE(), DATEADD() | NOW(), date + interval | High - 100+ queries |
| **String Concat** | + operator | \|\| operator or CONCAT() | High - 300+ queries |
| **Case Sensitivity** | Configurable | Case-sensitive by default | Medium |
| **Schema** | dbo.TableName | public.TableName | Low |
| **Stored Procs** | sp_* procedures | CREATE FUNCTION | High - Custom migration |
| **Transactions** | Same syntax | Same syntax | Low |
| **TOP n** | SELECT TOP n | SELECT ... LIMIT n | Medium - 50+ queries |
| **ISNULL()** | ISNULL(field, default) | COALESCE(field, default) | Medium - 100+ uses |
| **Authentication** | Windows Integrated | Username/Password | High - Security change |

---

## Code Changes Required

### 1. Assembly References

**Remove:**
```vb
Imports System.Data.OleDb
```

**Add:**
```vb
Imports Npgsql
Imports NpgsqlTypes
```

**Update Project Files (.vbproj):**
```xml
<!-- Remove or comment out -->
<!-- <Reference Include="System.Data.OleDb" /> -->

<!-- Add -->
<PackageReference Include="Npgsql" Version="7.0.0" />
```

### 2. Connection Objects

**Current Implementation (14 locations):**
```vb
Private mCnnSql As OleDbConnection
Private sqlTran1 As OleDbTransaction
Dim cmd1 As OleDbCommand
Dim adapter1 As OleDbDataAdapter
Dim reader1 As OleDbDataReader
Dim param1 As OleDbParameter
```

**New Implementation:**
```vb
Private mCnnSql As NpgsqlConnection
Private sqlTran1 As NpgsqlTransaction
Dim cmd1 As NpgsqlCommand
Dim adapter1 As NpgsqlDataAdapter
Dim reader1 As NpgsqlDataReader
Dim param1 As NpgsqlParameter
```

### 3. Connection String Locations

Files requiring connection string updates:

1. `JobMatix62.Net/modJobMatix62Main.vb` (line ~207)
2. `JMxRAs62.Net/modRAs35Main.vb` (line ~247)
3. `JMxJT620.NET/frmJobMatix42Main.vb` (line ~1984)
4. `JMxJT620ex.Net/modJT420Main.vb` (line ~197)
5. `JMxPOS620.Net/modPOS31Support.vb` (line ~1206)
6. `JMxPOS620.Net/clsGoodsInfo.vb` (line ~297)
7. `JMxPOS620.Net/clsSalesInvoiceReport.vb` (line ~124)
8. `JMxPOS620.Net/frmLookupGoods.vb` (line ~584)
9. `JMxPOS620.Net/ucChildSubscription.vb` (lines ~717, ~2261)
10. `JMxPOS620.Net/ucChildPosReports.vb` (line ~2066)
11. `JMxJT620.NET/ucChildJobReports42.vb` (line ~2606)
12. `backup-agent/.../modBackupMain.vb` (line ~291)
13. `JMxRetailHost620.Net/modAllFileAndSqlSubs.vb` (multiple)
14. `JMxJT620.NET/modJetLogin.vb` (for SQL connections)

### 4. Core Modules to Modify

**Critical Modules (Must be updated first):**

- `modAllFileAndSqlSubs.vb` - Core SQL functions
- `modSqlSupport31xDAO_SAVED.vb` - SQL support functions
- `modCreatePOSdb.vb` - Database creation for POS
- `modCreateJobs3.vb` - Database creation for Jobs
- `modJetLogin.vb` - Connection management
- `modAlterTableTrigger.vb` - Trigger management (needs complete rewrite)

**Function Signatures to Update:**

```vb
' OLD:
Public Function gbConnectSql(ByRef cnnSQL As OleDbConnection, _
                           ByVal sConnect As String) As Boolean

Public Function gbExecuteCmd(ByRef cnnSql As OleDbConnection, _
                            ByVal sSql As String, _
                            ByRef lAffected As Integer, _
                            ByRef sErrorMsg As String) As Boolean

' NEW:
Public Function gbConnectSql(ByRef cnnSQL As NpgsqlConnection, _
                           ByVal sConnect As String) As Boolean

Public Function gbExecuteCmd(ByRef cnnSql As NpgsqlConnection, _
                            ByVal sSql As String, _
                            ByRef lAffected As Integer, _
                            ByRef sErrorMsg As String) As Boolean
```

---

## Step-by-Step Migration Plan

### Phase 1: Preparation (Week 1, Days 1-2)

1. **Setup PostgreSQL Server**
   ```bash
   # Install PostgreSQL on Linux
   sudo apt-get update
   sudo apt-get install postgresql postgresql-contrib
   
   # Create database user
   sudo -u postgres createuser --interactive jobmatix_user
   
   # Create databases
   sudo -u postgres createdb jobmatix_jobs
   sudo -u postgres createdb jobmatix_pos
   ```

2. **Install Npgsql Package**
   ```bash
   # For each .vbproj file
   dotnet add package Npgsql --version 7.0.0
   ```

3. **Create Test Environment**
   - Set up development PostgreSQL instance
   - Create backup of current MSSQL databases
   - Document current database schemas

### Phase 2: Core Library Updates (Week 1, Days 3-5)

1. **Update Connection Management Module**
   - File: `JMxRetailHost620.Net/modAllFileAndSqlSubs.vb`
   - Update `gbConnectSql()` function
   - Update connection string builder
   - Test basic connectivity

2. **Update SQL Support Module**
   - File: `JMxPOS620.Net/modSqlSupport31xDAO_SAVED.vb`
   - Replace all OleDb references
   - Update error handling
   - Update parameter handling

3. **Update Database Creation Modules**
   - Files: `modCreatePOSdb.vb`, `modCreateJobs3.vb`
   - Convert CREATE TABLE statements
   - Convert data types
   - Remove/rewrite triggers

### Phase 3: SQL Syntax Conversion (Week 2)

1. **Convert CREATE TABLE Statements**
2. **Convert SELECT/INSERT/UPDATE/DELETE Queries**
3. **Convert Stored Procedures to Functions**
4. **Update Date/Time Functions**
5. **Update String Concatenation**

### Phase 4: Application-Specific Updates (Week 2-3)

1. **JobMatix Main Application** (`JobMatix62.Net`)
2. **Job Tracking Module** (`JMxJT620.NET`)
3. **POS System** (`JMxPOS620.Net`)
4. **Return Authorization** (`JMxRAs62.Net`)
5. **Retail Host** (`JMxRetailHost620.Net`)
6. **Backup Agent** (`backup-agent`)

### Phase 5: Testing (Week 3-4)

1. **Unit Testing** - Test each module independently
2. **Integration Testing** - Test application workflows
3. **Performance Testing** - Compare query performance
4. **Data Migration Testing** - Test data migration scripts

### Phase 6: Data Migration (Week 4)

1. **Export data from MSSQL**
2. **Transform data as needed**
3. **Import data to PostgreSQL**
4. **Verify data integrity**

### Phase 7: Deployment (Week 4-5)

1. **Final testing in staging**
2. **Production migration plan**
3. **Rollback procedures**
4. **Go-live**

---

## SQL Syntax Conversions

### Auto-Increment Columns

**MSSQL:**
```sql
CREATE TABLE dbo.Staff (
    staff_id INT IDENTITY (1,1) PRIMARY KEY CLUSTERED,
    ...
)
```

**PostgreSQL:**
```sql
CREATE TABLE staff (
    staff_id SERIAL PRIMARY KEY,
    ...
)
-- OR (PostgreSQL 10+, SQL standard compliant)
CREATE TABLE staff (
    staff_id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ...
)
```

### Data Type Conversions

**MSSQL → PostgreSQL:**
```sql
-- String types
nvarchar(50)     → VARCHAR(50)
nvarchar(max)    → TEXT
varchar(max)     → TEXT

-- Numeric types
INT              → INTEGER or INT
MONEY            → DECIMAL(19,4) or NUMERIC(19,4)
DECIMAL(5,2)     → DECIMAL(5,2) [same]
FLOAT            → DOUBLE PRECISION or REAL

-- Date/Time
datetime         → TIMESTAMP
date             → DATE
time             → TIME

-- Boolean
BIT              → BOOLEAN

-- Binary
varbinary(max)   → BYTEA
```

### Default Values

**MSSQL:**
```sql
date_created datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
date_modified datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
isActive BIT NOT NULL DEFAULT 0,
comments nvarchar(max) NOT NULL DEFAULT ''
```

**PostgreSQL:**
```sql
date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
date_modified TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
isActive BOOLEAN NOT NULL DEFAULT FALSE,
comments TEXT NOT NULL DEFAULT ''
```

### String Concatenation

**MSSQL:**
```sql
SELECT firstName + ' ' + lastName AS fullName FROM Staff
UPDATE Jobs SET notes = notes + @newNote WHERE job_id = @id
```

**PostgreSQL:**
```sql
SELECT firstName || ' ' || lastName AS fullName FROM Staff
UPDATE Jobs SET notes = notes || @newNote WHERE job_id = @id
-- OR using CONCAT (handles NULLs better)
SELECT CONCAT(firstName, ' ', lastName) AS fullName FROM Staff
```

### Date Functions

**MSSQL → PostgreSQL:**
```sql
-- Current date/time
GETDATE()                    → NOW() or CURRENT_TIMESTAMP
CURRENT_TIMESTAMP            → CURRENT_TIMESTAMP [same]

-- Date arithmetic
DATEADD(day, 7, date)        → date + INTERVAL '7 days'
DATEADD(month, -1, date)     → date - INTERVAL '1 month'
DATEADD(year, 1, date)       → date + INTERVAL '1 year'

-- Date difference
DATEDIFF(day, date1, date2)  → DATE_PART('day', date2 - date1)
DATEDIFF(month, d1, d2)      → DATE_PART('month', AGE(d2, d1))

-- Date parts
YEAR(date)                   → EXTRACT(YEAR FROM date)
MONTH(date)                  → EXTRACT(MONTH FROM date)
DAY(date)                    → EXTRACT(DAY FROM date)
```

### Limit/Top

**MSSQL:**
```sql
SELECT TOP 10 * FROM Jobs ORDER BY job_id DESC
SELECT TOP 1 job_id FROM Jobs WHERE status = 'Open'
```

**PostgreSQL:**
```sql
SELECT * FROM Jobs ORDER BY job_id DESC LIMIT 10
SELECT job_id FROM Jobs WHERE status = 'Open' LIMIT 1
```

### NULL Handling

**MSSQL:**
```sql
SELECT ISNULL(lastName, 'Unknown') FROM Staff
SELECT ISNULL(price, 0) * quantity FROM OrderLines
```

**PostgreSQL:**
```sql
SELECT COALESCE(lastName, 'Unknown') FROM Staff
SELECT COALESCE(price, 0) * quantity FROM OrderLines
```

### IF EXISTS Patterns

**MSSQL:**
```sql
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
              WHERE TABLE_NAME = 'Jobs' AND COLUMN_NAME = 'newField')
BEGIN
    ALTER TABLE Jobs ADD newField VARCHAR(50) NOT NULL DEFAULT ''
END
```

**PostgreSQL:**
```sql
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM information_schema.columns 
                   WHERE table_name = 'jobs' AND column_name = 'newfield') THEN
        ALTER TABLE jobs ADD COLUMN newfield VARCHAR(50) NOT NULL DEFAULT '';
    END IF;
END $$;
```

### Stored Procedures

**MSSQL:**
```sql
CREATE PROCEDURE sp_GetJobsByStatus
    @status VARCHAR(20)
AS
BEGIN
    SELECT * FROM Jobs WHERE JobStatus = @status
END
```

**PostgreSQL:**
```sql
CREATE OR REPLACE FUNCTION sp_GetJobsByStatus(p_status VARCHAR(20))
RETURNS TABLE (job_id INT, customer_name VARCHAR(100), ...) AS $$
BEGIN
    RETURN QUERY
    SELECT * FROM Jobs WHERE JobStatus = p_status;
END;
$$ LANGUAGE plpgsql;

-- Call it with:
SELECT * FROM sp_GetJobsByStatus('Open');
```

### Triggers

**MSSQL (Current - Database level trigger):**
```sql
CREATE TRIGGER trg_jobmatix_alter_table 
ON database 
FOR ALTER_TABLE 
AS 
-- Complex trigger logic
```

**PostgreSQL:**
```sql
-- PostgreSQL doesn't support DDL triggers at database level the same way
-- Options:
-- 1. Event triggers (PostgreSQL 9.3+)
CREATE OR REPLACE FUNCTION check_alter_table()
RETURNS event_trigger AS $$
DECLARE
    obj record;
BEGIN
    FOR obj IN SELECT * FROM pg_event_trigger_ddl_commands()
    LOOP
        IF obj.command_tag = 'ALTER TABLE' THEN
            -- Logic here
            RAISE NOTICE 'ALTER TABLE detected';
        END IF;
    END LOOP;
END;
$$ LANGUAGE plpgsql;

CREATE EVENT TRIGGER prevent_alter_table 
ON ddl_command_end 
EXECUTE FUNCTION check_alter_table();

-- 2. Application-level checks (recommended)
-- Handle this logic in the application code instead
```

### INFORMATION_SCHEMA Queries

**MSSQL:**
```sql
SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Jobs' AND COLUMN_NAME = 'CustomerName'

SELECT * FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME = 'Staff'
```

**PostgreSQL:**
```sql
-- Note: Table names are lowercase in PostgreSQL by default
SELECT * FROM information_schema.columns 
WHERE table_name = 'jobs' AND column_name = 'customername'

SELECT * FROM information_schema.tables 
WHERE table_name = 'staff' AND table_schema = 'public'
```

### System Stored Procedures (MSSQL Specific)

**MSSQL procedures that need replacement:**

```sql
-- These don't exist in PostgreSQL, need custom implementation:
sp_grantdbaccess       → CREATE USER / GRANT statements
sp_revokedbaccess      → DROP USER / REVOKE statements
sp_changedbowner       → ALTER DATABASE OWNER TO
sp_addlogin            → CREATE USER
sp_droplogin           → DROP USER
sp_help                → \d in psql, or information_schema queries
```

---

## Connection String Changes

### Current MSSQL Connection Strings

**Pattern 1 (Windows Authentication):**
```vb
sConnect = "Provider=SQLOLEDB; Server=" & msServer & _
           "; Trusted_Connection=true; Integrated Security=SSPI; ConnectionTimeout=10; "
```

**Pattern 2 (With Database):**
```vb
sConnect = "Provider=SQLOLEDB; Server=" & msServer & _
           "; Database=" & msDatabaseName & _
           "; Trusted_Connection=true; Integrated Security=SSPI; "
```

**Pattern 3 (MSDataShape for hierarchical data):**
```vb
sConnect = "Provider=MSDataShape; Server=" & msServer & _
           "; Trusted_Connection=true; Integrated Security=SSPI; " & _
           "; Data Provider=SQLOLEDB; Data Source=" & msServer & "; "
```

### New PostgreSQL Connection Strings

**Pattern 1 (Basic Connection):**
```vb
sConnect = "Host=" & msServer & _
           ";Port=5432" & _
           ";Database=" & msDatabaseName & _
           ";Username=" & msUsername & _
           ";Password=" & msPassword & _
           ";Timeout=10;CommandTimeout=30;"
```

**Pattern 2 (With Connection Pooling - Recommended):**
```vb
sConnect = "Host=" & msServer & _
           ";Port=5432" & _
           ";Database=" & msDatabaseName & _
           ";Username=" & msUsername & _
           ";Password=" & msPassword & _
           ";Pooling=true;Minimum Pool Size=1;Maximum Pool Size=20;" & _
           ";Timeout=10;CommandTimeout=30;"
```

**Pattern 3 (With SSL - Recommended for Production):**
```vb
sConnect = "Host=" & msServer & _
           ";Port=5432" & _
           ";Database=" & msDatabaseName & _
           ";Username=" & msUsername & _
           ";Password=" & msPassword & _
           ";SSL Mode=Require;Trust Server Certificate=true;" & _
           ";Pooling=true;Maximum Pool Size=20;"
```

**Configuration Storage:**
```vb
' Store these securely - NOT in code!
' Options:
' 1. app.config / web.config
' 2. Environment variables
' 3. Encrypted configuration file
' 4. Windows Credential Manager (for desktop apps)

' Example app.config:
'<configuration>
'  <connectionStrings>
'    <add name="JobMatixDB" 
'         connectionString="Host=localhost;Port=5432;Database=jobmatix_pos;Username=jobmatix_user;Password=SecureP@ss123"
'         providerName="Npgsql"/>
'  </connectionStrings>
'</configuration>
```

### Security Considerations

1. **No More Windows Authentication:**
   - Must create PostgreSQL users
   - Must manage passwords securely
   - Consider using connection string encryption

2. **Network Security:**
   - Configure `pg_hba.conf` for proper access control
   - Use SSL/TLS for remote connections
   - Limit access by IP address

3. **Password Storage:**
   - NEVER hardcode passwords
   - Use encrypted configuration
   - Consider using pgpass file for automation

---

## Estimated Effort

### Development Time Breakdown

| Phase | Task | Estimated Hours | Risk Level |
|-------|------|----------------|-----------|
| **Phase 1** | PostgreSQL Setup & Configuration | 8h | Low |
| | Npgsql Installation & Testing | 4h | Low |
| | Environment Setup | 4h | Low |
| **Phase 2** | Core Module Updates (5 files) | 40h | High |
| | Connection Management | 12h | High |
| | SQL Support Functions | 16h | High |
| | Database Creation Scripts | 12h | Medium |
| **Phase 3** | SQL Syntax Conversion | 60h | High |
| | CREATE TABLE statements (50+) | 20h | Medium |
| | Query conversions (500+) | 30h | High |
| | Stored Procedure rewrite (10+) | 10h | High |
| **Phase 4** | Application Updates | 80h | Medium |
| | JobMatix Main (50 files) | 20h | Medium |
| | POS System (80 files) | 25h | Medium |
| | Job Tracking (60 files) | 20h | Medium |
| | Other Modules (40 files) | 15h | Medium |
| **Phase 5** | Testing | 60h | High |
| | Unit Tests | 20h | Medium |
| | Integration Tests | 25h | High |
| | Performance Tests | 15h | Medium |
| **Phase 6** | Data Migration | 24h | High |
| | Migration Script Development | 12h | High |
| | Testing & Validation | 12h | High |
| **Phase 7** | Documentation & Deployment | 16h | Medium |
| | Update Documentation | 8h | Low |
| | Deployment Procedures | 8h | Medium |
| **TOTAL** | | **296 hours** | |
| | | **~7-8 weeks** | |

### Cost Factors

- **Developer Time:** 300 hours @ your rate
- **PostgreSQL License:** Free (open source)
- **Testing Environment:** Server costs
- **Training:** Team training on PostgreSQL
- **Contingency:** +25% for unforeseen issues

---

## Risks and Mitigation

### Technical Risks

| Risk | Impact | Probability | Mitigation Strategy |
|------|--------|------------|-------------------|
| **Trigger/Stored Proc Complexity** | High | High | Start with simple queries first; consider moving logic to app layer |
| **Performance Degradation** | High | Medium | Benchmark early; optimize indexes; use EXPLAIN ANALYZE |
| **Data Type Incompatibilities** | Medium | Medium | Create comprehensive conversion mapping; test thoroughly |
| **Authentication Changes** | High | Low | Plan credential management strategy early; test security |
| **Concurrent Connection Issues** | Medium | Low | Implement proper connection pooling; test under load |
| **Case Sensitivity** | Medium | High | Use lowercase table/column names consistently; test all queries |
| **Missing MSSQL Functions** | Medium | Medium | Create PostgreSQL equivalents; document differences |
| **Transaction Behavior** | Medium | Low | Test transaction isolation levels; adjust if needed |

### Business Risks

| Risk | Impact | Mitigation |
|------|--------|------------|
| **Extended Downtime** | High | Phased rollout; parallel running; quick rollback plan |
| **Data Loss** | Critical | Multiple backups; validation scripts; dry runs |
| **User Training** | Medium | Minimal (backend change); document any visible changes |
| **Third-party Integrations** | Medium | Audit all external connections; test thoroughly |

### Mitigation Strategies

1. **Parallel Operation Period:**
   - Run MSSQL and PostgreSQL side-by-side initially
   - Compare results for consistency
   - Gradual cutover by module

2. **Comprehensive Testing:**
   - Automated test suite for SQL queries
   - Data validation scripts
   - Performance benchmarking

3. **Rollback Plan:**
   - Keep MSSQL operational for 30 days
   - Automated rollback scripts
   - Clear rollback decision criteria

4. **Documentation:**
   - Document all SQL conversions
   - Create PostgreSQL administration guide
   - Update all technical documentation

---

## Testing Strategy

### 1. Unit Testing

**SQL Query Tests:**
```vb
' Create test harness for each converted query
' Example test class:
<TestClass>
Public Class PostgreSQLMigrationTests
    
    <TestMethod>
    Public Sub TestCreateStaffTable()
        ' Arrange
        Dim conn As New NpgsqlConnection(testConnectionString)
        Dim sql As String = GetCreateStaffTableSQL()
        
        ' Act
        conn.Open()
        Dim result = ExecuteSQL(conn, sql)
        
        ' Assert
        Assert.IsTrue(TableExists(conn, "staff"))
        conn.Close()
    End Sub
    
End Class
```

**Test Coverage Required:**
- All CREATE TABLE statements
- All INSERT/UPDATE/DELETE operations
- All SELECT queries with complex JOINs
- All date/time calculations
- All string operations
- All stored procedure replacements

### 2. Integration Testing

**Test Scenarios:**
1. **Complete Sales Transaction (POS)**
   - Create customer
   - Add items to cart
   - Process payment
   - Generate invoice
   - Verify all tables updated

2. **Job Workflow (JobMatix)**
   - Create new job
   - Add parts and labor
   - Update status
   - Complete job
   - Verify history

3. **Return Authorization**
   - Create RA
   - Process return
   - Update inventory
   - Generate credit

4. **Reporting**
   - Run all standard reports
   - Verify data accuracy
   - Check performance

### 3. Performance Testing

**Benchmarks to Establish:**
```sql
-- Before migration (MSSQL)
-- Record execution times for:
1. Dashboard load query (most frequent)
2. Search customers query
3. Generate daily sales report
4. Inventory update batch
5. Month-end processing

-- After migration (PostgreSQL)
-- Compare and tune to meet or exceed MSSQL performance
```

**Performance Tuning:**
- Create appropriate indexes
- Analyze and vacuum regularly
- Adjust PostgreSQL configuration (shared_buffers, work_mem, etc.)
- Use EXPLAIN ANALYZE for slow queries

### 4. Data Migration Testing

**Validation Scripts:**
```sql
-- Row count validation
SELECT 'MSSQL Staff Count' AS source, COUNT(*) AS row_count FROM MSSQL.dbo.Staff
UNION ALL
SELECT 'PostgreSQL Staff Count', COUNT(*) FROM postgresql.public.staff;

-- Data integrity checks
SELECT 
    'Orphaned orders' AS check_name,
    COUNT(*) AS issue_count
FROM orders o
LEFT JOIN customers c ON o.customer_id = c.customer_id
WHERE c.customer_id IS NULL;

-- Date range validation
SELECT 
    MIN(date_created) AS earliest_date,
    MAX(date_created) AS latest_date
FROM jobs;
```

### 5. User Acceptance Testing (UAT)

**Test Cases:**
1. Daily operations (normal workflow)
2. Month-end procedures
3. Report generation
4. Backup/restore
5. Error handling
6. Edge cases

---

## Important Files Reference

### Files Requiring Major Changes (Priority Order)

1. **JMxRetailHost620.Net/modAllFileAndSqlSubs.vb**
   - Lines: ~4100
   - Functions: 50+
   - Contains: Core SQL connection and execution functions
   - Priority: CRITICAL

2. **JMxPOS620.Net/modSqlSupport31xDAO_SAVED.vb**
   - Lines: ~2200
   - Contains: SQL support functions, stored procedure execution
   - Priority: CRITICAL

3. **JMxPOS620.Net/modCreatePOSdb.vb**
   - Lines: ~3500
   - Contains: Complete POS database schema creation
   - Tables: 20+
   - Priority: HIGH

4. **JMxRetailHost620.Net/modCreateJobs3.vb**
   - Lines: ~1200
   - Contains: Job tracking database schema
   - Tables: 15+
   - Priority: HIGH

5. **JMxJT620.NET/modAlterTableTrigger.vb**
   - Lines: ~250
   - Contains: Database-level DDL trigger
   - Priority: HIGH (may need complete redesign)

6. **All Connection Management Files:**
   - JobMatix62.Net/modJobMatix62Main.vb
   - JMxRAs62.Net/modRAs35Main.vb
   - JMxJT620.NET/frmJobMatix42Main.vb
   - Others listed in Section 4.3

### Configuration Files

- **app.config** files in each project
- **AssemblyInfo.vb** files (version updates)
- **.vbproj** files (NuGet package references)

---

## PostgreSQL Setup Guide

### Installation (Ubuntu/Debian)

```bash
# Update package list
sudo apt update

# Install PostgreSQL
sudo apt install postgresql postgresql-contrib

# Start PostgreSQL service
sudo systemctl start postgresql
sudo systemctl enable postgresql

# Verify installation
psql --version
```

### Initial Configuration

```bash
# Switch to postgres user
sudo -i -u postgres

# Create application user
createuser --interactive --pwprompt jobmatix_user
# Enter password when prompted
# Answer 'n' to superuser, 'y' to create databases

# Create databases
createdb -O jobmatix_user jobmatix_jobs
createdb -O jobmatix_user jobmatix_pos
createdb -O jobmatix_user jobmatix_backup

# Exit postgres user
exit
```

### Configure PostgreSQL for Remote Access

Edit `/etc/postgresql/14/main/postgresql.conf`:
```ini
# Listen on all interfaces (or specific IP)
listen_addresses = '*'

# Performance tuning (adjust based on your server)
shared_buffers = 256MB
work_mem = 16MB
maintenance_work_mem = 128MB
effective_cache_size = 1GB
max_connections = 100
```

Edit `/etc/postgresql/14/main/pg_hba.conf`:
```ini
# Allow password authentication from application server
# TYPE  DATABASE        USER            ADDRESS         METHOD
host    all            jobmatix_user    192.168.1.0/24  md5
host    all            jobmatix_user    127.0.0.1/32    md5
```

Restart PostgreSQL:
```bash
sudo systemctl restart postgresql
```

### Create Database Schemas

```sql
-- Connect to database
psql -U jobmatix_user -d jobmatix_jobs

-- Create schema if needed (optional, default is 'public')
CREATE SCHEMA IF NOT EXISTS jobmatix;

-- Set search path
ALTER DATABASE jobmatix_jobs SET search_path TO public;

-- Grant permissions
GRANT ALL PRIVILEGES ON DATABASE jobmatix_jobs TO jobmatix_user;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO jobmatix_user;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO jobmatix_user;
```

---

## Quick Start Conversion Example

### Before (MSSQL/OleDb):

```vb
' Connection
Dim mCnnSql As OleDbConnection
Dim sConnect As String

sConnect = "Provider=SQLOLEDB; Server=" & msServer & _
           "; Database=JobMatixPOS; Trusted_Connection=true;"

mCnnSql = New OleDbConnection
mCnnSql.ConnectionString = sConnect
mCnnSql.Open()

' Query
Dim cmd As New OleDbCommand
cmd.Connection = mCnnSql
cmd.CommandText = "SELECT TOP 10 * FROM Staff WHERE isActive = 1 ORDER BY lastName"

Dim reader As OleDbDataReader = cmd.ExecuteReader()
While reader.Read()
    Console.WriteLine(reader("firstName").ToString() + " " + reader("lastName").ToString())
End While
reader.Close()
mCnnSql.Close()
```

### After (PostgreSQL/Npgsql):

```vb
' Connection
Dim mCnnSql As NpgsqlConnection
Dim sConnect As String

sConnect = "Host=" & msServer & _
           ";Port=5432;Database=jobmatix_pos" & _
           ";Username=" & msUsername & _
           ";Password=" & msPassword & _
           ";Pooling=true;"

mCnnSql = New NpgsqlConnection(sConnect)
mCnnSql.Open()

' Query (note: lowercase table name, LIMIT instead of TOP, true instead of 1)
Dim cmd As New NpgsqlCommand
cmd.Connection = mCnnSql
cmd.CommandText = "SELECT * FROM staff WHERE isactive = true ORDER BY lastname LIMIT 10"

Dim reader As NpgsqlDataReader = cmd.ExecuteReader()
While reader.Read()
    Console.WriteLine(reader("firstname").ToString() & " " & reader("lastname").ToString())
End While
reader.Close()
mCnnSql.Close()
```

---

## Recommendations

### Approach Options

**Option A: Big Bang Migration (Not Recommended)**
- Convert everything at once
- High risk, high stress
- Shorter calendar time
- All-or-nothing deployment

**Option B: Phased Migration (Recommended)**
- Convert one application at a time
- Lower risk per phase
- Longer calendar time
- Easier rollback

**Option C: Hybrid Approach**
- Create database abstraction layer
- Support both MSSQL and PostgreSQL
- Gradual migration
- Highest initial effort, safest long-term

### Recommended Approach: Option B

**Phase 1:** Backup Agent (simplest, lowest risk)  
**Phase 2:** POS System (self-contained)  
**Phase 3:** Job Tracking (core business logic)  
**Phase 4:** Return Authorization  
**Phase 5:** Main JobMatix application  

### Key Success Factors

1. ✅ **Automated Testing** - Build comprehensive test suite first
2. ✅ **Version Control** - Branch strategy for parallel development
3. ✅ **Documentation** - Document every SQL conversion decision
4. ✅ **Backup Strategy** - Multiple backups before and during migration
5. ✅ **Monitoring** - Set up PostgreSQL monitoring and logging
6. ✅ **Performance Baseline** - Establish metrics before migration
7. ✅ **Training** - Train team on PostgreSQL basics
8. ✅ **Rollback Plan** - Clear criteria and procedures

---

## Appendix A: Common Conversion Patterns

### Pattern 1: Connection Management

```vb
' Create connection wrapper function
Public Function GetDatabaseConnection() As NpgsqlConnection
    Dim conn As New NpgsqlConnection
    conn.ConnectionString = GetConnectionString() ' From config
    Return conn
End Function

' Use Using statements for automatic cleanup
Using conn As NpgsqlConnection = GetDatabaseConnection()
    conn.Open()
    ' ... your code ...
End Using ' Automatically closes connection
```

### Pattern 2: Parameterized Queries

```vb
' OLD (vulnerable to SQL injection if building strings)
Dim sql As String = "SELECT * FROM Staff WHERE staff_id = " & staffId

' NEW (parameterized - safe and efficient)
Dim sql As String = "SELECT * FROM staff WHERE staff_id = @staffid"
Dim cmd As New NpgsqlCommand(sql, conn)
cmd.Parameters.AddWithValue("@staffid", staffId)
```

### Pattern 3: Transaction Handling

```vb
' PostgreSQL transactions work similarly
Using conn As New NpgsqlConnection(connString)
    conn.Open()
    Using trans As NpgsqlTransaction = conn.BeginTransaction()
        Try
            ' Execute multiple commands
            Dim cmd1 As New NpgsqlCommand("INSERT INTO ...", conn, trans)
            cmd1.ExecuteNonQuery()
            
            Dim cmd2 As New NpgsqlCommand("UPDATE ...", conn, trans)
            cmd2.ExecuteNonQuery()
            
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw
        End Try
    End Using
End Using
```

---

## Appendix B: PostgreSQL Administration Commands

```bash
# psql commands (run from terminal)
psql -U jobmatix_user -d jobmatix_jobs    # Connect to database
\l                                         # List databases
\dt                                        # List tables
\d table_name                             # Describe table
\du                                        # List users
\q                                         # Quit

# Backup
pg_dump -U jobmatix_user jobmatix_jobs > backup.sql

# Restore
psql -U jobmatix_user jobmatix_jobs < backup.sql

# Vacuum (maintenance)
VACUUM ANALYZE;

# Check database size
SELECT pg_size_pretty(pg_database_size('jobmatix_jobs'));
```

---

## Appendix C: Resources and References

### Official Documentation
- **Npgsql:** https://www.npgsql.org/doc/
- **PostgreSQL:** https://www.postgresql.org/docs/
- **SQL Migration Guide:** https://wiki.postgresql.org/wiki/Converting_from_other_Databases_to_PostgreSQL

### Tools
- **pgAdmin 4:** Database administration GUI
- **DBeaver:** Universal database tool
- **pgloader:** Automated migration tool (can help with data migration)
- **SQL Workbench/J:** Cross-database SQL tool

### Migration References
- Microsoft SQL Server to PostgreSQL migration guide
- Data type mapping references
- Performance tuning guides

---

## Contact and Support

For questions during migration:
- PostgreSQL Community: postgresql.org/support
- Npgsql GitHub: github.com/npgsql/npgsql
- Stack Overflow: [postgresql] and [npgsql] tags

---

**Document Version:** 1.0  
**Last Updated:** January 15, 2026  
**Next Review:** Start of migration project
