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

Public Class frmPOS34Setup

    '- POS31 Admin form..

    '-- grh JobMatixPOS31  v3.1.3101.1007 -

    '==
    '==  grh. JobMatix 3.1.3103.0302 ---  02Mar2005 ===
    '==   >>  Add Defs for Labour Charge Stock Items.-
    '==
    '==
    '==  grh. JobMatix 3.1.3103.0331 ---  31Mar2005 ===
    '==   >>  Add Defs for Bank Account Details..-
    '==   >>  Add TABCONTROL..
    '==
    '==  grh. JobMatix 3.1.3107.0813 ---  13-Aug-2015 ===
    '==   >>  GR Form now DOUBLES as PO and Goods Recvd.. 
    '==
    '==  grh. JobMatix 3.1.3107.0905 ---  05-Sep-2015 ===
    '==   >>  GR Form now DOUBLES as PO abd Goods Recvd.. 
    '==
    '==  grh. JobMatixPOS 3.2.3201.201 ---  01-Feb-2016 ===
    '==    >> Add StockTake  --
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==     v3.3.3301.510..  10-May-2016= ===
    '==       >>  Updates for Serial Lookup...
    '==
    '==    3301.704= 04Jul2016-
    '==-         >>  -P r i n t Till Balance - (via Reports).
    '==
    '==     3301.711= 11Jul2016-=
    '==       >> Add: mColPrefsStock.Add("isNonStockItem")
    '==
    '==     v3.3.3301.816..  16-August-2016= ===
    '==        >> Add Inputs to GoodsRecvd. for colPrefsSuppliers and colPrefsStock-
    '==  
    '==     v3.3.3303.0119..  19-Jan-2017= ===
    '==       >> Add more columns to Staff Browse/Update..
    '==   
    '==     v3.3.3307.0202/5..  02/08-Feb-2017= ===
    '==       >> Credit Notes.. 
    '==              Add button/form to POS Admin to show all credit-note history...
    '==              AND add Report printer Combo-
    '==
    '==     v33.3.3307.0221 =
    '==      -- Added class ShapedPanel for rounded corners Panel..
    '==
    '==     v3.4.3401.0319- 10Mar2017- =
    '==      -- Dropped all admin function buttons..
    '==               NOW is setup/options/....
    '==
    '==
    '==     3403.0915- 15-Sep-2017-
    '==      -- POS Setup/Options. Add Cust.Pricing Grade Rates...0..4.
    '==                and delete obsolete labour Rate stuff.
    '==      -- (panelLabourItems dropped)..
    '== 
    '==
    '==     3403.1007- 07-Oct-2017-
    '==      -- POS Setup/Options. Update Emailing Defaults..
    '==
    '==     3501.0930- --   30Sep2018-
    '==        -- Backup- Configure Server Paths..
    '==
    ''= = = =  = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Private Const k_reportPrtSettingKey As String = "POS_ReportPrinter"
    Private Const k_receiptPrtSettingKey As String = "POS_ReceiptPrinter"

    Private mbIsInitialising As Boolean = True
    Private mbFormLoading As Boolean = False
    Private mbActivated As Boolean = False

    Private mFrmParent As Form   '-- for locating Me..

    Private msServer As String = ""
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""
    Private msSqlVersion As String = ""
    Private mlngSqlMajorVersion As Integer = 0

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private mbIsSqlAdmin As Boolean = False

    Private msVersionPOS As String = ""

    Private msComputerName As String '--local machine--
    Private msAppPath As String
    '= Private msLastSqlErrorMessage As String = ""
    Private mbRunningOnServer As Boolean = False

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    '= Private mlJobId As Integer = -1

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    '=3301.428= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private msDefaultPrinterName As String = ""
    Private msReportprinterName As String = ""
    Private msReceiptPrinterName As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = =

    '--  Browser prefs..--

    Private mColPrefsStaff As Collection
    Private mColPrefsCustomer As Collection
    Private mColPrefsCategory1 As Collection
    Private mColPrefsCategory2 As Collection
    Private mColPrefsBrands As Collection

    Private mColPrefsStock As Collection
    Private mColPrefsSupplier As Collection
    '= = = = = = = = = = = = = = = = = = = =

    Private msBusinessABN As String
    '== Private msBusinessUser As String
    '== Private msJT2SecurityIdOriginal As String '--as stored in SytemInfo in Row "JT2SecurityId"..-
    '== Private msJT2SecurityId As String '-- AS computed from ABN DateCreated.  --
    Private msBusinessName As String
    Private msBusinessAddress1 As String
    Private msBusinessAddress2 As String
    Private msBusinessShortName As String
    Private msBusinessPhone As String
    Private msBusinessPostCode As String
    Private msBusinessState As String

    Private mDecGST_rate As Decimal = 10  '--default-

    Private mImageUserLogo As Image

    Private mIntLabourStock_id_pr1 As Integer = -1
    Private mIntLabourStock_id_pr2 As Integer = -1
    Private mIntLabourStock_id_pr3 As Integer = -1

    '=-- SMTP stuff- 3107.901--
    '=-- SMTP stuff- 3107.901--

    '--SAVE original SMTP Mail SystemInfo Settings..
    Private msSMTPHostName As String = ""
    Private mIntSMTPHostPort As Integer = 25    '--default..-

    Private msSMTPUsername As String = ""
    Private msSMTPPassword As String = ""

    Private mbSMTPDataChanged As Boolean = False

    '= = = = = = = = = = = = = = = = = = = =
    '-===FF->
    '-- Browse Selected table using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseTable(ByRef colPrefs As Collection, _
                                    ByRef sTitle As String, _
                                      ByRef sWhere As String, _
                                      ByRef colKeys As Collection, _
                                      ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Customer", _
                                         Optional ByVal bLookupOnly As Boolean = False) As Boolean
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
        '=3101.1207== add staff.-
        frmBrowse1.StaffId = mIntStaff_id
        frmBrowse1.StaffName = msStaffName
        frmBrowse1.versionPOS = msVersionPOS
        If bLookupOnly Then
            frmBrowse1.HideEditButtons = True
            frmBrowse1.lookupSelection = True
        Else '- can edit records-
            frmBrowse1.HideEditButtons = False
            frmBrowse1.lookupSelection = False
        End If
        '--go-
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

    '-- save systemInfo value..

    Private Function mbSaveSystemValue(ByRef txt1 As TextBox, _
                                  ByVal sSystemkey As String, _
                                  Optional ByVal bNotifyOK As Boolean = True) As Boolean

        Dim asChanges(1) As String '--update list..-
        mbSaveSystemValue = False
        If (Trim(txt1.Text) <> "") Then  '= AndAlso IsNumeric(txt1.Text) Then
            Try
                asChanges(0) = sSystemkey  '= "POS_sell_margin"
                asChanges(1) = Trim(txt1.Text)
                If Not mSysInfo1.UpdateSystemInfo(asChanges) Then  '= gbUpdateSystemInfo(mCnnSql, asChanges) Then
                    MsgBox("Couldn't save " & sSystemkey & " setting.", MsgBoxStyle.Exclamation)
                Else
                    mbSaveSystemValue = True
                    If bNotifyOK Then MsgBox("Settings saved ok..", MsgBoxStyle.Information)
                End If
            Catch ex As Exception
                MsgBox("Error in 'mSaveSystemValue'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
        End If
    End Function '-mbSaveSystemValue-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--Constructor-
    '--Constructor-

    Public Sub New(ByRef frmParent As Form, _
                     ByVal sServer As String, _
                      ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                       ByRef colSqlDBInfo As Collection, _
                       ByVal sVersionPOS As String, _
                       ByRef imageUserLogo As Image, _
                        ByVal intStaff_id As Integer, _
                         ByVal sStaffName As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        mFrmParent = frmParent
        msServer = sServer
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo
        msVersionPOS = sVersionPOS
        mImageUserLogo = imageUserLogo

        mIntStaff_id = intStaff_id
        msStaffName = sStaffName

        msComputerName = My.Computer.Name

        '--- Separate the "SQL-Server\InstanceName" bits-- if needed..
        If (msServer = "\") Then
            msSqlServerComputer = msComputerName
        Else
            Dim kx As Integer = InStr(msServer, "\")
            If (kx > 0) Then '-have instance..--
                msSqlServerComputer = VB.Left(msServer, kx - 1)
                msSqlServerInstance = Mid(msServer, kx + 1)
                If msSqlServerComputer = "" Then '--local instance..
                    msSqlServerComputer = msComputerName
                End If
            Else '--default instance,..-
                msSqlServerComputer = msServer '--no instance name included..-
            End If
        End If '--ni name-

    End Sub  '--new --
    '= = =  = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- mbGetStockInfo-

    Private Function mbGetStockInfo(ByVal intStock_id As Integer, _
                                     ByRef sDescription As String, _
                                     ByRef decPrice As Decimal) As Boolean
        Dim sSql, sTaxcode As String
        Dim dtStock As DataTable

        sSql = "SELECT * FROM [stock] WHERE (stock_id=" & CStr(intStock_id) & ");"
        If gbGetDataTable(mCnnSql, dtStock, sSql) AndAlso _
                               (Not (dtStock Is Nothing)) AndAlso (dtStock.Rows.Count > 0) Then
            With dtStock.Rows(0)
                sDescription = .Item("description")
                sTaxcode = .Item("sales_taxCode")
                decPrice = CDec(.Item("sellExTax"))
                If (UCase(sTaxcode) = "GST") Then
                    decPrice += ((decPrice * mDecGST_rate) / 100)
                End If
            End With
            mbGetStockInfo = True
        End If '-get-

    End Function '-get stock-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-Load-

    Private Sub frmPOS31Admin_Load(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) Handles MyBase.Load

        '=3301.428= Dim colSystemInfo As Collection
        Dim s1 As String

        '--  set up browser prefs..--
        '--  These are the DB Table fields that we need to display
        '--     in browser, and for edit/insert operations.

        '--  NB:  MUST include Primary key AN'D Foreign Keys..
        '--  NB:  MUST include Primary key AND Foreign Keys..
        '--  NB:  MUST include Primary key AND Foreign Keys..

        '== TABLE:  'Staff' has columns: 
        'staff_id; [Type:3] DefSize:0;  sqlType:int; NOT NULL IDENTITY 
        'barcode; [Type:130] DefSize:30;  sqlType:nvarchar(15); NOT NULL
        'lastName; [Type:130] DefSize:100;  sqlType:nvarchar(50); NOT NULL
        'firstName; [Type:130] DefSize:100;  sqlType:nvarchar(50); NOT NULL
        'docket_name; [Type:130] DefSize:100;  sqlType:nvarchar(50); NOT NULL
        'position; [Type:130] DefSize:100;  sqlType:nvarchar(50); NOT NULL DEFAULT ('')
        'isAdministrator; [Type:11] DefSize:0;  sqlType:bit; NOT NULL DEFAULT ((0))
        'inactive; [Type:11] DefSize:0;  sqlType:bit; NOT NULL DEFAULT ((0))
        'dateOfBirth; [Type:135] DefSize:0;  sqlType:datetime; NOT NULL
        'address; [Type:130] DefSize:-1;  sqlType:nvarchar(-1); NOT NULL DEFAULT ('')
        'suburb; [Type:130] DefSize:80;  sqlType:nvarchar(40); NOT NULL DEFAULT ('')
        'state; [Type:130] DefSize:60;  sqlType:nvarchar(30); NOT NULL DEFAULT ('')
        'postcode; [Type:130] DefSize:20;  sqlType:nvarchar(10); NOT NULL DEFAULT ('')
        'homePhone; [Type:130] DefSize:40;  sqlType:nvarchar(20); NOT NULL DEFAULT ('')
        'mobile; [Type:130] DefSize:40;  sqlType:nvarchar(20); NOT NULL DEFAULT ('')
        'emailAddress; [Type:130] DefSize:500;  sqlType:nvarchar(250); NOT NULL DEFAULT ('')
        'status; [Type:130] DefSize:30;  sqlType:nvarchar(15); NOT NULL DEFAULT ('')
        'password; [Type:130] DefSize:160;  sqlType:nvarchar(80); NOT NULL DEFAULT ('')
        'passwordHint; [Type:130] DefSize:500;  sqlType:nvarchar(250); NOT NULL DEFAULT ('')
        'staffPicture; [Type:204] DefSize:2147483647;  sqlType:image; 
        'date_created; [Type:135] DefSize:0;  sqlType:datetime; NOT NULL DEFAULT (getdate())
        'date_modified; [Type:135] DefSize:0;  sqlType:datetime; NOT NULL DEFAULT (getdate())

        '--  staff--
        mColPrefsStaff = New Collection
        mColPrefsStaff.Add("docket_name")
        mColPrefsStaff.Add("barcode")
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

        '-- Supplier --
        mColPrefsSupplier = New Collection
        mColPrefsSupplier.Add("supplier_id")
        mColPrefsSupplier.Add("barcode")
        mColPrefsSupplier.Add("supplierName")
        mColPrefsSupplier.Add("contactName")
        mColPrefsSupplier.Add("contactPosition")
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

        '-- Customer --
        mColPrefsCustomer = New Collection
        mColPrefsCustomer.Add("barcode")
        mColPrefsCustomer.Add("lastname")
        mColPrefsCustomer.Add("firstname")
        mColPrefsCustomer.Add("companyName")
        mColPrefsCustomer.Add("phone")
        mColPrefsCustomer.Add("mobile")
        mColPrefsCustomer.Add("isAccountCust")
        mColPrefsCustomer.Add("creditLimit")
        mColPrefsCustomer.Add("pricingGrade")
        mColPrefsCustomer.Add("openedStaff_id")
        mColPrefsCustomer.Add("openedStaffname")
        mColPrefsCustomer.Add("inactive")
        mColPrefsCustomer.Add("address")
        '==mColPrefsCustomer.Add("addr2")
        '=mColPrefsCustomer.Add("addr3")
        mColPrefsCustomer.Add("suburb")
        mColPrefsCustomer.Add("email")
        mColPrefsCustomer.Add("customer_id")
        mColPrefsCustomer.Add("date_modified")
        mColPrefsCustomer.Add("comments")

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

        '--  stock--
        mColPrefsStock = New Collection
        mColPrefsStock.Add("barcode")
        mColPrefsStock.Add("cat1")   '--fkey-
        mColPrefsStock.Add("cat2")   '-fkey-
        mColPrefsStock.Add("description")
        mColPrefsStock.Add("productPicture")
        mColPrefsStock.Add("stock_id")
        '=3301.711=  mColPrefsStock.Add("isServiceItem")
        '=3301.711= mColPrefsStock.Add("isLabour")
        '=3301.711= 
        mColPrefsStock.Add("isNonStockItem")
        mColPrefsStock.Add("track_serial")
        mColPrefsStock.Add("inactive")
        mColPrefsStock.Add("supplier_id")
        mColPrefsStock.Add("brandName")
        mColPrefsStock.Add("costExTax")
        mColPrefsStock.Add("goods_TaxCode")
        mColPrefsStock.Add("sellExTax")
        mColPrefsStock.Add("sales_TaxCode")
        mColPrefsStock.Add("qtyInStock")
        '== mColPrefsStock.Add("qtyOnLayby")
        mColPrefsStock.Add("allow_renaming")
        mColPrefsStock.Add("comments")
        '-productPicture
        grpBoxAdmin.Text = ""

        txtSellMargin.Text = ""
        txtGST_percentage.Text = ""

        txtLabourDescr_pr1.Text = "-1:"
        txtLabourDescr_pr2.Text = "-1:"
        txtLabourDescr_pr3.Text = "-1:"

        labLabourPrice_pr1.Text = ""
        labLabourPrice_pr2.Text = ""
        labLabourPrice_pr3.Text = ""

        btnSaveLabour_pr1.Enabled = False

        btnSaveTerms.Enabled = False  '--must be here after we load text..
        btnSaveMargin.Enabled = False
        btnSaveGST.Enabled = False

        '=3403.915=
        btnSaveGrades.Enabled = False
        txtPriceGrade1.Text = ""
        txtPriceGrade2.Text = ""
        txtPriceGrade3.Text = ""
        txtPriceGrade4.Text = ""

        btnSaveFontName.Enabled = False
        btnSaveFontSize.Enabled = False
        txtStockBarcodeFontName.Text = ""
        txtStockBarcodeFontSize.Text = ""

        '- Terms and bank Acount=-
        txtPOS_Terms.Text = ""
        txtBankName.Text = ""
        txtAccountName.Text = ""
        txtBSB1.Text = ""
        txtBSB2.Text = ""
        txtAccountNo.Text = ""

        grpBoxEmailOptions.Text = ""

        '-- get system Info table data.-
        '=3301.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '=3301.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        msBusinessAddress1 = mSysInfo1.item("BUSINESSADDRESS1")
        msBusinessAddress2 = mSysInfo1.item("BUSINESSADDRESS2")
        msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
        msBusinessState = mSysInfo1.item("BUSINESSSTATE")
        msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
        msBusinessPhone = mSysInfo1.item("BUSINESSPHONE")

        If mSysInfo1.contains("POS_SELL_MARGIN") Then
            txtSellMargin.Text = mSysInfo1.item("POS_SELL_MARGIN")
        End If
        If mSysInfo1.contains("GSTPercentage") Then
            txtGST_percentage.Text = mSysInfo1.item("GSTPercentage")
            If IsNumeric(txtGST_percentage.Text) Then
                mDecGST_rate = CDec(txtGST_percentage.Text)
            End If
        End If

        '=3403.915=
        If mSysInfo1.contains("POS_CUSTPRICINGGRADE_COSTPLUS_1") Then
            txtPriceGrade1.Text = mSysInfo1.item("POS_CUSTPRICINGGRADE_COSTPLUS_1")
        End If
        If mSysInfo1.contains("POS_CUSTPRICINGGRADE_COSTPLUS_2") Then
            txtPriceGrade2.Text = mSysInfo1.item("POS_CUSTPRICINGGRADE_COSTPLUS_2")
        End If
        If mSysInfo1.contains("POS_CUSTPRICINGGRADE_COSTPLUS_3") Then
            txtPriceGrade3.Text = mSysInfo1.item("POS_CUSTPRICINGGRADE_COSTPLUS_3")
        End If
        If mSysInfo1.contains("POS_CUSTPRICINGGRADE_COSTPLUS_4") Then
            txtPriceGrade4.Text = mSysInfo1.item("POS_CUSTPRICINGGRADE_COSTPLUS_4")
        End If
        '=END- =3403.915=


        If mSysInfo1.contains("POS_BARCODEFONTNAME") Then
            txtStockBarcodeFontName.Text = mSysInfo1.item("POS_BARCODEFONTNAME")
        End If
        If mSysInfo1.contains("POS_BARCODEFONTSIZE") Then
            txtStockBarcodeFontSize.Text = mSysInfo1.item("POS_BARCODEFONTSIZE")
        End If

        '-- get Labour Stock Item Id's  --
        Dim intStock_id As Integer
        Dim sDescription As String = ""
        Dim decPrice As Decimal = 0
        'If mSysInfo1.contains("POS_LABOURSTOCKID_PR1") Then
        '    s1 = mSysInfo1.item("POS_LABOURSTOCKID_PR1")
        '    If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then  '-valid-
        '        intStock_id = CInt(s1)
        '        mIntLabourStock_id_pr1 = intStock_id
        '        '--lookup--
        '        If mbGetStockInfo(intStock_id, sDescription, decPrice) Then
        '            labLabourPrice_pr1.Text = FormatCurrency(decPrice, 2)
        '            txtLabourDescr_pr1.Text = CStr(intStock_id) & ": " & sDescription
        '        End If  '-get-
        '    End If  '-numeric-
        'End If '-PR1-
        ''-pr2-
        'If mSysInfo1.contains("POS_LABOURSTOCKID_PR2") Then
        '    s1 = mSysInfo1.item("POS_LABOURSTOCKID_PR2")
        '    If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then  '-valid-
        '        intStock_id = CInt(s1)
        '        mIntLabourStock_id_pr2 = intStock_id
        '        '--lookup--
        '        If mbGetStockInfo(intStock_id, sDescription, decPrice) Then
        '            labLabourPrice_pr2.Text = FormatCurrency(decPrice, 2)
        '            txtLabourDescr_pr2.Text = CStr(intStock_id) & ": " & sDescription
        '        End If  '-get-
        '    End If  '-numeric-
        'End If '-PR2-
        ''-pr3-
        'If mSysInfo1.contains("POS_LABOURSTOCKID_PR3") Then
        '    s1 = mSysInfo1.item("POS_LABOURSTOCKID_PR3")
        '    If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then  '-valid-
        '        intStock_id = CInt(s1)
        '        mIntLabourStock_id_pr3 = intStock_id
        '        '--lookup--
        '        If mbGetStockInfo(intStock_id, sDescription, decPrice) Then
        '            labLabourPrice_pr3.Text = FormatCurrency(decPrice, 2)
        '            txtLabourDescr_pr3.Text = CStr(intStock_id) & ": " & sDescription
        '        End If  '-get-
        '    End If  '-numeric-
        'End If '-PR3-

        '-Terms and bank Account-
        If mSysInfo1.contains("POS_ACCOUNTTERMS") Then
            txtPOS_Terms.Text = mSysInfo1.item("POS_ACCOUNTTERMS")
        End If
        If mSysInfo1.contains("POS_BusinessBankName") Then
            txtBankName.Text = mSysInfo1.item("POS_BusinessBankName")
        End If
        If mSysInfo1.contains("POS_BusinessAccountName") Then
            txtAccountName.Text = mSysInfo1.item("POS_BusinessAccountName")
        End If
        If mSysInfo1.contains("POS_BusinessAccountBSB1") Then
            txtBSB1.Text = mSysInfo1.item("POS_BusinessAccountBSB1")
        End If
        If mSysInfo1.contains("POS_BusinessAccountBSB2") Then
            txtBSB2.Text = mSysInfo1.item("POS_BusinessAccountBSB2")
        End If
        If mSysInfo1.contains("POS_BusinessAccountNo") Then
            txtAccountNo.Text = mSysInfo1.item("POS_BusinessAccountNo")
        End If
        '=3301.428= End If  '-load sys info--

        '== Call CenterForm(Me)
        '- position on top of calling form..
        If mFrmParent Is Nothing Then
            Call CenterForm(Me)
        Else
            Me.Left = mFrmParent.Left + 16
            Me.Top = mFrmParent.Top + 33
        End If
        labStaffName.Text = msStaffName
        labDLLversion.Text = msVersionPOS

        '=-- SMTP stuff- 3107.901--
        '=-- SMTP stuff- 3107.901--
        '--  load SMTP Mail SystemInfo Settings..
        '--- NB: SAME systemInfo settings as JobMatix--
        '--   SHARED (common) if both systems are included.--

        '--  SMTPServer     --
        '--  SMTPUsername -  SMTPPassword   --
        chkHostUsesSSL.Checked = False

        '==3301.428= Re-code all that=
        If mSysInfo1.contains("smtphostname") Then
            msSMTPHostName = mSysInfo1.item("smtphostname")
            txtSMTPServer.Text = msSMTPHostName
        End If
        If mSysInfo1.contains("smtphostport") Then
            mIntSMTPHostPort = mSysInfo1.item("smtphostport")
        End If
        If mSysInfo1.contains("smtphostusesssl") Then
            s1 = mSysInfo1.item("smtphostusesssl")
            If (VB.Left(UCase(s1), 1) = "Y") Then
                chkHostUsesSSL.Checked = True
            End If
        End If
        If mSysInfo1.contains("smtpusername") Then
            msSMTPUsername = mSysInfo1.item("smtpusername")
            txtSMTPUsername.Text = msSMTPUsername
        End If
        If mSysInfo1.contains("smtppassword") Then
            msSMTPPassword = mSysInfo1.item("smtppassword")
            txtSMTPPassword1.Text = msSMTPPassword
        End If
        '==3301.428= DONE Re-code all that=



        txtSMTPHostPort.Text = CStr(mIntSMTPHostPort)
        frameSMTPSettings.Text = ""

        '-- email texts.--
        btnSaveEmailTexts.Enabled = False
        '-set defaults..
        'txtEmailTextInvoice.Text = vbCrLf & "(POS System- Beta test version).." & vbCrLf & _
        '                            "Please find attached your invoice as per above." & vbCrLf & _
        '                            "Thank You." & vbCrLf & "&&BusinessName"
        'txtEmailTextStatement.Text = vbCrLf & "(POS System- Beta test version).." & vbCrLf & _
        '                            "Please find attached your statement as per above." & vbCrLf & _
        '                            "Thank You." & vbCrLf & "&&BusinessName"
        'txtEmailTextPO.Text = vbCrLf & "(POS System- Beta test version).." & vbCrLf & _
        '                            "Please find attached our Purchase Order as per above." & vbCrLf & _
        '                            "Thank You." & vbCrLf & "&&BusinessName"
 
        '=3403.1007- load default. if new system.
        If mSysInfo1.contains("POS_emailtextinvoice") Then
            txtEmailTextInvoice.Text = mSysInfo1.item("POS_emailtextinvoice")
        Else  '=3403.1007- load default.
            '-set defaults..
            txtEmailTextInvoice.Text = vbCrLf & "&&subject" & vbCrLf & "&&greeting" & vbCrLf & _
                                        "Please find attached your invoice as per above." & vbCrLf & _
                                        "Thank You." & vbCrLf & "&&BusinessName"
            btnSaveEmailTexts.Enabled = True  '-was changed-
        End If
        If mSysInfo1.contains("POS_emailtextstatement") Then
            txtEmailTextStatement.Text = mSysInfo1.item("POS_emailtextstatement")
        Else
            '-set defaults..
            txtEmailTextStatement.Text = vbCrLf & "&&subject" & vbCrLf & "&&greeting" & vbCrLf & _
                                       "Please find attached your statement as per above." & vbCrLf & _
                                       "Thank You." & vbCrLf & "&&BusinessName"
            btnSaveEmailTexts.Enabled = True  '-was changed-
        End If
        If mSysInfo1.contains("POS_emailtextpo") Then
            txtEmailTextPO.Text = mSysInfo1.item("POS_emailtextpo")
        Else
            '-set defaults..
            txtEmailTextPO.Text = vbCrLf & "&&subject" & vbCrLf & "&&greeting" & vbCrLf & _
                                "Please find attached our Purchase Order as per above." & vbCrLf & _
                                "Thank You." & vbCrLf & "&&BusinessName"
            btnSaveEmailTexts.Enabled = True  '-was changed-
        End If
        '==3301.428= DONE Re-code all that=

        '=3403.1007- load values for Allow Email.
        If mSysInfo1.contains("POS_ALLOW_EMAIL_INVOICES") Then
            If (VB.Left(UCase(mSysInfo1.item("POS_ALLOW_EMAIL_INVOICES")), 1) = "Y") Then
                chkAllowEmailInvoices.Checked = True
            Else
                chkAllowEmailInvoices.Checked = False
            End If
        Else
            '-set defaults..
            chkAllowEmailInvoices.Checked = True
            btnSaveEmailTexts.Enabled = True  '-was changed-
        End If  '-contains-

        If mSysInfo1.contains("POS_ALLOW_EMAIL_STATEMENTS") Then
            If (VB.Left(UCase(mSysInfo1.item("POS_ALLOW_EMAIL_STATEMENTS")), 1) = "Y") Then
                chkAllowEmailStatements.Checked = True
            Else
                chkAllowEmailStatements.Checked = False
            End If
        Else
            '-set defaults..
            chkAllowEmailStatements.Checked = True
            btnSaveEmailTexts.Enabled = True  '-was changed-
        End If  '-contains-

        Me.Text = "JobMatix POS Admin Functions.."

        '-- get report printer..-
        '-- get report printer..-
        '-- get report printer..-

        Dim pd1 As New PrintDocument()
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim sName As String

        msDefaultPrinterName = ""  '== Printer.DeviceName '--  save name of original default printer..-
        cboReportPrinters.Items.Clear()
        cboReceiptPrinters.Items.Clear()
        '=3300.428= Call mbLoadSettings()
        msSettingsPath = gsLocalSettingsPath() '= "JobMatix33" default.
        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboReportPrinters.Items.Add(sName)
                cboReceiptPrinters.Items.Add(sName)
            Next sName
            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_reportPrtSettingKey) AndAlso _
                         mLocalSettings1.item(k_reportPrtSettingKey) <> "" Then
                '== gbQueryLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, s1) AndAlso (s1 <> "") Then
                s1 = mLocalSettings1.item(k_reportPrtSettingKey)
                If colPrinters.Contains(s1) Then '--set it- 
                    cboReportPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboReportPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboReportPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            ''-receipt-
            If mLocalSettings1.exists(k_receiptPrtSettingKey) AndAlso _
                    (mLocalSettings1.item(k_receiptPrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_receiptPrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it-
                    cboReceiptPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
                End If
            Else '-no pref-
                If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query receipt prt.--

        End If '-getAvail.-  
        msReportprinterName = cboReportPrinters.SelectedItem
        cboReportPrinters.Enabled = True

        panelLabourItems.Enabled = False   '=3403.915-- now dropped.

        '==
        '==     3501.0930- --   30Sep2018-
        '==        -- Backup- Configure Server Paths..
        '--  Enabled only when running on server..

        mbRunningOnServer = (UCase(msSqlServerComputer) = UCase(msComputerName))
        If mbRunningOnServer Then
            frameBackupPath.Enabled = True
        Else
            frameBackupPath.Enabled = False
        End If
        cmdSaveServerPath.Enabled = False

        LabHelpBackupPath.Text = "Server/Client-side backup of JobMatix Database:" & vbCrLf & _
                         "When backing-up the JobMatix database from a client computer, " & _
                           "the SQL Server Backup function needs to write the backup file to " & _
                             "a folder path accessible from the SQL server machine.."

        '-- load current values.

        txtServerBackupFolderLocal.Tag = "ServerShareLocalPath"
        txtServerBackupFolderNetworkPath.Tag = "ServerShareNetworkPath"

        txtServerBackupFolderLocal.Text = ""
        txtServerBackupFolderNetworkPath.Text = ""

        If mSysInfo1.contains("ServerShareLocalPath") Then
            txtServerBackupFolderLocal.Text = mSysInfo1.item("ServerShareLocalPath")
        End If

        If mSysInfo1.contains("ServerShareNetworkPath") Then
            txtServerBackupFolderNetworkPath.Text = mSysInfo1.item("ServerShareNetworkPath")
        End If
        '-- done server share stuff..

        mbIsInitialising = False

    End Sub  '--load -
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--Activated-

    Private Sub frmPOS34Setup_Activated(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) Handles MyBase.Activated

        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub '--activated-
    '= = = = = = = = = = = 


    Private Sub frmPOS34Setup_Shown(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) Handles MyBase.Shown

        '= If mbActivated Then Exit Sub
        '= mbActivated = True

        btnSaveSMTP.Enabled = False

        '-- show SMTP shared settings note if JobTracking is in this system..
        labJobTrackingSharing.Visible = False
        If mColSqlDBInfo.Contains("jobs") Then
            labJobTrackingSharing.Visible = True
        End If

        '== btnPurchasing.Enabled = False   '--not available yet.-

        '== btnStock.Select()   '=btnAccountPayments.Select()

        '=3403.1007=
        If btnSaveEmailTexts.Enabled Then
            MsgBox("Please Note:" & vbCrLf & _
                "Default Email permissions/texts have been displayed.. " & vbCrLf & _
                "  Modify and/or Save them for Emailing Invoices etc. ", MsgBoxStyle.Information)
        End If

    End Sub  '-shown-
    '= = = = =  = = = = = = = = = =
    '-===FF->

    ''= = = = Printers = = = = = = = = = =  = = =

    Private Sub cboReportPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles cboReportPrinters.SelectedIndexChanged

        If (cboReportPrinters.SelectedIndex >= 0) Then
            msReportprinterName = cboReportPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_reportPrtSettingKey, msReportprinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, msReportPrinterName) Then
                MsgBox("Failed to save report printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-

    End Sub  '- selected index-
    '= = = = = = = = = = = = = = =

    Private Sub cboReceiptPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                    ByVal e As System.EventArgs) _
                                                       Handles cboReceiptPrinters.SelectedIndexChanged
        '= Dim sName As String
        If (cboReceiptPrinters.SelectedIndex >= 0) Then
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_receiptPrtSettingKey, msReceiptPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, msReceiptPrinterName) Then
                MsgBox("Failed to save Receipt printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '-ReceiptPrinters-
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Settings--

    Private Sub txtSellMargin_TextChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles txtSellMargin.TextChanged
        If mbIsInitialising Then Exit Sub
        If (Trim(txtSellMargin.Text) <> "") AndAlso IsNumeric(txtSellMargin.Text) Then
            btnSaveMargin.Enabled = True
        Else
            btnSaveMargin.Enabled = False
        End If
    End Sub  '-txtSellMargin-
    '= = = = =  = = = = == = ==  

    Private Sub txtGST_percentage_TextChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles txtGST_percentage.TextChanged

        If mbIsInitialising Then Exit Sub
        If (Trim(txtGST_percentage.Text) <> "") AndAlso IsNumeric(txtGST_percentage.Text) Then
            btnSaveGST.Enabled = True
        Else
            btnSaveGST.Enabled = False
        End If
    End Sub '-txtGST_percentage-
    '= = = = = = = = = = = = = = 
    '= = = = = = = = = = == =
    '-===FF->

    '-- txtPriceGrade-

    Private Sub txtPriceGrade1_TextChanged(sender As Object, _
                                            e As EventArgs) Handles txtPriceGrade1.TextChanged, _
                                                               txtPriceGrade2.TextChanged, _
                                                               txtPriceGrade3.TextChanged, _
                                                               txtPriceGrade4.TextChanged
        If mbIsInitialising Then Exit Sub

        If ((Trim(txtPriceGrade1.Text) <> "") AndAlso IsNumeric(txtPriceGrade1.Text)) AndAlso _
              ((Trim(txtPriceGrade2.Text) <> "") AndAlso IsNumeric(txtPriceGrade2.Text)) AndAlso _
                 ((Trim(txtPriceGrade3.Text) <> "") AndAlso IsNumeric(txtPriceGrade3.Text)) AndAlso _
                     ((Trim(txtPriceGrade4.Text) <> "") AndAlso IsNumeric(txtPriceGrade4.Text)) Then
            btnSaveGrades.Enabled = True
        Else
            btnSaveGrades.Enabled = False
        End If

    End Sub   '- txtPriceGrade-
    '= = = = = = = = = = == =

    '-- Handle ENTER for all Grade textboxes..
    '--   Enter Key Pressed --

    Public Sub txtPriceGrade1_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                Handles txtPriceGrade1.KeyPress, _
                                                txtPriceGrade2.KeyPress, _
                                                txtPriceGrade3.KeyPress, _
                                                txtPriceGrade4.KeyPress
        If mbIsInitialising Then Exit Sub
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent
        Dim sData As String = Trim(textBox1.Text)
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If (keyAscii = 13) Then '--enter-
            '--  Just use validate--
            controlParent.SelectNextControl(textBox1, True, True, True, True)
            '--and then go to next-
            keyAscii = 0  '-done-
            eventArgs.Handled = True
        End If  '-13-

    End Sub '- txtPriceGrade1_KeyPress-
    '= = = = = = =  = = = = = == =  === =
    '-===FF->


    Private Sub txtPriceGrade1_validating(ByVal sender As System.Object, _
                                           ByVal ev As CancelEventArgs) _
                                            Handles txtPriceGrade1.Validating, _
                                            txtPriceGrade2.Validating, _
                                            txtPriceGrade3.Validating, _
                                            txtPriceGrade4.Validating
        Dim txt1 As TextBox = CType(sender, TextBox)
        If (txt1.Text <> "") AndAlso IsNumeric(txt1.Text) AndAlso _
                                   (CDec(txt1.Text) >= 0) AndAlso (CDec(txt1.Text) <= 100) Then '-ok-
        Else
            ev.Cancel = True
            MsgBox("Note: Cost-Plus percentage must be numeric only, " & vbCrLf & _
                                  "  and not empty, and Less than 100..", MsgBoxStyle.Exclamation)
        End If
    End Sub '- txtPriceGrade- validating.-
    '= = = = = =  = = = = = = = = = ==

    '- validated- reformat..

    Private Sub txtPriceGrade1_validated(ByVal sender As System.Object, _
                                          ByVal ev As System.EventArgs) Handles txtPriceGrade1.Validated, _
                                                                       txtPriceGrade2.Validated, _
                                                                       txtPriceGrade3.Validated, _
                                                                       txtPriceGrade4.Validated
        Dim txt1 As TextBox = CType(sender, TextBox)
        txt1.Text = Format(CDec(txt1.Text), " 0.00")

    End Sub '- txtPriceGrade- validated..-
    '= = = ==  == = = = = = = = = = =
    '-===FF->


    Private Sub txtStockBarcodeFontName_TextChanged(ByVal sender As System.Object, _
                                                   ByVal e As System.EventArgs) _
                                                     Handles txtStockBarcodeFontName.TextChanged
        If mbIsInitialising Then Exit Sub
        If (Trim(txtStockBarcodeFontName.Text) <> "") Then
            btnSaveFontName.Enabled = True
        Else
            btnSaveFontName.Enabled = False
        End If
    End Sub  '-StockBarcodeFontName-
    '= = = = = = = = = = = = = = = = 

    Private Sub txtStockBarcodeFontSize_TextChanged(ByVal sender As System.Object, _
                                                      ByVal e As System.EventArgs) _
                                                        Handles txtStockBarcodeFontSize.TextChanged
        If mbIsInitialising Then Exit Sub
        If (Trim(txtStockBarcodeFontSize.Text) <> "") AndAlso IsNumeric(txtStockBarcodeFontSize.Text) Then
            btnSaveFontSize.Enabled = True
        Else
            btnSaveFontSize.Enabled = False
        End If
    End Sub  '-StockBarcodeFontSize-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- Terms AND bank details..-

    Private Sub txtPOS_Terms_TextChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) _
                                               Handles txtPOS_Terms.TextChanged, txtBankName.TextChanged, _
                                                        txtBankName.TextChanged, _
                                                        txtAccountName.TextChanged, _
                                                        txtBSB1.TextChanged, txtBSB2.TextChanged, _
                                                        txtAccountNo.TextChanged

        If mbIsInitialising Then Exit Sub
        If (txtPOS_Terms.Text <> "") And (txtBankName.Text <> "") And (txtAccountName.Text) <> "" And _
              (txtBSB1.Text <> "") And (txtBSB2.Text <> "") And (txtAccountNo.Text <> "") Then
            '--check for numerics..-
            btnSaveTerms.Enabled = True
        Else
            btnSaveTerms.Enabled = False
            '==MsgBox("Note: ALL Terms and Account info fields must have values.", MsgBoxStyle.Exclamation)
        End If

    End Sub  '--terms--
    '= = = = = = = = = = = = =

    Private Sub txtBSB1_validating(ByVal sender As System.Object, _
                                               ByVal ev As CancelEventArgs) _
                                                Handles txtBSB1.Validating, txtBSB2.Validating, txtAccountNo.Validating
        Dim txt1 As TextBox = CType(sender, TextBox)

        If (txt1.Text <> "") AndAlso IsNumeric(txt1.Text) Then '-ok-
        Else
            ev.Cancel = True
            MsgBox("Note: BSB and Account No. must be numeric only, and not empty..", MsgBoxStyle.Exclamation)
        End If

    End Sub  '-txtBSB1_validating--
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- Save values -
    '-- Save values -

    Private Sub btnSaveGST_Click(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles btnSaveGST.Click
        If mbIsInitialising Then Exit Sub
        If mbSaveSystemValue(txtGST_percentage, "GSTPercentage") Then
            btnSaveGST.Enabled = False
        End If
    End Sub  '-save GST-
    '= = = = = = = = = = = = = =

    '-save font name-

    Private Sub btnSaveFontName_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles btnSaveFontName.Click
        If mbIsInitialising Then Exit Sub
        If mbSaveSystemValue(txtStockBarcodeFontName, "POS_BarcodeFontName") Then
            btnSaveFontName.Enabled = False
        End If
    End Sub '-save font name-
    '= = = = = = = = = = = = == 

    '-save font Size-

    Private Sub btnSaveFontSize_Click(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) Handles btnSaveFontSize.Click
        If mbIsInitialising Then Exit Sub
        If mbSaveSystemValue(txtStockBarcodeFontSize, "POS_BarcodeFontSize") Then
            btnSaveFontSize.Enabled = False
        End If
    End Sub '-save font Size-
    '= = = = = = = = = = = = =

    '-- Save Margin  -

    Private Sub btnSaveMargin_Click(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs) Handles btnSaveMargin.Click

        If mbIsInitialising Then Exit Sub
        If mbSaveSystemValue(txtSellMargin, "POS_sell_margin") Then
            btnSaveMargin.Enabled = False
        End If

        '== Dim asChanges(1) As String '--update list..-
        '== If (Trim(txtSellMargin.Text) <> "") AndAlso IsNumeric(txtSellMargin.Text) Then
        '== Try
        '== asChanges(0) = "POS_sell_margin"
        '== asChanges(1) = Trim(txtSellMargin.Text)
        '== If Not gbUpdateSystemInfo(mCnnSql, asChanges) Then
        '== MsgBox("Couldn't save sell_margin settings.", MsgBoxStyle.Exclamation)
        '== Else
        '== MsgBox("Settings saved ok..", MsgBoxStyle.Information)
        '== End If
        '== Catch ex As Exception
        '== MsgBox("Error in 'btn sell_margin'." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        '== End Try
        '== End If
    End Sub  '-btnSaveMargin-
    '= = = = =  = = = = = =  = =

    '---btnSaveGrades--

    Private Sub btnSaveGrades_Click(sender As Object, e As EventArgs) Handles btnSaveGrades.Click
        If mbIsInitialising Then Exit Sub
        If mbSaveSystemValue(txtPriceGrade1, "POS_CUSTPRICINGGRADE_COSTPLUS_1", False) AndAlso _
              mbSaveSystemValue(txtPriceGrade2, "POS_CUSTPRICINGGRADE_COSTPLUS_2", False) AndAlso _
                mbSaveSystemValue(txtPriceGrade3, "POS_CUSTPRICINGGRADE_COSTPLUS_3", False) AndAlso _
                       mbSaveSystemValue(txtPriceGrade4, "POS_CUSTPRICINGGRADE_COSTPLUS_4", False) Then
            btnSaveGrades.Enabled = False '-ok-
            MsgBox("Grades were updated ok.", MsgBoxStyle.Information)
        End If
    End Sub  '-btnSaveGrades-
    '= = = = = = = = = == = = = 
    '-===FF->

    '-- save Terms AND bank details--

    Private Sub btnSaveTerms_Click(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs) Handles btnSaveTerms.Click
        Dim bOk As Boolean = False

        If mbIsInitialising Then Exit Sub

        bOk = mbSaveSystemValue(txtPOS_Terms, "POS_AccountTerms")
        If bOk Then bOk = mbSaveSystemValue(txtBankName, "POS_BusinessBankName")
        If bOk Then bOk = mbSaveSystemValue(txtAccountName, "POS_BusinessAccountName")
        If bOk Then bOk = mbSaveSystemValue(txtBSB1, "POS_BusinessAccountBSB1")
        If bOk Then bOk = mbSaveSystemValue(txtBSB2, "POS_BusinessAccountBSB2")
        If bOk Then bOk = mbSaveSystemValue(txtAccountNo, "POS_BusinessAccountNo")

        If bOk Then
            btnSaveTerms.Enabled = False
        End If
    End Sub '-save-
    '= = = = = = = = = =
    '-===FF->

    '=-- SMTP stuff- 3107.901--
    '=-- SMTP stuff- 3107.901--
    '--  SMTP Mail Server stuff..--
    '--  SMTP Mail Server stuff..--
    '--  SMTP Mail Server stuff..--

    Private Sub txtSMTPServer_TextChanged(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) Handles txtSMTPServer.TextChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        mbSMTPDataChanged = True
        btnSaveSMTP.Enabled = True
        '= cmdClose.Text = "Cancel"
    End Sub  '--txtSMTPServer_TextChanged-
    '= = = = = = = = = = = = = = = = = = =

    '--  Port-no..-

    Private Sub txtSMTPHostPort_TextChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles txtSMTPHostPort.TextChanged
        Dim s1 As String = Trim(txtSMTPHostPort.Text)

        If mbIsInitialising Or mbFormLoading Then Exit Sub
        If (s1 <> "") Then
            If Not IsNumeric(s1) Then
                '== MsgBox("SMTP host port-no. must be numeric.", MsgBoxStyle.Exclamation)
            End If
        End If
        mbSMTPDataChanged = True
        '= cmdClose.Text = "Cancel"
    End Sub '--port-
    '= = = = = = = = = = 

    Private Sub txtSMTPHostPort_validating(ByVal sender As System.Object, _
                                               ByVal e As System.ComponentModel.CancelEventArgs) _
                                                    Handles txtSMTPHostPort.Validating
        Dim s1 As String = Trim(txtSMTPHostPort.Text)

        If (s1 <> "") Then
            If Not IsNumeric(s1) Then
                MsgBox("SMTP host port-no. must be numeric.", MsgBoxStyle.Exclamation)
                e.Cancel = True    '--keep focus..--
            Else
                btnSaveSMTP.Enabled = True
            End If
        Else  '--no entry..-
            txtSMTPHostPort.Text = "25"  '--default..
            btnSaveSMTP.Enabled = True
        End If
    End Sub  '--port validate-
    '= = = = = = = = = = = = = = 

    Private Sub chkHostUsesSSL_CheckedChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles chkHostUsesSSL.CheckedChanged

        mbSMTPDataChanged = True
        '== cmdClose.Text = "Cancel"
        btnSaveSMTP.Enabled = True
    End Sub  '--chkHostUsesSSL-
    '= = = = = = = = = = = = = 
    '-===FF->

    Private Sub txtSMTPUsername_TextChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles txtSMTPUsername.TextChanged

        If mbIsInitialising Or mbFormLoading Then Exit Sub
        mbSMTPDataChanged = True
        '== cmdClose.Text = "Cancel"
    End Sub  '--txtSMTPUsername_TextChanged-
    '= = = = = = = = = = == = = = = = = = =

    Private Sub txtSMTPPassword1_TextChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles txtSMTPPassword1.TextChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        txtSMTPPassword2.Visible = True
        labSMTPConfirm.Visible = True

        If (txtSMTPPassword1.Text = txtSMTPPassword2.Text) Then '--ok..-
            btnSaveSMTP.Enabled = True
        Else
            btnSaveSMTP.Enabled = False
        End If
        mbSMTPDataChanged = True
        '= cmdClose.Text = "Cancel"
    End Sub  '--txtSMTPPassword1_TextChanged--
    '= = = = = = = = = = = = = = = = = = = =

    Private Sub txtSMTPPassword2_TextChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles txtSMTPPassword2.TextChanged

        If mbIsInitialising Or mbFormLoading Then Exit Sub
        If (txtSMTPPassword1.Text = txtSMTPPassword2.Text) Then '--ok..-
            btnSaveSMTP.Enabled = True
        Else
            btnSaveSMTP.Enabled = False
        End If

        mbSMTPDataChanged = True
    End Sub  '--txtSMTPPassword1_TextChanged--
    '= = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '- save SMTP stuff--
    '--- NB: SAME systemInfo settings as JobMatix--
    '--   SHARED (common) if both systems are included.--

    Private Sub btnSaveSMTP_Click(sender As Object, ev As EventArgs) _
                                        Handles btnSaveSMTP.Click
        Dim sSSL As String

        If Trim(msSMTPPassword) <> Trim(txtSMTPPassword1.Text) Then  '--has changed..--
            If (Trim(txtSMTPPassword1.Text) <> Trim(txtSMTPPassword2.Text)) Then
                MsgBox(" SMTP (Mail) Password and confirmation fields are not identical..", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If

        '--  Update SMTP stuff if needed..-
        If mbSMTPDataChanged Then
            '== TabControl1.SelectedIndex = 2  '--SMTP--
            sSSL = "N"
            If chkHostUsesSSL.Checked Then sSSL = "Y"
            If MsgBox("The SMTP (Mail) Server credentials have been changed.." & vbCrLf & _
                   "Do you want to update the JobMatix database " & vbCrLf & _
                        "  to save these new values?", _
                              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If mSysInfo1.UpdateSystemInfo(New Object() _
                              {"smtpHostName", Trim(txtSMTPServer.Text), _
                              "smtpHostPort", Trim(txtSMTPHostPort.Text), _
                              "smtpHostUsesSSL", sSSL, _
                              "smtpUsername", Trim(txtSMTPUsername.Text), _
                             "smtpPassword", Trim(txtSMTPPassword1.Text)}) Then '--ok-
                    MsgBox("SMTP Credentials were updated ok..", MsgBoxStyle.Information)
                End If '--update-
            Else  '--no-
                '== Exit Sub
            End If '--yes.
        End If '--changed..-

    End Sub  '-- save SMTP stuff-
    '= = = = =  = = = = = = = = =
    '-===FF->

    '- Email texts-
    '- Email texts-

    Private Sub txtEmailTextInvoice_TextChanged(sender As Object, e As EventArgs) _
                                                 Handles txtEmailTextInvoice.TextChanged, _
                                                           txtEmailTextStatement.TextChanged, _
                                                              txtEmailTextPO.TextChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        btnSaveEmailTexts.Enabled = True

    End Sub '- txtEmailTextInvoice_TextChanged-
    '= = = = = = = = = = =  = = = = = = = = =

    '=3403.1007=--chkAllowEmailInvoices-

    Private Sub chkAllowEmailInvoices_CheckedChanged(sender As Object, _
                                                     e As EventArgs) _
                                                 Handles chkAllowEmailInvoices.CheckedChanged, _
                                                         chkAllowEmailStatements.CheckedChanged
        If mbIsInitialising Or mbFormLoading Then Exit Sub
        btnSaveEmailTexts.Enabled = True

    End Sub  '-chkAllowEmailInvoices-
    '= = = = = = = = = = = = = == =

    '-save email permissions and texts-

    Private Sub btnSaveEmailTexts_Click(sender As Object, e As EventArgs) _
                                          Handles btnSaveEmailTexts.Click
        Dim bOk As Boolean = True
        Dim sAllowInvoiceEmail As String = IIf(chkAllowEmailInvoices.Checked, "Y", "N")
        Dim sAllowStatementEmail As String = IIf(chkAllowEmailStatements.Checked, "Y", "N")

        '-- save Allow Email flags..
        '-chkAllowEmailInvoices.Checked-
        'bOk = mbSaveSystemValue(IIf(chkAllowEmailInvoices.Checked, "Y", "N"), "POS_ALLOW_EMAIL_INVOICES", False)
        'If bOk Then _
        '     bOk = mbSaveSystemValue(IIf(chkAllowEmailStatements.Checked, "Y", "N"), "POS_ALLOW_EMAIL_STATEMENTS", False)

        If Not mSysInfo1.UpdateSystemInfo(New Object() _
               {"POS_ALLOW_EMAIL_INVOICES", sAllowInvoiceEmail, _
               "POS_ALLOW_EMAIL_STATEMENTS", sAllowStatementEmail}) Then
            bOk = False
        End If

        If bOk Then bOk = mbSaveSystemValue(txtEmailTextInvoice, "POS_EmailTextInvoice", False)
        If bOk Then bOk = mbSaveSystemValue(txtEmailTextStatement, "POS_EmailTextStatement", False)
        If bOk Then bOk = mbSaveSystemValue(txtEmailTextPO, "POS_EmailTextPO", False)

        If bOk Then
            btnSaveEmailTexts.Enabled = False
            MsgBox("Updated ok..", MsgBoxStyle.Information)
        End If
    End Sub '-save email texts-
    '= = = = = = = =  = = = = = =
    '-===FF->

    '-- Backup-  Server Paths..
    '-- Backup-  Server Paths..
    '-- Backup-  Server Paths..

    '-cmdBrowseServerPath_Click-

    Private Sub cmdBrowseServerPath_Click(sender As Object, e As EventArgs) Handles cmdBrowseServerPath.Click

        '-  browse to path.
        Dim strFolderName As String

        '=  Set the help text description for the FolderBrowserDialog.
        Me.FolderBrowserDialog1.Description =
            "Select the directory that you want to use as the initial Backup."

        '==  Do not allow the user to create new files via the FolderBrowserDialog.
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        FolderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer

        '==        // Show the FolderBrowserDialog.
        Dim result As DialogResult = FolderBrowserDialog1.ShowDialog()
        If (result = DialogResult.OK) Then
            strFolderName = FolderBrowserDialog1.SelectedPath
            cmdSaveServerPath.Enabled = False
        Else
            Exit Sub
        End If  '-ok=

        '= MsgBox("Path is : " & strFolderName, MsgBoxStyle.Information) '--testing..--

        If (strFolderName <> "") Then
            txtServerBackupFolderLocal.Text = strFolderName
            '= Call mbSaveUpdatedText(txtServerBackupFolder(0))
            txtServerBackupFolderNetworkPath.Focus()
        End If
    End Sub  '-cmdBrowseServerPath_Click-
    '= = = = = = =  = = = = = = = = = = =

    '-- Enter Network path.

    Private Sub txtServerBackupFolderNetworkPath_Enter(sender As Object, e As EventArgs) _
                                          Handles txtServerBackupFolderNetworkPath.Enter

        If (Trim(txtServerBackupFolderLocal.Text) = "") Then
            MsgBox("Please set the local backup path first..", MsgBoxStyle.Exclamation)
            cmdBrowseServerPath.Focus()
            Exit Sub
        ElseIf (Trim(txtServerBackupFolderNetworkPath.Text) = "") Then
            '==  txtServerBackupFolderNetworkPath.Text = "\\" & msComputerName & "\"
        Else
            '- take it.
        End If
        '-- offer new network path.
        Dim s1 As String = LCase(Trim(txtServerBackupFolderLocal.Text))
        If (VB.Left(s1, 3) = "c:\") Then
            txtServerBackupFolderNetworkPath.Text = "\\" & msComputerName & "\" & Mid(s1, 4)
        End If

        txtServerBackupFolderNetworkPath.SelectionStart = 0
        txtServerBackupFolderNetworkPath.SelectionLength = Len(txtServerBackupFolderNetworkPath.Text)

    End Sub  ' -enter-
    '= = = = = =  = = =

    Private Sub txtServerBackupFolderNetworkPath_TextChanged(sender As Object, e As EventArgs) _
                                              Handles txtServerBackupFolderNetworkPath.TextChanged

        If mbIsInitialising Or mbFormLoading Then Exit Sub

        If (Trim(txtServerBackupFolderNetworkPath.Text) <> "") Then
            cmdSaveServerPath.Enabled = True
        Else
            cmdSaveServerPath.Enabled = False
        End If
    End Sub
    '= = = = = == = 

    '--cmdSaveServerPath_Click-

    Private Sub cmdSaveServerPath_Click(sender As Object, e As EventArgs) Handles cmdSaveServerPath.Click

        '-- write out path settings to disk..
        '-- Update the POS_EMAILQUEUE_SHAREPATH as well.

        Dim sNewQueuePath As String = Trim(txtServerBackupFolderNetworkPath.Text) & "\JobMatix34MailQueue"

        If mSysInfo1.UpdateSystemInfo(New Object() _
              {"ServerShareLocalPath", Trim(txtServerBackupFolderLocal.Text), _
              "ServerShareNetworkPath", Trim(txtServerBackupFolderNetworkPath.Text), _
              "POS_EMAILQUEUE_SHAREPATH", sNewQueuePath}) Then '--ok-
            MsgBox("Server paths were updated ok..", MsgBoxStyle.Information)

            cmdSaveServerPath.Enabled = False
        End If '--update-

    End Sub  '-- save-
    '= = = = = = = =  = = = = = =
    '-===FF->

    '- exit-

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub  '--exit-
    '= = = = = = = = = = = =

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmPOS31Admin_FormClosing(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim sMsg As String

        If (Not btnSaveGST.Enabled) And (Not btnSaveMargin.Enabled) And _
                      (Not btnSaveTerms.Enabled) And (Not btnSaveGrades.Enabled) Then '--updates empty..
            intCancel = 0 '--let it go--
        Else
            sMsg = "Abandon changes ?"
            '=If Not mbCreateNewDB Then sMsg = "Abandon changes ?"
            Select Case intMode
                Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                      System.Windows.Forms.CloseReason.TaskManagerClosing, _
                           System.Windows.Forms.CloseReason.FormOwnerClosing '== not in .NET --, vbFormCode
                    intCancel = 0 '--let it go--
                Case System.Windows.Forms.CloseReason.UserClosing
                    '-- confirm if form is to be abandoned..--
                    If MsgBox(sMsg, MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        intCancel = 0 '--let it go--
                    Else
                        intCancel = 1 '--cant close yet--'--was mistake..  keep going..
                    End If '--yes.--
                Case Else
                    intCancel = 0 '--let it go--
            End Select '--mode--
        End If '--exit.-
        eventArgs.Cancel = intCancel
    End Sub '--closing--
    '= = = = = = = = = = = =


End Class  '--frmPOS31Admin-
'= = =  = = =  = = = = = = = =

'== the end ==