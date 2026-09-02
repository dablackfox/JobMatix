using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JMxPOS8.Models;

namespace JMxPOS8.Services;

public class JobService
{
    private readonly DatabaseService _db;

    private const string SelectColumns = @"
        job_id, customerbarcode, rmcustomer_id, customername, customerphone, customermobile,
        priority, nominatedtech, jobstatus, goodsincare, goodsbrand, goodsmodel,
        databackupreqd, datadiskreqd, problemshort, problemlong, problemsymptoms,
        systemunderwarranty, datecreated, rcvdstaffname, diagnosis, servicenotes,
        datecompleted, techstaffname, techrmstaff_id, datedelivered, deliveredstaffname, dateupdated,
        customercompany, username, userpassword, goodsother, datepromised";

    public JobService(DatabaseService db)
    {
        _db = db;
    }

    // 200 was silently cutting off real jobs - only 300 jobs are actually "open" (not
    // delivered/cancelled) in the full migrated dataset (26k+ jobs total), and the cap sat
    // right in the middle of that, quietly hiding ~100 of them from every status group in
    // the Tickets tab's grouped view. 2000 comfortably covers real growth without turning
    // into an unbounded query - a shop with thousands of jobs sitting open at once would be
    // its own problem to notice long before this limit mattered.
    public async Task<List<JobRecord>> GetOpenJobsAsync(int limit = 2000)
    {
        var results = new List<JobRecord>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = $@"
            SELECT {SelectColumns}
            FROM jobs
            WHERE jobstatus NOT IN ('70-Delivered', '97-Cancelled')
            ORDER BY job_id DESC
            LIMIT @limit";
        AddParam(cmd, "@limit", limit);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            results.Add(ReadJob(reader));
        return results;
    }

    // On-site job list (ROADMAP.md "What Changed" #7) - "not a scheduling feature", just
    // this filtered query: any open job flagged ON-SITE, ordered by its promised
    // date/time. Broader status filter than the reminder poller below (any open status,
    // not just not-yet-started) since this is a general work list, not a "needs a
    // heads-up" trigger.
    public async Task<List<JobRecord>> GetOnSiteJobsAsync(int limit = 500)
    {
        var results = new List<JobRecord>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = $@"
            SELECT {SelectColumns}
            FROM jobs
            WHERE UPPER(goodsincare) = @marker
              AND jobstatus NOT IN ('70-Delivered', '97-Cancelled')
            ORDER BY datepromised ASC, job_id DESC
            LIMIT @limit";
        AddParam(cmd, "@marker", JobRecord.OnSiteMarker);
        AddParam(cmd, "@limit", limit);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            results.Add(ReadJob(reader));
        return results;
    }

    public async Task<JobRecord?> GetJobByIdAsync(int jobId)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = $@"
            SELECT {SelectColumns}
            FROM jobs
            WHERE job_id = @jobId";
        AddParam(cmd, "@jobId", jobId);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        if (!await Task.Run(() => reader.Read()))
            return null;
        return ReadJob(reader);
    }

    // Ticket search (any status, including delivered/cancelled - deliberately broader
    // than GetOpenJobsAsync) by exact job number, or a partial match on customer/problem
    // text. Added per direct feedback (2026-09-01): the Tickets tab had no way to find a
    // job that wasn't already in the open-jobs list.
    public async Task<List<JobRecord>> SearchJobsAsync(string term, int limit = 100)
    {
        var results = new List<JobRecord>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = $@"
            SELECT {SelectColumns}
            FROM jobs
            WHERE job_id::text = @exactId
               OR customername ILIKE @pattern
               OR customercompany ILIKE @pattern
               OR customerbarcode ILIKE @pattern
               OR problemshort ILIKE @pattern
               OR problemsymptoms ILIKE @pattern
            ORDER BY job_id DESC
            LIMIT @limit";
        AddParam(cmd, "@exactId", term);
        AddParam(cmd, "@pattern", $"%{term}%");
        AddParam(cmd, "@limit", limit);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
            results.Add(ReadJob(reader));
        return results;
    }

    public async Task<JobRecord> CreateJobAsync(JobRecord job)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        // RETURNING now reuses SelectColumns (a real bug until 2026-09-02: this used to
        // hand-duplicate a shorter column list that was missing customercompany/username/
        // userpassword/goodsother entirely, so ReadJob(reader) always threw
        // IndexOutOfRangeException reading past the actual result set - every "Create
        // Ticket" click silently failed with no visible error, since RelayCommand's
        // AsyncRelayCommand doesn't surface an unobserved task exception to the UI.
        cmd.CommandText = $@"
            INSERT INTO jobs (
                customerbarcode, rmcustomer_id, customername, customerphone, customermobile,
                priority, nominatedtech, goodsincare, goodsbrand, goodsmodel,
                databackupreqd, datadiskreqd, problemshort, problemlong, problemsymptoms,
                systemunderwarranty, rcvdstaffname, datepromised
            ) VALUES (
                @customerBarcode, @rmCustomerId, @customerName, @customerPhone, @customerMobile,
                @priority, @nominatedTech, @goodsInCare, @goodsBrand, @goodsModel,
                @dataBackupReqd, @dataDiskReqd, @problemShort, @problemLong, @problemSymptoms,
                @systemUnderWarranty, @rcvdStaffName, @datePromised
            )
            RETURNING {SelectColumns}";
        AddParam(cmd, "@customerBarcode", job.CustomerBarcode);
        AddParam(cmd, "@rmCustomerId", job.RmCustomerId);
        AddParam(cmd, "@customerName", job.CustomerName);
        AddParam(cmd, "@customerPhone", job.CustomerPhone);
        AddParam(cmd, "@customerMobile", job.CustomerMobile);
        AddParam(cmd, "@priority", job.Priority);
        AddParam(cmd, "@nominatedTech", job.NominatedTech);
        AddParam(cmd, "@goodsInCare", job.GoodsInCare);
        AddParam(cmd, "@goodsBrand", job.GoodsBrand);
        AddParam(cmd, "@goodsModel", job.GoodsModel);
        AddParam(cmd, "@dataBackupReqd", job.DataBackupReqd ? "Y" : "N");
        AddParam(cmd, "@dataDiskReqd", job.DataDiskReqd ? "Y" : "N");
        AddParam(cmd, "@problemShort", job.ProblemShort);
        AddParam(cmd, "@problemLong", job.ProblemLong);
        AddParam(cmd, "@problemSymptoms", job.ProblemSymptoms);
        AddParam(cmd, "@systemUnderWarranty", job.SystemUnderWarranty);
        AddParam(cmd, "@rcvdStaffName", job.RcvdStaffName);
        AddParam(cmd, "@datePromised", job.DatePromised);

        using var reader = await Task.Run(() => cmd.ExecuteReader());
        await Task.Run(() => reader.Read());
        return ReadJob(reader);
    }

    // Opening a job for edit flips a stable status to its locked "InProcess" variant, so
    // anyone else looking at the job list sees it's actively being worked on - a real
    // optimistic-locking mechanism carried over from the legacy app (ROADMAP.md Phase 3),
    // not cosmetic. No-op for statuses that have no locked variant (Created/Completed/etc).
    public async Task<string> OpenForEditAsync(int jobId)
    {
        return await TransitionAsync(jobId, new Dictionary<string, string>
        {
            ["20-Suspended"] = "23-InProcessSusp",
            ["30-Started"] = "33-InProcess",
            ["40-QA"] = "43-InProcessQA"
        });
    }

    public async Task<string> CloseEditAsync(int jobId)
    {
        return await TransitionAsync(jobId, new Dictionary<string, string>
        {
            ["23-InProcessSusp"] = "20-Suspended",
            ["33-InProcess"] = "30-Started",
            ["43-InProcessQA"] = "40-QA"
        });
    }

    public async Task StartWorkAsync(int jobId, string techStaffName, int? techStaffId)
    {
        await RequireTransitionAsync(jobId, new[] { "10-Created", "20-Suspended", "23-InProcessSusp" }, "30-Started",
            extraSql: "techstaffname = @techName, techrmstaff_id = @techId",
            extraParams: cmd =>
            {
                AddParam(cmd, "@techName", techStaffName);
                AddParam(cmd, "@techId", techStaffId);
            });
    }

    // Waitlisted means the goods haven't physically arrived yet (a reservation, e.g. "bring
    // it in Thursday") - direct feedback, 2026-09-01: "waitlisted doesnt give the option to
    // move to new. which means the job has arrived at the store and is waiting to be
    // started." Mirrors the legacy app's "Check-in Job" action.
    public async Task CheckInAsync(int jobId)
        => await RequireTransitionAsync(jobId, new[] { "05-WaitListed" }, "10-Created");

    // Undo an accidental Start Work - direct feedback: "in progress seems fine but doesnt
    // let you move back to new if was mistakenly started."
    public async Task ReturnToNewAsync(int jobId)
        => await RequireTransitionAsync(jobId, new[] { "30-Started", "33-InProcess" }, "10-Created");

    // Upgrades "Quotation Required" to "Proceed with Service" once the customer confirms
    // by phone/callback (direct feedback, 2026-09-01) - the legacy field this was restored
    // from was fixed at intake with no later edit path, but real workflow needs this: the
    // standard-service-charge diagnose-then-quote case (as distinct from a build quote,
    // which is its own separate not-yet-built pipeline - see ROADMAP.md). Rewrites the
    // marker in-place in problemshort, leaving the rest of the description untouched.
    public async Task ApproveQuoteAsync(int jobId)
    {
        var job = await GetJobByIdAsync(jobId);
        if (job == null) return;

        var cleaned = ProblemDescriptionHelper.StripCustomerInstructionMarker(job.ProblemShort);
        var newProblemShort = $"{cleaned} {ProblemDescriptionHelper.MarkerFor(CustomerInstruction.ProceedWithService)}".Trim();

        using (var conn = _db.GetConnection())
        {
            await Task.Run(() => conn.Open());
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE jobs SET problemshort = @problemShort WHERE job_id = @jobId";
            AddParam(cmd, "@problemShort", newProblemShort);
            AddParam(cmd, "@jobId", jobId);
            await Task.Run(() => cmd.ExecuteNonQuery());
        }

        await ConvertQuotePartsToPartsAsync(jobId);
    }

    // Phase 3, 2026-09-01: "the quote is converted to a job with all components added to
    // the job automatically" - the owner's own words. Locks in the quoted sell price (a
    // customer-facing commitment) but re-pulls the current cost price from stock rather
    // than trying to preserve one the quote never actually captured (quote_job_parts has
    // no cost column at all - only quotepart_sell_inc) - cost is an internal accounting
    // figure, so a fresh value is more accurate than none. Leaves the quote_job_parts rows
    // in place afterward as a permanent record of what was quoted, deliberately not deleted.
    private async Task ConvertQuotePartsToPartsAsync(int jobId)
    {
        var quoteParts = await GetQuotePartsAsync(jobId);
        if (quoteParts.Count == 0)
            return;

        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());

        foreach (var quotePart in quoteParts)
        {
            decimal costPrice = 0;
            if (quotePart.StockId.HasValue)
            {
                using var costCmd = conn.CreateCommand();
                costCmd.CommandText = "SELECT costprice FROM stock WHERE stock_id = @stockId";
                AddParam(costCmd, "@stockId", quotePart.StockId.Value);
                var result = await Task.Run(() => costCmd.ExecuteScalar());
                if (result != null && result != DBNull.Value)
                    costPrice = Convert.ToDecimal(result);
            }

            using var insertCmd = conn.CreateCommand();
            insertCmd.CommandText = @"
                INSERT INTO parts (job_id, stock_id, partcode, partdescr, quantity, costprice, sellprice)
                VALUES (@jobId, @stockId, @partCode, @partDescr, @quantity, @costPrice, @sellPrice)";
            AddParam(insertCmd, "@jobId", jobId);
            AddParam(insertCmd, "@stockId", quotePart.StockId.HasValue ? (object)quotePart.StockId.Value : DBNull.Value);
            AddParam(insertCmd, "@partCode", quotePart.Barcode);
            AddParam(insertCmd, "@partDescr", quotePart.Description);
            AddParam(insertCmd, "@quantity", quotePart.Quantity);
            AddParam(insertCmd, "@costPrice", costPrice);
            AddParam(insertCmd, "@sellPrice", quotePart.SellInc);
            await Task.Run(() => insertCmd.ExecuteNonQuery());
        }
    }

    public async Task SuspendAsync(int jobId)
        => await RequireTransitionAsync(jobId, new[] { "30-Started", "33-InProcess" }, "20-Suspended");

    public async Task SendToQaAsync(int jobId)
        => await RequireTransitionAsync(jobId, new[] { "30-Started", "33-InProcess" }, "40-QA");

    public async Task ReopenFromQaAsync(int jobId)
        => await RequireTransitionAsync(jobId, new[] { "40-QA", "43-InProcessQA" }, "30-Started");

    public async Task CompleteAsync(int jobId, string serviceNotes)
    {
        await RequireTransitionAsync(jobId, new[] { "40-QA", "43-InProcessQA" }, "50-Completed",
            extraSql: "datecompleted = CURRENT_TIMESTAMP, servicenotes = @notes",
            extraParams: cmd => AddParam(cmd, "@notes", serviceNotes));
    }

    // Phase 4, 2026-09-01: reuses SaleService.CommitSaleAsync's proven transaction shape
    // (same tax/serial/stock-movement logic) rather than reinventing it - see ROADMAP.md
    // for the full design discussion. One transaction covers the status flip AND the
    // invoice/stock/serial writes; any failure rolls back everything including the status
    // change, so the job stays exactly where it was and Complete can just be retried - no
    // partial "completed but not invoiced" limbo state to design UI for.
    private const decimal GstRate = 10.0m;

    public async Task<int> CompleteJobAndInvoiceAsync(int jobId, int? completingStaffId, string completingStaffName, string serviceNotes)
    {
        var job = await GetJobByIdAsync(jobId);
        if (job == null)
            throw new InvalidOperationException($"Job #{jobId} not found.");

        var parts = await GetJobPartsAsync(jobId);
        var labourRates = await GetLabourRatesAsync();

        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var transaction = conn.BeginTransaction();
        try
        {
            // Idempotency - the only protection available (see create-job-invoice-
            // extensions.sql for why a DB-level unique constraint on job_number isn't
            // possible against real historical data).
            using (var checkCmd = conn.CreateCommand())
            {
                checkCmd.Transaction = transaction;
                checkCmd.CommandText = "SELECT invoice_id FROM invoice WHERE job_number = @jobId LIMIT 1";
                AddParam(checkCmd, "@jobId", jobId);
                var existing = await Task.Run(() => checkCmd.ExecuteScalar());
                if (existing != null && existing != DBNull.Value)
                    throw new InvalidOperationException($"Job #{jobId} has already been invoiced (invoice #{existing}).");
            }

            // Status flip - inline rather than RequireTransitionAsync (which opens its own
            // connection) so it shares this transaction. Same optimistic-lock guard.
            using (var statusCmd = conn.CreateCommand())
            {
                statusCmd.Transaction = transaction;
                statusCmd.CommandText = @"
                    UPDATE jobs
                    SET jobstatus = '50-Completed', datecompleted = CURRENT_TIMESTAMP, servicenotes = @notes
                    WHERE job_id = @jobId AND jobstatus IN ('40-QA', '43-InProcessQA')";
                AddParam(statusCmd, "@notes", serviceNotes);
                AddParam(statusCmd, "@jobId", jobId);
                if (await Task.Run(() => statusCmd.ExecuteNonQuery()) == 0)
                    throw new InvalidOperationException($"Job #{jobId} is not in a state that allows completion.");
            }

            // Build every line's figures in memory first - the invoice header needs the
            // aggregated totals before it can be inserted, and lines need the header's
            // invoiceId before they can be inserted, so this has to happen in this order.
            var lines = new List<InvoiceLineData>();

            foreach (var part in parts)
            {
                int? serialAuditId = null;
                decimal unitCost = part.CostPrice;
                if (part.StockId.HasValue && !string.IsNullOrWhiteSpace(part.SerialNumber))
                {
                    using var lookupCmd = conn.CreateCommand();
                    lookupCmd.Transaction = transaction;
                    lookupCmd.CommandText = @"
                        SELECT serial_id, unit_cost FROM serial_audit
                        WHERE stock_id = @stockId AND serial_number = @serial
                        LIMIT 1";
                    AddParam(lookupCmd, "@stockId", part.StockId.Value);
                    AddParam(lookupCmd, "@serial", part.SerialNumber);
                    using var reader = await Task.Run(() => lookupCmd.ExecuteReader());
                    if (await Task.Run(() => reader.Read()))
                    {
                        serialAuditId = reader.GetInt32(0);
                        unitCost = reader.GetDecimal(1);
                    }
                }

                decimal lineTotalInc = part.SellPrice * part.Quantity;
                decimal costEx = Math.Round(unitCost * part.Quantity, 2);
                lines.Add(BuildLine(
                    stockId: part.StockId, // null routed to the Non-Stock placeholder below
                    description: part.PartDescr,
                    quantity: part.Quantity,
                    unitPriceInc: part.SellPrice,
                    lineTotalInc: lineTotalInc,
                    serialNumber: part.SerialNumber,
                    serialAuditId: serialAuditId,
                    costEx: costEx,
                    movesRealStock: part.StockId.HasValue));
            }

            decimal billableSeconds;
            using (var timeCmd = conn.CreateCommand())
            {
                timeCmd.Transaction = transaction;
                timeCmd.CommandText = @"
                    SELECT COALESCE(SUM(EXTRACT(EPOCH FROM (end_time - start_time))), 0)
                    FROM job_time_entries
                    WHERE job_id = @jobId AND billable = true AND end_time IS NOT NULL";
                AddParam(timeCmd, "@jobId", jobId);
                billableSeconds = Convert.ToDecimal(await Task.Run(() => timeCmd.ExecuteScalar()));
            }
            decimal billableHours = Math.Round(billableSeconds / 3600m, 2);

            if (billableHours > 0)
            {
                var rate = JobDocumentPdfService.RateForPriority(job.Priority, labourRates);
                var labourLineTotal = Math.Round(billableHours * rate, 2);
                if (labourLineTotal > 0)
                {
                    var labourStockId = await GetStockIdByBarcodeAsync(conn, transaction, "LABOUR-SVC");
                    lines.Add(BuildLine(
                        stockId: labourStockId,
                        description: $"Labour ({billableHours:0.##} hrs)",
                        quantity: 1,
                        unitPriceInc: labourLineTotal,
                        lineTotalInc: labourLineTotal,
                        serialNumber: null,
                        serialAuditId: null,
                        costEx: 0,
                        movesRealStock: false));
                }
            }

            var nonStockPlaceholderId = lines.Any(l => l.StockId == null)
                ? await GetStockIdByBarcodeAsync(conn, transaction, "NONSTOCK-MISC")
                : (int?)null;

            decimal subtotalEx = lines.Sum(l => l.SellEx);
            decimal taxAmount = lines.Sum(l => l.SellInc - l.SellEx);
            decimal totalInc = subtotalEx + taxAmount;
            var transDate = DateTime.Now; // app clock, matching CommitSaleAsync's own convention

            int invoiceId;
            using (var headerCmd = conn.CreateCommand())
            {
                headerCmd.Transaction = transaction;
                headerCmd.CommandText = @"
                    WITH next_id AS (SELECT nextval('invoice_invoice_id_seq') AS id)
                    INSERT INTO invoice (
                        invoice_id, customer_id, staff_id, transactiontype, invoicedate, invoicenumber,
                        subtotal, taxamount, total_inc, notes, job_number
                    )
                    SELECT id, @customerId, @staffId, 'SALE', @transDate,
                           'INV-' || to_char(@transDate, 'YYYYMMDD') || '-' || id::text,
                           @subtotalEx, @taxAmount, @totalInc, @notes, @jobId
                    FROM next_id
                    RETURNING invoice_id";
                AddParam(headerCmd, "@customerId", job.RmCustomerId ?? 1);
                AddParam(headerCmd, "@staffId", completingStaffId);
                AddParam(headerCmd, "@transDate", transDate);
                AddParam(headerCmd, "@subtotalEx", subtotalEx);
                AddParam(headerCmd, "@taxAmount", taxAmount);
                AddParam(headerCmd, "@totalInc", totalInc);
                AddParam(headerCmd, "@notes", $"Job #{jobId} completion - {completingStaffName}");
                AddParam(headerCmd, "@jobId", jobId);
                invoiceId = Convert.ToInt32(await Task.Run(() => headerCmd.ExecuteScalar()));
            }

            foreach (var line in lines)
            {
                int lineId;
                using (var lineCmd = conn.CreateCommand())
                {
                    lineCmd.Transaction = transaction;
                    lineCmd.CommandText = @"
                        INSERT INTO invoice_lines (
                            invoice_id, stock_id, description,
                            quantity, unitprice, linetotal, taxcode, serialnumber,
                            serial_audit_id, cost_ex, cost_inc, sell_ex, sell_inc, gross_profit
                        ) VALUES (
                            @invoiceId, @stockId, @description,
                            @quantity, @unitPrice, @lineTotal, @taxCode, @serialNumber,
                            @serialAuditId, @costEx, @costInc, @sellEx, @sellInc, @grossProfit
                        )
                        RETURNING line_id";
                    AddParam(lineCmd, "@invoiceId", invoiceId);
                    AddParam(lineCmd, "@stockId", line.StockId ?? nonStockPlaceholderId);
                    AddParam(lineCmd, "@description", line.Description);
                    AddParam(lineCmd, "@quantity", line.Quantity);
                    AddParam(lineCmd, "@unitPrice", line.UnitPriceInc);
                    AddParam(lineCmd, "@lineTotal", line.LineTotalInc);
                    AddParam(lineCmd, "@taxCode", "GST");
                    AddParam(lineCmd, "@serialNumber", (object?)line.SerialNumber ?? DBNull.Value);
                    AddParam(lineCmd, "@serialAuditId", (object?)line.SerialAuditId ?? DBNull.Value);
                    AddParam(lineCmd, "@costEx", line.CostEx);
                    AddParam(lineCmd, "@costInc", line.CostInc);
                    AddParam(lineCmd, "@sellEx", line.SellEx);
                    AddParam(lineCmd, "@sellInc", line.SellInc);
                    AddParam(lineCmd, "@grossProfit", line.GrossProfit);
                    lineId = Convert.ToInt32(await Task.Run(() => lineCmd.ExecuteScalar()));
                }

                // A part physically leaving the shelf reduces stock on hand, exactly like a
                // Sale - not the Labour or Non-Stock placeholder lines, which aren't real
                // inventory. Same serial_audit/serial_audit_trail sync as CommitSaleAsync.
                if (line.MovesRealStock && line.StockId.HasValue)
                {
                    using (var stockCmd = conn.CreateCommand())
                    {
                        stockCmd.Transaction = transaction;
                        stockCmd.CommandText = @"
                            UPDATE stock
                            SET quantityinstock = quantityinstock - @quantity,
                                date_modified = CURRENT_TIMESTAMP
                            WHERE stock_id = @stockId";
                        AddParam(stockCmd, "@quantity", line.Quantity);
                        AddParam(stockCmd, "@stockId", line.StockId.Value);
                        await Task.Run(() => stockCmd.ExecuteNonQuery());
                    }

                    if (line.SerialAuditId.HasValue)
                    {
                        using (var serialCmd = conn.CreateCommand())
                        {
                            serialCmd.Transaction = transaction;
                            serialCmd.CommandText = @"
                                UPDATE serial_audit
                                SET is_in_stock = false, status = 'SOLD', date_modified = CURRENT_TIMESTAMP
                                WHERE serial_id = @serialId";
                            AddParam(serialCmd, "@serialId", line.SerialAuditId.Value);
                            await Task.Run(() => serialCmd.ExecuteNonQuery());
                        }
                        using (var trailCmd = conn.CreateCommand())
                        {
                            trailCmd.Transaction = transaction;
                            trailCmd.CommandText = @"
                                INSERT INTO serial_audit_trail (stock_id, serial_audit_id, tran_type, type_id, type_line_id, movement, rm_tr_detail)
                                VALUES (@stockId, @serialId, 'SALE', @invoiceId, @lineId, -1, @detail)";
                            AddParam(trailCmd, "@stockId", line.StockId.Value);
                            AddParam(trailCmd, "@serialId", line.SerialAuditId.Value);
                            AddParam(trailCmd, "@invoiceId", invoiceId);
                            AddParam(trailCmd, "@lineId", lineId);
                            AddParam(trailCmd, "@detail", $"Job #{jobId} completion - invoice {invoiceId}");
                            await Task.Run(() => trailCmd.ExecuteNonQuery());
                        }
                    }
                }
            }

            transaction.Commit();
            return invoiceId;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static async Task<int?> GetStockIdByBarcodeAsync(System.Data.IDbConnection conn, System.Data.IDbTransaction transaction, string barcode)
    {
        using var cmd = conn.CreateCommand();
        cmd.Transaction = transaction;
        cmd.CommandText = "SELECT stock_id FROM stock WHERE barcode = @barcode LIMIT 1";
        AddParam(cmd, "@barcode", barcode);
        var result = await Task.Run(() => cmd.ExecuteScalar());
        return result == null || result == DBNull.Value ? null : Convert.ToInt32(result);
    }

    private static InvoiceLineData BuildLine(int? stockId, string description, decimal quantity, decimal unitPriceInc,
        decimal lineTotalInc, string? serialNumber, int? serialAuditId, decimal costEx, bool movesRealStock)
    {
        decimal costInc = Math.Round(costEx * (1 + (GstRate / 100m)), 2);
        decimal sellInc = lineTotalInc;
        decimal sellEx = Math.Round(sellInc / (1 + (GstRate / 100m)), 2);
        return new InvoiceLineData
        {
            StockId = stockId,
            Description = description,
            Quantity = quantity,
            UnitPriceInc = unitPriceInc,
            LineTotalInc = lineTotalInc,
            SerialNumber = string.IsNullOrWhiteSpace(serialNumber) ? null : serialNumber,
            SerialAuditId = serialAuditId,
            CostEx = costEx,
            CostInc = costInc,
            SellEx = sellEx,
            SellInc = sellInc,
            GrossProfit = sellEx - costEx,
            MovesRealStock = movesRealStock
        };
    }

    private class InvoiceLineData
    {
        public int? StockId { get; set; }
        public string Description { get; set; } = "";
        public decimal Quantity { get; set; }
        public decimal UnitPriceInc { get; set; }
        public decimal LineTotalInc { get; set; }
        public string? SerialNumber { get; set; }
        public int? SerialAuditId { get; set; }
        public decimal CostEx { get; set; }
        public decimal CostInc { get; set; }
        public decimal SellEx { get; set; }
        public decimal SellInc { get; set; }
        public decimal GrossProfit { get; set; }
        public bool MovesRealStock { get; set; }
    }

    public async Task DeliverAsync(int jobId, string deliveredStaffName, int? deliveredStaffId)
    {
        await RequireTransitionAsync(jobId, new[] { "50-Completed" }, "70-Delivered",
            extraSql: "datedelivered = CURRENT_TIMESTAMP, deliveredstaffname = @staffName, deliveredrmstaff_id = @staffId",
            extraParams: cmd =>
            {
                AddParam(cmd, "@staffName", deliveredStaffName);
                AddParam(cmd, "@staffId", deliveredStaffId);
            });
    }

    public async Task CancelAsync(int jobId)
        => await RequireTransitionAsync(jobId,
            new[] { "05-WaitListed", "10-Created", "20-Suspended", "23-InProcessSusp", "30-Started", "33-InProcess", "40-QA", "43-InProcessQA" },
            "97-Cancelled");

    public async Task<List<JobPartLine>> GetJobPartsAsync(int jobId)
    {
        var results = new List<JobPartLine>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT p.part_id, p.job_id, p.stock_id, p.partcode, p.partdescr, p.quantity,
                   p.costprice, p.sellprice, p.is_warranty_part, st.sellprice, p.serial_number,
                   COALESCE(st.requiresserial, false)
            FROM parts p
            LEFT JOIN stock st ON st.stock_id = p.stock_id
            WHERE p.job_id = @jobId
            ORDER BY p.part_id";
        AddParam(cmd, "@jobId", jobId);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            results.Add(new JobPartLine
            {
                PartId = reader.GetInt32(0),
                JobId = reader.GetInt32(1),
                StockId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                PartCode = reader.GetString(3),
                PartDescr = reader.GetString(4),
                Quantity = reader.GetDecimal(5),
                CostPrice = reader.GetDecimal(6),
                SellPrice = reader.GetDecimal(7),
                IsWarrantyPart = reader.GetBoolean(8),
                CurrentSellPrice = reader.IsDBNull(9) ? null : reader.GetDecimal(9),
                SerialNumber = reader.GetString(10),
                RequiresSerial = reader.GetBoolean(11)
            });
        }
        return results;
    }

    // SerialRequired mirrors SaleService.AddItemResult.SerialRequired exactly - same
    // enforcement pattern (stock.RequiresSerial blocks the add until a serial is given),
    // just ported to the Job/Parts path, which never had it (direct feedback, 2026-09-01:
    // serials should be "required before job is completed" - this is the entry point that
    // makes that possible to check for in Complete()).
    public enum AddPartResult { Added, NotFound, SerialRequired }

    public async Task<AddPartResult> AddPartByBarcodeAsync(int jobId, string barcode, StockService stockService, int? staffId, string staffName, string? serialNumber = null)
    {
        var stock = await stockService.FindStockByBarcodeAsync(barcode);
        if (stock == null)
            return AddPartResult.NotFound;
        if (stock.RequiresSerial && string.IsNullOrWhiteSpace(serialNumber))
            return AddPartResult.SerialRequired;

        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO parts (
                job_id, stock_id, partcode, partdescr, quantity, costprice, sellprice,
                serviced_by_staff_id, serviced_by_staff_name, serial_number
            ) VALUES (
                @jobId, @stockId, @partCode, @partDescr, 1, @costPrice, @sellPrice,
                @staffId, @staffName, @serialNumber
            )";
        AddParam(cmd, "@jobId", jobId);
        AddParam(cmd, "@stockId", stock.StockId);
        AddParam(cmd, "@partCode", stock.Barcode);
        AddParam(cmd, "@partDescr", stock.Description);
        AddParam(cmd, "@costPrice", stock.CostPrice);
        AddParam(cmd, "@sellPrice", stock.SellPrice);
        AddParam(cmd, "@staffId", staffId);
        AddParam(cmd, "@staffName", staffName);
        AddParam(cmd, "@serialNumber", serialNumber ?? "");
        await Task.Run(() => cmd.ExecuteNonQuery());
        return AddPartResult.Added;
    }

    public async Task RemovePartAsync(int partId)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM parts WHERE part_id = @partId";
        AddParam(cmd, "@partId", partId);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    // Quote Parts (Phase 2, 2026-09-01) - proposed components on a ticket at "Quotation
    // Required," backed by the legacy quote_job_parts table. Reads/writes the same shape
    // for both the 7,842 rows of historical data and new tickets going forward - no
    // separate "is this historical" flag needed, the table means the same thing either way.
    public async Task<List<QuotePartLine>> GetQuotePartsAsync(int jobId)
    {
        var results = new List<QuotePartLine>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT quotepart_id, quotepart_jobid, quotepart_stockid, quotepart_barcode,
                   quotepart_description, quotepart_orderqty, quotepart_sell_inc, date_created
            FROM quote_job_parts
            WHERE quotepart_jobid = @jobId
            ORDER BY quotepart_id";
        AddParam(cmd, "@jobId", jobId);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            results.Add(new QuotePartLine
            {
                QuotePartId = reader.GetInt32(0),
                JobId = reader.GetInt32(1),
                StockId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
                Barcode = reader.GetString(3),
                Description = reader.GetString(4),
                Quantity = reader.GetInt32(5),
                SellInc = reader.GetDecimal(6),
                DateCreated = reader.GetDateTime(7)
            });
        }
        return results;
    }

    public enum AddQuotePartResult { Added, NotFound }

    // Defaults the proposed price from the current stock sell price but doesn't force it -
    // a quote is a price proposal, staff may reasonably want to adjust it before the
    // customer sees it (see UpdateQuotePartAsync). quotepart_orderid is left NULL - that
    // column ties to a legacy purchase-order concept this phase doesn't touch.
    public async Task<AddQuotePartResult> AddQuotePartByBarcodeAsync(int jobId, string barcode, int quantity, StockService stockService)
    {
        var stock = await stockService.FindStockByBarcodeAsync(barcode);
        if (stock == null)
            return AddQuotePartResult.NotFound;

        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO quote_job_parts (
                quotepart_jobid, quotepart_stockid, quotepart_barcode, quotepart_description,
                quotepart_orderqty, quotepart_sell_inc
            ) VALUES (
                @jobId, @stockId, @barcode, @description, @quantity, @sellInc
            )";
        AddParam(cmd, "@jobId", jobId);
        AddParam(cmd, "@stockId", stock.StockId);
        AddParam(cmd, "@barcode", stock.Barcode);
        AddParam(cmd, "@description", stock.Description);
        AddParam(cmd, "@quantity", quantity);
        AddParam(cmd, "@sellInc", stock.SellPrice);
        await Task.Run(() => cmd.ExecuteNonQuery());
        return AddQuotePartResult.Added;
    }

    public async Task UpdateQuotePartAsync(int quotePartId, int quantity, decimal sellInc)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE quote_job_parts SET quotepart_orderqty = @quantity, quotepart_sell_inc = @sellInc WHERE quotepart_id = @quotePartId";
        AddParam(cmd, "@quantity", quantity);
        AddParam(cmd, "@sellInc", sellInc);
        AddParam(cmd, "@quotePartId", quotePartId);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    public async Task RemoveQuotePartAsync(int quotePartId)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM quote_job_parts WHERE quotepart_id = @quotePartId";
        AddParam(cmd, "@quotePartId", quotePartId);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    // On-site staff SMS reminder poller (ROADMAP.md "What Changed" #7 / Phase 3 "important,
    // independently schedulable" list). The legacy version (clsStaffReminders.vb) sent up to
    // 2 SMS/job/day (a same-day "wake up" plus a closer-to-time "reminder") gated by 3
    // configurable systeminfo keys. Deliberately simplified here to one reminder per job per
    // day - the real goal ("make sure staff know about today's on-site jobs") doesn't need
    // the two-stage cadence, and it avoids inventing a small state machine for marginal
    // value. Only considers jobs not yet started (05-WaitListed/10-Created) - once work has
    // actually begun the "don't forget about this" reminder has served its purpose.
    public record OnSiteReminderCandidate(int JobId, string CustomerName, string CustomerPhone,
        DateTime? DatePromised, string TechLabel, string? TechMobile);

    public async Task<List<OnSiteReminderCandidate>> GetOnSiteJobsDueForReminderAsync(DateTime today)
    {
        var results = new List<OnSiteReminderCandidate>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT j.job_id, j.customername, j.customerphone, j.datepromised,
                   COALESCE(NULLIF(NULLIF(j.techstaffname, ''), 'N/A'), NULLIF(NULLIF(j.nominatedtech, ''), 'N/A'), '') AS tech_label,
                   COALESCE(
                       ts.mobile,
                       -- Correlated subquery, not a JOIN - docket_name isn't unique (real data
                       -- has duplicate 'Jay' staff rows), so a plain LEFT JOIN on it would fan
                       -- out into duplicate reminder candidates for the one job and could send
                       -- the SMS twice. LIMIT 1 picks one deterministically instead.
                       (SELECT s2.mobile FROM staff s2
                        WHERE s2.docket_name = j.nominatedtech AND j.nominatedtech <> ''
                        ORDER BY s2.staff_id LIMIT 1)
                   ) AS tech_mobile
            FROM jobs j
            LEFT JOIN staff ts ON j.techrmstaff_id = ts.staff_id
            WHERE UPPER(j.goodsincare) = @marker
              AND j.jobstatus IN ('05-WaitListed', '10-Created')
              AND j.datepromised::date = @today
              AND j.datepromised::date <> @sentinel1
              AND j.datepromised::date <> @sentinel2
              AND NOT EXISTS (
                  SELECT 1 FROM jobother jo
                  WHERE jo.job_id = j.job_id AND jo.fieldname = @sentField AND jo.fieldvalue = @todayStr
              )";
        AddParam(cmd, "@marker", JobRecord.OnSiteMarker);
        AddParam(cmd, "@today", today.Date);
        AddParam(cmd, "@sentinel1", JobRecord.DatePromisedSentinels[0]);
        AddParam(cmd, "@sentinel2", JobRecord.DatePromisedSentinels[1]);
        AddParam(cmd, "@sentField", OnSiteReminderSentField);
        AddParam(cmd, "@todayStr", today.ToString("yyyy-MM-dd"));
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            results.Add(new OnSiteReminderCandidate(
                JobId: reader.GetInt32(0),
                CustomerName: reader.GetString(1),
                CustomerPhone: reader.GetString(2),
                DatePromised: reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                TechLabel: reader.GetString(4),
                TechMobile: reader.IsDBNull(5) ? null : reader.GetString(5)));
        }
        return results;
    }

    // Opt-in switch (systeminfo, config-via-DB like LabourHourlyRatePriority* - no
    // dedicated settings UI, matching that existing precedent) - defaults off, matching
    // the legacy app's own default, since a fresh unconfigured store shouldn't suddenly
    // start texting staff.
    public async Task<bool> IsOnSiteSmsRemindersEnabledAsync()
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT info_value FROM systeminfo WHERE info_key = 'EnableOnSiteSmsReminders'";
        var result = await Task.Run(() => cmd.ExecuteScalar());
        return result is string s && s.Equals("Y", StringComparison.OrdinalIgnoreCase);
    }

    private const string OnSiteReminderSentField = "ONSITE_SMS_SENT";

    public async Task MarkOnSiteReminderSentAsync(int jobId, DateTime today)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO jobother (job_id, fieldname, fieldvalue, datecreated)
            VALUES (@jobId, @fieldName, @fieldValue, @dateCreated)";
        AddParam(cmd, "@jobId", jobId);
        AddParam(cmd, "@fieldName", OnSiteReminderSentField);
        AddParam(cmd, "@fieldValue", today.ToString("yyyy-MM-dd"));
        AddParam(cmd, "@dateCreated", DateTime.Now);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    public async Task UpdateDatePromisedAsync(int jobId, DateTime? datePromised)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = "UPDATE jobs SET datepromised = @datePromised WHERE job_id = @jobId";
        AddParam(cmd, "@datePromised", datePromised);
        AddParam(cmd, "@jobId", jobId);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    // Matches the legacy pattern (frmNotifyCust22.vb) of logging every sent notification
    // straight onto the job record rather than a separate notifications table.
    public async Task AppendNotificationAsync(int jobId, string note)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            UPDATE jobs
            SET notifications = notifications || @note
            WHERE job_id = @jobId";
        AddParam(cmd, "@note", $"[{DateTime.Now:dd-MMM-yyyy HH:mm}] {note}\n");
        AddParam(cmd, "@jobId", jobId);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    private async Task<string> TransitionAsync(int jobId, Dictionary<string, string> statusMap)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());

        string currentStatus;
        using (var fetchCmd = conn.CreateCommand())
        {
            fetchCmd.CommandText = "SELECT jobstatus FROM jobs WHERE job_id = @jobId";
            AddParam(fetchCmd, "@jobId", jobId);
            var result = await Task.Run(() => fetchCmd.ExecuteScalar());
            if (result == null) throw new InvalidOperationException($"Job #{jobId} not found.");
            currentStatus = (string)result;
        }

        if (!statusMap.TryGetValue(currentStatus, out var newStatus))
            return currentStatus;

        using var updateCmd = conn.CreateCommand();
        updateCmd.CommandText = "UPDATE jobs SET jobstatus = @newStatus WHERE job_id = @jobId";
        AddParam(updateCmd, "@newStatus", newStatus);
        AddParam(updateCmd, "@jobId", jobId);
        await Task.Run(() => updateCmd.ExecuteNonQuery());
        return newStatus;
    }

    private async Task RequireTransitionAsync(int jobId, string[] fromStatuses, string toStatus,
        string? extraSql = null, Action<System.Data.IDbCommand>? extraParams = null)
    {
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        var paramNames = new List<string>();
        for (int i = 0; i < fromStatuses.Length; i++) paramNames.Add($"@from{i}");
        cmd.CommandText = $@"
            UPDATE jobs
            SET jobstatus = @toStatus{(extraSql != null ? ", " + extraSql : "")}
            WHERE job_id = @jobId AND jobstatus IN ({string.Join(",", paramNames)})";
        AddParam(cmd, "@toStatus", toStatus);
        AddParam(cmd, "@jobId", jobId);
        for (int i = 0; i < fromStatuses.Length; i++) AddParam(cmd, paramNames[i], fromStatuses[i]);
        extraParams?.Invoke(cmd);

        if (await Task.Run(() => cmd.ExecuteNonQuery()) == 0)
            throw new InvalidOperationException($"Job #{jobId} is not in a state that allows this action.");
    }

    // Per-priority labour hourly rates (systeminfo, seeded from real legacy values -
    // see sql-scripts/seed-labour-rates.sql). Used for the intake docket's terms
    // section (JobDocumentPdfService) and job reporting (ReportsViewModel).
    public async Task<Dictionary<string, decimal>> GetLabourRatesAsync()
    {
        var rates = new Dictionary<string, decimal>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT info_key, info_value FROM systeminfo
            WHERE info_key IN ('LabourHourlyRatePriority1', 'LabourHourlyRatePriority2', 'LabourHourlyRatePriority3')";
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            if (decimal.TryParse(reader.GetString(1), System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture, out var rate))
                rates[reader.GetString(0)] = rate;
        }
        return rates;
    }

    // Ticket notes (job_notes) - a running log, distinct from the single-value legacy
    // servicenotes/diagnosis columns. See sql-scripts/create-job-notes-table.sql.
    public async Task<List<JobNote>> GetJobNotesAsync(int jobId)
    {
        var results = new List<JobNote>();
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT note_id, job_id, note_text, is_private, staff_name, date_created
            FROM job_notes
            WHERE job_id = @jobId
            ORDER BY date_created DESC";
        AddParam(cmd, "@jobId", jobId);
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            results.Add(new JobNote
            {
                NoteId = reader.GetInt32(0),
                JobId = reader.GetInt32(1),
                NoteText = reader.GetString(2),
                IsPrivate = reader.GetBoolean(3),
                StaffName = reader.GetString(4),
                DateCreated = reader.GetDateTime(5)
            });
        }
        return results;
    }

    public async Task AddJobNoteAsync(int jobId, string noteText, bool isPrivate, string staffName)
    {
        // date_created must be stamped with app-local time, not the DB's own
        // CURRENT_TIMESTAMP - the DB container's clock runs UTC, so letting it default
        // made every note appear to have been written hours in the future/past relative
        // to the staff member who just typed it (matches the JobTimeService fix for the
        // same underlying clock mismatch).
        using var conn = _db.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            INSERT INTO job_notes (job_id, note_text, is_private, staff_name, date_created)
            VALUES (@jobId, @noteText, @isPrivate, @staffName, @dateCreated)";
        AddParam(cmd, "@jobId", jobId);
        AddParam(cmd, "@noteText", noteText);
        AddParam(cmd, "@isPrivate", isPrivate);
        AddParam(cmd, "@staffName", staffName);
        AddParam(cmd, "@dateCreated", DateTime.Now);
        await Task.Run(() => cmd.ExecuteNonQuery());
    }

    private static JobRecord ReadJob(System.Data.IDataReader reader) => new()
    {
        JobId = reader.GetInt32(0),
        CustomerBarcode = reader.GetString(1),
        RmCustomerId = reader.IsDBNull(2) ? null : reader.GetInt32(2),
        CustomerName = reader.GetString(3),
        CustomerPhone = reader.GetString(4),
        CustomerMobile = reader.GetString(5),
        Priority = reader.GetString(6),
        NominatedTech = reader.GetString(7),
        JobStatus = reader.GetString(8),
        GoodsInCare = reader.GetString(9),
        GoodsBrand = reader.GetString(10),
        GoodsModel = reader.GetString(11),
        DataBackupReqd = reader.GetString(12) == "Y",
        DataDiskReqd = reader.GetString(13) == "Y",
        ProblemShort = reader.GetString(14),
        ProblemLong = reader.GetString(15),
        ProblemSymptoms = reader.GetString(16),
        SystemUnderWarranty = reader.GetBoolean(17),
        DateCreated = reader.GetDateTime(18),
        RcvdStaffName = reader.GetString(19),
        Diagnosis = reader.GetString(20),
        ServiceNotes = reader.GetString(21),
        DateCompleted = reader.IsDBNull(22) ? null : reader.GetDateTime(22),
        TechStaffName = reader.GetString(23),
        TechRmStaffId = reader.IsDBNull(24) ? null : reader.GetInt32(24),
        DateDelivered = reader.IsDBNull(25) ? null : reader.GetDateTime(25),
        DeliveredStaffName = reader.GetString(26),
        DateUpdated = reader.GetDateTime(27),
        CustomerCompany = reader.GetString(28),
        Username = reader.GetString(29),
        UserPassword = reader.GetString(30),
        GoodsOther = reader.GetString(31),
        DatePromised = reader.IsDBNull(32) ? null : reader.GetDateTime(32)
    };

    private static void AddParam(System.Data.IDbCommand cmd, string name, object? value)
    {
        var param = cmd.CreateParameter();
        param.ParameterName = name;
        param.Value = value ?? DBNull.Value;
        cmd.Parameters.Add(param);
    }
}
