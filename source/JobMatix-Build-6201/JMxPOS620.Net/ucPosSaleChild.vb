Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports system.data.sqlclient
Imports system.data.OleDb
Imports System.Math
Imports System.ComponentModel

Public Class ucPosSaleChild
    '= Inherits System.Windows.Forms.Form

    Private Const TVM_SETEXTENDEDSTYLE As Integer = &H1100 + 44
    Private Const TVS_EX_DOUBLEBUFFER As Integer = &H4
    Dim currentNode As TreeNode
    '= Public Hot_Tracking_Node As TreeNode
    Private Declare Function SendMessageA Lib "User32" (ByVal hWnd As IntPtr, _
                                                        ByVal m As Integer, _
                                                        ByVal wParam As IntPtr, _
                                                        ByVal lParam As IntPtr) As IntPtr

    '-- POS1 Main Form..  Called from JobMatix..

    '==
    '==  grh =V3.0.3012.603== 03-Jun-2014=
    '==      POS Main form.. Called from JobMatix..
    '==
    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '== = = = = = =  =
    '==
    '==  JobMatix POS3- DLL -- 
    '==     v3.1.3101.925..  25-Sep-2014 ===
    '==
    '==     v3.1.3101.1007..  07-Oct-2014 ===
    '==      >> Cut all admin stuff and send to frmPOS31Admin -
    '==      >> Strip Sales and send all code to clsPOS3Main-
    '==
    '==     v3.1.3101.1206..  06-Dec-2014 ===
    '==      >> Replace chkChargeTpAcct with labCharge -
    '==
    '==     v3.1.3107.731..  06-Dec-2014 ===
    '==      >> Local settings now in C:\ProgramData..-
    '==
    '==     v3.1.3107.922..  22-Sep-2015= ===
    '==      >> Previous invoices--
    '==      >>  -- Now use command button to pop up list of invoices.
    '==
    '==     v3.3.3300.427..  27-Apr-2016= ===
    '==          >> Restore "OptSaleSale" RadioButton to set of Trans. options.
    '==
    '==     v3.3.3300.516..  16-may-2016= ===
    '==          >> Redesign POS Sale Form to use Textboxes for Entry of current sale.
    '==          >>   and 3300.525-  add Option for refund to take Cash or Credit.
    '==          >> 3300.602-  Drop txtSaleAvailCredit--.
    '==
    '==     v3.3.3300.1210..  12-Dec-2016= ===
    '==       >> Introducing CashDrawer ID's (as per MYOB RM.).- 
    '==
    '==     v3.3.3300.1227..  27-Dec-2016= ===
    '==       >> Cust Barcode- Must catch "Validating" event for TAB key. .- 
    '==
    '==     v3.3.3301.0202..  02-Feb-2017= ===
    '==       >> RESTORE AutoValidate to "EnablePreventFocusChange"  !!!!  .- 
    '==
    '==
    '==     v33.3.3301.0218/219 =
    '==      >> Handle txtSaleItemBarcode VALIDATED Event for all Line Item textboxes..
    '==      >> txtSaleItemBarcode_TextChanged Event now captured for ItemBarcode ONLY--
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW POS Build..  For Multiple Sale Instances..--
    '== 
    '==     v3.4.3401.0307 = 07Mar2017=
    '==      -- New Build for POS 34.  Extended TAB controls inside JobMatix POS Tab...
    '==      --  SALE Screen can NOW support THREE clsPOS34Sale instances (For Holding Sales in progress)--
    '==      --  This form renamed to frmPOS34Main..
    '==      --  Sales and Admin (Stock etc access buttons.. not Options) now in sub-tabs on Main form...
    '==      --  Options now will occupy remainder of Original POS Admin. form...
    '==     v3.4.3401.0307 = 07Mar2017=
    '==         >>  major restructuring of sale Screen layout..
    '==     3401.0314- SALE Screen- CASHOUT textbox and events removed.--
    '==
    '==
    '--   Was Archived and tested at Precise..
    '==
    '==  3401.0321- Fixes and Changes-..-
    '==     >>  Trans. selection. Replaced RadioButtons with Combo. (Sale/Refund/Quote). 
    '==     >>  Show Invoice/Quote. Replaced RadioButtons with two cmd buttons.. 
    '==     >>  Form.Keydown- F6 call Hold Button... 
    '==
    '==    3401.327=  Cashout is DROPPED !!
    '==      >> ALSO-- ADD EFTPOS_DR, EFTPOS_CR to Refund Options.
    '==
    '==    v.3401.0414- 14Apr2017=
    '==          >>  (possible thin Client) ComputerName now comes as input..-..-
    '= = = = = = = = = = = = =  ==  = = = = =
    '==
    '==  NEW POS Build..  For Adding on Layby's functionality...--
    '==  NEW POS Build..  For Adding on Layby's functionality...--
    '==  NEW POS Build..  For Adding on Layby's functionality...--
    '== 
    '==     v3.4.3403.0429 = 29Apr2017=
    '==      --  Added Tables- LayBy and LaybyLine
    '==      --  3404.510 Added button to show/print Layby.
    '==
    '==     3403.728/730- 28-30-July2017-
    '==      -- Main SALE Form..  Remove Comments/Delivery to separate form..  New button to call it.
    '==
    '==     3403.1014- 07/10/11/14-Oct-2017-
    '==      -- MAJOR SHIFT..
    '==           >> All Customers can have credit Notes (Account and non-a/c custs).
    '==           >>  For Sales to Account Cust-  only onAccount invoices will go to Debtors.
    '==                  So account cust can have normal Cash Sales
    '==                --  'chkOnAccount' Checkbox (Charge to Account) added to sale Payments Panel. 
    '==                    (User must check this to make on-account sale..).
    '==        >> 3411.1125 -- ADMIN- Add Button to call Subscriptions..
    '== 
    '==        >> 3411.1219 -- THIS Form frmPOS34Main (Sale form) moved into MxPOS340 dll assembly .
    '==                -- Is now NON-Modal form called from Main Program
    '==                -- Or can be compiled into POS349ex EXE to be launched from JobTracking.
    '==
    '==        >> 3411.0103 -- 03-Jan-2018== .
    '==                -- Integrating Email Agent Form. 
    '==        >> 3411.0126 -- 26-Jan-2018== .
    '==              -- Update Cust-Barcode Tooltip text for F5-New Cust... 
    '==
    '==      -- 3411.0208=  08-Feb-2018=
    '==            --  Sale Form- Add BUTTON to show Last Sales Invoice.
    '==
    '==    -- 3411.0311=  11-Mar-2018= Fixes for Lachlan- 
    '==        (i)  Sales Form:  Use buttons to select Trans..  
    '==                   !! Sale has to be without mouse..
    '==        (ii) Drop Combo, drop "Continue" button..  
    '==       (iii) Fix Selling out Job (1 item missing)..
    '==     -- 3411.0313=
    '==           -- catch ESCAPE for Sale form Cancel function..
    '==                    -= PreviewKeyDown is where you preview the key.
    '==
    '==     3411.0324- 24-March-2018-
    '==      -- POS Setup/Options Button. Set up Cash Drawer printer Connections....
    '==
    '==      3411.0402 -- 01-Apr-2018- 
    '==      --Main POS Form.. Catch CTL-Z and Open CashDrawer for current Till..
    '==       -- 05-Apr-2018- POS Form and Sale- Fix Discount Tab sequence...
    '==       -- 15-Apr-2018- POS Sale- Catch Mouse-Click on Item Barcode to set Selection stuff....
    '==       -- 22-Apr-2018- POS Sale- Move "Show last Sale to inside invoice list panel."...
    '==                     ( No.. take it back out..)
    '==   >> 3431.0621=  21-June-2018=
    '==    -- Cleanup Email XML Files for reserved chars (eg ampersand,<, > )...
    '==    -- frmPOS34Main..  Chenge Activated Event to Shown event.....
    '==
    '==   V3.5 NOW IS Mdi CHILD..  02July2018-
    '==   V3.5 NOW IS Mdi CHILD..  02July2018-
    '==
    '==   V3.5.3501.0702
    '==        --  NOW IS Mdi CHILD..  02July2018-  Drop all Hold/Restore Buttons.
    '==   V3.5.3501.0716       
    '==     -- Use Delegate to signal child closed to Main Parent.....
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
    '==      --  Fixes for Tab Text (delete cr/lf)
    '==
    '==   >>   '=3501.0824=   24-Aug-2018.
    '==     dgvPaymentDetails-- catch enter and pass to keydown.
    '==
    '==    
    '==   >> 3501.1001- 01Oct2018= 
    '==          -- Change Commit button colour if has focus.
    '==
    '==   >> 3501.1028- 28-Oct-2018= 
    '==          -- Drop Buttom "Show Last sale".  (Function moved to Top Tollbar Till Dropdown)
    '==
    '==    New Build No.- 3519.0117 17-Jan-2019= 
    '==     --  Fix Sale- nettCredited mustn't include PaymentCredited as credit note...
    '==     --  Fix Sale- Make "ChargeToAccount"=true (now two copies) the initial value for Account Cust. Sales.
    '==                 and make TABSTOP=false for Payments Grid when "ChargeToAccount" is checkek....
    '==
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==     -- Sales (clsPOS34Sale)-  
    '==       -- Looking up Customers- make special Sql-Select for Browser 
    '==    -- MAJOR-  Add TextBox to Payments panel- 
    '==            for User to decide on Amount of CreditNote to withdraw to pay fpr Sale.
    '==          ALSO- formBorderStyle  is now fixedToolWindow..
    '==
    '==   Updated.- 3519.0330  Started 30-March-2019= 
    '==    -- Sales- Add Trans. button and code to "Convert" a Quote into a sale.
    '==              ie.. Allow import of Quote items into sale. and ask for SerialNos where needed.
    '==    
    '== - - - - RELEASED as 3519.0414 --
    '==  
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==
    '==    First New Build- 4201.0416 -  Started 16-April-2019.
    '==    
    '==    -- 4201.0424. TDI Child forms now converted to UserControls....
    '==
    '==    -- 4201.0831. Removed Tabstop om Discount % combo...
    '==
    '== NEW revision-
    '== NEW revision-
    '==
    '==   -- 4201.1027/1030.  30-Oct-2019-  Started 27-Oct-2019-
    '==      -- MAIN TAB Control... Increased tab text size, and add proper Close X Icon...
    '==      -- MAJOR- WE Have DISABLED the Payments Frame- grpBoxSalePayments for On-Account Sales !!...
    '==                  Acompanying payments are no longer permitted.
    '==             ALSO- make Top chkOnAccount2 checkbox part of Tab sequence for selecting transaction.
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1106.  06-Nov-2019-  Started 06-November-2019-
    '==      -- Adjusted sizes for Child UserControls to fit inside Tab Pages...
    '==
    '==
    '==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
    '==    
    '==   == 4221.0207.  05-Feb-2020- 
    '==
    '==   -- Tags- 05-Feb-2020.. For Build 4221..
    '==         -- Add clsTags, and forms for Ref Tags and Cust tags..
    '==         -- Add Column  "Tags" to Customer Table in the StartUp class clsJMxPOS31..
    '==         -- Add buttons and subs to Customer admin form to update/show Tags....
    '==         -- Show Customer Tags on Sale form when Customer selected...
    '==
    '=== = = = =  = = = = = = =
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
    '==   ALSO FIXED- 06-June-2020.  In Sales Form (ucPosSaleChild) Fix Fonts in Item entry Line textboxes..
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==
    '= = = = = = Latest = = = = = = = = = = = = = ==  = = = = = = = = = = = = = = = = = = = = = = 

    '== Public Const K_SAVESETTINGSPATH As String = "localPOSSettings.txt"

    Private mbActivated As Boolean = False
    Friend mbLoginRequested As Boolean = False  '--makes visible to events..-

    '- - - - -
    Private mbIsInitialising As Boolean = True
    Private mbActive As Boolean = False
    Private mbStartingUp As Boolean
    Private mbMainLoadDone As Boolean = False

    Private mIntFormDesignHeight As Integer
    Private mIntFormDesignWidth As Integer '--save starting dimensions..-
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
    Private msAppPath As String
    Private msLastSqlErrorMessage As String = ""

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    Private mlJobId As Integer = -1

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1
    Private msStaffBarcodeSignedOn As String = ""

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

    '==  WAS for REPORTS..=  Private mCnnShape As ADODB.Connection '--for jobs SHAPE commands..--
    Private mbSqlConnectionFailure As Boolean = False

    '--  Browser prefs..--

    '== Private mColPrefsStaff As Collection
    '== Private mColPrefsCustomer As Collection
    '== Private mColPrefsCategory1 As Collection
    '== Private mColPrefsCategory2 As Collection
    '== Private mColPrefsBrands As Collection

    '== Private mColPrefsStock As Collection
    '== Private mColPrefsSupplier As Collection
    '= = = = = = = = = = = = = = = = = = = =

    '== Sale ==


    '== Sale ==
    '== Sale ==
    '== Sale ==


    '-  M a i n --

    '== POS  S A L E Stuff =
    '== POS  S A L E Stuff =
    '== POS  S A L E Stuff =

    '-- Actual Class Instances with Sale states ..-
    '-- These are always loaded instanced..
    'Private mClsSale_A As clsPOS34Sale
    'Private mClsSale_B As clsPOS34Sale
    'Private mClsSale_C As clsPOS34Sale

    Private mColFreeSalesInstances As Collection '--holds the free class instance ptrs.-

    '-- Point to current Instance that has the Screen.
    Private mClsSale1 As clsPOS34Sale
    '-- Point to who is in Held-1.
    '= Private mClsSaleHeld1 As clsPOS34Sale
    '-- Point to who is in Held-2 slot.
    '= Private mClsSaleHeld2 As clsPOS34Sale

    '- hold temp
    'Private mClsSaleHeldTemp As clsPOS34Sale

    ''--  ID (A,B,C) of who is in the slots..
    'Private msSaleCurrent_ID As String = ""  '-(A,B or C)-
    'Private msSaleHeld1_ID As String = ""  '-(A,B or C)-
    'Private msSaleHeld2_ID As String = ""  '-(A,B or C)-

    '-- POS Licence--
    Private mClsJmxPOS31_Licence As clsJMxPOS31
    Private mbIsEvaluating As Boolean = False

    '=3411.0402= 02Apr20111111118=
    '-- Startup- Check we have Till assigned..
    Private mStrOurTillId As String = ""

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport

    '==--Child uses Delegate to signal child STAFF SIGNED ON to Main Parent.....
    Public Delegate Sub subChildSignedOn(ByVal intStaffid As Integer, _
                                ByVal strStaffBarcode As String, _
                                 ByVal strStaffName As String)
    Public delChildSignedOn As subChildSignedOn
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- NEW- Till-id comes from MDI Parent.
    WriteOnly Property TillId() As String
        Set(ByVal Value As String)
            mStrOurTillId = Value
            '== labSaleTillId.Text = mStrOurTillId
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    '-- Computer Name now comes from caller..--
    '-- Now can be thin Client..

    WriteOnly Property computerName() As String
        Set(ByVal Value As String)
            msComputerName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    '-- Staff Id now comes from caller..--

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    '- and brcode..-
    WriteOnly Property Staffbarcode() As String
        Set(ByVal Value As String)
            msStaffBarcodeSignedOn = Value
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

    '=4201.0708=
    WriteOnly Property SettingsPath As String
        Set(value As String)
            msSettingsPath = value
        End Set
    End Property  '-settings.
    '= = = = = = = = = = = =  =

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
    '-===FF->

    '- Set DoubleBuffered..-

    Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
        SendMessageA(Me.Handle, TVM_SETEXTENDEDSTYLE, _
                         CType(TVS_EX_DOUBLEBUFFER, IntPtr), _
                             CType(TVS_EX_DOUBLEBUFFER, IntPtr))
        MyBase.OnHandleCreated(e)
    End Sub
    '= = = = = ==  = = = = = = = =

    '- Catch CLOSE MSG..
    '-- Allow close wothout Validating controls..

    Private Const WM_CLOSE As Integer = &H10
    '- - - - --
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_CLOSE Then
            AutoValidate = AutoValidate.Disable
        End If
        MyBase.WndProc(m)
    End Sub  '-wndPoroc=
    '= = = = == = = = = 

    '--load local settings.. eg printer names, sql serrvername..
    '==3300.428=  GONE=
    Private Function xxxx_mbLoadSettings() As Boolean
        Dim handle_Renamed As Short
        Dim lResult, k2 As Integer
        Dim sPath As String
        Dim sLine As String
        Dim sKey, sValue As String


    End Function '--load settings..--
    '= = = = = =  =  == = = == ==
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
            msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Update one setting..--
    '----change the setting in the static var dictionary..--
    '----- Write the dictionary back to disk..--

    '==3300.428=  GONE=

    Private Function xxx_mbSaveSetting(ByVal sKey As String, _
                                    ByVal sValue As String) As Boolean
    End Function '--save setting.--
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--init--

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub  '--new--
    '= = = =  = = = = = = = =

    '-Can form close --

    Private Function mbCanSaleClose() As Boolean

        Dim bCancel As Boolean = False '= = EventArgs.Cancel
        Dim sName, s1 As String
        Dim intPos, intTranNo As Integer

        mbCanSaleClose = False  '-no-

        intTranNo = 0
        sName = Me.Name
        '-- get trans. no from name.
        intPos = InStr(sName, "_")
        If (intPos > 0) AndAlso IsNumeric(Mid(sName, intPos + 1)) Then
            intTranNo = CInt(Mid(sName, intPos + 1))
        End If

        '-- check for incomplete transactions..
        If (intTranNo = 1) Then
            MsgBox("Note- The First sale form has to stay.. ", MsgBoxStyle.Information)
            bCancel = True
        ElseIf Not mClsSale1.HasCurrentSale Then   '-empty..  can be dropped.
            bCancel = False  '--let it go--
        ElseIf (MsgBox("Close this transaction ?", _
                      MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
            bCancel = True  '- reconsidered-
        Else  '-ok to close=
            'If (Not (mCnnSql Is Nothing)) AndAlso ((mCnnSql.State And 1) <> 0) Then '--open-
            '    mCnnSql.Close()
            'End If
            bCancel = False  '--let it go--
        End If

        If bCancel Then Exit Function '--keep alive.-
        mbCanSaleClose = True  '-ok to exit-

    End Function '-can we close ?-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '- UpdateTabText-

    Private Sub mSubUpdateTabText()

        Dim thisTabPage As TabPage = CType(Me.Parent, TabPage)
        thisTabPage.Text = labSaleTranType.Text & ": " & _
                             VB.Left(Replace(txtSaleCustName.Text, vbCrLf, "; "), 16)
        thisTabPage.ToolTipText = labSaleTranType.Text & ": " & VB.Left(txtSaleCustName.Text, 16)
        Me.Text = thisTabPage.Text
    End Sub  '- UpdateTabText-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)
    '= = = = = = = = = = = = = = = = = = = = == = 


    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                            ByVal intParentHeight As Integer)

        Me.Top = 0
        Me.Left = 0

        Me.Width = intParentWidth - 150 '= - 11
        Me.Height = intParentHeight - 150 '= - 11

        '--  grpboxSale is just inside the Child Form.
        grpboxSale.Width = Me.Width - 10
        grpboxSale.Height = Me.Height - 14

        panelSalesCurrentHdr.Left = grpboxSale.Width - panelSalesCurrentHdr.Width - 7

        panelSaleHdr.Width = grpboxSale.Width - panelSalesCurrentHdr.Width - 17
        '==picSaleItem.Left = panelSaleHdr.Width - picSaleItem.Width - 14
        panelSaleLineEntry.Width = panelSaleHdr.Width  '= - panelPayment.Width - 5 '= grpboxSale.Width - 3
        dgvSaleItems.Width = panelSaleLineEntry.Width  '= panelSaleHdr.Width  '= grpboxSale.Width - 3

        dgvSaleItems.Height = _
            (grpboxSale.Height - panelSaleLineEntry.Height - panelSaleHdr.Height - panelSaleFooter.Height - 20) '= 65)

        panelSaleFooter.Top = dgvSaleItems.Top + dgvSaleItems.Height + 3  '= grpboxSale.Height - panelSaleFooter.Height - 11
        panelSaleFooter.Width = panelSaleLineEntry.Width   '= grpboxSale.Width - 7

        panelSaleTotals.Left = panelSaleFooter.Width - panelSaleTotals.Width

        panelCommit.Left = panelSalesCurrentHdr.Left
        panelCommit.Top = grpboxSale.Height - panelCommit.Height - 15

        '-TEST--
        '= Me.btnMainExit.Text = "EXIT"
        DoEvents()

        Me.Invalidate()
        Me.Update()

    End Sub  '-FormResized-
    '= = = = = = = = = = = =  =

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click..
    '=- Return true if ok to Close.

    Public Function SubFormCloseRequest() As Boolean

        SubFormCloseRequest = mbCanSaleClose()
        '==Me.Close()
        '= Call close_me()
    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    '--L o a d--

    Private Sub frmPOS3Main_Load(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles MyBase.Load
        '== Dim L1, rx As Integer
        '== Dim s1 As String
        '== Dim col1, colRecords, colRow As Collection
        '== Dim sSql As String
        '== Dim item1 As System.Windows.Forms.ListViewItem
        '== Dim row1 As DataGridViewRow

        Me.Text = "JobMatix Retail POS"
        mbStartingUp = True

        '--  stuff from original LOAD..--
        mIntFormDesignHeight = Me.Height '--save starting dimensions..-
        mIntFormDesignWidth = Me.Width '--save starting dimensions..-

        '=3411.1220==  
        'If mIntFormUserTop >= 0 Then
        '    Me.Top = mIntFormUserTop
        '    If mIntFormUserLeft >= 0 Then
        '        Me.Left = mIntFormUserTop
        '    End If
        'Else
        '    Call CenterForm(Me)
        'End If

        '= LabServer.Text = ""
        '== labVersion.Text = ""
        '= labSqlLogin.Text = ""
        '= labStaffName.Text = ""
        labSaleHelp.Text = ""
        labSaleHelp2.Text = ""

        '= frameMain1.Text = ""
        '== grpBoxAdmin.Text = ""
        grpboxSale.Text = ""
        '= btnSaleShowLastSale.Enabled = False

        '-- No jobs for POS-Only--
        labSaleJobDelivery.Enabled = False

        '= mbSuperAdmin = False
        '= frameMain1.Text = ""
        '== panelLineEntry.Enabled = False
        '== btnLineEnter.Enabled = False

        dgvSaleItems.Enabled = False
        labSaleChange.Enabled = False

        '= Call gbLogMsg(gsRuntimeLogPath, "=== JobMatixPOS Main Sales Child form is loading..")

        '==Now INPUT  msComputerName = My.Computer.Name
        '--test-
        '= =MsgBox("Local ComputerName is :" + vbCrLf + msComputerName)

        msCurrentUserName = gsGetCurrentUser()

        '== msVersionPOS = "JobMatixPOS v:" & CStr(My.Application.Info.Version.Major) & "." & _
        '==                                  My.Application.Info.Version.Minor & ", Build: " & _
        '==                                      My.Application.Info.Version.Build & "." & _
        '== My.Application.Info.Version.Revision

        '== labVersion.Text = msVersionPOS
        DoEvents()
        Call gbLogMsg(gsRuntimeLogPath, "JobmatixPOS35 Sale Child form starting up for user:" & vbCrLf & _
                                                msComputerName & "\" & msCurrentUserName & "..")

        '=Call CenterForm(Me)

        '== NO TOOLBAR== '-- Create toolbar--
        '== NO TOOLBAR== '-- Not avalaible in the designer.--
        '== NO TOOLBAR== Call InitializeMyToolBar()
        '== NO TOOLBAR== toolBarTransaction.Enabled = False
        '=Me.Text = msMainVersionPOS

        '= labToday.Text = Format(CDate(DateTime.Today), "ddd dd-MMM-yyyy")

        '=3501.1001=
        btnCommitSale.BackColor = Color.WhiteSmoke

        Me.Visible = True

        '=4201.0424= - Main stuff FROM old Shown event..
        '=4201.0424= - Main stuff FROM old Shown event..
        '=4201.0424= - Main stuff FROM old Shown event..
        '- Main stuff FROM old Shown event..
        '- Main stuff FROM old Shown event..

        Dim pd1 As New PrintDocument()
        Dim s1 As String
        Dim ix, L1 As Integer
        Dim colSystemInfo As Collection

        'If mbActivated Then Exit Sub '-- do once only..--
        'mbActivated = True
        'Me.Update()
        'Application.DoEvents()   '--let form show..--

        msCurrentUserNT = msSqlServerComputer & "\" & msCurrentUserName
        '= labSqlLogin.Text = msCurrentUserNT

        '-- get system Info table data.-
        '=3300.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '=3300.428=If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        msBusinessAddress1 = mSysInfo1.item("BUSINESSADDRESS1")
        msBusinessAddress2 = mSysInfo1.item("BUSINESSADDRESS2")
        msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
        msBusinessState = mSysInfo1.item("BUSINESSSTATE")
        msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
        msBusinessPhone = mSysInfo1.item("BUSINESSPHONE")

        '== If mSdSystemInfo.Contains("POS_ACCOUNTTERMS") Then
        '==   txtPOS_Terms.Text = mSdSystemInfo.Item("POS_ACCOUNTTERMS")
        '= End If

        '=3300.428=End If  '-load sys info--
        '== btnSaveTerms.Enabled = False  '--must be here after we load text..

        '-- get local settings..
        '=3300.428= Call mbLoadSettings()
        '=4201.0708= msSettingsPath = gsLocalJobsSettingsPath("JobMatix35") '= msAppPath & K_SAVESETTINGSPATH

        '==3311.214= Load up Settings.
        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)
        If mLocalSettings1.queryLocalSetting("sqlserver", s1) Then
            '= MsgBox("TEST- Found setting sqlserver: " & s1, MsgBoxStyle.Information)
            '== msServer = s1
        End If

        '-- load printer assignments..
        labSaleHelp2.Text = vbCrLf & "  Checking Default Printer.."
        msDefaultPrinterName = ""  '== Printer.DeviceName '--  save name of original default printer..-
        '--  find name of default printer..--
        For ix = 0 To (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count - 1)
            s1 = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Item(ix)
            '--  set printer selected so we can test....--
            pd1.PrinterSettings.PrinterName = s1
            If pd1.PrinterSettings.IsDefaultPrinter Then
                msDefaultPrinterName = s1
                Exit For
            End If
            '== ListBox1.Items.Add(pkInstalledPrinters)
            '== cboPrtSelect.Items.Add(pkInstalledPrinters)
        Next ix
        '== L1 = Err().Number
        '== On Error GoTo activate_error
        If (msDefaultPrinterName = "") Then '== L1 <> 0 Then '--no printer..-
            MsgBox("Error in getting Default Printer.." & vbCrLf & _
                   "Error is " & L1 & " = " & ErrorToString(L1) & vbCrLf & vbCrLf & _
                      "Printers may not be set up in Windows.." & vbCrLf & _
                         "JobTracking printing may not be available..", MsgBoxStyle.Exclamation)
        End If

        '-- check out if "JobPrinters.txt" exists in local (app) directory..--
        '---  it should have  "prtAgreement=devicename" --
        '----    and "prtReceiptt=devicename" -----
        '-- for each function, find printer via printers collection..-
        '--- and set mPrtAgreement or mPrtReceipt to the related printer object..--
        '--Call mbLoadSettings  '--refresh..-
        labSaleHelp2.Text = vbCrLf & "  Checking Job Printers..."
        msColourPrtName = mLocalSettings1.item("PRTCOLOUR")
        msReceiptPrtName = mLocalSettings1.item("PRTRECEIPT")
        msLabelPrtName = mLocalSettings1.item("PRTLABEL")

        '--  if no saved printer names, then..---
        If (msColourPrtName = "") Then
            '== Call mbSetPrinter(msDefaultPrinter, 0)
            Call mLocalSettings1.SaveSetting("PRTCOLOUR", msDefaultPrinterName)
            MsgBox("Service agreements will print to your default printer !!" & vbCrLf & _
                   "Unless you change the JobMatix printer setup.." & vbCrLf & _
                       "(Main Menu ->File/Printers..)", MsgBoxStyle.Exclamation)
        End If
        If (msReceiptPrtName = "") Then
            '== Call mbSetPrinter(msDefaultPrinter, 1)
            Call mLocalSettings1.SaveSetting("PRTRECEIPT", msDefaultPrinterName)
            MsgBox("RECEIPTS will print to your default printer !!" & vbCrLf & _
                   "Unless you change theJobMatix printer setup.. ." & vbCrLf & _
                      "(Main Menu ->File/Printers..)", MsgBoxStyle.Exclamation)
        End If
        If (msLabelPrtName = "") Then
            labSaleHelp2.Text = vbCrLf & "No LABEL Printer is set up.. "
            '==MsgBox("No LABEL Printer is set up.. " & vbCrLf & vbCrLf & _
            '==        "You can set it up in printer setup..." & vbCrLf & _
            '==           "(Main Menu ->File->Printers..)", MsgBoxStyle.Information)
        End If
        '--refresh--
        msColourPrtName = mLocalSettings1.item("PRTCOLOUR")
        msReceiptPrtName = mLocalSettings1.item("PRTRECEIPT")
        '==msLabelPrtName = mSdSettings.Item("PRTLABEL")

        '= Me.KeyPreview = True  '--for picking up ENTER on textboxes..

        '== txtSaleCustBarcode.Select() '-focus--

        '-- POS set up Sale stuff..--
        '-- POS set up Sale stuff..--

        '=3501.0702=  Now we are a Child.
        mClsSale1 = New clsPOS34Sale(Me, ToolTip1, msServer, mCnnSql, msSqlDbName, _
                              mColSqlDBInfo, msMainVersionPOS, mImageUserLogo, _
                                 msSettingsPath, mIntStaff_id, msStaffName)
        ''-- POS Licence--
        'mClsJmxPOS31_Licence = New clsJMxPOS31(mCnnSql, msServer, msSqlDbName, mColSqlDBInfo, gsErrorLogPath)

 
        '= btnSaleHold.Enabled = True
        '= Me.KeyPreview = True   '- Tp catch F6 Hold..

        '= btnSaleShowLastSale.Enabled = True

        mbIsInitialising = False
        '==3401.312= txtSaleCustBarcode.Select()
        txtSaleStaffBarcode.Select()


    End Sub  '--load-
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '--Form Activated--
    'Private Sub frmPOS3Main_Activated(ByVal sender As System.Object, _
    '                                 ByVal e As System.EventArgs) Handles MyBase.Activated
    '    '= If mbActivated Then Exit Sub '-- do once only..--
    '    '= mbActivated = True
    '    '= Me.Update()
    '    Application.DoEvents()   '--let form show..--
    '    '= Me.WindowState = FormWindowState.Maximized

    'End Sub '-- Activated-
    '= = = = = = = = =  = = = = = = 

    '- Main stuff is now Shown event..

    'Private Sub frmPOS3Main_Shown(ByVal sender As System.Object, _
    '                                    ByVal e As System.EventArgs) Handles MyBase.Shown

    '== Dim event1 As System.EventArgs
    'Dim pd1 As New PrintDocument()
    'Dim s1 As String
    'Dim ix, L1 As Integer
    'Dim colSystemInfo As Collection

    ''If mbActivated Then Exit Sub '-- do once only..--
    ''mbActivated = True
    ''Me.Update()
    ''Application.DoEvents()   '--let form show..--

    'msCurrentUserNT = msSqlServerComputer & "\" & msCurrentUserName
    ''= labSqlLogin.Text = msCurrentUserNT

    ''-- get system Info table data.-
    ''=3300.428=
    'mSysInfo1 = New clsSystemInfo(mCnnSql)

    ''=3300.428=If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
    'msBusinessABN = mSysInfo1.item("BUSINESSABN")
    'msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
    ''== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
    'msBusinessName = mSysInfo1.item("BUSINESSNAME")
    'msBusinessAddress1 = mSysInfo1.item("BUSINESSADDRESS1")
    'msBusinessAddress2 = mSysInfo1.item("BUSINESSADDRESS2")
    'msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
    'msBusinessState = mSysInfo1.item("BUSINESSSTATE")
    'msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
    'msBusinessPhone = mSysInfo1.item("BUSINESSPHONE")

    ''== If mSdSystemInfo.Contains("POS_ACCOUNTTERMS") Then
    ''==   txtPOS_Terms.Text = mSdSystemInfo.Item("POS_ACCOUNTTERMS")
    ''= End If

    ''=3300.428=End If  '-load sys info--
    ''== btnSaveTerms.Enabled = False  '--must be here after we load text..

    ''-- get local settings..
    ''=3300.428= Call mbLoadSettings()
    'msSettingsPath = gsLocalJobsSettingsPath("JobMatix35") '= msAppPath & K_SAVESETTINGSPATH

    ''==3311.214= Load up Settings.
    ''=3300.428= 
    'mLocalSettings1 = New clsLocalSettings(msSettingsPath)
    'If mLocalSettings1.queryLocalSetting("sqlserver", s1) Then
    '    '= MsgBox("TEST- Found setting sqlserver: " & s1, MsgBoxStyle.Information)
    '    '== msServer = s1
    'End If

    ''-- load printer assignments..
    'labSaleHelp2.Text = vbCrLf & "  Checking Default Printer.."
    'msDefaultPrinterName = ""  '== Printer.DeviceName '--  save name of original default printer..-
    ''--  find name of default printer..--
    'For ix = 0 To (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count - 1)
    '    s1 = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Item(ix)
    '    '--  set printer selected so we can test....--
    '    pd1.PrinterSettings.PrinterName = s1
    '    If pd1.PrinterSettings.IsDefaultPrinter Then
    '        msDefaultPrinterName = s1
    '        Exit For
    '    End If
    '    '== ListBox1.Items.Add(pkInstalledPrinters)
    '    '== cboPrtSelect.Items.Add(pkInstalledPrinters)
    'Next ix
    ''== L1 = Err().Number
    ''== On Error GoTo activate_error
    'If (msDefaultPrinterName = "") Then '== L1 <> 0 Then '--no printer..-
    '    MsgBox("Error in getting Default Printer.." & vbCrLf & _
    '           "Error is " & L1 & " = " & ErrorToString(L1) & vbCrLf & vbCrLf & _
    '              "Printers may not be set up in Windows.." & vbCrLf & _
    '                 "JobTracking printing may not be available..", MsgBoxStyle.Exclamation)
    'End If

    ''-- check out if "JobPrinters.txt" exists in local (app) directory..--
    ''---  it should have  "prtAgreement=devicename" --
    ''----    and "prtReceiptt=devicename" -----
    ''-- for each function, find printer via printers collection..-
    ''--- and set mPrtAgreement or mPrtReceipt to the related printer object..--
    ''--Call mbLoadSettings  '--refresh..-
    'labSaleHelp2.Text = vbCrLf & "  Checking Job Printers..."
    'msColourPrtName = mLocalSettings1.item("PRTCOLOUR")
    'msReceiptPrtName = mLocalSettings1.item("PRTRECEIPT")
    'msLabelPrtName = mLocalSettings1.item("PRTLABEL")

    ''--  if no saved printer names, then..---
    'If (msColourPrtName = "") Then
    '    '== Call mbSetPrinter(msDefaultPrinter, 0)
    '    Call mLocalSettings1.SaveSetting("PRTCOLOUR", msDefaultPrinterName)
    '    MsgBox("Service agreements will print to your default printer !!" & vbCrLf & _
    '           "Unless you change the JobMatix printer setup.." & vbCrLf & _
    '               "(Main Menu ->File/Printers..)", MsgBoxStyle.Exclamation)
    'End If
    'If (msReceiptPrtName = "") Then
    '    '== Call mbSetPrinter(msDefaultPrinter, 1)
    '    Call mLocalSettings1.SaveSetting("PRTRECEIPT", msDefaultPrinterName)
    '    MsgBox("RECEIPTS will print to your default printer !!" & vbCrLf & _
    '           "Unless you change theJobMatix printer setup.. ." & vbCrLf & _
    '              "(Main Menu ->File/Printers..)", MsgBoxStyle.Exclamation)
    'End If
    'If (msLabelPrtName = "") Then
    '    labSaleHelp2.Text = vbCrLf & "No LABEL Printer is set up.. "
    '    '==MsgBox("No LABEL Printer is set up.. " & vbCrLf & vbCrLf & _
    '    '==        "You can set it up in printer setup..." & vbCrLf & _
    '    '==           "(Main Menu ->File->Printers..)", MsgBoxStyle.Information)
    'End If
    ''--refresh--
    'msColourPrtName = mLocalSettings1.item("PRTCOLOUR")
    'msReceiptPrtName = mLocalSettings1.item("PRTRECEIPT")
    ''==msLabelPrtName = mSdSettings.Item("PRTLABEL")

    ''== '-- select sales..-
    ''== labSaleTranType.Text = "Sale"
    ''== If (mlJobId > 0) Then
    ''== panelOptTranType.Enabled = False
    ''== labSaleJobDelivery.Visible = True
    ''== txtSaleJobNo.Visible = True
    ''== txtSaleJobNo.Text = CStr(mlJobId)
    ''== Else
    ''== panelOptTranType.Enabled = True
    ''== labSaleJobDelivery.Visible = False
    ''== txtSaleJobNo.Visible = False
    ''== labSaleTranType.Text = "Sale"
    ''== End If

    ''== optSaleSale.Checked = True
    ''== msTransactionType = "Sale"
    ''== labSaleHelp2.Text = ""

    ''== Call mbClearInvoice()
    ''== labSaleHelp.Text = "Enter Customer No. (F2 to lookup..)"

    'Me.KeyPreview = True  '--for picking up ENTER on textboxes..
    ''== txtSaleCustBarcode.Select() '-focus--

    ''-- POS set up Sale stuff..--
    ''-- POS set up Sale stuff..--
    ''-- POS set up Sale stuff..--

    ''=  3401.307=  Base class renamed..
    ''-- AND we have THREE Sale Instances..
    ''btnSaleHold.Enabled = False
    ''btnSaleRestore1.Enabled = False
    ''btnSaleRestore1.BackColor = Color.LightGray '-enabled = cornflowerBlue-

    ''btnSaleRestore2.Enabled = False
    ''btnSaleRestore2.BackColor = Color.LightGray  '-enabled = Plum-

    ''labSaleHeld1Lab.Enabled = False
    ''labSaleHeld2Lab.Enabled = False
    ' ''-- staff/ cust..
    '' ''= labHeld1Info.Text = ""
    ' ''= labHeld2Info.Text = ""
    ' ''=3403.731= 
    ''btnSaleRestore1.Text = ""
    ''btnSaleRestore2.Text = ""

    ''- Make them all now so they're ready.
    ''mClsSale_A = New clsPOS34Sale(Me, ToolTip1, msServer, mCnnSql, msSqlDbName, _
    ''                              mColSqlDBInfo, msMainVersionPOS, mImageUserLogo, _
    ''                                 mIntStaff_id, msStaffName)
    ''mClsSale_B = New clsPOS34Sale(Me, ToolTip1, msServer, mCnnSql, msSqlDbName, _
    ''                              mColSqlDBInfo, msMainVersionPOS, mImageUserLogo, _
    ''                                 mIntStaff_id, msStaffName)
    ''mClsSale_C = New clsPOS34Sale(Me, ToolTip1, msServer, mCnnSql, msSqlDbName, _
    ''                              mColSqlDBInfo, msMainVersionPOS, mImageUserLogo, _
    ''                                 mIntStaff_id, msStaffName)
    ' ''--make collection-
    ''mColFreeSalesInstances = New Collection
    ' ''== start with  Inst. _A for first transaction current..
    ''mClsSale1 = mClsSale_A
    ''msSaleCurrent_ID = "A"  '-A instance is current.-
    ' ''=
    ' ''- B and C are still free.
    ''mColFreeSalesInstances.Add(mClsSale_B)
    ''mColFreeSalesInstances.Add(mClsSale_C)

    ''=3501.0702=  Now we are a Child.
    'mClsSale1 = New clsPOS34Sale(Me, ToolTip1, msServer, mCnnSql, msSqlDbName, _
    '                      mColSqlDBInfo, msMainVersionPOS, mImageUserLogo, _
    '                         mIntStaff_id, msStaffName)

    ' ''-- POS Licence--
    ''mClsJmxPOS31_Licence = New clsJMxPOS31(mCnnSql, msServer, msSqlDbName, mColSqlDBInfo, gsErrorLogPath)

    ' ''=Private mbIsEvaluating As Boolean = False
    ''If Not mClsJmxPOS31_Licence.LicenceCheckPOS(mbIsEvaluating) Then
    ''    MsgBox("No POS Licence..", MsgBoxStyle.Information)
    ''ElseIf mbIsEvaluating Then
    ''    MsgBox("Site is still evaluating POS Licence..", MsgBoxStyle.Information)
    ''End If

    ' ''-- Startup- Check we have Till assigned..
    ' ''= Dim strOurTillId As String = ""
    ''If Not mClsJmxPOS31_Licence.StartupTillCheck(mStrOurTillId) Then
    ''    txtSaleCustBarcode.Text = "NO TILL!"
    ''    txtSaleCustBarcode.ReadOnly = True
    ''    txtSaleStaffBarcode.ReadOnly = True
    ''Else  '--ok-
    ''    labSaleTillId.Text = "- Till-" & mStrOurTillId & " -"
    ''    MsgBox("Your PC is currently assigned to Till-" & mStrOurTillId, MsgBoxStyle.Information)
    ''End If  '-till check-

    ''-- only available here..
    ''== msDllversion = "JobMatixPOS Dll: v" & mClsJmxPOS31_Licence.GetDllVersion  '=341.0127=

    ''--TEST-
    ''== Dim frmFind1 As frmFindSerial
    ''== Dim sSerialNo As String = "ser-1960-1"
    ''== Dim mbIsRefund As Boolean = True
    ''== frmFind1 = New frmFindSerial(mCnnSql, sSerialNo, mbIsRefund)
    ''== frmFind1.ShowDialog()
    ''== MsgBox("Find completed..", MsgBoxStyle.Information)
    ''== frmFind1.Close()
    ''--END test=
    ''= btnSaleHold.Enabled = True
    'Me.KeyPreview = True   '- Tp catch F6 Hold..
    ''= btnSaleShowLastSale.Enabled = True

    'mbIsInitialising = False
    ''==3401.312= txtSaleCustBarcode.Select()
    'txtSaleStaffBarcode.Select()

    '= End Sub  '--Shown.-
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- form resized --

    Private Sub frmPOS3Main_Resize(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        If mbIsInitialising Then Exit Sub

        '= MsgBox("Child Form was resized.")

        '= If (Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized) Then
        '-- We don't want user to move the child form.
        'Me.Top = 0
        'Me.Left = 0

        '--  cant make smaller than original..-
        'If (Me.Height < mIntFormDesignHeight) Then Me.Height = mIntFormDesignHeight
        'If (Me.Width < mIntFormDesignWidth) Then Me.Width = mIntFormDesignWidth

        '= Call SubFormResized(1, 1)  '--dummy parms.

        '-  grpboxSale is just inside the Child Form.
        'grpboxSale.Width = Me.Width - 15  '= Me.Parent.Width - 15  '= TabControlPOS.Width - 15
        'grpboxSale.Height = Me.Height - 15  '= Me.Parent.Height - 15  '= TabControlPOS.Height - 33

        'panelSalesCurrentHdr.Left = grpboxSale.Width - panelSalesCurrentHdr.Width - 5

        'panelSaleHdr.Width = grpboxSale.Width - panelSalesCurrentHdr.Width - 11
        ''==picSaleItem.Left = panelSaleHdr.Width - picSaleItem.Width - 14
        'panelSaleLineEntry.Width = panelSaleHdr.Width  '= - panelPayment.Width - 5 '= grpboxSale.Width - 3
        'dgvSaleItems.Width = panelSaleLineEntry.Width  '= panelSaleHdr.Width  '= grpboxSale.Width - 3

        'dgvSaleItems.Height = _
        '    (grpboxSale.Height - panelSaleLineEntry.Height - panelSaleHdr.Height - panelSaleFooter.Height - 30) '= 65)

        'panelSaleFooter.Top = dgvSaleItems.Top + dgvSaleItems.Height + 3  '= grpboxSale.Height - panelSaleFooter.Height - 11
        'panelSaleFooter.Width = panelSaleLineEntry.Width   '= grpboxSale.Width - 7

        'panelSaleTotals.Left = panelSaleFooter.Width - panelSaleTotals.Width

        'panelCommit.Left = panelSalesCurrentHdr.Left
        'panelCommit.Top = grpboxSale.Height - panelCommit.Height - 5

        '= End If  '--window state--
    End Sub  '--resize-
    '= = = = = = = = = = = = = =
    '-===FF->

    '= 3411.0313=
    '-- catch ESCAPE for Sale form Cancel function..

    '- PreviewKeyDown is where you preview the key.
    '- Do not put any logic here, instead use the
    '- KeyDown event after setting IsInputKey to true.
    Private Sub frmPOS34Main_PreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs) _
                                            Handles Me.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Escape '= Keys.Down, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub  '-Me.PreviewKeyDown-
    '= = = = = = = = =  = = = = = =

    '-- FORM- Key Down..-
    '-- catch F6 and ESCape...-

    Private Sub frmPOS34Main_KeyDown(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                               Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        If (KeyCode = System.Windows.Forms.Keys.F6) And _
                        ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--Hold--
            '-- If Sale Tab is open, and Hold button if enabled..
            '- let it go if we're clicking on different page.
            ''- we're ON POS page if we're in JobMatix..
            'If LCase(TabControlPOS.SelectedTab.Name) = "tabpagesale" Then
            '    If btnSaleHold.Visible AndAlso btnSaleHold.Enabled Then
            '        Call btnSaleHold_Click(btnSaleHold, New System.EventArgs)
            '        eventArgs.Handled = True
            '    End If
            'End If '-sale page-
        ElseIf (KeyCode = System.Windows.Forms.Keys.Escape) And _
                    ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--ESCAPE: cancel--
            '-- If Sale Tab is open, and Sale instance enabled..
            '-- ESCape is used to Cancel the Sale..
            '=If (LCase(TabControlmain.SelectedTab.Name) = "tabpagesales") Then
            If mClsSale1.HasCurrentSale AndAlso _
                           btnCancelSale.Visible AndAlso btnCancelSale.Enabled Then
                Call btnCancel_Click(btnCancelSale, New System.EventArgs)
                eventArgs.Handled = True
            End If
            '=End If  '-sale page-
        ElseIf ((KeyCode = System.Windows.Forms.Keys.Z) And (eventArgs.Control)) Then '--Ctl-Z- Open Cash Drawer.-
            '-- Ctl-Z- Open Cash Drawer...
            '=3411.0402=

            '== ALSO!!  SEE MDI MOTHER-
            If mClsSale1 IsNot Nothing Then
                '-- Call function in Sale class.
                Call mClsSale1.OpenCashDrawer()
            End If '-nothing.
        End If  '--F6/ESCAPE-
    End Sub '--keyDown..-
    '= = = = = = = = = = == =
    '-===FF->

    '--EmailQueue--

    'Private Sub btnEmailQueue_Click(sender As Object, e As EventArgs)
    '    Dim frmEmailMain1 As frmEmailMain
    '    Try
    '        '--  load RAs Main form and show it..
    '        frmEmailMain1 = New frmEmailMain
    '    Catch ex As Exception
    '        MsgBox("Error in loading Emails Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '    End Try
    '    '--show-
    '    Try
    '        frmEmailMain1.connection = mCnnSql
    '        frmEmailMain1.SqlServer = msServer
    '        frmEmailMain1.DBname = msSqlDbName
    '        frmEmailMain1.colTables = mColSqlDBInfo
    '        frmEmailMain1.StaffBarcode = msStaffBarcodeSignedOn
    '        '= frmEmailMain1.IsAdminTest = bAdminTest
    '        frmEmailMain1.DllVersion = msDllversion  '=3411.0127=

    '        frmEmailMain1.ShowDialog()
    '    Catch ex As Exception
    '        MsgBox("Error in showing Email Main form." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
    '    End Try

    'End Sub  '-Email Queue--
    '= = = = = = = = = = = = == 
    '-===FF->

    '=3411.0208=--ShowLastSale-

    Private Sub btnSaleShowLastSale_Click(eventSender As Object, ev As EventArgs)

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.btnSaleShowLastSale_Click(eventSender, ev)

    End Sub  '-ShowLastSale-
    '= = = = = = = =  ==  = =

    '=3107.922- 

    '-- Now use command button to pop up list of invoices.

    Private Sub btnSaleSelectInvoice_Click(eventSender As Object, ev As EventArgs) _
                                                 Handles btnSaleSelectInvoice.Click
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.btnSaleSelectInvoice_Click(eventSender, ev)

    End Sub  '-select invoice-
    '= = = = = = = = = = = = = =

    '-- Now use command button to pop up list of invoices.
    '-show Quotes..
    Private Sub btnSaleSelectQuote_Click(eventSender As Object, ev As EventArgs) Handles btnSaleSelectQuote.Click

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.btnSaleSelectQuote_Click(eventSender, ev)

    End Sub  '-show Quotes..
    '= = = = = = = = = = = = = =  =

    Private Sub btnSaleSelectLayby_Click(eventSender As Object, ev As EventArgs) Handles btnSaleSelectLayby.Click

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.btnSaleSelectLayby_Click(eventSender, ev)

    End Sub  '-layby..
    '= = = = = = = = = = = == =  = =
    '-===FF->


    '=  3401.311= 
    '-- Multi-Sale- Swapping..
    '--  Hold- Restore- etc..
    '--  Hold- Restore- etc..
    '--   AND we have THREE Sale Instances..

    '-Service Function-
    'Private Function msGetCurrentHoldInfo() As String
    '    Dim sCustNameText() As String = Split(txtSaleCustName.Text, vbCrLf)
    '    Dim sName1 As String = "No name."
    '    If sCustNameText.Length > 0 Then
    '        sName1 = sCustNameText(0)  '-get first cust name line..
    '    End If
    '    msGetCurrentHoldInfo = labSaleStaffName.Text & _
    '                             " [" & txtSaleStaffBarcode.Text & "]" & vbCrLf & _
    '                               sName1 & " (" & txtSaleTotal.Text & ")."
    'End Function  '-msGetCurrentHoldInfo-
    '= = = = = = = = = = = = = = = = = = 

    '-All Sale swapping done HERE in MAIN form..

    '-- Hold-  Crashing FIXED in 3401.321==

    'Private Sub btnSaleHold_Click(sender As Object, e As EventArgs)
    '    If Not mClsSale1.HasCurrentSale Then
    '        '=(Trim(txtSaleStaffBarcode.Text) = "") And (Trim(txtSaleCustBarcode.Text) = "") Then
    '        MsgBox("No Sale to hold..", MsgBoxStyle.Exclamation)
    '        Exit Sub
    '    End If '-currently no trans..

    '    If (mColFreeSalesInstances.Count > 0) Then
    '        If Not btnSaleRestore1.Enabled Then  '--we can use slot1.-
    '            Call mClsSale1.SaveScreenContents()
    '            '- save instance as button-1 
    '            mClsSaleHeld1 = mClsSale1
    '            '-- get a free instance- 
    '            mClsSale1 = mColFreeSalesInstances(1)
    '            mColFreeSalesInstances.Remove(1)
    '            btnSaleRestore1.BackColor = Color.Plum
    '            btnSaleRestore1.Enabled = True
    '            '=3403.731= labHeld1Info.Text = msGetCurrentHoldInfo() '= txtSaleStaffBarcode.Text & vbCrLf & txtSaleCustName.Text
    '            btnSaleRestore1.Text = msGetCurrentHoldInfo()
    '            txtSaleStaffBarcode.Text = ""
    '            txtSaleCustBarcode.Text = ""
    '            '-- Clear screen and Clear mClsSale1 ready for new trans.
    '            mClsSale1.ClearCurrentTransaction()

    '        ElseIf Not btnSaleRestore2.Enabled Then  '--we can use slot2.-
    '            Call mClsSale1.SaveScreenContents()
    '            '- save instance as button-2
    '            mClsSaleHeld2 = mClsSale1
    '            btnSaleRestore2.BackColor = Color.Plum '= Color.Plum
    '            btnSaleRestore2.Enabled = True
    '            '= labHeld2Info.Text = msGetCurrentHoldInfo() '=  txtSaleStaffBarcode.Text & vbCrLf & txtSaleCustName.Text
    '            btnSaleRestore2.Text = msGetCurrentHoldInfo()
    '            '-- get a free instance-
    '            mClsSale1 = mColFreeSalesInstances(1)
    '            mColFreeSalesInstances.Remove(1)
    '            txtSaleStaffBarcode.Text = ""
    '            txtSaleCustBarcode.Text = ""
    '            '-- Clear screen and Clear mClsSale1 ready for new trans.
    '            mClsSale1.ClearCurrentTransaction()
    '        Else
    '            MsgBox("There are already two Held Transactions to deal with..", MsgBoxStyle.Exclamation)
    '        End If  '-restores-
    '    Else
    '        MsgBox("No free Sales Instances.", MsgBoxStyle.Exclamation)
    '    End If  '--free-
    'End Sub '-Hold-
    '= = = = = = = = = = = = = =

    '--Restore1-
    '-- Must have a sale in it if enabled.

    'Private Sub btnSaleRestore1_Click(sender As Object, e As EventArgs)
    '    If (Not mClsSale1.HasCurrentSale) Then
    '        '-- Current instance is free, so pop it back on free list.
    '        mColFreeSalesInstances.Add(mClsSale1)
    '        mClsSale1 = mClsSaleHeld1   '--get rest-1-
    '        mClsSale1.RestoreScreenContents() '-- show it all-
    '        '-- free up Restore-1
    '        btnSaleRestore1.BackColor = Color.LightGray
    '        btnSaleRestore1.Enabled = False
    '        mClsSaleHeld1 = Nothing
    '        '=3403.731= labHeld1Info.Text = ""
    '        btnSaleRestore1.Text = ""

    '    Else '-occupied..  send to Hold2 if vacant,
    '        If (Not btnSaleRestore2.Enabled) Then   '- ok is vacant-
    '            '-- hold current tranaction in Hold-2..
    '            Call mClsSale1.SaveScreenContents()
    '            '- save instance as button-2
    '            mClsSaleHeld2 = mClsSale1
    '            btnSaleRestore2.BackColor = Color.Plum
    '            btnSaleRestore2.Enabled = True
    '            '=3403.731= labHeld2Info.Text = msGetCurrentHoldInfo() '=  txtSaleStaffBarcode.Text & vbCrLf & txtSaleCustName.Text
    '            btnSaleRestore2.Text = msGetCurrentHoldInfo()
    '            mClsSale1 = mClsSaleHeld1   '--get rest-1-
    '            mClsSale1.RestoreScreenContents() '-- show it all-
    '            '-- free up Restore-1
    '            btnSaleRestore1.BackColor = Color.LightGray
    '            btnSaleRestore1.Enabled = False
    '            mClsSaleHeld1 = Nothing
    '            '=3403.731= labHeld1Info.Text = ""
    '            btnSaleRestore1.Text = ""

    '        Else  '-no room in 2..
    '            '-- have to swap Current transaction with Hold-1 slot..
    '            Dim strTempHoldInfo As String = msGetCurrentHoldInfo() '= Save Hold descrip.
    '            Call mClsSale1.SaveScreenContents()
    '            mClsSaleHeldTemp = mClsSale1   '--save current in temp hold.
    '            '--Make hold1 current.
    '            mClsSale1 = mClsSaleHeld1   '--get rest-1-
    '            mClsSale1.RestoreScreenContents() '-- show it all-
    '            '- save old cuurent in Hold-1-
    '            mClsSaleHeld1 = mClsSaleHeldTemp
    '            '=3403.731= labHeld1Info.Text = strTempHoldInfo
    '            btnSaleRestore1.Text = strTempHoldInfo

    '            '    MsgBox("House if full.." & vbCrLf & _
    '            '            "You'll need to deal with the current transaction..", MsgBoxStyle.Exclamation)
    '            '    Exit Sub
    '        End If  '-hold2
    '    End If  '-has current.
    'End Sub  '--Restore1-
    '= = = = = =  = = = = = = =

    '--Restore2-

    'Private Sub btnSaleRestore2_Click(sender As Object, e As EventArgs)
    '    '-- Must have a sale in it if enabled.
    '    If (Not mClsSale1.HasCurrentSale) Then
    '        '-- Current instance is free, so pop it back on free list.
    '        mColFreeSalesInstances.Add(mClsSale1)
    '        mClsSale1 = mClsSaleHeld2   '--get rest-2-
    '        mClsSale1.RestoreScreenContents() '-- show it all-
    '        '-- free up Restore-2
    '        btnSaleRestore2.BackColor = Color.LightGray
    '        btnSaleRestore2.Enabled = False
    '        mClsSaleHeld2 = Nothing
    '        '= 3403.731= labHeld2Info.Text = ""
    '        btnSaleRestore2.Text = ""

    '    Else '-occupied..  send to Hold1 if vacant,
    '        If (Not btnSaleRestore1.Enabled) Then   '- ok 1 is vacant-
    '            '-- hold current tranaction in Hold-2..
    '            Call mClsSale1.SaveScreenContents()
    '            '- save instance as button-1
    '            mClsSaleHeld1 = mClsSale1
    '            btnSaleRestore1.BackColor = Color.Plum
    '            btnSaleRestore1.Enabled = True
    '            '=3403.731= labHeld1Info.Text = msGetCurrentHoldInfo() '=  txtSaleStaffBarcode.Text & vbCrLf & txtSaleCustName.Text
    '            btnSaleRestore1.Text = msGetCurrentHoldInfo()
    '            '=3403.719=-
    '            '- MUST restore H2 to current.
    '            mClsSale1 = mClsSaleHeld2   '--get rest-2-
    '            mClsSale1.RestoreScreenContents() '-- show it all-
    '            '-- free up Restore-2
    '            btnSaleRestore2.BackColor = Color.LightGray
    '            btnSaleRestore2.Enabled = False
    '            mClsSaleHeld2 = Nothing
    '            '= 3403.731= labHeld2Info.Text = ""
    '            btnSaleRestore2.Text = ""

    '        Else  '-no room anyway..
    '            '-- have to swap Current transaction with Hold-2 slot..
    '            Dim strTempHoldInfo As String = msGetCurrentHoldInfo() '= Save Hold descrip.
    '            Call mClsSale1.SaveScreenContents()
    '            mClsSaleHeldTemp = mClsSale1   '--save current in temp hold.
    '            '--Make hold1 current.
    '            mClsSale1 = mClsSaleHeld2   '--get rest-2-
    '            mClsSale1.RestoreScreenContents() '-- show what was held-2-
    '            '- save old current in Hold-2-
    '            mClsSaleHeld2 = mClsSaleHeldTemp
    '            '=3403.731= labHeld2Info.Text = strTempHoldInfo
    '            btnSaleRestore2.Text = strTempHoldInfo
    '            '= MsgBox("House if full.." & vbCrLf & _
    '            '=         "You'll need to deal with the current transaction..", MsgBoxStyle.Exclamation)
    '            'Exit Sub
    '        End If  '-hold2
    '    End If  '-has current.
    'End Sub  '--Restore2-
    '= = = = = =  = ==
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  S a l e  --
    '--  S a l e  --
    '--  S a l e  --

    '= 3401.311- Staff ID neded for each Sale !!!

    '- STAFF barcode entry--
    '- STAFF barcode entry--

    '=3411,0313-  ENTER-
    '- save old barcode for updating parent.
    Private msSavedOldStaffBarcode As String = ""

    Private Sub txtSaleStaffBarcode_Enter(eventsender As Object, EventArgs As System.EventArgs) _
                                                                       Handles txtSaleStaffBarcode.Enter
        If mbIsInitialising Then Exit Sub
        msSavedOldStaffBarcode = Trim(txtSaleStaffBarcode.Text)
        Call mClsSale1.txtSaleStaffBarcode_Enter(eventsender, EventArgs)

    End Sub  '-txtSaleStaffBarcode_Enter-
    '= = = = = = = = = = =  = = = = = = =

    '-- Staff barcode. Enter was Pressed --

    Private Sub txtSaleStaffBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtSaleStaffBarcode.KeyPress
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.txtSaleStaffBarcode_KeyPress(eventSender, eventArgs)
        '-check if changed..
        If Trim(txtSaleStaffBarcode.Text) <> msSavedOldStaffBarcode Then
            '-update parent Main form for new signon.
            Dim intNewId As Integer
            Dim sNewBarcode, sNewName As String
            Call mClsSale1.GetStaffSignOn(intNewId, sNewBarcode, sNewName)
            If Not delChildSignedOn Is Nothing Then
                delChildSignedOn.Invoke(intNewId, sNewBarcode, sNewName)
            End If
        End If
    End Sub  '--STAFF keypress=
    '= = = = = = = = = = = = = = = 

    '-- STAFF barcode TEXTBOX- Validating --
    '==
    '==      STAFF Barcode- Must catch "Validating" event for TAB key. .- 

    Private Sub txtSaleStaffBarcode_Validating(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As CancelEventArgs) _
                                       Handles txtSaleStaffBarcode.Validating

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleStaffBarcode_Validating(eventSender, eventArgs)
        '- that's all-
    End Sub  '--Validating-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '--  S a l e  --
    '--  S a l e  --
    '--  S a l e  --

    '- Customer barcode entry--
    '- Customer barcode entry--

    Private Sub txtSaleCustBarcode_TextChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) Handles txtSaleCustBarcode.TextChanged
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.txtSaleCustBarcode_TextChanged(sender, e)

    End Sub  '--txtCustBarcode_TextChanged--
    '= = = = = = = = = = = = = = =  = = = 

    '-- CUSTOMER  Enter Pressed --

    Private Sub txtSaleCustBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtSaleCustBarcode.KeyPress
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleCustBarcode_KeyPress(eventSender, _
                                       eventArgs)
        Exit Sub

        '-- thats all-

    End Sub  '--CUST keypress=
    '= = = = = = = = = = = = = = = 

    '-- Customer Search (F2)..--
    '-- barcode TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for Cust Lookup--

    Private Sub txtSaleCustBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                       Handles txtSaleCustBarcode.KeyDown

        Call mClsSale1.txtSaleCustBarcode_KeyDown(eventSender, eventArgs)
        '- that's all-
    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 

    '-- Customer barcode TEXTBOX- Validating --
    '==
    '==     v3.3.3300.1227..  27-Dec-2016= ===
    '==       >> Cust Barcode- Must catch "Validating" event for TAB key. .- 

    Private Sub txtSaleCustBarcode_Validating(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As CancelEventArgs) _
                                       Handles txtSaleCustBarcode.Validating

        Call mClsSale1.txtSaleCustBarcode_Validating(eventSender, eventArgs)
        '- that's all-
    End Sub  '--Validating-
    '= = = = = = = = = = = = = = = 

    '-- Validated event caught only to update main form Tab text..
    Private Sub txtSaleCustBarcode_Validated(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) _
                                       Handles txtSaleCustBarcode.Validated
        If (txtSaleCustName.Text <> "") Then
            If Not (Me.delReport Is Nothing) Then
                '=TEMP out-  delReport.Invoke(Me.Name, "UpdateTabText", VB.Left(txtSaleCustName.Text, 16))
            End If
            '=3501.0716-  Update Tab Text.
            Call mSubUpdateTabText()
        End If
    End Sub ''-- validated.
    '= = = = = = = = = = =  =
    '-===FF->

    '-- Job Search (F2)..--
    '-- JobNo TEXTBOX- Catch F2 on  --
    '--- check for F2 for JobNo Lookup--

    Private Sub txtSaleJobNo_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                       Handles txtSaleJobNo.KeyDown

        Call mClsSale1.txtSaleJobNo_KeyDown(eventSender, eventArgs)
        '- that's all-


    End Sub  '-- Job key down-
    '= = = = = = = = = = = = = = = 

    '-- Catch ENTER key on Job No..-

    Public Sub txtSaleJobNo_KeyPress(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                         Handles txtSaleJobNo.KeyPress
        Call mClsSale1.txtSaleJobNo_KeyPress(eventSender, eventArgs)
        '- that's all-

    End Sub  '-enter-
    '= = = = = = = = = = = = = = = 
    '-===FF->


    '--  S a l e  --
    '--  S a l e  --
    '--  S a l e  --

    '-- click to lookup (BROWSE) cust..--

    '-- Button Gone --
    '-- Button Gone --

    'Private Sub btnSaleLookupCust_Click(ByVal sender As System.Object, _
    '                                      ByVal e As System.EventArgs)
    '    If mbIsInitialising Then Exit Sub

    '    Call mClsSale1.btnSaleLookupCust_Click(sender, e)

    '    Exit Sub

    '    '-- thats all-
    'End Sub  '--get all customers-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- Option for Tran-Type--
    '-- SALE is Default..  just have buttons for Refund and Quote..

    '== Private Sub optSale_CheckedChanged(ByVal sender As System.Object, _
    '==                                     ByVal e As System.EventArgs)

    '==If mbIsInitialising Then Exit Sub
    '== Call mClsSale1.optSale_CheckedChanged(sender, e)
    '== End Sub  '-optSale-
    '= = = =  = = = = = = = 

    '-'-optSaleRefundOrQuote--

    'Private Sub optSaleRefundOrQuote_CheckedChanged(ByVal sender As System.Object, _
    '                                           ByVal ev As System.EventArgs)

    '    If mbIsInitialising Then Exit Sub
    '    Call mClsSale1.optSaleRefundOrQuote_CheckedChanged(sender, ev)

    'End Sub  '-optSaleRefundOrQuote-
    ''= = = = = = = = = = = = = = = 

    '=3519.0330=
    '--  Import Quote..
    '-- Lookup Quotes for this customer and choose one.

    'Private Sub btnImportQuote_Click(sender As Object, ev As EventArgs)

    '    If mbIsInitialising Then Exit Sub
    '    Call mClsSale1.btnImportQuote_Click(sender, ev)
    'End Sub  '-btnImportQuote_Click-
    '= = = = = = = = = =  = == = = =

    '- Is label so it doesn't respond to ENTER key.

    Private Sub labImportQuote_Click(sender As Object, ev As EventArgs) Handles labImportQuote.Click
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.labImportQuote_Click(sender, ev)

    End Sub  '- labImportQuote_Click=
    '= = = = = = = = = = = = = = = = = 

    '= 3401.321=
    '-- cboTransaction_SelectedIndexChanged-

    Private Sub cboTransaction_SelectedIndexChanged(sender As Object, _
                                                    ev As EventArgs)
    End Sub  '-cboTransaction_SelectedIndexChanged-
    '= = = = = = = = = = =  = = = = = = = ==  == = 

    '=3411.0311-  RESTORED radio buttons..
    '= 3501.0731-  Update Tab text..

    Private Sub optSaleRefundOrQuote_CheckedChanged(sender As Object, ev As EventArgs) _
                                            Handles optSaleSale.CheckedChanged, optSaleQuote.CheckedChanged, _
                                                 optSaleLayby.CheckedChanged, optSaleRefund.CheckedChanged
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.optSaleRefundOrQuote_CheckedChanged(sender, ev)

        '=3501.0716-  Update Tab Text.
        Call mSubUpdateTabText()

        'Dim thisTabPage As TabPage = CType(Me.Parent, TabPage)
        'thisTabPage.Text = labSaleTranType.Text & ": " & _
        '                     VB.Left(Replace(txtSaleCustName.Text, vbCrLf, "; "), 16)
        'thisTabPage.ToolTipText = labSaleTranType.Text & ": " & VB.Left(txtSaleCustName.Text, 16)
        'Me.Text = thisTabPage.Text

    End Sub '-'-- Sale etc option clicked..-
    '= = = = = = = = = = = = = = = = = = = =

    '-- catch ENTER on RadioButtons..

    Private Sub optSaleRefundOrQuote_keyPress(sender As Object, ev As KeyPressEventArgs) _
                                               Handles optSaleSale.KeyPress, optSaleRefund.KeyPress, _
                                                                optSaleQuote.KeyPress, optSaleLayby.KeyPress
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.optSaleRefundOrQuote_keyPress(sender, ev)
        If (txtSaleCustName.Text <> "") Then
            '=3501.0716-  Update Tab Text.
            Call mSubUpdateTabText()
        End If
    End Sub  '- optSaleRefundOrQuote_keyPress-
    '= = = = = = = = = = = = = = = = = = = = 

    '-- opSale Etc.- Catch TAB key, make it into ENTER. --
    '-- Catch TAB key-

    ' PreviewKeyDown is where you preview the key.
    ' Do not put any logic here, instead use the
    ' KeyDown event after setting IsInputKey to true.

    Private Sub optSaleRefundOrQuote_PreviewKeyDown(ByVal sender As Object, _
                                               ByVal e As PreviewKeyDownEventArgs) _
                                             Handles optSaleSale.PreviewKeyDown, optSaleRefund.PreviewKeyDown, _
                                                                optSaleQuote.PreviewKeyDown, optSaleLayby.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Tab     '= Keys.Down, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub '-PreviewKeyDown-
    '= = = = = = = = = = == =

    Private Sub optSaleRefundOrQuote_KeyDown(ByVal sender As Object, _
                                           ByVal EventArgs As KeyEventArgs) _
                                         Handles optSaleSale.KeyDown, optSaleRefund.KeyDown, _
                                                            optSaleQuote.KeyDown, optSaleLayby.KeyDown
        Dim KeyCode As Short = EventArgs.KeyCode
        Dim Shift As Short = EventArgs.KeyData \ &H10000

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.optSaleRefundOrQuote_KeyDown(sender, EventArgs)


    End Sub '-PreviewKeyDown-
    '= = = = = = = = = = == =
    '-===FF->


    '--btnSaleContinue-
    '-- Opt to Select Transaction done..
    '==  Go to Grid data entry..

    'Private Sub btnSaleContinue_Click(sender As Object, ev As EventArgs)

    '    If mbIsInitialising Then Exit Sub
    '    Call mClsSale1.btnSaleContinue_Click(sender, ev)

    'End Sub  '--btnSaleContinue-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-==3300.516=  All items now done in same line of text boxes.
    '-==3300.516=  All items now done in same line of text boxes.
    '-==3300.516=  All items now done in same line of text boxes.

    '-- Textbox Enter control for Item barcode.

    Private Sub txtSaleItemBarcode_Enter(sender As Object, ev As System.EventArgs) Handles txtSaleItemBarcode.Enter
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleItemBarcode_Enter(sender, ev)

    End Sub '-txtSaleItemBarcode_Enter-

    '==-- 15-Apr-2018- POS Sale- Catch Mouse-Click on Item Barcode to set Selection stuff....

    Private Sub txtSaleItemBarcode_Click(sender As Object, ev As System.EventArgs) Handles txtSaleItemBarcode.Click

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleItemBarcode_Click(sender, ev)

    End Sub  '-txtSaleItemBarcode_Click-
    '= = = = = = = = = = = = = = = = = =

    '-- Stock Item Search (F2)..--
    '-- Stock Item Search (F2)..--
    '-- Stock Item Search (F2)..--

    '-- Grid TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for STOCK Lookup--

    Private Sub txtSaleItemBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                       Handles txtSaleItemBarcode.KeyDown

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleItemBarcode_KeyDown(eventSender, eventArgs)
        '- that's all-

    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 
    '-===FF->


    '=3300.519= 
    '--  Catch TAB key on all Edit Lines textboxes.
    '-    and force it to be seen in KeyDown even..
    '==   So we navigate via ENTER key only.-
    '==   NOT USED =
    'Private Sub txtSaleItemBarcode_PreviewKeyDown(ByVal sender As Object, _
    '                                      ByVal ev As System.Windows.Forms.PreviewKeyDownEventArgs) _
    '                                        Handles txtSaleItemBarcode.PreviewKeyDown, _
    '                                                  txtSaleItemSerialNo.PreviewKeyDown,
    '                                                           txtSaleItemCategory.PreviewKeyDown, _
    '                                                             txtSaleItemDescription.PreviewKeyDown, _
    '                                                               txtSaleItemSellPrice.PreviewKeyDown, _
    '                                                                 txtSaleItemQty.TextChanged, _
    '                                                                   txtSaleItemExtension.PreviewKeyDown
    '    If ev.KeyCode = Keys.Tab Then
    '        ev.IsInputKey = True
    '    End If
    'End Sub  '- txtSaleItemBarcode_PreviewKeyDown-
    '= = = = = = = = = = = = = = = =
    '-===FF->


    Private Sub txtSaleItemBarcode_TextChanged(sender As Object, ev As EventArgs) _
                                                   Handles txtSaleItemBarcode.TextChanged
        'txtSaleItemSerialNo.TextChanged,
        '  txtSaleItemCategory.TextChanged, _
        '    txtSaleItemDescription.TextChanged, _
        '      txtSaleItemSellPrice.TextChanged, _
        '        txtSaleItemQty.TextChanged, _
        '          txtSaleItemExtension.TextChanged
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleItemBarcode_TextChanged(sender, ev)

    End Sub  '-txtSaleItemBarcode-
    '= = = = = = = = = = = = == = == =

    '-- SERIAL Textbox ENTER..
    '-- Textbox Enter control for Item SerialNo.

    Private Sub txtSaleItemSerialNo_Enter(eventSender As Object, _
                                           EventArgs As System.EventArgs) Handles txtSaleItemSerialNo.Enter
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleItemSerialNo_Enter(eventSender, EventArgs)

    End Sub '- txtSaleItemSerialNo_Enter-
    '= = = = = = = = =  = = = = = = = = =

    '-- Handle ENTER for all Line Item textboxes..
    '--   txtSaleItemBarcode-  Enter Pressed --

    Private Sub txtSaleItemBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtSaleItemBarcode.KeyPress, _
                                                     txtSaleItemSerialNo.KeyPress, _
                                                         txtSaleItemDescription.KeyPress, _
                                                          txtSaleItemSellPrice.KeyPress, _
                                                           txtSaleItemQty.KeyPress, _
                                                            txtSaleItemExtension.KeyPress
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleItemBarcode_KeyPress(eventSender, eventArgs)

    End Sub  '-txtSaleItemBarcode_KeyPress-
    '= = = = = = = =  = = = = = = = = == == 
    '-===FF->

    '-- Handle Validating for all Line Item textboxes..

    Private Sub txtSaleItemBarcode_Validating(ByVal sender As System.Object, _
                                              ByVal ev As CancelEventArgs) _
                                                 Handles txtSaleItemBarcode.Validating, _
                                                 txtSaleItemSerialNo.Validating, _
                                                 txtSaleItemDescription.Validating, _
                                                 txtSaleItemSellPrice.Validating, _
                                                 txtSaleItemQty.Validating, _
                                                 txtSaleItemExtension.Validating
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleItemBarcode_Validating(sender, ev)

    End Sub  '--txtSaleItemBarcode_Validating-
    '= = = = = = = = = = = = = = = = = = = == 

    '==3307.0218 =
    '-- Handle txtSaleItemBarcode VALIDATED Event for all Line Item textboxes..

    Private Sub txtSaleItemBarcode_Validated(ByVal sender As System.Object, _
                                              ByVal ev As System.EventArgs) _
                                                 Handles txtSaleItemBarcode.Validated, _
                                                 txtSaleItemSerialNo.Validated, _
                                                 txtSaleItemDescription.Validated, _
                                                 txtSaleItemSellPrice.Validated, _
                                                 txtSaleItemQty.Validated, _
                                                 txtSaleItemExtension.Validated
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtSaleItemBarcode_Validated(sender, ev)

    End Sub  '--txtSaleItemBarcode_Validated-
    '= = = = = = = = = = = = = = = = = = = ==  = = = = = = 
    '-===FF->

    '-- "OK" to finish Item Edit..

    Private Sub btnSaleLineOk_Click(sender As Object, ev As EventArgs) Handles btnSaleLineOk.Click

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.btnSaleLineOk_Click(sender, ev)
    End Sub  '-btnSaleLineOk-
    '= = = = = = = = = = = = = = 

    '- CLEAR the current item line texts..

    '--btnItemLineClear_Click--

    Private Sub btnItemLineClear_Click(sender As Object, ev As EventArgs) _
                                                              Handles btnSaleItemLineClear.Click, _
                                                                         btnSaleItemLineClear2.Click

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.btnItemLineClear_Click(sender, ev)

    End Sub  '-- btnItemLineClear_Click-
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '- Invoices listView..
    '-dgvSaleInvoices-

    '- NOW GONE..-
    'Private Sub dgvSaleInvoices_DoubleClick(ByVal eventSender As System.Object, _
    '                                               ByVal ev As System.EventArgs)
    '    If mbIsInitialising Then Exit Sub
    '    '== Call mClsSale1.dgvSaleInvoices_DoubleClick(eventSender, ev)
    '    Exit Sub
    'End Sub  '-dgvSaleInvoices_DoubleClick-
    '= = = = = = = = = = = =  = = = = = = = = = =

    '-- SALE data grid events..--
    '-- SALE data grid events..--
    '-- SALE  data grid events..--

    '-- Enter Row..  update picture.-

    Private Sub dgvSaleItems_RowEnter(ByVal sender As Object, _
                                         ByVal ev As DataGridViewCellEventArgs) _
                                                      Handles dgvSaleItems.RowEnter
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvSaleItems_RowEnter(sender, ev)

        Exit Sub
        '-thats all-

    End Sub  '--row enter-
    '= = = = = = = =  == = = =

    '-UserDeletingRow-

    Private Sub dgvSaleItems_UserDeletingRow(ByVal sender As Object, _
                                            ByVal ev As DataGridViewRowCancelEventArgs) _
                                             Handles dgvSaleItems.UserDeletingRow

        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvSaleItems_UserDeletingRow(sender, ev)

    End Sub  '-UserDeletingRow-
    '= = = = = = = = = = = = = = = = =

    '-User Deleted Row-  DONE-
    '-  (via DELETE key..)

    Private Sub dgvSaleItems_UserDeletedRow(ByVal sender As Object, _
                                             ByVal ev As DataGridViewRowEventArgs) _
                                            Handles dgvSaleItems.UserDeletedRow
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvSaleItems_UserDeletedRow(sender, ev)

    End Sub  '-User Deleted Row- 
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Catch Delete Button for Row..-

    Private Sub dgvSaleItems_CellContentClick(ByVal sender As Object, _
                                                ByVal ev As DataGridViewCellEventArgs) _
                                               Handles dgvSaleItems.CellContentClick

        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvSaleItems_CellContentClick(sender, ev)


    End Sub '=dgvSaleItems_CellContentClick=
    '= = = = = = = = = = = = =  =

    '--mouse activity---
    '-- Shows Popup menu to DELETE selected row..-
    Private Sub dgvSaleItems_CellMouseUp(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                            Handles dgvSaleItems.CellMouseUp
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvSaleItems_CellMouseUp(eventSender, eventArgs)

    End Sub '-CellMouseUp-
    '= = = = = = = = = = = = ==

    '-- DataGrid Item Barcode Textbox stuff.--

    '-  catch barcode change event..--
    '-- cell change.--
    '== REDUNDANT ???  --

    Private Sub dgvSaleItems_CellValueChanged(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellEventArgs) _
                                                            Handles dgvSaleItems.CellValueChanged
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvSaleItems_CellValueChanged(eventSender, eventArgs)

        Exit Sub

        '-- thats all-

    End Sub '= CellValueChangedEvent--
    '= = = = = = = = = = = = = = = = = 
    '= = = = = = = = = = == =
    '-===FF->

    '-- Textbox control has been activated on a cell.-
    '--  set event handlers to deal with the textbox..

    '-- to catch keypress...

    Private Sub dgvSaleItems_EditingControlShowing(ByVal sender As Object, _
                                                    ByVal e As DataGridViewEditingControlShowingEventArgs) _
                                              Handles dgvSaleItems.EditingControlShowing
        If mbIsInitialising Then Exit Sub

        Dim text1 As TextBox = CType(e.Control, TextBox)
        If (text1 IsNot Nothing) Then
            '-- Remove an existing event-handler, if present, to avoid 
            '-- adding multiple handlers when the editing control is reused.
            RemoveHandler text1.KeyDown, _
                New KeyEventHandler(AddressOf dgvSaleItems_KeyDown)
            '= RemoveHandler text1.KeyPress, _
            '=     New KeyPressEventHandler(AddressOf dgvSaleItems_KeyPress)
            '= RemoveHandler text1.DoubleClick, _
            '==     New EventHandler(AddressOf dgvSaleItems_DoubleClick)
        End If
        '-- Add the event handler. 
        AddHandler text1.KeyDown, _
             New KeyEventHandler(AddressOf dgvSaleItems_KeyDown)
        '== AddHandler text1.KeyPress, _
        '==     New KeyPressEventHandler(AddressOf dgvSaleItems_KeyPress)
        '== AddHandler text1.DoubleClick, _
        '==             New EventHandler(AddressOf dgvSaleItems_DoubleClick)
    End Sub  '--EditingControlShowing-
    '= = = = = = =  = = = == = = = 

    '-- Grid TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for STOCK Lookup--

    Private Sub dgvSaleItems_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) '= Handles txtPartNo.KeyDown

        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvSaleItems_KeyDown(eventSender, eventArgs)
        Exit Sub

        '-thats all-

    End Sub '--keydown.-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--- SaleItems- C e l l  V a l i d a t i n g--=  
    '--- SaleItems- C e l l  V a l i d a t i n g--=  

    Private Sub dgvSaleItems_CellValidating(ByVal sender As Object, _
                                                 ByVal ev As DataGridViewCellValidatingEventArgs) _
                                                  Handles dgvSaleItems.CellValidating
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.dgvSaleItems_CellValidating(sender, ev)
        Exit Sub

        '--thats all--

    End Sub  '--cell validating.--
    '= = = = = = = = = = == =

    Private Sub dgvSaleItems_CellEndEdit(ByVal sender As Object, _
                                         ByVal ev As System.Windows.Forms.DataGridViewCellEventArgs) _
                                           Handles dgvSaleItems.CellEndEdit

        ' Clear the row error in case the user presses ESC.   
        '== dgvSaleItems.Rows(e.RowIndex).ErrorText = String.Empty
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.dgvSaleItems_CellEndEdit(sender, ev)
        Exit Sub

    End Sub  '--CellEndEdit--
    '= = = = = = = = = = = = = = = ==

    '-- validate Row..-
    '-- Must call from ACTUAL event sub in POS class !!--

    Private Sub dgvSaleItems_RowValidating(ByVal sender As Object, _
                                            ByVal ev As DataGridViewCellCancelEventArgs) _
                                             Handles dgvSaleItems.RowValidating
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.dgvSaleItems_RowValidating(sender, ev)
        Exit Sub

    End Sub '-RowValidating-
    '= = = = = = = = = = = = = = = =

    '-===FF->

    '--  Double-click.--  ?? -

    Private Sub dgvSaleItems_DoubleClick(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As EventArgs)
        If mbIsInitialising Then Exit Sub
        '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
    End Sub '--dbl-click--
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '--chkOnAccount_CheckedChanged-

    '=3403.1014=

    Private Sub chkOnAccount_CheckedChanged(sender As Object, ev As EventArgs) _
                                            Handles chkOnAccount.CheckedChanged, _
                                                     chkOnAccount2.CheckedChanged

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.chkOnAccount_CheckedChanged(sender, ev)

    End Sub  '-chkOnAccount_CheckedChanged-
    '= = = = = = = =  == = =  == = = = = = = = = = = = = =


    '= 4201.2131.-chkOnAccount catch TAB key..-
    '-- ONLY for TOP Checkbox..

    Private Sub chkOnAccount2_PreviewKeyDown(ByVal sender As Object, _
                                            ByVal ev As PreviewKeyDownEventArgs) _
                                               Handles chkOnAccount2.PreviewKeyDown
        If mbIsInitialising Then Exit Sub
        Select Case (ev.KeyCode)
            Case Keys.Tab  '= Keys.Enter  '= Keys.Down, Keys.Up
                ev.IsInputKey = True
        End Select
    End Sub '- chkOnAccount_PreviewKeyDown-
    '= = = = = = = = = = = = = = = == = = =

    '- For KeyDown..  Call the handler class.

    Private Sub chkOnAccount2_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles chkOnAccount2.KeyDown
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.chkOnAccount2_KeyDown(eventSender, eventArgs)

    End Sub '- chkOnAccount_KeyDown=
    '= = = = = = = = == = = = = = = = = =

    '-chkOnAccount_KeyPress-

    Private Sub chkOnAccount_KeyPress(ByVal sender As Object, _
                                   ByVal ev As System.Windows.Forms.KeyPressEventArgs) _
                               Handles chkOnAccount.KeyPress, chkOnAccount2.KeyPress
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.chkOnAccount_KeyPress(sender, ev)
    End Sub  '-chkOnAccount_KeyPress-
    '= = = = = = = = = = = = = ==  = = = === ====== 


    '-- optRefundCash_CheckedChanged--

    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '== 
    '==  MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
    '==       --  Involves creating a REFUND DETAILS EXTRA Option (Other) (radioButton)
    '==                   to be able to refund same types as Payments..
    '==                 ie dropdown including ZipPay, bank Deposit etc..


    Private Sub optRefundCash_CheckedChanged(sender As Object, ev As EventArgs) _
                                                     Handles optRefundCash.CheckedChanged, _
                                                              optRefundCredit.CheckedChanged, _
                                                              optRefundEftPosCr.CheckedChanged, _
                                                              optRefundEftPosDr.CheckedChanged, _
                                                              optRefundOther.CheckedChanged
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.optRefundCash_CheckedChanged(sender, ev)

    End Sub  '--optRefundCash_CheckedChanged-
    '= = = = = = = =  = = = = = == = = = = = =

    '--cboRefundOtherDetails_SelectedIndexChanged-

    Private Sub cboRefundOtherDetails_SelectedIndexChanged(sender As Object, _
                                                           ev As EventArgs) _
                                                       Handles cboRefundOtherDetails.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.cboRefundOtherDetails_SelectedIndexChanged(sender, ev)

    End Sub  '-cboRefundOtherDetails_SelectedIndexChanged-
    '= = = = = = = == == = = = = = = = = = = = = = = = = =

    '==END OF   Target is new Build 4251..
    '==END OF   Target is new Build 4251..


    '--cboSaleDiscountPercent_SelectedIndexChanged-

    Private Sub cboSaleDiscountPercent_SelectedIndexChanged(ByVal sender As System.Object, _
                                                       ByVal ev As System.EventArgs) _
                                                        Handles cboSaleDiscountPercent.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        '-test msgbox-
        'MsgBox("Combo SelectedIndexChanged- " & vbCrLf & _
        '"Selected Item is: " & cboSaleDiscountPercent.SelectedItem, MsgBoxStyle.Information)

        Call mClsSale1.cboSaleDiscountPercent_SelectedIndexChanged(sender, ev)

    End Sub  '--cboSaleDiscountPercent_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '=3411.0404=
    '-- Must catch ENTER..
    '== NO! it stuffs up everything..

    'Private Sub cboSaleDiscountPercent_keyPress(ByVal sender As System.Object, _
    '                                                         ByVal ev As System.Windows.Forms.KeyPressEventArgs) _
    '                                            Handles cboSaleDiscountPercent.KeyPress
    '    If mbIsInitialising Then Exit Sub
    '    Call cboSaleDiscountPercent_keyPress(sender, ev)
    'End Sub  '-cboSaleDiscountPercent-keypress-
    '= = = = = = = = = = =  = = = == =  = = ==

    '=3519.0119=
    '- --TRY THIS catch ENTER key for TAB function..

    '-- cboSaleDiscountPercent_PreviewKeyDown

    '- PreviewKeyDown is where you preview the key.
    '- Do not put any logic here, instead use the
    '- KeyDown event after setting IsInputKey to true.
    Private Sub cboSaleDiscountPercent_PreviewKeyDown(ByVal sender As Object, _
                                                      ByVal ev As PreviewKeyDownEventArgs) _
                                                        Handles cboSaleDiscountPercent.PreviewKeyDown
        If mbIsInitialising Then Exit Sub
        Select Case (ev.KeyCode)
            Case Keys.Enter  '= Keys.Down, Keys.Up
                ev.IsInputKey = True
        End Select
    End Sub '-cboSaleDiscountPercent_PreviewKeyDown'-

    '-- Combo- Key Down..-
    '-- catch ENTER...-

    Private Sub cboSaleDiscountPercent_KeyDown(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                      Handles cboSaleDiscountPercent.KeyDown
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.cboSaleDiscountPercent_KeyDown(eventSender, eventArgs)

    End Sub  '-cboSaleDiscountPercent_KeyDown-
    '= = = = = = = = = = = = = = = = == = = = = = =


    '-- DISCOUNT-  Enter Pressed --

    Private Sub txtSaleDiscountCashout_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtSaleDiscount.KeyPress
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.txtSaleDiscountCashout_KeyPress(eventSender, eventArgs)
        Exit Sub

        '--thats all-

    End Sub  '--txtSaleDiscountCashout_KeyPress-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->


    Private Sub txtSaleDiscountCashout_Validating(ByVal sender As System.Object, _
                                               ByVal ev As CancelEventArgs) _
                                                  Handles txtSaleDiscount.Validating
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.txtSaleDiscountCashout_Validating(sender, ev)

        Exit Sub
        '--thats all-

    End Sub  '--discount-
    '= = = = = = = = = = = =

    Private Sub txtSaleDiscountCashout_Validated(ByVal sender As System.Object, _
                                               ByVal ev As System.EventArgs) _
                                                  Handles txtSaleDiscount.Validated
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.txtSaleDiscountCashout_Validated(sender, ev)

        Exit Sub
    End Sub  '--discount-
    '= = = = = = = = = = = =
    '-===FF->

    '--- P a y m e n t s --
    '--- P a y m e n t s --
    '==
    '==   Updated.- 3519.0317 
    '==    -- MAJOR-  Add TextBox to Payments panel- 
    '==            for User to decide on Amount of CreditNote to withdraw to pay fpr Sale.
    '==
    Private Sub txtCreditNoteWdl_Enter(sender As Object, _
                                             ev As EventArgs) Handles txtCreditNoteWdl.Enter
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtCreditNoteWdl_Enter(sender, ev)

    End Sub  '-enter-
    '= = = = = = = = = = =

    '-txtCreditNoteWdl_TextChanged-

    Private Sub txtCreditNoteWdl_TextChanged(sender As Object, _
                                             ev As EventArgs) Handles txtCreditNoteWdl.TextChanged
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtCreditNoteWdl_TextChanged(sender, ev)

    End Sub  '-txtCreditNoteWdl_TextChanged-
    '= = = = = = =  = = = = = = = = = == =

    Private Sub txtCreditNoteWdl_KeyPress(sender As Object, _
                                         ev As KeyPressEventArgs) Handles txtCreditNoteWdl.KeyPress
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtCreditNoteWdl_KeyPress(sender, ev)

    End Sub  '-txtCreditNoteWdl_keypress.-
    '= = = = = = =  = = = = = = = = = == =

    Private Sub txtCreditNoteWdl_Validating(sender As Object, _
                                             ev As CancelEventArgs) Handles txtCreditNoteWdl.Validating
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtCreditNoteWdl_Validating(sender, ev)

    End Sub  '-txtCreditNoteWdl_validating.-
    '= = = = = = =  = = = = = = = = = == =

    Private Sub txtCreditNoteWdl_validated(sender As Object, _
                                             ev As EventArgs) Handles txtCreditNoteWdl.Validated
        If mbIsInitialising Then Exit Sub
        Call mClsSale1.txtCreditNoteWdl_validated(sender, ev)

    End Sub  '-txtCreditNoteWdl_validated-
    '= = = = = = =  = = = = = = = = = == =
    '-===FF->

    '=3501.0824= 

    '--- P a y m e n t s --
    '--- P a y m e n t s --

    '- catch enter and F2 etc and pass to keydown.

    Private Sub dgvSalePaymentDetails_PreviewKeyDown(sender As DataGridView, ev As PreviewKeyDownEventArgs) _
                                                                    Handles dgvSalePaymentDetails.PreviewKeyDown
        If (ev.KeyCode = Keys.Enter) Or _
                   (ev.KeyCode = Keys.Back) Or (ev.KeyCode = Keys.Right) Then
            ev.IsInputKey = True
        End If
    End Sub  '-dgvSalePaymentDetails_PreviewKeyDown-
    '= = = = = = =  = = = = = = = = = = = = = = = = 

    Private Sub dgvSalePaymentDetails_KeyDown(sender As DataGridView, ev As KeyEventArgs) Handles dgvSalePaymentDetails.KeyDown

        Dim bEnterHandled As Boolean = False

        If (ev.KeyData = Keys.Enter) Then
            '= MsgBox("Enter pressed..", MsgBoxStyle.Information)
            '= ev.Handled = True
            Call mClsSale1.dgvSalePaymentDetails_KeyDown_EnterKey(sender, ev, bEnterHandled)
        ElseIf (ev.KeyData = Keys.Back) Or _
                      (ev.KeyData = Keys.Right) Or (ev.KeyData = Keys.F2) Then
            '- start edit..
            '== Call mClsSale1.dgvSalePaymentDetails_KeyDown_EditingStartKey(sender, ev)

            '- convert to F2 if needed (for the control)-
            '-- Let the control see it..
            '= If (ev.KeyData <> Keys.F2) Then SendKeys.Send("{F2}") '=Force start of editing with F2..
        End If
        ev.Handled = bEnterHandled

    End Sub  '--key down.
    '= = = = = = = = = = = = = 

    '--Editing started..  Load current balance if needed.

    Private Sub dgvSalePaymentDetails_EditingControlShowing(ByVal sender As Object, _
                                                    ByVal ev As DataGridViewEditingControlShowingEventArgs) _
                                              Handles dgvSalePaymentDetails.EditingControlShowing
        Dim text1 As DataGridViewTextBoxEditingControl = CType(ev.Control, DataGridViewTextBoxEditingControl)
        '-- load balance data if needed..
        '= Call mClsSale1.dgvSalePaymentDetails_EditingControlShowing(sender, ev)
        '-- set up to catch Enter event..
        If (text1 IsNot Nothing) Then
            '-- Remove an existing event-handler, if present, to avoid 
            '-- adding multiple handlers when the editing control is reused.
            RemoveHandler text1.Enter, _
                New EventHandler(AddressOf dgvSalePaymentDetails_EditingControlEnter)
        End If
        '-- Add the event handler. 
        AddHandler text1.Enter, _
             New EventHandler(AddressOf dgvSalePaymentDetails_EditingControlEnter)

    End Sub  '- dgvSalePaymentDetails_EditingControlShowing-
    '= = = = = = = =  = = = = = = = = = = = = = = = ===  = =

    '-- Grid TEXT box showing-  Enter-
    '--  This Editing textbox event captured to select the text.

    Private Sub dgvSalePaymentDetails_EditingControlEnter(eventsender As Object, EventArgs As System.EventArgs)
        Dim text1 As DataGridViewTextBoxEditingControl = CType(eventsender, DataGridViewTextBoxEditingControl)

        Dim bHasFocus As Boolean = text1.Focused
        '= MsgBox("Textbox Enter Event- Text= " & text1.Text & vbCrLf & "Has Focus = " & bHasFocus)

        '== Call mClsSale1.dgvSalePaymentDetails_EditingControlEnter(eventsender, EventArgs)

    End Sub  '-EditingControlEnter-
    '= = = = = = = = = =  = = ==  =


    '-PaymentDetails_CellEnter-

    Private Sub dgvSalePaymentDetails_CellEnter(sender As Object, _
                                                     ev As DataGridViewCellEventArgs) _
                                                              Handles dgvSalePaymentDetails.CellEnter
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvSalePaymentDetails_CellEnter(sender, ev)

        'If ev.ColumnIndex = 0 Or ev.ColumnIndex = 5 Then
        '    SendKeys.Send("{TAB}")
        'End If

    End Sub

    '--- Payments-- C e l l  V a l i d a t i n g--=  
    '--- Payments-- C e l l  V a l i d a t i n g--=  

    Private Sub dgvPaymentDetails_CellValidating(ByVal sender As Object, _
                                                 ByVal ev As DataGridViewCellValidatingEventArgs) _
                                                  Handles dgvSalePaymentDetails.CellValidating
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvPaymentDetails_CellValidating(sender, ev)
        Exit Sub

        '--thats all-

    End Sub  '--cell validating.--
    '= = = = = = = = = = = = = = = = = =

    '- Validated-
    Private Sub dgvPaymentDetails_CellValidated(ByVal sender As Object, _
                                                       ByVal ev As DataGridViewCellEventArgs) _
                                                           Handles dgvSalePaymentDetails.CellValidated
        If mbIsInitialising Then Exit Sub

        Call mClsSale1.dgvPaymentDetails_CellValidated(sender, ev)
        Exit Sub

        '--thats all-

    End Sub  '-validated-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '--update static Var to keep track of comments.-

    '3403.730= -Comments/Delivery gone to Separate Form..
    '3403.730= -Comments/Delivery gone to Separate Form..

    'Private Sub txtSaleComments_TextChanged(ByVal sender As System.Object, _
    '                                        ByVal e As System.EventArgs)

    '    If mbIsInitialising Then Exit Sub
    '    Call mClsSale1.txtSaleComments_TextChanged(sender, e)
    '    Exit Sub

    '    '--thats all-
    'End Sub '-Comments_TextChanged-
    ''= = = = = = = = = = = =  = = = =
    ''--update static Var to keep track of comments.-

    'Private Sub txtSaleDelivery_TextChanged(ByVal sender As System.Object, _
    '                                        ByVal e As System.EventArgs)

    '    If mbIsInitialising Then Exit Sub
    '    Call mClsSale1.txtSaleDelivery_TextChanged(sender, e)
    '    Exit Sub

    '    '--thats all-
    'End Sub '-delivery_TextChanged-
    '= = = = = = = = = = = =  = = = =
    '-===FF->

    '==3403.730--
    '- btnSaleComments--

    Private Sub btnSaleComments_Click(sender As Object, ev As EventArgs) Handles btnSaleComments.Click

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.btnSaleComments_Click(sender, ev)
        Exit Sub

        '-- thats all=

    End Sub '-btnSaleComments_Click-
    '= = = = = ==  == = == = = = = = == 

    '-- C o m m i t  S a l e - (or Refund)-
    '-- C o m m i t  S a l e --
    '-- C o m m i t  S a l e --

    '=3501.1001-  Change colur if has focus.

    Private Sub btnCommitSale_GotFocus(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) Handles btnCommitSale.GotFocus
        Dim btn1 As Button = CType(sender, Button)
        btn1.BackColor = Color.YellowGreen

    End Sub  '-got focus-
    '= = = = = = =  = = = = =
    Private Sub btnCommitSale_LostFocus(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs) Handles btnCommitSale.LostFocus
        Dim btn1 As Button = CType(sender, Button)
        btn1.BackColor = Color.WhiteSmoke

    End Sub  '-lost focus-
    '= = = = = = = = = = == =

    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    Private Sub btnCommitSale_Click(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) Handles btnCommitSale.Click

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.btnCommitSale_Click(sender, e)
        '-- drop this tab/form if commit done..
        If (Not mClsSale1.HasCurrentSale) Then   '-empty..  can be dropped.
            '- can exit
            Dim intPos As Integer
            Dim intTranNo As Integer = 0
            Dim sName As String = Me.Name
            '-- get trans. no from name.
            intPos = InStr(sName, "_")
            If (intPos > 0) AndAlso IsNumeric(Mid(sName, intPos + 1)) Then
                intTranNo = CInt(Mid(sName, intPos + 1))
            End If
            If (intTranNo > 1) Then  '-excess-
                '= Me.Close()
                Call close_me()

                Call close_me()
            End If
        End If  '-empty-
    End Sub  '--commit-
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Cancel invoice---

    Private Sub btnCancel_Click(ByVal sender As System.Object, _
                                       ByVal ev As System.EventArgs) Handles btnCancelSale.Click, btnCancelSale2.Click

        If mbIsInitialising Then Exit Sub
        Call mClsSale1.btnCancel_Click(sender, ev)
        Exit Sub

        '-- thats all=

    End Sub  '--cancel--
    '= = = = = = = = = = = = = =
    '-===FF->

    '--admin-
    '--admin-

    'Private Sub btnPOSAdmin_Click(ByVal sender As System.Object, _
    '                                ByVal ev As System.EventArgs)

    '    If mbIsInitialising Then Exit Sub
    '    Call mClsSale1.btnPOSAdmin_Click(sender, ev)
    '    Exit Sub
    '    '== Dim frmAdmin1 As frmPOS31Admin
    '    '== frmAdmin1 = New frmPOS31Admin(msServer, mCnnSql, msSqlDbName, mColSqlDBInfo, _
    '    '==                                 msVersionPOS, mImageUserLogo, mIntStaff_id, msStaffName)
    '    '= frmAdmin1.ShowDialog()

    'End Sub  '--admin-
    '= = = =  = = = =  = = = = = = 

    '- try to stop cust-barcode validation.

    'Private Sub btnPOSAdmin_KeyPress(ByVal eventSender As System.Object, _
    '                                   ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)

    '    Dim keyAscii As Short = Asc(eventArgs.KeyChar)

    '    If mbIsInitialising Then Exit Sub

    '    If (keyAscii = 13) Then '--enter-
    '        Call mClsSale1.btnPOSAdmin_Click(eventSender, eventArgs)
    '        keyAscii = 0  '-done-
    '    End If  '-enter-
    '    eventArgs.KeyChar = Chr(keyAscii)
    '    If keyAscii = 0 Then
    '        eventArgs.Handled = True
    '    End If
    'End Sub '-btnPOSAdmin- keypress-
    '= = = = = = == = = = === == = ==

    '- END of Sale Tab stuff..==

    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Admin Tab-  Button Events..
    '-- Admin Tab-  Button Events..
    '-- Admin Tab-  Button Events..

    '-- Handles ALL Buttons.-
    '-- adminFunctionButtons --

    '=--  3403.521=  PLUS View Laybys..
    '=--  3411.1125=  PLUS View Subscriptions..

    'Private Sub adminFunctionButtons_click(ByVal sender As System.Object, _
    '                                            ByVal ev As System.EventArgs)

    '    Dim button1 As Button = CType(sender, Button)
    '    Dim sButtonName As String = button1.Name

    '    '= MsgBox("you clicked button '" & button1.Name & "'..", MsgBoxStyle.Information)

    '    '- call admin exec function..
    '    Call mClsSale1.adminFunctionButtons_click(sender, ev)

    'End Sub  '-adminFunctionButtons-
    '= = = = = = = = = =  = = = = = = = = = =
    '-===FF->


    '--exit-

    Private Sub btnMainExit_Click(sender As Object, e As EventArgs) Handles btnMainExit.Click
        'If (MsgBox("Sure you want to exit the POS program ?", _
        '      MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
        '    Exit Sub '- reconsidered-
        'Else  '-ok to close=
        '    Me.Close()
        'End If
        '= Me.Close()
        Call close_me()

    End Sub  '-exit-
    '= = = = = = = = = =

    Private Sub close_me()
        Dim bCancel As Boolean = False '= = EventArgs.Cancel
        '= Dim intMode As System.Windows.Forms.CloseReason = EventArgs.CloseReason
        Dim sName, s1 As String
        Dim intPos, intTranNo As Integer

        intTranNo = 0
        sName = Me.Name
        '-- get trans. no from name.
        intPos = InStr(sName, "_")
        If (intPos > 0) AndAlso IsNumeric(Mid(sName, intPos + 1)) Then
            intTranNo = CInt(Mid(sName, intPos + 1))
        End If

        '-- check for incomplete transactions..
        If (intTranNo = 1) Then
            MsgBox("Note- The First sale form has to stay.. ", MsgBoxStyle.Information)
            bCancel = True
        ElseIf Not mClsSale1.HasCurrentSale Then   '-empty..  can be dropped.
            bCancel = False  '--let it go--
        ElseIf (MsgBox("Close this transaction ?", _
                      MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
            bCancel = True  '- reconsidered-
        Else  '-ok to close=
            'If (Not (mCnnSql Is Nothing)) AndAlso ((mCnnSql.State And 1) <> 0) Then '--open-
            '    mCnnSql.Close()
            'End If
            bCancel = False  '--let it go--
        End If

        If bCancel Then Exit Sub '--keep alive.-

        '- inform parent.-
        '- Report to Parent..-
        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

        'If Not bCancel Then  '--exiting.
        '    If Not (Me.delReport Is Nothing) Then
        '        delReport.Invoke(Me.Name, "FormClosed", "")
        '    End If
        'End If  '-cancel-

        'Me.Dispose()

    End Sub '--close me-
    '= = = = = = = = = =


    'Private Sub frmPOS34Main_FormClosing(ByVal eventSender As System.Object, _
    '                                     ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
    '                                        Handles Me.FormClosing
    '    Dim bCancel As Boolean = eventArgs.Cancel
    '    Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
    '    Dim sName, s1 As String
    '    Dim intPos, intTranNo As Integer

    '    intTranNo = 0
    '    sName = Me.Name
    '    '-- get trans. no from name.
    '    intPos = InStr(sName, "_")
    '    If (intPos > 0) AndAlso IsNumeric(Mid(sName, intPos + 1)) Then
    '        intTranNo = CInt(Mid(sName, intPos + 1))
    '    End If
    '    'Call gbLogMsg(gsRuntimeLogPath, "== POS34Main form is closing.." & vbCrLf & vbCrLf & _
    '    '                                                 "= = = = = = = = = = = =" & vbCrLf & vbCrLf)
    '    Select Case intMode
    '        Case System.Windows.Forms.CloseReason.WindowsShutDown, _
    '                  System.Windows.Forms.CloseReason.TaskManagerClosing, _
    '                           System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
    '            bCancel = False  '--let it go--
    '        Case System.Windows.Forms.CloseReason.UserClosing

    '            '-- check for incomplete transactions..
    '            If (intTranNo = 1) Then
    '                MsgBox("Note- The First sale form has to stay.. ", MsgBoxStyle.Information)
    '                bCancel = True
    '            ElseIf Not mClsSale1.HasCurrentSale Then   '-empty..  can be dropped.
    '                bCancel = False  '--let it go--
    '            ElseIf (MsgBox("Close this transaction ?", _
    '                          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
    '                bCancel = True  '- reconsidered-
    '            Else  '-ok to close=
    '                'If (Not (mCnnSql Is Nothing)) AndAlso ((mCnnSql.State And 1) <> 0) Then '--open-
    '                '    mCnnSql.Close()
    '                'End If
    '                bCancel = False  '--let it go--
    '            End If
    '            '==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '        Case Else
    '            bCancel = False  '--let it go--
    '    End Select '--mode--
    '    eventArgs.Cancel = bCancel

    '    '- Report to Parent..-
    '    If Not bCancel Then  '--exiting.
    '        '- Create an instance of the delegate.  
    '        '= Dim msd As subReportDelegate = AddressOf c1.Sub1
    '        ' Call the method.  
    '        '= msd.Invoke(Me.Name, "FormClosing")
    '        '= if (this.InvokeDel != null)
    '        '= InvokeDel.Invoke(this.txtMsg.Text);
    '        If Not (Me.delReport Is Nothing) Then
    '            delReport.Invoke(Me.Name, "FormClosing", "")
    '        End If
    '    End If  '-cancel-

    'End Sub  '-closing..=
    '= = = = = = = = = = === = 

    '-FormClosed-

    'Private Sub frmPOS34Main_FormClosed(ByVal eventSender As System.Object, _
    '                                     ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) _
    '                                        Handles Me.FormClosed
    '    If Not (Me.delReport Is Nothing) Then
    '        delReport.Invoke(Me.Name, "FormClosed", "")
    '    End If

    'End Sub  '-FormClosed-
    '= = = = = = = = = ==  = 


End Class  '--ucPosSaleChild-
'= = = = = = = = = = = = == = = = 

'== end of user Control (was form)==