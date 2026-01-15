-- JobMatix PostgreSQL Initialization Script
-- This script runs automatically when the container is first created
-- Date: 2026-01-15

-- Create additional databases for different JobMatix modules
CREATE DATABASE jobmatix_jobs;
CREATE DATABASE jobmatix_pos;
CREATE DATABASE jobmatix_backup;

-- Grant privileges to jobmatix_user
GRANT ALL PRIVILEGES ON DATABASE jobmatix_main TO jobmatix_user;
GRANT ALL PRIVILEGES ON DATABASE jobmatix_jobs TO jobmatix_user;
GRANT ALL PRIVILEGES ON DATABASE jobmatix_pos TO jobmatix_user;
GRANT ALL PRIVILEGES ON DATABASE jobmatix_backup TO jobmatix_user;

-- Connect to each database and grant schema privileges
\c jobmatix_jobs
GRANT ALL ON SCHEMA public TO jobmatix_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO jobmatix_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO jobmatix_user;

\c jobmatix_pos
GRANT ALL ON SCHEMA public TO jobmatix_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO jobmatix_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO jobmatix_user;

\c jobmatix_backup
GRANT ALL ON SCHEMA public TO jobmatix_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO jobmatix_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO jobmatix_user;

\c jobmatix_main
GRANT ALL ON SCHEMA public TO jobmatix_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO jobmatix_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO jobmatix_user;

-- Create a test table to verify setup
CREATE TABLE IF NOT EXISTS system_info (
    info_key VARCHAR(100) PRIMARY KEY,
    info_value TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO system_info (info_key, info_value) 
VALUES ('database_version', '1.0.0'),
       ('migration_status', 'initialized'),
       ('setup_date', CURRENT_TIMESTAMP::TEXT);

-- Display confirmation
SELECT 'PostgreSQL setup completed successfully!' AS status;
