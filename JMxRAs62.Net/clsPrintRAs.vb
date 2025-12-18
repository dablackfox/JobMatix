Option Strict Off
Option Explicit On

Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Drawing2D
Imports VB = Microsoft.VisualBasic
Imports System.Collections.Generic

Friend Class clsPrintRAs
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'

    '-- JobMatix PRINTING Class to:
    '--  Print New Job (Service Agreement)..
    '--    and Service (Delivery) Record --
    '--    and Receipt Dockets..
    '--
    '--    and RA Forms..
    '-------------------------

    '--grh - 18-July-2011= Rev-2912 =
    '--grh - 21-Aug-2011== Rev-2921 =  Fix Service Agreement printer msg.

    '--grh - 30-Oct-2011==JobMatix V3.0- Rev-3010--
    '--    New version..  to Print ALL Job/RA documents (NewJob and Maint)..--
    '------  Gettting ready to migrate to vb.net..-
    '= = = = =  =

    '--grh - 04-Jan-2012==JobMatix V3.0-Build-3023--
    '-----   Fixes for printing barcode list..

    '--grh - 17-Mar-2012==JobMatix V3.0-Build-3031.2--
    '--       Fix tabArray init in printTextInBox..--
    '==        AND Add CR/LF to each Main ServicePart/Item..
    '==         AND add "Date Printed" and today's date to Job footer..

    '==grh - 03-Apr-2012==JobMatix V3.0-Build-3037.0--
    '==        Add try/catch around Print statements..

    '==grh - 11-Apr-2012==JobMatix V3.0-Build-3041.1--
    '==       ServiceChargesInfoText
    '==grh - 12-Apr-2012==JobMatix V3.0-Build-3043.0--
    '==      Drop BOX drawn around ServiceChargesInfoText
    '==
    '==grh - 16-May-2012==JobMatix V3.0-Build-3053.0--
    '--     Fix Label Barcode position..
    '--     Fix Label Customer printing..
    '==
    '==grh - 22-May-2012==JobMatix V3.0-Build-3057.0--
    '--     Fix Extra Parts not printing....
    '==
    '==grh - 09-Feb-2013==JobMatix V3.0-Build-3072.0--
    '--     Fix EVAL overprint overprinting itself..
    '==
    '==grh - 14-Mar-2014==JobMatix V3.0-Build-3083.314--
    '--     ON-SITE Notice on Service Agreement..
    '==
    '==grh - 05-Apr-2014==JobMatix V3.0-Build-3083.405--
    '--     Print Service Record. Add Reminder to Deliver...
    '==
    '==   grh JobMatix31  V:3.1.3107.0517= 18May2015= 
    '==     Release Build-
    '==       >> Service Agreement- restrict problem details printout to 600 chars..
    '==       >> Service Record- Add up Labour + Parts for Total Cost printout...
    '==
    '==   grh JobMatix31  V:3.1.3107.0907= 07Sep2015= 
    '==     Release Build-
    '==       >> Job Main Form.. Colour Goods box for ON-SITE jobs.
    '==
    '==   grh JobMatix32  V:3.2.3203.103= 03Jan2016= 
    '==       >> Print Picture of ItemImage for RA's and Jobs...
    '==       >> 3203.110=
    '==        >> -- Job Maint CustomerReport- (from Service Record)-
    '==        >> -- 3203.118- New Job can have SystemUnderWarranty.-
    '==            And Job Ticket reorganised..  JobNo ON TOP...-
    '==
    '==   grh JobMatix32  V:3.2.3203.211= 11Feb2016= 
    '==       >> Tidying up Service Record...
    '==
    '==  grh 3203.212-  12-Feb-2016=
    '==    >> Print Label function can print (RA) Labels as per Job Labels...
    '==
    '==  -- 3203.218= 18Feb. JobMaint Footer- 
    '==      >>  Print Version first..
    '==         THEN print date at the right..
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW Version for RAs onlyy.  (for new JMxRAs330.exe)--
    '==
    '==  JMxRAs330.exe
    '==
    '==  grh. 3301.221= 18Feb2016- 
    '==      >>  Deleted Non-RAs stuff....
    '==
    '==  grh. 3311.319= 18Feb2016- 
    '==      >>  Update Print Shipping Label to print a bunch
    '==           of items to ship a whole package of RA items.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    Const k_TABPRINTPRIORITY As Short = 20
    Const k_TABPRINTBRAND As Short = 26
    Const k_TABPRINTMODEL As Short = 40
    Const k_TABPRINTSERIAL As Short = 56

    '== Private Const K_GOODS_ONSITEJOB = "ON-SITE JOB;"

    Private Const K_LEFT_MARG = 36
    Private Const K_TOP_MARG = 12
    Private Const K_HEADER_BOTTOM = 130

    '=  PrintDocument.OriginAtMargins Property (FALSE for us..).
    '= Gets or sets a value indicating whether the position of a graphics object associated with a page is located 
    '=  just inside the user-specified margins 
    '=   or at the top-left corner of the printable area of the page.
    '- --
    '==  https://msdn.microsoft.com/en-us/library/system.drawing.printing.printdocument.originatmargins(v=vs.90).aspx

    '==========================================
    '-- Printer units..-
    '---- for .net Graphics..
    '----- printer display units:  100/inch..--

    '--  FOR vb.net:
    Const PRT_UNIT = 1

    '--  FOR vb6:
    '--1440 twips per inch.

    '==  for VB^.. Const PRT_UNIT As Short = 15 '--twips per pixel.. (14.4 for PRTunits)
    '===========================================

    '--  font type --

    Private Structure userFontDef
        Dim sName As String '-- font name-
        Dim lngSize As Integer
        Dim bBold As Boolean
        Dim bUnderline As Boolean
        Dim bItalic As Boolean
    End Structure
    '= = = = = = = = = = = =  == 

    Private Enum printDocType
        JobLabels
        Receipt
        RAForm
        RAShippingLabel
    End Enum

    Private Enum textColour
        orange
        magenta
        DarkViolet
        black
        grey
        white
    End Enum
    '= = = = = = = = = = = = = 

    '--  Main printer object..-- 
    '--    DOES ALL PRINTING..--
    Private WithEvents printDocument1 As New System.Drawing.Printing.PrintDocument()
    '==AddHandler printDocument1.PrintPage, AddressOf Me.printDocument1_PrintPage

    '--  STATIC VARS for all printing..-  
    Private mDefaultUserFont As userFontDef
    Private mlPrtWidth As Integer '--pixels or twips (11,000).-

    '-- Top After Header-
    Private mIntMainDetailTop As Integer = 0

    '--  SAVE STATE..--
    '--   JOB MAINT page state vars..--
    '--   JOB MAINT page state vars..--
    '=RAs= Private mIntMaintPage As Short
    '=RAs= Private mIntLineCount As Short
    '=RAs= Private mIntWtyLoop As Short
    '=RAs= Private mIntWtyCount As Short
    '=RAs= Private mIntMaintItemIx As Short
    '=RAs= Private mLngItemCount As Integer  '--count items printed..-

    '--  save doc-type currently being printed..-- 
    Private mlPrintDocType As printDocType '== = -1

    '--labels- 
    Private mIntLineCount As Integer = 0
    Private mIntPageNo As Integer = 0

    '--  Print forms stuff..--

    Private mObjUserLogo As Image
    Private msStaffName As String = ""
    Private mColBusiness As Collection
    Private mStrVersion As String = ""
    Private mStrHeaderDate As String = ""
    Private mStrHeaderDate2 As String = ""

    '-- Printer to Use...--
    Private msSelectedPrinterName As String = ""

    '==  GONE  ==
    '==  Private mPrtSelected As Printer

    '== New Job Service Agreement..
    '== New Job Service Agreement..

    '=RAs= Private mbIsOnSiteJob As Boolean = False  '==3083==
    '=RAs= Private msOnSiteDate As String = ""       '==3083==
    '=RAs= Private msOnSiteTime As String = ""       '==3083==

    Private mLngJobNo As Integer = -1
    Private mbLicenceOK As Boolean = False
    '=RAs= Private mbJobReturned As Boolean = False
    '=RAs= Private mbSystemUnderWarranty As Boolean = False  '=3203.118=

    '=RAs= Private msJobStatus As String = ""

    '==Private mbQuotationRequired As Boolean
    '=RAs= Private mStrTermsText As String = ""
    '==Private mStrQuotationRequiredText As String
    '==Private mStrProceedWithServiceText As String

    Private mLngInstructionBGColour As Integer
    Private mStrInstructionLabel As String
    Private mStrInstructionText As String
    '-- 3041.1--
    Private mStrServiceChargesInfoText As String = ""

    Private mStrTicketDate As String
    Private mLngPriorityColour As Integer
    '=RAs= Private mCurLabourMinCharge As Decimal = 0
    '=RAs= Private mCurLabourHourlyRate As Decimal = 0
    '==Private mCurNotificationCostLimit As Currency

    Private mColCust As Collection
    Private msCustomerPrint As String = ""
    '= = = = = = = = = = = =  = = =  = =

    '--  Service form..
    '=RAs= Private mbQuotation As Boolean

    Private msItemBarcodeFontName As String = ""
    Private mlItemBarcodeFontSize As Integer = 9

    '=RAs= Private mColTasksCompleted As Collection
    '=RAs= Private msShowLabourCost As String

    '=RAs= Private mColServiceItems As Collection
    '=RAs= Private mColPartsItems As Collection

    '=RAs= Private mCurTotalItemValue As Decimal = 0 '--servce items and parts items.
    '=RAs= Private mCurTotalLabourValue As Decimal = 0

    '=RAs= Private msNotifications As String = ""
    '=RAs= Private msWorkHistory As String
    '=RAs= Private mObjPictureExtraPrint As Object

    '==Private mbIsOnSiteJob As Boolean = False

    '--  RA stuff.-

    Private mlRA_id As Integer = -1
    Private msSupplierRMA As String = ""
    Private msRAStatus As String = ""

    Private msSerialNo As String = ""
    Private msDescription As String
    Private msItemSupplierCode As String

    Private msItemBarcode As String
    Private msInvoiceInfo As String

    Private msProblemReported As String
    Private msSource As String
    Private msRA_customer As String

    Private msSupplier As String '-supplier name..-
    Private msSupplierDetails As String
    Private msRA_Progress As String

    Private msSupplierAddressInfo As String
    Private msSupplierMainPhone As String
    '==3311.319-
    Private mColRA_Items As Collection

    '= = = = = = = = = = = = = = = = =


    '--  RECEIPT  stuff SAVED. ..--
    Private mColReportLines As Collection

    '--  Job Labels stuff SAVED. ..--
    Private mLngJobId, mIntNoLabels As Integer
    Private msCustomer As String
    Private mbIsRAlabel As Boolean = False

    '-- 3203- Item IMAGE saved--
    '-- FOR JOB or RA..--
    Private imageItem1 As Image

    Private mIntItemsPrinted As Integer = 0
    '= = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--  property parameters..--
    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property
    '= = = = = = = ==  =

    '-- Printer Names..--
    '--  use for vb.net.--
    WriteOnly Property PrtSelectedPrinterName() As String
        Set(ByVal Value As String)
            msSelectedPrinterName = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = =

    '--  picture object..-
    WriteOnly Property UserLogo() As Image
        Set(ByVal Value As Image)
            mObjUserLogo = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    '--RA or JOB ITEM  picture object..-
    WriteOnly Property ItemImage() As Image
        Set(ByVal Value As Image)
            imageItem1 = Value
        End Set
    End Property  '-image-
    '= = = = = = = = = = = =  = = =  = =


    WriteOnly Property Version() As String
        Set(ByVal Value As String)
            mStrVersion = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property Business() As Collection
        Set(ByVal Value As Collection)
            mColBusiness = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property HeaderDate() As String
        Set(ByVal Value As String)
            mStrHeaderDate = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =
    WriteOnly Property HeaderDate2() As String
        Set(ByVal Value As String)
            mStrHeaderDate2 = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =


    '-- Stuff for  New Job Form..--
    '-- Stuff for  New Job Form..--

    '-- Job No..-

    WriteOnly Property JobNo() As Integer
        Set(ByVal Value As Integer)
            mLngJobNo = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property LicenceOK() As Boolean
        Set(ByVal Value As Boolean)
            mbLicenceOK = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property TicketDate() As String
        Set(ByVal Value As String)
            mStrTicketDate = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property PriorityColour() As Integer
        Set(ByVal Value As Integer)
            mLngPriorityColour = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =
    '==Property Let NotificationCostLimit(cur1 As Currency)
    '==    mCurNotificationCostLimit = cur1
    '==End Property
    '= = = = = = = = = = = =  = = =  = =


    WriteOnly Property Customer() As Collection
        Set(ByVal Value As Collection)
            mColCust = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =
      '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property ItemBarcodeFontName() As String
        Set(ByVal Value As String)
            msItemBarcodeFontName = Value
        End Set
    End Property '--barcode font.--
    '= = = = = = = =  = = =

    WriteOnly Property ItemBarcodeFontSize() As Integer
        Set(ByVal Value As Integer)
            mlItemBarcodeFontSize = Value
        End Set
    End Property '--barcode font.--
    '= = = = = = = =  = = =
    '-===FF->

    '-- properties for RA's --
    '-- properties for RA's --

    WriteOnly Property RA_No() As Integer
        Set(ByVal Value As Integer)
            mlRA_id = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property SupplierRMA() As String
        Set(ByVal Value As String)

            msSupplierRMA = Value
        End Set
    End Property '--RMA--
    '= = = = = = = = = =

    WriteOnly Property RAStatus() As String
        Set(ByVal Value As String)

            msRAStatus = Value
        End Set
    End Property '--RMA--
    '= = = = = = = = = =

    WriteOnly Property SerialNo() As String
        Set(ByVal Value As String)
            msSerialNo = Value
        End Set
    End Property
    '= = = = = = = = = =

    WriteOnly Property Description() As String
        Set(ByVal Value As String)
            msDescription = Value
        End Set
    End Property
    '= = = = = = = = = =

    WriteOnly Property ItemSupplierCode() As String
        Set(ByVal Value As String)
            msItemSupplierCode = Value
        End Set
    End Property
    '= = = = = = = = = =

    WriteOnly Property ItemBarcode() As String
        Set(ByVal Value As String)
            msItemBarcode = Value
        End Set
    End Property
    '= = = = = = = = = =

    WriteOnly Property InvoiceInfo() As String
        Set(ByVal Value As String)
            msInvoiceInfo = Value
        End Set
    End Property
    '= = = = = = = = = =


    WriteOnly Property ProblemReported() As String
        Set(ByVal Value As String)
            msProblemReported = Value
        End Set
    End Property
    '= = = = = = = = = =

    WriteOnly Property Source() As String
        Set(ByVal Value As String)
            msSource = Value
        End Set
    End Property
    '= = = = = = = = = =

    WriteOnly Property RA_customer() As String
        Set(ByVal Value As String)
            msRA_customer = Value
        End Set
    End Property
    '= = = = = = = = = =

    WriteOnly Property Supplier() As String
        Set(ByVal Value As String)
            msSupplier = Value
        End Set
    End Property
    '= = = = = = = = = =

    WriteOnly Property SupplierDetails() As String
        Set(ByVal Value As String)
            msSupplierDetails = Value
        End Set
    End Property
    '= = = = = = = = = =

    WriteOnly Property RA_Progress() As String
        Set(ByVal Value As String)
            msRA_Progress = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property SupplierAddressInfo() As String
        Set(ByVal Value As String)
            msSupplierAddressInfo = Value
        End Set
    End Property
    '= = = = = = = = = = = = =

    WriteOnly Property SupplierMainPhone() As String
        Set(ByVal Value As String)
            msSupplierMainPhone = Value
        End Set
    End Property
    '= = = = = = = = = = = =
    '-===FF->

    '-- initialize --
    '-- initialize --

    'UPGRADE_NOTE: class_initialize was upgraded to class_initialize_Renamed. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'

    Private Sub Class_Initialize_Renamed()


        mlPrtWidth = 734  '-- 11,010 twips..-

        '--  set default font for PrintText in box..--
        With mDefaultUserFont
            .sName = "Lucida Sans"  '==3203.118-    "Tahoma"
            .lngSize = 8
            .bBold = False
            .bUnderline = False
            .bItalic = False
        End With

        '== mlPrintDocType = -1

    End Sub  '--initialise..-
    '= = = = = = = = =  == =  ==  =

    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub '--initalise..--
    '= = = = = =  = = = ==
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
                                   ByRef UserFont As userFontDef) As Integer
        Dim sizeF1, sizeMeasure As SizeF
        Dim printFont As Font
        Dim style1 As New FontStyle
        Dim lngChars, lngLines As Integer

        '--  set font..--
        With UserFont
            style1 = .bBold Or .bUnderline Or .bItalic
            printFont = New Font(.sName, .lngSize, style1)
        End With

        '--  get width of line with proposed new word..
        sizeF1 = ev.Graphics.MeasureString(sText, printFont, sizeMeasure, _
                                 StringFormat.GenericTypographic, lngChars, lngLines)
        mlGetTextWidth = Convert.ToInt32(sizeF1.Width)
    End Function '== get width..-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Re-Written  PRINT SERVICE function..
    '-- Re-Written  PRINT SERVICE function..


    '-- General Print Routines..
    '-- General Print Routines..

    Private Function mlDrawLine(ByVal ev As PrintPageEventArgs, _
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
    '= = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--    print text string with User font style...--
    '--    Text will be truncated by the system if it goes out of bounds..-
    '--  Returns YPOS after print..-

    Private Function mlPrintTextString(ByVal ev As PrintPageEventArgs, _
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

        Xpos = Convert.ToSingle(lngStartXpos)
        Ypos = Convert.ToSingle(lngStartYpos)
        If userColour = textColour.white Then
            myBrush = New SolidBrush(Color.White)
        ElseIf (userColour = textColour.magenta) Then  '--default..-
            myBrush = New SolidBrush(Color.Magenta)
        ElseIf (userColour = textColour.black) Then  '--default..-
            myBrush = New SolidBrush(Color.Black)
        ElseIf (userColour = textColour.DarkViolet) Then  '--violet..-
            myBrush = New SolidBrush(Color.DarkViolet)
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
                    '--  Convert Char pos to Pixel/printer position..--
                    '--   at 5.3 pixels per char  (18 cpi approx..  8pt)..
                    '--  next segment will print here..-
                    Xpos = Convert.ToSingle(kx * 5.3) * PRT_UNIT
                End If
                sLine = Mid(sLine, iPos + 4) '--get remainder of text.
                iPos = InStr(sLine, vbTab)
            End While
            sLine = Replace(sLine, "^", " ")
            '== Printer.Print(sLine) '--print tail and allow next line..-
            ev.Graphics.DrawString(sLine, printFont, myBrush, Xpos, Ypos, myStringFormat)
        End If '--pos/tab-
        mlPrintTextString = Convert.ToInt32(Ypos + lineHeight) '== Printer.CurrentY

    End Function '--print text..-
    '= = = = = = = = = = ==
    '-===FF->

    '-- Draw signing boxes..--
    '---  Coordinates are for -
    '---     actual top-left corner of top-left box..
    '--- BG Colour of -1 means no bg.

    Private Function mbDrawSigningBoxes(ByVal ev As PrintPageEventArgs, _
                                         ByVal lngSigX As Integer, _
                                            ByVal lngSigY As Integer, _
                                            ByVal sNameCaption As String, _
                                              ByVal sDateCaption As String, _
                                                ByRef UserFont As userFontDef, _
                                                  Optional ByVal lngBG As Integer = &HDCDCDC) As Boolean
        Dim lngError As Integer
        '==Dim lngTextHeight As Long
        Dim lngSigY2 As Integer '--top of bottom boxes..--
        Dim lngSigXGap As Integer '--GAP to srart of date box.--
        Dim lngSigX2 As Integer '--srart of date box.--
        Dim lngSigYGap As Integer '--GAP to srart of date box.--
        Dim lngBoxDepth As Integer
        Dim lngNameBoxWidth As Integer
        Dim lngSignBoxWidth As Integer
        Dim lngDateBoxWidth As Integer
        Dim lngYpos, lngColour As Integer

        On Error GoTo DrawSign_error

        lngBoxDepth = (47 * PRT_UNIT) '--700   '--twips--
        lngNameBoxWidth = (323 * PRT_UNIT) '-- 4840 '--twips-
        lngSignBoxWidth = (190 * PRT_UNIT) '-- 2840  '--twips--
        lngDateBoxWidth = (120 * PRT_UNIT) '-- 1800  '--twips-
        lngSigY2 = lngSigY + lngBoxDepth + (20 * PRT_UNIT) '-- GAP is 300 twips--
        lngSigXGap = CInt(13.3 * PRT_UNIT) '= 200 '-twips-
        lngSigXGap = CInt(20 * PRT_UNIT) '= 300 '-twips-
        lngSigX2 = lngSigX + lngSignBoxWidth + lngSigXGap

        '--  draw all boxes..--
        '-- name box..-
        lngYpos = mlDrawBox(ev, lngSigX, lngSigY, lngNameBoxWidth, lngBoxDepth, lngBG)
        '--make Sig. box..--
        lngYpos = mlDrawBox(ev, lngSigX, lngSigY2, lngSignBoxWidth, lngBoxDepth, lngBG)
        '--make Date box..--
        lngYpos = mlDrawBox(ev, lngSigX2, lngSigY2, lngDateBoxWidth, lngBoxDepth, lngBG)

        '-- Write the captions..-
        '--  offsets were 120 twips (8px)  and 1000--
        Call mlPrintTextString(ev, sNameCaption, lngSigX + 8, lngSigY, UserFont)
        Call mlPrintTextString(ev, "Signed", lngSigX + 8, lngSigY2, UserFont)
        Call mlPrintTextString(ev, sDateCaption, lngSigX2 + 8, lngSigY2, UserFont)

        mbDrawSigningBoxes = True
        Exit Function

DrawSign_error:
        lngError = Err().Number
        MsgBox("!! ERROR in DrawSigningBoxes." & vbCrLf & _
                   "Error code: " & lngError & " = " & ErrorToString(lngError), MsgBoxStyle.Exclamation)
        mbDrawSigningBoxes = False

    End Function '--draw boxes..-
    '= = = = = = = = = = = = =
    '-===FF->

    '--  print a box of text with some formatting..-
    '--  print a box of text with some formatting..-
    '--  Printer must already be set..--

    '-- NOTE:  for vb.net:  Coordinates are now in DISPLAY (PRINTER) UNITS (1/100 inch)-
    '--      ( very close to PIXELS.  SO we call then DisplayUnits..--)
    '--- and 1 PrUnit= 14.4 TWIPS..--)
    '---   ("Long" vars are replaced with INTEGER).-
    '-- UL_X, UL_Y gives pos of top left corner..- 
    '----
    '=  3203.115 --- bDepthTestingOnly means no printing.. Just returns new yPos..
    '--

    Private Function mlPrintTextInBox(ByVal ev As PrintPageEventArgs, _
                                     ByVal sInputText As String, _
                                      ByVal lngUL_X As Integer, _
                                      ByVal lngUL_Y As Integer, _
                                      ByVal lngMargin As Integer, _
                                      ByVal lngBoxWidth As Integer, _
                                      ByVal lngBoxDepth As Integer, _
                                          ByVal bDrawBox As Boolean, _
                                   Optional ByVal lngFillColour As Integer = -1, _
                                   Optional ByVal bDepthTestingOnly As Boolean = False, _
                                   Optional ByVal userFontName1 As String = "Lucida Sans", _
                                   Optional ByVal userFontSize1 As Integer = 8) As Integer

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

        sFontName = userFontName1 '= mDefaultUserFont.sName
        lngFontSize = userFontSize1  '= mDefaultUserFont.lngSize

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

        sInputRem = Trim(sInputText)
        If bDrawBox Then
            '======     ev.Graphics.DrawRectangle(Pens.LightGray, lngUL_X, lngUL_Y, lngBoxWidth, lngBoxDepth)
            lngYpos = mlDrawBox(ev, lngUL_X, lngUL_Y, lngBoxWidth, lngBoxDepth, lngFillColour)
        End If  '--draw box.-
        '== txt1.Text = txt1.Text & vbCrLf & "=>PRINT TEXT in box..  boxWidth=" & lngWidth & vbCrLf
        '--max chars per line:  boxWidth div by average char width..-
        '-- Make line chunks for longer lines..-
        '--  TOP MARGIN is one line height..--
        yPos = Convert.ToSingle(lngUL_Y) + lineHeight
        While (Len(sInputRem) > 0) And (yPos < Convert.ToSingle(lngUL_Y + lngBoxDepth))
            iPos = InStr(sInputRem, vbCrLf) '--Major break at LF's if any..
            If iPos > 0 Then
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
                        If (Convert.ToInt32(sizeF1.Width) <= lngWidth) Then
                            sLine = sLine & " " + asWordList(ix)
                            ix = ix + 1
                        Else
                            bFittedOk = False '--no more will fit.-
                        End If
                    End While  '--width.-
                    '--start each line at same X pos..-
                    xPos = Convert.ToSingle(lngLeft)
                    sLine = Replace(sLine, "^", " ")
                    '- print the Line..-
                    If Not bDepthTestingOnly Then  '-- yes can rint.-
                        ev.Graphics.DrawString(sLine, printFont, Brushes.Black, xPos, yPos, myStringFormat)
                    End If
                    yPos = yPos + lineHeight
                    sLine = ""
                End While  '--wordcount.-
                printFont = normalFont
            End If  '--new line.-
        End While  '--sInputRem..-
        mlPrintTextInBox = Convert.ToInt32(yPos)
    End Function  '--print text..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Print text string IN RECTANGLE with User font style...--
    '-- Text will be RIGHT aligned if needed....-
    '--  NB:  bCentreAlign overrules leftAlign if true..
    '--  Returns << lineHeight >> of printed line..-
    '-- NB:  Stolen from "gIntPrintTextInRectangle" in POS print Subs..

    Private Function mIntPrintTextInRectangle(ByVal ev As PrintPageEventArgs, _
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

        '= Call Initialize_Default_font()  '-- we added this-
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

        mIntPrintTextInRectangle = lineHeight
    End Function  '- mIntPrintTextInRectangle-
    '= = = = = = = = = = = = = = = = =

 

    '-- END OF  Re-Written  PRINT SERVICE function..
    '-- END OF  Re-Written  PRINT SERVICE function..

    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Split text into what can fit, and the rest..
    '--   Return true and both parts if successful.-
    '--  False if nothing can fit..

    Private Function mbSplitTextToFit(ByVal ev As PrintPageEventArgs, _
                                      ByVal intDepthAvailable As Integer, _
                                        ByVal strSourceText As String, _
                                         ByVal intAvailTextWidth As Integer, _
                                          ByRef strFittedText As String, _
                                           ByRef strRemainder As String) As Boolean
        Dim intY2, intBiteSize, ix, ix2 As Integer
        Dim sTrial, sRem As String
        Dim bFitted As Boolean = False

        mbSplitTextToFit = False
        strFittedText = ""
        strRemainder = ""
        sRem = ""
        '--break full src into lines..
        '--split-
        Dim asSource() As String = Split(strSourceText, vbCrLf)
        If (asSource.Length <= 0) Then
            Exit Function
        End If

        '-- Starting from the whole thing, 
        '--   Take a smaller and smaller bite until it fits in lines avail..
        intBiteSize = asSource.Length

        While (intBiteSize > 0) And (Not bFitted)
            '--recombine the bite lines for testing.-
            sTrial = ""
            For ix = 1 To (intBiteSize)
                sTrial &= asSource(ix - 1) & vbCrLf
            Next ix
            '-- test depth.-- (Test only-  No print.-)
            intY2 = mlPrintTextInBox(ev, sTrial, K_LEFT_MARG, 1, _
                                             0, intAvailTextWidth, 1000, False, , True)
            If (intY2 <= intDepthAvailable) Then  '--fits.-
                strFittedText = sTrial
                If (ix < asSource.Length) Then  '--some remainder to recombine-
                    For ix2 = ix To asSource.Length
                        sRem &= asSource(ix2 - 1) & vbCrLf
                    Next ix2
                    strRemainder = sRem
                End If
                bFitted = True
                mbSplitTextToFit = True
            Else '- not yet.
                intBiteSize -= 1
            End If
        End While  '-trial-

    End Function  '-split-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- General Print stuff..

    '--  PRINT RA Header..---
    '--  PRINT RA Header..---

    Private Function mbPrintRA_Header(ByVal ev As PrintPageEventArgs, _
                                     ByVal sHdr1 As String, _
                                       ByVal lngTicketColour As Integer, _
                                        Optional ByVal sStatus As String = "") As Boolean
        Dim lngTicketX, lngTicketY As Integer
        Dim lngTicketWidth, lngTicketDepth As Integer
        '==Dim lngTxtHt, lngTxtWidth As Integer
        Dim lngHdrX, lngHdrY As Integer
        Dim lngXpos, lngYpos As Integer
        Dim lngLeftMarg As Integer
        Dim lngStatusPos As Integer
        Dim s1 As String
        Dim font1 As userFontDef

        lngLeftMarg = K_LEFT_MARG   '= (16 * PRT_UNIT) '- 240 twips..-
        '--  paint logo  top left.--
        '== Printer.PaintPicture Me.Picture2.Picture, 240, 0
        If Not (mObjUserLogo Is Nothing) Then
            ev.Graphics.DrawImage(mObjUserLogo, lngLeftMarg, 16)
        End If

        '-- print main title..-
        lngHdrX = 186  '= (146 * PRT_UNIT) '-- 2200 twips.
        lngTicketX = 456
        lngTicketY = 0

        '-- RA Ticket is longer and shallow..--
        lngTicketWidth = 260  '= mlPrtDx(240) '== 3600 twips..-
        lngTicketDepth = 30  '= mlPrtDx(27) '==  400 twips.-

        font1.sName = "Lucida Sans" '== Printer.FontName = "Tahoma"
        font1.lngSize = 18 '==Printer.FontSize = 18
        font1.bBold = False '== Printer.Font.Bold = False '--reset to normal IN CASE LEFT OVER..-
        font1.bUnderline = False '= Printer.Font.Underline = False
        font1.bItalic = False
        '== lngTxtHt = Printer.TextHeight(sHdr1)
        '== Printer.CurrentX = lngHdrX
        lngHdrY = (20 * PRT_UNIT) '--   300 twips '--down 1/3 title height from top..-
        '== Printer.Print sHdr1
        lngYpos = mlPrintTextString(ev, sHdr1, lngHdrX, lngHdrY, font1)

        '--next line is address..-

        font1.lngSize = 8 '== Printer.FontSize = 8
        font1.bBold = True '== Printer.FontBold = True

        lngXpos = lngHdrX '==  Printer.CurrentX = lngHdrX
        If Not (mColBusiness Is Nothing) Then
            '==s1 = mColBusiness("BusinessName")
            '==Printer.Print IIf((s1 <> ""), s1, "JobTracking Business")
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessName"), lngHdrX, lngYpos, font1)
            '==Printer.Print mColBusiness("BusinessAddress1")
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessAddress1"), lngHdrX, lngYpos, font1)
            '==Printer.Print mColBusiness("BusinessAddress2")
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessAddress2"), lngHdrX, lngYpos, font1)
            '==Printer.Print mColBusiness("BusinessState") & "  " & mColBusiness("BusinessPostCode")
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessState") & "  " & _
                                           mColBusiness.Item("BusinessPostCode"), lngHdrX, lngYpos, font1)
        End If '--nothing-
        '== lngYpos = mlPrintTextString("--", lngHdrX, lngYpos, font1)
        lngYpos = mlPrintTextString(ev, mStrHeaderDate, lngHdrX, lngYpos, font1)

        If mlRA_id > 0 Then
            '--draw box for RA No..-
            lngYpos = mlDrawBox(ev, lngTicketX, lngTicketY, lngTicketWidth, lngTicketDepth, lngTicketColour)
            '--print ticket no..-
            font1.sName = "Courier New" '= Printer.FontName = "Courier New"
            font1.lngSize = 18 '==.FontSize = 18
            font1.bBold = True '=.FontBold = True
            s1 = "Home RA No:" & VB6.Format(mlRA_id, "  000")
            lngYpos = mlPrintTextString(ev, s1, lngTicketX, lngTicketY, font1)
            '--  newline..  same font..
            lngYpos = mlPrintTextString(ev, "", lngTicketX, lngYpos, font1)
        End If
        font1.lngSize = 12 '==   Printer.FontSize = 12
        font1.bBold = True '==  Printer.FontBold = True
        font1.sName = "Lucida Sans" '== Printer.FontName = "Tahoma"
        If msSupplierRMA <> "" Then
            lngYpos = mlPrintTextString(ev, "Suppliers RMA No:", lngTicketX, lngYpos, font1)
            font1.sName = "Courier New" '==  Printer.FontName = "Courier New"
            lngYpos = mlPrintTextString(ev, msSupplierRMA, lngTicketX, lngYpos, font1)
        End If
        font1.lngSize = 10
        '--  Newline..   '==Printer.Print
        lngYpos = mlPrintTextString(ev, "", lngTicketX, lngYpos, font1)
        lngStatusPos = lngYpos '--  save--
        '-- status..--
        If (sStatus <> "") Then
            font1.sName = "Lucida Sans"
            font1.lngSize = 10
            lngYpos = mlPrintTextString(ev, "RA Status: ", lngTicketX, lngYpos, font1)
            '=font1.sName = "Courier New" '= Printer.FontName = "Courier New"
            lngYpos = mlPrintTextString(ev, sStatus, lngTicketX + 1260, lngStatusPos, font1)
        End If

        '-- end of header..-
        '--  main line..--1800 twips down from top..-
        Call mlDrawLine(ev, lngLeftMarg, K_HEADER_BOTTOM, mlPrtWidth)

    End Function '--RA header..--
    '= = = = = = = = = = = = = = = = =
    '-===FF->


    '== Page FOOTER for Service Record --
    '--     (ALL pages..) ==

    '=  PrintDocument.OriginAtMargins Property (FALSE for us..).
    '= Gets or sets a value indicating whether the position of a graphics object associated with a page is located 
    '=  just inside the user-specified margins 
    '=   or at the top-left corner of the printable area of the page.
    '- --
    '==  https://msdn.microsoft.com/en-us/library/system.drawing.printing.printdocument.originatmargins(v=vs.90).aspx

    Private Function mbJobMaintPageFooter(ByVal ev As PrintPageEventArgs) As Boolean
        Dim font1 As userFontDef
        Dim lngXpos, lngYpos, L1 As Integer
        Dim sText As String

        '=3203.103-
        '-- A Rectangle has Left, Top, Width, height--
        Dim rectPageBounds As Rectangle = ev.PageBounds    '-- printable rectangle-
        Dim intPrintHeight As Integer = rectPageBounds.Height - 16

        '== Printer.CurrentX = 240
        lngXpos = K_LEFT_MARG  '= 28  '= (16 * PRT_UNIT) 
        lngYpos = intPrintHeight - 40  '== 1076  

        With font1
            .sName = "Lucida Sans"
            .lngSize = 7
            .bBold = False
            .bUnderline = False
            .bItalic = False
        End With
        '== Printer.Print mStrVersion & ETC..
        sText = mStrVersion & IIf((msItemBarcodeFontName <> ""), "   -BarcodeFont: " & _
                       msItemBarcodeFontName & ":" & CStr(mlItemBarcodeFontSize) & "pt", Space(30))
        '-- 3203.218= Print Version first..
        '==  THEN print date at the right..
        L1 = mlPrintTextString(ev, sText, lngXpos, lngYpos, font1)
        sText = "== Printed: " & VB6.Format(Now, "dd-mmm-yyyy hh:mm")
        lngYpos = mlPrintTextString(ev, sText, lngXpos + mlPrtWidth - 160, lngYpos, font1)
        Call mlDrawLine(ev, lngXpos, lngYpos, mlPrtWidth)

    End Function '--Footer..-
    '= = = = = = = = = = = = =
    '-===FF->

    '-- E V E N T  H A N D L E R S functions --
    '---       `for  P u b l i c  M e t h o d s..--
    '-- E V E N T  H A N D L E R S functions  
    '---        for  P u b l i c  M e t h o d s..--

    '-- PAGE EVENT for Print Job Labels..--

    Private Function mbPrintJobLabels_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
        Dim lngTop, lngLHS As Integer
        Dim lngXpos, lngYpos As Integer
        Dim font1 As userFontDef
        Dim lngJobId As Integer
        Dim sCustomer, s1 As String

        '--  get calling values..--
        lngJobId = mLngJobId
        '==intNoLabels = mIntNoLabels
        sCustomer = msCustomer

        lngLHS = (12 * PRT_UNIT) '== 180 twips-    '== 60
        lngTop = (23 * PRT_UNIT) '== 345 twips-    '==60
        font1.bUnderline = False

        '--  Must print this time..
        '== For ix = 1 To intNoLabels
        '--print Label with Job no..-
        lngXpos = lngLHS '== Printer.CurrentX = lngLHS
        lngYpos = lngTop '== Printer.CurrentY = lngTop
        font1.sName = "Lucida Sans" '== printer.FontName = "Verdana"
        font1.lngSize = 14 '==  Printer.FontSize = 14
        font1.bBold = True '==  Printer.FontBold = True
        '== Printer.Print " JobNo: "
        Dim usercolor1 As textColour = textColour.black
        s1 = " JobNo: "
        If mbIsRAlabel Then
            s1 = "RA No:"
            '--RAs330- Docket printers are black and white..
            '==usercolor1 = textColour.DarkViolet
        End If
        lngYpos = mlPrintTextString(ev, s1, lngXpos, lngYpos, font1, usercolor1)
        '== Printer.CurrentX = lngLHS

        s1 = VB6.Format(lngJobId, "  0000")
        '==Printer.Print Format(lngJobId, "  0000")
        lngXpos = lngLHS '== Printer.CurrentX = lngLHS
        lngYpos = mlPrintTextString(ev, s1, lngXpos, lngYpos, font1, usercolor1)

        If (msItemBarcodeFontName <> "") Then '--have barcode..-
            font1.bBold = False '== Printer.FontBold = False
            lngXpos = lngLHS + mlGetTextWidth(ev, s1, font1) + 16  '= 60 twips.

            lngYpos = lngTop '== Printer.CurrentY = lngTop
            font1.sName = msItemBarcodeFontName '== Printer.FontName = msItemBarcodeFontName
            font1.lngSize = mlItemBarcodeFontSize '= Printer.FontSize = mlItemBarcodeFontSize
            lngYpos = mlPrintTextString(ev, "*" & lngJobId & "*", lngXpos, lngYpos, font1)
            '--next line
            font1.sName = "Lucida Sans" '== Printer.FontName = "Verdana"
        End If
        font1.bBold = True '== Printer.FontBold = True
        font1.lngSize = 8 '== Printer.FontSize = 8
        lngXpos = lngLHS '== Printer.CurrentX = lngLHS
        lngYpos = mlPrintTextString(ev, sCustomer, lngXpos, lngYpos, font1)
        '==  Printer.NewPage() '--to next label..-
        '== Next ix

        mIntPageNo += 1

        '-- Check to see if more pages are to be printed.
        ev.HasMorePages = (mIntPageNo < mIntNoLabels)

    End Function '==PrintJobLabels=
    '= = = = = = = = = = = = =
    '-===FF->

    '--  RA's  F O R M S ==
    '--    PRINT PAGE EVENT.--

    '-- Print the RA form.--

    Private Function mbPrintRAForm_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean

        Dim sHdr1, sText As String
        Dim s1, sAddress1 As String
        Dim sStaff As String
        Dim sRem As String
        Dim sCustText As String
        Dim lngTxtHt, lngTxtWidth As Integer
        Dim L1, kx, iPos, ix, lngYpos, L2 As Integer
        Dim lngError As Integer
        '==Dim lngTicketX, lngTicketY As Long
        Dim lngTermsX, lngTermsY As Integer
        Dim lngLHSX, lngRHSX As Integer
        Dim lngSigX, lngSigY As Integer
        Dim lngHdrX, lngMainTop As Integer
        Dim lngBoxMargin, lngMainDepth As Integer
        Dim sTermsText As String
        Dim sUsernames As String
        Dim lngShippingX, lngShippingY As Integer
        Dim lngLeftColWidth, lngRHSBoxWidth As Integer
        '==Dim iNoTermsLines As Long
        Dim sNotes As String
        Dim font1 As userFontDef

        mbPrintRAForm_PageEvent = False
        On Error GoTo PrintRA_Error

        lngLHSX = mlPrtDx(16) '==240

        '--  default font -
        font1 = mDefaultUserFont

        sHdr1 = "RA Record"

        lngMainTop = mlPrtDx(133) '== 2000 twips..-
        lngMainDepth = 550  '=mlPrtDx(676) '== 9600 twips..- (RA WAS 9400)
        lngBoxMargin = mlPrtDx(20) '== 300 twips..-
        lngLeftColWidth = 350  '= mlPrtDx(347) '==  5200 twips..-

        '--print full header..--
        Call mbPrintRA_Header(ev, sHdr1, RGB(224, 144, 0), msRAStatus)

        '-- end of header..-

        lngShippingX = lngLHSX '-- rename the bottom box.-
        lngShippingY = mlPrtDx(800) '== 12000  twips '---9600  '-- rename the bottom box.-

        lngRHSX = lngShippingX + lngLeftColWidth + mlPrtDx(24) '=360  '= + 5700
        lngTermsX = lngRHSX '--Sig. boxes now on RHS.--
        lngTermsY = mlPrtDx(800) '== 12000   '--- 8500     '--Sig. boxes now on RHS.--

        '--  Serial NO and Item Details..--
        sText = "<b>Item Details" & vbCrLf & vbCrLf & "<ul>Serial No:" & vbCrLf & "<b>" & msSerialNo & vbCrLf & vbCrLf
        sText = sText & "<ul>Description:" & vbCrLf & msDescription & vbCrLf & vbCrLf
        sText = sText & "<ul>Supplier Code:" & vbCrLf & msItemSupplierCode & vbCrLf & vbCrLf
        sText = sText & "<ul>Item Barcode:" & vbCrLf
        sText = sText & msItemBarcode & vbCrLf & vbCrLf
        sText = sText & "<ul>Invoice No:" & vbCrLf
        sText = sText & msInvoiceInfo & vbCrLf & vbCrLf
        sText = sText & "<ul>Problem Reported:" & vbCrLf
        sText = sText & msProblemReported & vbCrLf & vbCrLf

        '--  customer Info..--
        sCustText = "<ul>Customer: " & msRA_customer & vbCrLf & vbCrLf
        '-- TextInBox uses default font..-
        '== mDefaultUserFont.lngSize = 9
        Call mlPrintTextInBox(ev, sText & sCustText, lngLHSX, lngMainTop, _
                                lngBoxMargin, lngLeftColWidth, lngMainDepth, True, , , , 9)

        '= 3203.1231-- 
        '--  Print Item Image if any..-
        If (Not (imageItem1 Is Nothing)) AndAlso _
                              ((imageItem1.Height > 0) And (imageItem1.Width > 0)) Then
            '- Rect pos-
            Dim intImageRectTop As Integer = lngMainTop + lngMainDepth
            Dim intImageRectleft As Integer = lngLHSX
            '--Max rect must be square.
            Dim intImageMaxWidth As Integer = lngLeftColWidth
            Dim intImageMaxHeight As Integer = intImageMaxWidth '=240
            '-Rectangle has Left, Top, Width, height--
            '-- Make the Rectangle aspect the SAME as the Image..--
            Dim intRh, intRw As Integer   '--rect-
            If (imageItem1.Height > imageItem1.Width) Then   '--portrait-
                intRh = intImageMaxHeight
                intRw = CInt(intImageMaxWidth * (CDbl(imageItem1.Width) / imageItem1.Height))
            Else '-landscape-
                intRw = intImageMaxWidth
                intRh = CInt(intImageMaxHeight * (CDbl(imageItem1.Height) / imageItem1.Width))
            End If
            Dim rectImage1 As New Rectangle(intImageRectleft, intImageRectTop, intRw, intRh)
            ev.Graphics.DrawImage(imageItem1, rectImage1)
        End If  '-image-
        '--RHS --
        '--RHS --
        lngRHSBoxWidth = 382  '== mlPrtWidth - lngRHSX '= 11000 - lngRHSX

        '--RHS Supplier Info..-
        '--RHS Supplier Info..-  2800 twips deep.--
        L1 = 240 '=  mlPrtDx(187) '-- 2800 twips deep--
        '== Printer.FontSize = 9
        Call mlPrintTextInBox(ev, "<b>Supplier Details" & vbCrLf & vbCrLf & msSupplierDetails, _
                                                    lngRHSX, lngMainTop, lngBoxMargin, lngRHSBoxWidth, L1, True)
        '--RHS RA Progress..-
        '== Printer.FontSize = 8
        '== mDefaultUserFont.lngSize = 8
        L2 = (lngMainTop + L1 + mlPrtDx(8))
        Call mlPrintTextInBox(ev, "<b>RA Progress" & vbCrLf & msRA_Progress, _
                                                lngRHSX, L2, lngBoxMargin, lngRHSBoxWidth, L2, True, , , , 8)

        '== Printer.Font.Bold = False

        '--- PRINT Sig Boxes for customer..--
        '--SIGNING- main box..-- 11,000 twips wide.-
        L1 = mlPrtDx(187) '= now 2800 rwips.-   '==2600 '--sign box depth..-
        '== Printer.Line (lngRHSX, lngTermsY)-(mlPrtWidth, lngTermsY + L1), , B '--TERMS- main box..--

        lngYpos = mlDrawBox(ev, lngRHSX, lngTermsY, lngRHSBoxWidth, L1, -1)

        '--draw signing boxes..-
        lngSigX = lngTermsX + mlPrtDx(20)  '== 300
        lngSigY = lngTermsY + mlPrtDx(40)  '==600
        Call mbDrawSigningBoxes(ev, lngSigX, lngSigY, "Print Name", "Date Collected:", mDefaultUserFont, 1)

        '--Collected By..-
        font1.bBold = True '== Printer.FontBold = True
        font1.bItalic = True '==  Printer.FontItalic = True
        s1 = "Item Collected By:"

        '--Collected By..-
        Call mlPrintTextString(ev, s1, lngSigX, lngTermsY + 20, font1)

        '--  do footer stuff..-
        Call mbJobMaintPageFooter(ev)

        mbPrintRAForm_PageEvent = True '--ok..-
        '-- end print --
        '==MsgBox "OK.. RA Document has been sent to the printer:" & vbCrLf & vbCrLf & Printer.DeviceName & "..", vbInformation

        '--only one page..-
        ev.HasMorePages = False

        Exit Function

PrintRA_Error:

        lngError = Err().Number
        MsgBox("!! ERROR in PrintDocs.PrintRAForm." & vbCrLf & _
                      "Error code: " & lngError & " = " & ErrorToString(lngError), MsgBoxStyle.Exclamation)
        mbPrintRAForm_PageEvent = False

    End Function '-- print..--
    '= = = = = = = = = = = = 
    '-===FF->

    '--  RA Supplier LABEL --

    '--  Print Shipping Label --
    '--  Print Shipping Label --
    '-- 1st page- Print Shipping Name/Address ONLY.
    '--  2nd and Subseq. pages.. print packing slip with item list.--

    Public Function mbPrintShippingLabel_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
        '= mlPrtWidth = 734
        Const k_WIDTH_SEQUENCE = 44             '--width of sequence no. column.-
        Const k_WIDTH_ID = 60                 '--width of RA_id column.-
        Const k_WIDTH_SUPPLIER_RMA = 160      '--width of RMA column.-
        Const k_WIDTH_SERIALNO = 130          '--width of SerNo column.-
        Const k_WIDTH_DESCRIPTION = 260        '--width of description column.-
        Const k_WIDTH_SUPPLIERCODE = 80      '--width of SupplierCode column.-

        Const k_HDR_HEIGHT = 26
        Const K_SHIP_TO_BOX_DEPTH = 400

        Dim sHdr1, sText As String
        Dim s1, strPageNo As String
        Dim lngHdrX As Integer
        Dim ix, iPos, kx As Integer
        Dim lngShippingX, lngShippingY As Integer
        '== Dim lngRHSX As Integer
        '=Dim lngYpos, lngXpos, L1, x1, intError As Integer
        Dim lngAddressMargin As Integer
        Dim lngRAColour As Integer
        Dim font1 As userFontDef
        Dim saveFont As userFontDef
        '-multi stuff..
        Dim intGreyBGColour As Integer = &HE0E0E0&
        Dim fillColor As Color
        Dim intXpos, intXpos2, intYpos, intYpos2 As Integer
        Dim intHdrX As Integer = 150
        Dim intHdrY As Integer = 24
        Dim intLeftMarg As Integer = K_LEFT_MARG
        '-grid-
        Dim intGridYpos, intGridYdepth As Integer
        Dim intItemsRemaining, intLinesAvailable As Integer
        Dim penGrid As Pen = Pens.LightGray
        Dim colItem As Collection

        '= On Error GoTo PrintShipping_Error
        fillColor = ColorTranslator.FromOle(intGreyBGColour)
        mbPrintShippingLabel_PageEvent = False

        '-- SET PRINTER..-
        sHdr1 = "RA Record (Shipping)"

        '-- All Pages start..-
        '-- All Pages start..-
        lngShippingX = K_LEFT_MARG   '-- rename the bottom box.-
        lngShippingY = K_HEADER_BOTTOM + 12
        lngAddressMargin = 160  '= mlPrtDx(160) ' '--insets the address.-
        Try
            lngRAColour = &HC0C0FF '--pink-
            '--print full header..--
            Call mbPrintRA_Header(ev, sHdr1, lngRAColour, msRAStatus)
            '-- end of header..-
        Catch ex As Exception
            MsgBox("Runtime error in print ShippingLabel startup." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            ev.HasMorePages = False
            Exit Function
        End Try
        '---  Shipping Box..--
        '-- have to use default font..  so save it--
        saveFont = mDefaultUserFont
        font1 = mDefaultUserFont

        '-- 1st page only- Print Shipping Name/Address ONLY.
        Try
            If (mIntPageNo <= 0) Then
                font1.lngSize = 20 '== Printer.FontSize = 22
                '-- Over Print "Ship To:" --
                font1.bBold = True
                intXpos = mlPrtDx(27) '== 400 twips--
                intYpos = lngShippingY + 27 '== + 400
                font1.bItalic = True
                Call mlPrintTextString(ev, "Ship To:", K_LEFT_MARG + 8, intYpos, font1)

                '= font1.lngSize = 20 '== Printer.FontSize = 22
                font1.bBold = False '== Printer.Font.Bold = True
                font1.bItalic = False
                sText = msSupplier & vbCrLf & msSupplierAddressInfo & vbCrLf & _
                                 "Telephone: " & msSupplierMainPhone & vbCrLf '= & "       [RMA No: " & msSupplierRMA & "]"
                If msSupplierRMA <> "" Then
                    sText &= "       [RMA No: " & msSupplierRMA & "]"
                End If
                '-- Print Address- USES optionál FONT name,size..--
                intYpos = mlPrintTextInBox(ev, sText, lngShippingX, lngShippingY, _
                                              lngAddressMargin, mlPrtWidth, K_SHIP_TO_BOX_DEPTH, True, , , , 20)
                mIntItemsPrinted = 0   '--just started-
                If (Not (mColRA_Items Is Nothing)) AndAlso (mColRA_Items.Count > 0) Then '-items to come..
                    '- make list of RMA nos..
                    sText = ""   '= "RMA Numbers are: " & vbCrLf
                    For Each colItem In mColRA_Items
                        If sText <> "" Then
                            sText &= "; "
                        End If
                        sText &= colItem("RA_SupplierRMA_No")
                    Next
                    font1.lngSize = 10 '== Printer.FontSize = 22
                    intXpos = K_LEFT_MARG + 8
                    intYpos = lngShippingY + K_SHIP_TO_BOX_DEPTH + 11
                    '= intYpos = mlPrintTextString(ev, _
                    '=                         "Packing slip is enclosed.", intXpos, intYpos, font1)
                    intYpos = mlPrintTextInBox(ev, "Packing slip is enclosed." & vbCrLf & _
                                               msSupplier & " RMA Numbers are: " & vbCrLf & sText & ".", _
                                                 intXpos, intYpos, 40, mlPrtWidth, 200, True, , , , 12)
                    mIntPageNo = 1  '--don't come through here again !!
                    ev.HasMorePages = True
                Else
                    ev.HasMorePages = False  '--done.. address page only.
                End If
                '--  do footer stuff..-
                Call mbJobMaintPageFooter(ev)
                Exit Function
                '------ done this page ---------
                intItemsRemaining = mColRA_Items.Count - mIntItemsPrinted
            Else  '-- P1/P2 ++. 1st and subsequent Packing slip pages-
                intYpos = K_HEADER_BOTTOM + 16
                intXpos = K_LEFT_MARG + 8
                If (mIntPageNo = 1) Then  '-1st page of packing slip.
                    font1.bUnderline = True
                    font1.lngSize = 12 '== Printer.FontSize = 22
                    intYpos = mlPrintTextString(ev, "Returns Package has " & mColRA_Items.Count & " items:", intXpos, intYpos, font1)
                    '-- size up GRID for Detail Lines..--
                Else  '-- P2 ++. subsequent Oacking slip pages-
                    intYpos = mlPrintTextString(ev, "Continuing Package items..", intXpos, intYpos, font1)
                    intYpos += 16
                End If
                font1.lngSize = 9
                '-- GRID for Detail Lines..--
                intGridYpos = intYpos + 12
                intItemsRemaining = mColRA_Items.Count - mIntItemsPrinted
                intGridYdepth = 900
                intLinesAvailable = 28  '- SAY 31 px per line..
                intGridYpos = intYpos + 12
            End If  '=p0- 
        Catch ex As Exception
            MsgBox("Runtime error in printing ShippingLabel Address." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            ev.HasMorePages = False
            Exit Function
        End Try
        font1.bUnderline = False
        font1.lngSize = 9

        '--  all packing slip pages --
        '--  all packing slip pages --
        mIntPageNo += 1
        strPageNo = "Returns to " & msSupplier & "-  Page: " & CStr(mIntPageNo - 1)  '- (mIntPageNo includes Address Page).
        '- print page no at top right..
        Call mlPrintTextString(ev, strPageNo, K_LEFT_MARG + mlPrtWidth - 250, K_HEADER_BOTTOM - 18, font1)
        intXpos = K_LEFT_MARG  '= intLeftMarg

        '--draw the "grid" and the items...
        Try
            '--draw the "grid"..
            Call mlDrawLine(ev, intXpos, intGridYpos, mlPrtWidth)  '--top bar-
            '--column lines- 6 spaces, seven lines-
            Dim arrayIntWidths() As Integer = {k_WIDTH_SEQUENCE, k_WIDTH_ID, k_WIDTH_SUPPLIER_RMA, _
                                              k_WIDTH_SERIALNO, k_WIDTH_DESCRIPTION, k_WIDTH_SUPPLIERCODE}
            Dim arrayStrHdrs() As String = _
                          {"No.", "RA_id", "Supplier RMA", _
                                 "Serial No", "Description", "Suppl.Code"}
            For ix = 0 To UBound(arrayIntWidths)
                ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)
                intXpos += arrayIntWidths(ix)
            Next ix
            '--last vert line..
            ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)

            intXpos = intLeftMarg
            '-bottom bar-
            Call mlDrawLine(ev, intXpos, intGridYpos + intGridYdepth, mlPrtWidth)  '--bottom bar-

            '-- column header text line..
            '-- Can't use TAB stops because some items are Left-just and some are right..-
            '-- fill column headers BG..
            intXpos = intLeftMarg
            ev.Graphics.FillRectangle(New SolidBrush(fillColor), intXpos, intGridYpos, mlPrtWidth, k_HDR_HEIGHT)
            '-- PRINT column header TEXTS..
            '-- PRINT column header TEXTS..
            font1.lngSize = 8
            font1.bBold = True
            For ix = 0 To UBound(arrayStrHdrs)
                '-- all centre align.
                Call mIntPrintTextInRectangle(ev, arrayStrHdrs(ix), intXpos, intGridYpos, _
                                               arrayIntWidths(ix), k_HDR_HEIGHT, font1, textColour.black, False, True, True)
                intXpos += arrayIntWidths(ix)
            Next ix
            intYpos = intGridYpos + k_HDR_HEIGHT
            '--Print Actual detail lines..
            '-- make line rect deep enough for one line..
            Dim intItemLineHeight As Integer = k_HDR_HEIGHT '= + (k_HDR_HEIGHT \ 4)
            Dim intCurrentYpos As Integer   '--save for shading-

            '==3311.319=
            If (Not (mColRA_Items Is Nothing)) AndAlso (mColRA_Items.Count > 0) Then
                font1.lngSize = 8 '== Printer.FontSize = 22
                font1.bBold = False
                intXpos = K_LEFT_MARG
                For Each col1 As Collection In mColRA_Items
                    '= s1 = "RA_id=" & col1("ra_id") & ";  Description=" & col1("RM_ItemDescription")
                    '= intYpos = mlPrintTextString(ev, s1, intXpos, intYpos, font1)
                Next '-col1-
            End If  '-mColRA_Items nothing-

            '-- fill with as many lines as possible..
            If (mIntItemsPrinted < mColRA_Items.Count) Then
                Dim arrayStrData() As String
                Dim intItemx, intItemStart As Integer
                Dim bRightAlign As Boolean
                intItemStart = mIntItemsPrinted + 1
                For intItemx = intItemStart To mColRA_Items.Count
                    colItem = mColRA_Items(intItemx)
                    arrayStrData = {CStr(intItemx) & "  ", colItem("ra_id") & "  ", colItem("RA_SupplierRMA_No"), _
                                             colItem("RA_SerialNumber"), _
                                                   colItem("RM_ItemDescription"), colItem("RM_ItemSupplierCode")}
                    '--  shade alternate rows-
                    If (intLinesAvailable Mod 2) = 1 Then
                        ev.Graphics.FillRectangle(New SolidBrush(ColorTranslator.FromOle(&HF0F0F0)), _
                                                             intLeftMarg, intCurrentYpos, mlPrtWidth, intItemLineHeight)
                    End If
                    '- print all fields-
                    intXpos = K_LEFT_MARG
                    For ix = 0 To UBound(arrayStrHdrs)
                        bRightAlign = False
                        If ix <= 1 Then
                            bRightAlign = True '-seq-no and Ra-id-
                        End If
                        '- add leading and trailing spaces for padding.
                        Call mIntPrintTextInRectangle(ev, " " & arrayStrData(ix) & " ", intXpos, intYpos, _
                                                          arrayIntWidths(ix), k_HDR_HEIGHT, font1, textColour.black, bRightAlign)
                        intXpos += arrayIntWidths(ix)
                    Next ix
                    '- move to next line pos.
                    intYpos += intItemLineHeight  '= L1
                    intCurrentYpos = intYpos
                    '--  Compute total ITEM tax..
                    mIntItemsPrinted += 1
                    intLinesAvailable -= 1

                    '--drop out if no more room.. next page will continue.
                    If (intLinesAvailable <= 0) Then
                        Exit For
                    End If
                Next intItemx
            End If '- mIntItemsPrinted'-
            '-re-draw grid verticals- (was overprinted by line shading..-
            intXpos = K_LEFT_MARG  '= intLeftMarg
            For ix = 0 To UBound(arrayIntWidths)
                ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos + k_HDR_HEIGHT, intXpos, intGridYpos + intGridYdepth)
                intXpos += arrayIntWidths(ix)
            Next ix
            '--done-
        Catch ex As Exception
            MsgBox("Runtime error in printing ShippingLabel Items." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            ev.HasMorePages = False
            Exit Function

        End Try
        '--  do footer stuff..-
         If (mIntItemsPrinted < mColRA_Items.Count) Then '-items to come..
            ev.HasMorePages = True
        Else
            intYpos = mlPrintTextString(ev, "== The End ==", K_LEFT_MARG, intYpos, font1)
            ev.HasMorePages = False  '--done.. all printedy.
        End If
        Call mbJobMaintPageFooter(ev)

        mbPrintShippingLabel_PageEvent = True '--ok.-
        '-- end print --
  
        Exit Function

    End Function '--ship label..-
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--  end of Doc handler functions...-
    '--  end of Doc handler functions...-


    '--  Main print Page EVENT handler..--
    '--  Main print Page EVENT handler..--
    '--   FOR ALL PRINT FUNCTIONS..--

    Private Sub printDocument1_PrintPage(ByVal sender As Object, _
                                               ByVal ev As PrintPageEventArgs) _
                                            Handles printDocument1.PrintPage

        '--  check static var to determine doc-type..--
        '--   and call associated event function..

        Select Case mlPrintDocType
            Case printDocType.JobLabels
                Call mbPrintJobLabels_PageEvent(ev)
                '=Case printDocType.Receipt
                '=    Call mbPrintReceipt_PageEvent(ev)
             Case printDocType.RAForm
                Call mbPrintRAForm_PageEvent(ev)
            Case printDocType.RAShippingLabel
                Call mbPrintShippingLabel_PageEvent(ev)
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

    '--  Just Records Doc-type to print and calls COMMON print..

    '-- Job Labels.--
    '-- Job Labels.--
    '== 3203.212=   Also for RA's..--

    Public Function PrintJobLabels(ByVal lngJobId As Integer, _
                                          ByVal intNoLabels As Integer, _
                                             ByVal sCustomer As String, _
                                             Optional ByVal bIsRALabel As Boolean = False) As Boolean

        PrintJobLabels = False
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.JobLabels
        mLngJobId = lngJobId
        mIntNoLabels = intNoLabels
        msCustomer = sCustomer
        '=3203.212=
        mbIsRAlabel = bIsRALabel

        mIntPageNo = 0

        If (msSelectedPrinterName = "") Then
            MsgBox("Label printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        PrintJobLabels = True

        '-- WE use PHYSICAL PAGE- as Graphics Origin..-
        printDocument1.OriginAtMargins = False
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            MsgBox("Error in printing JobLabels.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintJobLabels = False
        End Try

    End Function  '-- Job Labels..-
    '= = = = = = = = = = = = = = 
    '-===FF->

    '--PrintRA-

    Public Function PrintRAForm() As Boolean

        PrintRAForm = False
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.RAForm

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Receipt printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        PrintRAForm = True
        '-- WE use PHYSICAL PAGE- as Graphics Origin..-
        printDocument1.OriginAtMargins = False
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName

            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            MsgBox("Error in printing RA Form..." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintRAForm = False
        End Try

    End Function  '--PrintRAForm-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-P r i n t  S h i p p i n g  L a b e l-

    Public Function PrintShippingLabel(Optional ByRef colRA_Items As Collection = Nothing) As Boolean

        PrintShippingLabel = False
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.RAShippingLabel

        '=3311.319- Bunch of RA's  For group print -
        mColRA_Items = colRA_Items
        mIntPageNo = 0

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("A4 printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        PrintShippingLabel = True
        '-- WE use PHYSICAL PAGE- as Graphics Origin..-
        printDocument1.OriginAtMargins = False
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            '== MessageBox.Show(ex.Message)
            MsgBox("Error in printing Shipping label..." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintShippingLabel = False
        End Try
    End Function  '-- PrintShippingLabel-
    '= = = = = = = = = = = = = =

    '= = = = = = = = = = = = = =
    '== end class.==
End Class