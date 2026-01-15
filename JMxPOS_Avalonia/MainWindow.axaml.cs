using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JMxPOS_Avalonia;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DatabaseHelper.Initialize();
        UpdateStatus("Application started - Ready");
    }

    private void UpdateStatus(string message)
    {
        var statusText = this.FindControl<TextBlock>("StatusText");
        if (statusText != null)
        {
            statusText.Text = $"{DateTime.Now:HH:mm:ss} - {message}";
        }
    }

    private void AppendOutput(string message)
    {
        var output = this.FindControl<TextBox>("OutputTextBox");
        if (output != null)
        {
            output.Text += message + Environment.NewLine;
        }
    }

    private async void OnTestConnection(object? sender, RoutedEventArgs e)
    {
        UpdateStatus("Testing connection...");
        AppendOutput("=====================================");
        AppendOutput("Testing PostgreSQL Connection");
        AppendOutput("=====================================");

        try
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                await Task.Run(() => conn.Open());
                AppendOutput($"✓ Connected successfully!");
                AppendOutput($"  Database: {conn.Database}");

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT info_key, info_value FROM systeminfo ORDER BY info_key";
                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        AppendOutput("");
                        AppendOutput("System Information:");
                        while (await Task.Run(() => reader.Read()))
                        {
                            AppendOutput($"  {reader["info_key"]} = {reader["info_value"]}");
                        }
                    }
                }

                UpdateStatus("Connection test successful!");
            }
        }
        catch (Exception ex)
        {
            AppendOutput($"✗ Error: {ex.Message}");
            UpdateStatus("Connection test failed!");
        }
    }

    private async void OnViewStock(object? sender, RoutedEventArgs e)
    {
        UpdateStatus("Loading stock...");
        AppendOutput("Loading stock items...");

        try
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                await Task.Run(() => conn.Open());

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT stock_id, stockcode, description, quantityinstock, sellprice, costprice FROM stock ORDER BY stockcode LIMIT 50";
                    
                    var items = new List<StockItem>();
                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new StockItem
                            {
                                StockId = Convert.ToInt32(reader["stock_id"]),
                                StockCode = reader["stockcode"].ToString() ?? "",
                                Description = reader["description"].ToString() ?? "",
                                Quantity = Convert.ToDecimal(reader["quantityinstock"]),
                                SellPrice = Convert.ToDecimal(reader["sellprice"]),
                                CostPrice = Convert.ToDecimal(reader["costprice"])
                            });
                        }
                    }

                    var stockGrid = this.FindControl<DataGrid>("StockDataGrid");
                    if (stockGrid != null)
                    {
                        stockGrid.ItemsSource = items;
                    }

                    AppendOutput($"✓ Loaded {items.Count} stock items");
                    UpdateStatus($"Stock loaded: {items.Count} items");
                }
            }
        }
        catch (Exception ex)
        {
            AppendOutput($"✗ Error loading stock: {ex.Message}");
            UpdateStatus("Failed to load stock!");
        }
    }

    private async void OnViewCustomers(object? sender, RoutedEventArgs e)
    {
        UpdateStatus("Loading customers...");
        AppendOutput("Loading customers...");

        try
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                await Task.Run(() => conn.Open());

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT customer_id, barcode, customername, homephone, emailaddress, accountbalance FROM customer ORDER BY customername LIMIT 50";
                    
                    var items = new List<CustomerItem>();
                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new CustomerItem
                            {
                                CustomerId = Convert.ToInt32(reader["customer_id"]),
                                CustomerCode = reader["barcode"].ToString() ?? "",
                                CustomerName = reader["customername"].ToString() ?? "",
                                Phone = reader["homephone"].ToString() ?? "",
                                Email = reader["emailaddress"].ToString() ?? "",
                                Balance = Convert.ToDecimal(reader["accountbalance"])
                            });
                        }
                    }

                    var customerGrid = this.FindControl<DataGrid>("CustomerDataGrid");
                    if (customerGrid != null)
                    {
                        customerGrid.ItemsSource = items;
                    }

                    AppendOutput($"✓ Loaded {items.Count} customers");
                    UpdateStatus($"Customers loaded: {items.Count}");
                }
            }
        }
        catch (Exception ex)
        {
            AppendOutput($"✗ Error loading customers: {ex.Message}");
            UpdateStatus("Failed to load customers!");
        }
    }

    private async void OnSearchStock(object? sender, RoutedEventArgs e)
    {
        var searchBox = this.FindControl<TextBox>("SearchBox");
        var searchTerm = searchBox?.Text ?? "";

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            OnViewStock(sender, e);
            return;
        }

        UpdateStatus($"Searching for: {searchTerm}");

        try
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                await Task.Run(() => conn.Open());

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT stock_id, stockcode, description, quantityinstock, sellprice, costprice FROM stock WHERE LOWER(stockcode) LIKE LOWER(@search) OR LOWER(description) LIKE LOWER(@search) ORDER BY stockcode LIMIT 50";
                    var param = cmd.CreateParameter();
                    param.ParameterName = "@search";
                    param.Value = $"%{searchTerm}%";
                    cmd.Parameters.Add(param);
                    
                    var items = new List<StockItem>();
                    using (var reader = await Task.Run(() => cmd.ExecuteReader()))
                    {
                        while (await Task.Run(() => reader.Read()))
                        {
                            items.Add(new StockItem
                            {
                                StockId = Convert.ToInt32(reader["stock_id"]),
                                StockCode = reader["stockcode"].ToString() ?? "",
                                Description = reader["description"].ToString() ?? "",
                                Quantity = Convert.ToDecimal(reader["quantityinstock"]),
                                SellPrice = Convert.ToDecimal(reader["sellprice"]),
                                CostPrice = Convert.ToDecimal(reader["costprice"])
                            });
                        }
                    }

                    var stockGrid = this.FindControl<DataGrid>("StockDataGrid");
                    if (stockGrid != null)
                    {
                        stockGrid.ItemsSource = items;
                    }

                    UpdateStatus($"Search complete: {items.Count} items found");
                }
            }
        }
        catch (Exception ex)
        {
            AppendOutput($"✗ Error: {ex.Message}");
            UpdateStatus("Search failed!");
        }
    }

    private void OnNewSale(object? sender, RoutedEventArgs e)
    {
        AppendOutput("New Sale - Feature coming soon");
        UpdateStatus("New Sale clicked");
    }

    private void OnTodaysSales(object? sender, RoutedEventArgs e)
    {
        AppendOutput("Today's Sales - Feature coming soon");
        UpdateStatus("Today's Sales clicked");
    }

    private void OnClearOutput(object? sender, RoutedEventArgs e)
    {
        var output = this.FindControl<TextBox>("OutputTextBox");
        if (output != null)
        {
            output.Text = "Output cleared.\n";
        }
        UpdateStatus("Output cleared");
    }

    private void OnAbout(object? sender, RoutedEventArgs e)
    {
        AppendOutput("=====================================");
        AppendOutput("JobMatix POS - Avalonia Demo");
        AppendOutput("Framework: Avalonia UI 11.3 / .NET 8");
        AppendOutput("Database: PostgreSQL 15");
        AppendOutput("");
        AppendOutput("Cross-platform POS running on Linux!");
        UpdateStatus("About displayed");
    }

    private void OnExit(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}

public class StockItem
{
    public int StockId { get; set; }
    public string StockCode { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Quantity { get; set; }
    public decimal SellPrice { get; set; }
    public decimal CostPrice { get; set; }
}

public class CustomerItem
{
    public int CustomerId { get; set; }
    public string CustomerCode { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    public decimal Balance { get; set; }
}
