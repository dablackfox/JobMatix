Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

<Assembly: AssemblyTitle("JobMatix62")>
<Assembly: AssemblyDescription("JobMatix POS and Job Tracking")>
<Assembly: AssemblyCompany("grh")>
<Assembly: AssemblyProduct("JobMatix62")>
<Assembly: AssemblyCopyright("Copyright © 2014..2021 grhaas@outlook.com")>
<Assembly: AssemblyTrademark("JobMatix")>

<Assembly: ComVisible(False)>

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("69a9580d-da61-4164-9e41-08560d92c7f6")>

'==
'== Created 3501.0606  06Jun2018=  for JobMatix35 etc..
'==    --  to Startup with POS system which can then call JobTracking exe. 
'==
'==
'== Created 3501.0727  06Jun2018=  Updated for JobMatix35 etc..
'==    --  For POS updates.. 
'==
'== >> 3501.0731  31-July-2018=  Updated for JobMatix35 etc..
'==    --  For POS updates.. 
'==    --  Remember last App started up, and prompt for change.
'==
'== >> 3501.0814  14-Aug-2018=  Updated for JobMatix35 etc..
'==    --  For JT and POS updates.. 
'==
'==
'== >> 3501.0916  16-Sep-2018= 
'==    --  Updated for POS GoodsRcvd fxes.. 
'==    --  Allow Multiple instances of JobTracking to be started from Main.... 
'==
'== >> 3501.1001  01-Oct-2018= 
'==    --  Updated AGAIN for more POS GoodsRcvd fxes, and JobTracking Service Record Fixes... 
'==
'== >> 3501.1030  30-Oct-2018= 
'==    --  Updated AGAIN for more POS GoodsRcvd fxes, and Stock Fixes... 
'--            and lots of cleanup stuff.
'==
'== >> 3501.1107/1108  07-Nov-2018= 
'==    --  Updated Release... 
'==
'==
'== >> 3501.1223  23-Dec-2018= 
'==    --  Updated Release...  
'==       Fixes to POS goods Received.. PLUS Lookup Goods, Print Goods.
'==
'=== NEW Build No-
'==
'== >> 3519.0119  09/19-Jan-2019= 
'==    --  Updated Release...  
'==       Fixes to JobTracking and POS for Number of Current users by Process..
'==
'== UPDATED- 3519.0124  24-Jan-2019= 
'==    --  Updated Release...  
'==     --  Fixes to JobTracking for Tracking mismatching Jobs on Update....
'==      --  And TabControl now used for JobsTree, Browser panels etc. etc.
'==
'== UPDATED- 3519.0130  29/30-Jan-2019= 
'==      --  Fixes to JobTracking Loading user logo.
'==      --  Fixes to POS Stock Sales report.
'==
'==  IN PRODUCTION- 07-Feb-2019--
'==  IN PRODUCTION- 07-Feb-2019--
'==  IN PRODUCTION- 07-Feb-2019--
'==  IN PRODUCTION- 07-Feb-2019--
'==
'==   Updated.- 3519.0207 07-Feb-2019= 
'==     -- various POS Fixes eg fix. to Show Invoice crash (No Cash Drawer column for Quotes.)-
'==
'==   Updated.- 3519.0209 09-Feb-2019= 
'==     -- various POS Fixes eg fix. to print ABN on Tax Invoice
'==      --  fix to Cashup Sessions. (was invalid sessions problem)
'==
'==   Updated.- 3519.0211 11-Feb-2019= 
'==     -- various POS Fixes eg fix. Fix INSERT stock failed. (missing quotes.)
'--     --  Fix Quote prinr crash.
'==
'==   Updated.- 3519.0214 14-Feb-2019= 
'==     -- various POS Fixes
'==          eg . Add sell_inc to GoodsReceived., Rounding for Stock sell_inc prices'=   
'==          -  Selecting od last stock Item inserted.. ttc.
'==
'==   Updated.- 3519.0217 17-Feb-2019= 
'==     -- POS Fixes
'==           Re-do Cashup to produce a proper till balance.=   
'==
'==   Updated.- 3519.0218 18-Feb-2019= 
'==     -- POS Fixes- EMPTY till crash..
'==
'==   Updated.- 3519.0219 19-Feb-2019= 
'==     -- POS Fixes-laybys fixes.  incl Add code to Cancel layby...
'==
'==
'==   Updated.- 3519.0221 21-Feb-2019= 
'==     -- POS Fixes-  Optional A4 Invoice printout for non-account sales....
'==     -- POS Fixes-  Stock Admin browsing fixes...
'==     -- POS Fixes-  Customer browse "8664" priblem..
'==
'==   Updated.- 3519.0224 24-Feb-2019= 
'==     -- POS Fixes- To Statements, Print Invoice (JH Williams
'==                      GoodsReceived (PO's) and Debtors Summary Report...
'==
'==   Updated.- 3519.0227 27-Feb-2019= 
'==     -- POS Fixes- To SGoods Received, Stock Admin, Customer Admin..
'==
'==   Updated.- 3519.0304 04-Mar-2019= 
'==     -- POS Fixes- To Goods Received (Context Menu), Cashup (Ctl-Z)..
'==              AND re-formatted What'In Stock Report
'==
'==
'==   Updated.- 3519.0311 11-Mar-2019= 
'==     -- POS Fixes- To Goods Received (F5 for new supplier), Cashup (Sorted sub-totals)..
'==              AND Stock Admin (new buttons for adding Cat1, cat2, Brands)
'==              And frmEdit additions for AutoGen Supplier barcodes
'==              And Subscriptions Invoice Batching.
'==              And Statements Days default to zero..
'==
'==   Updated.- 3519.0323 23-March-2019= 
'==     -- POS Fixes- Many fixes and updates...
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
'==
'==   Updated.- 3519.0325 25-March-2019= 
'==     -- POS Fixes- Subscriptions will be invoiced even if no email address for Cust..
'==
'==   Updated.- 3519.0331 31-March-2019= 
'==     -- POS Fixes- Quote can be imported into Sale...
'== !!  NOT RELEASED -
'==
'==   Further Updated.- 3519.0404 04-April-2019= 
'==     -- POS Fixes- Product Sales Report re-done with Report printer...
'==          And- GoodsREceived.. No updating Sell Price on grid if Cost not changed.
'==          And If selling NotInStock Serial..  Give more warning..
'==
'==   Further Updated.- 3519.0414 14-April-2019= 
'==     -- POS Fixes- For laybys and discount on Sale....
'==
'==
'==   Further Updated.- 3519.0501 01-May-2019= 
'==     -- POS Fixes- Stock takes changes for "Single" Free Range counting....
'==
'==
'==   Further Updated.- 3519.0505 05-May-2019= 
'==     -- POS Fixes- Addition to Payments Report...(Revenue/Till)..
'==
'==  -- RELEASED as 3519.0505..
'==  -- RELEASED as 3519.0505..
'==
'==   Updated 3519.0531. 31-May-2019=
'==         --  A. Fix modPOS31Support (gbGetCreditNoteHistory)-
'==                                to initialise "decCreditNoteCreditRemaining" to zero.
'==         --  B. Fix Cashup Session History to show comments in main Comments Textbox.... 
'==
'==  NEW VERSION.. 4.2..
'==  NEW VERSION.. 4.2..
'==
'== '==   Updated 4201.0627. 27-June-2019=
'==      For latest POS.. incl updated JobTracking for version No.
'== '==   Updated 4201.0717. 17-July-2019=
'==        For latest POS (with rounding).. 
'==              including updated JobTracking for extra Jobs Actions Context menu.
'== 
'== 
'==   Updated 4201.0727. 27-July-2019=
'==        For latest POS (with rounding of fractional CENTS)..  and Context menus..
'==
'==   Updated 4201.0831. 31-August-2019=
'==        For latest POS (with rounding of fractional CENTS)..  and Context menus..
'==
'==
'==   Updated 4201.1003. 03-October-2019=
'==        For latest POS (with Subscriptions updates for Locking,)..
'==                and Account Payments fixes. 
'==
'==   Updated 4201.1007. 07-October-2019=
'==        For latest POS (with Subscriptions updates for Search text ENTER key,)..
'==                and Account Payments fixes for Credit Notes usage.. 
'==
'==   Updated 4201.1013. 13-October-2019=  (see lachlan's email Fri. 11/10)
'==        For latest POS (with fixes for NonStockItems, and direct Debtors Report.,)..
'==
'==   Updated 4201.1031. 31-October-2019=  (see lachlan's email Fri. 11/10)
'==        For latest POS (with many fixes and Re-written direct Debtors Report.)..
'==
'==  NEW BUILD- 4219 VERSION
'==    Updated- 4219.1130 21-Nov-2019= 
'==      -- clsPrintDocs- JobMaint Printing-  Fix Printing WorkHistory for Multiple Pages.
'==      --  MAKE Forms PUBLIC- NewJobForm and Maint Form-  "frmNewJob32" and "frmJobMaint32"
'==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll for common sharing..
'==      --  Update RAs reference to call "JobMatixRAs42.exe"..
'==      --  Make "modAllFileAndSqlSubs" PUBLIC in JMxRetailHost.dll so EVERyONE can use it..
'==      --  Move  module  "modBackupRestore35" as Public into JMxRetailHost.dll so EVERyONE can use it..
'==      --  Move module "modCreateJobs3" and Attachments Form and class 
'==                        into JMxRetailHost.dll so EVERyONE  (RAs) can use it.
'==   
'==
'== NEW Revision-
'==   == 4219.1216.  16-Dec-2019-  Started 10-Dec-2019-
'==    Updated- 4219.1216 08-Dec-2019= 
'-- JOB-TRACKING-
'==      --  MAKE Form "frmNewJob32" INTO USERCONTROL.
'==      --  MAKE NEW Form "frmNewJobBase" INTO USERCONTROL.
'--            SO THAT we show it as a Child in a POS TAB..
'==              From JobTracking we call frmNewJobBase, which is container for the UserControl.
'==      --  Updates to dlgNoShow for AppName..  Can be called from POS also..
'==      --  Add a new Timer to Main Form to check for Exchange-Calendar xml files that might have come from POS...
'==               -- If there is any, and BG-Worker exchange is nor running, the Run It...
'==
'==  POS:
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
'==     -- In Child Control ucTransLookup for payments Looku[,
'==             we now SELECT nettAmpuntCredited (NOT totalAmountReceived.)...
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
'==  Target is new Build 4233..
'==  Target is new Build 4233..
'==
'==
'==    NEW BUILD == 4233.0421.  21-April-2020- 
'==    NEW BUILD == 4233.0421.  21-April-2020- 
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
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==   Updates to 4233.0421  Started 24-April-2020= 
'==   Updates to 4233.0421  Started 24-April-2020= 
'==
'==  Target is new Build 4234..
'==  Target is new Build 4234..  05May2020--
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
'==  Target-New-Build-4253..  28-June-2020..
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
'= = = = = = = = ==  = -= = = = = = = == = = = = = = = = = = = = = = = = = = = = = = = = = = = =  = = = == = = 
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
'==
'==Fixes to Build 4257.0707  
'==
'==   Target-New-Build-4259 -- (Started 17-Jul-2020)
'==   Target-New-Build-4259 -- (Started 17-Jul-2020)
'==   Target-New-Build-4259 -- (Started 17-Jul-2020)
'==
'== A.  POS System DONE on 30-Jul-2020
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

'==
'==  JOB TRACKING..
'==
'== Target-build-4267  (Started 07-Sep-2020)
'== Target-build-4267  (Started 07-Sep-2020)
'==
'==  -- Main- Exchange201-  Completion of BG-worker..  
'==      NEW module "modMyMsgBox" with Function to create a Form on the fly with textbox to show accumulated Exchange results 
'==           (.Net MessageBox is not suitable for exended message..)   
'==
'==
'==   POS..
'==
'==
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
'==       ie Max (DB order_quantity)..  Same as what MYOB does. Ignore Qty InStock if negative.
'==
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==   Target-New-Build-4277 -- (Started 05-Oct-2020)
'==   Target-New-Build-4277 -- (Started 05-Oct-2020)
'==   Target-New-Build-4277 -- (Started 05-Oct-2020)
'==
'==     On new lenovo Xeon with Server-2016
'==
'===   1. JobMatix Main is now Host for POS dll..
'==    2.  Next thing is to Incorporate Setup (Install) into This JobMatixmain..
'==               So as to drop setup as separate exe.. (NOT YET)--
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

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
'==               And CreditNote balance On Sale Screen ..
'==                AND  clsDebtors ("mbLoadCreditNoteBalances")..
'==          IF Payment record is a REVERSAL, then "creditNotePaymentCredited" and "creditNoteAmountDebited" need reversing.
'==   -- DONE --
'==
'==  (b)  POS-  Fix DivideByZero Runtime Error in Sales Invoice Report.
'==   -- DONE --
'==
'==  (c) POS Sales Commit-  Warn if transaction is taking in more than $500.00 into Credit Note.
'==
'==     Updated- 10-Oct-2020..
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = = = = == 

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
'==
'==  B. For JobMatix42Main-  Incorporate  Setup..
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
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
'==
'==     -- ALSO- Fix Terminal_id missing in clsCshSaleReversal, clsAccountReversal, 
'==                     and frmShowPayment (for Payment Reversal.).
'==
'==
'==  B. For POS Sales Reports-  Incorporate  Dropdown to select Staff Sales...
'==
'==
'==  C. For JobMatix42Main-  Incorporate Setup..
'==            So JobMatix42.exe is call from the SFX utility with /setup command line parm. 
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
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
'== UPDATES to Build 4284.1124  
'==
'==   Target-New-Build-4287 --  
'==      (28-Jan-2021)
'==
'==  (A) Goods Received Serials entry...  (Stewart 27-Jan-2021-)
'==     The system won't let you put in a Serial that's already in the system, 
'==     Or that you've already entered for that Invoice line.. 
'==     But it will Let you enter a Serial that was entered In a previous line In that invoice.. 
'==      (Does this mean you are trying To enter the same product twice In the one invoice ?)
'==           For the next release-  make sure that serial no's are unique over the whole Invoice, 
'==                And still also not on file already in the system. 
'==
'== (B) POS Customer Admin-  Customer data grid...  Jerami-28-Jan-2021-
'==      ADD tick box (exclude inactive) For customer search..
'==        Fix Browser Search functions..
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==   Target-New-Build-6201 --  (09-June-2021)
'==   Target-New-Build-6201 --  (09-June-2021)
'==   Target-New-Build-6201 --  (09-June-2021)
'==
'==
'==  For JobMatix62Main- OPEN SOURCE version...
'==  For JobMatix62Main- OPEN SOURCE version...
'==  For JobMatix62Main- OPEN SOURCE version...
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

'==
'== Target-New-Build-6201 --  (18-June-2021)
'==   Remove all End User Licencing Code for Open Source..
'==
'== Target-New-Build-6201 --  (15-July-2021) for Open Source..
'=   In JobTracking..
'==    -- Add CheckBox "Print Item Barcodes" to JobMaint Form (print section).
'==    --  In clsPrintDocs, print the item barcode list only if requested.
'= = = =
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


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

'==  JobMatix42 (Main)-  With Incorporated Setup..
'==  JobMatix42 (Main)-  With Incorporated Setup..

'==  No more setup now.. is open source..)
'==  JobMatix62 (Main)-  With NO Setup..  (use xcopy.)

<Assembly: AssemblyVersion("6.2.6201.0718")>
'= <Assembly: AssemblyFileVersion("1.0.0.0")> 
'==
'= = = = = = = = = = = == = = = = == = = = = = = = = = = = = = = == = = = = = = =
