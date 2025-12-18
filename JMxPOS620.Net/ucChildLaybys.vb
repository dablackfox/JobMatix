Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'= Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports system.data.sqlclient
Imports System.Data.OleDb

Public Class ucChildLaybys


    '== 
    '==     v3.4.3403.0503 = 03May2017=
    '==      -- Form to show all Laybys..
    '==
    '==
    '==   Updated.- 3519.0219  Started 18-Feb-2019= 
    '==     -- Fixes to Laybys.. 
    '==         - clsPos34Sale- Setup Cust. Allow to choose sale/layby after not delivering Layby..
    '==         - Add code to actually Cancel Layby if requested...
    '==
    '==   Updated.- 3519.0414  Started 14-April-2019= 
    '==     -- Fixes to Laybys for Discounts... 
    '==
    '== - NEW VERSION--
    '==    -- 4201.0528.  Make Layby's  into Child User Control. 
    '==               Add TabControlMain, and a tab to show ALL stock items under Layby..
    '==    -- 4201.0622.  Re-sizing.. 
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    '- Grid Columns
    Private Const k_GRIDCOL_CUST_NAME As Short = 0
    Private Const k_GRIDCOL_CUST_CREDIT As Short = 1
    Private Const k_GRIDCOL_LAYBY_ID As Short = 2
    Private Const k_GRIDCOL_DATE_STARTED As Short = 3
    Private Const k_GRIDCOL_IS_DELIVERED As Short = 4
    Private Const k_GRIDCOL_TOTAL_AMT As Short = 5
    Private Const k_GRIDCOL_CUST_ID As Short = 6
    Private Const k_GRIDCOL_DATE_DELIVERED As Short = 7
    Private Const k_GRIDCOL_COL_ITEMS As Short = 8
    '-laybyDiscount-
    Private Const k_GRIDCOL_DISCOUNT As Short = 9
    '= = = = =

    Private mbIsInitialising As Boolean = True
    Private mbGridRefreshing As Boolean = True

    Private mFrmParent As Form
    Private mbActivated As Boolean = False   '-to activate once only.-

    '--inputs--

    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    'Private msServer As String = ""
    'Private msSqlServerComputer As String = ""
    'Private msSqlServerInstance As String = ""
    'Private msSqlVersion As String = ""
    'Private mlngSqlMajorVersion As Integer = 0

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private mbIsSqlAdmin As Boolean = False

    Private msVersionPOS As String = ""

    Private msComputerName As String '--local machine--
    '== Private msAppPath As String
    '= Private msLastSqlErrorMessage As String = ""
    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    '-input-
    Private mIntSaleCustomer_id As Integer = -1
    Private mbSelectForDelivery As Boolean = False

    Private mColAllLaybys As Collection
    '-- follows grid.
    Private mColGridlaybys As Collection

    '-- Result-
    Private mIntSelectedLayby_id As Integer = -1
    Private mColSelectedLaybyInfo As Collection

    Private mbCancelled As Boolean = False
    Private mIntSelectedRowResults As Integer = -1

    '-browse32-
    Private mBrowseLaybyStock As clsBrowse3
    Private msSelectStockSql As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)

    '= = = = = = = = = = = = = = = = = = = = == = 
    '-===FF->

    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                            ByVal intParentHeight As Integer)

        Me.Width = intParentWidth - 11
        Me.Height = intParentHeight - 11
        '-- resize our controls..
        DoEvents()
        '-- resize main box and top panel-
        panelHdr.Width = Me.Width

        TabControlMain.Width = Me.Width - 11
        TabControlMain.Height = Me.Height - TabControlMain.Top - 15

        labHdrInfo.Left = TabPageCustomerLaybys.Width - labHdrInfo.Width - 5
        panelLayby.Left = labHdrInfo.Left
        panelLayby.Height = TabPageCustomerLaybys.Height - panelLayby.Top - 7

        dgvLaybys.Width = TabPageCustomerLaybys.Width - panelLayby.Width - 20
        dgvLaybys.Height = TabPageCustomerLaybys.Height - 11
        '-stock-
        frameBrowse.Width = TabControlMain.Width - 7
        frameBrowse.Height = TabPageLaybyStock.Height - 7

        dgvStockList.Width = frameBrowse.Width - 5
        dgvStockList.Height = frameBrowse.Height - dgvStockList.Top - 12

        DoEvents()

    End Sub '-SubFormResized
    '= = = = = = = = = = = =  === =

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

    '--sub new-
    '--sub new-
    '= 3401.414=  ComputerName is noe INPUT !!

    Public Sub New(ByVal sComputerName As String, _
                   ByRef FrmParent As Form, _
                     ByRef cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                       ByRef colSqlDBInfo As Collection, _
                          ByVal sVersionPOS As String, _
                            ByVal intStaff_id As Integer, _
                               ByVal sStaffName As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.
        '--save -
        msComputerName = sComputerName

        mFrmParent = FrmParent
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo

        msVersionPOS = sVersionPOS
        mIntStaff_id = intStaff_id
        msStaffName = sStaffName

    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==

    '- input collection of layby's.--

    'WriteOnly Property laybys() As Collection
    '    Set(ByVal Value As Collection)

    '        mColAllLaybys = Value
    '    End Set
    'End Property '--prefs..-
    ''= = = = =  =  = =  = =

    WriteOnly Property saleCustomer_id() As Integer
        Set(ByVal Value As Integer)

            mIntSaleCustomer_id = Value
        End Set
    End Property '--prefs..-
    '= = = = =  =  = =  = =

    '-- select for delivery-
    WriteOnly Property selectForDelivery As Boolean
        Set(value As Boolean)
            mbSelectForDelivery = value
        End Set
    End Property '-select=
    '= = = = = = = = = = = =

    '-result-
    ReadOnly Property selectedLaybyId As Integer
        Get
            selectedLaybyId = mIntSelectedLayby_id
        End Get
    End Property  '- selected id-
    '= = = = = = = = = = = == = = 
    '-result-
    ReadOnly Property selectedLaybyInfo As Collection
        Get
            selectedLaybyInfo = mColSelectedLaybyInfo
        End Get
    End Property  '- selected id-
    '= = = = = = = = = = = == = = 

    '-result-
    ReadOnly Property cancelled As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property  '- cancelled-
    '= = = = = = = = = = = == ==== 
    '= = = = = = = = = = = == ==== 
    '-===FF->

    '=4201.0529=--- INITIALISE Layby Stock Browser..  --
    '--      EX Serials..-.--
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
        browse1.tableName = "laybyline"  '==sHostTablename

        browse1.UserSelectList = sSelectList
        browse1.WhereCondition = sWhere

        '-serialNumber
        browse1.InitialOrder1 = "cat1"
        '= browse1.InitialOrder1 = "cat1"
        browse1.InitialOrder2 = "cat2"
        browse1.InitialOrder3 = "description"

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

    '- refresh laybys

    Private Function mbRefreshLaybys() As Boolean
        Dim s1, sDelivered As String
        Dim colCust, colThisCustLaybys As Collection  '-customers/layby's/items.
        Dim intCustomer_id, intX As Integer
        Dim sCustName, sCustBarcode, sCredit, sDateStarted, sDateDelivered As String
        Dim decCredits, decDebits, decCreditNoteBalance As Decimal
        Dim decLaybyDiscount_nett, decLaybyDiscount_tax, decLaybyDiscount_inc As Decimal
        Dim dtPayments As DataTable
        Dim bShowDelivered As Boolean = False
        Dim colItems As Collection

        If chkShowCompleted.Checked Then
            bShowDelivered = True
        End If
        '=3403.430= --  Check if any Undelivered Layby's on the shelf for this customer.
        '-- mIntSaleCustomer_id will be -1 if not called from sales.
        If Not gbCollectCustomerLaybys(mCnnSql, mColAllLaybys, mIntSaleCustomer_id, bShowDelivered) Then
            MsgBox("Failed to get Laybys..", MsgBoxStyle.Exclamation)
        Else  '-ok-
            '-- Load cust list..
            mbGridRefreshing = True
            dgvLaybys.Rows.Clear()
            mColGridlaybys = New Collection

            For Each colCust In mColAllLaybys
                '= colCust = mColAllLaybys.Item(CStr(mIntSaleCustomer_id))
                colThisCustLaybys = colCust.Item("laybys")
                sCustName = colCust.Item("customer_name")
                sCustBarcode = colCust.Item("customer_barcode")
                intCustomer_id = colCust.Item("customer_id")
                '-- get cr-Note Credit for this customer-
                decCreditNoteBalance = 0
                '==   Updated.- 3519.0219  Started 18-Feb-2019= 
                '==   - Fix code to use correct customer_id.)..
                'If Not gbGetCreditNoteHistory(mCnnSql, mIntSaleCustomer_id, dtPayments, _
                '                     decCredits, decDebits, decCreditNoteBalance) Then
                '    MsgBox("Failed Looking up credit notes.. ", MsgBoxStyle.Exclamation)
                '    '=Exit Function
                'End If  '--get-
                If Not gbGetCreditNoteHistory(mCnnSql, intCustomer_id, dtPayments, _
                                                  decCredits, decDebits, decCreditNoteBalance) Then
                    MsgBox("Failed Looking up credit notes.. ", MsgBoxStyle.Exclamation)
                    '=Exit Function
                End If  '--get-

                sCredit = FormatCurrency(decCreditNoteBalance, 2)
                '-test-
                s1 = ""
                For Each colThisLayby As Collection In colThisCustLaybys
                    '- each layby
                    s1 &= "LayByNo: " & colThisLayby.Item("layby_id") & _
                           ";  TotalAmt: " & CStr(colThisLayby.Item("total_inc"))
                    sDelivered = IIf((CInt(colThisLayby.Item("isDelivered")) <> 0), "Yes", "No")
                    sDateStarted = Format(colThisLayby.Item("Layby_date_started"), "dd-MMM-yyyy")
                    sDateDelivered = ""
                    If colThisLayby.Contains("Layby_date_delivered") Then
                        sDateDelivered = Format(colThisLayby.Item("Layby_date_delivered"), "dd-MMM-yyyy")
                    End If
                    '=3519.0414=
                    decLaybyDiscount_nett = colThisLayby.Item("discount_nett")
                    decLaybyDiscount_tax = colThisLayby.Item("discount_tax")
                    decLaybyDiscount_inc = decLaybyDiscount_nett + decLaybyDiscount_tax
                    colItems = colThisLayby.Item("items")
                    '-- add grid row -
                    Dim row1 = New DataGridViewRow
                    intX = dgvLaybys.Rows.Add(row1)
                    With dgvLaybys.Rows(intX)
                        .Cells(k_GRIDCOL_CUST_NAME).Value = sCustName & " [" & sCustBarcode & "]"
                        '= .Cells(1).Value = sCustBarcode
                        .Cells(k_GRIDCOL_CUST_CREDIT).Value = sCredit
                        .Cells(k_GRIDCOL_LAYBY_ID).Value = CStr(colThisLayby.Item("layby_id"))
                        .Cells(k_GRIDCOL_DATE_STARTED).Value = sDateStarted
                        .Cells(k_GRIDCOL_IS_DELIVERED).Value = sDelivered
                        .Cells(k_GRIDCOL_TOTAL_AMT).Value = FormatCurrency(colThisLayby.Item("total_inc"), 2)
                        .Cells(k_GRIDCOL_CUST_ID).Value = CStr(intCustomer_id)
                        .Cells(k_GRIDCOL_DATE_DELIVERED).Value = sDateDelivered  '=CStr(intCustomer_id)
                        .Cells(k_GRIDCOL_COL_ITEMS).Value = colItems  '=CStr(intCustomer_id)
                        .Cells(k_GRIDCOL_DISCOUNT).Value = FormatCurrency(decLaybyDiscount_inc, 2)  '=CStr(intCustomer_id)
                    End With
                    '-- save layby collection with grid Rowx in Static var.
                    '-- when row is selected, we pick up details.
                    mColGridlaybys.Add(colThisLayby, CStr(intX))
                Next colThisLayby
                '    MsgBox("Customer has " & colThisCustLaybys.Count & " laybys pending. viz:" & _
                '                                                      vbCrLf & s1, MsgBoxStyle.Information)
            Next colCust
            dgvLaybys.ClearSelection()
            '- refresh header --
            If (mIntSaleCustomer_id > 0) Then  '--selected customer only-
                labSubHdr.Text = "Layby's currently active- (Customer " & sCustName & "[" & sCustBarcode & "] only."
            Else
                labSubHdr.Text = "Layby's currently active (All customers)"
            End If  '-cust-
            mbGridRefreshing = False

            '=4201.0529- Show all layby stock in stock items grid..
            '=4201.0529- Show all layby stock in stock items grid..

            Dim sSqlSelect As String = ""
            Dim sWhere As String = ""

            sSqlSelect = "SELECT stock.cat1, stock.cat2, LL.description, quantity, stock.barcode AS stock_barcode, "
            sSqlSelect &= " serialNumber, LL.layby_id, LBY.total_inc AS layby_total_inc, "
            sSqlSelect &= "  customer.barcode AS customer_barcode,  "
            sSqlSelect &= "  customerName = CASE companyName  "
            sSqlSelect &= "     WHEN '' THEN (customer.lastname + ', ' + customer.firstname)"
            sSqlSelect &= "     WHEN 'n/a' THEN (customer.lastname + ', ' + customer.firstname)"
            sSqlSelect &= "     ELSE companyName "
            sSqlSelect &= "   END "
            '= sSql &= "     CUST.companyName, CUST.firstName, CUST.lastName "
            sSqlSelect &= " FROM dbo.laybyLine LL "
            sSqlSelect &= " JOIN dbo.stock ON (stock.stock_id=LL.stock_id) "
            sSqlSelect &= " JOIN dbo.layby LBY ON (LBY.layby_id=LL.layby_id) "
            sSqlSelect &= " JOIN dbo.customer ON (customer.customer_id=LBY.customer_id) "

            msSelectStockSql = sSqlSelect  '--save for later.

            '= sSqlSelect &= "   WHERE (LBY.isCancelled=0) AND (LBY.isDelivered=0) "
            sWhere = "(LBY.isCancelled=0) "

            If (Not chkShowCompleted.Checked) Then
                sWhere &= " AND (LBY.isDelivered=0) "
            End If
            '= sSqlSelect &= " ORDER BY stock.cat1, description;"

            Call mbInitialiseBrowse(mBrowseLaybyStock, sSqlSelect, sWhere, _
                                               dgvStockList, labRecCount, LabFind, txtFind)

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If  '--collect-

    End Function '-refresh-
    '= = = = = = = = == == == =
    '-===FF->

    '--load-

    Private Sub frmLaybys_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (mIntSaleCustomer_id > 0) Then  '--selected customer only-
            btnCancelLayby.Visible = False

        Else  '--all customers.
            btnOK.Visible = False
            btnCancel.Text = "Exit"

        End If  '-customer id=
        If mbSelectForDelivery Then
            labHdrInfo.Text = "Select the Layby to be deiivered, and Press OK.."
        End If
        '= Call CenterForm(Me)
        btnOK.Enabled = False

        txtStockSearch.Text = ""

        '-- STUFF fromForm Shown event..
        Call mbRefreshLaybys()

        mbIsInitialising = False
        If (dgvLaybys.Rows.Count > 0) Then
            dgvLaybys.Rows(0).Selected = True  '--select first row.
        End If

    End Sub  '-load-
    '= = = = = = = = == == == =

    'Private Sub frmLaybys_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

    '    If mbActivated Then Exit Sub
    '    mbActivated = True

    'End Sub  '-activated-
    '= = = = = = = = = = = = = == =

    '-- shown-

    'Private Sub frmLaybys_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
    '    Dim s1 As String
    '    Dim colCust, colThisCustLaybys As Collection  '-customers/layby's/items.
    '    Dim IntCustomer_id, intX As Integer
    '    Dim sCustName, sCustBarcode, sCredit As String

    '    '= If mbActivated Then Exit Sub
    '    '== mbActivated = True

    '    Call mbRefreshLaybys()

    '    mbIsInitialising = False
    '    If (dgvLaybys.Rows.Count > 0) Then
    '        dgvLaybys.Rows(0).Selected = True  '--select first row.
    '    End If

    'End Sub '-shown-
    '= = = = =  =  = =  = =

    Private Sub chkShowCompleted_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowCompleted.CheckedChanged
        Call mbRefreshLaybys()
    End Sub '--show Completed-
    '= = = = = = = = = = == = = 
    '-===FF->

    '- Selection changed.-
    '-- update layby details..

    Private Sub dgvLaybys_SelectionChanged(ByVal sender As Object, _
                                            ByVal ev As EventArgs) Handles dgvLaybys.SelectionChanged
        Dim intIdx As Integer = dgvLaybys.CurrentRow.Index
        Dim colThisLayby, colItems As Collection
        Dim decAmt As Decimal
        Dim intLaybyId As Integer

        If mbIsInitialising Or mbGridRefreshing Then
            Exit Sub
        End If

        txtLaybyItems.Text = ""
        If (dgvLaybys.Rows.Count > 0) Then
            '= dgvLaybys.Rows(0).Selected = True  '--select first row.
            If (intIdx >= 0) Then
                colThisLayby = mColGridlaybys.Item(CStr(intIdx))
                '-- test
                '= decAmt = colThisLayby.Item("total_inc")
                '= txtLaybyTotal.Text = FormatCurrency(decAmt, 2)
                '=colItems = colThisLayby.Item("items")
                '-Layby_date_started-
                '=txtLaybyDate.Text = Format(colThisLayby.Item("Layby_date_started"), "dd-MMM-yyyy")
                txtDateDelivered.Text = ""
                txtLaybyItems.Text = ""
                'If colThisLayby.Contains("Layby_date_delivered") Then
                '    txtDateDelivered.Text = Format(colThisLayby.Item("Layby_date_delivered"), "dd-MMM-yyyy")
                'End If

                With dgvLaybys.Rows(intIdx)
                    txtLaybyCustomer.Text = .Cells(k_GRIDCOL_CUST_NAME).Value
                    intLaybyId = CInt(.Cells(k_GRIDCOL_LAYBY_ID).Value)
                    '- this wasn't working from collection.
                    txtLaybyDate.Text = .Cells(k_GRIDCOL_DATE_STARTED).Value
                    txtDiscount.Text = .Cells(k_GRIDCOL_DISCOUNT).Value
                    txtLaybyTotal.Text = .Cells(k_GRIDCOL_TOTAL_AMT).Value
                    txtDateDelivered.Text = .Cells(k_GRIDCOL_DATE_DELIVERED).Value
                    colItems = .Cells(k_GRIDCOL_COL_ITEMS).Value
                End With
                txtLaybyId.Text = CStr(intLaybyId)
                If colItems IsNot Nothing Then
                    For Each colItem As Collection In colItems
                        txtLaybyItems.Text &= "barcode: " & colItem.Item("stock_barcode") & _
                                                          "  SerNo: " & colItem.Item("serialNumber") & vbCrLf
                        txtLaybyItems.Text &= colItem.Item("description") & vbCrLf & vbCrLf
                    Next colItem
                End If
                mIntSelectedLayby_id = intLaybyId
                mColSelectedLaybyInfo = colThisLayby  '-for caller-
                btnOK.Enabled = True
            End If '-index-
        End If  '-count-
    End Sub '-SelectionChanged-
    '= = = = = = = = = = = = =  =
    '-===FF->

    '--dgvLaybys_CellContentClick-

    Private Sub dgvLaybys_CellContentClick(sender As Object, _
                                            ev As DataGridViewCellEventArgs) Handles dgvLaybys.CellContentClick

    End Sub  '-dgvLaybys_CellContentClick-
    '= = = = = = = = = = = = = = = = == ==

    '-print label-

    Private Sub btnPrintLabel_Click(sender As Object, e As EventArgs) Handles btnPrintLabel.Click
        Dim clsPrint1 As New clsPrintSaleDocs
        Dim frmGetPrinter1 As frmGetPrinter
        Dim msPrinterName As String
        Dim intNewCount As Integer

        If mIntSelectedLayby_id <= 0 Then
            Exit Sub
        End If

        frmGetPrinter1 = New frmGetPrinter
        frmGetPrinter1.WhichPrinter = "label"
        frmGetPrinter1.RequestedNumberOfLabels = 2
        frmGetPrinter1.ShowDialog()
        If frmGetPrinter1.cancelled Then
            frmGetPrinter1.Close()
            MsgBox("Request was cancelled.", MsgBoxStyle.Information)
            Exit Sub
        End If
        intNewCount = frmGetPrinter1.NumberOfLabels
        msPrinterName = frmGetPrinter1.SelectedPrinterName
        frmGetPrinter1.Close()
        If (intNewCount > 0) Then
            '- do printing-
            Call clsPrint1.PrintLaybyLabels(mIntSelectedLayby_id, intNewCount, msPrinterName, _
                                    txtLaybyCustomer.Text, _
                                       txtLaybyDate.Text & "; Value:" & txtLaybyTotal.Text)
        End If  '-count-
        Me.BringToFront()

    End Sub  '-print label-
    '= = = = = = = = = = = = =  =
    '-===FF->

    '-cancel Layby-

    Private Sub btnCancelLayby_Click(sender As Object, e As EventArgs) Handles btnCancelLayby.Click
        '- get selected layby.

        Dim intIdx As Integer = dgvLaybys.CurrentRow.Index
        Dim intRecordsAffected As Integer
        '= Dim colThisLayby, colItem, colItems As Collection
        Dim colItem, colItems As Collection
        Dim thisRow As DataGridViewRow
        Dim sCust, sDescription As String
        Dim intLaybyId As Integer

        If (dgvLaybys.Rows.Count > 0) Then
            If (dgvLaybys.SelectedRows.Count > 0) Then
                thisRow = dgvLaybys.SelectedRows(0)
                intIdx = thisRow.Index
                '= colThisLayby = mColGridlaybys.Item(CStr(intIdx))
                '= colItems = colThisLayby.Item("items")
                '-  get first item.-
                sCust = thisRow.Cells(0).Value
                intLaybyId = CInt(thisRow.Cells(k_GRIDCOL_LAYBY_ID).Value)
                colItems = thisRow.Cells(k_GRIDCOL_COL_ITEMS).Value
                colItem = colItems.Item(1)
                sDescription = colItem.Item("description")

                If MsgBox("Sure you want to cancel this Layby No: " & intLaybyId & vbCrLf & _
                           sDescription & vbCrLf & "For Customer:" & vbCrLf & sCust, _
                           MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Dim sqlCmd1 As OleDbCommand  '== SqlCommand
                    Dim sMsg, sErrorMsg, sSql As String

                    sSql = "UPDATE dbo.layby SET isCancelled=1 "
                    sSql &= " , date_cancelled= CURRENT_TIMESTAMP "
                    sSql &= " , cancelled_staff_id=" & CStr(mIntStaff_id)
                    sSql &= "   WHERE (layby_id=" & CStr(intLaybyId) & ");"
                    Try
                        sqlCmd1 = New OleDbCommand(sSql, mCnnSql)  '== SqlCommand(sSqlSelect, cnnSql)
                        intRecordsAffected = sqlCmd1.ExecuteNonQuery
                        '--ok=
                        MsgBox("Cmd Completed ok.." & vbCrLf & _
                                "  and " & intRecordsAffected & " records were affected.", MsgBoxStyle.Information)
                    Catch ex As Exception
                        sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
                        sErrorMsg = "Cancel layby: Error in Executing Sql: " & vbCrLf & _
                                  sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & _
                                   "--- end of error msg.--" & vbCrLf
                        Call gbLogMsg(gsErrorLogPath, sErrorMsg)
                        '=msLastSqlErrorMessage = sErrorMsg
                        MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
                    End Try
                    '- refresh-
                    Call mbRefreshLaybys()
                Else  '-no
                    '- nothing to do.
                End If '-- yes-
            End If  '-count-
        End If  '-count-

    End Sub '-cancel Layby-
    '= = = = = = = = = = = = =  =
    '-===FF->

    '-- Stock List Data Grid..

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvdgvStockList_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvStockList.Sorted
        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvStockList.SortedColumn
        sName = currentColumn.HeaderText
        '= MsgBox("Calling sortColumn for " & sName)
        Call mBrowseLaybyStock.SortColumn(sName)
    End Sub  '--sorted-
    '= = = = = = = = =  = = =
  
    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtFind_Enter(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles txtFind.Enter
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = = = = = = = = 

    Private Sub txtFind_Leave(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtFind.Leave
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, False)

    End Sub '--Leave focus--
    '= = = = = = = = = = = = = = =  =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtResultsFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged

        If mbIsInitialising Then Exit Sub
        '-- go Find..
        Call mBrowseLaybyStock.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '=4201.00529-  Catch Enter Key on stock srch text-

    Private Sub txtStockSearch_keyPress(ByVal sender As System.Object, _
                                         ByVal EventArgs As KeyPressEventArgs) Handles txtStockSearch.KeyPress
        Dim keyAscii As Short = Asc(EventArgs.KeyChar)
        Dim e2 As New EventArgs
        If keyAscii = 13 Then '--enter-
            Call cmdStockSearch_Click(cmdStockSearch, e2)
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

    Private Sub cmdStockSearch_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles cmdStockSearch.Click
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
                      {"stock.barcode", "cat1", "cat2", "LL.description"}

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtStockSearch.Text), asColumns)
        sWhere = "(LBY.isCancelled=0) "

        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then
                sWhere = sWhere & " AND "
            End If
            sWhere = sWhere & sSql
        End If
        If (Not chkShowCompleted.Checked) Then
            sWhere &= " AND (LBY.isDelivered=0) "
        End If
        '= sSqlSelect &= " ORDER BY stock.cat1, description;"

        Call mbInitialiseBrowse(mBrowseLaybyStock, msSelectStockSql, sWhere, _
                                           dgvStockList, labRecCount, LabFind, txtFind)

    End Sub '-cmdStockSearch-
    '= = = = = = = = = = = = =  =

    Private Sub cmdClearStockSearch_Click(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) Handles cmdClearStockSearch.Click
        txtStockSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdStockSearch_Click(cmdStockSearch, New System.EventArgs())

    End Sub  '-ClearStockSearch-
    '= = = = = = = = = = = = = = = =

    '==  END of stock BROWSING..--
    '==  END of stock BROWSING..--




    '= = = = = = = = = = = = =  =
    '-===FF->


    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        '= Me.Hide()
        Call close_me()

    End Sub  '-ok-
    '= = = = = = = = = 

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        mbCancelled = True
        '= Me.Hide()
        Call close_me()

    End Sub  '-cancel-
    '= = = = = = = = = = = = = 

    Private Sub close_me()

        '- inform parent.-
        '- Report to Parent..-
        '= Dim objParms() As Object = {Me.Name, "FormClosed", ""}


        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)


        'If Not (Me.delReport Is Nothing) Then
        '    delReport.Invoke(Me.Name, "FormClosed", "")
        'End If

        '-test-
        '=MsgBox("closing name= " & Me.Name, MsgBoxStyle.Information)
        '==  Dosposing probably wrong because parent tabpage has just been disposed. (of)
        '= Me.Dispose(True)

    End Sub '--close me-
    '= = = = = = = = = = = = ==  == 

End Class '-frmLaybys-
'= = = = = = = = == = = == =

'== end form ==