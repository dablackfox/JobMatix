Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'= Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Windows.Forms.Application
Imports System.Data
Imports system.data.OleDb

Public Class ucChildSerialLookup

    '-- Lookup Serial and view history..
    '-- If no serial provided, invite user input of serial no..

    '- If no Barcode input, then lookup and select if multiples.
    '-- If found,  Return serial_id, stock_id and barcode.

    '==  grh JobMatixPOS 3.1.3101.1103 -
    '==       >> Implementing Serial Search..
    '==
    '==   grh- JobMatix POS3 3.1.3103.0218-
    '==      >>  Search-  Add grid to Browse all serials...
    '==
    '==   grh- JobMatix POS3 3.1.3103.0422-
    '==      >>  Add Goods info to info panel.  
    '==
    '==  grh. JobMatix 3.1.3107.0805 ---  05-Aug-2015 ===
    '==   >> Now for .Net 4.5.2- (NO.. is back to 3.5)..
    '== = = = = 
    '==
    '==  JmxPOS330.dll  VERSION- 3.3.3301.510  10May2016-
    '==     >> Now. using clsBrowse32 for serials Grid..
    '==
    '==  '=3301.0102  02Jan2017-
    '==     Fix to clear Goods Info text when no Goods info.-
    '==
    '==     v3.3.3307.0207..  07-Feb-2017= ===
    '==      '-chkInstockOnly_CheckedChanged'- NOW does a refresh..
    '== = = = = = = = = =
    '==
    '==     3403.625- 25Jun2017-
    '==      --   Fixes to Browsing/showing info... 
    '==
    '==
    '== -- Updated 3501.1101  01-Nov-2018=  
    '==     -- Fixing Col. Sorting disfunctions Crash and in FindSerials browsing....
    '==
    '==  = = = = = = = =
    '==
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==
    '==    First New Build- 4201.0416 -  Started 16-April-2019.
    '==    -- 4201.0422. NOW is "frmChildSerialLookup"..  
    '==           Is a Clone of frmFindSerial made into TDI NON-MODAL Child..
    '==        For Browsing and searching only..
    '==      NB: Sales will still call frmFindSerial to verify a Sale Serial No.
    '== - - - - RELEASED as 3519.0414 --
    '==    
    '==    -- 4201.0424. TDI Child forms now converted to UserControls....
    '==
    '==
    '= = = =  = = = = = = = = = = = = = =  == = =  = = = = == = = = = = = == == 

    '--  data grid column nos.--
    Private Const DGV_SERIALS_NUMBER As Short = 0
    Private Const DGV_SERIALS_BARCODE As Short = 1
    Private Const DGV_SERIALS_DESCRIPTION As Short = 2
    Private Const DGV_SERIALS_STATUS As Short = 3
    Private Const DGV_SERIALS_CAT1 As Short = 4
    Private Const DGV_SERIALS_CAT2 As Short = 5
    Private Const DGV_SERIALS_SERIALAUDIT_ID As Short = 6
    Private Const DGV_SERIALS_STOCK_ID As Short = 7

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    '= = = = = = = = = = = = = = = = = = = = = =  == 

    Private mbIsInitialising As Boolean = True
    Private mbActivated As Boolean = False   '-to activate once only.-

    Private mbCancelled As Boolean = False
    Private mIntSerial_id As Integer = -1  '- serialAudit_id
    Private mbIsInStock As Boolean = False


    '-- input parameters--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    Private msSerialNumber As String = ""
    Private mIntInputStock_id As Integer = -1
    Private mbIsRefund As Boolean = False

    Private mIntCurrentTrailRow As Integer = -1
    Private mIntCurrentInvoiceNo As Integer = -1

    '-results-
    Private mIntStock_id As Integer = -1
    Private msErrorText As String = ""
    Private msBarcode As String = ""
    Private mIntSalesInvoiceNo As Integer = -1

    Private mIntFormDesignWidth As Integer
    Private mIntFormDesignHeight As Integer

    '-browse32-
    Private mBrowseSerials As clsBrowse3

    Private mIntSelectedRowResults As Integer = -1

    '= = = = = = =

    '= 4201.0422-

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport

    '= = = = = = = = = = = = = = = = = = = = = = = ==

    '-result-
    ReadOnly Property serial_id() As Integer
        Get
            serial_id = mIntSerial_id
        End Get
    End Property '-selectedRow-
    '= = = = = =  = = = = = = =

    ReadOnly Property stock_id() As Integer
        Get
            stock_id = mIntStock_id
        End Get
    End Property '-selectedRow-
    '= = = = = =  = = = = = = =

    ReadOnly Property barcode() As String
        Get
            barcode = msBarcode
        End Get
    End Property '-selectedRow-
    '= = = = = =  = = = = = = =

    '=ReadOnly Property errorText() As String
    '=    Get
    '=        errorText = msErrorText
    '=    End Get
    '=End Property '-selectedRow-
    ''= = = = = =  = = = = = = =

    ReadOnly Property salesInvoiceNo() As Integer
        Get
            salesInvoiceNo = mIntSalesInvoiceNo
        End Get
    End Property
    '= = = = = = = = = = = =

    ReadOnly Property itemInStock As Boolean
        Get
            itemInStock = mbIsInStock
        End Get
    End Property
    '= = = = = = = = = = = = = = = = = = = =

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property '-cancelled-
    '= = = = = =  = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '---  n e w ---

    '-- We overload the constructor by defining more than one 'Sub New()' procedure--
    '-- http://www.vkinfotek.com/oops/constructor-overloading-vb-net.html --

    '--1. NO SerialNo available-
    '--  User must input..
    '--  Then search all serials for this SerNo-
    '--      and get user to select if more than one..
    '--  Then lookup History for this SerialNo-
    '--  and get user to OK or cancel..

    Public Sub New(ByVal cnnSql As OleDbConnection, _
                    ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection)
        mbIsInitialising = True

        InitializeComponent() '-- This call is required by the Windows Form Designer.
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo

        '== MsgBox("User must input serno.")
        labStatus.Text = "User must input serial no."
        msSerialNumber = ""
        txtSerialNo.Text = ""  '= msSerialNumber
    End Sub  '-new-1 -
    '= = = = = = =  = ==  = ==

    '-- SerialNo only available-
    '--  Search all serials for this SerNo-
    '--      and get user to select if more than one..
    '--  Then lookup History for this SerialNo-
    '--  and get user to OK or cancel..

    '=4201.0422- Child SerialLookup  - Find Serial not needed..
    '=4201.0422- Child SerialLookup  - Find Serial not needed..


    'Public Sub New(ByVal cnnSql As OleDbConnection, _
    '                ByVal strSerial As String, _
    '                   ByVal bIsRefund As Boolean)
    '    mbIsInitialising = True
    '    InitializeComponent() '-- This call is required by the Windows Form Designer.
    '    mCnnSql = cnnSql
    '    '== MsgBox(strSerial)
    '    msSerialNumber = strSerial
    '    txtSerialNo.Text = msSerialNumber
    '    mbIsRefund = bIsRefund

    'End Sub  '-new-2 -
    '= = = = = = =  = ==  = ==

    '-- SerialNo AND Stock_id available-
    '--  Lookup History for this SerialNo-
    '--  and get user to OK or cancel..

    '=4201.0422- Child SerialLookup  - Find Serial not needed..
    '=4201.0422- Child SerialLookup  - Find Serial not needed..


    'Public Sub New(ByVal cnnSql As OleDbConnection, _
    '                ByVal strSerial As String, _
    '                 ByVal intStock_id As Integer, _
    '                  ByVal bIsRefund As Boolean)
    '    mbIsInitialising = True
    '    InitializeComponent() '-- This call is required by the Windows Form Designer.
    '    mCnnSql = cnnSql
    '    '== MsgBox(strSerial)
    '    msSerialNumber = strSerial
    '    txtSerialNo.Text = msSerialNumber
    '    mIntInputStock_id = intStock_id
    '    mbIsRefund = bIsRefund

    'End Sub  '-new-3 -
    '= = = = = = = = = = = = = = = = = = = = =
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
        '-- resize main box and top panel-

    End Sub '-SubFormResized
    '= = = = = = = = = = = =  === =

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click...
    '- Return true if ok to Close.

    Public Function SubFormCloseRequest() As Boolean

        '=- Return true if ok to Close.
        SubFormCloseRequest = True

        '==Me.Close()
        '= Call close_me()
    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    '--- INITIALISE Serials Browser..  --
    '--      EX Stocktake.vb..-.--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(ByRef browse1 As clsBrowse3, _
                                        ByVal sSelectList As String, _
                                         ByVal sWhere As String, _
                                          ByRef dgv1 As DataGridView, _
                                           ByRef labRecCount As Label, _
                                           ByRef labFind As Label, _
                                           ByRef txtFind As TextBox) As Boolean
        Dim sHostTablename As String
        Dim colPrefs As Collection
        mbInitialiseBrowse = False
        If browse1 Is Nothing Then
            browse1 = New clsBrowse3 '== clsBrowse22
        End If
        browse1.connection = mCnnSql
        browse1.colTables = mColSqlDBInfo
        browse1.IsSqlServer = True
        browse1.DBname = msSqlDbName

        '--  get table/prefs info for this host..--
        browse1.tableName = "SerialAudit"  '==sHostTablename

        browse1.UserSelectList = sSelectList
        browse1.WhereCondition = sWhere

        '-serialNumber
        browse1.InitialOrder1 = "serialNumber"
        '= browse1.InitialOrder1 = "cat1"
        '= browse1.InitialOrder2 = "cat2"
        '= browse1.InitialOrder3 = "description"

        browse1.DataGrid = dgv1

        '--  pass controls..--
        browse1.showRecCount = labRecCount '--updates rec. retrieval..
        browse1.showFind = labFind '--updates Sort Column display..
        browse1.showTextFind = txtFind '--updates Sort Column display..
        '= sWhere = msMakeStockFilter()  '--service or not..-
        '-- add srch args..
        '== If (sSrchWhereCond <> "") Then
        '== If (sWhere <> "") Then
        '== sWhere &= " AND "
        '= End If
        '== sWhere &= sSrchWhereCond
        '== End If
        browse1.WhereCondition = sWhere
        browse1.PreferredColumns = Nothing  '== using our SELECT-  


        '= TEMP ==
        colPrefs = New Collection
        colPrefs.Add("serial_id")
        colPrefs.Add("serialNumber")
        colPrefs.Add("status")
        colPrefs.Add("stock_id")
        '== browse1.PreferredColumns = colPrefs
        '=- TEMP=  browse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly

        mIntSelectedRowResults = -1
        Try
            browse1.Activate() '-- go..--
            mbInitialiseBrowse = True
        Catch ex As Exception
            MsgBox("Failed to activate Browser object." & vbCrLf & ex.Message)
        End Try
        '== txtFind.Focus()

    End Function '--init-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--GetSerialInfo-
    '---  Lookup/JOIN tables to get info on specific SerialNo..-

    '--Every manufacturer has it's own naming scheme for serial numbers, 
    '--   and they are not guaranteed to be globally unique across manufacturers, 
    '-- but they should be unique per manufacturer as, 
    '--       after all, they want to be able to identify a specific unit. 
    '--  http://serverfault.com/questions/300448/is-a-hard-drives-serial-number-globally-unique

    '-- (Input: strSeriallNo, Stock_id;
    '--   Output: Stock_Id, barcode, cat1, cat2, description, cost, sell,
    '--             Goods_Id, Goodsline_id, Qty, Supplier_Id, SupplierCode, InStock (YES/NO) --)

    '--  Lookup SerialNo in SerialAudit table..--
    '---- SerialAudit links SerailNo to SerialAuditTrail, which --
    '---    gives Stock_id, Goods_Id and and GoodsLine_Id..--

    '-- FOR RA's: --
    '--Lookup Goodsline/Stock to get Line Qty and Supplier_Id, SupplierCode, info..--

    '--  FOR Jobmaint/NewPart, we need In-stock YES/NO column..--
    '--    So now check that this serialNo is actually still in stock..--

    '--= 3101.1024= Stock_id NOT optional..
    '--  ALSO:  colAllSerialsInfo is now a COLLECTION of reord collections..-
    '==    Since if not Stock_id input, then multiple serials may be discovered..-

    Private Function mbGetSerialInfo(ByVal intInputStock_id As Integer, _
                                       ByVal sSerialNo As String, _
                                           ByRef colSerialInfo As Collection) As Boolean
        Dim sSql As String
        Dim sInStock As String = "NO"
        '== Dim sItemBarcode As String
        '== Dim colSerialInfo As Collection
        '== Dim col1 As Collection
        '== Dim colItemFields As Collection
        Dim dtSerial, dtGoods As DataTable
        Dim dtRow1, dtRowGoods As DataRow
        Dim intGoodsId As Integer = -1
        Dim intGoodsLineId As Integer = -1
        Dim intSupplierId As Integer
        Dim sSupplierCode As String

        mbGetSerialInfo = False
        sSql = " SELECT SA.SerialNumber AS SerialNo, SA.Serial_Id, "
        sSql &= "  SA.stock_id AS stock_id, SAT.tran_type, isInStock, "
        sSql &= "  SAT.Type_id AS Goods_Id, "
        sSql &= "  SAT.Type_Line_id AS GoodsLine_Id "
        sSql &= "  FROM SerialAudit AS SA"
        sSql &= "    LEFT OUTER JOIN SerialAuditTrail AS SAT "
        sSql &= "     ON ((SAT.SerialAudit_Id=SA.Serial_Id) AND " & _
                                    "((SAT.tran_type LIKE 'GoodsRec%') OR (SAT.tran_type ='GR')) )"
        sSql &= "   WHERE (SA.SerialNumber='" & sSerialNo & "') AND ( SA.stock_id=" & CStr(intInputStock_id) & ");"

        '=3101.1024=  may be multiple records returned.-
        If Not gbGetDataTable(mCnnSql, dtSerial, sSql) Then
            MsgBox("Failed to get SerialAudit dataTable for Serial: " & sSerialNo & vbCrLf & _
                                     gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '-- get supplier and Goods Invoice info--
        If (dtSerial Is Nothing) OrElse (dtSerial.Rows.Count <= 0) Then
            '- no data-
            labStatus.Text = "No SerialAudit data available for Serial: " & sSerialNo & vbCrLf & _
                                      gsGetLastSqlErrorMessage()
            '== MsgBox("No SerialAudit data available for Serial: " & sSerialNo & vbCrLf & _
            '==                           gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
            Exit Function
        End If '-nothing-
        '-- get supplier and Goods Invoice info--
        dtRow1 = dtSerial.Rows(0)  '-first and only row.-
        If IsDBNull(dtRow1.Item("goods_id")) Then
            labStatus.Text = "No Goods Invoice data available for Serial: " & sSerialNo
            Exit Function
        End If
        intGoodsId = dtRow1.Item("goods_id")
        intGoodsLineId = dtRow1.Item("goodsLine_id")

        '--Lookup Goodsline/Stock to get Line Qty and Supplier_Id, SupplierCode, info..--
        sSql = " SELECT GRL.Goods_Id, GR.supplier_id AS supplier_id, supplierName, "
        sSql = sSql & "   invoice_no,  invoice_date,  "
        sSql = sSql & "   cost_ex,  GRL.quantity AS Qty  "
        sSql = sSql & " FROM (GoodsReceivedLine AS GRL "
        sSql = sSql & "  LEFT JOIN (GoodsReceived AS GR "
        sSql = sSql & "    LEFT JOIN Supplier "
        sSql = sSql & "    ON (Supplier.Supplier_id=GR.supplier_id) )"
        sSql = sSql & "  ON  (GR.Goods_Id=GRL.Goods_Id) )"
        sSql = sSql & "  WHERE (GRL.Goods_Id=" & intGoodsId & ")  "
        sSql = sSql & "        AND (GRL.Line_Id=" & intGoodsLineId & ") " '== AND (supplierCode.stock_id=" & lngStockId & ")  
        If Not gbGetDataTable(mCnnSql, dtGoods, sSql) Then
            MsgBox("Failed to get GoodsReceived dataTable for Serial: " & sSerialNo & vbCrLf & _
                                     gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        If (dtGoods Is Nothing) OrElse (dtGoods.Rows.Count <= 0) Then
            '- no data-
            MsgBox("No GoodsReceived data available for Serial: " & sSerialNo & vbCrLf & _
                                      gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
            Exit Function
        End If '-nothing-
        '-- RETURN the supplier and Goods Invoice info--
        dtRowGoods = dtGoods.Rows(0)  '-first and only row.-
        colSerialInfo = New Collection
        colSerialInfo.Add(sSerialNo, "serialno")
        colSerialInfo.Add(intInputStock_id, "stock_id")
        colSerialInfo.Add(intGoodsId, "goods_id")
        colSerialInfo.Add(intGoodsLineId, "goodsLine_id")
        colSerialInfo.Add(dtRowGoods.Item("supplier_id"), "supplier_id")
        colSerialInfo.Add(dtRowGoods.Item("supplierName"), "supplierName")
        colSerialInfo.Add(dtRowGoods.Item("invoice_no"), "invoice_no")
        colSerialInfo.Add(dtRowGoods.Item("invoice_date"), "invoice_date")
        colSerialInfo.Add(dtRowGoods.Item("cost_ex"), "cost_ex")
        colSerialInfo.Add(dtRowGoods.Item("qty"), "qty")

        labStatus.Text = ""
        mbGetSerialInfo = True
    End Function '-get info-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--show/print invoice-
    Private Function mbShowInvoice(ByVal intInvoice_Id As Integer) As Boolean
        Dim frmShowInvoice1 As frmShowInvoice

        frmShowInvoice1 = New frmShowInvoice
        frmShowInvoice1.connectionSql = mCnnSql
        frmShowInvoice1.InvoiceNo = intInvoice_Id

        '== ? ==  frmShowInvoice1.ColourPrinterName = msColourPrtName
        '== ? ==  frmShowInvoice1.ReceiptPrinterName = msReceiptPrtName
        '== ? ==  frmShowInvoice1.LabelPrinterName = msLabelPrtName
        '== ? ==  frmShowInvoice1.Settings = mSdSettings
        '== ? ==  '== frmShowInvoice1.SystemInfo = mSdSystemInfo
        '== ? ==  frmShowInvoice1.UserLogo = mImageUserLogo
        '== ? ==  frmShowInvoice1.versionPOS = msVersionPOS

        frmShowInvoice1.ShowDialog()

    End Function  '--show invoice..-
    '= = = = = =  = = = = = = ==  =
    '-===FF->

    '-- get trail records and load Trail Grid..-

    Private Function mbShowSerialTrail(ByVal intSerialAudit_id As Integer, _
                                       ByRef intCount As Integer, _
                                       ByRef sLastTrans As String, _
                                       ByRef intInvoice_id As Integer, _
                                       ByRef sErrorMsg As String) As Boolean
        Dim sSql, s1, sMsg As String
        Dim dtTrail As DataTable

        dgvTrail.Rows.Clear()
        intCount = 0
        mbShowSerialTrail = False
        '-- get SerialAudit Trail.  and populate grid. 
        sSql = " SELECT * FROM SerialAuditTrail AS SAT "
        sSql &= "    WHERE (SAT.SerialAudit_Id=" & intSerialAudit_id & ") "
        sSql &= "  ORDER BY trail_date; "
        If gbGetDataTable(mCnnSql, dtTrail, sSql) Then
            If (Not (dtTrail Is Nothing)) AndAlso (dtTrail.Rows.Count > 0) Then
                '--populate grid..-
                Dim gridrow1 As DataGridViewRow
                Dim rx As Integer = 0
                For Each drTrail As DataRow In dtTrail.Rows
                    gridrow1 = New DataGridViewRow
                    dgvTrail.Rows.Add(gridrow1)
                    dgvTrail.Rows(rx).Cells(0).Value = Format(drTrail.Item("trail_date"), "dd-MMM-yyyy")
                    dgvTrail.Rows(rx).Cells(1).Value = drTrail.Item("tran_type")
                    sLastTrans = drTrail.Item("tran_type")  '-save latest trans. type. 
                    dgvTrail.Rows(rx).Cells(2).Value = drTrail.Item("type_id")
                    dgvTrail.Rows(rx).Cells(3).Value = drTrail.Item("RM_tr_detail")
                    intInvoice_id = drTrail.Item("type_id")
                    rx += 1
                Next drTrail
                intCount = dtTrail.Rows.Count
            Else  '-no data-  
            End If '-nothing.
            mbShowSerialTrail = True
        Else '--Trail error- 
            sErrorMsg = gsGetLastSqlErrorMessage()  '--for caller-
            MsgBox("Error in getting SerialAudit Trail.. " & vbCrLf & _
                                              gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
        End If  '-Trail get- 

    End Function  '-mbGetSerialTrail-
    '= = = = = =  = = = = = = ==  =

    '-mbShowSerialInfo-

    Private Function mbShowSerialInfo(ByVal intGridRowIndex As Integer) As Boolean
        Dim intSerial_id, intCount, intInvoice_id As Integer
        Dim intStock_id As Integer
        Dim sErrorMsg, sLastTrans As String
        Dim colSerialInfo As Collection

        txtSerialGoodsInfo.Text = ""  '=3301.0102  02Jan2017-
        If (intGridRowIndex >= 0) Then  '== (dgvSerials.SelectedRows.Count > 0) And (intGridRowIndex >= 0) Then
            With dgvSerials.Rows(intGridRowIndex)
                intSerial_id = CInt(.Cells(DGV_SERIALS_SERIALAUDIT_ID).Value)
                intStock_id = CInt(.Cells(DGV_SERIALS_STOCK_ID).Value)
                txtSerialNo.Text = Trim(.Cells(DGV_SERIALS_NUMBER).Value)
                txtBarcode.Text = .Cells(DGV_SERIALS_BARCODE).Value
                labDescription.Text = .Cells(DGV_SERIALS_DESCRIPTION).Value
                labSerial_id.Text = CStr(intSerial_id)
                labSerialStatus.Text = .Cells(DGV_SERIALS_STATUS).Value

                If mbGetSerialInfo(intStock_id, txtSerialNo.Text, colSerialInfo) Then
                    txtSerialGoodsInfo.Text = "Date: " & Format(colSerialInfo.Item("invoice_date"), "dd-MMM-yyyy") & vbCrLf
                    txtSerialGoodsInfo.Text &= "Invoice No: " & colSerialInfo.Item("invoice_no") & vbCrLf
                    txtSerialGoodsInfo.Text &= "Supplier: " & colSerialInfo.Item("supplierName") & vbCrLf
                    txtSerialGoodsInfo.Text &= "Cost_ex: " & FormatCurrency(colSerialInfo.Item("cost_ex"), 2) & vbCrLf
                    txtSerialGoodsInfo.Text &= "Quantity: " & CStr(colSerialInfo.Item("qty"))
                End If
                '-- show history trail.
                If Not mbShowSerialTrail(intSerial_id, intCount, sLastTrans, intInvoice_id, sErrorMsg) Then
                    MsgBox("Error in Loading Trail history..", MsgBoxStyle.Information)
                End If
            End With
        End If
    End Function  '- mbShowSerialInfo-
    '= = = = = = = = = = = = = =  = = 
    '-===FF->

    '--  NEW VERSION --

    '--=3103.218= 
    '--Get list of Serial Audit Table..-
    '--  Cloned from RM clsRetailHost--

    Private Function mbRefreshSerials() As Boolean
        Dim intSerialCount As Integer
        Dim sSqlSelect, sSearchArg, sSearchSql As String
        '==Dim colSerialsList As Collection
        Dim s1, sWhere, strErrorReport, sMsg As String
        Dim asColumns As Object
        Dim dtSerials As DataTable

        mbRefreshSerials = False
        strErrorReport = ""
        '= mbSerialCancel = False
        sSearchArg = Trim(txtSerialArg.Text)
        On Error GoTo RefreshSerials_Error

        '-- make search ard Sql.--
        sSearchSql = ""
        '--  make srch arg.. sql..-
        asColumns = New Object() {"serialNumber", "barcode", "cat1", "description"}
        sSearchSql = gbMakeTextSearchSql(sSearchArg, asColumns)

        labStatus.Text = "Getting Serial-Audit list.."
        System.Windows.Forms.Application.DoEvents()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '--
        '= Changed= 3501.1101--
        sSqlSelect = "SELECT serialNumber, barcode, description, status, cat1, cat2,   "
        sSqlSelect &= "    serial_id, SA.stock_id "
        sSqlSelect &= " FROM dbo.serialAudit AS SA  "
        sSqlSelect &= "  JOIN stock ON (SA.stock_id= stock.stock_id) "
        '== sSql &= " WHERE (serialNumber='" & msSerialNumber & "') "
        sWhere = ""
        If sSearchSql <> "" Then
            sWhere &= " " & sSearchSql
        End If
        If chkInstockOnly.Checked Then  '-add instock only-
            s1 = " (status='instock' )"
            If (sWhere <> "") Then '-have where
                sWhere &= " AND " & s1
            Else  '-no where yet-
                sWhere &= " " & s1
            End If
        End If
        '= sSql = sSql & " ORDER BY serialNumber;"

        '==TEST=
        '== sWhere = ""

        '==3311.510= Now using clsBrowse32-
        Call mbInitialiseBrowse(mBrowseSerials, sSqlSelect, sWhere, _
                            dgvSerials, labResultsCount, LabResultsFind, txtResultsFind)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        labStatus.BackColor = Color.LightGoldenrodYellow
        Exit Function

RefreshSerials_Error:
        Dim lngError As Integer = Err.Number
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox("Runtime Error in  'mbRefreshSerials' function.." & vbCrLf & _
                "Error: " & lngError & ":  " & ErrorToString(lngError) & vbCrLf & _
                    "Serial lookup is/may be incomplete..", MsgBoxStyle.Exclamation)
        '== colSerialsList = Nothing
    End Function '-refresh serial list..-
    '= = = = = = = = = = = =
    '-===FF->

    '--No barcode.. Lookup serialNo and get user to choose..
    '--  can be Sale or Refund..--

    'Private Function mbFindSerial(ByRef intSerialAudit_id As Integer, _
    '                                  ByRef intStock_id As Integer, _
    '                                   ByRef sBarcode As String, _
    '                                   ByRef bIsInStock As Boolean, _
    '                                    ByRef sError As String) As Boolean
    '    Dim sSql, s1, sMsg, sErrorMsg As String
    '    Dim dataTable1 As DataTable
    '    '==Dim bIsInStock As Boolean = False
    '    Dim ix, intInvoice_id, intCount As Integer
    '    Dim frmListSelect1 As frmListSelect
    '    Dim sLastTrans As String = ""
    '    Dim colSerialInfo As Collection

    '    mbFindSerial = False
    '    sError = ""
    '    intInvoice_id = -1
    '    btnOK.Enabled = False

    '    '-- Lookup SerialAudit -
    '    sSql = "SELECT  stock.barcode, stock.cat1, stock.description, "
    '    sSql &= "     SA.isInStock, SA.serial_id, SA.stock_id, SA.status "
    '    sSql &= " FROM [serialAudit] AS SA JOIN stock ON (SA.stock_id= stock.stock_id)"
    '    sSql &= " WHERE (serialNumber='" & msSerialNumber & "') "
    '    If (mIntInputStock_id > 0) Then
    '        sSql &= " AND (SA.stock_id = " & CStr(mIntInputStock_id) & ");"
    '    Else '-no stock id supplied.
    '        sSql &= "; "
    '    End If
    '    If gbGetDataTable(mCnnSql, dataTable1, sSql) Then
    '        If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
    '            Dim dataRow1 As DataRow
    '            If (dataTable1.Rows.Count = 1) Then
    '                '--found one.. set it up..-
    '                dataRow1 = dataTable1.Rows(0)
    '                labStatus.Text = vbCrLf & "Found one Item.. "
    '                btnLookup.Enabled = False
    '                '-IF This is a sale.. must be InStock..
    '                '== If mbIsRefund Then
    '                '==   '--refund- Show the sale info-
    '                '==   '--refund- Show the sale Invoice info-
    '                '==   intInvoice_id = dataRow1.Item("invoice_id")
    '                '==   Call mbShowInvoice(intInvoice_id)
    '                '== End If
    '            Else '--choose from many.-
    '                frmListSelect1 = New frmListSelect
    '                frmListSelect1.inData = dataTable1
    '                frmListSelect1.hdrText = "Stock with Serial No: " & msSerialNumber
    '                frmListSelect1.Text = "Stock with Serial No: " & msSerialNumber
    '                '== If mbIsRefund Then
    '                '== frmListSelect1.hdrText = "Sales of Serial No: " & msSerialNumber
    '                '== Else
    '                '== End If
    '                frmListSelect1.ShowDialog()
    '                If frmListSelect1.cancelled Then
    '                    mbCancelled = True  '== sError = "selection canceled."
    '                    Exit Function
    '                End If
    '                ix = frmListSelect1.selectedRow
    '                '- send back barcode..
    '                dataRow1 = dataTable1.Rows(ix)
    '                '== intSerialAudit_id = dataRow1.Item("serial_id")
    '                '== intStock_id = dataRow1.Item("stock_id")
    '                '== sBarcode = dataRow1.Item("barcode")
    '                '== txtBarcode.Text = dataRow1.Item("barcode")
    '                '== labDescription.Text = dataRow1.Item("description")
    '                labStatus.Text = vbCrLf & "Item selected.. "
    '                btnLookup.Enabled = True   '--can lookup again.-
    '                '== labSerial_id.Text = CStr(intSerialAudit_id)

    '                '=== If mbIsRefund Then
    '                '=== '--refund- Show the sale Invoice info-
    '                '=== intInvoice_id = dataRow1.Item("invoice_id")
    '                '=== Call mbShowInvoice(intInvoice_id)
    '                '=== End If
    '            End If  '--count-
    '            '-- ok- show selected serial info.. 
    '            intSerialAudit_id = dataRow1.Item("serial_id")
    '            intStock_id = dataRow1.Item("stock_id")
    '            sBarcode = dataRow1.Item("barcode")
    '            txtBarcode.Text = dataRow1.Item("barcode")
    '            labDescription.Text = dataRow1.Item("description")
    '            labSerial_id.Text = CStr(intSerialAudit_id)
    '            labSerialStatus.Text = dataRow1.Item("status")
    '            '-- status can be instock, returnedGoods, sold.  '===3301.526=
    '            bIsInStock = LCase(dataRow1.Item("status")) = "instock"

    '            '-- add goods info..-
    '            If mbGetSerialInfo(intStock_id, txtSerialNo.Text, colSerialInfo) Then
    '                txtSerialGoodsInfo.Text = "Date: " & Format(colSerialInfo.Item("invoice_date"), "dd-MMM-yyyy") & vbCrLf
    '                txtSerialGoodsInfo.Text &= "Invoice No: " & colSerialInfo.Item("invoice_no") & vbCrLf
    '                txtSerialGoodsInfo.Text &= "Supplier: " & colSerialInfo.Item("supplierName") & vbCrLf
    '                txtSerialGoodsInfo.Text &= "Cost_ex: " & FormatCurrency(colSerialInfo.Item("cost_ex"), 2) & vbCrLf
    '                txtSerialGoodsInfo.Text &= "Quantity: " & CStr(colSerialInfo.Item("qty"))
    '            End If  '--goods.-

    '            If mbShowSerialTrail(intSerialAudit_id, intCount, sLastTrans, intInvoice_id, sErrorMsg) Then
    '                If (intCount > 0) Then '- got some trail rows.-

    '                    '-IF This is a sale.. must be InStock.. 
    '                    '-IF This is a REFUND..there must be a SALE trail.. 
    '                    If mbIsRefund Then
    '                        btnOK.Enabled = True
    '                        mbFindSerial = True
    '                        If (LCase(sLastTrans) <> "sale") Then
    '                            labStatus.Text = "No SALE record found for this Serial!" & vbCrLf & _
    '                                                  "Press ok to continue anyway."
    '                            mIntSalesInvoiceNo = -1
    '                        Else
    '                            mIntSalesInvoiceNo = intInvoice_id
    '                            labStatus.Text = "Press ok to continue with thia item."
    '                        End If  '-sale-
    '                    Else '-not refund.  
    '                        If Not bIsInStock Then
    '                            labStatus.Text = "This Serial is marked as not being in stock!"
    '                        Else  '-ok-
    '                            btnOK.Enabled = True
    '                            labStatus.Text = "Press ok to continue with this item."
    '                            mbFindSerial = True
    '                        End If  '-instock= 
    '                    End If  '--refund- - 

    '                Else  '-no trail rows..
    '                    sError = "No Trail record on file for serial no.: '" & msSerialNumber & "'.. "
    '                    Exit Function

    '                End If  '-count-
    '            Else  '-get failed-
    '                sError = "Error in getting SerialAudit Trail.. " & vbCrLf & _
    '                                                 sErrorMsg & vbCrLf
    '                Exit Function
    '            End If '-get trail failed
    '            '--have saved chosen barcode for caller.-
    '            mbFindSerial = True
    '        Else  '-no data- 
    '            sError = "No record of serial no.: '" & msSerialNumber & "' on file ! "
    '        End If
    '    Else '--error-
    '        sError = "Error in getting recordset for SerialAudit table. " & vbCrLf & _
    '                                       gsGetLastSqlErrorMessage() & vbCrLf
    '        Exit Function
    '    End If '--get table-

    'End Function  '--find serial-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- lookup the serial requested..

    '-- l o a d --

    Private Sub frmChildFindSerial_Load(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) Handles MyBase.Load

        mIntFormDesignWidth = Me.Width  '--save starting dim.
        mIntFormDesignHeight = Me.Height  '--save starting dim.

        btnLookup.Enabled = False
        btnShow.Enabled = False
        txtBarcode.Text = ""
        labDescription.Text = ""
        labSerial_id.Text = ""
        labSerialStatus.Text = ""

        frameSerialAudit.Text = ""

        txtSerialGoodsInfo.Text = ""
        LabResultsFind.Text = ""
        txtResultsFind.Text = ""
        '==3301.525--
        '--  Don't show form if we already know stock_id..
        '-- WON"T be used.. 

        '=4201.0422- Child SerialLookup  - Find Serial not needed..
        '=4201.0422- Child SerialLookup  - Find Serial not needed..

        'If (msSerialNumber <> "") And (mIntInputStock_id > 0) Then  '--called from Sale..-
        '    '-- call mbFindSerial  --
        '    '=MsgBox("TEST- frmFindSerial_Load..  Looking for serial: " & vbCrLf & _
        '    '=           "'" & msSerialNumber & "';  Stock_id: " & mIntInputStock_id, MsgBoxStyle.Information)
        '    If Not mbFindSerial(mIntSerial_id, mIntStock_id, msBarcode, mbIsInStock, msErrorText) Then
        '        MsgBox(msErrorText, MsgBoxStyle.Exclamation)
        '        mbCancelled = True
        '        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        '        '== labStatus.Text = "Press Cancel to abort.. "
        '        '== btnOK.Enabled = False
        '    Else '--found-                                                   
        '        '= labStatus.Text = "Press OK to continue, or Cancel to abort.. "
        '    End If
        '    Me.Hide()  '--don't show form..
        '    Exit Sub
        'End If

        '==Call CenterForm(Me)
        '=4201.0424=--  All stuff from SHOWN..
        '=4201.0424=--  All stuff from SHOWN..
        '=4201.0424=--  All stuff from SHOWN..

        If (msSerialNumber = "") Then  '--called from user enquiry button..-
            '-- browsing/searching serials..
            frameSerialAudit.Enabled = True
            txtSerialNo.ReadOnly = True '= False
            mbIsInitialising = False
            labSerialSearch.Enabled = True
            labVerify.Enabled = False

            '--now wait for serial entry and Lookup button..-
            '== dgvSerials.Columns.Clear()   '--get rid of setup cols..-
            Call mbRefreshSerials()
        Else  '--called from sales or Goods..
            '-- verifying serial..
            '=4201.0422- Child SerialLookup  - Find Serial not needed..
            '=4201.0422- Child SerialLookup  - Find Serial not needed..

            'frameSerialAudit.Enabled = False
            'labSerialSearch.Enabled = False
            'labVerify.Enabled = True

            'txtSerialNo.ReadOnly = True
            ''= mbIsInitialising = False
            'labStatus.Text = vbCrLf & "Searching.. "

            ''-- call mbFindSerial  --
            'If Not mbFindSerial(mIntSerial_id, mIntStock_id, msBarcode, mbIsInStock, msErrorText) Then
            '    MsgBox(msErrorText, MsgBoxStyle.Exclamation)
            '    mbCancelled = True
            '    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            '    Me.Hide()
            '    Exit Sub
            '    '== labStatus.Text = "Press Cancel to abort.. "
            '    '== btnOK.Enabled = False
            'Else '--found-
            '    '= labStatus.Text = "Press OK to continue, or Cancel to abort.. "
            'End If

            'If mbCancelled Then
            '    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            '    Me.Hide()
            '    Exit Sub
            'End If
        End If  '-serial no.
        mbIsInitialising = False

    End Sub  '--load-
    '= = = = = = = = = = =

    '- Activated -
    'Private Sub frmChildFindSerial_Activated(ByVal sender As System.Object, _
    '                              ByVal e As System.EventArgs) Handles MyBase.Activated

    '    If mbActivated Then Exit Sub
    '    mbActivated = True

    'End Sub  '- Activated -
    '= = = = = = = = = = =  == = =

    '-- S h o w n.-

    'Private Sub frmChildFindSerial_Shown(ByVal sender As System.Object, _
    '                                  ByVal e As System.EventArgs) Handles MyBase.Shown

    '= If mbActivated Then Exit Sub
    '= mbActivated = True

    'If (msSerialNumber = "") Then  '--called from user enquiry button..-
    '    '-- browsing/searching serials..
    '    frameSerialAudit.Enabled = True
    '    txtSerialNo.ReadOnly = True '= False
    '    mbIsInitialising = False
    '    labSerialSearch.Enabled = True
    '    labVerify.Enabled = False

    '    '--now wait for serial entry and Lookup button..-
    '    '== dgvSerials.Columns.Clear()   '--get rid of setup cols..-
    '    Call mbRefreshSerials()
    'Else  '--called from sales or Goods..
    '    '-- verifying serial..
    '    '=4201.0422- Child SerialLookup  - Find Serial not needed..
    '    '=4201.0422- Child SerialLookup  - Find Serial not needed..

    '    'frameSerialAudit.Enabled = False
    '    'labSerialSearch.Enabled = False
    '    'labVerify.Enabled = True

    '    'txtSerialNo.ReadOnly = True
    '    ''= mbIsInitialising = False
    '    'labStatus.Text = vbCrLf & "Searching.. "

    '    ''-- call mbFindSerial  --
    '    'If Not mbFindSerial(mIntSerial_id, mIntStock_id, msBarcode, mbIsInStock, msErrorText) Then
    '    '    MsgBox(msErrorText, MsgBoxStyle.Exclamation)
    '    '    mbCancelled = True
    '    '    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    '    '    Me.Hide()
    '    '    Exit Sub
    '    '    '== labStatus.Text = "Press Cancel to abort.. "
    '    '    '== btnOK.Enabled = False
    '    'Else '--found-
    '    '    '= labStatus.Text = "Press OK to continue, or Cancel to abort.. "
    '    'End If

    '    'If mbCancelled Then
    '    '    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    '    '    Me.Hide()
    '    '    Exit Sub
    '    'End If
    'End If  '-serial no.
    'mbIsInitialising = False

    '= End Sub  '--Activated-
    '= = = = = = = = = = = 
    '-===FF->

    '-- Resized Form..

    Private Sub frmChildSerialLookup_Resize(ByVal eventSender As System.Object, _
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

        '= panelHdr.Width = ((Me.Width - 30) \ 2) - 24
        '= panelHdr.Height = Me.Height - 140
        panelHdr.Left = Me.Width - panelHdr.Width - 24
        panelHdr.Height = Me.Height - panelHdr.Top - 18

        labVerify.Left = panelHdr.Left
        labStatus.Left = panelHdr.Left

        '= dgvTrail.Width = panelHdr.Width - 26

        '= btnCancel.Top = Me.Height - 84  '=54
        '= btnOK.Top = btnCancel.Top
        btnOK.Left = Me.Width - btnOK.Width - 30
        btnCancel.Left = btnOK.Left '= - btnCancel.Width - 30

        frameSerialAudit.Width = ((Me.Width - panelHdr.Width - 40))
        frameSerialAudit.Height = Me.Height - frameSerialAudit.Top - 20

        dgvSerials.Width = frameSerialAudit.Width - 11
        dgvSerials.Height = frameSerialAudit.Height - dgvSerials.Top - 12

    End Sub  '-resize-
    '= = = = = = = = = = = 
    '-===FF->

    '--Serial Audit Search..--

    Private Sub cmdSerialClear_Click(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles cmdSerialClear.Click
        txtSerialArg.Text = ""
    End Sub '--clear..==
    '= = = = = = = = = = =

    Private Sub cmdSerialSrch_Click(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles cmdSerialSrch.Click
        If mbIsInitialising Then Exit Sub

        cmdSerialSrch.Enabled = False
        Call mbRefreshSerials()
        cmdSerialSrch.Enabled = True
    End Sub '--srch..-
    '= = = = = = = = = = = = = = = = =

    '-chkInstockOnly_CheckedChanged-

    Private Sub chkInstockOnly_CheckedChanged(sender As Object, e As EventArgs) _
                                                Handles chkInstockOnly.CheckedChanged
        If mbIsInitialising Then Exit Sub
        cmdSerialSrch.Enabled = False
        Call mbRefreshSerials()
        cmdSerialSrch.Enabled = True
    End Sub  '-chkInstockOnly_CheckedChanged-
    '= = = = = = = = = = = = = = = = = = = = ==

    Private Sub txtSerialArg_KeyPress(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                         Handles txtSerialArg.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If mbIsInitialising Then Exit Sub
        If keyAscii = 13 Then '--enter--
            If Trim(txtSerialArg.Text) <> "" Then
                Call mbRefreshSerials()
            End If
            keyAscii = 0 '--processed..-
        End If '--enter-

        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--enter..-
    '= = = = = = = = =
    '-===FF->

    '- row enter-.-

    Private Sub dgvSerials_RowEnter(ByVal sender As Object, _
                            ByVal ev As DataGridViewCellEventArgs) _
                                     Handles dgvSerials.RowEnter
        Dim intRowIndex = ev.RowIndex
        If (intRowIndex >= 0) Then  '- case grid is loading.-
            mIntSelectedRowResults = intRowIndex
            Call mbShowSerialInfo(intRowIndex)
        End If

        'Dim i As Integer
        'For i = 0 To dgvSerials.Rows(ev.RowIndex).Cells.Count - 1
        '    dgvSerials(i, ev.RowIndex).Style.BackColor = Color.Yellow
        'Next i

    End Sub  '- - row enter-
    '= = = = = = = = = = =


    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvSerials_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvSerials.Sorted
        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvSerials.SortedColumn
        sName = currentColumn.HeaderText
        '= MsgBox("Calling sortColumn for " & sName)
        Call mBrowseSerials.SortColumn(sName)
    End Sub  '--sorted-
    '= = = = = = = = =  = = =

    '-- serial grid- Selection changed.-

    'Private Sub dgvSerials_SelectionChanged(ByVal sender As Object, _
    '                               ByVal e As EventArgs) Handles dgvSerials.SelectionChanged
    '    '--  get selected row and show serial info and history...
    '    '-- DGV_SERIALS_SERIALAUDIT_ID-
    '    If mbIsInitialising Then Exit Sub
    '    If dgvSerials.CurrentCell Is Nothing Then
    '        MsgBox("Testing Selection Changed-- Current cell is Nothing.", MsgBoxStyle.Information)
    '        Exit Sub
    '    End If
    '    '- If (dgvSerials.SelectedRows.Count > 0) Then
    '    Dim intRowIndex = dgvSerials.CurrentCell.RowIndex
    '    If (intRowIndex >= 0) Then  '- case grid is loading.-
    '        mIntSelectedRowResults = intRowIndex
    '        Call mbShowSerialInfo(intRowIndex)
    '    End If
    '    '- End If '-count-
    'End Sub  '--Selection changed-
    '= = = =  = = = = = = = = = == = =
    '-===FF->

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtResultsFind_Enter(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles txtResultsFind.Enter
        LabResultsFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        LabResultsFind.Font = VB6.FontChangeBold(LabResultsFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = = = = = = = = 

    Private Sub txtResultsFind_Leave(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtResultsFind.Leave
        LabResultsFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabResultsFind.Font = VB6.FontChangeBold(LabResultsFind.Font, False)

    End Sub '--Leave focus--
    '= = = = = = = = = = = = = = =  =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtResultsFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtResultsFind.TextChanged

        If mbIsInitialising Then Exit Sub
        '-- go Find..
        Call mBrowseSerials.Find(txtResultsFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = = = = = =
    '-===FF->


    '-SerialNo_TextChanged-

    'Private Sub txtSerialNo_TextChanged(ByVal sender As System.Object, _
    '                                    ByVal e As System.EventArgs) Handles txtSerialNo.TextChanged

    '    If mbIsInitialising Then Exit Sub

    '    msSerialNumber = Trim(txtSerialNo.Text)

    '    '== btnLookup.Enabled = True

    'End Sub  '--SerialNo_TextChanged-
    '= = = = = = = = = = = = = = = =

    '- lookup after user input serialNo..-

    Private Sub btnLookup_Click(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles btnLookup.Click
        '-- call mbFindSerial  --
        '=4201.0422- Child SerialLookup  - Find Serial not needed..
        '=4201.0422- Child SerialLookup  - Find Serial not needed..
        'If Not mbFindSerial(mIntSerial_id, mIntStock_id, msBarcode, mbIsInStock, msErrorText) Then
        '    MsgBox(msErrorText)
        '    '= Me.Hide()
        'End If

    End Sub  '--lookup--
    '= = = = = = = = = = = = =
    '-===FF->

    '-grid selection.

    Private Sub dgvTrail_SelectionChanged(ByVal sender As System.Object, _
                                           ByVal ev As System.EventArgs) Handles dgvTrail.SelectionChanged

        If mbIsInitialising Then Exit Sub

        mIntCurrentTrailRow = dgvTrail.CurrentRow.Index
        If (mIntCurrentTrailRow >= 0) Then  '-have row.-
            Dim gridRow1 As DataGridViewRow = dgvTrail.Rows(mIntCurrentTrailRow)
            If (LCase(gridRow1.Cells(1).Value) = "sale") Or _
                               (LCase(gridRow1.Cells(1).Value) = "refund") Then
                If IsNumeric(gridRow1.Cells(2).Value) Then
                    mIntCurrentInvoiceNo = CInt(gridRow1.Cells(2).Value)  '--"type_id"-
                End If
                btnShow.Enabled = True
            Else
                btnShow.Enabled = False
            End If
        End If  '-have row-.

    End Sub  '--SelectionChanged-
    '= = = = = = = = = == ==  

    '-- show invoice.-

    Private Sub btnShow_Click(ByVal sender As System.Object, _
                            ByVal e As System.EventArgs) Handles btnShow.Click
        If mIntCurrentInvoiceNo > 0 Then
            Call mbShowInvoice(mIntCurrentInvoiceNo)
        End If
    End Sub  '-- show invoice.-
    '= = = = = =  = = = = = ==  =

    '-ok-

    Private Sub btnOK_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) Handles btnOK.Click

        'Me.DialogResult = System.Windows.Forms.DialogResult.OK
        ''= Me.Hide()
        'Me.Close()
        Call close_me()
    End Sub '-ok-
    '= = = = = = = = = = =

    '-- cancel-

    Private Sub btnCancel_Click(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles btnCancel.Click
        mbCancelled = True
        'Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        ''= Me.Hide()
        'Me.Close()
        Call close_me()

    End Sub  '-cancel-
    '= = = = = = = = = = = 

    'Private Sub frmChildFindSerial_DeActivate(ByVal sender As System.Object, _
    '                                 ByVal e As System.EventArgs) Handles MyBase.Deactivate

    '    '== If mbCancelled Then MsgBox("Deactivated.. and Cancelled..", MsgBoxStyle.Information)

    'End Sub '-decactivate-
    '= = = = = = = = = = = 

    Private Sub close_me()

        '- inform parent.-
        '- Report to Parent..-

        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

        '- Create an instance of the delegate.  
        '= Dim msd As subReportDelegate = AddressOf c1.Sub1
        ' Call the method.  
        '= msd.Invoke(Me.Name, "FormClosing")
        '= if (this.InvokeDel != null)
        '= InvokeDel.Invoke(this.txtMsg.Text);
        If Not (Me.delReport Is Nothing) Then
            delReport.Invoke(Me.Name, "FormClosed", "")
        End If

        Me.Dispose()

    End Sub '--close me-


    '-FormClosed-

    'Private Sub frmChilderialLookup_FormClosed(ByVal eventSender As System.Object, _
    '                                     ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) _
    '                                        Handles Me.FormClosed
    '    If Not (Me.delReport Is Nothing) Then
    '        delReport.Invoke(Me.Name, "FormClosed", "")
    '    End If

    'End Sub  '-FormClosed-
    '= = = = = = = = = ==  = 


End Class  '- frmSerialLookup--
'= = = = = = = = = = = = = = = 

'== end form ==