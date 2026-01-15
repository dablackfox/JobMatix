# Customer Management Feature

## Overview
The Customer Management feature provides full CRUD (Create, Read, Update, Delete) operations for managing customer records in the JobMatix POS system.

## Components

### 1. CustomerViewModel.cs
Located in `ViewModels/CustomerViewModel.cs`, this ViewModel manages all customer-related operations:

**Properties:**
- `Customers` - ObservableCollection of all customers
- `SelectedCustomer` - Currently selected customer in the grid
- `SearchText` - Search query for filtering customers
- `StatusMessage` - Status/feedback message for user
- Customer detail fields (Barcode, CustomerName, CompanyName, ContactName, Address, Phone numbers, Email, Account info, etc.)
- `IsEditing` - Flag to show/hide the detail form

**Commands:**
- `LoadCustomersCommand` - Load all customers from database
- `SearchCustomersCommand` - Search customers by name, company, or barcode
- `NewCustomerCommand` - Start creating a new customer
- `EditCustomerCommand` - Load selected customer for editing
- `SaveCustomerCommand` - Save new or updated customer
- `CancelEditCommand` - Cancel editing and clear form
- `DeleteCustomerCommand` - Soft delete customer (sets inactive flag)
- `RefreshCommand` - Clear search and reload all customers

**Key Features:**
- Real-time search as you type (via OnSearchTextChanged)
- Form validation (requires Barcode and CustomerName)
- Automatic refresh after save/delete operations
- Comprehensive customer data capture (27 fields total)

### 2. CustomerService.cs Updates
Added CRUD methods to `Services/CustomerService.cs`:

**New Methods:**
- `AddCustomerAsync(Customer customer)` - Insert new customer, returns customer_id
- `UpdateCustomerAsync(Customer customer)` - Update existing customer by customer_id
- `DeleteCustomerAsync(int customerId)` - Soft delete (sets inactive = true)
- `AddCustomerParameters(IDbCommand cmd, Customer customer)` - Helper method for parameterized queries

**Security:**
- All methods use parameterized queries to prevent SQL injection
- Soft delete preserves data integrity (doesn't physically remove records)

### 3. Customer Model Updates
Extended `Models/POSModels.cs` Customer class with additional fields:

**Added Properties:**
- `ContactName` - Primary contact person name
- `ContactPosition` - Contact's job title
- `Country` - Country field
- `Fax` - Fax number
- `Website` - Website URL
- `Abn` - Australian Business Number
- `TaxCode` - Tax classification code
- `Notes` - General notes field

### 4. UI Implementation
Updated `Views/MainWindow.axaml` Customers tab with comprehensive UI:

**Layout:**
- **Left Panel (40%)**: DataGrid showing customer list with columns:
  - Barcode
  - Customer Name
  - Company
  - Phone
  - Balance
- **Right Panel (60%)**: Detailed form for add/edit operations (only visible when IsEditing = true)
- **Top Bar**: Search box and action buttons (New, Edit, Delete, Refresh)
- **Bottom**: Status message display

**Form Sections:**
1. Basic Info (Barcode, Name, Company, Grade, Inactive flag)
2. Contact Information (Contact Name, Position)
3. Address (Street, Suburb, State, Postcode, Country)
4. Phone Numbers (Business, Home, Mobile, Fax)
5. Email & Website
6. Tax & Account Information (ABN, Tax Code, Account flag, Balance, Credit Limit)
7. Notes (multi-line text area)
8. Action Buttons (Save, Cancel)

### 5. MainWindowViewModel Integration
Updated `ViewModels/MainWindowViewModel.cs`:
- Added `CustomerViewModel` property
- Initialized CustomerViewModel in constructor
- Updated CustomerList command to load customers when switching to Customers tab

## Usage

### Viewing Customers
1. Click "Customers" menu item or tab
2. Customer list loads automatically
3. Use search box to filter by name, company, or barcode

### Adding a Customer
1. Click "New" button
2. Fill in required fields (Barcode* and Customer Name*)
3. Fill in optional contact, address, and account information
4. Click "Save" to commit

### Editing a Customer
1. Select a customer from the list
2. Click "Edit" button
3. Modify fields as needed
4. Click "Save" to commit or "Cancel" to discard changes

### Deleting a Customer
1. Select a customer from the list
2. Click "Delete" button
3. Customer is marked as inactive (soft delete)

### Searching Customers
1. Type in search box at top of Customers tab
2. Search is performed automatically as you type
3. Searches across customer name, company name, and barcode
4. Click "Refresh" to clear search and show all customers

## Database Schema
Uses the PostgreSQL `customer` table with these columns:
- customer_id (primary key)
- barcode (unique, indexed)
- customername (indexed)
- companyname (indexed)
- grade, inactive, contactname, contactposition
- address, suburb, state, postcode, country
- businessphone, homephone, fax, mobile
- emailaddress, website
- abn, taxcode
- isaccount, accountbalance, creditlimit
- notes
- date_created, date_modified (timestamps)

## Future Enhancements
- Confirmation dialog before delete
- Customer photo/image support
- Customer purchase history view
- Export customer list to CSV/Excel
- Import customers from file
- Customer groups/categories
- Advanced filtering options
- Credit limit warnings
