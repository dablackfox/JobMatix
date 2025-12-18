

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

Public Class clsGoodsInfo

    '==
    '== -- Created 4201.1027  27-Oct-2019=  
    '==
    '==     -- New Class to Collect Info for a  Goods Received Transaction.
    '==
    '= = = = = = = = = = = = = = = = = = === = = = = = = = =

    Private mCnnSql As OleDbConnection  '=  ADODB.Connection
    Private mbIsSqlAdmin As Boolean = False

    '-mCnnShape-
    Private mCnnShape As OleDbConnection

    Private msServer As String = ""

    Private msSqlVersion As String = ""
    Private msInputDBNameJobs As String = ""

    Private mbIsLoading As Boolean = False
    Private mbCloseRequested As Boolean = False

    Private msSqlDbName As String = ""
    '= Private mColSqlDBInfo As Collection '--  jobs DB info--
    Private msRuntimeLogPath As String = ""

    Private msMachineName As String = "" '--local machine--
    Private msComputerName As String = "" '--client or Fat machine--
    '= Private mbIsThinClient As Boolean = False

    Private msAppPath As String = ""
    Private msDllversion As String = ""

    Private mSysInfo1 As clsSystemInfo

    '=3403.1010=-- now split server/instance..--
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""

    Private msVersionPOS As String = ""

    '= Private mFormWait1 As frmWait

    '= Private msStaffName As String = ""
    '= Private mIntStaff_id As Integer = -1
    '= = = = = =  = == =

    Private msBusinessName As String = ""
    Private msSupplierName As String = ""
    Private msSupplierBarcode As String = ""

    '- show Goods-
    Private mColAllItemSerials As Collection  '-- for current goods invoice.

    '-- All info this Goods.
    Private mColSelectedGoodsTransaction As Collection

    Private mIntGoods_id As Integer = -1
    Private msSerialsList As String = ""

    '--header Info for frmLookupGoods.
    Private msLabItemsHdr As String = ""
    Private msLabGoodsTotal As String = ""
    Private msLabReceivedBy As String = ""

    Private mbConnectedOk As Boolean = False
    '= = = = = =  = == =
    '-===FF->

    '-Collect all LABEL disply info for selected Goods-id..

    Private Function mbShowGoodsInfo(ByVal intGoods_id As Integer) As Boolean

        '-  get goods info..

        Dim sSql, sShapeSql, s1, sErrorMsg, sList As String
        Dim dataTable1, dataTableOrder As DataTable
        Dim goodsRow1, orderRow1 As DataRow
        Dim rdrItems As OleDbDataReader
        Dim cmd1 As OleDbCommand
        Dim colGoodsInfo, colGoodsItems, colGoodsPO As Collection

        mbShowGoodsInfo = False
        '= dgvGoodsList.Enabled = False   '-disbale to avoid re-enterting-
        '= txtSerialsList.Text = ""
        msSerialsList = ""

        mColSelectedGoodsTransaction = New Collection
        colGoodsInfo = New Collection

        '--lookup goods-id-
        mIntGoods_id = intGoods_id
        '--  get recordset as collection for SELECT..--
        sSql = "SELECT *, staff.docket_name, Supplier.supplierName, Supplier.barcode AS supplier_barcode "
        sSql &= " FROM [GoodsReceived] AS GR "
        sSql &= "    LEFT JOIN staff ON GR.staff_id=staff.staff_id "
        sSql &= "   LEFT JOIN Supplier ON (GR.Supplier_Id=Supplier.Supplier_Id)  "
        sSql &= " WHERE (goods_id=" & CStr(intGoods_id) & ");"
        If gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
                goodsRow1 = dataTable1.Rows(0)
                msLabItemsHdr = "Items Invoiced for: " & vbCrLf & _
                       "Invoice No: " & goodsRow1.Item("invoice_no")
                msLabGoodsTotal = "Total_Ex : " & FormatCurrency(CDec(goodsRow1.Item("total_ex")), 2) & vbCrLf & _
                                     "Total_Inc: " & FormatCurrency(CDec(goodsRow1.Item("total_inc")), 2)
                msLabReceivedBy = goodsRow1.Item("docket_name")
                '--ok-
            Else '-nothing-
                MsgBox("ERROR-no Goods dataset info returned...", MsgBoxStyle.Exclamation)
                Exit Function
            End If  '--nothing-
        Else  '-error-
            MsgBox("ERROR getting Goods dataset..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        sList = ""

        '- set up Goods main Info for printing.
        '--
        colGoodsInfo.Add(goodsRow1.Item("goods_id"), "goods_id")
        colGoodsInfo.Add(goodsRow1.Item("supplier_id"), "supplier_id")
        colGoodsInfo.Add(goodsRow1.Item("supplierName"), "supplierName")
        msSupplierName = goodsRow1.Item("supplierName")
        colGoodsInfo.Add(goodsRow1.Item("supplier_barcode"), "supplier_barcode")
        msSupplierBarcode = goodsRow1.Item("supplier_barcode")
        colGoodsInfo.Add(goodsRow1.Item("goods_date"), "goods_date")
        colGoodsInfo.Add(goodsRow1.Item("invoice_no"), "invoice_no")
        colGoodsInfo.Add(goodsRow1.Item("invoice_date"), "invoice_date")
        colGoodsInfo.Add(goodsRow1.Item("orderNoSuffix"), "orderNoSuffix")
        colGoodsInfo.Add(goodsRow1.Item("docket_name"), "docket_name")
        colGoodsInfo.Add(goodsRow1.Item("total_ex"), "total_ex")
        colGoodsInfo.Add(goodsRow1.Item("total_tax"), "total_tax")
        colGoodsInfo.Add(goodsRow1.Item("total_inc"), "total_inc")

        colGoodsPO = New Collection
        Dim intPO_id As Integer = goodsRow1.Item("order_id")
        If (intPO_id > 0) Then
            '--get PO info..
            sSql = "SELECT order_id, order_date, orderNoSuffix, PurchaseOrder.comments, "
            sSql &= " staff.docket_name "
            sSql &= " FROM dbo.PurchaseOrder "
            sSql &= "    JOIN staff ON PurchaseOrder.staff_id=staff.staff_id "
            sSql &= " WHERE (order_id=" & CStr(intPO_id) & "); "
            If gbGetDataTable(mCnnSql, dataTableOrder, sSql) Then
                If (Not (dataTableOrder Is Nothing)) AndAlso (dataTableOrder.Rows.Count > 0) Then  '-something-
                    '-- add P-order info to printing..
                    orderRow1 = dataTableOrder.Rows(0)
                    colGoodsPO.Add(orderRow1.Item("order_id"), "order_id")
                    colGoodsPO.Add(orderRow1.Item("order_date"), "order_date")
                    colGoodsPO.Add(orderRow1.Item("docket_name"), "docket_name")
                End If  '-nothing-
            End If  '-get
        End If  '-po-
        colGoodsInfo.Add(colGoodsPO, "Goods_PO")

        '-- Business stuff..
        mColSelectedGoodsTransaction.Add(msSupplierName, "SupplierName")
        mColSelectedGoodsTransaction.Add(msSupplierBarcode, "SupplierBarcode")
        mColSelectedGoodsTransaction.Add(msBusinessName, "BusinessName")

        '-- get all items, with serials..

        sShapeSql = "SHAPE {SELECT *, stock.barcode, cat1, description "
        sShapeSql &= "        FROM dbo.GoodsReceivedLine AS GRL "
        sShapeSql &= "          JOIN stock ON (stock.stock_id=GRL.stock_id) "
        sShapeSql &= "        WHERE (GRL.goods_id=" & CStr(intGoods_id) & ") ORDER BY GRL.line_id }"
        sShapeSql &= " APPEND ( {SELECT SA.SerialNumber, SAT.type_line_Id AS line_id, SAT.type_Id, SAT.tran_type "
        sShapeSql &= "            FROM SerialAuditTrail AS SAT "
        sShapeSql &= "              JOIN SerialAudit AS SA ON (SA.serial_id=SAT.SerialAudit_id) "
        sShapeSql &= "           WHERE (SAT.type_Id= " & intGoods_id & ") " '-- AND (SAT.type_line_Id=GRL.line_id ) "
        sShapeSql &= "            AND ( (SAT.tran_type LIKE 'GoodsRec%') OR (SAT.tran_type='GR') )  }"
        sShapeSql &= "    AS rsSerials RELATE line_id TO line_Id )"

        '-- start retrieval-
        Try
            '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
            cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
            rdrItems = cmd1.ExecuteReader
        Catch ex As Exception
            MsgBox("Error getting Goods Items recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            Exit Function
        End Try

        Dim rdrSerials As OleDbDataReader
        Dim intItemNo As Integer = 0
        Dim colThisItem, colItemSerials As Collection

        '-for printing-
        colGoodsItems = New Collection
        '==  load items listview..
        mColAllItemSerials = New Collection

        If rdrItems.HasRows Then
            Do While rdrItems.Read
                '= intLineNo = CInt(rdrItems.Item("line_id"))  '--item line no in goods.Lines
                intItemNo += 1
                s1 = rdrItems.Item("barcode")

                '-- Collect item info for printing..
                colThisItem = New Collection
                colThisItem.Add(rdrItems.Item("stock_id"), "stock_id")
                colThisItem.Add(rdrItems.Item("barcode"), "barcode")
                colThisItem.Add(rdrItems.Item("cat1"), "cat1")
                colThisItem.Add(rdrItems.Item("description"), "description")
                colThisItem.Add(rdrItems.Item("Goods_taxCode"), "Goods_taxCode")
                colThisItem.Add(rdrItems.Item("cost_ex"), "cost_ex")
                colThisItem.Add(rdrItems.Item("cost_tax"), "cost_tax")
                colThisItem.Add(rdrItems.Item("cost_inc"), "cost_inc")
                colThisItem.Add(rdrItems.Item("quantity"), "quantity")
                colThisItem.Add(rdrItems.Item("total_ex"), "total_ex")
                colThisItem.Add(rdrItems.Item("total_inc"), "total_inc")

                '-- check for serials..
                colItemSerials = New Collection
                If TypeOf rdrItems.Item("rsSerials") Is IDataReader Then
                    '-- has serials list.
                    rdrSerials = rdrItems.Item("rsSerials")
                    If rdrSerials.HasRows Then
                        Do While rdrSerials.Read
                            s1 = Trim(rdrSerials.Item("SerialNumber"))
                            If (s1 <> "") Then
                                colItemSerials.Add(s1)
                                '-test-
                                '= MsgBox("Found serial: " & s1, MsgBoxStyle.Information)
                            End If
                        Loop
                    End If
                    rdrSerials.Close()
                End If '-datareader-
                '-- save all serials in overall collection for this goods item.
                mColAllItemSerials.Add(colItemSerials, CStr(intItemNo))
                '-- save all serials for printing..
                colThisItem.Add(colItemSerials, "serials")
                '- add item to Goods.
                colGoodsItems.Add(colThisItem)
            Loop '-items-
        End If
        colGoodsInfo.Add(colGoodsItems, "items")
        mColSelectedGoodsTransaction.Add(colGoodsInfo, "Goods_Info")
        rdrItems.Close()

        mbShowGoodsInfo = True

    End Function  '-mbShowGoodsInfo-
    '= = = = = =  = == = = = = = = = =
    '-===FF->

    Public Sub New(ByVal sSqlServerName As String, _
                       ByRef cnnSql As OleDbConnection, _
                         ByVal sSqlDbName As String, _
                            ByVal sVersionPOS As String)

        MyBase.New()

        Dim sConnect, sErrors As String
        Dim intRecordsAffected As Integer

        mbConnectedOk = False
        mCnnSql = cnnSql
        msServer = sSqlServerName
        msSqlDbName = sSqlDbName
        '= mColSqlDBInfo = colSqlDBInfo
        '= msRuntimeLogPath = strLogPath
        '-- initialise..--
        '- Get actual machine running this app process. (NOT the remote client).
        msMachineName = My.Computer.Name  '- for actual machine running this app process. (NOT the remote client).

        msVersionPOS = sVersionPOS
        '= mImageUserLogo = imageUserLogo
        '= mIntStaff_id = intStaff_id
        '= msStaffName = sStaffName

        '-- setup sql SHAPE connection for Goods Serials..--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mCnnShape = New OleDbConnection '=  ADODB.Connection
        sConnect = "Provider=MSDataShape; Server=" & msServer & "; Trusted_Connection=true; Integrated Security=SSPI; "
        sConnect = sConnect & "; Data Provider=SQLOLEDB; Data Source=" & msServer & "; "
        '=== sConnect = sConnect + "; Password=" + msSqlPwd + "; User ID=" + msSqlUid
        If gbConnectSql(mCnnShape, sConnect) Then
            '--FrameReport.Enabled = True   '--show report options frame..--
            '--FrameStatus.Enabled = True
            If Not gbExecuteCmd(mCnnShape, "USE " & msSqlDbName & vbCrLf, intRecordsAffected, sErrors) Then
                MsgBox(vbCrLf & "Failed USE for SHAPE connection to DATABASE: '" & _
                                                msSqlDbName & "'.." & vbCrLf & sErrors, MsgBoxStyle.Critical)
            Else  '-ok=
                mbConnectedOk = True
            End If '--use-
        Else
            MsgBox("Login to sql SHAPE dataSource has failed." & vbCrLf, MsgBoxStyle.Exclamation)
        End If '--connected-
        '= mCnnShape.CommandTimeout = 10 '-- 10 sec cmd timeout..-
        '= mCnnShape.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        msBusinessName = mSysInfo1.item("BUSINESSNAME")


    End Sub  '-new-
    '= = = = = =  = == =
    '-===FF->

    '--header Info for frmLookupGoods.

    'Private msLabItemsHdr As String = ""
    'Private msLabGoodsTotal As String = ""
    'Private msLabReceivedBy As String = ""

    '== These only available after "GetCollectedGoodsInfo" is done..
    '== These only available after "GetCollectedGoodsInfo" is done..
    '== These only available after "GetCollectedGoodsInfo" is done..

    Public ReadOnly Property LabItemsHdr_text As String
        Get
            LabItemsHdr_text = msLabItemsHdr
        End Get
    End Property
    '= = = = = = =

    Public ReadOnly Property LabGoodsTotal_text As String
        Get
            LabGoodsTotal_text = msLabGoodsTotal
        End Get
    End Property
    '= = = = = = =
    Public ReadOnly Property LabReceivedBy_text As String
        Get
            LabReceivedBy_text = msLabReceivedBy
        End Get
    End Property
    '= = = = = = =
    '= = = = = =  = == =
    '-===FF->

    '-- Get/set up  all Info..

    Public Function GetCollectedGoodsInfo(ByVal intRequestedGoods_Id As Integer, _
                                          ByRef colGoodsInfo As Collection) As Boolean

        GetCollectedGoodsInfo = False

        If Not mbConnectedOk Then
            MsgBox("Error- Shaped Sql Connection not done..", MsgBoxStyle.Exclamation)
            Exit Function
        End If  '--connected.

        '-- get all goods info..
        If mbShowGoodsInfo(intRequestedGoods_Id) Then
            colGoodsInfo = mColSelectedGoodsTransaction
            GetCollectedGoodsInfo = True
        End If '-show-

    End Function  '- GetCollectedGoodsInfo-
    '= = = = = = = = = = = = = = = = = = =


End Class  '-clsGoodsInfo-
'= = = = = = = = = = = = =
