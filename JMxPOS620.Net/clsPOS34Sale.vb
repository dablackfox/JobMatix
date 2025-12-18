Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
Imports system.data.OleDb
Imports System.Math
Imports System.ComponentModel
Imports System.Threading

Public Class clsPOS34Sale

    '-  grh JobMatixPOS DLL started 3.1.3101.1007 -

    '== SALE class ==
    '--
    '-- Class to hold ALL the vars/code for POS Sales..--
    '-- Strips Sale Form of all but the basic event wrappers..-
    '==
    '==
    '==  grh JobMatixPOS 3.1.3101.1014 -
    '==     >> Updates to Commit for serials.
    '==         and- Enforce Qty of 1 for Serial Sale Item line.
    '==
    '==  grh JobMatixPOS 3.1.3101.1030 -
    '==       >> Implementing Transaction Types.
    '==
    '==  grh JobMatixPOS 3.1.3101.1104 -
    '==       >> No printers given in input. (ShowInvoice finds its own).
    '==
    '==  grh JMxPOS31 3.1.3101.1206 -
    '==       >> Drop table PaymentTypes.
    '==        >> Replace chkChargeToAcct with labCharge -
    '==
    '==  grh JobMatixPOS 3.1.3101.1226 -
    '==       >> New public sub to catch Staff Signon..
    '==
    '==  grh JobMatixPOS 3.1.3103.0101 -  01-Jan-2015
    '==       >> Fixes to quotes..
    '==
    '==  grh JobMatixPOS 3.1.3103.0111 -  11-Jan-2015
    '==       >> Load GST rate from systemInfo...
    '==       >> 3103.0115  staffTimeoutSuspended when Cust. selected.
    '==       >> 3103.0129  SECOND cancel button.
    '==
    '==  grh JobMatixPOS 3.1.3103.0216 -  16-Feb-2015
    '==       >> Add Columns to SerialAuditTrail -
    '==              to Track RM-Imported Serial Movements...
    '==
    '==  grh JobMatixPOS 3.1.3103.0227 -  27-Feb-2015
    '==       >> Fix Job Delivery WHERE syntax for Browse. -
    '==
    '==   grh. =3103.0301=  -01Mar2015=
    '==      >> Move all DB ALTER TABLE stuff to HERE from POS shell acitivate-
    '==
    '==      >> 3103.0305- Job Delivery: Use SystemInfo settings to find Labour Stock_id.. -
    '==
    '==   grh. =3103.0403=  -04Apr2015=
    '==      >> Don't write Payment Record if no Payment Rcvd (eg Charged to Account).-
    '==
    '==  grh JobMatixPOS 3.1.3103.0411 -  11-Apr-2015
    '==       >> Add Columns 'subtotal_ex_non_taxable', 'subtotal_ex_taxable' to Invoice table.  -
    '==       >> Add Column 'doNotEmailDocuments' to customer table.  -
    '==
    '==  grh JobMatixPOS 3.1.3103.0419 -  19-Apr-2015
    '==       >> Allow user to bypass Serial No. on sale item after we query it.  -
    '==       >> Add Combo to select Discount %.  -
    '==
    '==  grh JobMatixPOS 3.1.3107.0606 -  06-Jun-2015
    '==       >> Commit Quote- Don't insert "jobNumber" column...  -
    '==
    '==  JobMatix POS3- 3107.0713--  13-Jul-2015 ===
    '==   >>     MIGRATED to vs-2013 -==...- 
    '==   >>     MIGRATED to vs-2013 -==...- 
    '==   >>     MIGRATED to vs-2013 -==...- 
    '==   >> Accumulate Total Sale cost and Show Overall Margin.-==...- 
    '==
    '==  JobMatix POS3- 3107.0724--  24-Jul-2015 ===
    '==   >>  "Can't charge CASHOUT to Account.."
    '==   >>  0726-  Check for amount strings too long.
    '==   >>  0726-  Check Discount doesn't exceed SubTotal..
    '==   >>  0727-  Add "Delete Row" button column for Sales DGV..
    '==
    '==  grh. JobMatix 3.1.3107.0801 ---  01-Aug-2015 ===
    '==   >>  POSSettings file now in CommonApplicationData--
    '==   >>   Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)   
    '==
    '==  grh. JobMatix 3.1.3107.0805 ---  05-Aug-2015 ===
    '==   >>  POSSettings path now form xlsWinSpecial--
    '==   >> Now for .Net 4.5.2- 
    '==
    '==  grh JobMatixPOS 3.1.3107.0815 -  15-Aug-2015
    '==       >> Updates for purchaseOrders Table..  -
    '==       >> Add DocArchive Table..  -
    '==       >> 823- Add PO col. delvery_address..  -
    '==
    '==  grh JobMatixPOS 3.1.3107.0831 -  31-Aug-2015
    '==       >> Use form "dlgQueryCommit to get answer and print/email options...  -
    '==       >> BACK to .Net 4.5- 
    '==
    '==  grh JobMatixPOS 3.1.3107.0916 -  16-Sep-2015
    '==       >> Updates for PO and PO-Lines..  -
    '==       >> BACK to .Net 3.5- 
    '==
    '==   POS dll-  v3.1.3107.922..  22-Sep-2015= ===
    '==      >> Previous invoices--
    '==      >>  -- Now use command button to pop up list of invoices.
    '==
    '==  NEW VERSION-  JMxPOS32
    '==
    '==   POS dll-  v3.2.3201.131..  31Jan2016= ===
    '==      >> delete all DB ALTER updates..
    '==          Everyone must re-create new DB..--
    '==
    '== = =
    '==     v3.3.3301.427..  27-Apr-2016= ===
    '==        >> Restore "OptSaleSale" RadioButton
    '==              to set of Trans. options.  Still the Default..
    '==      >> NOW using clsSystemInfo-- 
    '==
    '==      >> 3301.511=  11May2016=
    '==   --    (UNSUCCESSFUL ATTEMPT)- If needs serial, move to next col. (serial)..
    '==
    '==
    '==     v3.3.3301.516..  16-May-2016= ===
    '==          >> Redesign POS Sale Form to use Textboxes for Entry of current sale.
    '==              Grid NO longer used for Entry of current sale.
    '==
    '== = = = =
    '==
    '==     v3.3.3301.622..  22-June-2016= ===
    '==       >> Update Invoice and PAYMENTS Table Schema-  
    '==     BACK to the ORIGINAL schema-  No IP Invoice records, 
    '==       and PaymentDisbursement records restored to duty..
    '==         (For Account customers, part payment with a sale records a separate Payment entry)
    '==
    '==     Credit-Note Credits/Debits are held and tracked in Payments Table Records..
    '==
    '==     v3.3.3301.710..  10-July-2016= ===
    '==       >> Update/fix Jobs Delivery- 
    '==  = = = = 
    '==
    '==     v3.3.3301.1112..  12-Nov-2016= ===
    '==       >> Fixes to Sale Coomit.- 
    '==                (Was not saving SerailAuditTrail trans. ).
    '==
    '==     v3.3.3301.1114..  14-Nov-2016= ===
    '==       >> Add Licence Checking (cloning POS licence off Jobmatix)..-
    '==
    '==     v3.3.3301.1210..  10-Dec-2016= ===
    '==       >> Introducing CashDrawer ID's (as per MYOB RM.).- 
    '==       >> Every W/s running POS must have a current CashDrawer ID. ("A".."Z".).- 
    '==       >> Any given CashDrawer ID can be used by several W/s at a time..- 
    '==       >> CashDrawerId/Computer (w/s) assignments are kept in SystemInfo Table-     
    '==       >> CashDrawer ID of W/s ("A".."Z".) is recorded in every invoice and Payment Record.
    '==                  as well as in CashUp Sessions.
    '==       >> Till Balances and Cashup Sessions are on the basis of CashDrawer ID. (NOT terminal_id).
    '==
    '==     v3.3.3301.1227..  27-Dec-2016= ===
    '==       >> Fixes to item editing sequences..- 
    '==       >> Cust Barcode- Must catch "Validating" event for TAB key. .- 
    '==
    '==     v3.3.3303.0116..  16-Jan-2017== ===
    '==       >> Fixes Limit/Outstanding display...-
    '==             (mLabSaleAvailCredit replaced by mListViewSaleAvailCredit).
    '==       >> Fixes to bypassing serial no on refund....- 
    '==
    '==  New build =
    '==
    '==     v3.3.3307.0205..  05-Feb-2017== ===
    '==       >> Commit Sale- Catch commit failed error...-
    '==       >> Fix have-changechange/(not credit note) error on account Sale computing balance....
    '==       >> 3307.0218/0219 =
    '==          -- Handle txtSaleItemBarcode VALIDATED Event for all Line Item textboxes..
    '==      >> txtSaleItemBarcode_TextChanged Event now captured for ItemBarcode ONLY--
    '==      >> txtSaleItemBarcode_TextChanged Event now captured for ItemBarcode ONLY--
    '== 
    '== 
    '==  NEW POS Build..
    '==
    '==     v3.3.3311.0225..  25-Feb-2017= ===
    '==       >> SALE Screen. Allow user to TAB (Fom blank barcode/serial) 
    '==                    to Payments Footer without using MOUSE !!! 
    '==            and allow Footer for Sale even if no items..
    '==       >> SALE/Commit Confirmation Form.. Add printer combos, and pass selection onto ShowInvoice..
    '=          -- Also When called from Commit, 
    '==                 printing of Invoice or Receipt auto-proceeds without further user decision.
    '==       >> 27Feb2017= SALE Screen. in clsPOS31Main- set dgvSaleItems.StandardTab -> TRUE !!! 
    '==               AND-  SALE Screen. in clsPOS31Main- set mDgvSalePaymentDetails.StandardTab -> TRUE !!! 
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW POS Build..  For Multiple Sale Instances..--
    '== 
    '==     v3.4.3401.0307 = 07Mar2017=
    '==      -- New Build for POS 34.  Extended TAB controls inside JobMatix POS Tab...
    '==      --  Plus- Extra form controls for staff barcode/name..
    '==             (Staff must "sign-in" for each Sale Transaction..)
    '==      -- SALE Screen can NOW support 3 clsPOS31Main instances (For Holding Sales in progress)--
    '==            ie the Main Form can be holding muliple instances of this class..
    '==      -- Added functions to save Screen controls info (eg datagridviews) after each totals update,
    '==            and public function RESTORE to re-load cotrols when this Instance gets screen back
    '==               (Any Sale Instance can lose the screen if HOLD is pressed, 
    '==                        the screen is given to a held Sale..).
    '== 
    '==      -- This now renamed to clsPOS34Sale..
    '==              AND receives and handles all Admin Function buttons Click events..
    '== 
    '==     3401.0314- SALE - CASHOUT totally removed.--
    '==
    '==
    '--   Was Archived and tested at Precise..
    '--   Was Archived and tested at Precise..
    '==
    '==   3401.0321- Fixes and Changes-..-
    '==     >>  Trans. selection. Replaced RadioButtons with Combo. (Sale/Refund/Quote). 
    '==     >>  Show Invoice/Quote. Replaced RadioButtons with two cmd buttons.. 
    '==
    '==
    '==    3401.327=   Cashout are DROPPED !!
    '==      >> ALSO-- ADD EFTPOS_DR, EFTPOS_CR to Refund Options.
    '==    v.3401.0414- 14Apr2017=
    '==          >> Fixes to find/use correct (Client) ComputerName in case of THIN CLIENT.-..-
    '==
    '= = = = = = = = = = = = =  ==  = = = = =
    '==
    '==  NEW POS Build..  For Adding on Layby's functionality...--
    '==  NEW POS Build..  For Adding on Layby's functionality...--
    '==  NEW POS Build..  For Adding on Layby's functionality...--
    '== 
    '==     v3.4.3403.0430 = 30Apr2017=
    '==      --  Add Tables LayBy and LaybyLine
    '==      --  Catch New LayBy Transaction & update Commit to Write LayBy/-Lines Records
    '==      --  Customer setup- Check if Laybys on shelf for customer.
    '==      --  3403.510-  Add Event for new button.. Show/Print layby..
    '==     v3.4.3403.0711 = 11Jul2017=
    '==      --  colPrefsStaff..  swap barcode & docket-name
    '==      -- Check for Microsoft PDF printer also.-
    '==
    '==     v3.4.3403.0731 = 31Jul2017=
    '==      -- clsPOS34Sale-  Fix TAX code choice if setting up line. (was using GoodsTax code-)..
    '==      -- Allow F5 to make NEW Customer..
    '==
    '==     3403.0917- 15/16/17-Sep-2017-
    '==      -- clsPOS34Sale-  Add code to apply correct Cust pricing Grade...
    '==      -- clsPOS34Sale-  Payments.. default Grid-payment amt to outst. balance...
    '==
    '==
    '==     3403.1014- 07/10/11/14-Oct-2017-
    '==      -- MAJOR SHIFT..
    '==           >> All Customers can have credit Notes (Account and non-a/c custs).
    '==           >>  For Sales to Account Cust-  only onAccount invoices will go to Debtors.
    '==                  So account cust can have normal Cash Sales
    '==                --  'chkOnAccount' Checkbox (Charge to Account) added to sale Payments Panel. 
    '==                    (User must check this to make on-account sale..).
    '==           >>  On-Account sales can have partial Debtors payment with it..       
    '==           >>  Refunds now the same for Account Custs and non-a/c custs.. 
    '==                      ie refund via cash or CreditNote.
    '==          >>  3403.1109= NO MORE empty Account payment Recordss..
    '==          '=3411.1118-
    '==          >>  3411.1118= UPdates to calling Confirm sale Form..
    '=--         >>  3411.1125=  PLUS View Subscriptions..
    '==
    '==        >> 3411.0107 -- 07-Jan-2018== .
    '==                -- For POS + JobTracking and Job Sale.. 
    '==                   Add "Diagnosis" text to Sale Invoice as an add-on to Comments as "Job-Report"..  . 
    '==     v3.4.3411.0109. 09-Jan-2018= ===
    '==       >> Show PDF printer if any.....
    '==       >> LIMIT the Invoice Line Qty to 999, 
    '==                as decimal(7,4) gives only 3 digits on the Left !!...
    '==
    '==     3411.0110- 10Jan2018=
    '==       -- Call function "gsGetPdfPrinterName" to find/decide PDF printer.
    '==     3411.0124- 24Jan2018=
    '==       -- Replaced all IDENT_CURRENT with "SCOPE_IDENTITY".
    '==       -- Selecting Customer-  F5 for new Customer.
    '==    3411.0205/0208= 08Feb2018= 
    '==     --Fix to beginEdit to NOT add tax if Tax COde is NOT GST (eg FRE)... 
    '==     --  Sale Form- Add BUTTON to show Last Sales Invoice.
    '==
    '==    -- 3411.0313=  11/13-Mar-2018= Fixes for Lachlan- 
    '==        (i)  Sales Form:  Use buttons to select Trans..  
    '==                   !! Sale has to be without mouse..
    '==        (ii) Drop Combo, drop "Continue" button..  
    '==       (iii) Fix Selling out Job (1 item missing)..
    '==       (iv) Looking up Stock.. Added extra browse form.
    '==                   --  frmBrowse33 (inc text search)..
    '==        (v) Account Sale with part payment- Fix "Can-Email".. 
    '==
    '==     3411.0324- 24-March-2018-
    '==      -- POS Setup/Options. Set Cash Drawer printer Connections....
    '==
    '==     3411.0402-  02-Apr-2018-
    '==        -- Main POS Form. AND here.. Catch CTL-Z and Open CashDrawer for current Till..
    '==             AND open Till after SALE committed..
    '==       -- 05-Apr-2018- POS Form and Sale- Fix Discount Tab sequence...
    '==       -- 07-Apr-2018- POS Sale- Strip leading Zeroes on Item Barcode...
    '==                  AND-   If barcode not found, Then retry with lead zeroes intact (if any).
    '==       -- 15-Apr-2018- POS Sale- Catch Mouse-Click on Item Barcode to set Selection stuff....
    '==
    '==
    '==-- (3411.0417 Was released to Precise..)
    '==-- (3411.0417 Was released to Precise..)
    '== - - - - - - - - - - - - - - - - - - -- - 
    '==
    '==   >> 3411.0420=  20-April-2018=
    '==     -- Fixing Sale Commit not updating stock ("isNonStockItem") was mis-named.
    '==     -- Fixing Sale Qty validating..  Add:    ev.Cancel = True  '=3411.0421=
    '==
    '==-- (3411.0423 Was released to Precise..)
    '==-- (3411.0423 Was released to Precise..)
    '== - - - - - - - - - - - - - - - - - - -- - 
    '==
    '==
    '==   >> 3431.0515=  15-may--2018=
    '==    -- Sales- Warn if Stock Qty low...
    '==
    '==
    '==   V3.5.3501.0702
    '==        --  NOW IS Mdi CHILD..  02July2018-  Drop all Hold/Restore Buttons.
    '==   V3.5.3501.0715
    '==        --  Drop all Hold/Restore Subs..  Move F6 And CashDrawer stuff to MDi Mother.
    '=       
    '== 
    '==   v3.5.3501.0725=  25-July-2018=
    '==  --  Sales-  for Non-Stock items..  do not warn of low stock, and do not update stock balance.
    '==  --  Sales Window..  Quote trans. should not have, or stop on, Payment Box.
    '==  --  Add public sub to retrieve current Sale Staff Signon. 
    '== 
    '==   >> 3501.0731=  31-July-2018=
    '==      --  AGAIN !! Fixes for Sales logic:  Bypass low stock warning for NonStock Items
    '==               AND Quotes are not to ask for SerialNos..
    '==               AND  Quote trans. should not have, or stop on, Payment Box.
    '==      --  Add "About" menu to POS main to show WhatsNew page..
    '== 
    '==
    '== -- Updated 3501.0809  09August2018=  
    '==     -- Use Combined File/Sql subs module...
    '==       Incl.  Fixes to modAllFileAndSqlSubs to Get correct appname for LocalDataDir..
    '== 
    '==
    '==  Updated   '=3501.0824=   24-Aug-2018.
    '==     dgvPaymentDetails-- catch enter with keydown.
    '==         -- If Payments balance debits.. Move Focus onto Commit
    '==
    '==  Updated   '=3501.0916=   16-Sep-2018.
    '==     >>  Drop msgbox confirming sale commitment.
    '==
    '==  Updated   '=3501.0920=   20-Sep-2018.
    '==     >>  Add staff_id to F5 New Cust call...
    '==
    '== -- Updated 3501.1029  24/29-Oct-2018=  
    '==     -- Fix to POS Main.. Move "Show Last Trans. to Till-Dropdown Menu on Main top toolstrip"...
    '==          AND add CashUp and Change-Till to dropdown menu.
    '==     -- Fix to POS Sales.. 
    '==            Do NOT Allow Sale Commit while there is no sale Items AND no Payments
    '==     -- Fix to POS Sales..
    '==         For Refund-  Disable "On Account" Checkbox..
    '==     -- Commit- ADD Flag to lock Commit code in case of multiple clicks..
    '==
    '--    NEW BUILD-
    '==       3519.0117= 17-Jan-2019=
    '==     --  mDecPaymentNettCredited APPLIES to what is credited to current SALE value.
    '--            So it excludes Change, and excludes Change saved as Credit Note..
    '==     --  Fix Sale- nettCredited mustn't include PaymentCredited as credit note...
    '==     --  Fix Sale- Make "ChargeToAccount"=true (now two copies) the initial value for Account Cust. Sales.
    '==                 and make TABSTOP=false for Payments Grid when "ChargeToAccount" is checkek....
    '==
    '==
    '==   Updated.- 3519.0217 17-Feb-2019= =
    '==     -- For Sale class "clsPOS34Sale" 
    '==            make sure that "mLabSaleChargeBalance" start off Trans. as .Visible = True
    '==            So it doesn't disappear after doing a Refund. 
    '==
    '==   Updated.- 3519.0219  Started 18-Feb-2019= 
    '==     -- Fixes to Laybys.. 
    '==         - clsPos34Sale- Setup Cust. Allow to choose sale/layby after not delivering Layby..
    '==         - Add code frmlayby to actually Cancel Layby if requested...
    '==
    '==   Updated.- 3519.0221  Started 21-Feb-2019= 
    '==     -- Fixes to Various modules and forms to allow optional A4 Invoice printing on NON-account Sale... 
    '==
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==     Sales (clsPOS34Sale)-  
    '==       -- Looking up Customers- make special Sql-Select for Browser 
    '==              to make a column of [lastName, firstName] in browser Grid.  
    '==    -- MAJOR-  Add TextBox to Payments panel- (inside new GroupBox.)
    '==            for User to decide on Amount of CreditNote to withdraw to pay for Sale.
    '==          ALSO- formBorderStyle  is now fixedToolWindow..
    '==
    '==   Updated.- 3519.0404  Started 30-March-2019= 
    '==    -- Sales- Add Trans. button and code to "Convert" a Quote into a sale.
    '==              ie.. Allow import of Quote items into sale. and ask for SerialNos where needed.
    '==    -- =3519.0404=  SALE with Not-instock Serial-  Confirm possibly wrong serial.
    '==
    '==   Updated.- 3519.0414  Started 12-April-2019= 
    '==    -- SALES-  Warn operator if Account Cust has outst. OVERDUE amounts (30+ days). 
    '==    -- SALES-  Make sure that %-calculated discount/discountTax are rounded to 2 decimals.. 
    '==              AND in Account Payments round off invoice amount to 2 digits when comparing with payment.
    '==    -- SALES- Selling out Layby..  Include any discount prev. set on Layby ..
    '==
    '== - - - - RELEASED as 3519.0414 --
    '==  
    '==  
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==
    '==    Build- 4201.0424 - 24-April-2019. 12:48pm
    '==    Build- 4201.0424 - 24-April-2019. 12:48pm
    '==    Build- 4201.0424 - 24-April-2019. 12:48pm
    '==    
    '==    -- 4201.0424. TDI Child forms now converted to UserControls....
    '==    -- 4201.0528.  Made Layby's  into Child User Control. 
    '==            So we can't call the form for user to choose layby..
    '==             So we make a menu in-line for user to choose..
    '==
    '== NEW revision-
    '==    -- 4201.0707/0708.  Started 05-July-2019-
    '==       -- Add file->Local Preference for LOCAL Re-ordering of payment Details.
    '==       -- Payments and Sales-  Use clsPaymentTypes to get PaymentDetails for Grid. .
    '==       -- Selling Quote- Fix Error report when serial was bypassed.. .
    '==       -- New Quote- Importing pold quote-  don't ask for serails !!.. .
    '==       -- mDgvSalePaymentDetails.CurrentCell = mDgvSalePaymentDetails.Rows(0).Cells(1)  '-start at 1st amount.
    '==    -- 4201.0717.  17-July-2019-
    '==       -- Add code to do Invoice Rounding for CASH only. 
    '==              USE "gIntGetRoundingCents" to do rounding calc.
    '==
    '== NEW revision-
    '==    -- 4201.0727.  Started 25-July-2019-
    '==      -- "gbCollectCustomerInvoices"- 
    '==               MUST Round the Invoice and Tax totals to take 3 decimals down to 2.ie Round to whole cents..
    '==                And SAME for "gbGetCreditNoteHistory"-  Each CreditNote item must be rounded as it is collected..
    '==      -- For SALES with discount..  Fix Discount-Percent IndexChanged , and "mbShowTaxSplit"- 
    '==               to  ROUND the discount Tax (NOT TRUNCATE)
    '==                  to take any with 3 decimals down to 2.ie Round to whole cents..
    '==            NB:  TAX on discount MUSTn't go negative (ie) when SALE items are NON-TAXABLE !!!
    '==            NB:  TAX on discount MUSTn't go negative (ie) when SALE items are NON-TAXABLE !!!
    '==            NB:  TAX on discount MUSTn't go negative (ie) when SALE items are NON-TAXABLE !!!
    '==            NB:  TAX on discount MUSTn't go negative (ie) when SALE items are NON-TAXABLE !!!
    '==      -- ALSO For SALES with discount..  Fix Spurious Popup msg re discount being re-set..- 
    '==
    '==
    '== NEW revision-
    '== NEW revision-
    '==
    '==    -- 4201.1007.  07-Oct-2019-  Started 07-Oct-2019-
    '==        -- Payments Grid- Fix crash on Enter Key if currently over-paid....
    '==        -- Sales Qty.. Enforce Integral quantity unless IsNonStockItem.....
    '==
    '==
    '== NEW revision to fix previous.-
    '==
    '==    -- 4201.1013.  13-Oct-2019..
    '==        -- SALE of IsNonStockItem.. Allowing fractions fails when item move from Grid back up to edit line...
    '==              In "dgvSaleItems_CellContentClick"   SAVE IsNonStockItem for edit.
    '==
    '== NEW revision-
    '== NEW revision-
    '==
    '==  -- 4201.1028/1031.  28-Oct-2019-  
    '==     -- MAJOR- Payments Frame- DISABLE grpBoxSalePayments for On-Account Sales !!....
    '==          Acompanying payments with On-Account Sales are no longer permitted.
    '==     -- ENABLE Cancel button after Staff code entered.. ???????????....
    '==     -- Commit- Allow emailing of Invoice for Non-account Sales.
    '==     -- Sale- Now has five-cent Rounding of Sell Price in Item EDit Line.
    '==     -- Commit Sale- Payment Disbursement Line Amount MUST include Credit-Note amt used (wdl).
    '==     -- Importing Quote-- MUST use Quoted Selling price...
    '==
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1129.  29-Nov-2019-  Started 06-November-2019-
    '==      --  New Quotes- "clsPOS34Sale". Fix problem making new Quote..  (Stewart 19/11/2019) 
    '==                User can't navigate back to add items once the Discount section has been arrived at.
    '==                 -- FIX- txtSaleDiscountCashout_Validated=
    '==                    --  Don't SELECT next control if it is a quote--
    '==
    '==
    '==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
    '==    
    '==   == 4221.0207.  05-Feb-2020- 
    '==   -- Tags- 07-Feb-2020.. For Build 4221..
    '==         -- Show Customer Tags on Sale form when Customer selected...
    '==   -- "chkOnAccount" 07-Feb-2020.. clsPOS34sale-  make chkOnAccount UNCHECKED as the DEFAULT..
    '==          Must behave as cash sale to start with.
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==
    '== 
    '==  MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
    '==       --  Involves creating a REFUND DETAILS EXTRA Option (radioButton) to be able to refund same types as Payments..
    '==                 ie dropdown including ZipPay, bank Deposit etc..
    '==             NOTE- Sales form will keep Refund Options Frame for continuity of process,
    '==                   allowing Refunds to Cash, CreditNote or EftPos as always..
    '==                BUT an extra Option (OTHER- Choose From List) will allow user to see a DropDown Combo
    '==                      of remaining keys from master List so User can choose ONE only..
    '==                ALSO TWO new columns to be added to Payments Table-  
    '==                      viz- "RefundOtherDetailAmount"(MONEY), and "RefundOtherDetailKey" (VARCHAR).
    '==                         to Cash/CreditNote/EftPosDr/EftPosCr already recorded in Payment record.
    '==
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== DEV PREP Updates to 4251.0606  Started 17-June-2020= 
    '== DEV PREP Updates to 4251.0606  Started 17-June-2020= 
    '==
    '==  Target is new Build 4253..
    '==  Target is new Build 4253..
    '==
    '==  Target-New-Build-4253..
    '==  Target-New-Build-4253..
    '==
    '==   1. MAIN REASON is FIXING implementation of EXTENDED Refund Details for REFUNDS..
    '==             -- ERROR-  If "Other" Payment Type is chosen, BUT NO combo list item was selected, 
    '==                     then BLANK Type is recorded in Payment Column..
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==
    '== --  Sale Form-  
    '==        The Customer Account information (outstanding amounts) shown on the Sales screen 
    '==                    do not yet take into account any reversed invoices for that customer..  
    '==                     Use clsDebtors to collect and show actual outstending only. 
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==  
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==   Target-New-Build-4277 -- (Started 07-October-2020)  
    '==
    '==   POS Sales Commit-  Warn if transaction is taking in more than $500.00 into Credit Note..
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
 


    Public Const K_ABSOLUTE_MAX_USERS_PERMITTED As Short = 32

    '=== Public Const K_SAVESETTINGSPATH As String = "localPOSSettings.txt"
    Private Const k_SALES_CHARGED_TO_ACCOUNT As String = _
                   "Note:  On-Account Sales are invoiced and fully charged to the Account..  " & vbCrLf & _
                    "Acompanying payments are disabled. A separate Account Payment can be used."

    '-- Sales DataGridView columns.--
    '=gone=Private Const k_GRIDCOL_DELETE As Short = 0
    Private Const k_GRIDCOL_BARCODE As Short = 0
    Private Const k_GRIDCOL_SERIALNO As Short = 1
    Private Const k_GRIDCOL_CAT1 As Short = 2
    Private Const k_GRIDCOL_CAT2 As Short = 3
    Private Const k_GRIDCOL_DESCRIPTION As Short = 4
    Private Const k_GRIDCOL_TAXCODE As Short = 5
    Private Const k_GRIDCOL_SELL_INC As Short = 6      '--Sell_ex from StockRecord PLUS Tax.
    Private Const k_GRIDCOL_SELLACTUAL_INC As Short = 7
    Private Const k_GRIDCOL_QTY As Short = 8
    Private Const k_GRIDCOL_SELLACTUAL_INC_EXTENDED As Short = 9
    '-- HIDDEN columns.--
    Private Const k_GRIDCOL_STOCK_ID As Short = 10           '-- HIDDEN column.--
    Private Const k_GRIDCOL_TRACK_SERIAL As Short = 11       '-- HIDDEN column.--
    Private Const k_GRIDCOL_SERIAL_AUDIT_ID As Short = 12       '-- HIDDEN column.--
    Private Const k_GRIDCOL_ISSERVICEITEM As Short = 13      '-- HIDDEN column.--
    Private Const k_GRIDCOL_COST_EX As Short = 14            '-- HIDDEN column.--
    Private Const k_GRIDCOL_COST_INC As Short = 15           '-- HIDDEN column.--
    Private Const k_GRIDCOL_SELL_EX As Short = 16            '-- HIDDEN column.--Sell_ex from StockRecord.
    Private Const k_GRIDCOL_SELLACTUAL_EX As Short = 17      '-- HIDDEN column.--
    Private Const k_GRIDCOL_SELLACTUAL_TAX As Short = 18             '-- HIDDEN column.--
    Private Const k_GRIDCOL_SELLACTUAL_EX_EXTENDED As Short = 19     '-- HIDDEN column.--
    Private Const k_GRIDCOL_SELLACTUAL_TAX_EXTENDED As Short = 20    '-- HIDDEN column.--

    '-- PAYMENTS DataGridView columns.--
    Private Const k_PAYGRIDCOL_PAYMENTTYPE_DESCR As Short = 0
    Private Const k_PAYGRIDCOL_AMOUNT As Short = 1
    Private Const k_PAYGRIDCOL_PAYMENTTYPE_ID As Short = 2

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = == 

    '--- For suppressing default popup menu..-
    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = =

    '=4201.0424- Parent Form is now a UserControl..
    Private mFrmSale As UserControl '= Form  '= frmPOS3Main  '-- !! TYPE HAS to change when host form changes.-

    '= Private msComputerName As String
    Private msMachineName As String = "" '--local machine--
    Private msComputerName As String = "" '--client or Fat machine--
    Private mbIsThinClient As Boolean = False

    Private msServer As String
    '=  Private msVersionPOS As String
    Private msDLLVersion As String
    Private msAppPath As String
    Private msCurrentUserName As String = ""  '=3301.1211= 11ec2016=

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    '= Private mlJobId As Integer = -1
    Private msSettingsPath As String = ""  '=4201.0708-

    '=3301.1210= Must have a Till..
    '== Private msCurrentCashDrawer As String = ""

    '--  Business Info-
    '--  Business Info-
    Private msBusinessABN As String
    Private msBusinessUser As String
    Private msJMPOS33SecurityIdOriginal As String '--as stored in SytemInfo in Row "JT2SecurityId"..-
    Private msJMPOS33SecurityId As String '-- AS computed from ABN DateCreated.  --
    Private msBusinessName As String
    Private msBusinessAddress1 As String
    Private msBusinessAddress2 As String
    Private msBusinessShortName As String
    Private msBusinessPhone As String
    Private msBusinessPostCode As String
    Private msBusinessState As String

    ''--  L i c e n c e --
    ''--  L i c e n c e --
    'Private mdDateCreated As Date
    'Private msLicenceKey As String = ""
    ''== Private msLicenceKeyLevel2 As String = ""
    'Private mbIsFullLicence As Boolean = False
    ''== 3072/3 == Private mbIsThreeUserLicence As Boolean = False
    'Private mbLicenceOK As Boolean = False
    'Private mIntMaxUsersPermitted As Integer = 0   '--none--
    'Private msJMPOS33_SecurityIdOriginal As String = ""
    'Private msJMPOS33_SecurityId As String = ""
    'Private mIntDatabaseDays As Integer = -1

    '= = = = 

    Private msMainStaffName As String = ""
    Private mIntMainStaff_id As Integer = -1
    Private mbStaffTimeoutSuspended As Boolean = False

    '= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo

    '= Private msColourPrtName As String = ""
    '= Private msReceiptPrtName As String = ""
    '= Private msLabelPrtName As String = ""
    Private msDefaultPrinterName As String = ""
    Private msPdfPrinterName As String = ""
    Private mbAllowEmailInvoices As Boolean = False

    Private mImageUserLogo As Image

    Private mColPrefsCustomer As Collection
    Private mColPrefsStock As Collection
    Private mColPrefsSupplier As Collection   '==3401.0308=
    Private mColPrefsStaff As Collection  '=3401.0308 -
    Private mColPrefsCategory1, mColPrefsCategory2, mColPrefsBrands
    '= = =  = = = = = = = =  = = = = = = = = = = = = = = = = ==  = ==

    Private mColStockImages As Collection
    Private msTransactionType As String = ""
    Private mbIsRefund As Boolean = False
    Private mbIsQuote As Boolean = False
    '--New Layby creating.
    Private mbIsLayby As Boolean = False
    Private mIntLayby_id As Integer = -1

    '= Delivering prev. Layby (now a SALE).
    Private mbIsDeliveringLayby As Boolean = False
    Private mIntDeliveredLayby_id As Integer = -1

    Private msPOS_account_terms As String = ""
    Private mDecSell_margin As Decimal = 10D    '--default value. get from setup.
    Private mDecGST_rate As Decimal = 10D    '--default. value-  get from setup.

    Private mIntPOS_labourStockId_pr1 As Integer = -1   '--default. value-  get from setup.
    Private mIntPOS_labourStockId_pr2 As Integer = -1   '--default. value-  get from setup.
    Private mIntPOS_labourStockId_pr3 As Integer = -1   '--default. value-  get from setup.
    Private mIntPOS_labourStockId_prQ As Integer = -1   '--default. value-  get from setup.
    Private mIntPOS_labourStockId_prH As Integer = -1   '--default. value-  get from setup.

    '== Private msItemBarcode As String
    Private mIntQtyInStock As Integer

    '== Sale ==
    '== Sale ==
    '== Sale ==
    Private mbIsCancelling As Boolean = False

    '-- Staff for this sale..
    '= Private msSaleStaffName As String = ""
    '= Private mIntSaleStaff_id As Integer = -1

    '- Customer--  SEE BELOW-
    '= Private msSaleCustomerBarcode As String = ""
    '= Private msSaleLastCustomerBarcode As String = ""

    Private mIntSaleCustomer_id As Integer = -1
    Private mbSaleIsAccountCust As Boolean = False
    Private mDecAccountCustCreditLimit As Decimal = 0  '==3301.529-
    Private mDecAccountCustLimitRemaining As Decimal = 0  '==3301.529-
    '- Non-account- Credit Notes.
    Private mDecCreditNoteCreditRemaining As Decimal = 0  '==3301.605-
    '=3403.917=
    Private msCustomerGrade As String = ""
    '=3519.0414=
    Private msAgedOverDueAmountsList As String = ""

    Private msCustomerEmail As String = ""
    Private mIntJob_id As Integer = -1
    '= 331.0107= Save Job Diagnosis as JobReport-
    Private msSaleJobReport As String = ""

    '-- Sale (Invoice) totals-
    Private mDecTotalTax As Decimal = 0
    Private mDecSubTotal As Decimal
    '==  3103.411 Add Columns 'subtotal_non_taxable', 'subtotal_taxable' to Invoice table.  -
    Private mDecSubTotalEx_non_taxable As Decimal
    Private mDecSubTotalEx_taxable As Decimal

    Private mDecTotalCostEx As Decimal
    Private mDecTotalCostInc As Decimal

    Private mDecDiscount As Decimal
    Private mDecDiscountTax As Decimal = 0
    '=3411.0405=
    Private msDiscountSelectedItem As String = ""  '--combo item..
    '= Private mDecCashout As Decimal

    Private mDecSubTotal2 As Decimal  '-after discount and cashout
    Private mDecRounding As Decimal
    Private mDecInvoiceTotal As Decimal  '--total Debits.-

    Private mDecPaymentTotalRcvd As Decimal
    Private mDecPaymentCashRcvd As Decimal
    Private mDecChange As Decimal
    Private mDecPaymentNettCredited As Decimal

    Private mDecChangeAsCreditNote As Decimal = 0
    Private mDecCreditNoteWdlAmount As Decimal = 0  '-- Credit Note contribution to sale..

    Private mDecBalance As Decimal = 0
    '=3519.0321= Warning for credit limit given
    Private mbCreditLimitWarningGiven As Boolean = False

    Private mIntCurrentPaymentTypeIndex As Integer = -1

    '== S A L E =
    '= Private msSaleComments As String = ""
    '= Private msSaleDeliveryInstr As String = ""

    '== Private mTxtBarcode As TextBox  '--text box calling stock lookup.-
    '== Private mIntGridRow, mIntGridCol As Integer


    '- Host Form Controls..
    '- H o s t  F o r m  C o n t r o l s..
    '- H o s t  F o r m  C o n t r o l s..
    '- H o s t  F o r m  C o n t r o l s..

    Private mToolTip1 As ToolTip

    '= Private mTabControlMain As TabControl   '=3401.319= ONLY if WE INSIDE JobMatix-
    '= Private mTabControlPOS As TabControl   '=3401.319=

    Private mPanelSaleInvoiceList As Panel
    '-- Private mListViewSaleInvoices As ListView
    '==3107.922= Private mDgvSaleInvoices As DataGridView

    '==3107.922= Now is cmd Button to show invoices..
    Private mBtnSaleSelectInvoice As Button
    Private mBtnSaleSelectQuote As Button '= 3401.322= and QUOTEs..

    '=3403.516= labSaleAccountInfo--
    Private mLabSaleAccountSalesInfo As Label

    '= 3401.322=  Private mOptSaleInvoiceList As RadioButton
    '= 3401.322=  Private mOptSaleQuotesList As RadioButton

    Private mPanelOptTranType As Panel
    '=DEFAULT= Private mLabChooseTrans As Label
    '-- TILL-  3301.1212=
    Private mLabSaleTillId As Label

    Private mLabSaleTranType As Label
    '= Private mBtnSaleContinue As Button

    '=3301.427= Restore- but STILL the DEFAULT= 
    Private mOptSaleSale As RadioButton
    Private mOptSaleRefund As RadioButton
    Private mOptSaleQuote As RadioButton
    Private mOptSaleLayby As RadioButton
    '== Private mOptSaleCreditNote As RadioButton  '=3301.525=

    '=3519.0330--
    '= Private mBtnImportQuote As Button
    Private mLabImportQuote As Label

    '=3401.321= Replaces the opt Radio Buttons.
    '= Private mCboTransaction As ComboBox

    Private mPanelSaleHdr As Panel
    Private mLabSaleJobDelivery As Label
    Private mTxtSaleJobNo As TextBox

    Private mLabSaleCust As Label
    Private mTxtSaleCustBarcode As TextBox
    Private mTxtSaleCustName As TextBox
    '=4221.0207-
    Private mLabSaleCustTags As Label

    '== 3303.0117= Private mLabSaleAvailCredit As Label
    Private mListViewSaleAvailCredit As ListView

    '= 3401.321= Now in with Custname-
    '--  Private mTxtSalePricingGrade As TextBox

    Private mPicSaleItem As PictureBox

    Private mDgvSaleItems As DataGridView

    Private mPanelSaleFooter As Panel
    '=3403.730= Private mTxtSaleDelivery As TextBox
    '=3403.730= Private mTxtSaleComments As TextBox
    '=3403.730= 
    Private mBtnSaleComments As Button
    Private mPanelSaleTotals As Panel    '==3301.525=

    Private mPanelPayment As Panel
    Private mLabSalePayments As Label
    Private mDgvSalePaymentDetails As DataGridView

    Private mTxtSalePaymentBalance As TextBox
    Private mLabSaleCrBal As Label

    '== Private mChkSaleChargeBalance As CheckBox
    Private mLabSaleChargeBalance As Label

    Private mLabSaleChange As Label
    Private mTxtSaleChange As TextBox

    '-is on Account.  (two copies on the form.)
    Private mChkOnAccount As CheckBox
    Private mChkOnAccount2 As CheckBox


    '==3301.525- Choose Refund type-
    Private mGrpBoxRefundType As GroupBox
    Private mOptRefundCash As RadioButton
    Private mOptRefundCredit As RadioButton
    '=3401.327=  Add EftPos refund..
    Private mOptRefundEftPosDr As RadioButton
    Private mOptRefundEftPosCr As RadioButton
    '==  Target is new Build 4251..
    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
    Private mOptRefundOther As RadioButton
    Private mCboRefundOtherDetails As ComboBox
    '== END Target is new Build 4251..

    Private mBtnDiscountPC As Button
    Private mCboSaleDiscountPercent As ComboBox

    Private mTxtSaleTotalTax As TextBox
    Private mTxtSaleSubTotal As TextBox

    Private mLabSaleDiscount As Label
    Private mTxtSaleDiscount As TextBox
    Private mTxtSaleDiscountAnalysis As TextBox

    '= Private mLabSaleCashout As Label
    '= Private mTxtSaleCashout As TextBox
    Private mTxtSaleSubTotal2 As TextBox
    Private mTxtSaleNettTax As TextBox
    Private mTxtSaleRounding As TextBox

    Private mLabSaleInvTotal As Label
    Private mTxtSaleTotal As TextBox

    Private mLabSaleHelp As Label
    Private mLabSaleHelp2 As Label

    Private mBtnCancelSale As Button
    Private mBtnCancelSale2 As Button
    Private mBtnCommitSale As Button

    '= Private mLabSaleDLLversion As Label

    '=3301.516= textBox controls for Sale Item Line Entry..
    '--  Now all done with line of text boxes above Grid..
    '--  Grid is now Read-only.
    Private mPanelSaleLineEntry As Panel

    '==Private mLabSaleItemNo As Label
    Private mTxtSaleItemBarcode As TextBox
    Private mTxtSaleItemSerialNo As TextBox
    '=3401.313=  Private mTxtSaleItemCategory As TextBox
    Private mTxtSaleItemDescription As TextBox
    Private mTxtSaleItemSellPrice As TextBox
    Private mTxtSaleItemQty As TextBox
    Private mTxtSaleItemExtension As TextBox

    Private mBtnSaleLineOk As Button
    '= 3401.0309--
    Private mBtnSaleItemLineClear As Button

    '= 3401.0309--
    '-- CONTROLs for HOLDING/RESTORING transactions..
    'Private mPanelSalesCurrentHdr As Panel
    'Private mLabSaleTranActive As Label
    ''--
    'Private mPanelSaleTranHeld1 As Panel
    'Private mBtnSaleRestore1 As Button
    'Private mLabHeld1Info As Label
    ''--
    'Private mPanelSaleTranHeld2 As Panel
    'Private mBtnSaleRestore2 As Button
    'Private mLabHeld2Info As Label
    '-- END of CONTROLs for HOLDING transactions..
    '-- END of CONTROLs for HOLDING transactions..
    '-
    '= 3401.0309--
    '-- Now Staff must id for each transaction..
    Private mTxtSaleStaffBarcode As TextBox
    Private mLabSaleStaffName As Label
    '= Private mBtnSaleHold As Button
    '==
    '==   Updated.- 3519.0317..
    '==    -- MAJOR-  Add TextBox to Payments panel- 
    '==            for User to decide on Amount of CreditNote to withdraw to pay for Sale.
    Private mTxtCreditNoteWdl As TextBox
    Private mGrpBoxSalePayments As GroupBox
 

    '=3301.516= MORE ON textBox controls for Sale Item Line Entry..

    '-- END of Main Form CONTROLs to be seen from here..
    '-- END of Main Form CONTROLs to be seen from here..

    '-- Current Item Edit..
    '-- Current Item Edit..
    '-- Current Item Edit..
    '== Private mIntCurrentEditRow As Integer = -1  '- if any-
    Private mIntStock_id As Integer = -1
    Private mbIsSerialItem As Boolean = False
    '=4201.1007-
    Private mbIsNonStockItem As Boolean = False

    Private mDatatableEditingItem As DataTable  '--stock table row(0) current edit..
    Private mIntSerialAudit_id As Integer = -1
    '-cost-
    Private msGoodsTaxcode As String = ""
    Private mDecCostExTax As Decimal = 0
    Private mDecCostIncTax As Decimal = 0

    Private msSalesTaxCode As String = ""
    Private mDecSellActualExTax As Decimal = 0
    Private mDecSellActualTaxAmount As Decimal = 0
    Private mDecSellActualIncTax As Decimal = 0
    Private mDecSellItemQty As Decimal = 0

    '-- END current Item..
    '= = = = = = = = = = = = = = = = = = = = = 

    Private mIntDgvSalesItemsCurrentRow As Integer = -1
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==  =
    '-===FF->

    '== S A L E =

    '-- Saved Screen Controls Contents and Sale Context..
    '--   To be restored to Screen (on RESTORE)..
    Private mbIsRestoringScreen As Boolean = False

    '-- Staff for this sale..
    Private msSaleStaffbarcode As String = ""
    Private msSaleStaffName As String = ""
    Private mIntSaleStaff_id As Integer = -1

    Private msSaleCustomerBarcode As String = ""
    Private msSaleLastCustomerBarcode As String = ""

    Private msSaleCustomerName As String = ""
    Private msSaleCustomerGrade As String = ""
    '- both-
    Private msSaleCustomerInfo As String = ""

    '-- save Credit info from ListView..
    Private mColCustomerCreditInfo As Collection
    '-Save mListViewSaleAvailCredit.Height-
    Private mIntSavedListViewSaleAvailCredit_height As Integer = 0

    '-- Save Sales Items Info from dgvItems.
    Private mColCustomerSalesItemsInfo As Collection

    '-- Save Payments Items Info from dgvItams.
    Private mColCustomerSalePaymentsInfo As Collection

    Private msSaleComments As String = ""
    Private msSaleDeliveryInstr As String = ""

    '=3403.1015=  Save "chkOnAccount (Charge to Account)" value.
    Private mSaleChkOnAccountEnabled As Boolean = False
    Private mbSaleIsChargedToAccount As Boolean = False
    '
    '==  end of screen SAVE..====

    Private mIntListViewSaleAvailCredit_height As Integer
    '= = = = = = = = = = = = = = = = = = ==  = = = = = = = =
    '-===FF->

    '-- Numeric test..

    Private Function mbIsNumeric(ByVal sInput As String) As Boolean
        mbIsNumeric = False

        If IsNumeric(sInput) Then  '--good start-
            '-  check for "+","-" that pass the isNumeric test, but fail in Sql Server. test.
            If (InStr(sInput, "+") <= 0) AndAlso (InStr(sInput, "+") <= 0) Then
                mbIsNumeric = True
            End If
        End If  '-numeric-
    End Function  '-is numeric-
    '= = = = = = = = = = =  = = = = =

    '=4201.1030-  Borrowed From Stock Admin.

    Private Function mDecGetRoundingAmount(ByVal decAmountToBeRounded As Decimal) As Decimal
        '-  Compute Rounding..--
        Dim decRounding As Decimal = 0
        Dim intCentsRounding As Integer = 0
        Dim intCents1 As Integer
        intCents1 = (decAmountToBeRounded * 100) Mod 10  '--get original cents.
        Select Case intCents1
            Case 1, 6 : intCentsRounding = -1
            Case 2, 7 : intCentsRounding = -2
            Case 3, 8 : intCentsRounding = 2
            Case 4, 9 : intCentsRounding = 1
        End Select
        decRounding = (intCentsRounding / 100)   '== make 0.0d  --
        mDecGetRoundingAmount = decRounding

    End Function  '-mIntGetRoundingCents-
    '= = = = = = = = =  = = = = = = = = = =
    '-===FF->

    '-- private support subs--

    '-mbIsOnAccount-
    Private Function mbIsOnAccount() As Boolean

        mbIsOnAccount = mChkOnAccount.Checked

    End Function  '-mbIsOnAccount-
    '= = = = = = = = = = = = = = =

    Private Function mDecComputeAmountExTax(ByVal decGrossAmount As Decimal) As Decimal
        Dim decAmountEx As Decimal

        '= mDecComputeAmountExTax = Decimal.Truncate((decGrossAmount * (100 / (100 + mDecGST_rate))) * 100) / 100

        '- eg.. for 10% gst, we compute 10/11ths of _inv value to get ex value..
        decAmountEx = (decGrossAmount * (100 / (100 + mDecGST_rate)))

        decAmountEx = Math.Round(decAmountEx, 2, MidpointRounding.AwayFromZero)
        mDecComputeAmountExTax = decAmountEx

    End Function '-- mDecComputeAmountExTax-
    '= = = = = = = = = = = =  = = = == = ==

    Private Function mbShowTaxSplit() As Boolean
        Dim decAmountExTax As Decimal

        mTxtSaleDiscountAnalysis.Text = ""
        decAmountExTax = mDecComputeAmountExTax(mDecDiscount)
        '=4201.0727-- Do ROUNDING..  Just in case...
        decAmountExTax = Math.Round(decAmountExTax, 2, MidpointRounding.AwayFromZero)

        mDecDiscountTax = mDecDiscount - decAmountExTax  '= mDecComputeAmountExTax(mDecDiscount)
        '==4201.0727-
        '=  NB:  TAX on discount MUSTn't go negative (ie) when SALE items are NON-TAXABLE !!!
        If (mDecDiscountTax > mDecTotalTax) Then
            mDecDiscountTax = mDecTotalTax
        End If
        '-- show discount/tax split.
        If mDecDiscount > 0 Then
            mTxtSaleDiscountAnalysis.Text = _
             "$" & FormatNumber((mDecDiscount - mDecDiscountTax), 4) & "/" & FormatNumber(mDecDiscountTax, 4)
            '= FormatCurrency((mDecDiscount - mDecDiscountTax), 2) & "/" & FormatCurrency(mDecDiscountTax, 2)
        End If '-zero-

    End Function '-mbShowTaxSplit-
    '= = = = = = = = = = = ==
    '-===FF->

    'Private Function msGetDllversion() As String
    '    Dim assemblyThis As Assembly
    '    Dim assName As AssemblyName
    '    Dim s1, sVersion As String

    '    msAppPath = My.Application.Info.DirectoryPath
    '    If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
    '    gsAppPath = msAppPath
    '    '==msAppPath = sApppath
    '    '-  new log each month..-
    '    s1 = VB.Format(Now, "yyyy-MM-dd")
    '    gsErrorLogPath = gsJobMatixLocalDataDir("Jobmatix34") & "\JMxPOS340-Runtime-" & VB.Left(s1, 7) & ".log"
    '    gsRuntimeLogPath = gsErrorLogPath  '--gsAppPath & "JTv3_Runtime.log"

    '    assemblyThis = System.Reflection.Assembly.GetExecutingAssembly()
    '    assName = assemblyThis.GetName
    '    With assName.Version
    '        sVersion = CStr(.Major) & "." & CStr(.Minor) & "." & CStr(.Build) & "." & Format(.Revision, "0000")
    '    End With

    '    msGetDllversion = sVersion
    'End Function  '--get version-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- get latest systemInfo.--

    Private Function mbRefreshSystemInfo() As Boolean
        '== Dim colSystemInfo As Collection
        Dim s1 As String

        '==3301.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        If mSysInfo1.contains("POS_ACCOUNTTERMS") Then
            msPOS_account_terms = mSysInfo1.item("POS_ACCOUNTTERMS")
        End If
        If mSysInfo1.contains("POS_SELL_MARGIN") Then
            s1 = mSysInfo1.item("POS_SELL_MARGIN")
            If IsNumeric(s1) Then
                mDecSell_margin = CDec(s1)
            End If
        End If
        If mSysInfo1.contains("GSTPercentage") Then
            s1 = mSysInfo1.item("GSTPercentage")
            '-mdecGST_percentage
            If IsNumeric(s1) Then
                mDecGST_rate = CDec(s1)
            End If
        End If
        '-- labour rates..
        If mSysInfo1.contains("POS_LABOURSTOCKID_PR1") AndAlso _
                    IsNumeric(mSysInfo1.item("POS_LABOURSTOCKID_PR1")) Then
            mIntPOS_labourStockId_pr1 = CInt(mSysInfo1.item("POS_LABOURSTOCKID_PR1"))
        End If  '-pr1-
        If mSysInfo1.contains("POS_LABOURSTOCKID_PR2") AndAlso _
                      IsNumeric(mSysInfo1.item("POS_LABOURSTOCKID_PR2")) Then
            mIntPOS_labourStockId_pr2 = CInt(mSysInfo1.item("POS_LABOURSTOCKID_PR2"))
        End If  '-pr1-
        If mSysInfo1.contains("POS_LABOURSTOCKID_PR3") AndAlso _
                      IsNumeric(mSysInfo1.item("POS_LABOURSTOCKID_PR3")) Then
            mIntPOS_labourStockId_pr3 = CInt(mSysInfo1.item("POS_LABOURSTOCKID_PR3"))
        End If  '-pr1-
        If mSysInfo1.contains("POS_LABOURSTOCKID_PRQ") AndAlso _
                      IsNumeric(mSysInfo1.item("POS_LABOURSTOCKID_PRQ")) Then
            mIntPOS_labourStockId_prQ = CInt(mSysInfo1.item("POS_LABOURSTOCKID_PRQ"))
        End If  '-pr1-
        If mSysInfo1.contains("POS_LABOURSTOCKID_PRH") AndAlso _
                      IsNumeric(mSysInfo1.item("POS_LABOURSTOCKID_PRH")) Then
            mIntPOS_labourStockId_prH = CInt(mSysInfo1.item("POS_LABOURSTOCKID_PRH"))
        End If  '-pr1-
        '==3301.428= IEnd If  '-load sys info--

        '==3301.1114= For Licence Key=
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        msBusinessUser = mSysInfo1.item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        msBusinessAddress1 = mSysInfo1.item("BUSINESSADDRESS1")
        msBusinessAddress2 = mSysInfo1.item("BUSINESSADDRESS2")
        msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
        msBusinessState = mSysInfo1.item("BUSINESSSTATE")
        msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
        msBusinessPhone = mSysInfo1.item("BUSINESSPHONE")

    End Function  '-mbRefreshSystemInfo-
    '= = = = = = = = = = = =  = = = == = ==
    '-===FF->

    '-- Execute SQL Command..--
    '-- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Private Function mbExecuteSql(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As OleDbTransaction) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbExecuteSql = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            '= mCnnSql.ChangeDatabase(msSqlDbName)
            intAffected = sqlCmd1.ExecuteNonQuery()
            mbExecuteSql = True   '--ok--
            '== MsgBox("Sql exec ok. " & intAffected & " records affected..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbExecuteSql: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            '= msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    '---subroutine create table --
    '---subroutine create table --
    Private Function mbDb_createTable(ByRef cnnSQL As OleDbConnection, _
                                       ByVal sTableName As String, _
                                       ByVal sCreate As String, _
                                       ByVal sCreateLogPath As String) As Boolean
        Dim bOk As Boolean
        Dim L1 As Integer
        Dim sErrorMsg As String

        Call gbLogMsg(sCreateLogPath, "Creating SQL Table:  '" & sTableName & "'..")
        Call gbLogMsg(sCreateLogPath, "SQL is:  " & sCreate)
        '==gAdvise "Creating SQL table '" + sTableName + "'.."
        bOk = gbExecuteCmd(cnnSQL, sCreate, L1, sErrorMsg)
        If Not bOk Then
            Call gbLogMsg(sCreateLogPath, "  Failed." & vbCrLf)
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg & vbCrLf)
            MsgBox("Table: " & sTableName & "-  Create failed.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
            '== iSqlErrors = iSqlErrors + 1
            '== gbCreateJobsDB = False
        Else '--ok--  add privileges--
            Call gbLogMsg(sCreateLogPath, "Table: " & sTableName & "-  created ok..")
        End If '--create ok-
        '==Return
        mbDb_createTable = bOk
    End Function '--create table..-
    '= = = = = = = = = = = = = = = =

    '- number grid rows..
    '--  show Grid row numbers..-

    Private Function mbNumberGridRows(ByRef dgv1 As DataGridView) As Boolean
        Dim rx As Integer

        If (dgv1.RowCount > 0) Then
            For rx = 0 To (dgv1.RowCount - 1)
                dgv1.Rows(rx).HeaderCell.Value = (rx + 1).ToString  '== CStr(rx + 1)
            Next rx
        End If
    End Function  '-- NumberGridRows --
    '= = = = = = = = = = 
    '-===FF->

    '-- Browse  table using --
    '--  Separate BROWSE33 FORM, (Includes TEXT SEARCH) and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    '== SPECIAL mbBrowseAndSearchTable for Customer Table.
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==       -- Looking up Customers- make special Sql-Select for Browser 
    '==              to make a column of [lastName, firstName] in browser Grid.  

    Private Function mbBrowseAndSearchCustomers(ByRef colPrefs As Collection, _
                                       ByRef sTitle As String, _
                                        ByRef sWhere As String, _
                                        ByRef colKeys As Collection, _
                                        ByRef colSelectedRow As Collection) As Boolean

        Dim frmBrowse1 As New frmBrowse  '--File: frmBrowse33 --
        Dim sSelectSql As String

        mbBrowseAndSearchCustomers = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = "Customer"  '=sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle

        '== make Special Select for Customer last/First Name combo.
        sSelectSql = " SELECT CASE WHEN (lastName='' ) THEN firstName "
        sSelectSql &= "  ELSE  lastName + ', ' + firstName "
        sSelectSql &= " END  AS CustomerName "
        '- add prefs.
        For Each sField As String In colPrefs
            'If (LCase(sField) <> "lastname") And (LCase(sField) <> "firstname") Then  '-we already have these.
            sSelectSql &= ", " & sField
            'End If
        Next sField
        sSelectSql &= " FROM dbo.customer "
        frmBrowse1.UserSelectList = sSelectSql
        '-test-
        '=  MsgBox("Select Sql is:  " & sSelectSql, MsgBoxStyle.Information)

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not (frmBrowse1.selectedRow Is Nothing) Then '= frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseAndSearchCustomers = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()

    End Function '-mbBrowseAndSearchCustomers
    '= = = = = = = = = = 
    '-===FF->

    '=3411.0313=
    '-- Browse  table using --
    '--  Separate BROWSE33 FORM, (Includes TEXT SEARCH) and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseAndSearchTable(ByRef colPrefs As Collection, _
                                           ByRef sTitle As String, _
                                            ByRef sWhere As String, _
                                            ByRef colKeys As Collection, _
                                            ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Customer") As Boolean
        Dim frmBrowse1 As New frmBrowse  '--File: frmBrowse33 --

        mbBrowseAndSearchTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        'If bHideEditButtons Then  '=3403.715- Default has changed-
        '    frmBrowse1.lookupSelection = True
        '    frmBrowse1.HideEditButtons = True
        'Else '--need to edit..
        '    frmBrowse1.lookupSelection = False
        '    frmBrowse1.HideEditButtons = False
        'End If
        'frmBrowse1.lookupSelection = True

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not (frmBrowse1.selectedRow Is Nothing) Then '= frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseAndSearchTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()


    End Function  '-mbBrowseAndSearchTable-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Browse  table using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseTable(ByRef colPrefs As Collection, _
                                    ByRef sTitle As String, _
                                      ByRef sWhere As String, _
                                      ByRef colKeys As Collection, _
                                      ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Customer", _
                                         Optional ByVal bHideEditButtons As Boolean = False) As Boolean
        Dim frmBrowse1 As New frmBrowsePOS

        mbBrowseTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        If bHideEditButtons Then  '=3403.715- Default has changed-
            frmBrowse1.lookupSelection = True
            frmBrowse1.HideEditButtons = True
        Else '--need to edit..
            frmBrowse1.lookupSelection = False
            frmBrowse1.HideEditButtons = False
        End If
        frmBrowse1.lookupSelection = True

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()
    End Function '--browse.--
    '= = = = = = = = = = = = = = =
    '-===FF->

    '=3411.0103=
    '-- Show Stock Picture if any--
    '-- Row1 is from Stock Table SQL..

    Private Function mbShowStockPicture(ByRef row1 As DataRow) As Boolean

        Dim image1 As Image
        Dim yBinaryData() As Byte
        If mColStockImages.Contains(CStr(mIntStock_id)) Then  '--we saved it.-
            image1 = mColStockImages.Item(CStr(mIntStock_id))
            mPicSaleItem.Image = image1
        Else  '-dig it out of the datatable.
            If Not IsDBNull(row1.Item("productPicture")) Then
                yBinaryData = row1.Item("productPicture") '==mRstEdit.Fields(sFldName).Value
                Try
                    '--- load picture from byte array..-
                    Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(yBinaryData)
                    image1 = System.Drawing.Image.FromStream(ms)
                    mPicSaleItem.Image = image1
                    ms.Close()
                    '--  save the image in static image collection..
                    mColStockImages.Add(image1, CStr(mIntStock_id))
                Catch ex As Exception
                    MsgBox("Failed to load image from stock table.. " & vbCrLf & _
                                      "Error: " & ex.Message)
                End Try
            Else  '- no image avail.
                mPicSaleItem.Image = Nothing
            End If
        End If  '--contains-
        '-- end new Pic stuff.

    End Function  '-mbShowStockPicture-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--show/print invoice-
    '= 3311.226=  Add Selected printer names-

    Private Function mbShowInvoice(ByVal intInvoice_Id As Integer, _
                                   ByVal sTranType As String, _
                                   Optional ByVal bCaptureInvoicePDF As Boolean = False, _
                                     Optional ByVal bPrintInvoiceAnyway As Boolean = False, _
                                     Optional ByVal bReallyWantsA4Invoice As Boolean = False, _
                                       Optional ByVal sSelectedInvoicePrinterName As String = "",
                                          Optional ByVal sSelectedReceiptPrinterName As String = "") As Boolean
        Dim frmShowInvoice1 As frmShowInvoice
        Dim bIsQuote As Boolean = (LCase(sTranType) = "quote")
        Dim bIsLayby As Boolean = (LCase(sTranType) = "layby")

        frmShowInvoice1 = New frmShowInvoice
        frmShowInvoice1.connectionSql = mCnnSql
        frmShowInvoice1.InvoiceNo = intInvoice_Id
        frmShowInvoice1.isQuote = bIsQuote
        frmShowInvoice1.islayby = bIsLayby
        '-- can use main signon- mIntMainStaff_id -
        If (mIntSaleStaff_id <= 0) Then
            frmShowInvoice1.Staff_id = mIntMainStaff_id  '- no current sale login.-
        Else  '-ok- current sale.
            frmShowInvoice1.Staff_id = mIntSaleStaff_id
        End If
        '=If bCaptureInvoicePDF Then
        frmShowInvoice1.CaptureInvoicePDF = bCaptureInvoicePDF  '--capture pdf for email..
        frmShowInvoice1.PrintInvoiceAnyway = bPrintInvoiceAnyway  '--if checked..
        frmShowInvoice1.A4InvoiceRequested = bReallyWantsA4Invoice
        '= End If
        frmShowInvoice1.UserLogo = mImageUserLogo
        '== ADDED 3401.319=
        frmShowInvoice1.selectedInvoicePrinterName = sSelectedInvoicePrinterName
        frmShowInvoice1.selectedReceiptPrinterName = sSelectedReceiptPrinterName

        frmShowInvoice1.ShowDialog()

    End Function  '--show invoice..-
    '= = = = = = = = = = = = = = = =

    '=3411.0208= 
    '- - get last sale Invoice..

    Private Function mbShowLastSaleInvoice()
        Dim sSql, sTranType As String
        Dim intInvoice_id As Integer
        Dim dataTable1 As DataTable

        sSql = "SELECT TOP (1) invoice_id,transactionType FROM dbo.Invoice "
        sSql &= " WHERE (transactionType IN ('sale','refund'))"
        sSql &= " ORDER BY invoice_id DESC;"

        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            MsgBox("Error in getting recordset for Invoice/SalesOrder/Layby table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        Else
            If Not (dataTable1 Is Nothing) AndAlso dataTable1.Rows.Count > 0 Then
                Dim row1 As DataRow = dataTable1.Rows(0)
                intInvoice_id = row1.Item("invoice_id")
                sTranType = row1.Item("transactionType")
                Call mbShowInvoice(intInvoice_id, sTranType)
            Else
                MsgBox("No Transaction to show..", MsgBoxStyle.Exclamation)
            End If  '-nothing-
        End If  '-get-
    End Function  '-mbShowLastSaleInvoice-
    '= = = = = =  = = = = = = ==  =
    '-===FF->

    '-- load invoice list.-
    '= 3107.922=  
    '- Updated 3301.601 (01June2016).
    '- JUST return the dataTable.-

    Private Function mbLoadInvoiceList(ByVal intCustomerId As Integer, _
                                       ByRef dataTable1 As DataTable, _
                                       ByVal bIsQuotes As Boolean, _
                                       Optional ByVal bIsLaybys As Boolean = False) As Boolean
        '-  load list of invoices for this cust..--
        Dim sSql, s1 As String
        Dim rx As Integer
        Dim sDateColumn, sIdColumn As String
        '-- re-build columns.
        mbLoadInvoiceList = False

        sIdColumn = "invoice_id"
        sDateColumn = "invoice_date"
        sSql = "SELECT * FROM dbo.Invoice "
        sSql &= " WHERE (Customer_id=" & CStr(intCustomerId) & ") AND (transactionType IN ('sale','refund'))"
        sSql &= " ORDER BY invoice_id DESC;"
        If bIsQuotes Then  '= mOptSaleQuotesList.Checked Then '= mbIsQuote Then
            sSql = "SELECT * FROM dbo.SalesOrder "
            sSql &= "   WHERE (Customer_id=" & CStr(intCustomerId) & ")  AND (transactionType ='quote')"
            sSql &= " ORDER BY SalesOrder_id DESC;"
            sIdColumn = "SalesOrder_id"
            sDateColumn = "SalesOrder_date"
        ElseIf bIsLaybys Then
            sSql = "SELECT * FROM dbo.layby WHERE (Customer_id=" & CStr(intCustomerId) & ")"
            sSql &= " ORDER BY Layby_id DESC;"
            sIdColumn = "layby_id"
            sDateColumn = "layby_date_started"
        End If
        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            MsgBox("Error in getting recordset for Invoice/SalesOrder/Layby table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        Else
            If Not (dataTable1 Is Nothing) Then
                mbLoadInvoiceList = True
            End If
        End If
        '- end invoices load..--
    End Function  '--mbLoadInvoiceList--
    '= = = = = = = = = = = = = = =
    '-===FF->

    '=3107.922- 
    '-- Now uses command button to pop up list of invoices.

    Private Sub SaleSelectInvoiceOrQuote(Optional ByVal bIsQuotes As Boolean = False, _
                                         Optional ByVal bIsLaybys As Boolean = False)
        Dim dataTable1 As DataTable
        Dim intInvoice_id, intRow As Integer
        Dim sTranType, sIdColumn As String

        If (mIntSaleCustomer_id > 0) Then
            '-- Use ListSelect to pop up list and select..
            If mbLoadInvoiceList(mIntSaleCustomer_id, dataTable1, bIsQuotes, bIsLaybys) Then
                If (dataTable1.Rows.Count > 0) Then
                    '-- show list-
                    '- select-
                    Dim frmListSelect1 As New frmListSelect
                    frmListSelect1.inData = dataTable1
                    frmListSelect1.hdrText = "Sales Invoices for " & mTxtSaleCustName.Text
                    frmListSelect1.Text = "Sales Invoices for " & mTxtSaleCustName.Text
                    sIdColumn = "invoice_id"
                    If bIsQuotes Then  '= mOptSaleQuotesList.Checked Then
                        frmListSelect1.hdrText = "Quotes for " & mTxtSaleCustName.Text
                        frmListSelect1.Text = "Quotes for " & mTxtSaleCustName.Text
                        sIdColumn = "salesorder_id"
                    ElseIf bIsLaybys Then
                        frmListSelect1.hdrText = "Layby's for " & mTxtSaleCustName.Text
                        frmListSelect1.Text = "Layby's for " & mTxtSaleCustName.Text
                        sIdColumn = "Layby_id"
                    End If
                    frmListSelect1.ShowDialog()
                    If frmListSelect1.cancelled Then
                        '= mbCancelled = True  '== sError = "selection cancelled."
                        frmListSelect1.Close()
                        Exit Sub
                    End If
                    '-get selected  row-
                    intRow = frmListSelect1.selectedRow
                    '- sent back index..
                    frmListSelect1.Close()
                    '-- then Show Invoice..-
                    intInvoice_id = dataTable1.Rows(intRow).Item(sIdColumn)
                    sTranType = dataTable1.Rows(intRow).Item("transactionType")
                    Call mbShowInvoice(intInvoice_id, sTranType)
                Else
                    If bIsQuotes Then
                        MsgBox("No Quotes to show.", MsgBoxStyle.Information)
                    ElseIf bIsLaybys Then
                        MsgBox("No Layby to show.", MsgBoxStyle.Information)
                    Else
                        MsgBox("No Invoices to show.", MsgBoxStyle.Information)
                    End If
                End If  '-count-
            End If  '=load-
        End If '-id-
    End Sub  '-select invoice-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Update SALE (Invoice) totals..--
    '-- Update SALE (Invoice) totals..--

    '-- payments:=
    '== mDecPaymentTotalRcvd '-- Total stuff incl Cash tendered..
    '== mDecPaymentCashRcvd As Decimal
    '== mDecChange As Decimal
    '== mDecPaymentNettCredited As Decimal
    '== mIntCurrentPaymentTypeIndex As Integer = -1

    '=3519.0115=  mDecPaymentNettCredited APPLIES to what is credited to current SALE value.
    '=3519.0115=  mDecPaymentNettCredited APPLIES to what is credited to current SALE value.
    '--  So it excludes Change, and Change saved as Credit Note..


    Private Function mbUpdateSaleTotal() As Boolean
        Dim row1 As DataGridViewRow
        Dim decAmountEx, decAmountInc, decAmountTax, decPaymentAmount, decOver As Decimal
        Dim decAmount1, decAmount2, decCostEx, decCostInc, decMarginRate As Decimal
        Dim s1 As String
        Dim intQty, intCount, intCents1, intCentsRounding As Integer

        mDecTotalCostEx = 0
        mDecTotalCostInc = 0
        mDecSubTotal = 0
        mDecSubTotalEx_non_taxable = 0 '=3103.411-
        mDecSubTotalEx_taxable = 0      '=3103.411-
        mDecTotalTax = 0

        mDecChangeAsCreditNote = 0
        mDecCreditNoteWdlAmount = 0
        mLabSaleChargeBalance.Text = ""

        mBtnCommitSale.Enabled = False
        intCount = 0 '-- number rows-
        If (mDgvSaleItems.Rows.Count > 0) Then
            For Each row1 In mDgvSaleItems.Rows
                intQty = CDec(row1.Cells(k_GRIDCOL_QTY).Value)
                decAmountEx = CDec(row1.Cells(k_GRIDCOL_SELLACTUAL_EX_EXTENDED).Value)
                decAmountInc = CDec(row1.Cells(k_GRIDCOL_SELLACTUAL_INC_EXTENDED).Value)
                mDecSubTotal += decAmountInc
                '--compute total items tax..--
                decAmountTax = CDec(row1.Cells(k_GRIDCOL_SELLACTUAL_TAX_EXTENDED).Value)
                mDecTotalTax += decAmountTax
                If (decAmountTax > 0) And (decAmountEx > 0) Then  '--taxable-
                    mDecSubTotalEx_taxable += decAmountEx
                Else  '-non-taxable-
                    mDecSubTotalEx_non_taxable += decAmountEx
                End If
                intCount += 1
                If Not row1.IsNewRow Then
                    row1.HeaderCell.Value = Trim(CStr(intCount)) & "."
                End If
                '=3107.714= Compute total Cost.-
                decCostEx = CDec(row1.Cells(k_GRIDCOL_COST_EX).Value) * intQty  '--extended cost..
                mDecTotalCostEx += decCostEx

            Next row1
        End If '--count-
        If (mDecSubTotal < mDecDiscount) Then
            mDecDiscount = 0
            mDecDiscountTax = 0
            mTxtSaleDiscount.Text = ""
            MsgBox("Discount was too big; " & vbCrLf & "    It has now been reset.", MsgBoxStyle.Exclamation)
        Else
            '-- recalculate discount from % dropdown. setting

        End If
        mDecSubTotal2 = mDecSubTotal - mDecDiscount  '= + mDecCashout

        '= With mFrmSale -
        mTxtSaleTotalTax.Text = FormatCurrency(mDecTotalTax, 2)
        mTxtSaleSubTotal.Text = FormatCurrency(mDecSubTotal, 2)
        mTxtSaleNettTax.Text = FormatCurrency((mDecTotalTax - mDecDiscountTax), 2)
        mTxtSaleSubTotal2.Text = FormatCurrency(mDecSubTotal2, 2)
        '= End With
        '=3411.0405=  Restore discount (in case of restoring)..
        mTxtSaleDiscount.Text = FormatCurrency(mDecDiscount, 2)
        '-and tax split-
        If (mDecDiscount > 0) Then
            Call mbShowTaxSplit()
        End If
        If mbIsRestoringScreen Then
            If msDiscountSelectedItem <> "" Then
                mCboSaleDiscountPercent.SelectedItem = msDiscountSelectedItem
            End If
        End If
        '-----------------------
        '=3107.714= Compute total Margin.-
        decAmount1 = mDecSubTotal - mDecTotalTax  '-subTotalEx..
        decAmount2 = decAmount1 - mDecTotalCostEx  '-margin amount..
        If decAmount1 = 0 Then
            decMarginRate = 0
        Else  '-can divide-
            decMarginRate = (decAmount2 / decAmount1) * 100
        End If
        '-- show gp % mouseover...
        mToolTip1.SetToolTip(mLabSaleDiscount, "Margin: " & Format(decMarginRate, "  00.00") & "%")

        '-  Compute Rounding..--
        mDecRounding = 0
        '==4201.0717.  17-July-2019-
        '== -- Add code to do Invoice Rounding for CASH only. 
        '==      USE "gIntGetRoundingCents" to do rounding calc.
        '== 4.2 ROUNDING RULES..
        '- 1. Only the Final Invoice Total is rounded..
        '- 2. For Inputted cash amounts- (validation)- 
        '-           ONLY "rounded" cash amounts to be accepted.
        '- 3. SALE- Rounding only applied if Sale has ONLY cash as Payment Detail, AND
        '-                                              "Cash" is selected for any change.
        '--          ("Change" will then automatically be rounded.)
        '- 4. REFUND- Rounding only occurs if cash is selected for Refund.
        '-   - - -
        Dim decRoundingAmount As Decimal = gIntGetRoundingCents(mDecSubTotal2)

        'intCentsRounding = 0
        'intCents1 = (mDecSubTotal2 * 100) Mod 10  '--get original cents.
        'Select Case intCents1
        '    Case 1, 6 : intCentsRounding = -1
        '    Case 2, 7 : intCentsRounding = -2
        '    Case 3, 8 : intCentsRounding = 2
        '    Case 4, 9 : intCentsRounding = 1
        'End Select
        'mDecRounding = (intCentsRounding / 100)   '== make 0.0d  --
        decRoundingAmount = (decRoundingAmount / 100) '== make 0.0d  --

        '= Decide below with Payments whether to round or not..
        '= Decide below with Payments whether to round or not..
        'mDecInvoiceTotal = mDecSubTotal2 + mDecRounding
        'mTxtSaleRounding.Text = FormatCurrency(mDecRounding, 2)
        'mTxtSaleTotal.Text = FormatCurrency(mDecInvoiceTotal, 2)
        'mTxtSalePaymentBalance.Text = FormatCurrency(mDecInvoiceTotal, 2)

        '-- First- get payments.-
        '--dvg payments--
        mDecPaymentTotalRcvd = 0
        mDecPaymentCashRcvd = 0
        mDecChange = 0
        mDecPaymentNettCredited = 0

        '=3519.0317=
        If (Trim(mTxtCreditNoteWdl.Text) <> "") AndAlso mbIsNumeric(Trim(mTxtCreditNoteWdl.Text)) Then
            mDecCreditNoteWdlAmount = CDec(mTxtCreditNoteWdl.Text)
        End If

        For Each row1 In mDgvSalePaymentDetails.Rows
            '== dgvPaymentDetails.Rows(rx).Cells(k_PAYGRIDCOL_AMOUNT).Value()
            decPaymentAmount = CDec(row1.Cells(k_PAYGRIDCOL_AMOUNT).Value)
            mDecPaymentTotalRcvd += decPaymentAmount
            s1 = row1.Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value
            If (InStr(LCase(s1), "cash") > 0) Then
                mDecPaymentCashRcvd += decPaymentAmount
            End If
        Next  '-row--
        If mDecPaymentTotalRcvd > 0 Then
            '=3401.317=  mLabSalePayments.Text = "-- Payments Received: " & FormatCurrency(mDecPaymentTotalRcvd, 2)
            '=3403.1014- restore this.
            '= mLabSalePayments.Width = mDgvSalePaymentDetails.Width
            mLabSalePayments.Text = "- New Payments: " & FormatCurrency(mDecPaymentTotalRcvd, 2)
        End If
        '== NOW to decide on rounding..
        '- check if we need to apply it
        If ((msTransactionType = "sale") AndAlso (mDecPaymentCashRcvd > 0) AndAlso _
                             (mDecPaymentCashRcvd = mDecPaymentTotalRcvd) AndAlso mOptRefundCash.Checked = True) Or _
                ((msTransactionType = "refund") AndAlso mOptRefundCash.Checked) Then
            mDecRounding = decRoundingAmount
        End If  '-cash for rounding.
        mDecInvoiceTotal = mDecSubTotal2 + mDecRounding  '- if any-
        mTxtSaleRounding.Text = FormatCurrency(mDecRounding, 2)
        mTxtSaleTotal.Text = FormatCurrency(mDecInvoiceTotal, 2)
        mTxtSalePaymentBalance.Text = FormatCurrency(mDecInvoiceTotal, 2)

        '= compute results--
        mLabSaleCrBal.Visible = False
        mLabSaleChange.Enabled = False

        '=3519.0317= 
        '--   We now can know how much CreditNote is being used. (User decides..)

        decOver = ((mDecPaymentTotalRcvd + mDecCreditNoteWdlAmount) - mDecInvoiceTotal)
        mLabSaleHelp2.Text = ""
        mDecPaymentNettCredited = mDecPaymentTotalRcvd  '=Only for Change-  - mDecCashout
        mDecBalance = mDecInvoiceTotal - mDecPaymentNettCredited - mDecCreditNoteWdlAmount
        mLabSaleChargeBalance.ForeColor = Color.Black

        mGrpBoxRefundType.Visible = False   '-no cr/note distraction-
        '-- Compute balance..
        'If (mDecCashout > 0) Then
        '    '--SALE, and Cash customers only..
        '    mLabSaleChange.Text = "Cash-out"
        '    mLabSaleChange.Enabled = True
        '    mTxtSaleChange.Text = mTxtSaleCashout.Text
        '    If (mDecInvoiceTotal > 0) And (mDecInvoiceTotal = mDecPaymentTotalRcvd) Then  '--balanced-
        '        mLabSaleChange.Text = "Cash to go:"
        '        mTxtSaleChange.Text = FormatCurrency(mDecCashout, 2)
        '        mBtnCommitSale.Enabled = True
        '    ElseIf (decOver > 0) Then
        '        mDecBalance = -decOver
        '        '==MsgBox("Invoice is over-paid.." & vbCrLf & _
        '        '==                    "Enter more Cashout amount if required..", MsgBoxStyle.Exclamation)
        '        mLabSaleHelp2.Text = "Invoice is over-paid.." & vbCrLf & _
        '                            "Enter more Cashout amount if required.."
        '    End If  '--cashout and balanced..
        'Else
        If mbIsRefund Then
            mDecBalance = -mDecInvoiceTotal
            If Abs(mDecInvoiceTotal) <= 0 Then
                '--nothing to refund-
            Else
                If (decOver > 0) Then
                    MsgBox("ERROR- REFUND Shouldn't have payment Rcvd.", MsgBoxStyle.Exclamation)
                    '=3403.1014= All refunds are refunded now=
                    '= ElseIf mbSaleIsAccountCust Then
                    '=    mLabSaleChargeBalance.Visible = True
                    '=    mTxtSaleChange.Text = ""  '= "Refunding " & FormatCurrency(mDecBalance, 2)
                    '=    mLabSaleChargeBalance.Text = "Refund will be credited to Account.."
                Else  '-not account-
                    mLabSaleChargeBalance.Visible = False
                    mGrpBoxRefundType.Visible = True
                    '= mDecBalance = mDecInvoiceTotal-
                    If mOptRefundCash.Checked Then
                        mTxtSaleChange.Text = "Refund: " & FormatCurrency(Abs(mDecBalance), 2)
                        mLabSaleChargeBalance.Text = mTxtSaleChange.Text
                    ElseIf mOptRefundCredit.Checked Then  '-credit note-
                        mTxtSaleChange.Text = ""
                        mLabSaleChargeBalance.Text = "Refund will be saved as Credit Note.."
                    ElseIf mOptRefundEftPosDr.Checked Then  '-EftPOS DR-
                        mTxtSaleChange.Text = ""
                        mLabSaleChargeBalance.Text = "Refund will be via -EftPOS DR.."
                    ElseIf mOptRefundEftPosCr.Checked Then  '-EftPOS CR-
                        mTxtSaleChange.Text = ""
                        mLabSaleChargeBalance.Text = "Refund will be  via -EftPOS CR...."
                    End If
                End If  '--account-
                mBtnCommitSale.Enabled = True
            End If  '-total-
        ElseIf mbIsLayby Then
            mLabSaleHelp2.Text = "Complete Payment will be saved as CreditNote.."  '= & vbCrLf & _
            mDecChangeAsCreditNote = mDecPaymentTotalRcvd
            mDecChange = 0
            mBtnCommitSale.Enabled = True

        Else  '--not cashout.-- Not refund-
            mTxtSaleChange.Text = ""
            If (decOver > 0) Then
                If (mDecPaymentCashRcvd < decOver) Then
                    If mbIsOnAccount() Then '=3403.1014=  mbSaleIsAccountCust Then
                        mLabSaleHelp2.Text = "Account Sale is Over-paid.. (not allowed)."
                        mDecChange = 0
                        mDecChangeAsCreditNote = 0
                        mLabSaleChange.Text = ""
                    Else  '-not account-
                        If (mDecCreditNoteWdlAmount > 0) Then
                            '-User is Applying CrNote- So he must adjust cr/note or payment.
                            mLabSaleHelp2.Text = "This Sale is Over-paid... Pls adjust."  '= & vbCrLf & _
                            mLabSaleChargeBalance.Text = mLabSaleHelp2.Text '= "Enter Cashout amount if cash required.."

                        Else  '--Not using Vr-note- so can save as cr-note.
                            mLabSaleHelp2.Text = "Over-payment will be saved as CreditNote.."  '= & vbCrLf & _
                            mLabSaleChargeBalance.Text = mLabSaleHelp2.Text '= "Enter Cashout amount if cash required.."
                            mDecChange = 0
                            mDecChangeAsCreditNote = decOver
                            '=3519.0115=  mDecPaymentNettCredited APPLIES to what is credited to current SALE value.
                            mDecPaymentNettCredited = mDecPaymentTotalRcvd - mDecChangeAsCreditNote
                            mLabSaleChange.Text = "Credit-Note:"
                            mLabSaleChange.Enabled = True
                            mTxtSaleChange.Text = FormatCurrency(decOver, 2)
                            mBtnCommitSale.Enabled = True
                        End If
                    End If  '--account/not-
                ElseIf (mDecPaymentCashRcvd >= decOver) Then '=Have cash- can be change.
                    '=- Have cash-in..  can be change.
                    If mbIsOnAccount() Then '=3403.1014=   mbSaleIsAccountCust Then
                        mGrpBoxRefundType.Enabled = False
                        mGrpBoxRefundType.Visible = False
                    Else
                        mGrpBoxRefundType.Enabled = True
                        mGrpBoxRefundType.Visible = True
                    End If
                    '= mGrpBoxRefundType.Enabled = True
                    '= mGrpBoxRefundType.Visible = True
                    If (mDecCreditNoteWdlAmount > 0) Then
                        '-User is Applying CrNote- So he must adjust cr/note or payment.
                        mLabSaleHelp2.Text = "Cash-over can only be given as change..."  '= & vbCrLf & _
                        mLabSaleChargeBalance.Text = mLabSaleHelp2.Text '= "Enter Cashout amount if cash required.."
                        If mOptRefundCash.Checked Then  '-ok-
                            mDecChange = decOver
                            mDecChangeAsCreditNote = 0
                            mLabSaleChange.Enabled = True
                            mLabSaleChange.Text = "Change:"
                            mTxtSaleChange.Text = FormatCurrency(mDecChange, 2)
                            mDecPaymentNettCredited = mDecPaymentTotalRcvd - mDecChange
                            mBtnCommitSale.Enabled = True
                        End If
                    Else '-not using cr-note-
                        If mbIsOnAccount() OrElse mOptRefundCash.Checked Then '=3403.1014=   mbSaleIsAccountCust OrElse mOptRefundCash.Checked Then
                            '=mTxtSaleChange.Text = "Cash Refund " & FormatCurrency(Abs(mDecBalance), 2)
                            mDecChange = decOver
                            mDecChangeAsCreditNote = 0
                            mLabSaleChange.Enabled = True
                            mLabSaleChange.Text = "Change:"
                            mTxtSaleChange.Text = FormatCurrency(mDecChange, 2)
                            mDecPaymentNettCredited = mDecPaymentTotalRcvd - mDecChange
                            mLabSaleChargeBalance.Text = "Cash-over will be given as change.."
                        Else  '-credit note-
                            mDecChange = 0
                            mDecChangeAsCreditNote = decOver
                            mLabSaleChange.Text = "Credit-Note:"
                            mLabSaleChange.Enabled = True
                            mTxtSaleChange.Text = FormatCurrency(decOver, 2)
                            mDecPaymentNettCredited = mDecPaymentTotalRcvd - mDecChangeAsCreditNote
                            mLabSaleChargeBalance.Text = "Cash-over will be saved as Credit Note.."
                        End If
                        mBtnCommitSale.Enabled = True
                    End If  '-usinf=g cr-note-
                    'Else
                    '    mDecPaymentNettCredited = mDecPaymentTotalRcvd
                    '    mGrpBoxRefundType.Enabled = False
                End If  '-cash decOver-
            ElseIf (decOver < 0) Then  '-not overpaid-
                '--insufficient funds-
                If mbIsOnAccount() Then '=3403.1014=   mbSaleIsAccountCust Then
                    mLabSaleHelp2.Text = "Invoice will be debited to Account."
                    mLabSaleChargeBalance.Text = "Invoice will be debited to Account."
                    mBtnCommitSale.Enabled = True
                Else  '-cash cust- 
                    '--insufficient funds-
                    '--not account..  apply credit note bal. if any..
                    '-- Check for Credit Notes avail...
                    '-  and if we can use credit bal. for payment..
                    If (mDecCreditNoteCreditRemaining > 0) Then
                        If (mDecCreditNoteCreditRemaining >= mDecBalance) Then  '-can fund it.
                            '= mDecCreditNoteWdlAmount = mDecBalance
                            mLabSaleHelp2.Text = "Under-funded- Credit Note could be used..."
                            '= mBtnCommitSale.Enabled = True
                        Else  '-use all credit for this, but still not enough.
                            '= mDecCreditNoteWdlAmount = mDecCreditNoteCreditRemaining
                            '== mDecBalance -= mDecCreditNoteWdlAmount
                            mLabSaleHelp2.Text = "More funds needed.."
                        End If
                        mLabSaleChargeBalance.ForeColor = Color.Magenta
                        mLabSaleChargeBalance.Text = "Credit Note Wdl:  " & FormatCurrency(mDecCreditNoteWdlAmount, 2)
                    Else  '-no credit note avail.
                        mLabSaleHelp2.Text = "Sale is still under-funded..."
                    End If  '-credit avail-
                End If '--account-
            Else  '-balanced-
                '=3519.0321=
                If (mDecInvoiceTotal > 0) Then
                    mBtnCommitSale.Enabled = True
                End If
            End If  '--decOver-
            mDecBalance = mDecInvoiceTotal - mDecPaymentNettCredited - mDecCreditNoteWdlAmount
        End If  '-cashout/refund.-- 

        mTxtSalePaymentBalance.Text = FormatCurrency((Abs(mDecBalance)), 2)
        '= End With '--frmsale-
        mLabSaleCrBal.Visible = True
        mLabSaleCrBal.Text = ""
        If (mDecBalance < 0) Then  '--credit-
            mLabSaleCrBal.Text = "Cr"
        ElseIf (mDecBalance > 0) Then
            mLabSaleCrBal.Text = "Dr"
        End If
        '-- Can commit QUOTE/Layby anyway..-
        If (mbIsQuote Or mbIsLayby) AndAlso (mDecInvoiceTotal > 0) Then
            mBtnCommitSale.Enabled = True
        ElseIf Not mbIsRefund Then
            '--Sale- Change must be a rounded amount.
            If (mDecChange <> 0) Then
                If (gIntGetRoundingCents(mDecChange) <> 0) Then
                    mBtnCommitSale.Enabled = False
                    mLabSaleHelp2.Text = "Change must be a rounded amount.."
                    MsgBox("Pls Note- Cash Change has to be a rounded amount.", MsgBoxStyle.Exclamation)
                End If  '-rounded-
            End If  '-change-
        End If '-tr-type-

        '=3307.0211= 
        '- -- WARN if ACCOUNT cust exceeding limit..- 
        If mbSaleIsAccountCust And (Not mbIsQuote) And mbIsOnAccount() And mBtnCommitSale.Enabled Then
            If (mDecInvoiceTotal > 0) AndAlso (mDecAccountCustLimitRemaining >= mDecInvoiceTotal) Then
                '=ok =   mBtnCommitSale.Enabled = True
            ElseIf (mDecAccountCustLimitRemaining < mDecInvoiceTotal) Then
                '==  mLabSaleHelp2.Text = "Invoice is more than remaining credit.."
                If Not mbCreditLimitWarningGiven Then
                    mbCreditLimitWarningGiven = True  '-don't keep repeating it.
                    MsgBox("Attention:  " & vbCrLf & _
                             " Invoice value exceeds remaining Account credit Limit..", MsgBoxStyle.Exclamation)
                End If
                '==still ok=  mBtnCommitSale.Enabled = False
            End If
        End If
        '= mCboSaleDiscountPercent.Visible = False     '--DISABLE FIRST !!-
        '== mCboSaleDiscountPercent.SelectedIndex = 0    '--default no discount.-
        '= mBtnDiscountPC.Enabled = True

        '=4201.1028=  If a payment amount wass entered,
        '-   DISABLE switching to Charge To Account..
        If (mDecPaymentNettCredited + mDecCreditNoteWdlAmount > 0) Then
            mChkOnAccount.Enabled = False
            mChkOnAccount2.Enabled = False
        End If

    End Function  '--UpdateSaleTotals-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    Private Function mbClearEditLine() As Boolean

        '=3311.516= CLEAR all  textBox controls for Sale Item Line Entry..
        '== mLabSaleItemNo.Text = ""
        '== mTxtSaleItemBarcode.Text = ""
        mTxtSaleItemSerialNo.Text = ""
        '==3401.313= mTxtSaleItemCategory.Text = ""
        mTxtSaleItemDescription.Text = ""
        mTxtSaleItemSellPrice.Text = ""
        '-clear cost tooltio-
        mToolTip1.SetToolTip(mTxtSaleItemSellPrice, "")
        mTxtSaleItemQty.Text = ""
        mTxtSaleItemExtension.Text = ""
        mBtnSaleLineOk.Enabled = False

        mIntStock_id = -1
        mbIsSerialItem = False
        mbIsNonStockItem = False

        mDatatableEditingItem = Nothing '--stock table row(0) current edit..
        mPicSaleItem.Image = Nothing

    End Function  '-clear edit.-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- clear Invoice..

    Private Function mbClearInvoice() As Boolean

        '= With mFrmSale
        '== mLabSaleAvailCredit.Text = ""

        '== 3401.317=  mLabSalePayments.Text = "- Payment Received- Enter Details -"
        '== btnLineEnter.Enabled = False
        mBtnCommitSale.Enabled = False
        mBtnSaleComments.Enabled = False

        mIntCurrentPaymentTypeIndex = -1

        mDgvSaleItems.Rows.Clear()

        '--clear payments-
        For Each gridrow1 As DataGridViewRow In mDgvSalePaymentDetails.Rows
            gridrow1.Cells(k_PAYGRIDCOL_AMOUNT).Value = "0.00"
        Next
        mDgvSalePaymentDetails.ClearSelection()
        '=4201.0708=
        '= mDgvSalePaymentDetails.CurrentCell = mDgvSalePaymentDetails.Rows(0).Cells(1)  '-1st amount.

        '=3519.0317=
        mTxtCreditNoteWdl.Text = ""

        mPicSaleItem.Image = Nothing

        mTxtSaleJobNo.Text = ""
        msSaleJobReport = ""  '=3411.0107=

        mColStockImages = New Collection

        '= Call mbClearItemEntry()
        mDecSubTotal = 0
        mDecDiscount = 0
        mDecDiscountTax = 0
        msDiscountSelectedItem = ""

        mDecTotalCostEx = 0
        mDecTotalCostInc = 0

        mDecRounding = 0
        mDecInvoiceTotal = 0
        '= mDecCashout = 0
        mDecBalance = 0
        mDecCreditNoteWdlAmount = 0
        mbCreditLimitWarningGiven = False

        mTxtSaleTotalTax.Text = ""
        mTxtSaleSubTotal.Text = ""

        mTxtSaleDiscount.Text = ""
        mTxtSaleDiscountAnalysis.Text = ""

        mTxtSaleNettTax.Text = ""

        '= mTxtSaleCashout.Text = ""
        mTxtSaleSubTotal2.Text = ""
        mTxtSaleRounding.Text = ""
        mTxtSaleTotal.Text = ""
        mLabSaleCrBal.Visible = False
        msSaleComments = ""
        msSaleDeliveryInstr = ""
        '= mTxtSaleComments.Text = ""
        '= mTxtSaleDelivery.Text = ""

        mTxtSaleChange.Text = ""
        mTxtSalePaymentBalance.Text = ""

        '= mCboSaleDiscountPercent.Visible = False  '-first.-
        mCboSaleDiscountPercent.SelectedIndex = 0    '--default no discount.-
        '= mBtnDiscountPC.Enabled = True

        '== End With '- mFrmSale-
        mbIsCancelling = False

        ''=3519.0118=
        ''-- clear account switch..
        'mOptSaleSale.Checked = False
        'mOptSaleRefund.Checked = False
        'mOptSaleLayby.Checked = False
        'mOptSaleQuote.Checked = False

        'mChkOnAccount.Enabled = False
        'mChkOnAccount2.Enabled = False

        'mChkOnAccount.Checked = False
        'mChkOnAccount2.Checked = False

    End Function  '--ClearInvoice-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- Default SETUP for Sale Transaction Type--
    '-- called when customer selected..
    '-- Can be overridden by REfund/Quote options..

    Private Function mbSetupSaleDefault() As Boolean

        mbIsRefund = False
        mbIsQuote = False
        '=3301.710= mDgvSaleItems.Columns("SerialNo").ReadOnly = False
        '=3411.0311=
        '--  Restore Sale Select RadioButtons.
        mOptSaleRefund.Checked = False
        mOptSaleQuote.Checked = False
        mOptSaleLayby.Checked = False
        mOptSaleSale.Checked = True

        mbIsDeliveringLayby = False   '=3403.523=
        mIntDeliveredLayby_id = -1

        mbIsLayby = False
        mIntLayby_id = -1
        mIntJob_id = -1

        mLabSaleTranType.Text = "Sale"
        mLabSaleTranType.Enabled = True

        msTransactionType = "sale"
        mLabSaleJobDelivery.Visible = True
        mTxtSaleJobNo.Visible = True
        mLabSaleTranType.ForeColor = Color.Blue
        '==mPanelPayment.Visible = True
        mPanelPayment.Enabled = True
        mDgvSalePaymentDetails.Enabled = True
        '=3519.0317=
        mGrpBoxSalePayments.Enabled = True
        If (mDecCreditNoteCreditRemaining > 0) Then
            '- have creditNote credit.
            mTxtCreditNoteWdl.Enabled = True
        Else
            mTxtCreditNoteWdl.Enabled = False
        End If

        'If mChkOnAccount.Enabled Then  '--account customer.-
        '    mChkOnAccount.Checked = True  '-default for Account Cust.
        '    mChkOnAccount2.Checked = True   '-- MUST keep them aligned.
        'Else
        '    mChkOnAccount.Checked = False
        '    mChkOnAccount2.Checked = False  '-- MUST keep them aligned.
        'End If
 
        'If Not mbSaleIsAccountCust Then  '-no cashout for a/c cust..
        '    mLabSaleCashout.Enabled = True
        '    mTxtSaleCashout.Enabled = True
        'Else
        '    mLabSaleCashout.Enabled = False
        '    mTxtSaleCashout.Enabled = False
        'End If
        mLabSaleInvTotal.Text = "Invoice Total"
        '== mLabSalePayments.Text = "-- Payment Details --"

        ''=- If JobTracking application-
        ''--  Check if any jobs to deliver for this customer.
        'Dim intJobId As Integer = -1
        'If (msTransactionType = "sale") AndAlso mColSqlDBInfo.Contains("jobs") Then
        '    intJobId = mbDeliveringJob()
        'End If  '-jobtracking-
        mDgvSaleItems.Enabled = True

        '=4201.1030-  NOT YET-  mPanelSaleFooter.Enabled = True

        mGrpBoxRefundType.Enabled = False
        '== mGrpBoxRefundType.Visible = False

        '= mCboSaleDiscountPercent.Visible = False  '-first-
        mCboSaleDiscountPercent.SelectedIndex = 0    '--default no discount.-
        '= mBtnDiscountPC.Enabled = True

        'If (intJobId > 0) Then  '--have job.. Is setted up..
        '    '-- emulate Continue button..-
        '    mPanelOptTranType.Enabled = False
        '    mLabSaleHelp.Text = "Scan or Enter barcodes for sales items."
        '    mPanelSaleLineEntry.Enabled = True
        '    mTxtSaleItemBarcode.Select()
        'Else '-no job-
        '    '=3301.427=
        '    '==mLabSaleHelp.Text = "Scan or Enter barcodes for sales items."
        '    mPanelOptTranType.Enabled = True  '== can change opt..
        '    mLabSaleHelp.Text = "Press Continue to go on.."
        '    mBtnSaleContinue.Enabled = True
        '    mBtnSaleContinue.Select()
        'End If  '-have job-

        '==3301.516= No more editing Grid..  
        '==   mDgvSaleItems.Select()
        '==mPanelSaleLineEntry.Enabled = True
        '= mTxtSaleItemBarcode.Select()

        '=4201.0727=
        mDgvSalePaymentDetails.CurrentCell = mDgvSalePaymentDetails.Rows(0).Cells(1)  '-1st amount.


    End Function '-mbSetupSaleDefault
    '= = = = = = = = = = = = = = = =  = =
    '-===FF->

 
    '-- Item Qty changed..-
    '-  Update extension in stock info grid row..--

    Private Function mbUpdateSaleStockItem(ByVal intGridRow As Integer, _
                                                  ByVal sQty As String) As Boolean
        '= Dim intStock_id As Integer
        Dim sBarcode As String = ""
        Dim decItemQty As Decimal
        Dim decSellActualExTax As Decimal '== After applying Pricing Grade..-"-
        Dim decSellActualIncTax As Decimal '== Plus tax.-..-"-
        Dim decItemExtension As Decimal '== After extension incl..-"-
        Dim decSellActualTotalTax As Decimal = 0 '== tax..-"-
        Dim decItemExtensionExTax As Decimal '== After extension incl..-"-

        decItemQty = CDec(sQty)

        With mDgvSaleItems.Rows(intGridRow)
            decSellActualExTax = CDec(.Cells(k_GRIDCOL_SELLACTUAL_EX).Value)
            decSellActualIncTax = CDec(.Cells(k_GRIDCOL_SELLACTUAL_INC).Value)
        End With

        '-- extension-
        decItemExtension = decSellActualIncTax * decItemQty
        decSellActualTotalTax = decItemExtension - (decSellActualExTax * decItemQty)
        decItemExtensionExTax = decItemExtension - decSellActualTotalTax

        '--update
        With mDgvSaleItems.Rows(intGridRow)
            .Cells(k_GRIDCOL_SELLACTUAL_INC_EXTENDED).Value = FormatCurrency(decItemExtension, 2)
            .Cells(k_GRIDCOL_SELLACTUAL_TAX_EXTENDED).Value = FormatCurrency(decSellActualTotalTax, 2)
            .Cells(k_GRIDCOL_SELLACTUAL_EX_EXTENDED).Value = FormatCurrency(decItemExtensionExTax, 2)
        End With

    End Function  '--mbUpdateSaleStockItem-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-  load Edit Line Item and stock info grid row..--

    '= NB:  This is coming from the Edit line..
    '-- Take the serialNo price, qty, from the Edit Text Boxes..

    Private Function mbSetupSaleStockItem(ByRef dataTable1 As DataTable, _
                                           ByVal intGridRow As Integer) As Boolean
        Dim row1 As DataRow
        Dim intStock_id As Integer
        Dim sBarcode As String = ""
        Dim sGoodsTaxcode, sSalesTaxCode As String
        Dim decCostExTax, decCostIncTax As Decimal
        Dim decSellExTax As Decimal  '--rrp ex tax-
        Dim decSellTaxAmount As Decimal = 0
        Dim decSellIncTax As Decimal '=="RRP"-
        '-- after pricing grade-
        Dim decSellActualExTax As Decimal '== After applying Pricing Grade..-"-
        Dim decSellActualTaxAmount As Decimal = 0 '== tax..-"-
        Dim decSellActualIncTax As Decimal '== Plus tax.-..-"-
        Dim decItemQty As Decimal = 1   '--default qty--
        Dim decItemExtension As Decimal '== After extension incl..-"-
        Dim decItemExtensionExTax As Decimal '== After extension incl..-"-
        Dim decSellActualTotalTax As Decimal = 0 '== tax..-"-

        '= Dim yBinaryData() As Byte
        '= Dim image1 As Image

        row1 = dataTable1.Rows(0)
        '= Call mbClearItemEntry()
        intStock_id = row1.Item("stock_id")
        '- check if already set up..=
        If CInt(mDgvSaleItems.Rows(intGridRow).Cells(k_GRIDCOL_STOCK_ID).Value) = intStock_id Then
            Exit Function  '--don't over-write existing info..-
        End If

        sBarcode = row1.Item("barcode")
        sGoodsTaxcode = UCase(row1.Item("Goods_taxcode"))
        sSalesTaxCode = UCase(row1.Item("Sales_taxcode"))

        decCostExTax = CDec(row1.Item("costExTax"))
        decCostIncTax = decCostExTax
        If sGoodsTaxcode = "GST" Then
            decCostIncTax = decCostExTax + (decCostExTax * mDecGST_rate / 100)
        End If
        decSellExTax = CDec(row1.Item("sellExTax"))  '--rrp ex tax-

        '-- get GST rate for this tax code..
        '-mDecGST_rate-
        '- compute rrp incl. -
        '==     v3.4.3403.0731 = 31Jul2017=
        '==      -- clsPOS34Sale-  Fix TAX code choice if setting up line. (was using GoodsTax code-)..
        If (sSalesTaxCode = "GST") Then '= (sGoodsTaxcode = "GST") Then
            decSellTaxAmount = ((decSellExTax * mDecGST_rate / 100))
        Else
            decSellTaxAmount = 0
        End If
        decSellIncTax = decSellExTax + decSellTaxAmount

        '-- ACTUAL=
        '== NOT HERE+ - GET cust Grade and compute ACTUAL price for this cust.. -
        '=DONE in edit-  decSellActualExTax = decSellExTax  '-- TEMP--
        '- compute Actual Incl. - mDecSellActualTaxAmount
        '=DONE in edit-decSellActualTaxAmount = ((decSellExTax * mDecGST_rate / 100))
        '=DONE in edit-decSellActualIncTax = mDecSellActualIncTax  '== decSellExTax + decSellTaxAmount
        decSellActualExTax = mDecSellActualExTax
        '=3403.717=
        decSellActualTaxAmount = mDecSellActualTaxAmount  '-'=3403.717==
        decSellActualIncTax = mDecSellActualIncTax
        decItemQty = mDecSellItemQty  '-from Edit.-
        '-- extension-
        decItemExtension = decSellActualIncTax * decItemQty
        decSellActualTotalTax = decItemExtension - (decSellActualExTax * decItemQty)
        decItemExtensionExTax = decItemExtension - decSellActualTotalTax

        '- show values-
        '-test-
        '- MsgBox("Moving item to Grid-" & vbCrLf & _
        '-          "Actual Tax Extended is: " & FormatCurrency(decSellActualTotalTax, 2), MsgBoxStyle.Information)

        With mDgvSaleItems.Rows(intGridRow)
            .Cells(k_GRIDCOL_BARCODE).Value = row1.Item("barcode")
            .Cells(k_GRIDCOL_SERIALNO).Value = mTxtSaleItemSerialNo.Text
            .Cells(k_GRIDCOL_CAT1).Value = row1.Item("cat1")
            .Cells(k_GRIDCOL_CAT2).Value = row1.Item("cat2")
            .Cells(k_GRIDCOL_DESCRIPTION).Value = row1.Item("description")
            .Cells(k_GRIDCOL_TAXCODE).Value = row1.Item("sales_TaxCode")
            .Cells(k_GRIDCOL_SELL_INC).Value = FormatCurrency(decSellIncTax, 2)
            .Cells(k_GRIDCOL_SELLACTUAL_INC).Value = FormatCurrency(decSellActualIncTax, 2)
            .Cells(k_GRIDCOL_QTY).Value = CStr(decItemQty) '= "1"
            .Cells(k_GRIDCOL_SELLACTUAL_INC_EXTENDED).Value = FormatCurrency(decItemExtension, 2)

            '-- Hidden--
            .Cells(k_GRIDCOL_STOCK_ID).Value = CStr(row1.Item("stock_id"))
            .Cells(k_GRIDCOL_COST_EX).Value = FormatCurrency(decCostExTax, 2)
            .Cells(k_GRIDCOL_COST_INC).Value = FormatCurrency(decCostIncTax, 2)
            .Cells(k_GRIDCOL_SELL_EX).Value = FormatCurrency(decSellExTax, 2)
            .Cells(k_GRIDCOL_SELLACTUAL_EX).Value = FormatCurrency(decSellActualExTax, 2)
            .Cells(k_GRIDCOL_SELLACTUAL_TAX).Value = FormatCurrency(decSellActualTaxAmount, 2)
            .Cells(k_GRIDCOL_SELLACTUAL_TAX_EXTENDED).Value = FormatCurrency(decSellActualTotalTax, 2)
            '--k_GRIDCOL_SELL_EXTAX_EXTENDED
            .Cells(k_GRIDCOL_SELLACTUAL_EX_EXTENDED).Value = FormatCurrency(decItemExtensionExTax, 2)

            '-- Serial No.-
            .Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value = "-1"  '--initialise..-
            If row1.Item("track_serial") Then  '--s/be boolean-
                '=If mbIsQuote Then
                '=    .Cells(k_GRIDCOL_SERIALNO).ReadOnly = True    '-- NO serial no for quotes.-
                '=Else '-not quote-
                '=    .Cells(k_GRIDCOL_SERIALNO).ReadOnly = False   '-- wants serial no.-
                '=End If '-quote-
                .Cells(k_GRIDCOL_TRACK_SERIAL).Value = "1"    '--save flag in hidden grid column.-
                .Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value = mIntSerialAudit_id  '=3301.1112--
                '--Item with serialno.  can only be ONE of per SerialNo. (per line)..
                .Cells(k_GRIDCOL_QTY).ReadOnly = True
            Else  '-no serial tracking.-
                '== .Cells(k_GRIDCOL_SERIALNO).ReadOnly = True
                .Cells(k_GRIDCOL_TRACK_SERIAL).Value = "0"
                .Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value = "-1"
                '==.Cells(k_GRIDCOL_QTY).ReadOnly = False   '--can be many..-
            End If
            If dataTable1.Columns.Contains("isNonStockItem") Then
                If row1.Item("isNonStockItem") Then  '--boolean in DataTable--
                    .Cells(k_GRIDCOL_ISSERVICEITEM).Value = "1"    '--save flag in hidden grid column.-
                Else
                    .Cells(k_GRIDCOL_ISSERVICEITEM).Value = "0"    '--save flag in hidden grid column.-
                End If
            End If

            '-- get tax, price and picture.--
            '== txtItemTax.Text = row1.Item("sales_TaxCode")
        End With  '--dgvSaleItems.Rows-

        '-- show picture if any..
        '= 3411.0103=  NO NO.. NOT here now.. Too late..

        '=qty=
        mIntQtyInStock = CInt(row1.Item("qtyInStock"))

        '-track-serial-
        '== If txtItemSerialNo.Enabled Then  '-track-serial-
        '== mDecItemExtension = mDecSellActual  '--Qty one only-
        '== txtItemExtension.Text = txtItemSellPrice.Text
        '== btnLineEnter.Enabled = True  '--can go now..-
        '== End If

        mPanelSaleFooter.Enabled = True
        '= mDgvSalePaymentDetails.Enabled = True
        '= If mbIsQuote Or mbIsRefund Then
        '=     mDgvSalePaymentDetails.Enabled = False
        '= End If

        mPanelOptTranType.Enabled = False  '--must cancel to change tran-type-

        DoEvents()
    End Function  '--SetupSaleStockItem-
    '= = = = = = = = = = = = = = = =  = =
    '-===FF->

    '=3403.510=-- ADD Item to Sale Items Grid..
    '--  For delivering Job, or DElivering Layby..
    '-- Returns index of new row.
    '==  -- 4201.1028/1031.  31-Oct-2019-  
    '==     -- Importing Quote-- MUST use Quoted Selling price...

    Private Function mbAddItemToSalesGrid(ByVal sBarcode As String, _
                                           ByVal intStock_id As Integer, _
                                           ByVal sSerialNo As String, _
                                           ByVal sDescription As String, _
                                           ByVal decQty As Decimal, _
                                           Optional ByVal bUseQuotedSellingPrice As Boolean = False, _
                                           Optional ByVal decQuotedSellingPrice As Decimal = -1) As Integer
        Dim sSql As String
        Dim intRowx, intAudit_id As Integer
        Dim dtStock As DataTable
        Dim dataRow1 As DataRow
        Dim gridRow1 As DataGridViewRow
        Dim bTrackSerial As Boolean = False

        mbAddItemToSalesGrid = -1
        '-- look up barcode and get stock item info..
        If (sBarcode <> "") Then  '--have barcode-
            '--lookup barcode-
            '--  get recordset as collection for SELECT..--
            '==sSql = "SELECT * FROM [stock] WHERE (barcode='" & sBarcode & "');"
            sSql = "SELECT * FROM [stock] WHERE (stock_id=" & CStr(intStock_id) & ");"
            If gbGetDataTable(mCnnSql, dtStock, sSql) AndAlso _
                                   (Not (dtStock Is Nothing)) AndAlso (dtStock.Rows.Count > 0) Then
                dataRow1 = dtStock.Rows(0)  '--only want 1st row.-
                '==intStock_id = dataRow1.Item("stock_id")
                '=4201.0708=
                If CInt(dataRow1.Item("track_serial")) <> 0 Then
                    bTrackSerial = True
                End If
                '- ok.  have stock info. ADD a row to the Sales Grid..
                gridRow1 = New DataGridViewRow
                intRowx = mDgvSaleItems.Rows.Add(gridRow1)
                mDgvSaleItems.Rows(intRowx).Cells(k_GRIDCOL_BARCODE).Value = sBarcode
                '=3301.711= Set sellActual as stock sell..
                If bUseQuotedSellingPrice AndAlso (decQuotedSellingPrice >= 0) Then
                    mDecSellActualTaxAmount = 0
                    mDecSellActualExTax = decQuotedSellingPrice
                    If UCase(dataRow1.Item("sales_taxCode")) = "GST" Then
                        mDecSellActualExTax = mDecComputeAmountExTax(decQuotedSellingPrice)
                        mDecSellActualTaxAmount = decQuotedSellingPrice - mDecSellActualExTax
                    End If  '-gst-
                Else '-use stock price.
                    mDecSellActualExTax = CDec(dataRow1.Item("sellExTax"))
                    mDecSellActualTaxAmount = 0
                    If UCase(dataRow1.Item("sales_taxCode")) = "GST" Then
                        mDecSellActualTaxAmount = ((mDecSellActualExTax * mDecGST_rate) / 100)
                    End If
                End If
                '-- Everyone-
                mDecSellActualIncTax = mDecSellActualExTax + mDecSellActualTaxAmount
                mDecSellItemQty = decQty '=3403.510- for layby.  '3301.710- --emulate Edit Line..
                '--have a row..-
                Call mbSetupSaleStockItem(dtStock, intRowx)
                '--  add serial no if exists..
                If (sSerialNo <> "") Then
                    intAudit_id = -1
                    '- Lookup serialno to validate it exists as a serial.--
                    sSql = "SELECT SA.serial_id, SA.status FROM [serialAudit] AS SA"
                    sSql &= " WHERE (serialNumber='" & sSerialNo & "') AND (stock_id=" & CStr(intStock_id) & ");"
                    If Not gbGetSqlScalarIntegerValue(mCnnSql, sSql, intAudit_id) Then
                        MsgBox("ERROR getting Serial-audit record for " & vbCrLf & _
                           " Serial-no: " & sSerialNo & "  Barcode: " & sBarcode & _
                                 vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    End If
                    If IsDBNull(intAudit_id) OrElse (intAudit_id <= 0) Then
                        MsgBox("ERROR: no Serial-audit record found for " & vbCrLf & _
                          " Serial-no: " & sSerialNo & "  Barcode: " & sBarcode & vbCrLf & vbCrLf & _
                              "No serial audit trail will be written for this item..", MsgBoxStyle.Exclamation)
                    Else  '-ok- save serial audit id.
                        '--ok. save serial audit id for COMMIT processing below..
                        mDgvSaleItems.Rows(intRowx).Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value = CStr(intAudit_id)
                    End If
                    mDgvSaleItems.Rows(intRowx).Cells(k_GRIDCOL_SERIALNO).Value = sSerialNo
                Else  '-no serial-
                    If bTrackSerial Then
                        mDgvSaleItems.Rows(intRowx).Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value = "-2"  '-was bypassed.
                    End If
                End If  '-have serial-
                '- update invoice total--
                Call mbUpdateSaleTotal()
                '=intRowx += 1
            Else '--not found..-
                MsgBox("No Stock record found for Job Part: " & vbCrLf & _
                           " stock_id: '" & intStock_id & "' " & _
                           " barcode: '" & sBarcode & "' " & _
                           " description: '" & sDescription & "' !" & vbCrLf & _
                         vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf & vbCrLf & _
                           "Please Find a replacement part for this sale.", MsgBoxStyle.Exclamation)
                Exit Function
            End If  '-get--
            mbAddItemToSalesGrid = intRowx  '-done-
        End If  '--have barcode-

    End Function  '-mbAddItemToSalesGrid-
    '= = = = = = = = = = = = = = = =  = =
    '-===FF->

    '-- Done Edit.  Add new Grid row..

    Private Function mbEndItemEdit() As Boolean
        Dim intX As Integer
        Dim bBypassSerialNo As Boolean = False
        '=3501.0731- Bypass for Non-stock item..
 
        '=If (mIntCurrentEditRow >= 0) Then  '-edit an existing row..
        '=Else  '-new row-
        If Not (mDatatableEditingItem Is Nothing) Then
            '=3501.0731- Bypass for Non-stock item..
            '=bIsNonStockItem = mDatatableEditingItem.Rows(0).Item("isNonStockItem")
            If mbIsSerialItem AndAlso (Not mbIsQuote) _
                                   AndAlso (Trim(mTxtSaleItemSerialNo.Text) = "") Then
                If (MsgBox("This item should have a serial number.." & vbCrLf & _
                              "Do you have one to enter ?", _
                              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                    mTxtSaleItemSerialNo.Select()
                    Exit Function
                Else '-- try again-
                    If MsgBox("NB: This item really SHOULD have a serial number.." & vbCrLf & _
                                   "Click YES if you're sure there isn't one !!", _
                                MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                        mTxtSaleItemSerialNo.Select()
                        Exit Function
                    End If '- sure ?-
                End If  '-- should have-
                '--CHECK IF WE NEED THIS: 
                '-Yes-
                '- dataRow1.Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value = "-2"  '--record vote for no serial.
                bBypassSerialNo = True
            ElseIf (mbIsSerialItem AndAlso mbIsQuote) Then
                bBypassSerialNo = True
            End If '-serial-

            '-- add grid row -
            Dim row1 = New DataGridViewRow
            intX = mDgvSaleItems.Rows.Add(row1)
            With mDgvSaleItems.Rows(intX)
                .Cells(k_GRIDCOL_BARCODE).Value = mTxtSaleItemBarcode.Text
                .Cells(k_GRIDCOL_SERIALNO).Value = mTxtSaleItemSerialNo.Text
                If bBypassSerialNo Then '==3301.1212-
                    mIntSerialAudit_id = "-2"  '--record vote for no serial.
                    '= .Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value = "-2"  '--record vote for no serial.
                End If
            End With
            Call mbSetupSaleStockItem(mDatatableEditingItem, intX)

            Call mbUpdateSaleTotal()
        End If  '-nothing-
        '= End If  '-new-
        mDgvSaleItems.ClearSelection()
        mTxtSaleItemBarcode.Text = ""
        Call mbClearEditLine()
        mTxtSaleItemSerialNo.Enabled = True
        mBtnSaleLineOk.Enabled = False
        mDatatableEditingItem = Nothing   '--edit line now vacant.
        '=3411.0103=
        mPicSaleItem.Image = Nothing

        '== mTxtSaleItemSerialNo.Enabled = False  '= ReadOnly = False
        '- in case of Back TAB..
        mTxtSaleItemSellPrice.Enabled = False
        mTxtSaleItemQty.Enabled = False

        mPanelSaleFooter.Enabled = True
        '=3401.410= Re-number-
        Call mbNumberGridRows(mDgvSaleItems)

        mTxtSaleItemBarcode.Select()

    End Function  '- mbEndItemEdit--
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '- "ENTER" on barcode -Start edit.  
    '--  get stock table item and set up Edit Line..
    '-- Set up cuurent edit and static vars..
    '==  3411.0407=-- 07-Apr-2018- POS Sale- Strip leading Zeroes on Item Barcode...
    '==         AND-   If barcode not found, Then retry with lead zeroes intact (if any).
    '==

    Private Function mbBeginEdit(ByVal sInputBarcode As String, _
                                  ByRef sFinalBarcode As String) As Boolean
        Dim sSql, sCategory, sBarcode As String
        Dim datatable1 As DataTable
        Dim bHasLeadZeroes As Boolean = False
        Dim bDoneLookUp As Boolean = False

        mbBeginEdit = False  '-if not valid barcode--
        bHasLeadZeroes = (VB.Left(sInputBarcode, 1) = "0")
        sBarcode = sInputBarcode
        '-- start with stripping leading zeroes..
        While (VB.Left(sBarcode, 1) = "0")
            sBarcode = Mid(sBarcode, 2)
        End While
        '--lookup barcode-
        Do
            '--  get recordset as collection for SELECT..--
            sSql = "SELECT * FROM [stock] WHERE (barcode='" & sBarcode & "');"
            If gbGetDataTable(mCnnSql, datatable1, sSql) AndAlso _
                                   (Not (datatable1 Is Nothing)) AndAlso (datatable1.Rows.Count > 0) Then

                '--   NEW item started..-
                '= NOT YET = '--have a row..-
                '= NOT YET = Call mbSetupSaleStockItem(dataTable1, lRow)
                '= NOT YET = '- update invoice total--
                '= NOT YET = Call mbUpdateSaleTotal()
                '==3301.511=

                '-- save datatable for EndEdit.. 
                mDatatableEditingItem = datatable1

                '-- set up item text boxes..
                Dim row1 As DataRow = datatable1.Rows(0)
                '== mTxtSaleItemCategory.Text = row1.Item("cat1") & "-" & row1.Item("cat2")
                '=3401.313= 
                sCategory = row1.Item("cat1") & "-" & row1.Item("cat2")
                mTxtSaleItemDescription.Text = sCategory & ":  " & row1.Item("description")
                mTxtSaleItemSellPrice.Text = row1.Item("sellExTax")

                mIntStock_id = row1.Item("stock_id")
                msGoodsTaxcode = UCase(row1.Item("goods_taxcode"))
                msSalesTaxCode = UCase(row1.Item("Sales_taxcode"))
                '-costExTax-
                mDecCostExTax = CDec(row1.Item("costExTax"))  '--rrp ex tax-

                '-- Must add TAX..
                mDecCostIncTax = mDecCostExTax
                If msGoodsTaxcode = "GST" Then
                    mDecCostIncTax = mDecCostExTax + (mDecCostExTax * mDecGST_rate / 100)
                End If
                '-- show Cost mouseover...
                mToolTip1.SetToolTip(mTxtSaleItemSellPrice, "Cost_ex: " & Format(mDecCostExTax, "  00.00") & ";" & vbCrLf & _
                                               "Tax: " & msGoodsTaxcode & ";" & vbCrLf & _
                                               "Cost_inc: " & Format(mDecCostIncTax, "  00.00") & ".")
                mDecSellActualExTax = CDec(row1.Item("sellExTax"))  '--rrp ex tax-

                '-- get GST rate for this tax code..
                '-mDecGST_rate-
                '- compute rrp incl. for Actual to start-

                '- GET cust Grade and compute ACTUAL price for this cust.. -
                '--NOW- 3403.917= not later-
                '- msCustomerGrade-
                If (msCustomerGrade >= "1") And (msCustomerGrade <= "4") Then  '-have grade-
                    Dim decCostPlusPercent As Decimal
                    Dim sKey As String = "POS_CUSTPRICINGGRADE_COSTPLUS_" & msCustomerGrade
                    If mSysInfo1.contains(sKey) Then
                        If IsNumeric(mSysInfo1.item(sKey)) Then
                            decCostPlusPercent = CDec(mSysInfo1.item(sKey))
                            mDecSellActualExTax = mDecCostExTax + (mDecCostExTax * decCostPlusPercent / 100)
                        End If
                    End If  '-contains.-
                End If  '-grade-

                '==3411.0205= 
                '--  Fix to beginEdit to NOT add tax if Tax COde is NOT GST (eg FRE)... 
                If (msSalesTaxCode = "GST") Then
                    mDecSellActualTaxAmount = ((mDecSellActualExTax * mDecGST_rate / 100))
                Else
                    mDecSellActualTaxAmount = 0
                End If
                mDecSellActualIncTax = mDecSellActualExTax + mDecSellActualTaxAmount

                '=4201.1030  Five-cents ROUNDING for Sell-Actual-
                '--  NOTE THAT THIS IS Done at runtime..
                '--   MUST round to 2 decimals first.
                mDecSellActualIncTax = Decimal.Round(mDecSellActualIncTax, 2)
                mDecSellActualIncTax += mDecGetRoundingAmount(mDecSellActualIncTax)
                '=txtSellIncTax.Text = FormatCurrency(decSellInc, 2)

                mTxtSaleItemSellPrice.Text = FormatCurrency(mDecSellActualIncTax, 2)

                '-- default qty is 1..
                mTxtSaleItemQty.Text = "1"
                mDecSellItemQty = 1
                mTxtSaleItemExtension.Text = mTxtSaleItemSellPrice.Text '= row1.Item("sellExTax")

                '--REMEMBER if NonStockItem. 4201.1007=
                mbIsNonStockItem = False
                If (CInt(row1.Item("isNonStockItem")) <> 0) Then
                    mbIsNonStockItem = True
                End If

                '-- REMEMBER If needs serial...
                '= NO-  mTxtSaleItemSerialNo.ReadOnly = True  '-is no serial-
                If row1.Item("track_serial") Then  '--s/be boolean-
                    mbIsSerialItem = True
                    '--Item with serialno.  can only be ONE of per SerialNo. (per line)..
                    mTxtSaleItemQty.ReadOnly = True
                    'If mbIsQuote Then
                    '    mTxtSaleItemSerialNo.Enabled = False  '=ReadOnly = True    '-- NO serial no for quotes.-
                    'Else '-not quote-
                    '    mTxtSaleItemSerialNo.Enabled = True   '=ReadOnly = False   '-- wants serial no.-
                    'End If '-quote-
                Else  '-no serial tracking.-
                    mbIsSerialItem = False
                    '== mTxtSaleItemSerialNo.Enabled = False  '=ReadOnly = True
                    mTxtSaleItemQty.ReadOnly = False    '-can be more qty.
                    '== mBtnSaleLineOk.Enabled = True
                End If

                '= 3411.0103=  Show Pic HERE if any..
                Call mbShowStockPicture(row1)
                sFinalBarcode = sBarcode  '-in case we stripped..
                bDoneLookUp = True
                mbBeginEdit = True   '-ok to go-
                If bHasLeadZeroes And (VB.Left(sBarcode, 1) = "0") Then  '-succeeded UNstripped version
                    MsgBox("Note: " & vbCrLf & "The Leading zeroes on Item Barcode have been retained..", MsgBoxStyle.Information)
                End If
            Else '--not found..-
                If bHasLeadZeroes And (VB.Left(sBarcode, 1) <> "0") Then  '-failed stripped version
                    sBarcode = sInputBarcode   '--try original unstripped..
                Else  '- no lead zreoes, or failed raw version.
                    mbBeginEdit = False   '--can't go-
                    MsgBox("No Stock record found for barcode: '" & sBarcode & "' !" & _
                             vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    bDoneLookUp = True
                End If
            End If  '-get--
        Loop Until bDoneLookUp

        '== mBtnSaleLineOk.Enabled = True    '--user can commit this line..
    End Function  '-mbBeginEdit-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- get trail records and load Trail collection...-

    Private Function mbGetSerialTrail(ByVal intSerialAudit_id As Integer, _
                                       ByRef colSerialTrail As Collection, _
                                       ByRef sLastTrans As String, _
                                       ByRef intInvoice_id As Integer, _
                                       ByRef sErrorMsg As String) As Boolean
        Dim sSql, s1, sMsg, sName As String
        Dim dtTrail As DataTable
        Dim col1 As Collection

        mbGetSerialTrail = False
        '-- get SerialAudit Trail.  and populate grid. 
        sSql = " SELECT * FROM SerialAuditTrail AS SAT "
        sSql &= "    WHERE (SAT.SerialAudit_Id=" & intSerialAudit_id & ") "
        sSql &= "  ORDER BY trail_date; "
        If gbGetDataTable(mCnnSql, dtTrail, sSql) Then
            colSerialTrail = New Collection
            If (Not (dtTrail Is Nothing)) AndAlso (dtTrail.Rows.Count > 0) Then
                '--populate collection..-
                For Each drTrail As DataRow In dtTrail.Rows
                    '==colSerialTrail.Add(Format(drTrail.Item("trail_date"), "dd-MMM-yyyy"), "trail_date")
                    col1 = New Collection
                    sName = "trail_date"
                    col1.Add(Format(drTrail.Item("trail_date")), "value")
                    col1.Add(sName, "name")
                    colSerialTrail.Add(col1)
                    col1 = New Collection
                    sName = "tran_type"
                    col1.Add(Format(drTrail.Item("tran_type")), "value")
                    col1.Add(sName, "name")
                    colSerialTrail.Add(col1)
                    '= colSerialTrail.Add(drTrail.Item("tran_type"), "tran_type")
                    '= colSerialTrail.Add(Format(drTrail.Item("type_id")), "type_id")
                    '= colSerialTrail.Add(Format(drTrail.Item("RM_tr_detail")), "RM_tr_detail")

                    sLastTrans = drTrail.Item("tran_type")  '-save latest trans. type. 
                    intInvoice_id = drTrail.Item("type_id")
                Next drTrail
            Else  '-no data-  
            End If '-nothing.
            mbGetSerialTrail = True
        Else '--Trail error- 
            sErrorMsg = "Error in getting SerialAudit Trail.. " & vbCrLf & gsGetLastSqlErrorMessage()  '--for caller-
            '= MsgBox("Error in getting SerialAudit Trail.. " & vbCrLf & _
            '=                                   gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
        End If  '-Trail get- 
    End Function  '-mbGetSerialTrail-
    '= = = = = =  = = = = = = ==  =
    '-===FF->

    '- have barcode and stock_id... 
    '--   Lookup serialNo and get info..
    '--  can be Sale or Refund..--

    Private Function mbCheckSerial(ByRef sSerialNumber As String, _
                                    ByVal intStock_id As Integer, _
                                      ByRef bIsInStock As Boolean, _
                                       ByRef intSerialAudit_id As Integer, _
                                       ByRef intInvoice_id As Integer, _
                                       ByRef sSerialTrail As String, _
                                        ByRef sError As String) As Boolean
        Dim sSql As String
        Dim dataTable1 As DataTable
        Dim sLastTrans, sStatus As String
        Dim colSerialTrail As Collection

        mbCheckSerial = False
        sError = ""
        sLastTrans = ""

        '-- Lookup SerialAudit -
        sSql = "SELECT  stock.barcode, stock.cat1, stock.description, "
        sSql &= "     SA.isInStock, SA.serial_id, SA.stock_id, SA.status "
        sSql &= " FROM [serialAudit] AS SA JOIN stock ON (SA.stock_id= stock.stock_id)"
        sSql &= " WHERE (serialNumber='" & sSerialNumber & "') "
        sSql &= " AND (SA.stock_id = " & CStr(intStock_id) & ");"
        If gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
                Dim dataRow1 As DataRow = dataTable1.Rows(0)

                '-- ok- show selected serial info.. 
                intSerialAudit_id = dataRow1.Item("serial_id")
                sStatus = dataRow1.Item("status")
                '-- status can be instock, returnedGoods, sold.  '===3301.526=
                bIsInStock = LCase(dataRow1.Item("status")) = "instock"
                '-- get trail--
                sSerialTrail = "Trail Info:" & vbCrLf
                If mbGetSerialTrail(intSerialAudit_id, colSerialTrail, sLastTrans, _
                                        intInvoice_id, sError) Then
                    If Not colSerialTrail Is Nothing Then
                        For Each col1 As Collection In colSerialTrail
                            sSerialTrail &= col1.Item("name") & ": " & col1.Item("value") & vbCrLf
                        Next col1
                    End If
                End If  '-GetSerialTrail-
                mbCheckSerial = True
            End If  '-nothing-
        Else '--error-
            sError = "Error in getting recordset for SerialAudit table. " & vbCrLf & _
                                           gsGetLastSqlErrorMessage() & vbCrLf
            Exit Function
        End If  '-get-

    End Function  '-mbCheckSerial-
    '= = = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '--No barcode.. Lookup serialNo and get user to choose..
    '--  can be Sale or Refund..--

    Private Function mbFindSerial(ByVal sSerialNo As String, _
                                    ByVal intInputStock_id As Integer, _
                                     ByRef intAudit_id As Integer, _
                                      ByRef intStock_id As Integer, _
                                       ByRef sBarcode As String, _
                                        ByRef intSalesInvoiceNo As Integer) As Boolean
        '== Dim sSql, s1, sMsg As String
        '== Dim dataTable1 As DataTable
        '== Dim ix, intInvoice_id As Integer
        '== Dim frmListSelect1 As frmListSelect
        Dim frmFind1 As frmFindSerial

        mbFindSerial = False
        '-- load find serial form..
        If (intInputStock_id > 0) Then
            '==sSql &= " AND (SA.stock_id = " & CStr(intInputStock_id) & ");"
            frmFind1 = New frmFindSerial(mCnnSql, sSerialNo, intInputStock_id, mbIsRefund)
        Else
            frmFind1 = New frmFindSerial(mCnnSql, sSerialNo, mbIsRefund)
        End If

        frmFind1.ShowDialog(mFrmSale)
        mFrmSale.Visible = True

        If frmFind1.cancelled Then
            '== MsgBox("Serial lookup cancelled.", MsgBoxStyle.Exclamation)
            frmFind1.Close()
            Exit Function
        Else
            'sError = frmFind1.errorText
            'If sError <> "" Then
            '    frmFind1.Close()
            '    Exit Function
            'End If
        End If
        '-ok- get results.
        intAudit_id = frmFind1.serial_id
        intStock_id = frmFind1.stock_id
        sBarcode = frmFind1.barcode
        intSalesInvoiceNo = frmFind1.salesInvoiceNo
        frmFind1.Close()

        mbFindSerial = True
        Exit Function

        '===  DELETE the rest..===
        '== deleted..-

    End Function  '--find serial
    '= = = = = = = = = = = = = = = =  = =
    '-===FF->

    '-- J o b T r a c k i n g--
    '-- J o b T r a c k i n g--

    Private Const K_GOODS_ONSITEJOB As String = "ON-SITE JOB;"   '--3083.312--

    '-- JobTracking-- check if Jobs to deliver for this customer..
    '--  Choose and set-up Job items and invoice info..

    Private Function mbDeliveringJob() As Integer

        Dim sSql, sWhere, sMsg As String
        Dim dtJobs, dtStock, dtSerialAudit As DataTable
        Dim dataRow1 As DataRow
        Dim colPrefsJobs As Collection
        Dim colKeys, colSelectedRow As Collection
        Dim colJobItems, colItem As Collection
        Dim sBarcode, sSessionTimes As String
        Dim sPriority, sSerialNo, sDescription As String
        Dim bIsOnSiteJob As Boolean = False
        Dim decLabourHours As Decimal
        Dim intStock_id, intResultStock_id, intAudit_id As Integer
        Dim intSalesInvoiceId As Integer
        Dim gridRow1 As DataGridViewRow
        Dim intRowx As Integer

        mbDeliveringJob = -1
        '-- get latest system Info table data.-
        Call mbRefreshSystemInfo()

        colPrefsJobs = New Collection
        colPrefsJobs.Add("job_id")
        colPrefsJobs.Add("JobStatus")
        colPrefsJobs.Add("TechStaffName")
        colPrefsJobs.Add("GoodsInCare")
        colPrefsJobs.Add("dateCompleted")
        sWhere = " (RMCustomer_id = " & CStr(mIntSaleCustomer_id) & ") AND (Left(JobStatus, 2) = '50') "

        '-- check if any jobs to deliver for this cust..
        sSql = "SELECT job_id, JobStatus, GoodsInCare, dateCompleted, TechStaffName "
        sSql &= " FROM [Jobs] WHERE " & sWhere & "; "
        If Not gbGetDataTable(mCnnSql, dtJobs, sSql) Then
            MsgBox("Error getting Jobs table.." & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If (dtJobs Is Nothing) OrElse (dtJobs.Rows.Count <= 0) Then
            Exit Function
        End If  '--nothing

        '-- ask if delivery happening.-
        sMsg = "There are " & CStr(dtJobs.Rows.Count) & " Job(s) ready to deliver for this customer." & vbCrLf & _
                          "Do you want to deliver a job now ?"
        If (MsgBox(sMsg, _
              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
            Exit Function
        End If

        '-- deliver--
        If dtJobs.Rows.Count = 1 Then
            mIntJob_id = dtJobs.Rows(0).Item("Job_id")
        Else  '--many-
            If Not mbBrowseTable(colPrefsJobs, "Jobs to Deliver for: ", sWhere, colKeys, colSelectedRow, "Jobs", True) Then
                MsgBox("Jobs Lookup cancelled.", MsgBoxStyle.Exclamation)
                Exit Function
            Else '-- chosen-
                If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                    mIntJob_id = CInt(colKeys(1))
                Else
                    MsgBox("No Job no. found..", MsgBoxStyle.Exclamation)
                    Exit Function
                End If
            End If  '-browse-
        End If

        If (mIntJob_id > 0) Then
            '-- have job -
            '= mTxtSaleJobNo.Text = CStr(mIntJob_id)
            '== MsgBox("Getting info to deliver Job No: " & mIntJob_id, MsgBoxStyle.Information)
            mLabSaleJobDelivery.Enabled = True
            '-- retrieve info for selected job..
            sSql = "SELECT * FROM [Jobs] WHERE (Job_id= " & CStr(mIntJob_id) & "); "
            If Not gbGetDataTable(mCnnSql, dtJobs, sSql) Then
                MsgBox("Error getting Job record.." & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                Exit Function
            End If
            If (dtJobs Is Nothing) OrElse (dtJobs.Rows.Count <= 0) Then
                Exit Function
            End If  '--nothing

            '-Test-
            '= MsgBox("Delivering Job: " & mIntJob_id, MsgBoxStyle.Information)

            sSessionTimes = dtJobs.Rows(0).Item("SessionTimes")
            sPriority = dtJobs.Rows(0).Item("Priority")
            decLabourHours = gCurComputeChargeableHours(sSessionTimes)  '--jobSubs.
            '=3411.0107= "Diagnosis" used as JobReport.
            msSaleJobReport = Trim(dtJobs.Rows(0).Item("Diagnosis"))

            '-- OnSite -- and Update JobInfo text..
            '==3401.318==
            Dim sGoodsIncare As String = UCase(Trim(dtJobs.Rows(0).Item("GoodsInCare")))
            '= MsgBox("GoodsInCare is: " & sGoodsIncare, MsgBoxStyle.Information)

            bIsOnSiteJob = (sGoodsIncare = K_GOODS_ONSITEJOB)
            'If (UCase(Trim(dtJobs.Rows(0).Item("GoodsInCare")("value"))) = K_GOODS_ONSITEJOB) Then
            '    bIsOnSiteJob = True
            'End If
            '= MsgBox("OnSiteJob is: " & IIf(bIsOnSiteJob, "yes", "No"), MsgBoxStyle.Information)
            '= mTxtSaleJobNo.Text = CStr(mIntJob_id)
            mTxtSaleJobNo.Text = "Job #" & CStr(mIntJob_id) & "; Pr:" & sPriority & IIf(bIsOnSiteJob, "; ONSITE", ".")
            DoEvents()

            '-test- 
            MsgBox("JobInfo is: " & vbCrLf & mTxtSaleJobNo.Text, MsgBoxStyle.Information)

            '=If bIsOnSiteJob Then
            '= mTxtSaleJobNo.TextAlign = HorizontalAlignment.Left
            'If mTxtSaleJobNo.Width < 100 Then
            '    mTxtSaleJobNo.Width += 120
            'End If
            ''= End If '-onsite-
            '--  get all items attached to Job..
            If Not gbCollectAllJobParts(mCnnSql, mIntJob_id, colJobItems) Then
                MsgBox("No Job Parts info found..", MsgBoxStyle.Exclamation)
                '== Exit Function
            Else  '-ok-
                '-- add all parts to grid.
                '-- add barcode, and lookup stock for all info..
                '--  add serial no if exists..
                '-- Update invoice total..
                mDgvSaleItems.Rows.Clear()
                intRowx = 0
                For Each colItem In colJobItems
                    sBarcode = colItem.Item("barcode")
                    sSerialNo = colItem.Item("serialNo")
                    intStock_id = CInt(colItem.Item("stock_id"))
                    sDescription = colItem.Item("Description")  '--fixed 3401.318=

                    '=3403.510= ADD TO GRID-
                    intRowx = mbAddItemToSalesGrid(sBarcode, intStock_id, sSerialNo, sDescription, 1)
                    If Not (intRowx >= 0) Then
                        MsgBox("Failed to add the item '" & sDescription & " to Grid.. ", MsgBoxStyle.Exclamation)
                    End If '--add item.
                    ''-- look up barcode and get stock item info..
                     DoEvents()
                Next colItem
            End If  '--collect-
            '-- Add Labour Item if any..
            '==  USE JobTracking Rates ALWAYS as we don't need POS labour..
            '==  USE JobTracking Rates ALWAYS as we don't need POS labour..
            If (decLabourHours > 0) Then
                Dim intLabourStock_id As Integer = -1
                Dim strLabourStockBarcode As String = ""
                Dim decOldHourlyRateInc As Decimal = 0  '-in case not set up-
                Dim strOldDescription As String = ""

                '-- get labour stock item from setup via priority..-
                '== MsgBox("Priority: " & sPriority & "  id=" & mIntPOS_labourStockId_pr1, MsgBoxStyle.Information)
                If Not gbGetPriorityInfoPOS(mCnnSql, sPriority, bIsOnSiteJob, _
                                         strLabourStockBarcode, _
                                         decOldHourlyRateInc, strOldDescription) Then
                    strLabourStockBarcode = ""
                    MsgBox("Error in getting Labour Rate.. Use manual lookup.", MsgBoxStyle.Exclamation)
                End If
                '-- press on..-
                If (strLabourStockBarcode <> "") Then  '= (intLabourStock_id > 0) Then '--lookup stock table below..-
                    '--lookup stock table below..-
                Else '-no def.. ask operator.
                    '--ask operator to browse for Stock Labour Item..
                    '-- add labour item to grid-
                    MsgBox("Still to do: This Job has " & CStr(decLabourHours) & " Labour Hours charged to it." & vbCrLf & _
                               " Please look up the stock table and " & vbCrLf & _
                                " select the stock item that corresponds to " & vbCrLf & _
                                " Priority " & sPriority & " labour on this job.", MsgBoxStyle.Information)
                    '-- now browse stock and select labour stock item to add to grid..
                    If Not mbBrowseTable(mColPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, "Stock", True) Then
                        MsgBox("Lookup cancelled." & vbCrLf & _
                                 "You'll have to add the Labour Cost manually.", MsgBoxStyle.Exclamation)
                    Else
                        If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                            strLabourStockBarcode = colSelectedRow.Item("barcode")("value")
                            intLabourStock_id = colSelectedRow.Item("stock_id")("value")
                        Else
                            MsgBox("No selection.", MsgBoxStyle.Exclamation)
                        End If '-selected
                    End If  '-browse.-
                End If  '-ask-
                '--lookup--
                If (strLabourStockBarcode <> "") Then  '= (intLabourStock_id > 0) Then
                    '-- intRowx is tracking rows added..
                    '=3401.318= sSql = "SELECT * FROM [stock] WHERE (stock_id=" & CStr(intLabourStock_id) & ");"
                    sSql = "SELECT * FROM [stock] WHERE (barcode='" & strLabourStockBarcode & "' );"
                    If gbGetDataTable(mCnnSql, dtStock, sSql) AndAlso _
                                           (Not (dtStock Is Nothing)) AndAlso (dtStock.Rows.Count > 0) Then
                        '-- emulate BeginEdit--
                        Dim datarowStock1 As DataRow = dtStock.Rows(0)

                        '- ok.  have stock info. ADD a row to the Sales Grid..
                        gridRow1 = New DataGridViewRow
                        '=3411.0312= was over-writinf last row..  mDgvSaleItems.Rows.Add(gridRow1)
                        intRowx = mDgvSaleItems.Rows.Add(gridRow1)  '=3411.0312= Needs index of new row,
                        mDgvSaleItems.Rows(intRowx).Cells(k_GRIDCOL_BARCODE).Value = strLabourStockBarcode

                        '-- emulate Text Line Entry. and BeginEdit--
                        '-- get GST rate for this tax code..
                        '-mDecGST_rate-
                        '- compute rrp incl. for Actual to start-
                        mDecSellActualExTax = CDec(datarowStock1.Item("sellExTax"))  '--rrp ex tax-

                        '- GET cust Grade and compute ACTUAL price for this cust.. -
                        '--later-
                        mDecSellActualTaxAmount = ((mDecSellActualExTax * mDecGST_rate / 100))
                        mDecSellActualIncTax = mDecSellActualExTax + mDecSellActualTaxAmount

                        '--have a row..-
                        Call mbSetupSaleStockItem(dtStock, intRowx)
                        '-fix actual qty of hours..-
                        mDgvSaleItems.Rows(intRowx).Cells(k_GRIDCOL_QTY).Value = CStr(decLabourHours)
                        Call mbUpdateSaleStockItem(intRowx, CStr(decLabourHours))
                        '- update invoice total--
                        Call mbUpdateSaleTotal()
                        intRowx += 1
                    Else
                        MsgBox("Can't find that item..", MsgBoxStyle.Exclamation)
                    End If  '-get
                End If  '-stock_d-
            End If  '--has hours..
            mLabSaleHelp.Text = "Adjust Items/Payments as needed, and Commit when ready."

            mbDeliveringJob = mIntJob_id
        End If '-job_id-
        '= dtStock.Dispose()
        '= gridRow1.Dispose()

    End Function  '-mbDeliveringJob-
    '= = = = = = = = = = = = = = = =  = =
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-mbShowCreditNoteInfo-

    Private Function mbShowCreditNoteInfo() As Boolean
        Dim listViewItem1 As ListViewItem
        Dim dtPayments As DataTable
        Dim decCredits As Decimal = 0
        Dim decDebits As Decimal = 0

        '- get all credit notes.. (as selected).

        If Not gbGetCreditNoteHistory(mCnnSql, mIntSaleCustomer_id, dtPayments, _
                                            decCredits, decDebits, mDecCreditNoteCreditRemaining) Then
            MsgBox("Failed Looking up credit notes.. ", MsgBoxStyle.Exclamation)
            Exit Function
        Else  '-ok-
            If (dtPayments IsNot Nothing) AndAlso (dtPayments.Rows.Count > 0) Then
                Dim intTrCount As Integer = dtPayments.Rows.Count
                '= MsgBox("Found " & intTrCount & " credit note transactionss..", MsgBoxStyle.Information)
            Else
                '= MsgBox("No credit note transaction found..", MsgBoxStyle.Information)
            End If  '-dt nothing-
        End If  '--get-

        '- update credit display..
        listViewItem1 = mListViewSaleAvailCredit.Items.Add("CreditNotes:")  '--First col does CREATE..-
        listViewItem1.SubItems(0).ForeColor = Color.DarkGreen
        listViewItem1.SubItems.Add(FormatCurrency(decCredits, 2))
        listViewItem1 = mListViewSaleAvailCredit.Items.Add("-Redeemed:")  '--First col does CREATE..-
        listViewItem1.SubItems.Add(FormatCurrency(decDebits, 2))
        listViewItem1 = mListViewSaleAvailCredit.Items.Add("Available:")  '--First col does CREATE..-
        listViewItem1.SubItems(0).ForeColor = Color.DarkGreen
        listViewItem1.SubItems.Add(FormatCurrency(mDecCreditNoteCreditRemaining, 2))

    End Function  '-mbShowCreditNoteInfo-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Setup Sale Customer-

    Private Function mbSetupSaleCustomer(ByRef colSelectedRow As Collection) As Boolean
        Dim s1, s2, sSql As String
        Dim int1 As Integer
        Dim dtPayments As DataTable
        Dim decCredits As Decimal = 0
        Dim decDebits As Decimal = 0
        Dim clsCustTags1 As clsTags

        mbStaffTimeoutSuspended = True
        mIntSaleCustomer_id = CInt(colSelectedRow.Item("customer_id")("value"))

        '==If colSelectedRow.Contains("barcode") Then
        mTxtSaleCustBarcode.Text = colSelectedRow.Item("barcode")("value")
        msSaleCustomerBarcode = Trim(mTxtSaleCustBarcode.Text)
        '==End If
        If colSelectedRow.Contains("companyName") Then
            mTxtSaleCustName.Text = colSelectedRow.Item("companyName")("value")
            '= txtCustName.Text = col1.Item("value")
        End If
        If mTxtSaleCustName.Text <> "" Then mTxtSaleCustName.Text &= vbCrLf
        If colSelectedRow.Contains("firstName") Then
            mTxtSaleCustName.Text &= colSelectedRow.Item("firstName")("value") & " "
            '=txtCustName.Text = col1.Item("value")
        End If
        If colSelectedRow.Contains("lastName") Then
            mTxtSaleCustName.Text &= colSelectedRow.Item("lastName")("value")
        End If
        '-save for RESTORE..-
        msSaleCustomerName = mTxtSaleCustName.Text
        '= 4221.0207-
        '= Show Cust Tags if any..
        Dim colTags As Collection
        Dim sList As String = ""
        clsCustTags1 = New clsTags(mCnnSql)
        If clsCustTags1.GetCustomerTags(mIntSaleCustomer_id, s1, s2, colTags) Then
            For Each sTag As String In colTags
                If sList <> "" Then
                    sList &= vbCrLf
                End If
                sList &= sTag
            Next sTag
            mToolTip1.SetToolTip(mLabSaleCustTags, "Tags are:" & vbCrLf & sList)
            mLabSaleCustTags.Text = sList
        End If '-get-

        If colSelectedRow.Contains("email") Then
            msCustomerEmail = colSelectedRow.Item("email")("value")
        End If
        DoEvents()

        mDecAccountCustCreditLimit = 0
        mDecAccountCustLimitRemaining = 0
        '= mLabSaleAvailCredit.Text = ""
        mListViewSaleAvailCredit.Items.Clear()

        '=3519.0414=
        msAgedOverDueAmountsList = ""

        '-"creditLimit"-  mDecCustCreditLimit -
        '=mDecCreditNoteCreditRemaining = 0
        '== - pricingGrade -
        '=3403.917= Dim sGrade As String = colSelectedRow.Item("pricingGrade")("value")
        '=3403.917=
        msCustomerGrade = colSelectedRow.Item("pricingGrade")("value")

        int1 = CInt(colSelectedRow.Item("isAccountCust")("value"))
        Dim colInvoices As Collection
        Dim decTotalInvoices, decTotalOutstanding As Decimal
        Dim listViewItem1 As ListViewItem
        Dim sPricingGrade As String = ""

        If (int1 <> 0) Then
            mbSaleIsAccountCust = True
            '= mTxtSalePricingGrade.Text = "Acc/" & sGrade
            sPricingGrade = "ACCOUNT Cust./" & msCustomerGrade '=sGrade
            mLabSaleAccountSalesInfo.Text = k_SALES_CHARGED_TO_ACCOUNT
            mChkOnAccount.Enabled = True
            mChkOnAccount2.Enabled = True  '--keep aligned.

            mGrpBoxRefundType.Visible = False
            '- No laybys for accounts-
            'mCboTransaction.Items.Clear()
            'mCboTransaction.Items.Add("Sale")
            'mCboTransaction.Items.Add("Refund")

            ''=3403.1014-  Can now do layby's..
            'mCboTransaction.Items.Add("Layby")
            'mCboTransaction.Items.Add("Quote")
            'mCboTransaction.SelectedIndex = -1

            '== mChkSaleChargeBalance.Enabled = True
            mDecAccountCustCreditLimit = CDec(colSelectedRow.Item("creditLimit")("value"))
            '-- Temp.. remaining.. 
            mDecAccountCustLimitRemaining = mDecAccountCustCreditLimit
            '=3403.1014-  from orig, ht..
            mListViewSaleAvailCredit.Height = (mIntListViewSaleAvailCredit_height * 2) + 5  '-double orig ht.


            '-- DO invoice/payments search to get outstanding debt..

            '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
            '==   Target-New-Build-4267 -- (Started 07-Sep-2020)

            '==     Use clsDebtors to collect and show actual outstanding only. 
            '==      (Excluding Reversed Invoices.)

            '-- get info-
            Dim clsDebtors1 As clsDebtors
            Dim bOUtstandingInvoicesOnly As Boolean = True
            Dim intClosedDaysToShow As Integer = 0
            Dim dateCutoff As Date = Today
            Dim colReportCustomers As Collection
            Dim intRequestedCustomer_id As Integer = mIntSaleCustomer_id
            Dim colCurrentReportCustomer As Collection

            clsDebtors1 = New clsDebtors(mCnnSql, msSqlDbName, _
                                           mColSqlDBInfo, mColPrefsCustomer, _
                                           msDLLVersion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
            '-- ALWAYS get Outstanding only..
            If Not clsDebtors1.GetAllDebtorReportInfo(bOUtstandingInvoicesOnly, intClosedDaysToShow, _
                                                       dateCutoff, colReportCustomers, intRequestedCustomer_id) Then
                '= gbCollectCustomerInvoices(mCnnSql, mIntSaleCustomer_id, True, Today, colInvoices, 
                '--  decTotalInvoices, decTotalOutstanding, 0) Then

                '== END Target-New-Build-4267 -- (Started 07-Sep-2020)
                '== END Target-New-Build-4267 -- (Started 07-Sep-2020)
                '== END Target-New-Build-4267 -- (Started 07-Sep-2020)


                '== Exit For  '=failed-
            Else '--ok-

                '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
                '==   Target-New-Build-4267 -- (Started 07-Sep-2020)

                If (Not (colReportCustomers Is Nothing)) AndAlso (colReportCustomers.Count > 0) Then
                    colCurrentReportCustomer = colReportCustomers.Item(1)
                    '=colCurrentCustomerInfo = colCurrentReportCustomer.Item("CustInfo")  '--from caller..-
                    colInvoices = colCurrentReportCustomer.Item("Invoices")  '--CurrentInvoices..-
                Else  '-nothing
                    colInvoices = New Collection  '-fake it..
                End If  '--colReportCust.
                decTotalOutstanding = 0
                '- Compute total outst. for customer.
                For Each colInvoice1 As Collection In colInvoices
                    decTotalOutstanding += colInvoice1.Item("amountOutstanding")
                Next colInvoice1

                '== END  Target-New-Build-4267 -- (Started 07-Sep-2020)
                '== END  Target-New-Build-4267 -- (Started 07-Sep-2020)



                listViewItem1 = mListViewSaleAvailCredit.Items.Add("Limit: ")  '--First col does CREATE..-
                listViewItem1.SubItems.Add(FormatCurrency(mDecAccountCustCreditLimit, 2))
                listViewItem1 = mListViewSaleAvailCredit.Items.Add("Outstand: ")  '--First col does CREATE..-
                listViewItem1.SubItems.Add(FormatCurrency(decTotalOutstanding, 2))
                listViewItem1 = mListViewSaleAvailCredit.Items.Add("Available: ")  '--First col does CREATE..-
                listViewItem1.SubItems.Add(FormatCurrency((mDecAccountCustCreditLimit - decTotalOutstanding), 2))
            End If  '--collect-
            '-- 3307.0211-
            mDecAccountCustLimitRemaining = mDecAccountCustCreditLimit - decTotalOutstanding
            '==
            '==   Updated.- 3519.0414  Started 12-April-2019= 
            '==    -- SALES-  Warn operator if Account Cust has outst. OVERDUE amounts (30+ days). 
            '==    
            '=3519.0414=
            Dim intDaysAged As Integer
            Dim decOutstanding As Decimal
            Dim decAgedCurrent As Decimal = 0
            Dim decAged30 As Decimal = 0
            Dim decAged60 As Decimal = 0
            Dim decAged90 As Decimal = 0

            msAgedOverDueAmountsList = ""
            For Each colInvoice1 As Collection In colInvoices
                decOutstanding = CDec(colInvoice1.Item("amountOutstanding"))
                If decOutstanding > 0 Then
                    '-- Age the outstanding on this invoice...
                    intDaysAged = DateDiff(DateInterval.Day, colInvoice1.Item("invoice_date"), Today)
                    If (intDaysAged <= 30) Then
                        decAgedCurrent += decOutstanding
                    ElseIf (intDaysAged <= 60) Then
                        decAged30 += decOutstanding
                    ElseIf (intDaysAged <= 90) Then
                        decAged60 += decOutstanding
                    Else  '--90 and over-
                        decAged90 += decOutstanding
                    End If  '-days-
                End If
            Next colInvoice1
            '-- check if any overdue.
            If (decAged30 > 0) Or (decAged60 > 0) Or (decAged90 > 0) Then
                msAgedOverDueAmountsList = "-- Current Month:  " & FormatCurrency(decAgedCurrent, 2) & ";" & vbCrLf & vbCrLf
                msAgedOverDueAmountsList &= "-- Overdue 30+ Days: " & FormatCurrency(decAged30, 2) & ";" & vbCrLf
                msAgedOverDueAmountsList &= "-- Overdue 60+ Days: " & FormatCurrency(decAged60, 2) & ";" & vbCrLf
                msAgedOverDueAmountsList &= "-- Overdue 90+ Days: " & FormatCurrency(decAged90, 2) & ";" & vbCrLf
            End If  '-overdue-
            '-- This will be reported before any Sale continues..
            '-- END of Aging..

            '-- Account Cust can have Credit Notes also..
            Call mbShowCreditNoteInfo()

            mLabSaleChargeBalance.Visible = True
            '= mTxtSaleCashout.Enabled = False   '--NO cashout for Accounts..-
            mLabSaleChargeBalance.Text = "If On-account, invoice will be debited to Cust. A/c."
        Else '-zero-(Not Account cust-)
            mbSaleIsAccountCust = False
            '== mTxtSalePricingGrade.Text = "CSH/" & sGrade
            sPricingGrade = "CASH Cust- /" & msCustomerGrade  '=sGrade
            mChkOnAccount.Enabled = False
            mChkOnAccount2.Enabled = False

            mLabSaleAccountSalesInfo.Text = "Not an account Customer.." & vbCrLf & _
                                            "Payment required on Sale." & vbCrLf & _
                                            " (Note: Layby deposits are accumulated as Credit Note credits..)"
            '- Can have layby's -
            'mCboTransaction.Items.Clear()
            'mCboTransaction.Items.Add("Sale")
            'mCboTransaction.Items.Add("Refund")
            'mCboTransaction.Items.Add("New Layby")
            'mCboTransaction.Items.Add("Quote")
            'mCboTransaction.SelectedIndex = -1

            mGrpBoxRefundType.Visible = True

            '-- Check for Credit Notes..
            mDecCreditNoteCreditRemaining = 0

            '=3519.0217= 
            '---- must start off visible.
            mLabSaleChargeBalance.Visible = True
            mLabSaleChargeBalance.Text = ""  '= "Overpayment wll be credited to Customer Account."
            '- get result of creditNotes -DebitNotes.
            '--  This can come from Refund,  or from Sale Transactions with extra payment.
            '--   Mutually exclusive with-CreditNote Amount Debited.
            '= sCreateSql &= "  creditNoteAmountCredited MONEY NOT NULL DEFAULT 0,"
            '--  This amount was spent in paying for the SALE..-
            '= sCreateSql &= "  creditNoteAmountDebited MONEY NOT NULL DEFAULT 0,"

            '-- Get all Payments for customer that have Credit-Note credit or debit..
            '=3307.0212==
            '- get all credit notes.. (as selected).

            ''- update credit display..
            '=3403.1014-  from orig, ht..
            mListViewSaleAvailCredit.Height = mIntListViewSaleAvailCredit_height  '-just orig ht.

            Call mbShowCreditNoteInfo()
        End If  '--not account--

        '=3401.321-  - show cust grade-
        mTxtSaleCustName.Text &= vbCrLf & "Grade:  " & sPricingGrade

        '- save for restore-
        msSaleCustomerGrade = sPricingGrade '= mTxtSalePricingGrade.Text
        '-- clear grid.-
        Call mbClearInvoice()
        mPanelSaleInvoiceList.Enabled = True

        '= 3401.322=  mOptSaleInvoiceList.Checked = False
        '= 3401.322=  mOptSaleInvoiceList.Checked = True  '--trip event.-

        '-  load list of invoices for this cust..--
        '= Call mbLoadInvoiceList(mIntSaleCustomer_id)

        '=DEFAULTS to SALE= mLabSaleCust.Enabled = False
        mTxtSaleCustBarcode.Enabled = False

        '=DEFAULTS to SALE= 
        '= mLabSaleTranType.Enabled = True
        mLabSalePayments.Text = "-- P a y m e n t s --"

        '-- Now customer is chosen FIRST (before tranType)--
        mPanelOptTranType.Enabled = False  '==True
        '=DEFAULTS to SALE= mLabChooseTrans.Visible = True
        '=DEFAULTS to SALE= mLabSaleHelp.Text = "Choose Type of Transaction.." '== "Scan or Enter barcodes for sales items."
        '=DEFAULTS to SALE= mDgvSaleItems.Enabled = False  '= True

        mOptSaleSale.Checked = True
        '= mCboTransaction.SelectedIndex = 0
        Call mbSetupSaleDefault()
        mbCreditLimitWarningGiven = False

        '=- If JobTracking application-
        '--  Check if any jobs to deliver for this customer.
        Dim intJobId As Integer = -1
        Dim intLabyId As Integer = -1
        Dim colLaybys, colCust, colThisCustLaybys As Collection  '-customers/layby's/items.
        Dim colLaybyInfo As Collection
        Dim intRowx As Integer
        Dim intQuoteCount As Integer = 0  '--for Converter..

        '= mBtnImportQuote.Enabled = False
        mLabImportQuote.Enabled = False

        '= If (msTransactionType = "sale") AndAlso mColSqlDBInfo.Contains("jobs") Then
        '= SALE tran not chosen yet..
        If mColSqlDBInfo.Contains("jobs") Then
            intJobId = mbDeliveringJob()
        End If  '-jobtracking-

        '=3403.430= --  Check if any Undelivered Layby's on the shelf for this customer.
        If gbCollectCustomerLaybys(mCnnSql, colLaybys, mIntSaleCustomer_id) Then
            If (colLaybys IsNot Nothing) AndAlso (colLaybys.Count > 0) Then
                colCust = colLaybys.Item(CStr(mIntSaleCustomer_id))
                colThisCustLaybys = colCust.Item("laybys")
                '-test-
                s1 = ""
                For Each colThisLayby As Collection In colThisCustLaybys
                    s1 &= "LayByNo: " & colThisLayby.Item("layby_id") & _
                           ";  TotalAmt: " & CStr(colThisLayby.Item("total_inc"))
                Next colThisLayby
                '== MsgBox("Customer has " & colThisCustLaybys.Count & " laybys pending. viz:" & _
                '=                                                vbCrLf & s1, MsgBoxStyle.Information)
            End If
        End If  '--collect-
        '=3519.0330--
        '= Private mBtnImportQuote As Button-
        '---  Check if customer has quotes-  in Last 12 months.
        Dim sQuotesSql As String = "SELECT COUNT(*) FROM dbo."
        Dim intQuoteResult As Integer = 0
        sQuotesSql = "SELECT COUNT(*) FROM dbo.SalesOrder "
        sQuotesSql &= " WHERE (Customer_id=" & CStr(mIntSaleCustomer_id) & ")"
        sQuotesSql &= " AND (DATEDIFF (month,SalesOrder_date, GETDATE()) <=12); "
        If gbGetSqlScalarIntegerValue(mCnnSql, sQuotesSql, intQuoteResult) Then
            intQuoteCount = intQuoteResult
        End If
        mBtnCancelSale.Enabled = True
        mBtnCancelSale2.Enabled = True

        If (intJobId > 0) Then  '--have job.. Is setted up..
            '-- emulate Continue button..-
            mPanelOptTranType.Enabled = False
            mLabSaleHelp.Text = "Scan or Enter barcodes for sales items."
            mPanelSaleLineEntry.Enabled = True
            mTxtSaleItemBarcode.Select()
        Else '-no job-
            '=3403.502= check if layby's-
            If (colThisCustLaybys IsNot Nothing) AndAlso (colThisCustLaybys.Count > 0) Then
                '-- check if layby is to be DELIVERED..

                If MsgBox("This customer has " & colThisCustLaybys.Count & " layby's on the shelf." & vbCrLf & _
                        "Does Customer wish to take delivery of a Layby ?", _
                       MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    '-- show laybys this cust.   ask to Complete or add payment.

                    '=4201.0528= YES..  make a list here inline,  and choose.
                    '=4201.0528= YES..  make a list here inline,  and choose.
                    '=4201.0528= YES..  make a list here inline,  and choose.
  
                    Dim colFirstItem As Collection
                    Dim sChoice, intChosenId As String
                    Dim intListIdx As Integer

                    intChosenId = -1
                    If (colThisCustLaybys.Count = 1) Then
                        colLaybyInfo = colThisCustLaybys.Item(1)
                        intChosenId = colLaybyInfo.Item("layby_id")
                    Else '- user chooses..
                        s1 = ""
                        Dim colThisLayby As Collection
                        For intListIdx = 1 To colThisCustLaybys.Count '= Each colThisLayby As Collection In colThisCustLaybys
                            colThisLayby = colThisCustLaybys.Item(intListIdx)
                            colFirstItem = colThisLayby.Item("items")(1)
                            s1 &= CStr(intListIdx) & ". LayByNo: " & colThisLayby.Item("layby_id") & _
                                ";  " & colFirstItem("description") & _
                                   ";  TotalAmt: " & CStr(colThisLayby.Item("total_inc")) & vbCrLf
                        Next intListIdx '= colThisLayby
                        sChoice = Trim(InputBox("Customer has " & colThisCustLaybys.Count & " laybys pending. " & vbCrLf & _
                                                "Pls Choose from list:" & _
                                                                       vbCrLf & s1, "Layby Choice"))
                        If (sChoice <> "") AndAlso mbIsNumeric(sChoice) AndAlso _
                                    (CInt(sChoice) > 0) AndAlso (CInt(sChoice) <= colThisCustLaybys.Count) Then
                            colLaybyInfo = colThisCustLaybys.Item(CInt(sChoice))
                            intChosenId = colLaybyInfo.Item("layby_id")
                        Else '-will fail below.
                            MsgBox("No choice made..", MsgBoxStyle.Exclamation)
                        End If
                    End If  '-count-

                    'Dim frmSaleDummy As New Form
                    'Dim frmlayby1 As New ucChildLaybys(msComputerName, frmSaleDummy, mCnnSql, _
                    '              msSqlDbName, msDLLVersion, mIntSaleStaff_id, msSaleStaffName)
                    ''= frmlayby1.laybys = colLaybys
                    'frmlayby1.saleCustomer_id = mIntSaleCustomer_id
                    'frmlayby1.ShowDialog()

                    '- if not cancelled..  get layby and dump into a SALE..

                    If intChosenId > 0 Then  '= Not frmlayby1.cancelled Then
                        Dim decLaybyDiscount_nett, decLaybyDiscount_tax, decLaybyDiscount_inc As Decimal
                        Dim sBarcode, sSerialNo, sDescription As String
                        Dim decQty, decAmt As Decimal
                        Dim intStock_id As Integer
                        '-- get/save layby no..
                        intLabyId = intChosenId  '= frmlayby1.selectedLaybyId
                        mIntDeliveredLayby_id = intLabyId
                        mbIsDeliveringLayby = True

                        '= colLaybyInfo = frmlayby1.selectedLaybyInfo
                        decAmt = colLaybyInfo.Item("total_inc")
                        decLaybyDiscount_nett = colLaybyInfo.Item("discount_nett")
                        decLaybyDiscount_tax = colLaybyInfo.Item("discount_tax")
                        decLaybyDiscount_inc = decLaybyDiscount_nett + decLaybyDiscount_tax

                        mTxtSaleJobNo.Text = "Layby #" & CStr(intLabyId) & "; Total Value:" & FormatCurrency(decAmt, 2)
                        DoEvents()
                        mDgvSaleItems.Rows.Clear()
                        '= intRowx = 0

                        '- Add a Grid row for each Item in the layby..
                        For Each colItem As Collection In colLaybyInfo.Item("items")
                            intStock_id = colItem.Item("stock_id")
                            sBarcode = colItem.Item("stock_barcode")
                            sSerialNo = colItem.Item("serialNumber")
                            sDescription = colItem.Item("description")
                            decQty = colItem.Item("quantity")
                            '- add to grid=
                            intRowx = mbAddItemToSalesGrid(sBarcode, intStock_id, sSerialNo, sDescription, decQty)
                            If Not (intRowx >= 0) Then
                                MsgBox("Failed to add the item '" & sDescription & " to Grid.. ", MsgBoxStyle.Exclamation)
                            End If '--add item.
                        Next colItem
                        mbIsDeliveringLayby = True
                        '-3519.0414- SHOW discount if any..
                        mTxtSaleDiscount.Text = FormatNumber(decLaybyDiscount_inc, 2)
                        If decLaybyDiscount_inc > 0 Then
                            mDecDiscount = decLaybyDiscount_inc  '=CDec(sData)
                            '- FOR SHOW- split tax from discount-
                            Call mbShowTaxSplit()
                            Call mbUpdateSaleTotal()
                        End If
                        '-- emulate Continue button..-
                        '-- SALE is already set up as DEFAULT-
                        mPanelOptTranType.Enabled = False
                        mLabSaleHelp.Text = "Layby set up- Scan Extra Sales items if needed.."
                        mPanelSaleLineEntry.Enabled = True
                        mTxtSaleItemBarcode.Select()
                        '- Done-
                    Else  '-cancelled-
                        '-- if cancelled just press on to select tran.
                        '=3519.0330-
                        If (intQuoteCount > 0) Then
                            mLabImportQuote.Enabled = True
                        End If
                        mPanelOptTranType.Enabled = True  '== can change opt..
                        '== mBtnSaleContinue.Enabled = True
                        mLabSaleHelp.Text = "Select Sale or Refund, and press 'Continue'."
                        '= mBtnSaleContinue.Select()
                        Call btnSaleContinue_Click()

                    End If  '-cancelled-
                Else  '- Don't deliver- just press on
                    '-temp- carry on..
                    '=3519.0330-
                    If (intQuoteCount > 0) Then
                        mLabImportQuote.Enabled = True
                    End If
                    mPanelOptTranType.Enabled = True  '== can change opt..
                    '= mBtnSaleContinue.Enabled = True
                    mLabSaleHelp.Text = "Select Sale or Refund, and press 'Continue'."
                    '=3519.0219= Call btnSaleContinue_Click()
                    mOptSaleSale.Select()
                End If '-yes-
                'ElseIf (intQuoteCount > 0) Then
                '    mBtnImportQuote.Enabled = True
            Else  '- no Job, No laybys- CHECK quotes.
                '=3519.0330-
                If (intQuoteCount > 0) Then
                    mLabImportQuote.Enabled = True
                End If
                '=3301.427=
                '==mLabSaleHelp.Text = "Scan or Enter barcodes for sales items."
                mPanelOptTranType.Enabled = True  '== can change opt..
                mLabSaleHelp.Text = "Press Continue to go on.."
                '= mBtnSaleContinue.Enabled = True
                mLabSaleHelp.Text = "Select Sale or Refund, and press ENTER.." '= 'Continue'."

                '=3411.0311= mBtnSaleContinue.Select()
                mOptSaleSale.Select()
            End If
        End If  '-have job/layby's-

        '=4201.0708=
        mDgvSalePaymentDetails.CurrentCell = mDgvSalePaymentDetails.Rows(0).Cells(1)  '-1st amount.

        '= End With '-sale-
    End Function  '--SetupSaleCustomer--
    '= = = = = = =
    '-===FF->

    '- P u b l i c  Methods--
    '- P u b l i c  Methods--
    '- P u b l i c  Methods--

    Public Sub New(ByRef frmSale As UserControl, _
                   ByVal tooltip1 As ToolTip, _
                     ByVal sServer As String, _
                      ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                         ByVal sVersionPOS As String, _
                             ByRef imageUserLogo As Image, _
                             ByVal SettingsPath As String, _
                              ByVal intStaff_id As Integer, _
                               ByVal sStaffName As String)

        Dim controls1() As Control
        '==Dim col1, colRecords, colRow As Collection
        Dim colPaymentTypes, col1 As Collection
        '= Dim sErrorMsg As String
        Dim s1, s2, s3, sSql, sUpdates As String
        Dim rx, L1 As Integer
        Dim row1 As DataGridViewRow
        '= Dim colTable, colFields, colFieldx As Collection

        '==Try
        '-- save ref to Main Form..-
        mFrmSale = frmSale
        mToolTip1 = tooltip1
        '--save -
        msServer = sServer
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo

        '=3501.0809= msVersionPOS = sVersionPOS
        msDLLVersion = sVersionPOS
        msSettingsPath = SettingsPath  '-4201.0708-

        mImageUserLogo = imageUserLogo
        '= mSdSystemInfo = sdSystemInfo
        '== msColourPrtName = sColourPrtName
        '== msReceiptPrtName = sReceiptPrtName
        '== msLabelPrtName = sLabelPrtName

        mIntMainStaff_id = intStaff_id
        msMainStaffName = sStaffName
        msCurrentUserName = gsGetCurrentUser()

        '- Find/save refs to host controls..--
        '- save refs to host controls..--
        Try  '--finding controls-
            '-mTabControlMAIN- 3401.319=  MAY NOT EXIST-
            'controls1 = mFrmSale.Controls.Find("TabControlMain", True)  '--searches all children..
            'If (controls1.Length > 0) Then mTabControlMain = controls1(0)

            ''-mTabControlPOS- 3401.319=
            'controls1 = mFrmSale.Controls.Find("TabControlPOS", True)  '--searches all children..
            'If (controls1.Length > 0) Then mTabControlPOS = controls1(0)

            '-mPanelSaleInvoicesList=
            controls1 = mFrmSale.Controls.Find("panelSaleInvoiceList", True)  '--searches all children..
            mPanelSaleInvoiceList = controls1(0)
            '-mOptSaleInvoicesList-
            'controls1 = mFrmSale.Controls.Find("optSaleInvoiceList", True)  '--searches all children..
            'mOptSaleInvoiceList = controls1(0)
            'controls1 = mFrmSale.Controls.Find("optSaleQuotesList", True)  '--searches all children..
            'mOptSaleQuotesList = controls1(0)
            '= controls1 = mFrmSale.Controls.Find("dgvSaleInvoices", True)
            '= mDgvSaleInvoices = controls1(0)

            '==3107.922= Now is cmd Button to show invoices..
            '= 3401.322= Also for Quotes-
            controls1 = mFrmSale.Controls.Find("btnSaleSelectInvoice", True)
            If (controls1.Length > 0) Then mBtnSaleSelectInvoice = controls1(0)
            controls1 = mFrmSale.Controls.Find("btnSaleSelectQuote", True)
            If (controls1.Length > 0) Then mBtnSaleSelectQuote = controls1(0)

            '- save refs to host controls..--
            controls1 = mFrmSale.Controls.Find("panelOptTranType", True)  '--searches all children..
            If (controls1.Length > 0) Then mPanelOptTranType = controls1(0)

            ' =DEFAULT= controls1 = mFrmSale.Controls.Find("labChooseTrans", True)  '--searches all children..
            '=DEFAULT= If (controls1.Length > 0) Then mLabChooseTrans = controls1(0)

            controls1 = mFrmSale.Controls.Find("labSaleTranType", True)  '--searches all children..
            If (controls1.Length > 0) Then mLabSaleTranType = controls1(0)

            ''='=3301.427= Restore- but STILL the DEFAULT= 
            '=3411.0311= RESTORE Radio Buttons.
            controls1 = mFrmSale.Controls.Find("optSaleSale", True)  '--searches all children..
            '=DEFAULT= 
            mOptSaleSale = controls1(0)

            controls1 = mFrmSale.Controls.Find("optSaleRefund", True)  '--searches all children..
            mOptSaleRefund = controls1(0)
            controls1 = mFrmSale.Controls.Find("optSaleQuote", True)  '--searches all children..
            mOptSaleQuote = controls1(0)
            controls1 = mFrmSale.Controls.Find("optSaleLayby", True)  '--searches all children..
            mOptSaleLayby = controls1(0)

            '=3519.0330--
            '= Private mBtnImportQuote As Button
            controls1 = mFrmSale.Controls.Find("labImportQuote", True)
            If (controls1.Length > 0) Then mLabImportQuote = controls1(0)

            '-gone- we use SALE-
            '= controls1 = mFrmSale.Controls.Find("optSaleCreditNote", True)  '--searches all children..
            '= mOptSaleCreditNote = controls1(0)

            '==3401.321= NOW is a COMBO==
            'controls1 = mFrmSale.Controls.Find("cboTransaction", True)  '--searches all children..
            'If (controls1.Length > 0) Then mCboTransaction = controls1(0)

            ''==mBtnSaleContinue--3301.528-
            'controls1 = mFrmSale.Controls.Find("btnSaleContinue", True)  '--searches all children..
            'If (controls1.Length > 0) Then mBtnSaleContinue = controls1(0)

            controls1 = mFrmSale.Controls.Find("panelSaleHdr", True)  '--searches all children..
            If (controls1.Length > 0) Then mPanelSaleHdr = controls1(0)

            controls1 = mFrmSale.Controls.Find("labSaleJobDelivery", True)  '--searches all children..
            mLabSaleJobDelivery = controls1(0)

            controls1 = mFrmSale.Controls.Find("txtSaleJobNo", True)  '--searches all children..
            mTxtSaleJobNo = controls1(0)

            controls1 = mFrmSale.Controls.Find("labSaleCust", True)  '--searches all children..
            mLabSaleCust = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleCustBarcode", True)  '--searches all children..
            mTxtSaleCustBarcode = controls1(0)

            controls1 = mFrmSale.Controls.Find("txtSaleCustName", True)
            mTxtSaleCustName = controls1(0)

            '=4221.0207-
            '= Private mLabSaleCustTags As Label
            controls1 = mFrmSale.Controls.Find("labSaleCustTags", True)
            mLabSaleCustTags = controls1(0)

            '==3303.0117=
            controls1 = mFrmSale.Controls.Find("listViewSaleAvailCredit", True)
            mListViewSaleAvailCredit = controls1(0)
            '=3403.1014-  save orig, ht..
            mIntListViewSaleAvailCredit_height = mListViewSaleAvailCredit.Height

            '=GONE- 3401.321=  controls1 = mFrmSale.Controls.Find("txtSalePricingGrade", True)
            '= mTxtSalePricingGrade = controls1(0)

            controls1 = mFrmSale.Controls.Find("picSaleItem", True)
            mPicSaleItem = controls1(0)

            controls1 = mFrmSale.Controls.Find("dgvSaleItems", True)
            mDgvSaleItems = controls1(0)
            '== mDgvSaleItems.Columns("DeleteRow").ToolTipText = "Delete Row"

            controls1 = mFrmSale.Controls.Find("panelSaleFooter", True)  '--searches all children..
            If (controls1.Length > 0) Then mPanelSaleFooter = controls1(0)
            controls1 = mFrmSale.Controls.Find("panelSaleTotals", True)  '--searches all children..
            If (controls1.Length > 0) Then mPanelSaleTotals = controls1(0)

            'controls1 = mFrmSale.Controls.Find("txtSaleDelivery", True)
            'mTxtSaleDelivery = controls1(0)
            'controls1 = mFrmSale.Controls.Find("txtSaleComments", True)
            'mTxtSaleComments = controls1(0)
            '=3403.730= -mBtnSaleComments-
            controls1 = mFrmSale.Controls.Find("btnSaleComments", True)  '--searches all children..
            If (controls1.Length > 0) Then mBtnSaleComments = controls1(0)

            controls1 = mFrmSale.Controls.Find("panelPayment", True)  '--searches all children..
            If (controls1.Length > 0) Then mPanelPayment = controls1(0)
            '==   Updated.- 3519.0317..
            '==    -- MAJOR-  Add TextBox to Payments panel- 
            '==            for User to decide on Amount of CreditNote to withdraw to pay for Sale.
            '= Private mTxtCreditNoteWdl As TextBox
            '= Private mGrpBoxSalePayments As GroupBox
            controls1 = mFrmSale.Controls.Find("grpBoxSalePayments", True)  '--searches all children..
            If (controls1.Length > 0) Then mGrpBoxSalePayments = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtCreditNoteWdl", True)  '--searches all children..
            If (controls1.Length > 0) Then mTxtCreditNoteWdl = controls1(0)
            '----

            controls1 = mFrmSale.Controls.Find("labSalePayments", True)
            If (controls1.Length > 0) Then mLabSalePayments = controls1(0)
            controls1 = mFrmSale.Controls.Find("dgvSalePaymentDetails", True)
            mDgvSalePaymentDetails = controls1(0)

            controls1 = mFrmSale.Controls.Find("txtSalePaymentBalance", True)
            mTxtSalePaymentBalance = controls1(0)
            controls1 = mFrmSale.Controls.Find("labSaleCrBal", True)
            mLabSaleCrBal = controls1(0)

            controls1 = mFrmSale.Controls.Find("labSaleChargeBalance", True)
            mLabSaleChargeBalance = controls1(0)

            controls1 = mFrmSale.Controls.Find("labSaleChange", True)
            mLabSaleChange = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleChange", True)
            mTxtSaleChange = controls1(0)

            '--  Totals--
            controls1 = mFrmSale.Controls.Find("txtSaleTotalTax", True)
            mTxtSaleTotalTax = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleSubTotal", True)
            mTxtSaleSubTotal = controls1(0)
        Catch ex As Exception
            MsgBox("Runtime Error: clsPOS31Main (New) FINDING CONTROLS-1. " & vbCrLf & _
                        ex.Message, MsgBoxStyle.Information)
            '= Exit Sub
        End Try '--finding controls-

        Try
            controls1 = mFrmSale.Controls.Find("btnDiscountPC", True)
            mBtnDiscountPC = controls1(0)
            '- cboSaleDiscountPercent
        Catch ex As Exception
            MsgBox("Runtime Error: clsPOS31Main (New) FINDING CONTROLS-2. " & vbCrLf & _
                        ex.Message, MsgBoxStyle.Information)
            '= Exit Sub
        End Try '--finding controls2-

        Try
            controls1 = mFrmSale.Controls.Find("cboSaleDiscountPercent", True)
            mCboSaleDiscountPercent = controls1(0)

            controls1 = mFrmSale.Controls.Find("labSaleDiscount", True)
            mLabSaleDiscount = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleDiscount", True)
            mTxtSaleDiscount = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleDiscountAnalysis", True)
            mTxtSaleDiscountAnalysis = controls1(0)

            'controls1 = mFrmSale.Controls.Find("labSaleCashout", True)
            'mLabSaleCashout = controls1(0)
            'controls1 = mFrmSale.Controls.Find("txtSaleCashout", True)
            'mTxtSaleCashout = controls1(0)

            controls1 = mFrmSale.Controls.Find("txtSaleSubTotal2", True)
            mTxtSaleSubTotal2 = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleNettTax", True)
            mTxtSaleNettTax = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleRounding", True)
            mTxtSaleRounding = controls1(0)

            controls1 = mFrmSale.Controls.Find("labSaleInvTotal", True)
            mLabSaleInvTotal = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleTotal", True)
            mTxtSaleTotal = controls1(0)

            controls1 = mFrmSale.Controls.Find("labSaleHelp", True)
            mLabSaleHelp = controls1(0)
            controls1 = mFrmSale.Controls.Find("labSaleHelp2", True)
            mLabSaleHelp2 = controls1(0)

            '--mBtnCancelSale- (x2)-
            controls1 = mFrmSale.Controls.Find("btnCancelSale", True)
            mBtnCancelSale = controls1(0)
            controls1 = mFrmSale.Controls.Find("btnCancelSale2", True)
            mBtnCancelSale2 = controls1(0)

            controls1 = mFrmSale.Controls.Find("btnCommitSale", True)
            mBtnCommitSale = controls1(0)

            '- mLabSaleDLLversion-
            '= controls1 = mFrmSale.Controls.Find("labSaleDllversion", True)
            '= mLabSaleDLLversion = controls1(0)

        Catch ex As Exception
            MsgBox("Runtime Error: clsPOS34Main (New) FINDING CONTROLS-3. " & vbCrLf & _
                        ex.Message, MsgBoxStyle.Information)
            '= Exit Sub
        End Try '--finding controls-

        '=3311.516= textBox controls for Sale Item Line Entry..
        '=Private mPanelSaleLineEntry As Panel
        '=Private mLabSaleItemNo As Label
        '=Private mTxtSaleItemBarcode As TextBox
        '=Private mTxtSaleItemSerialNo As TextBox
        '=Private mTxtSaleItemCategory As TextBox
        '=Private mTxtSaleItemDescription As TextBox
        '=Private mTxtSaleItemSellPrice As TextBox
        '=Private mTxtSaleItemQty As TextBox
        '=Private mTxtSaleItemExtension As TextBox
        '=Private mBtnSaleLineOk As Button
        Try
            '=txtSaleItemBarcode
            controls1 = mFrmSale.Controls.Find("panelSaleLineEntry", True)  '--searches all children..
            If (controls1.Length > 0) Then mPanelSaleLineEntry = controls1(0)
            '= controls1 = mFrmSale.Controls.Find("labSaleItemNo", True)  '--searches all children..
            '= If (controls1.Length > 0) Then mLabSaleItemNo = controls1(0)

            controls1 = mFrmSale.Controls.Find("txtSaleItemBarcode", True)  '--searches all children..
            If (controls1.Length > 0) Then mTxtSaleItemBarcode = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleItemSerialNo", True)  '--searches all children..
            If (controls1.Length > 0) Then mTxtSaleItemSerialNo = controls1(0)

            '= controls1 = mFrmSale.Controls.Find("txtSaleItemCategory", True)  '--searches all children..
            '= If (controls1.Length > 0) Then mTxtSaleItemCategory = controls1(0)

            controls1 = mFrmSale.Controls.Find("txtSaleItemDescription", True)  '--searches all children..
            If (controls1.Length > 0) Then mTxtSaleItemDescription = controls1(0)

            controls1 = mFrmSale.Controls.Find("txtSaleItemSellPrice", True)  '--searches all children..
            If (controls1.Length > 0) Then mTxtSaleItemSellPrice = controls1(0)
            controls1 = mFrmSale.Controls.Find("txtSaleItemQty", True)  '--searches all children..
            If (controls1.Length > 0) Then mTxtSaleItemQty = controls1(0)

            controls1 = mFrmSale.Controls.Find("txtSaleItemExtension", True)  '--searches all children..
            If (controls1.Length > 0) Then mTxtSaleItemExtension = controls1(0)

            controls1 = mFrmSale.Controls.Find("btnSaleLineOk", True)  '--searches all children..
            If (controls1.Length > 0) Then mBtnSaleLineOk = controls1(0)

            '=3403.104= 
            controls1 = mFrmSale.Controls.Find("chkOnAccount", True)  '--searches all children..
            If (controls1.Length > 0) Then mChkOnAccount = controls1(0)
            '=3519.117= 
            controls1 = mFrmSale.Controls.Find("chkOnAccount2", True)  '--searches all children..
            If (controls1.Length > 0) Then mChkOnAccount2 = controls1(0)

            '= Private mGrpBoxRefundType As GroupBox
            '= Private mOptRefundCash As RadioButton
            '= Private mOptRefundCredit As RadioButton
            '=3301.525=
            controls1 = mFrmSale.Controls.Find("grpBoxRefundType", True)  '--searches all children..
            If (controls1.Length > 0) Then mGrpBoxRefundType = controls1(0)
            controls1 = mFrmSale.Controls.Find("optRefundCash", True)  '--searches all children..
            If (controls1.Length > 0) Then mOptRefundCash = controls1(0)
            controls1 = mFrmSale.Controls.Find("optRefundCredit", True)  '--searches all children..
            If (controls1.Length > 0) Then mOptRefundCredit = controls1(0)
            '=3401.327 Add  mOptRefundEftPosDr As RadioButton
            controls1 = mFrmSale.Controls.Find("optRefundEftPosDr", True)  '--searches all children..
            If (controls1.Length > 0) Then mOptRefundEftPosDr = controls1(0)
            controls1 = mFrmSale.Controls.Find("optRefundEftPosCr", True)  '--searches all children..
            If (controls1.Length > 0) Then mOptRefundEftPosCr = controls1(0)


            '==  Target is new Build 4251..
            '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
            controls1 = mFrmSale.Controls.Find("optRefundOther", True)  '--searches all children..
            If (controls1.Length > 0) Then mOptRefundOther = controls1(0)
            controls1 = mFrmSale.Controls.Find("cboRefundOtherDetails", True)  '--searches all children..
            If (controls1.Length > 0) Then mCboRefundOtherDetails = controls1(0)

            '== END  Target is new Build 4251..


            '-- 
        Catch ex As Exception
            MsgBox("Runtime Error: clsPOS34Main (New) FINDING Sale textbox CONTROLS-4. " & vbCrLf & _
             ex.Message, MsgBoxStyle.Information)
        End Try

        '== Till-id-  17-Jan-2107=
        Try
            controls1 = mFrmSale.Controls.Find("labSaleTillId", True)
            If (controls1.Length > 0) Then
                mLabSaleTillId = controls1(0)
                mLabSaleTillId.Text = "-Till- ?"
                '= mLabSaleTillId.Text = "- Till-" & gsGetCurrentCashDrawer() & " -"
            End If
        Catch ex As Exception
            MsgBox("Runtime Error: clsPOS34Main (Sub New) FINDING Sale TILL-ID CONTROL-4. " & vbCrLf & _
             ex.Message, MsgBoxStyle.Information)
        End Try  '--till id-

        '= 3401.0309--
        '-- CONTROLs for HOLDING/RESTORING transactions..
        'Private mPanelSalesCurrentHdr As Panel
        'Private mLabSaleTranActive As Label
        ''--
        'Private mPanelSaleTranHeld1 As Panel
        'Private mBtnSaleRestore1 As Button
        'Private mLabHeld1Info As Label
        ''--
        'Private mPanelSaleTranHeld2 As Panel
        'Private mBtnSaleRestore2 As Button
        'Private mLabHeld12nfo As Label
        ''-
        'Private mTxtSaleStaffBarcode As TextBox
        'Private mLabSaleStaffName As Label
        'Private mBtnSaleHold As Button
        Try
            '= controls1 = mFrmSale.Controls.Find("btnSaleHold", True)  '--searches all children..
            '= If (controls1.Length > 0) Then mBtnSaleHold = controls1(0)

            ''--
            controls1 = mFrmSale.Controls.Find("txtSaleStaffBarcode", True)  '--searches all children..
            If (controls1.Length > 0) Then mTxtSaleStaffBarcode = controls1(0)
            controls1 = mFrmSale.Controls.Find("labSaleStaffName", True)  '--searches all children..
            If (controls1.Length > 0) Then mLabSaleStaffName = controls1(0)
            controls1 = mFrmSale.Controls.Find("btnSaleItemLineClear", True)  '--searches all children..
            If (controls1.Length > 0) Then mBtnSaleItemLineClear = controls1(0)
            '=3403.516= labSaleAccountInfo--
            controls1 = mFrmSale.Controls.Find("labSaleAccountSalesInfo", True)  '--searches all children..
            If (controls1.Length > 0) Then mLabSaleAccountSalesInfo = controls1(0)

        Catch ex As Exception
            MsgBox("Runtime Error: clsPOS31Main (Sub New) FINDING Sale HOLD/RESTORE CONTROLS. " & vbCrLf & _
                                                                         ex.Message, MsgBoxStyle.Information)
        End Try
        '-- END of CONTROLs for HOLDING transactions..
        '-
        '--init them-
        'mLabSaleTranActive.Text = "No Transaction Active."
        'mBtnSaleRestore1.Enabled = False
        'mBtnSaleRestore2.Enabled = False
        'mLabHeld1Info.Text = ""
        'mLabHeld2Info.Text = ""
        mTxtSaleStaffBarcode.Text = ""
        mLabSaleStaffName.Text = ""

        '-- show version on main form.-
        'msDLLVersion = "JobMatixPOS DLL ver: " & msGetDllversion()
        'mLabSaleDLLversion.Text = msDLLVersion

        '- save globals-
        Call gbSaveDllVersion(msDLLVersion)
        Call gbSaveUserLogo(mImageUserLogo)

        '-- initialise..--
        '= msComputerName = My.Computer.Name

        '-3401.414=
        '--  Get actual machine running this app process. (NOT the remote client).
        msMachineName = My.Computer.Name  '- for actual machine running this app process. (NOT the remote client).
        '--  get thin client if any=
        ' get the workstation name...
        mbIsThinClient = False
        msComputerName = Environment.GetEnvironmentVariable("clientname")
        ' if not a thin client, previous step returns nothing,
        ' this will get the name of a fat client...
        If (msComputerName IsNot Nothing) AndAlso (msComputerName <> "") Then
            mbIsThinClient = True
        Else '-no "client"  is Fat..
            '= machinename = Environment.GetEnvironmentVariable("computername")
            msComputerName = My.Computer.Name
        End If


        '--  staff--
        mColPrefsStaff = New Collection
        mColPrefsStaff.Add("docket_name")
        mColPrefsStaff.Add("lastname")
        mColPrefsStaff.Add("firstname")
        mColPrefsStaff.Add("barcode")
        mColPrefsStaff.Add("staff_id")
        mColPrefsStaff.Add("position")
        mColPrefsStaff.Add("isAdministrator")
        mColPrefsStaff.Add("inactive")
        mColPrefsStaff.Add("dateOfBirth")
        mColPrefsStaff.Add("address")
        mColPrefsStaff.Add("suburb")
        mColPrefsStaff.Add("state")
        mColPrefsStaff.Add("postcode")
        mColPrefsStaff.Add("homePhone")
        mColPrefsStaff.Add("mobile")
        mColPrefsStaff.Add("emailAddress")
        mColPrefsStaff.Add("staffPicture")

        '-- Customer --
        mColPrefsCustomer = New Collection
        mColPrefsCustomer.Add("lastname")
        mColPrefsCustomer.Add("firstname")
        mColPrefsCustomer.Add("companyName")
        mColPrefsCustomer.Add("barcode")
        mColPrefsCustomer.Add("isAccountCust")
        mColPrefsCustomer.Add("phone")
        mColPrefsCustomer.Add("mobile")
        mColPrefsCustomer.Add("customer_id")
        mColPrefsCustomer.Add("creditLimit")
        mColPrefsCustomer.Add("pricingGrade")
        mColPrefsCustomer.Add("inactive")
        mColPrefsCustomer.Add("address")
        '==mColPrefsCustomer.Add("addr2")
        '=mColPrefsCustomer.Add("addr3")
        mColPrefsCustomer.Add("suburb")
        mColPrefsCustomer.Add("email")
        '= mColPrefsCustomer.Add("date_modified")
        '= mColPrefsCustomer.Add("comments")

        '-- Supplier --
        mColPrefsSupplier = New Collection
        mColPrefsSupplier.Add("supplierName")
        mColPrefsSupplier.Add("contactName")
        mColPrefsSupplier.Add("barcode")
        mColPrefsSupplier.Add("contactPosition")
        mColPrefsSupplier.Add("supplier_id")
        mColPrefsSupplier.Add("address")
        '= mColPrefsSupplier.Add("main_addr2")
        '== mColPrefsSupplier.Add("main_addr3")
        mColPrefsSupplier.Add("suburb")
        mColPrefsSupplier.Add("state")
        mColPrefsSupplier.Add("postcode")
        mColPrefsSupplier.Add("country")
        mColPrefsSupplier.Add("phone")
        mColPrefsSupplier.Add("fax")
        mColPrefsSupplier.Add("emailAddress")
        mColPrefsSupplier.Add("webSiteURL")
        '== mColPrefsSupplier.Add("grade")
        mColPrefsSupplier.Add("inactive")
        mColPrefsSupplier.Add("freight_free")
        '==mColPrefsSupplier.Add("reject_backorders")
        mColPrefsSupplier.Add("deliveryDays")
        mColPrefsSupplier.Add("abn")
        mColPrefsSupplier.Add("comments")
        mColPrefsSupplier.Add("date_modified")

        '--  stock--
        mColPrefsStock = New Collection
        mColPrefsStock.Add("description")
        mColPrefsStock.Add("barcode")
        mColPrefsStock.Add("brandName")
        mColPrefsStock.Add("cat1")   '--fkey-
        mColPrefsStock.Add("cat2")   '-fkey-
        '= mColPrefsStock.Add("productPicture")
        mColPrefsStock.Add("stock_id")
        '=3301.606= mColPrefsStock.Add("isServiceItem")
        mColPrefsStock.Add("isNonStockItem")
        mColPrefsStock.Add("track_serial")
        mColPrefsStock.Add("inactive")
        mColPrefsStock.Add("supplier_id")
        mColPrefsStock.Add("costExTax")
        mColPrefsStock.Add("goods_TaxCode")
        mColPrefsStock.Add("sellExTax")
        mColPrefsStock.Add("sales_TaxCode")
        mColPrefsStock.Add("qtyInStock")
        '== mColPrefsStock.Add("qtyOnLayby")
        mColPrefsStock.Add("allow_renaming")
        mColPrefsStock.Add("comments")

        '--  Categories--
        mColPrefsCategory1 = New Collection
        mColPrefsCategory1.Add("cat1_key")  '--PKEY--
        mColPrefsCategory1.Add("description")
        mColPrefsCategory1.Add("date_created")
        '- - - -- -
        mColPrefsCategory2 = New Collection
        mColPrefsCategory2.Add("cat2_key")  '--PKEY--
        mColPrefsCategory2.Add("description")
        mColPrefsCategory2.Add("date_created")
        '- - - -- - - - - - 

        mColPrefsBrands = New Collection
        mColPrefsBrands.Add("Brand_id")  '--PKEY--
        mColPrefsBrands.Add("BrandName")
        mColPrefsBrands.Add("date_created")



        '-  load discount percentages..
        mCboSaleDiscountPercent.Items.Clear()
        mCboSaleDiscountPercent.Items.Add("0")
        mCboSaleDiscountPercent.Items.Add("2.5")
        mCboSaleDiscountPercent.Items.Add("5.0")
        mCboSaleDiscountPercent.Items.Add("7.5")
        mCboSaleDiscountPercent.Items.Add("10.0")
        mCboSaleDiscountPercent.Items.Add("12.5")
        mCboSaleDiscountPercent.Items.Add("15.0")
        mCboSaleDiscountPercent.Items.Add("17.5")
        mCboSaleDiscountPercent.Items.Add("20.0")
        mCboSaleDiscountPercent.Items.Add("25.0")
        mCboSaleDiscountPercent.Items.Add("30.0")
        mCboSaleDiscountPercent.Items.Add("35.0")
        mCboSaleDiscountPercent.Items.Add("40.0")
        mCboSaleDiscountPercent.Items.Add("50.0")
        mCboSaleDiscountPercent.Items.Add("60.0")
        mCboSaleDiscountPercent.Items.Add("80.0")
        mCboSaleDiscountPercent.Items.Add("90.0")
        mCboSaleDiscountPercent.Items.Add("100.0")

        mCboSaleDiscountPercent.SelectedIndex = 0    '--default no discount.-

        '-test-
        '= MsgBox("Discount combo loaded..", MsgBoxStyle.Information)

        '==   27Feb2017= SALE Screen. in clsPOS31Main- set dgvSaleItems.StandardTab -> TRUE !!! 
        mDgvSaleItems.StandardTab = True
        '==   27Feb2017= SALE Screen. in clsPOS31Main- set mDgvSalePaymentDetails.StandardTab -> TRUE !!! 
        mDgvSalePaymentDetails.StandardTab = True

        '=3311.516= CLEAR all  textBox controls for Sale Item Line Entry..
        mPanelSaleLineEntry.Enabled = False
        mBtnCommitSale.Enabled = False

        '= mLabSaleAvailCredit.Text = ""
        mListViewSaleAvailCredit.Items.Clear()

        Call mbClearEditLine()
        mTxtSaleItemBarcode.Text = ""
        '=3401.317= mLabSalePayments.Text = "- Payments Received -"

        '- load payment types..-
        mDgvSalePaymentDetails.Rows.Clear()
        '= 3401.322=
        '=   mLabSaleChargeBalance.Top = mGrpBoxRefundType.Top
        '=   mLabSaleChargeBalance.Height = 33
        mLabSaleChargeBalance.Text = ""  '= "Balance will be charged to Customer's Account."
        '=  mLabSaleChargeBalance.Visible = False
        mGrpBoxRefundType.Visible = False

        '=--3101.1206=
        '==
        '== NEW revision-
        '==    -- 4201.0708.  Started 05-July-2019-
        '==       -- Add file->Local Preference for LOCAL Re-ordering of payment Details.
        '==       -- Payments and Sales-  Use clsPaymentTypes to get PaymentDetails for Grid. .
        Dim clsPayTypes1 As clsPaymentTypes
        clsPayTypes1 = New clsPaymentTypes(msSettingsPath)
        '-test-
        '= MsgBox("Settings path: " & vbCrLf & msSettingsPath, MsgBoxStyle.Information)
        colPaymentTypes = clsPayTypes1.getColPaymentTypes()
        '- end of new bit..

        '= colPaymentTypes = gColPaymentTypes()  '--3101.1206= Get collection of types.
        rx = 0
        For Each col1 In colPaymentTypes
            row1 = New DataGridViewRow
            mDgvSalePaymentDetails.Rows.Add(row1)
            With mDgvSalePaymentDetails.Rows(rx)
                .Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value = col1("description")
                .Cells(k_PAYGRIDCOL_AMOUNT).Value = "0.00"
                .Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value = col1("key")
            End With
            rx += 1
        Next col1
        DoEvents()


        '==  Target is new Build 4251..
        '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
        '-- Load Refund Details Combo..
        mCboRefundOtherDetails.Items.Clear()
        For Each col1 In colPaymentTypes
            s1 = LCase(col1("key"))
            If (s1 <> "cash") And (s1 <> "eftpos_dr") And (s1 <> "eftpos_cr") Then
                '-- ok.. is an "other"..
                mCboRefundOtherDetails.Items.Add(col1("key"))
            End If
        Next col1
        mCboRefundOtherDetails.SelectedIndex = -1  '-nothing selected.
        '== END  Target is new Build 4251.. 



        '== mOptSaleSale.Text = "Sales"
        '== mOptSaleRefund.Text = "Refunds"
        '== mOptSaleQuote.Text = "Quotes"
        '=3403.430= Init Trans. combo-

        'mCboTransaction.Items.Clear()
        'mCboTransaction.Items.Add("Sale")
        'mCboTransaction.Items.Add("Refund")
        'mCboTransaction.Items.Add("New Layby")
        'mCboTransaction.Items.Add("Quote")
        'mCboTransaction.SelectedIndex = -1

        mPanelSaleInvoiceList.Enabled = False
        '== mDgvSaleInvoices.Enabled = False
        mLabSaleTranType.Enabled = False

        mDgvSalePaymentDetails.Enabled = False
        '=3519.0317=
        mGrpBoxSalePayments.Enabled = False

        mPanelSaleFooter.Enabled = False
        mPanelPayment.Enabled = False

        '== 3301.525.. Refund can choose cash/Credit.
        mOptRefundCredit.Checked = True
        mGrpBoxRefundType.Text = "Refund Type:"
        mGrpBoxRefundType.Enabled = False
        '== mGrpBoxRefundType.Visible = False

        '-- Customer Choice now comes first..
        mPanelOptTranType.Enabled = False  '= True
        '= mBtnSaleContinue.Enabled = False
        '== mLabChooseTrans.Visible = False  '= True
        mLabSaleHelp2.Text = ""
        mLabSaleHelp.Text = "Enter Staff ID, then Customer No. (F2 to lookup..)"

        Call mbClearInvoice()
        mDgvSaleItems.Enabled = False

        '-- get system Info table data.-
        '=3301.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        Call mbRefreshSystemInfo()

        '== mTxtSaleCustBarcode.Select() '-focus--

        '=3301.607= Check if we can save to email queue (docArchive table).
        mbAllowEmailInvoices = False
        If mSysInfo1.contains("POS_ALLOW_EMAIL_INVOICES") Then
            If (UCase(mSysInfo1.item("POS_ALLOW_EMAIL_INVOICES")) = "Y") Then
                mbAllowEmailInvoices = True    '= Yes, do emailing.-
            End If
        End If

        '= = = = = = = = = = = = = = = = = = = = = =

        ''==
        ''==     v3.3.3301.1112..  12-Nov-2016= ===
        ''==       >> Add Licence Checking (cloning POS licence off Jobmatix)..-



        ''--   L I C E N C E  C H E C K ----
        ''--   L I C E N C E  C H E C K ----
        ''--   L I C E N C E  C H E C K ----

        ''== end of licence stuff..
        ''= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

        '        Customer Barcode-   
        '--  F2 to Search.
        '--  F3 Same Customer.
        '-- SET tooltip-
        mToolTip1.SetToolTip(mTxtSaleCustBarcode, "Customer Barcode-" & vbCrLf & _
                                                    "--  F2 to Search." & vbCrLf & _
                                                    "--  F3 Same Customer." & vbCrLf & _
                                                    "--  F5 Add New Customer.")
        sSql = ""

        '-=3103.119== 
        '-  Add Grid Context menu to Sale (Main) form-
        '--  For Deleting Grid Rows.
        mLabSaleJobDelivery.Enabled = False
        mTxtSaleJobNo.ReadOnly = True

        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else  '--see below-
            'For Each sName As String In colPrinters
            '    If (InStr(LCase(sName), "adobe pdf") > 0) Or _
            '             ((InStr(LCase(sName), "microsoft") > 0) And (InStr(LCase(sName), "pdf") > 0)) Then
            '        msPdfPrinterName = sName  '-save PDF printer name--
            '    End If
            'Next sName
        End If  '- no printers-

        '=3411.0110= (Microsoft will be preferred)..
        If Not gsGetPdfPrinterName(msPdfPrinterName) Then
            MsgBox("Error- Failed to look-up pdf printer..", MsgBoxStyle.Exclamation)
        End If  '-gety-

        If (msPdfPrinterName = "") Then
            MsgBox("Please Note: " & vbCrLf & "No PDF printer is installed on this system." & vbCrLf & _
                    "Invoices created here can not be stored for emailing)..", MsgBoxStyle.Information)
        End If

        '=NOT USED= mContextMenuDataGrid = New ContextMenu
        '=NOT USED= '-mnuDataGridViewRowDelete-
        '=NOT USED= mContextMenuDataGrid.MenuItems.Add(mnuDataGridViewRowSep1)
        '=NOT USED= mContextMenuDataGrid.MenuItems.Add(mnuDataGridViewRowDelete)
        '=NOT USED= mContextMenuDataGrid.MenuItems.Add(mnuDataGridViewRowSep2)    

        '=3403.516= labSaleAccountInfo--
        mLabSaleAccountSalesInfo.BackColor = Color.LightGoldenrodYellow
        mLabSaleAccountSalesInfo.Text = ""

        '=3301.1210= Must have a Till..
        '= Private msCurrentCashDrawer As String 

        '--  Look up SystemInfo Till Assignment for this computer. (msComputerName)
        '-- Sysinfo key is like "CashDrawer_[ Computer ]=X" ( where "computer"=ComputerName)..
        '--         AND Where "X"  can be ["A".."Z" ) ie Till assigned to this computer..

        '- SEE mClsJmxPOS31_Licence.StartupTillCheck --
        '-- called from parent Main Form.--

        'Dim sCurrentCashDrawer As String = ""
        'Dim sTillId As String

        'If Not gbGetCashDrawer(cnnSql, msComputerName, sTillId) Then
        '    mTxtSaleCustBarcode.Text = "NO TILL!"
        '    mTxtSaleCustBarcode.ReadOnly = True
        'Else  '--ok-
        '    sCurrentCashDrawer = sTillId
        '    mLabSaleTillId.Text = "- Till-" & gsGetCurrentCashDrawer() & " -"
        '    MsgBox("You are currently assigned to Till-" & gsGetCurrentCashDrawer(), MsgBoxStyle.Information)
        '    mTxtSaleCustBarcode.Text = ""
        'End If  '-get-

        'sMsg = "Please note: " & vbCrLf & _
        '        " Each POS Computer must have a current Cash Drawer (Till) Id assigned.." & vbCrLf & _
        '          "Current Till Assignments are: " & vbCrLf

        '==  And if labSaleTillId exists, then show Till Id..
        '- mLabSaleTillId-

        '=3403.1014=
        mChkOnAccount.Enabled = False
        mChkOnAccount2.Enabled = False
        mChkOnAccount.Checked = False
        mChkOnAccount2.Checked = False

        '=3519.0330--
        '= Private mBtnImportQuote As Button
        mLabImportQuote.Enabled = False

        '=4221.0207=
        mLabSaleCustTags.Text = ""

        Exit Sub
        '= Catch ex As Exception
        '= MsgBox("Runtime Error in clsPOS31Main (New). " & vbCrLf & ex.Message, MsgBoxStyle.Information)
        '= End Try
    End Sub  '--new --
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- P u b l i c M e t h o d s--
    '-- P u b l i c M e t h o d s--
    '-- P u b l i c M e t h o d s--

    '-- Multi-Sale-  Save/Restore Screen Controls Contents.
    '-- Multi-Sale-  Save/Restore Screen Controls Contents.
    '-- Multi-Sale-  Save/Restore Screen Controls Contents.

    '-- check if any sale current..
    Public Function HasCurrentSale() As Boolean
        If (msSaleStaffbarcode <> "") And (msSaleCustomerBarcode <> "") Then
            HasCurrentSale = True    '-has barcodes-
        Else  '- '-is empty/free--
            HasCurrentSale = False
        End If
    End Function '- HasCurrentSale'-
    '= = = = = = = = = = = = = = =

    '-- Open cash Drawer- (Current Till).

    Public Sub OpenCashDrawer()

        Dim strTillId = gsGetCurrentCashDrawer()
        Dim clsPrintDirect1 As clsPrintDirect
        '=Dim sTillPrinterNameInfoKey, sTillOpenCodeInfoKey As String
        Dim sPrinterName As String = ""
        Dim sEscapeCodes As String = ""

        Dim sTillPrinterNameinfoKey As String = "POS_TillOpenPrinterName_Till_" & strTillId
        Dim sTillOpenCodeinfoKey As String = "POS_TillOpenCode_Till_" & strTillId

        clsPrintDirect1 = New clsPrintDirect
        If (clsPrintDirect1 Is Nothing) Or (mSysInfo1 Is Nothing) Then
            MsgBox("Error- Class not initialised...", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '= OpenCashDrawer = False
        If (InStr("ABCDEFGH", UCase(strTillId)) <= 0) Then
            MsgBox("Error- " & strTillId & " is invalid Till Id..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '==  get Printer and ESCape codes for this Till..
        If mSysInfo1.exists(sTillPrinterNameInfoKey) AndAlso _
                    (Trim(mSysInfo1.item(sTillPrinterNameInfoKey)) <> "") Then
            sPrinterName = Trim(mSysInfo1.item(sTillPrinterNameinfoKey))
            If mSysInfo1.exists(sTillOpenCodeinfoKey) AndAlso _
                  (Trim(mSysInfo1.item(sTillOpenCodeinfoKey)) <> "") Then
                sEscapeCodes = Trim(mSysInfo1.item(sTillOpenCodeinfoKey))
            End If
        End If  '-exists-
        If (sPrinterName = "") Or (sEscapeCodes = "") Then
            MsgBox("No Cash-Drawer printer is set up for- " & strTillId, MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '-- ok.. send ESCape codes to printer to Open Drawer..
        '-- send routine has to decode into actual ESC stuff.
        If Not clsPrintDirect1.SendCashDrawerOpenCommand(sPrinterName, sEscapeCodes) Then
            MsgBox("Open drawer Failed ! ", MsgBoxStyle.Exclamation)
        End If

    End Sub  '-open Till-
    '= = = = = = = = = = = = =
    '-===FF->

    '-- clear current transaction instance and screen..

    Public Sub ClearCurrentTransaction()

        msSaleCustomerBarcode = ""
        msSaleStaffbarcode = ""

        '=3403.1014=
        mChkOnAccount.Checked = False
        mChkOnAccount2.Checked = False  '--keep in sync.

        Call mbClearEditLine()  '==3301.1112--
        Call mbClearInvoice()

        '-- And clear the screen..
        mTxtSaleStaffBarcode.Text = ""
        mLabSaleStaffName.Text = ""

        mTxtSaleStaffBarcode.Enabled = True

        mTxtSaleCustBarcode.Text = ""
        mTxtSaleCustName.Text = ""
        mLabSaleCustTags.Text = ""
        mToolTip1.SetToolTip(mLabSaleCustTags, "")

        '--clear credit stuff listView.
        For Each item1 As ListViewItem In mListViewSaleAvailCredit.Items
            item1.Text = ""
            If item1.SubItems IsNot Nothing Then
                item1.SubItems(1).Text = ""
            End If
        Next item1
        '==  3403.719- 19July2017- Clear barcode..
        mTxtSaleItemBarcode.Text = ""
        mTxtSaleStaffBarcode.Select()

    End Sub  '--clear-
    '= = = = = = = = = = = = 
    '-===FF->

    '- SAVE-
    '-- User pressed HOLD to put this Sale (Instance) aside..
    '--  NB Some textboxes are already copied into Private (static) Vars..
    '' Hold/Restore Gone.
    '-===FF->

      '-staffSignedOn-
    '-staffSignedOn-

    'Public Sub staffSignedOn(ByVal intStaff_id As Integer, _
    '                           ByVal sStaffName As String)
    '    mIntMainStaff_id = intStaff_id
    '    msMainStaffName = sStaffName
    'End Sub  '-staffSignedOn-
    '= = = =  = = = = = = = 

    '=3501.0725=  Called from Child sale Form to fetch sale staff details.

    Public Sub GetStaffSignOn(ByRef intStaff_id As Integer, _
                              ByRef sStaffBarcode As String, _
                               ByRef sStaffName As String)
        intStaff_id = mIntSaleStaff_id
        sStaffBarcode = msSaleStaffbarcode
        sStaffName = msSaleStaffName
    End Sub  '-staffSignedOn-
    '= = = =  = = = = = = = = = = = = = =

    '-staffTimeoutSuspended -

    Public Function staffTimeoutSuspended() As Boolean

        staffTimeoutSuspended = mbStaffTimeoutSuspended

    End Function  '-staffTimeoutSuspended -
    '= = = =  = = = = = = = 
    '-===FF->

    '--  Event processing support..-
    '--  Event processing support..-

    '=3411.0208=  Transaction Lookups..
    '=3411.0208=--ShowLastSale-

    Public Sub btnSaleShowLastSale_Click(eventSender As Object, ev As EventArgs) '= Handles btnShowLastSale.Click

        '==Call mClsSale1.btnShowLastSale_Click(eventSender, ev)
        Call mbShowLastSaleInvoice()

    End Sub  '-ShowLastSale-
    '= = = = = = = =  ==  = =

    '--  btnSaleSelectInvoice-

    Public Sub btnSaleSelectInvoice_Click(sender As Object, e As EventArgs)
        '== Handles btnSaleSelectInvoice.Click
        Call SaleSelectInvoiceOrQuote()  '-invoices=
    End Sub '-show invoices-
    '= = = = = = = = = = = == = = == =

    Public Sub btnSaleSelectQuote_Click(sender As Object, e As EventArgs)
        '== Handles btnSaleSelectQuote.Click
        SaleSelectInvoiceOrQuote(True)  '-quotes-
    End Sub  '= Quotes-
    '= = = = = = = == = = = =

    Public Sub btnSaleSelectLayby_Click(eventSender As Object, ev As EventArgs)
        '= Handles btnSaleSelectLayby.Click

        '= If mbIsInitialising Then Exit Sub
        '= Call mClsSale1.btnSaleSelectLayby_Click(eventSender, ev)

        SaleSelectInvoiceOrQuote(False, True)  '-labys-

    End Sub  '-layby..
    '= = = = = = = = = = = == =  = =
    '-===FF->

    '--  S a l e  --
    '--  S a l e  --
    '--  S a l e  --

    '= 3401.311- Staff ID neded for each Sale !!!

    '- STAFF barcode entry--
    '- STAFF barcode entry--

    '=3411.0313-  ENTER-

    Public Sub txtSaleStaffBarcode_Enter(eventsender As Object, EventArgs As System.EventArgs) '= Handles txtSaleStaffBarcode.Enter
        '= If mbIsInitialising Then Exit Sub
        '= Call mClsSale1.txtSaleStaffBarcode_Enter(eventsender, EventArgs)

        '= mBtnSaleHold.Enabled = False

    End Sub  '-txtSaleStaffBarcode_Enter-
    '= = = = = = = = = = =  = = = = = = =

    '--  Enter was Pressed --

    Public Sub txtSaleStaffBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           '== Handles txtSaleStaffBarcode.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim sSql As String
        Dim colResult, colRecord As Collection
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent

        If keyAscii = 13 Then '--enter-
            '--check if some previous sale not committed..
            If (mDgvSaleItems.Rows.Count > 1) Or ((mDgvSaleItems.Rows.Count = 1) AndAlso _
                             (mDgvSaleItems.Rows(0).Cells(k_GRIDCOL_BARCODE).Value <> "")) Then  '--not empty-  Then
                If MsgBox("Discard current sale data ?", _
                          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                    mTxtSaleStaffBarcode.Text = msSaleStaffbarcode  '--restore-
                    Exit Sub
                End If
            End If  '-have items-
            s1 = Trim(mTxtSaleStaffBarcode.Text)
            If (s1 <> "") Then  '--have barcode-
                '--lookup barcode-
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [staff] WHERE (barcode='" & s1 & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    '== Call mbSetupSaleCustomer(colRecord)
                    msSaleStaffbarcode = s1
                    mIntSaleStaff_id = colRecord.Item("staff_id")("value")
                    msSaleStaffName = colRecord.Item("docket_name")("value")
                    mLabSaleStaffName.Text = msSaleStaffName
                    '== mPanelOptTranType.Focus()   '==  '==mDgvSaleItems.Select()   '--focus-
                    mTxtSaleCustBarcode.Enabled = True
                    mLabSaleCust.Enabled = True
                    '=4201.1030=\
                    mBtnCancelSale2.Enabled = True
                    '-- go to cistomer barcode-
                    controlParent.SelectNextControl(textBox1, True, True, True, True)
                Else '--not found..-
                    MsgBox("No Staff Record found for barcode: " & s1, MsgBoxStyle.Exclamation)
                    '-- select text-

                End If  '-get--
            Else  '- no barcode-
                MsgBox("You have to enter a valid Staff barcode: " & s1, MsgBoxStyle.Exclamation)
                '-- allow to pass, but not cust or trans. can go-
                '--  Just use validate--
                '== controlParent.SelectNextControl(textBox1, True, True, True, True)
            End If  '--have barcode-
            keyAscii = 0
        End If  '--key ascii-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub  '--STAFF keypress=
    '= = = = = = = = = = = = = = = 

    '-- STAFF barcode TEXTBOX- Validating --
    '==
    '==  STAFF Barcode-..
    '--   Must catch "Validating" event for TAB key. .- 

    Public Sub txtSaleStaffBarcode_Validating(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As CancelEventArgs) _
                                      '== Handles txtSaleStaffBarcode.Validating

        Dim sBarcode As String
        Dim sSql As String
        Dim colResult, colRecord As Collection

        '= Call mClsSale1.txtSaleCustBarcode_Validating(eventSender, eventArgs)
        If (Trim(mTxtSaleStaffBarcode.Text) = "") Then
            '= eventArgs.Cancel = True
            '- let it go if we're clicking on different page.
            '-- First- mTabControlMain
            'If (mTabControlMain IsNot Nothing) Then
            '    If LCase(mTabControlMain.SelectedTab.Name) <> "tabpagepos" Then
            '        Exit Sub
            '    End If
            'End If
            ''- we're ON POS page if we're in JobMatix..
            'If LCase(mTabControlPOS.SelectedTab.Name) <> "tabpagesale" Then
            '    Exit Sub
            'End If
            eventArgs.Cancel = True
            MsgBox("Must have Staff barcode: " & sBarcode, MsgBoxStyle.Exclamation)
        Else
            '- validate/lookup if not done yet..
            If (msSaleStaffbarcode = "") OrElse _
                   (msSaleStaffbarcode <> Trim(mTxtSaleStaffBarcode.Text)) Then  '-cust not set up--
                '--lookup -
                sBarcode = Trim(mTxtSaleStaffBarcode.Text)
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [staff] WHERE (barcode='" & sBarcode & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    msSaleStaffbarcode = sBarcode
                    mIntSaleStaff_id = colRecord.Item("staff_id")("value")
                    msSaleStaffName = colRecord.Item("docket_name")("value")
                    mLabSaleStaffName.Text = msSaleStaffName
                    '=4201.1030=\
                    mBtnCancelSale2.Enabled = True
                Else '--not found..-
                    eventArgs.Cancel = True
                    MsgBox("No Staff found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
                End If  '-get--
            End If  '-set up-
        End If  '-text-
    End Sub  '-- Staff Validating-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '--  S a l e  --
    '--  S a l e  --
    '--  S a l e  --

    '- Customer barcode entry--
    '- Customer barcode entry--

    Public Sub txtSaleCustBarcode_TextChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs)
        mBtnCancelSale.Enabled = False
        mBtnCancelSale2.Enabled = False

    End Sub  '--txtCustBarcode_TextChanged--
    '= = = = = = = = = = = = = = =  = = = 

    '-- CUSTOMER  Enter Pressed --

    Public Sub txtSaleCustBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)

        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim sSql As String
        Dim colResult, colRecord As Collection
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent

        If keyAscii = 13 Then '--enter-
            '--check if some previous sale not committed..
            If (mDgvSaleItems.Rows.Count > 1) Or ((mDgvSaleItems.Rows.Count = 1) AndAlso _
                             (mDgvSaleItems.Rows(0).Cells(k_GRIDCOL_BARCODE).Value <> "")) Then  '--not empty-  Then
                If MsgBox("Discard current sale data ?", _
                          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                    mTxtSaleCustBarcode.Text = msSaleCustomerBarcode  '--restore-
                    Exit Sub
                End If
            End If  '-have items-
            s1 = Trim(mTxtSaleCustBarcode.Text)
            If (s1 <> "") Then  '--have barcode-
                '--lookup barcode-
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [customer] WHERE (barcode='" & s1 & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    Call mbSetupSaleCustomer(colRecord)
                    '== mPanelOptTranType.Focus()   '==  '==mDgvSaleItems.Select()   '--focus-
                Else '--not found..-
                    MsgBox("No Customer found for barcode: " & s1, MsgBoxStyle.Exclamation)
                End If  '-get--
            Else  '- no barcode-
                '-- allow to pass, but not cust or trans. can go-
                '--  Just use validate--
                controlParent.SelectNextControl(textBox1, True, True, True, True)
            End If  '--have barcode-
            keyAscii = 0
        End If  '--key ascii-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub  '--CUST keypress=
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- Customer Search (F2)..--
    '-- Cust Barcode TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for Cust Lookup--

    Public Sub txtSaleCustBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) '=Handles txtSaleCustBarcode.KeyDown

        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sSql, s1 As String
        Dim colResult, colRecord As Collection

        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup Customer--
            '--check if previous sale not committed..
            If (mDgvSaleItems.Rows.Count > 1) Or ((mDgvSaleItems.Rows.Count = 1) AndAlso _
                             (mDgvSaleItems.Rows(0).Cells(k_GRIDCOL_BARCODE).Value <> "")) Then  '--not empty-  Then
                If MsgBox("Discard current sale ?", _
                      MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                    Exit Sub
                End If
                '-- clear grid.-
                Call mbClearInvoice()
            End If
            '== 3501.1104- Use frmBrowse33..
            '=3519.0317= Special Browse for Customers.
            If Not mbBrowseAndSearchCustomers(mColPrefsCustomer, "Lookup Customer", "", colKeys, colSelectedRow) Then
                '= Not mbBrowseTable(mColPrefsCustomer, "Lookup Customers", "", colKeys, colSelectedRow, "Customer", True) Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else  '--selected
                mTxtSaleCustName.Text = ""
                mLabSaleCustTags.Text = ""
                mToolTip1.SetToolTip(mLabSaleCustTags, "")
                If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                    Call mbSetupSaleCustomer(colSelectedRow)
                    '=3301.710= mPanelOptTranType.Focus()   '== mDgvSaleItems.Select()   '--focus-
                End If
            End If  '-browse-
            '= End With '-frmsale-
        ElseIf (KeyCode = System.Windows.Forms.Keys.F3) And _
                            ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--SAME Customer--
            '-- get previous barcode..
            If msSaleLastCustomerBarcode <> "" Then
                mTxtSaleCustBarcode.Text = msSaleLastCustomerBarcode
                s1 = Trim(mTxtSaleCustBarcode.Text)
                If (s1 <> "") Then  '--have barcode-
                    '--lookup barcode-
                    '--  get recordset as collection for SELECT..--
                    sSql = "SELECT * FROM [customer] WHERE (barcode='" & s1 & "');"
                    If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                           (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                        '--have a row..-
                        colRecord = colResult.Item(1)
                        Call mbSetupSaleCustomer(colRecord)
                        '=3301.710= mPanelOptTranType.Focus()   '==mDgvSaleItems.Select()   '--focus-
                    Else '--not found..-
                        MsgBox("No Customer found for barcode: " & s1, MsgBoxStyle.Exclamation)
                    End If  '-get--
                End If  '--have barcode-
            End If  '--last-
        ElseIf (KeyCode = System.Windows.Forms.Keys.F5) And _
                            ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--SAME Customer--
            '-- make new customer..
            Dim frmCust1 As New frmCustomer
            frmCust1.StaffId = mIntMainStaff_id
            frmCust1.StaffName = msMainStaffName
            frmCust1.SqlServer = msServer
            frmCust1.connectionSql = mCnnSql '--job tracking sql connenction..-
            frmCust1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..

            frmCust1.DBname = msSqlDbName
            frmCust1.VersionPOS = msDLLVersion
            frmCust1.AddNewCustomerOnly = True
            frmCust1.StaffId = mIntSaleStaff_id
            frmCust1.StaffName = msSaleStaffName
            '= frmCust1.form_left = mFrmSale.Left '=  Me.Left
            '= frmCust1.form_top = mFrmSale.Top = 30 '=  Me.Top + 30
            frmCust1.ShowDialog()
            If Not frmCust1.wasCancelled Then
                s1 = frmCust1.selectedBarcode
                If s1 <> "" Then
                    '-- lookup for details..
                    '--  get recordset as collection for SELECT..--
                    sSql = "SELECT * FROM [customer] WHERE (barcode='" & s1 & "');"
                    If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                           (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                        '--have a row..-
                        colRecord = colResult.Item(1)
                        Call mbSetupSaleCustomer(colRecord)
                        '=3301.710= mPanelOptTranType.Focus()   '==mDgvSaleItems.Select()   '--focus-
                    Else '--not found..-
                        MsgBox("No Customer found for barcode: " & s1, MsgBoxStyle.Exclamation)
                    End If  '-get--
                Else
                    MsgBox("No Selection", MsgBoxStyle.Exclamation)
                End If
            End If  '-cancelled-
        End If '--keycode
    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- Customer barcode TEXTBOX- Validating --
    '==
    '==     v3.3.3301.1227..  27-Dec-2016= ===
    '==   Cust Barcode- Must catch "Validating" event for TAB key. .- 

    Public Sub txtSaleCustBarcode_Validating(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As CancelEventArgs) '= Handles txtSaleCustBarcode.Validating
        Dim sBarcode As String
        Dim sSql As String
        Dim colResult, colRecord As Collection

        '= Call mClsSale1.txtSaleCustBarcode_Validating(eventSender, eventArgs)
        If (Trim(mTxtSaleCustBarcode.Text) = "") Then
            eventArgs.Cancel = True
            MsgBox("Must Have customer barcode", MsgBoxStyle.Exclamation)
            '=3307.0223= 
            '-- allow to pass, but not cust or trans. can go-
        Else
            '- validate/lookup if not done yet..
            If (msSaleCustomerBarcode = "") OrElse _
                   (msSaleCustomerBarcode <> Trim(mTxtSaleCustBarcode.Text)) Then  '-cust not set up--
                '--lookup -
                sBarcode = Trim(mTxtSaleCustBarcode.Text)
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [customer] WHERE (barcode='" & sBarcode & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    Call mbSetupSaleCustomer(colRecord)
                    '== mPanelOptTranType.Focus()   '==  '==mDgvSaleItems.Select()   '--focus-
                Else '--not found..-
                    eventArgs.Cancel = True
                    MsgBox("No Customer found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
                End If  '-get--
            End If  '-set up-
        End If  '-text-
    End Sub  '--vailidating-
    '= = = = = = = = = = = = = = = 
    '-===FF->


    '-- Job Search (F2)..--
    '-- JobNo TEXTBOX- Catch F2 on  --
    '--- check for F2 for JobNo Lookup--

    Public Sub txtSaleJobNo_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) '=Handles txtSaleJobNo.KeyDown

        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000


        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                        ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup Deliverable Jobs.--

            MsgBox("Job Delivery still to come..", MsgBoxStyle.Information)

        End If

    End Sub  '-- JobNo key down-
    '= = = = = = = = = = = = = = = 

    '-- Catch ENTER key on Job No..-

    Public Sub txtSaleJobNo_KeyPress(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-

            MsgBox("Job Delivery still to come..", MsgBoxStyle.Information)

            keyAscii = 0
        End If
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If

    End Sub  '-enter-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '--  S a l e  --
    '--  S a l e  --
    '--  S a l e  --

 
    '-- Subs to process Form Events..--
    '-- These are called from ACTUAL event subs on the host Sale Form..-

    '=3519.0331=
    '--  Import Quote..
    '--  Import Quote..
    '--  Import Quote..
    '-- Lookup Quotes for this customer and choose one.
    '==  -- 4201.1028/1031.  31-Oct-2019-  
    '==     -- Importing Quote-- MUST use Quoted Selling price...

    Public Sub labImportQuote_Click(sender As Object, e As EventArgs) '= Handles btnImportQuote.Click

        '-- get quotes again for this customer.
        '-  load list of invoices for this cust..--
        Dim sSql, s1 As String
        Dim rx, intRow, intSalesOrder_id As Integer
        Dim sDateColumn, sIdColumn As String
        Dim datatable1, datatableItems As DataTable

        sSql = "SELECT * FROM dbo.SalesOrder "
        sSql &= " WHERE (Customer_id=" & CStr(mIntSaleCustomer_id) & ")  AND (transactionType ='quote')"
        sSql &= "      AND (DATEDIFF (month,SalesOrder_date, GETDATE()) <=12) "
        sSql &= " ORDER BY SalesOrder_id DESC;"
        If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
            MsgBox("Error in getting recordset for Quotes (SalesOrders) table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        Else
            If (datatable1 Is Nothing) OrElse (datatable1.Rows.Count <= 0) Then
                '= mbLoadInvoiceList = True
                MsgBox("No quotes to show.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If  '-get-
        '- ok-
        '= MsgBox("Have " & datatable1.Rows.Count & " quotes..  Will show list..")

        '-- show list-
        '- select-
        Dim frmListSelect1 As frmListSelect

        frmListSelect1 = New frmListSelect
        frmListSelect1.inData = datatable1
        frmListSelect1.hdrText = "Quotes for " & mTxtSaleCustName.Text
        frmListSelect1.Text = "Quotes for " & mTxtSaleCustName.Text
        sIdColumn = "salesorder_id"
        frmListSelect1.ShowDialog()
        If frmListSelect1.cancelled Then
            '= mbCancelled = True  '== sError = "selection cancelled."
            frmListSelect1.Close()
            Exit Sub
        End If
        '-get selected  row-
        intRow = frmListSelect1.selectedRow
        '- sent back index..
        frmListSelect1.Close()
        '-- then Show Invoice..-
        intSalesOrder_id = datatable1.Rows(intRow).Item(sIdColumn)

        '-- get all Quote lines, and add to Grid..
        sSql = "SELECT *, stock.barcode, stock.track_serial "
        sSql &= " FROM dbo.SalesOrderLine "
        sSql &= "  JOIN stock ON (stock.stock_id= SalesOrderLine.stock_id) "
        sSql &= "  WHERE (SalesOrder_id=" & CStr(intSalesOrder_id) & ")"
        sSql &= "   ORDER BY line_id;"
        If Not gbGetDataTable(mCnnSql, datatableItems, sSql) Then

            MsgBox("Error in getting recordset for SalesOrderLine table: " & vbCrLf & _
                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '==Exit Function '--msg was displayed..
            Exit Sub
        Else '--ok.
        End If '--get-

        Dim sBarcode, sDescription, sSerialNo, sSerialTrail, sError As String
        Dim sSerialsCheckList As String
        Dim intStock_id, intRowx, intCount, intInvoice_id As Integer
        Dim decQty, decSellActual_ex, decSellActual_inc As Decimal
        Dim bTrackSerial, bCanBypass, bIsInStock As Boolean

        For Each row1 As DataRow In datatableItems.Rows
            bTrackSerial = False
            sSerialNo = ""
            sBarcode = CStr(row1.Item("barcode"))
            sDescription = CStr(row1.Item("description"))
            intStock_id = row1.Item("stock_id")
            decQty = row1.Item("quantity")
            decSellActual_ex = row1.Item("sellActual_ex")
            decSellActual_inc = row1.Item("sellActual_inc")
            If CInt(row1.Item("track_serial")) <> 0 Then
                bTrackSerial = True
            End If
            '-- ask for Serial No's if Tracking this item..
            '=4201.0708-  (not if we're importing a quote into a new Quote)
            If bTrackSerial And (Not mbIsQuote) Then
                sSerialsCheckList = ";"
                MsgBox("NB: This next quote line-item will need " & CInt(decQty) & "  Serial Nos.", MsgBoxStyle.Information)
                '-  get decQty serials..
                '=Do While intCount < CInt(decQty) '==For intCount = 1 To CInt(decQty)
                For intCount = 1 To CInt(decQty)
                    sSerialNo = ""
                    bCanBypass = False
                    '- next serial for this quote line..
                    Do While (sSerialNo = "") And (Not bCanBypass)
                        s1 = InputBox("Please enter a SerialNo #" & intCount & " for the Item:" & vbCrLf & _
                                        "Barcode: " & sBarcode & ";   " & sDescription, "Importing Quote")
                        sSerialNo = Trim(s1)
                        If (sSerialNo = "") Then
                            If (MsgBox("Nothing was entered !!" & vbCrLf & _
                                      "Are you sure there's NO Serial No. for this Item ?", _
                                      MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                                bCanBypass = True
                            Else  '-try again.
                            End If '-yes/no-
                        Else
                            '--check serial..
                            If mbCheckSerial(sSerialNo, intStock_id, bIsInStock, mIntSerialAudit_id, _
                                  intInvoice_id, sSerialTrail, sError) Then
                                '-- check it's not a duplicate..
                                If InStr(sSerialsCheckList, UCase(sSerialNo)) > 0 Then
                                    '-duplicate..
                                    '-- re-do inner loop.
                                Else '-ok
                                    sSerialsCheckList &= UCase(sSerialNo) & ";"
                                    '-will fall out of inner loop. and add to grid.
                                End If
                            Else
                                MsgBox("Invalid Serial No.:  " & sSerialNo, MsgBoxStyle.Exclamation)
                                sSerialNo = ""
                            End If  '-check-
                        End If '-serial =""..
                    Loop '= Until sSerialNo <> "" Or bCanBypass
                    '-ok-
                    '- add SERIAL item to grid ANYWAY= (with or without actual serial.)
                    intRowx = mbAddItemToSalesGrid(sBarcode, intStock_id, sSerialNo, _
                                                            sDescription, decQty, True, decSellActual_inc)
                    If Not (intRowx >= 0) Then
                        MsgBox("Failed to add the item '" & sDescription & " to Grid.. ", MsgBoxStyle.Exclamation)
                    End If '--add item.
                Next intCount
                '=Loop  '=  while.
            Else  '-not tracking
                '- add NON-serial to grid=
                intRowx = mbAddItemToSalesGrid(sBarcode, intStock_id, sSerialNo, _
                                                  sDescription, decQty, True, decSellActual_inc)
                If Not (intRowx >= 0) Then
                    MsgBox("Failed to add the item '" & sDescription & " to Grid.. ", MsgBoxStyle.Exclamation)
                End If '--add item.
            End If  '-tracking.
        Next row1
        '-  go to Sale..
        Call btnSaleContinue_Click()
        MsgBox("ok. Quote has been loaded to sales grid..", MsgBoxStyle.Information)

    End Sub  '-btnImportQuote_Click-
    '= = = = = = = = = =  = == = = =
    '-===FF->

    '= 3401.321=
    '-- cboTransaction_SelectedIndexChanged-

    Public Sub optSaleRefundOrQuote_CheckedChanged(ByVal sender As System.Object, _
                                                   ByVal e As System.EventArgs)
        '= Public Sub optSale_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '=     Handles optSaleSale.CheckedChanged, optSaleQuote.CheckedChanged, _
        '=                optSaleLayby.CheckedChanged, optSaleRefund.CheckedChanged

        If mbIsRestoringScreen Then Exit Sub '-just repainting held trans.

        Dim opt1 As RadioButton = CType(sender, RadioButton)
        Dim radioNameSelected As String = CType(sender, RadioButton).Name

        Dim sTransSelected As String  '= = mCboTransaction.SelectedItem

        '= 3411.0311 =
        If opt1.Checked Then
            '-- Select Trans. based on Radio Button..
            opt1.BackColor = Color.LavenderBlush     '= Color.PaleGoldenrod 
            Select Case LCase(radioNameSelected)
                Case "optsalesale"
                    sTransSelected = "sale"
                Case "optsalerefund"
                    sTransSelected = "refund"
                Case "optsalequote"
                    sTransSelected = "quote"
                Case "optsalelayby"
                    sTransSelected = "layby"
                Case Else
                    sTransSelected = "sale"
            End Select

            mbIsRefund = False
            mbIsQuote = False
            mbIsLayby = False
            mGrpBoxRefundType.Enabled = False
            mGrpBoxRefundType.Visible = False
            mDgvSaleItems.Columns("SerialNo").ReadOnly = False
            mLabSaleHelp.Text = ""
            mChkOnAccount.Enabled = False
            mChkOnAccount2.Enabled = False
            mChkOnAccount.Checked = False
            mChkOnAccount2.Checked = False  '-- MUST keep them aligned.
            mDgvSalePaymentDetails.TabStop = True
            mTxtCreditNoteWdl.TabStop = True
            Select Case LCase(sTransSelected)

                Case "sale"
                    '==Is DEFAULT- this opt NEVER Happens..
                    '== NOT  TRUE !!  this will happen when this option selected from Cust setup.
                    '==3301.427= Can happen.. still the default.
                    Call mbSetupSaleDefault()
                    If mbSaleIsAccountCust Then  '--account customer.-
                        mChkOnAccount.Enabled = True
                        mChkOnAccount2.Enabled = True
                        '==4221.0207- 07-Feb-2020..
                        '=     Make chkOnAccount UNCHECKED as the DEFAULT..
                        '==      AND don't disable payments in here..
                        '= mChkOnAccount.Checked = True  '-default for Account Cust.
                        '= mChkOnAccount2.Checked = True   '-- MUST keep them aligned.
                        '= mDgvSalePaymentDetails.TabStop = False
                        '= mTxtCreditNoteWdl.TabStop = False
                        '= mGrpBoxSalePayments.Enabled = False
                    Else  '-not account customer-
                        mChkOnAccount.Enabled = False
                        mChkOnAccount2.Enabled = False
                        mChkOnAccount.Checked = False
                        mChkOnAccount2.Checked = False  '-- MUST keep them aligned.
                    End If

                    mOptRefundEftPosCr.Enabled = False
                    mOptRefundEftPosDr.Enabled = False


                    '==  Target is new Build 4251..
                    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
                    mOptRefundOther.Enabled = False
                    mCboRefundOtherDetails.Enabled = False
                    mCboRefundOtherDetails.SelectedIndex = -1
                    '==  END OF  Target is new Build 4251..


                    mGrpBoxRefundType.Text = "Change Type"
                    mOptRefundCash.Checked = True
                    mGrpBoxRefundType.Enabled = True
                    mGrpBoxRefundType.Visible = True

                Case "refund"
                    '==MsgBox("Button 2..", MsgBoxStyle.Information)
                    mbIsRefund = True
                    mLabSaleTranType.Text = "Refund"
                    msTransactionType = "refund"
                    mLabSaleJobDelivery.Visible = False
                    mTxtSaleJobNo.Visible = False
                    mLabSaleTranType.ForeColor = Color.Tomato
                    '= mPanelPayment.Visible = True
                    mPanelPayment.Enabled = True
                    mDgvSalePaymentDetails.Enabled = False '= True
                    '=3519.0317=
                    mGrpBoxSalePayments.Enabled = False

                    mPanelSaleFooter.Enabled = False
                    '= mLabSaleDiscount.Enabled = True
                    '= mTxtSaleDiscount.Enabled = True
                    mLabSaleInvTotal.Text = "Refund Total"

                    '=3403.1014= Refund same now for all customers..
                    '== If Not mbSaleIsAccountCust Then
                    mOptRefundEftPosCr.Enabled = True
                    mOptRefundEftPosDr.Enabled = True

                    '==  Target is new Build 4251..
                    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
                    mOptRefundOther.Enabled = True
                    mCboRefundOtherDetails.Enabled = False   '-- User has to select opt button to enable Combo-
                    mCboRefundOtherDetails.SelectedIndex = -1
                    '==  END OF  Target is new Build 4251..


                    '= mOptRefundCredit.Checked = True
                    mOptRefundCash.Checked = True
                    mGrpBoxRefundType.Text = "Refund Type"
                    mGrpBoxRefundType.Enabled = True
                    mGrpBoxRefundType.Visible = True
                    '= mLabSaleChargeBalance.Text = "Refund will be saved as Credit Note. "
                    mLabSaleChargeBalance.Visible = False
                    '= End If  '-refund-
                Case "layby"
                    '=3403.430=
                    mbIsLayby = True
                    '= mLabSaleTranType.Text = "New Layby"
                    mLabSaleTranType.Text = "Layby"
                    msTransactionType = "layby"
                    mLabSaleTranType.ForeColor = Color.DarkViolet
                    mLabSaleJobDelivery.Visible = False
                    mTxtSaleJobNo.Visible = False
                    mDgvSalePaymentDetails.Enabled = True   '-can pay deposit-
                    '=3519.0317=
                    mGrpBoxSalePayments.Enabled = True
                    '- but can't use credit note to pay deposit..
                    mTxtCreditNoteWdl.Enabled = False

                    mLabSaleInvTotal.Text = "Layby Total"
                    '== mLabSalePayments.Text = ""
                    '== mDgvSaleItems.Columns("SerialNo").ReadOnly = True
                    mLabSaleChargeBalance.Text = ""
                    mLabSaleChargeBalance.Visible = False
                    mLabSaleHelp.Text = "Start New Layby- "

                Case "quote"
                    '==MsgBox("Button 2..", MsgBoxStyle.Information)
                    mbIsQuote = True
                    mLabSaleTranType.Text = "Quote"
                    msTransactionType = "quote"
                    mLabSaleTranType.ForeColor = Color.DarkOrange
                    mLabSaleJobDelivery.Visible = False
                    mTxtSaleJobNo.Visible = False
                    mDgvSalePaymentDetails.Enabled = False
                    '=3519.0317=
                    mGrpBoxSalePayments.Enabled = False

                    mLabSaleInvTotal.Text = "Quote Total"
                    '== mLabSalePayments.Text = ""
                    '== mDgvSaleItems.Columns("SerialNo").ReadOnly = True
                    mLabSaleChargeBalance.Text = ""
                    mLabSaleChargeBalance.Visible = False
            End Select '-trans-

            mLabSaleTranType.Enabled = True
            '=DEFAULTS to SALE= mLabChooseTrans.Visible = False

            '== NOW CUSTOMER is Chosen FIRST !!  --
            '=- If JobTracking application-
            '--  Check if any jobs to deliver for this customer.

            '=3411.0311=  mBtnSaleContinue.Enabled = True
            '=3411.0311=  mBtnSaleContinue.Select()
            '=3411.0311=  mLabSaleHelp.Text &= "Press Continue to go.."

            '= WATT FOR ENTER key..   Call btnSaleContinue_Click(opt1, New System.EventArgs)
        Else  '-unchecked.
            opt1.BackColor = Color.PaleGoldenrod
        End If  '-op1 checked-
        '= If mbIsInitialising Then Exit Sub
        '= Call mClsSale1.cboTransaction_SelectedIndexChanged(sender, ev)

    End Sub  '-cboTransaction_SelectedIndexChanged-
    '= = = = = = = = = = =  = = = = = = = ==  == = 
    '-===FF->

    '--btnSaleContinue-
    '-- NO button left now.. Just the Code to launch the Transaction..

    Public Sub btnSaleContinue_Click() '= Handles btnSaleContinue.Click

        '== mBtnSaleContinue.Enabled = False
        mPanelOptTranType.Enabled = False
        '==If Not mbIsCreditNote Then
        mLabSaleHelp.Text = "Scan or Enter barcodes for sales items."
        mDgvSaleItems.Enabled = True
        '=3311.225= Allow Footer for Sale -
        '== mPanelSaleFooter.Enabled = False

        mTxtSaleStaffBarcode.Enabled = False

        mTxtSaleItemSerialNo.Enabled = True
        mPanelSaleLineEntry.Enabled = True

        mBtnSaleComments.Enabled = True
        '= mBtnSaleHold.Enabled = True
        '=3501.0731-  No Payments for Quotes.
        '=3501.0731-  No Payments for Quotes.
        '=3501.0731-  No Payments for Quotes.
        If mbIsQuote Or mbIsRefund Then
            mDgvSalePaymentDetails.Enabled = False
            '=3519.0317=
            mGrpBoxSalePayments.Enabled = False

        Else  '-not quote-
            '-4201.1028-  No accomp. payments now are allowed with On-account Sale.
            If Not mbIsOnAccount() Then
                mDgvSalePaymentDetails.Enabled = True
                '=3519.0317=
                mGrpBoxSalePayments.Enabled = True
            Else  '--Trans is to be charged.
                mDgvSalePaymentDetails.Enabled = False
                mGrpBoxSalePayments.Enabled = False
            End If
            '=3519.0414=
            If (msAgedOverDueAmountsList <> "") Then
                MessageBox.Show("Please Note- " & vbCrLf & _
                                  "  Customer has overdue account-" & vbCrLf & vbCrLf & _
                                  msAgedOverDueAmountsList, "Overdue Account", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If '--overdue..
            '="mChkOnAccount"  Leave as it was set up for this Cust.
        End If
        If mbIsQuote Or mbIsRefund Then  '=3501.1029- Added Refund.=
            mChkOnAccount.Enabled = False
            mChkOnAccount2.Enabled = False
        End If

        '=4201.1030-  Must go Forwards.

        While Not mPanelSaleLineEntry.Enabled
            DoEvents()
            Thread.Sleep(100)  '-msecs.
        End While
        mTxtSaleItemBarcode.Select()  '=(True, True)

        '=4201.1030-  NOW can have Footer.  
        mPanelSaleFooter.Enabled = True

    End Sub  '--btnSaleContinue-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- catch ENTER on RadioButtons..

    '-optSaleRefundOrQuote_keyPress-

    Public Sub optSaleRefundOrQuote_keyPress(sender As Object, EventArgs As KeyPressEventArgs)
        '= dles optSaleSale.KeyPress, optSaleRefund.KeyPress, _
        '= tSaleQuote.KeyPress, optSaleLayby.KeyPress
        Dim keyAscii As Short = Asc(EventArgs.KeyChar)

        If keyAscii = 13 Then '--enter-
            Call btnSaleContinue_Click()
            EventArgs.Handled = True
        End If

    End Sub  '-optSaleRefundOrQuote_keyPress-
    '= = = = = = = = = = = = = = = == =  =

    Public Sub optSaleRefundOrQuote_KeyDown(ByVal sender As Object, _
                                          ByVal EventArgs As KeyEventArgs)
        'Handles optSaleSale.KeyDown, optSaleRefund.KeyDown, _
        '                   optSaleQuote.KeyDown, optSaleLayby.KeyDown
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000

        If (KeyCode = System.Windows.Forms.Keys.Tab) Then  '-fake enter-
            Call btnSaleContinue_Click()

        End If  '-tab-
        '= = = = = = ==  =

    End Sub '-PreviewKeyDown-
    '= = = = = = = = = = == =
    '-===FF->

    '-===FF->

    '-==3301.516=  All items now done in same line of text boxes.
    '-==3301.516=  All items now done in same line of text boxes.
    '-==3301.516=  All items now done in same line of text boxes.

    '-- Item Textbox ENTER..
    '-- Textbox Enter control for Item barcode.

    Public Sub txtSaleItemBarcode_Enter(sender As Object, _
                                          ev As System.EventArgs) '= Handles txtSaleItemBarcode.Enter
        '=If mbIsInitialising Then Exit Sub

        mBtnSaleLineOk.Enabled = False
        mTxtSaleItemSerialNo.Enabled = True

        '=3403.719= FORCE recognition of new barcode.
        mDatatableEditingItem = Nothing

        '=3411.0203= 
        '= - Restore these..
        mTxtSaleItemSellPrice.Enabled = True
        mTxtSaleItemQty.Enabled = True

        If mTxtSaleItemBarcode.Text = "" Then
            mTxtSaleItemBarcode.Text = "barcode"
        End If
        mTxtSaleItemBarcode.SelectionStart = 0
        mTxtSaleItemBarcode.SelectionLength = Len(mTxtSaleItemBarcode.Text)

    End Sub '-txtSaleItemBarcode_Enter-
    '= = = = = = = = = = = = = = = = = = = = = =

    '==-- 15-Apr-2018- POS Sale- Catch Mouse-Click on Item Barcode to set Selection stuff....

    Public Sub txtSaleItemBarcode_Click(sender As Object, ev As System.EventArgs) '== Handles txtSaleItemBarcode.Click

        mTxtSaleItemBarcode.SelectionStart = 0
        mTxtSaleItemBarcode.SelectionLength = Len(mTxtSaleItemBarcode.Text)

    End Sub  '-txtSaleItemBarcode_Click-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Stock Item Search (F2)..--
    '-- Stock Item Search (F2)..--
    '-- Stock Item Search (F2)..--

    '-- Barcode TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for STOCK Lookup--

    Public Sub txtSaleItemBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                       '== Handles txtSaleItemBarcode.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sBarcode, sFinalBarcode, sSql, s1 As String
        '== Dim colResult, colRecord As Collection
        Dim intStock_id As Integer

        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup stock--
            '= now uses frmBrowse33 for this. (incl Search).
            If Not mbBrowseAndSearchTable(mColPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, "stock") Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else  '--selected
                '=3403.718= Ouch!   mTxtSaleCustName.Text = ""
                If (Not (colSelectedRow Is Nothing)) AndAlso (colSelectedRow.Count > 0) Then
                    '==Call mbSetupSaleCustomer(colSelectedRow)
                    '= mPanelOptTranType.Focus()   '== mDgvSaleItems.Select()   '--focus-
                    intStock_id = CInt(colSelectedRow("stock_id")("value"))
                    sBarcode = colSelectedRow("barcode")("value")
                    '- setup selected stock item.
                    txtBarcode.Text = sBarcode
                    If mbBeginEdit(sBarcode, sFinalBarcode) Then '--edit started ok.
                        If mbIsSerialItem Then
                            If mbIsQuote Then '--no serials-
                                mBtnSaleLineOk.Enabled = True    '--user can commit this line..
                                mTxtSaleItemSellPrice.Select()
                            Else  '- not quote.. need serial..
                                mTxtSaleItemSerialNo.Select()
                            End If
                        Else  '-not serial-
                            mBtnSaleLineOk.Enabled = True    '--user can commit this line..
                            mTxtSaleItemSellPrice.Select()
                        End If  '-serial-
                    Else  '-- ERROR- no such barcode--
                        '= mBtnSaleLineOk.Enabled = True    '--user can commit this line..
                        '= mTxtSaleItemSellPrice.Select()
                        '- refuse..-
                        mTxtSaleItemBarcode.SelectionStart = 0
                        mTxtSaleItemBarcode.SelectionLength = Len(mTxtSaleItemBarcode.Text)
                    End If '--begin edit.-
                End If
            End If  '-browse-
        End If  '-keycode F2-
    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-txtSaleItemBarcode_TextChanged=
    '==      >> txtSaleItemBarcode_TextChanged Event now captured for ItemBarcode ONLY--
    '==      >> txtSaleItemBarcode_TextChanged Event now captured for ItemBarcode ONLY--


    Public Sub txtSaleItemBarcode_TextChanged(sender As Object, ev As EventArgs)
        '== Handles txtSaleItemBarcode.TextChanged  ONLY --

        '== If mbIsInitialising Then Exit Sub
        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)

        '--  If this is barcode, then reset all other fields..-

        If (LCase(textBox1.Name) = "txtsaleitembarcode") Then
            '=3307.0219=  mPanelOptTranType.Enabled = False
            '=3401.410= 
            '= DROP ALL THIS +
            '= Call mbClearEditLine()
            mTxtSaleItemSerialNo.Enabled = True
        End If  '-barcode..
    End Sub  '-txtSaleItemBarcode-
    '= = = = = = = = = = = = == = == =

    '-- SERIAL Textbox ENTER..
    '-- Textbox Enter control for Item SerialNo.

    Public Sub txtSaleItemSerialNo_Enter(eventSender As Object, _
                                          ev As System.EventArgs) '= Handles txtSaleItemSerialNo.Enter
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent
        '= mBtnSaleLineOk.Enabled = False

        If (mDatatableEditingItem Is Nothing) Then
            '-- No barcode. TAB was pressed... or clicked on next fld.
            If (mTxtSaleItemSerialNo.Text = "") Then
                mTxtSaleItemSerialNo.Text = "serial"
            End If
            mTxtSaleItemSerialNo.SelectionStart = 0
            mTxtSaleItemSerialNo.SelectionLength = Len(mTxtSaleItemSerialNo.Text)
        Else '- active item-
            If mbIsSerialItem AndAlso (Not mbIsQuote) Then
                If (mTxtSaleItemSerialNo.Text = "") Then
                    mTxtSaleItemSerialNo.Text = "serial"
                End If
                mTxtSaleItemSerialNo.SelectionStart = 0
                mTxtSaleItemSerialNo.SelectionLength = Len(mTxtSaleItemSerialNo.Text)
            Else '- not serial- and not quote keep going.-
                If Not mbIsSerialItem Then
                    If (mTxtSaleItemBarcode.Text = "") Then
                        '-- User wants to go Pay..  
                        mTxtSaleItemSerialNo.Enabled = False '--If user back tabs.. force him back to Barcode-
                        mBtnSaleLineOk.Enabled = False   '--force people back to barcode.
                        '-   Disable Price /Qty so he can back Tab to Serial/Barcode.
                        mTxtSaleItemSellPrice.Enabled = False
                        mTxtSaleItemQty.Enabled = False
                        mDgvSalePaymentDetails.Select()
                    Else  '--have barcode- keep going.
                        controlParent.SelectNextControl(textBox1, True, True, True, True)
                    End If
                ElseIf mbIsQuote Then
                    '=3501.0731-  Serial Item PLUS isQuote-
                    '-- keeo going.
                    controlParent.SelectNextControl(textBox1, True, True, True, True)
                End If '-serial-
            End If  '-serial-
        End If  '--nothing-

    End Sub '-txtSaleItemBarcode_Enter-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Handle ENTER for all Line Item textboxes..
    '--   txtSaleItemBarcode-  Enter Key Pressed --

    Public Sub txtSaleItemBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        '== Handles txtSaleItemBarcode.KeyPress, _
        '==  txtSaleItemSerialNo.KeyPress, _
        '==   txtSaleItemCategory.KeyPress, _
        '==    txtSaleItemDescription.KeyPress, _
        '==     txtSaleItemSellPrice.KeyPress, _
        '=      txtSaleItemQty.KeyPress, _
        '==       txtSaleItemExtension.KeyPress()
        '= If mbIsInitialising Then Exit Sub
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent
        Dim sData As String = Trim(textBox1.Text)
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        '== Dim s1, sBarcode, sSql, sSerialNo As String
        '= Dim datatable1 As DataTable

        If (keyAscii = 13) Then '--enter-
            '== If (sData <> "") Then
            '--  If this is barcode, then check if valid etc...-
            If (LCase(textBox1.Name) = "txtsaleitembarcode") Then

                '--  TEMP testing
                '--  Just use validate--
                controlParent.SelectNextControl(textBox1, True, True, True, True)
                '--and then go to serialNo-

            ElseIf (LCase(textBox1.Name) = "txtsaleitemserialno") Then
                '-- SerialNo. Entered.-
                '--  Just use validate--
                controlParent.SelectNextControl(textBox1, True, True, True, True)
                '--and then move on.-

            ElseIf (LCase(textBox1.Name) = "txtsaleitemsellprice") Then

                '--  Just use validate--
                controlParent.SelectNextControl(textBox1, True, True, True, True)
                '--and then move on.-

            ElseIf (LCase(textBox1.Name) = "txtsaleitemqty") Then
                '--  Just use validate--
                controlParent.SelectNextControl(textBox1, True, True, True, True)
                '--and then move on.-

            Else '-other textboxes-
                '--  Just use validate--
                controlParent.SelectNextControl(textBox1, True, True, True, True)

            End If  '-name/barcode etc..
            '== End If  '-data-
            keyAscii = 0  '-done-
        End If  '-enter-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If

    End Sub  '-txtSaleItemBarcode_KeyPress-
    '= = = = = = = =  = = = = = = = = == == 
    '-===FF->

    '-- Handle Validating for all Line Item textboxes..

    '-- JUST validate Barcode and serialNo in case TAB was used..

    Public Sub txtSaleItemBarcode_Validating(ByVal sender As System.Object, _
                                              ByVal ev As CancelEventArgs)
        '== Handles txtSaleItemBarcode.Validating, _
        '==   txtSaleItemSerialNo.Validating, _
        '==    txtSaleItemCategory.Validating, _
        '==    txtSaleItemDescription.Validating, _
        '==      txtSaleItemSellPrice.Validating, _
        '==       txtSaleItemQty.Validating, _
        '==          txtSaleItemExtension.Validating()
        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)
        Dim s1, sBarcode, sFinalBarcode, sSql, sSerialNo, sError, sSerialTrail As String
        '= Dim datatable1 As DataTable
        Dim intAudit_id, intStock_id, intSalesInvoiceId As Integer

        sError = ""
        '--  If this is barcode, then check if valid etc...-
        If (LCase(textBox1.Name) = "txtsaleitembarcode") Then
            '--barcode- check if processed by ENTER key.
            If (mDatatableEditingItem Is Nothing) Then
                '--ENTER or TAB was pressed...
                sBarcode = sData
                If LCase(sBarcode) <> "barcode" And (sBarcode <> "") Then '-have a barcode-
                    If mbBeginEdit(sBarcode, sFinalBarcode) Then '--edit started ok.
                        '=3411.0407=
                        If (sBarcode <> sFinalBarcode) Then  '-was stripped of leading zeroes.
                            textBox1.Text = sFinalBarcode  '--show stripped verion.
                        End If
                        If mbIsSerialItem Then
                            If mbIsQuote Then '--no serials-
                                mBtnSaleLineOk.Enabled = True    '--user can commit this line..
                                '== mTxtSaleItemSellPrice.Select()
                            Else  '- not quote.. need serial..
                                '-- will go to serial.
                            End If
                        Else     '-not serial- 
                            '- let it go-
                            mBtnSaleLineOk.Enabled = True    '--user can commit this line..
                            '== mTxtSaleItemSerialNo.Select()
                        End If  '-serial-
                    Else '-failed- '-no stock item-
                        '== mBtnSaleLineOk.Enabled = True    '--user can commit this line..
                        '=done=  sError &= vbCrLf & "No Stock record for " & sBarcode
                        ev.Cancel = True
                        '= MsgBox(sError, MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If '--begin edit.-
                Else '- no barcode-
                    mTxtSaleItemBarcode.Text = ""
                    '= mTxtSaleItemSerialNo.Enabled =True 
                    sBarcode = ""
                    '-let it go on..
                End If  '-have barcode-
            End If  '-nothing.-
        ElseIf (LCase(textBox1.Name) = "txtsaleitemserialno") Then
            '-- SerialNo. Entered.-
            If (sData = "serial") Then
                sData = ""
            End If
            If (mDatatableEditingItem Is Nothing) Then
                '-- No barcode. TAB was pressed... or clicked on next fld.
                If (sData = "") Then
                    '- no serial and no barcode
                    mBtnSaleLineOk.Enabled = False
                    mTxtSaleItemSerialNo.Text = ""   '--drop 'serial'
                    '--  Pick it up in validated-
                    '--  Go to Payments.
                    '= MsgBox("Must have Barcode or Serial No.", MsgBoxStyle.Exclamation)
                    '= mTxtSaleItemBarcode.Select()
                Else
                    '-- have serial, but no barcode..
                    '-- Lookup serial to identify item..
                    sSerialNo = sData
                    '- Lookup serialno to validate it exists and get barcode..--
                    '-- NO stock_id avail..
                    If mbFindSerial(sSerialNo, -1, intAudit_id, intStock_id, sBarcode, intSalesInvoiceId) Then
                        '- IF refund..  Check for sales Invoice..
                        If mbIsRefund AndAlso (intSalesInvoiceId <= 0) Then '-no sale record.-
                            If (MsgBox("No Sales record for this serial." & vbCrLf & _
                                  "Do you want to accept anyway ?", _
                                  MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                                sError &= vbCrLf & "No Sales record for " & sSerialNo
                                ev.Cancel = True
                                '== mDgvSaleItems.Rows(ev.RowIndex).ErrorText = sError
                                MsgBox(sError, MsgBoxStyle.Exclamation)
                                Exit Sub
                            End If  '--yes-
                        End If
                        '-ok-
                        '--set it up.- (incl barcode)..
                        mTxtSaleItemBarcode.Text = sBarcode
                        mIntSerialAudit_id = intAudit_id   '==3301.1112-
                        If Not mbBeginEdit(sBarcode, sFinalBarcode) Then
                            ev.Cancel = True
                        End If
                        ''--  get recordset as collection for SELECT..--
                        'sSql = "SELECT * FROM [stock] WHERE (barcode='" & sBarcode & "');"
                        'If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
                        '                       (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
                        '    '--have a row..-
                        '    mDgvSaleItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_BARCODE).Value = sBarcode
                        '    Call mbSetupSaleStockItem(dataTable1, lRow)
                        '    mDgvSaleItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value = CStr(intAudit_id)
                        '    '- update invoice total--
                        '    Call mbUpdateSaleTotal()
                        'Else  '--error-
                        '    MsgBox("ERROR: No stock record for Barcode: " & sBarcode, MsgBoxStyle.Exclamation)
                        '    ev.Cancel = True
                        '    mDgvSaleItems.Rows(ev.RowIndex).ErrorText = "ERROR: No stock record for Barcode: " & sBarcode
                        '    Exit Sub
                        'End If  '-get-
                    Else  '--sError has error-
                        sError &= vbCrLf & "Invalid serial no.: " & sSerialNo
                        ev.Cancel = True
                        '== mDgvSaleItems.Rows(ev.RowIndex).ErrorText = sError
                        MsgBox(sError, MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If  '--find serial-
                End If  '-have serial-
            Else  '-have barcode-
                '--editing started..
                If (mIntStock_id > 0) Then
                    '-- have mIntStock_id-
                    sSerialNo = sData
                    If (sData = "") Then
                        '-- just let it go.
                        '==sError &= vbCrLf & "Invalid serial no.: " & sSerialNo
                    Else '-have data-
                        Dim bIsInStock As Boolean
                        Dim intInvoice_id, intId As Integer
                        '=If mbFindSerial(sSerialNo, mIntStock_id, intAudit_id, intStock_id, sBarcode, intSalesInvoiceId) Then
                        If mbCheckSerial(sSerialNo, mIntStock_id, bIsInStock, mIntSerialAudit_id, _
                                        intInvoice_id, sSerialTrail, sError) Then
                            '-ok-
                            If mbIsRefund Then
                                If bIsInStock Then
                                    MsgBox("That Serial item seems to be still in stock." & vbCrLf & _
                                                                    sSerialTrail, MsgBoxStyle.Exclamation)
                                    '-- press on..
                                End If  '-instock-
                                '- else is sale-
                            ElseIf Not bIsInStock Then
                                MsgBox("That Serial item doesn't seem to be in stock !! " & vbCrLf & _
                                       "  It may have been already sold, or RA'd to supplier.." & vbCrLf & _
                                                                sSerialTrail, MsgBoxStyle.Exclamation)
                                '=3519.0404=  Confirm possibly wrong serial.
                                If MessageBox.Show("Are you sure you have the correct Serial Item ?", "Serial Check", _
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                                               MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
                                    sError &= vbCrLf & "Possibly a wrong serial Item ?? : " & sSerialNo & vbCrLf & _
                                               " -- JobMatix suggests checking the Item again.."
                                End If
                                '-- but press on anyway..
                                '=End If  '-instock-
                            Else '-check if we already scanned it..
                                '-- Check that this Serial not already in the Grid ..--
                                If mDgvSaleItems.Rows.Count > 0 Then
                                    For ix As Integer = 0 To (mDgvSaleItems.Rows.Count - 1)
                                        '=If (ix <> ev.RowIndex) Then '--not current row..
                                        s1 = Trim(mDgvSaleItems.Rows(ix).Cells(k_GRIDCOL_SERIALNO).Value)
                                        If (s1 <> "") Then
                                            '-wrong= intId = CInt(mDgvSaleItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_STOCK_ID).Value)
                                            intId = CInt(mDgvSaleItems.Rows(ix).Cells(k_GRIDCOL_STOCK_ID).Value)
                                            If (intId = mIntStock_id) AndAlso (LCase(s1) = LCase(sSerialNo)) Then
                                                sError = "This Serial already used in Grid Row: " & (ix + 1) & ".."
                                                Exit For
                                            End If  '-same-
                                        End If  '--s1-
                                        '=End If  '--not this-
                                    Next ix
                                End If  '-count-
                            End If '-refund-
                        Else  '--sError has error-
                            sError &= vbCrLf & "Invalid serial no.: " & sSerialNo
                        End If  '-find-
                    End If
                    If sError <> "" Then
                        ev.Cancel = True
                        MsgBox(sError, MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                Else
                    ev.Cancel = True
                    MsgBox("ERROR- no stock_id stored..", MsgBoxStyle.Critical)
                End If  '-id-
            End If  '-nothing.

        ElseIf (LCase(textBox1.Name) = "txtsaleitemsellprice") Then
            If (mDatatableEditingItem Is Nothing) Then
                '-- trx is empty. this may be called wrongly
                '--  by the system after Cancel button was hit.
                '- just leave-
            Else '=alive-
                '--User may have changed price. warn if price is below cost..
                '-- back compute tax..  and save for adding item to Grid..
                If IsNumeric(sData) AndAlso (CDec(sData) > 0) Then
                    mDecSellActualIncTax = CDec(sData)
                    '==3411.0205= 
                    '--  Fix to  NOT add tax if Tax COde is NOT GST (eg FRE)... 
                    '= 3501.0829=
                    If (msSalesTaxCode = "GST") Then
                        mDecSellActualExTax = (mDecSellActualIncTax / (1 + (mDecGST_rate / 100)))  '--1.1-
                        '= mDecSellActualTaxAmount = (mDecSellActualIncTax / 11)
                        mDecSellActualTaxAmount = mDecSellActualIncTax - mDecSellActualExTax
                    Else
                        mDecSellActualTaxAmount = 0  '-FREE-
                        mDecSellActualExTax = mDecSellActualIncTax   '-same
                    End If

                    '-mDecSellActualExTax = mDecSellActualIncTax - mDecSellActualTaxAmount
                    '-- update extension..
                     mTxtSaleItemExtension.Text = _
                         FormatCurrency(CDec(mTxtSaleItemSellPrice.Text) * CDec(mTxtSaleItemQty.Text), 2)
                    If (mDecCostIncTax > mDecSellActualIncTax) Then
                        MsgBox("Warning. Sell price is below cost..", MsgBoxStyle.Exclamation)
                    End If
                    '= NOT in validating EVENT !!!  mTxtSaleItemQty.Select()
                Else
                    ev.Cancel = True
                    MsgBox("Price must be numeric, and GT zero..", MsgBoxStyle.Exclamation)
                End If  '-numeric-
            End If '-nothing-
        ElseIf (LCase(textBox1.Name) = "txtsaleitemqty") Then
            If (mDatatableEditingItem Is Nothing) Then
                '-- trx is empty. this may be called wrongly
                '--  by the system after Cancel button was hit.
                '- just leave-
            Else '=alive-
                '- check if qty numeric- 
                '-- save in mDecSellItemQty --
                '=  NB- Only NonStock items can have fraction qty.
                Dim bIsInteger As Boolean = False
                Dim intTest1 As Integer
                If IsNumeric(sData) AndAlso Integer.TryParse(sData, intTest1) Then
                    bIsInteger = True
                End If
                If IsNumeric(sData) AndAlso (CDec(sData) > 0) _
                                     AndAlso (CDec(sData) <= 999) _
                                      AndAlso (mbIsNonStockItem Or bIsInteger) Then
                    mDecSellItemQty = CDec(mTxtSaleItemQty.Text)
                    '-- update extension..
                    mTxtSaleItemExtension.Text = _
                         FormatCurrency(CDec(mTxtSaleItemSellPrice.Text) * CDec(mTxtSaleItemQty.Text), 2)
                    '-- DONE.. add or update grid row..
                    '== Call mbEndItemEdit()
                    '= NOT in validating EVENT !!!  mBtnSaleLineOk.Select()
                Else
                    ev.Cancel = True  '=3411.0421=
                    MsgBox("Qty must be numeric, and GT zero, and less than 1,000.." & _
                              " (Note that fractional qty's are allowed only for non-stock items..)", MsgBoxStyle.Exclamation)
                End If  '-qty numeric.-
            End If  '--nothing-
        Else  '-just move on.-

        End If  '-name-

    End Sub  '--txtSaleItemBarcode_Validating-
    '= = = = = = = = = = = = = = = = = = = == 
    '-===FF->

    '==3307.0218 =
    '-- Handle txtSaleItemBarcode VALIDATED Event for all Line Item textboxes..

    Public Sub txtSaleItemBarcode_Validated(ByVal sender As System.Object, _
                                              ByVal ev As System.EventArgs)
        '= Handles txtSaleItemBarcode.Validated, _
        '=     txtSaleItemSerialNo.Validated, _
        '=     txtSaleItemCategory.Validated, _
        '=     txtSaleItemDescription.Validated, _
        '=     txtSaleItemSellPrice.Validated, _
        '=     txtSaleItemQty.Validated, _
        '=     txtSaleItemExtension.Validated()

        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)
        '= Dim s1, sBarcode, sSql, sSerialNo, sError, sSerialTrail As String
        '= Dim intAudit_id, intStock_id, intSalesInvoiceId As Integer

        '= sError = ""
        '--  If this is barcode, then check if serial needed. etc...-
        If (LCase(textBox1.Name) = "txtsaleitembarcode") Then
            If Not mbIsSerialItem Then  '= mTxtSaleItemSerialNo.Enabled Then '= ReadOnly Then
                '=3401.401= No Serial needed..  Jump to price Box.
                '= mTxtSaleItemSellPrice.Select()
                mTxtSaleItemSerialNo.Text = ""  '--clear "serial"..
                mTxtSaleItemSellPrice.Enabled = True
                mTxtSaleItemQty.Enabled = True
                mBtnSaleLineOk.Enabled = True    '--user can ok this line..  WE Will ask in end edit.
            End If
        ElseIf (LCase(textBox1.Name) = "txtsaleitemserialno") Then
            If (mDatatableEditingItem Is Nothing) Then
                '-- No barcode. TAB was pressed... or clicked on next fld.
                If (sData = "") Or (sData = "serial") Then
                    '- no serial and no barcode--
                    mTxtSaleItemSerialNo.Text = ""
                    '-- User wants to go Pay..  
                    mTxtSaleItemSerialNo.Enabled = False
                    mBtnSaleLineOk.Enabled = False   '--force people back to barcode.
                    '-   Disable Price /Qty so he can back Tab to Barcode.
                    mTxtSaleItemSellPrice.Enabled = False
                    mTxtSaleItemQty.Enabled = False
                    '=3411.0313=  mDgvSalePaymentDetails.Select()
                    mTxtSaleDiscount.Select() '=mBtnDiscountPC.Select()  '-has to go to discount BEFORE payment.
                End If  '-date-
            Else  '--editing started-
                If (sData = "serial") Then
                    mTxtSaleItemSerialNo.Text = ""
                End If
                mTxtSaleItemSellPrice.Enabled = True
                mTxtSaleItemQty.Enabled = True
                mBtnSaleLineOk.Enabled = True    '--user can ok this line..  WE Will ask in end edit.
            End If '-edit item-
        ElseIf (LCase(textBox1.Name) = "txtsaleitemsellprice") Then
            If (mDatatableEditingItem IsNot Nothing) Then
                '=3401.416-  Format Price.
                mTxtSaleItemSellPrice.Text = FormatCurrency(CDec(sData), 2)
                '-TEST-
                '= MsgBox("txtsaleitemsellprice validated ok for: " & mTxtSaleItemSellPrice.Text, MsgBoxStyle.Information)
            Else
                '-- trx is empty. this may because validating was called wrongly
                '--  by the system after Cancel button was hit.
                '- just leave-
            End If
        ElseIf (LCase(textBox1.Name) = "txtsaleitemqty") Then  '=3431.0515= -check stock..
            If (mDatatableEditingItem IsNot Nothing) AndAlso IsNumeric(sData) Then
                Dim decSaleQty, decQtyInStock, decReOrderLevel As Decimal
                '=3501.0731- Bypass for Non-stock item..
                Dim datarow_zero As DataRow = mDatatableEditingItem.Rows(0)
                Dim bIsNonStockItem As Boolean = False
                Dim intNonStock As Integer = CInt(datarow_zero.Item("isNonStockItem"))
                If (intNonStock <> 0) Then
                    bIsNonStockItem = True
                End If
                decSaleQty = CDec(sData)
                decQtyInStock = mDatatableEditingItem.Rows(0).Item("qtyInStock")
                decReOrderLevel = mDatatableEditingItem.Rows(0).Item("reOrderLevel")
                If (Not bIsNonStockItem) AndAlso ((decQtyInStock - decSaleQty) < decReOrderLevel) Then
                    MessageBox.Show("Note: " & vbCrLf & "Stock may need re-ordering.." & vbCrLf & _
                                    "(Qty in Stock: " & CStr(decQtyInStock) & _
                                    "; ReOrderLevel: " & CStr(decReOrderLevel) & ";", _
                                    "Stock levels..", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If  '-nothing.
        End If  '-barcode/serial-

    End Sub  '--txtSaleItemBarcode_Validated-
    '= = = = = = = = = = = = = = = = = = = ==  = = = = = = 

    '-- "OK" to finish Item Edit..

    Public Sub btnSaleLineOk_Click(sender As Object, ev As EventArgs)  '== Handles btnSaleLineOk.Click


        '-- DONE.. add or update grid row..
        Call mbEndItemEdit()

    End Sub  '-btnSaleLineOk-
    '= = = = = = = = = = = = = = 

    '- CLEAR the current item line texts..

    '--btnItemLineClear_Click--

    Public Sub btnItemLineClear_Click(sender As Object, e As EventArgs)  '= Handles btnItemLineClear.Click

        'If (MsgBox("Erase this current item ?", _
        '                MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
        '    Exit Sub
        'End If

        Call mbClearEditLine()  '==3301.1112--       

        '=3403.508- in case of Back TAB..
        mTxtSaleItemSellPrice.Enabled = False
        mTxtSaleItemQty.Enabled = False

        mTxtSaleItemBarcode.Select()

    End Sub  '-- btnItemLineClear_Click-
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- SALE data grid events..--
    '-- SALE data grid events..--
    '-- SALE  data grid events..--

    '-- Enter Row..  update picture.-

    Public Sub dgvSaleItems_RowEnter(ByVal sender As Object, _
                                         ByVal ev As DataGridViewCellEventArgs)

        Dim ix, lRow, lCol As Integer
        Dim sStockId As String
        Dim image1 As Image

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        mDgvSaleItems.Rows(ev.RowIndex).ErrorText = ""
        mDgvSaleItems.Rows(ev.RowIndex).Cells(lCol).ErrorText = ""
        If (lRow >= 0) And (mDgvSaleItems.Rows.Count > 0) Then
            sStockId = mDgvSaleItems.Rows(ev.RowIndex).Cells(k_GRIDCOL_STOCK_ID).Value
            If (sStockId <> "") Then
                '-- show picture if any..

                '=3411.0103= --NOT HERE--
                'If mColStockImages.Contains(sStockId) Then  '--we saved it.-
                '    image1 = mColStockImages.Item(sStockId)
                '    mPicSaleItem.Image = image1
                'End If

            End If  '-id-
        End If  '--selected a row.--
        '= For i = 0 To dgvSaleItems.Rows(e.RowIndex).Cells.Count - 1
        '=   dgvSaleItems(ix, e.RowIndex).Style.BackColor = Color.Yellow
        '= Next ix

    End Sub  '--row enter-
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-UserDeletingRow-
    '-  (via DELETE key..)

    Public Sub dgvSaleItems_UserDeletingRow(ByVal sender As Object, _
                                           ByVal ev As DataGridViewRowCancelEventArgs)
        '== Handles DataGridView1.UserDeletingRow
        Dim sBarcode As String
        Dim rowX As Integer = ev.Row.Index   '= ht.RowIndex

        If mbIsCancelling Then Exit Sub

        If (rowX < 0) Or mDgvSaleItems.Rows(rowX).IsNewRow Then
            MsgBox("Can't delete that row !", MsgBoxStyle.Exclamation)
            ev.Cancel = True
            Exit Sub
        End If
        sBarcode = ev.Row.Cells(k_GRIDCOL_BARCODE).Value
        If Not (MsgBox("Sure you want to delete the selected item: '" & sBarcode & "' ??", _
                                        vbYesNo + vbDefaultButton2 + vbQuestion) = vbYes) Then
            ev.Cancel = True
            Exit Sub
        End If
        '-- ok.. let it delete.-
        '- when done..  MUST update Invoice totals..

    End Sub  '-UserDeletingRow-
    '= = = = = = = = = = = = = = = = =

    '-User Deleted Row-  DONE-
    '-  (via DELETE key..)

    Public Sub dgvSaleItems_UserDeletedRow(ByVal sender As Object, _
                                              ByVal ev As DataGridViewRowEventArgs)
        '== Handles DataGridView1.UserDeletedRow
        Dim gridRow1 As DataGridViewRow = ev.Row

        '- Now deleted..  MUST update Invoice totals..
        '- update invoice total--
        '==330`.518==  Call mbUpdateSaleTotal()

    End Sub  '--dgvSaleItems_UserDeletedRow-
    '= = = = = = = = = = = = = = = = == 

    '- DELETE Button col.--

    Public Sub dgvSaleItems_CellContentClick(ByVal sender As Object, _
                                                  ByVal cellEvent As DataGridViewCellEventArgs)
        '==   Handles dgvSaleItems.CellContentClick
        'Dim rowX As Integer = cellEvent.RowIndex    '= ht.RowIndex
        'Dim sBarcode As String

        'If TypeOf mDgvSaleItems.Columns(cellEvent.ColumnIndex) Is DataGridViewButtonColumn _
        '                                AndAlso Not cellEvent.RowIndex = -1 Then
        '    If (rowX < 0) Or mDgvSaleItems.Rows(rowX).IsNewRow Then
        '        MsgBox("Can't delete that row !", MsgBoxStyle.Exclamation)
        '        Exit Sub
        '    End If
        '    sBarcode = mDgvSaleItems.Rows(rowX).Cells(k_GRIDCOL_BARCODE).Value
        '    If Not (MsgBox("Sure you want to delete the selected item: '" & sBarcode & "' ??", _
        '                                     vbYesNo + vbDefaultButton2 + vbQuestion) = vbYes) Then
        '        Exit Sub
        '    End If
        '    Try
        '        Me.mDgvSaleItems.Rows.RemoveAt(cellEvent.RowIndex)
        '    Catch ex As Exception
        '        MsgBox("Error in deleting Grid Row " & cellEvent.RowIndex & ".." & vbCrLf & _
        '                              ex.Message, MsgBoxStyle.Exclamation)
        '    End Try
        'End If  '-typeOf-
    End Sub '=dgvSaleItems_CellContentClick=
    '= = = = = = = = = = = = =  = = = = = =
    '-===FF->

    '-- datagridView Context menu stuff..--
    '-- DELETE selected row..-
    '== Context MENU NOT USED --
    '== Context MENU NOT USED --

    '===== Public Sub mnuDataGridViewRowDelete_Click(ByVal eventSender As System.Object, _
    '=====                                         ByVal eventArgs As System.EventArgs) _
    '=====                                       Handles mnuDataGridViewRowDelete.Click
    '===== Dim s1 As String = ""
    '===== Dim rowX As Integer = mIntDgvSalesItemsCurrentRow
    '=====     If (rowX < 0) Then
    '=====         Exit Sub
    '=====     End If
    '=====     Try
    '=====         Me.mDgvSaleItems.Rows.RemoveAt(rowX)
    '=====     Catch ex As Exception
    '=====         MsgBox("Error in deleting Grid Row " & rowX & ".." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '=====     End Try
    '===== End Sub '--part serial.-
    '= = = = = = = = = = =
    '-===FF->

    '--mouse activity---
    '--      NOT ANY MPRE-  Shows Popup menu to DELETE selected row..-
    '--Now- Transfers all data to the Edit Line.. and Static vars..
    '==  Lookup Stock Item and Save data table..

    Public Sub dgvSaleItems_CellMouseUp(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As DataGridViewCellMouseEventArgs)
        '= Handles mDgvSaleItems.CellMouseUp
        '=  http://stackoverflow.com/questions/15831680/datagridview-context-menu-always-shows-1-in-hittest

        Dim iButton As Short = eventArgs.Button \ &H100000
        Dim iShift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim ht As DataGridView.HitTestInfo
        ht = mDgvSaleItems.HitTest(eventArgs.Location.X, eventArgs.Location.Y)
        Dim rowX As Integer = eventArgs.RowIndex   '= ht.RowIndex

        If (eventArgs.Button = MouseButtons.Left) Then  '= iButton = 1 Then '--left --
            '-- MsgBox "Left Mouse clicked on row: " & index & "..", vbInformation
            '==3301.518= --mouse activity---  select row to edit--
            '==   AND TRANSFER to edit Line. --
            Dim lRow, lCol, intStock_id As Integer
            '= Dim colRowValues As Collection
            '= dim row1 as DataGridViewRow 
            Dim decSellInc, decQty, decExt As Decimal
            Dim bTracksSerials As Boolean
            Dim sBarcode, sSerialNo, s1, sSql, sErrorMsg, sCategory As String
            Dim datatable1 As DataTable
            Try
                lCol = eventArgs.ColumnIndex
                lRow = eventArgs.RowIndex
                If (lRow >= 0) Then '--NOT in header row--
                    '= MsgBox(" Clicked on Row: " & lRow & ", col :" & lCol)
                    '-- Transfer all data to the Edit Line.. and Static vars..
                    '==  Lookup Stock Item and Save data table..
                    With mDgvSaleItems.Rows(lRow)
                        sBarcode = .Cells(k_GRIDCOL_BARCODE).Value
                        sSerialNo = .Cells(k_GRIDCOL_SERIALNO).Value
                        intStock_id = CInt(.Cells(k_GRIDCOL_STOCK_ID).Value)
                        '--  save final (actual) price and Qty from grid row, and stuff them into text boxes.
                        decSellInc = CDec(.Cells(k_GRIDCOL_SELLACTUAL_INC).Value)
                        decQty = CDec(.Cells(k_GRIDCOL_QTY).Value)
                        decExt = CDec(.Cells(k_GRIDCOL_SELLACTUAL_INC_EXTENDED).Value)
                        bTracksSerials = (.Cells(k_GRIDCOL_TRACK_SERIAL).Value = "1")

                        sSql = "SELECT * FROM [stock] WHERE (stock_id=" & intStock_id & ");"
                        If gbGetDataTable(mCnnSql, datatable1, sSql) Then
                            If (Not (datatable1 Is Nothing)) AndAlso (datatable1.Rows.Count > 0) Then
                                '-- stuff selected barcode into grid's  textbox..
                                '== txtBarcode.Text = datatable1.Rows(0).Item("barcode")
                                '== Call mbSetupSaleStockItem(datatable1, intGridRow)
                            Else
                                MsgBox("No datatable returned..", MsgBoxStyle.Exclamation)
                                Exit Sub
                            End If '--nothing-
                        Else
                            sErrorMsg = gsGetLastSqlErrorMessage()
                            MsgBox("ERROR: No Stock datatable returned for Stock_id: " & intStock_id & _
                                      vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                            Exit Sub
                        End If  '-get-
                    End With  '-mDgvSaleItems-

                    '-- save datatable for EndEdit.. 
                    '-- MUST BE DOWN BELOW..(AFTER setting barcode textbox)..
                    '==   mDatatableEditingItem = datatable1
                    mTxtSaleItemBarcode.Text = sBarcode  '--Clears EVERYTHING-
                    mTxtSaleItemSerialNo.Text = sSerialNo
                    Dim row1 As DataRow = datatable1.Rows(0)
                    msGoodsTaxcode = UCase(row1.Item("goods_taxcode"))
                    msSalesTaxCode = UCase(row1.Item("Sales_taxcode"))

                    '==  In "dgvSaleItems_CellContentClick"   SAVE IsNonStockItem for edit.
                    '==   4201.1013-  SAVE IsNonStockItem for edit.
                    mbIsNonStockItem = (CInt(row1.Item("isNonStockItem")) <> 0)
                    '==   4201.1013- ALSO SAVE Is SerialItem for edit.
                    mbIsSerialItem = bTracksSerials

                    '== mTxtSaleItemCategory.Text = row1.Item("cat1") & "-" & row1.Item("cat2")
                    sCategory = row1.Item("cat1") & "-" & row1.Item("cat2")
                    mTxtSaleItemDescription.Text = sCategory & ":  " & row1.Item("description")
                    mTxtSaleItemSellPrice.Text = FormatCurrency(decSellInc, 2)  '-setup prev. selcted price.-
                    mTxtSaleItemQty.Text = CStr(decQty)  '-setup prev. selcted qty.-
                    mTxtSaleItemExtension.Text = FormatCurrency(decExt, 2)  '-setup prev. extension.
                    '-3401.401= last-
                    mIntStock_id = intStock_id   '--3401.401= save--
                    'If bTracksSerials Then
                    '    mTxtSaleItemSerialNo.Enabled = True
                    'Else
                    '    mTxtSaleItemSerialNo.Enabled = False
                    'End If
                    mTxtSaleItemSerialNo.Enabled = True
                    mTxtSaleItemSellPrice.Enabled = True
                    mTxtSaleItemQty.Enabled = True
                    '-- Now Delete the  row and update Sale Totals..
                    Try
                        Me.mDgvSaleItems.Rows.RemoveAt(eventArgs.RowIndex)
                    Catch ex As Exception
                        MsgBox("Error in deleting Grid Row " & eventArgs.RowIndex & ".." & vbCrLf & _
                                              ex.Message, MsgBoxStyle.Exclamation)
                    End Try
                    '-- save datatable for EndEdit.. 
                    '-- MUST BE HERE (not before setting barcode textbox)..
                    mDatatableEditingItem = datatable1
                    '=3401.410= Re-number-
                    Call mbNumberGridRows(mDgvSaleItems)


                    '- Row gone to Edit Line..update invoice total--
                    Call mbUpdateSaleTotal()

                    '==3411.0103= 03Jan2018=
                    '-- Show Pic if any..
                    '= 3411.0103=  Show Pic HERE if any..
                    Call mbShowStockPicture(row1)

                    '==3301.1227=
                    mBtnSaleLineOk.Enabled = True   '-was ok anyway-
                    mTxtSaleItemSellPrice.Select()

                    '== mIntCurrentRowNo = lRow  '--current edit row.-
                End If '--row--

            Catch ex As Exception
                MsgBox("Runtime Error in JobMatixPOS datagridSale_CellMouseUp (" & lRow & "/" & lCol & ") sub.." & vbCrLf & _
             "Error is " & ex.Message, MsgBoxStyle.Exclamation)
            End Try

        ElseIf (eventArgs.Button = MouseButtons.Right) Then  '=  iButton = 2 Then  '--right..-
            '-- must select row header.--
            '= If Not (ht.Type = DataGridViewHitTestType.RowHeader) Then
            '==MsgBox("Not on row hesder.", MsgBoxStyle.Information)
            '==Exit Sub
            '== End If
            If (rowX <= -1) Or mDgvSaleItems.Rows(rowX).IsNewRow Then
                Exit Sub
            End If

        End If '--button..-
    End Sub '--mouse..-
    '= = = = = = = = =  =
    '-===FF->

    '--mouse activity---  select row to edit--
    '==   AND TRANSFER to edit Line. --
    '-- This Event NOT catched.

    'Public Sub dgvSaleItems_CellMouseClick(ByVal eventSender As System.Object, _
    '                                           ByVal eventArgs As DataGridViewCellMouseEventArgs)

    'End Sub  '--click-
    '= = = = = = = = = = == =

    '-- DataGrid Item Barcode Textbox stuff.--

    '-  catch barcode change event..--
    '-- cell change.--
    '== REDUNDANT ???  --

    Public Sub dgvSaleItems_CellValueChanged(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellEventArgs)

        'Dim lRow, lCol As Integer
        'Dim sBarcode, sText As String
        ''== Dim streamIcon As New StreamReader

        'lCol = eventArgs.ColumnIndex
        'lRow = eventArgs.RowIndex
        'If (lRow >= 0) And (mDgvSaleItems.Rows.Count > 0) Then  '--selected a row.--
        '    If (lCol = k_GRIDCOL_BARCODE) Then  '--status-
        '        sBarcode = mDgvSaleItems.Rows(lRow).Cells(lCol).Value

        '        '== ?????? ==    Call mbSetDataGridIcon(lRow, sStatus, dgvChecklist)

        '    End If  '--status-
        '    '== Call mbSetDataModified()  '== mbDataChanged = True
        'End If  '--row-
    End Sub '= CellValueChangedEvent--
    '= = = = = = = = = = = = = = = = = 
    '= = = = = = = = = = == =
    '-===FF->

    '-- Textbox control has been activated on a cell.-
    '--  set event handlers to deal with the textbox..

    '-- to catch keypress...

    '-- NOT THIS..  it stays with form..--
    '-- NOT THIS..  it stays with form..--

    Public Sub dgvSaleItems_EditingControlShowing(ByVal sender As Object, _
                                                    ByVal e As DataGridViewEditingControlShowingEventArgs)

        'Dim text1 As TextBox = CType(e.Control, TextBox)
        'If (text1 IsNot Nothing) Then
        '    '-- Remove an existing event-handler, if present, to avoid 
        '    '-- adding multiple handlers when the editing control is reused.
        '    RemoveHandler text1.KeyDown, _
        '        New KeyEventHandler(AddressOf dgvSaleItems_KeyDown)
        '    '= RemoveHandler text1.KeyPress, _
        '    '=     New KeyPressEventHandler(AddressOf dgvSaleItems_KeyPress)
        '    '= RemoveHandler text1.DoubleClick, _
        '    '==     New EventHandler(AddressOf dgvSaleItems_DoubleClick)
        'End If
        ''-- Add the event handler. 
        'AddHandler text1.KeyDown, _
        '     New KeyEventHandler(AddressOf dgvSaleItems_KeyDown)
        ''== AddHandler text1.KeyPress, _
        ''==     New KeyPressEventHandler(AddressOf dgvSaleItems_KeyPress)
        ''== AddHandler text1.DoubleClick, _
        ''==             New EventHandler(AddressOf dgvSaleItems_DoubleClick)
    End Sub  '--EditingControlShowing-
    '= = = = = = =  = = = == = = = 

    '-- Grid TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for STOCK Lookup--

    Public Sub dgvSaleItems_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) '= Handles txtPartNo.KeyDown

    End Sub '--keydown.-
    '= = = = = = = = = = = = = = = = =
    '--- SaleItems- C e l l  V a l i d a t i n g--=  
    '--- SaleItems- C e l l  V a l i d a t i n g--=  

    '-- CALLED from ACTUAL event sub on Sale Form !!--

    Public Sub dgvSaleItems_CellValidating(ByVal sender As Object, _
                                                 ByVal ev As DataGridViewCellValidatingEventArgs)

    End Sub  '--cell validating.--
    '= = = = = = = = = = == =
    '-===FF->

    '==  CellEndEdit..--
    '-- CALLED from ACTUAL event sub on Sale Form !!--

    Public Sub dgvSaleItems_CellEndEdit(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        '== Handles dgvSaleItems.CellEndEdit

        ' Clear the row error in case the user presses ESC.   
        mDgvSaleItems.Rows(e.RowIndex).ErrorText = String.Empty

    End Sub  '-CellEndEdit-
    '= = = = = = = = = = == =

    '-- validate Row..-
    '-- CALLED from ACTUAL event sub on Sale Form !!--
    '== NOT NEEDED.. Row is validated in TEXT BOXES before going to grid..-

    Public Sub dgvSaleItems_RowValidating(ByVal sender As Object, _
                                            ByVal ev As DataGridViewCellCancelEventArgs)
        '== Handles dgvSaleItems.RowValidating
        'Dim s1, s2, sBarcode, sId As String
        'Dim dataRow1 As DataGridViewRow = mDgvSaleItems.Rows(ev.RowIndex)

        'If mbIsCancelling Then Exit Sub
        'If dataRow1.IsNewRow Then Exit Sub
        ''--check that row has barcode and stock_id..
        'sBarcode = Trim(dataRow1.Cells(k_GRIDCOL_BARCODE).Value)
        'sId = Trim(dataRow1.Cells(k_GRIDCOL_STOCK_ID).Value)
        'If (sBarcode = "") Or (sId = "") Then
        '    ev.Cancel = True
        '    MsgBox("Invalid Stock barcode, or invalid data on row " & (ev.RowIndex + 1), MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        ''--  Check that row has Serial if this stock item Tracks Serials !!!!
        'If (dataRow1.Cells(k_GRIDCOL_TRACK_SERIAL).Value = "1") Then
        '    s1 = Trim(dataRow1.Cells(k_GRIDCOL_SERIALNO).Value)
        '    s2 = Trim(dataRow1.Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value)
        '    If (s1 = "") Then  '-no serial entered.
        '        If IsNumeric(s2) AndAlso (CInt(s2) = -2) Then  '-was queried and bypassed.
        '            '-ok. continue to exit row. was queried and bypassed.
        '        Else '-query-
        '            If MsgBox("This Stock item '" & sBarcode & "' on row " & (ev.RowIndex + 1) & vbCrLf & _
        '                       "     requires a serial no. !! " & _
        '                       "Do you have a serial no. for this item ? ", _
        '                        MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
        '                ev.Cancel = True  '-go back-
        '            Else '-no serials-
        '                If MsgBox("This Stock item '" & sBarcode & "' on row " & (ev.RowIndex + 1) & vbCrLf & _
        '                            "     requires a serial no. !! " & _
        '                            "Are you SURE there is no serial no. for this item ? ", _
        '                             MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
        '                    ev.Cancel = True  '-no.. so go back-
        '                Else  '- sure no serial-
        '                    dataRow1.Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value = "-2"  '--record vote for no serial.
        '                End If '-sure-
        '            End If  '-msgbox query-
        '        End If  '-query-
        '        Exit Sub
        '    End If '-no serial-
        'End If
    End Sub '-RowValidating-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '--  Double-click.--  ?? -

    Public Sub dgvSaleItems_DoubleClick(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As EventArgs)
        Dim lRow, lCol As Integer
        '== Dim sOldStatus, sNewStatus As String
        Dim txt1 As TextBox = CType(eventSender, TextBox)
        lCol = mDgvSaleItems.CurrentCellAddress.X  '== eventArgs.ColumnIndex
        lRow = mDgvSaleItems.CurrentCellAddress.Y  '= eventArgs.RowIndex

        If (lRow >= 0) And (mDgvSaleItems.Rows.Count > 0) Then  '--selected a row.--
            If (lCol = k_GRIDCOL_BARCODE) Then  '--barcode-
                '== sOldStatus = txt1.Text
                '== '= sNewStatus = mbRotateTaskStatus(sOldStatus)
                '== txt1.Text = sNewStatus    '--show new status.--
                '== mDgvSaleItems.EndEdit()    '--== 3063.0 ==  to force text into grid..
                '== System.Windows.Forms.Application.DoEvents()
            End If  '--status-
        End If  '--row-
        '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
    End Sub '--dbl-click--
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '--chkOnAccount_CheckedChanged-

    '=3403.1014=
    Private mbCheckOnAccountActive As Boolean = False

    Public Sub chkOnAccount_CheckedChanged(sender As Object, ev As EventArgs) '= Handles chkOnAccount.CheckedChanged
        '--   chkOnAccount2.CheckedChanged

        If mbCheckOnAccountActive Then Exit Sub '--don't want double event.
        mbCheckOnAccountActive = True
        If mbIsRestoringScreen Then Exit Sub
        If HasCurrentSale() Then
            '=3519.0117=
            '-- keep checks syncronised..
            Dim checkBox1 As CheckBox = CType(sender, CheckBox)
            If checkBox1.Checked Then
                If LCase(checkBox1.Name) = "chkonaccount" Then
                    mChkOnAccount2.Checked = True
                Else
                    mChkOnAccount.Checked = True
                End If
                '-- disable tabstop for Payments, so user has to click on it..
                mGrpBoxSalePayments.Enabled = False
                mDgvSalePaymentDetails.TabStop = False
                mDgvSalePaymentDetails.Enabled = False
                mTxtCreditNoteWdl.TabStop = False
            Else  '-became unchecked.
                '-- keep checks syncronised..
                If LCase(checkBox1.Name) = "chkonaccount" Then
                    mChkOnAccount2.Checked = False
                Else
                    mChkOnAccount.Checked = False
                End If
                '-- no longer charged to account.
                mGrpBoxSalePayments.Enabled = True
                mDgvSalePaymentDetails.Enabled = True
                mDgvSalePaymentDetails.TabStop = True
                mTxtCreditNoteWdl.TabStop = True
            End If  '-checked.
            Call mbUpdateSaleTotal()
        End If  '-current.
        mbCheckOnAccountActive = False
    End Sub  '-chkOnAccount_CheckedChanged-
    '= = = = = = = =  == = =  == = = = =
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '= 4201.2131.-chkOnAccount catch TAB key..-

    Public Sub chkOnAccount2_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs)
        '= Handles chkOnAccount2.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        If (KeyCode = System.Windows.Forms.Keys.Tab) Then '--treat as enter--
            If (mPanelSaleLineEntry.Enabled) Then
                mTxtSaleItemBarcode.Select()
            Else '-not enabled yet.
                '-do the Continue button to complete sale selection.
                Call btnSaleContinue_Click()
            End If
            eventArgs.Handled = True
        End If  '-tab-
    End Sub '- chkOnAccount_KeyDown=
    '= = = = = = = = == = = = = = 

    '-chkOnAccount_KeyPress-
    '-TOP checkbox- do the Continue button to complete sale selection.
    '-- Bottom- Just move to next control-

    Public Sub chkOnAccount_KeyPress(ByVal sender As Object, _
                                      ByVal EventArgs As System.Windows.Forms.KeyPressEventArgs)
        '= Handles chkOnAccount.KeyPress, chkOnAccount2.keypress
        Dim checkBox1 As CheckBox = CType(sender, CheckBox)
        Dim controlParent As Control = checkBox1.Parent

        If Keys.Enter Then
            EventArgs.Handled = True
            If LCase(checkBox1.Name) = "chkonaccount" Then  '--bottom checkbox..
                '= controlParent.SelectNextControl(checkBox1, True, True, True, True)
                SendKeys.Send("{TAB}")
            Else  '-TOP CheckBox=
                If (mPanelSaleLineEntry.Enabled) Then
                    mTxtSaleItemBarcode.Select()
                Else '-not enabled yet.
                    '-do the Continue button to complete sale selection.
                    Call btnSaleContinue_Click()
                End If
            End If '-top/bottom.
            EventArgs.Handled = True
        End If
    End Sub  '-chkOnAccount_KeyPress-
    '= = = = = = = = = = = = = = = =
    '-===FF->


    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..

    '-- optRefundCash_CheckedChanged-
    Public Sub optRefundCash_CheckedChanged(sender As Object, ev As EventArgs)
        '= Handles optRefundCash.CheckedChanged, _
        '== optRefundCredit.CheckedChanged()
        '= optRefundEftPosCr.CheckedChanged, _
        '= optRefundEftPosDr.CheckedChanged, _
        '= optRefundOther.CheckedChanged()

        If mbIsRestoringScreen Then Exit Sub
        Dim opt1 As RadioButton = CType(sender, RadioButton)

        '- update invoice total--
        If opt1.Checked Then    '-just do it once..-
            Call mbUpdateSaleTotal()
        End If

        '==  Target is new Build 4251..
        '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
        If (LCase(opt1.Name) = "optrefundother") Then
            If opt1.Checked Then    '-wants Other List..-
                mCboRefundOtherDetails.Enabled = True
            Else
                mCboRefundOtherDetails.Enabled = False
                mCboRefundOtherDetails.SelectedIndex = -1
            End If
        End If  '-other-
        '== END  Target is new Build 4251..

    End Sub  '--optRefundCash_CheckedChanged-
    '= = = = = = = =  = = = = = == = = = = = =

    '==  Target is new Build 4251..
    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..

    '--cboRefundOtherDetails_SelectedIndexChanged-

    Public Sub cboRefundOtherDetails_SelectedIndexChanged(sender As Object, ev As EventArgs)
        '= Handles cboRefundOtherDetails.SelectedIndexChanged
        If mbIsRestoringScreen Then Exit Sub
        '= Call mClsSale1.optRefundCash_SelectedIndexChanged(sender, ev)

    End Sub  '-cboRefundOtherDetails_SelectedIndexChanged-
    '= = = = = = = == == = = = = = = = = = = = = = = = = =
    '== END  Target is new Build 4251..
    '== END  Target is new Build 4251..
    '-===FF->

    '--cboSaleDiscountPercent_SelectedIndexChanged-
    '== Handles cboSaleDiscountPercent.SelectedIndexChanged

    '- recompute and set up discount Amount..-
    '==   Updated.- 3519.0414  Started 12-April-2019= 
    '==    --  Make sure that %-calculated discount/discountTax are rounded to 2 decimals.. 
    '==              AND in Account Payments round off invoice amount to 2 digits when comparing with payment.
    '==    

    Public Sub cboSaleDiscountPercent_SelectedIndexChanged(ByVal sender As System.Object, _
                                                             ByVal ev As System.EventArgs)
        Dim combo1 As ComboBox = CType(sender, ComboBox)
        Dim sData As String = Trim(combo1.SelectedItem)
        Dim decPercentage As Decimal

        If Not mCboSaleDiscountPercent.Visible Then Exit Sub '-ignore-
        If mbIsRestoringScreen Then Exit Sub

        If IsNumeric(sData) Then
            decPercentage = CDec(sData)
            mDecDiscount = (mDecSubTotal * decPercentage) / 100
            '- TRUNCATE excess deimals-
            '= mDecDiscount = (Decimal.Truncate(mDecDiscount * 100) / 100)
            '=4201.0727-- Do ROUNDING..  Just in case...
            mDecDiscount = Math.Round(mDecDiscount, 2, MidpointRounding.AwayFromZero)

            mTxtSaleDiscount.Text = FormatCurrency(mDecDiscount, 2)
            '-- show tax split..
            Call mbShowTaxSplit()
            '- update invoice total--
            Call mbUpdateSaleTotal()
            '--save-
            msDiscountSelectedItem = sData   '=3411.0405=

            '=3519.0119- NOT HERE..  This may be OnAccount.
            '--      mDgvSalePaymentDetails.Select()

        End If  '-numeric-
    End Sub  '--cboSaleDiscountPercent_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '=3411.0404=
    '-- Must catch ENTER..
    '= NO! it stuff up everything.

    'Public Sub cboSaleDiscountPercent_keyPress(ByVal sender As System.Object, _
    '                                                         ByVal ev As System.Windows.Forms.KeyPressEventArgs)
    '    '-- handles cboSaleDiscountPercent.keypress
    '    Dim keyAscii As Short = Asc(ev.KeyChar)
    '    If keyAscii = 13 Then '--enter-
    '        '- CRASHES - Call cboSaleDiscountPercent_SelectedIndexChanged(sender, New System.EventArgs)
    '        '= ev.Handled = True
    '    End If
    'End Sub  '-cboSaleDiscountPercent-keypress-
    '= = = = = = = = = = =  = = = == =  = = ==

    '=3519.0119=
    '- --TRY THIS catch ENTER key for TAB function..

    '-- cboSaleDiscountPercent_PreviewKeyDown

    '- PreviewKeyDown is where you preview the key.
    '- Do not put any logic here, instead use the
    '- KeyDown event after setting IsInputKey to true.

    '-- THIS BIT is ACTUALLY in the FORM code..
    'Private Sub cboSaleDiscountPercent_PreviewKeyDown(ByVal sender As Object, _
    '                                                  ByVal ev As PreviewKeyDownEventArgs) _
    '                                                    Handles cboSaleDiscountPercent.PreviewKeyDown
    '    If mbIsInitialising Then Exit Sub
    '    Select Case (ev.KeyCode)
    '        Case Keys.Enter  '= Keys.Down, Keys.Up
    '            ev.IsInputKey = True
    '    End Select
    'End Sub '-cboSaleDiscountPercent_PreviewKeyDown'-

    '-- Combo- Key Down..-
    '-- catch ENTER...-

    Public Sub cboSaleDiscountPercent_KeyDown(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyEventArgs)
        '= Handles cboSaleDiscountPercent.KeyDown
        Dim combo1 As ComboBox = CType(eventSender, ComboBox)
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim controlParent As Control = combo1.Parent

        If (KeyCode = System.Windows.Forms.Keys.Enter) And _
                        ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--Hold--
            '- emulate TAB key..
            '- emulate TAB key..
            '-test=
            '= MsgBox("Comb- got Enter key..", MsgBoxStyle.Information)
            SendKeys.Send("{TAB}")
            '-- SelectNextControl goes back to discount amount.-
            '= controlParent.SelectNextControl(combo1, True, True, True, True)
        End If  '-enter-
    End Sub  '-cboSaleDiscountPercent_KeyDown-
    '= = = = = = = = = = = = = = = = == = = = = = =
    '-===FF->

    '-- DISCOUNT-  Enter Pressed --

    Public Sub txtSaleDiscountCashout_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        '== Handles txtSaleDiscount.KeyPress, txtSaleCashout.KeyPress - SEE ACTUAL FORM-

        Dim text1 As TextBox = CType(eventSender, TextBox)
        Dim sData As String = Trim(text1.Text)
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        '= Dim controlParent As Control = text1.Parent
        Dim controlParent As Control = text1.Parent.Parent  '== THIS is the LAST TABSTOP in this panel..
        '--         So go up one panel..

        If keyAscii = 13 Then '--enter-
            If (sData.Length > 9) Then
                MsgBox("Amount is too long..", MsgBoxStyle.Exclamation)
            ElseIf (sData = "") OrElse mbIsNumeric(sData) Then
                '--ok-
                If (sData <> "") AndAlso mbIsNumeric(sData) Then
                    text1.Text = Format(CDec(sData), "  0.00")
                    If LCase(text1.Name) = "txtsalediscount" Then
                        If (CDec(sData) > mDecSubTotal) Then
                            MsgBox("Discount can't exceed Item SubTotal..", MsgBoxStyle.Exclamation)
                        Else '-ok-
                            mDecDiscount = CDec(sData)
                            '=4201.0727-- Do ROUNDING..  Just in case...
                            mDecDiscount = Math.Round(mDecDiscount, 2, MidpointRounding.AwayFromZero)
                            '- split tax from discount-
                            Call mbShowTaxSplit()
                        End If
                    ElseIf LCase(text1.Name) = "txtsalecashout" Then
                        '= mDecCashout = CDec(sData)
                    End If
                ElseIf (sData = "") Then
                    If LCase(text1.Name) = "txtsalediscount" Then
                        mDecDiscount = 0
                        mTxtSaleDiscountAnalysis.Text = ""
                    ElseIf LCase(text1.Name) = "txtsalecashout" Then
                        '= mDecCashout = 0
                    End If
                End If '--numeric
                '- update invoice total--
                Call mbUpdateSaleTotal()
                '-- navigate--
                If LCase(text1.Name) = "txtsalediscount" Then
                    '= Don't go to payments if being charged.-
                    If mDgvSalePaymentDetails.TabStop And mGrpBoxSalePayments.Enabled Then  '=3519.0118=-not charged to account.
                        If (mDecCreditNoteCreditRemaining > 0) And mTxtCreditNoteWdl.Enabled Then
                            mTxtCreditNoteWdl.Select()
                        Else
                            mDgvSalePaymentDetails.Select()
                        End If
                    Else  '--Payments tabstop disabled..  keep going.
                        controlParent.SelectNextControl(text1, True, True, True, True)
                    End If
                End If
            Else
                MsgBox("Amount must be numeric.  !", MsgBoxStyle.Exclamation)
            End If  '--numeric-
            keyAscii = 0
        End If  '--13-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub  '--txtSaleDiscountCashout_KeyPress-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--DiscountCashout_Validating-

    Public Sub txtSaleDiscountCashout_Validating(ByVal sender As System.Object, _
                                               ByVal ev As CancelEventArgs)
        '==  Handles txtSaleDiscount.Validating, txtSaleCashout.Validating -SEE FORM=

        Dim text1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(text1.Text)

        If (sData.Length > 9) Then
            ev.Cancel = True
            MsgBox("Amount is too long..", MsgBoxStyle.Exclamation)
        ElseIf (sData = "") OrElse mbIsNumeric(sData) Then
            '--ok-
            If LCase(text1.Name) = "txtsalediscount" Then
                If (sData <> "") AndAlso (CDec(sData) > mDecSubTotal) Then
                    ev.Cancel = True
                    MsgBox("Discount can't exceed Item SubTotal..", MsgBoxStyle.Exclamation)
                End If
            ElseIf LCase(text1.Name) = "txtsalecashout" Then
                If (sData <> "") AndAlso (CDec(sData) > 0) Then
                    If (mDecPaymentCashRcvd > 0) Then
                        ev.Cancel = True
                        MsgBox("Can't have both cash-in and cash-out!", MsgBoxStyle.Exclamation)
                    End If
                End If
            End If  '--cashout.-
        Else
            ev.Cancel = True
            MsgBox("Amount must be numeric.  !", MsgBoxStyle.Exclamation)
        End If  '--numeric-
    End Sub  '--discount-
    '= = = = = = = = = = = =
    '-===FF->

    '--DiscountCashout_Validated-

    Public Sub txtSaleDiscountCashout_Validated(ByVal sender As System.Object, _
                                               ByVal ev As System.EventArgs)
        '=  Handles txtSaleDiscount.Validated, txtSaleCashout.Validated -SEE FORM=
        Dim text1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(text1.Text)
        Dim controlParent As Control = text1.Parent.Parent  '== THIS is the LAST TABSTOP in this panel..
        '--         So go up one panel..

        If (sData <> "") AndAlso IsNumeric(sData) Then
            text1.Text = Format(CDec(sData), "  0.00")
            If LCase(text1.Name) = "txtsalediscount" Then
                mDecDiscount = CDec(sData)
                '=4201.0727-- Do ROUNDING..  Just in case...
                mDecDiscount = Math.Round(mDecDiscount, 2, MidpointRounding.AwayFromZero)
                '- split tax from discount-
                Call mbShowTaxSplit()
                'mTxtSaleDiscountAnalysis.Text = ""
                'mDecDiscountTax = mDecDiscount - mDecComputeAmountExTax(mDecDiscount)
                ''-- show discount/tax split.
                'If mDecDiscount > 0 Then
                '    mTxtSaleDiscountAnalysis.Text = _
                '      FormatCurrency((mDecDiscount - mDecDiscountTax), 2) & "/" & FormatCurrency(mDecDiscountTax, 2)
                'End If '-zero-
            ElseIf LCase(text1.Name) = "txtsalecashout" Then
                '= mDecCashout = CDec(sData)
            End If
        ElseIf (sData = "") Then
            If LCase(text1.Name) = "txtsalediscount" Then
                mDecDiscount = 0
                mTxtSaleDiscountAnalysis.Text = ""
            ElseIf LCase(text1.Name) = "txtsalecashout" Then
                '= mDecCashout = 0
            End If
        End If '--numeric
        '- update invoice total--
        Call mbUpdateSaleTotal()

        '==   -- 4219.1126.  06-Nov-2019-  Started 06-November-2019-
        '==      --  New Quotes- "clsPOS34Sale". Fix problem making new Quote..  (Stewart 19/11/2019) 
        '==                User can't navigate back to add items once the Discount section has been arrived at.

        '= controlParent.SelectNextControl(text1, True, True, True, True)
        '-- txtSaleDiscountCashout_Validated=
        '--  Don't SELECT next control if it is a quote--
        If Not mbIsQuote Then
            '-- Select only if NOT Quote- (No where to go if Quote..)
            controlParent.SelectNextControl(text1, True, True, True, True)
        End If
    End Sub  '--discount-
    '= = = = = = = = = = = =
    '-===FF->

    '--- P a y m e n t s --
    '--- P a y m e n t s --
    '==
    '==   Updated.- 3519.0317 
    '==    -- MAJOR-  Add TextBox to Payments panel- 
    '==        for User to decide on Amount of CreditNote to withdraw to pay for Sale.
    '==

    '-txtCreditNoteWdl-
    '-enter-

    Public Sub txtCreditNoteWdl_Enter(sender As Object, _
                                         ev As EventArgs)  '= Handles txtCreditNoteWdl.Enter
        If (mGrpBoxSalePayments.Enabled And mTxtCreditNoteWdl.Enabled) Then
            '-re-enable tabstops in case user wants to add payment to Charged-Sale..
            mTxtCreditNoteWdl.TabStop = True
            mDgvSalePaymentDetails.TabStop = True
            If (mDecCreditNoteCreditRemaining > 0) And (mTxtCreditNoteWdl.Text = "") Then  '-have some credit.
                '- just auto-fill the first time.
                If (mDecCreditNoteCreditRemaining >= mDecBalance) Then
                    '--use balance
                    mTxtCreditNoteWdl.Text = FormatNumber(CDec(mDecBalance), 2)
                Else
                    mTxtCreditNoteWdl.Text = FormatNumber(CDec(mDecCreditNoteCreditRemaining), 2)
                End If  '-balance-
                mTxtCreditNoteWdl.SelectionStart = 0
                mTxtCreditNoteWdl.SelectionLength = Len(mTxtCreditNoteWdl.Text)
            End If  '-credit-
        End If '-enabled-

    End Sub  '-enter-
    '= = = = = = = = = = = = = = = == = 

    '-txtCreditNoteWdl_TextChanged-

    Public Sub txtCreditNoteWdl_TextChanged(sender As Object, _
                                             ev As EventArgs) '==  Handles txtCreditNoteWdl.TextChanged
        '= If mbIsInitialising Then Exit Sub
        '= Call mClsSale1.txtCreditNoteWdl_TextChanged(sender, ev)

    End Sub  '-txtCreditNoteWdl_TextChanged-
    '= = = = = = =  = = = = = = = = = == =

    '-- keypress.

    Public Sub txtCreditNoteWdl_KeyPress(eventsender As Object, _
                                         EventArgs As KeyPressEventArgs)  '= Handles txtCreditNoteWdl.KeyPress

        Dim text1 As TextBox = CType(eventsender, TextBox)
        Dim sData As String = Trim(text1.Text)
        Dim keyAscii As Short = Asc(EventArgs.KeyChar)
        Dim s1 As String
        Dim controlParent As Control = text1.Parent

        If keyAscii = 13 Then '--enter-
            If (sData = "") OrElse mbIsNumeric(sData) Then  '-ok
                If (sData <> "") AndAlso mbIsNumeric(sData) Then
                    text1.Text = FormatNumber(CDec(sData), 2)
                    '- update invoice total--
                    '=Call mbUpdateSaleTotal()
                ElseIf (sData = "") Then  '-go to
                    text1.Text = "0.00"
                End If  '-some data..
                If CDec(text1.Text) <= mDecCreditNoteCreditRemaining Then  '-ok-
                    '- update invoice total--
                    Call mbUpdateSaleTotal()
                    controlParent.SelectNextControl(text1, True, True, True, True)
                Else  '  error-
                    MsgBox("More then available Credit Note balance..", MsgBoxStyle.Exclamation)
                End If
            End If '-ok=
            EventArgs.Handled = True
        End If '-enter-

    End Sub  '-txtCreditNoteWdl_keypress.-
    '= = = = = = =  = = = = = = = = = == =

    Public Sub txtCreditNoteWdl_Validating(sender As Object, _
                                             ev As CancelEventArgs)  '=  Handles txtCreditNoteWdl.Validating
        '= If mbIsInitialising Then Exit Sub

    End Sub  '-txtCreditNoteWdl_TextChanged-
    '= = = = = = =  = = = = = = = = = == =

    Public Sub txtCreditNoteWdl_validated(sender As Object, _
                                             ev As EventArgs)  '= Handles txtCreditNoteWdl.Validated
        '= If mbIsInitialising Then Exit Sub

    End Sub  '-txtCreditNoteWdl_TextChanged-
    '= = = = = = =  = = = = = = = = = == =
    '-===FF->

    '--- P a y m e n t s --
    '--- P a y m e n t s --

    '==  Updated   '=3501.0827=   27-Aug-2018.
    '==     dgvPaymentDetails-- catch F2 (and its colnes) with keypreview..
    '==         -- If Payments balance is still non zero..  load cell with balance..
    '==  Control will pick it up and load into textBox for edit.

    'Public Sub dgvSalePaymentDetails_KeyDown_EditingStartKey(sender As DataGridView, _
    '                                                                    ev As KeyEventArgs)

    '    Dim intRow As Integer = mDgvSalePaymentDetails.CurrentCell.RowIndex
    '    Dim intColumn As Integer = mDgvSalePaymentDetails.CurrentCell.ColumnIndex
    '    Dim sData As String = mDgvSalePaymentDetails.Rows(intRow).Cells(intColumn).Value
    '    Dim sNewText As String = ""

    '    If (sData IsNot Nothing) AndAlso _
    '  IsNumeric(Trim(sData)) AndAlso (CDec(sData) <= 0) Then
    '        If (mDecBalance > 0) Then
    '            sNewText = FormatCurrency(mDecBalance, 2)
    '            sNewText = Replace(sNewText, "$", "")  '--drop dollar sign..
    '            mDgvSalePaymentDetails.Rows(intRow).Cells(intColumn).Value = sNewText
    '            DoEvents()
    '        End If  '--balance-
    '    End If  '--nothing-
    'End Sub  '-dgvSalePaymentDetails_KeyDown_EnterKey
    ''= = = = = = =  = = = = = = = = = = = ==  ==

    '==  Updated   '=3501.0824=   24-Aug-2018.
    '==     dgvPaymentDetails-- catch GRID enter-key with keydown.
    '==         -- If Payments balance debits.. Move Focus onto Commit
    '==
    '==    -- 4201.1007.  07-Oct-2019-  
    '==        -- Payments Grid- Fix crash on Enter Key if currently over-paid....

    Public Sub dgvSalePaymentDetails_KeyDown_EnterKey(sender As DataGridView, _
                                                       ev As KeyEventArgs, _
                                                       ByRef bHandled As Boolean)

        Dim intRow As Integer = mDgvSalePaymentDetails.CurrentCell.RowIndex
        Dim intColumn As Integer = mDgvSalePaymentDetails.CurrentCell.ColumnIndex
        Dim sData As String
        bHandled = False

        If mBtnCommitSale.Enabled Then
            mBtnCommitSale.Select()
            bHandled = True
        ElseIf (mDecBalance > 0) Then
            '-not balanced, not overpaid- Load balance into current cell.
            sData = FormatCurrency(mDecBalance, 2)
            sData = Replace(sData, "$", "")  '--drop dollar sign..
            mDgvSalePaymentDetails.CurrentCell.Value = sData
            Call mbUpdateSaleTotal()
        End If
    End Sub  '-dgvSalePaymentDetails_KeyDown_EnterKey
    '= = = = = = =  = = = = = = = = = = = ==  ==

    '--Editing started..  Load current balance if cell empty (0 ie no value)..

    'Public Sub dgvSalePaymentDetails_EditingControlShowing(ByVal sender As Object, _
    '                                               ByVal ev As DataGridViewEditingControlShowingEventArgs)
    '    '=Handles dgvSalePaymentDetails.EditingControlShowing
    '    Dim text1 As DataGridViewTextBoxEditingControl = CType(ev.Control, DataGridViewTextBoxEditingControl)
    '    If (text1 IsNot Nothing) AndAlso _
    '                 IsNumeric(Trim(text1.Text)) AndAlso (CDec(text1.Text) <= 0) Then
    '        If (mDecBalance > 0) Then
    '            ev.CellStyle.BackColor = Color.LavenderBlush
    '            text1.Text = FormatCurrency(mDecBalance, 2)
    '            '= text1.SelectionStart = 0
    '            '= text1.SelectionLength = Len(text1.Text)
    '        End If  '--balance-
    '    End If  '--nothing-

    'End Sub  '-dgvSalePaymentDetails_EditingControlShowing-
    '= = = = = = = = = = = = = = = = = = = = = = = = = = =

    '-- TEXT box showing-  Enter-
    '--  This Editing DGV textbox event captured to select the text.

    'Public Sub dgvSalePaymentDetails_EditingControlEnter(eventsender As Object, ev As System.EventArgs)
    '    Dim text1 As DataGridViewTextBoxEditingControl = CType(eventsender, DataGridViewTextBoxEditingControl)

    '    If (text1 IsNot Nothing) AndAlso _
    '          IsNumeric(Trim(text1.Text)) AndAlso (CDec(text1.Text) <= 0) Then
    '        If (mDecBalance > 0) Then
    '            text1.Text = FormatCurrency(mDecBalance, 2)
    '            text1.Text = Replace(text1.Text, "$", "")  '--drop dollar sign..
    '            DoEvents()
    '        End If  '--balance-
    '    End If  '--nothing-
    '    If (text1 IsNot Nothing) Then
    '        text1.BackColor = Color.PaleGoldenrod
    '        DoEvents()
    '        text1.SelectionStart = 0
    '        text1.SelectionLength = Len(text1.Text)
    '        '= text1.SelectAll()
    '    End If

    'End Sub  '-EditingControlEnter-
    '= = = = = = = = = =  = = ==  = = = = = = ==


    '--- Payments-- C e l l  Enter --=  
    '--  If First Col (Descr) then TAB to Amount column.--

    Public Sub dgvSalePaymentDetails_CellEnter(sender As Object, ev As DataGridViewCellEventArgs) _
                                                          '-- Handles mDgvSalePaymentDetails.CellEnter
        If (ev.ColumnIndex = 0) Then  '= Or ev.ColumnIndex = 5 Then
            SendKeys.Send("{RIGHT}") '=SendKeys.Send("{TAB}")
        End If

    End Sub '- cell enter-
    '= = = = = = = = = = = = =

    '--- Payments-- C e l l  V a l i d a t i n g--=  
    '--- Payments-- C e l l  V a l i d a t i n g--=  

    Public Sub dgvPaymentDetails_CellValidating(ByVal sender As Object, _
                                                 ByVal ev As DataGridViewCellValidatingEventArgs)

        Dim lRow, lCol As Integer
        Dim sData, s1 As String

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        mDgvSalePaymentDetails.Rows(ev.RowIndex).ErrorText = Nothing
        mDgvSalePaymentDetails.Rows(ev.RowIndex).Cells(lCol).ErrorText = Nothing
        If mDgvSalePaymentDetails.Columns(lCol).Name = "Amount" Then
            '== sData = Me.dgvPaymentDetails.Rows(ev.RowIndex).Cells(lCol).FormattedValue
            sData = Trim(ev.FormattedValue.ToString)
            If (sData.Length > 9) Then
                ev.Cancel = True
                mDgvSalePaymentDetails.Rows(ev.RowIndex).ErrorText = "Amount is too long..  "
                mDgvSalePaymentDetails.Rows(ev.RowIndex).Cells(lCol).ErrorText = "Amount is too long.. ."
                MsgBox("Amount is too long..  !", MsgBoxStyle.Exclamation)
            ElseIf (sData = "") OrElse IsNumeric(sData) Then
                '--ok-  check if cashout also..
                '--  get payment descr.
                s1 = mDgvSalePaymentDetails.Rows(ev.RowIndex).Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value
                If (InStr(LCase(s1), "cash") > 0) Then  '--cash paid.-
                    '== MsgBox("Cash paid: " & sData, MsgBoxStyle.Information)
                    If (sData <> "") AndAlso (CDec(sData) > 0) Then
                        'If (mDecCashout > 0) Then
                        '    ev.Cancel = True
                        '    mDgvSalePaymentDetails.Rows(ev.RowIndex).ErrorText = "Can't have both cash-in and cash-out! "
                        '    mDgvSalePaymentDetails.Rows(ev.RowIndex).Cells(lCol).ErrorText = _
                        '                                                "Can't have both cash-in and cash-out!"
                        '    MsgBox("Can't have both cash-in and cash-out!", MsgBoxStyle.Exclamation)
                        'End If
                    End If
                End If  '--cashout.-
            Else
                ev.Cancel = True
                mDgvSalePaymentDetails.Rows(ev.RowIndex).ErrorText = "Amount must be numeric. "
                mDgvSalePaymentDetails.Rows(ev.RowIndex).Cells(lCol).ErrorText = "Amount must be numeric."
                MsgBox("Amount must be numeric.  !", MsgBoxStyle.Exclamation)
            End If  '--numeric-
        End If '--amount col.-
    End Sub  '--cell validating.--
    '= = = = = = = = = = = = = = = = = =

    '- Validated-
    Public Sub dgvPaymentDetails_CellValidated(ByVal sender As Object, _
                                                       ByVal ev As DataGridViewCellEventArgs)

        Dim lRow, lCol As Integer
        Dim sData As String
        lRow = ev.RowIndex
        lCol = ev.ColumnIndex

        sData = Trim(mDgvSalePaymentDetails.Rows(ev.RowIndex).Cells(lCol).Value)
        If (sData <> "") AndAlso IsNumeric(sData) Then
            mDgvSalePaymentDetails.Rows(ev.RowIndex).Cells(lCol).Value = Format(CDec(sData), "  0.00")
        ElseIf (sData = "") Then
            mDgvSalePaymentDetails.Rows(ev.RowIndex).Cells(lCol).Value = "0.00"
        End If
        Call mbUpdateSaleTotal()
        ' Clear any error messages that may have been set in cell validation.
        mDgvSalePaymentDetails.Rows(ev.RowIndex).ErrorText = Nothing
    End Sub  '-validated-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '--update static Var to keep track of comments.-

    '3403.730= -Comments/Delivery gone to Separate Form..
    '3403.730= -Comments/Delivery gone to Separate Form..

    'Public Sub txtSaleComments_TextChanged(ByVal sender As System.Object, _
    '                                        ByVal e As System.EventArgs)
    '    msSaleComments = Trim(mTxtSaleComments.Text)
    'End Sub '-Comments_TextChanged-
    ''= = = = = = = = = = = =  = = = =
    ''--update static Var to keep track of comments.-

    'Public Sub txtSaleDelivery_TextChanged(ByVal sender As System.Object, _
    '                                        ByVal e As System.EventArgs)
    '    msSaleDeliveryInstr = Trim(mTxtSaleDelivery.Text)
    'End Sub '-delivery_TextChanged-
    '= = = = = = = = = = = =  = = = =

    '==3403.730--
    '- btnSaleComments--
    '- load/show  Comments Form.

    Public Sub btnSaleComments_Click(sender As Object, ev As EventArgs) '== Handles btnSaleComments.Click
        Dim frmComments1 As frmSaleComments

        '-- pass current comment vale..
        frmComments1 = New frmSaleComments(msSaleComments, msSaleDeliveryInstr)

        frmComments1.ShowDialog()
        If Not frmComments1.wasCancelled Then
            '--save new values.
            msSaleComments = frmComments1.commentInfo
            msSaleDeliveryInstr = frmComments1.deliveryInfo
        End If
        '-- thats all=

    End Sub '-btnSaleComments_Click-
    '= = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- C o m m i t  S a l e - (or Refund)-
    '-- C o m m i t  S a l e --
    '-- C o m m i t  S a l e --

    '- Lock in case of multiple Commit button hits..
    Private mbCommitLocked As Boolean = False  '- Lock in case of multiple Commit button hits..

    '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
    '- 2. INSERT Main Invoice record -  "SALE" or "REFUND"..  ("CREDITNOTE" is a (Non-A/c Sale) that's overpaid)--
    '--   Note: if AccountCust, AND IsOnAccount checked, 
    '--                    AND partially paid, then column "isOnAccount" must be set to TRUE.
    '--             and an "accountPayment" invoice record written out for Amount Paid.
    '--    NB:  CreditNote is produced by a SALE Invoice, but with zero lines and value, and some  Paymant Received...
    '- 3. Retrieve Invoice No. (IDENTITY of Invoice record written.)-
    '- 4. FOR EACH Invoice Line- INSERT Invoice-Line.. -
    '--                     And ->  Retrieve Invoice Line_id (IDENTITY of Line record written.)- -
    '--                                       ( for SerialAuditTrail)
    '--                     And ->  UPDATE Stock Record with +/- Qty. (IF NOT Service/labour) -
    '--                     And ->  UPDATE SerialAudit status -> SOLD. -
    '--                     And ->  INSERT SerialAuditTrail record.. (SALE/REFUND (INSTOCK)).-
    '--4-X  Write out "accountPayment" invoice record if needed to record payment for Statements.
    '--
    '- 5. INSERT Payment Record.. -
    '- 6. Retrieve Payment No. (IDENTITY of Payment record written.)-
    '- 7. FOR EACH Payment Detail: INSERT Payment-Detail Line.-
    '- 8. INSERT Payment-Disbursement Table Row for THIS SALE Invoice.-
    '- -
    '- 9.  Commit TRANSACTION.-
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    Public Sub btnCommitSale_Click(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) '== Handles btnCommitSale.Click

        Const k_statusDelivered As String = "70-Delivered"

        If mbCommitLocked Then
            Exit Sub
        End If
        '-- lock now--
        mbCommitLocked = True

        Dim sqlTransaction1 As OleDbTransaction
        Dim sMainTable, sLineTable, sBarcode As String
        Dim sSql, sValues, sQty, sAudit_id, sStock_id, sSerialNo As String
        Dim v2 As Object
        Dim bIsCredit As Boolean = (LCase(msTransactionType) = "refund")
        Dim bCanPrint, bCanEmail, bIsOnAccount As Boolean
        Dim bReallyWantsA4Invoice As Boolean = False
        Dim intInvoice_id, intInvoiceLine_id, intID As Integer
        Dim intAccountPaymentInvoice_id As Integer = -1
        Dim intPayment_Id As Integer = -1
        Dim row1 As DataGridViewRow
        Dim sPayAmount, sDescription, sKey, sJobStatus As String
        Dim decDiscountNett, decTotalEx, decNettTax As Decimal
        Dim decCost_ex, decSellActual_ex, decGrossProfit As Decimal
        Dim decCreditNote As Decimal = 0
        '=3311.226=
        Dim sSelectedInvoicePrinterName As String = ""
        Dim sSelectedReceiptPrinterName As String = ""

        '==3107.724- Can't charge CASHOUT to Account.-
        'If (Not mbIsQuote) AndAlso mbSaleIsAccountCust AndAlso _
        '           (mDecBalance > 0) AndAlso (mDecCashout > 0) Then
        '    MsgBox("Can't charge CASHOUT to Account..", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If '-charging cashout-

        '=3301.606= -- set up refunded Amount split..
        Dim decRefundedAsCash As Decimal = 0
        Dim decRefundedAsCreditNote As Decimal = 0
        Dim decRefundedAsEftPosDr As Decimal = 0
        Dim decRefundedAsEftPosCr As Decimal = 0


        '==  Target is new Build 4251..
        '==  Target is new Build 4251..
        '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
        Dim decRefundedAsOther As Decimal = 0
        Dim strRefundOtherDetailkey As String = ""
        '== END of  Target is new Build 4251..
 


        '=3501.1029=  Block Commit if no Sale Item and no Payment.
        If (mDgvSaleItems.Rows.Count <= 0) And (mDecPaymentTotalRcvd <= 0) Then
            MsgBox("There's nothing to Commit..", MsgBoxStyle.Exclamation)
            mbCommitLocked = False
            Exit Sub
        ElseIf (mDgvSaleItems.Rows.Count <= 0) And (mChkOnAccount.Checked) Then
            MsgBox("There's no item to charge to the Account..", MsgBoxStyle.Exclamation)
            mbCommitLocked = False
            Exit Sub
        End If

        
        '==  
        '==   Target-New-Build-4277 -- (Started 07-October-2020)
        '==   Target-New-Build-4277 -- (Started 07-October-2020)
        '==
        '==  (c) POS Sales-  Warn if transaction is taking in more than $500.00 into Credit Note..
        '==
        If (mDecChangeAsCreditNote > 500) Then
            If MessageBox.Show(mFrmSale, "Please Note- " & vbCrLf & "  You are taking in a Payment," & vbCrLf & _
                        "   which will add " & FormatCurrency(mDecChangeAsCreditNote, 2) & _
                        " to this Customer's Credit Note balance.. " & vbCrLf & vbCrLf & _
                         "Are you sure this is correct ? " & vbCrLf & vbCrLf, "Credit Note $500 warning..", _
                              MessageBoxButtons.YesNo, MsgBoxStyle.Question, _
                                                        MessageBoxDefaultButton.Button2) <> MsgBoxResult.Yes Then
                '- don't commit..
                mbCommitLocked = False
                Exit Sub
            End If '--yes/no.-
        End If  '-credit note amt.
        '-ok. keep going.
        '== END  Target-New-Build-4277 -- (Started 07-October-2020)

        
        '=3519.0117- 
        '==  - confirm OnAccount Sale with payment.
        'If (LCase(msTransactionType) = "sale") AndAlso _
        '              mbIsOnAccount() AndAlso (mDecPaymentTotalRcvd > 0) Then
        '    If MsgBox("Please Note- " & vbCrLf & "  You are completing an On Account Sale," & vbCrLf & _
        '                "   with an accompanying payment.. " & vbCrLf & vbCrLf & _
        '                 "This has been disallowed ! " & vbCrLf & vbCrLf, _
        '                        MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '        '-ok. keep going.
        '    Else  '- don't commit..
        '        mbCommitLocked = False
        '        Exit Sub
        '    End If '--yes/no.-
        'End If '--account sale-

        If mbIsRefund Then
            If mOptRefundCredit.Checked Then
                decRefundedAsCreditNote = Abs(mDecBalance)
            ElseIf mOptRefundCash.Checked Then  '-cash
                decRefundedAsCash = Abs(mDecBalance)
            ElseIf mOptRefundEftPosDr.Checked Then  '-eftpos dr-
                decRefundedAsEftPosDr = Abs(mDecBalance)
            ElseIf mOptRefundEftPosCr.Checked Then  '-eftpos cr-
                decRefundedAsEftPosCr = Abs(mDecBalance)


                '==  Target is new Build 4251..
                '==  Target is new Build 4251..
                '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
            ElseIf mOptRefundOther.Checked Then  '-eftpos cr-
                decRefundedAsOther = Abs(mDecBalance)
                strRefundOtherDetailkey = Trim(mCboRefundOtherDetails.SelectedItem)


                '==  Target-New-Build-4253..
                '==
                '==   1. MAIN REASON is FIXING implementation of EXTENDED Refund Details for REFUNDS..
                '==             -- ERROR-  If "Other" Payment Type is chosen, BUT NO combo list item was selected, 
                '==                     then BLANK Type is recorded in Payment Column..
                If (mCboRefundOtherDetails.SelectedIndex < 0) Or (strRefundOtherDetailkey = "") Then
                    MsgBox("Can't commit- " & vbCrLf & _
                                    " No refund OTHER Dropdown type selected.", MsgBoxStyle.Exclamation)
                    mbCommitLocked = False
                    Exit Sub
                End If
                '== END  Target-New-Build-4253..


            Else
                MsgBox("Can't commit- No refund type selected.", MsgBoxStyle.Exclamation)
                mbCommitLocked = False
                Exit Sub
                '== END of  Target is new Build 4251..

            End If
        Else  '- not refund-
            If (mDecChangeAsCreditNote > 0) Then
                '= decRefundedAsCreditNote = mDecChangeAsCreditNote
                '=decRefundedAsCash = 0
            End If
        End If  '-refund-
        '=   refundCashAmount MONEY NOT NULL DEFAULT 0,"
        '==  refundCreditNoteAmount MONEY NOT NULL DEFAULT 0,"
        bCanPrint = True   '-for Quote-
        '= 3107.831-- confirmations..-
        If (Not mbIsQuote) Then
            Dim dlgQueryCommit1 As New dlgQueryCommit
            '=3411.0109=
            dlgQueryCommit1.labPdfPrinter.Text = msPdfPrinterName

            '-check Customer email address.
            '= 4201.1030-  Allow emailing of Invoice for Non-account Sales.
            dlgQueryCommit1.labEmail.Text = ""
            If (msCustomerEmail <> "") And _
                             (msPdfPrinterName <> "") And (mbAllowEmailInvoices) Then
                dlgQueryCommit1.chkEmail.Enabled = True
                If mbIsOnAccount() Then
                    dlgQueryCommit1.chkEmail.Checked = True
                End If
                dlgQueryCommit1.labEmail.Text = msCustomerEmail
            Else '-no email-
                dlgQueryCommit1.chkEmail.Enabled = False
                dlgQueryCommit1.chkEmail.Checked = False
            End If
            If bIsCredit Then
                dlgQueryCommit1.Text = "Committing Refund.."
                '== dlgQueryCommit1.labMessage.Text = "Refunding Cash: " & FormatCurrency(mDecPaymentCashRcvd, 2) & ".."
                dlgQueryCommit1.labMessage.Text = mLabSaleChargeBalance.Text
                dlgQueryCommit1.labQuestion.Text = "OK to commit this REFUND ?"
            Else '-dr-
                dlgQueryCommit1.Text = IIf(mbIsLayby, "Committing Layby.", "Committing SALE.")
                dlgQueryCommit1.labMessage.Text = "Change is : " & FormatCurrency(mDecChange, 2) & ".."
                If (mDecChangeAsCreditNote > 0) Then
                    dlgQueryCommit1.labMessage.Text &= vbCrLf & _
                                  "Saved as CreditNote: " & FormatCurrency(mDecChangeAsCreditNote, 2) & ".."
                End If
                dlgQueryCommit1.labQuestion.Text = IIf(mbIsLayby, "OK to commit this LAYBY ?", "OK to commit this SALE ?")
            End If
            '- 3311.226=  choose doc.
            If mbSaleIsAccountCust And mbIsOnAccount() Then
                dlgQueryCommit1.labDocType.Text = "Invoice"
            Else
                dlgQueryCommit1.labDocType.Text = "Receipt"
            End If
            '--  Make "auto" printing easy to flow on..
            dlgQueryCommit1.chkPrint.Checked = True
            dlgQueryCommit1.ShowDialog()
            If dlgQueryCommit1.DialogResult = DialogResult.Cancel Then
                mbCommitLocked = False
                Exit Sub
            Else  '-ok-
                '-save selections.
                sSelectedInvoicePrinterName = dlgQueryCommit1.selectedInvoicePrinter
                sSelectedReceiptPrinterName = dlgQueryCommit1.selectedReceiptPrinter
            End If
            '-ok=
            '--  Get checkbox results..
            '= MsgBox("TEST-  " & vbCrLf & "Email checked=" & dlgQueryCommit1.chkEmail.Checked & vbCrLf & _
            '=                 "Print checked= " & dlgQueryCommit1.chkPrint.Checked, MsgBoxStyle.Information)
            '- save print preferences..
            bCanPrint = dlgQueryCommit1.chkPrint.Checked
            bCanEmail = dlgQueryCommit1.chkEmail.Checked
            bReallyWantsA4Invoice = dlgQueryCommit1.A4InvoiceChecked

            '=3103.117 = Job Delivery=
            If (mIntJob_id > 0) Then
                If MsgBox("This will complete Delivery of job:" & mIntJob_id & ".." & vbCrLf & _
                            "Do you want to continue? " & vbCrLf & vbCrLf, _
                              MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    sJobStatus = k_statusDelivered
                Else  '- don't commit..
                    mbCommitLocked = False
                    Exit Sub
                End If '--yes/no.-
            End If

        End If  '-not quote-

        sMainTable = "invoice"
        sLineTable = "invoiceLine"
        If mbIsQuote Then
            sMainTable = "SalesOrder"
            sLineTable = "SalesOrderLine"
        Else  '-not quote-
            If mbIsLayby Then
                sMainTable = "Layby"
                sLineTable = "LaybyLine"
            End If
            If Not bIsCredit Then  '-sale-
                '-3401.327= update comments to show change if any.
                msSaleComments &= sMainTable & ":" & vbCrLf & _
                         "Cash Tendered: " & FormatCurrency(mDecPaymentCashRcvd, 2) & _
                         ";   Change: " & FormatCurrency(mDecChange, 2) & vbCrLf & msSaleComments
                '=3411.0107 = Job Delivery= Report-
                If (mIntJob_id > 0) AndAlso (msSaleJobReport <> "") Then
                    msSaleComments &= vbCrLf & "Job-Report:" & vbCrLf & msSaleJobReport
                End If
            End If  '-credit-
        End If '-quote-
        '- bIsOnAccount means not fully paid here and now-
        '--  ie so full amount is charged, and a separate Payment record made for the INCOMING-
        '= 3307.0211- bIsOnAccount = (Not mbIsQuote) AndAlso _
        '= 3307.0211-               mbSaleIsAccountCust AndAlso (mDecInvoiceTotal > mDecPaymentTotalRcvd)
        bIsOnAccount = (Not (mbIsQuote Or mbIsLayby)) AndAlso mbIsOnAccount() '=3402.1014= mbSaleIsAccountCust
        mLabSaleHelp2.Text = "Saving Invoice.."
        decDiscountNett = mDecDiscount - mDecDiscountTax
        decNettTax = mDecTotalTax - mDecDiscountTax
        decTotalEx = mDecInvoiceTotal - decNettTax
        mCnnSql.ChangeDatabase(msSqlDbName) '--USE our db..-

        Dim colPayDetails As New Collection
        '- WAS moved to here so is available later for Till Opening.

        Try  '--Main try-

            '==  3301.1211-11Dec2016=  ADD CashDrawer and CurrentUserName to INVOICES and Payments=
            '==  3301.1211-11Dec2016=  ADD CashDrawer and CurrentUserName to INVOICES and Payments=

            '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
            sqlTransaction1 = mCnnSql.BeginTransaction

            '- 2. INSERT Main Invoice record -
            sSql = "INSERT INTO dbo." & sMainTable & " ("
            sSql &= "  staff_id, customer_id, transactionType,  "
            sValues = "VALUES ( " & CStr(mIntSaleStaff_id) & ", " & _
                          CStr(mIntSaleCustomer_id) & ", '" & msTransactionType & "', "
            If Not mbIsQuote Then
                If Not mbIsLayby Then
                    sSql &= " JobNumber, delivered_layby_id, terminal_id, cashDrawer, currentWindowsUserName,  "
                    sSql &= " isOnAccount, "
                    sValues &= CStr(mIntJob_id) & ", " & CStr(mIntDeliveredLayby_id) & ", '" & msComputerName & "', "
                    sValues &= "'" & gsGetCurrentCashDrawer() & "', '" & msCurrentUserName & "', "
                    sValues &= IIf(bIsOnAccount, "1, ", "0, ")
                Else  '-layby-
                    sSql &= " JobNumber, terminal_id, cashDrawer, currentWindowsUserName,  "
                    sValues &= CStr(mIntJob_id) & ", '" & msComputerName & "', "
                    sValues &= "'" & gsGetCurrentCashDrawer() & "', '" & msCurrentUserName & "', "
                End If
            End If  '-quote-
            '==  3301.531- THESE WERE MISSING:  
            '--    -subtotal_ex_non_taxable- and  -subtotal_ex_taxable=
            '=3401.322.. NOT for Quotes..
            If Not mbIsQuote Then
                sSql &= " subtotal_ex_non_taxable, subtotal_ex_taxable, "
                sValues &= CStr(mDecSubTotalEx_non_taxable) & ", " & CStr(mDecSubTotalEx_taxable) & ", "
            End If
            sSql &= " subtotal_tax, subtotal_inc, discount_nett, discount_tax, "
            sValues &= CStr(mDecTotalTax) & ", " & CStr(mDecSubTotal) & ", " & _
                                    CStr(decDiscountNett) & ", " & CStr(mDecDiscountTax) & ", "
            'If Not mbIsQuote Then
            '    sSql &= "  cashOut, "
            '    sValues &= CStr(mDecCashout) & ", "
            'End If
            sSql &= " rounding, total_ex, total_tax, total_inc, "
            sValues &= CStr(mDecRounding) & ", "
            sValues &= CStr(decTotalEx) & ", " & CStr(decNettTax) & ", " & CStr(mDecInvoiceTotal) & ", "
            '=If (Not mbIsQuote) AndAlso mbSaleIsAccountCust Then
            '=sSql &= "  amountDebitedToAccount, "
            '=sValues &= CStr(mDecBalance) & ", "
            '=End If

            '-- Now Finish-
            sSql &= " comments, deliveryInstructions  "
            sSql &= ") "
            sValues &= " '" & gsFixSqlStr(msSaleComments) & "', '" & gsFixSqlStr(msSaleDeliveryInstr) & "' "
            sValues &= "); "

            If Not mbExecuteSql(mCnnSql, sSql & sValues, True, sqlTransaction1) Then
                mLabSaleHelp2.Text = "Saving " & msTransactionType & " FAILED.."
                mbCommitLocked = False
                Exit Sub
            End If  '--exec invoice-

            '- 3. Retrieve Invoice No. (IDENTITY of Invoice record written.)-
            '= sSql = "SELECT CAST(SCOPE_IDENTITY ('dbo." & sMainTable & "') AS int);"
            sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
            If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                intInvoice_id = intID
                '-- update invoice display later..-
            Else
                MsgBox("Failed to retrieve latest invoice No..", MsgBoxStyle.Exclamation)
                mbCommitLocked = False
                Exit Sub
            End If
            mLabSaleHelp2.Text = "Saving Invoice #" & intInvoice_id & " details.."
            DoEvents()
            If mbIsLayby Then
                mIntLayby_id = intID
                mLabSaleHelp2.Text = "Saving LAYBY #" & intInvoice_id & " details.."
            End If

            '- 4. FOR EACH Invoice Line- INSERT Invoice-Line.. -
            '--                     And ->  UPDATE Stock Record with +/- Qty. (IF NOT Service/labour) -
            If (mDgvSaleItems.Rows.Count > 0) Then
                For Each row1 In mDgvSaleItems.Rows
                    '=decAmount = CDec(row1.Cells(k_GRIDCOL_EXTENSION).Value)
                    '=decAmount = CDec(row1.Cells(k_GRIDCOL_SELL_TAX_EXTENDED).Value)
                    '-- BYPASS EMPTY edit row at the end..--
                    If (row1.Cells(k_GRIDCOL_BARCODE).Value <> "") Then  '--not empty-
                        With row1
                            sBarcode = .Cells(k_GRIDCOL_BARCODE).Value
                            sQty = .Cells(k_GRIDCOL_QTY).Value
                            sStock_id = .Cells(k_GRIDCOL_STOCK_ID).Value
                            '=3403.717=- decCost_ex, decSellActual_ex, decGrossProfit As Decimal
                            decCost_ex = CDec(.Cells(k_GRIDCOL_COST_EX).Value)
                            decSellActual_ex = CDec(.Cells(k_GRIDCOL_SELLACTUAL_EX).Value)
                            decGrossProfit = (decSellActual_ex - decCost_ex) * CDec(sQty)
                            '-- GET serial audit id for serial status update...
                            sAudit_id = .Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value
                            '- check serial entered if needed..
                            sSerialNo = .Cells(k_GRIDCOL_SERIALNO).Value
                            sSql = "INSERT INTO dbo." & sLineTable & " ("
                            If Not mbIsQuote Then
                                If mbIsLayby Then
                                    sSql &= " layby_id, stock_id,  SerialNumber, SerialAudit_id, "
                                Else '-sale-
                                    sSql &= " invoice_id, stock_id,  SerialNumber, SerialAudit_id, "
                                End If
                                sValues = "VALUES ( " & CStr(intInvoice_id) & ", " & sStock_id & ", " & _
                                           "'" & gsFixSqlStr(sSerialNo) & "', " & sAudit_id & ", "
                            Else  '-quote-
                                sSql &= " salesOrder_id, stock_id, "
                                sValues = "VALUES ( " & CStr(intInvoice_id) & ", " & sStock_id & ", "
                            End If
                            sSql &= " description, cost_ex, cost_inc, "
                            sSql &= " sell_ex, sales_taxCode, sales_taxPercentage, "
                            sSql &= " sell_inc, sellActual_ex, sellActual_Tax, sellActual_inc, "
                            sSql &= " quantity, total_ex, total_tax, total_inc "
                            If Not mbIsQuote Then
                                sSql &= ", gross_profit  "
                            End If
                            sSql &= ") "
                            sValues &= "'" & gsFixSqlStr(.Cells(k_GRIDCOL_DESCRIPTION).Value) & "', " & _
                                  Replace(.Cells(k_GRIDCOL_COST_EX).Value, ",", "") & ", " & _
                                   Replace(.Cells(k_GRIDCOL_COST_INC).Value, ",", "") & ", " & _
                                   Replace(.Cells(k_GRIDCOL_SELL_EX).Value, ",", "") & ", " & _
                                   "'" & gsFixSqlStr(.Cells(k_GRIDCOL_TAXCODE).Value) & "', " & CStr(mDecGST_rate) & ", " & _
                                   Replace(.Cells(k_GRIDCOL_SELL_INC).Value, ",", "") & ", " & _
                                   Replace(.Cells(k_GRIDCOL_SELLACTUAL_EX).Value, ",", "") & ", " & _
                                   Replace(.Cells(k_GRIDCOL_SELLACTUAL_TAX).Value, ",", "") & ", " & _
                                   Replace(.Cells(k_GRIDCOL_SELLACTUAL_INC).Value, ",", "") & ", " & _
                                   sQty & ", " & Replace(.Cells(k_GRIDCOL_SELLACTUAL_EX_EXTENDED).Value, ",", "") & ", " & _
                                     Replace(.Cells(k_GRIDCOL_SELLACTUAL_TAX_EXTENDED).Value, ",", "") & ", " & _
                                       Replace(.Cells(k_GRIDCOL_SELLACTUAL_INC_EXTENDED).Value, ",", "")
                            If Not mbIsQuote Then
                                sValues &= ", " & CStr(decGrossProfit)
                            End If
                            sValues &= "); "
                            '-- insert this row..-
                            If Not mbExecuteSql(mCnnSql, sSql & sValues, True, sqlTransaction1) Then
                                mLabSaleHelp2.Text = "Insert Failed.."
                                mbCommitLocked = False
                                Exit Sub
                            End If  '--exec invoice LINE-
                            '=3403.430=  Layby doesn't actuall leave stock..-
                            If (Not mbIsQuote) And (Not mbIsLayby) Then  '==  !!!  AND NOT nonStock Item !!!!  --
                                '==  !!!  AND NOT nonStock Item !!!!  --

                                '-- get invoice Line_id--
                                '-get ID of last line inserted.. (For Serial-Audit-Trail).
                                '= sSql = "SELECT CAST(SCOPE_IDENTITY ('dbo.InvoiceLine') AS int);"
                                sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
                                If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                                    intInvoiceLine_id = intID  '-- this goes into serialAuditTrail below..-
                                Else
                                    MsgBox("Failed to read latest Invoice LINE-ID No..", MsgBoxStyle.Exclamation)
                                    mbCommitLocked = False
                                    Exit Sub
                                End If
                                '--  update stock on hand if not service..-
                                If (.Cells(k_GRIDCOL_ISSERVICEITEM).Value = "0") Then  '--not service.-
                                    sSql = "UPDATE dbo.stock SET "
                                    sSql &= " qtyInStock=(qtyInStock " & IIf(bIsCredit, "+", "-") & sQty & ")"
                                    sSql &= "  WHERE (stock_id = " & sStock_id & " );"
                                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                                        mLabSaleHelp2.Text = "Update Stock Failed.."
                                        mbCommitLocked = False
                                        Exit Sub
                                    End If  '--exec Update stock qty-
                                End If
                                '--   IF SERIALized ->  UPDATE SerialAudit status -> SOLD. -
                                '--            And ->  INSERT SerialAuditTrail record.. (SALE/REFUND (INSTOCK)).-
                                If (.Cells(k_GRIDCOL_TRACK_SERIAL).Value = "1") Then  '--track serial.-
                                    '-- GET serial audit id for serial status update...
                                    '= sAudit_id = .Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value
                                    If IsNumeric(sAudit_id) AndAlso (CInt(sAudit_id) > 0) Then
                                        sSql = "UPDATE dbo.serialAudit  "
                                        sSql &= IIf(bIsCredit, "SET status='INSTOCK', isInStock=1", "SET status='SOLD', isInStock=0")
                                        sSql &= "  WHERE (serial_id=" & sAudit_id & ");"
                                        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                                            mLabSaleHelp2.Text = "Update SerialAudit Failed.."
                                            mbCommitLocked = False
                                            Exit Sub
                                        End If  '--exec Update serial status-
                                        '--ok.. insert SerialAuditTrail record.-
                                        '- This is the "transaction" trail for this serial.-
                                        sSql = "INSERT INTO dbo.SerialAuditTrail ("
                                        sSql &= " stock_id, SerialAudit_id, "
                                        sSql &= "  tran_type, type_id, type_line_id, movement"
                                        sSql &= ") "
                                        sSql &= "VALUES ( "
                                        sSql &= sStock_id & ", " & sAudit_id & ", "
                                        sSql &= "'" & msTransactionType & "', "
                                        sSql &= CStr(intInvoice_id) & ", " & CStr(intInvoiceLine_id) & ", 1"
                                        sSql &= "); "
                                        '-- insert this TRAIL rec..-
                                        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                                            mLabSaleHelp2.Text = "Saving Serial Audit TRAIL record FAILED.."
                                            mbCommitLocked = False
                                            Exit Sub
                                        End If  '--exec serial-
                                    ElseIf (CInt(sAudit_id) <> -2) Then  '-no Audit id- (-2 means ok. was bypassed by user.)
                                        MsgBox("Error- 'Missing SerialAudit Id for Invoice Line." & _
                                                "Audit-id is " & sAudit_id & vbCrLf & _
                                                "Stock Barcode is " & sBarcode & vbCrLf & _
                                                "SerialNo is: " & sSerialNo & vbCrLf & vbCrLf & _
                                                "SerialAudit Trail was not written..  COMMIT is aborted..", MsgBoxStyle.Exclamation)
                                        sqlTransaction1.Rollback()
                                        mbCommitLocked = False
                                        Exit Sub
                                    End If '-audit_id valid-
                                End If  '--track serial-
                            End If '-not quote-
                        End With  '-row1-
                    End If '--not empty-
                Next row1 '-dgvSaleItems.Rows-
            End If '--count-

            '==3301.529=
            sSql = ""
            Dim sFieldList As String = ""
            '- INSERT Additional "invoice" table row for accountPayment (if any)-
            '--   OR CreditNote (if any)..
            If bIsOnAccount And (mDecPaymentTotalRcvd > 0) Then
                '== 3301.623=  
                '-- NO  MORE IP records..
                ''-- end of account payment invoice-

            ElseIf (Not mbSaleIsAccountCust) AndAlso _
                       ((mbIsRefund And mOptRefundCredit.Checked) Or ((Not mbIsRefund) And (mDecBalance < 0))) Or _
                                                            ((Not mbIsRefund) And (mDecCreditNoteWdlAmount > 0)) Then
                '== 3301.623=  
                '-- NO  MORE IP records..
                '-- CreditNote Amounts are recorded in Payment Record..-
            End If  '--extra invoice record.

            '-- P a y m e n t s --
            '-- Payments somehow have to balance-with/refer-to the invoices..
            '==  3301.1211-11Dec2016=  ADD CashDrawer and CurrentUserName to INVOICES and Payments=
            '==  3301.1211-11Dec2016=  ADD CashDrawer and CurrentUserName to INVOICES and Payments=

            '==
            '==    3401.327=  Change and  Cashout are DROPPED !!
            '==       All payment amounts to be represented by Detail Records.
            '==       Payment Detail record is now created for Standard Types
            '==                   (ie Cash (in), cheque, EftPos etc- 
            '==       PLUS:  cashRefund (like cash), account (debit amt), CreditNoteDr, CreditNoteCr..
            '==

            '==3301.623==
            '--    First write out "empty" payment Record for Account Sale with + Amount debited..-
            '= 3403.1109= NO MORE empty Account payment Recordss..

            'If bIsOnAccount Then '= And (mDecPaymentTotalRcvd < mDecInvoiceTotal) Then
            '    '-- Not fully paid here, so Invoice will be for Full Amount.
            '    '-- Dummy payment record is made to match with it..
            '    '-- No details or disb. as it is a null payment..
            '    mLabSaleHelp2.Text = "Invoice #" & intInvoice_id & ":  Saving Payment record.."
            '    sSql = "INSERT INTO dbo.payments ("
            '    sSql &= "  staff_id, customer_id, invoice_id, "
            '    sSql &= "  transactionType, amountDebitedToAccount,"
            '    sSql &= "    terminal_id,  cashDrawer, currentWindowsUserName, comments "
            '    sSql &= ") "
            '    sSql &= "VALUES ( "
            '    sSql &= CStr(mIntSaleStaff_id) & ", " & CStr(mIntSaleCustomer_id) & ", " & CStr(intInvoice_id) & ", "
            '    sSql &= "'" & msTransactionType & "', " & CStr(mDecInvoiceTotal) & ", "
            '    '==sSql &= IIf(bIsOnAccount, CStr(mDecBalance), "0") & ", "
            '    sSql &= "'" & msComputerName & "', "
            '    sSql &= "'" & gsGetCurrentCashDrawer() & "', '" & msCurrentUserName & "', "
            '    sSql &= "'Account SALE: " & gsFixSqlStr(msSaleComments) & "'"
            '    sSql &= "); "
            '    '-- Save-
            '    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
            '        mLabSaleHelp2.Text = "Saving Payment Record FAILED.."
            '        Exit Sub
            '    End If  '--exec invoice-
            'End If

            '-- payment-'=3411.0402- 

            If (Not mbIsQuote) AndAlso (Not (bIsOnAccount And (mDecPaymentTotalRcvd <= 0))) Then  '-=3103.0403=
                '- 5. INSERT SALE Payment Record.. -
                '--   3301.623-  NOT for Account Sale with no accomp.payment-

                '==   3301.698- No More Main Payment record..  Just (detail) Payments as per RM..
                '==3301.615- == NO!  We NEED Payment record to hold details together for Pay Event. ==

                Dim sPaymentTran1 As String = msTransactionType '-- not onAccount-
                If bIsOnAccount Then
                    sPaymentTran1 = "Account"   '--Account Sale came with partial Payment.-
                End If
                mLabSaleHelp2.Text = "Invoice #" & intInvoice_id & ":  Saving Payment record.."
                sSql = "INSERT INTO dbo.payments ("
                sSql &= "  staff_id, customer_id, invoice_id, "
                '== sSql &= "  transactionType, totalAmountReceived, changeGiven, cashoutGiven, nettAmountCredited,"
                sSql &= "  transactionType, totalAmountReceived, nettAmountCredited,"
                If mbIsRefund Then
                    sSql &= "  refundCashAmount, refundAsCreditNoteCredited, refundAsEftPosDr, refundAsEftPosCr, "


                    '==  Target is new Build 4251..
                    '==  Target is new Build 4251..
                    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
                    sSql &= "  RefundOtherDetailAmount,  RefundOtherDetailKey, "
                    '== END OF Target is new Build 4251..
 

                Else  '-sale-
                    sSql &= "  changeGiven, creditNotePaymentCredited, "
                End If
                sSql &= "  creditNoteAmountDebited, "
                sSql &= "  terminal_id, cashDrawer, currentWindowsUserName,  comments "
                sSql &= ") "
                sSql &= "VALUES ( "
                sSql &= CStr(mIntSaleStaff_id) & ", " & CStr(mIntSaleCustomer_id) & ", " & CStr(intInvoice_id) & ", "
                sSql &= "'" & sPaymentTran1 & "', "
                sSql &= CStr(mDecPaymentTotalRcvd) & ", "
                '== sSql &= CStr(mDecChange) & ", " & CStr(mDecCashout) & ", " & CStr(mDecPaymentNettCredited) & ", "
                sSql &= CStr(mDecPaymentNettCredited) & ", "
                If mbIsRefund Then
                    sSql &= CStr(decRefundedAsCash) & ", " & CStr(decRefundedAsCreditNote) & ", "
                    sSql &= CStr(decRefundedAsEftPosDr) & ", " & CStr(decRefundedAsEftPosCr) & ", "


                    '==  Target is new Build 4251..
                    '==  Target is new Build 4251..
                    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
                    sSql &= CStr(decRefundedAsOther) & ", '" & strRefundOtherDetailkey & "', "
                    '== END of  Target is new Build 4251..

  
                Else  '-sale-
                    sSql &= CStr(mDecChange) & ", " & CStr(mDecChangeAsCreditNote) & ", "
                End If
                sSql &= CStr(mDecCreditNoteWdlAmount) & ", "
                sSql &= "'" & msComputerName & "', "
                sSql &= "'" & gsGetCurrentCashDrawer() & "', '" & msCurrentUserName & "', "
                sSql &= "'PAYMENT: " & gsFixSqlStr(msSaleComments) & "'"
                sSql &= "); "
                '-- Save-
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    mLabSaleHelp2.Text = "Saving Payment Record FAILED.."
                    mbCommitLocked = False
                    Exit Sub
                End If  '--exec invoice-

                '- 6. Retrieve Payment No. (IDENTITY of Payment record written.)-
                '= sSql = "SELECT CAST(SCOPE_IDENTITY ('dbo.payments') AS int);"
                sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
                If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                    intPayment_Id = intID
                    '-- update invoice display later..-
                Else
                    MsgBox("Failed to retrieve latest Payment No..", MsgBoxStyle.Exclamation)
                    mbCommitLocked = False
                    Exit Sub
                End If
                mLabSaleHelp2.Text = "Saving Payment details.."
                DoEvents()

                '- 7. FOR EACH Payment Detail: INSERT Child Payment-Detail Line.-
                '- - --  For each type of payment received..
                '-- BUT first, write a negative cash detail line for Cashout if any..
                '--    (this is to balance the Cashup Report.)- 
                '= But NO! PaymentsDetails are only for cashin (credits to cust.)..
                'If (LCase(msTransactionType) = "sale") AndAlso (mDecCashout > 0) Then
                '    '-- save detail line-
                '    sKey = "cash"
                '    sDescription = "Cashout"
                '    sSql = "INSERT INTO dbo.paymentDetails ("
                '    sSql &= "  payment_id, paymentType_key, paymentType_descr, "
                '    sSql &= "  amount, terminal_id )"
                '    sSql &= "  VALUES (" & CStr(intPayment_Id) & ", " & "'" & gsFixSqlStr(sKey) & "', "
                '    sSql &= "'" & gsFixSqlStr(sDescription) & "', " & CStr(-mDecCashout) & ", '" & msComputerName & "'"
                '    sSql &= "); "
                '    '-- insert this row..-
                '    '= NO! PaymentsDetails are only for cashin (credits to cust.)..
                '    '= If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                '    '==  mLabSaleHelp2.Text = "Insert CASHOUT Paymnt-detail Failed.."
                '    '==  Exit Sub
                '    '= End If  '--exec pay detail LINE-
                'End If '--cashout.-

                '-- now details for all actual payment amounts received from cust.

                '- 7X. 3301.608== 
                '-- FOR EACH Payment TYPE: INSERT Payment DETAIL Record.-
                '- - --  For each type of payment received..
                '--   make collections of stuff from Payments Grid..
                '--   Add to collection for other stuff (account/credit note)-
                '--   then INSERT all lines from the collection..

                '==  NB= 3301.623=
                '--  These details are ONLY for Payments Actually INCOMING-
                '==    ie Cash, ETPOS, Credit-card, Amex etc. 
                '--     and NOT for account, credit note etc..
                '==
                '= Dim colPayDetails As New Collection
                Dim colDetail1 As Collection
                Dim intPaymentInvoice_id As Integer = intInvoice_id
                Dim sComments As String = ""
                If (LCase(msTransactionType) = "sale") Or (LCase(msTransactionType) = "layby") Then
                    If (mDgvSalePaymentDetails.Rows.Count > 0) Then
                        If (bIsOnAccount And (mDecPaymentTotalRcvd > 0)) AndAlso (intAccountPaymentInvoice_id > 0) Then
                            '-must point to accountPayment record..
                            intPaymentInvoice_id = intAccountPaymentInvoice_id
                        End If  '--on account-
                        For Each row1 In mDgvSalePaymentDetails.Rows
                            sComments = ""
                            sPayAmount = row1.Cells(k_PAYGRIDCOL_AMOUNT).Value
                            sKey = Trim(row1.Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value)
                            sDescription = row1.Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value
                            If IsNumeric(sPayAmount) AndAlso (CDec(sPayAmount) > 0) Then
                                '-- NO NO NO !!  If Cash line, then deduct change, if any..
                                '--  Detail lines must show nett receipts.
                                If (InStr(LCase(sKey), "cash") > 0) Then
                                    '== '--cash paid line.-
                                    sComments &= "Cash tendered: " & FormatCurrency(CDec(sPayAmount), 2)
                                    If (mDecChange > 0) Then
                                        '== 3301.630- NO !!  
                                        '-- Must go in as amt tendered. sPayAmount = CStr(CDec(sPayAmount) - mDecChange)
                                    End If
                                    sComments &= " Change: " & FormatCurrency(mDecChange, 2) & "."
                                End If '-cash-
                                '-- add detail to collection-
                                colDetail1 = New Collection
                                colDetail1.Add(intPaymentInvoice_id, "invoice_id")
                                colDetail1.Add(sPayAmount, "payamount")
                                colDetail1.Add(sKey, "key")
                                colDetail1.Add(sDescription, "description")
                                colDetail1.Add(sComments, "comments")
                                '-- save detail line-
                                colPayDetails.Add(colDetail1)
                                '= sSql = "INSERT INTO dbo.paymentDetails ("
                                '= sSql &= "  payment_id, paymentType_key, paymentType_descr, "
                                '= sSql &= "  amount, terminal_id )"
                                '= sSql &= "  VALUES (" & CStr(intPayment_Id) & ", " & "'" & gsFixSqlStr(sKey) & "', "
                                '= sSql &= "'" & gsFixSqlStr(sDescription) & "', " & sPayAmount & ", '" & msComputerName & "'"
                                '= sSql &= "); "
                                '= '-- insert this row..-
                                '= If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                                '=     mLabSaleHelp2.Text = "Insert Paymnt-detail Failed.."
                                '=     Exit Sub
                                '= End If  '--exec pay detail LINE-
                            End If  '-numeric-
                        Next row1
                    End If  '-row count-
                    '-- Still SALE--
                    '-- if ON-ACCOUNT-  details for "account" amounts debited to cust.
                    '--  balance to debit after any part payment..
                    'If (bIsOnAccount) And (mDecBalance > 0) Then
                    '    colDetail1 = New Collection
                    '    colDetail1.Add(intPaymentInvoice_id, "invoice_id")
                    '    colDetail1.Add(CStr(mDecBalance), "payamount")
                    '    colDetail1.Add("account", "key")
                    '    colDetail1.Add("Charged to Account", "description")
                    '    colDetail1.Add("Charged to Account: " & FormatCurrency(mDecBalance, 2) & "..", "comments")
                    '    '-- save detail line-
                    '    colPayDetails.Add(colDetail1)
                    'End If
                ElseIf (mbIsRefund And bIsOnAccount) Then
                    '--  ON-ACCOUNT-  (-ve) "account" detail line for refund amt amount..
                    'colDetail1 = New Collection
                    'colDetail1.Add(intPaymentInvoice_id, "invoice_id")
                    'colDetail1.Add(CStr(-mDecBalance), "payamount")
                    'colDetail1.Add("account", "key")
                    'colDetail1.Add("Credited to Account", "description")
                    'colDetail1.Add(sComments, "comments")
                    ''-- save detail line-
                    'colPayDetails.Add(colDetail1)
                ElseIf (mbIsRefund And (Not bIsOnAccount)) AndAlso (decRefundedAsCash > 0) Then
                    'colDetail1 = New Collection
                    'colDetail1.Add(intPaymentInvoice_id, "invoice_id")
                    'colDetail1.Add(CStr(-decRefundedAsCash), "payamount")
                    'colDetail1.Add("cash", "key")
                    'colDetail1.Add("Refunded as Cash.", "description")
                    'colDetail1.Add("Refunded as Cash: " & FormatCurrency(decRefundedAsCash, 2) & "..", "comments")
                    ''-- save detail line-
                    'colPayDetails.Add(colDetail1)
                ElseIf (decCreditNote > 0) Then
                    ''--  and then-  NOT ON-ACCOUNT-  (-ve) detail line for "creditnote" amount if any..
                    'sDescription = "Refunded to Credit Note."
                    'If Not mbIsRefund Then
                    '    sDescription = "Payment rcvd saved as Credit Note."
                    'End If
                    'colDetail1 = New Collection
                    'colDetail1.Add(intPaymentInvoice_id, "invoice_id")
                    'colDetail1.Add(CStr(decCreditNote), "payamount")
                    'colDetail1.Add("creditnote", "key")
                    'colDetail1.Add(sDescription, "description")
                    'colDetail1.Add(sComments, "comments")
                    ''-- save detail line-
                    'colPayDetails.Add(colDetail1)
                End If  '--sale/row count.-

                '- 7X. 3301.608== 
                '-- BUT ALSO first, write a negative cash detail line for Cashout if any..
                '--    (this is to balance the Cashup Report.)- 
                'If (LCase(msTransactionType) = "sale") AndAlso (mDecCashout > 0) Then
                '    '    sKey = "cash"
                '    '    sDescription = "Cashout"
                '    colDetail1 = New Collection
                '    colDetail1.Add(intPaymentInvoice_id, "invoice_id")
                '    colDetail1.Add(CStr(-mDecCashout), "payamount")
                '    colDetail1.Add("cash", "key")
                '    colDetail1.Add("Cashout", "description")
                '    colDetail1.Add("Cashout-", "comments")
                '    '-- save detail line-
                '    colPayDetails.Add(colDetail1)
                'End If '-cashout-

                '-- NOW..  Write out all Pay details.. as a Batch)-
                sSql = ""
                If (colPayDetails.Count > 0) Then
                    For Each colDetail1 In colPayDetails
                        sSql &= "INSERT INTO dbo.paymentdetails (payment_id, "
                        sSql &= " paymentType_key, paymentType_descr, "
                        sSql &= "  amount, comments )"
                        sSql &= "  VALUES (" & CStr(intPayment_Id) & ", "
                        sSql &= "'" & gsFixSqlStr(colDetail1.Item("key")) & "', "
                        sSql &= "'" & gsFixSqlStr(colDetail1.Item("description")) & "', "
                        sSql &= colDetail1.Item("payamount") & ", "
                        sSql &= "'" & gsFixSqlStr(colDetail1.Item("comments")) & "' "
                        sSql &= "); "
                    Next colDetail1
                    '--exec-
                    '-- insert all rows..-
                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                        mLabSaleHelp2.Text = "Insert Paymnt-details Failed.."
                        mbCommitLocked = False
                        Exit Sub
                    End If  '--exec pay detail LINE-
                End If  '--count-

                '- 8. INSERT Payment-Disbursement Child Table Row for THIS SALE Invoice.-
                '--  THIS invoice receives ALL of the nett payment..
                '==4201.1031-
                '--   Commit Sale- Payment Disbursement Line Amount MUST include Credit-Note amt used (wdl).
                '-- And add TRANCODE to field value list.
                '-- save detail line-
                If Not mbIsLayby Then
                    sSql = "INSERT INTO dbo.paymentDisbursements ("
                    sSql &= "  payment_id, invoice_id, tranCode, "
                    sSql &= "  amount )"
                    sSql &= "  VALUES (" & CStr(intPayment_Id) & ", " & CStr(intInvoice_id) & ", "
                    sSql &= " '" & msTransactionType & "', "
                    '= sSql &= CStr(mDecPaymentNettCredited)
                    sSql &= CStr(mDecPaymentNettCredited + mDecCreditNoteWdlAmount)
                    sSql &= "); "
                    '-- insert this row..-
                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                        mLabSaleHelp2.Text = "Insert Pay-disbursement Failed.."
                        mbCommitLocked = False
                        Exit Sub
                    End If  '--exec pay disb. LINE-
                End If  '-layby-
            End If '-not quote- or bIsOnAccount-
            '----------------------------------

            mLabSaleHelp2.Text = "Committing SQL Transaction.."

            '-- JOB OR LAYBY Delivery--
            '=3103.117 = Job Delivery=
            If (mIntJob_id > 0) Then
                mLabSaleHelp2.Text = "Updating Job.."
                sSql = "UPDATE [jobs] SET jobStatus='" & sJobStatus & "'"
                sSql &= ", DeliveredRMStaff_Id=" & CStr(mIntSaleStaff_id) & ", DeliveredStaffName='" & msSaleStaffName & "'"
                sSql &= ", dateDelivered= CURRENT_TIMESTAMP "
                sSql = sSql & ", dateUpdated= CURRENT_TIMESTAMP "
                sSql = sSql & " WHERE (job_id=" & CStr(mIntJob_id) & ") "
                '-- execute Job update-
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    mLabSaleHelp2.Text = "Job update- Failed.."
                    '= Exit Sub
                End If  '--execJob update--
            ElseIf mbIsDeliveringLayby Then
                '-- Update Layby to Mark as Complted/Delivered-
                '-  isDelivered bit NOT NULL DEFAULT 0,"   '= completed and invoiced.-
                '-  Layby_date_delivered datetime  NULL,"
                '-   Layby_delivered_invoice_id INT NOT NULL DEFAULT -1,"
                mLabSaleHelp2.Text = "Updating LAYBY.."
                sSql = "UPDATE [layby] SET isDelivered=1 "
                sSql &= ", Layby_delivered_invoice_id=" & CStr(intInvoice_id)
                sSql &= ", Layby_date_delivered= CURRENT_TIMESTAMP "
                sSql = sSql & " WHERE (layby_id=" & CStr(mIntDeliveredLayby_id) & ") "
                '-- execute Layby update-
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    mLabSaleHelp2.Text = "LAYBY update- Failed.."
                    '= Exit Sub
                End If  '--execlayby update--
            End If  '-Job/Layby-
        Catch ex As Exception
            sqlTransaction1.Rollback()
            MsgBox("ERROR in main Sale-Commit routine-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            mbCommitLocked = False
            Exit Sub
        End Try '--Main try-

        mLabSaleHelp2.Text = "Committing SQL Transaction.."

        '- 9.  Commit TRANSACTION.-
        Try
            sqlTransaction1.Commit()
            '= MsgBox(mLabSaleTranType.Text & " Transaction committed ok.." & vbCrLf & _
            '=         sMainTable & " No: " & intInvoice_id, MsgBoxStyle.Information)
        Catch ex As Exception
            Try
                sqlTransaction1.Rollback()
                MsgBox("Transaction commit FAILED.. rollback completed.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                '=MsgBox("Transaction rolback completed.. " & vbCrLf & ex.Message, MsgBoxStyle.Information)
            Catch ex2 As Exception
                MsgBox("Transaction commit FAILED.. and Rollback FAILED.. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
            mbCommitLocked = False
            Exit Sub
        End Try  '-commit-
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        mLabSaleHelp2.Text = ""
        '=3411.0402=  Open cash Drawer..
        If (colPayDetails.Count > 0) Then
            Dim sTypeKey As String
            Dim bCanOpenTill As Boolean = False
            For Each colDetail1 As Collection In colPayDetails
                sTypeKey = LCase(colDetail1.Item("key"))
                If (InStr(sTypeKey, "cash") > 0) Or _
                           (InStr(sTypeKey, "cheque") > 0) Then
                    bCanOpenTill = True
                End If
            Next colDetail1
            If bCanOpenTill Then
                Call OpenCashDrawer()   '--see public above..
            End If
        End If  '-count-
        '- done Till.

        '--  Print invoice..-
        Call mbShowInvoice(intInvoice_id, msTransactionType, _
                            bCanEmail, bCanPrint, bReallyWantsA4Invoice, _
                                 sSelectedInvoicePrinterName, sSelectedReceiptPrinterName)  '--AND capture pdf for email..

        '3301.627= - For Account Sale with Part Payment,
        '--  print Payment Receipt..
        If (bIsOnAccount And (mDecPaymentTotalRcvd > 0) And (intPayment_Id > 0)) Then
            '-- have part payment.-
            Dim frmShowPayment1 As New frmShowPayment
            frmShowPayment1.connectionSql = mCnnSql
            frmShowPayment1.PaymentNo = intPayment_Id
            frmShowPayment1.UserLogo = mImageUserLogo
            frmShowPayment1.versionPOS = msDLLVersion
            frmShowPayment1.Staff_id = mIntSaleStaff_id   '=3411.1118=
            frmShowPayment1.CaptureReceiptPDF = bCanEmail
            frmShowPayment1.CanPrintReceipt = True  '= 3519.0324= bCanPrint
            frmShowPayment1.ShowDialog()
        End If '-- have part payment.-

        mTxtSaleItemBarcode.Text = ""  '=33017.0219=
        Call mbClearEditLine()  '==3301.1112--

        '-- clear grid.-
        Call mbClearInvoice()
        mDgvSaleItems.Enabled = False
        '== mDgvSaleInvoices.Rows.Clear()
        mListViewSaleAvailCredit.Items.Clear()


        '=DEFAULTS to SALE= mOptSaleSale.Checked = False
        mLabSaleTranType.Enabled = False
        mLabSaleTranType.Text = "Sale"

        '= mOptSaleRefund.Checked = False
        '= mOptSaleQuote.Checked = False
        '= mCboTransaction.SelectedIndex = 0  '= Item = "Sale"
        mPanelSaleInvoiceList.Enabled = False

        '-  RE-fresh  list of invoices for this cust..--
        '= NO= Call mbLoadInvoiceList(mIntSaleCustomer_id)
        '== mDgvSaleInvoices.Rows.Clear()

        '-- Force new Customer Choice.-

        mPanelSaleFooter.Enabled = False

        msSaleLastCustomerBarcode = msSaleCustomerBarcode
        '=3401.312=
        mTxtSaleCustBarcode.Text = ""
        msSaleCustomerBarcode = ""

        '= mTxtSaleCustBarcode.Enabled = True
        mTxtSaleCustName.Text = ""
        mLabSaleCustTags.Text = ""
        mToolTip1.SetToolTip(mLabSaleCustTags, "")

        mIntSaleCustomer_id = -1
        mIntJob_id = -1
        mIntDeliveredLayby_id = -1

        mPanelOptTranType.Enabled = False  '= True
        '=DEFAULTS to SALE= mLabChooseTrans.Visible = False  '= True

        '= mPanelSaleHdr.Enabled = False
        mPanelSaleFooter.Enabled = False

        mbStaffTimeoutSuspended = False   '--can timeout now..-

        mLabSaleCust.Enabled = True
        '= mTxtSaleCustBarcode.Select()
        '= mBtnSaleHold.Enabled = False

        mTxtSaleStaffBarcode.Text = ""
        mTxtSaleStaffBarcode.Enabled = True

        '=3519.0118=
        '-- clear account switch..
        mOptSaleSale.Checked = False
        mOptSaleRefund.Checked = False
        mOptSaleLayby.Checked = False
        mOptSaleQuote.Checked = False

        mChkOnAccount.Enabled = False
        mChkOnAccount2.Enabled = False

        mChkOnAccount.Checked = False
        mChkOnAccount2.Checked = False


        mbCommitLocked = False

        mTxtSaleStaffBarcode.Select()

    End Sub  '--commit-
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Cancel invoice---

    Public Sub btnCancel_Click(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs)
        '=  Handles btnCancelSale.Click AND btnCancelSale2.Click

        If (mDgvSaleItems.Rows.Count > 0) And (mDecInvoiceTotal > 0) Then
            If MsgBox("Discard current " & mLabSaleTranType.Text & " data ?", _
                        MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If  '--count-

        '-Cancel-

        mbIsCancelling = True
        mDgvSaleItems.CancelEdit()
        '- clear all error texts..
        For Each row1 As DataGridViewRow In mDgvSaleItems.Rows
            row1.ErrorText = ""
        Next row1
        mPanelSaleLineEntry.Enabled = False
        mTxtSaleItemBarcode.Text = ""  '=33017.0219=

        Call mbClearEditLine()  '==3301.1112--
        Call mbClearInvoice()
        mDgvSaleItems.Enabled = False
        '== mDgvSaleInvoices.Rows.Clear()
        '=DEFAULTS to SALE= mOptSaleSale.Checked = False
        '= mOptSaleRefund.Checked = False
        '= mOptSaleQuote.Checked = False
        '== mCboTransaction.SelectedIndex = 0  '= Item = "Sale"
        '=DEFAULT= 
        mLabSaleTranType.Enabled = False
        mLabSaleTranType.Text = "Sale"

        mPanelSaleInvoiceList.Enabled = False

        '-- Force new Transaction Choice.-
        mPanelOptTranType.Enabled = False  '= True
        '=DEFAULTS to SALE= mLabChooseTrans.Visible = False  '= True

        '= mPanelSaleHdr.Enabled = False
        mPanelSaleFooter.Enabled = False

        msSaleLastCustomerBarcode = msSaleCustomerBarcode
        mTxtSaleCustBarcode.Text = ""
        msSaleCustomerBarcode = ""

        '= mTxtSaleCustBarcode.Enabled = True
        mTxtSaleCustName.Text = ""
        mLabSaleCustTags.Text = ""
        mToolTip1.SetToolTip(mLabSaleCustTags, "")

        mIntSaleCustomer_id = -1
        mIntJob_id = -1

        mbStaffTimeoutSuspended = False   '--can timeout now..-

        mLabSaleCust.Enabled = True

        '= mLabSaleAvailCredit.Text = ""
        mListViewSaleAvailCredit.Items.Clear()

        mTxtSaleStaffBarcode.Text = ""
        msSaleStaffbarcode = ""
        mTxtSaleStaffBarcode.Enabled = True
        '= mTxtSaleCustBarcode.Select()
        '= mBtnSaleHold.Enabled = False
        '=3519.0118=
        '-- clear account switch..
        mOptSaleSale.Checked = False
        mOptSaleRefund.Checked = False
        mOptSaleLayby.Checked = False
        mOptSaleQuote.Checked = False

        mChkOnAccount.Enabled = False
        mChkOnAccount2.Enabled = False

        mChkOnAccount.Checked = False
        mChkOnAccount2.Checked = False

        DoEvents()
        mTxtSaleStaffBarcode.Select()

    End Sub  '--cancel--
    '= = = = = = = = = = = = = =
    '-===FF->

    '- Admin -
    '- Admin -
    '- Admin -

    'Public Sub btnPOSAdmin_Click(ByVal sender As System.Object, _
    '                                ByVal e As System.EventArgs)  '== Handles btnPOSAdmin.Click
    '    Dim frmAdmin1 As frmPOS34Setup

    '    frmAdmin1 = New frmPOS34Setup(mFrmSale, msServer, mCnnSql, msSqlDbName, mColSqlDBInfo, _
    '                                    msDLLVersion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
    '    frmAdmin1.ShowDialog()

    'End Sub  '-  btnPOSAdmin_Click'-
    '= = = = = = =  = = = == = == = == 

End Class  '-clsPOS3Main--

'== the end ==
