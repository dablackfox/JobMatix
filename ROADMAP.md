# JobMatix Revival Roadmap v3

**Last Updated**: August 31, 2026
**Supersedes**: `ROADMAP-ARCHIVE-2026-08.md` (v2 — Phase 2 was accurate and is now complete; Phase 3 was a thin, partly-wrong placeholder, corrected below after a real code audit)
**Project**: Martin Fenwick, solo maintainer, AI-assisted (Claude Code)
**Status of the software**: JobMatix/JMxPOS was last live in production ~2020–2021 at Precise PCs. It is **not currently running anywhere**. This is a revival for a fresh multi-store IBG rollout, not a live cutover.

---

## What Changed From v2 (and why)

v2 got Phase 2 right and shipped it. Phase 3 was still a guess — a thin bullet list written before anyone had actually read `JMxJT620.NET`. This version replaces that guess with findings from a real line-by-line audit of the legacy Job Tracking, Return Authorisations, and launcher source, cross-checked against the live `jobmatix_jobs` Postgres database (which already holds real migrated data: 26,383 jobs, 30,888 parts, 46,571 tasks, 1,323 return authorisations).

1. **Phase 2 is done.** Cash-up, Stocktake, Customer Statements, Goods Received, and Transaction Lookup/Void all shipped and were verified against live data this session. What's left in Phase 2 is only the items that were always blocked on a decision (receipt printing/cash drawer hardware, subscriptions, email/SMTP) — see Phase 2 below.
2. **"Zero foreign keys in the Jobs database" (v1/v2's Phase 1 gap) is stale — already fixed.** `create-jobs-schema-extensions.sql` added 10 FK constraints (jobs→documents/jobchecklists/jobother/parts/returnauthorizations/tasks/job_service_checklists, plus quote_job_parts, ra_attachments, tasktypes) before this session started. Verified live. Don't keep re-flagging this.
3. **A new, previously-unknown security gap found**: `jobs.username`/`jobs.userpassword` store **customers' own PC login credentials** in plaintext (collected at intake so a tech can log into the machine being repaired) — 6,681 of 26,383 real jobs have a populated username, 7,591 have a password. This is distinct from the already-known `staff.password` plaintext issue (that one's an internal login; this one is customers' personal credentials, arguably worse exposure). Both need fixing, treat as two separate line items.
4. **A second instance of Phase 2's missing-SERIAL-sequence bug found and fixed in the Jobs database.** Same root cause as the 17-table bug already fixed in `jobmatix_pos`: `quote_job_parts`, `model_checklist`, `ra_attachments`, `job_service_checklists` were declared `INTEGER PRIMARY KEY` with no sequence. Fixed live and in `create-jobs-schema-extensions.sql` this session.
5. **Return Authorisations does not belong "with the rest of job-management" after all — it's barely coupled to jobs.** Real data: only 126 of 1,323 RAs (9.5%) reference a job at all; 60.7% originate from stock, 28.4% from a counter return. The Job-linked RA case is just a customer-lookup convenience, not a workflow dependency. RA's actual integration point is a POS-side stock/serial-audit transaction (`POS_GoodsReturned`) that already has a matching Postgres schema (`supplier_returns`, `supplier_return_line`, `serial_audit`, `serial_audit_trail` — all already modeled, no gap). **RA can be built as a standalone feature attached to Phase 2's POS/Void/Refund area, independent of and not blocked on the rest of Phase 3.** It is functionally a POS satellite module that happens to store its rows in the jobs database for historical-schema reasons.
6. **"Customer job history" should not be a separate Job Tracking screen.** The legacy app already combines POS purchase history and job history into one hybrid screen (`frmCustHistory3.vb`). The right port target is a **new "Jobs" sub-tab on Phase 2's existing Customer screen** (`JMxPOS8/ViewModels/CustomerViewModel.cs`, which already has Invoices/Item Sales/Payments/Quotes sub-tabs) — not a standalone Job Tracking feature. Small, low-risk, and can be built any time (doesn't need to wait for the rest of Phase 3).
7. **"On-site/mobile job scheduling" isn't a scheduling feature.** It's a magic-string flag (`GoodsInCare='ON-SITE JOB;'`) plus a filtered list view using the job's ordinary `DatePromised` field — there is no calendar engine, no mobile app, nothing to build beyond a filtered query. Two *separate* integrations hang off that flag: Exchange/EWS calendar sync (real but optional, dying API — **recommend dropping outright**, see Phase 0.3) and a staff SMS reminder poller (small, reuses the SMS client already being built, worth keeping).
8. **`GoodsInCare` isn't free text — it's a delimited multi-item collection** (type/brand/model/serial per item, up to several items per job, packed into one 250-char column via legacy encode/decode logic). The current ported schema copied the flat-column shape faithfully but not the actual semantics. This needs an explicit decision before building the intake/goods-in-care UI — see Phase 0.4.
9. **Two real schema gaps found that block features, not just cosmetic drift**: `service_model_checklists` was ported with the wrong shape entirely (invented `ModelName`/`ItemOrder` columns instead of the real `RMStockId`/`TaskDescription` link) — blocks porting the service-checklist-template feature as-is; `returnauthorizations` is missing three legacy columns (`RA_Symptoms`, `RA_DateGoodsReceivedBack`, `RA_ReturnResultComment`).
10. **The legacy job-reporting module uses SQL Server's proprietary `SHAPE` syntax and a T-SQL scalar function** — neither has a Postgres equivalent. This is a real rewrite (two queries + in-app grouping, move the scalar-function logic into C#), not a mechanical port — flagged so it isn't underestimated.
11. **RA attachment blob content was never actually migrated** — metadata only (title/size/format), `doc_file_content` is `NULL` on all 8 RA attachment rows and all 142 job document rows. Whether the real bytes are recoverable from the old SQL Server backup is an open question worth resolving before promising "view old attachments" as a Phase 3 feature.
12. **The Job Tracking app never had its own database connection historically — confirmed architecturally significant.** The whole legacy suite ran one process against one shared SQL Server database via one shared connection; `CustomerBarcode` on a job was always just a lookup key into the same database, never a real cross-database reference. Now that POS and Jobs are two separate Postgres databases, **the new Job Tracking app needs an explicit answer for how it reaches POS data** (customer/stock/staff lookups, labour pricing) — this is new design work, not something the legacy code already solved. See Phase 0.4. (A partial, never-finished dual-connection retrofit exists in the legacy VB.NET source from an earlier session — `DatabaseConfig.vb`/`modDatabaseAbstraction.vb` — confirmed inconsistent/inert; treat as noise, not a foundation.)

---

## Phase 0: Architecture Decisions — do this before more Phase 2/3 work

### 0.1 Store/location data model

*(unchanged from v2 — see `ROADMAP-ARCHIVE-2026-08.md` if you need the full option comparison)*. **Recommendation stands: Option A**, separate Postgres per store + a small central reporting API later (Phase 4). Schema stays single-tenant per database.

### 0.2 Historical data from Precise PCs

**Resolved.** Real historical data was migrated for both POS and Jobs databases this session via the `LegacyDataImport` tool (14,899 stock items, 11,129 customers, 39,884 invoices, 26,383 jobs, 30,888 parts, 46,571 tasks, 1,323 return authorisations, and more). This is no longer an open question.

### 0.3 Feature calls needed before building

- **Recurring "Subscriptions" billing** — still open. Does any current IBG store actually need recurring billing? If not, drop it.
- **Exchange/calendar sync** (`clsExchange20.vb`) — **now recommend dropping outright**, stronger than v2's hedge. Confirmed: it was optional even in the legacy app (silently skipped if unconfigured), fails gracefully, uses a dying API (EWS retirement), and provides no value the new stack couldn't get more cheaply from a modern calendar integration later if ever actually requested. The SMS-based staff reminder that shares the same "on-site" flag is unrelated and worth keeping — don't drop that too.

### 0.4 New Job Tracking architecture decisions (found during the Phase 3 audit)

These weren't visible until the legacy code was actually read. Answer before starting Phase 3 build work.

- **Cross-database access pattern.** The new Job Tracking app needs to reach POS data (customer lookup, stock/parts lookup and re-pricing, staff lookup, labour-rate info) that lives in a separate Postgres database (`jobmatix_pos`). Two real options: (a) a direct second connection string to `jobmatix_pos` from the Job Tracking app (simplest, matches "one Postgres instance per store" — both DBs are on the same server), or (b) call into JMxPOS8's existing service layer via some in-process/API boundary. Given Phase 0.1 already settled on same-instance-per-store, **(a) is the pragmatic default** unless you want the two apps more decoupled for other reasons — flag if you want to discuss further, otherwise this roadmap assumes (a).
- **`GoodsInCare` schema.** Keep the legacy flat-text encoding (fast to ship, matches 26k historical rows as-is, needs a decode step to display), or redesign as a proper `job_goods_items(job_id, goods_type, brand, model, serial_no)` child table (matches what the data actually represents, requires a one-time backfill parse of history using the same decode logic already identified in the legacy code). **Recommend the child-table redesign** — it's a one-time migration cost against 26k rows, done once, versus re-implementing string encode/decode indefinitely. Defer to a fast-follow after a v1 slice ships with the flat field if you want to see the core workflow running sooner.
- **`service_model_checklists` schema fix** — needs its columns corrected to match what the legacy code actually reads/writes (`rm_stock_id`, `task_description`, drop the invented `model_name`/`item_order`) before the checklist-template feature can be built. Small, mechanical, do it whenever that feature is scheduled (not core-path, see Phase 3 below).
- **`returnauthorizations` missing columns** — add `ra_symptoms`, `ra_date_goods_received_back`, `ra_return_result_comment` before building RA (see Phase 3 below for RA's own scope, now independent of the rest of this phase).
- **Attachment/blob storage pattern.** Both RA attachments and job documents need a file-storage answer at some point (photos of damaged goods, supplier paperwork, job photos). Real usage is low-volume (8 RA rows, 142 job rows historically) — **recommend a simple `BYTEA` column in Postgres**, matching the legacy approach directly, rather than standing up S3/object storage for this alone. Decide once, reuse for both RA and Job documents rather than solving it twice.
- **Old attachment content recovery** — separately, check whether the actual file bytes for the 150 existing attachment rows (currently metadata-only, `NULL` content) are recoverable from the old SQL Server backup, if "view historical attachments" matters to you. Not a blocker for new attachments going forward either way.

---

## Phase 1: Infrastructure & Database — done

**Done:**
- ✅ Docker Postgres 15 + pgAdmin running (5433/5050)
- ✅ Schemas for `jobmatix_pos` and `jobmatix_jobs` deployed, real historical data migrated for both
- ✅ Foreign keys added to the Jobs database (was flagged as a gap in v1/v2 — confirmed already fixed via `create-jobs-schema-extensions.sql`, verified live: 10 FK constraints across the tables that need them)
- ✅ Missing SERIAL sequences fixed across all known instances — 17 tables in `jobmatix_pos` (Phase 2 work) + 4 tables in `jobmatix_jobs` (`quote_job_parts`, `model_checklist`, `ra_attachments`, `job_service_checklists`, fixed this session)
- ✅ `stock.requiresserial` schema drift reconciled

**Still open:**
- 🔧 **Hash staff passwords** (`jobmatix_pos.staff.password`, plaintext, internal login) — real security bug, fix before any deployment.
- 🔧 **Encrypt or redesign `jobs.username`/`jobs.userpassword`** (customers' own PC login credentials, plaintext, actively used by ~7,500 real jobs) — this needs *reversible* protection (a tech has to retrieve and use the value to log into the customer's machine), not a one-way hash. Consider whether this needs to persist at all versus being handled transiently.
- 🔧 **`service_model_checklists` and `returnauthorizations` schema fixes** — see Phase 0.4.
- 🔧 **Define or drop `jobmatix_main` and `jobmatix_backup`** — still empty shells, still an open call.
- 🔧 Fix `Jobs.DatePromised` hardcoded default (`2020-12-25` literal) — should be null or computed.
- 🗑️ **Drop `JMxRetailHost620.Net`** and **`JMxKeyGen420_OS`** — unchanged from v2, still the right call.

---

## Phase 2: POS Application (JMxPOS8) — feature-complete for what's unblocked

**Done (this session, all verified against live migrated data):**
- ✅ Core services, full MVVM, multi-document Sale tabs, staff admin (manager-override pattern), complete sale workflow, stock/customer CRUD, reporting.
- ✅ Cash-up/EOD reconciliation.
- ✅ Stocktake (physical inventory count/reconciliation).
- ✅ Goods Received (supplier stock receiving workflow).
- ✅ Customer Statement report.
- ✅ Transaction lookup/void.

**Everything else in Phase 2 is blocked on a decision, not on effort:**
- **Receipt printing / cash drawer kick** — needs the CUPS/ESC-POS hardware architecture decided and test hardware in hand.
- **Subscriptions/recurring billing** — needs Phase 0.3's business confirmation (does any store actually need it).
- **Email integration** (send invoices/statements) — needs an SMTP decision.
- **Serial number tracking UI polish** — lower priority, not blocking a store from running.

**New, small, low-risk additions surfaced by the Phase 3 audit — can be picked up any time, don't need to wait for the rest of Phase 3:**
- **Customer "Jobs" sub-tab** — extend the existing Customer screen's Invoices/Item Sales/Payments/Quotes sub-tabs with a Jobs history tab (see "What Changed" #6). Requires the cross-database read decided in Phase 0.4.
- **Return Authorisations** — can be built as a POS-adjacent feature now that it's confirmed not to depend on the rest of Job Tracking (see "What Changed" #5 and Phase 3 below for its own scope). Needs the `returnauthorizations` schema fix from Phase 0.4 first, and a decision on attachment storage if RA attachments matter to you.

**Hardware** — unchanged from v2, still start procurement now in parallel with software.

---

## Phase 3: JobMatix Main Application (job/repair tracking) — 0% built, now properly scoped

The legacy app (`JMxJT620.NET`) is ~94,000 lines across 79 files. A full read-through this session (not just file listing) found it's more tractable than the LOC count suggests — most of the bulk is Designer-generated UI boilerplate and repetitive per-document print-layout code, not business logic.

### Must ship together (size: L, this is the real core)

Job intake, job status/maintenance workflow, and parts lookup/allocation are **not independently buildable** — they share the same optimistic-locking model and status transitions are gated by parts/tasks-complete checks. You can't demo "create a job and move it through statuses" without at least a stubbed stock/parts lookup.

- **Job intake** — customer (barcode lookup + snapshot denormalized onto the job, matching legacy behavior), goods-in-care items (see Phase 0.4), problem description + checkbox symptoms list, priority, nominated tech, backup/recovery flags, warranty flag, optional photo. Three intake modes (Booking/Check-In/On-Site) share one save path. Job number is a plain identity column — no separate docket-numbering scheme to port.
- **Job status workflow** — 11 real statuses, not a simple linear flow:

  | Code | Meaning |
  |---|---|
  | `05-WaitListed` | Booked, not yet checked in |
  | `10-Created` | Intake complete, awaiting work |
  | `20-Suspended` | Paused (parts/customer wait) |
  | `23-InProcessSusp` | Suspended job, **locked** for edit |
  | `30-Started` | Actively being worked |
  | `33-InProcess` | Started job, **locked** for edit |
  | `40-QA` | Quality-assurance review |
  | `43-InProcessQA` | QA job, **locked** for edit |
  | `50-Completed` | Servicing finished |
  | `70-Delivered` | Handed back to customer |
  | `97-Cancelled` | Cancelled |

  The `2x/3x/4x` "InProcess" variants are a real optimistic-locking mechanism (opening a job for edit flips its status to the locked variant so other users see it's in use, releasing on close) — **this concurrency guard needs to be preserved**, it's load-bearing multi-user behavior, not cosmetic. Completing a job is gated by a checklist-complete check with a confirmation warning if no labour time/tasks were recorded; delivery is a separate explicit action from completion, not automatic.
- **Parts lookup/allocation** — pulls live from POS stock (search/browse, serial-number validation against POS's serialised-stock tracking), plus a re-pricing feature that flags when a part's price has drifted since it was added to the job. Unavoidably coupled to the Phase 0.4 cross-database decision.

### Can ship separately / deferred without blocking the core

- **Job docket/quote printing** — confirmed to use plain `PrintDocument`/GDI+ drawing, no Crystal Reports/RDLC dependency. Cleanly deferrable: doesn't gate any status transition, doesn't reach back into the database mid-render. Six distinct document types, mechanically similar. (One caveat: the quote-form file also contains real cancel-on-requote workflow logic bundled with its print code — extract that logic before deferring the print half.)
- **Brand/model reference data** — `Brands`/`GoodsTypes`/`Symptoms` are trivial flat lookup tables editable through one generic reusable list-editor pattern (the legacy app already does this with one parameterized form for all of them). Can be stubbed with seed data initially.
- **Service-checklist templates** (`service_model_checklists`) — a real but secondary feature layered on top of core job tracking; needs the Phase 0.4 schema fix first regardless, so naturally deferred.
- **Goods-in-care itemization** — v1 can ship with the flat legacy-compatible field; the child-table redesign (Phase 0.4) is a fast-follow, not a blocker.

### Important, independently schedulable (not part of the "must ship together" core)

- **Return Authorisations** — moved out of "tightly coupled to Job Tracking" (see "What Changed" #5). Scope: 7-state lifecycle (Created → RMA-Requested → RMA-Granted → GoodsSentToSupplier → GoodsCompleted/Refused/Cancelled), origin can be Job/Counter/Stock, integrates with POS via a stock/serial-audit transaction that already has a matching schema. Printing is 3 internal document types (record slip, courier label, packing slip) — no supplier-facing email/electronic submission exists in the legacy app, it's all printed paperwork. Estimated 2-3 weeks for one developer given Phase 2's stock/supplier/serial-audit plumbing already exists to build on.
- **SMS notifications** — confirmed low-risk. 4 plain HTTP(S) gateways (SMS Boss, SMS Broadcast, SMSGlobal, DirectSMS), each a small adapter (form-POST vs. query-string request format and XML vs. substring-match response parsing differ per gateway, so budget 4 small adapters, not 1 generic client). Config lives in a generic key/value settings table already ported. Low coupling — 2 call sites in the whole legacy app, user-initiated with confirmation, not automatically triggered by status changes.
- **Email notifications (SMTP)** — a natural sibling to SMS, same settings store and trigger pattern, independent of the Exchange/EWS calendar integration (which is being dropped, see Phase 0.3) — don't conflate the two, they're unrelated despite both being "email."
- **Job reporting** — 4 report types (Jobs/Parts/Staff/Timesheet). Reuse Phase 2's already-built `ReportsViewModel` grid/summary pattern rather than porting the legacy GDI+ print-report renderer. Real technical risk here (not cosmetic): the legacy Jobs report uses SQL Server's proprietary `SHAPE` syntax for a parent/child recordset and a dynamically-created T-SQL scalar function for parsing chargeable hours from a session-log string — neither has a Postgres equivalent, needs an actual rewrite (two queries + in-app grouping; move the parsing into C#).
- **On-site job list + staff SMS reminder** — just a filtered query plus a background poller reusing the SMS client above. Not a scheduling engine (see "What Changed" #7).

### Explicitly dropped

- MYOB Retail Manager quote import (discontinued product, no modern equivalent needed — JMxPOS8 is the quote/order source going forward).
- **Exchange/EWS calendar sync** — now a firm recommendation to drop (see Phase 0.3), not just a hedge.

### Needs a small replacement, not a rewrite (size: S)

- `JobMatix62.Net` launcher — confirmed purely mechanical (picks POS vs. Job Tracking, remembers last-used choice, no licensing/update-check logic). The SQL-Server-instance-discovery complexity in the legacy launcher is moot (Phase 1 already fixes the Postgres connection). New equivalent: a simple "remember last-used module" app setting plus a POS/Job-Tracking picker inside the one Avalonia app — no separate launcher process needed, since both halves are being built into the same new-stack application rather than as separate historical .exes.

---

## Phase 4: Cross-Store Reporting

*(unchanged from v2)* — genuinely new work, not a port of anything. Build the small central reporting API (Phase 0.1 decision) after Phase 2/3 are solid at a single store; each store's install must keep working standalone without it.

**Backups**: unchanged — rsnapshot + DO Spaces + `pg_dump` cron, not `backup-agent`.

---

## Phase 5: Testing, Pilot, Rollout

*(unchanged from v2)* — pilot at one store first, no live-cutover risk (greenfield), hardware should already be procured by this point.

---

## Risk Register (updated)

| Risk | Notes |
|---|---|
| Multi-store model decision (Phase 0.1) | Blocks Phase 4 design, does **not** block Phase 2/3. |
| Solo maintainer bus factor | One person + AI assistance. Keep things simple and documented. |
| Hardware procurement lead time | Order test units now, in parallel with software work. |
| Cross-database access pattern (Phase 0.4) | New risk this version — Job Tracking has no working prior-art for this (the legacy app never needed it; a partial VB.NET retrofit exists but is inconsistent/inert). Decide before starting the core Job workflow build, since parts lookup depends on it directly. |
| `GoodsInCare` schema decision (Phase 0.4) | Affects intake UI design directly — decide before building that screen, not after. |
| Legacy report SQL (`SHAPE`/T-SQL scalar function) | Real rewrite required, no direct Postgres equivalent — budget accordingly, don't treat job reporting as a mechanical port. |
| Two newly-found plaintext-credential gaps | `staff.password` (known) and `jobs.username`/`userpassword` (newly found, arguably worse — customer PC credentials). Fix both before real deployment. |
| RA attachment content not migrated | Metadata-only for all 150 historical attachment rows; check SQL Server backup recoverability before promising "view old attachments." |
| EWS/calendar retirement | Now moot — recommend dropping the integration outright rather than rebuilding against Graph. |
| ~~Live cutover / parallel-run~~ | Not applicable — greenfield. |
| ~~Printer/EFTPOS integration complexity~~ | Unchanged from v2 — lower risk than originally feared, well-understood CUPS/ESC-POS work. |
| ~~Zero FKs in Jobs database~~ | Resolved — was already fixed before this session, confirmed live. |
| ~~RA tightly coupled to Job Tracking~~ | Resolved by audit — RA is mostly independent (90.5% of real RAs have no job link), can ship on its own schedule. |

---

## Documentation Index

- `ROADMAP.md` — this file, the master plan
- `ROADMAP-ARCHIVE-2026-08.md` — superseded v2, kept for history
- `ROADMAP-ARCHIVE-2026-01.md` — superseded v1, kept for history
- `MIGRATION-STATUS.md`, `CURRENT-STATUS.txt` — **stale**, written against v1's assumptions, don't use for planning
- `JMxPOS8/CONVERSION_STATUS.md` — POS-specific progress detail
- `POSTGRESQL_MIGRATION_GUIDE.md` — still useful as SQL conversion reference
