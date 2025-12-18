Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class frmNewJob32
    Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. 
    '= = = = = = = = = = = = =

    '- -JobTracking-  New Job Form.---
    '= = =  = = = = = = = = = = = = = =

    '--- grh ==07-Jun-2010= Use ListView and GoodsEdit form form for Goods Entry..=
    '--- grh ==30-Jun-2010= Evaluation Text on service agreement.-
    '--- grh ==15-Jul-2010= Print State and Postcode on Receipt...-
    '--- grh ==11Aug-2010= Print Password even if no username.
    '-----    Also:  Recovery Disk now "extra" (not $15:00).-
    '--- grh ==21Aug-2010= Print $Extra (instead of $15.00) for Data Disk..
    '--- grh ==19-Sep-2010= "Printed OK?" questions default to YES....

    '--- grh ==06-Oct-2010= =Rev: 2478== Notify has its own form...
    '=== grh ==13-Oct-2010= =Rev: 2480= '--Refresh Customer Details..-
    '===    --------------     Drop Progress Bar..
    '---- JobMatix ---
    '--- grh ==15-Nov-2010= =Rev: 2787== Notify needs staffName...
    '--- grh ==25-Nov-2010= =Rev: 2788== Print Job Labels......
    '--- grh ==29-Nov-2010= =Rev: 2788== Update TechStaffName with Amend/Cancel updates...
    '--- grh ==01-Dec-2010= =Rev: 2788== Update GoodsInCare all done in form GoodsEdit..
    '--- grh ==07-Dec-2010= =Rev: 2790== All stuff now on one page (no Tabs..)..
    '--- grh ==18-Dec-2010= =Rev: 2790== "check-in" is special type of Amend...
    '--- grh ==25-Dec-2010= =Rev: 2791== Cancel can exit immediately if no dataChanged.-.
    '--- grh ==07-Jan-2011= =Rev: 2792== Remember priority was clicked...-.
    '--- grh ==14-Jan-2011= =Rev: 2796== NominTech validate fixed (was allowing unchecked alpha entry.)..-
    '--- grh ==18-Jan-2011= =Rev: 2798== NominTech validate fixed (was converting to LONG !!)..-
    '--- grh ==20-Jan-2011= =Rev: 2800== NominTech validate..  separate text boxes for Barcode/name.-
    '--- grh ==31-Jan-2011= =Rev: 2803== Don't enable printall cmd if no job...-
    '-- =Rev: 2804==
    '--- grh ==30-Mar-2011= =Rev: 2804== New Job== Check if cust. is waitlisted..-mbGetCustomerWaitList==
    '-- =Rev: 2806==
    '--- grh ==04-Apr-2011= =Rev: 2804== New Job== Print "n/a" instead of ZERO labour rate..==
    '- =V2.2.2902 ==
    '--- grh ==17-May-2011= =Rev: 2902== New Job== Print ONE ONLY label after first printing pass.==
    '- =V2.2.2914 ==
    '--- grh ==30-Jul-2011= =Rev: 2914== PRINT New Job== use printDocs class.==
    '-----      AND-- New Option "Quotation Required..--
    '--- grh ==06Aug-2011= =Rev: 2918== Full Terms text now an input property..==
    '========

    '--- grh ==06-Oct-2011= =Rev: 3010== NEW VERSION- MultiRetailHost....==
    '--- grh ==01-Nov-2011= = All Printing sent to clsPrtDocs --....==
    '--- grh ==19-Nov-2011= = FOr Upgrade to vb.net  -  DROP all GOSUB's....==

    '--- grh ==24-Nov-2011= = UPGRADED VERSION....==
    '--- grh ==09-Jan-2012= = Fixes...==
    '--- grh ==05/06-Mar-2012= = Fixes...=Also SET TransparencyKey REMOVED=
    '===     and delete main menu strip..----
    '===     AND  re-position GoodsEdit form to avoid flicker when it exits.. 
    '==     NB: !!!
    '--          If we keep the child form positioned to side of the calling form.
    '---         and not invalidating the centre of the caller form..  then the --
    '---         calling form doesn't seem to need total re-painiting..
    '== grh- 3031.7 ==
    '==     Make BOLD the  Receipt reference no..
    '===    AND..  Replace all 'Today'  with 'Cdate(datetime.today)'..--
    '==
    '== grh- 3037.0 ==03-Apr-2012=
    '==     Allow cancelling Job if Amending (WAS if customer_id valid..)...
    '==
    '== grh- 3041.1 ==11-Apr-2012=
    '==     Print ServiceChargesInfoText if supplied....
    '==
    '== grh- 3043.3 ==22-Apr-2012=
    '--     DROP offer to change printer...
    '==
    '== grh- 3049.3 ==03-May-2012=
    '--    Activate..  Start Focus on CheckGoods....
    '==
    '== grh- 3053.0 ==17-May-2012=
    '--  Tidy up Form..
    '--
    '== grh- 3053.1 ==21-May-2012=
    '==  Amend..  RE-instate enable frameGoods !!!  --
    '==
    '== grh- 3057.1 ==29-May-2012=
    '==    GoodsInCare.. show/select previous goods..  --
    '==
    '== grh '== 3067 ==
    '==    >> Add help File..  Revamp NewJob Symptoms..=" 
    '==
    '== = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '== grh '== 3072/3073 ++ 04-Feb-2013 ==
    '==    >> Add frmGoodsInCare..  Revamp Goods area..=" 
    '==    >> Make ExtrasInCare checkBoxes available for AMEND...=" 
    '==    >> Cust. Receipt- more space after EVALUATION notice..
    '==
    '== grh '== 3077 ++ 18-May-2013 ==
    '==     Move "Return" and "Priority" combo to end of 2nd Tab..--
    '==
    '== grh = 3083 ++ 26-Feb-2014 ==
    '==      >> Add "DatePromised".--
    '==      >> Now spread over 3 panels..--
    '==      >> New Job Type: ON-SITE..-
    '==
    '== grh = 3083.401 == 01-Apr-2014 ==
    '==        >>  fix Date day overflow for NewJob date/Time promised..
    '==        >>  Fix Tab indexes..
    '==
    '--== 3083.402= Previous Jobs (Goods).. filter out if was ON-SITE Job--
    '==
    '--== 3083.619= Returned Job-  Move Checkbox to 1st page..  Save Original JobNo..--
    '==              >> Save Prev. Job No in GoodsOther..
    '==
    '--== 3083.717= UpDown Control for No of Labels to print....--
    '==        _chkPrtDocs_2 
    '==
    '== = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '== 
    '==  grh. JobMatix 3.1.3101 ---  18-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider is needed for Jet OleDb driver).
    '== 
    '==  grh. JobMatix 3.1.3101 ---  30-Sep-2014 ===
    '==     For Updating (Amend) just use Optimistic concurrency..
    '==      ie. Check for DateUpdate changed in UPDATE command..
    '== 
    '==  grh. JobMatix 3.1.3107 ---  18-May-2015 ===
    '==       >> ProblemLong column now 4000.
    '==
    '==  grh. JobMatix 3.1.3107.707 ---  07-Jul-2015 ===
    '==       >> Amending ONSITE.. Fix onsiteJob type recognition..
    '==
    '==  grh. JobMatix 3.1.3107.727 ---  27-Jul-2015 ===
    '==       >> Add ddd to date promised.
    '==
    '==  grh. JobMatix 3.1.3107.904 ---  04-Sep-2015 ===
    '==       >> cmdFinish was forcing "H" priority for ON-SITE jobs..
    '==       >> AMEND was not recognising ON-SITE jobs (wrong logic in Activated)..
    '==       >>  Print Sevice Agreemnt- Show ONSITE ticket box colour-
    '==
    '==     >> 3107.1213-  13-Dec-2015=
    '==           Fix to Finish to explain mandatory UserName/password partnership...
    '==
    '==
    '==  NEW VERSION 3203.102-  02-Jan-2015=
    '==   With Attachments Table and CLASS plus frmAttachments -
    '==    >>  New JOB can have Item Image and it can print..
    '==
    '==    >> 3203.106: ServiceAgreement.. Added Combo to choose printer (cf RA's)....
    '==    >> 07Jan2016-  Form now has Combos to choose (set) all three PRINTER targets.
    '==    >> 3203.116: Add Business Email to SA and Docket.....
    '==    >> 3203.117: Add "System Under Warranty" (opposed to JobReturned)..
    '==
    '==  >> 23Jan2016-  AcceptJob/BookJob decision now made in New Job Form..
    '==                   (If New Job then show option- AcceptJob/BookJob..)
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW VERSION 3.3.3311-  25-Feb-2016=
    '==
    '==    >> All systemInfo work now via Class clsSystemInfo ..
    '==    >> All localSettings work now via Class clsLocalSettings..
    '==
    '==    >> 3311.228= Staff Lookup to Browse for Tech...
    '==
    '==  grh 3.3.3311.331-  31-Mar-2016=
    '==         >>  priority Labour prices and descriptions 
    '==                   now from MYOB/POS via stock Barcodes.
    '==         >>  3311.410=  Uncheck print options if Booking-in checked..  
    '==         >>  3311.422=  Amending..Allow changing password..  
    '==
    '==  -- 3327.0119- 19-Jan-2017-
    '==         >>-- Fix to Add static var lock on cmdFinish to stop re-entry-..-- 
    '==
    '==   v3.4.3403.0604 -- 04-Jun-2017= x-
    '==    --  ON-SITE Jobs can post/Update appointment to Exchange Calendar if defined....
    '==
    '==   V3.4.3431.0503-
    '==        -- New Job/Amend- Send Exchange calendar update to BG Exchange20 Queue.. 
    '==                 (NOT directly to Exchange)...
    '==             And on main Form-  Add BG Exchange Update thread to process Updates..
    '=         -- 3431.0503=  Move activated stuff to Shown event..
    '==        --3431.0527- set Limit on txtProblem from problemLong.
    '==        --3431.0527- ADD PREVIOUS Job info to txtProblem if needed. (to PRINT on SA.).
    '==
    '==    >> 3431.0622- 23-June-2018=
    '==       --  Exchange BG task- Detect invalid XML file data eg. reserved chars (eg Amoersand etc.)...
    '==       --  NewJob/Amend- Fix (cleanup) XML file data for reserved chars (eg Ampersand etc.)...
    '==
    '==    >> 3501.0625- 25-June-2018=
    '==       -- TESTING porting of- Fix (cleanup) XML file data for reserved chars (eg Ampersand etc.)...
    '==
    '== -- Updated 3501.0814  14August2018=  
    '==     -- Fix clsJMxPOS31 to separate out function CreatePosTables into clsJMxCreatePOS...
    '==     -- Amending Job: Make entry into ServiceNotes if GoodsIn Care changed. ...
    '== 
    '== = = = = = = = = = =
    '==
    '== -- Updated 3501.1105  05-Nov-2018=  
    '==     -- New Job Form (ON-SITE Jobs.)  Add numericUpDown control to select Job Duration in hours. 
    '==            ALSO, then add this value as Meeting Duration in Exchange Calendar update.
    '==               ie.. ExchangeBG Worker will pass an extra parameter (Duration) to the Exchange module.
    '==     -- New Job Form-  Remind user to ensure Charger/PwerSupply included with Goods if needed..
    '==
    '==
    '== -- Updated 3501.1223  23-Dec-2018=  
    '==     --  Fix ONSTTE Duration Label on New Job Form..
    '==     --  Add Date promised to ON-SITE Customer Docket..
    '==
    '==
    '==    Updated- 4201.1007-  Started 06-October-2019= 
    '==     --  Fix to Amend start-up to Fix problem (AGAIN) about
    '==              Extra 'Slashes' that appear in User Passwords in Job Record.
    '==
    '==
    '==  NEW BUILD- 4219 VERSION
    '==    Updated- 4219.1121 21-Nov-2019= 
    '==      --  MAKE Forms PUBLIC- NewJobForm and Maint Form-  "frmNewJob32" and "frmJobMaint32"
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '== const k_version As String = "NewJobForm32-V3.2.3203.116==16Jan2015=5:47pm="

    Const k_statusWaitListed As String = "05-WaitListed"
    Const k_statusCreated As String = "10-Created"
    Const k_statusStarted As String = "30-Started"
    Const k_statusCancelled As String = "97-Cancelled"

    '-- Tabs--
    Const k_SSTAB_CUSTOMER As Short = 0
    Const k_SSTAB_GOODS As Short = 1
    '========Const k_SSTAB_USERS = 2
    Const k_SSTAB_PROBLEM As Short = 2

    '====Const k_maxGoodsInCare = 3   '-- max index no of TXT items..
    Const k_maxExtrasInCare As Short = 4 '-- no of checkboxes--
    Const k_maxUserNames As Short = 3 '--  no of usernames possible--
    '---Const k_maxProblems = 10       '-- no of checkboxes--
    Const k_TABPRINTPRIORITY As Short = 26
    Const k_TABPRINTBRAND As Short = 34
    Const k_TABPRINTMODEL As Short = 52
    '= = = = = = = = = = = = =  =
    Const k_PRINT_AGREEMENT As Short = 0
    Const k_PRINT_RECEIPT As Short = 1
    Const k_PRINT_LABEL As Short = 2

    Const K_GOODS_ONSITEJOB As String = "ON-SITE JOB;"

    '= = = = = = = = = = = =

    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer

    Private msAppPath As String
    Private mbActive As Boolean = False  '-- stops activate being re-entered..-
    Private mbFormStarting As Boolean = True  '-- for premature firing of events....-

    Private mbAmending As Boolean = False  '-- amend existing  job...-
    Private mbIsBooking As Boolean = False '--NOW DECIDED HERE..  Form called up by BOOKING command..--
    Private mbIsCheckIn As Boolean = False '-- Form called up by CHECK-In command..--
    Private mbIsOnSiteJob As Boolean = False '-- Form called up by New OnSite Job command..--

    Private mCnnJobs As OleDbConnection  '== ADODB.Connection '--SQL jobs connection --
    Private mTransactionUpdateJob As OleDbTransaction
    Private mTransactionInsertJob As OleDbTransaction

    '== Private mCnnJet  As ADODB.connection    '--  Retail Manager Jet connection..--
    Private mRetailHost1 As _clsRetailHost

    '== Private mColJetDBInfo As Collection
    Private mColSqlDBInfo As Collection

    Private msLocalSettingsPath As String = ""
    Private mLocalSettings1 As clsLocalSettings  '==3311.225=

    '-- new job data --
    '-- new job data --
    Private mlStaffId As Integer = -1 '--save staff-id--
    Private msStaffName As String = ""

    Private mlJobId As Integer = -1 '--AMEND JOBno, OR (Created) Identity retrieved..-
    Private mColJobFields As Collection '--Retrieved Existing Job Record to AMEND..--

    Private mlCustomerId As Integer = -1 '--save cust-id--
    Private msCustomerBarcode As String = "" '--main cust. identifier..-
    Private msCustomerCompany As String = ""
    Private msCustomerName As String = ""
    '==3083=
    Private msCustomerAddress As String = ""

    Private msCustomerPhone As String = ""
    Private msCustomerMobile As String = ""
    '== 3057.1 ==
    Private mColCustomerJobsGoods As Collection
    Private mColCustomerPrevGoodsFlat As Collection  '--cooresponds to combo box..-

    Private msPriority As String = ""
    Private msPriorityText As String = ""
    Private mlPriorityColour As Integer
    '=3203.119=
    Private mColorPriorityFG As Color = Color.Black

    '--Private maiGoods() As Integer   '-- checked states of Goods list box..--
    Private mlGoodsCount As Integer = -1 '--count of goods in txt array..-
    Private msGoodsInCare As String = ""
    '=3501.0814=  Remember orig. in case amended..
    Private msOriginalGoodsInCare As String = ""
    Private mbGoodsOtherChanged As Boolean = False

    Private mColInitialGoodsInCare As Collection

    Private msPrintGoodsInCare As String = ""
    Private miGoodsIndex As Short
    '--Private msOtherGoods As String
    '--Private mbBrandClicked As Boolean   '--otherwise txt in textbox..--

    Private msExtrasInCare As String = ""

    '===  REPLACED by PROBLEM-SHORT--   Private msProblems As String
    Private msProblemShort As String = ""
    Private msProblemDetails As String = ""
    '== Private maiSymptoms() As Short '-- checked states of symptoms list box..--
    '--  (Now upgraded to CheckListBox.. )

    Private msSymptoms As String = "" '-- final accum..-

    Private mbBrowsing As Boolean = False
    Private mbCreated As Boolean = False '--record has been created..--

    '--  printers--
    '== Private mPrtColour As Printer
    Private msColourPrinterName As String = ""
    Private msDefaultPrinterName As String = ""

    '== Private mPrtReceipt As Printer
    Private msReceiptPrinterName As String = ""

    '== Private mPrtLabel As Printer
    Private msLabelPrinterName As String = ""
    '==Private mlProgress As Long  '--progress value..-
    '= = = = =  = =  = = = = =

    Private mbLicenceOK As Boolean = False
    Private msABN As String = ""
    Private msBusinessName As String
    Private msBusinessShortName As String
    Private msBusinessAddress1 As String
    Private msBusinessAddress2 As String
    Private msBusinessState As String
    Private msBusinessPostCode As String
    Private msBusinessPhone As String
    Private msBusinessEmail As String = ""

    '== Private msNewJobDocketFootnote As String = ""
    Private msNewJobDocketFootnote As String = "We will notify you when job is ready..  Payable on collection.."
    '= = = = = = = = = = = = = =

    Private mCurLabourHourlyRate As Decimal = 1
    '==3311.331= Private mCurLabourHourlyRatePriority1 As Decimal = 1
    '==3311.331= Private mCurLabourHourlyRatePriority2 As Decimal = 1
    '==3311.331= Private mCurLabourHourlyRatePriority3 As Decimal = 1

    Private mCurLabourMinCharge As Decimal = 1
    Private mCurNotificationCostLimit As Decimal = 1
    '= = = = = = = = = = = = =
    '===Private msDescriptionPriority1 As String
    '===Private msDescriptionPriority2 As String
    '===Private msDescriptionPriority3 As String
    Private mColPriorities As Collection

    '= = = = = = = = = = = = =
    '-- Label printing..--
    Private msLabelBarcodeFontName As String = ""
    Private mlLabelBarcodeFontSize As Integer = 9
    '-- Job Label Dimensions..-
    '===Private mcurJobLabelPrintDepth As Currency  '-- label actual depth mm.. (max 1 decimal)--
    '===Private mcurJobLabelGapDepth As Currency   '-- label GAP depth mm.. (max 1 decimal.)--

    '= = = = = = = = = = = = = = = =  =
    Private mlCmdSymptomLeft As Integer '-- save orig. pos.--
    Private mlCmdSymptomTop As Integer '-- save orig. pos.--
    '= = = = = = = = = = = = = = = = =

    Private msJobOriginalStatus As String = ""
    '--original Job Record dateTime-
    Private mDateOriginalTimeStamp As DateTime

    '==  NEW FOR GOODS..==
    '==  NEW FOR GOODS..==
    Private mColGoodsTypes As Collection
    Private mColBrands As Collection

    Private mbCustomerRefreshed As Boolean = False
    Private mbDataChanged As Boolean = False
    '=3519.0414-
    Private mbInvalidPasswordDataOnStartup As Boolean = False

    Private mbLabelsPrinted As Boolean = False

    Private msTermsText As String = ""
    Private msServiceChargesInfoText As String = ""   '--3041.1==

    '==3083== OnSite--
    Private msDatePromised As String = ""
    Private msTimepromised As String = ""
    '=3403.604=  SAVE Original date promised.
    Private mDatePromisedOriginal As Date = DateTime.MinValue

    '==3083.717= Labels
    Private mbUserChangedNumberOfLabels As Boolean = False
    Private mbNumUpDownLabels_Updating As Boolean = False

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Private mClsSystemInfo As clsSystemInfo

    Private mIntPreviousJobNo As Integer = -1
    '-- drawing experiments..-
    '== Private mTabArea As Rectangle
    '== Private mTabTextArea As RectangleF


    '==  A t t a c h m e n t s -
    '==  A t t a c h m e n t s -
    '==  A t t a c h m e n t s -

    Private mClsAttachments1 As clsAttachments

    '-- NEW File to be Attached (For NEW RA PIC)..

    Private mByteNewFile As Byte()
    Private msNewFileFullPath As String = ""
    Private msNewFileFileTitle As String = ""
    Private msNewFileFormat As String = ""

    Private mIntDoc_id As Integer = -1    '--Current Main attachment image if any..
    Private mbMainImageToBeUpdated As Boolean = False

    '=3431.0505--EXCHANGE Calendar updates-
    '--  This is passed back to Main Form if Calendar updated was queued.
    Private msExchangeCalendarUpdateXmlFileName As String = ""

    '=3431.0527=
    Private mIntMaxTxtProblem As Integer = 4000

    '=3501.1105=
    Private mbByPassLaptopChargerCheck As Boolean = False
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    WriteOnly Property connectionSql() As OleDbConnection  '==  ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =

    '===  NOW MUST GO VIA RetailHost CLASS..--
    '===  NOW MUST GO VIA RetailHost CLASS..--

    '== Property Let connectionJet(cnn1 As ADODB.connection)

    '==   Set mCnnJet = cnn1

    '== End Property  '--cnn jet..--
    '= = = = = = =  = = = = =

    '===  NOW MUST GO VIA RetailHost CLASS..--
    WriteOnly Property retailHost() As _clsRetailHost
        Set(ByVal Value As _clsRetailHost)

            mRetailHost1 = Value
        End Set
    End Property '-host-
    '= = = = = = = = = = =

    WriteOnly Property dbInfoSql() As Collection
        Set(ByVal Value As Collection)

            mColSqlDBInfo = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

    '== Property Let dbInfoJet(dbinfo As Collection)

    '==   Set mColJetDBInfo = dbinfo

    '== End Property  '--info jet..--
    '= = = = = = = = = = = =

    '--  Customer Details for New Job..--

    WriteOnly Property CustomerId() As Integer
        Set(ByVal Value As Integer)

            mlCustomerId = Value
        End Set
    End Property '--CustomerId-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerBarcode() As String
        Set(ByVal Value As String)

            msCustomerBarcode = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerCompany() As String
        Set(ByVal Value As String)

            msCustomerCompany = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerName() As String
        Set(ByVal Value As String)
            msCustomerName = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerPhone() As String
        Set(ByVal Value As String)

            msCustomerPhone = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =
    WriteOnly Property CustomerMobile() As String
        Set(ByVal Value As String)

            msCustomerMobile = Value
        End Set
    End Property '--Customerbarcode-
    '= = = = = = = = = = = = = =

    WriteOnly Property CustomerJobsGoods() As Collection
        Set(ByVal Value As Collection)

            mColCustomerJobsGoods = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =


    '--  printers..--
    '--  printers..--

    '== 3203.107=  Printers are NOW discovered here within..

    '== 3203.107=WriteOnly Property ColourPrinterName() As String
    '== 3203.107=   Set(ByVal Value As String)
    '== 3203.107=        msColourPrinterName = Value
    '== 3203.107=    End Set
    '== 3203.107=End Property '--abn.--
    '= = = = = = = =  = = =
    '== 3203.107=WriteOnly Property ReceiptPrinterName() As String
    '== 3203.107=    Set(ByVal Value As String)
    '== 3203.107=         msReceiptPrinterName = Value
    '== 3203.107=    End Set
    '== 3203.107=End Property '--abn.--
    '= = = = = = = =  = = =
    '== 3203.107=WriteOnly Property LabelPrinterName() As String
    '== 3203.107=    Set(ByVal Value As String)
    '== 3203.107=       msLabelPrinterName = Value
    '== 3203.107=   End Set
    '== 3203.107=End Property '--abn.--
    '= = = = = = = =  = = =

    '--licensing..--

    WriteOnly Property LicenceOK() As Boolean
        Set(ByVal Value As Boolean)
            mbLicenceOK = Value
        End Set
    End Property '--licence..-
    '= = = = = = = = = = = =

    WriteOnly Property LabourHourlyRate() As Decimal
        Set(ByVal Value As Decimal)
            mCurLabourHourlyRate = Value
        End Set
    End Property '--rate.--
    '= = = = = = = =  = = =

    '-- priority rates..-
    '-- priority rates..-

    '==3311.331= WriteOnly Property LabourHourlyRatePriority1() As Decimal
    '==3311.331= Set(ByVal Value As Decimal)
    '==3311.331=    mCurLabourHourlyRatePriority1 = Value
    '==3311.331= End Set
    '==3311.331= End Property '--rate.--
    '= = = = = = = =  = = =

    '==3311.331= WriteOnly Property LabourHourlyRatePriority2() As Decimal
    '==3311.331=     Set(ByVal Value As Decimal)
    '==3311.331=        mCurLabourHourlyRatePriority2 = Value
    '==3311.331=     End Set
    '==3311.331= End Property '--rate.--
    '= = = = = = = =  = = =

    '==3311.331= WriteOnly Property LabourHourlyRatePriority3() As Decimal
    '==3311.331=     Set(ByVal Value As Decimal)
    '==3311.331=         mCurLabourHourlyRatePriority3 = Value
    '==3311.331=    End Set
    '==3311.331= End Property '--rate.--
    '= = = = = = = =  = = =

    WriteOnly Property LabourMinCharge() As Decimal
        Set(ByVal Value As Decimal)

            mCurLabourMinCharge = Value
        End Set
    End Property '-min.--
    '= = = = = = = =  = = =

    WriteOnly Property NotificationCostLimit() As Decimal
        Set(ByVal Value As Decimal)

            mCurNotificationCostLimit = Value
        End Set
    End Property '--limit.--
    '= = = = = = = =  = = =

    '-- Staff Id now comes from caller..--

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
    '= = = = = = = = = = = =  =

    '--  job No. comes if this is AMENDING new Job..-

    WriteOnly Property JobId() As Integer
        Set(ByVal Value As Integer)

            mlJobId = Value
        End Set
    End Property '--id.-
    '= = = = = = = = = = = =  =

    '-- Booking or Real Job..--
    '==3203.123=  NOE DECIDED HERE..
    '== WriteOnly Property IsBooking() As Boolean
    '==     Set(ByVal Value As Boolean)
    '==         mbIsBooking = Value
    '==     End Set
    '== End Property '--licence..-
    '= = = = = = = = = = = =

    WriteOnly Property IsCheckIn() As Boolean
        Set(ByVal Value As Boolean)
            mbIsCheckIn = Value
        End Set
    End Property '-- IsCheckIn--
    '= = = = = = = = =  = = = =

    WriteOnly Property IsOnSiteJob() As Boolean
        Set(ByVal Value As Boolean)
            mbIsOnSiteJob = Value
        End Set
    End Property '-- IsCheckIn--
    '= = = = = = = = =  = = = =

    '-- set user logo for printing..--
    WriteOnly Property UserLogo() As Object
        Set(ByVal Value As Object)

            picUserLogo.Image = Value
        End Set
    End Property '--logo..--
    '= = = = = = = =  ==

    '=3431.0505=
    '-- -results-
    '-- msExchangeCalendarUpdateXmlFileName-

    ReadOnly Property ExchangeCalendarUpdateXmlFileName As String
        Get
            ExchangeCalendarUpdateXmlFileName = msExchangeCalendarUpdateXmlFileName
        End Get
    End Property  '--msExchangeCalendarUpdateXmlFileName-
    '= = = = = = = = = = = = = == = = = = = = = == = = =

    '-- E n d properties..-
    '-- E n d properties..-
    '-===FF->

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String
        msFixSqlStr = Replace(sInstr, "'", "''")
    End Function '--fixSql-
    '= = = = = = = = = = = =

    '== 3431.0622--CleanUpXMLData-

    Private Function msCleanUpXMLData(ByVal strContent As String) As String
        Dim sResult As String

        sResult = Replace(strContent, "&", "&amp;")
        sResult = Replace(sResult, "<", "&lt;")
        sResult = Replace(sResult, ">", "&gt;")
        sResult = Replace(sResult, "'", "&apos;")
        sResult = Replace(sResult, """", "&quot;")
        msCleanUpXMLData = sResult

        '-TEMP testing-  main BG error detection.
        '= msCleanUpXMLData = strContent

    End Function '-msCleanUpXMLData-
    '= = = = = = = = = = = = = = = =

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
    '= = = = = = = = = = = = = =  =
    '-===FF->

    '-- Execute SQL Command..--
    '-- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Private Function mbExecuteSql(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As OleDbTransaction, _
                                      ByRef sLastSqlErrorMessage As String) As Boolean
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
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            sLastSqlErrorMessage = sErrorMsg
            '== MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    '--Loading..  
    '--    Check pre-selected ExtrasInCare CHECKBOXES...--
    '--3072/3 ==14Feb2013=

    Private Function mbCheckSelectedExtrasIncare(ByVal sExtrasText As String) As Boolean
        Dim s1, s2, s3, s4 As String
        Dim asExtras() As String
        Dim idx, ix, sx As Integer

        s3 = ""  '-- count unmatched file items..
        If (sExtrasText <> "") Then '-already have Extras.-
            asExtras = Split(sExtrasText, "; ")

            '-- Match file items to chkBox texts..
            If (asExtras.Length > 0) Then  '-- have some.
                For sx = 0 To UBound(asExtras)
                    s2 = ""  '--not missing-
                    For ix = 0 To (k_maxExtrasInCare - 1) '=listSymptoms.Items.Count - 1

                        s1 = UCase((Trim(chkExtras(ix).Text)))
                        '== s1 = UCase(listSymptoms.Items(ix).ToString)
                        If (InStr(s1, UCase(Trim(asExtras(sx)))) > 0) Then  '--found-
                            '= listSymptoms.SetItemCheckState(ix, CheckState.Checked)
                            chkExtras(ix).Checked = True
                            s2 = asExtras(sx)  '--found-
                            Exit For
                        Else  '--not found in ref list.
                        End If  '--foud-
                    Next  '--ix-
                    If (s2 = "") Then  '--not found-
                        s3 = s3 & asExtras(sx) & vbCrLf
                    End If  '--orphan-
                Next sx
            End If  '--file count-
        End If  '--have--
        '--report any orphan Extras..-
        If (s3 <> "") Then  '--have orphans..-
            MsgBox("NOTE: " & vbCrLf & "The Form's Checkboxes seems to have been changed," & vbCrLf & _
                   " and these prev. reported Extras have been orphaned: " & vbCrLf & s3 & vbCrLf & _
                       "Please check Job Extras again..", MsgBoxStyle.Information)
        End If
    End Function  '--mbCheckSelectedExtrasIncare-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- pack extras into string..

    Private Function mbSaveAllExtras() As String
        Dim sExtrasInCare As String
        Dim ix As Integer

        sExtrasInCare = ""
        mbSaveAllExtras = ""
        For ix = 0 To (k_maxExtrasInCare - 1)
            If (chkExtras(ix).CheckState = 1) Then '--add to list..-
                If (sExtrasInCare <> "") Then sExtrasInCare &= "; "
                sExtrasInCare &= chkExtras(ix).Text
                chkExtras(ix).Font = VB6.FontChangeBold(chkExtras(ix).Font, True) '--highlight for printer..-
            Else '--not checked..-
                chkExtras(ix).Font = VB6.FontChangeBold(chkExtras(ix).Font, False)
            End If
        Next ix
        mbSaveAllExtras = sExtrasInCare

    End Function  '-mbSaveAllExtras-
    '= = = = = = = = = = = = = =  =
    '-===FF->

    '-- Compute no of labels needed -
    '--  according to the no. of items checked in.

    Private Function mIntNumberOfLabels() As Integer
        Dim va1 As Object
        Dim colGoodsList As Collection
        Dim intNoLabels As Integer = 0

        If (msGoodsInCare <> "") Then  '--decode to count..
            Call gbDecodeGoodsIncare(msGoodsInCare, colGoodsList)
            If colGoodsList IsNot Nothing Then
                intNoLabels = colGoodsList.Count
            End If
        End If
        If msExtrasInCare <> "" Then '--count extras as well..-
            va1 = Split(msExtrasInCare, ";")
            If Not (IsNothing(va1)) Then
                intNoLabels = intNoLabels + UBound(va1) + 1
            End If '--empty..-
        End If '--extras..-
        mIntNumberOfLabels = intNoLabels

    End Function  '-mIntNumberOfLabels-
    '= = = = = = = =  = = = = == =  == 

    Private Sub mSetDataChanged()
        Dim intNoLabels As Integer = 0

        mbDataChanged = True
        cmdPrintAll.Enabled = False
        If mbAmending Then cmdFinish.Enabled = True
        cmdCancel.Text = "Cancel"

        '-recompute exras string..
        msExtrasInCare = mbSaveAllExtras()

        '-recompute no of labels needed..
        intNoLabels = mIntNumberOfLabels()
        If Not mbUserChangedNumberOfLabels Then
            '= DOESN't top fring.-NumUpDownLabels.Enabled = False  '--disable while we change it.
            mbNumUpDownLabels_Updating = True
            NumUpDownLabels.Value = intNoLabels
            mbNumUpDownLabels_Updating = False
            '= DOESN't top fring.-NumUpDownLabels.Enabled = True
        End If
    End Sub '--data changed.-
    '= = = = = = = =  = = =

    '--  edit goods line for text box display..-   
    Private Function msShowGoodsInCare(ByVal sGoodsLine As String) As String

        msShowGoodsInCare = Replace(sGoodsLine, vbTab, "; ")
    End Function  '--show goods..
    '= = = = = = = = = = = = = =  =
    '-===FF->

    '-- get priority..--
    '--- return opt index..-

    Private Function miGetPriority() As Short
        Dim index As Short
        Dim sDescr, s1 As String

        miGetPriority = 0
        If (cboPriority.SelectedIndex >= 0) Then '--selected.-
            sDescr = cboPriority.Text '--selected itm.--
            If IsNumeric(VB.Left(sDescr, 1)) Then
                index = CShort(VB.Left(sDescr, 1)) '--selected.. -
                If index = 3 Then
                    '===If (optCustPriority(2).Value = True) Then  '--pr-3-
                    mlPriorityColour = gIntPriority3Colour()   '--magenta--
                    '==3311.331= mCurLabourHourlyRate = mCurLabourHourlyRatePriority3
                    msPriority = "3"
                    index = 2
                ElseIf index = 2 Then  '==(optCustPriority(1).Value = True) Then  '-pr-2-
                    mlPriorityColour = gIntPriority2Colour()  '--cyan-
                    '==3311.331= mCurLabourHourlyRate = mCurLabourHourlyRatePriority2
                    msPriority = "2"
                    index = 1
                    '===  LabTicket.BackColor = vbBlue
                ElseIf index = 1 Then  '== (optCustPriority(0).Value = True) Then   '--pr-1-
                    mlPriorityColour = gIntPriority1Colour()   '-L. Sky Blue.
                    '==3311.331= mCurLabourHourlyRate = mCurLabourHourlyRatePriority1
                    msPriority = "1"
                    index = 0
                    '===LabTicket.BackColor = &HFF00FF     '-Home---magenta..-
                End If
            Else
                MsgBox("Invalid Priority in Descriptor.", MsgBoxStyle.Exclamation)
                Exit Function
            End If '--numeric
            '3311.410=-- TEST new system..  only if selected
            If gbGetPriorityInfo(mCnnJobs, msPriority, mbIsOnSiteJob, mRetailHost1, mCurLabourHourlyRate, s1) Then
                '== MsgBox("Labour Price is: " & _
                '==          FormatCurrency(mCurLabourHourlyRate, 2) & vbCrLf & s1, MsgBoxStyle.Information)
                '=LabDetailPriority.Text = s1
            End If  '-get-
        End If '--selected..-

        '==3311.331=   GET Labour rate for selected priority..

        mColorPriorityFG = Color.Black
        '--IF returned, overrides colour..-
        If chkReturned.Checked Then  '= (chkReturned_deleted.CheckState = 1) Then '--job returned..-
            mlPriorityColour = RGB(255, 0, 0) '--red.-
        End If  '-returned-
        If chkSystemUnderWarranty.Checked Then
            '=3203.117=
            mlPriorityColour = gIntUnderWarrantyColour()  '-dk violet-
            mColorPriorityFG = Color.White
        ElseIf mbIsOnSiteJob Then
            mlPriorityColour = gIntPriorityOnSiteJobColour()
        End If
        msPriorityText = sDescr '== optCustPriority(index).Caption
        miGetPriority = index

    End Function '--priority--
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- load list box of reference table of SYMPTOMS..-
    '-- load list box of reference table of SYMPTOMS..-

    Private Function mbLoadRefSymptoms() As Boolean
        Dim sSql As String
        Dim s1 As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim ix As Integer

        mbLoadRefSymptoms = False
        listSymptoms.Items.Clear()
        sSql = "Select * from [Symptoms] "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
            MsgBox("Failed to get Symptoms recordset.." & vbCrLf & _
                     "Error text:" & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Else '--build list box list of tasks performed so far..-
            If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                '==If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    '--add to list box for job..
                    s1 = Trim(dataRow1.Item("symptomDescr"))
                    listSymptoms.Items.Add(s1)
                Next dataRow1
                '== While (Not rs1.EOF) '---And (cx < 100)
                '==  rs1.MoveNext()
                '== End While '-eof-
                '= rs1.Close()
                mbLoadRefSymptoms = True
            End If '--rs-
        End If ''--get rs-
        rs1 = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
    End Function '--Load symptoms..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Check selected symptoms..

    Private Function mbCheckSelectedSymptoms(ByVal sSymptomText As String) As Boolean
        Dim s1, s2, s3, s4 As String
        Dim asSymptoms() As String
        Dim idx, ix, sx As Integer

        s3 = ""  '-- count unmatched file items..
        If (sSymptomText <> "") Then '-already have symptoms.-
            '==3067.0== cmdAddSymptom.Text = "New List"
            '==3067.0== labHelpProblem.Text = "Symptoms may have to be re-entered if Ref list changed.."
            asSymptoms = Split(sSymptomText, vbCrLf)
            '==3067.0 ==
            '--  set checked status for each symptom on file..-
            If (listSymptoms.Items.Count > 0) Then
                If (asSymptoms.Length > 0) Then  '-- have some.
                    For sx = 0 To UBound(asSymptoms)
                        s2 = ""  '--not missing-
                        For ix = 0 To listSymptoms.Items.Count - 1
                            s1 = UCase(listSymptoms.Items(ix).ToString)
                            If (InStr(s1, UCase(Trim(asSymptoms(sx)))) > 0) Then  '--found-
                                listSymptoms.SetItemCheckState(ix, CheckState.Checked)
                                s2 = asSymptoms(sx)  '--found-
                                Exit For
                            Else  '--not found in ref list.
                            End If  '--foud-
                        Next  '--ix-
                        If (s2 = "") Then  '--not found-
                            s3 = s3 & asSymptoms(sx) & vbCrLf
                        End If  '--orphan-
                    Next sx
                End If  '--file count-
            Else  '--no ref list.-
                If (asSymptoms.Length > 0) Then  '-- have some file symptoms.
                    s3 = txtSymptoms.Text  '--all are missing.-
                End If
            End If '--list count-
        End If  '-text.-
        '--report any orphan symptoms..-
        If (s3 <> "") Then  '--have orphans..-
            MsgBox("NOTE: " & vbCrLf & "The Ref. table of symptoms seems to have been changed," & vbCrLf & _
                            " and these prev. reported symptoms have been orphaned: " & vbCrLf & s3 & vbCrLf & _
                            "Please check Job symptoms again..", MsgBoxStyle.Information)
        End If
    End Function  '-check--
    '= = = = = = =  =  = =
    '-===FF->

    '-- L o a d  J o b  R e c o r d..-
    '-- L o a d  J o b  R e c o r d..-
    '-- L o a d  J o b  R e c o r d..-
    '==  REMOVED:  ByVal bBeginTrans As Boolean- (Optimistic)-


    Private Function mbGetJobRecord(ByVal lngJobNo As Integer, _
                                    ByVal bIsUpdateTransaction As Boolean, _
                                         ByRef ColJobFields As Collection) As Boolean

        Dim RsJob As DataTable  '= ADODB.Recordset
        Dim sSql, sName As String
        Dim ok As Boolean
        Dim colFld As Collection

        mbGetJobRecord = False
        sSql = "SELECT * from [jobs]  "
        sSql = sSql & " WHERE (job_id=" & CStr(mlJobId) & ")  " & vbCrLf
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If bIsUpdateTransaction Then
            ok = gbGetDataTableEx(mCnnJobs, RsJob, sSql, mTransactionUpdateJob)
        Else  '--no-
            ok = gbGetDataTable(mCnnJobs, RsJob, sSql)
        End If
        If Not ok Then  '== gbGetDataTable(mCnnJobs, RsJob, sSql) Then
            MsgBox("Failed to get JOB recordset.." & vbCrLf & _
                       "Error text:" & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf & _
                          vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Me.Hide
            Exit Function
        End If
        '--txtMessages.Text = ""
        ColJobFields = New Collection
        If (Not (RsJob Is Nothing)) AndAlso (RsJob.Rows.Count > 0) Then
            '== If RsJob.BOF And (Not RsJob.EOF) Then
            '== RsJob.MoveFirst()
            '== End If
            Dim dataRow1 As DataRow = RsJob.Rows(0)  '--first row-
            '== If (Not RsJob.EOF) Then '---And (cx < 100)
            '--return complete row..-
            For Each column1 As DataColumn In RsJob.Columns '= fld1 In RsJob.Fields
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
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsJob = Nothing
    End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '--Check for all required flds completed..
    '-- return string list of missing flds..

    Private Function msCheckFormComplete() As String
        Dim sResult As String
        Dim s1 As String
        Dim gx, ix As Integer
        '== Dim item1 As System.Windows.Forms.ListViewItem
        Dim colGoodsItem As Collection
        Dim colResultGoodsInCare As Collection

        sResult = ""
        '=3077= If (msPriority = "") Then sResult = "Job Priority not selected.."
        '==3072== msGoodsInCare = ""
        '==3072== msPrintGoodsInCare = ""
        '==3072== colResultGoodsInCare = mColCollectGoodsInCare()
        '==3072== Call gbEncodeGoodsIncare(colResultGoodsInCare, msGoodsInCare, msPrintGoodsInCare)
        If (msGoodsInCare = "") And (txtGoodsOther.Text = "") Then
            sResult = sResult & vbCrLf & "GoodsInCare."
        End If

        '-- (NOW Gone)- Extras can only be entered once for the job..--
        '== 3072/3= 14Feb2013= 
        '--   NOW user can always Amend ExtrasInCare..-
        '== 3072/3= If (msExtrasInCare = "") Then '--can save check boxes..--
        '--Now accum labels of all checked EXTRAS..-
        msExtrasInCare = ""
        For ix = 0 To (k_maxExtrasInCare - 1)
            If (chkExtras(ix).CheckState = 1) Then '--add to list..-
                If (msExtrasInCare <> "") Then msExtrasInCare = msExtrasInCare & "; "
                msExtrasInCare = msExtrasInCare & chkExtras(ix).Text
                chkExtras(ix).Font = VB6.FontChangeBold(chkExtras(ix).Font, True) '--highlight for printer..-
            Else '--not checked..-
                chkExtras(ix).Font = VB6.FontChangeBold(chkExtras(ix).Font, False)
            End If
        Next ix
        '== 3072/3= End If '--extras..-

        '==  msProblems = ""  '--  SEE "ProblemShort"..--
        msProblemDetails = txtProblem.Text

        msSymptoms = txtSymptoms.Text
        '=============  If (msProblems = "") And (msProblemDetails = "") And (msSymptoms = "") Then
        If (msProblemDetails = "") And (msSymptoms = "") Then
            sResult = sResult & vbCrLf & "Symptoms/Problems."
        End If
        '-- Rev-2914--
        If (optQuotation(0).Checked = False) And (optQuotation(1).Checked = False) And (optQuotation(2).Checked = False) Then
            sResult = sResult & vbCrLf & "Customer Instruction must be selected."
        End If

        '==If (Not mbIsOnSiteJob) Then
        If (msPriority = "") Or (cboPriority.SelectedIndex < 0) Then
            sResult = sResult & vbCrLf & "Job Priority must be selected.."
        End If
        '==End If
        '==3083==
        If mbIsOnSiteJob Then
            If Not datePromised.Checked Then
                sResult = sResult & vbCrLf & "ON-SITE Job must have Date Promised selected."
            End If
        End If
        If mbIsOnSiteJob Then
            If (txtTechName.Text = "") Then
                sResult = sResult & vbCrLf & "ON-SITE Job must specify Nominated Tech name."
            End If
        End If

        msCheckFormComplete = sResult
        colResultGoodsInCare = Nothing
        colGoodsItem = Nothing
    End Function '--check form..--
    '= = = = = = = = =
    '= = = = = = =  =  = =
    '-===FF->

    '--- Print the NewJob form.--
    '-- NEW Rev:2914..  Uses PrintDocs class
    '-- NEW Rev:2914..  Uses PrintDocs class
    '-- Print the NewJob form.--

    Private Function mbPrintNewJobForm() As Integer
        Dim prtDocs1 As New clsPrintDocs
        Dim kx, iPos, ix, lResult As Integer
        Dim s1, s2 As String
        '== Dim sTermsText As String
        '== Dim sFullPath As String
        Dim sUsernames As String
        Dim colBusiness As Collection
        Dim colCustomer As Collection
        Dim colResultGoodsInCare As Collection
        Dim col1 As New Collection

        mbPrintNewJobForm = -1
        Call miGetPriority()

        iPos = InStr(LabTicket.Text, ":")
        If iPos > 1 Then
            '== prtDocs1.TicketDate = VB.Left(LabTicket.Text, iPos - 1) '--"Sep-09"
            '3203.119=
            prtDocs1.TicketDate = Trim(VB.Mid(LabTicket.Text, iPos + 1)) '--"Sep-2016"
        End If '--iPos..-
        If mbAmending Then
            s2 = "Updated/printed By: " '--stay on this line..-
            s1 = FormatDateTime(CDate(DateTime.Today), DateFormat.LongDate) & ", " & _
                                             FormatDateTime(TimeOfDay, DateFormat.ShortTime)
        Else '--new..-
            s2 = "Received By: " '--stay on this line..-
            s1 = LabDateRcvd.Text
        End If
        prtDocs1.HeaderDate = s2 & msStaffName & Space(12) & s1

        '==3083== ON-SITE Jobs..-
        If mbIsOnSiteJob Then
            prtDocs1.IsOnSiteJob = True
            prtDocs1.OnSiteDate = msDatePromised
            prtDocs1.OnSiteTime = msTimepromised
        End If

        '== prtDocs1.PrtSelectedPrinter = mPrtColour
        prtDocs1.PrtSelectedPrinterName = msColourPrinterName

        prtDocs1.Version = LabVersion.Text
        prtDocs1.UserLogo = picUserLogo.Image

        '=3203.103=  ADD picture if any..
        prtDocs1.ItemImage = picSubjectItem.Image

        prtDocs1.JobNo = mlJobId
        prtDocs1.LicenceOK = mbLicenceOK

        '=3107.904= Show ONSITE ticket box colour-
        If mbIsOnSiteJob Then
            prtDocs1.PriorityColour = RGB(218, 165, 32)  '-goldenrod-
        Else
            prtDocs1.PriorityColour = mlPriorityColour '==vbMagenta
        End If

        '--load business info..-
        colBusiness = New Collection
        colBusiness.Add(msBusinessName, "BusinessName")
        colBusiness.Add(msBusinessShortName, "BusinessShortName")
        colBusiness.Add(msBusinessAddress1, "BusinessAddress1")
        colBusiness.Add(msBusinessAddress2, "BusinessAddress2")
        colBusiness.Add(msBusinessState, "BusinessState")
        colBusiness.Add(msBusinessPostCode, "BusinessPostcode")
        colBusiness.Add(msBusinessEmail, "BusinessEmail")
        prtDocs1.Business = colBusiness

        prtDocs1.LabourMinCharge = mCurLabourMinCharge
        prtDocs1.LabourHourlyRate = mCurLabourHourlyRate
        '====prtDocs1.NotificationCostLimit = mCurNotificationCostLimit

        '--load cust info..-
        colCustomer = New Collection
        colCustomer.Add(msCustomerBarcode, "CustomerBarcode")
        colCustomer.Add(msCustomerName, "CustomerName")
        colCustomer.Add(msCustomerCompany, "CustomerCompany")
        colCustomer.Add(msCustomerPhone, "CustomerPhone")
        colCustomer.Add(msCustomerMobile, "CustomerMobile")
        colCustomer.Add(msPriorityText, "CustomerPriorityText")
        colCustomer.Add(txtTechName.Text, "CustomerTechName")
        prtDocs1.Customer = colCustomer

        '--load goods info..-
        '==3072== colResultGoodsInCare = mColCollectGoodsInCare()
        Call gbDecodeGoodsIncare(msGoodsInCare, colResultGoodsInCare)

        prtDocs1.ResultGoods = colResultGoodsInCare
        prtDocs1.ExtrasInCare = txtGoodsOther.Text & vbCrLf & vbCrLf & msExtrasInCare

        prtDocs1.JobReturned = chkReturned.Checked   '== (chkReturned_deleted.CheckState = 1)
        '= 3203.118=
        prtDocs1.SystemUnderWarranty = chkSystemUnderWarranty.Checked   '== (chkReturned_deleted.CheckState = 1)

        '-- Print users/passwords..
        sUsernames = ""
        For ix = 0 To k_maxUserNames - 1
            If sUsernames <> "" Then sUsernames = sUsernames & vbCrLf '--not the first..-
            If (Trim(txtUserName(ix).Text) <> "") Or (Trim(txtPassword(ix).Text) <> "") Then
                s1 = Trim(txtUserName(ix).Text)
                If (s1 = "") Then s1 = " ? "
                sUsernames = sUsernames & s1 & "  (" & txtPassword(ix).Text & ")"
            End If
        Next ix
        prtDocs1.UserNames = sUsernames
        prtDocs1.Symptoms = txtSymptoms.Text
        prtDocs1.Problem = txtProblem.Text

        s1 = ""
        If (ChkBackupReq.CheckState = 1) Then '--data backup..-
            s1 = "* Data Backup Required. "
        End If '--backup.-
        If (ChkRecovDisk.CheckState = 1) Then '--data backup..-
            s1 = s1 & " * Recovery Disk $Extra. "
        End If '--backup.-
        prtDocs1.DataBackup = s1

        '==prtDocs1.QuotationRequired = optQuotation(0).Value
        prtDocs1.InstructionBGColour = System.Drawing.ColorTranslator.ToOle(LabCostLimit.BackColor)
        If (optQuotation(0).Checked = True) Then '--quote.-
            prtDocs1.InstructionLabel = "Quotation Required!"
            prtDocs1.InstructionText = "Assessment and Quotation are required. " & _
                                   " The standard minimum charge of " & FormatCurrency(mCurLabourMinCharge, 2) & " will apply."
        ElseIf (optQuotation(1).Checked = True) Then  '--proceed ok...-
            prtDocs1.InstructionLabel = "Proceed ok as agreed."
            prtDocs1.InstructionText = "Approval is given to proceed with agreed service.."
        ElseIf (optQuotation(2).Checked = True) Then  '--proceed to Limit...-
            prtDocs1.InstructionLabel = "Proceed to Limit of " & FormatCurrency(mCurNotificationCostLimit, 2)
            prtDocs1.InstructionText = "Approval is given to proceed with service up to " & _
                                         FormatCurrency(mCurNotificationCostLimit, 2) & " total cost.. " & _
                                         "  Customer will be called for approval if any further cost is anticipated.."
        End If

        If (msServiceChargesInfoText <> "") Then
            prtDocs1.ServiceChargesInfoText = msServiceChargesInfoText
        End If
        prtDocs1.TermsText = msTermsText

        '-- go..--
        mbPrintNewJobForm = prtDocs1.PrintNewJobForm

        prtDocs1 = Nothing

    End Function '-- PrintNewJob--
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  Build Receipt-lines collection..--
    '---  must FOLLOW "checkFormComplete" in time..--

    Private Function mbBuildReceipt(ByRef lngJobNo As Integer, ByRef colLines As Collection) As Boolean
        Dim sShortDate As String
        Dim s1, s2 As String
        Dim L1 As Integer
        Dim sLine As String
        Dim sABN As String
        Dim sAllGoods As String = ""
        Dim sDatePromised As String = ""
        Dim colResultGoodsInCare As Collection
        Dim col1 As Collection

        On Error GoTo Receipt_error
        sShortDate = VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy ") & FormatDateTime(TimeOfDay, DateFormat.ShortTime)
        '-- Format ABN for printing..-
        sABN = VB.Left(msABN, 2) & " " & Mid(msABN, 3, 3) & " " & Mid(msABN, 6, 3) & " " & Mid(msABN, 9, 3)

        colLines = New Collection

        colLines.Add("") '--new line..--
        colLines.Add("") '--new line..--
        colLines.Add("<bold>")
        If mbAmending Then
            colLines.Add("Job Amended: " & sShortDate)
        Else '--new..-
            colLines.Add("Job Submitted: " & sShortDate)
        End If
        colLines.Add("") '--new line..--
        colLines.Add("<big>")
        colLines.Add("<bold>")
        colLines.Add(IIf((msBusinessName <> ""), msBusinessName, "JobMatix JobTracking"))
        colLines.Add("<bold>")
        colLines.Add("ABN: " & sABN & ".")
        '---Precise; 82 563 967 866.--
        colLines.Add(msBusinessAddress1) '--"13 Commercial Rd."
        colLines.Add(msBusinessAddress2) '--"Murwillumbah NSW 2484."
        '===colLines.Add "<bold>"
        colLines.Add(msBusinessState & "  " & msBusinessPostCode & " Australia.")
        colLines.Add("Telephone: " & msBusinessPhone)
        colLines.Add("Email: " & msBusinessEmail)
        '--colLines.Add "Tel: (02) 6672 8300."
        colLines.Add("")
        '--colLines.Add sShortDate
        colLines.Add("Served by: " & msStaffName)
        colLines.Add("")
        If Not mbLicenceOK Then
            colLines.Add("<big>")
            colLines.Add("<bold>")
            colLines.Add("=  J o b M a t i x ==")
            colLines.Add("<big>")
            colLines.Add("<bold>")
            colLines.Add("= Job Tracking:  S o f t w a r e =")
            colLines.Add("<big>")
            colLines.Add("<bold>")
            colLines.Add("= F o r  E v a l u t i o n  O n l y = ")
            colLines.Add("") '--new line..--
            colLines.Add("") '--new line..--
        End If
        colLines.Add("<bold>")
        colLines.Add("Customer:")
        If (msCustomerCompany <> "") And (msCustomerCompany <> "--") And (UCase(msCustomerCompany) <> "N/A") Then '--show company..-
            colLines.Add("Company: " & msCustomerCompany)
        End If
        If (msCustomerName <> "") Then colLines.Add(msCustomerName)
        colLines.Add("") '--new line..--
        colLines.Add("<bold>")
        colLines.Add("Priority: " & msPriorityText)
        colLines.Add("") '--new line..--

        colLines.Add("<bold>")
        colLines.Add("Goods Accepted for Service:") '--new line..--

        '== 09Mar2012= Ver-3031-  reformat for printing.-
        '== s1 = Replace(msGoodsInCare, "SerialNo", vbCrLf & "  * SerialNo")
        '== colLines.Add(s1) '== msGoodsInCare

        '== 09Mar2012= Ver-3031-  reformat for printing.-
        '==3072== colResultGoodsInCare = mColCollectGoodsInCare()
        Call gbDecodeGoodsIncare(msGoodsInCare, colResultGoodsInCare)

        For Each col1 In colResultGoodsInCare
            s1 = col1.Item("type")
            s1 = s1 & ";  " & col1.Item("brand")
            s1 = s1 & ";  " & col1.Item("model")
            s2 = col1.Item("serialno")
            If (s2 <> "") Then s1 = s1 & vbCrLf & "  SerialNo: " & s2
            If (sAllGoods <> "") Then sAllGoods = sAllGoods & vbCrLf
            sAllGoods = sAllGoods & s1
        Next col1
        colLines.Add(sAllGoods) '== msGoodsInCare

        '-3501.1223= ONSITE Date-
        If mbIsOnSiteJob Then  '--show timeBooked
            sDatePromised = Format(datePromised.Value, "ddd dd-MMM-yyyy ") '=PRINTING sDatePromised
            sDatePromised &= Format(TimeValue(timeOnSite.Value), "hh:mm tt")
            '= msTimepromised = Format(TimeValue(timeOnSite.Value), "hh:mm tt")
            colLines.Add("ON-SITE Date/Time agreed:") '--new line..--
            colLines.Add("-- " & sDatePromised) '-..--
        Else '--not onsite.-
            '=sDatePromised &= "17:00 "
        End If

        If (txtGoodsOther.Text <> "") Then colLines.Add(txtGoodsOther.Text)
        If msExtrasInCare <> "" Then
            '--colLines.Add "Extras: "   '--new line..--
            colLines.Add(msExtrasInCare)
        End If
        colLines.Add("") '--new line..--

        colLines.Add("<bold>")
        colLines.Add("Problem Reported:") '--new line..--
        '--colLines.Add msProblems
        '===If (msSymptoms <> "") Then colLines.Add msSymptoms
        '===If (msProblemDetails <> "") Then colLines.Add msProblemDetails
        If (txtSymptoms.Text <> "") Then colLines.Add(txtSymptoms.Text)
        If (txtProblem.Text <> "") Then colLines.Add(txtProblem.Text)
        colLines.Add("") '--new line..--

        '-- Instructions..-
        colLines.Add("<bold>")
        colLines.Add("Cust.Instructions:") '--new line..--
        colLines.Add(LabCostLimit.Text)

        '--  set up labour rate display..-
        If (msServiceChargesInfoText <> "") Then  '--print supplied info..-
            colLines.Add("") '--new line..--
            colLines.Add("<bold>")
            colLines.Add("Non-warranty Service Charge Rates:")
            colLines.Add(msServiceChargesInfoText)
            colLines.Add("") '--new line..--
        Else  '--show setup rates.. 
            s1 = "n/a"
            If (mCurLabourMinCharge > 0) Then s1 = FormatCurrency(mCurLabourMinCharge, 2)
            s2 = "n/a"
            If (mCurLabourHourlyRate > 0) Then s2 = FormatCurrency(mCurLabourHourlyRate, 2)
            colLines.Add("") '--new line..--
            colLines.Add("<bold>")
            colLines.Add("== Minimum Charge: " & s1 & " ==") '==$44.00 =="   '--new line..--
            colLines.Add("<bold>")
            colLines.Add("== Charge per Hour: " & s2 & " ==") '== $88.00 =="
            colLines.Add("") '--new line..--
            '--colLines.Add "Your Reference:"
        End If

        colLines.Add("<big>")
        colLines.Add("<bold>")
        colLines.Add("Your Reference: " & CStr(lngJobNo) & ".")
        colLines.Add("Thank You.")
        colLines.Add("= = = = = = = = = = = = = = = = = =")
        colLines.Add(msNewJobDocketFootnote)
        '===== colLines.Add "= JT Rev: " & App.Revision & " = = "  '--new line..--
        colLines.Add("= = = = = = = = = = = = = = = = = =")
        mbBuildReceipt = True
        Exit Function

Receipt_error:
        L1 = Err().Number
        MsgBox("Runtime Error in Build-Receipt.." & vbCrLf & "Error=" & L1 & ": " & ErrorToString(L1), MsgBoxStyle.Critical)
    End Function '--Receipt--
    '= = = = = = =  =  = =
    '-===FF->

    '--  NEW..--
    '--   PrintReceipt  --
    '--   PrintReceipt  --

    Private Function mbPrintReceipt(ByRef colReportLines As Collection) As Boolean
        Dim prtDocs1 As New clsPrintDocs

        prtDocs1 = New clsPrintDocs
        prtDocs1.Version = LabVersion.Text

        'UPGRADE_WARNING: Couldn't resolve default property of object prtDocs1.UserLogo. Click for more: 'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        prtDocs1.UserLogo = picUserLogo.Image
        '== prtDocs1.PrtSelectedPrinter = mPrtReceipt
        prtDocs1.PrtSelectedPrinterName = msReceiptPrinterName

        mbPrintReceipt = prtDocs1.PrintReceipt(colReportLines)

    End Function
    '= = = = = = =  = = = =  = = =
    '-===FF->

    '-- lookup RM Staff to given long ID..--
    '-- lookup RM Staff to given long ID..--

    '====19May2010=  NOW MUST USE BARCODE..=====
    '====19May2010=  NOW MUST USE BARCODE..=====

    '====Private Function mbLookupStaff(lngStaffId As Long  ====
    Private Function mbLookupStaffBarcode(ByRef sBarcode As String, ByRef colFields As Collection) As Boolean
        Dim colFld As Collection '--"name"=, "value"-
        '== Dim fld1 As ADODB.Field
        Dim s1 As String
        Dim sName As String

        mbLookupStaffBarcode = False

        '--staff Signon..--
        If mRetailHost1.staffGetStaffRecord(sBarcode, -1, colFields) Then '--found..--
            '== s1 = colFields("docket_name")("value")
            '== MsgBox "Found: " & s1, vbInformation
            mbLookupStaffBarcode = True
        Else
            MsgBox("Staff code not found.", MsgBoxStyle.Exclamation)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

    End Function '--staff lookup..--
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Get Waitlisted Jobs List..-
    '-- Get Waitlisted Jobs List..-

    Private Function mbGetCustomerWaitList(ByVal lngCustomerId As Integer, _
                                                 ByRef lngJobNo As Integer, _
                                                   ByRef strGoods As String, _
                                                         ByRef sDate As String) As Boolean
        Dim lngCount As Integer
        Dim sSql As String
        Dim rs1 As DataTable  '= ADODB.Recordset

        mbGetCustomerWaitList = False
        lngJobNo = -1
        sSql = " SELECT Job_id, DateCreated, TechStaffName AS Tech,  "
        sSql = sSql & "  JobStatus,  GoodsInCare, ProblemSymptoms, Priority "
        sSql = sSql & "  FROM Jobs WHERE (RMCustomer_Id= " & _
                                              CStr(mlCustomerId) & ") AND (LEFT(JobStatus,2)='05')  "
        sSql = sSql & "   ORDER BY Job_Id; "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If gbGetDataTable(mCnnJobs, rs1, sSql) Then '--ok-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--return job info..-
            If (Not (rs1 Is Nothing)) AndAlso (rs1.Rows.Count > 0) Then
                '== If (Not rs1.BOF) And (Not rs1.EOF) Then '--not empty.-
                '= rs1.MoveFirst()
                Dim dataRow1 As DataRow = rs1.Rows(0)  '--first rwo-
                '== If (Not rs1.EOF) Then '---And (cx < 100)
                lngJobNo = dataRow1.Item("Job_Id")
                strGoods = dataRow1.Item("GoodsInCare")
                sDate = VB6.Format(CDate(dataRow1.Item("DateCreated")), "dd-mm-yyyy")
                mbGetCustomerWaitList = True
                '= End If '--eof-
                '== End If '--empty.
            End If '--nothing.-
        Else '--failed..-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get Cust/Jobs recordset.." & vbCrLf & _
                      "Error text:" & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        End If
    End Function '--refresh..-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--  set up customer details from RETAIL customer record..--
    '--  set up customer details from customer record..--

    Private Function mbSetupCustomer(ByVal bCheckJobs As Boolean, _
                                      ByRef colFields As Collection) As Boolean
        Dim sName, sValue As String
        Dim s1, s2, s3, s4 As String
        Dim lngJobNo As Integer
        Dim sGoods, sDate As String
        Dim sAddress As String = ""

        mbSetupCustomer = True
        msCustomerName = ""
        msCustomerCompany = "" : s3 = "" : s4 = ""
        mlCustomerId = CInt(colFields.Item("customer_id")("value"))
        msCustomerBarcode = colFields.Item("barcode")("value")
        msCustomerCompany = colFields.Item("company")("value")
        If (msCustomerCompany = "") Then '--no company name..--
            msCustomerCompany = "--" '--"n/a" '==  don't want blank company..--
        End If
        sName = msCustomerCompany
        s3 = colFields.Item("given_names")("value")
        s4 = colFields.Item("surname")("value")
        msCustomerPhone = colFields.Item("phone")("value")
        If Len(msCustomerPhone) > 20 Then
            msCustomerPhone = VB.Left(Replace(msCustomerPhone, " ", ""), 20)
        End If
        msCustomerMobile = colFields.Item("mobile")("value")
        If Len(msCustomerMobile) > 20 Then
            msCustomerMobile = VB.Left(Replace(msCustomerMobile, " ", ""), 20)
        End If
        '-- save customer info..--
        '---msCustomerName = Left(s3 + " " + s4, 50)   '--max 50--
        msCustomerName = s4 '--max 50--SURNAME, GivenNames --
        If msCustomerName <> "" Then msCustomerName = msCustomerName & ", "
        '== txtCustName.Text = Left(UCase(msCustomerName) + s3, 50)   '--max 50--SURNAME, GivenNames --
        msCustomerName = VB.Left(msCustomerName & s3, 50) '--max 50--Surname, GivenNames --
        txtCustomer.Text = "[" & msCustomerBarcode & "]  " & msCustomerCompany & vbCrLf & _
                                                          msCustomerName & vbCrLf & msCustomerPhone & " [" & msCustomerMobile & "]"
        '==3083== Add Packed ADRESS--
        sAddress = IIf(IsDBNull(colFields.Item("addr1")("value")), "", colFields.Item("addr1")("value"))
        If (sAddress <> "") Then sAddress &= vbCrLf
        s1 = IIf(IsDBNull(colFields.Item("addr2")("value")), "", colFields.Item("addr2")("value"))
        If (s1 <> "") Then sAddress &= s1 & vbCrLf
        '== If (sAddress <> "") Then sAddress &= vbCrLf
        s1 = IIf(IsDBNull(colFields.Item("addr3")("value")), "", colFields.Item("addr3")("value"))
        If (s1 <> "") Then sAddress &= s1 & vbCrLf
        '== If (sAddress <> "") Then sAddress &= vbCrLf
        sAddress &= IIf(IsDBNull(colFields.Item("suburb")("value")), "", colFields.Item("suburb")("value"))
        sAddress &= IIf(IsDBNull(colFields.Item("state")("value")), "", " " & colFields.Item("state")("value"))
        sAddress &= IIf(IsDBNull(colFields.Item("postcode")("value")), "", " " & colFields.Item("postcode")("value"))
        msCustomerAddress = sAddress

        If bCheckJobs Then
            If mbGetCustomerWaitList(mlCustomerId, lngJobNo, sGoods, sDate) Then
                If MsgBox("Important Note: " & vbCrLf & _
                          "There is already a WAIT-LISTED Job Booking on file for this Customer !" & vbCrLf & vbCrLf & _
                            "    Job No: " & lngJobNo & ";" & vbCrLf & "    Date Created: " & sDate & ";" & vbCrLf & _
                            "    Goods to Check-in: " & sGoods & ";" & vbCrLf & vbCrLf & _
                                        " Do you want to continue and set up a new Job ? ", _
                                  MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                    mbSetupCustomer = False '-- Me.Hide
                End If '--msg..-
            Else '-- no waitng jobs..-
            End If '--get rs..-
        End If '--check..-
    End Function '--setup--
    '= = = = = = = = =  =
    '---===FF->

    '-- L o a d---
    '-- L o a d---

    '===  PROPERTY VARS are ALREADY SET...
    '------  DO NOT CHANGE THEM..-

    Private Sub mbOriginal_frmNewJob3_Load()
        Dim kx, gx, ix, lngError As Integer
        '==  Dim sName As String
        Dim s1, s2 As String
        '== Dim sFullPath As String

        msAppPath = My.Application.Info.DirectoryPath
        If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"

        msExtrasInCare = ""
        '-- Booking can't do extras..--
        For ix = 0 To (k_maxExtrasInCare - 1)
            chkExtras(ix).Enabled = False
        Next ix

        Call CenterForm(Me)

        txtCustomer.Text = ""

        msPriorityText = ""
        msPriority = ""
        mlPriorityColour = &HF0F0F0 '--white

        '=== txtNomTech.Enabled = False
        txtNomTech.Text = ""
        txtTechName.Text = ""
        '== txtTechName.Enabled = False
        txtTechName.ReadOnly = True

        mlGoodsCount = 0
        txtGoodsList.Text = ""

        txtGoodsOther.Text = ""
        txtExtrasInCare.Text = ""
        txtExtrasInCare.Top = chkExtras(0).Top
        txtExtrasInCare.Left = chkExtras(0).Left
        txtExtrasInCare.Height = 47

        '== txtExtrasInCare.Width = VB6.TwipsToPixelsX(4560)
        txtExtrasInCare.Visible = False

        '--txtModel.Text = ""
        '=== For gx = 0 To k_maxGoodsInCare: txtModel(gx).Text = "": Next gx  '--clear modellist.-
        '--DISable Deletions..-
        '=== For ix = 0 To k_maxGoodsInCare
        '===          cmdGoodsDelete(ix).Enabled = False
        '===Next ix

        txtProblem.Text = ""
        FrameGoods.Enabled = False
        '==3067.0== labHelpProblem.Text = ""

        '-- CAN'T disable TabControl panels..   ?????  -- see below-
        '== SSTabNewJob.TabPageCollection(0).enabled = False

        '-- "Enabled" property is not visible but extsts..--
        '-  FROM:
        '= http://social.msdn.microsoft.com/Forums/windows/en-US/985b41c3-a1de-4744-8875-63262d4c2718/tabcontrol-disableenable-tab-page?forum=winforms
        _SSTabNewJob_TabPage1.Enabled = False
        _SSTabNewJob_TabPage2.Enabled = False

        '-- Binds the event handler DrawOnTab to the DrawItem event 
        '-- through the DrawItemEventHandler delegate.
        '== AddHandler SSTabNewJob.DrawItem, AddressOf SSTabNewJob_DrawOnTab
        '-- experimenting--
        '== mTabArea = SSTabNewJob.GetTabRect(0)
        '= mTabTextArea = RectangleF.op_Implicit(SSTabNewJob.GetTabRect(0))


        FrameUsers.Text = ""  '---- "-User Details-"
        '== frameNominTech.Text = ""
        frameProblem.Text = ""
        frameProblem.Enabled = False
        frameInstructions.Enabled = False
        frameInstructions.Text = ""

        FrameUsers.Enabled = False
        '== frameNominTech.Enabled = False

        ChkUsers.CheckState = System.Windows.Forms.CheckState.Unchecked '--Multiple users not checked..-
        For ix = 0 To k_maxUserNames - 1
            txtUserName(ix).Text = ""
            txtPassword(ix).Text = ""
            txtUserName(ix).Enabled = False
            txtPassword(ix).Enabled = False
        Next ix

        '--  INIT symptoms list box..--
        txtSymptoms.Text = "" '--clear..-
        listSymptoms.Items.Clear()
        listSymptoms.Enabled = True
        '== 3067.0 == listSymptoms.Visible = False
        '==3067.0== cmdSymptomsApply.Enabled = False
        '===txtNewSymptom.Text = ""
        cmdCheckGoods.Enabled = False

        LabTicket.Text = ""

        '== mbLicenceOK = False
        '== msABN = ""

        '== == ==  Me.KeyPreview = True  '--catch TABs ??--
        '-- !! This must be last..-
        cmdFinish.Enabled = False
        cmdPrintAll.Enabled = False
        '=====cmdPrintLabel.Enabled = False
        '--  truncate Terms box until print time.--

        '--Pic2 is for printing..--
        '==Picture2.Top = FrameCust.Top + 600 '--hide pic2--
        '==Picture2.Left = FrameCust.Left + 600
        picUserLogo.Visible = False

        msJobOriginalStatus = ""
        LabJobStatus.Text = ""
        chkRefreshCustomer.Enabled = False
        mbCustomerRefreshed = False

        cboPriority.Items.Clear()

        chkPrtDocs(k_PRINT_AGREEMENT).Enabled = False
        chkPrtDocs(k_PRINT_RECEIPT).Enabled = False
        chkPrtDocs(k_PRINT_LABEL).Enabled = False
        NumUpDownLabels.Enabled = False   '==3093.717=

        mbLabelsPrinted = False

        optQuotation(0).Enabled = False
        optQuotation(1).Enabled = False
        optQuotation(2).Enabled = False
        optQuotation(0).Checked = False
        optQuotation(1).Checked = False
        optQuotation(2).Checked = False

        LabCostLimit.Text = ""
        '-- Load Terms text..-Rev-2914--
        '-load terms text..--
        '== msTermsText = ""

        '-- Rev-2918= Terms text now input property..==--
        '-- Rev-2918= Terms text now input property..==--

        msProblemShort = "" '-- now used to hold Flags-
        '--   eg. *QUOTATION-REQUIRED*  or *PROCEED-WITH-SERVICE*---
        '== msNewJobDocketFootnote = "We will notify you when job is ready..  Payable on collection.."

        '== cboPrevGoods.Top = ListViewGoods.Location.Y
        '=3083= cboPrevGoods.Width = 77  '== 340
        cboPrevGoods.Enabled = False
        '== cmdPrevGoods.Enabled = False
        labSelectPrevious.Visible = False

        '== 3067.0 ==
        '== s1 = Dir(msAppPath & "JobMatix3.chm")
        s1 = gsGetHelpFileName()
        If (s1 <> "") Then
            HelpProvider1.HelpNamespace = s1
            HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
            HelpProvider1.SetHelpKeyword(Me, "JT3-ServiceAgreement.htm")
        End If
        '-- 3083--
        datePromised.MinDate = DateTime.Today
        '-- In Activated- Set time for Now + 1 hour..
        '==3311.410= datePromised.MinDate = DateAdd(DateInterval.Hour, 1, DateTime.Now)
        datePromised.Format = DateTimePickerFormat.Custom
        datePromised.CustomFormat = "ddd dd-MMM-yyyy"

        timeOnSite.Visible = False
        labOnSiteTime.Visible = False
        labOnSiteFields.Visible = False

        SSTabNewJob.DrawMode = TabDrawMode.OwnerDrawFixed

    End Sub '--load--
    '= = = = = = =  = = =
    '---===FF->

    '--  EX Activate  GOSUB--
    '--  EX Activate  GOSUB--

    Private Sub mActivate_EnablePrinting()

        '-- print docs..--
        '==If Not mbIsBooking Then  '--check-in or new job.--  can print..
        chkPrtDocs(k_PRINT_AGREEMENT).Enabled = True
        chkPrtDocs(k_PRINT_AGREEMENT).CheckState = System.Windows.Forms.CheckState.Checked '--yes.-
        chkPrtDocs(k_PRINT_RECEIPT).Enabled = True
        chkPrtDocs(k_PRINT_RECEIPT).CheckState = System.Windows.Forms.CheckState.Checked '--yes.-
        '====If (mcurJobLabelPrintDepth > 0) Then '--have label size....-
        '==If Not (mPrtLabel Is Nothing) Then
        If (msLabelPrinterName <> "") Then   '--  have one..--
            chkPrtDocs(k_PRINT_LABEL).Enabled = True
            chkPrtDocs(k_PRINT_LABEL).CheckState = System.Windows.Forms.CheckState.Checked '--checked..--
            NumUpDownLabels.Enabled = True
        Else '--no printer- no labels..--
            chkPrtDocs(k_PRINT_LABEL).Enabled = False
            chkPrtDocs(k_PRINT_LABEL).CheckState = System.Windows.Forms.CheckState.Unchecked '--not checked..--
            NumUpDownLabels.Enabled = False
        End If
        '==End If  '--not booking.-

    End Sub '-Activate_EnablePrinting-
    '= = = = = = = = = = = = = = = = =

    '=3083= OnSite (Customer site Jobs..--

    Private Function mbOnSiteSetup() As Boolean
        Dim date1 As DateTime

        labGoodsHdr.Text = "ON-SITE Job"
        labGoodsHdr.BackColor = Color.Goldenrod

        labSelectPrevious.Visible = False
        cmdCheckGoods.Visible = False
        cmdGoodsClear.Visible = False
        '= cmdPrevGoods.Visible = False
        cboPrevGoods.Visible = False

        labGoodsInfoRequired.Text = "Check On-Site Address"
        '==txtGoodsList.Text = "ON-SITE JOB"

        LabOtherGoods.Text = "Check/Update Site Address"
        FrameUsers.Enabled = True

        frameProblem.Enabled = True '--can continue..-
        cmdNext.Enabled = True
        _SSTabNewJob_TabPage1.Enabled = True
        cmdNavBackToStart.Enabled = True
        labLogonRequired.Visible = False

        labOnSiteHdrP3.Visible = True

        date1 = DateTime.Today

        labOnSiteTime.Visible = True
        timeOnSite.Visible = True
        '= msPriorityText = "-ON-SITE JOB-"
        timeOnSite.CustomFormat = "h:mm tt"
        timeOnSite.Value = Now.AddDays(1)
        labOnSiteFields.Visible = True
        '=3501.1105= 05Nov2018=
        labOnSiteDuration.Visible = True
        labOnSiteDurationWarning.Visible = True
        numUpDownOnSiteDuration.Visible = True

        mbOnSiteSetup = True
    End Function  '--mbOnSiteSetup--
    '= = = = = = = = = = = = = = = = =
    '---===FF->

    '-- L o a d --
    '-- L o a d --

    Private Sub frmNewJob3_Load(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim s1, s2, s3, s4 As String
        Dim idx, ix, sx, lngError, L1 As Integer
        Dim sDay, sName As String
        Dim intDefaultPrinterIndex As Integer
        Dim colPrinters As Collection

        Call mbOriginal_frmNewJob3_Load()
        Call CenterForm(Me)
        cmdNext.Enabled = False

        sDay = VB.Left(msDayOfWeek(CDate(DateTime.Today)), 3)
        s1 = sDay & ", " & VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy") & " - " & VB6.Format(TimeOfDay, "hh:mm")
        LabDateRcvd.Text = s1

        LabVersion.Text = "JobMatix- V:" & CStr(My.Application.Info.Version.Major) & "." & _
                                           My.Application.Info.Version.Minor & "- Build: " & _
                                               My.Application.Info.Version.Build & "." & _
                                                    My.Application.Info.Version.Revision & "="
        labOnSiteTime.Visible = False
        timeOnSite.Visible = False
        labOnSiteHdrP3.Visible = False
        '=3501.1105= 05Nov2018=
        labOnSiteDuration.Visible = False
        labOnSiteDurationWarning.Visible = False
        numUpDownOnSiteDuration.Visible = False

        grpBoxItemPic.Enabled = False

        '== MUST create Attachments class to save new job pic if any..
        '-- But must have Class to be able to INSERT Picure with New JOB.
        '-- NO User Attachment Controls for NEW JOB..
        grpBoxItemPic.Text = "Add/Update Picture.."
        Try
            mClsAttachments1 = New clsAttachments(Me, mCnnJobs, "JOB", openDlg1)
        Catch ex As Exception
            MsgBox("ERROR creating new clsAttachments." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Me.Close()
        End Try

        '-- If AMEND..  Find any prev pic and load it..
        If (mlJobId > 0) Then  '--job exists..-
            Dim intDoc_id As Integer
            Dim sFileTitle As String
            Dim byteImageBytes() As Byte
            Dim ms As System.IO.MemoryStream

            grpBoxBooking.Visible = False   '=3203.123=--don't need this.. only for NewJob.
            picSubjectMain.Visible = True
            picSubjectMain.Left = 439
            picSubjectMain.Top = 1
            picSubjectMain.Width = 77
            picSubjectMain.Height = 71
            '-- Have a JOB.. show image if any. on 1st Panel.
            If Not mClsAttachments1.GetFirstImage(mlJobId, intDoc_id, sFileTitle, byteImageBytes) Then
                '= NONE=  MsgBox("Failed to retieve image..", MsgBoxStyle.Information)
            Else '-ok-
                ms = New System.IO.MemoryStream(byteImageBytes)
                Dim image1 As Image = System.Drawing.Image.FromStream(ms)
                picSubjectItem.Image = image1
                picSubjectMain.Image = image1  '=3203.101=
                ms.Close()
                '=picSubjectItem.Image = byteImageBytes
                mIntDoc_id = intDoc_id     '--save for updating if needed.
            End If  '-get-
        End If  '- job exists.-

        '-- Combo for Colour printer..
        '=3203.106= get printers.
        cboColourPrinters.Items.Clear()   '--Colour printer..
        cboReceiptPrinters.Items.Clear()
        cboLabelPrinters.Items.Clear()

        msLocalSettingsPath = gsLocalJobsSettingsPath()
        mLocalSettings1 = New clsLocalSettings(msLocalSettingsPath)

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            '--  load all combos with printers list.
            For Each sName In colPrinters
                cboColourPrinters.Items.Add(sName)
                cboReceiptPrinters.Items.Add(sName)
                cboLabelPrinters.Items.Add(sName)
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

            '-- C. check local settings (prefs) for LABEL printer..
            If mLocalSettings1.queryLocalSetting(gK_SETTING_PRTLABEL, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--pref. defined, so set it- 
                    cboLabelPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboLabelPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboLabelPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            msLabelPrinterName = cboLabelPrinters.SelectedItem

        End If '-getAvail.-  

        '=3203.117=
        '-- Reset optRet and Warranty colours-
        chkReturned.BackColor = Color.LavenderBlush
        chkReturned.Checked = False
        chkSystemUnderWarranty.BackColor = Color.LavenderBlush
        chkSystemUnderWarranty.ForeColor = Color.Black
        chkSystemUnderWarranty.Checked = False

        '=3431.0505=
        '-- -results-
        msExchangeCalendarUpdateXmlFileName = ""

        '=3431.0527- Limit on problemLong.
        Dim colTable As Collection = mColSqlDBInfo.Item("jobs")
        Dim colFields As Collection = colTable.Item("FIELDS")
        If colFields.Contains("ProblemLong") Then  '-expand to 4000 if not done..
            '--intMaxTxtProblem=
            mIntMaxTxtProblem = CInt(colFields.Item("ProblemLong")("CHAR_MAX_LENGTH"))
        End If  '-problemLong-
        txtProblem.MaxLength = mIntMaxTxtProblem
        '-TEST-
        '= MessageBox.Show("INFO only. Max problemLong is: " & txtProblem.MaxLength, _
        '=                   "New Job startup.", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub  '--load==
    '= = = = = = = = = = = = = = = = =
    '---===FF->


    '-- =3083= A c t i v a t e Restored..--
    '-- =3083= A c t i v a t e Restored..--
    '= 3431.0503=  Move activated stuff to Shown event..

    Private Sub frmNewJob3_Activated(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        If mbActive Then Exit Sub '--re-entered after every child form..--
        mbActive = True


    End Sub  '-activated-
    '= = = = = = = = = = =


    Private Sub frmNewJob3_Shown(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles MyBase.Shown

        Dim s1, s2, s3, s4 As String
        Dim idx, ix, sx, lngError, L1 As Integer
        Dim sDay As String
        Dim sShortDate As String
        Dim colItem1 As Collection
        Dim item1 As System.Windows.Forms.ListViewItem
        '== Dim sName As String
        Dim v1 As Object
        Dim aUsers, aPwds As Object
        Dim colJobGoods As Collection
        Dim colGoodsInCare As Collection
        Dim colCustFields As Collection
        Dim date1, date2 As Date

        '= If mbActive Then Exit Sub '--re-entered after every child form..--
        '= mbActive = True
        '=--  load symptoms..-
        Call mbLoadRefSymptoms()

        '--  load system info..--
        mClsSystemInfo = New clsSystemInfo(mCnnJobs)

        '--=3072= 08Feb2013=  Get business info..--
        msABN = mClsSystemInfo.item("BUSINESSABN")
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

        If mClsSystemInfo.exists("NEWJOBDOCKETFOOTNOTE") Then
            msNewJobDocketFootnote = mClsSystemInfo.item("NEWJOBDOCKETFOOTNOTE")
        End If
        If mClsSystemInfo.exists("SERVICECHARGESINFOTEXT") Then
            msServiceChargesInfoText = mClsSystemInfo.item("SERVICECHARGESINFOTEXT")
        End If
        msTermsText = mClsSystemInfo.item("TERMSANDCONDITIONS")
        msLabelBarcodeFontName = mClsSystemInfo.item("ITEMBARCODEFONTNAME")
        s1 = mClsSystemInfo.item("ITEMBARCODEFONTSIZE")
        If IsNumeric(s1) Then
            L1 = CInt(s1)
            If (L1 > 3) And (L1 < 36) Then
                mlLabelBarcodeFontSize = L1
            End If
        End If
        '--=3072= 08Feb2013= D o n e ==
        LabHdr2.Text = msBusinessName & vbCrLf & msBusinessState & " " & msBusinessPostCode

        '=3501.1105-
        mbByPassLaptopChargerCheck = False
        If mClsSystemInfo.exists("BYPASS_LAPTOP_CHARGER_CHECK") Then
            s1 = mClsSystemInfo.item("BYPASS_LAPTOP_CHARGER_CHECK")
            If (UCase(s1) = "Y") Or (UCase(s1) = "YES") Then
                mbByPassLaptopChargerCheck = True
            End If
        End If

        '== 3067.0 == If (listSymptoms.Items.Count <= 0) Then cmdAddSymptom.Enabled = False
        '== 3067.0 == 
        '==3067.0== cmdAddSymptom.Enabled = False
        '--txtRcvdName.SetFocus '--  must enter staff no..--
        txtRcvdName.Text = msStaffName
        txtRcvdName.ReadOnly = True
        If msStaffName = "" Then
            MsgBox("staff Id must be entered..", MsgBoxStyle.Exclamation)
            Me.Hide()
            Exit Sub
        End If
        '-- load priorities from SystemInfo..--
        If gbGetPriorityDescriptorsEx(mCnnJobs, mbIsOnSiteJob, mRetailHost1, mColPriorities) Then
            '=gbGetPriorityDescriptors(mCnnJobs, mColPriorities) Then
            For Each v1 In mColPriorities
                cboPriority.Items.Add(CStr(v1))
            Next v1 '--v1-
        End If
        cboPriority.SelectedIndex = -1 '--not selected..-
        cboPriority.Enabled = False
        '-- has previous form..--
        cboPrevGoods.Items.Clear()
        '==30571.= Load Previous Jobs Goods if any..--
        If (mColCustomerJobsGoods IsNot Nothing) AndAlso (mColCustomerJobsGoods.Count > 0) Then
            mColCustomerPrevGoodsFlat = New Collection
            For idx = 1 To mColCustomerJobsGoods.Count
                colJobGoods = mColCustomerJobsGoods(idx)
                s1 = colJobGoods("Job_Id")
                s2 = colJobGoods("DateUpdated")
                colGoodsInCare = colJobGoods("ColGoodsInCare")
                For Each colItem1 In colGoodsInCare
                    '--  ADD to flattened corresponding collection..-
                    mColCustomerPrevGoodsFlat.Add(colItem1)   '--matches combo.-
                    cboPrevGoods.Items.Add("Job #" & s1 & ": " & s2 & _
                                           ": " & colItem1("Type") & "; " & colItem1("Brand") & _
                                           "; " & colItem1("Model") & "; " & colItem1("SerialNo"))
                Next '--item-
            Next idx '-job-
            '= For Each colJobGoods In mColCustomerJobsGoods
            '== Next '--job-
            '== cmdPrevGoods.Enabled = True
            '== cboPrevGoods.Visible = True
        End If
        '-- set up prev goods..==
        If (mlJobId > 0) Then '-- CHECK-IN (OR AMEND) existing Job..-
            '=3107.904=- FIRST- get Job record-
            '==  NO Transaction will be used-..  If Not mbGetJobRecord(mlJobId, True, mColJobFields) Then
            If Not mbGetJobRecord(mlJobId, False, mColJobFields) Then
                Me.Hide()
                Exit Sub
            End If
            msGoodsInCare = Trim(mColJobFields.Item("GoodsInCare")("value"))
            msOriginalGoodsInCare = msGoodsInCare   '-3501.0814- Save in case changes.
            '==done 904=
            If (InStr(UCase(msGoodsInCare), K_GOODS_ONSITEJOB) > 0) Then
                mbIsOnSiteJob = True
            End If
        Else  '-new job- ONSITE has been set by newjob caller..
            '== picSubjectItem.Visible = False   '--need space for Bookin Option..-
            picSubjectMain.Visible = False   '--need space for Bookin Option..-
            grpBoxBooking.Text = "Book In"
        End If
        If mbIsOnSiteJob Then
            Call mbOnSiteSetup()
        Else  '--normal job-
            labSelectPrevious.Visible = True
            labSelectPrevious.Enabled = False
            cboPrevGoods.Visible = True
            If cboPrevGoods.Items.Count > 0 Then
                labSelectPrevious.Enabled = True
                cboPrevGoods.Enabled = True
            End If
            grpBoxItemPic.Enabled = True
        End If  '--onsite-

        '-ok..  Finish setup..--
        '-ok..  Finish setup..--
        txtSymptoms.ReadOnly = True
        If (mlJobId > 0) Then '-- CHECK-IN (OR AMEND) existing Job..-
            '--Retrieve indicated Job..--
            '--  load/show some data..-
            mbAmending = True '-- Means CHECK-IN..--
            cmdNext.Enabled = True
            '== LabHdr1.BackStyle = 1 '--opaque..-
            LabHdr1.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '--yellowish..--
            LabJobStatus.Visible = True
            '==If Not mbGetJobRecord(mlJobId, False, mColJobFields) Then

            '=3101= OleDb-- Should lock for update-
            '=3107.904='==  NO Transaction will be used-..  If Not mbGetJobRecord(mlJobId, True, mColJobFields) Then
            '=3107.904=If Not mbGetJobRecord(mlJobId, False, mColJobFields) Then
            '=3107.904=Me.Hide()
            '=3107.904=Exit Sub
            '=3107.904=End If
            '==3101= save dateUpdated-
            mDateOriginalTimeStamp = CDate(mColJobFields.Item("DateCreated")("value"))
            txtRcvdName.Text = Trim(mColJobFields.Item("RcvdStaffName")("value"))
            LabRcvdBy.Text = "Created By:"
            LabDateRcvd.Text = VB6.Format(CDate(mColJobFields.Item("DateCreated")("value")), "dd-mmm-yyyy")
            '===LabHelpCustomer.Enabled = False
            '== LabEnterCust.Enabled = False
            mlCustomerId = CInt(mColJobFields.Item("RMCustomer_Id")("value"))
            '== txtCustCompany.Text = mColJobFields("CustomerCompany")("value")
            msCustomerCompany = mColJobFields.Item("CustomerCompany")("value") '== txtCustCompany.Text
            '== txtCustName.Text = mColJobFields("CustomerName")("value")
            msCustomerName = mColJobFields.Item("CustomerName")("value") '== txtCustName.Text
            mlCustomerId = CInt(mColJobFields.Item("RMCustomer_Id")("value"))
            msCustomerBarcode = Trim(mColJobFields.Item("CustomerBarcode")("value"))
            msCustomerPhone = mColJobFields.Item("CustomerPhone")("value") '== txtCustPhone.Text
            '== txtCustMobile.Text = mColJobFields("CustomerMobile")("value")
            msCustomerMobile = mColJobFields.Item("CustomerMobile")("value") '=--txtCustMobile.Text
            txtCustomer.Text = "[" & msCustomerBarcode & "]  " & msCustomerCompany & vbCrLf & _
                              msCustomerName & vbCrLf & msCustomerPhone & " [" & msCustomerMobile & "]"

            chkRefreshCustomer.Enabled = True

            '====txtNotification.Text = mColJobFields("notifications")("value")
            msJobOriginalStatus = mColJobFields.Item("jobstatus")("value")
            LabJobStatus.Text = "Job Status: " & vbCrLf & msJobOriginalStatus
            LabJobStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '--yellowish..--
            '==chkReturned_deleted.CheckState = IIf((UCase(mColJobFields.Item("JobReturned")("value")) = "Y"), 1, 0)
            '=3203.117=
            If mColJobFields.Item("SystemUnderWarranty")("value") <> 0 Then
                chkSystemUnderWarranty.Checked = True
                chkSystemUnderWarranty.BackColor = ColorTranslator.FromOle(gIntUnderWarrantyColour())
            End If
            If (UCase(mColJobFields.Item("JobReturned")("value")) = "Y") Then
                chkReturned.Checked = True
                '= optReturned.Checked = IIf((UCase(mColJobFields.Item("JobReturned")("value")) = "Y"), 1, 0)
                chkReturned.BackColor = Color.Crimson '=3203.117=   chkReturned_deleted.BackColor = Color.Crimson
            End If
            '=3107=904= msGoodsInCare = Trim(mColJobFields.Item("GoodsInCare")("value"))
            '==3107.707==
            If UCase(msGoodsInCare) = K_GOODS_ONSITEJOB Then
                mbIsOnSiteJob = True
            End If
            s1 = UCase(mColJobFields.Item("Priority")("value"))
            '=======  LabPriority = IIf((s1 = "H"), "HOME", "BUSINESS")
            cboPriority.Enabled = True
            If (s1 = "Q") Then
                MsgBox("Can't amend a QUOTATION JOB..", MsgBoxStyle.Exclamation)
                Me.Close()    '==Me.Hide()
                Exit Sub
                '== ElseIf (s1 = "S") Then  '--OnSite.-
                '== cboPriority.Enabled = False
                '== mbIsOnSiteJob = True
                '== Call mbOnSiteSetup()
            ElseIf (s1 = "H") Or (s1 = "1") Then  '-"home".-
                '==LabTicket.BackColor = &HFF00FF
                '==optCustPriority(0).Value = True
                cboPriority.SelectedIndex = 0
            ElseIf (s1 = "B") Or (s1 = "2") Then  '-- business.--
                '==LabTicket.BackColor = vbBlue
                '===optCustPriority(1).Value = True
                cboPriority.SelectedIndex = 1
            Else '--3--
                '==optCustPriority(2).Value = True
                cboPriority.SelectedIndex = 2
            End If
            Call miGetPriority() '-- set colour.-(incl on-site-)
            '=   If mbIsOnSiteJob Then
            '=     LabTicket.BackColor = Color.Goldenrod
            '=    LabTicket.ForeColor = Color.Black
            '=  Else
            LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(mlPriorityColour)
            LabTicket.ForeColor = mColorPriorityFG
            '=  End If

            '===ChkReserved.Visible = False
            txtTechName.Text = Trim(mColJobFields.Item("NominatedTech")("value"))
            '--lookup Tech barcode..--
            If (txtTechName.Text <> "") Then
                '==  NOT LEGIT to try and lookup staff using docket-name..--

                '== s1 = txtTechName.Text
                '== If mbLookupStaffName(s1, colFields) Then    '--ok-
                '==      txtNomTech.Text = colFields("barcode")("value")
                '== End If
            End If
            sShortDate = VB6.Format(CDate(mColJobFields.Item("DateCreated")("value")), "dd-mmm-yyyy")
            '====LabTicket.Caption = Mid(sShortDate, 4, 4) + Right(sShortDate, 2) + vbCrLf + Format(mlJobId, "  000")

            '=3203.119- REFORMAT-  LabTicket.Text = Mid(sShortDate, 4, 4) & VB.Right(sShortDate, 2) & ":" & VB6.Format(mlJobId, "  000")
            LabTicket.Text = VB6.Format(mlJobId, "  000") & ": " & Mid(sShortDate, 4, 8)  '== eg  "Sep-2016"--

            txtGoodsList.Text = msShowGoodsInCare(msGoodsInCare) & vbCrLf  '== 3072 ==

            cmdCheckGoods.Enabled = True
            msPrintGoodsInCare = msGoodsInCare

            '--=3072/3= NEW for Extras in care--
            msExtrasInCare = Trim(mColJobFields.Item("GoodsExtras")("value"))
            '--If not waitlisted, no extras yet..-  Can still Check them.-
            If (VB.Left(msJobOriginalStatus, 2) >= "10") Then
                For ix = 0 To (k_maxExtrasInCare - 1)
                    chkExtras(ix).Enabled = True
                Next ix
            End If  '--waitlisted-
            Call mbCheckSelectedExtrasIncare(msExtrasInCare)
            '--=3072/3= END new Extras in care--

            txtGoodsOther.Text = mColJobFields.Item("GoodsOther")("value")

            '--show usernames if they were supplied.-
            '--  temp disable switches..-
            '=3519.0414-
            '--  Safety Check to stop crash if they put slashes in the passwords..
            '=4201.1007-  Update.. Drop Trailing slashes first,
            '-    AND don't clear the passwoeds..
            '-      NB..  Two trailng slashes are normal when there is only one user/pwd.
            mbInvalidPasswordDataOnStartup = False
            OptLogin(0).Enabled = False : OptLogin(1).Enabled = False
            ChkUsers.Enabled = False
            s1 = Trim(mColJobFields.Item("Username")("value"))
            s2 = Trim(mColJobFields.Item("UserPassword")("value"))
            '--drop trailing slashes..
            While (VB.Right(s1, 1) = "/")
                s1 = VB.Left(s1, (Len(s1) - 1))
            End While
            While (VB.Right(s2, 1) = "/")
                s2 = VB.Left(s2, (Len(s2) - 1))
            End While
            '--
            If (s1 <> "") Then '-- usernames, display..-
                OptLogin(0).Checked = True
                aUsers = Split(s1, "/")
                aPwds = Split(s2, "/")
                If (Not IsNothing(aUsers)) And (Not IsNothing(aPwds)) Then '--ok-
                    '=3519.0414-  Big Test..
                    If (InStr(s1, "//") > 0) Or (InStr(s2, "//") > 0) Or _
                                               (aUsers.length <> aPwds.length) Then
                        '-- too many because of slashes as names or passwords.
                        MsgBox("Please Note:" & vbCrLf & _
                               " The User names or passwords that were set up can not be successfully parsed." & vbCrLf & _
                               "    They are-  UserName: " & s1 & "  Pwds: " & s2 & "" & vbCrLf & _
                               " These will be left as they are, as one name and one pwd..." & vbCrLf &
                               "   (Note- Slash chars in names or pwds are not allowed here..)", MsgBoxStyle.Information)
                        '= Call mSetDataChanged()
                        mbInvalidPasswordDataOnStartup = True  '-see below-  MOT reall relevant now..
                        txtUserName(0).Enabled = True
                        txtUserName(0).Text = Trim(mColJobFields.Item("Username")("value"))
                        txtPassword(0).Enabled = True
                        txtPassword(0).Text = Trim(mColJobFields.Item("UserPassword")("value"))
                    Else  '-ok=
                        For ix = 0 To UBound(aUsers)
                            txtUserName(ix).Enabled = True
                            txtUserName(ix).Text = Trim(aUsers(ix))
                        Next ix
                        If (s2 <> "") Then
                            '=aPwds = Split(s2, "/")
                            If (Not IsNothing(aPwds)) Then '--ok-
                                For ix = 0 To UBound(aPwds)
                                    txtPassword(ix).Enabled = True
                                    txtPassword(ix).Text = Trim(aPwds(ix))
                                Next ix
                            End If '--empty-
                        End If '--pwds..-
                    End If  '-big test-
                End If '--users nothing--
            Else
                OptLogin(1).Checked = True '-- no logon.--
            End If '--usernames..-
            ChkUsers.CheckState = IIf((UCase(mColJobFields.Item("MultiAccounts")("value")) = "Y"), 1, 0)
            '--  ENABLE  switches..-
            OptLogin(0).Enabled = True : OptLogin(1).Enabled = True
            ChkUsers.Enabled = True

            '==3067.0 ==cmdEditSymptoms.Visible = False
            '===cmdAddSymptom.Visible = False
            txtSymptoms.Text = Trim(mColJobFields.Item("ProblemSymptoms")("value"))
            Call mbCheckSelectedSymptoms(txtSymptoms.Text)

            txtSymptoms.ReadOnly = True
            txtProblem.Text = VB.Right(mColJobFields.Item("ProblemLong")("value"), mIntMaxTxtProblem) '=3431.0527=
            LabCostLimit.Text = ""
            '--  ProblemShort used to hold flags..--
            msProblemShort = mColJobFields.Item("ProblemShort")("value")
            '--   eg. *QUOTATION-REQUIRED*  or *PROCEED-WITH-SERVICE*---
            If (InStr(UCase(msProblemShort), "*QUOTATION-REQUIRED*") > 0) Then
                optQuotation(0).Checked = True
                LabCostLimit.BackColor = System.Drawing.Color.Yellow
                '==LabCostLimit.Enabled = False
                LabCostLimit.Text = "QUOTATION REQUIRED:" & vbCrLf & "Minimum Charge of " & _
                                                                      FormatCurrency(mCurLabourMinCharge, 2) & " applies.."
            ElseIf (InStr(UCase(msProblemShort), "*PROCEED-WITH-SERVICE*") > 0) Then
                optQuotation(1).Checked = True
                LabCostLimit.BackColor = System.Drawing.Color.Lime
                LabCostLimit.Text = "Proceed ok with agreed Service.. "
                '==LabCostLimit.Enabled = True
            ElseIf (InStr(UCase(msProblemShort), "*PROCEED-TO-LIMIT*") > 0) Then
                optQuotation(2).Checked = True
                LabCostLimit.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFC0) '-- light Green--
                LabCostLimit.Text = Replace(msBusinessShortName, "_", " ") & _
                                     " to notify Customer if cost can exceed " & FormatCurrency(mCurNotificationCostLimit, 2)
            End If
            '-- now can enable..-
            optQuotation(0).Enabled = True
            optQuotation(1).Enabled = True
            optQuotation(2).Enabled = True

            '== FrameCust.Enabled = False  '--can't change customer..--
            '== 3053.0 ==  FrameGoods.Enabled = True
            '== 3053.1 ==  
            FrameGoods.Enabled = True '== 3053.1 ==RE-INSTATE !!  --
            FrameUsers.Enabled = True '--  allow change logon..--
            '== frameNominTech.Enabled = True
            frameProblem.Enabled = True '--can continue..-
            cmdNext.Enabled = True
            _SSTabNewJob_TabPage1.Enabled = True
            _SSTabNewJob_TabPage2.Enabled = True
            cmdNavBackToStart.Enabled = True

            frameInstructions.Enabled = True '--  allow problem notes to be updated..--
            '-- setup checkboxes..--
            ChkBackupReq.CheckState = IIf((UCase(mColJobFields.Item("DataBackupReqd")("value")) = "Y"), 1, 0)
            ChkRecovDisk.CheckState = IIf((UCase(mColJobFields.Item("DataDiskReqd")("value")) = "Y"), 1, 0)
            '=======SSTab1.Tab = k_SSTAB_CUSTOMER
            '====txtGoodsOther.SetFocus
            '-- Check if we are stll WaitListed-only.--
            '===txtBookingHdr.Text = "Original Status"
            '==cmdPrintAll.Enabled = True
            cmdFinish.Enabled = False
            cmdCancel.Text = "Exit"
            If (VB.Left(msJobOriginalStatus, 2) = "05") And mbIsCheckIn Then '--CheckIn booked job..-
                '===LabShopReady.Visible = True
                Me.Text = "Check-in Job No. " & mlJobId
                LabHdr1.Text = "Service Agreement (Check-In)"
                LabHelpStatus.BackColor = System.Drawing.ColorTranslator.FromOle(&HC1C8FF) '-- &HC0C0FF     '--pink..-
                LabHelpStatus.Text = "Goods for This Job are now being checked-in... " & vbCrLf & _
                                                                 "All Job details can be verified..."
                For ix = 0 To (k_maxExtrasInCare - 1)
                    chkExtras(ix).Enabled = True
                Next ix
                '==GoSub Activate_EnablePrinting
                Call mActivate_EnablePrinting()

                '== Rev-2804==
                cmdPrintAll.Enabled = False '--True==  Can't print until saved.--
                cmdCancel.Text = "Cancel"
                cmdFinish.Text = "Check-In   && Print"
                cmdFinish.Enabled = True
            ElseIf (VB.Left(msJobOriginalStatus, 2) = "05") Then
                Me.Text = "Amending Job No. " & mlJobId
                LabHdr1.Text = "Service Booking (Amending)"
                LabHelpStatus.Text = "This Job is still Wait-listed.. " & vbCrLf & _
                                                                  "Job details can be amended.."
                cmdFinish.Text = "Save.."
            Else '- "10"---Goods were prev. received..  carry on as normal...
                '===FrameBooking.Visible = False
                Me.Text = "Amending Job No. " & mlJobId
                LabHdr1.Text = "Service Agreement (Amending)"
                '== LabHelpStatus.BackColor = System.Drawing.Color.Lime
                LabHelpStatus.ForeColor = Color.Maroon
                If (VB.Left(msJobOriginalStatus, 2) = "10") Then
                    LabHelpStatus.Text = "This Job is ready to start.." & vbCrLf & "You can still amend some details..."
                ElseIf (VB.Left(msJobOriginalStatus, 2) >= "20") And (VB.Left(msJobOriginalStatus, 2) < "30") Then
                    LabHelpStatus.Text = "This Job is currently suspended.." & vbCrLf & _
                                                                "But some details can be amended..."
                Else
                    LabHelpStatus.Text = "This Job has been started.." & vbCrLf & _
                                                            "But some details can be amended..."
                End If
                '== GoSub Activate_EnablePrinting
                Call mActivate_EnablePrinting()
                cmdPrintAll.Enabled = True
            End If '--booking..-
            If mbIsOnSiteJob Then
                LabHelpStatus.Text = "ON-SITE Job.." & vbCrLf & _
                      "Check/Update ON-SITE Customer Address and proceed.." & vbCrLf & _
                         "Some details can be amended...."
                chkPrtDocs(k_PRINT_AGREEMENT).CheckState = System.Windows.Forms.CheckState.Unchecked  '-no.-
                chkPrtDocs(k_PRINT_LABEL).CheckState = System.Windows.Forms.CheckState.Unchecked '--not checked..--
                chkPrtDocs(k_PRINT_RECEIPT).CheckState = System.Windows.Forms.CheckState.Unchecked  '-no.-
            End If
            '==3083== datePromised==
            '==3083== datePromised==
            '== datePromised shouldn't ever be less than this..== 
            '--  Make it midnight, so promised time will be greater..-
            datePromised.MinDate = CDate(VB6.Format(mColJobFields.Item("DateCreated")("value"), "dd-mmm-yyyy") & " 00:00:00")

            '--  NOTE: If not actually promised then DatePromised column is way in future..
            If (Not IsDBNull(mColJobFields.Item("DatePromised")("value"))) AndAlso _
                               IsDate(mColJobFields.Item("DatePromised")("value")) Then
                date1 = CDate(mColJobFields.Item("DatePromised")("value"))
                '=3403.604=  SAVE Original date promised.
                mDatePromisedOriginal = date1

                If (DateDiff(DateInterval.Month, Now, date1) < 12) Then  '--was promised within a year.-
                    datePromised.CustomFormat = "ddd dd-MMM-yyyy"
                    datePromised.Checked = True
                    datePromised.Value = date1  '--show promised date..-
                    '- if OnSite..  then set timeBooked picker.
                    If mbIsOnSiteJob Then
                        '== datePromised.MinDate = CDate(mColJobFields.Item("DateCreated")("value"))
                        labOnSiteTime.Visible = True
                        timeOnSite.Visible = True
                        timeOnSite.Value = date1  '-- TimeValue(date1) '--shows time only.-
                        timeOnSite.CustomFormat = "h:mm tt"
                        '=3501.1105-
                        labOnSiteDuration.Visible = True
                        labOnSiteDurationWarning.Visible = True
                        numUpDownOnSiteDuration.Visible = True
                        numUpDownOnSiteDuration.Value = 2   '-- default 2 hours.
                    End If
                Else '--not promised
                    datePromised.Checked = False
                    datePromised.MinDate = DateTime.Today
                    datePromised.CustomFormat = " "
                End If
            Else  '--no date in column--
                datePromised.Checked = False
                datePromised.CustomFormat = " "
            End If
            '-- else is New job Button..--
        Else '-- NEW JOB Button..--
            '-- New job Button..--
            '-- New job Button..--
            cmdCancelJob.Visible = False
            '== FrameCust.Enabled = True

            '--  NEW Job must have customer before calling..--
            If msCustomerBarcode = "" Then
                MsgBox("Please choose customer before calling New Job..", MsgBoxStyle.Exclamation)
                Me.Hide()
                Exit Sub
            End If
            txtCustomer.Text = "[" & msCustomerBarcode & "]  " & msCustomerCompany & vbCrLf & _
                               msCustomerName & vbCrLf & msCustomerPhone & " [" & msCustomerMobile & "]"
            '==3311.410= datePromised = DateAdd(DateInterval.Hour, 1, DateTime.Now)
            datePromised.Value = DateAdd(DateInterval.Hour, 1, DateTime.Now)  '=DateTime.Now
            datePromised.Checked = False  '--disabled to start off..--
            datePromised.CustomFormat = " "
            If mbIsBooking Then
                '=3203.123= --WILL BE FALSE.. THIS NOT DECIDED YET--
                LabHelpStatus.Text = "Booking for New Job (for Wait-listing)." & vbCrLf & _
                                   " Enter Goods Details, and proceed." & vbCrLf & _
                                      "Visit panels in turn to finish form."
                LabHdr1.Text = "Service Booking"
                cmdFinish.Text = "Save.."
                '==txtBookingHdr.Text = "Job will be WAIT-LISTED.."
            ElseIf mbIsOnSiteJob Then
                LabHelpStatus.Text = "Booking for an ON-SITE Job.." & vbCrLf & _
                                    "Check/Update ON-SITE Customer Address..  " & _
                                       " - Visit all panels to finish form."
                LabHelpStatus.BackColor = Color.Goldenrod
                '== Call mbOnSiteSetup()
                txtGoodsList.Text = K_GOODS_ONSITEJOB
                msGoodsInCare = txtGoodsList.Text

                '== grh = 3083.401 == 01-Apr-2014 == FIX day overflow..
                '== OVERFLOWS !! =  date1 = New DateTime(Today.Year, Today.Month, (Today.Day + 1), 10, 0, 0)
                '--  == FIX day overflow..
                datePromised.Checked = True
                '==3311.410=date1 = DateAdd(DateInterval.Day, 1, Today)  '-- make tomorrow--
                '==3311.410=date2 = New DateTime(DatePart(DateInterval.Year, date1), DatePart(DateInterval.Month, date1), _
                '==3311.410=                                                     DatePart(DateInterval.Day, date1), 10, 0, 0)
                '==3311.410=datePromised.Value = date2
                '==3311.410= datePromised = DateAdd(DateInterval.Hour, 1, DateTime.Now)
                datePromised.Value = DateAdd(DateInterval.Hour, 1, DateTime.Now)  '=DateTime.Now
                '=3311.410=
                timeOnSite.Value = datePromised.Value  '= date2  '-- TimeValue(date1) '--shows time only.-
                '--  set up address..--
                If mRetailHost1.customerGetCustomerRecord(msCustomerBarcode, mlCustomerId, colCustFields) Then '--found..--
                    Call mbSetupCustomer(False, colCustFields) '--don't check for wait-listed jobs..-
                    mbCustomerRefreshed = True
                Else '--not found-
                    MsgBox("Cust. not found-  Barcode: " & msCustomerBarcode & vbCrLf & _
                            "and Customer Id: " & mlCustomerId & " not found..", MsgBoxStyle.Exclamation, "Refresh Customer..")
                End If
                '== txtGoodsList.Text = "ON-SITE JOB"
                txtGoodsOther.Text = "ON-SITE ADDRESS:" & vbCrLf & msCustomerAddress
                Call mActivate_EnablePrinting()
                '-- untick printing..
                chkPrtDocs(k_PRINT_AGREEMENT).CheckState = System.Windows.Forms.CheckState.Unchecked  '-no.-
                chkPrtDocs(k_PRINT_LABEL).CheckState = System.Windows.Forms.CheckState.Unchecked '--not checked..--
                chkPrtDocs(k_PRINT_RECEIPT).CheckState = System.Windows.Forms.CheckState.Unchecked  '-no.-
            Else
                LabHelpStatus.Text = "New Workshop Job (Goods being Checked-in)." & vbCrLf & _
                                   " Enter Goods Details, select Windows Logon/No Logon." & vbCrLf & _
                                      "Visit all panels in turn to finish form."
                For ix = 0 To (k_maxExtrasInCare - 1)
                    chkExtras(ix).Enabled = True
                Next ix
                '== GoSub Activate_EnablePrinting
                Call mActivate_EnablePrinting()
            End If
            Call miGetPriority() '=3311.410= Just for ON-SIE/Returned color- No priority selected. 
            LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(mlPriorityColour)
            '=3203.119--
            LabTicket.ForeColor = mColorPriorityFG

            optQuotation(0).Enabled = True
            optQuotation(1).Enabled = True
            optQuotation(2).Enabled = True

            cmdPrintAll.Enabled = False '--Was turned on by clicking check-boxes..-

            '--  CAN PROCEED..-
            cmdFinish.Enabled = False '--probably got clicked on..
            FrameGoods.Enabled = True
            cmdCheckGoods.Enabled = True
            '== cmdCheckGoods.Focus()
            '-- 3083--
            '==datePromised.MinDate = DateTime.Today
            '==datePromised.Format = DateTimePickerFormat.Short

        End If '--new job/amend..-
        '=4201.1007-  Update.. Drop Trailing slashes first,
        '-    AND don't clear the passwoeds..
        '-      NB..  Two trailng slashes are normal when there is only one user/pwd.

        'If mbInvalidPasswordDataOnStartup Then
        '    Call mSetDataChanged()
        '    '= cmdCancel.Text = "Cancel"  '-done in mSetDataChanged
        'Else
        mbDataChanged = False '--probably got clicked on..
        'End If

        Application.DoEvents()
        '--  focus on add goods.--
        If mbIsOnSiteJob Then
            '== msPriorityText = "-ON-SITE JOB-"
            cmdNext.Enabled = True
            cmdNext.Select()
        Else
            cmdCheckGoods.Select()
        End If

        '=3083.717=
        mbNumUpDownLabels_Updating = True   '--"disable" while we change it.
        NumUpDownLabels.Value = mIntNumberOfLabels()
        mbNumUpDownLabels_Updating = False   '-- free..

        mbUserChangedNumberOfLabels = False
        mbFormStarting = False  '--startup done.-
        Exit Sub

    End Sub '- Shown - -activate--
    '= = = = = = = = = =
    '-===FF->

    '-- Catch keyPress for form..--
    Private Sub frmNewJob3_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                     Handles MyBase.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        '-- MsgBox "Form KeyPress Event:  Key: " & keyAscii & " was pressed..", vbInformation

        If keyAscii = System.Windows.Forms.Keys.Tab Then '--TAB key..--

            '=3072== MsgBox("Form Event:  TAB pressed..", MsgBoxStyle.Information)
            '--keyAscii = 0 '--processed.-
        End If '--tab.-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = = = = = = = =

    '--got function key----
    '--- check for TAB--
    Private Sub frmNewJob3_KeyUp(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                          Handles MyBase.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        '--Dim lngActualSize As Long
        Dim lngControl As Integer
        Dim AltDown, ShiftDown, CtrlDown As Integer


        ShiftDown = (Shift And VB6.ShiftConstants.ShiftMask) > 0
        AltDown = (Shift And VB6.ShiftConstants.AltMask) > 0
        CtrlDown = (Shift And VB6.ShiftConstants.CtrlMask) > 0

        lngControl = (VB6.ShiftConstants.ShiftMask + VB6.ShiftConstants.AltMask + VB6.ShiftConstants.CtrlMask)

        '--sOrigKey = Trim(txtCustName.Text) '--save orig key--

        If (KeyCode = System.Windows.Forms.Keys.F2) And ((Shift And lngControl) = 0) Then '--lookupbrand--
            '=3072== MsgBox("Form KeyUp Event:  F2 pressed..", MsgBoxStyle.Information)
        ElseIf (KeyCode = System.Windows.Forms.Keys.Tab) Then  '--lookupbrand--

            '=3072== MsgBox("Form KeyUp Event:  TAB pressed..", MsgBoxStyle.Information)

        End If '--f2-

    End Sub '--F2-
    '= = = = = = = =
    '-===FF->

    '--Refresh Customer Details..-
    '--Refresh Customer Details..-

    'UPGRADE_WARNING: Event chkRefreshCustomer.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub chkRefreshCustomer_CheckStateChanged(ByVal eventSender As System.Object, _
                                                     ByVal eventArgs As System.EventArgs) _
                                                      Handles chkRefreshCustomer.CheckStateChanged
        Dim colFields As Collection

        If (chkRefreshCustomer.CheckState = 1) Then '--checked..
            '== If mbLookupCustomerId(mlCustomerId, colFields) Then
            If mRetailHost1.customerGetCustomerRecord(msCustomerBarcode, mlCustomerId, colFields) Then '--found..--
                Call mbSetupCustomer(False, colFields) '--don't check for wait-listed jobs..-
                mbCustomerRefreshed = True
                Call mSetDataChanged() '=== mbDataChanged = True
            Else '--not found-
                MsgBox("Cust. not found-  Barcode: " & msCustomerBarcode & vbCrLf & _
                        "and Customer Id: " & mlCustomerId & " not found..", MsgBoxStyle.Exclamation, "Refresh Customer..")
            End If
        End If '--checked..--

    End Sub '--refresh..-
    '= = = = = = = = =

    Private Sub chkBooking_CheckedChanged(sender As Object, ev As EventArgs) _
                                         Handles chkBooking.CheckedChanged

        '-- Booking-
        '=3311.410=-- Uncheck the printing options..

        If chkBooking.Checked Then
            LabHelpStatus.Text = "Job being booked in.."
            mbIsBooking = True
            '=3311.410=-- Uncheck the printing options..
            chkPrtDocs(k_PRINT_AGREEMENT).Checked = False
            chkPrtDocs(k_PRINT_RECEIPT).Checked = False
            chkPrtDocs(k_PRINT_LABEL).Checked = False

        Else  '-Accepting Job..
            LabHelpStatus.Text = "Job being Accepted in now...."
            mbIsBooking = False
        End If

    End Sub   '--chkBooking-
    '= = = = = = = = = = =  = = = =
    '-===FF->

    '--  RETURNED..-
    '-  THIS is GONE ====

    Private Sub ChkReturned_deleted_CheckStateChanged(ByVal eventSender As System.Object, _
                                                 ByVal eventArgs As System.EventArgs)
        '== Call miGetPriority()
        '== If chkReturned_deleted.Checked Then
        '==     chkReturned_deleted.BackColor = Color.Crimson
        '==     LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(mlPriorityColour)
        '== Else  '--unchecked--
        '==     chkReturned_deleted.BackColor = Color.LavenderBlush
        '==     '== mIntOriginalJobNo = -1
        '== End If  '--checked
        '== Call mSetDataChanged() '=== mbDataChanged = True
    End Sub '--returned.-
    '= = = = = = = = = = = = =
    Private Sub chkReturned_CheckedChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs)
    End Sub    ' ----
    '= = = = =  = = = = = =

    '=3203.117-- 
    '--  RETURNED and WARRANTY..-
    '-  Now are Radio buttons..

    '== Private Sub optReturned_CheckedChanged(sender As Object, ev As EventArgs)
    '== Dim opt1 As RadioButton = CType(sender, RadioButton)
    '=     Call miGetPriority()
    '==     If LCase(opt1.Name) = "optreturned" Then
    '==         If optReturned.Checked Then
    '==             optReturned.BackColor = Color.Crimson
    '==             LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(mlPriorityColour)
    '==         Else  '--unchecked--
    '==             optReturned.BackColor = Color.LavenderBlush
    '=         End If  '--checked
    '==     Else '--warranty--
    '==         If optSystemUnderWarranty.Checked Then
    '=             optSystemUnderWarranty.BackColor = ColorTranslator.FromOle(gIntUnderWarrantyColour())
    '==             optSystemUnderWarranty.ForeColor = Color.White
    '==             LabTicket.BackColor = optSystemUnderWarranty.BackColor
    '==             LabTicket.ForeColor = Color.White
    '==         Else  '-not checked-
    '==             optSystemUnderWarranty.BackColor = Color.LavenderBlush
    '==             optSystemUnderWarranty.ForeColor = Color.Black
    '==         End If  '-checked-
    '==     End If  '-name-
    '==     Call mSetDataChanged()
    '== End Sub  '-optReturned_CheckedChanged-
    '= = = = = =  == = = = = = = = = = = = = =

    Private Sub chkReturned_CheckedChanged_1(sender As Object, ev As EventArgs) _
                                               Handles chkReturned.CheckedChanged
        Call miGetPriority()
        If chkReturned.Checked Then
            chkReturned.BackColor = Color.Crimson
            LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(mlPriorityColour)
        Else  '--unchecked--
            chkReturned.BackColor = Color.LavenderBlush
        End If  '--checked
        Call mSetDataChanged()
    End Sub  '-chkReturned-
    '= = = = = = = =  = = = = = =

    Private Sub chkSystemUnderWarranty_CheckedChanged(sender As Object, ev As EventArgs) _
                                                        Handles chkSystemUnderWarranty.CheckedChanged
        Call miGetPriority()
        If chkSystemUnderWarranty.Checked Then
            chkSystemUnderWarranty.BackColor = ColorTranslator.FromOle(gIntUnderWarrantyColour())
            chkSystemUnderWarranty.ForeColor = Color.White
            LabTicket.BackColor = chkSystemUnderWarranty.BackColor
            LabTicket.ForeColor = Color.White
        Else  '-not checked-
            chkSystemUnderWarranty.BackColor = Color.LavenderBlush
            chkSystemUnderWarranty.ForeColor = Color.Black
            LabTicket.BackColor = ColorTranslator.FromOle(RGB(128, 128, 128))
        End If  '-checked-
        Call mSetDataChanged()
    End Sub '-chkSystemUnderWarranty-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- cboPrevGoods -- "KeyPress"..-
    '--  To TRAP ESC Char..--

    Private Sub cboPrevGoods_KeyPress(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) '=3038= Handles cboPrevGoods.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If (keyAscii = 13) Then '-enter--
            '== keyAscii = 0
        ElseIf (keyAscii = 27) Then  '--ESC--
            cboPrevGoods.Enabled = False
            cboPrevGoods.Width = 77
            labSelectPrevious.Visible = False
            '== cmdPrevGoods.Enabled = True
            keyAscii = 0
        End If

        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
        cmdCheckGoods.Select()
    End Sub '--key press..--
    '= = = = = = = = = == =
    '-===FF->

    '-- cboPrevGoods -- "Click"..-

    Private Sub cboPrevGoods_SelectedIndexChanged(ByVal sender As System.Object, _
                                                     ByVal e As System.EventArgs) _
                                                     Handles cboPrevGoods.SelectedIndexChanged
        Dim colGoods, colForEncode As Collection
        '==Dim col1, colJobGoods As Collection
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim sSelectedItem As String
        Dim s1, s2, sGoods, sDisplay As String
        Dim ix, iPos, iPos2 As Integer

        '--  get selected index and retrieve matching collection entry..
        If (cboPrevGoods.SelectedIndex >= 0) Then '--selected.-
            Try
                sSelectedItem = cboPrevGoods.Items(cboPrevGoods.SelectedIndex)
                colGoods = mColCustomerPrevGoodsFlat.Item(cboPrevGoods.SelectedIndex + 1)  '--selected goodsline.-
                If Not ((colGoods Is Nothing)) AndAlso (colGoods.Count > 0) Then
                    colForEncode = New Collection
                    colForEncode.Add(colGoods)
                    Call gbEncodeGoodsIncare(colForEncode, sGoods, sDisplay)
                    '--== 3083.402=  filter out if ON-SITE --
                    If (InStr(UCase(sGoods), "ON-SITE") > 0) Then
                        MsgBox("Selected Job was an ON-SITE Job.. " & vbCrLf & vbCrLf & _
                                  "Goods info is available only for in-house jobs..", MsgBoxStyle.Information)
                        Exit Sub
                    End If
                    '--Update static goods..--
                    If (msGoodsInCare <> "") Then msGoodsInCare = msGoodsInCare & vbCrLf
                    msGoodsInCare = msGoodsInCare & sGoods

                    If (txtGoodsList.Text <> "") Then txtGoodsList.Text = txtGoodsList.Text & vbCrLf
                    txtGoodsList.Text = txtGoodsList.Text & msShowGoodsInCare(sGoods)
                    labGoodsInfoRequired.Enabled = False
                    FrameUsers.Enabled = True
                    Call mSetDataChanged() '=== mbDataChanged = True
                End If
                '--  get Job No..
                iPos = InStr(LCase(sSelectedItem), "job #")
                If (iPos > 0) Then
                    s2 = Mid(sSelectedItem, iPos + 5)   '- get jobno plus ":"
                    iPos2 = InStr(s2, ":")
                    If (iPos2 > 0) Then
                        s1 = VB.Left(s2, iPos2 - 1)  '-- get job no.-
                        If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                            mIntPreviousJobNo = CInt(s1)
                            If Trim(txtGoodsOther.Text) = "" Then  '--flag prev job..-
                                txtGoodsOther.Text = "NB: Previous Job No: " & s1 & ";" & vbCrLf
                            End If
                        End If  '--numeric-
                    End If  '-pos2-
                End If  '--ipos-
            Catch ex As Exception
                MsgBox("Error in selecting previous Job.." & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If  '-selected-
    End Sub  '-- cboPrevGoods--
    '= = = = = = = = = 
    '-===FF->

    '--  force user to complete or ESC the combo..--

    Private Sub cboPrevGoods_Validating(ByVal sender As Object, _
                                        ByVal e As System.ComponentModel.CancelEventArgs) _
                                           Handles cboPrevGoods.Validating
        If (cboPrevGoods.Width >= 300) Then  '--still acrive--
            '=3083= e.Cancel = True
        End If  '--active-
    End Sub  '--validate--
    '= = = = = = = = = = = = 
    '-===FF->

    '== NEW ==
    '-- cmd Check Goods list..--

    Private Sub cmdCheckGoods_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles cmdCheckGoods.Click

        Dim frmGoods As frmGoodsInCare
        Dim colResultGoodsInCare As Collection
        Dim iPos, iPos2 As Integer
        Dim s1, s2 As String

        '-- Parse goods string.  make collection of goods.
        If Not gbDecodeGoodsIncare(msGoodsInCare, mColInitialGoodsInCare) Then '--load listview.--
            Exit Sub
        End If '-decode..-
        frmGoods = New frmGoodsInCare
        frmGoods.connection = mCnnJobs
        frmGoods.initialValues = mColInitialGoodsInCare
        frmGoods.MandatedFormTop = Me.Top + (Me.Height \ 5)
        frmGoods.MandatedFormLeft = Me.Left + (Me.Width \ 5)

        frmGoods.ShowDialog()

        If Not frmGoods.cancelled Then
            '--All goods updated..-
            colResultGoodsInCare = frmGoods.result
            If Not ((colResultGoodsInCare Is Nothing)) AndAlso (colResultGoodsInCare.Count > 0) Then
                Call gbEncodeGoodsIncare(colResultGoodsInCare, msGoodsInCare, msPrintGoodsInCare)
                txtGoodsList.Text = msShowGoodsInCare(msGoodsInCare) & vbCrLf
                labGoodsInfoRequired.Enabled = False
                '=3077= cboPriority.Enabled = True
                FrameUsers.Enabled = True
                '=3083.717= Call mSetDataChanged() '=== mbDataChanged = True
            Else '--nothing in list--
                txtGoodsList.Text = ""
                msGoodsInCare = ""
                msPrintGoodsInCare = ""
                '- cancel prev. Job if was there.-
                iPos = InStr(txtGoodsOther.Text, vbCrLf)  '--get first line-
                If iPos > 0 Then
                    s1 = VB.Left(txtGoodsOther.Text, iPos - 1)
                    If InStr(LCase(s1), "nb: previous job") > 0 Then  '--delete it-
                        txtGoodsOther.Text = Mid(txtGoodsOther.Text, iPos + 2)
                    End If
                End If
            End If  '--nothing in list-
            Call mSetDataChanged() '=== mbDataChanged = True
            '=3501.1105- 05Nov2018= REMINDER about Chargers..
            If Not mbByPassLaptopChargerCheck Then  '-must do it.
                If (InStr(LCase(msGoodsInCare), "laptop") > 0) Or _
                                (InStr(LCase(msGoodsInCare), "all in one") > 0) Or _
                                (InStr(LCase(msGoodsInCare), "notebook") > 0) Or _
                                  (InStr(LCase(msGoodsInCare), "netbook") > 0) Then
                    If MessageBox.Show("Note- A Laptop or Notebook is being accepted.." & vbCrLf & _
                             "  Is there a Charger/Power Supply included ?.", "Goods Extras", _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        _chkExtras_2.Checked = True
                    Else
                        _chkExtras_2.Checked = False
                    End If
                End If  '--laptop-
            End If  '-bypass-
        End If  '--cancelled.-
        If (Trim(txtGoodsList.Text) = "") And (Trim(txtGoodsOther.Text) = "") Then
            labGoodsInfoRequired.Enabled = True
            MsgBox("NO goods defined.", MsgBoxStyle.Exclamation)
        End If
        DoEvents()
    End Sub '--cmd goods..-
    '= = = = = = = = = = = = = =

    '-cmdGoodsClear--

    Private Sub cmdGoodsClear_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles cmdGoodsClear.Click
        txtGoodsList.Text = ""
        FrameUsers.Enabled = False
        msGoodsInCare = ""

    End Sub  '--clear--
    '= = = = = = = = = = = = 

    '--  catch changes..--

    'UPGRADE_WARNING: Event chkExtras.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub chkExtras_CheckStateChanged(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As System.EventArgs) Handles chkExtras.CheckStateChanged
        Dim index As Short = chkExtras.GetIndex(eventSender)

        If mbFormStarting Then Exit Sub

        Call mSetDataChanged() '=== mbDataChanged = True
        '===If mbAmending Then cmdFinish.Enabled = True
    End Sub '--extras..--
    '= = = = = = = = = = =
    '-===FF->


    'UPGRADE_WARNING: Event txtGoodsOther.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtGoodsOther_TextChanged(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As System.EventArgs) Handles txtGoodsOther.TextChanged

        Call mSetDataChanged() '=== mbDataChanged = True
        '==If mbAmending Then cmdFinish.Enabled = True
        If (txtGoodsOther.Text = "") And (txtGoodsList.Text = "") Then
            labGoodsInfoRequired.Enabled = True
        End If
        mbGoodsOtherChanged = True
        DoEvents()
    End Sub '--other.-
    '= = = = = = = = = =

    '--3203.102--
    '=  Browse for PICTURE--

    Private Sub picSubjectItem_Click(sender As Object, _
                                       ev As EventArgs) Handles picSubjectItem.Click
        Dim intDoc_id As Integer
        Dim bUpdating As Boolean = False  '--
        Dim sFileTitle As String
        Dim byteImageBytes() As Byte

        If ((Not (picSubjectItem.Image Is Nothing)) Or (msNewFileFullPath <> "")) Then
            If (MsgBox("Change this image ?", _
                       MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                Exit Sub
            End If  '-change ?-
            '-- has image.. wants to change..-
            If (mlJobId > 0) Then  '-not new- checkin or amend.
                '-- will later update image in Table..
                bUpdating = True
            End If '-job exists-
        End If
        '-- browse File System for new Image.
        If Not mClsAttachments1.OpenFileBrowse(msNewFileFullPath, msNewFileFileTitle, mByteNewFile) Then
            Exit Sub
        End If  '--open-

        '-- load to Pic Image..
        msNewFileFormat = ""
        '- check format..-
        Dim intPos1 As Integer = InStrRev(msNewFileFileTitle, ".")
        Dim s1 As String

        If (intPos1 > 0) Then '--found-
            s1 = Mid(msNewFileFileTitle, intPos1 + 1)
            If (Not mClsAttachments1.IsImageFile(s1)) Then  '=3311.227= And (UCase(s1) <> "PDF") Then
                MsgBox("Invalid File Type (not Image)..", MsgBoxStyle.Exclamation)
                Exit Sub
            Else
                Dim ms As System.IO.MemoryStream
                ms = New System.IO.MemoryStream(mByteNewFile)
                Dim image1 As Image = System.Drawing.Image.FromStream(ms)
                picSubjectItem.Visible = True
                picSubjectItem.Image = image1
                picSubjectMain.Image = image1
                ms.Close()
            End If
            msNewFileFormat = s1
            If (mIntDoc_id > 0) And bUpdating Then
                mbMainImageToBeUpdated = True    '- so we update when saving.
                '== Call mSetDataChanged()
                Dim intAffected As Integer
                '-- call update attachment.-
                If Not mClsAttachments1.UpdateAttachment(mIntDoc_id, msNewFileFullPath, msNewFileFileTitle, _
                                                              msNewFileFormat, msStaffName, mByteNewFile, _
                                                                                  False, Nothing, intAffected) Then
                    MsgBox("Update Failed.", MsgBoxStyle.Exclamation)
                    Exit Sub
                Else
                    MsgBox("Updated ok..", MsgBoxStyle.Information)
                End If
            ElseIf (mlJobId > 0) Then
                '-- must insert.
                Dim intAffected As Integer
                If Not mClsAttachments1.InsertNewAttachment(msNewFileFullPath, msNewFileFileTitle, msNewFileFormat, _
                                                                mlJobId, msCustomerName, "$$NewJOBImage" & vbCrLf, _
                                                                     False, Nothing, intAffected) Then
                    MsgBox("Failed to insert new image into DB. ", MsgBoxStyle.Exclamation)
                Else
                    MsgBox("Image inserted ok..", MsgBoxStyle.Information)
                End If
            End If  '-doc-id-
        Else '--invalid -
            MsgBox("Invalid File Type (no suffix)..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
    End Sub  '-picSubjectItem_Click-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Login Yes/No must be selected.--
    '--  Login Yes/No must be selected.--

    'UPGRADE_WARNING: Event OptLogin.CheckedChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub OptLogin_CheckedChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles OptLogin.CheckedChanged
        If eventSender.Checked Then
            Dim index As Short = OptLogin.GetIndex(eventSender)
            Dim ix As Short

            If OptLogin(index).Enabled Then '-normal new job..-
                frameProblem.Enabled = True '--can continue..-
                '== frameNominTech.Enabled = True
                '= cboPriority.Enabled = True
                cmdNext.Enabled = True
                _SSTabNewJob_TabPage1.Enabled = True
                cmdNavBackToStart.Enabled = True
                labLogonRequired.Enabled = False

                If OptLogin(0).Checked = True Then '--enable logins..-
                    ChkUsers.Enabled = True
                    txtUserName(0).Enabled = True
                    txtPassword(0).Enabled = True
                Else '--no login..-
                    For ix = 0 To k_maxUserNames - 1
                        txtUserName(ix).Text = ""
                        txtPassword(ix).Text = ""
                        txtUserName(ix).Enabled = False
                        txtPassword(ix).Enabled = False
                    Next ix
                    ChkUsers.Enabled = False
                    ChkUsers.CheckState = System.Windows.Forms.CheckState.Unchecked
                    '---chkProblem(0).SetFocus
                    '== 3067.0 = If Not mbAmending Then cmdAddSymptom.Focus()
                End If '--login.--
                Call mSetDataChanged() '=== mbDataChanged = True
                '===If mbAmending Then cmdFinish.Enabled = True
            End If '--enabled..-

        End If
    End Sub '-optLogin--
    '= = = = = = =  = =
    '-===FF->

    '--multiple users..--

    'UPGRADE_WARNING: Event ChkUsers.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub ChkUsers_CheckStateChanged(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As System.EventArgs) Handles ChkUsers.CheckStateChanged

        If ChkUsers.Enabled Then
            If ChkUsers.CheckState = 1 Then '--enable 2/3--
                txtUserName(1).Enabled = True
                txtPassword(1).Enabled = True
                txtUserName(2).Enabled = True
                txtPassword(2).Enabled = True
                txtUserName(1).Focus()
            Else
                txtUserName(1).Enabled = False
                txtPassword(1).Enabled = False
                txtUserName(2).Enabled = False
                txtPassword(2).Enabled = False
                txtUserName(1).Text = "" : txtPassword(1).Text = ""
                txtUserName(2).Text = "" : txtPassword(2).Text = ""
                '--chkProblem(0).SetFocus
                '== 3067.0 = If Not mbAmending Then cmdAddSymptom.Focus()
            End If '--true-
            Call mSetDataChanged() '=== mbDataChanged = True
            '===If mbAmending Then cmdFinish.Enabled = True
        End If '--enabled..-
    End Sub '--ChkUsers-
    '= = = = = = = = = = =
    '-===FF->

    '==3311.422 =--Now can change users/passwords..

    Private Sub _txtUserName_0_TextChanged(sender As Object, ev As EventArgs) _
                                                    Handles _txtUserName_0.TextChanged, _
                                                             _txtUserName_1.TextChanged, _
                                                               _txtUserName_2.TextChanged
        If mbFormStarting Then Exit Sub
        Call mSetDataChanged() '=== mbDataChanged = True

    End Sub  '-_txtUserName_0_TextChanged-
    '= = = = = = = = = = = = = = = = = = =

    Private Sub _txtPassword_0_TextChanged(sender As Object, ev As EventArgs) _
                                           Handles _txtPassword_0.TextChanged, _
                                           _txtPassword_1.TextChanged, _
                                           _txtPassword_2.TextChanged
        If mbFormStarting Then Exit Sub
        Call mSetDataChanged() '=== mbDataChanged = True

    End Sub  '-_txtPassword_0_TextChanged-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- n a v i g a t i o n --
    '-- n a v i g a t i o n --
    '-- n a v i g a t i o n --

    '--  Grey out Tab name if disabled..--

    '-- Declares the "DrawItem" event handler DrawOnTab which is a method that
    '-- draws a string and Rectangle on the tabPage1 tab.

    Private Sub SSTabNewJob_DrawOnTab(ByVal sender As Object, _
                                       ByVal e As DrawItemEventArgs) Handles SSTabNewJob.DrawItem
        Dim SSTab1 As TabControl = DirectCast(sender, TabControl)
        Dim pagex As TabPage = SSTab1.TabPages(e.Index)
        Dim g As Graphics = e.Graphics
        Dim font1 As Font = SSTab1.Font
        '== Dim brush As New SolidBrush(Color.Black)
        '== Dim tabTextArea As RectangleF = RectangleF.op_Implicit(SSTab1.GetTabRect(e.Index))
        Dim tabTextArea As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2)
        Dim brushGrey As New SolidBrush(SystemColors.GrayText)
        Dim brushNormal As New SolidBrush(pagex.ForeColor)
        Dim brushBGsel As New SolidBrush(pagex.BackColor)  '--selected bg.
        '==Dim brushBGunsel As New SolidBrush(Color.Gainsboro)
        Dim brushBGunsel As New SolidBrush(Color.Gainsboro)

        '== If SSTabNewJob.SelectedIndex = e.Index Then
        '== font = New Font(font, FontStyle.Bold)
        '== brush = New SolidBrush(Color.Red)
        '== End If
        '== g.DrawRectangle(p, mTabArea)
        '== g.DrawString("tabPage1", font, brush, mTabTextArea)

        If (SSTabNewJob.SelectedIndex = e.Index) Then  '--selected tab..- IS enabled.
            '--this is the background color of the tabpage
            '--Make this a standard color for the selected page
            '==br = New SolidBrush(pagex.BackColor)
            'this is the background color of the tab page
            g.FillRectangle(brushBGsel, e.Bounds)
            g.DrawString(SSTab1.TabPages(e.Index).Text, font1, brushNormal, tabTextArea)

        Else  '--not selected..
            g.FillRectangle(brushBGunsel, e.Bounds)
            If pagex.Enabled Then
                g.DrawString(SSTab1.TabPages(e.Index).Text, font1, brushNormal, tabTextArea)
            Else
                g.DrawString(SSTab1.TabPages(e.Index).Text, font1, brushGrey, tabTextArea)
            End If
        End If

    End Sub  '--SSTabNewJob_DrawOnTab--
    '= = = = = = = = = = = = = = = = = 

    '-- selecting.--

    Private Sub SSTabNewJob_Selecting(ByVal sender As System.Object, _
                                       ByVal e As System.Windows.Forms.TabControlCancelEventArgs) _
                                            Handles SSTabNewJob.Selecting

        If e.TabPageIndex = 1 Then
            If (Not _SSTabNewJob_TabPage1.Enabled) Then e.Cancel = True
        ElseIf e.TabPageIndex = 2 Then
            If (Not _SSTabNewJob_TabPage2.Enabled) Then e.Cancel = True
        End If

    End Sub
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    Private Sub cmdNext_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles cmdNext.Click
        SSTabNewJob.SelectTab(1)
    End Sub  '--next--
    '= = = = = = = = = = ==

    Private Sub cmdNavBackToStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
                                              Handles cmdNavBackToStart.Click
        SSTabNewJob.SelectTab(0)

    End Sub  '--BackToStart--
    '= = = = = = = = = = =  =

    '-- go to page 3..--

    Private Sub cmdNext2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext2.Click

        SSTabNewJob.SelectTab(2)
    End Sub  '--cmdNext2--
    '= = = = = = = = = = = =

    '--  back to page2--
    Private Sub cmdNavBack_Click(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs) Handles cmdNavBack.Click
        SSTabNewJob.SelectTab(1)
    End Sub  '--back--
    '= = = = = = = = = 
    '-===FF->

    '-- Symptoms listBox..--
    '-- Symptoms listBox..--
    '==3067.0==  NO LONGER USED ==
    '==3067.0==  NO LONGER USED ==
    '==3067.0==  NO LONGER USED ==

    Private Sub cmdAddSymptom_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) '==3067.0==  Handles cmdAddSymptom.Click
        Dim ix As Integer

        '==3067.0== labHelpProblem.Text = "Select from the List of common Symptoms."
        txtSymptoms.Visible = False
        listSymptoms.Top = txtSymptoms.Top
        listSymptoms.Height = VB6.TwipsToPixelsY(3200)
        listSymptoms.Visible = True
        '==cmdAddSymptom.Caption = "Apply"
        '==3067.0== cmdAddSymptom.Enabled = False
        '==3067.0== cmdSymptomsApply.Enabled = True

        cmdEditSymptoms.Enabled = False
        '===cmdProblemPrev.Enabled = False
        listSymptoms.Focus()
    End Sub '--addsymptom.--
    '= = = = =  = =  = =  ==
    '= = = = = = = = = = =
    '-===FF->

    '-- Checked a symptom..-- May have been turned on or off.--
    '--- no way of examining checked status of list item..
    '---- except by keeping our own status list..-

    'UPGRADE_WARNING: ListBox event ListSymptoms.ItemCheck has a new behavior. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'

    Private Sub ListSymptoms_ItemCheck(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.Windows.Forms.ItemCheckEventArgs) _
                                                Handles listSymptoms.ItemCheck
        Dim ix, cx As Integer

        '==3067.0==  
        '==3067.0==  
        '--  check not registered yet....
        '== FIND WHICH ITEM changed..  --
        txtSymptoms.Text = ""

        '--  check out the rest...-
        For ix = 0 To listSymptoms.Items.Count - 1
            '== listBox1.Items.Add(checkedListBox1.CheckedItems(i))
            If (ix = eventArgs.Index) Then  '--current this item-
                If (eventArgs.CurrentValue = CheckState.Unchecked) Then
                    '--was unchecked..  will be checked after this event..-
                    If txtSymptoms.Text <> "" Then
                        txtSymptoms.Text = txtSymptoms.Text & vbCrLf
                    End If
                    txtSymptoms.Text = txtSymptoms.Text & Trim(listSymptoms.Items(eventArgs.Index))
                End If
            Else  '--not current item..   include if checked.-
                '== cx = listSymptoms.CheckedIndices(ix)
                '==If listSymptoms.Items(cx).checkedstate = CheckState.Checked Then
                '--  CAN"T be Indeterminate-  
                '--  MUST be CHECKED..
                If listSymptoms.CheckedIndices.Contains(ix) Then
                    If txtSymptoms.Text <> "" Then
                        txtSymptoms.Text = txtSymptoms.Text & vbCrLf
                    End If
                    txtSymptoms.Text = txtSymptoms.Text & Trim(listSymptoms.Items(ix))
                    '= Trim(VB6.GetItemString(listSymptoms, ix))
                End If  '--contains item .-
            End If  '--not the one  clicked-
        Next ix
        '=3083=
        '=3083=cmdFinish.Enabled = True
        cmdNext2.Enabled = True
        _SSTabNewJob_TabPage2.Enabled = True
        frameInstructions.Enabled = True
        labProblemRequired.Enabled = False

        '---MsgBox "ItemCheck..  Item: " & index & " now has value: " & maiSymptoms(index)
        Call mSetDataChanged() '=== mbDataChanged = True

    End Sub '--itemCheck.--
    '= = = = =  = =  = =  =

    '--  leaving symptoms..--
    Private Sub ListSymptoms_KeyPress(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)

        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If keyAscii = 13 Then '--enter..--
            '== Call cmdAddSymptom_Click(cmdAddSymptom, New System.EventArgs())
            keyAscii = 0 '--processed.-
        ElseIf keyAscii = 27 Then  '--ESC..--
            '--restore normal state..--

            '== 3067.0 == listSymptoms.Visible = False
            txtSymptoms.Visible = True
            '== cmdAddSymptom.Enabled = True
            '===cmdProblemPrev.Enabled = True
            '==3067.0== cmdSymptomsApply.Enabled = False
            cmdEditSymptoms.Enabled = True
            keyAscii = 0 '--processed.-
            '==3067.0== cmdAddSymptom.Focus()
        End If
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = = = = = = = =

    '-- problem texts..-

    'UPGRADE_WARNING: Event txtProblem.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtProblem_TextChanged(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles txtProblem.TextChanged
        '==3083=cmdFinish.Enabled = True
        '=3083=
        cmdNext2.Enabled = True
        _SSTabNewJob_TabPage2.Enabled = True
        frameInstructions.Enabled = True
        labProblemRequired.Enabled = False

        Call mSetDataChanged() '=== mbDataChanged = True

        '== frameNominTech.Enabled = True
    End Sub '--txtProblem.--
    '= = = = = = = = = = = =
    '-===FF->

    '-- Edit table of symptoms..-
    Private Sub cmdEditSymptoms_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles cmdEditSymptoms.Click
        Dim frmListEdit1 As New frmListEdit

        frmListEdit1.maxLength = 48
        frmListEdit1.tableName = "Symptoms"
        frmListEdit1.DescrColumn = "SymptomDescr"
        frmListEdit1.IdColumn = "Symptom_Id"
        frmListEdit1.PrimaryKeyColName = "Symptom_Id"
        frmListEdit1.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + 3000)
        frmListEdit1.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(txtSymptoms.Left) + 5000)
        frmListEdit1.connection = mCnnJobs
        frmListEdit1.deletionsOK = True
        '--MsgBox "Calling edit for: " + sTableName, vbInformation
        frmListEdit1.ShowDialog()

        '==3072/3== If Not frmListEdit1.cancelled Then '--update..-
        '--load goods list..-
        Call mbLoadRefSymptoms()
        '--If (ListGoods.ListCount <= 0) Then cmdCheckGoods.Enabled = False
        '=3067.0= Check off existing symptoms..
        Call mbCheckSelectedSymptoms(txtSymptoms.Text)
        '==3072/3== End If '--cancelled..-
        frmListEdit1.Close()

    End Sub '-- edit symptoms.-
    '= = = = = = = = = = = = =
    '-===FF->

    '--  Backup Check-boxes..--

    'UPGRADE_WARNING: Event ChkBackupReq.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub ChkBackupReq_CheckStateChanged(ByVal eventSender As System.Object, _
                                                   ByVal eventArgs As System.EventArgs) Handles ChkBackupReq.CheckStateChanged

        Call mSetDataChanged() '=== mbDataChanged = True
    End Sub
    '= = = = = = = = = = = =

    'UPGRADE_WARNING: Event ChkRecovDisk.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub ChkRecovDisk_CheckStateChanged(ByVal eventSender As System.Object, _
                                                  ByVal eventArgs As System.EventArgs) Handles ChkRecovDisk.CheckStateChanged

        Call mSetDataChanged() '=== mbDataChanged = True
    End Sub
    '= = = = = = = = = = == =
    '-===FF->

    '--  Quotation Options..--
    '--  Quotation Options..--

    'UPGRADE_WARNING: Event optQuotation.CheckedChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub optQuotation_CheckedChanged(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As System.EventArgs) Handles optQuotation.CheckedChanged
        If eventSender.Checked Then
            Dim index As Short = optQuotation.GetIndex(eventSender)

            LabInstructions.BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0)
            If (optQuotation(0).Checked = True) Then '--quote.-
                LabCostLimit.BackColor = System.Drawing.Color.Yellow
                '== LabCostLimit.Enabled = False
                LabCostLimit.Text = "QUOTATION REQUIRED:" & vbCrLf & _
                                         "Minimum Charge of " & FormatCurrency(mCurLabourMinCharge, 2) & " applies.."
            ElseIf (optQuotation(1).Checked = True) Then  '--proceed OK..-
                LabCostLimit.BackColor = System.Drawing.Color.Lime
                '== LabCostLimit.Enabled = True
                LabCostLimit.Text = "Proceed ok with agreed Service.. "
            ElseIf (optQuotation(2).Checked = True) Then  '--proceed to limit..-
                LabCostLimit.Text = Replace(msBusinessShortName, "_", " ") & _
                                  " to notify Customer if cost can exceed " & FormatCurrency(mCurNotificationCostLimit, 2)
                LabCostLimit.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFC0)
            End If
            Call mSetDataChanged() '=== mbDataChanged = True
            cboPriority.Enabled = True
            labInstructionRequired.Enabled = False
        End If
    End Sub
    '= = = = = = = = = = = = =
    '-===FF->

    '-- Priority..--

    'UPGRADE_WARNING: Event cboPriority.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub cboPriority_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                     ByVal eventArgs As System.EventArgs) _
                                                            Handles cboPriority.SelectedIndexChanged
        Call miGetPriority()
        LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(mlPriorityColour)
        '=3077= FrameUsers.Enabled = True
        '=3077= frameNominTech.Enabled = True
        Call mSetDataChanged() '=== mbDataChanged = True
        '===If mbAmending Then cmdFinish.Enabled = True
        labPriorityRequired.Visible = False
        cmdFinish.Enabled = True

    End Sub '--priority..=
    '= = = = = = = = = = =

    '-- datePromised  --

    Private Sub datePromised_ValueChanged(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) Handles datePromised.ValueChanged
        If datePromised.Checked Then
            datePromised.CustomFormat = "ddd dd-MMM-yyyy"
            '== datePromised.Value = DateTime.Now.AddDays(1)  '--show promised date..-
        Else
            datePromised.CustomFormat = "  "
        End If
        Call mSetDataChanged() '=== mbDataChanged = True
        'If mbIsOnSiteJob And mbAmending Then
        '    MsgBox("OnSite Job-  NB. Make sure you set the right Duration value for the Calendar, " & vbCrLf & _
        '              "   as the (Duration) UpDown control will have been set back to its default value..", MsgBoxStyle.Exclamation)
        'End If
    End Sub  '-- datePromised-
    '= = = = = = = = = = = = = = =

    Private Sub timeOnSite_ValueChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles timeOnSite.ValueChanged

        Call mSetDataChanged() '=== mbDataChanged = True
        'If mbIsOnSiteJob And mbAmending Then
        '    MsgBox("OnSite Job-  NB. Make sure you set the right Duration value for the Calendar, " & vbCrLf & _
        '              "   as the (Duration) UpDown control will have been set back to its default value..", MsgBoxStyle.Exclamation)
        'End If
    End Sub  '--time changed..--
    '= = = = = = =  = == = = =  =
    '-===FF->

    '=3311.228= -- Browse/Select Staff-

    Private Sub btnLookupStaff_Click(sender As Object, ev As EventArgs) _
                                      Handles btnLookupStaff.Click
        Dim sId As String
        Dim colStaffFields As Collection
        Dim colSelectedRow As Collection
        Dim colFld As Collection
        Dim sBarcode, sName As String

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If mRetailHost1.staffLookup(colStaffFields) Then
            '--retrieve selected record and fill in cust details..--
            If colStaffFields Is Nothing Then
                '==Exit function
            Else '--selection made..-
                '-- get barcode..--
                If (colStaffFields.Count() > 0) Then
                    sBarcode = colStaffFields.Item("barcode")("value")
                    sName = Trim(colStaffFields.Item("docket_name")("value"))
                    If (sName = "") Then
                        MsgBox("Please choose a staff member with a Docket Name.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    sId = CStr(colStaffFields.Item("staff_id")("value"))
                    txtNomTech.Text = sBarcode
                    txtTechName.Text = sName  '= colStaffFields.Item("docket_name")("value")
                    Call mSetDataChanged()
                    cmdFinish.Enabled = True
                    '== MsgBox("Found staff id:  " & sId, MsgBoxStyle.Information)
                Else
                    If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                End If '--got row..--
            End If '--nothing..-
        Else
            MsgBox("Lookup cancelled.", MsgBoxStyle.Information)
        End If '--lookup..-
        System.Windows.Forms.Application.DoEvents()
    End Sub  '--lookup-
    '= = = = = = = = = = = = = = == 
    '-===FF->

    'UPGRADE_WARNING: Event txtNomTech.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtNomTech_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs)
        Call mSetDataChanged() '=== mbDataChanged = True
        '==If mbAmending Then cmdFinish.Enabled = True
    End Sub '-tech..-
    '= = = = = = = = =

    '-- nominated tech/staff--
    '-- nominated tech/staff--
    Private Sub txtNomTech_Enter(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) _
                                      Handles txtNomTech.Enter

        txtNomTech.SelectionStart = 0
        txtNomTech.SelectionLength = Len(txtNomTech.Text)
    End Sub '--gotfocus.-
    '= = = = = = = = = = =
    '-===FF->

    '-- NomTech.. Enter BARCODE..-

    Private Sub txtNomTech_KeyPress(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                       Handles txtNomTech.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim colFields As Collection
        Dim bOk As Boolean

        bOk = True
        If keyAscii = 13 Then '--enter-
            s1 = Trim(txtNomTech.Text)
            If (s1 <> "") Then '--empty is ok..--
                '==If IsNumeric(s1) Then  '--number must be id..-
                If mbLookupStaffBarcode(s1, colFields) Then '--ok-
                    '===txtNomTech.Text = colFields("docket_name")("value")
                    txtTechName.Text = colFields.Item("docket_name")("value")
                    Call mSetDataChanged() '=== mbDataChanged = True
                    cmdFinish.Enabled = True
                    '=3077= cboPriority.Focus()
                Else
                    bOk = False
                    MsgBox("Invalid staff barcode..", MsgBoxStyle.Exclamation)
                End If '--lookup.-
                '==Else  '--alpha..  accept anything..-
                '==End If  '--numeric.-
            Else '--empty..-
                txtTechName.Text = ""
                Call mSetDataChanged() '=== mbDataChanged = True
                '==If mbAmending Then cmdFinish.Enabled = True
            End If '--empty-
            '====  If bOk Then cmdCheckGoods.SetFocus   '-cmdfinish.SetFocus
            keyAscii = 0 '--processed..-
        End If '--13--
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--enter-
    '= = = =  = = =

    '-- NomTech.-- Validate..--

    Private Sub txtNomTech_Validating(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                           Handles txtNomTech.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim s1 As String
        Dim colFields As Collection

        s1 = Trim(txtNomTech.Text)
        If (s1 <> "") Then '--empty is ok..--
            '===If IsNumeric(s1) Then  '--number must be id..-
            If mbLookupStaffBarcode(s1, colFields) Then '--ok-
                '===txtNomTech.Text = colFields("docket_name")("value")
                txtTechName.Text = colFields.Item("docket_name")("value")
                '===If mbAmending Then cmdFinish.Enabled = True
                Call mSetDataChanged() '=== mbDataChanged = True
                cmdFinish.Enabled = True
            Else
                MsgBox("Invalid staff barcode..", MsgBoxStyle.Exclamation)
                keepfocus = True
            End If
            '===End If  '--numeric.-
        Else '--empty..-
            txtTechName.Text = ""
            '===If mbAmending Then cmdFinish.Enabled = True
            Call mSetDataChanged() '=== mbDataChanged = True
        End If '--empty-

        eventArgs.Cancel = keepfocus
    End Sub '--tech..--
    '= = = = = = = = = = =
    '-===FF->

    '=3203.106= get printer sel..
    '--catch printer combo selections..--
    '-- update settings.-

    Private Sub cboColourPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboColourPrinters.SelectedIndexChanged
        '= Dim sName As String
        If mbFormStarting Then Exit Sub
        If (cboColourPrinters.SelectedIndex >= 0) Then
            msColourPrinterName = cboColourPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(gK_SETTING_PRTCOLOUR, msColourPrinterName) Then
                MsgBox("Failed to save COLOUR printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  cboColourPrinters-
    '= = = = = = = = = = = = = = = = = =

    Private Sub cboReceiptPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                     ByVal e As System.EventArgs) _
                                                     Handles cboReceiptPrinters.SelectedIndexChanged
        '= Dim sName As String
        If mbFormStarting Then Exit Sub
        If (cboReceiptPrinters.SelectedIndex >= 0) Then
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(gK_SETTING_PRTRECEIPT, msReceiptPrinterName) Then
                MsgBox("Failed to save RECEIPT printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  cboReceiptPrinters-
    '= = = = = = = = = = = == = = = = 

    Private Sub cboLabelPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                      ByVal e As System.EventArgs) _
                                                      Handles cboLabelPrinters.SelectedIndexChanged
        '= Dim sName As String
        If mbFormStarting Then Exit Sub
        If (cboLabelPrinters.SelectedIndex >= 0) Then
            msLabelPrinterName = cboLabelPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(gK_SETTING_PRTLABEL, msLabelPrinterName) Then
                MsgBox("Failed to save Label printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  cboLabelPrinters-
    '= = = = = = = = = = = = =  =
    '-===FF->


    '-- printer Commands..--
    '-- c m d P r i n t L a b e l --

    Private Sub cmdPrintLabel_Click()
        Dim ix, lngError As Integer
        Dim sCust As String
        '==Dim curGap As Currency, curLabelDepth As Currency  '--in twips..--
        Dim intNoLabels As Short = 0
        '==Dim lngLHS, lngTop As Long
        Dim va1 As Object
        Dim colGoodsList As Collection
        Dim prtDocs1 As New clsPrintDocs

        On Error GoTo cmdPrintLabel_Error
        '==If (mPrtLabel Is Nothing) Then Exit Sub
        If (msLabelPrinterName = "") Then Exit Sub

        prtDocs1 = New clsPrintDocs
        '==Set Printer = mPrtLabel
        '== prtDocs1.PrtSelectedPrinter = mPrtLabel
        prtDocs1.PrtSelectedPrinterName = msLabelPrinterName

        prtDocs1.ItemBarcodeFontName = msLabelBarcodeFontName
        prtDocs1.ItemBarcodeFontSize = mlLabelBarcodeFontSize

        '=== 3083.717 =
        intNoLabels = mIntNumberOfLabels()

        If (intNoLabels < 1) Then intNoLabels = 1
        '=== 3083.717 = Up/Down control now rules...-
        If NumUpDownLabels.Enabled Then
            intNoLabels = CInt(NumUpDownLabels.Value)
        End If
        '=== 3083.717 = '-- V2.2.2902--  ONE ONLY after 1st lot done..-
        '=== 3083.717 = If mbLabelsPrinted Then intNoLabels = 1

        sCust = IIf(((msCustomerCompany <> "") And (msCustomerCompany <> "--")), msCustomerCompany, msCustomerName)
        sCust = Trim(VB.Left(sCust, 24)) & " [" & msCustomerBarcode & "]"
        '-- ASSUMING no need to feed to top of label..--
        '==For ix = 1 To lngTopmargin
        '==    Printer.Print ""
        '==Next ix
        If (intNoLabels > 0) Then  '=3083=
            Call prtDocs1.PrintJobLabels(mlJobId, intNoLabels, sCust)
        End If

        mbLabelsPrinted = True
        prtDocs1 = Nothing
        Exit Sub

cmdPrintLabel_Error:
        lngError = Err().Number
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox("Runtime Error in cmd PrintLabel.." & vbCrLf & _
                     "Error=" & lngError & ": " & ErrorToString(lngError), MsgBoxStyle.Critical)

    End Sub '--PrintLabel--
    '= = = = = = = = ==
    '-===FF->

    '--c m d P r i n t A g r e e m e n t --
    '--c m d P r i n t A g r e e m e n t --

    Private Sub cmdPrintAgreement_Click()
        Dim ans As Short
        Dim lngError As Integer
        Dim bNewPrinter As Boolean
        Dim bReceiptOk As Boolean
        Dim bCancelled As Boolean
        Dim colReportLines As Collection
        Dim sFinish As String
        '== Dim frmPrtSelect1 As New frmPrtSelect

        On Error GoTo cmdPrintAgreement_Error
        '===End If '--build..-

        '--Print form..--
        '--Print form..--
        System.Windows.Forms.Application.DoEvents()
        ans = 0 : bCancelled = False
        While (ans <> MsgBoxResult.Yes) And (Not bCancelled)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            '--On Error Resume Next
            '--Me.PrintForm
            '--lngError = Err()
            '-- V2: Custom Print the form.-
            '=== Set Printer = mPrtColour '-- set main printer--
            lngError = mbPrintNewJobForm()
            '-On Error GoTo cmdFinish_error:
            System.Windows.Forms.Application.DoEvents()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If (lngError <> 0) Then '--failed..-
                ans = MsgBox("Printing can not start..  Check Printer Server..", _
                                     MsgBoxStyle.Question + MsgBoxStyle.RetryCancel + MsgBoxStyle.DefaultButton1)
                If (ans = MsgBoxResult.Cancel) Then
                    bCancelled = True
                    '--Exit Sub
                Else
                    '== GoSub cmdPrintAgreement_NewPrinter
                    bNewPrinter = False
                End If '--cancel-
            Else '--Spooled ok.-
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                '-- 15 sec delay showing progress bar..--
                '======Call mbWaitProgress("-Printing Service Agreement-")
                System.Windows.Forms.Application.DoEvents()
                ans = MsgBoxResult.Yes
            End If '--error-
        End While '--ans-
        System.Windows.Forms.Application.DoEvents()
        '--cancel ??--
        If bCancelled Then
            cmdCancel.Enabled = True
            cmdFinish.Text = sFinish
            cmdCancel.Text = "Cancel"
            MsgBox("Printing has been abandoned..", MsgBoxStyle.Exclamation)
            '===Me.Hide
            Exit Sub '-- go back..-
        End If '--print cancelled-
        '-- Check if Receipt done OK..--
        '-- RETRY Receipt loop--
        ans = 0
        Exit Sub

cmdPrintAgreement_Error:
        lngError = Err().Number
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox("Runtime Error in cmd PrintAgreement.." & vbCrLf & "Error=" & lngError & ": " & ErrorToString(lngError), MsgBoxStyle.Critical)

    End Sub '--PrintAgreement-
    '= = = = = = = = = =
    '-===FF->

    '--print what's indicated..--
    '--print what's indicated..--

    Private Sub cmdPrintAll_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles cmdPrintAll.Click
        Dim colReportLines As Collection
        Dim lngError As Integer

        On Error GoTo cmdPrintAll_Error
        If (optQuotation(0).Checked = False) And (optQuotation(1).Checked = False) And (optQuotation(2).Checked = False) Then
            MsgBox("Customer Instruction must be selected...", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If (chkPrtDocs(k_PRINT_RECEIPT).CheckState = 1) Then
            '--  Build Receipt lines collection..--
            If Not mbBuildReceipt(mlJobId, colReportLines) Then '-ok-
                MsgBox("Error in building Receipt content.", MsgBoxStyle.Exclamation)
                '==bCancelled = True
            End If
            '==bReceiptOk = False
            '===If mbBuildReceipt(mlJobId, colReportLines) Then '-ok-
            '===ans = 0: bCancelled = False
            Call mbPrintReceipt(colReportLines)
            '-- SEND Receipt..  ask later..--
        End If '--receipt..-
        If (chkPrtDocs(k_PRINT_AGREEMENT).CheckState = 1) Then
            Call cmdPrintAgreement_Click()
        End If
        If (chkPrtDocs(k_PRINT_LABEL).CheckState = 1) Then '--checked..--
            Call cmdPrintLabel_Click()
        End If
        colReportLines = Nothing
        Exit Sub

cmdPrintAll_Error:
        lngError = Err().Number
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox("Runtime Error in cmd PrintAll.." & vbCrLf & _
                      "Error=" & lngError & ": " & ErrorToString(lngError), MsgBoxStyle.Critical)

    End Sub '--print what's indicated..--
    '= = = = = = = = = =
    '-===FF->

    '--Printer doc. options..--
    '--- disable print cmd if nothing checked..-
    '----  or if no actual job yet..--

    'UPGRADE_WARNING: Event chkPrtDocs.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub chkPrtDocs_CheckStateChanged(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As System.EventArgs) Handles chkPrtDocs.CheckStateChanged
        Dim index As Short = chkPrtDocs.GetIndex(eventSender)
        Dim ix As Short
        Dim bCanPrint As Boolean = False

        If mbFormStarting Then Exit Sub
        For ix = 0 To 2
            If chkPrtDocs(ix).CheckState = 1 Then bCanPrint = True '--is checked..-
        Next ix
        If bCanPrint And (mlJobId > 0) And (Not mbIsCheckIn) Then
            '===  cmdPrintAll.Enabled = True
        Else
            '=== cmdPrintAll.Enabled = False
        End If
        If bCanPrint Then
            cmdFinish.Text = "Save && Print"
        Else
            cmdFinish.Text = "Save"
        End If
        '==3083=-disable Up/Down if no labels needed.
        If (chkPrtDocs(k_PRINT_LABEL).Enabled) AndAlso (chkPrtDocs(k_PRINT_LABEL).CheckState = 1) Then
            NumUpDownLabels.Enabled = True
        Else
            NumUpDownLabels.Enabled = False
        End If

    End Sub '--docs.--
    '= = = = = = = = =

    '-NumUpDownLabels_ValueChanged-

    Private Sub NumUpDownLabels_ValueChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles NumUpDownLabels.ValueChanged

        '==MsgBox("NumUpDownLabels_ValueChanged event fired..", MsgBoxStyle.Information) '--TEST -

        If mbNumUpDownLabels_Updating Or mbFormStarting Then Exit Sub
        '- Was changed by user.
        '== MsgBox("NumUpDownLabels_ValueChanged by user..", MsgBoxStyle.Information) '--TEST -
        mbUserChangedNumberOfLabels = True
    End Sub '--NumUpDownLabels_ValueChanged-
    '= = = = = = = = = = = = = = = = = =

    '--  replace blanks with zero.-
    '-   (bug in control.)
    Private Sub NumUpDownLabels_Validating(ByVal sender As Object, _
                                           ByVal e As System.ComponentModel.CancelEventArgs) _
                                              Handles NumUpDownLabels.Validating
        If NumUpDownLabels.Text = "" Then
            NumUpDownLabels.Value = 0
        End If
    End Sub  '--NumUpDownLabels_Validating-
    '= = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '== SAVE and Print = = = = = ==
    '== SAVE and Print = = = = = ==
    '==
    '==  -- 3327.0119- 19-Jan-2017-
    '==     -- Fix to Add static var lock on cmdFinish to stop re-entry-..-- 

    Private mbFinishInProgress As Boolean = False

    '== SAVE and Print = = = = = ==
    '== SAVE and Print = = = = = ==

    Private Sub cmdFinish_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdFinish.Click
        Dim ans As Short
        Dim bCancelled As Boolean
        Dim sStuff As String
        Dim sSql As String
        Dim ix, intAffected, iPos, lngError As Integer
        Dim lngCurrentJobNo As Integer
        Dim s1, sErrorMsg As String
        Dim sRem, sChunk As String
        Dim sUsernames, sPwds As String
        Dim rs1 As DataTable '= ADODB.Recordset
        '--Dim sPriority As String
        Dim sFinish As String
        Dim sJobFinalStatus As String
        Dim sCustomerRefresh As String
        Dim sNotification As String
        Dim sShortDate As String '-- dd-mmm-yyyy --
        Dim sDatePromised As String = ""
        '==Dim vline As Variant, control1 As Control
        Dim cmdUpd As OleDbCommand
        '== Dim parameter1 As OleDbParameter
        Dim colJobFields As Collection

        If mbFinishInProgress Then
            Exit Sub     '--catch spurious click.
        End If
        mbFinishInProgress = True    '= NOW block spurious click-
        '== On Error GoTo cmdFinish_error
        bCancelled = False
        lngCurrentJobNo = 0
        cmdCancel.Enabled = False
        sFinish = cmdFinish.Text '--save..-
        '--check all stuff (flds) have been done..--
        '===If (Not mbAmending) Then
        sStuff = msCheckFormComplete()
        If (sStuff <> "") Then
            MsgBox("This Form is not complete.." & vbCrLf & _
                     "The following info is still missing: " & vbCrLf & sStuff & vbCrLf, MsgBoxStyle.Exclamation)
            cmdCancel.Enabled = True
            mbFinishInProgress = False  '-Release Roger-
            Exit Sub
        End If '--stuff..-
        '===End If  '--amending..-

        '-- Pack up users/passwords..
        sUsernames = "" : sPwds = ""
        For ix = 0 To k_maxUserNames - 1
            If sUsernames <> "" Then '--not the first..-
                sUsernames = sUsernames & "/"
                sPwds = sPwds & "/"
            End If
            sUsernames = sUsernames & Trim(txtUserName(ix).Text)
            sPwds = sPwds & Trim(txtPassword(ix).Text)
        Next ix
        '-- check if password needed..--
        '== 3107.1213=  UserName and Password must both be there if any..
        If ((sUsernames = "") And (sPwds <> "")) Or _
               ((sUsernames <> "") And (sPwds = "")) Then '--not complete...-
            MsgBox("Please Note: " & vbCrLf & _
                     "UserName and Password must both be entered, if any..", MsgBoxStyle.Exclamation)
            cmdCancel.Enabled = True
            mbFinishInProgress = False  '-Release Roger-
            Exit Sub
        End If  '-incomplete-
        s1 = ""
        If (Not mbAmending) And (msCustomerName = "") Then s1 = s1 & "Customer Name" & vbCrLf
        If OptLogin(0).Checked = True Then '--enable logins..-
            If (sUsernames = "") Or (sPwds = "") Then '--confirm..-
                s1 = s1 & "Username/Password info." & vbCrLf
            End If '--usernames..-
        End If '--optLogin..-
        If (s1 <> "") Then '--some optional flds..-
            If MsgBox("Please Note:  The fields:" & vbCrLf & s1 & vbCrLf & _
                           " May be Incomplete.." & vbCrLf & " Do you want to complete these fields?", _
                                MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                cmdCancel.Enabled = True
                mbFinishInProgress = False  '-Release Roger-
                Exit Sub
            End If '--yes.-
        End If '--s1-
        sShortDate = VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy")
        '-- Save (INSERT) job amd retrieve identity as mlJobId..-
        '---and update ticket no on form..
        '=3107.904=-- call priority for everyone-msPriority = "H"
        If mbIsOnSiteJob Then
            '== NO MORE- 3083.312--  msPriority = "S"   '== 3083==
            msGoodsInCare = K_GOODS_ONSITEJOB   '--make sure..-   
        Else
            '=Call miGetPriority()
        End If
        '=3107.904=-- call priority for everyone-
        Call miGetPriority()
        LabTicket.BackColor = System.Drawing.ColorTranslator.FromOle(mlPriorityColour)
        sJobFinalStatus = "" '-- assuming no change...-
        sNotification = ""
        '--  Set up correct QUOTATION/PROCEED flag for ProblemShort..--
        '--  TRANSLATE Quotation options..
        '--  first clear the flag, whatever it is..-
        msProblemShort = Replace(msProblemShort, "*PROCEED-WITH-SERVICE*;", "") '--erase..MUST be first..
        msProblemShort = Replace(msProblemShort, "*PROCEED-WITH-SERVICE*", "") '--erase.. old vers...

        msProblemShort = Replace(msProblemShort, "*PROCEED-TO-LIMIT*;", "") '--erase.. MUST be first..
        msProblemShort = Replace(msProblemShort, "*PROCEED-TO-LIMIT*", "") '--erase..

        msProblemShort = Replace(msProblemShort, "*QUOTATION-REQUIRED*;", "") '--erase "Quote Required"..
        msProblemShort = Replace(msProblemShort, "*QUOTATION-REQUIRED*", "") '--erase (old ver..)..
        '-- now set up correct flag.-
        If (optQuotation(0).Checked = True) Then '--quote.-
            msProblemShort = msProblemShort & "*QUOTATION-REQUIRED*;"
        ElseIf (optQuotation(1).Checked = True) Then  '--proceed OK..-
            msProblemShort = msProblemShort & "*PROCEED-WITH-SERVICE*;"
        ElseIf (optQuotation(2).Checked = True) Then  '--proceed to limit..-
            msProblemShort = msProblemShort & "*PROCEED-TO-LIMIT*;"
        End If

        '==3083== DatePromised==
        '==3083== DatePromised==
        '==3083== DatePromised==
        If datePromised.Checked Then
            sDatePromised = Format(datePromised.Value, "dd-MMM-yyyy ")  '-for DB-
            msDatePromised = Format(datePromised.Value, "ddd dd-MMM-yyyy ") '=PRINTING sDatePromised
            If mbIsOnSiteJob Then  '--save timeBooked
                sDatePromised &= Format(TimeValue(timeOnSite.Value), "hh:mm tt")
                msTimepromised = Format(TimeValue(timeOnSite.Value), "hh:mm tt")
            Else '--not onsite.-
                sDatePromised &= "17:00 "
            End If
        Else
            sDatePromised = "25-Dec-2050 17:00"   '--not promised..-
        End If
        '--  INSERT New Job,  Or UPDATE Existng Job..--
        '--  INSERT New Job,  Or UPDATE Existng Job..--
        If mbAmending Then '--update..-
            '=3501.0814-  Update- Add to ServiceNotes if GoodsInCare was Changed..
            Dim sNewNote As String = ""
            If mbDataChanged And ((Trim(msGoodsInCare) <> Trim(msOriginalGoodsInCare)) Or mbGoodsOtherChanged) Then
                sNewNote = vbCrLf & "** NB: GoodsInCare may have been amended." & vbCrLf & _
                    "= New Goods List is:" & vbCrLf & _
                            msFixSqlStr(msShowGoodsInCare(msGoodsInCare)) & vbCrLf
                If Trim(txtGoodsOther.Text) <> "" Then
                    sNewNote &= msFixSqlStr(txtGoodsOther.Text)
                End If
                If (VB.Right(Trim(sNewNote), 2) <> vbCrLf) Then
                    sNewNote &= vbCrLf
                End If
                sNewNote &= "= By:  " & msStaffName & "-  on " & Format(Now, "dd-MMM-yyyy hh:mm tt") & ";" & vbCrLf
            End If  '--goods changed.

            '-- Update..  DO NOT change status..-
            If VB.Left(msJobOriginalStatus, 2) = "05" Then '--was a booking..-
                If mbIsCheckIn Then sJobFinalStatus = k_statusCreated '-- "10-Created"  '-- assuming got goods.-
            End If '--was booking.-
            sCustomerRefresh = ""
            If mbCustomerRefreshed Then '-- checked..
                sCustomerRefresh = ", CustomerCompany='" & msFixSqlStr(msCustomerCompany) & "' "
                sCustomerRefresh = sCustomerRefresh & ", CustomerName='" & msFixSqlStr(msCustomerName) & "' "
                sCustomerRefresh = sCustomerRefresh & ", CustomerPhone='" & msFixSqlStr(msCustomerPhone) & "' "
                sCustomerRefresh = sCustomerRefresh & ", CustomerMobile='" & msFixSqlStr(msCustomerMobile) & "' "
            End If '--refresh.-
            sSql = "UPDATE [jobs] SET "
            '== WIZARD did->  sSql = CStr(CDbl(sSql) + IIf((sJobFinalStatus = ""), "", " JobStatus='" & sJobFinalStatus & "', "))
            sSql &= IIf((sJobFinalStatus = ""), "", " JobStatus='" & sJobFinalStatus & "', ")
            sSql &= "Priority='" & msPriority & "' "
            sSql &= sCustomerRefresh
            sSql &= ", NominatedTech='" & msFixSqlStr((txtTechName.Text)) & "'"
            sSql &= ", GoodsInCare='" & msFixSqlStr(VB.Left(msGoodsInCare, 250)) & "'"
            sSql &= ", GoodsOther='" & msFixSqlStr(VB.Left(txtGoodsOther.Text, 250)) & "'"
            sSql &= ", GoodsExtras='" & msFixSqlStr(VB.Left(msExtrasInCare, 250)) & "'"
            '== WIZARD did->  sSql = CStr(CDbl(sSql & ", MultiAccounts='") + IIf((ChkUsers.CheckState = 1), "Y", "N") + CDbl("'"))
            sSql &= ", MultiAccounts='" & IIf((ChkUsers.CheckState = 1), "Y", "N") & "'"
            sSql &= ", Username='" & msFixSqlStr(VB.Left(sUsernames, 32)) & "'"
            sSql &= ", UserPassword='" & msFixSqlStr(VB.Left(sPwds, 32)) & "'"
            sSql &= ", ProblemSymptoms='" & msFixSqlStr(VB.Left(msSymptoms, 250)) & "'"
            sSql &= ", ProblemLong='" & msFixSqlStr((txtProblem.Text)) & "'"
            sSql &= ", ProblemShort='" & msFixSqlStr(msProblemShort) & "'"

            '= 3203.117=  sSql &= ", JobReturned='" & IIf((chkReturned_deleted.CheckState = 1), "Y", "N") & "'"
            sSql &= ", JobReturned='" & IIf(chkReturned.Checked, "Y", "N") & "'"
            '= 3203.118= 
            sSql &= ", SystemUnderWarranty=" & IIf(chkSystemUnderWarranty.Checked, 1, 0)

            sSql &= ", DataBackupReqd='" & IIf((ChkBackupReq.CheckState = 1), "Y", "N") & "'"
            '== WIZARD did->  sSql = CStr(CDbl(sSql & ", DataDiskReqd='") + IIf((ChkRecovDisk.CheckState = 1), "Y", "N") + CDbl("'"))
            sSql &= ", DataDiskReqd='" & IIf((ChkRecovDisk.CheckState = 1), "Y", "N") & "'"
            If sNotification <> "" Then
                sSql &= sNotification
            End If
            If (sNewNote <> "") Then
                sSql &= ", ServiceNotes=(ServiceNotes+'" & sNewNote & "')"
            End If  '-newnote=
            sSql &= ", TechRMStaff_id=" & CStr(mlStaffId) & ", TechStaffName='" & msStaffName & "'"
            sSql &= ", DatePromised= '" & sDatePromised & "' "  '==3083==
            sSql &= ", dateUpdated= CURRENT_TIMESTAMP "
            sSql &= " WHERE (job_id=" & CStr(mlJobId) & ") "
            '--=3101.930=  Use CMD and test for record changed..--
            '-- get a current copy of the record and check if changed..
            mTransactionUpdateJob = mCnnJobs.BeginTransaction
            If Not mbGetJobRecord(mlJobId, True, colJobFields) Then
                mbFinishInProgress = False  '-Release Roger-
                Exit Sub
            End If
            If CDate(colJobFields.Item("DateUpdated")("value")) <> CDate(mColJobFields.Item("DateUpdated")("value")) Then
                '== mCnnJobs.RollbackTrans()
                If Not (mTransactionUpdateJob Is Nothing) Then
                    mTransactionUpdateJob.Rollback()
                End If
                MsgBox("Job Record has been changed by another User during your edit.." & vbCrLf & _
                                                                 "This session is aborted..  ", MsgBoxStyle.Exclamation)
                mbFinishInProgress = False  '-Release Roger-
                Exit Sub
            End If
            '-execute-
            Try
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                '== intAffected = cmdUpd.ExecuteNonQuery()
                If Not gbExecuteSql(mCnnJobs, sSql, True, mTransactionUpdateJob, intAffected, sErrorMsg) Then
                    '-- rollback was done..
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    MsgBox("JOB record was not updated..." & vbCrLf & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                Else
                    '-ok-
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
                    mTransactionUpdateJob.Commit()
                    MsgBox("Update completed ok for Job No: " & mlJobId & vbCrLf & _
                                      "(" & CStr(intAffected) & " row(s) affected.)", MsgBoxStyle.Information)
                End If
            Catch ex As Exception
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                MsgBox("Sql Error in UPDATE Job record: " & vbCrLf & "SQL: " & _
                             sSql & vbCrLf + ex.Message, MsgBoxStyle.Exclamation)
                mTransactionUpdateJob.Rollback()
            End Try

            '== If Not gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrorMsg) Then
            '== MsgBox("Failed to UPDATE DB JOB record.." & vbCrLf & _
            '==                                     sErrorMsg & vbCrLf, MsgBoxStyle.Exclamation)
            '== Else '--ok-
            '== End If '--execute..-
            '=================
        Else '--Create New job..--
            '--  Add JobStatus to INSERT flds..--
            '----'05' or '10'.. Depending on Waitlisted or CheckedIn status..-
            sJobFinalStatus = k_statusCreated
            '===If OptBooking(0).Value = True Then  '--booking.-
            If mbIsBooking Then
                sJobFinalStatus = k_statusWaitListed
            End If
            '= 3431.0527=  Show Previous JobNo if any..
            If (mIntPreviousJobNo > 0) Then
                txtProblem.Text = "** PREVIOUS JobNo: " & mIntPreviousJobNo & ".." & vbCrLf & txtProblem.Text
            End If  '==-Prev.-

            '--  22 fields..--
            '-- Original before upgrade..-
            '----  with some fixes for vb.net.-
            sSql = "INSERT INTO Jobs "
            sSql &= " (CustomerBarcode, RMCustomer_Id, CustomerCompany, CustomerName, CustomerPhone,CustomerMobile, "
            sSql &= " Priority, JobStatus, "
            sSql &= "NominatedTech, goodsInCare, GoodsOther, GoodsExtras, "
            sSql &= "MultiAccounts, Username, UserPassword, DataBackupReqd, DataDiskReqd, "
            sSql &= "ProblemShort, ProblemLong, ProblemSymptoms, "
            sSql &= "JobReturned, SystemUnderWarranty, RcvdStaffName, RcvdRMStaff_Id, DatePromised ) "
            sSql &= " VALUES  ('" & msCustomerBarcode & "', " & CStr(mlCustomerId) & ", "
            sSql &= "'" & msFixSqlStr(msCustomerCompany) & "', "
            sSql &= "'" & msFixSqlStr(msCustomerName) & "', "
            sSql &= "'" & msCustomerPhone & "', '" & msCustomerMobile & "', "
            sSql &= "'" & msPriority & "', '" & sJobFinalStatus & "', "
            sSql &= "'" & msFixSqlStr(txtTechName.Text) & "', "
            sSql &= "'" & msFixSqlStr(VB.Left(msGoodsInCare, 250)) & "',"
            sSql &= " '" & msFixSqlStr(VB.Left(txtGoodsOther.Text, 250)) & "', "
            sSql &= "'" & msFixSqlStr(VB.Left(msExtrasInCare, 250)) & "'," & _
                              " '" & IIf((ChkUsers.CheckState = 1), "Y", "N") & "', "
            sSql &= "'" & msFixSqlStr(VB.Left(sUsernames, 32)) & "', '" & msFixSqlStr(VB.Left(sPwds, 32)) & "', "
            sSql &= "'" & IIf((ChkBackupReq.CheckState = 1), "Y", "N") & "', "
            sSql &= "'" & IIf((ChkRecovDisk.CheckState = 1), "Y", "N") & "', "
            sSql &= "'" & msFixSqlStr(VB.Left(msProblemShort, 250)) & "', '" & msFixSqlStr(msProblemDetails) & "', "
            sSql &= " '" & msFixSqlStr(VB.Left(msSymptoms, 250)) & "', "

            sSql &= "'" & IIf(chkReturned.Checked, "Y", "N") & "', "
            sSql &= IIf(chkSystemUnderWarranty.Checked, 1, 0) & ", "

            sSql &= "'" & msFixSqlStr(msStaffName) & "', " + CStr(mlStaffId) & ", "
            sSql &= "'" & sDatePromised & "' )"
            '-- go ---
            '= mCnnJobs.BeginTrans()
            mTransactionInsertJob = mCnnJobs.BeginTransaction
            If mbExecuteSql(mCnnJobs, sSql, True, mTransactionInsertJob, sErrorMsg) Then '--ok
                '--get last IDENTITY (allocated order_id)..--
                '=sSql = "SELECT IDENT_CURRENT ('dbo.jobs')"
                sSql = "SELECT CAST(IDENT_CURRENT ('dbo.Jobs') AS int);"
                If gbGetSqlScalarIntegerValue_Trans(mCnnJobs, sSql, True, mTransactionInsertJob, lngCurrentJobNo) Then
                    '= intInvoice_id = intID
                    mlJobId = lngCurrentJobNo
                    '==3203.103= INSERT Picture If Any..
                    '--  INSERT PICTURE if any into Attachments Table...---
                    If (Not (picSubjectItem.Image Is Nothing)) And _
                                      (msNewFileFullPath <> "") And (Not (mByteNewFile Is Nothing)) Then
                        If Not mClsAttachments1.InsertNewAttachment(msNewFileFullPath, msNewFileFileTitle, msNewFileFormat, _
                                                                      mlJobId, msCustomerName, "$$NewJOBImage" & vbCrLf, _
                                                                      True, mTransactionInsertJob, intAffected) Then
                            mTransactionInsertJob.Rollback()
                            MsgBox("Failed to INSERT Image for new Job record.." & vbCrLf & _
                                      "This Job is Abandoned.", MsgBoxStyle.Exclamation)
                            '= cmdSave.Enabled = False
                            cmdCancel.Text = "Exit"
                            bCancelled = True
                            mbFinishInProgress = False  '-Release Roger-
                            Exit Sub
                        Else  '-ok-
                        End If  '-insert-
                    End If  '-nothing-
                    '- Now can commit all of it..
                    '-- update invoice display later..-
                    mTransactionInsertJob.Commit()
                    '= 3203.119 REFORMAT ==
                    '- LabTicket.Text = Mid(sShortDate, 4, 4) & VB.Right(sShortDate, 2) & ":" & VB6.Format(mlJobId, "  000")
                    LabTicket.Text = VB6.Format(mlJobId, "  000") & ": " & Mid(sShortDate, 4, 8)  '== eg  "Sep-2016"--
                Else
                    MsgBox("Failed to retrieve ID id of new job record.." & vbCrLf & _
                                  "Error text:" & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    bCancelled = True
                End If
            Else '-insert failed--
                '== mCnnJobs.RollbackTrans()
                '= mTransactionInsertJob.Rollback()
                MsgBox("Failed to insert new job record.." & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                bCancelled = True
            End If '--execute--
        End If '--update/Create..-
        System.Windows.Forms.Application.DoEvents()
        If (VB.Left(sJobFinalStatus, 2) < "10") And (sJobFinalStatus <> "") Then '--Was and/or still IS WaitListed.--
            MsgBox("OK.. Job No: " & mlJobId & " has been created/updated and is now Wait-Listed..", MsgBoxStyle.Information)
            mbFinishInProgress = False  '-Release Roger-
            Me.Hide()
            Exit Sub
        End If
        cmdFinish.Enabled = False
        cmdCancel.Text = "Exit"
        '-- no more changes..--

        '== No more activty allowed..
        '== No more activty allowed..

        chkRefreshCustomer.Enabled = False
        '==FramePriority.Enabled = False
        FrameGoods.Enabled = False
        txtGoodsOther.ReadOnly = True
        FrameUsers.Enabled = False
        '== frameNominTech.Enabled = False
        frameProblem.Enabled = False
        frameInstructions.Enabled = False

        '=3403= ON-SITE- Add to Exchange-Calendar..
        '= -- 3431.0512= ON-SITE- Only if New Job, OR something chaged....
        If mbIsOnSiteJob And ((Not mbAmending) Or (mbAmending And mbDataChanged)) Then
            '- update if we have Exchange credentials defined.
            '-- Load credentials.-
            If mClsSystemInfo.exists("EXCHANGE_MAILBOX_USER") AndAlso
                                  (mClsSystemInfo.item("EXCHANGE_MAILBOX_USER") <> "") AndAlso _
                                                                          IsDate(sDatePromised) Then
                Dim datePromisedOld, datePromisedNew As Date
                Dim bIsNewJob As Boolean = (Not mbAmending)
                datePromisedNew = CDate(sDatePromised)
                Dim sBody, sCust, sLocatio As String
                If (mDatePromisedOriginal <> DateTime.MinValue) Then
                    '-- have an old date promised.
                    datePromisedOld = mDatePromisedOriginal
                Else '- no old date-
                    datePromisedOld = datePromisedNew
                End If
                Dim sDatePromisedOld = Format(datePromisedOld, "dd-MMM-yyyy hh:mm:ss tt ")  '-for DB-

                sBody = txtCustomer.Text & vbCrLf & txtGoodsOther.Text & vbCrLf
                sBody &= txtProblem.Text
                sCust = IIf((msCustomerCompany <> ""), msCustomerCompany, "") '= & msCustomerName
                sCust &= IIf((msCustomerName <> ""), " (" & msCustomerName & ")", "") '= & msCustomerName
                '-- send appointment.
                LabHelpStatus.Text = vbCrLf & "- Updating Exchange Calendar.."
                DoEvents()
                '=3431.503-  Just send to BG queue for processing..
                '==  Make an XML file with the appointment data, and 
                '--    Drop it in to the ProgramData Directory. at gsRuntimeLogPath..
                '--  gsJobMatixLocalDataDir("JobMatix34") -

                Dim sExchangeFullPath As String = Trim(gsJobMatixLocalDataDir())  '-defaults to Assembly name.
                Dim sXmlFileTitle As String = "Exchange20_Appt_JobNo_" & CStr(mlJobId) & ".xml"
                Dim sXmlFileXml As String = ""

                sExchangeFullPath &= "\temp"
                If Not My.Computer.FileSystem.DirectoryExists(sExchangeFullPath) Then  '-must create..-
                    My.Computer.FileSystem.CreateDirectory(sExchangeFullPath)
                End If '-- exists dir.-
                '-- add title-
                sExchangeFullPath &= "\" & sXmlFileTitle

                '== make xml descriptor File.
                '=3431.0622-  Now cleaned up..

                sXmlFileXml = "<Exchange_Appointment> " & vbCrLf
                sXmlFileXml &= "<appt_JobNo> "
                sXmlFileXml &= CStr(mlJobId) & "</appt_JobNo> " & vbCrLf

                sXmlFileXml &= "<appt_IsNewJob> "
                sXmlFileXml &= IIf(bIsNewJob, "Y", "N") & "</appt_IsNewJob> " & vbCrLf

                sXmlFileXml &= "<appt_TechName> "
                sXmlFileXml &= msCleanUpXMLData(txtTechName.Text) & "</appt_TechName> " & vbCrLf
                sXmlFileXml &= "<appt_Body> "
                sXmlFileXml &= msCleanUpXMLData(sBody) & "</appt_Body> " & vbCrLf
                sXmlFileXml &= "<appt_Cust> "
                sXmlFileXml &= msCleanUpXMLData(sCust) & "</appt_Cust> " & vbCrLf

                sXmlFileXml &= "<appt_DatePromisedOld> "
                sXmlFileXml &= sDatePromisedOld & "</appt_DatePromisedOld> " & vbCrLf

                sXmlFileXml &= "<appt_DatePromisedNew> "
                sXmlFileXml &= sDatePromised & "</appt_DatePromisedNew> " & vbCrLf

                '=3501.1105- -5Nov2018=  Include Value for Duration.
                sXmlFileXml &= "<appt_Duration> "
                sXmlFileXml &= CStr(numUpDownOnSiteDuration.Value) & "</appt_Duration> " & vbCrLf

                '== Finish xml descriptor File.
                sXmlFileXml &= "</Exchange_Appointment> " & vbCrLf

                '-ok.. create xml descriptor file on share..
                '-- WriteAllText overwrites any existing file..-
                Try
                    File.WriteAllText(sExchangeFullPath, sXmlFileXml)
                    '= gbSaveDocumentToEmailQueue = True
                    MessageBox.Show("NB: A Calendar Update Info XML descr. File " & vbCrLf & _
                           " has been queued OK to the Data Dir..", "New/Amend..", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("ERROR- Failed to save Exchange20 XML descr. file to Data Dir..", _
                                     "New/Amend..", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '==Exit Sub
                End Try

                '-- Send Info back to Main for exchange BG thread to be woken up..
                msExchangeCalendarUpdateXmlFileName = sExchangeFullPath
                '- all done- for Calendar-

                'If Not gbUpdateOnsiteCalendar(mlJobId, bIsNewJob, txtTechName.Text, _
                '                          sBody, sCust, "--", _
                '                                datePromisedOld, datePromisedNew, mClsSystemInfo) Then
                '    MsgBox("Failed to add/Update ON-SITE Appointment to calendar.", MsgBoxStyle.Exclamation)
                'Else
                '    MsgBox("ON-SITE Appointment was added/updated to calendar.", MsgBoxStyle.Information)
                'End If  '-send-

            End If '-credentials.-
        End If  '-onsite-
        '=3403= END of ON-SITE- Add to Exchange-Calendar..
        LabHelpStatus.Text = ""

        '==cmdPrintAgreement.Enabled = True
        '==cmdPrintLabel.Enabled = True
        '===If mbAmending Then  '--printing optional..-
        '===  If Not MsgBox("Print the Service Agreement?", vbYesNo + vbDefaultButton1 + vbQuestion) = vbYes Then
        '==Me.Hide
        '===      cmdCancel.Enabled = True  '--now EXIT..--
        '===      cmdCancel.SetFocus   '--EXIT..--
        '===      Exit Sub  '--all done..-
        '===  End If
        '===End If  '--amending..-
        If Not mbIsBooking Then
            cmdPrintAll.Enabled = True
            Call cmdPrintAll_Click(cmdPrintAll, New System.EventArgs())
        End If '--booking.-
        '===Call cmdPrintAgreement_Click
        '===Call cmdPrintLabel_Click
        cmdCancel.Enabled = True '--now EXIT..--
        mbFinishInProgress = False  '-Release Roger-
        cmdCancel.Focus() '--EXIT..--
        Exit Sub

cmdFinish_error:
        lngError = Err().Number
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox("Runtime Error in cmd FINISH.." & vbCrLf & _
                      "Error=" & lngError & ": " & ErrorToString(lngError), MsgBoxStyle.Critical)
        '==Me.Hide
        mbFinishInProgress = False  '-Release Roger-

    End Sub '--finish--
    '= = = = = =  = =
    '-===FF->

    '--cancel New job or cancel update..
    '---- OR EXIT..--

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        Dim sMsg As String

        If (cmdCancel.Text = "Exit") Then
            Me.Hide()
            Exit Sub
        Else
            sMsg = "Abandon this new job ?"
            If mbAmending Then sMsg = "Abandon changes ?"
            '== If (mlCustomerId < 0) Then '--still emppty..-
            '== Me.Hide()
            '==     Exit Sub
            '== Else
            If mbDataChanged Then
                '-- confirm if job is to be abandoned..--
                If Not MsgBox(sMsg, _
                            MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    '== txtRcvdName.Focus()
                    Exit Sub '--was mistake..  keep going..
                Else '--yes--
                    Me.Hide()
                    Exit Sub
                End If
            Else '--no change...-
                '-- then exit..--
                Me.Hide()
                Exit Sub
            End If '--started..-
        End If '-exit..-
    End Sub '--cancel--
    '= = = = = =  = = =

    '--Cancel JOB completely..--
    '--- UPDATE and Set CANCELLED status.-

    Private Sub cmdCancelJob_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles cmdCancelJob.Click
        Dim sMsg As String
        Dim sSql, sErrors As String
        Dim lngaffected As Integer

        sMsg = " Are you sure you want to COMPLETELY CANCEL this Job ?"
        '---If mbAmending Then sMsg = "Abandon this update ?"
        If mbAmending Then  '== mlCustomerId >= 0 Then '--started
            '-- confirm if job is to be abandoned..--
            If Not MsgBox(sMsg, MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '== txtRcvdName.Focus()
                Exit Sub '--was mistake..  keep going..
            Else '--yes--
                '-- Update and set status..-
                sSql = "UPDATE [jobs] SET jobStatus='" & k_statusCancelled & "' "
                sSql = sSql & ", TechRMStaff_id=" & CStr(mlStaffId) & ", TechStaffName='" & msStaffName & "'"
                sSql = sSql & ", dateUpdated= CURRENT_TIMESTAMP "
                sSql = sSql & " WHERE (job_id=" & CStr(mlJobId) & ") "
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                If Not gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrors) Then
                    '--mCnnJobs.RollbackTrans
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    MsgBox("Failed to Set Cancelled Status on DB JOB record.." & vbCrLf & sErrors & vbCrLf, MsgBoxStyle.Exclamation)
                Else '--ok-
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
                    MsgBox("Job No: " & mlJobId & " has been Cancelled.. " & vbCrLf & _
                                "( " & lngaffected & " Record(s) affected..)", MsgBoxStyle.Information)
                End If '--execute..-
                Me.Hide()
                Exit Sub
            End If
        Else '--not started..-
            '-- then exit..--
            Me.Hide()
            Exit Sub
        End If
    End Sub '--cancel job--
    '= = = = = =  = = =


    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmNewJob3_FormClosing(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim sMsg As String

        If (cmdCancel.Text = "Exit") Then
            Me.Hide()
            Exit Sub
        Else
            sMsg = "Abandon this new job ?"
            If mbAmending Then sMsg = "Abandon changes ?"
            Select Case intMode
                Case System.Windows.Forms.CloseReason.WindowsShutDown, System.Windows.Forms.CloseReason.TaskManagerClosing, System.Windows.Forms.CloseReason.FormOwnerClosing '==  NOT for vb.net.- , vbFormCode
                    intCancel = 0 '--let it go--
                Case System.Windows.Forms.CloseReason.UserClosing
                    If mlCustomerId = -1 Then '--still emppty..-
                        intCancel = 0 '--let it go--
                    ElseIf mbDataChanged Then
                        '--customer was selected..-
                        '-- confirm if job is to be abandoned..--
                        If MsgBox(sMsg, MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            intCancel = 0 '--let it go--
                        Else
                            intCancel = 1 '--cant close yet--'--was mistake..  keep going..
                        End If '--yes.--
                    Else '--no changes..-
                        intCancel = 0 '--let it go--
                    End If
                Case Else
                    intCancel = 0 '--let it go--
            End Select '--mode--
        End If '--exit.-
        eventArgs.Cancel = intCancel
    End Sub '--unload--
    '= = = = = = = = = = = =

    '=== End of form ==
    '=== end of form. ==


End Class   '-frmNewJob3.=
'= = = = = = = = = = = = = = = =