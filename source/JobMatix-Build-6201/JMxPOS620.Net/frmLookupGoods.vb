
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

Public Class frmLookupGoods

    '-- grh= 19-June-2017-
    '--  started Lookup for Goods..
    '--   Caller supplies supplier_id-

    '-- 3411.1117-  updates to labels..
    '==
    '==
    '== -- Updated 3501.1217  17-Dec-2018=  
    '==     -- New FORM to print Goods Received Transaction.
    '==
    '==
    '== -- Updated 4201.1027  27/28-Oct-2019=  
    '==     -- NOW uses New Class clsGoodsInfo
    '==             to Collect Info for a particular Goods Received Transaction.
    '==
    '= = = = = = = = = = = = = = = = = = === = = = = = = = = = = == = = = = = =

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    Private mbActivated As Boolean = False
    Private mbIsLoading As Boolean = True

    '--inputs--
    Private msVersionPOS As String = ""
    Private msStaffName As String = ""
    Private msBusinessName As String = ""

    Private msServer As String
    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection '--
    '- SHAPE cnn for us here only-
    Private mCnnShape As OleDbConnection   '=  ADODB.Connection

    Private mIntSupplier_id As Integer = -1
    Private msSupplierName As String = ""
    Private msSupplierBarcode As String = ""

    '- Stock list now in dataGridView -
    Private mColPrefsGoods As Collection
    Private mBrowse1 As clsBrowse3
    Private mLngSelectedRow As Integer = -1

    Private mIntGoods_id As Integer = -1
    '= = = = = = = = = = = = =
    Private msDefaultPrinterName As String = ""

    '- for printing..
    Private mColSelectedGoodsTransaction As Collection

    '-- now this gets all Goods Info..
    Private mClsGoodsInfo1 As clsGoodsInfo
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- P r o p e r t i e s --
    '-- P r o p e r t i e s --

    WriteOnly Property VersionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
            labVersion.Text = msVersionPOS
        End Set
    End Property
    '= = = = = = = = = = = = = = 

    WriteOnly Property BusinessName() As String
        Set(ByVal value As String)
            msBusinessName = value
        End Set
    End Property  '-business name-
    '= = = = = = = = = = = = = = 

    WriteOnly Property SupplierName() As String
        Set(ByVal value As String)
            labSupplier.Text = value
            msSupplierName = value
        End Set
    End Property
    '= = = = = = = = = = = = = 

    WriteOnly Property SupplierBarcode() As String
        Set(ByVal value As String)
            msSupplierBarcode = value
        End Set
    End Property
    '= = = = = = = = = = = = = = 

    WriteOnly Property SqlServer() As String
        Set(ByVal Value As String)
            msServer = Value
        End Set
    End Property '--server-
    '= = = = = =  = = =  = =

    WriteOnly Property connectionSql() As OleDbConnection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--cnn sql..--
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
        Set(ByVal Value As Integer)
            mIntSupplier_id = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = 

    WriteOnly Property staffNname() As String
        Set(ByVal Value As String)
            labStaffName.Text = Value
            msStaffName = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = 
    '-===FF->

    '--- INITIALISE Goods Browser.for Goods Lookup--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(ByVal sTableName As String, _
                                        Optional ByVal sSrchWhereCond As String = "") As Boolean

        '=Dim colPrefs As Collection
        '= Dim sHostTablename As String
        Dim sWhere As String = ""

        If (mIntSupplier_id > 0) Then
            sWhere = " (supplier_id=" & CStr(mIntSupplier_id) & ") "
        End If

        mBrowse1 = New clsBrowse3 '== clsBrowse22

        mBrowse1.connection = mCnnSql  '= mRetailHost1.connection
        mBrowse1.colTables = mColSqlDBInfo '= mRetailHost1.colTables 
        mBrowse1.IsSqlServer = True   '= mRetailHost1.IsSqlServer
        mBrowse1.DBname = msSqlDbName  '= mRetailHost1.DBname

        '--  get table/prefs info for this host..--
        mBrowse1.tableName = sTableName  '= "customer"  '==sHostTablename
        mBrowse1.DataGrid = dgvGoodsList

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
        mBrowse1.PreferredColumns = mColPrefsGoods '== mColPrefs
        mBrowse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly

        '=3501.1221=
        mBrowse1.ReformatDecimalColumns = True
        FrameBrowse.Enabled = True

        mLngSelectedRow = -1
        mBrowse1.Activate() '-- go..--
        '== txtFind.Focus()
        dgvGoodsList.Select()

    End Function '--init-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--

    Private Function mbBrowseGoodsTable(Optional ByRef sSrchWhereCond As String = "") As Boolean
        Dim sWhere As String = ""

        If (mBrowse1 Is Nothing) Then
            Call mbInitialiseBrowse("GoodsReceived")
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
    End Function  ''--mbBrowseStockTable--
    '= = = = = =  = == =
    '-===FF->

    '- show Goods-
    Private mColAllItemSerials As Collection  '-- for current goods invoice.

    'Private Function mbShowGoodsInfo_EXPORTED(ByVal intGoods_id As Integer) As Boolean

    '    '-  get goods info..

    '    Dim sSql, sShapeSql, s1, sErrorMsg, sList As String
    '    Dim dataTable1, dataTableOrder As DataTable
    '    Dim goodsRow1, orderRow1 As DataRow
    '    Dim rdrItems As OleDbDataReader
    '    Dim cmd1 As OleDbCommand
    '    Dim colGoodsInfo, colGoodsItems, colGoodsPO As Collection

    '    mbShowGoodsInfo_EXPORTED = False
    '    '= dgvGoodsList.Enabled = False   '-disbale to avoid re-enterting-
    '    txtSerialsList.Text = ""

    '    mColSelectedGoodsTransaction = New Collection
    '    colGoodsInfo = New Collection

    '    '--lookup goods-id-
    '    mIntGoods_id = intGoods_id
    '    '--  get recordset as collection for SELECT..--
    '    sSql = "SELECT *, staff.docket_name FROM [GoodsReceived] "
    '    sSql &= "    JOIN staff ON GoodsReceived.staff_id=staff.staff_id "
    '    sSql &= " WHERE (goods_id=" & CStr(intGoods_id) & ");"
    '    If gbGetDataTable(mCnnSql, dataTable1, sSql) Then
    '        If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
    '            goodsRow1 = dataTable1.Rows(0)
    '            labItemsHdr.Text = "Items Invoiced for: " & vbCrLf & _
    '                                   "Invoice No: " & goodsRow1.Item("invoice_no")
    '            labGoodsTotal.Text = "Total_Ex : " & FormatCurrency(CDec(goodsRow1.Item("total_ex")), 2) & vbCrLf & _
    '                                 "Total_Inc: " & FormatCurrency(CDec(goodsRow1.Item("total_inc")), 2)
    '            labReceivedBy.Text = goodsRow1.Item("docket_name")
    '            '--ok-
    '        Else '-nothing-
    '            MsgBox("ERROR-no Goods dataset info returned...", MsgBoxStyle.Exclamation)
    '            Exit Function
    '        End If  '--nothing-
    '    Else  '-error-
    '        MsgBox("ERROR getting Goods dataset..", MsgBoxStyle.Exclamation)
    '        Exit Function
    '    End If
    '    sList = ""

    '    '- set up Goods main Info for printing.
    '    '--
    '    colGoodsInfo.Add(goodsRow1.Item("goods_id"), "goods_id")
    '    colGoodsInfo.Add(goodsRow1.Item("supplier_id"), "supplier_id")
    '    colGoodsInfo.Add(goodsRow1.Item("goods_date"), "goods_date")
    '    colGoodsInfo.Add(goodsRow1.Item("invoice_no"), "invoice_no")
    '    colGoodsInfo.Add(goodsRow1.Item("invoice_date"), "invoice_date")
    '    colGoodsInfo.Add(goodsRow1.Item("orderNoSuffix"), "orderNoSuffix")
    '    colGoodsInfo.Add(goodsRow1.Item("docket_name"), "docket_name")
    '    colGoodsInfo.Add(goodsRow1.Item("total_ex"), "total_ex")
    '    colGoodsInfo.Add(goodsRow1.Item("total_tax"), "total_tax")
    '    colGoodsInfo.Add(goodsRow1.Item("total_inc"), "total_inc")

    '    colGoodsPO = New Collection
    '    Dim intPO_id As Integer = goodsRow1.Item("order_id")
    '    If (intPO_id > 0) Then
    '        '--get PO info..
    '        sSql = "SELECT order_id, order_date, orderNoSuffix, PurchaseOrder.comments, "
    '        sSql &= " staff.docket_name "
    '        sSql &= " FROM dbo.PurchaseOrder "
    '        sSql &= "    JOIN staff ON PurchaseOrder.staff_id=staff.staff_id "
    '        sSql &= " WHERE (order_id=" & CStr(intPO_id) & "); "
    '        If gbGetDataTable(mCnnSql, dataTableOrder, sSql) Then
    '            If (Not (dataTableOrder Is Nothing)) AndAlso (dataTableOrder.Rows.Count > 0) Then  '-something-
    '                '-- add P-order info to printing..
    '                orderRow1 = dataTableOrder.Rows(0)
    '                colGoodsPO.Add(orderRow1.Item("order_id"), "order_id")
    '                colGoodsPO.Add(orderRow1.Item("order_date"), "order_date")
    '                colGoodsPO.Add(orderRow1.Item("docket_name"), "docket_name")
    '            End If  '-nothing-
    '        End If  '-get
    '    End If  '-po-
    '    colGoodsInfo.Add(colGoodsPO, "Goods_PO")

    '    '-- Business stuff..
    '    mColSelectedGoodsTransaction.Add(msSupplierName, "SupplierName")
    '    mColSelectedGoodsTransaction.Add(msSupplierBarcode, "SupplierBarcode")
    '    mColSelectedGoodsTransaction.Add(msBusinessName, "BusinessName")

    '    '-- get all items, with serials..
    '    listViewGoodsItems.Items.Clear()
    '    listViewGoodsItems.SelectedItems.Clear()

    '    sShapeSql = "SHAPE {SELECT *, stock.barcode, cat1, description "
    '    sShapeSql &= "        FROM dbo.GoodsReceivedLine AS GRL "
    '    sShapeSql &= "          JOIN stock ON (stock.stock_id=GRL.stock_id) "
    '    sShapeSql &= "        WHERE (GRL.goods_id=" & CStr(intGoods_id) & ") ORDER BY GRL.line_id }"
    '    sShapeSql &= " APPEND ( {SELECT SA.SerialNumber, SAT.type_line_Id AS line_id, SAT.type_Id, SAT.tran_type "
    '    sShapeSql &= "            FROM SerialAuditTrail AS SAT "
    '    sShapeSql &= "              JOIN SerialAudit AS SA ON (SA.serial_id=SAT.SerialAudit_id) "
    '    sShapeSql &= "           WHERE (SAT.type_Id= " & intGoods_id & ") " '-- AND (SAT.type_line_Id=GRL.line_id ) "
    '    sShapeSql &= "            AND ( (SAT.tran_type LIKE 'GoodsRec%') OR (SAT.tran_type='GR') )  }"
    '    sShapeSql &= "    AS rsSerials RELATE line_id TO line_Id )"

    '    '-- start retrieval-
    '    Try
    '        '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
    '        cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
    '        rdrItems = cmd1.ExecuteReader
    '    Catch ex As Exception
    '        MsgBox("Error getting Goods Items recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '        dgvGoodsList.Enabled = True
    '        Exit Function
    '    End Try

    '    Dim listItem1 As ListViewItem
    '    Dim rdrSerials As OleDbDataReader
    '    Dim intItemNo As Integer = 0
    '    Dim colThisItem, colItemSerials As Collection

    '    '-for printing-
    '    colGoodsItems = New Collection
    '    '==  load items listview..
    '    mColAllItemSerials = New Collection

    '    If rdrItems.HasRows Then
    '        Do While rdrItems.Read
    '            '= intLineNo = CInt(rdrItems.Item("line_id"))  '--item line no in goods.Lines
    '            intItemNo += 1
    '            listItem1 = New ListViewItem(CStr(intItemNo))  '-- number lines from 1..
    '            s1 = rdrItems.Item("barcode")
    '            listItem1.SubItems.Add(s1)  '-barcode
    '            listItem1.SubItems.Add(rdrItems.Item("cat1"))
    '            listItem1.SubItems.Add(rdrItems.Item("description"))
    '            listItem1.SubItems.Add(FormatCurrency(rdrItems.Item("cost_ex"), 2))
    '            listItem1.SubItems.Add(rdrItems.Item("Goods_taxCode"))
    '            '= listItem1.SubItems.Add(FormatCurrency(rdrItems.Item("cost_tax"), 2))
    '            listItem1.SubItems.Add(FormatCurrency(rdrItems.Item("cost_inc"), 2))
    '            listItem1.SubItems.Add(CStr(rdrItems.Item("quantity")))
    '            listItem1.SubItems.Add(FormatCurrency(rdrItems.Item("total_ex"), 2))
    '            listItem1.SubItems.Add(FormatCurrency(rdrItems.Item("total_inc"), 2))

    '            listViewGoodsItems.Items.Add(listItem1)
    '            '-- Collect item info for printing..
    '            colThisItem = New Collection
    '            colThisItem.Add(rdrItems.Item("stock_id"), "stock_id")
    '            colThisItem.Add(rdrItems.Item("barcode"), "barcode")
    '            colThisItem.Add(rdrItems.Item("cat1"), "cat1")
    '            colThisItem.Add(rdrItems.Item("description"), "description")
    '            colThisItem.Add(rdrItems.Item("Goods_taxCode"), "Goods_taxCode")
    '            colThisItem.Add(rdrItems.Item("cost_ex"), "cost_ex")
    '            colThisItem.Add(rdrItems.Item("cost_tax"), "cost_tax")
    '            colThisItem.Add(rdrItems.Item("cost_inc"), "cost_inc")
    '            colThisItem.Add(rdrItems.Item("quantity"), "quantity")
    '            colThisItem.Add(rdrItems.Item("total_ex"), "total_ex")
    '            colThisItem.Add(rdrItems.Item("total_inc"), "total_inc")

    '            '-- check for serials..
    '            colItemSerials = New Collection
    '            If TypeOf rdrItems.Item("rsSerials") Is IDataReader Then
    '                '-- has serials list.
    '                rdrSerials = rdrItems.Item("rsSerials")
    '                If rdrSerials.HasRows Then
    '                    Do While rdrSerials.Read
    '                        s1 = Trim(rdrSerials.Item("SerialNumber"))
    '                        If (s1 <> "") Then
    '                            colItemSerials.Add(s1)
    '                            '-test-
    '                            '= MsgBox("Found serial: " & s1, MsgBoxStyle.Information)
    '                        End If
    '                    Loop
    '                End If
    '                rdrSerials.Close()
    '            End If '-datareader-
    '            '-- save all serials in overall collection for this goods item.
    '            mColAllItemSerials.Add(colItemSerials, CStr(intItemNo))
    '            '-- save all serials for printing..
    '            colThisItem.Add(colItemSerials, "serials")
    '            '- add item to Goods.
    '            colGoodsItems.Add(colThisItem)
    '        Loop '-items-
    '    End If
    '    colGoodsInfo.Add(colGoodsItems, "items")
    '    mColSelectedGoodsTransaction.Add(colGoodsInfo, "Goods_Info")
    '    rdrItems.Close()
    '    listViewGoodsItems.SelectedItems.Clear()

    '    If (listViewGoodsItems.Items.Count > 0) Then
    '        '== NO!  do not move focus from Goods..  listViewGoodsItems.Items(0).Focused = True
    '        listViewGoodsItems.Items(0).Selected = True
    '    End If

    '    '== dgvGoodsList.Enabled = True
    '    '= listViewGoodsItems.Select()
    '    '= Call listViewGoodsItems_Click(listViewGoodsItems, New System.EventArgs)

    'End Function  '-mbShowGoodsInfo-
    '= = = = = =  = == = = = = = = =
    '-===FF->

    '-4201.1027-
    '-- Updated version.. get info from new class.

    Private Function mbShowGoodsInfo(ByVal intGoods_id As Integer) As Boolean
        Dim colGoodsInfo, colGoodsItems, colGoodsPO, colThisItem As Collection
        Dim colItemSerials As Collection
        Dim s1 As String

        mbShowGoodsInfo = False

        If Not mClsGoodsInfo1.GetCollectedGoodsInfo(intGoods_id, mColSelectedGoodsTransaction) Then
            MsgBox("Lookup Goods: Error- Failed to get Goods Info.", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '- pick out what we need from full collection..
        colGoodsInfo = mColSelectedGoodsTransaction.Item("Goods_Info")
        colGoodsItems = colGoodsInfo.Item("items")
        colGoodsPO = colGoodsInfo.Item("Goods_PO")

        labReceivedBy.Text = mClsGoodsInfo1.LabReceivedBy_text
        labGoodsTotal.Text = mClsGoodsInfo1.LabGoodsTotal_text
        labItemsHdr.Text = mClsGoodsInfo1.LabItemsHdr_text

        '-- get all items, with serials..
        listViewGoodsItems.Items.Clear()
        listViewGoodsItems.SelectedItems.Clear()

        Dim listItem1 As ListViewItem
        Dim intItemNo As Integer = 0

        '--collect together (remember) all serials for all items..
        mColAllItemSerials = New Collection

        For Each colThisItem In colGoodsItems
            intItemNo += 1
            listItem1 = New ListViewItem(CStr(intItemNo))  '-- number lines from 1..
            s1 = colThisItem.Item("barcode")
            listItem1.SubItems.Add(s1)  '-barcode
            listItem1.SubItems.Add(colThisItem.Item("cat1"))
            listItem1.SubItems.Add(colThisItem.Item("description"))
            listItem1.SubItems.Add(FormatNumber(colThisItem.Item("cost_ex"), 2))
            listItem1.SubItems.Add(colThisItem.Item("Goods_taxCode"))
            '= listItem1.SubItems.Add(FormatCurrency(rdrItems.Item("cost_tax"), 2))
            listItem1.SubItems.Add(FormatNumber(colThisItem.Item("cost_inc"), 2))
            listItem1.SubItems.Add(CStr(colThisItem.Item("quantity")))
            listItem1.SubItems.Add(FormatNumber(colThisItem.Item("total_ex"), 2))
            listItem1.SubItems.Add(FormatNumber(colThisItem.Item("total_inc"), 2))

            listViewGoodsItems.Items.Add(listItem1)
            '-- get serials..
            colItemSerials = colThisItem.Item("serials")
            '-- save all serials in overall collection for this goods item.
            mColAllItemSerials.Add(colItemSerials, CStr(intItemNo))
        Next colThisItem

        listViewGoodsItems.SelectedItems.Clear()
        If (listViewGoodsItems.Items.Count > 0) Then
            '== NO!  do not move focus from Goods..  listViewGoodsItems.Items(0).Focused = True
            listViewGoodsItems.Items(0).Selected = True
        End If

    End Function  '-mbShowGoodsInfo=
    '= = = = = =  = == =
    '-===FF->

    '-- grh= 19-June-2017-

    Private Sub frmLookupGoods_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim s1, sName As String

        '--  stock--
        mColPrefsGoods = New Collection
        mColPrefsGoods.Add("goods_id")
        mColPrefsGoods.Add("invoice_no")
        mColPrefsGoods.Add("orderNoSuffix")
        mColPrefsGoods.Add("goods_date")
        mColPrefsGoods.Add("invoice_date")

        mColPrefsGoods.Add("total_ex")
        mColPrefsGoods.Add("total_inc")
        mColPrefsGoods.Add("supplier_id")
        mColPrefsGoods.Add("staff_id")

        FrameBrowse.Text = ""
        grpBoxGoodsLookup.Text = ""
        grpBoxInvoice.Text = ""

        txtGoodsSearch.Text = ""
        labGoodsTotal.Text = ""
        labReceivedBy.Text = ""

        '- get printers collection and set up combos.
        cboInvoicePrinters.Items.Clear()
        '= cboReceiptPrinters.Items.Clear()

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboInvoicePrinters.Items.Add(sName)
                '= cboReceiptPrinters.Items.Add(sName)
                '=3411.0421- See below-
                '= If (InStr(LCase(sName), "adobe pdf") > 0) Then
                '=     msPdfPrinterName = sName  '-save PDF printer name--
                '= End If
            Next sName
            If (msDefaultPrinterName <> "") Then
                cboInvoicePrinters.SelectedItem = msDefaultPrinterName
            Else
                cboInvoicePrinters.SelectedIndex = 0
            End If
        End If '-getAvail.-  
        labToday.Text = Format(CDate(DateTime.Today), "ddd dd-MMM-yyyy")

        Call CenterForm(Me)

     End Sub  '-load-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--activated---

    Private Sub frmLookupGoods_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        '-- do sub at startup only..
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '-activated-
    '= = = = == = = = = = = = =

    '--Shown-

    Private Sub frmLookupGoods_Shown(sender As Object, _
                                     e As EventArgs) Handles MyBase.Shown
        Dim sConnect, sErrors As String
        Dim intRecordsAffected As Integer

        If (mIntSupplier_id <= 0) Then
            MsgBox("No supplier id provided..", MsgBoxStyle.Exclamation)
            Me.Hide()
            Exit Sub
        End If

        '-- setup sql SHAPE connection for reports..--
        '-- setup sql SHAPE connection for reports..--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mCnnShape = New OleDbConnection '=  ADODB.Connection
        sConnect = "Provider=MSDataShape; Server=" & msServer & "; Trusted_Connection=true; Integrated Security=SSPI; "
        sConnect = sConnect & "; Data Provider=SQLOLEDB; Data Source=" & msServer & "; "
        '=== sConnect = sConnect + "; Password=" + msSqlPwd + "; User ID=" + msSqlUid
        If gbConnectSql(mCnnShape, sConnect) Then
            '--FrameReport.Enabled = True   '--show report options frame..--
            '--FrameStatus.Enabled = True
        Else
            MsgBox("Login to sql SHAPE dataSource has failed." & vbCrLf, MsgBoxStyle.Exclamation)
            '====FrameReport.Enabled = False
            Me.Hide()
            '==End
        End If '--connected-
        If Not gbExecuteCmd(mCnnShape, "USE " & msSqlDbName & vbCrLf, intRecordsAffected, sErrors) Then
            MsgBox(vbCrLf & "Failed USE for SHAPE connection to DATABASE: '" & _
                                            msSqlDbName & "'.." & vbCrLf & sErrors, MsgBoxStyle.Critical)
        End If '--use-
        '= mCnnShape.CommandTimeout = 10 '-- 10 sec cmd timeout..-
        '= mCnnShape.CursorLocation = ADODB.CursorLocationEnum.adUseClient

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '--init class for goods Info.

        mClsGoodsInfo1 = New clsGoodsInfo(msServer, mCnnSql, msSqlDbName, msVersionPOS)

        '-- get initial goods Invoices list
        Call mbInitialiseBrowse("GoodsReceived")

        mbIsLoading = False

    End Sub  '--Shown-
    '= = = = = = = = == = 
    '= = = = = = = = = = = = = =  
    '-===FF->

    '- G o o d s  - stuff--

    '--BROWSING Goods.. --

    Private Sub dgvGoodsList_SelectionChanged(ByVal sender As Object, _
                                               ByVal ev As EventArgs) Handles dgvGoodsList.SelectionChanged
        Dim ix, intRow, intGoods_id As Integer
        Dim s1 As String

        If mbIsLoading Then
            Exit Sub
        End If
        If (dgvGoodsList.Rows.Count > 0) Then
            intRow = dgvGoodsList.CurrentRow.Index   '= ev.RowIndex
            If (intRow >= 0) Then
                With dgvGoodsList.Rows(intRow)
                    s1 = .Cells(0).Value
                    If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                        intGoods_id = CInt(s1)
                        If (intGoods_id > 0) And (intGoods_id <> mIntGoods_id) Then '-- has changed..-
                            Call mbShowGoodsInfo(intGoods_id)
                        End If
                    End If
                End With
            End If  '-intRow-
        End If  '-count-
        '=lCol = ev.ColumnIndex
    End Sub  '-SelectionChanged-
    '= = = = = = = = = === 
    '-===FF->

    '--  Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvGoodsList_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvGoodsList.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvGoodsList.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowse1.SortColumn(sName)
    End Sub
    '= = = = = = = = =  = = =
    '= = = = = = = = =  = = =
    '-===FF->

    '-- cell click.--
    '-- cell click.--

    Private Sub dgvGoodsList_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvGoodsList.CellMouseClick
        Dim lRow, lCol As Integer
        Dim intGoods_id As Integer
        Dim colKeys, colRowValues As Collection

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (dgvGoodsList.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                intGoods_id = colRowValues.Item("goods_id")("value")
                Call mbShowGoodsInfo(intGoods_id)
                'intCustomer_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                'If (intCustomer_id > 0) And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                '    Call mbShowCustomerInfo(intCustomer_id)
                'End If
            End If  '-selected-
        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =

    '- print Goods Invoice..

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim clsPrintGoods1 As clsPrintGoods

        If mColSelectedGoodsTransaction IsNot Nothing Then
            clsPrintGoods1 = New clsPrintGoods()

            clsPrintGoods1.StaffName = msStaffName
            clsPrintGoods1.BusinessName = msBusinessName
            clsPrintGoods1.SupplierName = msSupplierName
            clsPrintGoods1.SupplierBarcode = msSupplierBarcode
            clsPrintGoods1.versionPOS = msVersionPOS

            '- Now preview-
            If Not clsPrintGoods1.PrintGoodsReceived(mColSelectedGoodsTransaction, _
                                                     cboInvoicePrinters.SelectedItem, True) Then


            End If  '-print..

        End If   '-nothing.-

    End Sub  '-print-
    '= = = = = =  ==  =
    '-===FF->

    '--mouse activity---  
 
    '-- key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                           Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        Dim intStaff_id, intGoods_id As Integer
        Dim colKeys, colRowValues As Collection

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If dgvGoodsList.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = dgvGoodsList.SelectedRows(0).Cells(0).RowIndex
                If (lRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow
                    '= Call mbSelectStockRow(mLngSelectedRow)
                    Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                    intGoods_id = colRowValues.Item("goods_id")("value")
                    Call mbShowGoodsInfo(intGoods_id)
                    'intCustomer_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                    'If (intCustomer_id > 0) And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                    '    Call mbShowCustomerInfo(intCustomer_id)
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
        If mbIsLoading Then
            Exit Sub
        End If

        Call mBrowse1.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '-- Stock Browser..  Full text Search..--
    '-- Stock Browser..  Full text Search..--

    Private Sub cmdGoodsSearch_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles cmdGoodsSearch.Click
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
                      {"invoice_no", "orderNoSuffix"}

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtGoodsSearch.Text), asColumns)
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere + " AND "
            sWhere = sWhere + sSql
        End If
        Call mbBrowseGoodsTable(sWhere)

    End Sub '-cmdCustomerSearch-
    '= = = = = = = = = = = = =  =

    Private Sub cmdClearCustomerSearch_Click(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) Handles cmdClearGoodsSearch.Click
        txtGoodsSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdGoodsSearch_Click(cmdGoodsSearch, New System.EventArgs())

    End Sub  '-ClearStockSearch-
    '= = = = = = = = = = = = = = = =

    '==  END of goods BROWSING..--
    '==  END of goods BROWSING..--
    '-===FF->

    '-- Show Serials if any.

    '--listViewJobs_Click--

    Private Sub listViewGoodsItems_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles listViewGoodsItems.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim intItemNo As Integer
        Dim colItemSerials As Collection
        '--  update serials info display if selection has moved..--
        txtSerialsList.Text = ""
        Dim selectedItems As ListView.SelectedListViewItemCollection = listViewGoodsItems.SelectedItems
        If (selectedItems IsNot Nothing) AndAlso (selectedItems.Count > 0) Then
            item1 = selectedItems(0)
        Else
            Exit Sub
        End If

        '= item1 = listViewGoodsItems.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else  '- get selected serials if any.
            intItemNo = CInt(item1.Text)
            If (mColAllItemSerials IsNot Nothing) Then
                If mColAllItemSerials.Contains(item1.Text) Then  '-item no.
                    colItemSerials = mColAllItemSerials.Item(item1.Text)
                    For Each sSerial As String In colItemSerials
                        txtSerialsList.Text &= sSerial & vbCrLf
                    Next sSerial
                End If

            End If  '-nothing-
        End If '--selected..-
    End Sub '--listViewJobs_Click--
    '= = = = = = = =  =

    '-listViewSaleItems_SelectedIndexChanged-

    Private Sub listViewGoodsItems_SelectedIndexChanged(sender As Object, _
                                                       ev As EventArgs) Handles listViewGoodsItems.SelectedIndexChanged
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim intItemNo As Integer
        Dim colItemSerials As Collection
        '--  update serials info display if selection has moved..--
        txtSerialsList.Text = ""
        Dim selectedItems As ListView.SelectedListViewItemCollection = listViewGoodsItems.SelectedItems
        If (selectedItems IsNot Nothing) AndAlso (selectedItems.Count > 0) Then
            item1 = selectedItems(0)
        Else
            Exit Sub
        End If

        '= item1 = listViewGoodsItems.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else  '- get selected serials if any.
            intItemNo = CInt(item1.Text)
            If (mColAllItemSerials IsNot Nothing) Then
                If mColAllItemSerials.Contains(item1.Text) Then  '-item no.
                    colItemSerials = mColAllItemSerials.Item(item1.Text)
                    For Each sSerial As String In colItemSerials
                        txtSerialsList.Text &= sSerial & vbCrLf
                    Next sSerial
                End If

            End If  '-nothing-
        End If '--selected..-

    End Sub '-listViewGoodsItems_SelectedIndexChanged-
    '= = = = = = = === 

    '-closing-

    Private Sub me_formclosing(sender As Object, ev As EventArgs) Handles Me.FormClosing

        '- clost mCnnShape if not nothing.


    End Sub  '-closing-
    '= = = = = = == = = =

End Class  '--frmLookupGoods=

'= = = = the end == == == 