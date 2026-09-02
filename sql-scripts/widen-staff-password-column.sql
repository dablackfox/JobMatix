-- ROADMAP.md Phase 1: staff.password was plaintext varchar(80) - now stores a PBKDF2 hash
-- ("iterations:base64(salt):base64(hash)", ~76 chars at today's settings, `text` so a
-- future iteration-count increase never risks truncation) instead. Investigated
-- 2026-09-02: all 45 real staff rows already have password = '' (the field has never
-- actually been used for any real login - barcode entry is this app's real identity
-- mechanism), so there is no plaintext data to migrate/re-hash.
ALTER TABLE staff ALTER COLUMN password TYPE text;
