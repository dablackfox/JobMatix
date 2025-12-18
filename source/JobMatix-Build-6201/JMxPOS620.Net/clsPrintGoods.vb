
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'== Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Threading

'==
'== -- Updated 3501.1217  17-Dec-2018=  
'==     -- New Class to print Goods Received Transaction.
'==
'==
'== -- Updated 4201.1027  27/28-Oct-2019=  
'==     -- NOW has option of PREVIEW..
'==     -- Add Goods_id to Header info...
'==
'= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


Public Class clsPrintGoods

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private mbIsSqlAdmin As Boolean = False

    Private msVersionPOS As String = ""

    Private msStaffName As String = ""
    '= Private mIntStaff_id As Integer = -1
    Private mImageUserLogo As Image
    Private msBusinessName As String

    Private msSelectedPrinterName As String = ""
    Private msSupplierName As String = ""
    Private msSupplierBarcode As String = ""


    '--  Main printer object..-- 
    '--    DOES ALL PRINTING..--
    Private WithEvents mPrintDocument1 As New System.Drawing.Printing.PrintDocument()

    '=Preview-
    '= Private WithEvents printPreview1 As PreviewPrintController

    '--3311.225= use the sub-class
    Private printPreviewDialog1 As New clsPrintPreviewDialogWithPrinter

    Private mColGoodsTransaction As Collection
    Private mColGoodsInfo As Collection
    Private mColGoodsItems As Collection

    Private mIntGoods_id As Integer = -1

    Private mIntOurOrder_id As Integer = -1
    Private msSupplierInvoiceNo As String = ""
    Private msOurOrderNoSuffix As String = ""
    Private msSupplierInvoiceDate As String = ""
    Private msPO_date_created
    Private msOriginatingStaffName As String = ""
    '- in progress=

    Private mbPrintingCompleted As Boolean = False
    Private mIntPageNo As Integer = 0
    Private msOurOrderNumberString As String = ""

    Private mIntNoItemsStillToPrint As Integer = 0
    Private mIntItemsPrintCount As Integer = 0
    Private mIntActuaItemCount As Integer = 0

    Private mDecTotal_ex As Decimal
    Private mDecTotalTax As Decimal
    Private mDecTotal_inc As Decimal

    '= = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-version-
    WriteOnly Property versionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
            '= labVersion.Text = msVersionPOS
        End Set
    End Property  '--version--
    '= = = = = = = = = = = = = = = = = = == 

    '-- Staff Name/Id now comes from caller..--

    WriteOnly Property BusinessName() As String
        Set(ByVal Value As String)
            msBusinessName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    WriteOnly Property SupplierName() As String
        Set(ByVal Value As String)
            msSupplierName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    WriteOnly Property SupplierBarcode() As String
        Set(ByVal Value As String)
            msSupplierBarcode = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    '-- Constructor- I n i t ----

    Public Sub New()
        MyBase.New()

        '= Call mbRefresh()   '--load current system info..--
    End Sub '--init..--
    '= = = = = = = = = = = = =
    '-===FF->

    '=3107.821- PO Printing Events..==
    '=3107.821- PO Printing Events..==

    '--  Main print Page EVENT handler..--
    '--  Main print Page EVENT handler..--
    '--   FOR PO PRINT FUNCTION..--

    Private Sub mPrintDocument1_PrintPage(ByVal sender As Object, _
                                               ByVal ev As PrintPageEventArgs) _
                                            Handles mPrintDocument1.PrintPage
        '= Const k_WIDTH_SUPCODE = 120      '--width of Barcode column.-
        Const k_WIDTH_BC = 120  '--width of barcode column.-
        Const k_WIDTH_SERIALS = 160  '--width of SerialNos column.-
        Const k_WIDTH_DESCR = 210  '--width of Description column.-
        Const k_WIDTH_TAX = 50     '--width of TAXcode column.-
        Const k_WIDTH_PRICE = 80   '--width of Price column.-
        Const k_WIDTH_QTY = 50      '--width of Qty column.-
        Const k_WIDTH_TOTAL = 90  '--width of Ext.Total column.-

        Const k_PRTWIDTH = 760
        Const k_LEFTMARGIN = 32

        Dim intGreyBGColour As Integer = &HE0E0E0&
        Dim fillColor As Color
        Dim intXpos, intXpos2, intYpos, intYpos2 As Integer
        Dim intYposTopDetails As Integer
        Dim intHdrX As Integer = 150
        Dim intHdrY As Integer = 50
        Dim intLeftMarg As Integer
        Dim L1, ix, x1, intError As Integer
        '= Dim rowSupplier, rowDetail, row1 As DataRow
        Dim rowDetail, row1 As DataRow
        Dim s1, sInvoiceNo, sDocType As String
        '== Dim sJobNo As String = "--"
        Dim datePO As DateTime
        Dim sSupplierBarcode, sSupplierInfo As String
        Dim sDelivery As String
        Dim font1 As userFontDef
        Dim intInfoBoxWidth As Integer = 88  '= 120
        Dim intInfoBoxDepth As Integer = 27
        Dim intAddressBoxWidth As Integer = 340
        Dim intAddressBoxDepth As Integer = 140
        Dim intTermsBoxDepth As Integer = 100
        Dim intXposRhsBox, intXposPrice, intXposExt As Integer
        '-grid-
        Dim intGridYpos, intGridYdepth As Integer
        Dim intLinesAvailable As Integer = 40
        Dim penGrid As Pen = Pens.LightGray

        '= MsgBox("Ready to set up print page for PO..")

        fillColor = ColorTranslator.FromOle(intGreyBGColour)
        '= rowSupplier = mDataTableSupplier.Rows(0)   '--only row..-

        '= MsgBox("Ready to print Invoice to " & msSelectedPrinterName & "..", MsgBoxStyle.Information)
        'sSupplierBarcode = rowSupplier.Item("barcode")
        'sSupplierInfo = rowSupplier.Item("contactName") & vbCrLf & rowSupplier.Item("supplierName") & vbCrLf & _
        '                      rowSupplier.Item("address") & vbCrLf & rowSupplier.Item("suburb") & vbCrLf & _
        '                      rowSupplier.Item("state") & "  " & rowSupplier.Item("postcode") & vbCrLf & _
        '                       rowSupplier.Item("country") & vbCrLf & _
        '                       vbCrLf & "Phone:  " & rowSupplier.Item("Phone")
        '= sDelivery = txtGoodsDeliveryAddress.Text
        sSupplierInfo = "Supplier: (" & msSupplierBarcode & ") " & msSupplierName

        '== On Error GoTo PrintInvoice_Error
        intLeftMarg = k_LEFTMARGIN '== iixels-  (16 * PRT_UNIT) '- 240 twips..-
        '--  paint BIZ logo  top left.--
        x1 = 0
        If Not (mImageUserLogo Is Nothing) Then
            x1 = mImageUserLogo.Width
            ev.Graphics.DrawImage(mImageUserLogo, intLeftMarg + k_PRTWIDTH - x1, 0)
        End If
        intXposRhsBox = k_LEFTMARGIN + k_PRTWIDTH - intAddressBoxWidth

        '-- print biz name-
        intXpos = intLeftMarg
        intHdrX = intLeftMarg
        intYpos = intHdrY

        font1.sName = "Lucida Sans"     '== "Tahoma" '== Printer.FontName = "Tahoma"
        font1.lngSize = 10
        font1.bBold = False  '--reset to normal IN CASE LEFT OVER..-
        font1.bUnderline = False
        font1.bItalic = False
        intYpos = gIntPrintTextString(ev, msBusinessName, intHdrX, intYpos, font1)
        intYpos += 4  '- move below main biz name..
        '-- draw line under main name---
        Call gIntDrawLine(ev, intLeftMarg, intYpos, k_PRTWIDTH - x1)  '-- don't draw over logo..
        intYpos += 16  '- move below line..
        intYposTopDetails = intYpos  '--save for top order details pos..
        msSupplierInvoiceNo = mColGoodsInfo.Item("invoice_no")
        msSupplierInvoiceDate = Format(mColGoodsInfo.Item("invoice_date"), "dd-MMM-yyyy")

        '-- Main biz Hdr stuff is on First page only..
        If (mIntPageNo <= 0) Then  '-first time-
            Try
                font1.lngSize = 9 '== Printer.FontSize = 8
                font1.bBold = True '== Printer.FontBold = True
                'intYpos = gIntPrintTextString(ev, "ABN: " & msBusinessDisplayABN, intHdrX, intYpos, font1)
                ''== intYpos = gIntPrintTextString(ev, msBusinessName, intHdrX, intYpos, font1)
                'intYpos = gIntPrintTextString(ev, msBusinessAddress1, intHdrX, intYpos, font1)
                'intYpos = gIntPrintTextString(ev, msBusinessAddress2, intHdrX, intYpos, font1)
                'intYpos = gIntPrintTextString(ev, "Phone:  " & msBusinessPhone, intHdrX, intYpos, font1)
                ''--blank line-
                'intYpos = gIntPrintTextString(ev, "", intHdrX, intYpos, font1)
                font1.lngSize = 18 '==Printer.FontSize = 18

                intYpos = gIntPrintTextString(ev, "Goods Received Invoice", intHdrX, intYpos, font1, textColour.orange)
                '-- Blank line..
                intYpos = gIntPrintTextString(ev, "", intHdrX, intYpos, font1)
                font1.lngSize = 9 '== Printer.FontSize = 8
                '-- Supplier-
                intYpos = gIntPrintTextString(ev, sSupplierInfo, intHdrX, intYpos, font1)
                intYpos = gIntPrintTextString(ev, _
                             "Supplier Invoice No: " & msSupplierInvoiceNo & "; " & msSupplierInvoiceDate, intHdrX, intYpos, font1)

                '= decTotalTax = 0
                msOurOrderNoSuffix = mColGoodsInfo.Item("orderNoSuffix")
                '-- Print PO No. and Date..
                Dim colGoodsPO As Collection = mColGoodsInfo.Item("Goods_PO")
                If (colGoodsPO.Count > 0) Then  '-have PO info0
                    msPO_date_created = Format(colGoodsPO.Item("order_date"), "dd-MMM-yyyy hh:mm tt")
                    mIntOurOrder_id = colGoodsPO.Item("order_id")
                    msOriginatingStaffName = colGoodsPO.Item("docket_name")
                End If
                intYpos2 = intYpos  '-- save Ypos forSupplier Info...
                '-- Print Order Info box-  top RHS..
                Dim intOrderBoxDepth = 120
                font1.bItalic = False
                intYpos = intYposTopDetails
                L1 = gIntPrintTextInBox(ev, "<b><ul>Invoice/Order Details:", _
                         intXposRhsBox, intYpos, 16, intAddressBoxWidth, intOrderBoxDepth, True, , 9)
                '-- Print order details..-
                intYpos += 36   '--space under header..
                intXpos = intXposRhsBox + 24
                font1.bBold = True

                '=4201.1028= Print   mIntGoods_id
                intYpos = gIntPrintTextString(ev, "POS Goods ID: " & mIntGoods_id, intXpos, intYpos, font1)

                '--order no. line..
                L1 = gIntPrintTextString(ev, "Order No: ", intXpos, intYpos, font1)
                font1.bBold = False
                msOurOrderNumberString = Format(mIntOurOrder_id, "   000") & "/" & msOurOrderNoSuffix
                intYpos = gIntPrintTextString(ev, msOurOrderNumberString, intXpos + 112, intYpos, font1)

                intYpos += 6   '--more space under liner..
                '= intXpos = intXposRhsBox + 24

                '--Created Date line.. msPO_date_created-
                font1.bBold = True
                L1 = gIntPrintTextString(ev, "Date Created: ", intXpos, intYpos, font1)
                font1.bBold = False
                s1 = msPO_date_created  '= Format(mDatePO_date_created, "dd-MMM-yyyy hh:mm tt")
                intYpos = gIntPrintTextString(ev, s1, intXpos + 112, intYpos, font1)

                '-- Expected --
                'Dim datex As Date = DateAdd(DateInterval.Day, mIntSupplierDeliveryDays, Date.Today)
                'font1.bBold = True
                'L1 = gIntPrintTextString(ev, "Date Expected: ", intXpos, intYpos, font1)
                'font1.bBold = False
                's1 = Format(datex, "dd-MMM-yyyy ")
                'intYpos = gIntPrintTextString(ev, s1, intXpos + 112, intYpos, font1)

                '--Created by. line..
                font1.bBold = True
                L1 = gIntPrintTextString(ev, "Created By: ", intXpos, intYpos, font1)
                font1.bBold = False
                intYpos = gIntPrintTextString(ev, msOriginatingStaffName, intXpos + 112, intYpos, font1)

                '- print Supplier Name/address in box..--
                intYpos2 = intYpos  '-- set Ypos for Cust...

                '-  ???    mDefaultUserFont.lngSize = 9

                intXpos = intLeftMarg
                'If Trim(txtGoodsComments.Text) <> "" Then
                '    sDelivery &= vbCrLf & "<b>Comments:" & vbCrLf & txtGoodsComments.Text
                'End If
                'L1 = gIntPrintTextString(ev, "To:", intXpos + 60, intYpos + 59, font1)
                'L1 = gIntPrintTextInBox(ev, sSupplierInfo, _
                '                  intXpos, intYpos2 + 44, 85, intAddressBoxWidth, intAddressBoxDepth, True, , 9)

                ''-- Delivery-- 9pt Font-
                'L1 = gIntPrintTextInBox(ev, "<b>Deliver To:" & vbCrLf & sDelivery, _
                '   intXpos + k_PRTWIDTH - intAddressBoxWidth, intYpos2 + 44, 6, _
                '                                         intAddressBoxWidth, intAddressBoxDepth, True, , 9)
                '-hdr done.
                intLinesAvailable = 46  '= 18
                intGridYdepth = 720
                '-- GRID for Detail Lines..--
                intGridYpos = intYpos2 + 44  '= + intAddressBoxDepth + 12

            Catch ex As Exception
                MsgBox("Error printing GOODS header." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                ev.HasMorePages = False  '--all done-
                Exit Sub
            End Try

        Else  '- second and subseq. pages.
            intLinesAvailable = 52
            intGridYdepth = 750
            font1.lngSize = 9
            '-- Supplier-
            intYpos = gIntPrintTextString(ev, sSupplierInfo, intHdrX, intYpos, font1)
            intYpos = gIntPrintTextString(ev, _
                                   "Supplier Invoice No: " & msSupplierInvoiceNo & "; " & msSupplierInvoiceDate, intHdrX, intYpos, font1)
            intYpos = gIntPrintTextString(ev, "Order No: " & msOurOrderNumberString, intLeftMarg + 480, intYpos, font1)

            '-- GRID for Detail Lines..--
            intGridYpos = intYpos + 36
        End If  '-first page--

        '- All pages..-
        mIntPageNo += 1

        intXpos = intLeftMarg
        '--draw the "grid"..
        Call gIntDrawLine(ev, intXpos, intGridYpos, k_PRTWIDTH)  '--top bar-
        '--column lines- 8 spaces, nine lines-
        Dim arrayIntWidths() As Integer = _
                      {k_WIDTH_BC, k_WIDTH_SERIALS, k_WIDTH_DESCR, k_WIDTH_TAX, k_WIDTH_PRICE, k_WIDTH_QTY, k_WIDTH_TOTAL}
        For ix = 0 To UBound(arrayIntWidths)
            ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)
            intXpos += arrayIntWidths(ix)
        Next ix
        '--last vert line..
        ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)

        intXpos = intLeftMarg
        Call gIntDrawLine(ev, intXpos, intGridYpos + intGridYdepth, k_PRTWIDTH)  '--bottom bar-

        '-- column header text line..
        '-- Can't use TAB stops because some items are Left-just and some are right..-
        '-- fill column headers BG..
        Const k_HDR_HEIGHT = 22
        intXpos = intLeftMarg
        ev.Graphics.FillRectangle(New SolidBrush(fillColor), intXpos, intGridYpos, k_PRTWIDTH, k_HDR_HEIGHT)
        '-- box for col. hdrs. text-
        '=Dim rectHdr As New RectangleF(intXpos, intGridYpos, k_WIDTH_BC, k_HDR_HEIGHT)

        '-- PRINT column header TEXTS..
        '-- PRINT column header TEXTS..
        font1.lngSize = 8
        font1.bBold = True
        'Call gIntPrintTextInRectangle(ev, " Sup.Code", intXpos, intGridYpos, _
        '                                         k_WIDTH_BC, k_HDR_HEIGHT, font1, textColour.black, False, True)
        ''== e.Graphics.DrawString("Bar Code", drawFont, drawBrush, drawRect, drawFormat)
        'intXpos += k_WIDTH_SUPCODE
        Call gIntPrintTextInRectangle(ev, " Barcode", intXpos, intGridYpos, _
                                                 k_WIDTH_BC, k_HDR_HEIGHT, font1, textColour.black, False, True)
        intXpos += k_WIDTH_BC
        Call gIntPrintTextInRectangle(ev, " Serial-Nos", intXpos, intGridYpos, _
                                                 k_WIDTH_SERIALS, k_HDR_HEIGHT, font1, textColour.black, False, True)
        intXpos += k_WIDTH_SERIALS
        Call gIntPrintTextInRectangle(ev, " Description", intXpos, intGridYpos, _
                                                  k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False, True)
        intXpos += k_WIDTH_DESCR
        Call gIntPrintTextInRectangle(ev, "Tax", intXpos, intGridYpos, _
                                        k_WIDTH_TAX, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
        intXpos += k_WIDTH_TAX
        intXposPrice = intXpos  '--save for totals..-
        Call gIntPrintTextInRectangle(ev, "Cost_inc", intXpos, intGridYpos, _
                                       k_WIDTH_PRICE, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
        intXpos += k_WIDTH_PRICE
        Call gIntPrintTextInRectangle(ev, "Qty", intXpos, intGridYpos, _
                                         k_WIDTH_QTY, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
        intXpos += k_WIDTH_QTY
        intXposExt = intXpos   '-save-
        L1 = gIntPrintTextInRectangle(ev, "Total_inc", intXpos, intGridYpos, _
                                         k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
        '-- L1 has line height.
        '-
        '- We wouldn't be here if there was nothing left to print..-
        font1.bBold = False
        intYpos = intGridYpos + k_HDR_HEIGHT + L1 '--vert. pos. to 1st detail line..-
        '-- make line rect deep enough for two lines..
        Dim intItemLineHeight As Integer = k_HDR_HEIGHT + (k_HDR_HEIGHT \ 4)
        Dim intCurrentYpos As Integer   '--save for shading-
        Dim colItem, colSerials As Collection
        Dim decItemCost_ex, decItemTax, decItemCost_inc

        font1.lngSize = 9
        '--Print Actual detail lines..
        '--Print Actual detail lines..
        While (intLinesAvailable > 0) And (mIntNoItemsStillToPrint > 0)
            '- Fill page or run out all items..
            intXpos = intLeftMarg
            intCurrentYpos = intYpos  '--save for shading-
            '-- print items with '-no box-/vertical align at top.
            '=With clsDgvGoodsItems.Rows(mIntGridLinesPrintCount)
            '= mIntItemsPrintCount += 1
            colItem = mColGoodsItems.Item(mIntItemsPrintCount + 1)  '-get next-

            If (colItem.Count > 0) Then  '= (.Cells(k_GRIDCOL_BARCODE).Value <> "") Then  '--not empty-
                '-- get serials, if any, and compute lines needed, and if can fit on current page..
                colSerials = colItem.Item("serials")
                '-- Check if we can fit Serials list..
                If (colSerials.Count > intLinesAvailable) Then
                    '-- drop out, print this item on next page..
                    Exit While
                End If
                '--  shade alternate rows-
                If (mIntItemsPrintCount Mod 2) = 1 Then
                    ev.Graphics.FillRectangle(New SolidBrush(ColorTranslator.FromOle(&HF0F0F0)), _
                                                         intLeftMarg, intCurrentYpos, k_PRTWIDTH, k_HDR_HEIGHT)
                End If
                'Call gIntPrintTextInRectangle(ev, colItem.Item("sup_code"), intXpos, intYpos, _
                '                             k_WIDTH_SUPCODE, intItemLineHeight, font1, textColour.black, False, , , True) '=-top-
                'intXpos += k_WIDTH_SUPCODE
                Call gIntPrintTextInRectangle(ev, colItem.Item("barcode"), intXpos, intYpos, _
                                            k_WIDTH_BC, intItemLineHeight, font1, textColour.black, False, , , True) '-no box-
                intXpos += k_WIDTH_BC
                '-print first serialNo if any.
                If (colSerials.Count > 0) Then
                    Call gIntPrintTextInRectangle(ev, "1:  " & colSerials.Item(1), intXpos, intYpos, _
                                                k_WIDTH_SERIALS, intItemLineHeight, font1, textColour.black, False, , , True) '-no box-
                End If  '-count-
                intXpos += k_WIDTH_SERIALS

                Call gIntPrintTextInRectangle(ev, colItem.Item("description"), intXpos, intYpos, _
                                             k_WIDTH_DESCR, intItemLineHeight, font1, textColour.black, False, , , True) '-no box-
                intXpos += k_WIDTH_DESCR
                Call gIntPrintTextInRectangle(ev, colItem.Item("goods_taxCode"), intXpos, intYpos, _
                                           k_WIDTH_TAX, intItemLineHeight, font1, textColour.black, False, , , True) '-no box-
                intXpos += k_WIDTH_TAX
                s1 = FormatCurrency(colItem.Item("cost_inc"), 2)
                Call gIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                           k_WIDTH_PRICE, intItemLineHeight, font1, textColour.black, True, , , True) '-no box-
                intXpos += k_WIDTH_PRICE
                Call gIntPrintTextInRectangle(ev, colItem.Item("quantity"), intXpos, intYpos, _
                                           k_WIDTH_QTY, intItemLineHeight, font1, textColour.black, True, , , True) '-no box-
                intXpos += k_WIDTH_QTY
                s1 = FormatCurrency(colItem.Item("total_inc"), 2)
                L1 = gIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                          k_WIDTH_TOTAL, intItemLineHeight, font1, textColour.black, True, , , True) '-no box-
                mIntActuaItemCount += 1

                '--  Print SerialNos (if any) under Barcode.
                '= intYpos += intItemLineHeight  '=get under barcode

                If (colSerials.Count > 1) Then
                    intXpos = intLeftMarg + k_WIDTH_BC
                    For ixs As Integer = 2 To colSerials.Count
                        intYpos += L1
                        Call gIntPrintTextInRectangle(ev, CStr(ixs) & ":  " & colSerials.Item(ixs), intXpos, intYpos, _
                                               k_WIDTH_SERIALS, intItemLineHeight, font1, textColour.black, False, , , True) '-no box-
                        intLinesAvailable -= 1
                    Next ixs
                End If  '-count-
            End If  '-empty row-
            '= End With  '-row-
            intYpos += intItemLineHeight  '=L1
            intLinesAvailable -= 1
            mIntItemsPrintCount += 1
            mIntNoItemsStillToPrint -= 1
            '= mIntGridLinesPrintCount += 1  '-starts from zero--
            If mIntNoItemsStillToPrint <= 0 Then
                '--all done
                '-- show totals-- JUST below grid..-
                intYpos = intGridYpos + intGridYdepth
                intYpos += 10
                font1.bBold = True
                font1.lngSize = 9
                Call gIntDrawLine(ev, intXposPrice, intYpos, k_WIDTH_PRICE + k_WIDTH_QTY + k_WIDTH_TOTAL)
                intYpos += 10
                L1 = gIntPrintTextInRectangle(ev, "Totals-  No. Items = " & mIntActuaItemCount & "..", _
                                            intXposPrice, intYpos, _
                                            k_WIDTH_PRICE + k_WIDTH_QTY + k_WIDTH_TOTAL, k_HDR_HEIGHT, _
                                               font1, textColour.black, False, False) '-no rt Align.-
                intYpos += L1 + 8
                '-- subtotal-
                Call gIntPrintTextInRectangle(ev, "Total (Ex)", _
                                           intXposPrice, intYpos, _
                                           k_WIDTH_PRICE + k_WIDTH_QTY, k_HDR_HEIGHT, _
                                              font1, textColour.black, False, False) '-no rt Align.-
                s1 = FormatCurrency(mDecTotal_ex, 2)
                L1 = gIntPrintTextInRectangle(ev, s1, intXposExt, intYpos, _
                                        k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-no box-
                intYpos += L1
                '-- tax-
                Call gIntPrintTextInRectangle(ev, "Tax: ", _
                                           intXposPrice, intYpos, _
                                           k_WIDTH_PRICE + k_WIDTH_QTY, k_HDR_HEIGHT, _
                                              font1, textColour.black, False, False) '-Right Align.-
                s1 = FormatCurrency(mDecTotalTax, 2)
                L1 = gIntPrintTextInRectangle(ev, s1, intXposExt, intYpos, _
                                        k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-no box-
                intYpos += L1
                '-- TOTAL-
                Call gIntPrintTextInRectangle(ev, "Total (inc): ", _
                                           intXposPrice, intYpos, _
                                           k_WIDTH_PRICE + k_WIDTH_QTY, k_HDR_HEIGHT, _
                                              font1, textColour.black, False, False) '-Right Align.-
                s1 = FormatCurrency(mDecTotal_inc, 2)
                L1 = gIntPrintTextInRectangle(ev, s1, intXposExt, intYpos, _
                                        k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-no box-
                intYpos += L1
                Call gIntDrawLine(ev, intXposPrice, intYpos + 12, k_WIDTH_PRICE + k_WIDTH_QTY + k_WIDTH_TOTAL)
                ev.HasMorePages = False  '--all done-
            Else  '- more to come-
                ev.HasMorePages = True   '- come back for more--
                '= Exit While
            End If
        End While '-lines-

        '-- Footer -- every page-
        Dim strPageNo As String = "Page: " & CStr(mIntPageNo)

        Call gbPageFooter(ev, msVersionPOS, strPageNo)


    End Sub 'page event..-
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    '-- End pribt event..
    '-- Signal competion..

    Private Sub mPrintDocument1_EndPrint(ByVal sender As Object, _
                                           ByVal ev As PrintEventArgs) _
                                             Handles mPrintDocument1.EndPrint

        '=MsgBox("Printing Completed", MsgBoxStyle.Information)  '-test-
        mbPrintingCompleted = True

    End Sub  '-end print-
    '= = = = = = = = = = = = = = = = =

    '-- Print Goods Recvd Trans.

    Public Function PrintGoodsReceived(ByRef colGoodsTransaction As Collection, _
                                       ByVal strPrinterName As String, _
                                       Optional ByVal bPreviewOnly As Boolean = False) As Boolean

        PrintGoodsReceived = False
        msSelectedPrinterName = strPrinterName
        mbPrintingCompleted = False
        mColGoodsTransaction = colGoodsTransaction

        mColGoodsInfo = mColGoodsTransaction.Item("Goods_Info")
        mIntGoods_id = mColGoodsInfo.Item("Goods_id")

        mIntPageNo = 0
        If (mColGoodsInfo Is Nothing) OrElse (mColGoodsInfo.Count <= 0) Then
            Exit Function
        End If
        mColGoodsItems = mColGoodsInfo.Item("items")
        mIntNoItemsStillToPrint = mColGoodsItems.Count

        msSupplierName = mColGoodsTransaction.Item("SupplierName")
        msSupplierBarcode = mColGoodsTransaction.Item("SupplierBarcode")
        msBusinessName = mColGoodsTransaction.Item("BusinessName")

        mIntItemsPrintCount = 0

        mDecTotal_ex = mColGoodsInfo.Item("total_ex")
        mDecTotalTax = mColGoodsInfo.Item("total_tax")
        mDecTotal_inc = mColGoodsInfo.Item("total_inc")

        '-- go--
        '==
        '== -- Updated 4201.1027  27/28-Oct-2019=  
        '==     -- NOW has option of PREVIEW..
        If bPreviewOnly Then
            Try
                '--  preview selected..- Set parms..-
                mPrintDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
                '-get screen size.
                Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
                Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

                '-  PREVIEW requested- 
                printPreviewDialog1.Document = mPrintDocument1
                printPreviewDialog1.Top = 0
                printPreviewDialog1.Left = 300
                printPreviewDialog1.Height = screenHeight - 120  '=800
                printPreviewDialog1.Width = 840
                '=4201.0623-  make intitallyy 100%..
                printPreviewDialog1.PrintPreviewControl.Zoom = 0.9
                printPreviewDialog1.PrintPreviewControl.UseAntiAlias = True
                '--  start the preview..--
                printPreviewDialog1.ShowDialog()
                PrintGoodsReceived = True
            Catch ex As Exception
                '== MessageBox.Show(ex.Message)
                MsgBox("Error in PrintGoodsReceived Report preview.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                PrintGoodsReceived = False
            End Try  '- prewview-

        Else  '-normal straight print.
            Try
                '--  set printer selected..--
                mPrintDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
                '--  start the printer..--
                mbPrintingCompleted = False
                mPrintDocument1.Print()
                While Not mbPrintingCompleted
                    Thread.Sleep(500)
                End While
                PrintGoodsReceived = True
            Catch ex As Exception
                '== MessageBox.Show(ex.Message)
                MsgBox("Error in printing GoodsRecvd Trans..." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                '== PrintSalesInvoice = False
            End Try
        End If  '-preview-

    End Function  '-PrintGoodsReceived-
    '= = = = = = = = = = == = =  == = =

End Class  '-clsPrintGoods--
'= = = = = = = = = = ==  ==
