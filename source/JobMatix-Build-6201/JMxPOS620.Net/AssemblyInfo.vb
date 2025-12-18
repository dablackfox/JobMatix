Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("JMxPOS62 POS Library")>
<Assembly: AssemblyDescription("JobMatix62 Advanced Point of Sale")>
<Assembly: AssemblyCompany("grh")>
<Assembly: AssemblyProduct("JMxPOS620")>
<Assembly: AssemblyCopyright("Copyright © grhaas@outlook.com 2014..2021")>
<Assembly: AssemblyTrademark("")> 

<Assembly: ComVisible(False)>

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("74a00bd3-a95c-471d-a3b0-21dc7a27132a")>

'==
'== JobMatixPOS dll- "3.1.3107.0813 - 13Aug2015-
'==  Add Purchase Orders Functionality to Admin..
'==
'== JobMatixPOS dll- "3.1.3107.0911 - 11Sep2015-
'==  BACKTRACK to .Net 3.5 --..
'==
'==  NEW VERSION
'==  NEW VERSION
'==  NEW VERSION
'==
'== JobMatixPOS dll- "3.2.3201.130 - 30Jan2016-
'==   >> To go with JobMatix 3.2---..
'==   >>  Start adding StockTake functionality..
'==
'== = =
'==     v3.3.3301.427..  27-Apr-2016= ===
'==        >> clsPOS31Main- Restore "OptSaleSale" RadioButton
'==              to set of Trans. options.  Still the Default..
'==       >>  Big cleanup of LocalSettings and SysInfo using classes..
'==
'==  = 3301.429= 29Apr2016- clsLocalSettings-  
'==      >> Drop "mSdSettings As clsStrDictionary"..
'==
'==  = 3301.506= 06May2016- frmWait-  
'==      >> Added frmWait for statements startup....
'==  = 3301.514= 14May2016- frmStocktake--  
'==      >> Fix case-sensitive compare when building Categories Tree.....
'==
'==     v3.3.3301.516..  16-May-2016= ===
'==          >> Redesign POS Sale Form to use Textboxes for Entry of current sale.
'==
'==
'==     v3.3.3301.525..  25-May-2016= ===
'==       >> Update Invoice Table Schema-  Invoices to reflect RM transactions (IV,IP, SA) types
'==         (For Account customers, part payment with sale records an accountPayment invoice entry)
'==       Everyone must re-create new DB..--
'==
'==     v3.3.3301.607..  07-June-2016= ===
'==       >> Update Stock Table Schema- (and fix frmImportRM.)
'==             Column "isNonStockItem" replaces   "isServiceItem", "isLabour".
'=                  (Imports "static_quantity".. )
'==       >> Update Payments Schemas- Now is Just one table (payments) as per RM.
'==       >> 3301.611/615- Update frmPayments Form-code to write AccountPayments records, 
'==               and new-type payments records..
'--               and re-organise Cashup Analysis..
'== = = = =
'==     v3.3.3301.622..  22-June-2016= ===
'==       >> Update Invoice and PAYMENTS Table Schema-  
'==     BACK to the ORIGINAL schema-  No IP Invoice records, and PaymentDisbursement records Re-stored.
'==         (For Account customers, part payment with sale records a separate Payment entry)
'==
'==     v3.3.3301.710..  10-July-2016= ===
'==       >> Update/fix Jobs Delivery- 
'==
'==     v3.3.3301.813/814/816/817..  17-August-2016= ===
'==       >> Fixes to Stocktake- 
'==                (autoscrolling, single stocktake, Zero items button moved).
'==       >> Fixes to frmStockLabels- add row-validating event.
'==                  (and "IDAutomationHC39M" as-default barcode font.-)
'==       >> Fixes to frmGoodsRecvd- make sure stockAdmin accessible for NEW stock items..
'==       >> frmShowInvoice- Add msgBox to advise Sent to printer...
'==
'==     v3.3.3301.1112..  12-Nov-2016= ===
'==       >> Fixes to Sale Commit.- 
'==                (Was not saving SerailAuditTrail trans. ).
'==       >> Add Licence Checking (cloning POS licence off Jobmatix..-
'==       >> 3301.1129. Updates to cashup_session table..
'==             -- Add Licence Checking (cloning POS licence off Jobmatix..-
'==             --  Started new Till Balance/ cashup processing (new form).
'==
'==     v3.3.3301.1212..  12-Dec-2016= ===
'==       >> Introducing CashDrawer ID's (as per MYOB RM.).- 
'==       >> Every W/s running POS must have a current CashDrawer ID. ("A".."Z".).- 
'==       >> Any given CashDrawer ID can be used by several W/s at a time..- 
'==       >> CashDrawerId/Computer (w/s) assignments are kept in SystemInfo Table-     
'==       >> CashDrawer ID of W/s ("A".."Z".) is recorded in every invoice and Payment Record.
'==                  as well as in CashUp Sessions.
'==       >> Till Balances and Cashup Sessions are on the basis of CashDrawer ID. (NOT terminal_id).
'==
'==     v3.3.3303.0102..  02-Jan-2017= ===
'==       >> Rewriting Sales/Invoices reports to use clsReportprinter..- 
'==  
'==     v3.3.3303.0109..  09-Jan-2017= ===
'==       >> Add functionality to Debtors Payments to
'==                   Disburse Refund values to Outstanding Invoices...- 
'==  
'==     v3.3.3303.0119..  19-Jan-2017= ===
'==       >> frmPOS31Admin- Add more columns to Staff Browse/Update..
'==       >> frmImport- (in POS333 Shell) Fix columns in Staff import (suburb/state)...
'==
'==     v3.3.3303.0121..  21-Jan-2017= ===
'==       >> frmStock- Tidying up..
'== 
'==  NEW Build..
'==
'==     v3.3.3307.0202/5/11..  02/08-Feb-2017= ===
'==       >> Debtor Payments.. Accept Sales InvoiceNo as lookup to find Customer..
'==       >> Returns- Add SupplierReturns Table,  
'==            and accept GoodsShipped info from RAs Application to update Stock and SerialAudit Info...
'==       >> Credit Notes.. 
'==              Add button/form to POS Admin to show all credit-note history...
'==       >> Sales Reports.. 
'==              Add Product Sales by Customer Report.. incl Filter for Particular Customer...
'== 
'==       >> clsPOS31Main.. 
'==              Fix change/(not credit note) error account Sale computing balanve....
'== 
'==       >> POS Reports-.. 
'==              Add Sales by Customer-  Remove Till Balance Stuff...
'==       >> 3307.0218 =
'==          -- Handle txtSaleItemBarcode VALIDATED Event for all Line Item textboxes..
'==
'==     v33.3.3307.0221/0223 =
'==      -- Added class ShapedPanel for rounded corners Panel..
'==      -- Allow empty Cust barcode to pass validation (but no transactions enabled)..
'==
'== 
'==  NEW POS Build..
'==
'==     v3.3.3311.0225..  25-Feb-2017= ===
'==       >> Report Printer class.  ADD sub-classed printPreviewDialog to catch Print Button 
'==                              and show print Dialog to choose printer...
'==       >> SALE Screen. Allow to TAB to Payments Footer withou using MOUSE !!! 
'==       >> SALE/Commit Confirmation.. Add printer combos, and pass selection onto ShowInvoice..
'=          -- Also When called from Commit, 
'==                 printing of Invoice or Receipt auto-proceeds without further user decision.
'==       >> 27Feb2017= SALE Screen. in clsPOS31Main- set dgvSaleItems.StandardTab -> TRUE !!! 
'== 
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==  NEW POS Build..  For Multiple Sale Instances..--
'==  NEW POS Build..  For Multiple Sale Instances..--
'== 
'==     v3.4.3401.0307 = 07Mar2017=
'==      -- New Build for POS 34.  Extended TAB controls inside JobMatix POS Tab...
'==      --  SALE Screen can NOW support 3 clsPOS31Main instances (For Holding Sales in progress)--
'==      --   Register Extra Transaction Holding controls in top panel..
'==      --   Staff must "sign-in" (separate, additional) for each Sale Transaction....
'==
'==     3401.0314- SALE Screen/code- CASHOUT totally removed.--
'==     3401.0318- Tidy up Delivering Jobs..-
'==
'--   Was Archived and tested at Precise..
'==
'==  v.3401.0321- Fixes and Changes-..-
'==     >>  Trans. selection. Replaced RadioButtons with Combo. (Sale/Refund/Quote). 
'==     >>  Show Invoice/Quote. Replaced RadioButtons with two cmd buttons.. 
'==     >> 3401.327- Drop Cashout columns from payment record..
'==     >> 3401.327- ADD EFTPOS_DR, EFTPOS_CR to Refund Options.
'==     >> 01Apr2017= Fixes to Line Entry.
'==    v.3401.0414/416/417- 14Apr2017=
'==       >> Fixes find correct (Client) ComputerName in case of THIN CLIENT.-..-
'==       >>  3401.417- Updates to clsSystemInfo
'= 
'==   Released to website..
'==
'= = = = = = = = = = = = =  ==  = = = = =
'==
'==  NEW POS Build..  For Adding on Layby's functionality...--
'==  NEW POS Build..  For Adding on Layby's functionality...--
'==  NEW POS Build..  For Adding on Layby's functionality...--
'== 
'==     v3.4.3403.0429 = 29Apr2017=
'==      -- modCreatePOS DB..  Add Tables- LayBy and LaybyLine
'==      --  3403.510- Layby Logic.. Plus- Add Event for new button.. Show/Print layby..
'==     3403.615- 15Jun2017-
'==      --  Goods Received..  Fixes to Tabbing.. 
'==               PLUS NEW Lookup Form to see past GR transactions.
'==     3403.711- 11July2017-
'==      -- Debtors Payments. Fix Commit crash-
'==              (was trying to insert "terminal_id" into payment details..) 
'==      -- Debtors Statements. Fix Execute-all crash-
'==       -- Check for Microsoft PDF printer also.-
'==
'==     3403.717- 17July2017-
'==      -- Product Sales Report- Show cost-ex, sell_ex columns..
'==      -- Sales Screen.  Fix errors with Hold, and changing Item barcode..
'==
'==     3403.728/730/731/0801- 28-30-July/01August-2017-
'==      -- Customer Admin Form..  fix barcode/customer_id mistake in grid row selection...
'==                     AND make form re-sizable.
'==      -- Main SALE Form..  Remove Comments/Delivery to separate form..  New button to call it.
'==                            Re-arrange Restore buttons to rhs sidebar..
'==      -- clsPOS34Sale-  Fix TAX code choice if setting up line. (was using GoodsTax code-)..
'==
'==     3403.0919- 15/16/17/18/19-Sep-2017-
'==      -- POS Setup/Options. Add Cust.Pricing Grade Rates...0..4.
'==                and delete obsolete labour Rate stuff.
'==      -- Customer details form-  
'==            Add dropdown for Cust.Pricing Grade Rates...0..4.
'==      -- POS Reports- Fix Invoice Report for NULL Invoice lines..
'==                         Add totals for Discount..
'==      -- clsPOS34Sale-  Add code to apply correct Cust pricing Grade...
'==      -- clsPOS34Sale-  Payments.. default Grid-payment amt to outst. balance...
'==
'==     3403.1014- 07/10/11/14/31-Oct-2017-
'==          3403.1101 01-Nov-2017=
'==      -- POS Emails now to use Server File-System to store Invoice PDF's for Email..
'==            at \\[server]\users\public\JobMatixPOS-EmailQueue\ 
'==               NB: (Table "DocArchive" to be DROPPED..)
'==      --  XML Descriptor file to go with each PDF for Email sending info. 
'==      --  POS Setup/Options. Update Emailing Defaults..
'==      --  POS Create db.. Add Emailing Defaults to initial SystemInfo..
'==      -- MAJOR SHIFT..
'==        >> All Customers can have credit Notes (Account and non-a/c custs).
'==        >>  For Sales to Account Cust-  only onAccount invoices will go to Debtors.
'==             --  So account cust can have normal Cash Sales
'==             --  'chkOnAccount' Checkbox (Charge to Account) added to sale Payments Panel. 
'==                    (User must check this to make on-account sale..).
'==        >>  On-Account sales can have partial Debtors payment with it..       
'==        >>  Refunds now the same for Account Custs and non-a/c custs..   
'==             -- So Account Payments can draw on CreditNotes for payment of invoices..  
'==             --    and Table "paymentRefundDetails" is dropped. 
'==        >> Account Payments can have discount given on invoices..  
'==             (add paymentDisbursement record "discount")
'==        >> Account - frmShowPayment upgraded to support eMailing Receipt...  
'==        >> Account Reversals- frmShowPayment upgraded to include Reversal Commit....  
'==        >> 3403.1115 --  Add more settings to POS Create DB..
'==     == THIS now upgraded to Build 3411---.
'==     == THIS now upgraded to Build 3411---.
'==        >> 3411.1117 -- Fixes to Print Sales Invoice for Empty Payments...
'==        >> 3411.1125 -- Add TABLES and FORMS for Subscriptions..
'==        >> 3411.1209 -- File clsBrowse32.vb renamed clsBrowse34.vb..
'==                -- actual class name renamed to "clsBrowse3"
'==        >> 3411.1219 -- Form frmPOS34Main (Sale form) moved into MxPOS340 dll assembly .
'==                -- Is now NON-Modal form called from Main Program
'==                -- Or can be compiled into POS349ex EXE to be launched from JobTracking.
'==        >> 3411.0103 -- 03-Jan-2018== .
'==                -- Integrating Email Agent Form. 
'==        >> 3411.0107 -- 07-Jan-2018== .
'==            -- For POS + JobTracking and Job Sale.. 
'==                   Add "Diagnosis" text to Sale Invoice as an add-on to Comments as "Job-Report"..  . 
'==            -- 3411.0109=  get Microsoft PDF Printer to print To File (for emailing PDF)..
'==                  AND-   Give priority to the Microsoft PDF Printer for the Emailing PDF's 
'==            -- 3411.0124=  Upgrade frmCustomer to allow being called just to Add NEW CUSTOMER...
'==                  AND-   Fix New-Customer commit to retry in case of failed UNIQUE Barcode. 
'==            -- 3411.0129=  Tidy up Subscriptions..
'==
'==--     (3411.0129 Was released to prcise..)
'==--     (3411.0129 Was released to prcise..)
'==--     (3411.0129 Was released to prcise..)
'==
'==      -- 3411.0201=  01-Feb-2018= Fixes to Stock REport...
'==             --     AND-   Fix to Print Grid to reduce width.. 
'==          -- Add Sort options to Stock Report-. 
'==          -- frmIMPORT from RM- UPDATE columns, and Add Layby Qty to in-stock.. 
'==          -- clsPOS34Sale.. Fix to beginEdit to NOT add tax if Tax COde is NOT GST (eg FRE)... 
'==      -- 3411.0208=  08-Feb-2018=
'==            --  Fixes to Sales Invoice (Address Window)..
'==            --  Sale Form- Add BUTTON to show Last Sales Invoice.
'==      -- 3411.0217=  17-Feb-2018=
'==              -- ReCompile to go with JobMatix 3411.0217..
'==
'==--     (3411.0217 Was released to Precise..)
'==--     (3411.0217 Was released to Precise..)
'==
'==      -- 3411.0225=  24/25-Feb-2018=
'==              -- Fixes to Subscriptions..  
'==                  Add Context menu to Edit Line Items (Price/Qty)...
'==                  Add Sub. EndDate (optional)
'==
'==--     (3411.0228 Was released to Precise..)
'==--     (3411.0228 Was released to Precise..)
'==
'==      -- 3411.0304=  04-Mar-2018=
'==              -- More Fixes to Subscriptions (Browsing)..
'==                   And fix crash (index 8) when editing.
'==
'==-- (3411.0305 Was released to Precise..)
'==-- (3411.0305 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - 
'==
'==     -- 3411.0307=  07-Mar-2018=
'==             --  Email Agent-   Automate Sending Emails.
'==                  ie Add Checkboxes to each row to select which to send..  Add Form button- "Send All Selected"
'==
'==    -- 3411.0313=  09/13-Mar-2018= Fixes for Lachlan- 
'==        (i)  Sales Form:  Use buttons to select Trans..  Sale has to be without mouse..
'==            -- and Drop Combo, drop "Continue" button..  
'==       (ii) Fix Selling out Job (1 item missing)..
'==       (iii)Sale-  Trap ESCape to Cancel Sale....
'==       (iv) Browser Form- Add Full Text Srch...
'==        (v) Account Sale with part payment- Fix "Can-Email".. 
'==
'==-- (3411.0314 Was released to Precise..)
'==-- (3411.0314 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - 
'==
'==   >> 3411.0324=  24/31-Mar-2018=
'==       -- New Options/Setup Form for Till (CashDrawer) Connections.
'==       -- Add clsPrintDirect tp print the CashDrawer codes via Win32 printing....
'==       -- 01-Apr-2018- Main POS Form.. Catch CTL-Z and Open CashDrawer for current Till..
'==       -- 05-Apr-2018- POS Sale- Fix Discount Tab sequence...
'==       -- 07-Apr-2018- POS Sale- Strip leading Zeroes on Item Barcode...
'==                  AND-   If barcode not found, Then retry with lead zeroes intact (if any).
'==       -- 08-Apr-2018- New Customer- Change of rules.  
'==                            Required at least one name, and one contact no. (or email)..
'==       -- 15-Apr-2018- POS Sale- Catch Mouse-Click on Item Barcode to set Selection stuff....
'==       -- 17-Apr-2018- modCreatePOSdb Fix Subscription line.. commas missing..
'==
'==-- (3411.0417 Was released to Precise..)
'==-- (3411.0417 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - 
'==
'==   >> 3411.0420=  20-April-2018=
'==    -- Re-interpreting Stock Columns for Purchase Orders.
'==             1. "reOrderLevel" (from RM "order_threshold") Means Minimum to hold in stock..
'==                 (Re-order when stock falls BELOW this level-)
'==             2. "order_quqntity" (from RM "order_quantity") Means MAXIMUm qty to hold in stock..
'==                 (Re-order sufficient to top up stock UP to this level-)
'==    -- Emailing Purchase Order...  with Commit Dialog.
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
'==   >> 3431.0621=  21-June-2018=
'==    -- Cleanup Email XML Files for reserved chars (eg ampersand,<, > )...
'==    -- frmPOS34Main..  Chenge Acivated Event to Shown event.....
'==
'==-- (3431.0623 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - 
'==
'==  NEW POS VERSION.. v3.5 For MDI functionality...--
'==          -- Multiple MDI Child Sales forms inside TAB Control..
'==   >> 3501.0715=  15-July-2018=
'==    -- Move F6 and CashDrawer stuff from Chile Sale form to Mdi Mother....
'==    -- Use Delegate to signal child closed to Main Parent.....
'==    -- Option to change Till now only avail on Startup...
'== 
'==   >> 3501.0725=  25-July-2018=
'==  --  (a) Sales-  for Non-Stock items..  do not warn of low stock, and do not update stock balance.
'==  --  (b) Shortcuts- Ctrl + O	Stock - Opens the Stock window
'==  --  	Ctrl + P	Suppliers - Opens the Supplier window
'==  -- 	Ctrl + U	Customers - Opens the Customer window
'==  --		Ctrl + F	Staff - Opens the staff window
'==  --  (c) Sales Window..  Quote trans. should not have, or stop on, Payment Box.
'== 
'==   >> 3501.0731=  31-July-2018=
'==      --  Fixes for Sales logic:  Bypass low stock warning for NonStock Items
'==               AND Quotes are not to ask for SerialNos..
'==               AND  Quote trans. should not have, or stop on, Payment Box.
'==      --  Add "About" menu to POS main to show WhatsNew page..
'== 
'==
'== -- Updated 3501.0809  09August2018=  
'==     -- Use Combined File/Sql subs module...
'==       Incl.  Fixes to modAllFileAndSqlSubs to Get correct appname for LocalDataDir..
'==
'== -- Updated 3501.0814  14August2018=  
'==     -- Fix clsJMxPOS31 to separate CreatePosTables into clsJMxCreatePOS...
'== 
'==
'== -- Updated 3501.0824  24August2018=  
'==     -- Add ENTER and BackSpace capture for Sales Payments....
'== -- Updated 3501.0901  01-Sept-2018=  
'==     -- Fixes (Capitulation) for Goods received.. Update stock prices.....
'==     -- 3501.0920-  Improve AUTO-barcode for NEW Customer...
'==     -- 3501.0920-  ADD AUTO-barcode option for NEW STOCK Item....
'==     -- 3501.1001-  01-Oct-2018 -Make POS_EMAILQUEUE_SHAREPATH dependant on "ServerShareNetworkPath"....
'==                 AND  - add Setup for Backup-Server Paths..
'==
'== -- Updated 3501.1024  24/29/30-Oct-2018=  
'==     -- Fixed Crash in Sales Invoice Report due to Null Payment info....
'==     -- FIXed-  POS Admin Panel-  (Setup) btnCashDrawers is (mistakenly?) disabled on Main form Load, 
'==            NOW is enabled to be able to assign Cash Drawers to printers.
'==     -- Customer Admin Form. Editing mode..  Enable Credit Limit if AccountCust checked...
'==     -- Fix to POS Main.. Move "Show Last Trans. to Till-Dropdown Menu on Main top toolstrip"...
'==          AND add CashUp and Change-Till to dropdown menu.
'==     -- Fix to POS Sales.. 
'==            Do NOT Allow Sale Commit while there are o sale Items AND no Payments
'==     -- Show Till (cashDrawer) on ShowInvoice/showPayment Forms... 
'==     -- STOCK Admin Form. RE-arrange Category and Brand, Supplier DropDowns for more clarity...
'==
'== -- Updated 3501.1104  01/02/03/04-Nov-2018=  
'==     -- Fixing Col. Sorting disfunctions Crash in FindSerials browsing....
'==            THIS is actually a problem with clsBrowse34..  When user supplies the SQl SELECT script,
'==               The ColTables collection passed in does not necessarily contain Table Columns info
'==                for JOINED tables that may be included in the Users SELECT script.. 
'==                  So references to these for Data tyoes will cause runtime errors..
'==             Solution is to not reference colTables when user supplies SQL script.
'==
'==     -- Fixing Brower FORM.. Get latest frmBrowse33 (class frmBrowse) From JobTracking.
'==            and update to catch ENTER key to Return currently selected row....
'==     -- Goods Received. Use latest frmBrowse33 (class frmBrowse) so we get Full text Search
'==            and move Supplier Name to 1st Col. of colPrefs for Supplier....
'==            and move Dexcription to 1st Col. of colPrefs for Stock Lookup...
'==
'== -- Updated 3501.1107  07-Nov-2018=  
'==     -- Re-importing Brower FORMs.. Get (AGAIN) latest frmBrowse33 (class frmBrowse) 
'==           and clsBrowse34.vb From JobTracking.
'==     -- Goods Received.  Show SerialsInput form after Qty validated in Grid Line. 
'==     -- Final Compile for Release with Updated clsBrowse34... 
'==     -- 3501.1108-  AnotherFinal Compile for Release with Updated clsBrowse34... 
'==
'== -- Updated 3501.1217  17/20-Dec-2018=  
'==     -- New Class to print Goods-Received Transaction.
'==     -- Goods Received.  Fix to Commit GoodsReceivedLine..
'==             (Was writing out total-ex value instead of total-inc value)
'==     --  clsBrowse34- Now In POS. Reformat Decimal (money) columns if requested...
'==     --  Add RETRY to gbLogMsg... in File Support..
'==
'==
'==    New Build No.- 3519.0117 17-Jan-2019= 
'==         -- Fixes to Discovering users for POS only)
'==         --  Fixes to Reports- Sales Invoice List to show ALL payments for Invoice.
'==         --  Update Copyright 2019..
'==         --  Fix Sale- nettCredited mustn't include PaymentCredited as credit note...
'==         --  Fix Sale- Make "ChargeToAccount"=true the initial value for Account Cust. Sales.
'==                 and make TABSTOP=false for Payments Grid when "ChargeToAccount" is checked....
'==                 and fix tabbing/Enter for Discount....
'==
'==   Updated.- 3519.0127/0130 27/30-Jan-2019= 
'==     -- Fixes to Stock Sales Report for Stewart-
'==            ie Select Optional Category to Report on, and report on Average Sell Price..
'==
'==  IN PRODUCTION- 07-Feb-2019--
'==  IN PRODUCTION- 07-Feb-2019--
'==  IN PRODUCTION- 07-Feb-2019--
'==  IN PRODUCTION- 07-Feb-2019--
'==
'==   Updated.- 3519.0207 07-Feb-2019= 
'==     -- Fixes to Show Invoice (No Cash Drawer column for Quotes.)-
'==     -- Fixes to frmStocklabels to show corect RRP (Not sell_ex)-
'==     -- Fixes to GoodsReceived to strip leading zeroes from scanned barcode id=f necessary.-
'==
'==   Updated.- 3519.0209 08/09-Feb-2019= 
'==     -- Fixes to Tax Invoice to Print ABN and bank stuff.-
'==     -- Fixes to Cashup Sessions to overcome zero payment is's in payment-no cols..-
'==
'==   Updated.- 3519.0211 11-Feb-2019= 
'==     -- Fixes to frmShowInvoice to print "Tax Invoice" on Receipt.-
'==     -- Fixes to frmShowInvoice etc for Print Quote crashes.-
'==     -- Fix Stock Admin Alpha barcode INSERT error.- (was missing quotes.)
'==
'==   Updated.- 3519.0214 12/14-Feb-2019= 
'==     -- Fixes to Stock Admin for Grid browsing problems 
'==              (Force a delay after selection changed before showing details.)..-
'==     -- Fixes to Stock Admin to make last added Barcode item selectable. 
'==     -- Fixes to Stock Admin Stock Item Detail panel to make Brand,Supplier AutoSuggestive.. 
'==     -- Updates to GoodsReceived to add Sell_inc editable column. 
'==     -- Fixes to Subscriptions-  crash in Select Sub from from ENTER on Find text box.
'==
'==   Updated.- 3519.0217 17-Feb-2019= 
'==     -- Fixes to Cashup/Paynents Analysis to rename current summary to "Revenue"-
'==          and to build new summary for actual Till analysis..
'==           and to drop "Discount on Payment" from all analyses as it is irrelevant in this context
'==     -- For Sale class "clsPOS34Sale" 
'==               make sure that "mLabSaleChargeBalance" start off Trans. as .Visible = True
'==
'==   Updated.- 3519.0218 18-Feb-2019= 
'==     -- Fixes to Cashup/Paynents for enpty Till crash..
'==
'==
'==   Updated.- 3519.0219  Started 18-Feb-2019= 
'==     -- Fixes to Laybys.. 
'==         - "gbCollectCustomerLaybys"- Fix code to add to layby_id collection (needs cstr() as key.)..
'==         - clsPos34Sale- Setup Cust. Allow to choose sale/layby after not delivering Layby..
'==         - Add code to actually Cancel Layby if requested...
'==
'==   Updated.- 3519.0221  Started 21-Feb-2019= 
'==     -- Fixes to Various modules and forms to allow A4 Invoice printing on NON-account Sale... 
'==     -- Fixes Customer Admin to stop "8664" from appearing in the Find Textbox on startup.... 
'==     -- Fixes Customer Admin-  Catch ENTER key for Full Text Search.... 
'==     -- MORE Fixes to for Grid browsing problems..-  
'--           ie. Use "RowEnter" instead of SelectionChanged (as per Customer Admin)
'==
'==   Updated.- 3519.0224  Started 22-Feb-2019= 
'==     -- Update to Debtors Report- Add option for Summary only.... 
'==     -- Update to ClsPrintSaleDoc- Fix error in 17-Line invoice No 584 from JH Williams.... 
'==             (17 lines means it should have thrown new page to printo totals.)
'==     -- Update to Debtors Statements to Add option for Summary only.... 
'==     -- Update to GoodsReceived to disable Grid while pre-loading PO details..... 
'==
'==   Updated.- 3519.0227  Started 26-Feb-2019= 
'==     -- Update to GoodsReceived- Recalculate Extension when Cost validated, as well as wgen Qty is.
'==                        AND ALSO re-calculate Sell_inc from Cost_inc +Margin, then re-cal. Sell_ex.... 
'==     -- Update to frmStockLabels to Fix Rounding. (Must decimal.round to 2 decimals before our rounding.).
'==     -- Fix code frmPosMainMdi code to Check max permited users logged in... (Now max=3 for TRIAL EXPIRED.)
'==     -- Customer Admin-  Show All Invoices for Customer. (Not just on-account.)
'==
'==   Updated.- 3519.0304  Started 02-March-2019= 
'==     -- New class clsWhatsInStock to redo this report as Standard Report with Cat1 SUBTOTALS...
'==            (No longer a grid report.)
'==     -- Product Report- Add option to report on Taxable/Non-taxable....
'==     -- GoodsReceived- Add ContextMenu on Grid to Copy Barcode to ClipBoard....
'=      --3519.0304=  Cashup Form- catch Ctl-Z for open-Till function..
'==
'==   Updated.- 3519.0311  Started 05-March-2019= 
'==     -- Updates to clsCashupPayments to add sub-totals to Sorted Item Details....
'==     -- Update to Subscriptions Invoicing Log to add Mark/Unmark checkbox to Subs rows, 
'==                 and to implement Multiple Invoice-all-Marked functions..
'==             And to use frmBrowse33 for lookups..
'==      >> -grh 3519.0311  Started 08-March-2019= 
'==      >> Supplier Barcode now Auto-generated !!! (Updates to frmEdit)
'==      >> GoodsReceived/PurchaseOrder-  Catch F5 on supplierBarcode for new Supplier.
'==      >> Stock Admin- Add references buttons for Cat1, Cat2, Brands..
'==      >>  Statements- Make ZERO the default for Closed invoices Days-to-show..
'== Was Released to Precise..
'==
'==   Updated.- 3519.0323  Started 14/23-March-2019= 
'==     -- Sales (clsPOS34Sale)-  
'==          => Looking up Customers- make special Sql-Select for Browser 
'==              to make a column of [lastName, firstName] in browser Grid.  
'--    -- ALSO- Update frmBrowse33 to accept User's Select-Sql..  
'==            ALSO check that frmBrowse33 cancels out on ESCape pressed.
'==    -- Customer Admin-  Include Refunds in customer Info Tab..
'==         ALSO- frmShowInvoice:  Show Refund amounts destinations.. (Refund as cash, CreditNore etc.)
'==         ALSO- Ditto for printing Invoice  (clsSaleprintDocs.)
'==    -- Purchase Order Printing- Email text needs to have Our OrderNo (Suffix also.)
'==    -- MAJOR-  "frmPosSaleChild"- Add TextBox to Payments panel-
'==            for User to decide on Amount of CreditNote to withdraw to pay for Sale.
'==                  ALSO- formBorderStyle  is now fixedToolWindow..
'==    -- Subscriptions..  Add button to show StockAdmin form.
'==    -- Email Queue. Put Try/catch around the SendAll loop to catch error..
'==    -- Statements- Add extra scan button to mean "Include Email recipients only"..
'==    -- SALE- Reduce frequency of Credit Limit warnings to ONE..
'==    -- frmPayments- Full Text Srch (frmBrowse33) now used for Cust Srch. 
'==    -- fremBrowsePOS- Add full text search functions (Cloned from frmBrowse33..)
'==                   (eg used for Supplier Admin.)
'==    -- frmPosMainMdi-   Tidy up Main ToolStrip items... 
'==
'==   Updated.- 3519.0325  Started 25-March-2019= 
'==    -- Subscriptions..  If No email address for customer, Invoice anyway, and report "Not Emailed".
'==        ALSO, Allow creating new Subscription, even if no email address..
'==
'==   Updated.- 3519.0404  Started 30-March-2019= 
'==    -- Subscriptions.. When Reporting Subs/Custs with no email, 
'==                 DON'T show the actual list in the MsgBox popup. .
'==    -- Sales- Add button (label) and code to "Convert" a Quote into a sale.
'==              ie.. Allow import of Quote items into sale. and ask for SerialNos where needed.
'==    -- Sales- Selling Serial Item.  If Serial NotInStock, then
'==                  Confirm with operator that Item is correct.
'==    -- Reports-  Product Sales Fix- Must Total TotalCost_ex column (NOT cost_ex.)
'==                 ALSO DON'T use PrintDataGridView Function- 
'==                 INSTEAD, Convert to user Standard clsReportPrinter.
'==    -- Goods Received. Editing grid line. CHECK if Cost changed before updating Sell..
'==    -- TRIAL PERIOD Extended to ninety days for everyone...
'==         AND Unlicenced Users restricted to ONE user, EXCEPT for Precise (still THREE users.) 
'==
'==   RELEASED as 3519.0404..
'==
'==   Updated.- 3519.0414  Started 12-April-2019= 
'==    -- SALES-  Warn operator if Account Cust has outst. OVERDUE amounts (30+ days). 
'==    -- SALES-  Make sure that discount/discountTax are rounded to 2 decimals.. 
'==              AND in Account Payments round off invoice amount to 2 digits when comparing with payment.
'==    -- StockTake: Add code for btnStockAdmin to show Stock Form.
'==    -- SALES- Selling out Layby..  Include any discount prev. set on Layby ..
'==     -- Fixes to Laybys Form for Discounts... 
'==
'== - - - - RELEASED as 3519.0414 --
'==
'= = == = =  = = = === 
'==  
'==    NEW VERSION 4.2  FIRST Version 4.
'==    NEW VERSION 4.2  FIRST Version 4.
'==
'==    First New Build- 4201.0416 -  Started 16-April-2019.
'==    -- 4201.0421.  frmStockAdmin made into Child..
'==    -- 4201.0422.  frmFindSerial made into TDI Child..
'==    
'==    -- 4201.0424.  Child forms now converted to UserControls....
'==    -- 4201.0503.  use TS-ProfessionalRenderer for "Ribbon" TS buttons....
'==    -- 4201.0507.  New Stocktake and Payments-Reports migrated from JobMatix35....
'== NEW STUFF-
'==    -- 4201.0519/520.  Purchase Orders to have txtSupplierCode textbox, 
'==          and update of SupplierCode Table for New SupplierCode.....
'==    -- 4201.0528.  Make Layby's  into Child User Control. 
'==               Add TabControlMain, and a tab to show ALL stock items under Layby..
'==    -- 4201.0530.  Make original frmCustomer into a host shell for ucChildCustomer
'==              So that it can still be called from JobTracking... 
'==
'==    -- 4201.0531.  Fix modPOS31Support (gbGetCreditNoteHistory)-
'==                                to initialise "decCreditNoteCreditRemaining" to zero.
'==               -- ALSO fix Cashup Session History to show comments in main Comments Textbox.... 
'==    -- 4201.0611.  11/13-June-2019-   
'==         --  New Report "Goods REceived in Period..
'==        --   Add Cat2 to Stock Sales Cat selection..
'==    -- 4201.0618/0623.  11/18-June-2019-   
'==         --  Debtors Report and Statements. 
'==               Show Credit-Note Available balance and Cust. Phone No....
'==         --  Subscriptions-  Fix crash happening after cancelling a Customer Lookup.. 
'==         --  clsReportPrinter-  Fix For preview- make starting zoom to 90%... 
'==         --  Credit Notes History Report-  Tidy up... 
'==
'== - -- RELEASED as 4201.0627 --
'==
'= = == = =  = = = === 
'==
'== NEW revision-
'==    -- 4201.0707/0708.  Started 05-July-2019-
'==       -- Add file->Local Preference for LOCAL Re-ordering of payment Details.
'==       -- Payments and Sales- AND Cashup..
'==                   ALL  Use clsPaymentTypes to get PaymentDetails for Grid. .
'==       -- Payments- Catch ENTER on PaymentDetails Grid (As per Sales). Load balance into Grid. .
'==       -- MAIN- Form. Add Shift-F6 shortcut for "New Payment" form.
'==       -- REPORTS- Sales Invoice Listing..  Fixes to crash, and to Showing of Payment Details..
'==       -- Selling Quote- Fix Error report when serial was bypassed.. .
'==       -- New Quote- Importing old quote-  don't ask for serials !!.. .
'==       -- Payments and Sales-
'==                 Each new transaction must start with First PaymentDetail row as CURRENT cell...
'==       -- Payments: Commit-Confirm dialog:  Make Email/Print options UNCHECKED to start.. 
'==    -- 4201.0717.  Started 17-July-2019-
'==       -- SupplierCode Table-  DROP Primary Key (not needed), expand supCode col. to 40 chars..
'==       -- PurchaseOrderLine Table-  Expand supplierCode col. to 40 chars..
'==       -- SALES- Apply ROUNDING to Invoice only if CASH only transaction...
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
'==      -- ALSO For SALES with discount..  
'==                      Fix Spurious Popup msg re discount being re-set..- 
'==      -- For the Stock Admin screen, on Serials Tab (grid) 
'==               add right-click context menu to copy  Serial Number to the clipboard.  
'==               AND on the Purchases Tab (grid) add right-click context menu to copy  Supplier Invoice Number to the clipboard. 
'==      --  Similarly, for the Customer Admin screen, on INVOICES Tab (grid) 
'==                add right-click context menu to copy  Invoice Number to the clipboard.  
'==           AND on the Item Sales Tab (grid) add right-click context menu to copy  the Item Barcode to the clipboard. 
'==
'== NEW revision-
'==    -- 4201.0830.  Started 28-August-2019-
'==        -- New UserControl for Transaction Lookup (Sales Invoices and Payments)..
'==        -- FIX frmCustomer form (AND the child ucChildCustomer)..
'==        -- Launching JobTracking-  Allow multiple instances..
'==        -- Add "Statements" to Accounts Menu new Dropdown button.. Drop Accounts TS, Statements button..
'==        -- REPORTS- Fix Product Sales Report by Category.-
'==        -- Sales. Removed Tabstop om Discount % combo...
'==
'== = = =
'==
'== NEW revision-
'== NEW revision-
'==
'==    -- 4201.0929/930/1002/1003.  03-Oct-2019-  Started 19-September-2019-
'==        -- New Buttons and icons for Stock, purchasing and categories...
'==                  And now using ContextMenuStrip..
'==        -- Fix PurchaseOrders printing- Capturing PDF for Email has corruoted PDF....
'==                 (Hint- Must Wait for print Completion as per Print Sales Invoice..)
'==        -- Subscriptions-  Lock Invoicing loop (Using a higher level transaction only),
'==                   and the SubscriptionInvoices Table to keep out competitors.
'==        -- Subscriptions- Add column to Subscriptions Table: "OkToEmailInvoices bit default 0"
'==        -- Subscriptions- Report (After Refresh) any Subs that are terminating in this cycle..
'==        -- Debtors Payments- Allow Reversing a Payment even when nothing is outstanding...
'==        -- Debtors Payments- Allow user to Discount full Outst. balance of Invoice...
'==        -- Quote printing- Removed "Invoice Paid" text at foot...
'==
'== NEW revision-
'== NEW revision-
'==
'==    -- 4201.1007.  07-Oct-2019-  Started 07-Oct-2019-
'==        -- Debtors Payments- Fix problem with CreditNote as change (showing when CreditNote is Input)...
'==        -- Payments Grid- Fix crash on Enter Key if currently over-paid....
'==        -- Sales Qty.. Enforce Integral quantity unless IsNonStockItem.....
'==        -- frmShowPayment-  Show any Discount on Payment in the Listbox of payments.... 
'==        -- Subscriptions--  Catch Enter Key on Subs srch text- Execute the Search. 
'==
'== NEW revision to fix previous.-
'==
'==    -- 4201.1013.  13-Oct-2019..
'==        --(a) FIX to SALE of IsNonStockItem.. Allowing fractions was failing
'==                    when item move from Grid back up to edit line...
'==        -- (b)  Stock Admin.  On POS Item Sales Tab, 
'==                      catch DoubleCiick event on invoice line to bring up  (show) the full invoice.
'==        -- (c) Goods Received.  make Exit button the last in the Tab Sequence.
'==        -- (d) Debtors Report- Make Separate Menu entry, class,  and function call for direct access to Debtors Report
'== NEW revision-
'== NEW revision-
'==
'==   -- 4201.1027/1030/1031.  31-Oct-2019-  Started 27-Oct-2019-
'==      -- MAIN TAB Control... Increase tab text size, and add proper Close X Icon...
'==      -- Debtors Report... Add extra menu entries for Detailed Report +30/60 days Closed Invoices..
'==                  Also Add feature to choose Customer for Debtors History. 
'==                  Also Fix printing of report from preview..
'==      -- Stock Admin (Purchases Tab)... Add code to catch DblClick..  Show Goods Received Invoice for Selected Line.
'==      -- frmLookupGoods-  Extract function "ShowGoodsInfo" into class so it can be called from StockAdmin. .
'==                                 (New class 'clsGoodsInfo' and Preview Option for clsPrintGoods.)
'==      -- ucChildGoodsRecvd.-  Move button LookupGoods up to top RHS of form. .
'==      -- ucChildStockAdmin.-  Add DblClick to Item Purchases Grid to show GoodsReceived for that Item. .
'==      -- MAJOR- WE Have DISABLED the Payments Frame- grpBoxSalePayments for On-Account Sales !!....
'==                  Acompanying payments are no longer permitted.
'==             ALSO- make Top chkOnAccount2 checkbox part of Tab sequence for selecting transaction.
'==      -- Commit Sale- Allow emailing of Invoice for Non-account Sales.
'==      -- Sale- Now has five-cent Rounding of Sell Price in Item EDit Line.
'==      -- Commit Sale- Payment Disbursement Line Amount MUST include Credit-Note amt used (wdl).
'==      -- Importing Quote-- MUST use Quoted Selling price...
'==      -- STATEMENTS-- For Emailing, address customer as << FirstName LastName >>...
'==  = = = = = = 
'==
'== NEW Build-
'==
'==   -- 4219.1130.  06-Nov-2019-  Started 06-November-2019-
'==      -- Finish Main Ribbon, with DropDown buttons for Reports, Settings..
'==      -- Paint background unused tab space for Main TabControl..
'==      -- Adjusted sizes for Child UserControls to fit inside Tab Pages...
'==      -- POS Main- Drop "labStatus" from Backup call....
'==      -- Add "Quotes" to Lookup Transaction...
'==              ALSO add "isOnAccount" column to Invoice Lookup.
'==              ALSO add Extra REFRESH button..
'==      -- Add Ctrl-L Shortcut to Lookup Transaction....
'==      -- Stock Admin- Showing Stock Info. Clear old stock Picture first (if any)..
'==      -- Statements User Control-  Update to use new Debtors info class (as per Debtors Report from menu). 
'==      -- Statements printing-  Adjust Lines per page to stop overprinting.. 
'==      -- Customer Admin.. Add Tabs for a/c Payments, Quotes AND Jobs. 
'==      -- INCLUDE Ref to JMxRetailHost.dll for 
'==                  access to modRetailHostIF and clsRetailHostJMPOS in project so POS can access View Job. 
'==      -- On "frmShowInvoice"-  IF isOnAccount then 
'==                Show "Charged to Account" in Payments Footer.
'==      -- Moved modules "modBackupRestore35"and "modAllFileAndSqlSubs" to the new Dll JMxRetailHost.dll
'==      -- frmPOsMainMdi-  Load Updated RAs exe-  "JobMatixRAs42.exe"
'==      --  New Quotes- "clsPOS34Sale". Fix problem making new Quote..  (Stewart 19/11/2019) 
'==                User can't navigate back to add items once the Discount section has been arrived at.
'==      --  On frmShowPayment, (and receipt) For non-account (ie, for Cash sales), 
'==            the Payment record column "nettAmountCredited" does NOT include creditNoteWDl amount..
'==              So add this Cr-noteWdl amt in to show total contriburion to payment for the Sale.
'==
'== NEW Revision-
'==   == 4219.1216.  16-Dec-2019-  Started 10-Dec-2019-
'==     --  Customer Admin- ucChildCustomer-  
'==         --  Add code to delay Find Text in case of starting with "1" (General) in case more digits coming,
'==                  Because going straight off and finding all trans. for General is very slow.
'==         --  ALSO  Add code to Cust Grid list RowEnter to stop ReEntrancy crash.
'==         --  ALSO  Add "Start New Job" buttons and Checkin/Amend context Menu to Customer Jobs Panel
'==                  to launch an instance of "ucChildNewJob" UserControl inside a new Tab...
'==     -- POS Reports.. 
'==            Invoice-Listing- Add TabPage/Panel with options to select on-account or cash sales.. 
'==     -- Purchase-Order PDF (email) printing.... 
'==            In "gbSaveDocumentToEmailQueue", 
'==                  keep local file copy of PDF for SEVEN days after copying to Email Queue.. 
'==            ALSO- in "gsGetPDF_file_path" (modPrintSubs), Get AppName from gsGetAppname
'==     -- In main frmPosMainMdi, catch event from ucChildCustomer to launch ucChildNewJob, and
'==            catch the close event from ucChildNewJob to check if Exchange Calendar is to be updated..
'==     -- In main Form frmPosMainMdi, Add "About" to File menu..
'==              Note: the Form "dlgNoShow" resides in the JobTracking DLL JMxJT420.dll. 
'==                WE load it from there at runtime.
'==     -- In the UserControl ucTransLookup for payments,
'==             we must SELECT nettAmpuntCredited (NOT totalAmountReceived.)...
'==
'==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
'==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
'==    
'==   == 4221.0207.  05-Feb-2020- 
'==
'==-- 1.  IN GOODS REceived--  Printing PO..
'--           set CORRECT printer selected..--
'--      FIX to 4219.1216 !!! --   mPrintDocument1.PrinterSettings.PrinterName = msInvoicePrinterName
'==
'==    -- FIX to 4219.1216 !!! --
'==      THIS is CORRECT= mPrintDocument1.PrinterSettings.PrinterName = sPrinterName '= '-- FIX to 4219.1216 !!! - msInvoicePrinterName
'==
'==   2.  Reports- 22-Jan-2020.. 
'==            Till Analysis- Add functions to show the Report in DataGrid also...
'==
'==   3.  Emailing Quotes....  (frmShowInvoice.)
'==            Email heading/text need to say "Quote"  (NOT "Invoice")...
'==
'==   4.  Tags- 05-Feb-2020.. For Build 4221..
'==         -- Add clsTags, and forms for Ref Tags and Cust tags..
'==         -- Add Column  "Tags" to Customer Table in the StartUp class clsJMxPOS31..
'==         -- Add buttons and subs to Customer admin form to update/show Tags....
'==         -- Show Customer Tags on Sale form when Customer selected...
'==   5.  In clsPOS34sale-  make chkOnAccount UNCHECKED as the DEFAULT..
'==
'==    
'= = = = = = = = = = = = = = = = = = = = = = = == =  == = =  = = = = = = == 
'==
'==  FROM DEVEL- PREP FOR- Updates to 4221.0207  Started 24-March-2020= 
'==
'==  Target is new Build 4233..
'==  Target is new Build 4233..
'==
'== NEW BUILD == 4233.0421.  21-April-2020- 
'== NEW BUILD == 4233.0421.  21-April-2020- 
'==
'==   1.  frmEmailmain- disable the event "dgvEmailList_SelectionChanged" because it is causing the crash that
'==                      comes after sending a single email because the grid is empty.
'==   2.  ucChildCustomer-  Fix for Barcode "1" pause not working first time in..
'==          IN the event "timerFind_Tick", drop the test for valid customer_id around call to Browse.Find..
'==   3.  frmPOS3Reports-  NOW a CHILD UserControl-
'==            --  For Sales Invoice Report-  Link to new class for re-write..
'==            -- For large reports=  Confirm preview or Cancel....
'==            -- FIXed dateAdd for prev 12-months..  (- 1 year PLUS ONE DAY..)..
'==   4.  frmStockLabels-  Re-design input Barcode, qty as textbox line, 
'==             to avoid editing in grid and crashing on qty. (Grid now read-only)
'==
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==   Updates to 4233.0421  Started 24-April-2020= 
'==   Updates to 4233.0421  Started 24-April-2020= 
'==
'==  Target is new Build 4234..
'==  Target is new Build 4234..
'==
'==   1.  ucChildPosReports-  STILL a CHILD UserControl-
'==       --  For Sales Invoice Report-  ADD PROFIT on Invoice For BOTH GRID and preview Versions.
'==   2.  Subscriptions- Add new TreeView subclass,
'==                    and add an "analysis" panel to Subs Form to show Product/Sub-Cust Query Result..
'==   3.  CustomerAdmin-  Creating new Job in POS-  
'==          -- Set CustomerName as "lastName, firstName", as per JobTracking..
'==   4.  GET RID of all DoEvents calls in Stock Admin.. 
'==                 -- IT Allows re-entry to DGV refresh routines  !!!!
'==   5.  Add Startup code to check running code is not earlier than Latest Version Build set in SystemInfo.
'==             -- Also, update Build in SystemInfo if running code is later..
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==
'== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
'== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
'== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
'==
'==  Target is new Build 4251..
'==  Target is new Build 4251..
'==
'==
'==   1. MAIN THEME is implementation of ACCOUNT-INVOICE-REVERSAL (Account "refund")
'==       --  Involves creating a REFUND (onAccount) full transaction as a mirror of Original.
'==       --  Not allowed if payments have been made towards original Invoice.
'==       --  Not allowed if original Invoice involved DELIVERY of a Job or a Layby..
'==       --  Transaction is accessible only from frmShowInvoice (showing original Invoice)..
'==                 Needs NEW CLASS  "clsAccountReversal".. 
'==       --  Transaction needs SUPERVISOR PASSWORD...
'==       --  Reversed Invoicess noted on Debtores Report.
'==
'==   2. SECOND MAIN THEME is implementation of EXTENDED Refund Details for REFUNDS..
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
'==   3. THIRD MAIN THEME is implementation of a SupplierCode Field in the StockAdmin details Tab..
'==       --  Involves also adding code to the New/Update Stock Commit to also add/Update Supplier Code Table if needed.
'==       --  ALSO Involves FINALLY revamping frmStock,
'==                   so as to make into purely a container for the ucChildStockAdmin UserControl.
'==       -- ALSO for StockAdmin- Load the cat1/Cat2 Combos SORTED in Alpha Order.
'==
'== --
'==   4. ALSO Updated the CreateDB Module for the TWO new columns to be added to Payments Table-  
'==                      viz- "RefundOtherDetailAmount"(MONEY), and "RefundOtherDetailKey" (VARCHAR).
'==                         to Cash/CreditNote/EftPosDr/EftPosCr already recorded in Payment record.
'=    '==   ALSO HERE we wanted to update the Create Module to add "Tags" column to the customer Table
'==      BUT it stuffs up the importing Customers from RM, so column has to be added on the fly at POS Startup.
'==
'==
'==   5.  ALSO FIXED-  In Cashup Commit- Negative payments (ie more Refund ) were not being written out to Shortages.
'==
'==   6.  ALSO FIXED-  In frmStockLabels, make up/down control NOT readOnly, so user can enter a Number.
'==
'==   7.  ALSO FIXED-  In Sales Form (ucPosSaleChild) Fix Fonts in Item entry Line textboxes..
'==
'==   8.  ALSO FIXED-  In ucChildPayments-  ListBox of previous payments not having/showing full Paymnent-ID..
'==                               Results in wrong payment being show when clicked on..
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
'==   2. Sales Report- Show Nett sales after Refunds, and check we show Profit After Discount.
'==                  -- ALSO- Totals-only Option-  Add function get totals section from last report run..
'==
'==   3. Purchase Orders-  NewSpecialOrder..  DO NOT adjust order Qty for qtyInStock-  Use DB order_quantity..
'==
'==   4. Purchase Orders-  NewSpecialOrder.. SupplierCode must be installed in Grid being auto-built.
'==   
'==   5.  Stock Labels- bug reported- user is trapped in loop if Invalid barcode entered (Stewart 28/6/2020)..
'==
'==   6.  StockAdmin-  frmStock Host form not closing when new stock item added via call from GoodsReceived..
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
'==
'==
'==
'==  Target-New-Build-4257..
'==  Target-New-Build-4257..  07-July-2020.
'==
'==   1. MAIN REASON is to Print list of Stock Table Items with barcode in Barcode Font..
'==            -- ADDS NEW CLASS clsStockBarcodeList..
'==            --  Also adds extra checkbox to Report Stock Options "Don't include stock with postive qtyInStock." 
'==
'==   2. Allow Account-Invoice Reversal with WARNING for Delivered-Job Invoices..
'==
'==   3. In Stocktake- negative qtyInStock-   
'==           -- ADD a checkbox option-  Don't pre-load items with negative qtyInStock balance.
'==
'==   4. Cust-Admin Jobs Tab-  Fix OnSite Job Button text.. So OnSite is not broken..
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
'==
'==Fixes to Build 4257.0707  
'==
'==   Target-New-Build-4259 -- (Started 17-Jul-2020)
'==   Target-New-Build-4259 -- (Started 17-Jul-2020)
'==   Target-New-Build-4259 -- (Started 17-Jul-2020)
'==
'== A.  POS System  30-Jul-2020
'==
'== (a) To Fix- frmShowInvoice crashes when displaying a Quote.. 
'==             See end of Event Shown, where "invoice_date" column is referenced without checking if Quote ot Invoice.
'== (b) Account Invoice Reversal- 
'==      -- If Account payments have been made to the Invoice 
'==           - User should be able to reverse the Account Invoice if all the Account Payments are reversed first..
'==      -- Payments Form needs ReversedInvoices to be filtered out..
'==           ALSO before committing payment, check that Invoices were not changed in the meantime..
'==             (NEEDS "gbCollectCustomerInvoices" to have option to be in a Transaction.)
'==
'== (c) Stock Labels (Stewart 17Jul2020).. 
'==    --  When focussing  on Qty Up/Down, select text content for easy over-writing.. 
'==         (a TextBox will replace the NumericUpDown control.)
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
'==
'== Fixes to Build 4259.0730  
'==
'==   Target-New-Build-4262 -- (Started 14-Aug-2020)
'==   Target-New-Build-4262 -- (Started 14-Aug-2020)
'==   Target-New-Build-4262 -- (Started 14-Aug-2020)  26-Aug-2020..
'==
'==    A.  --  JobTracking has MADE Form "frmJobMaint32" INTO USERCONTROL- ucChildJobMaint..
'==          --  MAKE NEW Form "frmJobMaintBase" to hold USERCONTROL.
'--            SO THAT we show it as a Child in a POS TAB..
'==               -- SO now From Customer Admin we can use the UserControl in a TAB page..
'==
'==    B. -- Customer Admin-  Account Invoice Reversal- 
'==         -- Customer Admin to show "REVERSED" instead of Outstanding invoice amt if Invoice was reversed.. 
'==            AND 1. ADD mnu Items to Active Jobs Context menu to View/Update Job via new ucChildJobMaint UserControl.  
'==                2. Add Phone Nos. to Cust. search args..
'==
'==    C. -- Customer Account Payments-  
'==         1. NOT DONE-  Add code to not allow negative outstandings to be processed..  
'==         2. Payments Grid- (Stewart 24Aug2020) Fix problem with Payments Amount being displayed in in the 
'==               detail descr. label column..  
'==               (In dgvSalePaymentDetails_KeyDown, check for CurrentCell being readonly if ENTER key pressed..)
'==    D. - Subscriptions.
'==        (1)  Add filter to Product Analysis to screen out non-activated and cancelled Subs.  (Martin email 11-Aug-2020)
'==        (2)  Add code to invoicing to show list of checked items about to be invoiced to get confirmation.. 
'==                 (Check/Fix why unchecked items get invoiced !!)
'==
'==    E.  Debtors Report-  Allow space for Reversed Invoices so as to stop overprinting the next customer.. . 
'==         (Martin email 26-Aug-2020)..  Also, show Reversed Invoices only on detailed report..
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==
'== Fixes to Build 4262.0826  
'==
'==   Target-New-Build-4267 -- (Started 07-Sep-2020)
'==   Target-New-Build-4267 -- (Started 07-Sep-2020)
'==   Target-New-Build-4267 -- (Started 07-Sep-2020)
'==
'== (a) Debtors Report-   
'==     Show Reversed Invoices only on detailed report, and only for current period..  
'==      ..  On Summary, DO NOT show customers with zero balance, even if thaey have reversed invoices in current period.
'==      --  PLUS-  MAKE new class "clsDebtorsReport" to give report its own class.
'==            --DONE----29-Sep-2020--
'==
'== (b)  Sale Form-  
'==        The Customer Account information (outstanding amounts) shown on the Sales screen 
'==                    do not yet take into account any reversed invoices for that customer..  
'==                     Use clsDebtors to collect and show actual outstanding only. 
'==           -- DONE --29-Sep-2020-
'==
'== (c) Customer Admin-  
'==          After Editing Customer Details.. make sure same cust can be selected for re-editing if needed.
'==            --DONE----29-Sep-2020--
'== 
'== (d) clsDebtors-  FIX crash that happens when getting invoices and NONE are on file
'==                    (eg for invoices for a particular customer.)
'==            --DONE----29-Sep-2020--
'==  
'==
'== (e) POS DB Create-   (1) Add INSERTIONS to new  Stock Table to set up six Labour Hourly Rates..  
'==                               Barcodes like "01-LAB-HRLY-WKSH-P1" etc etc  
'==            (2) ALSO add "SERVCE" to Cat1 and Cat2. 
'==            --DONE----29-Sep-2020--
'==
'== (f) Purchase Orders- Auto  ordering..  
'==       (Martin 22/9/2020.)  Qty to order to only be enough to make stock back up to Max level.. ?  
'==       ie Max (DB order_quantity)..  Same as what MYOB does.
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==

'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==
'== Fixes to Build 4267.0929  
'==
'==   Target-New-Build-4277 -- (Started 07-October-2020)
'==   Target-New-Build-4277 -- (Started 07-October-2020)
'==   Target-New-Build-4277 -- (Started 07-October-2020)
'==
'==  (a) POS- Reversals to Payments that included Saved Credits Notes-   
'==        -- Credit note amounts are being treated As credits To Customer instead Of showing As debit (reversed).  
'==          See "gbGetCreditNoteHistory" CreditNote Report, 
'==               And CreditNote balance On Sale Screen...
'==                AND  clsDebtors ("mbLoadCreditNoteBalances")..
'==          IF Payment record is a REVERSAL, then "creditNotePaymentCredited" and "creditNoteAmountDebited" need reversing.
'==  -- DONE --
'==
'==  (b)  POS-  Fix DivideByZero Runtime Error in Sales Invoice Report.
'==
'==  (c) POS Sales Commit-  Warn if transaction is taking in more than $500.00 into Credit Note..
'==        10-Oct-2020..
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = = = = == 
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==
'== Fixes to Build 4277.1010  
'==
'==   Target-New-Build-4282 --  (22-October-2020)
'==   Target-New-Build-4282 --  (22-October-2020)
'==   Target-New-Build-4282 --  (22-October-2020)
'==
'==  A.  New Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
'==         Done by cloning clsAccountReversal and moddying and adding Payment Reversal stuff..
'==     -- ALSO-  Needs new button added to frmShowInvoice.
'==     --  ALSO Fix formatting in frmCrediNotesReport.
'==
'==     -- ALSO- Fix Terminal_id missing in clsCshSaleReversal, clsAccountReversal, 
'==                     and frmShowPayment (for Payment Reversal.).
'==
'==
'==  B. For POS Sales Reports-  Incorporate  Dropdown to select Staff Sales...
'==
'==
'==  C. For JobMatix42Main-  Incorporate  Setup..
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'=='= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==
'== Fixes to Build 4282.1025  
'==
'==   Target-New-Revision-4282.1102 --  (02-November-2020)
'==   Target-New-Revision-4282.1102 --  (02-November-2020)
'==     -- Fix PosReports (Load Event) to get rid of "ClearwaterJT" DB name out of Staff Quuery..
'==
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==
'== UPDATES to Build 4282.1102  
'==
'==   Target-New-Build-4284 --  (24-November-2020)
'==   Target-New-Build-4284 --  (24-November-2020)
'==   Target-New-Build-4284 --  (24-November-2020)
'==
'==  A.  New Child USERCONTROL to move Suppliers Admin into Main Tab Control.
'==
'==   Target-New-Build-4284-EXTRA-EXTRA --  (20-Nov-2020 +)
'==   Target-New-Build-4284-EXTRA-EXTRA --  (20-Nov-2020)
'==
'==  B.  -- Customer Admin..  Speed up loading Invoices Grid using DGV.AddRange...
'==           ALSO-  Fix Resizing for Min size needed for RHS details TabControl
'==      -- modPos32Support-- Use new gbCollectCusdtomerInvoicesEx2 using Shaping to get Invoices/Disbursements..
'==
'==  C.  -- frmEdit..  Add Staff barcode to list of Autogen fields....
'==            ALSO-  Update "date_updated" column when updating table that has it..
'==
'==  D.  New Child USERCONTROL to move STAFF Admin into Main Tab Control.
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==
'== UPDATES to Build 4284.1124  
'== UPDATES to Build 4284.1124  
'==
'==   Target-New-Build-4287 --  (30-Jan-2021)
'==   Target-New-Build-4287 --  (30-Jan-2021)
'==   Target-New-Build-4287 --  (30-Jan-2021)
'==
'== (A)   Goods Received Serials entry...  (Stewart 27-Jan-2021-)
'==      The system won't let you put in a Serial that's already in the system, 
'==        Or that you've already entered for that Invoice line.. 
'==      But it will Let you enter a Serial that was entered In a previous line In that invoice.. 
'==      (Does this mean you are trying To enter the same product twice In the one invoice ?)
'==          For the next release-  make sure that serial no's are unique over the whole Invoice, 
'==              And still also not on file already in the system. 
'==
'== (B) POS Customer Admin-  Customer data grid...  Jerami-28-Jan-2021-
'==      ADD tick box (exclude inactive) For customer search..
'==        Fix Search functions.. mbInitialiseBrowse, mbBrowseCustomerTable.
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==   Target-New-Build-6201 --  (09-June-2021)
'==   Target-New-Build-6201 --  (09-June-2021)
'==   Target-New-Build-6201 --  (09-June-2021)
'==
'==  For JobMatix62Main- OPEN SOURCE version...
'==  For JobMatix62Main- OPEN SOURCE version...
'==  For JobMatix62Main- OPEN SOURCE version...
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

'== Target 6201- Updating 12-July-2021..
'==    Updates For Target OpenSource version Build 6201...
'==  Add features to Product Sales and What's In Stock to filter for a particular stock Supplier..
'= = = = =



' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:
' <Assembly: AssemblyVersion("1.0.*")> 
'==
'== AssemblyProduct("JMxPOS420")==
'== NOW is AssemblyProduct("JMxPOS620")==

<Assembly: AssemblyVersion("6.2.6201.0718")>
'==
'== <Assembly: AssemblyFileVersion("1.0.0.0")> 
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
