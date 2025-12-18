
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports system.data.sqlclient
Imports System.Data.OleDb
Imports System.Math
Imports System.ComponentModel

Public Class frmCreditNotesReport

    '==   
    '==     v3.3.3307.021..  21-Feb-2017= ===
    '==       >> Credit Notes Report-.. 
    '==             Moved to this new form form Admin Form
    '==..
    '==    -- 4201.0618/0623.  11/18-June-2019-   
    '==         --  clsReportPrinter-  Fix For preview- make starting zoom to 90%... 
    '==         --  Credit Notes History Report-  Tidy up... 
    '==
    '== = = = = = = = = = = = = = =  ==  = = = = = = = = = = = = = = =  = ==  == = 
    '==
    '== = = = = = = = = = = = = = =  ==  = = = = = = = = = = = = = = =  = ==  == = 
    '==
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==
    '==   POS- Reversals to Payments that included Saved Credits Notes-   
    '==        -- Credit note amounts are being treated As credits To Customer instead Of showing As debit (reversed).  
    '==          See "gbGetCreditNoteHistory" CreditNote Report, 
    '==               And CreditNote balance On Sale Screen. ..
    '==          IF Payment record is a REVERSAL, then "creditNotePaymentCredited" and "creditNoteAmountDebited" need reversing.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = = = = == 

    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==
    '==  A.  New Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
    '==         Done by cloning clsAccountReversal and moddying and adding Payment Reversal stuff..
    '==     -- ALSO-  Needs new button added to frmShowInvoice.
    '==     --  ALSO Fix formatting in frmCrediNotesReport.
    '==
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    Private Const k_reportPrtSettingKey As String = "POS_ReportPrinter"

    Private mbIsInitialising As Boolean = True
    Private mbFormLoading As Boolean = False
    Private mbActivated As Boolean = False

    Private mFrmParent As Form   '-- for locating Me..

    Private msServer As String = ""
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""
    Private msSqlVersion As String = ""
    Private mlngSqlMajorVersion As Integer = 0

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    Private mbIsSqlAdmin As Boolean = False

    Private msVersionPOS As String = ""

    Private msComputerName As String '--local machine--
    Private msAppPath As String
    '= Private msLastSqlErrorMessage As String = ""

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    '= Private mlJobId As Integer = -1

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    '=3301.428= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private msDefaultPrinterName As String = ""
    Private msReportprinterName As String = ""
    Private mImageUserLogo As Image

    Private mColPrefsCustomer As Collection

    Private msBusinessABN As String = ""
    Private msBusinessName As String = ""
    '= = = = = = = = = = = = = = = = = = = = = = =

    '--Constructor-
    '--Constructor-

    Public Sub New(ByVal sServer As String, _
                      ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                       ByRef colSqlDBInfo As Collection, _
                       ByVal sVersionPOS As String, _
                       ByRef imageUserLogo As Image, _
                        ByVal intStaff_id As Integer, _
                         ByVal sStaffName As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        '==mFrmParent = frmParent
        msServer = sServer
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo
        msVersionPOS = sVersionPOS
        mImageUserLogo = imageUserLogo

        mIntStaff_id = intStaff_id
        msStaffName = sStaffName

        msComputerName = My.Computer.Name

    End Sub  '--new --
    '= = =  = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = =
    '-===FF->
    '-- Browse Selected table using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseTable(ByRef colPrefs As Collection, _
                                    ByRef sTitle As String, _
                                      ByRef sWhere As String, _
                                      ByRef colKeys As Collection, _
                                      ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Customer", _
                                         Optional ByVal bLookupOnly As Boolean = False) As Boolean
        Dim frmBrowse1 As New frmBrowsePOS

        mbBrowseTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        '=3101.1207== add staff.-
        frmBrowse1.StaffId = mIntStaff_id
        frmBrowse1.StaffName = msStaffName
        frmBrowse1.versionPOS = msVersionPOS
        If bLookupOnly Then
            frmBrowse1.HideEditButtons = True
            frmBrowse1.lookupSelection = True
        Else '- can edit records-
            frmBrowse1.HideEditButtons = False
            frmBrowse1.lookupSelection = False
        End If
        '--go-
        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()
    End Function '--browse.--
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--  Load-

    Private Sub frmCreditNotesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim pd1 As New PrintDocument()
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim sName, s1 As String

        Call CenterForm(Me)
        Me.Text = "Credit Notes History."

        '-- get system Info table data.-
        '=3301.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '=3301.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        msBusinessName = mSysInfo1.item("BUSINESSNAME")

        '-- Customer --
        mColPrefsCustomer = New Collection
        mColPrefsCustomer.Add("barcode")
        mColPrefsCustomer.Add("lastname")
        mColPrefsCustomer.Add("firstname")
        mColPrefsCustomer.Add("companyName")
        mColPrefsCustomer.Add("phone")
        mColPrefsCustomer.Add("mobile")
        mColPrefsCustomer.Add("isAccountCust")
        mColPrefsCustomer.Add("creditLimit")
        mColPrefsCustomer.Add("pricingGrade")
        mColPrefsCustomer.Add("openedStaff_id")
        mColPrefsCustomer.Add("openedStaffname")
        mColPrefsCustomer.Add("inactive")
        mColPrefsCustomer.Add("address")
        '==mColPrefsCustomer.Add("addr2")
        '=mColPrefsCustomer.Add("addr3")
        mColPrefsCustomer.Add("suburb")
        mColPrefsCustomer.Add("email")
        mColPrefsCustomer.Add("customer_id")
        mColPrefsCustomer.Add("date_modified")
        mColPrefsCustomer.Add("comments")

        msDefaultPrinterName = ""  '== Printer.DeviceName '--  save name of original default printer..-
        cboReportPrinters.Items.Clear()
        '= cboReceiptPrinters.Items.Clear()
        '=3300.428= Call mbLoadSettings()
        msSettingsPath = gsLocalSettingsPath() '= "JobMatix33" default.
        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboReportPrinters.Items.Add(sName)
                '= cboReceiptPrinters.Items.Add(sName)
            Next sName
            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_reportPrtSettingKey) AndAlso _
                         mLocalSettings1.item(k_reportPrtSettingKey) <> "" Then
                '== gbQueryLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, s1) AndAlso (s1 <> "") Then
                s1 = mLocalSettings1.item(k_reportPrtSettingKey)
                If colPrinters.Contains(s1) Then '--set it- 
                    cboReportPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboReportPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboReportPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            ''-receipt-
            'If mLocalSettings1.exists(k_receiptPrtSettingKey) AndAlso _
            '        (mLocalSettings1.item(k_receiptPrtSettingKey) <> "") Then
            '    s1 = mLocalSettings1.item(k_receiptPrtSettingKey)
            '    '= gbQueryLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, s1) AndAlso (s1 <> "") Then
            '    If colPrinters.Contains(s1) Then '--set it-
            '        cboReceiptPrinters.SelectedItem = s1
            '    Else
            '        If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
            '    End If
            'Else '-no pref-
            '    If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
            'End If  '-query receipt prt.--

        End If '-getAvail.-  
        msReportprinterName = cboReportPrinters.SelectedItem
        cboReportPrinters.Enabled = True

        mIntCreditNotesCustomer_id = -1
        labCreditNotesCustName.Text = ""

        mbIsInitialising = False

    End Sub  '-load-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--  B E G I N  Credit Notes Reporting Stuff-
    '--  B E G I N  Credit Notes Reporting Stuff-
    '--  B E G I N  Credit Notes Reporting Stuff-

    '-  define tab columns- (ofsets from our marg)-
    Const k_TAB_CRN_CUST As Integer = 0
    Const k_TAB_CRN_DATE As Integer = 70  '=-Build-4282 was 140
    Const k_TAB_CRN_DESCR As Integer = 150  '--Build-4282  was 220
    Const k_TAB_CRN_TYPE As Integer = 330
    Const k_TAB_CRN_AMOUNT_CR As Integer = 400
    Const k_TAB_CRN_AMOUNT_DR As Integer = 480
    Const k_TAB_CRN_AMOUNT_BAL As Integer = 560
    '=Const k_WIDTH_DESCR As Short = 24
    Const k_WIDTH_CRN_AMT As Short = 80
    '= Const k_TAB_TRANCODE As Integer = 420
    Const k_TAB_CRN_STAFF As Integer = 670

    '-mShowCreditNoteTotals-
    '-  Return Totals line ready to print.

    Private Function mShowCreditNoteTotals(ByVal sText As String, _
                                      ByVal decCredits As Decimal, _
                                      ByVal decDebits As Decimal) As String
        Dim s1, sLine As String
        Dim sAmount As String

        sLine = "<textline>"
        sLine &= "<txt TAB=""" & k_TAB_CRN_CUST & """  fontstyle=""bold""  >" & sText & "</txt>"
        sLine &= "<txt TAB=""" & k_TAB_CRN_TYPE & """  >Cr/Dr:</txt>"
        sAmount = FormatCurrency(decCredits, 2)
        sLine &= "<txt TAB=""" & k_TAB_CRN_AMOUNT_CR & """  align=""right""  fontstyle=""bold""  "
        sLine &= " width = """ & k_WIDTH_CRN_AMT & """  >" & sAmount & "</txt>"
        sAmount = FormatCurrency(decDebits, 2)
        sLine &= "<txt TAB=""" & k_TAB_CRN_AMOUNT_DR & """  align=""right""  fontstyle=""bold""  "
        sLine &= " width = """ & k_WIDTH_CRN_AMT & """  >" & sAmount & "</txt>"
        '- compute balance-
        s1 = IIf((decCredits >= decDebits), " Cr", " Dr")
        sAmount = FormatCurrency(Abs(decCredits - decDebits), 2)
        sLine &= "<txt TAB=""" & k_TAB_CRN_AMOUNT_BAL & """  align=""right""  fontstyle=""bold""  "
        sLine &= " width = """ & k_WIDTH_CRN_AMT & """  >" & sAmount & s1 & "</txt>"
        sLine &= "</textline>"
        '== colReportLines.Add(sLine)
        '= s1 = "<drawline fontstyle=""bold"" />"

        mShowCreditNoteTotals = sLine
    End Function  '-mShowCreditNoteTotals()-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Show Credit Notes Report-
    '--  Show Credit Notes Report-
    '--  Show Credit Notes Report-
    '==
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==
    '==  (a) POS- Reversals to Payments that included Saved Credits Notes-   
    '==        -- Credit note amounts are being treated As credits To Customer instead Of showing As debit (reversed).  
    '==          See "gbGetCreditNoteHistory" CreditNote Report, 
    '==               And CreditNote balance On Sale Screen. ..
    '==          IF Payment record is a REVERSAL, then "creditNotePaymentCredited" and "creditNoteAmountDebited" need reversing.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = = = = == 


    Private mIntCreditNotesCustomer_id As Integer = -1

    '- get all credit notes.. (as selected).

    Private Sub btnCreditNotesReport_Click(sender As Object, e As EventArgs) _
                                                  Handles btnCreditNotesReport.Click
        '=ByVal intCustomer_id As Integer, _
        Dim dtCreditNotes As DataTable
        Dim decCredit, decCustCredits As Decimal
        Dim decDebit, decCustDebits As Decimal
        Dim decTotalCredits, decTotalDebits As Decimal
        Dim decCreditNoteCreditRemaining As Decimal
        Dim colReportLines As Collection   '- For all--
        Dim colThisCustLines As Collection  '--accum cust first-
        Dim clsReportPrint1 As clsReportPrinter
        Dim sCustomer, sLastCustomer, sLine As String
        Dim s1, sDate, sDescr, sAmount, sStaff As String

        '==   Target-New-Build-4277 -- (Started 07-October-2020)
        Dim bIsReversal As Boolean
        '==  END Target-New-Build-4277 -- (Started 07-October-2020)

        '- get all credit notes.. (as selected).

        If Not gbGetCreditNoteHistory(mCnnSql, mIntCreditNotesCustomer_id, dtCreditNotes, _
                                             decTotalCredits, decTotalDebits, decCreditNoteCreditRemaining) Then
            MsgBox("Failed Looking up credit notes.. ", MsgBoxStyle.Exclamation)
            Exit Sub
        Else  '-ok-
            If (dtCreditNotes IsNot Nothing) AndAlso (dtCreditNotes.Rows.Count > 0) Then
                Dim intTrCount As Integer = dtCreditNotes.Rows.Count

                '= MsgBox("Found " & intTrCount & " credit note transactionss..", MsgBoxStyle.Information)

                clsReportPrint1 = New clsReportPrinter
                colReportLines = New Collection
                colThisCustLines = New Collection
                sLastCustomer = ""
                '-- recalc finals as only some may get selected.
                decTotalCredits = 0
                decTotalDebits = 0

                '-- make report--
                '-- show all in cust groups..

                '--have credit note history..
                For Each row1 As DataRow In dtCreditNotes.Rows
                    sCustomer = Trim(row1.Item("customerName"))
                    If (LCase(sCustomer) <> LCase(sLastCustomer)) Then
                        '-new or first customer-
                        If (sLastCustomer = "") Then  '- previous-
                            '-- First trans. of report-
                        Else '--just passed a completed customer-
                            '- print cust transactions and totals..
                            '-- for sLastCustomer - 
                            '--    (only if has outstanding, or we are printing all.)
                            '-- 
                            If (Not chkCreditNotesOutstOnly.Checked) OrElse (decCustCredits <> decCustDebits) Then
                                '-- print name line-
                                sLine = "<textline>"
                                sLine &= "<txt TAB=""" & k_TAB_CRN_CUST & """  >" & sLastCustomer & "</txt>"
                                sLine &= "</textline>"
                                colReportLines.Add(sLine)
                                '- print trans. lines
                                For Each sCustLine As String In colThisCustLines
                                    colReportLines.Add(sCustLine)
                                Next
                                sLine = mShowCreditNoteTotals("Totals for : " & sLastCustomer, decCustCredits, decCustDebits)
                                colReportLines.Add(sLine)
                                s1 = "<drawline fontstyle=""bold"" />"
                                colReportLines.Add(s1)
                                '-- add to final  totals-
                                decTotalCredits += decCustCredits
                                decTotalDebits += decCustDebits
                            End If  '-this cust-
                            '-- done with last cust.-
                        End If '-last cust-.
                        '--new  customer-
                        'sLine = "<textline>"
                        'sLine &= "<txt TAB=""" & k_TAB_CRN_CUST & """  >" & sCustomer & "</txt>"
                        'sLine &= "</textline>"
                        'colReportLines.Add(sLine)
                        sLastCustomer = sCustomer
                        '- reset totals.
                        decCustCredits = 0
                        decCustDebits = 0
                        colThisCustLines = New Collection
                    End If  '-changed cust.
                    '-- get details this transaction.
                    sDate = Format(CDate(row1.Item("payment_date")), "dd-MMM-yyyy")
                    sDescr = row1.Item("transactionType")
                    sDescr &= "; Inv # " & row1.Item("invoice_id")
                    sStaff = row1.Item("docket_name")

                    '- Print the cr-note Detail Line. Include Trancode if any.
                    sLine = "<textline>"
                    '- Cust & Desc Amt for each Detail-
                    sLine &= "<txt TAB=""" & k_TAB_CRN_DATE & """  >" & sDate & "</txt>"
                    sLine &= "<txt TAB=""" & k_TAB_CRN_DESCR & """  >" & sDescr & "</txt>"

                    '==   Target-New-Build-4277 -- (Started 07-October-2020)
                    bIsReversal = (row1.Item("isReversal") <> 0)
                    '== END  Target-New-Build-4277 -- (Started 07-October-2020)


                    If (CDec(row1.Item("creditNotePaymentCredited")) <> 0) Then
                        decCredit = CDec(row1.Item("creditNotePaymentCredited"))
                        '-- format credit line..

                        '==   Target-New-Build-4277 -- (Started 07-October-2020)
                        '== ---- decCustCredits += decCredit
                        If bIsReversal Then
                            decCustCredits -= Math.Round(decCredit, 2, MidpointRounding.AwayFromZero)
                            decCredit = -decCredit
                        Else  '-not-
                            decCustCredits += Math.Round(decCredit, 2, MidpointRounding.AwayFromZero)
                        End If
                        '== END  Target-New-Build-4277 -- (Started 07-October-2020)


                        sLine &= "<txt TAB=""" & k_TAB_CRN_TYPE & """  >Paymt.Credit:</txt>"
                        sAmount = FormatCurrency(decCredit, 2)
                        sLine &= "<txt TAB=""" & k_TAB_CRN_AMOUNT_CR & """  align=""right""  "
                        sLine &= " width = """ & k_WIDTH_CRN_AMT & """  >" & sAmount & "</txt>"
                    ElseIf (CDec(row1.Item("refundAsCreditNoteCredited")) <> 0) Then
                        decCredit = CDec(row1.Item("refundAsCreditNoteCredited"))
                        '-- format credit line..
                        decCustCredits += decCredit
                        sLine &= "<txt TAB=""" & k_TAB_CRN_TYPE & """  >Refund-Credit:</txt>"
                        sAmount = FormatCurrency(decCredit, 2)
                        sLine &= "<txt TAB=""" & k_TAB_CRN_AMOUNT_CR & """  align=""right""  "
                        sLine &= " width = """ & k_WIDTH_CRN_AMT & """  >" & sAmount & "</txt>"
                    ElseIf (CDec(row1.Item("creditNoteAmountDebited")) <> 0) Then
                        decDebit = CDec(row1.Item("creditNoteAmountDebited"))

                        '==   Target-New-Build-4277 -- (Started 07-October-2020)
                        '==  ----   decCustDebits += decDebit
                        If bIsReversal Then
                            decDebit = -decDebit
                        Else  '-not reversal-
                        End If
                        decCustDebits += Math.Round(decDebit, 2, MidpointRounding.AwayFromZero)
                        '== END  Target-New-Build-4277 -- (Started 07-October-2020)


                        '-- form debit line-
                        sLine &= "<txt TAB=""" & k_TAB_CRN_TYPE & """  >Debit:</txt>"
                        sAmount = FormatCurrency(decDebit, 2)
                        sLine &= "<txt TAB=""" & k_TAB_CRN_AMOUNT_DR & """  align=""right""  "
                        sLine &= " width = """ & k_WIDTH_CRN_AMT & """  >" & sAmount & "</txt>"
                    Else
                        '-- null line ??
                    End If
                    '-- add staff-
                    sLine &= "<txt TAB=""" & k_TAB_CRN_STAFF & """  >" & sStaff & "</txt>"
                    sLine &= "</textline>"
                    '== colReportLines.Add(sLine)
                    '-- JUST accumulate cust lines until end of customer..
                    colThisCustLines.Add(sLine)
                Next  '--row1-

                '-- all rows seen..
                '--  Show totals for last cust.
                '-- for Latest Customer -
                If (Not chkCreditNotesOutstOnly.Checked) OrElse (decCustCredits <> decCustDebits) Then
                    '-- print name line-
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_CRN_CUST & """  >" & sCustomer & "</txt>"
                    sLine &= "</textline>"
                    colReportLines.Add(sLine)
                    '- print trans. lines
                    For Each sCustLine As String In colThisCustLines
                        colReportLines.Add(sCustLine)
                    Next
                    sLine = mShowCreditNoteTotals("Totals for : " & sCustomer, decCustCredits, decCustDebits)
                    colReportLines.Add(sLine)
                    s1 = "<drawline fontstyle=""bold"" />"
                    colReportLines.Add(s1)
                    decTotalCredits += decCustCredits
                    decTotalDebits += decCustDebits
                End If
                '-- done with last cust.-
                decCreditNoteCreditRemaining = decTotalCredits - decTotalDebits

                '-- Print final totals.. 
                sLine = mShowCreditNoteTotals("Final Totals- All Customers: ", decTotalCredits, decTotalDebits)
                colReportLines.Add(sLine)
                s1 = "<drawline fontstyle=""bold"" />"
                colReportLines.Add(s1)

                s1 = "<drawline fontstyle=""bold"" />"
                colReportLines.Add(s1)

                '-- Print report-
                Dim bPreviewOnly As Boolean = True
                '-- TEMP=
                '= Dim sReportPrinterName As String = "ADOBE PDF"
                Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS,
                                          colReportLines, msReportprinterName,
                                         msBusinessName, "CREDIT NOTES History Report-",
                                         Color.DarkBlue,
                                        "All Credit Note History: " & vbCrLf & "By Customer",
                                        " and individual transactions.. ",
                                          "  Customer   " & Space(14) & "   Date  " &
                                                Space(20) & "  Description" & Space(48) &
                                                "   Amount Cr - Dr " & Space(15) & "     Balance           Staff  ")
            Else
                MsgBox("No credit note transaction found..", MsgBoxStyle.Information)
            End If  '-dt nothing-

        End If  '--get-
    End Sub  '--credit notes-
    '= = = = = = = =  = = = = = = =
    '-===FF->

    '--btnCreditNotesCustLookup-
    '-- Lookup/Choose Customer-

    Private Sub btnCreditNotesCustLookup_Click(sender As Object, e As EventArgs) _
                                                    Handles btnCreditNotesCustLookup.Click
        Dim colPrefs, colKeys As Collection
        Dim colSelectedRow As Collection
        Dim intCustomer_id As Integer
        Dim sCustomer As String
        Dim sName As String

        intCustomer_id = -1
        mIntCreditNotesCustomer_id = -1
        sCustomer = ""
        '=3301.816=
        colPrefs = mColPrefsCustomer   '== New Collection

        If Not mbBrowseTable(colPrefs, "Lookup Customer", "", colKeys, colSelectedRow, "customer", True) Then
            MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
        Else '-ok-
            If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                '== MsgBox("Selected : " & colKeys(1))
                intCustomer_id = CInt(colKeys(1))  '--save fkey as data..
                mIntCreditNotesCustomer_id = intCustomer_id
                sName = colSelectedRow.Item("companyName")("value")
                If (sName = "") Then
                    sName = colSelectedRow.Item("lastName")("value") & ", " & colSelectedRow.Item("firstName")("value")
                End If
                labCreditNotesCustName.Text = sName
                chkCreditNotesAllCust.Checked = False
            End If  '--keys- 
        End If  '--browse-

    End Sub  '-- btnCreditNotesCustLookup-
    '= = = = = = = == = = = = = = = =  ==
    '-===FF->

    '-chkCreditNotesAllCust-

    Private Sub chkCreditNotesAllCust_CheckedChanged(sender As Object, e As EventArgs) _
                                                  Handles chkCreditNotesAllCust.CheckedChanged

        If chkCreditNotesAllCust.Checked Then
            labCreditNotesCustName.Text = ""
            mIntCreditNotesCustomer_id = -1
        End If

    End Sub  '-chkCreditNotesAllCust-
    '= = = = = = = = = = = = = =  = = =

    Private Sub cboReportPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles cboReportPrinters.SelectedIndexChanged

        If (cboReportPrinters.SelectedIndex >= 0) Then
            msReportPrinterName = cboReportPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_reportPrtSettingKey, msReportPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, msReportPrinterName) Then
                MsgBox("Failed to save report printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-

    End Sub  '- selected index-
    '= = = = = = = = = = = = = = =


    '--  E N D  Credit Notes Reporting Stuff-
    '--  E N D  Credit Notes Reporting Stuff-
    '--  E N D  Credit Notes Reporting Stuff-

End Class  '-frmCreditNotesReport-

'==== end form =