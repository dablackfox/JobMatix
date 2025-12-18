Option Strict Off
Option Explicit On

Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Drawing2D
Imports System.Math
Imports VB = Microsoft.VisualBasic


Public Class clsDebtorsReport


    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =  = = = = = = =

    '==  Target is new Build 4251..  05-June-2020..
    '==
    '==        colThisCustomerReversedInvoices = colThisCustomer.Item("reversedInvoices")
    '==                    
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==
    '==
    '==   1. MAIN THEME is implementation of ACCOUNT-INVOICE-REVERSAL (Account "refund")
    '==       --  Involves creating a REFUND (onAccount) full transaction as a mirror of Original.
    '==       --  Not allowed if payments have been made towards original Invoice.
    '==       --  Not allowed if original Invoice involved DELIVERY of a Job or a Layby..
    '==       --  Transaction is accessible only from frmShowInvoice (showing original Invoice)..
    '==                 Needs NEW CLASS  "clsAccountReversal".. 
    '==       --  Reversed Invoicess noted on Debtors Report.
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =  = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== Fixes to Build 4259.0730  
    '==
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)  26-Aug-2020..
    '==
    '==    E.  Debtors Report-  Allow space for Reversed Invoices so as to stop overprinting the next customer.. . 
    '==         (Martin email 26-Aug-2020)..  Also, show Reversed Invoices only on detailed report..
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '==
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==
    '== == Debtors Report-   
    '==     Show Reversed Invoices only on detailed report, and only for current period..  
    '==     --  On Summary, DO NOT show customers with zero balance, even if thaey have reversed invoices in current period.
    '==
    '==      --  PLUS-  THIS IS the new class "clsDebtorsReport" to give report its own class.
    '==
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==

    '-- Printer units..-
    '---- for .net Graphics..
    '----- printer display units:  100/inch..--

    '==   -- 4219.1113.  06-Nov-2019-  Started 06-November-2019-
    '==      -- Statements printing-  Adjust Lines per page to stop overprinting.. 

    Const k_PRTWIDTH = 760
    Const k_LEFTMARGIN = 32

    Const k_STATEMENT_DETAIL_LINES_PAGE1 = 40  '- at 8 per inch.- (3mm per line)
    Const k_STATEMENT_DETAIL_LINES = 54  '- at 8 per inch.- (3mm per line)
    Const k_STATEMENT_SUMMARY_LINES = 16  '- at 8 per inch.-
    Const k_STATEMENT_DETAIL_DEPTH = 640  '- 160mm . 
    Const k_STATEMENT_SUMMARY_TOP = 880  '=930  '- ypos for totals.- Abslute-

    '--  FOR vb.net:
    Const PRT_UNIT = 1

    '--  FOR vb6:
    '--1440 twips per inch.
    '==  for VB^.. Const PRT_UNIT As Short = 15 '--twips per pixel.. (14.4 for PRTunits)
    '===========================================

    '--  font type -- SEE modprintSubs-

    '= Private Structure userFontDef
    '= Dim sName As String '-- font name-
    '= Dim lngSize As Integer
    '= Dim bBold As Boolean
    '= Dim bUnderline As Boolean
    '= Dim bItalic As Boolean
    '= End Structure
    '= = = = = = = = = = = =  == 


    Private Enum printDocType
        Receipt
        SalesInvoice
        dgv  '--dataGridView
        StockLabels
        LaybyLabels
        Statement
        DebtorsReport
    End Enum
    '= = = = = = = = = = = =  == 

    '--  textcolour -- SEE modprintSubs-

    '= Private Enum textColour
    '=     orange
    '=     magenta
    '=     black
    '=     grey
    '=     white
    '= End Enum
    '= = = = = = = = = = = = = 

    '--  Main printer object..-- 
    '--    DOES ALL PRINTING..--
    Private WithEvents printDocument1 As New System.Drawing.Printing.PrintDocument()
    '==AddHandler printDocument1.PrintPage, AddressOf Me.printDocument1_PrintPage

    '--3403.1102= use the sub-class
    Private printPreviewDialog1 As New PrintPreviewDialogWithPrinter

    '-- -  Cpmpletion Info..-
    Private mbPrintingCompleted As Boolean = False
    Private mbPrintError As Boolean = False
    Private msPrintErrorMsg As String = ""

    '--  STATIC VARS for all printing..-  
    Private mDefaultUserFont As userFontDef
    Private mlPrtWidth As Integer '--pixels or twips (11,000).-

    Private msVersionPOS As String = ""

    Private msColourPrinterName As String = ""
    Private msReceiptPrinterName As String = ""
    Private msLabelPrinterName As String = ""

    '== Private mSdSettings As clsStrDictionary '--  holds local job settings..

    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo

    '- business info-
    '--  Business Info-
    '--  Business Info-
    Private msBusinessABN As String
    '= Private msBusinessUser As String
    '= Private msJT2SecurityIdOriginal As String '--as stored in SytemInfo in Row "JT2SecurityId"..-
    '== Private msJT2SecurityId As String '-- AS computed from ABN DateCreated.  --
    Private msBusinessName As String
    Private msBusinessAddress1 As String
    Private msBusinessAddress2 As String
    Private msBusinessShortName As String
    Private msBusinessPhone As String
    Private msBusinessPostCode As String
    Private msBusinessState As String


    '--  SAVE STATE..--
    '--   JOB MAINT page state vars..--

    '--  save doc-type currently being printed..-- 
    Private mlPrintDocType As printDocType '== = -1

    '--  Print forms stuff..--

    Private mImageUserLogo As Image '= System.Drawing.Bitmap  '-= Object

    Private mIntStaff_id As Integer = -1
    Private msStaffName As String = ""
    Private mColBusiness As Collection
    Private mStrVersion As String = ""
    Private mStrHeaderDate As String = ""
    Private mStrHeaderDate2 As String = ""

    Private mColCustomerInfo As Collection  '-Statements and whoever esle..

    '--For PDF's -
    Private msPrintToFileFullPath As String = ""

    Private mIntItemsPrinted As Integer = 0

    '-- Printer to Use...--
    Private msSelectedPrinterName As String = ""
    Private mIntPageNo As Integer = 0

    Private miPageNo As Integer = 0
    Private mDateCutOff As Date

    '-- Debtors Report--
    Private mColReportCustomers As Collection
    Private mIntCustomerCount As Integer  '--track customers-
    Private mIntInvoiceCount As Integer   '-track invoices per customer-

    '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
    Private mIntCustomerSequence As Integer  '--track customers-
    '== END  Target-New-Build-4267 -- (Started 18-Sep-2020)


    Private mDecReportTotalTax As Decimal
    Private mDecReportTotalDebits As Decimal
    Private mDecReportTotalCredits As Decimal

    Private mDecReportTotalOutstanding As Decimal
    '-- aged totals.-
    Private mDecReportAgedTotalCurrent As Decimal
    Private mDecReportAgedTotal30 As Decimal
    Private mDecReportAgedTotal60 As Decimal
    Private mDecReportAgedTotal90 As Decimal

    Private mIntReportLineCount As Integer
    '3519.0224--- Debtors Report--
    Private mbDebtorsReportSummaryOnly As Boolean = False
    '-4201.1027--- Debtors Report--
    Private msDebtorsReportSubHeading As String = ""

    Private mDecStatementTotalDebits As Decimal
    '= Private mDecStatementTotalDebitsNonTaxable As Decimal
    Private mDecStatementTotalCredits As Decimal

    Private mDecStatementTotalOutstanding As Decimal
    '-- aged totals.-
    Private mDecStatementAgedTotalCurrent As Decimal
    Private mDecStatementAgedTotal30 As Decimal
    Private mDecStatementAgedTotal60 As Decimal
    Private mDecStatementAgedTotal90 As Decimal

    '= = = = = = = = = = = = = = = = = = = =  = = = = = =
    '-===FF->

    '--  property parameters..--

    '-version-
    WriteOnly Property versionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
        End Set
    End Property  '--version--
    '= = = = = = = = = = = = = = = = = = == 

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)

            msStaffName = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = ==

    WriteOnly Property xxxx_Settings() As clsStrDictionary
        Set(ByVal value As clsStrDictionary)
            '= mSdSettings = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = =

    WriteOnly Property SystemInfo() As clsSystemInfo '=3301.428=  clsStrDictionary
        Set(ByVal value As clsSystemInfo)
            mSysInfo1 = value  '=  mSdSystemInfo = value
        End Set
    End Property '-systeminfo-
    '= = = = = = = = = = = = = = = = =


    '-- Printer Names..--
    '--  use for vb.net.--

    '--  printers..--
    '--  printers..--

    WriteOnly Property ColourPrinterName() As String
        Set(ByVal Value As String)
            msColourPrinterName = Value
        End Set
    End Property '--colour.--
    '= = = = = = = =  = = =

    WriteOnly Property ReceiptPrinterName() As String
        Set(ByVal Value As String)
            msReceiptPrinterName = Value
        End Set
    End Property '--receipt.--
    '= = = = = = = =  = = =

    WriteOnly Property LabelPrinterName() As String
        Set(ByVal Value As String)
            msLabelPrinterName = Value
        End Set
    End Property '--label.--
    '= = = = = = = =  = = =


    WriteOnly Property PrtSelectedPrinterName() As String
        Set(ByVal Value As String)

            msSelectedPrinterName = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = =

    '=3411.0109= -msPrintToFileFullPath=

    WriteOnly Property PrintToFileFullPath() As String
        Set(ByVal Value As String)
            msPrintToFileFullPath = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = =

    '--  picture object..-
    WriteOnly Property UserLogo() As Image
        Set(ByVal Value As Image)
            mImageUserLogo = Value
        End Set
    End Property
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

    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--Print results-
    '--Print results-

    ReadOnly Property PrintingCompleted As Boolean
        Get
            PrintingCompleted = mbPrintingCompleted
        End Get
    End Property  '-completed-
    '= = = = = = = =  = = = = = = = = =

    ReadOnly Property PrintError As Boolean
        Get
            PrintError = mbPrintError
        End Get
    End Property  '-error-
    '= = = = = = = =  = = = = = = = = =

    ReadOnly Property PrintErrorMsg As String
        Get
            PrintErrorMsg = msPrintErrorMsg
        End Get
    End Property  '--error msg-
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '- Keys are all UCASE as base dictionary class is case sensitive..-

    Private Function mbInitialiseBusiness() As Boolean

        If Not mSysInfo1 Is Nothing Then  '= mSdSystemInfo Is Nothing Then
            msBusinessABN = mSysInfo1.item("BUSINESSABN")
            msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
            '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
            msBusinessName = mSysInfo1.item("BUSINESSNAME")
            msBusinessAddress1 = mSysInfo1.item("BUSINESSADDRESS1")
            msBusinessAddress2 = mSysInfo1.item("BUSINESSADDRESS2")
            msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
            msBusinessShortName = Replace(msBusinessShortName, "_", "")  '=3519.0224--drop underscores..

            msBusinessState = mSysInfo1.item("BUSINESSSTATE")
            msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
            msBusinessPhone = mSysInfo1.item("BUSINESSPHONE")
            '--terms and Bank details.--
            '-Trams and bank Account-
            'msPOS_Terms = mSysInfo1.item("POS_ACCOUNTTERMS")
            'msBankName = mSysInfo1.item("POS_BUSINESSBANKNAME")
            'msAccountName = mSysInfo1.item("POS_BUSINESSACCOUNTNAME")
            'msBSB1 = mSysInfo1.item("POS_BUSINESSACCOUNTBSB1")
            'msBSB2 = mSysInfo1.item("POS_BUSINESSACCOUNTBSB2")
            'msAccountNo = mSysInfo1.item("POS_BUSINESSACCOUNTNO")
        End If

    End Function  '-mbInitialiseBusiness-
    '= = = = = = = =  = = = = = = = = =
    '-===FF->

    '-- initialize --
    '-- initialize --

    'UPGRADE_NOTE: class_initialize was upgraded to class_initialize_Renamed. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()

        mlPrtWidth = k_PRTWIDTH '== 760  '== 734  '==pixels--   (734 * PRT_UNIT) '-- 11,010 twips..-
        '--  set default font for PrintText in box..--
        With mDefaultUserFont
            .sName = "Lucida Sans"
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

    '-- General Print Routines..
    '-- General Print Routines..

    Private Function mlDrawLine(ByVal ev As PrintPageEventArgs, _
                                 ByVal lngUL_X As Integer, _
                                  ByVal lngUL_Y As Integer, _
                                   ByVal lngLineWidth As Integer) As Long

        '- exported to subs-
        mlDrawLine = gIntDrawLine(ev, lngUL_X, lngUL_Y, lngLineWidth)
        Exit Function
        '= = = = = =  ==  = = = = = = =

        ' Create pen.
        'Dim blackPen As New Pen(Color.Gray, 1)

        '' Create coordinates of points that define line.
        'Dim x1 As Integer = lngUL_X
        'Dim y1 As Integer = lngUL_Y
        'Dim x2 As Integer = lngUL_X + lngLineWidth
        'Dim y2 As Integer = lngUL_Y

        '' Draw line to screen.
        'ev.Graphics.DrawLine(blackPen, x1, y1, x2, y2)

    End Function  '-- DrawLine --
    '= = = = = = = = = = = = = =


    '-- Input dimension in Pixels--
    '--  Return print dimension according to system used..-
    '---  ie Twips (vb6) or pixels (vb.net..)--

    Private Function mlPrtDx(ByRef lngPixels As Integer) As Integer
        Dim sinResolution As Single

        sinResolution = PRT_UNIT
        mlPrtDx = CInt(sinResolution * lngPixels)

    End Function '- mlPrtUnits-
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Print text string IN RECTANGLE with User font style...--
    '-- Text will be RIGHT aligned if needed....-
    '--  NB:  bCentreAlign overrules leftAlign if true..
    '--  Returns << lineHeight >> of printed line..-

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
                                                  Optional ByVal bCentreAlign As Boolean = False) As Integer

        '- now exported to printSubs.
        mIntPrintTextInRectangle = gIntPrintTextInRectangle(ev, sText, intUL_Xpos, intUL_Ypos, intWidth, intDepth, _
                                          UserFont, userColour, bRightAlign, bDrawBox, bCentreAlign)

        Exit Function
        '=================


    End Function  '- mIntPrintTextInRectangle-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Print text string with User font style...--
    '--    Text will be truncated by the system if it goes out of bounds..-
    '--  Returns YPOS after print..-
    '--   3.1.3103.0318 - (lngStartXpos=-1) Means CENTRE string on Page.-

    Private Function mlPrintTextString(ByVal ev As PrintPageEventArgs, _
                                        ByVal sText As String, _
                                         ByVal lngStartXpos As Integer, _
                                          ByVal lngStartYpos As Integer, _
                                            ByRef UserFont As userFontDef, _
                                            Optional ByVal userColour As textColour = textColour.black) As Integer
        '- Now exported to printSubs.
        mlPrintTextString = gIntPrintTextString(ev, sText, lngStartXpos, lngStartYpos, UserFont, userColour)
        Exit Function
        '= = = =  = = = = = = === 

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

    Private Function mlPrintTextInBox(ByVal ev As PrintPageEventArgs, _
                                     ByVal sInputText As String, _
                                      ByVal lngUL_X As Integer, _
                                      ByVal lngUL_Y As Integer, _
                                      ByVal lngMargin As Integer, _
                                      ByVal lngBoxWidth As Integer, _
                                      ByVal lngBoxDepth As Integer, _
                                          ByVal bDrawBox As Boolean, _
                                   Optional ByVal lngFillColour As Integer = -1) As Integer

        '-- now exported to printSubs..-
        '-- add size parameter to pass default font size-.
        mlPrintTextInBox = gIntPrintTextInBox(ev, sInputText, lngUL_X, lngUL_Y, lngMargin, _
                                               lngBoxWidth, lngBoxDepth, bDrawBox, lngFillColour, mDefaultUserFont.lngSize)
        Exit Function
        '= = = = = = = = = = = = =
    End Function  '--print text..-
    '= = = = = = = = = = = = = =

    '== Page FOOTER for Invoice etc --
    '--     (ALL pages..) ==

    Private Function mbPageFooter(ByVal ev As PrintPageEventArgs, _
                                   Optional ByVal strPageNo As String = "") As Boolean
        '- exported-
        mbPageFooter = gbPageFooter(ev, msVersionPOS, strPageNo)

        Exit Function
        '= = = = =  = =

    End Function '--Footer..-
    '= = = = = = = = = = = = =


    '-- END OF  Re-Written  PRINT SERVICE function..
    '-- END OF  Re-Written  PRINT SERVICE function..

    '= = = = = = = = = = = = = = = = =
    '-===FF->



    '-- Print the DebtorsReport -
    '-- Print the DebtorsReport -
    '==
    '==   Updated.- 3519.0224  Started 22-Feb-2019= 
    '==     -- Update to Debtors Report- Add option for Summary only.... 
    '==  based on mbDebtorsReportSummaryOnly..

    '==
    '==  NEW VERSION 4.--
    '==  NEW VERSION 4.--
    '==
    '==    -- 4201.0618.  11/18-June-2019-   
    '==         --  Debtors Report and Statements. 
    '==               Show Credit-Note Available balance and Cust. Phone No....

    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==        colThisCustomerReversedInvoices = colThisCustomer.Item("reversedInvoices")

    '==
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)  26-Aug-2020..
    '==      Debtors Report-  Allow space for Reversed Invoices so as to stop overprinting the next customer.. . 
    '==         (Martin email 26-Aug-2020)..  Also, show Reversed Invoices only on detailed report..
    '==
    '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
    '==     Show Reversed Invoices only on detailed report, and only for current period..  
    '==     --  On Summary, DO NOT show customers with zero balance, even if thaey have reversed invoices in current period.


    Public Function mbPrintDebtorsReport_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
        Const k_HDR_HEIGHT = 26
        '= mlPrtWidth = 760
        Const k_WIDTH_CUST = 120      '--width of customer column.-
        Const k_WIDTH_DATE = 100      '--width of date column.-
        Const k_WIDTH_REFNO = 80  '--width of InvoiceNo column.-
        Const k_WIDTH_DESCR = 120  '--width of Description column.-
        Const k_WIDTH_DEBIT = 80     '--width of Invoice Amt column.-
        Const k_WIDTH_CREDITS = 80      '--width of CREDITS column.-
        Const k_WIDTH_BALANCE = 80   '--width of balance column.-
        Const k_WIDTH_DUE = 100      '--width of DUE date column.-
        '==Const k_WIDTH_TOTAL = 100  '--width of Ext.Total column.-

        '--  SUMMARY has a different Layout..
        '--  One line per Customer with ageing.
        Const k_WIDTH_SUMMARY_CUST = 340      '--width of SUMMARY customer column.-
        Const k_WIDTH_SUMMARY_CURRENT = 84      '--width of SUMMARY  column.-
        Const k_WIDTH_SUMMARY_30_DAYS = 84      '--width of SUMMARY  column.-
        Const k_WIDTH_SUMMARY_60_DAYS = 84      '--width of SUMMARY  column.-
        Const k_WIDTH_SUMMARY_90_DAYS = 84      '--width of SUMMARY  column.-
        Const k_WIDTH_SUMMARY_TOTAL = 84      '--width of SUMMARY  column.-

        Const k_REPORT_DETAIL_LINES As Integer = 56  '=44 '-TEMP 7 for test.== 62  '--Detail section.  220mm/3.5mm per line..-
        Const k_REPORT_SUMMARY_LINES As Integer = 40  '=30 '-TEMP 7 for test.== 62  '--Detail section.  220mm/3.5mm per line..-

        Dim intGreyBGColour As Integer = &HE0E0E0&
        Dim fillColor As Color
        Dim intXpos, intXpos2, intYpos, intYpos2 As Integer
        Dim intYposSummaryLine, intYposSummaryExtras As Integer
        Dim intHdrX As Integer = 150
        Dim intHdrY As Integer = 24
        Dim intLeftMarg, x1 As Integer
        Dim intXposDescr, intXposDebit, intXposCredits, intXposBal As Integer
        Dim L1, ix, intError, intCount1 As Integer
        Dim intDetailLinesNeeded, intDetailLinesAvailable As Integer
        '== Dim rowInvoice, rowDetail, row1 As DataRow
        Dim s1, sInvoiceNo, sDocType As String
        Dim sJobNo As String = "--"
        Dim dateInvoice As DateTime
        Dim sCustomerBarcode, sCustomerInfo As String
        Dim sABN, sPageNo As String
        Dim sChangeGiven, sNettRecvd As String
        Dim bPageFull As Boolean = False
        Dim decExtension As Decimal
        Dim decTotalTax As Decimal = 0
        '= Dim decDiscount, decRounding As Decimal
        '== Dim decSubTotalTax, decDiscTax As Decimal

        '= Dim decPayAmount, decTotalPayments, decAmountDebitedToAccount As Decimal
        Dim font1 As userFontDef
        Dim intInfoBoxWidth As Integer = 88  '= 120
        Dim intInfoBoxDepth As Integer = 27
        Dim intAddressBoxWidth As Integer = 380
        Dim intAddressBoxDepth As Integer = 120
        Dim intTermsBoxDepth As Integer = 100
        '-grid-
        Dim intGridYpos, intGridYdepth As Integer
        Dim penGrid As Pen = Pens.LightGray
        '-current customer-
        Dim colCurrentReportCustomer As Collection
        Dim colCurrentCustomerInfo As Collection
        Dim colCurrentInvoices As Collection
        '==  Target is new Build 4251..
        Dim colThisCustomerReversedInvoices As Collection

        Dim colInvoice, colRefunds As Collection
        Dim sCustName, sBarcode, sCustNameSummary, sCustNameLine2 As String

        '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
        Dim sCustNameSummaryOneLine As String
        '==  END Target-New-Build-4267 -- (Started 18-Sep-2020)

        Dim intCreditDays As Integer
        Dim colPayments As Collection
        Dim decInvoiceAmt, decTotalCredits As Decimal
        Dim decOutstanding As Decimal = 0  '==, decTotalRefundsAvail As Decimal = 0
        Dim sInvoiceId, sInvoiceDate, sTranType As String
        Dim sInvoiceTotal, sCreditTotal, sList As String
        Dim intDaysAged As Integer
        Dim intSummaryLineCount As Integer
        '=4201.0618=
        Dim sCustPhone, sCustMobile As String
        Dim decCustCreditNoteBalance As Decimal

        Call mbInitialiseBusiness()
        '-- Format ABN for printing..-
        sABN = VB.Left(msBusinessABN, 2) & " " & Mid(msBusinessABN, 3, 3) & _
                  " " & Mid(msBusinessABN, 6, 3) & " " & Mid(msBusinessABN, 9, 3)

        fillColor = ColorTranslator.FromOle(intGreyBGColour)

        '= MsgBox("Ready to print Invoice to " & msSelectedPrinterName & "..", MsgBoxStyle.Information)

        On Error GoTo PrintDebtorsReport_error
        font1.sName = "Lucida Sans Unicode"     '== "Tahoma" '== Printer.FontName = "Tahoma"
        font1.lngSize = 8 '==Printer.FontSize = 18
        font1.bBold = False '== Printer.Font.Bold = False '--reset to normal IN CASE LEFT OVER..-
        font1.bUnderline = False '= Printer.Font.Underline = False
        font1.bItalic = False

        intLeftMarg = k_LEFTMARGIN '== pixels-  (16 * PRT_UNIT) '- 240 twips..-
        intXpos = intLeftMarg
        intHdrX = intLeftMarg
        intYpos = 24  '--top pos.--
        intYpos2 = intYpos

        '-- Main Hdr stuff is on First page only..
        If (miPageNo <= 0) Then
            '--  paint BIZ logo  top left.--
            If Not (mImageUserLogo Is Nothing) Then
                x1 = mImageUserLogo.Width
                '= ev.Graphics.DrawImage(mImageUserLogo, intLeftMarg + mlPrtWidth - x1, 0)
                ev.Graphics.DrawImage(mImageUserLogo, intLeftMarg, 0)
            End If
            '-- initialise totals stuff..
            mIntCustomerCount = 1  '--first cust..
            mIntInvoiceCount = 0   '--but nothing done yet..

            '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
            mIntCustomerSequence = 1  '--track customers printing-
            '== END  Target-New-Build-4267 -- (Started 18-Sep-2020)


            mDecReportTotalTax = 0
            mDecReportTotalDebits = 0
            mDecReportTotalCredits = 0
            mDecReportTotalOutstanding = 0

            mDecReportAgedTotalCurrent = 0
            mDecReportAgedTotal30 = 0
            mDecReportAgedTotal60 = 0
            mDecReportAgedTotal90 = 0

        End If  '-first page-
        '-- report headr all pages..
        '-- print biz name (CENTERED)-
        font1.lngSize = 12
        font1.bBold = True
        intYpos = mlPrintTextString(ev, msBusinessName, -1, intYpos, font1)
        L1 = mlPrintTextString(ev, _
                              "Debtors Report" & IIf(mbDebtorsReportSummaryOnly, " (Summary)", ""), -1, intYpos, font1)
        '- As At DATE should be INPUT parameter..
        x1 = intLeftMarg + (mlPrtWidth \ 2) + 60  '= 120   '-- Date to right of "statement"..
        font1.lngSize = 9
        '-4201.1027--- Debtors Report--
        If (msDebtorsReportSubHeading <> "") Then  'was supplied-
            intYpos = mlPrintTextString(ev, msDebtorsReportSubHeading, x1, intYpos + 18, font1, textColour.black)
        Else  '-default.
            intYpos = mlPrintTextString(ev, "Outstanding as at: " & _
                                           Format(mDateCutOff, "dd-MMM-yyyy"), x1, intYpos + 18, font1, textColour.black)
        End If  '-sub heading.
        '-- Blank line..
        intYpos = mlPrintTextString(ev, "", intHdrX, intYpos, font1)
        intGridYpos = intYpos  '= + 16

        '--Page-1 Hdr done.--
        miPageNo += 1

        '-- All pages-
        '-- All pages-
        '-- GRID for Detail Lines..--
        '== intGridYpos = intYpos2 + 44 + intAddressBoxDepth + 12
        intGridYdepth = k_HDR_HEIGHT   '= 400
        intXpos = intLeftMarg

        '-print Page no..-
        sPageNo = "Page: " & CStr(miPageNo) & "."
        L1 = mIntPrintTextInRectangle(ev, sPageNo, _
                                      intLeftMarg + mlPrtWidth - k_WIDTH_DUE, intGridYpos, _
                                         k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, True, False, False) '-right Align.-
        '--draw the "grid"..
        intGridYpos += k_HDR_HEIGHT
        Call mlDrawLine(ev, intXpos, intGridYpos, mlPrtWidth)  '--top bar-
        '--column lines- 8 spaces, nine lines-
        '-here-

        '--Choose which Headings..
        If (Not mbDebtorsReportSummaryOnly) Then
            '--DETAIL REPORT- 
            '--   column lines- 8 spaces, nine lines-
            Dim arrayIntWidths() As Integer = _
                   {k_WIDTH_DATE, k_WIDTH_REFNO, k_WIDTH_DESCR, k_WIDTH_DEBIT, k_WIDTH_CREDITS, k_WIDTH_BALANCE, k_WIDTH_DUE}
            For ix = 0 To UBound(arrayIntWidths)
                ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)
                intXpos += arrayIntWidths(ix)
            Next ix
        Else '--summary-
            '--   column lines- 6 spaces,seven lines-
            Dim arrayIntWidths() As Integer = _
                   {k_WIDTH_SUMMARY_CUST, k_WIDTH_SUMMARY_CURRENT, _
                                  k_WIDTH_SUMMARY_30_DAYS, k_WIDTH_SUMMARY_60_DAYS, k_WIDTH_SUMMARY_90_DAYS, k_WIDTH_SUMMARY_TOTAL}
            For ix = 0 To UBound(arrayIntWidths)
                ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)
                intXpos += arrayIntWidths(ix)
            Next ix
        End If

        '--last vert line..
        ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)

        intXpos = intLeftMarg
        Call mlDrawLine(ev, intXpos, intGridYpos + intGridYdepth, mlPrtWidth)  '--bottom bar-

        '-- column header text line..
        '-- Can't use TAB stops because some items are Left-just and some are right..-
        '-- fill column headers BG..
        intXpos = intLeftMarg
        ev.Graphics.FillRectangle(New SolidBrush(fillColor), intXpos, intGridYpos, mlPrtWidth, k_HDR_HEIGHT)
        '-- box for col. hdrs. text-
        '=Dim rectHdr As New RectangleF(intXpos, intGridYpos, k_WIDTH_BC, k_HDR_HEIGHT)

        '-- PRINT column header TEXTS..
        '-- PRINT column header TEXTS..
        font1.lngSize = 8
        font1.bBold = True
        '--Choose which Headings..
        If (Not mbDebtorsReportSummaryOnly) Then
            '-detail report-
            Call mIntPrintTextInRectangle(ev, "Customer", intXpos, intGridYpos, _
                                                    k_WIDTH_CUST, k_HDR_HEIGHT, font1, textColour.black, False, True)
            intXpos += k_WIDTH_CUST
            Call mIntPrintTextInRectangle(ev, " Date", intXpos, intGridYpos, _
                                                     k_WIDTH_DATE, k_HDR_HEIGHT, font1, textColour.black, False, True)
            intXpos += k_WIDTH_DATE
            Call mIntPrintTextInRectangle(ev, " Invoice No.", intXpos, intGridYpos, _
                                                     k_WIDTH_REFNO, k_HDR_HEIGHT, font1, textColour.black, False, True)
            intXpos += k_WIDTH_REFNO
            Call mIntPrintTextInRectangle(ev, " Description", intXpos, intGridYpos, _
                                                      k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False, True)
            intXposDescr = intXpos  '--save position-
            intXpos += k_WIDTH_DESCR
            intXposDebit = intXpos  '-save-
            Call mIntPrintTextInRectangle(ev, "Invoice Amt", intXpos, intGridYpos, _
                                            k_WIDTH_DEBIT, k_HDR_HEIGHT, font1, textColour.black, False, True) '-NO Right Align.-
            intXpos += k_WIDTH_DEBIT
            intXposCredits = intXpos  '-save-
            Call mIntPrintTextInRectangle(ev, "Credits", intXpos, intGridYpos, _
                                            k_WIDTH_CREDITS, k_HDR_HEIGHT, font1, textColour.black, False, True, True) '-centre Align.-
            intXpos += k_WIDTH_CREDITS
            intXposBal = intXpos  '-save-
            Call mIntPrintTextInRectangle(ev, "Outstanding", intXpos, intGridYpos, _
                                           k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, False, True, True) '-CENTRE Align.-
            intXpos += k_WIDTH_BALANCE
            L1 = mIntPrintTextInRectangle(ev, "Due", intXpos, intGridYpos, _
                                             k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, False, True, True) '-CENTRE Align.-
        Else  '--Summary-
            Call mIntPrintTextInRectangle(ev, "Customer", intXpos, intGridYpos, _
                                                    k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, True)
            intXpos += k_WIDTH_SUMMARY_CUST
            Call mIntPrintTextInRectangle(ev, " Current", intXpos, intGridYpos, _
                                                     k_WIDTH_SUMMARY_CURRENT, k_HDR_HEIGHT, font1, textColour.black, True) '-R-align-
            intXpos += k_WIDTH_SUMMARY_CURRENT
            Call mIntPrintTextInRectangle(ev, " 30 Days.", intXpos, intGridYpos, _
                                                     k_WIDTH_SUMMARY_30_DAYS, k_HDR_HEIGHT, font1, textColour.black, True)
            intXpos += k_WIDTH_SUMMARY_30_DAYS
            Call mIntPrintTextInRectangle(ev, " 60 Days.", intXpos, intGridYpos, _
                                                      k_WIDTH_SUMMARY_60_DAYS, k_HDR_HEIGHT, font1, textColour.black, True)
            intXposDescr = intXpos  '--save position-
            intXpos += k_WIDTH_SUMMARY_60_DAYS
            '= intXposDebit = intXpos  '-save-
            Call mIntPrintTextInRectangle(ev, "90+ days", intXpos, intGridYpos, _
                                            k_WIDTH_SUMMARY_90_DAYS, k_HDR_HEIGHT, font1, textColour.black, True) '- Right Align.-
            intXpos += k_WIDTH_SUMMARY_90_DAYS
            '= intXposCredits = intXpos  '-save-
            Call mIntPrintTextInRectangle(ev, "Total Outst.", intXpos, intGridYpos, _
                                            k_WIDTH_SUMMARY_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-R. Align.-
            intXpos += k_WIDTH_SUMMARY_TOTAL
            '= intXposBal = intXpos  '-save-
        End If  '-summary-

        '= intXpos += k_WIDTH_DUE
        '-- L1 has line height.
        intXpos = intLeftMarg
        intYpos = intGridYpos + k_HDR_HEIGHT + 22  '= 12 '--jump col hdr row.
        font1.bBold = False

        '--  Print first/next page of Cust/Invoice Data..
        bPageFull = False
        '==If (mIntCustomerCount <= 0) Then  '--first page..  just starting-
        '== mIntCustomerCount = 1
        '== mIntInvoiceCount = 0
        '== colCurrentReportCustomer = mColReportCustomers.Item(1)
        '== Else '-pick up from prev. page.-
        '== colCurrentReportCustomer = mColReportCustomers.Item(mIntCustomerCount)
        '== End If
        intDetailLinesAvailable = k_REPORT_DETAIL_LINES

        mIntReportLineCount = 0  '-starting a page.-
        intSummaryLineCount = 0
        If (mIntCustomerCount <= mColReportCustomers.Count) Then '--still some customers.-
            '-- main loop for current/next customer..-
            Do  '--current/next customer-
                colCurrentReportCustomer = mColReportCustomers.Item(mIntCustomerCount)
                colCurrentCustomerInfo = colCurrentReportCustomer.Item("CustInfo")  '--from caller..-
                colCurrentInvoices = colCurrentReportCustomer.Item("Invoices")  '--from caller..-
                '-- get cust details..-
                sBarcode = colCurrentCustomerInfo.Item("barcode")
                '== sCustName = Trim(colCurrentCustomerInfo.Item("companyName"))

                '==  Target is new Build 4251..
                '==        colThisCustomerReversedInvoices = colThisCustomer.Item("reversedInvoices")
                colThisCustomerReversedInvoices = colCurrentReportCustomer.Item("reversedInvoices")  '--from caller..-
                '= END =


                '== Target-New-Build-4267 -- (Started 18-Sep-2020)
                '== Target-New-Build-4267 -- (Started 18-Sep-2020)

                '-- Customer may be included because of historical REVERSED invoices.
                '--  If No actual invoices, and no REVERSAL refunds in current period,
                '--    tnen BYPASS this customer.
                If (Not mbDebtorsReportSummaryOnly) And (colCurrentInvoices.Count <= 0) Then
                    '- Detail report- no invoices..
                    Dim bCanBypassCustomer As Boolean = True
                    Dim colRefund As Collection
                    Dim dateRefund As Date
                    If (colThisCustomerReversedInvoices.Count > 0) Then
                        '-- check if any invoices reversed in the last 30 days..
                        For Each colInv1 As Collection In colThisCustomerReversedInvoices
                            colRefund = colInv1.Item("refundInvoice")
                            dateRefund = CDate(colRefund.Item("invoice_date"))
                            If DateDiff(DateInterval.Day, dateRefund, Today) <= 30 Then
                                '-- current reversal-
                                bCanBypassCustomer = False
                                Exit For
                            End If  '-diff-
                        Next colInv1
                    End If '-reversals-
                    If bCanBypassCustomer Then
                        mIntCustomerCount += 1  '--started at 1..  Seen this customer.
                        Continue Do   '-next customer.
                    End If
                End If  '-bypass-
                '== END Target-New-Build-4267 -- (Started 18-Sep-2020)
                '== END Target-New-Build-4267 -- (Started 18-Sep-2020)




                '=3510.0224-
                intCount1 = colCurrentInvoices.Count
                If (intCount1 > 44) Then
                    intCount1 = 44    '== so we dont get to infinit page loop.
                End If
                '== Check if enough room for this customer's invoices.
                '=4201.0618=  more CHECK min room for a customer..
                intDetailLinesNeeded = intCount1 + 8  '= 7
                If (Not mbDebtorsReportSummaryOnly) AndAlso (intDetailLinesNeeded > intDetailLinesAvailable) Then
                    '--  exit and come back on new page for this customer..
                    bPageFull = True
                    Exit Do
                End If

                '== If (sCustName = "") Then  '-use person..-
                '==   sCustName = colCurrentCustomerInfo.Item("firstName") & " " & colCurrentCustomerInfo.Item("lastName")
                '=  End If

                '=3303.0116= 16Jan2017=
                colRefunds = colCurrentReportCustomer.Item("Refunds")

                sCustName = VB.Left(Trim(colCurrentCustomerInfo.Item("custShortName")), 36)
                sCustName &= " (" & sBarcode & ")"
                sCustNameSummary = sCustName   '-for summary..
                sCustNameLine2 = ""  '= for summary if name +Phone is too long.

                intCreditDays = CInt(colCurrentCustomerInfo.Item("creditDays"))
                '=4201.0618=
                sCustPhone = Trim(colCurrentCustomerInfo.Item("phone"))
                sCustMobile = Trim(colCurrentCustomerInfo.Item("mobile"))
                decCustCreditNoteBalance = colCurrentCustomerInfo.Item("CreditNoteBalance")

                '- print cust.name-  (if started, show "Continued"..)
                If (mIntReportLineCount = 0) And (miPageNo > 1) And (mIntInvoiceCount > 0) And _
                                        (mIntInvoiceCount <= colCurrentInvoices.Count) Then '--start of page 2 etc.-
                    sCustName &= " (continued)."
                End If
                font1.lngSize = 9
                font1.bBold = True
                '-save this vert pos for SUMMARY aged summary.
                intYposSummaryLine = intYpos
                intYposSummaryExtras = 0  '-in case of extra line.
                '- Print cust.name- (=4201.0618-  Add Phone No..)
                '--  (=4201.0618-  Add Phone No..)


                '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
                '= s1 = CStr(mIntCustomerCount) & ". " & sCustName & "; (Tel: " & sCustPhone & "; " & sCustMobile & ")"
                s1 = CStr(mIntCustomerSequence) & ". " & sCustName & "; (Tel: " & sCustPhone & "; " & sCustMobile & ")"
                '== END  Target-New-Build-4267 -- (Started 18-Sep-2020)


                '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
                sCustNameSummaryOneLine = s1  '-save to print with SUUMARY report cust line below.
                '==  END Target-New-Build-4267 -- (Started 18-Sep-2020)

                sCustNameLine2 = " (Tel: " & sCustPhone & "; " & sCustMobile & ")"
                '- print name-
                If (Not mbDebtorsReportSummaryOnly) Then
                    '-detail-
                    intYpos = mlPrintTextString(ev, s1, intLeftMarg, intYpos, font1)
                Else  '--summary-


                    '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
                    '==   Target-New-Build-4267 -- (Started 18-Sep-2020)

                    '-- For SUMMARY REPORT-  Print Cust name below when printing data line..
                    '-- For SUMMARY REPORT-  Print Cust name below when printing data line..

                    'font1.bBold = False
                    ''-- print TelNo on second line if too long.
                    ''--  VERTICAL pos doesn't move after this, as aged balance goes on same line.,
                    'If (Len(s1) < 48) Then  '= ie < 340 px..
                    '    Call mIntPrintTextInRectangle(ev, s1, intLeftMarg, intYpos, _
                    '                            k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, False)
                    'Else '--too long..
                    '    '-two lines..
                    '    sCustNameSummary = CStr(mIntCustomerCount) & ". " & sCustNameSummary
                    '    Call mIntPrintTextInRectangle(ev, sCustNameSummary, intLeftMarg, intYpos, _
                    '                   k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, False)
                    '    '- Tel. no on second line. (Just below name.)
                    '    font1.lngSize = 8
                    '    intYposSummaryExtras = 13  '--pixels.
                    '    Call mIntPrintTextInRectangle(ev, sCustNameLine2, intLeftMarg + 80, intYpos + 13, _
                    '                   k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, False)
                    'End If  '-too long-

                    '== END  Target-New-Build-4267 -- (Started 18-Sep-2020)


                End If
                mIntReportLineCount += 2

                '-- loop through Invoices..-
                font1.lngSize = 8
                font1.bBold = False
                If (colCurrentInvoices.Count > 0) Then
                    Do
                        mIntInvoiceCount += 1
                        colInvoice = colCurrentInvoices.Item(mIntInvoiceCount)
                        '= Dim decTotalCredits As Decimal = 0

                        colPayments = colInvoice.Item("invoicePayments")
                        sInvoiceDate = Format(colInvoice.Item("invoice_date"), "dd-MMM-yyyy")
                        sInvoiceId = CStr(colInvoice.Item("invoice_id"))
                        sTranType = colInvoice.Item("transactionType")
                        If (LCase(sTranType) = "sale") Then
                            sTranType = "Invoice (Sale)"
                        End If
                        decInvoiceAmt = CDec(colInvoice.Item("invoiceTotal"))
                        sInvoiceTotal = FormatCurrency(decInvoiceAmt, 2)
                        sInvoiceTotal = Replace(sInvoiceTotal, "$", "")
                        '-paymentTotalThisInvoice-
                        decTotalCredits = CDec(colInvoice.Item("paymentTotalThisInvoice"))
                        sCreditTotal = FormatCurrency(decTotalCredits, 2)
                        sCreditTotal = Replace(sCreditTotal, "$", "")
                        decOutstanding = decInvoiceAmt - decTotalCredits
                        '= decTaxAmt = CDec(colInvoice.Item("total_tax"))
                        '= sTax = FormatCurrency(decTaxAmt, 2)

                        If (LCase(sTranType) = "refund") Then
                            '= decTaxAmt = -decTaxAmt
                            decInvoiceAmt = -decInvoiceAmt
                        End If
                        '-- add to summary totals..-
                        mDecStatementTotalDebits += decInvoiceAmt
                        mDecReportTotalDebits += decInvoiceAmt

                        mDecStatementTotalCredits += decTotalCredits
                        mDecReportTotalCredits += decTotalCredits

                        mDecStatementTotalOutstanding += decOutstanding
                        mDecReportTotalOutstanding += decOutstanding
                        '-- Age the outstanding..
                        intDaysAged = DateDiff(DateInterval.Day, colInvoice.Item("invoice_date"), Today)
                        If (intDaysAged <= 30) Then
                            mDecStatementAgedTotalCurrent += decOutstanding
                            mDecReportAgedTotalCurrent += decOutstanding
                        ElseIf (intDaysAged <= 60) Then
                            mDecStatementAgedTotal30 += decOutstanding
                            mDecReportAgedTotal30 += decOutstanding
                        ElseIf (intDaysAged <= 90) Then
                            mDecStatementAgedTotal60 += decOutstanding
                            mDecReportAgedTotal60 += decOutstanding
                        Else  '--90 and over-
                            mDecStatementAgedTotal90 += decOutstanding
                            mDecReportAgedTotal90 += decOutstanding
                        End If

                        '- Show invoice line if Detail REport-
                        If (Not mbDebtorsReportSummaryOnly) Then
                            intXpos = intLeftMarg + k_WIDTH_CUST
                            '-- print invoice line -
                            '= intYpos += 3   '-- down a bit from prev. line..
                            Call mIntPrintTextInRectangle(ev, sInvoiceDate, intXpos, intYpos, _
                                                                       k_WIDTH_DATE, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                            intXpos += k_WIDTH_DATE
                            Call mIntPrintTextInRectangle(ev, sInvoiceId & "  ", intXpos, intYpos, _
                                                        k_WIDTH_REFNO, k_HDR_HEIGHT, font1, textColour.black, True) '-no box-
                            intXpos += k_WIDTH_REFNO
                            Call mIntPrintTextInRectangle(ev, "  " & sTranType, intXpos, intYpos, _
                                                      k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                            intXpos += k_WIDTH_DESCR
                            '-invoice amount-
                            L1 = mIntPrintTextInRectangle(ev, sInvoiceTotal, intXpos, intYpos, _
                                                     k_WIDTH_DEBIT, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                            intXpos += k_WIDTH_DEBIT
                            L1 = mIntPrintTextInRectangle(ev, sCreditTotal, intXpos, intYpos, _
                                                         k_WIDTH_CREDITS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                            intXpos += k_WIDTH_CREDITS
                            s1 = FormatCurrency(decOutstanding, 2)
                            L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                                         k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                            intXpos += k_WIDTH_BALANCE
                            '-- Calculate and print DUE date..
                            s1 = Format(DateAdd(DateInterval.Day, intCreditDays, CDate(colInvoice.Item("invoice_date"))), "dd-MMM-yyyy")
                            If (decOutstanding > 0) Then
                                Call mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                                            k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, False, False, True) '-no box-
                            End If
                            intYpos += L1
                            mIntReportLineCount += 1
                            intDetailLinesAvailable -= 1  '--this will m ake sure the next customer fits.
                            bPageFull = (mIntReportLineCount > k_REPORT_DETAIL_LINES)
                        End If  '-detail-
                    Loop Until (mIntInvoiceCount >= colCurrentInvoices.Count) Or bPageFull
                    '===If bPageFull Then Exit Do '--bypass 
                End If  '-invoice count-



                '==  Target is new Build 4251..
                '==  Target is new Build 4251..
                '-First, show any reversed invoices..

                '==   Target-New-Build-4262 -- (Started 14-Aug-2020)  26-Aug-2020..
                '==      Debtors Report-  Allow space for Reversed Invoices so as to stop overprinting the next customer.. . 
                '==      ALSO.. show REVERSALS only for Detailed Report..
                '==colThisCustomerReversedInvoices = colCurrentReportCustomer.Item("reversedInvoices")  '--from caller..-
                If (Not bPageFull) AndAlso (Not mbDebtorsReportSummaryOnly) Then
                    If (colThisCustomerReversedInvoices IsNot Nothing) AndAlso _
                       (colThisCustomerReversedInvoices.Count > 0) Then
                        Dim colRefund As Collection
                        font1.lngSize = 8
                        intYpos = mlPrintTextString(ev, "", intLeftMarg, intYpos, font1)  '--gap-
                        If (mbDebtorsReportSummaryOnly) Then
                            intYpos += (k_HDR_HEIGHT \ 7)  '-- a bit more gap.
                            intYpos = mlPrintTextString(ev, "", intLeftMarg, intYpos, font1)  '--more gap-
                        End If
                        For Each colInv1 As Collection In colThisCustomerReversedInvoices
                            colRefund = colInv1.Item("refundInvoice")
                            s1 = "** Invoice #" & colInv1.Item("invoice_id") & _
                                           " was reversed by Refund #" & colRefund.Item("invoice_id") & _
                                              " on " & Format(colRefund.Item("invoice_date"), "dd-MMM-yyyy") & vbCrLf
                            '== Target-New-Build-4267 -- (Started 18-Sep-2020)
                            '--  Don't show old ones..
                            If (DateDiff(DateInterval.Day, CDate(colRefund.Item("invoice_date")), Today) <= 28) Then
                                intYpos = mlPrintTextString(ev, s1, intLeftMarg, intYpos, font1)
                                mIntReportLineCount += 1
                                '==   Target-New-Build-4262 -- (Started 14-Aug-2020)  26-Aug-2020..
                                '==      Debtors Report-  Allow space for Reversed Invoices so as to stop overprinting the next customer.. . 
                                intDetailLinesAvailable -= 1  '--this will m ake sure the next customer fits.
                            End If  '-datediff.
                        Next colInv1
                        If (mbDebtorsReportSummaryOnly) Then
                            intYpos += (k_HDR_HEIGHT \ 3)  '-- a bit more gap after.
                        End If
                    End If  '-reversed..
                    '== END OF Target is new Build 4251..

                End If  '-page full etc.
                '== END  Target-New-Build-4262 -- (Started 14-Aug-2020)  26-Aug-2020..



                If (mIntInvoiceCount < colCurrentInvoices.Count) Then  '-cust not finished-
                    '-- continued next page-
                    intYpos = mlPrintTextString(ev, "This Customer continued on next page..", intLeftMarg, intYpos, font1)
                ElseIf bPageFull And (mIntInvoiceCount <= 0) Then
                    '--new customer on next page.
                Else '--cust done-
                    '- Show Cust totals line if Detail REport-
                    If (Not mbDebtorsReportSummaryOnly) Then
                        '= intYpos += 15  '--plus a line.-
                        '-- Totals for Customer !! -

                        '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
                        mIntReportLineCount += 10 '==Target-New-Build-4267  - was 3 ---
                        '== END  Target-New-Build-4267 -- (Started 18-Sep-2020)

                        font1.bBold = True
                        Call mlDrawLine(ev, intXposDebit, intYpos, k_WIDTH_DEBIT + k_WIDTH_CREDITS + k_WIDTH_BALANCE)
                        Call mIntPrintTextInRectangle(ev, "Cust. Totals:", intXposDescr, intYpos, _
                                                                       k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                        s1 = FormatCurrency(mDecStatementTotalDebits, 2)
                        L1 = mIntPrintTextInRectangle(ev, s1, intXposDebit, intYpos, _
                                                      k_WIDTH_DEBIT, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        s1 = FormatCurrency(mDecStatementTotalCredits, 2)
                        L1 = mIntPrintTextInRectangle(ev, s1, intXposCredits, intYpos, _
                                                      k_WIDTH_CREDITS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        s1 = FormatCurrency(mDecStatementTotalOutstanding, 2)
                        L1 = mIntPrintTextInRectangle(ev, s1, intXposBal, intYpos, _
                                                      k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        intYpos += k_HDR_HEIGHT + 5

                        '-- Aged totals for Customer !! -
                        font1.bBold = False
                        font1.lngSize = 8
                        '=Call mIntPrintTextInRectangle(ev, "Aged balance: ", intXposDescr, intYpos, _
                        '=                                                 k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                        s1 = "Current:" & FormatCurrency(mDecStatementAgedTotalCurrent, 2)
                        s1 &= ";  30 Days:" & FormatCurrency(mDecStatementAgedTotal30, 2)
                        s1 &= ";  60 Days:" & FormatCurrency(mDecStatementAgedTotal60, 2)
                        s1 &= ";  90+ Days:" & FormatCurrency(mDecStatementAgedTotal90, 2)
                        intYpos = mlPrintTextString(ev, "Aged balance: ", intXposDescr, intYpos, font1)
                        intYpos = mlPrintTextString(ev, s1, intXposDescr, intYpos, font1)
                        intYpos += 2  '=3  '--plus half a line.-
                        '==  3303.0114- 14Jan2017=
                        '-- FIRST print total Avail. refunds if any.-
                        '=4201.0618- NO more Refunds on Debtors..
                        '--  Now using CreditNotes..
                        '--    decTotalRefundsAvail = 0

                        'sList = " in Refunds not yet credited- (Refunds Nos: "
                        'If (colRefunds.Count > 0) Then
                        '    For Each colRefund As Collection In colRefunds
                        '        decTotalRefundsAvail += CDec(colRefund.Item("AmountAvailable"))
                        '        sList &= CStr(colRefund.Item("invoice_id")) & "; "
                        '    Next colRefund
                        If (decCustCreditNoteBalance > 0) Then '= (decTotalRefundsAvail > 0) Then
                            '-- reduce outstanding..
                            '- not now-mDecStatementTotalOutstanding -= decTotalRefundsAvail
                            '= adjust aged bals..
                            '= not now-
                            '-print credit refunds line-
                            '= intYpos += k_HDR_HEIGHT
                            '= s1 = "NB: " & FormatCurrency(decTotalRefundsAvail, 2) & " " & sList & " not applied).."
                            s1 = "(NB: " & FormatCurrency(decCustCreditNoteBalance, 2) & " Credit-Note balance not yet applied).."
                            font1.bBold = True
                            intYpos = mlPrintTextString(ev, s1, intXposDescr, intYpos, font1, textColour.magenta)
                            Call mlDrawLine(ev, k_LEFTMARGIN, intYpos + 3, mlPrtWidth - k_WIDTH_DUE)
                            font1.bBold = False
                        Else  '-no avail.-
                            '- Draw line under customer..
                            font1.bBold = False
                            font1.lngSize = 8
                            Call mlDrawLine(ev, k_LEFTMARGIN, intYpos, mlPrtWidth - k_WIDTH_DUE)
                        End If '-avail-
                        'End If  '- REFUND count-
                        intDetailLinesAvailable -= 7  '-- was space for cust totals..

                        '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
                        intYpos += k_HDR_HEIGHT  '-for next customer..
                        mIntCustomerSequence += 1 ' '-- sequence on list..
                        '== END  Target-New-Build-4267 -- (Started 18-Sep-2020)

                    ElseIf (mDecStatementTotalOutstanding > 0) Then  '- '==   Target-New-Build-4267 -- (Started 18-Sep-2020)

                        '--Aged SUMMARY report Line for cust.

                        '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
                        '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
                        '==     Show Reversed Invoices only on detailed report, and only for current period..  
                        '==     --  On Summary, DO NOT show customers with zero balance, even if thaey have reversed invoices in current period.


                        font1.bBold = False
                        '-- print TelNo on second line if too long.
                        '--  VERTICAL pos doesn't move after this, as aged balance goes on same line.,
                        If (Len(sCustNameSummaryOneLine) < 48) Then  '= ie < 340 px..
                            Call mIntPrintTextInRectangle(ev, sCustNameSummaryOneLine, intLeftMarg, intYpos, _
                                                    k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, False)
                        Else '--too long..
                            '-two lines..
                            sCustNameSummary = CStr(mIntCustomerSequence) & ". " & sCustNameSummary
                            Call mIntPrintTextInRectangle(ev, sCustNameSummary, intLeftMarg, intYpos, _
                                           k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, False)
                            '- Tel. no on second line. (Just below name.)
                            font1.lngSize = 8
                            intYposSummaryExtras = 13  '--pixels.
                            Call mIntPrintTextInRectangle(ev, sCustNameLine2, intLeftMarg + 80, intYpos + 13, _
                                           k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, False)
                        End If  '-too long-

                        '== END Target-New-Build-4267 -- (Started 07-Sep-2020)
                        '== END Target-New-Build-4267 -- (Started 07-Sep-2020)
                        '== END Target-New-Build-4267 -- (Started 07-Sep-2020)


                        intXpos = intLeftMarg + k_WIDTH_SUMMARY_CUST
                        intYpos = intYposSummaryLine
                        s1 = FormatCurrency(mDecStatementAgedTotalCurrent, 2)
                        s1 = Replace(s1, "$", "")  '--drop dollar-
                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                             k_WIDTH_SUMMARY_CURRENT, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        intXpos += k_WIDTH_SUMMARY_CURRENT

                        s1 = FormatCurrency(mDecStatementAgedTotal30, 2)
                        s1 = Replace(s1, "$", "")  '--drop dollar-
                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                             k_WIDTH_SUMMARY_30_DAYS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        intXpos += k_WIDTH_SUMMARY_30_DAYS

                        s1 = FormatCurrency(mDecStatementAgedTotal60, 2)
                        s1 = Replace(s1, "$", "")  '--drop dollar-
                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                             k_WIDTH_SUMMARY_60_DAYS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        intXpos += k_WIDTH_SUMMARY_60_DAYS

                        s1 = FormatCurrency(mDecStatementAgedTotal90, 2)
                        s1 = Replace(s1, "$", "")  '--drop dollar-
                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                             k_WIDTH_SUMMARY_90_DAYS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        intXpos += k_WIDTH_SUMMARY_90_DAYS

                        s1 = FormatCurrency(mDecStatementTotalOutstanding, 2)
                        s1 = Replace(s1, "$", "")  '--drop dollar-
                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                             k_WIDTH_SUMMARY_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        '= intXpos += k_WIDTH_SUMMARY_CURRENT
                        mIntReportLineCount += 1
                        '= bPageFull = (mIntReportLineCount > k_REPORT_SUMMARY_LINES)
                        intSummaryLineCount += 1
                        If intYposSummaryExtras > 0 Then
                            intYpos += intYposSummaryExtras  '--in case Tel.No on second line.
                            intSummaryLineCount += 1
                        End If
                        bPageFull = (intSummaryLineCount > k_REPORT_SUMMARY_LINES)


                        '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
                        intYpos += k_HDR_HEIGHT  '-for next customer..
                        mIntCustomerSequence += 1 ' '-- sequence on list..
                        '== END  Target-New-Build-4267 -- (Started 18-Sep-2020)
                    Else
                        '--ignore customer with zero outstanding.
                    End If 'detail/summary-

                    '-- All reports- clear statement totals.

                    '==   Target-New-Build-4267-   intYpos += k_HDR_HEIGHT  '-for next customer..

                    mIntInvoiceCount = 0
                    mDecStatementTotalDebits = 0
                    mDecStatementTotalCredits = 0
                    mDecStatementTotalOutstanding = 0
                    mDecStatementAgedTotalCurrent = 0
                    mDecStatementAgedTotal30 = 0
                    mDecStatementAgedTotal60 = 0
                    mDecStatementAgedTotal90 = 0

                    mIntCustomerCount += 1  '--started at 1..

                End If  '-cust done-
            Loop Until (mIntCustomerCount > mColReportCustomers.Count) Or bPageFull
        End If '--still some customers.-
        '-- print report totals..--
        If bPageFull Then
            '-- next page-
        Else  '-print-
            Dim intAgeWidth = 90
            If (mIntReportLineCount <= 44) Then '- = (k_REPORT_LINES - 14) == have room for totals..
                intYpos = k_STATEMENT_SUMMARY_TOP
                L1 = mlPrintTextInBox(ev, vbCrLf & "<b>Final Totals- Aged Balances:", _
                        k_LEFTMARGIN, intYpos, 16, intAddressBoxWidth, intAddressBoxDepth + 48, True)
                '-- SUMMARY of aged info--
                intXpos = k_LEFTMARGIN + 16
                intYpos += 36
                '-- Aged headings-
                L1 = mIntPrintTextInRectangle(ev, "Current", intXpos, intYpos, _
                                                    intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                L1 = mIntPrintTextInRectangle(ev, "30 Days", intXpos + intAgeWidth, intYpos, _
                                                     intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                L1 = mIntPrintTextInRectangle(ev, "60 Days", intXpos + (intAgeWidth * 2), intYpos, _
                                                     intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                L1 = mIntPrintTextInRectangle(ev, "90 Days", intXpos + (intAgeWidth * 3), intYpos, _
                                                     intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                '--Aged totals-
                intYpos += L1 + 4
                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecReportAgedTotalCurrent, 2), intXpos, intYpos, _
                                                intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecReportAgedTotal30, 2), intXpos + intAgeWidth, intYpos, _
                                                 intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecReportAgedTotal60, 2), intXpos + (intAgeWidth * 2), intYpos, _
                                                 intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecReportAgedTotal90, 2), intXpos + (intAgeWidth * 3), intYpos, _
                                                 intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                intYpos += k_HDR_HEIGHT + 8

                '- print total outstanding.-

                font1.bBold = True
                intYpos = mlPrintTextString(ev, "Final Total Outstanding: " & _
                                              FormatCurrency(mDecReportTotalOutstanding, 2), k_LEFTMARGIN + 16, intYpos, font1)
                intYpos = mlPrintTextString(ev, "For: " & _
                                          CStr(mColReportCustomers.Count) & " Account customers.", k_LEFTMARGIN + 16, intYpos, font1)
                intYpos += 6

                '-- Print Total Invoices and Credits..
                '--RHS invoice totals-
                '--RHS invoice totals-
                intYpos = k_STATEMENT_SUMMARY_TOP
                intXpos = k_LEFTMARGIN
                intXpos2 = intXpos + mlPrtWidth - intAddressBoxWidth
                '== intYpos = intGridYpos + intGridYdepth + 24
                L1 = mlPrintTextInBox(ev, vbCrLf & "<b>Total all Invoices:", _
                          intXpos2, intYpos, 16, intAddressBoxWidth, intAddressBoxDepth + 48, True)
                intYpos += 32
                font1.bBold = True
                font1.bItalic = False
                '- Private mDecStatementTotalTax As Decimal
                '- Private mDecStatementTotalDebits As Decimal
                '- Private mDecStatementTotalCredits As Decimal
                Dim intTotalsCaptionWidth As Integer = 140
                Dim intTotalsValueWidth As Integer = 170
                Dim intXposTotal = intXpos2 + 28
                '--Totals line..
                s1 = FormatCurrency(mDecReportTotalDebits, 2)
                L1 = mIntPrintTextInRectangle(ev, "Total Debits Inc Tax: ", intXposTotal, intYpos, _
                                     intTotalsCaptionWidth, k_HDR_HEIGHT, font1, textColour.black, False) '-L/Align, no box-
                font1.bBold = False
                L1 = mIntPrintTextInRectangle(ev, s1, intXposTotal + intTotalsCaptionWidth, intYpos, _
                                                   intTotalsValueWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                intYpos += L1
                '-- blank line.-
                intYpos = mlPrintTextString(ev, "", intXposTotal, intYpos, font1)
                '--Credits line..
                font1.bBold = True
                s1 = FormatCurrency(mDecReportTotalCredits, 2)
                L1 = mIntPrintTextInRectangle(ev, "Total Credits: ", intXposTotal, intYpos, _
                                     intTotalsCaptionWidth, k_HDR_HEIGHT, font1, textColour.black, False) '-L/Align, no box-
                font1.bBold = False
                L1 = mIntPrintTextInRectangle(ev, s1, intXposTotal + intTotalsCaptionWidth, intYpos, _
                                                   intTotalsValueWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                '-- add minus sign.-
                x1 = intXposTotal + intTotalsCaptionWidth + intTotalsValueWidth
                Call mlPrintTextString(ev, "-", x1, intYpos + (k_HDR_HEIGHT \ 2), font1)
                intYpos += (L1 + (L1 \ 2)) + 3
                font1.bBold = True
                '-- balance-
                Dim decOverallBalance As Decimal = mDecReportTotalDebits - mDecReportTotalCredits
                Call mlDrawLine(ev, intXposTotal, intYpos, intTotalsCaptionWidth + intTotalsValueWidth)
                Call mIntPrintTextInRectangle(ev, "Total Balance:", intXposTotal, intYpos, _
                                                              intTotalsCaptionWidth, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                s1 = FormatCurrency(decOverallBalance, 2)
                L1 = mIntPrintTextInRectangle(ev, s1, intXposTotal + intTotalsCaptionWidth, intYpos, _
                                                   intTotalsValueWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                intYpos += (L1 + (L1 \ 2))
                '-- end of this invoice-
                font1.bBold = False
                Call gIntDrawLine(ev, intXposTotal, intYpos + 3, intTotalsCaptionWidth + intTotalsValueWidth)
            Else
                bPageFull = True  '-totals on next page-
            End If  '--have room-
        End If  '--print totals.-


        '--TEMP--??-
        Call mbPageFooter(ev, sPageNo)
        ev.HasMorePages = bPageFull   '--all done-
        '-27-Oct-2019-
        If Not ev.HasMorePages Then
            '-  = RESET this when no more pages- (For printing from Dialog).
            miPageNo = 0
            '=mIntPagesPrintedCount = 0
        End If

        Exit Function  '--done--

PrintDebtorsReport_error:
        L1 = Err().Number
        MsgBox("Runtime Error in mbPrintDebtorsReport_PageEvent function.." & vbCrLf & _
                                       "Error=" & L1 & ": " & ErrorToString(L1), MsgBoxStyle.Critical)

    End Function  '-DebtorsReport-
    '= = = = = = = = = = = = = = = = =
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
            'Case printDocType.SalesInvoice
            '    Call mbPrintSalesInvoice_PageEvent(ev)
            'Case printDocType.Receipt
            '    Call mbPrintReceipt_PageEvent(ev)
            'Case printDocType.dgv
            '    Call mbPrintDataGridView_PageEvent(ev)
            'Case printDocType.StockLabels
            '    Call mbPrintStockLabels_PageEvent(ev)
            'Case printDocType.LaybyLabels
            '    Call mbPrintLaybyLabels_PageEvent(ev)
            'Case printDocType.Statement
            '    Call mbPrintStatement_PageEvent(ev)
            Case printDocType.DebtorsReport
                Call mbPrintDebtorsReport_PageEvent(ev)
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

    '-- Debtors Report --
    '== NEW- 11-Oct-2019-
    '==  ADD PREVIEW OPTION to Print Debtors Report..
    '== NEW- 11-Oct-2019-
    '==  ADD PREVIEW OPTION to Print Debtors Report..
    '== NEW- 27-Oct-2019-
    '==  ADD SUB HEADING to Print Debtors Report..

    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==
    '== == Debtors Report-   
    '==     Show Reversed Invoices only on detailed report, and only for current period..  
    '==     --  On Summary, DO NOT show customers with zero balance, even if thaey have reversed invoices in current period.
    '==
    '==      --  PLUS-  THIS IS the new class "clsDebtorsReport" to give report its own class.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    Public Function PrintDebtorsReport2(ByRef colReportCustomers As Collection, _
                                        ByVal dateCutOff As Date, _
                                       ByRef SystemInfo As clsSystemInfo, _
                                       ByRef ImageUserLogo As Image, _
                                       ByVal sVersionPOS As String, _
                                       ByVal sSelectedPrinterName As String, _
                                       Optional ByVal bDebtorsReportSummaryOnly As Boolean = True, _
                                       Optional ByVal bPreviewOnly As Boolean = False, _
                                       Optional ByVal strSubHeading As String = "") As Boolean

        PrintDebtorsReport2 = False

        mbPrintingCompleted = False  '=3107.820-
        mbPrintError = False
        msPrintErrorMsg = ""

        mColReportCustomers = colReportCustomers
        '= crap= mColRefunds = mColReportCustomers.Item("Refunds")

        mDateCutOff = dateCutOff
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.DebtorsReport
        miPageNo = 0
        If (colReportCustomers Is Nothing) OrElse (colReportCustomers.Count <= 0) Then
            MsgBox("No Debtors Invoices to print..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        mSysInfo1 = SystemInfo  '= mSdSystemInfo = sdSystemInfo
        mImageUserLogo = ImageUserLogo
        msVersionPOS = sVersionPOS
        msSelectedPrinterName = sSelectedPrinterName

        '=3519.0224-
        mbDebtorsReportSummaryOnly = bDebtorsReportSummaryOnly
        '-4201.1027--- Debtors Report--
        msDebtorsReportSubHeading = strSubHeading

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Report printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        PrintDebtorsReport2 = True
        '== NEW- 11-Oct-2019-
        '==  ADD PREVIEW OPTION to Print Debtors Report..
        If bPreviewOnly Then
            Try
                '--  set printer selected..--
                printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
                '--  start the printer..--
                '-  PREVIEW requested- 
                printPreviewDialog1.Document = printDocument1
                printPreviewDialog1.Height = 1200
                printPreviewDialog1.Width = 800
                printPreviewDialog1.PrintPreviewControl.UseAntiAlias = True
                '-4201.1027=
                printPreviewDialog1.PrintPreviewControl.Zoom = 0.95
                printPreviewDialog1.ShowDialog()
                PrintDebtorsReport2 = True
            Catch ex As Exception
                MsgBox("Error in Print Debtors preview.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                PrintDebtorsReport2 = False
            End Try  '-preview-
        Else '-normal- straight print.
            Try
                '--  set printer selected..--
                printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
                '--  start the printer..--
                printDocument1.Print()
            Catch ex As Exception
                '== MessageBox.Show(ex.Message)
                MsgBox("Error in printing DebtorsReport." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                PrintDebtorsReport2 = False
            End Try
        End If '-preview

    End Function  '-PrintDebtorsReport-
    '= = = = = = = = = = = = = = = = = == 


End Class  '--clsDebtorsReport2-
'= = = = = = = = = = = = = = =
