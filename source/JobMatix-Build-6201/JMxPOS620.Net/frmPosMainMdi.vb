
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Reflection
Imports System.IO
Imports System.Threading
Imports System.Drawing.Printing
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.ComponentModel
Imports System.Data.OleDb

'==--V3.5 = Main MDI Mother Form..
'== -- Started  v3.5.3501.0702=

'== grh 08-June-2021-  This is the MAIN "MDI" form for the Open Source JobMatix..
'== grh 08-June-2021-  This is the MAIN "MDI" form for the Open Source JobMatix..
'== grh 08-June-2021-  This is the MAIN "MDI" form for the Open Source JobMatix..

' Copyright 2021 grhaas@outlook.com

'Licensed under the Apache License, Version 2.0 (the "License");
'you may Not use this file except In compliance With the License.
'You may obtain a copy Of the License at

'    http://www.apache.org/licenses/LICENSE-2.0

'Unless required by applicable law Or agreed To In writing, software
'distributed under the License Is distributed On an "AS IS" BASIS,
'WITHOUT WARRANTIES Or CONDITIONS Of ANY KIND, either express Or implied.
'See the License For the specific language governing permissions And
'limitations under the License.

'= = = =  ==  = = = = == = = = = = = = = = = == = = = = = = = = = = = 

'==
'==   >> 3501.0715=  15-July-2018=
'==    -- Move F6 and CashDrawer stuff from Child Sale form to Mdi Mother....
'==    -- Use Delegate to signal child closed to Main Parent.....
'==
'==    3501.0717 17July2018-
'==        -- Option to change Till now only avail on Startup...
'==
'==    3501.0722 22July2018-
'==        -- MAIN FORM only simulates MDI Container using Tabs.....
'== 
'==    3501.0725=  25-July-2018=
'==  --  (a) Sales-  for Non-Stock items..  do not warn of low stock, and do not update stock balance.
'==  --  (b) Shortcuts- Ctrl + O	Stock - Opens the Stock window
'==  --  	Ctrl + P	Suppliers - Opens the Supplier window
'==  -- 	Ctrl + U	Customers - Opens the Customer window
'==  --		Ctrl + F	Staff - Opens the staff window
'==  --  (c) Sales Window..  Quote trans. should not have, or stop on, Payment Box.
'==    
'==   >> 3501.0731=  31-July-2018=
'==      --  Add "About" menu to POS main to show WhatsNew page..
'==
'== -- Updated 3501.0809  09August2018=  
'==     -- Use Combined File/Sql subs module...
'==       Incl.  Fixes to modAllFileAndSqlSubs to Get correct appname for LocalDataDir..
'= = = = = = = = ==  =
'==
'== -- Updated 3501.1024  24-Oct-2018=  
'==     -- Fixed Crash in Sales Invoice Report due to Null Payment info....
'==     -- FIXed-  POS Admin Panel-  (Setup)-
'==            btnCashDrawers is (mistakenly?) disabled on Main form Load, 
'==            NOW is enabled to be able to assign Cash Drawers to printers.
'==     -- Fix to POS Main.. Move "Show Last Trans. to Till-Dropdown Main top toolstrip"...
'==            Copy code from clsPOS34Sale to show invoice..
'==
'==    New Build No.- 3519.0110 10-Jan-2019= 
'==         -- Add code to Discover DB users for POS app. only-
'==            (Stole code from JobTracking)
'==
'==   Updated.- 3519.0227  Started 26-Feb-2019= 
'==     -- Fix code frmPosMainMdi code LOOP to Check max permited users logged in... 
'==            (Now max=3 users for when TRIAL EXPIRED.)
'==                AND some nagging popups here and there..
'== 
'==   Updated.- 3519.0317  Started 14-March-2019= 
'==    -- MAJOR-  "frmPosSaleChild"- Add TextBox to Payments panel-
'==            for User to decide on Amount of CreditNote to withdraw to pay fpr Sale.
'==                  ALSO- formBorderStyle  is now fixedToolWindow..
'==
'==   Updated.- 3519.0404  Started 30-March-2019= 
'==    -- TRIAL PERIOD Extended to ninety days for everyone...
'==         AND Unlicenced Users restricted to ONE user, EXCEPT for Precise (still THREE users.) 
'==
'==  
'==    NEW VERSION 4.2  FIRST Version 4.
'==    NEW VERSION 4.2  FIRST Version 4.
'==
'==    First New Build- 4201.0416 -
'==   Updated.- 4201.0416- 
'==     --for TDI- Tabbed Document Interface.. 
'==         Admin forms in Tabs inside Main Form...
'==    -- 4201.0528.  Make Layby's  into Child User Control. 
'==               Add TabControlMain, and a tab to show ALL stock items under Layby..
'==     --- SHIFT-Control-U -- --  Test MODAL frmCustomer as it's called from JobTracking..
'==    
'==   Updated.- 4201.0627- 
'==     -- Ribbon updates..
'==     -- Delegate for Child Close request is now a function
'==           Child answers can close ? yes/no ..
'==
'==
'== - -- RELEASED as 4201.0627 --
'==
'= = == = =  = = = === 
'==
'== NEW revision-
'==    -- 4201.0707.  Started 05-July-2019-
'==       -- Add Local Preference for Re-ordering of payment Details.
'==       -- Add Shift-F6 for a New Payment Control...
'==
'== NEW revision-
'==    -- 4201.0830.  Started 28-August-2019-
'==        -- New UserControl for Transaction Lookup (Sales Invoices and Payments)..
'==--      -- Launching JobTracking-  Allow multiple instances..
'==        -- Add "Statements" to Accounts Menu. Drop Statements button..
'==              New Dropdown button for Accounts and Labys/CreditNotes.
'==
'== NEW revision-
'==    -- 4201.0922.  Started 22-September-2019-
'==        --  New Dropdown buttons for StockAdmin, Purchases, Categories..
'==              With context menus.
'==        --  For Dropdown buttons menus.  Change menu Control to "ContextMenuStrip"..
'==
'== NEW revision to fix previous.-
'==
'==    -- 4201.1013.  13-Oct-2019..
'==        --  Make Separate Menu entry, class,  and function call for direct access to Debtors Report
'==    --  11-Oct-2019=  Building clsDebtors for Debtors Report.
'==              -- PLUS add Main Menu Item "Debtors Report" and code to launch.         
'==              -- PLUS add PREVIEW-only OPTION to Print Debtors Report.         
'==
'==   -- 4201.1027.  27-Oct-2019-  Started 27-Oct-2019-
'==      -- MAIN TAB Control... Increase tab text size, and add proper Close X Icon...
'==      -- Debtors Report... Add extra menu entries for Detailed Report +30/60 days Closed Invoices..
'==                  Also Add feature to choose Customer for Debtors History. 
'==                  Also Fix printing of report from preview..
'==
'==
'== NEW Build-
'==
'==   -- 4219.1106/1125.  06-Nov-2019-  Started 06-November-2019-
'==      -- Finish Main Ribbon, with DropDown buttons for Reports, Settings..
'==      -- Fixing re-sizing of Main Tab Control.
'==      -- POS Main- Drop "labStatus" from Backup call....
'==      -- Add Ctl-L Shortcut to Lookup Transaction....
'==      -- Moved modules "modBackupRestore35"and "modAllFileAndSqlSubs" to the new Dll JMxRetailHost.dll
'==            '-- Dropped SysInfo parm from backup call.
'==      -- Load Updated RAs exe-  "JobMatixRAs42.exe"
'==
'== NEW Revision-
'==   == 4219.1216.  16-Dec-2019-  Started 10-Dec-2019-
'==     -- In main frmPosMainMdi, catch event from ucChildCustomer to launch ucChildNewJob, and
'==            catch the close event from ucChildNewJob to check if Exchange Calendar is to be updated..
'==     -- In main frmPosMainMdi, Add "About" to File menu..
'==
'==  Target is new Build 4233..
'==  Target is new Build 4233..
'==  Target is new Build 4233..
'==
'==   -- frmPOS3Reports is NOW a CHILD UserControl-
'==               --  For Sales Invoice Report-  Load Child UserControl instead of Form...
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
'==
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
'==
'==  Target is new Build 4251..
'==  Target is new Build 4251..
'==
'==   06-June-2020..
'==
'==   1. MAIN THEME is implementation of ACCOUNT-INVOICE-REVERSAL (Account "refund")
'==       --  Involves creating a REFUND (onAccount) full transaction as a mirror of Original.
'==       --  Not allowed if payments have been made towards original Invoice.
'==       --  Not allowed if original Invoice involved DELIVERY of a Job or a Layby..
'==       --  Transaction is accessible only from frmShowInvoice (showing original Invoice)..
'==                 Needs NEW CLASS  "clsAccountReversal".. 
'==       --  Transaction needs SUPERVISOR PASSWORD...
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
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
'==
'== Fixes to Build 4259.0730   (Started 14-Aug-2020)
'==
'==   Target-New-Build-4262 -- ( 26-Aug-2020)
'==   Target-New-Build-4262 -- 
'==   Target-New-Build-4262 -- ( 26-Aug-2020)
'==
'==      --  JobTracking has MADE Form "frmJobMaint32" INTO USERCONTROL- ucChildJobMaint..
'==      --  MAKE NEW Form "frmJobMaintBase" to hold USERCONTROL.
'--            SO THAT we show it as a Child in a POS TAB..
'==     -- SO now From Customer Admin we can use the UserControl in a TAB page..
'==
'==     Target-New-Build-4262 -- 
'==      --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
'==      --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)
'==
'= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
'==
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
'==   Target-New-Build-4287 --  (30-Jan-2021)
'==   Target-New-Build-4287 --  (31-Jan-2021)
'==   --     Update Copyright.. 2017-2021
'==
'= = = = = = = = = = = = = == =  == = == = = = = = = = = = = = = = = = = = = = = = = == = =

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


'= Main Mdi Top Form.


Public Class frmPosMainMdi
    Inherits System.Windows.Forms.Form

    '-SetForegroundWindow-  To switch to POS process..
    Declare Function SetForegroundWindow Lib "user32.dll" (ByVal hwnd As Integer) As Integer

    '--- For suppressing default popup menu..-
    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = =

    Private mIntChildCount As Integer = 0

    Private mlFormDesignHeight As Integer '-- starting dimensions..-
    Private mlFormDesignWidth As Integer '-- starting dimensions..-

    Private mbActivated As Boolean = False
    Friend mbLoginRequested As Boolean = False  '--makes visible to events..-

    '- - - - -
    Private mbIsInitialising As Boolean = True
    Private mbActive As Boolean = False
    Private mbStartingUp As Boolean
    Private mbMainLoadDone As Boolean = False

    '= Private mIntFormDesignHeight As Integer
    '= Private mIntFormDesignWidth As Integer '--save starting dimensions..-
    '--3411.1220=
    Private mIntFormUserTop As Integer = -1
    Private mIntFormUserLeft As Integer = -1 '--save caller requested position..-

    '==3300.428= NOW using clsSystemInfo--
    '==3300.428= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '==3300.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private msServer As String = ""
    '== Public gbIsSqlServer As Boolean = False
    '= Public gbIsJetDB As Boolean = False
    '-- now split server/instance..--
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""
    Private msSqlVersion As String = ""
    Private mlngSqlMajorVersion As Integer = 0

    '== Private msDataSourceName As String = ""

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private mbIsSqlAdmin As Boolean = False

    Private msMainVersionPOS As String = ""
    Private msDllversion As String = ""

    Private msComputerName As String '--local machine--
    Private mbIsThinClient As Boolean = False
    Private msMachineName As String = ""

    Private msAppPath As String
    Private msLastSqlErrorMessage As String = ""

    '--inputs--

    Private msSqlDbName As String = ""
    Private mColSqlDBInfo As Collection '--  POS DB info--
    Private mIntJobMatixDBid As Integer = -1

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    Private mlJobId As Integer = -1

    'Private msStaffBarcodeSignedOn As String = ""
    'Private msAdminStaffName As String = ""
    'Private mIntAdminStaff_id As Integer = -1

    Private msDefaultPrinterName As String = ""

    Private msColourPrtName As String = ""
    Private msReceiptPrtName As String = ""
    Private msLabelPrtName As String = ""

    '-- Business Info-
    '--  Business Info-
    Private msBusinessABN As String
    '== Private msBusinessUser As String
    Private msJT2SecurityIdOriginal As String '--as stored in SytemInfo in Row "JT2SecurityId"..-
    Private msJT2SecurityId As String '-- AS computed from ABN DateCreated.  --
    Private msBusinessName As String
    Private msBusinessAddress1 As String
    Private msBusinessAddress2 As String
    Private msBusinessShortName As String
    Private msBusinessPhone As String
    Private msBusinessPostCode As String
    Private msBusinessState As String

    Private mImageUserLogo As Image

    '- ex clsSale
    '-- POS Licence--
    Private mClsJmxPOS31_Licence As clsJMxPOS31
    Private mbIsEvaluating As Boolean = False

    '=3411.0402= 02Apr20111111118=
    '-- Startup- Check we have Till assigned..
    Private mStrOurTillId As String = ""

    '- from clsPOS34Sale.
    Private msMainStaffBarcode As String = ""
    Private msMainStaffName As String = ""
    Private mIntMainStaff_id As Integer = -1

    Private mColPrefsCustomer As Collection
    Private mColPrefsStock As Collection
    Private mColPrefsSupplier As Collection   '==3401.0308=
    Private mColPrefsStaff As Collection  '=3401.0308 -
    Private mColPrefsCategory1, mColPrefsCategory2, mColPrefsBrands
    '= = =  = = = = = = = =  = = = = = = = = = = = = = = = = ==  = ==

    Private msJobmatixAppName As String = ""

    '--Current Child forms.
    Private mColCurrentChildForms As Collection

    '- About-
    Private strAboutJobMatix3HTML As String = ""
    Private msJobMatixVersion As String = ""

    '=3519-0110=
    Private mIntMaxUsersPermitted As Integer = 0
    Private msCurrentProcessName As String = ""
    Private mbIsPosLicenceOK As Boolean = False

    '== VERSION 4.2 ==
    '== VERSION 4.2 ==
    '== VERSION 4.2 ==

    '--  Popup menus for Top menu buttons...-
    '--  Popup menus for Top menu buttons...-
    '--  Popup menus for Top menu buttons...-
    '==
    '== NEW revision-
    '==    -- 4201.0922.  Started 22-September-2019-
    '==        --  New Dropdown buttons for StockAdmin, Purchases, Categories..
    '==              With context menus.
    '==

    '- S t o c k --
    'Private mContextMenuStockAction As ContextMenu
    'Private WithEvents mnuMainStockAdmin As New MenuItem("Stock Admin")
    'Private WithEvents mnuMainStockSerials As New MenuItem("Stock Serials")
    'Private WithEvents mnuMainStockStocktake As New MenuItem("Stock Stocktake")
    'Private WithEvents mnuMainStockLabels As New MenuItem("Stock Labels")


    '--toolstrip- renderer-

    Private clsTsRendererPOS1 As clsTsRendererPOS

    '- For showing new Job Child..
    '= 34219.1216=  Check if Calendar update needed..
    Private msXmlPath As String = ""  '= frmNewJob3A.ExchangeCalendarUpdateXmlFileName

    '-- Save Running Build No..
    Private mIntCurrentBuildNo As Integer = 0

    '==  Target is new Build 4251..
    Private mbStartupAborted As Boolean = False

    '= = = = = = = =  = = = = = = = = = == = = = = = = = = == 
    '-===FF->

    '--init--

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        '= MsgBox("TEST- POS Main Form Constructor..", MsgBoxStyle.Information)


    End Sub  '--new--
    '= = = =  = = = = = = = =
    '-===FF->

    '-- Computer Name now comes from caller..--
    '-- Now can be thin Client..

    WriteOnly Property computerName() As String
        Set(ByVal Value As String)
            msComputerName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    '-- Staff Id now comes from caller..-- =3501.0725= NO IT doesn't !!

    'WriteOnly Property StaffName() As String
    '    Set(ByVal Value As String)
    '        msMainStaffName = Value
    '        labAdminStaffName.Text = msMainStaffName
    '    End Set
    'End Property '--name--
    ''= = = = = = = =  = = =

    ''- and brcode..-
    'WriteOnly Property Staffbarcode() As String
    '    Set(ByVal Value As String)
    '        msStaffBarcodeSignedOn = Value
    '    End Set
    'End Property '--name--
    ''= = = = = = = =  = = =

    'WriteOnly Property StaffId() As Integer
    '    Set(ByVal Value As Integer)

    '        mIntMainStaff_id = Value
    '    End Set
    'End Property '--id.-
    '= = = = = = = = = = = =  =

    '-- Sql Connection Info from Sub Main..-
    '-- Sql Connection Info from Sub Main..-

    WriteOnly Property SqlServer() As String
        Set(ByVal Value As String)
            msServer = Value
        End Set
    End Property '--server-
    '= = = = = =  = = =  = =

    WriteOnly Property SqlServerComputer() As String
        Set(ByVal Value As String)
            msSqlServerComputer = Value
        End Set
    End Property '--server-
    '= = = = = =  = = =  = =

    WriteOnly Property connectionSql() As OleDbConnection '= SqlConnection
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
    '- - - - - - = = = = = = 

    WriteOnly Property mainVersionPOS() As String
        Set(ByVal Value As String)
            msMainVersionPOS = Value
        End Set
    End Property
    '- - - - - - = = = = = = 

    '--IF DELIVERY-  caller supplies job no--

    WriteOnly Property JobNo() As Integer
        Set(ByVal Value As Integer)

            mlJobId = Value
        End Set
    End Property '--job--
    '= = = = = = = = = = = = = = = = = 

    '-- set user logo for printing..--
    WriteOnly Property UserLogo() As Object
        Set(ByVal Value As Object)

            mImageUserLogo = Value
        End Set
    End Property '--logo..--
    '= = = = = = = = = = = = = = = = =

    '--3411.1220=
    '= Private mIntFormUserTop As Integer = -1
    '= Private mIntFormUserLeft As Integer = -1 '--save caller requested position..-

    WriteOnly Property formUserTop() As Integer
        Set(ByVal Value As Integer)
            mIntFormUserTop = Value
        End Set
    End Property '--mIntFormUserTop --
    '= = = = = = = = = = = = = = = = = 
    WriteOnly Property formUserLeft() As Integer
        Set(ByVal Value As Integer)
            mIntFormUserLeft = Value
        End Set
    End Property '--mIntFormUserLeft --
    '= = = = = = = = = = = = = = = = = 

    WriteOnly Property JobmatixAppName As String
        Set(value As String)
            msJobmatixAppName = value
        End Set
    End Property
    '= = = = = = = = = = = = = =
    '-===FF->

    '=3519.0404-IsPrecisePCs-

    Private Function mbIsPrecisePCs() As Boolean

        mbIsPrecisePCs = False

        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        If (msSqlDbName = "") Then
            MsgBox("ERROR- No DB name for Company Check..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If (msBusinessABN = "82563967866") And (InStr(LCase(msSqlDbName), "precise") > 0) Then
            mbIsPrecisePCs = True
        End If

    End Function  '-IsPrecisePCs=
    '= = = = = = = = = = = = = = = 

    '==-- Child uses EVENT Delegate to signal child closed to Main Parent..
    '-- Child has this event definition..
    '--   Public Event PosChildClosing(ByVal strChildName As String)

    '-- Dispose of Tab Page and Child Control..
    '-- Dispose of Tab Page and Child..

    Private Sub posChild_Closing(ByVal strChildName As String)
        Dim sFormType As String '= = tabPage1.Tag

        msXmlPath = ""  '- use as indicator.
        For Each tabPage1 As TabPage In Me.TabControlMain.TabPages
            If (Not (tabPage1 Is Nothing)) AndAlso _
                         (LCase(tabPage1.Name) = LCase(strChildName)) Then
                sFormType = tabPage1.Tag
                Dim ucThis As UserControl   '= frmPosSaleChild
                If mColCurrentChildForms.Contains(strChildName) Then
                    ucThis = mColCurrentChildForms(strChildName)
                    '--each form type must be treated separately.
                    Try
                        '- remove handler, and dispose..
                        Select Case LCase(sFormType)
                            Case "ucpossalechild"
                                Dim frmChild1 As ucPosSaleChild
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            Case "ucchildstockadmin"
                                Dim frmChild1 As ucChildStockAdmin
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            Case "ucchildseriallookup"
                                Dim frmChild1 As ucChildSerialLookup
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                                '    delCloseRequest = Nothing
                            Case "ucchildstocktake"
                                Dim frmChild1 As ucChildStocktake
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            Case "ucchildgoodsrecvd"
                                Dim frmChild1 As ucChildGoodsRecvd
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            Case "ucchildpayments"
                                Dim frmChild1 As ucChildPayments
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            Case "ucchildsubscription"
                                Dim frmChild1 As ucChildSubscription
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            Case "ucchildcustomer"
                                Dim frmChild1 As ucChildCustomer
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            Case "ucchildstatements"
                                Dim frmChild1 As ucChildStatements
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            Case "ucchildlaybys"
                                Dim frmChild1 As ucChildLaybys
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            Case "ucchildnewjob"
                                Dim frmChild1 As ucChildNewJob
                                frmChild1 = ucThis
                                '-- get Calendar update xml file if any..
                                msXmlPath = frmChild1.ExchangeCalendarUpdateXmlFileName
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                                '==   Target-New-Build-4262 -- 
                            Case "ucchildjobmaint"
                                Dim frmChild1 As ucChildJobMaint
                                frmChild1 = ucThis
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing

                            Case Else
                        End Select
                    Catch ex As Exception
                        MsgBox("Error removing event handler for: " & strChildName & vbCrLf & _
                                   ex.Message & vbCrLf & vbCrLf & " (Processing will continue..)", MsgBoxStyle.Exclamation)
                        '=Exit For
                    End Try
                End If  '-contains-
                '--ok-
                tabPage1.Dispose()
                DoEvents()
                If mColCurrentChildForms.Contains(strChildName) Then
                    mColCurrentChildForms.Remove(strChildName)
                End If  '-2nd contains.
                DoEvents()
                Exit For
            End If  '-this child.
        Next tabPage1
        '-4219.1216-
        '-- if  msXmlPath <> "" then launch JobTracking if nor already active.
        If (msXmlPath <> "") Then
            '-- just means calendar needs to be updated.
            '--  by JobTracking process.
            If mbLaunchJobTracking(True) Then  '- launch if not already running.
                MsgBox("Please Note- " & vbCrLf & _
                        "An instance of JobTracking process been launched" & vbCrLf & _
                        " to initiate updating the OnSite Exchange Calendar..", MsgBoxStyle.Information)
            End If
        End If  '-xml-
    End Sub '-posChildClosing()--
    '= = = = = = = = = = = = = = 
    '-===FF->

    '-- called via delegate from Child.

    Public Sub subChildReport(ByVal strChildName As String, _
                              ByVal strEvent As String, _
                               ByVal sText As String)

        '= If LCase(strEvent) = "formclosed" Then
        For Each tabPageChild1 As TabPage In Me.TabControlMain.TabPages
            If (Not (tabPageChild1 Is Nothing)) AndAlso _
                       (LCase(tabPageChild1.Name) = LCase(strChildName)) Then
                If LCase(strEvent) = "formclosed" Then
                    '-Child form has closed..
                    '-- find tab page and delete it..
                    '= MsgBox("Closing: " & strChildName, MsgBoxStyle.Information)  '-test-
                    tabPageChild1.Dispose()
                    DoEvents()
                    Exit For
                End If  '--closed-
            ElseIf LCase(strEvent) = "updatetabtext" Then
                If (sText <> "") Then
                    tabPageChild1.Text = sText
                End If
            End If  '-nothing-
        Next  '-page-
        '= End If  '-closed-

    End Sub  '--ChildReport-
    '= = = = = = =  =  = =

    '==--Child uses Delegate to signal child STAFF SIGNED ON to Main Parent.....

    Public Sub subChildSignedOn(ByVal intStaffid As Integer, _
                                ByVal strStaffBarcode As String, _
                                 ByVal strStaffName As String)
        '--save as main signon-
        mIntMainStaff_id = intStaffid
        msMainStaffBarcode = strStaffBarcode
        msMainStaffName = strStaffName

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
    '= = = = = = = = = = = = =

    Private Function msGetDllversion() As String
        Dim assemblyThis As Assembly
        Dim assName As AssemblyName
        Dim s1, sVersion As String

        'msAppPath = My.Application.Info.DirectoryPath
        'If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
        'gsAppPath = msAppPath
        ''==msAppPath = sApppath
        ''-  new log each month..-
        's1 = VB.Format(Now, "yyyy-MM-dd")
        'gsErrorLogPath = gsJobMatixLocalDataDir("Jobmatix34") & "\JMxPOS340-Runtime-" & VB.Left(s1, 7) & ".log"
        'gsRuntimeLogPath = gsErrorLogPath  '--gsAppPath & "JTv3_Runtime.log"

        assemblyThis = System.Reflection.Assembly.GetExecutingAssembly()
        assName = assemblyThis.GetName
        With assName.Version
            sVersion = CStr(.Major) & "." & CStr(.Minor) & "." & CStr(.Build) & "." & Format(.Revision, "0000")
            '-save Build..
            mIntCurrentBuildNo = CInt(.Build)
        End With

        msGetDllversion = sVersion
    End Function  '--get version-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Update one setting..--
    '----change the setting in the static var dictionary..--
    '----- Write the dictionary back to disk..--

    Private Function mbSaveSetting(ByVal sKey As String, ByVal sValue As String) As Boolean
    
        If Not mLocalSettings1.SaveSetting(sKey, sValue) Then
        End If
    End Function '--save setting.--
    '- - - -  - - - -  --

    '-- DO NOT SHOW dialogue..
    '-- Show msg and record DO NOT SHOW result..-

    Private Function mbDoNotShowMsgDialogue(ByVal sShowMsgKey As String, _
                                            ByVal sSubHdr As String, _
                                            ByVal sMsgText As String, _
                                            Optional ByVal bIsRichText As Boolean = False, _
                                            Optional ByVal bHideDoNotShowCheckbox As Boolean = False, _
                                            Optional ByVal bCanViewWhatsNew As Boolean = False, _
                                            Optional ByVal sMainTitle As String = "JobMatix Message") As Boolean
        Dim bShow As Boolean = False
        Dim dlgDoNotShow1 As dlgNoShow
        Dim s1 As String

        '= mlStaffTimeout = -1 '--SUSPEND timing out..--
        mbDoNotShowMsgDialogue = False   '--wasn't shown--
        bShow = True
        If Not bHideDoNotShowCheckbox Then
            If mLocalSettings1.queryLocalSetting(sShowMsgKey, s1) Then '= mSdSettings.Exists(sShowMsgKey) Then
                '= s1 = UCase(mLocalSettings1.Item(sShowMsgKey))  '==  "DONOTSHOW_NEWUSERINFO1"-
                If UCase(VB.Left(s1, 1)) = "Y" Then
                    bShow = False
                End If  '-y-
            End If  '-query-
        End If
        If bShow Then
            mbDoNotShowMsgDialogue = True    '--was shown--
            dlgDoNotShow1 = New dlgNoShow
            dlgDoNotShow1.BackColor = Color.WhiteSmoke
            dlgDoNotShow1.MainTitle = sMainTitle
            dlgDoNotShow1.SubHdr = sSubHdr
            dlgDoNotShow1.isRichText = bIsRichText
            dlgDoNotShow1.Message = sMsgText
            dlgDoNotShow1.HideDoNotShowControl = bHideDoNotShowCheckbox
            dlgDoNotShow1.CanViewWhatsNew = bCanViewWhatsNew
            dlgDoNotShow1.ShowDialog()
            If Not bHideDoNotShowCheckbox Then
                If dlgDoNotShow1.DoNotShow = True Then
                    '--don't show it again.-
                    Call mbSaveSetting(sShowMsgKey, "Y")
                End If
            End If
            dlgDoNotShow1.Close()
            dlgDoNotShow1.Dispose()
        End If  '--show..-
        '= mlStaffTimeout = 0 '--now timing out..--
    End Function  '--mbDoNotShowMsgDialogue--
    '= = = = = = = = == = = == = = = = = 
    '-===FF->

    '-- Stuff for Checking on no.of users. (ex JobTracking..)

    '-3519.0108-  Special whoUsing for including program name..

    '--  Who Using this DB.. --
    '--  Who Using this DB.. --
    '-- call sp_who to get list of current users of DBname --
    '-- Rev-2912.. 21-Jul-2011==  ONLY worry about tasks that are "runnable"..-
    '-- Rev-3069.. 16-Oct-2011==  Worry about all tasks that are NOT "dormant"..-
    '--  ALSO-=3071=  
    '==     For SQL Server 2005 and later..  Use "sys.sysprocesses" system view..
    '==
    '--  NEW--=3103.0203=  
    '==     For SQL Server 2008 and later ONLY..  Use "sys.sysprocesses" system view..
    '==   NB: SQL Server 2005 with oledb has problem with "sys.sysprocesses" system view..
    '==          ("Protocol Error in TDS data stream" )
    '== = =

    '-3519.0108-  Special LOCAL Who-Using version -
    '-- for including program name in returned collection..
    '==  NOTE- THIS Means that SqlServer2005 will not be returnibg full info..
    '-  NB sys.sysprocesses still works in Sql Server 2017..

    Public Function mbWhoUsingEx(ByRef cnn1 As OleDbConnection, _
                              ByVal sDBName As String, _
                               ByRef colWhichUsers As Collection) As Boolean

        Dim i, j As Integer
        Dim k As Short
        Dim sSql, s1, sErrors As String
        Dim sLoginName, sHostName, sStatus As String
        Dim rs1 As OleDbDataReader  '= ADODB.Recordset
        Dim lResult, L1 As Integer
        Dim colUser As Collection

        '= msLastSqlErrorMessage = ""
        '--USE--
        '== s1 = " USE master " '-- + sDBName
        '==3069=  If Not gbExecuteCmd(cnn1, s1, L1, sErrors) Then MsgBox("Failed sql: " & s1)
        mbWhoUsingEx = False
        If gbIsSqlServer2008Plus() Then  '=If gbIsSqlServer2005Plus() Then
            If gbIntJobMatixDBid() <= 0 Then Exit Function '-- NO DB ID was saved !!-
            sSql = "SELECT loginame, dbid, hostname, nt_domain, status, program_name " & vbCrLf & _
                   "FROM sys.sysprocesses; "
            If Not gbGetReader(cnn1, rs1, sSql) Then  '= gbGetRst(cnn1, rs1, sSql) Then
                s1 = "Failed to retrieve list of users.." & vbCrLf & _
                      "Error msg: " & vbCrLf & gsGetLastSqlErrorMessage() & vbCrLf
                '= If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
                MsgBox(s1, MsgBoxStyle.Exclamation)
                '=msLastSqlErrorMessage = s1
            Else
                '--check recordset--
                If Not (rs1 Is Nothing) Then
                    colWhichUsers = New Collection
                    If rs1.HasRows Then  '== Not (rs1.BOF And rs1.EOF) Then  '--not empty-
                        '== If rs1.BOF Then rs1.MoveFirst()
                        While rs1.Read  '= Not rs1.EOF
                            sStatus = ""
                            If (Not IsDBNull(rs1.Item("status"))) Then
                                sStatus = CStr(rs1.Item("status"))
                            End If
                            If (Not IsDBNull(rs1.Item("loginame"))) And (Not IsDBNull(rs1.Item("dbid"))) Then
                                '== colUser = New Collection '--each user is collection..-
                                sLoginName = CStr(rs1.Item("loginame"))
                                If (CInt(rs1.Item("dbid")) = gbIntJobMatixDBid()) And _
                                                                        (sStatus <> "dormant") Then  '--our DB--
                                    colUser = New Collection '--each user is collection..-
                                    colUser.Add(sLoginName, "LOGINAME")
                                    If Not IsDBNull(rs1.Item("hostname")) Then
                                        colUser.Add(CStr(rs1.Item("hostname")), "HOSTNAME")
                                    Else
                                        colUser.Add("", "HOSTNAME")
                                    End If '--null-
                                    '=3519.0108= GET program name also..
                                    If Not IsDBNull(rs1.Item("program_name")) Then
                                        colUser.Add(CStr(rs1.Item("program_name")), "PROGRAM_NAME")
                                    Else
                                        colUser.Add("", "PROGRAM_NAME")
                                    End If '--null-
                                    colWhichUsers.Add(colUser)
                                End If
                            End If  '--null login.-
                            '== rs1.MoveNext()
                        End While  '-read-
                    End If  '--empty- 
                    rs1.Close()
                    mbWhoUsingEx = True
                End If  '--nothing..-
            End If  '--get rset-
        Else  '-sql server 20002005 --
            lResult = glExecSP(cnn1, "sp_who", "", sErrors, rs1)
            If lResult <> 0 Then
                s1 = "Failed to retrieve list of users.." & vbCrLf & _
                       "Error msg: " & vbCrLf & sErrors & vbCrLf
                '= If gbDebug Then MsgBox(s1, MsgBoxStyle.Exclamation)
                MsgBox(s1, MsgBoxStyle.Exclamation)
                '= msLastSqlErrorMessage = s1
                '==MsgBox(sErrors, MsgBoxStyle.Critical)
            Else '--  check rs1 for users.--
                '--check recordset--
                If Not (rs1 Is Nothing) Then
                    colWhichUsers = New Collection
                    If rs1.HasRows Then  '= Not (rs1.BOF And rs1.EOF) Then  '--not empty-
                        '= If rs1.BOF Then rs1.MoveFirst()
                        While rs1.Read  '== Not rs1.EOF
                            If Not IsDBNull(rs1.Item("dbname")) Then
                                '=3069.0= If (UCase(rs1.Fields("dbname").Value) = UCase(sDBName)) And _
                                '=3069.0=               (LCase(rs1.Fields("status").Value) = "runnable") Then '-- this one is using....--
                                If (UCase(rs1.Item("dbname")) = UCase(sDBName)) And _
                                           (LCase(rs1.Item("status")) <> "dormant") And _
                                                 (LCase(rs1.Item("status")) <> "background") Then '-- this one is using..--
                                    colUser = New Collection '--each user is collection..-
                                    '--MsgBox "Found user: " + rs1("LoginName")
                                    colUser.Add(CStr(rs1.Item("loginame")), "LOGINAME")
                                    If Not IsDBNull(rs1.Item("hostname")) Then
                                        colUser.Add(CStr(rs1.Item("hostname")), "HOSTNAME")
                                    Else
                                        colUser.Add("", "HOSTNAME")
                                    End If '--null-
                                    colUser.Add("", "PROGRAM_NAME")  '-No prog name returned bu sp_who-
                                    colWhichUsers.Add(colUser)
                                End If '--this db--
                            End If '--isnull-
                            '== rs1.MoveNext()
                        End While
                    End If  '--empty..-
                    rs1.Close()
                    mbWhoUsingEx = True
                End If '--nothing..-
            End If '--exec ok-
        End If  '--sql 2005--
        rs1 = Nothing
        colUser = Nothing
    End Function '--WhoUsingEx..-
    '= = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  get current DISTINCT logged-in users...
    '--   User can have multiple sessions..--
    '--  Uses "sp_who":
    '--    in sql server 2000-  defaults to PUBLIC role..
    '--    in Sql Server 2005 needs VIEW ANY DATABASE permission
    '--      and "The public role is granted VIEW ANY DATABASE permission."
    '--        SEE:   http://msdn.microsoft.com/en-us/library/ms175892(v=sql.90).aspx   --

    '-3519.0108-  Special LOCAL Who-Using version -
    '-- for including program name in returned collection..
    '--  So that we only look at JobTracking users (for licence restrictions.)
    '==  NOTE- THIS Means that SqlServer2005 will not be returning full info..
    '-    NB: USES sys.sysprocesses- still works in Sql Server 2017..


    Private Function mbShowLoggedInUsers(ByRef colWhichUsers As Collection, _
                                          ByRef strUserList As String) As Boolean

        Dim col1 As Collection
        Dim colAllProcesses As Collection
        Dim sLogin, sHost, sItem, sProgram_name As String
        Dim sMsg, sDistinctUsers As String

        mbShowLoggedInUsers = False
        sDistinctUsers = ";"
        strUserList = ""
        '== ToolTip1.SetToolTip(labLoggedInUsers, "")
        If Not mbWhoUsingEx(mCnnSql, msSqlDbName, colAllProcesses) Then
            '= gbWhoUsing(mCnnSql, msSqlDbName, colAllProcesses) Then
            MsgBox("Failed to get user list.." & vbCrLf & _
                    "Sql cmd was 'exec sp_who'..  " & vbCrLf & vbCrLf & _
                    gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
        Else '--ok--
            '= Dim processThis As Process = Process.GetCurrentProcess
            Dim sOurName As String = msCurrentProcessName  '= processThis.ProcessName

            sMsg = "Current JobTracking users are: " & vbCrLf & vbCrLf
            colWhichUsers = New Collection
            If (colAllProcesses.Count > 0) Then
                For Each col1 In colAllProcesses
                    sLogin = Trim(col1.Item("LOGINAME"))
                    sHost = Trim(col1.Item("HOSTNAME"))
                    sProgram_name = Trim(col1.Item("program_name"))
                    '- check if this is a jobTracking program user.
                    If LCase(sProgram_name) = LCase(sOurName) Then
                        '-- IS Job Tracking User
                        sItem = LCase(sHost & "!" & sLogin)
                        If Not (InStr(sDistinctUsers, sItem & ";") > 0) Then  '--new-
                            sDistinctUsers = sDistinctUsers & LCase(sItem) & ";"
                            sMsg = sMsg & sLogin & " on: " & sHost & ".." & vbCrLf
                            colWhichUsers.Add(col1)
                        End If
                    End If  '-our name-
                Next col1 '--col1-
                '== labLoggedInUsers.Text = "Who's using JobMatix-" & vbCrLf & _
                '==                                colWhichUsers.Count & " user(s) logged in.."
                '== ToolTip1.SetToolTip(labLoggedInUsers, sMsg)
                strUserList = sMsg
            Else
                '== labLoggedInUsers.Text = vbCrLf & "No User.."
            End If  '--count.-
            Application.DoEvents()
            mbShowLoggedInUsers = True
        End If  '--who--
    End Function  '--show users.-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- Check LoggedIn Users --
    '==  mIntMaxUsersPermitted= -1  -> means no limit to users..

    Private Function mbCheckLoggedInUsers() As Boolean
        Dim colWhichUsers As Collection
        Dim sMsg, s1, s2 As String
        Dim sUserList As String = ""
        Dim col1 As Collection
        Dim bOk As Boolean = False

        '= mlStaffTimeout = -1 '--SUSPEND timing out..--
        mbCheckLoggedInUsers = False
        '-  Check who's logged in..
        '=3519.0227=  FIX looping-
        Do
            If mbShowLoggedInUsers(colWhichUsers, sUserList) Then
                '-- if ThreeUser Licence, then report and retry/cancel.
                If (mIntMaxUsersPermitted >= 0) AndAlso _
                          (colWhichUsers.Count > mIntMaxUsersPermitted) Then   '- (was 3)-our login is included in count..--
                    sMsg = "Current logins are (incl. this one): " & vbCrLf & vbCrLf
                    For Each col1 In colWhichUsers
                        s1 = Trim(col1.Item("LOGINAME"))
                        s2 = Trim(col1.Item("HOSTNAME"))
                        sMsg = sMsg & s1 & " on: " & s2 & ".." & vbCrLf
                    Next col1
                    'While (colWhichUsers.Count > mIntMaxUsersPermitted)
                    If (MsgBox("This JobMatixPOS Licence supports only " & mIntMaxUsersPermitted & _
                             " concurrent user(s) .." & vbCrLf & vbCrLf & sMsg & vbCrLf & vbCrLf & _
                             "To continue, close one of the listed users, and retry.." & vbCrLf & _
                               "Do you want to retry?", _
                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                    Else  '--no-
                        Me.Close()
                        Exit Function   '=End
                    End If
                    Call mbShowLoggedInUsers(colWhichUsers, sUserList)
                    'End While
                Else  '-ok-
                    bOk = True
                    mbCheckLoggedInUsers = True
                End If  '--check max-
            Else  '--show failed-
            End If
        Loop Until bOk '--exits ALSO when NO is replied.

        '= mlStaffTimeout = 0 '--now timing out..--
    End Function  '--check users..-
    '= = = = = = = = = = = = = = =

    '--  END of Stuff for Checking on no.of users. (ex JobTracking..)
    '--  END of Stuff for Checking on no.of users. (ex JobTracking..)
    '--  END of Stuff for Checking on no.of users. (ex JobTracking..)
    '= = = = = = = = == = = == = = = = = = = = = = = = = = = == = = =
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

    '== SPECIAL mbBrowseAndSearchTable for Customer Table.
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==       -- Looking up Customers- make special Sql-Select for Browser 
    '==              to make a column of [lastName, firstName] in browser Grid.  

    '=3519.0321=  STOLEN from clsPOS34Sale..
    '=4201.1025=  STOLEN from ucChildPayments..

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
        Try
            frmBrowse1.ShowDialog()
        Catch ex As Exception
            MsgBox("" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not (frmBrowse1.selectedRow Is Nothing) AndAlso _
                                (frmBrowse1.selectedRow.Count > 0) Then '= frmBrowse1.cancelled Then
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

    '-- Launch JobTracking..  can be silent, ot ask operator.
    '--  If Silent, then launch only if not already running..
    '--  

    Private Function mbLaunchJobTracking(Optional ByVal bSilent As Boolean = True) As Boolean

        Dim intResult As Integer  '- Double
        Dim sJT350Name, sJT350Path, sJT350Args As String
        Dim bCanLaunch As Boolean = False

        mbLaunchJobTracking = False

        '==   Target-New-Build-6201 --  (16-June-2021)
        '=  --    sJT350Name = "JmxJT420ex"
        sJT350Name = "JMxJT620ex"
        '==  END Target-New-Build-6201 --  (16-June-2021)

        sJT350Path = msAppPath & sJT350Name & ".exe"
        sJT350Args = " /server=" & msServer & _
                       " /JT_dbname=" & msSqlDbName & " /JobMatixAppName=" & msJobmatixAppName
        '-- check if process already running..
        Dim ap As Process() = Process.GetProcessesByName(sJT350Name)
        If (ap IsNot Nothing) AndAlso (ap.Length > 0) Then
            '-- is alive.. so switch to it..
            If bSilent Then
                bCanLaunch = False  '-silent means no new instance needed.
            Else  '-ask
                If MessageBox.Show("Note- A JobTracking instance is already open." & vbCrLf & _
                                   "Do you want to start another one ?", "Launching JobTracking", MessageBoxButtons.YesNo, _
                                   MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                    bCanLaunch = True
                Else  '-no- Switch to current.
                    Dim intId As IntPtr = ap(0).MainWindowHandle
                    '-found-  Make it active-
                    intResult = SetForegroundWindow(intId)
                End If  '-yes..
            End If
        Else '-not there.. so can launch..-
            bCanLaunch = True
        End If  '-nothing-
        If bCanLaunch Then
            Try
                Process.Start(sJT350Path, sJT350Args)
                mbLaunchJobTracking = True
            Catch ex As Exception
                MsgBox("ERROR executing StartProcess cmd: " & vbCrLf & sJT350Path & vbCrLf & vbCrLf & _
                       "Error: " & vbCrLf & ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
            End Try
        End If '-can launch-
    End Function  '- mbLaunchJobTracking
    '= = = = = = = = = = 
    '-===FF->

    '- NEW CHILD-- Can be SALE or ADMIN..
    '- Form Tag has type name.

    'Private Function mbAddNewChild(ByRef frmNewChild1 As Form, _
    '                               ByVal strFormClassName As String, _
    '                               ByVal strFormTabText As String, _
    '                               ByRef tabPageChild1 As TabPage) As Boolean

    '    '= Dim tabPageChild1 As TabPage = New TabPage
    '    tabPageChild1 = New TabPage
    '    tabPageChild1.Tag = strFormClassName

    '    mIntChildCount += 1
    '    '=frmNewChild1.MdiParent = Me

    '    frmNewChild1.Name = strFormClassName & "_" & CStr(mIntChildCount)
    '    frmNewChild1.Text = frmNewChild1.Name
    '    frmNewChild1.Dock = DockStyle.Fill
    '    '=frmNewChild1.Top = TabControlMain.Top + TabControlMain.Height + 3
    '    frmNewChild1.AutoSize = False
    '    '= frmNewChild1.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
    '    '= frmNewChild1.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
    '    tabPageChild1.Name = frmNewChild1.Name
    '    tabPageChild1.Font = New Font(Me.Font.FontFamily, 9)

    '    tabPageChild1.Text = strFormTabText & Space(12) '= frmNewChild1.Name & Space(5)  '--leave room for "X"
    '    tabPageChild1.ToolTipText = frmNewChild1.Text
    '    tabPageChild1.BackColor = Color.White  '= Color.Yellow
    '    tabPageChild1.CausesValidation = False

    '    TabControlMain.TabPages.Add(tabPageChild1)

    '    frmNewChild1.TopLevel = False
    '    frmNewChild1.Parent = tabPageChild1   '== Attach to tab.
    '    mColCurrentChildForms.Add(frmNewChild1, frmNewChild1.Name)

    '    '-this is to make sure that tab page is selected in the same time
    '    TabControlMain.SelectTab(tabPageChild1)

    '    '= frmNewChild1.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
    '    frmNewChild1.MinimizeBox = False
    '    frmNewChild1.MaximizeBox = False
    '    frmNewChild1.ControlBox = False
    '    frmNewChild1.ShowIcon = False
    '    '= frmNewChild1.ShowInTaskbar = False
    '    frmNewChild1.SizeGripStyle = Windows.Forms.SizeGripStyle.Hide
    '    frmNewChild1.CausesValidation = False
    '    '= frmNewChild1.doublebuffered = True

    '    frmNewChild1.WindowState = FormWindowState.Normal   '= FormWindowState.Maximized

    '    frmNewChild1.Show()
    '    '= frmNewChild1.Select()
    '    '- now position.
    '    frmNewChild1.Left = 0
    '    frmNewChild1.Top = 0
    '    frmNewChild1.Width = tabPageChild1.Width
    '    frmNewChild1.Height = tabPageChild1.Height
    '    '-- make sure form controls are resize to current form size..
    '    '--  In case main form was enlarged.
    '    'delResized = New SubFormResized(AddressOf frmNewChild1.SubFormResized)
    '    'Call delResized(TabControlSales.Width - 7, TabControlSales.Height - 7)
    '    'delResized = Nothing

    'End Function  '-new generic Child.
    '= = = = = = = = = = = = = = = = = = = == = =
    '-===FF->

    '- NEW USER CONTROL CHILD-- Can be SALE or ADMIN..

    Private Function mbAddNewChild2(ByRef ucNewChild1 As UserControl, _
                                  ByVal strFormClassName As String, _
                                   ByVal strFormTabText As String, _
                                   ByRef tabPageChild1 As TabPage) As Boolean

        '= Dim tabPageChild1 As TabPage = New TabPage
        tabPageChild1 = New TabPage
        tabPageChild1.Tag = strFormClassName

        mIntChildCount += 1
        '=frmNewChild1.MdiParent = Me

        ucNewChild1.Name = strFormClassName & "_" & CStr(mIntChildCount)
        ucNewChild1.Text = ucNewChild1.Name
        ucNewChild1.Dock = DockStyle.Fill
        '=frmNewChild1.Top = TabControlMain.Top + TabControlMain.Height + 3
        ucNewChild1.AutoSize = False

        '=ucNewChild1.Dock = DockStyle.Fill
        '=frmNewChild1.Top = TabControlMain.Top + TabControlMain.Height + 3
        ucNewChild1.AutoSize = False
        '= frmNewChild1.AutoSizeMode = Windows.Forms.AutoSizeMode.GrowAndShrink
        '= frmNewChild1.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        tabPageChild1.Name = ucNewChild1.Name
        tabPageChild1.Font = New Font(Me.Font.FontFamily, 9)

        tabPageChild1.Text = strFormTabText & Space(12) '= frmNewChild1.Name & Space(5)  '--leave room for "X"
        '= tabPageChild1.Text = ucNewChild1.Text
        tabPageChild1.ToolTipText = ucNewChild1.Text
        tabPageChild1.BackColor = ColorTranslator.FromOle(RGB(218, 228, 241)) '=light blue  '= Color.Yellow
        tabPageChild1.CausesValidation = False

        TabControlMain.TabPages.Add(tabPageChild1)

        '= ucNewChild1.TopLevel = False
        ucNewChild1.Parent = tabPageChild1   '== Attach to tab.

        '--NEW !-
        tabPageChild1.Controls.Add(ucNewChild1)

        mColCurrentChildForms.Add(ucNewChild1, ucNewChild1.Name)

        '-this is to make sure that tab page is selected in the same time
        TabControlMain.SelectTab(tabPageChild1)

        ucNewChild1.CausesValidation = False
        '= frmNewChild1.doublebuffered = True
        ucNewChild1.BorderStyle = BorderStyle.FixedSingle   '= BorderStyle.Fixed3D
        ucNewChild1.BackColor = Color.White

        ucNewChild1.Show()
        '= frmNewChild1.Select()
        '- now position.
        ucNewChild1.Left = 0
        ucNewChild1.Top = 0
        ucNewChild1.Width = tabPageChild1.Width
        ucNewChild1.Height = tabPageChild1.Height
        '-- make sure form controls are resize to current form size..
        '--  In case main form was enlarged.
        'delResized = New SubFormResized(AddressOf frmNewChild1.SubFormResized)
        'Call delResized(TabControlSales.Width - 7, TabControlSales.Height - 7)
        'delResized = Nothing

    End Function  '-new generic Child.
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- New Admin Tabbed Children..

    '-1. frmChildStock-

    '-- NEW Stock Admin Child..
    '-- NEW Stock Admin Child..

    Private Function mbNewStockChild() As Boolean

        Dim ucNewChildStock1 As ucChildStockAdmin = New ucChildStockAdmin
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucNewChildStock1.StaffName = msMainStaffName
        ucNewChildStock1.SqlServer = msServer
        ucNewChildStock1.connectionSql = mCnnSql '--job tracking sql connection..-
        ucNewChildStock1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
        ucNewChildStock1.DBname = msSqlDbName
        ucNewChildStock1.VersionPOS = msDllversion
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucNewChildStock1.PosChildClosing, AddressOf posChild_Closing

        Call mbAddNewChild2(ucNewChildStock1, "ucChildStockAdmin", "Stock Admin", tabPageChild1)

        ucNewChildStock1.form_left = Me.Left '=  Me.Left
        ucNewChildStock1.form_top = Me.Top + 40  '=Me.Top + 30
        ucNewChildStock1.delReport = AddressOf Me.subChildReport
        '= frmNewChildStock1.delChildSignedOn = AddressOf Me.subChildSignedOn

        '= ucNewChildStock1.Show()  '= frmStock1.ShowDialog()

        delResized = New SubFormResized(AddressOf ucNewChildStock1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function  '-mbNewStockChild-
    '= = = = = = = = == = = = = 
    '-===FF->

    '-- New Admin Tabbed Children..
    '-2.  New Child- F i n d S e r i a l..  (To Browse Serials..)

    Private Function mbNewSerialLookupChild() As Boolean

        Dim ucNewChildSerial1 As ucChildSerialLookup
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucNewChildSerial1 = New ucChildSerialLookup(mCnnSql, msSqlDbName, mColSqlDBInfo)
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucNewChildSerial1.PosChildClosing, AddressOf posChild_Closing

        '= tabPageChild1.Tag = "frmChildStock"
        Call mbAddNewChild2(ucNewChildSerial1, "ucChildSerialLookup", "Serial Lookup", tabPageChild1)

        ucNewChildSerial1.delReport = AddressOf Me.subChildReport
        '= frmNewChildStock1.delChildSignedOn = AddressOf Me.subChildSignedOn

        '= frmNewChildSerial1.Show()  '= frmStock1.ShowDialog()

        delResized = New SubFormResized(AddressOf ucNewChildSerial1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function  '-mbNewFindSerialChild-
    '= = = = = = = = == = = = = = = = = = =

    '-- New Admin Tabbed Children..
    '-3.  New Child-NewStocktake Child--

    Private Function mbNewStocktakeChild() As Boolean
        Dim ucNewChildStocktake1 As ucChildStocktake
        Dim tabPageChild1 As TabPage '= = New TabPage

        '--ONLY ONE stocktake instance allowed..
        Dim s1 As String
        For Each ucInstance As UserControl In mColCurrentChildForms
            s1 = LCase(ucInstance.Name)
            If (InStr(s1, "stocktake") > 0) Then
                MsgBox("Sorry, we can't proceed with this request-" & vbCrLf & _
                         " There is already one Stocktake instance running..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
        Next ucInstance

        ucNewChildStocktake1 = New ucChildStocktake(Me, mCnnSql, msServer, msSqlDbName, mColSqlDBInfo, _
                                         msDllversion, mIntMainStaff_id, msMainStaffName)
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucNewChildStocktake1.PosChildClosing, AddressOf posChild_Closing

        Call mbAddNewChild2(ucNewChildStocktake1, "ucChildStocktake", "Stocktake", tabPageChild1)

        ucNewChildStocktake1.delReport = AddressOf Me.subChildReport
 
        delResized = New SubFormResized(AddressOf ucNewChildStocktake1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function  '- NewStocktakeChild-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- New Admin Tabbed Children..
    '-4.  New Purchase Order Child-- 
    '-  (Uses ucChildGoodsRecvd)
    '--  OR GoodsReceived if NOT bIsPurchaseOrder..--

    Private Function mbNewPurchaseOrderChild(Optional ByVal bIsPurchaseOrder As Boolean = True) As Boolean
        Dim ucChildGoodsRecvd1 As ucChildGoodsRecvd
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucChildGoodsRecvd1 = New ucChildGoodsRecvd
        ucChildGoodsRecvd1.IsPurchaseOrder = bIsPurchaseOrder
        ucChildGoodsRecvd1.versionPOS = msDllversion
        ucChildGoodsRecvd1.StaffId = mIntMainStaff_id
        ucChildGoodsRecvd1.StaffName = msMainStaffName
        ucChildGoodsRecvd1.SqlServer = msServer
        ucChildGoodsRecvd1.connectionSql = mCnnSql '--job tracking sql connenction..-
        ucChildGoodsRecvd1.dbInfoSql = mColSqlDBInfo
        ucChildGoodsRecvd1.PreferredColumnsSuppliers = mColPrefsSupplier
        ucChildGoodsRecvd1.PreferredColumnsStock = mColPrefsStock
        ucChildGoodsRecvd1.DBname = msSqlDbName
        ucChildGoodsRecvd1.form_left = Me.Left '= Me.Left
        ucChildGoodsRecvd1.form_top = Me.Top + 40  '= Me.Top + 40
        ucChildGoodsRecvd1.UserLogo = mImageUserLogo

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChildGoodsRecvd1.PosChildClosing, AddressOf posChild_Closing

        If bIsPurchaseOrder Then
            Call mbAddNewChild2(ucChildGoodsRecvd1, "ucChildGoodsRecvd", "Purchase Orders", tabPageChild1)
        Else
            Call mbAddNewChild2(ucChildGoodsRecvd1, "ucChildGoodsRecvd", "Goods Received", tabPageChild1)
        End If

        ucChildGoodsRecvd1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChildGoodsRecvd1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function  '- mbNewPurchaseOrderChild-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- New Admin Tabbed Children..
    '-5.  New Debtors Payments Child-- 

    Private Function mbNewPaymentsChild() As Boolean
        Dim ucChildPayments1 As ucChildPayments
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucChildPayments1 = New ucChildPayments(Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                          mColPrefsCustomer, msSettingsPath, _
                                               msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChildPayments1.PosChildClosing, AddressOf posChild_Closing

        Call mbAddNewChild2(ucChildPayments1, "ucChildPayments", "Payments", tabPageChild1)

        ucChildPayments1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChildPayments1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing
    End Function  '-Payments-
    '= = = = = = = = = = = = = = = = = = = = = 

    '-- New Admin Tabbed Children..
    '-6.  mbNewSubscription Child-- 

    Private Function mbNewSubscriptionChild() As Boolean
        Dim ucChildSubs1 As ucChildSubscription
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucChildSubs1 = New ucChildSubscription()

        ucChildSubs1.SqlServer = msServer
        ucChildSubs1.ComputerName = msComputerName
        ucChildSubs1.connectionSql = mCnnSql '-- sql connenction..-
        ucChildSubs1.dbInfoSql = mColSqlDBInfo
        ucChildSubs1.staff_id = mIntMainStaff_id
        ucChildSubs1.UserLogo = mImageUserLogo

        ucChildSubs1.DBname = msSqlDbName
        ucChildSubs1.VersionPOS = msDllversion
        '= frmlookup1.supplier_id = mIntSupplier_id
        '= frmlookup1.SupplierName = txtSupplierName.Text
        ucChildSubs1.staffNname = msMainStaffName
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChildSubs1.PosChildClosing, AddressOf posChild_Closing


        Call mbAddNewChild2(ucChildSubs1, "ucChildSubscription", "Subscriptions", tabPageChild1)

        ucChildSubs1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChildSubs1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function  '-mbNewSubscriptionChild-
    '= = = = = = = = == = = == = = = = = = =
    '-===FF->

    '-- New Admin Tabbed Children..
    '-7.  NewCustomer Child-- 

    Private Function mbNewCustomerChild() As Boolean
        Dim ucChild1 As ucChildCustomer
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucChild1 = New ucChildCustomer
        ucChild1.StaffId = mIntMainStaff_id
        ucChild1.StaffName = msMainStaffName
        ucChild1.SqlServer = msServer
        ucChild1.connectionSql = mCnnSql '--job tracking sql connection..-
        ucChild1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
        ucChild1.DBname = msSqlDbName
        ucChild1.VersionPOS = msDllversion

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChild1.PosChildClosing, AddressOf posChild_Closing

        '=4219.1214=
        '-- Add Handler for NewJob Event raised from ucChildCustomer..
        AddHandler ucChild1.PosChildNewJobRequest, AddressOf PosChild_NewJobRequest


        '==  Target-New-Build-4262 -- 
        '==      --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
        '==      --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)
        '-- EVENT to get Child JobMaint UserControl loaded..
        AddHandler ucChild1.PosChildJobMaintRequest, AddressOf PosChildJobMaintRequest
        '==END  Target-New-Build-4262 -- 


        Call mbAddNewChild2(ucChild1, "ucChildCustomer", "Customers", tabPageChild1)

        ucChild1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChild1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function '- NewCustomer Child--
    '= = = = = = = = == = = == = = = = = = =

    '-- New Admin Tabbed Children..
    '- 8. Statements--

    Private Function mbNewStatementsChild() As Boolean
        Dim ucChildStatements1 As ucChildStatements
        Dim tabPageChild1 As TabPage '= = New TabPage

        '= Dim frmStatements1 As ucChildStatements
        ucChildStatements1 = New ucChildStatements(Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                         mColPrefsCustomer, msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChildStatements1.PosChildClosing, AddressOf posChild_Closing

        Call mbAddNewChild2(ucChildStatements1, "ucChildStatements", "Statements", tabPageChild1)

        ucChildStatements1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChildStatements1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing
    End Function  '-Statements-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- New Admin Tabbed Children..
    '- 9. Laybys--

    Private Function mbNewLaybysChild() As Boolean
        Dim ucChildLaybys1 As ucChildLaybys
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucChildLaybys1 = New ucChildLaybys(msComputerName, Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                          msDllversion, mIntMainStaff_id, msMainStaffName)
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChildLaybys1.PosChildClosing, AddressOf posChild_Closing

        Call mbAddNewChild2(ucChildLaybys1, "ucChildLaybys", "Laybys", tabPageChild1)

        ucChildLaybys1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChildLaybys1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing
    End Function  '-LayBys-
    '= = = = = = = = = = = = = = = = = = = = = 

    '-- New Trans.Lookup Child..

    Private Function mbNewTransLookupChild() As Boolean
        Dim ucChildTrLookup1 As ucTransLookup
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucChildTrLookup1 = New ucTransLookup(mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                                   mImageUserLogo, mIntMainStaff_id, msMainStaffName)
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChildTrLookup1.PosChildClosing, AddressOf posChild_Closing

        Call mbAddNewChild2(ucChildTrLookup1, "ucTransLookup", "Trans.Lookup", tabPageChild1)

        ucChildTrLookup1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChildTrLookup1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function   '-- New Trans.Lookup Child..
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->


    '--  NEW reports Child...
    '==   FOR- 4233.0421  --

    Private Function mbNewPosReportsChild() As Boolean
        Dim ucChildPosReports1 As ucChildPosReports
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucChildPosReports1 = New ucChildPosReports(Me)

        ucChildPosReports1.StaffName = msMainStaffName
        ucChildPosReports1.connectionSql = mCnnSql '--job tracking sql connenction..-
        ucChildPosReports1.DBname = msSqlDbName
        ucChildPosReports1.ColSqlDBInfo = mColSqlDBInfo
        ucChildPosReports1.versionPOS = msDllversion

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChildPosReports1.PosChildClosing, AddressOf posChild_Closing

        Call mbAddNewChild2(ucChildPosReports1, "ucChildPosReports", "POS Reports", tabPageChild1)

        ucChildPosReports1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChildPosReports1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function  '-mbNewPosReportsChild-
    '= = = = = = = = == = = =  == ===== 
    '-- END OF NEW reports Child...
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Process "NewJobRequest" event fromucChildCustomer..
    '==-- Child uses EVENT Delegate to signal "NewJobRequest" to Main Parent..
    '-- Child has this event definition..
    '--   Public Event PosChildNewJobRequest(ByVal strChildName As String)

    Private mRetailHostPOS1 As _clsRetailHost
    '-- Show NewJob Form..
 

    Public Sub PosChild_NewJobRequest(ByVal strChildName As String, _
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
        Dim tabPageChild1 As TabPage
        Dim ucChild1 As ucChildNewJob

        '--  create new JobMatixPOS interface class..
        mRetailHostPOS1 = New clsRetailHostJMPOS
        '--Initialise:  Pass sql connection, dbname and colTables..
        Try
            Call mRetailHostPOS1.SetConnection(msServer, mCnnSql, mColSqlDBInfo, msSqlDbName)
        Catch ex As Exception
            MsgBox("Error- Failed to Connect POS to Jobs." & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        '--  Load Child UserControl..
        ucChild1 = New ucChildNewJob

        ucChild1.StaffId = mIntMainStaff_id
        ucChild1.StaffName = msMainStaffName
        '= ucChild1.SqlServer = msServer
        ucChild1.connectionSql = mCnnSql '--job tracking sql connection..-
        ucChild1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
        ucChild1.retailHost = mRetailHostPOS1

        ucChild1.dbInfoSql = mColSqlDBInfo

        '- THESE temp. omitted.
        'ucChild1.LabourMinCharge = mCurLabourMinCharge
        'ucChild1.NotificationCostLimit = mCurNotificationCostLimit
        ucChild1.LicenceOK = True  '= FOW NOW..  mbLicenceOK

        ''-- Passing JOB-NO Indicate RE-EDIT existing Agreement..--
        ucChild1.JobId = intJob_id '==== CLng(colKeys(1))
        ucChild1.IsOnSiteJob = bIsNewOnSiteJob
        'ucChild1.UserLogo = picUserLogo.Image '--pass logo..-

        '==  Target-New-Build-4262 -- 
        ucChild1.UserLogo = mImageUserLogo '--pass logo..-
        '== END Target-New-Build-4262 -- 


        '=If bCheckingIn Then ucChild1.IsCheckIn = True
        ucChild1.IsCheckIn = bIsCheckin

        ucChild1.CustomerBarcode = strCustomerBarcode
        ucChild1.CustomerId = intCustomer_id
        ucChild1.CustomerCompany = strCustomerCompany
        ucChild1.CustomerName = strCustomerName
        ucChild1.CustomerPhone = strCustomerPhone
        ucChild1.CustomerMobile = strCustomerMobile

        'If mColCustomerJobsGoods IsNot Nothing Then
        '    ucChild1.CustomerJobsGoods = mColCustomerJobsGoods
        'End If  '--nothing-

        Call mbAddNewChild2(ucChild1, "ucChildNewJob", "New/Amend Job", tabPageChild1)

        ucChild1.delReport = AddressOf Me.subChildReport
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChild1.PosChildClosing, AddressOf posChild_Closing

        DoEvents()

    End Sub  '- PosChild_NewJobRequest-
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '==  Target-New-Build-4262 -- 
    '==      --  ADD Items to Jobs Context Menu to call Maint Form (View/Update)
    '==      --  ADD Event to signal MAIN to load a child JobMaint Form (View/Update)

    '-- EVENT to get Child JobMaint UserControl loaded..

    Public Sub PosChildJobMaintRequest(ByVal strChildName As String, _
                                       ByVal bIsServiceUpdate As Boolean, _
                                       ByVal bIsDeliveryUpdate As Boolean, _
                                       ByVal intCustomer_id As Integer, _
                                       ByVal strCustomerBarcode As String, _
                                       ByVal intJob_id As Integer)

        Dim tabPageChild1 As TabPage
        Dim ucChild1 As ucChildJobMaint
        Dim colRMCustomerDetails As Collection
        Dim bOk As Boolean
        Dim sTabText As String

        '--  create new JobMatixPOS interface class..
        mRetailHostPOS1 = New clsRetailHostJMPOS
        '--Initialise:  Pass sql connection, dbname and colTables..
        Try
            Call mRetailHostPOS1.SetConnection(msServer, mCnnSql, mColSqlDBInfo, msSqlDbName)
        Catch ex As Exception
            MsgBox("Error- Failed to Connect POS to Jobs." & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        '--  Load Child UserControl..
        ucChild1 = New ucChildJobMaint

        ucChild1.parentForm = Me
        ucChild1.StaffId = mIntMainStaff_id
        ucChild1.StaffName = msMainStaffName

        ucChild1.StaffBarcode = msMainStaffBarcode
        ucChild1.UserLogo = mImageUserLogo '--pass logo..-

        '= ucChild1.SqlServer = msServer
        ucChild1.connectionSql = mCnnSql '--job tracking sql connection..-
        ucChild1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
        ucChild1.retailHost = mRetailHostPOS1
        ucChild1.dbInfoSql = mColSqlDBInfo
        ucChild1.JobNo = intJob_id

        sTabText = "View "
        If bIsServiceUpdate Then
            ucChild1.ServiceUpdate = True '-- request service type--
            sTabText = "Update "
        ElseIf bIsDeliveryUpdate Then
            ucChild1.DeliveryUpdate = True '-- delivery type--
            sTabText = "Deliver  "
        End If

        '-- Get Customer details in RM form..
        If (intCustomer_id > 0) Then  '--use _id.. -
            bOk = mRetailHostPOS1.customerGetCustomerRecord("", intCustomer_id, colRMCustomerDetails)
        Else  '--no id..-
            '--  MUST Force to use BARCODE to get cust..
            bOk = mRetailHostPOS1.customerGetCustomerRecord(strCustomerBarcode, -1, colRMCustomerDetails)
        End If
        If Not bOk Then
            MsgBox("Error- Failed to Get POS Cust. Details for Jobs.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '-- Pass to child the cust. details collection..
        ucChild1.CustomerDetails = colRMCustomerDetails

        Call mbAddNewChild2(ucChild1, "ucChildJobMaint", sTabText & "Job " & CStr(intJob_id), tabPageChild1)

        ucChild1.delReport = AddressOf Me.subChildReport
        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChild1.PosChildClosing, AddressOf posChild_Closing


    End Sub  '-PosChildJobMaintRequest-
    '== END  Target-New-Build-4262 -- 
    '= = = = = = = = = = = = = = = = = = = = == = = = = = == = = = = = == =
    '-===FF->

    '==   Target-New-Build-4284 --  (08-Nov-2020)
    '==  A.  New Child USERCONTROL to move Suppliers Admin into Main Tab Control.
    '==
    '-- New Admin Tabbed Children..
    '-x.  New Supplier Child-- 

    Private Function mbNewSupplierChild() As Boolean
        Dim ucChild1 As ucChildSupplier
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucChild1 = New ucChildSupplier
        ucChild1.StaffId = mIntMainStaff_id
        ucChild1.StaffName = msMainStaffName
        ucChild1.SqlServer = msServer
        ucChild1.connectionSql = mCnnSql '--job tracking sql connection..-
        ucChild1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
        ucChild1.DBname = msSqlDbName
        ucChild1.VersionPOS = msDllversion

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChild1.PosChildClosing, AddressOf posChild_Closing

        '=4219.1214=
        '-- Add Handler for NewJob Event raised from ucChildCustomer..
        '=AddHandler ucChild1.PosChildNewJobRequest, AddressOf PosChild_NewJobRequest

        Call mbAddNewChild2(ucChild1, "ucChildSupplier", "Suppliers", tabPageChild1)

        ucChild1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChild1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function '- NewSupplier Child--
    '==  END Target-New-Build-4284 --  (08-Nov-2020-2020)
    '= = = = = = = = == = = == = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = == = = = = = == = = = = = == =
    '-===FF->

    '==   Target-New-Build-4284-EXTRA-EXTRA --  (22-Nov-2020)
    '==
    '==  -- New Child USERCONTROL to move STAFF Admin into Main Tab Control.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    Private Function mbNewStaffChild() As Boolean
        Dim ucChild1 As ucChildStaff
        Dim tabPageChild1 As TabPage '= = New TabPage

        ucChild1 = New ucChildStaff
        ucChild1.StaffId = mIntMainStaff_id
        ucChild1.StaffName = msMainStaffName
        ucChild1.SqlServer = msServer
        ucChild1.connectionSql = mCnnSql '--job tracking sql connection..-
        ucChild1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
        ucChild1.DBname = msSqlDbName
        ucChild1.VersionPOS = msDllversion

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucChild1.PosChildClosing, AddressOf posChild_Closing
        '=4219.1214=
        '-- Add Handler for NewJob Event raised from ucChildCustomer..
        '= AddHandler ucChild1.PosChildNewJobRequest, AddressOf PosChild_NewJobRequest

        Call mbAddNewChild2(ucChild1, "ucChildStaff", "Staff", tabPageChild1)

        ucChild1.delReport = AddressOf Me.subChildReport

        delResized = New SubFormResized(AddressOf ucChild1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

    End Function '- NewCustomer Child--
    '==  END Target-New-Build-4284-EXTRA --  (22-Nov-2020)
    '= = = = = = = = == = = == = = = = = = =
    '-===FF->

    '--L o a d-
    '--L o a d-

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim s1 As String

        mlFormDesignHeight = Me.Height '--save starting dimensions..-
        mlFormDesignWidth = Me.Width '--save starting dimensions..-

        msSettingsPath = gsLocalSettingsPath(msJobmatixAppName) '= default Jobmatix33=
        '-test-
        '= MsgBox("Settings path: " & vbCrLf & msSettingsPath, MsgBoxStyle.Information)

        mLocalSettings1 = New clsLocalSettings(msSettingsPath)


        '- fix window menu..
        '-- mnuWindow.MdiList = True  
        '-- NOT THERE !!
        '= https://social.msdn.microsoft.com/Forums/en-US/18be8f23-d498-49af-aab1-f0f25398aaf3/mdilist-property-missing?forum=vbide
        '= I think I found the solution to this problem,
        '-- the instructions given in the MSDN walkthrough for this are incorrect.  
        '--  To define which of your menu items will be the 'Window' command to hold the child forms, 
        '-  set the MdiWindowListItem property of your MenuStrip control to the proper ToolStripMenuItem; 
        '-- don() 't go banging your head searching for the ToolStripMenuItem.MdiList property 
        '--     as it doesn't appear to exist.

        mSysInfo1 = New clsSystemInfo(mCnnSql)

        Try
            '=Call CenterForm(Me)
            '= TabControlMain.DrawMode = TabDrawMode.OwnerDrawFixed
            TabControlMain.DrawMode = TabDrawMode.OwnerDrawFixed

            s1 = msDayOfWeek(CDate(DateTime.Today))
            '== If gbDebug Then MsgBox(" day is: " & s1 & "....", MsgBoxStyle.Information)
            '= tsLabToday.Text = s1 & ", " & vbCrLf & Format(CDate(DateTime.Today), "dd-MMM-yyyy")

            labToday.Text = Format(CDate(DateTime.Today), "ddd dd-MMM-yyyy")

            '= tsLabSaleTillId.Text = "- Till-"
            btnMainTill.Text = "- Till-"

            msCurrentUserName = gsGetCurrentUser()
            '= tsLabServer.Text = "Server:" & vbCrLf & msServer
            '= tsLabServer.ToolTipText = "Sql-Server:" & vbCrLf & msServer

            msCurrentUserNT = msSqlServerComputer & "\" & msCurrentUserName

            '=3501.0809=
            Dim sAppPath As String = gsAppPath()
            If VB.Right(sAppPath, 1) <> "\" Then sAppPath = sAppPath & "\"
            msAppPath = sAppPath

            '=3501.0809-- MUST DO THIS FIRST !!-
            '=3501.0809-- MUST DO THIS FIRST !!-
            '=3501.0809-- MUST DO THIS FIRST !!-
            Call gSubSetAppName(msJobmatixAppName)

            '-- show version on main form.-
            msDllversion = "JobMatixPOS DLL ver: " & msGetDllversion()
            '= mLabSaleDLLversion.Text = msDllversion
            Call gbLogMsg(gsRuntimeLogPath, " == " & msDllversion & " Load Function is starting..")

            '--  staff--
            mColPrefsStaff = New Collection
            mColPrefsStaff.Add("barcode")
            mColPrefsStaff.Add("docket_name")
            mColPrefsStaff.Add("lastname")
            mColPrefsStaff.Add("firstname")
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
            mColPrefsCustomer.Add("barcode")
            mColPrefsCustomer.Add("lastname")
            mColPrefsCustomer.Add("firstname")
            mColPrefsCustomer.Add("companyName")
            mColPrefsCustomer.Add("phone")
            mColPrefsCustomer.Add("mobile")
            mColPrefsCustomer.Add("customer_id")
            mColPrefsCustomer.Add("isAccountCust")
            mColPrefsCustomer.Add("creditLimit")
            mColPrefsCustomer.Add("pricingGrade")
            mColPrefsCustomer.Add("inactive")
            mColPrefsCustomer.Add("address")
            '==mColPrefsCustomer.Add("addr2")
            '=mColPrefsCustomer.Add("addr3")
            mColPrefsCustomer.Add("suburb")
            mColPrefsCustomer.Add("email")
            mColPrefsCustomer.Add("date_modified")
            mColPrefsCustomer.Add("comments")

            '-- Supplier --
            mColPrefsSupplier = New Collection
            mColPrefsSupplier.Add("barcode")
            mColPrefsSupplier.Add("supplierName")
            mColPrefsSupplier.Add("contactName")
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
            mColPrefsStock.Add("cat1")   '--fkey-
            mColPrefsStock.Add("cat2")   '-fkey-
            mColPrefsStock.Add("description")
            mColPrefsStock.Add("barcode")
            mColPrefsStock.Add("brandName")
            mColPrefsStock.Add("productPicture")
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

            Me.Text = "POS Main-  User: " & msCurrentUserNT & ";  " & msDllversion & ".."

            Me.Visible = True

            'grpBoxAdmin.Text = ""
            'grpBoxAdminPage.Text = ""
            'grpBoxAdmin.Enabled = False  '--needs admin sign on.
            shapedPanelAdmin.Enabled = False

            '== btnCashDrawers.Enabled = False  '-can only change on startup.
            '== 3501.1024-- Enabled now- was disabled by mistake.

            '= mnuDBInfo.Text = "DB: " & msSqlDbName & " on: " & msServer

            '-3401.415=
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
            '= labStatus.Text = ""
            '- clear designed pages..
            TabControlMain.TabPages.Clear()
            labAdminStaffName.Text = ""
            msJobMatixVersion = msJobmatixAppName & "-  v" & CStr(My.Application.Info.Version.Major) & "." & _
                                 My.Application.Info.Version.Minor & ". Build: " & _
                              My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision

            mColCurrentChildForms = New Collection
            Call CenterForm(Me)

            '== VERSION 4.0 ==
            '== VERSION 4.0 ==
            '== VERSION 4.0 ==
            '--  Popup menu for Top menu buttons...-

            '-- Stock menu-
            'mContextMenuStockAction = New ContextMenu
            'mnuMainStockAdmin.Name = "mnuMainStockAdmin"
            'mContextMenuStockAction.MenuItems.Add(mnuMainStockAdmin)
            ''-- add all menu items..
            'mContextMenuStockAction.MenuItems.Add(mnuStockActionSep1)
            'mnuMainStockSerials.Name = "mnuMainStockSerials"
            'mContextMenuStockAction.MenuItems.Add(mnuMainStockSerials)
            'mContextMenuStockAction.MenuItems.Add(mnuStockActionSep3)

            'mnuMainStockStocktake.Name = "mnuMainStockStocktake"
            'mContextMenuStockAction.MenuItems.Add(mnuMainStockStocktake)

            'mnuMainStockLabels.Name = "mnuMainStockLabels"
            'mContextMenuStockAction.MenuItems.Add(mnuMainStockLabels)

            '-- Purchases/GoodsReceived..
            'mContextMenuPurchases = New ContextMenu
            'mnuMainPurchaseOrders.Name = "mnuMainPurchaseOrders"
            'mContextMenuPurchases.MenuItems.Add(mnuMainPurchaseOrders)
            ''=mContextMenuStockAction.MenuItems.Add(mnuStockActionSep1)
            'mnuMainGoodsReceived.Name = "mnuMainGoodsReceived"
            'mContextMenuPurchases.MenuItems.Add(mnuMainGoodsReceived)
            'mnuMainGoodsReturned.Name = "mnuMainGoodsReturned"
            'mContextMenuPurchases.MenuItems.Add(mnuMainGoodsReturned)
            'mnuMainGoodsSuppliers.Name = "mnuMainGoodsSuppliers"
            'mContextMenuPurchases.MenuItems.Add(mnuMainGoodsSuppliers)

            '-- Categories..
            'mContextMenuCategories = New ContextMenu
            'mnuMainCategoriesCat1.Name = "mnuMainCategoriesCat1"
            'mContextMenuCategories.MenuItems.Add(mnuMainCategoriesCat1)
            'mnuMainCategoriesCat2.Name = "mnuMainCategoriesCat2"
            'mContextMenuCategories.MenuItems.Add(mnuMainCategoriesCat2)
            'mnuMainCategoriesBrands.Name = "mnuMainCategoriesBrands"
            'mContextMenuCategories.MenuItems.Add(mnuMainCategoriesBrands)


            '-- TILL-
            'Private mContextMenuTillAction As ContextMenu
            '' Till Functions- 
            ''-- Show Last Transaction.
            ''-- Change Till.
            ''-- CashUp/Till balance.
            'Private WithEvents mnuMainTillLastTran As New MenuItem("Show Last Transaction")
            'Private WithEvents mnuMainTillChange As New MenuItem("Change Till")
            'Private WithEvents mnuMainTillBalance As New MenuItem("CashUp/Till balance")

            'mContextMenuTillAction = New ContextMenu
            'mnuMainTillLastTran.Name = "mnuMainTillLastTran"
            'mContextMenuTillAction.MenuItems.Add(mnuMainTillLastTran)
            ''---
            'mnuMainTillChange.Name = "mnuMainTillChange"
            'mContextMenuTillAction.MenuItems.Add(mnuMainTillChange)
            ''--
            'mnuMainTillBalance.Name = "mnuMainTillBalance"
            'mContextMenuTillAction.MenuItems.Add(mnuMainTillBalance)
            '--
            '== laybys/CreditNotes menu.
            'mContextMenuLaybys = New ContextMenu
            'mnuLaybysLaybys.Name = "mnuLaybysLaybys"
            'mContextMenuLaybys.MenuItems.Add(mnuLaybysLaybys)

            'mnuLaybysCreditNotes.Name = "mnuLaybysCreditNotes"
            'mContextMenuLaybys.MenuItems.Add(mnuLaybysCreditNotes)

            '-- Accounts DropDown button..
            'mContextMenuAccounts = New ContextMenu
            'mnuAccountPayments.Name = "mnuAccountPayments"
            'mContextMenuAccounts.MenuItems.Add(mnuAccountPayments)

            'mnuAccountSubs.Name = "mnuAccountSubs"
            'mContextMenuAccounts.MenuItems.Add(mnuAccountSubs)

            'mnuAccountCustomers.Name = "mnuAccountCustomers"
            'mContextMenuAccounts.MenuItems.Add(mnuAccountCustomers)

            'mnuAccountStatements.Name = "mnuAccountStatements"
            'mContextMenuAccounts.MenuItems.Add(mnuAccountStatements)
            '= = = = =  = = = = = = = = = = = =

            TabControlMain.BackColor = ColorTranslator.FromOle(RGB(180, 217, 255))  '- Light Blue.
            '--    doesn't appear on VS prop. list..

            '-Toolstrip- Pro Rendering.
            '-Toolstrip- Pro Rendering.
            clsTsRendererPOS1 = New clsTsRendererPOS  '-our sub-class..
            '= tsAccounts.RenderMode = ToolStripRenderMode.Professional
            '= tsAccounts.Renderer = clsTsRendererPOS1

            '= tsStock.RenderMode = ToolStripRenderMode.Professional
            '= tsStock.Renderer = clsTsRendererPOS1

            ToolStripFile.RenderMode = ToolStripRenderMode.Professional
            ToolStripFile.Renderer = clsTsRendererPOS1

            Exit Sub

        Catch ex As Exception
            MsgBox("Error in PosMain Main Load function.." & vbCrLf & ex.Message)
            Me.Close()
            '= End
        End Try

    End Sub  '-load-
    '= = = = = = = = =
    '-===FF->

    '- Shown--

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim s1, sFileName As String
        Dim v1 As Object

        '= mSysInfo1 = New clsSystemInfo(mCnnSql)

        '--  Check licence,  and Till..
        '==    3501.0724 24-July-2018= 
        '==       -- "NewInJobMatix35.htm" is now picked at runtime from Runtime Directory.-
        '= msJobmatixAppName=
        '=4201.0627=- make html file name from msJobmatixAppName..
        sFileName = "NewInJobMatix"
        If IsNumeric(VB.Right(msJobmatixAppName, 2)) Then
            sFileName &= VB.Right(msJobmatixAppName, 2) & ".htm"
        Else '= default.
            sFileName = "NewInJobMatix35.htm"
        End If

        Dim sNewInJobMatixPath = msAppPath & sFileName  '= "NewInJobMatix35.htm"
        Try
            strAboutJobMatix3HTML = My.Computer.FileSystem.ReadAllText(sNewInJobMatixPath)
        Catch ex As Exception
            MsgBox("ERROR- No 'NewInJobMatix35' HTML file returned.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
        '== strNewInJobMatix3083HTML = rtfJobDetails.Rtf '--save for dialog..--
        '== done ==
        '=3519.0110=
        '-3519.0108= - But we need this here-
        Call gbSetupSqlVersion(mCnnSql)
        msSqlVersion = gsGetSqlVersion()
        '-- SAVE id for who_using.. mIntJobMatixDBid --
        If gbGetSelectValueEx(mCnnSql, "SELECT DB_ID() AS current_db_id;", v1) Then
            If Not (v1 Is Nothing) Then
                mIntJobMatixDBid = CLng(v1)
            End If
        End If  '--get-
        Call gSetJobMatixDBid(mIntJobMatixDBid)

        '=3519.0110=
        Dim processThis As Process = Process.GetCurrentProcess
        msCurrentProcessName = processThis.ProcessName

        '--  WARN against old versions of Sql Server.
        If Not gbIsSqlServer2008Plus() Then  '=If gbIsSqlServer2005Plus() Then
            MsgBox("PLease Note- " & vbCrLf & _
                   "JobMatix now needs a minimum Sql Version of 2008-R2" & vbCrLf & vbCrLf & _
                   "To update, Backup your JobMatix database, " & vbCrLf & _
                   "    install Sql Server 2008R2 or later, " & _
                   "    and RESTORE the backup DB to the new server..", MsgBoxStyle.Exclamation)
        End If

        '-- POS Licence--
        mClsJmxPOS31_Licence = New clsJMxPOS31(mCnnSql, msServer, msSqlDbName, mColSqlDBInfo, gsErrorLogPath)

        '=Private mbIsEvaluating As Boolean = False
        mbIsPosLicenceOK = False

        If Not mClsJmxPOS31_Licence.LicenceCheckPOS(mbIsEvaluating) Then
            '==  Target is new Build 4251..  '==   06-June-2020..
            '-- function failed..
            '--  FIRST check if upgrade was refused..
            If mClsJmxPOS31_Licence.UpgradeWasRefused Then
                mbStartupAborted = True
                Me.Close()
                Exit Sub '=End=
            End If
            '-- Licence check failed.
            '= Check licence no.s allowed now.
            '=3519.0404= 3 for Precise= ONLY !
            If mbIsPrecisePCs() Then
                mIntMaxUsersPermitted = 3
            Else '-everyone else..
                mIntMaxUsersPermitted = 1
            End If
            '=3519.0227- Now 3 users-
            MsgBox("No POS Licence.. " & vbCrLf &
                      " -- Use is restricted to max " & mIntMaxUsersPermitted & " user(s) at a time..", MsgBoxStyle.Information)
        Else
            '=3519.0110=
            '=3519.0110=
            mbIsPosLicenceOK = True
            If mbIsEvaluating Then
                mIntMaxUsersPermitted = -1  '-unlimited while evaluation..
                MsgBox("Site is still evaluating POS Licence..", MsgBoxStyle.Information)
            Else  '- ok.
                mIntMaxUsersPermitted = mClsJmxPOS31_Licence.GetMaxPosUsers
            End If  '-eval-
        End If
        '- check users..
        If Not mbCheckLoggedInUsers() Then
            mbStartupAborted = True
            Me.Close()
            Exit Sub '=End=
        End If

        '-- Startup- Check we have Till assigned..
        '= Dim strOurTillId As String = ""
        If Not mClsJmxPOS31_Licence.StartupTillCheck(mStrOurTillId) Then
            'txtSaleCustBarcode.Text = "NO TILL!"
            'txtSaleCustBarcode.ReadOnly = True
            'txtSaleStaffBarcode.ReadOnly = True
            mbStartupAborted = True
            MessageBox.Show("No Till Assigned !!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
            Exit Sub
        Else  '--ok-
            '= tsLabSaleTillId.Text = "- Till-" & mStrOurTillId & " -"
            btnMainTill.Text = "- Till-" & mStrOurTillId & " -"
            '== MsgBox("Your PC is currently assigned to Till-" & mStrOurTillId, MsgBoxStyle.Information)
        End If  '-till check-
        Me.KeyPreview = True

        If (msJobmatixAppName = "") Then
            MessageBox.Show("No JobMatix Application name supplied !!", "", _
                               MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            mbStartupAborted = True
            Me.Close()
            Exit Sub
        End If

        '-- Startup- Check we have latest version of DLL..

        '==  Target is new Build 4234..
        '==
        '==    Add Startup code to check running code is not earlier than Latest Version Build set in SystemInfo.
        '==             -- Also, update Build in SystemInfo if running code is later..

        '-Test-
        '= MsgBox("Currently running Build No is: " & mIntCurrentBuildNo & "..", MsgBoxStyle.Information)
        '==
        Dim intDB_latest_build As Integer = 0
        Dim asChanges(1) As String
        Dim sLatestKey As String = "POS_LATEST_BUILD_NUMBER"

        If mSysInfo1.contains(sLatestKey) Then
            s1 = Trim(mSysInfo1.item(sLatestKey))
            If IsNumeric(s1) Then
                intDB_latest_build = CInt(s1)
                If (mIntCurrentBuildNo < intDB_latest_build) Then
                    MsgBox("Please Note: " & _
                            "This version of JobMatix POS is not the latest for this Database.." & _
                            "Some features may not work- " & _
                             " You need to run at least Build No: " & intDB_latest_build, MsgBoxStyle.Exclamation)
                    MsgBox("And..  Your database may be affected.", MsgBoxStyle.Information)
                End If
            End If
        End If  '-contains..-

        '-- Update latest build..
        If (mIntCurrentBuildNo > intDB_latest_build) Then
            '-- Running a new version..
            '-- updated build no..
            Try
                asChanges(0) = sLatestKey
                asChanges(1) = Trim(CStr(mIntCurrentBuildNo))
                If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                    MsgBox("Couldn't save " & sLatestKey & " setting.", MsgBoxStyle.Exclamation)
                Else
                    MsgBox(" ok.. " & _
                            " The POS_LATEST_BUILD_NUMBER has been updated in SystemInfo..", MsgBoxStyle.Information)
                End If
            Catch ex As Exception
                MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If  '-mIntCurrentBuildNo -
        '==  Done with Build version.-

        msMainStaffBarcode = ""  '- force new admin signon.

        '-start first Sale Child.
        Call mnuFileNew_Click(sender, e)

    End Sub  '-shown-
    '= = = = = = = = = == = = = 
    '-===FF->

    '-- Mdi Main form resized..--
    '--  form resized..--
    '-- DELEGATE for Resizing Child..
    Public Delegate Sub SubFormResized(ByVal intParentWidth As Integer, _
                                        ByVal intParentHeight As Integer)
    '-- This is instantiated below.
    Public delResized As SubFormResized '--    = AddressOf frmPosMainMdi.subChildReport

    Private Sub frmJobMatixmain_Resize(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
        If Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized Then
            '--  cant make smaller than original..-
            If (Me.Height < mlFormDesignHeight) Then Me.Height = mlFormDesignHeight
            If (Me.Width < mlFormDesignWidth) Then Me.Width = mlFormDesignWidth

            shapedPanelAdmin.Width = Me.Width - shapedPanelSignOn.Width - 6
            DoEvents()
            TabControlMain.Width = Me.Width - 20  '= 13
            TabControlMain.Height = Me.Height - shapedPanelSignOn.Height - 50  '= 35

            DoEvents()
            '-try and resize the sales and admin boxes..
            '= MsgBox("WE have " & Me.MdiChildren.Length & " children..")
            '-- loop through tab pages.

            '= Dim frmThis As Form
            Dim ucThis As UserControl
            Dim sName, sFormType As String
            '= Note- This resize can be called when form is created. (before Load event.)
            If (mColCurrentChildForms IsNot Nothing) Then
                For Each TabPage1 As TabPage In TabControlMain.TabPages '= frmChild0 As Form In Me.MdiChildren
                    sName = TabPage1.Name
                    If mColCurrentChildForms.Contains(sName) Then
                        ucThis = mColCurrentChildForms(sName)
                        '--each form type must be treated separately.
                        sFormType = TabPage1.Tag
                        Select Case LCase(sFormType)
                            Case "ucpossalechild"
                                Dim frmChild1 As ucPosSaleChild
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "ucchildstockadmin"
                                Dim frmChild1 As ucChildStockAdmin
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "ucchildseriallookup"
                                Dim frmChild1 As ucChildSerialLookup
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "ucchildstocktake"
                                Dim frmChild1 As ucChildStocktake
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "ucchildgoodsrecvd"
                                Dim frmChild1 As ucChildGoodsRecvd
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "ucchildpayments"
                                Dim frmChild1 As ucChildPayments
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "ucchildcustomer"
                                Dim frmChild1 As ucChildCustomer
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "ucchildstatements"
                                Dim frmChild1 As ucChildStatements
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "ucchildlaybys"
                                Dim frmChild1 As ucChildLaybys
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "uctranslookup"
                                Dim frmChild1 As ucTransLookup
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)

                                '==   Target-New-Build-4284 --  (24-Nov-2020) 
                                '==  A.  New Child USERCONTROL to move Suppliers Admin into Main Tab Control.
                            Case "ucchildsupplier"
                                Dim frmChild1 As ucChildSupplier
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                            Case "ucchildstaff"
                                Dim frmChild1 As ucChildStaff
                                frmChild1 = ucThis
                                delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                                Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                                '== END  Target-New-Build-4284 --  (24-Nov-2020)

                        End Select  '-type-
                        delResized = Nothing
                    End If  '-contains-
                    DoEvents()
                Next TabPage1 '= frmChild..
            End If '-nothing.-
        End If  '-window state.-
    End Sub  '--resized..
    '= = = = = = = = = = == = = = = =  =

    'Private Sub frmJobMatixmain_ResizeEnd(ByVal eventSender As System.Object, _
    '                                   ByVal eventArgs As System.EventArgs) Handles MyBase.ResizeEnd

    'End Sub  '=  ResizEnd.
    '= = = = = = = = = = = =
    '-===FF->

    '-- TabControlMain_DrawItem--
    '--  SEE the subClassed TabControl clsJmxTabControl --
    '--  SEE the subClassed TabControl clsJmxTabControl --

    '= I found this Sub somewhere and changed it slightly. Add this sub to your program. 
    '= It colors the Tab (not the page) the same colors as the background

    '= of the tab, if it is the one selected. The other tabs are colored a standard white with black text. 
    '= You can modify it anyway you want, but it seems to work very well for coloring the tabs.
    '= https://social.msdn.microsoft.com/Forums/en-US/77f5b624-45af-492b-aef0-55e9f6176128/how-to-change-the-color-of-the-tab-control-in-vbnet?forum=vbide&forum=vbide

    '--  SEE the subClassed TabControl clsJmxTabControl --

    'Private Sub TabControlMain_DrawItem(ByVal sender As Object, _
    '                                    ByVal ev As System.Windows.Forms.DrawItemEventArgs) Handles TabControlMain.DrawItem

    '    Dim TabControl1 As TabControl = CType(sender, TabControl)
    '    Dim g As Graphics = ev.Graphics
    '    Dim tp As TabPage = TabControl1.TabPages(ev.Index)
    '    Dim br As Brush
    '    Dim sf As New StringFormat
    '    Dim r As New RectangleF(ev.Bounds.X, ev.Bounds.Y + 2, ev.Bounds.Width, ev.Bounds.Height - 2)
    '    sf.Alignment = StringAlignment.Center
    '    Dim strTitle As String = tp.Text

    '    '-  If the current index is the Selected Index, change the color
    '    If TabControl1.SelectedIndex = ev.Index Then
    '        '- this is the background color of the tabpage
    '        '- you could make this a stndard color for the selected page
    '        '= br = New SolidBrush(tp.BackColor)
    '        br = New SolidBrush(ColorTranslator.FromOle(RGB(255, 255, 170)))  '-light yellow.
    '        g.FillRectangle(br, ev.Bounds)
    '        br = New SolidBrush(tp.ForeColor)
    '        g.DrawString(strTitle, TabControl1.Font, br, r, sf)
    '    Else
    '        '= these are the standard colors for the unselected tab pages
    '        br = New SolidBrush(Color.WhiteSmoke)
    '        g.FillRectangle(br, ev.Bounds)
    '        br = New SolidBrush(Color.Black)
    '        g.DrawString(strTitle, TabControl1.Font, br, r, sf)

    '    End If '-selected-
    'End Sub  '-draw-item
    '= = = = = = = = = = == 
    '-===FF->

    '-- New Child-

    Private Sub mnuFileNew_Click(sender As Object, e As EventArgs)

        Dim ucNewChild1 As ucPosSaleChild = New ucPosSaleChild()
        Dim tabPageChild1 As TabPage = New TabPage

        ucNewChild1.TillId = mStrOurTillId
        ucNewChild1.connectionSql = mCnnSql
        ucNewChild1.SqlServer = msServer
        ucNewChild1.DBname = msSqlDbName
        ucNewChild1.dbInfoSql = mColSqlDBInfo
        ucNewChild1.StaffId = mIntMainStaff_id
        ucNewChild1.StaffName = msMainStaffName
        '-msStaffBarcode-
        ucNewChild1.Staffbarcode = msMainStaffBarcode  '-03-Jan-2018-
        ucNewChild1.mainVersionPOS = msDllversion
        ucNewChild1.UserLogo = mImageUserLogo
        '=4201.0708-
        ucNewChild1.SettingsPath = msSettingsPath

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler ucNewChild1.PosChildClosing, AddressOf posChild_Closing


        Call mbAddNewChild2(ucNewChild1, "ucPosSaleChild", "Sales-Trans.", tabPageChild1)

        If mIntChildCount = 1 Then
            ucNewChild1.btnMainExit.Enabled = False   '--can't exit first Child.
        End If
        ucNewChild1.delReport = AddressOf Me.subChildReport
        ucNewChild1.delChildSignedOn = AddressOf Me.subChildSignedOn

        ucNewChild1.Show()
        '= frmNewChild1.Select()
        '- now position.
        ucNewChild1.Left = 0
        ucNewChild1.Top = 0
        ucNewChild1.Width = tabPageChild1.Width
        ucNewChild1.Height = tabPageChild1.Height
        '-- make sure form controls are resize to current form size..
        '--  In case main form was enlarged.
        delResized = New SubFormResized(AddressOf ucNewChild1.SubFormResized)
        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
        delResized = Nothing

        '= frmNewChild1.Show()

    End Sub '-mnuFileNew click-
    '= = = = = = = = == = = = = 
    '-===FF->

    '-- DB Info..
    '=-- 3519.0110--

    Private Sub mnuDBInfo_Click(sender As Object, e As EventArgs)

        '=   mnuDBInfo.Text = "DB: " & msSqlDbName & " on: " & msServer
        Dim sMsg As String
        Dim strUserList As String
        Dim col1 As Collection

        sMsg = "Sql Server Version is: " & msSqlVersion & vbCrLf & "On: " & msServer & vbCrLf
        sMsg &= " (Database: " & msSqlDbName & ")."
        sMsg &= vbCrLf & vbCrLf & "Login: " & msSqlServerComputer & "\" & msCurrentUserName & vbCrLf & vbCrLf

        Call mbShowLoggedInUsers(col1, strUserList)
        sMsg &= strUserList & vbCrLf & vbCrLf & vbCrLf & _
               "Current process is: " & msCurrentProcessName & vbCrLf
        MsgBox(sMsg, MsgBoxStyle.Information, "JobMatixPOS Sql Server Info")

    End Sub  '-dbinfo-
    '= = = = = = = = == = = = = 

    '--btnDbInfo_Click-
    Private Sub btnDbInfo_Click(sender As Object, ev As EventArgs)
        Call mnuDBInfo_Click(sender, ev)
    End Sub  '--btnDbInfo_Click-
    '= = = = = =  = = = = = = = == 

    '-tsFileMenuItemNew_Click-
    Private Sub tsFileMenuItemNew_Click(sender As Object, ev As EventArgs) Handles tsFileMenuItemNew.Click

        Call mnuFileNew_Click(sender, ev)
    End Sub  '-tsFileMenuItemNew_Click-
    '= = = = = =  = = = = = ===== =

    Private Sub tsFileMenuItemDbInfo_Click(sender As Object, ev As EventArgs) Handles tsFileMenuItemDbInfo.Click
        Call mnuDBInfo_Click(sender, ev)

    End Sub  '-dbinfo-
    '= = = = = = = = === 

    Private Sub tsFileMenuItemExit_Click(sender As Object, ev As EventArgs) Handles tsFileMenuItemExit.Click
        Call mnuFileExit_Click(sender, ev)

    End Sub  '-exit-
    '= = = = = = = = == =
    '-===FF->

    '-- Till Drop down Functions..
    '-- Shows Last Transction for this Till..

    Private Sub tsMenuItemLastTrans_Click(sender As Object, _
                                          ev As EventArgs)
        '=MsgBox("Shows Last Transction for this Till", MsgBoxStyle.Exclamation)

        Dim sSql, sTranType As String
        Dim intInvoice_id As Integer
        Dim dataTable1 As DataTable
        Dim frmShowInvoice1 As frmShowInvoice

        If (mIntMainStaff_id <= 0) Then
            MsgBox("Please do Staff SignOn on the Admin Tab. ", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        sSql = "SELECT TOP (1) invoice_id,transactionType FROM dbo.Invoice "
        sSql &= " WHERE (transactionType IN ('sale','refund')) AND "
        sSql &= "  (cashDrawer= '" & mStrOurTillId & "')"
        sSql &= " ORDER BY invoice_id DESC;"

        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            MsgBox("Error in getting recordset for Invoice/SalesOrder/Layby table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        Else
            If Not (dataTable1 Is Nothing) AndAlso dataTable1.Rows.Count > 0 Then
                Dim row1 As DataRow = dataTable1.Rows(0)
                intInvoice_id = row1.Item("invoice_id")
                sTranType = row1.Item("transactionType")
                '= Call mbShowInvoice(intInvoice_id, sTranType)
                frmShowInvoice1 = New frmShowInvoice
                frmShowInvoice1.connectionSql = mCnnSql
                frmShowInvoice1.InvoiceNo = intInvoice_id
                frmShowInvoice1.isQuote = False
                frmShowInvoice1.islayby = False
                '-- can use main signon- mIntMainStaff_id -
                frmShowInvoice1.Staff_id = mIntMainStaff_id
                frmShowInvoice1.ShowDialog()
            Else
                MsgBox("No Transaction to show..", MsgBoxStyle.Exclamation)
            End If  '-nothing-
        End If  '-get-

    End Sub  '--Last Trans.
    '= = = = = = = = = = = 
    '-===FF->

    '== Change Till.

    Private Sub tsMenuItemChangeTill_Click(sender As Object, _
                                           ev As EventArgs)
        '= MsgBox("Change Till for this Computer.", MsgBoxStyle.Exclamation)
        Dim sTillId As String
        If Not gbChangeCashDrawer(mCnnSql, msComputerName, sTillId) Then
            MsgBox("NO TILL!")
        Else  '--ok-
            mStrOurTillId = gsGetCurrentCashDrawer()
            '= tsLabSaleTillId.Text = "- Till-" & mStrOurTillId & " -"
            btnMainTill.Text = "- Till-" & mStrOurTillId & " -"
            MsgBox("You are now on Till-" & gsGetCurrentCashDrawer(), MsgBoxStyle.Information)
        End If  '-get-
        '-done-
    End Sub '-Change Till.
    '= = = = = = = = = = = = = =

    '-- Cashup and Till Balance -

    Private Sub tsMenuItemCashup_Click(sender As Object, ev As EventArgs)
        '== MsgBox("End of Day- Cashup and Till Balance.", MsgBoxStyle.Exclamation)

        If (msMainStaffBarcode = "") Then '= (mIntMainStaff_id <= 0) Then
            MsgBox("Please do Admin Staff SignOn..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim frmCashup1 As frmCashup
        frmCashup1 = New frmCashup(msComputerName, Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                        msDllversion, mImageUserLogo, msSettingsPath, mIntMainStaff_id, msMainStaffName)
        frmCashup1.ShowDialog()
        '--done=

    End Sub  '--Cashup..
    '= = = = = = = = = = = == 
    '-===FF->

    '-About-

    'Private Sub mnuAboutJobMatix35_Click(sender As Object, e As EventArgs)
    '    Call mbDoNotShowMsgDialogue("", "", strAboutJobMatix3HTML, True, True, False, "About " & msJobMatixVersion)

    'End Sub  '-About-
    '= = = = = = = = = = = = 

    Private Sub tsFileMenuItemAbout_Click(sender As Object, e As EventArgs) Handles tsFileMenuItemAbout.Click

        Call mbDoNotShowMsgDialogue("", "", strAboutJobMatix3HTML, True, True, False, "About " & msJobMatixVersion)
    End Sub  '=about-
    '= = = = = = = = = = = =

    Private Sub picJmxLogo_Click(sender As Object, e As EventArgs) Handles picJmxLogo.Click

        Call mbDoNotShowMsgDialogue("", "", strAboutJobMatix3HTML, True, True, False, "About " & msJobMatixVersion)
    End Sub  '-Logo-
    '= = = = = = = === = 

    Private Sub tsBtnNewSale_Click(sender As Object, e As EventArgs)

        Call mnuFileNew_Click(sender, e)
    End Sub  '-btnNew-
    '= = = = = = = = ==  =

    '- btnNewSale_Click-
    Private Sub btnNewSale_Click(sender As Object, e As EventArgs)

        Call mnuFileNew_Click(sender, e)
    End Sub  '- btnNewSale_Click-
    '= = = = = = = = = = = = = = 

    Private Sub btnDropdownNewSale_Click(sender As Object, e As EventArgs) Handles btnDropdownNewSale.Click
        Call mnuFileNew_Click(sender, e)

    End Sub  '-btnDropdownNewSale_Click-
    '= = = = = =
    '-- catch F6..

    '= 3411.0313=
    '-- catch ESCAPE for Sale form Cancel function..

    '- PreviewKeyDown is where you preview the key.
    '- Do not put any logic here, instead use the
    '- KeyDown event after setting IsInputKey to true.
    Private Sub frmPOS35Main_PreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs) _
                                            Handles Me.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Escape '= Keys.Down, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub  '-Me.PreviewKeyDown-
    '= = = = = = = = =  = = = = = =
    '-===FF->

    '-- FORM- Key Down..-
    '-- catch F6 and ESCape...- ALSO Catch k/b Shortcuts..

    Private Sub frmPOS35Main_KeyDown(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                               Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        If (KeyCode = System.Windows.Forms.Keys.F6) And _
                        ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--New Sale--
            '-- If Sale Tab is open, 
            '- let it go if we're clicking on different page.
            'If LCase(TabControlMain.SelectedTab.Name) = "tabpagesales" Then
            Call mnuFileNew_Click(eventSender, eventArgs)
            eventArgs.Handled = True
            'End If '-sales page-
        ElseIf (KeyCode = System.Windows.Forms.Keys.F6) And _
                    ((eventArgs.Shift) And (Not eventArgs.Control)) Then '- SHIFT F6--New Payments--
            If (msMainStaffBarcode = "") Then
                MsgBox("Staff must sign-on first..", MsgBoxStyle.Exclamation)
            Else
                Call mbNewPaymentsChild()
            End If
            eventArgs.Handled = True
        ElseIf (KeyCode = System.Windows.Forms.Keys.Escape) And _
                    ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--ESCAPE: cancel--
            '-- If Sale Tab is open, and Sale instance enabled..
            '-- ESCape is used to Cancel the Sale..
            'If (LCase(TabControlPOS.SelectedTab.Name) = "tabpagesale") Then
            '    If mClsSale1.HasCurrentSale AndAlso _
            '                   btnCancelSale.Visible AndAlso btnCancelSale.Enabled Then
            '        Call btnCancel_Click(btnCancelSale, New System.EventArgs)
            '        eventArgs.Handled = True
            '    End If
            'End If  '-sale page-
        ElseIf ((KeyCode = System.Windows.Forms.Keys.Z) And (eventArgs.Control)) Then '--Ctl-Z- Open Cash Drawer.-
            '-- Ctl-Z- Open Cash Drawer...
            '=3411.0402=
            'If mClsSale1 IsNot Nothing Then
            '    '-- Call function in Sale class.
            '    Call mClsSale1.OpenCashDrawer()
            'End If '-nothing.

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
            If mSysInfo1.exists(sTillPrinterNameinfoKey) AndAlso _
                        (Trim(mSysInfo1.item(sTillPrinterNameinfoKey)) <> "") Then
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
            eventArgs.Handled = True
        ElseIf (eventArgs.Control) And (Not (eventArgs.Shift)) And ((KeyCode = System.Windows.Forms.Keys.O) Or _
                                        (KeyCode = System.Windows.Forms.Keys.U) Or _
                                           (KeyCode = System.Windows.Forms.Keys.P) Or _
                                           (KeyCode = System.Windows.Forms.Keys.L) Or _
                                           (KeyCode = System.Windows.Forms.Keys.F)) Then
            '--process shortcuts.
            If (msMainStaffBarcode = "") Then
                MsgBox("Staff must sign-on first..", MsgBoxStyle.Exclamation)
                eventArgs.Handled = True
                Exit Sub
            Else  '-ok-
                If (KeyCode = System.Windows.Forms.Keys.O) Then '--Ctl-O- Stock.-
                    '--call up stock form..-
                    '= Call mSub_AdminFunctionButtons_click(btnStock, New System.EventArgs)  '--call up stock form..-
                    Call mbNewStockChild()
                ElseIf (KeyCode = System.Windows.Forms.Keys.U) Then '--Ctl-U- Customers.-
                    '--   call up customer form..-
                    Call mbNewCustomerChild()
                    '= Call mSub_AdminFunctionButtons_click(btnCustomers, New System.EventArgs)  '--   call up customer form..-
                ElseIf (KeyCode = System.Windows.Forms.Keys.L) Then '--Ctl-L- Lookup Trans..-
                    Call mbNewTransLookupChild()
                ElseIf (KeyCode = System.Windows.Forms.Keys.P) Then '--Ctl-P- Suppliers.-
                    '==Call mSub_AdminFunctionButtons_click(btnSuppliers, New System.EventArgs)  '--   call up Suppliers form..-
                    Call mnuPurchases_click(mnuMainGoodsSuppliers, New System.EventArgs)  '--   call up Suppliers form..-
                ElseIf (KeyCode = System.Windows.Forms.Keys.F) Then '--Ctl-F- Staff.-
                    '= Call mSub_AdminFunctionButtons_click(btnStaff, New System.EventArgs)  '--   call up Staff  form..-
                    Dim colKeys As Collection
                    Dim colSelectedRow As Collection
                    If Not mbBrowseTable(mColPrefsStaff, "Lookup Staff", "", colKeys, colSelectedRow, "Staff") Then
                        MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                    End If
                End If
                eventArgs.Handled = True
            End If  '-staff-
        ElseIf (eventArgs.Control) And ((KeyCode = System.Windows.Forms.Keys.T)) Then
            '-- Switch main TAB..
            txtAdminStaffBarcode.Select()
            'If LCase(TabControlMain.SelectedTab.Name) = "tabpagesales" Then
            '    '-switch to Admin.
            '    TabControlMain.SelectedTab = TabControlMain.TabPages("TabPageAdmin")
            '    txtAdminStaffBarcode.Text = msMainStaffBarcode
            '    txtAdminStaffBarcode.Select()
            'Else '-go to sales-
            '    TabControlMain.SelectedTab = TabControlMain.TabPages("TabPageSales")
            'End If  '-Main tab.
        ElseIf (eventArgs.Control) And (eventArgs.Shift) And ((KeyCode = System.Windows.Forms.Keys.U)) Then
            '- SHIFT-Control-U --
            '--  Test frmCustomer as it's called from JobTracking..
            Dim frmCust1 As New frmCustomer
            frmCust1.StaffId = mIntMainStaff_id
            frmCust1.StaffName = msMainStaffName
            frmCust1.SqlServer = msServer
            frmCust1.connectionSql = mCnnSql '--job tracking sql connection..-
            frmCust1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..

            frmCust1.DBname = msSqlDbName
            frmCust1.VersionPOS = msDllversion
            '= frmCust1.form_left = mFrmSale.Left '=  Me.Left
            '= frmCust1.form_top = mFrmSale.Top = 30 '=  Me.Top + 30
            frmCust1.ShowDialog()

            frmCust1.Dispose()

        End If  '--F6/ESCAPE-
    End Sub '--keyDown..-
    '= = = = = = = = = = == =
    '-===FF->

    '--Tab page selected=

    '==   Updated.- 4201.0416- 
    '==     --for TDI- Tabbed Document Interface.. 

    Private Sub TabControlSales_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                       Handles TabControlMain.SelectedIndexChanged
        Dim ucThis As UserControl '= frmPosSaleChild
        Dim sName As String
        '-- select form..

        If Not TabControlMain.SelectedTab Is Nothing Then
            Dim tabPage1 As TabPage = TabControlMain.SelectedTab
            Dim sFormType As String = tabPage1.Tag
            sName = tabPage1.Name
            '== MsgBox("Selected: " & sName)
            If mColCurrentChildForms.Contains(sName) Then
                ucThis = mColCurrentChildForms(sName)
                '--each form type must be treated separately.
                Select Case LCase(sFormType)
                    Case "ucpossalechild"
                        Dim frmChild1 As ucPosSaleChild
                        frmChild1 = ucThis
                        frmChild1.Select()   '--  //activate MDI child
                        '= resize if needed.
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
                        delResized = Nothing
                    Case "ucchildstockadmin"
                        Dim frmChild1 As ucChildStockAdmin
                        frmChild1 = ucThis
                        frmChild1.Select()   '--  //activate MDI child
                        '= resize if needed.
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
                    Case "ucchildseriallookup"
                        Dim frmChild1 As ucChildSerialLookup
                        frmChild1 = ucThis
                        frmChild1.Select()   '--  //activate MDI child
                        '= resize if needed.
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
                    Case "ucchildstocktake"
                        Dim frmChild1 As ucChildStocktake
                        frmChild1 = ucThis
                        frmChild1.Select()   '--  //activate MDI child
                        '= resize if needed.
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
                    Case "ucchildgoodsrecvd"
                        Dim frmChild1 As ucChildGoodsRecvd
                        frmChild1 = ucThis
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                    Case "ucchildpayments"
                        Dim frmChild1 As ucChildPayments
                        frmChild1 = ucThis
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                    Case "ucchildstatements"
                        Dim frmChild1 As ucChildStatements
                        frmChild1 = ucThis
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                    Case "ucchildlaybys"
                        Dim frmChild1 As ucChildLaybys
                        frmChild1 = ucThis
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                    Case "uctranslookup"
                        Dim frmChild1 As ucTransLookup
                        frmChild1 = ucThis
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                    Case "ucChildPosReports"
                        Dim frmChild1 As ucChildPosReports
                        frmChild1 = ucThis
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)

                        '== Target-Build-4284-EXTRA=
                        '--  Added Customer, Supplier and RAs.
                    Case "ucchildcustomer"
                        Dim frmChild1 As ucChildCustomer
                        frmChild1 = ucThis
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                    Case "ucchildsupplier"
                        Dim frmChild1 As ucChildSupplier
                        frmChild1 = ucThis
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                    Case "ucchildstaff"
                        Dim frmChild1 As ucChildStaff
                        frmChild1 = ucThis
                        delResized = New SubFormResized(AddressOf frmChild1.SubFormResized)
                        Call delResized(TabControlMain.Width - 11, TabControlMain.Height - 7)
                        '== END Target-Build-4284-EXTRA=

                    Case Else
                End Select
            End If  '-contains-
        End If  '-nothing-
    End Sub  '-TabControlSales_SelectedIndexChanged=
    '= = = = = = = = = = = = = = = = = == =  = 
    '-===FF->

    '-- Mdi Main form Tab close clicked..--
    '--  form to close..--

    '==SUB to close the child..
    Private Function mbCloseSelectedChild(ByRef tabPage1 As TabPage, _
                                            ByVal sName As String) As Boolean
        Dim bCanCloseOk As Boolean = False
        '- ok.  ask child if we can close it.
        mbCloseSelectedChild = False
        If Not delCloseRequest Is Nothing Then
            bCanCloseOk = delCloseRequest.Invoke
            '= MsgBox("bCanCloseOk is: " & bCanCloseOk, MsgBoxStyle.Information)
            If bCanCloseOk Then
                '-close this child.
                '= MsgBox("Closing: " & strChildName, MsgBoxStyle.Information)  '-test-
                tabPage1.Dispose()
                If mColCurrentChildForms.Contains(sName) Then
                    mColCurrentChildForms.Remove(sName)
                    '-remove the close event handler..
                    '-- Add Handler for ClosingEvent for this Child INSTANCE..
                    '= AddHandler ucChildLaybys1.PosChildClosing, AddressOf posChild_Closing
                End If
                mbCloseSelectedChild = True
                DoEvents()
            End If  '-can close-
            delCloseRequest = Nothing
        End If '-delCloseRequest-
    End Function  '-close-
    '= = = == =  = = == 

    '-- DELEGATE for CLOSING Child..
    '= Public Delegate Sub SubFormCloseRequest()
    Public Delegate Function SubFormCloseRequest() As Boolean

    '-- This is instantiated below.
    Public delCloseRequest As SubFormCloseRequest '--    = AddressOf frmPosMainMdi.subChildReport

    '-- Mouse on "X" on child Tab..

    '- When the user clicks on a tab, 
    '-      the program determines whether the mouse is over any tab's X. 
    '- If the mouse is over an X, the program removes the corresponding tab.

    Private Sub TabControlMain_MouseDown(ByVal sender As Object, _
                                             ByVal ev As System.Windows.Forms.MouseEventArgs) _
                                            Handles TabControlMain.MouseDown
        Dim ucThis As UserControl   '= frmPosSaleChild
        Dim sName As String
        '- See if this is over a tab.
        For intTabIndex As Integer = 0 To (TabControlMain.TabPages.Count - 1)
            If TabControlMain.IsMouseOverCloseX(intTabIndex, ev) Then
                '= MsgBox("Tab " & intTabIndex & " was clicked..", MsgBoxStyle.Information)
                Dim tabPage1 As TabPage = TabControlMain.TabPages(intTabIndex)
                Dim bCanCloseOk As Boolean = False
                '-  get form in this tab and tell Child to Close..

                Dim sFormType As String = tabPage1.Tag
                sName = tabPage1.Name
                '== MsgBox("Selected: " & sName)
                If mColCurrentChildForms.Contains(sName) Then
                    ucThis = mColCurrentChildForms(sName)
                    '--each form (uc-child) type must be treated separately.
                    Select Case LCase(sFormType)
                        Case "ucpossalechild"
                            Dim frmChild1 As ucPosSaleChild
                            frmChild1 = ucThis
                            '= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            'Call delCloseRequest()
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            'delCloseRequest = Nothing
                        Case "ucchildstockadmin"
                            Dim frmChild1 As ucChildStockAdmin
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            'Call delCloseRequest()
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            'delCloseRequest = Nothing
                        Case "ucchildseriallookup"
                            Dim frmChild1 As ucChildSerialLookup
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            '    Call delCloseRequest()
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            '    delCloseRequest = Nothing
                        Case "ucchildstocktake"
                            Dim frmChild1 As ucChildStocktake
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            'Call delCloseRequest()
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            'delCloseRequest = Nothing
                        Case "ucchildgoodsrecvd"
                            Dim frmChild1 As ucChildGoodsRecvd
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            'Call delCloseRequest()
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            'delCloseRequest = Nothing
                        Case "ucchildpayments"
                            Dim frmChild1 As ucChildPayments
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            'Call delCloseRequest()
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            'delCloseRequest = Nothing
                        Case "ucchildsubscription"
                            Dim frmChild1 As ucChildSubscription
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            '    Call delCloseRequest()
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            '    delCloseRequest = Nothing
                        Case "ucchildcustomer"
                            Dim frmChild1 As ucChildCustomer
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            'Call delCloseRequest()
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            'delCloseRequest = Nothing
                        Case "ucchildstatements"
                            Dim frmChild1 As ucChildStatements
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            '    Call delCloseRequest()
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            '    delCloseRequest = Nothing
                        Case "ucchildlaybys"
                            Dim frmChild1 As ucChildLaybys
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                        Case "uctranslookup"
                            Dim frmChild1 As ucTransLookup
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                        Case "ucchildnewjob"
                            Dim frmChild1 As ucChildNewJob
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                        Case "ucchildposreports"
                            Dim frmChild1 As ucChildPosReports
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                        Case "ucchildjobmaint"
                            '==   Target-New-Build 4262 --
                            Dim frmChild1 As ucChildJobMaint
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If

                                                            '==   Target-New-Build-4284 --  (08-Nov-2020)
                                '==  A.  New Child USERCONTROL to move Suppliers Admin into Main Tab Control.
                        Case "ucchildsupplier"
                            Dim frmChild1 As ucChildSupplier
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            '== END  Target-New-Build-4284 --  (30-October-2020)

                                '== Target-New-Build-4284-EXTRA --  (12-Nov-2020) 
                                '==   INCLUDING. New Child USERCONTROL to bring RAs Main into Main Tab Control.
                        Case "ucchildstaff"
                            Dim frmChild1 As ucChildStaff
                            frmChild1 = ucThis
                            ''= send close cmd..
                            delCloseRequest = New SubFormCloseRequest(AddressOf frmChild1.SubFormCloseRequest)
                            If mbCloseSelectedChild(tabPage1, sName) Then
                                '-remove close event handler.
                                RemoveHandler frmChild1.PosChildClosing, AddressOf posChild_Closing
                            End If
                            '== END Target-New-Build-4284-EXTRA --  (12-Nov-2020) 


                        Case Else
                    End Select
                    delCloseRequest = Nothing
                    ''- ok.  ask child if we can close it.
                    'If Not delCloseRequest Is Nothing Then
                    '    bCanCloseOk = delCloseRequest.Invoke
                    '    '= MsgBox("bCanCloseOk is: " & bCanCloseOk, MsgBoxStyle.Information)
                    '    If bCanCloseOk Then
                    '        '-close this child.
                    '        '= MsgBox("Closing: " & strChildName, MsgBoxStyle.Information)  '-test-
                    '        tabPage1.Dispose()
                    '        If mColCurrentChildForms.Contains(sName) Then
                    '            mColCurrentChildForms.Remove(sName)
                    '            '-remove the close event handler..
                    '            '-- Add Handler for ClosingEvent for this Child INSTANCE..
                    '            '= AddHandler ucChildLaybys1.PosChildClosing, AddressOf posChild_Closing

                    '        End If
                    '        DoEvents()
                    '    End If  '-can close-
                    '    delCloseRequest = Nothing
                    'End If '-delCloseRequest-
                End If  '-contains-
                '-test- show current forms.
                Dim sList As String = ""
                For Each ucChild As UserControl In mColCurrentChildForms
                    sList &= ucChild.Name & ";  "
                Next
                '= MsgBox("Current Child UC's are:" & vbCrLf & sList, MsgBoxStyle.Information)  '-TEST-
                Exit For
            End If  '- click on X..
        Next intTabIndex
    End Sub '- TabControlMain_MouseDown=
    '= = = = = = = = = = = = = = = = = == =  = 
    '-===FF->

    '-- child closed itself..  ??

    Private Sub form_mdiChildActivate(sender As Object, ev As EventArgs) Handles Me.MdiChildActivate
        Dim frmChildSender As Form = CType(sender, Form)

        'If Not TabControlSales.SelectedTab Is Nothing Then
        '    For Each frmChild1 As Form In mColCurrentChildForms '=Me.MdiChildren
        '        '= If Not TabControlSales.SelectedTab.Equals(vbNull) Then
        '        '-ok-
        '        If (frmChild1.Name = TabControlSales.SelectedTab.Name) Then
        '            '=frmChild1.Close()   '--  //activate MDI child
        '            If (frmChild1 Is Nothing) OrElse (Not frmChild1.Visible) Then
        '                TabControlSales.SelectedTab.Dispose()
        '                Exit For
        '            End If
        '        End If
        '        '= End If '-null-
        '    Next frmChild1

        'End If  '-nothing-
        '-- //this is why you must make sure menu's and tab page's name are same
        '= Me.ActiveMdiChild.Close()
        '-- //because of synchronize routine, you must close the form first before tab.

    End Sub  '-mdiChildActivate-
    '= = = = = = = = = = = == = = = 
    '-===FF->

    '-- Admin Staff Sign-on-

    '--  Enter was Pressed --

    Public Sub txtAdminStaffBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtAdminStaffBarcode.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim sSql As String
        Dim colResult, colRecord As Collection
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent

        If keyAscii = 13 Then '--enter-
            s1 = Trim(txtAdminStaffBarcode.Text)
            If (s1 <> "") Then  '--have barcode-
                '--lookup barcode-
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [staff] WHERE (barcode='" & s1 & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    '== Call mbSetupSaleCustomer(colRecord)
                    msMainStaffBarcode = s1
                    mIntMainStaff_id = colRecord.Item("staff_id")("value")
                    msMainStaffName = colRecord.Item("docket_name")("value")
                    labAdminStaffName.Text = msMainStaffName
                    '= grpBoxAdmin.Enabled = True
                    shapedPanelAdmin.Enabled = True
                    '= tsStock.Select()
                    btnDropdownStock.Select()
                Else '--not found..-
                    MsgBox("No Staff Record found for barcode: " & s1, MsgBoxStyle.Exclamation)
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

    Public Sub txtAdminStaffBarcode_Validating(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As CancelEventArgs) _
                                       Handles txtAdminStaffBarcode.Validating

        Dim sBarcode As String
        Dim sSql As String
        Dim colResult, colRecord As Collection

        '= Call mClsSale1.txtSaleCustBarcode_Validating(eventSender, eventArgs)
        If (Trim(txtAdminStaffBarcode.Text) = "") Then
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
            '= eventArgs.Cancel = True
            '= MsgBox("Must have Staff barcode: " & sBarcode, MsgBoxStyle.Exclamation)
        Else
            '- validate/lookup if not done yet..
            If (msMainStaffBarcode = "") OrElse _
                   (msMainStaffBarcode <> Trim(txtAdminStaffBarcode.Text)) Then  '-cust not set up--
                '--lookup -
                sBarcode = Trim(txtAdminStaffBarcode.Text)
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [staff] WHERE (barcode='" & sBarcode & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    msMainStaffBarcode = sBarcode
                    mIntMainStaff_id = colRecord.Item("staff_id")("value")
                    msMainStaffName = colRecord.Item("docket_name")("value")
                    labAdminStaffName.Text = msMainStaffName
                Else '--not found..-
                    eventArgs.Cancel = True
                    MsgBox("No Staff found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
                End If  '-get--
            End If  '-set up-
        End If  '-text-
    End Sub  '-- Staff Validating-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- Launch JobTracking..
    '== 4201.0838=  Allow multiple instances..

    Private Sub tsBtnJobTracking_Click(sender As Object, e As EventArgs)

        '=4219.1216=
        '==  Launch JT, but ask first if alreday running..
        Call mbLaunchJobTracking(False)  '-NOT silent.

        'Dim intResult As Integer  '- Double
        'Dim sJT350Name, sJT350Path, sJT350Args As String
        'Dim bCanLaunch As Boolean = False

        'sJT350Name = "JmxJT420ex"
        'sJT350Path = msAppPath & sJT350Name & ".exe"
        'sJT350Args = " /server=" & msServer & _
        '               " /JT_dbname=" & msSqlDbName & " /JobMatixAppName=" & msJobmatixAppName
        ''-- check if process already running..
        'Dim ap As Process() = Process.GetProcessesByName(sJT350Name)
        'If (ap IsNot Nothing) AndAlso (ap.Length > 0) Then
        '    '-- is alive.. so switch to it..
        '    If MessageBox.Show("Note- A JobTracking instance is already open." & vbCrLf & _
        '                       "Do you want to start another one ?", "Launching JobTracking", MessageBoxButtons.YesNo, _
        '                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
        '        bCanLaunch = True
        '    Else  '-no- Switch to current.
        '        Dim intId As IntPtr = ap(0).MainWindowHandle
        '        '-found-  Make it active-
        '        intResult = SetForegroundWindow(intId)
        '    End If  '-yes..
        'Else '-not there.. so can launch..-
        '    bCanLaunch = True
        'End If  '-nothing-
        'If bCanLaunch Then
        '    Try
        '        Process.Start(sJT350Path, sJT350Args)
        '    Catch ex As Exception
        '        MsgBox("ERROR executing StartProcess cmd: " & vbCrLf & sJT350Path & vbCrLf & vbCrLf & _
        '               "Error: " & vbCrLf & ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
        '    End Try
        'End If '-can launch-
    End Sub  '-'-- Launch JobTracking..-
    '= = = = = = = = = = = = =  = = = 

    '-picJobTracking_Click-
    Private Sub picJobTracking_Click(sender As Object, ev As EventArgs) Handles picJobTracking.Click
        Call tsBtnJobTracking_Click(sender, ev)
    End Sub  '-picJobTracking_Click-
    '= = = = = = = =  = = = = = = =

    '-===FF->

    '- tsBtnRAs_Click-

    Private Sub tsBtnRAs_Click(sender As Object, e As EventArgs)
        '= MsgBox("THis will launch RAs..", MsgBoxStyle.Information)

        Dim intResult As Integer  '- Double
        Dim sRAs34Name, sRAs34Path, sRAs34Args As String

        If msMainStaffBarcode = "" Then
            MessageBox.Show("Must have admin signon.", "JobMatix POS ", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtAdminStaffBarcode.Select()
            Exit Sub
        End If

        '= sRAs34Name = "JobMatixRAs34"
        '= sRAs34Path = msAppPath & sRAs34Name & ".exe"
        '=sRAs34Name = "JobMatixRAs35"

        '=4219.1126=

        '==   Target-New-Build-6201 --  (26-June-2021)
        '= sRAs34Name = "JobMatixRAs42"
        sRAs34Name = "JobMatixRAs62"
        '==   Target-New-Build-6201 --  (26-June-2021)

        sRAs34Path = msAppPath & sRAs34Name & ".exe"
        '- try v3.5 first.
        If Dir(sRAs34Path) = "" Then
            sRAs34Name = "JobMatixRAs35"
            sRAs34Path = msAppPath & sRAs34Name & ".exe"
        End If

        '-- send  form pos also....
        sRAs34Args = " /server=" & msServer & _
         " /RAs_dbname=" & msSqlDbName & " /StaffBarcode=" & msMainStaffBarcode & _
         " /formTop=" & CStr(Me.Top + 60) & " /formleft=" & CStr(Me.Left + 30)

        '-- check if process already running..
        Dim ap As Process() = Process.GetProcessesByName(sRAs34Name)
        If (ap IsNot Nothing) AndAlso (ap.Length > 0) Then
            '-- is alive.. so switch to it..
            Dim intId As IntPtr = ap(0).MainWindowHandle
            '-found-  Make it active-
            intResult = SetForegroundWindow(intId)
        Else '-not there.. so can launch..-
            Try
                Process.Start(sRAs34Path, sRAs34Args)
            Catch ex As Exception
                MsgBox("ERROR executing StartProcess cmd: " & vbCrLf & sRAs34Path & vbCrLf & vbCrLf & _
                       "Error: " & vbCrLf & ex.Message & vbCrLf, MsgBoxStyle.Exclamation)
            End Try
        End If  '-nothing-

    End Sub  '-ras-
    '= = = = = = = = ==  =
    '-- btnLaunchRAs_Click-

    Private Sub btnLaunchRAs_Click(sender As Object, ev As EventArgs)
        Call tsBtnRAs_Click(sender, ev)
    End Sub  '- btnLaunchRAs_Click-
    '= = = = = = = = ==  == === =
    '-===FF->

    '-- Emails..

    Private Sub tsBtnEmail_Click(sender As Object, e As EventArgs)
        Dim frmEmailMain1 As frmEmailMain

        If msMainStaffBarcode = "" Then
            MessageBox.Show("Must have admin signon.", "JobMatix POS ", _
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtAdminStaffBarcode.Select()
            Exit Sub
        End If
        Try
            '--  load RAs Main form and show it..
            frmEmailMain1 = New frmEmailMain
        Catch ex As Exception
            MsgBox("Error in loading Emails Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
        '--show-
        Try
            frmEmailMain1.connection = mCnnSql
            frmEmailMain1.SqlServer = msServer
            frmEmailMain1.DBname = msSqlDbName
            frmEmailMain1.colTables = mColSqlDBInfo
            frmEmailMain1.StaffBarcode = msMainStaffBarcode
            '= frmEmailMain1.IsAdminTest = bAdminTest
            frmEmailMain1.DllVersion = msDllversion  '=3411.0127=

            frmEmailMain1.ShowDialog()
        Catch ex As Exception
            MsgBox("Error in showing Email Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Sub  '-email-
    '= = = = = = = = = = =  =

    '--btnEmailQueue_Click-
    'Private Sub btnEmailQueue_Click(sender As Object, ev As EventArgs)

    '    Call tsBtnEmail_Click(sender, ev)
    'End Sub '-btnEmailQueue_Click-
    '= = = = = = = = = = = = = 

    Private Sub btnDropdownEmailQueue_Click(sender As Object, ev As EventArgs) Handles btnDropdownEmailQueue.Click
        Call tsBtnEmail_Click(sender, ev)
    End Sub '-btnDropdownEmailQueue_Click-
    '= = = = = = =  = = == = = = = == 
    '-===FF->

    '-- Admin Tab-  Button Events..

    '-- Admin Tab-  Button Events..
    '-- Handles ALL Buttons.-
    '-- adminFunctionButtons --

    Private Sub mSub_AdminFunctionButtons_click(ByVal sender As System.Object, _
                                                ByVal ev As System.EventArgs)
        'Handles btnPurchasing.Click, _
        '        btnStock.Click, _
        '        btnGoods.Click, _
        '        btnStockLabels.Click, _
        '        btnStockTake.Click, _
        '        btnSerialLookup.Click, _
        '         btnReports.Click, _
        '         btnCashup.Click, _
        '          btnChangeTill.Click, _
        '          btnStaff.Click, _
        '           btnSuppliers.Click, _
        '            btnCategory1.Click, _
        '            btnCategory2.Click, _
        '             btnBrands.Click, _
        '             btnAccountPayments.Click, _
        '             btnStatements.Click, _
        '             btnCreditNotesHistory.Click, _
        '             btnCustomers.Click, _
        '                 btnSetup.Click
        Dim button1 As Button = CType(sender, Button)
        Dim sButtonName As String = button1.Name

        '= MsgBox("you clicked button '" & button1.Name & "'..", MsgBoxStyle.Information)

        '- call admin exec function..
        Select Case LCase(sButtonName)
            Case "btnpurchasing"
                'Dim frmGoods1 As New frmGoodsRecvd
                'frmGoods1.IsPurchaseOrder = True
                'frmGoods1.versionPOS = msDllversion
                'frmGoods1.StaffId = mIntMainStaff_id
                'frmGoods1.StaffName = msMainStaffName
                'frmGoods1.SqlServer = msServer
                'frmGoods1.connectionSql = mCnnSql '--job tracking sql connenction..-
                'frmGoods1.dbInfoSql = mColSqlDBInfo
                'frmGoods1.PreferredColumnsSuppliers = mColPrefsSupplier
                'frmGoods1.PreferredColumnsStock = mColPrefsStock
                'frmGoods1.DBname = msSqlDbName
                'frmGoods1.form_left = Me.Left '= Me.Left
                'frmGoods1.form_top = Me.Top + 40  '= Me.Top + 40
                'frmGoods1.UserLogo = mImageUserLogo
                'frmGoods1.ShowDialog()

                '= Call mbNewPurchaseOrderChild()

                '-- done-
            Case "btnstock"
                'Dim frmStock1 As New frmStock
                'frmStock1.StaffName = msMainStaffName
                'frmStock1.SqlServer = msServer
                'frmStock1.connectionSql = mCnnSql '--job tracking sql connection..-
                'frmStock1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
                'frmStock1.DBname = msSqlDbName
                'frmStock1.VersionPOS = msDllversion
                'frmStock1.form_left = Me.Left '=  Me.Left
                'frmStock1.form_top = Me.Top + 40  '=Me.Top + 30
                'frmStock1.ShowDialog()
                '--done-
            Case "btngoods"
                'Dim frmGoods1 As New frmGoodsRecvd
                'frmGoods1.IsPurchaseOrder = False
                'frmGoods1.versionPOS = msDllversion
                'frmGoods1.StaffId = mIntMainStaff_id
                'frmGoods1.StaffName = msMainStaffName
                'frmGoods1.SqlServer = msServer
                'frmGoods1.connectionSql = mCnnSql '--job tracking sql connenction..-
                'frmGoods1.dbInfoSql = mColSqlDBInfo
                'frmGoods1.DBname = msSqlDbName
                ''=3301.816=
                'frmGoods1.PreferredColumnsSuppliers = mColPrefsSupplier
                'frmGoods1.PreferredColumnsStock = mColPrefsStock
                'frmGoods1.form_left = Me.Left '=  Me.Left
                'frmGoods1.form_top = Me.Top + 40  '= Me.Top + 20
                'frmGoods1.UserLogo = mImageUserLogo
                'frmGoods1.ShowDialog()
                '--done-
            Case "btnstocklabels"
                'Dim colGridLabels As Collection
                'Dim frmStockLabels1 As frmStockLabels
                'colGridLabels = New Collection '--leave empty-
                'frmStockLabels1 = New frmStockLabels(Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                '                                  msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName, colGridLabels)
                'frmStockLabels1.ShowDialog()
                '--done-
            Case "btnstocktake"
                'Dim frmStocktake1 As New ucStocktake(Me, mCnnSql, msServer, msSqlDbName, mColSqlDBInfo, _
                '                         msDllversion, mIntMainStaff_id, msMainStaffName)
                'frmStocktake1.ShowDialog()
            Case "btnseriallookup"
                'Dim frmFind1 As frmFindSerial
                ''== Dim sError, sBarcode As String
                ''-- load find serial form..
                'frmFind1 = New frmFindSerial(mCnnSql, msSqlDbName, mColSqlDBInfo)
                'frmFind1.ShowDialog()
                'If frmFind1.cancelled Then
                '    frmFind1.Close()
                '    MsgBox("Form Cancelled", MsgBoxStyle.Exclamation)
                '    Exit Sub
                'Else
                '    'sError = frmFind1.errorText
                '    'If sError <> "" Then
                '    '    frmFind1.Close()
                '    '    Exit Sub
                '    'End If
                '    frmFind1.Close()
                'End If
                '-ok- get results.
                '=intAudit_id = frmFind1.serial_id
                '=intStock_id = frmFind1.stock_id
                '== sBarcode = frmFind1.barcode
            Case "btnreports"
                'Dim frmReports1 As New frmPOS3Reports
                'frmReports1.StaffName = msMainStaffName
                'frmReports1.connectionSql = mCnnSql '--job tracking sql connenction..-
                'frmReports1.DBname = msSqlDbName
                'frmReports1.ColSqlDBInfo = mColSqlDBInfo
                'frmReports1.versionPOS = msDllversion
                'frmReports1.ShowDialog()
                '--done-
            Case "btncashup"
                Dim frmCashup1 As frmCashup
                '=3519.0227=
                If (Not mbIsPosLicenceOK) Then
                    MsgBox("Product is not licenced for this intallation.", MsgBoxStyle.Information)
                End If
                frmCashup1 = New frmCashup(msComputerName, Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                                  msDllversion, mImageUserLogo, msSettingsPath, mIntMainStaff_id, msMainStaffName)
                frmCashup1.ShowDialog()
                '--done=
            Case "btnchangetill"
                Dim sTillId As String
                If Not gbChangeCashDrawer(mCnnSql, msComputerName, sTillId) Then
                    MsgBox("NO TILL!")
                Else  '--ok-
                    mStrOurTillId = gsGetCurrentCashDrawer()
                    '= tsLabSaleTillId.Text = "- Till-" & mStrOurTillId & " -"
                    btnMainTill.Text = "- Till-" & mStrOurTillId & " -"
                    MsgBox("You are now on Till-" & gsGetCurrentCashDrawer(), MsgBoxStyle.Information)
                End If  '-get-
                '-done-
            Case "btnstaff"
                Dim colKeys As Collection
                Dim colSelectedRow As Collection
                If Not mbBrowseTable(mColPrefsStaff, "Lookup Staff", "", colKeys, colSelectedRow, "Staff") Then
                    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                End If
                '--done-
            Case "btnsuppliers"
                Dim colKeys As Collection
                Dim colSelectedRow As Collection
                If Not mbBrowseTable(mColPrefsSupplier, "Lookup Suppliers", "", colKeys, colSelectedRow, "supplier") Then
                    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                End If
                '-done-
            Case "btncategory1"
                'Dim colKeys As Collection
                'Dim colSelectedRow As Collection
                'If Not mbBrowseTable(mColPrefsCategory1, "Lookup Category1", "", colKeys, colSelectedRow, "category1") Then
                '    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                'End If
                '-done-
            Case "btncategory2"
                'Dim colKeys As Collection
                'Dim colSelectedRow As Collection
                'If Not mbBrowseTable(mColPrefsCategory2, "Lookup Category2", "", colKeys, colSelectedRow, "category2") Then
                '    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                'End If
                '-done-
            Case "btnbrands"
                'Dim colKeys As Collection
                'Dim colSelectedRow As Collection
                'If Not mbBrowseTable(mColPrefsBrands, "Lookup Brands", "", colKeys, colSelectedRow, "StockBrands") Then
                '    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                'End If
                '-done-
            Case "btnaccountpayments"
                'Dim frmPayments1 As ucChildPayments
                'frmPayments1 = New ucChildPayments(Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                '                                 mColPrefsCustomer, msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
                'frmPayments1.ShowDialog()

                '= Call mbNewPaymentsChild()

                '-done-
            Case "btnstatements"
                'Dim frmStatements1 As ucChildStatements
                'frmStatements1 = New ucChildStatements(Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                '                                 mColPrefsCustomer, msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
                'frmStatements1.ShowDialog()
                '-done-
            Case "btncreditnoteshistory"
                'Dim frmCreditNotes1 As frmCreditNotesReport
                'frmCreditNotes1 = New frmCreditNotesReport(msServer, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                '                                msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
                'frmCreditNotes1.ShowDialog()
                '-done-
            Case "btnviewlaybys"
                '-- show Laybys Form.-
                '-- show laybys ALL cust.   
                'Dim frmlayby1 As New ucChildLaybys(msComputerName, Me, mCnnSql, _
                '              msSqlDbName, msDllversion, mIntMainStaff_id, msMainStaffName)
                ''= frmlayby1.laybys = colLaybys
                'frmlayby1.saleCustomer_id = -1
                'frmlayby1.ShowDialog()
                '=      Call mbNewLaybysChild()

            Case "btnsubscriptions"
                '=3411.1125=
                '= MsgBox("Subscriptions still coming..", MsgBoxStyle.Information)
                'Dim frmSubs1 As New ucChildSubscription

                ''= frmlookup1.SqlServer = msServer
                'frmSubs1.SqlServer = msServer
                'frmSubs1.ComputerName = msComputerName
                'frmSubs1.connectionSql = mCnnSql '-- sql connenction..-
                'frmSubs1.dbInfoSql = mColSqlDBInfo
                'frmSubs1.staff_id = mIntMainStaff_id
                'frmSubs1.UserLogo = mImageUserLogo

                'frmSubs1.DBname = msSqlDbName
                'frmSubs1.VersionPOS = msDllversion
                ''= frmlookup1.supplier_id = mIntSupplier_id
                ''= frmlookup1.SupplierName = txtSupplierName.Text
                'frmSubs1.staffNname = msMainStaffName
                'frmSubs1.ShowDialog()
                'frmSubs1.Close()

                '= Call mbNewSubscriptionChild()

            Case "btncustomers"
                'Dim frmCust1 As New frmCustomer
                'frmCust1.StaffId = mIntMainStaff_id
                'frmCust1.StaffName = msMainStaffName
                'frmCust1.SqlServer = msServer
                'frmCust1.connectionSql = mCnnSql '--job tracking sql connection..-
                'frmCust1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..

                'frmCust1.DBname = msSqlDbName
                'frmCust1.VersionPOS = msDllversion
                ''= frmCust1.form_left = mFrmSale.Left '=  Me.Left
                ''= frmCust1.form_top = mFrmSale.Top = 30 '=  Me.Top + 30
                'frmCust1.ShowDialog()

                '= Call mbNewCustomerChild()

            Case "btnsetup"
                '= MsgBox("Button '" & button1.Name & "' still to come...", MsgBoxStyle.Information)
                Dim frmAdmin1 As frmPOS34Setup
                frmAdmin1 = New frmPOS34Setup(Me, msServer, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                                msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
                frmAdmin1.ShowDialog()
            Case "btncashdrawers"
                '= MsgBox("Button '" & button1.Name & "' still to come...", MsgBoxStyle.Information)
                Dim frmCashDrawers1 As frmCashDrawers
                frmCashDrawers1 = New frmCashDrawers(Me, msServer, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                                msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
                frmCashDrawers1.ShowDialog()
            Case Else
                MsgBox("No such button as '" & button1.Name & "'..", MsgBoxStyle.Information)
        End Select  '--button-
    End Sub  '-adminFunctionButtons-
    '= = = = = = = = = =  = = = = = = = = = =
    '-===FF->

    '-- Handles ALL Buttons.-
    '-- adminFunctionButtons --

    '=--  3403.521=  PLUS View Laybys..
    '=--  3411.1125=  PLUS View Subscriptions..

    '= 4201.0424-
    '== no more  btnStock.Click, _
    '==           btnSerialLookup.Click, _
    '==             btnStockTake.Click, _
    '==             purchases, Goods., _

    Private Sub adminFunctionButtons_click(ByVal sender As System.Object, _
                                                ByVal ev As System.EventArgs)
        Dim button1 As Button = CType(sender, Button)
        Dim sButtonName As String = button1.Name

        '= MsgBox("you clicked button '" & button1.Name & "'..", MsgBoxStyle.Information)

        Call mSub_AdminFunctionButtons_click(sender, ev)  '--see above..-

    End Sub  '-adminFunctionButtons-
    '= = = = = = = = = =  = = = = = = = = = =
    '-===FF->

    '-- Admin ribbon Buttons with Context menu..
    '-- Admin ribbon Buttons with Context menu..
    '-- Admin ribbon Buttons with Context menu..

    '-btnTrLookup-

    Private Sub btnTrLookup_Click(sender As Object, ev As EventArgs)
        Call mbNewTransLookupChild()
    End Sub  '-btnTrLookup-
    '= = = = = = =  = = = = = = =

    '-btnMainTill_Click-
    '-btnMainTill-

    Private Sub mnuMainTillmenu_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) _
                                  Handles mnuMainTillBalance.Click, mnuMainTillChange.Click, mnuMainTillLastTran.Click
        Dim thisMenu As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)

        Select Case thisMenu.Name
            Case "mnuMainTillChange"
                Call tsMenuItemChangeTill_Click(eventSender, eventArgs)
            Case "mnuMainTillBalance"
                Call tsMenuItemCashup_Click(eventSender, eventArgs)
            Case "mnuMainTillLastTran"
                Call tsMenuItemLastTrans_Click(eventSender, eventArgs)
        End Select

    End Sub  '-till menu.
    '= = = = = = = = = = =

    '-btnMainTill_Click-

    Private Sub btnMainTill_Click(sender As Object, e As EventArgs) Handles btnMainTill.Click

        '=MsgBox("Till Menu..")
        Dim thisButton As Button = CType(sender, Button)

        DoEvents()
        Thread.Sleep(100)
        DoEvents()
        '-- Avoid the 'disabled' gray text by locking updates
        LockWindowUpdate(thisButton.Handle.ToInt32)
        '---- A disabled TextBox will not display a context menu
        thisButton.Enabled = False
        '--- Give the previous line time to complete
        System.Windows.Forms.Application.DoEvents()
        '-- Display our own context menu
        mContextMenuStripTillAction.Show(thisButton, (New Point(20, 40)))
        '= mContextMenuTillAction.Show(thisButton, (New Point(200, 50)))
        ' Enable the control again
        thisButton.Enabled = True
        '-- Unlock updates
        LockWindowUpdate(0)

    End Sub  '-btnMainTill_Click-
    '= = = = = === = = = = = = = 
    '-===FF->

    '-- Drop down buttons-  Stock--
    '-- Drop down buttons-  Stock--

    Private Sub mnuStock_click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) _
                               Handles mnuMainStockAdmin.Click, mnuMainStockSerials.Click, _
                                                mnuMainStockStocktake.Click, mnuMainStockLabels.Click
        Dim thisMenu As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)

        Select Case LCase(thisMenu.Name)
            Case "mnumainstockadmin"
                Call mbNewStockChild()
            Case "mnumainstockserials"
                Call mbNewSerialLookupChild()
            Case "mnumainstockstocktake"
                Call mbNewStocktakeChild()
            Case "mnumainstocklabels"
                Dim colGridLabels As Collection
                Dim frmStockLabels1 As frmStockLabels
                colGridLabels = New Collection '--leave empty-
                frmStockLabels1 = New frmStockLabels(Me, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                                  msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName, colGridLabels)
                frmStockLabels1.ShowDialog()

            Case Else
        End Select

    End Sub  '-stock-
    '= = == = = = = 

    '--Purchases-

    Private Sub mnuPurchases_click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles mnuMainPurchaseOrders.Click, _
                                                                   mnuMainGoodsReceived.Click, mnuMainGoodsReturned.Click, _
                                                                   mnuMainGoodsSuppliers.Click
        Dim thisMenu As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)

        Select Case LCase(thisMenu.Name)
            Case "mnumainpurchaseorders"
                Call mbNewPurchaseOrderChild(True)
            Case "mnumaingoodsreceived"
                Call mbNewPurchaseOrderChild(False)
            Case "mnumaingoodsreturned"
                Call tsBtnRAs_Click(eventSender, eventArgs)
            Case "mnumaingoodssuppliers"

                '==   Target-New-Build-4284 --  (08-Nov-2020)
                '==   Target-New-Build-4284 --  (08-Novenber-2020)
                '==  A.  New Child USERCONTROL to move Suppliers Admin into Main Tab Control.

                '- THIS out..
                'Dim colKeys As Collection
                'Dim colSelectedRow As Collection
                'If Not mbBrowseTable(mColPrefsSupplier, "Lookup Suppliers", "", colKeys, colSelectedRow, "supplier") Then
                '    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                'End If
                Call mbNewSupplierChild()
                '== END  Target-New-Build-4284 --  (30-October-2020)

            Case Else
        End Select
    End Sub  '-purchases..
    '= = = = = = == = = = = =
    '-===FF->

    '==Categories..

    Private Sub mnuCategories_click(ByVal eventSender As System.Object, _
                               ByVal eventArgs As System.EventArgs) Handles mnuMainCategoriesCat1.Click, _
                                                                            mnuMainCategoriesCat2.Click, _
                                                                            mnuMainCategoriesBrands.Click
        Dim thisMenu As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)

        Select Case LCase(thisMenu.Name)
            Case "mnumaincategoriescat1"
                Dim colKeys As Collection
                Dim colSelectedRow As Collection
                If Not mbBrowseTable(mColPrefsCategory1, "Lookup Category1", "", colKeys, colSelectedRow, "category1") Then
                    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                End If
            Case "mnumaincategoriescat2"
                Dim colKeys As Collection
                Dim colSelectedRow As Collection
                If Not mbBrowseTable(mColPrefsCategory2, "Lookup Category2", "", colKeys, colSelectedRow, "category2") Then
                    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                End If
                '-done-
            Case "mnumaincategoriesbrands"
                Dim colKeys As Collection
                Dim colSelectedRow As Collection
                If Not mbBrowseTable(mColPrefsBrands, "Lookup Brands", "", colKeys, colSelectedRow, "StockBrands") Then
                    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                End If
        End Select
    End Sub  '-mnuCategories-
    '= = = = = = = = = = = = =
    '= = = = = = = = = = == = 
    '-===FF->

    '== NEW- 11-Oct-2019-
    '== NEW VERSION- 27-Oct-2019-
    '==  Standalone call to Print Debtors Report..

    Private Function mbShowDebtorsReport(ByVal bSummaryOnly As Boolean, _
                                         Optional ByVal intClosedDaysToShow As Integer = 0, _
                                         Optional ByVal bSelectedCustomer As Boolean = False) As Boolean
        Dim clsDebtors1 As clsDebtors
        Dim colReportCustomers As Collection
        Dim msReportPrintername As String = ""
        Dim dateCutoff As Date = Today
        Dim clsPrint1 As clsPrintSaleDocs
        Dim sReportPrinterName As String = ""
        Dim bOUtstandingInvoicesOnly As Boolean = True
        Dim bPreviewOnly As Boolean = True

        Dim intDefaultPrinterIndex As Integer
        Dim colPrinters As Collection
        Dim intRequestedCustomer_id As Integer = -1

        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sCustomerName As String = "--"
        Dim sSubHeading As String = ""

        mbShowDebtorsReport = False
        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
            Exit Function
        Else
            If (msDefaultPrinterName <> "") Then
                sReportPrinterName = msDefaultPrinterName
            Else
                MsgBox("No Default printer set ! ", MsgBoxStyle.Exclamation)
                Exit Function
            End If
        End If
        '==  Select Customer if needed..
        If bSelectedCustomer Then
            If Not mbBrowseAndSearchCustomers(mColPrefsCustomer, "Lookup Customers", _
                                               " (isAccountCust<>0) ", colKeys, colSelectedRow) Then
                '= "WHERE" keyword is illegal and not needed here.
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                Exit Function
            Else  '--selected
                '= txtCustName.Text = ""
                If (Not (colSelectedRow Is Nothing)) Then    '= AndAlso (colKeys.Count > 0) Then
                    intRequestedCustomer_id = CInt(colSelectedRow.Item("customer_id")("value"))
                    '-CustomerName-
                    sCustomerName = CStr(colSelectedRow.Item("CustomerName")("value"))
                    '= MsgBox("Selected: " & sCustomerName, MsgBoxStyle.Information)
                    bOUtstandingInvoicesOnly = False   '-- We want full history.
                    sSubHeading = "Account History for " & sCustomerName
                Else
                    MsgBox("Lookup empty..", MsgBoxStyle.Exclamation)
                    Exit Function
                End If
            End If  '-browse-
        End If  '--select customer.
        '-- get info-
        clsDebtors1 = New clsDebtors(mCnnSql, msSqlDbName, _
                                       mColSqlDBInfo, mColPrefsCustomer, _
                                       msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
        '-- ALWAYS get Outstanding only..
        '--  + intClosedDaysToShow of prev. closed invoices
        If clsDebtors1.GetAllDebtorReportInfo(bOUtstandingInvoicesOnly, intClosedDaysToShow, _
                                               dateCutoff, colReportCustomers, intRequestedCustomer_id) Then
            If (Not (colReportCustomers Is Nothing)) AndAlso (colReportCustomers.Count > 0) Then
                clsPrint1 = New clsPrintSaleDocs
                '-- now print to whereever..
                '--  WITH PREVIEW--
                Call clsPrint1.PrintDebtorsReport(colReportCustomers, dateCutoff, _
                                                     mSysInfo1, mImageUserLogo, msDllversion, _
                                                        sReportPrinterName, bSummaryOnly, bPreviewOnly, sSubHeading)
                mbShowDebtorsReport = True
            Else
                '-failed.-
            End If  '--nothing
        Else
            '-failed.-
        End If

    End Function '-mbShowDebtorsReport=
    '= = = = = = = = = = == = 
    '-===FF->

    '--btnDropdownAccounts--
    '--btnDropdownAccounts--

    Private Sub mnuAccounts_click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) _
                                Handles mnuAccountPayments.Click, mnuAccountSubs.Click, mnuAccountLaybys.Click, _
                                         mnuAccountCustomers.Click, mnuAccountStatements.Click
        Dim thisMenu As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)

        Select Case LCase(thisMenu.Name)
            Case "mnuaccountpayments"
                Call mbNewPaymentsChild()
            Case "mnuaccountsubs"
                Call mbNewSubscriptionChild()
            Case "mnuaccountlaybys"
                Call mbNewLaybysChild()
            Case "mnuaccountcustomers"
                Call mbNewCustomerChild()
            Case "mnuaccountstatements"
                Call mbNewStatementsChild()
                'Case "mnudebtorssummary"
                '    Call mbShowDebtorsReport(True)  '-IS summary.
                'Case "mnudebtorsdetailed_outst"
                '    Call mbShowDebtorsReport(False)  '-not summary.
                'Case "mnudebtorsdetailed_outst_30"
                '    Call mbShowDebtorsReport(False, 30)  '-not summary.
                'Case "mnudebtorsdetailed_outst_60"
                '    Call mbShowDebtorsReport(False, 60)  '-not summary.
                'Case "mnudebtorsselectedcustomerhistory"
                '    Call mbShowDebtorsReport(False, , True)  '-not summary. Select Customer.
            Case Else
        End Select
    End Sub  '- mnuAccounts_click=
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-REPORTS Dropdown menu.

    Private Sub mnuReports_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) _
                                             Handles mnuReportsSales.Click, _
                                             mnuReportsTransLookup.Click, mnuReportsCreditNotes.Click, _
                                             mnuReportsDebtorsSummary.Click, mnuReportsDebtorsDetailed_outst.Click, _
                                             mnuReportsDebtorsDetailed_outst_30.Click, mnuReportsDebtorsDetailed_outst_60.Click, _
                                             mnuReportsDebtorsCustomerHistory.Click
        Dim thisMenu As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)

        Select Case LCase(thisMenu.Name)
            Case "mnureportssales"
                '= Call Popup form if wanted..
                'If MsgBox("Test new  Reports Child ?", _
                '                 MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) = MsgBoxResult.Yes Then
                Call mbNewPosReportsChild()
                Exit Sub
                'End If
                '-- popup old form.
                'Dim frmReports1 As New frmPOS3Reports
                'frmReports1.StaffName = msMainStaffName
                'frmReports1.connectionSql = mCnnSql '--job tracking sql connenction..-
                'frmReports1.DBname = msSqlDbName
                'frmReports1.ColSqlDBInfo = mColSqlDBInfo
                'frmReports1.versionPOS = msDllversion
                'frmReports1.ShowDialog()
                ''--done-
                'frmReports1.Dispose()
            Case "mnureportstranslookup"
                Call mbNewTransLookupChild()
            Case "mnureportscreditnotes"
                Dim frmCreditNotes1 As frmCreditNotesReport
                frmCreditNotes1 = New frmCreditNotesReport(msServer, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                                msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
                frmCreditNotes1.ShowDialog()
                '-- debtors Report.-
            Case "mnureportsdebtorssummary"
                Call mbShowDebtorsReport(True)  '-IS summary.
            Case "mnureportsdebtorsdetailed_outst"
                Call mbShowDebtorsReport(False)  '-not summary.
            Case "mnureportsdebtorsdetailed_outst_30"
                Call mbShowDebtorsReport(False, 30)  '-not summary.
            Case "mnureportsdebtorsdetailed_outst_60"
                Call mbShowDebtorsReport(False, 60)  '-not summary.
            Case "mnureportsdebtorscustomerhistory"
                Call mbShowDebtorsReport(False, , True)  '-not summary. Select Customer.
            Case Else
        End Select

    End Sub  '-mnuLaybys_Click-
    '= = = = = = = = == = = = = = = 

    '- Settings Menu..

    Private Sub mnuSettings_click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles mnuSettingsSetupOptions.Click, _
                                                                             mnuSettingsCashDrawers.Click, mnuSettingsStaff.Click
        Dim thisMenu As ToolStripMenuItem = CType(eventSender, ToolStripMenuItem)

        Select Case LCase(thisMenu.Name)
            Case "mnusettingssetupoptions"
                Dim frmAdmin1 As frmPOS34Setup
                frmAdmin1 = New frmPOS34Setup(Me, msServer, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                                msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
                frmAdmin1.ShowDialog()
                frmAdmin1.Dispose()

            Case "mnusettingscashdrawers"
                Dim frmCashDrawers1 As frmCashDrawers
                frmCashDrawers1 = New frmCashDrawers(Me, msServer, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                                msDllversion, mImageUserLogo, mIntMainStaff_id, msMainStaffName)
                frmCashDrawers1.ShowDialog()
                frmCashDrawers1.Dispose()
            Case "mnusettingsstaff"

                '==   Target-New-Build-4284-EXTRA-EXTRA --  (22-Nov-2020)
                '==  -- New Child USERCONTROL to move STAFF Admin into Main Tab Control.
                '==
                'Dim colKeys As Collection
                'Dim colSelectedRow As Collection
                'If Not mbBrowseTable(mColPrefsStaff, "Lookup Staff", "", colKeys, colSelectedRow, "Staff") Then
                '    MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
                'End If
                Call mbNewStaffChild()
                '== END  Target-New-Build-4284-EXTRA-EXTRA --  (22-Nov-2020)

        End Select

    End Sub  '-mnuSettings_click-
    '= = = = = = = = = = = == =
    '-===FF->

    '- Dropdown buttons menus.
    '- Dropdown buttons menus.

    Private Sub btnDropdownButtons_Click(sender As Object, ev As EventArgs) _
                                            Handles btnDropdownStock.Click, btnDropdownPurchases.Click, _
                                            btnDropdownCategories.Click, _
                                            btnDropdownAccounts.Click, btnDropdownReports.Click, _
                                            btnDropdownSettings.Click
        '=MsgBox("Till Menu..")
        Dim thisButton As Button = CType(sender, Button)

        DoEvents()
        Thread.Sleep(100)
        DoEvents()
        '-- Avoid the 'disabled' gray text by locking updates
        LockWindowUpdate(thisButton.Handle.ToInt32)
        '---- A disabled TextBox will not display a context menu
        thisButton.Enabled = False
        '--- Give the previous line time to complete
        System.Windows.Forms.Application.DoEvents()
        '-- Display our own context menu
        Select Case LCase(thisButton.Name)

            Case "btndropdownstock"
                mContextMenuStripStockAction.Show(thisButton, (New Point(0, 90)))
            Case "btndropdownpurchases"
                mContextMenuStripPurchases.Show(thisButton, (New Point(0, 90)))
            Case "btndropdowncategories"
                mContextMenuStripCategories.Show(thisButton, (New Point(0, 90)))
            Case "btndropdownaccounts"
                mContextMenuStripAccounts.Show(thisButton, (New Point(0, 90)))
            Case "btndropdownreports"
                mContextMenuStripReports.Show(thisButton, (New Point(0, 90)))
            Case "btndropdownsettings"
                mContextMenuStripSettings.Show(thisButton, (New Point(0, 90)))
            Case Else

        End Select
        ' Enable the control again
        thisButton.Enabled = True
        '-- Unlock updates
        LockWindowUpdate(0)
    End Sub  '-btnDropdownlaybys=
    '= = = = = = = = = == =====
    '-===FF->

    '-- B a c k u p  POS/JOBS DB --
    '-- B a c k u p  POS/JOBS DB --

    '-- Build backup file-name as "JobsBackup_ddmmmyyyy-tttt"  ---  "tttt" is 24-hr time.-
    '---  Use SAVE-AS dialogue to get backup File-spec..--

    '== Build 3077.519 19-May2013=  
    '--     DB Backup.. If running on Server.. Create .bak on local directory first if exists.
    '==                                           Then copy to target..
    '==
    '==   -- 4219.1106.  06-Nov-2019-  Started 06-November-2019-
    '==      -- POS Main- Drop "labStatus" from Backup call....

    Private Sub btnDbBackup_Click(sender As Object, e As EventArgs) Handles btnDbBackup.Click
        Dim sFinalBackupPath As String = ""

        If (msMainStaffBarcode = "") Then Exit Sub '--was signed off..-
        '= mlStaffTimeout = -1 '--SUSPEND timing out..--

        '=3401.415- CALL BACKUP Function..=
        'If Not gbBackupJobsDatabase(Me, msSqlServerComputer, mCnnSql, msMachineName, msComputerName, _
        '                            msJobmatixAppName, msCurrentUserNT, _
        '                              msSqlDbName, dlg1Save, mSysInfo1, sFinalBackupPath) Then
        If Not gbBackupJobsDatabase(Me, msSqlServerComputer, mCnnSql, msMachineName, msComputerName, _
                                    msJobmatixAppName, msCurrentUserNT, _
                                      msSqlDbName, dlg1Save, sFinalBackupPath) Then
            MsgBox("Backup not done.", MsgBoxStyle.Exclamation)
        Else  '-ok-
            MsgBox("ok- the DB '" & msSqlDbName & "' was backed up successfully to: " & vbCrLf & _
                                                                  sFinalBackupPath, MsgBoxStyle.Information)
        End If  '-backup-

        Me.BringToFront()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '= mlStaffTimeout = 0 '--start timing out..--
        '= labStatus.Text = ""
    End Sub  '-backup-
    '= = = = = = = = = = = = == = =


    '-- close button..

    Private Sub tsBtnClose_Click(sender As Object, e As EventArgs)

        Me.Close()
    End Sub  '-btn Close-
    '= = = = = = = == = = = = 

    '- EXIT-  FROM Parent !!

    Private Sub mnuFileExit_Click(sender As Object, e As EventArgs)

        '-- Check if application can allow the form to close.. ??
        Me.Close()

    End Sub  '--close-
    '= = = = == = = == 

    Private Sub btnExit_Click(sender As Object, ev As EventArgs)

        Call mnuFileExit_Click(sender, ev)

    End Sub  '-exit.
    '= = = = = = = = = = == = = = = =  =

    '--Reorder Payment Details-
    '--Reorder Payment Details-

    Private Sub mnuReorderPaymentDetails_Click(sender As Object, _
                                               ev As EventArgs) Handles mnuReorderPaymentDetails.Click
        '-- FOR File-> PREFs request to re-order payment TYpes..
        '-- FOR File-> PREFs request to re-order payment TYpes..
        '-- FOR File-> PREFs request to re-order payment TYpes..

        '-- re-order the list from local settings key order if any..

        '--USE new class-

        Dim clsPayTypes1 As clsPaymentTypes

        '-- check local settings (prefs) for paylist order...
        msSettingsPath = gsLocalSettingsPath(msJobmatixAppName) '= default Jobmatix33=
        '=3300.428= 
        '= mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        '-test-
        '= MsgBox("Settings path: " & vbCrLf & msSettingsPath, MsgBoxStyle.Information)

        clsPayTypes1 = New clsPaymentTypes(msSettingsPath)
        clsPayTypes1.UpdatePaymentPrefs()

        '--  test getting full collection.
        Dim sMsg As String = ""

        Dim colPaymentTypes As Collection = clsPayTypes1.getColPaymentTypes

        For Each colType As Collection In colPaymentTypes
            sMsg &= colType.Item("key") & " / " & colType.Item("description") & vbCrLf
        Next colType
        '= MsgBox("Payment Types are:" & vbCrLf & sMsg, MsgBoxStyle.Information)

    End Sub  '--Reorder Payment Details-
    '= = = = = = = = = = == = = = = =  =
    '-===FF->

    '== MAIN frmPosMain_FormClosing ==

    Private Sub frmPosMain_FormClosing(sender As Object, EventArgs As FormClosingEventArgs) _
                                       Handles Me.FormClosing

        'Dim messageBoxVB As New System.Text.StringBuilder()
        'messageBoxVB.AppendFormat("{0} = {1}", "CloseReason", ev.CloseReason)
        'messageBoxVB.AppendLine()
        'messageBoxVB.AppendFormat("{0} = {1}", "Cancel", ev.Cancel)
        'messageBoxVB.AppendLine()
        'MessageBox.Show(messageBoxVB.ToString(), "FormClosing Event")

        Dim intCancel As Boolean = EventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = EventArgs.CloseReason

        '--MsgBox "frmMaint UNload event..'"  '-debug--
        '--If Not gbclosingDown Then
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                              System.Windows.Forms.CloseReason.TaskManagerClosing, _
                                            System.Windows.Forms.CloseReason.FormOwnerClosing '==  NOT FOR vb.net.., vbFormCode
                '= mbCancelled = True
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                If mbStartupAborted Then
                    intCancel = 0 '--let it go--
                    '=End If
                    '-- check if any children still open.
                ElseIf (Application.OpenForms.Count > 0) Then

                    If MessageBox.Show("Ok to close all open Tabs/forms ?", "POS Main Closing", _
                                       MessageBoxButtons.YesNo, _
                                       MessageBoxIcon.Question, _
                                       MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.Yes Then
                        intCancel = 0 '--Yes- let it go--
                    Else  '-no- stay in..
                        intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                    End If  '-yes-

                End If '- form count-
                '= mbCancelled = True
            Case Else
                intCancel = 0 '--let it go--

        End Select
        EventArgs.Cancel = intCancel

    End Sub  '== closing-
    '= = = = = = = = = = = == 


End Class  '-frmMain-
'= = = = = = = = = = = = = = = = =

'==-- the end ===
