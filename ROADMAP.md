# JobMatix Revival Roadmap v3

**Last Updated**: September 1, 2026
**Supersedes**: `ROADMAP-ARCHIVE-2026-08.md` (v2 — Phase 2 was accurate and is now complete; Phase 3 was a thin, partly-wrong placeholder, corrected below after a real code audit)
**Project**: Martin Fenwick, solo maintainer, AI-assisted (Claude Code)
**Status of the software**: JobMatix/JMxPOS was last live in production ~2020–2021 at Precise PCs. It is **not currently running anywhere**. This is a revival for a fresh multi-store IBG rollout, not a live cutover.

**Pick up here next session** (2026-09-01): Brand/model reference data screens, Phase 6.1 (per-unit serial costing/COGS), job reporting (Jobs/Parts/Staff/Timesheet), and the new-job intake docket PDF are all done. Phase 6.2 (central multi-store stock/pricing control via `franky-forge`) is scoped with a concrete plan and a decision made (same product records as the online catalog); the `franky-forge`-side read endpoint is built and tested but deliberately held back (not pushed to origin, no partner key issued) until ready to test end-to-end - do not push or otherwise touch `franky-forge` without an explicit go-ahead. Job docket/quote printing (see Phase 3 below) has its PDF pipeline proven on one of six document types - the other five are mechanical extensions of the same pattern whenever picked up, physical printer/cash-drawer hardware remains explicitly deferred pending its own evaluation. **Also done 2026-09-01, direct UI feedback**: navigation cleanup (tab strip reduced to Sale/Stock/Customers/Tickets, everything else moved to the menu bar, Jobs renamed to Tickets throughout) plus real feature gaps found while reviewing the live app - ticket search, status-grouped ticket list with counts, a notes section on the ticket detail panel, and click-through from a customer's ticket history to the ticket itself. See "UI/UX notes" near the end of Phase 3 for detail. Other candidates: the two still-open plaintext-credential fixes from Phase 1 (`staff.password` hashing, `jobs.username`/`userpassword` encryption) - worth doing before any real deployment, not urgent for continued dev. See "Important, independently schedulable" and "Can ship separately" under Phase 3 below for the full remaining list.

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
12. **The Job Tracking app never had its own database connection historically — confirmed architecturally significant.** The whole legacy suite ran one process against one shared SQL Server database via one shared connection; `CustomerBarcode` on a job was always just a lookup key into the same database, never a real cross-database reference. (A partial, never-finished dual-connection retrofit exists in the legacy VB.NET source from an earlier session — `DatabaseConfig.vb`/`modDatabaseAbstraction.vb` — confirmed inconsistent/inert; treat as noise, not a foundation.)
13. **Resolved, decisively: JobMatix is going back to being one single app, matching its actual history.** JobMatix was originally built as Job Tracking software; RA was added next; POS was added last (replacing MYOB Retail Manager) — it was never three separate products, that split only happened when this port work began. Per direction from the project owner, this port undoes that split: POS, Job Tracking, and RA become one Avalonia application, and — since a single app talking to two databases just recreates the cross-database problem in a new form — **the databases were merged too**. `jobmatix_jobs`'s 17 tables (26,383 jobs, 30,888 parts, 46,571 tasks, 1,323 RAs, and the rest) were merged into `jobmatix_pos` this session (full `pg_dump` backups of both databases taken first, kept in `db-backups/`; the standalone `jobmatix_jobs` database was left in place, untouched, as an extra safety net rather than dropped). This finally allows the real foreign keys that were never possible across two databases: `jobs.rmcustomer_id → customer`, `jobs.{rcvd,tech,delivered}rmstaff_id → staff`, `parts.stock_id → stock`, `parts.serviced_by_staff_id → staff`, `tasks.performed_by_staff_id → staff`, `returnauthorizations.{rm_stock_id→stock, supplier_id→supplier, staff_id_created/updated→staff}` — 14 new FKs added, all nullable (`ON DELETE SET NULL`), after nulling out a small number of historical rows (≤5% of any given column) whose legacy numeric ID didn't resolve to a real row. `returnauthorizations.goods_id`/`order_id` were deliberately left without FKs — ~74% orphaned, because the POS goods-received integration was added late in the legacy product's life and most historical RAs predate it. **This closes out Phase 0.4's cross-database question entirely** — there is no cross-database problem to solve anymore, Job Tracking/RA screens will just query the same connection JMxPOS8 already has open. It also means the `JobMatix62.Net` launcher's whole reason to exist (pick between two separate apps/processes) goes away — Job Tracking and RA become new tabs in the same window, not a separately launched app.

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

These weren't visible until the legacy code was actually read. The cross-database access question this section originally posed is now moot — see "What Changed" #13: POS, Job Tracking, and RA are becoming one single app over one single database (`jobmatix_pos`), matching JobMatix's original history, so there's no second connection or API boundary to design. What's left to decide:

- **`GoodsInCare` schema.** Keep the legacy flat-text encoding (fast to ship, matches 26k historical rows as-is, needs a decode step to display), or redesign as a proper `job_goods_items(job_id, goods_type, brand, model, serial_no)` child table (matches what the data actually represents, requires a one-time backfill parse of history using the same decode logic already identified in the legacy code). **Recommend the child-table redesign** — it's a one-time migration cost against 26k rows, done once, versus re-implementing string encode/decode indefinitely. Defer to a fast-follow after a v1 slice ships with the flat field if you want to see the core workflow running sooner.
- **`service_model_checklists` schema fix** — needs its columns corrected to match what the legacy code actually reads/writes (`rm_stock_id`, `task_description`, drop the invented `model_name`/`item_order`) before the checklist-template feature can be built. Small, mechanical, do it whenever that feature is scheduled (not core-path, see Phase 3 below).
- **`returnauthorizations` missing columns** — add `ra_symptoms`, `ra_date_goods_received_back`, `ra_return_result_comment` before building RA (see Phase 3 below for RA's own scope, now independent of the rest of this phase).
- **Attachment/blob storage pattern.** Both RA attachments and job documents need a file-storage answer at some point (photos of damaged goods, supplier paperwork, job photos). Real usage is low-volume (8 RA rows, 142 job rows historically) — **recommend a simple `BYTEA` column in Postgres**, matching the legacy approach directly, rather than standing up S3/object storage for this alone. Decide once, reuse for both RA and Job documents rather than solving it twice.
- **Old attachment content recovery** — separately, check whether the actual file bytes for the 150 existing attachment rows (currently metadata-only, `NULL` content) are recoverable from the old SQL Server backup, if "view historical attachments" matters to you. Not a blocker for new attachments going forward either way.

---

## Phase 1: Infrastructure & Database — done

**Done:**
- ✅ Docker Postgres 15 + pgAdmin running (5433/5050)
- ✅ **`jobmatix_jobs` merged into `jobmatix_pos`** — JobMatix is now one app over one database, see "What Changed" #13. `jobmatix_pos` is the single source of truth for POS, Job Tracking, and RA data going forward. Pre-merge backups of both databases kept in `db-backups/`.
- ✅ Foreign keys added to the Jobs tables (was flagged as a gap in v1/v2 — confirmed already fixed via `create-jobs-schema-extensions.sql` before this session; 14 more cross-domain FKs added this session as part of the merge, now that jobs/RA and customer/stock/staff/supplier live in the same database)
- ✅ Missing SERIAL sequences fixed across all known instances — 17 tables from the original `jobmatix_pos` (Phase 2 work) + 4 tables from the merged-in jobs tables (`quote_job_parts`, `model_checklist`, `ra_attachments`, `job_service_checklists`, fixed this session)
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
- **Customer "Jobs" sub-tab** — extend the existing Customer screen's Invoices/Item Sales/Payments/Quotes sub-tabs with a Jobs history tab (see "What Changed" #6). Now a plain same-database query (`jobs.rmcustomer_id` has a real FK to `customer` since the merge) — no cross-database plumbing needed.
- **Return Authorisations** — can be built as a POS-adjacent feature now that it's confirmed not to depend on the rest of Job Tracking (see "What Changed" #5 and Phase 3 below for its own scope). Needs the `returnauthorizations` schema fix from Phase 0.4 first, and a decision on attachment storage if RA attachments matter to you.

**Hardware** — unchanged from v2, still start procurement now in parallel with software.

---

## Phase 3: JobMatix Main Application (job/repair tracking) — core built this session

The legacy app (`JMxJT620.NET`) is ~94,000 lines across 79 files. A full read-through this session (not just file listing) found it's more tractable than the LOC count suggests — most of the bulk is Designer-generated UI boilerplate and repetitive per-document print-layout code, not business logic.

### Must ship together (size: L, this is the real core) — ✅ done

Job intake, job status/maintenance workflow, and parts lookup/allocation were **not independently buildable** — they share the same optimistic-locking model and status transitions are gated by parts/tasks-complete checks. Built and verified against real data this session as a new Jobs tab in JMxPOS8, alongside the Customer Jobs sub-tab and Return Authorisations that were also completed:

- Job intake: customer barcode lookup (snapshot denormalized onto the job, matching legacy behavior), goods-in-care/brand/model as flat fields for v1 (the itemization redesign from Phase 0.4 is still a deferred fast-follow, not blocking), problem description + symptoms, priority, backup/warranty flags, staff attribution. Booking/On-Site intake modes weren't built (only the equivalent of legacy's default Check-In path) - low-risk to add later since they share the same save path.
- The real 11-state status workflow, including the optimistic-locking "InProcess" mechanism (selecting a job to view/edit flips it to a locked variant, releasing on deselect, re-applying after any transition performed while still viewing it) - verified this actually round-trips correctly across selection changes, not just on paper.
- Parts lookup/allocation with live price-drift detection against current stock pricing (the legacy `gbShowAllParts` repricing feature).
- Not yet built from the original "must ship together" scope: the checklist-complete gate before completing a job (v1 allows completing without checking for recorded tasks/parts, just a soft warning), and photo attachments at intake.

**Original status-table reference** (unchanged from the audit, still the vocabulary the build follows):

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

  The `2x/3x/4x` "InProcess" variants are the optimistic-locking mechanism described above - preserved in the build, not simplified away.

### Can ship separately / deferred without blocking the core

- **Job docket/quote printing** — 🔧 in progress, PDF rendering started 2026-09-01. Confirmed to use plain `PrintDocument`/GDI+ drawing, no Crystal Reports/RDLC dependency. Six distinct document types (job labels, receipt, quote, new-job intake docket, service/delivery record, customer maintenance report — `clsPrintDocs3.vb`), mechanically similar.
  - **Business decision (2026-09-01)**: real physical printer/cash-drawer hardware output is explicitly **deferred, not stubbed** — real-world experience with services like Syncro's print integration (cash drawers routinely failing to open, prints failing outright) means that needs its own real evaluation before committing to an approach, not a rushed add-on bundled into this. In the meantime, every document renders as a real, complete PDF (opened with the OS's default viewer) so the actual content/layout can be reviewed now.
  - ✅ **New-job intake docket** ("Customer Service Agreement") done — `JobDocumentPdfService.cs`, using PDFsharp (chosen over QuestPDF specifically to avoid QuestPDF's revenue-based Community license cap, a real commercial-licensing risk not worth taking here). Verified by actually rendering PDFs against real jobs and opening them, not just checking file magic bytes — found and fixed a real bug this way: `goodsincare`'s legacy tab-delimited packed format (see Phase 0.4) rendered as run-together words ("BlackGENERIC") because PDFsharp doesn't render raw tabs as visible whitespace.
  - Needed a `PdfFontResolver` (PDFsharp 6.x has no font enumeration outside Windows GDI) — resolves to Liberation Sans/DejaVu on Linux, real Arial on Windows/macOS.
  - **Not yet built**: the other 5 document types are mechanical extensions of the same rendering pattern. Quote printing specifically also needs the underlying job-quote workflow built first (no "awaiting quote" job state exists yet in this port) — the roadmap's original caveat about extracting real cancel-on-requote logic bundled into the legacy quote-print code still applies once that workflow gets built.
  - The `JobRecord`/`JobService` fields the docket needed but the app didn't read yet (`CustomerCompany`, `Username`/`UserPassword`, `GoodsOther`) were added — `Username`/`UserPassword` print faithfully (matching legacy) but are only ever populated on historical migrated jobs; this port's own intake form doesn't collect them, so new jobs correctly omit that section entirely.
- **Brand/model reference data** — ✅ done. `GoodsTypes`/`Brands`/`Symptoms`/`TaskTypes` (all four, not just the three originally named here) are edited through one generic reusable `ReferenceDataViewModel`/`ReferenceListEditorView`, matching the legacy app's single parameterized `frmListEdit` form used for all of them - a new "Reference Data" tab with four sub-tabs, plus matching entries under the Jobs menu. Verified against live data (108/145/156/202 real rows) including the delete-blocked-by-FK path (tried deleting an in-use `tasktype_id`, confirmed it fails cleanly instead of crashing). Not wired into job intake yet - `JobViewModel`'s Brand/GoodsInCare/Symptoms fields are still free text, not dropdowns bound to these tables; that wiring is a separate small follow-up whenever it's wanted.
- **Service-checklist templates** (`service_model_checklists`) — a real but secondary feature layered on top of core job tracking; needs the Phase 0.4 schema fix first regardless, so naturally deferred.
- **Goods-in-care itemization** — v1 can ship with the flat legacy-compatible field; the child-table redesign (Phase 0.4) is a fast-follow, not a blocker.

### Important, independently schedulable (not part of the "must ship together" core)

- **Return Authorisations** — ✅ done. Built as a new Return Auths tab: the real 7-state lifecycle (Created → RMA-Requested → RMA-Granted → GoodsSentToSupplier → GoodsCompleted/Refused/Cancelled), origin Job/Counter/Stock, and the POS-side integration (permanently decrements stock and marks the serial audit row RETURNED when goods ship to the supplier, plus a `supplier_returns` header/line) - verified against real stock and a real serialized item. Printing (3 internal document types - record slip, courier label, packing slip) wasn't built; there's no supplier-facing email/electronic submission in the legacy app anyway, it's all printed paperwork, so this is a clean deferral.
- **Customer "Jobs" sub-tab** — ✅ done, built on the existing Phase 2 Customer screen rather than as a separate view (see "What Changed" #6).
- **SMS notifications** — ✅ done, DirectSMS only. All 4 legacy-supported gateways were built and verified first (via a fake HTTP handler, since no real credentials exist and a live send costs money/texts a real number), but checking the restored legacy SQL Server database (`JobTracking.SystemInfo` on the `jobmatix-mssql-restore` container) showed only DirectSMS was ever actually configured in production (`SmsGatewayHostName='directSMS'`, real Precise-PCs-era credentials on file) — the other 3 were evidently tried early on and abandoned. Simplified down to DirectSMS only rather than carrying 3 dead options forward. Settings live in the existing `systeminfo` key/value table (Staff tab, admin-gated); "Notify Customer" on the Jobs tab sends and logs onto `jobs.notifications`, matching the legacy append-to-job-record pattern. The real historical password was deliberately not copied anywhere (live credential for a paid third-party account under the old business name) - enter it directly in the Staff tab if you want to reuse that account, or set up a fresh one.
- **Email notifications (SMTP)** — ✅ done. Same pattern as SMS: real historical config confirmed in the restored legacy database (Mailgun relay, smtp.mailgun.org:587), settings panel next to SMS on the Staff tab, "Send Email" alongside "Send SMS" on the Jobs tab's Notify Customer panel (looks up the linked customer's on-file email via the real `rmcustomer_id` FK). Independent of the Exchange/EWS calendar integration (dropped, see Phase 0.3) — unrelated despite both being "email."
- **Job reporting** — ✅ done. 4 report types (Jobs/Parts/Staff/Timesheet), built into Phase 2's existing `ReportsViewModel` grid/summary pattern rather than porting the legacy GDI+ print-report renderer. The feared SQL Server `SHAPE`/scalar-function rewrite turned out to already be solved in the legacy VB app itself — its own `gCurComputeChargeableHours`/`gbQueryWorkSessions` parsed `jobs.sessiontimes` (a free-text session log) in application code; the dynamically-created T-SQL function just repeated that same parsing for use inside the SHAPE query. Ported that parsing to C# (`SessionTimesParser.cs`), verified against 2,000 real jobs at 99.95% agreement with each job's own recorded `totalservicetime`. The SHAPE parent/child structure became a plain query plus a JOIN (Parts report) or in-app grouping (Jobs/Staff reports) — no Postgres SHAPE equivalent needed at all. Labour hourly rates (per-priority) recovered from the restored legacy SQL Server database and seeded into `systeminfo` (real values: 110/135/185, not invented). All four verified against the full real dataset (25,532 jobs, 30,017 part lines, 87 staff, 4,711 timesheet sessions). Found a real legacy data anomaly while spot-checking (one part named "Not in System Yet" priced at $100,000,000) — confirmed as pre-existing dirty data, not a bug, and left as-is rather than silently filtered.
- **On-site job list + staff SMS reminder** — just a filtered query plus a background poller reusing the SMS client above. Not a scheduling engine (see "What Changed" #7).

### Explicitly dropped

- MYOB Retail Manager quote import (discontinued product, no modern equivalent needed — JMxPOS8 is the quote/order source going forward).
- **Exchange/EWS calendar sync** — now a firm recommendation to drop (see Phase 0.3), not just a hedge.

### No longer needed at all

- `JobMatix62.Net` launcher — under v2 this was scoped as "needs a small replacement" (a POS/Job-Tracking picker). That's now moot: per "What Changed" #13, JobMatix is going back to being one single app, not two apps a launcher picks between. Job Tracking and RA are new tabs in the same JMxPOS8 window (matching the existing Sale/Stock/Customers/Staff/Reports/Transactions tabs), not a separately launched module. Nothing to build here.

### UI/UX notes (2026-09-01, direct feedback from running the live app) — ✅ done

The business owner ran the app and flagged the top tab strip as cluttered and poorly labelled (11 tabs, one row wrapping), plus real gaps found while looking at the Tickets (formerly "Jobs") tab specifically:

- **Navigation**: tab strip reduced to Sale/Stock/Customers/Tickets only. Staff, Reports, Transaction Lookup, Stocktake, Intake (renamed from Goods Received), Returns, and Reference Data are unchanged as actual tabs (`SelectedTabIndex` navigation, all existing menu commands) but no longer shown as strip headers — a new `TabItem.navhidden` style (`IsVisible=False`) hides the header button while leaving `SelectedIndex`-based selection and content working normally. Reached via the menu bar instead (added the one missing menu item, Transaction Lookup, under Sales).
- **Renamed "Jobs" to "Tickets"** throughout the UI (tab, menu, "Create Job" → "Create Ticket", the Customer screen's Jobs sub-tab) — UI copy only, the underlying `JobViewModel`/`JobRecord`/`JobService` names are deliberately unchanged.
- **Restored the legacy tree-style ticket list** (status groups — Waitlisted/New/Suspended/In Progress/QA/Completed — each with a count, expand/collapse) that this port had flattened into one flat list. Rebuilt live from `OpenJobs` whenever it changes.
- **Ticket search** (`JobService.SearchJobsAsync`) — by exact ticket number or partial customer/problem match, across every status including delivered/cancelled. There was previously no way to find a ticket that wasn't already in the open list at all.
- **Notes section** added to the ticket detail panel (Problem Details/Diagnosis/Service Notes) — these were being captured (at intake and at completion) but never shown again once you navigated away from the ticket.
- **Click-through from Customer → Tickets sub-tab**: the ticket number is now a clickable link that switches to the Tickets tab and opens that exact ticket (`JobViewModel.OpenJobByIdAsync`, new — works for any status, not just open ones, since a customer's ticket history is mostly closed/delivered/cancelled tickets).

Verified by actually running the app and screenshotting each piece against real data, including opening a delivered ticket through the new cross-navigation path to confirm the existing optimistic-lock logic (`HandleSelectionChangeAsync`) degrades safely outside the open-jobs list (it already did — the lock transition only applies to Suspended/Started/QA statuses).

---

## Phase 4: Cross-Store Reporting

*(unchanged from v2)* — genuinely new work, not a port of anything. Build the small central reporting API (Phase 0.1 decision) after Phase 2/3 are solid at a single store; each store's install must keep working standalone without it.

**Backups**: unchanged — rsnapshot + DO Spaces + `pg_dump` cron, not `backup-agent`.

---

## Phase 5: Testing, Pilot, Rollout

*(unchanged from v2)* — pilot at one store first, no live-cutover risk (greenfield), hardware should already be procured by this point.

---

## Phase 6: Serial-Level Costing (COGS/FIFO) & Central Multi-Store Catalog

Raised by the business owner 2026-09-01. Two related asks: real cost-of-goods-sold reporting (per-unit, not per-SKU), and a central place to control stock/pricing across stores, similar to how Fieldpine/Neto work today.

### 6.1 Per-unit cost tracking (the "COGS/FIFO" ask) — ✅ done

**The actual problem, confirmed in code**: `stock.costprice` is one field per SKU, overwritten on every goods-received transaction (`GoodsReceivedService.cs` — "latest cost wins"). So a product that came in at two different prices over time can only ever report the most recent one; there's no way to know what a specific unit actually cost when it sold. The real per-receipt cost already exists (`goods_received_line.cost_ex`, tied to a specific `goods_received` batch and `stock_id`), it's just never linked to an individual unit.

**The business's proposed fix** — print a special barcode per incoming unit encoding order number + SKU + serial, for 3 new printers being installed for stock processing — was evaluated and **not recommended**. It requires whoever applies the label to correctly match the right barcode to the right physical unit, which is exactly the failure mode the business itself flagged as likely. It also duplicates data that's already derivable from the database.

**Built instead, at the database layer rather than the label layer**:
- `serial_audit` gained `unit_cost` and `received_line_id` (FK to `goods_received_line`) — see `sql-scripts/create-serial-costing-extensions.sql`. `invoice_lines.serial_audit_id` (already present from the legacy port, 15,619 historical rows populated but never constrained) now has a real FK too, after nulling 1,018 orphaned values (6.5%, consistent with the ~5% orphan rate from the 2026-08-31 cross-database merge).
- A real gap was found and fixed while building this: **no code anywhere in the app created a `serial_audit` row** — receiving a serialized item was never wired up in this port at all. Goods Received now captures serial numbers per line (for `requiresserial` items) and stamps each new `serial_audit` row with that receipt's actual cost, with non-fatal warnings for a serial-count mismatch or a serial already recorded as in stock.
- At sale, the specific serial picked now supplies its own cost for `invoice_lines.cost_ex/cost_inc/sell_ex/sell_inc/gross_profit` — `stock.costprice` is no longer the source of truth for COGS/margin, only for defaulting the sell price on new sales. `serial_audit.is_in_stock`/`status` is now kept in sync on Sale (false/SOLD) and Refund (true/IN_STOCK) — previously never written at all, which meant the Sale tab's "available serials" picker (`SerialService.GetAvailableSerialsAsync`) was already silently relying on stale legacy data.
- Also fixed while in this area: `SerialService`'s "already sold" check queried `invoice_lines.serialnumber`, which is empty for all 14,578 historically-sold serials (they use the legacy `serial_number` column instead) — it now checks both, so a serial sold before this port existed is correctly detected as unavailable.
- New **Cost / Margin** report (`ReportsViewModel.RunCostMarginReport`) using the real per-unit figures, next to the existing reports.
- Ordinary product barcodes on the shelf didn't change — still scan-to-sell by SKU exactly as before. The only new physical step is printing a serial label at receiving time if the manufacturer doesn't already supply a scannable serial.

Verified end-to-end against live data: received a test serial at a distinct cost, sold it, refunded it, and hit the duplicate-serial warning path - confirmed correct cost/margin values at each step and that cleanup left no trace.

### 6.2 Central multi-store stock/pricing control (Fieldpine/Neto-style) — investigated 2026-09-01, plan below, not built

This matches the architecture already recommended back in Phase 0.1/Phase 4 — separate Postgres per store, plus a central reporting/control API layered on top later — so it doesn't need new architectural work to accommodate, just needs Phase 4 built out further than "reporting" into "control" (push pricing/stock changes out to stores, not just read them in).

**`franky-forge` (sibling project in IBG_HUB) is a much better fit than expected — verified by reading its actual code, not just its README**: it already has a multi-tenant product model (`tenants`, `products.tenant_id`), per-tenant *and* per-channel pricing markup rules, and a proven generalized channel-adapter pattern (`BaseChannel.syncProduct(product, existingExternalId, meta)`) with three shipped adapters (Shopify, BigCommerce, WooCommerce) plus idempotent per-product sync-status tracking (`product_channel_status`) and per-tenant encrypted credentials (`channel_configs`). On 2026-08-10 the project's own direction pivoted to exactly this shape: "tenant" was redefined from an internal segment to **"each buying-group member shop is a tenant"**, and there's live work (`franky-forge/CW_MASTER_DATA_ROADMAP.md`) solving the same shared-catalog-with-local-pricing-override problem for a different multi-franchisee buying group, using a `products.shared_group` tag mechanism on top of the tenant model.

**Instructive prior art**: a Fieldpine connector (referencing IBG_HUB's `everydaypos/` FD1 client) was scoped in franky-forge but **explicitly downgraded to speculative and deprioritized on 2026-08-12** — not because the idea was wrong, but because writing into Fieldpine's proprietary third-party API was an unverified, high-risk unknown with no proven write path. That risk doesn't apply here: IBG owns the JobMatix schema outright, so a JobMatix integration is actually the *lowest-risk* channel target attempted yet.

**Decision made (2026-09-01, by the business owner): a JobMatix store's stock items should be the *same* product records as the Neto/Shopify-facing catalog** — one shared product/pricing source of truth, not a separate catalog. Concrete plan, following that decision:

1. **Tenant mapping**: each IBG computer-repair store becomes one `franky-forge` tenant — reuses the existing tenant model as-is, no schema change needed there.
2. **Identity bridge**: add a nullable `stock.franky_product_id INTEGER` column in JobMatix, linking a locally-stocked item to its shared `franky-forge` `products.product_id` for centrally-managed SKUs. Nullable so repair-only parts (the long tail that will never appear in an online storefront) stay purely local and untouched.
3. **Auth**: reuse `franky-forge`'s existing `partner_api_keys` mechanism (already does per-tenant issuance/hashing) — issue one partner API key per JobMatix store.
4. **`franky-forge`'s read gap — ✅ built 2026-09-01, not yet pushed to origin**: `GET /partner/v1/products` added to `franky-forge/src/routes/partnerProducts.js` (that file was inbound-only before - a partner pushing their catalog *into* Franky for enrichment - and `products.js`'s GET routes are portal-staff-JWT-scoped, not partner-key-scoped, so this was real new work, not a config change). Supports `?updated_since=` for incremental sync and `?sku=a,b,c` for an exact batch lookup, applies any `product_overrides` price correction before markup, and computes price via the existing per-tenant/per-channel markup mechanism with a new `channel_type='jobmatix'` value (no schema change - reuses `channel_markup_rules` exactly like Shopify/BigCommerce/WooCommerce do). Deliberately ignores the `publish_enabled`/`channelPublishGate` check - that's about storefront readiness (image quality, confidence), irrelevant to whether a physical unit can be sold in-store. Tests added (`franky-forge/tests/partnerProducts.test.js`, router.stack-introspection pattern matching that repo's own convention); full suite (670 tests) still passes. **Committed locally in `franky-forge` but not pushed to origin/main** - that's a separate live production repo (shares a DB cluster with an unrelated live platform, and push may auto-deploy via DigitalOcean App Platform) - needs an explicit go-ahead before pushing, not routine JobMatix dev work.
   - Still needed on the `franky-forge` side before a JobMatix store can actually use this: issue a real `partner_api_keys` row for a store/tenant (existing admin UI, no code needed) and decide the `channel_markup_rules` value for `channel_type='jobmatix'` per tenant if a store's in-person price should differ from its Shopify one.
   - JobMatix side still needs: a small sync job (periodic or a manual "Sync Catalog" action) that calls this endpoint and updates `stock.sellprice`/`description` for linked items, plus the `stock.franky_product_id` link column from item 2 above. Not started.
5. **Pull, not push, for v1** — matches the hybrid local/cloud model the business also floated (local operation continues offline, catches up on reconnect). JobMatix pulling from Franky's API is inherently robust to a store being briefly offline; a live push *into* each store's database from outside is exactly the cross-network-write risk that got Fieldpine's connector deprioritized. `partner_api_keys.webhook_url`/`webhook_secret` (already built, currently unused for this) is a good fast-follow for near-real-time price pushes once the pull path is proven — not needed for v1.
6. **Sales/stock-movement reporting back centrally** (JobMatix → Franky) is a separate, later direction with no existing mechanism yet — deliberately out of scope for this phase.

**Status**: the `franky-forge`-side read endpoint is built and tested (pending push). Nothing on the JobMatix side is built yet - next concrete step when picked up: push the `franky-forge` commit (with go-ahead), issue a test partner API key, then build the `stock.franky_product_id` column + sync job here.

**Status**: not scoped into a session yet. Next step when picked up: (1) decide the `serial_audit` cost-tracking schema addition (own column vs. FK to `goods_received_line`), (2) build the Goods Received serial-capture step, (3) revisit Phase 4's central API scope once 6.1 is done and there's real per-store data worth centralizing.

---

## Risk Register (updated)

| Risk | Notes |
|---|---|
| Multi-store model decision (Phase 0.1) | Blocks Phase 4 design, does **not** block Phase 2/3. |
| Solo maintainer bus factor | One person + AI assistance. Keep things simple and documented. |
| Hardware procurement lead time | Order test units now, in parallel with software work. |
| ~~Cross-database access pattern~~ | Resolved by merging `jobmatix_jobs` into `jobmatix_pos` this session — JobMatix is one app over one database now, matching its original history. No cross-database design work left. |
| `GoodsInCare` schema decision (Phase 0.4) | Affects intake UI design directly — decide before building that screen, not after. |
| ~~Legacy report SQL (`SHAPE`/T-SQL scalar function)~~ | Resolved — the real parsing already lived in the legacy VB app, not SQL Server; ported to C# (`SessionTimesParser.cs`), verified at 99.95% against real data. |
| Two newly-found plaintext-credential gaps | `staff.password` (known) and `jobs.username`/`userpassword` (newly found, arguably worse — customer PC credentials). Fix both before real deployment. |
| RA attachment content not migrated | Metadata-only for all 150 historical attachment rows; check SQL Server backup recoverability before promising "view old attachments." |
| EWS/calendar retirement | Now moot — recommend dropping the integration outright rather than rebuilding against Graph. |
| ~~Live cutover / parallel-run~~ | Not applicable — greenfield. |
| ~~Printer/EFTPOS integration complexity~~ | Unchanged from v2 — lower risk than originally feared, well-understood CUPS/ESC-POS work. |
| ~~Zero FKs in Jobs database~~ | Resolved — was already fixed before this session, confirmed live. |
| ~~RA tightly coupled to Job Tracking~~ | Resolved by audit — RA is mostly independent (90.5% of real RAs have no job link), can ship on its own schedule. |
| ~~Serial-receiving pipeline doesn't exist yet (Phase 6.1)~~ | Resolved — Goods Received now creates `serial_audit` rows with real per-unit cost; Sale/Refund keep `is_in_stock`/`status` in sync. |
| Central catalog: reuse `franky-forge` (Phase 6.2) | Decided 2026-09-01 — reuse it, same product records as the Neto/Shopify catalog. The `franky-forge`-side read endpoint is built and tested but not pushed to origin yet (separate production repo, needs explicit go-ahead) - nothing on the JobMatix side is built. |

---

## Documentation Index

- `ROADMAP.md` — this file, the master plan
- `ROADMAP-ARCHIVE-2026-08.md` — superseded v2, kept for history
- `ROADMAP-ARCHIVE-2026-01.md` — superseded v1, kept for history
- `MIGRATION-STATUS.md`, `CURRENT-STATUS.txt` — **stale**, written against v1's assumptions, don't use for planning
- `JMxPOS8/CONVERSION_STATUS.md` — POS-specific progress detail
- `POSTGRESQL_MIGRATION_GUIDE.md` — still useful as SQL conversion reference
