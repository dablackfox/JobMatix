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

Public Class ucChildSupplier
    Inherits UserControl

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '== UPDATES to Build 4282.1025  
    '==
    '==   Target-New-Build-4284 --  (30-October-2020)
    '==   Target-New-Build-4284 --  (30-October-2020)
    '==   Target-New-Build-4284 --  (30-October-2020)
    '==
    '==  A.  New Child USERCONTROL to move Suppliers Admin Browse into Main Tab Control.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    '--- For suppressing default popup menu..-
    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = =

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--
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

    Private mbAddNewSupplierOnly As Boolean = False  '--dive straight into New Customer.

    Private mbIsInitialising As Boolean = True
    Private mbIsCancelled As Boolean = False

    '-CAN input supplier-id-  ?????
    Private mIntSupplier_id As Integer = -1

    '==3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo

    Private msDefaultPrinterName As String = ""

    Private msRequiredFields As String = ""
    Private msRequiredFields2 As String = ""

    '--  Current Item--
    Private mbIsNewItem As Boolean = False

    Private mbItemIsLoading As Boolean = False

    Private msModifiedControls As String = ""  '--names of controls modified.-

    '-  list now in dataGridView -
    Private mColPrefsSupplier As Collection
    Private mBrowse1 As clsBrowse3
    Private mLngSelectedRow As Integer = -1

    Private mDecTotalInvoices As Decimal = 0
    Private mDecTotalOutstanding As Decimal = 0

    '==3301.705=
    '== NOT RESOLVED yet- Private msLastNewCustomerBarcode As String = ""

    '=3403-
    Private msSelectedSupplierBarcode As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    Private mColKeyValues As Collection
    Private mColRowValues As Collection
    Private mColPrimaryKeys As Collection
    Private mColTable As Collection
    Private mColFields As Collection
    Private mColOtherIndexes As Collection

    Private msBusinessName As String = ""
    Private msSupplierBarcode As String = ""
    '= = = = = = = = = = = = = = = = = = = == = 

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String,
                                          ByVal strEvent As String,
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

    WriteOnly Property supplier_id() As Integer
        Set(ByVal value As Integer)
            mIntSupplier_id = value
        End Set
    End Property  '--left-
    '= = = = = = = = = = = = = 

    WriteOnly Property AddNewSupplierOnly As Boolean
        Set(value As Boolean)
            mbAddNewSupplierOnly = value
        End Set
    End Property  '-AddNewCustomerOnly-
    '= = = = = = =  = = = = = = = = = 
    '-===FF->

    '--results-

    '- result of selection..
    Public ReadOnly Property selectedBarcode As String
        Get
            selectedBarcode = msSelectedSupplierBarcode
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
    '= = = = = = = = = = = = = = = = = = = = == = = = = = == = = = = = == =
    '-===FF->

    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer,
                            ByVal intParentHeight As Integer)

        Me.Width = intParentWidth - 11
        Me.Height = intParentHeight  '= - 11
        '-- resize our controls..
        '= DoEvents()
        '-- resize main box and top panel-

        grpBoxSupplier.Width = Me.Width - 5  '= panelBanner.Width
        '=4221.0207=  grab back some height.
        grpBoxSupplier.Height = Me.Height - 12  '- 48  '= 120  '=93

        '= labDLLversion.Top = grpBoxCustomer.Top + grpBoxCustomer.Height + 27
        '= DoEvents()  '--time to adjust contents.

        panelSupplierHdr.Left = grpBoxSupplier.Width - panelSupplierHdr.Width - 13
        panelSupplierDetail.Left = panelSupplierHdr.Left

        FrameBrowse.Width = grpBoxSupplier.Width - panelSupplierHdr.Width - 20
        FrameBrowse.Height = grpBoxSupplier.Height - FrameBrowse.Top - 12
        panelBanner.Width = grpBoxSupplier.Width - 10

        dgvSupplierList.Width = FrameBrowse.Width - 11
        dgvSupplierList.Height = FrameBrowse.Height - dgvSupplierList.Top - 12

        btnExit.Left = grpBoxSupplier.Width - btnExit.Width - 40  '= btnOK.Left + 240
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
            If (MsgBox("Abandon changes ?" & vbCrLf & "(" & msModifiedControls & ")..",
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

    Private Function mbGetSqlScalarStringValue(ByRef cnnSql As OleDbConnection,
                                           ByVal sSql As String,
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
            sErrorMsg = "mbGetSqlScalarStringValue: Error in Executing Sql: " & vbCrLf &
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback &
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbGetSqlScalarIntegerValue-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    'Private Function mbGetSqlScalarIntValue(ByRef cnnSql As OleDbConnection,
    '                                         ByVal sSql As String,
    '                                        ByRef intResult As Integer) As Boolean
    '    Dim sqlCmd1 As OleDbCommand
    '    '==Dim intResult As Integer
    '    Dim sMsg, sErrorMsg As String
    '    Dim sRollback As String = ""

    '    mbGetSqlScalarIntValue = False
    '    Try
    '        sqlCmd1 = New OleDbCommand(sSql, cnnSql)
    '        intResult = sqlCmd1.ExecuteScalar
    '        mbGetSqlScalarIntValue = True   '--ok--
    '        '= MsgBox("Sql exec ok. " & intResult & " is result..", MsgBoxStyle.Information)
    '    Catch ex As Exception
    '        '= lAffected = lError
    '        sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
    '        sErrorMsg = "mbGetSqlScalarIntValue: Error in Executing Sql: " & vbCrLf &
    '                  sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback &
    '                   "--- end of error msg.--" & vbCrLf
    '        '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
    '        Call gbLogMsg(gsErrorLogPath, sErrorMsg)
    '        MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
    '    End Try
    'End Function '-mbGetSqlScalarIntegerValue-
    '= = = = = = = = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--GetLastCustomerIdent-
    '==  GLOBAL- CURRENT --

    'Private Function mbGetLastCustomerIdent_Current(ByRef intIdent As Integer) As Boolean
    '    Dim sSql As String
    '    Dim intId As Integer

    '    mbGetLastCustomerIdent_Current = False
    '    '-  Retrieve customer_id:  (IDENTITY of Customer record written.)-
    '    sSql = "SELECT CAST(IDENT_CURRENT ('dbo.customer') AS int);"
    '    If mbGetSqlScalarIntValue(mCnnSql, sSql, intId) Then
    '        intIdent = intId
    '        '-- update invoice display later..-
    '        mbGetLastCustomerIdent_Current = True
    '        '== MsgBox("OK..  Added new Customer record..." & vbCrLf & _
    '        '=          "Customer_id is " & intIdent, MsgBoxStyle.Information)
    '    Else
    '        '== MsgBox("Failed to retrieve latest Customer No..", MsgBoxStyle.Exclamation)
    '        Exit Function
    '    End If
    'End Function '-GetLastCustomerIdent_Current-
    '= = = = = = = = = = = = = = = = = = =


    '--GetLastCustomerIdent-
    '-- IN OUR SCOPE --

    'Private Function mbGetLastSupplierIdent(ByRef intIdent As Integer) As Boolean
    '    Dim sSql As String
    '    Dim intId As Integer

    '    mbGetLastSupplierIdent = False
    '    '-  Retrieve customer_id:  (IDENTITY of Customer record written.)-
    '    '== sSql = "SELECT CAST(SCOPE_IDENTITY ('dbo.customer') AS int);"
    '    sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
    '    If mbGetSqlScalarIntValue(mCnnSql, sSql, intId) Then
    '        intIdent = intId
    '        '-- update invoice display later..-
    '        mbGetLastSupplierIdent = True
    '        '== MsgBox("OK..  Added new Customer record..." & vbCrLf & _
    '        '=          "Customer_id is " & intIdent, MsgBoxStyle.Information)
    '    Else
    '        '== MsgBox("Failed to retrieve latest Customer No..", MsgBoxStyle.Exclamation)
    '        Exit Function
    '    End If
    'End Function '-GetLastSupplierIdent-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Show some Supplier Info--

    Private Function mbShowSupplierInfo(ByVal intSupplier_id As Integer) As Boolean
        Dim sSql, s1, s2, sErrorMsg As String
        Dim dataTable1 As DataTable
        Dim row1 As DataRow

        mbShowSupplierInfo = False
        btnEdit.Enabled = False
        '=btnTagEdit.Enabled = False
        btnLookupGoods.Enabled = False

        txtBarcode.Text = ""
        '= txtCustomer_id.Text = ""
        labSupplierName.Text = ""
        mIntSupplier_id = -1
        txtContactName.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtWebsite.Text = ""
        txtAddress.Text = ""
        txtComments.Text = ""

        If (intSupplier_id <= 0) Then
            Exit Function
        End If
        '--lookup customer-id-
        '--  get recordset as collection for SELECT..--
        sSql = "SELECT * FROM [supplier] WHERE (supplier_id=" & CStr(intSupplier_id) & ");"
        If gbGetDataTable(mCnnSql, dataTable1, sSql) AndAlso
                               (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
            If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
                row1 = dataTable1.Rows(0)  '-first and only row for supplier_id.
                txtBarcode.Text = row1.Item("barcode")
                msSupplierBarcode = txtBarcode.Text
                mIntSupplier_id = intSupplier_id

                labSupplierName.Text = row1.Item("supplierName")
                txtContactName.Text = row1.Item("contactName")
                txtPhone.Text = row1.Item("phone")
                txtFax.Text = row1.Item("fax")
                txtEmail.Text = row1.Item("emailAddress")
                txtWebsite.Text = row1.Item("webSiteURL")
                txtAddress.Text = row1.Item("address") & vbCrLf & row1.Item("suburb") & vbCrLf
                txtAddress.Text &= row1.Item("state") & "  " & row1.Item("postcode")

                txtComments.Text = row1.Item("comments")

                btnEdit.Enabled = True
                btnLookupGoods.Enabled = True
            Else
                '--no data-
                MsgBox("No supplier data !", MsgBoxStyle.Exclamation)

            End If  '-have data-
        Else
            '--get failed-
            MsgBox("ERROR gettingsupplier data !" & vbCrLf &
                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        End If '-get-

    End Function  '-mbShowSupplierInfo-
    '= = = = = = = = = == = = = = = = 
    '-===FF->

    '---select record--

    Private Sub mSelectRecord(ByVal lngRow As Integer)

        Dim sName As String
        Dim cx As Integer
        Dim colFld As Collection
        Dim v1 As Object

        '--setup key-values col. of current row for retrieval of correct full record--
        mColKeyValues = New Collection
        For Each v1 In mColPrimaryKeys
            '--find column name--
            For cx = 0 To dgvSupplierList.Columns.Count - 1   '===  (MSHFlexgrid1.get_Cols() - 1)
                If LCase(CStr(v1)) = LCase(dgvSupplierList.Columns(cx).HeaderCell.Value) Then
                    '== LCase(MSHFlexgrid1.get_TextMatrix(0, cx)) Then
                    '--found this key col.-
                    '===  mColKeyValues.Add(MSHFlexgrid1.get_TextMatrix(lngRow, cx)) '--save key value for row..-
                    mColKeyValues.Add(dgvSupplierList.Rows(lngRow).Cells(cx).Value) '--save key value for row..-
                    Exit For
                End If '--found-
            Next cx
        Next v1
        '-- send back complete row  from grid..--
        mColRowValues = New Collection
        For cx = 0 To dgvSupplierList.Columns.Count - 1   '=== (MSHFlexgrid1.get_Cols() - 1)
            colFld = New Collection
            '== colFld.Add(LCase(MSHFlexgrid1.get_TextMatrix(0, cx)), "name") '--col hdr..-
            sName = dgvSupplierList.Columns(cx).HeaderCell.Value
            colFld.Add(LCase(sName), "name") '--col hdr..-

            '== colFld.Add(MSHFlexgrid1.get_TextMatrix(lngRow, cx), "value") '--col hdr..-
            colFld.Add(dgvSupplierList.Rows(lngRow).Cells(cx).Value, "value") '--col hdr..-
            mColRowValues.Add(colFld, LCase(sName))
        Next cx

    End Sub '--select--
    '= = = = = = = = = = = = = =
    '-===FF->

    Private Function mbEditRecord(ByVal intRow As Integer)
        Dim frmEdit1 As frmEdit

        frmEdit1 = New frmEdit
        frmEdit1.connection = mCnnSql '--POS sql connenction..-
        frmEdit1.colTables = mColSqlDBInfo
        frmEdit1.DBname = msSqlDbName
        frmEdit1.tableName = "supplier"
        '== frmEdit1.IsSqlServer = True '--bIsSqlServer
        frmEdit1.newRecord = False
        frmEdit1.StaffId = mIntStaff_id
        frmEdit1.StaffName = msStaffName

        '--- set PKEY WHERE condition for edit.--
        frmEdit1.selectedKey = mColKeyValues
        frmEdit1.PreferredColumns = mColPrefsSupplier
        frmEdit1.Title = "Editing Supplier record"

        frmEdit1.versionPOS = msVersionPOS

        frmEdit1.ShowDialog()

        frmEdit1.Close()
        '--get new r/set, load grid--
        '= If mbReload() Then

        '= End If

    End Function  '--edit-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  GET READY for Function KEY..
    '--- INITIALISE customer Browser.for Lookup--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(Optional ByVal sSrchWhereCond As String = "") As Boolean

        '=Dim colPrefs As Collection
        '= Dim sHostTablename As String
        Dim sWhere As String

        mBrowse1 = New clsBrowse3 '== clsBrowse22

        mBrowse1.connection = mCnnSql  '= mRetailHost1.connection
        mBrowse1.colTables = mColSqlDBInfo '= mRetailHost1.colTables 
        mBrowse1.IsSqlServer = True   '= mRetailHost1.IsSqlServer
        mBrowse1.DBname = msSqlDbName  '= mRetailHost1.DBname

        '--  get table/prefs info for this host..--
        mBrowse1.tableName = "supplier"  '==sHostTablename

        '= mBrowse1.FlexGrid = MSHFlexGrid1
        mBrowse1.DataGrid = dgvSupplierList

        '--  pass controls..--
        mBrowse1.showRecCount = labRecCount '--updates rec. retrieval..
        mBrowse1.showFind = LabFind '--updates Sort Column display..
        mBrowse1.showTextFind = txtFind '--updates Sort Column display..
        '= sWhere = msMakeStockFilter()  '--service or not..-
        '-- add srch args..
        If (sSrchWhereCond <> "") Then
            If (sWhere <> "") Then
                sWhere &= " AND "
            End If
            sWhere &= sSrchWhereCond
        End If
        mBrowse1.WhereCondition = sWhere
        mBrowse1.PreferredColumns = mColPrefsSupplier '== mColPrefs
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

    Private Function mbBrowseSupplierTable(Optional ByRef sSrchWhereCond As String = "") As Boolean
        Dim sWhere As String = ""

        If (mBrowse1 Is Nothing) Then
            Call mbInitialiseBrowse()
        Else
            sWhere = ""  '=LATER=   msMakeStockFilter()  '--service or not..-
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

    '-- L o a d --

    Private Sub ucChildSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        grpBoxSupplier.Text = ""
        txtSupplierSearch.Text = ""
        FrameBrowse.Text = ""

        labSupplierName.Text = ""
        Me.Text = "JobMatixPOS- Supplier Info and Admin."

        '== grpBoxGoods.Text = ""
        mIntFormDesignWidth = Me.Width  '--save starting dim.
        mIntFormDesignHeight = Me.Height  '--save starting dim.

        '-- get system Info table data.-
        '==3301.428= 
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        msBusinessName = mSysInfo1.item("BUSINESSNAME")

        '= panelSupplierDetail.Enabled = False
        txtContactName.Text = ""
        txtPhone.Text = ""
        txtFax.Text = ""
        txtEmail.Text = ""
        txtWebsite.Text = ""
        txtAddress.Text = ""
        txtComments.Text = ""

        btnEdit.Enabled = False
        btnNew.Enabled = False
        btnLookupGoods.Enabled = False
        '= labEditing.Visible = False
        '= labAddingNew.Visible = False

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
        mColPrefsSupplier.Add("date_created")
        mColPrefsSupplier.Add("date_modified")

        mColTable = mColSqlDBInfo.Item("supplier")
        '--Set mColTableInfo = mColTable(1)
        mColFields = mColTable.Item("FIELDS")
        mColPrimaryKeys = mColTable.Item("PRIMARYKEYS")
        mColOtherIndexes = mColTable.Item("OTHERINDEXES")

        btnNew.Enabled = True

        labGettingData.Text = ""
        Call mbInitialiseBrowse()

        mbIsInitialising = False  '-must be after browse..


    End Sub  '--load-
    '= = = = == = = = ===  =
    '-===FF->

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        Dim lRow As Integer
        '== Dim frmEdit1 As frmEdit

        lRow = dgvSupplierList.CurrentCell.RowIndex   '==  MSHFlexgrid1.Row

        If (lRow >= 0) Then '--ok row--
            '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol

            '-- set up row collections..-
            Call mSelectRecord(lRow)
            Call mbEditRecord(lRow)
        End If '--row--

    End Sub  '-edit
    '= = = = = = = = = 

    '-- Lookup-

    Private Sub btnLookupGoods_Click(sender As Object, e As EventArgs) Handles btnLookupGoods.Click
        Dim frmlookup1 As New frmLookupGoods

        '= frmlookup1.SqlServer = msServer
        frmlookup1.SqlServer = msServer
        frmlookup1.connectionSql = mCnnSql '-- sql connenction..-
        frmlookup1.dbInfoSql = mColSqlDBInfo

        frmlookup1.DBname = msSqlDbName
        frmlookup1.VersionPOS = msVersionPOS
        frmlookup1.supplier_id = mIntSupplier_id
        frmlookup1.SupplierName = labSupplierName.Text
        frmlookup1.SupplierBarcode = msSupplierBarcode
        frmlookup1.staffNname = msStaffName
        frmlookup1.BusinessName = msBusinessName

        frmlookup1.ShowDialog()
        frmlookup1.Close()

    End Sub '--Lookup-
    '= = = = = = = = = = =
    '-===FF->

    '-New Supplier-

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Dim frmEdit1 As frmEdit

        frmEdit1 = New frmEdit
        frmEdit1.connection = mCnnSql '--job tracking sql connenction..-
        frmEdit1.colTables = mColSqlDBInfo
        frmEdit1.DBname = msSqlDbName
        frmEdit1.tableName = "supplier" '--"jobs"
        '== frmEdit1.IsSqlServer = True '--bIsSqlServer
        frmEdit1.newRecord = True
        frmEdit1.StaffId = mIntStaff_id
        frmEdit1.StaffName = msStaffName
        frmEdit1.versionPOS = msVersionPOS

        '--- set WHERE condition for jobStatus..--
        frmEdit1.PreferredColumns = mColPrefsSupplier
        frmEdit1.Title = "Add New Supplier record"

        frmEdit1.ShowDialog()

        frmEdit1.Close()
        '--get new r/set, load grid--
        '= If mbReload() Then
        '= End If

    End Sub  '--new-
    '= = = = = = == = = = =
    '-===FF->


    '--  Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvSupplierList_Sorted(ByVal sender As Object,
                                      ByVal e As System.EventArgs) Handles dgvSupplierList.Sorted
        Dim sName As String

        If mbIsInitialising Then
            Exit Sub
        End If
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvSupplierList.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowse1.SortColumn(sName)
    End Sub
    '= = = = = = = = =  = = =
    '= = = = = = = = =  = = =
    '-===FF->

    '-- cell click.--
    '-- cell click.--

    Private Sub dgvSupplierList_CellMouseClickEvent(ByVal eventSender As System.Object,
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvSupplierList.CellMouseClick
        Dim lRow, lCol As Integer
        Dim intSupplier_id As Integer
        Dim colKeys, colRowValues As Collection

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (dgvSupplierList.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                intSupplier_id = CInt(colRowValues("Supplier_id")("value")) '= colKeys.Item(1) --
                If (intSupplier_id >= 0) And (intSupplier_id <> mIntSupplier_id) Then '-- has changed..-
                    Call mbShowSupplierInfo(intSupplier_id)
                End If
            End If  '-selected-
        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =

    '-SelectionChanged-

    Private Sub dgvSupplierList_SelectionChanged(ByVal eventSender As System.Object,
                                                      ByVal eventArgs As EventArgs) Handles dgvSupplierList.SelectionChanged
        Dim ix, intRowIndex, intSupplier_id As Integer
        Dim colKeys, colRowValues As Collection

        If mbIsInitialising Or (dgvSupplierList.CurrentCell Is Nothing) Then
            Exit Sub
        End If
        intRowIndex = dgvSupplierList.CurrentCell.RowIndex
        If (intRowIndex >= 0) And (dgvSupplierList.Rows.Count > 0) Then  '--selected a row.--
            mLngSelectedRow = intRowIndex
            Call mBrowse1.SelectRecord(intRowIndex, colKeys, colRowValues)
            intSupplier_id = CInt(colRowValues("Supplier_id")("value")) '= colKeys.Item(1) --
            If (intSupplier_id >= 0) And (intSupplier_id <> mIntSupplier_id) Then '-- has changed..-
                Call mbShowSupplierInfo(intSupplier_id)
            End If
        End If  '-selected-
    End Sub  '-SelectionChanged-
    '= = = = = = = = = ==  = = = 
    '-===FF->

    '--mouse activity---  
    '-- select row to edit--
    '-- select row to edit--

    Private Sub dgvSupplierList_CellMouseDblClickEvent(ByVal eventSender As System.Object,
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvSupplierList.CellMouseDoubleClick
        Dim lRow As Integer
        Dim intSupplier_id As Integer
        Dim colKeys, colRowValues As Collection

        '== lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow >= 0) Then '--ok--
            mLngSelectedRow = lRow
            '--  get customer id and start edit.--
            If (lRow >= 0) And (dgvSupplierList.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                intSupplier_id = CInt(colRowValues("Supplier_id")("value"))  '=colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                If (intSupplier_id >= 0) And (intSupplier_id <> mIntSupplier_id) Then '-- has changed..-
                    Call mbShowSupplierInfo(intSupplier_id)
                End If
            End If  '-selected-
        End If '--row--
    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '-- Supplier Browser.. txt FIND Activity.--
    '-- Supplier Browser.. txt FIND Activity.--
    '--BROWSING Supplier.. --
    '--BROWSING..  catch Find text box changes..-

    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object,
                                      ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged

        Call mBrowse1.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = = = = =

    '-- key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object,
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        Dim intSupplier_id As Integer
        Dim colKeys, colRowValues As Collection

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If dgvSupplierList.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = dgvSupplierList.SelectedRows(0).Cells(0).RowIndex
                If (lRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow
                    '= Call mbSelectStockRow(mLngSelectedRow)
                    Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                    intSupplier_id = CInt(colRowValues("Supplier_id")("value")) '= colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                    If (intSupplier_id >= 0) And (intSupplier_id <> mIntSupplier_id) Then '-- has changed..-
                        Call mbShowSupplierInfo(intSupplier_id)
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

    Private Sub txtFind_Enter(ByVal eventSender As System.Object,
                                ByVal eventArgs As System.EventArgs) Handles txtFind.Enter
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, True)
    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFind_Leave(ByVal eventSender As System.Object,
                                 ByVal eventArgs As System.EventArgs) Handles txtFind.Leave
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '-- supplier Browser..  Full text Search..--
    '=3519.0221-  Catch Enter Key on SUPP srch text-

    Private Sub txtSupplierSearch_keyPress(ByVal sender As System.Object,
                                         ByVal EventArgs As KeyPressEventArgs) Handles txtSupplierSearch.KeyPress
        Dim keyAscii As Short = Asc(EventArgs.KeyChar)
        Dim e2 As New EventArgs
        If keyAscii = 13 Then '--enter-
            Call cmdSupplierSearch_Click(cmdSupplierSearch, e2)
            keyAscii = 0 '--processed..-
        End If  '13-
        EventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            EventArgs.Handled = True
        End If

    End Sub  '-keypress.
    '= = = = = = = = == 
    '-===FF->

    '-- S e a r c h --

    Private Sub cmdSupplierSearch_Click(sender As Object, e As EventArgs) Handles cmdSupplierSearch.Click
        Dim sWhere As String = ""
        Dim sSql As String '--search sql..-- 
        '= Dim s1, s2 As String
        Dim asColumns As Object

        '--  rebuild Search Columns and call makeTextSearch...-

        '-- arg can be multiple tokens..--
        '===asArgs = Split(Trim(txtSearch(index).Text))
        '--  now in the Interface..--
        '== asColumns = mRetailHost1.stockSearchColumns()

        asColumns = New Object() _
                      {"barcode", "supplierName", "contactName", "phone", "webSiteURL", "emailAddress"}

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtSupplierSearch.Text), asColumns)
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere + " AND "
            sWhere = sWhere + sSql
        End If
        Call mbBrowseSupplierTable(sWhere)

    End Sub  '--search-
    '= = = = = = = == = = = = = ==  

    Private Sub cmdClearSupplierSearch_Click(sender As Object, e As EventArgs) Handles cmdClearSupplierSearch.Click
        txtSupplierSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdSupplierSearch_Click(cmdSupplierSearch, New System.EventArgs())

    End Sub  '--clear-
    '= = = = = = = = = = =

    '==  END of supplier BROWSING..--
    '==  END of supplier BROWSING..--
    '-===FF->

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If (msModifiedControls <> "") Then
            If (MsgBox("Abandon changes ?",
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

End Class  '-ucChildSupplier-
'= = = = = =  = = = = = == = == =
