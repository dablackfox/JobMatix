# JobMatix Revival Roadmap v2

**Last Updated**: August 31, 2026
**Supersedes**: `ROADMAP-ARCHIVE-2026-01.md` (see "What Changed" below — that version had factual errors and fictional resourcing; don't use it for planning, keep it only for history)
**Project**: Martin Fenwick, solo maintainer, AI-assisted (Claude Code)
**Status of the software**: JobMatix/JMxPOS was last live in production ~2020–2021 at Precise PCs. It is **not currently running anywhere**. This is a revival for a fresh multi-store IBG rollout, not a live cutover — there is no production system to protect or migrate off today.

---

## What Changed From v1 (and why)

The old roadmap (Jan 2026) was written after a burst of work on JMxPOS8 and contained several errors that would have caused real damage if left unfixed:

1. **"Phase 4: Remote Agent" was based on a wrong guess.** `JMxRAs62.Net` was assumed to be a data-sync/replication engine. It is actually **Return Authorisations** (supplier warranty-return tracking) — there is no sync/replication code anywhere in the old or new codebase. Cross-store sync was never designed by anyone; it's a real gap now handled explicitly in Phase 4 below, and RA has moved to Phase 3 where it actually belongs.
2. **The database abstraction layer (`JMxRetailHost620.Net`) was assumed to need "40-60 hours of wiring up."** In reality only 13 of 638 raw database call sites across the legacy apps route through it — finishing it is effectively a full rewrite anyway. **This roadmap abandons that approach** in favor of what `JMxPOS8` already proved works: a clean C#/Avalonia rewrite per app, service by service.
3. **Fictional resourcing.** v1 was denominated in "1-2 developers full-time for 6 months" with a placeholder "System Migration Team" owner. Reality: one person plus AI-assisted development, part-time. Every calendar date in v1 was wrong twice over — once because 7.5 months passed with zero activity, and again because the underlying pace assumption never matched who's doing the work. **This version does not invent calendar dates.** Phases are ordered by dependency and sized relatively (S/M/L/XL); add real dates yourself once you know your hours/week, and update `date-modified` at the top of this file whenever you revisit it — that's the whole "review cadence," no forced schedule.
4. **No live-cutover risk after all** — confirmed this is a revival of dormant software, not a migration off a system stores depend on today. This removes v1's implicit (and unstated) parallel-run/rollback pressure. It does **not** remove the question of whether old Precise PCs data (job history, customer records) is worth importing — that's a Phase 0 decision below, not a Phase 7 afterthought like v1 had it.
5. **Multi-store data model was never designed in** — it appeared 3 times across all v1 docs, always as an unelaborated bullet buried in the wrong phase (Phase 4). It's the entire reason this project was revived and needs to be decided *before* Phase 3 (12 weeks of planned work in the old plan) is built on top of a single-tenant assumption. See Phase 0.
6. **Dead modules identified and dropped**: `JMxKeyGen420_OS` (licensing — already disabled by Geoff when he open-sourced JobMatix under Apache 2.0), `backup-agent` (superseded by IBG's existing rsnapshot+DO-Spaces backup infra), MYOB Retail Manager quote-import (MYOB Retail Manager was discontinued years ago, no modern equivalent).
7. **New gaps found that weren't on v1 at all**: stocktake, customer statements, schema drift (checked-in SQL scripts no longer match the live database), zero foreign keys in the Jobs database, plaintext staff passwords, an undefined role/permissions model beyond one `isadministrator` boolean, and zero offline-resilience planning for a retail POS.
8. **Good news v1 didn't know**: the old POS's "EFTPOS integration" and the barcode scanner were both lower-risk than feared — EFTPOS was only ever manual bookkeeping (no live terminal API to port), and scanners are standard HID keyboard-wedge devices that should just work with the existing UI.

---

## Phase 0: Architecture Decisions — do this before more Phase 2/3 work

These are the decisions that would be expensive to reverse later. Everything downstream assumes an answer here.

### 0.1 Store/location data model — **you're weighing two real options**

| Option | What it costs now | What you get |
|---|---|---|
| **A. Separate Postgres per store + a new lightweight central reporting API** (recommended) | Small — no schema changes needed today, just a per-store provisioning script. The reporting API is a new, small Node/Express service on DO (matches IBG's existing infra pattern — you already run comparable services for rmm-psa), built independently, later. | Ships fastest, keeps the 40%-built JMxPOS8 investment intact, each store's POS keeps working even if the internet or the central link drops (real requirement for a till — you can't stop selling because head office is unreachable). |
| **B. Full rewrite as a webapp/Electron app, everything via API** | Large — this is a genuine full rewrite, not a port. Throws away the working Avalonia app. Offline resilience (register still needs to work if the network drops) has to be re-solved explicitly — a native app talking to a local DB gets this close to free; a browser/Electron-over-API app doesn't. | Centralized deployment/updates, one codebase instead of native+API. Only worth it if you decide native desktop is the wrong long-term bet. |

**Recommendation: A.** Keep building JMxPOS8 native, add a small central reporting service later (Phase 4) that each store's Postgres pushes summaries to/from. Revisit B only if A proves genuinely unworkable in practice — don't pre-pay for a full rewrite against a hypothetical.

If you go with A, the schema stays single-tenant per database (no `store_id` column needed) — a store's DB simply *is* that store. If you ever go with B or otherwise consolidate to one shared database, that's when `location_id` + row-level security is needed — `rmm-psa-backend`'s `feature/multi-tenant-rls` branch already has this exact tenant-middleware + RLS pattern built and is a directly reusable reference.

### 0.2 Historical data from Precise PCs

Is there an old SQL Server backup/database from the Precise PCs deployment worth importing (job history, customer records, warranty history)? If yes, add a one-time import task to Phase 1. If no surviving data or it doesn't matter, this whole project stays greenfield — no import task needed. **Open question, needs your answer before Phase 1 closes out.**

### 0.3 Feature calls needed before building

- **Recurring "Subscriptions" billing** (`ucChildSubscription.vb` in the old POS — auto-generates periodic invoices/emails) — does any current IBG store actually need recurring billing? If not, drop it and save the effort.
- **Exchange/calendar sync** (`clsExchange20.vb`, used for tech appointment scheduling) — uses the EWS API, which Microsoft is retiring in favor of Graph API. If this is still wanted, it needs a full rebuild against Graph, not a port. Recommend dropping unless you confirm it's actually used for appointment scheduling today.

---

## Phase 1: Infrastructure & Database — mostly done, needs rework

**Done (from v1):**
- ✅ Docker Postgres 15 + pgAdmin running (ports remapped this session to 5433/5050 to avoid a local conflict)
- ✅ Initial schemas for `jobmatix_pos` (8 tables) and `jobmatix_jobs` (13 tables) deployed

**New work identified this session:**
- 🔧 **Fix schema drift**: `stock.requiresserial` exists on the live database but is missing from the checked-in `sql-scripts/create-pos-schema-postgresql.sql` — someone hand-patched the live DB without updating the script. Anyone rebuilding from scratch (new store, disaster recovery) gets a broken schema today. Audit the live DB against the scripts and reconcile; treat the scripts as source of truth going forward, not the other way around.
- 🔧 **Add foreign keys to the Jobs database** — currently zero FK constraints across all 13 tables (Jobs/Tasks/Parts/Documents/ReturnAuthorizations included), unlike the POS database which does this correctly. Orphaned rows are possible today.
- 🔧 **Hash staff passwords** — currently plaintext (`Staff.password VARCHAR(80)`, confirmed live). Fix before any real deployment, this is a genuine security bug, not a style nit.
- 🔧 **Define or drop `jobmatix_main` and `jobmatix_backup`** — both are empty shells today (no tables beyond a placeholder). Decide what "main" (cross-app config? store registry?) actually needs to hold, or drop the database entirely if Phase 0.1 makes it unnecessary.
- 🔧 Fix a leftover hardcoded default (`Jobs.DatePromised` defaults to the literal `2020-12-25`, a copy-paste artifact from the original VB.NET code) — should be null or computed.
- 🗑️ **Drop `JMxRetailHost620.Net`** (the SQL-Server/Postgres abstraction layer) — confirmed to be a facade covering 13 of 638 call sites; finishing it is a full rewrite in disguise. Rewrite each remaining legacy app fresh instead, the way JMxPOS8 already did.
- 🗑️ **Drop `JMxKeyGen420_OS`** — licensing/activation, already disabled in code by Geoff, irrelevant to an internal deployment.

---

## Phase 2: POS Application (JMxPOS8) — ~40% complete, continue

**Done (from v1, verified this session — it still builds clean):**
- ✅ Core services (Database/Stock/Customer/Staff/Sale), full MVVM, 4-tab UI (Sale/Stock/Customers/Reports), complete sale workflow, stock/customer CRUD, basic reporting.

**Critical — blocks any real store from using this (size: L):**
- **Receipt printing** — this is a rewrite, not a port. The old POS used `System.Drawing.Printing.PrintDocument` and raw Win32 `WritePrinter` calls, both Windows-only. Linux path: CUPS raw queue + generated ESC/POS commands.
- **Cash drawer kick** — same underlying mechanism as receipt printing (drawer is wired through the printer's kick connector); same CUPS/ESC-POS approach, needs hardware validation per printer model.
- **Serial number tracking** — UI placeholders exist, validation/lookup logic doesn't.
- **Cash-up / EOD reconciliation** — till reconciliation across Cash/EFTPOS/CreditNote with refund handling. Nothing built yet.

**Important — real features, not yet on v1's radar (size: M each):**
- **Stocktake** (physical inventory count/reconciliation) — gap, not previously scoped at all.
- **Customer statements** — gap, not previously scoped at all.
- **Transaction lookup/void** (carried over from v1).
- **Goods received** (supplier stock receiving workflow).
- **Email integration** (send invoices/statements directly).

**Confirm before building (Phase 0.3):**
- Subscriptions/recurring billing — only build if a store actually needs it.

**Explicitly dropped:**
- Nothing else from the old POS — no other dead modules were found in it.

**Hardware — start procurement now, in parallel with software (real lead time, don't let it gate on code readiness):**
- Thermal receipt printer (80mm) — order a test unit now.
- Cash drawer — same.
- Barcode scanner — low risk (standard HID keyboard-wedge device, should already work with the existing UI), but still buy one and verify, don't assume.
- Label printer (Brother QL/Dymo) — separate driver concern from the receipt printer, currently unscoped anywhere — add an explicit test task once Phase 2's critical items are done.

---

## Phase 3: JobMatix Main Application (job/repair tracking) — 0%, largest phase

This is the biggest remaining phase. The legacy app (`JMxJT620.NET`) is ~94,000 lines across 79 files, with one 14,400-line main form.

**Core must-have (size: XL, this is most of the phase):**
- Job intake/creation, job maintenance/status workflow (largest single legacy component at ~7,200 lines), parts lookup/allocation, model/brand management, goods-in-care tracking, job docket/quote printing.

**Important (size: M each):**
- SMS notifications — **low risk**, the old code uses 4 plain-HTTP Australian SMS gateways (SMS Boss, SMS Broadcast, SMSGlobal, DirectSMS), no modem/GSM hardware dependency, trivial to port.
- General customer notifications, job reporting, customer job history, on-site/mobile job scheduling.
- **Return Authorisations** (`JMxRAs62.Net`, moved here from v1's mis-scoped "Phase 4") — supplier warranty-return tracking, ties directly into POS goods-returned logic. Belongs with the rest of the job-management workflow, not as standalone infrastructure.

**Explicitly dropped:**
- MYOB Retail Manager quote import — MYOB Retail Manager was discontinued years ago; JMxPOS8 should be the quote/order source going forward instead.

**Needs a small replacement, not a rewrite (size: S):**
- `JobMatix62.Net` is the app launcher/bootstrapper (picks between POS and Job Tracking, remembers last-used app) — not dead code, but small and mechanical. Needs an equivalent menu/picker in the new stack.

**Decision carried from Phase 0.3:**
- Exchange/calendar sync — build against Graph API if genuinely needed, otherwise drop.

---

## Phase 4: Cross-Store Reporting (replaces v1's mis-scoped "Remote Agent" phase)

v1 assumed `JMxRAs62.Net` was a data-sync/replication engine and planned a whole phase around wiring it up. It doesn't do that (see "What Changed" above) — this is genuinely new work, not a port of anything.

- Build the small central reporting API decided in Phase 0.1 (Node/Express on DO) — each store's local Postgres pushes/pulls summary data to/from it for head-office visibility.
- Each store's POS/JobMatix installation must keep working standalone without this — it's additive, not a dependency.
- Sequence this **after** Phase 2/3 are solid at a single store — don't let it block getting one store fully running.

**Backups**: don't port `backup-agent`. Replace with IBG's existing rsnapshot + DigitalOcean Spaces infrastructure plus a simple `pg_dump` cron per store — far less effort for the same outcome.

---

## Phase 5: Testing, Pilot, Rollout

- Pilot at one store first before wider IBG rollout — natural candidate is wherever you want to prove the workflow with real staff.
- No live-cutover/parallel-run risk since this is greenfield (confirmed: JobMatix isn't running anywhere today) — lower pressure than v1 assumed, but still worth validating the workflow against real repair-shop habits if any ex-Precise-PCs staff/knowledge is available.
- Hardware (Phase 2) should already be procured and tested by this point, not starting fresh here.

---

## Risk Register (updated)

| Risk | Notes |
|---|---|
| Multi-store model decision (Phase 0.1) | Blocks Phase 4 design, does **not** block Phase 2/3 — a single store's POS is architecture-agnostic in the near term. Don't let indecision here stall the work that doesn't depend on it. |
| Solo maintainer bus factor | One person + AI assistance. Keep things simple and documented; avoid cleverness that only you can maintain. |
| Hardware procurement lead time | Real, currently the easiest risk to eliminate — order test units now, in parallel with Phase 2 software work. |
| Schema-as-documentation drift | The checked-in SQL scripts already drifted from the live DB once. Make reconciling them part of Phase 1, and treat scripts as authoritative from then on. |
| EWS/calendar retirement | Only a risk if Phase 0.3 confirms calendar sync is still wanted — otherwise moot. |
| ~~Live cutover / parallel-run~~ | Not applicable — confirmed greenfield, nothing is live today. |
| ~~Printer/EFTPOS integration complexity~~ | Lower than v1 feared — EFTPOS was never a live terminal integration even in the old app (manual bookkeeping only), and the scanner is a standard HID device. Receipt printing/cash drawer are real work but well-understood (CUPS/ESC-POS), not unknowns. |

---

## Documentation Index

- `ROADMAP.md` — this file, the master plan
- `ROADMAP-ARCHIVE-2026-01.md` — superseded v1, kept for history only
- `MIGRATION-STATUS.md`, `CURRENT-STATUS.txt` — **stale**, written against v1's assumptions and fictional dates; don't use for planning until rewritten against this roadmap
- `JMxPOS8/CONVERSION_STATUS.md` — POS-specific progress detail, still broadly accurate for what's built
- `POSTGRESQL_MIGRATION_GUIDE.md` — still useful as SQL conversion reference, not as a schedule
