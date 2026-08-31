# JobMatix

[![License: Apache 2.0](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)

A point-of-sale, inventory, and job-tracking system for computer repair shops. Originally written by Geoff Haas (`grhaas@outlook.com`) as a Windows/.NET Framework 3.5/SQL Server application, open-sourced under Apache 2.0 in 2021 (version 6.2.6201). It ran in production for years at Precise PCs.

This fork is an active revival: porting the whole suite to .NET 8, Avalonia UI (cross-platform), and PostgreSQL, so it can run on Linux across multiple IBG-owned repair/retail stores. **The legacy VB.NET/SQL Server code below still exists in this repo for reference and as the source of business logic being ported — it is not what you should run today.**

**For current project status, what's built, what's next, and known gaps — see [`ROADMAP.md`](ROADMAP.md). That file is the source of truth, not this README.**

## What's actually running today

The active development target is `JMxPOS8` — a from-scratch .NET 8/Avalonia rewrite of the POS module (`JMxPOS620.Net`), backed by PostgreSQL instead of SQL Server. It is not a line-for-line port; large parts of the old POS (thermal printing, cash drawer control) used Windows-only APIs and are being rebuilt against Linux equivalents (CUPS/ESC-POS). See `ROADMAP.md` for what's done versus outstanding.

The main job-tracking app (`JMxJT620.NET`) has not been ported yet — that's the next major phase.

### Run the current build

```bash
git clone https://github.com/dablackfox/JobMatix.git
cd JobMatix

# Start PostgreSQL + pgAdmin (see docker-compose.yml for the actual port mapping in use)
docker-compose up -d

# Build and run the POS app
cd JMxPOS8
dotnet run --project JMxPOS8.csproj
```

Database connection settings are read from `.env` / `JMxPOS8/.env` (`JOBMATIX_PG_HOST`, `JOBMATIX_PG_PORT`, `JOBMATIX_PG_USER`, `JOBMATIX_PG_PASSWORD`) — check those files for the values actually in use in your environment, since ports get remapped when they conflict with other local services.

If `dotnet build`/`dotnet run` complains about a missing `net8.0` runtime, you have a newer .NET SDK installed with only that runtime present — either install the .NET 8 runtime, or run with `DOTNET_ROLL_FORWARD=LatestMajor` set so it rolls forward to whatever runtime you have.

### Repo layout

```
JobMatix/
├── JMxPOS8/                  # ACTIVE — .NET 8 / Avalonia POS rewrite, PostgreSQL-backed
├── docker-compose.yml        # PostgreSQL + pgAdmin for local development
├── sql-scripts/              # PostgreSQL schema (source of truth for the new DB — keep in sync with the live DB, see ROADMAP.md)
├── ROADMAP.md                # Current project plan — read this first
│
├── JMxPOS620.Net/            # LEGACY — original VB.NET POS, reference only
├── JMxJT620.NET/             # LEGACY — original VB.NET job-tracking app, not yet ported
├── JMxJT620ex.Net/           # LEGACY — job-tracking extensions
├── JobMatix62.Net/           # LEGACY — original app launcher/bootstrapper
├── JMxRAs62.Net/             # LEGACY — Return Authorisations (supplier warranty returns)
├── JMxRetailHost620.Net/     # LEGACY — SQL Server/Postgres toggle layer, not being continued (see ROADMAP.md)
├── JMxKeyGen420_OS/          # LEGACY — license key generation, not relevant to internal use, being dropped
├── backup-agent/             # LEGACY — DB backup tool, being replaced by IBG's existing backup infrastructure
├── runtime/                  # LEGACY — original Windows deployment build
└── documentation/            # Original project docs from the 2021 open-source release
```

## Legacy Windows application (original, for reference)

This section describes the original VB.NET/.NET Framework 3.5 application as released in 2021. It's kept accurate for reference — if you actually need to run the old Windows version (e.g. to cross-check business logic while porting), the steps below still apply to it. It is **not** how you run the current PostgreSQL/Linux build described above.

### Requirements
- Windows 7/10 or Windows Server
- SQL Server 2008 R2+ (Express supported)
- .NET Framework 3.5
- Visual Studio 2017+ for development

### Building
```
Open JobMatix62.Net/JobMatix62Main.sln in Visual Studio
Build Solution (Ctrl+Shift+B)
```

### Core modules
- **Job Tracking** (`JMxJT620.NET`) — repair/service job workflow (intake → diagnosis → parts → completion), quality checklists, goods-in-care, customer notifications (SMS/email)
- **Point of Sale** (`JMxPOS620.Net`) — sales/refunds/quotes/layby, stock, customer accounts, subscriptions, cash-up/EOD reconciliation
- **Return Authorisations** (`JMxRAs62.Net`) — supplier warranty-return tracking
- **License key generation** (`JMxKeyGen420_OS`) — was used to gate the original commercial product; disabled since the 2021 open-source release

Historical note: the original app included a migration path from MYOB Retail Manager (staff/supplier/stock/customer/serial history — not invoices or payments). MYOB Retail Manager was discontinued years ago; this integration is not being carried forward into the new build.

## License

Apache License 2.0 — see [`documentation/LICENSE`](documentation/LICENSE).

Original codebase copyright © Geoff Haas (`grhaas@outlook.com`), 2014–2021. Fork maintained by Martin Fenwick / Independent Business Group for internal multi-store deployment.
