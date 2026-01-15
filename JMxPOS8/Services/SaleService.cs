using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JMxPOS8.Models;

namespace JMxPOS8.Services
{
    /// <summary>
    /// Core POS sale service - manages current sale state, calculations, and transactions
    /// Converted from clsPOS34Sale.vb
    /// </summary>
    public class SaleService
    {
        private readonly DatabaseService _db;
        private readonly StockService _stockService;
        private readonly CustomerService _customerService;

        // Current sale state
        public ObservableCollection<SaleLineItem> SaleItems { get; private set; }
        public Customer? CurrentCustomer { get; private set; }
        public Staff? CurrentStaff { get; private set; }
        public string TransactionType { get; set; } = "Sale"; // Sale, Refund, Quote, Layby
        public string CashDrawerId { get; set; } = "A";

        // Sale totals
        public decimal SubtotalEx => CalculateSubtotalEx();
        public decimal TaxAmount => CalculateTaxAmount();
        public decimal TotalInc => SubtotalEx + TaxAmount;
        public decimal DiscountAmount { get; set; }
        public decimal AmountDue => TotalInc - DiscountAmount;

        // Payment tracking
        public ObservableCollection<Payment> Payments { get; private set; }
        public decimal TotalPaid => Payments?.Sum(p => p.Amount) ?? 0m;
        public decimal Change => TotalPaid - AmountDue;

        // Configuration
        private decimal _gstRate = 10.0m; // Default 10% GST

        public SaleService(DatabaseService db, StockService stockService, CustomerService customerService)
        {
            _db = db;
            _stockService = stockService;
            _customerService = customerService;
            SaleItems = new ObservableCollection<SaleLineItem>();
            Payments = new ObservableCollection<Payment>();
        }

        public void SetCustomer(Customer customer)
        {
            CurrentCustomer = customer;
        }

        public void SetStaff(Staff staff)
        {
            CurrentStaff = staff;
        }

        public void SetGstRate(decimal rate)
        {
            _gstRate = rate;
        }

        public async System.Threading.Tasks.Task<bool> AddItemByBarcodeAsync(string barcode, decimal quantity = 1m)
        {
            var stock = await _stockService.FindStockByBarcodeAsync(barcode);
            if (stock == null)
                return false;

            return AddStockItem(stock, quantity);
        }

        public bool AddStockItem(StockItem stock, decimal quantity = 1m, string? serialNumber = null)
        {
            // Check for serial requirement
            if (stock.RequiresSerial && string.IsNullOrEmpty(serialNumber))
            {
                // Serial required but not provided
                return false;
            }

            // Get next line number
            int lineNumber = SaleItems.Count > 0 ? SaleItems.Max(x => x.LineNumber) + 1 : 1;

            var saleItem = new SaleLineItem
            {
                LineNumber = lineNumber,
                StockId = stock.StockId,
                Barcode = stock.Barcode,
                SerialNumber = serialNumber,
                Description = stock.Description,
                Quantity = quantity,
                UnitPrice = stock.SellPrice,
                Extension = stock.SellPrice * quantity,
                TaxCode = "GST"
            };

            SaleItems.Add(saleItem);
            return true;
        }

        public void RemoveItem(int lineNumber)
        {
            var item = SaleItems.FirstOrDefault(x => x.LineNumber == lineNumber);
            if (item != null)
            {
                SaleItems.Remove(item);
            }
        }

        public void UpdateItemQuantity(int lineNumber, decimal newQuantity)
        {
            var item = SaleItems.FirstOrDefault(x => x.LineNumber == lineNumber);
            if (item != null)
            {
                item.Quantity = newQuantity;
                item.Extension = item.UnitPrice * newQuantity;
            }
        }

        public void UpdateItemPrice(int lineNumber, decimal newPrice)
        {
            var item = SaleItems.FirstOrDefault(x => x.LineNumber == lineNumber);
            if (item != null)
            {
                item.UnitPrice = newPrice;
                item.Extension = item.Quantity * newPrice;
            }
        }

        public void AddPayment(string paymentType, decimal amount, string reference = "")
        {
            Payments.Add(new Payment
            {
                PaymentType = paymentType,
                Amount = amount,
                Reference = reference,
                PaymentDate = DateTime.Now,
                CashDrawerId = CashDrawerId
            });
        }

        public void ClearPayments()
        {
            Payments.Clear();
        }

        public void ClearSale()
        {
            SaleItems.Clear();
            Payments.Clear();
            CurrentCustomer = null;
            DiscountAmount = 0;
            TransactionType = "Sale";
        }

        private decimal CalculateSubtotalEx()
        {
            decimal total = 0m;
            foreach (var item in SaleItems)
            {
                if (item.TaxCode == "GST")
                {
                    // Calculate ex-tax from inc-tax price
                    total += item.Extension / (1 + (_gstRate / 100m));
                }
                else
                {
                    // Tax-free item
                    total += item.Extension;
                }
            }
            return Math.Round(total, 2);
        }

        private decimal CalculateTaxAmount()
        {
            decimal tax = 0m;
            foreach (var item in SaleItems)
            {
                if (item.TaxCode == "GST")
                {
                    decimal exTax = item.Extension / (1 + (_gstRate / 100m));
                    tax += item.Extension - exTax;
                }
            }
            return Math.Round(tax, 2);
        }

        public bool CanCommit()
        {
            // Must have items
            if (SaleItems.Count == 0)
                return false;

            // Must have staff
            if (CurrentStaff == null)
                return false;

            // For sales (not quotes), must have payment or be on-account
            if (TransactionType == "Sale" && TotalPaid < AmountDue && CurrentCustomer?.IsAccount != true)
                return false;

            return true;
        }

        public async System.Threading.Tasks.Task<int> CommitSaleAsync()
        {
            if (!CanCommit())
                throw new InvalidOperationException("Cannot commit sale - validation failed");

            using (var conn = _db.GetConnection())
            {
                await System.Threading.Tasks.Task.Run(() => conn.Open());
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Insert invoice
                        int invoiceId;
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = @"
                                INSERT INTO invoice (
                                    customer_id, staff_id, transactiontype, invoicedate,
                                    subtotal, taxamount, total_inc, notes
                                ) VALUES (
                                    @customerId, @staffId, @transType, @transDate,
                                    @subtotalEx, @taxAmount, @totalInc, @notes
                                ) RETURNING invoice_id";

                            AddParameter(cmd, "@customerId", CurrentCustomer?.CustomerId ?? 1); // Default walk-in customer
                            AddParameter(cmd, "@staffId", CurrentStaff!.StaffId);
                            AddParameter(cmd, "@transType", "SALE");
                            AddParameter(cmd, "@transDate", DateTime.Now);
                            AddParameter(cmd, "@subtotalEx", SubtotalEx);
                            AddParameter(cmd, "@taxAmount", TaxAmount);
                            AddParameter(cmd, "@totalInc", TotalInc);
                            AddParameter(cmd, "@notes", string.Empty);

                            Console.WriteLine($"[SQL INVOICE] {cmd.CommandText}");
                            Console.WriteLine($"[PARAMS] customer={CurrentCustomer?.CustomerId ?? 1}, staff={CurrentStaff!.StaffId}, subtotal={SubtotalEx}, tax={TaxAmount}, total={TotalInc}");

                            invoiceId = Convert.ToInt32(cmd.ExecuteScalar());
                            Console.WriteLine($"[INVOICE CREATED] invoice_id={invoiceId}");
                        }

                        // 2. Insert invoice lines
                        foreach (var item in SaleItems)
                        {
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = @"
                                    INSERT INTO invoice_lines (
                                        invoice_id, stock_id, description,
                                        quantity, unitprice, linetotal, taxcode
                                    ) VALUES (
                                        @invoiceId, @stockId, @description,
                                        @quantity, @unitPrice, @lineTotal, @taxCode
                                    )";

                                AddParameter(cmd, "@invoiceId", invoiceId);
                                AddParameter(cmd, "@stockId", item.StockId);
                                AddParameter(cmd, "@description", item.Description);
                                AddParameter(cmd, "@quantity", item.Quantity);
                                AddParameter(cmd, "@unitPrice", item.UnitPrice);
                                AddParameter(cmd, "@lineTotal", item.Extension);
                                AddParameter(cmd, "@taxCode", item.TaxCode);

                                Console.WriteLine($"[SQL LINE] {cmd.CommandText}");
                                Console.WriteLine($"[PARAMS] invoice={invoiceId}, stock={item.StockId}, qty={item.Quantity}, price={item.UnitPrice}, total={item.Extension}");

                                cmd.ExecuteNonQuery();
                            }

                            // Update stock quantity (for sales, reduce stock)
                            if (TransactionType == "Sale")
                            {
                                using (var cmd = conn.CreateCommand())
                                {
                                    cmd.Transaction = transaction;
                                    cmd.CommandText = @"
                                        UPDATE stock 
                                        SET quantityinstock = quantityinstock - @quantity,
                                            date_modified = CURRENT_TIMESTAMP
                                        WHERE stock_id = @stockId";

                                    AddParameter(cmd, "@quantity", item.Quantity);
                                    AddParameter(cmd, "@stockId", item.StockId);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // 3. Insert payments
                        foreach (var payment in Payments)
                        {
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = @"
                                    INSERT INTO payments (
                                        invoice_id, customer_id, staff_id, payment_date,
                                        payment_type, amount, reference, cashdrawer_id
                                    ) VALUES (
                                        @invoiceId, @customerId, @staffId, @paymentDate,
                                        @paymentType, @amount, @reference, @cashDrawerId
                                    )";

                                AddParameter(cmd, "@invoiceId", invoiceId);
                                AddParameter(cmd, "@customerId", CurrentCustomer?.CustomerId ?? 1);
                                AddParameter(cmd, "@staffId", CurrentStaff!.StaffId);
                                AddParameter(cmd, "@paymentDate", payment.PaymentDate);
                                AddParameter(cmd, "@paymentType", payment.PaymentType);
                                AddParameter(cmd, "@amount", payment.Amount);
                                AddParameter(cmd, "@reference", payment.Reference);
                                AddParameter(cmd, "@cashDrawerId", CashDrawerId);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 4. Update customer balance if on-account
                        if (CurrentCustomer?.IsAccount == true && TotalPaid < AmountDue)
                        {
                            using (var cmd = conn.CreateCommand())
                            {
                                cmd.Transaction = transaction;
                                cmd.CommandText = @"
                                    UPDATE customer 
                                    SET accountbalance = accountbalance + @amount,
                                        date_modified = CURRENT_TIMESTAMP
                                    WHERE customer_id = @customerId";

                                AddParameter(cmd, "@amount", AmountDue - TotalPaid);
                                AddParameter(cmd, "@customerId", CurrentCustomer.CustomerId);
                                cmd.ExecuteNonQuery();
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
            }
        }

        private void AddParameter(System.Data.IDbCommand cmd, string name, object value)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }
    }
}
