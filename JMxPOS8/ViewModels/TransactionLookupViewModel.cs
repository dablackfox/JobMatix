using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Models;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels
{
    public partial class TransactionLookupViewModel : ViewModelBase
    {
        private readonly DatabaseService _dbService;
        private readonly CustomerService _customerService;
        private readonly StaffService _staffService;

        [ObservableProperty]
        private string _lookupType = "Invoice"; // Invoice, Quote, Payment

        [ObservableProperty]
        private string _staffBarcode = "";

        [ObservableProperty]
        private string _customerBarcode = "";

        [ObservableProperty]
        private string _itemBarcode = "";

        [ObservableProperty]
        private string _serialNumber = "";

        [ObservableProperty]
        private string _datePeriod = "Any"; // Today, ThisMonth, 12Months, Any, Custom

        [ObservableProperty]
        private DateTime _dateFrom = DateTime.Now.AddMonths(-12);

        [ObservableProperty]
        private DateTime _dateTo = DateTime.Now;

        [ObservableProperty]
        private TransactionSummary? _selectedTransaction;

        [ObservableProperty]
        private string _statusMessage = "";

        public ObservableCollection<TransactionSummary> Transactions { get; }

        // Voiding is a manager-override action, checked at the point of use (same pattern
        // as Staff admin) rather than tied to any persistent signed-in session.
        [ObservableProperty]
        private bool _isVoidPromptOpen;

        [ObservableProperty]
        private string _voidOverrideBarcode = "";

        [ObservableProperty]
        private string _voidStatusMessage = "";

        public TransactionLookupViewModel(DatabaseService dbService, CustomerService customerService, StaffService staffService)
        {
            _dbService = dbService;
            _customerService = customerService;
            _staffService = staffService;
            Transactions = new ObservableCollection<TransactionSummary>();
        }

        [RelayCommand]
        private async Task Search()
        {
            try
            {
                StatusMessage = "Searching...";
                Transactions.Clear();

                var results = await SearchTransactionsAsync();
                
                foreach (var item in results)
                {
                    Transactions.Add(item);
                }

                StatusMessage = $"Found {Transactions.Count} transactions";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                Console.WriteLine($"[LOOKUP] Error searching: {ex.Message}");
            }
        }

        // Used by "Show Last Invoice" - runs an unfiltered, newest-first search and
        // selects the given invoice if it's found.
        public async Task SelectInvoiceByIdAsync(int invoiceId)
        {
            LookupType = "Invoice";
            ClearFilters();
            await Search();
            SelectedTransaction = Transactions.FirstOrDefault(t => t.TransactionId == invoiceId);
            StatusMessage = SelectedTransaction != null
                ? $"Showing invoice #{invoiceId}"
                : $"Invoice #{invoiceId} not found";
        }

        [RelayCommand]
        private void ClearFilters()
        {
            StaffBarcode = "";
            CustomerBarcode = "";
            ItemBarcode = "";
            SerialNumber = "";
            DatePeriod = "Any";
            StatusMessage = "Filters cleared";
        }

        [RelayCommand]
        private async Task ViewTransaction()
        {
            if (SelectedTransaction == null)
            {
                StatusMessage = "No transaction selected";
                return;
            }

            Console.WriteLine($"[LOOKUP] Viewing transaction: {SelectedTransaction.TransactionType} ID {SelectedTransaction.TransactionId}");
            // TODO: Open invoice viewer/printer
            StatusMessage = $"Viewing {SelectedTransaction.TransactionType} #{SelectedTransaction.TransactionId}";
        }

        [RelayCommand]
        private void RequestVoid()
        {
            if (SelectedTransaction == null)
            {
                StatusMessage = "No transaction selected";
                return;
            }

            if (LookupType == "Payment")
            {
                StatusMessage = "Void a payment's invoice instead, not the payment row directly";
                return;
            }

            if (SelectedTransaction.Status == "VOIDED")
            {
                StatusMessage = $"#{SelectedTransaction.TransactionId} is already voided";
                return;
            }

            VoidOverrideBarcode = "";
            VoidStatusMessage = "";
            IsVoidPromptOpen = true;
        }

        [RelayCommand]
        private void CancelVoid()
        {
            IsVoidPromptOpen = false;
            VoidOverrideBarcode = "";
            VoidStatusMessage = "";
        }

        [RelayCommand]
        private async Task ConfirmVoid()
        {
            if (SelectedTransaction == null)
                return;

            if (string.IsNullOrWhiteSpace(VoidOverrideBarcode))
                return;

            var staff = await _staffService.FindStaffByBarcodeAsync(VoidOverrideBarcode.Trim());
            if (staff == null)
            {
                VoidStatusMessage = $"Staff not found for '{VoidOverrideBarcode}'";
                return;
            }

            if (!staff.IsAdministrator)
            {
                VoidStatusMessage = $"{staff.DocketName} is not an administrator";
                return;
            }

            try
            {
                await VoidInvoiceAsync(SelectedTransaction.TransactionId, staff.DocketName);
                IsVoidPromptOpen = false;
                VoidOverrideBarcode = "";
                VoidStatusMessage = "";
                StatusMessage = $"#{SelectedTransaction.TransactionId} voided by {staff.DocketName}";
                await Search();
            }
            catch (Exception ex)
            {
                VoidStatusMessage = $"Error voiding: {ex.Message}";
            }
        }

        // Reverses whatever stock effect the original transaction had (a voided Sale puts
        // stock back, a voided Refund takes it back out; Quotes/Laybys never touched stock
        // so voiding them doesn't either - mirrors SaleService.CommitSaleAsync's own logic),
        // then marks the invoice VOIDED. Wrapped in one DB transaction for atomicity.
        private async Task VoidInvoiceAsync(int invoiceId, string voidedByStaffName)
        {
            using var conn = _dbService.GetConnection();
            await Task.Run(() => conn.Open());
            using var transaction = conn.BeginTransaction();

            try
            {
                string transactionType;
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = transaction;
                    cmd.CommandText = "SELECT transactiontype, status FROM invoice WHERE invoice_id = @id FOR UPDATE";
                    AddParameter(cmd, "@id", invoiceId);
                    using var reader = await Task.Run(() => cmd.ExecuteReader());
                    if (!await Task.Run(() => reader.Read()))
                        throw new InvalidOperationException($"Invoice #{invoiceId} not found");
                    transactionType = reader.GetString(0);
                    if (reader.GetString(1) == "VOIDED")
                        throw new InvalidOperationException("Already voided");
                }

                if (transactionType == "SALE" || transactionType == "REFUND")
                {
                    decimal sign = transactionType == "SALE" ? 1m : -1m;
                    using var cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;
                    // Aggregated by stock_id first - an invoice can have multiple lines for
                    // the same item (e.g. serials sold individually), and a plain UPDATE...
                    // FROM join would only apply one of them, not the sum of all.
                    cmd.CommandText = @"
                        UPDATE stock
                        SET quantityinstock = quantityinstock + (agg.total_qty * @sign),
                            date_modified = CURRENT_TIMESTAMP
                        FROM (
                            SELECT stock_id, SUM(quantity) AS total_qty
                            FROM invoice_lines
                            WHERE invoice_id = @id
                            GROUP BY stock_id
                        ) agg
                        WHERE stock.stock_id = agg.stock_id";
                    AddParameter(cmd, "@sign", sign);
                    AddParameter(cmd, "@id", invoiceId);
                    await Task.Run(() => cmd.ExecuteNonQuery());
                }

                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = transaction;
                    cmd.CommandText = @"
                        UPDATE invoice
                        SET status = 'VOIDED',
                            notes = notes || @noteSuffix,
                            date_modified = CURRENT_TIMESTAMP
                        WHERE invoice_id = @id";
                    AddParameter(cmd, "@noteSuffix", $" [VOIDED by {voidedByStaffName} on {DateTime.Now:dd-MMM-yyyy HH:mm}]");
                    AddParameter(cmd, "@id", invoiceId);
                    await Task.Run(() => cmd.ExecuteNonQuery());
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        [RelayCommand]
        private async Task PrintTransaction()
        {
            if (SelectedTransaction == null)
            {
                StatusMessage = "No transaction selected";
                return;
            }

            Console.WriteLine($"[LOOKUP] Printing: {SelectedTransaction.TransactionType} ID {SelectedTransaction.TransactionId}");
            // TODO: Implement printing
            StatusMessage = $"Printing {SelectedTransaction.TransactionType} #{SelectedTransaction.TransactionId}...";
        }

        partial void OnDatePeriodChanged(string value)
        {
            // Update date range based on selection
            DateTo = DateTime.Now;

            switch (value)
            {
                case "Today":
                    DateFrom = DateTime.Now.Date;
                    break;
                case "ThisMonth":
                    DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    break;
                case "12Months":
                    DateFrom = DateTime.Now.AddMonths(-12);
                    break;
                case "Any":
                    DateFrom = DateTime.Now.AddYears(-10);
                    break;
                // Custom uses the manually set DateFrom/DateTo
            }
        }

        private async Task<List<TransactionSummary>> SearchTransactionsAsync()
        {
            var results = new List<TransactionSummary>();

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    string sql = BuildSearchQuery();
                    cmd.CommandText = sql;

                    // Add parameters
                    if (!string.IsNullOrWhiteSpace(StaffBarcode))
                    {
                        AddParameter(cmd, "@staffBarcode", StaffBarcode);
                    }
                    if (!string.IsNullOrWhiteSpace(CustomerBarcode))
                    {
                        AddParameter(cmd, "@customerBarcode", CustomerBarcode);
                    }
                    if (!string.IsNullOrWhiteSpace(ItemBarcode))
                    {
                        AddParameter(cmd, "@itemBarcode", ItemBarcode);
                    }
                    if (!string.IsNullOrWhiteSpace(SerialNumber))
                    {
                        AddParameter(cmd, "@serialNumber", SerialNumber.Trim());
                    }
                    if (DatePeriod != "Any")
                    {
                        AddParameter(cmd, "@dateFrom", DateFrom);
                        AddParameter(cmd, "@dateTo", DateTo.AddDays(1)); // Include full day
                    }

                    Console.WriteLine($"[LOOKUP] SQL: {sql}");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new TransactionSummary
                            {
                                TransactionId = reader.GetInt32(0),
                                TransactionDate = reader.GetDateTime(1),
                                TransactionType = reader.GetString(2),
                                TotalAmount = reader.GetDecimal(3),
                                CustomerName = reader.IsDBNull(4) ? "Walk-in" : reader.GetString(4),
                                CustomerBarcode = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                StaffName = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                IsOnAccount = !reader.IsDBNull(7) && reader.GetBoolean(7),
                                InvoiceNumber = !reader.IsDBNull(8) ? reader.GetString(8) : "",
                                Status = !reader.IsDBNull(9) ? reader.GetString(9) : ""
                            });
                        }
                    }
                }
            }

            return results;
        }

        private string BuildSearchQuery()
        {
            string sql;

            if (LookupType == "Invoice")
            {
                if (!string.IsNullOrWhiteSpace(ItemBarcode) || !string.IsNullOrWhiteSpace(SerialNumber))
                {
                    // Search invoice lines for a specific item and/or serial number
                    // (serial lookup answers "who bought this / when was it sold" for warranty purposes)
                    sql = @"
                        SELECT DISTINCT inv.invoice_id, inv.invoicedate, inv.transactiontype,
                               inv.total_inc,
                               COALESCE(c.companyname, c.customername) as customer_name,
                               c.barcode as customer_barcode,
                               s.docket_name as staff_name,
                               CASE WHEN EXISTS(
                                   SELECT 1 FROM payments p
                                   WHERE p.invoice_id = inv.invoice_id
                                   AND p.paymentmethod = 'Account'
                               ) THEN true ELSE false END as isonaccount,
                               inv.invoicenumber,
                               inv.status
                        FROM invoice inv
                        INNER JOIN invoice_lines il ON inv.invoice_id = il.invoice_id
                        INNER JOIN stock st ON il.stock_id = st.stock_id
                        LEFT JOIN customer c ON inv.customer_id = c.customer_id
                        LEFT JOIN staff s ON inv.staff_id = s.staff_id
                        WHERE 1=1";

                    if (!string.IsNullOrWhiteSpace(ItemBarcode))
                        sql += " AND st.barcode = @itemBarcode";
                    if (!string.IsNullOrWhiteSpace(SerialNumber))
                        sql += " AND il.serialnumber ILIKE @serialNumber";
                }
                else
                {
                    // Search all invoices
                    sql = @"
                        SELECT inv.invoice_id, inv.invoicedate, inv.transactiontype, 
                               inv.total_inc,
                               COALESCE(c.companyname, c.customername) as customer_name,
                               c.barcode as customer_barcode,
                               s.docket_name as staff_name,
                               CASE WHEN EXISTS(
                                   SELECT 1 FROM payments p 
                                   WHERE p.invoice_id = inv.invoice_id 
                                   AND p.paymentmethod = 'Account'
                               ) THEN true ELSE false END as isonaccount,
                               inv.invoicenumber,
                               inv.status
                        FROM invoice inv
                        LEFT JOIN customer c ON inv.customer_id = c.customer_id
                        LEFT JOIN staff s ON inv.staff_id = s.staff_id
                        WHERE 1=1";
                }

                // Add filters
                if (!string.IsNullOrWhiteSpace(StaffBarcode))
                {
                    sql += " AND s.barcode = @staffBarcode";
                }
                if (!string.IsNullOrWhiteSpace(CustomerBarcode))
                {
                    sql += " AND c.barcode = @customerBarcode";
                }
                if (DatePeriod != "Any")
                {
                    sql += " AND inv.invoicedate >= @dateFrom AND inv.invoicedate < @dateTo";
                }

                sql += " ORDER BY inv.invoicedate DESC, inv.invoice_id DESC";
            }
            else if (LookupType == "Payment")
            {
                sql = @"
                    SELECT p.payment_id, p.paymentdate, p.transactiontype,
                           p.amount,
                           COALESCE(c.companyname, c.customername) as customer_name,
                           c.barcode as customer_barcode,
                           s.docket_name as staff_name,
                           false as isonaccount,
                           '' as invoicenumber,
                           '' as status
                    FROM payments p
                    LEFT JOIN customer c ON p.customer_id = c.customer_id
                    LEFT JOIN staff s ON p.staff_id = s.staff_id
                    WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(StaffBarcode))
                {
                    sql += " AND s.barcode = @staffBarcode";
                }
                if (!string.IsNullOrWhiteSpace(CustomerBarcode))
                {
                    sql += " AND c.barcode = @customerBarcode";
                }
                if (DatePeriod != "Any")
                {
                    sql += " AND p.paymentdate >= @dateFrom AND p.paymentdate < @dateTo";
                }

                sql += " ORDER BY p.paymentdate DESC, p.payment_id DESC";
            }
            else // Quote
            {
                sql = @"
                    SELECT inv.invoice_id, inv.invoicedate, inv.transactiontype,
                           inv.total_inc,
                           COALESCE(c.companyname, c.customername) as customer_name,
                           c.barcode as customer_barcode,
                           s.docket_name as staff_name,
                           false as isonaccount,
                           inv.invoicenumber,
                               inv.status
                    FROM invoice inv
                    LEFT JOIN customer c ON inv.customer_id = c.customer_id
                    LEFT JOIN staff s ON inv.staff_id = s.staff_id
                    WHERE inv.transactiontype = 'QUOTE'";

                if (!string.IsNullOrWhiteSpace(StaffBarcode))
                    sql += " AND s.barcode = @staffBarcode";
                if (!string.IsNullOrWhiteSpace(CustomerBarcode))
                    sql += " AND c.barcode = @customerBarcode";
                if (DatePeriod != "Any")
                    sql += " AND inv.invoicedate >= @dateFrom AND inv.invoicedate < @dateTo";

                sql += " ORDER BY inv.invoicedate DESC, inv.invoice_id DESC";
            }

            return sql;
        }

        private void AddParameter(System.Data.IDbCommand cmd, string name, object value)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }
    }

    // Model for transaction summary display
    public class TransactionSummary
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; } = "";
        public string CustomerBarcode { get; set; } = "";
        public string StaffName { get; set; } = "";
        public bool IsOnAccount { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public string Status { get; set; } = "";

        public string DisplayDate => TransactionDate.ToString("dd-MMM-yyyy HH:mm");
        public string DisplayAmount => TotalAmount.ToString("C");
        public string DisplayType => IsOnAccount ? $"{TransactionType} (Account)" : TransactionType;
    }
}
