# Quick Testing Guide - JMxPOS8

## ✅ Application Running - UPDATED
The POS is now live with fixes for barcode scanning and totals!

## 🔧 Changes Made
1. **Staff login now uses numbers** (1, 2, 3) instead of barcodes
2. **Items auto-add** when you press Enter on barcode
3. **Totals update automatically** when items are added
4. **Workflow streamlined** - scan and go!

## Test Data Available

### Staff
- **Staff Number**: `1` (System Admin)

### Stock Items
1. **Barcode**: `12345` - Test Product 1 ($15.50)
2. **Barcode**: `67890` - Test Product 2 ($39.99)
3. **Barcode**: `LAPTOP` - Gaming Laptop ($1,299.00)
4. **Barcode**: `MOUSE` - Wireless Mouse ($29.95)
5. **Barcode**: `KEYB` - Mechanical Keyboard ($89.99)

### Customer
- **Barcode**: `CUST001` - Walk-In Customer (existing)

---

## 🧪 Test Scenario 1: Simple Cash Sale

**Follow these steps in the running application:**

### Step 1: Sign In Staff
1. Look at the **Sale** tab (should be selected by default)
2. Find the **"Staff:"** field at the top
3. Click in the field and type: `1`
4. Press **Enter**
5. ✅ **Expected**: Status bar at bottom shows "Staff: Admin"

### Step 2: Add Items to Sale
1. Find the **"Barcode"** field in the item entry section
2. Type: `MOUSE`
3. Press **Enter**
4. ✅ **Expected**: 
   - Description shows "Wireless Mouse"
   - Price shows "29.95"
   - Extension shows "29.95"
   - **Item automatically adds to grid below**
   - **Totals update immediately**

### Step 3: Add More Items
1. In the Barcode field, type: `KEYB`
2. Press **Enter**
3. ✅ **Expected**: Mechanical Keyboard ($89.99) appears in grid, totals update
4. In the Barcode field, type: `12345`
5. Before pressing Enter, change Qty to: `2`
6. Press **Enter**
7. ✅ **Expected**: 2x Test Product 1 ($15.50 each = $31.00) appears, totals update

### Step 4: Review Totals
✅ **Expected Totals**:
- **Subtotal**: $136.81 (ex GST)
- **Tax (GST)**: $13.68 (10%)
- **Total**: $150.49

### Step 5: Take Payment
1. Click the **"Cash (F1)"** button
2. ✅ **Expected**: Status message shows "Cash payment added: $150.49"

### Step 6: Commit the Sale
1. Click **"Commit Sale (F12)"** button (green button at bottom right)
2. ✅ **Expected**: 
   - Status shows "Sale committed! Invoice #1" (or next number)
   - After 1.5 seconds, grid clears
   - Form ready for next sale
   - Totals reset to $0.00

### Step 7: Verify in Database
Open a terminal and run:
```bash
docker exec jobmatix-postgres psql -U jobmatix_user -d jobmatix_pos -c "
SELECT i.invoice_id, i.total, il.description, il.quantity, il.unitprice 
FROM invoice i
JOIN invoice_lines il ON i.invoice_id = il.invoice_id
ORDER BY i.invoice_id DESC 
LIMIT 10;
"
```
✅ **Expected**: Your sale appears with all 3 line items

---

## 🧪 Test Scenario 2: High-Value Sale

### Steps:
1. Staff: `1` → Enter
2. Item: `LAPTOP` → Enter (auto-adds)
3. ✅ **Expected**: Total = $1,299.00 (GST inc), totals update
4. Click **"Cash (F1)"**
5. Click **"Commit Sale (F12)"**
6. ✅ **Expected**: Invoice created successfully

---

## 🧪 Test Scenario 3: Multiple Items Same Product

### Steps:
1. Staff: `1` → Enter
2. Item: `67890` → Change Qty to `5` → Enter (auto-adds)
3. ✅ **Expected**: 
   - 5x Test Product 2 @ $39.99 each
   - Total: $219.95 (inc GST)
   - Totals visible immediately
4. Click **"Cash (F1)"**
5. Click **"Commit Sale (F12)"**

---

## 🧪 Test Scenario 4: With Discount

### Steps:
1. Staff: `1` → Enter
2. Item: `MOUSE` → Enter (auto-adds)
3. Item: `KEYB` → Enter (auto-adds)
4. ✅ **Subtotal before discount**: $109.04
5. In the **"Discount"** field, type: `10`
6. ✅ **Expected**: Total recalculates ($119.94 - $10 = $109.94)
7. Click **"Cash (F1)"**
8. Click **"Commit Sale (F12)"**

---

## 🧪 Test Scenario 5: Transaction Types

### Try Different Types:
1. Click **"Refund"** button
   - ✅ Status: "Transaction type: Refund"
2. Click **"Quote"** button
   - ✅ Status: "Transaction type: Quote"
3. Click **"Layby"** button
   - ✅ Status: "Transaction type: Layby"
4. Click **"Sale"** button to return to normal sale

---

## 🧪 Test Scenario 6: Customer Barcode

### Steps:
1. Staff: `ADMIN001` → Enter
2. Customer: `CUST001` → Enter
3. ✅ **Expected**: Customer info shows below barcode field
4. Add items and complete sale
5. ✅ **Expected**: Sale linked to customer in database

---

## 🐛 Troubleshooting

### "Staff not found!"
- Check staff barcode is exactly `ADMIN001`
- Verify staff exists:
  ```bash
  docker exec jobmatix-postgres psql -U jobmatix_user -d jobmatix_pos -c "SELECT * FROM staff;"
  ```

### "Item not found!"
- Check barcode matches exactly: `12345`, `MOUSE`, `KEYB`, etc.
- Verify stock exists:
  ```bash
  docker exec jobmatix-postgres psql -U jobmatix_user -d jobmatix_pos -c "SELECT barcode, description FROM stock;"
  ```

### Nothing happens when clicking buttons
- Check terminal for error messages
- Verify database connection in `.env` file

### "Error committing sale"
- Check terminal output for exception details
- Verify all foreign key relationships exist
- Check staff is signed in

---

## 📊 Verify Stock Quantities Updated

After completing a sale, check stock was decremented:

```bash
docker exec jobmatix-postgres psql -U jobmatix_user -d jobmatix_pos -c "
SELECT barcode, description, quantityinstock 
FROM stock 
WHERE barcode IN ('MOUSE', 'KEYB', '12345')
ORDER BY barcode;
"
```

**Before sales**:
- MOUSE: 200
- KEYB: 75
- 12345: 100

**After Test Scenario 1**:
- MOUSE: 199 (sold 1)
- KEYB: 74 (sold 1)
- 12345: 98 (sold 2)

---

## ✨ Success Criteria

You've successfully tested when:
- ✅ Staff barcode validates
- ✅ Items scan and add to grid
- ✅ Totals calculate correctly (with GST)
- ✅ Discounts apply properly
- ✅ Payments record
- ✅ Sales commit to database
- ✅ Stock quantities decrease
- ✅ Form clears for next sale

## 🎉 Next Steps

Once basic sales work:
1. Test all 5 stock items
2. Test EFTPOS and Credit Card payments
3. Test multiple payments on one sale
4. Try edge cases (zero quantity, high quantities)
5. Move on to Customer Management tab
6. Move on to Stock Management tab

---

**Happy Testing!** 🚀
