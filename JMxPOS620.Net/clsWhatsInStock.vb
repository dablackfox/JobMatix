
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'== Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class clsWhatsInStock

    '==
    '==   Created.- 3519.0303  Started 02-March-2019= 
    '==     -- New class clsWhatsInStock to redo this report as Standard Repoert with Cat1 SUBTOTALS...
    '==            (No longer a grid report.)
    '==     --  Called from main Reports Form..


    '== Target 6201- Updating 14-July-2021..
    '==    Updates For Target OpenSource version Build 6201...
    '==  Add features to Product Sales and What's In Stock to filter for a particular stock Supplier..
    '= = = = =



    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--

    Private mFrmReportParent1 As Form

    '-- parent Controls..
    Private mOptCat1Cat2Description As RadioButton
    Private mOptCat1Description As RadioButton
    Private mOptDescription As RadioButton

    Private mChkNoReportIfZeroStock As CheckBox
    Private mChkNoReportIfNegStock As CheckBox

    '== Target 6201- Updating 14-July-2021..
    Private mCboSupplierStock As ComboBox
    '== END Target 6201- Updating 14-July-2021..


    Private mColReportLines As Collection

    '= = = = = = = =  = = = = = = = = = =
    '-===FF->



    '- P u b l i c  Methods--
    '- P u b l i c  Methods--
    '- P u b l i c  Methods--

    Public Sub New(ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                         ByVal sVersionPOS As String, _
                               ByVal sStaffName As String)

         '= Dim sErrorMsg As String
        Dim s1, s2, s3, sSql, sUpdates As String
        Dim rx, L1 As Integer
   
        '==Try
         '--save -
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName

    End Sub  '-new-
    '= = = =  = = = == = =
    '= = = = = = = =  = = = = = = = = = =
    '-===FF->

    '-- Build Report..

    Public Function BuildReport(ByRef frmReportParent1 As Form, _
                                ByRef colReportLines As Collection) As Boolean
        Dim controls1() As Control
        Dim sWhere As String = ""
        Dim sSQL, s1 As String
        Dim dtStockItems As DataTable

        BuildReport = False

        '-- save ref to Main Form..-
        mFrmReportParent1 = frmReportParent1

        '- save option controls..
        Try
            controls1 = mFrmReportParent1.Controls.Find("OptCat1Cat2Description", True)
            If (controls1.Length > 0) Then mOptCat1Cat2Description = controls1(0)
            controls1 = mFrmReportParent1.Controls.Find("OptCat1Description", True)
            If (controls1.Length > 0) Then mOptCat1Description = controls1(0)
            controls1 = mFrmReportParent1.Controls.Find("OptDescription", True)
            If (controls1.Length > 0) Then mOptDescription = controls1(0)

            controls1 = mFrmReportParent1.Controls.Find("chkNoReportIfZeroStock", True)
            If (controls1.Length > 0) Then mChkNoReportIfZeroStock = controls1(0)
            controls1 = mFrmReportParent1.Controls.Find("chkNoReportIfNegStock", True)
            If (controls1.Length > 0) Then mChkNoReportIfNegStock = controls1(0)

            '== Target 6201- Updating 14-July-2021..
            '=Private mCboSupplierStock As ComboBox
            controls1 = mFrmReportParent1.Controls.Find("cboSupplierStock", True)
            If (controls1.Length > 0) Then mCboSupplierStock = controls1(0)
            '== END Target 6201- Updating 14-July-2021..

        Catch ex As Exception
            MsgBox("Error in WhatsInStock class (capturing parent controls.).." & vbCrLf & _
                                                         ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        '== test options..

        Dim sOrderBy As String = "cat1, cat2, description"
        Try
            If mOptCat1Cat2Description.Checked Then
                sOrderBy = "cat1, cat2, description"
            ElseIf mOptCat1Description.Checked Then
                sOrderBy = "cat1,  description"
            ElseIf mOptDescription.Checked Then
                sOrderBy = " description"
            End If
        Catch ex As Exception
            MsgBox("Error in WhatsInStock class (Accessing parent Option controls.).." & vbCrLf & _
                                                                   ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        '= labReportName.Text = "Report: Stock on Hand."
        '= labExplain.Text = "Shows all Stock on hand by Product as of Now."
        sWhere = ""

        sSQL = "SELECT  description, cat1, cat2, barcode, stock_id,   "
        sSql &= "   costExTax AS cost_ex_$, sellExTax AS sell_ex_$, qtyInStock, "
        sSql &= "    (costExTax *qtyInStock) AS ExtCost_ex_$, "
        sSql &= "    (sellExTax *qtyInStock) AS ExtSell_ex_$ "
        sSql &= " FROM dbo.stock "

        Try
            If mChkNoReportIfZeroStock.Checked And mChkNoReportIfNegStock.Checked Then
                sWhere = " (qtyInStock >0)  "  '- no zero or neg stock wanted.
            ElseIf mChkNoReportIfZeroStock.Checked And (Not mChkNoReportIfNegStock.Checked) Then
                sWhere = " (qtyInStock <>0) "  '- no zero, but neg stock wanted.
            Else  '=If not chkNoReportIfZeroStock.Checked And not chkNoReportIfNegStock.Checked Then
                sWhere = ""  '--include everything..
            End If
        Catch ex As Exception
            MsgBox("Error in WhatsInStock class (Accessing parent CheckBox controls.).." & vbCrLf &
                                                                        ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        '== Target 6201- Updating 14-July-2021..
        '=Private mCboSupplierStock As ComboBox
        '==    Updates For Target OpenSource version Build 6201...
        '==  Add features to Product Sales and What's In Stock to filter for a particular stock Supplier..
        '= = = = =

        '-- Select for Supplier if op. selected..
        Dim sSupplierSelection As String = "- All Suppliers -"  '-default.

        If (mCboSupplierStock.SelectedIndex > 0) Then  '-not "ALL"..
            '-  get staff id for item.
            Dim sId As String
            Dim ix As Integer
            s1 = Trim(mCboSupplierStock.SelectedItem)
            ix = InStr(s1, ".")
            If (ix > 1) Then  '-have something.
                sId = Trim(Mid(s1, ix + 1))
                sSupplierSelection = Trim(VB.Left(s1, ix - 1))
                If (sWhere <> "") Then
                    sWhere &= " AND "
                End If
                sWhere &= " (stock.supplier_id= " & sId & ") "
            End If  '-ix-
        End If  '-selected index-
        '== END Target 6201- Updating 12-July-2021..
        '==    Updates For Target OpenSource version Build 6201...
        '==  Add features to Product Sales and What's In Stock to filter for a particular stock Supplier..
        '= = = = =
        '== END Target 6201- Updating 14-July-2021..

        '-ok-

        If (sWhere <> "") Then
            sSQL &= " WHERE " & sWhere
        End If
        sSql &= " ORDER BY " & sOrderBy & " ;"

        '- get Query result and load datagrid..-
        If Not gbGetDataTable(mCnnSql, dtStockItems, sSQL) Then
            MsgBox("Error in SELECT for Stock table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        End If  '-get-
        '- ok-

        If dtStockItems Is Nothing Then
            MsgBox("Error in SELECT for Stock table: " & vbCrLf & _
                                "Datatable object is NOTHING. ", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '== DON'T USE GRID- 
        '-- Use our Report printer Class-
        mColReportLines = New Collection

        If (dtStockItems.Rows.Count <= 0) Then
            MsgBox("No items to report in Stock table: " & vbCrLf & _
                                "Datatable is Empty. ", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        Dim bMakeCat1Totals As Boolean = False
        Dim sCat1 As String = ""
        Dim sLastCat1 As String = ""

        Dim intStock_id As Integer = -1
        Dim sLine, sCat2, sDescription, sQty, sAmount, sStaff As String
        Dim decCost, decSell, decQty As Decimal
        Dim decExtCost, decExtSell As Decimal
        Dim decCat1SubTotalCost, decCat1SubTotalSell As Decimal

        Dim decFinalTotalCost As Decimal = 0
        Dim decFinalTotalSell As Decimal = 0

        '-  define tab columns- (ofsets from our marg)-  Max Width is 760-
        Const k_TAB_STOCK_DESCRIPTION As Integer = 0  '-size 200-
        Const k_TAB_STOCK_CAT1 As Integer = 200   '-size 50= +10 gap..
        Const k_TAB_STOCK_CAT2 As Integer = 260  '-size 50 +10
        Const k_TAB_STOCK_BARCODE As Integer = 320 '-size 70 
        '=costExTax-
        Const k_TAB_STOCK_COST_EX_TAX As Integer = 390  '-size 70
        Const k_TAB_STOCK_SELL_EX_TAX As Integer = 450  '-size 70
        Const k_TAB_STOCK_QTY As Integer = 530  '-size 50
        Const k_TAB_STOCK_EXTCOST_EX_TAX As Integer = 570  '-size 70
        Const k_TAB_STOCK_EXTSELL_EX_TAX As Integer = 650 '-size 80

        Const k_WIDTH_STOCK_AMOUNT As Short = 70
        Const k_WIDTH_STOCK_EXT_AMOUNT As Short = 80
        Const k_WIDTH_STOCK_QTY As Short = 40

        If mOptCat1Cat2Description.Checked Or mOptCat1Description.Checked Then
            bMakeCat1Totals = True
        End If

        decCat1SubTotalCost = 0
        decCat1SubTotalSell = 0

        '--  do all selected stock items..
        For Each drStockItem As DataRow In dtStockItems.Rows

            sCat1 = Trim(drStockItem.Item("cat1"))
            If bMakeCat1Totals Then
                '-- Tracking Cat1's 
                If (LCase(sCat1) <> LCase(sLastCat1)) Then
                    '-new or first Cat1-
                    If (sLastCat1 = "") Then  '- previous-
                        '-- First item. of report-
                    Else '--just passed a completed Cat1.-
                        '-  show Cat1 totals..
                        '= "Totals ext-cost/sell
                        sAmount = FormatCurrency(decCat1SubTotalCost, 2)
                        sLine = "<textline>"
                        sLine &= "<txt TAB=""" & (k_TAB_STOCK_DESCRIPTION + 40) & """  >Total for Cat1:   " & sLastCat1 & "</txt>"
                        sLine &= "<txt TAB=""" & k_TAB_STOCK_EXTCOST_EX_TAX & """  align=""right""  fontstyle=""bold""  "
                        sLine &= " width = """ & k_WIDTH_STOCK_EXT_AMOUNT & """  >" & sAmount & "</txt>"
                        '-- total sell-
                        sAmount = FormatCurrency(decCat1SubTotalSell, 2)
                        sLine &= "<txt TAB=""" & k_TAB_STOCK_EXTSELL_EX_TAX & """  align=""right""  fontstyle=""bold""  "
                        sLine &= " width = """ & k_WIDTH_STOCK_EXT_AMOUNT & """  >" & sAmount & "</txt>"
                        sLine &= "</textline>"
                        mColReportLines.Add(sLine)

                        '= mColReportLines.Add(sLine)
                        s1 = "<drawline fontstyle=""bold"" />"
                        mColReportLines.Add(s1)
                        '-- done with last cat1.-
                    End If  '-first/prev.-
                    '--start new  Cat1-
                    sLastCat1 = sCat1
                    '- reset totals.
                    decCat1SubTotalCost = 0
                    decCat1SubTotalSell = 0
                End If  '-same Cat1-
            End If  '-Keeping Cat1 totals-

            '-- Now Process this item..
            decCost = drStockItem.Item("cost_ex_$")
            decSell = drStockItem.Item("sell_ex_$")
            decExtCost = drStockItem.Item("ExtCost_ex_$")
            decExtSell = drStockItem.Item("ExtSell_ex_$")
            '- qtyInStock-
            decQty = drStockItem.Item("qtyInStock")
            decCat1SubTotalCost += decExtCost
            decCat1SubTotalSell += decExtSell
            decFinalTotalCost += decExtCost
            decFinalTotalSell += decExtSell

            sDescription = drStockItem.Item("description")
            If (Len(sDescription) > 34) Then
                '-truncate-
                sDescription = VB.Left(sDescription, 34) & ".."
            End If

            '-- Build Stock Line.
            sLine = "<textline>"
            sLine &= "<txt TAB=""" & k_TAB_STOCK_DESCRIPTION & """  >" & sDescription & "</txt>"
            sLine &= "<txt TAB=""" & k_TAB_STOCK_CAT1 & """  >" & drStockItem.Item("cat1") & "</txt>"
            sLine &= "<txt TAB=""" & k_TAB_STOCK_CAT2 & """  >" & drStockItem.Item("cat2") & "</txt>"
            sLine &= "<txt TAB=""" & k_TAB_STOCK_BARCODE & """  >" & drStockItem.Item("barcode") & "</txt>"
            '-cost-
            sAmount = FormatNumber(decCost, 2)
            sLine &= "<txt TAB=""" & k_TAB_STOCK_COST_EX_TAX & """  align=""right""    "
            sLine &= " width = """ & k_WIDTH_STOCK_AMOUNT & """  >" & sAmount & "</txt>"
            '--sell-
            sAmount = FormatNumber(decSell, 2)
            sLine &= "<txt TAB=""" & k_TAB_STOCK_SELL_EX_TAX & """  align=""right""    "
            sLine &= " width = """ & k_WIDTH_STOCK_AMOUNT & """  >" & sAmount & "</txt>"
            '-qty-
            sLine &= "<txt TAB=""" & k_TAB_STOCK_QTY & """  align=""right"" "
            sLine &= " width = """ & k_WIDTH_STOCK_QTY & """  >" & CStr(decQty) & "</txt>"

            '= "Line ext-cost/sell
            sAmount = FormatNumber(decExtCost, 2)
            sLine &= "<txt TAB=""" & k_TAB_STOCK_EXTCOST_EX_TAX & """  align=""right""   "
            sLine &= " width = """ & k_WIDTH_STOCK_EXT_AMOUNT & """  >" & sAmount & "</txt>"
            '-- ext sell-
            sAmount = FormatNumber(decExtSell, 2)
            sLine &= "<txt TAB=""" & k_TAB_STOCK_EXTSELL_EX_TAX & """  align=""right""    "
            sLine &= " width = """ & k_WIDTH_STOCK_EXT_AMOUNT & """  >" & sAmount & "</txt>"

            sLine &= "</textline>"
            mColReportLines.Add(sLine)

        Next drStockItem

        '-- done all items.-

        ''-- show last cat1 total.
        If bMakeCat1Totals Then
            sAmount = FormatCurrency(decCat1SubTotalCost, 2)
            sLine = "<textline>"
            sLine &= "<txt TAB=""" & (k_TAB_STOCK_DESCRIPTION + 40) & """  >Total for Cat1:   " & sLastCat1 & "</txt>"
            sLine &= "<txt TAB=""" & k_TAB_STOCK_EXTCOST_EX_TAX & """  align=""right""  fontstyle=""bold""  "
            sLine &= " width = """ & k_WIDTH_STOCK_EXT_AMOUNT & """  >" & sAmount & "</txt>"
            '-- total sell-
            sAmount = FormatCurrency(decCat1SubTotalSell, 2)
            sLine &= "<txt TAB=""" & k_TAB_STOCK_EXTSELL_EX_TAX & """  align=""right""  fontstyle=""bold""  "
            sLine &= " width = """ & k_WIDTH_STOCK_EXT_AMOUNT & """  >" & sAmount & "</txt>"
            sLine &= "</textline>"
            mColReportLines.Add(sLine)
        End If  '- cat1 totals.-
        s1 = "<drawline fontstyle=""bold"" />"
        mColReportLines.Add(s1)

        '-- Show final totals.
        sAmount = FormatCurrency(decFinalTotalCost, 2)
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & (k_TAB_STOCK_DESCRIPTION + 40) & """  fontstyle=""bold"" >Final Totals: </txt>"
        sLine &= "<txt TAB=""" & k_TAB_STOCK_EXTCOST_EX_TAX & """  align=""right""  fontstyle=""bold""  "
        sLine &= " width = """ & k_WIDTH_STOCK_EXT_AMOUNT & """  >" & sAmount & "</txt>"
        '-- total sell-
        sAmount = FormatCurrency(decFinalTotalSell, 2)
        sLine &= "<txt TAB=""" & k_TAB_STOCK_EXTSELL_EX_TAX & """  align=""right""  fontstyle=""bold""  "
        sLine &= " width = """ & k_WIDTH_STOCK_EXT_AMOUNT & """  >" & sAmount & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)
        s1 = "<drawline fontstyle=""bold"" />"
        mColReportLines.Add(s1)

        '-- send back result
        colReportLines = mColReportLines
        BuildReport = True

    End Function '-build report.
    '= = = = = = = = = = ====



End Class  '--clsWhatsInStock-
'= = = = = = = = =  = = = ==  = =
