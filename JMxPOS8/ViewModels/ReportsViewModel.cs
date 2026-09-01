using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

    // Direct feedback, 2026-09-01: the date range and customer-barcode controls used to
    // show unconditionally above every report, doing nothing for the reports that ignore
    // them. Only show each control for the reports that actually read it (see the
    // StartDate/EndDate and StatementCustomerBarcode usages in the Run*Report methods
    // below), and only highlight whichever report button is actually active (ReportTitle
    // doubles as a unique key per report, so no separate "selected index" bookkeeping).
    public bool ShowDateRange => ReportTitle is not ("Stock Value Report" or "Low Stock Report"
        or "Customer Accounts Report" or "Cash Up" or "Select a report to run");
    public bool ShowCustomerBarcode => ReportTitle == "Customer Statement";

    public bool IsDailySalesSelected => ReportTitle == "Daily Sales Report";
    public bool IsStockValueSelected => ReportTitle == "Stock Value Report";
    public bool IsLowStockSelected => ReportTitle == "Low Stock Report";
    public bool IsCustomerAccountsSelected => ReportTitle == "Customer Accounts Report";
    public bool IsTopCustomersSelected => ReportTitle == "Top Customers by Sales";
    public bool IsTopProductsSelected => ReportTitle == "Top Products by Sales";
    public bool IsCostMarginSelected => ReportTitle == "Cost / Margin Report (serialized items with known cost)";
    public bool IsJobsSelected => ReportTitle == "Jobs Report";
    public bool IsPartsSelected => ReportTitle == "Parts Report";
    public bool IsStaffSelected => ReportTitle == "Staff Report";
    public bool IsTimesheetSelected => ReportTitle == "Timesheet Report";
    public bool IsCustomerStatementSelected => ReportTitle == "Customer Statement";
    public bool IsCashupSelected => ReportTitle == "Cash Up";

    // Descriptive per-report column headers, replacing the generic "Column 1..4" labels -
    // set alongside ReportTitle at the top of each Run*Report method.
    [ObservableProperty]
    private string _column1Header = "Column 1";

    [ObservableProperty]
    private string _column2Header = "Column 2";

    [ObservableProperty]
    private string _column3Header = "Column 3";

    [ObservableProperty]
    private string _column4Header = "Column 4";

    // Column sorting (direct feedback, 2026-09-01: "stock value report shows non descript
    // column names and arent sortable by column") - generic across every report, since
    // ReportItem's four columns are shared by all of them. 0 = unsorted.
    [ObservableProperty]
    private int _sortColumn = 0;

    [ObservableProperty]
    private bool _sortAscending = true;

    public string SortGlyph => SortAscending ? "▲" : "▼";
    public bool IsSortedByColumn1 => SortColumn == 1;
    public bool IsSortedByColumn2 => SortColumn == 2;
    public bool IsSortedByColumn3 => SortColumn == 3;
    public bool IsSortedByColumn4 => SortColumn == 4;

    partial void OnSortColumnChanged(int value)
    {
        OnPropertyChanged(nameof(IsSortedByColumn1));
        OnPropertyChanged(nameof(IsSortedByColumn2));
        OnPropertyChanged(nameof(IsSortedByColumn3));
        OnPropertyChanged(nameof(IsSortedByColumn4));
    }

    partial void OnSortAscendingChanged(bool value) => OnPropertyChanged(nameof(SortGlyph));

    [RelayCommand]
    private void SortByColumn(string columnParam)
    {
        if (!int.TryParse(columnParam, out var col) || ReportData.Count == 0)
            return;

        SortAscending = SortColumn == col ? !SortAscending : true;
        SortColumn = col;

        Func<ReportItem, string> selector = col switch
        {
            1 => r => r.Column1,
            2 => r => r.Column2,
            3 => r => r.Column3,
            4 => r => r.Column4,
            _ => r => r.Column1
        };

        var sorted = SortAscending
            ? ReportData.OrderBy(selector, ReportColumnComparer.Instance).ToList()
            : ReportData.OrderByDescending(selector, ReportColumnComparer.Instance).ToList();

        ReportData.Clear();
        foreach (var item in sorted)
            ReportData.Add(item);
    }

    // Direct feedback, 2026-09-01: "daily sails report shows from and to but there is no
    // button or trigger to load the report data" - changing the date range after a report
    // is already selected didn't re-run it (only re-clicking the same report-type button
    // did, which isn't an obvious "reload" action once it's already highlighted active).
    // Re-dispatches to whichever report is currently showing.
    [RelayCommand]
    private async Task RefreshCurrentReport()
    {
        switch (ReportTitle)
        {
            case "Daily Sales Report": await RunDailySalesReport(); break;
            case "Top Customers by Sales": await RunTopCustomersReport(); break;
            case "Top Products by Sales": await RunTopProductsReport(); break;
            case "Jobs Report": await RunJobsReport(); break;
            case "Parts Report": await RunPartsReport(); break;
            case "Staff Report": await RunStaffReport(); break;
            case "Timesheet Report": await RunTimesheetReport(); break;
            case "Customer Statement": await RunCustomerStatement(); break;
            case "Cost / Margin Report (serialized items with known cost)": await RunCostMarginReport(); break;
        }
    }

    partial void OnReportTitleChanged(string value)
    {
        SortColumn = 0;
        OnPropertyChanged(nameof(IsCashupView));
        OnPropertyChanged(nameof(ShowDateRange));
        OnPropertyChanged(nameof(ShowCustomerBarcode));
        OnPropertyChanged(nameof(IsDailySalesSelected));
        OnPropertyChanged(nameof(IsStockValueSelected));
        OnPropertyChanged(nameof(IsLowStockSelected));
        OnPropertyChanged(nameof(IsCustomerAccountsSelected));
        OnPropertyChanged(nameof(IsTopCustomersSelected));
        OnPropertyChanged(nameof(IsTopProductsSelected));
        OnPropertyChanged(nameof(IsCostMarginSelected));
        OnPropertyChanged(nameof(IsJobsSelected));
        OnPropertyChanged(nameof(IsPartsSelected));
        OnPropertyChanged(nameof(IsStaffSelected));
        OnPropertyChanged(nameof(IsTimesheetSelected));
        OnPropertyChanged(nameof(IsCustomerStatementSelected));
        OnPropertyChanged(nameof(IsCashupSelected));
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
            Column1Header = "Date"; Column2Header = "Invoices"; Column3Header = "Total Sales"; Column4Header = "Avg Sale";
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
            Column1Header = "Code"; Column2Header = "Description"; Column3Header = "Qty"; Column4Header = "Cost Value";
            ReportData.Clear();

            // The QuantityInStock > 0 filter below runs client-side, so the fetch limit
            // must cover every active stock row (~14,750), not an arbitrary page size -
            // capping at 1000 (ordered by stockcode) silently dropped most in-stock items
            // whose code sorted past the cutoff, understating this report by over 90%
            // (direct feedback, 2026-09-01: "the reports only show 106 items for stock
            // value... i assume its 0 per page or something").
            var stocks = await _stockService.GetAllStockAsync(20000);

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
            Column1Header = "Code"; Column2Header = "Description"; Column3Header = "Qty on Hand"; Column4Header = "Reorder Level";
            ReportData.Clear();

            var stocks = await _stockService.GetAllStockAsync(20000);

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
            Column1Header = "Barcode"; Column2Header = "Customer"; Column3Header = "Balance"; Column4Header = "Credit Limit";
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
            Column1Header = "Barcode"; Column2Header = "Customer"; Column3Header = "Purchases"; Column4Header = "Spent";
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
            Column1Header = "Code"; Column2Header = "Description"; Column3Header = "Qty Sold"; Column4Header = "Sales";
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

    // Job reporting (ROADMAP.md Phase 3 - "job reporting", the legacy SQL Server
    // SHAPE/scalar-function report). The real work here already lived in the legacy
    // app's own VB code, not SQL Server: gCurComputeChargeableHours/gbQueryWorkSessions
    // parsed jobs.sessiontimes in application code, and the dynamically-created
    // JT2_ChargeableHours T-SQL function just repeated that same string parsing for
    // use inside the SHAPE query. SessionTimesParser is that same parsing, ported to
    // C# and verified against 2,000 real jobs (99.95% match against the job's own
    // recorded totalservicetime). The SHAPE parent/child structure itself becomes a
    // plain query plus a JOIN (Parts report) or in-app grouping (Jobs/Staff reports) -
    // no Postgres equivalent of SHAPE is needed at all.

    private async Task<Dictionary<string, decimal>> GetLabourRatesAsync()
    {
        var rates = new Dictionary<string, decimal>();
        using var conn = _dbService.GetConnection();
        await Task.Run(() => conn.Open());
        using var cmd = conn.CreateCommand();
        cmd.CommandText = @"
            SELECT info_key, info_value FROM systeminfo
            WHERE info_key IN ('LabourHourlyRatePriority1', 'LabourHourlyRatePriority2', 'LabourHourlyRatePriority3')";
        using var reader = await Task.Run(() => cmd.ExecuteReader());
        while (await Task.Run(() => reader.Read()))
        {
            if (decimal.TryParse(reader.GetString(1), NumberStyles.Number, CultureInfo.InvariantCulture, out var rate))
                rates[reader.GetString(0)] = rate;
        }
        return rates;
    }

    // Matches the legacy CASE Priority WHEN '3' ... WHEN '2' ... ELSE (Priority-1 rate)
    // exactly - every other priority code (including this port's own 'H'/'B' and the
    // legacy 'Q') falls through to the Priority-1 rate, same as it always did.
    private static decimal LabourRateForPriority(string priority, Dictionary<string, decimal> rates)
    {
        var key = priority switch
        {
            "3" => "LabourHourlyRatePriority3",
            "2" => "LabourHourlyRatePriority2",
            _ => "LabourHourlyRatePriority1",
        };
        return rates.TryGetValue(key, out var rate) ? rate : 0m;
    }

    [RelayCommand]
    private async Task RunJobsReport()
    {
        try
        {
            StatusMessage = "Running jobs report...";
            ReportTitle = "Jobs Report";
            Column1Header = "Ticket"; Column2Header = "Customer (Status)"; Column3Header = "Hours"; Column4Header = "Charge";
            ReportData.Clear();

            var rates = await GetLabourRatesAsync();
            var start = (StartDate ?? DateTimeOffset.Now.AddDays(-30)).DateTime;
            var end = (EndDate ?? DateTimeOffset.Now).DateTime.AddDays(1);

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT job_id, priority, jobstatus, sessiontimes,
                               CASE WHEN customercompany IN ('N/A','--','') THEN customername ELSE customercompany END AS customer
                        FROM jobs
                        WHERE datecreated BETWEEN @start AND @end
                        ORDER BY job_id";
                    AddCmdParam(cmd, "@start", start);
                    AddCmdParam(cmd, "@end", end);

                    decimal totalHours = 0, totalCharge = 0;

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            var priority = reader.GetString(1);
                            var sessionTimes = reader.GetString(3);
                            var hours = SessionTimesParser.ComputeChargeableHours(sessionTimes);
                            var charge = hours * LabourRateForPriority(priority, rates);

                            totalHours += hours;
                            totalCharge += charge;

                            ReportData.Add(new ReportItem
                            {
                                Column1 = $"#{reader.GetInt32(0)}",
                                Column2 = $"{reader.GetString(4)} ({reader.GetString(2)})",
                                Column3 = hours.ToString("N2"),
                                Column4 = charge.ToString("C")
                            });
                        }
                    }

                    Summary1Label = "Jobs:";
                    Summary1Value = ReportData.Count.ToString();
                    Summary2Label = "Total Chargeable Hours:";
                    Summary2Value = totalHours.ToString("N2");
                    Summary3Label = "Total Labour Charge:";
                    Summary3Value = totalCharge.ToString("C");
                    Summary4Label = string.Empty;
                    Summary4Value = string.Empty;
                }
            }

            StatusMessage = $"Report complete: {ReportData.Count} jobs";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    [RelayCommand]
    private async Task RunPartsReport()
    {
        try
        {
            StatusMessage = "Running parts report...";
            ReportTitle = "Parts Report";
            Column1Header = "Part Code"; Column2Header = "Description"; Column3Header = "Ticket"; Column4Header = "Line Total";
            ReportData.Clear();

            var start = (StartDate ?? DateTimeOffset.Now.AddDays(-30)).DateTime;
            var end = (EndDate ?? DateTimeOffset.Now).DateTime.AddDays(1);

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT p.job_id, p.partcode, p.partdescr, p.quantity, p.sellprice, p.is_warranty_part,
                               CASE WHEN j.customercompany IN ('N/A','--','') THEN j.customername ELSE j.customercompany END AS customer
                        FROM parts p
                        JOIN jobs j ON j.job_id = p.job_id
                        WHERE j.datecreated BETWEEN @start AND @end
                        ORDER BY p.cat1, p.partdescr";
                    AddCmdParam(cmd, "@start", start);
                    AddCmdParam(cmd, "@end", end);

                    decimal totalSell = 0;
                    int warrantyCount = 0;

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            var qty = reader.GetDecimal(3);
                            var sell = reader.GetDecimal(4);
                            var isWarranty = reader.GetBoolean(5);
                            var lineTotal = qty * sell;
                            totalSell += lineTotal;
                            if (isWarranty) warrantyCount++;

                            ReportData.Add(new ReportItem
                            {
                                Column1 = reader.GetString(1),
                                Column2 = isWarranty ? $"{reader.GetString(2)} [WARRANTY]" : reader.GetString(2),
                                Column3 = $"Job #{reader.GetInt32(0)} - {reader.GetString(6)}",
                                Column4 = lineTotal.ToString("C")
                            });
                        }
                    }

                    Summary1Label = "Part Lines:";
                    Summary1Value = ReportData.Count.ToString();
                    Summary2Label = "Warranty Lines:";
                    Summary2Value = warrantyCount.ToString();
                    Summary3Label = "Total Sell Value:";
                    Summary3Value = totalSell.ToString("C");
                    Summary4Label = string.Empty;
                    Summary4Value = string.Empty;
                }
            }

            StatusMessage = $"Report complete: {ReportData.Count} part lines";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    [RelayCommand]
    private async Task RunStaffReport()
    {
        try
        {
            StatusMessage = "Running staff report...";
            ReportTitle = "Staff Report";
            Column1Header = "Staff"; Column2Header = "Ticket"; Column3Header = "Hours"; Column4Header = "Charge";
            ReportData.Clear();

            var rates = await GetLabourRatesAsync();
            var start = (StartDate ?? DateTimeOffset.Now.AddDays(-30)).DateTime;
            var end = (EndDate ?? DateTimeOffset.Now).DateTime.AddDays(1);

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT job_id, priority, jobstatus, sessiontimes,
                               CASE WHEN nominatedtech IN ('N/A','') THEN techstaffname ELSE nominatedtech END AS staffname,
                               CASE WHEN customercompany IN ('N/A','--','') THEN customername ELSE customercompany END AS customer
                        FROM jobs
                        WHERE datecreated BETWEEN @start AND @end
                        ORDER BY staffname, job_id";
                    AddCmdParam(cmd, "@start", start);
                    AddCmdParam(cmd, "@end", end);

                    decimal totalHours = 0, totalCharge = 0;
                    var staffSeen = new HashSet<string>();

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            var priority = reader.GetString(1);
                            var sessionTimes = reader.GetString(3);
                            var staffName = reader.GetString(4);
                            var hours = SessionTimesParser.ComputeChargeableHours(sessionTimes);
                            var charge = hours * LabourRateForPriority(priority, rates);

                            totalHours += hours;
                            totalCharge += charge;
                            staffSeen.Add(staffName);

                            ReportData.Add(new ReportItem
                            {
                                Column1 = staffName,
                                Column2 = $"Job #{reader.GetInt32(0)} - {reader.GetString(5)}",
                                Column3 = hours.ToString("N2"),
                                Column4 = charge.ToString("C")
                            });
                        }
                    }

                    Summary1Label = "Staff:";
                    Summary1Value = staffSeen.Count.ToString();
                    Summary2Label = "Jobs:";
                    Summary2Value = ReportData.Count.ToString();
                    Summary3Label = "Total Hours:";
                    Summary3Value = totalHours.ToString("N2");
                    Summary4Label = "Total Labour Charge:";
                    Summary4Value = totalCharge.ToString("C");
                }
            }

            StatusMessage = $"Report complete: {ReportData.Count} jobs";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            ClearSummary();
        }
    }

    [RelayCommand]
    private async Task RunTimesheetReport()
    {
        try
        {
            StatusMessage = "Running timesheet report...";
            ReportTitle = "Timesheet Report";
            Column1Header = "Staff"; Column2Header = "Date (Ticket)"; Column3Header = "Hours"; Column4Header = "Cost";
            ReportData.Clear();

            var rates = await GetLabourRatesAsync();
            var start = (StartDate ?? DateTimeOffset.Now.AddDays(-30)).DateTime.Date;
            var end = (EndDate ?? DateTimeOffset.Now).DateTime.Date;

            using (var conn = _dbService.GetConnection())
            {
                await Task.Run(() => conn.Open());
                using (var cmd = conn.CreateCommand())
                {
                    // Matches gbQueryWorkSessions' own pre-filter (job-level DateUpdated >=
                    // start, no upper bound) - the real per-session date filter (which is
                    // what actually determines inclusion) happens below, in C#, against each
                    // parsed session's own date.
                    cmd.CommandText = "SELECT job_id, priority, sessiontimes FROM jobs WHERE dateupdated >= @start";
                    AddCmdParam(cmd, "@start", start);

                    var sessionRows = new List<(int JobId, string StaffName, DateTime Date, decimal Hours, decimal Cost, bool Chargeable)>();

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            var jobId = reader.GetInt32(0);
                            var priority = reader.GetString(1);
                            var sessionTimes = reader.GetString(2);
                            var rate = LabourRateForPriority(priority, rates);

                            foreach (var entry in SessionTimesParser.Parse(sessionTimes))
                            {
                                if (entry.Date == null || entry.Date < start || entry.Date > end)
                                    continue;

                                if (entry.HoursChargeable > 0)
                                    sessionRows.Add((jobId, entry.StaffName, entry.Date.Value, entry.HoursChargeable, entry.HoursChargeable * rate, true));
                                if (entry.HoursNonChargeable > 0)
                                    sessionRows.Add((jobId, entry.StaffName, entry.Date.Value, entry.HoursNonChargeable, 0m, false));
                            }
                        }
                    }

                    decimal totalChargeable = 0, totalNc = 0, totalCost = 0;
                    foreach (var row in sessionRows.OrderBy(r => r.StaffName).ThenBy(r => r.Date))
                    {
                        if (row.Chargeable)
                        {
                            totalChargeable += row.Hours;
                            totalCost += row.Cost;
                        }
                        else
                        {
                            totalNc += row.Hours;
                        }

                        ReportData.Add(new ReportItem
                        {
                            Column1 = row.StaffName,
                            Column2 = $"{row.Date:dd-MMM-yyyy} (Job #{row.JobId})",
                            Column3 = row.Chargeable ? row.Hours.ToString("N2") : $"{row.Hours:N2} (NC)",
                            Column4 = row.Cost.ToString("C")
                        });
                    }

                    Summary1Label = "Chargeable Hours:";
                    Summary1Value = totalChargeable.ToString("N2");
                    Summary2Label = "Non-Chargeable Hours:";
                    Summary2Value = totalNc.ToString("N2");
                    Summary3Label = "Total Labour Cost:";
                    Summary3Value = totalCost.ToString("C");
                    Summary4Label = "Sessions:";
                    Summary4Value = sessionRows.Count.ToString();
                }
            }

            StatusMessage = $"Report complete: {ReportData.Count} sessions";
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
            // Set title/headers first (not after the barcode check below) so clicking this
            // button always reveals the customer-barcode field, even the first time before
            // any barcode has been entered - otherwise the field, which only shows for this
            // report, would never appear for someone who hasn't run it yet.
            ReportTitle = "Customer Statement";
            Column1Header = "Date"; Column2Header = "Description"; Column3Header = "Amount"; Column4Header = "Balance";

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

    // Phase 6.1 (ROADMAP.md): real per-unit COGS/margin, using invoice_lines.cost_ex (the
    // specific serial's actual landed cost, stamped at receiving time) rather than
    // stock.costprice's "latest cost wins" value. Only lines with a resolved cost
    // (cost_ex > 0) are counted - a serial sold before Phase 6.1 shipped, or one never
    // received through the new Goods Received flow, has no per-unit cost lineage yet.
    [RelayCommand]
    private async Task RunCostMarginReport()
    {
        try
        {
            StatusMessage = "Running cost/margin report...";
            ReportTitle = "Cost / Margin Report (serialized items with known cost)";
            Column1Header = "Code"; Column2Header = "Description (units sold)"; Column3Header = "Cost"; Column4Header = "Profit";
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
                            COUNT(*) as units_sold,
                            SUM(il.cost_ex) as total_cost,
                            SUM(il.sell_ex) as total_revenue,
                            SUM(il.gross_profit) as total_profit
                        FROM invoice_lines il
                        JOIN invoice i ON i.invoice_id = il.invoice_id
                        JOIN stock s ON s.stock_id = il.stock_id
                        WHERE i.date_created BETWEEN @start_date AND @end_date
                          AND i.transactiontype = 'SALE'
                          AND il.cost_ex > 0
                        GROUP BY s.stock_id, s.stockcode, s.description
                        ORDER BY total_profit DESC
                        LIMIT 50";

                    AddCmdParam(cmd, "@start_date", (StartDate ?? DateTimeOffset.Now.AddDays(-30)).DateTime);
                    AddCmdParam(cmd, "@end_date", (EndDate ?? DateTimeOffset.Now).DateTime.AddDays(1));

                    decimal totalCost = 0, totalRevenue = 0, totalProfit = 0;
                    int totalUnits = 0;

                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            var units = Convert.ToInt32(reader["units_sold"]);
                            var cost = Convert.ToDecimal(reader["total_cost"]);
                            var revenue = Convert.ToDecimal(reader["total_revenue"]);
                            var profit = Convert.ToDecimal(reader["total_profit"]);

                            totalUnits += units;
                            totalCost += cost;
                            totalRevenue += revenue;
                            totalProfit += profit;

                            ReportData.Add(new ReportItem
                            {
                                Column1 = reader["stockcode"]?.ToString() ?? "",
                                Column2 = $"{reader["description"]} ({units} sold)",
                                Column3 = cost.ToString("C"),
                                Column4 = profit.ToString("C")
                            });
                        }
                    }

                    Summary1Label = "Units Sold:";
                    Summary1Value = totalUnits.ToString();
                    Summary2Label = "Total Cost:";
                    Summary2Value = totalCost.ToString("C");
                    Summary3Label = "Total Profit:";
                    Summary3Value = totalProfit.ToString("C");
                    Summary4Label = "Margin %:";
                    Summary4Value = totalRevenue > 0 ? (totalProfit / totalRevenue).ToString("P1") : "n/a";
                }
            }

            StatusMessage = $"Report complete: {ReportData.Count} product(s) with known unit cost";
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

// Report columns are pre-formatted display strings ("$1,234.56", "12-Sep-2026", "#32363"),
// not raw values, so a plain string sort would put "$100" before "$20". Parses each side as
// a number or date first (stripping currency/thousands formatting) and only falls back to
// text comparison for genuinely text columns (descriptions, statuses).
public class ReportColumnComparer : IComparer<string>
{
    public static readonly ReportColumnComparer Instance = new();

    public int Compare(string? x, string? y)
    {
        x ??= ""; y ??= "";
        var xNumeric = x.Replace("$", "").Replace(",", "").Trim();
        var yNumeric = y.Replace("$", "").Replace(",", "").Trim();
        if (decimal.TryParse(xNumeric, NumberStyles.Any, CultureInfo.InvariantCulture, out var xNum) &&
            decimal.TryParse(yNumeric, NumberStyles.Any, CultureInfo.InvariantCulture, out var yNum))
            return xNum.CompareTo(yNum);

        if (DateTime.TryParse(x, CultureInfo.InvariantCulture, DateTimeStyles.None, out var xDate) &&
            DateTime.TryParse(y, CultureInfo.InvariantCulture, DateTimeStyles.None, out var yDate))
            return xDate.CompareTo(yDate);

        return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
    }
}
