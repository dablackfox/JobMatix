# LegacyDataImport

A .NET console tool that imports a legacy JobMatix MSSQL database (the combined
`JobTracking` schema used by `JMxPOS620.Net` / `JMxJT620.NET`) into the new
split-database PostgreSQL schema (`jobmatix_pos` / `jobmatix_jobs`). Built to
validate a PostgreSQL-side equivalent of JobMatix's old built-in MSSQL
backup/restore feature, using a real restored store backup as the test case.

Legacy IDs (job numbers, invoice numbers, customer IDs, etc.) are preserved as-is
so cross-table references stay consistent with the original data; each target
table's sequence is reset to `MAX(id)+1` after import so new app-generated rows
don't collide with imported history.

## What it does

- Reads every table from the restored MSSQL database via `Microsoft.Data.SqlClient`.
- Writes into Postgres via `Npgsql`'s text-format `COPY` (not binary — Postgres's
  text-format parser applies each column's normal input function, so int-into-numeric,
  1/0-into-boolean etc. all just work, the same as a plain `INSERT` would).
- On a constraint violation (FK or PK) during the bulk `COPY`, falls back to a
  per-row `INSERT ... ON CONFLICT DO NOTHING`, so genuinely bad rows are skipped
  and logged rather than aborting the whole table, and rows already present from
  a prior run are correctly not double-counted.
- Binary blob columns (photos, staff pictures, attachments) are **not** migrated —
  bytea's `\x` hex format collides with COPY TEXT's own backslash-escape parsing.
  All other columns for those rows still migrate; only the raw file bytes are
  dropped. A future pass could restore these via binary-format COPY scoped just
  to the handful of blob columns, or via `psql \copy` with a separate binary path.

## Usage

```bash
cd LegacyDataImport
dotnet run [-- <mssql-conn-string> <pg-pos-conn-string> <pg-jobs-conn-string>]
```

All three connection strings default to this session's local dev containers if
not supplied. To point at a different store's restored backup:

```bash
dotnet run -- \
  "Server=<host>,<port>;Database=<db>;User Id=sa;Password=<pw>;TrustServerCertificate=True;" \
  "Host=<host>;Port=<port>;Database=jobmatix_pos;Username=jobmatix_user;Password=<pw>" \
  "Host=<host>;Port=<port>;Database=jobmatix_jobs;Username=jobmatix_user;Password=<pw>"
```

or set env vars `LEGACY_MSSQL_CONNSTR`, `PG_POS_CONNSTR`, `PG_JOBS_CONNSTR` instead.

## Important operational note

Preserving legacy primary key values means this should only be run against a
database that isn't simultaneously being used live — during this session's test
run, the JMxPOS8 app was left open and processed a real transaction while the
import was running in the background, landing on a `payment_id` inside the
legacy ID range. For a real store cutover, run this during a migration window
with the target app closed, not against a live system.

## Known limitations (see JobMatix `ROADMAP.md` for the bigger picture)

- Binary blobs not migrated (see above).
- A handful of rows in `GoodsReceived`/`GoodsReceivedLine`/`PurchaseOrderLine`
  reference purchase orders that don't exist anywhere in the source database
  itself (not a `-1` sentinel — genuinely dangling references, most likely
  purchase orders purged over the database's 15+ year life while receiving
  records were retained). These rows are skipped and logged, not silently
  dropped — see the run's printed Import Summary for exact counts per table.
- `JobTasks` has 542 rows referencing job IDs that don't exist in `Jobs` — same
  class of pre-existing historical orphan, skipped and logged.
