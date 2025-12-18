

Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'= Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Windows.Forms
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb
Imports System.Math
Imports System.ComponentModel
Imports System.Reflection

Public Class ucTransLookup
    Inherits UserControl

    '- grh 02-Aug-2019-
    '--  For POS Build 4201..
    '==
    '==  Updated 4201.1031-  31-Oct-2019=
    '==    Add CreditNote debited to payments lookup grid.
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1117/1130.  10-Nov-2019-  Started 06-November-2019-
    '==      -- Add "Quotes" to Lookup Transaction....
    '==      --  Add isOnAccount column to Invoice grid.     
    '==              ALSO add Extra REFRESH button..
    '==
    '== NEW Revision-
    '==   == 4219.1216.  16-Dec-2019- 
    '==     -- In Child ucTransLookup for payments, select nettAmpuntCredited (NOT totalAmountReceived.)..
    '==
    '==      
    '= = = = = = = = = == = = = = = = = = == = = = = = = = = = = = =

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    Private mbIsInitialising As Boolean = True
    '=Private mbActivated As Boolean = False   '-to activate once only.-

    Private mbCancelled As Boolean = False
    '= Private mIntSerial_id As Integer = -1  '- serialAudit_id
    '= Private mbIsInStock As Boolean = False
    Dim msDllVersion As String

    '-- input parameters--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    Private msLookupTableName As String = ""
    '= Private msLookupSqlSales As String = ""
    '= Private msLookupSqlpayments As String = ""

    '- Stock list now in dataGridView -
    Private mColPrefsInvoices As Collection
    Private mColPrefsPayments As Collection

    Private mBrowse1 As clsBrowse3
    Private mLngSelectedRow As Integer = -1

    Private mImageUserLogo As Image
    Private mIntMainStaff_id As Integer = -1

    Private msMainStaffName As String = ""

    Private mColPrefsCustomer As Collection
    Private mColPrefsStock As Collection

    '= = = =  = = = = = = = =  = ==  = = == = = = = =  ===  =
    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport

    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->
    '- new-
    '- new-

    Public Sub New(ByVal cnnSql As OleDbConnection, _
                 ByVal sSqlDbName As String, _
                     ByRef colSqlDBInfo As Collection, _
                        ByRef imageUserLogo As Image, _
                            ByVal intStaff_id As Integer, _
                               ByVal sStaffName As String)

        '= mbIsInitialising = True

        InitializeComponent() '-- This call is required by the Windows Form Designer.
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo

        '= msLookupTableName = sLookupTableName
        mImageUserLogo = imageUserLogo

        mIntMainStaff_id = intStaff_id
        msMainStaffName = sStaffName


        '== MsgBox("User must input serno.")
        '= labStatus.Text = "User must input serial no."
        '= msSerialNumber = ""
        '= txtSerialNo.Text = ""  '= msSerialNumber
    End Sub  '-new-1 -
    '= = = = = = =  = ==  = ==
    '-===FF->

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)
    '= = = = = = = = = = = = = = = = = = = = == = 


    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                            ByVal intParentHeight As Integer)
        Me.Width = intParentWidth - 11
        Me.Height = intParentHeight  '= - 11
        '-- resize our controls..
        DoEvents()

        frameBrowse.Width = Me.Width - 17
        frameBrowse.Height = Me.Height - frameBrowse.Top - 15
        dgvResultList.Width = frameBrowse.Width - 20
        dgvResultList.Height = frameBrowse.Height - dgvResultList.Top - 16

        panelBanner.Width = frameBrowse.Width
        btnExit.Left = panelBanner.Width - btnExit.Width - 7

    End Sub  '-resized=
    '= = = = = = = = = = 

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click....
    '- Return true if ok to Close.

    Public Function SubFormCloseRequest() As Boolean

        SubFormCloseRequest = True   '-ok.. can close..
        '==Me.Close()
        '=Call close_me()
    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    '--show/print invoice-
    '= 3311.226=  Add Selected printer names-

    Private Function mbShowInvoice(ByVal intInvoice_Id As Integer, _
                                   ByVal sTranType As String, _
                                   Optional ByVal bCaptureInvoicePDF As Boolean = False, _
                                     Optional ByVal bPrintInvoiceAnyway As Boolean = False, _
                                     Optional ByVal bReallyWantsA4Invoice As Boolean = False, _
                                       Optional ByVal sSelectedInvoicePrinterName As String = "",
                                          Optional ByVal sSelectedReceiptPrinterName As String = "") As Boolean
        Dim frmShowInvoice1 As frmShowInvoice
        Dim bIsQuote As Boolean = (LCase(sTranType) = "quote")
        Dim bIsLayby As Boolean = (LCase(sTranType) = "layby")

        frmShowInvoice1 = New frmShowInvoice
        frmShowInvoice1.connectionSql = mCnnSql
        frmShowInvoice1.InvoiceNo = intInvoice_Id
        frmShowInvoice1.isQuote = bIsQuote
        frmShowInvoice1.islayby = bIsLayby
        '-- can use main signon- mIntMainStaff_id -
        'If (mIntSaleStaff_id <= 0) Then
        frmShowInvoice1.Staff_id = mIntMainStaff_id  '- no current sale login.-
        'Else  '-ok- current sale.
        '    frmShowInvoice1.Staff_id = mIntSaleStaff_id
        'End If
        '=If bCaptureInvoicePDF Then
        frmShowInvoice1.CaptureInvoicePDF = bCaptureInvoicePDF  '--capture pdf for email..
        frmShowInvoice1.PrintInvoiceAnyway = bPrintInvoiceAnyway  '--if checked..
        frmShowInvoice1.A4InvoiceRequested = bReallyWantsA4Invoice
        '= End If
        frmShowInvoice1.UserLogo = mImageUserLogo
        '== ADDED 3401.319=
        frmShowInvoice1.selectedInvoicePrinterName = sSelectedInvoicePrinterName
        frmShowInvoice1.selectedReceiptPrinterName = sSelectedReceiptPrinterName

        frmShowInvoice1.ShowDialog()

    End Function  '--show invoice..-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- set up Data1/Date2 WHERE SQL condition..-
    '-- based on the Form's  datePickers controls From/To..

    Private Function msReportSetupWhereCondition(ByVal strDateColumn As String, _
                                                 ByRef sWhere As String) As String
        Dim sDate1, sDate2 As String

        '-- format dates for SQL..-
        sdate1 = Format(DTPickerFrom.Value, "dd-MMM-yyyy") & " 00:00"  '-min-
        sDate2 = Format(DTPickerTo.Value, "dd-MMM-yyyy") & " 23:59"  '--max.--
        If (sWhere = "") Then
            '= sWhere = " WHERE "
        Else
            sWhere = sWhere & " AND "
        End If
        sWhere = sWhere & " ((" & strDateColumn & ">='" & sDate1 & "') AND (" & strDateColumn & "<='" & sDate2 & "')) "

        '= msReportPeriod = "From: " & sDate1 & "  To: " & sDate2
        msReportSetupWhereCondition = sWhere

    End Function  '--SetupWhereCondition-
    '= = = =  = =  == = = = = = =  = = =
    '-===FF->

    '== SPECIAL mbBrowseAndSearchTable for Customer Table.
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==       -- Looking up Customers- make special Sql-Select for Browser 
    '==              to make a column of [lastName, firstName] in browser Grid.  

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

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not (frmBrowse1.selectedRow Is Nothing) Then '= frmBrowse1.cancelled Then
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

    '-- Browse  table using --
    '--  Separate BROWSE33 FORM, (Includes TEXT SEARCH) and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseAndSearchTable(ByRef colPrefs As Collection, _
                                           ByRef sTitle As String, _
                                            ByRef sWhere As String, _
                                            ByRef colKeys As Collection, _
                                            ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Customer") As Boolean
        Dim frmBrowse1 As New frmBrowse  '--File: frmBrowse33 --

        mbBrowseAndSearchTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not (frmBrowse1.selectedRow Is Nothing) Then '= frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseAndSearchTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()

    End Function  '-mbBrowseAndSearchTable-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  GET READY for Function KEY..
    '--- INITIALISE Invoice Browser.for TRANS. Lookup--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(ByVal sTablename As String, _
                                         ByVal sSelectList As String, _
                                          ByVal sSrchWhereCond As String) As Boolean

        '=Dim colPrefs As Collection
        Dim sHostTablename As String
        '=Dim sWhere As String = ""
        mbInitialiseBrowse = False

        If mBrowse1 Is Nothing Then
            mBrowse1 = New clsBrowse3 '== clsBrowse22
        End If

        mBrowse1.connection = mCnnSql  '= mRetailHost1.connection
        mBrowse1.colTables = mColSqlDBInfo '= mRetailHost1.colTables 
        mBrowse1.IsSqlServer = True   '= mRetailHost1.IsSqlServer
        mBrowse1.DBname = msSqlDbName  '= mRetailHost1.DBname

        '--  get table/prefs info for this host..--

        mBrowse1.tableName = sTablename   '==sHostTablename

        '= mBrowse1.FlexGrid = MSHFlexGrid1
        mBrowse1.DataGrid = dgvResultList
        mBrowse1.UserSelectList = sSelectList

        '--  pass controls..--
        mBrowse1.showRecCount = labRecCount '--updates rec. retrieval..
        mBrowse1.showFind = LabFind '--updates Sort Column display..
        mBrowse1.showTextFind = txtFind '--updates Sort Column display..
        mBrowse1.WhereCondition = sSrchWhereCond
        '= mBrowse1.PreferredColumns = colPrefs   '== mColPrefs
        mBrowse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly
        frameBrowse.Enabled = True

        mLngSelectedRow = -1
        Try
            mBrowse1.Activate() '-- go..--
            mbInitialiseBrowse = True
        Catch ex As Exception
            MsgBox("Error in activating Browser." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

        '== txtFind.Focus()

    End Function '--init-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--Refresh the Browser Grid..

    Private Function mbRefreshGrid(Optional ByVal sSrchWhereCond As String = "") As Boolean

        Dim sSelectSql, sWhere As String
        Dim sDateColumn As String = "invoice_date"

        '-- Build Select Sql depending on table..
        sWhere = ""

        If (msLookupTableName = "invoice") Then
            If (txtItemBarcode.Text <> "") Then
                '-- Search Invoice lines for this stock item..
                sSelectSql = "SELECT stock.barcode AS stock_barcode, IL.description, "
                sSelectSql &= "  INV.invoice_id, INV.invoice_date, INV.transactionType, "
                sSelectSql &= "   isOnAccount,  IL.total_inc AS line_total, "
                sSelectSql &= "  Customer.barcode AS cust_barcode, customer.customer_id, "
                sSelectSql &= "  customerName = CASE companyName  "
                sSelectSql &= "     WHEN '' THEN (customer.lastname + '.' + customer.firstname)"
                sSelectSql &= "     WHEN 'n/a' THEN (customer.lastname + '.' + customer.firstname)"
                sSelectSql &= "     ELSE companyName "
                sSelectSql &= " END + '.[' + customer.barcode + ']', "  '--case- Include barcode in Name.
                sSelectSql &= "  staff.barcode AS staff_barcode, staff.docket_name "
                sSelectSql &= "  FROM dbo.InvoiceLine AS IL "
                sSelectSql &= "  JOIN dbo.Invoice AS INV ON (IL.invoice_id=INV.invoice_id) "
                sSelectSql &= "  JOIN dbo.stock ON (stock.stock_id=IL.stock_id) "
                sSelectSql &= "   JOIN Customer on (Customer.customer_id =INV.customer_id) "
                sSelectSql &= "   JOIN staff on (staff.staff_id =INV.staff_id) "
                sWhere = " (stock.barcode='" & txtItemBarcode.Text & "') "

            Else '-no stock barcode
                '-- search Sales Invoices..
                sSelectSql = "SELECT invoice_id, invoice_date, transactionType, isOnAccount, total_inc AS inv_total, "
                sSelectSql &= " Customer.barcode AS cust_barcode, customer.customer_id, "
                sSelectSql &= "  customerName = CASE companyName  "
                sSelectSql &= "     WHEN '' THEN (customer.lastname + '.' + customer.firstname)"
                sSelectSql &= "     WHEN 'n/a' THEN (customer.lastname + '.' + customer.firstname)"
                sSelectSql &= "     ELSE companyName "
                sSelectSql &= " END + '.[' + customer.barcode + ']', "  '--case- Include barcode in Name.
                sSelectSql &= "  staff.barcode AS staff_barcode, staff.docket_name "
                sSelectSql &= "  FROM dbo.Invoice "
                sSelectSql &= "   JOIN Customer on (Customer.customer_id =Invoice.customer_id) "
                sSelectSql &= "   JOIN staff on (staff.staff_id =Invoice.staff_id) "
                '=sSelectSql &= "    WHERE (invoice_id=" & CStr(mIntInvoice_id) & ");"
            End If  '-barcode-
        ElseIf (msLookupTableName = "salesorder") Then
            If (txtItemBarcode.Text <> "") Then
                '-- Search QUOTE lines for this stock item..
                '-- Search QUOTE lines for this stock item..
                sSelectSql = "SELECT stock.barcode AS stock_barcode, SAL.description, "
                sSelectSql &= "  SA.salesorder_id, SA.salesorder_date, 'quote' AS transactionType, SAL.total_inc AS line_total, "
                sSelectSql &= "  Customer.barcode AS cust_barcode, customer.customer_id, "
                sSelectSql &= "  customerName = CASE companyName  "
                sSelectSql &= "     WHEN '' THEN (customer.lastname + '.' + customer.firstname)"
                sSelectSql &= "     WHEN 'n/a' THEN (customer.lastname + '.' + customer.firstname)"
                sSelectSql &= "     ELSE companyName "
                sSelectSql &= " END + '.[' + customer.barcode + ']', "  '--case- Include barcode in Name.
                sSelectSql &= "  staff.barcode AS staff_barcode, staff.docket_name "
                sSelectSql &= "  FROM dbo.SalesOrderLine AS SAL "
                sSelectSql &= "  JOIN dbo.SalesOrder AS SA ON (SAL.salesorder_id=SA.salesorder_id) "
                sSelectSql &= "  JOIN dbo.stock ON (stock.stock_id=SAL.stock_id) "
                sSelectSql &= "   JOIN Customer on (Customer.customer_id =SA.customer_id) "
                sSelectSql &= "   JOIN staff on (staff.staff_id =SA.staff_id) "
                sWhere = " (stock.barcode='" & txtItemBarcode.Text & "') "
            Else
                '- no stock barcode.
                '-- Search Quotes-
                sSelectSql = "SELECT SA.salesorder_id, salesorder_date, 'quote'  AS transactionType, total_inc AS inv_total, "
                sSelectSql &= " Customer.barcode AS cust_barcode, customer.customer_id, "
                sSelectSql &= "  customerName = CASE companyName  "
                sSelectSql &= "     WHEN '' THEN (customer.lastname + '.' + customer.firstname)"
                sSelectSql &= "     WHEN 'n/a' THEN (customer.lastname + '.' + customer.firstname)"
                sSelectSql &= "     ELSE companyName "
                sSelectSql &= " END + '.[' + customer.barcode + ']', "  '--case- Include barcode in Name.
                sSelectSql &= "  staff.barcode AS staff_barcode, staff.docket_name "
                sSelectSql &= "  FROM dbo.SalesOrder AS SA "
                sSelectSql &= "   JOIN Customer on (Customer.customer_id =SA.customer_id) "
                sSelectSql &= "   JOIN staff on (staff.staff_id =SA.staff_id) "
            End If  '-stock-

        ElseIf (msLookupTableName = "payments") Then
            '--search payments..
            sSelectSql = "SELECT payment_id, payment_date, transactionType, terminal_id, "
            sSelectSql &= "  nettAmountCredited AS newPaymentReceived, creditNoteAmountDebited, "
            sSelectSql &= "  customerName = CASE companyName  "
            sSelectSql &= "     WHEN '' THEN (customer.lastname + '.' + customer.firstname)"
            sSelectSql &= "     WHEN 'n/a' THEN (customer.lastname + '.' + customer.firstname)"
            sSelectSql &= "     ELSE companyName "
            sSelectSql &= " END + '.[' + customer.barcode + ']', "  '--case- Include barcode in Name.
            sSelectSql &= "   staff.barcode AS staff_barcode,  "
            sSelectSql &= "  staff.docket_name "
            sSelectSql &= " FROM dbo.Payments "
            sSelectSql &= "   JOIN Customer on (Customer.customer_id =payments.customer_id) "
            sSelectSql &= "   JOIN staff on (staff.staff_id =payments.staff_id) "
            '== sSelectSql &= "  WHERE (Payments.payment_id=" & CStr(mIntPayment_id) & ")"
        End If  '-table-

        '-- check for Particular Staff and/or Customer.
        If (Trim(txtStaffBarcode.Text) <> "") Then
            If (sWhere <> "") Then
                sWhere &= " AND "
            End If
            sWhere &= "(staff.barcode= '" & Trim(txtStaffBarcode.Text) & "') "
        End If

        If (Trim(txtCustBarcode.Text) <> "") Then
            If (sWhere <> "") Then
                sWhere &= " AND "
            End If
            sWhere &= "(customer.barcode= '" & Trim(txtCustBarcode.Text) & "') "
        End If

        '-- check for date-
        If Not optPeriodAny.Checked Then
            '- needs Period.
            If msLookupTableName = "invoice" Then
                sDateColumn = "invoice_date"
            ElseIf msLookupTableName = "salesorder" Then
                sDateColumn = "salesorder_date"
            ElseIf msLookupTableName = "payments" Then
                sDateColumn = "payment_date"
            End If
            sWhere = msReportSetupWhereCondition(sDateColumn, sWhere)
        End If  '-period-

        If (Trim(sSrchWhereCond) <> "") Then
            If (sWhere <> "") Then
                sWhere &= " AND "
            End If
            sWhere &= Trim(sSrchWhereCond)
        End If

        If Not mbInitialiseBrowse(msLookupTableName, sSelectSql, sWhere) Then
            MsgBox("Lookup query failed..", MsgBoxStyle.Exclamation)
        End If

    End Function  '- mbRefreshGrid-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--Clear filter..

    Private Sub mSubClearAll()

        '=DTPickerFrom.Value = DTPickerFrom.MinDate
        '= DTPickerTo.Value = DTPickerTo.MaxDate

        txtCustBarcode.Text = ""
        txtFind.Text = ""
        txtTranSearch.Text = ""
        txtCustName.Text = ""
        txtStaffBarcode.Text = ""
        labStaffName.Text = ""
        txtItemBarcode.Text = ""
        txtItemDescription.Text = ""

        optPeriodToday.Checked = True

    End Sub  '-mSubClearAll=
    '= = = = = = = = = = = = = =
    '-===FF->

    '--Load--
    '--Load--

    Private Sub ucTransLookup_Load(sender As Object, _
                                   ev As EventArgs) Handles MyBase.Load

        '- set up Prefs etc
        '--  Set label texts according to Table to search..
        'DTPickerFrom.Value = DTPickerFrom.MinDate
        'DTPickerTo.Value = DTPickerTo.MaxDate

        'txtCustBarcode.Text = ""
        'txtFind.Text = ""
        txtTranSearch.Text = ""
        frameBrowse.Text = ""
        'txtCustName.Text = ""
        'txtStaffBarcode.Text = ""
        'labStaffName.Text = ""
        Call mSubClearAll()

        txtItemBarcode.Text = ""

        cboLookupType.Items.Clear()

        'msLookupSqlSales = "SELECT invoice_id, invoice_date, "
        'msLookupSqlSales &= " FROM invoice "

        cboLookupType.Items.Add("Sales Invoices")
        cboLookupType.Items.Add("Payments")
        cboLookupType.Items.Add("Quotes")

        cboLookupType.SelectedIndex = 0

        optPeriodToday.Checked = True

        '-- Customer --
        mColPrefsCustomer = New Collection
        mColPrefsCustomer.Add("lastname")
        mColPrefsCustomer.Add("firstname")
        mColPrefsCustomer.Add("companyName")
        mColPrefsCustomer.Add("barcode")
        mColPrefsCustomer.Add("isAccountCust")
        mColPrefsCustomer.Add("phone")
        mColPrefsCustomer.Add("mobile")
        mColPrefsCustomer.Add("customer_id")
        mColPrefsCustomer.Add("creditLimit")
        mColPrefsCustomer.Add("pricingGrade")
        mColPrefsCustomer.Add("inactive")
        mColPrefsCustomer.Add("address")
        '==mColPrefsCustomer.Add("addr2")
        '=mColPrefsCustomer.Add("addr3")
        mColPrefsCustomer.Add("suburb")
        mColPrefsCustomer.Add("email")

        '--  stock--
        mColPrefsStock = New Collection
        mColPrefsStock.Add("description")
        mColPrefsStock.Add("barcode")
        mColPrefsStock.Add("brandName")
        mColPrefsStock.Add("cat1")   '--fkey-
        mColPrefsStock.Add("cat2")   '-fkey-
        '= mColPrefsStock.Add("productPicture")
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

        Dim assemblyThis As assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim assName As AssemblyName = assemblyThis.GetName
        With assName.Version
            msDllVersion = CStr(.Major) & "." & CStr(.Minor) & "." & CStr(.Build) & "." & CStr(.Revision)
        End With

        '= msGetDllversion = sVersion

        mbIsInitialising = False
        '= btnRefresh.Select()

    End Sub  '-load-
    '= = = = = = = = = = =
    '-===FF->

    '--optPeriodToday_CheckedChanged-

    Private Sub optPeriodToday_CheckedChanged(sender As Object, e As EventArgs) _
                                          Handles optPeriodToday.CheckedChanged, optperiodThisMonth.CheckedChanged, _
                                              optPeriod12Months.CheckedChanged, _
                                                optPeriodAny.CheckedChanged
        '=panelPeriodFromTo.Enabled = False
        If optPeriodToday.Checked Then
            DTPickerFrom.Value = Today
            DTPickerTo.Value = Today
        ElseIf optPeriodAny.Checked Then
            DTPickerFrom.Value = DTPickerFrom.MinDate
            DTPickerTo.Value = DTPickerTo.MaxDate
        ElseIf optperiodThisMonth.Checked Then
            DTPickerFrom.Value = DateAdd("d", -(Today.Day - 1), Today) '--start at 1st day of this month..-
            DTPickerTo.Value = Today.Date                                 '-- end date is today.-
        ElseIf optPeriod12Months.Checked Then
            DTPickerFrom.Value = DateAdd("d", -(366), Today) '--start at 1st day of this month..-
            DTPickerTo.Value = Today.Date                                 '-- end date is today.-
            'ElseIf optPeriodSelect.Checked Then
            '    panelPeriodFromTo.Enabled = True
        Else  '-nothing

        End If
    End Sub '-optPeriods--
    '= = = = = = = = = = = = 



    '-- clear all-

    Private Sub btnClearFilter_Click(sender As Object, e As EventArgs) Handles btnClearFilter.Click

        Call mSubClearAll()


    End Sub   '--clear-
    '= = = = = = = = = = = =

    '--refresh--

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) _
                                       Handles btnRefresh.Click, btnRefresh2.Click

        '-- check that dtpicker dates are valid..
        Call mbRefreshGrid()

    End Sub  '-refresh grid-
    '= = = = = = = = = = = = = 

    '-cboLookupType_SelectedIndexChanged-

    Private Sub cboLookupType_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                   Handles cboLookupType.SelectedIndexChanged

        Dim sItem As String = cboLookupType.SelectedItem

        Call mSubClearAll()
        '-- set filter fields for selected lookup..

        If (InStr(LCase(sItem), "sales") > 0) Then
            msLookupTableName = "invoice"
            '- EN-able stock lookup controls..
            txtItemBarcode.Enabled = True
        ElseIf (InStr(LCase(sItem), "quotes") > 0) Then
            msLookupTableName = "salesorder"
            '- EN-able stock lookup controls..
            txtItemBarcode.Enabled = True
        ElseIf (InStr(LCase(sItem), "payments") > 0) Then
            msLookupTableName = "payments"
            '- disable stock lookup controls..
            txtItemBarcode.Enabled = False
        Else
            MsgBox("Error- unrecognised selection..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

    End Sub  '-cboLookupType_SelectedIndexChanged-
    '= =  = = == = = == =  === =

    '-DTPickerFrom_ValueChanged-

    Private Sub DTPickerFrom_ValueChanged(sender As Object, ev As EventArgs) _
                                                  Handles DTPickerFrom.ValueChanged, DTPickerTo.ValueChanged


    End Sub  '-DTPickerFrom_ValueChanged-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '- STAFF barcode entry--
    '- STAFF barcode entry--


    Private Sub txtStaffBarcode_Enter(eventsender As Object, EventArgs As System.EventArgs) _
                                                                       Handles txtStaffBarcode.Enter
        If mbIsInitialising Then Exit Sub
        '= msSavedOldStaffBarcode = Trim(txtSaleStaffBarcode.Text)
        '= Call mClsSale1.txtSaleStaffBarcode_Enter(eventsender, EventArgs)



    End Sub  '-txtSaleStaffBarcode_Enter-
    '= = = = = = = = = = =  = = = = = = =

    '-- Staff barcode. Enter was Pressed --

    Private Sub txtStaffBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtStaffBarcode.KeyPress
        If mbIsInitialising Then Exit Sub

        '= Call mClsSale1.txtSaleStaffBarcode_KeyPress(eventSender, eventArgs)
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim sSql, s1 As String
        Dim colResult, colRecord As Collection
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent

        If keyAscii = 13 Then '--enter-
            '--check if some previous sale not committed..
            s1 = Trim(txtStaffBarcode.Text)
            If (s1 <> "") Then  '--have barcode-
                '--lookup barcode-
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [staff] WHERE (barcode='" & s1 & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    '= mIntSaleStaff_id = colRecord.Item("staff_id")("value")
                    '= msSaleStaffName = colRecord.Item("docket_name")("value")
                    labStaffName.Text = colRecord.Item("docket_name")("value")
                    '-- go to cistomer barcode-
                    controlParent.SelectNextControl(textBox1, True, True, True, True)
                Else '--not found..-
                    MsgBox("No Staff Record found for barcode: " & s1, MsgBoxStyle.Exclamation)
                    '-- select text-

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
    '-===FF->

    '-- STAFF barcode TEXTBOX- Validating --
    '==
    '==      STAFF Barcode- Must catch "Validating" event for TAB key. .- 

    Private Sub txtStaffBarcode_Validating(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As CancelEventArgs) _
                                       Handles txtStaffBarcode.Validating

        If mbIsInitialising Then Exit Sub
        '= Call mClsSale1.txtSaleStaffBarcode_Validating(eventSender, eventArgs)
        Dim sBarcode As String
        Dim sSql As String
        Dim colResult, colRecord As Collection

        '= Call mClsSale1.txtSaleCustBarcode_Validating(eventSender, eventArgs)
        If (Trim(txtStaffBarcode.Text) = "") Then
            '= OK-
            '= eventArgs.Cancel = True
            '= MsgBox("Must have Staff barcode: " & sBarcode, MsgBoxStyle.Exclamation)
        Else
            '- validate/lookup if not done yet..
            'If (msSaleStaffbarcode = "") OrElse _
            '       (msSaleStaffbarcode <> Trim(mTxtSaleStaffBarcode.Text)) Then  '-cust not set up--
            '    '--lookup -
            sBarcode = Trim(txtStaffBarcode.Text)
            '--  get recordset as collection for SELECT..--
            sSql = "SELECT * FROM [staff] WHERE (barcode='" & sBarcode & "');"
            If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                   (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                '--have a row..-
                colRecord = colResult.Item(1)
                'msSaleStaffbarcode = sBarcode
                'mIntSaleStaff_id = colRecord.Item("staff_id")("value")
                'msSaleStaffName = colRecord.Item("docket_name")("value")
                labStaffName.Text = colRecord.Item("docket_name")("value")
            Else '--not found..-
                eventArgs.Cancel = True
                MsgBox("No Staff found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
            End If  '-get--
            'End If  '-set up-
        End If  '-text-
        '- that's all-
    End Sub  '--Validating-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Filter Customer.

    '- Customer barcode entry--
    '- Customer barcode entry--

    Private Sub txtCustBarcode_TextChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) Handles txtCustBarcode.TextChanged
        If mbIsInitialising Then Exit Sub

        '= Call mClsSale1.txtSaleCustBarcode_TextChanged(sender, e)
    End Sub  '--txtCustBarcode_TextChanged--
    '= = = = = = = = = = = = = = =  = = = 

    '-- CUSTOMER  Enter Pressed --

    Private Sub txtCustBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtCustBarcode.KeyPress
        If mbIsInitialising Then Exit Sub
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim sSql As String
        Dim colResult, colRecord, colSelectedRow As Collection
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent

        If keyAscii = 13 Then '--enter-
            s1 = Trim(txtCustBarcode.Text)
            If (s1 <> "") Then  '--have barcode-
                '--lookup barcode-
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [customer] WHERE (barcode='" & s1 & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    colSelectedRow = colRecord
                    '= Call mbSetupSaleCustomer(colRecord)
                    txtCustBarcode.Text = colSelectedRow.Item("barcode")("value")
                    '=msSaleCustomerBarcode = Trim(mTxtSaleCustBarcode.Text)
                    '==End If
                    If colSelectedRow.Contains("companyName") Then
                        txtCustName.Text = colSelectedRow.Item("companyName")("value")
                        '= txtCustName.Text = col1.Item("value")
                    End If
                    If txtCustName.Text <> "" Then txtCustName.Text &= vbCrLf
                    If colSelectedRow.Contains("firstName") Then
                        txtCustName.Text &= colSelectedRow.Item("firstName")("value") & " "
                        '=txtCustName.Text = col1.Item("value")
                    End If
                    If colSelectedRow.Contains("lastName") Then
                        txtCustName.Text &= colSelectedRow.Item("lastName")("value")
                    End If
                Else '--not found..-
                    MsgBox("No Customer found for barcode: " & s1, MsgBoxStyle.Exclamation)
                End If  '-get--
            Else  '- no barcode-
                '-- allow to pass, but not cust or trans. can go-
                '--  Just use validate--
                controlParent.SelectNextControl(textBox1, True, True, True, True)
            End If  '--have barcode-
            keyAscii = 0
        End If  '--key ascii-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
        '-- thats all-

    End Sub  '--CUST keypress=
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- Customer Search (F2)..--
    '-- barcode TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for Cust Lookup--

    Private Sub txtCustBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                       Handles txtCustBarcode.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sSql, s1 As String
        Dim colResult, colRecord As Collection

        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup Customer--
            If Not mbBrowseAndSearchCustomers(mColPrefsCustomer, "Lookup Customer", "", colKeys, colSelectedRow) Then
                '= Not mbBrowseTable(mColPrefsCustomer, "Lookup Customers", "", colKeys, colSelectedRow, "Customer", True) Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else  '--selected
                txtCustName.Text = ""
                If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                    '=Call mbSetupSaleCustomer(colSelectedRow)
                    '=3301.710= mPanelOptTranType.Focus()   '== mDgvSaleItems.Select()   '--focus-
                    txtCustBarcode.Text = colSelectedRow.Item("barcode")("value")
                    '=msSaleCustomerBarcode = Trim(mTxtSaleCustBarcode.Text)
                    '==End If
                    If colSelectedRow.Contains("companyName") Then
                        txtCustName.Text = colSelectedRow.Item("companyName")("value")
                        '= txtCustName.Text = col1.Item("value")
                    End If
                    If txtCustName.Text <> "" Then txtCustName.Text &= vbCrLf
                    If colSelectedRow.Contains("firstName") Then
                        txtCustName.Text &= colSelectedRow.Item("firstName")("value") & " "
                        '=txtCustName.Text = col1.Item("value")
                    End If
                    If colSelectedRow.Contains("lastName") Then
                        txtCustName.Text &= colSelectedRow.Item("lastName")("value")
                    End If
                End If
            End If  '-browse-
        End If  '-F2-

        '- that's all-
    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 
    '-===FF->
    '-- Customer barcode TEXTBOX- Validating --
    '==       >> Cust Barcode- Must catch "Validating" event for TAB key. .- 

    Private Sub txtCustBarcode_Validating(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As CancelEventArgs) _
                                       Handles txtCustBarcode.Validating
        Dim sBarcode As String
        Dim sSql As String
        Dim colResult, colRecord As Collection
        Dim colSelectedRow As Collection

        If (Trim(txtCustBarcode.Text) = "") Then
            '= eventArgs.Cancel = True
            '= MsgBox("Must Have customer barcode", MsgBoxStyle.Exclamation)
            '-- allow to pass, optional-
        Else
            '--lookup -
            sBarcode = Trim(txtCustBarcode.Text)
            '--  get recordset as collection for SELECT..--
            sSql = "SELECT * FROM [customer] WHERE (barcode='" & sBarcode & "');"
            If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                   (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                '--have a row..-
                colRecord = colResult.Item(1)
                colSelectedRow = colRecord
                '= Call mbSetupSaleCustomer(colRecord)
                txtCustBarcode.Text = colSelectedRow.Item("barcode")("value")
                If colSelectedRow.Contains("companyName") Then
                    txtCustName.Text = colSelectedRow.Item("companyName")("value")
                    '= txtCustName.Text = col1.Item("value")
                End If
                If txtCustName.Text <> "" Then txtCustName.Text &= vbCrLf
                If colSelectedRow.Contains("firstName") Then
                    txtCustName.Text &= colSelectedRow.Item("firstName")("value") & " "
                    '=txtCustName.Text = col1.Item("value")
                End If
                If colSelectedRow.Contains("lastName") Then
                    txtCustName.Text &= colSelectedRow.Item("lastName")("value")
                End If
            Else '--not found..-
                eventArgs.Cancel = True
                MsgBox("No Customer found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
            End If  '-get--
        End If  '-text-
        '- that's all-
    End Sub  '--Validating-
    '= = = = = = = = = = = = = = = 
    '-- Validated event caught only to update main form Tab text..
    Private Sub txtCustBarcode_Validated(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) _
                                       Handles txtCustBarcode.Validated
    End Sub ''-- validated.
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Product barcode..
    '-- Product barcode..
    '-- Product barcode..

    '-- Textbox Enter control for Item barcode.

    Private Sub txtItemBarcode_Enter(sender As Object, ev As System.EventArgs) Handles txtItemBarcode.Enter
        If mbIsInitialising Then Exit Sub
        '= Call mClsSale1.txtSaleItemBarcode_Enter(sender, ev)
        If txtItemBarcode.Text = "" Then
            txtItemBarcode.Text = "barcode"
        End If
        txtItemBarcode.SelectionStart = 0
        txtItemBarcode.SelectionLength = Len(txtItemBarcode.Text)

    End Sub '-txtItemBarcode_Enter-

    '==-- 15-Apr-2018- POS Sale- Catch Mouse-Click on Item Barcode to set Selection stuff....

    Private Sub txtItemBarcode_Click(sender As Object, ev As System.EventArgs) Handles txtItemBarcode.Click

        If mbIsInitialising Then Exit Sub
        '= Call mClsSale1.txtSaleItemBarcode_Click(sender, ev)
        txtItemBarcode.SelectionStart = 0
        txtItemBarcode.SelectionLength = Len(txtItemBarcode.Text)

    End Sub  '-txtItemBarcode_Click-
    '= = = = = = = = = = = = = = = = = =

    '-- Stock Item Search (F2)..--
    '-- Stock Item Search (F2)..--
    '-- Stock Item Search (F2)..--

    '-- Grid TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for STOCK Lookup--

    Private Sub txtItemBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                       Handles txtItemBarcode.KeyDown

        If mbIsInitialising Then Exit Sub
        '= Call mClsSale1.txtSaleItemBarcode_KeyDown(eventSender, eventArgs)
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sBarcode, sFinalBarcode, sSql, s1 As String
        Dim intStock_id As Integer

        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup stock--
            '= now uses frmBrowse33 for this. (incl Search).
            If Not mbBrowseAndSearchTable(mColPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, "stock") Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else  '--selected
                '=3403.718= Ouch!   mTxtSaleCustName.Text = ""
                If (Not (colSelectedRow Is Nothing)) AndAlso (colSelectedRow.Count > 0) Then
                    intStock_id = CInt(colSelectedRow("stock_id")("value"))
                    sBarcode = colSelectedRow("barcode")("value")
                    '- setup selected stock item.
                    txtBarcode.Text = sBarcode
                    txtItemDescription.Text = colSelectedRow("description")("value")
                End If
            End If  '-browse-
        End If  '--F2-
    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 
    '-===FF->


    Private Sub txtItemBarcode_TextChanged(sender As Object, ev As EventArgs) _
                                                Handles txtItemBarcode.TextChanged
        If mbIsInitialising Then Exit Sub
        '= Call mClsSale1.txtSaleItemBarcode_TextChanged(sender, ev)

    End Sub  '-txtItemBarcode-
    '= = = = = = = = = = = = == = == =

    '-- Handle ENTER for all Line Item textboxes..
    '--   txtSaleItemBarcode-  Enter Pressed --

    Private Sub txtItemBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtItemBarcode.KeyPress
        If mbIsInitialising Then Exit Sub

        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent
        Dim sData As String = Trim(textBox1.Text)
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
 
        If (keyAscii = 13) Then '--enter-
            '--  Just use validate--
            controlParent.SelectNextControl(textBox1, True, True, True, True)
            keyAscii = 0  '-done-
        End If  '-enter-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If

    End Sub  '-txtItemBarcode_KeyPress-
    '= = = = = = = =  = = = = = = = = == == 
    '-===FF->

    '-- Handle Validating for all Line Item textboxes..

    Private Sub txtItemBarcode_Validating(ByVal sender As System.Object, _
                                              ByVal ev As CancelEventArgs) _
                                                 Handles txtItemBarcode.Validating
        If mbIsInitialising Then Exit Sub
        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(textBox1.Text)
        Dim s1, sBarcode, sFinalBarcode, sSql, sSerialNo, sError, sSerialTrail As String
        Dim datatable1 As DataTable
        '=Dim intAudit_id, intStock_id, intSalesInvoiceId As Integer

        sError = ""
        '--  If this is barcode, then check if valid etc...-
        sBarcode = sData
        If LCase(sBarcode) <> "barcode" And (sBarcode <> "") Then '-have a barcode-
            '- lookup..
            '--  get recordset as collection for SELECT..--
            sSql = "SELECT * FROM [stock] WHERE (barcode='" & sBarcode & "');"
            If gbGetDataTable(mCnnSql, datatable1, sSql) AndAlso _
                                   (Not (datatable1 Is Nothing)) AndAlso (datatable1.Rows.Count > 0) Then
                Dim row1 As DataRow = datatable1.Rows(0)
                txtItemDescription.Text = row1.Item("description")
            Else
                ev.Cancel = True
                '= MsgBox(sError, MsgBoxStyle.Exclamation)
                MsgBox("No Stock record found for barcode: '" & sBarcode & "' !" & _
                                 vbCrLf & gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                Exit Sub
            End If  '-get-
        Else '- no barcode-
            txtItemBarcode.Text = ""
            '= mTxtSaleItemSerialNo.Enabled =True 
            sBarcode = ""
            '-let it go on..
        End If  '-have barcode-
    End Sub  '--txtItemBarcode_Validating-
    '= = = = = = = = = = = = = = = = = = = == 

    Private Sub txtItemBarcode_Validated(ByVal sender As System.Object, _
                                          ByVal ev As System.EventArgs) _
                                             Handles txtItemBarcode.Validated
        If mbIsInitialising Then Exit Sub
        '=Call mClsSale1.txtSaleItemBarcode_Validated(sender, ev)

    End Sub  '--txtItemBarcode_Validated-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- GRID and Full text Search Stuff.
    '- dgvResultList_CellContentClick-

    Private Sub dgvResultList_CellContentClick(sender As Object, _
                                               ev As DataGridViewCellEventArgs) Handles dgvResultList.CellContentClick

    End Sub  '- dgvResultList_CellContentClick-
    '= = = = =  = = = = = = == = ==  == = = = =

    '-- cell click.--
    '-- cell click.--

    Private Sub dgvResultList_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvResultList.CellMouseClick
        Dim lRow, lCol As Integer
        Dim sName As String

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            If (lRow >= 0) And (dgvResultList.Rows.Count > 0) Then  '--selected a row.--
                '= cmdOk.Enabled = True
            End If

        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =

    '--mouse activity---  
    '-- select row to select Invoice to show..--

    Private Sub dgvResultList_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvResultList.CellMouseDoubleClick
        Dim lRow, lCol As Integer
        Dim colKeyValues As Collection '--PKEYS of selected record-
        Dim colRowValues As Collection '--selected grid row-

        lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow < 0) Then '--in header row--
        Else
            '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
            mLngSelectedRow = lRow
            Call mBrowse1.SelectRecord(mLngSelectedRow, colKeyValues, colRowValues)

            '-- show Invoice..
            Dim intInvoice_id, intPayment_id As Integer
            Dim sTranType As String
            If colRowValues.Contains("invoice_id") Then
                intInvoice_id = colRowValues.Item("invoice_id")("value")
                sTranType = colRowValues.Item("transactionType")("value")
                Call mbShowInvoice(intInvoice_id, sTranType)
            ElseIf colRowValues.Contains("salesorder_id") Then
                '- Quote-
                intInvoice_id = colRowValues.Item("salesorder_id")("value")
                sTranType = colRowValues.Item("transactionType")("value")
                Call mbShowInvoice(intInvoice_id, sTranType)
            ElseIf colRowValues.Contains("payment_id") Then
                intPayment_id = colRowValues.Item("payment_id")("value")
                sTranType = colRowValues.Item("transactionType")("value")
                Dim frmShowPayment1 As frmShowPayment

                frmShowPayment1 = New frmShowPayment
                frmShowPayment1.connectionSql = mCnnSql
                frmShowPayment1.sqlDbname = msSqlDbName
                frmShowPayment1.PaymentNo = intPayment_id
                '== frmShowPayment1.Settings = mSdSettings
                '== frmShowInvoice1.SystemInfo = mSdSystemInfo
                frmShowPayment1.UserLogo = mImageUserLogo
                frmShowPayment1.versionPOS = msDllVersion
                '= frmShowPayment1.selectedReceiptPrinterName = sSelectedReceiptPrinterName
                frmShowPayment1.CaptureReceiptPDF = False '= bCaptureReceiptPDF
                frmShowPayment1.CanPrintReceipt = True '= bCanPrintReceipt
                '-- current staff_id and msStaffName-
                frmShowPayment1.Staff_id = mIntMainStaff_id
                frmShowPayment1.StaffName = msMainStaffName
                frmShowPayment1.ShowDialog()
            End If  '-contains.
            '= Call cmdExit_Click()
        End If '--row--
    End Sub '--click--
    '= = = = = = = = = = = = = = = = = =

    '-- selection changed-   Allow OK..

    Private Sub DataGridView1_SelectionChanged(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As EventArgs) _
                                                            Handles dgvResultList.SelectionChanged
        If (dgvResultList.SelectedRows.Count > 0) Then
            '= cmdOk.Enabled = True
        Else
            '= cmdOk.Enabled = False
        End If
    End Sub  '-- selection changed-
    '= = = =  = = = = = =
    '-===FF->

    '-- Grid and Full text Search Stuff.

    '-- STOCK Browser.. txt FIND Activity.--
    '-- STOCK Browser.. txt FIND Activity.--
    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvResultList_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvResultList.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvResultList.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowse1.SortColumn(sName)
    End Sub
    '= = = = = = = = =  = = =

    '--BROWSING STOCK.. --

    '-- key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        Dim intStock_id As Integer
        Dim colKeys, colRowValues As Collection

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If dgvResultList.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = dgvResultList.SelectedRows(0).Cells(0).RowIndex
                If (lRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow
                    '= Call mbSelectStockRow(mLngSelectedRow)
                    Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                    'intStock_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                    'If (intStock_id > 0) And (intStock_id <> mIntStock_id) Then '-- has changed..-
                    '    Call mbShowStockInfo(intStock_id)
                    'End If
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

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged

        Call mBrowse1.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '=3411.0417-  Catch Enter Key on stock srch text-

    Private Sub txtStockSearch_keyPress(ByVal sender As System.Object, _
                                         ByVal EventArgs As KeyPressEventArgs) Handles txtTranSearch.KeyPress
        Dim keyAscii As Short = Asc(EventArgs.KeyChar)
        Dim e2 As New EventArgs
        If keyAscii = 13 Then '--enter-
            Call cmdTransSearch_Click(cmdTranSearch, e2)
            keyAscii = 0 '--processed..-
        End If  '13-
        EventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            EventArgs.Handled = True
        End If

    End Sub  '-keypress.
    '= = = = = = = = == 

    '-- Stock Browser..  Full text Search..--
    '-- Stock Browser..  Full text Search..--

    Private Sub cmdTransSearch_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles cmdTranSearch.Click
        '=Dim sWhere As String = ""
        Dim sSql As String '--search sql..-- 
        '= Dim s1, s2 As String
        Dim asColumns As Object = {}

        '--  rebuild Search Columns and call makeTextSearch...-

        '-- arg can be multiple tokens..--
        '===asArgs = Split(Trim(txtSearch(index).Text))

        '-- columns depend if searching Invoices, StockBarcodes or Payments..
        If (msLookupTableName = "invoice") Or (msLookupTableName = "quotes") Then
            If (txtItemBarcode.Text <> "") Then
                '-- Search Invoice lines for this stock item..
                asColumns = New Object() _
              {"stock.barcode", "description"}
            Else
                '- no stock barcode entered. srch Invoices.
                asColumns = New Object() _
                       {"customer.companyName", "customer.firstName", "customer.lastName", "docket_name"}
            End If  '-stock=
        ElseIf (msLookupTableName = "payments") Then
            asColumns = New Object() _
                    {"customer.companyName", "customer.firstName", "customer.lastName", "docket_name"}
        End If

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtTranSearch.Text), asColumns)
        '= Call mbBrowseStockTable(sWhere)

        '-- Refresh--
        Call mbRefreshGrid(sSql)

    End Sub '-cmdStockSearch-
    '= = = = = = = = = = = = =  =

    Private Sub cmdClearStockSearch_Click(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) Handles cmdClearTranSearch.Click
        txtTranSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdTransSearch_Click(cmdTranSearch, New System.EventArgs())

    End Sub  '-ClearStockSearch-
    '= = = = = = = = = = = = = = = =

    Private Sub close_me()

        '- inform parent.-
        '- Report to Parent..-
        '= Dim objParms() As Object = {Me.Name, "FormClosed", ""}


        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

    End Sub '--close me-
    '= = = = = = = = = = = = ==  == 

    '-exit-

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Call close_me()
    End Sub  '-exit-
    '= = = = = = = = = = = = 

End Class '-  ucTransLookup'-
'= = =  == =  = ==  = == = =

'== the end ===
