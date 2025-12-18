
Option Strict Off
Option Explicit On
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Drawing2D
Imports System.Math
Imports VB = Microsoft.VisualBasic

'== Created 05-Sep-2016-  For JobReports Upgrade..
'==
'==     v3.3.3301.905..  05-Sep-2016= ===
'==       >> started..
'== 
'==  NEW POS Build..
'==
'==     v3.3.3311.0225..  25-Feb-2017= ===
'==       >> Report Printer class.  ADD sub-classed printPreviewDialog to catch Print Button 
'==                              and show print Dialog to choose printer...
'==
'==     v3.4.3403.1110..  10-Nov-2017= ===
'==       >> Report Printer class.  ADD OPTION for Fixed Pitch content.
'==
'==     v3.5.3519.0115  15-Jan-2019==
'==       >> Report Printer class.  ADD OPTION for "<MINPAGEROOM LINES= n  />"
'==         To force NEW Page if lines not avail.
'==
'==     v3.5.3519.0404  03-Apr-2019==
'==       >> Report Printer class.  ADD OPTION for SECOND header Line.
'==              AND small Gap..
'==
'==    -- 4201.0618/0623.  11/18/21-June-2019-   
'==         --  clsReportPrinter-  Fix starting zoom to 100%... 
'==
'==
'= = = =  = = = = = = = = = = = = = = == = =  = = = = == = = = = = = = = = = = = = = = = = =


'-  https://social.msdn.microsoft.com/Forums/vstudio/en-US/4e6e60f8-55fe-4a14-848f-c8c1103864ff/printpreviewdialog-how-can-i-add-a-button-to-select-a-printer

'= Here is what I did to fix the problem.

'=    I created a class called PrintPreviewDialogSelectPrinter.  In it I have two subs. 
'== Basically, the class inherits the PrintPreviewDialog, adds a button that functions as I want, 
'--  and removes the button that doesn't.  I finally found a sample online that I could understand.  
'==   I'm sure there are better ways of doing this, but it works very well.  Call it just like PrintPreviewDialog.

'= Very good solution. Thanks a lot.

'========  Mod to above..==

'= I allow myself some remarks to the code:
'= 1. Moving the initialising code to sub New() prevents errors at second call
'= 2. It is enough, to add a handler to the existing button. One may not exchange it.
'= 3. It is usefull, to close the dialog, wenn job is done.

'= So that is what I use:

Friend Class PrintPreviewDialogWithPrinter
    Inherits PrintPreviewDialog

    Private ts As ToolStrip
    Private printItem, myPrintItem As ToolStripItem

    Public Sub New()
        '=Dim PrintButton As ToolStripItem = ts.Items("printToolStripButton")

        '-- grh-  can't add handler..
        '-- must override-
        '--  https://msdn.microsoft.com/en-us/library/aa290043(v=vs.71).aspx
        '== AddHandler PrintButton.Click, AddressOf MyPrintItemClicked

        '--SO- use J.mohson.88 method to add new button.
        'Get the toolstrip from the base control
        ts = CType(Me.Controls(1), ToolStrip)
        'Get the print button from the toolstrip
        printItem = ts.Items("printToolStripButton")

        'Add a new button 
        With printItem
            '= Dim myPrintItem As ToolStripItem
            myPrintItem = ts.Items.Add(.Text, .Image, New EventHandler(AddressOf MyPrintItemClicked))
            '= myPrintItem.DisplayStyle = ToolStripItemDisplayStyle.Image
            myPrintItem.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            myPrintItem.Width = 100 '-grh-
            'Relocate the item to the beginning of the toolstrip
            ts.Items.Insert(0, myPrintItem)
        End With

        'Remove the orginal button
        ts.Items.Remove(printItem)

    End Sub  '--new-
    '= = = = = ===== =

    Private Sub myPrintPreview_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'if you like:
        '= Me.Height = FrmMain.Height
        '= Me.Width = FrmMain.Width
    End Sub
    '= = = = = == == =

    Private Sub MyPrintItemClicked(ByVal sender As Object, ByVal e As EventArgs)

        Dim dlgPrint As New PrintDialog
        Try
            With dlgPrint
                .AllowSelection = True
                .ShowNetwork = True
                .Document = Me.Document 'me verweist offenbar schon auf das Doc im Dialog.
            End With
            If dlgPrint.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.Document.Print()
            End If
        Catch ex As Exception
            MsgBox("Print Error: " & ex.Message)
        End Try
        Me.Close()  'to make it more convenient
    End Sub
End Class  '-PrintPreviewDialogWithPrinter-
'= = = = = = = = = = = = = = == = = = = = =

'=== the end of sub-class= 
'=== the end of sub-class= 
'-===FF->

'== NOW the actual Print Report Class-
'== NOW the actual Print Report Class-

Public Class clsReportPrinter

    '==
    '--  Report Body Content: 
    '--    The input collection "ColReportLines" should contain (HTML-like] marked up text lines-
    '--      Viz: 1.)  A Text line thus: 
    '--              <textline> ...[text items]..  </textline>   
    '--               which can contain multiple marked-up text items (fields) 
    '--                   Each item thus: 
    '--                       <txt [TAB:npx] [BOLD] [ITALIC] [WIDTH:npx] [RALIGN]> --text flddata-- </txt>  
    '--      Or 2.)  A Draw line command thus: 
    '--                  <drawline [TAB:npx] [WIDTH:npx] </drawline>
    '--    NB:  "npx" is value in pixels..
    '--
    '--    The Lines will be printed on the report body as they are encountered in the collection.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = ==  =

    '-- Printer units..-
    '---- for .net Graphics..
    '----- printer display units:  100/inch..--
    Const k_PRTWIDTH = 720
    Const k_LEFTMARGIN = 60

    '--  FOR vb.net:
    Const PRT_UNIT = 1

    '--  FOR vb6:
    '--1440 twips per inch.
    '==  for VB^.. Const PRT_UNIT As Short = 15 '--twips per pixel.. (14.4 for PRTunits)
    '===========================================

    '--  font type --

    'Private Structure userFontDef
    '    Dim sName As String '-- font name-
    '    Dim lngSize As Integer
    '    Dim bBold As Boolean
    '    Dim bUnderline As Boolean
    '    Dim bItalic As Boolean
    'End Structure
    '= = = = = = = = = = = =  == 

    Public Enum textColour
        orange
        magenta
        black
        grey
        white
        firebrick
    End Enum
    '= = = = = = = = = = = = = 
    Private Enum printDocType
        Receipt
        StandardReport
    End Enum
    '= = = = = = = = = = = =  == 
    Private Structure AttribInfo
        Dim Name As String
        Dim Value As String
    End Structure
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--  Main printer object..-- 
    '--    DOES ALL PRINTING..--
    Private WithEvents printDocument1 As New System.Drawing.Printing.PrintDocument()
    '==AddHandler printDocument1.PrintPage, AddressOf Me.printDocument1_PrintPage

    '=Preview-
    '= Private WithEvents printPreview1 As PreviewPrintController

    '--3311.225= use the sub-class
    Private printPreviewDialog1 As New PrintPreviewDialogWithPrinter

    '-- 3107.820-  Cpmpletion Info..-
    Private mbPrintingCompleted As Boolean = False
    Private mbPrintError As Boolean = False
    Private msPrintErrorMsg As String = ""

    '--  STATIC VARS for all printing..-  
    '= Private mDefaultUserFont As userFontDef
    Private mlPrtWidth As Integer '--pixels or twips (11,000).-

    Private msBusinessname As String = "--"
    Private msVersion As String = ""

    Private msColourPrinterName As String = ""
    Private msReceiptPrinterName As String = ""
    Private msLabelPrinterName As String = ""

    '- Fonts-
    Private mFontTitle, mFontSubHdr As Font
    Private mFontColHdrs As Font
    Private mFontPageHdr, mFontSubHdr2, mFontContent, mFontContentBold, mFontContentBig As Font
    Private mFontFooterText As Font

    Private mbFixedPitchContent As Boolean = False

    '- Barcode Font if needed-
    Private msBarcodeFontName As String = ""
    Private mIntBarcodeFontSize As Integer = -1

    Private mFontBarcode As Font
    Private mIntBarcodeStandardLines As Integer

    '--  SAVE STATE..--
    '--    page state vars..--

    '--  save doc-type currently being printed..-- 
    Private mlPrintDocType As printDocType '== = -1

    '-- Printer to Use...--
    Private msSelectedPrinterName As String = ""
    Private mIntPageNo As Integer = 0
    Private mIntUserItemsPrinted As Integer = 0

    '- receipt-
    Private mColReportLines As Collection '--receipt lines-
    Private msReportTitle As String = ""
    Private mColorTitleForeColor As Color

    '--  Print REPORT stuff..--
    '--  Print REPORT stuff..--
    Private msReportSubHeading As String = ""
    Private msReportSubHeading2 As String = ""
    Private msReportColumnHdrLine As String = ""
    Private msReportColumnHdrLine2 As String = ""

    '= Private mDataGridView1 As DataGridView
    Private mIntTotalWidth As Integer = 0

    Private mbFirstPage As Boolean = True
    '= Private mColColumnLefts As Collection
    '= Private mColColumnWidths As Collection

    '-- formatting strings..-
    Private mStrFormat1 As StringFormat
    Private mStrFormatRight As StringFormat

    Private mIntCellHeight As Integer = 0
    Private mIntHeaderHeight As Integer = 0

    Private mIntCurrentRow As Integer = 0
    Private mIntTopMargin As Integer = 0
    '= = = = = = = = = = = = = = = = = = == = = = = = = 
    '-===FF->

    '- set these if needed before callinh print-report

    WriteOnly Property BarcodeFontName As String
        Set(value As String)
            msBarcodeFontName = value
        End Set
    End Property  '-barcode font-
    '= = = = == =

    WriteOnly Property BarcodeFontSize As Integer
        Set(value As Integer)
            mIntBarcodeFontSize = value
        End Set
    End Property  '-font size-
    '= = = = = ==  == = = = == = = = = = = = == = 

    WriteOnly Property FixedPitchContent As Boolean
        Set(value As Boolean)
            mbFixedPitchContent = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = == = = 

    Public Sub New()

        MyBase.New()
        '== Class_Initialize_Renamed()
        mlPrtWidth = k_PRTWIDTH '== 760  '== 734  '==pixels--   (734 * PRT_UNIT) '-- 11,010 twips..-
        '--  set default font for PrintText in box..--
        'With mDefaultUserFont
        '    .sName = "Lucida Sans"
        '    .lngSize = 8
        '    .bBold = False
        '    .bUnderline = False
        '    .bItalic = False
        'End With

        mFontTitle = New Font("Lucida Sans", 16, FontStyle.Bold)  '--report title texts-
        mFontSubHdr = New Font("Lucida Sans", 8, FontStyle.Bold)  '--sub hdr texts-
        mFontPageHdr = New Font("Lucida Sans", 10, FontStyle.Bold)  '--Biz. page hdr texts-
        mFontSubHdr2 = New Font("Lucida Sans", 9, FontStyle.Bold Or FontStyle.Italic)  '--col hdr texts-
        mFontColHdrs = New Font("Lucida Sans", 8, FontStyle.Bold)  '--col hdr texts-
        mFontContent = New Font("Lucida Sans", 8, FontStyle.Regular)
        mFontContentBold = New Font("Lucida Sans", 8, FontStyle.Bold)
        mFontContentBig = New Font("Lucida Sans", 12, FontStyle.Bold)
        mFontFooterText = New Font("Lucida Sans", 7, FontStyle.Regular)

    End Sub '--initalise..--
    '= = = = = =  = = = ==
    '-===FF->

    '- Draw Line- -

    Private Function mIntDrawLine(ByVal ev As PrintPageEventArgs, _
                              ByVal intUL_X As Integer, _
                               ByVal intUL_Y As Integer, _
                                ByVal lngLineWidth As Integer, _
                                 ByVal intWidth As Integer, _
                                 ByVal color1 As Color) As Integer

        ' Create pen.
        Dim blackPen As New Pen(color1, 1)

        blackPen.Width = intWidth
        ' Create coordinates of points that define line.
        Dim x1 As Integer = intUL_X
        Dim y1 As Integer = intUL_Y
        Dim x2 As Integer = intUL_X + lngLineWidth
        Dim y2 As Integer = intUL_Y

        ' Draw line to screen.
        ev.Graphics.DrawLine(blackPen, x1, y1, x2, y2)

    End Function  '-- DrawLine --
    '= = = = = = = = = = = = = =

    '-- overloaded-- Default Gray, 1-pixel-

    Private Function mIntDrawLine(ByVal ev As PrintPageEventArgs, _
                               ByVal intUL_X As Integer, _
                                ByVal intUL_Y As Integer, _
                                 ByVal lngLineWidth As Integer) As Integer

        ' Create pen.
        '== Dim blackPen As New Pen(Color.Gray, 1)

        '=blackPen.Width = intWidth
        Call mIntDrawLine(ev, intUL_X, intUL_Y, lngLineWidth, 1, Color.Gray)

    End Function  '-- DrawLine (2) --
    '-===FF->

    '-- General Print stuff..

    '-- Input dimension in Pixels--
    '--  Return print dimension according to system used..-
    '---  ie Twips (vb6) or pixels (vb.net..)--

    Private Function mlPrtDx(ByRef lngPixels As Integer) As Integer
        Dim sinResolution As Single

        sinResolution = PRT_UNIT
        mlPrtDx = CInt(sinResolution * lngPixels)

    End Function '- mlPrtUnits-
    '= = = = = = = = = = = = = = = = = = = = 

    '-- Get width of text string..

    Private Function mlGetTextWidth(ByVal ev As PrintPageEventArgs, _
                                   ByVal sText As String, _
                                   ByRef printFont As Font) As Integer
        Dim sizeF1, sizeMeasure As SizeF
        '= Dim printFont As Font
        Dim style1 As New FontStyle
        Dim lngChars, lngLines As Integer

        '--  set font..--
        'With UserFont
        '    style1 = .bBold Or .bUnderline Or .bItalic
        '    printFont = New Font(.sName, .lngSize, style1)
        'End With
        '--  get width of line with proposed new word..
        sizeF1 = ev.Graphics.MeasureString(sText, printFont, sizeMeasure, _
                                 StringFormat.GenericTypographic, lngChars, lngLines)
        mlGetTextWidth = Convert.ToInt32(sizeF1.Width)
    End Function '== get width..-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-mbLoadAttributes-

    Private Function mbLoadAttributes(ByVal strAttributeText As String, _
                                       ByRef colAttributes As Collection) As Boolean
        Dim sRemainder As String = strAttributeText
        Dim intPos As Integer
        Dim sAttribName, sAttribValue As String
        Dim attribInfo1 As AttribInfo

        mbLoadAttributes = False
        Try
            colAttributes = New Collection
            While (Len(sRemainder) > 0)
                intPos = InStr(sRemainder, "=")
                If (intPos = 0) Then
                    sRemainder = ""
                Else
                    sAttribName = Trim(Left(sRemainder, intPos - 1))
                    sRemainder = Trim(Mid(sRemainder, intPos + 1))
                    If (sRemainder <> "") Then
                        '= intPos = FindUnquotedWhitespace(sRemainder, 1)
                        '-- RHS may be quoted (double-quotes only) or not.
                        If Left(sRemainder, 1) = Chr(34) Then '--starts with double quote-
                            intPos = InStr(2, sRemainder, Chr(34))
                            If (intPos > 0) Then
                                sAttribValue = Trim(Mid(sRemainder, 2, intPos - 2))
                                sRemainder = Trim(Mid(sRemainder, intPos + 1))
                            Else  '-no closing quote-
                                sRemainder = ""
                                Exit While
                            End If
                        Else  '-no quote-
                            '-take next token as rhs-
                            intPos = InStr(2, sRemainder, " ")
                            If (intPos > 0) Then
                                sAttribValue = Trim(Left(sRemainder, intPos - 1))
                            Else
                                '- end bit-
                                sAttribValue = Trim(sRemainder)
                                sRemainder = ""
                            End If
                        End If '-quote-
                        attribInfo1.Name = sAttribName
                        attribInfo1.Value = sAttribValue
                        colAttributes.Add(attribInfo1) '=nAttributeCount = nAttributeCount + 1
                    End If  '-remainder-
                End If  '-pos-
            End While  '-remainder-
            mbLoadAttributes = True
            Exit Function

        Catch ex As Exception
            MsgBox("ReportPrinter- Error in LoadAttributes-" & vbCrLf & ex.Message)
        End Try
    End Function  '-mbLoadAttributes-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- check for attribute-
    '-- Return the value if found..-

    Private Function mbHasAttribute(ByVal colAttributes As Collection, _
                                    ByVal strAttribute As String, _
                                    ByRef strvalue As String) As Boolean
        mbHasAttribute = False
        If (Not (colAttributes Is Nothing)) AndAlso (colAttributes.Count > 0) Then
            For Each attrib1 As AttribInfo In colAttributes
                If UCase(attrib1.Name) = UCase(strAttribute) Then
                    strvalue = attrib1.Value
                    mbHasAttribute = True
                    Exit For
                End If
            Next attrib1
        End If  '-nothing-
    End Function  '-check for attr.-
    '= = = = = =  = = = = == = = = = = = = ==

    Private Function mbFieldIsRightAligned(colAttributes As Collection, _
                                           ByRef intX As Integer, _
                                           ByRef intWidth As Integer) As Boolean

        mbFieldIsRightAligned = False
        '= Dim asTokens() As String = Split(UCase(Replace(strFullAttributeText, ":", " ")), " ")
        'If (asAttributeTokens.Length > 0) Then  '-has some attribute(s).
        '    For ix As Integer = 0 To (asAttributeTokens.Length - 1)
        '        If asAttributeTokens(ix) = "RALIGN" Then  '-yes-


        '            mbFieldIsRightAligned = True
        '            Exit For
        '        End If
        '    Next ix
        'End If  '-length-


    End Function  '-RALIGN-
    '= = = = = =  = = = = == = =


    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '== Page FOOTER for Invoice etc --
    '--     (ALL pages..) ==

    Private Function mbPageFooter(ByVal ev As PrintPageEventArgs, _
                                 ByVal strVersionPOS As String, _
                                   Optional ByVal strPageNo As String = "") As Boolean
        Const k_HDR_HEIGHT = 11
        '- Dim font1 As userFontDef
        Dim lngXpos, lngYpos, L1 As Integer
        Dim intPageNoWidth As Integer = 120
        Dim sText As String
        Dim myStringFormat As New StringFormat

        myStringFormat.Alignment = StringAlignment.Far
        myStringFormat.LineAlignment = StringAlignment.Center  '--vertical-

        '== Printer.CurrentX = 240
        lngXpos = k_LEFTMARGIN  '==(16 * PRT_UNIT) '- 240 twips..-
        lngYpos = (1090 * PRT_UNIT) '- 15,900 twips..-
        'With font1
        '    .sName = "Lucida Sans"
        '    .lngSize = 6
        '    .bBold = False
        '    .bUnderline = False
        '    .bItalic = False
        'End With
        sText = strVersionPOS & Space(100) & _
                       "== Printed: " & Format(Now, "dd-MMM-yyyy hh:mm tt") & Space(10)  '== & strPageNo
        If (strPageNo <> "") Then
            'font1.bBold = True
            'font1.lngSize = 9
            'L1 = gIntPrintTextInRectangle(ev, strPageNo, k_LEFTMARGIN + mlPrtWidth - intPageNoWidth, lngYpos - 6, _
            '                             intPageNoWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
            Dim rectPageNo = New Rectangle(k_LEFTMARGIN + mlPrtWidth - intPageNoWidth, lngYpos - 12, _
                                                                                intPageNoWidth, k_HDR_HEIGHT)
            ev.Graphics.DrawString(strPageNo, _
                      mFontContent, New SolidBrush(Color.Black), rectPageNo, myStringFormat)
        End If  '-page-
        'font1.bBold = False
        'font1.lngSize = 6
        '-- version-
        '= lngYpos = gIntPrintTextString(ev, sText, lngXpos, lngYpos + 2, mfont)
        ev.Graphics.DrawString(sText, _
                  mFontFooterText, New SolidBrush(Color.Black), lngXpos, lngYpos + 3)

        Call mIntDrawLine(ev, lngXpos, lngYpos, mlPrtWidth, 2, mColorTitleForeColor)

    End Function '--Footer..-
    '= = = = = = = = = = = = =
    '-===FF->

    '--  Individual Douments Event handlers..
    '--  Individual Douments Event handlers..
    '--  Individual Douments Event handlers..

    '-- PAGE EVENT support for Print REPORT.-
    '-- Print the REPORT NEXT PAGE -

    Public Function mbPrintReport_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
        Dim intLeftMargin As Integer
        Dim intTmpWidth, intCount As Integer
        Dim bMorePagesToPrint As Boolean
        Dim bHalfLineCounted As Boolean = False
        Dim strDate As String = DateTime.Now.ToLongDateString() & " " & _
                        DateTime.Now.ToShortTimeString()
        Dim s1, sText, sTextline As String
        Dim strFormat1 As StringFormat
        '= Dim font1 As userFontDef
        Dim fontPrintText As Font
        '-- A Rectangle has Left, Top, Width, height--
        Dim rectPageBounds As Rectangle = ev.PageBounds    '-- printable rectangle-
        Dim intPrintHeight As Integer = rectPageBounds.Height - 32
        Dim intPrintWidth As Integer = rectPageBounds.Width - 32
        Dim intXpos, intXpos2, intYpos, intYpos2 As Integer

        'font1.sName = "Lucida Sans"     '== "Tahoma" '== Printer.FontName = "Tahoma"
        'font1.lngSize = 8 '==Printer.FontSize = 18
        'font1.bBold = False '== Printer.Font.Bold = False '--reset to normal IN CASE LEFT OVER..-
        'font1.bUnderline = False '= Printer.Font.Underline = False
        'font1.bItalic = False

        '- Set the left margin
        intLeftMargin = k_LEFTMARGIN   '=rectPageBounds.Left  '= ev.MarginBounds.Left
        '-- Set the top margin
        mIntTopMargin = rectPageBounds.Top + 16  '=rectPageBounds.Top   '=ev.MarginBounds.Top
        '--  Whether more pages have to print or not
        bMorePagesToPrint = False
        intTmpWidth = 0
        mIntPageNo += 1

        Try  '--main try-
            If mbFirstPage Then

                mIntCurrentRow = 0
                mbFirstPage = False
            End If  '-first page-

            '- print Titles..
            '-- Start of Every Page.--
            '-- Start of Every Page.--
            '-- Draw Header text--
            'ev.Graphics.DrawString(msReportTitle & " -        Page: " & mIntPageNo, _
            '         New Font(mDataGridView1.Font, FontStyle.Bold), _
            '               Brushes.Black, ev.MarginBounds.Left, _
            '                   ev.MarginBounds.Top - ev.Graphics.MeasureString("Testing print dataGridView", _
            '                      fontPageHdr, ev.MarginBounds.Width).Height - 13)
            intXpos = intLeftMargin
            intYpos = mIntTopMargin + 16
            '- Biz name at top-
            ev.Graphics.DrawString(msBusinessname & Space(60) & " -    Page: " & mIntPageNo, _
                     mFontPageHdr, Brushes.Black, intXpos, intYpos)
            '= lineHeight = printFont.GetHeight(ev.Graphics)
            intYpos += mFontTitle.GetHeight(ev.Graphics)

            '-Report Title-
            ev.Graphics.DrawString(msReportTitle, _
                     mFontTitle, New SolidBrush(mColorTitleForeColor), intXpos, intYpos)
            '-- Sub-title beside Title-
            Dim intTitleHeight As Integer = mFontTitle.GetHeight(ev.Graphics) + 20
            Dim rectSubTitle As New Rectangle(intXpos + 500, intYpos + 6, 320, intTitleHeight + 16)
            ev.Graphics.DrawString(msReportSubHeading, _
                                      mFontSubHdr, New SolidBrush(mColorTitleForeColor), rectSubTitle)
            intYpos += intTitleHeight
            '-Hdr 2--
            ev.Graphics.DrawString(msReportSubHeading2, _
                     mFontSubHdr2, New SolidBrush(Color.Black), intXpos, intYpos)
            intYpos += mFontSubHdr2.GetHeight(ev.Graphics) + 3
            '-- thick line under headings.
            Call mIntDrawLine(ev, intLeftMargin, intYpos, mlPrtWidth, 3, mColorTitleForeColor)

            '--- space down to column headers.-
            intYpos += (mFontPageHdr.GetHeight(ev.Graphics)) + 3
            '-- col hdrs.-
            If (msReportColumnHdrLine <> "") Then
                ev.Graphics.DrawString(msReportColumnHdrLine, _
                                      mFontColHdrs, New SolidBrush(Color.Black), intXpos, intYpos)
            End If
            If (msReportColumnHdrLine2 <> "") Then
                intYpos += (mFontPageHdr.GetHeight(ev.Graphics)) + 3
                ev.Graphics.DrawString(msReportColumnHdrLine2, _
                                      mFontColHdrs, New SolidBrush(Color.Black), intXpos, intYpos)
            End If
            intYpos += (mFontPageHdr.GetHeight(ev.Graphics) * 2)

            '= end of header stuff-

            '-- Report lines-
            Dim intItemLineHeight As Integer = mFontContent.GetHeight(ev.Graphics)
            Dim intMaxLines As Integer = (intPrintHeight - intYpos - 60) \ (intItemLineHeight + 4)
            '-- Compute no lines occupied by barcode..
            Dim intBarcodeLineHeight As Integer = mFontBarcode.GetHeight(ev.Graphics) + 6
            Dim intBarcodeContentLines As Integer = (intBarcodeLineHeight \ intItemLineHeight) + 1

            Dim intLineCount As Integer = 0
            Dim intLinesNeeded As Integer
            Dim iPos, iPos2, iPos3, intX, intWidth As Integer
            Dim sFldText, sFldAttributes, sValue As String
            '=Dim asAttributeTokens() As String
            Dim colAttributes As Collection '- of AttribInfo
            Dim colTextFieldItems As Collection
            Dim brushContent = New SolidBrush(Color.Black)
            Dim rectField As Rectangle   '= New Rectangle(intXpos, intYpos, 20, 20)
            Dim bLineHasBarcode As Boolean
            '-print a page of report lines-
            Try
                While (mIntUserItemsPrinted < mColReportLines.Count) And (intLineCount < intMaxLines)
                    bLineHasBarcode = False
                    '-- NEXT LINE-  PARSE the the string for text items..-
                    sText = Trim(mColReportLines.Item(mIntUserItemsPrinted + 1))
                    If (VB.Left(UCase(sText), 9) = "<TEXTLINE") Then
                        colTextFieldItems = New Collection
                        '-extract fld list-
                        iPos = InStr(10, sText, ">")
                        If (iPos > 0) Then '-found start tag end-
                            iPos2 = InStr(iPos + 1, UCase(sText), "</TEXTLINE")
                            If (iPos2 > iPos + 1) Then '-found </TEXTLINE-
                                sTextline = Trim(Mid(sText, (iPos + 1), (iPos2 - (iPos + 1)) + 1))
                                '- Parse and extract/print fld list-
                                intXpos = intLeftMargin
                                iPos = InStr(1, UCase(sTextline), "<TXT")
                                While (iPos > 0) And (sTextline <> "")
                                    iPos2 = InStr(iPos + 4, sTextline, ">")  '--end of txt tag stuff.
                                    If (iPos2 > 0) Then
                                        sFldAttributes = Trim(Mid(sTextline, iPos + 4, (iPos2 - (iPos + 4)) + 1))
                                        '==asAttributeTokens = Split(UCase(Replace(sFldAttributes, ":", " ")), " ")
                                        If Not mbLoadAttributes(sFldAttributes, colAttributes) Then
                                            ev.HasMorePages = False  '-error-
                                            Exit Function
                                        End If
                                        '-- TEST- show all attributes-
                                        s1 = "Attributes are:" & vbCrLf
                                        For Each att1 As AttribInfo In colAttributes
                                            s1 &= att1.Name & "=" & att1.Value & vbCrLf
                                        Next '--att1-
                                        '= MsgBox(s1)
                                        '-- get inner data to print-
                                        iPos3 = InStr(iPos2 + 1, UCase(sTextline), "</TXT>")  '-find closing tag-
                                        '== MsgBox("Found fld text at pos: " & iPos)
                                        If (iPos3 > 0) Then
                                            sFldText = Trim(Mid(sTextline, iPos2 + 1, iPos3 - (iPos2 + 1)))
                                            sTextline = Trim(Mid(sTextline, iPos3 + 6)) '- get Remainder-
                                            '= MsgBox("Found fld text:" & vbCrLf & sFldText)
                                            '-- get TAB pos if any..
                                            If mbHasAttribute(colAttributes, "TAB", sValue) Then
                                                If IsNumeric(sValue) AndAlso (CInt(sValue) < 900) Then
                                                    intXpos = CInt(sValue) + intLeftMargin
                                                End If  '-numeric-
                                            End If  '-tab-
                                            fontPrintText = mFontContent
                                            If mbHasAttribute(colAttributes, "FONTSTYLE", sValue) Then
                                                If InStr(LCase(sValue), "bold") > 0 Then
                                                    fontPrintText = mFontContentBold
                                                ElseIf InStr(LCase(sValue), "big") > 0 Then
                                                    fontPrintText = mFontContentBig
                                                ElseIf InStr(LCase(sValue), "barcode") > 0 Then
                                                    fontPrintText = mFontBarcode
                                                    bLineHasBarcode = True
                                                End If  '-bold-
                                            End If  '-font-
                                            intWidth = mlGetTextWidth(ev, sFldText, fontPrintText)
                                            If mbHasAttribute(colAttributes, "WIDTH", sValue) Then
                                                If IsNumeric(sValue) AndAlso (CInt(sValue) < 900) Then
                                                    intWidth = CInt(sValue)
                                                End If  '-numeric-
                                            End If  '-width-
                                            Try
                                                '- check if ALIGN= right (needs Rectangle)-
                                                If mbHasAttribute(colAttributes, "ALIGN", sValue) AndAlso _
                                                                            (InStr(LCase(sValue), "right") > 0) Then
                                                    rectField = New Rectangle(intXpos, intYpos, intWidth, intItemLineHeight)
                                                    ev.Graphics.DrawString(sFldText, _
                                                                   fontPrintText, brushContent, rectField, mStrFormatRight)
                                                Else  '-left align-
                                                    rectField = New Rectangle(intXpos, intYpos, intWidth * 2, intItemLineHeight)
                                                    ev.Graphics.DrawString(sFldText, _
                                                       fontPrintText, brushContent, rectField, mStrFormat1)
                                                End If  '--align-
                                            Catch ex As Exception
                                                MsgBox("ERROR in graphcs drawstring-" & vbCrLf & _
                                                       ex.Message & vbCrLf & "Text was: " & sFldText, MsgBoxStyle.Exclamation)
                                            End Try
                                            '-- add width to xpos-  (in case next fld has no TAB pos)-
                                            intXpos += intWidth '= mlGetTextWidth(ev, sFldText, fontPrintText)
                                            '-- look for next txt fld in this line.
                                            iPos = InStr(1, UCase(sTextline), "<TXT")

                                        End If  '-ipos3-
                                    Else  '-invalid txt tag..
                                        Exit While
                                    End If
                                End While  '--<txt-
                            Else '-no end tag-
                            End If '-found </TEXTLINE-
                        Else  '-bad textline start tag-
                        End If '-found start tag end-
                    ElseIf (VB.Left(UCase(sText), 9) = "<DRAWLINE") Then
                        '-- draw line-
                        intWidth = mlPrtWidth
                        intXpos = intLeftMargin  '-default-
                        '-- get attributes !! --
                        iPos2 = InStr(10, sText, "/>")  '--end of txt tag stuff.
                        If (iPos2 > 0) Then
                            sFldAttributes = Trim(Mid(sText, 10, (iPos2 - 10) + 1))
                            If mbLoadAttributes(sFldAttributes, colAttributes) Then
                                If mbHasAttribute(colAttributes, "TAB", sValue) Then
                                    If IsNumeric(sValue) AndAlso (CInt(sValue) < 900) Then
                                        intXpos = CInt(sValue) + intLeftMargin
                                        intWidth = mlPrtWidth - CInt(sValue)
                                    End If  '-numeric-
                                End If  '-tab-
                                If mbHasAttribute(colAttributes, "WIDTH", sValue) Then
                                    If IsNumeric(sValue) AndAlso (CInt(sValue) < 900) Then
                                        intWidth = CInt(sValue)
                                    End If  '-numeric-
                                End If  '-width-
                            End If '-load-
                        End If  '-pos-
                        Call mIntDrawLine(ev, intXpos, intYpos, intWidth)
                        'ev.Graphics.DrawLine(New Pen(Color.Gray, 1), _
                        '                         intXpos, intYpos + 3, mlPrtWidth, intYpos + 3)
                    ElseIf (VB.Left(UCase(sText), 12) = "<MINPAGEROOM") Then
                        '=3519.0115=
                        '-- "n" lines to be avaliable..
                        intLinesNeeded = 0
                        iPos2 = InStr(10, sText, "/>")  '--end of txt tag stuff.
                        If (iPos2 > 0) Then
                            sFldAttributes = Trim(Mid(sText, 13, (iPos2 - 13) + 1))
                            If mbLoadAttributes(sFldAttributes, colAttributes) Then
                                If mbHasAttribute(colAttributes, "LINES", sValue) Then
                                    If IsNumeric(sValue) AndAlso (CInt(sValue) < 100) Then
                                        intLinesNeeded = CInt(sValue)
                                        '-(intLineCount < intMaxLines)-
                                        If (intLinesNeeded > (intMaxLines - intLineCount)) Then
                                            '-- not enough room left.
                                            intLineCount = intMaxLines  '-force new page.
                                        End If
                                    End If  '-numeric-
                                End If  '-has-
                            End If  '-load
                        End If  '- pos2.
                        mIntUserItemsPrinted += 1
                        Continue While
                    ElseIf (VB.Left(UCase(sText), 21) = "<VERTICALGAP_HALFLINE") Then
                        intYpos += intItemLineHeight \ 2 '= 3
                        mIntUserItemsPrinted += 1
                        '-- count a line every second half-space..
                        If bHalfLineCounted Then
                            bHalfLineCounted = False
                        Else
                            intLineCount += 1
                            bHalfLineCounted = True   '--flip-  
                        End If
                        Continue While
                    End If  '-textline-etc.
                    mIntUserItemsPrinted += 1
                    If bLineHasBarcode Then
                        intYpos += intBarcodeLineHeight
                        intLineCount += intBarcodeContentLines
                    Else  '-normal
                        intYpos += intItemLineHeight + 4
                        intLineCount += 1
                    End If
                End While  '-mIntUserItemsPrinted-
            Catch ex As Exception
                MsgBox("Error in PrintReport Content Try." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                ev.HasMorePages = False
                Exit Function
            End Try

            '- update page count-
            Dim frm1 As Form = CType(printPreviewDialog1, Form)
            frm1.Text = msReportTitle & " (" & mIntPageNo & " pages).."

            '-print footer-
            Call mbPageFooter(ev, msVersion, "Page: " & mIntPageNo)

            '= RESET this when no more pages- (For printing from Dialog).
            '= mIntPageNo = 0
            '== https://social.msdn.microsoft.com/Forums/vstudio/en-US/48203a8c-8c64-4a3d-964d-d7a330c1827f/printdocument-control-only-prints-last-page-of-document?forum=vbgeneral

            ev.HasMorePages = (mIntUserItemsPrinted < mColReportLines.Count)
            If (mIntUserItemsPrinted < mColReportLines.Count) Then
                ev.HasMorePages = True
            Else
                ev.HasMorePages = False
                '--RE- do some initial setup--
                mbFirstPage = True
                mIntPageNo = 0
                mIntUserItemsPrinted = 0  '-Progress in mColReportLines-
            End If
            Exit Function

        Catch ex As Exception  '-main try-
            MsgBox("Error in PrintReport Main Try." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)

        End Try  '-main try-

    End Function  '-mbPrintReport_PageEvent-
    '= = = = = = = = = = = = ==  == = = == 
    '-===FF->

    '--  Main print Page EVENT handler..--
    '--  Main print Page EVENT handler..--
    '--   FOR ALL PRINT FUNCTIONS..--

    Private Sub printDocument1_PrintPage(ByVal sender As Object, _
                                               ByVal ev As PrintPageEventArgs) _
                                            Handles printDocument1.PrintPage

        '--  check static var to determine doc-type..--
        '--   and call associated event function..

        Select Case mlPrintDocType
            Case printDocType.StandardReport
                Call mbPrintReport_PageEvent(ev)
            Case printDocType.Receipt
                '= Call mbPrintReceipt_PageEvent(ev)
            Case Else
        End Select

        '-- Check to see if more pages are to be printed.
        '== ev.HasMorePages = False  '== (miPageNo < 2)    '==stringToPrint.Length > 0

    End Sub  '--page event..-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- A C T U A L   P u b l i c  M e t h o d s..--
    '-- A C T U A L   P u b l i c  M e t h o d s..--
    '-- A C T U A L   P u b l i c  M e t h o d s..--

    '- print PrintStandardReport - (eg for Reports).-
    '- print PrintStandardReport - (eg for Reports).-

    '== "colReportLines" has collection of Marked-up Report Lines to print.-

    Public Function PrintStandardReport(ByVal bPreviewOnly As Boolean, _
                                        ByVal strVersion As String, _
                                        ByRef colReportLines As Collection, _
                                        ByVal sSelectedPrinterName As String, _
                                         ByVal strBusinessname As String, _
                                          ByVal strReportTitle As String, _
                                           ByVal colorTitleForeColor As Color, _
                                           Optional ByVal strReportSubHeading As String = "", _
                                            Optional ByVal strReportSubHeading2 As String = "", _
                                            Optional ByVal strReportColumnHdrLine As String = "", _
                                            Optional ByVal strReportColumnHdrLine2 As String = "") As Boolean

        PrintStandardReport = False

        mbPrintingCompleted = False  '=3107.820-
        mbPrintError = False
        msPrintErrorMsg = ""

        '-- save stuff to print..-
        msVersion = strVersion
        '== mDataGridView1 = dataGridView1
        mColReportLines = colReportLines
        If mColReportLines Is Nothing Then
            MsgBox("Report info collection not specified..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        msSelectedPrinterName = sSelectedPrinterName
        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Report printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        msBusinessname = strBusinessname

        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.StandardReport
        msReportTitle = strReportTitle
        mColorTitleForeColor = colorTitleForeColor

        msReportSubHeading = strReportSubHeading
        msReportSubHeading2 = strReportSubHeading2
        msReportColumnHdrLine = strReportColumnHdrLine
        msReportColumnHdrLine2 = strReportColumnHdrLine2

        '-- do some initial setup--
        '-- do some initial setup--
        mbFirstPage = True
        mIntPageNo = 0
        mIntUserItemsPrinted = 0  '-Progress in mColReportLines-

        mIntTotalWidth = 0
        '= mColColumnLefts = New Collection
        '= mColColumnWidths = New Collection

        mStrFormat1 = New StringFormat()
        mStrFormat1.Alignment = StringAlignment.Near
        mStrFormat1.LineAlignment = StringAlignment.Center
        mStrFormat1.Trimming = StringTrimming.EllipsisCharacter

        mStrFormatRight = New StringFormat()
        mStrFormatRight.Alignment = StringAlignment.Far
        mStrFormatRight.LineAlignment = StringAlignment.Center  '--vertical-
        mStrFormatRight.Trimming = StringTrimming.EllipsisCharacter

        '-- set up barcode font if needed..
        If (msBarcodeFontName <> "") AndAlso (mIntBarcodeFontSize > 0) Then
            mFontBarcode = New Font(msBarcodeFontName, mIntBarcodeFontSize, FontStyle.Regular)  '--page hdr texts-
        Else
            '-- any font as placeholder-
            mFontBarcode = New Font("Courier New", 12, FontStyle.Bold)  '--default for barcode-
        End If  '-barcode-

        '==     v3.4.3403.1110..  10-Nov-2017= ===
        '==       >> Report Printer class.  ADD OPTION for Fixed Pitch content.
        '-set fixed pitch if needed..
        If mbFixedPitchContent Then
            mFontContent = New Font("Lucida Console", 8, FontStyle.Regular)
            mFontContentBold = New Font("Lucida Console", 8, FontStyle.Bold)
            mFontContentBig = New Font("Lucida Console", 12, FontStyle.Bold)
        End If  '-fixed..

        If bPreviewOnly Then
            Try
                '--  preview selected..- Set parms..-
                printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
                '-get screen size.
                Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
                Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height

                '-  PREVIEW requested- 
                printPreviewDialog1.Document = printDocument1
                printPreviewDialog1.Top = 0
                printPreviewDialog1.Left = 300
                printPreviewDialog1.Height = screenHeight - 120  '=800
                printPreviewDialog1.Width = 840
                '=4201.0623-  make intitallyy 100%..
                printPreviewDialog1.PrintPreviewControl.Zoom = 0.9
                printPreviewDialog1.PrintPreviewControl.UseAntiAlias = True

                '--  start the preview..--
                printPreviewDialog1.ShowDialog()

                PrintStandardReport = True
            Catch ex As Exception
                '== MessageBox.Show(ex.Message)
                MsgBox("Error in Report preview.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                PrintStandardReport = False
            End Try  '- prewview-
        Else  '-print-
            Try
                '--  set printer selected..--
                printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
                '--  start the printer..--
                '-  PREVIEW requested- 
                printDocument1.Print()
                PrintStandardReport = True

            Catch ex As Exception
                MsgBox("Error in Report printing.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                PrintStandardReport = False

            End Try  'print-

        End If '-bPreviewOnly or print-

    End Function  '--PrintReport--
    '= = = = = = = = = = = = = = = = = = = = 

End Class  '--clsReportPrinter-
'= = = = = = = = = = = = ==  = = =

'== the end===
