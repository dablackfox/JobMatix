using System;

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
}
