Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms.ListViewItem.ListViewSubItem
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections.Generic

Public Class ucChildJobMaint
    Inherits System.Windows.Forms.UserControl   '== .Form

    '= = = = = = = = = = = = =

    '--Precise Job  M A I N T   Form.---

    '--- FORM used BOTH for:
    '--    Updating service details, AND  --
    '----  Delivering completed job..--
    '= = = = = = = = = = = = = = = = = = = = = MessageBox.Show(Me,

    '==-- =09-Sep-2009=  Revised Transaction Processing and updating..==
    '---     1) - No Transaction to be active during User Input..-- (SQL lock escalation problem)..-
    '---     2) - Job record "locked" with status "33-InProcess"  during edit.. --
    '---     3) - Changes to Tasks/Parts stored on SCRATCHPAD as they are input by user.. --
    '---     3) - Changes to Tasks/Parts Committed only at SAVE/EXIT udate.. --
    '---            ie SAVE-> BeginTrans,
    '---                        Apply scratchpad changes to Tasks/Parts tables,
    '---                        Update Job record,
    '---                      CommitTrans.         -----
    '= = = =  =

    '==-- =18-Jan-2010=  Added PartSerialNumber to all JobPart stuff (ex V1.4)..==
    '==-- =21-Jan-2010=  View only.. Allow ESC to cancel....==
    '==-- =30-Mar-2010=  Printing Barcodes:  Separate into Wty/NonWty..-...==
    '==-- =17-Apr-2010=  Add radio buttons to sessionTime [Charge/No-Charge]..-...==
    '-----             --  Add "-NC" to sessionLine on sessionTimes list..
    '-----             --  Fix labour cost calc. to  exclude No-Charge sessions.
    '==-- =27-May-2010=  VIEW- Enable Quote Checklist GRID for Viewing etc..(only mbService can modify..).==
    '==-- =29-May-2010=  Apply Labour Min.Charge if applicable..==
    '====             And- Drop Parts Costs..--
    '==-- =14-Jun-2010=  Add "Job Completed" to Notify text....==
    '==-- =15-Jul-2010=  Added State and PostCode and Phone..==
    '===- =28-Jul-2010=  Added ==  "mlItemBarcodeFontSize" for FontSize from SystemInfo.. ==
    '===- =29-Jul-2010=  Added ==  Support for SERVICE CHARGE CAT1 lookup.. ==
    '===- =11-Aug-2010=  Delete Disk $15.00 from costing... ==
    '-----      Also-  don't force session time entry..-
    '===- =28-Aug-2010=  Rev.2464..  Allow Printing of Documents on Job completion... ==
    '===- =31-Aug-2010=  Rev.2466..  Update label colour to GREEN before printing on Job completion... ==
    '===- =19-Sep-2010=  Rev.2476..  "Printed OK?" questions now default to "YES".. ==
    '===  ---       ----   ALSO:  Service Completion..  update Notifications BEFORE printing...
    '===- =04-Oct-2010=  Rev.2478..  New Form CustomerNotifications with SMS..=
    '===- =10-Oct-2010=  Rev.2478..  Part price to be updated in partsTable if cost changed...=
    '===- =13-Oct-2010=  Rev.2480..  Drop Progres Bar on printing...=
    '===- =19-Oct-2010=  Rev.2485..  Drop gbDebug=true  in Notify routine...=
    '== JOBMATIX..==
    '===- =15-Nov-2010=  Rev.2787..  Notify form needs staffname.-
    '------   --- AND don't charge min charge if no labour hours..
    '===- =11-Dec-2010=  Rev.2790..  Cmd button to SAVE and SUSPEND Job.-
    '===- =15-Dec-2010=  Rev.2790..  Cut excess CR/LF in logging service nores...-
    '===- =12-Jan-2011=  Rev.2796..  NominTech is Job owner..
    '------    -------     Add Feature make current user the owner..-
    '===- =21-Jan-2011=  Rev.2801..  Change Date dosplay format on JobTask to dd-mmm-yyyy.. (For Charlie..)--
    '----                 ------      ALSO:   set mbDataChanged for "chkMyJOb"..--
    '===- =03-Apr-2011=  Rev.2804..  Change cmdFinish Tooltiptext for Deivery..--

    '== JOBMATIX V2.2.2900==28-April-2011--==
    '--------   Service tasks and StockParts now on separate ListViewParts instances....--
    '--------   Task checklist for all Service tasks..--
    '---------  QUOTATON-  Can print receipt-docket if CHECKED..--

    '== JOBMATIX V2.2.2908==18-Jun-2011--==
    '---  "NewPart" Form ADDS "SpecialPrice" "column" to part collection. (ex MYOB "allow_renaming" )
    '---  "NewPart" Form now adds GST before returning..
    '---   New Status k_statusQA = "40-QA"..----

    '== JOBMATIX V2.2.29162918==04-Aug-2011--==
    '--  Rev-2916/2918/2919 -- Show Customer's Instructions.  (eg: QUOTATION-REQUIRED).-
    '---          AND fix printing of GoodsIncare..--
    '---          AND add printing of Latest Notification Text..--

    '== JOBMATIX V3.0.3010==08-Oct-2011--==
    '--- CustomerHistory need  mRetailHost1..--
    '--  Form printing tranferred to clsPrintDocs...
    '--=19-Nov-2011=  FOR vb.net:  NO MORE GOSUB's.-

    '== JOBMATIX V3.0.3021==27-Nov-2011--==
    '---- PREP for vb.net UPGRADE..
    '-----  Replace the Control Array of ServiceChecklist FlexGrids/txtEdit boxes -
    '---      with ONE FlexGrid and EditBox,  backed up by data arrays..
    '----    ie. a variant array of "flexgrid" (2-d) arrays...

    '== JOBMATIX V3.0.3021==29-Nov-2011--==
    '---- UPGRADED now to vb.net..

    '== JOBMATIX V3.0.3023==17-Dec-2011--==
    '---- Staff Barcode now an Input Property...

    '== JOBMATIX V3.0.3031==25-Feb-2012--==
    '--grh--=25-Feb-2012===  Replace MSHFlexGrid with dotNet "DataGridView" control...--
    '--grh--03-Mar-2012=== 
    '--        Replace COM Scripting.Dictionary  with class << clsStrDictionary >>...--
    '--grh--08-Mar-2012=== 
    '---       Send Form Top/Left props. to FrmNewPart..--
    '---   AND use Option buttons to select EXIT STRATEGY..--
    '--  ALSO:
    '--       If (ListViewTasks.Items.Count > 0) Or _
    '--         (ListViewParts(k_LV_INDEX_SERVICE).Items.Count > 0) Then '--can complete--
    '= = = = = = = = = = = = =  

    '== JOBMATIX V3.0.3031.1 ==13-Mar-2012--==
    '==   =frmJobMaint33  ==  REDUCED Footprint version  --

    '== JOBMATIX V3.0.3031.2 ==18-Mar-2012--==
    '==   Re-worked the job Save/Exit strategy  !!!   ==
    '==
    '== JOBMATIX V3.0.3031.8 ==28-Mar-2012--==
    '==   Fix: msDayOfWeek(Today)  (TRIES to set time, and crashes)..
    '--     Relplace all format(today)  with format(cdate(datetime.today)..) --
    '==
    '== JOBMATIX V3.0.3031.11 ==29-Mar-2012--==
    '==    Fix Add new task.. (DateSort column data not added..).
    '==  
    '== JOBMATIX V3.0.3041.0 ==03-Apr-2012--==
    '==    Fix POpup menu for service/parts items..
    '==      AND Fix DELETE for service/parts items..
    '==
    '== JOBMATIX V3.0.3043.0 ==03-Apr-2012--==
    '==   Cust.Email Updates for Notify...
    '==  AND Refreshed Cust. details input from MAIN..
    '== V3.0.3043.1 ==21-Apr-2012--==
    '==   Drop Changing printers..
    '==
    '== V3.0.3047.1 ==30-Apr-2012--==
    '==  Copy part serial-no..  Fix crash for empty column data..
    '==
    '==  V3.0.3049.3  03-May-2012  --  Fixes to Exit options..
    '==
    '==  V3.0.3053.0  17-May-2012  --  
    '==    = ReNumber all Tab Indexes..
    '--
    '== Build: 3059.1= 03-Jun-2012=
    '==    Restore passing the Customer_ID to history form..
    '==
    '== Build: 3061.0= 07-Jun-2012=
    '==    Fix "checkFormComplete" for empty checklist..
    '==      and incorrect TempParts index check..
    '==      and ditto for UpdateJob..-
    '==      AND update Checklist display-
    '==         on startup and when Service item selection changes..
    '==
    '== Build: 3063.0=  01Jul2012==
    '--  >> QuotesPartsStatus:  Fix checked/unchecked icons not showing..
    '==  >> Checklists:  Commit changes back to home array following change event.
    '==
    '== Build: 3067.0 == 23-Jul-2012==
    '==   >> Add HelpProvider  ==
    '==
    '=  = = = = = = = = = = = = = = = = = = = = = == = = = = = = = 
    '== 
    '==  grh. JobMatix 3.1.3101 ---  16-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider needed for Jet OleDb driver).
    '== 
    '==  grh. JobMatix 3.1.3103.0117 ---  17-Jan2015 ===
    '==        >> For JobMaint (Update)- Allow Zeroizing SessionTimes..
    '== 
    '==  grh. JobMatix 3.1.3103.423 ---  23Apr2015 ===
    '==        >>  Allow Zeroizing SessionTimes..
    '==         >>  NOW   3103.423= Anybody can do it..
    '==
    '==  grh. JobMatix 3.1.3107.518 ---  18-May-2015 ===
    '==        >>  ServiceNotes and SessionTimes cols now 4000.
    '==        >> Log to Work Notes: ** NB: Job Session Hours were reset.
    '==        >> Fix bug- current Session Hours were double added to msSessionTimesTodate..
    '==        >> Massage form/controls size/bg/fonts.. ..
    '==
    '==  grh. JobMatix 3.1.3107.611 ---  11-Jun-2015 ===
    '==        >>  mbCheckCompletion- Add mCurLabour and txtWorkDetails to things to enable optMarkAsComplete.
    '==        >>  On Completion- Query if No Tasks...
    '==
    '==  grh JobMatix 3.1.3107.1013-  13-Oct-2015  ==
    '==    >> For Gavin (LE Charlestown). Notify Cust. (SMS) 
    '==            Allow user to insert Cust Name in SMS.. (keywords &&FIRSTNAME, &&LASTNAME)
    '==    >> SO- must pass collection of RM Cust Info as input to Notify...
    '==    >>  ALSO-  3017.1017=  FIXED- "QuotePartDescription"  was showing instead of actual quote part..
    '==    >>  ALSO-  3017.1017=  FIXED- Initial quote parts wasn't showing unfixed icon.
    '==
    '==
    '==  NEW VERSION 3.2.1228.0-  28-Dec-2015=
    '==   With Attachments Table and CLASS plus frmAttachments -
    '==    >> Add new (Main) TAB Control To enclose Work Progress Tabset and Attachments main Tab---
    '==    >> 3203.110 10Jan2016- Add new CUSTOMER REPORT print Option.---
    '==    >> 3203.120 20Jan2016- Add stuff for SystemUnderWarranty, --
    '==         and Re-organise Job Ticket Format.---
    '==    >> 25Jan2016-  Maint Update. Session now has two combos- Hours and tenths.- 
    '==                      And CheckBox to override Min.Charge.
    '==
    '==
    '==    >> 11Feb2016-  3203.211- 
    '==             -- JobMatix Setup Form- ADD Checkbox [Do not enforce MinCharge].
    '==                         ( Default is unchecked =No)
    '==                  So we have: systemInfo Enforce_Minimum_Charge= Y/N  
    '==                     (Default is Y(ES) if not in systemInfo..
    '==
    '==  grh. 3203.218- 18Feb2016- 
    '==         >>- FIX- Customer Report was going to the Receipt printer
    '==                Now sending it to A4 Colour printer.
    '==
    '==  grh. 3311.225- 25Feb2016- 
    '==         >>- FIX- Use clsLocalSettings.
    '==
    '==     3311.331-
    '==        >>-  Labour prices now from Retail Host Stock..
    '==                    We just keep barcodes for P1,P2,P3...   
    '==
    '==     3311.423- (23Apr2016)= IF mClsSystemInfo.item("AutoAssignOrphanJobsOnUpdate") ="Y" --
    '==         >> Unassigned Jobs are now auto-assigned to current tech Updater-
    '==                   MsgBox("NB: This Job was previously an orphan.." & vbCrLf & vbCrLf & _
    '==                   "  It has now been assigned to you (" & msStaffName & ").. ", MsgBoxStyle.Information)
    '==
    '==  -- 3311.731- 31July2016-
    '==         >>  Added Job Status "43-InProcessQA" for Job in use in QA..
    '==                 (so that it stll appears in QA sub-tree in the Jobs Tree.)
    '==
    '==  -- 3311.819- 19-Aug-2016-
    '==         >>-- Fix JobMaint Update-  (33- Job in use)-
    '==                When forcing release, InProcess Status was NOT being changed back to Started..
    '==
    '==     v33.3.3357.0219/0223 =
    '==      >> 23Feb2017= JobMaint- Update--  Timestamp a ServiceNote and save for any status change
    '==               eg.  back to bench from QA.. -
    '==
    '==   v3.4.3403.0607 -- 07-Jun-2017= x-
    '==         -- JobMaint- Show Part CostPrice in ListView- tagged onto Description-
    '==         -- JobMaint- FIX date format in ALERT Work Notes Message.-
    '==
    '==   v3.4.3403.0628 -- 28Jun2017= - FIX UP for relaease.
    '==         -- REVSERSE this- JobMaint- CostPrice tagged onto Listview Description-
    '==         --    (make new listView column for Cost-Price)..  
    '==
    '==
    '==  REVISED for fixes..
    '==
    '==   v3.4.3403.0909 -- 09Sep2017= - 
    '==      --  Fix JobMaint for QUOTE Parts and others
    '==                   check to access correct (changed) listviewParts Stock_id Col...
    '==
    '==                      and we have have POS Button JmxPOS349ex EXE can be launched from JobTracking.
    '==
    '==   >> 3411.0107 -- 07Jan2018- Udated for POS.. 
    '==        --  Diagnosis column resized..  is now used for POS JobReport on Job Sale Invoice.
    '==
    '==
    '==-- (3411.0423 Was released to Precise..)
    '==-- (3411.0423 Was released to Precise..)
    '== - - - - - - - - - - - - - - - - - - -- - - - - - - -- - 
    '==
    '==  NEW BUILD-
    '==
    '==    >> 3431.0427=  27/28-April-2018..
    '==     -- FIX ALL FORMS to replace "msgbox"  with .Net "MessageBox"..
    '==             and Add "Me" to ShowDialog calls..
    '==     -- FIX ALL FORMS to MOVE "Activated" event stuff to "Shown" event..
    '==                    (more stable)..
    '==     -- 3431.0512- maint. Update Job.. 
    '==             On Saving updates, log all Item movements to Service Notes...
    '==             Increase Current Job Notes textbox max chars to 3000 chars...
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  DLL version of JobTracking. 10-June-2018=
    '==    JMxJT350.dll 
    '==   3501.0610 -- -  
    '==      Main form now called from the Base EXE JMxJT350Ex..
    '==       --  Removed all "End" statements.....
    '==
    '== -- Updated 3501.1001  01-Oct-2018=  
    '==     -- Fix to frmJobMaint32 Print Service Record to add Updated By Line to current texts, and 
    '==                 print correct Tech on Service Record (Not N/A !)....
    '==
    '==
    '==  NEW BUILD- 4219 VERSION
    '==    Updated- 4219.1121 21-Nov-2019= 
    '==      --  MAKE Forms PUBLIC- NewJobForm and Maint Form-  "frmNewJob32" and "frmJobMaint32"
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== Fixes to Build 4221.0207  
    '==
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==
    '==      --  MAKE Form "frmJobMaint32" INTO USERCONTROL- ucChildJobMaint..
    '==      --  MAKE NEW Form "frmJobMaintBase" to hold USERCONTROL.
    '--            SO THAT we show it as a Child in a POS TAB..
    '==              From JobTracking we call frmJobMaintBase, which is container for the UserControl.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '==
    '== Target-New-Build-6201 --  (15-July-2021) for Open Source..
    '==    -- Add CheckBox "Print Item Barcodes" to JobMaint Form (print section).
    '==    --  In clsPrintDocs, print the item barcode list only if requested.
    '= = = =
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    '==  NB  WARNING  !!
    '==  NB  WARNING  !!
    '==  NB  WARNING  !!
    '==   DO NOT USE  setCancel(TRUE) for the Cancel button.
    '==    For Update entries.. (ie Service or Delivery)..

    '===   It CANCELS the FORM !!!  =========================

    '== Const k_version As String = "frmJobMaint3.2.3203.211==11Feb2016==2:24pm="

    Private Const K_GOODS_ONSITEJOB = "ON-SITE JOB;"   '--3083.312--

    Const k_statusSuspended As String = "20-Suspended"
    Const k_statusInProcessSusp As String = "23-InProcessSusp" '=3311.0802=--locked during SUSP jobMaint..-
    Const k_statusStarted As String = "30-Started"
    Const k_statusInProcess As String = "33-InProcess" '--locked during jobMaint..-
    Const k_statusQA As String = "40-QA"
    Const k_statusInProcessQA As String = "43-InProcessQA" '=3311.731=--locked during QA jobMaint..-
    Const k_statusCompleted As String = "50-Completed"
    Const k_statusDelivered As String = "70-Delivered"

    Const k_colourDelivered As Integer = &HE0E0E0

    '==== Const k_labourPrice = 88#
    Const k_CheckPrtRecord As Short = 0
    Const k_CheckPrtDocket As Short = 1


    Const k_maxOptTime As Short = 9 '-- no of Time options.--
    Const k_maxOptNotified As Short = 3 '-- no of notify options.--
    Const k_TABPRINTPRIORITY As Short = 26
    '= = = = = = = = = = = = = = = = = =  = = =  = =
    '-- Parts or Service..--
    Private Const k_LV_INDEX_SERVICE As Short = 1 '-- which listview..-
    Private Const k_LV_INDEX_PARTS As Short = 0 '-- which listview..-
    '= = = = = =  = = =  = = ==

    '= 3403.909 ==
    Private Const k_LV_PARTS_STOCK_ID_IDX As Short = 9 '-- Stock_id Col in Parts listview..-
    '= = = = = =  = = =  = = ==

    '--  Checklist FlexGrid columns..-
    '--  col-0 is fixed row no..-
    Private Const k_GRIDCOL_STATUS_IMAGE As Short = 0
    Private Const k_GRIDCOL_ITEM As Short = 1
    Private Const k_GRIDCOL_STATUS As Short = 2
    Private Const k_GRIDCOL_COMMENTS As Short = 3
    Private Const k_GRIDCOL_DATE As Short = 4
    Private Const k_GRIDCOL_NAME As Short = 5
    Private Const k_GRIDCOL_ORIG_STATUS As Short = 6
    Private Const k_GRIDCOL_ORIG_COMMENTS As Short = 7

    '= = = = = = = = = = = = = = = = = = = = =

    '--- For suppressing default popup menu..-
    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = =

    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '-- Parent Form..
    Private mFrmMyParent As Form


    Private mbIsInitialising As Boolean = True   '=3.2.1229==

    Private mbActive As Boolean = False '-- stops activate being re-entered..-
    Private mbStartupCompleted As Boolean = False

    Private mbUpdateFinished As Boolean = False

    Private mbService As Boolean = False  '-- caller requests service update...-
    Private mbDelivery As Boolean = False  '-- caller requests DELIVERY function...-
    '== Private mbNotify As Boolean = False  '-- caller requests NOTIFY function...-
    Private mbQuotation As Boolean = False '-- Job is Quote to build..-

    '==3311.331=
    Private mbIsOnSiteJob As Boolean = False '-- Job is ONSITE..-

    Private mCnnJobs As OleDbConnection  '== ADODB.Connection '--SQL jobs connection --
    Private mTransactionJobUpdate As OleDbTransaction

    Private mColSqlDBInfo As Collection
    Private msLocalSettingsPath As String = ""
    '==3311.225=
    Private mLocalSettings1 As clsLocalSettings

    Private msABN As String
    Private msBusinessName As String
    Private msBusinessShortName As String
    Private msBusinessAddress1 As String
    Private msBusinessAddress2 As String
    Private msBusinessState As String
    Private msBusinessPostCode As String
    Private msBusinessPhone As String
    Private msBusinessEmail As String

    Private msGSTPercentage As String
    Private mCurGST As Decimal = 1
    Private msServiceChargeCat1 As String = "SERVCE" '--default..-
    Private msServiceChargeCat2 As String = ""

    '--  TO DELETE..--
    '== Private mCnnJet  As ADODB.connection    '--  Retail Manager Jet connection..--
    '== Private mColJetDBInfo As Collection

    '-- retrieved job data --
    '-- retrieved job data --
    Private mlStaffId As Integer = -1 '--save staff-id--
    Private msStaffName As String = ""
    Private msStaffBarcode As String = ""

    Private mlCustomerId As Integer = -1 '--save cust-id--
    Private mlJobId As Integer = -1 '-- Identity retrieved..-
    Private msCustomerBarcode As String = ""
    Private msJobOriginalStatus As String = "" '--in case of Cancel..-
    Private mlQuoteSalesOrderId As Integer = -1

    Private msJobStatus As String = ""
    Private msPriority As String = ""
    Private mdDateCreated As Date
    Private msShortDateCreated As String
    Private mbSystemUnderWarranty As Boolean = False  '=3203.120=

    Private msOriginalNominTech As String = "" '--original.--
    Private msNominTech As String = "" '-current.--
    Private msNewOwner As String = ""
    Private mbIsOwner As Boolean = False

    '--  Customer..-
    Private mColRMCustomerDetails As Collection
    '--Private msCustomerName As String
    Private msCustomerPhone As String = ""
    Private msCustomerMobile As String = ""
    Private msCustomerPrint As String = ""
    '--new-
    Private msCustomerName As String = ""
    Private msCustomerCompany As String = ""
    Private msCustomerEmail As String = ""


    Private msDateUpdated As String = ""

    Private msGoodsInCare As String = ""
    Private msOtherGoods As String = ""
    Private msExtrasInCare As String = ""

    Private msProblems As String = ""
    Private msProblemDetails As String = ""
    Private msProblemShort As String '--Rev-2916--

    Private maiTaskTypes() As Short

    Private mbBrowsing As Boolean = False
    Private mbDataChanged As Boolean = False

    '----Private mRsJob As ADODB.Recordset   '--  current job record--
    Private mColJobFields As Collection
    '= = = = =  = =  = = = = =

    Private msTimeSpent As String = "" '--ToDate- excl. this session..eg. "2.5" --
    '== Private msSessionTimes As String = "" '- ToDate !!   --
    Private msSessionTimesToDate As String = ""
    Private msSessionTime As String = "0" '--eg. "2.5" --
    '== 3107.518==
    Private mbSessionTimesReset As Boolean = False

    Private mCurLabourHourlyRate As Decimal = 1 '--actual rate used..-
    Private mCurLabourMinCharge As Decimal = 1 '--actual rate used..-
    Private mbEnforceMinCharge As Boolean = True   '== DEFAULT.. value if any from systemInfo..-
    '--Set up 3 rates..
    '==3311.331= Private mCurLabourHourlyRateP1 As Decimal = 1
    '==3311.331= Private mCurLabourHourlyRateP2 As Decimal = 1
    '==3311.331= Private mCurLabourHourlyRateP3 As Decimal = 1

    '==3311.331= Private msDescriptionPriority1 As String = ""
    '==3311.331= Private msDescriptionPriority2 As String = ""
    '==3311.331= Private msDescriptionPriority3 As String = ""

    Private mCurTotalChargeableHours As Decimal = 0 '--incl current session if chargeable..-
    Private mCurLabour As Decimal = 0
    Private mCurParts As Decimal = 0
    Private mCurOrigParts As Decimal = 0

    '= = = = =  = =  = = = = =
    '--  printers--
    Private msColourPrinterName As String = ""
    Private msReceiptPrinterName As String = ""
    Private msDefaultPrinterName As String = ""

    Private mlProgress As Integer '--progress count..  (-1)= NotActive..--

    '-- notification..--
    Private mlNotifyBGColour As Integer
    Private mbNotifyCancelled As Boolean
    Private mbNotifiedOK As Boolean
    '= = = = = = = = = = = = = =  =

    '--  SCRATCHPAD for Tasks/Parts Changes..-
    '--  SCRATCHPAD for Tasks/Parts Changes..-
    '--  SCRATCHPAD for Tasks/Parts Changes..-
    Private malDeletedTasks() As Integer '--Record ID's of tasks to be deleted from live table..--
    Private malDeletedParts() As Integer '--Record ID's of PARTS to be deleted  do ..--
    Private mlTaskDeletions As Integer
    Private mlPartDeletions As Integer

    '--- these array slots are targetted by listBox ItemData values..--
    '-----  NOW all ZERO-based..-- for vb.net..--

    Private mlTempTasksCount As Integer '--array count..-
    Private malTempTasksIndexes() As Integer '-- -1=Deleted, 0=NewRecord, 1..n=ExistingTableRecordId  --
    '-------                                      (for NewRecord, collection is in mavTempNewTasks at same index..)-
    Private mavTempNewTasks() As Object '-- FldCollection  if this task has been added, or Nothing.-
    '===
    Private mlTempPartsCount As Integer '--array count..-
    Private malTempPartsIndexes() As Integer '-- -1=Deleted, 0=NewRecord, 1..n=ExistingTableRecordId  --
    '-------                                      (for NewRecord, collection is in mavTempNewData at same index..)-
    Private mavTempNewParts() As Object '-- FldCollection  if this PART has been added, or Nothing.-
    Private maCurTempPartsCosts() As Decimal '-- cost of parts in parts listbox..--
    Private mabTempPartsCostUpdated() As Boolean '-- cost of part has changed since PartRecord last written...--
    '==                                            '--  has to be updated in table..--
    '--  Multiple Service Checklists..--
    '---  Array of indexes into ListViewChecklist control array..--
    Private malActiveChecklists() As Integer '- Control array indexes.. slots are targetted by Parts/Service listBox ItemData values..--
    '--Array Counted by mlTempPartsCount..--
    Private mavGridChecklists() As Object '-- emulates/replaces FlexGrid control-array..
    '---     Each v-item holds a (2-dim) (row,col) string array which holds/backs-up the FlexGrid data..

    Private mlNextChecklistIndex As Integer '-- next index to load/use in fgChecklist control array..

    '--  SAVE fgGid index of ChechList currently in FLEXGris..--
    Private mlCurrentChecklistIndex As Integer '-- index to save fgChecklist into..

    '== -- 3431.0512- maint. Update Job.. 
    '==             On Saving updates, log all Item movements to Service Notes...
    Private msItemMovementLog As String = ""

    '-- END of  SCRATCHPAD for Changes..-
    '-- END of  SCRATCHPAD for Changes..-
    '= = = = = = = = = = =  =  = =  =

    '--  Remember original CheckList Comments.-
    '==  NOW STORED in FlexGrid Row.. ==  Private masCheckListComments() As String

    Private mItemSelectedPart As System.Windows.Forms.ListViewItem

    Private msItemBarcodeFontName As String = ""
    Private mlItemBarcodeFontSize As Integer = 9

    '--  Rev-2916 --
    Private mCurNotificationCostLimit As Decimal = 160
    Private msDeliveryDocketFootnote As String = " Default Footnote.."

    '-- Rev-3010--
    Private mRetailHost1 As _clsRetailHost

    '-- Rev-3031--
    Private mIntFormTop As Integer = -1
    Private mIntFormLeft As Integer = -1

    '--  Popup menu for Right click on parts..-
    '--  Popup menu for Right click on parts..-

    '--  Popup menu for Right click on parts..-
    Private mContextMenuPartInfo As ContextMenu
    Private WithEvents mnuCopyPartDescription As New MenuItem("Copy Part Description.")
    Private WithEvents mnuCopyPartBarcode As New MenuItem("Copy Part Barcode.")
    Private WithEvents mnuCopyPartSerialNo As New MenuItem("Copy Part SerialNo.")
    Private WithEvents mnuPartMenuSep1 As New MenuItem("-")
    Private WithEvents mnuDeletePart As New MenuItem("Delete item.")

    '-- exit menu..-
    Private mContextMenuExit As ContextMenu

    Private WithEvents menuItemExitSave As New MenuItem("0. No status change.")
    Private WithEvents menuItemExitSuspend As New MenuItem("1. Suspend Job.")
    Private WithEvents menuItemExitQA As New MenuItem("2. Send to QA")
    Private WithEvents menuItemExitCompleted As New MenuItem("3. Mark as Completed.")

    '== Private mIntExitMenuCompletedIndex As Integer = -1

    '==  Private mIntExitMenuSelectedIndex As Integer = -1
    Private msExitMenuSelectedName As String = ""

    Private mbStatusWasSuspOrQA As Boolean
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->


    '--=3.2.1229=

    '=  Context menu for Pasting- attachment file name-
    '--  Popup menu for Right click on txt File name..-

    Private mContextMenuPasteFileName As ContextMenu
    Private WithEvents mnuPasteFileName As New MenuItem("Paste File")
    Private WithEvents mnuPasteFileSep1 As New MenuItem("-")
    Private WithEvents mnuPasteFileSep2 As New MenuItem("-")

    '-- Dummy to disable default menu-
    '-- LEAVE empty.-
    Private mContextMenuDummy As New ContextMenu

    '== clsAttachments -

    Private mClsAttachments1 As clsAttachments

    '== 3203.116=
    '--  Load all Business Info here..
    '--  load system info..--
    Private mClsSystemInfo As clsSystemInfo

    '==3311.819=  Flag if me.Close was called.. (for FormClosing Event-) 
    Private mbProgramClosing As Boolean = False
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '--   Can't RAISEEVENT from SHOWN..
    Private mbCloseRequestedAtStartup As Boolean = False

    Private mbHostIsJobTracking As Boolean = False

    '== Target-New-Build-6201 --  (16-July-2021) for Open Source..
    Private msRetailHostname As String = ""
    '== END Target-New-Build-6201 --  (16-July-2021) for Open Source..

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)

    WriteOnly Property parentForm As Form
        Set(value As Form)
            mFrmMyParent = value
        End Set
    End Property  '-parent..-
    '= = == =  == = = = =  = 

    WriteOnly Property connectionSql() As OleDbConnection  '==  ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =

    '=== Property Let connectionJet(cnn1 As ADODB.connection)

    '==   Set mCnnJet = cnn1

    '== End Property  '--cnn jet..--
    '= = = = = = =  = = = = =

    WriteOnly Property dbInfoSql() As Collection
        Set(ByVal Value As Collection)

            mColSqlDBInfo = Value

        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

    WriteOnly Property retailHost() As _clsRetailHost
        Set(ByVal Value As _clsRetailHost)

            mRetailHost1 = Value
        End Set
    End Property '--host..--
    '= = = = =  = =  = = =


    '== Property Let dbInfoJet(dbinfo As Collection)

    '==   Set mColJetDBInfo = dbinfo

    '== End Property  '--info jet..--
    '= = = = = = = = = = = =


    '--caller supplies job no--

    WriteOnly Property JobNo() As Integer
        Set(ByVal Value As Integer)

            mlJobId = Value
        End Set
    End Property '--job--
    '= = = = = = = = = = = =

    '--caller requests Service details updating..--

    WriteOnly Property ServiceUpdate() As Boolean
        Set(ByVal Value As Boolean)

            mbService = Value

        End Set
    End Property '--service--
    '= = = = = = = = = = = =

    '--caller requests Delivery details updating..--
    WriteOnly Property DeliveryUpdate() As Boolean
        Set(ByVal Value As Boolean)

            mbDelivery = Value
        End Set
    End Property '--delivery--
    '= = = = = = = = = = = =
    '--caller requests Delivery details updating..--
    '== WriteOnly Property NotifyUpdate() As Boolean
    '== 	Set(ByVal Value As Boolean)
    '== 		mbNotify = Value
    '== 	End Set
    '== End Property '--delivery--
    '= = = = = = = = = = = =

    '--  printers..--
    '== 3203.107- Now discovered here internally..

    '== WriteOnly Property ColourPrinterName() As String
    '==     Set(ByVal Value As String)
    '==         msColourPrinterName = Value
    '==     End Set
    '== End Property '--PrinterName.--
    '= = = = = = = =  = = =
    '== WriteOnly Property ReceiptPrinterName() As String
    '==     Set(ByVal Value As String)
    '==         msReceiptPrinterName = Value
    '==     End Set
    '== End Property '--ReceiptPrinter.--
    '= = = = = = = =  = = =

    '-- Staff Id now comes from caller..--

    WriteOnly Property StaffBarcode() As String
        Set(ByVal Value As String)
            msStaffBarcode = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =
    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    WriteOnly Property StaffId() As Integer
        Set(ByVal Value As Integer)

            mlStaffId = Value
        End Set
    End Property '--id.-
    '= = = = = = = =  = = =

    '--business info.--

    '-- set user logo for printing..--
    WriteOnly Property UserLogo() As Object
        Set(ByVal Value As Object)

            Picture2.Image = Value
        End Set
    End Property '--logo..--
    '= = = = = = = =  ==

    '== WriteOnly Property BusinessABN() As String
    '==     Set(ByVal Value As String)
    '==         msABN = Value
    '=     End Set
    '== End Property '--abn.--
    '= = = = = = = =  = = =

    '== WriteOnly Property BusinessName() As String
    '==     Set(ByVal Value As String)
    '==         msBusinessName = Value
    '==     End Set
    '== End Property '--name.--
    '= = = = = = = =  = = =

    '== WriteOnly Property BusinessShortName() As String
    '=     Set(ByVal Value As String)
    '==         msBusinessShortName = Value
    '==     End Set
    '== End Property '--name.--
    '= = = = = = = =  = = =

    '== WriteOnly Property BusinessAddress1() As String
    '==     Set(ByVal Value As String)
    '==         msBusinessAddress1 = Value
    '==     End Set
    '== End Property '--addr.--
    '= = = = = = = =  = = =

    '== WriteOnly Property BusinessAddress2() As String
    '==     Set(ByVal Value As String)
    '==         msBusinessAddress2 = Value
    '==     End Set
    '== End Property '--addr.--
    '= = = = = = = =  = = =

    '== WriteOnly Property BusinessState() As String
    '==     Set(ByVal Value As String)
    '==         msBusinessState = Value
    '==     End Set
    '== End Property '--state--
    '= = = = = = = =  = = =

    '== WriteOnly Property BusinessPostCode() As String
    '==     Set(ByVal Value As String)
    '==         msBusinessPostCode = Value
    '==     End Set
    '== End Property '--state--
    '= = = = = = = =  = = =

    '== WriteOnly Property BusinessPhone() As String
    '==     Set(ByVal Value As String)
    '==         msBusinessPhone = Value
    '==     End Set
    '== End Property '--state--
    '= = = = = = = =  = = =

    '== WriteOnly Property GSTPercentage() As String
    '==     Set(ByVal Value As String)
    '==         msGSTPercentage = Value
    '==     End Set
    '= End Property '--abn.--
    '= = = = = = = =  = = =

    '== WriteOnly Property LabourHourlyRatePriority1() As Decimal
    '==     Set(ByVal Value As Decimal)
    '==            mCurLabourHourlyRateP1 = Value
    '==     End Set
    '== End Property '--abn.--
    '= = = = = = = =  = = =
    '== WriteOnly Property LabourHourlyRatePriority2() As Decimal
    '==     Set(ByVal Value As Decimal)
    '==             mCurLabourHourlyRateP2 = Value
    '==     End Set
    '= End Property '--abn.--
    '= = = = = = = =  = = =

    '== WriteOnly Property LabourHourlyRatePriority3() As Decimal
    '==     Set(ByVal Value As Decimal)
    '==             mCurLabourHourlyRateP3 = Value
    '==     End Set
    '== End Property '--abn.--
    '= = = = = = = = = = =
    '== WriteOnly Property LabourMinCharge() As Decimal
    '==     Set(ByVal Value As Decimal)
    '==             mCurLabourMinCharge = Value
    '==     End Set
    '== End Property '--abn.--
    '= = = = = = = = = = =

    '== WriteOnly Property DescriptionPriority1() As String
    '==     Set(ByVal Value As String)
    '==             msDescriptionPriority1 = Value
    '==     End Set
    '== End Property
    '= = = = = = = = = = = = = = =
    '== WriteOnly Property DescriptionPriority2() As String
    '==     Set(ByVal Value As String)
    '==             msDescriptionPriority2 = Value
    '==     End Set
    '== End Property
    '= = = = = = = = = = = = = = =
    '== WriteOnly Property DescriptionPriority3() As String
    '==     Set(ByVal Value As String)
    '==             msDescriptionPriority3 = Value
    '==     End Set
    '== End Property
    '= = = = = = = = = = = = = = =

    '== WriteOnly Property ItemBarcodeFontName() As String
    '==     Set(ByVal Value As String)
    '==         msItemBarcodeFontName = Value
    '==     End Set
    '== End Property '--barcode font.--
    '= = = = = = = =  = = =

    '== WriteOnly Property ItemBarcodeFontSize() As Integer
    '==     Set(ByVal Value As Integer)
    '==         mlItemBarcodeFontSize = Value
    '==     End Set
    '== End Property '--barcode font.--
    '= = = = = = = =  = = =

    '== WriteOnly Property ServiceChargeCat1() As String
    '==    Set(ByVal Value As String)
    '==         msServiceChargeCat1 = Value
    '=     End Set
    '= End Property
    '= = = = = = = = = = = = = = =

    '== WriteOnly Property ServiceChargeCat2() As String
    '==     Set(ByVal Value As String)
    '==             msServiceChargeCat2 = Value
    '==     End Set
    '==  End Property
    '= = = = = = = = = = = = = = =

    '--   REV-2916--

    '== WriteOnly Property NotificationCostLimit() As Decimal
    '==     Set(ByVal Value As Decimal)
    '==             mCurNotificationCostLimit = Value
    '=     End Set
    '== End Property '--limit.--
    '= = = = = = = =  = = =

    '= WriteOnly Property DeliveryDocketFootnote() As String
    '=     Set(ByVal Value As String)
    '=         msDeliveryDocketFootnote = Value
    '=     End Set
    '= End Property '--state--
    '= = = = = = = =  = = =

    '--Mandated location..-
    WriteOnly Property MandatedFormTop() As Integer
        Set(ByVal Value As Integer)
            If Not (Value < 0) Then
                mIntFormTop = Value
            End If '--nothing--
        End Set
    End Property '--initial.--
    '= = = = = = = = = = = = =
    WriteOnly Property MandatedFormLeft() As Integer
        Set(ByVal Value As Integer)
            If Not (Value < 0) Then
                mIntFormLeft = Value
            End If '--nothing--
        End Set
    End Property '--initial.--
    '= = = = = = = = = = = = 

    WriteOnly Property CustomerDetails() As Collection
        Set(ByVal Value As Collection)
            mColRMCustomerDetails = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '=Private mbHostIsJobTracking As Boolean = False
    WriteOnly Property HostIsJobTracking As Boolean
        Set(value As Boolean)
            mbHostIsJobTracking = value
        End Set
    End Property '-HostIsJobTracking
    ''= = = = = = = = = = = = = = = =

    '-- end properties..-
    '-===FF->

    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)

    '-- Child publics..

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport

    '= = = = = = = = = = = = = = = = = = = = == = 

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)

    '= = = = = = = = = = = = = = = = = = = = == = 

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                        ByVal intParentHeight As Integer)

        Me.Width = intParentWidth - 11
        Me.Height = intParentHeight - 11
        '-- resize our controls..
        '= DoEvents()
        '-- resize main box and top panel-


    End Sub  '-resized-
    '= = = = = = = = = = = =

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click....

    Public Function SubFormCloseRequest() As Boolean
        '== Call close_me()
        Dim intCancel As Integer = 0  '-- zero means let close continue.. 
        SubFormCloseRequest = False  '- false means deny request..

        '- Ask if ok to close, and return result..

        'If (mbDataChanged) Then
        '    If (MsgBox("Abandon changes ?", _
        '                  MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
        '        '- no, don't close.
        '        SubFormCloseRequest = False
        '    Else '-yes- ok to close-
        '        SubFormCloseRequest = True
        '    End If
        'Else  '-no change.
        '    SubFormCloseRequest = True
        'End If  '- modified.-

        If Not (mbService Or mbDelivery) Then '--view only..-
            intCancel = 0 '--let it go--
        Else '--lock is held..
            If mbUpdateFinished Or mbProgramClosing Then  '=3311.819= -updated or me.close-
                intCancel = 0 '--let it go--
            ElseIf mbService And (Not mbDataChanged) Then  '== And (mIntExitMenuSelectedIndex <= 0) Then
                '-- Just cancel the transaction..-
                Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
                '-Call mbRollbackTransaction
                intCancel = 0 '--let it go--
            ElseIf (mbService And (mbDataChanged)) Then  '== Or (mIntExitMenuSelectedIndex > 0)) Then
                '-- QUERY cancel the transaction..-
                '-- confirm if changes are to be trashed...--
                If Not MessageBox.Show(Me, "DISCARD all changes and exit this job ?", _
                                       "JobMatix Update", MessageBoxButtons.YesNo, _
                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                    '===txtDiagnosis.SetFocus
                    intCancel = 1
                    '== MUST UPDATE arg..  ==Exit Sub '--was mistake..  keep form....
                Else '--yes, scrap all changes.. --
                    msJobStatus = k_statusStarted
                    Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
                    intCancel = 0 '--let it go--
                End If '--yes--
            ElseIf mbDelivery Then
                If MessageBox.Show(Me, "ABANDON this delivery ?", _
                        "JobMatix Update", MessageBoxButtons.YesNo, _
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                    intCancel = 0 '--let it go--
                Else  '--resume delivery..-
                    intCancel = 1 '--cant close yet-
                End If '--ans.-
            Else '- ? -data changed.  ????? .--
                intCancel = 1 '--cant close yet-- Must use cancel.-
            End If
        End If  '-service/deliv.-

        If (intCancel = 0) Then  '-let it go-
            SubFormCloseRequest = True
        End If

    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    '== Target-New-Build-6201 --  (16-July-2021) for Open Source..
    '=Private msRetailHostname As String = ""

    '-- Is RetailManager--

    Private Function mbIsRetailManager() As Boolean

        mbIsRetailManager = (LCase(msRetailHostname) = "retailmanager")
    End Function  '- mbIsRetailManager--
    '= = = = = = = = = = = = = = = = == 

    '-- IsIsJobmatixPOS--

    Private Function mbIsJobmatixPOS() As Boolean

        mbIsJobmatixPOS = (LCase(msRetailHostname) = "jobmatixpos")
    End Function  '- mbIsJobmatixPOS--
    '= = = = = = = = = = = = = = = = == 
    '== END Target-New-Build-6201 --  (16-July-2021) for Open Source..
    '-===FF->

    '--  disable tasks/parts controls for view/notify screens..--

    Private Function mbDisableServiceControls() As Boolean

        '=== Allow Viewing.. ==  fgCheckList.Enabled = False
        txtWorkDetails.ReadOnly = True
        txtDiagnosis.ReadOnly = True
        cmdAddTask.Enabled = False
        CmdCreateTask.Enabled = False
        cmdAddPart(0).Enabled = False
        cmdAddPart(1).Enabled = False
        '==ListViewTasks.Enabled = False  '--will be "locked" but can be scrolled etc...-
        '==ListViewParts.Enabled = False

    End Function '--disable.--
    '= = = = = = = = = = = =

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =

    '--get day of week--
    Private Function msDayOfWeek(ByRef date1 As Date) As String
        Dim sDay As String

        Select Case DatePart(Microsoft.VisualBasic.DateInterval.Weekday, date1)
            Case 1 : sDay = "Sunday"
            Case 2 : sDay = "Monday"
            Case 3 : sDay = "Tuesday"
            Case 4 : sDay = "Wednesday"
            Case 5 : sDay = "Thursday"
            Case 6 : sDay = "Friday"
            Case 7 : sDay = "Saturday"
        End Select
        msDayOfWeek = sDay

    End Function '--day--
    '= = = = = = =  =  = =
    '-===FF->

    '--  Data changed..--

    Private Function mbSetDataModified(Optional ByVal bStatusChangeOnly As Boolean = False) As Boolean
        If mbStartupCompleted Then
            If Not bStatusChangeOnly Then mbDataChanged = True
            cmdFinish.Enabled = True
            cmdCancel.Text = "Cancel"
            ToolTip1.SetToolTip(cmdCancel, "Abandon changes and exit..")
            ToolTip1.SetToolTip(cmdFinish, "Save changes/status, and exit..")
            '== NOT NEEDED ==   VB6.SetCancel(cmdCancel, False) '--  ESC can NOT exit..--

        End If  '--startup-
    End Function  '--DataChanged--
    '= = = = = = = = = = = = = = =

    '- mbCheckCompletion --

    Private Function mbCheckCompletion(Optional ByVal bViewOnly As Boolean = False) As Boolean

        mbCheckCompletion = False
        If (ListViewTasks.Items.Count > 0) Or _
                      (ListViewParts(k_LV_INDEX_SERVICE).Items.Count > 0) Or _
                          (mCurLabour > 0) Or (Trim(txtWorkDetails.Text) <> "") Then '--3107.611 = can complete--
            If Not bViewOnly Then optCompleted.Enabled = True
            labTaskNeeded.Visible = False
            mbCheckCompletion = True
        End If

    End Function  '--mbCheckCompletion--
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '--  Convert ListView into collection of item collections..--
    '--  Convert ListView into collection of item collections..--

    '==  NB:  HEY !!!  --
    '== The first subitem in the ListViewItem.ListViewSubItemCollection is always the item that owns the subitems.
    '==  When performing operations on subitems in the collection, 
    '==    be sure to reference index position 1 instead of 0 to make changes to the first subitem.
    '==Ref:     http://msdn.microsoft.com/en-us/library/system.windows.forms.listviewitem.subitems(v=VS.80).aspx

    Private Function mbMakeListViewCollection(ByRef ListView1 As System.Windows.Forms.ListView, _
                                                 ByRef colResult As Collection) As Boolean
        Dim ix, sx As Integer
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim subItem1 As System.Windows.Forms.ListViewItem.ListViewSubItem
        Dim col1 As Collection
        Dim sResult As String

        mbMakeListViewCollection = False
        sResult = "" '--testing..-
        colResult = New Collection
        If ListView1 Is Nothing Then Exit Function
        If (ListView1.Items.Count > 0) Then
            '==sDatePrev = "" '--control break..-
            ix = 0
            For Each item1 In ListView1.Items
                '==    For ix = 1 To (ListView1.Items.Count)
                col1 = New Collection
                ix += 1
                '--col header as key.- (Hdrs collection is 1-based.)-
                '--col header as key.- (Hdrs collection is 1-based.)-
                col1.Add(item1.Text, ListView1.Columns.Item(0).Text)
                sResult = sResult & ListView1.Columns.Item(0).Text & "=" & item1.Text & ";"
                '== MsgBox("Listview collection..  sResult=" & sResult & vbCrLf & _
                '==                   "SubItems count: " & item1.SubItems.Count, MsgBoxStyle.Information)
                If (ListView1.Columns.Count > 1) Then '--has sub-items..-
                    '== For Each subItem1 In item1.SubItems
                    '--  ACTUAL SUB-items start at 1...
                    For sx = 1 To (item1.SubItems.Count - 1)
                        '== col1.Add(item1.SubItems(sx).Text, ListView1.Columns.Item(sx + 1).Text) '--col header as key.-
                        subItem1 = item1.SubItems(sx)
                        col1.Add(subItem1.Text, ListView1.Columns.Item(sx).Text) '--col header as key.-
                        sResult = sResult & ListView1.Columns.Item(sx).Text & "=" & subItem1.Text & ";"
                    Next sx
                    '==Next subItem1
                End If '--count..-
                '== col1.Add(item1.SmallIcon, "SmallIcon") '-for Quotes..-
                col1.Add(item1.ImageKey, "SmallIcon") '-for Quotes..-

                colResult.Add(col1)
                sResult = sResult & vbCrLf & "======" & vbCrLf
                '== Next ix
            Next item1
        End If '--count..-
        '--  testing.  show result..-
        '== MsgBox "Result collection is: " & vbCrLf & sResult, vbInformation
        mbMakeListViewCollection = True

    End Function '== mColMakeListViewCollection--
    '= = = = = = = = = = = = = =
    '-===FF->


    Private Function mbIsServiceCharge(ByRef sCat1 As String, ByRef sCat2 As String) As Boolean

        mbIsServiceCharge = (LCase(msServiceChargeCat1) = LCase(sCat1)) And _
            ((msServiceChargeCat2 = "") Or (((msServiceChargeCat2 <> "") And (LCase(msServiceChargeCat2) = LCase(sCat2)))))

    End Function
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  compute labour chargeable hours to date..-
    Private Function mCurSearchChargeableHours(ByVal sSessionTimes As String) As Decimal
        Dim sRem As String
        Dim sName, s1 As String
        Dim sDate As String
        Dim sTimeSpent As String
        Dim iPos2, iPos, iPos3 As Integer
        Dim bChargeable As Boolean
        Dim curResult As Decimal

        '-- Diseect accumulated session string..--
        curResult = 0
        sRem = Trim(sSessionTimes) '--get all sessions TO DATE this job..-
        While (sRem <> "")
            sName = "" : sDate = "" : sTimeSpent = ""
            bChargeable = True
            iPos = InStr(sRem, vbCrLf)
            If iPos > 0 Then
                s1 = Trim(VB.Left(sRem, iPos - 1))
                sRem = Trim(Mid(sRem, iPos + 2)) '--drop cf/lf-
            Else
                s1 = Trim(sRem) : sRem = "" '--last session..-
            End If '--ipos.-
            '--dissect session..-
            If s1 <> "" Then
                iPos = InStr(s1, ":")
                If (iPos > 1) Then
                    sDate = Trim(VB.Left(s1, iPos - 1))
                    If Not IsDate(sDate) Then
                        '--sDate = sDateCompleted  '--in case of bad stuff.--
                    Else
                        sDate = VB6.Format(CDate(sDate), "dd-mmm-yyyy") '--reformat..--
                    End If
                    s1 = Trim(Mid(s1, iPos + 1))
                    iPos2 = InStr(s1, "+")
                    If (iPos2 > 0) Then
                        sName = Trim(VB.Left(s1, iPos2 - 1))
                        If sName = "" Then sName = "YY_UNKNOWN"
                        sTimeSpent = UCase(Trim(Mid(s1, iPos2 + 1)))
                        iPos3 = InStr(sTimeSpent, "-NC") '--17Apr2010--
                        If (iPos3 > 0) Then '--no charge-
                            bChargeable = False
                            sTimeSpent = Replace(sTimeSpent, "-NC", "") '-get rid of marker..-
                        End If
                    End If
                End If
            End If '--s1
            '====sTest = sTest + sDate + "," + sName + "," + sTimeSpent + "; "
            '--create session record.-
            If bChargeable And (sName <> "") And (sDate <> "") And IsNumeric(sTimeSpent) Then '-- sTimeSpent = ""
                curResult = curResult + CDec(sTimeSpent)
            End If
        End While '--sessions.=
        mCurSearchChargeableHours = curResult
    End Function '--chargeable hours.-
    '= = = = = = = = = == =
    '-===FF->

    '---  Show full Job costing..--
    '---  Show full Job costing..--

    Private Function mbShowFullCost() As Boolean
        Dim sShowCost As String
        Dim sMinCharge As String
        '--Dim curLabour As Currency
        Dim curSessionTime As Decimal
        Dim curDisk As Decimal
        Dim ix As Short

        curDisk = 0
        sMinCharge = ""
        '-- 08Dec2009  == Compute total of current parts..--
        mCurParts = 0
        If (mlTempPartsCount > 0) Then
            '==For ix = 1 To mlTempPartsCount
            For ix = 0 To (mlTempPartsCount - 1)
                mCurParts = mCurParts + maCurTempPartsCosts(ix)
            Next ix
        End If
        mCurTotalChargeableHours = mCurSearchChargeableHours(msSessionTimesToDate) '--hours to date--
        curSessionTime = CDec(CDbl(msSessionTime)) '--current session..-
        If (optChargeable(0).Checked = True) Then '--Charge this session..-
            mCurTotalChargeableHours = (mCurTotalChargeableHours + curSessionTime) '-- total hours..-
        End If
        mCurLabour = (mCurTotalChargeableHours * mCurLabourHourlyRate) '--chargeable labour cost.. carry fwd old cost..-
        '--  v2787-- REPLACE with Min Charge IF Applicable.=
        '--  Build 3203.126 UNLESS overridden..=
        '--   and 3203.211--
        '-- (Not chkOverrideMin.Checked) AndAlso 
        If mbEnforceMinCharge AndAlso _
                       ((mCurTotalChargeableHours > 0) And (mCurLabour < mCurLabourMinCharge)) Then
            mCurLabour = mCurLabourMinCharge
            sMinCharge = "(Minimum Charge: " & VB6.Format(mCurLabourMinCharge, "$###0.00") & ")."
        End If
        sShowCost = Space(10)
        sShowCost = RSet(FormatCurrency(mCurParts, 2), Len(sShowCost))
        '===  labFullCost.Caption = " -- JOB COST: --" + vbCrLf + "Items:   " + sShowCost + vbCrLf
        labFullCost.Text = "Items:   " & sShowCost & vbCrLf
        sShowCost = Space(10)
        sShowCost = RSet(FormatCurrency(mCurLabour, 2), Len(sShowCost))
        labFullCost.Text = labFullCost.Text & "Labour:  " & sShowCost & vbCrLf
        '==If (ChkRecovDisk.Value = 1) Then  '--add $15.00--
        '==     sShowCost = Space(10)
        '==     RSet sShowCost = "15.00"
        '==     curDisk = 15#
        '==     labFullCost.Caption = labFullCost.Caption + "DataDisk:" + sShowCost + vbCrLf
        '==End If
        sShowCost = Space(10)
        sShowCost = RSet(FormatCurrency(mCurParts + mCurLabour + curDisk, 2), Len(sShowCost))
        labFullCost.Text = labFullCost.Text & "Total:   " & sShowCost

        LabTotalTime.Text = "Chargeable Time: " & VB6.Format(mCurTotalChargeableHours, "0.00") & " Hrs." & vbCrLf & _
                             "Labour Rate: " & VB6.Format(mCurLabourHourlyRate, "$###0.00") & " per hr." & vbCrLf & _
                                    "Labour Cost: " & VB6.Format(mCurLabour, "$###0.00") & " ===" & vbCrLf & sMinCharge

    End Function '-show cost..-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--Check for all required flds completed..
    '-- return string list of missing flds..
    '== V2.2 ===
    '-- Checklists must be completed if present.--

    Private Function msCheckFormComplete() As String
        Dim sResult As String
        Dim s1, s2 As String
        Dim sStockId As String
        Dim sStockDescription As String
        Dim sStatus As String
        Dim ix, lngaffected As Integer
        Dim idxTemp, idxService, idxGrid As Integer
        Dim lngStock_id As Integer
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim vasChecklist As Object

        sResult = ""

        '== V2.2 ===
        '-- Checklists must be completed if present.--
        '--  EXAMINE all surviving JOB SERVICE checklists.. --
        If (ListViewParts(k_LV_INDEX_SERVICE).Items.Count > 0) Then
            For Each item1 In ListViewParts(k_LV_INDEX_SERVICE).Items
                '==s1 = Trim(item1.SubItems(3)) '--serial no.- Print only if NOT blank..
                '=3404.909- k_LV_PARTS_STOCK_ID_IDX
                '== lngStock_id = CInt(item1.SubItems(5).Text)
                lngStock_id = CInt(item1.SubItems(k_LV_PARTS_STOCK_ID_IDX).Text)
                sStockId = CStr(lngStock_id)
                sStockDescription = CStr(item1.SubItems(1).Text)
                idxTemp = IIf((item1.Tag = ""), -1, CInt(item1.Tag)) '--get index into temp parts..-
                If (idxTemp >= 0) Then   '==3061.0 == idxTemp > 0 Then '--valid.-
                    idxGrid = malActiveChecklists(idxTemp) '--get grid array index if any..-
                    If idxGrid >= 0 Then '--have a checklist..--
                        '==With fgCheckList(idxGrid)
                        '--  Check each checklist..-
                        vasChecklist = mavGridChecklists(idxGrid)
                        For ix = 0 To UBound(vasChecklist, 1) '== 1 To .Rows - 1
                            '==sDescr = Trim(.TextArray(miFgi(fgCheckList(idxGrid), ix, k_GRIDCOL_ITEM))) '--task description..
                            '==sStatus = Trim(.TextArray(miFgi(fgCheckList(idxGrid), ix, k_GRIDCOL_STATUS))) '-- get current status value..
                            sStatus = Trim(vasChecklist(ix, k_GRIDCOL_STATUS)) '-- get current status value..
                            If (sStatus = "") Or (sStatus = "--") Then
                                sResult = sResult & _
                                      "- Service Checklist for: '" & sStockDescription & "'  is not complete.." & vbCrLf
                                Exit For
                            End If
                        Next ix
                        '==End With
                    End If '--idx Grid.-
                End If '--idxTemp..-
            Next item1
        End If '--service items..-

        If mbQuotation Then
            '=== If Not mbChecklistStarted Then
            '===    sResult = sResult + "No Checklist Item has been noted..." + vbCrLf
            '== End If
        ElseIf (ListViewTasks.Items.Count <= 0) Then  '====  .ListCount <= 0) Then '--no tasks--
            '== 2902 now have checklists..==   sResult = sResult + "No Tasks have been performed..." + vbCrLf
        End If
        If (msSessionTime = "0") Then '--Tasks but no time..--
            '==sResult = sResult + "No labour hours have been recorded.." + vbCrLf
        ElseIf (optChargeable(0).Checked = False) And (optChargeable(1).Checked = False) Then  '--No indic of charge.-
            sResult = sResult & "- No selection has been made for Charge/No-Charge of SessionTime.." & vbCrLf
        End If
        msCheckFormComplete = sResult
    End Function '--check form..--
    '= = = = = = = = = = = = = = =  =
    '-===FF->

    '--  C O M M I T  Transaction..-
    '--  C O M M I T  Transaction..-

    Private Function mbCommitTransaction() As Boolean
        Dim lngError As Integer

        mbCommitTransaction = False
        On Error Resume Next
        If (Not (mTransactionJobUpdate Is Nothing)) AndAlso _
                    (Not (mTransactionJobUpdate.Connection Is Nothing)) Then
            mTransactionJobUpdate.Commit()
        End If
        '= mCnnJobs.CommitTrans()
        lngError = Err().Number
        On Error GoTo 0
        If lngError = 0 Then
            mbCommitTransaction = True
            If gbDebug Then
                MessageBox.Show(Me, "COMMIT TRANSACTION executed ok..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else '-failed..-
            MessageBox.Show(Me, "COMMIT TRANSACTION FAILED.." & vbCrLf & _
                      "Error: " & lngError & " = " & ErrorToString(lngError) & vbCrLf, _
                         "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Function '--commit--
    '= = = = = = = = = = =  =

    '-- R O L L B A C K ----
    '-- R O L L B A C K ----
    '--  Rollback Current Transaction ---

    Private Function mbRollbackTransaction() As Boolean
        Dim lngError As Integer

        mbRollbackTransaction = False
        On Error Resume Next
        '== mCnnJobs.RollbackTrans()
        If Not (mTransactionJobUpdate Is Nothing) AndAlso _
                            (Not (mTransactionJobUpdate.Connection Is Nothing)) Then
            mTransactionJobUpdate.Rollback()
        End If
        lngError = Err().Number
        On Error GoTo 0
        If lngError = 0 Then
            mbRollbackTransaction = True
            If gbDebug Then
                MessageBox.Show(Me, "ROLLBACK TRANSACTION executed..", _
                                  "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else '-failed..-
            MessageBox.Show(Me, "ROLLBACK TRANSACTION FAILED.." & vbCrLf & _
                       "Error: " & lngError & " = " & ErrorToString(lngError) & vbCrLf, _
                                "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Function '--rollback--
    '= = = = = = = = = = =  =
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '-- L o a d  J o b  R e c o r d..-
    '-- L o a d  J o b  R e c o r d..-
    '-- L o a d  J o b  R e c o r d..-

    Private Function mbGetJobRecord(ByVal lngJobNo As Integer, _
                                     ByRef bBeginTrans As Boolean, _
                                       ByRef ColJobFields As Collection) As Boolean

        Dim RsJob As DataTable  '= ADODB.Recordset
        Dim sSql, sName As String
        Dim ok As Boolean
        Dim colFld As Collection

        mbGetJobRecord = False
        sSql = "SELECT * from [jobs]  "
        If bBeginTrans Then '-- start trans and lock record.. TO SET InProcess STATUS--
            sSql = sSql & " WITH (ROWLOCK, UPDLOCK) "
            '-- mTransactionJobUpdate --
            '== oleDb =  mCnnJobs.BeginTrans()
            mTransactionJobUpdate = mCnnJobs.BeginTransaction
        End If
        sSql = sSql & " WHERE (job_id=" & CStr(mlJobId) & ")  " & vbCrLf
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If bBeginTrans Then
            ok = gbGetDataTableEx(mCnnJobs, RsJob, sSql, mTransactionJobUpdate)
        Else '-no trans.-
            ok = gbGetDataTable(mCnnJobs, RsJob, sSql)
        End If
        If Not ok Then  '= gbGetDataTable(mCnnJobs, RsJob, sSql) Then
            If bBeginTrans Then Call mbRollbackTransaction()
            '--If mbService Or mbDelivery Then mCnnJobs.RollbackTrans
            MessageBox.Show(Me, "Failed to get JOB recordset.." & vbCrLf & _
                              "Job Record may be in use..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Me.Hide
            Exit Function
        End If
        '--txtMessages.Text = ""
        ColJobFields = New Collection
        If (Not (RsJob Is Nothing)) And (RsJob.Rows.Count > 0) Then
            '== If RsJob.BOF And (Not RsJob.EOF) Then
            '== RsJob.MoveFirst()
            '== End If
            '== If (Not RsJob.EOF) Then '---And (cx < 100)
            '--return complete row..-
            Dim dataRow1 As DataRow = RsJob.Rows(0)  '--first row-
            For Each column1 As DataColumn In RsJob.Columns '== fld1 In RsJob.Fields
                colFld = New Collection
                sName = column1.ColumnName
                colFld.Add(LCase(sName), "name")
                colFld.Add(dataRow1.Item(sName), "value")
                ColJobFields.Add(colFld, LCase(sName))
            Next column1  '= fld1
            mbGetJobRecord = True
            '== Else '--not found-
            '== End If '-eof-
            '== RsJob.Close()
        End If '--rs-
        RsJob = Nothing
    End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '--Release job..-
    '-- set status to "started".== or to "msJobOriginalStatus"--
    '-- SERVICE jobs only..--

    '-- Different processes for Transaction/ No-Transaction !! --

    Private Function mbReleaseJob(Optional ByVal sNewStatus As String = k_statusStarted) As Boolean
        Dim sSql As String
        Dim sErrors As String
        Dim ix, lngaffected As Integer

        mbReleaseJob = False
        sSql = "UPDATE [jobs] SET jobStatus='" & sNewStatus & "' "
        sSql = sSql & ", dateUpdated= CURRENT_TIMESTAMP "
        sSql = sSql & " WHERE (job_id=" & CStr(mlJobId) & ") "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrors) Then
            '--mCnnJobs.RollbackTrans
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MessageBox.Show(Me, "Failed to Release InProcessLock on DB JOB record.." & vbCrLf & _
                                                sErrors & vbCrLf, "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else '--ok-
            mbReleaseJob = True
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
    End Function '--release..-
    '= = = = = = = = = = =  =

    '-- Different processes for WITH Transaction !! --

    Private Function mbReleaseJobEx(Optional ByVal sNewStatus As String = k_statusStarted) As Boolean
        Dim sSql As String
        Dim sErrors As String
        Dim ix, lngaffected As Integer

        mbReleaseJobEx = False
        sSql = "UPDATE [jobs] SET jobStatus='" & sNewStatus & "' "
        sSql = sSql & ", dateUpdated= CURRENT_TIMESTAMP "
        sSql = sSql & " WHERE (job_id=" & CStr(mlJobId) & ") "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbExecuteSql(mCnnJobs, sSql, True, mTransactionJobUpdate, lngaffected, sErrors) Then
            '--mCnnJobs.RollbackTrans
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MessageBox.Show(Me, "Failed to Release InProcessLock on DB JOB record.." & vbCrLf & _
                                                               sErrors & vbCrLf, "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else '--ok-
            mbReleaseJobEx = True
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
    End Function '--release..-
    '= = = = = = = = = = =  =
    '-===FF->

    '-- lock Job..--
    '-- set status to "InProcess".==
    '---  !!  MUST update DateUpdated !!!!  --
    '-- SERVICE jobs only..--
    '==3311.731= Special status for QA-in process==

    Private Function mbLockJob(ByVal sOriginalStatus As String, _
                                 Optional ByVal bIsTransaction As Boolean = True) As Boolean
        Dim sSql As String
        Dim sErrors As String
        Dim ok As Boolean
        Dim ix, lngAffected As Integer

        mbLockJob = False
        If LCase(sOriginalStatus) = LCase(k_statusStarted) Then
            sSql = "UPDATE [jobs] SET jobStatus='" & k_statusInProcess & "' "
        ElseIf LCase(sOriginalStatus) = LCase(k_statusQA) Then
            sSql = "UPDATE [jobs] SET jobStatus='" & k_statusInProcessQA & "' "
        ElseIf LCase(sOriginalStatus) = LCase(k_statusSuspended) Then
            sSql = "UPDATE [jobs] SET jobStatus='" & k_statusInProcessSusp & "' "
        Else
            sSql = "UPDATE [jobs] SET jobStatus='" & k_statusInProcess & "' "
        End If
        '=3311.731= sSql = "UPDATE [jobs] SET jobStatus='" & k_statusInProcess & "' "
        sSql = sSql & ", dateUpdated= CURRENT_TIMESTAMP "
        sSql = sSql & " WHERE (job_id=" & CStr(mlJobId) & ") "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If bIsTransaction Then
            ok = gbExecuteSql(mCnnJobs, sSql, True, mTransactionJobUpdate, lngAffected, sErrors)
        Else
            ok = gbExecuteCmd(mCnnJobs, sSql, lngAffected, sErrors)
        End If
        If Not ok Then  '= gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrors) Then
            '--mCnnJobs.RollbackTrans
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MessageBox.Show(Me, "Failed to Set InProcessLock on DB JOB record.." & vbCrLf & _
                                    sErrors & vbCrLf, "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else '--ok-
            If lngAffected = 1 Then mbLockJob = True
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
    End Function '--Lock..-
    '= = = = = = = = = = = = = = 
    '-===FF->

    '--  show Grid row numbers..-

    Private Function mbNumberGridRows(ByRef dgv1 As DataGridView) As Boolean
        Dim rx As Integer

        If dgv1.RowCount > 0 Then
            For rx = 0 To (dgv1.RowCount - 1)
                dgv1.Rows(rx).HeaderCell.Value = (rx + 1).ToString  '== CStr(rx + 1)
            Next rx
        End If

    End Function  '-- NumberGridRows --
    '= = = = = = = = = = 

    '--  set up the correct ststus icon..--

    Private Function mbSetDataGridIcon(ByVal intRow As Integer, _
                                         ByVal sStatus As String, _
                                                ByRef dgv1 As DataGridView) As Boolean
        Dim sIconName As String
        Dim sText As String
        '--  Resources..-
        Dim statusIcon As Icon
        Dim asm As Assembly = Assembly.GetExecutingAssembly()

        '--  set corect status image.--

        '== With dgvChecklist.Rows(lRow).Cells(k_GRIDCOL_STATUS_IMAGE)
        sText = Trim(LCase(sStatus))
        If (sText = "") Then
            sIconName = "Blank.ico"  '= "QMANAGER_16.ICO"
        ElseIf (InStr(sText, "started") > 0) Then
            sIconName = "TRANSFRM16.ico"
        ElseIf (InStr(sText, "completed") > 0) Then
            sIconName = "Checked_T2.ico"
        ElseIf (InStr(sText, "skipped") > 0) Then
            sIconName = "Skipped.ico"
        ElseIf (InStr(sText, "queried") > 0) Then
            sIconName = "stop16.ico"
        Else
            sIconName = "Blank.ico"
        End If  '--text-

        If sIconName <> "" Then
            Try
                Dim streamIcon As Stream = asm.GetManifestResourceStream("JobMatix3." & sIconName)
                If streamIcon Is Nothing Then
                    MessageBox.Show(Me, "Error loading stream from ICON resource file: " & vbCrLf & _
                                                        sIconName & "..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else '--ok.-
                    Try
                        statusIcon = New Icon(streamIcon)
                        If Not (statusIcon Is Nothing) Then  '--ok-
                            '==Me.rtfJobDetails.LoadFile(streamRtf, RichTextBoxStreamType.RichText)
                            dgv1.Rows(intRow).Cells(k_GRIDCOL_STATUS_IMAGE).Value = statusIcon '- statusIcon.ToBitmap()
                        Else
                            MessageBox.Show(Me, "No icon stream returned..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    Catch ex2 As Exception
                        MessageBox.Show(Me, "Error loading ICON object from stream.." & vbCrLf & _
                            vbCrLf & ex2.ToString, "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End Try
                End If  '--stream nothing.-
            Catch ex As Exception    ' Catch the error.
                '--error-
                MessageBox.Show(Me, "Error loading stream from ICON resource file.." & vbCrLf & _
                                       vbCrLf & ex.ToString, "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        Else  '--no icon name=
            dgv1.Rows(intRow).Cells(k_GRIDCOL_STATUS_IMAGE).Value = Nothing
        End If
        '-- testing.-
        '== PicTest.Image = dgv1.Rows(lrow).Cells(k_GRIDCOL_STATUS_IMAGE).Value
        '= dgv1.Rows(lrow).Cells(k_GRIDCOL_COMMENTS).Value = "Status changed.."

    End Function   '-- set icon..--
    '= = = = = = = = = = = = = = =
    '-===FF->


    '-- CLEAR grid and set up x grid rows..

    Private Function mbLoadGrid(ByVal asDescriptions() As String, _
                                 ByRef dgv1 As DataGridView) As Boolean
        Dim intRowCount, rx As Integer
        Dim row1 As DataGridViewRow
        Dim sDescription As String
        Dim imageCell0 As DataGridViewImageCell

        With dgv1.RowTemplate
            .DefaultCellStyle.BackColor = Color.Bisque
            .Height = 21
            .MinimumHeight = 20
        End With

        '--  clear grid..
        dgv1.Rows.Clear()
        '--  create 7 new rows.
        intRowCount = asDescriptions.Length

        For rx = 1 To intRowCount

            row1 = New DataGridViewRow
            dgv1.Rows.Add(row1)
            '-- cells are created with the row..
            '=--  just update the cell values..--
            sDescription = asDescriptions(rx - 1)

            '-- col-0-  status-IMAGE--
            imageCell0 = dgv1.Rows(rx - 1).Cells(k_GRIDCOL_STATUS_IMAGE)
            imageCell0.ValueIsIcon = True

            '== dgv1.Rows(rx - 1).Cells(k_GRIDCOL_STATUS_IMAGE).Value = Nothing
            Call mbSetDataGridIcon(rx - 1, "", dgv1)

            '-- col-1-  Item description--
            dgv1.Rows(rx - 1).Cells(k_GRIDCOL_ITEM).Value = sDescription
            '-- col-2-  status--
            dgv1.Rows(rx - 1).Cells(k_GRIDCOL_STATUS).Value = ""
            '-- col-3.. comments.- 
            dgv1.Rows(rx - 1).Cells(k_GRIDCOL_COMMENTS).Value = ""
            '-- 4. Date updated.-
            dgv1.Rows(rx - 1).Cells(k_GRIDCOL_DATE).Value = ""
            '--5. Staff.-
            dgv1.Rows(rx - 1).Cells(k_GRIDCOL_NAME).Value = ""
            '--6. Prev. status.-
            dgv1.Rows(rx - 1).Cells(k_GRIDCOL_ORIG_STATUS).Value = ""
            '--7. Prev coments.-
            dgv1.Rows(rx - 1).Cells(k_GRIDCOL_ORIG_COMMENTS).Value = ""
        Next  '--rx--

        Call mbNumberGridRows(dgv1)

        '-- lighter colour for status column..-
        dgv1.Columns(k_GRIDCOL_STATUS).DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0)

        '-- lighter for comments column..-
        dgv1.Columns(k_GRIDCOL_COMMENTS).DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromOle(&HE8E8E8)

        dgv1.Columns(k_GRIDCOL_DATE).DefaultCellStyle.BackColor = Color.LightGray
        dgv1.Columns(k_GRIDCOL_NAME).DefaultCellStyle.BackColor = Color.LightGray

        '-  set column backcolour for History columns..--
        dgv1.Columns(k_GRIDCOL_ORIG_STATUS).DefaultCellStyle.BackColor = Color.Gray
        dgv1.Columns(k_GRIDCOL_ORIG_COMMENTS).DefaultCellStyle.BackColor = Color.Gray

    End Function  '--load grid..-
    '= = = = = = = = =  = = = = = = = = 
    '-===FF->

    '--  LOAD THE (only) Checklist FLEXGRID from Checklist array..--
    '---  NOTE:  Checklist arrays MUST have at least one row (1st dim)..

    '== FlexGrid == Private Function mbLoadFlexgridFromChecklist(ByVal lngFgIndex As Integer, _
    '== FlexGrid ==                              ByRef fgCheckList As AxMSHierarchicalFlexGridLib.AxMSHFlexGrid) As Boolean

    Private Function mbLoadFlexgridFromChecklist(ByVal lngFgIndex As Integer, _
                                                ByRef dgv1 As DataGridView) As Boolean
        Dim ix, lngRows As Integer
        Dim intRowCount, rx As Integer
        Dim lngCount As Integer
        Dim vasChecklist As Object
        Dim s1 As String
        Dim row1 As DataGridViewRow
        Dim sDescription As String
        Dim imageCell0 As DataGridViewImageCell

        '-- Finish set up grid stuff..-
        mbLoadFlexgridFromChecklist = False
        If lngFgIndex >= 0 Then '--have a grid..-
            vasChecklist = mavGridChecklists(lngFgIndex)
            If Not IsArray(vasChecklist) Then
                MessageBox.Show(Me, "Can't load checklist for index: " & lngFgIndex & vbCrLf & _
                                               " (not an array..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function
            End If
            '--  Set up the grid.-

            With dgv1.RowTemplate
                .DefaultCellStyle.BackColor = Color.Bisque
                .Height = 27
                .MinimumHeight = 20
            End With
            '--  clear grid..
            dgv1.Rows.Clear()
            '--  create 7 new rows.
            intRowCount = UBound(vasChecklist, 1) + 1  '== asDescriptions.Length

            '-- Create rows, and Load the data from the checklist array..--
            For rx = 1 To intRowCount
                row1 = New DataGridViewRow
                dgv1.Rows.Add(row1)
                '-- cells are created with the row..
                '=--  just update the cell values..--
                '= sDescription = asDescriptions(rx - 1)

                '-- col-0-  status-IMAGE--
                imageCell0 = dgv1.Rows(rx - 1).Cells(k_GRIDCOL_STATUS_IMAGE)
                imageCell0.ValueIsIcon = True

                '== dgv1.Rows(rx - 1).Cells(k_GRIDCOL_STATUS_IMAGE).Value = Nothing
                Call mbSetDataGridIcon(rx - 1, "", dgv1)
                ix = rx - 1  '-- array index..-
                '-- col-1-  Item description--
                dgv1.Rows(rx - 1).Cells(k_GRIDCOL_ITEM).Value = vasChecklist(ix, k_GRIDCOL_ITEM)
                '-- col-2-  status--
                dgv1.Rows(rx - 1).Cells(k_GRIDCOL_STATUS).Value = vasChecklist(ix, k_GRIDCOL_STATUS)
                '-- col-3.. comments.- 
                dgv1.Rows(rx - 1).Cells(k_GRIDCOL_COMMENTS).Value = vasChecklist(ix, k_GRIDCOL_COMMENTS)
                '-- 4. Date updated.-
                dgv1.Rows(rx - 1).Cells(k_GRIDCOL_DATE).Value = vasChecklist(ix, k_GRIDCOL_DATE)
                '--5. Staff.-
                dgv1.Rows(rx - 1).Cells(k_GRIDCOL_NAME).Value = vasChecklist(ix, k_GRIDCOL_NAME)
                '--6. Prev. status.-
                dgv1.Rows(rx - 1).Cells(k_GRIDCOL_ORIG_STATUS).Value = vasChecklist(ix, k_GRIDCOL_ORIG_STATUS)
                '--7. Prev coments.-
                dgv1.Rows(rx - 1).Cells(k_GRIDCOL_ORIG_COMMENTS).Value = vasChecklist(ix, k_GRIDCOL_ORIG_COMMENTS)

                Call mbSetDataGridIcon((rx - 1), vasChecklist(ix, k_GRIDCOL_STATUS), dgv1)
            Next  '--rx--
            Call mbNumberGridRows(dgv1)

            '-- lighter colour for status column..-
            dgv1.Columns(k_GRIDCOL_STATUS).DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0)
            '-- lighter for comments column..-
            dgv1.Columns(k_GRIDCOL_COMMENTS).DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromOle(&HE8E8E8)
            dgv1.Columns(k_GRIDCOL_DATE).DefaultCellStyle.BackColor = Color.LightGray
            dgv1.Columns(k_GRIDCOL_NAME).DefaultCellStyle.BackColor = Color.LightGray
            '-  set column backcolour for History columns..--
            dgv1.Columns(k_GRIDCOL_ORIG_STATUS).DefaultCellStyle.BackColor = Color.Gray
            dgv1.Columns(k_GRIDCOL_ORIG_COMMENTS).DefaultCellStyle.BackColor = Color.Gray

            '-- remember who is in the grid.-

            mlCurrentChecklistIndex = lngFgIndex
            mbLoadFlexgridFromChecklist = True
        End If '-grid-
    End Function '--load Grid.--
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  SAVE THE (only) Checklist FLEXGRID data INTO indicated Checklist array..--
    '---  NOTE:  Checklist arrays MUST have at leat one row (1st dim)..

    '==FG== Private Function mbSaveFlexgridIntoChecklist(ByRef fgCheckList As AxMSHierarchicalFlexGridLib.AxMSHFlexGrid, _
    '==FG==                                                      ByVal lngFgIndex As Integer) As Boolean

    Private Function mbSaveFlexgridIntoChecklist(ByRef dgv1 As DataGridView, _
                                                            ByVal lngFgIndex As Integer) As Boolean
        Dim lngRows As Integer
        Dim ix, lngCount As Integer
        Dim vasChecklist(,) As String  '--  2-dim.--

        '-- save grid data..-
        If (lngFgIndex >= 0) Then '--have a grid..-
            vasChecklist = mavGridChecklists(lngFgIndex)
            '--  Set up the grid.-
            Erase vasChecklist
            '== ReDim vasChecklist(fgCheckList.Rows - 2, 7) '-- n rows, 8 cols..-
            ReDim vasChecklist(dgv1.RowCount - 1, 7) '-- n rows, 8 cols..-

            '--  SAVE the FG data INTO the checklist array..--
            For ix = 0 To UBound(vasChecklist, 1)
                '== lngCount = ix + 1 '--grid data row..-  start at 1..--
                '==With fgCheckList
                '--  (re-creating) save ITEM description..  it doesn't change..-
                '== vasChecklist(ix, k_GRIDCOL_ITEM) = .get_TextArray(miFgi(fgCheckList, lngCount, k_GRIDCOL_ITEM))
                vasChecklist(ix, k_GRIDCOL_ITEM) = dgv1.Rows(ix).Cells(k_GRIDCOL_ITEM).Value
                '== vasChecklist(ix, k_GRIDCOL_STATUS) = .get_TextArray(miFgi(fgCheckList, lngCount, k_GRIDCOL_STATUS))
                vasChecklist(ix, k_GRIDCOL_STATUS) = dgv1.Rows(ix).Cells(k_GRIDCOL_STATUS).Value
                '== vasChecklist(ix, k_GRIDCOL_COMMENTS) = .get_TextArray(miFgi(fgCheckList, lngCount, k_GRIDCOL_COMMENTS))
                vasChecklist(ix, k_GRIDCOL_COMMENTS) = dgv1.Rows(ix).Cells(k_GRIDCOL_COMMENTS).Value
                '== vasChecklist(ix, k_GRIDCOL_DATE) = .get_TextArray(miFgi(fgCheckList, lngCount, k_GRIDCOL_DATE))
                vasChecklist(ix, k_GRIDCOL_DATE) = dgv1.Rows(ix).Cells(k_GRIDCOL_DATE).Value
                '== vasChecklist(ix, k_GRIDCOL_NAME) = .get_TextArray(miFgi(fgCheckList, lngCount, k_GRIDCOL_NAME))
                vasChecklist(ix, k_GRIDCOL_NAME) = dgv1.Rows(ix).Cells(k_GRIDCOL_NAME).Value
                '== vasChecklist(ix, k_GRIDCOL_ORIG_STATUS) = .get_TextArray(miFgi(fgCheckList, lngCount, k_GRIDCOL_ORIG_STATUS))
                vasChecklist(ix, k_GRIDCOL_ORIG_STATUS) = dgv1.Rows(ix).Cells(k_GRIDCOL_ORIG_STATUS).Value
                '== vasChecklist(ix, k_GRIDCOL_ORIG_COMMENTS) = .get_TextArray(miFgi(fgCheckList, lngCount, k_GRIDCOL_ORIG_COMMENTS))
                vasChecklist(ix, k_GRIDCOL_ORIG_COMMENTS) = dgv1.Rows(ix).Cells(k_GRIDCOL_ORIG_COMMENTS).Value
                '==End With
            Next ix
            '--  save the array into its correct slot..-
            mavGridChecklists(lngFgIndex) = VB6.CopyArray(vasChecklist)
        End If '--index..-
    End Function '--save..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Display checklist--
    '--  save Current checklist from Grid
    '--    and show new one into Checklist Grid.....--
    '--
    Private Function mbShowChecklist(ByVal item1 As ListViewItem) As Boolean
        Dim ix, idxTemp As Integer
        Dim idxGrid, lngErr As Integer
        '== Dim item1 As System.Windows.Forms.ListViewItem
        Dim sDescription As String

        idxTemp = CInt(item1.Tag) '--get scratchpad index..-
        sDescription = item1.SubItems(1).Text  '--2nd sub-item..
        LabChecklist.Text = "NO Task checklist for item  #" & item1.Index + 1 & ": " & vbCrLf & _
                                                                       item1.SubItems(1).Text '--descr..-
        ToolTip1.SetToolTip(LabChecklist, "") ''-default..-

        '--  SAVE current flexgrid data into it's correct checklist..--
        '==      Call mbSaveFlexgridIntoChecklist(fgCheckList, mlCurrentChecklistIndex)
        Call mbSaveFlexgridIntoChecklist(dgvChecklist, mlCurrentChecklistIndex)

        If (idxTemp >= 0) Then '--have scratchpad index--
            '--make all grids invisible except the chosen one..-
            '==For ix = 0 To (mlTempPartsCount - 1)
            idxGrid = malActiveChecklists(idxTemp) '--see if this is grid control index..-
            If (idxGrid >= 0) Then '--have checklist..
                '==       If (ix = idxTemp) Then '--this is selected item..--
                dgvChecklist.Visible = True
                '==      dgvChecklist.set_RowHeight(-1, 300)
                '--  lOAD checklist data into grid..--
                '== Call mbLoadFlexgridFromChecklist(idxGrid, fgCheckList)
                Call mbLoadFlexgridFromChecklist(idxGrid, dgvChecklist)

                LabChecklist.Enabled = True
                LabChecklist.Text = "Task Checklist for: item #" & item1.Index + 1 & ":  " & vbCrLf & sDescription
                ToolTip1.SetToolTip(LabChecklist, "<c>Double-Click on task status to update the task to a new status..")
                '==       Else
                '==            '== fgCheckList(idxGrid).Visible = False '--don't want to see this one..--
                '==       End If
            Else '--no checklist this item....-
                LabChecklist.Text = "No Task Checklist for item #" & item1.Index + 1 & ":  " & vbCrLf & sDescription
                ToolTip1.SetToolTip(LabChecklist, "")
                '== fgCheckList.Visible = False '--don't want to see this one.
                dgvChecklist.Visible = False '--don't want to see this one.
                mlCurrentChecklistIndex = -1 '==nothing in grid..-
            End If
            '==Next ix
        Else '--no scratchpad index !!!!..
            '==LabChecklist.Caption = "No Task checklist.."
            LabChecklist.Enabled = False
            mlCurrentChecklistIndex = -1 '==nothing in grid..-
        End If '-index-
    End Function  '--mbShowChecklist-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--SERVICE ITEM..  load a job checklist, or initialise a new one..
    '== ex GOSUB  -- LoadJobChecklist_LoadGrid
    '--  add new grid INSTANCE..--

    '--  load flexgrid..-
    Private Sub mLoadJobChecklist_LoadGrid(ByVal lngNoDataRows As Integer, _
                                                ByRef lngFgIndex As Short, _
                                                   ByRef vasChecklist(,) As String)
        '==Dim vasChecklist() As String

        '--  OK.. Allocate a FlexGrid instance for this ServiceChecklist..-
        '-- ACTUALLY, just allocate a new 2-dim array..-
        lngFgIndex = mlNextChecklistIndex '--use this index anyway...-
        '== If (mlNextChecklistIndex > 0) Then
        ReDim vasChecklist(lngNoDataRows - 1, 7) '-- n rows, 8 cols..-
        '--  Boost "control array"  --
        ReDim Preserve mavGridChecklists(lngFgIndex)
        '-- and save this checklist data array..-
        '==  SAVE AFTER LOADING..   mavGridChecklists(lngFgIndex) = vasChecklist()
        '== End If
        mlNextChecklistIndex = mlNextChecklistIndex + 1 '--update for next service checklisr this session..-

    End Sub '--LoadJobChecklist-
    '= = = = = = = = = = = = = =

    '--SERVICE ITEM..  load a job checklist, or initialise a new one..
    '---  returns -1 if no fgChecklist instance could be loaded..
    '------Else Returns ControlArray index of loaded flexGrid(Checklist).

    '==  NB: ver:3021--
    '--   NOW only allocates a new 2-dim array to hold grid data..
    '-----  The actual loading of the data into the ONLY Checklist-FlexGrid -
    '---         is done by a separate function called when slected Service Item changes..--

    Private Function mbLoadJobChecklist(ByVal bNewItem As Boolean, _
                                          ByRef lngJobId As Integer, _
                                           ByRef lngStockId As Integer, _
                                            ByRef intSequence As Short, _
                                            ByRef vasChecklist(,) As String) As Integer
        Dim lngFgIndex As Short
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim ix, lngCount As Integer
        Dim s1, sSql As String
        Dim sDescription As String
        Dim sStaff As String
        Dim sStatus, sComments As String
        '==Dim vasChecklist() As String

        mbLoadJobChecklist = -1
        lngFgIndex = -1 '--updated if grid loaded..-
        If Not bNewItem Then
            '--look for the next checklist for this job/stockId.
            lngCount = 0
            sSql = "Select * from [JobServiceCheckLists]  "
            sSql = sSql & " WHERE (JobChecklist_JobId=" & CStr(lngJobId) & ")"
            sSql = sSql & "   AND (JobChecklist_RMStockId=" & CStr(lngStockId) & ")"
            sSql = sSql & "    AND (JobChecklistSequence=" & CStr(intSequence) & ")"
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
                MessageBox.Show(Me, "Failed to get JobsCheckLists recordset.." & vbCrLf & _
                                                "Job Record may be in use..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            Else '--build checklist from saved list for Job..-
                If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                    '== If (rs1.BOF And rs1.EOF) Then '--empty.. no records this checklist..-
                    '-- no checklist yet for this job..-
                    '== Else '--not empty- have some.-
                    '== rs1.MoveLast()
                    '== rs1.MoveFirst()
                    '== GoSub LoadJobChecklist_LoadGrid
                    '--add new grid (v-array) INSTANCE..--
                    Call mLoadJobChecklist_LoadGrid(rs1.Rows.Count, lngFgIndex, vasChecklist)

                    lngCount = 0
                    '-- load fgCheckList FLEXGRID from rs1..--
                    For Each dataRow1 As DataRow In rs1.Rows
                        '--add to list for job..
                        '== lngCount = lngCount + 1
                        sStaff = Trim(dataRow1.Item("JobCheckListStaffName"))
                        sStatus = Trim(dataRow1.Item("JobCheckListStatus"))
                        sComments = Trim(dataRow1.Item("JobCheckListComments"))
                        sDescription = Trim(dataRow1.Item("JobCheckListTaskDescription"))
                        '===With fgCheckList(lngFgIndex)
                        '-- load Item task descr..-
                        '== .TextArray(miFgi(fgCheckList(lngFgIndex), lngCount, k_GRIDCOL_ITEM)) = sDescription
                        vasChecklist(lngCount, 0) = CStr(lngCount + 1) '--item no..-
                        vasChecklist(lngCount, k_GRIDCOL_ITEM) = sDescription
                        If CInt(dataRow1.Item("JobCheckList_StaffId")) > 0 Then '--has been updated.-
                            '--  !!  MUST USE  Alpha Month format..  for various platforms.. !!--
                            '== .TextArray(miFgi(fgCheckList(lngFgIndex), lngCount, k_GRIDCOL_DATE)) =
                            vasChecklist(lngCount, k_GRIDCOL_DATE) = _
                                            VB6.Format(CDate(dataRow1.Item("JobCheckListDateUpdated")), "dd-mmm-yyyy hh:mm")
                            '== .TextArray(miFgi(fgCheckList(lngFgIndex), lngCount, k_GRIDCOL_NAME)) = sStaff
                            vasChecklist(lngCount, k_GRIDCOL_NAME) = sStaff
                            '== .TextArray(miFgi(fgCheckList(lngFgIndex), lngCount, k_GRIDCOL_ORIG_STATUS)) = sStatus
                            vasChecklist(lngCount, k_GRIDCOL_ORIG_STATUS) = sStatus
                            '--remember orig comments for update.
                            '== .TextArray(miFgi(fgCheckList(lngFgIndex), lngCount, k_GRIDCOL_ORIG_COMMENTS)) = sComments
                            vasChecklist(lngCount, k_GRIDCOL_ORIG_COMMENTS) = sComments
                        End If
                        '== NOW IN GRID ..==   ReDim Preserve masCheckListComments(1 To lngCount)
                        '== NOW IN GRID ..==   masCheckListComments(lngCount) = sComments  
                        '--    remember original comments for update..
                        '== .TextArray(miFgi(fgCheckList(lngFgIndex), lngCount, k_GRIDCOL_STATUS)) = sStatus
                        vasChecklist(lngCount, k_GRIDCOL_STATUS) = sStatus

                        '==  APPLY icon when GRID ACTUALLY loaded.
                        '== .TextArray(miFgi(fgCheckList(lngFgIndex), lngCount, k_GRIDCOL_COMMENTS)) = sComments
                        vasChecklist(lngCount, k_GRIDCOL_COMMENTS) = sComments

                        '===End With
                        lngCount += 1 '--actual count.-
                    Next dataRow1
                    '== While (Not rs1.EOF) '---And (cx < 100)
                    '==  rs1.MoveNext()
                    '== End While '-eof-
                    '==  SAVE AFTER LOADING..=
                    mavGridChecklists(lngFgIndex) = VB6.CopyArray(vasChecklist)

                    '== NOT HERE..=
                    '== Call mbNumberGridRows(fgCheckList(lngFgIndex))

                    mbLoadJobChecklist = lngFgIndex '--OK.. return index..-
                    '== End If '--empty..-
                    '== rs1.Close()
                Else '--nothing
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    Exit Function
                End If '--rs nothing-
            End If '--get rs-
        End If '--new item..-
        '-- IF there was no checklist for this job, then load the model checklist..-
        If bNewItem Or (lngFgIndex = -1) Then
            '-new part being added by operator..
            '--load the model for this stock-id into a new Grid....--
            '-- GET MODEL list..-
            lngCount = 0
            sSql = "Select * from [ServiceModelCheckLists]  WHERE (ModelChecklist_RMStockId=" & CStr(lngStockId) & ")"
            If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
                MessageBox.Show(Me, "Failed to get ModelCheckList recordset.." & vbCrLf, _
                                            "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                Exit Function
            Else '--build NEW checklist instance ..-
                If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                    '== If (rs1.BOF And rs1.EOF) Then '--empty.. no model..-
                    '--no model defined..-
                    '== Else '--have some.-
                    '== rs1.MoveLast()
                    '== rs1.MoveFirst()
                    '==GoSub LoadJobChecklist_LoadGrid   '--add new grid INSTANCE..--
                    Call mLoadJobChecklist_LoadGrid(rs1.Rows.Count, lngFgIndex, vasChecklist)
                    '-- load fgCheckList FLEXGRID from rs1..--
                    s1 = "" '--debug-
                    lngCount = 0
                    For Each dataRow1 As DataRow In rs1.Rows
                        '--add to list for job..
                        '== lngCount = lngCount + 1
                        sDescription = Trim(dataRow1.Item("ModelCheckListTaskDescription"))
                        vasChecklist(lngCount, k_GRIDCOL_ITEM) = sDescription '--Item.-
                        vasChecklist(lngCount, k_GRIDCOL_STATUS) = "--"
                        vasChecklist(lngCount, k_GRIDCOL_COMMENTS) = ""
                        vasChecklist(lngCount, k_GRIDCOL_DATE) = ""
                        vasChecklist(lngCount, k_GRIDCOL_NAME) = ""
                        vasChecklist(lngCount, k_GRIDCOL_ORIG_STATUS) = ""
                        vasChecklist(lngCount, k_GRIDCOL_ORIG_COMMENTS) = ""
                        '== NOW IN GRID ..==   masCheckListComments(lngCount) = ""   '--remember original comments for update..
                        lngCount = lngCount + 1
                        s1 = s1 & sDescription & vbCrLf
                    Next dataRow1
                    '== While (Not rs1.EOF) '---And (cx < 100)
                    '==    rs1.MoveNext()
                    '== End While '-eof-
                    '==  SAVE AFTER LOADING..
                    mavGridChecklists(lngFgIndex) = VB6.CopyArray(vasChecklist)

                    '== NOT HERE..  Call mbNumberGridRows(fgCheckList(lngFgIndex))

                    mbLoadJobChecklist = lngFgIndex '--OK.. return index..-
                    '== End If '--empty..-
                    '= rs1.Close()
                End If '--rs nothing-
            End If '--get rs-
        End If '--new..-
        '-- Finish set up grid stuff..-
        mbLoadJobChecklist = lngFgIndex '--PASS BACK CONTROL INDEX..--
        '== End If '--grid..-
        Exit Function

    End Function '--load checklist.--
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  build listBox of Tasks performed on this job so far..--
    '--  build listBox of Tasks performed on this job so far..--

    Private Function mbShowJobTasks() As Boolean
        Dim sSql As String
        Dim s1, sDate As String
        Dim sDateSort As String
        Dim sStaff As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim ix, lngCount As Integer
        Dim item1 As System.Windows.Forms.ListViewItem

        mbShowJobTasks = False
        '==ListTasks.Clear
        ListViewTasks.Items.Clear()
        ListViewTasks.Columns.Clear()
        ListViewTasks.Columns.Add("", "Task Description", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewTasks.Width) \ 2)))
        ListViewTasks.Columns.Add("", "Date", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewTasks.Width) \ 4)))
        ListViewTasks.Columns.Add("", "Tech.", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewTasks.Width) \ 4)))
        '--  make rh col to sort date.--
        ListViewTasks.Columns.Add("", "yyyy-mm", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewTasks.Width) \ 5)))
        '== ListViewTasks.SortKey = 1 '-- 26Jan2010..sorted on date..-

        Erase malTempTasksIndexes '--reflects listbox..--
        Erase mavTempNewTasks '--do-
        sSql = "Select * from [jobtasks] WHERE (taskJob_id=" & CStr(mlJobId) & ")"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
            MessageBox.Show(Me, "Failed to get JobTasks recordset.." & vbCrLf & _
                                              "Job Record may be in use..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Else '--build list box list of tasks performed so far..-
            If Not (rs1 Is Nothing) Then
                '= If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    '--add to list box for job..
                    '===sDate = Format(CDate(rs1("DateCreated")), "yyyy-mm-dd hh:mm")
                    sDate = VB6.Format(CDate(dataRow1.Item("DateCreated")), "dd-mmm-yyyy hh:mm")
                    sDateSort = VB6.Format(CDate(dataRow1.Item("DateCreated")), "yyyy-mm-dd hh:mm")
                    '--s1 = Space(24)
                    '--LSet
                    s1 = Trim(dataRow1.Item("description"))
                    sStaff = Trim(dataRow1.Item("performedByStaffName"))
                    mlTempTasksCount = mlTempTasksCount + 1
                    '--initial scratchpad reflects live table at the start..-
                    ReDim Preserve malTempTasksIndexes(mlTempTasksCount - 1) '-- build scratchpad--
                    malTempTasksIndexes(mlTempTasksCount - 1) = CInt(dataRow1.Item("Task_id")) '--store row id [1..n] with list item..-
                    ReDim Preserve mavTempNewTasks(mlTempTasksCount - 1) '-- build scratchpad--
                    mavTempNewTasks(mlTempTasksCount - 1) = Nothing '-- first lot are not new..--
                    '-- add to listview--
                    item1 = ListViewTasks.Items.Add(s1)
                    '== item1.Text = s1 '--1st column.-
                    item1.SubItems.Add(sDate)
                    item1.SubItems.Add(sStaff)
                    item1.SubItems.Add(sDateSort)
                    item1.Tag = CStr(mlTempTasksCount - 1) '--save index to scratchpad with list item..-
                Next dataRow1
                '== While (Not rs1.EOF) '---And (cx < 100)
                '==  rs1.MoveNext()
                '== End While '-eof-
                mbShowJobTasks = True
                '== rs1.Close()
                '== ListViewTasks.SortKey = 3 '--4th col-  yyyy-mm---
                '--  needs special sort compare object..
                ' Set the ListViewItemSorter property to a new ListViewItemComparer 
                ' object. Setting this property immediately sorts the 
                ' ListView using the ListViewItemComparer object.
                Me.ListViewTasks.ListViewItemSorter = New ListViewMaintItemComparer(3)  '--Zero is FIRST SUB item..--

            End If '--rs-
        End If ''--get rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        rs1 = Nothing
    End Function '--show tasks..-
    '= = = = = = =  =  = =

    '--  build COMBO Box of Tasks TYPES available for jobs..--

    Private Function mbBuildTaskTypes() As Boolean
        Dim sSql As String
        Dim iCount As Short
        Dim s1 As String
        Dim rs1 As DataTable  '=  ADODB.Recordset
        Dim idx As Integer

        mbBuildTaskTypes = False
        ListTaskTypes.Items.Clear()
        '--  get table of task definitions..--
        iCount = 0
        sSql = "Select * from [JobTaskTypes] "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
            MessageBox.Show(Me, "Failed to get JobTaskTypes recordset.." & vbCrLf & _
                               "Job Record may be in use..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else ''-ok-
            '--build combo box of task types available..-
            If Not (rs1 Is Nothing) Then
                '== If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    '--add to list box for job..
                    '--- add type id at end to keep track..--
                    iCount = iCount + 1
                    idx = ListTaskTypes.Items.Add(VB.Left(dataRow1.Item("TaskTypedescription"), 36))
                    '--------- + ";;" + Format(CInt(rs1("TaskType_id")), "00")
                    VB6.SetItemData(ListTaskTypes, idx, CInt(dataRow1.Item("TaskType_id"))) '--store id with list item..-
                Next dataRow1
                '= While (Not rs1.EOF) '---And (cx < 100)
                '== rs1.MoveNext()
                '= End While '-eof-
                ListTaskTypes.Enabled = True
                '-- add special CREATE item..--
                '--If (iCount < 999) Then '--more tasks types can be added..--
                '--      ListTaskTypes.AddItem "ZZ- CREATE new task type definition.."
                '--      ListTaskTypes.ItemData(ListTaskTypes.NewIndex) = 9999  '--store max id with list item..-
                '--End If
                mbBuildTaskTypes = True
                '--LabAddTask.Visible = True
                cmdAddTask.Visible = True
                '= rs1.Close()
            End If '--rs nothing-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If '--get rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        rs1 = Nothing
    End Function '--build.==
    '= = = = = = =  =  = =
    '= = = = = = =  =  = =
    '-===FF->

    '--  build listBox of PARTS added to this job so far..--
    '--  build listBox of PARTS added to this job so far..--

    Private Function mbShowAllParts(ByRef bUpdatePrices As Boolean) As Boolean
        Dim s1, sW As String
        Dim s2, sShowCost As String
        Dim s3 As String
        Dim sBarcode As String
        Dim sSerialNo As String
        Dim intx As Short
        Dim sCat1, sCat2, sDescription As String
        Dim ix, j, i, k, staffIndex As Integer
        Dim sDay As String
        '== Dim sSql As String, sSqlCosts As String
        '== Dim sdCostRows As Scripting.Dictionary
        '== Dim sdAllowRenamingRows As Scripting.Dictionary
        Dim colJobItems As Collection
        Dim colPart As Collection
        Dim colFld As Collection
        '== Dim fld1 As ADODB.Field
        Dim sShortDate As String
        '== Dim rs1 As ADODB.Recordset
        '== Dim rsCheckList As ADODB.Recordset
        Dim lngStockId, lngPartId As Integer
        Dim sStockId As String
        Dim sStaff As String
        Dim sSpecialPrice, sCostPrice As String
        Dim curCost, curTotalParts As Decimal
        Dim decCostPrice, curNewCost As Decimal
        Dim bUpdatedCost As Boolean
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim idxFg As Integer
        Dim sdSequences As New clsStrDictionary  '== Scripting.Dictionary
        Dim vasChecklist(,) As String

        mbShowAllParts = False
        '==ListParts.Clear
        '==   v3.4.3403.0628 -- 28Jun2017= - FIX UP for relaease.
        '==         -- REVSERSE this- JobMaint- CostPrice tagged onto Listview Description-
        '==         --    (make new listView column for Cost-Price)..      

        '---  BUILD BOTH ListView controls..--
        For ix = 0 To 1
            With ListViewParts(ix)
                .Items.Clear()
                .Columns.Clear()
                .Items.Clear()
                .Columns.Add("Cat1", (.Width \ 10), HorizontalAlignment.Left) '-- width..-
                .Columns.Add("Description", (.Width \ 5))  '--40% of width..-
                .Columns.Add("Barcode", (.Width \ 8))
                .Columns.Add("Serial-No", (.Width \ 8))
                .Columns.Add("Wty", (.Width \ 12))
                '= .Columns.Add("StockId", (.Width \ 8), HorizontalAlignment.Right)
                .Columns.Add("Cost-Price", (.Width \ 8), HorizontalAlignment.Right)
                .Columns.Add("Sell-Price", (.Width \ 8), HorizontalAlignment.Right)
                .Columns.Add("Tech.", (.Width \ 10))
                .Columns.Add("SpecialPrice", (.Width \ 10))
                .Columns.Add("StockId", (.Width \ 12), HorizontalAlignment.Right)
            End With
        Next ix
        If mbQuotation Then ListViewParts(k_LV_INDEX_PARTS).SmallImageList = ImageList1 '--for quoted parts..-

        curTotalParts = 0
        '--build list box list of PARTS so far..-

        If gbShowAllParts(mCnnJobs, mRetailHost1, mCurGST, mlJobId, colJobItems, mCurOrigParts, mCurParts) Then
            If (colJobItems.Count() > 0) Then '--not empty..-
                '-- now load parts..-
                '=== rs1.MoveFirst

                '== While (Not rs1.EOF)    '---And (cx < 100)
                For Each colPart In colJobItems
                    '--add to list box for job..
                    '-- RECORDSET -W A S-  from JOBPARTS Table..--
                    lngPartId = CInt(colPart.Item("part_id"))
                    sW = " "
                    '== If (UCase(rs1("IsWarrantyPart")) = "Y") Then sW = "W"
                    If (UCase(colPart.Item("Wty")) = "Y") Then sW = "W"
                    lngStockId = CInt(colPart.Item("stock_id")) '== CLng(rs1("RMStock_id"))
                    sStockId = Trim(CStr(lngStockId))
                    sBarcode = colPart.Item("Barcode") '== Trim(CStr(rs1("RMBarcode")))
                    sSerialNo = colPart.Item("SerialNo") '== Trim(CStr(rs1("PartSerialNumber")))

                    sCat1 = colPart.Item("Cat1") '== rs1("RMCat1")
                    sCat2 = colPart.Item("Cat2") '== rs1("RMCat2")
                    '==s2 = Space(28)

                    '=3403.621- Show CostPrice-inc.--
                    sDescription = colPart.Item("Description")
                    '=3403.607=
                    decCostPrice = colPart.Item("cost")
                    decCostPrice = decCostPrice + ((decCostPrice * mCurGST) / 100)
                    sCostPrice = FormatCurrency(decCostPrice, 2)
                    '=3403.628 NO= =sDescription &= " [CP: " & sCostPrice & " ]"

                    '==LSet s2 = Trim(Left(rs1("RMDescription"), 28))
                    curCost = colPart.Item("Orig_SellPrice") '== CCur(rs1("RMSell"))
                    curNewCost = colPart.Item("Upd_SellPrice") '== CCur(rs1("RMSell"))
                    '==NOW ALREADY includes GST.. '== curNewCost = curNewCost + ((curNewCost * mCurGST) / 100)

                    '--  check latest cost..-
                    sSpecialPrice = "No"
                    bUpdatedCost = False
                    '--check for special price..-
                    If (colPart.Item("allow_renaming") <> 0) Then
                        sSpecialPrice = "Yes"
                    End If
                    If bUpdatePrices Then
                        '--  stock item may have been deleted..--
                        '---  OR JobTracking DB and RM DB can be out of sync.-
                        If (curNewCost <> curCost) And (LCase(sSpecialPrice) <> "yes") Then '--cost changed AND NOT SpecialPrice..-
                            bUpdatedCost = True
                            curCost = curNewCost '--use latest cost..-
                        End If
                    End If '--update..-
                    curTotalParts = curTotalParts + curCost
                    '==sShowCost = Space(10)

                    '--  mark new cost with ASTERISK..--
                    sShowCost = IIf(bUpdatedCost, "* ", "") & FormatCurrency(curCost, 2)
                    '==sStaff = Space(8)
                    sStaff = colPart.Item("servicedByStaffName") '== Left(rs1("servicedByStaffName"), 8)
                    '-- add to temp live parts array..-
                    mlTempPartsCount = mlTempPartsCount + 1
                    '--initial scratchpad reflects live table at the start..-
                    ReDim Preserve malTempPartsIndexes(mlTempPartsCount - 1) '-- build scratchpad--
                    malTempPartsIndexes(mlTempPartsCount - 1) = lngPartId '==CLng(rs1("Part_id")) '--store row id [1..n] with list item..-
                    ReDim Preserve mavTempNewParts(mlTempPartsCount - 1) '-- build scratchpad--
                    mavTempNewParts(mlTempPartsCount - 1) = Nothing '-- first lot are not new..--
                    ReDim Preserve maCurTempPartsCosts(mlTempPartsCount - 1) '-- build parts costs scratchpad--
                    maCurTempPartsCosts(mlTempPartsCount - 1) = curCost '-- first lot are from Table..--
                    ReDim Preserve mabTempPartsCostUpdated(mlTempPartsCount - 1)
                    mabTempPartsCostUpdated(mlTempPartsCount - 1) = bUpdatedCost

                    ReDim Preserve malActiveChecklists(mlTempPartsCount - 1) '-- indexes for l/view checklists..
                    malActiveChecklists(mlTempPartsCount - 1) = -1 '--Say no checklist this part..
                    '-- add to listview--
                    '--  CHOOSE if ServiceCharge or PART..--
                    If Not mbIsServiceCharge(sCat1, sCat2) Then
                        item1 = ListViewParts(k_LV_INDEX_PARTS).Items.Add(sCat1)
                    Else '--yes.-
                        '--load current checklist (if any)..
                        '----  else build new checklist from model..--
                        '--  Since there can be multiple job checklists for any stock-id, each -
                        '----  c/list has a sequence no [1..n] which is in all the records of that c/list..-
                        If sdSequences.Exists(sStockId) Then
                            intx = sdSequences(sStockId) + 1
                            sdSequences(sStockId) = intx '--update for next time.-
                        Else '--none yet for this stock item..-
                            intx = 1
                            sdSequences.Add(sStockId, intx)
                        End If
                        '--load checklist (if any) into new grid..
                        idxFg = mbLoadJobChecklist(False, mlJobId, lngStockId, intx, vasChecklist)
                        If (idxFg >= 0) Then
                            malActiveChecklists(mlTempPartsCount - 1) = idxFg '--YES, checklist this part..
                            '--  boosting of FG "control array"  has been done..
                            '-- and checklist stored at mavGridChecklists(idxFg)--
                        End If
                        item1 = ListViewParts(k_LV_INDEX_SERVICE).Items.Add(sCat1)
                    End If
                    '== item1.Text = sCat1 '== Trim(rs1("RMCat1"))    '--1st column.-
                    item1.SubItems.Add(sDescription) '=(colPart.Item("Description")) '==Trim(rs1("RMDescription"))   '--2nd column.-
                    item1.SubItems.Add(sBarcode)
                    item1.SubItems.Add(sSerialNo)
                    item1.SubItems.Add(sW)
                    '= item1.SubItems.Add(sStockId)
                    item1.SubItems.Add(sCostPrice)
                    item1.SubItems.Add(sShowCost)
                    item1.SubItems.Add(sStaff)
                    item1.SubItems.Add(sSpecialPrice)
                    item1.SubItems.Add(sStockId)    '=3404.909- k_LV_PARTS_STOCK_ID_IDX
                    item1.Tag = CStr(mlTempPartsCount - 1) '--save index to scratchpad with list item..-
                    '== rs1.MoveNext
                Next colPart
                '==Wend  '-eof-
            End If '--count/empty..-
            '-  Show first Service Checklist, if any.-
            If (ListViewParts(k_LV_INDEX_SERVICE).Items.Count > 0) Then  '--have service items..-
                item1 = ListViewParts(k_LV_INDEX_SERVICE).Items(0)
                item1.Selected = True
                Call mbShowChecklist(item1)
            End If
            mbShowAllParts = True
            '== rs1.Close
            '== End If  '--rs-nothing-
        End If '--gbShowAll--
        mCurParts = curTotalParts
        Call mbShowFullCost()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '==Set rs1 = Nothing
        '==Set sdCostRows = Nothing
        '==Set sdSequences = Nothing
        colPart = Nothing
        colJobItems = Nothing
    End Function '-- showParts.--
    '= = = = = = =  =  = =
    '-===FF->

    '--QUOTE-  Show Quoted Parts in "ListViewQuote"..-
    '--QUOTE-  Show Quoted Parts in "ListViewQuote"..-
    '---  and show original Quote/SalesOrderNo..-

    Private Function mbShowQuotedParts() As Boolean
        Dim s1, sW As String
        Dim s2, sShowCost As String
        Dim s3, sBarcode As String
        Dim ix, j, i, k, staffIndex As Integer
        Dim lngWidth As Integer
        Dim sDay As String
        Dim sSql As String
        Dim colFld As Collection
        '== Dim fld1 As ADODB.Field
        Dim sShortDate As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim sStockId As String
        Dim curCost, curTotalParts As Decimal
        Dim item1 As System.Windows.Forms.ListViewItem

        mbShowQuotedParts = False
        '==ListParts.Clear
        With ListViewQuote
            .Items.Clear()
            .Columns.Clear()
            .Items.Clear()
            lngWidth = VB6.PixelsToTwipsX(.Width)
            .Columns.Add("", "Cat1", CInt(VB6.TwipsToPixelsX(lngWidth \ 5)))
            .Columns.Add("", "Description", CInt(VB6.TwipsToPixelsX((lngWidth \ 10) * 3))) '--30% of width..-
            .Columns.Add("", "StockId", CInt(VB6.TwipsToPixelsX(lngWidth \ 8)))
            .Columns.Add("", "Barcode", CInt(VB6.TwipsToPixelsX(lngWidth \ 6)))
            .Columns.Add("", "Sell-Price", CInt(VB6.TwipsToPixelsX(lngWidth \ 8)))
        End With
        '--  set up Extra Parts List Headers  also..-
        With ListViewExtraParts
            .Items.Clear()
            .Columns.Clear()
            .Items.Clear()
            .Columns.Add("", "Cat1", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(.Width) \ 5))) '--1/8th of width..-
            .Columns.Add("", "Description", CInt(VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(.Width) \ 10) * 3))) '--30% of width..-
            .Columns.Add("", "StockId", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(.Width) \ 8)))
            .Columns.Add("", "Barcode", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(.Width) \ 6)))
            .Columns.Add("", "Sell-Price", CInt(VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(.Width) \ 8)))
        End With

        curTotalParts = 0
        ListViewQuote.SmallImageList = ImageList1
        sSql = "Select * from [QuoteJobParts] WHERE (QuotePart_JobId=" & CStr(mlJobId) & ")"
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
            MessageBox.Show(Me, "Failed to get QUOTED JobPARTS recordset.." & vbCrLf & _
                             "Job Record may be in use..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        '--build listview list of PARTS so far..-
        If (Not (rs1 Is Nothing)) And (rs1.Rows.Count > 0) Then
            '== If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
            mlQuoteSalesOrderId = CInt(rs1.Rows(0).Item("QuotePart_OrderId")) '--1st row only..-
            For Each dataRow1 As DataRow In rs1.Rows
                '--add to list box for job..
                '-- RECORDSET is from QUOTED JOBPARTS Table..--
                '== If (rs1.AbsolutePosition = 1) Then
                '=   mlQuoteSalesOrderId = CInt(rs1.Fields("QuotePart_OrderId").Value) '--1st row only..-
                '== End If
                sStockId = Trim(CStr(dataRow1.Item("QuotePart_StockId")))
                sBarcode = Trim(CStr(dataRow1.Item("QuotePartBarcode")))
                curCost = CDec(dataRow1.Item("QuotePart_Sell_inc"))
                curTotalParts = curTotalParts + curCost
                '==sShowCost = Space(10)
                sShowCost = FormatCurrency(curCost, 2)
                '==sStaff = Space(8)
                '=====sStaff = Left(rs1("servicedByStaffName"), 8)
                '-- add to listview--
                s1 = Trim(dataRow1.Item("QuotePartCat1"))  '-- FIXED- 3107.1017==
                item1 = ListViewQuote.Items.Add(s1)
                '== item1.Text = Trim(rs1.Fields("QuotePartCat1").Value)
                item1.SubItems.Add(Trim(dataRow1.Item("QuotePartDescription"))) '--1st column.-
                item1.SubItems.Add(sStockId)
                item1.SubItems.Add(sBarcode)
                '==item1.ListSubItems.Add , , sW
                item1.SubItems.Add(sShowCost)
                '==item1.ListSubItems.Add , , sStaff
                '==item1.Tag = mlTempPartsCount  '--save index to scratchpad with list item..-
                item1.ImageKey = "unchecked"  '=  '==  item1.SmallIcon = 1 '--unchecked..--
            Next dataRow1
            '== While (Not rs1.EOF) '---And (cx < 100)
            '==   rs1.MoveNext()
            '== End While '-eof-
            mbShowQuotedParts = True
            '= rs1.Close()
        End If '--rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        rs1 = Nothing
    End Function '-- show Quoted Parts.--
    '= = = = = = =  =  = =
    '-===FF->

    '-- show updated QUOTE/PARTS Status.--
    '---- Show Comparison of QuotedParts vs actual Parts..-
    '------  Build array of stockIds to shadow quote (listViewQuote).--
    '-------   with sibling array of booleans to represent used/not-used..--
    '------ Traverse listViewParts and tick off quote array items as used.--
    '------ Set CheckedUnchecked Icon in listViewQuote according to used/not-used aarray..--

    Private Function mbShowQuotedPartsStatus() As Boolean

        Dim alQuoteStockIds() As Integer
        Dim alPartsExtras() As Integer '--listViewParts indexes..--
        Dim abSupplied() As Boolean
        Dim qx, ix, id1, px, sx As Integer
        Dim lngExtras As Integer
        Dim item1, item2 As System.Windows.Forms.ListViewItem
        Dim sList As String

        If Not mbQuotation Then Exit Function
        If (ListViewQuote.Items.Count > 0) Then '--have items in quote.--
            '--build arrays..-
            Erase alQuoteStockIds : Erase alPartsExtras
            Erase abSupplied
            lngExtras = 0
            With ListViewQuote
                For qx = 0 To (.Items.Count - 1)
                    item1 = .Items.Item(qx)
                    id1 = CInt(item1.SubItems(2).Text) '--get stock id for this quote part..- 
                    ReDim Preserve alQuoteStockIds(qx) '--Build stock id array for this quote..-
                    alQuoteStockIds(qx) = id1
                    ReDim Preserve abSupplied(qx)
                    abSupplied(qx) = False '--mark all initially as NOT supplied..
                Next qx
            End With
            '--  match parts supplied with quote items..--

            '--  SHOULD Look in BOTH listviews..
            '==  AND BUILD ONE list (collection) of extra parts..-
            If (ListViewParts(k_LV_INDEX_PARTS).Items.Count > 0) Then '--have parts supplid.--
                With ListViewParts(k_LV_INDEX_PARTS)
                    For sx = 0 To (.Items.Count - 1)
                        item1 = .Items(sx)
                        '=3404.909- k_LV_PARTS_STOCK_ID_IDX
                        '= id1 = CInt(item1.SubItems(5).Text) '--get stock id for this quote part..-
                        id1 = CInt(item1.SubItems(k_LV_PARTS_STOCK_ID_IDX).Text) '--get stock id for this quote part..-
                        '--  find a matching FREE quote part.-
                        '---- If not..  add to Extra Parts list..-
                        For qx = 0 To UBound(alQuoteStockIds)
                            If (id1 = alQuoteStockIds(qx)) And (Not abSupplied(qx)) Then '--not supplied..-
                                '--mark as supplied..-
                                abSupplied(qx) = True
                                id1 = -1 '--say part found in quote..
                                Exit For
                            End If
                        Next qx
                        If (id1 > 0) Then '--not found in quote..-
                            '--add to extras list..
                            lngExtras = lngExtras + 1
                            ReDim Preserve alPartsExtras(lngExtras)
                            alPartsExtras(lngExtras) = sx '-- parts itemlist index--
                            item1.ImageKey = "alert" '== item1.SmallIcon = "alert" '--extra to quotre..-
                        Else '--quoted..-
                            item1.ImageKey = "checked"  '==item1.SmallIcon = "checked" '--ticked.-
                        End If '==not found.-
                    Next sx
                End With '--ListViewParts--
                '-- Set all icons in quote parts.-
                With ListViewQuote
                    For qx = 0 To (.Items.Count - 1)
                        item1 = .Items.Item(qx)
                        If abSupplied(qx) Then
                            item1.ImageKey = "checked"  '==3063.0=  2  '== item1.SmallIcon = 2 '--checked.-
                        Else
                            item1.ImageKey = "unchecked"  '==3063.0=  1   '==  item1.SmallIcon = 1 '--UNchecked.-
                        End If
                    Next qx
                End With '--quote.-
                '-- build listview of extra parts if any..-
                ListViewExtraParts.Items.Clear()
                If lngExtras > 0 Then
                    For sx = 0 To (lngExtras - 1)
                        px = alPartsExtras(sx) '-- get parts index..-
                        item1 = ListViewParts(k_LV_INDEX_PARTS).Items(px) '--this part couldn't be found in quote.-
                        item2 = ListViewExtraParts.Items.Add(item1.Text) '--add a row..
                        '==item2.Text = item1.Text '--copy Cat1.-

                        item2.SubItems.Add(item1.SubItems.Item(1).Text) '-- copy Descr...
                        '= item2.SubItems.Add(item1.SubItems.Item(5).Text) '-- copy stockid..
                        '=3404.909- k_LV_PARTS_STOCK_ID_IDX
                        item2.SubItems.Add(item1.SubItems.Item(k_LV_PARTS_STOCK_ID_IDX).Text) '-- copy stockid..
                        item2.SubItems.Add(item1.SubItems.Item(2).Text) '--copy barcode..-
                        item2.SubItems.Add(item1.SubItems.Item(6).Text) '--copy SellPrice..-
                    Next sx
                End If '--extras..
                sList = ""
                For qx = 0 To UBound(alQuoteStockIds)
                    If Not abSupplied(qx) Then '--not yet supplied..-
                        If sList <> "" Then sList = sList & "; "
                        sList = sList & alQuoteStockIds(qx)
                    End If
                Next qx
                If (sList <> "") Then sList = "Quoted Parts not supplied (Stock_Ids):" & vbCrLf & sList & vbCrLf
                '--  show reconciliation summary.--
                If (lngExtras > 0) Then sList = sList & "Extra Parts: " & vbCrLf & lngExtras & " parts not quoted.."
                LabReconciliation.Text = sList

            End If '--parts count..-
        End If '--quote count..-

    End Function '--show quote..-
    '= = = = = = =  =  = =
    '-===FF->

    '--3203-110-  NEW-- Build Items Supplied Lines Collection
    '--    For Receipt Docket and Customer Report..
    '--PARTS AND CHARGES --
    '-- colLines may already be populated with stuff to keep..

    Private Function mbBuildItemsSuppliedList(ByVal intLinesSize As Integer, _
                                              ByRef colLines As Collection) As Boolean
        '== Dim colLines As New Collection
        Dim s1, s2, sSell, sDescr As String
        Dim item1 As ListViewItem
        Dim ix, idxTemp, idxGrid, rx, cx As Integer
        Dim vasChecklist As Object

        '--PARTS AND CHARGES --
        If (ListViewParts(k_LV_INDEX_SERVICE).Items.Count > 0) Then
            colLines.Add("<bold>")
            colLines.Add("Service Charge Items:") '--new line..--
            ix = 0
            For Each item1 In ListViewParts(k_LV_INDEX_SERVICE).Items
                colLines.Add("<lucida>")
                ix += 1
                '== item1 = ListViewParts(k_LV_INDEX_SERVICE).Items(ix)
                '-- show Descr, barcode and SerialNo.-
                s1 = New String(" ", intLinesSize)
                sDescr = VB.Left(CStr(ix) & ". " & item1.SubItems(1).Text, 34)
                sSell = Trim(item1.SubItems(6).Text) '--Sell-inc.. 
                s1 = RSet(sSell, Len(s1))
                Mid(s1, 1, Len(sDescr)) = sDescr
                colLines.Add(s1)
                '--  show checklist if any..--
                idxTemp = CInt(item1.Tag) '--get scratchpad index..-
                If (idxTemp > 0) Then
                    idxGrid = malActiveChecklists(idxTemp) '--get control array index..-
                    If (idxGrid >= 0) Then
                        '--only if we have a checklist grid..-
                        '--  print all checklist items..-- (Descr[Action] )
                        vasChecklist = mavGridChecklists(idxGrid)
                        If IsArray(vasChecklist) Then
                            For rx = 0 To UBound(vasChecklist, 1) '== .FixedRows To .Rows - 1
                                s2 = Trim(vasChecklist(rx, k_GRIDCOL_STATUS))
                                '====If (s2 <> "") Then '--must have action.-
                                If (InStr(LCase(s2), "completed") > 0) Then '--must be completed..-
                                    '===  colLines.Add "<lucida>"
                                    colLines.Add("<fontsize=7>")
                                    s1 = Trim(vasChecklist(rx, k_GRIDCOL_ITEM)) & " [" & s2 & "]"
                                    colLines.Add(" - " & s1)
                                End If '--s2.-
                            Next rx
                        End If '--is array-
                    End If '--idxGrid..-
                End If '--idxtemp.-
            Next  '-item1-
            colLines.Add("")
        Else '--no parts.
            colLines.Add("<bold>")
            colLines.Add("(No Service Charge Items.)") '--new line..--
            colLines.Add("")
            '==== colLines.Add "<ul>NO Item Supplied:" + vbCrLf + vbCrLf
        End If '-list-
        '--parts..--
        If (ListViewParts(k_LV_INDEX_PARTS).Items.Count > 0) Then
            colLines.Add("<bold>")
            ix = 0
            colLines.Add("Other Items Supplied:") '--new line..--
            For Each item1 In ListViewParts(k_LV_INDEX_PARTS).Items
                ix += 1
                colLines.Add("<lucida>")
                '==  item1 = ListViewParts(k_LV_INDEX_PARTS).Items(ix)
                '-- show Descr, barcode and SerialNo.-
                s1 = New String(" ", intLinesSize)
                sDescr = VB.Left(CStr(ix) & ". " & Trim(item1.Text) & ": " & item1.SubItems(1).Text, 34)
                sSell = Trim(item1.SubItems(6).Text) '--Sell-inc..
                s1 = RSet(sSell, Len(s1))
                Mid(s1, 1, Len(sDescr)) = sDescr
                colLines.Add(s1)
                '== Next ix
            Next  '--item1-     '==colLines.Add ""
        Else '--no parts.
            '===colLines.Add "<lucida>"
            colLines.Add("<ul>")
            colLines.Add(">No Part Supplied:")
        End If '-list-

        '--show total value..--
        colLines.Add("<lucida>")
        colLines.Add("- - - - - - - - - - - - - - - - - - - - -") '--new line..--
        sSell = FormatCurrency(mCurParts, 2)
        s1 = New String(" ", intLinesSize)
        s1 = RSet(sSell, Len(s1))
        Mid(s1, 1, Len("  == Total Item Value:")) = "  == Total Item Value:"
        colLines.Add("<lucida>")
        colLines.Add(s1)
        colLines.Add("<lucida>")
        colLines.Add("- - - - - - - - - - - - - - - - - - - - -") '--new line..--

    End Function  '--ItemsSupplied-
    '= = = = = = =  =  = = = = = = = = =
    '-===FF->

    '--  NEW-- Print the Job form.--
    '-- NEW-- Print the Job form.--

    Private Function mbPrintJobMaintForm() As Integer

        Dim prtDocs1 As clsPrintDocs
        Dim sx, ix, iPos, kx, lResult As Integer
        Dim s1, s2 As String
        Dim sName As String
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim sJobRequirements As String
        Dim sFullPath As String
        Dim sUsernames As String
        Dim colBusiness As Collection
        Dim colCustomer As Collection
        Dim colResultGoodsInCare As Collection
        Dim colTasksCompleted As Collection
        Dim colServiceItems As Collection
        Dim colPartsItems As Collection
        Dim col1 As New Collection


        mbPrintJobMaintForm = -1
        '==mbPrintNewJobForm = -1
        '== Call miGetPriority
        prtDocs1 = New clsPrintDocs
        '== prtDocs1.PrtSelectedPrinter = mPrtColour
        prtDocs1.PrtSelectedPrinterName = msColourPrinterName

        iPos = InStr(LabTicket.Text, ":")
        If iPos > 1 Then
            '=== 3203.120= prtDocs1.TicketDate = VB.Left(LabTicket.Text, iPos - 1) '--"Sep-09"
            prtDocs1.TicketDate = Trim(VB.Mid(LabTicket.Text, iPos + 1)) '--"Sep-2016"
        End If '--iPos..-
        sName = msStaffName
        If VB.Left(msJobStatus, 2) >= "70" Then '-delivered.- 
            s2 = "Delivered By: " '--stay on this line..-
            sName = _txtStaffName_1.Text
            If Not IsDBNull(mColJobFields.Item("datedelivered")("value")) Then
                s1 = FormatDateTime(mColJobFields.Item("datedelivered")("value"))
            End If '--null-
        Else '--not delivered..-
            s2 = "Updated By: " '--stay on this line..-
            sName = msStaffName  '=3501.1001=  _txtStaffName_0.Text
            s1 = msDateUpdated '==LabDateUpdated.Text
            '==s2 = "Received By: "  '--stay on this line..-
            '==s1 = LabDateRcvd.Caption
        End If

        prtDocs1.Version = LabVersion.Text
        prtDocs1.UserLogo = Picture2.Image
        prtDocs1.ExtraPicture = PictureExtraPrint.Image

        prtDocs1.JobNo = mlJobId
        prtDocs1.JobStatus = msJobStatus

        '==  prtDocs1.LicenceOK = mbLicenceOK

        prtDocs1.IsQuotation = mbQuotation
        '= 3203.120=
        prtDocs1.SystemUnderWarranty = mbSystemUnderWarranty

        prtDocs1.HeaderDate = "Received By: " & LabRcvdStaff.Text & ";  " & _
                                             labDateCreated.Text & "..  " & s2 & sName & Space(12) & s1
        prtDocs1.PriorityColour = System.Drawing.ColorTranslator.ToOle(LabTicket.BackColor)

        '--load business info..-
        colBusiness = New Collection
        colBusiness.Add(msBusinessName, "BusinessName")
        colBusiness.Add(msBusinessShortName, "BusinessShortName")
        colBusiness.Add(msBusinessAddress1, "BusinessAddress1")
        colBusiness.Add(msBusinessAddress2, "BusinessAddress2")
        colBusiness.Add(msBusinessState, "BusinessState")
        colBusiness.Add(msBusinessPostCode, "BusinessPostcode")
        colBusiness.Add(msBusinessEmail, "BusinessEmail")
        '-- Format ABN for printing..-
        Dim sABN As String = VB.Left(msABN, 2) & " " & Mid(msABN, 3, 3) & " " & Mid(msABN, 6, 3) & " " & Mid(msABN, 9, 3)
        colBusiness.Add(sABN, "BusinessABN")
        prtDocs1.Business = colBusiness

        prtDocs1.LabourMinCharge = mCurLabourMinCharge
        prtDocs1.LabourHourlyRate = mCurLabourHourlyRate
        '====prtDocs1.NotificationCostLimit = mCurNotificationCostLimit
        prtDocs1.ItemBarcodeFontName = msItemBarcodeFontName
        prtDocs1.ItemBarcodeFontSize = mlItemBarcodeFontSize

        '--load cust info..-
        colCustomer = New Collection
        colCustomer.Add(msCustomerBarcode, "CustomerBarcode")
        colCustomer.Add(msCustomerPrint, "CustomerPrint")

        colCustomer.Add("..", "CustomerName")
        colCustomer.Add("..", "CustomerCompany")
        colCustomer.Add(msCustomerPhone, "CustomerPhone")
        colCustomer.Add(msCustomerMobile, "CustomerMobile")

        colCustomer.Add(labPriority.Text, "CustomerPriorityText")
        colCustomer.Add(txtNomTech.Text, "CustomerTechName")
        prtDocs1.Customer = colCustomer

        '--load goods info..-
        '== Set colResultGoodsInCare = mColCollectGoodsInCare

        colResultGoodsInCare = New Collection
        '==colResultGoodsInCare.Add txtGoods.Text
        If mbQuotation Then
            colResultGoodsInCare.Add("<b>Quotation No: " & mlQuoteSalesOrderId & vbCrLf)
        Else '--normal-
            colResultGoodsInCare.Add(Replace(txtGoods.Text, vbTab, "; ") & vbCrLf)
        End If
        prtDocs1.ResultGoods = colResultGoodsInCare
        '== prtDocs1.ExtrasInCare = txtGoodsOther.Text + vbCrLf + vbCrLf + msExtrasInCare

        prtDocs1.JobReturned = (chkReturned.CheckState = 1)

        '--  everything was packed into txtSymptoms..--
        If Not mbQuotation Then
            sJobRequirements = "<ul>Problems Reported" & vbCrLf & vbCrLf
            sJobRequirements = sJobRequirements & txtSymptoms.Text & vbCrLf & vbCrLf
            If (ChkBackupReq.CheckState = 1) Then '--data backup..-
                sJobRequirements = sJobRequirements & "* Data Backup Required.   " & vbCrLf
            End If '--backup.-
            If (ChkRecovDisk.CheckState = 1) Then '--data backup..-
                sJobRequirements = sJobRequirements & "* Recovery Disk $15.00 " & vbCrLf
            End If '--backup.-
            If Trim(txtDiagnosis.Text) <> "" Then
                sJobRequirements = sJobRequirements & "<ul>Diagnosis" & vbCrLf & txtDiagnosis.Text & vbCrLf & vbCrLf
            End If
        Else '--quote..-
            sJobRequirements = sJobRequirements & "<ul>Special Comments:" & vbCrLf & txtDiagnosis.Text & vbCrLf & vbCrLf
        End If '--not quote--
        prtDocs1.Problem = sJobRequirements

        '-- Tasks--
        '--  Convert Tasks listview into Collection..-
        If mbMakeListViewCollection(ListViewTasks, colTasksCompleted) Then
            prtDocs1.TasksCompleted = colTasksCompleted
        Else
            MessageBox.Show(Me, "Failed to build Tasks list/view collection..", _
                              "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        '-- send labour cost, if any..--
        If (mCurTotalChargeableHours > 0) Then
            prtDocs1.ShowLabourCost = LabTotalTime.Text
        Else
            prtDocs1.ShowLabourCost = ""
        End If

        '--  Build collection of Service Items..--
        '==Set colServiceItems = New Collection
        If mbMakeListViewCollection(ListViewParts(k_LV_INDEX_SERVICE), colServiceItems) Then
            prtDocs1.ServiceItems = colServiceItems
        Else
            MessageBox.Show(Me, "Failed to build Service list/view collection..", _
                              "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        '--  Build collection of PARTS Items..--
        If mbMakeListViewCollection(ListViewParts(k_LV_INDEX_PARTS), colPartsItems) Then
            prtDocs1.PartsItems = colPartsItems
        Else
            MessageBox.Show(Me,
                        "Failed to build PartsItems list/view collection..",
                          "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        '==
        '== Target-New-Build-6201 --  (15-July-2021) for Open Source..
        '==    -- Add CheckBox "Print Item Barcodes" to JobMaint Form (print section).
        '==    --  In clsPrintDocs, print the item barcode list only if requested.
        '= = = =
        prtDocs1.CanPrintJobItemBarcodes = chkPrintItemBarcodes.Checked
        '== END Target-New-Build-6201 --  (15-July-2021) for Open Source..

        '--total items cost.-
        prtDocs1.TotalLabourValue = mCurLabour  '==3107.518=
        prtDocs1.TotalItemValue = mCurParts
        '==  s1 = FormatCurrency(mCurParts, 2)
        '==  sText = sText & "<b> === Total Item Value: " & s1 & vbCrLf

        '-- Notifications..--
        prtDocs1.Notifications = txtNotification.Text
        s1 = ""
        '--Work History..-
        '-- Fixed 3501.1001- 
        If (Trim(txtWorkHistory.Text) <> "") Or (Trim(txtWorkDetails.Text) <> "") Then
            s2 = msStaffName & ": " & Format(Now, "dd-MMM-yyyy hh:mm tt ")  '--staff/date prefix..-
            s1 = Trim(Replace(txtWorkHistory.Text, vbCrLf & vbCrLf, vbCrLf)) & vbCrLf
            If mbService Then
                s1 &= "-- Updated By: " & s2 & vbCrLf & Trim(txtWorkDetails.Text) & vbCrLf '--if any..--
            End If
        End If
        prtDocs1.WorkHistory = s1

        '-- go..--
        mbPrintJobMaintForm = prtDocs1.PrintJobMaintForm

        prtDocs1 = Nothing
    End Function '--PrintJobMaintForm- (NEW)--
    '= = = = = = =  =  = = = = = = =  = = =
    '-===FF->

    '--  Build Delivery Docket-lines collection..--

    Private Function mbBuildDocket(ByRef lngJobNo As Integer, ByRef colLines As Collection) As Boolean
        Dim sShortDate As String
        Dim sABN As String
        Dim s1, sDescr As String
        Dim s2 As String
        Dim ix, L1, rx, intPos As Integer
        Dim idxTemp, idxGrid As Integer
        Dim sLine As String
        Dim sSell As String
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim vasChecklist As Object

        '== On Error GoTo Docket_error

        colLines = New Collection
        sShortDate = VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy ") & FormatDateTime(TimeOfDay, DateFormat.ShortTime)

        If (Not mbDelivery) And (VB.Left(msJobStatus, 2) >= "70") Then '--prev. delivered.-
            If Not IsDBNull(mColJobFields.Item("datedelivered")("value")) Then
                sShortDate = FormatDateTime(mColJobFields.Item("datedelivered")("value"))
            End If
        End If
        '-- Format ABN for printing..-
        sABN = VB.Left(msABN, 2) & " " & Mid(msABN, 3, 3) & " " & Mid(msABN, 6, 3) & " " & Mid(msABN, 9, 3)
        colLines.Add("") '--new line..--
        colLines.Add("") '--new line..--
        colLines.Add("<bold>")
        '--colLines.Add "<bold>"
        colLines.Add("Delivery Receipt: " & sShortDate)
        If (Not mbDelivery) And (VB.Left(msJobStatus, 2) >= "70") Then '--prev. delivered.-
            colLines.Add("<bold>")
            colLines.Add("- R e p r i n t - ")
        End If '-prev.-
        colLines.Add("") '--new line..--
        colLines.Add("<big>")
        colLines.Add("<bold>")
        If (msBusinessName <> "") Then
            colLines.Add(msBusinessName) '--"Precise PCs"
        Else
            colLines.Add("- JobMatix JobTracking -")
        End If
        colLines.Add("<bold>")
        '====colLines.Add "<ul>"
        colLines.Add("ABN: " & sABN & ".")
        '===colLines.Add ""
        '---Precise; 82 563 967 866.--
        colLines.Add(msBusinessAddress1) '--"13 Commercial Rd."
        colLines.Add(msBusinessAddress2) '---"Murwillumbah NSW 2484."
        colLines.Add(msBusinessState & "  " & msBusinessPostCode & " Australia.")
        colLines.Add("Telephone: " & msBusinessPhone)
        colLines.Add("Email: " & msBusinessEmail)
        '=====colLines.Add "Tel: (02) 6672 8300."
        '--colLines.Add sShortDate
        colLines.Add("Served by: " & msStaffName)
        colLines.Add("")
        colLines.Add("<bold>")
        colLines.Add("Customer:")
        s1 = Replace(msCustomerPrint, ")", "")
        s1 = Replace(s1, "(", vbCrLf) '-- company on next line if any.-
        If (s1 <> "") Then
            colLines.Add(s1)
        Else
            colLines.Add(" - ?? -")
        End If
        '=3203.210
        colLines.Add("") '--new line..--
        If mbSystemUnderWarranty Then
            colLines.Add("<bold>")
            colLines.Add("U n d e r  W a r r a n t y")
            colLines.Add("") '--new line..--
        End If
        colLines.Add("<bold>")
        colLines.Add("Priority: " & labPriority.Text)
        colLines.Add("") '--new line..--
        '-- put all goods together..
        colLines.Add("<bold>")
        colLines.Add("Goods Delivered:") '--new line..--
        '---colLines.Add "Brand: " + LabBrand.Caption + " (" + Labmodel.Caption + ")"
        s1 = txtGoods.Text
        intPos = InStr(UCase(s1), "USERNAMES")
        If (intPos > 1) Then
            '-- drop usernames-
            s1 = VB.Left(s1, intPos - 1)
        ElseIf intPos = 1 Then
            s1 = ""  '-no goods, only names.
        End If
        colLines.Add(Replace(s1, vbTab, "; ")) '--- Labgoods.Caption + " " + LabGoodsOther.Caption
        colLines.Add("") '--new line..--
        colLines.Add("<bold>")
        colLines.Add("Problems Reported:") '--new line..--
        colLines.Add(txtSymptoms.Text)
        colLines.Add("") '--new line..--
        If (txtDiagnosis.Text <> "") Then
            colLines.Add("<bold>")
            colLines.Add("Diagnosis:") '--new line..--
            colLines.Add(txtDiagnosis.Text)
        End If
        colLines.Add("<bold>")
        '==colLines.Add "<ul>"

        '---  SHOW CHECKLISTS UNDER SERVICE ITEMS LIST..-

        '--PARTS AND CHARGES --
        '--3203.10-  now a function.--
        Call mbBuildItemsSuppliedList(42, colLines)

        '-  NB! ListView.ListItems collection items are numbered from 1..n--
        If (ListViewTasks.Items.Count > 0) Then
            colLines.Add("<bold>")
            colLines.Add("Other Tasks Performed:") '--new line..--
            ix = 0
            For Each item1 In ListViewTasks.Items
                '== For ix = 1 To (ListViewTasks.Items.Count)
                ix += 1
                '== item1 = ListViewTasks.Items.Item(ix)
                colLines.Add("-- " & item1.Text & " (" & item1.SubItems(2).Text & ")" & vbCrLf)
                '== Next ix
            Next item1
            colLines.Add("")
        End If '-list-

        '-- add labour cost, if any..--
        If (mCurTotalChargeableHours > 0) Then
            colLines.Add(vbCrLf & LabTotalTime.Text & vbCrLf)
            colLines.Add("")
        Else
            '===sText = sText & vbCrLf & "No Chargeable labour time.." & vbCrLf
        End If
        '--colLines.Add "Your Reference:"
        '===colLines.Add "<times>"
        colLines.Add("<bold>")
        colLines.Add("<big>")
        '--colLines.Add "<bold>"
        colLines.Add("Your Reference: " & CStr(lngJobNo) & ".")
        colLines.Add("Thank You.")
        '== colLines.Add("<lucida>")
        colLines.Add("= = = = = = = = = = = = = = = = = = = = =") '--new line..--
        colLines.Add(msDeliveryDocketFootnote)
        colLines.Add("= = = = = = = = = = = = = = = = = = = = =") '--new line..--
        mbBuildDocket = True
        Exit Function

Docket_error:
        L1 = Err().Number
        MessageBox.Show(Me, "Runtime Error in Build-Docket.." & vbCrLf & _
                          "Error=" & L1 & ": " & ErrorToString(L1), _
                            "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

    End Function '--docket--
    '= = = = = = =  =  = =
    '-===FF->

    '--  NEW..--
    '--   PrintReceipt  --
    '--   PrintReceipt  --

    Private Function mbPrintReceipt(ByRef colReportLines As Collection) As Boolean
        Dim prtDocs1 As clsPrintDocs

        prtDocs1 = New clsPrintDocs
        prtDocs1.Version = LabVersion.Text

        prtDocs1.UserLogo = Picture2.Image
        '== prtDocs1.PrtSelectedPrinter = mPrtReceipt
        prtDocs1.PrtSelectedPrinterName = msReceiptPrinterName

        mbPrintReceipt = prtDocs1.PrintReceipt(colReportLines)


    End Function
    '= = = = = = =  = = = =  = = =

    '--3203.116=
    '-   Collect IDs of Selected Images..

    Private Function mIntGetSelectedImageIds(ByRef listImages As List(Of Integer)) As Integer

        '-- get list of checked images if any..
        Dim listImageIDs As New List(Of Integer)
        Dim bHaveImagesOnFile As Boolean = False
        mIntGetSelectedImageIds = -1  '--nothing on file..-

        If (lvwDocs.Items.Count > 0) Then
            For Each item1 As ListViewItem In lvwDocs.Items
                If mClsAttachments1.IsImageFile(item1.SubItems(2).Text) Then '=3311.227=
                    bHaveImagesOnFile = True
                    If item1.Checked Then  '--file is image-
                        listImageIDs.Add(CInt(item1.Text)) '--save doc-id=
                    End If
                End If
            Next  '-item-
            If bHaveImagesOnFile Then
                mIntGetSelectedImageIds = listImageIDs.Count
            End If
        End If  '-count-
        listImages = listImageIDs

    End Function  '-get image IDs
    '= = = = = = = = = = = = =  = = = = = =
    '-===FF->

    '--  NEW NEW NEW 3202.110..-- 10jan2016=
    '--   Print Customer A4 Report  --
    '--   Print Customer A4 Report  --

    Private Function mbPrintCustomerReport() As Boolean
        '= Dim colReportLines As Collection
        Dim s1, s2 As String
        Dim sWorkHistory As String = ""
        Dim colBusiness, colCustomer, colResultGoodsInCare As Collection
        Dim dtPictures As DataTable   '--pics for this job..

        Dim prtDocs1 As clsPrintDocs
        Dim colServiceItems As Collection
        Dim colPartsItems As Collection
        '=Dim sABN As String
        Dim sJobRequirements As String = ""
        Dim sShowLabourCost As String = ""
        Dim asFileTitles() As String
        Dim listImages As List(Of Image)

        mbPrintCustomerReport = False
        '--load business info..-
        colBusiness = New Collection
        colBusiness.Add(msBusinessName, "BusinessName")
        colBusiness.Add(msBusinessShortName, "BusinessShortName")
        colBusiness.Add(msBusinessAddress1, "BusinessAddress1")
        colBusiness.Add(msBusinessAddress2, "BusinessAddress2")
        colBusiness.Add(msBusinessState, "BusinessState")
        colBusiness.Add(msBusinessPostCode, "BusinessPostcode")
        colBusiness.Add(msBusinessEmail, "BusinessEmail")
        '-- Format ABN for printing..-
        Dim sABN As String = VB.Left(msABN, 2) & " " & Mid(msABN, 3, 3) & " " & Mid(msABN, 6, 3) & " " & Mid(msABN, 9, 3)
        colBusiness.Add(sABN, "BusinessABN")
        '==prtDocs1.Business = colBusiness

        '--load cust info..-
        colCustomer = New Collection
        colCustomer.Add(msCustomerBarcode, "CustomerBarcode")
        colCustomer.Add(msCustomerPrint, "CustomerPrint")

        colCustomer.Add("..", "CustomerName")
        colCustomer.Add("..", "CustomerCompany")
        colCustomer.Add(msCustomerPhone, "CustomerPhone")
        colCustomer.Add(msCustomerMobile, "CustomerMobile")

        colCustomer.Add(labPriority.Text, "CustomerPriorityText")
        colCustomer.Add(txtNomTech.Text, "CustomerTechName")
        '== prtDocs1.Customer = colCustomer

        '--load goods info..-
        colResultGoodsInCare = New Collection
        '==colResultGoodsInCare.Add txtGoods.Text
        Dim intPos As Integer
        If mbQuotation Then
            colResultGoodsInCare.Add("Quotation No: " & mlQuoteSalesOrderId & vbCrLf)
        Else '--normal-
            s1 = txtGoods.Text
            intPos = InStr(UCase(s1), "USERNAMES")
            If (intPos > 1) Then
                '-- drop usernames-
                s1 = VB.Left(s1, intPos - 1)
            ElseIf intPos = 1 Then
                s1 = ""  '-no goods, only names.
            End If
            colResultGoodsInCare.Add(Replace(s1, vbTab, "; ") & vbCrLf)
            sJobRequirements = txtSymptoms.Text
        End If
        '-- get parts.-
        '--PARTS AND CHARGES --
        '--3203.10-  now a function.--
        '= colReportLines = New Collection
        '= Call mbBuildItemsSuppliedList(42, colReportLines)

        '--  Build collection of Service Items..--
        '==Set colServiceItems = New Collection
        If mbMakeListViewCollection(ListViewParts(k_LV_INDEX_SERVICE), colServiceItems) Then
            '= prtDocs1.ServiceItems = colServiceItems
        Else
            MessageBox.Show(Me, "Failed to build Service list/view collection..", _
                               "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        '--  Build collection of PARTS Items..--
        If mbMakeListViewCollection(ListViewParts(k_LV_INDEX_PARTS), colPartsItems) Then
            '= prtDocs1.PartsItems = colPartsItems
        Else
            MessageBox.Show(Me, _
                            "Failed to build PartsItems list/view collection..", _
                               "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        '-- send labour cost, if any..--
        If (mCurTotalChargeableHours > 0) Then
            sShowLabourCost = LabTotalTime.Text
        End If
        '--total items cost.-
        '== prtDocs1.TotalLabourValue = mCurLabour  '==3107.518=
        '== prtDocs1.TotalItemValue = mCurParts


        prtDocs1 = New clsPrintDocs
        prtDocs1.Version = LabVersion.Text

        '= prtDocs1.UserLogo = Picture2.Image
        '--Work History..-
        If (Trim(txtWorkHistory.Text) <> "") Or (Trim(txtWorkDetails.Text) <> "") Then
            sWorkHistory = Trim(Replace(txtWorkHistory.Text, vbCrLf & vbCrLf, vbCrLf)) & vbCrLf & _
                                                                             Trim(txtWorkDetails.Text) '--if any..--
        End If  '-history-
        '-- get list of checked images if any..
        Dim listImageIDs As List(Of Integer)
        '= If (lvwDocs.Items.Count > 0) Then
        '= For Each item1 As ListViewItem In lvwDocs.Items
        '=  If item1.Checked And gbIsImageFile(item1.SubItems(2).Text) Then  '--file is image-
        '=    listImageIDs.Add(CInt(item1.Text)) '--save doc-id=
        '=  End If
        '= Next  '-item-
        '= End If
        Call mIntGetSelectedImageIds(listImageIDs)
        '--get all images if any--
        If (listImageIDs.Count > 0) Then
            Dim intCount As Integer = mClsAttachments1.GetSelectedImages(mlJobId, listImageIDs, asFileTitles, listImages)
        Else
            '--send empty list to print..
            listImages = New List(Of Image)  '-empty-
            asFileTitles = {}
        End If
        '==
        '==  3203.218- 18Feb2016- >>- FIX- Customer Report was going to the Receipt printer
        '==                       >>-     Now sending it to A4 Colour printer.
        '==
        mbPrintCustomerReport = prtDocs1.PrintJobMaintCustomerReport(colBusiness, colCustomer, mlJobId, mbQuotation, _
                                                                     colResultGoodsInCare, _
                                                                      sJobRequirements, _
                                                                      colServiceItems, colPartsItems, _
                                                                      sShowLabourCost, mCurLabour, mCurParts, _
                                                                       sWorkHistory, msDeliveryDocketFootnote, Picture2.Image, _
                                                                        asFileTitles, listImages, msColourPrinterName)
        '-- Done..

    End Function  '--cust report-
    '= = = = = = =  = = = =  = = =

    '-===FF->

    '--  update job record when service stuff enterered..--
    '---            ie SAVE-> BeginTrans,
    '---                        Apply scratchpad changes to Tasks/Parts tables,
    '---                        Update Job record,
    '---                      CommitTrans.         -----
    '= = = =  =
    '---  update to reflect contents of txtDiagnosis, txtWorkDetails, SessionTime (spent)--
    Private Function mbUpdateJob() As Boolean

        Dim sSql As String
        Dim s1, s2 As String
        Dim ix, lngaffected As Integer
        Dim intSequence As Short
        Dim idxTemp, idxService, idxGrid As Integer
        Dim lngTypeId, lngStock_id As Integer
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim sHours As String '-- actually decimal--
        Dim sErrors, sStaff As String
        Dim sSession, sAllSessions, sDescr As String
        Dim sSessionWork As String
        Dim sLongDescr As String
        Dim sStatus As String
        Dim sName, sComment, sDate As String
        Dim sValues As String
        Dim colFields As Collection
        Dim ColJobFields As Collection
        '-- part record flds..--
        Dim sStockId As String
        Dim sWarranty As String
        Dim sCost, sPartNo, sSell As String
        Dim sSerialNo As String
        Dim sCat2, sCat1, sCat3 As String
        Dim curSell As Decimal
        Dim sdSequences As New clsStrDictionary  '== Scripting.Dictionary
        Dim vasChecklist As Object

        mbUpdateJob = False
        '-- Build a batch of Deletes/Inserts..--
        sSql = ""
        '===If mbQuotation Then
        '--  delete all SERVICE checklists for this job..  we will add all.-
        sSql = sSql & " DELETE FROM [JobServiceCheckLists] WHERE " & "(JobCheckList_JobId=" & CStr(mlJobId) & "); " & vbCrLf
        '===Else
        If (mlTaskDeletions > 0) Then '--normal tasks..-
            For ix = 1 To mlTaskDeletions
                sSql = sSql & " DELETE FROM [jobTasks] WHERE " & _
                       "(TaskJob_Id=" & CStr(mlJobId) & ") AND (task_Id=" & CStr(malDeletedTasks(ix)) & "); " & vbCrLf
            Next ix
        End If '--task deletions.-
        '===End If  '--quote
        If (mlPartDeletions > 0) Then
            For ix = 1 To mlPartDeletions
                sSql = sSql & "DELETE FROM [jobParts] WHERE " & _
                        "(PartJob_Id=" & CStr(mlJobId) & ") AND (part_Id=" & CStr(malDeletedParts(ix)) & "); " & vbCrLf
            Next ix
        End If '--part deletions.-
        '--  INSERTs for NEW tasks and parts..-
        '=== If mbQuotation Then  '--  INSERT checklist for this job..-

        '--  RE-create all surviving JOB SERVICE checklists.. --
        intSequence = 1
        If (ListViewParts(k_LV_INDEX_SERVICE).Items.Count > 0) Then
            If (mlCurrentChecklistIndex >= 0) Then '--save current grid data..--
                '== Call mbSaveFlexgridIntoChecklist(fgCheckList, mlCurrentChecklistIndex)
                Call mbSaveFlexgridIntoChecklist(dgvChecklist, mlCurrentChecklistIndex)
            End If
            idxService = 0
            For Each item1 In ListViewParts(k_LV_INDEX_SERVICE).Items
                '==For idxService = 1 To (ListViewParts(k_LV_INDEX_SERVICE).Items.Count)
                idxService += 1
                '== item1 = ListViewParts(k_LV_INDEX_SERVICE).Items(idxService)
                '==s1 = Trim(item1.SubItems(3)) '--serial no.- Print only if NOT blank..
                '=3404.909- k_LV_PARTS_STOCK_ID_IDX
                '= lngStock_id = CInt(item1.SubItems(5).Text)
                lngStock_id = CInt(item1.SubItems(k_LV_PARTS_STOCK_ID_IDX).Text)
                sStockId = CStr(lngStock_id)
                '--  INCR. SEQUENCE WITHIN stock-id.. ===
                If sdSequences.Exists(sStockId) Then
                    intSequence = sdSequences(sStockId) + 1
                    sdSequences(sStockId) = intSequence '--update for next time.-
                Else '--none yet for this stock item..-
                    intSequence = 1
                    sdSequences.Add(sStockId, intSequence)
                End If
                idxTemp = IIf((item1.Tag = ""), -1, CInt(item1.Tag)) '--get index into temp parts..-
                If (idxTemp >= 0) Then '==3061.0==  (idxTemp > 0) Then '--valid.-
                    idxGrid = malActiveChecklists(idxTemp) '--get grid array index if any..-
                    If idxGrid >= 0 Then '--have a checklist..--
                        '--  get data from Checklist array..-
                        '==With fgCheckList(idxGrid)
                        vasChecklist = mavGridChecklists(idxGrid)
                        If IsArray(vasChecklist) Then
                            For ix = 0 To UBound(vasChecklist, 1) '== 1 To .Rows - 1
                                sDescr = Trim(vasChecklist(ix, k_GRIDCOL_ITEM)) '--task description..
                                sStatus = Trim(vasChecklist(ix, k_GRIDCOL_STATUS)) '-- get current status value..
                                sComment = Trim(vasChecklist(ix, k_GRIDCOL_COMMENTS)) '-- get current comments..
                                sName = Trim(vasChecklist(ix, k_GRIDCOL_NAME)) '-- get current name..
                                sDate = Trim(vasChecklist(ix, k_GRIDCOL_DATE)) '-- get current value..
                                s1 = Trim(vasChecklist(ix, k_GRIDCOL_ORIG_STATUS)) '-- get original status value..
                                s2 = Trim(vasChecklist(ix, k_GRIDCOL_ORIG_COMMENTS)) '-- get original Comment value..
                                sSql = sSql & "INSERT INTO [JobServiceCheckLists] " & _
                                                " (JobCheckList_JobId, JobCheckList_RMStockId, JobCheckListSequence, " & _
                                                 "JobCheckListTaskDescription, JobCheckListStatus, JobCheckListComments "
                                sValues = " VALUES ( " & CStr(mlJobId) & ", " & CStr(lngStock_id) & ", " & _
                                                       CStr(intSequence) & ", '" & msFixSqlStr(sDescr) & "', " & _
                                                       "'" & msFixSqlStr(sStatus) & "', '" & msFixSqlStr(sComment) & "' "
                                '-- insert staff id,name only if comments were changed..--
                                '---- THIS IS TO KEEP ORIGINAL dateUpdated..-----

                                '=====If (sComment <> masCheckListComments(ix)) Then    '--updated today..--
                                '==If (sComment <> s1) Then    '--updated today..--
                                If ((sStatus <> s1) Or (sComment <> s2)) Then '--updated today..--
                                    sSql = sSql & ", JobCheckList_StaffId, JobCheckListStaffName) "
                                    sValues = sValues & ", " & CStr(mlStaffId) & ", '" & msFixSqlStr(msStaffName) & "');"
                                ElseIf (sName <> "") Then  '--propagate previous update..-
                                    sSql = sSql & ", JobCheckList_StaffId, JobCheckListStaffName,JobCheckListDateUpdated) "
                                    sValues = sValues & ", " & CStr(mlStaffId) & ", '" & msFixSqlStr(sName) & "', '" & sDate & "'  );"
                                Else '--still hasn't changed..-
                                    sSql = sSql & ") " '--end of cols list..--
                                    sValues = sValues & "  );"
                                End If
                                sSql = sSql & sValues & vbCrLf
                            Next ix
                        End If '--array-
                        '==End With
                    End If '--idxGrid..-
                End If '--idxTemp.-
                '== Next idxService
            Next item1
        End If '--SERVICE--
        '=== Else '--normal job..-
        If (mlTempTasksCount > 0) Then '--we have a TASKs scratchpad..-
            For ix = 0 To (mlTempTasksCount - 1)
                If malTempTasksIndexes(ix) = 0 Then '--surviving new task..-
                    colFields = mavTempNewTasks(ix) '--get fld collection new task..--
                    If Not (colFields Is Nothing) Then
                        '== 07Dec2009==MUST KEEP  "taskType_Id" in INSERT list..-
                        '==   SINCE Precise DB was created without any Column DEFAULT !!  --
                        sSql = sSql & "INSERT INTO [jobTasks] " & _
                               " (taskJob_id, taskType_id, description, PerformedByRMStaff_id, PerformedByStaffName) "
                        sSql = sSql & "VALUES (" & _
                                    CStr(mlJobId) & ", " & CStr(colFields.Item("TASKTYPE_ID")) & "," & _
                                       " '" & msFixSqlStr(colFields.Item("DESCRIPTION")) & "', " & _
                                          CStr(mlStaffId) & ", '" & msFixSqlStr(msStaffName) & "');" & vbCrLf
                        '== 07Dec2009==MUST KEEP  "taskType_Id" in INSERT list..-
                        '==   SINCE Precise DB was created without any Column DEFAULT !!  --

                    End If '--nothing-
                End If '--new task..-
            Next ix
        End If '--tempTasks..-
        '=== End If  '--quote.-
        '-- INSERT all new Parts.- UPDATE any price changes..--
        If (mlTempPartsCount > 0) Then '--we have a PARTS scratchpad..-
            '== For ix = 1 To mlTempPartsCount
            For ix = 0 To (mlTempPartsCount - 1)
                If (malTempPartsIndexes(ix) > 0) Then '--surviving old Part..-
                    If (mabTempPartsCostUpdated(ix) = True) Then '--price was updated..--
                        '--add to jobParts table..--
                        sSell = FormatCurrency(maCurTempPartsCosts(ix), 2) '--get new cost.-
                        sSql = sSql & "UPDATE [jobParts]  SET "
                        sSql = sSql & "  RMSell= " & Replace(sSell, ",", "")
                        sSql = sSql & " WHERE ((PartJob_id= " & CStr(mlJobId) & ") "
                        sSql = sSql & "   AND (Part_id= " & CStr(malTempPartsIndexes(ix)) & ") ); "
                    End If '--updated..-
                ElseIf malTempPartsIndexes(ix) = 0 Then  '--surviving new Part..-
                    colFields = mavTempNewParts(ix) '--get fld collection new PART..--
                    If Not (colFields Is Nothing) Then
                        '-- add part to parts table for this job..--
                        '-- fld collection is RM=jet STOCK record..  PLUS some..--
                        sPartNo = Trim(colFields.Item("Barcode")("value"))
                        sSerialNo = msFixSqlStr(Trim(colFields.Item("PartSerialNumber")("value")))
                        sWarranty = Trim(colFields.Item("iswarranty")("value"))
                        '--  clean descr. of all "|" which we use as separator..--
                        lngStock_id = colFields.Item("stock_id")("value")
                        '=====    curSell = CCur(colFields("sell")("value"))
                        '-- add GST..--
                        '=====    curSell = curSell + ((curSell * mCurGST) / 100)
                        '=== V22--  GST alreday done..
                        curSell = CDec(colFields.Item("SellInclGST")("value"))
                        sSell = FormatCurrency(curSell, 2)
                        sCost = FormatCurrency(colFields.Item("cost")("value"), 2)
                        '--  clean stuff for sql..--
                        sDescr = msFixSqlStr(Replace(Trim(colFields.Item("Description")("value")), "|", "-"))
                        sLongDescr = msFixSqlStr(Replace(Trim(colFields.Item("longDesc")("value")), "|", "-"))
                        sCat1 = msFixSqlStr(Replace(Trim(colFields.Item("cat1")("value")), "|", "-"))
                        sCat2 = msFixSqlStr(Replace(Trim(colFields.Item("cat2")("value")), "|", "-"))
                        sCat3 = msFixSqlStr(Replace(Trim(colFields.Item("cat3")("value")), "|", "-"))

                        '--add to jobParts table..--
                        sSql = sSql & "INSERT INTO [jobParts] " & _
                                           " (PartJob_id, RMBarcode, PartSerialNumber, RMStock_Id, " & _
                                             " RMDescription,RMLongDescription, " & _
                                               " RMCat1, RMCat2, RMCat3, " & _
                                                    "  RMCost, RMSell, IsWarrantyPart, " & _
                                                       "ServicedByRMStaff_id, ServicedByStaffName) "
                        sSql = sSql & "VALUES (" & CStr(mlJobId) & ", " & " '" & msFixSqlStr(Trim(sPartNo)) & "', '" & _
                                                 msFixSqlStr(sSerialNo) & "', " & CStr(lngStock_id) & ", " & _
                                                 "'" & msFixSqlStr(sDescr) & "', '" & msFixSqlStr(sLongDescr) & "', " & _
                                                  " '" & msFixSqlStr(sCat1) & "', '" & msFixSqlStr(sCat2) & "', '" & _
                                                     msFixSqlStr(sCat3) & "', " & Replace(sCost, ",", "") & ", " & _
                                                      Replace(sSell, ",", "") & ", " & " '" & sWarranty & "', " & _
                                                        CStr(mlStaffId) & ", '" & msFixSqlStr(msStaffName) & "');" & vbCrLf
                    End If '--nothing-
                End If '--new Part..-
            Next ix
        End If '--tempTasks..-
        '--ok..  deletes/inserts are ready..-
        If gbDebug Then MessageBox.Show(Me, "deletes/inserts are:" & sSql, _
                     "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '=== mCnnJobs.BeginTrans
        '-- Check if DateUpdated changed..--IF so then abort/rollback..-
        '-re-read job, AND BeginTrans.--
        If Not mbGetJobRecord(mlJobId, True, ColJobFields) Then  '--NB. Starts Transaction !!--
            Exit Function
        End If
        If CDate(ColJobFields.Item("DateUpdated")("value")) <> CDate(mColJobFields.Item("DateUpdated")("value")) Then
            '== mCnnJobs.RollbackTrans()
            If Not (mTransactionJobUpdate Is Nothing) Then
                mTransactionJobUpdate.Rollback()
            End If
            MessageBox.Show(Me, _
                            "Job Record has been changed by another User during your edit.." & vbCrLf & _
                              "This session is aborted..  ", _
                                 "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Function
        End If
        '--  Apply INSERTS/DELETES..--
        If mbService And (sSql <> "") Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            If Not gbExecuteSql(mCnnJobs, sSql, True, mTransactionJobUpdate, lngaffected, sErrors) Then
                '== mCnnJobs.RollbackTrans()
                If Not (mTransactionJobUpdate Is Nothing) Then
                    mTransactionJobUpdate.Rollback()
                End If
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MessageBox.Show(Me, "Failed to DELETE/INSERT DB tasks/parts items.." & vbCrLf & _
                                   sErrors, "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Function
            Else '--ok--
                If gbDebug Then MessageBox.Show(Me, "TASKs/Parts record deletes/inserts ok.." & vbCrLf & _
                                            "And " & lngaffected & " Records were affected.", _
                                                    "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If '--exec.-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If '--service..-
        sSessionWork = ""
        '--- add ALERT if not the owner doing the update..--
        If (Trim(txtWorkDetails.Text) <> "") Or (Not mbIsOwner) Then
            '====If txtWorkHistory.Text <> "" Then sSessionWork = vbCrLf
            If (txtWorkHistory.Text <> "") And (VB.Right(txtWorkHistory.Text, 2) <> vbCrLf) Then sSessionWork = vbCrLf
            '==s1 = msStaffName & ": " & VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy ") & VB6.Format(TimeOfDay, "hh:mm:ss") '--staff/date prefix..-
            s1 = msStaffName & ": " & Format(Now, "dd-MMM-yyyy hh:mm tt ")  '--staff/date prefix..-
            If (Not mbIsOwner) And (VB.Left(msJobStatus, 2) < "50") Then '--NOT COMPLETING.. can add ALERT..--
                sSessionWork &= "** ALERT! UPDATED BY: " & s1 & vbCrLf & _
                                 IIf((Trim(txtWorkDetails.Text) <> ""), Trim(msFixSqlStr((txtWorkDetails.Text))) & vbCrLf, "")
            Else '-- no alert..
                sSessionWork &= vbCrLf & "Updated By: " & s1 & vbCrLf & Trim(msFixSqlStr((txtWorkDetails.Text))) & vbCrLf
            End If
        End If '--details..-
        '--3107.518= add warning if sessionTimes were reset..
        If mbSessionTimesReset Then
            s1 = msStaffName & ": " & VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy  ") & VB6.Format(TimeOfDay, "hh:mm") '--staff/date prefix..-
            sSessionWork &= "** NB: Job Hours were reset by " & s1 & "." & vbCrLf
        End If
        '--sHours = msHoursSpent()  '--"0.00"
        sHours = VB6.Format(CDec(msTimeSpent) + CDec(msSessionTime), "0.00")
        '==sStaff = Space(8)
        sStaff = LSet(msStaffName, 8)
        '--sSessions = txtStaffSessions.Text + Format(Date, "dd/mmm/yy") + ": " + _
        ''--                          sStaff + " +" + Format(CCur(msSessionTime), "0.00") + vbCrLf
        s1 = IIf((optChargeable(0).Checked = True), "", " -NC") '--chargeable or not..-
        sSession = VB6.Format(CDate(DateTime.Today), "dd-mmm-yy") & ": " & sStaff & " +" & _
                                                                VB6.Format(CDec(msSessionTime), "0.00") & s1 & vbCrLf
        If sHours = "" Then sHours = "0.00"
        If mbService Then
            '== -- 3431.0512- maint. Update Job.. 
            '==       On Saving updates, log all Item movements to Service Notes...
            If (msItemMovementLog <> "") Then
                sSessionWork &= "= Items Added/Removed by " & msStaffName & ": " & _
                        Format(Today, "ddd dd-MMM-yyyy") & ":" & vbCrLf & msItemMovementLog & "= = =" & vbCrLf
            End If
            '-- done movement log..

            sSql = "UPDATE [jobs] SET jobStatus='" & msJobStatus & "' "
            '--  set new owner if job was overtaken..--
            If (msNewOwner <> "") Then '--new owner..-
                sSql = sSql & ", NominatedTech='" & Trim(msFixSqlStr(msNewOwner)) & "'"
            End If '-new..-
            sSql = sSql & ", diagnosis='" & Trim(msFixSqlStr((txtDiagnosis.Text))) & "'"
            '=======sSql = sSql & IIf((sSessionWork <> ""), "serviceNotes=RIGHT(ServiceNotes+'" + sSessionWork + "',3249)", "")

            '== -- 3431.0512- SERVICE NOTES should now be varchar(max).. 
            If mbIsOwner Or (VB.Left(msJobStatus, 2) >= "50") Then
                '--COMPLETING also clears notes of any alerts during update..-
                '== sSql = sSql & ", ServiceNotes=RIGHT(REPLACE(ServiceNotes,'** ALERT!','--')+'" & sSessionWork & "',4000)"
                sSql = sSql & ", ServiceNotes=REPLACE(ServiceNotes,'** ALERT!','--')+'" & sSessionWork & "'"
            Else '--
                '== sSql = sSql & ", ServiceNotes=RIGHT(ServiceNotes+'" & sSessionWork & "',4000)"
                sSql = sSql & ", ServiceNotes=ServiceNotes+'" & sSessionWork & "'"
            End If '--owner..-
            sSql = sSql & ", TotalServiceTime=" & sHours

            '==3107.518= Don't add current session.  it's doubling the current hours.
            sAllSessions = msSessionTimesToDate
            If (CDec(msSessionTime) > 0) Then '-- add line only if time spent..-
                '== sSql = sSql & ", SessionTimes=RIGHT(SessionTimes+'" & sSession & "',1022)"
                sAllSessions &= sSession
            End If
            '=3102.117= SessionTimes MAY have been zeroized, so we must REPLACE column value..
            sSql = sSql & ", SessionTimes= '" & VB.Right(sAllSessions, 4000) & "'"
            sSql = sSql & ", TechRMStaff_id=" & CStr(mlStaffId) & ", TechStaffName='" & msStaffName & "'"
            If (msJobStatus = k_statusCompleted) Then sSql = sSql & ", DateCompleted= CURRENT_TIMESTAMP "
        Else '--delivery--
            sSql = "UPDATE [jobs] SET jobStatus='" & msJobStatus & "'"
            sSql = sSql & ", DeliveredRMStaff_Id=" & CStr(mlStaffId) & ", DeliveredStaffName='" & msStaffName & "'"
            sSql = sSql & ", dateDelivered= CURRENT_TIMESTAMP "
        End If
        sSql = sSql & ", dateUpdated= CURRENT_TIMESTAMP "
        sSql = sSql & " WHERE (job_id=" & CStr(mlJobId) & ") "

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim bUpdateOk As Boolean
        bUpdateOk = gbExecuteSql(mCnnJobs, sSql, True, mTransactionJobUpdate, lngaffected, sErrors)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        If Not bUpdateOk Then  '= gbExecuteSql(mCnnJobs, sSql, True, mTransactionJobUpdate, lngaffected, sErrors) Then
            '=3431.0513- -- Rollback was done.
            '=If Not (mTransactionJobUpdate Is Nothing) Then
            '=    mTransactionJobUpdate.Rollback()
            '=End If
            MessageBox.Show(Me, "Failed to update DB JOB work details.." & vbCrLf & _
                            sErrors & vbCrLf, "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else '--ok--
            If Not (mTransactionJobUpdate Is Nothing) Then
                Try
                    mTransactionJobUpdate.Commit()
                    mbUpdateJob = True
                    If gbDebug Then MessageBox.Show(Me, "Job details were updated OK.. " & vbCrLf & _
                                                   "SQL was:" & vbCrLf & sSql & vbCrLf, _
                                                     "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Catch ex As Exception
                    MessageBox.Show(Me, "Failed to COMMIT all DB JOB work details.." & vbCrLf & _
                                    ex.Message & vbCrLf, "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            End If
        End If
        '--08Dec2009  ==  show updated sessions/cost..-
        txtStaffSessions.Text = txtStaffSessions.Text & sSession
        LabTotalTime.Text = "Total Time: " & sHours & " Hrs."
        Call mbShowFullCost() '-- show full labour cost incl. current session.-- 07Dec2009==

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        sdSequences = Nothing
    End Function '-- updateJob--
    '= = = = = = = = = = = = = =
    '-===FF->

    '--- Accept Customer Notification Event.--
    '--- Show option frame..
    '----Wait for OK..
    '---  Update Job record to add Notification text..-

    Private Function mbAcceptNotification() As Boolean
        Dim ix, index, L1 As Integer
        Dim sSql As String
        Dim sItem As String
        Dim sErrorMsg As String
        Dim frmNotifyCust1 As frmNotifyCust

        mbAcceptNotification = False
        '=== 2485 ==  gbDebug = True
        sItem = ""
        frmNotifyCust1 = New frmNotifyCust
        'UPGRADE_ISSUE: Load statement is not supported. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"'
        '==Load(frmNotifyCust)

        frmNotifyCust1.connection = mCnnJobs

        frmNotifyCust1.Previous = txtNotification.Text '== "Previous lots of stuff.."
        '==SHRINK==  frmNotifyCust1.CustomerName = txtCustCompany.Text & vbCrLf & txtCustName.Text '=== "Customer Jim"
        frmNotifyCust1.CustomerName = msCustomerCompany & vbCrLf & _
                                        msCustomerName & "[" & msCustomerBarcode & "]"
        frmNotifyCust1.CustomerPhone = msCustomerPhone '== txtCustPhone.Text '=="02 1234 5678"
        frmNotifyCust1.CustomerMobile = msCustomerMobile '== txtCustMobile.Text '== "0424 101 360"
        '=== 2485 ===   frmNotifyCust.CustomerMobile = "0424 101 360"
        frmNotifyCust1.CustomerEmail = msCustomerEmail

        '==3107.1013 - Pass on the full RM cust stuff for given names etc..
        frmNotifyCust1.RMCustomerDetails = mColRMCustomerDetails

        frmNotifyCust1.Reason = "Job completed.."

        '===frmNotifyCust.SmsGatewayURL = "http://www.smsCentral.com.au/api/sms.asmx/SendSMS"

        frmNotifyCust1.JobId = mlJobId '--  789   '- 2777
        frmNotifyCust1.StaffName = msStaffName

        frmNotifyCust1.ShowDialog(Me)

        '--  get result text, if any..
        If Not frmNotifyCust1.cancelled Then
            sItem = frmNotifyCust1.FinalText
        End If
        frmNotifyCust1.Close()

        If (sItem <> "") Then '--show.-
            txtNotification.Text = txtNotification.Text & sItem

            '--  DB has been updated by "frmNotifyCust"..
            mbAcceptNotification = True
        End If

    End Function '--notification.--
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- ORIGINAL -  L o a d---
    '-- ORIGINAL -  L o a d---

    '-- 3059.1=Private Sub mbOriginal_frmJobMaint3_Load()

    Private Sub frmJobMaint3_Load(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim ix As Short
        Dim s1, s2, sName As String
        Dim lngError As Integer
        txtNomTech.Text = ""
        '== msPriority = ""
        '== cmdSuspend.Enabled = False
        '== cmdQA.Enabled = False

        msLocalSettingsPath = gsLocalJobsSettingsPath()
        '=3311.225=
        mLocalSettings1 = New clsLocalSettings(msLocalSettingsPath)

        txtGoods.Text = ""

        labPriority.Text = ""

        LabTicket.Text = ""
        LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0)
        labWarrantyJob.Visible = False  '=3203.120=

        ListTaskTypes.Visible = False
        ListTaskTypes.Enabled = False
        '---LabAddTask.Visible = False
        cmdAddTask.Visible = False
        CmdCreateTask.Visible = False
        cmdDeleteTask.Visible = False

        cmdAddPart(k_LV_INDEX_PARTS).Visible = False
        cmdAddPart(k_LV_INDEX_SERVICE).Visible = False
        cmdDeletePart(k_LV_INDEX_PARTS).Visible = False
        cmdDeletePart(k_LV_INDEX_SERVICE).Visible = False

        dgvChecklist.ReadOnly = True

        _txtStaffName_0.Text = "" '--serviced by--
        '=====txtStaffName(0).Enabled = False

        _txtStaffName_1.Text = "" '--DELIVERED by--
        '=====txtStaffName(1).Enabled = False
        '== msProblemShort = ""
        txtGoods.Text = ""
        txtSymptoms.Text = ""

        txtDiagnosis.Text = ""
        txtWorkDetails.Text = ""

        txtDiagnosis.ReadOnly = True
        txtWorkDetails.ReadOnly = True
        cmdAddTask.Visible = False

        mlProgress = -1 '--stopped..-
        txtStaffSessions.Text = ""
        labResultHours.Text = ""

        LabelTimeSelect.Text = "Time this Session" & vbCrLf & "  Hours/Tenths"

        '--load Session time combo..--
        cboTimeSelectHours.Items.Clear()
        cboTimeSelectHours.Items.Add("0")
        cboTimeSelectHours.Items.Add("1")
        cboTimeSelectHours.Items.Add("2")
        cboTimeSelectHours.Items.Add("3")
        cboTimeSelectHours.Items.Add("4")
        cboTimeSelectHours.Items.Add("5")
        cboTimeSelectHours.Items.Add("6")
        cboTimeSelectHours.Items.Add("7")
        cboTimeSelectHours.SelectedIndex = 0
        cboTimeSelectHours.Enabled = False  '= Visible = False

        cboTimeSelectTenths.Items.Clear()
        cboTimeSelectTenths.Items.Add(".00")
        cboTimeSelectTenths.Items.Add(".10")
        cboTimeSelectTenths.Items.Add(".20")
        cboTimeSelectTenths.Items.Add(".30")
        cboTimeSelectTenths.Items.Add(".40")
        cboTimeSelectTenths.Items.Add(".50")
        cboTimeSelectTenths.Items.Add(".60")
        cboTimeSelectTenths.Items.Add(".70")
        cboTimeSelectTenths.Items.Add(".80")
        cboTimeSelectTenths.Items.Add(".90")
        cboTimeSelectTenths.SelectedIndex = 0
        cboTimeSelectTenths.Enabled = False   '=Visible = False

        '== FrameSessionTime.Visible = False
        FrameSessionTime.Text = "-Session Time- "
        '== msSessionTime = "0"
        optChargeable(0).Enabled = False
        optChargeable(1).Enabled = False

        '== cmdClearSessionTimes.Enabled = False   '=3107.611=

        Erase malDeletedTasks
        Erase malDeletedParts
        mlTaskDeletions = 0
        mlPartDeletions = 0
        mlTempTasksCount = 0
        mlTempPartsCount = 0
        Erase maCurTempPartsCosts '--tracks costs of parts in listbox.-
        Erase mabTempPartsCostUpdated '--tracks if part cost needs to be updated with JobUpdate..-

        Erase malActiveChecklists
        Erase mavTempNewTasks
        Erase malTempTasksIndexes
        Erase malTempPartsIndexes

        mlNextChecklistIndex = 0 '-  first instance already loaded..-
        mlCurrentChecklistIndex = -1 '--none on dislpay..-
        '--  NOT NOW with normal array of arrays to hold backup grid data..--

        FrameQuotation.Visible = False
        '== mbQuotation = False
        '== mCurLabourHourlyRate = 0
        '--Pic2 is for printing..--
        Picture1.Visible = False
        Picture2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(SSTab1.Top) + 600) '--hide pic2--
        Picture2.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(SSTab1.Left) + 600)
        Picture2.Visible = False
        PictureExtraPrint.Visible = False

        '==SHRINK== chkPrtDocs(k_CheckPrtRecord).CheckState = System.Windows.Forms.CheckState.Unchecked
        '==SHRINK== chkPrtDocs(k_CheckPrtDocket).CheckState = System.Windows.Forms.CheckState.Unchecked
        _chkPrtDocs_0.CheckState = System.Windows.Forms.CheckState.Unchecked
        _chkPrtDocs_1.CheckState = System.Windows.Forms.CheckState.Unchecked

        labNCMsg.Text = "Note: ""NC"" means NO CHARGE for that session.."

        chkMyJob.Enabled = False
        chkMyJob.CheckState = System.Windows.Forms.CheckState.Unchecked '-unchecked..-

        msOriginalNominTech = ""
        chkReturned.Enabled = False
        chkReturned.CheckState = System.Windows.Forms.CheckState.Unchecked '--unchecked..-
        LabJobReturned.Enabled = False
        '= msCustomerPrint = ""

        FrameWorkRecord.Text = ""
        FrameServiceItems.Text = ""
        FrameStockItems.Text = ""
        FrameOtherTasks.Text = ""
        FrameNotified.Text = ""

        '==  3031.1 ==
        frameJobDates.Text = ""
        labInstructions.Text = ""
        frameProblem.Text = ""
        frameProblem.Top = FrameQuotation.Top
        frameProblem.Height = FrameQuotation.Height
        frameProblem.Width = FrameQuotation.Width

        '=3203.107= FramePrintOpts.Text = ""  '== "Exit Strategy.."

        'If (mIntFormTop >= 0) And (mIntFormLeft >= 0) Then
        '    Me.Top = mIntFormTop
        '    Me.Left = mIntFormLeft
        'Else
        '    Call CenterForm(Me)
        'End If

        '=3431.0513=
        '=Target-4262- Call CenterForm(Me)

        '--  Popup menu for Right click on parts..-
        mContextMenuPartInfo = New ContextMenu
        mnuCopyPartDescription.Name = "CopyPartDescription"
        mContextMenuPartInfo.MenuItems.Add(mnuCopyPartDescription)
        mnuCopyPartBarcode.Name = "CopyPartBarcode"
        mContextMenuPartInfo.MenuItems.Add(mnuCopyPartBarcode)
        mnuCopyPartSerialNo.Name = "CopyPartSerialNo"
        mContextMenuPartInfo.MenuItems.Add(mnuCopyPartSerialNo)
        mnuPartMenuSep1.Name = "PartMenuSep1"
        mContextMenuPartInfo.MenuItems.Add(mnuPartMenuSep1)
        mnuDeletePart.Name = "DeletePart"
        mContextMenuPartInfo.MenuItems.Add(mnuDeletePart)

        mnuDeletePart.Enabled = False

        '-- setup exit menu..-
        '--  MENU NOT USED --  Add MenuItems to menu..-.
        mContextMenuExit = New ContextMenu
        '--  MENU NOT USED --  Add MenuItems to menu..-.
        menuItemExitSave.Name = "KeepStatus"
        mContextMenuExit.MenuItems.Add(menuItemExitSave)
        menuItemExitSuspend.Name = "MarkSuspended"
        mContextMenuExit.MenuItems.Add(menuItemExitSuspend)
        menuItemExitQA.Name = "MarkQA"
        mContextMenuExit.MenuItems.Add(menuItemExitQA)
        menuItemExitCompleted.Name = "MarkCompleted"
        menuItemExitCompleted.Enabled = False '--  disable completion until checks carried out..
        mContextMenuExit.MenuItems.Add(menuItemExitCompleted)

        '== mIntExitMenuCompletedIndex = 3   '--remember completed item index..--

        '--  init exit options..--
        optCompleted.Checked = False
        optCompleted.Enabled = False
        optSuspend.Checked = False
        optSuspend.Enabled = False
        optQA.Checked = False
        optQA.Enabled = False
        optSaveExit.Checked = False
        optSaveExit.Enabled = False

        cmdFinish.Enabled = False

        '== ToolTip1.SetToolTip(labExitAction, "Options to change Job Status. ie:" & vbCrLf & vbCrLf & _
        '==                                    "To suspend Job, Mark Job for QA, " & vbCrLf & "  or Mark as Complete..")
        '== labExitAction.Enabled = False
        '== labExitAction.Visible = False

        '--  disable completion until checks carried aout..
        '== mContextMenuExit.MenuItems(mIntExitMenuCompletedIndex).Enabled = False

        ToolTip1.SetToolTip(labTaskNeeded, "Note: At least one Task or Service item " & vbCrLf & _
                                                                  "is needed before Job can be completed..")

        '== 3067.0 ==
        s1 = gsGetHelpFileName()
        If (s1 <> "") Then
            HelpProvider1.HelpNamespace = s1
            HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
            HelpProvider1.SetHelpKeyword(Me, "JT3-JobMaint.htm")
        End If

        '=Target-4262-  Me.KeyPreview = True  '-To catch Ctl-V (Pasting File)..

        '--=3.2.1229=  29Dec2015=

        '=  Attachments- Context menu for pasting file Name--
        '--  Popup menu for Right click on txt File name..-
        mContextMenuPasteFileName = New ContextMenu
        mnuPasteFileName.Name = "mnuPasteFileName"
        '= mContextMenuPasteFileName.MenuItems.Add(mnuPasteFileSep1)
        mContextMenuPasteFileName.MenuItems.Add(mnuPasteFileName)
        '= mContextMenuPasteFileName.MenuItems.Add(mnuPasteFileSep2)
        '--  done that menu.--
        '-- disable default menu.
        txtNewFileName.ContextMenu = mContextMenuDummy

        '= 3203.114-  fix Attachments listView=
        Me.lvwDocs.Columns(0).TextAlign = HorizontalAlignment.Right
        Me.lvwDocs.Columns(1).TextAlign = HorizontalAlignment.Right
        Me.lvwDocs.Columns(3).TextAlign = HorizontalAlignment.Right
        '== 3203.107==
        '--  Load printer Combos..
        '-- Combo for Colour printer..
        '=3203.107= get printers.
        Dim intDefaultPrinterIndex As Integer
        Dim colPrinters As Collection

        cboColourPrinters.Items.Clear()   '--Colour printer..
        cboReceiptPrinters.Items.Clear()

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MessageBox.Show(Me, "No printers available ! ", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            '--  load all combos with printers list.
            For Each sName In colPrinters
                cboColourPrinters.Items.Add(sName)
                cboReceiptPrinters.Items.Add(sName)
            Next sName
            '-- A. check local settings (prefs) for COLOUR printer..
            If mLocalSettings1.queryLocalSetting(gK_SETTING_PRTCOLOUR, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboColourPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboColourPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboColourPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            msColourPrinterName = cboColourPrinters.SelectedItem
            '-- B. check local settings (prefs) for RECEIPT printer..
            If mLocalSettings1.queryLocalSetting(gK_SETTING_PRTRECEIPT, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--pref. defined, so set it- 
                    cboReceiptPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem
        End If '-getAvail.-  

        '== 3203.116=
        '--  Load all Business Info here..

        '--  load system info..--
        mClsSystemInfo = New clsSystemInfo(mCnnJobs)

        '--=3072= 08Feb2013=  Get business info..--
        msABN = mClsSystemInfo.item("BusinessABN")
        msABN = Replace(msABN, " ", "") '--strip blanks..-
        '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
        msBusinessName = mClsSystemInfo.item("BUSINESSNAME")
        msBusinessAddress1 = mClsSystemInfo.item("BUSINESSADDRESS1")
        msBusinessAddress2 = mClsSystemInfo.item("BUSINESSADDRESS2")
        msBusinessShortName = mClsSystemInfo.item("BUSINESSSHORTNAME")
        msBusinessState = mClsSystemInfo.item("BUSINESSSTATE")
        msBusinessPostCode = mClsSystemInfo.item("BUSINESSPOSTCODE")
        msBusinessPhone = mClsSystemInfo.item("BUSINESSPHONE")
        msBusinessEmail = mClsSystemInfo.item("BusinessEmail")

        '==3311.331= If (mClsSystemInfo.item("LABOURHOURLYRATEPRIORITY1") <> "") Then
        '==3311.331= mCurLabourHourlyRateP1 = CDec(mClsSystemInfo.item("LABOURHOURLYRATEPRIORITY1"))
        '==3311.331= End If
        '==3311.331= If (mClsSystemInfo.item("LABOURHOURLYRATEPRIORITY2") <> "") Then
        '==3311.331= mCurLabourHourlyRateP2 = CDec(mClsSystemInfo.item("LABOURHOURLYRATEPRIORITY2"))
        '==3311.331= End If
        '==3311.331= If (mClsSystemInfo.item("LABOURHOURLYRATEPRIORITY3") <> "") Then
        '==3311.331= mCurLabourHourlyRateP3 = CDec(mClsSystemInfo.item("LABOURHOURLYRATEPRIORITY3"))
        '==3311.331= End If
        If (mClsSystemInfo.item("LABOURMINCHARGE") <> "") Then
            mCurLabourMinCharge = CDec(mClsSystemInfo.item("LABOURMINCHARGE"))
        End If

        '=3203.211=  -------
        mbEnforceMinCharge = True   '-default-
        If (mClsSystemInfo.item("ENFORCE_MINIMUM_CHARGE") <> "") Then
            s1 = UCase(VB.Left(mClsSystemInfo.item("ENFORCE_MINIMUM_CHARGE"), 1))
            mbEnforceMinCharge = IIf((s1 = "Y"), True, False)
            labMinCharge.Visible = mbEnforceMinCharge
        End If
        '-------------------
        '==3311.331= msDescriptionPriority1 = mClsSystemInfo.item("DESCRIPTIONPRIORITY1")
        '==3311.331= msDescriptionPriority2 = mClsSystemInfo.item("DESCRIPTIONPRIORITY2")
        '==3311.331= msDescriptionPriority3 = mClsSystemInfo.item("DESCRIPTIONPRIORITY3")

        '-6 more-
        msDeliveryDocketFootnote = mClsSystemInfo.item("DELIVERYDOCKETFOOTNOTE")
        msServiceChargeCat1 = mClsSystemInfo.item("STOCKSERVICECHARGECAT1")
        msServiceChargeCat2 = mClsSystemInfo.item("STOCKSERVICECHARGECAT2")

        msItemBarcodeFontName = mClsSystemInfo.item("ITEMBARCODEFONTNAME")
        s1 = mClsSystemInfo.item("ITEMBARCODEFONTSIZE")
        If IsNumeric(s1) Then
            Dim L1 As Integer = CInt(s1)
            If (L1 > 3) And (L1 < 36) Then
                mlItemBarcodeFontSize = L1
            End If
        End If
        msGSTPercentage = mClsSystemInfo.item("GSTPERCENTAGE")
        '- and-
        s2 = mClsSystemInfo.item("SERVICENOTIFICATIONCOSTLIMIT")
        If IsNumeric(s2) Then
            mCurNotificationCostLimit = CDec(s2)
        End If
        '== -- 3431.0512- maint. Update Job.. 
        '==             On Saving updates, log all Item movements to Service Notes...
        msItemMovementLog = ""


        '== Target-New-Build-6201 --  (16-July-2021) for Open Source..
        '=  Private msRetailHostname As String = ""
        If mClsSystemInfo.exists("RETAILHOSTNAME") Then
            msRetailHostname = mClsSystemInfo.item("RETAILHOSTNAME")
        End If
        '==END Target-New-Build-6201 --  (16-July-2021) for Open Source..


        '- done system info..

        '=Target-4262-
        '=Target-4262-  USERCONTROL-

        Call frmJobMaint3_Shown() '= Handles MyBase.Shown

    End Sub '-Original-load--
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- EX:  F o r m  A c t i v a t e --
    '-- EX:   F o r m  A c t i v a t e --

    'Private Sub frmJobmaint3_Activated(ByVal eventSender As System.Object, _
    '                                      ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
    '    If mbActive Then Exit Sub '--re-entered after every child form..--
    '    mbActive = True

    'End Sub  '-Activated-
    '= = = = = = = = = ==  == =

    '= 3411.0428=
    '-- S h o wn   may be more stable-

    Private Sub frmJobMaint3_Shown() '= Handles MyBase.Shown

        'Private Sub frmJobMaint3_Shown(ByVal eventSender As System.Object, _
        '                                      ByVal eventArgs As System.EventArgs) Handles MyBase.Shown

        Dim s1 As String
        Dim s2, s3, s4 As String
        Dim iPos, idxGrid As Integer
        Dim L1, lngError As Integer
        Dim k, i, j, ix As Integer
        Dim item1 As System.Windows.Forms.ListViewItem
        '===Dim staffIndex As Long
        Dim sDay As String
        Dim sSql As String
        Dim colFld As Collection
        '== Dim fld1 As ADODB.Field
        Dim sShortDate As String
        Dim sName As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim bNotCompleted, bAutoAssignJob As Boolean

        'If mbActive Then Exit Sub '--re-entered after every child form..--
        'mbActive = True
        '= Call mbOriginal_frmJobMaint3_Load()
        '==3311.423=
        bAutoAssignJob = False
        If mClsSystemInfo.exists("AutoAssignOrphanJobsOnUpdate") AndAlso _
              UCase((mClsSystemInfo.item("AutoAssignOrphanJobsOnUpdate")) = "Y") Then
            bAutoAssignJob = True
        End If
        '== 3031.1 ==  Call CenterForm(Me)
        '== TRIES to set time.-     sDay = VB.Left(msDayOfWeek(Today), 3)
        sDay = VB.Left(msDayOfWeek(CDate(DateTime.Today)), 3)

        s1 = sDay & ", " & VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy") & " - " & VB6.Format(TimeOfDay, "hh:mm")
        '==SHRINK== LabToday.Text = s1

        LabVersion.Text = "JobMatix- V:" & CStr(My.Application.Info.Version.Major) & "." & _
                           My.Application.Info.Version.Minor & " Build: " & _
                               My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision & "."
        If (mlStaffId < 0) And (msStaffBarcode = "") Then
            MessageBox.Show(Me, "No staff signon..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            mbCloseRequestedAtStartup = True '=Call close_me() '= Me.Close() '==Me.Hide
            Exit Sub
        End If
        If mlJobId = -1 Then
            MessageBox.Show(Me, "Job No. must be supplied..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            mbCloseRequestedAtStartup = True   '=Call close_me() '= Me.Close() '==Me.Hide
            Exit Sub
        End If
        '==SHRINK== LabHdr2.Caption = msBusinessName + vbCrLf + msBusinessAddress1 + vbCrLf + _
        '==SHRINK==                            msBusinessAddress2 + vbCrLf + msBusinessState + " " + msBusinessPostCode
        '==SHRINK== LabHdr2.Text = msBusinessName & " " & msBusinessState & " " & msBusinessPostCode
        Me.Text = "JobNo: " & VB6.Format(mlJobId, "##00") & "."

        sShortDate = VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy")
        '==SHRINK== LabToday.Text = sShortDate

        LabTicket.Text = vbCrLf & VB6.Format(mlJobId, " 000")
        System.Windows.Forms.Application.DoEvents()
        mCurGST = 0
        If IsNumeric(msGSTPercentage) Then mCurGST = CDec(msGSTPercentage)
        '-- L o a d  J o b  R e c o r d..-
        '-- With Transaction for Service request-
        If Not mbGetJobRecord(mlJobId, mbService, mColJobFields) Then
            mbCloseRequestedAtStartup = True  '=Call close_me() '= Me.Close() '==Me.Hide
            Exit Sub
        End If
        labDateUpdated.Text = ""

        '--  Show ORIGINAL dateUpdated before locking the record..--
        If Not IsDBNull(mColJobFields.Item("DateUpdated")("value")) Then '--show date..-
            labDateUpdated.Text = VB6.Format(CDate(mColJobFields.Item("DateUpdated")("value")), "dd-mmm-yyyy hh:mm")
        End If
        msDateUpdated = VB6.Format(CDate(mColJobFields.Item("DateUpdated")("value")), "dd-mmm-yyyy hh:mm")

        '--save original status..-
        msJobStatus = mColJobFields.Item("jobstatus")("value")
        msJobOriginalStatus = msJobStatus '--save for reelease after cancel..

        labJobStatus.Text = "Job Status:" & vbCrLf & msJobStatus

        msNominTech = Trim(mColJobFields.Item("NominatedTech")("value"))
        txtNomTech.Text = msNominTech
        mbSessionTimesReset = False   '==3107.518=
        '-- 13/1/2011--   WARN if msStaffName is not the owner..--
        If mbService Then
            If (msNominTech <> "") Then '--has owner..-
                If LCase(msNominTech) <> LCase(msStaffName) Then '--not the owner..-
                    If Not (MessageBox.Show(Me, "This Job is currently assigned to staff member:  " & msNominTech & vbCrLf & vbCrLf & _
                                  "Do you want to continue this service update?", _
                                 "JobMatix Update", _
                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes) Then
                        '--no..  so abort..--
                        Call mbRollbackTransaction()
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
                        mbCloseRequestedAtStartup = True  '=Call close_me() '= Me.Close() '==Me.Hide
                        Exit Sub
                    End If '--yes..-
                End If '-not owner.-
            Else  '-no owner-
                If bAutoAssignJob Then
                    msNominTech = msStaffName
                    txtNomTech.Text = msStaffName
                    msNewOwner = msStaffName
                    mbIsOwner = True
                    chkMyJob.Enabled = False
                    chkMyJob.Checked = True
                    '== Call mbSetDataModified()  '== mbDataChanged = True
                    MessageBox.Show(Me, "NB: This Job was previously an orphan.." & vbCrLf & vbCrLf & _
                            " If you (" & msStaffName & ") make changes now, it will become your Job.. ", _
                            "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If  '-auto-
            End If '--owner..-
        End If '--service..-
        If mbService Then
            If (msJobStatus = k_statusInProcess) Or (msJobStatus = k_statusInProcessQA) Or _
                                                            (msJobStatus = k_statusInProcessSusp) Then '--locked..--
                If MessageBox.Show(Me, "The Record for Job: " & mlJobId & " is marked as being in use.." & vbCrLf & _
                              " If you are sure that no one is using it, you can release it.." & vbCrLf & _
                              "  Do you want to want to unlock it ? ", "JobMatix Update", _
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                    '=3311.819= Call mbReleaseJobEx(msJobOriginalStatus)  '=3311.731= 'to Original status-
                    '=3311.819= Must set back to FREE status..
                    Dim sOriginalFreeStatus As String = k_statusStarted  '-in case was started-
                    If msJobStatus = k_statusInProcessQA Then
                        sOriginalFreeStatus = k_statusQA    '--free back to QA-
                    ElseIf msJobStatus = k_statusInProcessSusp Then
                        sOriginalFreeStatus = k_statusSuspended   '--Back to free susp.
                    End If
                    '=3311.819= and fix this- was not checking result-..
                    If Not mbReleaseJobEx(sOriginalFreeStatus) Then '=3311.819= 'to Original FREE status-
                        Call mbRollbackTransaction()
                        MessageBox.Show(Me, "Error- Job NOT released.. ", _
                                        "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        mbProgramClosing = True
                        mbCloseRequestedAtStartup = True  '=Call close_me() '= Me.Close()
                        Exit Sub
                    End If
                    '=3311.819= 'to Original FREE status-
                    Call mbCommitTransaction()  '=3311.731= -save-
                    '=3311.731= msJobOriginalStatus = k_statusStarted '--cancel goes back to "Started"--
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
                    '-- and continue with service update..-
                    '-- no- exit..  must re-enter.
                    MessageBox.Show(Me, "Job has been released.. " & vbCrLf & _
                                         "Please restart this update !", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    mbProgramClosing = True
                    mbCloseRequestedAtStartup = True  '=Call close_me() '= Me.Close() '==Me.Hide
                    Exit Sub
                Else '--leave it alone..-
                    Call mbRollbackTransaction()
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
                    mbProgramClosing = True
                    mbCloseRequestedAtStartup = True   '=Call close_me() '= Me.Close() '==Me.Hide
                    Exit Sub
                End If '--yes.--
            Else '-not locked.. we have to lock it..-
                '--MsgBox " about to lock job: " & mlJobId, vbInformation
                If Not mbLockJob(msJobOriginalStatus) Then
                    MessageBox.Show(Me, "Couldn't lock Job record status !", _
                                         "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Call mbRollbackTransaction()
                    mbProgramClosing = True
                    mbCloseRequestedAtStartup = True  '=Call close_me() '= Me.Close() '==Me.Hide
                    Exit Sub
                End If
                '-- commit lock and close transaction for normal service input..-
                Call mbCommitTransaction()
            End If
        End If '--service--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
        '- Now read Job record again with no sql lock..--
        If Not mbGetJobRecord(mlJobId, False, mColJobFields) Then
            mbProgramClosing = True
            mbCloseRequestedAtStartup = True  '=Call close_me() '= Me.Close() '==Me.Hide
            Exit Sub
        End If

        If mColJobFields.Count() <= 0 Then '--no data-
            MessageBox.Show(Me, "JOB recordset collection is empty..", _
                                  "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '--Call mbRollbackTransaction
            mbProgramClosing = True
            mbCloseRequestedAtStartup = True  '=Call close_me() '= Me.Close() '==Me.Hide
            Exit Sub
        End If '--count-

        '-- update status display..-
        labJobStatus.Text = "Orig Status:" & vbCrLf & msJobOriginalStatus
        ToolTip1.SetToolTip(labJobStatus, "(Temporarily: " & mColJobFields.Item("jobstatus")("value") & ")")

        '-- show job infos..--
        mbIsOnSiteJob = (UCase(mColJobFields.Item("GoodsInCare")("value")) = K_GOODS_ONSITEJOB)
        '==SHRINK== txtCustCompany.Text = mColJobFields.Item("CustomerCompany")("value")
        '==SHRINK== txtCustName.Text = mColJobFields.Item("CustomerName")("value")

        '--  get JOB Customer in case no Cust.record..-
        msCustomerCompany = Trim(mColJobFields.Item("CustomerCompany")("value"))
        msCustomerName = Trim(mColJobFields.Item("CustomerName")("value"))
        mlCustomerId = CInt(mColJobFields.Item("RMCustomer_Id")("value"))
        msCustomerBarcode = Trim(mColJobFields.Item("CustomerBarcode")("value"))
        '==SHRINK== FrameCust.Text = " Customer: " & msCustomerBarcode & " " '--CStr(mlCustomerId) + " "
        '==SHRINK== txtCustPhone.Text = mColJobFields.Item("CustomerPhone")("value")
        msCustomerPhone = mColJobFields.Item("CustomerPhone")("value")  '== txtCustPhone.Text
        '==SHRINK== txtCustMobile.Text = mColJobFields.Item("CustomerMobile")("value")
        msCustomerMobile = mColJobFields.Item("CustomerMobile")("value")  '== txtCustMobile.Text

        '=3043.0==  Refresh customer details from INPUT RM cust.record ..
        If (mColRMCustomerDetails Is Nothing) Then
            '== MsgBox("No input customer record for job update.." & vbCrLf & _
            '==         "Cust. detail from job record will be shown..", MsgBoxStyle.Exclamation)
            cmdCustHistory.Enabled = False
            ToolTip1.SetToolTip(txtCustomer, "No input customer record for job update.." & vbCrLf & _
                    "Cust. detail from job record will be shown..")
            s1 = msCustomerName
        Else   '--ok-  use latest RM details..-
            If (LCase(mColRMCustomerDetails.Item("barcode")("value")) <> LCase(msCustomerBarcode)) Then
                MessageBox.Show(Me, "Retail customer record for job update does not match Job Record.." & vbCrLf & _
                        "Retail-Cust. barcode: " & mColRMCustomerDetails.Item("barcode")("value") & vbCrLf & _
                 "Cust. Barcode/Name on Job record: " & msCustomerBarcode & " (" & msCustomerName & ")" & _
                    vbCrLf & vbCrLf & _
                    "Cust. details from retail DB record will be shown..", _
                      "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '==3059.1= If mbService Then Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
                '==3059.1= Me.Close()
                '==3059.1= Exit Sub
            End If
            s3 = mColRMCustomerDetails.Item("given_names")("value")
            s4 = mColRMCustomerDetails.Item("surname")("value")
            msCustomerName = s4 '--max 50--SURNAME, GivenNames --
            If (msCustomerName <> "") And (s3 <> "") Then msCustomerName = msCustomerName & ", "
            msCustomerName = VB.Left(UCase(msCustomerName) & s3, 50) '--max 50--SURNAME, GivenNames --
            s1 = msCustomerName
            msCustomerCompany = mColRMCustomerDetails.Item("company")("value")
            '=3059.1=  Use latest barcode also..-=
            msCustomerBarcode = mColRMCustomerDetails.Item("barcode")("value")  '== txtCustPhone.Text

            msCustomerPhone = mColRMCustomerDetails.Item("phone")("value")  '== txtCustPhone.Text
            '==SHRINK== txtCustMobile.Text = mColJobFields.Item("CustomerMobile")("value")
            msCustomerMobile = mColRMCustomerDetails.Item("mobile")("value")  '== txtCustMobile.Text
            msCustomerEmail = mColRMCustomerDetails.Item("email")("value")  '== txtCustMobile.Text
        End If  '--customer details..-
        '====  s1 = IIf((Trim(txtCustName.Text) <> ","), txtCustName.Text, "")
        s2 = IIf(((msCustomerCompany <> "--") And (LCase(msCustomerCompany) <> "n/a")), msCustomerCompany, "")
        If (s1 = "") Then '-- no name-
            msCustomerPrint = s2 '--company only..--
        ElseIf (s2 <> "") Then
            msCustomerPrint = s1 & " (" & s2 & ")" '--join on company..--
        Else
            msCustomerPrint = s1 '--name only..
        End If
        txtCustomer.Text = msCustomerPrint & " [" & msCustomerBarcode & "]"  '==3059.1=

        msPriority = UCase(mColJobFields.Item("Priority")("value"))
        '===LabPriority = IIf((s1 = "H"), "HOME", "BUSINESS")
        '=3203.120=
        mbSystemUnderWarranty = False
        If mColJobFields.Item("SystemUnderWarranty")("value") <> 0 Then
            mbSystemUnderWarranty = True
            labWarrantyJob.Visible = True
            _optChargeable_1.Checked = True   '= NO CHARGE=
        End If
        If UCase(mColJobFields.Item("JobReturned")("value")) = "Y" Then
            '==LabReturned.Visible = True
            chkReturned.CheckState = System.Windows.Forms.CheckState.Checked '--checked
            chkReturned.BackColor = System.Drawing.ColorTranslator.FromOle(&H4040FF) '--  vbRed
            LabJobReturned.Enabled = True
            LabJobReturned.Font = VB6.FontChangeBold(LabJobReturned.Font, True)
            LabJobReturned.BackColor = System.Drawing.ColorTranslator.FromOle(&H4040FF) '-- vbRed
        End If
        Select Case msPriority
            Case "1", "H"
                frameProblem.Visible = True
                '=3311.331= labPriority.Text = msDescriptionPriority1
                '=3311.331= mCurLabourHourlyRate = mCurLabourHourlyRateP1
                LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(gIntPriority1Colour) '--mauve..-
            Case "2", "B"
                frameProblem.Visible = True
                '=3311.331= labPriority.Text = msDescriptionPriority2
                '=3311.331= mCurLabourHourlyRate = mCurLabourHourlyRateP2
                LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(gIntPriority2Colour)  '--pink..-
            Case "3"
                frameProblem.Visible = True
                '=3311.331= labPriority.Text = msDescriptionPriority3
                '=3311.331= mCurLabourHourlyRate = mCurLabourHourlyRateP3
                LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(gIntPriority3Colour)
            Case "Q"
                '=3311.331= mCurLabourHourlyRate = mCurLabourHourlyRateP1
                frameProblem.Visible = False
                FrameQuotation.Visible = True
                labPriority.Text = "QUOTATION"
                LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(gIntPriorityQuoteColour)
                mbQuotation = True
        End Select
        '=3311.331= -- TEST new system..
        '== Dim s1 As String
        If gbGetPriorityInfo(mCnnJobs, msPriority, mbIsOnSiteJob, mRetailHost1, mCurLabourHourlyRate, s1) Then
            '= MsgBox("Labour Price is: " & _
            '=            FormatCurrency(mCurLabourHourlyRate, 2) & vbCrLf & s1, MsgBoxStyle.Information)
            If msPriority = "Q" Then
                labPriority.Text = "QUOTATION"
            Else  '-normal-
                labPriority.Text = s1
            End If
        End If  '-get-

        '= 3203.121=
        If mbSystemUnderWarranty Then
            LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(gIntUnderWarrantyColour)
            LabTicket.ForeColor = Color.White
        Else
            LabTicket.ForeColor = Color.Black   '--normal.-
        End If
        If labPriority.Text = "" Then
            labPriority.Text = msPriority
        End If

        txtNomTech.Text = mColJobFields.Item("NominatedTech")("value")
        msNominTech = Trim(txtNomTech.Text)
        msOriginalNominTech = msNominTech
        If mbService Then '--can claim the job.-
            If (msNominTech = "") Then '--no owner..  make avail. to current staff..--
                '====chkMyJob.Value = 1
                '====msNominTech = msStaffName
                '====txtNomTech.Text = msStaffName
                '====msNewOwner = msStaffName
                '====mbIsOwner = True
                chkMyJob.Enabled = True
            ElseIf LCase(Trim(msStaffName)) = LCase(msNominTech) Then  '--already owned by msStaffName..-
                chkMyJob.Enabled = False
                chkMyJob.Visible = False
                mbIsOwner = True
            Else '--not my job.--
                chkMyJob.Enabled = True '--can grab it if he wants..
            End If
        End If '--service..-

        '--  REV-2916== 04-Aug-2011==
        '----ProblemShort used to hold flags..--
        msProblemShort = mColJobFields.Item("ProblemShort")("value")
        '--   eg. *QUOTATION-REQUIRED*  or *PROCEED-WITH-SERVICE*---
        If (InStr(UCase(msProblemShort), "*QUOTATION-REQUIRED*") > 0) Then
            labInstructions.BackColor = System.Drawing.Color.Yellow
            '==LabCostLimit.Enabled = False
            labInstructions.Text = "QUOTATION-REQUIRED" & vbCrLf & "Minimum Charge of " & _
                                                            FormatCurrency(mCurLabourMinCharge, 2) & " applies.."
        ElseIf (InStr(UCase(msProblemShort), "*PROCEED-WITH-SERVICE*") > 0) Then
            labInstructions.BackColor = System.Drawing.Color.Lime
            labInstructions.Text = "Proceed ok with agreed Service.. "
            '==LabCostLimit.Enabled = True
        ElseIf (InStr(UCase(msProblemShort), "*PROCEED-TO-LIMIT*") > 0) Then
            labInstructions.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFC0) '-- light Green--
            labInstructions.Text = "PROCEED-TO-LIMIT:  " & _
                                  Replace(msBusinessShortName, "_", " ") & " to notify Customer if cost can exceed " & _
                                                                              FormatCurrency(mCurNotificationCostLimit, 2)
        End If

        txtGoods.Text = mColJobFields.Item("GoodsInCare")("value") & vbCrLf & mColJobFields.Item("GoodsOther")("value")
        '--  Labgoods.Caption = mColJobFields("GoodsInCare")("value")
        '--LabGoodsOther.Caption = mColJobFields("GoodsOther")("value")
        s1 = UCase(Trim(mColJobFields.Item("GoodsBrand")("value")))
        '--  in case of old-style newJob form..-
        If (s1 <> "") And (s1 <> "N/A") Then '--some old-style brand..-
            txtGoods.Text = txtGoods.Text & vbCrLf & _
                            mColJobFields.Item("GoodsBrand")("value") & " (" & mColJobFields.Item("GoodsModel")("value") & ")"
        End If
        '--LabBrand.Caption = mColJobFields("GoodsBrand")("value")
        '--LabModel.Caption = mColJobFields("GoodsModel")("value")
        '==== --LabExtras.Caption = mColJobFields("GoodsExtras")("value")
        s1 = mColJobFields.Item("GoodsExtras")("value")
        If (s1 <> "") Then txtGoods.Text = txtGoods.Text & vbCrLf & "EXTRAS: " & s1
        '===If UCase(mColJobFields("MultiAccounts")("value")) = "Y" Then
        '===  ChkUsers.Value = 1
        '===  ChkUsers.FontBold = True
        '===End If
        s1 = mColJobFields.Item("Username")("value")
        s2 = mColJobFields.Item("Userpassword")("value")
        '--Rev2916--  Move LOGON onfo into Goods info..
        txtGoods.Text = txtGoods.Text & vbCrLf & "USERNAMES: " & s1
        txtGoods.Text = txtGoods.Text & vbCrLf & "PWDS: " & s2

        '==txtUserName.Text = s1  '==2916--
        '==txtPassword.Text = s2  '--2916.-

        If UCase(mColJobFields.Item("DataBackupReqd")("value")) = "Y" Then
            ChkBackupReq.CheckState = System.Windows.Forms.CheckState.Checked
            ChkBackupReq.Font = VB6.FontChangeBold(ChkBackupReq.Font, True)
        End If
        If UCase(mColJobFields.Item("DataDiskReqd")("value")) = "Y" Then
            ChkRecovDisk.CheckState = System.Windows.Forms.CheckState.Checked
            ChkRecovDisk.Font = VB6.FontChangeBold(ChkRecovDisk.Font, True)
        End If
        '---LabProblem.Caption = "REPORTED: " + mColJobFields("ProblemShort")("value")
        '--txtProblemDetails.Text = mColJobFields("ProblemLong")("value")
        '==2916==  s1 = mColJobFields("ProblemShort")("value")
        '==2916==  If (s1 <> "N/A") And (s1 <> "") Then txtSymptoms.Text = s1 + vbCrLf
        txtSymptoms.Text = txtSymptoms.Text + mColJobFields.Item("ProblemSymptoms")("value")
        s1 = mColJobFields.Item("ProblemLong")("value")
        If s1 <> "" Then
            txtSymptoms.Text = txtSymptoms.Text & vbCrLf & "NOTES:" & vbCrLf & s1
        End If

        txtDiagnosis.Text = mColJobFields.Item("Diagnosis")("value")
        '===txtWorkDetails.Text = mColJobFields("ServiceNotes")("value")
        txtWorkHistory.Text = mColJobFields.Item("ServiceNotes")("value")
        txtWorkHistory.SelectionStart = Len(txtWorkHistory.Text)
        txtWorkHistory.SelectionLength = 0

        LabRcvdStaff.Text = "by: " & mColJobFields.Item("RcvdStaffName")("value")
        mdDateCreated = CDate(mColJobFields.Item("datecreated")("value"))
        msShortDateCreated = VB6.Format(mdDateCreated, "dd-mmm-yyyy")
        msJobStatus = mColJobFields.Item("jobstatus")("value")
        '===msJobOriginalStatus = msJobStatus  '--save for reelease aftre cancel..
        txtNotification.Text = mColJobFields.Item("notifications")("value")
        '==labJobStatus.Caption = msJobStatus

        labDateCreated.Text = msShortDateCreated
        '-- show ticket job no..--
        '=3203.120= REFORMAT= 
        '==    LabTicket.Text = Mid(msShortDateCreated, 4, 4) & VB.Right(msShortDateCreated, 2) & ": " & VB6.Format(mlJobId, "  000")
        LabTicket.Text = VB6.Format(mlJobId, "  000") & ": " & Mid(msShortDateCreated, 4, 8)  '== eg  "Sep-2016"--

        '-- get list of tasks completed so for this job..--
        '---  ie from table [jobTasks] --
        If Not mbShowJobTasks() Then
            '---Call mbRollbackTransaction
            mbCloseRequestedAtStartup = True '=Call close_me() '= Me.Close() '==Me.Hide
            Exit Sub
        End If
        txtStaffSessions.Text = mColJobFields.Item("sessiontimes")("value")
        msSessionTimesToDate = mColJobFields.Item("sessiontimes")("value") '-- to compute cost to date..-
        msTimeSpent = VB6.Format(mColJobFields.Item("totalservicetime")("value"), "0.00")
        LabTotalTime.Text = "Total Time: " & msTimeSpent & " Hrs."
        If mCurLabourHourlyRate = 0 Then mCurLabourHourlyRate = 100.0# '--default..-

        '--  build listBox of PARTS added to this job so far..--
        '--  build listBox of PARTS added to this job so far..--
        bNotCompleted = (VB.Left(msJobOriginalStatus, 2) < "50")
        If Not mbShowAllParts(bNotCompleted) Then
            '--Call mbRollbackTransaction
            mbCloseRequestedAtStartup = True '=Call close_me() '= Me.Close() '==Me.Hide
            Exit Sub
        End If '--show--
        _txtStaffName_0.Text = mColJobFields.Item("TechStaffName")("value") '--was last serviced by..-

        labDeliveredBy.Text = "Your Name:" '--current staff sign-on.-

        _txtStaffName_1.Text = msStaffName
        '-- Quote can be also for Viewing or Delivery etc..--
        If mbQuotation Then
            LabHdr1.Text = "Quotation Build Record"
            LabHdr1.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF)
            Call mbShowQuotedParts() '--load quote list box..-
            FrameQuotation.Text = "Quote Order No: " & mlQuoteSalesOrderId
            Call mbShowQuotedPartsStatus()
            '== CAN STAY.  2916.. ListViewTasks.Visible = False
            '== CAN STAY.  2916..cmdDeleteTask.Visible = False
            '===If Not mbLoadJobChecklist Then
            '===    Unload Me   '==Me.Hide
            '===    Exit Sub
            '===End If
            LabComments.Text = "Special Comments"
            '======LabTasksDone.Caption = "CheckList.."
            txtDiagnosis.ReadOnly = True
        Else '--not quote-
            PictureExtra.Visible = False
            LabExtra.Visible = False
        End If '--quote-

        '--  setting up..--
        If mbService Then '--job is being serviced..--
            '--  can exit anyway if nothing happens..--
            cmdCancel.Text = "Exit"
            ToolTip1.SetToolTip(cmdCancel, "Exit")

            '== NO ==
            '== NO ==  VB6.SetCancel(cmdCancel, True) '--  ESC can exit..--
            '== NO ==

            '== labExitAction.Visible = True
            '== labExitAction.Enabled = True
            LabHdr1.Text = "Update Job Service Record"

            '--  setup exit options..--
            '== optSaveExit.Checked = True
            optSaveExit.Enabled = True
            '== optCompleted.Checked = False

            optSuspend.Enabled = True
            optQA.Enabled = True
            optCompleted.Enabled = False
            Call mbCheckCompletion()  '--check if completion allowed..

            mbStatusWasSuspOrQA = (VB.Left(msJobOriginalStatus, 2) = "20") Or _
                                                         (VB.Left(msJobOriginalStatus, 2) = "40")
            '---  ==    Enable QA/Suspended regardless..
            If (VB.Left(msJobOriginalStatus, 2) = "20") Then  '--wass suspended..-
                optSuspend.Checked = True
            ElseIf (VB.Left(msJobOriginalStatus, 2) = "40") Then   '--was QA.-
                optQA.Checked = True
            Else  '--started..-
                optSaveExit.Checked = True
            End If
            '===========

            cmdAddPart(k_LV_INDEX_PARTS).Visible = True
            cmdAddPart(k_LV_INDEX_SERVICE).Visible = True

            cmdDeletePart(k_LV_INDEX_PARTS).Visible = True
            cmdDeletePart(k_LV_INDEX_PARTS).Enabled = False
            cmdDeletePart(k_LV_INDEX_SERVICE).Visible = True
            cmdDeletePart(k_LV_INDEX_SERVICE).Enabled = False
            mnuDeletePart.Enabled = True  '--3041.0 ==

            dgvChecklist.ReadOnly = False

            labJobMatix.Text = "JobMatix" & vbCrLf & "-Job Update-"

            If mbQuotation Then
                '===LabTicket.BackColor = vbCyan
                Me.Text = Me.Text & " - Build from Quote.."
                txtDiagnosis.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0C0C0)
                '== If (CDec(msTimeSpent) <> 0) Then '===== And mbChecklistStarted Then  '--can complete--
                '==   mContextMenuExit.MenuItems(mIntExitMenuCompletedIndex).Enabled = True  '== cmdFinish.Enabled = True
                '==   labTaskNeeded.Visible = False
                '== End If
            Else '--normal service job..-
                '--  build comboBox of available task types..--
                '---  ie from table [jobTasks] --
                '====If Not mbQuotation Then
                Me.Text = Me.Text & " - Service Update.."
                If Not mbBuildTaskTypes() Then
                    '--Call mbRollbackTransaction
                    mbCloseRequestedAtStartup = True '=Call close_me() '= Me.Close() '==Me.Hide
                    Exit Sub
                End If '--build task types-
                '-- Service can change data..
                txtDiagnosis.ReadOnly = False
                '===If (CCur(msTimeSpent) <> 0) And (ListViewTasks.ListItems.Count > 0) Then  '--can complete--
            End If '--quote.-
            cmdAddTask.Visible = True
            CmdCreateTask.Visible = True
            cmdDeleteTask.Visible = True
            cmdDeleteTask.Enabled = False
            '=====LabPartsHdr.Enabled = True
            '--SERVICE: can NOW enable all time buttons.--
            '--For ix = 0 To (k_maxOptTime - 1)
            '--   optTime(ix).Enabled = True
            '--Next ix
            txtWorkDetails.ReadOnly = False
            _txtStaffName_1.BackColor = System.Drawing.ColorTranslator.FromOle(&HEEEEEE) '--grey out staff delivered..-
            '--  ok.. start off with tech-name.--
            '======= staffIndex = 0 '--serviced by..-
            '==     cmdFinish.Text = "Mark as Completed"
            '== LabPrintDocs.Text = "On exiting-    Print these:"

            '== chkPrtDocs(k_CheckPrtDocket).CheckState = System.Windows.Forms.CheckState.Unchecked
            '== _chkPrtDocs_0.CheckState = System.Windows.Forms.CheckState.Checked '--default.-
            _chkPrtDocs_0.CheckState = System.Windows.Forms.CheckState.Unchecked   '-- default only for completion.
            _chkPrtDocs_1.CheckState = System.Windows.Forms.CheckState.Unchecked
            '==OLD== FrameNotification.BackColor = &HE0E0E0
            optChargeable(0).Checked = False
            optChargeable(1).Checked = False
            cboTimeSelectHours.Enabled = True   '=Visible = True
            cboTimeSelectTenths.Enabled = True    '=Visible = True

            optChargeable(0).Enabled = True
            optChargeable(1).Enabled = True
            FrameSessionTime.Visible = True
            '-- Q.A. --
            '== cmdQA.Enabled = True
            If (VB.Left(msJobOriginalStatus, 2) >= "40") Then '--currently in QA..-
                '== cmdQA.Text = "Back to Bench"
                '== ToolTip1.SetToolTip(cmdQA, "Mark as Started" & vbCrLf & "(Return Job to Bench..)")
                '-- else..  leave as "Send to QA"..--
            End If
        ElseIf mbDelivery Then  '--must be delivery function.-
            '--  can exit anyway if nothing happens..--
            cmdCancel.Text = "Cancel"
            ToolTip1.SetToolTip(cmdCancel, "Abandon Delivery and Exit")

            '== NO ==      Can cancel the FORM..!  --
            '== NO ==  VB6.SetCancel(cmdCancel, True) '--  ESC can invoke Cancel cmd...--
            '== NO ==

            '=== staffIndex = 1 '--delivered by..--
            labTaskNeeded.Visible = False
            Me.Text = Me.Text & " - Delivering.."
            LabHdr1.Text = "Job Service Record"
            labJobMatix.Text = "JobMatix" & vbCrLf & "-Job Delivery-"
            '== txtExitText.Text = "-Job Delivery-"

            labDeliveredBy.Text = "Delivered By:" '--current staff sign-on.-

            '== LabPrintDocs.Text = "On Delivery-   Print these:"

            LabTicket.BackColor = System.Drawing.Color.Lime
            '== DELIVERY can always finish without data-changed....-
            cmdFinish.Enabled = True
            '= cmdFinish.Text = "Mark Job as Delivered.."
            ToolTip1.SetToolTip(cmdFinish, "Mark Job as Delivered..")
            '== cmdExit.Visible = False
            '== chkPrtDocs(k_CheckPrtRecord).CheckState = System.Windows.Forms.CheckState.Unchecked
            '== chkPrtDocs(k_CheckPrtDocket).CheckState = System.Windows.Forms.CheckState.Checked
            _chkPrtDocs_0.CheckState = System.Windows.Forms.CheckState.Unchecked
            _chkPrtDocs_1.CheckState = System.Windows.Forms.CheckState.Checked
        Else '--notify or view.-
            '-- V I E W --/print only..--
            '-- V I E W --/print only..--
            cboTimeSelectHours.Enabled = False
            cboTimeSelectTenths.Enabled = False

            LabelTimeSelect.Enabled = False
            optChargeable(0).Enabled = False
            optChargeable(1).Enabled = False
            cmdClearSessionTimes.Enabled = False   '=3107.611=

            Call mbCheckCompletion(True)  '--check if completion allowed..(VIEW ONLY)..

            '==ALL were DISABLED on LOAD..==  Call mbDisableServiceControls()
            '== cmdExit.Text = "Exit"
            If VB.Left(msJobStatus, 2) >= "70" Then '--finished and gone..-
                LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(k_colourDelivered) '--light grey..-
            ElseIf VB.Left(msJobStatus, 2) >= "50" Then  '--Completed- not delivered....-
                LabTicket.BackColor = System.Drawing.Color.Lime
            End If
            '== If mbNotify Then
            '==   Me.Text = Me.Text & " - Notify.."
            '==   LabJobMatix.Text = "JobMatix" & vbCrLf & "-Notify Cust.-"
            '==   LabTicket.BackColor = System.Drawing.Color.Lime
            '==   FramePrintOpts.Enabled = False
            '==OLD== FrameNotification.Visible = True
            '== Else '-- V I E W --/print only..--
            '--txtStaffName(0).Text = mColJobFields("TechStaffName")("value") '--was serviced by..-
            Me.Text = Me.Text & " - Viewing.."
            labJobMatix.Text = "JobMatix" & vbCrLf & "-View Job-"

            '== OK for VIEWING..  Form can be ditched..-
            '--- target-Build-4262- ONLY if JobTracking..
            If mbHostIsJobTracking Then
                VB6.SetCancel(cmdCancel, True) '--  ESC can exit..--
            End If
            '= VB6.SetCancel(cmdCancel, True) '--  ESC can exit..--
            '--- END- target-Build-4262- ONLY if JobTracking..

            LabHdr1.Text = "View Job Record"

            If VB.Left(msJobStatus, 2) >= "70" Then '--finished and gone..-
                labDeliveredBy.Text = "Delivered By:" '--current staff sign-on.-
                _txtStaffName_1.Text = mColJobFields.Item("DeliveredStaffName")("value") '--was serviced by..-
            End If

            cmdFinish.Text = "Print && Exit"
            ToolTip1.SetToolTip(cmdFinish, "")
            ToolTip1.SetToolTip(cmdCancel, "Exit")
            '== ToolTip1.SetToolTip(cmdExit, "")
            cmdFinish.Enabled = True
            '== End If '--view-
        End If '--service/deliver/view..--

        If mbDelivery Or mbService Then '-- stop press...--
            '======If (InStr(1, UCase(txtWorkHistory.Text), "STOP PRESS:") > 0) Then '-alert..-
            If (InStr(1, txtWorkHistory.Text, "** ALERT!") > 0) Then '-alert..-
                '====SSTab1.Tab = 3 '--Show 4th tab..-
                txtWorkHistory.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0C0FF) '--pink--
                txtWorkHistory.SelectionStart = Len(txtWorkHistory.Text)
                txtWorkHistory.SelectionLength = 0
            ElseIf mbService Then
                '--  show first checklist, if any..
                txtDiagnosis.Focus() '--move on..--
            End If
            '== ElseIf mbNotify Then
            '==    cmdFinish.Enabled = False
            '==    cmdExit.Enabled = False
            '==   cmdCancel.Enabled = False
            '==   Call mbAcceptNotification()
            '==   '--mRsJob.Close
            '==   Me.Close() '==Me.Hide
            '==   Exit Sub
        Else '--view only--
            '-- cmdFinish.Enabled = False
        End If '--view-
        If (VB.Left(msJobOriginalStatus, 2) <= "10") Then
            SSTab1.SelectedIndex = 0 '--Show first tab (Job Requirements.)..-
            If mbQuotation Then
                ListViewQuote.Select()
            Else
                frameProblem.Select() '== txtSymptoms.Select()
            End If
        Else
            SSTab1.SelectedIndex = 1 '--Show 2nd tab (work history..)..
            '== txtWorkDetails.Select()
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '--=3.2.1229=  29Dec2015=
        '= Attachments--

        '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
        '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
        '= Dim frmTemp1 As New Form

        Try
            mClsAttachments1 = New clsAttachments(mFrmMyParent, mCnnJobs, mColSqlDBInfo, "JOB", _
                                         mlJobId, txtCustomer.Text, mlStaffId, msStaffName, openDlg1)
        Catch ex As Exception
            MessageBox.Show(Me, "ERROR creating new (JobMaint) clsAttachments." & vbCrLf & _
                                  ex.Message, "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

        '-- 3203.120= Find any prev pic and load it..
        If (mlJobId > 0) Then  '--job exists..-
            Dim intDoc_id As Integer
            Dim sFileTitle As String
            Dim byteImageBytes() As Byte
            Dim ms As System.IO.MemoryStream
            '-- Have a JOB.. show image if any. on 1st Panel.
            If Not mClsAttachments1.GetFirstImage(mlJobId, intDoc_id, sFileTitle, byteImageBytes) Then
                '= NONE=  MsgBox("Failed to retieve image..", MsgBoxStyle.Information)
            Else '-ok-
                ms = New System.IO.MemoryStream(byteImageBytes)
                Dim image1 As Image = System.Drawing.Image.FromStream(ms)
                picSubjectMain.Image = image1  '=3203.101=
                ms.Close()
                '= mIntDoc_id = intDoc_id     '--save for updating if needed.
            End If  '-get-
        End If  '- job exists.-

        mbStartupCompleted = True
        mbDataChanged = False '--was set on by our loading of data..

        colFld = Nothing
        mbIsInitialising = False
        Exit Sub

activate_error:
        L1 = Err().Number
        MessageBox.Show(Me, "Runtime Error in JobMaint Shown sub.." & vbCrLf & _
                              "Error is " & L1 & " = " & ErrorToString(L1), _
                              "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        mbCloseRequestedAtStartup = True '=Call close_me() '= Me.Close()
        '== End

    End Sub '--Shown--
    '= = = = = = =  = = =
    '-===FF->

    '-- make this My Job..--

    'UPGRADE_WARNING: Event chkMyJob.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub chkMyJob_CheckStateChanged(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As System.EventArgs) Handles chkMyJob.CheckStateChanged

        If chkMyJob.Enabled Then '--ok.. active.-
            '=====MsgBox "chkMyJob clicked..   value is : " & chkMyJob.Value, vbInformation  '--TESTING..--
            If (chkMyJob.CheckState = 1) Then '--take it..-
                msNominTech = msStaffName
                txtNomTech.Text = msStaffName
                msNewOwner = msStaffName
                mbIsOwner = True
                Call mbSetDataModified()  '== mbDataChanged = True
            Else '-- give it back..--
                mbIsOwner = False
                msNewOwner = "" '--not changed.-
                msNominTech = msOriginalNominTech
                txtNomTech.Text = msNominTech
            End If
        End If '--enabled
    End Sub '--my job..-
    '= = = = = = =  = = =

    '--Show Customer History..-
    '--Show Customer History..-

    Private Sub cmdCustHistory_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles cmdCustHistory.Click
        Dim frmCustHistory1 As frmCustHistory

        If (mlJobId > 0) Then '--active.-
            frmCustHistory1 = New frmCustHistory
            '== Load(frmCustHistory)

            '== frmCustHistory.connectionJet = mCnnJet
            frmCustHistory1.connectionSql = mCnnJobs
            '== frmCustHistory.dbInfoJet = mColJetDBInfo
            frmCustHistory1.dbInfoSql = mColSqlDBInfo
            frmCustHistory1.retailHost = mRetailHost1
            frmCustHistory1.BusinessName = msBusinessName

            '== 3059.1 =  Restore passing ID to history form..
            frmCustHistory1.CustomerId = mlCustomerId
            frmCustHistory1.CustomerBarcode = msCustomerBarcode
            frmCustHistory1.ShowDialog(Me)

            frmCustHistory1.Close()
        End If
    End Sub '--History..-
    '= = = = = = = = = =
    '-===FF->

    '--  Tab Click..  set focus....-

    Private Sub SSTab1_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As System.EventArgs) Handles SSTab1.SelectedIndexChanged
        Static PreviousTab As Short = SSTab1.SelectedIndex()

        '--  set focus on listview..
        Select Case SSTab1.SelectedIndex
            Case 0 : If (txtDiagnosis.Text = "") Then txtDiagnosis.Focus() Else txtWorkHistory.Focus()

            Case 1 : ListViewParts(1).Focus() '-- Service items..-

            Case 2 : ListViewParts(0).Focus() '-- Parts items..-
            Case 3 : ListViewTasks.Focus() '-- Service items..-

        End Select

        PreviousTab = SSTab1.SelectedIndex()
    End Sub '-- tab click..-
    '= = = = = = = = = = =

    '-- 1-second Timer for progress bar..--

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles Timer1.Tick

        If mlProgress >= 0 Then '--active--
            mlProgress = mlProgress + 1
            '=== ProgressBar1.Value = mlProgress
        End If

        If mbCloseRequestedAtStartup Then
            Call close_me()
            mbCloseRequestedAtStartup = False   '-don't keep doing it..
            Timer1.Stop()
        End If

    End Sub '-- timer..--
    '= = = = = =  = =  = =

    '-- Notify CMD buttons..--
    '-- Notify CMD buttons..--

    '==OLD===Private Sub cmdNotifiedOK_Click()
    '==OLD===  mbNotifiedOK = True
    '==OLD===End Sub  '--notify--
    '--------

    '==OLD===Private Sub cmdNotifyCancel_Click()
    '==OLD===   mbNotifyCancelled = True
    '==OLD===End Sub  '--notify cancel.-
    '= = = = = = = = = = =  =

    '-- catch txt changes..--
    'UPGRADE_WARNING: Event txtDiagnosis.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtDiagnosis_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs)

        Call mbSetDataModified()  '== mbDataChanged = True

    End Sub '--changed--
    '= = = = =  = =  = = = =

    '-- catch txt changes..--
    'UPGRADE_WARNING: Event txtWorkDetails.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtWorkDetails_TextChanged(ByVal eventSender As System.Object, _
                                                 ByVal eventArgs As System.EventArgs) Handles txtWorkDetails.TextChanged
        If Not mbStartupCompleted Then Exit Sub
        Call mbSetDataModified()  '== mbDataChanged = True
        Call mbCheckCompletion()  '--check if completion allowed..

    End Sub '--changed--
    '= = = =  = = =  = = =
    '= = = = = = = = = = =
    '-===FF->

    '-- SERVICE Items --- Editing Checklist FlexGrid--
    '-- SERVICE Items --- Editing Checklist FlexGrid--

    '-- Rotate status..--
    '-- Rotate status..--

    Private Function mbRotateTaskStatus(ByVal sOldStatus As String) As String
        Dim sText As String
        Dim sNewStatus As String = ""

        sText = Trim(LCase(sOldStatus))
        If (sText = "") Then  '--was blank-
            sNewStatus = "Started."
        ElseIf (InStr(sText, "started") > 0) Then
            sNewStatus = "Completed."
        ElseIf (InStr(sText, "completed") > 0) Then
            sNewStatus = "Skipped."
        ElseIf (InStr(sText, "skipped") > 0) Then
            sNewStatus = "Queried."
        ElseIf (InStr(sText, "queried") > 0) Then
            sNewStatus = ""
        Else
            sNewStatus = ""
        End If  '--text-

        mbRotateTaskStatus = sNewStatus
    End Function '--rotate.-
    '= = = = = = = = = = = = 
    '-===FF->

    '-  catch Status change event..--
    '-- cell change.--

    Private Sub dgvChecklist_CellValueChangedEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellEventArgs) _
                                                            Handles dgvChecklist.CellValueChanged
        Dim lRow, lCol As Integer
        Dim sStatus, sText As String
        '== Dim streamIcon As New StreamReader

        lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex
        If (lRow >= 0) And (dgvChecklist.Rows.Count > 0) Then  '--selected a row.--
            If (lCol = k_GRIDCOL_STATUS) Then  '--status-
                sStatus = dgvChecklist.Rows(lRow).Cells(lCol).Value

                '== ?????? ==    Call mbSetDataGridIcon(lRow, sStatus, dgvChecklist)

            End If  '--status-
            Call mbSetDataModified()  '== mbDataChanged = True
        End If  '--row-
    End Sub '= CellValueChangedEvent--
    '= = = = = = = = = = = = = = = = = 

    '-- to catch keypress...
    Private Sub dgvChecklist_EditingControlShowing(ByVal sender As Object, _
                                                     ByVal e As DataGridViewEditingControlShowingEventArgs) _
                                                Handles dgvChecklist.EditingControlShowing

        Dim text1 As TextBox = CType(e.Control, TextBox)
        If (text1 IsNot Nothing) Then

            '-- Remove an existing event-handler, if present, to avoid 
            '-- adding multiple handlers when the editing control is reused.
            RemoveHandler text1.KeyPress, _
                New KeyPressEventHandler(AddressOf dgvChecklistStatus_KeyPress)
            RemoveHandler text1.DoubleClick, _
                New EventHandler(AddressOf dgvChecklistStatus_DoubleClickEvent)
        End If
        '-- Add the event handler. 
        AddHandler text1.KeyPress, _
            New KeyPressEventHandler(AddressOf dgvChecklistStatus_KeyPress)

        AddHandler text1.DoubleClick, _
            New EventHandler(AddressOf dgvChecklistStatus_DoubleClickEvent)

    End Sub  '--EditingControlShowing-
    '= = = = = = =  = = = == = = = 

    '-- TEXTBOX KeyPress --

    '-- Check for the SPACE char...
    '-- Check for the SPACE char...

    Private Sub dgvChecklistStatus_KeyPress(ByVal sender As Object, _
                                           ByVal eventArgs As KeyPressEventArgs)

        Dim lRow, lCol As Integer
        Dim sOldStatus, sNewStatus As String
        Dim txt1 As TextBox = CType(sender, TextBox)

        '- Check for the SPACE char...
        '==If (e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return)) 
        '--  needs  current row/cell..--
        lCol = Me.dgvChecklist.CurrentCellAddress.X  '== eventArgs.ColumnIndex
        lRow = Me.dgvChecklist.CurrentCellAddress.Y  '= eventArgs.RowIndex

        If (lRow >= 0) And (dgvChecklist.Rows.Count > 0) Then  '--selected a row.--
            If (lCol = k_GRIDCOL_STATUS) Then  '--status-
                If (eventArgs.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Space)) Then
                    '== sOldStatus = dgvChecklist.Rows(lRow).Cells(lCol).Value
                    sOldStatus = txt1.Text
                    '--rotate..-
                    sNewStatus = mbRotateTaskStatus(sOldStatus)
                    '== dgvChecklist.Rows(lRow).Cells(lCol).Value = sNewStatus    '--show new status.--
                    txt1.Text = sNewStatus    '--show new status.--
                    Call mbSetDataGridIcon(lRow, sNewStatus, dgvChecklist)
                    '--== 3063.0 ==   SAVE current flexgrid data into it's correct checklist..--
                    Call mbSaveFlexgridIntoChecklist(dgvChecklist, mlCurrentChecklistIndex)
                    Call mbSetDataModified()  '== mbDataChanged = True
                End If
                '-- Stop the character from being entered into the control-
                '--  since we provide rotating ststua..
                eventArgs.Handled = True
            End If  '--col-
        End If  '--row--

    End Sub 'textBox1_KeyPress
    '= = = = = = = = = = = = = = 

    '--  Double-click.--

    Private Sub dgvChecklistStatus_DoubleClickEvent(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As EventArgs)
        Dim lRow, lCol As Integer
        Dim sOldStatus, sNewStatus As String
        Dim txt1 As TextBox = CType(eventSender, TextBox)
        '== lRow = eventArgs.RowIndex
        '== lCol = eventArgs.ColumnIndex
        lCol = Me.dgvChecklist.CurrentCellAddress.X  '== eventArgs.ColumnIndex
        lRow = Me.dgvChecklist.CurrentCellAddress.Y  '= eventArgs.RowIndex

        If (lRow >= 0) And (dgvChecklist.Rows.Count > 0) Then  '--selected a row.--
            If (lCol = k_GRIDCOL_STATUS) Then  '--status-
                '== sOldStatus = dgvChecklist.Rows(lRow).Cells(lCol).Value
                sOldStatus = txt1.Text
                sNewStatus = mbRotateTaskStatus(sOldStatus)
                '== dgvChecklist.Rows(lRow).Cells(lCol).Value = sNewStatus    '--show new status.--
                txt1.Text = sNewStatus    '--show new status.--
                dgvChecklist.EndEdit()    '--== 3063.0 ==  to force text into grid..
                System.Windows.Forms.Application.DoEvents()
                Call mbSetDataGridIcon(lRow, sNewStatus, dgvChecklist)
                '==test-
                '==   MsgBox("Cell value is: " & dgvChecklist.Rows(lRow).Cells(lCol).Value)

                '--== 3063.0 ==   SAVE current flexgrid data into it's correct checklist..--
                Call mbSaveFlexgridIntoChecklist(dgvChecklist, mlCurrentChecklistIndex)
                Call mbSetDataModified()  '== mbDataChanged = True
            End If  '--status-
            '== eventArgs.Handled = True
        End If  '--row-
        '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
    End Sub '--dbl-click--
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Normal Service Jobs..--
    '-- SELECT- the task to add this job..--
    '-- SELECT- the task to add this job..--

    '-- save selected items..-

    Private Sub listTaskTypes_saveItems()
        Dim sTask, sId As String
        Dim id, dx, k, i, j, cx, ix, scrx As Integer
        Dim lngTypeId, lngCount As Integer
        Dim sSql, sDescr As String
        Dim sErrorMsg As String '--, sStaff As String
        Dim col1 As Collection
        Dim item1 As System.Windows.Forms.ListViewItem

        sTask = ""
        lngTypeId = 0
        lngCount = 0
        '-- 27Jan2010- Check if any checked..-
        For ix = 0 To UBound(maiTaskTypes)
            If maiTaskTypes(ix) <> 0 Then '--checked..--
                lngCount = lngCount + 1
            End If
        Next ix
        If lngCount > 0 Then '--have some.-
            If MessageBox.Show(Me, "Add all (" & lngCount & ") Checked tasks to this Job List ?" & vbCrLf, _
                                 "JobMatix Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                                     MessageBoxDefaultButton.Button1) = MsgBoxResult.Yes Then
                For ix = 0 To UBound(maiTaskTypes)
                    If maiTaskTypes(ix) <> 0 Then '--checked..--
                        col1 = New Collection '--to save item data for later INSERT..-
                        sTask = VB6.GetItemString(ListTaskTypes, ix) '-- selected COMBO item --
                        lngTypeId = VB6.GetItemData(ListTaskTypes, ix) '--get id from combo item..---
                        '--sDescr = Space(24)
                        '--LSet sDescr = Trim(sTask)
                        sDescr = Trim(sTask)
                        '--sStaff = Space(8)
                        '--LSet sStaff = msStaffName
                        col1.Add(lngTypeId, "TASKTYPE_ID")
                        col1.Add(sDescr, "DESCRIPTION")
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                        '===If MsgBox("Add the task:" + vbCrLf + vbCrLf + _
                        ''===             sDescr + vbCrLf + vbCrLf + " to this Job List ?" + _
                        ''===                 vbCrLf, vbQuestion + vbDefaultButton2 + vbYesNo) = vbYes Then
                        '-- Add new task to Scratchpad..-
                        mlTempTasksCount = mlTempTasksCount + 1
                        ReDim Preserve malTempTasksIndexes(mlTempTasksCount - 1) '-- expand scratchpad--
                        malTempTasksIndexes(mlTempTasksCount - 1) = 0 '--means a new item.-
                        ReDim Preserve mavTempNewTasks(mlTempTasksCount - 1) '-- expand scratchpad--
                        mavTempNewTasks(mlTempTasksCount - 1) = col1 '--  new..--
                        '--add to list and to job-task table..--

                        '-- add to listview--
                        item1 = New ListViewItem(sDescr)
                        '== item1.Text = sDescr '--1st column.-
                        '=====item1.ListSubItems.Add , , Format(Date, "yyyy-mm-dd") & " " & Format(Time, "hh:mm")
                        item1.SubItems.Add(VB6.Format(CDate(DateTime.Now), "dd-mmm-yyyy"))
                        item1.SubItems.Add(msStaffName)
                        '== sDateSort = VB6.Format(CDate(rs1.Fields("DateCreated").Value), "yyyy-mm-dd hh:mm")
                        item1.SubItems.Add(VB6.Format(CDate(DateTime.Now), "yyyy-mm-dd hh:mm"))
                        item1.Tag = CStr(mlTempTasksCount - 1) '--save index to scratchpad with list item..-
                        ListViewTasks.Items.Add(item1)

                        Call mbSetDataModified()  '== mbDataChanged = True
                        '== If (ListViewTasks.Items.Count > 0) Then '--can complete--
                        '== cmdFinish.Enabled = True
                        '== End If
                        '== 3031 ==15Mar2012==
                        Call mbCheckCompletion()

                        '===End If  '--yes..-
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    End If '--checked..-
                Next ix '--next item..-

            End If '--yes..-
        End If '--have some.-
        col1 = Nothing
        item1 = Nothing
    End Sub '---save-
    '= = = = = = = = = = =
    '-===FF->

    '-- Actual click event..-
    '--Private Sub listTaskTypes_click()

    '--   MsgBox "Actual Click Event..", vbInformation

    '--Call listTaskTypes_saveItem
    '--   cmdAddTask.SetFocus

    '--End Sub  '--cboTasks-Click-
    '== = = = = = = = = = =
    '--  Adding tasks..--
    '--Private Sub cboTasks_gotFocus()
    '-- force drop down..-
    '--    SendKeys "{F4}"    '--send F4..--
    '--End Sub  '--gotFocus..-
    '= = = = = = = = = = =


    '-- Checked a task type..-- May have been turned on or off.--
    '--- no way of examining checked status of list item..
    '---- except by keeping our own status list..-

    'UPGRADE_WARNING: ListBox event ListTaskTypes.ItemCheck has a new behavior. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'

    Private Sub ListTaskTypes_ItemCheck(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.Windows.Forms.ItemCheckEventArgs) _
                                              Handles ListTaskTypes.ItemCheck

        '--reverse current status.---
        maiTaskTypes(eventArgs.Index) = maiTaskTypes(eventArgs.Index) Xor 1 '--flip--

        '--MsgBox "ItemCheck..  Item: " & index & " now has value: " & maiTaskTypes(index)

    End Sub '--itemCheck.--
    '= = = = =  = =  = =  =

    '--  lost focus..==
    Private Sub listTaskTypes_Leave(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles ListTaskTypes.Leave

        If ListTaskTypes.Visible Then '-- wasn't cancelled..-

            '--- Save all checked items..--
            Call listTaskTypes_saveItems()

            ListTaskTypes.Visible = False
            cmdAddTask.Text = "Add Task"
        End If

    End Sub '--lostfocus.-

    '--enter..-
    Private Sub listTaskTypes_KeyPress(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                    Handles ListTaskTypes.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim sText As String
        Dim L1, ix As Integer

        If keyAscii = 27 Then '--ESC.--
            ListTaskTypes.Visible = False
            cmdAddTask.Text = "Add Task"
            cmdAddTask.Focus()
            keyAscii = 0
        ElseIf keyAscii = 13 Then
            '--If (ListTaskTypes.ListIndex >= 0) Then
            '--     Call listTaskTypes_saveItem
            '--Else
            '--sText = Trim(cboTasks.Text)
            '------MsgBox "Text box is:" + cboTasks.Text, vbInformation
            '--L1 = Len(sText)
            '--If (L1 > 0) Then  '--find typed-in item to get ID..-
            '--   For ix = 0 To cboTasks.ListCount
            '--      If UCase(sText) = UCase(Left(cboTasks.List(ix), L1)) Then
            '--          cboTasks.ListIndex = ix
            '--          Exit For
            '--      End If
            '--   Next ix
            '--   If (cboTasks.ListIndex >= 0) Then
            '--      Call cboTasks_saveItem
            '--   End If
            '--End If  '--L1-
            '--End If
            cmdAddTask.Focus()
            keyAscii = 0
        End If '--enter..-

        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = = = = = =  =

    '-- CMD- add another task to this job..--
    Private Sub cmdAddTask_Enter(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles cmdAddTask.Enter

        '====  ListTaskTypes.Visible = False
        '--ListTaskTypes.ListIndex = -1 '--no selection..-
    End Sub '--got focus..-
    '= = = = = = = = = = = = =

    '-- CMD- SHOW task type list..  to add tasks to this job..--
    Private Sub cmdAddTask_Click(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles cmdAddTask.Click
        Dim ix As Integer
        If Not ListTaskTypes.Visible Then
            '--Call mbBuildTaskTypes     '--reset/rebuild combo box..--
            If Not mbBuildTaskTypes() Then
                '---Call mbRollbackTransaction
                MessageBox.Show(Me, "Current updates have been cancelled..", _
                                 "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Call close_me() '= Me.Close() '==Me.Hide
                Exit Sub
            End If '--build task types-

            If ListTaskTypes.Items.Count > 0 Then
                ReDim maiTaskTypes(ListTaskTypes.Items.Count - 1)
                For ix = 0 To UBound(maiTaskTypes)
                    maiTaskTypes(ix) = 0 '--all unchecked..--
                Next ix
            End If '--count.-
            cmdAddTask.Text = "OK"
            ListTaskTypes.Visible = True
            ListTaskTypes.Focus()
            cmdDeleteTask.Enabled = False
            System.Windows.Forms.Application.DoEvents()

        Else '--listbox was visible..
            cmdAddTask.Text = "Add Task"
            '==cmdDeleteTask.Enabled =True
        End If
    End Sub '-- add task--
    '= = = = = = = = = = =
    '-===FF->

    '--  LISTVIEW version..-
    '--  LISTVIEW version..-

    Private Sub listViewTasks_DoubleClick(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs) Handles ListViewTasks.DoubleClick
        Dim kx As Integer
        Dim sSql As String
        Dim s1, s2 As String
        Dim sBarcode, sDescr As String
        Dim lngErr, scrx, task_id, lngaffected As Integer
        Dim sErrorMsg As String
        Dim item1 As System.Windows.Forms.ListViewItem

        '-- if an item is selected, offer to remove it..--
        If Not mbService Then Exit Sub '--job NOT being serviced..--
        On Error Resume Next
        item1 = ListViewTasks.FocusedItem '--   .ListIndex
        lngErr = Err().Number
        On Error GoTo 0
        If (lngErr = 0) And (Not (item1 Is Nothing)) Then
            s1 = item1.Text '-- ListTasks.List(idx)
            scrx = CInt(item1.Tag) '-- ListTasks.ItemData(idx)  '--get scratchpad index..-
            If scrx >= 0 Then  '--fixed 3041.0==
                If MessageBox.Show(Me, "Do you want to DELETE this TASK from the job:" & vbCrLf & vbCrLf & _
                           "Descr:  " & s1 & vbCrLf & vbCrLf, "JobMatix Update", _
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                    task_id = malTempTasksIndexes(scrx)
                    If (task_id > 0) Then '--exists in live table..  must queue for deletion.--
                        mlTaskDeletions = mlTaskDeletions + 1
                        ReDim Preserve malDeletedTasks(mlTaskDeletions)
                        malDeletedTasks(mlTaskDeletions) = task_id '--Queue for deletion at SAVE/EXIT.--
                    ElseIf (task_id = 0) Then  '--was added in this session.-
                        mavTempNewTasks(scrx) = Nothing
                    Else '--was already deleted..-
                        MessageBox.Show(Me, "Task Was already deleted..", _
                                          "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                    malTempTasksIndexes(scrx) = -1 '--mark as deleted..-
                    ListViewTasks.Items.RemoveAt(item1.Index) '--ListTasks.RemoveItem (idx)
                    System.Windows.Forms.Application.DoEvents()
                    Call mbSetDataModified()  '== mbDataChanged = True
                End If '--yes--
            Else
                MessageBox.Show(Me, "Can't find selected irem!!", _
                                  "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If '--part_id-

        End If '--index
    End Sub '--dblclick/remove-
    '= = = = = = = = = = =

    Private Sub listViewTasks_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles ListViewTasks.Click
        cmdDeleteTask.Enabled = True
    End Sub '-- listViewTasks_Click--
    '= = = = = = = =  = =  = = = =

    '-- deleteTask button..-

    Private Sub cmdDeleteTask_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles cmdDeleteTask.Click

        Call listViewTasks_DoubleClick(ListViewTasks, New System.EventArgs())
        cmdDeleteTask.Enabled = False
    End Sub '--cmdDelete..-
    '= = = = = = = = = =
    '-===FF->

    '= 3103.0117-
    '= 3103.0117-

    Private Sub cmdClearSessionTimes_Click(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles cmdClearSessionTimes.Click

        '==  3103.423= Anybody can do it..
        '== If Not gbIsSqlAdmin() Then
        '==    MsgBox("This function for SQL Admin users only.", MsgBoxStyle.Exclamation)
        '==    Exit Sub
        '== End If  '-admin-

        '--Warning.-
        If MessageBox.Show(Me, "Warning: This clears all Session Hours. " & vbCrLf & _
                   " (For Admin use only). " & vbCrLf & _
                   "All previous session times will be erased." & vbCrLf & vbCrLf & _
                   " Do you really want to continue? ", "JobMatix Update", _
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> MsgBoxResult.Yes Then
            Exit Sub
        End If
        '-- do it-
        msTimeSpent = "0"
        msSessionTimesToDate = ""
        txtStaffSessions.Text = ""
        mbSessionTimesReset = True  '--for logging..-

        '-- update cost-
        Call mbShowFullCost()

        Call mbSetDataModified()  '== mbDataChanged = True

    End Sub  '-- cmdClearSessionTimes-
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--EDIT tasks REF table..--
    '--EDIT tasks REF table..--

    '---- pass to ListEdit form..--
    '--Private Function mbEditRefTable(ByVal sTableName As String, _
    ''--                                ByVal sIdColName As String, _
    ''--                                ByVal sDescrColName As String, _
    ''--                                ByVal lngMaxLength As Long, _
    ''--                                ByVal bDeletePermitted As Boolean) As Boolean

    '-- create new definition..-
    Private Sub CmdCreateTask_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles CmdCreateTask.Click
        Dim sDescr As String
        Dim frmListEdit1 As New frmListEdit

        '--mbEditRefTable = False
        '--mlStaffTimeout = -1  '--SUSPEND timing out..--
        'UPGRADE_ISSUE: Load statement is not supported. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"'
        '== Load(frmListEdit)

        frmListEdit1.maxLength = 48
        frmListEdit1.tableName = "JobTaskTypes"
        frmListEdit1.DescrColumn = "TaskTypeDescription"
        frmListEdit1.IdColumn = "TaskType_Id"
        frmListEdit1.PrimaryKeyColName = "TaskType_Id"
        frmListEdit1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + 3000)
        frmListEdit1.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(ListViewTasks.Left) + 5000) '--- ListTasks.Left + 5000
        frmListEdit1.connection = mCnnJobs
        frmListEdit1.deletionsOK = True
        '--MsgBox "Calling edit for: " + sTableName, vbInformation
        frmListEdit1.ShowDialog(Me)
        If Not frmListEdit1.cancelled Then '--update..-

        End If '--cancelled..-
        frmListEdit1.Close()
        '--mlStaffTimeout = 0  '--start timing out..--

        '--Set rs1 = Nothing
    End Sub '--edit..--
    '= = = = = = = = = =
    '-===FF->

    '--COMPUTE- Session time .--

    Private Function mDecComputeSessionTime() As Decimal
        Dim decHours As Decimal = 0
        With cboTimeSelectHours
            If .SelectedIndex >= 0 Then  '-selected
                decHours = CDec(Replace(VB6.GetItemString(cboTimeSelectHours, .SelectedIndex), "Hrs", ""))
            End If
        End With
        With cboTimeSelectTenths
            If .SelectedIndex >= 0 Then  '-selected
                decHours += CDec(VB6.GetItemString(cboTimeSelectTenths, .SelectedIndex))
            End If
        End With '-tenths-
        mDecComputeSessionTime = decHours
    End Function  '-compute-

    '--Session time select.--
    '--Session time select.--

    'UPGRADE_WARNING: Event cboTimeSelect.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboTimeSelectHours_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As System.EventArgs) _
                                                          Handles cboTimeSelectHours.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        '= With cboTimeSelectHours
        '= msSessionTime = Replace(VB6.GetItemString(cboTimeSelectHours, .SelectedIndex), "Hrs", "") '-- current item
        '= End With
        msSessionTime = CStr(mDecComputeSessionTime())
        labResultHours.Text = "= " & msSessionTime & " hrs."  '--show-

        Call mbSetDataModified()  '== mbDataChanged = True
        Call mbShowFullCost() '-- show full labour cost incl. current session.--3107.518 18May2015==

        If Not mbQuotation Then
            Call mbCheckCompletion()
        End If '--quote..--
        '= optChargeable(0).Focus()
        cboTimeSelectTenths.Select()
    End Sub '--cboTimeSelect-HOURS_click--
    '= = = = = = = = = = = = = = = = = = = 

    Private Sub cboTimeSelectTenths_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                       Handles cboTimeSelectTenths.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        msSessionTime = CStr(mDecComputeSessionTime())
        Call mbSetDataModified()  '== mbDataChanged = True
        Call mbShowFullCost() '-- show full labour cost incl. current session.--3107.518 18May2015==
        labResultHours.Text = "= " & msSessionTime & " hrs." '--show-

        If Not mbQuotation Then
            Call mbCheckCompletion()
        End If '--quote..--
        optChargeable(0).Focus()

    End Sub  '= Tenths-
    '= = = = = = = = = = = = = = =
    '-- Can override Min charge..

    Private Sub chkOverrideMin_CheckedChanged(sender As Object, ev As EventArgs)

        Call mbShowFullCost() '-- show full labour cost incl. current session.--3107.518 18May2015==
    End Sub  '-override-
    '= = = = = = = =  = =
    '-- Select Chargable/Non-Chargable--

    'UPGRADE_WARNING: Event optChargeable.CheckedChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub optChargeable_CheckedChanged(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As System.EventArgs) Handles optChargeable.CheckedChanged
        If eventSender.Checked Then
            Dim index As Short = optChargeable.GetIndex(eventSender)
        End If
        Call mbShowFullCost() '-- show full labour cost incl. current session.--3107.518 18May2015==
    End Sub
    '= = = = = = = = = =
    '-===FF->

    '--  lookup/add new part or SERVICE Charge..---
    '--  lookup/add new part---
    '--Private Sub LabPartsHdr_Click()

    Private Sub cmdAddPart_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles cmdAddPart.Click
        Dim index As Short = cmdAddPart.GetIndex(eventSender)
        Dim colFields As Collection
        Dim col1 As Collection
        Dim id, dx, k, i, j, cx, ix, qx As Integer
        Dim idxThisGrid As Integer
        Dim intLastIndex As Integer = -1    '--last added..
        Dim lngId, lngaffected As Integer
        Dim s1, s2 As String
        Dim sSql, sDescr As String
        Dim sDescr2 As String
        Dim sPartNo, sBarcode As String
        Dim sSerialNo As String
        Dim curSell, decCostPrice As Decimal
        Dim sSell, sCost As String
        Dim sStockId As String
        Dim lngStock_id, lngQty As Integer
        Dim bWarranty As Boolean
        Dim bServiceCharge As Boolean
        Dim sW, sName, sShowCost As String
        Dim sErrorMsg As String
        '==Dim sStaff As String
        Dim sDescription, sLongDescription As String
        Dim sCat2, sCat1, sCat3 As String
        Dim sSpecialPrice, sCostPrice As String
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim vasChecklist(,) As String
        '== Dim vaChecklistInput As Object
        Dim frmNewPart1 As New frmNewPart

        cmdDeletePart(index).Enabled = False
        bServiceCharge = IIf((index = 1), True, False)

        '== frmNewPart.connectionJet = mCnnJet
        '== frmNewPart.dbInfoJet = mColJetDBInfo
        frmNewPart1.retailHost = mRetailHost1

        frmNewPart1.GST_Percentage = mCurGST '--V22--
        frmNewPart1.ServiceChargeCat1 = msServiceChargeCat1
        frmNewPart1.ServiceChargeCat2 = msServiceChargeCat2
        If bServiceCharge Then
            frmNewPart1.ServiceChargeRequested = True
        End If
        frmNewPart1.MandatedFormTop = Me.Top + 70  '==SSTab1.Top + ListViewParts(index).Top + 30
        '===frmNewPart.Left = Me.Left + SSTab1.Left + ListViewParts(index).Left + (ListViewParts(index).width \ 3)
        frmNewPart1.MandatedFormLeft = (Me.Left + SSTab1.Left)
        frmNewPart1.ShowDialog(Me)

        If frmNewPart1.cancelled Then
            frmNewPart1.Close()
            Exit Sub
        End If
        bWarranty = frmNewPart1.warranty
        lngQty = frmNewPart1.quantity
        colFields = frmNewPart1.selectedRecord
        sSerialNo = frmNewPart1.SerialNo

        frmNewPart1.Close()
        '-- colFields RECORDSET is from RETAIL-MANAGER Stock Table..--
        '-- colFields RECORDSET is from RETAIL-MANAGER Stock Table..--
        s1 = IIf(bWarranty, "Y", "N")
        col1 = New Collection
        col1.Add("ISWARRANTY", "name")
        col1.Add(s1, "value")
        colFields.Add(col1, "iswarranty") '-- for eventual INSERT..-
        '--  ADD SerialNo to collection..--
        col1 = New Collection
        col1.Add("PARTSERIALNUMBER", "name")
        col1.Add(sSerialNo, "value")
        colFields.Add(col1, "partserialnumber") '-- for eventual INSERT..-

        '--  add to Parts listBox..-
        '-- get stuff from collection..
        sW = " "
        If bWarranty Then sW = "W"
        sCat1 = Trim(colFields.Item("Cat1")("value"))
        sCat2 = Trim(colFields.Item("Cat2")("value"))
        sDescription = Trim(colFields.Item("Description")("value"))
        sStockId = Trim(colFields.Item("stock_id")("value"))
        sBarcode = Trim(colFields.Item("Barcode")("value"))
        sSpecialPrice = Trim(colFields.Item("SpecialPrice")("value"))
        '=3403.607=
        decCostPrice = colFields.Item("cost")("value")
        decCostPrice = decCostPrice + ((decCostPrice * mCurGST) / 100)
        sCostPrice = FormatCurrency(decCostPrice, 2)
        '=3403.628-NO NO   sDescription &= " [CP: " & sCostPrice & " ]"

        '====    curSell = CCur(colFields("sell")("value"))
        '-- add GST..--
        '--- NO!  New Part Form now adds GST before returning..
        '=====   curSell = curSell + ((curSell * mCurGST) / 100)

        curSell = CDec(colFields.Item("SellInclGST")("value"))
        '==sShowCost = Space(10)
        sShowCost = FormatCurrency(curSell, 2)
        If lngQty > 0 Then
            For qx = 1 To lngQty '-- add each instance as a new item..-
                '==ListParts.ItemData(ListParts.NewIndex) = mlTempPartsCount  '--save index to scratchpad with list item..-
                '-- add part COLLECTION to temp parts..-
                mlTempPartsCount = mlTempPartsCount + 1
                ReDim Preserve malTempPartsIndexes(mlTempPartsCount - 1) '-- expand scratchpad--
                malTempPartsIndexes(mlTempPartsCount - 1) = 0 '--means new list item..-
                ReDim Preserve mavTempNewParts(mlTempPartsCount - 1) '-- expand scratchpad--
                mavTempNewParts(mlTempPartsCount - 1) = colFields '-- new part..--
                ReDim Preserve maCurTempPartsCosts(mlTempPartsCount - 1) '-- build parts costs scratchpad--
                maCurTempPartsCosts(mlTempPartsCount - 1) = curSell '-- first lot are from Table..--

                ReDim Preserve malActiveChecklists(mlTempPartsCount - 1) '-- indexes for l/view checklists..
                malActiveChecklists(mlTempPartsCount - 1) = -1 '--Say no checklist this part..

                '-- add to listview--
                item1 = ListViewParts(index).Items.Add(sCat1)
                '--  CHOOSE if ServiceCharge or PART..--
                '-- if SERVICE item, load model checklist, if any..
                '==If Not mbIsServiceCharge(sCat1, sCat2) Then
                '==Set item1 = ListViewParts(k_LV_INDEX_PARTS).ListItems.Add
                '==Else  '--yes.-
                If bServiceCharge Then
                    '--  SAVE current flexgrid data into it's correct checklist..--
                    '== Call mbSaveFlexgridIntoChecklist(fgCheckList, mlCurrentChecklistIndex)
                    Call mbSaveFlexgridIntoChecklist(dgvChecklist, mlCurrentChecklistIndex)

                    '--load checklist (if any) into new grid..
                    idxThisGrid = mbLoadJobChecklist(True, -1, CInt(sStockId), -1, vasChecklist)
                    If (idxThisGrid >= 0) Then
                        malActiveChecklists(mlTempPartsCount - 1) = idxThisGrid '--YES, checklist this part..
                        '--  boosting of FG "control array"  has been done..

                        '== '--make all grids invisible except the chosen one..-
                        '== For ix = 0 To (mlTempPartsCount - 1)
                        '==    cx = malActiveChecklists(ix)  '--see if this is grid control index..-
                        '==    If (cx >= 0) Then
                        '==        If (cx = idxThisGrid) Then '--this is selected item..--
                        '==          '== fgCheckList(idxThisGrid).Visible = True
                        dgvChecklist.Visible = True   '==  fgCheckList.Visible = True

                        LabChecklist.Enabled = True
                        LabChecklist.Text = "Task Checklist for item #" & item1.Index + 1 & ":  " & vbCrLf & sDescription
                        ToolTip1.SetToolTip(LabChecklist, "<c>Double-Click on task status to update the task to a new status..")

                        '--  lOAD checklist data into grid..--
                        '== Call mbLoadFlexgridFromChecklist(idxThisGrid, fgCheckList)
                        Call mbLoadFlexgridFromChecklist(idxThisGrid, dgvChecklist)
                        '==       Else
                        '==          '==  fgCheckList(cx).Visible = False '--don't want to see this one..--
                        '==       End If  '--found..-
                        '==    End If  '--cx-
                        '== Next ix

                    Else '--no checklist this item..-
                        LabChecklist.Text = "No Task Checklist for item #" & item1.Index + 1 & ":  " & vbCrLf & sDescription
                        ToolTip1.SetToolTip(LabChecklist, "")
                        '== For cx = 0 To mlNextChecklistIndex - 1 '--hide all checklists..-
                        '==             fgCheckList(cx).Visible = False '--don't want to see this one..--
                        '== Next cx
                        '==fgCheckList.Visible = False '--don't want to see this one.
                        dgvChecklist.Visible = False '--don't want to see this one.

                        mlCurrentChecklistIndex = -1 '==nothing in grid..-
                    End If '--idx grid..-
                    '== cmdFinish.Enabled = True   '--3031--  11Mar2012-  Can complete now.-
                End If '--service..-
                '== item1.Text = sCat1 '--1st column.-
                item1.SubItems.Add(sDescription)  '= (Trim(colFields.Item("Description")("value"))) '--now 2nd column.-
                item1.SubItems.Add(sBarcode)
                item1.SubItems.Add(sSerialNo)
                item1.SubItems.Add(sW)
                '= item1.SubItems.Add(sStockId)
                item1.SubItems.Add(sCostPrice)
                item1.SubItems.Add(sShowCost)
                item1.SubItems.Add(msStaffName)
                item1.SubItems.Add(sSpecialPrice)
                '=3404.909- k_LV_PARTS_STOCK_ID_IDX
                item1.SubItems.Add(sStockId)  '-k_LV_PARTS_STOCK_ID_IDX-=9..
                item1.Tag = CStr(mlTempPartsCount - 1) '--save index to scratchpad with list item..-
                intLastIndex = item1.Index
                '== -- 3431.0512- maint. Update Job.. 
                '==       On Saving updates, log all Item movements to Service Notes...
                s2 = ""
                If (sSerialNo <> "") Then
                    s2 = " SerialNo= " & sSerialNo & "; "
                End If
                msItemMovementLog &= " --" & Format(TimeOfDay, "HH:mm") & _
                                     " Added Item: " & VB.Left(sDescription, 20) & "; Barcode=" & sBarcode & "; " & s2 & vbCrLf
            Next qx
            '==3061.0=  Select last item added..-
            If (ListViewParts(index).Items.Count > 0) AndAlso (intLastIndex >= 0) Then
                ListViewParts(index).Items(intLastIndex).Selected = True
            End If
            Call mbSetDataModified()  '== mbDataChanged = True
            Call mbCheckCompletion()
        End If '--lngQty-
        msJobStatus = k_statusStarted
        Call mbShowQuotedPartsStatus() '--update quote if there is..-
        Call mbShowFullCost() '-- show full labour cost incl. current session.-- 07Dec2009==
    End Sub '--add part.--
    '= = = = = = = = = = =  =
    '-===FF->

    '-- Function to Remove Part/Service FROM the Job --
    '-- if an item is selected, offer to remove it..--

    Private Function mbRemovePartItem(ByRef ListViewParts_x As ListView, _
                                        ByRef item1 As System.Windows.Forms.ListViewItem) As Boolean

        Dim idx, kx, lngErr As Integer
        Dim idxGrid As Integer
        Dim idxView As Short
        Dim sSql As String
        Dim s1, s2 As String
        Dim sBarcode, sDescr, sSerialNo As String
        Dim sCat1 As String
        Dim scrx, part_id, lngaffected As Integer
        Dim sErrorMsg As String
        '== Dim item1 As System.Windows.Forms.ListViewItem

        mbRemovePartItem = False
        If Not mbService Then Exit Function '--job NOT being serviced..--
        idxView = ListViewParts.GetIndex(ListViewParts_x)
        '== On Error Resume Next
        '== item1 = ListViewParts(idxView).FocusedItem '--   .ListIndex
        '== lngErr = Err().Number
        '== On Error GoTo 0
        '== If (lngErr = 0) Then
        If (item1 Is Nothing) Then
            MessageBox.Show(Me, "Nothing selected", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else '-ok-
            sCat1 = item1.Text '-- ListTasks.List(idx)
            sDescr = item1.SubItems(1).Text '-- ListTasks.List(idx)
            sBarcode = item1.SubItems(2).Text
            sSerialNo = item1.SubItems(3).Text
            scrx = CInt(item1.Tag) '--get scratchpad index..-
            If (scrx >= 0) Then
                If MessageBox.Show(Me, "Do you want to DELETE this part from the job:" & vbCrLf & vbCrLf & _
                        "Descr:  " & sCat1 & "- " & sDescr & vbCrLf & _
                        "Barcode: " & sBarcode & vbCrLf & vbCrLf, "JobMatix Update", _
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then

                    part_id = malTempPartsIndexes(scrx)
                    If (part_id > 0) Then '--exists in live table..  must queue for deletion.--
                        mlPartDeletions = mlPartDeletions + 1
                        ReDim Preserve malDeletedParts(mlPartDeletions)
                        malDeletedParts(mlPartDeletions) = part_id '--Queue for deletion at SAVE/EXIT.--
                    ElseIf (part_id = 0) Then  '--part was added in this session.-
                        mavTempNewParts(scrx) = Nothing
                    Else '--was already deleted..-
                        MessageBox.Show(Me, "Part Was already deleted..", _
                                           "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Function
                    End If
                    malTempPartsIndexes(scrx) = -1 '--mark as deleted..-
                    maCurTempPartsCosts(scrx) = 0 '--either way.. part is gone..-
                    '-- disable and hide checklist..
                    If (idxView = k_LV_INDEX_SERVICE) Then
                        idxGrid = malActiveChecklists(scrx) '--get control array index..-
                        '== If (idxGrid >= 0) Then fgCheckList(idxGrid).Visible = False  '--only if we have a checklist grid..-
                        '===malActiveChecklists(scrx)=-1   '--no checklist to write out..
                        '==  fgCheckList.Visible = False
                        dgvChecklist.Visible = False
                        mlCurrentChecklistIndex = -1 '==nothing in grid..-
                    End If
                    '--ListParts.RemoveItem idx
                    '==3041.0== ListViewParts(idxView).Items.RemoveAt(item1.Index) '--REMOVE from view..--
                    ListViewParts_x.Items.RemoveAt(item1.Index) '--REMOVE from view..--
                    System.Windows.Forms.Application.DoEvents()
                    '== -- 3431.0512- maint. Update Job.. 
                    '==       On Saving updates, log all Item movements to Service Notes...
                    s2 = ""
                    If (sSerialNo <> "") Then
                        s2 = " SerialNo= " & sSerialNo & "; "
                    End If
                    msItemMovementLog &= " --" & Format(TimeOfDay, "HH:mm") & _
                                           " Removed Item: " & VB.Left(sDescr, 20) & "; Barcode=" & sBarcode & "; " & s2 & vbCrLf
                    Call mbSetDataModified()  '== mbDataChanged = True
                    mbRemovePartItem = True
                Else  '--cahange of mind.--
                    mbRemovePartItem = True
                End If '--yes--
            Else
                MessageBox.Show(Me, "Can't find selected item!!", _
                                   "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If '--part_id-
        End If '--nothing.-
        '== End If '--error
        Call mbShowQuotedPartsStatus() '--update quote if there is..-
        Call mbShowFullCost() '-- show full labour cost incl. current session.-- 07Dec2009==

    End Function  '--mbRemovePartItem-
    '= = = = = = = = = = =  =
    '-===FF->

    '-- Remove FROM the Job --
    '--   a SERVICE item or stock part from the ServiceList or Stock list..--
    '-- LISTVIEW Version..-
    '-- index is:
    '---   k_LV_INDEX_PARTS  for parts  and   k_LV_INDEX_SERVICE for service..-

    Private Sub listViewParts_DoubleClick(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs) Handles ListViewParts.DoubleClick
        Dim idxView As Short = ListViewParts.GetIndex(eventSender)
        Dim idx, kx, lngErr As Integer
        Dim idxGrid As Integer
        Dim sSql As String
        Dim s1, s2 As String
        Dim sBarcode, sDescr As String
        Dim sCat1 As String
        Dim scrx, part_id, lngaffected As Integer
        Dim sErrorMsg As String
        Dim item1 As System.Windows.Forms.ListViewItem

        '-- if an item is selected, offer to remove it..--
        If Not mbService Then Exit Sub '--job NOT being serviced..--
        On Error Resume Next
        item1 = ListViewParts(idxView).FocusedItem '--   .ListIndex
        lngErr = Err().Number
        On Error GoTo 0
        If (lngErr = 0) Then
            If (item1 Is Nothing) Then
                MessageBox.Show(Me, "Nothing selected", _
                                  "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else '-ok-

                If Not mbRemovePartItem(ListViewParts(idxView), item1) Then
                    MessageBox.Show(Me, "Couldn't delete..", _
                                       "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            End If '--nothing.-
        End If '--error
        '== Call mbShowQuotedPartsStatus() '--update quote if there is..-
        '== Call mbShowFullCost() '-- show full labour cost incl. current session.-- 07Dec2009==
    End Sub '--dblclick/remove-
    '= = = = = = = = = = =
    '-===FF->

    '== show correct checklist, if any..-

    Private Sub listViewParts_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles ListViewParts.Click
        Dim idxView As Short = ListViewParts.GetIndex(eventSender)
        Dim ix, idxTemp As Integer
        Dim idxGrid, lngErr As Integer
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim sDescription As String

        cmdDeletePart(idxView).Enabled = True

        '--if SERVICE listview..  show correct checklist Grid, if any..
        If (idxView = k_LV_INDEX_SERVICE) Then
            On Error Resume Next
            item1 = ListViewParts(idxView).FocusedItem '--   .ListIndex
            lngErr = Err().Number
            On Error GoTo 0
            If (lngErr = 0) Then
                If (item1 Is Nothing) Then
                    MessageBox.Show(Me, "Nothing selected", _
                                      "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else '-ok-
                    idxTemp = CInt(item1.Tag) '--get scratchpad index..-
                    sDescription = item1.SubItems(1).Text  '--2nd sub-item..
                    LabChecklist.Text = "NO Task checklist for item  #" & item1.Index + 1 & ": " & vbCrLf & _
                                                                                 item1.SubItems(1).Text '--descr..-
                    ToolTip1.SetToolTip(LabChecklist, "") ''-default..-

                    '--  SAVE current flexgrid data into it's correct checklist..--
                    '==      Call mbSaveFlexgridIntoChecklist(fgCheckList, mlCurrentChecklistIndex)
                    Call mbSaveFlexgridIntoChecklist(dgvChecklist, mlCurrentChecklistIndex)

                    If (idxTemp >= 0) Then '--have scratchpad index--
                        '--make all grids invisible except the chosen one..-
                        '==For ix = 0 To (mlTempPartsCount - 1)
                        idxGrid = malActiveChecklists(idxTemp) '--see if this is grid control index..-
                        If (idxGrid >= 0) Then '--have checklist..
                            '==       If (ix = idxTemp) Then '--this is selected item..--
                            '== fgCheckList(idxGrid).Visible = True
                            '== fgCheckList(idxGrid).RowHeight(-1) = 300

                            '==  fgCheckList.Visible = True
                            '==  fgCheckList.set_RowHeight(-1, 300)
                            dgvChecklist.Visible = True
                            '==      dgvChecklist.set_RowHeight(-1, 300)
                            '--  lOAD checklist data into grid..--
                            '== Call mbLoadFlexgridFromChecklist(idxGrid, fgCheckList)
                            Call mbLoadFlexgridFromChecklist(idxGrid, dgvChecklist)

                            LabChecklist.Enabled = True
                            LabChecklist.Text = "Task Checklist for: item #" & item1.Index + 1 & ":  " & vbCrLf & sDescription
                            ToolTip1.SetToolTip(LabChecklist, "<c>Double-Click on task status to update the task to a new status..")
                            '==       Else
                            '==            '== fgCheckList(idxGrid).Visible = False '--don't want to see this one..--
                            '==       End If
                        Else '--no checklist this item....-
                            LabChecklist.Text = "No Task Checklist for item #" & item1.Index + 1 & ":  " & vbCrLf & sDescription
                            ToolTip1.SetToolTip(LabChecklist, "")
                            '== fgCheckList.Visible = False '--don't want to see this one.
                            dgvChecklist.Visible = False '--don't want to see this one.

                            mlCurrentChecklistIndex = -1 '==nothing in grid..-
                        End If
                        '==Next ix
                    Else '--no scratchpad index !!!!..
                        '==LabChecklist.Caption = "No Task checklist.."
                        LabChecklist.Enabled = False
                        mlCurrentChecklistIndex = -1 '==nothing in grid..-
                    End If '-index-
                End If '--nothing..-
            End If '--error.-
        End If '--service..-
    End Sub '--listViewParts_Click-
    '= = = = = = = = = = = =  = = =
    '-===FF->

    '-- ListView parts/Service..
    '--  catch Selection Index change..

    Private Sub listViewParts_SelectionChanged(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles ListViewParts.SelectedIndexChanged
        Dim idxView As Short = ListViewParts.GetIndex(eventSender)
        Dim ix, idxTemp As Integer
        Dim idxGrid, lngErr As Integer
        Dim item1 As System.Windows.Forms.ListViewItem

        '--if SERVICE listview..  show correct checklist Grid, if any..
        If (idxView = k_LV_INDEX_SERVICE) Then
            On Error Resume Next
            item1 = ListViewParts(idxView).FocusedItem '--   .ListIndex
            lngErr = Err().Number
            On Error GoTo 0
            If (lngErr = 0) Then
                If (item1 Is Nothing) Then
                    '== MsgBox("Nothing selected", MsgBoxStyle.Exclamation)
                Else '-ok-
                    Call mbShowChecklist(item1)
                End If  '-nothing.-
            End If  '--error-
        End If '--Service..-

    End Sub  '--SelectionChanged-
    '= = = = = = = = = = = =  = = =
    '-===FF->

    '--cmdDeletePart-

    Private Sub cmdDeletePart_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles cmdDeletePart.Click
        Dim idxView As Short = cmdDeletePart.GetIndex(eventSender)
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngError As Integer

        On Error GoTo cmdDeletePart_error
        '==3041.0= Call listViewParts_DoubleClick(ListViewParts.Item(idxView), New System.EventArgs())
        item1 = ListViewParts(idxView).FocusedItem '--   .ListIndex
        If (item1 Is Nothing) Then
            MessageBox.Show(Me, "Nothing selected", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else '-ok-
            If Not mbRemovePartItem(ListViewParts(idxView), item1) Then
                MessageBox.Show(Me, "Couldn't delete..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If  '--nothing..-
        Exit Sub

cmdDeletePart_error:
        lngError = Err().Number
        MessageBox.Show(Me, "Runtime Error in cmdDeletePart sub.." & vbCrLf & _
                         "Error=" & lngError & ": " & ErrorToString(lngError), _
                            "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub '--delete..-
    '= = = = = = = = = = = = = =

    '-- MENU..  DELETE selected part..-

    Public Sub mnuDeletePart_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuDeletePart.Click
        '== Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngError As Integer

        On Error GoTo mnuDeletePart_error
        If Not (mItemSelectedPart Is Nothing) Then
            '== MsgBox("Please Double-click on item to delete it.", MsgBoxStyle.Information)
            If Not mbRemovePartItem(mItemSelectedPart.ListView, mItemSelectedPart) Then
                MessageBox.Show(Me, "Couldn't delete..", _
                                     "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
        Exit Sub
mnuDeletePart_error:
        lngError = Err().Number
        MessageBox.Show(Me, "Runtime Error in mnuDeletePart sub.." & vbCrLf & _
                         "Error=" & lngError & ": " & ErrorToString(lngError), _
                              "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub '--part descr.-
    '= = = = = = = = = = =
    '-===FF->

    '--  menu stuff for copy part info.-

    '-- COPY Description of selected part..-
    Public Sub mnuCopyPartDescription_Click(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) _
                                        Handles mnuCopyPartDescription.Click
        Dim s1 As String = ""
        If Not (mItemSelectedPart Is Nothing) Then
            My.Computer.Clipboard.Clear() ' Clear Clipboard.
            s1 = Trim(mItemSelectedPart.SubItems(1).Text) '-- descr is item-2nd item..-.
            If (s1 <> "") Then
                My.Computer.Clipboard.SetText(s1) '-- descr is item-2nd item..-.
            Else
                MessageBox.Show(Me, "No description info..", _
                                    "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If  '--empty-
        End If
    End Sub '--part descr.-
    '= = = = = = = = = = =

    '-- COPY Barcode of selected part..-
    Public Sub mnuCopyPartBarcode_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles mnuCopyPartBarcode.Click
        Dim s1 As String = ""
        If Not (mItemSelectedPart Is Nothing) Then
            My.Computer.Clipboard.Clear() ' Clear Clipboard.
            s1 = Trim(mItemSelectedPart.SubItems(2).Text) '-- barcode is 3rd..-.
            If (s1 <> "") Then
                My.Computer.Clipboard.SetText(s1) '-- descr is item-2nd item..-.
            Else
                MessageBox.Show(Me, "No barcode..", _
                                  "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If  '--empty-
            '== My.Computer.Clipboard.SetText(mItemSelectedPart.SubItems(2).Text) '-- barcode is 3rd..-.
        End If
    End Sub '--part barcode.-
    '= = = = = = = = = = =

    '-- COPY Serial-No of selected part..-
    Public Sub mnuCopyPartSerialNo_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuCopyPartSerialNo.Click
        Dim s1 As String = ""
        If Not (mItemSelectedPart Is Nothing) Then
            My.Computer.Clipboard.Clear() ' Clear Clipboard.
            s1 = Trim(mItemSelectedPart.SubItems(3).Text)
            '== My.Computer.Clipboard.SetText(mItemSelectedPart.SubItems(3).Text)
            If (s1 <> "") Then
                My.Computer.Clipboard.SetText(s1) '-- descr is item-2nd item..-.
            Else
                MessageBox.Show(Me, "No Serial-no..", _
                                  "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If  '--empty-
        End If
    End Sub '--part serial.-
    '= = = = = = = = = = =
    '-===FF->

    '--mouse activity---
    '-- Popup COPY Part info menu..--
    '-- Popup COPY Part info menu..--

    Private Sub listViewParts_MouseDown(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles ListViewParts.MouseDown
        Dim iButton As Short = eventArgs.Button \ &H100000
        Dim iShift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        Dim idxView As Short = ListViewParts.GetIndex(eventSender)
        Dim lRow, ix, lCol As Integer
        Dim sName As String
        Dim j, i, k As Integer
        Dim index As Short
        Dim item1 As System.Windows.Forms.ListViewItem

        item1 = ListViewParts(idxView).FocusedItem
        '-index=listViewParts.li
        '====If index < mlGoodsCount Then '--exists.-
        If Not (item1 Is Nothing) Then '--item selected.. -
            If iButton = 1 Then '--left --
                '-- MsgBox "Left Mouse clicked on row: " & index & "..", vbInformation
            ElseIf iButton = 2 Then  '--right..-
                '--If MsgBox("Delete the item: '" + txtGoodsList(index).Text + "' ??", _
                ''--                                 vbYesNo + vbDefaultButton2 + vbQuestion) = vbYes Then
                '-- Avoid the 'disabled' gray text by locking updates
                LockWindowUpdate(ListViewParts(idxView).Handle.ToInt32)
                '---- A disabled TextBox will not display a context menu
                ListViewParts(idxView).Enabled = False
                '--- Give the previous line time to complete
                System.Windows.Forms.Application.DoEvents()
                '-- Display our own context menu
                mItemSelectedPart = item1 '--pass item to mnu routine..-
                'UPGRADE_ISSUE: Form method frmJobMaint3.PopupMenu was not upgraded. Click for more: 
                '== 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'

                '== TO FIX -->    PopupMenu(mnuSelectedPartInfo)
                '== MsgBox("Popup menu still to come..", MsgBoxStyle.Information)

                mContextMenuPartInfo.Show(CType(eventSender, Control), ListViewParts(idxView).Location)

                ' Enable the control again
                ListViewParts(idxView).Enabled = True
                '-- Unlock updates
                LockWindowUpdate(0)
                '--End If  '--yes..-
            End If '--button..-
        End If '--nothing--
    End Sub '--mouse..-
    '= = = = = = = = =  =
    '-===FF->

    '=3203.106= get printer sel..
    '--catch printer combo selections..--
    '-- update settings.-

    Private Sub cboColourPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboColourPrinters.SelectedIndexChanged
        '= Dim sName As String
        If mbIsInitialising Then Exit Sub
        If (cboColourPrinters.SelectedIndex >= 0) Then
            msColourPrinterName = cboColourPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(gK_SETTING_PRTCOLOUR, msColourPrinterName) Then
                MessageBox.Show(Me, "Failed to save COLOUR printer setting.", _
                                 "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If '-index-
    End Sub '--  cboColourPrinters-
    '= = = = = = = = = = = = = = = = = =

    Private Sub cboReceiptPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                     ByVal e As System.EventArgs) _
                                                     Handles cboReceiptPrinters.SelectedIndexChanged
        '= Dim sName As String
        If mbIsInitialising Then Exit Sub
        If (cboReceiptPrinters.SelectedIndex >= 0) Then
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(gK_SETTING_PRTRECEIPT, msReceiptPrinterName) Then
                MessageBox.Show(Me, "Failed to save RECEIPT printer setting.", _
                                   "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If '-index-
    End Sub '--  cboReceiptPrinters-
    '= = = = = = = = = = = == = = = = 

    '=3203.116-  Warn about selecting images..

    Private Sub _chkPrtDocs_Report_CheckedChanged(sender As Object, e As EventArgs) _
                                                   Handles _chkPrtDocs_Report.CheckedChanged
        Dim intCount As Integer
        Dim listIDs As List(Of Integer)

        intCount = mIntGetSelectedImageIds(listIDs)
        If intCount >= 0 Then
            If _chkPrtDocs_Report.CheckState = CheckState.Checked Then
                If intCount = 0 Then
                    MessageBox.Show(Me, "Note: " & vbCrLf & _
                                    "NO image has been selected for Customer Report.", _
                                        "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    MessageBox.Show(Me, "NB: " & vbCrLf & CStr(intCount) & _
                                    " images have been selected for Customer Report.", _
                                    "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If  '-count-
            End If  '-checked-
        Else
            '-- nothing on file.-
        End If
    End Sub  '-- report check changed.-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '== Target-New-Build-6201 --  (15-July-2021) for Open Source..
    '== Target-New-Build-6201 --  (15-July-2021) for Open Source..
    '-- print Service record-- Check changed.

    Private Sub _chkPrtDocs_0_CheckedChanged(sender As Object,
                                              ev As EventArgs) Handles _chkPrtDocs_0.CheckedChanged

        If _chkPrtDocs_0.Checked Then
            If (Not mbIsJobmatixPOS()) Then '= is legacy mbHostIsJobTracking Then
                chkPrintItemBarcodes.Checked = True
            Else
                '--POS-
                '=chkPrintItemBarcodes.Checked = False
            End If
        Else
            '- unchecked.
            If (Not mbIsJobmatixPOS()) Then '=  mbHostIsJobTracking Then
                chkPrintItemBarcodes.Checked = False
            Else
                '-POS-
                chkPrintItemBarcodes.Checked = False
            End If  '--job tracking.
        End If  '--checked-
    End Sub  '-_chkPrtDocs_0_CheckedChanged-
    '= = = = = = = = =
    '== END Target-New-Build-6201 --  (15-July-2021) for Open Source..
    '-===FF->

    '--  Print documents as requested..--
    '--  Print documents as requested..--

    Private Function mbPrintAllRequests(ByVal sTask As String) As Boolean
        Dim ans As Short
        Dim lngError As Integer
        Dim bCancelled As Boolean
        Dim bPrintingReceipt As Boolean
        Dim bReceiptOk As Boolean
        Dim bNewPrinter As Boolean
        Dim SMsg As String
        Dim colReportLines As Collection
        '== Dim frmPrtSelect1 As frmPrtSelect

        bCancelled = False
        bPrintingReceipt = False
        mbPrintAllRequests = False
        '==  Print receipt..  ask later..

        '--  Build Receipt lines collection..--
        '--SHRINK== If (chkPrtDocs(k_CheckPrtDocket).CheckState = 1) Then '-- Rev-2902- QUOTATON-  Can print docket if CHECKED..--
        If (_chkPrtDocs_1.CheckState = 1) Then '-- Rev-2902- QUOTATON-  Can print docket if CHECKED..--
            If Not mbBuildDocket(mlJobId, colReportLines) Then '-ok-
                MessageBox.Show(Me, "Error in building Receipt content.", _
                                  "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                bCancelled = True
            Else '--ok-
                bReceiptOk = mbPrintReceipt(colReportLines)
                bPrintingReceipt = True
            End If
        End If '--delivered.-
        '-- SEND Receipt..  ask later..--

        '--Print form..--
        ans = 0 : bCancelled = False
        '---If Not mbService Then  '--print delivery..--
        '==SHRINK== If (chkPrtDocs(k_CheckPrtRecord).CheckState = 1) Then '--print service record...--
        If (_chkPrtDocs_0.CheckState = 1) Then '--print service record...--
            '==LabReturned.Visible = False
            '== Printer = mPrtColour '-- set main printer--
            While (ans <> MsgBoxResult.Yes) And (Not bCancelled)
                '--On Error Resume Next
                '--Me.PrintForm
                '--lngError = Err()
                '--On Error GoTo cmdFinish_error:
                '== Set Printer = mPrtColour '-- set main printer--
                lngError = mbPrintJobMaintForm()
                System.Windows.Forms.Application.DoEvents()
                If lngError <> 0 Then '--failed..-
                    ans = MessageBox.Show(Me, "Printing can not start..  Check Printer Server..", _
                                 "JobMatix Update", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If (ans = MsgBoxResult.Cancel) Then
                        bCancelled = True
                        '--Exit Sub
                    End If '--cancel-
                Else '--started ok.-
                    '-- 15 sec delay showing progress bar..--
                    '===Call mbWaitProgress("-Printing Service Record-")
                    SMsg = "This Form Should be printed before " & sTask & " is complete..'"
                    If Not (mbService Or mbDelivery) Then SMsg = ""
                    ans = MessageBox.Show(Me, SMsg & vbCrLf & vbCrLf & _
                                "Has Service Record for Job-no: " & CStr(mlJobId) & " printed ok?", _
                                "JobMatix Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    If (ans = MsgBoxResult.Cancel) Then
                        bCancelled = True
                    ElseIf (ans = MsgBoxResult.No) Then
                        '== GoSub cmdFinish_NewPrinter
                        bNewPrinter = False
                        ans = MsgBoxResult.No '--force reprint..-
                    End If '--cancel-
                End If '--error-
            End While '--ans-
        End If '--not service.--
        If bCancelled Then
            '== cmdExit.Enabled = True
            '===  ????  ==  Exit Sub  '-- go back..-
        End If

        '= 3203.110 10Jan2016=
        '--  Print Customer Report if checked..-

        If (_chkPrtDocs_Report.CheckState = 1) Then
            Call mbPrintCustomerReport()

        End If  '-report-

        System.Windows.Forms.Application.DoEvents()
        '-- Check if Receipt done OK..--
        '-- RETRY Receipt loop--
        ans = 0
        '===If (Not mbQuotation) And (Not mbService) And (Not mbNotify) And _
        ''===                         Left(msJobStatus, 2) >= "70" Then   '-- mbDelivery or ViewAll  Can print docket if delivered..--
        If bPrintingReceipt Then
            While (ans <> MsgBoxResult.Yes) And (ans <> MsgBoxResult.Cancel)
                ans = MessageBox.Show(Me, "Has the DELIVERY DOCKET for Job-no: " & CStr(mlJobId) & " printed ok?", _
                                      "JobMatix Update", MessageBoxButtons.YesNoCancel, _
                                                         MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                '-- Print again if needed..--
                If (ans <> MsgBoxResult.Yes) And (ans <> MsgBoxResult.Cancel) Then
                    bReceiptOk = mbPrintReceipt(colReportLines)
                End If
            End While '--retry receipt....-
        End If '--deliver receipt.-
        mbPrintAllRequests = True
        '--cancel ??--
        '-- end --
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '--mRsJob.Close
        colReportLines = Nothing

    End Function
    '= = = = = = = = =  =
    '-===FF->

    '== SAVE and Print = = = = = ==
    '== SAVE and Print = = = = = ==

    '---   Delivery or Servicing complete..--

    '== Private Sub cmdFinish_Click(ByVal eventSender As System.Object, _
    '==                                ByVal eventArgs As System.EventArgs) Handles cmdFinish.Click
    Private Function mbMarkAsCompleted() As Boolean

        Dim ix, iPos As Integer
        '== Dim ans As Short
        Dim bCancelled As Boolean
        '== Dim bPrintingReceipt As Boolean
        '== Dim bReceiptOk As Boolean
        '== Dim bNewPrinter As Boolean
        Dim s1 As String
        Dim sStuff, sFinish As String
        Dim sTask, sMsg As String
        Dim sRem1 As String
        Dim sRem2, sChunk As String
        Dim lngaffected As Integer
        Dim lngError As Integer
        Dim sErrorMsg As String
        Dim sPriority As String
        Dim sShortDate As String '-- dd-mmm-yyyy --
        Dim colReportLines As Collection
        Dim vline As Object
        '== Dim control1 As System.Windows.Forms.Control
        '== Dim frmPrtSelect1 As frmPrtSelect

        '== On Error GoTo cmdFinish_error
        mbMarkAsCompleted = False
        bCancelled = False
        '== bPrintingReceipt = False
        sTask = "DELIVERY" '--lngCurrentJobNo = 0
        '== cmdExit.Enabled = False
        cmdCancel.Enabled = False
        '== sFinish = cmdFinish.Text '--save--
        '--check all stuff (flds) have been done..--
        If mbService Then '--job is being serviced..--
            sStuff = msCheckFormComplete()
            If sStuff <> "" Then
                MessageBox.Show(Me, "This Service Form is not complete.." & vbCrLf & _
                          "Still to be done: " & vbCrLf & sStuff & vbCrLf, _
                             "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '= cmdExit.Enabled = True
                cmdCancel.Enabled = True
                Exit Function
            Else '--ok--
                s1 = ""
                '-- update data and set status as completed..--
                If (msSessionTime = "0") And (CDec(msTimeSpent) = 0) Then '--Tasks but no time..--
                    s1 = "(Note: No labour hours have been recorded..)" & vbCrLf
                End If
                '- add (ListViewTasks.Items.Count <= 0)=
                If (ListViewTasks.Items.Count <= 0) Then '--No Tasks..--
                    s1 &= "(Check: No tasks have been recorded..)" & vbCrLf
                End If
                If MessageBox.Show(Me, "This will complete all servicing on the job.." & vbCrLf & _
                            s1 & vbCrLf & "No more service data will be accepted.. " & vbCrLf & _
                             "The job will be marked as ready for delivery, " & vbCrLf & _
                           "  and the Customer Notification screen will be displayed.. " & vbCrLf & _
                          vbCrLf & "Do you want to continue and complete this job? " & vbCrLf & vbCrLf, _
                       "JobMatix Update", MessageBoxButtons.YesNo, _
                                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                    msJobStatus = k_statusCompleted
                    LabTicket.BackColor = System.Drawing.Color.Lime
                    labJobStatus.Text = k_statusCompleted
                Else
                    '== cmdExit.Enabled = True
                    cmdCancel.Enabled = True
                    Exit Function
                End If
                sTask = "SERVICE" '--lngCurrentJobNo = 0
                '--Call mbUpdatejob
                '--  and change colour..--
                '--Me.Hide
                '--Exit Sub
            End If '--stuff..-
        ElseIf mbDelivery Then
            If MessageBox.Show(Me, "This will complete Delivery of this job:" & vbCrLf & _
                       "Do you want to continue? " & vbCrLf & vbCrLf, _
                       "JobMatix Update", MessageBoxButtons.YesNo, _
                             MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                msJobStatus = k_statusDelivered
                labJobStatus.Text = k_statusDelivered
            Else  '- don't exit..
                '== cmdExit.Enabled = True
                cmdCancel.Enabled = True
                Exit Function
            End If '--yes.-
        End If '--service.--
        sShortDate = VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy")
        '-- Save job Status and print..
        '---and update ticket colour on form..
        '--Call mbUpdateJob
        System.Windows.Forms.Application.DoEvents()
        If (mbService Or mbDelivery) Then '--can update/commit--
            If Not mbUpdateJob() Then
                MessageBox.Show(Me, "mbUpdateJob call failed from cmdFinish routine..", _
                                   "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '== Me.Close() '== Me.Hide
                Exit Function
                '--mCnnJobs.RollbackTrans
                '--MsgBox "ROLLBACK TRANSACTION executed..", vbInformation
                '--If Not gbExecuteCmd(mCnnJobs, "ROLLBACK TRANSACTION", lngaffected, sErrorMsg) Then
                '--           MsgBox "Failed in ROLLBACK TRANSACTION.." + vbCrLf + sErrorMsg, vbCritical
                '--End If  '--rollback.--
            Else '--update ok--
                '--mCnnJobs.CommitTrans
                '--If gbDebug Then MsgBox "COMMIT TRANSACTION executed..", vbInformation
                '--If Not gbExecuteCmd(mCnnJobs, "COMMIT TRANSACTION", lngaffected, sErrorMsg) Then
                '--           MsgBox "Failed in COMMIT TRANSACTION.." + vbCrLf + sErrorMsg, vbCritical
                '--End If  '--commit.--
            End If '--update--
        End If '--service--

        '== cmdExit.Visible = False
        cmdCancel.Visible = False
        '== cmdFinish.Caption = ""
        '== cmdExit.Caption = ""
        System.Windows.Forms.Application.DoEvents()
        '--NOTIFY.. REv-2476..-
        If mbService Then '--job is being serviced/completed..--
            LabTicket.BackColor = System.Drawing.Color.Lime
            '== FrameSessionTime.Visible = False
            FrameSessionTime.Enabled = False
            '--notify customer..--
            '--  update Notification on Job Record..--
            '===MsgBox "This job is Complete.." + vbCrLf + "Please notify customer, and " + vbCrLf + _
            ''===       "RECORD THE RESULT" + vbCrLf + " in the Notification frame on the Job Form.", vbExclamation
            Call mbAcceptNotification()
        End If '--service..-

        '--  PRINTING..  see above function..--

        '== Call mbPrintAllRequests(sTask)

        If mbService Then '--job is being serviced/completed..--
            '===LabTicket.BackColor = vbGreen
            '===FrameSessionTime.Visible = False
            '--notify customer..--
            '--  update Notification on Job Record..--
            '===MsgBox "This job is Complete.." + vbCrLf + "Please notify customer, and " + vbCrLf + _
            ''===       "RECORD THE RESULT" + vbCrLf + " in the Notification frame on the Job Form.", vbExclamation
            '===Call mbAcceptNotification
            '===ElseIf Not mbNotify Then '-- mbDelivery or ViewAll  Can print docket..--
            '== ElseIf Not mbNotify And VB.Left(msJobStatus, 2) >= "70" Then  '-- mbDelivery or ViewAll  Can print docket if delivered..--
        ElseIf VB.Left(msJobStatus, 2) >= "70" Then  '-- mbDelivery or ViewAll  Can print docket if delivered..--
            '--delivery done..-
            '--delivery done..-
            msJobStatus = k_statusDelivered
            '--print deleivery docket.--
            '--Call mbUpdatejob
            LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(k_colourDelivered) '--light grey..-
        End If '--delivery..-

        '==  NO  !!!!    mbDataChanged = False  '-- FORCE Query-unload to let it go..--
        '== mbUpdateFinished = True
        '--Set mRsJob = Nothing
        '==Me.Close() '== Me.Hide
        mbMarkAsCompleted = True
        Exit Function

cmdFinish_error:
        lngError = Err().Number
        MessageBox.Show(Me, "Runtime Error in Completed function.." & vbCrLf & _
                         "Error=" & lngError & ": " & ErrorToString(lngError), _
                              "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Call close_me() '= Me.Close() '==Me.Hide
    End Function '--Finish--
    '= = = = = = = = = = =
    '-===FF->

    '-- Service or View only-
    '--To Save/exit OR Save/Suspend.. job..--
    '-- KEEP or set "Started" status..--

    Private Function mbSaveAndExit(ByVal bSuspendJob As Boolean, _
                                          ByVal bMarkQA As Boolean) As Boolean
        Dim sErrors As String
        Dim L1 As Integer
        Dim sStuff As String
        Dim sMsg, sTimeStamp As String

        '==On Error GoTo SaveAndExit_Error
        mbSaveAndExit = False
        If (mbService) Then '= If (mbService Or mbDelivery) Then
            '=3357.0223= Update on status change. even if no other Changes.
            sTimeStamp = msStaffName & ": " & _
                                  Format(CDate(Now), "dd-MMM-yyyy hh:mm tt ")  '--staff/date prefix..-
            If (Not (mbDataChanged Or bSuspendJob Or bMarkQA)) AndAlso _
                                        (VB.Left(msJobOriginalStatus, 2) <> "40") AndAlso _
                                                (VB.Left(msJobOriginalStatus, 2) <> "20") Then '-- no changes/ no suspend.--
                '-- no changes/ no suspend.--  AND NOT changing from QA or SUSP. back to started..
                '=If mbService Then
                If mbStatusWasSuspOrQA Then  '-original status was Susp or QA-
                    Call mbReleaseJob(k_statusStarted) '-- Reset status to STARTED--
                Else
                    Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
                End If
                '= End If
                mbSaveAndExit = True   '--finished..-
                Exit Function
                'ElseIf Not (bSuspendJob Or bMarkQA) Then '-- changes, but no suspend or QA.--
                '    '=If mbService Then
                '    If mbStatusWasSuspOrQA Then  '-original status was Susp or QA-
                '        Call mbReleaseJob(k_statusStarted) '-- Reset status to STARTED--
                '        '=3311.802= Call mbReleaseJob(msJobOriginalStatus) 
                '        '=3311.731-NO! might be in QA=-- Reset status to STARTED--
                '    Else
                '        Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
                '    End If
                '    '=End If
                '    mbSaveAndExit = True   '--finished..-
                '    Exit Function
            Else '--mbDataChanged Or Suspend/QA. --  or Back to bench from QA..
                '=If (mbService) Then
                If (msSessionTime = "0") Then '--no time..--
                    sMsg = "Work has been done on this job, but" & vbCrLf & _
                          "no labour hours have been selected.. " & vbCrLf & vbCrLf & _
                                             "Do you still want to leave this session?"
                    '==cmdExit.Enabled = True
                    '==cmdCancel.Enabled = True
                    '==Exit Sub
                    '==End If  '==yes-
                ElseIf (optChargeable(0).Checked = False) And (optChargeable(1).Checked = False) Then  '--No indic of charge.-
                    MessageBox.Show(Me, _
                                    "No selection has been made for Charge/No-Charge of SessionTime..", _
                                                   "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '== cmdExit.Enabled = True
                    cmdCancel.Enabled = True
                    Exit Function  '==3311.731= He must fix it..
                Else
                    sMsg = "OK to leave this service job ?"
                    If bSuspendJob Then
                        sMsg = "OK to Suspend this Job ?"
                    ElseIf bMarkQA Then
                        sMsg = "OK to move this Job to QA ?"
                        If (VB.Left(msJobOriginalStatus, 2) >= "40") Then '--currently in QA..-
                            sMsg = "OK to move this Job back to Bench ?"
                        End If
                    End If
                End If '--time..-
                '=End If '--service..-
                '-- confirm if job is to be put aside..--
                If Not MessageBox.Show(Me, sMsg, _
                                  "JobMatix Update", MessageBoxButtons.YesNo, _
                                  MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = MsgBoxResult.Yes Then
                    txtDiagnosis.Focus()
                    Exit Function '--was mistake..  keep going..
                Else '--yes--
                    If (txtWorkDetails.Text <> "") Then
                        txtWorkDetails.Text = txtWorkDetails.Text & vbCrLf
                    End If
                    msJobStatus = msJobOriginalStatus '=3311.731 k_statusStarted
                    If bSuspendJob Then
                        msJobStatus = k_statusSuspended
                        txtWorkDetails.Text = txtWorkDetails.Text & vbCrLf & _
                                             "-- JOB SUSPENDED --" & vbCrLf & sTimeStamp
                    ElseIf bMarkQA Then
                        If (VB.Left(msJobOriginalStatus, 2) <= "39") Then '--currently NOT in QA..-
                            txtWorkDetails.Text = txtWorkDetails.Text & vbCrLf & _
                                                      "-- Job Sent for QA.. --" & vbCrLf & sTimeStamp
                        Else '--  else was in qa..  mark as Started..-
                            txtWorkDetails.Text = txtWorkDetails.Text & vbCrLf & _
                                                       "-- Job Retained in QA.. --" & vbCrLf & sTimeStamp
                        End If
                        msJobStatus = k_statusQA
                    Else '-started checked..
                        msJobStatus = k_statusStarted
                        If (VB.Left(msJobOriginalStatus, 2) = "40") Or _
                                     (VB.Left(msJobOriginalStatus, 2) = "20") Then '--currently in QA or was Susp...-
                            txtWorkDetails.Text = txtWorkDetails.Text & vbCrLf & _
                                                         "-- Job sent back to Bench.. --" & vbCrLf & sTimeStamp
                        End If  '--was qa.-
                    End If '-sstatus-
                End If  '-yes.-
                If Not mbUpdateJob() Then
                    MessageBox.Show(Me, "mbUpdateJob call failed in cmdExit routine..", _
                                        "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '== Me.Close() '== Me.Hide
                    Exit Function
                    '--mCnnJobs.RollbackTrans
                Else '--ok--
                    '==  NO!!! mbDataChanged = False  '-- Force Query-unload to let it go..--
                    '== mbUpdateFinished = True  '--for closing event..
                    '--mCnnJobs.CommitTrans
                    '--If gbDebug Then MsgBox "COMMIT TRANSACTION executed..", vbInformation
                    mbSaveAndExit = True   '--can finish..-
                End If
                '== Me.Close() '== Me.Hide
            End If '--changed..--
        Else '-- view only..-
            '= Me.Close() '== Me.Hide
        End If '--changed.--
        Exit Function

SaveAndExit_Error:
        L1 = Err().Number
        MessageBox.Show(Me, "Runtime Error in SaveExitt function.." & vbCrLf & _
                                "Error=" & L1 & ": " & ErrorToString(L1), _
                                   "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '== Me.Close()
    End Function '--exit--
    '= = = = = = = = = = =
    '-===FF->

    '--Norrmal Save/exit job..--
    '--Norrmal Save/exit job..--

    '== Private Sub cmdExit_Click(ByVal eventSender As System.Object, _
    '==                              ByVal eventArgs As System.EventArgs)

    '==     Call mbSaveAndExit(False, False) '--exit, no suspend.. no QA-
    '== End Sub '--cmdExit..-
    '= = = = = = = = = =

    '--Save and Suspend Job..--
    '--Save and Suspend Job..--

    '== Private Sub cmdSuspend_Click(ByVal eventSender As System.Object, _
    '==                                  ByVal eventArgs As System.EventArgs)

    '==     Call mbSaveAndExit(True, False) '--save and Suspend..-

    '== End Sub '-- Suspend..-
    '= = = = = = = = = = =

    '--  mark as QA..  OR Return to bench..--

    '= Private Sub cmdQA_Click(ByVal eventSender As System.Object, _
    '==                            ByVal eventArgs As System.EventArgs)
    '==     Call mbSaveAndExit(False, True) '--save and QA or RECYCLE..-

    '== End Sub '--qa..-
    '= = = = = = = = =

    '-- CMD F I N I S H  --
    '-- CMD  F I N I S H  --
    '-===FF->

    '== ALL SAVE and Print exits = = = = = ==

    '-- SHOW Popup EXIT menu..
    '--  Save choice in "mIntExitMenuSelectedIndex"  ==

    '= MENU NOT USED ==

    '== Private Sub labExitAction_Click(ByVal sender As System.Object, _
    '==                                     ByVal e As System.EventArgs) Handles labExitAction.Click

    '=     mContextMenuExit.Show(CType(sender, Control), txtExitText.Location)

    '== End Sub  '--ExitAction_Click-
    '= = = = = = = = = = = = = = == 

    '-- optSave--  Click--

    Private Sub optSaveExit_CheckedChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) _
            Handles optSaveExit.CheckedChanged, optSuspend.CheckedChanged, optQA.CheckedChanged, optCompleted.CheckedChanged

        '== mbSetDataModified(True)  '==cmdFinish.Enabled = True
        If optCompleted.Checked Then
            _chkPrtDocs_0.CheckState = System.Windows.Forms.CheckState.Checked '--default.-
        End If  '--optCompl.-

        '--  changing status..  can finish..-
        '---- IF Job was Susp.n QA...  optSaveExit can set Finish..
        If (optSuspend.Checked Or optQA.Checked Or optCompleted.Checked Or _
                                            (optSaveExit.Checked And mbStatusWasSuspOrQA)) Then
            mbSetDataModified(True)  '==cmdFinish.Enabled = True
        End If

    End Sub  '-opt click..-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '== SAVE and Print = = = = = ==

    Private Sub cmdFinish_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles cmdFinish.Click
        Dim bExitOK As Boolean = False
        Dim sPrintCaption As String = "JOB UPDATE.."
        '===      '==Select Case cboExitAction.SelectedIndex

        If mbDelivery Then
            bExitOK = mbMarkAsCompleted() '--mark delivery/print..-
        ElseIf mbService Then  '--service or View..-
            '==   Select Case LCase(msExitMenuSelectedName)
            If optSaveExit.Checked Then  '--Actually is "Started"-
                bExitOK = mbSaveAndExit(False, False) '--exit, no suspend.. no QA-
            ElseIf optSuspend.Checked Then
                bExitOK = mbSaveAndExit(True, False) '--save and Suspend..-
            ElseIf optQA.Checked Then
                bExitOK = mbSaveAndExit(False, True) '--save and QA..-
            ElseIf optCompleted.Checked Then
                bExitOK = mbMarkAsCompleted() '--complete..-
                sPrintCaption = "JOB COMPLETED.."
            Else  '--nothing..
                If Not (mbService Or mbDelivery) Then '--can be VIEW only--
                    '==  ?? ==   Call mbMarkAsCompleted() '--complete..-
                Else
                    MessageBox.Show(Me, "No exit mode/status selected..", _
                                       "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If  '--opt.-
        Else  '--view..-
            sPrintCaption = "VIEWING.."
            bExitOK = True   '--view exits on finish.-
        End If  '--delivery.-
        '--  close or not..-
        If bExitOK Then  '--done.-
            Call mbPrintAllRequests(sPrintCaption)
            mbUpdateFinished = True   '--gets through formClosing..
            Call close_me() '= Me.Close()
            '-- else stay in this form.-
        End If
    End Sub  '-- Finish --
    '= = = = = = = = = = = 
    '-===FF->

    '-- Cancel/Exit..--
    '-- Cancel/Exit..--
    Private Sub Cancel_Exit_Process()
        Dim sMsg, sErrors As String
        Dim L1 As Integer

        On Error GoTo cmdCancelExit_error
        sMsg = "DISCARD changes and exit this Job ?"
        If mbDelivery Then sMsg = "Cancel this Delivery ?"
        If (mbService Or mbDelivery) Then
            If (mbService And (Not mbDataChanged)) Then  '== And (mIntExitMenuSelectedIndex <= 0) Then '-- no changes.--
                If mbService Then Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
                '-- In case cmdFinish was disabled, but printing requested..
                If (LCase(cmdCancel.Text) = "exit") Then
                    Call mbPrintAllRequests("EXIT PRINTING")
                End If
                mbUpdateFinished = True   '--gets through formClosing..
                Call close_me() '= Me.Close()
                '==Me.Hide
            Else '--mbDataChanged or Delivery..--
                '-- confirm if changes are to be trashed...--
                If MessageBox.Show(Me, sMsg & vbCrLf, _
                                   "JobMatix Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                       MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                    '==  NOT HERE !!==  txtDiagnosis.Focus()
                    '==Exit Sub '--was mistake..  keep going..
                    '--YES, scrap all changes.. --
                    msJobStatus = k_statusStarted
                    If mbService Then Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
                    mbUpdateFinished = True   '--gets through formClosing..
                    If (LCase(cmdCancel.Text) = "exit") Then Call mbPrintAllRequests("EXIT PRINTING")
                    Call close_me() '= Me.Close()
                    '===Me.Hide
                Else  '--NO, keep and continue..--
                    '-  should fall out of sub..--
                End If
            End If '--changed..--
        Else '-- view only..-
            If (LCase(cmdCancel.Text) = "exit") Then
                Call mbPrintAllRequests("EXIT PRINTING")
            End If
            mbUpdateFinished = True   '--gets through formClosing..
            Call close_me() '= Me.Close()
            '==Me.Hide
        End If '--changed.--
        Exit Sub

cmdCancelExit_error:
        L1 = Err().Number
        MessageBox.Show(Me, "Runtime Error in CANCEL-EXIT sub." & vbCrLf & _
                             "Error=" & L1 & ": " & ErrorToString(L1), _
                               "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Call close_me() '= Me.Close()
    End Sub '--exit--
    '= = = = = = = = = = =

    '-- Actual Cancel/Exit EVENT..--
    '-- Calls Cancel/Exit because if EXIT SUB failure
    '----    in event routine..--
    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Dim L1 As Integer
        '=== On Error GoTo cmdCancel_error


        Call Cancel_Exit_Process()
        '== Exit Sub

        '=== cmdCancel_error:
        '=== L1 = Err().Number
        '== MsgBox("Runtime Error in cmd CANCEL." & vbCrLf & "Error=" & L1 & ": " & ErrorToString(L1), MsgBoxStyle.Critical)
        '=== Me.Close()
    End Sub '--exit--
    '= = = = = = = = = = =

    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)

    '-- Close_me..

    Private Sub close_me()
        'Dim bCancel As Boolean = False '= = EventArgs.Cancel
        ''= Dim intMode As System.Windows.Forms.CloseReason = EventArgs.CloseReason

        '- inform parent.-
        '- Report to Parent..-
        mbUpdateFinished = True   '--gets through formClosing..

        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

    End Sub '--close me-
    '= = = = = = = = = =  = = 
    '-===FF->

    '- NEW events for Attachments panel..


    '== ALL CONTROL EVENTS are passed to the CLASS for processing.-- 
    '== ALL CONTROL EVENTS are passed to the CLASS for processing.-- 
    '== ALL CONTROL EVENTS are passed to the CLASS for processing.-- 

    '--PREVIEW KeyPress..-
    '-- Catch  ctl-V- to Paste.

    Private Sub frmAttachments_KeyDown(sender As Object, _
                                  eventArgs As System.Windows.Forms.KeyEventArgs) _
                                      Handles MyBase.KeyDown
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.frmAttachments_KeyDown(sender, eventArgs)
        Exit Sub

    End Sub  '-- keydown-
    '= = = = = = = = = = = =
    '-===FF->

    '--=3119.1222=  PAST-FILE Context menu stuff--
    '--=3119.1222=  PAST-FILE Context menu stuff--

    '-- menu click-

    Public Sub mnuPasteFileName_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles mnuPasteFileName.Click
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.mnuPasteFileName_Click(eventSender, eventArgs)
        Exit Sub

    End Sub  '-mnuPasteFileName- click-
    '= = = = = = = = = = = = = = = =  = = = = = = =
    '-===FF->

    '-  MOUSE ACTION- txtNewFileName-

    '-- HANDLED HERE !!!
    '-- HANDLED HERE !!!
    '-- HANDLED HERE !!!

    Private Sub txtNewFileName_MouseUp(sender As Object, _
                                         ev As MouseEventArgs) Handles txtNewFileName.MouseUp

        '== Dim sFileFullpath, sFileTitle, sFormat As String
        If mbIsInitialising Then Exit Sub
        Dim data_object As Object = Clipboard.GetDataObject()

        ' If the right mouse button was clicked and released, 
        ' display the shortcut menu assigned to the txt.  
        If ev.Button = MouseButtons.Right Then
            '== If mbGetFileFromClipboard(sFileFullpath, sFileTitle, sFormat) Then
            If (data_object.GetDataPresent(DataFormats.FileDrop)) Then
                mnuPasteFileName.Enabled = True
            Else  '-nothing on clipboard-
                mnuPasteFileName.Enabled = False
            End If '-get
            '--show menu.. user must ckick.. 
            mContextMenuPasteFileName.Show(txtNewFileName, New Point(ev.X, ev.Y))
        End If
    End Sub  '-txtNewFile mouse up-
    '= = = = = = = = = == = = = = = = = = 
    '-===FF->

    '-- A d d Attachment--
    '-- A d d Attachment--

    Private Sub btnBrowse_Click(sender As Object, _
                                ev As EventArgs) Handles btnBrowse.Click

        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnBrowse_Click(sender, ev)
        Exit Sub

    End Sub  '-browse.-
    '= = = = = = = = = = = = 

    Private Sub txtNewComment_TextChanged(sender As Object, _
                                              ev As EventArgs) Handles txtNewComment.TextChanged
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.txtNewComment_TextChanged(sender, ev)
        Exit Sub

    End Sub  '--new comment-
    '= = = = = = = = = = == = = 

    '-- save new File to DB..

    Private Sub btnSaveAttachment_Click(sender As Object, ev As EventArgs) Handles btnSaveAttachment.Click
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnSaveAttachment_Click(sender, ev)
        Exit Sub


    End Sub  'save new-
    '= = = = = = = = = =
    '-===FF->

    '-- view current doc..
    '-- Doc has been selected from listView..

    Private Sub btnViewDoc_Click(sender As Object, ev As EventArgs) Handles btnViewDoc.Click
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnViewDoc_Click(sender, ev)
        Exit Sub

    End Sub  '--  view -
    '= = = = = = = = = = = = = = = = =

    '-- Delete -

    Private Sub btnDelete_Click(sender As Object, ev As EventArgs) Handles btnDelete.Click

        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnDelete_Click(sender, ev)
        Exit Sub

    End Sub  '--delete-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- list vew=  selection changed..--

    Private Sub lvwDocs_SelectedIndexChanged(sender As Object, _
                                              ev As EventArgs) Handles lvwDocs.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.lvwDocs_SelectedIndexChanged(sender, ev)
        Exit Sub

    End Sub '-SelectedIndexChanged-
    '= = = = = = = = = = = = = = = 

    '--listViewDocs_DblClick--

    Private Sub lvwDocs_DblClick(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles lvwDocs.DoubleClick

        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.lvwDocs_DblClick(eventSender, eventArgs)
        Exit Sub

    End Sub '--listView_dblClick--

    '= = = = = =  = = =
    '-===FF->

    '--= = = u n l o a d = = = = = = =
    'Private Sub frmJobMaint3_FormClosing(ByVal eventSender As System.Object, _
    '                                          ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
    '                                                     Handles Me.FormClosing
    '    Dim intCancel As Boolean = eventArgs.Cancel
    '    Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

    '    '--MsgBox "frmMaint UNload event..'"  '-debug--
    '    '--If Not gbclosingDown Then
    '    Select Case intMode
    '        Case System.Windows.Forms.CloseReason.WindowsShutDown, _
    '                          System.Windows.Forms.CloseReason.TaskManagerClosing, _
    '                                        System.Windows.Forms.CloseReason.FormOwnerClosing '==  NOT FOR vb.net.., vbFormCode
    '            intCancel = 0 '--let it go--
    '        Case System.Windows.Forms.CloseReason.UserClosing
    '            If Not (mbService Or mbDelivery) Then '--view only..-
    '                intCancel = 0 '--let it go--
    '            Else '--lock is held..
    '                If mbUpdateFinished Or mbProgramClosing Then  '=3311.819= -updated or me.close-
    '                    intCancel = 0 '--let it go--
    '                ElseIf mbService And (Not mbDataChanged) Then  '== And (mIntExitMenuSelectedIndex <= 0) Then
    '                    '-- Just cancel the transaction..-
    '                    Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
    '                    '-Call mbRollbackTransaction
    '                    intCancel = 0 '--let it go--
    '                ElseIf (mbService And (mbDataChanged)) Then  '== Or (mIntExitMenuSelectedIndex > 0)) Then
    '                    '-- QUERY cancel the transaction..-
    '                    '-- confirm if changes are to be trashed...--
    '                    If Not MessageBox.Show(Me, "DISCARD all changes and exit this job ?", _
    '                                           "JobMatix Update", MessageBoxButtons.YesNo, _
    '                                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
    '                        '===txtDiagnosis.SetFocus
    '                        intCancel = 1
    '                        '== MUST UPDATE arg..  ==Exit Sub '--was mistake..  keep form....
    '                    Else '--yes, scrap all changes.. --
    '                        msJobStatus = k_statusStarted
    '                        Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
    '                        intCancel = 0 '--let it go--
    '                    End If '--yes--
    '                ElseIf mbDelivery Then
    '                    If MessageBox.Show(Me, "ABANDON this delivery ?", _
    '                            "JobMatix Update", MessageBoxButtons.YesNo, _
    '                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
    '                        intCancel = 0 '--let it go--
    '                    Else  '--resume delivery..-
    '                        intCancel = 1 '--cant close yet-
    '                    End If '--ans.-
    '                Else '- ? -data changed.  ????? .--
    '                    intCancel = 1 '--cant close yet-- Must use cancel.-
    '                End If
    '            End If  '-service/deliv.-
    '        Case Else
    '            intCancel = 0 '--let it go--
    '    End Select '--mode--
    '    eventArgs.Cancel = intCancel
    'End Sub '--unload--
    '= = = = = = = = =

    '=== End of form ==
    '=== end of form. ==


End Class  '-form-  now -ucChildJobMaint-

'==  listView column comparartor..-

' Implements the manual sorting of items by columns.
Class ListViewMaintItemComparerUcChild
    Implements IComparer

    Private col As Integer

    Public Sub New()
        col = 0
    End Sub

    Public Sub New(ByVal column As Integer)
        col = column
    End Sub

    Public Function Compare(ByVal x As Object, _
                             ByVal y As Object) As Integer _
       Implements IComparer.Compare
        Return [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
    End Function
End Class

'=== totally the end ===
