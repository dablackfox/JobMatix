Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms.ListViewItem.ListViewSubItem
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections.Generic

Public Class frmJobMaint3
    Inherits System.Windows.Forms.Form

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
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== Fixes to Build 4221.0207  
    '==
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==
    '==      --  MAKE Copy of Form "frmJobMaint32" INTO USERCONTROL- ucChildJobMaint..
    '==      --  MAKE "frmJobMaint32" Form into "frmJobMaintBase" to hold USERCONTROL.
    '--            SO THAT we show it as a Child in a POS TAB..
    '==              From JobTracking we call frmJobMaintBase, which is container for the UserControl.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

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
    '= Private mColJobFields As Collection
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

    '== Private mIntExitMenuCompletedIndex As Integer = -1

    '==  Private mIntExitMenuSelectedIndex As Integer = -1
    Private msExitMenuSelectedName As String = ""

    Private mbStatusWasSuspOrQA As Boolean

    '-frm Base-
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '-save child form for resizing..
    Private mUcChild1 As ucChildJobMaint

    '= = = = = = = = = = = = = = = = = = =
    '-===FF->


    '--=3.2.1229=

    '=  Context menu for Pasting- attachment file name-
    '--  Popup menu for Right click on txt File name..-

    'Private mContextMenuPasteFileName As ContextMenu
    'Private WithEvents mnuPasteFileName As New MenuItem("Paste File")
    'Private WithEvents mnuPasteFileSep1 As New MenuItem("-")
    'Private WithEvents mnuPasteFileSep2 As New MenuItem("-")

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
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

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

    '-- end properties..-
    '-===FF->

    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 12-Aug-2020)

    '==-- Child uses Delegate to signal child closed to Main Parent.....

    Public Sub subChildReport(ByVal strChildName As String, _
                              ByVal strEvent As String, _
                               ByVal sText As String)

        '= If LCase(strEvent) = "formclosed" Then
        'For Each tabPageChild1 As TabPage In Me.TabControlMain.TabPages
        '    If (Not (tabPageChild1 Is Nothing)) AndAlso _
        '               (LCase(tabPageChild1.Name) = LCase(strChildName)) Then
        '        If LCase(strEvent) = "formclosed" Then
        '            '-Child form has closed..
        '            '-- find tab page and delete it..
        '            tabPageChild1.Dispose()
        '            Exit For
        '        End If  '--closed-
        '    ElseIf LCase(strEvent) = "updatetabtext" Then
        '        If (sText <> "") Then
        '            tabPageChild1.Text = sText
        '        End If
        '    End If  '-nothing-
        'Next  '-page-
        '= End If  '-closed-

    End Sub  '--ChildReport-
    '= = = = = = =  =  = =
    '==-- Child uses EVENT Delegate to signal child closed to Main Parent..
    '-- Child has this event definition..
    '--   Public Event PosChildClosing(ByVal strChildName As String)

    '-- Dispose of Tab Page and Child Control..
    '-- Dispose of Tab Page and Child..

    Public Sub posChild_Closing(ByVal strChildName As String)

 
        RemoveHandler mUcChild1.PosChildClosing, AddressOf posChild_Closing

        '= MsgBox("Child is closing- so are we..", MsgBoxStyle.Information)  '--testing..-

        Me.Hide()

    End Sub  '- posChild_Closing-
    '= = = = = = = = = = = === = = =

    '==--Child uses Delegate to signal child STAFF SIGNED ON to Main Parent.....

    Public Sub subChildSignedOn(ByVal intStaffid As Integer, _
                                ByVal strStaffBarcode As String, _
                                 ByVal strStaffName As String)
        '--save as main signon-
        '= mIntStaff_id = intStaffid
        '= msStaffBarcode = strStaffBarcode
        msStaffName = strStaffName

    End Sub '' child signed on..-
    '= = = = = = = == = = = = = = = = = = = =
    '-===FF->

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


    '-- ORIGINAL -  L o a d---
    '-- ORIGINAL -  L o a d---

    '-- 3059.1=Private Sub mbOriginal_frmJobMaint3_Load()

    Private Sub frmJobMaint3_Load(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim ix As Short
        Dim s1, s2, sName As String
        Dim lngError As Integer
        '= txtNomTech.Text = ""
        '== msPriority = ""
        '== cmdSuspend.Enabled = False
        '== cmdQA.Enabled = False

        msLocalSettingsPath = gsLocalJobsSettingsPath()
        '=3311.225=
        mLocalSettings1 = New clsLocalSettings(msLocalSettingsPath)

        ''--Pic2 is for printing..--
        'Picture1.Visible = False
        'Picture2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(SSTab1.Top) + 600) '--hide pic2--
        'Picture2.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(SSTab1.Left) + 600)
        Picture2.Visible = False
        'PictureExtraPrint.Visible = False

        msOriginalNominTech = ""

        '=3431.0513=
        Call CenterForm(Me)

        '== 3067.0 ==
        s1 = gsGetHelpFileName()
        If (s1 <> "") Then
            HelpProvider1.HelpNamespace = s1
            HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
            HelpProvider1.SetHelpKeyword(Me, "JT3-JobMaint.htm")
        End If

        Me.KeyPreview = True  '-To catch Ctl-V (Pasting File)..

        '--=3.2.1229=  29Dec2015=

        '== 3203.107==
        '--  Load printer Combos..
        '-- Combo for Colour printer..
        '=3203.107= get printers.
        Dim intDefaultPrinterIndex As Integer
        Dim colPrinters As Collection

 
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
        If (mClsSystemInfo.item("LABOURMINCHARGE") <> "") Then
            mCurLabourMinCharge = CDec(mClsSystemInfo.item("LABOURMINCHARGE"))
        End If

        '=3203.211=  -------
        mbEnforceMinCharge = True   '-default-
        If (mClsSystemInfo.item("ENFORCE_MINIMUM_CHARGE") <> "") Then
            s1 = UCase(VB.Left(mClsSystemInfo.item("ENFORCE_MINIMUM_CHARGE"), 1))
            mbEnforceMinCharge = IIf((s1 = "Y"), True, False)
            '= labMinCharge.Visible = mbEnforceMinCharge
        End If
        '-------------------
 
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

        grpBoxMain.Text = ""
        '- done system info..

    End Sub '-Original-load--
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- EX:  F o r m  A c t i v a t e --
    '-- EX:   F o r m  A c t i v a t e --

    Private Sub frmJobmaint3_Activated(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If mbActive Then Exit Sub '--re-entered after every child form..--
        mbActive = True

    End Sub  '-Activated-
    '= = = = = = = = = ==  == =

    '= 3411.0428=
    '-- S h o wn   may be more stable-

    Private Sub frmJobMaint3_Shown(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles MyBase.Shown


        Dim s1 As String
        Dim s2, s3, s4 As String
        Dim iPos, idxGrid As Integer
        Dim L1, lngError As Integer
        Dim k, i, j, ix As Integer
        '=Dim item1 As System.Windows.Forms.ListViewItem
        '===Dim staffIndex As Long
        Dim sDay As String
        Dim sSql As String
        Dim colFld As Collection
        '== Dim fld1 As ADODB.Field
        Dim sShortDate As String
        'Dim sName As String
        'Dim rs1 As DataTable  '= ADODB.Recordset
        'Dim bNotCompleted, bAutoAssignJob As Boolean

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
            Me.Close() '==Me.Hide
            Exit Sub
        End If
        If mlJobId = -1 Then
            MessageBox.Show(Me, "Job No. must be supplied..", "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close() '==Me.Hide
            Exit Sub
        End If
        Me.Text = "JobNo: " & VB6.Format(mlJobId, "##00") & "."

        sShortDate = VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy")
        '==SHRINK== LabToday.Text = sShortDate

        'LabTicket.Text = vbCrLf & VB6.Format(mlJobId, " 000")
        'System.Windows.Forms.Application.DoEvents()
        mCurGST = 0
        If IsNumeric(msGSTPercentage) Then mCurGST = CDec(msGSTPercentage)

          '====  s1 = IIf((Trim(txtCustName.Text) <> ","), txtCustName.Text, "")
        s2 = IIf(((msCustomerCompany <> "--") And (LCase(msCustomerCompany) <> "n/a")), msCustomerCompany, "")
        If (s1 = "") Then '-- no name-
            msCustomerPrint = s2 '--company only..--
        ElseIf (s2 <> "") Then
            msCustomerPrint = s1 & " (" & s2 & ")" '--join on company..--
        Else
            msCustomerPrint = s1 '--name only..
        End If
        '= txtCustomer.Text = msCustomerPrint & " [" & msCustomerBarcode & "]"  '==3059.1=

        If mCurLabourHourlyRate = 0 Then mCurLabourHourlyRate = 100.0# '--default..-

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
        '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
        '==   Target-New-Build 4262 -- (Started 12-Aug-2020)
        '--  Load Child UserControl..

        Dim ucChild1 As ucChildJobMaint

        '- make as static first, first, for resizing.
        mUcChild1 = New ucChildJobMaint

        '-save- for re-size..
        ucChild1 = mUcChild1

        ucChild1.parentForm = Me

        ucChild1.JobNo = mlJobId

        ucChild1.connectionSql = mCnnJobs
        '==frmJobMaint2.connectionJet = mCnnJet
        ucChild1.retailHost = mRetailHost1

        ucChild1.dbInfoSql = mColSqlDBInfo
        '== frmJobMaint2.dbInfoJet = mColJetDBInfo
        ucChild1.ServiceUpdate = False '-- request service type--
        ucChild1.DeliveryUpdate = False '-- delivery type--
        '== frmJobMaint3A.NotifyUpdate = False

        ucChild1.StaffId = mlStaffId
        ucChild1.StaffName = msStaffName
        ucChild1.StaffBarcode = msStaffBarcode
        ucChild1.UserLogo = Picture2.Image '--pass logo..-

        ucChild1.CustomerDetails = mColRMCustomerDetails

        If mbService Then
            ucChild1.ServiceUpdate = True '-- request service type--
            '==            ElseIf bNotify Then
            '==               frmJobMaint3A.NotifyUpdate = True
        ElseIf mbDelivery Then
            ucChild1.DeliveryUpdate = True '-- delivery type--
        End If
        ucChild1.HostIsJobTracking = True   '--Allows ESC to activate Cancel button.

        ucChild1.Name = "ucChildJobMaint1" '= strFormClassName & "_" & CStr(mIntChildCount)
        ucChild1.Text = ucChild1.Name
        ucChild1.Dock = DockStyle.Fill
        ucChild1.AutoSize = False
        '=ucChild1.Dock = DockStyle.Fill
        ucChild1.AutoSize = False

        ucChild1.Parent = grpBoxMain
        grpBoxMain.Controls.Add(ucChild1)

        ucChild1.delReport = AddressOf Me.subChildReport

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler mUcChild1.PosChildClosing, AddressOf posChild_Closing

        mbStartupCompleted = True
        mbDataChanged = False '--was set on by our loading of data..

        colFld = Nothing
        mbIsInitialising = False
        Exit Sub

activate_error:
        L1 = Err().Number
        MessageBox.Show(Me, "Runtime Error in JobMaint Form Shown sub.." & vbCrLf & _
                              "Error is " & L1 & " = " & ErrorToString(L1), _
                              "JobMatix Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Me.Close()
        '== End

    End Sub '--Shown--
    '= = = = = = =  = = =
    '-===FF->


 
    '-- 1-second Timer for progress bar..--

    Private Sub Timer1_Tick(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles Timer1.Tick

        'If mlProgress >= 0 Then '--active--
        '    mlProgress = mlProgress + 1
        '    '=== ProgressBar1.Value = mlProgress
        'End If

    End Sub '-- timer..--
    '= = = = = =  = =  = =
    '= = = = = =  = = =
    '-===FF->

    '--= = = u n l o a d = = = = = = =
    '-- DELEGATE for CLOSING Child..
    '= Public Delegate Sub SubFormCloseRequest()
    Public Delegate Function SubFormCloseRequest() As Boolean

    '-- This is instantiated below.
    Public delCloseRequest As SubFormCloseRequest '--    = AddressOf frmPosMainMdi.subChildReport


    Private Sub frmJobMaint3_FormClosing(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                         Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim bCanCloseOk As Boolean = False

        '--MsgBox "frmMaint UNload event..'"  '-debug--
        '--If Not gbclosingDown Then
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                              System.Windows.Forms.CloseReason.TaskManagerClosing, _
                                            System.Windows.Forms.CloseReason.FormOwnerClosing '==  NOT FOR vb.net.., vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing

                '= send close cmd..
                delCloseRequest = New SubFormCloseRequest(AddressOf mUcChild1.SubFormCloseRequest)
                'Call delCloseRequest()
                '= If mbCloseSelectedChild(tabPage1, sName) Then
                bCanCloseOk = delCloseRequest.Invoke
                '= MsgBox("bCanCloseOk is: " & bCanCloseOk, MsgBoxStyle.Information)
                If bCanCloseOk Then
                    '-remove close event handler.
                    RemoveHandler mUcChild1.PosChildClosing, AddressOf posChild_Closing
                    intCancel = 0 '--let it go--
                Else
                    '-can't close
                    intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                End If


                'If Not (mbService Or mbDelivery) Then '--view only..-
                '    intCancel = 0 '--let it go--
                'Else '--lock is held..
                '    If mbUpdateFinished Or mbProgramClosing Then  '=3311.819= -updated or me.close-
                '        intCancel = 0 '--let it go--
                '    ElseIf mbService And (Not mbDataChanged) Then  '== And (mIntExitMenuSelectedIndex <= 0) Then
                '        '-- Just cancel the transaction..-
                '        '=    Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
                '        '-Call mbRollbackTransaction
                '        intCancel = 0 '--let it go--
                '    ElseIf (mbService And (mbDataChanged)) Then  '== Or (mIntExitMenuSelectedIndex > 0)) Then
                '        '-- QUERY cancel the transaction..-
                '        '-- confirm if changes are to be trashed...--
                '        If Not MessageBox.Show(Me, "DISCARD all changes and exit this job ?", _
                '                               "JobMatix Update", MessageBoxButtons.YesNo, _
                '                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                '            '===txtDiagnosis.SetFocus
                '            intCancel = 1
                '            '== MUST UPDATE arg..  ==Exit Sub '--was mistake..  keep form....
                '        Else '--yes, scrap all changes.. --
                '            msJobStatus = k_statusStarted
                '            '-== Call mbReleaseJob(msJobOriginalStatus) '-- Reset status to point of entry--
                '            intCancel = 0 '--let it go--
                '        End If '--yes--
                '    ElseIf mbDelivery Then
                '        If MessageBox.Show(Me, "ABANDON this delivery ?", _
                '                "JobMatix Update", MessageBoxButtons.YesNo, _
                '                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = MsgBoxResult.Yes Then
                '            intCancel = 0 '--let it go--
                '        Else  '--resume delivery..-
                '            intCancel = 1 '--cant close yet-
                '        End If '--ans.-
                '    Else '- ? -data changed.  ????? .--
                '        intCancel = 1 '--cant close yet-- Must use cancel.-
                '    End If
                'End If  '-service/deliv.-
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--unload--
    '= = = = = = = = =

    '=== End of form ==
    '=== end of form. ==


End Class  '-form-

'==  listView column comparartor..-

' Implements the manual sorting of items by columns.
Class ListViewMaintItemComparer
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
