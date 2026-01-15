# Stock Management Feature

## Overview
The Stock Management feature provides full CRUD (Create, Read, Update, Delete) operations for managing inventory stock items in the JobMatix POS system.

## Components

### 1. StockViewModel.cs
Located in `ViewModels/StockViewModel.cs`, this ViewModel manages all stock-related operations:

**Properties:**
- `StockItems` - ObservableCollection of all stock items
- `SelectedStock` - Currently selected stock item in the grid
- `SearchText` - Search query for filtering stock
- `StatusMessage` - Status/feedback message for user
- Stock detail fields (Barcode, StockCode, Description, Category, Pricing, Inventory levels, etc.)
- `IsEditing` - Flag to show/hide the detail form

**Commands:**
- `LoadStockCommand` - Load all stock items from database
- `SearchStockCommand` - Search stock by code, barcode, or description
- `NewStockCommand` - Start creating a new stock item
- `EditStockCommand` - Load selected stock item for editing
- `SaveStockCommand` - Save new or updated stock item
- `CancelEditCommand` - Cancel editing and clear form
- `DeleteStockCommand` - Soft delete stock item (sets inactive flag)
- `RefreshCommand` - Clear search and reload all stock
- `AdjustQuantityCommand` - Quick access to adjust stock quantity

**Key Features:**
- Real-time search as you type (via OnSearchTextChanged)
- Form validation (requires Barcode, StockCode, and Description)
- Automatic refresh after save/delete operations
- Comprehensive stock data capture (14 fields total)
- Stock quantity tracking with reorder levels
- Serial number tracking support

### 2. StockService.cs Updates
Added CRUD methods to `Services/StockService.cs`:

**New Methods:**
- `AddStockAsync(StockItem stock)` - Insert new stock item, returns stock_id
- `UpdateStockAsync(StockItem stock)` - Update existing stock item by stock_id
- `DeleteStockAsync(int stockId)` - Soft delete (sets inactive = true)
- `AddStockParameters(IDbCommand cmd, StockItem stock)` - Helper method for parameterized queries

**Security:**
- All methods use parameterized queries to prevent SQL injection
- Soft delete preserves data integrity (doesn't physically remove records)

### 3. Stock Model Updates
Extended `Models/POSModels.cs` StockItem class with additional fields:

**Added Properties:**
- `ReorderLevel` - Minimum quantity before reorder alert
- `ReorderQuantity` - Suggested quantity to reorder
- `Supplier` - Primary supplier name
- `Location` - Physical location/bin in warehouse
- `Notes` - General notes field

### 4. UI Implementation
Updated `Views/MainWindow.axaml` Stock tab with comprehensive UI:

**Layout:**
- **Left Panel (40%)**: DataGrid showing stock list with columns:
  - Barcode
  - Stock Code
  - Description
  - Quantity
  - Sell Price
- **Right Panel (60%)**: Detailed form for add/edit operations (only visible when IsEditing = true)
- **Top Bar**: Search box and action buttons (New, Edit, Delete, Adjust Qty, Refresh)
- **Bottom**: Status message display

**Form Sections:**
1. Basic Info (Barcode, Stock Code, Description, Category, Inactive flag)
2. Pricing (Cost Price, Sell Price)
3. Inventory (Quantity, Reorder Level, Reorder Qty, Location, Requires Serial flag)
4. Supplier Information
5. Notes (multi-line text area)
6. Action Buttons (Save, Cancel)

### 5. MainWindowViewModel Integration
Updated `ViewModels/MainWindowViewModel.cs`:
- Added `StockViewModel` property
- Initialized StockViewModel in constructor
- Updated StockList command to load stock when switching to Stock tab

## Usage

### Viewing Stock
1. Click "Stock" menu item or tab
2. Stock list loads automatically
3. Use search box to filter by code, barcode, or description

### Adding a Stock Item
1. Click "New" button
2. Fill in required fields (Barcode*, Stock Code*, Description*)
3. Fill in optional pricing, inventory, and supplier information
4. Click "Save" to commit

### Editing a Stock Item
1. Select a stock item from the list
2. Click "Edit" button
3. Modify fields as needed
4. Click "Save" to commit or "Cancel" to discard changes

### Adjusting Stock Quantity
1. Select a stock item from the list
2. Click "Adjust Qty" button
3. Modify the quantity field
4. Click "Save"

### Deleting a Stock Item
1. Select a stock item from the list
2. Click "Delete" button
3. Stock item is marked as inactive (soft delete)

### Searching Stock
1. Type in search box at top of Stock tab
2. Search is performed automatically as you type
3. Searches across stock code, barcode, and description
4. Click "Refresh" to clear search and show all stock

## Database Schema
Uses the PostgreSQL `stock` table with these columns:
- stock_id (primary key)
- barcode (unique, indexed)
- stockcode (indexed)
- description
- category
- quantityinstock
- costprice
- sellprice
- inactive
- requiresserial
- reorderlevel
- reorderquantity
- supplier
- location
- notes
- date_created, date_modified (timestamps)

## Key Features

### Inventory Tracking
- Real-time quantity tracking
- Reorder level alerts (visual indicators when stock is low)
- Location/bin tracking for warehouse management
- Serial number requirement flag for tracked items

### Pricing Management
- Separate cost and sell prices
- Profit margin calculation (can be added)
- Price history tracking (future enhancement)

### Search & Filter
- Multi-field search (barcode, stock code, description)
- Instant results as you type
- Case-insensitive matching

### Data Integrity
- Soft delete preserves transaction history
- Inactive items hidden from normal views but retained in database
- Foreign key relationships maintained

## Future Enhancements
- Confirmation dialog before delete
- Stock photos/images
- Multiple barcodes per item
- Supplier management integration
- Purchase order integration
- Stock movement history
- Low stock alerts/notifications
- Bulk import/export (CSV, Excel)
- Stock take/audit functionality
- Barcode label printing
- Price change history
- Stock valuation reports
- Category management
- Multi-location support
- Serial number tracking grid
