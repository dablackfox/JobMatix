Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

' General Information about an assembly is controlled through the following
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.


' TODO: Review the values of the assembly attributes


<Assembly: AssemblyTitle("JobMatix62 Advanced POS/Job/RMA Tracking")>
<Assembly: AssemblyDescription("JobMatix JobTracking Functions Library")>
<Assembly: AssemblyCompany("grh")>
<Assembly: AssemblyProduct("JMxJT620")>
<Assembly: AssemblyCopyright("Copyright © 2014..2019, 2020, 2021 grhaas@outlook.com")>
<Assembly: AssemblyTrademark("JobMatix")>
<Assembly: AssemblyCulture("")>

'==
'== FROM- 3107.1007= 
'==  ALL updates to build Summarized here...
'==    and not in main form code==
'====
'==
'==   grh  3.1.3107.1007 -  07-Oct-2015 
'==     >> clsRetailHost.vb: REMOVE DUPLICATE  "mColPrefsSupplier.Add("supplier")" 
'==                     (was crashing Browser)-
'==     >> 3107.1012-  frmQuoteJobs.  Adding CheckBox + code 
'==               to allow user to build ONE job even if no motherboard.
'==
'==     >> 3107.1013-  For Gavin. Notify Cust. (SMS) 
'==            Allow user to insert Cust Name in SMS.. 
'==             (keywords &&TITLE, &&FIRSTNAME, &&LASTNAME)
'==
'==     >> 3107.1015-  clsRetailHostJMPOS-  fix to translate Supplier to RM col.names- 
'==              AND fixes to JobMaint for Quotes.
'==              And updates to WhatsNew HTML file..
'==
'==     >> 3107.1213-  13-Dec-2015=
'==               (1) Fixes to frmStaffSignOn to report blank Staff DocketName.
'==           And (2) fixes to frmNewJob to explain mandatory UserName/password partnership...
'==           ANd (3)  gsJobMatixLocalDataDir()  fix to ADD CurrentUser as SubDirectory.- 
'==                       (needed for Gavin for Terminal Services aka RDS operation.)
'==           And (4) fixes to frmNotifyCust22/Load 
'==              to set smskeys combo selectedIndex to -1 (Not selected)... 
'==                 So User is forced to select a message. This ensures SMS Injection.
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

'==  NEW VERSION 3.2.3203-  16-Dec-2015=
'==  NEW VERSION 3.2.3203-  16-Dec-2015=
'==  
'==   Add Attachments Table and CLASS plus frmAttachments -
'==     To collect docs/Pics for Jobs AND RA's ..---
'==  >> Add Attachments Panel to NewRAs and JobMaint Forms.
'==
'==  >> 30Dec2015-  NewRA and NewJob can add image when New Job/Ra is INSERTED..
'==  >> 01Jan2016-  NewRA and NewJob can also PRINT image on Main Printouts..
'==  >> 07Jan2016-  NewJob32 now has Combos to choose (set) all three PRINTER targets.
'==  >> 09Jan2016-  Separate Attachments Tables for JOBS ans RAs..
'==  >> 12Jan2016-  JobMaint32 now has Combos to choose (set) the TWO PRINTER targets.
'==  >> 16Jan2016-  JobMaint32 Extra PrintOp to print Customer Report with service Details...
'==
'==  >> 17Jan2016-  Business Email address box added to Job Setup...
'==  >> 17Jan2016-  New Job Designation "System under Warranty"..
'==                     and Overhauling all Ticket Colours..
'==
'==  >> 23Jan2016-  AcceptJob/BookJob decision now made in NewJob Form..
'==  >> 24Jan2016-  Main JobsTree- 
'==                    Added "Send back to Input Queue" to TreeView Context menu...
'==                      and JobDetail Action Toolbar.
'==  >> 25Jan2016-  Maint Update. SessionTime now has two combos- Hours and tenths.- 
'==                      And CheckBox to override Min.Charge.
'==
'==  >> 11Feb2016-  3203.211- 
'==                -- GoodsIncare Entry Form- 
'==                     (SubClass DataGridView to change ENTER key to TAB)..
'==                -- JobMatix Setup Form- ADD Checkbox [Do not enforce MinCharge].
'==                         ( Default is unchecked =No)
'==                -- JobMatix Jobs Tree. Lose the ATTEMTION icon..
'==                -- JobMatix Jobs Tree. Attention Jobs now Dark-BLUE..
'==                -- Queued Job can go back to Wait-listed.
'==
'==  3203.212- 12Feb2016- >>- New RA- Now can print "Job" (RA) Labels to stick on RA Item.
'==                       >>-  New RA-  add button to refresh Supplier ADDRESS/Phone.
'==
'==  3203.218- 18Feb2016- >>- FIX- Customer Report was going to the Receipt printer
'==                       >>-     Now sending it to A4 Colour printer.
'==             >>- 3203.219- Add "Clear Alert"to Job Context menu..
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==  NEW VERSION 3.3.3311-  24-Feb-2016=
'==  NEW VERSION 3.3.3311-  24-Feb-2016=
'==
'==    1. - Update Attachments to include MS Word and Excel documents -
'==    2. - (REVERSED below) Split RAs back into separate EXE.. (See RAs project for udtaes).
'==    3. - ON-SITE Jobs. >> Server-side notifies (SMS) staff at x-am (start of business)..
'==                               AND x-mins before time..
'==           (NO MORE)-  NEEDS JobMatix startup to be Windows Scheduled Task for am..
'==    4. - ON-SITE Jobs. >> Client-side notifies staff at Signon.)..
'==
'==  3311.302- 02Mar2016-
'==
'==    5. - Update clsRetailHost and cls-JMPOS (mbGetSerialInfo) -
'==            - to add SALE/Customer Info (if any) to SerialInfo for caller.
'==    6.  - Update to frmNewPart-
'==        >> (mbGetSerialInfo) - Show (popup) SALE/Customer Info (if any) for SerialInfo.
'==        >>  Catch ENTER key on text search text..
'==        >>  Stock Lookup button now pulls up Full Stock Browse form..
'==
'==  grh. 3311.311- 07/11Mar2016- 
'==         >>- Migration-  Importing SerialAuditTrail-
'==                must Import RM Sales-Cust Info also..
'==        >>-  RA's Main Integrated into JobMatix Main Form      
'==        >>-  RA's Suppliers Grid to choose RAs via supplier.      
'==
'==  grh. 3311.327- 271Mar2016- 
'==        >>- RA's- Revamped NewRA- replace sstab1 with one panel and numbered Steps.-
'==    
'==        >>   '=3311.329=  New Labour Rates Stuff.
'==--            -- Labour Rates now in MYOB/POS (Service Cat.)..
'==--            -- In Setup, WE keep track of the stock barcodes..
'==                    (SystemInfo:  'labourRateP1RetailBarcode'  etc.)
'==       = 3311.329=
'==             >> ON-SITE SMS reminders sent.
'==  grh 3311.410/411-
'==        >>- Now referencing JMxPOS330.dll..
'== 
'==  grh 3311.420/422/423-
'==  ----------------------
'==        >>- Fixes to Bugs in 3.2-
'==               1. Gavin-  Change Username/Password won't save (not enabled).. (FIXED)-
'==               2. Jobs Treeview. Delivered Jobs- Context menu item "View.Record"- no execution. (FIXED)
'==        >>- Fixes to 3311.411-
'==               RA's- 1. Print Group Label in Loop. ("ReceiptPrinter not specified")..(FIXED)-
'==                     2. New RA won't proceed if no SerialNo. (FIXED)-
'==                     3. New RA- Update Suppl.Addr Button needs to be disabled until RA saved. (FIXED)-
'==                     4. New RA.  freezes if JobNo focussed/clicked. ? (FIXED)-
'==        >>- NEW- 
'==           (a) (NOT IMPLEMENTED) Suspended Jobs..  
'==                    Warning if not updated for "SuspendedJobsMaxDays" days..(NOT IMPLEMENTED).
'==           (b) JobUpdate (Maint) If no Tech owner, first Update Auto claimed for Updater. (DONE)-
'==                  ( "AutoAssignOrphanJobsOnUpdate") )
'==
'==  grh 3311.507-
'==  ----------------------
'==        >>- Fix to New RA's Form..  Replacement SerNo is optional..
'==                   and if any, is recorded in RA ResultComment as "serno=..." 
'==
'==        >>- Fixes to Bug in Printing Job Service Report- 
'==                   (was overflowing item barcodes at bottom of page)
'==        >> Add "Sale" RoadioButton to POS Sale Tab for updated POS dll.
'==        >>  3311.511  Add TimePromised to Staff ON-SITE SMS..
'== = = = = = = = = = = =  = = = = = = = = = = = = = = = = = = = = =
'-
'== New Release-
'==
'==  -- 3311.708- 08July2016-
'==          >> Update SALE frame for re-designed POS Item Entry via Textboxes..
'==              (No more editing in Sale Items Grid..)
'==
'==  -- 3311.710- 10July2016-
'==        >> Fixes to Main CustomerGrid/Cust-Jobs Listview-
'==                 (Making DblClick work to View Job Service Record..)
'==
'==  -- 3311.731- 31July2016-
'==         >>  Adding THREE new SMS Gateways.. "smsBroadcast", "smsGlobal" and "directSMS"..
'==         >>  Fixes to smsReminders class for POS staff...
'==         >>  Added Job Status "43-InProcessQA" for Job in use in QA..
'==                 (so that it stll appears in QA sub-tree in the Jobs Tree.)
'==  -- 3311.0802- 02Aug2016-
'==         >>  Added Job Status "23-InProcessSUSP" for SUSPENDED Job..
'==                 (so that it stll appears in SUSPENDED sub-tree in the Jobs Tree.)
'==         >>-- 3311.0803-  Main Screen SMS Reminder msgs to go RED if errors...
'==  -- 3311.0817- 17Aug2016-
'==         >>-- 3311.0817/819-   Fixed 'gbReformatJobCustomerName' for updating in TRANSACTION !
'==         >>--  JobMatix Main. Don't refresh JobsGrid after doing stuff !!!..
'==                 AND  Don't refresh Jobs TreeView if we're not in Treeview Frame..
'==                 AND reset Staff Timer to full tank after any activity. 
'==                 AND Disable Search/Clear Grid Buttons during search. (stops multiple hits).
'==         >>-- Also, a column sort feature has been added to the NewRA form (Supplier Invoices ListView)..
'==         >>-- Also, Fix JobMaint Update-  (33- Job in use)-
'==                When forcing release, InProcess Status was NOT being changed back to Started..
'==  -- 3311.0831- 31Aug2016-
'==         >>-- Fixes to JobMatix Main- 
'==                  A) JobDetails priority Combo was overpopulated.
'==                 ALSO- B) Fix to stop SecurityId TEST popup msg onStartup-
'==                   And C) Checking JOB-DETAIL refresh after Job Update etc..
'==                    And D) Add Link to JobReports33.exe (.Net).. if installed.
'==                    And E) Fix "Find" label size in Cust. Grid Panel (courtesy Gavin).
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==  NEW BUILD 3323 --
'==
'==  -- 3323.1111- 11-Nov-2016=-
'==         >>-- NEW JobsTree Context menu Option-- 
'==             ( Can TRANSFER Job to different Customer..)-
'==         >>--  Fix to clsRetailHostJMPOS for GetSerials SQL Error..
'==
'==       RELEASED to Precise PCs ONLY..
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = 
'==
'==   ANOTHER NEW BUILD 3327.0117 --
'==   ANOTHER NEW BUILD 3327.0117 --
'==
'==  -- 3327.0117- 17-Jan-2017-
'==         >>-- Update to go with Updated POS 3303.-- 
'==               (With Cashup and Improved Reports)...
'==         >>  Also fixing Alt-F4 escape from Sign-on..
'==         >>  Also Fix to frmNewPart ( getting quantity combo value)-
'==
'==  -- 3327.0121- 21-Jan-2017-
'==         >>--includes Updated POS to fix frmImport for Staff Columns..-- 
'==         >>-- Fixes SQL ERROR in clsStaffReminders SMS update (INSERT not escaping apostrophes.)..-- 
'==         >>-- Fixes ERROR in clsRAsMain33 SQL error when NO RAs on file. (empty list)..-- 
'==         >>-- Fix to frmNewJob- Add static var lock on cmdFinish to stop re-entry-..-- 
'==         >>-- TABLE RAItems-  Expand column "RA_Symptoms" from 240 to 511 chars-..--
'==                  and user must now TAB qway from Symptoms textbox.
'==         >>-- Fixes frmNewRA31 for txtSymptoms "ENTER" key behaviour...-- 
'==
'==     THIS IS now for GENERAL RELEASE..
'==      (Released 30-Jan-2017--
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==
'==   AND STILL
'==        ANOTHER NEW BUILD 3341.0205 --
'==   AND STILL ANOTHER NEW BUILD 3341.0205 --
'==  
'==
'==  -- 3357.0205- 05-Feb-2017-
'==         >>-- Update to go with Updated POS 3307==-- 
'==             (SupplierReturns Update Function to be called from RAs.)...
'==         >>--RAs-  Add column RM_SerialAudit_id" "to RAItems Table ==-- 
'==               and Expand RM_ItemBarcode to 40 chars.
'==         >>--RAs-  IF Retail System is JobMatix POS, then call PO-GoodsReturned when Goods Sent. ==-- 
'==
'==     v33.3.3357.0219/0223 =
'==      >> Handle the POS txtSaleItemBarcode VALIDATED Event for all Line Item textboxes..
'==      >> txtSaleItemBarcode_TextChanged Event now captured for ItemBarcode ONLY--
'==      >> 23Feb2017= JobMaint- Update--  Timestamp a ServiceNote and save for any status change
'==               eg.  back to bench from QA.. -
'==      >> 23Feb2017= main Screen Job Details- ESCAPE backslash chars for RichText Box.
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==  NEW VERSION 3.4.3401-  15-Mar-2017=
'==    NEW VERSION 3.4.3401-  15-Mar-2017=
'==      NEW VERSION 3.4.3401-  15-Mar-2017=
'==
'==    1. - Re-design main form (POS Panel) and fix startup Code for POS Multi-Sale presentation. -
'==    2. -3401.0411 Updates- Replace Prev.Inv. RadioButtons with Buttons. -
'==       - Replace Trans.Selection  RadioButtons with Combo DropDpwn.
'==    3. -3401.0415 Backup Function updated and moved to "modCreateJobs" module. . -
'==       - Recognition of Thin Clients..
'==    4. -3401.0417 Fix clsSystemInfo to update DateUpdated when updating..-
'==
'==   v3.4.3401.0424 -- 24Apr2017= Extra fix-
'==         -- frmSetupJobsDB3..-  New MYOB-RM users must have "-jobtracking" DB name..
'==                  AND NO POS tables will be included in DB..
'==         -- frmMigration..  New db name n(x_jmpos) set up is now fixed (user can't change)..     
'==
'==
'==     THIS IS now on GENERAL RELEASE.. v3.4.3401.0424 -
'==      (Released 24-Apr-2017--
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==  New Build 3403 24-May-2017=
'==  New Build 3403 24-May-2017=
'==
'==
'==   v3.4.3403.0524 -- 24may2017= x-
'==         -- Main Form updates for POS Laybys..
'==   v3.4.3403.0531 -- 31may2017= x-
'==         -- Fixes to (frmNotifyCust) SendMail to capture InvalidCertificate Callback...
'==         -- ADD to (frmSMSUpdate) panel for EXCHANGE-SERVER EWS mailbox and password...
'==         --  ALSO ON-SITE Jobs can post/Update appointment to Exchange Calendar if defined....
'==         -- 3403.0607-- 07Jun2017=  JobMaint- Show Part CostPrice tagged onto Listview Description-
'==         -- 3403.0608-  clsRAsMain-  Refresh Supplier RAs Listview after "Sending".
'==         --    ""  JobMaint- FIX date format in ALERT Work Notes Message.-
'==         --  3403.0609- JobActionReturnToQueue- Ask if Job is to be UN-ASSIGNED..
'==         --  3403.0611- New RA- Extract and show the Item Sales InvoiceNo. and SaleDate...
'==                  AND- Add Info to RA Printout.. AND recover this Sales Info when re-editing the RA.  
'==
'==   v3.4.3403.0627 -- 27Jun2017= - Tidy UP for relaease.
'==         -- Fixes to (RAS's) to Prompt0....
'==         -- various fixes..
'==   v3.4.3403.0629 -- 29Jun2017= - FIX UP for release.
'==         -- REVERSE this- JobMaint- CostPrice tagged onto Listview Description-
'==         --    (make new listView column for Cost-Price)..      
'==         -- ADD Customer Company and CustomerAddress/Phone to ON-SITE SMS..
'==   v3.4.3403.0711 -- 11Jul2017= - FIX UP for release.
'==         -- Fix RA's main- problem with ViewRA showing the wrong RA when switching TABs...-
'==         -- For NEW RA's..  Save request notes text if any...
'==         -- ALSO has Latest POS updates/fixes....
'==         --  3403.719  Add event for POS Item Line CLEAR..
'==   v3.4.3403.0801 -- 01Aug2017= - 
'==         --  3403.801  Re-arrange POS HOLD/Restore buttons...
'==
'== ---- POSTED on website.  ---
'==
'==  Build REVISED for fixes...
'==
'==   v3.4.3403.0909 -- 09Sep2017= - 
'==      -- Fix JobMaint for QUOTE Parts check to access correct (changed) listviewParts Stock_id Col...
'==      -- JobMatix main- (or whereever) Fix Job Details not including New Second Hand parts price in total.
'==      -- Backup- DELETE source BAK after successful copy (if OK by user).  
'==
'==  Build REVISED AGAIN for fixes...
'==
'==   v3.4.3403.0919 -- 19Sep2017= - 
'==      -- Just for Fixes to POS Sales-Invoice Report, and Adding POS Customer pricing Grades....
'== 
'==   == THIS now upgraded to Build 3411---.
'==   == THIS now upgraded to Build 3411--
'==      -- 21 -Dec-2017=
'==   >> 3411.1221 -- Form frmPOS34Main (Sale form) was moved into MxPOS340 dll assembly .
'==                --  and is so compiled into POS349ex EXE to be launched from JobTracking.
'==                --  SO- all POS stuff is stripped from JobMatix- 
'==                      and we have have POS Button JmxPOS349ex EXE can be launched from JobTracking.
'==
'==   >> 3411.0107 -- 07Jan2018- Udated for POS.. 
'==        --  Diagnosis column is used for POS JobReport on Job Sale Invoice.
'==   >> 3411.0113 -- 13Jan2018- Updated for POS.. 
'==        --  Fixes.. NotifyCust. SEND SMS -- Re-instate the Update Job call.. for SMS SEnt ok..
'==
'==   >> 3411.0118 -- 18Jan2018-  
'==        -- Another attempt to move RA's to it's own Process/EXE. 
'==              (Separate out the clsRetailHost Interface)-
'==        -- 3411.0121  DROP all the RAs Tan Panel..
'==        --   and Add Button to Launch RAs .exe..
'==        -- 3411.0129  
'==        --   Update for release...
'==
'==--     (3411.0129 Was released to prcise..)
'==--     (3411.0129 Was released to prcise..)
'==--     (3411.0129 Was released to prcise..)
'==
'==   >> 3411.0212=  12/17-Feb-2018= Updated Import Form...
'==          -- frmIMPORT from RM- UPDATE columns, Set Package Qty's tp Zero, and Add Layby Qty to in-stock.. 
'==          -- frmStartup- Added Tab Control for New/Old users... 
'==          -- 3411.0217-Create new DB (Setup)
'==                 Make it optional to set up the Goods/Tasks etc as per computer Shop.... 
'==          -- 3411.0217-JobTracking- (If POS used, show NewCustomer Button in Customer Srch Frame.
'== 
'==--     (3411.0217 Was released to Precise..)
'==--     (3411.0217 Was released to Precise..)
'==--     (3411.0217 Was released to Precise..)
'==
'==   >> 3411.0228=  26/27/28-Feb-2018= Dropping SignOn Form....
'==          -- SignOff/SignOn now on Main Page. 
'==
'==--     (3411.0228 Was released to Precise..)
'==--     (3411.0228 Was released to Precise..)
'==
'==   >> 3411.0302=  02-mar-2018= ..
'==         -- Remember local user's sigin-off Timeout preference.
'==         -- Reset staff timeout on keyboard/mouse activity.. 
'==         -- clsBrowse34 (ex POS) now replacing clsBrowse3.. 
'==
'==
'==-- (3411.0305 Was released to Precise..)
'==-- (3411.0305 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - 
'==
'==   >> 3411.0314=  14-mar-2018= ..
'==         --Use "GetLastInputInfo". (User32.dll) to measure Idle Time..
'==
'==-- (3411.0314 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - 
'==   >> 3411.0411=  11/13-Apr-2018= ..
'==        -- Re-do design of "mbRefreshJobsTreeView". to speed up.
'==        -- use MyTreeView for tvwJobs..
'==        -- Re-design Jobs Tree rebuild algorithm.   
'==              (Rebuild completely every time.. and Now only three months of cancelled jobs.)-
'==
'==-- (3411.0417 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - 
'==
'==   >> 3411.0423=  23-Apr-2018= ..
'==        -- Update just for POS Purchase-Orders changes..
'==           And for fix to POS Goods Returned function...
'==
'==-- (3411.0423 Was released to Precise..)
'==-- (3411.0423 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - - - - - - -- - 
'==
'==  NEW BUILD-
'==
'==    >> 3431.0427=  27/28/30-April-2018..
'==     -- FIX ALL FORMS to replace "msgbox"  with .Net "MessageBox"..
'==     -- FIX ALL FORMS to MOVE "Activated" event stuff to "Shown" event..
'==     -- Convert MAIN Startup to "Sub Main".. (Trying to fix JobsBrowser Col.Hdr Failed sort-)
'==     -- 3431.0501- Fix Browser transparency issue on Main Form.
'==     -- 3431.0501- No update/but updated on frmMaint..
'==     -- 3431.0503- New Job/Amend- Send Exchange calendar update to DISK BG Queue..
'==          And send FileName back to main Form- 
'==          And on main Form-  Add Exchange Update BG-Worker thread to do Exchange Work..
'==     -- 3431.0512- maint. Update Job.. 
'==                On Saving updates, log all Item movements to Service Notes...
'==     -- 3431.0514-
'==           Increase [Jobs] ServiceNotes width to MAX..
'==
'==-- (3431.0515 Was released to Precise..)
'==-- (3431.0515 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - - - - - - -- - 
'==
'==    >> 3431.0523- 23-may-2018=
'==       --  Add Create Trigger for ALTER_TABLE..
'==            This trigger is to detect previous builds of JobMatix -
'---                  - from restoring old columns widths (4000) for the columns-
'==             ProblemLong, ServiceNotes or SessionTimes or Notifications..
'==       --  Fix Exchange BG Worker to check for .\temp Directory before reading it..
'==       --  3431.0527-  Fix Amend crash when no LabourRates/Descriptions set up
'==                           Revert to legacy descriptors...
'==       --  3431.0527-  Add PREVIOUS JobNo to NewJob Problem text if needed...

'==-- (3431.0527 Was released to Precise..)
'== - - - - - - - - - - - - - - - - - - -- - - - - - - -- - 
'==
'==
'==  DLL version of JobTracking. 10-June-2018=
'==  DLL version of JobTracking. 10-June-2018=
'==  DLL version of JobTracking. 10-June-2018=
'==
'==    3501.0610 -- frmNotifyCust22-  
'==       --  Some event form references removed For JobTracking now being a DLL..
'==       --  Create New Module modAllFileAndSqlSubs
'==           to combine All fileSupport and sql support Functions.
'==            (Take all content from and obsoletes modFileSupport33, modSqlSupport31, and modSqlInfoSchema31.)
'==    3501.0617 --  
'==       --  frmQuoteJob3:  Move Activated to Shown..
'==       --  On-site Jobs Panel.. Add Context menu to update Job etc....
'==       --  On-site Jobs Panel.. Order Jobs Date DESCENDING....
'==
'==    3501.0625 25-June-2018= (ported from 3431.0622- )
'==       --  Exchange BG task- Detect invalid XML file data eg. reserved chars (eg Amoersand etc.)...
'==       --  NewJob/Amend- Fix (cleanup) XML file data for reserved chars (eg Ampersand etc.)...
'==       --  Deliver Job from JobsTree Context menu..  Add Option to Expedite mark as Delivered..
'==       --  Fixes to modAllFileAndSqlSubs to Get correct appname for LocalDataDir..
'==
'==    3501.0708 08-July-2018= (Updates from 3431.0707- )
'==       -- Exchange201 Updates from 3431.0707-
'==
'==    3501.0713 13-July-2018= (Updates from 3431.0712- )
'==       -- frmMigration and frmImportRM Updates from 3431.0712-
'==       -- Updated Reports Launch Function to pass across Labour Rates-
'==       -- 3501.714  Separate out backup/Restore into separate Module.
'==    3501.0724 24-July-2018= 
'==       -- "NewInJobMatix35.htm" is now picked at runtime from Runtime Directory.-
'==
'==    3501.0806 06-August-2018= 
'==       -- Fixes to modAllFileAndSqlSubs for Speeding up getSchema at startup...
'==
'==
'==    3501.0812 12-August-2018= 
'==       -- Updated revision no...
'==
'== -- Updated 3501.0814  14August2018=  
'==     -- Fix clsJMxPOS31 to separate out function CreatePosTables into clsJMxCreatePOS...
'==     -- Amending Job: Make entry into ServiceNotes if GoodsIn Care changed. ...
'==
'== -- Updated 3501.1001  01-Oct-2018=  
'==     -- Fix to clsExchange20 (GetAppointmentsForDate) to check for NOTHING returned....
'==     -- Fix to frmJobMaint32 to print correct Tech on Service Record (Not N/A !)....
'== = == =
'==
'== -- Updated 3501.1105  05/07/08-Nov-2018=  
'==     -- (a)  DB RESTORE-  make sure both db-names "_jobtracking" and "_Jmpos" are accepted.
'==     -- (b)  IMPORT latest browser form frmBrowse33 and  clsBrowse34 class from POS latest.
'==                AND tidy up clsBrowse34 to work properly with JobTracking Aliased Pref.Cols.
'==                   AND Fix frmBrowse33 to take account of Aliased flds for the Search-columns array.
'==     -- (c)  New Job Form (ON-SITE Jobs.)  Add  a numericUpDown control to select Job Duration in hours. 
'==            ALSO, then add this value as Meeting Duration in Exchange Calendar update.
'==     -- (d)  New Job Form-  QUESTION Yes/No entry for Charger/PwerSupply included or not with Goods.
'==     -- (e)  IMPORT latest browser clsBrowse34 class AGAIN from POS latest.
'==     --  3501.1108.  More on clsBrowse34.vb and frmBrowse33..
'==
'== -- Updated 3501.1223  23-Dec-2018=  
'==     -- (a)  Get Latest clsBrowse34 and File Support (modAllFileAndSqlSubs)  from POS..
'==     -- (b)  Fix ONSTTE Duration  Label on New Job Form..
'==     -- (c)  Add Date promised to ON-SITE Customer Docket..
'==
'==
'==    New Build No.- 3519.0108 08/19-Jan-2019= 
'==           (Fixes to Discovering users for JobTracking only)
'==           (Fixes to Setup Backup Path panel for better visibility)
'==
'==    Updated- 3519.0124 22/24-Jan-2019= 
'==           - Fixes to Discovering and fixing Updating Wrong Job Problem..
'==           - Now using TabControl for TreeView/OnSite/Jobs/Customer panels....
'==
'==    Updated- 3519.0129 29-Jan-2019= 
'==           - Fixes to Startup AppPath to correctly find user Biz logo...
'==           - Fixes to Print Customer Report for supporting multiple pages of images and text....
'==
'==    Updated- 3519.0414 14-April-2019= 
'==       (a)  FROM POS--     GET Updated version of frmBrowse33 (to accept User's Select-Sql)..
'==       (b)  Allow JobTracking to deliver Job directiy if no value, and user confirms.
'==       (c) Extra 'Slashes' may appear in User Passwords in Job Record.. 
'==                Detect these in the Job Amend Shown event, and reset password fields on the new Job Form.  
'==               ie. In the code below, if extra slashes gives more users or passwords then there are text boxes, 
'==                     then clear all users/passwords and tell user to enter then again.   
'==                  ALSO, check for them in the User/Password textbox validations
'==
'==    Updated- 4201.0627 27-June-2019= 
'==       Just FOR NEW POS version 4.2..
'==
'==    Updated- 4201.0717 16-July-2019= 
'==       >> Job Search DataGridView.. ALSO Customer SearchGrid/Jobs Listview-  
'==            Add Right-click context menu for Job Actions, as per Active Jobs Tree treeview.
'==
'==    Updated- 4201.0727 27-July-2019= 
'==     --  Fix to Timer/activity sensor to stop immediate timing out after staff sign-on
'==
'==    Updated- 4201.1007-  07-October-2019= 
'==     --  Fix to the Job-Amendment start-up to Fix problem (AGAIN) about
'==              Extra 'Slashes' that appear in User Passwords in Job Record.
'==               (Do not clear invalid passwords any more, Just show them as one user/pwd..)
'==
'==  NEW BUILD-    4219 VERSION
'==    Updated- 4219.1130 21-Nov-2019= 
'==      -- clsPrintDocs- JobMaint Printing-  Fix Printing WorkHistory for Multiple Pages.
'==      --  MAKE Forms PUBLIC- NewJobForm and Maint Form-  "frmNewJob32" and "frmJobMaint32"
'==      --  Move Retail Host Interfaces and classes to JMxRetailHost.dll for common sharing..
'==      --  Update RAs reference to call "JobMatixRAs42.exe"..
'==      --  Make "modAllFileAndSqlSubs" PUBLIC in JMxRetailHost.dll so EVERyONE can use it..
'==      --  Move  module  "modBackupRestore35" as Public into JMxRetailHost.dll so EVERyONE can use it..
'==      --  Move module "modCreateJobs3" and Attachments Form and class 
'==                        into JMxRetailHost.dll so EVERyONE  (RAs) can use it.
'==      -- JobMatixMain- Drop Error Popup "There is no Job No or details selected." 
'==                 Happens when DoubleClicking on Tree Section. 
'==      --  Fixes to Setup form to fix visibility of labels on Backup Path...
'==
'==  NEW REVISION  4219.1214 VERSION started 08-Dec-2019=
'==    Updated- 4219.1216 08-Dec-2019= 
'==      --  MAKE Form "frmNewJob32" INTO USERCONTROL.
'==      --  MAKE NEW Form "frmNewJobBase" INTO USERCONTROL.
'--            SO THAT we show it as a Child in a POS TAB..
'==              From JobTracking we call frmNewJobBase, which is container for the UserControl.
'==      --  Updates to dlgNoShow for AppName..  Can be called from POS also..
'==      --  Add a new Timer to Main Form to check for Exchange-Calendar xml files that might have come from POS...
'==               -- If there is any, and BG-Worker exchange is nor running, the Run It...
'==
'==  NEW Build 4221.0207  '- 07Feb2020.
'==        -- To Show Customer Tags on Main Form..
'==
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
'==
'== Fixes to Build 4221.0207  
'==
'==   Target-New-Build 4262 -- (Started 12-Aug-2020)
'==   Target-New-Build 4262 -- (Started 12-Aug-2020)
'==
'==   Target-New-Build-4262 -- (Started 12-Aug-2020)
'==   Target-New-Build-4262 -- (Started 12-Aug-2020)
'==
'==      --  MAKE Form "frmJobMaint32" INTO USERCONTROL- ucChildJobMaint..
'==      --  MAKE NEW Form "frmJobMaintBase" to hold USERCONTROL.
'--            SO THAT we show it as a Child in a POS TAB..
'==              From JobTracking we call frmJobMaintBase, which is container for the UserControl.
'==      --  In Main Form- BG Worker Exchange201-.   see modOnsiteCalendar.vb   
'==             ie We need to put in more try/catch code into the Function mbFindJobAppointment  
'==                to catch Karens's string crash (Martin email 12-Aug-2020/)
'==
'==   --  In Main Form, cmdStopPress_click need to be updated..   
'==       1. The UPDATE statement clause
'==            sSql = sSql & " ServiceNotes=RIGHT(ServiceNotes+'" & sNewNote & "',4000) "
'==            Needs to be changed to this: (because ServiceNotes is now varchar(max).)
'==                  sSql = sSql & " ServiceNotes=(ServiceNotes+'" & sNewNote & "') "
'==       2. The procedure should check for external updates AFTER getting msg text from 	UI,
'==                	and inside a transaction..
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = == = = = = = = 
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
'==
'==
'== Target-build-4267  (Started 07-Sep-2020)
'== Target-build-4267  (Started 07-Sep-2020)
'== Target-build-4267  (Started 07-Sep-2020)
'==
'==  --Main- Exchange201-  Completion of BG-worker..  
'==      NEW module "modMyMsgBox" with Function to create a Form on the fly with textbox to show accumulated Exchange results 
'==           (.Net MessageBox is not suitable for exended message..)   
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
'==
'== Target-Build-4284  (Re-Started 23-Nov-2020)
'== Target-Build-4284  (Re-Started 23-Nov-2020)
'== Target-Build-4284  (Re-Started 23-Nov-2020)
'==
'==   A. -- Bring JobReports into JobTracking Main as a UserControl..
'==
'==   B. -- Cancel Job (frmNewJob etc)..  Update Calendar to delete the Appointment..
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
'==
'==   Target-New-Build-6201 --  (16-June-2021)
'==   Target-New-Build-6201 --  (16-June-2021)
'==
'==  For JobMatix62Main- OPEN SOURCE version...
'==  For JobMatix62Main- OPEN SOURCE version...
'==
'== Target-New-Build-6201 --  (18-June-2021)
'==   Remove all End User Licencing Code for Open Source..
'==
'== Target-New-Build-6201 --  (15-July-2021) for Open Source..
'==    -- Add CheckBox "Print Item Barcodes" to JobMaint Form (print section).
'==    --  In clsPrintDocs, print the item barcode list only if requested.
'= = = =
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

' Version information for an assembly consists of the following four values:

'	Major version
'	Minor Version
'	Build Number
'	Revision
'=WAS  =<Assembly: AssemblyProduct("JMxJT420")> 

'==<Assembly: AssemblyProduct("JMxJT620")> 

' You can specify all the values or you can default the Build and Revision Numbers
' by using the '*' as shown below:

<Assembly: AssemblyVersion("6.2.6201.0718")>

'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


