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
                                IsOnAccount = LookupType == "Invoice" && !reader.IsDBNull(7) && reader.GetBoolean(7),
                                InvoiceNumber = LookupType == "Invoice" && !reader.IsDBNull(8) ? reader.GetString(8) : ""
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
                               inv.invoicenumber
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
                               inv.invoicenumber
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
                           '' as invoicenumber
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
                sql = "SELECT 0, CURRENT_TIMESTAMP, 'Quote', 0, '', '', '', false, '' WHERE 1=0"; // Placeholder - quotes not implemented yet
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

        public string DisplayDate => TransactionDate.ToString("dd-MMM-yyyy HH:mm");
        public string DisplayAmount => TotalAmount.ToString("C");
        public string DisplayType => IsOnAccount ? $"{TransactionType} (Account)" : TransactionType;
    }
}
