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
Imports system.data.OleDb
Imports System.Threading

Public Class frmStock

    '-- JobMatix POS 3.0 --
    '--  Stock and GoodsReceived Functions..--

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
    '==   grh- JobMatix POS3 3.1.3103.0205-
    '==      >>   Fix Browse Refresh crash (srch should Activate, not refresh..
    '==
    '==   grh- JobMatix POS3 3.1.3103.0217-
    '==      >>   Re-size..  adjust all controls..
    '==
    '==   grh- JobMatix POS3 3.1.3107.0716-
    '==      >> Sell margin is applied to CostExTax..
    '==
    '==  grh. JobMatix 3.1.3107.0805 ---  05-Aug-2015 ===
    '==   >> Now for .Net 4.5.2- 
    '==
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==
    '==  '=3301.606= 
    '==     v3.3.3301.606..  06-June-2016= ===
    '==       >>  Drop radio buttons for isService, isLabour etc.
    '==            Add "chkIsNonStockItem"..
    '==
    '==     v3.3.3303.0121..  21-Jan-2017= ===
    '==       >> frmStock- Tidying up..
    '== 
    '==     v3.4.3403.0512 = 12May2017=
    '==      -- Added code to select Label printer (frmGetprinter).
    '==
    '= =     v3.4.3411.0417- 17-April-2018=
    '==             -- catch Enter on srch text-
    '==
    '==       V3.5. 3501.0826- Called From Goods Rcvd.  
    '==         -- Add new Stock (have barcode).
    '==         -- 3501.0913-  Add new Stock (Fix Supplier name.).
    '==  
    '== -- Updated 3501.0922  22-Sept-2018=  
    '==     -- 3501.0922-  ADD AUTO-barcode option for NEW STOCK Item....
    '==           -- New idea.. get list of existing numeric barcodes.
    '==                         --  And pick the lowest missing one..
    '== 
    '== -- Updated 3501.1030  30-Oct-2018=  
    '==     -- RE-arrange Category and Brand, Supplier DropDowns for more clarity...
    '==
    '==   Updated.- 3519.0211 11-Feb-2019= 
    '==     -- Fix Stock Admin Alpha barcode INSERT error.-  
    '--             barcode is alpha, and Must have quotes..
    '==
    '==   Updated.- 3519.0214 12/14-Feb-2019= 
    '==     -- Fixes to for Grid browsing problems..-
    '==         (Force a delay after selection changed before showing details.)..-
    '==     -- Fixes  for making last-added item selectable...-
    '==     -- Fixes to Stock Item Detail panel to make Brand,Supplier AutoSuggestive.. 
    '==     --  Fixes to round off Sell_inc..
    '==     -- MORE Fixes to for Grid browsing problems..-  
    '--           ie. Use "RowEnter" instead of SelectionChanged (as per Customer Admin)
    '==
    '==   Updated.- 3519.0227 27-Feb-2019= 
    '==     -- MORE Fixes to for Grid browsing problems..-
    '==              3519.0227= MUST update WHERE each time so CLEAR works to re-fill Grid.
    '==     --  Fix validations for Stock re-order level..
    '==
    '==   Updated.- 3519.0311  Started 05-March-2019= 
    '==      >> Stock Admin- Add references buttons for Cat1, Cat2, Brands.. 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..  Started in here 19-May-2020
    '==
    '==   THIRD MAIN THEME is implementation of a SupplierCode Field in the StockAdmin details Tab..
    '==       --  Involves also adding code to the New/Update Stock Commit to also add/Update Supplier Code Table if needed.
    '==    ALSO Involves FINALLY revamping frmStock,
    '==                   so as to make into purely a SHELL (container) for the ucChildStockAdmin UserControl.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    '--  data grid column nos.--
    Private Const DGV_SERIALS_NUMBER As Short = 0
    Private Const DGV_SERIALS_STATUS As Short = 1
    Private Const DGV_SERIALS_HISTORY As Short = 2

    '-- goods recvd.-
    Private Const DGV_GOODS_ID As Short = 0
    Private Const DGV_GOODS_DATE As Short = 1
    Private Const DGV_GOODS_SUPPLIER As Short = 2
    Private Const DGV_GOODS_INVOICENO As Short = 3
    Private Const DGV_GOODS_QTY As Short = 4

    '= = = = = = = = = = = = = = = = = = = = = = = = =

    Private mbActivated As Boolean = False

    '--inputs--
    Private msVersionPOS As String = ""

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
    '==3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo

    Private msDefaultPrinterName As String = ""

    Private mDecGSTPercentage As Decimal = 0
    Private mDecSellMargin As Decimal = 0

    Private msBusinessShortName As String = ""
    Private msBarcodeFontName As String = "IDAutomationHC39M" '-default-
    Private mIntBarcodeFontSize As Integer = 8  '-default-
    '== Private msColourPrtName As String = ""
    '== Private msReceiptPrtName As String = ""
    '== Private msLabelPrtName As String = ""

    Private mIntForm_top As Integer = -1
    Private mIntForm_left As Integer = -1

    '-- stock list sorting..-
    Private mlSortOrderStock As System.Windows.Forms.SortOrder  '== Integer
    Private mlSortKeyStock As Integer

    Private msRequiredFields As String = ""

    '--  Current Item--
    Private mbIsNewItem As Boolean = False

    Private mbItemIsLoading As Boolean = False
    Private mIntStock_id As Integer = -1
    Private mByteImage1 As Byte()

    Private msModifiedControls As String = ""  '--names of controls modified.-

    ''- Stock list now in dataGridView -
    'Private mColPrefsStock As Collection
    'Private mBrowse1 As clsBrowse3
    'Private mLngSelectedRow As Integer = -1

    Private mIntFormDesignWidth As Integer
    Private mIntFormDesignHeight As Integer

    '=3501.0826-  From Goods Rcvd.  Add new Stock (have barcode).
    Private mbAddNewStockOnly As Boolean = False
    Private mbAddNewStockAutoGenBarcode As Boolean = False

    Private msAddNewStockBarcode As String = ""
    Private mIntAddNewStockSupplier_id As Integer = -1
    Private msAddNewStockSupplierName As String = ""

    Private mbIsCancelled As Boolean = False

    'Private mColPrefsCategory1 As Collection
    'Private mColPrefsCategory2 As Collection
    'Private mColPrefsBrands As Collection

    '-save child form for resizing..
    Private mUcChild1 As ucChildStockAdmin


    '= = = = = = = = = = = = = = = = = = = = = = = = = 
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

    '-AddNewStockOnly-

    WriteOnly Property AddNewStockOnly As Boolean
        Set(value As Boolean)
            mbAddNewStockOnly = value
        End Set
    End Property  '-AddNewCustomerOnly-
    '= = = = = = =  = = = = = = = = = = = =

    '-- F5 from GoodsReceived (New stock with AutoGen barcode)
    WriteOnly Property AddNewStockAutoGenBarcode As Boolean
        Set(value As Boolean)
            mbAddNewStockAutoGenBarcode = value
        End Set
    End Property  '-AddNewCustomerOnly-
    '= = = = = = =  = = = = = = = = = = = =

    '-- Barcode supplied from Goods Received.-
    WriteOnly Property AddNewStockBarcode As String
        Set(value As String)
            msAddNewStockBarcode = value
            '= txtBarcode.Text = msAddNewStockBarcode
        End Set
    End Property  '-barcode input-
    '= = = = = = = = = =  = == = = = =

    WriteOnly Property AddNewStockSupplier_id As Integer
        Set(value As Integer)
            mIntAddNewStockSupplier_id = value
            '= txtSupplier_id.Text = CStr(value)
        End Set
    End Property  '--supplier_id-
    '= = = = = = = = = = = = = ==

    WriteOnly Property AddNewStockSupplierName As String
        Set(value As String)
            '= txtSupplierName.Text = value
            msAddNewStockSupplierName = value
        End Set
    End Property  '-barcode input-
    '= = = = = = = = = =  = == = = = =
    '-===FF->

    '--results-

    '- result of selection..
    ReadOnly Property NewStock_id As Integer
        Get
            NewStock_id = mIntStock_id
        End Get
    End Property '-invoice-
    '= = = = = = = = = = = = = = = = = = = = = = = =

    ReadOnly Property wasCancelled As Boolean
        Get
            wasCancelled = mbIsCancelled
        End Get
    End Property  '-cancelled--
    '= = = = = = = = = = = = = = = = = = = = = = == = = 
    '-===FF->

    '==-- Child uses Delegate to signal child closed to Main Parent.....

    Public Sub subChildReport(ByVal strChildName As String, _
                              ByVal strEvent As String, _
                               ByVal sText As String)

 
    End Sub  '--ChildReport-
    '= = = = = = =  =  = =

    '==-- Child uses EVENT Delegate to signal child closed to Main Parent..
    '-- Child has this event definition..
    '--   Public Event PosChildClosing(ByVal strChildName As String)

    '-- Dispose of Tab Page and Child Control..
    '-- Dispose of Tab Page and Child..

    Private Sub posChild_Closing(ByVal strChildName As String)

        RemoveHandler mUcChild1.PosChildClosing, AddressOf posChild_Closing

        '-return result to caller..

        mbIsCancelled = mUcChild1.wasCancelled
        '= msSelectedCustomerBarcode = mUcChild1.selectedBarcode

        Me.Hide()

    End Sub  '- posChild_Closing-
    '= = = = = = = = = = = === = = =

    '==--Child uses Delegate to signal child STAFF SIGNED ON to Main Parent.....

    Public Sub subChildSignedOn(ByVal intStaffid As Integer, _
                                ByVal strStaffBarcode As String, _
                                 ByVal strStaffName As String)
        '--save as main signon-
        mIntStaff_id = intStaffid
        '=msStaffBarcode = strStaffBarcode
        msStaffName = strStaffName

    End Sub '' child signed on..-
    '= = = = = = = == = = = = = = = = = = = =
    '-===FF->


    '-== L o a d ==

    Private Sub frmStock_Load(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles MyBase.Load

        grpBoxMain.Text = ""
        Me.Text = "Stock Admin- MODAL. " & msVersionPOS

        mIntFormDesignWidth = Me.Width  '--save starting dim.
        mIntFormDesignHeight = Me.Height  '--save starting dim.

      
        labDLLversion.Text = msVersionPOS

        'If (mIntForm_top = -1) Or (mIntForm_left = -1) Then
        Call CenterForm(Me)
        'Else
        '    '-- caller provided-
        '    Me.Top = mIntForm_top
        '    Me.Left = mIntForm_left
        'End If

    End Sub  '-- load --
    '= = = = = = = = = = = = = =  

    '--Activated-

    Private Sub frmStock_Activated(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles MyBase.Activated
        '-- do sub at startup only..
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '--activated-
    '= = = = = = = = = = === =

    '3431.0515-  now SHOWN..

    Private Sub frmStock_Shown(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim s1, sList As String
        Dim sSqlType As String

        '= If mbActivated Then Exit Sub
        '= mbActivated = True

        '-- Create instance of  ucChildStockAdmin..

        Dim ucChild1 As ucChildStockAdmin
        '= Dim tabPageChild1 As TabPage '= = New TabPage

        '- make as static first, first, for resizing.
        mUcChild1 = New ucChildStockAdmin

        '-save- for re-size..
        ucChild1 = mUcChild1

        ucChild1.StaffId = mIntStaff_id
        ucChild1.StaffName = msStaffName
        ucChild1.SqlServer = msServer
        ucChild1.connectionSql = mCnnSql '--job tracking sql connection..-
        ucChild1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
        ucChild1.DBname = msSqlDbName
        ucChild1.VersionPOS = msVersionPOS

        ucChild1.AddNewStockOnly = mbAddNewStockOnly
        ucChild1.AddNewStockAutoGenBarcode = mbAddNewStockAutoGenBarcode

        ucChild1.AddNewStockBarcode = msAddNewStockBarcode
        ucChild1.AddNewStockSupplier_id = mIntAddNewStockSupplier_id
        ucChild1.AddNewStockSupplierName = msAddNewStockSupplierName

        ucChild1.Name = "ucChildStockAdmin1" '= strFormClassName & "_" & CStr(mIntChildCount)
        ucChild1.Text = ucChild1.Name
        ucChild1.Dock = DockStyle.Fill
        ucChild1.AutoSize = False
        ucChild1.Dock = DockStyle.Fill
        ucChild1.AutoSize = False

        ucChild1.Parent = grpBoxMain
        grpBoxMain.Controls.Add(ucChild1)

        ucChild1.delReport = AddressOf Me.subChildReport

        '-- Add Handler for ClosingEvent for this Child INSTANCE..
        AddHandler mUcChild1.PosChildClosing, AddressOf posChild_Closing


        '- for pacing the grid row selections.
        '= timerGrid.Enabled = True
        '= timerGrid.Start()

    End Sub  '-SHOWN..  - Activated --
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Form Resized --
    '--  form resized..--
    '-- DELEGATE for Resizing Child..
    Public Delegate Sub SubFormResized(ByVal intParentWidth As Integer, _
                                        ByVal intParentHeight As Integer)
    '-- This is instantiated below.
    Public delResized As SubFormResized '--    = AddressOf frmPosMainMdi.subChildReport

    '- Form re-sized..

    Private Sub frmStock_Resize(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        '== If mbIsInitialising Then Exit Sub

        If (Me.WindowState = System.Windows.Forms.FormWindowState.Minimized) Then
            Exit Sub
        End If

        '--  can't make smaller than original..-
        If (Me.Height < mIntFormDesignHeight) Then
            Me.Height = mIntFormDesignHeight
        End If
        If (Me.Width < mIntFormDesignWidth) Then
            Me.Width = mIntFormDesignWidth
        End If
        '-- resize main box and top panel-
        grpBoxMain.Width = Me.Width - 11
        grpBoxMain.Height = Me.Height - 60

        If (mUcChild1 IsNot Nothing) Then
            Dim ucChild1 As ucChildStockAdmin = mUcChild1
            delResized = New SubFormResized(AddressOf ucChild1.SubFormResized)
            '= Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
            Call delResized(grpBoxMain.Width - 7, grpBoxMain.Height - 7)
            delResized = Nothing
        End If  '-nothing.-

        labDLLversion.Top = grpBoxMain.Top + grpBoxMain.Height + 3

    End Sub  '--resize-                                                                                                                                     
    '= = = = = = = = = = = = = =  
    '-===FF->

    '-- DELEGATE for CLOSING Child..
    '= Public Delegate Sub SubFormCloseRequest()
    Public Delegate Function SubFormCloseRequest() As Boolean

    '-- This is instantiated below.
    Public delCloseRequest As SubFormCloseRequest '--    = AddressOf frmPosMainMdi.subChildReport

    '-- Mouse on "X" on child Tab..

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmStock_FormClosing(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim bCanCloseOk As Boolean = False
        '== Call gbLogMsg(gsRuntimeLogPath, "== JobMatixPOS Stock form is closing.." & vbCrLf & vbCrLf & _
        '==                                                  "= = = = = = = = = = = =" & vbCrLf & vbCrLf)
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                      System.Windows.Forms.CloseReason.TaskManagerClosing, _
                               System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
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

                'If (msModifiedControls <> "") Then
                '    If (MsgBox("Abandon changes ?", _
                '         MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
                '        intCancel = 1  '--cant close yet--'--was mistake..  keep going..
                '        Exit Sub
                '    End If
                'End If
                'intCancel = 0 '--let it go---
                '==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--closing--
    '= = = = = = =  = = = = = = = == 


End Class  '--frmStock-
'== end form==


'== the end ==