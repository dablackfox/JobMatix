# JMxPOS8 - Avalonia POS Application (.NET 8)

## Overview

JMxPOS8 is the cross-platform conversion of JMxPOS620.Net, migrated from .NET Framework 3.5 + Windows Forms to .NET 8 + Avalonia UI. This enables the POS application to run natively on Linux, Windows, and macOS.

## Project Status: **Phase 1 - Foundation Complete** ✅

### Completed Components

#### Core Services (100% Complete)
- ✅ **DatabaseService** - PostgreSQL connection management with .env support
- ✅ **StockService** - Stock item queries, search, barcode lookup, quantity updates
- ✅ **CustomerService** - Customer queries, search, barcode lookup
- ✅ **StaffService** - Staff authentication and lookup by barcode
- ✅ **SaleService** - Core POS sale logic (converted from clsPOS34Sale.vb)
  - Sale line item management (add, remove, update)
  - Automatic tax calculations (GST-aware)
  - Payment tracking and change calculation
  - Multi-transaction types (Sale, Refund, Quote, Layby)
  - Full database commit with transaction support
  - Stock quantity updates
  - Customer account balance tracking

#### Data Models (100% Complete)
- ✅ **StockItem** - Complete stock model with all fields
- ✅ **Customer** - Full customer model with account features
- ✅ **Staff** - Staff/employee model
- ✅ **Invoice** - Transaction header model
- ✅ **InvoiceLine** - Transaction line items with serial support
- ✅ **Payment** - Payment tracking model
- ✅ **SaleLineItem** - In-progress sale line item

### Architecture

```
JMxPOS8/
├── Models/
│   └── POSModels.cs (all data models)
├── Services/
│   ├── DatabaseService.cs (connection factory)
│   ├── StockService.cs (stock operations)
│   ├── CustomerService.cs (customer operations)
│   ├── StaffService.cs (staff operations)
│   └── SaleService.cs (core POS logic)
├── ViewModels/ (MVVM pattern - to be created)
├── Views/ (Avalonia XAML - to be created)
└── App.axaml (Avalonia application)
```

### Key Features Implemented

1. **Database Integration**
   - Full PostgreSQL support via Npgsql
   - Lowercase column name compatibility
   - Transaction support for atomic commits
   - Proper parameter binding to prevent SQL injection

2. **Sale Processing**
   - Add items by barcode scan
   - Quantity and price adjustments
   - Automatic GST calculations (configurable rate)
   - Multiple payment types
   - Account customer support
   - Discount handling
   - Change calculation

3. **Business Rules**
   - Serial number validation for required items
   - Stock quantity tracking
   - Customer credit limit awareness
   - Staff authentication requirement
   - Transaction type handling (Sale/Refund/Quote/Layby)

### Original VB.NET Files Converted

| Original File | New C# Component | Status |
|---------------|------------------|---------|
| clsPOS34Sale.vb (8,524 lines) | Services/SaleService.cs | ✅ Core logic ported |
| clsStockBarcodeList.vb | Services/StockService.cs | ✅ Query methods ported |
| clsDebtors.vb | Services/CustomerService.cs | ✅ Account features ported |
| modPOS31Support.vb | Services/DatabaseService.cs | ✅ DB abstraction ported |

### PostgreSQL Column Mapping

All queries use lowercase column names to match PostgreSQL schema:

**Stock Table:**
- `stock_id`, `barcode`, `stockcode`, `description`, `category`
- `quantityinstock`, `costprice`, `sellprice`, `inactive`, `requiresserial`

**Customer Table:**
- `customer_id`, `barcode`, `customername`, `companyname`, `grade`
- `homephone`, `businessphone`, `mobile`, `emailaddress`
- `isaccount`, `accountbalance`, `creditlimit`, `inactive`

**Staff Table:**
- `staff_id`, `barcode`, `firstname`, `lastname`, `docket_name`
- `position`, `isadministrator`, `inactive`

**Invoice Table:**
- `invoice_id`, `customer_id`, `staff_id`, `transaction_type`
- `transaction_date`, `subtotal_ex`, `tax_amount`, `total_inc`
- `discount_amount`, `is_on_account`, `cashdrawer_id`

## Next Steps - Phase 2: UI Layer

### To Be Created

1. **Main Window (frmPosMainMdi.vb → MainWindow.axaml)**
   - Menu system (File, Sales, Stock, Customers, Reports)
   - Tab-based navigation
   - Status bar with staff/till info
   - Keyboard shortcuts (F2=Search, F5=New, etc.)

2. **Sale Window (ucPosSaleChild.vb → SaleView.axaml)**
   - Staff barcode entry
   - Customer barcode entry with lookup
   - Item barcode scanning
   - Sale items DataGrid
   - Payment panel with multiple payment types
   - Transaction type selection (Sale/Refund/Quote)
   - Commit button with validation

3. **Stock Management Views**
   - Stock list/browse
   - Stock search
   - Stock item editor
   - Barcode label printing

4. **Customer Management Views**
   - Customer list/browse
   - Customer search
   - Customer editor
   - Account history

5. **ViewModels (MVVM Pattern)**
   - MainWindowViewModel
   - SaleViewModel (wraps SaleService)
   - StockViewModel
   - CustomerViewModel

## Dependencies

```xml
<PackageReference Include="Avalonia" Version="11.3.11" />
<PackageReference Include="Avalonia.Controls.DataGrid" Version="11.3.11" />
<PackageReference Include="Npgsql" Version="10.0.1" />
```

## Database Configuration

Create `.env` file in the application directory:

```
DB_CONNECTION_STRING_POSTGRES=Host=localhost;Port=5432;Database=jobmatix_pos;Username=jobmatix_user;Password=jobmatix123;Include Error Detail=true
```

## Testing the Services

The core services can be tested independently:

```csharp
// Load environment
DatabaseService.LoadEnvironment();

// Initialize services
var db = new DatabaseService();
var stockService = new StockService(db);
var customerService = new CustomerService(db);
var staffService = new StaffService(db);
var saleService = new SaleService(db, stockService, customerService);

// Test stock lookup
var stock = await stockService.FindStockByBarcodeAsync("123456");

// Test sale processing
var staff = await staffService.FindStaffByBarcodeAsync("STAFF01");
saleService.SetStaff(staff);
await saleService.AddItemByBarcodeAsync("123456", 1);
saleService.AddPayment("CASH", 100.00m);
int invoiceId = await saleService.CommitSaleAsync();
```

## Migration Notes

### Changes from Original VB.NET

1. **Language**: VB.NET → C#
2. **UI Framework**: Windows Forms → Avalonia UI
3. **Data Access**: OleDb → Npgsql with IDbConnection
4. **Async/Await**: All database operations are async
5. **Collections**: ObservableCollection for UI binding
6. **MVVM Pattern**: Separation of business logic and UI
7. **Cross-Platform**: Runs on Linux, Windows, macOS

### Preserved Business Logic

- All GST calculations match original
- Payment handling rules unchanged
- Stock quantity updates match original behavior
- Account customer credit tracking preserved
- Serial number validation rules maintained
- Transaction types (Sale/Refund/Quote/Layby) identical

## Build and Run

```bash
cd /home/cw/Documents/JobMatix/JMxPOS8
dotnet restore
dotnet build
dotnet run
```

## Conversion Estimate

**Total Original Code**: ~127 VB.NET files, ~50,000 lines
**Completed**: ~30% (core services and models)
**Remaining**: ~70% (UI layer, dialogs, reports)

**Time Estimate for Completion**: 120-180 hours remaining
- Phase 2 (Main UI): 60-80 hours
- Phase 3 (Admin/Management): 40-60 hours  
- Phase 4 (Testing/Polish): 20-40 hours

---

**Last Updated**: January 15, 2026  
**Conversion Lead**: GitHub Copilot  
**Target Framework**: .NET 8.0  
**UI Framework**: Avalonia 11.3
