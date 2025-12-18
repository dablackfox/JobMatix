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

Public Class frmCustomer

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
    '==  = = = = = = = = = =  = = = = = = = = = = = = = = = = = = = = = == = =
    '==
    '==    -- 4201.0424.  Child forms now converted to UserControls....
    '==    -- 4201.0503.  use TS-ProfessionalRenderer for "Ribbon" TS buttons....
    '==    -- 4201.0507.  New Stocktake and Payments-Reports migrated from JobMatix35....
    '== NEW STUFF-
     '==    -- 4201.0530.  Make original frmCustomer into a host shell for ucChildCustomer
    '==              So that it can still be called from JobTracking... 
    '==
    '==
    '== NEW revision-
    '==    -- 4201.0830.  Started 28-August-2019-
    '==        -- FIXes to frmCustomer (has child ucChildCustomer)..
    '==
    '== NEW revision-
    '==    -- 4201.0930.  Started 30-Sep-2019-
    '==        -- FIXes to heading labels...
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    'Const K_FINDACTIVEBG As Integer = &HC0FFFF
    'Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    ''-- INVOICES DataGridView columns.--
    'Private Const k_INVGRIDCOL_INV_DATE As Short = 0
    'Private Const k_INVGRIDCOL_INV_NO As Short = 1
    'Private Const k_INVGRIDCOL_TRANTYPE As Short = 2
    'Private Const k_INVGRIDCOL_ON_ACCOUNT As Short = 3
    'Private Const k_INVGRIDCOL_INV_TOTAL As Short = 4
    'Private Const k_INVGRIDCOL_PREV_PAID As Short = 5
    'Private Const k_INVGRIDCOL_OUTSTANDING As Short = 6

    'Private Const k_RequiredFieldsMsg As String = "Required: at least one Customer (or Company) Name, " & vbCrLf & _
    '                            "   and at least one Contact Number (or email).."

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
    Private msStaffBarcode As String = ""

    Private mbAddNewCustomerOnly As Boolean = False  '--dive straight into New Customer.

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

    '-- stock list sorting..-
    'Private mlSortOrderStock As System.Windows.Forms.SortOrder  '== Integer
    'Private mlSortKeyStock As Integer

    Private msRequiredFields As String = ""
    Private msRequiredFields2 As String = ""

    '--  Current Item--
    Private mbIsNewItem As Boolean = False

    Private mbItemIsLoading As Boolean = False
    '== Private mIntStock_id As Integer = -1
    '== Private mByteImage1 As Byte()

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

    '-save child form for resizing..
    Private mUcChild1 As ucChildCustomer

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
    ReadOnly Property selectedBarcode As String
        Get
            selectedBarcode = msSelectedCustomerBarcode
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

    Private Sub posChild_Closing(ByVal strChildName As String)

        RemoveHandler mUcChild1.PosChildClosing, AddressOf posChild_Closing

        '-return result to caller..

        mbIsCancelled = mUcChild1.wasCancelled
        msSelectedCustomerBarcode = mUcChild1.selectedBarcode

        Me.Hide()

    End Sub  '- posChild_Closing-
    '= = = = = = = = = = = === = = =

    '==--Child uses Delegate to signal child STAFF SIGNED ON to Main Parent.....

    Public Sub subChildSignedOn(ByVal intStaffid As Integer, _
                                ByVal strStaffBarcode As String, _
                                 ByVal strStaffName As String)
        '--save as main signon-
        mIntStaff_id = intStaffid
        msStaffBarcode = strStaffBarcode
        msStaffName = strStaffName

    End Sub '' child signed on..-
    '= = = = = = = == = = = = = = = = = = = =
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

    '-- L o a d --
    '-- L o a d --

    Private Sub frmCustomer_Load(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles MyBase.Load

        grpBoxMain.Text = ""
        'txtCustomerSearch.Text = ""
        'FrameBrowse.Text = ""
        labStaffName.Text = msStaffName
        labToday.Text = Format(Today, "dd-MMM-yyyy")

        'labItemHeader.Text = ""
        Me.Text = "JobMatixPOS- Customer Admin.  " & msVersionPOS

        '== grpBoxGoods.Text = ""
        mIntFormDesignWidth = Me.Width  '--save starting dim.
        mIntFormDesignHeight = Me.Height  '--save starting dim.

        '-- get system Info table data.-
        '==3301.428= 
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        ''--clear all-
        'Call mbClearAllTextFields()

        labDLLversion.Text = msVersionPOS
        If (mIntForm_top = -1) Or (mIntForm_left = -1) Then
            Call CenterForm(Me)
        Else
            '-- caller provided-
            Me.Top = mIntForm_top
            Me.Left = mIntForm_left
        End If

    End Sub  '-- load --
    '= = = = = = = = = = = = = =  

    '--Activated-

    Private Sub frmCustomer_Activated(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles MyBase.Activated
        '-- do sub at startup only..
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '--activated-

    '--Shown-

    Private Sub frmCustomer_Shown(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles MyBase.Shown

        '=4201.0530==
        '--  Load Child UserControl..

        Dim ucChild1 As ucChildCustomer
        '= Dim tabPageChild1 As TabPage '= = New TabPage

        '- make as static first, first, for resizing.
        mUcChild1 = New ucChildCustomer

        '-save- for re-size..
        ucChild1 = mUcChild1

        ucChild1.StaffId = mIntStaff_id
        ucChild1.StaffName = msStaffName
        ucChild1.SqlServer = msServer
        ucChild1.connectionSql = mCnnSql '--job tracking sql connection..-
        ucChild1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..
        ucChild1.DBname = msSqlDbName
        ucChild1.VersionPOS = msVersionPOS

        ucChild1.AddNewCustomerOnly = mbAddNewCustomerOnly

        '==Call mbAddNewChild2(ucChild1, "ucChildCustomer", "Customers", tabPageChild1)

        ucChild1.Name = "ucChildCustomer1" '= strFormClassName & "_" & CStr(mIntChildCount)
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


        'delResized = New SubFormResized(AddressOf ucChild1.SubFormResized)
        'Call delResized(grpBoxMain.Width - 7, grpBoxMain.Height - 7)
        'delResized = Nothing

        DoEvents()

    End Sub  '-- SHOWN --
    '= = = = = = = = = = = = = =  
    '-===FF->

    '-- Mdi Main form resized..--
    '--  form resized..--
    '-- DELEGATE for Resizing Child..
    Public Delegate Sub SubFormResized(ByVal intParentWidth As Integer, _
                                        ByVal intParentHeight As Integer)
    '-- This is instantiated below.
    Public delResized As SubFormResized '--    = AddressOf frmPosMainMdi.subChildReport

    '- Form re-sized..

    Private Sub frmCustomer_Resize(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

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
        panelBanner.Width = Me.Width - 11
        grpBoxMain.Width = panelBanner.Width
        grpBoxMain.Height = Me.Height - 93

        labDLLversion.Top = grpBoxMain.Top + grpBoxMain.Height + 27
        DoEvents()  '--time to adjust contents.

        '= btnExit.Left = btnOK.Left + 240
        '= btnExit.Top = btnOK.Top
        If (mUcChild1 IsNot Nothing) Then
            Dim ucChild1 As ucChildCustomer = mUcChild1
            delResized = New SubFormResized(AddressOf ucChild1.SubFormResized)
            '= Call delResized(TabControlMain.Width - 7, TabControlMain.Height - 7)
            Call delResized(grpBoxMain.Width - 7, grpBoxMain.Height - 7)
            delResized = Nothing
        End If  '-nothing.-

        DoEvents()

    End Sub   '-resize-
    '= = = = = = = = = = = = = =  
    '-===FF->

    '-- ok-

    'Private Sub btnOK_Click(sender As Object, e As EventArgs)
    '    If (msModifiedControls <> "") Then
    '        If (MsgBox("Abandon changes ?", _
    '             MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
    '            '= intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '            Exit Sub
    '        End If
    '    Else  '-go-
    '        '== mbIsCancelled = True
    '        Me.Hide()
    '    End If  '--modified-
    'End Sub  '--ok-
    ''= = = = = = = =  == 

    'Private Sub btnExit_Click(sender As Object, e As EventArgs)
    '    If (msModifiedControls <> "") Then
    '        If (MsgBox("Abandon changes ?", _
    '             MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes) Then
    '            '= intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '            Exit Sub
    '        End If
    '    Else  '-go-
    '        mbIsCancelled = True
    '        Me.Hide()
    '    End If  '--modified-

    'End Sub  '-exit-
    '= = = = = = = = === 
    '= = = = = = = = = = = 
    '-===FF->

    '--= = =Query  u n l o a d = = = = = = =
    '-- DELEGATE for CLOSING Child..
    '= Public Delegate Sub SubFormCloseRequest()
    Public Delegate Function SubFormCloseRequest() As Boolean

    '-- This is instantiated below.
    Public delCloseRequest As SubFormCloseRequest '--    = AddressOf frmPosMainMdi.subChildReport

    '-- Mouse on "X" on child Tab..


    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmCustomer_FormClosing(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                                Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim bCanCloseOk As Boolean = False

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
                ''==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--closing--
    '= = = = = = =  = = = = = = = == 


End Class  '-frmCustomer-
'= = = = = = =  = = = = =

'== end form ==