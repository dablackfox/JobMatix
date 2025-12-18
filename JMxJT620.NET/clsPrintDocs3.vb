Option Strict Off
Option Explicit On

Imports System
Imports System.Drawing
Imports System.IO
Imports System.Drawing.Printing
Imports System.Drawing.Drawing2D
Imports VB = Microsoft.VisualBasic
Imports System.Collections.Generic

Friend Class clsPrintDocs
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
    '==  -- 3203.229= 29Feb. Jobs SA/SR HEADER- 
    '==      >>  FIXING TICKET- Job 10003 has last digit cut off..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '==
    '==  NEW VERSION 3.3.3311.410-  10-Apr-2016=
    '==      >> 3311.410/411=  No special Colour now for ON-SITE completed Job. (keep it Green).
    '==      >>  Delete RA's Functions ( see clsprintRAs.vb)..
    '==      >>  ON-SITE S/A.. Add boxes for Times Started/Finished...
    '==      >>  Maint S/R.. Reformat Labour/Parts Total Cost into box....
    '==
    '==
    '==  grh 3311.507-
    '==  ----------------------
    '==
    '==        >>- Fixes to Bug in Printing Job Service Report- 
    '==                   (was overflowing item barcodes at bottom of page)
    '==
    '==  NEW BUILD-
    '==    Updated- 3519.0129 29-Jan-2019= 
    '==           - Fixes to Print Customer Report for supporting multiple pages of images....
    '==
    '==
    '==  NEW BUILD- 4219 VERSION
    '==    Updated- 4219.1121 21-Nov-2019= 
    '==      -- clsPrintDocs- JobMaint Printing-  Fix Printing WorkHistory for Multiple Pages.
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '==
    '== Target-New-Build-6201 --  (15-July-2021) for Open Source..
    '==    -- Add CheckBox "Print Item Barcodes" to JobMaint Form (print section).
    '==    --  In clsPrintDocs, print the item barcode list only if requested.
    '= = = =
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    Const k_TABPRINTPRIORITY As Short = 20
	Const k_TABPRINTBRAND As Short = 26
	Const k_TABPRINTMODEL As Short = 40
	Const k_TABPRINTSERIAL As Short = 56

    Private Const K_GOODS_ONSITEJOB = "ON-SITE JOB;"

    Private Const K_LEFT_MARG = 36
    Private Const K_TOP_MARG = 12
    Private Const K_HEADER_BOTTOM = 120

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
        QuoteJob
        NewJob
        JobMaint
        JobMaintCustomerReport
        '= RAForm
        '= RAShippingLabel
    End Enum

    Private Enum textColour
        magenta
        black
        white
        DarkViolet
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
    Private mIntMaintPage As Short
    '==3311.507= Private mIntLineCount As Short
    Private mIntWtyLoop As Short
    Private mIntWtyCount As Short
    Private mIntMaintItemIx As Short
    Private mLngItemCount As Integer  '--count items printed..-

    '--  save doc-type currently being printed..-- 
    Private mlPrintDocType As printDocType '== = -1

    '--labels- 
    Private miLineCount As Integer = 0
    Private miPageNo As Integer = 0

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

    Private mbIsOnSiteJob As Boolean = False  '==3083==
    Private msOnSiteDate As String = ""       '==3083==
    Private msOnSiteTime As String = ""       '==3083==

    Private mLngJobNo As Integer = -1
    Private mbLicenceOK As Boolean = False
    Private mbJobReturned As Boolean = False
    Private mbSystemUnderWarranty As Boolean = False  '=3203.118=

    Private msJobStatus As String = ""
	
	'==Private mbQuotationRequired As Boolean
    Private mStrTermsText As String = ""
	'==Private mStrQuotationRequiredText As String
	'==Private mStrProceedWithServiceText As String
	
	Private mLngInstructionBGColour As Integer
	Private mStrInstructionLabel As String
    Private mStrInstructionText As String
    '-- 3041.1--
    Private mStrServiceChargesInfoText As String = ""
	
	Private mStrTicketDate As String
	Private mLngPriorityColour As Integer
    Private mCurLabourMinCharge As Decimal = 0
    Private mCurLabourHourlyRate As Decimal = 0
	'==Private mCurNotificationCostLimit As Currency
	
	Private mColCust As Collection
    Private msCustomerPrint As String = ""
	
	Private mColResultGoods As Collection
	Private mStrExtrasInCare As String
	Private mStrUserNames As String
	Private mStrSymptoms As String
	Private mStrProblem As String
	Private mStrDataBackup As String
	'= = = = = = = = = = = =  = = =  = =
	
	'--  Service form..
	Private mbQuotation As Boolean
	
    Private msItemBarcodeFontName As String = ""
    Private mlItemBarcodeFontSize As Integer = 9
	
	Private mColTasksCompleted As Collection
	Private msShowLabourCost As String
	
	Private mColServiceItems As Collection
	Private mColPartsItems As Collection
	
    Private mCurTotalItemValue As Decimal = 0 '--servce items and parts items.
    Private mCurTotalLabourValue As Decimal = 0
	
    Private msNotifications As String = ""
	Private msWorkHistory As String
	Private mObjPictureExtraPrint As Object

    '==Private mbIsOnSiteJob As Boolean = False

    '== Target-New-Build-6201 --  (15-July-2021) for Open Source..
    Private mbCanPrintJobItemBarcodes As Boolean = False
    '== END Target-New-Build-6201 --  (15-July-2021) for Open Source..

    '--  RA stuff.-

    Private mlRA_id As Integer = -1
    Private msSupplierRMA As String = ""
    Private msRAStatus As String = ""
	
	Private msSerialNo As String
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
	'= = = = = = = = = = = = = = = = =
	
	'-- Q U O T E  S T U F F --
	'-- Q U O T E  S T U F F --
	
    Private mlQuoteOrderId As Integer = -1
    Private mlQuoteNumberOfJobs As Integer = 0
    Private mlQuoteJobSequence As Integer = -1
    Private msModelChecklist As String = ""
    Private msJobItemsRequired As String = ""
    Private msQuoteInstructions As String = ""
    Private msQuoteFullJobList As String = ""
    '= = = = = = = = = = = = = = =


    '--  RECEIPT  stuff SAVED. ..--
    Private mColReportLines As Collection

    '--  Job Labels stuff SAVED. ..--
    Private mLngJobId, mIntNoLabels As Integer
    Private msCustomer As String
    Private mbIsRAlabel As Boolean = False

    '-- 3203- Item IMAGE saved--
    '-- FOR JOB or RA..--
    Private imageItem1 As Image

    '= 3203.110=
    '-- Job Maint CustomerReport- ( stuff SAVED. ..)-
    '== Private mColCustReportLines As Collection
    Private msWorkRecord As String = ""
    '== Private mDtPictures As DataTable    '-- Table of Attached Pics if any.-
    Private mListJobImages As List(Of Image)
    Private masFileTitles() As String

    Private msDeliveryFootNote As String = ""
    '-- remaining WorkNotes left to print..

    Private msReportTextRemaining As String = ""
    Private mIntReportImagesPrinted As Integer = 0

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

    '==3083== ONSITE Jobs..-
    WriteOnly Property IsOnSiteJob() As Boolean
        Set(ByVal value As Boolean)
            mbIsOnSiteJob = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = = = =

    '==3083== ONSITE Jobs..-
    WriteOnly Property OnSiteDate() As String
        Set(ByVal value As String)
            msOnSiteDate = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = = = =

    '==3083== ONSITE Jobs..-
    WriteOnly Property OnSiteTime() As String
        Set(ByVal value As String)
            msOnSiteTime = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = = = =

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

    WriteOnly Property JobReturned() As Boolean
        Set(ByVal Value As Boolean)
            mbJobReturned = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    '=3203.118=
    WriteOnly Property SystemUnderWarranty() As Boolean
        Set(ByVal Value As Boolean)
            mbSystemUnderWarranty = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property JobStatus() As String
        Set(ByVal Value As String)
            msJobStatus = Value
        End Set
    End Property

    '==Property Let QuotationRequired(b1 As Boolean)
    '==     Let mbQuotationRequired = b1
    '==End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property TermsText() As String
        Set(ByVal Value As String)
            mStrTermsText = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property InstructionBGColour() As Integer
        Set(ByVal Value As Integer)
            mLngInstructionBGColour = Value
        End Set
    End Property
    '= = = = = = = = = = = = = =

    WriteOnly Property InstructionLabel() As String
        Set(ByVal Value As String)
            mStrInstructionLabel = Value
        End Set
    End Property
    '= = = = = = = = = = = = =  = = =

     WriteOnly Property InstructionText() As String
        Set(ByVal Value As String)
            mStrInstructionText = Value
        End Set
    End Property
    '= = = = = = = = = = = = =  = = =

    WriteOnly Property ServiceChargesInfoText() As String
        Set(ByVal Value As String)
            mStrServiceChargesInfoText = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = =

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

    WriteOnly Property LabourMinCharge() As Decimal
        Set(ByVal Value As Decimal)
            mCurLabourMinCharge = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property LabourHourlyRate() As Decimal
        Set(ByVal Value As Decimal)
            mCurLabourHourlyRate = Value
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

    WriteOnly Property ResultGoods() As Collection
        Set(ByVal Value As Collection)
            mColResultGoods = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property ExtrasInCare() As String
        Set(ByVal Value As String)
            mStrExtrasInCare = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property UserNames() As String
        Set(ByVal Value As String)
            mStrUserNames = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property Symptoms() As String
        Set(ByVal Value As String)
            mStrSymptoms = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property Problem() As String
        Set(ByVal Value As String)
            mStrProblem = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property DataBackup() As String
        Set(ByVal Value As String)
            mStrDataBackup = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =


    '-- Service Record stuff..--
    '-- Service Record stuff..--

    WriteOnly Property IsQuotation() As Boolean
        Set(ByVal Value As Boolean)
            mbQuotation = Value
        End Set
    End Property
    '= = = = = = = = = = =

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

    WriteOnly Property TasksCompleted() As Collection
        Set(ByVal Value As Collection)
            mColTasksCompleted = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property ShowLabourCost() As String
        Set(ByVal Value As String)

            msShowLabourCost = Value
        End Set
    End Property
    '= = = = = = = = = = =

    WriteOnly Property ServiceItems() As Collection
        Set(ByVal Value As Collection)
            mColServiceItems = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    WriteOnly Property PartsItems() As Collection
        Set(ByVal Value As Collection)
            mColPartsItems = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =

    '== Target-New-Build-6201 --  (15-July-2021) for Open Source..
    '= Private mbCanPrintJobItemBarcodes As Boolean = False

    WriteOnly Property CanPrintJobItemBarcodes() As Boolean
        Set(ByVal Value As Boolean)
            mbCanPrintJobItemBarcodes = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =



    '== END Target-New-Build-6201 --  (15-July-2021) for Open Source..


    WriteOnly Property TotalLabourValue() As Decimal
        Set(ByVal Value As Decimal)

            mCurTotalLabourValue = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = =

    '- all items.. service items and parts items.

    WriteOnly Property TotalItemValue() As Decimal
        Set(ByVal Value As Decimal)

            mCurTotalItemValue = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = =

    WriteOnly Property Notifications() As String
        Set(ByVal Value As String)

            msNotifications = Value
        End Set
    End Property
    '= = = = = = = = = = = = =

    WriteOnly Property WorkHistory() As String
        Set(ByVal Value As String)

            msWorkHistory = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = =

    '-- EXTRA picture object..-
    WriteOnly Property ExtraPicture() As Object
        Set(ByVal Value As Object)
            mObjPictureExtraPrint = Value
        End Set
    End Property
    '= = = = = = = = = = = =  = = =  = =
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

    '-- Q U O T E  S T U F F --
    '-- Q U O T E  S T U F F --

    WriteOnly Property QuoteOrderId() As Integer
        Set(ByVal Value As Integer)
            mlQuoteOrderId = Value
        End Set
    End Property
    '= = = = = = = = =

    WriteOnly Property QuoteNumberOfJobs() As Integer
        Set(ByVal Value As Integer)

            mlQuoteNumberOfJobs = Value
        End Set
    End Property '--no of jobs..-
    '= = = = = = = = = = = = =

    WriteOnly Property QuoteJobSequence() As Integer
        Set(ByVal Value As Integer)

            mlQuoteJobSequence = Value
        End Set
    End Property '--no of jobs..-
    '= = = = = = = = = = = = =

    WriteOnly Property ModelChecklist() As String
        Set(ByVal Value As String)
            msModelChecklist = Value
        End Set
    End Property

    WriteOnly Property JobItemsRequired() As String
        Set(ByVal Value As String)
            msJobItemsRequired = Value
        End Set
    End Property
    '= = = = = = = = = = = = = =

    WriteOnly Property QuoteInstructions() As String
        Set(ByVal Value As String)

            msQuoteInstructions = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = =

    WriteOnly Property QuoteFullJobList() As String
        Set(ByVal Value As String)

            msQuoteFullJobList = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- initialize --
    '-- initialize --

    'UPGRADE_NOTE: class_initialize was upgraded to class_initialize_Renamed. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'

    Private Sub Class_Initialize_Renamed()

 
        mlPrtWidth = 748  '-3203.229-..-

        '--  set default font for PrintText in box..--
        With mDefaultUserFont
            .sName = "Lucida Sans Unicode"  '==3519.0130-    "Tahoma"
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
                                   Optional ByVal bDepthTestingOnly As Boolean = False) As Integer

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

        sFontName = mDefaultUserFont.sName
        lngFontSize = mDefaultUserFont.lngSize

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

    '--  Print NewJob/Service header.--

    Private Function mbPrintJobHeader(ByVal ev As PrintPageEventArgs, _
                                     ByVal sHdr1 As String, _
                                      ByVal lngTicketColour As Integer, _
                               Optional ByVal sStatus As String = "", _
                               Optional ByVal bPrintTicketBox As Boolean = True) As Boolean
        Dim lngTicketX, lngTicketY As Integer
        Dim lngTicketWidth, lngTicketDepth As Integer
        Dim lngTxtHt, lngTxtWidth As Integer
        Dim lngHdrX, lngHdrY As Integer
        Dim lngXpos, lngYpos, intYpos2 As Integer
        Dim lngLeftMarg As Integer
        Dim L1, lngError As Integer
        Dim s1, s2 As String
        Dim font1 As userFontDef

        On Error GoTo PrintJobHdr_Error
        lngLeftMarg = K_LEFT_MARG  '= (16 * PRT_UNIT) '- 240 twips..-
        '--  paint BIZ logo  top left.--
        If Not (mObjUserLogo Is Nothing) Then
            ev.Graphics.DrawImage(mObjUserLogo, lngLeftMarg, K_TOP_MARG)
        End If
        '-- print main title..-
        lngHdrX = K_LEFT_MARG + 148 '= 186  '=(146 * PRT_UNIT) 
        lngTicketX = K_LEFT_MARG + 584  '=3203.219= 596 '= 616  '= (616 * PRT_UNIT) '-- 8600 twips..-
        lngTicketY = K_TOP_MARG
        lngTicketWidth = 164  '= mlPrtDx(160) '== 2200 twips..-
        lngTicketDepth = mlPrtDx(93) '==  1395 twips.-

        font1.sName = "Lucida Sans" '== Printer.FontName = "Tahoma"
        font1.lngSize = 16 '==Printer.FontSize = 18
        font1.bBold = False '== Printer.Font.Bold = False '--reset to normal IN CASE LEFT OVER..-
        font1.bUnderline = False '= Printer.Font.Underline = False
        font1.bItalic = False
        lngHdrY = K_TOP_MARG + 8
        '--  20 pixels.. 300twips- '--down 1/3 title height from top..-
        lngYpos = mlPrintTextString(ev, sHdr1, lngHdrX, lngHdrY, font1)

        '--next line is address..-
        font1.lngSize = 8 '== Printer.FontSize = 8
        font1.bBold = True '== Printer.FontBold = True

        lngXpos = lngHdrX '==  Printer.CurrentX = lngHdrX
        If Not (mColBusiness Is Nothing) Then
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessName"), lngHdrX, lngYpos, font1)
            If mColBusiness.Contains("BusinessABN") Then
                lngYpos = mlPrintTextString(ev, "ABN: " & mColBusiness.Item("BusinessABN"), lngHdrX, lngYpos, font1)
            End If
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessAddress1"), lngHdrX, lngYpos, font1)
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessAddress2") & " " & _
                                              mColBusiness.Item("BusinessState") & " " & _
                                                    mColBusiness.Item("BusinessPostCode"), lngHdrX, lngYpos, font1)
            '==lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessState") & "  " & _
            '==                               mColBusiness.Item("BusinessPostCode"), lngHdrX, lngYpos, font1)
            lngYpos = mlPrintTextString(ev, "Email: " & mColBusiness.Item("BusinessEmail"), lngHdrX, lngYpos, font1)
        End If '--nothing-
        lngYpos = mlPrintTextString(ev, "", lngHdrX, lngYpos, font1)  '--blank line.
        font1.bBold = False
        '-- find bottom of header stuff...
        If ((lngTicketY + K_TOP_MARG) > lngYpos) Then
            lngYpos = (lngTicketY + K_TOP_MARG)
        End If
        L1 = lngYpos   '--save for status..-
        lngYpos = mlPrintTextString(ev, mStrHeaderDate, lngLeftMarg, lngYpos, font1)
        '--status on same line..-
        font1.bBold = True
        If sStatus <> "" Then
            Call mlPrintTextString(ev, "Status: " & sStatus, lngTicketX, L1, font1)
        End If

        If bPrintTicketBox Then
            Dim colorTicketFG As textColour = textColour.black
            If mbSystemUnderWarranty Then colorTicketFG = textColour.white
            '--draw box for ticket.-
            Call mlDrawBox(ev, lngTicketX, lngTicketY, lngTicketWidth, lngTicketDepth, lngTicketColour)
            '--print ticket no..-
            '== font1.sName = "Courier New" '== Printer.FontName = "Courier New"
            font1.sName = "Lucida Sans"  '-now with comma- Courier no good..
            font1.lngSize = 30
            font1.bBold = True
            '==intYpos2 = mlPrintTextString(ev, mStrTicketDate, lngTicketX, lngTicketY, font1)
            '==3203.229= s1 = VB6.Format(mLngJobNo, "000000") '== works ok with full 6-digits !! =
            s2 = VB6.Format(mLngJobNo, "###,000") '== works ok with full 6-digits !! =
            s1 = RSet(s2, 7)  '--ticket is actually ony 6-digits wide at 30-point.
            '=3203.18= Job No now on TOP.-
            '- Backtrack 1/2-digit from start of Job Ticket to allow for some right margin..
            intYpos2 = mlPrintTextString(ev, s1, lngTicketX - 6, lngTicketY, font1, colorTicketFG)
            font1.lngSize = 22 '== Printer.FontSize = 30
            '--3203.118- Make year yy into YYYY --
            '= mStrTicketDate = Replace(mStrTicketDate, "-", "- ")  '-add 1 space for lucida.
            intYpos2 = mlPrintTextString(ev, Replace(mStrTicketDate, "-", "- "), lngTicketX, intYpos2, font1, colorTicketFG)
        End If
        '--  status-
        font1.sName = "Lucida Sans"
        font1.lngSize = 8

        '-- end of header..-
        mIntMainDetailTop = lngYpos + 8
        '--  main line..--1800 twips down from top..-
        Call mlDrawLine(ev, lngLeftMarg, lngYpos, mlPrtWidth)
        mbPrintJobHeader = True
        Exit Function

PrintJobHdr_Error:

        lngError = Err().Number
        MsgBox("!! ERROR in PrintJobHdr." & vbCrLf & "Error code: " & lngError & " = " & ErrorToString(lngError), MsgBoxStyle.Exclamation)
        mbPrintJobHeader = False

    End Function '--PrintJobHeader-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  PRINT RA Header..---
    '--  GONE to clsPrintRAs-..---

    '=3311.411= Private Function mbPrintRA_Header(ByVal ev As PrintPageEventArgs, _
    '=3311.411=                                 ByVal sHdr1 As String, _
    '=3311.411=                                   ByVal lngTicketColour As Integer, _
    '=3311.411=                                    Optional ByVal sStatus As String = "") As Boolean
    '=3311.411= End Function '--RA header..--
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '==   ENCODE GoodsInCare  DB string..--
    '----  FROM collection of item collections..==
    Private Function mbEncodeGoodsIncare(ByRef colGoods As Collection, _
                                         ByRef sResultGoods As String, _
                                            ByRef sPrintGoods As String) As Boolean
        '===Const k_TABPRINTBRAND = 34
        '===Const k_TABPRINTMODEL = 52

        Dim L1 As Integer
        Dim sResult As String
        Dim sPrintResult As String
        Dim colItem1 As Collection
        Dim sType As String
        Dim sBrand As String
        Dim sModel As String
        Dim sSerial As String

        mbEncodeGoodsIncare = False
        sResult = ""
        sPrintResult = ""
        If Not (colGoods Is Nothing) Then
            For Each colItem1 In colGoods
                sType = colItem1.Item("Type")
                sBrand = colItem1.Item("Brand")
                sModel = colItem1.Item("Model")
                sSerial = colItem1.Item("SerialNo")

                If (sResult <> "") Then sResult = sResult & vbCrLf
                If (sPrintResult <> "") Then sPrintResult = sPrintResult & vbCrLf
                sResult = sResult & sType & vbTab & sBrand & vbTab & sModel & vbTab & sSerial
                sType = Replace(sType, " ", "-")
                sBrand = Replace(sBrand, " ", "-")
                sModel = Replace(sModel, " ", "-")
                sSerial = Replace(sSerial, " ", "-")
                Dim sSep1 As String = " | "
                '== sPrintResult = sPrintResult & sType & vbTab & VB6.Format(k_TABPRINTBRAND, "000") & sBrand & vbTab & _
                '==           VB6.Format(k_TABPRINTMODEL, "000") & sModel & vbTab & VB6.Format(k_TABPRINTSERIAL, "000") & sSerial
                sPrintResult = sPrintResult & sType & sSep1 & sBrand & sSep1 & sModel & sSep1 & sSerial
            Next colItem1 '--item1..-
        End If '--nothing..-
        sResultGoods = sResult
        sPrintGoods = sPrintResult
        mbEncodeGoodsIncare = True
        Exit Function

EncodeGoodsIncare_Error:
        L1 = Err().Number
        MsgBox("Runtime Error in ENCODE Goods function.." & vbCrLf & "Error is " & L1 & " = " & ErrorToString(L1))
    End Function '--encode..-
    '= = = = = = = = = = =
    '= = = = = = =  =  = =
    '-===FF->

    '== Page Header for Service Record --
    '--     (Items/barcodes pages only..) ==
    '== Page Header for Service Record ==
    '--     (Items/barcodes pages only..) ==

    Private Function mlJobMaintPageHeader(ByVal ev As PrintPageEventArgs, _
                                          ByVal intPage As Integer, _
                                          ByRef lngTotalNoItems As Integer) As Integer
        Dim sHdr0 As String
        Dim sHdr1 As String
        Dim lngXpos, lngYpos As Integer
        Dim lngHdrX, lngTxtHt As Integer
        Dim lngBarcodeX, lngBarcodeY As Integer
        Dim font1 As userFontDef

        '=== Printer.NewPage()
        '== intPage = intPage + 1
        lngHdrX = (64 * PRT_UNIT) '-- 960 twips..-
        With font1
            .sName = "Tahoma"
            .lngSize = 8
            .bBold = True
            .bUnderline = False
            .bItalic = False
        End With

        lngXpos = lngHdrX '= Printer.CurrentX = lngHdX
        lngYpos = K_TOP_MARG  '==  Printer.CurrentY = lngTxtHt / 2  '--down 1/2 title height from top..-
        '==Printer.Print mColBusiness("BusinessName"); Tab(50); FormatDateTime(Date, vbLongDate); Tab(120); "Page: " & intPage
        sHdr0 = mColBusiness.Item("BusinessName") & Space(30) & _
                                       FormatDateTime(Today, DateFormat.LongDate) & Space(50) & "Page: " & intPage
        '== Printer.Print sHdr0
        lngYpos = mlPrintTextString(ev, sHdr0, lngXpos, lngYpos, font1)
        lngBarcodeY = lngYpos '--save for barcode..-

        sHdr1 = "Items supplied " & "(" & lngTotalNoItems & _
                                        " in total)-         For Job No: " & VB6.Format(mLngJobNo, "000") & ". "
        With font1
            .lngSize = 16
            .bBold = False
        End With
        lngXpos = lngHdrX
        lngYpos = mlPrintTextString(ev, sHdr1, lngXpos, lngYpos, font1)
        '--  font size now set..

        lngBarcodeX = mlGetTextWidth(ev, sHdr1, font1) + 100   '--  getTextWidth a bit dodgy..-

        font1.sName = msItemBarcodeFontName '==  Printer.FontName = msItemBarcodeFontName
        font1.lngSize = mlItemBarcodeFontSize '== Printer.FontSize = mlItemBarcodeFontSize
        '== Printer.Print "*" & mLngJobNo & "*"
        lngYpos = mlPrintTextString(ev, "*" & mLngJobNo & "*", lngBarcodeX, lngBarcodeY, font1)

        With font1
            .sName = "Tahoma"
            .lngSize = 10
            .bBold = True
            .bUnderline = False
            .bItalic = False
        End With
        '--next line

        '--customer on first page only..-
        '== If intPage = 1 Then
        lngXpos = lngHdrX
        lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)  '-- blank line..-
        lngYpos = mlPrintTextString(ev, "Customer: " & msCustomerPrint & ".  ", lngXpos, lngYpos, font1)

        '==    Printer.CurrentX = lngHdrX
        font1.bBold = False
        font1.sName = msItemBarcodeFontName '==  Printer.FontName = msItemBarcodeFontName
        font1.lngSize = mlItemBarcodeFontSize '== Printer.FontSize = mlItemBarcodeFontSize
        lngYpos = mlPrintTextString(ev, "*" & mColCust.Item("CustomerBarcode") & "*", lngXpos, lngYpos, font1)
        '== End If '--page 1..-

        With font1
            .sName = "Tahoma"
            .lngSize = 8
            .bBold = True
            .bUnderline = False
            .bItalic = False
        End With

        '--blank line..--
        lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)
        '--  Return current Ypos..--
        mlJobMaintPageHeader = lngYpos
    End Function '--Header page.-
    '= = = = = = = = = = = = =
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

    '--  PRINT Receipt-lines collection..--
    '---  must FOLLOW "BuildReceipt" in time..--

    Private Function mbPrintReceiptLines(ByVal ev As PrintPageEventArgs, _
                                         ByRef colLines As Collection, _
                                         ByVal intLineSize As Integer, _
                                         ByRef lngXpos As Integer, _
                                         ByRef lngYpos As Integer, _
                                         ByRef font1 As userFontDef) As Boolean
        Dim sRem1, sRem2 As String
        Dim s1 As String
        Dim sChunk As String
        Dim ix, iPos, L1 As Integer
        Dim lngLeftMarg As Long
        '== Dim lngXpos, lngYpos As Integer
        Dim sLine As String
        Dim vline As Object
        '= Dim font1 As userFontDef

        With font1
            .sName = "Tahoma"
            .lngSize = 8
            .bBold = False
            .bUnderline = False
            .bItalic = False
        End With

        '-- Note: Markups eg: <big> occupy one collection item each..--
        For Each vline In colLines
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
            ElseIf Left(sRem1, 8) = "<times>" Then
                font1.sName = "Times New Roman" '== Printer.Font.Name = "Times New Roman"
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
                    '--Inner loop..-  break rem2 into 40 char lines--
                    '--max 40 chars per line..-
                    '-- Make line chunks for longer lines..-
                    While (Len(sRem2) > 0)
                        If (Len(sRem2) > intLineSize) Then '--break line close to 40..-
                            '--find last space or punctuation..-
                            sChunk = Left(sRem2, intLineSize) '--try max..-
                            If (Right(sChunk, 1) <> " ") And (Right(sChunk, 1) <> ";") Then '--find break--
                                iPos = InStrRev(sChunk, " ")
                                If iPos < (intLineSize \ 2) Then iPos = InStrRev(sChunk, ";") '--don't break under half..-
                                If (iPos >= (intLineSize \ 2)) Then '--break
                                    sChunk = Left(sRem2, iPos)
                                End If
                            End If '--break..-
                            s1 = sChunk
                            sRem2 = Mid(sRem2, Len(sChunk) + 1)
                        Else
                            s1 = sRem2 : sRem2 = "" '--last chunk-
                        End If '--break..-
                        '== Printer.Print Replace(s1, "^", " ") '--print current chunk..-
                        '--print current chunk..-
                        lngYpos = mlPrintTextString(ev, Replace(s1, "^", " "), lngXpos, lngYpos, font1)
                    End While '--rem2-
                End While '--sRem1..-
                font1.bBold = False '== Printer.Font.Bold = False '--reset to normal..-
                font1.bUnderline = False

                '----iLines = iLines + 1       '--counts data lines..-
                '==Printer.Font.Name = "Tahoma"  '== "Verdana"  '-- "Times New Roman"  '--  "Lucida Console"
                font1.sName = "Tahoma" '==  Printer.Font.Name = "Tahoma"
                font1.lngSize = 8 '==  Printer.FontSize = 8
            End If '--bold--
        Next vline '--line--

    End Function  '-PrintReceiptLines-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Build text to print Service and Parts Items..
    '--  Collections for Service (mColServiceItems) and Parts (mColPartsItems) must be populated.

    Private Function msBuildSuppliedItemsText() As String
        Dim sText As String = ""
        Dim s1, sSell, sWty As String
        Dim col1 As Collection
        Dim ix As Integer

        '=3311.411= '-- add labour cost, if any..--
        '=3311.411= sText &= msShowLabourCost & vbCrLf

        '--parts..--
        '--  AND SERVICE CHARGES..--
        If (mColServiceItems.Count() > 0) Then
            sText &= "<ul><b>Service Items Supplied: (" & mColServiceItems.Count() & ")" & vbCrLf
            ix = 0
            For Each col1 In mColServiceItems
                ix = ix + 1
                sSell = col1.Item("Sell-Price")
                sText &= CStr(ix) & ". " & col1.Item("Cat1") & ": " & _
                          col1.Item("Description") & " ^^(" & col1.Item("Barcode") & ") " & " ^^-Sell:" & sSell & "="
                sText &= vbCrLf
            Next col1
            sText &= vbCrLf
        Else '--no service.
            sText &= "<ul><b>NO Service Item was Supplied.." & vbCrLf & vbCrLf
        End If '-service count..-

        '--parts..--
        '-- Show Parts..-
        If (mColPartsItems.Count() > 0) Then
            sText &= "<ul><b>Parts Supplied: (" & mColPartsItems.Count() & ")" & vbCrLf
            ix = 0
            For Each col1 In mColPartsItems
                ix = ix + 1
                sSell = col1.Item("Sell-Price")
                s1 = col1.Item("Serial-No")
                sWty = col1.Item("Wty")
                sText &= CStr(ix) & ". " & col1.Item("Cat1") & ": " & _
                          col1.Item("Description") & " ^^(" & col1.Item("Barcode") & ") " & _
                             IIf((s1 <> ""), " ^^[SerNo:" & s1 & "]", "") & _
                                  IIf((sWty = "W"), " ^^*WARRANTY*", "") & " ^^-Sell:" & sSell & "="
                If mbQuotation Then
                    '--  Flag items extra to quote..-
                    If LCase(col1.Item("SmallIcon")) = "alert" Then
                        sText &= " [++]"
                    End If
                    '== sText = sText & " -" + col1.Item("SmallIcon") + "-"
                End If
                sText &= vbCrLf
            Next col1
            sText &= vbCrLf & vbCrLf
        Else '--no part.
            sText &= "<ul><b>NO Stock Part Item was Supplied.." & vbCrLf & vbCrLf
        End If '-parts count..-

        '=3311.411= s1 = FormatCurrency(mCurTotalItemValue, 2)
        '=3311.411= sText &= "<b> = Total Item Value (Service+Parts): " & s1 & vbCrLf
        '=3311.411= s1 = FormatCurrency((mCurTotalLabourValue + mCurTotalItemValue), 2)
        '=3311.411= sText &= "<b><ul> = Total Cost (Labour+Items): " & s1 & vbCrLf

        msBuildSuppliedItemsText = sText

    End Function  '-msBuildSuppliedItemsText--
    '= = = = = = = = = = = = = = = = = = =
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
            usercolor1 = textColour.DarkViolet
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

        miPageNo += 1

        '-- Check to see if more pages are to be printed.
        ev.HasMorePages = (miPageNo < mIntNoLabels)

    End Function '==PrintJobLabels=
    '= = = = = = = = = = = = =
    '-===FF->

    '-- PAGE EVENT for Print Job Labels..--

    '--  PRINT Receipt-lines collection..--
    '---  must FOLLOW "BuildReceipt" in time..--

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
        '==Set Printer = mPrtReceipt '-- set receipt printer--
        '== If mPrtSelected Is Nothing Then Exit Function
        '-- SET PRINTER..-
        '--   == Caller must set printer property first.. ==-
        '== Printer = mPrtSelected '-- set receipt printer--

        lngLeftMarg = (16 * PRT_UNIT) '- 240 twips..-
        If Not (mObjUserLogo Is Nothing) Then
            '== Printer.PaintPicture(mObjUserLogo, 240, 0)
            ev.Graphics.DrawImage(mObjUserLogo, lngLeftMarg, 0)
        End If
        lngXpos = (3 * PRT_UNIT) '--45 twips..-
        lngYpos = 0
        With font1
            .sName = "Tahoma"
            .lngSize = 8
            .bBold = False
            .bUnderline = False
            .bItalic = False
        End With
        '--  Feed down past logo..--
        For ix = 1 To 5
            '==  Printer.Print ""
            lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)
        Next ix
        System.Windows.Forms.Application.DoEvents()
        '--Printer.Font.Name = "Courier New"
        '-- Note: Markups eg: <big> occupy one collection item each..--

        Call mbPrintReceiptLines(ev, mColReportLines, 43, lngXpos, lngYpos, font1)
        '-- Note: Markups eg: <big> occupy one collection item each..--

        '-- lngYpos was updated..
        font1.sName = "Tahoma" '=  Printer.FontName = "Tahoma"
        font1.lngSize = 7 '== Printer.FontSize = 7
        '==Printer.Print mStrVersion
        lngYpos = mlPrintTextString(ev, mStrVersion, lngXpos, lngYpos, font1)

        '--  push receipt up..-
        For ix = 1 To 24
            '== Printer.Print ""
            lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)
        Next ix

        lngYpos = mlPrintTextString(ev, "...", lngXpos, lngYpos, font1)
        '--LabWait.Visible = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        System.Windows.Forms.Application.DoEvents()

        mbPrintReceipt_PageEvent = True
        '--only one page..-
        ev.HasMorePages = False
        Exit Function

PrintReceipt_error:
        L1 = Err().Number
        MsgBox("Runtime Error in Print-Receipt.." & vbCrLf & "Error=" & L1 & ": " & ErrorToString(L1), MsgBoxStyle.Critical)
    End Function '--print receipt..-
    '= = = = = = =  = = = =  = = =
    '-===FF->

    '--  PRINT PAGE EVENT..  
    '---Print the Quote Job form.--
    '-- Print the Quote Job form.--

    Private Function mbPrintQuoteJob_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean

        Dim sHdr1, sText As String
        Dim s1, sAddress1 As String
        Dim sRem As String
        Dim sCustText As String
        Dim lngTxtHt, lngTxtWidth As Integer
        Dim kx, iPos, ix, L1 As Integer
        Dim lngError, lngLeftMarg As Integer
        Dim lngTicketX, lngTicketY As Integer
        Dim lngTicketWidth, lngTicketDepth As Integer
        Dim lngBoxMargin As Integer
        Dim lngMainTop As String
        Dim lngTermsX, lngTermsY As Integer
        Dim lngLeftColWidth, lngRightColWidth As Integer
        Dim lngSigX, lngSigY As Integer
        Dim lngXpos, lngYpos As Integer
        Dim lngDepth As String
        Dim lngRHSX As Integer
        Dim lngHdrX, lngHdrY As Integer
        Dim sTermsText As String
        Dim sShortName As String
        Dim font1 As userFontDef
        Dim savedFont As userFontDef

        '--  get terms text..-
        On Error GoTo PrintQuoteJob_Error
        '==== sShortName = Replace(msBusinessShortName, "_", " ") '--remove underscores.--
        mbPrintQuoteJob_PageEvent = False
        lngLeftMarg = mlPrtDx(16) '- 240 twips..-
        lngHdrX = mlPrtDx(147) '-  2200

        '--  paint BIZ logo  top left.--
        '=== Printer.PaintPicture Me.Picture2.Picture, 240, 0
        If Not (mObjUserLogo Is Nothing) Then
            '== Printer.PaintPicture(mObjUserLogo, lngLeftMarg, 0)
            ev.Graphics.DrawImage(mObjUserLogo, lngLeftMarg, 0)
        End If

        lngTicketX = mlPrtDx(447) '-  6700   '--8600
        lngTicketY = 0
        lngTicketWidth = mlPrtDx(293) '== QUOTE 4400 twips..-
        lngTicketDepth = mlPrtDx(180) '== QUOTE 2700 twips.-

        lngTermsX = mlPrtDx(16) '- 240
        lngTermsY = mlPrtDx(800) '-  12000

        lngMainTop = CStr(mlPrtDx(227)) '== 3400 twips..-
        lngBoxMargin = mlPrtDx(20) '== 300 twips..-

        '== lngRHSX = lngTermsX + 5700
        lngLeftColWidth = mlPrtDx(340) '== 5100 twips..-
        lngRHSX = lngTermsX + lngLeftColWidth + mlPrtDx(20) '== +300=   5700 twips..-
        lngRightColWidth = mlPrtWidth - lngRHSX

        '-- print main title..-
        '== sHdr1 = "Quotation Build Job "
        '-- print main title..-
        sHdr1 = "Quotation Build Job "
        font1.sName = "Tahoma" '== Printer.FontName = "Tahoma"
        font1.lngSize = 18 '==Printer.FontSize = 18
        font1.bBold = False '== Printer.Font.Bold = False '--reset to normal IN CASE LEFT OVER..-
        font1.bUnderline = False '= Printer.Font.Underline = False
        font1.bItalic = False
        lngHdrY = mlPrtDx(12) '--  12 pixels.. 180twips- '--down 1/3 title height from top..-
        lngYpos = mlPrintTextString(ev, sHdr1, lngHdrX, lngHdrY, font1)

        '--  second line of hdr1.-
        '== Printer.CurrentX = lngHdrX
        '== sHdr1 = "(#" & lngJobSequence & " of " & mlNoJobs & ")"
        '== Printer.Print sHdr1
        '== Printer.Print

        '--  second line of hdr1.-
        sHdr1 = "(#" & mlQuoteJobSequence & " of " & mlQuoteNumberOfJobs & " job(s)..  )"
        lngYpos = mlPrintTextString(ev, sHdr1, lngHdrX, lngYpos, font1)
        lngYpos = mlPrintTextString(ev, "--", lngHdrX, lngYpos, font1) '--newline..-

        '--  sub-heading.--
        font1.lngSize = 14
        sHdr1 = "Quote (SalesOrder) No: " & mlQuoteOrderId
        lngYpos = mlPrintTextString(ev, sHdr1, lngHdrX, lngYpos, font1)

        '--next line is BIZ name/address..-
        font1.lngSize = 8 '== Printer.FontSize = 8
        font1.bBold = True '== Printer.FontBold = True
        '--next line is BIZ name/address..-
        If Not (mColBusiness Is Nothing) Then
            '==s1 = mColBusiness("BusinessName")
            '==Printer.Print IIf((s1 <> ""), s1, "JobTracking Business")
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessName"), lngHdrX, lngYpos, font1)
            '==Printer.Print mColBusiness("BusinessAddress1")
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessAddress1"), lngHdrX, lngYpos, font1)
            '==Printer.Print mColBusiness("BusinessAddress2")
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessAddress2"), lngHdrX, lngYpos, font1)
        End If '--nothing-
        font1.bBold = False
        '==lngYpos = mlPrintTextString("--", lngHdrX, lngYpos, font1)
        '--  dates.--
        lngYpos = mlPrintTextString(ev, mStrHeaderDate, lngHdrX, lngYpos, font1)
        lngYpos = mlPrintTextString(ev, mStrHeaderDate2, lngHdrX, lngYpos, font1)

        '--draw box for ticket.-
        lngYpos = mlDrawBox(ev, lngTicketX, lngTicketY, lngTicketWidth, lngTicketDepth, mLngPriorityColour)
        '--print ticket no..-
        font1.sName = "Courier New" '== Printer.FontName = "Courier New"
        font1.lngSize = 56 '== Printer.FontSize = 30
        font1.bBold = True '== Printer.FontBold = True
        lngYpos = mlPrintTextString(ev, mStrTicketDate, lngTicketX, lngTicketY, font1)
        s1 = VB6.Format(mLngJobNo, "  000") '== Format(mlJobId, "  000")
        lngYpos = mlPrintTextString(ev, s1, lngTicketX, lngYpos, font1)

        '-- end of header..-
        L1 = (207 * PRT_UNIT) '-  3100  '--ypos of line.-
        '== Printer.Line((lngLeftMarg, L1) - (mlPrtWidth, L1)) '--main line..--
        Call mlDrawLine(ev, lngLeftMarg, L1, mlPrtWidth)

        '--  customer Info..--
        '--  customer Info..--
        sCustText = "<b>Customer: " & mColCust.Item("CustomerBarcode") & "." & vbCrLf & _
                       mColCust.Item("CustomerName") & vbCrLf & mColCust.Item("CustomerCompany") & vbCrLf
        sCustText = sCustText & _
                     mColCust.Item("CustomerPhone") & ";   Mob: " & mColCust.Item("CustomerMobile") & vbCrLf & vbCrLf
        sCustText = sCustText & "<b>Nom. Tech:" & vbCrLf & mColCust.Item("CustomerTechName") & vbCrLf
        '--  NOW..   PRINT  CUST text..
        lngDepth = CStr(mlPrtDx(160)) '-  2400 '--cust box depth..
        Call mlPrintTextInBox(ev, sCustText, lngTermsX, CInt(lngMainTop), lngBoxMargin, lngLeftColWidth, CInt(lngDepth), True)


        '-- LHS  Checklist Info...--
        '-- LHS  Checklist Info...--

        '==  THIS -> lngYpos = lngMainTop + lngDepth + mlPrtDx(12)  '--move down to Job box..-
        '==    PRODUCES result of  lngYpos = 34Million plus..
        lngYpos = CInt(lngMainTop)
        lngYpos = lngYpos + CDbl(lngDepth)
        L1 = mlPrtDx(12)
        lngYpos = lngYpos + L1
        lngDepth = CStr(mlPrtDx(380)) '-  5700 '--checklist box depth..
        sText = "<b>Job Requirements:" & vbCrLf & vbCrLf
        sText = sText & "<ul>Model Checklist:" & vbCrLf & vbCrLf & msModelChecklist
        Call mlPrintTextInBox(ev, sText, lngTermsX, lngYpos, lngBoxMargin, lngLeftColWidth, CInt(lngDepth), True)

        '-- RHS Print list of assigned parts..--
        '-- RHS Print list of assigned parts..--
        savedFont = mDefaultUserFont '- save default font..-
        mDefaultUserFont.sName = "Courier New"
        mDefaultUserFont.lngSize = 8
        lngDepth = CStr(mlPrtDx(553)) '-  8300-- '--parts list box depth..
        Call mlPrintTextInBox(ev, msJobItemsRequired, _
                                 lngRHSX, CInt(lngMainTop), lngBoxMargin, lngRightColWidth, CInt(lngDepth), True)
        mDefaultUserFont = savedFont '--restore..

        '--Make TERMS Box and print terms and conditions..-
        '== '--TERMS- main box..-- 11,000 twips wide.-
        '== Printer.Line (lngTermsX, lngTermsY)-(lngTermsX + 10760, lngTermsY + 2300), , B '--TERMS- main box..--

        '--TERMS- main box..-- 11,000 twips wide.-
        lngDepth = CStr(mlPrtDx(160)) '-  2400 '-Terms box depth..
        lngYpos = mlDrawBox(ev, lngTermsX, lngTermsY, mlPrtWidth, CInt(lngDepth), -1) '--  nobg..--

        '--draw signing boxes..-
        lngSigX = lngRHSX + mlPrtDx(20) '=300 twips..-
        lngSigY = lngTermsY + mlPrtDx(20) '= 300 twips..-
        Call mbDrawSigningBoxes(ev, lngSigX, lngSigY, "Authorised By (Print Name)", "date Submitted", mDefaultUserFont, 1)

        '-- print terms text..-
        s1 = "<b><ul>Special Instructions this Job:" & vbCrLf & msQuoteInstructions & vbCrLf & vbCrLf
        s1 = s1 & "<b><ul>Full Job List this Quote:" & vbCrLf & msQuoteFullJobList & vbCrLf
        '== Call gbPrintTextInBox(s1, lngTermsX, lngTermsY, 300, 5200, 4000, False)  '--LHS- print all but last.-lngdepth=
        lngDepth = CStr(mlPrtDx(200)) '=  3000
        Call mlPrintTextInBox(ev, s1, lngTermsX, lngTermsY, lngBoxMargin, lngLeftColWidth, CInt(lngDepth), False) '--LHS- print --

        '--  do footer stuff..-
        Call mbJobMaintPageFooter(ev)
        '==Printer.EndDoc
        mbPrintQuoteJob_PageEvent = 0 '--ok..-
        '-- end print --
        '--only one page..-
        ev.HasMorePages = False
        Exit Function

PrintQuoteJob_Error:

        lngError = Err().Number
        MsgBox("!! ERROR in mbPrintQuoteJobForm." & vbCrLf & _
                    "Error code: " & lngError & " = " & ErrorToString(lngError), MsgBoxStyle.Exclamation)
        mbPrintQuoteJob_PageEvent = -lngError

    End Function '-- print Quote..--
    '= = = = = = = =
    '-===FF->

    '-- Print the NewJob form.--
    '-- Print the NewJob form.--
    '----  CALLER must set required property parameters -
    '--      and the correct printer before call..--

    '=  PrintDocument.OriginAtMargins Property (FALSE for us..).
    '= Gets or sets a value indicating whether the position of a graphics object associated with a page is located 
    '=  just inside the user-specified margins 
    '=   or at the top-left corner of the printable area of the page.
    '- --
    '==  https://msdn.microsoft.com/en-us/library/system.drawing.printing.printdocument.originatmargins(v=vs.90).aspx

    Private Function mbPrintNewJobForm_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean

        Dim sHdr1, sText As String
        Dim s1, s2 As String
        Dim sAddress1 As String
        Dim sRem As String
        Dim sCustText As String
        '==Dim lngTicketX, lngTicketY As Long
        '==Dim lngTxtHt, lngTxtWidth As Long
        Dim ix, iPos, iPos2, kx As Integer
        Dim lngCount, lngSize, L1 As Integer
        Dim lngError As Integer
        Dim lngTermsX, lngTermsY As Integer
        Dim lngMainTop, lngMainDepth As Integer
        Dim lngLeftColWidth, lngRightColWidth As Integer
        Dim lngSigX, lngSigY As Integer
        Dim lngXpos, lngYpos As Integer
        Dim lngBoxMargin As Integer
        Dim lngRHSX As Integer
        Dim lngBoxBG As Integer
        Dim lngInfoBoxWidth, lngInfoBoxDepth As Integer
        '==Dim lngHdrX As Long
        Dim sFullPath As String
        '=== Dim sTermsText As String
        Dim sTechName As String
        Dim sPrintGoodsInCare As String
        Dim sGoodsInCare As String '-- display only..-
        Dim sUserNameText As String
        Dim iNoTermsLines As Integer
        Dim asLines() As String
        Dim sTermsLHS, sTermsRHS As String
        Dim sShortName As String
        Dim font1 As userFontDef
        '-- A Rectangle has Left, Top, Width, height--
        Dim rectPageBounds As Rectangle = ev.PageBounds    '-- printable rectangle-
        Dim intPrintHeight As Integer = rectPageBounds.Height - 16

        mbPrintNewJobForm_PageEvent = False
        If (mColBusiness Is Nothing) Or (mColCust Is Nothing) Then Exit Function
        On Error GoTo PrintNewJob_Error

        '== Printer = mPrtSelected
        '--  default font -
        mDefaultUserFont.sName = "Lucida Sans"   '--3203.118=
        font1 = mDefaultUserFont '--  8pt..-

        sShortName = Replace(mColBusiness.Item("BusinessShortName"), "_", " ") '--remove underscores.--
        iNoTermsLines = 0
        sRem = mStrTermsText
        Erase asLines
        '--scan TermsText for lines separated by cr/lf
        '--build array of lines--
        While (Len(sRem) > 0)
            iPos = InStr(1, sRem, vbCrLf)
            If (iPos > 0) Then
                s1 = Trim(Left(sRem, iPos - 1))
                sRem = Mid(sRem, iPos + 2)
            Else
                s1 = Trim(sRem) : sRem = ""
            End If
            '-------If (Len(s1) > 0) Then  '---drop comment lines==
            iNoTermsLines = iNoTermsLines + 1
            ReDim Preserve asLines(iNoTermsLines)
            asLines(iNoTermsLines) = s1
            '--End If
        End While '--sRem--
        sHdr1 = "Customer Service Agreement"

        '--print full header..--
        Call mbPrintJobHeader(ev, sHdr1, mLngPriorityColour)

        lngMainTop = mIntMainDetailTop  '=FROM Hdr Function.-  
        lngMainDepth = 430  '= (387 * PRT_UNIT) '== 5800 twips..-
        lngBoxMargin = (20 * PRT_UNIT) '== 300 twips..-

        lngTermsX = K_LEFT_MARG   '=28  '== (16 * PRT_UNIT) '== 240 twips..
        lngTermsY = 580  '= (533 * PRT_UNIT) '== 8000 twips..   '=TERMS BOX..==

        lngLeftColWidth = 360 '= (360 * PRT_UNIT) '== 5400 twips..-
        lngRHSX = lngTermsX + lngLeftColWidth + 20  '=(20 * PRT_UNIT) 
        lngRightColWidth = lngTermsX + mlPrtWidth - lngRHSX

        '--  customer Info..--
        sCustText = "<b>Customer: " + mColCust.Item("CustomerBarcode") & "." & vbCrLf & _
                         mColCust.Item("CustomerName") & vbCrLf & mColCust.Item("CustomerCompany") & vbCrLf
        sCustText = sCustText & _
                   mColCust.Item("CustomerPhone") & ";   Mob: " & mColCust.Item("CustomerMobile") & vbCrLf & vbCrLf

        '==3083==
        '-- print Customer first, in case ON-SITE to follow..
        '--  NOW..   PRINT  CUST ONLY to start..
        lngYpos = mlPrintTextInBox(ev, sCustText, lngTermsX, lngMainTop, lngBoxMargin, lngLeftColWidth, lngMainDepth, True)

        '==3083==
        lngInfoBoxWidth = 300  '= (253 * PRT_UNIT) '==  3800 twips..-
        lngInfoBoxDepth = (57 * PRT_UNIT)
        If mbIsOnSiteJob Then  '--GoldenRod Box..-
            sText = "ON-SITE Job" & vbCrLf & _
                       "Date: " & msOnSiteDate & vbCrLf & "Time: " & msOnSiteTime & vbCrLf
            lngYpos = mlPrintTextInBox(ev, sText, lngTermsX + lngBoxMargin, lngYpos, _
                                         5, lngInfoBoxWidth, lngInfoBoxDepth, True, RGB(218, 165, 32))
        End If

        '-- Now print Priority, Tech and Goods..
        sCustText = "<ul>PRIORITY: " & vbTab & VB6.Format(k_TABPRINTPRIORITY, "000") & _
                                                        mColCust.Item("CustomerPriorityText") & vbCrLf & vbCrLf
        sTechName = mColCust.Item("CustomerTechName")
        If (sTechName <> "") Then
            sCustText &= "<ul>** Job Reserved- " & vbCrLf & "Nominated Tech: " & sTechName & vbCrLf
        Else
            sCustText &= "   (Job not Reserved..) " & vbCrLf
        End If

        '==  PRINT THIS LATER IN ONE BOX WITH GOODS..  =====
        '===    Call mbPrintTextInBox(sCustText, 240, 2000, 300, 5100, 3000, True)

        '= 3203.103-
        '-- PRINT IMAGE (if any) before Goods..

        '--  Print Item Image if any..-
        If (Not (imageItem1 Is Nothing)) AndAlso _
                              ((imageItem1.Height > 0) And (imageItem1.Width > 0)) Then
            '--Max rect must be square.
            Dim intImageMaxWidth As Integer = 180
            Dim intImageMaxHeight As Integer = intImageMaxWidth '=240
            '- Rect pos-
            Dim intImageRectTop As Integer = lngMainTop + lngMainDepth - intImageMaxHeight
            Dim intImageRectleft As Integer = lngTermsX + lngLeftColWidth - intImageMaxWidth
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
            '-- align rectangle to RH side of main box..
            intImageRectleft = lngTermsX + lngLeftColWidth - intRw - 2
            '--and at bottom of rect.
            intImageRectTop = lngMainTop + lngMainDepth - intRh - 2
            Dim rectImage1 As New Rectangle(intImageRectleft, intImageRectTop, intRw, intRh)
            ev.Graphics.DrawImage(imageItem1, rectImage1)
        End If  '-image-

        '-- Goods In Care..-
        '-- Goods In Care..-

        '--  encode from collection..--
        Call mbEncodeGoodsIncare(mColResultGoods, sGoodsInCare, sPrintGoodsInCare)
        sText = ""
        Dim sSep1 As String = " | "
        If Not mbIsOnSiteJob Then
            '= sText = "<b>Goods In Care" & vbCrLf & "<ul>Goods-Type" & vbTab & VB6.Format(k_TABPRINTBRAND, "000") & _
            '=                 "Brand" & vbTab & VB6.Format(k_TABPRINTMODEL, "000") & _
            '=                      "Model" & vbTab & VB6.Format(k_TABPRINTSERIAL, "000") & "S/no." & vbCrLf
            sText = "<b>Goods In Care" & vbCrLf & "<ul>Goods-Type" & sSep1 & _
                            "Brand" & sSep1 & "Model" & sSep1 & "SerialNo." & vbCrLf
        End If
        sText &= sPrintGoodsInCare & vbCrLf & vbCrLf
        If Not mbIsOnSiteJob Then
            sText &= "<ul>Other Goods and Extras in Care:" & vbCrLf
        End If
        sText &= mStrExtrasInCare
 
        '--  NOW..   PRINT  CUST plus  GOODS text..
        lngYpos = mlPrintTextInBox(ev, sCustText & vbCrLf & _
                               sText, lngTermsX, lngYpos, lngBoxMargin, lngLeftColWidth, lngMainDepth, False)

        '==3311.411=
        '--ON-SITE- Print stamp for time started/ time finished.. (shaded box)..
        If mbIsOnSiteJob Then
            L1 = mlPrintTextInBox(ev, "<ul>ON-SITE Time Started:", _
                                   lngTermsX + lngBoxMargin, lngYpos, 5, lngInfoBoxWidth, 50, True, RGB(230, 230, 230))
            lngYpos += 52
            lngYpos = mlPrintTextInBox(ev, "<ul>ON-SITE Time Finished:", _
                                        lngTermsX + lngBoxMargin, lngYpos, 5, lngInfoBoxWidth, 50, True, RGB(230, 230, 230))
        End If  '--onsite-

        '--RHS User Logon Info..-
        '--RHS User Logon Info..-

        '==  PRINT USERS in SINGLE RHS box AHEAD of JobRequirements.--

        sUserNameText = "<b>User Logon Details:" & vbCrLf & mStrUserNames & vbCrLf
        '-- RHS  Problem Info...--
        '===sText = "<b>Job Requirements" + vbCrLf + vbCrLf
        sText = "<b>Problems Reported" & vbCrLf
        sText = sText & mStrSymptoms & vbCrLf & vbCrLf
        sText = sText & "<b>Problem Details:" & vbCrLf & _
                           VB.Left(mStrProblem, 600) & vbCrLf & vbCrLf & mStrDataBackup & vbCrLf

        lngYpos = mlPrintTextInBox(ev, sUserNameText & vbCrLf & _
                                    sText, lngRHSX, lngMainTop, lngBoxMargin, lngRightColWidth, lngMainDepth, True)

        '-- "CurrentY" should be following Details line..--
        lngInfoBoxWidth = 253 '==  3800 twips..-
        lngInfoBoxDepth = 36  '== 450
        '--  RED BOX 440 twps deep..for JobReturned..-
        '----  where-ever we are at..
        lngYpos = lngYpos + 12
        lngXpos = lngRHSX + 20
        If mbJobReturned Then
            sText = "<b> !  Job is being Returned.."
            Call mlPrintTextInBox(ev, sText, _
                                 lngXpos, lngYpos, 6, lngInfoBoxWidth, lngInfoBoxDepth, True, RGB(255, 0, 0))
            lngYpos = lngYpos + 44
        ElseIf mbSystemUnderWarranty Then
            '=3203.118=
            font1.bBold = True
            font1.lngSize = 9
            Call mlDrawBox(ev, lngXpos, lngYpos, lngInfoBoxWidth, lngInfoBoxDepth, gIntUnderWarrantyColour)
            sText = "** System Under Warranty.."
            Call mlPrintTextString(ev, sText, lngXpos + 8, lngYpos + 6, font1, textColour.white)
            '= Call mlPrintTextInBox(ev, sText, _
            '=                      lngXpos, lngYpos, 6, lngInfoBoxWidth, lngInfoBoxDepth, True, gIntUnderWarrantyColour)
            lngYpos = lngYpos + 44
        End If '--returned/Warranty-

        '--  Yellow BOX 440 twps deep..for Quote Required.-
        '----  where-ever we are at..
        lngXpos = lngRHSX + 20  '== Printer.CurrentX = lngRHSX + 300
        sText = "<b> ** " & mStrInstructionLabel '==Quotation Required.."
        '== Printer.Line (lngRHSX + (20 * PRT_UNIT), lngYpos)-(lngRHSX + lngInfoBoxWidth, lngYpos + lngInfoBoxDepth), , B
        lngYpos = mlPrintTextInBox(ev, sText, _
                         lngXpos, lngYpos, 6, lngInfoBoxWidth, lngInfoBoxDepth, True, mLngInstructionBGColour)
        '--  Print service cahrges text if any..--
        If (mStrServiceChargesInfoText <> "") Then  '--supplied text..--
            '-- inser CR/LF where asked.--
            mStrServiceChargesInfoText = Replace(mStrServiceChargesInfoText, "\n", vbCrLf)
            sText = "<b>Non-warranty Charge Rates:" & vbCrLf & mStrServiceChargesInfoText & vbCrLf
            lngYpos = mlPrintTextInBox(ev, vbCrLf & sText, lngRHSX, lngYpos, 12, mlPrtWidth - lngRHSX, 100, False)
        End If

        font1.bBold = False '== Printer.FontBold = False
        font1.lngSize = 8 '== Printer.FontSize = 8  '==9

        '--Make TERMS Box and print terms and conditions..-
        '--TERMS- main box..-- 11,000 twips wide.-  7,000 twips deep.-

        lngYpos = mlDrawBox(ev, lngTermsX, lngTermsY, mlPrtWidth, 477, -1) '--  nobg..--
        '--draw signing boxes..-
        '--draw signing boxes..-

        If Not mbLicenceOK Then
            '== Printer.FillColor = &HA0A0A0  '-med-Grey--
            lngBoxBG = &HA0A0A0
        Else '--ok-
            lngBoxBG = &HDCDCDC
        End If
        lngSigX = lngRHSX + lngBoxMargin
        lngSigY = lngTermsY + (346 * PRT_UNIT) '=5200 twips.-
        Call mbDrawSigningBoxes(ev, lngSigX, lngSigY, "Customer (Print Name)", "Date Submitted:", mDefaultUserFont, 1)

        '--  overprint with nag text if not licenced..-
        If Not mbLicenceOK Then

            lngXpos = lngSigX + (7 * PRT_UNIT)
            s1 = "          - This JobTracking Software -"
            s2 = "       -  For Evaluation purposes only -"
            '--  WHITE overprint..-
            font1.lngSize = 12
            lngYpos = mlPrintTextString(ev, s1, lngXpos, lngSigY + 12, font1, textColour.magenta)
            lngYpos = mlPrintTextString(ev, s2, lngXpos, lngYpos, font1, textColour.magenta)
        End If

        '-- print terms text..-
        '--  set up labour rate display..-
        s1 = "n/a"
        If (mCurLabourMinCharge > 0) Then s1 = FormatCurrency(mCurLabourMinCharge, 2)
        s2 = "n/a"
        If (mCurLabourHourlyRate > 0) Then s2 = FormatCurrency(mCurLabourHourlyRate, 2) & " Per Hour."

        sTermsLHS = "<b><ul>Terms and Conditions" & vbCrLf & vbCrLf
        If (mStrServiceChargesInfoText = "") Then  '--supplied text..--
            sTermsLHS = sTermsLHS & "<b><ul>Minimum Charge: " & s1 & vbCrLf & _
                                "<b><ul>Service Fees (Priority " & mColCust.Item("CustomerPriorityText") & "): " & s2 & vbCrLf
        End If  '--service charges..
        sTermsRHS = "" '--RHS--

        kx = iNoTermsLines
        If (iNoTermsLines > 1) Then kx = (iNoTermsLines - 1) '--index of last para...-
        lngSize = 0
        If iNoTermsLines > 0 Then '--some terms.-
            For ix = 1 To iNoTermsLines '--compute total size...)
                lngSize = lngSize + Len(asLines(ix))
            Next ix
            lngCount = 0
            '--accumulate 2/3 rds for LHS. rest for RHS..-(unless only 1..)
            For ix = 1 To iNoTermsLines
                s1 = asLines(ix)
                If ix = iNoTermsLines Then '--last para..--
                    If Left(s1, 1) <> "<" Then s1 = "<ul>" & s1 '--add underlining if needed..-
                End If
                If (lngCount < (lngSize \ 5) * 3) Then '--add to rhs.-
                    sTermsLHS = sTermsLHS & s1 & vbCrLf
                Else '--rhs..-
                    sTermsRHS = sTermsRHS & s1 & vbCrLf
                End If
                lngCount = lngCount + Len(asLines(ix))
            Next ix
            '--  Insert this business actual short name.-
            sTermsLHS = Replace(sTermsLHS, "&&BizShortName", sShortName, , , CompareMethod.Text)
            '--  left "box" 5500x6300 --
            Call mlPrintTextInBox(ev, sTermsLHS, lngTermsX, lngTermsY, lngBoxMargin, 367 * PRT_UNIT, 420 * PRT_UNIT, False) '--LHS- print -
            '--  Insert this business actual short name.-
            sTermsRHS = Replace(sTermsRHS, "&&BizShortName", sShortName, , , CompareMethod.Text)
            '====If Left(s1, 1) <> "<" Then s1 = "<ul>" + s1 '--add underlining if needed..-
            '-- right "box" 5,000 x 5,300  --
            Call mlPrintTextInBox(ev, sTermsRHS, lngRHSX, lngTermsY, lngBoxMargin, 334 * PRT_UNIT, 354 * PRT_UNIT, False)
            '===End If
        End If

        '--  Print Quotation or Service Notice above signing box..--

        font1.bItalic = True '= Printer.Font.Italic = True
        '== Call mbPrintTextInBox(mStrInstructionText, (lngTermsX + 5600), (lngSigY - 900), 200, 4700, 560, False)
        lngYpos = (lngSigY - mlPrtDx(60)) '==900
        Call mlPrintTextInBox(ev, mStrInstructionText, lngRHSX, lngYpos, lngBoxMargin, lngRightColWidth, mlPrtDx(38), False)
        '== Printer.Font.Italic = False

        '--  do footer stuff..-
        Call mbJobMaintPageFooter(ev)

        MsgBox("Service Agreement was sent to the printer: " & vbCrLf & msSelectedPrinterName, MsgBoxStyle.Information)

        mbPrintNewJobForm_PageEvent = True '--ok..-
        '-- end print --

        '--only one page..-
        ev.HasMorePages = False
        Exit Function

PrintNewJob_Error:

        lngError = Err().Number
        MsgBox("!! ERROR in method PrintNewJobForm." & vbCrLf & _
                               "Error code: " & lngError & " = " & ErrorToString(lngError), MsgBoxStyle.Exclamation)
        mbPrintNewJobForm_PageEvent = False

    End Function '-- print new job..--
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  Print Maint/Service/Delivery Documents--
    '--  Print Maint/Service/Delivery Documents--
    '--  Print Maint/Service/Delivery Documents--

    '-- Maint/Service/Delivery Job Form
    '---      and Stock Barcodes..-
    '----  CALLER must set required property parameters -
    '--      and the correct printer before call..--
    '==    Updated- 4219.1121 21-Nov-2019= 
    '==      -- clsPrintDocs- JobMaint Printing-  Fix Printing WorkHistory for Multiple Pages.
    '= =

    '==
    '== Target-New-Build-6201 --  (15-July-2021) for Open Source..
    '==    -- Add CheckBox "Print Item Barcodes" to JobMaint Form (print section).
    '==    --  In clsPrintDocs, print the item barcode list only if requested.
    '= = = =
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    Private msWorkTextRemaining As String = ""
    '= = == 


    Private Function mbPrintJobMaintForm_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean

        Dim sHdr1, sText As String
        Dim s1, s2 As String
        Dim col1 As Collection
        Dim sDescr As String
        Dim sCat1 As String
        Dim sSerialNo, sBarcode As String
        Dim sStockId, sWty As String
        Dim sSell As String
        '== Dim sRem As String
        Dim sCustText As String
        '==Dim lngTxtHt, lngTxtWidth As Integer
        Dim ix As Integer
        Dim Ly, L1, lngXpos, lngYpos, Lx, lngHistoryPos As Integer
        Dim lngTermsX, lngTermsY As Integer
        Dim lngTermsDepth As Integer
        Dim lngLHSX, lngRHSX As Integer
        Dim lngSigX, lngSigY As Integer
        Dim lngMainTop, lngMainDepth As Integer
        Dim lngLeftColWidth, lngRightColWidth As Integer
        Dim lngBoxMargin As Integer

        Dim lngError, lngNoItems As Integer
        Dim lngNoItemsParts, lngNoItemsService As Integer
        Dim lngTotalNoItems As Integer
        Dim lngListItemCount As Integer
        '==Dim lngItemCount As Integer
        Dim lngFillColour As Integer
        Dim sDate1, sDatePrev As String
        Dim bExtraToQuote As Boolean
        Dim bSpecialPrice As Boolean
        Dim font1 As userFontDef
        Dim txtColour1 As textColour
        Dim msGoodsIncare As String
        Dim intCustDepth As Integer = 120
        Dim intYpos As Integer
        '==3311.507= 
        Dim intPageLineCount As Integer = 0  '--itms on page.-

        '==  4219 VERSION
        '-- A Rectangle has Left, Top, Width, height--
        '== 4219.1119 19-Nov-2019= 
        Dim rectPageBounds As Rectangle = ev.PageBounds    '-- printable rectangle-
        Dim intPrintHeight As Integer = rectPageBounds.Height
        Dim intMainDetailBoxDepth As Integer = 880
        Dim intWorkDetailTop, intDetailDepthRemaining, intTextLengthRemaining As Integer
        Dim intXpos As Integer  '= = lngLHSX + 20
        Dim intMainWorkTextWidth As Integer = mlPrtWidth

        '--  get terms text..-
        '== On Error GoTo PrintJob_Error
        mbPrintJobMaintForm_PageEvent = False

        font1 = mDefaultUserFont '--  8pt..-

        lngLHSX = K_LEFT_MARG   '= mlPrtDx(16) '=  240 twips.-
        intXpos = lngLHSX   '=+ 20

        '--  another page..-
        mIntMaintPage += 1   '--  another page..-

        '= lngMainTop = mlPrtDx(133) '== 2000 twips..-
        lngMainDepth = 740  '= mlPrtDx(640) '== 9600 twips..-
        lngBoxMargin = mlPrtDx(20) '== 300 twips..-
        lngLeftColWidth = 290  '= mlPrtDx(347) '==  5200 twips..-

        lngTermsX = K_LEFT_MARG   '= mlPrtDx(16) '= 240 twips..-
        lngTermsY = 900 '= mlPrtDx(800) '== 12000 twips--
        lngRHSX = lngLHSX + lngLeftColWidth + mlPrtDx(16) '=Gap 240 twips..-
        lngRightColWidth = K_LEFT_MARG + mlPrtWidth - lngRHSX '== (11000 - lngRHSX)
        lngTermsDepth = mlPrtDx(174) '==2600 twips..-

        '==  Check age..-- PAGE ==
        If (mIntMaintPage = 1) Then
            '==  FIRST PAGE ==
            msGoodsIncare = CStr(mColResultGoods.Item(1))
            If InStr(UCase(msGoodsIncare), K_GOODS_ONSITEJOB) > 0 Then
                mbIsOnSiteJob = True
            End If

            '=3203.211=  Print "Don't forget to Delver at the top now..
            '== REMINDER to Deliver--
            sText = "<b>Remember to deliver Job in JobMatix when invoice is completed !"
            Call mlPrintTextInBox(ev, sText, _
                                 lngRHSX + 40, 44, 6, 232, 56, True, RGB(255, 241, 168))
            '=lngYpos = lngYpos + (60 * PRT_UNIT)

            sHdr1 = "Job Service Record - [Office Use Only]"

            '--print full header..--
            '==3311.410=  No special Colour for ON-SITE completed Job.
            '==3311.410= If mbIsOnSiteJob Then  '--goldenrod-
            '==3311.410= Call mbPrintJobHeader(ev, sHdr1, RGB(218, 165, 32), msJobStatus)
            '==3311.410= Else  '-normal-
            Call mbPrintJobHeader(ev, sHdr1, mLngPriorityColour, msJobStatus)
            '==3311.410= End If

            lngMainTop = mIntMainDetailTop  '=FROM Hdr Function.
            '-- end of header..-
            font1.sName = "Tahoma" '== printer.FontName = "Tahoma"
            font1.lngSize = 8 '==  Printer.FontSize = 9

            '--  customer Info..--
            msCustomerPrint = mColCust.Item("CustomerPrint")
            '--  customer Info..--
            s1 = Replace(msCustomerPrint, ")", "")
            s1 = Replace(s1, "(", vbCrLf) '-- company on next line if any.-

            sCustText = "<b>Customer: " + mColCust.Item("CustomerBarcode") & "." & vbCrLf & s1 & vbCrLf
            sCustText = sCustText + mColCust.Item("CustomerPhone") & ";" & _
                              "  Mob: " & mColCust.Item("CustomerMobile") & vbCrLf & vbCrLf
            sCustText = sCustText & "<ul>PRIORITY: " & vbTab & VB6.Format(k_TABPRINTPRIORITY, "000") & _
                                                          mColCust.Item("CustomerPriorityText") & vbCrLf & vbCrLf
            sCustText = sCustText & "<ul>Nomin.Tech: " & vbTab & VB6.Format(k_TABPRINTPRIORITY, "000") & _
                                             mColCust.Item("CustomerTechName") + vbCrLf + vbCrLf + vbCrLf
            '--  PRINT LHS Stuff..--
            '- draw main box-
            Call mlPrintTextInBox(ev, "", lngLHSX, lngMainTop, lngBoxMargin, lngLeftColWidth, lngMainDepth, True)

            '--3107.907= Print cust stuff first.-
            Call mlPrintTextInBox(ev, sCustText, lngLHSX, lngMainTop, lngBoxMargin, lngLeftColWidth, intCustDepth, False)
            intYpos = lngMainTop + intCustDepth

            '-- add Goods in care..-
            If mbIsOnSiteJob Then
                sCustText = "<b>" & CStr(mColResultGoods.Item(1))
                '--3107.907= Print Goods or ONSITE stuff..-
                Call mlPrintTextInBox(ev, sCustText, lngLHSX, intYpos, _
                                               lngBoxMargin, ((lngLeftColWidth \ 3) * 2), 140, True, RGB(218, 165, 32))
            Else
                sCustText = "<b>Goods In Care" & vbCrLf & vbCrLf & CStr(mColResultGoods.Item(1))
                '--3107.907= Print Goods or ONSITE stuff..-
                Call mlPrintTextInBox(ev, sCustText, lngLHSX, intYpos, _
                                               lngBoxMargin, lngLeftColWidth, 140, False)
            End If

            intYpos += 140
            '--add problems..
            sCustText = "<b>Job Requirements" & vbCrLf & vbCrLf & mStrProblem
            '--3107.907= PrintGoogs or ONSITE stuff..-
            '=4219.1121- make Depth 340.
            Call mlPrintTextInBox(ev, sCustText, lngLHSX, intYpos, _
                                           lngBoxMargin, lngLeftColWidth, 340, False)

            '--  PRINT LHS Stuff..--
            '==done= Call mlPrintTextInBox(ev, sCustText, lngLHSX, lngMainTop, lngBoxMargin, lngLeftColWidth, lngMainDepth, True)

            '-- RHS  work done stuff.....--
            '-- RHS  work done stuff.....--
            sText = "<b>Job History" & vbCrLf & vbCrLf

            '==  SHOW SERVICE CHECKLISTS under SERVICE ITEMS  =
            '---   NO !!  !!!===

            sText = sText & "<ul>Misc.Tasks Completed" & vbCrLf

            '--  now comes in as a collection..--
            If (mColTasksCompleted.Count() > 0) Then
                sDatePrev = "" '--control break..-
                '--do each task item..-
                For Each col1 In mColTasksCompleted
                    If (col1.Count() >= 3) Then
                        sDate1 = Left(col1.Item(2), 11) '--"dd-mmm-yyyy"...--
                        If (sDate1 <> sDatePrev) Then '--new date- show Date--
                            If sDatePrev <> "" Then sText = sText & vbCrLf '-- not the 1st date.-
                            sText = sText & "== " & sDate1 & " ==" & vbCrLf '-- date "header".--
                        End If '--new-
                        sDatePrev = sDate1
                        '-- show task descr. and Staff..-
                        sText = sText & "--" & col1.Item(1) & ":(" & col1.Item(3) & ");  " '--let box fn break the lines..-
                    End If '-3-
                Next col1 '--col1-
                sText = sText & vbCrLf & vbCrLf '--last one plus break..-
            End If '--count.-

            '--parts..--
            '--  AND SERVICE CHARGES..--
            sText &= msBuildSuppliedItemsText()

            '---  Work Details on new page..-
            '--  PRINT RHS Stuff..--
            lngYpos = mlPrintTextInBox(ev, sText & vbCrLf, _
                                       lngRHSX, lngMainTop, lngBoxMargin, lngRightColWidth, lngMainDepth, True)

            '==3311.411-
            '-- Totals in shaded box.
            '== Need Labour here..
            '-- add labour cost, if any..--
            sText = "<b>Cost Summary-" & vbCrLf & vbCrLf & msShowLabourCost & vbCrLf

            s1 = FormatCurrency(mCurTotalItemValue, 2)
            sText &= "<b> - Items Supplied (Service +Parts): " & s1 & vbCrLf & vbCrLf
            s1 = FormatCurrency((mCurTotalLabourValue + mCurTotalItemValue), 2)
            sText &= "<b><ul> - Total Job Cost (Labour +Items): " & s1 & vbCrLf
            lngYpos = mlPrintTextInBox(ev, sText & vbCrLf, _
                                   lngRHSX + lngBoxMargin, lngYpos, 10, 320, 160, True, RGB(240, 240, 240))

            '== REMINDER to Deliver--
            '= sText = "<b>Important: Don't forget to deliver Job in JobMatix when the Customer invoice is completed !"
            '= Call mlPrintTextInBox(ev, sText, _
            '==                      lngRHSX, lngYpos, 6 * PRT_UNIT, lngRightColWidth, 60, True, RGB(255, 241, 168))
            '== lngYpos = lngYpos + (60 * PRT_UNIT)

            '-- "CurrentY" should be following Details line..--
            '--save in case we fit history on this page..
            lngHistoryPos = lngYpos

            font1 = mDefaultUserFont
            '--TERMS- main box..-- 11,000 twips wide.-
            '== Printer.Line (lngTermsX, lngTermsY)-(lngTermsX + 10760, lngTermsY + 2600), , B '--TERMS- main box..--

            '--NOTIFICATIONS box..-- 4500.. NOW 5200 twips wide./ 2600 deep--
            '==Printer.Line (lngTermsX, lngTermsY)-(lngTermsX + lngLeftColWidth, lngTermsY + lngTermsDepth), , B
            lngYpos = mlDrawBox(ev, lngTermsX, lngTermsY, lngLeftColWidth, lngTermsDepth, -1)

            '--SIGNING- main box..-- 11,000 twips RH edge.-
            '== Printer.Line (lngRHSX, lngTermsY)-(mlPrtWidth, lngTermsY + lngTermsDepth), , B
            lngYpos = mlDrawBox(ev, lngRHSX, lngTermsY, lngRightColWidth, lngTermsDepth, -1)

            '--draw signing boxes..-
            lngSigX = lngRHSX + mlPrtDx(20) '=300 twips..-
            lngSigY = lngTermsY + mlPrtDx(40) '= 600 twips..-
            Call mbDrawSigningBoxes(ev, lngSigX, lngSigY, "Print Name", "Date Collected:", mDefaultUserFont, 1)

            '-- print Notifications text..-
            Call mlPrintTextInBox(ev, "<ul>Notifications:" & vbCrLf & _
                      msNotifications, lngTermsX, lngTermsY, 20, lngLeftColWidth, lngTermsDepth, False) '--LHS- print in box.-
            '--Collected By..-
            font1.bBold = True '== Printer.FontBold = True
            font1.bItalic = True '==  Printer.FontItalic = True
            s1 = "Job Collected By:"
            lngXpos = lngSigX '==  Printer.CurrentX = lngSigX  '--above PrintName box..--
            lngYpos = lngTermsY + mlPrtDx(12) '--180 twips.   '==Printer.CurrentY = lngTermsY + Printer.TextHeight(s1) - 60
            '== Printer.Print s1
            lngYpos = mlPrintTextString(ev, s1, lngXpos, lngYpos, font1)

            font1 = mDefaultUserFont '--restore--
            Call mbJobMaintPageFooter(ev)

            '--  Work Details on separate page.--
            '==
            '==  TEST VERSION
            '==    Updated- 4219.1119 19-Nov-2019= 
            '==       >> clsPrintDocs- JobMaint Printing-  Fix Printing WorkHistory for Multiple Pages.
            '==
            '-- PAGE-1 only-  Prepare final work details text..
            msWorkTextRemaining = msWorkHistory '-        "-- The end --"

            sText = ""
            ev.HasMorePages = True

        ElseIf (mIntMaintPage >= 2) And (msWorkTextRemaining <> "") Then  '--onto OR IN history page..-
            font1.sName = "Tahoma"
            font1.lngSize = 12

            lngYpos = K_TOP_MARG
            lngXpos = lngLHSX
            lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)
            lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)
            lngYpos = mlPrintTextString(ev, mColBusiness.Item("BusinessName") & Space(50) & _
                                         FormatDateTime(Today, DateFormat.LongDate) & "   Page: " & mIntMaintPage, _
                                         lngXpos, lngYpos, font1)
            lngYpos = mlPrintTextString(ev, "Job No: " & VB6.Format(mLngJobNo, "000") & _
                                                  ".    Customer: " & msCustomerPrint, lngXpos, lngYpos, font1)
            font1.lngSize = 8 '= Printer.FontSize = 8

            '==  4219 VERSION-
            '==    Updated- 4219.1119 19-Nov-2019= 
            '==GET enough text to fit in this page..
            intWorkDetailTop = lngYpos + 12  '- draw main detail box below header..
            intDetailDepthRemaining = intMainDetailBoxDepth - 60  '--avail for text..

            s1 = msWorkTextRemaining
            intTextLengthRemaining = Len(msWorkTextRemaining)
            If (s1 = "") Then
                ev.HasMorePages = False
                '-- finished-
            Else  '-have some.
                '-- test depth.--  Allow 3 lines (24 px) as margin.
                L1 = mlPrintTextInBox(ev, "<b><ul>Work Details-" & vbCrLf & vbCrLf & s1, intXpos, intWorkDetailTop, _
                                                 20, intMainWorkTextWidth, intMainDetailBoxDepth, False, , True)
                If ((L1 + 24) <= (intWorkDetailTop + intMainDetailBoxDepth)) Then
                    '--ok- Didn't go over the botton.
                    '-- will fit..  print it..
                    intYpos = mlPrintTextInBox(ev, "<b><ul>Work Details-" & vbCrLf & vbCrLf & _
                                                      msWorkTextRemaining & " -- the end --", intXpos, intWorkDetailTop, _
                                                                20, intMainWorkTextWidth, intMainDetailBoxDepth, True)
                    msWorkTextRemaining = ""
                    ev.HasMorePages = False
                    '-- finished-
                    '= L1 = mlPrintTextString(ev, "-- The end --", intXpos, intYpos, font1)
                Else
                    '--needs extra pages..
                    '-- Find how much will fit on this page..
                    '--  print it, and leave Remainder for next page..-
                    If mbSplitTextToFit(ev, (intDetailDepthRemaining - 32), _
                                                             msWorkTextRemaining, intMainWorkTextWidth, s1, s2) Then
                        msWorkTextRemaining = s2  '--save for next page..
                        '--s1- will fit..  print it..
                        intYpos = mlPrintTextInBox(ev, "<b><ul>Work Details-" & vbCrLf & vbCrLf & s1, intXpos, intWorkDetailTop, _
                                                                                     20, intMainWorkTextWidth, intMainDetailBoxDepth, True)
                        font1.bItalic = True
                        L1 = mlPrintTextString(ev, "-- " & " Continued on Page : " & _
                                                           CStr(mIntMaintPage + 1) & "  --", intXpos, intYpos, font1)
                        ev.HasMorePages = True
                    Else
                        '-- couldn't fit anything in.-
                        font1.bItalic = True
                        L1 = mlPrintTextString(ev, "-- Continued on Page : " & _
                                                           CStr(mIntMaintPage + 1) & "  --", intXpos, intYpos, font1)
                        ev.HasMorePages = True
                    End If  '-if fits-
                End If  '--depth test-
            End If  '-have text-
            '== END this TEST VERSION segment.

            '==    Updated- 4219.1119 19-Nov-2019= 
            '==  NOT NEEDED..
            '-- Page work area is 8000 x 12000 twips..--
            's1 = msWorkHistory
            'sText = sText & "<b><ul>Work Details-" & vbCrLf & vbCrLf & s1 & vbCrLf
            'lngMainTop = lngYpos + 12  '--3mm-
            'Call mlPrintTextInBox(ev, sText, lngTermsX, lngMainTop, lngBoxMargin, mlPrtDx(534), mlPrtDx(800), True)

            '== GoSub PrintJob_Footer--
            Call mbJobMaintPageFooter(ev)
 
            '--- Print Item barcodes on NEXT PAGE if Font defined..-
            '==    Updated- 4219.1119 19-Nov-2019= 
            '==  DO THIS ONLY IF ALL Work Details are printed.
            If (msWorkTextRemaining = "") Then  '-work history done.
                lngNoItemsParts = mColPartsItems.Count() '--ListViewParts(k_LV_INDEX_PARTS).ListItems.Count
                lngNoItemsService = mColServiceItems.Count() '- ListViewParts(k_LV_INDEX_SERVICE).ListItems.Count
                lngTotalNoItems = lngNoItemsService + lngNoItemsParts
                '== Target-New-Build-6201 --  (16-July-2021) for Open Source..
                If (msItemBarcodeFontName <> "") And
                       ((lngNoItemsParts > 0) Or (lngNoItemsService > 0)) And mbCanPrintJobItemBarcodes Then '--have barcode font..-
                    '== END Target-New-Build-6201 --  (16-July-2021) for Open Source..
                    ev.HasMorePages = True
                Else
                    ev.HasMorePages = False
                End If  '--  checking if barcode..
            End If '-work history done.
        Else   '--Page-3 or more..  (OR NO MORE Work detail pages..)
            '-- WE must have (AND BE IN) barcodes..--
            '--  mIntWtyLoop is kept in STATIC..--
            '--  PAGE event..  RESUME wty loop..-

            '--  MAKE 3 loops so we ALSO print  SERVICE ITEMS.. ---
            lngNoItemsParts = mColPartsItems.Count() '--ListViewParts(k_LV_INDEX_PARTS).ListItems.Count
            lngNoItemsService = mColServiceItems.Count() '- ListViewParts(k_LV_INDEX_SERVICE).ListItems.Count
            lngTotalNoItems = lngNoItemsService + lngNoItemsParts
            '--  PAGE event needs Header..
            lngYpos = mlJobMaintPageHeader(ev, mIntMaintPage, lngTotalNoItems)

            '== Target-New-Build-6201 --  (15-July-2021) for Open Source..
            If (msItemBarcodeFontName <> "") And ((lngNoItemsParts > 0) Or (lngNoItemsService > 0)) And  '=Then '--have barcode font..-
                                                                             mbCanPrintJobItemBarcodes Then
                '==END  Target-New-Build-6201 --  (15-July-2021) for Open Source..

                '--  do THREE passes of parts list..
                '--- FIRST to print SERVICE items..--
                '---  SECOND to print Non-Wty items, Then do Wty items..-
                '== While mIntWtyLoop < 3
                While (mIntWtyLoop <= 3)   '== FIXED in 3057.0 22May2012==
                    '==    For intWtyLoop = 1 To 3
                    lngListItemCount = lngNoItemsParts '--loops 2,3 --
                    If (mIntWtyLoop = 1) Then '--Service--
                        lngListItemCount = lngNoItemsService
                    End If
                    While (mIntMaintItemIx < lngListItemCount)
                        '== For ix = 1 To (lngListItemCount)
                        '-- each part/item..--
                        mIntMaintItemIx += 1
                        ix = mIntMaintItemIx
                        sWty = ""
                        If (mIntWtyLoop = 1) Then '--Service--
                            col1 = mColServiceItems.Item(ix) '== item1 = ListViewParts(k_LV_INDEX_SERVICE).ListItems(ix)
                        Else
                            col1 = mColPartsItems.Item(ix) '==item1 = ListViewParts(k_LV_INDEX_PARTS).ListItems(ix)
                        End If
                        sCat1 = col1.Item("Cat1") '== item1.Text
                        sDescr = col1.Item("Description") '== item1.SubItems(1)
                        sBarcode = col1.Item("Barcode") '==  Trim(item1.SubItems(2))
                        sSerialNo = col1.Item("Serial-No") '== Trim(item1.SubItems(3)) '--serial no.- Print only if NOT blank..
                        sSell = col1.Item("Sell-Price") '==  Trim(item1.SubItems(6))        '--Sell-inc..
                        '-- Check allow_renaming..--
                        bSpecialPrice = False
                        s1 = col1.Item("SpecialPrice") '==  Trim(item1.SubItems(8))        '--special.
                        If UCase(Left(s1, 1)) = "Y" Then bSpecialPrice = True
                        bExtraToQuote = False
                        txtColour1 = textColour.black   '--default..-
                        If mbQuotation Then
                            If (LCase(col1.Item("SmallIcon")) = "alert") Then '--extra..-
                                bExtraToQuote = True
                            End If
                        Else
                            sWty = col1.Item("Wty") '==  Trim(item1.SubItems(4))        '--Warranty..
                        End If
                        sStockId = VB6.Format(CInt(col1.Item("StockId")), " 0000") '== Format(CLng(item1.SubItems(5)), " 0000") '-StockId--
                        font1.sName = "Tahoma" '== Printer.FontName = "Tahoma"
                        font1.lngSize = 9 '==  Printer.FontSize = 9
                        '== Printer.Print
                        '--blank line bewteen items..--
                        '==3057.0= MOVED to below print line.= 
                        '==    lngYpos = mlPrintTextString(ev, "- - - -", lngXpos, lngYpos, font1)
                        font1.bBold = True '== Printer.FontBold = True
                        '== Printer.CurrentX = 960
                        '-- First Pass through prints SERVICE, 2nd pass Non-Warranty parts..-
                        '--   if SECOND pass, print only if Wty OR EXTRA..-
                        If (mIntWtyLoop = 1) Or ((mIntWtyLoop = 2) And (sWty <> "W") And (Not bExtraToQuote)) Or
                                                              ((mIntWtyLoop = 3) And ((sWty = "W") Or bExtraToQuote)) Then
                            '-- Printing this item..--
                            '-- -- Max nine items per page..-
                            '== If ((intLineCount Mod 8) = 0) Then '--start new page--
                            '== '== GoSub PrintJob_Header
                            '== lngYpos = mlJobMaintPageHeader(ev, intPage, lngTotalNoItems)
                            '== intLineCount = 0
                            '== End If '--hdr..-
                            If ((sWty = "W") Or bExtraToQuote) Then
                                '-- Special message in box..--
                                If (mIntWtyCount = 0) Then '-- if =0 then First wty item..-
                                    '== Printer.FillStyle = 0
                                    lngFillColour = RGB(255, &HC0S, &HCBS) '== Printer.FillColor = RGB(255, &HC0, &HCB)   '--pink.-
                                    s1 = " ** ** WARRANTY PARTS FOLLOW..  ** **"
                                    If bExtraToQuote Then
                                        s1 = " ** These Parts are EXTRA to Quotation..**"
                                        lngFillColour = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow) '== Printer.FillColor = vbYellow '--- RGB(255, &HC0, &H9)   '--yellow.-
                                    End If
                                    '--draw box for Warranty/Extras Header.-
                                    lngXpos = 64  '==3057.0 ==  960
                                    '== Printer.Print
                                    lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1) '--blank line..-
                                    '==lngYpos = Printer.CurrentY   '--get wty start pos..-
                                    '==Printer.Line (960, lngYpos)-(5000, lngYpos + lngTxtHt + 200), , B
                                    '== Printer.FillStyle = 0
                                    '== Printer.CurrentX = 960
                                    '== Printer.CurrentY = lngYpos + 100
                                    '== Printer.Print s1
                                    lngYpos = mlPrintTextInBox(ev, s1,
                                                  lngXpos, lngYpos, 8, lngRightColWidth - 30, 30, True, lngFillColour)
                                    lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1) '--blank line..-
                                    '==Printer.Print
                                    '=3311.507= mIntLineCount = mIntLineCount + 1
                                    intPageLineCount += 1
                                End If
                                mIntWtyCount = mIntWtyCount + 1
                            End If '--wty-
                            font1.sName = "Tahoma" '== Printer.FontName = "Tahoma"
                            font1.lngSize = 9 '==  Printer.FontSize = 9
                            font1.bBold = True '== Printer.FontBold = True
                            lngXpos = mlPrtDx(64) '== Printer.CurrentX = 960 twips..
                            '--  print DESCR/price in purple if special price..--
                            If bSpecialPrice Then
                                '== Printer.ForeColor = &HE000E0 '--magenta-ish..-
                                txtColour1 = textColour.magenta
                            End If
                            '==Printer.Print sCat1 + ": "; sDescr;
                            L1 = lngYpos '--  save for warranty..-
                            lngYpos = mlPrintTextString(ev, sCat1 & ": " & sDescr, lngXpos, lngYpos, font1, txtColour1)

                            lngXpos = lngRHSX '==  Printer.CurrentX = lngRHSX
                            '== Printer.Print IIf((sWty = "W"), " [WARRANTY PART]", "")    '--Description..--
                            If (sWty = "W") Then
                                lngYpos = mlPrintTextString(ev, " [WARRANTY PART]", lngXpos, L1, font1)
                            End If
                            '--  print SPECIAL price in purple if special price..--
                            If bSpecialPrice Then
                                lngXpos = mlPrtDx(64) '= Printer.CurrentX = 960
                                '== Printer.ForeColor = &HEE00EE '--magenta-ish..-
                                '== Printer.Print "*** SPECIAL PRICE: " & sSell & "  ***"
                                txtColour1 = textColour.magenta
                                lngYpos = mlPrintTextString(ev, "*** SPECIAL PRICE: " & sSell & "  ***",
                                                                                    lngXpos, lngYpos, font1, txtColour1)
                            End If
                            '== Printer.ForeColor = &H0S
                            font1.bBold = False '== Printer.FontBold = False
                            lngXpos = mlPrtDx(64) '= Printer.CurrentX = 960
                            L1 = lngYpos '--save..- so we can print on same line..
                            '== Printer.Print "   Product Code: " & sBarcode;
                            lngYpos = mlPrintTextString(ev, "   Product Code: " & sBarcode, lngXpos, lngYpos, font1)
                            lngYpos = L1 '--  for same line.
                            lngXpos = lngRHSX '== Printer.CurrentX = lngRHSX
                            '== Printer.Print "Serial Number: " & sSerialNo
                            lngYpos = mlPrintTextString(ev, "Serial Number: " & sSerialNo, lngXpos, L1, font1)

                            '--  NOW print barcode line..-
                            lngXpos = mlPrtDx(32) '== Printer.CurrentX = 480
                            '--  Keep Ypos..  stay on same line..--
                            Call mlPrintTextString(ev, VB6.Format(mLngItemCount + 1, "00"), lngXpos, lngYpos, font1)

                            '-- Print the barcode..-  Keep on same line..
                            lngXpos = mlPrtDx(64) '= Printer.CurrentX = 960
                            font1.sName = msItemBarcodeFontName '==  Printer.FontName = msItemBarcodeFontName
                            font1.lngSize = mlItemBarcodeFontSize '==  Printer.FontSize = mlItemBarcodeFontSize
                            '==  Printer.Print "*" & sBarcode & "*";
                            Ly = lngYpos
                            '--  PRINT BARCODE barcode..- Keep Ypos..
                            Call mlPrintTextString(ev, "*" & sBarcode & "*", lngXpos, lngYpos, font1)
                            '- save stock pos..
                            '== Lx = lngXpos + Printer.TextWidth("*" & sBarcode & "*")
                            Lx = lngXpos + mlGetTextWidth(ev, "*" & sBarcode & "*", font1)

                            '== lngXpos = Printer.CurrentX '-- save for ALERT gif.-
                            '== lngYpos = Printer.CurrentY '-- save for ALERT gif.-
                            font1.sName = "Tahoma" '==  Printer.FontName = "Tahoma"
                            font1.lngSize = 9 '==  Printer.FontSize = 9

                            '==Printer.Print " StockId: " & sStockId;
                            L1 = lngYpos '--save..-
                            lngYpos = mlPrintTextString(ev, " StockId: " & sStockId, Lx, lngYpos, font1)
                            If bExtraToQuote Then
                                '==Printer.PaintPicture Me.PictureExtraPrint.Picture, lngXpos + 200, lngYpos + lngTxtHt
                                '==  Printer.PaintPicture(mObjPictureExtraPrint, lngXpos + mlPrtDx(15), Ly + lngTxtHt)
                                '== ev.Graphics.DrawImage(mObjPictureExtraPrint, lngXpos + mlPrtDx(15), lngYpos)
                                ev.Graphics.DrawImage(mObjPictureExtraPrint, Lx + mlPrtDx(15), lngYpos)  '==fixed 3057.0=
                            End If
                            lngYpos = L1 '-restore..-
                            lngXpos = lngRHSX '== Printer.CurrentX = lngRHSX
                            '== Printer.CurrentY = lngYpos
                            '--  PRINT SERIAL-NO barcode..-
                            font1.sName = msItemBarcodeFontName '==  Printer.FontName = msItemBarcodeFontName
                            font1.lngSize = mlItemBarcodeFontSize '==  Printer.FontSize = mlItemBarcodeFontSize
                            '== Printer.Print "*" & sSerialNo & "*"
                            lngYpos = mlPrintTextString(ev, "*" & sSerialNo & "*", lngXpos, lngYpos, font1)

                            '==3311.507= mIntLineCount = mIntLineCount + 1
                            intPageLineCount += 1
                            mLngItemCount = mLngItemCount + 1
                            If (mLngItemCount = lngTotalNoItems) Then '--last item..-
                                font1.sName = "Tahoma" '== Printer.FontName = "Tahoma"
                                font1.lngSize = 9 '== Printer.FontSize = 9
                                '==Printer.Print
                                lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1) '--blank line..-
                                lngXpos = mlPrtDx(32) '== Printer.CurrentX = 480
                                '== Printer.Print "= = = = End of Items = = = = = = "
                                lngYpos = mlPrintTextString(ev, "= = = = End of Items = = = = = = ", lngXpos, lngYpos, font1)
                                Call mbJobMaintPageFooter(ev)
                                ev.HasMorePages = False
                                Exit Function
                                '==End If
                            ElseIf (intPageLineCount >= 8) Or (mLngItemCount = lngTotalNoItems) Then '--end of page..- Then
                                '=3311.507=  ((mIntLineCount Mod 8) = 0) Or (mLngItemCount = lngTotalNoItems) Then '--end of page..-
                                '== GoSub PrintJob_Footer
                                Call mbJobMaintPageFooter(ev)
                                If (mLngItemCount >= lngTotalNoItems) Then '--last item..-
                                    ev.HasMorePages = False
                                Else  '-- still more items..--
                                    '--will be on next page..
                                    ev.HasMorePages = True
                                End If
                                Exit Function
                            Else  '==3057.0 = another line-
                                font1.sName = "Tahoma" '== Printer.FontName = "Tahoma"
                                font1.lngSize = 9 '==  Printer.FontSize = 9
                                '--blank line bewteen items..--
                                lngYpos = mlPrintTextString(ev, "", lngXpos, lngYpos, font1)
                            End If
                        End If '--WTY/non wty..- '-- Printing this item..--
                        '==Next ix
                    End While  '-- mIntMaintItemIx-
                    mIntWtyLoop += 1
                    mIntMaintItemIx = 0   '--  start items again..--
                    '==Next intWtyLoop '--does it twice..-
                End While  '== mIntWtyLoop < 3
                '--  fell out at end of loop..--  NO more to print.
                lngXpos = mlPrtDx(32) '== Printer.CurrentX = 480
                lngYpos = mlPrintTextString(ev, "= Fell out at: " & mIntWtyLoop & "/" & mIntMaintItemIx, lngXpos, lngYpos, font1)
                Call mbJobMaintPageFooter(ev)
                ev.HasMorePages = False

                '=Else  '--no parts.
                '==sText = sText + "<ul>NO Part Supplied:" + vbCrLf
                '=End If  '-list-
            Else  '--no items or font --
                ev.HasMorePages = False
            End If '--have font.-
        End If  '--maint page--

        mbPrintJobMaintForm_PageEvent = True '--ok..-
        '-- end print --
        '-- FOR NOW....  only one page..-
        '-- FOR NOW....  only one page..-
        '==ev.HasMorePages = False

        Exit Function

PrintJob_Error:

        lngError = Err().Number
        MsgBox("!! ERROR in Print method 'PrintJobMaintForm'." & vbCrLf & _
                       "Error code: " & lngError & " = " & ErrorToString(lngError), MsgBoxStyle.Exclamation)
        mbPrintJobMaintForm_PageEvent = False

    End Function '-- print..--
    '= = = = = = = =
    '-===FF->

    '-- Job Maint Customer Report  --
    '--    Docket Stuff PLUS Service Details --
    '---     and Pics on A4..
    '==
    '==    Updated- 3519.0129 29-Jan-2019= 
    '==           - Fixes to Startup AppPath to correctly find user Biz logo...
    '==           - Fixes to Print Customer Report for supporting multiple pages of images....


    Private Function mbPrintJobMaintCustomerReport_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
        Dim sHdr1, sText As String
        Dim s1, s2 As String
        Dim col1 As Collection
        Dim sCustText As String
        '==Dim lngTxtHt, lngTxtWidth As Integer
        '-- A Rectangle has Left, Top, Width, height--
        '=3203.113-
        Dim rectPageBounds As Rectangle = ev.PageBounds    '-- printable rectangle-
        Dim intPrintHeight As Integer = rectPageBounds.Height

        Dim ix, L1 As Integer
        '= Dim Ly, L1, Lx, lngHistoryPos As Integer
        '= Dim lngTermsX, lngTermsY As Integer
        '== Dim lngTermsDepth As Integer
        Dim lngLHSX, lngRHSX As Integer
        '= Dim lngSigX, lngSigY As Integer
        Dim lngMainTop, lngMainDepth As Integer
        '= Dim lngLeftColWidth, lngRightColWidth As Integer
        Dim lngBoxMargin As Integer = 0

        '= Dim lngFillColour As Integer
        '= Dim sDate1, sDatePrev As String
        Dim font1 As userFontDef
        '= Dim txtColour1 As textColour
        '= Dim msGoodsIncare As String
        Dim intCustDepth As Integer = 120
        Dim intXpos, intYpos, intYposStartWork As Integer
        Dim intMainTextWidth As Integer = 472
        Dim intMainDetailTop, intMainDetailDepth As Integer  '= = intPrintHeight - 84 '== 916
        Dim intRHColumnTop As Integer

        mbPrintJobMaintCustomerReport_PageEvent = False
        lngLHSX = 40 '=  240 twips.-
        '=mIntMaintPage += 1   '--  another page..-

        lngMainTop = mlPrtDx(133) '== 2000 twips..-
        lngMainDepth = mlPrtDx(640) '== 9600 twips..-
        intYpos = 0
        intXpos = lngLHSX
        mDefaultUserFont.sName = "Lucida Sans Unicode"
        font1 = mDefaultUserFont '--  8pt..-
        font1.sName = "Lucida Sans Unicode" '== Printer.FontName = "Tahoma"

        mIntMaintPage += 1   '-- First (1) or another page..-
        If mIntMaintPage = 1 Then  '--First page-
            sHdr1 = "Customer Service Report"
            mStrHeaderDate = FormatDateTime(Today, DateFormat.LongDate)

            Call mbPrintJobHeader(ev, sHdr1, mLngPriorityColour, "", False)
        End If '-p1-
        lngMainTop = mIntMainDetailTop  '=FROM Hdr Function.
        intYpos = lngMainTop  '--jump header.
        '--  customer Info..--
        font1.lngSize = 12 '==  Printer.FontSize = 12
        msCustomerPrint = mColCust.Item("CustomerPrint")
        s1 = "Job No: " & VB6.Format(mLngJobNo, "000") & ".    Customer: " & msCustomerPrint
        s2 = "Page " & mIntMaintPage
        '-- Put Cust name in Pale-Green bar..-
        Call mlDrawBox(ev, intXpos, intYpos, mlPrtWidth, 24, RGB(&H98, &HFB, &H98))
        '== intYpos = mlPrintTextInBox(ev, s1, intXpos, intYpos, 0, mlPrtWidth, 24, False)
        font1.lngSize = 10
        L1 = mlPrintTextString(ev, s1, intXpos, intYpos, font1)
        '-   print page no.
        intYpos = mlPrintTextString(ev, s2, intXpos + mlPrtWidth - 60, intYpos, font1)

        font1.lngSize = 8 '= Printer.FontSize = 8
        intYpos += 10     '--jump to below green bar.
        intXpos = lngLHSX
        '-- draw main detail box-
        intMainDetailTop = intYpos
        intMainDetailDepth = intPrintHeight - intYpos - 60 '== 916
        Call mlDrawBox(ev, intXpos, intMainDetailTop, mlPrtWidth, intMainDetailDepth)

        intXpos += 20
        intYpos += 20
        intRHColumnTop = intYpos   '-- save for top of pictures column..-

        '-- Compute Depth of RH pic-column, and print pictures if any..
        Dim intPicAreaDepth As Integer = (intMainDetailDepth - (intRHColumnTop - mIntMainDetailTop))
        Dim intPicCount As Integer
        Dim intPicsFrameWidth As Integer = 216
        Dim intPicsLeftMargin As Integer = K_LEFT_MARG + mlPrtWidth - intPicsFrameWidth  '= - 16
        '=Dim intPicsFrameWidth = mlPrtWidth - intPicsLeftMargin  '--320 ??-
        Dim intPicsFrameDepth As Integer = intPicsFrameWidth
        Dim intMaxPics, intPicsOnShow, px As Integer
        Dim intGap As Integer = 8

        '= All pages-- Print some images if there are any..
        If (Not (mListJobImages Is Nothing)) AndAlso (mListJobImages.Count > 0) Then '= (mIntReportImagesPrinted > 0) Then
            '== Pics  on RHS of Page 1 etc.  -
            If (mIntReportImagesPrinted < mListJobImages.Count) Then  '-still some to do..
                '= Print some on this page.
                intPicCount = (mListJobImages.Count - mIntReportImagesPrinted)
                intMaxPics = intPicAreaDepth \ (intPicsFrameDepth + intGap)
                If (intMaxPics >= intPicCount) Then
                    intPicsOnShow = intPicCount  '--don't need all that space.
                Else
                    intPicsOnShow = intMaxPics '-- can only print intMaxPics (what we have space for)..
                End If
                font1.lngSize = 7
                L1 = mlPrintTextString(ev, "--Have " & CStr(intPicCount) & " pics selected. " & _
                                           "Room for: " & intMaxPics & ".. -- ", intPicsLeftMargin, intRHColumnTop - 12, font1)
                If (intPicsOnShow > 0) Then
                    Dim imageItem1 As Image
                    Dim intFramexTop, intFramexLeft As Integer
                    '--Max rect must be square.
                    Dim intImageMaxWidth As Integer = intPicsFrameWidth - 8
                    Dim intImageMaxHeight As Integer = intPicsFrameDepth - 8
                    '- Rect pos-
                    Dim intImageRectTop As Integer
                    Dim intImageRectleft As Integer
                    '-Rectangle has Left, Top, Width, height--
                    '-- Make the Rectangle aspect the SAME as the Image..--
                    Dim intRh, intRw As Integer   '--rect-
                    '-- draw empty frames..
                    '--    and put in the pictures..
                    For px = 0 To (intPicsOnShow - 1)
                        intFramexLeft = intPicsLeftMargin
                        intFramexTop = intRHColumnTop + ((intPicsFrameDepth + intGap) * px)
                        Call mlDrawBox(ev, intPicsLeftMargin, intFramexTop, _
                                              intPicsFrameWidth, intPicsFrameWidth)
                        '--  make "rectangle"..
                        '--  Print Item Image if any..-
                        '-test-
                        '= MsgBox("About to print image index: " & (px + mIntReportImagesPrinted), MsgBoxStyle.Information)
                        imageItem1 = mListJobImages(mIntReportImagesPrinted)
                        If (Not (imageItem1 Is Nothing)) AndAlso _
                                              ((imageItem1.Height > 0) And (imageItem1.Width > 0)) Then
                            If (imageItem1.Height > imageItem1.Width) Then   '--portrait-
                                intRh = intImageMaxHeight
                                intRw = CInt(intImageMaxWidth * (CDbl(imageItem1.Width) / imageItem1.Height))
                            Else '-landscape-
                                intRw = intImageMaxWidth
                                intRh = CInt(intImageMaxHeight * (CDbl(imageItem1.Height) / imageItem1.Width))
                            End If
                            '-- align rectangle to RH side of main box..
                            intImageRectleft = intFramexLeft + 4  '= lngTermsX + lngLeftColWidth - intRw - 2
                            '--and at bottom of rect.
                            intImageRectTop = intFramexTop + 4  '=lngMainTop + lngMainDepth - intRh - 2
                            Dim rectImage1 As New Rectangle(intImageRectleft, intImageRectTop, intRw, intRh)
                            ev.Graphics.DrawImage(imageItem1, rectImage1)
                        End If  '-image-
                        mIntReportImagesPrinted += 1  '--count them..
                    Next px
                End If  '-maxPics-
            Else  '- no pic-
                L1 = mlPrintTextString(ev, "-- --", intPicsLeftMargin, intRHColumnTop, font1)
                L1 = mlPrintTextString(ev, "-- No more pica to do.--", intPicsLeftMargin, L1, font1)
                L1 = mlPrintTextString(ev, "-- --", intPicsLeftMargin, L1, font1)
            End If  '--pics--
        Else  '-no images-
            L1 = mlPrintTextString(ev, "-- --", intPicsLeftMargin, intRHColumnTop, font1)
            L1 = mlPrintTextString(ev, "-- No picture selected.--", intPicsLeftMargin, L1, font1)
            L1 = mlPrintTextString(ev, "-- --", intPicsLeftMargin, L1, font1)
        End If  '-images remaining.

        Dim intDetailDepthRemaining As Integer
        '-- First page has Goods and first of the PICS etc.--
        font1.lngSize = 8   '-restore normal font size..
        If mIntMaintPage = 1 Then  '--First page- Seriously-
            Try
                '-- Goods.--
                font1.bBold = True
                font1.bUnderline = True
                intYpos = mlPrintTextString(ev, "Goods In Care", intXpos, intYpos, font1)
                sCustText = CStr(mColResultGoods.Item(1))
                '--3107.907= Print Goods or ONSITE stuff..-
                font1.bBold = False
                font1.bUnderline = False
                intYpos = mlPrintTextInBox(ev, sCustText, intXpos, intYpos, _
                                                 0, intMainTextWidth, 140, False)
                '- blank line.
                intYpos = mlPrintTextString(ev, "", intXpos, intYpos, font1)
                '== intYpos = mlPrintTextString(ev, sCustText, intXpos, intYpos, font1)

                '-- Problems reported..
                '--add problems..
                If Not mbQuotation Then
                    font1.bBold = True
                    font1.bUnderline = True
                    intYpos = mlPrintTextString(ev, "Job Requirements" & vbCrLf, intXpos, intYpos, font1)
                    '== sCustText = "<b>Job Requirements" & vbCrLf & vbCrLf & mStrProblem
                    '--3107.907= PrintGoogs or ONSITE stuff..-
                    intYpos = mlPrintTextInBox(ev, mStrProblem, intXpos, intYpos, _
                                                   0, intMainTextWidth, 300, False)
                    font1.bBold = False
                    font1.bUnderline = False
                End If
                '--parts and service-
                '--parts..--
                '--  AND SERVICE CHARGES..--
                '--   and costs...
                sText = msBuildSuppliedItemsText()
                '-- Check if fits  on page..
                intDetailDepthRemaining = intMainDetailDepth + intMainDetailTop - intYpos

                intYpos = mlPrintTextInBox(ev, sText, intXpos, intYpos, _
                                   0, intMainTextWidth, intDetailDepthRemaining, False)
                intYpos += 18
                '- Mark start of Work.
                intYposStartWork = intYpos

                '-- Print Work Record AFTER Pics, as it may go on to more pages..
                '--onto history page (s)..-
                '= If (msWorkRecord <> "") Then
                '-- We are at intYposStartWork on P1..
                '-- CHECK if WorkDetails will fit on Page 1.
                intYpos = intYposStartWork

                '-- PAGE-1 only-  Prepare final work details text and Footnote..
                msReportTextRemaining = "<b>Work Details-" & vbCrLf & msWorkRecord & vbCrLf & vbCrLf & _
                                            "<b>Please Note: " & vbCrLf & msDeliveryFootNote & vbCrLf & _
                                               "-- The end --"
            Catch ex As Exception  '-- P1 catch-
                ev.HasMorePages = False
                MsgBox("Error in 'mbPrintJobMaintCustomerReport_PageEvent' Page-1 " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                Exit Function
            End Try  '-Page-1 try--
        Else  '- PAGE-2 or later..-
            '-- print 2nd+ page header..
            '-- depth test remainder and print -
            '- start from after header.
            intYposStartWork = intYpos
        End If  '-page-

        '-- All pages.. print some text..
        Dim intTextLengthRemaining As Integer
        '= Dim intDetailDepthRemaining As Integer
        Try
            '-- Check if fits  on page..
            intDetailDepthRemaining = intMainDetailDepth + intMainDetailTop - intYposStartWork
            intYpos = intYposStartWork
            s1 = msReportTextRemaining
            intTextLengthRemaining = Len(msReportTextRemaining)
            If (s1 = "") Then
                ev.HasMorePages = False
                '-- finished-
            Else  '-have some.
                '-- test depth.--  Allow 3 lines (24 px) as margin.
                L1 = mlPrintTextInBox(ev, s1, intXpos, intYpos, _
                                                 0, intMainTextWidth, intDetailDepthRemaining, False, , True)
                If ((L1 + 24) <= (intMainDetailTop + intMainDetailDepth)) Then
                    '--ok- Didn't go over the botton.
                    '-- will fit..  print it..
                    intYpos = mlPrintTextInBox(ev, msReportTextRemaining & " (all done)..", intXpos, intYpos, _
                                       0, intMainTextWidth, intDetailDepthRemaining, False)
                    msReportTextRemaining = ""
                    ev.HasMorePages = False
                    '-- finished-
                    '= L1 = mlPrintTextString(ev, "-- The end --", intXpos, intYpos, font1)
                Else
                    '--needs extra pages..
                    '-- Find how much will fit on this page..
                    '--  print it, and leave Remainder for next page..-
                    If mbSplitTextToFit(ev, (intDetailDepthRemaining - 32), _
                                                             msReportTextRemaining, intMainTextWidth, s1, s2) Then
                        msReportTextRemaining = s2  '--save for next page..
                        '--s1- will fit..  print it..
                        intYpos = mlPrintTextInBox(ev, s1, intXpos, intYpos, _
                                           0, intMainTextWidth, intDetailDepthRemaining, False)
                        font1.bItalic = True
                        L1 = mlPrintTextString(ev, "-- " & " Continued on Page : " & _
                                                           CStr(mIntMaintPage + 1) & "  --", intXpos, intYpos, font1)
                        ev.HasMorePages = True
                    Else
                        '-- couldn't fit anything in.-
                        font1.bItalic = True
                        L1 = mlPrintTextString(ev, "-- Continued on Page : " & _
                                                           CStr(mIntMaintPage + 1) & "  --", intXpos, intYpos, font1)
                        ev.HasMorePages = True
                    End If  '-if fits-
                End If  '--depth test-
            End If  '-have text-
        Catch ex As Exception
            ev.HasMorePages = False
            MsgBox("Error in 'mbPrintJobMaintCustomerReport_PageEvent' Page-" & mIntMaintPage & vbCrLf & _
                                                                              ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try

        '-- But in case there are more images to print..
        '= 3510.0129=
        If (Not (mListJobImages Is Nothing)) AndAlso (mListJobImages.Count > 0) Then
            If (mIntReportImagesPrinted < mListJobImages.Count) Then  '-still some to do
                ev.HasMorePages = True
            End If
        End If
 
        '- all page ends..-
        Call mbJobMaintPageFooter(ev)
        mbPrintJobMaintCustomerReport_PageEvent = True

    End Function  '-- CustomerReport--
    '= = = = = = = = = = = = = = = = = 
    '-===FF->

    '--  RA's  F O R M S ==
    '--    PRINT PAGE EVENT.--

    '-- Print the RA form.--

    '== Private Function mbPrintRAForm_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
    '== End Function '-- print..--
    '= = = = = = = = = = = = 
    '--  RA Supplier LABEL --

    '--  Print Shipping Label --
    '--  Print Shipping Label --

    '== Public Function mbPrintShippingLabel_PageEvent(ByVal ev As PrintPageEventArgs) As Boolean
    '== End Function '--ship label..-
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
            Case printDocType.Receipt
                Call mbPrintReceipt_PageEvent(ev)
            Case printDocType.QuoteJob
                Call mbPrintQuoteJob_PageEvent(ev)
            Case printDocType.NewJob
                Call mbPrintNewJobForm_PageEvent(ev)
            Case printDocType.JobMaint
                Call mbPrintJobMaintForm_PageEvent(ev)
            Case printDocType.JobMaintCustomerReport
                Call mbPrintJobMaintCustomerReport_PageEvent(ev)
                '= Case printDocType.RAForm
                '=     Call mbPrintRAForm_PageEvent(ev)
                '= Case printDocType.RAShippingLabel
                '=     Call mbPrintShippingLabel_PageEvent(ev)
            Case Else

        End Select

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

        miPageNo = 0

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

    Public Function PrintReceipt(ByRef colReportLines As Collection) As Boolean

        PrintReceipt = False
        '-- save stuff to print..-
        mColReportLines = colReportLines
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.Receipt

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Receipt printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        PrintReceipt = True
        '-- WE use PHYSICAL PAGE- as Graphics Origin..-
        printDocument1.OriginAtMargins = False
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            '== MessageBox.Show(ex.Message)
            MsgBox("Error in printing Receipt/docket.." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintReceipt = False
        End Try

    End Function  '--PrintReceipt-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    Public Function PrintQuoteJob() As Integer

        PrintQuoteJob = -1  '--failed.
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.QuoteJob
        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Quote printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        PrintQuoteJob = 0  '-- is OK. 
        '-- WE use PHYSICAL PAGE- as Graphics Origin..-
        printDocument1.OriginAtMargins = False
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            MsgBox("Error in printing Quote Job..." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintQuoteJob = -1  '--failed.
        End Try
    End Function  '--quote..-
    '= = = = = = = = = = = =  = =
    '-===FF->

    Public Function PrintNewJobForm() As Integer

        PrintNewJobForm = -1  '--failed..-False
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.NewJob
        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("S.Agreement printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        PrintNewJobForm = 0  '-- is OK.  True
        '-- WE use PHYSICAL PAGE- as Graphics Origin..-
        printDocument1.OriginAtMargins = False
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            MsgBox("Error in printing New Job..." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintNewJobForm = -1  '--failed..-
        End Try
    End Function  '--PrintNewJob-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- Job Maint- (Service Record)-

    Public Function PrintJobMaintForm() As Integer

        PrintJobMaintForm = -1  '--failed..-
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.JobMaint

        '--  check and set printer..-
        If (msSelectedPrinterName = "") Then
            MsgBox("Service Record printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '--  initialise page vars --
        '==3311.507= mIntLineCount = 0  '--itms on page.-

        '==lngItemCount = 0
        mIntWtyLoop = 1    '--  ready for first.-
        mIntWtyCount = 0   '--outer filter loop..--
        mIntMaintPage = 0       '--  actual page no..
        mIntMaintItemIx = 0
        mLngItemCount = 0
        PrintJobMaintForm = 0   '-- zero is ok..
        '-- WE use PHYSICAL PAGE- as Graphics Origin..-
        printDocument1.OriginAtMargins = False
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            MsgBox("Error in printing Service/Delivery record..." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintJobMaintForm = -1  '-- is failed--
        End Try

    End Function  '--PrintJobMaint-
    '= = = = = = = = = = = = = = =  =
    '-===FF->

    '= 3203.110=
    '-- Job Maint CustomerReport- (from Service Record)-

    Public Function PrintJobMaintCustomerReport(ByRef colBusiness As Collection, _
                                                ByRef colCustomer As Collection, _
                                                ByVal intJobNo As Integer,
                                                ByVal bIsQuotation As Boolean, _
                                                ByRef colResultGoodsInCare As Collection, _
                                                ByVal strJobRequirements As String, _
                                                ByRef colServiceItems As Collection, _
                                                ByRef colPartsItems As Collection, _
                                                ByVal sShowLabourCost As String, _
                                                ByVal decTotalLabourValue As Decimal, _
                                                ByVal decTotalItemValue As Decimal, _
                                                ByVal strWorkRecord As String, _
                                                ByVal strDeliveryFootnote As String, _
                                                ByRef userLogo As Image, _
                                                ByRef asFileTitles() As String, _
                                                ByRef listJobImages As List(Of Image), _
                                                ByVal strSelectedPrinterName As String) As Integer

        PrintJobMaintCustomerReport = -1  '--failed..-
        '--  SET static var to SAVE doc-type for page-event...--
        mlPrintDocType = printDocType.JobMaintCustomerReport

        '--  check and set printer..-
        If (strSelectedPrinterName = "") Then
            MsgBox("Report printer not specified", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '-- save parms.-
        mColBusiness = colBusiness
        mColCust = colCustomer
        mLngJobNo = intJobNo
        mbQuotation = bIsQuotation

        mColResultGoods = colResultGoodsInCare
        mStrProblem = strJobRequirements
        '== mColCustReportLines = colReportLines  '-- parts and tasks..

        mColServiceItems = colServiceItems
        mColPartsItems = colPartsItems
        msShowLabourCost = sShowLabourCost
        mCurTotalLabourValue = decTotalLabourValue
        mCurTotalItemValue = decTotalItemValue

        msSelectedPrinterName = strSelectedPrinterName  '--SAVE--
        msWorkRecord = strWorkRecord
        msDeliveryFootNote = strDeliveryFootnote
        mObjUserLogo = userLogo

        '=mDtPictures = dtPics
        masFileTitles = asFileTitles
        mListJobImages = listJobImages

        '=3519.0129=
        mIntReportImagesPrinted = 0

        '--  initialise page vars --
        '=3311.507= mIntLineCount = 0  '--itms on page.-
        '==lngItemCount = 0
        mIntWtyLoop = 1    '--  ready for first.-
        '= mIntWtyCount = 0   '--outer filter loop..--
        mIntMaintPage = 0       '--  actual page no..
        msReportTextRemaining = ""
        mLngItemCount = 0

        PrintJobMaintCustomerReport = 0   '-- zero is ok..
        '-- WE use PHYSICAL PAGE- as Graphics Origin..-
        printDocument1.OriginAtMargins = False
        Try
            '--  set printer selected..--
            printDocument1.PrinterSettings.PrinterName = msSelectedPrinterName
            '--  start the printer..--
            printDocument1.Print()
        Catch ex As Exception
            MsgBox("Error inPrintJobMaintCustomerReport Public Method..." & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            PrintJobMaintCustomerReport = -1  '-- is failed--
        End Try

    End Function  '--PrintJobMaintCustomerReport-
    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->


    '--PrintRA-

    '== Public Function PrintRAForm() As Boolean

    '== End Function  '--PrintRAForm-
    '= = = = = = = = = = = = = = = =

    '== Public Function PrintShippingLabel() As Boolean

    '== End Function  '-- PrintShippingLabel-
    '= = = = = = = = = = = = = =

    '= = = = = = = = = = = = = =
    '== end class.==
End Class  '--print docs-
'= = = = = = = = = = = = =