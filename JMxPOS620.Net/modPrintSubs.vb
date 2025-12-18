Option Strict Off
Option Explicit On
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Drawing2D
Imports VB = Microsoft.VisualBasic

Module modPrintSubs

    '==
    '==  grh JobMatixPOS 3.1.3107.0821 - 21-Aug-2015
    '==       >> Subs to share with all Doc. prints...
    '==
    '==   3403.1031- Added "JobMatix34" parm to  gsGetPDF_file_path
    '=       (get data dir)
    '==
    '==   3411.0110- 10Jan2018=
    '=       -- Add functiom "gsGetPdfPrinterName"
    '==
    '==
    '== NEW Revision-
    '==   == 4219.1214.  10-Dec-2019-  Started 10-Dec-2019-
     '==     -- PO PDF (email) printing.... 
    '==            In "gbSaveDocumentToEmailQueue", 
    '==                  keep local file copy of PDF for SEVEN days after copying to Email Queue.. 
    '==            ALSO- in "gsGetPDF_file_path" (modPrintSubs), Get AppName from gsGetAppname
    '==
    '= = = =  = = = = = = = = = = = = = = == = =  = = = = == = = =

    '==========================================
    '-- Printer units..-
    '---- for .net Graphics..
    '----- printer display units:  100/inch..--

    Const k_PRTWIDTH = 760
    Const k_LEFTMARGIN = 32

    '--  FOR vb.net:
    Const PRT_UNIT = 1

    '--  FOR vb6:
    '--1440 twips per inch.
    '==  for VB^.. Const PRT_UNIT As Short = 15 '--twips per pixel.. (14.4 for PRTunits)
    '===========================================


    '--  font type --

    Public Structure userFontDef
        Dim sName As String '-- font name-
        Dim lngSize As Integer
        Dim bBold As Boolean
        Dim bUnderline As Boolean
        Dim bItalic As Boolean
    End Structure
    '= = = = = = = = = = = =  == 

    Public Enum textColour
        orange
        magenta
        black
        grey
        white
        DarkViolet
        FireBrick
        Green
    End Enum
    '= = = = = = = = = = = = = 
    '-===FF->

    '--  STATIC VARS for all printing..-  
    Private mDefaultUserFont As userFontDef
    Private mlPrtWidth As Integer '--pixels or twips (11,000).-

    '-- init default user font.-

    Private Sub Initialize_Default_font()

        mlPrtWidth = k_PRTWIDTH '== 760  '== 734  '==pixels--   (734 * PRT_UNIT) '-- 11,010 twips..-
        '--  set default font for PrintText in box..--
        With mDefaultUserFont
            .sName = "Lucida Sans"
            .lngSize = 8
            .bBold = False
            .bUnderline = False
            .bItalic = False
        End With

    End Sub  '--initialise..-
    '= = = = = = = = =  == =  ==  =

    '- get PDF file path in ProgramData
    '==
    '== NEW Revision-
    '==   == 4219.1214.  10-Dec-2019-  Started 10-Dec-2019-
    '==     -- PO PDF (email) printing.... 
    '==            ALSO- in "gsGetPDF_file_path" (modPrintSubs), Get AppName from gsGetAppName
    '==


    Public Function gsGetPDF_file_path() As String

        Dim sInvoiceFilePath As String
        '=Dim s1 = gsJobMatixLocalDataDir("JobMatix34")  '== 3403.1031=
        Dim sAppName As String = gsGetAppName()
        Dim s1 = gsJobMatixLocalDataDir(sAppName)  '== 3403.1031=
        '== sInvoiceFilePath = s1 & "\AllDocuments"
        sInvoiceFilePath = s1 & "\temp"
        If Not My.Computer.FileSystem.DirectoryExists(sInvoiceFilePath) Then  '-must create..-
            My.Computer.FileSystem.CreateDirectory(sInvoiceFilePath)
        End If '-- exists statement dir.-
        gsGetPDF_file_path = sInvoiceFilePath

    End Function  '-gsGetPDF_file_path-
    '= = = = = = = = = = = = = = = = = 

    '-- load Adobe registry PDF FileName key-

    Public Function gbSetAdobeFileName(ByVal sFullFilePath As String, _
                                        ByVal sAppFullname As String) As Boolean

        gbSetAdobeFileName = False
        Try
            My.Computer.Registry.CurrentUser.CreateSubKey("Software\Adobe\Acrobat Distiller\PrinterJobControl")
            '- WE are 32-bit app--
            If gbIsWow64() Then  '-we are on 64-bit os.--
                '-- for wow 64-  splwow64.exe
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Adobe\Acrobat Distiller\PrinterJobControl", _
                                                 "c:\windows\splwow64.exe", sFullFilePath)
            Else  '--32 bit OS-
                My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Adobe\Acrobat Distiller\PrinterJobControl", _
                                                sAppFullname, sFullFilePath)
            End If  '-wow64-
            gbSetAdobeFileName = True
        Catch ex As Exception
            MsgBox("ERROR in setting Adobe registry value." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function '-adobe-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '==   3411.0110- 10Jan2018=
    '=       -- Add function "gbGetPdfPrinterName"

    Public Function gsGetPdfPrinterName(ByRef sPrinterName As String) As Boolean

        Dim sPdfPrinterName As String = ""
        Dim sDefaultPrinterName As String = ""
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        gsGetPdfPrinterName = False

        If Not gbGetAvailablePrinters(colPrinters, sDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("'GetPdfPrinterName'- No printers available ! ", MsgBoxStyle.Exclamation)
            Exit Function
        Else
            '-- Look for Microsoft first as priority..
            For Each sName As String In colPrinters
                If ((InStr(LCase(sName), "microsoft") > 0) And (InStr(LCase(sName), "pdf") > 0)) Then
                    sPdfPrinterName = sName  '-save PDF printer name--
                End If
            Next sName
            If (sPdfPrinterName = "") Then
                '-- No Microsoft- Look for ADOBE printer..
                For Each sName As String In colPrinters
                    If (InStr(LCase(sName), "adobe pdf") > 0) Then
                        sPdfPrinterName = sName  '-save Adobe PDF printer name--
                    End If
                Next sName
            End If
        End If  '- no printers-
        sPrinterName = sPdfPrinterName
        gsGetPdfPrinterName = True  '-function worked ok..
        If (sPdfPrinterName = "") Then
            MsgBox("'GetPdfPrinterName'- Note: " & vbCrLf & "No PDF printer is installed on this system." & vbCrLf & _
                    "Invoices created can not be stored for emailing)..", MsgBoxStyle.Information)
        End If
    End Function '-gsGetPdfPrinterName-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- Draw box..  with or without colour..-
    '--  Arguments:  xpos, ypos, width, depth..-
    '--  FillColour:  -1=None (transparent)--
    '---              +1= Hatched grey..   
    '---               >3 = (Solid) Fill Colour BG..--
    '--  Printer must already be set..--

    Private Function mlDrawBox(ByVal ev As PrintPageEventArgs, _
                              ByVal lngUL_X As Integer, _
                                ByVal lngUL_Y As Integer, _
                                 ByVal lngBoxWidth As Integer, _
                                  ByVal lngBoxDepth As Integer, _
                                    Optional ByVal lngFillColour As Integer = -1) As Integer


        ' Create an integer representation of an HTML color.
        '==  Dim oleColor As Integer = &HFF00

        ' Translate oleColor to a GDI+ Color structure.
        '== Dim myColor As Color = ColorTranslator.FromOle(oleColor)

        ' Fill a rectangle with myColor.
        '== e.Graphics.FillRectangle(New SolidBrush(myColor), 0, 0, 100, 100)

        Dim myColor As Color   '==   = ColorTranslator.FromOle(lngFillColour)
        Dim hBrush As New HatchBrush(HatchStyle.ZigZag, Color.Gray, Color.White)


        If lngFillColour > 3 Then '-- solid fill it..--
            myColor = ColorTranslator.FromOle(lngFillColour)
            ev.Graphics.FillRectangle(New SolidBrush(myColor), lngUL_X, lngUL_Y, lngBoxWidth, lngBoxDepth)
        ElseIf lngFillColour > 0 Then '--Hatch it...--
            ev.Graphics.FillRectangle(hBrush, lngUL_X, lngUL_Y, lngBoxWidth, lngBoxDepth)
        Else  '-- -1 is transparent..
            ev.Graphics.DrawRectangle(Pens.LightGray, lngUL_X, lngUL_Y, lngBoxWidth, lngBoxDepth)
        End If


    End Function  '--draw box..-
    '= = = = = = = = = = = = = = 
    '-===FF->

    '-- P u b l ic  S u b s  -
    '-- P u b l ic  S u b s  -
    '-- P u b l ic  S u b s  -
    '-- P u b l ic  S u b s  -


    Public Function gIntDrawLine(ByVal ev As PrintPageEventArgs, _
                                 ByVal lngUL_X As Integer, _
                                  ByVal lngUL_Y As Integer, _
                                   ByVal lngLineWidth As Integer) As Long

        ' Create pen.
        Dim blackPen As New Pen(Color.Gray, 1)

        ' Create coordinates of points that define line.
        Dim x1 As Integer = lngUL_X
        Dim y1 As Integer = lngUL_Y
        Dim x2 As Integer = lngUL_X + lngLineWidth
        Dim y2 As Integer = lngUL_Y

        ' Draw line to screen.
        ev.Graphics.DrawLine(blackPen, x1, y1, x2, y2)

    End Function  '-- DrawLine --
    '= = = = = = = = = = = = = =

    '-- Print text string IN RECTANGLE with User font style...--
    '-- Text will be RIGHT aligned if needed....-
    '--  NB:  bCentreAlign overrules leftAlign if true..
    '--  Returns << lineHeight >> of printed line..-

    Public Function gIntPrintTextInRectangle(ByVal ev As PrintPageEventArgs, _
                                        ByVal sText As String, _
                                         ByVal intUL_Xpos As Integer, _
                                          ByVal intUL_Ypos As Integer, _
                                          ByVal intWidth As Integer, _
                                          ByVal intDepth As Integer, _
                                            ByRef UserFont As userFontDef, _
                                            Optional ByVal userColour As textColour = textColour.black, _
                                                Optional ByVal bRightAlign As Boolean = False, _
                                                  Optional ByVal bDrawBox As Boolean = False, _
                                                  Optional ByVal bCentreAlign As Boolean = False, _
                                                  Optional ByVal bVertAlignTop As Boolean = False) As Integer

        Dim lngChars As Integer = 0
        Dim lngLines As Integer = 0
        Dim Xpos, Ypos As Single
        Dim myStringFormat As New StringFormat
        Dim lineHeight As Single = 0
        '== Dim sizeF1, sizeMeasure As SizeF
        Dim s1 As String
        Dim printFont As Font
        Dim style1 As New FontStyle
        Dim myBrush As SolidBrush

        Call Initialize_Default_font()  '-- we added this-
        If bCentreAlign Then
            myStringFormat.Alignment = StringAlignment.Center
        ElseIf bRightAlign Then
            myStringFormat.Alignment = StringAlignment.Far
        Else
            myStringFormat.Alignment = StringAlignment.Near
        End If
        myStringFormat.LineAlignment = StringAlignment.Center  '--vertical-

        Xpos = Convert.ToSingle(intUL_Xpos)
        Ypos = Convert.ToSingle(intUL_Ypos)
        Dim drawRect As New RectangleF(Xpos, Ypos, Convert.ToSingle(intWidth), Convert.ToSingle(intDepth))

        If userColour = textColour.white Then
            myBrush = New SolidBrush(Color.White)
        ElseIf (userColour = textColour.magenta) Then  '--default..-
            myBrush = New SolidBrush(Color.Magenta)
        ElseIf (userColour = textColour.grey) Then  '-grey..-
            myBrush = New SolidBrush(Color.Gray)
        ElseIf (userColour = textColour.black) Then  '--default..-
            myBrush = New SolidBrush(Color.Black)
        ElseIf (userColour = textColour.orange) Then  '--default..-
            myBrush = New SolidBrush(Color.Orange)
        ElseIf (userColour = textColour.FireBrick) Then  '--default..-
            myBrush = New SolidBrush(Color.Firebrick)
        ElseIf (userColour = textColour.DarkViolet) Then  '--default..-
            myBrush = New SolidBrush(Color.DarkViolet)
        ElseIf (userColour = textColour.Green) Then  '--4201.0620..-
            myBrush = New SolidBrush(Color.Green)
        Else
            myBrush = New SolidBrush(Color.Black)
        End If

        '--  set up required font from User Spec..--
        With UserFont
            If .bBold Then style1 = FontStyle.Bold
            If .bUnderline Then style1 = style1 Or FontStyle.Underline
            If .bItalic Then style1 = style1 Or FontStyle.Italic
            '== MsgBox("FINAL  Style1 value is: " & style1, MsgBoxStyle.Information)
            printFont = New Font(.sName, .lngSize, style1)
        End With
        lineHeight = printFont.GetHeight(ev.Graphics)
        If bDrawBox Then
            ev.Graphics.DrawRectangle(Pens.Gray, intUL_Xpos, intUL_Ypos, intWidth, intDepth)
        End If

        ev.Graphics.DrawString(sText, printFont, myBrush, drawRect, myStringFormat)

        gIntPrintTextInRectangle = lineHeight
    End Function  '- mIntPrintTextInRectangle-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Print text string with User font style...--
    '--    Text will be truncated by the system if it goes out of bounds..-
    '--  Returns YPOS after print..-
    '--   3.1.3103.0318 - (lngStartXpos=-1) Means CENTRE string on Page.-

    Public Function gIntPrintTextString(ByVal ev As PrintPageEventArgs, _
                                        ByVal sText As String, _
                                         ByVal lngStartXpos As Integer, _
                                          ByVal lngStartYpos As Integer, _
                                            ByRef UserFont As userFontDef, _
                                            Optional ByVal userColour As textColour = textColour.black) As Integer
        Dim sLine As String
        Dim iPos, kx As Integer
        Dim lngChars As Integer = 0
        Dim lngLines As Integer = 0
        Dim Xpos, Ypos As Single
        Dim myStringFormat As New StringFormat
        Dim lineHeight As Single = 0
        Dim sizeF1, sizeMeasure As SizeF
        Dim s1 As String
        Dim printFont As Font
        Dim style1 As New FontStyle
        Dim myBrush As SolidBrush

        Call Initialize_Default_font()  '-- we added this-
        If (lngStartXpos = -1) Then '-centre the text-
            Xpos = (k_PRTWIDTH \ 2)
            myStringFormat.Alignment = StringAlignment.Center
        Else  '-normal-
            Xpos = Convert.ToSingle(lngStartXpos)
        End If
        Ypos = Convert.ToSingle(lngStartYpos)
        myStringFormat.LineAlignment = StringAlignment.Center  '--vertical-

        If userColour = textColour.white Then
            myBrush = New SolidBrush(Color.White)
        ElseIf (userColour = textColour.magenta) Then  '--default..-
            myBrush = New SolidBrush(Color.Magenta)
        ElseIf (userColour = textColour.black) Then  '--default..-
            myBrush = New SolidBrush(Color.Black)
        ElseIf (userColour = textColour.orange) Then  '--default..-
            myBrush = New SolidBrush(Color.Orange)
        ElseIf (userColour = textColour.FireBrick) Then  '--default..-
            myBrush = New SolidBrush(Color.Firebrick)
        ElseIf (userColour = textColour.DarkViolet) Then  '--default..-
            myBrush = New SolidBrush(Color.DarkViolet)
        ElseIf (userColour = textColour.Green) Then  '--4201.0620..-
            myBrush = New SolidBrush(Color.Green)
        Else
            myBrush = New SolidBrush(Color.Black)
        End If

        '--  set up required font from User Spec..--
        '== MsgBox("Style1 value is: " & style1, MsgBoxStyle.Information)

        With UserFont
            '-- if bold is true then ALL style options are set, incl Srtikethrough.   !!!!!
            '== style1 = .bBold Or .bUnderline Or .bItalic

            If .bBold Then style1 = FontStyle.Bold
            If .bUnderline Then style1 = style1 Or FontStyle.Underline
            If .bItalic Then style1 = style1 Or FontStyle.Italic
            '== MsgBox("FINAL  Style1 value is: " & style1, MsgBoxStyle.Information)
            printFont = New Font(.sName, .lngSize, style1)
        End With
        lineHeight = printFont.GetHeight(ev.Graphics)
        '--  BOX for Measure method is deliberately made too big-
        '---  so that our own line width calc. is allowed to trigger first..
        sizeMeasure.Width = 800   '-- Page width..--lngWidth * 2
        sizeMeasure.Height = lineHeight
        '==Printer.Print sText
        sLine = sText
        '--check for tabs..-
        iPos = InStr(sLine, vbTab)
        If iPos <= 0 Then '-- no tabs..  just print the line..-
            sLine = Replace(sLine, "^", " ")
            ev.Graphics.DrawString(sLine, printFont, myBrush, Xpos, Ypos, myStringFormat)
        Else '--break into columns..-
            While (iPos > 0) And (sLine <> "")
                s1 = VB.Left(sLine, iPos - 1) '--stuff before tab..-
                s1 = Replace(s1, "^", " ")

                sizeF1 = ev.Graphics.MeasureString(sLine & s1, printFont, sizeMeasure, _
                                           myStringFormat, lngChars, lngLines)
                '== Printer.Print(s1) '-- stay on the line..
                ev.Graphics.DrawString(s1, printFont, myBrush, Xpos, Ypos, myStringFormat)
                Xpos = Xpos + sizeF1.Width  '--  next xpos.-
                If IsNumeric(Mid(sLine, iPos + 1, 3)) Then
                    kx = CShort(Mid(sLine, iPos + 1, 3)) '-- get tab pos nnn following tab char..-
                    '== Printer.Print Tab(kx);   '--stay on line..-
                    '--  Convert Char pos to Pixel/printer position..--
                    '--   at 5.3 pixels per char  (18 cpi approx..  8pt)..
                    '--  next segment will print here..-
                    '== Printer.CurrentX = CInt(kx * 5.3) * PRT_UNIT
                    Xpos = Convert.ToSingle(kx * 5.3) * PRT_UNIT
                End If
                sLine = Mid(sLine, iPos + 4) '--get remainder of text.
                iPos = InStr(sLine, vbTab)
            End While
            sLine = Replace(sLine, "^", " ")
            '== Printer.Print(sLine) '--print tail and allow next line..-
            ev.Graphics.DrawString(sLine, printFont, myBrush, Xpos, Ypos, myStringFormat)

        End If '--pos/tab-
        gIntPrintTextString = Convert.ToInt32(Ypos + lineHeight) '== Printer.CurrentY

    End Function '--print text..-
    '= = = = = = = = = = ==
    '-===FF->

    '--  print a box of text with some formatting..-
    '--  print a box of text with some formatting..-
    '--  Printer must already be set..--

    '-- NOTE:  for vb.net:  Coordinates are now in DISPLAY (PRINTER) UNITS (1/100 inch)-
    '--      ( very close to PIXELS.  SO we call then DisplayUnits..--)
    '--- and 1 PrUnit= 14.4 TWIPS..--)
    '---   ("Long" vars are replaced with INTEGER).-
    '-- UL_X, UL_Y gives pos of top left corner..- 

    Public Function gIntPrintTextInBox(ByVal ev As PrintPageEventArgs, _
                                     ByVal sInputText As String, _
                                      ByVal lngUL_X As Integer, _
                                      ByVal lngUL_Y As Integer, _
                                      ByVal lngMargin As Integer, _
                                      ByVal lngBoxWidth As Integer, _
                                      ByVal lngBoxDepth As Integer, _
                                          ByVal bDrawBox As Boolean, _
                                   Optional ByVal lngFillColour As Integer = -1, _
                                   Optional ByVal lngUserFontSize As Integer = -1) As Integer

        Dim sRem As String
        Dim sLine As String
        Dim sInputRem As String
        '--Dim sChunk As String
        Dim iPos, ix, kx As Integer
        Dim lngWidth, lngLeft As Integer
        Dim lngYpos As Integer
        Dim lngWordCount As Integer
        '==Dim lngCurrentY As Integer   '--pixels-
        Dim lngChars As Integer = 0
        Dim lngLines As Integer = 0
        '== Dim lngTwipsPerChar, lngMaxCharsPerLine As Integer
        Dim asWordList As Object
        Dim bFittedOk As Boolean
        Dim printFont, normalFont, boldFont, ulFont, boldULFont As Font
        Dim lineHeight As Single = 0
        Dim xPos, yPos As Single
        Dim sizeBox As New SizeF
        Dim sizeMeasure As New SizeF
        Dim sizeF1 As SizeF
        Dim myStringFormat As New StringFormat
        Dim tabStops As Single() = {}
        Dim lngStops As Integer
        Dim bBold, bUnderline, bItalic As Boolean
        Dim sFontName As String
        Dim lngFontSize As Integer

        Call Initialize_Default_font()  '-- we added this-
        sFontName = mDefaultUserFont.sName
        lngFontSize = mDefaultUserFont.lngSize
        If lngUserFontSize > 0 Then
            lngFontSize = lngUserFontSize
        End If

        '-- Unavoidably messy with multiple styles..
        normalFont = New Font(sFontName, lngFontSize)
        boldFont = New Font(sFontName, lngFontSize, FontStyle.Bold)
        ulFont = New Font(sFontName, lngFontSize, FontStyle.Underline)
        boldULFont = New Font(sFontName, lngFontSize, FontStyle.Underline Or FontStyle.Bold)

        printFont = normalFont
        lineHeight = printFont.GetHeight(ev.Graphics)
        '-- Scale WAS always in twips..--
        '= FIX !!  ==   lngTwipsPerChar  =  Printer.Font.SIZE * 20
        lngLeft = lngUL_X + lngMargin
        lngWidth = lngBoxWidth - (lngMargin * 2)
        '==lngMaxCharsPerLine = lngWidth \ lngTwipsPerChar
        sizeBox.Width = lngWidth
        sizeBox.Height = lngBoxDepth
        '--  BOX for Measure method is deliberately made too big-
        '---  so that our own line width calc. is allowed to trigger first..
        sizeMeasure.Width = lngWidth * 2
        sizeMeasure.Height = lineHeight
        myStringFormat = StringFormat.GenericTypographic
        '--Form1.Print
        '--Form1.Print " lngTwipsPerChar = " & lngTwipsPerChar
        '--Form1.Print " lngMaxCharsPerPrinterLine = " & (Printer.Width \ lngTwipsPerChar)
        '--Form1.Print " lngMaxCharsPerLine (in box) = " & lngMaxCharsPerLine

        sInputRem = Trim(sInputText)
        If bDrawBox Then
            '======     ev.Graphics.DrawRectangle(Pens.LightGray, lngUL_X, lngUL_Y, lngBoxWidth, lngBoxDepth)
            lngYpos = mlDrawBox(ev, lngUL_X, lngUL_Y, lngBoxWidth, lngBoxDepth, lngFillColour)
            '=  Printer.FillStyle = 1
            '=  Printer.Line (lngUL_X, lngUL_Y)-(lngUL_X + lngBoxWidth, lngUL_Y + lngBoxDepth), , B
        End If  '--draw box.-
        '== txt1.Text = txt1.Text & vbCrLf & "=>PRINT TEXT in box..  boxWidth=" & lngWidth & vbCrLf
        '--max chars per line:  boxWidth div by average char width..-
        '-- Make line chunks for longer lines..-
        '--  TOP MARGIN is one line height..--
        '= Printer.CurrentY = lngUL_Y + Printer.TextHeight("Average Text")   '--should auto increase --
        yPos = Convert.ToSingle(lngUL_Y) + (lineHeight / 2)  '--In POS. start half a line dow.n-
        While (Len(sInputRem) > 0) And (yPos < Convert.ToSingle(lngUL_Y + lngBoxDepth))
            iPos = InStr(sInputRem, vbCrLf) '--Major break at LF's if any..
            If (iPos > 0) Then
                sRem = RTrim(VB.Left(sInputRem, iPos - 1))   '--pick out line to print..-
                sInputRem = Mid(sInputRem, iPos + 2)
            Else  '--no lf.. 1 long text line..-
                sRem = sInputRem
                sInputRem = ""
            End If
            '-- check style.-  can be at start of line(para) only..-
            printFont = normalFont
            bBold = False : bUnderline = False : bItalic = False
            While (LCase(VB.Left(sRem, 3)) = "<b>") Or _
                                                   (LCase(VB.Left(sRem, 4)) = "<ul>")
                If LCase(VB.Left(sRem, 3)) = "<b>" Then
                    bBold = True
                    sRem = Mid(sRem, 4) '--drop markup..-
                ElseIf LCase(VB.Left(sRem, 4)) = "<ul>" Then
                    bUnderline = True
                    sRem = Mid(sRem, 5) '--drop markup..-
                End If
            End While
            '== set the right font..-
            If bBold And bUnderline Then
                printFont = boldULFont  '==bold AND UL.-
            ElseIf bBold Then
                printFont = boldFont  '==printFont.Bold = True '--true  '---false
            ElseIf bUnderline Then
                printFont = ulFont  '==  Printer.Font.Underline = True '--true  '---false
            End If

            lineHeight = printFont.GetHeight(ev.Graphics)
            '--  max chars per line varies according to size/style  ??? ..-
            '--  break long line if needed.-
            '--  SCAN for TAB stops ann set them, if any..-
            myStringFormat = New StringFormat
            '== already empty array.- ==  Erase tabStops
            lngStops = 0
            iPos = InStr(sRem, vbTab)
            If (iPos > 0) Then  '-- have tabs..  
                While (iPos > 0) '= And (sLine <> "")
                    If IsNumeric(Mid(sRem, iPos + 1, 3)) Then
                        kx = CInt(Mid(sRem, iPos + 1, 3)) '-- get tab pos nnn following tab char..-
                        ReDim Preserve tabStops(lngStops)
                        tabStops(lngStops) = kx
                        '===  Printer.Print Tab(kx);   '--stay on line..-
                        lngStops += 1
                    End If
                    sRem = VB.Left(sRem, iPos) & Mid(sRem, iPos + 4) '--Drop nnn...
                    iPos = InStr(iPos + 1, sRem, vbTab)  '-- keep going down the line..-
                End While
                If (tabStops.Length > 0) Then myStringFormat.SetTabStops(0.0F, tabStops)
            End If  '--tabs..--
            If (Trim(sRem) = "") Then  '--empty line.-
                yPos = yPos + lineHeight
                '=  Printer.Print()
            Else  '--something non blank..-
                asWordList = Split(sRem, " ")  '-- dissociate line into word-tokens..-
                '-- NOTE !!  "Split" counts all leading spaces as null substrings..--
                '-- NOTE !!  "Split" counts all leading spaces as null substrings..--
                ix = 0 '--index into word list.-
                lngWordCount = UBound(asWordList) + 1
                '== txt1.Text = txt1.Text & "= Word count=" & lngWordCount & vbCrLf
                '--pack as many words into each line as will fit.-
                '---  print this "para"..-  can be multiple lines.-
                While (ix < lngWordCount)  '--More stuff.. can go to last word..-
                    sLine = asWordList(ix) '-- start with first word..-
                    ix = ix + 1 '-point to second word (if any)..-
                    '--  while still more words, try and fit next word.-
                    bFittedOk = True
                    While (ix <= (lngWordCount - 1)) And (bFittedOk)
                        '--  get width of line with proposed new word..
                        sizeF1 = ev.Graphics.MeasureString(sLine & " " + asWordList(ix), printFont, sizeMeasure, _
                                                 StringFormat.GenericTypographic, lngChars, lngLines)
                        '== If (Printer.TextWidth(sLine + asWordList(ix)) <= lngWidth) Then  '--fits.-
                        '== txt1.Text = txt1.Text & "=> Measure-" & ix & ": w=" & Convert.ToInt32(sizeF1.Width) & vbCrLf
                        If (Convert.ToInt32(sizeF1.Width) <= lngWidth) Then
                            sLine = sLine & " " + asWordList(ix)
                            ix = ix + 1
                        Else
                            bFittedOk = False '--no more will fit.-
                        End If
                    End While  '--width.-
                    '=  Printer.CurrentX = lngLeft 
                    '--start each line at same X pos..-
                    xPos = Convert.ToSingle(lngLeft)
                    '--check for tabs..-
                    '==iPos = InStr(sLine, vbTab)
                    '==If (iPos <= 0) Then '-- no tabs..  just print the line..-
                    sLine = Replace(sLine, "^", " ")
                    ev.Graphics.DrawString(sLine, printFont, Brushes.Black, xPos, yPos, myStringFormat)
                    yPos = yPos + lineHeight
                    '== Printer.Print(sLine) '--print current chunk/line..-
                    ''=End If
                    sLine = ""
                End While  '--wordcount.-
                '== Printer.Font.Bold = False '--reset to normal..-
                '--Printer.FontSize = 10
                '== Printer.Font.Underline = False
                printFont = normalFont
            End If  '--new line.-
        End While  '--sInputRem..-
        gIntPrintTextInBox = Convert.ToInt32(yPos)
    End Function  '--print text..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '== Page FOOTER for Invoice etc --
    '--     (ALL pages..) ==

    Public Function gbPageFooter(ByVal ev As PrintPageEventArgs, _
                                 ByVal strVersionPOS As String, _
                                   Optional ByVal strPageNo As String = "") As Boolean
        Const k_HDR_HEIGHT = 11
        Dim font1 As userFontDef
        Dim lngXpos, lngYpos, L1 As Integer
        Dim intPageNoWidth As Integer = 120
        Dim sText As String

        '== Printer.CurrentX = 240
        lngXpos = k_LEFTMARGIN  '==(16 * PRT_UNIT) '- 240 twips..-
        lngYpos = (1090 * PRT_UNIT) '- 15,900 twips..-
        With font1
            .sName = "Lucida Sans"
            .lngSize = 6
            .bBold = False
            .bUnderline = False
            .bItalic = False
        End With
        sText = strVersionPOS & Space(100) & _
                       "== Printed: " & Format(Now, "dd-MMM-yyyy hh:mm tt") & Space(10)  '== & strPageNo
        If (strPageNo <> "") Then
            font1.bBold = True
            font1.lngSize = 9
            L1 = gIntPrintTextInRectangle(ev, strPageNo, k_LEFTMARGIN + mlPrtWidth - intPageNoWidth, lngYpos - 6, _
                                         intPageNoWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
        End If
        font1.bBold = False
        font1.lngSize = 6
        '-- version-
        lngYpos = gIntPrintTextString(ev, sText, lngXpos, lngYpos + 2, font1)

        Call gIntDrawLine(ev, lngXpos, lngYpos, mlPrtWidth)

    End Function '--Footer..-
    '= = = = = = = = = = = = =

    '-- END OF  Re-Written  PRINT SERVICE function..
    '-- END OF  Re-Written  PRINT SERVICE function..

    '= = = = = = = = = = = = = = = = =



End Module '-modPrintSubs-
'= = = = = = = = = = = = =

'== the end ==
