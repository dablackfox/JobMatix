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
        private readonly SerialService _serialService;

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
            _serialService = new SerialService(db);
            SaleItems = new ObservableCollection<SaleLineItem>();
            Payments = new ObservableCollection<Payment>();
        }

        // Result of attempting to add a serialized item, so the UI can show a specific reason
        // rather than a generic "failed to add".
        public enum AddItemResult
        {
            Added,
            NotFound,
            SerialRequired,
            SerialAlreadyInSale,
            SerialAlreadySold
        }

        public async System.Threading.Tasks.Task<AddItemResult> AddItemByBarcodeAsync(string barcode, decimal quantity, string? serialNumber)
        {
            var stock = await _stockService.FindStockByBarcodeAsync(barcode);
            if (stock == null)
                return AddItemResult.NotFound;

            if (stock.RequiresSerial)
            {
                if (string.IsNullOrWhiteSpace(serialNumber))
                    return AddItemResult.SerialRequired;

                serialNumber = serialNumber.Trim();

                if (SaleItems.Any(i => string.Equals(i.SerialNumber, serialNumber, StringComparison.OrdinalIgnoreCase)))
                    return AddItemResult.SerialAlreadyInSale;

                // Refunds legitimately re-record a serial that was previously sold, so only
                // block on an existing SALE for outgoing sale transactions.
                if (TransactionType == "Sale" && await _serialService.IsSerialCurrentlySoldAsync(serialNumber))
                    return AddItemResult.SerialAlreadySold;
            }

            return AddStockItem(stock, quantity, serialNumber) ? AddItemResult.Added : AddItemResult.NotFound;
        }

        public void SetCustomer(Customer customer)
        {
            CurrentCustomer = customer;
        }

        public void SetStaff(Staff? staff)
        {
            CurrentStaff = staff;
        }

        public void SetGstRate(decimal rate)
        {
            _gstRate = rate;
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

        public void RemoveItem(SaleLineItem item)
        {
            SaleItems.Remove(item);
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

        // Parks the current sale so another customer can be served, returning a snapshot
        // that can later be restored with ResumeHeldSale. Refuses if a payment has already
        // been taken against this sale - too late to park safely, commit or clear instead.
        public bool TryHoldCurrentSale(int holdId, string heldByStaffName, out HeldSale? held)
        {
            held = null;
            if (SaleItems.Count == 0)
                return false;
            if (Payments.Count > 0)
                return false;

            held = new HeldSale
            {
                HoldId = holdId,
                HeldByStaffName = heldByStaffName,
                Customer = CurrentCustomer,
                TransactionType = TransactionType,
                DiscountAmount = DiscountAmount,
                Items = SaleItems.Select(i => new SaleLineItem
                {
                    LineNumber = i.LineNumber,
                    Barcode = i.Barcode,
                    SerialNumber = i.SerialNumber,
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Extension = i.Extension,
                    TaxCode = i.TaxCode,
                    StockId = i.StockId
                }).ToList()
            };

            ClearSale();
            return true;
        }

        // Restores a previously held sale as the active sale. Only sensible when the active
        // sale is currently empty - the caller is responsible for checking that first.
        public void ResumeHeldSale(HeldSale held)
        {
            ClearSale();
            CurrentCustomer = held.Customer;
            TransactionType = held.TransactionType;
            DiscountAmount = held.DiscountAmount;

            foreach (var item in held.Items)
                SaleItems.Add(item);
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
                        // Invoice number embeds the real invoice_id (drawn from the same
                        // sequence via the CTE below) rather than a wall-clock timestamp -
                        // two sales committed in the same second used to collide on the
                        // invoicenumber unique constraint.
                        int invoiceId;
                        string transTypeCode = TransactionType.ToUpperInvariant();
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = @"
                                WITH next_id AS (SELECT nextval('invoice_invoice_id_seq') AS id)
                                INSERT INTO invoice (
                                    invoice_id, customer_id, staff_id, transactiontype, invoicedate, invoicenumber,
                                    subtotal, taxamount, total_inc, notes
                                )
                                SELECT id, @customerId, @staffId, @transType, @transDate,
                                       'INV-' || to_char(@transDate, 'YYYYMMDD') || '-' || id::text,
                                       @subtotalEx, @taxAmount, @totalInc, @notes
                                FROM next_id
                                RETURNING invoice_id";

                            AddParameter(cmd, "@customerId", CurrentCustomer?.CustomerId ?? 1); // Default walk-in customer
                            AddParameter(cmd, "@staffId", CurrentStaff!.StaffId);
                            AddParameter(cmd, "@transType", transTypeCode);
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
                                        quantity, unitprice, linetotal, taxcode, serialnumber
                                    ) VALUES (
                                        @invoiceId, @stockId, @description,
                                        @quantity, @unitPrice, @lineTotal, @taxCode, @serialNumber
                                    )";

                                AddParameter(cmd, "@invoiceId", invoiceId);
                                AddParameter(cmd, "@stockId", item.StockId);
                                AddParameter(cmd, "@description", item.Description);
                                AddParameter(cmd, "@quantity", item.Quantity);
                                AddParameter(cmd, "@unitPrice", item.UnitPrice);
                                AddParameter(cmd, "@lineTotal", item.Extension);
                                AddParameter(cmd, "@taxCode", item.TaxCode);
                                AddParameter(cmd, "@serialNumber", (object?)item.SerialNumber ?? DBNull.Value);

                                Console.WriteLine($"[SQL LINE] {cmd.CommandText}");
                                Console.WriteLine($"[PARAMS] invoice={invoiceId}, stock={item.StockId}, qty={item.Quantity}, price={item.UnitPrice}, total={item.Extension}");

                                cmd.ExecuteNonQuery();
                            }

                            // Sales reduce stock on hand; refunds put it back. Quotes and
                            // laybys don't move stock yet (layby holds/releases stock as
                            // part of its own deposit/pickup workflow, not on initial commit).
                            if (TransactionType == "Sale" || TransactionType == "Refund")
                            {
                                decimal quantityDelta = TransactionType == "Sale" ? -item.Quantity : item.Quantity;

                                using (var cmd = conn.CreateCommand())
                                {
                                    cmd.Transaction = transaction;
                                    cmd.CommandText = @"
                                        UPDATE stock
                                        SET quantityinstock = quantityinstock + @quantityDelta,
                                            date_modified = CURRENT_TIMESTAMP
                                        WHERE stock_id = @stockId";

                                    AddParameter(cmd, "@quantityDelta", quantityDelta);
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
                                        invoice_id, customer_id, staff_id, paymentdate,
                                        paymentmethod, amount, paymentreference, transactiontype, cash_drawer
                                    ) VALUES (
                                        @invoiceId, @customerId, @staffId, @paymentDate,
                                        @paymentMethod, @amount, @reference, @transactionType, @cashDrawer
                                    )";

                                AddParameter(cmd, "@invoiceId", invoiceId);
                                AddParameter(cmd, "@customerId", CurrentCustomer?.CustomerId ?? 1);
                                AddParameter(cmd, "@staffId", CurrentStaff!.StaffId);
                                AddParameter(cmd, "@paymentDate", payment.PaymentDate);
                                AddParameter(cmd, "@paymentMethod", payment.PaymentType);
                                AddParameter(cmd, "@amount", payment.Amount);
                                AddParameter(cmd, "@reference", payment.Reference);
                                AddParameter(cmd, "@transactionType", transTypeCode);
                                AddParameter(cmd, "@cashDrawer", payment.CashDrawerId);

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
