using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JMxPOS8.Services;

namespace JMxPOS8.ViewModels;

public partial class ReportsViewModel : ViewModelBase
{
    private readonly DatabaseService _dbService;
    private readonly StockService _stockService;
    private readonly CustomerService _customerService;
    private readonly StaffService _staffService;

    public ObservableCollection<ReportItem> ReportData { get; }

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    [ObservableProperty]
    private string _reportTitle = "Select a report to run";

    // Cash-up is interactive (operator enters counted amounts) rather than a read-only
    // report grid, so it gets its own panel instead of populating ReportData.
    public bool IsCashupView => ReportTitle == "Cash Up";

    partial void OnReportTitleChanged(string value)
    {
        OnPropertyChanged(nameof(IsCashupView));
    }

    [ObservableProperty]
    private string _statementCustomerBarcode = "";

    [ObservableProperty]
    private DateTimeOffset? _startDate = DateTimeOffset.Now.AddDays(-30);

    [ObservableProperty]
    private DateTimeOffset? _endDate = DateTimeOffset.Now;

    [ObservableProperty]
    private int _selectedReportIndex = 0;

    // Summary fields
    [ObservableProperty]
    private string _summary1Label = string.Empty;

    [ObservableProperty]
    private string _summary1Value = string.Empty;

    [ObservableProperty]
    private string _summary2Label = string.Empty;

    [ObservableProperty]
    private string _summary2Value = string.Empty;

    [ObservableProperty]
    private string _summary3Label = string.Empty;

    [ObservableProperty]
    private string _summary3Value = string.Empty;

    [ObservableProperty]
    private string _summary4Label = string.Empty;

    [ObservableProperty]
    private string _summary4Value = string.Empty;

    // Cash-up / EOD reconciliation
    public ObservableCollection<CashupLine> CashupLines { get; } = new();

    [ObservableProperty]
    private string _cashupTill = "A";

    [ObservableProperty]
    private string _cashupStaffBarcode = "";

    [ObservableProperty]
    private DateTime? _cashupPeriodStart;

    [ObservableProperty]
    private string _cashupComments = "";

    [ObservableProperty]
    private string _cashupStatusMessage = "";

    public decimal CashupTotalReported => CashupLines.Sum(l => l.Reported);
    public decimal CashupTotalCounted => CashupLines.Sum(l => l.Counted);
    public decimal CashupTotalVariance => CashupTotalCounted - CashupTotalReported;

    public ReportsViewModel(DatabaseService dbService, StockService stockService, CustomerService customerService, StaffService staffService)
    {
        _dbService = dbService;
        _stockService = stockService;
        _customerService = customerService;
        _staffService = staffService;
        ReportData = new ObservableCollection<ReportItem>();
    }

    [RelayCommand]
    private async Task RunDailySalesReport()
    {
        try
        {
            StatusMessage = "Running daily sales report...";
            ReportTitle = "Daily Sales Report";
            ReportData.Clear();

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 
                            DATE(i.date_created) as sale_date,
                            COUNT(DISTINCT i.invoice_id) as num_invoices,
                            SUM(i.total_inc) as total_sales,
                            AVG(i.total_inc) as avg_sale
                        FROM invoice i
                        WHERE i.date_created BETWEEN @start_date AND @end_date
                          AND i.transactiontype = 'SALE'
                        GROUP BY DATE(i.date_created)
                        ORDER BY sale_date DESC";

                    var p1 = cmd.CreateParameter();
                    p1.ParameterName = "@start_date";
                    p1.Value = (StartDate ?? DateTimeOffset.Now.AddDays(-30)).DateTime;
                    cmd.Parameters.Add(p1);

                    var p2 = cmd.CreateParameter();
                    p2.ParameterName = "@end_date";
                    p2.Value = (EndDate ?? DateTimeOffset.Now).DateTime;
                    cmd.Parameters.Add(p2);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        decimal totalSales = 0;
                        int totalInvoices = 0;

                        while (await Task.Run(() => reader.Read()))
                        {
                            var date = reader["sale_date"] != DBNull.Value ? Convert.ToDateTime(reader["sale_date"]) : DateTime.MinValue;
                            var numInvoices = reader["num_invoices"] != DBNull.Value ? Convert.ToInt32(reader["num_invoices"]) : 0;
                            var sales = reader["total_sales"] != DBNull.Value ? Convert.ToDecimal(reader["total_sales"]) : 0m;
                            var avgSale = reader["avg_sale"] != DBNull.Value ? Convert.ToDecimal(reader["avg_sale"]) : 0m;

                            totalInvoices += numInvoices;
                            totalSales += sales;

                            ReportData.Add(new ReportItem
                            {
                                Column1 = date.ToString("yyyy-MM-dd"),
                                Column2 = numInvoices.ToString(),
                                Column3 = sales.ToString("C"),
                                Column4 = avgSale.ToString("C")
                            });
                        }

                        Summary1Label = "Total Invoices:";
                        Summary1Value = totalInvoices.ToString();
                        Summary2Label = "Total Sales:";
                        Summary2Value = totalSales.ToString("C");
                        Summary3Label = "Average Sale:";
                        Summary3Value = totalInvoices > 0 ? (totalSales / totalInvoices).ToString("C") : "$0.00";
                        Summary4Label = "Days:";
                        Summary4Value = ReportData.Count.ToString();
                    }
                }
            }

            StatusMessage = $"Report complete: {ReportData.Count} records";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    [RelayCommand]
    private async Task RunStockValueReport()
    {
        try
        {
            StatusMessage = "Running stock value report...";
            ReportTitle = "Stock Value Report";
            ReportData.Clear();

            var stocks = await _stockService.GetAllStockAsync(1000);
            
            decimal totalCostValue = 0;
            decimal totalSellValue = 0;
            decimal totalQuantity = 0;
            int itemCount = 0;

            foreach (var stock in stocks.Where(s => !s.Inactive && s.QuantityInStock > 0))
            {
                var costValue = stock.QuantityInStock * stock.CostPrice;
                var sellValue = stock.QuantityInStock * stock.SellPrice;

                totalCostValue += costValue;
                totalSellValue += sellValue;
                totalQuantity += stock.QuantityInStock;
                itemCount++;

                ReportData.Add(new ReportItem
                {
                    Column1 = stock.StockCode,
                    Column2 = stock.Description,
                    Column3 = stock.QuantityInStock.ToString("N2"),
                    Column4 = costValue.ToString("C")
                });
            }

            Summary1Label = "Total Items:";
            Summary1Value = itemCount.ToString();
            Summary2Label = "Total Units:";
            Summary2Value = totalQuantity.ToString("N2");
            Summary3Label = "Cost Value:";
            Summary3Value = totalCostValue.ToString("C");
            Summary4Label = "Sell Value:";
            Summary4Value = totalSellValue.ToString("C");

            StatusMessage = $"Report complete: {itemCount} items";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    [RelayCommand]
    private async Task RunLowStockReport()
    {
        try
        {
            StatusMessage = "Running low stock report...";
            ReportTitle = "Low Stock Report";
            ReportData.Clear();

            var stocks = await _stockService.GetAllStockAsync(1000);
            
            int lowStockCount = 0;

            foreach (var stock in stocks.Where(s => !s.Inactive && s.QuantityInStock <= s.ReorderLevel && s.ReorderLevel > 0))
            {
                lowStockCount++;

                ReportData.Add(new ReportItem
                {
                    Column1 = stock.StockCode,
                    Column2 = stock.Description,
                    Column3 = stock.QuantityInStock.ToString("N2"),
                    Column4 = stock.ReorderLevel.ToString("N2")
                });
            }

            Summary1Label = "Low Stock Items:";
            Summary1Value = lowStockCount.ToString();
            Summary2Label = string.Empty;
            Summary2Value = string.Empty;
            Summary3Label = string.Empty;
            Summary3Value = string.Empty;
            Summary4Label = string.Empty;
            Summary4Value = string.Empty;

            StatusMessage = $"Report complete: {lowStockCount} items need reordering";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    [RelayCommand]
    private async Task RunCustomerAccountsReport()
    {
        try
        {
            StatusMessage = "Running customer accounts report...";
            ReportTitle = "Customer Accounts Report";
            ReportData.Clear();

            var customers = await _customerService.GetAllCustomersAsync(1000);
            
            decimal totalBalance = 0;
            int accountCount = 0;

            foreach (var customer in customers.Where(c => c.IsAccount && !c.Inactive))
            {
                accountCount++;
                totalBalance += customer.AccountBalance;

                ReportData.Add(new ReportItem
                {
                    Column1 = customer.Barcode,
                    Column2 = customer.CustomerName,
                    Column3 = customer.AccountBalance.ToString("C"),
                    Column4 = customer.CreditLimit.ToString("C")
                });
            }

            Summary1Label = "Total Accounts:";
            Summary1Value = accountCount.ToString();
            Summary2Label = "Total Balance:";
            Summary2Value = totalBalance.ToString("C");
            Summary3Label = "Avg Balance:";
            Summary3Value = accountCount > 0 ? (totalBalance / accountCount).ToString("C") : "$0.00";
            Summary4Label = string.Empty;
            Summary4Value = string.Empty;

            StatusMessage = $"Report complete: {accountCount} account customers";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    [RelayCommand]
    private async Task RunTopCustomersReport()
    {
        try
        {
            StatusMessage = "Running top customers report...";
            ReportTitle = "Top Customers by Sales";
            ReportData.Clear();

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 
                            c.customername,
                            c.barcode,
                            COUNT(i.invoice_id) as num_purchases,
                            SUM(i.total_inc) as total_spent
                        FROM customer c
                        INNER JOIN invoice i ON c.customer_id = i.customer_id
                        WHERE i.date_created BETWEEN @start_date AND @end_date
                          AND i.transactiontype = 'SALE'
                        GROUP BY c.customer_id, c.customername, c.barcode
                        ORDER BY total_spent DESC
                        LIMIT 50";

                    var p1 = cmd.CreateParameter();
                    p1.ParameterName = "@start_date";
                    p1.Value = (StartDate ?? DateTimeOffset.Now.AddDays(-30)).DateTime;
                    cmd.Parameters.Add(p1);

                    var p2 = cmd.CreateParameter();
                    p2.ParameterName = "@end_date";
                    p2.Value = (EndDate ?? DateTimeOffset.Now).DateTime.AddDays(1);
                    cmd.Parameters.Add(p2);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        decimal totalSales = 0;
                        int totalCustomers = 0;

                        while (await Task.Run(() => reader.Read()))
                        {
                            var name = reader["customername"] != DBNull.Value ? reader["customername"].ToString() ?? "" : "";
                            var barcode = reader["barcode"] != DBNull.Value ? reader["barcode"].ToString() ?? "" : "";
                            var purchases = reader["num_purchases"] != DBNull.Value ? Convert.ToInt32(reader["num_purchases"]) : 0;
                            var spent = reader["total_spent"] != DBNull.Value ? Convert.ToDecimal(reader["total_spent"]) : 0m;

                            totalCustomers++;
                            totalSales += spent;

                            ReportData.Add(new ReportItem
                            {
                                Column1 = barcode,
                                Column2 = name,
                                Column3 = purchases.ToString(),
                                Column4 = spent.ToString("C")
                            });
                        }

                        Summary1Label = "Customers:";
                        Summary1Value = totalCustomers.ToString();
                        Summary2Label = "Total Sales:";
                        Summary2Value = totalSales.ToString("C");
                        Summary3Label = string.Empty;
                        Summary3Value = string.Empty;
                        Summary4Label = string.Empty;
                        Summary4Value = string.Empty;
                    }
                }
            }

            StatusMessage = $"Report complete: {ReportData.Count} customers";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    [RelayCommand]
    private async Task RunTopProductsReport()
    {
        try
        {
            StatusMessage = "Running top products report...";
            ReportTitle = "Top Products by Sales";
            ReportData.Clear();

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 
                            s.stockcode,
                            s.description,
                            SUM(il.quantity) as total_quantity,
                            SUM(il.linetotal) as total_sales
                        FROM stock s
                        INNER JOIN invoice_lines il ON s.stock_id = il.stock_id
                        INNER JOIN invoice i ON il.invoice_id = i.invoice_id
                        WHERE i.date_created BETWEEN @start_date AND @end_date
                          AND i.transactiontype = 'SALE'
                        GROUP BY s.stock_id, s.stockcode, s.description
                        ORDER BY total_sales DESC
                        LIMIT 50";

                    var p1 = cmd.CreateParameter();
                    p1.ParameterName = "@start_date";
                    p1.Value = (StartDate ?? DateTimeOffset.Now.AddDays(-30)).DateTime;
                    cmd.Parameters.Add(p1);

                    var p2 = cmd.CreateParameter();
                    p2.ParameterName = "@end_date";
                    p2.Value = (EndDate ?? DateTimeOffset.Now).DateTime.AddDays(1);
                    cmd.Parameters.Add(p2);

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        decimal totalSales = 0;
                        decimal totalQuantity = 0;

                        while (await Task.Run(() => reader.Read()))
                        {
                            var code = reader["stockcode"] != DBNull.Value ? reader["stockcode"].ToString() ?? "" : "";
                            var description = reader["description"] != DBNull.Value ? reader["description"].ToString() ?? "" : "";
                            var quantity = reader["total_quantity"] != DBNull.Value ? Convert.ToDecimal(reader["total_quantity"]) : 0m;
                            var sales = reader["total_sales"] != DBNull.Value ? Convert.ToDecimal(reader["total_sales"]) : 0m;

                            totalQuantity += quantity;
                            totalSales += sales;

                            ReportData.Add(new ReportItem
                            {
                                Column1 = code,
                                Column2 = description,
                                Column3 = quantity.ToString("N2"),
                                Column4 = sales.ToString("C")
                            });
                        }

                        Summary1Label = "Products:";
                        Summary1Value = ReportData.Count.ToString();
                        Summary2Label = "Total Units:";
                        Summary2Value = totalQuantity.ToString("N2");
                        Summary3Label = "Total Sales:";
                        Summary3Value = totalSales.ToString("C");
                        Summary4Label = string.Empty;
                        Summary4Value = string.Empty;
                    }
                }
            }

            StatusMessage = $"Report complete: {ReportData.Count} products";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    // Lists an account customer's activity for the period alongside their live
    // accountbalance - the running "Balance" column is scoped to what's shown here, not the
    // real account balance, since accountbalance only accrues the *unpaid* portion of each
    // on-account sale (see SaleService.CommitSaleAsync) rather than gross invoice/payment
    // totals, so an independently-derived reconciliation would risk silently disagreeing
    // with the live figure. The live figure is shown as its own summary line instead.
    [RelayCommand]
    private async Task RunCustomerStatement()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(StatementCustomerBarcode))
            {
                StatusMessage = "Enter a customer barcode first";
                return;
            }

            var customer = await _customerService.FindCustomerByBarcodeAsync(StatementCustomerBarcode.Trim());
            if (customer == null)
            {
                StatusMessage = $"Customer not found for '{StatementCustomerBarcode}'";
                return;
            }

            StatusMessage = "Generating customer statement...";
            ReportTitle = "Customer Statement";
            ReportData.Clear();

            var start = (StartDate ?? DateTimeOffset.Now.AddDays(-30)).DateTime;
            var end = (EndDate ?? DateTimeOffset.Now).DateTime.AddDays(1);

            var lines = new System.Collections.Generic.List<(DateTime Date, string Label, decimal Amount)>();

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT invoicedate, invoicenumber, transactiontype, total_inc
                        FROM invoice
                        WHERE customer_id = @customerId
                          AND invoicedate BETWEEN @start AND @end
                          AND transactiontype IN ('SALE', 'REFUND')
                          AND (status IS NULL OR status <> 'VOIDED')
                        ORDER BY invoicedate";
                    AddCmdParam(cmd, "@customerId", customer.CustomerId);
                    AddCmdParam(cmd, "@start", start);
                    AddCmdParam(cmd, "@end", end);
                    using var reader = await Task.Run(() => cmd.ExecuteReader());
                    while (await Task.Run(() => reader.Read()))
                    {
                        var date = reader.GetDateTime(0);
                        var invoiceNumber = reader.GetString(1);
                        var transType = reader.GetString(2);
                        var total = reader.GetDecimal(3);
                        decimal signedAmount = transType == "REFUND" ? -total : total;
                        lines.Add((date, $"Invoice {invoiceNumber} ({transType})", signedAmount));
                    }
                }

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT paymentdate, paymentmethod, amount
                        FROM payments
                        WHERE customer_id = @customerId
                          AND paymentdate BETWEEN @start AND @end
                        ORDER BY paymentdate";
                    AddCmdParam(cmd, "@customerId", customer.CustomerId);
                    AddCmdParam(cmd, "@start", start);
                    AddCmdParam(cmd, "@end", end);
                    using var reader = await Task.Run(() => cmd.ExecuteReader());
                    while (await Task.Run(() => reader.Read()))
                    {
                        var date = reader.GetDateTime(0);
                        var method = reader.GetString(1);
                        var amount = reader.GetDecimal(2);
                        lines.Add((date, $"Payment ({method})", -amount));
                    }
                }
            }

            decimal running = 0;
            decimal totalInvoiced = 0;
            decimal totalPaid = 0;
            foreach (var line in lines.OrderBy(l => l.Date))
            {
                running += line.Amount;
                if (line.Amount > 0) totalInvoiced += line.Amount;
                else totalPaid += -line.Amount;

                ReportData.Add(new ReportItem
                {
                    Column1 = line.Date.ToString("dd-MMM-yyyy"),
                    Column2 = line.Label,
                    Column3 = line.Amount.ToString("C"),
                    Column4 = running.ToString("C")
                });
            }

            Summary1Label = "Customer:";
            Summary1Value = customer.CustomerName;
            Summary2Label = "Total Invoiced:";
            Summary2Value = totalInvoiced.ToString("C");
            Summary3Label = "Total Paid:";
            Summary3Value = totalPaid.ToString("C");
            Summary4Label = "Current Account Balance:";
            Summary4Value = customer.AccountBalance.ToString("C");

            StatusMessage = $"Statement generated: {ReportData.Count} transaction(s)";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    [RelayCommand]
    private void ExportReport()
    {
        StatusMessage = "Export feature - Coming soon (CSV/Excel export)";
        // TODO: Implement CSV export functionality
    }

    [RelayCommand]
    private void PrintReport()
    {
        StatusMessage = "Print feature - Coming soon";
        // TODO: Implement print functionality
    }

    private void ClearSummary()
    {
        Summary1Label = string.Empty;
        Summary1Value = string.Empty;
        Summary2Label = string.Empty;
        Summary2Value = string.Empty;
        Summary3Label = string.Empty;
        Summary3Value = string.Empty;
        Summary4Label = string.Empty;
        Summary4Value = string.Empty;
    }

    // Cash-up / EOD reconciliation: "reported" is what the system says was taken per
    // payment method since the last cashup for this till; the operator enters "counted"
    // (what's physically in the drawer/settled) and the difference is the variance.
    [RelayCommand]
    private async Task LoadCashup()
    {
        try
        {
            ReportTitle = "Cash Up";
            CashupStatusMessage = "Loading...";
            CashupLines.Clear();

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());

                // Start of period = the last completed cashup for this till, or start of
                // today if this till has never been cashed up before.
                DateTime periodStart;
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT MAX(session_date) FROM cashup_sessions WHERE cash_drawer = @till";
                    var p = cmd.CreateParameter();
                    p.ParameterName = "@till";
                    p.Value = CashupTill;
                    cmd.Parameters.Add(p);
                    var result = await Task.Run(() => cmd.ExecuteScalar());
                    periodStart = (result == null || result is DBNull)
                        ? DateTime.Today
                        : Convert.ToDateTime(result);
                }
                CashupPeriodStart = periodStart;

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT paymentmethod, SUM(amount) as total
                        FROM payments
                        WHERE cash_drawer = @till AND paymentdate > @periodStart
                        GROUP BY paymentmethod
                        ORDER BY paymentmethod";
                    var p1 = cmd.CreateParameter();
                    p1.ParameterName = "@till";
                    p1.Value = CashupTill;
                    cmd.Parameters.Add(p1);
                    var p2 = cmd.CreateParameter();
                    p2.ParameterName = "@periodStart";
                    p2.Value = periodStart;
                    cmd.Parameters.Add(p2);

                    using var reader = await Task.Run(() => cmd.ExecuteReader());
                    while (await Task.Run(() => reader.Read()))
                    {
                        var line = new CashupLine
                        {
                            PaymentMethod = reader.IsDBNull(0) ? "(unspecified)" : reader.GetString(0),
                            Reported = reader.GetDecimal(1)
                        };
                        line.PropertyChanged += (_, _) =>
                        {
                            OnPropertyChanged(nameof(CashupTotalCounted));
                            OnPropertyChanged(nameof(CashupTotalVariance));
                        };
                        CashupLines.Add(line);
                    }
                }
            }

            OnPropertyChanged(nameof(CashupTotalReported));
            OnPropertyChanged(nameof(CashupTotalCounted));
            OnPropertyChanged(nameof(CashupTotalVariance));

            CashupStatusMessage = CashupLines.Count > 0
                ? $"{CashupLines.Count} payment method(s) since {periodStartDisplay(CashupPeriodStart)} - enter counted amounts"
                : $"No payments recorded for till {CashupTill} since {periodStartDisplay(CashupPeriodStart)}";
        }
        catch (Exception ex)
        {
            CashupStatusMessage = $"Error: {ex.Message}";
        }

        static string periodStartDisplay(DateTime? d) => d?.ToString("dd-MMM-yyyy HH:mm") ?? "";
    }

    [RelayCommand]
    private async Task CompleteCashup()
    {
        if (CashupLines.Count == 0)
        {
            CashupStatusMessage = "Run Load Cash Up first";
            return;
        }

        if (string.IsNullOrWhiteSpace(CashupStaffBarcode))
        {
            CashupStatusMessage = "Enter the staff barcode doing the cash-up";
            return;
        }

        var staff = await _staffService.FindStaffByBarcodeAsync(CashupStaffBarcode.Trim());
        if (staff == null)
        {
            CashupStatusMessage = $"Staff not found for '{CashupStaffBarcode}'";
            return;
        }

        try
        {
            using var conn = _dbService.GetConnection();
            await Task.Run(() => conn.Open());
            using var transaction = conn.BeginTransaction();

            int sessionId;
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = transaction;
                cmd.CommandText = @"
                    INSERT INTO cashup_sessions (
                        staff_id, staff_name, session_date, cash_drawer, status,
                        stock_value, stock_variance, comments
                    ) VALUES (
                        @staffId, @staffName, @sessionDate, @till, 'CLOSED',
                        0, @variance, @comments
                    )
                    RETURNING session_id";
                AddCmdParam(cmd, "@staffId", staff.StaffId);
                AddCmdParam(cmd, "@staffName", staff.DocketName);
                // Sale-side timestamps (payments.paymentdate, invoice.invoicedate) are all
                // stamped with app-side DateTime.Now, not the DB server's clock - the two can
                // differ by hours (e.g. a UTC Postgres container vs. a local-timezone client).
                // session_date must use the same clock so the next Load Cash Up's
                // "paymentdate > periodStart" comparison doesn't re-include already-reconciled
                // payments or miss ones taken in the gap.
                AddCmdParam(cmd, "@sessionDate", DateTime.Now);
                AddCmdParam(cmd, "@till", CashupTill);
                AddCmdParam(cmd, "@variance", CashupTotalVariance);
                AddCmdParam(cmd, "@comments", CashupComments);
                sessionId = Convert.ToInt32(await Task.Run(() => cmd.ExecuteScalar()));
            }

            foreach (var line in CashupLines)
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;
                cmd.CommandText = @"
                    INSERT INTO cashup_shortages (
                        session_id, paymenttype_key, paymenttype_descr, amount_reported, amount_counted
                    ) VALUES (
                        @sessionId, @key, @descr, @reported, @counted
                    )";
                AddCmdParam(cmd, "@sessionId", sessionId);
                AddCmdParam(cmd, "@key", line.PaymentMethod);
                AddCmdParam(cmd, "@descr", line.PaymentMethod);
                AddCmdParam(cmd, "@reported", line.Reported);
                AddCmdParam(cmd, "@counted", line.Counted);
                await Task.Run(() => cmd.ExecuteNonQuery());
            }

            transaction.Commit();
            CashupStatusMessage = $"Cash-up #{sessionId} completed by {staff.DocketName} - variance {CashupTotalVariance:C}";
            CashupLines.Clear();
            CashupStaffBarcode = "";
            CashupComments = "";
            OnPropertyChanged(nameof(CashupTotalReported));
            OnPropertyChanged(nameof(CashupTotalCounted));
            OnPropertyChanged(nameof(CashupTotalVariance));
        }
        catch (Exception ex)
        {
            CashupStatusMessage = $"Error completing cash-up: {ex.Message}";
        }
    }

    private static void AddCmdParam(System.Data.IDbCommand cmd, string name, object value)
    {
        var param = cmd.CreateParameter();
        param.ParameterName = name;
        param.Value = value ?? DBNull.Value;
        cmd.Parameters.Add(param);
    }
}

// One payment-method line in a cash-up session - Counted is operator-entered, Variance is
// computed live as they type.
public partial class CashupLine : ObservableObject
{
    public string PaymentMethod { get; set; } = "";
    public decimal Reported { get; set; }

    [ObservableProperty]
    private decimal _counted;

    public decimal Variance => Counted - Reported;

    partial void OnCountedChanged(decimal value)
    {
        OnPropertyChanged(nameof(Variance));
    }
}

public class ReportItem
{
    public string Column1 { get; set; } = string.Empty;
    public string Column2 { get; set; } = string.Empty;
    public string Column3 { get; set; } = string.Empty;
    public string Column4 { get; set; } = string.Empty;
}
