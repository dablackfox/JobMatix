using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace JMxPOS8.Models
{
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
        public DateTime InvoiceDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class CustomerPaymentSummary
    {
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
        public string ProblemSymptoms { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
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
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerMobile { get; set; } = string.Empty;
        public string Priority { get; set; } = "H";
        public string NominatedTech { get; set; } = string.Empty;
        public string JobStatus { get; set; } = "10-Created";
        public string GoodsInCare { get; set; } = string.Empty;
        public string GoodsBrand { get; set; } = string.Empty;
        public string GoodsModel { get; set; } = string.Empty;
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

        public string Summary => $"#{JobId} - {CustomerName} - {ProblemShort} - {JobStatus}";
        public bool IsLocked => JobStatus is "23-InProcessSusp" or "33-InProcess" or "43-InProcessQA";
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

        // Populated at read-time by comparing against the live stock sell price - flags
        // drift since the part was added to the job (the legacy app's gbShowAllParts
        // repricing feature, ROADMAP.md Phase 3).
        public decimal? CurrentSellPrice { get; set; }
        public bool HasPriceDrift => CurrentSellPrice.HasValue && CurrentSellPrice.Value != SellPrice;
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
