Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections.Generic

Friend Class frmNewRA
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = = = =  = =
	'-- JobTracking-  RA Form..--
	'=== Return Merchandise Authorisation ===
	'--- grh -- 24-Nov-2009- ====
	'--- grh -- 31-Jan-2010-= Fixes..===
	'--- grh -- 21-Feb-2010-= Can Lookup from BARCODE (No Serial).. if desired..
	'----            ==  Redesign form.. add TAB ctl.. ListViewGoods to lookup Invoice..===
	'--- grh -- 26-Feb-2010-= Use SerialAudit Table to Lookup SerialNo..
	'--- grh -- 04-Mar-2010-= Fixes..  Print RequestNotesHistory on RA form..
	'--- grh -- 31-Mar-2010-= Add CancelForever Button..
	'--- grh -- 02-Apr-2010-= Make "Activate/LoadRA-Record: re-usable...--
	'-------    ----     So User can progress through RA updates --
	'-------    ----        without having to exit the form and reload.---
	'--- grh -- 08-Apr-2010-= NEW RA:  Fix malfunctioning Invoice Selection (disappearing.)..--
	'----                ---  Also allow change of supplier other than originator of Invoice..-
	'--- grh -- 17-Apr-2010-= NEW RA:  Permit Dulicate SerialNo if previous RA refused or Cancelled...--
	'--- grh -- 19-May-2010-= NEW RA:  DO NOT STRIP leading zeroes from SERIAL-No....--
	'--- grh -- 21-May-2010-= Change STATUS description "30-RMA-Received" to "30-RMA-Granted"..--
	'--- grh -- 25-May-2010-= RequestNotes History and new Notes added to Goods Panel..- --
	'--- grh -- 29-May-2010-= Printing SENT to printer Confirmation...- --
	'--- grh -- 03-Jun-2010-= RE-Open Cmd so Goods can be sent BACK AGAIN..- --
	'----         Also some visual changes to colour and shapes.-
	'--- grh -- 14-Jun-2010-= Separate shipping label printing....- --
	'------    Add signing boxes to RA printout..-
	'--- grh -- 30-Jul-2010-= Enforce SQL max barcode length of 31..- --
	'--- grh -- 31-Aug-2010-= RA REcord Printout.. Make label colour Khaki (&H90E0)..- --
	'--- grh -- 18-Sep-2010-= Rev-2474-  Add "Credited" to Goods Results..- --
	'--- grh -- 08-Oct-2010-= Rev-2478-  Allow NewSupplier from Update (RMA Request panel).- --
	'--- grh -- 15-Nov-2010-= Rev-2787-  JobMatix-  New RA:  Enable printing ONLY after new record created...- --
	'--- grh -- 17-Jan-2011-= Rev-2797-  clean up label caption BG's.. --
	'--- grh -- 30-Jan-2011-= Rev-2802-  Picture1 needs Jobmatix logo as backup.. --
	
	'===  Rev: 2907 ==
	'--- grh -- 15-Jun-2011-= Rev-2907- Item (Origin) can Transfer to Stock..-- . --
	
	'===  Rev: 2916 ==
	'--- grh -- 05-Aug-2011-= Can open NEW RA for duplicate SerialNo if original was COMPLETED..-- . --
	
	'===  Rev: 3010 ==
	'--- grh -- 19-Oct-2011-= Multiple Retail Hosts version.. --
	
    '===  Rev: 3013 ++   ==
    '--- grh -- 23-Nov-2011-=  UPGRADED vb.net version... --

    '===  Rev: 3031.2  ==
    '--- grh -- 23-Mar-2012-=  Fix labVersion.... --
    '==
    '===  Rev: 3031.11  ==
    '--- grh -- 29-Mar-2012-=  Fix Today date functions...... --
    '==
    '===  Rev: 3049.0  ==
    '--- grh -- 01-May-2012-=  Fix Initial focus.. --
    '==      and tidy up form..
    '===  Rev: 3049.3  ==
    '--- grh -- 03-May-2012-=  
    '==      drop double exit confirmation...
    '==
    '===  Rev: 3053.0- 16May2012.=  ==
    '--   After Create..  Lock SerialNo to Disable further change.-
    '==
    '===  Rev: 3053.1- 21May2012.=  ==
    '--   After Create..  Lock Symptoms, cmdnewSupplier etc...-
    '==
    '===  Rev: 3057.0- 23May2012.=  ==
    '--   After Create..  set mbCreate flag to bypass ENTER key on serialNo/Barcode...-
    '==
    '===  Rev: 3067.0- 25Jul2012.=  ==
    '--   >>  Add Help Provider..-
    '==
    '==   grh 20-May-2013== Build 3077.520==
    '--       >>  ALLOW leading zeroes on PRODUCT BARCODE..--
    '==
    '== grh= 11-Jun-2013== Build 3077.611==
    '==        >>  Add 'chkKeepScannedLeadZeroes'
    '==            (to optionally disable stripping of PRODUCT BARCODE leading zeroes and spaces..)
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '== grh== Build 3101.922== 22-Sep-2014=10:03am =
    '==      For JobMatix v3.1 and JobMatixPOS--
    '==
    '==     >>  RAs now a DLL (JMx31RAs)..
    '==       (with ADO.net and system.data.oleDb.
    '==
    '==     >> =3101.1022- Must Get Product Barcode.--
    '==           Now must have Stock_id to look up serial.
    '==     >> =3101.1029- NO !  Lookup Serial only..  Handle multiple result lines..
    '==
    '==     >> =3101.1111- 
    '==     >>  RAs now Compiled In MAIN ASSEMBLY (JobMatix31)..
    '==           Avail. Printers are discovered on Load.
    '==
    '==     == 3103.0129- 
    '==     >>  Re-arranged the Printer Selection/buttons stuff..
    '==
    '==     == 3103.0423-  23April2015 
    '==     >>  New-RA- Add F2 Lookup for barcode text box...
    '==
    '==  NEW VERSION 3203.1227-  27-Dec-2015=
    '==   With Attachments Table and CLASS plus frmAttachments -
    '==    >> Add new Main TAB Control To enclose Progress Tabset and Attachments Tab---
    '==    >>  New RA can have Item Image and it can print..
    '==
    '==  grh 3203.212-  12-Feb-2016=
    '==    >> Now can print "Job" (RA) Labels to stick on RA Item.
    '==             With new combo for Label printer..
    '==    >> New RA-  add button to refresh Supplier ADDRESS/Phone..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '==
    '==  NEW Version for RAs only.  (for new JMxRAs330.exe)--
    '==
    '==  JMxRAs330.exe
    '==
    '==  grh. 3301.222= 22Feb2016- 
    '==      >>  Tidying up....
    '==       >> Check if Jobs Table exists before allowing JobNo..
    '==
    '==  NEW VERSION 3.3.3311.305-  05-Mar-2016=
    '==
    '==    >> Has Updated Attachments to include MS Word and Excel documents -
    '==    >> Split RAs back into separate EXE..
    '==    >> All systemInfo work now via Class clsSystemInfo ..
    '==    >> All localSettings work now via Class clsLocalSettings..
    '==
    '==    >> NewRA- GetSerialInfo.. Catch SALE info if any, and 
    '==                ask if customer is to be used for this new RA..
    '==
    '==  grh 3.3.3311.328-  28-Mar-2016=
    '==        >> Re-vamped Progress Tab- One panel for complete process.
    '==
    '==  grh 3.3.3311.420-  20-Apr-2016=
    '==        >> Fix to enable Origin Frame for Non-serial item..
    '==             And to enable JobNo option.
    '==
    '==
    '==  grh 3311.507-
    '==  ----------------------
    '==        >>- Fix to New RA's Form..  Replacement SerNo is optional..
    '==                   and if any, is recorded in RA ResultComment as "serno=..." 
    '==
    '==  grh 3.3.3311.817-  17-Aug-2016=
    '==        >> Add column sort to Invoices ListView (listViewGoods)...
    '==
    '==
    '==  -- 3327.0119- 19-Jan-2017-
    '==         >>-- TABLE RAItems-  Expand column RA_Symptoms from 240 to 511 chars-..-- 
    '==                  and user must now TAB away from Symptoms textbox.
    '== = = = =
    '==
    '==  -- 3357.0205- 05-Feb-2017-
    '==         >>-- Update to go with Updated POS 3307==-- 
    '==             (SupplierReturns Update Function to be called from RAs.)...
    '==         >>--RAs-  Add column RM_SerialAudit_id" "to RAItems Table ==-- 
    '==               and Expand RM_ItemBarcode to 40 chars.
    '==         >>--RAs-  IF Retail System is JobMatix POS, then call PO-GoodsReturned when Goods Sent. ==-- 
    '== = = = = = = = = = =
    '==
    '==   v3.4.3403.0611 -- 11Jun2017= x-
    '==      -  New RA- Extract and show the Item Sales InvoiceNo. and SaleDate in Customer Box....
    '==                  AND- Add Info to RA Printout.. AND recover this Sales Info when re-editing the RA.  
    '==
    '==   v3.4.3403.0627 -- 27Jun2017= x-
    '==       -- Tidy up labUserPrompt0..
    '==   v3.4.3403.0711 -- 11Jul2017= - FIX UP for release.
    '==      -- For NEW RA's..  Save request notes if any...-
    '==      -- 3403.0719..  Disable RequestNotes until NewRA validated (with symptoms etc)...-
    '==                          (Was crashing in premature Create..)
    '==
    '==   v3.4.3411.0128= RAs back to being EXE..
    '==       3411.0128 -txtCustNo.Focus()
    '==       3411.0128 - Stock doesn't need Customer..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


	Const k_SSTAB_CUST As Short = 0
	Const k_SSTAB_RMA As Short = 1
	Const k_SSTAB_GOODS As Short = 2
	'= = = = = = = = = = = = = =
	
    '=3311.326= Const k_MAXGOODSRESULT As Short = 4 '--  [0..4]..
	'= = = = = = = = = = = = = =
	
	Const K_STATUS_CREATED As String = "10-Created"
	Const K_STATUS_RMA_REQUESTED As String = "20-RMA-Requested"
	Const K_STATUS_RMA_RECEIVED As String = "30-RMA-Granted"
	Const K_STATUS_GOODSSENT As String = "50-GoodsSentToSupplier"
	Const K_STATUS_GOODSCOMPLETED As String = "70-GoodsCompleted"
	Const K_STATUS_RMA_REFUSED As String = "95-RMA-Refused"
	Const K_STATUS_RMA_CANCELLED As String = "97-RMA-Cancelled"

    Private Const k_RA_PrtSettingKey As String = "RA_PRTCOLOUR"
    Private Const k_RA_PrtLabelSettingKey As String = "RA_PRT_LABEL"

	'= = = = = = = = = = = = = = = = = = = = = = = =

    Private mbIsInitialising As Boolean = True

    Private mbActive As Boolean = False
    Private mbUpdating As Boolean = False  '--not new.-
    Private mbCreated As Boolean = False
    Private mbPrinted As Boolean = False
    Private mbUserCancelled As Boolean = False
	
    Private msBusinessABN As String = ""
    Private msBusinessName As String = ""
    Private msBusinessAddress1 As String = ""
    Private msBusinessAddress2 As String = ""
	
    Private mCnnJobs As OleDbConnection '== ADODB.Connection '--SQL jobs connection --
    '==3301.222-
    Private mbHasJobTracking As Boolean = False

	'== Private mCnnJet  As ADODB.connection    '--  Retail Manager Jet connection..--
    '== Private mColJetDBInfo As Collection

    '=3357.0206=
    Private msServer As String = ""
    Private msSqlDbName As String = ""

	Private mColSqlDBInfo As Collection
	
    Private msOriginalStatus As String = "" '-- starting status on Activation..-
    Private msInterimStatus As String = "" '-- starting status on Activation..-
    Private msCurrentStatus As String = "" '-- starting status on Activation..-
	'-- new RA data --
	'-- new RA data --
    Private mlRA_id As Integer = -1
    Private mlStaffId As Integer = -1 '--save staff-id--
    Private mlCustomerId As Integer = -1 '--save cust-id--
    Private mlJobId As Integer = -1 '--AMEND JOBno, OR (Created) Identity retrieved..-
    Private mbV20_Only As Boolean = False
    '= = = = = = = = = = = =  = =

    '=3203.212=
    '==3311.305=Private mSdSystemInfo As New clsStrDictionary  '== Scripting.Dictionary
    '==3311.305=Private mColSystemInfo As Collection
    '= = = = = =
    '==Private msSettingsPath As String = ""
    '==3311.305=
    Private mLocalSettings1 As clsLocalSettings
    '=3311.305=
    Private mSysInfo1 As clsSystemInfo

    Private msCustomerBarcode As String = "" '--main cust. identifier..-
    Private msStaffName As String = ""
    Private msCustomerName As String = ""
    Private msCustomerPhone As String = ""
    Private msCustomerMobile As String = ""
    Private msCustomerCompany As String = ""
    Private msPriority As String = ""
    '= = = = = = = = = = = = = = = = = = = =

	'--  printers--

    Private msColourPrinterName As String = ""
    '=3203.212=
    Private msItemLabelPrinterName As String = "" '=3203.212=
    Private msDefaultPrinterName As String = ""

    Private msLocalSettingsPath As String = ""
    Private msItemLabelBarcodeFontName As String = ""
    Private mIntItemLabelBarcodeFontSize As Integer = 9


    '==Private mlProgress As Long  '--progress value..-
	'= = = = =  = =  = = = = =
	'-- Current NEW Item..--
	Private mColItemFields As Collection
	Private mColInvoiceFields As Collection '--Includes Supplier Name/Adress.--
	
	'--  current RA for updating..--
	Private mColRAFields As Collection
	'-- referred Job if any..-
	Private mColJobFields As Collection
	'--current item..-
    Private msSerialNo As String
    '==  -- 3357.0205- 05-Feb-2017-
    Private mIntSerialAudit_id As Integer = -1

	Private msItemSupplierCode As String
	Private msItemBarcode As String
	Private msDescr As String
	Private msCat2, msCat1, msCat3 As String
	
    Private mlStockId As Integer = -1
    Private mlGoodsId As Integer = -1
    Private mlGoodsLineId As Integer = -1
	Private mCurSellEx As Decimal
	'= = = = = = = = = = = = = = =
	'== Private mRsGoods As ADODB.Recordset
	'= = = = = = = = = = = = = = =
	
	Private msOrigSupplier As String
	Private mlSupplierId As Integer
	Private msSupplier As String
	Private msSupplierBarcode As String
	Private msSupplierAddressInfo As String
	Private msSupplierMainPhone As String
	Private msSupplierMainFax As String
	Private msSupplierMainEmail As String
	
	Private msOrigin As String
	
	Private msActionUpdate As String '-- "UPDATE" SQL for update save..
	Private msShowRAProgress As String
	Private msInterimRAProgress As String
    Private msCurrentNotes As String

    Private mColorNextStep As Color
	'= = = = = = = = = = =  =  = =  =
    	
    '-- Retail Host..-
	Private mRetailHost1 As _clsRetailHost
    Private msRetailHostname As String = ""
    '= = = = = = = = = = = = = = = == = = = = = = = = =

    '--=3203.1222=
    '=  Context menu for Pasting- attachment file name-
    '--  Popup menu for Right click on txt File name..-
    Private mContextMenuPasteFileName As ContextMenu
    Private WithEvents mnuPasteFileName As New MenuItem("Paste File")
    Private WithEvents mnuPasteFileSep1 As New MenuItem("-")
    Private WithEvents mnuPasteFileSep2 As New MenuItem("-")

    '-- Dummy to disable default menu-
    '-- LEAVE empty.-
    Private mContextMenuDummy As New ContextMenu

    '== clsAttachments -

    Private mClsAttachments1 As clsAttachments

    '-- NEW File to be Attached (For NEW RA PIC)..

    Private mByteNewFile As Byte()
    Private msNewFileFullPath As String = ""
    Private msNewFileFileTitle As String = ""
    Private msNewFileFormat As String = ""

    '--  Invoices Goods listing.--
    Private mColQuoteRecords As Collection
    Private mlSortKey As Integer = -1 '--col index for sort..-
    Private mlSortOrder As Integer '-asc/desc-

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->
	
    WriteOnly Property connectionSql() As OleDbConnection '==  ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =

    '=3357.0206=
    '= Private msServer As String = ""
    '== Private msSqlDbName As String = ""
    WriteOnly Property server() As String
        Set(ByVal Value As String)
            msServer = Value
        End Set
    End Property '--abn.--
    '= = = = = = = =  = = =
    WriteOnly Property dbName() As String
        Set(ByVal Value As String)
            msSqlDbName = Value
        End Set
    End Property '--abn.--
    '= = = = = = = =  = = =

    '== Property Let connectionJet(cnn1 As ADODB.connection)
	
	'===   Set mCnnJet = cnn1
	
	'== End Property  '--cnn jet..--
	'= = = = = = =  = = = = =
	
	WriteOnly Property dbInfoSql() As Collection
		Set(ByVal Value As Collection)
			
			mColSqlDBInfo = Value
        End Set
	End Property '--info sql jobs..--
	'= = = = = = =  = = = = =
	
	'=== Property Let dbInfoJet(dbinfo As Collection)
	
	'===   Set mColJetDBInfo = dbinfo
	
	'=== End Property  '--info jet..--
	'= = = = = = = = = = = = = = = = = = = =
	
	WriteOnly Property retailHost() As _clsRetailHost
		Set(ByVal Value As _clsRetailHost)
			
			mRetailHost1 = Value
		End Set
	End Property '-host..-
	'= = = = = = = = = = = = = = = = =
	
	'-- if v2.0 only..--
	WriteOnly Property V20_Only() As Boolean
		Set(ByVal Value As Boolean)
			mbV20_Only = Value
		End Set
	End Property '--v20=
	'= = = = = = = = = = =
	
	'--  printers..--
	'--  printers..--
 
    '== 3101.1111- WriteOnly Property ColourPrinterName() As String
    '== 3101.1111-     Set(ByVal Value As String)
    '== 3101.1111-         msColourPrinterName = Value
    '== 3101.1111-     End Set
    '== 3101.1111- End Property '--prtColour
    '= = = = = = = = = = =  =  = =  =

    WriteOnly Property LocalSettingsPath() As String
        Set(ByVal value As String)
            msLocalSettingsPath = value
        End Set
    End Property  '=settings path..=
    '= = = =  = = = = = = = == = = =

    '--licensing..--

    '==Property Let LicenceOk(bOk As Boolean)
    '==mbLicenceOk = bOk
    '==End Property  '--licence..-
    '= = = = = = = = = = = =

    WriteOnly Property BusinessABN() As String
        Set(ByVal Value As String)
            msBusinessABN = Value
        End Set
    End Property '--abn.--
    '= = = = = = = =  = = =

    WriteOnly Property BusinessName() As String
        Set(ByVal Value As String)
            msBusinessName = Value
        End Set
    End Property '--abn.--
    '= = = = = = = =  = = =
    WriteOnly Property BusinessAddress1() As String
        Set(ByVal Value As String)
            msBusinessAddress1 = Value
        End Set
    End Property '--abn.--
    '= = = = = = = =  = = =
    WriteOnly Property BusinessAddress2() As String
        Set(ByVal Value As String)
            msBusinessAddress2 = Value
        End Set
    End Property '--abn.--
    '= = = = = = = =  = = =


    '-- Staff Id now comes from caller..--

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = =

    WriteOnly Property StaffId() As Integer
        Set(ByVal Value As Integer)

            mlStaffId = Value

        End Set
    End Property '--id.-
    '= = = = = = = = = = = =  =

    '--  RA No. comes if this is Updating existing RA..-

    WriteOnly Property RA_Id() As Integer
        Set(ByVal Value As Integer)

            mlRA_id = Value

        End Set
    End Property '--id.-
    '= = = = = = = = = = = =  =
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '--  clean up sql string data ..--
    Private Function msFixSqlStr(ByRef vInstr As Object) As String

        Dim sInstr As String
        sInstr = CStr(vInstr)
        msFixSqlStr = Replace(sInstr, "'", "''")

    End Function '--fixSql-
    '= = = = = = = = = = = =

    Private Function mbIsImageFile(ByVal sSuffix As String) As Boolean
        Dim listTypes As New List(Of String)   '=== {"BMP", "JPG", "GIF", "PNG", "ICO"}

        mbIsImageFile = mClsAttachments1.IsImageFile(sSuffix)
    End Function '-is image-
    '= = = = = = = = =  = = == 

    '-- Is RetailManager--

    Private Function mbIsRetailManager() As Boolean

        mbIsRetailManager = (LCase(msRetailHostname) = "retailmanager")
    End Function  '- mbIsRetailManager--
    '= = = = = = = = = = = = = = = = == 

    '-- IsIsJobmatixPOS--

    Private Function mbIsJobmatixPOS() As Boolean

        mbIsJobmatixPOS = (LCase(msRetailHostname) = "jobmatixpos")
    End Function  '- mbIsJobmatixPOS--
    '= = = = = = = = = = = = = = = = == 

    '-===FF->

    '--convert numeric data for sorted display..-

    Private Function msFormat(ByVal v1 As Object, ByVal vType As Object, ByVal lSize As Integer) As String
        Dim sResult As String
        Dim sType As String '--sql type--

        sResult = CStr(v1) '--for strings..-
        sType = UCase(gsGetSqlType(vType, lSize))
        If (sType = "MONEY") Or (sType = "SAMLLMONEY") Then '--currency..-
            sResult = New String(" ", 9)
            sResult = RSet(FormatCurrency(v1, 2), Len(sResult))
        ElseIf gbIsNumericType(sType) Then
            sResult = New String(" ", 5)
            sResult = RSet(VB6.Format(v1, "####0"), Len(sResult))

        ElseIf gbIsDate(sType) Then
            sResult = VB6.Format(CDate(v1), "yyyy-mm-dd")

        End If
        msFormat = sResult

    End Function '--convert--

    '--get day of week--
    Private Function msDayOfWeek(ByRef date1 As Date) As String
        Dim sDay As String

        Select Case DatePart(Microsoft.VisualBasic.DateInterval.Weekday, date1)
            Case 1 : sDay = "Sunday"
            Case 2 : sDay = "Monday"
            Case 3 : sDay = "Tuesday"
            Case 4 : sDay = "Wednesday"
            Case 5 : sDay = "Thursday"
            Case 6 : sDay = "Friday"
            Case 7 : sDay = "Saturday"
        End Select

        msDayOfWeek = sDay

    End Function '--day--
    '= = = = = = =  =  = =
    '-===FF->

    '-- Choose Result Colour..--
    '-- Choose Result Colour..--
    Private Function mlResultColour(ByVal sResult As String) As Integer
        Dim lngResult As Integer
        Dim sText As String

        lngResult = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
        sText = LCase(VB.Left(sResult, 8))
        Select Case sText
            Case "replaced" : lngResult = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Lime)
            Case "repaired" : lngResult = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow)
            Case "returned" : lngResult = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
            Case "credited" : lngResult = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Magenta)
            Case Else
        End Select
        mlResultColour = lngResult
    End Function '--result colour..-
    '= = = = = = = = = =

    '-- erase Customer etc..-

    Private Function mbResetNewFields() As Boolean

        txtItemDescription.Text = ""
        '==3311.323= txtSupplierCode.Text = ""
        txtInvoiceInfo.Text = ""
        txtSupplierInfo.Text = ""

        '==3311.325= FrameCust.Text = ""
        msCustomerName = ""
        msCustomerCompany = ""
        mlCustomerId = -1
        msCustomerBarcode = ""
        txtCustName.Text = ""
        txtCustCompany.Text = ""
        txtCustPhone.Text = ""
        txtCustMobile.Text = ""

        txtSymptoms.Text = ""
        txtJobNo.Text = ""

        '=3403.611=
        labItemSaleHdr.Text = ""
        txtItemSaleInvoiceNo.Text = ""
        txtItemSaleInvoiceDate.Text = ""

    End Function '--reset..-
    '= = = = = = =  =  = =
    '-===FF->

    '= -3301.222=
    '-- Check if Table exists.

    Private Function mbTableExists(ByRef cnnSql As OleDbConnection, _
                                    ByVal sTableName As String) As Boolean
        Dim sSql, sErrorMsg As String
        Dim rdr1 As OleDbDataReader

        mbTableExists = False
        '-- The following example checks for the existence of a specified table
        '='--     by verifying that the table has an object ID. 
        sSql = "SELECT * FROM sys.objects " & _
                   "WHERE object_id = OBJECT_ID(N'[dbo].[" & sTableName & "]') AND type in (N'U')"
        sErrorMsg = ""
        If gbGetReader(cnnSql, rdr1, sSql) Then  '--check if row exists..-
            If rdr1.HasRows Then '-table exists..-
                mbTableExists = True
            End If
            rdr1.Close()
        Else  '-get rdr error
            '--  GET error text !!--
            sErrorMsg = gsGetLastSqlErrorMessage()
            MsgBox("Error in reading sys.objects table.." & vbCrLf & sErrorMsg, MsgBoxStyle.Critical)
        End If  '--get rdr--

    End Function  '-mbTableExists-
    '= = = = = = =  =  = = = = = = =
    '-===FF->

    '-- L o a d  ANY JobTracking R e c o r d..-
    '-- L o a d  ANY JobTracking R e c o r d..-
    '-- L o a d  ANY JobTracking R e c o r d..-

    Private Function mbGetJobTrackingRecord(ByVal sSql As String, _
                                               ByRef ColJobFields As Collection) As Boolean

        Dim RsJob As DataTable '= ADODB.Recordset
        Dim colFld As Collection
        Dim sName As String

        mbGetJobTrackingRecord = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, RsJob, sSql) Then
            MsgBox("Failed to get RA recordset.." & vbCrLf & "Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        '--txtMessages.Text = ""
        ColJobFields = New Collection
        If (Not (RsJob Is Nothing)) AndAlso (RsJob.Rows.Count > 0) Then
            Dim dataRow1 As DataRow = RsJob.Rows(0)  '--first row-
            For Each column1 As DataColumn In RsJob.Columns '= fld1 In RsJob.Fields
                sName = column1.ColumnName
                colFld = New Collection
                colFld.Add(LCase(sName), "name")
                colFld.Add(dataRow1.Item(sName), "value")
                ColJobFields.Add(colFld, LCase(sName))
            Next column1 '= fld1
            mbGetJobTrackingRecord = True
        End If '--rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsJob = Nothing
    End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '-- L o a d  ANY JobTracking R e c o r d..-
    '--  IN TRANSACTION..

    Private Function mbGetJobTrackingRecord_trans(ByVal sSql As String, _
                                               ByRef ColJobFields As Collection, _
                                               ByRef trans1 As OleDbTransaction) As Boolean

        Dim RsJob As DataTable '= ADODB.Recordset
        Dim colFld As Collection
        Dim sName As String

        mbGetJobTrackingRecord_trans = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTableEx(mCnnJobs, RsJob, sSql, trans1) Then
            MsgBox("Failed to get RA recordset.." & vbCrLf & "Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        '--txtMessages.Text = ""
        ColJobFields = New Collection
        If (Not (RsJob Is Nothing)) AndAlso (RsJob.Rows.Count > 0) Then
            Dim dataRow1 As DataRow = RsJob.Rows(0)  '--first row-
            For Each column1 As DataColumn In RsJob.Columns '= fld1 In RsJob.Fields
                sName = column1.ColumnName
                colFld = New Collection
                colFld.Add(LCase(sName), "name")
                colFld.Add(dataRow1.Item(sName), "value")
                ColJobFields.Add(colFld, LCase(sName))
            Next column1 '= fld1
            mbGetJobTrackingRecord_trans = True
        End If '--rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsJob = Nothing
    End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->


    '-- Execute SQL Command..--
    '-- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Private Function mbExecuteSql(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As OleDbTransaction) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbExecuteSql = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            '= mCnnSql.ChangeDatabase(msSqlDbName)
            intAffected = sqlCmd1.ExecuteNonQuery()
            mbExecuteSql = True   '--ok--
            '== MsgBox("Sql exec ok. " & intAffected & " records affected..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            If bIsTransaction Then
                sqlTran1.Rollback()
                sRollback = vbCrLf & "RollBack was executed.." & vbCrLf
            End If
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbExecuteSql: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            '= msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    Private Function msAddSupplierItem(ByVal sAddressInfo As String, _
                                        ByRef colFields As Collection, _
                                           ByVal sColName As String) As String
        Dim s1 As String
        If Not IsDBNull(colFields.Item(sColName)("value")) Then
            s1 = colFields.Item(sColName)("value")
            If s1 <> "" Then
                msAddSupplierItem = VB.Left(sAddressInfo & s1 & vbCrLf, 490)  '==3203.213= max db colsize..(Plus quotes ?)
            Else
                msAddSupplierItem = sAddressInfo  '--no change-
            End If
        End If
    End Function '--add-
    '= = = = = = = = = = =

    '-- Set up Supplier Info text from ColRecord..--
    '-- Set up Supplier Info text from ColRecord..--

    Private Function mbSetUpSupplier(ByRef colInvoiceFields As Collection, _
                                     Optional ByVal bAddressUpdateOnly As Boolean = False) As Integer
        '= Dim s1 As String  '=v1 As Object

        msSupplierAddressInfo = ""
        If Not bAddressUpdateOnly Then  '--new supplier-  save ID etc..
            mlSupplierId = CInt(colInvoiceFields.Item("supplier_id")("value"))
            msSupplier = colInvoiceFields.Item("supplier")("value")
            msSupplierBarcode = colInvoiceFields.Item("supplierbarcode")("value")
        End If

        '-- build supplier composite address info..-
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colInvoiceFields, "Main_Addr1")
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colInvoiceFields, "Main_Addr2")
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colInvoiceFields, "Main_Addr3")

        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colInvoiceFields, "Main_suburb")
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colInvoiceFields, "Main_state")
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colInvoiceFields, "Main_postcode")
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colInvoiceFields, "Main_country")

        msSupplierMainPhone = colInvoiceFields.Item("Main_Phone")("value")
        msSupplierMainFax = colInvoiceFields.Item("Main_fax")("value")
        msSupplierMainEmail = colInvoiceFields.Item("Main_email")("value")

        txtSupplierInfo.Text = "RETURN TO: " & colInvoiceFields.Item("supplier")("value") & vbCrLf & _
                                "Phone: " & msSupplierMainPhone & vbCrLf & msSupplierAddressInfo & vbCrLf
        txtSupplierInfo.Text = txtSupplierInfo.Text & "Fax: " & msSupplierMainFax & vbCrLf
        txtSupplierInfo.Text = txtSupplierInfo.Text & "Email: " & msSupplierMainEmail & vbCrLf

    End Function '--SetUpSupplier-
    '= = = = = = =  =
    '-===FF->

    '--  Browse Suppliers to choose Alternate Supplier.-
    '--  Browse Suppliers to choose Alternate Supplier.-

    Private Function mbChooseSupplier(ByRef colSupplierFields As Collection) As Boolean
        Dim cx, j, i, k, fx As Integer
        Dim s1, s2 As String
        '== Dim sSql As String
        Dim sId As String
        '--Dim lngActualSize As Long
        '== Dim colKeys As Collection
        '== Dim colPrefs As Collection
        '== Dim colSelectedRow As Collection
        Dim colFld As Collection
        Dim sBarcode As String
        '== Dim colRecord As Collection  '--full cust record..-

        mbChooseSupplier = False
        '=== Load frmBrowse
        '=== '-- - --- MUST load/unload each time--
        '=== frmBrowse.connection = mCnnJet    '--Retail Manager Jet connenction..-
        '=== frmBrowse.colTables = mColJetDBInfo
        '=== frmBrowse.DBname = ""
        '=== frmBrowse.tableName = "supplier"
        '=== frmBrowse.IsSqlServer = False   '--bIsSqlServer
        '=== '--  pass preferred cols..-
        '=== Set colPrefs = New Collection
        '=== colPrefs.Add "supplier_id"
        '=== colPrefs.Add "barcode"
        '=== colPrefs.Add "supplier"
        '=== colPrefs.Add "main_addr1"
        '=== colPrefs.Add "main_addr2"
        '=== colPrefs.Add "main_addr3"
        '=== colPrefs.Add "main_suburb"
        '=== colPrefs.Add "main_state"
        '=== colPrefs.Add "main_postcode"
        '=== colPrefs.Add "main_country"
        '=== colPrefs.Add "main_phone"
        '==colPrefs.Add "mobile"
        '=== frmBrowse.PreferredColumns = colPrefs

        '=== '=== mbBrowsing = True '--bypass lost focus -
        '=== frmBrowse.Show vbModal
        '--End If  '--tbrowse-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

        '=== '--  get selected record key..--
        '=== Set colKeys = frmBrowse.selectedKey
        '=== Set colSelectedRow = frmBrowse.selectedRow  '--get grid row selected..-

        '=== Unload frmBrowse
        '== mbBrowsing = False '--enable lost focus -
        System.Windows.Forms.Application.DoEvents()

        If mRetailHost1.supplierLookup(colSupplierFields) Then
            '--retrieve selected record and fill in cust details..--
            If colSupplierFields Is Nothing Then
                '==Exit function
            Else '--selection made..-
                '-- get barcode..--
                If (colSupplierFields.Count() > 0) Then
                    sBarcode = colSupplierFields.Item("barcode")("value")
                    sId = CStr(colSupplierFields.Item("supplier_id")("value"))
                    '== sSql = " SELECT * FROM [supplier] WHERE (supplier_id=" & sId & "); "
                    '== If Not mbRMLookup(sSql, colSupplierFields) Then
                    '==      MsgBox "Failed to retrieve supplier record for Barcode: '" + sBarcode + "'..", vbExclamation
                    '== Else '--ok--
                    '--FUDGE: add "SupplierBarcode" to emulate "LookupSupplier"-- result..
                    colFld = New Collection
                    colFld.Add("SupplierBarcode", "name")
                    colFld.Add(sBarcode, "value")
                    colSupplierFields.Add(colFld, "SupplierBarcode")
                    '--caller sets up Supplier details.-
                    mbChooseSupplier = True
                    If gbDebug Then MsgBox("Found suppplier:  " & colSupplierFields.Item(1)("value"), MsgBoxStyle.Information)
                    '== End If
                Else
                    If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                End If '--got row..--
            End If '--nothing..-
        Else

        End If '--looku..-

        System.Windows.Forms.Application.DoEvents()
        '== Set colPrefs = Nothing

    End Function '--choose-
    '= = = = = = =  =
    '-===FF->

    '--  Load all supplier Invoices for GIVEN Stock_Id..--
    '--  Load all supplier Invoices for GIVEN Stock_Id..--
    '----- LOAD recordset AND listViewGOODS..--

    '== Private Function mbLoadInvoices(lngStockId As Long, _
    ''==                                  rsGoods As ADODB.Recordset) As Boolean

    Private Function mbLoadInvoices(ByRef lngStockId As Integer) As Boolean
        '== Dim sSql As String
        Dim colInvoiceList As Collection
        Dim s1 As String
        Dim date1 As Date
        Dim Date12MonthsAgo As Date
        '--Dim fldx As ADODB.Field
        Dim colRecord As Collection
        Dim colFldx As Collection
        Dim lngNoCols, ix, lCount As Integer
        Dim lngType, lngSize As Integer
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim v1 As Object

        mbLoadInvoices = False
        Date12MonthsAgo = DateAdd(Microsoft.VisualBasic.DateInterval.Month, -12, CDate(DateTime.Today))
        '--  lookup/Load all Goods Received lines for this stock_id..--
        '=== sSql = " SELECT   "
        '=== sSql = sSql + "   Goods.Invoice_No, Goods.Goods_Date,  "
        '=== sSql = sSql + "   Supplier.Supplier, SupplierCode.SupCode, Goods.Supplier_id,  "
        '=== sSql = sSql + "  GoodsLine.goods_id AS Goods_Id, GoodsLine.quantity AS Qty   "
        '=== '===== sSql = sSql + "     GoodsLine.stock_id as Stock_id "
        '=== sSql = sSql + " FROM   "
        '=== sSql = sSql + "  (GoodsLine LEFT JOIN  "
        '=== sSql = sSql + "   (Goods LEFT JOIN  "
        '=== sSql = sSql + "    (Supplier LEFT JOIN "
        '=== sSql = sSql + "       SupplierCode "
        '=== sSql = sSql + "       ON ((SupplierCode.Supplier_id= Supplier.Supplier_id) AND (SupplierCode.stock_id=" & mlStockId & ")) )"
        '=== sSql = sSql + "      ON (Supplier.Supplier_id=Goods.supplier_id)  )"
        '=== sSql = sSql + "    ON (Goods.Goods_id=GoodsLine.goods_id) )  "
        '=== sSql = sSql + " WHERE (GoodsLine.stock_id=" & mlStockId & ") "
        '=== '==If Chk12Mths.Value = 1 Then _
        ''=== '==             sSql = sSql + " AND (Goods.Invoice_Date > '" + sDate12MonthsAgo + "') "
        '=== sSql = sSql + "   ORDER BY Goods.Invoice_Date "
        '-- GET recordset of invoices for that stock item..==

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '== If Not gbGetRst(mCnnJet, rsGoods, sSql) Then
        If Not mRetailHost1.invoiceGetStockItemInvoices("", lngStockId, colInvoiceList) Then
            MsgBox("Failed to get Invoices for Stock item: '" & lngStockId & "'..  ", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End If
        '--------  ===== labStatus.Caption = "Loading ListView with: " & colInvoiceList.Count & " Invoices.."
        '---------  ===== Call mbLoadListView(colInvoiceList, ListView1)
        '==If Not (rsGoods Is Nothing) Then
        If Not (colInvoiceList Is Nothing) Then
            '-- LOAD listViewGoods with all invoices..--
            '-- create column headers...--
            FrameSupplier.Visible = False
            grpBoxOrigin.Visible = False   '==3311.323=
            FrameInvoiceList.Visible = True
            FrameInvoiceList.Text = "Supplier invoices on file:"
            ListViewGoods.Enabled = True
            ListViewGoods.Items.Clear()
            ListViewGoods.Columns.Clear()
            lngNoCols = 0
            '=== For Each fldx In rsGoods.Fields
            '===       lngNoCols = lngNoCols + 1
            '===       s1 = "" & CStr(fldx.Name)
            '===       ListViewGoods.ColumnHeaders.Add , , s1    '--, ListView1.Width \ 8
            '=== Next '--fldx  --
            If (colInvoiceList.Count() > 0) Then '--some thing..-
                colRecord = colInvoiceList.Item(1) '--  get first record..-
                '--  get all column names..--
                For Each colFldx In colRecord
                    lngNoCols = lngNoCols + 1
                    s1 = "" & CStr(colFldx.Item("Name"))
                    ListViewGoods.Columns.Add(s1) '--, ListView1.Width \ 8
                Next colFldx '--fldx  --
                lCount = 0
                '==3311.817=  SORTING-
                ListViewGoods.ListViewItemSorter = New ListViewItemComparer_RAs(0, SortOrder.Descending)
                '=== If (Not rsGoods.BOF) And (Not rsGoods.EOF) Then  '--something..-
                '===   rsGoods.MoveFirst
                '===   rsGoods.MoveLast
                '===   '==== MsgBox "Found " & rsGoods.RecordCount & " records..", vbInformation
                '===   rsGoods.MoveFirst
                For Each colRecord In colInvoiceList
                    '== While (Not rsGoods.EOF)    '--load all records..-
                    lCount = lCount + 1
                    date1 = CDate(colRecord.Item("Goods_Date")("Value"))
                    '-- filter on date if needed..--
                    If (Chk12Mths.CheckState = 0) Or ((Chk12Mths.CheckState = 1) And (date1 >= Date12MonthsAgo)) Then
                        '== s1 = msFormat(rsGoods(0).Value, rsGoods(0).Type, rsGoods(0).DefinedSize)
                        '== item1 = ListViewGoods.Items.Add()
                        '== item1.Text = s1
                        ix = 0
                        For Each colFldx In colRecord
                            v1 = colFldx.Item("value")
                            lngType = CInt(colFldx.Item("type"))
                            lngSize = CInt(colFldx.Item("definedSize"))
                            s1 = gsFormat(v1, lngType, lngSize)
                            If ix = 0 Then
                                '==item1.Text = s1
                                '--  NOW creates ITEM with 1st col..-
                                item1 = ListViewGoods.Items.Add(s1)
                            Else
                                item1.SubItems.Add(s1)
                            End If
                            ix = ix + 1
                        Next colFldx '== ix
                        '==   For ix = 1 To lngNoCols - 1 '--remainder of flds..-
                        '==      s1 = "null"  '--CStr(mRstQuote(ix).Value)
                        '==      If Not IsNull(rsGoods(ix).Value) Then
                        '==          s1 = msFormat(rsGoods(ix).Value, rsGoods(ix).Type, rsGoods(ix).DefinedSize)
                        '==      End If
                        '==      item1.ListSubItems.Add , , s1
                        '==   Next ix
                        item1.Tag = CStr(lCount) '--ID of this invoice line..-
                    End If '--date-
                    '==  rsGoods.MoveNext
                    '== Wend   '--eof..-
                    '== Call gbLoadListViewFromCollection(colInvoiceList, ListViewGoods)
                Next colRecord '--record..-
                LabSelectInvoice.Enabled = True
                cmdSelectInvoice.Enabled = True
            End If '--some records.-
        End If '--nothing..-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        mbLoadInvoices = True

    End Function '--mbLoadInvoices--
    '= = = = = = = = = = = =  =
    '-===FF->

    '-- Retrieve full Invoice and Supplier Info..-
    '-- Retrieve full Invoice and Supplier Info..-

    Private Function mbLookupSupplier(ByRef lngGoods_Id As Integer, ByRef colInvoiceFields As Collection) As Boolean

        '===        Dim sSql As String
        '===        Dim v1 As Variant

        mbLookupSupplier = False
        '-- Lookup Goods table and get invoice/supplier Info..-

        '===    sSql = " SELECT Goods.goods_id, Invoice_no, invoice_date,goods_date,  "
        '===    '=== sSql = sSql + " order_no, order_id, supplier.supplier_id, supplier, GoodsLine.stock_id, GoodsLine.quantity  "
        '===    sSql = sSql + " order_no, order_id, supplier.supplier_id as supplier_id, "
        '===    sSql = sSql + "supplier.barcode as SupplierBarcode, supplier, "
        '===    sSql = sSql + "main_contact, main_position, main_addr1, main_addr2, main_addr3, "
        '===    sSql = sSql + "main_suburb, main_state, main_postcode, main_country, "
        '===    sSql = sSql + "main_phone, main_fax, main_email  "
        '===    sSql = sSql + "  FROM Goods  "
        '===    sSql = sSql + "      LEFT JOIN Supplier "
        '===    sSql = sSql + "         ON  (supplier.supplier_id= Goods.supplier_id)   "
        '===    sSql = sSql + "  WHERE (Goods.goods_id=" & mlGoodsId & "); "
        '===    '====== NO! 11-May-2010 ==    msSupplierAddressInfo = ""
        '===    If mbRMLookup(sSql, colInvoiceFields) Then
        If mRetailHost1.invoiceGetInvoiceInfo("", lngGoods_Id, colInvoiceFields) Then
            mbLookupSupplier = True
        Else
            MsgBox("Can't find Goods Recvd Info..", MsgBoxStyle.Exclamation)
        End If '--lookup goods..-
    End Function '-- Supplier..-
    '= = = = = = = = = = = =  =
    '-===FF->

    '-- lookup RM Customers to check Customer given long ID..--
    '-- lookup RM Customers, return record as collection of fld collections.--

    '=== Private Function mbLookupCustomer(sBarcode As String, _
    ''===                                      colFields As Collection) As Boolean
    '===       Dim colFld As Collection  '--"name"=, "value"-
    '===       Dim fld1 As ADODB.Field
    '===       Dim s1 As String
    '===       Dim sSql As String
    '===       Dim sName As String
    '===       Dim rs1 As ADODB.Recordset

    '===   mbLookupCustomer = False

    '===   sSql = "Select * from [customer] WHERE barcode='" + sBarcode + "' "
    '===   Screen.MousePointer = vbHourglass
    '===   If Not gbGetRst(mCnnJet, rs1, sSql) Then
    '===           MsgBox "Failed to get Customer recordset..", vbExclamation
    '===           Screen.MousePointer = vbDefault
    '===           Exit Function
    '===   End If
    '===      '--txtMessages.Text = ""
    '===   If Not (rs1 Is Nothing) Then
    '===          If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst
    '===          If (Not rs1.EOF) Then   '---And (cx < 100)
    '===             '--return complete row..-
    '===             Set colFields = New Collection
    '===             For Each fld1 In rs1.Fields
    '===                Set colFld = New Collection
    '===                colFld.Add LCase(fld1.Name), "name"
    '===                colFld.Add fld1.Value, "value"
    '===                colFields.Add colFld, LCase(fld1.Name)
    '===             Next fld1
    '===             mbLookupCustomer = True
    '===          Else  '--not found-
    '===          End If  '-eof-
    '===   End If  '--rs-

    '===   Screen.MousePointer = vbDefault '--in case Browser failed--

    '=== End Function  '--get customer-
    '= = = = = = =  =  = =
    '-===FF->

    '--  set up customer details from RM customer record..--
    '--  set up customer details from RM customer record..--

    Private Function mbSetupCustomer(ByRef colFields As Collection) As Boolean
        Dim s1 As String
        Dim sName, sValue As String
        Dim s3, s4 As String


        msCustomerName = ""
        msCustomerCompany = "" : s3 = "" : s4 = ""
        mlCustomerId = -1
        msCustomerBarcode = ""
        txtCustName.Text = ""
        txtCustCompany.Text = ""
        txtCustPhone.Text = ""
        txtCustMobile.Text = ""

        If (colFields.Count() > 0) Then '--not empty..-
            mlCustomerId = CInt(colFields.Item("customer_id")("value"))
            msCustomerBarcode = colFields.Item("barcode")("value")
            msCustomerCompany = colFields.Item("company")("value")
            If (msCustomerCompany = "") Then '--no company name..--
                msCustomerCompany = "--" '--"n/a" '==  don't want blank company..--
            End If
            sName = msCustomerCompany
            s3 = colFields.Item("given_names")("value")
            s4 = colFields.Item("surname")("value")
            msCustomerPhone = colFields.Item("phone")("value")
            If Len(msCustomerPhone) > 20 Then
                msCustomerPhone = VB.Left(Replace(msCustomerPhone, " ", ""), 20)
            End If
            msCustomerMobile = colFields.Item("mobile")("value")
            If Len(msCustomerMobile) > 20 Then
                msCustomerMobile = VB.Left(Replace(msCustomerMobile, " ", ""), 20)
            End If
            '-- save customer info..--
            '---msCustomerName = Left(s3 + " " + s4, 50)   '--max 50--
            msCustomerName = s4 '--max 50--SURNAME, GivenNames --
            If msCustomerName <> "" Then msCustomerName = msCustomerName & ", "
            txtCustName.Text = VB.Left(UCase(msCustomerName) & s3, 50) '--max 50--SURNAME, GivenNames --
            msCustomerName = VB.Left(msCustomerName & s3, 50) '--max 50--Surname, GivenNames --
            txtCustCompany.Text = msCustomerCompany
            txtCustPhone.Text = msCustomerPhone
            txtCustMobile.Text = msCustomerMobile
            '==3311.325= FrameCust.Text = " Customer: " & msCustomerBarcode
        End If '--empty..-
        mbSetupCustomer = True

    End Function '--setup--
    '= = = = = = = = =  =
    '-===FF->

    Private Function mbExtractItemSalesInfo(ByRef colItemFields As Collection, _
                                            ByRef sCustBarcode As String, _
                                            ByRef sSaleHdr As String, _
                                            ByRef sInvoiceNo As String, _
                                            ByRef sSaleDate As String, _
                                            ByRef sInfo As String) As Boolean
        mbExtractItemSalesInfo = False
        sInfo = ""
        sSaleHdr = ""
        sInvoiceNo = ""
        sSaleDate = ""
        If colItemFields.Contains("item_sale_info") AndAlso _
                          (colItemFields.Item("item_sale_info") IsNot Nothing) Then
            Dim colSaleInfo As Collection = mColItemFields.Item("item_sale_info")
            Dim sName, sValue As String
            If (colSaleInfo.Count > 0) Then  '-have some-
                For Each col1 As Collection In colSaleInfo
                    sName = LCase(col1.Item("name"))
                    sValue = col1.Item("value")
                    sInfo &= sName & " :  "
                    If (sName = "sale_date") Then
                        sSaleDate = VB.Format(CDate(col1.Item("value")), "dd-MMM-yyyy")
                        sInfo &= VB.Format(CDate(col1.Item("value")), "dd-MMM-yyyy") & vbCrLf
                    Else
                        sInfo &= sValue & vbCrLf
                        If (sName = "customer_barcode") Then
                            sCustBarcode = sValue
                        ElseIf (sName = "customer_company") Then
                            txtCustCompany.Text = sValue
                        ElseIf (InStr(sName, "sale_invoice_no") > 0) Then
                            '=3403.611= - Show invoice No. and date.
                            '== labItemSaleHdr.Text = sName
                            sSaleHdr = sName
                            sInvoiceNo = sValue
                        End If
                    End If
                Next col1
                mbExtractItemSalesInfo = True
            End If  '-count-
        End If  '-contains-
    End Function  '-ExtractItemSalesInfo-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- NEW RA Printing now in clsPrintDocs..--
    '-- NEW RA Printing now in clsPrintDocs..--

    Private Function mlPrintRAForm() As Integer
        Dim prtDocs1 As clsPrintRAs
        Dim colBusiness As Collection
        Dim s1, s2 As String
        Dim lngError As Integer
        Dim sStaff As String
        Dim sCustText As String
        Dim sNotes As String

        prtDocs1 = New clsPrintRAs

        '== prtDocs1.PrtSelectedPrinter = mPrtColour
        prtDocs1.PrtSelectedPrinterName = msColourPrinterName

        prtDocs1.Version = LabVersion.Text
        prtDocs1.UserLogo = Picture2.Image

        '=3203.1231=  ADD picture if any..
        prtDocs1.ItemImage = picSubjectItem.Image

        prtDocs1.RA_No = mlRA_id
        prtDocs1.SupplierRMA = txtSupplierRMA.Text

        '==prtDocs1.LicenceOK = mbLicenceOK
        sStaff = msStaffName
        If mbUpdating Then '--view/update.-
            s2 = "Last Updated By: " '- ; -stay on this line..-
            If msActionUpdate = "" Then '--not changed..--
                s1 = LabPrevUpdate.Text
                sStaff = LabPrevStaff.Text
            Else '--changed now-
                s1 = VB6.Format(CDate(DateTime.Now), "dd-mmm-yyyy hh:mm")  '--now..-
            End If '--changed..-
        Else '--new..-
            s2 = "Created By: " '-- ;-- stay on this line..-
            s1 = LabDateCreated.Text
        End If

        prtDocs1.HeaderDate = s2 & sStaff & Space(4) & s1
        '== prtDocs1.PriorityColour = mlPriorityColour   '==vbMagenta

        '--load business info..-
        colBusiness = New Collection
        colBusiness.Add(msBusinessName, "BusinessName")
        colBusiness.Add("", "BusinessShortName")
        colBusiness.Add(msBusinessAddress1, "BusinessAddress1")
        colBusiness.Add(msBusinessAddress2, "BusinessAddress2")
        colBusiness.Add("", "BusinessState")
        colBusiness.Add("", "BusinessPostcode")
        prtDocs1.Business = colBusiness

        prtDocs1.RAStatus = msCurrentStatus

        '--  Serial NO and Item Details..--
        prtDocs1.SerialNo = msSerialNo
        prtDocs1.Description = txtItemDescription.Text
        prtDocs1.ItemSupplierCode = msItemSupplierCode
        prtDocs1.ItemBarcode = msItemBarcode
        prtDocs1.InvoiceInfo = txtInvoiceInfo.Text
        prtDocs1.ProblemReported = txtSymptoms.Text

        '--  customer Info..--
        prtDocs1.Source = msOrigin
        '==sCustText = IIf((mlJobId = -1), "", "  (JobNo: " & mlJobId & ")")
        '== sCustText = sCustText + vbCrLf + vbCrLf
        sCustText = " (" & msCustomerBarcode & ")" & vbCrLf & txtCustName.Text & vbCrLf & "Co. " & txtCustCompany.Text & vbCrLf
        sCustText &= "Tel: " & txtCustPhone.Text & " ... " & txtCustMobile.Text & vbCrLf
        '--  add job no if any..
        If (mlJobId >= 0) Then
            sCustText &= "(JobNo: " & mlJobId & ")"
        End If
        '=3403.613- 
        '--  Show Sales InvoiceNo if any.
        sCustText &= vbCrLf & "-- Item Sale Info --" & vbCrLf
        If (Trim(labItemSaleHdr.Text) <> "") Then
            sCustText &= labItemSaleHdr.Text & vbCrLf
            sCustText &= txtItemSaleInvoiceNo.Text & ".  " & txtItemSaleInvoiceDate.Text
        Else
            sCustText &= "- none -"
        End If

        '==Call gbPrintTextInBox(sText + sCustText, 240, 2000, 300, 5100, 9400, True)
        prtDocs1.RA_customer = sCustText
        prtDocs1.SupplierDetails = txtSupplierInfo.Text

        '--prepare notes.-
        sNotes = ""
        '--  History Notes are the same on both panels..-- (0 and 1 )..--
        '==  NB:  3311.326-  ONLY ONE Notes history and new-notes boxes now.
        If (Trim(txtRequestNotesHistory.Text) <> "") Or (msCurrentNotes <> "") Then
            If (Trim(txtRequestNotesHistory.Text) <> "") Then
                sNotes = Replace(Trim(txtRequestNotesHistory.Text), vbCrLf & vbCrLf, vbCrLf)
            End If '--history.-
            If (msCurrentNotes <> "") Then
                sNotes = sNotes & msCurrentNotes
            End If
        End If '--notes..-
        prtDocs1.RA_Progress = sNotes & msShowRAProgress
        '--  go--
        If prtDocs1.PrintRAForm Then
            MsgBox("OK.. RA Document has been sent to the printer:" & vbCrLf & _
                                vbCrLf & msColourPrinterName & "..", MsgBoxStyle.Information)
        End If
        colBusiness = Nothing
        prtDocs1 = Nothing
        Exit Function

PrintRA_Error:
        lngError = Err().Number
        MsgBox("!! ERROR in mbPrintNewJobForm." & vbCrLf & _
                        "Error code: " & lngError & " = " & ErrorToString(lngError), MsgBoxStyle.Exclamation)
        mlPrintRAForm = -lngError
    End Function '--mlPrintRAForm-
    '= = = = = = = = = = = = = =

    '-- NEW  Print Shipping Label --
    '--  Print Shipping Label --

    Private Function mbPrintShippingLabel() As Boolean
        Dim prtDocs1 As clsPrintRAs
        Dim colBusiness As Collection
        Dim s1, s2 As String
        Dim sStaff As String
        Dim lngError As Integer

        On Error GoTo PrintShipping_Error
        mbPrintShippingLabel = False
        prtDocs1 = New clsPrintRAs

        '== prtDocs1.PrtSelectedPrinter = mPrtColour
        prtDocs1.PrtSelectedPrinterName = msColourPrinterName

        prtDocs1.Version = LabVersion.Text
        prtDocs1.UserLogo = Picture2.Image
        prtDocs1.RA_No = mlRA_id
        prtDocs1.SupplierRMA = txtSupplierRMA.Text

        '==prtDocs1.LicenceOK = mbLicenceOK
        sStaff = msStaffName
        If mbUpdating Then '--view/update.-
            s2 = "Last Updated By: " '- ; -stay on this line..-
            If msActionUpdate = "" Then '--not changed..--
                s1 = LabPrevUpdate.Text
                sStaff = LabPrevStaff.Text
            Else '--changed now-
                s1 = VB6.Format(CDate(DateTime.Now), "dd-mmm-yyyy hh:mm")  '--now..-
            End If '--changed..-
        Else '--new..-
            s2 = "Created By: " '-- ;-- stay on this line..-
            s1 = LabDateCreated.Text
        End If

        prtDocs1.HeaderDate = s2 & sStaff & Space(4) & s1

        '== prtDocs1.PriorityColour = mlPriorityColour   '==vbMagenta

        '--load business info..-
        colBusiness = New Collection
        colBusiness.Add(msBusinessName, "BusinessName")
        colBusiness.Add("", "BusinessShortName")
        colBusiness.Add(msBusinessAddress1, "BusinessAddress1")
        colBusiness.Add(msBusinessAddress2, "BusinessAddress2")
        colBusiness.Add("", "BusinessState")
        colBusiness.Add("", "BusinessPostcode")
        prtDocs1.Business = colBusiness

        prtDocs1.RAStatus = msCurrentStatus

        prtDocs1.Supplier = msSupplier
        prtDocs1.SupplierAddressInfo = msSupplierAddressInfo
        prtDocs1.SupplierMainPhone = msSupplierMainPhone

        '--  go-
        If prtDocs1.PrintShippingLabel Then
            MsgBox("OK.. Shipping Label has been sent to the printer:" & vbCrLf & vbCrLf & _
                                                        msColourPrinterName & "..", MsgBoxStyle.Information)
        End If
        colBusiness = Nothing
        prtDocs1 = Nothing
        Exit Function

PrintShipping_Error:

        lngError = Err().Number
        MsgBox("!! ERROR in Print Shipping Label.." & vbCrLf & _
                                "Error code: " & lngError & " = " & ErrorToString(lngError), MsgBoxStyle.Exclamation)
    End Function '--mbPrintShippingLabel--
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- Re-Usable - A c t i v a t e --
    '-- Re-Usable -  A c t i v a t e --
    '--  Load/Reload Current RA Record.-
    '----  and set up RA-Progress Controls..-

    Private Sub ActivateReActivate()

        '= Dim s1 As String
        '= Dim s2 As String
        Dim sStatus2 As String
        Dim sSql As String
        Dim idx, ix, lngError As Integer
        Dim colOrigSupplier As Collection
        '= Dim sDay As String
        '= Dim sToday As String
        '= Dim sShortDate As String
        '= Dim sName As String
        Dim sPrompt1 As String
        Dim sPrompt2 As String

        '==LabStatus.Caption = ""
        txtRAStatusOrig.Text = ""
        txtRAStatusFriendly.Text = ""
        msShowRAProgress = ""
        txtSymptoms.ReadOnly = True
        txtJobNo.ReadOnly = True
        '-- set up SOURCE combo..-
        ListViewGoods.Enabled = False

        LabDateGoodsSent.Text = ""
        LabGoodsResult.Text = ""
        chkGoodsSent.Enabled = False
        chkGoodsSent.CheckState = System.Windows.Forms.CheckState.Unchecked '--UN-checked..-
        txtCourierBarcode.Text = ""
        '=3311.326= For ix = 0 To k_MAXGOODSRESULT
        '=3311.326= OptGoodsResult(ix).Enabled = False
        '=3311.326= OptGoodsResult(ix).Checked = False
        '=3311.326= Next ix
        cboGoodsResult.Enabled = False

        LabGoodsResult.Visible = False

        txtItemBarcode.Enabled = True
        txtItemSerial.Enabled = True '-can cut but not change..--
        '==3311.325= SSTab1.TabPages.Item(k_SSTAB_RMA).Enabled = True
        FrameRMARequest.Enabled = True
        labUserPrompt0.Visible = False
        labUserPrompt1.Visible = True

        ListViewGoods.BackColor = System.Drawing.ColorTranslator.FromOle(&H8000000F)
        '==  ????   ==  LabDateRcvd.Caption = sToday

        chkTfrToStock.Visible = False

        '==3203.213=  Clear so note doesn't print twice 
        '--  if RA form printed after update..
        msCurrentNotes = ""  '==3203.213=  Clear so doesn't print twice...

        '--set all step colors to off..
        labStep1.BackColor = Color.Transparent
        labStep2.BackColor = Color.Transparent
        labStep3.BackColor = Color.Transparent
        labStep4.BackColor = Color.Transparent

        labStep1.Enabled = False
        labStep2.Enabled = False
        labStep3.Enabled = False
        labStep4.Enabled = False

        '=== FrameItem.Enabled = False
        sSql = "SELECT * from [RAItems]  "
        sSql = sSql & " WHERE (RA_id=" & CStr(mlRA_id) & ")  " & vbCrLf
        If mbGetJobTrackingRecord(sSql, mColRAFields) Then
            '--load all RA data for show..--
            txtItemBarcode.ReadOnly = True
            txtItemSerial.ReadOnly = True
            LabOurRANumber.Text = VB6.Format(mlRA_id, "  00")
            LabOurRANumber.BackColor = System.Drawing.ColorTranslator.FromOle(RGB(224, 144, 0))
            msOriginalStatus = mColRAFields.Item("RA_Status")("value")
            msCurrentStatus = msOriginalStatus
            txtRAStatusOrig.Text = msCurrentStatus
            sStatus2 = ""
            Select Case VB.Left(msCurrentStatus, 2)
                Case "10" : sStatus2 = "* QUEUED"
                Case "20" : sStatus2 = "* REQUESTED"
                Case "30" : sStatus2 = "* GRANTED"
                Case "50" : sStatus2 = "* SHIPPED"
                Case "70" : sStatus2 = "* RECEIVED"
                Case "95" : sStatus2 = "* RMA REFUSED"
                Case "97" : sStatus2 = "* RMA CANCELLED"
            End Select
            txtRAStatusFriendly.Text = sStatus2
            txtCreatedName.Text = mColRAFields.Item("RM_StaffNameCreated")("value")
            LabDateCreated.Text = VB6.Format(CDate(mColRAFields.Item("RA_DateCreated")("value")), "dd-mmm-yyyy")
            If Not IsDBNull(mColRAFields.Item("RA_DateUpdated")("value")) Then
                LabPrevUpdate.Text = VB6.Format(CDate(mColRAFields.Item("RA_DateUpdated")("value")), "dd-mmm-yyyy, hh:mm")
                LabPrevStaff.Text = mColRAFields.Item("RM_StaffNameUpdated")("value") '-- !!! ---
            End If '--updated.-
            txtItemSerial.Text = mColRAFields.Item("RA_SerialNumber")("value")
            msSerialNo = txtItemSerial.Text
            '== 3357.0206== (mIntSerialAudit_id, "RM_SerialAudit_id")
            mIntSerialAudit_id = mColRAFields.Item("RM_SerialAudit_id")("value")
            msItemSupplierCode = mColRAFields.Item("RM_ItemSupplierCode")("value")
            msItemBarcode = mColRAFields.Item("RM_ItemBarcode")("value")
            txtItemBarcode.Text = msItemBarcode
            mlStockId = CInt(mColRAFields.Item("RM_stockId")("value"))
            mlGoodsId = CInt(mColRAFields.Item("RM_goodsId")("value"))
            msDescr = Trim(mColRAFields.Item("RM_ItemDescription")("value"))
            msCat1 = Trim(mColRAFields.Item("RM_ItemCat1")("value"))
            msCat2 = Trim(mColRAFields.Item("RM_ItemCat2")("value"))
            msCat3 = Trim(mColRAFields.Item("RM_ItemCat3")("value"))
            mCurSellEx = CDec(mColRAFields.Item("RM_Item_sell_ex")("value"))
            '== txtSupplierCode.Text = msItemSupplierCode
            txtItemDescription.Text = msDescr & vbCrLf & _
                                     "StockId: " & mlStockId & " (" & msCat1 & "- " & msCat2 & ") : " & _
                                        FormatCurrency(mCurSellEx, 2) & " (Ex GST.)" & vbCrLf & _
                                        "Suppl.Code: " & msItemSupplierCode
            txtInvoiceInfo.Text = "Invoice No: " & mColRAFields.Item("RM_InvoiceNo")("value") & _
                                  "  Date: " & VB6.Format(CDate(mColRAFields.Item("RM_InvoiceDate")("value"))) & vbCrLf & _
                                    "Goods Date: " & VB6.Format(CDate(mColRAFields.Item("RM_GoodsDate")("value"))) & vbCrLf & _
                                     "Order No: " + mColRAFields.Item("RM_orderNo")("value")
            '--SUPPLIER..--
            mlSupplierId = CInt(mColRAFields.Item("RM_SupplierId")("value"))
            msSupplier = mColRAFields.Item("RM_Supplier")("value")
            msSupplierBarcode = mColRAFields.Item("RM_SupplierBarcode")("value")
            msSupplierAddressInfo = mColRAFields.Item("RM_Supplier_AddressInfo")("value")
            msSupplierMainPhone = mColRAFields.Item("RM_Supplier_Main_Phone")("value")
            msSupplierMainFax = mColRAFields.Item("RM_Supplier_Main_Fax")("value")
            msSupplierMainEmail = mColRAFields.Item("RM_Supplier_Main_Email")("value")
            txtSupplierInfo.Text = "RETURN TO: (" & mColRAFields.Item("RM_SupplierBarcode")("value") & ") " & vbCrLf & _
                                                        msSupplier & vbCrLf & "Main Phone: " & msSupplierMainPhone & vbCrLf & _
                                                                      msSupplierAddressInfo & vbCrLf
            txtSupplierInfo.Text &= "Main Fax: " & msSupplierMainFax & vbCrLf
            txtSupplierInfo.Text &= "Main Email: " & msSupplierMainEmail & vbCrLf
            '--Lookup Orig.Supplier in case changed..--
            msOrigSupplier = ""
            If mlGoodsId >= 0 Then '--have goods_id.-
                If mbLookupSupplier(mlGoodsId, colOrigSupplier) Then
                    msOrigSupplier = colOrigSupplier.Item("supplier")("value")
                    txtInvoiceInfo.Text = txtInvoiceInfo.Text & vbCrLf & "ORIG.SUPPLIER: " & msOrigSupplier
                End If '--supplier..-
            End If '--goods.-
            '=3311.323= cboOrigin.Visible = False
            '=3311.323= LabOrigin.Top = cboOrigin.Top
            '=3311.323= LabOrigin.Height = VB6.TwipsToPixelsY(400)
            '=3311.323= LabOrigin.Width = VB6.TwipsToPixelsX(2000)
            msOrigin = mColRAFields.Item("RA_Origin")("value")
            '= LabOrigin.Text = "Source:" & vbCrLf & msOrigin
            If (InStr(LCase(msOrigin), "stock") <= 0) Then '--not stock yet..-
                chkTfrToStock.Visible = True '-can transfer..-
                chkTfrToStock.CheckState = System.Windows.Forms.CheckState.Unchecked '--not checked..-
                labOrigin.BackColor = System.Drawing.Color.Yellow '--not stock yet..-
                '-not stock-
                If (InStr(LCase(msOrigin), "counter") > 0) Then '--counter.
                    optOrigin_counter.Checked = True
                ElseIf (InStr(LCase(msOrigin), "job") > 0) Then '--counter.
                    optOrigin_job.Checked = True
                End If
            Else
                optOrigin_stock.Checked = True
                labOrigin.BackColor = System.Drawing.ColorTranslator.FromOle(&HC0FFFF) '--pastel.-
            End If

            mlJobId = CInt(mColRAFields.Item("RA_JobId")("value"))
            txtJobNo.Text = CStr(mlJobId)

            msCustomerBarcode = mColRAFields.Item("RA_CustomerBarcode")("value")
            txtCustNo.Text = msCustomerBarcode  '=3403.612=

            txtCustCompany.Text = mColRAFields.Item("RA_CustomerCompany")("value")
            txtCustName.Text = mColRAFields.Item("RA_CustomerName")("value")
            txtCustPhone.Text = mColRAFields.Item("RA_CustomerPhone")("value")
            txtCustMobile.Text = mColRAFields.Item("RA_CustomerMobile")("value")
            '==3311.325= FrameCust.Text = " Customer: " & msCustomerBarcode

            txtSymptoms.Text = mColRAFields.Item("RA_Symptoms")("value")
            '--check status by checking progress dated for NULL..-

            '=3403.612- Extract and show the Item Sales InvoiceNo. and SaleDate in Customer Box....
            '==  AND- Add Info to RA Printout.. AND recover this Sales Info when re-editing the RA.  
            '--  Lookup SerialNo in SerialAudit table..--
            '---- SerialAudit links SerailNo to SerialAuditTrail, which --
            '---    gives Stock_id, Goods_Id and and GoodsLine_Id..--
            If (msSerialNo <> "") And (msItemBarcode <> "") Then  '-have serialNo-
                Dim colSerialResults As Collection
                If mRetailHost1.serialGetSerialInfo(-1, msSerialNo, colSerialResults) AndAlso _
                                  (Not (colSerialResults Is Nothing)) AndAlso (colSerialResults.Count > 0) Then
                    If (colSerialResults.Count = 1) Then
                        mColItemFields = colSerialResults.Item(1)  '--only 1..-
                    Else '-Multiples. we must choose the one matching the item barcode-.
                        For Each colSerialInfo As Collection In colSerialResults
                            If (Trim(colSerialInfo.Item("barcode")("value")) = msItemBarcode) Then '-found-
                                mColItemFields = colSerialInfo
                                Exit For
                            End If
                        Next colSerialInfo
                    End If  '-multiples.-
                    '-- get sales info if any-
                    If (Not (mColItemFields Is Nothing)) AndAlso mColItemFields.Contains("item_sale_info") Then
                        Dim sItemSaleHdr, sBarcode As String
                        Dim sInvoiceNo, sSaleDate As String
                        Dim sInfo As String
                        If mbExtractItemSalesInfo(mColItemFields, sBarcode, _
                                                      sItemSaleHdr, sInvoiceNo, sSaleDate, sInfo) Then
                            labItemSaleHdr.Text = UCase(sItemSaleHdr)
                            txtItemSaleInvoiceNo.Text = sInvoiceNo  '= & ". " & sSaleDate
                            txtItemSaleInvoiceDate.Text = sSaleDate
                        End If  '-extract-
                    End If  '-contains info-
                End If  '-serialGetSerialInfo-
            End If '-have serialNo-

            '===LabSupplierRMA.Caption = mColRAFields("RA_SupplierRMA_No")("value")
            txtSupplierRMA.Text = mColRAFields.Item("RA_SupplierRMA_No")("value")
            '======LabStatus.Caption = msOriginalStatus
            sPrompt1 = " UPDATING RA RECORD:  "
            sPrompt2 = " UPDATING RA RECORD.." & vbCrLf
            txtRequestNotes.Text = ""
            '==txtRequestNotes(1).Text = ""
            If Not mbV20_Only Then '--DB is V21..--
                txtRequestNotesHistory.Text = mColRAFields.Item("RA_RMA_RequestNotes")("value")
                txtRequestNotesHistory.SelectionStart = Len(txtRequestNotesHistory.Text) '--show last requests..-
                txtRequestNotesHistory.SelectionLength = 0
                '--set up for GoodsProgress as well.--
                '== txtRequestNotesHistory(1).Text = mColRAFields.Item("RA_RMA_RequestNotes")("value")
                '== txtRequestNotesHistory(1).SelectionStart = Len(txtRequestNotesHistory(1).Text) '--show last requests..-
                '== txtRequestNotesHistory(1).SelectionLength = 0
            End If
            If IsDBNull(mColRAFields.Item("RA_DateRMA_Requested")("value")) Then
                chkRMARequested.Enabled = True
                '==3311.325= SSTab1.TabPages.Item(k_SSTAB_RMA).Enabled = True
                '==3311.325= SSTab1.SelectedIndex = k_SSTAB_RMA
                FrameRMARequest.Enabled = True
                cmdNewSupplier2.Enabled = True '--can still change..-
                labStep1.BackColor = mColorNextStep
                labStep1.Enabled = True
                sPrompt1 = sPrompt1 & vbCrLf & vbCrLf & "   RMA not yet requested from Supplier.." & vbCrLf
            Else '--not null- Was requested..
                txtRMAReceived.Enabled = True
                txtResultComments.Enabled = True '--still locked..--
                chkRMARequested.Enabled = False
                chkRMARequested.CheckState = System.Windows.Forms.CheckState.Checked '--checked because RMA requested..
                LabDateRMARequested.Text = VB6.Format(CDate(mColRAFields.Item("RA_DateRMA_Requested")("value")))
                msShowRAProgress = msShowRAProgress & "<ul>RMA Requested:" & vbCrLf & LabDateRMARequested.Text & vbCrLf
                If IsDBNull(mColRAFields.Item("RA_DateRMA_Response")("value")) Then '--NO Response Yet..-
                    '==3311.325= SSTab1.TabPages.Item(k_SSTAB_RMA).Enabled = True
                    '==3311.325= SSTab1.SelectedIndex = k_SSTAB_RMA
                    FrameRMARequest.Enabled = True
                    sPrompt1 = sPrompt1 & vbCrLf & "Waiting for Supplier RMA.." & vbCrLf & _
                                                                       "Indicate result in option buttons.."
                    OptRMAResult(0).Enabled = True
                    OptRMAResult(1).Enabled = True '--this is next one to click..-
                    labStep2.BackColor = mColorNextStep
                    labStep2.Enabled = True
                Else '--not null.. Response Was received..
                    '======  chkRMAReceived.Value = 1  '--done.. can't go back.-
                    OptRMAResult(0).Enabled = False
                    OptRMAResult(1).Enabled = False
                    '=3311.326= txtRequestNotes(0).ReadOnly = True '-- no more on this panel.-
                    txtRMAReceived.Enabled = False
                    If UCase(mColRAFields.Item("RA_RMA_Granted")("value")) = "Y" Then '-- granted..  can continue--
                        OptRMAResult(0).Checked = True
                        txtRMAReceived.Text = mColRAFields.Item("RA_SupplierRMA_No")("value")
                        txtRMAReceived.ReadOnly = True '--was enabled by setting optResult..
                        LabDateRMAResult.Text = VB6.Format(CDate(mColRAFields.Item("RA_DateRMA_Response")("value"))) '--result !!!!-
                        msShowRAProgress = msShowRAProgress & "<ul>RMA Granted:" & vbCrLf & LabDateRMAResult.Text & vbCrLf
                        '==FrameRMARequest.Enabled = False
                        txtCourierBarcode.Enabled = True
                        '==3311.325= SSTab1.TabPages.Item(k_SSTAB_GOODS).Enabled = True
                        '==3311.325= SSTab1.SelectedIndex = k_SSTAB_GOODS
                        FrameGoods.Enabled = True
                        labUserPrompt1.Visible = False
                        labUserPrompt2.Visible = True
                        If IsDBNull(mColRAFields.Item("RA_DateGoodsSentBack")("value")) Then '--Goods not yet Sent..
                            '=== LabStatus.Caption = "Supplier RMA Received..  Goods not yet Sent.."
                            sPrompt2 = sPrompt2 & "Supplier RMA Received.." & vbCrLf & _
                                              "When Goods are sent back, check the SENT box," & vbCrLf & _
                                                          " and Enter/Scan the Courier Barcode.."
                            chkGoodsSent.Enabled = True
                            labStep3.BackColor = mColorNextStep
                            labStep3.Enabled = True
                        Else '--goods sent--
                            chkGoodsSent.Enabled = False
                            chkGoodsSent.CheckState = System.Windows.Forms.CheckState.Checked '--checked..-
                            txtCourierBarcode.Text = mColRAFields.Item("RA_CourierBarcode")("value")
                            txtCourierBarcode.ReadOnly = True
                            LabDateGoodsSent.Text = VB6.Format(CDate(mColRAFields.Item("RA_DateGoodsSentBack")("value")))
                            msShowRAProgress = msShowRAProgress & "<ul>Goods Sent:" & vbCrLf & _
                                                   LabDateGoodsSent.Text & " (ID: " & txtCourierBarcode.Text & ")" & vbCrLf
                            If IsDBNull(mColRAFields.Item("RA_DateGoodsReceivedBack")("value")) Then
                                '==chkGoodsReceived.Enabled = True
                                labStep4.BackColor = mColorNextStep
                                labStep4.Enabled = True
                                sPrompt2 = sPrompt2 & "Goods have been sent back to Supplier.." & vbCrLf & _
                                                                 "Indicate the Supplier response when available.."
                                '=3311.326= For ix = 0 To k_MAXGOODSRESULT
                                '=3311.326= OptGoodsResult(ix).Enabled = True
                                '=3311.326= Next ix
                                cmdReOpen.Visible = True '--can re-open..--
                                cboGoodsResult.Enabled = True
                                '=== LabStatus.Caption = "Waiting goods from Supplier.."
                            Else '--must be completed..
                                '===chkGoodsReceived.Value = 1
                                '-- Disappear the Radio Buttons..--  !! -
                                '=3311.326= For ix = 0 To k_MAXGOODSRESULT
                                '=3311.326= OptGoodsResult(ix).Visible = False
                                '=3311.326= Next ix
                                cboGoodsResult.Enabled = False
                                LabGoodsResult.Visible = True
                                '==LabGoodsResult.Top = LabResultHdr.Top + LabResultHdr.Height
                                '==LabGoodsResult.Top = OptGoodsResult(0).Top
                                '==LabGoodsResult.Left = OptGoodsResult(0).Left
                                '=3311.326= LabResultHdr.Top = txtRequestNotes(0).Top
                                LabGoodsResult.Text = mColRAFields.Item("RA_ReturnResult")("value")
                                LabGoodsResult.ForeColor = System.Drawing.ColorTranslator.FromOle(mlResultColour(LabGoodsResult.Text))
                                LabDateGoodsResult.Text = VB6.Format(CDate(mColRAFields.Item("RA_DateGoodsReceivedBack")("value")))
                                txtResultComments.Text = mColRAFields.Item("RA_ReturnResultComment")("value")
                                '===LabDateGoodsResult.Top = LabGoodsResult.Top + LabGoodsResult.Height
                                msShowRAProgress = msShowRAProgress & "<ul>Goods Result:" & vbCrLf & _
                                                        LabDateGoodsResult.Text & vbCrLf & LabGoodsResult.Text & _
                                                                    ". Comment: " & txtResultComments.Text & vbCrLf
                                sPrompt2 = vbCrLf & "This RA is Completed.."
                                labUserPrompt2.Font = VB6.FontChangeBold(labUserPrompt2.Font, True)
                                cmdCancel.Text = "Exit"
                                '=====FrameGoods.Enabled = False
                                cmdReOpen.Visible = True '--can re-open..--
                                labUserPrompt0.Text = sPrompt2 '--FRONT panel also..--
                                '===LabStatus.Caption = "Goods/Response RCVD from Supplier.."
                            End If '--goods received.-
                        End If '--goods sent..-
                    Else '--REFUSED..-
                        sPrompt1 = sPrompt1 & vbCrLf & "Supplier RMA was REFUSED.." & vbCrLf
                        '= SSTab1.SelectedIndex = k_SSTAB_RMA
                        OptRMAResult(1).Checked = True '--refused.-
                        txtRMAReceived.Enabled = False '--was enabled by setting optResult..
                        msShowRAProgress = msShowRAProgress & "<ul>RMA Refused:" & vbCrLf & LabDateRMAResult.Text & vbCrLf
                        cmdCancel.Text = "Exit"
                    End If '--RMA granted..-
                End If '--RMA_Received--
            End If '--RAM requested..-
            cmdSave.Enabled = False '--  was set on by our clicking buttons..-
            msActionUpdate = "" '--  ditto.-
            labUserPrompt1.Text = sPrompt1
            labUserPrompt2.Text = sPrompt2
            cmdPrintRAForm.Enabled = True
            cmdPrintShippingLabel.Enabled = True
            btnPrintItemLabel.Enabled = True

            If (VB.Left(msOriginalStatus, 2) = "97") Then '--cancelled.--
                '==3311.325= FrameCust.Enabled = False
                FrameRMARequest.Enabled = False
                '====FrameGoods.Enabled = False
                cmdCancelRARecord.Enabled = False
            Else
                cmdCancelRARecord.Enabled = True
            End If '--cancelled..-
            txtItemBarcode.Enabled = True '--WAS disabled by serial-Change.. is still locked..-
        Else '--no record..-

        End If '--get..-
        colOrigSupplier = Nothing
        '==End If  '--new..-
    End Sub '--RE-Activate--
    '= = = = = = = = = = = =
    '-===FF->

    '--  Update RA Record..--
    '--  Update RA Record..--
    '==3357.0206- 06Feb2017=
    '--  If action is GoodsSent, and POS system if JobMatixPOs,
    '--    then call POS Goods Set update to do COMPLETE update as Transaction.
    '--      (ie. Update RA record and ADD SupplerReturn POS records.).

    Private Function mbUpdateRARecord(ByVal sUpdateSql As String, _
                                      Optional ByVal bGoodsWereSent As Boolean = False) As Boolean
        Dim sSqlRead2 As String
        Dim sStatus As String
        Dim colRAFields As Collection
        Dim lngaffected As Integer
        Dim sErrors As String
        Dim trans1 As OleDbTransaction

        mbUpdateRARecord = False
        '-- Check status in case we have been usurped.-
        sSqlRead2 = "SELECT * from [RAItems]  "
        sSqlRead2 = sSqlRead2 & " WHERE (RA_id=" & CStr(mlRA_id) & ")  " & vbCrLf
        '== mCnnJobs.BeginTrans()
        trans1 = mCnnJobs.BeginTransaction
        If mbGetJobTrackingRecord_trans(sSqlRead2, colRAFields, trans1) Then '--OK..-
            sStatus = colRAFields.Item("RA_Status")("value") '--get latest status..-
            If VB.Left(sStatus, 2) <> VB.Left(msOriginalStatus, 2) Then '--has changed..--
                '== mCnnJobs.RollbackTrans()
                trans1.Rollback()
                MsgBox("Attention needed.." & vbCrLf & " This RA record has been updated by another process.." & vbCrLf & vbCrLf & _
                         " Your starting status was: " & msOriginalStatus & ".." & vbCrLf & _
                         " Record status has since been changed to: " & sStatus & ".." & vbCrLf & _
                                                            "Your current updates are being abandoned..", MsgBoxStyle.Exclamation)
                '=Unload Me
                Exit Function '--Sub
            End If '--changed..
        Else '--re-read failed..-
            '== mCnnJobs.RollbackTrans()
            trans1.Rollback()
            MsgBox("Failed to RE-Read RA record to check status.." & vbCrLf, MsgBoxStyle.Critical)
            '===Unload Me
            Exit Function '==Sub
        End If '--re-read..-
        '-- Status still ok..  continue update transaction..-
        '--  BUT, if GoodsSent, and mbIsJobmatixPOS() then call POS
        '--    to do it all all..
        If mbIsJobmatixPOS() And bGoodsWereSent Then
            trans1.Rollback()  '--dump current transaction.
            '-- nb: Server name/DB name not available..
            Dim JMx31POS1 As JMxPOS330.clsJMxPOS31
            Dim colRA_items, colItem As Collection
            Dim sComments As String = ""

            '-- buils collection of data flds for this RA..
            colRA_items = New Collection
            colItem = New Collection
            colItem.Add(mlRA_id, "RA_id")
            colItem.Add(Trim(txtSupplierRMA.Text), "RA_SupplierRMA_No")
            colItem.Add(mColRAFields.Item("RM_InvoiceNo")("value"), "RM_InvoiceNo")
            colItem.Add(mlStockId, "RM_stockid")
            colItem.Add(msItemBarcode, "RM_ItemBarcode")
            colItem.Add(msDescr, "RM_ItemDescription")
            colItem.Add(mIntSerialAudit_id, "RM_SerialAudit_id")
            colItem.Add(msSerialNo, "RA_SerialNumber")
            colItem.Add(1, "quantity")
            colItem.Add(VB.Left(txtSymptoms.Text, 500), "RA_Symptoms")
            colItem.Add(VB.Right(txtRequestNotesHistory.Text & vbCrLf & msCurrentNotes, 2040), "RA_RMA_RequestNotes")

            '- one item only to go..-
            colRA_items.Add(colItem)

            JMx31POS1 = New JMxPOS330.clsJMxPOS31(mCnnJobs, msServer, msSqlDbName, mColSqlDBInfo, gsRuntimeLogPath)
            '-- do Update for all..
            If Not JMx31POS1.POS_GoodsReturned(mlStaffId, msStaffName, _
                                                mlSupplierId, sComments, colRA_items, sUpdateSql) Then
                MsgBox("Failed to update POS Returns and RA status..", MsgBoxStyle.Exclamation)
            Else  '-ok-
                mbUpdateRARecord = True
                MsgBox("POS Returns and RA Updates were completed ok.. ", MsgBoxStyle.Information)
            End If  'POS-
        Else  '-just RA update..
            If mbExecuteSql(mCnnJobs, sUpdateSql, True, trans1) Then
                trans1.Commit()
                cmdSave.Enabled = False
                cmdCancel.Text = "Exit"
                '===MsgBox "RA was Updated OK..", vbInformation
                If gbDebug Then MsgBox("RA was Updated ok.." & vbCrLf & " SQL was:" & vbCrLf & sUpdateSql, MsgBoxStyle.Information)
                '==FrameRMARequest.Enabled = False  '--no more changes this session..-
                '==FrameGoods.Enabled = False
                mbUpdateRARecord = True
            Else '-insert failed--
                '= mCnnJobs.RollbackTrans()
                '= WAS done=  trans1.Rollback()
                MsgBox("Failed to UPDATE RA record.." & vbCrLf & sErrors, MsgBoxStyle.Critical)
            End If '--execute..-
        End If '-POS-

    End Function '--update.--
    '= = = = = = = = = = = =
    '-===FF->

    '-- L o a d --
    '-- L o a d Initialising Stuff needs to go to mybase.New()  ==--
    '--  L o a d --
    '-- L o a d Initialising Stuff needs to go to mybase.New()  ==--
    '-- L o a d Initialising Stuff needs to go to mybase.New()  ==--

    '--  THIS sub denatured of PROPERTIES setting statements..


    Private Sub mbOriginal_frmNewRA_Load()
        Dim ix As Integer
        Dim lngError As Short
        Dim sName As String

        '= mbActive = False
        '== mbUpdating = False

        '== PROPERTY- can't change now..    mlRA_id = -1 '-- new.- no number yet..-
        '== PROPERTY- can't change now..   mlJobId = -1 '--  no JOB..-

        '=3311.325= FrameCust.Enabled = False
        grpBoxNotes.Text = ""

        txtItemSerial.Text = ""
        txtItemBarcode.Text = ""
        msSerialNo = ""
        msItemBarcode = ""
        '--reset flds..-
        Call mbResetNewFields()

        FrameSupplier.Text = ""
        FrameInvoiceList.Text = ""
        FrameInvoiceList.Visible = False

        chkRMARequested.Enabled = False
        chkRMARequested.CheckState = System.Windows.Forms.CheckState.Unchecked '--unchecked..-
        '--chkRMAReceived.Enabled = False
        '--chkRMAReceived.Value = 0    '--unchecked..-
        OptRMAResult(0).Enabled = False
        OptRMAResult(1).Enabled = False

        OptRMAResult(0).Checked = False
        OptRMAResult(1).Checked = False

        chkGoodsSent.Enabled = False
        chkGoodsSent.CheckState = System.Windows.Forms.CheckState.Unchecked '--unchecked..-
        '======chkGoodsReceived.Enabled = False
        '=====chkGoodsReceived.Value = 0    '--unchecked..-
        '=3311.326= For ix = 0 To k_MAXGOODSRESULT
        '=3311.326= OptGoodsResult(ix).Enabled = False
        '=3311.326= OptGoodsResult(ix).Checked = False
        '=3311.326= Next ix

        '==3311.325-
        cboGoodsResult.Items.Clear()
        cboGoodsResult.Items.Add("Replaced")
        cboGoodsResult.Items.Add("Repaired")
        cboGoodsResult.Items.Add("Returned")
        cboGoodsResult.Items.Add("Credited")
        cboGoodsResult.Items.Add("Other")
        cboGoodsResult.SelectedIndex = -1
        cboGoodsResult.Enabled = False
        '-- done cbo rsult.-

        txtRMAReceived.ReadOnly = True
        txtCourierBarcode.ReadOnly = True
        txtResultComments.ReadOnly = True

        txtRMAReceived.Enabled = False
        txtRMAReceived.Text = ""
        txtCourierBarcode.Enabled = False
        txtRequestNotesHistory.Text = ""
        txtRequestNotes.Text = ""
        '-- and on Goods Panel.-
        '=3311.326= txtRequestNotesHistory(1).Text = ""
        '=3311.326= txtRequestNotes(1).Text = ""

        txtResultComments.Text = ""  '==3311.507=
        txtResultComments.Enabled = False

        cmdSave.Enabled = False
        cmdPrintRAForm.Enabled = False
        cmdPrintShippingLabel.Enabled = False
        btnPrintItemLabel.Enabled = False

        cmdNewSupplier.Enabled = False

        '== PROPERTY- can't chnge now..   mbCreated = False
        '== PROPERTY- can't chnge now..   mbPrinted = False

        msOriginalStatus = ""
        msInterimStatus = ""
        msCurrentStatus = ""
        msOrigin = ""
        msActionUpdate = ""
        msCurrentNotes = ""

        '===LabInvoiceInfo.Caption = ""
        LabDateGoodsSent.Text = ""
        LabGoodsResult.Text = ""

        '==LabSupplierRMA.Caption = ""
        txtSupplierRMA.Text = ""
        txtRAStatusFriendly.Text = ""
        txtRAStatusOrig.Text = ""

        LabDateRMARequested.Text = ""
        LabDateRMAResult.Text = ""
        LabDateGoodsResult.Text = ""
        mlSupplierId = -1
        msSupplierAddressInfo = ""
        msShowRAProgress = ""
        msInterimRAProgress = ""

        LabPrevStaff.Text = ""
        LabPrevUpdate.Text = ""
        labUserPrompt0.Text = ""

        LabJobNo.Enabled = False
        txtJobNo.Enabled = False
        '==SSTab1.TabEnabled(k_SSTAB_CUST) = False
        '=3311.325= SSTab1.TabPages.Item(k_SSTAB_RMA).Enabled = False
        '=3311.325= SSTab1.TabPages.Item(k_SSTAB_GOODS).Enabled = False
        '=3311.325= 
        FrameRMARequest.Enabled = False
        FrameGoods.Enabled = False

        ListViewGoods.Enabled = False
        Chk12Mths.Enabled = False
        LabSelectInvoice.Enabled = False
        cmdSelectInvoice.Enabled = False

        cmdCancelRARecord.Enabled = False
        cmdReOpen.Visible = False
        cmdNewSupplier2.Enabled = False

        '=3311.323= cboOrigin.Items.Clear()
        '=3301.222=  -No Jobs if POS only..
        If mbTableExists(mCnnJobs, "Jobs") Then
            '=3311.323= cboOrigin.Items.Add("Job")
            optOrigin_job.Enabled = True
            mbHasJobTracking = True
        Else
            optOrigin_job.Enabled = False
        End If  '-has jobs.-
        '==If Not mbV20_Only Then '--can have jobless RAs.-
        '=3311.323= cboOrigin.Items.Add("Counter")
        '=3311.323= cboOrigin.Items.Add("Stock")
        '== End If
        '=3311.323= cboOrigin.Enabled = False
        panelOrigin.Enabled = False  '= grpBoxOrigin.Enabled = False
        optOrigin_job.Checked = False
        optOrigin_counter.Checked = False
        optOrigin_stock.Checked = False

        '--Pic2 is for printing..--
        '==3311.325= Picture2.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(SSTab1.Top) + 600) '--hide pic2--
        '==3311.325= Picture2.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(SSTab1.Left) + 600)
        Picture2.Visible = False

        '-- load biz. logo..--
        '--  can accept GIF, JPG or BMP..--
        sName = Dir("userlogo*.gif")
        If sName = "" Then sName = Dir("userlogo*.jpg")
        If sName = "" Then sName = Dir("userlogo*.bmp")
        If sName = "" Then sName = Dir("jobMatix_small.jpg")
        If sName = "" Then '--no logo-
            '--Picture1.Visible = False  '--Kepp jobMatix..-
            Picture2.Image = Picture1.Image '--jobmatix logo..--
        Else '--ok-
            On Error Resume Next
            Picture2.Image = System.Drawing.Image.FromFile(sName)
            lngError = Err().Number
            If lngError <> 0 Then
                MsgBox("Failed to load user business logo: '" & sName & "'.." & vbCrLf & _
                        "Error: " & lngError & "  (" & ErrorToString(lngError) & ")", MsgBoxStyle.Exclamation)
            End If
        End If '--dir ok..-

        '== PROPERTY- can't change now..   chkTfrToStock.Visible = False '--assume new RA..-

        '== PROPERTY- can't change now..   msColourPrinterName = ""

    End Sub '-Original-load--
    '= = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Actual Activate --
    '-- Actual Activate --
    '-- L o a d Initialising Stuff needs to go to mybase.New()  ==--

    '--  Original Activate is now the LOAD EVENT.--

    Private Sub frmNewRA_Load(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim sDay, sToday As String
        '= Dim sShortDate As String
        '== Dim sName As String
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim s1, sName As String

        If mbActive Then Exit Sub '--re-entered after every child form..--
        mbActive = True

        mColorNextStep = labStartHere.BackColor   '--SAVE BG colour for pointing the way..
        '--set all step colors to off..
        labStep1.BackColor = Color.Transparent
        labStep2.BackColor = Color.Transparent
        labStep3.BackColor = Color.Transparent
        labStep4.BackColor = Color.Transparent

        labStep1.Enabled = False
        labStep2.Enabled = False
        labStep3.Enabled = False
        labStep4.Enabled = False

        '-- DO STUFF FROM Original LOAD..---
        '----  WITHOOUT changing properties vars..--
        Call mbOriginal_frmNewRA_Load()

        '=3403.611-
        labItemSaleHdr.Text = ""
        txtItemSaleInvoiceNo.Text = ""
        txtItemSaleInvoiceDate.Text = ""

        LabHdr2.Text = msBusinessName '== + vbCrLf + msBusinessAddress1 + vbCrLf + msBusinessAddress2

        sDay = VB.Left(msDayOfWeek(CDate(DateTime.Today)), 3)
        s1 = sDay & ", " & VB6.Format(CDate(DateTime.Now), "dd-mmm-yyyy hh:mm")
        '==LabDateCreated.Caption = s1
        Call CenterForm(Me)
        With My.Application.Info
            LabVersion.Text = "JobMatix v" & .Version.Major & "." & _
               .Version.Minor & "." & .Version.Build & "." & .Version.Revision
        End With
        '== LabVersion.Text = "JMxRAs330 V" & My.Application.Info.Version.Major & "." & _
        '=   My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & _
        '=     My.Application.Info.Version.Revision
        Me.Text = LabVersion.Text & " - Tracking Supplier Returns."
        sDay = VB.Left(msDayOfWeek(CDate(DateTime.Today)), 3)
        sToday = sDay & ", " & VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy") '--- + " - " + Format$(Time, "hh:mm")
        LabToday.Text = sToday
        txtUpdatedName.Text = msStaffName
        Picture1.Visible = False

        '=3311.305= Call mbLoadSettings()
        '=3311.305= Load up Settings.  File Path was parameter from Main caller.
        mLocalSettings1 = New clsLocalSettings(msLocalSettingsPath)

        '-Load up systemInfo object.
        '==3311= SysInfo. use class instance.
        mSysInfo1 = New clsSystemInfo(mCnnJobs)

        '=3101.1111= get printers.
        cboPrinters.Items.Clear()
        cboItemLabelPrinters.Items.Clear()
        '==3202.212= AND  msItemLabelPrinterName=
        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboPrinters.Items.Add(sName)
                '= cboReceiptPrinters.Items.Add(sName)
                cboItemLabelPrinters.Items.Add(sName)
            Next sName
            '-- check local settings (prefs) for printers..
            If mLocalSettings1.queryLocalSetting(k_RA_PrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then
                        cboPrinters.SelectedItem = msDefaultPrinterName
                    End If
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then
                    cboPrinters.SelectedItem = msDefaultPrinterName
                End If
            End If  '-query- 
            msColourPrinterName = cboPrinters.SelectedItem
            '-- C. check local settings (prefs) for LABEL printer..
            If mLocalSettings1.queryLocalSetting(k_RA_PrtLabelSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--pref. defined, so set it- 
                    cboItemLabelPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then
                        cboItemLabelPrinters.SelectedItem = msDefaultPrinterName
                    End If
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then
                    cboItemLabelPrinters.SelectedItem = msDefaultPrinterName
                End If
            End If  '-query- 
            msItemLabelPrinterName = cboItemLabelPrinters.SelectedItem
        End If '-getAvail.-  
        '==3311.325=
        labUserPrompt0.Width = FrameRMARequest.Width - 5

        labUserPrompt1.Visible = False
        labUserPrompt1.Left = labUserPrompt0.Left
        labUserPrompt1.Width = labUserPrompt0.Width
        labUserPrompt2.Visible = False
        labUserPrompt2.Left = labUserPrompt0.Left
        labUserPrompt2.Width = labUserPrompt0.Width

        If (mlRA_id = -1) Then '-- NEW.- no number yet..-
            '== 3067.0 ==
            s1 = gsGetHelpFileName()
            If (s1 <> "") Then
                HelpProvider1.HelpNamespace = s1
                HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
                HelpProvider1.SetHelpKeyword(Me, "JT3-NewRA3.htm")
            End If
            btnUpdateSupplierAddress.Enabled = False  '==3311.420=

            txtCreatedName.Text = msStaffName
            LabDateCreated.Text = VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy")
            '=3311.325= SSTab1.TabPages.Item(k_SSTAB_CUST).Enabled = True
            '=3311.325= SSTab1.SelectedIndex = k_SSTAB_CUST
            '= s1 = s1 & "RA records capture and track RMA requests to the relevant supplier "
            '= s1 = s1 & "for the return and replacement of faulty items.. " & vbCrLf & vbCrLf
            s1 = "  CREATING NEW RA RECORD:   "
            s1 &= "  -- To start, enter/scan the "
            s1 &= "Serial No. of the item; verify Supplier Invoice/Date details." & vbCrLf
            s1 &= "If no SerialNo. available, scan the Product Barcode,"
            s1 &= " and choose a Supplier Invoice from the list for that product."
            s1 &= " Then enter the defect, and Job No. if any." & vbCrLf
            s1 &= " Complete Customer details where available.."
            labUserPrompt0.Text = s1 & vbCrLf & "To save, Press 'Create', below right."
            cmdSave.Text = "Create"
            ToolTip1.IsBalloon = True
            ToolTip1.SetToolTip(labUserPrompt0, labUserPrompt0.Text)

            txtItemBarcode.Enabled = True
            txtItemBarcode.ReadOnly = False '--can choose barcode first..-
            '--must enter serial no..-
            txtItemSerial.Select()

        Else '--updating..  retrieve requested RA record..--
            '== 3067.0 ==
            s1 = gsGetHelpFileName()
            If (s1 <> "") Then
                HelpProvider1.HelpNamespace = s1
                HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
                HelpProvider1.SetHelpKeyword(Me, "JT3-RA3Update.htm")
            End If

            mbUpdating = True
            LabHdr1.Text = "Updating RA Record"
            Call ActivateReActivate() '--  load RA record 1st time..--
        End If '--new..--
        grpBoxItemPic.Text = "RA Item Image"
        '-- Make new inst. of clsAttachments.
        '-- we have controls on this form, so we can use full NEW method..
        Try
            mClsAttachments1 = New clsAttachments(Me, mCnnJobs, mColSqlDBInfo, "RA", _
                                         mlRA_id, msSupplier, mlStaffId, msStaffName, openDlg1)
        Catch ex As Exception
            MsgBox("ERROR creating new clsAttachments." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Me.Close()
        End Try
        If (mlRA_id > 0) Then  '-not new-
            Me.KeyPreview = True  '-To catch Ctl-V (Pasting File)..
            grpBoxItemPic.Enabled = False

            '--=3119.1222=
            '=  Context menu for pasting file Name--
            '--  Popup menu for Right click on txt File name..-
            mContextMenuPasteFileName = New ContextMenu
            mnuPasteFileName.Name = "mnuPasteFileName"
            '= mContextMenuPasteFileName.MenuItems.Add(mnuPasteFileSep1)
            mContextMenuPasteFileName.MenuItems.Add(mnuPasteFileName)
            '= mContextMenuPasteFileName.MenuItems.Add(mnuPasteFileSep2)
            '--  done that menu.--
            '-- disable default menu.
            txtNewFileName.ContextMenu = mContextMenuDummy
            Dim intDoc_id As Integer
            Dim sFileTitle As String
            Dim byteImageBytes() As Byte
            Dim ms As System.IO.MemoryStream

            '-- Have an RA.. show image if any. on 1st Panel.
            If Not mClsAttachments1.GetFirstImage(mlRA_id, intDoc_id, sFileTitle, byteImageBytes) Then
                '= NONE=  MsgBox("Failed to retieve image..", MsgBoxStyle.Information)
            Else '-ok-
                ms = New System.IO.MemoryStream(byteImageBytes)
                Dim image1 As Image = System.Drawing.Image.FromStream(ms)
                picSubjectItem.Visible = True
                picSubjectItem.Image = image1
                picSubjectMain.Image = image1  '=3203.101=
                ms.Close()
                '=picSubjectItem.Image = byteImageBytes
            End If  '--get-
            labStartHere.BackColor = Color.Transparent   '--don't need to be green now..
        Else '- new RA-  So no Attachments YET-
            '-- But must have Class to be able to INSERT Picure with New RA.
            '-- NO User Attachment Controls for NEW RA..
            '= Try
            '==mClsAttachments1 = New clsAttachments(Me, mCnnJobs, mColSqlDBInfo, "RA", _
            '=                            mlRA_id, msSupplier, mlStaffId, msStaffName, openDlg1, False)
            '==Catch ex As Exception
            '==MsgBox("ERROR creating new clsAttachments." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            '==Me.Close()
            '== End Try

            '=3403.719=
            '-  Disable txtRequestNotes until Suppl. Invoice No. selected.
            txtRequestNotes.Enabled = False

            grpBoxAddNew.Enabled = False
            grpBoxItem.Enabled = False
            txtNewFileName.Text = ""
            txtNewComment.Text = ""
            txtComments.Text = ""
            SSTabMain.TabPages("mainTabPage_attachments").Enabled = False
            labStartHere.BackColor = mColorNextStep   '--lawn green..
            labStartHere.Enabled = True
        End If  '--new-

        '=3203'212=
        '-- get current system info..-
        '=3311.305= If gbLoadsystemInfo(mCnnJobs, mColSystemInfo, mSdSystemInfo) Then
        If mSysInfo1.exists("ITEMBARCODEFONTNAME") Then
            msItemLabelBarcodeFontName = mSysInfo1.item("ITEMBARCODEFONTNAME")
        End If
        If mSysInfo1.exists("ITEMBARCODEFONTSIZE") Then
            s1 = mSysInfo1.item("ITEMBARCODEFONTSIZE")
            If IsNumeric(s1) Then
                mIntItemLabelBarcodeFontSize = CInt(s1)
            End If
        End If
        '= 3357.0206=
        If mSysInfo1.exists("RETAILHOSTNAME") Then
            msRetailHostname = mSysInfo1.item("RETAILHOSTNAME")
        End If

        labStartHere.Enabled = False
        '=3311.305= End If  '-load system info.
        mbIsInitialising = False   '=3203.1227=

    End Sub '--load--
    '= = = = = = = = = = = =
    '-===FF->

    '-- Got focus on SerialNo..-

    Private Sub txtItemSerial_Enter(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles txtItemSerial.Enter
        Dim L1 As Integer

        If mbUpdating Or mbCreated Then Exit Sub
        '-- If create done, don't clear..
        If txtItemSerial.ReadOnly Then Exit Sub
        '==txtItemBarcode.Locked = True
        txtItemBarcode.Text = ""

        mlStockId = -1  '==3101.1029=

        Chk12Mths.Enabled = False
        LabSelectInvoice.Enabled = False
        cmdSelectInvoice.Enabled = False
        cmdNewSupplier.Enabled = False

        ListViewGoods.Items.Clear()
        ListViewGoods.Enabled = False

        txtItemDescription.Text = ""
        txtInvoiceInfo.Text = ""
        '= txtSupplierCode.Text = ""
        '===LabInvoiceInfo.Caption = ""
        txtSupplierInfo.Text = ""

        L1 = Len(txtItemSerial.Text)
        If L1 > 0 Then
            txtItemSerial.SelectionStart = 0
            txtItemSerial.SelectionLength = L1
        End If

    End Sub '--gotFocus..-
    '= = = = = = = = = = = =
    '-===FF->

    '-- Enable barcode if serial fld cleared..-

    'UPGRADE_WARNING: Event txtItemSerial.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtItemSerial_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles txtItemSerial.TextChanged

        If Trim(txtItemSerial.Text) = "" Then '--allow barcode e3ntry..-
            txtItemBarcode.Enabled = True
            txtItemBarcode.ReadOnly = False
        Else '--have serial..-
            txtItemBarcode.Enabled = False
            txtItemBarcode.ReadOnly = True
        End If

    End Sub '--change..-
    '= = = = = = = = = = = =
    '-===FF->

    '--  lookup SERIAL no.. --
    '--  lookup SERIAL no.. --

    '-- trap ENTER key on SERIAL no..--
    '--  lookup serial and get stock-id, barcode, etc--

    Private Sub txtItemSerial_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                           Handles txtItemSerial.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1, s2, sList, sAns As String
        Dim sName, sBarcode As String
        Dim sSql As String
        Dim v1 As Object
        Dim colSerialInfo As Collection
        Dim colSerialResults, colRAFields As Collection
        Dim ix, lngRA_id As Integer
        Dim sStatus As String

        If mbUpdating Or mbCreated Then GoTo EventExitSub
        If keyAscii = 13 Then '--enter-
            txtSymptoms.ReadOnly = True
            s1 = Trim(txtItemSerial.Text)
            If (s1 = "") Then '--empty serial no,,-
                txtItemBarcode.Enabled = True
                txtItemBarcode.ReadOnly = False
                txtItemBarcode.Focus()
            Else '--some serial..-
                '=3101= -- TEMP- Get Product Barcode.--
                '--  Now must have Stock_id to look up serial.
                '=3101.1029== NO! Lookup Serial ONLY..
                '==  Prod barcode may not be available..
                '=3101.1029==If Trim(txtItemBarcode.Text) = "" Then
                '=3101.1029==sBarcode = Trim(InputBox("Please scan or Enter product Barcode.."))
                '=3101.1029==If sBarcode = "" Then
                '=3101.1029==keyAscii = 0
                '=3101.1029==GoTo EventExitSub
                '=3101.1029==End If
                '-- get stock id..
                '=3101.1029==msItemBarcode = sBarcode
                '=3101.1029==If mRetailHost1.stockGetStockRecord(msItemBarcode, -1, mColItemFields) Then
                '=3101.1029==mlStockId = CInt(mColItemFields.Item("stock_id")("value"))
                '=3101.1029==Else
                '=3101.1029==keyAscii = 0
                '=3101.1029==GoTo EventExitSub
                '=3101.1029==End If
                '=3101.1029==End If  '-barcode-

                '=3101.1029==txtItemBarcode.Text = sBarcode
                '=3101.1029==txtItemBarcode.ReadOnly = True

                '--DO NOT strip leading zeroes..--
                '===While (Left(txtItemSerial.Text, 1) = "0") Or (Left(txtItemSerial.Text, 1) = " ")
                '===  txtItemSerial.Text = Mid(txtItemSerial.Text, 2)
                '===Wend
                sName = ""
                s1 = Trim(txtItemSerial.Text)
                '==LabItemDetails.Caption = ""
                txtItemDescription.Text = ""
                msSerialNo = s1
                msItemSupplierCode = ""
                Call mbResetNewFields() '--clear cust.. etc..
                '--  Lookup SerialNo in SerialAudit table..--
                '---- SerialAudit links SerailNo to SerialAuditTrail, which --
                '---    gives Stock_id, Goods_Id and and GoodsLine_Id..--

                If mRetailHost1.serialGetSerialInfo(-1, msSerialNo, colSerialResults) AndAlso _
                                  (Not (colSerialResults Is Nothing)) AndAlso (colSerialResults.Count > 0) Then
                    '== mlGoodsId = CLng(colSerialInfo("goods_id")("value"))
                    '== mlStockId = CLng(colSerialInfo("stock_id")("value"))
                    '== Call mbShowRecord("-Lookup SERIAL results.. -", colSerialInfo)

                    '=3101.1029= Serial may have multiple results..
                    '-- ask user to choose -> mColItemFields..
                    If (colSerialResults.Count = 1) Then
                        mColItemFields = colSerialResults.Item(1)  '--only 1..-
                    Else '-Multiples. user must choose.
                        sList = ""
                        ix = 0
                        For Each colSerialInfo In colSerialResults
                            ix += 1  '--count-
                            '== colSerialInfo = colSerialResults.Item(ix)
                            s1 = Trim(colSerialInfo.Item("barcode")("value"))
                            s2 = Trim(colSerialInfo.Item("description")("value"))
                            sList &= CStr(ix) & ". b/code: " & s1 & "  (" & s2 & ")." & vbCrLf
                        Next colSerialInfo
                        sList &= CStr(ix + 1) & ". None.  Cancel. "
                        sAns = ""
                        While (Not IsNumeric(sAns)) OrElse ((CStr(sAns) < 1) Or (CStr(sAns) > ix + 1))
                            sAns = InputBox("There are " & colSerialResults.Count & _
                                   " products on file with SerialNo: " & msSerialNo & ": " & vbCrLf & _
                                     "Please choose- " & vbCrLf & vbCrLf & sList & vbCrLf & vbCrLf, "RA Serials.")
                        End While '-ans-
                        If CStr(sAns) = ix + 1 Then
                            Exit Sub
                        Else  '-choice-
                            If colSerialResults.Contains(CStr(sAns)) Then
                                mColItemFields = colSerialResults.Item(CStr(sAns))
                            Else
                                Exit Sub
                            End If
                        End If
                    End If '-count-
                    FrameInvoiceList.Visible = False
                    FrameSupplier.Visible = True
                    grpBoxOrigin.Visible = True
                    '===msItemSupplierCode = CLng(mColItemFields("SupplierCode")("value"))

                    '==  -- 3357.0205- 05-Feb-2017-
                    '==  PLUS-   mIntSerialAudit_id As Integer = -1
                    '== colSerialInfo.Item("SerialAudit_Id")("value")
                    mIntSerialAudit_id = CInt(mColItemFields.Item("SerialAudit_Id")("value"))

                    mlStockId = CInt(mColItemFields.Item("stock_id")("value"))
                    mlGoodsId = CInt(mColItemFields.Item("goods_id")("value"))
                    mlGoodsLineId = CInt(mColItemFields.Item("goodsLine_id")("value"))
                    msDescr = Trim(mColItemFields.Item("description")("value"))
                    msCat1 = Trim(mColItemFields.Item("cat1")("value"))
                    msCat2 = Trim(mColItemFields.Item("cat2")("value"))
                    mCurSellEx = CDec(mColItemFields.Item("sell")("value"))
                    msItemBarcode = mColItemFields.Item("barcode")("value")
                    txtItemBarcode.Text = msItemBarcode
                    txtItemDescription.Text = msCat1 & "- " & msDescr & vbCrLf & _
                                        "StockId: " & mlStockId & " (" & msCat1 & "- " & msCat2 & ")" & ": " & _
                                                                   FormatCurrency(mCurSellEx, 2) & " (Ex GST.)" & vbCrLf
                    System.Windows.Forms.Application.DoEvents()
                    '-- Now lookup Goodsline/Stock to get Line Qty and SupplierCode info..--
                    '==  NOT NEEDED..  all included in above collection.-

                    '== If Not mbRMLookup(sSql, colRAFields) Then
                    '-- no matches means no SuplierCode.. --
                    '=== MsgBox "Failed to retrieve SupplierCode Info." + vbCrLf + "SQL was:" + vbCrLf + sSql, vbExclamation
                    '== Else '--found..-
                    '== If Not IsNull(colRAFields("SupplierCode")("value")) Then
                    '==             msItemSupplierCode = colRAFields("SupplierCode")("value")
                    '== End If

                    If Not IsDBNull(mColItemFields.Item("SupplierCode")("value")) Then
                        msItemSupplierCode = mColItemFields.Item("SupplierCode")("value")
                    End If
                    '==3311.323=  txtSupplierCode.Text = msItemSupplierCode
                    txtItemDescription.Text &= "Suppl.Code: " & msItemSupplierCode
                    '== End If   '--lookup supCode..-
                    '-- check if SerialNo aleady has RA..--
                    '--  Ignore those Refused or Cancelled..-
                    '---- WARN if status=completed..  05Aug2011--
                    '---- WARN if status=completed..  05Aug2011--
                    sSql = " SELECT * FROM [RAItems] "
                    sSql = sSql & " WHERE (RA_SerialNumber='" & msSerialNo & "') AND (LEFT(RA_Status,2)<'90')"
                    If mbGetJobTrackingRecord(sSql, colRAFields) Then
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        lngRA_id = colRAFields.Item("RA_Id")("value")
                        sStatus = colRAFields.Item("RA_Status")("value")
                        '--  COULD be COMPLETED..--
                        If (VB.Left(sStatus, 2) < "70") Then '--current.--
                            MsgBox("Possible duplicate serialNo in a CURRENT RA:" & vbCrLf & _
                                   " RA No: " & lngRA_id & "  Status: " & sStatus & vbCrLf & _
                                           " already exists for SerialNo: " & msSerialNo, MsgBoxStyle.Exclamation)
                            txtItemSerial.SelectionStart = 0
                            txtItemSerial.SelectionLength = Len(txtItemSerial.Text)
                            keyAscii = 0
                            colRAFields = Nothing
                            GoTo EventExitSub
                        Else '-- is RECEIVED/completed..-
                            If (MsgBox("Possible duplicate serialNo!  the COMPLETED RA:" & vbCrLf & _
                                         "     RA No: " & lngRA_id & "  Status: " & sStatus & vbCrLf & vbCrLf & _
                                            " already exists for SerialNo: " & msSerialNo & vbCrLf & vbCrLf & _
                                            "Do you still want to create a new RA for this Serial?", _
                                    MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then
                                txtItemSerial.SelectionStart = 0
                                txtItemSerial.SelectionLength = Len(txtItemSerial.Text)
                                keyAscii = 0
                                colRAFields = Nothing
                                GoTo EventExitSub
                            End If '--yes..-
                        End If '--completed..--
                    End If '--get job..--
                    '-- Now lookup Goods table and get invoice/supplier Info..-
                    If Not mbLookupSupplier(mlGoodsId, mColInvoiceFields) Then
                        MsgBox("Failed to get supplier info.", MsgBoxStyle.Exclamation)
                    Else '--ok-
                        Call mbSetUpSupplier(mColInvoiceFields)
                        txtInvoiceInfo.Text = "InvoiceNo: " & mColInvoiceFields.Item("Invoice_no")("value") & _
                                        "  Date: " & VB6.Format(CDate(mColInvoiceFields.Item("Invoice_date")("value"))) & _
                                     vbCrLf & "Goods Date: " & VB6.Format(CDate(mColInvoiceFields.Item("goods_date")("value"))) & _
                                     vbCrLf & "Order No: " & mColInvoiceFields.Item("order_no")("value") & vbCrLf & _
                                                                  "ORIG.SUPPLIER: " & mColInvoiceFields.Item("supplier")("value")
                        cmdNewSupplier.Enabled = True
                        txtSymptoms.ReadOnly = False
                        '=3311.325= SSTab1.TabPages.Item(k_SSTAB_CUST).Enabled = True
                        txtSymptoms.Focus()
                    End If '--supplier info..-
                Else '--not found..-
                    MsgBox("Can't find that item SerialNo..", MsgBoxStyle.Exclamation)
                End If '-Lookup SerialAudit-
            End If '--empty--
            keyAscii = 0
        End If '--13-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

EventExitSub:
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--  lookup Serial.--
    '= = = = = = = = = = = = = = = =  =
    '-===FF->

    '-- Got focus on Barcode..-

    Private Sub txtItemBarcode_Enter(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles txtItemBarcode.Enter
        Dim L1 As Integer

        If mbUpdating Or mbCreated Then Exit Sub
        If txtItemSerial.Text <> "" Then
            '==   MsgBox "Can't enter SerialNo AND Barcode..", vbExclamation
            Exit Sub
        End If
        cmdNewSupplier.Enabled = False
        txtItemDescription.Text = ""
        txtInvoiceInfo.Text = ""
        '=3311.323= txtSupplierCode.Text = ""
        '===LabInvoiceInfo.Caption = ""
        txtSupplierInfo.Text = ""

        L1 = Len(txtItemBarcode.Text)
        If L1 > 0 Then
            txtItemBarcode.SelectionStart = 0
            txtItemBarcode.SelectionLength = L1
        End If
    End Sub  '-barcode-enter-
    '= = = = = = = = = = = = =
    '-===FF->

    Private Sub mSetupStockRecord(ByVal sBarcode As String)

        Call mbResetNewFields()
        txtItemDescription.Text = ""
        '==LabItemDetails.Caption = ""
        msItemBarcode = sBarcode
        msItemSupplierCode = ""
        '--lookup RM stock table--
        If mRetailHost1.stockGetStockRecord(msItemBarcode, -1, mColItemFields) Then
            '===msItemSupplierCode = CLng(mColItemFields("SupplierCode")("value"))
            mlStockId = CInt(mColItemFields.Item("stock_id")("value"))
            '===   mlGoodsId = CLng(mColItemFields("goods_id")("value"))
            msDescr = Trim(mColItemFields.Item("description")("value"))
            msCat1 = Trim(mColItemFields.Item("cat1")("value"))
            msCat2 = Trim(mColItemFields.Item("cat2")("value"))
            mCurSellEx = CDec(mColItemFields.Item("sell")("value"))
            txtItemBarcode.Text = msItemBarcode
            txtItemDescription.Text = msCat1 & "- " & msDescr & vbCrLf & _
                       "StockId: " & mlStockId & " (" & msCat1 & "- " & msCat2 & ")" & _
                                    ": " & FormatCurrency(mCurSellEx, 2) & " (Ex GST.)"
            If Not mbLoadInvoices(mlStockId) Then

            Else '--ok-
                Chk12Mths.Enabled = True
            End If '--load-
        Else
            MsgBox("Can't find that item..", MsgBoxStyle.Exclamation)
            msItemBarcode = ""
        End If '--lookup..-
    End Sub  '-- setup..-
    '= = = = = = =  = == = = = =
    '-===FF->

    '-- trap F2 key on item BARCODE..--
    '--  lookup Stock-List (Browse) and get barcode, get stock item info.. etc--
    '-- THEN lookup Goodslines for that Stock id, and SHOW all Invoices..--

    Public Sub txtItemBarcode_KeyDown(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                        Handles txtItemBarcode.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        '== Dim colKeys As Collection
        Dim colRecord As Collection
        Dim strBarcode As String
        Dim intStock_id As Integer

        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup stock--
            If mRetailHost1.stockLookup(False, "", "", colRecord) Then
                strBarcode = colRecord.Item("barcode")("value")
                intStock_id = colRecord.Item("Stock_id")("value")
                txtBarcode.Text = strBarcode
                If strBarcode <> "" Then
                    Call mSetupStockRecord(strBarcode)
                Else
                    txtItemSerial.Enabled = True '--can go back to serial no..-
                End If
            Else
                txtItemSerial.Enabled = True '--can go back to serial no..-
            End If  '--Lookup-
        End If  '-F2-

    End Sub  '-txtItemBarcode_KeyDown-
    '= = = = = = = = = = = = =
    '-===FF->

    '-- trap ENTER key on item BARCODE..--
    '--  lookup stock-id FROM barcode, and get stock item info.. etc--
    '-- THEN lookup Goodslines for that Stock id, and SHOW all Invoices..--

    Private Sub txtItemBarcode_KeyPress(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                            Handles txtItemBarcode.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        Dim s1 As String
        Dim sName As String

        If mbUpdating Or mbCreated Then GoTo EventExitSub
        If keyAscii = 13 Then '--enter-
            '--REv: 2907..-- strip leading zeroes..--

            '== -- 3077-20May2013- NO! DO NOT strip leading zeroes
            '==3077.611= While (VB.Left(txtItemBarcode.Text, 1) = " ")
            '==3077.611= txtItemBarcode.Text = Mid(txtItemBarcode.Text, 2)
            '==3077.611= End While

            '==YES- Build-3077.611:  Strip LEADING ZEROES Unless "Keep" is checked..
            If Not chkKeepScannedLeadZeroes.Checked Then
                While (VB.Left(txtItemBarcode.Text, 1) = "0") Or (VB.Left(txtItemBarcode.Text, 1) = " ")
                    '==    While (VB.Left(txtPartNo.Text, 1) = " ")
                    txtItemBarcode.Text = Mid(txtItemBarcode.Text, 2)
                End While
            End If  '--checked..-

            s1 = Trim(txtItemBarcode.Text)
            sName = ""
            If (s1 <> "") Then
                Call mSetupStockRecord(s1)
                '=== Call mbResetNewFields()
                '=== txtItemDescription.Text = ""
                '=== '==LabItemDetails.Caption = ""
                '=== msItemBarcode = s1
                '=== msItemSupplierCode = ""
                '=== '--lookup RM stock table--
                '=== If mRetailHost1.stockGetStockRecord(msItemBarcode, -1, mColItemFields) Then
                '===   '===msItemSupplierCode = CLng(mColItemFields("SupplierCode")("value"))
                '=== mlStockId = CInt(mColItemFields.Item("stock_id")("value"))
                '=== '===   mlGoodsId = CLng(mColItemFields("goods_id")("value"))
                '=== msDescr = Trim(mColItemFields.Item("description")("value"))
                '=== msCat1 = Trim(mColItemFields.Item("cat1")("value"))
                '=== msCat2 = Trim(mColItemFields.Item("cat2")("value"))
                '=== '==msCat3 = Trim(mColItemFields("cat3")("value"))
                '=== mCurSellEx = CDec(mColItemFields.Item("sell")("value"))
                '=== txtItemBarcode.Text = msItemBarcode
                '=== txtItemDescription.Text = msCat1 & "- " & msDescr & vbCrLf & _
                '===            "StockId: " & mlStockId & " (" & msCat1 & "- " & msCat2 & ")" & _
                '===                         ": " & FormatCurrency(mCurSellEx, 2) & " (Ex GST.)"
                '=== If Not mbLoadInvoices(mlStockId) Then
                '=== Else '--ok-
                '===  Chk12Mths.Enabled = True
                '== Else
                '== MsgBox("Can't find that item..", MsgBoxStyle.Exclamation)
                '== msItemBarcode = ""
                '== End If '--lookup..-
            Else
                txtItemSerial.Enabled = True '--can go back to serial no..-
            End If '--empty--
            keyAscii = 0
        End If '--13-
        '== Set rsGoods = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

EventExitSub:
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--barcode..-
    '= = = = = = = = = =

    '-- click 12 Months check box..--
    '--- Reload listView..-

    'UPGRADE_WARNING: Event Chk12Mths.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub Chk12Mths_CheckStateChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Chk12Mths.CheckStateChanged

        If msItemBarcode <> "" Then
            If Not mbLoadInvoices(mlStockId) Then

            End If
        End If

    End Sub '--12 months..-
    '-===FF->

    '- sort invoices.--

    '--re-sort on selected column..-
    '--re-sort on selected column..-

    Private Sub listViewGoods_ColumnClick(ByVal eventSender As System.Object, _
                                                   ByVal eventArgs As System.Windows.Forms.ColumnClickEventArgs) _
                                                     Handles ListViewGoods.ColumnClick
        Dim lngNewKey As Integer
        Dim colHdr1 As System.Windows.Forms.ColumnHeader = ListViewGoods.Columns(eventArgs.Column)

        lngNewKey = eventArgs.Column   '== colHdr1.Index - 1 '-- get zero index of column clicked..-
        '--MsgBox "Clicked col-no: " & lngNewKey
        With ListViewGoods
            ListViewGoods.Sort()
            If (lngNewKey = mlSortKey) Then '--same col clicked again..-
                If mlSortOrder = System.Windows.Forms.SortOrder.Ascending Then
                    mlSortOrder = System.Windows.Forms.SortOrder.Descending '--invert order..-
                    colHdr1.ImageKey = "ArrowDown"
                Else
                    mlSortOrder = System.Windows.Forms.SortOrder.Ascending
                    colHdr1.ImageKey = "ArrowUp"
                End If
                '== .Sort()
                .Sorting = mlSortOrder
            Else '--changed col.--
                '--  clear arrow from old column.-
                If (mlSortKey >= 0) Then  '--was sorted.-
                    ListViewGoods.Columns(mlSortKey).ImageKey = ""
                End If
                .Sorting = System.Windows.Forms.SortOrder.Ascending  '--start asc..-
                mlSortOrder = System.Windows.Forms.SortOrder.Ascending  '--remember.-
                colHdr1.ImageKey = "ArrowUp"
            End If
        End With
        mlSortKey = lngNewKey '--remember current column.-
        ListViewGoods.ListViewItemSorter = New ListViewItemComparer_RAs(eventArgs.Column, ListViewGoods.Sorting)
    End Sub '--colClick..-
    '= = = = = = = = =  = = = = = = = = 
    '-===FF->

    '-  Select Invoice..-
    '-  Select Invoice..-

    Private Sub listViewGoods_DoubleClick(ByVal eventSender As System.Object, _
                                           ByVal eventArgs As System.EventArgs) Handles ListViewGoods.DoubleClick
        Dim idx, kx, lngErr As Integer
        Dim sSql As String
        Dim s1, s2 As String
        Dim sBarcode, sGoodsId As String
        Dim sInvoiceNo As String
        Dim sSuppCode As String
        Dim scrx, part_id, lngaffected As Integer
        Dim sErrorMsg As String
        Dim item1 As System.Windows.Forms.ListViewItem

        '-- if an item is selected, then show invoice details..-..--

        On Error Resume Next
        item1 = ListViewGoods.FocusedItem '--   .ListIndex
        lngErr = Err().Number
        On Error GoTo 0
        If (lngErr = 0) Then
            If (item1 Is Nothing) Then
                MsgBox("Nothing selected", MsgBoxStyle.Exclamation)
            Else '-ok-
                '-- set up invoice details..-
                '--  NOW get full INVOICE/Supplier recordset..-

                '==  DO NOT DO THIS IN ITS OWN CLICK ROUTINE..
                '==          ==FrameInvoiceList.Visible = False

                mlGoodsId = -1
                sGoodsId = item1.SubItems(5).Text '-- ListTasks.List(idx)
                If IsNumeric(sGoodsId) Then mlGoodsId = CInt(sGoodsId)
                sInvoiceNo = item1.Text '-- ListTasks.List(idx)
                sSuppCode = item1.SubItems(3).Text

                '--  SHOW suppliercode for this Item/Invoice/Supplier--(Fourth iiem col.)--
                FrameSupplier.Visible = True
                '==3311.323=  txtSupplierCode.Text = sSuppCode
                msItemSupplierCode = sSuppCode
                txtItemDescription.Text &= "Suppl.Code: " & msItemSupplierCode
                txtInvoiceInfo.Text = sInvoiceNo

                '===MsgBox "Invoice " + sInvoiceNo + " selected.."
                '-- Now lookup Goods table and get invoice/supplier Info..-
                If Not mbLookupSupplier(mlGoodsId, mColInvoiceFields) Then
                    MsgBox("Failed to get supplier info.", MsgBoxStyle.Exclamation)
                Else '--ok-
                    Call mbSetUpSupplier(mColInvoiceFields)
                    txtInvoiceInfo.Text = "InvoiceNo: " & mColInvoiceFields.Item("Invoice_no")("value") & _
                                   "  Date: " & VB6.Format(CDate(mColInvoiceFields.Item("Invoice_date")("value"))) & vbCrLf & _
                                    "Goods Date: " & VB6.Format(CDate(mColInvoiceFields.Item("goods_date")("value"))) & vbCrLf & _
                                    "Order No: " & mColInvoiceFields.Item("order_no")("value") & vbCrLf & _
                                    "ORIG.SUPPLIER: " + mColInvoiceFields.Item("supplier")("value")
                    cmdNewSupplier.Enabled = True
                    grpBoxOrigin.Visible = True  '==3311.420= Fix=
                    txtSymptoms.ReadOnly = False
                    '=3311.325= SSTab1.TabPages.Item(k_SSTAB_CUST).Enabled = True
                    txtSymptoms.Focus()
                End If
            End If '--empty--
        End If '--error-
    End Sub '--dblclick--
    '= = = = = = = = = =

    '--cmdSelectInvoice--

    Private Sub cmdSelectInvoice_Click(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles cmdSelectInvoice.Click

        Call listViewGoods_DoubleClick(ListViewGoods, New System.EventArgs())
    End Sub '--cmdSelectInvoice--
    '= = = = = = = =
    '-===FF->

    '-- Change Supplier- IN New RA--.--
    '-- Change Supplier- IN New RA--.--

    Private Sub cmdNewSupplier_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles cmdNewSupplier.Click
        Dim colSupplierRecord As Collection

        If mbChooseSupplier(colSupplierRecord) Then '--ok-
            '--load new supplier info.-
            Call mbSetUpSupplier(colSupplierRecord)
        End If
        colSupplierRecord = Nothing
        txtSymptoms.Focus()

    End Sub '--new supplier..-
    '= = = = = = = = = = =

    '-- Change Supplier-  WHEN Updating EXISTING RA. (Before RMA Request..)--
    '-- Change Supplier-  WHEN Updating EXISTING RA. (Before RMA Request..)--

    Private Sub cmdNewSupplier2_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles cmdNewSupplier2.Click
        Dim colSupplierRecord As Collection
        Dim sSql As String
        Dim s1 As String
        Dim sSupplier As String
        Dim ix As Integer

        If mbChooseSupplier(colSupplierRecord) Then '--ok-
            sSupplier = colSupplierRecord.Item("supplier")("value")
            If (MsgBox("Are you sure you want to change Supplier to '" & sSupplier & "' ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                '--load new supplier info.-
                Call mbSetUpSupplier(colSupplierRecord)
                sSql = "UPDATE  [RAItems] SET "
                sSql = sSql & " RM_SupplierId=" & CStr(mlSupplierId)
                sSql = sSql & ", RM_SupplierBarcode=" & " '" & msFixSqlStr(msSupplierBarcode) & "'"
                sSql = sSql & ", RM_Supplier=" & " '" & msFixSqlStr(msSupplier) & "'"
                sSql = sSql & ", RM_Supplier_main_phone=" & " '" & msFixSqlStr(msSupplierMainPhone) & "'"
                sSql = sSql & ", RM_Supplier_main_fax=" & " '" & msFixSqlStr(msSupplierMainFax) & "'"
                sSql = sSql & ", RM_Supplier_main_email=" & " '" & msFixSqlStr(msSupplierMainEmail) & "'"
                sSql = sSql & ", RM_Supplier_AddressInfo=" & " '" & msFixSqlStr(msSupplierAddressInfo) & "'"
                sSql = sSql & ", RM_StaffNameUpdated='" & msStaffName & "'"
                sSql = sSql & ", RA_DateUpdated=CURRENT_TIMESTAMP "
                sSql = sSql & " WHERE (RA_id=" & CStr(mlRA_id) & ") "
                If Not mbUpdateRARecord(sSql) Then
                    Me.Close()
                    Exit Sub
                Else '--ok..-
                    cmdNewSupplier2.Enabled = False
                End If '--update.-
            End If '--yes..-
            Call ActivateReActivate() '-- TextBox has changed. RE-load updated RA record to refresh...--
        End If  '--chose supplier-
        colSupplierRecord = Nothing
    End Sub '--NewSupplier2--
    '= = = = = = = = = =
    '-===FF->

    '--Update Supplier Address details from Retail manager..

    Private Sub btnUpdateSupplierAddress_Click(sender As Object, ev As EventArgs) _
                                                      Handles btnUpdateSupplierAddress.Click
        Dim colSupplierRecord As Collection
        Dim sSql As String
        '== Dim s1 As String
        '== Dim sSupplier As String
        '== Dim ix As Integer

        If (MsgBox("Update Supplier Address Details from Retail Database ?", _
                            MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
            If mRetailHost1.supplierGetSupplierRecord(msSupplierBarcode, mlSupplierId, colSupplierRecord) Then
                '--load new supplier info.-
                Call mbSetUpSupplier(colSupplierRecord, True)  '--Address update only..
                '-- UPDATE RA recoed..
                sSql = "UPDATE  [RAItems] SET "
                '== sSql = sSql & " RM_SupplierId=" & CStr(mlSupplierId)
                '== sSql = sSql & ", RM_SupplierBarcode=" & " '" & msFixSqlStr(msSupplierBarcode) & "'"
                '== sSql = sSql & ", RM_Supplier=" & " '" & msFixSqlStr(msSupplier) & "'"
                sSql = sSql & " RM_Supplier_main_phone=" & " '" & msFixSqlStr(msSupplierMainPhone) & "'"
                sSql = sSql & ", RM_Supplier_main_fax=" & " '" & msFixSqlStr(msSupplierMainFax) & "'"
                sSql = sSql & ", RM_Supplier_main_email=" & " '" & msFixSqlStr(msSupplierMainEmail) & "'"
                sSql = sSql & ", RM_Supplier_AddressInfo=" & " '" & msFixSqlStr(msSupplierAddressInfo) & "'"
                sSql = sSql & ", RM_StaffNameUpdated='" & msStaffName & "'"
                sSql = sSql & ", RA_DateUpdated=CURRENT_TIMESTAMP "
                sSql = sSql & " WHERE (RA_id=" & CStr(mlRA_id) & ") "
                If Not mbUpdateRARecord(sSql) Then
                    Me.Close()
                    Exit Sub
                Else '--ok..-
                    btnUpdateSupplierAddress.Enabled = False
                End If '--update.-
                Call ActivateReActivate() '-- TextBox has changed. RE-load updated RA record to refresh...--
                MsgBox("Ok. Update done..", MsgBoxStyle.Information)
            Else
                MsgBox("Failed to get Retail Supplier Record..", MsgBoxStyle.Exclamation)
            End If  '-set record-
        End If '-yes, do it-
    End Sub  '-btnUpdateSupplierAddress-
    '= = = = = = = = = =  = = = = == = = = = = = 
    '-===FF->

    '-- Symptoms..-
    Private Sub txtSymptoms_Enter(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles txtSymptoms.Enter


        FrameInvoiceList.Visible = False '==in case..-
    End Sub
    '= = = = = = = = = = = =

    'UPGRADE_WARNING: Event txtSymptoms.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtSymptoms_TextChanged(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles txtSymptoms.TextChanged
        If mbIsInitialising Then Exit Sub
        panelOrigin.Enabled = True  '=grpBoxOrigin.Enabled = True   '=   cboOrigin.Enabled = True
        '==txtJobNo.Enabled = True

    End Sub '-symptoms--
    '= = = = = = = = = = = = =

    '==3311.306= If Customer Info available from Serial Lookup..

    'Private Sub txtSymptoms_KeyPress(ByVal eventSender As System.Object, _
    '                             ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)  '==  Handles txtSymptoms.KeyPress
    '    Dim keyAscii As Short = Asc(eventArgs.KeyChar)
    '    Dim s1, sValue, sName, sBarcode, sInfo As String
    '    Dim colFields As Collection

    '    If keyAscii = 13 Then '--enter-
    '        '==3311.306= If Customer Info available from Serial Lookup..
    '        '---  then ask if we can use this customer..
    '        If (Not (mColItemFields Is Nothing)) AndAlso mColItemFields.Contains("item_sale_info") Then
    '            Dim colSaleInfo As Collection = mColItemFields.Item("item_sale_info")
    '            If (colSaleInfo.Count > 0) Then  '-have some-
    '                sInfo = "NB: This Item seems to have a Sales History. Details are:" & vbCrLf
    '                For Each col1 As Collection In colSaleInfo
    '                    sName = LCase(col1.Item("name"))
    '                    sInfo &= sName & " :  "
    '                    If (sName = "sale_date") Then
    '                        sInfo &= VB.Format(CDate(col1.Item("value")), "dd-MMM-yyyy") & vbCrLf
    '                    Else
    '                        sValue = col1.Item("value")
    '                        sInfo &= sValue & vbCrLf
    '                        If (sName = "customer_barcode") Then
    '                            sBarcode = sValue
    '                        ElseIf (sName = "customer_company") Then
    '                            txtCustCompany.Text = sValue
    '                        End If
    '                    End If
    '                Next col1
    '                txtCustNo.Text = sBarcode
    '                '--lookup RM cust table--
    '                '-- set up customer..
    '                If mRetailHost1.customerGetCustomerRecord(sBarcode, -1, colFields) Then '--found..--
    '                    '== If mbLookupCustomer(s1, colFields) Then
    '                    Call mbSetupCustomer(colFields)
    '                    sName = msCustomerCompany
    '                    cmdSave.Enabled = True
    '                Else '--not found-
    '                    MsgBox("Customer: " & sBarcode & " not found..", MsgBoxStyle.Exclamation)
    '                End If
    '                MsgBox(sInfo & vbCrLf & vbCrLf & _
    '                                 "You can change the Customer if needed..", MsgBoxStyle.Exclamation)
    '            End If  '-count-
    '            optOrigin_counter.Checked = True  '= cboOrigin.SelectedItem = "Counter"
    '        Else '-no sale on record,
    '            grpBoxOrigin.Enabled = True '= cboOrigin.Enabled = True
    '            '= cboOrigin.Select()
    '        End If
    '        keyAscii = 0
    '        eventArgs.Handled = True
    '    End If  '--13-
    'End Sub  '--enter key on symptoms.
    '= = = = = = = =  = = = = = == =  =
    '-===FF->

    '==  -- 3327.0119- 19-Jan-2017-
    '==             NOW user must now TAB away from Symptoms textbox.

    '-txtSymptoms_validating-

    Private Sub txtSymptoms_validating(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.ComponentModel.CancelEventArgs) _
                                   Handles txtSymptoms.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim s1 As String
        Dim lngJobId As Integer

        s1 = Trim(txtSymptoms.Text)
        If s1 = "" Then
            MsgBox("Symptoms is a required field.  PLs enter something..", MsgBoxStyle.Exclamation)
            keepfocus = True
        Else  '-ok-
            keepfocus = False   '-let it go-
        End If
    End Sub  '-txtSymptoms_validating-
    '= = = = = = = = = = = = = = = = = =

    '- txtSymptoms_Validated-

    Private Sub txtSymptoms_Validated(ByVal sender As Object, _
                                     ByVal e As System.EventArgs) Handles txtSymptoms.Validated

        Dim s1, sValue, sName, sBarcode, sInfo, sItemSaleHdr As String
        Dim sSaleDate, sInvoiceNo As String
        Dim colFields As Collection

        '==3327.0120= /0121-
        If (txtCustNo.Text <> "") Then
            Exit Sub  '-already have customer-
        End If
        sSaleDate = ""
        sInvoiceNo = ""
        '- If Customer Info available from Serial Lookup..
        '---    then ask if we can use this customer..
        If (Not (mColItemFields Is Nothing)) AndAlso mColItemFields.Contains("item_sale_info") AndAlso
                  mbExtractItemSalesInfo(mColItemFields, sBarcode, sItemSaleHdr, sInvoiceNo, sSaleDate, sInfo) Then

            '= Dim colSaleInfo As Collection = mColItemFields.Item("item_sale_info")
            '= If (colSaleInfo.Count > 0) Then  '-have some-
            'sInfo = ""  '= "NB: This Item seems to have a Sales History. Details are:" & vbCrLf
            'For Each col1 As Collection In colSaleInfo
            '    sName = LCase(col1.Item("name"))
            '    sValue = col1.Item("value")
            '    sInfo &= sName & " :  "
            '    If (sName = "sale_date") Then
            '        sSaleDate = VB.Format(CDate(col1.Item("value")), "dd-MMM-yyyy")
            '        sInfo &= VB.Format(CDate(col1.Item("value")), "dd-MMM-yyyy") & vbCrLf
            '    Else
            '        sInfo &= sValue & vbCrLf
            '        If (sName = "customer_barcode") Then
            '            sBarcode = sValue
            '        ElseIf (sName = "customer_company") Then
            '            txtCustCompany.Text = sValue
            '        ElseIf (InStr(sName, "sale_invoice_no") > 0) Then
            '            '=3403.611= - Show invoice No. and date.
            '            labItemSaleHdr.Text = sName
            '            sInvoiceNo = sValue
            '        End If
            '    End If
            'Next col1
            labItemSaleHdr.Text = UCase(sItemSaleHdr)
            txtItemSaleInvoiceNo.Text = sInvoiceNo  '= & ". " & sSaleDate
            txtItemSaleInvoiceDate.Text = sSaleDate
            txtCustNo.Text = sBarcode
            '--lookup RM cust table--
            '-- set up customer..
            If mRetailHost1.customerGetCustomerRecord(sBarcode, -1, colFields) Then '--found..--
                '== If mbLookupCustomer(s1, colFields) Then
                Call mbSetupCustomer(colFields)
                sName = msCustomerCompany
                cmdSave.Enabled = True
                txtRequestNotes.Enabled = True  '=3403.719=
            Else '--not found-
                MsgBox("Customer: " & sBarcode & " not found..", MsgBoxStyle.Exclamation)
            End If
            MsgBox("NB: This Item seems to have a Sales History. Details are:" & vbCrLf & _
                      sInfo & vbCrLf & vbCrLf & _
                             "You can change the Customer if needed..", MsgBoxStyle.Exclamation)
            '= End If  '-count-
            optOrigin_counter.Checked = True  '= cboOrigin.SelectedItem = "Counter"
        Else '-no sale on record,
            panelOrigin.Enabled = True '= grpBoxOrigin.Enabled = True '= cboOrigin.Enabled = True
            '= cboOrigin.Select()
        End If
    End Sub '- txtSymptoms_Validated-
    '= = = = = = = = = = = = = == = = = = 
    '-===FF->


    Private Sub cboOrigin_SelectedIndexChanged(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As System.EventArgs)
        Dim colEmptyCust As Collection
        colEmptyCust = New Collection
        '==331.323= If (cboOrigin.SelectedIndex >= 0) Then '--something
        '==331.323= msOrigin = VB6.GetItemString(cboOrigin, cboOrigin.SelectedIndex)
        '==331.323= txtJobNo.Enabled = False
        '==331.323= LabJobNo.Enabled = False
        '==331.323= txtJobNo.Text = ""
        '==331.323= If InStr(LCase(msOrigin), "job") > 0 Then '-is job..-
        '==331.323= LabJobNo.Enabled = True
        '==331.323= txtJobNo.Enabled = True
        '==331.323= txtJobNo.ReadOnly = False
        '==331.323= txtJobNo.Focus()
        '==331.323= ElseIf (InStr(LCase(msOrigin), "counter") > 0) Or (InStr(LCase(msOrigin), "stock") > 0) Then  '-counter/stock..-
        '==331.323= FrameCust.Enabled = True
        '==331.323= txtCustNo.Focus()
        '==331.323= Else '--stock..-
        '==331.323= End If
        '==331.323= End If '--listindex..-
        '==331.323= colEmptyCust = Nothing
    End Sub '--click..-
    '= = = = = = = = =

    '--job or counter..

    Private Sub optOrigin_counter_CheckedChanged(sender As Object, e As EventArgs) _
                                                            Handles optOrigin_counter.CheckedChanged, _
                                                            optOrigin_job.CheckedChanged, _
                                                            optOrigin_stock.CheckedChanged
        If mbIsInitialising Then Exit Sub
        txtJobNo.Enabled = False
        LabJobNo.Enabled = False
        txtJobNo.Text = ""

        If optOrigin_counter.Checked Then
            txtCustNo.Focus()
            msOrigin = "counter"
        ElseIf optOrigin_job.Checked Then
            msOrigin = "job"
            LabJobNo.Enabled = True
            txtJobNo.Enabled = True
            txtJobNo.ReadOnly = False
            txtJobNo.Focus()

        ElseIf optOrigin_stock.Checked Then
            msOrigin = "stock"
            '== 3411.0128 -txtCustNo.Focus()
            '== 3411.0128 - Stock doesn't need Customer..
            cmdSave.Enabled = True
            txtRequestNotes.Enabled = True  '=3403.719=

        End If  '-checked-

    End Sub  '-- optOrigin_counter-
    '= = = = = = = =  = =
    '-===FF->

    '--Transfer to Stock..--

    'UPGRADE_WARNING: Event chkTfrToStock.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub chkTfrToStock_CheckStateChanged(ByVal eventSender As System.Object, _
                                                   ByVal eventArgs As System.EventArgs) Handles chkTfrToStock.CheckStateChanged

        If mbIsInitialising Then Exit Sub
        cmdSave.Enabled = True
        txtRequestNotes.Enabled = True  '=3403.719=
        cmdCancel.Text = "Cancel" '-- now loaded..-

    End Sub '--tfr click..--
    '= = = = = = = =  = =
    '-===FF->

    '--Lookup JOB --
    '-- Setup Job Cust Details..--
    Private Function mbSetupJobCustomer(ByRef lngJobId As Integer) As Boolean
        Dim sSql As String

        mbSetupJobCustomer = False
        sSql = "SELECT * from [jobs]  "
        sSql = sSql & " WHERE (job_id=" & CStr(lngJobId) & ")  " & vbCrLf
        If mbGetJobTrackingRecord(sSql, mColJobFields) Then
            mlCustomerId = CInt(mColJobFields.Item("RMCustomer_Id")("value"))
            msCustomerBarcode = mColJobFields.Item("CustomerBarcode")("value")
            '=3311.325= FrameCust.Text = "Customer: " & msCustomerBarcode

            '--show customer details..--
            txtCustCompany.Text = mColJobFields.Item("CustomerCompany")("value")
            txtCustName.Text = mColJobFields.Item("CustomerName")("value")
            txtCustPhone.Text = mColJobFields.Item("CustomerPhone")("value")
            txtCustMobile.Text = mColJobFields.Item("CustomerMobile")("value")
            msCustomerName = txtCustName.Text
            msCustomerPhone = txtCustPhone.Text
            msCustomerMobile = txtCustMobile.Text
            msCustomerCompany = txtCustCompany.Text
            mlJobId = lngJobId
            mbSetupJobCustomer = True
        Else
            MsgBox("No Job Record " & lngJobId & " found..", MsgBoxStyle.Exclamation)
        End If '--get

    End Function '--SetpJobCustomer--
    '= = = = = = = = =
    '-===FF->

    '-- VALIDATE JOB no..--
    '--  lookup Job and get Customer Details--

    Private Sub txtJobNo_Validating(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtJobNo.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim s1 As String
        Dim lngJobId As Integer

        s1 = Trim(txtJobNo.Text)
        If s1 = "" Then '--no job..-
            mlJobId = -1
            '= '=3311.325=  FrameCust.Enabled = True
            txtCustCompany.Focus()
        ElseIf IsNumeric(s1) Then
            lngJobId = CInt(s1)
            If mbSetupJobCustomer(lngJobId) Then
                cmdSave.Enabled = True
                txtRequestNotes.Enabled = True  '=3403.719=
                '===cmdPrintRAForm.Enabled = True
                '===cmdPrintLabel.Enabled = True
                '= '=3311.325=  FrameCust.Enabled = False
            Else
                keepfocus = True
            End If '--setup--
        Else
            keepfocus = True '--non-numeric-
        End If '--numeric..-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        eventArgs.Cancel = keepfocus
    End Sub '--validate--
    '= = = = = = = = = ==

    '-- trap ENTER key on JOB no..--
    '--  lookup Job and get Customer Details--
    Private Sub txtJobNo_KeyPress(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtJobNo.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim lngJobId As Integer

        If keyAscii = 13 Then '--enter-
            s1 = Trim(txtJobNo.Text)
            If s1 = "" Then '--no job..-
                mlJobId = -1
                '=3311.325= FrameCust.Enabled = True
                txtCustCompany.Focus()
            ElseIf IsNumeric(s1) Then
                lngJobId = CInt(s1)
                If mbSetupJobCustomer(lngJobId) Then
                    cmdSave.Enabled = True
                    txtRequestNotes.Enabled = True  '=3403.719=
                    '===cmdPrintRAForm.Enabled = True
                    '===cmdPrintLabel.Enabled = True
                    '=3311.325= FrameCust.Enabled = False
                End If '--setup--
            End If '--numeric..-
            keyAscii = 0
        End If '--13-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--job..-
    '= = = = = = = = = =  =
    '-===FF->

    '--customer Lookup Stuff..--

    '-- Customer Name Entry/Lookup..--
    '-- Customer Name Entry/Lookup..--
    '-- Customer Name Entry/Lookup..--

    Private Sub txtCustNo_Enter(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles txtCustNo.Enter

        '--txtCustName.Text = ""
        '==txtCustNo.Text = "(Cust Barcode.)"
        txtCustNo.SelectionStart = 0
        txtCustNo.SelectionLength = Len(txtCustNo.Text)
    End Sub '--cust name got focus--
    '= = = =  = =  = =
    '-===FF->

    '--got function key----
    '--- check for F2 for cust Lookup--
    Private Sub txtCustNo_KeyUp(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles txtCustNo.KeyUp
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        Dim cx, j, i, k, fx As Integer
        Dim s1, s2 As String
        '--Dim lngActualSize As Long
        Dim lngControl As Integer
        Dim AltDown, ShiftDown, CtrlDown As Integer
        Dim sBarcode As String
        Dim colRecord As Collection '--full cust record..-

        ShiftDown = (Shift And VB6.ShiftConstants.ShiftMask) > 0
        AltDown = (Shift And VB6.ShiftConstants.AltMask) > 0
        CtrlDown = (Shift And VB6.ShiftConstants.CtrlMask) > 0

        lngControl = (VB6.ShiftConstants.ShiftMask + VB6.ShiftConstants.AltMask + VB6.ShiftConstants.CtrlMask)

        '--sOrigKey = Trim(txtCustName.Text) '--save orig key--
        If (KeyCode = System.Windows.Forms.Keys.F2) And ((Shift And lngControl) = 0) Then '--lookup cust--
            '--If ShiftDown And CtrlDown And AltDown Then  Txt = "SHIFT+CTRL+ALT+F2."
            '== mbBrowsing = False '--enable lost focus -
            System.Windows.Forms.Application.DoEvents()
            '--retrieve selected record and fill in cust details..--
            '--customer lookup (ext. browse)..---
            If mRetailHost1.customerLookup(colRecord) Then '--found..--
                If colRecord Is Nothing Then
                    '===Unload Me  '==Me.Hide
                    Exit Sub
                Else '--selection made..-
                    '-- get barcode..--
                    If (colRecord.Count() > 0) Then
                        sBarcode = colRecord.Item("barcode")("value")
                        txtCustNo.Text = sBarcode
                        '=== If Not mbLookupCustomer(sBarcode, colRecord) Then
                        '==     MsgBox "Failed to retrieve customer record for Barcode: '" + sBarcode + "'..", vbCritical
                        '== Else '--ok--
                        '--set up customer details.-
                        Call mbSetupCustomer(colRecord)
                        cmdSave.Enabled = True
                        txtRequestNotes.Enabled = True  '=3403.719=
                        '== End If
                    Else
                        If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                        '===Unload Me  '==Me.Hide
                        Exit Sub
                    End If '--got row..--
                End If '--nothing..-
            End If '--lookup..-
            System.Windows.Forms.Application.DoEvents()
        End If '--F2--
    End Sub '--keyup-
    '= = = = = = = = = = = =
    '-===FF->

    '--Cust Losing focus..  fetch cust name--
    '--Cust Losing focus..  fetch cust name--
    Private Sub txtCustNo_Validating(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.ComponentModel.CancelEventArgs) Handles txtCustNo.Validating
        Dim keepfocus As Boolean = eventArgs.Cancel
        Dim s1 As String
        Dim sName, sValue As String
        Dim colFields As Collection
        '= Dim colFld As Collection

        '===  If mbAmending Then Exit Sub  '--ok..  not available to user..-
        s1 = txtCustNo.Text '--barcode--
        sName = ""
        '== If IsNumeric(s1) Then '--lookup-
        If (s1 <> "") Then '--lookup-
            '--lookup RM cust table--
            '== If mbLookupCustomer(s1, colFields) Then
            If mRetailHost1.customerGetCustomerRecord(s1, -1, colFields) Then '--found..--
                Call mbSetupCustomer(colFields)
                sName = msCustomerCompany
            Else '--not found-
                MsgBox("Customer: " & s1 & " not found..", MsgBoxStyle.Exclamation)
            End If
        ElseIf (mlCustomerId >= 0) Then  '--keep prev entry..--
            sName = msCustomerCompany
            txtCustCompany.Text = sName
        Else
            '==MsgBox "cust ID must be numeric..", vbExclamation
            sName = "" '--loop again..-
        End If '--lookup-
        '--Wend  '--entry-
        If sName = "" Then
            '=== txtCustNo.Text = "(Cust No.)"
            txtCustNo.SelectionStart = 0
            txtCustNo.SelectionLength = Len(txtCustNo.Text)
            keepfocus = True '--txtCustName.SetFocus
        Else '-ok-
            cmdSave.Enabled = True
            txtRequestNotes.Enabled = True  '=3403.719=
            '===cmdPrintRAForm.Enabled = True
            '===cmdPrintLabel.Enabled = True
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '--End If '--browsing
        eventArgs.Cancel = keepfocus
    End Sub '--cust name validate--
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '-- ENTER KEY on Cust-name ---
    '-- ENTER KEY on Cust-name ---

    Private Sub txtCustNo_KeyPress(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtCustNo.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim sName, sValue As String
        Dim colFields As Collection
        '= Dim colFld As Collection
        '= Dim s3, s4 As String

        '-If Not mbBrowsing Then  '--ok-
        s1 = txtCustNo.Text
        sName = ""
        If keyAscii = 13 Then '--enter-
            '== If IsNumeric(s1) Then '--lookup barcode---
            If (s1 <> "") Then
                '--lookup RM cust table--
                If mRetailHost1.customerGetCustomerRecord(s1, -1, colFields) Then '--found..--
                    '== If mbLookupCustomer(s1, colFields) Then
                    Call mbSetupCustomer(colFields)
                    sName = msCustomerCompany
                Else '--not found-
                    MsgBox("Customer: " & s1 & " not found..", MsgBoxStyle.Exclamation)
                End If
            ElseIf (mlCustomerId >= 0) Then  '--keep prev entry..--
                sName = msCustomerCompany
                txtCustCompany.Text = sName
            Else
                '==MsgBox "cust ID must be numeric..", vbExclamation
                sName = "" '--loop again..-
            End If '--lookup-
            '--Wend  '--entry-
            If sName = "" Then
                '== txtCustNo.Text = "(Cust No.)"
                txtCustNo.SelectionStart = 0
                txtCustNo.SelectionLength = Len(txtCustCompany.Text)
                '--KeepFocus = True '--txtCustName.SetFocus
            Else '--ok-
                cmdSave.Enabled = True
                txtRequestNotes.Enabled = True  '=3403.719=
                '===cmdPrintRAForm.Enabled = True
                '===cmdPrintLabel.Enabled = True
            End If
            keyAscii = 0
        End If '--13-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--cust name keypress--
    '= = = = = = = = = = =  =  = =  =
    '== end ENTER key --
    '-===FF->

    '- NEW RA..--
    '-- Browse for Picture, keep FullPath and FileTitle, and load into Pic Image..

    Private Sub picSubjectItem_Click(sender As Object, _
                                       ev As EventArgs) Handles picSubjectItem.Click

        If (mlRA_id > 0) Then  '-not new-
            Exit Sub
        End If
        If Not mClsAttachments1.OpenFileBrowse(msNewFileFullPath, msNewFileFileTitle, mByteNewFile) Then
            Exit Sub
        End If  '--open-

        '-- load to Pic Image..
        msNewFileFormat = ""
        '- check format..-
        Dim intPos1 As Integer = InStrRev(msNewFileFileTitle, ".")
        Dim s1 As String

        If (intPos1 > 0) Then '--found-
            s1 = Mid(msNewFileFileTitle, intPos1 + 1)
            If (Not mbIsImageFile(s1)) Then  '= And (UCase(s1) <> "PDF") Then
                MsgBox("Invalid File Type (not Image or PDF)..", MsgBoxStyle.Exclamation)
                Exit Sub
            Else
                Dim ms As System.IO.MemoryStream
                ms = New System.IO.MemoryStream(mByteNewFile)
                Dim image1 As Image = System.Drawing.Image.FromStream(ms)
                picSubjectItem.Visible = True
                picSubjectItem.Image = image1
                picSubjectMain.Image = image1
                ms.Close()
            End If
            msNewFileFormat = s1
        Else '--invalid -
            MsgBox("Invalid File Type (no suffix)..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '= mTxtNewFileName.Text = msNewFileFullPath
    End Sub  '-browse-
    '= = = = = = = = = = =  =  = =  =
    '== end Browse --
    '-===FF->

    '-- Progress- Action checkboxes..--
    '-- Progress- Action checkboxes..--

    'UPGRADE_WARNING: Event chkRMARequested.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub chkRMARequested_CheckStateChanged(ByVal eventSender As System.Object, _
                                                    ByVal eventArgs As System.EventArgs) _
                                                        Handles chkRMARequested.CheckStateChanged

        If Not chkRMARequested.Enabled Then Exit Sub '-- Shouldn't need this !!   MUST be vb6 problem..--
        If chkRMARequested.CheckState = 1 Then '--checked..-
            LabDateRMARequested.Text = VB6.Format(CDate(DateTime.Now), "dd-MMM-yyyy hh:mm")
            msActionUpdate = " RA_DateRMA_Requested='" & LabDateRMARequested.Text & "', RA_Status='" & K_STATUS_RMA_REQUESTED & "'"
            cmdSave.Enabled = True
            msInterimStatus = K_STATUS_RMA_REQUESTED
            msInterimRAProgress = "<ul>RMA Requested:" & vbCrLf & LabDateRMARequested.Text & vbCrLf
            cmdCancel.Text = "Cancel" '-- now loaded..-
        Else '--reversed..-
            LabDateRMARequested.Text = ""
            msActionUpdate = ""
            msInterimStatus = ""
            msInterimRAProgress = ""
            cmdSave.Enabled = False
            cmdCancel.Text = "Exit" '-- NOT loaded..-
        End If
    End Sub
    '= = = = = = = = = = = = =

    '--  Request notes..-
    'UPGRADE_WARNING: Event txtRequestNotes.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtRequestNotes_TextChanged(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) Handles txtRequestNotes.TextChanged
        '='=3311.326=  Dim index As Short = txtRequestNotes.GetIndex(eventSender)

        If (Trim(txtRequestNotes.Text) <> "") Then
            cmdSave.Enabled = True
            cmdCancel.Text = "Cancel" '-- now loaded..-
        End If

    End Sub '--notes-
    '= = = = = = = = =

    '-- RMA Request Result..--

    'UPGRADE_WARNING: Event optRMAResult.CheckedChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub optRMAResult_CheckedChanged(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs) Handles OptRMAResult.CheckedChanged
        If eventSender.Checked Then
            Dim index As Short = OptRMAResult.GetIndex(eventSender)

            If Not OptRMAResult(index).Enabled Then Exit Sub '--just in case..-
            If (OptRMAResult(0).Checked = False) And (OptRMAResult(1).Checked = False) Then '--reversed..-
                cmdSave.Enabled = False
                msInterimStatus = ""
                If (Trim(txtRequestNotes.Text) = "") Then cmdCancel.Text = "Exit" '-- NOT loaded..-
            Else '--some result.-
                LabDateRMAResult.Text = VB6.Format(CDate(DateTime.Now), "dd-MMM-yyyy hh:mm")
                cmdCancel.Text = "Cancel" '-- now loaded..-
                If (OptRMAResult(0).Checked = True) Then '--granted..--
                    '--enable RMA txt Entry..--
                    txtRMAReceived.ReadOnly = False
                    msActionUpdate = " RA_DateRMA_Response='" & LabDateRMAResult.Text & "', RA_Status='" & K_STATUS_RMA_RECEIVED & "'"
                    msInterimStatus = K_STATUS_RMA_RECEIVED
                    msInterimRAProgress = "<ul>RMA Granted:" & vbCrLf & LabDateRMAResult.Text & vbCrLf
                    cmdSave.Enabled = True
                    txtRMAReceived.Focus()
                Else '--refused..-
                    msActionUpdate = " RA_DateRMA_Response='" & LabDateRMAResult.Text & "', RA_Status='" & K_STATUS_RMA_REFUSED & "'"
                    cmdSave.Enabled = True
                    msInterimStatus = K_STATUS_RMA_REFUSED
                    msInterimRAProgress = "<ul>RMA REFUSED:" & vbCrLf & LabDateRMAResult.Text & vbCrLf
                End If '--granted..-
            End If
        End If  '-checked-
    End Sub '--received..-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--  goods sent..--

    'UPGRADE_WARNING: Event chkGoodsSent.CheckStateChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub chkGoodsSent_CheckStateChanged(ByVal eventSender As System.Object, _
                                                  ByVal eventArgs As System.EventArgs) Handles chkGoodsSent.CheckStateChanged

        If Not chkGoodsSent.Enabled Then Exit Sub '--just in case..-

        If chkGoodsSent.CheckState = 1 Then '--checked..-

            LabDateGoodsSent.Text = VB6.Format(CDate(DateTime.Now), "dd-MMM-yyyy hh:mm")
            txtCourierBarcode.ReadOnly = False

            msActionUpdate = " RA_DateGoodsSentBack='" & LabDateGoodsSent.Text & "', RA_Status='" & K_STATUS_GOODSSENT & "'"
            msInterimStatus = K_STATUS_GOODSSENT
            msInterimRAProgress = "<ul>Goods Sent:" & vbCrLf & LabDateGoodsSent.Text & vbCrLf
            cmdSave.Enabled = True
            cmdCancel.Text = "Cancel" '-- now loaded..-
            txtCourierBarcode.Focus()
        Else '--reversed..-
            msActionUpdate = ""
            txtCourierBarcode.ReadOnly = True
            txtCourierBarcode.Text = ""
            cmdSave.Enabled = False
            cmdCancel.Text = "Exit" '-- NOT loaded..-
            msInterimStatus = ""
            msInterimRAProgress = ""
            msInterimRAProgress = ""
        End If '--checked.-
    End Sub
    '= = = = = = = = = = = = =

    Private Sub txtCourierBarcode_KeyPress(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                Handles txtCourierBarcode.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--courier..-
    '= = = = = = = = =  =

    '-- Choose goods Result..-

    '=3311.326= Private Sub optGoodsResult_CheckedChanged(ByVal eventSender As System.Object, _
    '=3311.326=                                             ByVal eventArgs As System.EventArgs) Handles OptGoodsResult.CheckedChanged
    '=3311.326=     If eventSender.Checked Then
    '=3311.326= Dim index As Short = OptGoodsResult.GetIndex(eventSender)
    '=3311.326= Dim ix, idx As Integer
    '=3311.326= Dim bAllClear As Boolean

    'If Not OptGoodsResult(index).Enabled Then Exit Sub '--just in case..-

    'bAllClear = True
    'For ix = 0 To k_MAXGOODSRESULT
    '    If (OptGoodsResult(ix).Checked = True) Then
    '        bAllClear = False
    '        Exit For
    '    End If
    'Next ix
    'If bAllClear Then
    '    LabGoodsResult.Text = ""
    '    LabDateGoodsResult.Text = ""
    '    msInterimStatus = ""
    '    msInterimRAProgress = ""
    '    cmdSave.Enabled = False
    '    cmdCancel.Text = "Exit" '-- NOT loaded..-
    'Else '--one is selected..-
    '    For ix = 0 To k_MAXGOODSRESULT
    '        If OptGoodsResult(ix).Checked = True Then '--this is it..-
    '            LabGoodsResult.Visible = True
    '            LabGoodsResult.Text = OptGoodsResult(ix).Text '--get text..
    '            LabGoodsResult.ForeColor = System.Drawing.ColorTranslator.FromOle(mlResultColour(LabGoodsResult.Text))
    '            LabDateGoodsResult.Text = VB6.Format(CDate(DateTime.Now), "dd-mmm-yyyy hh:mm")
    '            msActionUpdate = " RA_ReturnResult='" & msFixSqlStr(VB.Left(LabGoodsResult.Text, 15)) & "', " & _
    '             " RA_DateGoodsReceivedBack='" & LabDateGoodsResult.Text & "', RA_Status='" & K_STATUS_GOODSCOMPLETED & "'"
    '            txtResultComments.ReadOnly = False
    '            cmdSave.Enabled = True
    '            cmdCancel.Text = "Cancel" '-- now loaded..-
    '            Exit For '--done..-
    '        End If
    '    Next ix
    '    msInterimStatus = K_STATUS_GOODSCOMPLETED
    '    msInterimRAProgress = "<ul>Goods Result:" & vbCrLf & LabDateGoodsResult.Text & vbCrLf & LabGoodsResult.Text & vbCrLf
    '    txtResultComments.Focus()
    'End If '--selected.-

    '=3311.326= End If
    '=3311.326= End Sub '-goodsresult.-
    '= = = = = = = = = = = = = =

    '3311.325= -cboGoodsResult_SelectedIndex-

    Private Sub cboGoodsResult_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                   Handles cboGoodsResult.SelectedIndexChanged

        If mbIsInitialising Then Exit Sub

        If (cboGoodsResult.SelectedIndex >= 0) Then  '-selected-
            LabGoodsResult.Visible = True
            LabGoodsResult.Text = cboGoodsResult.SelectedItem   '= OptGoodsResult(ix).Text '--get text..
            LabGoodsResult.ForeColor = System.Drawing.ColorTranslator.FromOle(mlResultColour(LabGoodsResult.Text))
            LabDateGoodsResult.Text = VB6.Format(CDate(DateTime.Now), "dd-mmm-yyyy hh:mm")
            msActionUpdate = " RA_ReturnResult='" & msFixSqlStr(VB.Left(LabGoodsResult.Text, 15)) & "', " & _
             " RA_DateGoodsReceivedBack='" & LabDateGoodsResult.Text & "', RA_Status='" & K_STATUS_GOODSCOMPLETED & "'"
            txtResultComments.ReadOnly = False
            cmdSave.Enabled = True
            cmdCancel.Text = "Cancel" '-- now loaded..-
        End If  '-selected-

    End Sub  '-cboGoodsResult_SelectedIndexChanged=
    '= = = = = = = = = = = = = =  = = =
    '-===FF->

    '=3101.1111= get printer sel..
    '--catch printer combo selections..--
    '-- update settings.-

    Private Sub cboPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboPrinters.SelectedIndexChanged
        '= Dim sName As String
        If (cboPrinters.SelectedIndex >= 0) Then
            msColourPrinterName = cboPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_RA_PrtSettingKey, msColourPrinterName) Then
                MsgBox("Failed to save RA Colour (A4) printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  cboPrinters-
    '= = = = = = = = = = = = =  =

    '--label-

    Private Sub cboItemLabelPrinters_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                            Handles cboItemLabelPrinters.SelectedIndexChanged
        If (cboItemLabelPrinters.SelectedIndex >= 0) Then
            msItemLabelPrinterName = cboItemLabelPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_RA_PrtLabelSettingKey, msItemLabelPrinterName) Then
                MsgBox("Failed to save RA Item-Label printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub  '-cboItemLabelPrinters-
    '= = = = = = = = = = = = = = = = = 

    '-- Print Item (job) Label.--

    Private Sub btnPrintItemLabel_Click(sender As Object, e As EventArgs) _
                                                   Handles btnPrintItemLabel.Click
        Dim ix, lngError As Integer
        Dim sSupplier As String
        '==Dim curGap As Currency, curLabelDepth As Currency  '--in twips..--
        Dim intNoLabels As Short = 0
        '==Dim lngLHS, lngTop As Long
        Dim va1 As Object
        Dim colGoodsList As Collection
        Dim prtDocs1 As New clsPrintRAs

        On Error GoTo cmdPrintLabel_Error
        '==If (mPrtLabel Is Nothing) Then Exit Sub
        If (msItemLabelPrinterName = "") Then Exit Sub

        prtDocs1 = New clsPrintRAs
        '==Set Printer = mPrtLabel
        '== prtDocs1.PrtSelectedPrinter = mPrtLabel
        prtDocs1.PrtSelectedPrinterName = msItemLabelPrinterName
        prtDocs1.ItemBarcodeFontName = msItemLabelBarcodeFontName
        prtDocs1.ItemBarcodeFontSize = mIntItemLabelBarcodeFontSize

        '=sSupplier = IIf(((msCustomerCompany <> "") And (msCustomerCompany <> "--")), msCustomerCompany, msCustomerName)
        sSupplier = Trim(VB.Left(msSupplier, 24)) & " [" & msSupplierBarcode & "]"
        '-- ASSUMING no need to feed to top of label..--
        '==For ix = 1 To lngTopmargin
        '==    Printer.Print ""
        '==Next ix
        '= If (intNoLabels > 0) Then  '=3083=
        intNoLabels = 1
        Call prtDocs1.PrintJobLabels(mlRA_id, intNoLabels, sSupplier, True)
        '= End If
        prtDocs1 = Nothing
        Exit Sub

cmdPrintLabel_Error:
        lngError = Err().Number
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        MsgBox("Runtime Error in cmd Print RA Item Label.." & vbCrLf & _
                     "Error=" & lngError & ": " & ErrorToString(lngError), MsgBoxStyle.Critical)

    End Sub  '-Item Label-
    '= = = = = = = = = = = = = = = =

    '-- Print Shipping Label.--

    Private Sub cmdPrintShippingLabel_Click(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs) Handles cmdPrintShippingLabel.Click

        '== Printer = mPrtColour '-- set main printer--
        Call mbPrintShippingLabel()
    End Sub '--print label..-
    '= = = = = =  = = = = = =

    '-- print form..-

    Private Sub cmdPrintRAForm_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles cmdPrintRAForm.Click

        '== Printer = mPrtColour '-- set main printer--
        Call mlPrintRAForm()
    End Sub '-- print.-
    '= = = = = = = = = = =
    '-===FF->

    '-- RE-OPEN.  SET status back to GRANTED..--
    '-- RE-OPEN.  SET status back to GRANTED..--

    Private Sub cmdReOpen_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles cmdReOpen.Click
        Dim sSql As String
        Dim s1 As String
        Dim sNotes As String
        Dim ix As Integer

        If MsgBox("Are you sure you want to reset this RA Status back to 'GRANTED' ?", _
                       MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            '--  add msg to notes.--
            sNotes = "RE-OPEN: Original Result was: " & LabGoodsResult.Text & " (" & LabDateGoodsResult.Text & ")" & vbCrLf
            '-- Add reversed message..-
            sNotes = sNotes & "RA RE-OPENED by: " & UCase(msStaffName) & "- " & _
                                           VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy") & ".." & vbCrLf
            sSql = "UPDATE  [RAItems] SET "
            sSql = sSql & " RA_DateGoodsReceivedBack=NULL "
            sSql = sSql & ", RA_ReturnResult='' "
            sSql = sSql & ", RA_DateGoodsSentBack=NULL "
            sSql = sSql & ", RA_CourierBarcode='' "
            sSql = sSql & ", RA_Status='" & K_STATUS_RMA_RECEIVED & "'"
            sSql = sSql & ", RA_RMA_RequestNotes=RIGHT(RA_RMA_RequestNotes+'" & msFixSqlStr(sNotes) & "',2039) "
            sSql = sSql & ", RM_StaffNameUpdated='" & msStaffName & "'"
            sSql = sSql & ", RA_DateUpdated=CURRENT_TIMESTAMP "
            sSql = sSql & " WHERE (RA_id=" & CStr(mlRA_id) & ") "
            If Not mbUpdateRARecord(sSql) Then
                Me.Close()
                Exit Sub
            Else '--ok..-
                cmdReOpen.Visible = False '-hide..
                '=3311.326= For ix = 0 To k_MAXGOODSRESULT
                '=3311.326= OptGoodsResult(ix).Enabled = False
                '=3311.326= OptGoodsResult(ix).Visible = True '--RE-appear..-
                '=3311.326= Next ix
                '=3311.326= LabResultHdr.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(OptGoodsResult(0).Top) - 360)
                Call ActivateReActivate() '--  load updated RA record for more updates..--
            End If '--update.-
        End If '--yes..-
    End Sub
    '= = = = = = = = = = = = = =
    '-===FF->

    '--  Create (new) or Save (update) -

    Private Sub cmdSave_Click(ByVal eventSender As System.Object, _
                               ByVal eventArgs As System.EventArgs) Handles cmdSave.Click
        Dim sSql, s1, sSqlRead2 As String
        '== Dim sStatus As String
        '== Dim colRAFields As Collection
        '= Dim lngaffected As Integer
        Dim sErrors As String
        Dim sRMA As String
        Dim sCourier As String
        Dim sComments As String
        Dim rs1 As DataTable  '= ADODB.Recordset
        Dim bShipping, bGoodsWereSent As Boolean
        Dim transaction1 As OleDbTransaction

        bShipping = False
        bGoodsWereSent = False
        If mbUpdating Then '--save update.-
            If (msActionUpdate <> "") Or (chkTfrToStock.Visible And (chkTfrToStock.CheckState <> 0)) Or _
                                (Trim(txtRequestNotes.Text) <> "") Then '--changed..--
                sRMA = Trim(txtRMAReceived.Text)
                '--  Check if required text has been entered..-
                '--   ie.  Supplier-RMA, OR courier Barcode, OR Goods OTHER Comments..--
                '-- Final warning.--
                If MsgBox("Caution..  Update cannot be reversed.. Continue update?", _
                            MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Else '--chickened out..-
                    Exit Sub
                End If '--vbYes..-
                '-- DON'T EXIT SUB if "msActionUpdate" has changed..==
                If (InStr(UCase(msActionUpdate), UCase(K_STATUS_RMA_RECEIVED)) > 0) Then '--must have Suppl. RMA no..--
                    If sRMA = "" Then
                        MsgBox("Suppliers RMA must be entered..", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    '-- ok..  add RMA to update..-
                    bShipping = True '-- to print shipping form..-
                    msActionUpdate = msActionUpdate & ", RA_SupplierRMA_No='" & msFixSqlStr(sRMA) & "', RA_RMA_Granted='Y'"
                    '=== LabSupplierRMA.Caption = sRMA
                    txtSupplierRMA.Text = sRMA
                ElseIf (InStr(UCase(msActionUpdate), UCase(K_STATUS_GOODSSENT)) > 0) Then  '--must have Courier no..--
                    sCourier = VB.Left(Trim(txtCourierBarcode.Text), 31) '--max for barcode..-
                    If sCourier = "" Then
                        MsgBox("Courier Barcode must be entered..", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    '--  ok to update goodsSent..--
                    bGoodsWereSent = True   '- for POS update-
                    msActionUpdate = msActionUpdate & ", RA_CourierBarcode='" & msFixSqlStr(sCourier) & "' "
                    msInterimRAProgress = msInterimRAProgress & "(Courier ID: " & sCourier & vbCrLf
                ElseIf (InStr(UCase(msActionUpdate), UCase(K_STATUS_GOODSCOMPLETED)) > 0) Then  '--can have Comments..--
                    sComments = Trim(txtResultComments.Text)
                    If (sComments = "") And (msSerialNo <> "") Then  '--original has serial.-
                        If InStr(UCase(LabGoodsResult.Text), "REPLACED") > 0 Then '--needs comments (SerialNo.)..-
                            If MsgBox("NB: A Replacement Serial-No. should be entered for this item.." & vbCrLf & vbCrLf & _
                                      "Do you want to enter a serial No. ?", _
                                      MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) = MsgBoxResult.Yes Then
                                Exit Sub
                            End If  '-yes-
                        Else '--optional..--
                        End If '--other..-
                    End If '-comments.-
                    '--save comments.-
                    msActionUpdate = msActionUpdate & ", RA_ReturnResultComment='" & msFixSqlStr(VB.Left(sComments, 63)) & "' "
                    msInterimRAProgress = msInterimRAProgress & "<ul>Comments:" & vbCrLf & txtResultComments.Text
                End If '--status..-
                If Not mbV20_Only Then '--can update request notes..--
                    'If Not txtRequestNotes(0).ReadOnly Then '--rma panel..-
                    '    msCurrentNotes = Trim(txtRequestNotes(0).Text)
                    'Else '--is locked..  must be Goods Panel.-
                    '    msCurrentNotes = Trim(txtRequestNotes(1).Text)
                    'End If
                    msCurrentNotes = Trim(txtRequestNotes.Text)
                    If (msCurrentNotes <> "") Then '--add staff/date..--
                        msCurrentNotes = UCase(msStaffName) & "- " & _
                                            VB6.Format(CDate(DateTime.Today), "dd-mmm-yyyy") & ":-  " & msCurrentNotes & vbCrLf
                        If msActionUpdate <> "" Then
                            msActionUpdate = msActionUpdate & ", "
                        End If
                        msActionUpdate = msActionUpdate & _
                                    " RA_RMA_RequestNotes=RIGHT(RA_RMA_RequestNotes+'" & msFixSqlStr(msCurrentNotes) & "',2039) "
                    End If '--s1--
                End If
                If (msActionUpdate <> "") Then msActionUpdate = msActionUpdate & ", "
                sSql = "UPDATE  [RAItems] SET " & msActionUpdate
                '--change origin to stock if requested..--
                If (chkTfrToStock.Visible And (chkTfrToStock.CheckState <> 0)) Then '--change origin to stock..--
                    sSql = sSql & " RA_Origin='Stock (ex " & VB.Left(msOrigin, 11) & ")',"
                End If
                sSql = sSql & " RM_StaffNameUpdated='" & msStaffName & "'"
                sSql = sSql & ", RA_DateUpdated=CURRENT_TIMESTAMP "
                sSql = sSql & " WHERE (RA_id=" & CStr(mlRA_id) & ") "
                If Not mbUpdateRARecord(sSql, bGoodsWereSent) Then
                    Me.Close()
                    Exit Sub
                End If '--update.-
                '--  print shipping form if needed..--
                msCurrentStatus = msInterimStatus '--final status..-
                msShowRAProgress = msShowRAProgress & msInterimRAProgress '-- to print the latest..-
                Call ActivateReActivate() '--  load updated RA record for more updates..--
            End If '--update.-
        Else '--NEW RA to be created..-
            '-- CREATE..--
            If Not MsgBox("Create new RA record now?'  ", _
                                 MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Exit Sub
            End If

            '-- Insert new RA record..
            '==  -- 3357.0205- 05-Feb-2017-
            '==  PLUS-   mIntSerialAudit_id As Integer = -1
            '==   v3.4.3403.0711 -- 11Jul2017= - FIX UP for release.
            '==         -- For NEW RA's..  Save request notes if any...-
            msCurrentNotes = Trim(txtRequestNotes.Text)

            '---  34 flds inserted in new record..--
            sSql = "INSERT INTO [RAItems] "
            sSql &= " ( RA_Status, RA_SerialNumber, "
            sSql &= " RM_SerialAudit_id, "
            sSql = sSql & " RA_Origin, RA_JobId, RA_CustomerBarcode, RA_RMCustomer_Id, "
            sSql = sSql & "RA_CustomerCompany, RA_CustomerName, RA_CustomerPhone, RA_CustomerMobile, "
            sSql = sSql & "RM_StockId, RM_ItemSupplierCode, "
            sSql = sSql & "RM_ItemBarcode, RM_ItemDescription, RM_ItemCat1, RM_ItemCat2, RM_ItemCat3, "
            sSql = sSql & "RM_Item_Sell_Ex, "
            sSql = sSql & "RM_GoodsId, RM_InvoiceNo, RM_InvoiceDate, RM_GoodsDate, "
            sSql = sSql & "RM_OrderNo, RM_OrderId, RM_SupplierId, RM_SupplierBarcode, RM_Supplier, "
            sSql = sSql & "RM_Supplier_main_phone, RM_Supplier_main_fax, RM_Supplier_main_email, RM_Supplier_AddressInfo, "
            sSql = sSql & "RA_Symptoms, RA_RMA_RequestNotes, "
            sSql = sSql & " RM_StaffIdCreated, RM_StaffNameCreated ) "

            sSql &= " VALUES  ('10-Created', '" & msFixSqlStr(msSerialNo) & "', "
            sSql &= CStr(mIntSerialAudit_id) & ", "
            sSql = sSql & "'" & msOrigin & "', " & CStr(mlJobId) & ", "
            sSql = sSql & " '" & msCustomerBarcode & "', "
            sSql = sSql & CStr(mlCustomerId) & ", "
            sSql = sSql & "'" & msFixSqlStr(msCustomerCompany) & "', '" & msFixSqlStr(msCustomerName) & "', "
            sSql = sSql & "'" & msCustomerPhone & "', '" & msCustomerMobile & "', "
            sSql = sSql & CStr(mlStockId) & ", '" & msFixSqlStr(msItemSupplierCode) & "', "
            sSql = sSql & " '" & msFixSqlStr(msItemBarcode) & "', "
            sSql = sSql & " '" & msFixSqlStr(msDescr) & "', "
            sSql = sSql & " '" & msFixSqlStr(msCat1) & "', "
            sSql = sSql & " '" & msFixSqlStr(msCat2) & "', "
            sSql = sSql & " '" & msFixSqlStr(msCat3) & "', "
            sSql = sSql & VB6.Format(mCurSellEx, "######0.00") & ", "
            sSql = sSql & CStr(mlGoodsId) & ", "
            sSql = sSql & " '" & msFixSqlStr(mColInvoiceFields.Item("Invoice_no")("value")) & "', "
            sSql = sSql & " '" & VB6.Format(CDate(mColInvoiceFields.Item("Invoice_date")("value")), "dd-mmm-yyyy") & "', "
            sSql = sSql & " '" & VB6.Format(CDate(mColInvoiceFields.Item("goods_date")("value")), "dd-mmm-yyyy") & "', "
            sSql = sSql & " '" & msFixSqlStr(mColInvoiceFields.Item("order_no")("value")) & "', "
            sSql = sSql & CStr(CInt(mColInvoiceFields.Item("order_id")("value"))) & ", "
            '--- NOTE: "mColInvoiceFields" does NOT necessarily contain final supplier selected..--
            sSql = sSql & CStr(mlSupplierId) & ", "
            sSql = sSql & " '" & msFixSqlStr(msSupplierBarcode) & "', "
            sSql = sSql & " '" & msFixSqlStr(msSupplier) & "', "
            sSql = sSql & " '" & msFixSqlStr(msSupplierMainPhone) & "', "
            sSql = sSql & " '" & msFixSqlStr(msSupplierMainFax) & "', "
            sSql = sSql & " '" & msFixSqlStr(msSupplierMainEmail) & "', "
            sSql = sSql & " '" & msFixSqlStr(VB.Left(msSupplierAddressInfo, 500)) & "', "
            '= 3357.0206= sSql = sSql & " '" & msFixSqlStr(VB.Left(txtSymptoms.Text, 1000)) & "', "
            sSql = sSql & " '" & msFixSqlStr(VB.Left(txtSymptoms.Text, 511)) & "', "
            '= 3403.711-
            sSql = sSql & " '" & msFixSqlStr(msCurrentNotes) & "', "
            sSql = sSql & CStr(mlStaffId) & ", '" & msFixSqlStr(msStaffName) & "')"

            '== mCnnJobs.BeginTrans()
            Dim intAffected As Integer
            transaction1 = mCnnJobs.BeginTransaction
            If mbExecuteSql(mCnnJobs, sSql, True, transaction1) Then
                '== gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrors) Then '--ok--
                '--get last IDENTITY (allocated order_id)..--
                sSql = "SELECT IDENT_CURRENT ('dbo.RAItems')"
                If Not gbGetSqlScalarIntegerValue_Trans(mCnnJobs, sSql, True, transaction1, mlRA_id) Then
                    '=If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
                    '--MsgBox sErrorMsg, vbCritical
                    '==mCnnJobs.RollbackTrans()
                    transaction1.Rollback()
                    MsgBox("Failed to retrieve ID id of new RA record.." & vbCrLf & sErrors, MsgBoxStyle.Critical)
                    '==bCancelled = True
                    '--Response.End
                Else '--ok--  Have ID..   '=SHOULD be on 1st row...
                    '--  INSERT PICTURE if any into RA Attachments Table..-
                    If (Not (picSubjectItem.Image Is Nothing)) And _
                                      (msNewFileFullPath <> "") And (Not (mByteNewFile Is Nothing)) Then
                        If Not mClsAttachments1.InsertNewAttachment(msNewFileFullPath, msNewFileFileTitle, msNewFileFormat, _
                                                                      mlRA_id, msSupplier, "$$NewRAImage" & vbCrLf, _
                                                                      True, transaction1, intAffected) Then
                            transaction1.Rollback()
                            MsgBox("Failed to INSERT Image for new RA record.." & vbCrLf & _
                                      "This RA is Abandoned.", MsgBoxStyle.Exclamation)
                            cmdSave.Enabled = False
                            cmdCancel.Text = "Exit"
                            Exit Sub
                        Else  '-ok-
                        End If  '-insert-
                    End If  '-nothing-

                    '- Now can commit all of it..
                    transaction1.Commit()
                    LabOurRANumber.Text = VB6.Format(mlRA_id, "  00")
                    '== rs1.Close()
                    MsgBox("OK.. New RA record has been created.." & vbCrLf & _
                                                 "Our Home RA No. is " & mlRA_id, MsgBoxStyle.Information)
                    cmdPrintRAForm.Enabled = True
                    cmdPrintShippingLabel.Enabled = True
                    btnPrintItemLabel.Enabled = True

                    '-- Disable further change.-
                    mbCreated = True
                    txtItemBarcode.ReadOnly = True
                    txtItemSerial.ReadOnly = True
                    txtSymptoms.ReadOnly = True
                    cmdNewSupplier.Enabled = False
                    panelOrigin.Enabled = False  '= grpBoxOrigin.Enabled = False  '=cboOrigin.Enabled = False
                    chkTfrToStock.Enabled = False
                    txtJobNo.Enabled = False
                    txtCustNo.Enabled = False
                    '=End If '--open-
                End If '--get rst--
                msCurrentStatus = "10-Created"
            Else '-insert failed--
                '= mCnnJobs.RollbackTrans()
                transaction1.Rollback()
                MsgBox("Failed to insert new RA record.." & vbCrLf, MsgBoxStyle.Critical)
                '==bCancelled = True
            End If '--update/Create..-
            cmdSave.Enabled = False
            cmdCancel.Text = "Exit"
        End If '--update/new..-
        rs1 = Nothing

    End Sub '-- Save --
    '= = = = = = = = = = =
    '= = = = = = = = = = = = = =
    '-===FF->

    '--Cancel RA completely..--
    '--- UPDATE and Set CANCELLED status.-

    Private Sub cmdCancelRARecord_Click(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs) Handles cmdCancelRARecord.Click
        Dim sMsg As String
        Dim sSql, sErrors As String
        Dim lngaffected As Integer

        sMsg = " Are you sure you want to COMPLETELY CANCEL (forever) this RA Record ?"
        '---If mbAmending Then sMsg = "Abandon this update ?"
        If mlRA_id >= 0 Then '--started
            '-- confirm if job is to be abandoned..--
            If Not MsgBox(sMsg, MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                '===txtRcvdName.SetFocus
                Exit Sub '--was mistake..  keep going..
            Else '--yes--
                '-- Update and set status..-
                sSql = "UPDATE [RAItems] SET RA_Status='" & K_STATUS_RMA_CANCELLED & "' "
                sSql = sSql & ", RA_dateUpdated= CURRENT_TIMESTAMP "
                sSql = sSql & " WHERE (RA_id=" & CStr(mlRA_id) & ") "
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                If Not gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrors) Then
                    '--mCnnJobs.RollbackTrans
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    MsgBox("Failed to Set Cancelled Status on DB RA record.." & vbCrLf & _
                                                                     sErrors & vbCrLf, MsgBoxStyle.Exclamation)
                Else '--ok-
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case --
                    MsgBox("RA No: " & mlRA_id & " has been Cancelled.. " & vbCrLf & _
                                                     "( " & lngaffected & " Record(s) affected..)", MsgBoxStyle.Information)
                End If
                Me.Close() '--Me.Hide
                Exit Sub
            End If
        Else '--not started..-
            '-- then exit..--
            '==Me.Hide
            Exit Sub
        End If
    End Sub '--finish--
    '= = = = = =  = = =
    '-===FF->

    '--Query cancel..
    '----Return TRUE to continue CANCEL..--

    Private Function mbQueryCancel() As Boolean

        mbQueryCancel = True '--assume confirm cancel..-
        If Not mbUpdating Then '--new--
            If cmdCancel.Text = "Exit" Then
                Exit Function '--Me.Hide  '--done..-
            ElseIf cmdSave.Enabled Then  '--stuff entered..-
                If Not MsgBox("Abandon this New RA entry ??", _
                              MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    mbQueryCancel = False '--don't cancel.-
                    Exit Function
                End If
                '====Me.Hide
            End If
        ElseIf (msActionUpdate <> "") Then  '--Updating, changed..-
            If cmdCancel.Text = "Exit" Then
                '==Me.Hide
            ElseIf Not MsgBox("Abandon this update ??", _
                               MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                mbQueryCancel = False '--don't cancel.-
                Exit Function
            Else '--yes.-
                '==Me.Hide
            End If
        Else '--ok-
            '==Me.Hide
        End If

    End Function '-- Cancel--
    '= = = = = = = = = = =

    '-- Cancel/Exit..-

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click
        If mbQueryCancel() Then
            mColInvoiceFields = Nothing
            mbUserCancelled = True
            Me.Close() '==Me.Hide
        End If
    End Sub '--cmdCancel.-
    '= = = = = = = = =  = =
    '= = = = = =  = = =
    '-===FF->

    '- NEW events for Attachments panel..


    '== ALL CONTROL EVENTS are passed to the CLASS for processing.-- 
    '== ALL CONTROL EVENTS are passed to the CLASS for processing.-- 
    '== ALL CONTROL EVENTS are passed to the CLASS for processing.-- 

    '--PREVIEW KeyPress..-
    '-- Catch  ctl-V- to Paste.

    Private Sub frmAttachments_KeyDown(sender As Object, _
                                  eventArgs As System.Windows.Forms.KeyEventArgs) _
                                      Handles MyBase.KeyDown
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.frmAttachments_KeyDown(sender, eventArgs)
        Exit Sub

    End Sub  '-- keydown-
    '= = = = = = = = = = = =
    '-===FF->

    '--=3119.1222=  PAST-FILE Context menu stuff--
    '--=3119.1222=  PAST-FILE Context menu stuff--

    '-- menu click-

    Public Sub mnuPasteFileName_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) Handles mnuPasteFileName.Click
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.mnuPasteFileName_Click(eventSender, eventArgs)
        Exit Sub

    End Sub  '-mnuPasteFileName- click-
    '= = = = = = = = = = = = = = = =  = = = = = = =
    '-===FF->

    '-  MOUSE ACTION- txtNewFileName-

    '-- HANDLED HERE !!!
    '-- HANDLED HERE !!!
    '-- HANDLED HERE !!!

    Private Sub txtNewFileName_MouseUp(sender As Object, _
                                         ev As MouseEventArgs) Handles txtNewFileName.MouseUp

        '== Dim sFileFullpath, sFileTitle, sFormat As String
        If mbIsInitialising Then Exit Sub
        Dim data_object As Object = Clipboard.GetDataObject()

        ' If the right mouse button was clicked and released, 
        ' display the shortcut menu assigned to the txt.  
        If ev.Button = MouseButtons.Right Then
            '== If mbGetFileFromClipboard(sFileFullpath, sFileTitle, sFormat) Then
            If (data_object.GetDataPresent(DataFormats.FileDrop)) Then
                mnuPasteFileName.Enabled = True
            Else  '-nothing on clipboard-
                mnuPasteFileName.Enabled = False
            End If '-get
            '--show menu.. user must ckick.. 
            mContextMenuPasteFileName.Show(txtNewFileName, New Point(ev.X, ev.Y))
        End If
    End Sub  '-txtNewFile mouse up-
    '= = = = = = = = = == = = = = = = = = 
    '-===FF->

    '-- A d d Attachment--
    '-- A d d Attachment--

    Private Sub btnBrowse_Click(sender As Object, _
                                ev As EventArgs) Handles btnBrowse.Click

        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnBrowse_Click(sender, ev)
        Exit Sub

    End Sub  '-browse.-
    '= = = = = = = = = = = = 

    Private Sub txtNewComment_TextChanged(sender As Object, _
                                              ev As EventArgs) Handles txtNewComment.TextChanged
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.txtNewComment_TextChanged(sender, ev)
        Exit Sub

    End Sub  '--new comment-
    '= = = = = = = = = = == = = 

    '-- save new File to DB..

    Private Sub btnSaveAttachment_Click(sender As Object, ev As EventArgs) Handles btnSaveAttachment.Click
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnSaveAttachment_Click(sender, ev)
        Exit Sub


    End Sub  'save new-
    '= = = = = = = = = =
    '-===FF->

    '-- view current doc..
    '-- Doc has been selected from listView..

    Private Sub btnViewDoc_Click(sender As Object, ev As EventArgs) Handles btnViewDoc.Click
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnViewDoc_Click(sender, ev)
        Exit Sub

    End Sub  '--  view -
    '= = = = = = = = = = = = = = = = =

    '-- Delete -

    Private Sub btnDelete_Click(sender As Object, ev As EventArgs) Handles btnDelete.Click

        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.btnDelete_Click(sender, ev)
        Exit Sub

    End Sub  '--delete-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- list vew=  selection changed..--

    Private Sub lvwDocs_SelectedIndexChanged(sender As Object, _
                                              ev As EventArgs) Handles lvwDocs.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.lvwDocs_SelectedIndexChanged(sender, ev)
        Exit Sub

    End Sub '-SelectedIndexChanged-
    '= = = = = = = = = = = = = = = 

    '--listViewDocs_DblClick--

    Private Sub lvwDocs_DblClick(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles lvwDocs.DoubleClick

        If mbIsInitialising Then Exit Sub
        Call mClsAttachments1.lvwDocs_DblClick(eventSender, eventArgs)
        Exit Sub

    End Sub '--listView_dblClick--
    '= = = = = = = = =
    '= = = = = =  = = =
    '-===FF->

    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    Private Sub frmNewRA_FormClosing(ByVal eventSender As System.Object, _
    ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

        'UPGRADE_ISSUE: Constant vbFormCode was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                       System.Windows.Forms.CloseReason.TaskManagerClosing, _
                                         System.Windows.Forms.CloseReason.FormOwnerClosing  '==, vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                If mbUserCancelled Then
                    intCancel = 0 '--confirmed. let it go--
                ElseIf mbQueryCancel() Then
                    intCancel = 0 '--confirmed. let it go--
                Else '--don't cancel..-..-
                    intCancel = 1 '--cant close yet--'--was mistake..  keep going..
                End If
            Case Else
                intCancel = 0 '--else let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--unload--
    '= = = = = = = = = =

End Class  '--form-
'= = = = = = = = = = = = = = = =  = =

' Implements the manual sorting of items by columns.
Class ListViewItemComparer_RAs
    Implements IComparer
    Private col As Integer
    Private order As SortOrder

    Public Sub New()
        col = 0
        order = SortOrder.Ascending
    End Sub

    Public Sub New(ByVal column As Integer, ByVal order As SortOrder)
        col = column
        Me.order = order
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
                        Implements System.Collections.IComparer.Compare
        Dim returnVal As Integer = -1

        Try
            returnVal = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, _
                                                     CType(y, ListViewItem).SubItems(col).Text)
        Catch ex As Exception
            MsgBox("Error sorting Invoices Listview.." & vbCrLf & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
        End Try

        '-- Determine whether the sort order is descending.
        If order = SortOrder.Descending Then
            ' Invert the value returned by String.Compare.
            returnVal *= -1
        End If

        Return returnVal
    End Function
End Class
'==  the end (2)..==
'== end RA form.====
'== end RA form.====
