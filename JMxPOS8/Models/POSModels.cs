using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace JMxPOS8.Models
{
    // Row in one of the flat id/description lookup tables (GoodsTypes, Brands, Symptoms,
    // TaskTypes) - see Services/ReferenceDataService.cs.
    public class ReferenceItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    // Stock item model
    public class StockItem
    {
        public int StockId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string StockCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal QuantityInStock { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public bool Inactive { get; set; }
        public bool RequiresSerial { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal ReorderQuantity { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }

    // Customer model
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactPosition { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Suburb { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string BusinessPhone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string Abn { get; set; } = string.Empty;
        public string TaxCode { get; set; } = string.Empty;
        public bool IsAccount { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal CreditLimit { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool Inactive { get; set; }
    }

    // Staff model
    public class Staff
    {
        public int StaffId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DocketName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public bool IsAdministrator { get; set; }
        public bool Inactive { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Suburb { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordHint { get; set; } = string.Empty;
    }

    // Invoice model
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public string TransactionType { get; set; } = "Sale"; // Sale, Refund, Quote, Layby
        public DateTime TransactionDate { get; set; }
        public decimal SubtotalEx { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalInc { get; set; }
        public decimal DiscountAmount { get; set; }
        public bool IsOnAccount { get; set; }
        public string Comments { get; set; } = string.Empty;
        public string CashDrawerId { get; set; } = string.Empty;
    }

    // Invoice line model
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public int StockId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Extension { get; set; }
        public string TaxCode { get; set; } = "GST";
        public string? SerialNumber { get; set; }
    }

    // Payment model
    public class Payment
    {
        public int PaymentId { get; set; }
        public int? InvoiceId { get; set; }
        public int CustomerId { get; set; }
        public int StaffId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; } = "CASH"; // CASH, EFTPOS, CREDIT_CARD, CHEQUE, etc.
        public decimal Amount { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string CashDrawerId { get; set; } = string.Empty;
    }

    // Sale line item (for current sale in progress)
    public class SaleLineItem
    {
        public int LineNumber { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string? SerialNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal Extension { get; set; }
        public string TaxCode { get; set; } = "GST";
        public int StockId { get; set; }
    }

    // Customer detail sub-tabs (Invoices/Item Sales/Payments/Quotes) - read-only summaries,
    // one query per tab, mirroring the legacy customer screen's tabbed history views.
    public class CustomerInvoiceSummary
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public decimal TotalInc { get; set; }
    }

    public class CustomerItemSaleSummary
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class CustomerPaymentSummary
    {
        public int InvoiceId { get; set; }
        // Avalonia's IsVisible binding needs an actual bool - binding straight to InvoiceId
        // (0 = no invoice) risks the same silent-conversion-failure class of bug already
        // found once this session with a nullable-object binding.
        public bool HasInvoiceId => InvoiceId > 0;
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = string.Empty;
    }

    // A customer's job/repair history - only possible as a direct query since jobs and
    // customers share one database now (see ROADMAP.md "What Changed" #13).
    public class CustomerJobSummary
    {
        public int JobId { get; set; }
        public DateTime DateUpdated { get; set; }
        public string TechStaffName { get; set; } = string.Empty;
        public string JobStatus { get; set; } = string.Empty;
        public string GoodsInCare { get; set; } = string.Empty;
        public string ProblemShort { get; set; } = string.Empty;
        public string ProblemLong { get; set; } = string.Empty;
        public string ProblemSymptoms { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;

        public string DisplaySummary => ProblemDescriptionHelper.Summarize(ProblemShort, ProblemLong, ProblemSymptoms);
    }

    // ProblemShort is overwhelmingly a legacy workflow marker, not a real description - a
    // direct query found ~85% of real jobs have it set to one of exactly three literal
    // strings ("customer authorized us to proceed" / "needs a quote first" / a spend-limit
    // marker), not anything a person would recognize as "what's wrong with this machine".
    // Shared between JobRecord and CustomerJobSummary so ticket lists show something
    // actually useful instead of "*PROCEED-WITH-SERVICE*;" (direct feedback, 2026-09-01).
    // The legacy "Customer Instruction" 3-way approval flag (ucChildNewJob.vb's
    // optQuotation radio group - "Customer Instruction must be selected" was a hard
    // requirement on the legacy New Job form). Never a real column - it's baked directly
    // into problemshort as one of three boilerplate marker strings, which is also why that
    // field so often shows nothing BUT the marker (direct feedback, 2026-09-01: "there is a
    // proceed with service note on all jobs which was supposed to indicate someone selected
    // proceed with service... to indicate the work was approved"). This port had no control
    // for it at all - restoring it as an explicit selection at intake, kept in the same
    // field/marker format for compatibility with ~26k already-migrated jobs.
    public enum CustomerInstruction
    {
        None,
        QuotationRequired,
        ProceedWithService,
        ProceedToLimit
    }

    public static class ProblemDescriptionHelper
    {
        // Order matters for detection when a job predates the trailing-semicolon format
        // (ucChildNewJob.vb clears both "*X*;" and the older "*X*" on every save) - check
        // the more specific proceed-to-limit marker before proceed-with-service since
        // neither is a prefix of the other, but keep both checked as plain Contains so
        // position within the field (start, end, or mixed with real description text)
        // never matters.
        private static readonly (string Marker, CustomerInstruction Instruction)[] InstructionMarkers =
        {
            ("*PROCEED-TO-LIMIT*", CustomerInstruction.ProceedToLimit),
            ("*PROCEED-WITH-SERVICE*", CustomerInstruction.ProceedWithService),
            ("*QUOTATION-REQUIRED*", CustomerInstruction.QuotationRequired),
        };

        public static CustomerInstruction ExtractCustomerInstruction(string? problemShort)
        {
            if (string.IsNullOrWhiteSpace(problemShort))
                return CustomerInstruction.None;
            foreach (var (marker, instruction) in InstructionMarkers)
                if (problemShort.Contains(marker, StringComparison.OrdinalIgnoreCase))
                    return instruction;
            return CustomerInstruction.None;
        }

        public static string MarkerFor(CustomerInstruction instruction) => instruction switch
        {
            CustomerInstruction.QuotationRequired => "*QUOTATION-REQUIRED*;",
            CustomerInstruction.ProceedWithService => "*PROCEED-WITH-SERVICE*;",
            CustomerInstruction.ProceedToLimit => "*PROCEED-TO-LIMIT*;",
            _ => ""
        };

        public static string LabelFor(CustomerInstruction instruction) => instruction switch
        {
            CustomerInstruction.QuotationRequired => "Quotation Required",
            CustomerInstruction.ProceedWithService => "Approved - Proceed with Service",
            CustomerInstruction.ProceedToLimit => "Approved - Proceed only to Cost Limit",
            _ => ""
        };

        // Strips the marker wherever it sits in the string (start, end, or mixed with real
        // description text - the legacy form appended it to whatever was already typed) so
        // callers see the actual customer-facing description, not internal bookkeeping.
        public static string StripCustomerInstructionMarker(string? problemShort)
        {
            if (string.IsNullOrWhiteSpace(problemShort))
                return string.Empty;
            var result = problemShort;
            foreach (var (marker, _) in InstructionMarkers)
            {
                result = result.Replace(marker + ";", "", StringComparison.OrdinalIgnoreCase);
                result = result.Replace(marker, "", StringComparison.OrdinalIgnoreCase);
            }
            return result.Trim();
        }

        public static string Summarize(string? problemShort, string? problemLong, string? problemSymptoms, int maxLength = 60)
        {
            var cleanedShort = StripCustomerInstructionMarker(problemShort);
            if (!string.IsNullOrWhiteSpace(cleanedShort) && !string.Equals(cleanedShort, "N/A", StringComparison.OrdinalIgnoreCase))
                return Truncate(cleanedShort, maxLength);
            if (!string.IsNullOrWhiteSpace(problemLong))
                return Truncate(problemLong.Replace("\r\n", " ").Replace('\n', ' '), maxLength);
            if (!string.IsNullOrWhiteSpace(problemSymptoms) && !string.Equals(problemSymptoms.Trim(), "N/A", StringComparison.OrdinalIgnoreCase))
                return Truncate(problemSymptoms, maxLength);
            return "(no description)";
        }

        private static string Truncate(string text, int maxLength)
        {
            text = text.Trim();
            return text.Length <= maxLength ? text : text[..maxLength].TrimEnd() + "…";
        }
    }

    // The core Job Tracking record. JobStatus follows the legacy 11-state vocabulary
    // (ROADMAP.md Phase 3): 05-WaitListed, 10-Created, 20-Suspended, 23-InProcessSusp,
    // 30-Started, 33-InProcess, 40-QA, 43-InProcessQA, 50-Completed, 70-Delivered,
    // 97-Cancelled. The "InProcess" variants are a real optimistic-locking mechanism (a job
    // being actively edited shows as locked to anyone else looking at the list) - preserved
    // here, not simplified away.
    public class JobRecord
    {
        public int JobId { get; set; }
        public string CustomerBarcode { get; set; } = string.Empty;
        public int? RmCustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerCompany { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerMobile { get; set; } = string.Empty;
        public string Priority { get; set; } = "H";
        public string NominatedTech { get; set; } = string.Empty;
        public string JobStatus { get; set; } = "10-Created";
        public string GoodsInCare { get; set; } = string.Empty;
        public string GoodsBrand { get; set; } = string.Empty;
        public string GoodsModel { get; set; } = string.Empty;
        public string GoodsOther { get; set; } = string.Empty;
        // The customer's own PC login, collected at intake so a tech can access the
        // machine being repaired - printed on the intake docket (JobDocumentPdfService)
        // exactly like the legacy app did. Stored in plaintext (ROADMAP.md Phase 1 -
        // still-open security fix, tracked separately, not addressed by this feature).
        public string Username { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public bool DataBackupReqd { get; set; }
        public bool DataDiskReqd { get; set; }
        public string ProblemShort { get; set; } = string.Empty;
        public string ProblemLong { get; set; } = string.Empty;
        public string ProblemSymptoms { get; set; } = string.Empty;
        public bool SystemUnderWarranty { get; set; }
        public DateTime DateCreated { get; set; }
        public string RcvdStaffName { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string ServiceNotes { get; set; } = string.Empty;
        public DateTime? DateCompleted { get; set; }
        public string TechStaffName { get; set; } = string.Empty;
        public int? TechRmStaffId { get; set; }
        public DateTime? DateDelivered { get; set; }
        public string DeliveredStaffName { get; set; } = string.Empty;
        public DateTime DateUpdated { get; set; }

        // Uses DisplaySummary rather than the raw ProblemShort - that field is where the
        // Customer Instruction marker lives (see ProblemDescriptionHelper), so showing it
        // raw here used to put "*PROCEED-WITH-SERVICE*;" literally in the ticket header
        // instead of the actual problem description.
        public string Summary => $"#{JobId} - {CustomerName} - {DisplaySummary} - {JobStatus}";
        public bool IsLocked => JobStatus is "23-InProcessSusp" or "33-InProcess" or "43-InProcessQA";
        public string DisplaySummary => ProblemDescriptionHelper.Summarize(ProblemShort, ProblemLong, ProblemSymptoms);
        public CustomerInstruction Instruction => ProblemDescriptionHelper.ExtractCustomerInstruction(ProblemShort);
        public string InstructionLabel => ProblemDescriptionHelper.LabelFor(Instruction);
        public bool HasInstruction => Instruction != CustomerInstruction.None;
        public bool IsQuotationRequired => Instruction == CustomerInstruction.QuotationRequired;
        public bool IsProceedWithService => Instruction == CustomerInstruction.ProceedWithService;
        public bool IsProceedToLimit => Instruction == CustomerInstruction.ProceedToLimit;
    }

    public class JobPartLine
    {
        public int PartId { get; set; }
        public int JobId { get; set; }
        public int? StockId { get; set; }
        public string PartCode { get; set; } = string.Empty;
        public string PartDescr { get; set; } = string.Empty;
        public decimal Quantity { get; set; } = 1;
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public bool IsWarrantyPart { get; set; }

        // Wired up 2026-09-01 - the `parts.serial_number` column existed in the schema
        // untouched by any C# code until now. Required before a job can be Completed for
        // any part whose stock item has RequiresSerial (see JobViewModel.Complete()).
        public string SerialNumber { get; set; } = "";
        public bool RequiresSerial { get; set; }
        public bool MissingRequiredSerial => RequiresSerial && string.IsNullOrWhiteSpace(SerialNumber);

        // Populated at read-time by comparing against the live stock sell price - flags
        // drift since the part was added to the job (the legacy app's gbShowAllParts
        // repricing feature, ROADMAP.md Phase 3).
        public decimal? CurrentSellPrice { get; set; }
        public bool HasPriceDrift => CurrentSellPrice.HasValue && CurrentSellPrice.Value != SellPrice;
    }

    // A ticket note (job_notes) - a running log entry, distinct from the single-value
    // legacy jobs.servicenotes/diagnosis fields. IsPrivate distinguishes an internal-only
    // note from one meant to be customer-facing (ROADMAP.md - added per direct feedback,
    // 2026-09-01: there was nowhere to enter new notes, public or private).
    public class JobNote
    {
        public int NoteId { get; set; }
        public int JobId { get; set; }
        public string NoteText { get; set; } = string.Empty;
        public bool IsPrivate { get; set; } = true;
        public string StaffName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }

    // Ticket time tracking (job_time_entries) - concurrent by design: a running timer is
    // just a row with EndTime == null, so any number of jobs can each have their own
    // running timer at once (ROADMAP.md - direct feedback, 2026-09-01).
    public class JobTimeEntry
    {
        public int EntryId { get; set; }
        public int JobId { get; set; }
        public int? StaffId { get; set; }
        public string StaffName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool Billable { get; set; } = true;

        public bool IsRunning => EndTime == null;
        public TimeSpan Elapsed => (EndTime ?? DateTime.Now) - StartTime;
    }

    // One row in the "jobs with a timer currently running" list - the target of the
    // status-bar indicator's click-through (direct feedback: "clicking it should take us
    // to a filtered list of the jobs with timers").
    public class RunningTimerSummary
    {
        public int EntryId { get; set; }
        public int JobId { get; set; }
        public string StaffName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ProblemSummary { get; set; } = string.Empty;

        public TimeSpan Elapsed => DateTime.Now - StartTime;
    }

    // A physical stock count session - stays open while staff scan items, then gets
    // committed (adjusting stock.quantityinstock to match what was counted) or cancelled.
    public class StocktakeSession
    {
        public int StocktakeId { get; set; }
        public string StocktakeType { get; set; } = string.Empty;
        public bool IsCommitted { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedStaffName { get; set; } = string.Empty;
        public DateTime? DateCommitted { get; set; }
        public string CommittedStaffName { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;

        public string StatusDisplay => IsCancelled ? "Cancelled" : IsCommitted ? "Committed" : "Open";
        public string Summary => $"#{StocktakeId} - {DateCreated:dd-MMM-yyyy HH:mm} - {CreatedStaffName} ({StatusDisplay})";
    }

    // One counted line within a stocktake - qty_on_record is a snapshot of stock.quantityinstock
    // taken the moment the item is first scanned into this session, not a live value.
    public class StocktakeItem
    {
        public int ItemId { get; set; }
        public int StocktakeId { get; set; }
        public int StockId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int QtyOnRecord { get; set; }
        public int QtyCounted { get; set; }
        public int QtyDifference { get; set; }
    }

    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string SupplierName { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string BusinessPhone { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public bool Inactive { get; set; }
    }

    // One line being built up in the Goods Received UI before it's submitted - there is no
    // draft state in the DB (unlike Stocktake): goods_received/_line rows are only written
    // once, at the point of receiving, matching the legacy schema (no is_committed flag).
    public partial class GoodsReceivedLine : ObservableObject
    {
        public int StockId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // decimal (not int) so it binds directly to NumericUpDown.Value with no conversion -
        // the DB column is an integer, so callers round when writing it back.
        [ObservableProperty]
        private decimal _quantity = 1;

        [ObservableProperty]
        private decimal _costEx;

        public decimal LineTotalEx => Quantity * CostEx;

        partial void OnQuantityChanged(decimal value) => OnPropertyChanged(nameof(LineTotalEx));
        partial void OnCostExChanged(decimal value) => OnPropertyChanged(nameof(LineTotalEx));

        // Phase 6.1 (ROADMAP.md): when the scanned item requires serials, the operator
        // enters them here (one per line, comma/newline separated) so each unit's
        // serial_audit row can be stamped with this line's actual cost at receiving time.
        public bool RequiresSerial { get; set; }

        [ObservableProperty]
        private string _serialNumbersText = string.Empty;

        public List<string> ParseSerialNumbers() =>
            SerialNumbersText
                .Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .ToList();
    }

    public class GoodsReceivedSummary
    {
        public int GoodsId { get; set; }
        public DateTime GoodsDate { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string InvoiceNo { get; set; } = string.Empty;
        public decimal TotalInc { get; set; }
    }

    // Supplier warranty-return tracking. Origin can be Job/Counter/Stock - only ~9.5% of
    // real historical RAs actually reference a job (see ROADMAP.md Phase 3 audit), so this
    // is a standalone POS-adjacent feature, not part of the core job workflow.
    public class ReturnAuthorization
    {
        public int RaId { get; set; }
        public int? JobId { get; set; }
        public string CustomerBarcode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string RaNumber { get; set; } = string.Empty;
        public DateTime RaDate { get; set; }
        public string RaStatus { get; set; } = "10-Created";
        public string Origin { get; set; } = "Counter";
        public string ItemDescription { get; set; } = string.Empty;
        public int? RmStockId { get; set; }
        public string ItemBarcode { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string ProblemDescription { get; set; } = string.Empty;
        public string RaSymptoms { get; set; } = string.Empty;
        public string RmaRequestNotes { get; set; } = string.Empty;
        public string SupplierRmaNo { get; set; } = string.Empty;
        public string CourierBarcode { get; set; } = string.Empty;
        public string ReturnResult { get; set; } = string.Empty;
        public string ReturnResultComment { get; set; } = string.Empty;
        public string Resolution { get; set; } = string.Empty;
        public int? StaffIdCreated { get; set; }
        public string StaffNameCreated { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime? DateGoodsSentBack { get; set; }
        public DateTime? DateGoodsReceivedBack { get; set; }
        public DateTime? DateCompleted { get; set; }

        public string Summary => $"#{RaId} - {RaNumber} - {ItemDescription} - {RaStatus}";
    }

    // A parked sale, held aside to serve another customer, that can be resumed later.
    public class HeldSale
    {
        public int HoldId { get; set; }
        public DateTime HeldAt { get; set; } = DateTime.Now;
        public string HeldByStaffName { get; set; } = string.Empty;
        public Customer? Customer { get; set; }
        public string TransactionType { get; set; } = "Sale";
        public decimal DiscountAmount { get; set; }
        public List<SaleLineItem> Items { get; set; } = new();

        public string CustomerDisplay => Customer?.CustomerName ?? "Walk-in Customer";
        public string Summary => $"Hold #{HoldId} - {HeldAt:HH:mm} - {CustomerDisplay} ({Items.Count} item{(Items.Count == 1 ? "" : "s")})";
    }
}
