Option Strict Off
Option Explicit On

Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Drawing2D
Imports System.Math
Imports VB = Microsoft.VisualBasic

Public Class clsPrintSaleDocs

    '==grh - 05-Jun-2014==JobMatixPOS V3.0-Build-3012.605--
    '--     
    '==  INVOICE printing.. Scavanged from Jobmatix clsPrintDocs.
    '==
    '==  grh JobMatixPOS 3.1.3103.0109 -  09-Jan-2015
    '==       >> Stock Label printing....
    '==       >> 3103.117  Job No on invoice hdr.
    '==
    '==  grh JobMatixPOS 3.1.3103.0318 - 18-Mar-2015
    '==       >> Statement printing....
    '==
    '==  grh JobMatixPOS 3.1.3103.0412 - 12-Apr-2015
    '==       >> DebtorsReport printing....
    '==
    '==  grh JobMatixPOS 3.1.3107.0820 - 20-Aug-2015
    '==       >> Catch End print Event and set End property for user..
    '==       >>  Export text subs to "modPrintSubs" ==
    '==
    '==  grh JobMatixPOS 3.1.3107.0906 - 06-Sep-2015
    '==       >> Statements/Debtors- Add parm for Cutoff Date..
    '==
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==
    '==     v3.3.3301.505..  05-May-2016= ===
    '==       >>  Fixes to printDataGridView. (fonts, widths)...
    '==
    '==     v3.3.3301.602..  02-June-2016= ===
    '==       >>  Fixes to statements
    '==         for Payments now coming from Invoice table (accountPayments)...
    '==
    '==     v3.3.3301.627..  27-June-2016= ===
    '==       >>  Fixes to Sales Invoice for CreditNotes..
    '==
    '==     v3.3.3303.0111..  11-Jan-201= ===
    '==       >>  Fixes to Statements re payments...
    '==
    '==     v3.3.3303.0114..  14-Jan-2017= ===
    '==       >>  Refunds applied in Payments are shown as already disbursed credits..
    '==       >>  Outstanding Refunds are now sourced from colRefunds.
    '==             and listed sepatately.
    '= = = =  = = = = = = = = = = = = = = == = =  = = = = == = = =
    '==
    '==  v.3401.0330- 30Mar2017= Fixes and Changes-..-
    '==     >> Fixes for creditNotes (now from payment and refund)-.
    '==        and dropped cashout..
    '==     >>  3401.417- Add JMx Prefix to InvoiceNo on Invoice..
    '==
    '==  v.3403.0513- 13May2017=Laybys-..-
    '==     >> Add new function for Layby Labels)-.
    '==
    '==  v.3403.1102- 02-Nov-2017-=Statements-..-
    '==     >> Add Preview option for Statement.-.
    '==  v.3403.1107- 02-Nov-2017-=Statements-..-
    '==     >> Show Payment Reversals..-.
    '==
    '==     == THIS now upgraded to Build 3411---.
    '==        >> 3411.1117 -- Fixes to Print Sales Invoice for Empty Payments...
    '==
    '=--  3411.1120  -- For Statements- PrintPreview needs this for printing from Preview-
    '== -  = RESET this when no more pages- (For printing from Dialog).
    '= mIntPageNo = 0
    '==
    '==
    '==   3411.0107 -- 07-Jan-2018== .
    '==        -- For Sale Invoice + (JobTracking and Job Sale..) 
    '==               Print Comments (if any) as "Job-Report"..  . 
    '==        -- For Sale Invoice + PDF- Use PrintToFile if path given..
    '==
    '==   3411.0131=  Fixes to Stock Report...
    '==                  AND-   Fix to Print Grid to reduce width.. 
    '==   3411.0208=  08-Feb-2018=
    '==            --  Fixes to Sales Invoice (Address Window)..
    '==            --  Add suburb etc to cutomer Address..
    '==
    '==  IN PRODUCTION- 07-Feb-2019--
    '==  IN PRODUCTION- 07-Feb-2019--
    '==
    '==   Updated.- 3519.0208 08-Feb-2019= 
    '==     -- Fixes to Tax Invoice to Print ABN and bank stuff.-
    '==
    '==  == THIS now upgraded to Build 3519---.
    '==    >> 3519.0211 -- Fixes to PrintInvoice (QUOTE)-  has no Payments datatable....
    '==
    '==         - Add code to actually Cancel Layby if requested...
    '==
    '==   Updated.- 3519.0221  Started 21-Feb-2019= 
    '==     -- Fixes to Sales-Invoice printing-  No terms or Bank stuff if on NON-account Sale... 
    '==
    '==   Updated.- 3519.0224  Started 22-Feb-2019= 
    '==     -- Update to Debtors Report- Add option for Summary only.... 
    '==     -- Update to Invoice- Fix error in 17-Line invoice No 584 from JH Williams.... 
    '==             (17 lines means it should have thrown new page to print totals.)
    '==             and drop underscores from short name at the top..
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==    -- Customer Admin-  Include Refunds in customer Info Tab..
    '==         ALSO- frmShowInvoice:  Show Refund amounts destinations.. (Refund as cash, CreditNore etc.)
    '==         ALSO- Ditto for printing Invoice  (clsSalePrintDocs.)
    '==    
    '==
    '==  NEW VERSION 4.--
    '==  NEW VERSION 4.--
    '==
    '==    -- 4201.0618.  11/18-June-2019-   
    '==         --  Debtors Report and Statements. 
    '==               Show Credit-Note Available balance and Cust. Phone No....
    '== NEW revision-
    '==
    '==    -- 4201.0929/930/1002/1003.  03-Oct-2019-  Started 19-September-2019-
    '==        -- Quote (SalesInvoice) printing- Removed "Invoice Paid" text at foot...  
    '==
    '== NEW revision to fix previous.-
    '==
    '==    -- 4201.1013.  13-Oct-2019..
    '==         NEW- 13-Oct-2019-
    '==            ADD PREVIEW OPTION to Print Debtors Report..
    '==
    '== MORE NEW revision to fix previous.-
    '==
    '==    -- 4201.1027.  27-Oct-2019..
    '==         NEW- 13-Oct-2019-
    '==           FIX PREVIEW OPTION to Print Debtors Report..
    '==   -27-Oct-2019-
    '==    If Not ev.HasMorePages Then
    '==-  = RESET this when no more pages- (For printing from Dialog).
    '==       miPageNo = 0
    '==    End If
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1113.  06-Nov-2019-  Started 06-November-2019-
    '==      -- Statements printing-  Adjust Lines per page to stop overprinting.. 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =  = = = = = = =
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
    '==      --  PLUS-  MAKE new class "clsDebtorsReport" to give report its own class.
    '==
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    '-  https://social.msdn.microsoft.com/Forums/vstudio/en-US/4e6e60f8-55fe-4a14-848f-c8c1103864ff/printpreviewdialog-how-can-i-add-a-button-to-select-a-printer

    '= Here is what I did to fix the problem.

    '=    I created a class called PrintPreviewDialogSelectPrinter. 
    '=  In it I have two subs.  Basically, the class inherits the PrintPreviewDialog, adds a button that functions as I want,
    '--   and removes the button that doesn't.  I finally found a sample online that I could understand. 
    '-  I'm sure there are better ways of doing this, but it works very well.  Call it just like PrintPreviewDialog.


    '= Very good solution. Thanks a lot.

    '========  Mod to above..==

    '= I allow myself some remarks to the code:
    '= 1. Moving the initialising code to sub New() prevents errors at second call
    '= 2. It is enough, to add a handler to the existing button. One may not exchange it.
    '= 3. It is usefull, to close the dialog, wenn job is done.

    '= So that is what I use:

    Friend Class PrintPreviewDialogWithPrinterItem
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
    End Class  '-PrintPreviewDialogWithPrinterItem-

    '=== the end of sub-class= 


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

    '--INVOICE-
    Private mStrTermsText As String = ""

    '- Current INVOICE DETAILS --
    '- Current INVOICE DETAILS --

    Private mIntInvoice_id As Integer = -1
    Private mbIsQuote As Boolean = False
    Private mbIsRefund As Boolean = False
    Private mbIsLayby As Boolean = False

    '-- invoice data.-
    Private mDataTableInvoice As DataTable
    Private mDataTableSaleItems As DataTable
    Private mDataTablePayments As DataTable
    Private mDataTablePaymentDetails As DataTable

    Private mIntItemsPrinted As Integer = 0

    '-- Printer to Use...--
    Private msSelectedPrinterName As String = ""
    Private mIntPageNo As Integer = 0

    '- receipt-
    Private mColReportLines As Collection '--receipt lines-
    Private msReportTitle As String = ""

    '--  Print dataGridView stuff..--
    '--  Print dataGridView stuff..--
    Private mbShadeAlternateRows As Boolean = False  '=3411.0131=
    Private msReportSubHeading As String = ""
    Private msReportSubHeading2 As String = ""

    Private mDataGridView1 As DataGridView
    Private mIntTotalWidth As Integer = 0

    Private mbFirstPage As Boolean = True
    Private mColColumnLefts As Collection
    Private mColColumnWidths As Collection

    '-- formatting strings..-
    Private mStrFormat1 As StringFormat
    Private mStrFormatRight As StringFormat

    Private mIntCellHeight As Integer = 0
    Private mIntHeaderHeight As Integer = 0

    Private mIntCurrentRow As Integer = 0
    Private mIntTopMargin As Integer = 0
    '= = = = = = = = = = = = = = = = = = == 

    '-- Labels-
    Private mColStockLabels As Collection
    Private msItemBarcodeFontName As String = ""
    Private mIntItemBarcodeFontSize As Integer = 9

    '--labels- 
    Private miLineCount As Integer = 0
    Private miPageNo As Integer = 0

    '- S t a t e m e n t s --
    '- S t a t e m e n t s --
    '- S t a t e m e n t s --
    '- bank stuff-
    Private mDateCutOff As Date
    Private msPOS_Terms As String = ""
    Private msBankName As String = ""
    Private msAccountName As String = ""
    Private msBSB1 As String = ""
    Private msBSB2 As String = ""
    Private msAccountNo As String = ""

    Private mColInvoices As Collection
    '==3303.0114= 14Jan2017=
    Private mColRefunds As Collection
    Private mIntPagesPrintedCount As Integer

    Private mDecStatementTotalTax As Decimal
    Private mDecStatementTotalDebits As Decimal
    '= Private mDecStatementTotalDebitsNonTaxable As Decimal
    Private mDecStatementTotalCredits As Decimal

    Private mDecStatementTotalOutstanding As Decimal
    '-- aged totals.-
    Private mDecStatementAgedTotalCurrent As Decimal
    Private mDecStatementAgedTotal30 As Decimal
    Private mDecStatementAgedTotal60 As Decimal
    Private mDecStatementAgedTotal90 As Decimal

    Private mColStatementDetailPages As Collection '- invoices saved into pages into overall collection-
    Private mbStatementSummaryOnNewPage As Boolean

    Private mIntTotalPageCount As Integer = 0

    '-- Debtors Report--
    Private mColReportCustomers As Collection
    Private mIntCustomerCount As Integer  '--track customers-
    Private mIntInvoiceCount As Integer   '-track invoices per customer-

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

    WriteOnly Property TermsText() As String
        Set(ByVal Value As String)
            mStrTermsText = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property isQuote() As Boolean
        Set(ByVal value As Boolean)
            mbIsQuote = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = =

    '=3519.0211-
    WriteOnly Property isLayby() As Boolean
        Set(ByVal value As Boolean)
            mbIsLayby = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = =

    WriteOnly Property DataTableInvoice() As DataTable
        Set(ByVal value As DataTable)
            mDataTableInvoice = value
        End Set
    End Property  '--DataTableInvoice-
    '= = = = = = = = = = = =

    WriteOnly Property DataTableSaleItems() As DataTable
        Set(ByVal value As DataTable)
            mDataTableSaleItems = value
        End Set
    End Property  '--DataTable sale items-
    '= = = = = = = = = = = =

    WriteOnly Property DataTablePayments() As DataTable
        Set(ByVal value As DataTable)
            mDataTablePayments = value
        End Set
    End Property  '--DataTablePayments-
    '= = = = = = = = = = = = = = = = = = =


    WriteOnly Property DataTablePaymentDetails() As DataTable
        Set(ByVal value As DataTable)
            mDataTablePaymentDetails = value
        End Set
    End Property  '--DataTablePay details-
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
            msPOS_Terms = mSysInfo1.item("POS_ACCOUNTTERMS")
            msBankName = mSysInfo1.item("POS_BUSINESSBANKNAME")
            msAccountName = mSysInfo1.item("POS_BUSINESSACCOUNTNAME")
            msBSB1 = mSysInfo1.item("POS_BUSINESSACCOUNTBSB1")
            msBSB2 = mSysInfo1.item("POS_BUSINESSACCOUNTBSB2")
            msAccountNo = mSysInfo1.item("POS_BUSINESSACCOUNTNO")
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
    '= = = = = = = = = = = = = =

    '= = = = = = = = = = = = = = = = =
    '-===FF->

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

        '== With Printer
        '==    .FillStyle = 0  '--fill with colour.-
        '==    .FillColor = lngBG '== &HDCDCDC  '-LightGrey--
        '==    .FontName = UserFont.sName
        '==    .FontSize = UserFont.lngSize
        '==    .FontBold = UserFont.bBold
        '==    .FontUnderline = UserFont.bUnderline
        '==    .FontItalic = UserFont.bItalic
        '== End With

        '--  draw all boxes..--
        '-- name box..-
        '== Printer.Line (lngSigX, lngSigY)-(lngSigX + lngNameBoxWidth, lngSigY + lngBoxDepth), , B  '--PrintName box..--
        lngYpos = mlDrawBox(ev, lngSigX, lngSigY, lngNameBoxWidth, lngBoxDepth, lngBG)

        '--make Sig. box..--
        '==Printer.Line (lngSigX, lngSigY2)-(lngSigX + lngSignBoxWidth, lngSigY2 + lngBoxDepth), , B  '-- signed box..--
        lngYpos = mlDrawBox(ev, lngSigX, lngSigY2, lngSignBoxWidth, lngBoxDepth, lngBG)

        '--make Date box..--
        '== Printer.Line (lngSigX2, lngSigY2)-(lngSigX2 + lngDateBoxWidth, lngSigY2 + lngBoxDepth), , B
        lngYpos = mlDrawBox(ev, lngSigX2, lngSigY2, lngDateBoxWidth, lngBoxDepth, lngBG)

        '-- Write the captions..-
        '== With Printer
        '==    .FillStyle = 1  '-reset to transparent..-
        '==    '==lngTextHeight = .TextHeight(sDateCaption)
        '== End With
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

    '-- END OF  Re-Written  PRINT SERVICE function..
    '-- END OF  Re-Written  PRINT SERVICE function..

    '= = = = = = = = = = = = = = = = =
    '-===FF->

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
    '-===FF->

    '--  Individual Douments Event handlers..
    '--  Individual Douments Event handlers..
    '--  Individual Douments Event handlers..

    '-- PAGE EVENT support for Print DataGridView..-
    '-- Print the DataGridView NEXT PAGE -
    '--    Roughly based on:
    '--    http://www.codeproject.com/Articles/28046/Printing-of-DataGridView

    Public Function mbPrintDataGridView_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
        Dim intLeftMargin As Integer
        Dim intTmpWidth, intCount As Integer
        Dim bMorePagesToPrint As Boolean
        Dim strDate As String = DateTime.Now.ToLongDateString() & " " & _
                        DateTime.Now.ToShortTimeString()
        Dim strFormat1 As StringFormat
        Dim fontPageHdr, fontHdr, fontContent, fontSmall As Font
        Dim font1 As userFontDef
        '-- A Rectangle has Left, Top, Width, height--
        Dim rectPageBounds As Rectangle = ev.PageBounds    '-- printable rectangle-
        Dim intPrintHeight As Integer = rectPageBounds.Height - 32
        Dim intPrintWidth As Integer = rectPageBounds.Width - 100  '=3411.0131-- was 32 -
        Dim intXpos, intXpos2, intYpos, intYpos2 As Integer

        Dim GridRow1 As DataGridViewRow '= = mDataGridView1.Rows(mIntCurrentRow)
        Dim intRowHdrWidth As Integer = 44


        fontPageHdr = New Font("Tahoma", 9, FontStyle.Bold)  '--page hdr texts-
        fontHdr = New Font("Tahoma", 8, FontStyle.Regular)  '--col hdr texts-
        fontContent = New Font("Lucida Sans", 8, FontStyle.Regular)
        fontSmall = New Font("Lucida Sans", 7, FontStyle.Regular)

        font1.sName = "Lucida Sans"     '== "Tahoma" '== Printer.FontName = "Tahoma"
        font1.lngSize = 8 '==Printer.FontSize = 18
        font1.bBold = False '== Printer.Font.Bold = False '--reset to normal IN CASE LEFT OVER..-
        font1.bUnderline = False '= Printer.Font.Underline = False
        font1.bItalic = False

        '- Set the left margin
        intLeftMargin = 16  '=rectPageBounds.Left  '= ev.MarginBounds.Left
        '-- Set the top margin
        mIntTopMargin = rectPageBounds.Top + 16  '=rectPageBounds.Top   '=ev.MarginBounds.Top
        '--  Whether more pages have to print or not
        bMorePagesToPrint = False
        intTmpWidth = 0
        mIntPageNo += 1

        Try  '--main try-
            If mbFirstPage Then
                Call mbInitialiseBusiness()
                '-- total width.-
                mIntTotalWidth = 0
                For Each dgvGridCol1 As DataGridViewColumn In mDataGridView1.Columns
                    mIntTotalWidth += dgvGridCol1.Width
                Next
                Dim intLeftPos As Integer = intLeftMargin + intRowHdrWidth
                '--  For the first page to print set the cell width and header height---
                For Each GridCol1 As DataGridViewColumn In mDataGridView1.Columns
                    '=intTmpWidth = CInt(Math.Floor(GridCol1.Width / mIntTotalWidth * mIntTotalWidth * (ev.MarginBounds.Width / mIntTotalWidth)))
                    intTmpWidth = _
                        CInt(Math.Floor(GridCol1.Width / mIntTotalWidth * mIntTotalWidth * (intPrintWidth / mIntTotalWidth)))

                    mIntHeaderHeight = _
                        (ev.Graphics.MeasureString(GridCol1.HeaderText, GridCol1.InheritedStyle.Font, intTmpWidth).Height) + 11

                    '-- Save width and height of headers
                    mColColumnLefts.Add(intLeftPos)
                    mColColumnWidths.Add(intTmpWidth)
                    intLeftPos += intTmpWidth
                Next GridCol1
                mIntCurrentRow = 0
                mbFirstPage = False
            End If  '--first page.-

            '-- Start of Every Page.--
            '-- Start of Every Page.--
            '-- Draw Header text--
            'ev.Graphics.DrawString(msReportTitle & " -        Page: " & mIntPageNo, _
            '         New Font(mDataGridView1.Font, FontStyle.Bold), _
            '               Brushes.Black, ev.MarginBounds.Left, _
            '                   ev.MarginBounds.Top - ev.Graphics.MeasureString("Testing print dataGridView", _
            '                      fontPageHdr, ev.MarginBounds.Width).Height - 13)
            intXpos = intLeftMargin + 40
            intYpos = mIntTopMargin + 16
            font1.lngSize = 9
            font1.bBold = True
            intYpos = gIntPrintTextString(ev, msReportTitle & " -     Page: " & mIntPageNo, intXpos, intYpos, font1)
            font1.lngSize = 8
            font1.bBold = False
            If (msReportSubHeading <> "") Then
                intYpos = gIntPrintTextString(ev, msReportSubHeading, intXpos, intYpos, font1)
                intYpos = gIntPrintTextString(ev, msReportSubHeading2, intXpos, intYpos, font1)
            End If
            '-- Draw Column Headers--                 
            mIntTopMargin = intYpos + 32 '= ev.MarginBounds.Top
            '--Drawing Row Hdr Cells Borders -
            ev.Graphics.DrawRectangle(Pens.Gray, _
                New Rectangle(intLeftMargin, mIntTopMargin, _
                intRowHdrWidth, mIntHeaderHeight))

            intCount = 0
            For Each GridCol1 As DataGridViewColumn In mDataGridView1.Columns
                intCount += 1
                ev.Graphics.FillRectangle(New SolidBrush(Color.LightGray), _
                    New Rectangle(mColColumnLefts(intCount), mIntTopMargin, _
                    mColColumnWidths(intCount), mIntHeaderHeight))
                ev.Graphics.DrawRectangle(Pens.Black, _
                    New Rectangle(mColColumnLefts(intCount), mIntTopMargin, _
                    mColColumnWidths(intCount), mIntHeaderHeight))
                ev.Graphics.DrawString(GridCol1.HeaderText, _
                    fontHdr, _
                    New SolidBrush(GridCol1.InheritedStyle.ForeColor), _
                    New RectangleF(mColColumnLefts(intCount), mIntTopMargin, _
                    mColColumnWidths(intCount), mIntHeaderHeight), mStrFormat1)
                '== iCount++;
            Next GridCol1
            mIntTopMargin += mIntHeaderHeight

            '-- Loop till all the grid rows not get printed-
            '--  This loop may be interrupted by Page breaks..
            While (mIntCurrentRow <= mDataGridView1.Rows.Count - 1)
                '-- get next row..
                GridRow1 = mDataGridView1.Rows(mIntCurrentRow)
                '-- Set the cell height-
                mIntCellHeight = GridRow1.Height + 5
                intCount = 0
                '- Check whether the current page settings allows more rows to print-
                If (mIntTopMargin + mIntCellHeight >= ev.MarginBounds.Height + ev.MarginBounds.Top) Then
                    '- page is full.
                    bMorePagesToPrint = True
                    Exit While
                    '-- falls out the bottom..
                Else  '--can print more lines-
                    intCount = 0  '-track column indexes..
                    '-- print Row header-
                    '-- shade alternate rows..-
                    If mbShadeAlternateRows Then
                        If (mIntCurrentRow Mod 2) = 1 Then
                            ev.Graphics.FillRectangle(New SolidBrush(Color.Gainsboro), _
                                  New Rectangle(intLeftMargin, mIntTopMargin, _
                                    intRowHdrWidth, mIntCellHeight))
                        End If
                    End If  '-shade-
                    '-- Row no..
                    '-- Draw - Pad at the right by 4..
                    ev.Graphics.DrawString(CStr(mIntCurrentRow + 1), fontContent, _
                        New SolidBrush(Color.Black), _
                        New RectangleF(intLeftMargin, _
                             mIntTopMargin, (intRowHdrWidth - 4), mIntCellHeight), mStrFormatRight)
                    '--Drawing Row Hdr Cells Borders -
                    ev.Graphics.DrawRectangle(Pens.Gray, _
                        New Rectangle(intLeftMargin, mIntTopMargin, _
                        intRowHdrWidth, mIntCellHeight))
                    '--NOW..  draw the column contents this row..--
                    Try
                        For Each Cell1 As DataGridViewCell In GridRow1.Cells
                            '-- shade alternate rows..-
                            If mbShadeAlternateRows Then
                                If (mIntCurrentRow Mod 2) = 1 Then
                                    ev.Graphics.FillRectangle(New SolidBrush(Color.Gainsboro), _
                                          New Rectangle(mColColumnLefts(intCount + 1), mIntTopMargin, _
                                            mColColumnWidths(intCount + 1), mIntHeaderHeight))
                                End If
                            End If  '-shade-
                            If (Not IsDBNull(Cell1.Value)) AndAlso (Not (Cell1.Value Is Nothing)) Then
                                '-- choose cell content data alignment left or right.-
                                strFormat1 = mStrFormat1  '--left-
                                If mDataGridView1.Columns(intCount).DefaultCellStyle.Alignment = _
                                                              DataGridViewContentAlignment.MiddleRight Then
                                    strFormat1 = mStrFormatRight
                                End If
                                '-- Draw cell Content-
                                ev.Graphics.DrawString(Cell1.Value.ToString(), fontContent, _
                                    New SolidBrush(Cell1.InheritedStyle.ForeColor), _
                                    New RectangleF(mColColumnLefts(intCount + 1), _
                                    mIntTopMargin, mColColumnWidths(intCount + 1), mIntCellHeight), strFormat1)
                            End If '--nul-
                            '--Drawing Cells Borders -
                            ev.Graphics.DrawRectangle(Pens.Gray, _
                                New Rectangle(mColColumnLefts(intCount + 1), mIntTopMargin, _
                                mColColumnWidths(intCount + 1), mIntCellHeight))
                            intCount += 1
                        Next Cell1
                    Catch ex As Exception
                        MsgBox("Error in Print DataGridView." & vbCrLf & _
                                 "Printing Cell Contents." & ex.Message, MsgBoxStyle.Exclamation)
                    End Try
                    mIntTopMargin += mIntCellHeight
                    mIntCurrentRow += 1
                End If  '--end of page-
            End While  '--rows.-

            '- Print footer line..
            ev.Graphics.DrawString(msBusinessShortName & ": " & msVersionPOS & ";  Printed: " & strDate, fontSmall, _
                       New SolidBrush(Color.Black), _
                                  New RectangleF(ev.MarginBounds.Left, mIntTopMargin + (mIntCellHeight * 2), _
                                                        mIntTotalWidth, mIntCellHeight), mStrFormat1)
            '-- If more lines exist, print another page.
            If (bMorePagesToPrint) Then
                ev.HasMorePages = True
            Else
                ev.HasMorePages = False
            End If

        Catch ex As Exception '--main try-
            MsgBox("Error in Print DataGridView." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try  '--main try-

    End Function  '--PrintDataGridView-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Print the Sales Invoice -
    '-- Print the Sales Invoice -
    '= 3519.0211-  Updated for Quotes.

    Public Function mbPrintSalesInvoice_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean

        '= mlPrtWidth = 760
        Const k_WIDTH_BC = 116      '--width of Barcode column.-
        Const k_WIDTH_SERIALNO = 120  '--width of Description column.-
        Const k_WIDTH_DESCR = 250  '--width of Description column.-
        Const k_WIDTH_TAX = 44     '--width of TAXcode column.-
        Const k_WIDTH_PRICE = 90   '--width of Price column.-
        Const k_WIDTH_QTY = 50      '--width of Qty column.-
        Const k_WIDTH_TOTAL = 90  '--width of Ext.Total column.-

        Const k_HDR_HEIGHT = 26

        Dim intGreyBGColour As Integer = &HE0E0E0&
        Dim fillColor As Color
        Dim intXpos, intXpos2, intYpos, intYpos2 As Integer
        Dim intHdrX As Integer = 150
        Dim intHdrY As Integer = 24
        Dim intLeftMarg As Integer
        Dim L1, ix, x1, intError As Integer
        Dim rowInvoice, rowDetail, rowPayment, row1 As DataRow
        Dim s1, sInvoiceNo, sDocType, strPageNo As String
        Dim sJobNo As String = "--"
        Dim dateInvoice As DateTime
        Dim sCustomerBarcode, sCustomerInfo, sABN As String
        Dim sDelivery, sComments As String
        Dim sChangeGiven, sNettRecvd As String
        Dim bIsAccountCust As Boolean = False
        Dim bIsOnAccount As Boolean = False    '==3301.607= This Invoice..
        Dim bHavePayment As Boolean = False    '==3411.1117= This Invoice..
        Dim bIsTotalsPage As Boolean
        '== Dim decExtension As Decimal
        Dim decTotalTax As Decimal = 0
        Dim decDiscount, decRounding As Decimal
        Dim decSubTotalTax, decDiscTax As Decimal

        Dim decPayAmount, decTotalPayments, decAmountDebitedToAccount As Decimal
        '==3301.627=
        Dim decCreditNoteAmountCredited As Decimal = 0
        Dim decCreditNoteAmountDebited As Decimal = 0

        Dim font1 As userFontDef
        Dim intInfoBoxWidth As Integer = 88  '= 120
        Dim intInfoBoxDepth As Integer = 27
        Dim intAddressBoxWidth As Integer = 340
        Dim intAddressBoxDepth As Integer = 100  '==3411.0208=  Was 140-
        Dim intTermsBoxDepth As Integer = 70
        '-grid-
        Dim intGridYpos, intGridYdepth As Integer
        Dim intItemsRemaining, intLinesAvailable As Integer
        Dim penGrid As Pen = Pens.LightGray

        Call mbInitialiseBusiness()
        fillColor = ColorTranslator.FromOle(intGreyBGColour)
        '= MsgBox("Ready to print Invoice to " & msSelectedPrinterName & "..", MsgBoxStyle.Information)
        '-- Format ABN for printing..-
        sABN = VB.Left(msBusinessABN, 2) & " " & Mid(msBusinessABN, 3, 3) & _
                  " " & Mid(msBusinessABN, 6, 3) & " " & Mid(msBusinessABN, 9, 3)

        '-- All pages- beginning..
        Try
            rowInvoice = mDataTableInvoice.Rows(0)   '--only row..-
            '=  3301.627=
            '== 3411.1117= rowPayment = mDataTablePayments.Rows(0)   '--only row..- 3301.627=
            '==     == THIS now upgraded to Build 3411---.
            '==        >> 3411.1117 -- Fixes to Print Sales Invoice for Empty Payments...
            '==
            '==  == THIS now upgraded to Build 3519---.
            '==    >> 3519.0211 -- Fixes for QUOTE-  (has no Payments datatable)....
            '==
            If (mDataTablePayments IsNot Nothing) AndAlso _
                                    (mDataTablePayments.Rows.Count > 0) Then
                rowPayment = mDataTablePayments.Rows(0)   '--only row..- 3301.627=
                bHavePayment = True
            End If
            If LCase(rowInvoice.Item("transactionType") = "refund") Then mbIsRefund = True
            If mbIsQuote Then
                sInvoiceNo = CStr(rowInvoice.Item("SalesOrder_id"))
                dateInvoice = rowInvoice.Item("SalesOrder_date")
                sDocType = "Quotation"
            Else
                sInvoiceNo = CStr(rowInvoice.Item("Invoice_id"))
                dateInvoice = rowInvoice.Item("Invoice_date")
                sDocType = "Invoice"
                If CInt(rowInvoice.Item("JobNumber")) > 0 Then
                    sJobNo = rowInvoice.Item("JobNumber")
                End If
            End If
            sCustomerBarcode = rowInvoice.Item("barcode")
            'sCustomerInfo = rowInvoice.Item("firstname") & " " & rowInvoice.Item("lastname") & vbCrLf & _
            '                      rowInvoice.Item("companyname") & vbCrLf & rowInvoice.Item("address") & _
            '                       vbCrLf & "Phone:  " & rowInvoice.Item("Phone")
            '=3411.0208= Re-do-
            sCustomerInfo = rowInvoice.Item("firstname") & " " & rowInvoice.Item("lastname") & vbCrLf & _
                                   rowInvoice.Item("companyname") & vbCrLf & rowInvoice.Item("address") & vbCrLf & _
                                    rowInvoice.Item("suburb") & " " & _
                                    rowInvoice.Item("state") & " " & rowInvoice.Item("postcode")
            sDelivery = rowInvoice.Item("DeliveryInstructions")
            sComments = Trim(rowInvoice.Item("Comments"))
            bIsAccountCust = False
            If (Not mbIsQuote) And (Not mbIsLayby) Then
                If mDataTableInvoice.Columns.Contains("isAccountCust") Then
                    If rowInvoice.Item("isAccountCust") Then
                        bIsAccountCust = True
                        '==3301.607=
                        If rowInvoice.Item("isOnAccount") Then  '=This invoice=
                            bIsOnAccount = True
                        End If
                    End If
                End If
            End If

            '== On Error GoTo PrintInvoice_Error
            intLeftMarg = k_LEFTMARGIN '== iixels-  (16 * PRT_UNIT) '- 240 twips..-
            '--  paint BIZ logo  top left.--
            If Not (mImageUserLogo Is Nothing) Then
                x1 = mImageUserLogo.Width
                ev.Graphics.DrawImage(mImageUserLogo, intLeftMarg + mlPrtWidth - x1, 0)
            End If
            '-- print biz name-
            intXpos = intLeftMarg
            intHdrX = intLeftMarg
            intYpos = 24

            font1.sName = "Lucida Sans Unicode"     '== "Tahoma" '== Printer.FontName = "Tahoma"
            font1.lngSize = 18 '==Printer.FontSize = 18
            font1.bBold = False '== Printer.Font.Bold = False '--reset to normal IN CASE LEFT OVER..-
            font1.bUnderline = False '= Printer.Font.Underline = False
            font1.bItalic = False

            intYpos = mlPrintTextString(ev, msBusinessShortName, intHdrX, intHdrY, font1)
            font1.lngSize = 8 '== Printer.FontSize = 8
            font1.bBold = True '== Printer.FontBold = True
            intYpos = mlPrintTextString(ev, msBusinessName & "  ABN: " & sABN, intHdrX, intYpos, font1)
            intYpos = mlPrintTextString(ev, msBusinessAddress1, intHdrX, intYpos, font1)
            intYpos = mlPrintTextString(ev, msBusinessAddress2, intHdrX, intYpos, font1)
            intYpos = mlPrintTextString(ev, "Phone:  " & msBusinessPhone, intHdrX, intYpos, font1)
            '--blank line-
            intYpos = mlPrintTextString(ev, "", intHdrX, intYpos, font1)
        Catch ex As Exception
            MsgBox("Runtime error in invoice print startup." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            ev.HasMorePages = False
            Exit Function
        End Try  '-start-
        decAmountDebitedToAccount = 0

        '-- all pages-  this must be ready for last (totals) page as well.
        decDiscount = CDec(rowInvoice.Item("discount_nett")) + CDec(rowInvoice.Item("discount_tax"))
        decDiscTax = CDec(rowInvoice.Item("discount_tax"))
        decTotalTax = CDec(rowInvoice.Item("total_tax"))
        decSubTotalTax = CDec(rowInvoice.Item("subtotal_tax"))

        '-- header stuff on 1st page only-
        mDefaultUserFont.lngSize = 9
        Try
            If (mIntPageNo <= 0) Then
                font1.lngSize = 18 '==Printer.FontSize = 18
                If mbIsQuote Then
                    intYpos = mlPrintTextString(ev, "Quotation", intHdrX, intYpos, font1, textColour.orange)
                Else  '-not quote-
                    If mbIsRefund Then
                        intYpos = mlPrintTextString(ev, "R e f u n d", intHdrX, intYpos, font1, textColour.magenta)
                    Else
                        intYpos = mlPrintTextString(ev, "Tax Invoice", intHdrX, intYpos, font1)
                        If bIsOnAccount Then
                            decAmountDebitedToAccount = CDec(rowInvoice.Item("total_inc"))
                        End If
                    End If
                End If  '-quote-

                '-- Blank line..
                font1.lngSize = 9  '--for hdr line-
                intYpos = mlPrintTextString(ev, "", intHdrX, intYpos, font1)

                '== decTotalTax = 0
                '-- Print Invoice No. and Date..
                intYpos2 = intYpos  '-- set Ypos for invoice No..

                'decDiscount = CDec(rowInvoice.Item("discount_nett")) + CDec(rowInvoice.Item("discount_tax"))
                'decDiscTax = CDec(rowInvoice.Item("discount_tax"))
                'decTotalTax = CDec(rowInvoice.Item("total_tax"))
                'decSubTotalTax = 0

                mDefaultUserFont.lngSize = 8
                intXpos = intLeftMarg
                '-  Invoice No.-
                L1 = mlPrintTextInBox(ev, "<b>" & sDocType & " No:", _
                                  intXpos, intYpos2, 6, intInfoBoxWidth, intInfoBoxDepth, True, intGreyBGColour)
                '=3401.417- Add JMx Prefix.
                L1 = mlPrintTextInBox(ev, "JMX " & sInvoiceNo, _
                                   intXpos + intInfoBoxWidth, intYpos2, 6, intInfoBoxWidth, intInfoBoxDepth, True)
                intXpos += (intInfoBoxWidth * 2) + 18

                '-- Job No- 3103.117=
                L1 = mlPrintTextInBox(ev, "<b>Job No:", _
                                   intXpos, intYpos2, 6, intInfoBoxWidth, intInfoBoxDepth, True, intGreyBGColour)
                L1 = mlPrintTextInBox(ev, sJobNo, _
                                   intXpos + intInfoBoxWidth, intYpos2, 6, intInfoBoxWidth, intInfoBoxDepth, True)
                intXpos += (intInfoBoxWidth * 2) + 18

                '- date-
                L1 = mlPrintTextInBox(ev, "<b>" & sDocType & " Date:", _
                              intXpos, intYpos2, 6, intInfoBoxWidth, intInfoBoxDepth, True, intGreyBGColour)
                s1 = Format(dateInvoice, "dd-MMM-yyyy")
                L1 = mlPrintTextInBox(ev, s1, _
                               intXpos + intInfoBoxWidth, intYpos2, 6, intInfoBoxWidth, intInfoBoxDepth, True)
                intXpos += (intInfoBoxWidth * 2) + 18

                '-Cust No.-
                L1 = mlPrintTextInBox(ev, "<b>Cust. No:", _
                              intXpos, intYpos2, 6, intInfoBoxWidth, intInfoBoxDepth, True, intGreyBGColour)
                L1 = mlPrintTextInBox(ev, sCustomerBarcode, _
                                intXpos + intInfoBoxWidth, intYpos2, 6, intInfoBoxWidth, intInfoBoxDepth, True)
                '--blank-=
                intXpos = intLeftMarg
                intYpos = mlPrintTextInBox(ev, "", _
                                 intXpos, intYpos2, 6, intInfoBoxWidth, intInfoBoxDepth, True)
                '-- Customer and Delivery..
                '-- set top of address box-
                intYpos = intYpos2 + intInfoBoxDepth + 13 + 40  '==3411.0208=
                intYpos2 = intYpos

                mDefaultUserFont.lngSize = 9
                font1.lngSize = 9
                L1 = mlPrintTextString(ev, "To:", intXpos + 20, intYpos2 + 16, font1)
                L1 = mlPrintTextInBox(ev, sCustomerInfo, _
                                  intXpos, intYpos2, 80, intAddressBoxWidth, intAddressBoxDepth, True)
                L1 = mlPrintTextInBox(ev, "<b>Deliver To:" & vbCrLf & sDelivery, _
                                  intXpos + mlPrtWidth - intAddressBoxWidth, intYpos2, 6, _
                                                                        intAddressBoxWidth, intAddressBoxDepth, True)
                '-- GRID for Detail Lines..--
                intGridYpos = intYpos2 + intAddressBoxDepth + 12
                mIntItemsPrinted = 0   '--just started-
                intItemsRemaining = mDataTableSaleItems.Rows.Count - mIntItemsPrinted
                '-- vary lines avail. dep. on if Totals page.
                If intItemsRemaining <= 13 Then  '- single page only.-
                    '=3519.0224=  
                    '-- Update to ClsPrintSaleDoc- Fix error in 17-Line invoice No 584 from JH Williams.... 
                    '==             (17 lines means it should have thrown new page to print totals.)
                    intGridYdepth = 450
                    intLinesAvailable = 13  '=3519.0224- Was 14  '- SAY 31 px per line..
                    bIsTotalsPage = True
                Else  '-there will be more than 1 page
                    '- can print more on 1st page-
                    intGridYdepth = 680
                    intLinesAvailable = 18  '= 3519.0224- Was 20  '- SAY 31 px per line..
                    bIsTotalsPage = False
                End If  '-rem-

            Else  '-subseq pages - P2 ++
                '-- Just show invoice and page no above grid..-
                intYpos += 16
                font1.lngSize = 9
                intYpos = gIntPrintTextString(ev, "Invoice No: " & sInvoiceNo & _
                                                 "  From Page: " & CStr(mIntPageNo), intLeftMarg + 480, intYpos, font1)
                '-- GRID for Detail Lines..--
                intGridYpos = intYpos2 + 124
                intItemsRemaining = mDataTableSaleItems.Rows.Count - mIntItemsPrinted
                '-- vary lines avail. dep. on if Totals page.
                If intItemsRemaining <= 13 Then  '- 3519.0224- Was 20 Then  '- last page .-
                    intGridYdepth = 640
                    intLinesAvailable = 13  '=3519.0224- Was 14  20  '- SAY 31 px per line..
                    bIsTotalsPage = True
                Else  '-there will be more than 1 page
                    '- can print more on 1st page-
                    intGridYdepth = 840
                    intLinesAvailable = 18  '= 3519.0224- was 24  '- SAY 31 px per line..
                    bIsTotalsPage = False
                End If  '-rem-
            End If  '-1st/next page-
        Catch ex As Exception
            MsgBox("Runtime error in invoice print Page Setup." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            ev.HasMorePages = False
            Exit Function
        End Try  '-start-

        mDefaultUserFont.lngSize = 9  '-for grid stuff.
        '--  all pages --
        mIntPageNo += 1
        strPageNo = "Page: " & CStr(mIntPageNo)
        intXpos = intLeftMarg

        '--draw the "grid"..
        Try
            Call mlDrawLine(ev, intXpos, intGridYpos, mlPrtWidth)  '--top bar-
            '--column lines- 8 spaces, nine lines-
            Dim arrayIntWidths() As Integer = _
                          {k_WIDTH_BC, k_WIDTH_SERIALNO, k_WIDTH_DESCR, k_WIDTH_TAX, k_WIDTH_PRICE, k_WIDTH_QTY, k_WIDTH_TOTAL}
            For ix = 0 To UBound(arrayIntWidths)
                ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)
                intXpos += arrayIntWidths(ix)
            Next ix
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
            Call mIntPrintTextInRectangle(ev, " Bar Code", intXpos, intGridYpos, _
                                                     k_WIDTH_BC, k_HDR_HEIGHT, font1, textColour.black, False, True)
            '== e.Graphics.DrawString("Bar Code", drawFont, drawBrush, drawRect, drawFormat)
            intXpos += k_WIDTH_BC
            Call mIntPrintTextInRectangle(ev, " Serial No.", intXpos, intGridYpos, _
                                                     k_WIDTH_BC, k_HDR_HEIGHT, font1, textColour.black, False, True)
            intXpos += k_WIDTH_SERIALNO
            Call mIntPrintTextInRectangle(ev, " Description", intXpos, intGridYpos, _
                                                      k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False, True)
            intXpos += k_WIDTH_DESCR
            Call mIntPrintTextInRectangle(ev, "Tax", intXpos, intGridYpos, _
                                            k_WIDTH_TAX, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
            intXpos += k_WIDTH_TAX
            Call mIntPrintTextInRectangle(ev, "Price $", intXpos, intGridYpos, _
                                           k_WIDTH_PRICE, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
            intXpos += k_WIDTH_PRICE
            Call mIntPrintTextInRectangle(ev, "Qty", intXpos, intGridYpos, _
                                             k_WIDTH_QTY, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
            intXpos += k_WIDTH_QTY
            L1 = mIntPrintTextInRectangle(ev, "Total $", intXpos, intGridYpos, _
                                             k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True, True) '-Right Align.-
            '-- L1 has line height.
            '--Print Actual detail lines..
            '-- make line rect deep enough for overflow lines..
            Dim intItemLineHeight As Integer = k_HDR_HEIGHT + (k_HDR_HEIGHT \ 10) '=3411.0208=  + (k_HDR_HEIGHT \ 6)
            Dim intCurrentYpos As Integer   '--save for shading-
            Dim decQty As Decimal, intQty As Integer, decFraction As Decimal

            font1.bBold = False
            mDefaultUserFont.lngSize = 9
            font1.lngSize = 8
            intYpos = intGridYpos + k_HDR_HEIGHT + (L1 \ 2) '--vert. pos. to 1st detail line..-
            '- jump over continuation text..
            If (mIntPageNo > 1) Then
                intYpos += k_HDR_HEIGHT
            End If
            intCurrentYpos = intYpos
            While (intLinesAvailable > 0) And (mIntItemsPrinted < mDataTableSaleItems.Rows.Count)
                rowDetail = mDataTableSaleItems.Rows(mIntItemsPrinted)
                intXpos = intLeftMarg
                '--  shade alternate rows-
                If (intLinesAvailable Mod 2) = 1 Then
                    ev.Graphics.FillRectangle(New SolidBrush(ColorTranslator.FromOle(&HF0F0F0)), _
                                                         intLeftMarg, intCurrentYpos, k_PRTWIDTH, intItemLineHeight)
                End If
                Call mIntPrintTextInRectangle(ev, rowDetail.Item("barcode"), intXpos, intYpos, _
                                                          k_WIDTH_BC, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                '== e.Graphics.DrawString("Bar Code", drawFont, drawBrush, drawRect, drawFormat)
                intXpos += k_WIDTH_BC
                s1 = "--"
                If mDataTableSaleItems.Columns.Contains("serialNumber") Then
                    s1 = rowDetail.Item("serialNumber")
                End If
                Call mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                                           k_WIDTH_BC, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                intXpos += k_WIDTH_SERIALNO
                Call mIntPrintTextInRectangle(ev, rowDetail.Item("description"), intXpos, intYpos, _
                                                          k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False)
                intXpos += k_WIDTH_DESCR
                Call mIntPrintTextInRectangle(ev, rowDetail.Item("sales_taxCode"), intXpos, intYpos, _
                                                k_WIDTH_TAX, k_HDR_HEIGHT, font1, textColour.black, True) '-Right Align.-no box
                intXpos += k_WIDTH_TAX
                '-Actual sell Price-
                s1 = FormatCurrency(rowDetail.Item("sellActual_inc"), 2)
                s1 = Replace(s1, "$", "")
                Call mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                               k_WIDTH_PRICE, k_HDR_HEIGHT, font1, textColour.black, True) '-Right Align.-
                intXpos += k_WIDTH_PRICE
                decQty = CDec(rowDetail.Item("quantity"))
                intQty = Int(Abs(decQty))
                decFraction = decQty - CDec(intQty)  '--see if any decimals..
                If decFraction = 0 Then '-- integer only-
                    s1 = CStr(intQty)  '--"qty"
                Else '--has fraction-
                    s1 = Format(decQty, "   0.00")   '--"qty"
                End If
                Call mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                                 k_WIDTH_QTY, k_HDR_HEIGHT, font1, textColour.black, True) '-Right Align.-
                intXpos += k_WIDTH_QTY
                '-- TEMP- Extension should come from rowDetail..
                '==decExtension = CDec(rowDetail.Item("sellActual_inc")) * CDec(rowDetail.Item("quantity"))
                s1 = FormatCurrency(rowDetail.Item("total_inc"), 2)
                s1 = Replace(s1, "$", "")
                L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                                 k_WIDTH_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-Right Align.-
                intYpos += intItemLineHeight  '= L1
                intCurrentYpos = intYpos
                '--  Compute total ITEM tax..
                '= 3519.0224- Get fromInvoice.
                '--  decSubTotalTax += CDec(rowDetail.Item("total_inc")) - CDec(rowDetail.Item("total_ex"))
                mIntItemsPrinted += 1
                intLinesAvailable -= 1
            End While  '-available-
        Catch ex As Exception
            MsgBox("Runtime error in invoice print Details Page." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            ev.HasMorePages = False
            Exit Function
        End Try  '-start-

        '-  save current YPos for Report..
        Dim intReportYpos As Integer = intYpos
        '= Next rowDetail

        '-- DONE all lines this page-
        '= intYpos = intGridYpos + intGridYdepth + 24

        '-- If more to come, or totals to come, then exit this page..
        '=3519.0224=  
        '-- Update to ClsPrintSaleDoc- Fix error in 17-Line invoice No 584 from JH Williams.... 
        '==          (17 lines means it should have thrown new page to print totals.)
        If (mIntItemsPrinted < mDataTableSaleItems.Rows.Count) Or (Not bIsTotalsPage) Then  '-still more to come-
            '--print this page and come back for more-
            intYpos = gIntPrintTextString(ev, "Invoice No: " & sInvoiceNo & _
                                 "  continues on Page: " & CStr(mIntPageNo + 1), intLeftMarg + 480, intYpos + 16, font1)
            '-- Footer--
            Call mbPageFooter(ev, strPageNo)
            ev.HasMorePages = True
            Exit Function
        End If
        '-- all items printed-
        '-- DONE all lines this page-
        intYpos = intGridYpos + intGridYdepth + 24

        '=3411.0107=  Print JobReport if any..
        If (sComments <> "") Then
            Dim sJobReport As String = ""
            Dim iPos1, intChars, intReportLines As Integer
            Dim sizeMeasure, sizeOut As SizeF
            iPos1 = InStr(sComments, "Job-Report:", CompareMethod.Text)
            If iPos1 > 0 Then
                sJobReport = Mid(sComments, iPos1 + 11)
                '-compute lines needed..
                sizeMeasure = New SizeF(400.0F, 400.0F) '-half the page-
                sizeOut = ev.Graphics.MeasureString(sJobReport, _
                                                    New Font("Lucida Sans", 8, FontStyle.Regular), sizeMeasure, _
                                                     StringFormat.GenericTypographic, intChars, intReportLines)
                If intReportLines <= intLinesAvailable Then
                    '--can print report here.  NO BOX..
                    intXpos = intLeftMarg + k_WIDTH_BC   '-at serial-
                    '- sJobReport starts with cr/lf..
                    L1 = mlPrintTextInBox(ev, "<b>Job-Report:" & sJobReport, _
                                           intXpos, intReportYpos, 6, 400, 400, False)
                    '-- and fall through to print totals..
                Else '-get new page..  and come back again for report..-
                    '-- Footer--
                    Call mbPageFooter(ev, strPageNo)
                    ev.HasMorePages = True
                    Exit Function
                End If
            End If '-pos-
        End If  '-comments-

        '-- Invoice Summary Info..--
        intXpos = intLeftMarg
        intYpos = intGridYpos + intGridYdepth + 12

        Try
            '-- SUMMARY- draw boxes.-
            Dim intTotalsBoxDepth As Integer = 160  '=3411.0208=  intAddressBoxDepth + 20
            Dim intPayDescrWidth As Integer = 160
            Dim intPayAmtWidth As Integer = 120
            '=3519.0317=
            Dim decRefundedAsCash As Decimal = 0
            Dim decRefundedAsCreditNote As Decimal = 0
            Dim decRefundedAsEftPosDr As Decimal = 0
            Dim decRefundedAsEftPosCr As Decimal = 0

            '== 3301.627=
            '= decCreditNoteAmountCredited = CDec(rowPayment.Item("creditNoteAmountCredited"))
            '-3401.330= refundAsCreditNoteCredited-
            If bHavePayment Then  '-have payment record.
                decCreditNoteAmountCredited = CDec(rowPayment.Item("creditNotePaymentCredited")) + _
                                                          CDec(rowPayment.Item("refundAsCreditNoteCredited"))
                decCreditNoteAmountDebited = CDec(rowPayment.Item("creditNoteAmountDebited"))
                '=3519.0317=
                If mbIsRefund Then
                    decRefundedAsCash = CDec(rowPayment.Item("RefundCashAmount"))
                    decRefundedAsCreditNote = CDec(rowPayment.Item("RefundAsCreditNoteCredited"))
                    decRefundedAsEftPosDr = CDec(rowPayment.Item("RefundAsEftPosDr"))
                    decRefundedAsEftPosCr = CDec(rowPayment.Item("RefundAsEftPosCr"))
                End If ' -refund-
            End If  '-payment record
 
            If (Not (mbIsQuote Or mbIsRefund)) Then  '-ie. is sale-
                L1 = mlPrintTextInBox(ev, "<b>Payment Details:", _
                                       intXpos, intYpos, 16, intAddressBoxWidth, intTotalsBoxDepth, True)
                '--payments.. if any..

                '-- get payment change if any..-
                '==If (Not bIsAccountCust) AndAlso _
                '==      (Not (mDataTablePayments Is Nothing)) AndAlso (mDataTablePayments.Rows.Count > 0) Then
                If (Not (mDataTablePayments Is Nothing)) AndAlso (mDataTablePayments.Rows.Count > 0) Then
                    row1 = mDataTablePayments.Rows(0)
                    sNettRecvd = FormatCurrency(row1.Item("nettAmountCredited"), 2)
                    s1 = FormatCurrency(row1.Item("changeGiven"), 2)
                    sChangeGiven = RSet(s1, 12)
                End If
                intXpos = intLeftMarg + 40
                intYpos2 = intYpos + 24
                '== If (Not bIsAccountCust) AndAlso (Not (mDataTablePaymentDetails Is Nothing)) Then
                If (Not (mDataTablePaymentDetails Is Nothing)) Then
                    For Each rowDetail In mDataTablePaymentDetails.Rows
                        L1 = mIntPrintTextInRectangle(ev, rowDetail.Item("paymenttype_descr"), intXpos, intYpos2, _
                                                     intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                        L1 = mIntPrintTextInRectangle(ev, FormatCurrency(rowDetail.Item("amount"), 2), _
                                                     intXpos + intPayDescrWidth, intYpos2, _
                                                     intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                        intYpos2 += L1 + 4 '--add line height +..
                    Next rowDetail
                End If
                intYpos2 += 6 '--add half line height..
                L1 = mIntPrintTextInRectangle(ev, "Change Given: ", intXpos, intYpos2, _
                                            intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                L1 = mIntPrintTextInRectangle(ev, sChangeGiven, intXpos + intPayDescrWidth, intYpos2, _
                                              intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                intYpos2 += L1 + 6 '--add line height..
                L1 = mIntPrintTextInRectangle(ev, "Nett Amount Credited: ", intXpos, intYpos2, _
                                                  intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                L1 = mIntPrintTextInRectangle(ev, sNettRecvd, intXpos + intPayDescrWidth, intYpos2, _
                                                    intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                '==3301.627-  print Credit-Note Stuff...
                '==3301.627-  print Credit-Note Stuff...
                '==3301.627-  print Credit-Note Stuff...
                intYpos2 += L1 + 6 '--add line height..
                If (decCreditNoteAmountDebited <> 0) Then
                    s1 = FormatCurrency(decCreditNoteAmountDebited, 2)
                    L1 = mIntPrintTextInRectangle(ev, "Credit-Note Redeemed: ", intXpos, intYpos2, _
                                                      intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                    L1 = mIntPrintTextInRectangle(ev, s1, intXpos + intPayDescrWidth, intYpos2, _
                                                        intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                ElseIf (decCreditNoteAmountCredited <> 0) Then
                    s1 = FormatCurrency(decCreditNoteAmountCredited, 2)
                    L1 = mIntPrintTextInRectangle(ev, "Saved as Credit-Note : ", intXpos, intYpos2, _
                                                      intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                    L1 = mIntPrintTextInRectangle(ev, s1, intXpos + intPayDescrWidth, intYpos2, _
                                                        intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                End If  '-credit Note-
                '-- on account only..-
                If (decAmountDebitedToAccount > 0) Then
                    intYpos2 += L1 + 6 '--add line height..
                    s1 = FormatCurrency(decAmountDebitedToAccount, 2)
                    L1 = mIntPrintTextInRectangle(ev, "Amount Debited to A/c: ", intXpos, intYpos2, _
                                                      intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                    L1 = mIntPrintTextInRectangle(ev, s1, intXpos + intPayDescrWidth, intYpos2, _
                                                        intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                End If  '-debited-
            End If  '-not quote-
            '=3519.0317=
            If mbIsRefund Then
                '- show where refund went.
                L1 = mlPrintTextInBox(ev, "<b>Refund Details:", _
                        intXpos, intYpos, 16, intAddressBoxWidth, intTotalsBoxDepth, True)
                intXpos = intLeftMarg + 40
                intYpos2 = intYpos + 24
                '- show details..
                If (decRefundedAsCash > 0) Then
                    s1 = RSet(FormatCurrency(decRefundedAsCash, 2), 12)
                    L1 = mIntPrintTextInRectangle(ev, "Refunded As Cash: ", intXpos, intYpos2, _
                                intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                    L1 = mIntPrintTextInRectangle(ev, s1, intXpos + intPayDescrWidth, intYpos2, _
                                                  intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                    intYpos2 += L1 + 6 '--add line height..
                End If '-cash-
                '- cr-note-
                If (decRefundedAsCreditNote > 0) Then
                    s1 = RSet(FormatCurrency(decRefundedAsCreditNote, 2), 12)
                    L1 = mIntPrintTextInRectangle(ev, "Refunded As CreditNote: ", intXpos, intYpos2, _
                                intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                    L1 = mIntPrintTextInRectangle(ev, s1, intXpos + intPayDescrWidth, intYpos2, _
                                                  intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                    intYpos2 += L1 + 6 '--add line height..
                End If '-cr-note-
                '-Eftpos.
                If (decRefundedAsEftPosDr > 0) Then
                    s1 = RSet(FormatCurrency(decRefundedAsEftPosDr, 2), 12)
                    L1 = mIntPrintTextInRectangle(ev, "Refunded As EftPos Dr: ", intXpos, intYpos2, _
                                intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                    L1 = mIntPrintTextInRectangle(ev, s1, intXpos + intPayDescrWidth, intYpos2, _
                                                  intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                    intYpos2 += L1 + 6 '--add line height..
                End If '-eftpos-
                If (decRefundedAsEftPosCr > 0) Then
                    s1 = RSet(FormatCurrency(decRefundedAsEftPosCr, 2), 12)
                    L1 = mIntPrintTextInRectangle(ev, "Refunded As EftPos Cr: ", intXpos, intYpos2, _
                                intPayDescrWidth, 16, font1, textColour.black, False) '-left Align.-
                    L1 = mIntPrintTextInRectangle(ev, s1, intXpos + intPayDescrWidth, intYpos2, _
                                                  intPayAmtWidth, 16, font1, textColour.black, True) '-Right Align.-
                    intYpos2 += L1 + 6 '--add line height..
                End If '- eftpos cr-
            End If '-refund-
            '--invoice totals.-
            '--invoice totals.-
            '--invoice totals.-
            intXpos = intLeftMarg
            intXpos2 = intXpos + mlPrtWidth - intAddressBoxWidth
            '== intYpos = intGridYpos + intGridYdepth + 24
            L1 = mlPrintTextInBox(ev, "<b>Invoice Totals:", _
                      intXpos2, intYpos, 16, intAddressBoxWidth, intTotalsBoxDepth, True)
            intYpos2 = intYpos + 24
            intXpos2 += 40
            L1 = mIntPrintTextInRectangle(ev, "Sub Total:", intXpos2, intYpos2, _
                                              140, 16, font1, textColour.black, False) '-left Align.-
            L1 = mIntPrintTextInRectangle(ev, FormatCurrency(rowInvoice.Item("subtotal_inc"), 2), intXpos2 + 120, intYpos2, _
                                               120, 16, font1, textColour.black, True) '-right Align.-
            intYpos2 += L1 + 4 '--add line height..
            '--Item GST-
            L1 = mIntPrintTextInRectangle(ev, "(Includes Tax:", intXpos2, intYpos2, _
                                              140, 16, font1, textColour.grey, False) '-left Align.-
            L1 = mIntPrintTextInRectangle(ev, FormatCurrency(decSubTotalTax, 2) & ")", intXpos2 + 120, intYpos2, _
                                               120, 16, font1, textColour.grey, True) '-right Align.-
            intYpos2 += L1 + 4 '--add line height..

            '-- Discount--
            L1 = mIntPrintTextInRectangle(ev, "Discount:", intXpos2, intYpos2, _
                                                140, 16, font1, textColour.black, False) '-left Align.-
            L1 = mIntPrintTextInRectangle(ev, FormatCurrency(decDiscount, 2), intXpos2 + 120, intYpos2, _
                                               120, 16, font1, textColour.black, True) '-right Align.-
            intYpos2 += L1 + 4 '--add line height..

            '-- Final Tax--
            L1 = mIntPrintTextInRectangle(ev, "Nett Tax:", intXpos2, intYpos2, _
                                                140, 16, font1, textColour.black, False) '-left Align.-
            L1 = mIntPrintTextInRectangle(ev, FormatCurrency(decTotalTax, 2), intXpos2 + 120, intYpos2, _
                                               120, 16, font1, textColour.black, True) '-right Align.-
            intYpos2 += L1 + 4 '--add line height..

            '-- Cashout..-
            'If Not mbIsQuote Then
            '    L1 = mIntPrintTextInRectangle(ev, "Cashout:", intXpos2, intYpos2, _
            '                                         140, 16, font1, textColour.black, False) '-left Align.-
            '    L1 = mIntPrintTextInRectangle(ev, FormatCurrency(rowInvoice.Item("cashout"), 2), intXpos2 + 120, intYpos2, _
            '                                       120, 16, font1, textColour.black, True) '-right Align.-
            'End If
            'intYpos2 += L1 + 4 '--add line height..

            '--Rounding..-
            L1 = mIntPrintTextInRectangle(ev, "Rounding:", intXpos2, intYpos2, _
                                                  140, 16, font1, textColour.black, False) '-left Align.-
            L1 = mIntPrintTextInRectangle(ev, FormatCurrency(rowInvoice.Item("rounding"), 2), intXpos2 + 120, intYpos2, _
                                               120, 16, font1, textColour.black, True) '-right Align.-
            intYpos2 += L1 + 4 '--add line height..

            '-TOTAL--
            font1.bBold = True
            L1 = mIntPrintTextInRectangle(ev, "TOTAL:", intXpos2, intYpos2, _
                                                  140, 16, font1, textColour.black, False) '-left Align.-
            L1 = mIntPrintTextInRectangle(ev, FormatCurrency(rowInvoice.Item("total_inc"), 2), intXpos2 + 120, intYpos2, _
                                               120, 16, font1, textColour.black, True) '-right Align.-
            intYpos2 += L1 + 4 '--add line height..

            '-- Terms -
            intXpos = intLeftMarg
            intYpos = intGridYpos + intGridYdepth + intTotalsBoxDepth + 32

            '-- terms- draw boxes.-
            '=3519.0221-  Terms only if on account-
            If bIsOnAccount Then
                L1 = mlPrintTextInBox(ev, "<b>Account Terms:" & vbCrLf & mStrTermsText, _
                                       intXpos, intYpos, 16, 400, intTermsBoxDepth, True)
            ElseIf mbIsRefund Then
                L1 = mlPrintTextInBox(ev, "<b>Thank You..", _
                                       intXpos, intYpos, 16, 400, intTermsBoxDepth, True)
            ElseIf mbIsQuote Then
                '==    -- 4201.0929/930/1002/1003.  03-Oct-2019-  Started 19-September-2019-
                '==        -- Quote (SalesInvoice) printing- Removed "Invoice Paid" text at foot...  
                L1 = mlPrintTextInBox(ev, "<b>Quotation Only.." & vbCrLf & "Thank You.", _
                        intXpos, intYpos, 16, 400, intTermsBoxDepth, True)
            Else '-paid Sale-
                L1 = mlPrintTextInBox(ev, "<b>Invoice Paid.." & vbCrLf & "Thank You.", _
                                       intXpos, intYpos, 16, 400, intTermsBoxDepth, True)
            End If

            '-- Print Bank details..-
            '--ONLY if is onAccount Sale --
            If bIsOnAccount Then
                intXpos = k_LEFTMARGIN + 450
                font1.bBold = True
                font1.bItalic = False
                '--bank name line..
                L1 = mlPrintTextString(ev, "Bank Name: ", intXpos, intYpos, font1)
                font1.bBold = False
                intYpos = mlPrintTextString(ev, msBankName, intXpos + 112, intYpos, font1)
                '--Account name line..
                font1.bBold = True
                L1 = mlPrintTextString(ev, "Account Name: ", intXpos, intYpos, font1)
                font1.bBold = False
                intYpos = mlPrintTextString(ev, msAccountName, intXpos + 112, intYpos, font1)
                '--Account No line..
                font1.bBold = True
                L1 = mlPrintTextString(ev, "BSB/Account No: ", intXpos, intYpos, font1)
                font1.bBold = False
                intYpos = mlPrintTextString(ev, msBSB1 & "-" & msBSB2 & " " & msAccountNo, _
                                                                            intXpos + 112, intYpos, font1)
            End If  '-on account-

            '-- Footer--
            Call mbPageFooter(ev, strPageNo)
            '-- end print --
            '--LAST page..-
            ev.HasMorePages = False
            Exit Function
        Catch ex As Exception
            MsgBox("Runtime error in invoice print Invoice Totals." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            ev.HasMorePages = False
            Exit Function
        End Try

    End Function  '-PrintSalesInvoice-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- PAGE EVENT support for Print Receipt (Docket)..--
    '--  PRINT Receipt-lines collection..--
    '-- Note: Markups eg: <big> -
    '--   occupy one collection item each, and apply to next line only.-

    Private Function mbPrintReceipt_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
        Dim sRem1, sRem2 As String
        Dim s1 As String
        Dim sChunk As String
        Dim ix, iPos, L1 As Integer
        Dim lngLeftMarg As Long
        Dim lngXpos, lngYpos As Integer
        Dim sLine As String
        Dim vline As Object
        Dim font1 As userFontDef

        mbPrintReceipt_PageEvent = False
        On Error GoTo PrintReceipt_error
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '== System.Windows.Forms.Application.DoEvents()
        lngLeftMarg = 16 '- 240 twips..-
        If Not (mImageUserLogo Is Nothing) Then
            ev.Graphics.DrawImage(mImageUserLogo, lngLeftMarg, 0)
        End If
        lngXpos = 3  '--45 twips..-
        lngYpos = 0
        With font1
            .sName = "Tahoma"
            .lngSize = 8
            .bBold = False
            .bUnderline = False
            .bItalic = False
        End With
        '--  Feed down past logo..--
        For ix = 1 To 10
            lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)
        Next ix
        System.Windows.Forms.Application.DoEvents()
        '--Printer.Font.Name = "Courier New"
        '-- Note: Markups eg: <big> occupy one collection item each..--
        '-- Note: Markups eg: <big> occupy one collection item each..--
        For Each vline In mColReportLines
            sRem1 = CStr(vline)
            If Left(sRem1, 6) = "<bold>" Then
                font1.bBold = True '== Printer.Font.Bold = True
            ElseIf Left(sRem1, 5) = "<big>" Then
                font1.lngSize = 11 '==  Printer.FontSize = 11
            ElseIf Left(sRem1, 12) = "<fontsize=7>" Then
                font1.lngSize = 7 '== Printer.FontSize = 7
            ElseIf Left(sRem1, 8) = "<tahoma>" Then
                font1.sName = "Tahoma" '==  Printer.Font.Name = "Tahoma"
            ElseIf Left(sRem1, 8) = "<lucida>" Then
                font1.sName = "Lucida Console" '== Printer.Font.Name = "Lucida Console"
                font1.lngSize = 8 '==   Printer.FontSize = 8
            ElseIf Left(sRem1, 4) = "<ul>" Then
                font1.bUnderline = True '== Printer.Font.Underline = True
            ElseIf sRem1 = "" Then  '--new line..-
                '== Printer.Print ""
                lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)
            Else '--data line..-
                '-- Outer Loop-- break at LF's if any..
                While (Len(sRem1) > 0)
                    iPos = InStr(sRem1, vbCrLf) '--break at LF's if any..
                    If iPos > 0 Then
                        sRem2 = Left(sRem1, iPos - 1)
                        sRem1 = Mid(sRem1, iPos + 2)
                    Else '--no cr-
                        sRem2 = sRem1
                        sRem1 = "" '--last outer line-
                    End If '--cr/lf-
                    '--Inner loop..-  break rem2 Line into 40 char lines--
                    '--max 40 chars per line..-
                    '-- Make line chunks for longer lines..-
                    While (Len(sRem2) > 0)
                        If (Len(sRem2) > 43) Then '--break line close to 40..-
                            '--find last space or punctuation..-
                            sChunk = Left(sRem2, 43) '--try max..-
                            If (Right(sChunk, 1) <> " ") And (Right(sChunk, 1) <> ";") Then '--find break--
                                iPos = InStrRev(sChunk, " ")
                                If iPos < 20 Then iPos = InStrRev(sChunk, ";") '--don't break under half..-
                                If (iPos >= 20) Then '--break
                                    sChunk = Left(sRem2, iPos)
                                End If
                            End If '--break..-
                            s1 = sChunk
                            sRem2 = Mid(sRem2, Len(sChunk) + 1)
                        Else
                            s1 = sRem2 : sRem2 = "" '--last chunk-
                        End If '--break..-
                        '--print current chunk..-
                        lngYpos = mlPrintTextString(ev, Replace(s1, "^", " "), lngXpos, lngYpos, font1)
                    End While '--rem2-
                End While '--sRem1..-
                '-- reset to defaolut font style etc.
                font1.bBold = False  '--reset to normal..-
                font1.bUnderline = False
                font1.sName = "Tahoma" '=='restore "Tahoma"
                font1.lngSize = 8 '==   = 8
            End If '--bold--
        Next vline '--line--
        font1.sName = "Tahoma"
        font1.lngSize = 7
        lngYpos = mlPrintTextString(ev, msVersionPOS, lngXpos, lngYpos, font1)

        '--  push receipt up..-
        For ix = 1 To 24
            lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)
        Next ix

        lngYpos = mlPrintTextString(ev, "...", lngXpos, lngYpos, font1)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        System.Windows.Forms.Application.DoEvents()

        mbPrintReceipt_PageEvent = True

        '--only one page..-
        ev.HasMorePages = False
        Exit Function

PrintReceipt_error:
        L1 = Err().Number
        MsgBox("Runtime Error in Print-Receipt.." & vbCrLf & _
                                       "Error=" & L1 & ": " & ErrorToString(L1), MsgBoxStyle.Critical)
    End Function '--print receipt..-
    '= = = = = = =  = = = =  = = =
    '-===FF->

    '-- E V E N T  H A N D L E R S functions  
    '---        for  P u b l i c  M e t h o d s..--

    '-- PAGE EVENT for Print STOCK Labels..--

    Private Function mbPrintStockLabels_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
        Dim lngTop, lngLHS As Integer
        Dim lngXpos, lngYpos As Integer
        Dim font1 As userFontDef
        Dim s1, sBarcode As String
        Dim sDescription, sPrice As String
        Dim colLabelx As Collection

        '==intNoLabels = mIntNoLabels

        '- Get first/next label-
        colLabelx = mColStockLabels.Item(miPageNo + 1)
        sBarcode = colLabelx.Item("barcode")
        sDescription = colLabelx.Item("description")
        sPrice = FormatCurrency(CDec(colLabelx.Item("price")), 2)

        lngLHS = (12 * PRT_UNIT) '== 180 twips-    '== 60
        lngTop = (23 * PRT_UNIT) '== 345 twips-    '==60
        font1.bUnderline = False

        '--print Label with Stock barcode--

        lngXpos = lngLHS
        lngYpos = lngTop
        font1.sName = "Verdana"

        If (msItemBarcodeFontName <> "") Then '--have barcode..-
            font1.bBold = False
            lngXpos = lngLHS
            font1.sName = msItemBarcodeFontName
            font1.lngSize = mIntItemBarcodeFontSize
            lngYpos = mlPrintTextString(ev, "*" & sBarcode & "*", lngXpos, lngYpos, font1)

            '--next line
            font1.sName = "Verdana"
        Else  '- no barcode font-
            font1.sName = "Verdana"
            font1.lngSize = 11
            font1.bBold = True
            lngYpos = mlPrintTextString(ev, sBarcode, lngXpos, lngYpos, font1)
        End If
        font1.bBold = False
        font1.lngSize = 7
        lngXpos = lngLHS
        lngYpos = mlPrintTextString(ev, sDescription, lngXpos, lngYpos, font1)

        '-price-
        lngXpos = lngLHS + 120
        font1.sName = "Verdana"
        font1.lngSize = 10
        font1.bBold = True
        lngYpos = mlPrintTextString(ev, sPrice, lngXpos, lngYpos, font1)

        '-- Business Name-
        If (msBusinessName <> "") Then
            lngXpos = lngLHS
            font1.sName = "Verdana"
            font1.lngSize = 7
            font1.bBold = False
            lngYpos = mlPrintTextString(ev, msBusinessName, lngXpos, lngYpos, font1)
        End If  '-business.

        miPageNo += 1

        '-- Check to see if more pages are to be printed.
        ev.HasMorePages = (miPageNo < mColStockLabels.Count)

    End Function '==PrintStockabels=
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '=3403.513=  layby Labels
    Private mLngLaybyId As Integer = -1
    Private mIntNoLaybyLabels As Integer = 1
    Private msLaybyCustomer As String = ""
    Private msLaybyInfo As String = ""
    '-- PAGE EVENT for Print Job Labels..--

    Private Function mbPrintLaybyLabels_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
        Dim lngTop, lngLHS As Integer
        Dim lngXpos, lngYpos As Integer
        Dim font1 As userFontDef
        Dim lngLaybyId As Integer
        Dim sCustomer, s1 As String

        '--  get calling values..--
        lngLaybyId = mLngLaybyId
        '==intNoLabels = mIntNoLabels
        sCustomer = msLaybyCustomer

        lngLHS = (12 * PRT_UNIT) '== 180 twips-    '== 60
        lngTop = (23 * PRT_UNIT) '== 345 twips-    '==60
        font1.bUnderline = False

        '--  Must print this time..
        '== For ix = 1 To intNoLabels
        '--print Label with Job no..-
        lngXpos = lngLHS '== Printer.CurrentX = lngLHS
        lngYpos = lngTop '== Printer.CurrentY = lngTop
        font1.sName = "Lucida Sans" '== printer.FontName = "Verdana"
        font1.lngSize = 16 '==  Printer.FontSize = 14
        font1.bBold = True '==  Printer.FontBold = True
        '== Printer.Print " JobNo: "
        Dim usercolor1 As textColour = textColour.FireBrick
        s1 = " Layby: " & VB6.Format(lngLaybyId, "  000")
        lngYpos = mlPrintTextString(ev, s1, lngXpos, lngYpos, font1, usercolor1)
        's1 = VB6.Format(lngLaybyId, " 000")
        'lngXpos = lngLHS '== Printer.CurrentX = lngLHS
        'lngYpos = mlPrintTextString(ev, s1, lngXpos, lngYpos, font1, usercolor1)

        'If (msItemBarcodeFontName <> "") Then '--have barcode..-
        '    font1.bBold = False '== Printer.FontBold = False
        '    lngXpos = lngLHS + mlGetTextWidth(ev, s1, font1) + 16  '= 60 twips.

        '    lngYpos = lngTop '== Printer.CurrentY = lngTop
        '    font1.sName = msItemBarcodeFontName '== Printer.FontName = msItemBarcodeFontName
        '    font1.lngSize = mlItemBarcodeFontSize '= Printer.FontSize = mlItemBarcodeFontSize
        '    lngYpos = mlPrintTextString(ev, "*" & lngJobId & "*", lngXpos, lngYpos, font1)
        '    '--next line
        '    font1.sName = "Lucida Sans" '== Printer.FontName = "Verdana"
        'End If
        font1.bBold = True
        font1.lngSize = 8
        lngXpos = lngLHS '== Printer.CurrentX = lngLHS
        lngYpos = mlPrintTextString(ev, sCustomer, lngXpos, lngYpos, font1)
        font1.lngSize = 7
        lngYpos = mlPrintTextString(ev, msLaybyInfo, lngXpos, lngYpos, font1)

        miPageNo += 1

        '-- Check to see if more pages are to be printed.
        ev.HasMorePages = (miPageNo < mIntNoLaybyLabels)

    End Function '==PrintLaybyLabels=
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Print the Statement -
    '-- Print the Statement -
    '==    -- 4201.0618/0623.  11/18/20-June-2019-   
    '==         --  Debtors Report and Statements. 
    '==               Show Credit-Note Available balance...


    Public Function mbPrintStatement_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean

        '= mlPrtWidth = 760
        Const k_WIDTH_DATE = 104      '--width of date column.-
        Const k_WIDTH_REFNO = 132  '--width of InvoiceNo column.-
        Const k_WIDTH_DESCR = 220  '--width of Description column.-
        Const k_WIDTH_TAX = 80     '--width of TAX Amt column.-
        Const k_WIDTH_BALANCE = 108   '--width of balance column.-
        Const k_WIDTH_DUE = 116      '--width of DUE date column.-
        '==Const k_WIDTH_TOTAL = 100  '--width of Ext.Total column.-

        Dim intGreyBGColour As Integer = &HE0E0E0&
        Dim fillColor As Color
        Dim intXpos, intXpos2, intYpos, intYpos2 As Integer
        Dim intHdrX As Integer = 150
        Dim intHdrY As Integer = 24
        Dim intLeftMarg, x1 As Integer
        Dim intXposDescr, intXposTax, intXposBal As Integer
        Dim L1, ix, intError As Integer
        '== Dim rowInvoice, rowDetail, row1 As DataRow
        Dim s1, sInvoiceNo, sDocType As String
        Dim sJobNo As String = "--"
        Dim dateInvoice As DateTime
        Dim sCustomerBarcode, sCustomerInfo As String
        Dim intCreditDays As Integer
        Dim sABN, sPageNo As String
        Dim sChangeGiven, sNettRecvd As String
        Dim bIsAccountCust As Boolean = False
        Dim decExtension As Decimal
        Dim decTotalTax As Decimal = 0
        '= Dim decDiscount, decRounding As Decimal
        '= Dim decSubTotalTax, decDiscTax As Decimal

        Dim decPayAmount, decTotalPayments, decAmountDebitedToAccount As Decimal
        Dim font1 As userFontDef
        Dim intInfoBoxWidth As Integer = 88  '= 120
        Dim intInfoBoxDepth As Integer = 27
        Dim intAddressBoxWidth As Integer = 340
        Dim intAddressBoxDepth As Integer = 120
        Dim intTermsBoxDepth As Integer = 100
        '-grid-
        Dim intGridYpos, intGridYdepth As Integer
        Dim penGrid As Pen = Pens.LightGray
        '=4201.0618=
        '= Dim sCustPhone, sCustMobile As String
        Dim decCustCreditNoteBalance As Decimal

        Call mbInitialiseBusiness()
        '-- Format ABN for printing..-
        sABN = VB.Left(msBusinessABN, 2) & " " & Mid(msBusinessABN, 3, 3) & _
                  " " & Mid(msBusinessABN, 6, 3) & " " & Mid(msBusinessABN, 9, 3)

        fillColor = ColorTranslator.FromOle(intGreyBGColour)

        sCustomerBarcode = mColCustomerInfo.Item("barcode")
        sCustomerInfo = mColCustomerInfo.Item("firstname") & " " & mColCustomerInfo.Item("lastname") & vbCrLf & _
                         mColCustomerInfo.Item("companyname") & vbCrLf & mColCustomerInfo.Item("address") & _
                        vbCrLf & mColCustomerInfo.Item("suburb") & " " & _
                         mColCustomerInfo.Item("state") & " " & mColCustomerInfo.Item("postcode") & _
                        vbCrLf & mColCustomerInfo.Item("country")
        '=4201.0618=
        decCustCreditNoteBalance = mColCustomerInfo.Item("CreditNoteBalance")

        On Error GoTo PrintStatement_error

        intCreditDays = CInt(mColCustomerInfo.Item("creditDays"))
        font1.sName = "Lucida Sans"     '== "Tahoma" '== Printer.FontName = "Tahoma"
        font1.lngSize = 8 '==Printer.FontSize = 18
        font1.bBold = False '== Printer.Font.Bold = False '--reset to normal IN CASE LEFT OVER..-
        font1.bUnderline = False '= Printer.Font.Underline = False
        font1.bItalic = False

        intLeftMarg = k_LEFTMARGIN '== iixels-  (16 * PRT_UNIT) '- 240 twips..-
        intXpos = intLeftMarg
        intHdrX = intLeftMarg
        intYpos = 24  '--top pos.--
        intYpos2 = intYpos

        '-- Main Hdr stuff is on First page only..
        If (miPageNo <= 0) Then
            '--  paint BIZ logo  top left.--
            If Not (mImageUserLogo Is Nothing) Then
                x1 = mImageUserLogo.Width
                ev.Graphics.DrawImage(mImageUserLogo, intLeftMarg + mlPrtWidth - x1, 0)
            End If
            '-- print biz name (CENTERED-
            font1.lngSize = 18 '==Printer.FontSize = 18

            '== intYpos = mlPrintTextString(ev, msBusinessShortName, -1, intHdrY, font1)
            font1.bBold = True '== Printer.FontBold = True
            intYpos = mlPrintTextString(ev, msBusinessName, -1, intYpos, font1)
            font1.lngSize = 8 '== Printer.FontSize = 8
            intYpos = mlPrintTextString(ev, "ABN: " & sABN, -1, intYpos, font1)
            intYpos = mlPrintTextString(ev, msBusinessAddress1, -1, intYpos, font1)
            intYpos = mlPrintTextString(ev, msBusinessAddress2, -1, intYpos, font1)
            intYpos = mlPrintTextString(ev, "Phone:  " & msBusinessPhone, -1, intYpos, font1)
            '--blank line-
            intYpos = mlPrintTextString(ev, "", intHdrX, intYpos, font1)
            font1.lngSize = 18 '==Printer.FontSize = 18
            L1 = mlPrintTextString(ev, "Statement", -1, intYpos, font1, textColour.black)
            '- "== Printed: " & Format(Now, "dd-MMM-yyyy hh:mm")
            font1.lngSize = 8
            font1.bBold = False

            '- As At DATE should be INPUT parameter..
            x1 = intLeftMarg + (mlPrtWidth \ 2) + 120   '-- Date to right of "statement"..
            intYpos = mlPrintTextString(ev, "As at: " & Format(mDateCutOff, "dd-MMM-yyyy"), x1, intYpos + 6, font1, textColour.black)
            '-- Blank line..
            intYpos = mlPrintTextString(ev, "", intHdrX, intYpos, font1)

            '-- Print Customer info..
            intYpos2 = intYpos  '-- set Ypos for Cust...

            '-- Customer and Info...
            mDefaultUserFont.lngSize = 9
            L1 = mlPrintTextString(ev, "To:", intXpos + 60, intYpos + 59, font1)
            L1 = mlPrintTextInBox(ev, sCustomerInfo, _
                              intXpos, intYpos2 + 44, 85, intAddressBoxWidth, intAddressBoxDepth, True)
            s1 = "Customer Account ID: " & sCustomerBarcode & vbCrLf & vbCrLf & _
                   "Credit Limit:    " & FormatCurrency(mColCustomerInfo.Item("creditLimit"), 2) & vbCrLf & _
                   "Payment Terms:    " & mColCustomerInfo.Item("creditDays") & " days from Invoice Date."
            L1 = mlPrintTextInBox(ev, s1, _
                              intXpos + mlPrtWidth - intAddressBoxWidth, intYpos2 + 44, 6, _
                                                                    intAddressBoxWidth, intAddressBoxDepth, True)
            '-restore-
            mDefaultUserFont.lngSize = 8

            '-- Clear all totals this customer run..-
            mIntPagesPrintedCount = 0
            mDecStatementTotalTax = 0
            mDecStatementTotalDebits = 0
            '= mDecStatementTotalDebitsNonTaxable = 0
            mDecStatementTotalCredits = 0

            mDecStatementTotalOutstanding = 0

            mDecStatementAgedTotalCurrent = 0
            mDecStatementAgedTotal30 = 0
            mDecStatementAgedTotal60 = 0
            mDecStatementAgedTotal90 = 0

            '-compute total page count.--
            mIntTotalPageCount = mColStatementDetailPages.Count
            If mbStatementSummaryOnNewPage Then
                mIntTotalPageCount += 1
            End If
        End If  '--first time-

        '--Page-1 Hdr done.--
        miPageNo += 1

        '-- All pages-
        '-- All pages-
        Const k_HDR_HEIGHT = 26
        '-- GRID for Detail Lines..--
        intGridYpos = intYpos2 + 44 + intAddressBoxDepth + 12
        intGridYdepth = k_HDR_HEIGHT   '= 400
        intXpos = intLeftMarg

        '-print Page no..-
        '-- For 2nd and subseq. pages, print Account Id also..
        If (miPageNo > 1) Then
            '-- and start grid at the top..-
            intYpos = 24  '--top pos.--
            intYpos2 = intYpos
            font1.lngSize = 8
            font1.bBold = True '== Printer.FontBold = True
            intYpos = mlPrintTextString(ev, msBusinessName, -1, intYpos, font1)
            intYpos = mlPrintTextString(ev, "Statement", -1, intYpos, font1)
            intGridYpos = intYpos + 16
            L1 = mIntPrintTextInRectangle(ev, "Cust. Account ID: " & sCustomerBarcode, _
                                           intLeftMarg, intGridYpos, _
                                              k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False, False) '-right Align.-
        End If '->1-
        sPageNo = "Page: " & CStr(miPageNo) & "of " & CStr(mIntTotalPageCount)
        L1 = mIntPrintTextInRectangle(ev, sPageNo, _
                                      intLeftMarg + mlPrtWidth - k_WIDTH_DUE, intGridYpos, _
                                         k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, True, False, False) '-right Align.-
        '--draw the "grid"..
        intGridYpos += k_HDR_HEIGHT
        Call mlDrawLine(ev, intXpos, intGridYpos, mlPrtWidth)  '--top bar-
        '--column lines- 8 spaces, nine lines-
        Dim arrayIntWidths() As Integer = _
                      {k_WIDTH_DATE, k_WIDTH_REFNO, k_WIDTH_DESCR, k_WIDTH_TAX, k_WIDTH_BALANCE, k_WIDTH_DUE}
        For ix = 0 To UBound(arrayIntWidths)
            ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)
            intXpos += arrayIntWidths(ix)
        Next ix
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
        Call mIntPrintTextInRectangle(ev, " Date", intXpos, intGridYpos, _
                                                 k_WIDTH_DATE, k_HDR_HEIGHT, font1, textColour.black, False, True)
        '== e.Graphics.DrawString("Bar Code", drawFont, drawBrush, drawRect, drawFormat)
        intXpos += k_WIDTH_DATE
        Call mIntPrintTextInRectangle(ev, " Invoice/Paymt.No.", intXpos, intGridYpos, _
                                                 k_WIDTH_REFNO, k_HDR_HEIGHT, font1, textColour.black, False, True)
        intXpos += k_WIDTH_REFNO
        Call mIntPrintTextInRectangle(ev, " Description", intXpos, intGridYpos, _
                                                  k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False, True)
        intXposDescr = intXpos  '--save position-
        intXpos += k_WIDTH_DESCR
        intXposTax = intXpos  '-save-
        Call mIntPrintTextInRectangle(ev, "Tax Amt", intXpos, intGridYpos, _
                                        k_WIDTH_TAX, k_HDR_HEIGHT, font1, textColour.black, False, True) '-NO Right Align.-
        intXpos += k_WIDTH_TAX
        intXposBal = intXpos  '-save-
        Call mIntPrintTextInRectangle(ev, "Balance $", intXpos, intGridYpos, _
                                       k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, False, True, True) '-CENTRE Align.-
        intXpos += k_WIDTH_BALANCE
        L1 = mIntPrintTextInRectangle(ev, "Due", intXpos, intGridYpos, _
                                         k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, False, True, True) '-CENTRE Align.-
        '= intXpos += k_WIDTH_DUE
        '-- L1 has line height.
        intXpos = intLeftMarg
        intYpos = intGridYpos + k_HDR_HEIGHT  '--jump col hdr row.
        font1.bBold = False
        '--  Print next page of Invoice/payment Data..
        Dim colDetailPage As Collection
        Dim colInvoice As Collection
        Dim colPayments As Collection
        Dim decInvoiceAmt, decTaxAmt As Decimal
        Dim sInvoiceId, sInvoiceDate, sTranType, sList As String
        Dim sInvoiceTotal, sTax, sInvoiceDueDate As String
        Dim intDaysAged As Integer
        Dim decTotalRefundsAvail As Decimal
        '--  Print next page of Invoice/payment Data..
        If (mIntPagesPrintedCount < mColStatementDetailPages.Count) Then '-more invoices to do-

            mIntPagesPrintedCount += 1
            colDetailPage = mColStatementDetailPages.Item(mIntPagesPrintedCount)
            For Each colInvoice In colDetailPage
                Dim decTotalCredits As Decimal = 0
                Dim decOutstanding As Decimal = 0

                colPayments = colInvoice.Item("invoicePayments")
                sInvoiceDate = Format(colInvoice.Item("invoice_date"), "dd-MMM-yyyy")
                sInvoiceId = RSet(CStr(colInvoice.Item("invoice_id")), 7)
                sTranType = colInvoice.Item("transactionType")
                If (LCase(sTranType) = "sale") Then
                    sTranType = "Invoice (Sale)"
                End If
                decInvoiceAmt = CDec(colInvoice.Item("invoiceTotal"))
                sInvoiceTotal = FormatCurrency(decInvoiceAmt, 2)
                decTaxAmt = CDec(colInvoice.Item("total_tax"))
                sTax = FormatCurrency(decTaxAmt, 2)

                '==  3303.0114- 
                '-- No refunds in the invoices col. any more-
                '= If (LCase(sTranType) = "refund") Then
                '=     decTaxAmt = -decTaxAmt
                '=     decInvoiceAmt = -decInvoiceAmt
                '= End If

                '-- add to summary totals..-
                mDecStatementTotalDebits += decInvoiceAmt
                mDecStatementTotalTax += decTaxAmt

                intXpos = intLeftMarg
                '-- print invoice line -
                intYpos += 3   '-- down a bit from prev. line..
                Call mIntPrintTextInRectangle(ev, sInvoiceDate, intXpos, intYpos, _
                                                           k_WIDTH_DATE, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                intXpos += k_WIDTH_DATE
                Call mIntPrintTextInRectangle(ev, sInvoiceId, intXpos, intYpos, _
                                                            k_WIDTH_REFNO, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                intXpos += k_WIDTH_REFNO
                Call mIntPrintTextInRectangle(ev, sTranType, intXpos, intYpos, _
                                                            k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                intXpos += k_WIDTH_DESCR
                '-tax amount-
                L1 = mIntPrintTextInRectangle(ev, sTax, intXpos, intYpos, _
                                                    k_WIDTH_TAX, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                intXpos += k_WIDTH_TAX
                L1 = mIntPrintTextInRectangle(ev, sInvoiceTotal, intXpos, intYpos, _
                                                   k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                intXpos += k_WIDTH_BALANCE
                '-- Calculate and print DUE date..
                sInvoiceDueDate = Format(DateAdd(DateInterval.Day, intCreditDays, CDate(colInvoice.Item("invoice_date"))), "dd-MMM-yyyy")
                '- PRINTED BELOW..
                '=Call mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                '=                                  k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, False, False, True) '-no box-
                intYpos += L1
                '--print payments this invoice-
                '==  v.3403.1107- 02-Nov-2017-=Statements-..-
                '==     >> Show Payment Reversals..-.
                If (colPayments.Count > 0) Then
                    Dim decPaymentAmt As Decimal
                    Dim sPaymentDate As String
                    Dim sSource, sDisbTrancode, sPaymentId As String
                    Dim bIsReversal As Boolean
                    For Each col1 As Collection In colPayments
                        intXpos = intLeftMarg
                        sPaymentDate = Format(col1.Item("date"), "dd-MMM-yyyy")
                        decPaymentAmt = CDec(col1.Item("amount"))
                        sDisbTrancode = col1.Item("disbTrancode")
                        bIsReversal = CBool(col1.Item("isReversal"))
                        sPaymentId = RSet(CStr(col1.Item("payment_id")), 7)

                        sSource = sDisbTrancode & "- Credit"  '= col1.Item("sourceOfFunds")
                        If bIsReversal Then
                            sSource = sDisbTrancode & "- Reversal"  '= col1.Item("sourceOfFunds")
                            decTotalCredits -= decPaymentAmt
                        Else  '-normal credit-
                            decTotalCredits += decPaymentAmt
                        End If
                        'If (LCase(sTranType) = "refund") Then
                        '    decPaymentAmt = -decPaymentAmt
                        'End If
                        '-- print PAYMENT line -
                        Call mIntPrintTextInRectangle(ev, sPaymentDate, intXpos, intYpos, _
                                                                   k_WIDTH_DATE, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                        intXpos += k_WIDTH_DATE
                        Call mIntPrintTextInRectangle(ev, sPaymentId, intXpos, intYpos, _
                                                                    k_WIDTH_REFNO, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                        intXpos += k_WIDTH_REFNO
                        Call mIntPrintTextInRectangle(ev, sSource, intXpos, intYpos, _
                                                                    k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                        intXpos += k_WIDTH_DESCR
                        '-tax amount-
                        '==L1 = mIntPrintTextInRectangle(ev, sTax, intXpos, intYpos, _
                        '==                                     k_WIDTH_TAX, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        intXpos += k_WIDTH_TAX
                        s1 = FormatCurrency(decPaymentAmt, 2)
                        '-- add minus sign.-
                        If Not bIsReversal Then
                            s1 = "-" & s1
                        End If
                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
                                                           k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                        intXpos += k_WIDTH_BALANCE
                        '-- add minus sign.-
                        '= see above= Call mlPrintTextString(ev, "-", intXpos, intYpos + (k_HDR_HEIGHT \ 2), font1)

                        intYpos += L1
                    Next col1  '-next payment-
                    mDecStatementTotalCredits += decTotalCredits
                End If  '--payments-
                '-- print Total credits.-
                '-- Blank line..
                intYpos = mlPrintTextString(ev, "", intHdrX, intYpos, font1)
                Call mIntPrintTextInRectangle(ev, "Total Payments:", intXposDescr, intYpos, _
                                                             k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                s1 = FormatCurrency(decTotalCredits, 2)
                If (decTotalCredits > 0) Then  '- real credit.. needs - sign
                    s1 = "-" & s1
                End If
                L1 = mIntPrintTextInRectangle(ev, s1, intXposBal, intYpos, _
                                                   k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                '-- add minus sign.-
                '= see above= 
                '=Call mlPrintTextString(ev, "-", intXposBal + k_WIDTH_BALANCE, intYpos + (k_HDR_HEIGHT \ 2), font1)
                intYpos += (L1 + (L1 \ 2))

                '-print Invoice balance.-
                '-- 1st print short line-
                decOutstanding = decInvoiceAmt - decTotalCredits
                mDecStatementTotalOutstanding += decOutstanding
                font1.bBold = True
                Call mlDrawLine(ev, intXposTax, intYpos, k_WIDTH_TAX + k_WIDTH_BALANCE)
                Call mIntPrintTextInRectangle(ev, "Balance:", intXposTax, intYpos, _
                                                               k_WIDTH_TAX, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                s1 = FormatCurrency(decOutstanding, 2)
                L1 = mIntPrintTextInRectangle(ev, s1, intXposBal, intYpos, _
                                                   k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                '=3403.1107- Due date HERE now..
                Call mIntPrintTextInRectangle(ev, sInvoiceDueDate, intXpos, intYpos, _
                                                  k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, False, False, True) '-no box-
                intYpos += (L1 + (L1 \ 2))
                '-- end of this invoice-
                font1.bBold = False
                '== Call mlDrawLine(ev, k_LEFTMARGIN, intYpos + 3, mlPrtWidth - k_WIDTH_DUE)
                Call mlDrawLine(ev, k_LEFTMARGIN, intYpos + 3, mlPrtWidth)
                '-- Age the outstanding..
                intDaysAged = DateDiff(DateInterval.Day, colInvoice.Item("invoice_date"), Today)
                If (intDaysAged <= 30) Then
                    mDecStatementAgedTotalCurrent += decOutstanding
                ElseIf (intDaysAged <= 60) Then
                    mDecStatementAgedTotal30 += decOutstanding
                ElseIf (intDaysAged <= 90) Then
                    mDecStatementAgedTotal60 += decOutstanding
                Else  '--90 and over-
                    mDecStatementAgedTotal90 += decOutstanding
                End If
            Next colInvoice
        End If  '-mIntPagesPrintedCount-

        '-- If we just printed last page of invoices.
        '--  If room for Summary, then print summary.-
        '-mbStatementSummaryOnNewPage-
        If (mIntPagesPrintedCount < mColStatementDetailPages.Count) Then '-more invoices to do-
            Call mbPageFooter(ev, sPageNo)
            ev.HasMorePages = True
        Else  '--all invoices printed-
            If mbStatementSummaryOnNewPage AndAlso (miPageNo < mIntTotalPageCount) Then
                '-- throw page and come back for totals
                Call mbPageFooter(ev, sPageNo)
                ev.HasMorePages = True
            Else  '--print totals on last invoice page.-
                '==  3303.0114- 14Jan2017=
                '-- FIRST print total Avail. refunds if any.-
                '=4201.0618- NO more Refunds on Debtors..
                '--  Now using CreditNotes..
                '--    decTotalRefundsAvail = 0

                'decTotalRefundsAvail = 0
                'sList = " in Refunds not yet credited- (Refunds Nos: "
                ''= sList = "Not yet credited- (Refunds not allocated- Nos: "
                'If (mColRefunds.Count > 0) Then
                '    For Each colRefund As Collection In mColRefunds
                '        decTotalRefundsAvail += CDec(colRefund.Item("AmountAvailable"))
                '        sList &= CStr(colRefund.Item("invoice_id")) & "; "
                '    Next colRefund
                If (decCustCreditNoteBalance > 0) Then  '= (decTotalRefundsAvail > 0) Then
                    '-- reduce outstanding..
                    '- not now-mDecStatementTotalOutstanding -= decTotalRefundsAvail
                    '= adjust aged bals..
                    '= not now-
                    '-print credit refunds line-
                    intYpos += k_HDR_HEIGHT
                    '=  s1 = "NB: " & FormatCurrency(decTotalRefundsAvail, 2) & " " & sList & ")"
                    s1 = "-- NB: You have a " & FormatCurrency(decCustCreditNoteBalance, 2) & " Credit-Note balance (not yet applied).."
                    font1.bBold = True
                    intYpos = mlPrintTextString(ev, s1, intHdrX + 100, intYpos, font1, textColour.Green)
                    Call mlDrawLine(ev, k_LEFTMARGIN, intYpos + 3, mlPrtWidth - k_WIDTH_DUE)
                    font1.bBold = False
                End If '-avail-
                'End If  '- REFUND count-

                Dim intAgeWidth = 80
                intYpos = k_STATEMENT_SUMMARY_TOP
                L1 = mlPrintTextInBox(ev, "<b>Aged Balances:", _
                        k_LEFTMARGIN, intYpos, 16, intAddressBoxWidth, intAddressBoxDepth + 48, True)

                '-- SUMMARY of aged info--
                intXpos = k_LEFTMARGIN + 16
                intYpos += 18
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
                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecStatementAgedTotalCurrent, 2), intXpos, intYpos, _
                                                intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecStatementAgedTotal30, 2), intXpos + intAgeWidth, intYpos, _
                                                 intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecStatementAgedTotal60, 2), intXpos + (intAgeWidth * 2), intYpos, _
                                                 intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecStatementAgedTotal90, 2), intXpos + (intAgeWidth * 3), intYpos, _
                                                 intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                intYpos += k_HDR_HEIGHT + 8

                '- print total outstanding.-

                font1.bBold = True
                intYpos = mlPrintTextString(ev, "Total Outstanding: " & _
                                                  FormatCurrency(mDecStatementTotalOutstanding, 2), k_LEFTMARGIN + 16, intYpos, font1)
                intYpos += 6
                font1.bBold = False
                font1.bItalic = True
                intYpos = mlPrintTextString(ev, "Thank You. Please remit any outstanding to:", k_LEFTMARGIN + 16, intYpos, font1)
                '-- Print Bank details..-
                intXpos = k_LEFTMARGIN + 16
                font1.bBold = True
                font1.bItalic = False
                '--bank name line..
                L1 = mlPrintTextString(ev, "Bank Name: ", intXpos, intYpos, font1)
                font1.bBold = False
                intYpos = mlPrintTextString(ev, msBankName, intXpos + 112, intYpos, font1)
                '--Account name line..
                font1.bBold = True
                L1 = mlPrintTextString(ev, "Account Name: ", intXpos, intYpos, font1)
                font1.bBold = False
                intYpos = mlPrintTextString(ev, msAccountName, intXpos + 112, intYpos, font1)
                '--Account No line..
                font1.bBold = True
                L1 = mlPrintTextString(ev, "BSB/Account No: ", intXpos, intYpos, font1)
                font1.bBold = False
                intYpos = mlPrintTextString(ev, msBSB1 & "-" & msBSB2 & " " & msAccountNo, _
                                                                            intXpos + 112, intYpos, font1)

                '-print Customer barcode as ID..-
                intYpos = mlPrintTextString(ev, "", intXpos, intYpos, font1)
                font1.bBold = True
                L1 = mlPrintTextString(ev, "Your AccountID: ", intXpos, intYpos, font1)
                font1.bBold = False
                intYpos = mlPrintTextString(ev, sCustomerBarcode, intXpos + 112, intYpos, font1)

                '--RHS invoice totals-
                '--RHS invoice totals-
                intYpos = k_STATEMENT_SUMMARY_TOP
                intXpos = k_LEFTMARGIN
                intXpos2 = intXpos + mlPrtWidth - intAddressBoxWidth
                '== intYpos = intGridYpos + intGridYdepth + 24
                L1 = mlPrintTextInBox(ev, "<b>Invoice Totals:", _
                          intXpos2, intYpos, 16, intAddressBoxWidth, intAddressBoxDepth + 48, True)
                intYpos += 18
                font1.bBold = True
                font1.bItalic = False
                '- Private mDecStatementTotalTax As Decimal
                '- Private mDecStatementTotalDebits As Decimal
                '- Private mDecStatementTotalCredits As Decimal
                Dim intTotalsCaptionWidth As Integer = 120
                Dim intTotalsValueWidth As Integer = 160
                Dim intXposTotal = intXpos2 + 28
                '--Tax line..
                s1 = FormatCurrency(mDecStatementTotalTax, 2)
                L1 = mIntPrintTextInRectangle(ev, "Tax Amount: ", intXposTotal, intYpos, _
                                     intTotalsCaptionWidth, k_HDR_HEIGHT, font1, textColour.black, False) '-L/Align, no box-
                font1.bBold = False
                L1 = mIntPrintTextInRectangle(ev, s1, intXposTotal + intTotalsCaptionWidth, intYpos, _
                                                  intTotalsValueWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                intYpos += L1
                font1.bBold = True
                '--Totals line..
                s1 = FormatCurrency(mDecStatementTotalDebits, 2)
                L1 = mIntPrintTextInRectangle(ev, "Sub-total Inc Tax: ", intXposTotal, intYpos, _
                                     intTotalsCaptionWidth, k_HDR_HEIGHT, font1, textColour.black, False) '-L/Align, no box-
                font1.bBold = False
                L1 = mIntPrintTextInRectangle(ev, s1, intXposTotal + intTotalsCaptionWidth, intYpos, _
                                                   intTotalsValueWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                intYpos += L1
                '-- blank line.-
                intYpos = mlPrintTextString(ev, "", intXposTotal, intYpos, font1)
                '--Credits line..
                font1.bBold = True
                s1 = FormatCurrency(mDecStatementTotalCredits, 2)
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
                Dim decOverallBalance As Decimal = mDecStatementTotalDebits - mDecStatementTotalCredits
                Call mlDrawLine(ev, intXposTotal, intYpos, intTotalsCaptionWidth + intTotalsValueWidth)
                Call mIntPrintTextInRectangle(ev, "Balance:", intXposTotal, intYpos, _
                                                              intTotalsCaptionWidth, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
                s1 = FormatCurrency(decOverallBalance, 2)
                L1 = mIntPrintTextInRectangle(ev, s1, intXposTotal + intTotalsCaptionWidth, intYpos, _
                                                   intTotalsValueWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
                intYpos += (L1 + (L1 \ 2))
                '-- end of this invoice-
                font1.bBold = False
                Call mlDrawLine(ev, intXposTotal, intYpos + 3, intTotalsCaptionWidth + intTotalsValueWidth)

                Call mbPageFooter(ev, sPageNo)
                ev.HasMorePages = False  '--all done-
                '=3411.1120
                '-  = RESET this when no more pages- (For printing from Dialog).
                '= mIntPageNo = 0
                miPageNo = 0
                mIntPagesPrintedCount = 0

                '== https://social.msdn.microsoft.com/Forums/vstudio/en-US/48203a8c-8c64-4a3d-964d-d7a330c1827f/printdocument-control-only-prints-last-page-of-document?forum=vbgeneral
            End If '-on new page-
        End If  '-more-

        Exit Function

PrintStatement_error:
        L1 = Err().Number
        MsgBox("Runtime Error in Print-Statement.." & vbCrLf & _
                                       "Error=" & L1 & ": " & ErrorToString(L1), MsgBoxStyle.Critical)

    End Function  '-mbPrintStatement_PageEvent-
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

    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==  TRANSFERED to new Class..


    '    Public Function mbPrintDebtorsReport_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
    '        Const k_HDR_HEIGHT = 26
    '        '= mlPrtWidth = 760
    '        Const k_WIDTH_CUST = 120      '--width of customer column.-
    '        Const k_WIDTH_DATE = 100      '--width of date column.-
    '        Const k_WIDTH_REFNO = 80  '--width of InvoiceNo column.-
    '        Const k_WIDTH_DESCR = 120  '--width of Description column.-
    '        Const k_WIDTH_DEBIT = 80     '--width of Invoice Amt column.-
    '        Const k_WIDTH_CREDITS = 80      '--width of CREDITS column.-
    '        Const k_WIDTH_BALANCE = 80   '--width of balance column.-
    '        Const k_WIDTH_DUE = 100      '--width of DUE date column.-
    '        '==Const k_WIDTH_TOTAL = 100  '--width of Ext.Total column.-

    '        '--  SUMMARY has a different Layout..
    '        '--  One line per Customer with ageing.
    '        Const k_WIDTH_SUMMARY_CUST = 340      '--width of SUMMARY customer column.-
    '        Const k_WIDTH_SUMMARY_CURRENT = 84      '--width of SUMMARY  column.-
    '        Const k_WIDTH_SUMMARY_30_DAYS = 84      '--width of SUMMARY  column.-
    '        Const k_WIDTH_SUMMARY_60_DAYS = 84      '--width of SUMMARY  column.-
    '        Const k_WIDTH_SUMMARY_90_DAYS = 84      '--width of SUMMARY  column.-
    '        Const k_WIDTH_SUMMARY_TOTAL = 84      '--width of SUMMARY  column.-

    '        Const k_REPORT_DETAIL_LINES As Integer = 56  '=44 '-TEMP 7 for test.== 62  '--Detail section.  220mm/3.5mm per line..-
    '        Const k_REPORT_SUMMARY_LINES As Integer = 40  '=30 '-TEMP 7 for test.== 62  '--Detail section.  220mm/3.5mm per line..-

    '        Dim intGreyBGColour As Integer = &HE0E0E0&
    '        Dim fillColor As Color
    '        Dim intXpos, intXpos2, intYpos, intYpos2 As Integer
    '        Dim intYposSummaryLine, intYposSummaryExtras As Integer
    '        Dim intHdrX As Integer = 150
    '        Dim intHdrY As Integer = 24
    '        Dim intLeftMarg, x1 As Integer
    '        Dim intXposDescr, intXposDebit, intXposCredits, intXposBal As Integer
    '        Dim L1, ix, intError, intCount1 As Integer
    '        Dim intDetailLinesNeeded, intDetailLinesAvailable As Integer
    '        '== Dim rowInvoice, rowDetail, row1 As DataRow
    '        Dim s1, sInvoiceNo, sDocType As String
    '        Dim sJobNo As String = "--"
    '        Dim dateInvoice As DateTime
    '        Dim sCustomerBarcode, sCustomerInfo As String
    '        Dim sABN, sPageNo As String
    '        Dim sChangeGiven, sNettRecvd As String
    '        Dim bPageFull As Boolean = False
    '        Dim decExtension As Decimal
    '        Dim decTotalTax As Decimal = 0
    '        '= Dim decDiscount, decRounding As Decimal
    '        '== Dim decSubTotalTax, decDiscTax As Decimal

    '        '= Dim decPayAmount, decTotalPayments, decAmountDebitedToAccount As Decimal
    '        Dim font1 As userFontDef
    '        Dim intInfoBoxWidth As Integer = 88  '= 120
    '        Dim intInfoBoxDepth As Integer = 27
    '        Dim intAddressBoxWidth As Integer = 380
    '        Dim intAddressBoxDepth As Integer = 120
    '        Dim intTermsBoxDepth As Integer = 100
    '        '-grid-
    '        Dim intGridYpos, intGridYdepth As Integer
    '        Dim penGrid As Pen = Pens.LightGray
    '        '-current customer-
    '        Dim colCurrentReportCustomer As Collection
    '        Dim colCurrentCustomerInfo As Collection
    '        Dim colCurrentInvoices As Collection
    '        '==  Target is new Build 4251..
    '        Dim colThisCustomerReversedInvoices As Collection

    '        Dim colInvoice, colRefunds As Collection
    '        Dim sCustName, sBarcode, sCustNameSummary, sCustNameLine2 As String
    '        Dim intCreditDays As Integer
    '        Dim colPayments As Collection
    '        Dim decInvoiceAmt, decTotalCredits As Decimal
    '        Dim decOutstanding As Decimal = 0  '==, decTotalRefundsAvail As Decimal = 0
    '        Dim sInvoiceId, sInvoiceDate, sTranType As String
    '        Dim sInvoiceTotal, sCreditTotal, sList As String
    '        Dim intDaysAged As Integer
    '        Dim intSummaryLineCount As Integer
    '        '=4201.0618=
    '        Dim sCustPhone, sCustMobile As String
    '        Dim decCustCreditNoteBalance As Decimal

    '        Call mbInitialiseBusiness()
    '        '-- Format ABN for printing..-
    '        sABN = VB.Left(msBusinessABN, 2) & " " & Mid(msBusinessABN, 3, 3) & _
    '                  " " & Mid(msBusinessABN, 6, 3) & " " & Mid(msBusinessABN, 9, 3)

    '        fillColor = ColorTranslator.FromOle(intGreyBGColour)

    '        '= MsgBox("Ready to print Invoice to " & msSelectedPrinterName & "..", MsgBoxStyle.Information)

    '        On Error GoTo PrintDebtorsReport_error
    '        font1.sName = "Lucida Sans Unicode"     '== "Tahoma" '== Printer.FontName = "Tahoma"
    '        font1.lngSize = 8 '==Printer.FontSize = 18
    '        font1.bBold = False '== Printer.Font.Bold = False '--reset to normal IN CASE LEFT OVER..-
    '        font1.bUnderline = False '= Printer.Font.Underline = False
    '        font1.bItalic = False

    '        intLeftMarg = k_LEFTMARGIN '== pixels-  (16 * PRT_UNIT) '- 240 twips..-
    '        intXpos = intLeftMarg
    '        intHdrX = intLeftMarg
    '        intYpos = 24  '--top pos.--
    '        intYpos2 = intYpos

    '        '-- Main Hdr stuff is on First page only..
    '        If (miPageNo <= 0) Then
    '            '--  paint BIZ logo  top left.--
    '            If Not (mImageUserLogo Is Nothing) Then
    '                x1 = mImageUserLogo.Width
    '                '= ev.Graphics.DrawImage(mImageUserLogo, intLeftMarg + mlPrtWidth - x1, 0)
    '                ev.Graphics.DrawImage(mImageUserLogo, intLeftMarg, 0)
    '            End If
    '            '-- initialise totals stuff..
    '            mIntCustomerCount = 1  '--first cust..
    '            mIntInvoiceCount = 0   '--but nothing done yet..

    '            mDecReportTotalTax = 0
    '            mDecReportTotalDebits = 0
    '            mDecReportTotalCredits = 0
    '            mDecReportTotalOutstanding = 0

    '            mDecReportAgedTotalCurrent = 0
    '            mDecReportAgedTotal30 = 0
    '            mDecReportAgedTotal60 = 0
    '            mDecReportAgedTotal90 = 0

    '        End If  '-first page-
    '        '-- report headr all pages..
    '        '-- print biz name (CENTERED)-
    '        font1.lngSize = 12
    '        font1.bBold = True
    '        intYpos = mlPrintTextString(ev, msBusinessName, -1, intYpos, font1)
    '        L1 = mlPrintTextString(ev, _
    '                              "Debtors Report" & IIf(mbDebtorsReportSummaryOnly, " (Summary)", ""), -1, intYpos, font1)
    '        '- As At DATE should be INPUT parameter..
    '        x1 = intLeftMarg + (mlPrtWidth \ 2) + 60  '= 120   '-- Date to right of "statement"..
    '        font1.lngSize = 9
    '        '-4201.1027--- Debtors Report--
    '        If (msDebtorsReportSubHeading <> "") Then  'was supplied-
    '            intYpos = mlPrintTextString(ev, msDebtorsReportSubHeading, x1, intYpos + 18, font1, textColour.black)
    '        Else  '-default.
    '            intYpos = mlPrintTextString(ev, "Outstanding as at: " & _
    '                                           Format(mDateCutOff, "dd-MMM-yyyy"), x1, intYpos + 18, font1, textColour.black)
    '        End If  '-sub heading.
    '        '-- Blank line..
    '        intYpos = mlPrintTextString(ev, "", intHdrX, intYpos, font1)
    '        intGridYpos = intYpos  '= + 16

    '        '--Page-1 Hdr done.--
    '        miPageNo += 1

    '        '-- All pages-
    '        '-- All pages-
    '        '-- GRID for Detail Lines..--
    '        '== intGridYpos = intYpos2 + 44 + intAddressBoxDepth + 12
    '        intGridYdepth = k_HDR_HEIGHT   '= 400
    '        intXpos = intLeftMarg

    '        '-print Page no..-
    '        sPageNo = "Page: " & CStr(miPageNo) & "."
    '        L1 = mIntPrintTextInRectangle(ev, sPageNo, _
    '                                      intLeftMarg + mlPrtWidth - k_WIDTH_DUE, intGridYpos, _
    '                                         k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, True, False, False) '-right Align.-
    '        '--draw the "grid"..
    '        intGridYpos += k_HDR_HEIGHT
    '        Call mlDrawLine(ev, intXpos, intGridYpos, mlPrtWidth)  '--top bar-
    '        '--column lines- 8 spaces, nine lines-
    '        '-here-

    '        '--Choose which Headings..
    '        If (Not mbDebtorsReportSummaryOnly) Then
    '            '--DETAIL REPORT- 
    '            '--   column lines- 8 spaces, nine lines-
    '            Dim arrayIntWidths() As Integer = _
    '                   {k_WIDTH_DATE, k_WIDTH_REFNO, k_WIDTH_DESCR, k_WIDTH_DEBIT, k_WIDTH_CREDITS, k_WIDTH_BALANCE, k_WIDTH_DUE}
    '            For ix = 0 To UBound(arrayIntWidths)
    '                ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)
    '                intXpos += arrayIntWidths(ix)
    '            Next ix
    '        Else '--summary-
    '            '--   column lines- 6 spaces,seven lines-
    '            Dim arrayIntWidths() As Integer = _
    '                   {k_WIDTH_SUMMARY_CUST, k_WIDTH_SUMMARY_CURRENT, _
    '                                  k_WIDTH_SUMMARY_30_DAYS, k_WIDTH_SUMMARY_60_DAYS, k_WIDTH_SUMMARY_90_DAYS, k_WIDTH_SUMMARY_TOTAL}
    '            For ix = 0 To UBound(arrayIntWidths)
    '                ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)
    '                intXpos += arrayIntWidths(ix)
    '            Next ix
    '        End If

    '        '--last vert line..
    '        ev.Graphics.DrawLine(penGrid, intXpos, intGridYpos, intXpos, intGridYpos + intGridYdepth)

    '        intXpos = intLeftMarg
    '        Call mlDrawLine(ev, intXpos, intGridYpos + intGridYdepth, mlPrtWidth)  '--bottom bar-

    '        '-- column header text line..
    '        '-- Can't use TAB stops because some items are Left-just and some are right..-
    '        '-- fill column headers BG..
    '        intXpos = intLeftMarg
    '        ev.Graphics.FillRectangle(New SolidBrush(fillColor), intXpos, intGridYpos, mlPrtWidth, k_HDR_HEIGHT)
    '        '-- box for col. hdrs. text-
    '        '=Dim rectHdr As New RectangleF(intXpos, intGridYpos, k_WIDTH_BC, k_HDR_HEIGHT)

    '        '-- PRINT column header TEXTS..
    '        '-- PRINT column header TEXTS..
    '        font1.lngSize = 8
    '        font1.bBold = True
    '        '--Choose which Headings..
    '        If (Not mbDebtorsReportSummaryOnly) Then
    '            '-detail report-
    '            Call mIntPrintTextInRectangle(ev, "Customer", intXpos, intGridYpos, _
    '                                                    k_WIDTH_CUST, k_HDR_HEIGHT, font1, textColour.black, False, True)
    '            intXpos += k_WIDTH_CUST
    '            Call mIntPrintTextInRectangle(ev, " Date", intXpos, intGridYpos, _
    '                                                     k_WIDTH_DATE, k_HDR_HEIGHT, font1, textColour.black, False, True)
    '            intXpos += k_WIDTH_DATE
    '            Call mIntPrintTextInRectangle(ev, " Invoice No.", intXpos, intGridYpos, _
    '                                                     k_WIDTH_REFNO, k_HDR_HEIGHT, font1, textColour.black, False, True)
    '            intXpos += k_WIDTH_REFNO
    '            Call mIntPrintTextInRectangle(ev, " Description", intXpos, intGridYpos, _
    '                                                      k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False, True)
    '            intXposDescr = intXpos  '--save position-
    '            intXpos += k_WIDTH_DESCR
    '            intXposDebit = intXpos  '-save-
    '            Call mIntPrintTextInRectangle(ev, "Invoice Amt", intXpos, intGridYpos, _
    '                                            k_WIDTH_DEBIT, k_HDR_HEIGHT, font1, textColour.black, False, True) '-NO Right Align.-
    '            intXpos += k_WIDTH_DEBIT
    '            intXposCredits = intXpos  '-save-
    '            Call mIntPrintTextInRectangle(ev, "Credits", intXpos, intGridYpos, _
    '                                            k_WIDTH_CREDITS, k_HDR_HEIGHT, font1, textColour.black, False, True, True) '-centre Align.-
    '            intXpos += k_WIDTH_CREDITS
    '            intXposBal = intXpos  '-save-
    '            Call mIntPrintTextInRectangle(ev, "Outstanding", intXpos, intGridYpos, _
    '                                           k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, False, True, True) '-CENTRE Align.-
    '            intXpos += k_WIDTH_BALANCE
    '            L1 = mIntPrintTextInRectangle(ev, "Due", intXpos, intGridYpos, _
    '                                             k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, False, True, True) '-CENTRE Align.-
    '        Else  '--Summary-
    '            Call mIntPrintTextInRectangle(ev, "Customer", intXpos, intGridYpos, _
    '                                                    k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, True)
    '            intXpos += k_WIDTH_SUMMARY_CUST
    '            Call mIntPrintTextInRectangle(ev, " Current", intXpos, intGridYpos, _
    '                                                     k_WIDTH_SUMMARY_CURRENT, k_HDR_HEIGHT, font1, textColour.black, True) '-R-align-
    '            intXpos += k_WIDTH_SUMMARY_CURRENT
    '            Call mIntPrintTextInRectangle(ev, " 30 Days.", intXpos, intGridYpos, _
    '                                                     k_WIDTH_SUMMARY_30_DAYS, k_HDR_HEIGHT, font1, textColour.black, True)
    '            intXpos += k_WIDTH_SUMMARY_30_DAYS
    '            Call mIntPrintTextInRectangle(ev, " 60 Days.", intXpos, intGridYpos, _
    '                                                      k_WIDTH_SUMMARY_60_DAYS, k_HDR_HEIGHT, font1, textColour.black, True)
    '            intXposDescr = intXpos  '--save position-
    '            intXpos += k_WIDTH_SUMMARY_60_DAYS
    '            '= intXposDebit = intXpos  '-save-
    '            Call mIntPrintTextInRectangle(ev, "90+ days", intXpos, intGridYpos, _
    '                                            k_WIDTH_SUMMARY_90_DAYS, k_HDR_HEIGHT, font1, textColour.black, True) '- Right Align.-
    '            intXpos += k_WIDTH_SUMMARY_90_DAYS
    '            '= intXposCredits = intXpos  '-save-
    '            Call mIntPrintTextInRectangle(ev, "Total Outst.", intXpos, intGridYpos, _
    '                                            k_WIDTH_SUMMARY_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-R. Align.-
    '            intXpos += k_WIDTH_SUMMARY_TOTAL
    '            '= intXposBal = intXpos  '-save-
    '        End If  '-summary-

    '        '= intXpos += k_WIDTH_DUE
    '        '-- L1 has line height.
    '        intXpos = intLeftMarg
    '        intYpos = intGridYpos + k_HDR_HEIGHT + 22  '= 12 '--jump col hdr row.
    '        font1.bBold = False

    '        '--  Print first/next page of Cust/Invoice Data..
    '        bPageFull = False
    '        '==If (mIntCustomerCount <= 0) Then  '--first page..  just starting-
    '        '== mIntCustomerCount = 1
    '        '== mIntInvoiceCount = 0
    '        '== colCurrentReportCustomer = mColReportCustomers.Item(1)
    '        '== Else '-pick up from prev. page.-
    '        '== colCurrentReportCustomer = mColReportCustomers.Item(mIntCustomerCount)
    '        '== End If
    '        intDetailLinesAvailable = k_REPORT_DETAIL_LINES

    '        mIntReportLineCount = 0  '-starting a page.-
    '        intSummaryLineCount = 0
    '        If (mIntCustomerCount <= mColReportCustomers.Count) Then '--still some customers.-
    '            '-- main loop for current/next customer..-
    '            Do  '--current/next customer-
    '                colCurrentReportCustomer = mColReportCustomers.Item(mIntCustomerCount)
    '                colCurrentCustomerInfo = colCurrentReportCustomer.Item("CustInfo")  '--from caller..-
    '                colCurrentInvoices = colCurrentReportCustomer.Item("Invoices")  '--from caller..-
    '                '-- get cust details..-
    '                sBarcode = colCurrentCustomerInfo.Item("barcode")
    '                '== sCustName = Trim(colCurrentCustomerInfo.Item("companyName"))

    '                '==  Target is new Build 4251..
    '                '==        colThisCustomerReversedInvoices = colThisCustomer.Item("reversedInvoices")
    '                colThisCustomerReversedInvoices = colCurrentReportCustomer.Item("reversedInvoices")  '--from caller..-

    '                '=3510.0224-
    '                intCount1 = colCurrentInvoices.Count
    '                If (intCount1 > 44) Then
    '                    intCount1 = 44    '== so we dont get to infinit page loop.
    '                End If
    '                '== Check if enough room for this customer's invoices.
    '                '=4201.0618=  more CHECK min room for a customer..
    '                intDetailLinesNeeded = intCount1 + 8  '= 7
    '                If (Not mbDebtorsReportSummaryOnly) AndAlso (intDetailLinesNeeded > intDetailLinesAvailable) Then
    '                    '--  exit and come back on new page for this customer..
    '                    bPageFull = True
    '                    Exit Do
    '                End If

    '                '== If (sCustName = "") Then  '-use person..-
    '                '==   sCustName = colCurrentCustomerInfo.Item("firstName") & " " & colCurrentCustomerInfo.Item("lastName")
    '                '=  End If

    '                '=3303.0116= 16Jan2017=
    '                colRefunds = colCurrentReportCustomer.Item("Refunds")

    '                sCustName = VB.Left(Trim(colCurrentCustomerInfo.Item("custShortName")), 36)
    '                sCustName &= " (" & sBarcode & ")"
    '                sCustNameSummary = sCustName   '-for summary..
    '                sCustNameLine2 = ""  '= for summary if name +Phone is too long.

    '                intCreditDays = CInt(colCurrentCustomerInfo.Item("creditDays"))
    '                '=4201.0618=
    '                sCustPhone = Trim(colCurrentCustomerInfo.Item("phone"))
    '                sCustMobile = Trim(colCurrentCustomerInfo.Item("mobile"))
    '                decCustCreditNoteBalance = colCurrentCustomerInfo.Item("CreditNoteBalance")

    '                '- print cust.name-  (if started, show "Continued"..)
    '                If (mIntReportLineCount = 0) And (miPageNo > 1) And (mIntInvoiceCount > 0) And _
    '                                        (mIntInvoiceCount <= colCurrentInvoices.Count) Then '--start of page 2 etc.-
    '                    sCustName &= " (continued)."
    '                End If
    '                font1.lngSize = 9
    '                font1.bBold = True
    '                '-save this vert pos for SUMMARY aged summary.
    '                intYposSummaryLine = intYpos
    '                intYposSummaryExtras = 0  '-in case of extra line.
    '                '- Print cust.name- (=4201.0618-  Add Phone No..)
    '                '--  (=4201.0618-  Add Phone No..)
    '                s1 = CStr(mIntCustomerCount) & ". " & sCustName & "; (Tel: " & sCustPhone & "; " & sCustMobile & ")"
    '                sCustNameLine2 = " (Tel: " & sCustPhone & "; " & sCustMobile & ")"
    '                '- print name-
    '                If (Not mbDebtorsReportSummaryOnly) Then
    '                    '-detail-
    '                    intYpos = mlPrintTextString(ev, s1, intLeftMarg, intYpos, font1)
    '                Else  '--summary-
    '                    font1.bBold = False
    '                    '-- print TelNo on second line if too long.
    '                    '--  VERTICAL pos doesn't move after this, as aged balance goes on same line.,
    '                    If (Len(s1) < 48) Then  '= ie < 340 px..
    '                        Call mIntPrintTextInRectangle(ev, s1, intLeftMarg, intYpos, _
    '                                                k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, False)
    '                    Else '--too long..
    '                        '-two lines..
    '                        sCustNameSummary = CStr(mIntCustomerCount) & ". " & sCustNameSummary
    '                        Call mIntPrintTextInRectangle(ev, sCustNameSummary, intLeftMarg, intYpos, _
    '                                       k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, False)
    '                        '- Tel. no on second line. (Just below name.)
    '                        font1.lngSize = 8
    '                        intYposSummaryExtras = 13  '--pixels.
    '                        Call mIntPrintTextInRectangle(ev, sCustNameLine2, intLeftMarg + 80, intYpos + 13, _
    '                                       k_WIDTH_SUMMARY_CUST, k_HDR_HEIGHT, font1, textColour.black, False, False)
    '                    End If  '-too long-
    '                End If
    '                mIntReportLineCount += 2

    '                '-- loop through Invoices..-
    '                font1.lngSize = 8
    '                font1.bBold = False
    '                If (colCurrentInvoices.Count > 0) Then
    '                    Do
    '                        mIntInvoiceCount += 1
    '                        colInvoice = colCurrentInvoices.Item(mIntInvoiceCount)
    '                        '= Dim decTotalCredits As Decimal = 0

    '                        colPayments = colInvoice.Item("invoicePayments")
    '                        sInvoiceDate = Format(colInvoice.Item("invoice_date"), "dd-MMM-yyyy")
    '                        sInvoiceId = CStr(colInvoice.Item("invoice_id"))
    '                        sTranType = colInvoice.Item("transactionType")
    '                        If (LCase(sTranType) = "sale") Then
    '                            sTranType = "Invoice (Sale)"
    '                        End If
    '                        decInvoiceAmt = CDec(colInvoice.Item("invoiceTotal"))
    '                        sInvoiceTotal = FormatCurrency(decInvoiceAmt, 2)
    '                        sInvoiceTotal = Replace(sInvoiceTotal, "$", "")
    '                        '-paymentTotalThisInvoice-
    '                        decTotalCredits = CDec(colInvoice.Item("paymentTotalThisInvoice"))
    '                        sCreditTotal = FormatCurrency(decTotalCredits, 2)
    '                        sCreditTotal = Replace(sCreditTotal, "$", "")
    '                        decOutstanding = decInvoiceAmt - decTotalCredits
    '                        '= decTaxAmt = CDec(colInvoice.Item("total_tax"))
    '                        '= sTax = FormatCurrency(decTaxAmt, 2)

    '                        If (LCase(sTranType) = "refund") Then
    '                            '= decTaxAmt = -decTaxAmt
    '                            decInvoiceAmt = -decInvoiceAmt
    '                        End If
    '                        '-- add to summary totals..-
    '                        mDecStatementTotalDebits += decInvoiceAmt
    '                        mDecReportTotalDebits += decInvoiceAmt

    '                        mDecStatementTotalCredits += decTotalCredits
    '                        mDecReportTotalCredits += decTotalCredits

    '                        mDecStatementTotalOutstanding += decOutstanding
    '                        mDecReportTotalOutstanding += decOutstanding
    '                        '-- Age the outstanding..
    '                        intDaysAged = DateDiff(DateInterval.Day, colInvoice.Item("invoice_date"), Today)
    '                        If (intDaysAged <= 30) Then
    '                            mDecStatementAgedTotalCurrent += decOutstanding
    '                            mDecReportAgedTotalCurrent += decOutstanding
    '                        ElseIf (intDaysAged <= 60) Then
    '                            mDecStatementAgedTotal30 += decOutstanding
    '                            mDecReportAgedTotal30 += decOutstanding
    '                        ElseIf (intDaysAged <= 90) Then
    '                            mDecStatementAgedTotal60 += decOutstanding
    '                            mDecReportAgedTotal60 += decOutstanding
    '                        Else  '--90 and over-
    '                            mDecStatementAgedTotal90 += decOutstanding
    '                            mDecReportAgedTotal90 += decOutstanding
    '                        End If

    '                        '- Show invoice line if Detail REport-
    '                        If (Not mbDebtorsReportSummaryOnly) Then
    '                            intXpos = intLeftMarg + k_WIDTH_CUST
    '                            '-- print invoice line -
    '                            '= intYpos += 3   '-- down a bit from prev. line..
    '                            Call mIntPrintTextInRectangle(ev, sInvoiceDate, intXpos, intYpos, _
    '                                                                       k_WIDTH_DATE, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
    '                            intXpos += k_WIDTH_DATE
    '                            Call mIntPrintTextInRectangle(ev, sInvoiceId & "  ", intXpos, intYpos, _
    '                                                        k_WIDTH_REFNO, k_HDR_HEIGHT, font1, textColour.black, True) '-no box-
    '                            intXpos += k_WIDTH_REFNO
    '                            Call mIntPrintTextInRectangle(ev, "  " & sTranType, intXpos, intYpos, _
    '                                                      k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
    '                            intXpos += k_WIDTH_DESCR
    '                            '-invoice amount-
    '                            L1 = mIntPrintTextInRectangle(ev, sInvoiceTotal, intXpos, intYpos, _
    '                                                     k_WIDTH_DEBIT, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                            intXpos += k_WIDTH_DEBIT
    '                            L1 = mIntPrintTextInRectangle(ev, sCreditTotal, intXpos, intYpos, _
    '                                                         k_WIDTH_CREDITS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                            intXpos += k_WIDTH_CREDITS
    '                            s1 = FormatCurrency(decOutstanding, 2)
    '                            L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
    '                                                         k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                            intXpos += k_WIDTH_BALANCE
    '                            '-- Calculate and print DUE date..
    '                            s1 = Format(DateAdd(DateInterval.Day, intCreditDays, CDate(colInvoice.Item("invoice_date"))), "dd-MMM-yyyy")
    '                            If (decOutstanding > 0) Then
    '                                Call mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
    '                                                            k_WIDTH_DUE, k_HDR_HEIGHT, font1, textColour.black, False, False, True) '-no box-
    '                            End If
    '                            intYpos += L1
    '                            mIntReportLineCount += 1
    '                            intDetailLinesAvailable -= 1  '--this will m ake sure the next customer fits.
    '                            bPageFull = (mIntReportLineCount > k_REPORT_DETAIL_LINES)
    '                        End If  '-detail-
    '                    Loop Until (mIntInvoiceCount >= colCurrentInvoices.Count) Or bPageFull
    '                    '===If bPageFull Then Exit Do '--bypass 
    '                End If  '-invoice count-



    '                '==  Target is new Build 4251..
    '                '==  Target is new Build 4251..
    '                '-First, show any reversed invoices..

    '                '==   Target-New-Build-4262 -- (Started 14-Aug-2020)  26-Aug-2020..
    '                '==      Debtors Report-  Allow space for Reversed Invoices so as to stop overprinting the next customer.. . 
    '                '==      ALSO.. show REVERSALS only for Detailed Report..
    '                '==colThisCustomerReversedInvoices = colCurrentReportCustomer.Item("reversedInvoices")  '--from caller..-
    '                If (Not bPageFull) AndAlso (Not mbDebtorsReportSummaryOnly) Then
    '                    If (colThisCustomerReversedInvoices IsNot Nothing) AndAlso _
    '                       (colThisCustomerReversedInvoices.Count > 0) Then
    '                        Dim colRefund As Collection
    '                        font1.lngSize = 8
    '                        intYpos = mlPrintTextString(ev, "", intLeftMarg, intYpos, font1)  '--gap-
    '                        If (mbDebtorsReportSummaryOnly) Then
    '                            intYpos += (k_HDR_HEIGHT \ 7)  '-- a bit more gap.
    '                            intYpos = mlPrintTextString(ev, "", intLeftMarg, intYpos, font1)  '--more gap-
    '                        End If
    '                        For Each colInv1 As Collection In colThisCustomerReversedInvoices
    '                            colRefund = colInv1.Item("refundInvoice")
    '                            s1 = "** Invoice #" & colInv1.Item("invoice_id") & _
    '                                           " was reversed by Refund #" & colRefund.Item("invoice_id") & _
    '                                              " on " & Format(colRefund.Item("invoice_date"), "dd-MMM-yyyy") & vbCrLf
    '                            intYpos = mlPrintTextString(ev, s1, intLeftMarg, intYpos, font1)
    '                            mIntReportLineCount += 1

    '                            '==   Target-New-Build-4262 -- (Started 14-Aug-2020)  26-Aug-2020..
    '                            '==      Debtors Report-  Allow space for Reversed Invoices so as to stop overprinting the next customer.. . 
    '                            intDetailLinesAvailable -= 1  '--this will m ake sure the next customer fits.

    '                        Next colInv1
    '                        If (mbDebtorsReportSummaryOnly) Then
    '                            intYpos += (k_HDR_HEIGHT \ 3)  '-- a bit more gap after.
    '                        End If
    '                    End If  '-reversed..
    '                    '== END OF Target is new Build 4251..

    '                End If  '-page full etc.
    '                '== END  Target-New-Build-4262 -- (Started 14-Aug-2020)  26-Aug-2020..



    '                If (mIntInvoiceCount < colCurrentInvoices.Count) Then  '-cust not finished-
    '                    '-- continued next page-
    '                    intYpos = mlPrintTextString(ev, "This Customer continued on next page..", intLeftMarg, intYpos, font1)
    '                ElseIf bPageFull And (mIntInvoiceCount <= 0) Then
    '                    '--new customer on next page.
    '                Else '--cust done-
    '                    '- Show Cust totals line if Detail REport-
    '                    If (Not mbDebtorsReportSummaryOnly) Then
    '                        '= intYpos += 15  '--plus a line.-
    '                        '-- Totals for Customer !! -
    '                        mIntReportLineCount += 3

    '                        font1.bBold = True
    '                        Call mlDrawLine(ev, intXposDebit, intYpos, k_WIDTH_DEBIT + k_WIDTH_CREDITS + k_WIDTH_BALANCE)
    '                        Call mIntPrintTextInRectangle(ev, "Cust. Totals:", intXposDescr, intYpos, _
    '                                                                       k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
    '                        s1 = FormatCurrency(mDecStatementTotalDebits, 2)
    '                        L1 = mIntPrintTextInRectangle(ev, s1, intXposDebit, intYpos, _
    '                                                      k_WIDTH_DEBIT, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                        s1 = FormatCurrency(mDecStatementTotalCredits, 2)
    '                        L1 = mIntPrintTextInRectangle(ev, s1, intXposCredits, intYpos, _
    '                                                      k_WIDTH_CREDITS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                        s1 = FormatCurrency(mDecStatementTotalOutstanding, 2)
    '                        L1 = mIntPrintTextInRectangle(ev, s1, intXposBal, intYpos, _
    '                                                      k_WIDTH_BALANCE, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                        intYpos += k_HDR_HEIGHT + 5

    '                        '-- Aged totals for Customer !! -
    '                        font1.bBold = False
    '                        font1.lngSize = 8
    '                        '=Call mIntPrintTextInRectangle(ev, "Aged balance: ", intXposDescr, intYpos, _
    '                        '=                                                 k_WIDTH_DESCR, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
    '                        s1 = "Current:" & FormatCurrency(mDecStatementAgedTotalCurrent, 2)
    '                        s1 &= ";  30 Days:" & FormatCurrency(mDecStatementAgedTotal30, 2)
    '                        s1 &= ";  60 Days:" & FormatCurrency(mDecStatementAgedTotal60, 2)
    '                        s1 &= ";  90+ Days:" & FormatCurrency(mDecStatementAgedTotal90, 2)
    '                        intYpos = mlPrintTextString(ev, "Aged balance: ", intXposDescr, intYpos, font1)
    '                        intYpos = mlPrintTextString(ev, s1, intXposDescr, intYpos, font1)
    '                        intYpos += 2  '=3  '--plus half a line.-
    '                        '==  3303.0114- 14Jan2017=
    '                        '-- FIRST print total Avail. refunds if any.-
    '                        '=4201.0618- NO more Refunds on Debtors..
    '                        '--  Now using CreditNotes..
    '                        '--    decTotalRefundsAvail = 0

    '                        'sList = " in Refunds not yet credited- (Refunds Nos: "
    '                        'If (colRefunds.Count > 0) Then
    '                        '    For Each colRefund As Collection In colRefunds
    '                        '        decTotalRefundsAvail += CDec(colRefund.Item("AmountAvailable"))
    '                        '        sList &= CStr(colRefund.Item("invoice_id")) & "; "
    '                        '    Next colRefund
    '                        If (decCustCreditNoteBalance > 0) Then '= (decTotalRefundsAvail > 0) Then
    '                            '-- reduce outstanding..
    '                            '- not now-mDecStatementTotalOutstanding -= decTotalRefundsAvail
    '                            '= adjust aged bals..
    '                            '= not now-
    '                            '-print credit refunds line-
    '                            '= intYpos += k_HDR_HEIGHT
    '                            '= s1 = "NB: " & FormatCurrency(decTotalRefundsAvail, 2) & " " & sList & " not applied).."
    '                            s1 = "(NB: " & FormatCurrency(decCustCreditNoteBalance, 2) & " Credit-Note balance not yet applied).."
    '                            font1.bBold = True
    '                            intYpos = mlPrintTextString(ev, s1, intXposDescr, intYpos, font1, textColour.magenta)
    '                            Call mlDrawLine(ev, k_LEFTMARGIN, intYpos + 3, mlPrtWidth - k_WIDTH_DUE)
    '                            font1.bBold = False
    '                        Else  '-no avail.-
    '                            '- Draw line under customer..
    '                            font1.bBold = False
    '                            font1.lngSize = 8
    '                            Call mlDrawLine(ev, k_LEFTMARGIN, intYpos, mlPrtWidth - k_WIDTH_DUE)
    '                        End If '-avail-
    '                        'End If  '- REFUND count-
    '                        intDetailLinesAvailable -= 7  '-- was space for cust totals..
    '                    Else
    '                        '--Aged SUMMARY report Line for cust.
    '                        intXpos = intLeftMarg + k_WIDTH_SUMMARY_CUST
    '                        intYpos = intYposSummaryLine
    '                        s1 = FormatCurrency(mDecStatementAgedTotalCurrent, 2)
    '                        s1 = Replace(s1, "$", "")  '--drop dollar-
    '                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
    '                                             k_WIDTH_SUMMARY_CURRENT, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                        intXpos += k_WIDTH_SUMMARY_CURRENT

    '                        s1 = FormatCurrency(mDecStatementAgedTotal30, 2)
    '                        s1 = Replace(s1, "$", "")  '--drop dollar-
    '                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
    '                                             k_WIDTH_SUMMARY_30_DAYS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                        intXpos += k_WIDTH_SUMMARY_30_DAYS

    '                        s1 = FormatCurrency(mDecStatementAgedTotal60, 2)
    '                        s1 = Replace(s1, "$", "")  '--drop dollar-
    '                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
    '                                             k_WIDTH_SUMMARY_60_DAYS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                        intXpos += k_WIDTH_SUMMARY_60_DAYS

    '                        s1 = FormatCurrency(mDecStatementAgedTotal90, 2)
    '                        s1 = Replace(s1, "$", "")  '--drop dollar-
    '                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
    '                                             k_WIDTH_SUMMARY_90_DAYS, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                        intXpos += k_WIDTH_SUMMARY_90_DAYS

    '                        s1 = FormatCurrency(mDecStatementTotalOutstanding, 2)
    '                        s1 = Replace(s1, "$", "")  '--drop dollar-
    '                        L1 = mIntPrintTextInRectangle(ev, s1, intXpos, intYpos, _
    '                                             k_WIDTH_SUMMARY_TOTAL, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                        '= intXpos += k_WIDTH_SUMMARY_CURRENT
    '                        mIntReportLineCount += 1
    '                        '= bPageFull = (mIntReportLineCount > k_REPORT_SUMMARY_LINES)
    '                        intSummaryLineCount += 1
    '                        If intYposSummaryExtras > 0 Then
    '                            intYpos += intYposSummaryExtras  '--in case Tel.No on second line.
    '                            intSummaryLineCount += 1
    '                        End If
    '                        bPageFull = (intSummaryLineCount > k_REPORT_SUMMARY_LINES)
    '                    End If 'detail/summary-

    '                    '-- All reports- clear statement totals.

    '                    intYpos += k_HDR_HEIGHT  '-for next customer..
    '                    mIntInvoiceCount = 0
    '                    mDecStatementTotalDebits = 0
    '                    mDecStatementTotalCredits = 0
    '                    mDecStatementTotalOutstanding = 0
    '                    mDecStatementAgedTotalCurrent = 0
    '                    mDecStatementAgedTotal30 = 0
    '                    mDecStatementAgedTotal60 = 0
    '                    mDecStatementAgedTotal90 = 0

    '                    mIntCustomerCount += 1  '--started at 1..
    '                End If  '-cust done-
    '            Loop Until (mIntCustomerCount > mColReportCustomers.Count) Or bPageFull
    '        End If '--still some customers.-
    '        '-- print report totals..--
    '        If bPageFull Then
    '            '-- next page-
    '        Else  '-print-
    '            Dim intAgeWidth = 90
    '            If (mIntReportLineCount <= 44) Then '- = (k_REPORT_LINES - 14) == have room for totals..
    '                intYpos = k_STATEMENT_SUMMARY_TOP
    '                L1 = mlPrintTextInBox(ev, vbCrLf & "<b>Final Totals- Aged Balances:", _
    '                        k_LEFTMARGIN, intYpos, 16, intAddressBoxWidth, intAddressBoxDepth + 48, True)
    '                '-- SUMMARY of aged info--
    '                intXpos = k_LEFTMARGIN + 16
    '                intYpos += 36
    '                '-- Aged headings-
    '                L1 = mIntPrintTextInRectangle(ev, "Current", intXpos, intYpos, _
    '                                                    intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                L1 = mIntPrintTextInRectangle(ev, "30 Days", intXpos + intAgeWidth, intYpos, _
    '                                                     intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                L1 = mIntPrintTextInRectangle(ev, "60 Days", intXpos + (intAgeWidth * 2), intYpos, _
    '                                                     intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                L1 = mIntPrintTextInRectangle(ev, "90 Days", intXpos + (intAgeWidth * 3), intYpos, _
    '                                                     intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                '--Aged totals-
    '                intYpos += L1 + 4
    '                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecReportAgedTotalCurrent, 2), intXpos, intYpos, _
    '                                                intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecReportAgedTotal30, 2), intXpos + intAgeWidth, intYpos, _
    '                                                 intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecReportAgedTotal60, 2), intXpos + (intAgeWidth * 2), intYpos, _
    '                                                 intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                L1 = mIntPrintTextInRectangle(ev, FormatCurrency(mDecReportAgedTotal90, 2), intXpos + (intAgeWidth * 3), intYpos, _
    '                                                 intAgeWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                intYpos += k_HDR_HEIGHT + 8

    '                '- print total outstanding.-

    '                font1.bBold = True
    '                intYpos = mlPrintTextString(ev, "Final Total Outstanding: " & _
    '                                              FormatCurrency(mDecReportTotalOutstanding, 2), k_LEFTMARGIN + 16, intYpos, font1)
    '                intYpos = mlPrintTextString(ev, "For: " & _
    '                                          CStr(mColReportCustomers.Count) & " Account customers.", k_LEFTMARGIN + 16, intYpos, font1)
    '                intYpos += 6

    '                '-- Print Total Invoices and Credits..
    '                '--RHS invoice totals-
    '                '--RHS invoice totals-
    '                intYpos = k_STATEMENT_SUMMARY_TOP
    '                intXpos = k_LEFTMARGIN
    '                intXpos2 = intXpos + mlPrtWidth - intAddressBoxWidth
    '                '== intYpos = intGridYpos + intGridYdepth + 24
    '                L1 = mlPrintTextInBox(ev, vbCrLf & "<b>Total all Invoices:", _
    '                          intXpos2, intYpos, 16, intAddressBoxWidth, intAddressBoxDepth + 48, True)
    '                intYpos += 32
    '                font1.bBold = True
    '                font1.bItalic = False
    '                '- Private mDecStatementTotalTax As Decimal
    '                '- Private mDecStatementTotalDebits As Decimal
    '                '- Private mDecStatementTotalCredits As Decimal
    '                Dim intTotalsCaptionWidth As Integer = 140
    '                Dim intTotalsValueWidth As Integer = 170
    '                Dim intXposTotal = intXpos2 + 28
    '                '--Totals line..
    '                s1 = FormatCurrency(mDecReportTotalDebits, 2)
    '                L1 = mIntPrintTextInRectangle(ev, "Total Debits Inc Tax: ", intXposTotal, intYpos, _
    '                                     intTotalsCaptionWidth, k_HDR_HEIGHT, font1, textColour.black, False) '-L/Align, no box-
    '                font1.bBold = False
    '                L1 = mIntPrintTextInRectangle(ev, s1, intXposTotal + intTotalsCaptionWidth, intYpos, _
    '                                                   intTotalsValueWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                intYpos += L1
    '                '-- blank line.-
    '                intYpos = mlPrintTextString(ev, "", intXposTotal, intYpos, font1)
    '                '--Credits line..
    '                font1.bBold = True
    '                s1 = FormatCurrency(mDecReportTotalCredits, 2)
    '                L1 = mIntPrintTextInRectangle(ev, "Total Credits: ", intXposTotal, intYpos, _
    '                                     intTotalsCaptionWidth, k_HDR_HEIGHT, font1, textColour.black, False) '-L/Align, no box-
    '                font1.bBold = False
    '                L1 = mIntPrintTextInRectangle(ev, s1, intXposTotal + intTotalsCaptionWidth, intYpos, _
    '                                                   intTotalsValueWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                '-- add minus sign.-
    '                x1 = intXposTotal + intTotalsCaptionWidth + intTotalsValueWidth
    '                Call mlPrintTextString(ev, "-", x1, intYpos + (k_HDR_HEIGHT \ 2), font1)
    '                intYpos += (L1 + (L1 \ 2)) + 3
    '                font1.bBold = True
    '                '-- balance-
    '                Dim decOverallBalance As Decimal = mDecReportTotalDebits - mDecReportTotalCredits
    '                Call mlDrawLine(ev, intXposTotal, intYpos, intTotalsCaptionWidth + intTotalsValueWidth)
    '                Call mIntPrintTextInRectangle(ev, "Total Balance:", intXposTotal, intYpos, _
    '                                                              intTotalsCaptionWidth, k_HDR_HEIGHT, font1, textColour.black, False) '-no box-
    '                s1 = FormatCurrency(decOverallBalance, 2)
    '                L1 = mIntPrintTextInRectangle(ev, s1, intXposTotal + intTotalsCaptionWidth, intYpos, _
    '                                                   intTotalsValueWidth, k_HDR_HEIGHT, font1, textColour.black, True) '-R/Align, no box-
    '                intYpos += (L1 + (L1 \ 2))
    '                '-- end of this invoice-
    '                font1.bBold = False
    '                Call mlDrawLine(ev, intXposTotal, intYpos + 3, intTotalsCaptionWidth + intTotalsValueWidth)
    '            Else
    '                bPageFull = True  '-totals on next page-
    '            End If  '--have room-
    '        End If  '--print totals.-


    '        '--TEMP--??-
    '        Call mbPageFooter(ev, sPageNo)
    '        ev.HasMorePages = bPageFull   '--all done-
    '        '-27-Oct-2019-
    '        If Not ev.HasMorePages Then
    '            '-  = RESET this when no more pages- (For printing from Dialog).
    '            miPageNo = 0
    '            '=mIntPagesPrintedCount = 0
    '        End If

    '        Exit Function  '--done--

    'PrintDebtorsReport_error:
    '        L1 = Err().Number
    '        MsgBox("Runtime Error in mbPrintDebtorsReport_PageEvent function.." & vbCrLf & _
    '                                       "Error=" & L1 & ": " & ErrorToString(L1), MsgBoxStyle.Critical)

    '    End Function  '-DebtorsReport-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '=3107.820-
    '-- End pribt event..
    '-- Signal competion..

    Private Sub printDocument1_EndPrint(ByVal sender As Object, _
                                           ByVal ev As PrintEventArgs) _
                                             Handles printDocument1.EndPrint
        mbPrintingCompleted = True

        '-check for errors-

        '-- ????---


    End Sub  '-end print-
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
            Case printDocType.SalesInvoice
                Call mbPrintSalesInvoice_PageEvent(ev)
            Case printDocType.Receipt
                Call mbPrintReceipt_PageEvent(ev)
            Case printDocType.dgv
                Call mbPrintDataGridView_PageEvent(ev)
            Case printDocType.StockLabels
                Call mbPrintStockLabels_PageEvent(ev)
            Case printDocType.LaybyLabels
                Call mbPrintLaybyLabels_PageEvent(ev)
            Case printDocType.Statement
                Call mbPrintStatement_PageEvent(ev)

                '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
                '==  TRANSFERED to new Class..

                'Case printDocType.DebtorsReport
                '    Call mbPrintDebtorsReport_PageEvent(ev)

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

    '- Public print Document Functions..--


    '--PrintReceipt-
    '--PrintReceipt-
    '= 3411.0113=  Wait for pdf file to be made..

    Public Function PrintDocket(ByRef colReportLines As Collection, _
                                Optional ByVal bWaitForCompletion As Boolean = False) As Boolean

        PrintDocket = False

        mbPrintingCompleted = False  '=3107.820-
        mbPrintError = False
        msPrintErrorMsg = ""

        '-- save stuff to print..-
        mColReportLines = colReportLines
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.Receipt

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Receipt printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        PrintDocket = True
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '=3411.0109= MS PDF.- msPrintToFileFullPath-
            If (msPrintToFileFullPath <> "") Then  '-printing to file..
                '- Don't do this for adobe-
                If (InStr(LCase(msSelectedPrinterName), "adobe") <= 0) Then '-no adobe-
                    '-- should be Microsoft pdf.
                    printDocument1.PrinterSettings.PrintToFile = True
                    printDocument1.PrinterSettings.PrintFileName = msPrintToFileFullPath
                End If
            End If
            '--  start the printer..--
            printDocument1.Print()
            If bWaitForCompletion Then
                '--3411.0113- WAIT for PDF to be made..-
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Dim intStart, intFinish As Integer
                intStart = CInt(VB.Timer)
                intFinish = intStart + 30
                While (Not mbPrintingCompleted) And (CInt(VB.Timer) < intFinish)
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(500)  '--milliseconds..-
                End While
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                If (Not mbPrintingCompleted) Then
                    MsgBox("Printing not completed..", MsgBoxStyle.Exclamation)
                    PrintDocket = False
                    Exit Function
                End If
                '-- Print Completed may happen, but File still not finished.
                '-- wait a bit, then test..
                intStart = CInt(VB.Timer)
                intFinish = intStart + 30
                Do
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(1500)  '--milliseconds..-
                Loop Until My.Computer.FileSystem.FileExists(msPrintToFileFullPath) Or _
                                                                 (CInt(VB.Timer) >= intFinish)
                If My.Computer.FileSystem.FileExists(msPrintToFileFullPath) Then
                    PrintDocket = True
                End If
            Else  '-no wait-
                PrintDocket = True
            End If  '-wait-
        Catch ex As Exception
            '== MessageBox.Show(ex.Message)
            MsgBox("Error in printing Receipt/docket.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintDocket = False
        End Try

    End Function  '--PrintReceipt-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '--Invoice -
    '=3411.0109= MS PDF.- msPrintToFileFullPath-

    Public Function PrintSalesInvoice(Optional ByVal bWaitForCompletion As Boolean = True) As Boolean

        PrintSalesInvoice = False
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.SalesInvoice

        mbPrintingCompleted = False  '=3107.820-
        mbPrintError = False
        msPrintErrorMsg = ""
        mIntPageNo = 0

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Invoice printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        If mDataTableInvoice Is Nothing Then
            MsgBox("Invoice Data Table not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        '= PrintSalesInvoice = True
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '=3411.0109= MS PDF.- msPrintToFileFullPath-
            If (msPrintToFileFullPath <> "") Then  '-printing to file..
                '- Don't do this for adobe-
                If (InStr(LCase(msSelectedPrinterName), "adobe") <= 0) Then '-no adobe-
                    '-- should be Microsoft pdf.
                    printDocument1.PrinterSettings.PrintToFile = True
                    printDocument1.PrinterSettings.PrintFileName = msPrintToFileFullPath
                End If
            End If

            '--  start the printer..--
            printDocument1.Print()
            If bWaitForCompletion Then
                '--3411.1117- WAIT for PDF to be made..-
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                Dim intStart, intFinish As Integer
                intStart = CInt(VB.Timer)
                intFinish = intStart + 30
                While (Not mbPrintingCompleted) And (CInt(VB.Timer) < intFinish)
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(500)  '--milliseconds..-
                End While
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                If (Not mbPrintingCompleted) Then
                    MsgBox("Printing not completed..", MsgBoxStyle.Exclamation)
                    PrintSalesInvoice = False
                    Exit Function
                End If
                '-- Print Completed may happen, but File still not finished.
                '-- wait a bit, then test..
                intStart = CInt(VB.Timer)
                intFinish = intStart + 30
                Do
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(1500)  '--milliseconds..-
                Loop Until My.Computer.FileSystem.FileExists(msPrintToFileFullPath) Or _
                                                                 (CInt(VB.Timer) >= intFinish)
                If My.Computer.FileSystem.FileExists(msPrintToFileFullPath) Then
                    PrintSalesInvoice = True
                End If
            Else  '-no wait-
                PrintSalesInvoice = True
            End If  '-wait-
        Catch ex As Exception
            '== MessageBox.Show(ex.Message)
            MsgBox("Error in printing Invoice.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintSalesInvoice = False
        End Try
    End Function  '-- Print invoice-
    '= = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '- print dataGridView - (eg for Reports).-
    '- print dataGridView - (eg for Reports).-

    '-- Roughly based on:
    '--   http://www.codeproject.com/Articles/28046/Printing-of-DataGridView

    Public Function PrintDataGridView(ByVal strReportTitle As String, _
                                      ByRef dataGridView1 As DataGridView, _
                                      Optional ByVal strReportSubHeading As String = "", _
                                      Optional ByVal strReportSubHeading2 As String = "", _
                                      Optional ByVal bShadeAlternateRows As Boolean = False) As Boolean

        PrintDataGridView = False

        mbPrintingCompleted = False  '=3107.820-
        mbPrintError = False
        msPrintErrorMsg = ""
        mbShadeAlternateRows = bShadeAlternateRows

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Receipt printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '-- save stuff to print..-
        mDataGridView1 = dataGridView1

        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.dgv
        msReportTitle = strReportTitle
        msReportSubHeading = strReportSubHeading
        msReportSubHeading2 = strReportSubHeading2
        '-- do some initial setup--
        '-- do some initial setup--
        mbFirstPage = True
        mIntPageNo = 0

        mIntTotalWidth = 0
        mColColumnLefts = New Collection
        mColColumnWidths = New Collection

        mStrFormat1 = New StringFormat()
        mStrFormat1.Alignment = StringAlignment.Near
        mStrFormat1.LineAlignment = StringAlignment.Center
        mStrFormat1.Trimming = StringTrimming.EllipsisCharacter

        mStrFormatRight = New StringFormat()
        mStrFormatRight.Alignment = StringAlignment.Far
        mStrFormatRight.LineAlignment = StringAlignment.Center  '--vertical-
        mStrFormatRight.Trimming = StringTrimming.EllipsisCharacter

        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
            PrintDataGridView = True
        Catch ex As Exception
            '== MessageBox.Show(ex.Message)
            MsgBox("Error in printing Grid.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintDataGridView = False
        End Try

    End Function  '--PrintDataGridView--
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--stock labels.-

    Public Function PrintStockLabels(ByRef colLabels As Collection, _
                                      ByVal sBusinessName As String, _
                                      ByVal sSelectedPrinterName As String, _
                                       ByVal sItemBarcodeFontName As String, _
                                       ByVal intItemBarcodeFontSize As Integer) As Boolean

        PrintStockLabels = False

        mbPrintingCompleted = False  '=3107.820-
        mbPrintError = False
        msPrintErrorMsg = ""

        '-- save stuff to print..-
        mColStockLabels = colLabels
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.StockLabels
        miPageNo = 0
        If (colLabels Is Nothing) OrElse (colLabels.Count <= 0) Then
            MsgBox("No Labels to print..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        msBusinessName = sBusinessName
        msSelectedPrinterName = sSelectedPrinterName
        msItemBarcodeFontName = sItemBarcodeFontName
        mIntItemBarcodeFontSize = intItemBarcodeFontSize

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Label printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        PrintStockLabels = True
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            '== MessageBox.Show(ex.Message)
            MsgBox("Error in printing Stock Labels.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintStockLabels = False
        End Try

    End Function  '--PrintStockLabels-
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '=3403.513=-
    '- -Layby labels.-

    Public Function PrintLaybyLabels(ByVal lngLaybyId As Integer, _
                                      ByVal intNoLabels As Integer, _
                                         ByVal sSelectedPrinterName As String, _
                                         ByVal sCustomer As String, _
                                         ByVal strInfo As String) As Boolean

        PrintLaybyLabels = False
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.LaybyLabels
        mLngLaybyId = lngLaybyId
        mIntNoLaybyLabels = intNoLabels
        msLaybyCustomer = sCustomer
        msSelectedPrinterName = sSelectedPrinterName
        '=3203.212=
        '= mbIsRAlabel = bIsRALabel
        msLaybyInfo = strInfo

        miPageNo = 0

        If (msSelectedPrinterName = "") Then
            MsgBox("Label printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        PrintLaybyLabels = True

        '-- WE use PHYSICAL PAGE- as Graphics Origin..-
        printDocument1.OriginAtMargins = False
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            MsgBox("Error in printing JobLabels.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintLaybyLabels = False
        End Try

    End Function  '-- Job Labels..-
    '= = = = = = = = = = = = = = 


    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Print A Statement-
    '=3411.1118= Add Wait for completion-

    Public Function PrintStatement(ByRef colInvoices As Collection, _
                                   ByRef colRefunds As Collection, _
                                    ByRef colCustomerInfo As Collection, _
                                    ByVal dateCutOff As Date, _
                                      ByRef SystemInfo As clsSystemInfo, _
                                       ByRef ImageUserLogo As Image, _
                                      ByVal sVersionPOS As String, _
                                      ByVal sSelectedPrinterName As String, _
                                      Optional ByVal bPreviewOnly As Boolean = False, _
                                      Optional ByVal bWaitForCompletion As Boolean = True) As Boolean

        Dim colInvoice, colDetailPage As Collection
        Dim colPayments As Collection
        Dim intLines, intLinesAvail As Integer

        PrintStatement = False

        mbPrintingCompleted = False  '=3107.820-
        mbPrintError = False
        msPrintErrorMsg = ""

        '-- save stuff to print..-
        mColInvoices = colInvoices
        mColRefunds = colRefunds
        mColCustomerInfo = colCustomerInfo
        mDateCutOff = dateCutOff

        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.Statement
        miPageNo = 0
        If (mColInvoices Is Nothing) OrElse (mColInvoices.Count <= 0) Then
            MsgBox("No Statement Invoices to print..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        '== mSdSystemInfo = sdSystemInfo
        mSysInfo1 = SystemInfo  '= mSdSystemInfo = sdSystemInfo
        mImageUserLogo = ImageUserLogo
        msVersionPOS = sVersionPOS
        msSelectedPrinterName = sSelectedPrinterName

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Statement printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '-- Look through invoices/payments, and -
        '--  break into pages.
        mColStatementDetailPages = New Collection
        colDetailPage = New Collection   '--first page--
        '=4219-1113-  First page has fewer detail lines..
        intLinesAvail = k_STATEMENT_DETAIL_LINES_PAGE1
        For Each colInvoice In mColInvoices
            '-- get no lines needed for this invoice.-
            colPayments = colInvoice.Item("invoicePayments")
            intLines = colPayments.Count + 4
            If (colPayments.Count > 0) Then
                intLines += 1  '--for total credits..-
            End If
            '- try and fit on to current page..-
            If (intLines <= intLinesAvail) Then  '--  yes-
                intLinesAvail -= intLines
                colDetailPage.Add(colInvoice)   '-add invoice to current page-
            Else  '-won't fit.  start new page-
                mColStatementDetailPages.Add(colDetailPage)
                colDetailPage = New Collection
                colDetailPage.Add(colInvoice)   '-add invoice to new page-
                intLinesAvail = k_STATEMENT_DETAIL_LINES - intLines
            End If
        Next colInvoice
        '--save last page-
        mColStatementDetailPages.Add(colDetailPage)
        If (intLinesAvail >= k_STATEMENT_SUMMARY_LINES) Then
            mbStatementSummaryOnNewPage = False
        Else  '--needs new page for summary-
            mbStatementSummaryOnNewPage = True
        End If
        PrintStatement = True

        '==  v.3403.1102- 02-Nov-2017-=Statements-..-
        '==     >> Add Preview option for Statement.-.
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
                PrintStatement = True
            Catch ex As Exception
                MsgBox("Error in Print Statement preview.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                PrintStatement = False
            End Try  '-preview-

        Else  '-straight print.-
            Try
                '--  set printer selected..--
                printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
                '- For Microsoft pdf- Set file name..
                '=3411.0109= MS PDF.- msPrintToFileFullPath-
                If (msPrintToFileFullPath <> "") Then  '-printing to file..
                    '- Don't do this for adobe-
                    If (InStr(LCase(msSelectedPrinterName), "adobe") <= 0) Then '-no adobe-
                        '-- should be Microsoft pdf.
                        printDocument1.PrinterSettings.PrintToFile = True
                        printDocument1.PrinterSettings.PrintFileName = msPrintToFileFullPath
                    End If
                End If

                '-  for xps.  try and bypass SAVE dialog..
                '== If (LCase(VB.Right(sPrintFileName, 4)) = ".xps") Then
                '==     printDocument1.PrinterSettings.PrintFileName = sPrintFileName
                '==     With printDocument1
                '==     .DefaultPageSettings.PrinterSettings.PrintToFile = True
                '==     .DefaultPageSettings.PrinterSettings.PrintFileName = sPrintFileName
                '==     End With
                '== End If  '-xps=

                '--  start the printer..--
                printDocument1.Print()
                If bWaitForCompletion Then
                    '--3411.1117- WAIT for PDF to be made..-
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                    Dim intStart, intFinish As Integer
                    intStart = CInt(VB.Timer)
                    intFinish = intStart + 30
                    While (Not mbPrintingCompleted) And (CInt(VB.Timer) < intFinish)
                        Application.DoEvents()
                        System.Threading.Thread.Sleep(500)  '--milliseconds..-
                    End While
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    If (Not mbPrintingCompleted) Then
                        MsgBox("Printing not completed..", MsgBoxStyle.Exclamation)
                        PrintStatement = False
                        Exit Function
                    End If
                    '-- Print Completed may happen, but File still not finished.
                    '-- wait a bit, then test..
                    intStart = CInt(VB.Timer)
                    intFinish = intStart + 30
                    Do
                        Application.DoEvents()
                        System.Threading.Thread.Sleep(1500)  '--milliseconds..-
                    Loop Until My.Computer.FileSystem.FileExists(msPrintToFileFullPath) Or _
                                                                     (CInt(VB.Timer) >= intFinish)
                    If My.Computer.FileSystem.FileExists(msPrintToFileFullPath) Then
                        PrintStatement = True
                    End If
                End If  '-wait-
            Catch ex As Exception
                '== MessageBox.Show(ex.Message)
                MsgBox("Error in printing Statement." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                PrintStatement = False
            End Try
        End If  '-preview-
    End Function  '--PrintStatement-
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Debtors Report --
    '== NEW- 11-Oct-2019-
    '==  ADD PREVIEW OPTION to Print Debtors Report..
    '== NEW- 11-Oct-2019-
    '==  ADD PREVIEW OPTION to Print Debtors Report..
    '== NEW- 27-Oct-2019-
    '==  ADD SUB HEADING to Print Debtors Report..

    '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
    '==  TRANSFER to new Class..

    Public Function PrintDebtorsReport(ByRef colReportCustomers As Collection, _
                                        ByVal dateCutOff As Date, _
                                       ByRef SystemInfo As clsSystemInfo, _
                                       ByRef ImageUserLogo As Image, _
                                       ByVal sVersionPOS As String, _
                                       ByVal sSelectedPrinterName As String, _
                                       Optional ByVal bDebtorsReportSummaryOnly As Boolean = True, _
                                       Optional ByVal bPreviewOnly As Boolean = False, _
                                       Optional ByVal strSubHeading As String = "") As Boolean

        PrintDebtorsReport = False

        '==   Target-New-Build-4267 -- (Started 07-Sep-2020)
        '==
        '== == Debtors Report-   
        '==     Show Reversed Invoices only on detailed report, and only for current period..  
        '==     --  On Summary, DO NOT show customers with zero balance, even if thaey have reversed invoices in current period.
        '==
        '==      --  PLUS-  THIS IS the new class "clsDebtorsReport" to give report its own class.
        '==
        '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

        '==  TRANSFER to new Class..

        Dim clsPrint1 As clsDebtorsReport

        clsPrint1 = New clsDebtorsReport

        PrintDebtorsReport = clsPrint1.PrintDebtorsReport2(colReportCustomers, dateCutOff, _
                                                     SystemInfo, ImageUserLogo, sVersionPOS, _
                                                        sSelectedPrinterName, bDebtorsReportSummaryOnly, bPreviewOnly, strSubHeading)



        '== DONE  TRANSFER to new Class..
        Exit Function

        '--REST is obsolete



        'mbPrintingCompleted = False  '=3107.820-
        'mbPrintError = False
        'msPrintErrorMsg = ""

        'mColReportCustomers = colReportCustomers
        ''= crap= mColRefunds = mColReportCustomers.Item("Refunds")

        'mDateCutOff = dateCutOff
        ''--  SET static var to SAVE doc-type for page-event...--
        'mlPrintDocType = printDocType.DebtorsReport
        'miPageNo = 0
        'If (colReportCustomers Is Nothing) OrElse (colReportCustomers.Count <= 0) Then
        '    MsgBox("No Debtors Invoices to print..", MsgBoxStyle.Exclamation)
        '    Exit Function
        'End If
        'mSysInfo1 = SystemInfo  '= mSdSystemInfo = sdSystemInfo
        'mImageUserLogo = ImageUserLogo
        'msVersionPOS = sVersionPOS
        'msSelectedPrinterName = sSelectedPrinterName

        ''=3519.0224-
        'mbDebtorsReportSummaryOnly = bDebtorsReportSummaryOnly
        ''-4201.1027--- Debtors Report--
        'msDebtorsReportSubHeading = strSubHeading

        ''--  check and set printer..-
        'If (msSelectedPrinterName = "") Then
        '    MsgBox("Report printer not specified", MsgBoxStyle.Exclamation)
        '    Exit Function
        'End If
        'PrintDebtorsReport = True
        ''== NEW- 11-Oct-2019-
        ''==  ADD PREVIEW OPTION to Print Debtors Report..
        'If bPreviewOnly Then
        '    Try
        '        '--  set printer selected..--
        '        printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
        '        '--  start the printer..--
        '        '-  PREVIEW requested- 
        '        printPreviewDialog1.Document = printDocument1
        '        printPreviewDialog1.Height = 1200
        '        printPreviewDialog1.Width = 800
        '        printPreviewDialog1.PrintPreviewControl.UseAntiAlias = True
        '        '-4201.1027=
        '        printPreviewDialog1.PrintPreviewControl.Zoom = 0.95
        '        printPreviewDialog1.ShowDialog()
        '        PrintDebtorsReport = True
        '    Catch ex As Exception
        '        MsgBox("Error in Print Debtors preview.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        '        PrintDebtorsReport = False
        '    End Try  '-preview-
        'Else '-normal- straight print.
        '    Try
        '        '--  set printer selected..--
        '        printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
        '        '--  start the printer..--
        '        printDocument1.Print()
        '    Catch ex As Exception
        '        '== MessageBox.Show(ex.Message)
        '        MsgBox("Error in printing DebtorsReport." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        '        PrintDebtorsReport = False
        '    End Try
        'End If '-preview

    End Function  '-PrintDebtorsReport-
    '= = = = = = = = = = = = = = = = = == 


End Class '-clsPrintSaleDocs --

'=== end class ==


