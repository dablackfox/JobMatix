-- ROADMAP.md Phase 1: jobs.username/jobs.userpassword were plaintext varchar(32) -
-- widened to text to hold AES-256-GCM ciphertext (base64-encoded nonce+ciphertext+tag,
-- always longer than the original plaintext - a 32-char plaintext, the column's own
-- current max, encodes to roughly 96 base64 chars). See Services/CredentialEncryptor.cs
-- and the migrate-jobs-credentials console tool for the actual data migration - this
-- script only widens the columns, it doesn't touch any data.
ALTER TABLE jobs ALTER COLUMN username TYPE text;
ALTER TABLE jobs ALTER COLUMN userpassword TYPE text;
