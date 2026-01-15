# Reports Feature Documentation

## Overview
The Reports module provides comprehensive reporting capabilities for sales, inventory, and customer analysis in the JMxPOS8 application.

## Components

### ReportsViewModel
**Location:** `ViewModels/ReportsViewModel.cs`

The ReportsViewModel handles all reporting functionality with the following features:

#### Properties
- **ReportData** (ObservableCollection<ReportItem>): Collection of report rows
- **StartDate** / **EndDate**: Date range selectors for time-based reports
- **ReportTitle**: Dynamic title showing current report type
- **StatusMessage**: User feedback messages
- **Summary1-4 Label/Value**: Four configurable summary fields for totals and statistics

#### Available Reports

##### 1. Daily Sales Report
- **Command:** `RunDailySalesReportCommand`
- **Description:** Shows sales broken down by day with invoice counts and totals
- **Columns:**
  - Date
  - Number of Invoices
  - Total Sales
  - Average Sale
- **Summary:** Total invoices, total sales, average sale, number of days

##### 2. Stock Value Report
- **Command:** `RunStockValueReportCommand`
- **Description:** Shows inventory value based on current stock levels
- **Columns:**
  - Stock Code
  - Description
  - Quantity in Stock
  - Cost Value
- **Summary:** Total items, total units, cost value, sell value

##### 3. Low Stock Report
- **Command:** `RunLowStockReportCommand`
- **Description:** Lists items that have fallen below reorder level
- **Columns:**
  - Stock Code
  - Description
  - Current Quantity
  - Reorder Level
- **Summary:** Count of low stock items

##### 4. Customer Accounts Report
- **Command:** `RunCustomerAccountsReportCommand`
- **Description:** Shows all customer accounts with balances and credit limits
- **Columns:**
  - Customer Barcode
  - Customer Name
  - Account Balance
  - Credit Limit
- **Summary:** Total accounts, total balance, average balance

##### 5. Top Customers Report
- **Command:** `RunTopCustomersReportCommand`
- **Description:** Ranks customers by sales volume within date range (top 50)
- **Columns:**
  - Customer Barcode
  - Customer Name
  - Number of Purchases
  - Total Spent
- **Summary:** Number of customers, total sales

##### 6. Top Products Report
- **Command:** `RunTopProductsReportCommand`
- **Description:** Ranks products by sales volume within date range (top 50)
- **Columns:**
  - Stock Code
  - Description
  - Total Quantity Sold
  - Total Sales Value
- **Summary:** Number of products, total units sold, total sales

### User Interface
**Location:** `Views/MainWindow.axaml` (Reports Tab)

The UI is organized into five sections:

1. **Report Selection Bar**
   - Six buttons for different report types
   - Date range pickers (From/To dates)

2. **Report Title**
   - Displays current report name

3. **Data Grid**
   - Four flexible columns that adapt to each report type
   - Read-only display with grid lines
   - Scrollable for large datasets

4. **Summary Panel**
   - Four summary statistics with labels and values
   - Highlighted background for visibility

5. **Action Bar**
   - Export to CSV button (placeholder)
   - Print button (placeholder)
   - Status message display

### Integration
**Location:** `ViewModels/MainWindowViewModel.cs`

The ReportsViewModel is integrated into the main application:

```csharp
public ReportsViewModel ReportsViewModel { get; }

// In constructor:
ReportsViewModel = new ReportsViewModel(_dbService, _stockService, _customerService);

// Menu command:
[RelayCommand]
private void Reports()
{
    StatusText = "Select a report to run...";
    SelectedTabIndex = 3; // Switch to Reports tab
}
```

## Usage

### Running a Report

1. Click the "Reports" menu item or button
2. Select a report type by clicking one of the six report buttons
3. For time-based reports (Daily Sales, Top Customers, Top Products):
   - Adjust the "From" and "To" dates as needed
   - Default range is last 30 days
4. View results in the data grid
5. Check summary statistics at the bottom

### Date Range Selection

Time-based reports respect the selected date range:
- Daily Sales Report: Groups sales by date within range
- Top Customers Report: Aggregates customer purchases within range
- Top Products Report: Aggregates product sales within range

Stock and customer reports use current data regardless of date range.

## Database Queries

### Daily Sales Report
```sql
SELECT 
    DATE(i.date_created) as sale_date,
    COUNT(DISTINCT i.invoice_id) as num_invoices,
    SUM(i.total_inc) as total_sales,
    AVG(i.total_inc) as avg_sale
FROM invoice i
WHERE i.date_created BETWEEN @start_date AND @end_date
  AND i.invoice_type = 'SALE'
GROUP BY DATE(i.date_created)
ORDER BY sale_date DESC
```

### Top Customers Report
```sql
SELECT 
    c.customername,
    c.barcode,
    COUNT(i.invoice_id) as num_purchases,
    SUM(i.total_inc) as total_spent
FROM customer c
INNER JOIN invoice i ON c.customer_id = i.customer_id
WHERE i.date_created BETWEEN @start_date AND @end_date
  AND i.invoice_type = 'SALE'
GROUP BY c.customer_id, c.customername, c.barcode
ORDER BY total_spent DESC
LIMIT 50
```

### Top Products Report
```sql
SELECT 
    s.stockcode,
    s.description,
    SUM(il.quantity) as total_quantity,
    SUM(il.linetotal) as total_sales
FROM stock s
INNER JOIN invoiceline il ON s.stock_id = il.stock_id
INNER JOIN invoice i ON il.invoice_id = i.invoice_id
WHERE i.date_created BETWEEN @start_date AND @end_date
  AND i.invoice_type = 'SALE'
GROUP BY s.stock_id, s.stockcode, s.description
ORDER BY total_sales DESC
LIMIT 50
```

## Key Features

1. **Dynamic Column Headers**: Data grid columns adapt to each report type
2. **Real-time Calculations**: Summary statistics computed as data loads
3. **Date Range Flexibility**: Easy date selection for time-based analysis
4. **Async Operations**: All queries run asynchronously to keep UI responsive
5. **Error Handling**: Graceful error messages if queries fail
6. **Large Dataset Support**: Top reports limited to 50 records for performance

## Future Enhancements

### Planned Features
1. **CSV Export**: Export report data to CSV file for Excel analysis
2. **Print Functionality**: Print reports directly from application
3. **PDF Export**: Generate PDF reports with formatting
4. **Chart Visualization**: Add charts/graphs for visual analysis
5. **Scheduled Reports**: Automatically generate reports at specified times
6. **Custom Date Ranges**: Quick selection for "This Week", "This Month", "Last Quarter"
7. **Report Templates**: Save custom report configurations
8. **Email Reports**: Send reports automatically via email
9. **More Report Types**:
   - Staff performance reports
   - Profit margin analysis
   - Stock movement history
   - Customer payment history
   - Tax reports (GST/VAT)

### Extension Points

To add a new report:

1. Add a new `RelayCommand` method in ReportsViewModel:
```csharp
[RelayCommand]
private async Task RunMyNewReport()
{
    // Set title and clear data
    ReportTitle = "My New Report";
    ReportData.Clear();
    
    // Query database
    // Populate ReportData
    // Set summary values
}
```

2. Add a button in MainWindow.axaml:
```xml
<Button Content="My New Report" 
        Command="{Binding ReportsViewModel.RunMyNewReportCommand}" 
        Width="120"/>
```

## Testing

To test the reports feature:

1. Ensure test data exists in the database (sales, stock, customers)
2. Run the application and navigate to the Reports tab
3. Test each report type with various date ranges
4. Verify summary calculations are correct
5. Test with empty result sets (no data in date range)
6. Test with large datasets (>100 records)

## Performance Considerations

- Top reports limited to 50 records to prevent UI slowdown
- Stock reports load all items but can be optimized with filtering
- Database queries use proper indexes on date_created and foreign keys
- Async/await pattern prevents UI blocking during data loading

## Dependencies

- **DatabaseService**: Database connection management
- **StockService**: Stock data access
- **CustomerService**: Customer data access
- **CommunityToolkit.Mvvm**: MVVM framework for commands and properties
