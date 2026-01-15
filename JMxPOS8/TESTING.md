# JMxPOS8 Testing Guide

## Current Status
✅ **Phase 1 Complete** - Core sale functionality is now fully functional!

## What's Working

### 1. Sale Entry System
- Staff barcode validation
- Customer barcode lookup
- Item barcode scanning
- Real-time price calculations
- Transaction type selection (Sale/Refund/Quote/Layby)
- Multiple payment methods (Cash/EFTPOS/Credit Card/Account)
- Sale commit to database
- Stock quantity updates

### 2. Database Integration
- PostgreSQL connection working
- Staff authentication
- Customer lookup
- Stock lookup
- Invoice creation
- Payment recording

### 3. UI Features
- Menu system with keyboard shortcuts
- Tab-based interface
- Status bar with staff/till info
- Real-time totals (Subtotal, GST, Total)
- Sale items grid
- Payment buttons

## How to Test the Sale Workflow

### Prerequisites
1. Ensure PostgreSQL is running:
   ```bash
   docker ps | grep jobmatix-postgres
   ```

2. Launch the application:
   ```bash
   cd /home/cw/Documents/JobMatix/JMxPOS8
   ./bin/Debug/net8.0/JMxPOS8
   ```

### Test Case 1: Simple Cash Sale

**Steps:**
1. **Sign in Staff:**
   - Click in the "Staff" barcode field
   - Type: `STAFF001` (or scan a valid staff barcode)
   - Press Enter
   - **Expected:** Status bar shows "Staff: [staff name]"

2. **Add Customer (Optional for walk-in):**
   - Click in the "Customer" barcode field
   - Type a customer barcode (e.g., `CUST001`)
   - Press Enter
   - **Expected:** Customer info appears below barcode field

3. **Scan Items:**
   - Click in the "Barcode" field (item entry section)
   - Type a stock barcode (e.g., `STK001`, `12345`, etc.)
   - Press Enter
   - **Expected:** 
     - Item description appears
     - Price fills in
     - Extension calculated (Qty × Price)
   - Click the "+" button or press Enter again
   - **Expected:** Item appears in the grid below
   - Repeat for additional items

4. **Review Totals:**
   - **Expected:** 
     - Subtotal shows sum of all items (ex GST)
     - Tax shows GST amount (Subtotal × 0.1)
     - Total shows final amount (inc GST)

5. **Add Discount (Optional):**
   - Type a discount amount in the "Discount" field
   - **Expected:** Total recalculates

6. **Take Payment:**
   - Click "Cash (F1)" button
   - **Expected:** Status message shows payment added

7. **Commit Sale:**
   - Click "Commit Sale (F12)" button
   - **Expected:** 
     - Status shows "Sale committed! Invoice #[number]"
     - Grid clears after 1.5 seconds
     - Form ready for next sale
     - Check database:
       ```sql
       SELECT * FROM invoice ORDER BY invoice_id DESC LIMIT 1;
       SELECT * FROM invoice_lines WHERE invoice_id = [last_invoice_id];
       SELECT * FROM payments WHERE invoice_id = [last_invoice_id];
       ```

### Test Case 2: Account Customer Sale

**Steps:**
1. Sign in staff (as above)
2. Scan account customer barcode
3. Add items to sale
4. Click "Account" button instead of Cash
5. Commit sale
6. **Expected:** 
   - Sale charged to customer account
   - Customer balance updated in database

### Test Case 3: Multiple Payment Types

**Steps:**
1. Sign in staff
2. Add items totaling $100.00
3. Click "Cash (F1)" - pay $50
4. **Expected:** Amount due now shows $50.00
5. Click "EFTPOS (F2)" - pay remaining $50
6. **Expected:** Amount due shows $0.00
7. Commit sale
8. **Expected:** Both payments recorded in database

### Test Case 4: Refund Transaction

**Steps:**
1. Click "Refund" button in transaction type section
2. **Expected:** Status shows "Transaction type: Refund"
3. Scan items to refund
4. Take payment (usually cash out)
5. Commit
6. **Expected:** 
   - Invoice created with negative amounts
   - Stock quantities increase

## Known Keyboard Shortcuts

- **F1**: Cash payment
- **F2**: EFTPOS payment / Search customer
- **F5**: New (stock/customer - not yet implemented)
- **F6**: Hold sale (not yet implemented)
- **F8**: Show last invoice (not yet implemented)
- **F9**: New sale
- **F12**: Commit sale
- **Ctrl+O**: Stock list (not yet implemented)
- **Ctrl+U**: Customer list (not yet implemented)

## Database Verification Queries

After completing a sale, verify in PostgreSQL:

```sql
-- Check last invoice
SELECT * FROM invoice ORDER BY invoice_id DESC LIMIT 1;

-- Check invoice lines
SELECT il.*, s.description 
FROM invoice_lines il
JOIN stock s ON il.stock_id = s.stock_id
WHERE invoice_id = [last_invoice_id];

-- Check payments
SELECT * FROM payments WHERE invoice_id = [last_invoice_id];

-- Check stock quantity updated
SELECT stock_id, barcode, description, quantityinstock 
FROM stock 
WHERE barcode = '[scanned_barcode]';

-- Check customer balance (if account customer)
SELECT customer_id, customername, accountbalance 
FROM customer 
WHERE barcode = '[customer_barcode]';
```

## Troubleshooting

### Issue: "Staff not found"
- **Solution:** Check staff table for valid barcodes:
  ```sql
  SELECT barcode, firstname, lastname, docket_name FROM staff;
  ```

### Issue: "Item not found"
- **Solution:** Check stock table:
  ```sql
  SELECT barcode, description, sellprice FROM stock LIMIT 10;
  ```

### Issue: Database connection error
- **Solution:** 
  - Check `.env` file exists in JMxPOS8 directory
  - Verify PostgreSQL container running
  - Check connection string in `.env`

### Issue: Application won't start
- **Solution:**
  - Rebuild: `dotnet build`
  - Check for errors in terminal output
  - Verify all dependencies installed

## Next Steps

### Coming Soon:
1. **Customer Management Tab**
   - Customer list/browse
   - Customer search
   - Add/edit customers

2. **Stock Management Tab**
   - Stock list/browse
   - Stock search
   - Add/edit stock items

3. **Reports Tab**
   - Sales reports
   - Stock reports
   - Customer reports
   - Cash up

4. **Additional Features**
   - Hold/retrieve sales (F6)
   - Show last invoice (F8)
   - Serial number handling
   - Barcode printing
   - Receipt printing

## Test Data

If you need test data, you can insert sample records:

```sql
-- Add test staff
INSERT INTO staff (barcode, firstname, lastname, docket_name, position)
VALUES ('STAFF001', 'Test', 'User', 'TestUser', 'Manager');

-- Add test stock
INSERT INTO stock (barcode, stockcode, description, quantityinstock, costprice, sellprice)
VALUES 
  ('12345', 'TEST001', 'Test Item 1', 100, 10.00, 15.00),
  ('67890', 'TEST002', 'Test Item 2', 50, 20.00, 30.00);

-- Add test customer
INSERT INTO customer (barcode, customername, isaccount, accountbalance, creditlimit)
VALUES ('CUST001', 'Test Customer', true, 0, 1000);
```

## Success Criteria

The sale workflow is considered successful when:
- ✅ Staff can sign in via barcode
- ✅ Customers can be selected via barcode
- ✅ Items can be scanned and added to sale
- ✅ Totals calculate correctly (including GST)
- ✅ Multiple payment types work
- ✅ Sales commit to database successfully
- ✅ Stock quantities update after sale
- ✅ Customer balances update for account sales
- ✅ Form clears and ready for next sale

**Status: ALL CRITERIA MET! ✅**
