# Database Schema Cross-Reference

## Comparison between Original VB Code and Current PostgreSQL Schema

### Stock Table

#### Missing Columns (from VB original):
1. **track_serial** (BIT) - Original name for what we called `requiresserial` ✅ Added as `requiresserial`
2. **model_no** (nvarChar(40)) - Model number field - ❌ MISSING
3. **sales_prompt** (nvarChar(50)) - Sales prompt text - ❌ MISSING  
4. **isNonStockItem** (BIT) - Service items, labor, non-stock - ❌ MISSING
5. **allow_renaming** (BIT) - Allow item description override - ❌ MISSING
6. **longDescription** (nvarchar(max)) - Extended description - ❌ MISSING
7. **cat1** / **cat2** (nvarChar(6)) - Category codes - ⚠️ We have `category` as single field
8. **BrandName** (varchar(50)) - Brand name - ❌ MISSING
9. **goods_taxCode** (nvarChar(7)) - Tax code for purchase - ⚠️ We have `taxcode` (assumed sales)
10. **costExTax** (MONEY) - Cost excluding tax - ⚠️ We have `costprice` (unclear if inc/ex)
11. **sales_taxCode** (nvarChar(7)) - Tax code for sales - ⚠️ Merged into `taxcode`
12. **sellExTax** (MONEY) - Sell price excluding tax - ⚠️ We have `sellprice` (unclear if inc/ex)
13. **qtyInStock** (INT) - Integer quantity - ⚠️ We have `quantityinstock` (DECIMAL)
14. **reOrderLevel** (INT) - Reorder level - ⚠️ We have `minstocklevel` (DECIMAL)
15. **order_quantity** (INT) - Order quantity - ⚠️ We have `reorderquantity` (DECIMAL)
16. **freight** (BIT) - Freight charges applicable - ❌ MISSING
17. **cost_account** (nvarChar(50)) - GL cost account - ❌ MISSING
18. **income_account** (nvarChar(50)) - GL income account - ❌ MISSING
19. **comments** (nvarchar(max)) - Comments - ⚠️ We have `notes`
20. **productPicture** (image) - Product picture - ⚠️ We have `stockimage` (bytea)

#### Our Extra Columns (not in original):
- `stockcode` (VARCHAR(40)) - We added this for alternate code
- `suppliercode` (VARCHAR(40)) - Supplier's code for item
- `maxstocklevel` (DECIMAL) - Maximum stock level
- `taxrate` (DECIMAL(5,2)) - Tax rate percentage
- `unit_of_measure` (VARCHAR(20)) - Unit of measure text

### Invoice Table  

#### Missing Columns (from VB original):
1. **total_inc** (MONEY) - Total including tax - ❌ We use `totalamount`
2. **total_ex** (MONEY) - Total excluding tax - ✅ We have `subtotal`
3. **total_tax** (MONEY) - Total tax - ✅ We have `taxamount`
4. **subtotal_ex_non_taxable** (MONEY) - Non-taxable subtotal - ❌ MISSING
5. **subtotal_ex_taxable** (MONEY) - Taxable subtotal - ❌ MISSING
6. **subtotal_tax** (MONEY) - Subtotal tax - ❌ MISSING
7. **subtotal_inc** (MONEY) - Subtotal including tax - ❌ MISSING
8. **discount_nett** (MONEY) - Net discount - ❌ MISSING
9. **discount_tax** (MONEY) - Discount tax - ❌ MISSING
10. **rounding** (MONEY) - Rounding adjustment - ❌ MISSING
11. **isOnAccount** (bit) - Is on account sale - ❌ MISSING
12. **payment_id** (INT) - Related payment ID - ❌ MISSING
13. **JobNumber** (INT) - Related job number - ❌ MISSING
14. **delivered_layby_id** (INT) - Delivered layby ID - ❌ MISSING
15. **original_id** (INT) - Original invoice (for refunds) - ❌ MISSING
16. **terminal_id** (nvarChar(150)) - Computer name - ❌ MISSING
17. **cashDrawer** (nvarChar(15)) - Till/drawer identifier - ❌ MISSING
18. **currentWindowsUserName** (nvarChar(80)) - Windows user - ❌ MISSING
19. **deliveryInstructions** (nvarchar(max)) - Delivery notes - ❌ MISSING
20. **comments** (nvarchar(max)) - Comments - ⚠️ We have `notes`

#### Our Simpler Columns (different from original):
- `invoiceNumber` (VARCHAR(20)) - We added this
- `dueDate` (TIMESTAMP) - We added this
- `status` (VARCHAR(15)) - We added this
- `amountPaid` (DECIMAL) - We added this
- `amountDue` (DECIMAL) - We added this
- `paymentMethod` (VARCHAR(20)) - We added this
- `paymentReference` (VARCHAR(50)) - We added this

### InvoiceLine Table

Our table is called `invoice_lines` (lowercase with underscore).

#### Original Columns (from VB):
- line_id, invoice_id, stock_id
- description (nvarChar(40))
- serialNumber (nvarChar(40))
- cost_ex, cost_inc (MONEY)
- cost_taxCode (nvarChar(7))
- sellActual_ex, sellActual_Tax, sellActual_inc (MONEY)
- quantity (decimal(7,4))
- total_ex, total_tax, total_inc (MONEY)
- gross_profit (MONEY)

#### Our Current Columns:
Need to check what we actually have...

## Recommendations

### Critical Missing Fields to Add:

1. **Stock Table - Business Logic:**
   - `track_serial` → We added `requiresserial` ✅
   - `isNonStockItem` (CRITICAL) - For service items, labor hours
   - `model_no` - Important for inventory management
   - `allow_renaming` - POS sales flexibility

2. **Invoice Table - Sales Tracking:**
   - Rename `totalamount` → `total_inc` for consistency with original code
   - Add `total_ex` and `total_tax` - separate fields for tax calc
   - Add `subtotal_ex_non_taxable`, `subtotal_ex_taxable` - GST compliance
   - Add `discount_nett`, `discount_tax`, `rounding` - price adjustments
   - Add `isOnAccount` - account vs cash sales differentiation
   - Add `terminal_id`, `cashDrawer` - multi-till support
   - Add `original_id` - refund tracking

3. **Stock Table - Categories:**
   - Add `cat1`, `cat2` - Original uses 6-char category codes
   - Add `BrandName` - Brand tracking
   - Consider keeping our single `category` field for simplicity

4. **Stock Table - Financial:**
   - Clarify if prices are inc/ex tax (original has separate fields)
   - Add `cost_account`, `income_account` - GL integration

### Lower Priority:
- `longDescription` - Extended descriptions
- `sales_prompt` - POS prompts
- `freight` - Freight flag
- `comments` field rename to match original

## Action Items

1. ✅ Added `requiresserial` to stock table
2. ⚠️ Invoice table uses different naming: `totalamount` vs `total_inc`
3. ❌ Missing critical business logic fields for service items
4. ❌ Missing multi-till support fields
5. ❌ Missing discount and rounding fields for proper invoicing

## Notes

- The original code is more comprehensive for retail operations
- Our schema is simpler but missing key POS features
- Category system differs: original uses cat1/cat2 codes, we use single category text
- Price fields need clarification: inc/ex tax distinction
- Original has more detailed financial tracking (GL accounts, detailed tax breakdowns)
