
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
Imports System.Threading

Public Class clsStockBarcodeList

    '==
    '==
    '==  Target-New-Build-4257..
    '==  Target-New-Build-4257..
    '==
    '==   1. MAIN REASON is to Print list of Stock Table Items with barcode in Barcode Font..
    '==            -- ADDS NEW CLASS clsStockBarcodeList..
    '==            --  Also adds extra checkbox to Report Stock Options "Don't include stock with postive qtyInStock." 
    '==
    '==   Created.- Build 4257..  Started 02-July-2020= 
    '==     --  Called from main Reports Form.. 
    '==
    '== = = = = = = = = = = = = = = = = =  = = = = = = = = = = = = = == 

    '--  FOR vb.net:
    Const PRT_UNIT = 1

    '--  Main printer object..-- 
    '--    DOES ALL PRINTING..--
    Private WithEvents mPrintDocument1 As New System.Drawing.Printing.PrintDocument()
  
    '-- use the sub-class
    Private printPreviewDialog1 As New PrintPreviewDialogWithPrinter

    Private mbPrintingCompleted As Boolean = False

    '--  STATIC VARS for all printing..-  
    Private mDefaultUserFont As userFontDef
    Private mlPrtWidth As Integer '--pixels or twips (11,000).-

    '-- Top After Header-
    Private mIntMainDetailTop As Integer = 0

    '--  SAVE STATE..--
    '--   JOB MAINT page state vars..--
    '--   JOB MAINT page state vars..--
    Private mIntMaintPage As Short
    '==3311.507= Private mIntLineCount As Short
    Private mIntItemCount As Integer  '--count items printed..-

    '--  save doc-type currently being printed..-- 
    '= Private mlPrintDocType As printDocType '== = -1

    '--labels- 
    Private miLineCount As Integer = 0
    Private mIntPageNo As Integer = 0


    Private msSqlDbName As String = ""
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--

    Private mFrmReportParent1 As Form

    Private mColReportLines As Collection

    Private msVersionPOS As String = ""
    Private msStaffname As String = ""

    Private mStrPrinterName As String = ""
    Private mbPreviewOnly As Boolean

    Private msItemBarcodeFontName As String = ""
    Private mIntItemBarcodeFontSize As Integer = 9

    Private mDtStockItems As DataTable

    Private fontHdr As Font
    Private fontText As Font
    Private fontBarcode As Font

    '= = = = = = = =  = = = = = = = = = =
    'msItemBarcodeFontName = mClsSystemInfo.item("ITEMBARCODEFONTNAME")

    '--  SAMPLE texts..
    's1 = mClsSystemInfo.item("ITEMBARCODEFONTSIZE")
    'If IsNumeric(s1) Then
    '   Dim L1 As Integer = CInt(s1)
    '   If (L1 > 3) And (L1 < 36) Then
    '     mlItemBarcodeFontSize = L1
    '   End If
    'End If
    '= = = = = = = = = =  = = =

    ''-- Print the barcode..-  Keep on same line..
    'lngXpos = mlPrtDx(64) '= Printer.CurrentX = 960
    'font1.sName = msItemBarcodeFontName '==  Printer.FontName = msItemBarcodeFontName
    'font1.lngSize = mlItemBarcodeFontSize '==  Printer.FontSize = mlItemBarcodeFontSize
    ''==  Printer.Print "*" & sBarcode & "*";
    'Ly = lngYpos
    ''--  PRINT BARCODE barcode..- Keep Ypos..
    'Call mlPrintTextString(ev, "*" & sBarcode & "*", lngXpos, lngYpos, font1)
    '-- (gIntPrintTextString)
    '= = = =  == = = = = = = = ==  == = =

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
        'Dim s1, s2, s3, sSql, sUpdates As String
        'Dim rx, L1 As Integer

        '--save -
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName

        mColSqlDBInfo = colSqlDBInfo

    End Sub  '-new-
    '= = = =  = = = == = =
    '= = = = = = = =  = = = = = = = = = =
    '-===FF->

    '-- print Document Page print Handler..

    '--  Main print Page EVENT handler..--
    '--  Main print Page EVENT handler..--
    '--   FOR PO PRINT FUNCTION..--

    Private Sub mPrintDocument1_PrintPage(ByVal sender As Object, _
                                               ByVal ev As PrintPageEventArgs) _
                                                 Handles mPrintDocument1.PrintPage
        Const k_WIDTH_SUPCODE = 120      '--width of Barcode column.-
        Const k_WIDTH_BC = 120  '--width of Description column.-
        Const k_WIDTH_DESCR = 240  '--width of Description column.-
        Const k_WIDTH_TAX = 50     '--width of TAXcode column.-
        Const k_WIDTH_PRICE = 80   '--width of Price column.-
        Const k_WIDTH_QTY = 50      '--width of Qty column.-
        Const k_WIDTH_TOTAL = 100  '--width of Ext.Total column.-

        Const k_PRTWIDTH = 760
        Const k_LEFTMARGIN = 20

        '= Dim font1 As userFontDef
        Dim txtColour1 As textColour

        Dim intYpos, intXpos, intLy As Integer
        '==3311.507= 
        Dim intPageLineCount As Integer = 0  '--itms on page.-
        Dim intMaxItemsPerPage As Integer = 14

        '-- A Rectangle has Left, Top, Width, height--
        '== 4219.1119 19-Nov-2019= 
        Dim rectPageBounds As Rectangle = ev.PageBounds    '-- printable rectangle-
        Dim intPrintHeight As Integer = rectPageBounds.Height
        Dim intMainDetailBoxDepth As Integer = 880
 
        Dim sCat1, sCat2 As String
        Dim sDescr, sBarcode As String
        Dim sStockId, sLine As String
        Dim sQty As String
        Dim dataRow1 As DataRow
        Dim myBrush = New SolidBrush(Color.Black)
        Dim myStringFormat As New StringFormat

        fontHdr = New Font("Lucida Sans Unicode", 11, FontStyle.Regular)
        fontText = New Font("Lucida Sans Unicode", 9, FontStyle.Regular)
        '- For Win-7 problem.
        Try
            fontBarcode = New Font(msItemBarcodeFontName, mIntItemBarcodeFontSize, FontStyle.Regular)
        Catch ex As Exception
            MsgBox("Error creating new Barcode Font Object." & vbCrLf & _
                      ex.Message & vbCrLf & _
                      "  Barcode Font is: " & msItemBarcodeFontName & vbCrLf & _
                      "  Barcode Font Size is: " & mIntItemBarcodeFontSize, MsgBoxStyle.Exclamation)
            ev.HasMorePages = False
            Exit Sub
        End Try

        Dim lineHeight As Single = 0

        lineHeight = fontBarcode.GetHeight(ev.Graphics)

        myStringFormat.LineAlignment = StringAlignment.Center  '--vertical-
        mIntPageNo += 1

        '-- print page heading...
        intYpos = 40  '-temp top line..
        intXpos = k_LEFTMARGIN

        sLine = "== " & msSqlDbName & " Stock Barcode Listing-   Page: " & CStr(mIntPageNo) & "."
        ev.Graphics.DrawString(sLine, fontHdr, myBrush, intXpos, intYpos, myStringFormat)

        intYpos += 50
        '-- print all items..
        If (mDtStockItems IsNot Nothing) AndAlso (mDtStockItems.Rows.Count > 0) Then
            While (mIntItemCount < mDtStockItems.Rows.Count) And (intPageLineCount < intMaxItemsPerPage)
                dataRow1 = mDtStockItems.Rows(mIntItemCount)

                sBarcode = CStr(dataRow1.Item("barcode"))
                sCat1 = CStr(dataRow1.Item("cat1"))
                sCat2 = CStr(dataRow1.Item("cat2"))
                sDescr = CStr(dataRow1.Item("description"))
                sQty = CStr(dataRow1.Item("qtyInStock"))

                intXpos = k_LEFTMARGIN

                '-- Print the barcode as text....  keep ypos.
                intLy = intYpos

                sLine = RSet(CStr(mIntItemCount + 1), 4) & ". " & LSet(sCat1, 6) & "." & _
                                              LSet(sCat2, 6) & LSet(".[" & sBarcode & "] " & sDescr, 34)
                ev.Graphics.DrawString(sLine, fontText, myBrush, intXpos, intLy, myStringFormat)

                sLine = " Qty: " & sQty
                ev.Graphics.DrawString(sLine, fontText, myBrush, intXpos + 378, intLy, myStringFormat)

                '-- Print the barcode itself..-  Keep on same line..
                sLine = "*" & sBarcode & "*"
                ev.Graphics.DrawString(sLine, fontBarcode, myBrush, intXpos + 450, intYpos, myStringFormat)

                '=intYpos += 60  '--temp..
                '-new vert pos-
                intYpos = Convert.ToInt32(intYpos + lineHeight) + 24

                intPageLineCount += 1
                mIntItemCount += 1

                If (mIntItemCount >= mDtStockItems.Rows.Count) Then
                    '-done-
                    ev.HasMorePages = False
                    Exit While
                ElseIf (intPageLineCount >= intMaxItemsPerPage) Then
                    ev.HasMorePages = True
                    Exit While
                Else
                    '-- keep going.
                End If
            End While '-mIntItemCount-

            '-- done all items-
            If Not ev.HasMorePages Then
                '-new vert pos was done.-
                '= intYpos = Convert.ToInt32(intYpos + lineHeight) + 12
                sLine = "== The End == "
                ev.Graphics.DrawString(sLine, fontText, myBrush, intXpos + 30, intYpos, myStringFormat)
            End If

        Else  '-nothing-
            MsgBox("No stock data..", MsgBoxStyle.Exclamation)
            ev.HasMorePages = False
        End If  '-nothing-

    End Sub  '-print page-
    '= = = = = = = =  = = = = = = = = = =
    '-===FF->

    '-- End pribt event..
    '-- Signal competion..

    Private Sub mPrintDocument1_EndPrint(ByVal sender As Object, _
                                           ByVal ev As PrintEventArgs) _
                                             Handles mPrintDocument1.EndPrint

        '=MsgBox("Printing Completed", MsgBoxStyle.Information)  '-test-
        mbPrintingCompleted = True
        '-- for hard copy..
        mIntPageNo = 0
        mIntItemCount = 0


    End Sub  '-end print-
    '= = = = = = = = = = = = = = = = =

    '-- print barcode list..

    Public Function printStockBarcodeList(ByVal bShowNegativeStock As Boolean, _
                                          ByVal bShowZeroStock As Boolean, _
                                          ByVal bShowPositiveStock As Boolean, _
                                          ByVal strPrinterName As String, _
                                          ByVal sItemBarcodeFontName As String, _
                                          ByVal intItemBarcodeFontSize As Integer, _
                                          Optional ByVal bPreviewOnly As Boolean = True) As Boolean

        Dim sWhere As String = ""
        Dim sSQL, s1 As String
        '= Dim dtStockItems As DataTable
        Dim sOrderBy As String

        printStockBarcodeList = False

        mStrPrinterName = strPrinterName
        mbPreviewOnly = bPreviewOnly

        msItemBarcodeFontName = sItemBarcodeFontName
        mintItemBarcodeFontSize = intItemBarcodeFontSize

        '-- Get table of stock Items..

        sSQL = "SELECT  description, cat1, cat2, barcode, stock_id, qtyInStock,  "
        sSQL &= "   costExTax AS cost_ex_$, sellExTax AS sell_ex_$,  "
        sSQL &= "    (costExTax *qtyInStock) AS ExtCost_ex_$, "
        sSQL &= "    (sellExTax *qtyInStock) AS ExtSell_ex_$ "
        sSQL &= " FROM dbo.stock "

        'If (Not bShowZeroStock) And (Not bShowNegativeStock) Then
        '    sWhere = " (qtyInStock >0)  "  '- no zero or neg stock wanted.
        'ElseIf (Not bShowZeroStock) And bShowNegativeStock Then
        '    sWhere = " (qtyInStock <0) "  '- no zero, but neg stock wanted.
        'Else  '=If not chkNoReportIfZeroStock.Checked And not chkNoReportIfNegStock.Checked Then
        '    sWhere = ""  '--include everything..
        'End If

        '-re-do-
        If bShowNegativeStock Then
            sWhere = " (qtyInStock <0) "  '- neg stock wanted.
        End If
        If bShowZeroStock Then
            If (sWhere <> "") Then
                sWhere &= " OR "
            End If
            sWhere &= " (qtyInStock =0) "  '- zero stock wanted.
        End If
        If bShowPositiveStock Then
            If (sWhere <> "") Then
                sWhere &= " OR "
            End If
            sWhere &= " (qtyInStock >0) "  '- zero stock wanted.
        End If
        '-ok-

        '--TEMP test all-
        '== sWhere = ""  '--include everything..

        sOrderBy = "cat1, cat2, barcode"

        If (sWhere <> "") Then
            sSQL &= " WHERE (" & sWhere & ") "
        Else  '-all checked to exclude.
            MsgBox("Error-  Nothing left to SELECT for Printing. " & vbCrLf & _
                     " (All checked to be excluded.). ", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        sSQL &= " ORDER BY " & sOrderBy & " ;"

        '- get Query result and load datagrid..-
        If Not gbGetDataTable(mCnnSql, mDtStockItems, sSQL) Then
            MsgBox("Error in SELECT for Stock table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        End If  '-get-
        '- ok-

        If (mDtStockItems Is Nothing) Then
            MsgBox("Error in SELECT for Stock table: " & vbCrLf & _
                                "Datatable object is NOTHING. ", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If (mDtStockItems.Rows.Count <= 0) Then
            MsgBox("No items to report in Stock table: " & vbCrLf & _
                                "Datatable is Empty. ", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        mIntPageNo = 0
        mIntItemCount = 0
        '-- go--

        If bPreviewOnly Then
            Try
                '--  set printer selected..--
                mPrintDocument1.PrinterSettings.PrinterName = mStrPrinterName
                '--  start the printer..--
                '-  PREVIEW requested- 
                printPreviewDialog1.Document = mPrintDocument1
                printPreviewDialog1.Height = 1200
                printPreviewDialog1.Width = 800
                printPreviewDialog1.PrintPreviewControl.UseAntiAlias = True
                '-4201.1027=
                printPreviewDialog1.PrintPreviewControl.Zoom = 0.95
                printPreviewDialog1.ShowDialog()
                printStockBarcodeList = True
            Catch ex As Exception
                MsgBox("Error in Print Barcodes preview.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                printStockBarcodeList = False
            End Try  '-preview-

        Else '-no preview-
            Try
                '--  set printer selected..--
                '-- FIX to 4219.1216 !!! --   mPrintDocument1.PrinterSettings.PrinterName = msInvoicePrinterName
                '-- FIX to 4219.1216 !!! --
                mPrintDocument1.PrinterSettings.PrinterName = mStrPrinterName
                '--  start the printer..--
                mbPrintingCompleted = False
                mPrintDocument1.Print()
            Catch ex As Exception
                '== MessageBox.Show(ex.Message)
                MsgBox("Error in printing Barcode List.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                '== PrintSalesInvoice = False
            End Try
        End If

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '=3411.0110=- wait for Completion..
        Thread.Sleep(1500)  '--milliseconds..-
        Dim intStart, intFinish As Integer
        intStart = CInt(VB.Timer)
        intFinish = intStart + 60  '= 20
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        While (Not mbPrintingCompleted) And (CInt(VB.Timer) < intFinish)
            DoEvents()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Thread.Sleep(1000)  '--milliseconds..-
        End While
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '=4201.0929==  Check for completion..
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        If (Not mbPrintingCompleted) Then
            MsgBox("Error- Stock Printing not completed..", MsgBoxStyle.Exclamation)
            '=PrintSalesInvoice = False
            '= labCreatingPDF.Visible = False
            Exit Function
        End If
        '-- Print Completed may happen, but File still not finished.


    End Function  '-printStockBarcodeList-
    '= = = = = = = =  = = = = = = = = = =
  

End Class  '-clsStockBarcodeList-
'= = = = = = = = = = = = = = ==  =
