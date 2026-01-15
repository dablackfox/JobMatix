# Transaction Types Implementation Guide

## Overview
The POS system supports 4 transaction types: Sale, Refund, Quote, and Layby. Each has distinct behavior in the UI and database commit process.

## Current Status (Phase 2)
✅ Basic Sale functionality implemented
❌ Refund, Quote, Layby need implementation

---

## 1. SALE (Default) - Currently Implemented
**Purpose**: Standard retail sale transaction

### UI Behavior:
- **Label Color**: Default/Black
- **Label Text**: "Sale"
- **Payments Grid**: Enabled - accepts all payment types
- **Discount**: Enabled
- **On-Account checkbox**: Enabled (for account customers)
- **Invoice Total Label**: "Invoice Total"

### Validation:
- Must have items in sale
- Must have staff authenticated
- For cash sales: Total paid must equal or exceed total amount
- For account sales: Can be partially paid or unpaid

### Database Operations:
1. **Invoice table**: Insert with transactiontype='Sale'
2. **Invoice_lines table**: Insert all line items
3. **Payments table**: Insert payment record(s)
4. **Stock table**: Decrement quantity for each item sold
5. **Customer table**: Update account balance if on-account

### Special Rules:
- Cash rounding applies (5-cent rounding for cash payments)
- Change is calculated and returned
- Can use credit notes to pay
- Serial number tracking for items that require it

---

## 2. REFUND - NOT YET IMPLEMENTED
**Purpose**: Return items and refund money to customer

### UI Behavior:
- **Label Color**: Tomato/Red
- **Label Text**: "Refund"
- **Payments Grid**: DISABLED (refunds don't take payments)
- **Discount**: Enabled
- **Refund Type Panel**: VISIBLE with options:
  - Cash
  - Credit (creates credit note)
  - EFTPOS Debit
  - EFTPOS Credit
  - Other (various payment types)
- **Invoice Total Label**: "Refund Total"

### Validation:
- Must have items (being returned)
- Must select refund type
- Quantities are NEGATIVE
- Prices can be adjusted (partial refund value)

### Database Operations:
1. **Invoice table**: Insert with transactiontype='Refund'
   - Amounts are NEGATIVE
2. **Invoice_lines table**: Insert with NEGATIVE quantities
3. **Payments table**: Insert NEGATIVE payment based on refund type
4. **Stock table**: INCREMENT quantity (items returned to stock)
5. **Customer table**: 
   - If refund to credit: Add to credit note balance
   - If refund from account: Reduce account balance

### Special Rules:
- For refund to credit note: Creates positive credit note balance
- For refund to cash/eftpos: Negative payment record
- Can reference original invoice (though not required)
- Serial numbers should be captured for tracked items

---

## 3. QUOTE - NOT YET IMPLEMENTED  
**Purpose**: Price quotation for customer, no money changes hands

### UI Behavior:
- **Label Color**: DarkOrange
- **Label Text**: "Quote"  
- **Payments Grid**: DISABLED (quotes don't involve payment)
- **Discount**: Enabled (to show discounted pricing)
- **On-Account checkbox**: Hidden/Disabled
- **Invoice Total Label**: "Quote Total"

### Validation:
- Must have items
- Must have staff authenticated
- NO payment validation (quotes don't require payment)
- Can save with $0 or any amount

### Database Operations:
1. **Invoice table**: Insert with transactiontype='Quote'
   - No payment/balance tracking
2. **Invoice_lines table**: Insert all line items
3. **Payments table**: NO inserts (no payments on quotes)
4. **Stock table**: NO changes (no stock movement)
5. **Customer table**: NO balance changes

### Special Rules:
- Quote can be "converted" to Sale later (not auto-implemented)
- Useful for pricing inquiries
- Can be emailed/printed for customer
- Serial numbers NOT required/captured

### Conversion Process (when quote becomes sale):
1. Load quote items into new sale
2. Verify stock availability
3. Process as normal sale
4. Original quote record remains unchanged

---

## 4. LAYBY - NOT YET IMPLEMENTED
**Purpose**: Reserve items with deposit, pay off over time, collect when paid

### UI Behavior:
- **Label Color**: DarkViolet/Purple
- **Label Text**: "Layby"
- **Payments Grid**: ENABLED (accepts deposit payments)
- **Discount**: Enabled
- **On-Account checkbox**: Hidden/Disabled (laybys can't be on-account)
- **Credit Note Payment**: DISABLED (can't pay layby deposit with credit note)
- **Invoice Total Label**: "Layby Total"

### Validation:
- Must have items
- Must have staff authenticated
- Must have at least partial payment (deposit)
- Typically require minimum % deposit (configurable)

### Database Operations:
1. **Layby table**: Insert new layby record
   - Fields: layby_id, customer_id, staff_id, date_created
   - total_amount, amount_paid, balance, status
2. **Layby_lines table**: Insert all line items
3. **Payments table**: Insert deposit payment
4. **Stock table**: Decrement quantity (items reserved)
5. **Customer table**: NO balance change (laybys separate from account)

### Layby Lifecycle:
1. **New Layby**: Create with deposit, items reserved
2. **Additional Payments**: Add payments against layby_id
3. **Sell Out Layby**: Convert to sale when fully paid
   - Create normal Sale invoice
   - Reference layby_id
   - Include any discount from original layby
   - Transfer stock (already reserved)
   - Close layby record (status = 'Completed')
4. **Cancel Layby**: Refund payments, return stock
   - Status = 'Cancelled'
   - Stock returns to available

### Special Rules:
- Items are RESERVED (not available for other sales)
- Customer can make multiple payments over time
- Store policy determines deposit % and payment terms
- Can apply discount at time of layby creation
- Serial numbers captured at layby creation

---

## Implementation Priority

### Phase 2 Completion (Current Focus):
1. ✅ **Sale** - Working (needs receipt printing)
2. **Refund** - Priority 2 (after receipt printing)
3. **Quote** - Priority 3
4. **Layby** - Priority 4 (most complex, separate tables needed)

### Database Schema Requirements:

#### Already Created:
- invoice (has transactiontype column)
- invoice_lines
- payments
- stock
- customer

#### Need to Create for Layby:
```sql
CREATE TABLE layby (
    layby_id SERIAL PRIMARY KEY,
    customer_id INT NOT NULL REFERENCES customer(customer_id),
    staff_id INT NOT NULL REFERENCES staff(staff_id),
    date_created TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    total_amount NUMERIC(19,4) NOT NULL,
    amount_paid NUMERIC(19,4) NOT NULL DEFAULT 0,
    balance NUMERIC(19,4) NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'Active', -- Active, Completed, Cancelled
    notes TEXT,
    CONSTRAINT layby_customer_fkey FOREIGN KEY (customer_id) REFERENCES customer(customer_id),
    CONSTRAINT layby_staff_fkey FOREIGN KEY (staff_id) REFERENCES staff(staff_id)
);

CREATE TABLE layby_lines (
    laybyline_id SERIAL PRIMARY KEY,
    layby_id INT NOT NULL REFERENCES layby(layby_id),
    stock_id INT NOT NULL REFERENCES stock(stock_id),
    description VARCHAR(255),
    quantity NUMERIC(19,4) NOT NULL,
    unitprice NUMERIC(19,4) NOT NULL,
    linetotal NUMERIC(19,4) NOT NULL,
    serialno VARCHAR(50),
    CONSTRAINT laybyline_layby_fkey FOREIGN KEY (layby_id) REFERENCES layby(layby_id),
    CONSTRAINT laybyline_stock_fkey FOREIGN KEY (stock_id) REFERENCES stock(stock_id)
);
```

---

## Code Changes Needed

### 1. SaleViewModel.cs - Add Transaction Type Logic
```csharp
// Add property for refund type
[ObservableProperty]
private string _refundType = "Cash"; // Cash, Credit, EftposDr, EftposCr, Other

// Add boolean flags
public bool IsRefund => TransactionType.Equals("Refund", StringComparison.OrdinalIgnoreCase);
public bool IsQuote => TransactionType.Equals("Quote", StringComparison.OrdinalIgnoreCase);
public bool IsLayby => TransactionType.Equals("Layby", StringComparison.OrdinalIgnoreCase);

// Update validation
private bool CanCommit()
{
    if (IsQuote)
        return SaleItems.Count > 0 && CurrentStaff != null; // No payment validation
    
    if (IsRefund || IsLayby)
        return SaleItems.Count > 0 && CurrentStaff != null && TotalPaid >= 0;
    
    // Sale
    return SaleItems.Count > 0 && CurrentStaff != null && 
           (TotalPaid >= AmountDue || CurrentCustomer?.IsAccount == true);
}
```

### 2. MainWindow.axaml - Add Refund Type Selection
```xml
<!-- Refund Type Panel (visible only when TransactionType = Refund) -->
<Border IsVisible="{Binding SaleViewModel.IsRefund}">
    <StackPanel>
        <TextBlock Text="Refund Type:" FontWeight="Bold"/>
        <RadioButton Content="Cash" GroupName="RefundType" 
                    IsChecked="{Binding SaleViewModel.RefundType, Converter={StaticResource EnumToBoolConverter}, ConverterParameter=Cash}"/>
        <RadioButton Content="Credit Note" GroupName="RefundType"
                    IsChecked="{Binding SaleViewModel.RefundType, Converter={StaticResource EnumToBoolConverter}, ConverterParameter=Credit}"/>
        <RadioButton Content="EFTPOS Debit" GroupName="RefundType"/>
        <RadioButton Content="EFTPOS Credit" GroupName="RefundType"/>
    </StackPanel>
</Border>
```

### 3. SaleService.cs - Modify Commit Logic
```csharp
public async Task CommitSaleAsync()
{
    // Determine operation based on transaction type
    if (TransactionType.Equals("Refund", StringComparison.OrdinalIgnoreCase))
    {
        await CommitRefundAsync();
    }
    else if (TransactionType.Equals("Quote", StringComparison.OrdinalIgnoreCase))
    {
        await CommitQuoteAsync();
    }
    else if (TransactionType.Equals("Layby", StringComparison.OrdinalIgnoreCase))
    {
        await CommitLaybyAsync();
    }
    else
    {
        await CommitSaleTransactionAsync(); // Current implementation
    }
}
```

---

## Next Steps

1. **Complete Sale** (Priority 1):
   - ✅ Fix invoice number generation
   - ✅ Fix payment schema
   - ⏳ Add receipt printing
   - ⏳ Test end-to-end sale workflow

2. **Implement Refund** (Priority 2):
   - Add refund type selection UI
   - Modify quantities to negative
   - Handle stock returns (increment)
   - Handle refund payment types

3. **Implement Quote** (Priority 3):
   - Simplest - just disable payments
   - No stock movement
   - Add quote printing

4. **Implement Layby** (Priority 4):
   - Create database tables
   - Build layby management UI
   - Implement payment tracking
   - Implement sell-out process
