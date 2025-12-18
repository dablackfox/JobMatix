Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'== Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports system.data.sqlclient
Imports System.Data.OleDb
Imports System.Diagnostics
Imports System.Threading

Public Class ucChildCustomer

    '-- JobMatix POS 3.0 --
    '--  Customer Details and Maint...--

    '-- NB:  textBox and CheckBox controls
    '--      have the DB Column name set in the "Tag" property..

    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==   grh- JobMatix POS3 3.1.3101.1019-
    '==      >>  Add tabs for showing Serials, GoodsReceived and Sales..
    '==
    '==   grh- JobMatix POS3 3.1.3103.0125-
    '==      >>  CLONED from frmStock !!! 
    '==      >>   3.1.3103.0203-  Checkbox to show outstanding invoices only.. 
    '==      >>    and Fix Browse Refresh crash (srch should Activate, not refresh..
    '==
    '==   grh- JobMatix POS3 3.1.3103.0205-
    '==      >>  Barcode now Auto-generated !!! 
    '==
    '==  grh. JobMatix 3.1.3107.0805 ---  05-Aug-2015 ===
    '==   >> Now for .Net 4.5.2- 
    '==
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==        >>  Cleanup LocalSettings and SysInfo using classes..
    '==
    '==     v3.3.3301.705..  05-July-2016= ===
    '==        >>  Cleanup Data Entry problems...
    '==        >> ( NOT RESOLVED yet) To Save Barcode of New Cust created (if any)-
    '==
    '==     v3.4.3401.319.  19-Mar-2107= ===
    '==        >>  Fixed browse column order (downgrade customer_id)...
    '== = = = = = = = =
    '==
    '==     3403.728/731- 28-31-July2017-
    '==      -- Customer Admin Form..  fix barcode/customer_id mistake in grid row selection...
    '==                     AND make form re-sizable.
    '==      -- Add OK and Exit buttons to enable selection return...
    '==
    '==
    '==     3403.0918- 15/16/17/18-Sep-2017-
    '==      -- POS Setup/Options. Add Cust.Pricing Grade Rates...0..4.
    '==      -- Customer details form-  
    '==            Add dropdown for Cust.Pricing Grade Rates...0..4.
    '==
    '==     3411.1119- Update calling showInvoice to give staff_id.-
    '==
    '==     3411.0124=  Upgrade frmCustomer to allow being called just to Add NEW CUSTOMER...
    '==                  AND-   Fix New Customer commit to retry in case of failed UNIQUE Barcode. 
    '==                    
    '==     3411.0408=  08-Apr-2018- 
    '==        --New Customer- Change of rules.  
    '==                            Required at least one name, and one contact no. (or email)..
    '==        --Old Customer- Allow upgrade to Account Status..  
    '==
    '==
    '==     3501.1024- 24-Oct-2018-
    '==      -- Editing mode..  Enable Credit Limit/Days fields if AccountCust checked...
    '==            ALSO fix problem with CreditDays not being committed.
    '==
    '==   Updated.- 3519.0221  Started 21-Feb-2019= 
    '==     -- Fixes Customer Admin (Shown) to stop "8664" from appearing in the Find Textbox on startup.... 
    '==     -- Fixes Customer Admin-  Catch ENTER key for Full Text Search.... 
    '==
    '==   Updated.- 3519.0227  Started 27-Feb-2019= 
    '==        --  Show all Invoices for Customer. (Not just on-account.)
    '==
    '== - - - - RELEASED as 3519.0414 --
    '==  
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==
    '==    First New Build- 4201.0416 -
    '==    New Build- 4201.0416 -  Started 16-April-2019.
    '==
    '==   Updated.- 4201.0428- 
    '==     --for TDI Admin forms are now in Tabs inside Main Form...
    '==    -- 4201.0426. TDI Child forms now converted to UserControls.... 
    '==    -- 4201.0528  Replacing me.hide with "call close_me"
    '==
    '==
    '== NEW revision-
    '==    -- 4201.0727.  Started 25-July-2019-
    '==      --  For the Customer Admin screen, on INVOICES Tab (grid) 
    '==                add right-click context menu to copy  Invoice Number to the clipboard.  
    '==           AND on the Item Sales Tab (grid) add right-click context menu to copy  the Item Barcode to the clipboard. 
    '== = = = = = = =
    '==
    '== NEW revision-
    '==    -- 4201.0830.  Started 28-August-2019-
    '==        -- FIXes to frmCustomer (AND to child ucChildCustomer)..
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1121.  06-Nov-2019-  Started 06-November-2019-
    '==      -- Customer Admin.. Add Tabs for a/c Payments, Quotes AND Jobs.  
    '==      -- INCLUDE Ref to JMxRetailHost.dll for 
    '==                  access to modRetailHostIF and clsRetailHostJMPOS in project so POS can access View Job. 
    '==
    '==
    '== NEW Revision-
    '==   -- 4219.1214.  14-Dec-2019-  Started 10-Dec-2019-
    '==        Customer Admin- ucChildCustomer-  
    '==           --  Add code to delay Find Text in case of starting with "1" (General) in case more digits coming,
    '==                  Because going straight off and finding all trans. for General is very slow.
    '==           --  ALSO  Add code to Cust Grid list RowEnter to stop ReEntrancy crash.
    '==         --  ALSO  Add "Start New Job" buttons  and Checkin/Amend context Menu  to Customer Jobs Panel
    '==                  to launch an instance of "ucChildNewJob" UserControl inside a new Tab...
    '==
    '==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
    '==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
    '==    
    '==   == 4221.0205.  05-Feb-2020- 
    '==   --  Tags- 05-Feb-2020.. For Build 4221..
    '==         -- Add clsTags, and forms for Ref Tags and Cust tags..
    '==         -- Add Column  "Tags" to Customer Table in the StartUp class clsJMxPOS31..
    '==         -- Add buttons and subs to Customer admin form to update/show Tags....
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==   New Build 4233.0421. PREPED in Devel FOR- Updates to 4221.0207  Started 24-March-2020= 
    '==
    '==  --  ucChildCustomer-  Fix for Barcode "1" pause not working first time in..
    '==          IN the event "timerFind_Tick", drop the test for valid customer_id around call to Browse.Find..
    '==  --  --  ALSO, in function "mbPostNewJobRequest" add a space into the FirstName+ LastName assembly.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '== 
    '==   Updates to 4233.0421  Started 24-April-2020= 
    '==   Updates to 4233.0421  Started 24-April-2020= 
    '==
    '==  Target is new Build 4234..
    '==  Target is new Build 4234..
    '==
    '==   3.  CustomerAdmin-  Creating new Job in POS-  in function "mbPostNewJobRequest"  AGAIN..
    '==          -- Set CustomerName as "lastName, firstName", as per JobTracking..  (Fixes previous Fix)
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==
    '==  Target-New-Build-4257..
    '==  Target-New-Build-4257..  07-July-2020.
    '== 
    '==   4. Cust-Admin Jobs Tab-  Fix OnSite Job Button text.. So OnSite is not broken..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== Fixes to Build 4259.0730   (Started 14-Aug-2020)
    '==
    '==   Target-New-Build 4262 --
    '==   Target-New-Build 4262 -- (Started 14-Aug-2020)
    '==   Target-New-Build 4262 -- (Started 14-Aug-2020)
    '==   Target-New-Build-4262 -- 
    '==   Target-New-Build-4262 -- 
    '==
    '==      --  JobTracking has MADE Form "frmJobMaint32" INTO USERCONTROL- ucChildJobMaint..
    '==              --  MAKE NEW Form "frmJobMaintBase" to hold USERCONTROL.
    '--                  SO THAT we show it as a Child in a POS TAB..
    '==               -- SO now From Customer Admin we can use the UserControl in a TAB page..
    '==     Target-New-Build-4262 -- 
    '==    A.   --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
    '==         --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)
    '==
    '==     Target-New-Build-4262 -- 
    '==    B. --  Account Invoice Reversal- 
    '==         -- Customer Admin to show "REVERSED" instead of Outstanding invoice amt if Invoice was reversed.. 
    '==            AND 1. ADD mnu Items to Active Jobs Context menu to View/Update Job via new ucChildJobMaint UserControl.  
    '==                2. Add Phone Nos. to Cust. search args..
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '== Fixes to Build 4262.0826  
    '==
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==       After Editing Customer Details.. make sure same cust can be selected for re-editing if needed.
    '== 
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '==   Target-New-Build-4284-EXTRA-EXTRA --  (20-Nov-2020 +)
    '==   Target-New-Build-4284-EXTRA-EXTRA --  (20-Nov-2020)
    '==
    '==  B.  -- Customer Admin..  Speed up loading Invoices Grid using DGV.AddRange...
    '==           ALSO-  Fix Resizing for Min size needed for RHS details TabControl
    '==      -- modPos32Support-- Use new gbCollectCustomerInvoicesEx2 using Shaping to get Invoices/Disbursements..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    '== UPDATES to Build 4284.1124  
    '==
    '==   Target-New-Build-4287 -- (30-Jan-2021)
    '==
    '== (B) POS Customer Admin-  New Customer entry...  Jerami-28-Jan-2021-
    '==      ADD tick box (exclude inactive) For customer search..
    '==        Fix Search functions.. mbInitialiseBrowse, mbBrowseCustomerTable.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==

    '--- For suppressing default popup menu..-
    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = =

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    '-- INVOICES DataGridView columns.--
    '==   Target-New-Build-4262 -- 
    Private Const k_INVGRIDCOL_INV_NO As Short = 0
    Private Const k_INVGRIDCOL_INV_DATE As Short = 1
    '== END Target-New-Build-4262 -- 


    Private Const k_INVGRIDCOL_TRANTYPE As Short = 2
    Private Const k_INVGRIDCOL_ON_ACCOUNT As Short = 3
    Private Const k_INVGRIDCOL_INV_TOTAL As Short = 4
    Private Const k_INVGRIDCOL_PREV_PAID As Short = 5
    Private Const k_INVGRIDCOL_OUTSTANDING As Short = 6

    Private Const k_RequiredFieldsMsg As String = "Required: at least one Customer (or Company) Name, " & vbCrLf & _
                                "   and at least one Contact Number (or email).."

    '= = = = = = = = = = = = = = = = = = = = = = = = =

    Private mbActivated As Boolean = False

    '--inputs--
    Private msVersionPOS As String = ""
    Private mIntFormDesignWidth As Integer
    Private mIntFormDesignHeight As Integer

    Private msServer As String
    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '-- DB schema info the Stck Table Columns..-
    Private mDataTableColumns As DataTable
    Private mColColumnDataTypes As Collection  '-- Col. name is key-- data = sqlTYpe-

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection '--
    Private mlJobId As Integer = -1

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    Private mbAddNewCustomerOnly As Boolean = False  '--dive straight into New Customer.

    Private mbIsInitialising As Boolean = True
    Private mbIsCancelled As Boolean = False

    '-CAN input customer-id-
    Private mIntCustomer_id As Integer = -1
    Private mbIsAccountCustomer As Boolean = False   '---- AccountCust being added or updated--


    '==3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo

    Private msDefaultPrinterName As String = ""

    '== Private mDecGSTPercentage As Decimal = 0
    '==Private mDecSellMargin As Decimal = 0

    '=3403.918=  Priving Grades.
    Private mColPricingGrades As Collection
 
    Private mIntForm_top As Integer = -1
    Private mIntForm_left As Integer = -1

    Private msRequiredFields As String = ""
    Private msRequiredFields2 As String = ""

    '--  Current Item--
    Private mbIsNewItem As Boolean = False

    Private mbItemIsLoading As Boolean = False

    Private msModifiedControls As String = ""  '--names of controls modified.-

    '-  list now in dataGridView -
    Private mColPrefsCustomer As Collection
    Private mBrowse1 As clsBrowse3
    Private mLngSelectedRow As Integer = -1

    Private mDecTotalInvoices As Decimal = 0
    Private mDecTotalOutstanding As Decimal = 0

    '==3301.705=
    '== NOT RESOLVED yet- Private msLastNewCustomerBarcode As String = ""

    '=3403-
    Private msSelectedCustomerBarcode As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = = 

    '--  Popup menu for Right click on sales Invoices Grid...-
    '--  Popup menu for Right click on Sales Invoices Grid...-
    Private mContextMenuInvoiceInfo As ContextMenu
    Private WithEvents mnuCopyItemInvoiceNo As New MenuItem("Copy Item Invoice No.")
  
    '-- mContextMenuSalesInfo-
    Private mContextMenuSalesInfo As ContextMenu
    Private WithEvents mnuCopyItemBarcode As New MenuItem("Copy Item Barcode.")

    '=4219.1214-- Jobs Grid Context menu stuff..
    '--  Popup menu for Right click on Job Node in Jobs TreeView..-
    Private mContextMenuJobsAction As ContextMenu
    '=Private WithEvents mnuJobActionTfrToNewCust As New MenuItem("Transfer Job To New Cust.") '==3323.1109=
    Private WithEvents mnuJobActionCheckIn As New MenuItem("Check-in Job")
    '=Private WithEvents mnuJobActionReturnToWaitlist As New MenuItem("Return Job To WaitList")  '=3203.211=
    Private WithEvents mnuJobActionAmend As New MenuItem("Amend Service Agreement")

    Private WithEvents mnuJobActionSep1 As New MenuItem("-")

    '==     Target-New-Build-4262 -- 
    '==      --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
    '==      --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)
    Private WithEvents mnuJobActionView As New MenuItem("View Service Record")
    Private WithEvents mnuJobActionSep2 As New MenuItem("-")
    Private WithEvents mnuJobActionUpdate As New MenuItem("Update Job Record")
    Private WithEvents mnuJobActionDeliver As New MenuItem("Deliver Job")
    Private WithEvents mnuJobActionSep3 As New MenuItem("-")


    '= = = = = = = = = = = = = = = = = = = == = 

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport
 
    '= = = = = = = = = = = = = = = = = = = = == = 
    '-===FF->

    '-- P r o p e r t i e s --
    '-- P r o p e r t i e s --

    WriteOnly Property VersionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
        End Set
    End Property
    '= = = = = = = = = = = = = = 


    '-- Staff Id now comes from caller..--

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    WriteOnly Property StaffId() As Integer
        Set(ByVal Value As Integer)

            mIntStaff_id = Value
        End Set
    End Property '--id.-
    '= = = = = = = = = = = =  =

    '-- Sql Connection Info from Sub Main..-
    '-- Sql Connection Info from Sub Main..-

    WriteOnly Property SqlServer() As String
        Set(ByVal Value As String)
            msServer = Value
        End Set
    End Property '--server-
    '= = = = = =  = = =  = =

    '== WriteOnly Property SqlServerComputer() As String
    '=     Set(ByVal Value As String)
    '==         msSqlServerComputer = Value
    '==     End Set
    '== End Property '--server-
    '= = = = = =  = = =  = =

    WriteOnly Property connectionSql() As OleDbConnection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =
    '= = = = = = =  = = = = =

    WriteOnly Property dbInfoSql() As Collection
        Set(ByVal Value As Collection)
            mColSqlDBInfo = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

    WriteOnly Property DBname() As String
        Set(ByVal Value As String)
            msSqlDbName = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = 

    WriteOnly Property form_top() As Integer
        Set(ByVal value As Integer)
            mIntForm_top = value
        End Set
    End Property  '--top-
    '= = = = = = = = = = = = = 

    WriteOnly Property form_left() As Integer
        Set(ByVal value As Integer)
            mIntForm_left = value
        End Set
    End Property  '--left-
    '= = = = = = = = = = = = = 

    WriteOnly Property customer_id() As Integer
        Set(ByVal value As Integer)
            mIntCustomer_id = value
        End Set
    End Property  '--left-
    '= = = = = = = = = = = = = 

    WriteOnly Property AddNewCustomerOnly As Boolean
        Set(value As Boolean)
            mbAddNewCustomerOnly = value
        End Set
    End Property  '-AddNewCustomerOnly-
    '= = = = = = =  = = = = = = = = = 
    '-===FF->

    '--results-

    '- result of selection..
    Public ReadOnly Property selectedBarcode As String
        Get
            selectedBarcode = msSelectedCustomerBarcode
        End Get
    End Property '-invoice-
    '= = = = = = = = = = = = = = = = = = = = = = = =

    Public ReadOnly Property wasCancelled As Boolean
        Get
            wasCancelled = mbIsCancelled
        End Get
    End Property  '-cancelled--
    '= = = = = = = = = = = = = = = = = = = = = = == = = 
    '-===FF->

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)

    Public Event PosChildNewJobRequest(ByVal strChildName As String, _
                                       ByVal bIsCheckin As Boolean, _
                                       ByVal bIsAmending As Boolean, _
                                       ByVal bIsNewOnSiteJob As Boolean, _
                                       ByVal intCustomer_id As Integer, _
                                       ByVal intJob_id As Integer, _
                                       ByVal strCustomerBarcode As String, _
                                       ByVal strCustomerCompany As String, _
                                       ByVal strCustomerName As String, _
                                       ByVal strCustomerPhone As String, _
                                       ByVal strCustomerMobile As String)

    '= = = = = = = = = = = = = = = = = = = = == = = = = = == = = = = = == =

    '==  Target-New-Build-4262 -- 
    '==      --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
    '==      --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)

    '-- EVENT to get Child JobMaint UserControl loaded..

    Public Event PosChildJobMaintRequest(ByVal strChildName As String, _
                                       ByVal bIsServiceUpdate As Boolean, _
                                       ByVal bIsDeliveryUpdate As Boolean, _
                                       ByVal intCustomer_id As Integer, _
                                       ByVal strCustomerBarcode As String, _
                                       ByVal intJob_id As Integer)

    '== END  Target-New-Build-4262 -- 
    '= = = = = = = = = = = = = = = = = = = = == = = = = = == = = = = = == =

    '= = = = = = = = = = = = = = = = = = = = == = = = = = == = = = = = == =
    '-===FF->

    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                            ByVal intParentHeight As Integer)

        Me.Width = intParentWidth - 11
        Me.Height = intParentHeight  '= - 11
        '-- resize our controls..
        DoEvents()
        '-- resize main box and top panel-

        grpBoxCustomer.Width = Me.Width - 5  '= panelBanner.Width
        '=4221.0207=  grab back some height.
        grpBoxCustomer.Height = Me.Height - 12  '- 48  '= 120  '=93

        '= labDLLversion.Top = grpBoxCustomer.Top + grpBoxCustomer.Height + 27
        '= DoEvents()  '--time to adjust contents.

        '==   Target-New-Build-4262 --
        TabControlCustomer.Width = (grpBoxCustomer.Width / 2) - 24
        '== END  Target-New-Build-4262 --

        '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
        '- Needs min size..
        If (TabControlCustomer.Width < 520) Then
            TabControlCustomer.Width = 520
        End If
        '== END Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)

        '--move customer details.
        TabControlCustomer.Left = grpBoxCustomer.Width - TabControlCustomer.Width - 7
        TabControlCustomer.Height = grpBoxCustomer.Height - TabControlCustomer.Top - 13

        panelCustHdr.Left = TabControlCustomer.Left

        FrameBrowse.Width = grpBoxCustomer.Width - TabControlCustomer.Width - 13
        FrameBrowse.Height = grpBoxCustomer.Height - FrameBrowse.Top - 12
        panelBanner.Width = FrameBrowse.Width

        dgvCustomerList.Width = FrameBrowse.Width - 11
        dgvCustomerList.Height = FrameBrowse.Height - dgvCustomerList.Top - 12

        dgvInvoices.Height = TabPageInvoices.Height - dgvInvoices.Top - 22

        '==  Target-New-Build-4262 --
        Dim intGridHeight = TabPageInvoices.Height - dgvInvoices.Top - 15
        dgvInvoices.Width = TabPageInvoices.Width - 10
        labInvoiceReversed.Top = TabPageInvoices.Height - 17
        dgvSales.Width = dgvInvoices.Width
        dgvPayments.Width = dgvInvoices.Width
        dgvPayments.Height = intGridHeight
        '== END  Target-New-Build-4262 --

        '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
        dgvQuotes.Width = dgvInvoices.Width
        dgvJobs.Width = dgvInvoices.Width
        '==  END Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)

        '= dgvSales.Height = dgvInvoices.Height
        dgvSales.Height = intGridHeight '= New-Build 4262 15

        '= btnOK.Left = panelCustHdr.Left
        '= btnOK.Top = Me.Height - 40  '= labDLLversion.Top - 17

        btnExit.Left = grpBoxCustomer.Width - btnExit.Width - 7  '= btnOK.Left + 240
        '= btnExit.Top = btnOK.Top

    End Sub '-SubFormResized
    '= = = = = = = = = = = =  === =
    '-===FF->

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click....

    Public Function SubFormCloseRequest() As Boolean
        '== Call close_me()
        SubFormCloseRequest = False

        '- Ask if ok to close, and retutn result..

        If (msModifiedControls <> "") Then
            If (MsgBox("Abandon changes ?" & vbCrLf & "(" & msModifiedControls & ")..", _
                          MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
                '- no, don't close.
                SubFormCloseRequest = False
            Else '-yes- ok to close-
                SubFormCloseRequest = True
            End If
        Else  '-no change.
            SubFormCloseRequest = True
        End If  '- modified.-

    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    'Private Function mDecComputeAmountExTax(ByVal decGrossAmount As Decimal) As Decimal

    '    mDecComputeAmountExTax = Decimal.Truncate((decGrossAmount * (100 / (100 + mDecGSTPercentage))) * 100) / 100
    'End Function '-- mDecComputeAmountExTax-
    '= = = = = = = = = = = =  = = = == = ==

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef sInstr As String) As String

        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =

    '-- Get STRING Select Value -- (cmd.getScalar)--

    Private Function mbGetSqlScalarStringValue(ByRef cnnSql As OleDbConnection, _
                                           ByVal sSql As String, _
                                          ByRef strResult As String) As Boolean
        Dim sqlCmd1 As OleDbCommand
        '==Dim intResult As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbGetSqlScalarStringValue = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            strResult = Convert.ToString(sqlCmd1.ExecuteScalar)
            mbGetSqlScalarStringValue = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarStringValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarIntegerValue-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    Private Function mbGetSqlScalarIntValue(ByRef cnnSql As OleDbConnection, _
                                             ByVal sSql As String, _
                                            ByRef intResult As Integer) As Boolean
        Dim sqlCmd1 As OleDbCommand
        '==Dim intResult As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbGetSqlScalarIntValue = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            intResult = sqlCmd1.ExecuteScalar
            mbGetSqlScalarIntValue = True   '--ok--
            '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbGetSqlScalarIntValue: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarIntegerValue-
    '= = = = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--GetLastCustomerIdent-
    '==  GLOBAL- CURRENT --

    Private Function mbGetLastCustomerIdent_Current(ByRef intIdent As Integer) As Boolean
        Dim sSql As String
        Dim intId As Integer

        mbGetLastCustomerIdent_Current = False
        '-  Retrieve customer_id:  (IDENTITY of Customer record written.)-
        sSql = "SELECT CAST(IDENT_CURRENT ('dbo.customer') AS int);"
        If mbGetSqlScalarIntValue(mCnnSql, sSql, intId) Then
            intIdent = intId
            '-- update invoice display later..-
            mbGetLastCustomerIdent_Current = True
            '== MsgBox("OK..  Added new Customer record..." & vbCrLf & _
            '=          "Customer_id is " & intIdent, MsgBoxStyle.Information)
        Else
            '== MsgBox("Failed to retrieve latest Customer No..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
    End Function '-GetLastCustomerIdent_Current-
    '= = = = = = = = = = = = = = = = = = =


    '--GetLastCustomerIdent-
    '-- IN OUR SCOPE --

    Private Function mbGetLastCustomerIdent(ByRef intIdent As Integer) As Boolean
        Dim sSql As String
        Dim intId As Integer

        mbGetLastCustomerIdent = False
        '-  Retrieve customer_id:  (IDENTITY of Customer record written.)-
        '== sSql = "SELECT CAST(SCOPE_IDENTITY ('dbo.customer') AS int);"
        sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
        If mbGetSqlScalarIntValue(mCnnSql, sSql, intId) Then
            intIdent = intId
            '-- update invoice display later..-
            mbGetLastCustomerIdent = True
            '== MsgBox("OK..  Added new Customer record..." & vbCrLf & _
            '=          "Customer_id is " & intIdent, MsgBoxStyle.Information)
        Else
            '== MsgBox("Failed to retrieve latest Customer No..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
    End Function '-GetLastCustomerIdent-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  GET READY for Function KEY..
    '--- INITIALISE customer Browser.for Lookup--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(Optional ByVal sSrchWhereCond As String = "") As Boolean

        '=Dim colPrefs As Collection
        Dim sHostTablename As String
        Dim sWhere As String

        mBrowse1 = New clsBrowse3 '== clsBrowse22
        '-- show full frame..-
        '== FrameBrowse.Height = (LabDescription.Top - FrameBrowse.Top - 4)
        '== DataGridView1.Height = (FrameBrowse.Height - DataGridView1.Top - 8)

        mBrowse1.connection = mCnnSql  '= mRetailHost1.connection
        mBrowse1.colTables = mColSqlDBInfo '= mRetailHost1.colTables 
        mBrowse1.IsSqlServer = True   '= mRetailHost1.IsSqlServer
        mBrowse1.DBname = msSqlDbName  '= mRetailHost1.DBname

        '--  get table/prefs info for this host..--
        mBrowse1.tableName = "customer"  '==sHostTablename

        '= mBrowse1.FlexGrid = MSHFlexGrid1
        mBrowse1.DataGrid = dgvCustomerList

        '--  pass controls..--
        mBrowse1.showRecCount = labRecCount '--updates rec. retrieval..
        mBrowse1.showFind = LabFind '--updates Sort Column display..
        mBrowse1.showTextFind = txtFind '--updates Sort Column display..
        '= sWhere = msMakeStockFilter()  '--service or not..-

        '==  Target-New-Build-4287 -- (30-Jan-2021)
        '--  Exclude Inactive "by default."
        sWhere = ""
        If (Not chkIncludeInactiveCust.Checked) Then
            sWhere = " (inactive = 0) "
        End If
        '==  END Target-New-Build-4287 -- (30-Jan-2021)

        '-- add srch args..
        If (sSrchWhereCond <> "") Then
            If (sWhere <> "") Then
                sWhere &= " AND "
            End If
            sWhere &= sSrchWhereCond
        End If
        mBrowse1.WhereCondition = sWhere
        mBrowse1.PreferredColumns = mColPrefsCustomer '== mColPrefs
        mBrowse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly
        FrameBrowse.Enabled = True

        mLngSelectedRow = -1
        mBrowse1.Activate() '-- go..--

        '== txtFind.Focus()

    End Function '--init-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  F2 was pressed..  Browse for customer code..--
    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--

    Private Function mbBrowseCustomerTable(Optional ByRef sSrchWhereCond As String = "") As Boolean
        Dim sWhere As String = ""

        If (mBrowse1 Is Nothing) Then
            Call mbInitialiseBrowse()
        Else
            sWhere = ""  '=LATER=   msMakeStockFilter()  '--service or not..-

            '==  Target-New-Build-4287 -- (30-Jan-2021)
            '--  Exclude Inactive "by default."
            sWhere = ""
            If (Not chkIncludeInactiveCust.Checked) Then
                sWhere = " (inactive = 0) "
            End If
            '==  END Target-New-Build-4287 -- (30-Jan-2021)

            '-- add srch args..
            If (sSrchWhereCond <> "") Then
                If sWhere <> "" Then
                    sWhere &= "AND "
                End If
                sWhere &= sSrchWhereCond
            End If
            mBrowse1.WhereCondition = sWhere '-- sWhere -
            '== mBrowse1.refresh()
            '==3103-203==
            mBrowse1.Activate()  '==3103-203==
        End If
        txtFind.Focus()

        System.Windows.Forms.Application.DoEvents()
    End Function  ''--mbBrowseTable--
    '= = = = = =  = == =
    '-===FF->

    Private mRetailHostPOS1 As _clsRetailHost
    '-- Show JobMaint Form..

    Private Function mbShowJobMaintForm(ByVal intJob_id As Integer) As Boolean
        '-- Multi-Retail-Host--
        Dim frmJobMaint3A As frmJobMaint3

        mbShowJobMaintForm = False
        '--  create new JobMatixPOS interface class..
        mRetailHostPOS1 = New clsRetailHostJMPOS

        '--Initialise:  Pass sql connection, dbname and colTables..
        Try
            Call mRetailHostPOS1.SetConnection(msServer, mCnnSql, mColSqlDBInfo, msSqlDbName)
        Catch ex As Exception
            MsgBox("Error- Failed to Connect to Jobs." & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        frmJobMaint3A = New frmJobMaint3
        '--load parms.. and show form..
        Try
            frmJobMaint3A.JobNo = intJob_id
            frmJobMaint3A.connectionSql = mCnnSql
            '==frmJobMaint2.connectionJet = mCnnJet
            frmJobMaint3A.retailHost = mRetailHostPOS1

            frmJobMaint3A.dbInfoSql = mColSqlDBInfo
            '== frmJobMaint2.dbInfoJet = mColJetDBInfo
            frmJobMaint3A.ServiceUpdate = False '-- request service type--
            frmJobMaint3A.DeliveryUpdate = False '-- delivery type--
            '== frmJobMaint3A.NotifyUpdate = False
            frmJobMaint3A.StaffId = mIntStaff_id
            frmJobMaint3A.StaffName = msStaffName
            frmJobMaint3A.StaffBarcode = msStaffName

            '== frmJobMaint3A.UserLogo = Picture2.Image '--pass logo..-
            '--position--
            frmJobMaint3A.MandatedFormTop = Me.Top + 30  '== 170  '== (Me.Height \ 5) + 50
            frmJobMaint3A.MandatedFormLeft = Me.Left + (Me.Width \ 5)

            '= VB6.ShowForm(frmJobMaint3A, VB6.FormShowConstants.Modal, Me)
            frmJobMaint3A.ShowDialog()

            frmJobMaint3A.Close()
            mbShowJobMaintForm = True
        Catch ex As Exception
            MsgBox("Error- Failed to Load/Show Jobs Form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
    End Function  '--showJobMaint..
    '= = = = = = = == = = = = = = =
    '-===FF->

    '--  mbRefreshInvoiceList --

    '==     Target-New-Build-4262 -- 
    Private mColAllAccountReversals As Collection
    Private imageDummy As Image
    '==     --  Account Invoice Reversal- 
    '==     -- Customer Admin to show "REVERSED" 
    '==          instead of Outstanding invoice amt if Invoice was reversed.. 
    '==


    Private Function mbRefreshInvoiceList(ByVal intCustomer_Id As Integer) As Boolean
        Dim s1, sSql, sPayList, sTranType As String
        Dim int1, intHeight, intInvoice_id As Integer
        Dim colInvoices, colInvoice1 As Collection
        Dim col1, col2, colPayList As Collection
        Dim bIsRefund, bIsOnAccount As Boolean
        Dim decInvoiceTotal, decPaymentAmount As Decimal
        Dim decPaymentTotal, decAmountOutstanding As Decimal

        '=  labHelp.Text = " Loading and checking Customer Invoices."


        '==     Target-New-Build-4262 -- 
        '==     --  Account Invoice Reversal- 
        '== -- Payments Form needs ReversedInvoices to be filtered out..
        '=mColAllAccountReversals-
        Dim intReversedInvoiceRefund_id As Integer
        Dim colRefund As Collection
        Dim sReversedInfo As String = ""
        Dim clsDebtors1 As clsDebtors
        Dim listInvoicesReversals As New List(Of Integer)
        clsDebtors1 = New clsDebtors(mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                         mColPrefsCustomer, msVersionPOS, imageDummy, mIntStaff_id, msStaffName)
        If Not clsDebtors1.CollectAllAccountReversals(mColAllAccountReversals) Then
            '- error..  take it as none..
            Cursor = Cursors.Default
            '=mbIsLoadingCustomer = False
            MsgBox("ERROR checking Reversed Invoices.. !", MsgBoxStyle.Exclamation)
            '= Call close_me()
            '= Exit Function
            '=mColAllAccountReversals = New Collection
        Else '==ok-
            '--test-
            s1 = ""
            For Each colRefund In mColAllAccountReversals
                listInvoicesReversals.Add(colRefund.Item("original_id"))
                s1 &= "Inv.No: " & colRefund.Item("original_id") & _
                           " Amt: " & FormatNumber(colRefund.Item("total_inc"), 2) & vbCrLf
            Next
            '= MsgBox("Found " & mColAllAccountReversals.Count & " account invoices reversed.." & vbCrLf & s1, MsgBoxStyle.Information)
        End If
        'ToolTip1.SetToolTip(labReversedInvoices, "")
        'labReversedInvoices.Text = ""
        '== END  Target-New-Build-4262 -- 



        '--Get all invoices for this customer-
        '--  get all PaymentDisbursements (JOIN Payments) for Customer..
        '-- Apply disb. amounts to invoices..
        '--   Collect invoices not fully paid..
        '-- Load Grid with all Outstanding Invoices for this customer --

        '==   Target-New-Build-4284-EXTRA-EXTRA --  (15-Nov-2020)
        labGettingData.Text = "Wait-" & vbCrLf & "Retrieving DB Data.."
        '== END Target-New-Build-4284-EXTRA-EXTRA --  (15-Nov-2020)

        dgvInvoices.Rows.Clear()

        '-- 3519.0227-
        '--   THIS will get ALL invoices.. NOT just Account Sales..
        '-- AND now gets REFUNDS as well.

        '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
        '--   NOW using gbCollectCustomerInvoicesEx2..

        If Not gbCollectCustomerInvoicesEx2(mCnnSql, intCustomer_Id, False, DateTime.Today,
                                   colInvoices, mDecTotalInvoices, mDecTotalOutstanding) Then
            'If Not gbCollectCustomerInvoicesEx(mCnnSql, intCustomer_Id, False, DateTime.Today,
            '                                            colInvoices, mDecTotalInvoices, mDecTotalOutstanding) Then
            '==  END Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
            '= Me.Close()
            Call close_me()
                Exit Function
            End If
            If (colInvoices Is Nothing) OrElse (colInvoices.Count <= 0) Then
            '== MsgBox("No invoice found for this Customer ", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '-- Load Grid with all Outstanding Invoices for this customer --
        Dim intCount As Integer = 0
        '=Dim gridRow1 As DataGridViewRow

        '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

        '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
        '==    Customer Admin..  Speed up loading Invoices Grid using DGV.AddRange...
        '= Call mWaitFormOff()
        '==
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        labGettingData.Text = "Wait- " & vbCrLf & " Building Array for grid.."
        DoEvents()
        '==
        Dim gridRowRange() As DataGridViewRow = {}
        Dim asRowValues() As String  '= invoice_no, invoices_invoice_date, tranType, isOnAccount, inv_total, prev_paid, outstanding.--
        Dim sIsOnAccount As String   '--0/1-
        Dim sThisInvTotal, sThisOutstanding, sThisDate As String
        Dim buffer() As Object  '= = New Object()
        Dim listRows As List(Of DataGridViewRow) = New List(Of DataGridViewRow)()
        '==  https://stackoverflow.com/questions/148854/adding-rows-to-datagridview-with-existing-columns
        '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)


        For Each colInvoice1 In colInvoices
            '=decPaymentTotal = 0
            intInvoice_id = colInvoice1.Item("invoice_id")
            sTranType = colInvoice1.Item("transactionType")
            bIsRefund = (UCase(sTranType) = "REFUND")
            decInvoiceTotal = CDec(colInvoice1.Item("InvoiceTotal"))
            bIsOnAccount = colInvoice1.Item("isOnAccount")
            colPayList = colInvoice1.Item("invoicePayments")
            sPayList = colInvoice1.Item("invoicePaymentList")
            '--paymentTotalThisInvoice-
            decPaymentTotal = colInvoice1.Item("paymentTotalThisInvoice")
            '-decAmountOutstanding-
            decAmountOutstanding = colInvoice1.Item("amountOutstanding")

            '==   Target-New-Build-4262 -- 
            intReversedInvoiceRefund_id = -1
            If listInvoicesReversals.Contains(intInvoice_id) Then
                '-- this was reversed..
                colRefund = mColAllAccountReversals.Item(CStr(intInvoice_id))  '-get refund this inv.
                intReversedInvoiceRefund_id = colRefund.Item("invoice_id")  '-inv.no of refund.
            End If
            '==  END Target-New-Build-4262 -- 


            '--load grid with all invoices-
            If (Not chkOutstanding.Checked) Or
                            (chkOutstanding.Checked And (decAmountOutstanding <> 0)) Then  '-he must pay..-

                '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
                '-- MAKE a List of new ROWS FIRST..

                'gridRow1 = New DataGridViewRow  '--prepare datagrid report row..
                'dgvInvoices.Rows.Add(gridRow1)
                ''-- load invoice no into grid row.
                'dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_INV_DATE).Value =
                '                          Format(colInvoice1.Item("invoice_date"), "dd-MMM-yyyy")
                'dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_INV_NO).Value = intInvoice_id
                'dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_TRANTYPE).Value = sTranType  '=3519,0317=
                'dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_ON_ACCOUNT).Value = colInvoice1.Item("isOnAccount")
                'dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_INV_TOTAL).Value = FormatNumber(decInvoiceTotal, 2)
                'dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_PREV_PAID).Value = sPayList
                '- make paylist fixed pitch..-
                sThisDate = Format(colInvoice1.Item("invoice_date"), "dd-MMM-yyyy")
                sIsOnAccount = CStr(colInvoice1.Item("isOnAccount"))
                sThisInvTotal = FormatNumber(decInvoiceTotal, 2)
                sThisOutstanding = ""
                '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)


                '==   Target-New-Build-4262 -- 
                '-- NO !!
                '= Dim font1 As New Font("Lucida Console", 8)
                '= dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_PREV_PAID).Style.Font = font1
                '== END  Target-New-Build-4262 -- 


                If bIsOnAccount Then
                    If Not bIsRefund Then
                        '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
                        '= dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_OUTSTANDING).Value = FormatNumber(decAmountOutstanding, 2)
                        sThisOutstanding = FormatNumber(decAmountOutstanding, 2)
                        '==  END Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)

                        '== Target-New-Build-4262 -- 
                        '--  Override Outst. if was reversed..
                        If (intReversedInvoiceRefund_id > 0) Then
                            '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
                            '= dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_OUTSTANDING).Value = "* #" & CStr(intReversedInvoiceRefund_id)
                            sThisOutstanding = "* #" & CStr(intReversedInvoiceRefund_id)
                            '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
                        End If  '-reversed.
                        '==END Target-New-Build-4262 -- 

                    End If  '-refund-
                Else
                    '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
                    '=dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_OUTSTANDING).Value = ""
                    '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
                End If  '-om account-

                '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
                'dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_INV_DATE).Style.Alignment =
                '                                               DataGridViewContentAlignment.MiddleLeft
                ''-make white bg for paying col.
                ''== dgvInvoices.Rows(intCount).Cells(k_INVGRIDCOL_PAYING_NOW).Style.BackColor = Color.White
                'intHeight = dgvInvoices.Rows(intCount).Height
                ''-- make row deeper if more than one payment.
                'If (colPayList.Count > 1) Then
                '    dgvInvoices.Rows(intCount).Height = intHeight + ((intHeight \ 2) * colPayList.Count)
                'End If
                buffer = {intInvoice_id, sThisDate, sTranType, sIsOnAccount, sThisInvTotal, sPayList, sThisOutstanding}
                listRows.Add(New DataGridViewRow())
                listRows(listRows.Count - 1).CreateCells(dgvInvoices, buffer)
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)

                intCount += 1  '-count grid rows..-
            End If  '-outstanding-
        Next  '-colInvoice1-

        '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
        '== DUMP all rows into Grid..
        labGettingData.Text = "Wait-" & vbCrLf & "Loading Grid Data.."
        DoEvents()
        dgvInvoices.Rows.AddRange(listRows.ToArray())
        labGettingData.Text = "ok-" & vbCrLf & "Invoice Grid Loaded."
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)

        If (dgvInvoices.RowCount > 0) Then
            dgvInvoices.Rows(0).Selected = True
            btnShowInvoice.Enabled = True
        Else
            btnShowInvoice.Enabled = False
        End If

    End Function  '--mbRefreshInvoiceList-
    '= = = = = =  = == =
    '-===FF->

    '--  Sales --

    Private Function mbRefreshSalesList(ByVal intCustomer_Id As Integer) As Boolean
        Dim sSql As String
        Dim dataTable1 As DataTable
        Dim dgvRow1 As DataGridViewRow

        dgvSales.Rows.Clear()
        '--List all Invoice LINES for this customer_id..-
        sSql = "SELECT  IL.stock_id, stock.barcode, stock.description, IL.total_inc, IL.quantity,  "
        sSql &= "   IL.invoice_id, invoice.invoice_date   "
        sSql &= "  FROM dbo.InvoiceLine AS IL "
        sSql &= "   LEFT OUTER JOIN stock on (IL.stock_id=stock.stock_id) "
        sSql &= "   LEFT OUTER JOIN invoice on (IL.invoice_id=invoice.invoice_id) "
        sSql &= "   LEFT OUTER JOIN customer on (invoice.customer_id=customer.customer_id) "
        sSql &= " WHERE (invoice.customer_id= " & CStr(intCustomer_Id) & ") "
        sSql &= " ORDER BY invoice.invoice_date desc;"

        '- get Query result and load datagrid..-
        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            MsgBox("Error in SELECT for InvoiceLine table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        End If  '--get-

        '--get ok-
        If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            '-- DUMP the datatable into the Grid..
            For Each dataRow1 As DataRow In dataTable1.Rows
                dgvRow1 = New DataGridViewRow
                dgvSales.Rows.Add(dgvRow1)
                With dgvSales.Rows(dgvSales.Rows.Count - 1)   '= last row..  
                    .Cells("sale_invoice_no").Value = dataRow1.Item("invoice_id")
                    .Cells("invoice_date").Value = Format(dataRow1.Item("invoice_date"), "dd-MMM-yyyy")
                    .Cells("barcode").Value = dataRow1.Item("barcode")
                    .Cells("description").Value = dataRow1.Item("description")
                    .Cells("sale_qty").Value = dataRow1.Item("quantity")
                    .Cells("sale_value").Value = FormatCurrency(dataRow1.Item("total_inc"), 2)
                End With
            Next dataRow1
        End If  '--nothing-
    End Function  '--sales-
    '= = = = = = = = = = = = = =
    '-===FF->

    '==
    '==   -- 4219.1116.  06-Nov-2019-  Started 06-November-2019-
    '==      -- Customer Admin.. Add Tabs for a/c Payments, Quotes.  

    Private Function mbRefreshPaymentsList(ByVal intCustomer_Id As Integer) As Boolean
        Dim sSelectSql As String
        Dim dataTable1 As DataTable
        Dim dgvRow1 As DataGridViewRow

        dgvPayments.Rows.Clear()

        sSelectSql = "SELECT payment_id, payment_date, transactionType, terminal_id, "
        sSelectSql &= "  totalAmountReceived, creditNoteAmountDebited, "
        sSelectSql &= "  staff.barcode AS staff_barcode,  "
        sSelectSql &= "  staff.docket_name "
        sSelectSql &= " FROM dbo.Payments "
        sSelectSql &= "   JOIN staff on (staff.staff_id =payments.staff_id) "
        sSelectSql &= " WHERE (payments.customer_id= " & CStr(intCustomer_Id) & ") "
        sSelectSql &= "       AND (LOWER(transactionType)= 'account' )"
        sSelectSql &= " ORDER BY payment_date desc;"

        '- get Query result and load datagrid..-
        If Not gbGetDataTable(mCnnSql, dataTable1, sSelectSql) Then
            MsgBox("Error in SELECT for Payments table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        End If  '--get-

        '--get ok-
        If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            '-- DUMP the datatable into the Grid..
            For Each dataRow1 As DataRow In dataTable1.Rows
                dgvRow1 = New DataGridViewRow
                dgvPayments.Rows.Add(dgvRow1)
                With dgvPayments.Rows(dgvPayments.Rows.Count - 1)   '= last row..  
                    .Cells("payment_id").Value = dataRow1.Item("payment_id")
                    .Cells("payment_date").Value = Format(dataRow1.Item("payment_date"), "dd-MMM-yyyy")
                    .Cells("amount_received").Value = FormatNumber(dataRow1.Item("totalAmountReceived"), 2)
                    .Cells("credit_note_contribution").Value = FormatNumber(dataRow1.Item("creditNoteAmountDebited"), 2)
                    .Cells("tran_Type").Value = dataRow1.Item("transactionType")
                    .Cells("docket_name").Value = dataRow1.Item("docket_name")
                End With
            Next dataRow1
        End If  '--nothing-

    End Function  '-mbRefreshPaymentsList-
    '= = = = = = = = = = = = = =
    '-===FF->
    '==
    '==   -- 4219.1116.  06-Nov-2019-  Started 06-November-2019-
    '==      -- Customer Admin.. Add Tabs for a/c Payments, Quotes.  

    Private Function mbRefreshQuotesList(ByVal intCustomer_Id As Integer) As Boolean
        Dim sSelectSql As String
        Dim dataTable1 As DataTable
        Dim dgvRow1 As DataGridViewRow

        dgvQuotes.Rows.Clear()

        '-- Search Quotes-
        sSelectSql = "SELECT SA.salesorder_id, salesorder_date,  SA.total_inc, "
        sSelectSql &= "  staff.barcode AS staff_barcode, staff.docket_name "
        sSelectSql &= "  FROM dbo.SalesOrder AS SA "
        sSelectSql &= "   JOIN staff on (staff.staff_id =SA.staff_id) "
        sSelectSql &= " WHERE (customer_id= " & CStr(intCustomer_Id) & ") "
        sSelectSql &= " ORDER BY salesorder_date desc;"

        '- get Query result and load datagrid..-
        If Not gbGetDataTable(mCnnSql, dataTable1, sSelectSql) Then
            MsgBox("Error in SELECT for SalesOrder (Quotes) table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        End If  '--get-

        '--get ok-
        If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            '-- DUMP the datatable into the Grid..
            For Each dataRow1 As DataRow In dataTable1.Rows
                dgvRow1 = New DataGridViewRow
                dgvQuotes.Rows.Add(dgvRow1)
                With dgvQuotes.Rows(dgvQuotes.Rows.Count - 1)   '= last row..  
                    .Cells("salesorder_id").Value = dataRow1.Item("salesorder_id")
                    .Cells("salesorder_date").Value = Format(dataRow1.Item("salesorder_date"), "dd-MMM-yyyy")
                    .Cells("total_inc").Value = dataRow1.Item("total_inc")
                    .Cells("staff_docket_name").Value = dataRow1.Item("docket_name")

                End With
            Next dataRow1
        End If  '-nothing.-
    End Function  '-mbRefreshPaymentsList-
    '= = = = = = = = = = = = = =
    '-===FF->

    '- Refresh JOBS List..

    '==   -- 4219.1116.  06-Nov-2019-  Started 06-November-2019-
    '==      -- Customer Admin.. Add Tabs for a/c Payments, Quotes, JOBS.  

    Private Function mbRefreshJobsList(ByVal intCustomer_Id As Integer) As Boolean
        Dim sSql, sWhere As String
        Dim dtJobs As DataTable
        Dim dgvRow1 As DataGridViewRow

        dgvJobs.Rows.Clear()
        sWhere = " (RMCustomer_id = " & CStr(intCustomer_Id) & ") AND (Left(JobStatus, 2) <= '70') "

        '-- check if any jobs to deliver for this cust..
        sSql = "SELECT job_id, JobStatus, GoodsInCare, dateCompleted, NominatedTech, dateupdated "
        sSql &= " FROM [Jobs] WHERE " & sWhere & "; "
        If Not gbGetDataTable(mCnnSql, dtJobs, sSql) Then
            MsgBox("Error getting Jobs table.." & vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If (dtJobs Is Nothing) OrElse (dtJobs.Rows.Count <= 0) Then
            Exit Function
        End If  '--nothing

        '-- DUMP the datatable into the Grid..
        For Each dataRow1 As DataRow In dtJobs.Rows
            dgvRow1 = New DataGridViewRow
            dgvJobs.Rows.Add(dgvRow1)
            With dgvJobs.Rows(dgvJobs.Rows.Count - 1)   '= last row.. 
                .Cells("job_id").Value = dataRow1.Item("job_id")
                .Cells("jobstatus").Value = dataRow1.Item("jobstatus")
                .Cells("NominatedTech").Value = dataRow1.Item("NominatedTech")
                .Cells("dateupdated").Value = dataRow1.Item("dateupdated")
            End With
        Next dataRow1

    End Function  '- mbRefreshJobsList-
    '= = = = = = = = = = = = = =
    '-===FF->

    Private Function mbClearAllTextFields() As Boolean
        Dim txtData As TextBox

        txtBarcode.Text = ""
        txtBarcode.ReadOnly = False   '--to be scanned in-

        For Each control1 As Control In panelCustomerDetail.Controls
            If TypeOf control1 Is TextBox Then
                '==MsgBox("The control is a textbox.")
                '-- must be textbox-
                txtData = CType(control1, TextBox)
                txtData.Text = ""
            End If  '-type-
        Next control1

        txtCustomer_id.Text = ""
        txtInactive.Text = ""
        chkInactive.Checked = False
        txtIsAccountCust.Text = ""
        chkIsAccountCust.Checked = False

        txtCreditLimit.Text = "0"
        txtCreditDays.Text = "0"

        '== mByteImage1 = Nothing

    End Function '--mbClearAllTextFields-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-   SET data modified.-
    '--  Check if completion possible..-
    '== 3411.0408= Check for min required flds.

    Private Function mbSetDataModified(ByVal strControlName As String) As Boolean
        Dim sControlname, sColumnName As String
        Dim sFieldList As String = ""
        Dim sFieldList2 As String = ""
        Dim txtdata As TextBox

        If Not InStr(msModifiedControls, strControlName) > 0 Then
            msModifiedControls &= strControlName   '--flag as being modified.
        End If
        If Not mbIsNewItem Then
            btnCustomerCommit.Enabled = True
        Else  '-new item
            '-- Iterate through all textbox controls in edit panel, and match against required list.
            For Each control1 As Control In panelCustomerDetail.Controls
                sControlname = LCase(control1.Name)
                '-- check if in required list--
                sColumnName = LCase(control1.Tag)  '--retrieve DB column name-
                If (sColumnName <> "") Then
                    '-- check if this is in group-1.
                    If (InStr(LCase(msRequiredFields), sColumnName & ";") > 0) Then  '--required-
                        txtdata = CType(control1, TextBox)
                        If (Trim(txtdata.Text) <> "") Then  '= Have one in 1st set. = "" Then  '--still missing-
                            sFieldList &= sColumnName & "; "
                        End If  '--missing-
                    End If '- in gr-1-
                    '-- check if this is in group-2.
                    If (InStr(LCase(msRequiredFields2), sColumnName & ";") > 0) Then  '--required-
                        txtdata = CType(control1, TextBox)
                        If (Trim(txtdata.Text) <> "") Then  '= Have one in 1st set. = "" Then  '--still missing-
                            sFieldList2 &= sColumnName & "; "
                        End If  '--missing-
                    End If '- in gr-1-
                End If  '-name-
            Next control1
            If (sFieldList <> "") And (sFieldList2 <> "") Then
                labRequiredFields.Text = "ok.."
                btnCustomerCommit.Enabled = True
            Else
                btnCustomerCommit.Enabled = False
                labRequiredFields.Text = k_RequiredFieldsMsg '= "Still required: " & sFieldList
            End If
        End If  '--new-
        btnCustomerCancel.Enabled = True

    End Function  '-mbSetDataModified-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- show Selected Customer item details in edit frame.--

    '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
    '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
    '--  Stop this from being re-entered.
    Private mbShowCustomerInfo_IsLoading As Boolean = False
    '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)


    Private Function mbShowCustomerInfo(ByVal intCustomer_id As Integer) As Boolean
        '=Dim sBarcode, sSerialNo, sQty As String
        Dim sSql, s1, s2, sErrorMsg As String
        Dim dataTable1 As DataTable
        Dim row1 As DataRow
        'Dim yBinaryData() As Byte
        'Dim image1 As Image
        'Dim decCost, decCostInc As Decimal
        'Dim decSell, decSellInc As Decimal

        mbShowCustomerInfo = False

        '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
        If mbShowCustomerInfo_IsLoading Then
            Exit Function
        End If
        mbShowCustomerInfo_IsLoading = True   '--Lock--
        If (mIntCustomer_id = intCustomer_id) Then
            '-- alreday showing..-
            mbShowCustomerInfo_IsLoading = False  '--UN-Lock--
            Exit Function
        End If
        mIntCustomer_id = intCustomer_id
        '== END Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)

        btnEdit.Enabled = False
        btnTagEdit.Enabled = False

        txtBarcode.Text = ""
        txtCustomer_id.Text = ""
        txtCompanyName.Text = ""
        txtLastName.Text = ""
        txtFirstName.Text = ""
        labItemHeader.Text = ""
        msSelectedCustomerBarcode = ""

        If (intCustomer_id <= 0) Then
            Exit Function
        End If
        '--lookup customer-id-
        '--  get recordset as collection for SELECT..--
        sSql = "SELECT * FROM [customer] WHERE (customer_id=" & CStr(intCustomer_id) & ");"
        If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso _
                               (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            '--have a row..-
            '- load column data to test boxes..
            '--   and Set control.tag to column-name-
            mbItemIsLoading = True
            row1 = dataTable1.Rows(0)
            txtBarcode.Text = row1.Item("barcode")
            txtBarcode.ReadOnly = True  '--can't change in edit mode.-

            '=3403.731=
            msSelectedCustomerBarcode = Trim(txtBarcode.Text)
            '= btnOK.Enabled = True

            txtCustomer_id.Text = CStr(intCustomer_id)
            txtCompanyName.Text = Trim(row1.Item("CompanyName"))

            txtLastName.Text = row1.Item("LastName")
            txtFirstName.Text = row1.Item("FirstName")

            labItemHeader.Text = txtCompanyName.Text
            If (labItemHeader.Text <> "") Then
                labItemHeader.Text &= "; "
            End If
            labItemHeader.Text &= txtFirstName.Text & " " & txtLastName.Text

            '-- check boxes..-
            chkInactive.Checked = IIf(row1.Item("Inactive"), True, False)
            txtInactive.Text = IIf(row1.Item("Inactive"), "1", "0")  '--THIS for for sql UPDATE-

            chkIsAccountCust.Checked = IIf(row1.Item("isAccountCust"), True, False)
            chkIsAccountCust.Enabled = False
            txtIsAccountCust.Text = IIf(row1.Item("isAccountCust"), "1", "0")  '--THIS for for sql UPDATE-
            If (txtIsAccountCust.Text = "0") Then  '--not account..  allow upgrade.
                chkIsAccountCust.Enabled = True
            End If

            txtAddress.Text = row1.Item("address")
            txtSuburb.Text = row1.Item("suburb")
            txtState.Text = row1.Item("state")
            txtPostCode.Text = row1.Item("postcode")
            txtCountry.Text = row1.Item("country")


            txtPhone.Text = row1.Item("phone")
            txtFax.Text = row1.Item("fax")
            txtMobile.Text = row1.Item("mobile")
            txtEmail.Text = row1.Item("email")

            '=3403.918=  Show Grade-
            cboPricingGrade.SelectedIndex = -1
            txtPricingGrade.Text = row1.Item("pricingGrade")
            '- set combo to show grade..
            For ix As Integer = 0 To (cboPricingGrade.Items.Count - 1)
                If VB.Left(cboPricingGrade.Items(ix), 1) = VB.Left(txtPricingGrade.Text, 1) Then
                    cboPricingGrade.SelectedItem = cboPricingGrade.Items(ix)
                    Exit For
                End If
            Next
            '== end grade=

            txtCreditLimit.Text = FormatCurrency(row1.Item("CreditLimit"), 2)
            txtCreditDays.Text = row1.Item("CreditDays")

            '-- comments--
            txtComments.Text = row1.Item("comments")

            btnEdit.Enabled = True
            btnTagEdit.Enabled = True

            msModifiedControls = ""
            mIntCustomer_id = intCustomer_id  '--save current id..-

 
            '=4219.1214== FIX FOR 4219.1130=
            labGettingData.Text = "Wait-" & vbCrLf & "Getting Data.."
            DoEvents()

            '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
            '--THESE WERE BEING DONE TWICE..
            '--THESE WERE BEING DONE TWICE..
            '--THESE WERE BEING DONE TWICE..
            '-- SHOW Cust.Invoices--
            '-- SHOW Cust.Invoices--
            'Call mbRefreshInvoiceList(intCustomer_id)
            ''--sales--
            ''== labSalesBarcode.Text = txtBarcode.Text
            ''== labSalesDescription.Text = txtCompanyName.Text
            'Call mbRefreshSalesList(intCustomer_id)
            '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)


            '-- SHOW Cust.Invoices--
            Call mbRefreshInvoiceList(intCustomer_id)

            '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
            labGettingData.Text = "Wait-" & vbCrLf & "Getting More Data.."
            '== END  Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)

            '--sales--
            '== labSalesBarcode.Text = txtBarcode.Text
            '== labSalesDescription.Text = txtCompanyName.Text
            Call mbRefreshSalesList(intCustomer_id)
            '=4219.1116=
            Call mbRefreshPaymentsList(intCustomer_id)
            Call mbRefreshQuotesList(intCustomer_id)
            Call mbRefreshJobsList(intCustomer_id)

            '=4219.1214=== FIX FOR 4219.1130=
            labGettingData.Text = ""
            DoEvents()

            '=4221.0207=
            '==  Show Customer Tags..
            Dim clsTags1 As clsTags
            Dim colTags As Collection
            Dim sList As String = ""

            clsTags1 = New clsTags(mCnnSql)
            If clsTags1.GetCustomerTags(mIntCustomer_id, s1, s2, colTags) Then
                For Each sTag As String In colTags
                    If sList <> "" Then
                        sList &= vbCrLf
                    End If
                    sList &= sTag
                Next sTag
                ToolTip1.SetToolTip(labCustTags, sList)
                labCustTags.Text = sList
            End If '-get-

            mbShowCustomerInfo = True
        Else '--not found..-
            '== ev.Cancel = True
            sErrorMsg = gsGetLastSqlErrorMessage()
            MsgBox("No Customer record found for Customer_id: '" & intCustomer_id & "' !" & _
                                            vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
        End If  '-get--
        mbItemIsLoading = False

        '==   Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)
        mbShowCustomerInfo_IsLoading = False   '--UN-Lock--
        '== END Target-New-Build-4284-EXTRA-EXTRA --  (24-Nov-2020)

    End Function  '--ShowCustomerInfo-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '- set up to add new Customer.

    Private Sub mSubNewCustomerSetup()

        mbIsNewItem = True
        mbItemIsLoading = True

        btnNew.Enabled = False
        btnEdit.Enabled = False
        btnTagEdit.Enabled = False
        btnCustomerCancel.Enabled = True

        FrameBrowse.Enabled = False

        btnCustomerCommit.Enabled = False

        '--  clear all text fields..--
        Call mbClearAllTextFields()

        '- refresh all  column combos..-
        '== Call mbRefreshFkeyCombos()
        panelCustomerDetail.Enabled = True
        '==mbItemIsLoading = False

        '== 3101.1207= Special for Customer Table.=
        mbIsAccountCustomer = False
        '= mbIsCustomerTable = (LCase(msEditTableName) = "customer")
        '=If mbNewRow AndAlso mbIsCustomerTable Then
        chkIsAccountCust.Checked = False
        txtIsAccountCust.Text = "0"  '==3411.0124=
        '=End If '-new-
        chkIsAccountCust.Enabled = False
        '=3403.918=
        chkInactive.Enabled = False
        txtInactive.Text = "0"

        cboPricingGrade.SelectedIndex = 0
        txtPricingGrade.Text = "0"

        txtCreditLimit.Enabled = False
        txtCreditDays.Enabled = False

        labAddingNew.Visible = True
        mbItemIsLoading = False

    End Sub  '-mSubNewCustomerSetup-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- L o a d --
    '-- L o a d --

    Private Sub ucChildCustomer_Load(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles MyBase.Load

        grpBoxCustomer.Text = ""
        txtCustomerSearch.Text = ""
        FrameBrowse.Text = ""

        labItemHeader.Text = ""
        Me.Text = "JobMatixPOS- Customer Info and Admin."

        '== grpBoxGoods.Text = ""
        mIntFormDesignWidth = Me.Width  '--save starting dim.
        mIntFormDesignHeight = Me.Height  '--save starting dim.

        '-- get system Info table data.-
        '==3301.428= 
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        panelCustomerDetail.Enabled = False

        btnEdit.Enabled = False
        btnTagEdit.Enabled = False

        btnNew.Enabled = False
        btnShowInvoice.Enabled = False
        '= btnOK.Enabled = False

        labEditing.Visible = False
        labAddingNew.Visible = False

        '- hide textboxes that capture checkbox results in the bacgground..-
        txtInactive.Visible = False

        labCustTags.Text = ""
        '-- Tag all input controls with DB Table column names..-
        '-- Tag all input controls with DB Table column names..-
        txtBarcode.Tag = "barcode"
        txtCustomer_id.Tag = "customer_id"
        txtInactive.Tag = "Inactive"
        txtIsAccountCust.Tag = "IsAccountCust"

        txtTitle.Tag = "title"
        txtLastName.Tag = "LastName"
        txtFirstName.Tag = "FirstName"

        txtCompanyName.Tag = "CompanyName"
        txtABN.Tag = "abn"
        txtAddress.Tag = "address"
        txtSuburb.Tag = "suburb"
        txtState.Tag = "state"
        txtPostCode.Tag = "postcode"
        txtCountry.Tag = "country"

        txtPhone.Tag = "phone"
        txtFax.Tag = "fax"
        txtMobile.Tag = "mobile"
        txtEmail.Tag = "email"

        txtPricingGrade.Tag = "pricinggrade"

        txtCreditLimit.Tag = "creditlimit"
        txtCreditDays.Tag = "creditdays"

        txtComments.Tag = "comments"

        '- set required fld list for New Records-
        '= msRequiredFields = "barcode; firstname; lastname; address; suburb; state; phone;"
        msRequiredFields = "firstname; lastname; companyname;"
        msRequiredFields2 = "phone; mobile; email; "

        labRequiredFields.Text = k_RequiredFieldsMsg  '= "Required: at least one name (or company name) " & vbCrLf & _
        '=                           "   and one contact Number.."

        '-- serials tab..-
        '-- wrap text for tran-list.-

        '==  WRAP ==  dgvSerials.Columns(2).DefaultCellStyle.WrapMode = DataGridViewTriState.True
        '--  --
        mColPrefsCustomer = New Collection
        mColPrefsCustomer.Add("barcode")
        mColPrefsCustomer.Add("lastName")   '--fkey-
        mColPrefsCustomer.Add("firstName")   '-fkey-
        mColPrefsCustomer.Add("companyName")   '-fkey-
        mColPrefsCustomer.Add("inactive")
        mColPrefsCustomer.Add("isAccountCust")
        mColPrefsCustomer.Add("address")
        mColPrefsCustomer.Add("suburb")
        mColPrefsCustomer.Add("phone")
        mColPrefsCustomer.Add("mobile")
        mColPrefsCustomer.Add("email")
        '==     Target-New-Build-4262 -- 
        mColPrefsCustomer.Add("customer_id")

        cboState.Items.Clear()
        cboState.Items.Add("NSW")
        cboState.Items.Add("QLD")
        cboState.Items.Add("SA")
        cboState.Items.Add("TAS")
        cboState.Items.Add("VIC")
        cboState.Items.Add("WA")
        cboState.Items.Add("ACT")
        cboState.Items.Add("NT")
        cboState.SelectedIndex = -1 '--none..--
        '== txtState.Visible = False
        '== txtState.Left = cboState.Left

        '=3403.918=  Pricing Grades. 
        mColPricingGrades = New Collection
        If mSysInfo1.contains("POS_CUSTPRICINGGRADE_COSTPLUS_1") Then
            mColPricingGrades.Add("1. +" & mSysInfo1.item("POS_CUSTPRICINGGRADE_COSTPLUS_1") & "%", "1")
        Else
            mColPricingGrades.Add("1. rrp", "1")
        End If
        If mSysInfo1.contains("POS_CUSTPRICINGGRADE_COSTPLUS_2") Then
            mColPricingGrades.Add("2. +" & mSysInfo1.item("POS_CUSTPRICINGGRADE_COSTPLUS_2") & "%", "2")
        Else
            mColPricingGrades.Add("2. rrp", "2")
        End If
        If mSysInfo1.contains("POS_CUSTPRICINGGRADE_COSTPLUS_3") Then
            mColPricingGrades.Add("3. +" & mSysInfo1.item("POS_CUSTPRICINGGRADE_COSTPLUS_3") & "%", "3")
        Else
            mColPricingGrades.Add("3. rrp", "3")
        End If
        If mSysInfo1.contains("POS_CUSTPRICINGGRADE_COSTPLUS_4") Then
            mColPricingGrades.Add("4. +" & mSysInfo1.item("POS_CUSTPRICINGGRADE_COSTPLUS_4") & "%", "4")
        Else
            mColPricingGrades.Add("4. rrp", "4")
        End If
        cboPricingGrade.Items.Clear()
        cboPricingGrade.Items.Add("0. RRP (Default)")
        For Each item1 As String In mColPricingGrades
            cboPricingGrade.Items.Add(item1)
        Next
        If gbIsSqlAdmin() Then
            cboPricingGrade.Enabled = True
        Else
            cboPricingGrade.Enabled = False
        End If

        '--clear all-
        Call mbClearAllTextFields()

        '= labDLLversion.Text = msVersionPOS
        'If (mIntForm_top = -1) Or (mIntForm_left = -1) Then
        '    Call CenterForm(Me)
        'Else
        '    '-- caller provided-
        '    Me.Top = mIntForm_top
        '    Me.Left = mIntForm_left
        'End If

        '=4201.0727=
        '-- SerialNo Context menu..
        '- --  Popup menu for Right click on Serial Grid..-
        mContextMenuInvoiceInfo = New ContextMenu
        mnuCopyItemInvoiceNo.Name = "CopyItemInvoiceNo"
        mContextMenuInvoiceInfo.MenuItems.Add(mnuCopyItemInvoiceNo)

        '-- mContextMenuSalesInfo-
        mContextMenuSalesInfo = New ContextMenu
        mnuCopyItemBarcode.Name = "CopyItemBarcode"
        mContextMenuSalesInfo.MenuItems.Add(mnuCopyItemBarcode)

        '4219.1214-- 
        '= Popup Action menu for Right click on Jobs Grid..-
        mContextMenuJobsAction = New ContextMenu
        '-- add all menu items..

        mnuJobActionCheckIn.Name = "NodeActionCheckIn"
        mContextMenuJobsAction.MenuItems.Add(mnuJobActionCheckIn)
        mContextMenuJobsAction.MenuItems.Add(mnuJobActionSep1)

        mnuJobActionAmend.Name = "NodeActionAmend"
        mContextMenuJobsAction.MenuItems.Add(mnuJobActionAmend)


        '==     Target-New-Build-4262 -- 
        '==      --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
        '==      --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)
        mContextMenuJobsAction.MenuItems.Add(mnuJobActionSep2)
        mnuJobActionUpdate.Name = "UpdateJobServiceRecord"
        mContextMenuJobsAction.MenuItems.Add(mnuJobActionUpdate)
        mnuJobActionDeliver.Name = "DeliverJob"
        mContextMenuJobsAction.MenuItems.Add(mnuJobActionDeliver)
        '--
        mContextMenuJobsAction.MenuItems.Add(mnuJobActionSep3)
        mnuJobActionView.Name = "ViewJobServiceRecord"
        mContextMenuJobsAction.MenuItems.Add(mnuJobActionView)
        '-- done menus..
        '==  END  Target-New-Build-4262 -- 

        '-- done menus..

        '--
        '= 4201.0428=
        '--  Stuff from SHOWN..
        '--  Stuff from SHOWN..
        '--  Stuff from SHOWN..

        Dim restrictions(3) As String
        Dim s1, sList As String
        Dim sSqlType As String
        Dim intADOtype, intSize, intPos As Integer

        ''-- do sub at startup only..
        'If mbActivated Then Exit Sub
        'mbActivated = True

        '= labStaffName.Text = msStaffName
        '= labToday.Text = Format(Today, "ddd dd-MMM-yyyy")

        '-- get schema info for table columns..-
        restrictions(1) = "dbo"
        restrictions(2) = "customer"   '--get CUSTOMER table cols info..
        Try
            mDataTableColumns = mCnnSql.GetSchema("columns", restrictions)
        Catch ex As Exception
            MsgBox("Failed to get columns schema info for customer Table.." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            '= Me.Close()
            Call close_me()
        End Try

        '- save col info.
        sList = ""
        mColColumnDataTypes = New Collection
        If Not (mDataTableColumns Is Nothing) Then
            For Each row1 As DataRow In mDataTableColumns.Rows
                '== NB:  oleDb types come back as ADO type..
                intADOtype = CInt(row1.Item("data_type"))
                '-CHARACTER_MAXIMUM_LENGTH
                intSize = 0
                If Not IsDBNull(row1.Item("CHARACTER_MAXIMUM_LENGTH")) Then
                    intSize = CInt(row1.Item("CHARACTER_MAXIMUM_LENGTH"))
                End If
                sSqlType = gsGetSqlType(intADOtype, intSize)
                '-- drop size suffix for varchar.-
                intPos = InStr(sSqlType, "(")
                If (intPos > 0) Then
                    sSqlType = Trim(VB.Left(sSqlType, intPos - 1))
                End If
                sList &= "Col: " & row1.Item("column_name") & _
                             "-  DataType= " & sSqlType & vbCrLf
                mColColumnDataTypes.Add(UCase(sSqlType), LCase(row1.Item("column_name")))
            Next row1
        End If
        '--test-
        '== MsgBox("Found schema cols: " & vbCrLf & sList, MsgBoxStyle.Information)

        btnCustomerCancel.Enabled = False

        '=dgv= Call mbRefreshStockList()  '--TEMP=
        mbIsInitialising = False

        Call mbInitialiseBrowse()
        If mbAddNewCustomerOnly Then
            Call mSubNewCustomerSetup()
            txtTitle.Select()   '--focus-
            Exit Sub
        End If  '-- add new=

        '-- select customer if specified on input..-
        '- ????-
        '==
        '==   Updated.- 3519.0221  Started 21-Feb-2019= 
        '==     -- Fixes Customer Admin to stop 8664 from appearing in the Find Textbox on startup.... 
        If mbAddNewCustomerOnly AndAlso (mIntCustomer_id > 0) Then
            txtFind.Text = CStr(mIntCustomer_id)
            Call mBrowse1.Find(txtFind)
            '--  show cust details of selected row..
            With dgvCustomerList
                If (.SelectedRows.Count > 0) Then
                    '--check id-
                    s1 = .SelectedRows(0).Cells(0).Value
                    If IsNumeric(s1) AndAlso (CInt(s1) = mIntCustomer_id) Then
                        Call mbShowCustomerInfo(mIntCustomer_id)
                    End If
                End If
            End With
        End If
        btnNew.Enabled = True

        '=4219.1214=  FIX FOR 4219.1130=
        labGettingData.Text = ""
        DoEvents()

    End Sub  '-- load --
    '= = = = = = = = = = = = = =  

    '--Activated-

    'Private Sub frmCustomer_Activated(ByVal sender As System.Object, _
    '                              ByVal e As System.EventArgs) Handles MyBase.Activated
    '    '-- do sub at startup only..
    '    If mbActivated Then Exit Sub
    '    mbActivated = True
    'End Sub  '--activated-

    '--Shown-

    'Private Sub frmCustomer_Shown(ByVal sender As System.Object, _
    '                                  ByVal e As System.EventArgs) Handles MyBase.Shown
    '    'Dim restrictions(3) As String
    'Dim s1, sList As String
    'Dim sSqlType As String
    'Dim intADOtype, intSize, intPos As Integer

    ' ''-- do sub at startup only..
    ''If mbActivated Then Exit Sub
    ''mbActivated = True

    'labStaffName.Text = msStaffName
    'labToday.Text = Format(Today, "ddd dd-MMM-yyyy")

    ''-- get schema info for table columns..-
    'restrictions(1) = "dbo"
    'restrictions(2) = "customer"   '--get CUSTOMER table cols info..
    'Try
    '    mDataTableColumns = mCnnSql.GetSchema("columns", restrictions)
    'Catch ex As Exception
    '    MsgBox("Failed to get columns schema info for customer Table.." & vbCrLf & ex.Message, MsgBoxStyle.Information)
    '    Me.Close()
    'End Try

    ''- save col info.
    'sList = ""
    'mColColumnDataTypes = New Collection
    'If Not (mDataTableColumns Is Nothing) Then
    '    For Each row1 As DataRow In mDataTableColumns.Rows
    '        '== NB:  oleDb types come back as ADO type..
    '        intADOtype = CInt(row1.Item("data_type"))
    '        '-CHARACTER_MAXIMUM_LENGTH
    '        intSize = 0
    '        If Not IsDBNull(row1.Item("CHARACTER_MAXIMUM_LENGTH")) Then
    '            intSize = CInt(row1.Item("CHARACTER_MAXIMUM_LENGTH"))
    '        End If
    '        sSqlType = gsGetSqlType(intADOtype, intSize)
    '        '-- drop size suffix for varchar.-
    '        intPos = InStr(sSqlType, "(")
    '        If (intPos > 0) Then
    '            sSqlType = Trim(VB.Left(sSqlType, intPos - 1))
    '        End If
    '        sList &= "Col: " & row1.Item("column_name") & _
    '                     "-  DataType= " & sSqlType & vbCrLf
    '        mColColumnDataTypes.Add(UCase(sSqlType), LCase(row1.Item("column_name")))
    '    Next row1
    'End If
    ''--test-
    ''== MsgBox("Found schema cols: " & vbCrLf & sList, MsgBoxStyle.Information)

    'btnCustomerCancel.Enabled = False

    ''=dgv= Call mbRefreshStockList()  '--TEMP=

    'Call mbInitialiseBrowse()
    'If mbAddNewCustomerOnly Then
    '    Call mSubNewCustomerSetup()
    '    txtTitle.Select()   '--focus-
    '    Exit Sub
    'End If  '-- add new=

    ''-- select customer if specified on input..--
    ''==
    ''==   Updated.- 3519.0221  Started 21-Feb-2019= 
    ''==     -- Fixes Customer Admin to stop 8664 from appearing in the Find Textbox on startup.... 
    'If mbAddNewCustomerOnly AndAlso (mIntCustomer_id > 0) Then
    '    txtFind.Text = CStr(mIntCustomer_id)
    '    Call mBrowse1.Find(txtFind)
    '    '--  show cust details of selected row..
    '    With dgvCustomerList
    '        If (.SelectedRows.Count > 0) Then
    '            '--check id-
    '            s1 = .SelectedRows(0).Cells(0).Value
    '            If IsNumeric(s1) AndAlso (CInt(s1) = mIntCustomer_id) Then
    '                Call mbShowCustomerInfo(mIntCustomer_id)
    '            End If
    '        End If
    '    End With
    'End If
    'btnNew.Enabled = True

    '= End Sub  '-- SHOWN --
    '= = = = = = = = = = = = = =  
    '-===FF->

    '- Form re-sized..

    Private Sub ucChildCustomer_Resize(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        'If (Me.WindowState = System.Windows.Forms.FormWindowState.Minimized) Then
        '    Exit Sub
        'End If
        '--  can't make smaller than original..-
        'If (Me.Height < mIntFormDesignHeight) Then
        '    Me.Height = mIntFormDesignHeight
        'End If
        'If (Me.Width < mIntFormDesignWidth) Then
        '    Me.Width = mIntFormDesignWidth
        'End If
        '-- resize main box and top panel-
        '= panelBanner.Width = Me.Width - 11

    End Sub   '-resize-
    '= = = = = = = = = = = = = =  
    '-===FF->

    '- C u s t o m e r - stuff--
    '- C u s t o m e r - stuff--
    '- C u s t o m e r - stuff--

    '= 4219.1214= = FIX FOR 4219.1130=
    '= 4219.1214= = FIX FOR 4219.1130=
 
    '-- Customer grid- Selection changed.-
    '-- save time we got into row..
    '--   So we don't try and show Customer info until selecting has settled.
    '--  set timer start time when row is selected
    Private mDblGridTimerStart As Double = 0
    Private mIntCurrentCustomer_id As Integer = -1

    Private Sub timerGrid_Tick(sender As Object, ev As EventArgs) Handles timerGrid.Tick
        Dim dblTimerNow As Double
        Dim colKeys, colRowValues As Collection

        If mDblGridTimerStart <= 0 Then
            Exit Sub   '--not active.
        End If
        dblTimerNow = VB.Timer
        If (dblTimerNow > (mDblGridTimerStart + 0.3)) Then  '-
            mDblGridTimerStart = 0
            timerGrid.Stop()
            If (mIntCurrentCustomer_id > 0) Then
                Call mbShowCustomerInfo(mIntCurrentCustomer_id)
            End If
        End If
    End Sub '-timerGrid_Tick-
    '= = = = =  = = = = = = = = = = = ==  =
    '-===FF->


    '--BROWSING Customer.. --

    '--  D at a  G r i d  E v e n t s..--
    '--  D at a  G r i d  E v e n t s..--

    Private Sub dgvCustomerList_RowEnter(ByVal sender As Object, _
                                           ByVal ev As DataGridViewCellEventArgs) _
                                           Handles dgvCustomerList.RowEnter
        Dim ix, intRow, intCol, intCustomer_id As Integer
        Dim s1 As String
        Dim colKeys, colRowValues As Collection

        intRow = ev.RowIndex
        intCol = ev.ColumnIndex

        With dgvCustomerList.Rows(ev.RowIndex)
            '--==--  s1 = .Cells(0).Value
            '== fixed 3403.728-==
            Call mBrowse1.SelectRecord(intRow, colKeys, colRowValues)
            s1 = colRowValues("customer_id")("value")
            If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                intCustomer_id = CInt(s1)
                If (intCustomer_id >= 0) And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                    '= 4219.1214= = FIX FOR 4219.1130=
                    '= 4219.1214= = FIX FOR 4219.1130=
                    '= Call mbShowCustomerInfo(intCustomer_id)
                    mDblGridTimerStart = VB.Timer
                    mIntCurrentCustomer_id = intCustomer_id  '-set current id for display.
                    timerGrid.Enabled = True
                    timerGrid.Start()
                End If
            End If
        End With
    End Sub  '-row enter.-
    '= = = = = = = = = = = = = =

    '--  Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvCustomerList_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvCustomerList.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvCustomerList.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowse1.SortColumn(sName)
    End Sub
    '= = = = = = = = =  = = =
    '= = = = = = = = =  = = =
    '-===FF->

    '-- cell click.--
    '-- cell click.--

    Private Sub dgvCustomerList_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvCustomerList.CellMouseClick
        Dim lRow, lCol As Integer
        Dim intCustomer_id As Integer
        Dim colKeys, colRowValues As Collection

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (dgvCustomerList.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                intCustomer_id = CInt(colRowValues("customer_id")("value")) '= colKeys.Item(1) --
                If (intCustomer_id >= 0) And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                    Call mbShowCustomerInfo(intCustomer_id)
                End If
            End If  '-selected-
        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  
    '-- select row to edit--
    '-- select row to edit--

    Private Sub dgvCustomerList_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvCustomerList.CellMouseDoubleClick
        Dim lRow As Integer
        Dim intCustomer_id As Integer
        Dim colKeys, colRowValues As Collection

        '== lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow >= 0) Then '--ok--
            mLngSelectedRow = lRow
            '--  get customer id and start edit.--
            If (lRow >= 0) And (dgvCustomerList.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                intCustomer_id = CInt(colRowValues("customer_id")("value"))  '=colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                If (intCustomer_id >= 0) And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                    Call mbShowCustomerInfo(intCustomer_id)
                End If
            End If  '-selected-
        End If '--row--
    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '-- customer Browser.. txt FIND Activity.--
    '-- customer Browser.. txt FIND Activity.--
    '--BROWSING customers.. --

    '-- key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        Dim intCustomer_id As Integer
        Dim colKeys, colRowValues As Collection

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If dgvCustomerList.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = dgvCustomerList.SelectedRows(0).Cells(0).RowIndex
                If (lRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow
                    '= Call mbSelectStockRow(mLngSelectedRow)
                    Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                    intCustomer_id = CInt(colRowValues("customer_id")("value")) '= colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                    If (intCustomer_id >= 0) And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                        Call mbShowCustomerInfo(intCustomer_id)
                    End If
                End If '--row--
                iKeyAscii = 0 '--processed--
            End If '--count--
            eventArgs.KeyChar = Chr(iKeyAscii)
            If iKeyAscii = 0 Then
                eventArgs.Handled = True
            End If
        End If  '--Enter-
    End Sub '--click--
    '= = = = = = = = = = =

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtFind_Enter(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles txtFind.Enter
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFind_Leave(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtFind.Leave
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    '= 4219.1214= = FIX FOR 4219.1130=
    '= 4219.1214= = FIX FOR 4219.1130=

    '-- Customer barcode text Find txt changed.-
    '-- save time we got into text..
    '--   So we don't try and Find Customer row until selecting has settled.
    '--  set timer start time when barcode text is started..
    Private mDblFindTimerStart As Double = 0
    '= Private mIntCurrentCustomer_id As Integer = -1

    Private Sub timerFind_Tick(sender As Object, ev As EventArgs) Handles timerFind.Tick
        Dim dblTimerNow As Double
        Dim colKeys, colRowValues As Collection

        If mDblFindTimerStart <= 0 Then
            Exit Sub   '--not active.
        End If
        dblTimerNow = VB.Timer
        If (dblTimerNow > (mDblFindTimerStart + 0.3)) Then  '-
            mDblFindTimerStart = 0
            timerFind.Stop()
            '==
            '==   4233.0421.  PREPed in Devel FOR- Updates to 4221.0207  Started 24-March-2020= 
            '==  .  ucChildCustomer-  Fix for Barcode "1" pause not working first time in..
            '==          IN the event "timerFind_Tick", drop the test for valid customer_id around call to Browse.Find..
            '==

            'If (mIntCurrentCustomer_id > 0) Then
            '    '= Call mbShowCustomerInfo(mIntCurrentCustomer_id)
            Call mBrowse1.Find(txtFind)
            'End If
        End If
    End Sub '-timerFind_Tick-
    '= = = = =  = = = = = = = = = = = ==  =


    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged
        '= FIX FOR 4219.1130=
        '= 4219.1214= = FIX FOR 4219.1130=
        '-- small delay to allow "1" to be added to..
        '- Since "1" is the barcode of GENERAL Customer with heaps of invoices..
        '--  So if first char is "1", delay FINDING until we see if another char is coming.

        Dim sBarcode As String
        If InStr(LCase(LabFind.Text), "barcode") > 0 Then
            '-searching barcode column.
            sBarcode = LCase(Trim(txtFind.Text))
            If sBarcode = "1" Then
                '-delay-
                '= 4219.1214= = FIX FOR 4219.1130=
                '= 4219.1214= = FIX FOR 4219.1130=
                '=Call mbShowCustomerInfo(intCustomer_id)
                mDblFindTimerStart = VB.Timer
                '= mIntCurrentCustomer_id = intCustomer_id  '-set current id for display.
                timerFind.Enabled = True
                timerFind.Start()
                Exit Sub
            End If
        Else
        End If
        '-normal=
        Call mBrowse1.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '-- customer Browser..  Full text Search..--
    '-- customer Browser..  Full text Search..--
    '=3519.0221-  Catch Enter Key on CUST srch text-

    Private Sub txtCustomerSearch_keyPress(ByVal sender As System.Object, _
                                         ByVal EventArgs As KeyPressEventArgs) Handles txtCustomerSearch.KeyPress
        Dim keyAscii As Short = Asc(EventArgs.KeyChar)
        Dim e2 As New EventArgs
        If keyAscii = 13 Then '--enter-
            Call cmdCustomerSearch_Click(cmdCustomerSearch, e2)
            keyAscii = 0 '--processed..-
        End If  '13-
        EventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            EventArgs.Handled = True
        End If

    End Sub  '-keypress.
    '= = = = = = = = == 


    Private Sub cmdCustomerSearch_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles cmdCustomerSearch.Click
        Dim sWhere As String = ""
        Dim sSql As String '--search sql..-- 
        '= Dim s1, s2 As String
        Dim asColumns As Object

        '--  rebuild Search Columns and call makeTextSearch...-

        '-- arg can be multiple tokens..--
        '===asArgs = Split(Trim(txtSearch(index).Text))
        '--  now in the Interface..--
        '== asColumns = mRetailHost1.stockSearchColumns()

        '==
        '==     Target-New-Build-4262 -- 
        '==    B. --  Account Invoice Reversal- 
        '==         -- Customer Admin to show "REVERSED" instead of Outstanding invoice amt if Invoice was reversed.. 
        '==            AND 1. ADD mnu Items to Active Jobs Context menu to View/Update Job via new ucChildJobMaint UserControl.  
        '==                2. Add Phone Nos. to Cust. search args..
        '==

        asColumns = New Object() _
                      {"barcode", "lastname", "firstname", "companyname", "phone", "mobile", "email"}

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtCustomerSearch.Text), asColumns)
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere + " AND "
            sWhere = sWhere + sSql
        End If
        Call mbBrowseCustomerTable(sWhere)

    End Sub '-cmdCustomerSearch-
    '= = = = = = = = = = = = =  =

    Private Sub cmdClearCustomerSearch_Click(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) Handles cmdClearCustomerSearch.Click
        txtCustomerSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdCustomerSearch_Click(cmdCustomerSearch, New System.EventArgs())

    End Sub  '-ClearcustSearch-
    '= = = = = = = = = = = = = = = =

    '==  END of customer BROWSING..--
    '==  END of customer BROWSING..--
    '-===FF->

    '-- Context menus for SalesInvoice and Sales Item Barcode.

    '== SalesInvoice and Sales Item Barcode Grids.  
    '==     Context Menus..
    Private mIntContextMenuInvoiceGridRow As Integer = -1
    Private mIntContextMenuItemSalesGridRow As Integer = -1

    '-- COPY INVOICE No of selected invoice..-
    Public Sub mnuCopyItemInvoiceNo_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles mnuCopyItemInvoiceNo.Click
        Dim sInvoiceNo As String

        If (mIntContextMenuInvoiceGridRow >= 0) Then
            sInvoiceNo = Trim(dgvInvoices.Rows(mIntContextMenuInvoiceGridRow).Cells("invoice_no").Value)
            My.Computer.Clipboard.Clear() ' Clear Clipboard.
            If (sInvoiceNo <> "") Then
                My.Computer.Clipboard.SetText(sInvoiceNo) '-- serial is first clumn..-.
            Else
                MessageBox.Show(Me, "No Invoice No..", _
                                  "JobMatixPOS Cust. Invoices", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If  '--empty-
        End If
    End Sub '--invoice..-
    '= = = = = = = = = = = = = = == = 

    '-- COPY barcode of selected item..-

    Public Sub mnuCopyItemBarcodeNo_Click(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles mnuCopyItemBarcode.Click
        Dim sBarcode As String

        If (mIntContextMenuItemSalesGridRow >= 0) Then
            sBarcode = Trim(dgvSales.Rows(mIntContextMenuItemSalesGridRow).Cells("barcode").Value)
            My.Computer.Clipboard.Clear() ' Clear Clipboard.
            If (sBarcode <> "") Then
                My.Computer.Clipboard.SetText(sBarcode) '-- ..-.
            Else
                MessageBox.Show(Me, "No barcode..", _
                                  "JobMatixPOS Cust. Sales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If  '--empty-
        End If
    End Sub '--part barcode..-
    '= = = = = = = = = = = = = = == = 
    '-===FF->

    '--Serials Cell Mouse-down..  Context menu.. 
    '--   CopySerialNo..

    Private Sub dgvInvoices_CellMouseDown(ByVal sender As Object, _
                                              ByVal ev As DataGridViewCellMouseEventArgs) _
                                               Handles dgvInvoices.CellMouseDown
        Dim intRow As Integer = ev.RowIndex
        Dim intColumn As Integer = ev.ColumnIndex
        Dim sInvoiceNo As String = ""
        Dim p1 = New Point(ev.X + 200, ev.Y + ((intRow + 1) * dgvInvoices.RowTemplate.Height))

        If (intRow >= 0) And (intColumn >= 0) Then
            If ev.Button = Windows.Forms.MouseButtons.Right Then
                sInvoiceNo = Trim(dgvInvoices.Rows(intRow).Cells("invoice_no").Value)
                If (sInvoiceNo <> "") Then
                    '-- select the row..
                    With Me.dgvInvoices
                        .CurrentCell = .Rows(intRow).Cells(intColumn)
                        .Rows(intRow).Selected = True
                    End With
                    '-- wait for row to be selected.
                    DoEvents()
                    Thread.Sleep(100)
                    DoEvents()
                    '-- Avoid the 'disabled' gray text by locking updates
                    LockWindowUpdate(dgvInvoices.Handle.ToInt32)
                    '---- A disabled TextBox will not display a context menu
                    dgvInvoices.Enabled = False
                    '--- Give the previous line time to complete
                    System.Windows.Forms.Application.DoEvents()
                    '-- Display our own context menu
                    mIntContextMenuInvoiceGridRow = intRow
                    mContextMenuInvoiceInfo.Show(CType(sender, Control), p1)
                    ' Enable the control again
                    dgvInvoices.Enabled = True
                    '-- Unlock updates
                    LockWindowUpdate(0)
                End If  '-barcode-
            End If  '-right button.
        End If  '-intRow-
    End Sub  '-dgvSerials_CellMouseDown-
    '= = = = = = = = = = = = = = = ==  =
    '-===FF->

    '--Item Sales Cell Mouse-down..  Context menu.. 
    '--Item Sales Cell Mouse-down..  Context menu.. 
    '--Item Sales Cell Mouse-down..  Context menu.. 
    '--   Copy barcode.

    Private Sub dgvSales_CellMouseDown(ByVal sender As Object, _
                                              ByVal ev As DataGridViewCellMouseEventArgs) _
                                               Handles dgvSales.CellMouseDown
        Dim intRow As Integer = ev.RowIndex
        Dim intColumn As Integer = ev.ColumnIndex
        Dim sBarcode As String = ""
        Dim p1 = New Point(ev.X + 200, ev.Y + ((intRow + 1) * dgvSales.RowTemplate.Height))

        If (intRow >= 0) And (intColumn >= 0) Then
            If ev.Button = Windows.Forms.MouseButtons.Right Then
                sBarcode = Trim(dgvSales.Rows(intRow).Cells("barcode").Value)
                If (sBarcode <> "") Then
                    '-- select the row..
                    With Me.dgvSales
                        .CurrentCell = .Rows(intRow).Cells(intColumn)
                        .Rows(intRow).Selected = True
                    End With
                    '-- wait for row to be selected.
                    DoEvents()
                    Thread.Sleep(100)
                    DoEvents()
                    '-- Avoid the 'disabled' gray text by locking updates
                    LockWindowUpdate(dgvSales.Handle.ToInt32)
                    '---- A disabled TextBox will not display a context menu
                    dgvSales.Enabled = False
                    '--- Give the previous line time to complete
                    System.Windows.Forms.Application.DoEvents()
                    '-- Display our own context menu
                    mIntContextMenuItemSalesGridRow = intRow
                    mContextMenuSalesInfo.Show(CType(sender, Control), p1)
                    ' Enable the control again
                    dgvSales.Enabled = True
                    '-- Unlock updates
                    LockWindowUpdate(0)
                End If  '-barcode-
            End If  '-right button.
        End If  '-intRow-

    End Sub '-dgvGoods_CellMouseDown-
    '= = = = = = = = = = = = = = = = = =
    '==  END of customer Context menus..--
    '-===FF->

    Private Sub btnEdit_Click(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) Handles btnEdit.Click
        mbItemIsLoading = True
        btnNew.Enabled = False
        btnEdit.Enabled = False
        btnCustomerCancel.Enabled = True

        '- refresh all column combos..-
        txtBarcode.ReadOnly = True

        mbIsNewItem = False
        btnCustomerCommit.Enabled = False

        '=dgv= listViewStock.Enabled = False
        FrameBrowse.Enabled = False

        '= Call mbRefreshFkeyCombos()
        labRequiredFields.Text = ""
        panelCustomerDetail.Enabled = True
        mbItemIsLoading = False
        labEditing.Visible = True
        '= cboCat1.Select()
        txtLastName.Select()

    End Sub  '--edit-
    '= = = = = = = = = = = = 
    '-===FF->

    '-- new --

    Private Sub btnNew_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles btnNew.Click
        Dim sMsg As String

        'mbIsNewItem = True
        'mbItemIsLoading = True
        'btnNew.Enabled = False
        'btnEdit.Enabled = False
        'btnCustomerCancel.Enabled = True

        'FrameBrowse.Enabled = False

        'btnCustomerCommit.Enabled = False

        ''--  clear all text fields..--
        'Call mbClearAllTextFields()

        ''- refresh all  column combos..-
        ''== Call mbRefreshFkeyCombos()
        'panelCustomerDetail.Enabled = True
        'mbItemIsLoading = False

        ''== 3101.1207= Special for Customer Table.=
        'mbIsAccountCustomer = False
        ''= mbIsCustomerTable = (LCase(msEditTableName) = "customer")
        ''=If mbNewRow AndAlso mbIsCustomerTable Then
        'chkIsAccountCust.Checked = False
        'txtIsAccountCust.Text = "0"  '==3411.0124=
        ''sMsg = "IMPORTANT:" & vbCrLf & _
        ''         "Is this NEW Customer to be " & vbCrLf & _
        ''         "  an Account (debtors ledger) customer or Cash Customer ?" & vbCrLf & vbCrLf & _
        ''         " (Answer 'No' to create a normal (cash) Customer.)" & vbCrLf & _
        ''       vbCrLf & "  NB: Can NOT be changed later !!" & vbCrLf
        ''If MsgBox(sMsg, MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
        ''    mbIsAccountCustomer = True
        ''    chkIsAccountCust.Checked = True
        ''End If '-yes-
        ''=End If '-new-
        'chkIsAccountCust.Enabled = False
        ''=3403.918=
        'cboPricingGrade.SelectedIndex = 0
        'txtPricingGrade.Text = "0"

        Call mSubNewCustomerSetup()

        '== txtBarcode.Select()   '--focus-
        txtTitle.Select()   '--focus-

    End Sub  '--new-
    '= = = = = = = = = = = = = 
    '-===FF->

    '-- C u s t o m e r--
    '-- Check box changes--
    '-- Check box changes--

    Private Sub chkInactive_CheckedChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles chkInactive.CheckedChanged
        If mbItemIsLoading Or mbIsInitialising Then Exit Sub
        '-  update BG text box.-
        txtInactive.Text = IIf(chkInactive.Checked, "1", "0")
        Call mbSetDataModified("txtInactive;")
    End Sub  '--chkInactive--
    '= = = = = = = = = = = = =

    Private Sub chkIsAccountCust_CheckedChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) Handles chkIsAccountCust.CheckedChanged
        If mbItemIsLoading Or mbIsInitialising Then Exit Sub
        '-  update BG text box.-
        txtIsAccountCust.Text = IIf(chkIsAccountCust.Checked, "1", "0")
        Call mbSetDataModified("txtIsAccountCust;")

        '=3501.1024-
        If chkIsAccountCust.Checked Then  '-can have credit limit.
            txtCreditLimit.Enabled = True
            txtCreditDays.Enabled = True
        Else
            txtCreditLimit.Enabled = False
            txtCreditDays.Enabled = False
        End If  '-checked.
    End Sub  '--chkTrackSerials-
    '= = = =  = = = = = = = =  =
    '-===FF->

    '-- S t o c k--

    '-- EDITABLE Text box changes--
    '-- EDITABLE Text box changes--

    '-- barcode only for NEW..--
    '==3103.205== Barcode now autoGen.-

    '==3103.205== Private Sub txtBarcode_TextChanged(ByVal sender As System.Object, _
    '==3103.205==                                    ByVal e As System.EventArgs) Handles txtBarcode.TextChanged
    '==3103.205==     If mbItemIsLoading Then Exit Sub
    '==3103.205==     Call mbSetDataModified("txtBarcode;")
    '==3103.205== End Sub  '--barcode-
    '= = = = = = = = = = =  = =

    '- ENTER key on barcode..-

    '==3103.205== Private Sub txtBarcode_KeyPress(ByVal eventSender As System.Object, _
    '==3103.205==                                 ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
    '==3103.205==                                 Handles txtBarcode.KeyPress
    '==3103.205== Dim iKeyAscii As Short = Asc(EventArgs.KeyChar)
    '==3103.205== Dim sBarcode, sSql, sErrorMsg As String
    '==3103.205== Dim datatable1 As DataTable

    '==3103.205== If iKeyAscii = System.Windows.Forms.Keys.Return Then
    '==3103.205==        sBarcode = Trim(txtBarcode.Text)
    '==3103.205==         If mbIsNewItem AndAlso (sBarcode <> "") Then
    '==3103.205==             sSql = "SELECT * FROM customer WHERE (Barcode='" & sBarcode & "');"
    '==3103.205==             If gbGetDataTable(mCnnSql, datatable1, sSql) AndAlso _
    '==3103.205==                          (Not (datatable1 Is Nothing)) Then
    '==3103.205==                 If (datatable1.Rows.Count > 0) Then
    '==3103.205==                     MsgBox("There is already a Customer for barcode " & sBarcode & vbCrLf & _
    '==3103.205==                     "Customer_id is " & datatable1.Rows(0).Item("customer_id") & ".." & vbCrLf & _
    '==3103.205==                               "Duplicates are not allowed." & vbCrLf, MsgBoxStyle.Exclamation)
    '==3103.205== '== eventArgs.Cancel = True
    '==3103.205==                 Else
    '==3103.205== '--ok-
    '==3103.205== '==cboCat1.Select()
    '==3103.205==                     txtLastName.Select()
    '==3103.205==                     iKeyAscii = 0 '--processed--
    '==3103.205==                 End If '--has rows-
    '==3103.205==             Else
    '==3103.205==                 sErrorMsg = gsGetLastSqlErrorMessage()
    '==3103.205==                 MsgBox("Error in reading Stock datatable for barcode: " & sBarcode & _
    '==3103.205==                           vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
    '==3103.205==             End If '--get table-
    '==3103.205==         End If  '-new-
    '==3103.205==        eventArgs.KeyChar = Chr(iKeyAscii)
    '==3103.205==         If iKeyAscii = 0 Then
    '==3103.205==            eventArgs.Handled = True
    '==3103.205==         End If
    '==3103.205==     End If  '--Enter key-
    '==3103.205== End Sub  '- txtBarcode_KeyPress--
    '= = =  = = = =  = = = = = = =
    '-===FF->

    '- check for duplicates-

    '==3103.205== Private Sub txtBarcode_Validating(ByVal sender As System.Object, _
    '==3103.205==                                      ByVal ev As System.ComponentModel.CancelEventArgs) _
    '==3103.205==                                             Handles txtBarcode.Validating
    '==3103.205== Dim sBarcode, sSql, sErrorMsg As String
    '==3103.205== Dim datatable1 As DataTable
    '==3103.205==     sBarcode = Trim(txtBarcode.Text)
    '==3103.205==    If mbIsNewItem AndAlso (sBarcode <> "") Then
    '==3103.205==         sSql = "SELECT * FROM customer WHERE (barcode='" & sBarcode & "');"
    '==3103.205==         If gbGetDataTable(mCnnSql, datatable1, sSql) AndAlso _
    '==3103.205==                     (Not (datatable1 Is Nothing)) Then
    '==3103.205==             If (datatable1.Rows.Count > 0) Then
    '==3103.205==                MsgBox("There is already a customer for barcode " & sBarcode & vbCrLf & _
    '==3103.205==                 "Customer_id is " & datatable1.Rows(0).Item("customer_id") & ".." & vbCrLf & _
    '==3103.205==                           "Duplicates are not allowed." & vbCrLf, MsgBoxStyle.Exclamation)
    '==3103.205==                 ev.Cancel = True
    '==3103.205==             Else
    '==3103.205== '--ok-
    '==3103.205==             End If '--has rows-
    '==3103.205==         Else
    '==3103.205==             sErrorMsg = gsGetLastSqlErrorMessage()
    '==3103.205==             MsgBox("Error in reading customer datatable for barcode: " & sBarcode & _
    '==3103.205==                       vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
    '==3103.205==        End If '--get table-
    '==3103.205==     End If  '-new-
    '==3103.205== End Sub  '--barcode-
    '= = = = = = = = = = =  = =
    '-===FF->

    '-- catch all alpha text fields-

    Private Sub txtCustomerInfo_TextChanged(ByVal sender As System.Object, _
                                                ByVal ev As System.EventArgs) _
                                                Handles txtCompanyName.TextChanged, _
                                                        txtTitle.TextChanged, _
                                                        txtLastName.TextChanged, _
                                                        txtFirstName.TextChanged, _
                                                        txtABN.TextChanged, _
                                                        txtAddress.TextChanged, _
                                                        txtSuburb.TextChanged, _
                                                        txtPostCode.TextChanged, _
                                                        txtCountry.TextChanged, _
                                                        txtPhone.TextChanged, _
                                                        txtMobile.TextChanged, _
                                                        txtFax.TextChanged, txtEmail.TextChanged


        Dim txtData As TextBox = CType(sender, System.Windows.Forms.TextBox)
        If mbItemIsLoading Or mbIsInitialising Then Exit Sub

        Call mbSetDataModified(txtData.Name & ";")

    End Sub '-txt info-
    '= = = = = = = = = = = = ==

    '- Catch ENTER key on text fields-
    '-- EXCEPT   txtAddress.KeyPress (multi-line field).-

    Private Sub txtCustomerInfo_KeyPress(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                        Handles txtCompanyName.KeyPress, _
                                                          txtTitle.KeyPress, _
                                                        txtLastName.KeyPress, _
                                                        txtFirstName.KeyPress, _
                                                        txtABN.KeyPress, _
                                                        txtSuburb.KeyPress, _
                                                        txtPostCode.KeyPress, _
                                                        txtCountry.KeyPress, _
                                                        txtPhone.KeyPress, _
                                                        txtMobile.KeyPress, _
                                                        txtFax.KeyPress, txtEmail.KeyPress

        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        '= Dim txtData As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            iKeyAscii = 0  '-done-
            Me.SelectNextControl(ActiveControl, True, True, True, True)
            eventArgs.KeyChar = Chr(iKeyAscii)
            '=If iKeyAscii = 0 Then
            eventArgs.Handled = True
            '=End If
        End If  '--return-
    End Sub  '-txtCustomerInfo_KeyPress-
    '= = = = = = = = = = =  = = = = = = =
    '-===FF->


    'Private Sub txtCompany_TextChanged(ByVal sender As System.Object, _
    '                                           ByVal e As System.EventArgs) Handles txtCompanyName.TextChanged
    '    If mbItemIsLoading Then Exit Sub
    '    Call mbSetDataModified("txtcompanyName;")
    'End Sub '-txtCompany-
    '= = = = = = = = = = = = ==


    Private Sub cboState_SelectedIndexChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) _
                                                Handles cboState.SelectedIndexChanged
        If mbItemIsLoading Or mbIsInitialising Then Exit Sub
        If (cboState.SelectedIndex >= 0) Then
            txtState.Text = cboState.SelectedItem
            Call mbSetDataModified("txtState;")
            '=Me.SelectNextControl(ActiveControl, True, True, True, True)
            txtPostCode.Select()

        End If '-index-
    End Sub  '--cboState_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = = = == 

    '-cboPricingGrade_SelectedIndexChanged-

    Private Sub cboPricingGrade_SelectedIndexChanged(sender As Object, e As EventArgs) _
                                           Handles cboPricingGrade.SelectedIndexChanged
        If mbItemIsLoading Or mbIsInitialising Then Exit Sub
        If (cboPricingGrade.SelectedIndex >= 0) Then
            '=If Not gbIsSqlAdmin() Then
            '=    MsgBox("Please Note:" & vbCrLf & _
            '=            " Only Admin users can change Customer Grade..", MsgBoxStyle.Exclamation)
            '=    Exit Sub
            '=End If
            txtPricingGrade.Text = VB.Left(cboPricingGrade.SelectedItem, 1)
            Call mbSetDataModified("txtPricingGrade;")

        End If '-index-
    End Sub  '-cboPricingGrade_SelectedIndexChanged-
    '= = = = = = =  ==  = = = = = = = = = == = = = = 

    '-===FF->

    '-txtCostExTax-
    Private Sub txtCreditLimit_TextChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles txtCreditLimit.TextChanged
        If mbItemIsLoading Or mbIsInitialising Then Exit Sub
        '==If IsNumeric(txtCostExTax.Text) Then
        Call mbSetDataModified("txtCreditLimit;")
        '==End If
    End Sub  '-txtCostExTax-
    '= = = = = = = = = = = = =


    Private Sub txtCreditDays_TextChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles txtCreditDays.TextChanged
        If mbItemIsLoading Or mbIsInitialising Then Exit Sub
        Call mbSetDataModified("txtCreditDays;")

    End Sub  '--txtSellExTax-
    '= = = = = = = = = = = = = = 


    '-Catch ENTER on price flds..

    Private Sub txtNumericPriceFlds_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                    Handles txtCreditLimit.KeyPress, txtCreditDays.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim txtData As TextBox = CType(eventSender, System.Windows.Forms.TextBox)

        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If (Trim(txtData.Text) = "") Then
                If (LCase(txtData.Name) = "txtcreditlimit") Then
                    txtData.Text = "0.00"
                Else
                    txtData.Text = "0"
                End If
            End If
            iKeyAscii = 0  '-done-
            Me.SelectNextControl(ActiveControl, True, True, True, True)
        End If '- Enter key-
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If

    End Sub  '--txtNumericPriceFlds_KeyPress-
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- txtCostExTax    VALIDATE numeric--
    '-- txtSellExTax    VALIDATE numeric--

    Private Sub txtNumericPriceFlds_validating(ByVal sender As System.Object, _
                                              ByVal ev As System.ComponentModel.CancelEventArgs) _
                                                  Handles txtCreditLimit.Validating, txtCreditDays.Validating
        Dim txtData As TextBox = CType(sender, System.Windows.Forms.TextBox)
        Dim errorMsg As String = ""
        Dim decCost, decCostInc As Decimal
        Dim decSell, decSellInc As Decimal

        If (Trim(txtData.Text) <> "") Then
            '==If gbIsNumericType(sSqlType) Then
            If (Not IsNumeric(txtData.Text)) Then
                errorMsg = "Field is for NUMERIC data only."
            Else
                If (LCase(txtData.Name) = "txtcreditlimit") Then
                    txtData.Text = FormatCurrency(CDec(txtData.Text), 2)
                End If  '--cost- 
            End If  '-numeric-
            '==End If
        End If  '--empty-

        If errorMsg <> "" Then  '--error
            ev.Cancel = True
            MsgBox(errorMsg, MsgBoxStyle.Exclamation)
        End If

    End Sub '-validating-
    '= = = = = = = = = = = = == 


    Private Sub txtComments_TextChanged(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles txtComments.TextChanged
        If mbItemIsLoading Or mbIsInitialising Then Exit Sub
        Call mbSetDataModified("txtComments;")

    End Sub '--txtComments--
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-chkOutstanding-

    Private Sub chkOutstanding_CheckedChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles chkOutstanding.CheckedChanged
        If mbItemIsLoading Or mbIsInitialising Then Exit Sub
        '-- SHOW Cust.Invoices--
        Call mbRefreshInvoiceList(mIntCustomer_id)

    End Sub '-chkOutstanding-
    '= = = = = = = = = = = = = = 

    '-- Invoices..
    '-- Invoices..

    Private Sub btnShowInvoice_Click(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) Handles btnShowInvoice.Click
        Dim frmShowInvoice1 As frmShowInvoice
        '== Dim bIsQuote As Boolean = (LCase(sTranType) = "quote")
        Dim intInvoice_id As Integer

        If (dgvInvoices.SelectedRows.Count > 0) Then
            If IsNumeric(dgvInvoices.SelectedRows(0).Cells(k_INVGRIDCOL_INV_NO).Value) Then
                intInvoice_id = CInt(dgvInvoices.SelectedRows(0).Cells(k_INVGRIDCOL_INV_NO).Value)

                frmShowInvoice1 = New frmShowInvoice
                frmShowInvoice1.connectionSql = mCnnSql
                frmShowInvoice1.InvoiceNo = intInvoice_id
                frmShowInvoice1.isQuote = False  '=bIsQuote
                frmShowInvoice1.Staff_id = mIntStaff_id

                frmShowInvoice1.ShowDialog()
            End If '-numeric-
        End If '--selected-

    End Sub  '-btnShowInvoice_Click-
    '= = = = = = = = = = = = = = = 

    '-- Invoices Grid..
    '-- Enable Show Invoice if valid row selected..

    Private Sub dgvInvoices_RowEnter(ByVal sender As Object, _
                                            ByVal ev As DataGridViewCellEventArgs) _
                                            Handles dgvInvoices.RowEnter
        Dim ix, lRow, lCol, intCustomer_id As Integer
        Dim s1 As String

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        btnShowInvoice.Enabled = True

    End Sub  '-row enter.-
    '= = = = = = = = = = = = = = = = = =

    '==   Target-New-Build-4262 -- 
    '==   Target-New-Build-4262 -- 
    '--  Try and force Invoice date to align at top..

    Private Sub dgvInvoices_CellFormatting(ByVal sender As Object, ByVal ev As DataGridViewCellFormattingEventArgs) _
                                           Handles dgvInvoices.CellFormatting
        ' -// If the column is the date column, fix the alignment.

        If (dgvInvoices.Columns(ev.ColumnIndex).Name = "invoices_invoice_date") Then
            ev.CellStyle.Alignment = DataGridViewContentAlignment.TopLeft
        End If  '-date-

    End Sub '-dgvInvoices_CellFormatting-
    '= = = = ==  ==  = == = = = = = = =  =
    '== END  Target-New-Build-4262 -- 
    '-===FF->

    '- Show Payment..

    Private Function mbShowPayment(ByVal intPayment_id As Integer) As Boolean

        Dim frmShowPayment1 As frmShowPayment

        frmShowPayment1 = New frmShowPayment
        frmShowPayment1.connectionSql = mCnnSql
        frmShowPayment1.sqlDbname = msSqlDbName
        frmShowPayment1.PaymentNo = intPayment_id
        '== frmShowPayment1.Settings = mSdSettings
        '== frmShowInvoice1.SystemInfo = mSdSystemInfo
        '= frmShowPayment1.UserLogo = mImageUserLogo
        frmShowPayment1.versionPOS = msVersionPOS
        '= frmShowPayment1.selectedReceiptPrinterName = sSelectedReceiptPrinterName
        frmShowPayment1.CaptureReceiptPDF = False '= bCaptureReceiptPDF
        frmShowPayment1.CanPrintReceipt = True '= bCanPrintReceipt
        '-- current staff_id and msStaffName-
        frmShowPayment1.Staff_id = mIntStaff_id
        frmShowPayment1.StaffName = msStaffName
        frmShowPayment1.ShowDialog()

        frmShowPayment1.Dispose()

    End Function  '-mbShowPayment-
    '= = = = = = = = = = = = == =

    Private Sub btnShowPayment_Click(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs) Handles btnShowPayment.Click
        Dim intPayment_id As Integer

        If (dgvPayments.SelectedRows.Count > 0) Then
            If IsNumeric(dgvPayments.SelectedRows(0).Cells("payment_id").Value) Then
                intPayment_id = CInt(dgvPayments.SelectedRows(0).Cells("payment_id").Value)
                Call mbShowPayment(intPayment_id)
            End If  '-id-
        End If

    End Sub  '-btnShowPayment_Click-
    '= = = = = = = = = = = = = = = = = =

    '-- select row to select Invoice to show..--

    Private Sub dgvPayments_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvPayments.CellMouseDoubleClick
        Dim intCol As Integer = eventArgs.ColumnIndex
        Dim intRow As Integer = eventArgs.RowIndex
        Dim intPayment_id As Integer

        If (dgvPayments.SelectedRows.Count > 0) Then
            If IsNumeric(dgvPayments.Rows(intRow).Cells("payment_id").Value) Then
                intPayment_id = CInt(dgvPayments.Rows(intRow).Cells("payment_id").Value)
                Call mbShowPayment(intPayment_id)
            End If  '-id-
        End If
    End Sub  '-dgvPayments_CellMouseDblClickEvent=
    '= = = = = = = = = = = = = = == = = = = = = == 
    '-===FF->

    '-- show selected Quote..

    Private Sub dgvQuotes_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                                    Handles dgvQuotes.CellMouseDoubleClick
        Dim intCol As Integer = eventArgs.ColumnIndex
        Dim intRow As Integer = eventArgs.RowIndex
        Dim intSalesOrder_id As Integer
        Dim frmShowInvoice1 As frmShowInvoice

        If IsNumeric(dgvQuotes.Rows(intRow).Cells("SalesOrder_id").Value) Then
            intSalesOrder_id = CInt(dgvQuotes.Rows(intRow).Cells("SalesOrder_id").Value)

            frmShowInvoice1 = New frmShowInvoice
            frmShowInvoice1.connectionSql = mCnnSql
            frmShowInvoice1.InvoiceNo = intSalesOrder_id
            frmShowInvoice1.isQuote = True  '=bIsQuote
            frmShowInvoice1.Staff_id = mIntStaff_id

            frmShowInvoice1.ShowDialog()
        End If  '-id-

    End Sub '-dgvQuotes_CellMouseDblClickEvent-
    '= = = = = = = = = = = = = = == = = = = = = == 

    '-- Show Selected Job..

    Private Sub dgvJobs_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                        Handles dgvJobs.CellMouseDoubleClick
        Dim intCol As Integer = eventArgs.ColumnIndex
        Dim intRow As Integer = eventArgs.RowIndex
        Dim intJob_id As Integer

        '==     Target-New-Build-4262 -- 
        '==      --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
        '==      --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)

        '==  RAISE EVENT to load Child JobMaint UserControl.

        If IsNumeric(dgvJobs.Rows(intRow).Cells("job_id").Value) Then
            intJob_id = CInt(dgvJobs.Rows(intRow).Cells("job_id").Value)
            '= Call mbShowJobMaintForm(intJob_id)
            RaiseEvent PosChildJobMaintRequest(Me.Name, False, False, _
                                         mIntCustomer_id, txtBarcode.Text, intJob_id)
        End If

    End Sub  '-dgvJobs_CellMouseDblClickEvent-
    '= = = = = = = = = = = = = = = = = == = =
    '-===FF->

    '-- Tell MainForm to Load New-Job UserControl Child..


    Private Function mbPostNewJobRequest(ByVal bIsAmending As Boolean, _
                                          ByVal bIsCheckIn As Boolean, _
                                          ByVal bIsNewOnSiteJob As Boolean, _
                                          Optional ByVal intJob_id As Integer = -1) As Boolean

        'RaiseEvent PosChildNewJobRequest(Me.Name, bIsCheckIn, bIsAmending, bIsNewOnSiteJob, _
        '                                   mIntCustomer_id, intJob_id, msSelectedCustomerBarcode, _
        '                                      txtCompanyName.Text, txtFirstName.Text + " " + txtLastName.Text, _
        '                                        txtPhone.Text, txtMobile.Text)
        '==  Target is new Build 4234..
        '==  Target is new Build 4234..
        '==
        '==   3.  CustomerAdmin-  Creating new Job in POS-  
        '==          -- Set CustomerName as "lastName, firstName", as per JobTracking..  (Fixes previous Fix)
        '==
        RaiseEvent PosChildNewJobRequest(Me.Name, bIsCheckIn, bIsAmending, bIsNewOnSiteJob, _
                                    mIntCustomer_id, intJob_id, msSelectedCustomerBarcode, _
                                       txtCompanyName.Text, txtLastName.Text + ", " + txtFirstName.Text, _
                                         txtPhone.Text, txtMobile.Text)


    End Function '-mbPostNewJobRequest-
    '= = = = = = = = = = == = = = == =

    Private mIntJobNoContextMenuSelected As Integer = -1

    '-- Check-In..-
    Public Sub mnuJobActionCheckIn_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionCheckIn.Click
        '-Checkin a job-
        Call mbPostNewJobRequest(False, True, False, mIntJobNoContextMenuSelected)

    End Sub '--check-in.-
    '= = = = = = = = = = = =

    '-- Amend..-
    Public Sub mnuJobActionAmend_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionAmend.Click
        '-Amend a job-
        Call mbPostNewJobRequest(True, False, False, mIntJobNoContextMenuSelected)
    End Sub '--Amend.-
    '= = = = = = = = = = = =
    '-===FF->

    '==  Target-New-Build-4262 -- 
    '==      --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
    '==      --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)

    '-- RAISE Event PosChildJobMaintRequest to get Child JobMaint UserControl loaded..

    Private Sub mnuJobActionView_click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionView.Click
        '=MsgBox("View Job..", MsgBoxStyle.Information)
        RaiseEvent PosChildJobMaintRequest(Me.Name, False, False, mIntCustomer_id, txtBarcode.Text, mIntJobNoContextMenuSelected)

    End Sub '- mnuJobActionView_click=
    '= = = = = = ==  == =  == = = =  =

    Private Sub mnuJobActionUpdate_click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles mnuJobActionUpdate.Click
        '=MsgBox("Update Job..", MsgBoxStyle.Information)
        RaiseEvent PosChildJobMaintRequest(Me.Name, True, False, mIntCustomer_id, txtBarcode.Text, mIntJobNoContextMenuSelected)

    End Sub '- mnuJobActionView_click=
    '= = = = = = ==  == =  == = = =  =

    Private Sub mnuJobActionDeliver_click() Handles mnuJobActionDeliver.Click

        RaiseEvent PosChildJobMaintRequest(Me.Name, False, True, mIntCustomer_id, txtBarcode.Text, mIntJobNoContextMenuSelected)

    End Sub  '-deliver-
    '= = = = = = = =  = = 
    '== END Target-New-Build-4262 -- 
    '-===FF->


    '-- Jobs Grid Context menu stuff..

    '==    Updated- 4219.1214= 
    '==       >> Jobs DataGridView..   
    '==        --  Add Right-click context menu for Job Actions, as per Active Jobs Tree treeview.
    '==

    Private Sub dgvJobs_CellMouseDown(eventSender As Object,
                                                     EventArgs As DataGridViewCellMouseEventArgs) _
                                                        Handles dgvJobs.CellMouseDown
        Dim intRowIndex As Integer = EventArgs.RowIndex
        Dim gridRow1 As DataGridViewRow
        Dim intJobNo As Integer
        Dim s1, sJobNo, sJobStatus As String
        Dim sActionList As String = ""
        '= Dim p1 = New Point(EventArgs.X, EventArgs.Y)

        Dim dgv1 As DataGridView = CType(eventSender, DataGridView)
        Dim sGridName As String = dgv1.Name

        If EventArgs.Button = Windows.Forms.MouseButtons.Right Then
            If (intRowIndex >= 0) Then
                gridRow1 = dgv1.Rows(intRowIndex)
                sJobNo = CStr(gridRow1.Cells("job_id").Value)
                If IsNumeric(sJobNo) Then
                    intJobNo = CInt(sJobNo)
                Else
                    MessageBox.Show("No JobNo..", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                mIntJobNoContextMenuSelected = intJobNo

                sJobStatus = CStr(gridRow1.Cells("jobstatus").Value)
                '-- get actual cell pos..
                Dim intX, intY As Integer
                '= Dim rectThisCell As Rectangle = dataGridViewOnSite.GetCellDisplayRectangle(EventArgs.ColumnIndex, EventArgs.RowIndex, True)
                Dim rectThisCell As Rectangle = dgv1.GetCellDisplayRectangle(EventArgs.ColumnIndex, EventArgs.RowIndex, True)
                intX = rectThisCell.Location.X
                intY = rectThisCell.Location.Y + rectThisCell.Height

                Dim p1 As Point = New Point(intX, intY)

                mnuJobActionAmend.Enabled = False
                mnuJobActionCheckIn.Enabled = False
                If (VB.Left(sJobStatus, 2) <= "05") Then '--created..-
                    mnuJobActionCheckIn.Enabled = True
                End If  '-created=
                If (VB.Left(sJobStatus, 2) <= "49") Then  '--QC.--
                    mnuJobActionAmend.Enabled = True
                End If

                '==  Target-New-Build-4262 -- 
                '==      --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
                '==      --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)
                mnuJobActionUpdate.Enabled = False
                '=mnuJobActionView.Enabled = False
                mnuJobActionDeliver.Enabled = False
                If (VB.Left(sJobStatus, 2) >= "10") And (VB.Left(sJobStatus, 2) <= "49") Then  '--updateable.--
                    mnuJobActionUpdate.Enabled = True
                ElseIf (VB.Left(sJobStatus, 2) = "50") Then  '-- completed..  not delivered..--
                    mnuJobActionDeliver.Enabled = True
                    '= mnuJobActionView.Enabled = True
                End If
                mnuJobActionView.Enabled = True
                '== END Target-New-Build-4262 -- 


                '- Show menu..
                '-show context menu.
                mContextMenuJobsAction.Show(CType(eventSender, Control), p1)


            End If  '-index=

        End If  '-button.-

    End Sub '-dgvJobs_CellMouseDown
    '= = = = = = = = = = = = = = = = = == = =
    '-===FF->

    '-btnAcceptJob_Click- 

    Private Sub btnAcceptJob_Click(sender As Object, e As EventArgs) Handles btnAcceptJob.Click
        '-workshop job-
        If (mIntCustomer_id <= 0) Then
            MsgBox("NO Customer selected.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Call mbPostNewJobRequest(False, False, False)

    End Sub  '-btnAcceptJob_Click-
    '= = = = = = = =  == =  = = = 

    '-btnOnSiteJob_Click-

    Private Sub btnOnSiteJob_Click(sender As Object, e As EventArgs) Handles btnOnSiteJob.Click

        '-OnSite job-
        If (mIntCustomer_id <= 0) Then
            MsgBox("NO Customer selected.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Call mbPostNewJobRequest(False, False, True)

    End Sub  '-btnOnSiteJob_Click-
    '= = = = = = = = = = = ==  = =
    '-===FF->

    '-- T a g s-
    '-- T a g s-
    '-- T a g s-

    '==  view/Update Reference List for ALL Customer Tags..

    Private Sub btnTagRef_Click(sender As Object, e As EventArgs) Handles btnTagRef.Click
        Dim frmTagRef1 As frmCustTagReference

        btnTagRef.Enabled = False
        frmTagRef1 = New frmCustTagReference(mCnnSql)
        frmTagRef1.ShowDialog()
        frmTagRef1.Close()
        frmTagRef1.Dispose()
        btnTagRef.Enabled = True
    End Sub  '-btnTagRef_Click.
    '= = = = = = = = = = = ==  = =

    '==  view/Update Tag List for SELECTED Customer..

    Private Sub labCustTags_Click(sender As Object, e As EventArgs) Handles labCustTags.Click
        Dim frmShowTags1 As frmShowCustomerTags

        labCustTags.Enabled = False
        If (mIntCustomer_id <= 0) Then
            MsgBox("No Customer selected for Tag Update..", MsgBoxStyle.Exclamation)
            '= Me.Close()
            labCustTags.Enabled = True
            Exit Sub
        End If
        frmShowTags1 = New frmShowCustomerTags(mCnnSql)
        frmShowTags1.customer_id = mIntCustomer_id

        frmShowTags1.ShowDialog()

        If Not frmShowTags1.cancelled Then
            Dim colNewTags As Collection
            Dim sList As String = ""

            colNewTags = frmShowTags1.UpdatedTags
            If colNewTags IsNot Nothing Then
                For Each sTag As String In colNewTags
                    If sList <> "" Then
                        sList &= vbCrLf
                    End If
                    sList &= sTag
                Next sTag
                ToolTip1.SetToolTip(labCustTags, sList)
                labCustTags.Text = sList
                MsgBox("ok.. Updated tags are: " & vbCrLf & sList, MsgBoxStyle.Information)
            End If  '-nothing.
        End If
        frmShowTags1.Close()
        frmShowTags1.Dispose()
        labCustTags.Enabled = True
    End Sub  '-labCustTags_Click-
    '-= = = = = = ==  = = = = = =

    Private Sub btnTagEdit_Click(sender As Object, e As EventArgs) Handles btnTagEdit.Click

        Call labCustTags_Click(btnTagEdit, New EventArgs)
    End Sub '-- tag edit-
    '= = = = = = = = = = = =
    '-===FF->

    '-customer commit-
    '-customer commit-

    Private Sub btnCustomerCommit_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles btnCustomerCommit.Click
        Dim sControlName, sColumnName As String
        Dim txtData As TextBox
        Dim sFldList As String = ""
        Dim sValueList As String = ""
        Dim sUpdate As String = ""
        Dim sSqlDataType, sFldData, sSql As String
        Dim ix, intAffected, intID, intCustomer_id As Integer
        '== Dim imageParameters1() = New OleDbParameter() {}  '--instantiates zero-length 1-dim array.--
        '== Dim parameter1 As OleDbParameter
        Dim cmd1, cmdUpdate As OleDbCommand
        '== Dim sTempBarcode = "TempBarcode"

        If mbIsNewItem Then
            Dim sqlTransaction1 As OleDbTransaction
            Dim myProcId As Integer
            myProcId = Process.GetCurrentProcess.Id
            '-- make temp barcode..
            '= txtBarcode.Text = sTempBarcode '= "TempBarcode"  '== & VB.Right(CStr(myProcId), 3)
            '- this will be replaced by customer_id as permanent barcode.

            '-- Build sql INSERT field list for new item..-
            '-- Iterate through all textbox controls in edit panel, and match against list.
            For Each control1 As Control In panelCustomerDetail.Controls
                sControlName = LCase(control1.Name)
                '-- get new data for update-
                sColumnName = control1.Tag
                If (sColumnName <> "") AndAlso mColColumnDataTypes.Contains(sColumnName) Then
                    sSqlDataType = UCase(mColColumnDataTypes(sColumnName))
                    If (UCase(sSqlDataType) = "IMAGE") Or _
                                   (UCase(sSqlDataType) = "BINARY") Or (UCase(sSqlDataType) = "VARBINARY") Then
                        '--IMAGE column- can't use strings.--
                        '--  make SQL cmd parameter..-
                        '-- BUILD SQL cmd parameter for image byte[]...
                        '== If Not mByteImage1 Is Nothing Then
                        '== If (sFldList <> "") Then
                        '== sFldList = sFldList & ", "
                        '== sValueList = sValueList & ", "
                        '== End If
                        '== sFldList = sFldList & sColumnName
                        '== sValueList = sValueList & " ? "
                        '== parameter1 = New OleDbParameter("@" & sColumnName, SqlDbType.VarBinary)
                        '== parameter1.Value = mByteImage1  '= mColRowImages(sFldName)
                        '== Dim k As Integer = imageParameters1.Length + 1
                        '== ReDim Preserve imageParameters1(k - 1)
                        '== imageParameters1(k - 1) = parameter1
                        '= End If  '--nothing=
                    Else '-not image--
                        '-- must be textbox-
                        txtData = CType(control1, TextBox)
                        If gbIsDate(sSqlDataType) Then
                            sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                        ElseIf gbIsText(sSqlDataType) Then
                            sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                        Else  '--numeric-
                            sFldData = Replace(txtData.Text, ",", "")
                        End If '--type-
                        '-- Add barcode later..
                        If (txtData.Text <> "") AndAlso (LCase(sColumnName) <> "customer_id") AndAlso _
                                   (LCase(sColumnName) <> "barcode") Then  '--don't include if no text or IDENTITY col.-
                            '--don't include ident flds--
                            If sFldList <> "" Then
                                sFldList = sFldList & ", "
                                sValueList = sValueList & ", "
                            End If
                            sFldList = sFldList & sColumnName
                            sValueList = sValueList & sFldData
                        End If  '--empty-
                    End If '--image/not image-
                End If  '-contains.-
            Next '--control1--
            '-- testing--
            '-- ok.. now INSERT New Record-
            If (sFldList <> "") Then '--something--
                '3103.205= - MUST add Staff-opened id..
                sFldList &= ", openedstaff_id, openedstaffname "
                sValueList &= ", " & CStr(mIntStaff_id) & ", '" & msStaffName & "' "

                Dim intCount As Integer = 5 '--retry times..-
                '= Dim intAffected As Integer
                Dim bCompletedOk As Boolean = False
                Dim sSqlInsert, sSqlUpdate As String
                Dim sFields2, sValues2 As String
                Dim dataTable1 As DataTable
                '== sSqlInsert = "INSERT INTO [customer] (" + sFldList + ")  VALUES (" + sValueList + ");"
                '==     -- 3501.0920-  Improve AUTO-barcode for NEW Customer...
                '==     -- 3501.0920-  Improve AUTO-barcode for NEW Customer...
                '- try insert/update x times
                While (intCount > 0) And (Not bCompletedOk)
                    intCount -= 1
                    '== MsgBox("SQL Insert cmd is : " & vbCrLf & sSql, MsgBoxStyle.Information)
                    Try
                        '--  add next attempt at predicting ID, for barcode.. 
                        '--  Must be GLOBAL next IDENT..
                        sqlTransaction1 = mCnnSql.BeginTransaction
                        '-- dummy SELECT to Lock the Table with HINT..
                        sSql = "SELECT TOP (1) barcode FROM dbo.customer WITH (SERIALIZABLE) ;"
                        If Not gbGetDataTableEx(mCnnSql, dataTable1, sSql, sqlTransaction1) Then
                            '- was rolled back-
                            MsgBox("Sql Error in DUMMY SELECT Customer record: " & vbCrLf & _
                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                            Exit While
                        End If
                        '- ok get last IDENT.
                        '--  Must be GLOBAL next IDENT..
                        sSql = "SELECT CAST(IDENT_CURRENT ('dbo.customer') AS int) ;"
                        If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                            If intID >= 0 Then
                                txtBarcode.Text = Trim(CStr(intID + 1))
                            Else
                                MsgBox("Couldn't get valid NEXT barcode value.", MsgBoxStyle.Exclamation)
                                sqlTransaction1.Rollback()
                                Exit While
                            End If
                        Else
                            MsgBox("Failed to retrieve CURRENT Customer No..", MsgBoxStyle.Exclamation)
                            '-was rolled back-
                            Exit While
                        End If
                        sFields2 = sFldList & ", barcode "
                        sValues2 = sValueList & ", '" & txtBarcode.Text & "' "
                        sSqlInsert = "INSERT INTO [customer] (" & sFields2 + ")  VALUES (" + sValues2 + ");"
                        ''-- Now do the insert..
                        cmd1 = New OleDbCommand(sSqlInsert, mCnnSql, sqlTransaction1)
                        Try
                            cmd1.ExecuteNonQuery()
                            '-was good.. should commit now to bed in new ids.
                            Try
                                sqlTransaction1.Commit()
                            Catch ex As Exception
                                MsgBox("Sql Error in COMMIT Customer record.." & vbCrLf & _
                                         ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
                                Exit While '= Sub
                            End Try
                            '-  Retrieve customer_id:  (IDENTITY of Customer record written.)-
                            '= sSql = "SELECT CAST(SCOPE_IDENTITY ('dbo.customer') AS int);"
                            sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
                            If gbGetSqlScalarIntegerValue(mCnnSql, sSql, intID) Then
                                intCustomer_id = intID
                                '-- update invoice display later..-
                            Else
                                '-- rollback was done..
                                MsgBox("Failed to retrieve latest Customer No..", MsgBoxStyle.Exclamation)
                                Exit While '= Sub
                            End If
                            '--worked-
                            bCompletedOk = True
                            If mbAddNewCustomerOnly Then
                                msSelectedCustomerBarcode = txtBarcode.Text
                                mbIsCancelled = False
                                '- now back to caller with new barcode.
                                '= Me.Hide()
                                Call close_me()
                                Exit Sub
                            Else  '-stay with customer form.
                                '=3404.731=-- show just new cust in browse..  selected-
                                Call mbBrowseCustomerTable("(customer_id=" & CStr(intCustomer_id) & ")") '-sWhere-
                                MsgBox("OK..  We Added new Customer record..." & vbCrLf & _
                                          "Customer_id is: " & intCustomer_id & vbCrLf & _
                                          "Customer Barcode is: " & txtBarcode.Text & "..", MsgBoxStyle.Information)
                            End If
                        Catch ex As Exception
                            sqlTransaction1.Rollback()
                            MsgBox("Sql Error in INSERT Customer record: " & vbCrLf & _
                                          ex.Message & vbCrLf & vbCrLf & _
                                          "NB. AutoGen barcode must be UNIQUE and may have clashed.." & vbCrLf & _
                                            "We will Retry the Commit to overcome the problem." & vbCrLf & _
                                            "SQL Command was: " & vbCrLf & _
                                               sSqlInsert & vbCrLf, MsgBoxStyle.Exclamation)
                            Thread.Sleep(1000)  '-msecs
                        End Try
                    Catch ex As Exception
                        sqlTransaction1.Rollback()
                        '-- Assume failed because Is was already in use as a barcode.
                        MsgBox("Error INSERTing Customer record: " & vbCrLf & _
                                      ex.Message & vbCrLf & vbCrLf, MsgBoxStyle.Exclamation)
                        Exit While
                    End Try  '--cmd 1=
                End While  '-completed-
            Else
                MsgBox("Error- No fields to in INSERT Customer record: ", MsgBoxStyle.Exclamation)
                Exit Sub
            End If  '-sFldList-

        Else '-NOT new item. editing-
            '-  build sql UPDATE SET list for modified fields.--
            '- "msModifiedControls" has names of modified controls..-
            '-- Iterate through all textbox controls in edit panel, and match against list.
            For Each control1 As Control In panelCustomerDetail.Controls
                sControlName = LCase(control1.Name)
                If (InStr(LCase(msModifiedControls), sControlName & ";") > 0) Then  '-was modified-
                    '-- get new data for update-
                    sColumnName = control1.Tag
                    If (sColumnName <> "") AndAlso mColColumnDataTypes.Contains(sColumnName) Then
                        sSqlDataType = UCase(mColColumnDataTypes(sColumnName))
                        If (UCase(sSqlDataType) = "IMAGE") Or _
                                     (UCase(sSqlDataType) = "BINARY") Or (UCase(sSqlDataType) = "VARBINARY") Then
                            '--IMAGE column- can't use strings.--
                            '--  make SQL cmd parameter..-
                            If sUpdate <> "" Then
                                sUpdate = sUpdate & ", "
                            End If
                            '== sUpdate = sUpdate & sColumnName & "=@" & sColumnName  '==parameter ref..-
                            sUpdate = sUpdate & sColumnName & "= ?"  '=oleDB =parameter ref..-
                            '-- BUILD SQL cmd parameter for image byte[]...
                            '== If Not mByteImage1 Is Nothing Then
                            '== parameter1 = New OleDbParameter("@" & sColumnName, SqlDbType.VarBinary)
                            '== parameter1.Value = mByteImage1  '= mColRowImages(sFldName)
                            '= Dim k As Integer = imageParameters1.Length + 1
                            '== ReDim Preserve imageParameters1(k - 1)
                            '== imageParameters1(k - 1) = parameter1
                            '== End If  '--nothing=
                        Else  '--not IMAGE-
                            '-- must be textbox-
                            txtData = CType(control1, TextBox)
                            If gbIsDate(sSqlDataType) Then
                                sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                            ElseIf gbIsText(sSqlDataType) Then
                                sFldData = "'" + msFixSqlStr(txtData.Text) + "'"
                            Else  '--numeric-
                                sFldData = msFixSqlStr(Replace(txtData.Text, ",", "")) '-clear commas from num.fld.-
                            End If
                            If sUpdate <> "" Then
                                sUpdate = sUpdate & ", "
                            End If
                            sUpdate = sUpdate + sColumnName + "=" + sFldData
                        End If  '--datatype-
                    End If  '--contains-
                End If  '--was modified-
            Next control1

            '-- testing--
            '== MsgBox("SQL Update list is : " & vbCrLf & sUpdate, MsgBoxStyle.Information)
            '--update-
            intCustomer_id = CInt(txtCustomer_id.Text)  '=3403.731=
            If (Len(sUpdate) > 0) Then  '= Or (imageParameters1.Length > 0) Then '--some to do--
                sUpdate = "UPDATE [customer]  SET " & sUpdate & _
                             " WHERE (customer_id=" & txtCustomer_id.Text & ");"
                '--  EXECUTE--
                Try
                    cmd1 = New OleDbCommand(sUpdate, mCnnSql)
                    '== If (imageParameters1.Length > 0) Then
                    '==  For ix = 0 To (imageParameters1.Length - 1)
                    '==    cmd1.Parameters.Add(imageParameters1(ix))
                    '==  Next
                    '== End If
                    intAffected = cmd1.ExecuteNonQuery()
                    '-ok-
                    msModifiedControls = ""  '=mbModified = False


                    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
                    '==       After Editing Customer Details.. make sure same cust can be selected for re-editing if needed.
                    '= DROP THIS- Call mbBrowseCustomerTable("(customer_id=" & CStr(intCustomer_id) & ")") '-sWhere-
                    '==  END Target-New-Build-4267 -- (Started 07-Sep-2020)



                    MsgBox("Update completed.." & vbCrLf & _
                                 "(" & intAffected & " row(s) were affected..)", MsgBoxStyle.Information)
                Catch ex As Exception
                    MsgBox("Sql Error in UPDATE customer record: " & vbCrLf & "SQL: " & _
                                sUpdate & vbCrLf + ex.Message, MsgBoxStyle.Exclamation)
                End Try
            Else  '--no change--
                '==mbUpdateRecord = True
            End If  '--update--
        End If  '--new/edit--

        labEditing.Visible = False
        labAddingNew.Visible = False

        panelCustomerDetail.Enabled = False
        btnNew.Enabled = True
        btnEdit.Enabled = False
        btnTagEdit.Enabled = False
        btnCustomerCancel.Enabled = False

        '== listView.Enabled = True
        FrameBrowse.Enabled = True

        msModifiedControls = ""
        '=3403.731= Call mbBrowseCustomerTable()  '-refresh browse-


        '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
        '==       After Editing Customer Details.. make sure same cust can be selected for re-editing if needed.
        mIntCustomer_id = -1  '--allow re-selection.
        '== END  Target-New-Build-4267 -- (Started 07-Sep-2020)


    End Sub  '- commit-
    '= = = = = = = = = = = = 
    '-===FF->


    '- Customer cancel --
    '- Customer cancel --
    '- Customer cancel --

    Private Sub btnCustomerCancel_Click(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles btnCustomerCancel.Click

        If (msModifiedControls <> "") Then
            If (MsgBox("Discard changes to this Customer item ?", _
                 MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
                '==intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                Exit Sub
            End If
        End If
        '-- wants reset--
        panelCustomerDetail.Enabled = False
        btnNew.Enabled = True
        btnEdit.Enabled = False
        btnTagEdit.Enabled = False
        btnCustomerCancel.Enabled = False

        labEditing.Visible = False
        labAddingNew.Visible = False

        '--  CLEAR all fields !! -
        Call mbClearAllTextFields()

        FrameBrowse.Enabled = True

        msModifiedControls = ""
        Call mbBrowseCustomerTable()  '-refresh-

        '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
        '==       After Editing Customer Details.. make sure same cust can be selected for re-editing if needed.
        mIntCustomer_id = -1  '--allow re-selection.
        '== END  Target-New-Build-4267 -- (Started 07-Sep-2020)


    End Sub  '-cancel-
    '= = = = = = = = = = = 
    '-===FF->

    '-- ok-

    'Private Sub btnOK_Click(sender As Object, e As EventArgs)
    '    If (msModifiedControls <> "") Then
    '        If (MsgBox("Abandon changes ?", _
    '             MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
    '            '= intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '            Exit Sub
    '        Else  'yes- go-
    '        End If
    '    Else  '-go-
    '        '== mbIsCancelled = True
    '        '= Me.Hide()
    '    End If  '--modified-
    '    Call close_me()

    'End Sub  '--ok-
    '= = = = = = = =  == 

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If (msModifiedControls <> "") Then
            If (MsgBox("Abandon changes ?", _
                 MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
                '= intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                Exit Sub
            Else  '-yes. go.-
            End If
        Else  '-no changes- go-
            '= Me.Hide()
            '=Call close_me()
        End If  '--modified-
        '- inform parent.-
        '- Report to Parent..-
        mbIsCancelled = True
        Call close_me()

    End Sub  '-exit-
    '= = = = = = = = === 
    '= = = = = = = = = = = 
    '-===FF->

    Private Sub close_me()
        'Dim bCancel As Boolean = False '= = EventArgs.Cancel
        ''= Dim intMode As System.Windows.Forms.CloseReason = EventArgs.CloseReason

        'If (msModifiedControls <> "") Then
        '    If (MsgBox("Abandon changes ?", _
        '         MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
        '        bCancel = True   '--cant close yet--'--was mistake..  keep going..
        '        Exit Sub
        '    End If
        'End If
        'If bCancel Then Exit Sub '--keep alive.-

        '- inform parent.-
        '- Report to Parent..-

        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

    End Sub '--close me-
    '= = = = = = == = = = =
    '= = = = = = = = = = = 
    '-===FF->


    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    'Private Sub ucChildCustomer_FormClosing(ByVal eventSender As System.Object, _
    '                                         ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
    '                                            Handles Me.FormClosing
    '    Dim intCancel As Boolean = eventArgs.Cancel
    '    Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

    '    Select Case intMode
    '        Case System.Windows.Forms.CloseReason.WindowsShutDown, _
    '                  System.Windows.Forms.CloseReason.TaskManagerClosing, _
    '                           System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
    '            intCancel = 0 '--let it go--
    '        Case System.Windows.Forms.CloseReason.UserClosing
    '            If (msModifiedControls <> "") Then
    '                If (MsgBox("Abandon changes ?", _
    '                     MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
    '                    intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '                    Exit Sub
    '                End If
    '            End If
    '            intCancel = 0 '--let it go---
    '            '==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '        Case Else
    '            intCancel = 0 '--let it go--
    '    End Select '--mode--
    '    eventArgs.Cancel = intCancel
    'End Sub '--closing--
    '= = = = = = =  = = = = = = = == 


End Class  '-ucChildCustomer-
'= = = = = = =  = = = = =

'== end form ==