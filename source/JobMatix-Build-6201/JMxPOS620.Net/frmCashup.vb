
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
Imports System.ComponentModel

Public Class frmCashup

    '-- Created 29-Nov-2016--
    '-  POS 3.3.3301.1129--

    '==
    '==     v3.3.3301.1212..  12-Dec-2016= ===
    '==       >> Introducing CashDrawer ID's (as per MYOB RM.).- 
    '==       >> Every W/s running POS must have a current CashDrawer ID. ("A".."Z".).- 
    '==       >> Any given CashDrawer ID can be used by several W/s at a time..- 
    '==       >> CashDrawerId/Computer (w/s) assignments are kept in SystemInfo Table-     
    '==       >> CashDrawer ID of W/s ("A".."Z".) is recorded in every invoice and Payment Record.
    '==                  as well as in CashUp Sessions.
    '==       >> Till Balances and Cashup Sessions are on the basis of CashDrawer ID. (NOT terminal_id).
    '==  
    '==    v.3401.0414- 14Apr2017=
    '==          >> Fixes to INPUT the correct (Client) ComputerName in case of THIN CLIENT.-..-
    '==
    '= ==  3403.711- 11July2017=
    '==       -- Add code to Cancel button.
    '==
    '= ==  3501.1030- 30Oct2018=
    '==       -- Show sstaff name..
    '==
    '==  IN PRODUCTION- 07-Feb-2019--
    '==  IN PRODUCTION- 07-Feb-2019--
    '==
    '==   Updated.- 3519.0208 08-Feb-2019= 
    '==     -- Fixes to Cashup Sessions to overcome zero payment is's in payment-no cols..-
    '==
    '==   Updated.- 3519.0217 17-Feb-2019= 
    '==     -- Fixes to Cashup/Paynents Analysis to rename current summary to "Revenue"-
    '==       and to build new summary for actual Till analysis..
    '==       and to drop "Discount on Payment" from all analyses as it is irrelevant in this context
    '==
    '==
    '==   Updated.- 3519.0304  Started 02-March-2019= 
    '==      -- catch Ctl-Z for open-Till function..
    '==
    '==  NEW VERSION..
    '==
    '==    -- 4201.0531.  fix Cashup Session History to show comments in main Comments Textbox.... 
    '==
    '==
    '== NEW revision-
    '==    -- 4201.0708.  Started 05-July-2019-
    '==       -- Add file->Local Preference for LOCAL Re-ordering of payment Details.
    '==       -- Payments and Sales-  Use clsPaymentTypes to get PaymentDetails for Grid. .
    '==
    '= = = = = = = = = = = = = = = =  = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==
    ''== 4251- Fixed- Negative payments (ie more Refund ) were not being written out to Shortages.
    '==
    '= = = = = = = = = = = = = = = =  = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    Private Const k_reportPrtSettingKey As String = "POS_ReportPrinter"
    Private Const k_receiptPrtSettingKey As String = "POS_ReceiptPrinter"

    '-- PAYMENTS DataGridView columns.--
    Private Const k_PAYGRIDCOL_PAYMENTTYPE_DESCR As Short = 0
    Private Const k_PAYGRIDCOL_AMOUNT_REPORTED As Short = 1
    Private Const k_PAYGRIDCOL_AMOUNT_COUNTED As Short = 2
    Private Const k_PAYGRIDCOL_AMOUNT_DIFFERENCE As Short = 3
    Private Const k_PAYGRIDCOL_PAYMENTTYPE_ID As Short = 4
    Private Const k_PAYGRIDCOL_VALIDATED As Short = 5

    '-- grh JobMatixPOS31  v3.1.3101.1007 -
    Private mbIsInitialising As Boolean = True

    Private mFrmParent As Form
    Private mbActivated As Boolean = False   '-to activate once only.-

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
    '== Private msAppPath As String
    '= Private msLastSqlErrorMessage As String = ""

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    '= Private mlJobId As Integer = -1
    Private mColPrefsCustomer As Collection
    Private mImageUserLogo As Image

    Private msBusinessName As String = "Test Business Name"
    Private msBusinessABN As String = ""
    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    '=3301.428= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..

    Private msReportPrinterName As String = ""
    Private msReceiptPrinterName As String = ""
    '== Private msLabelPrinterName As String = ""
    Private msDefaultPrinterName As String = ""
    Private msPdfPrinterName As String = ""

    '== Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    'Private mDecBalanceOwing As Decimal
    'Private mDecPaymentTotalRcvd As Decimal
    'Private mDecPaymentCashRcvd As Decimal
    'Private mDecChange As Decimal
    'Private mDecPaymentNettCredited As Decimal
    'Private mIntCurrentPaymentTypeIndex As Integer = -1

    Private mColCashDrawers As Collection
    Private mListTills As List(Of String)
    Private msCurrentAssignedTill As String = ""

    Private mColLastSessions As Collection
    Private mIntFirstPayment_id, mIntLastPayment_id As Integer

    ''- Last Till Results of Analysis
    'Private mColPayments As Collection  '-All payments Last Till Analysis.-
    'Private mIntFirstPayment_id, mIntLastPayment_id As Integer
    'Private mColReceiptLines As Collection  '-- ie till balance..-
    ''-- collection of amounts with payment key-
    'Private mColTillAnalysis As Collection
    Private mColTillSummaryLines As Collection

    ''-- collection of formatted Till Payments report lines-
    'Private mColReportLines As Collection

    '-- BROWSE SESSIONS-
    '- Session list now in dataGridView -
    Private mColPrefsSessions As Collection
    Private mBrowse1 As clsBrowse3
    Private mLngSelectedSessionRow As Integer = -1
    Private mIntSession_id As Integer = -1

    '-- Analysis Class Instance-
    Private mClsCashupPayments1 As clsCashupPayments
    '=3403.711-
    Private mbModified As Boolean = False
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '--sub new-
    '--sub new-
    '= 3401.414=  ComputerName is noe INPUT !!

    Public Sub New(ByVal sComputerName As String, _
                   ByRef FrmParent As Form, _
                     ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                          ByVal sVersionPOS As String, _
                          ByRef imageUserLogo As Image, _
                             ByVal SettingsPath As String, _
                            ByVal intStaff_id As Integer, _
                               ByVal sStaffName As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        msComputerName = sComputerName

        mFrmParent = FrmParent

        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName

        mColSqlDBInfo = colSqlDBInfo

        '=  mColPrefsCustomer = colPrefsCustomer
        msVersionPOS = sVersionPOS

        mImageUserLogo = imageUserLogo
        msSettingsPath = SettingsPath  '-4201.0708-

        mIntStaff_id = intStaff_id
        msStaffName = sStaffName

    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
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

    '=3519.0304-
    '-- -- Open cash Drawer- (Current Till). EX clsPOS34Sale-

    Private Function mbOpenCashDrawer() As Boolean
 
        Dim strTillId = gsGetCurrentCashDrawer()
        Dim clsPrintDirect1 As clsPrintDirect
        '=Dim sTillPrinterNameInfoKey, sTillOpenCodeInfoKey As String
        Dim sPrinterName As String = ""
        Dim sEscapeCodes As String = ""
        Dim sTillPrinterNameinfoKey As String = "POS_TillOpenPrinterName_Till_" & strTillId
        Dim sTillOpenCodeinfoKey As String = "POS_TillOpenCode_Till_" & strTillId

        mbOpenCashDrawer = False

        clsPrintDirect1 = New clsPrintDirect
        If (clsPrintDirect1 Is Nothing) Or (mSysInfo1 Is Nothing) Then
            MsgBox("Error- Class not initialised...", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        '= OpenCashDrawer = False
        If (InStr("ABCDEFGH", UCase(strTillId)) <= 0) Then
            MsgBox("Error- " & strTillId & " is invalid Till Id..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        '==  get Printer and ESCape codes for this Till..
        If mSysInfo1.exists(sTillPrinterNameinfoKey) AndAlso _
                    (Trim(mSysInfo1.item(sTillPrinterNameinfoKey)) <> "") Then
            sPrinterName = Trim(mSysInfo1.item(sTillPrinterNameinfoKey))
            If mSysInfo1.exists(sTillOpenCodeinfoKey) AndAlso _
                  (Trim(mSysInfo1.item(sTillOpenCodeinfoKey)) <> "") Then
                sEscapeCodes = Trim(mSysInfo1.item(sTillOpenCodeinfoKey))
            End If
        End If  '-exists-
        If (sPrinterName = "") Or (sEscapeCodes = "") Then
            MsgBox("No Cash-Drawer printer is set up for- " & strTillId, MsgBoxStyle.Exclamation)
            Exit Function
        End If
        '-- ok.. send ESCape codes to printer to Open Drawer..
        '-- send routine has to decode into actual ESC stuff.
        If Not clsPrintDirect1.SendCashDrawerOpenCommand(sPrinterName, sEscapeCodes) Then
            MsgBox("Open drawer Failed ! ", MsgBoxStyle.Exclamation)
        Else
            mbOpenCashDrawer = True
        End If
    End Function  '-open Till-
    '= = = = = = = = = = = = =
    '-===FF->

    '--  BROWSE Previous SESSIONS --

    '--- INITIALISE SESSION Browser.for Session Lookup/Reporting.--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(Optional ByVal sSrchWhereCond As String = "") As Boolean

        '=Dim colPrefs As Collection
        Dim sHostTablename As String
        Dim sWhere As String

        mBrowse1 = New clsBrowse3 '== clsBrowse22
        '-- show full frame..-
        '== FrameBrowse.Height = (LabDescription.Top - FrameBrowse.Top - 4)
        '== DataGridView1.Height = (FrameBrowse.Height - DataGridView1.Top - 8)

        mBrowse1.connection = mCnnSql  '= mRetailHost1.connection
        mBrowse1.colTables = mColSqlDBInfo '= mRetailHost1.colTables 
        mBrowse1.IsSqlServer = True   '= mRetailHost1.IsSqlServer
        mBrowse1.DBname = msSqlDbName  '= mRetailHost1.DBname

        '--  get table/prefs info for this host..--

        '= If Not mRetailHost1.browseGetPrefColumns("stock", sHostTablename, colPrefs) Then
        '==  MsgBox("Can't translate table name to host table..", MsgBoxStyle.Exclamation)
        '== End If
        mBrowse1.tableName = "cashup_sessions"  '==sHostTablename

        '= mBrowse1.FlexGrid = MSHFlexGrid1
        mBrowse1.DataGrid = dgvSessions

        '--  pass controls..--
        mBrowse1.showRecCount = labRecCount '--updates rec. retrieval..
        mBrowse1.showFind = LabFind '--updates Sort Column display..
        mBrowse1.showTextFind = txtFind '--updates Sort Column display..
        '= sWhere = msMakeStockFilter()  '--service or not..-
        '-- add srch args..
        '-- Browser adds "WHERE"..
        If (sSrchWhereCond <> "") Then
            If (sWhere <> "") Then
                sWhere &= " AND "
            End If
            sWhere &= sSrchWhereCond
        End If
        mBrowse1.WhereCondition = sWhere
        mBrowse1.PreferredColumns = mColPrefsSessions  '== mColPrefs
        mBrowse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly
        '= frameBrowse.Enabled = True

        mLngSelectedSessionRow = -1
        mBrowse1.Activate() '-- go..--

        '== txtFind.Focus()

    End Function '--init-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-  Show cashup details for selected session.
    '==    -- 4201.0531.  fix Cashup Session History to show comments in main Comments Textbox.... 
    '==    -- 4201.0531.  fix Cashup Session History to show comments in main Comments Textbox.... 

    Private Function mbShowSessionInfo(ByVal intSession_id As Integer, _
                                         ByVal sComments As String)
        Dim sSql, sList, s1, s2 As String
        Dim sSelectedTill As String = labCashDrawer.Text
        Dim rx As Integer
        Dim datatable1 As DataTable

        txtSessionComment.Text = sComments
        '-- Get all "shortage records" for this session, and display in ListView.
        If (intSession_id > 0) Then
            sSql = "SELECT * FROM dbo.cashup_shortages "
            sSql &= " WHERE (session_id =" & CStr(intSession_id) & "); "
            If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
                MsgBox("Error in getting Shortages recordset for Till payments: " & vbCrLf & _
                                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                Exit Function
            Else
                If Not (datatable1 Is Nothing) AndAlso (datatable1.Rows.Count > 0) Then
                    '- have "shortages" for selected session. for TILL x..
                    Dim item1 As System.Windows.Forms.ListViewItem
                    Dim decReported, decCounted, decDiff As Decimal

                    listViewSession.Items.Clear()
                    '= txtSessionComment.Text = ""
                    For Each datarow1 As DataRow In datatable1.Rows
                        s1 = datarow1.Item("paymenttype_key")
                        item1 = listViewSession.Items.Add(s1)  '--First col does CREATE..-
                        decReported = CDec(datarow1.Item("amount_reported"))
                        item1.SubItems.Add(FormatCurrency(decReported, 2))
                        decCounted = CDec(datarow1.Item("amount_counted"))
                        item1.SubItems.Add(FormatCurrency(decCounted, 2))
                        '-- show difference.
                        item1.SubItems.Add(FormatCurrency((decCounted - decReported), 2))

                    Next datarow1
                Else
                    MsgBox("No Till payments found for selected Session." & vbCrLf & _
                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Information)
                End If '-nothing.-
            End If  '-get-
        End If  '-id-
    End Function  '- mbShowSessionInfo-
    '= = = = = = == = = = = = = = = = = =
    '= = = = = = = = = = = = = =
    '-===FF->


    ''--  Cashup Analysis (Till Balance or Historical Session)...--
    ''== = =
    ''==     v3.3.3301.615..  15-Jun-2016= ===
    ''==       >>  Cashup Analysis- For re-organised payments table-
    ''==              (NOW HAVE "mother" payment record back now. PLUS Details as individ. payments..
    ''--                 So use PAYMENT as main cashup line anchor..)
    ''==   Report now based on RM  Current Session Payments..
    ''==

    '- GONE to class-

     ''-===FF->

    '- update column totals..

    Private Sub mUpdateColumnTotals()
        Dim s1 As String
        Dim decTotalReported As Decimal = 0
        Dim decTotalCounted As Decimal = 0
        For Each gridrow1 As DataGridViewRow In dgvPaymentDetails.Rows
            With gridrow1
                s1 = gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_REPORTED).Value
                decTotalReported += IIf(IsNumeric(s1), CDec(s1), 0)
                s1 = gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_COUNTED).Value
                decTotalCounted += IIf(IsNumeric(s1), CDec(s1), 0)
            End With  '- gridrow1
        Next gridrow1
        txtTotalReported.Text = FormatCurrency(decTotalReported, 2)
        txtTotalCounted.Text = FormatCurrency(decTotalCounted, 2)
        txtTotalDifference.Text = FormatCurrency(decTotalCounted - decTotalReported, 2)
    End Sub  '-mUpdateColumnTotals-
    '= = = = = = =  = = = = = = ==  ==
    '-===FF->

    '-- Analyse for Requested Terminal-
    '--     Must follow Refresh Terminal..-
    '- Get Tlll Balance summary and Load Cashup Till Grid column..

    Private Function mbTillAnalysis(ByVal strTill_id As String) As Boolean
        Dim s1 As String
        Dim colSession As Collection
        '- start/end Payment ID's for this session.
        Dim intPrevLastPayment As Integer = -1
        Dim intStartPayment As Integer = -1
        Dim intEndPayment As Integer = -1

        '== 3519.0208-
        mIntFirstPayment_id = -1
        mIntLastPayment_id = -1
        '=3519-0208=-- Clear the Grid first.
        For Each gridrow1 As DataGridViewRow In dgvPaymentDetails.Rows
            gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_REPORTED).Value = "0.00"
            gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_COUNTED).Value = "0.00"
            '-- Load Difference column with Zeroes..
            gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_DIFFERENCE).Value = "0.00"
        Next
        '-mColLastSessions has last completed session if any..
        If mColLastSessions.Contains(strTill_id) Then
            colSession = mColLastSessions.Item(strTill_id)
            '== 3519.0208-  RECOVERY-  IGNORE invalid zero payment no records..
            If colSession.Contains("last_payment_id") AndAlso _
                        (CInt(colSession.Item("last_payment_id")) > 0) Then
                intPrevLastPayment = colSession.Item("last_payment_id")
            End If
        End If
        '-- Send -1 as intPrevLastPayment if no valid prev. session.
        Call mClsCashupPayments1.cashupAnalysis(strTill_id, intPrevLastPayment, intStartPayment, intEndPayment)
        '==   Updated.- 3519.0208 08-Feb-2019= 
        '--   Return the Payment_id limits..
        mIntFirstPayment_id = mClsCashupPayments1.resultFirstPaymentId
        mIntLastPayment_id = mClsCashupPayments1.resultLastPaymentId
        '- - - - --

        '== REAL Till balance..
        txtTillSummary.Text = vbCrLf
        txtTillSummary.Text &= "== TILL BALANCE ==" & vbCrLf & vbCrLf
        txtTillSummary.Text &= "  For Till- " & strTill_id & ". to " & _
                                 Format(Now, "ddd dd-MMM-yyyy hh:mm tt") & vbCrLf & vbCrLf

        mColTillSummaryLines = New Collection

        mColTillSummaryLines.Add("== TILL BALANCE ==" & vbCrLf & _
                             "== TILL BALANCE ==" & vbCrLf & vbCrLf)
        mColTillSummaryLines.Add("  For Till- " & strTill_id & ". to " & Format(Now, "ddd dd-MMM-yyyy hh:mm tt") & vbCrLf)
        mColTillSummaryLines.Add("")
        Dim sKey1 As String
        Dim sTillLine, sAmount As String
        Dim decAmount As Decimal
        Dim decTillTotal As Decimal = 0

        '-- Show Till figures in CashUp 'Reported' grid column.
        For Each gridrow1 As DataGridViewRow In dgvPaymentDetails.Rows
            With gridrow1
                sKey1 = .Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value
                If mClsCashupPayments1.colTillBalance.Contains(sKey1) Then
                    '-load till reported amt into grid-
                    decAmount = CDec(mClsCashupPayments1.colTillBalance.Item(sKey1))
                    '= sAmount = FormatCurrency(CDec(mClsCashupPayments1.colTillBalance.Item(sKey1)), 2)
                    sAmount = FormatCurrency(decAmount, 2)
                    sAmount = Replace(sAmount, "$", "")  '--strip dollar sign.
                    .Cells(k_PAYGRIDCOL_AMOUNT_REPORTED).Value = sAmount

                    '-- Add a line to Till Printout print collection.
                    '- txtTillSummary.Text -
                    mColTillSummaryLines.Add("<lucida>")   '-- lucida console fixed pitch.
                    sTillLine = LSet(sKey1, 20) & RSet(sAmount, 12)
                    mColTillSummaryLines.Add(sTillLine)
                    '-- Add a line to Till Balance Text Box display...
                    txtTillSummary.Text &= "  - " & sTillLine & vbCrLf
                    decTillTotal += decAmount
                End If
            End With
        Next gridrow1
        '--done-
        '- show text total..
        sAmount = FormatCurrency(decTillTotal, 2)
        sAmount = Replace(sAmount, "$", "")  '--strip dollar sign...
        sTillLine = LSet("== Total:", 20) & RSet(sAmount, 12)
        mColTillSummaryLines.Add("<lucida>")   '-- lucida console fixed pitch.
        txtTillSummary.Text &= vbCrLf & "  - " & sTillLine & vbCrLf

        txtTillSummary.Text &= vbCrLf & "  == the end == " & vbCrLf

        '-- finish print col.
        mColTillSummaryLines.Add("")
        mColTillSummaryLines.Add(sTillLine)
        mColTillSummaryLines.Add("")
        mColTillSummaryLines.Add(vbCrLf & "  == the end == " & vbCrLf)

        Call mUpdateColumnTotals()

        '- Load sessions Grid this Drawer..
        listViewSession.Items.Clear()

        Dim sSrchWhereCond As String = " (cashDrawer='" & strTill_id & "') "
        ' -- Load  session browse Grid..
        Call mbInitialiseBrowse(sSrchWhereCond)

    End Function '- mbTillAnalysis-
    '= = = = = =  ==  ==  == = =  == == =
    '-===FF->

    '-- Refresh Tills/Last-Sessions/Till Balances..
    '-- Discover all known Workstations (Tills) -
    '--    and their latest completed cashup sessions (if any)..

    Private Function mbRefeshTills() As Boolean
        Dim sSql, sList, s1, s2 As String
        Dim sCurrentTill As String = ""
        Dim rx As Integer
        '= Dim row1 As DataGridViewRow
        Dim datatable1 As DataTable

        mbRefeshTills = False
        labRefreshing.Visible = True
        DoEvents()

        '-- save current Till selection if any..
        If (cboCashDrawers.Items.Count > 0) AndAlso (cboCashDrawers.SelectedIndex >= 0) Then
            sCurrentTill = cboCashDrawers.Items(cboCashDrawers.SelectedIndex)
        End If
        mColCashDrawers = New Collection
        sList = ""
        '-- first scan all Cashup sessions.
        sSql = " SELECT DISTINCT cashDrawer "
        sSql &= " FROM dbo.Cashup_Sessions ORDER BY cashDrawer; "
        '-- get the recordset (datatable)..
        If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
            MsgBox("Error in getting recordset for Cashup_Sessions table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '= Me.Close()
            Exit Function
        Else
            If Not (datatable1 Is Nothing) AndAlso (datatable1.Rows.Count > 0) Then
                '-- make a list of distinct Till-ids found...
                For Each datarow1 As DataRow In datatable1.Rows
                    s1 = datarow1.Item("cashDrawer")
                    mColCashDrawers.Add(s1, s1)
                    s2 = "'" & s1 & "'"  '--for SQL list-
                    sList &= IIf(sList = "", s2, ", " & s2)
                Next datarow1
            End If  '-nothing-
        End If '-get-

        '-- Discover any w/s with payments(s) but not yet ever cashed up..
        sSql = " SELECT DISTINCT cashDrawer "
        sSql &= " FROM dbo.payments "
        If sList <> "" Then
            sSql &= " WHERE (cashDrawer NOT IN (" & sList & ") ) "
        End If
        sSql &= " ORDER BY cashDrawer "
        '-- get the recordset (datatable)..
        If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
            MsgBox("Error in getting DISTINCT recordset for terminal payments: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '= Me.Close()
            Exit Function
        Else
            If Not (datatable1 Is Nothing) AndAlso (datatable1.Rows.Count > 0) Then
                '-- make a list of distinct Till-ids found...
                For Each datarow1 As DataRow In datatable1.Rows
                    s1 = datarow1.Item("cashDrawer")
                    mColCashDrawers.Add(s1, s1)
                Next datarow1
            End If  '-nothing-
        End If '-get-

        '-- Add current Till if not discovered.
        'If Not mColTerminals.Contains(msComputerName) Then
        '    mColTerminals.Add(msComputerName, msComputerName)
        'End If
        If (Not mColCashDrawers.Contains(msCurrentAssignedTill)) Then
            mColCashDrawers.Add(msCurrentAssignedTill, msCurrentAssignedTill)
        End If
        '-- Now make and sort the list of Tills..-
        mListTills = New List(Of String)
        For Each s1 In mColCashDrawers
            mListTills.Add(s1)
        Next s1
        mListTills.Sort()

        Dim sTill As String
        '--load into Terminal List/Combo
        cboCashDrawers.Items.Clear()
        For Each sTill In mListTills
            cboCashDrawers.Items.Add(sTill)
        Next

        If (sCurrentTill <> "") Then
            cboCashDrawers.SelectedItem = sCurrentTill '= gsGetCurrentCashDrawer()
        ElseIf (msCurrentAssignedTill <> "") Then
            '-- select us..
            cboCashDrawers.SelectedItem = msCurrentAssignedTill '= gsGetCurrentCashDrawer()
        ElseIf (cboCashDrawers.Items.Count > 0) Then
            cboCashDrawers.SelectedIndex = 0
        End If

        '-- Now discover and save the latest cashup-session for each Till..
        mColLastSessions = New Collection

        Dim colSession As Collection

        For Each sTill In mListTills
            colSession = New Collection
            Dim datarow1 As DataRow '= In datatable1.Rows
            sSql = " SELECT TOP (1) session_id, terminal_id, cashDrawer, session_date, last_payment_id "
            sSql &= " FROM dbo.Cashup_Sessions "
            sSql &= " WHERE (cashDrawer='" & sTill & "') "
            sSql &= " ORDER BY session_date DESC; "
            If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
                MsgBox("Error in getting Session recordset for Till payments: " & vbCrLf & _
                                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '=Me.Close()
                Exit Function
            Else
                If Not (datatable1 Is Nothing) AndAlso (datatable1.Rows.Count > 0) Then
                    '- have last cashup session for TILL x..
                    datarow1 = datatable1.Rows(0)
                    colSession.Add(datarow1.Item("cashDrawer"), "cashDrawer")
                    colSession.Add(datarow1.Item("terminal_id"), "terminal_id")
                    colSession.Add(datarow1.Item("session_id"), "session_id")
                    colSession.Add(datarow1.Item("session_date"), "session_date")
                    colSession.Add(datarow1.Item("last_payment_id"), "last_payment_id")
                End If '-nothing.-
            End If  '-get-
            mColLastSessions.Add(colSession, sTill)  '-- can be empty session collection..
        Next sTill

        '= Dim sSrchWhereCond As String = " (cashDrawer='" & sCurrentTill & "') "
        ' -- Load  session browse Grid..
        '= Call mbInitialiseBrowse(sSrchWhereCond)

        labRefreshing.Visible = False
        DoEvents()

        mbRefeshTills = True

    End Function  '-mbRefeshTerminals-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- If any counts are different from reported values.

    Private Function mbCashupHasDifferences() As Boolean
        Dim decReported, decCounted As Decimal
        Dim s1, s2 As String

        mbCashupHasDifferences = False
        '-- Check if all Payment Type rows have counts entered..
        For Each gridrow1 As DataGridViewRow In dgvPaymentDetails.Rows
            s1 = gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_REPORTED).Value
            decReported = IIf(IsNumeric(s1), CDec(s1), 0)
            s1 = gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_COUNTED).Value
            decCounted = IIf(IsNumeric(s1), CDec(s1), 0)
            If (decCounted <> decReported) Then
                mbCashupHasDifferences = True
                Exit For
            End If
        Next gridrow1
    End Function  '-mbCashupHasDifferences-
    '= = = = = = = = = = = = = = = = =

    '- if Cashup can Commit..-

    Private Function mbCashupCompletedOK() As Boolean

        mbCashupCompletedOK = False
        '-- Check if all Payment Type rows have counts entered..
        Dim bAllDone As Boolean = True   '--assume all done-
        Dim s1 As String

        For Each gridrow1 As DataGridViewRow In dgvPaymentDetails.Rows
            s1 = gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_REPORTED).Value
            If IsNumeric(s1) AndAlso (CDec(s1) <> 0) Then  '-needs a count -
                If (gridrow1.Cells(k_PAYGRIDCOL_VALIDATED).Value <> "1") Then
                    bAllDone = False
                End If
            End If
        Next gridrow1
        If bAllDone Then mbCashupCompletedOK = True '= btnCashupCommit.Enabled = True
    End Function  '-mbCashupCommitOK-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    Private Sub frmCashup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sName, s1 As String
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer

        '=grpBoxPayment.Text = ""
        '=3401.414== 
        '--  IS NOW an INPUT-  msComputerName = My.Computer.Name

        msCurrentUserName = gsGetCurrentUser()

        labCashDrawer.Text = msComputerName
        labThisComputer.Text = msComputerName
        btnCashupCommit.Enabled = False
        labExplain.Visible = False
        txtCashupComments.Text = ""
        msCurrentAssignedTill = gsGetCurrentCashDrawer()
        labCurrentTill.Text = msCurrentAssignedTill

        labNotYOurTill.Left = labIsYourTill.Left
        labNotYOurTill.Visible = False

        labStaffName.Text = msStaffName  '=3501.1030=

        '- set white BG for paying col..
        '== dgvInvoices.Columns(k_INVGRIDCOL_PAYING_NOW).DefaultCellStyle.BackColor = Color.White
        '- position on top of calling form..
        If mFrmParent Is Nothing Then
            Call CenterForm(Me)
        Else
            Me.Left = mFrmParent.Left + 16
            Me.Top = mFrmParent.Top + 33
        End If

        '= msSettingsPath = gsLocalSettingsPath() '= default Jobmatix33=
        '==
        '== NEW revision-
        '==    -- 4201.0708.  Started 05-July-2019-
        '==       -- Add file->Local Preference for LOCAL Re-ordering of payment Details.
        '==       -- Payments and Sales-  Use clsPaymentTypes to get PaymentDetails for Grid. .

        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboReportPrinters.Items.Add(sName)
                cboReceiptPrinters.Items.Add(sName)
                If (InStr(LCase(sName), "adobe pdf") > 0) Then
                    msPdfPrinterName = sName  '-save PDF printer name--
                End If
            Next sName

            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_reportPrtSettingKey) AndAlso _
                     (mLocalSettings1.item(k_reportPrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_reportPrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboReportPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboReportPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboReportPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            '-receipt-
            If mLocalSettings1.exists(k_receiptPrtSettingKey) AndAlso _
                    (mLocalSettings1.item(k_receiptPrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_receiptPrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it-
                    cboReceiptPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
                End If
            Else '-no pref-
                If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query receipt prt.--
        End If '-getAvail.-  

        Me.Text = "JobMatixPOS Cashup"
        grpBoxPrinters.Text = "Set Selected printers-"
        grpBoxTerminal.Text = "Workstation selected-"

        mColPrefsSessions = New Collection
        mColPrefsSessions.Add("cashDrawer AS Till")
        mColPrefsSessions.Add("session_id")
        mColPrefsSessions.Add("session_date")
        mColPrefsSessions.Add("first_payment_id AS PmntStart")
        mColPrefsSessions.Add("last_payment_id AS PmntEnd")
        mColPrefsSessions.Add("staff_name")
        mColPrefsSessions.Add("comments")

        '-- get system Info table data.-
        '=3403.1112=
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        '= Call mbRefreshSystemInfo()
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        msBusinessName = mSysInfo1.item("BUSINESSNAME")


        '-  make instance for analysis..
        mClsCashupPayments1 = New clsCashupPayments(mCnnSql, msSqlDbName, mColSqlDBInfo)

    End Sub  '-load -
    '= = = = = = = = = = = =
    '-===FF->

    '--Activated--
    '--Activated--
    Private Sub frmCashup_Activated(ByVal sender As System.Object, _
                            ByVal e As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub
        mbActivated = True

    End Sub  '- --Activated---
    '= = = = = = = = = = == =

    '-- S h o w n --

    Private Sub frmCashup_Shown(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) Handles MyBase.Shown
        Dim sSql, sList, s1, s2 As String
        Dim colPaymentTypes, col1 As Collection
        Dim rx As Integer
        Dim row1 As DataGridViewRow
        Dim datatable1 As DataTable

        '= If mbActivated Then Exit Sub
        '= mbActivated = True

        labDLLversion.Text = msVersionPOS

        labStaffName.Text = msStaffName

        '- load payment types..-
        '==sSql = "SELECT * from [paymentTypes];"
        dgvPaymentDetails.Rows.Clear()

        '=--3101.1206=
        '== NEW revision-
        '==    -- 4201.0708.  Started 05-July-2019-
        '==       -- Add file->Local Preference for LOCAL Re-ordering of payment Details.
        '==       -- Payments and Sales-  Use clsPaymentTypes to get PaymentDetails for Grid. .
        Dim clsPayTypes1 As clsPaymentTypes
        clsPayTypes1 = New clsPaymentTypes(msSettingsPath)
        '-test-
        '= MsgBox("Settings path: " & vbCrLf & msSettingsPath, MsgBoxStyle.Information)
        colPaymentTypes = clsPayTypes1.getColPaymentTypes()
        '- end of new bit..
        '= colPaymentTypes = gColPaymentTypes()  '--3101.1206= Get collection of types.
        rx = 0
        For Each col1 In colPaymentTypes
            row1 = New DataGridViewRow
            dgvPaymentDetails.Rows.Add(row1)
            With dgvPaymentDetails.Rows(rx)
                .Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value = col1("description")
                .Cells(k_PAYGRIDCOL_AMOUNT_COUNTED).Value = "0.00"
                .Cells(k_PAYGRIDCOL_AMOUNT_COUNTED).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                '-- Load Difference column with Zeroes..
                .Cells(k_PAYGRIDCOL_AMOUNT_DIFFERENCE).Value = "0.00"
                .Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value = col1("key")
                .Cells(k_PAYGRIDCOL_VALIDATED).Value = "0"
            End With
            rx += 1
        Next col1
        DoEvents()

        '-- Discover all known CashDrawers (Tills) -
        '--    and their latest cashup sessions (if any)..
        If Not mbRefeshTills() Then
            Me.Close()
            Exit Sub
        End If

        '--  GET Current Till balance stuff,
        '--   and load details into grid..

        '- Get Tlll Balance summary and Load Cashup Till Grid column..
        Call mbTillAnalysis(labCashDrawer.Text)

        '= txtCustBarcode.Select()
        labHelpCashup.ForeColor = Color.Blue
        labHelpCashup.Text = "Enter Details of Cash/Cheques counted, and EFTPOS Settlement amounts."

        TabControlMain.SelectTab("TabPageTillBalance") '--Start on Auto..-

        Me.KeyPreview = True  '-to catch Ctl-Z

        mbIsInitialising = False

    End Sub  '--SHOWN..  was Activated--
    '= = = = = = = = = = = = = = = == = = =
    '-===FF->
    '= 3519.0304=
    '-- catch Ctl-Z for ope-Till function..

    '- PreviewKeyDown is where you preview the key.
    '- Do not put any logic here, instead use the
    '- KeyDown event after setting IsInputKey to true.
    Private Sub frmCashup_PreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs) _
                                            Handles Me.PreviewKeyDown
        Select Case (e.KeyCode)
            Case Keys.Escape '= Keys.Down, Keys.Up
                e.IsInputKey = True
        End Select
    End Sub  '-Me.PreviewKeyDown-
    '= = = = = = = = =  = = = = = =

    '-- FORM- Key Down..-
    '-- catch Ctl-Z...-

    Private Sub frmPOS34Main_KeyDown(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                               Handles MyBase.KeyDown
        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000

        If (KeyCode = System.Windows.Forms.Keys.F6) And _
                        ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--Hold--
            '-- If Sale Tab is open, and Hold button if enabled..
            '- let it go if we're clicking on different page.
            ''- we're ON POS page if we're in JobMatix..
         ElseIf (KeyCode = System.Windows.Forms.Keys.Escape) And _
                    ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--ESCAPE: cancel--
            '-- If Sale Tab is open, and Sale instance enabled..
            '-- ESCape is used to Cancel the Sale..
            '=If (LCase(TabControlmain.SelectedTab.Name) = "tabpagesales") Then
            '=End If  '-sale page-
        ElseIf ((KeyCode = System.Windows.Forms.Keys.Z) And (eventArgs.Control)) Then '--Ctl-Z- Open Cash Drawer.-
            '-- Ctl-Z- Open Cash Drawer...
            '= MsgBox("Got Ctl-Z..", MsgBoxStyle.Information)   '--TEST-
            'If mClsSale1 IsNot Nothing Then
            '    Call mClsSale1.OpenCashDrawer()
            'End If '-nothing.
            If mbOpenCashDrawer() Then
                '-ok-
                '= MsgBox("Cmd was sent..", MsgBoxStyle.Information)  '--TEST-
            End If
        End If  '--F6/ESCAPE-
    End Sub '--keyDown..-
    '= = = = = = = = = = == =
    '-===FF->

    '-cboTerminals_SelectedIndexChanged- 

    Private Sub CashDrawers_SelectedIndexChanged(sender As Object, _
                                                   e As EventArgs) Handles cboCashDrawers.SelectedIndexChanged
        labCashDrawer.Text = cboCashDrawers.SelectedItem

        If mbIsInitialising Then Exit Sub

        '- show mine/yours.
        If UCase(labCashDrawer.Text) = UCase(msCurrentAssignedTill) Then  '--is showing our till-
            labNotYOurTill.Visible = False
            labIsYourTill.Visible = True
        Else  '- not the current till for this computer.
            labIsYourTill.Visible = False
            labNotYOurTill.Visible = True
        End If
        '- Get Tlll Balance summary and Load Cashup Till Grid column..
        Call mbTillAnalysis(labCashDrawer.Text)

    End Sub '--cboTerminals_SelectedIndexChanged-
    '= = = = = = = = = = = = =

    '-cboReportPrinters_SelectedIndexChanged-

    Private Sub cboReportPrinters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboReportPrinters.SelectedIndexChanged

        If (cboReportPrinters.SelectedIndex >= 0) Then
            msReportPrinterName = cboReportPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_reportPrtSettingKey, msReportPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, msReportPrinterName) Then
                MsgBox("Failed to save Report printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-

    End Sub  '-cboReportPrinters_SelectedIndexChanged-
    '= = = = = = = = = = = =  = = = = = = = = = ==  =

    '-cboReceiptPrinters_SelectedIndexChanged-
    Private Sub cboReceiptPrinters_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboReceiptPrinters.SelectedIndexChanged

        If (cboReceiptPrinters.SelectedIndex >= 0) Then
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_receiptPrtSettingKey, msReceiptPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, msReceiptPrinterName) Then
                MsgBox("Failed to save Receipt printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-

    End Sub  '-cboReceiptPrinters_SelectedIndexChanged-
    '= = = = = = = = = = =  = = = = = = = = = == =  = =

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click

        If mbIsInitialising Then Exit Sub

        btnRefresh.Enabled = False
        If Not mbRefeshTills() Then
            Me.Close()
            Exit Sub
        End If
        '- Get Tlll Balance summary and Load Cashup Till Grid column..
        Call mbTillAnalysis(labCashDrawer.Text)
        btnRefresh.Enabled = True
    End Sub '-refresh-
    '= = = = = = = = = = = = = == =
    '-===FF->

    '-btnPrint Till Balance_Click-

    Private Sub btnPrintTillBalance_Click(sender As Object, e As EventArgs) Handles btnPrintTillSummary.Click
        Dim prtDocs1 As clsPrintSaleDocs

        If (cboReportPrinters.Items.Count > 0) AndAlso (cboReportPrinters.SelectedIndex >= 0) Then
            '-- load prt object-
            prtDocs1 = New clsPrintSaleDocs
            prtDocs1.versionPOS = msVersionPOS
            '== prtDocs1.PrtSelectedPrinter = mPrtReceipt
            prtDocs1.PrtSelectedPrinterName = msReceiptPrinterName   '= msDefaultPrinterName
        Else
            MsgBox("No printer selected..", MsgBoxStyle.Exclamation)
        End If  '--have printer-

        '==  3519.0217=  Should be REAL Till balance.

        If (Not (mColTillSummaryLines Is Nothing)) AndAlso (mColTillSummaryLines.Count > 0) Then
            '-- go print--
            If Not prtDocs1.PrintDocket(mColTillSummaryLines) Then
                MsgBox("Print Failed..", MsgBoxStyle.Exclamation)
            End If
        End If  '-nothing-

    End Sub  '-btnPrintTillBalance_Click-
    '= = = = = = = = = = = = = == = = = = =

    '=  PrintRevenueSummary --

    Private Sub btnPrintRevenueSummary_Click(sender As Object, e As EventArgs) _
                                                  Handles btnPrintRevenueSummary.Click
        Dim prtDocs1 As clsPrintSaleDocs

        If (cboReportPrinters.Items.Count > 0) AndAlso (cboReportPrinters.SelectedIndex >= 0) Then
            '-- load prt object-
            prtDocs1 = New clsPrintSaleDocs
            prtDocs1.versionPOS = msVersionPOS
            '== prtDocs1.PrtSelectedPrinter = mPrtReceipt
            prtDocs1.PrtSelectedPrinterName = msReceiptPrinterName   '= msDefaultPrinterName
        Else
            MsgBox("No printer selected..", MsgBoxStyle.Exclamation)
        End If  '--have printer-
        If (Not (mClsCashupPayments1.ColRevenueSummaryLines Is Nothing)) AndAlso _
                                               (mClsCashupPayments1.ColRevenueSummaryLines.Count > 0) Then
            '-- go print--
            If Not prtDocs1.PrintDocket(mClsCashupPayments1.ColRevenueSummaryLines) Then
                MsgBox("Print Failed..", MsgBoxStyle.Exclamation)
            End If
        End If  '-nothing-
    End Sub  '-btnPrintRevenueSummary_Click-
    '= = = = = = = = = = = = = =  = = = = = =
    '-===FF->

    '- PrintTillListing --

    Private Sub btnPrintTillListing_Click(sender As Object, e As EventArgs) Handles btnPrintTillListing.Click

        If (mClsCashupPayments1.colReportLines IsNot Nothing) Then
            Dim s1 As String
            Dim clsReportPrint1 As clsReportPrinter
            Dim bPreviewOnly As Boolean = True

            '--load report info and show--
            clsReportPrint1 = New clsReportPrinter
            s1 = "<drawline fontstyle=""bold"" />"
            mClsCashupPayments1.colReportLines.Add(s1)

            Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS, _
                                                     mClsCashupPayments1.colReportLines, msReportPrinterName, _
                                                      msBusinessName, "Till Payments- Current Session-", _
                                                       Color.DarkMagenta, _
                                                       "Shows Payments received " & vbCrLf & _
                                                       "For Till: " & labCashDrawer.Text, _
                                                       Format(Now, "ddd dd-MMM-yyyy hh:mm tt"), _
                                                 " -Payment- " & Space(40) & " Description" & Space(60) & _
                                                           "- Invoice-        Staff  ")
            System.Windows.Forms.Application.DoEvents()
        End If  '-nothing-
    End Sub  '-PrintTillListing-
    '= = = = = == = = = = = = = == =

    '-btnPrintSortedDetail--

    Private Sub btnPrintSortedDetail_Click(sender As Object, e As EventArgs) Handles btnPrintSortedDetail.Click
        Dim s1 As String
        Dim clsReportPrint1 As clsReportPrinter
        Dim bPreviewOnly As Boolean = True

        If (mClsCashupPayments1.colSortedDetailLines IsNot Nothing) Then
            '--load report info and show--
            clsReportPrint1 = New clsReportPrinter
            s1 = "<drawline fontstyle=""bold"" />"
            mClsCashupPayments1.colSortedDetailLines.Add(s1)

            clsReportPrint1.FixedPitchContent = True
            Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS, _
                                                     mClsCashupPayments1.colSortedDetailLines, msReportPrinterName, _
                                                      msBusinessName, "Sorted Payment Details- (Revenue)-", _
                                                       Color.DarkMagenta, _
                                                       "Payment Items by Type " & vbCrLf & _
                                                       "For Till: " & labCashDrawer.Text, _
                                                        "Current Session: " & Format(Now, "ddd dd-MMM-yyyy hh:mm tt"), _
                                                 "--   Paym Type -- " & Space(20) & " PaymentID" & Space(16) & _
                                                           "- Amount-             - Invoice -")
            System.Windows.Forms.Application.DoEvents()
        End If '-nothing-
    End Sub  '-btnPrintSortedDetail--
    '= = = = = = = = = =  = = = = = = =
    '-===FF->

    '-- Cashup Sessions (Historical) Grid and Reports--
    '-- Cashup Sessions (Historical) Grid and Reports--
    '-- Cashup Sessions (Historical) Grid and Reports--

    '- Session Report - (Payments list for selected session.-

    Private Sub btnSessionReport_Click(sender As Object, e As EventArgs) Handles btnSessionReport.Click
        Dim colKeys, colRowValues As Collection
        Dim intPrevLastPayment, intStartPayment, intEndPayment As Integer
        Dim dateOfSession As Date

        intPrevLastPayment = -1  '-not getting current till.
        '-- get selected session-
        If (mLngSelectedSessionRow >= 0) Then
            Call mBrowse1.SelectRecord(mLngSelectedSessionRow, colKeys, colRowValues)

            intStartPayment = CInt(colRowValues.Item("pmntstart")("value"))
            intEndPayment = CInt(colRowValues.Item("pmntend")("value"))
            dateOfSession = CDate(colRowValues.Item("session_date")("value"))

            '-- get report Lines collection.
            '--  Will be saved in "mColReportLines"..
            Call mClsCashupPayments1.cashupAnalysis(labCashDrawer.Text, intPrevLastPayment, intStartPayment, intEndPayment)

            '-- print  report-
            Dim s1 As String
            Dim clsReportPrint1 As clsReportPrinter
            Dim bPreviewOnly As Boolean = True

            '--load report info and show--
            clsReportPrint1 = New clsReportPrinter
            s1 = "<drawline fontstyle=""bold"" />"
            mClsCashupPayments1.colReportLines.Add(s1)

            Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS, _
                                                     mClsCashupPayments1.colReportLines, msReportPrinterName, _
                                                    msBusinessName, "Till Report- SELECTED Session-", _
                                                    Color.DarkViolet, _
                                                   "Shows Session Payments for Till: " & labCashDrawer.Text & vbCrLf & _
                                                   "For Session ending: " & Format(dateOfSession, "ddd dd-MMM-yyyy hh:mm tt"), _
                                                   "For Session: " & Format(dateOfSession, "ddd dd-MMM-yyyy hh:mm tt"), _
                                                     " TILL- -Payment-    Description" & Space(48) & _
                                                           "  -Customer-.       Staff  ")

            System.Windows.Forms.Application.DoEvents()
        End If  '-sel row-

    End Sub  '-btnSessionReport_Click-
    '= = = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = == =
    '-===FF->


    '--BROWSING SESSIONS.. --

    '--  D at a  G r i d  E v e n t s..--
    '--  D at a  G r i d  E v e n t s..--

    '--  Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvSessions_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvSessions.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvSessions.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowse1.SortColumn(sName)
    End Sub
    '= = = = = = = = = = = = = == =
    '-===FF->

    '-- cell click.--
    '-- cell click.--
    '==    -- 4201.0531.  fix Cashup Session History to show comments in main Comments Textbox.... 

    Private Sub dgvSessions_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                                      Handles dgvSessions.CellMouseClick

        Dim lRow, lCol As Integer
        Dim intSession_id As Integer
        Dim colKeys, colRowValues As Collection
        Dim sComments As String

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (dgvSessions.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedSessionRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedSessionRow, colKeys, colRowValues)
                intSession_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                sComments = colRowValues("comments")("value")
                If (intSession_id > 0) And (intSession_id <> mIntSession_id) Then '-- has changed..-
                    '==    -- 4201.0531.  fix Cashup Session History to show comments in main Comments Textbox.... 
                    Call mbShowSessionInfo(intSession_id, sComments)
                End If
            End If  '-selected-
        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  
    '-- select row to edit--
    '-- select row to edit--
    '==    -- 4201.0531.  fix Cashup Session History to show comments in main Comments Textbox.... 

    Private Sub dgvSessions_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                    ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                                Handles dgvSessions.CellMouseDoubleClick

        Dim lRow As Integer
        Dim intSession_id As Integer
        Dim colKeys, colRowValues As Collection
        Dim sComments As String

        '== lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow >= 0) Then '--ok--
            mLngSelectedSessionRow = lRow
            '--  get session id and show info..--
            If (lRow >= 0) And (dgvSessions.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedSessionRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedSessionRow, colKeys, colRowValues)
                intSession_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                sComments = colRowValues("comments")("value")
                If (intSession_id > 0) And (intSession_id <> mIntSession_id) Then '-- has changed..-
                    '==    -- 4201.0531.  fix Cashup Session History to show comments in main Comments Textbox.... 
                    Call mbShowSessionInfo(intSession_id, sComments)
                End If
            End If  '-selected-
        End If '--row--
    End Sub '--click--
    '= = = = = = = = = =

    '-- sessions grid- Selection changed.-
    '==  4201.0531.  fix Cashup Session History to show comments in main Comments Textbox.... 


    Private Sub dgvSessions_SelectionChanged(ByVal sender As Object, _
                                   ByVal e As EventArgs) Handles dgvSessions.SelectionChanged
        Dim intSession_id As Integer
        Dim colKeys, colRowValues As Collection
        Dim sComments As String

        If (dgvSessions.SelectedRows.Count > 0) Then
            Dim intRowIndex = dgvSessions.CurrentCell.RowIndex
            If (intRowIndex >= 0) Then
                mLngSelectedSessionRow = intRowIndex
                Call mBrowse1.SelectRecord(mLngSelectedSessionRow, colKeys, colRowValues)
                intSession_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                sComments = colRowValues("comments")("value")
                If (intSession_id > 0) And (intSession_id <> mIntSession_id) Then '-- has changed..-
                    '== - 4201.0531.  fix Cashup Session History to show comments in main Comments Textbox.... 
                    Call mbShowSessionInfo(intSession_id, sComments)
                End If
            End If  '-index-
        End If '-count-
    End Sub  '-SessionsList_SelectionChanged-
    '== = = = =  == = = = = = = = = =  =
    '-===FF->

    '-- Sessions Browser.. txt FIND Activity.--
    '-- Sessions Browser.. txt FIND Activity.--
    '--BROWSING Sessions.. --

    '-- key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress

    End Sub '--click--
    '= = = = = = = = = = =

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--


    Private Sub txtFind_Enter(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles txtFind.Enter
        Dim styleBold As FontStyle = FontStyle.Bold

        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        '= LabFind.Font = VB.FontChangeBold(LabFind.Font, True)
        LabFind.Font = New Font(LabFind.Font, styleBold)  '= VB.FontChangeBold(LabFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFind_Leave(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtFind.Leave
        Dim styleReg As FontStyle = FontStyle.Regular

        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabFind.Font = New Font(LabFind.Font, styleReg)  '= VB6.FontChangeBold(LabFind.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged

        Call mBrowse1.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '--- End of day-  C a s h u p --
    '--- End of day-  C a s h u p --

    '--- C a s h u p --
    '--- C a s h u p --

    Private Sub txtComments_TextChanged(sender As Object, e As EventArgs) Handles txtCashupComments.TextChanged

        If (Trim(txtCashupComments.Text) <> "") AndAlso mbCashupCompletedOK() Then
            labExplain.Visible = False
            btnCashupCommit.Enabled = True
        End If
    End Sub  '-txtComments_TextChanged-
    '= = = = = = = = = = = = == = = ==

    '--amount_counted --
    '-   C e l l  V a l i d a t i n g--=  

    Private Sub dgvPaymentDetails_CellValidating(ByVal sender As Object, _
                                                 ByVal ev As DataGridViewCellValidatingEventArgs) Handles dgvPaymentDetails.CellValidating

        If mbIsInitialising Then Exit Sub

        Dim lRow, lCol As Integer
        Dim sData, s1 As String

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        dgvPaymentDetails.Rows(ev.RowIndex).ErrorText = Nothing
        '=dgvPaymentDetails.Rows(ev.RowIndex).Cells(lCol).ErrorText = Nothing
        If LCase(dgvPaymentDetails.Columns(lCol).Name) = "amount_counted" Then
            '== sData = Me.dgvPaymentDetails.Rows(ev.RowIndex).Cells(lCol).FormattedValue
            sData = Trim(ev.FormattedValue.ToString)
            If (sData = "") OrElse IsNumeric(sData) Then
                '--ok-  check if cashout also..
                '--  get payment descr.
                s1 = dgvPaymentDetails.Rows(ev.RowIndex).Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value
                If (InStr(LCase(s1), "cash") > 0) Then  '--cash paid.-
                    '== MsgBox("Cash paid: " & sData, MsgBoxStyle.Information)
                    If (sData <> "") AndAlso (CDec(sData) > 0) Then
                        '-- was checking cashout.
                    End If
                End If  '--cashout.-
            Else
                ev.Cancel = True
                dgvPaymentDetails.Rows(ev.RowIndex).ErrorText = "Amount must be numeric. "
                '= dgvPaymentDetails.Rows(ev.RowIndex).Cells(lCol).ErrorText = "Amount must be numeric."
                MsgBox("Amount must be numeric.  !", MsgBoxStyle.Exclamation)
            End If  '--numeric-
        End If '--amount col.-

        '--thats all-

    End Sub  '--cell validating.--
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '- Validated-

    Private Sub dgvPaymentDetails_CellValidated(ByVal sender As Object, _
                                                       ByVal ev As DataGridViewCellEventArgs) Handles dgvPaymentDetails.CellValidated

        If mbIsInitialising Then Exit Sub

        Dim intRow, intCol As Integer
        Dim sData, s1 As String
        intRow = ev.RowIndex
        intCol = ev.ColumnIndex
        Dim decReported, decCounted, decDiff As Decimal

        mbModified = True
        If LCase(dgvPaymentDetails.Columns(intCol).Name) = "amount_counted" Then
            sData = Trim(dgvPaymentDetails.Rows(ev.RowIndex).Cells(intCol).Value)
            decCounted = 0
            If (sData = "") Then
                sData = "0.00"
            End If
            If (sData <> "") AndAlso IsNumeric(sData) Then
                decCounted = CDec(sData)
                dgvPaymentDetails.Rows(ev.RowIndex).Cells(intCol).Value = FormatCurrency(CDec(sData), 2)
            End If
            ' Clear any error messages that may have been set in cell validation.
            '== dgvPaymentDetails.Rows(ev.RowIndex).ErrorText = Nothing
            '= Call mbUpdateTransactionTotal()

            '-- Update Difference Column....
            With dgvPaymentDetails.Rows(ev.RowIndex)
                s1 = .Cells(k_PAYGRIDCOL_AMOUNT_REPORTED).Value
                decReported = IIf(IsNumeric(s1), CDec(s1), 0)
                decDiff = decCounted - decReported
                .Cells(k_PAYGRIDCOL_AMOUNT_DIFFERENCE).Value = FormatCurrency(decDiff, 2)
                '-set forecolour-
                If (decDiff < 0) Then
                    .Cells(k_PAYGRIDCOL_AMOUNT_DIFFERENCE).Style.ForeColor = Color.Red
                Else
                    .Cells(k_PAYGRIDCOL_AMOUNT_DIFFERENCE).Style.ForeColor = Color.Black
                End If
                '-- flag columns as having been attempted.
                .Cells(k_PAYGRIDCOL_VALIDATED).Value = "1"
            End With

            '- update column totals..
            Call mUpdateColumnTotals()

            '-- Check if all Payment Type rows have counts entered..
            If (Trim(txtCashupComments.Text) <> "") AndAlso _
                              mbCashupCompletedOK() AndAlso (Not mbCashupHasDifferences()) Then
                labExplain.Visible = False
                btnCashupCommit.Enabled = True
            Else
                If (Trim(txtCashupComments.Text) <> "") Then
                    labExplain.Visible = False
                Else
                    labExplain.Visible = True
                End If
            End If

            'Dim bAllDone As Boolean = True   '--assume all done-
            'For Each gridrow1 As DataGridViewRow In dgvPaymentDetails.Rows
            '    If (gridrow1.Cells(k_PAYGRIDCOL_VALIDATED).Value <> "1") Then
            '        bAllDone = False
            '    End If
            'Next gridrow1
            'If bAllDone Then btnCashupCommit.Enabled = True

        End If '-amount_counted-

        '--thats all-

    End Sub  '-validated-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '- cancel- cashup..

    Private Sub btnCashupCancel_Click(sender As Object, e As EventArgs) Handles btnCashupCancel.Click

        If mbModified Then
            If MsgBox("Abandon changes ", _
                   MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + +MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Me.Hide()
            End If
        End If

    End Sub  '-cancel-
    '= = = = = = = = ===

    '--commit--

    Private Sub btnCashupCommit_Click(sender As Object, e As EventArgs) Handles btnCashupCommit.Click


        '==   Updated.- 3519.0208 08-Feb-2019= 
        '- check limits.
        If (mIntFirstPayment_id <= 0) Or (mIntLastPayment_id <= 0) Then
            MsgBox("No valid Payment No. limits in current analysis.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If MsgBox("This Commit will complete and close this Till Session." & vbCrLf & _
                   "Ok to Continue ?", _
                    MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Ok Then
            Exit Sub
        End If '-ok-

        '-- Commit Cashup Session.
        Dim sqlTransaction1 As OleDbTransaction
        Dim sSql, sValues, sCashDrawer As String
        Dim v2 As Object
        Dim intID As Integer
        Dim intSession_Id As Integer = -1
        Dim sKey, sDescription, sPayAmountReported, sPayAmountCounted As String
        Dim decPayAmountReported, decPayAmountCounted As Decimal
        sCashDrawer = VB.Left(Trim(labCashDrawer.Text), 1)

        mCnnSql.ChangeDatabase(msSqlDbName) '--USE our db..-
        Try  '--Main try-

            '- 1. Start Transaction.
            sqlTransaction1 = mCnnSql.BeginTransaction

            '- 2- Write Session Record..
            sSql = "INSERT INTO dbo.cashup_sessions ("
            sSql &= "  staff_id, staff_name, cashdrawer, currentWindowsUserName, "
            sSql &= "  terminal_id, first_payment_id, last_payment_id,  "

            sValues = "VALUES ( " & CStr(mIntStaff_id) & ", " & _
                           " '" & gsFixSqlStr(msStaffName) & "', '" & sCashDrawer & "', " & _
                                                      " '" & gsFixSqlStr(msCurrentUserName) & "', "
            sValues &= " '" & gsFixSqlStr(msComputerName) & "', " & _
                            CStr(mIntFirstPayment_id) & ", " & CStr(mIntLastPayment_id) & ", "

            '-- Now Finish Session rec.-
            sSql &= " comments  "
            sSql &= ") "
            sValues &= " '" & gsFixSqlStr(txtCashupComments.Text) & " '"
            sValues &= "); "

            If Not mbExecuteSql(mCnnSql, sSql & sValues, True, sqlTransaction1) Then
                labHelpCashup.Text = "Saving Session Record FAILED.."
                Exit Sub
            End If  '--exec invoice-

            '- 3. Retrieve Invoice No. (IDENTITY of Invoice record written.)-
            sSql = "SELECT CAST(IDENT_CURRENT ('dbo.cashup_sessions') AS int);"
            If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                intSession_Id = intID
                '-- update session display later..-
            Else
                MsgBox("Failed to retrieve latest Session No..", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            '- 4- Write batch of "shortage" records..  one
            '--   for each Payment Type with value..
            If (dgvPaymentDetails.Rows.Count > 0) Then
                sSql = ""
                For Each gridrow1 As DataGridViewRow In dgvPaymentDetails.Rows
                    decPayAmountReported = 0
                    decPayAmountCounted = 0
                    sPayAmountReported = gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_REPORTED).Value
                    sPayAmountCounted = gridrow1.Cells(k_PAYGRIDCOL_AMOUNT_COUNTED).Value
                    sKey = Trim(gridrow1.Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value)
                    sDescription = gridrow1.Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value
                    If IsNumeric(sPayAmountReported) Then
                        decPayAmountReported = CDec(sPayAmountReported)
                    End If
                    If IsNumeric(sPayAmountCounted) Then
                        decPayAmountCounted = CDec(sPayAmountCounted)
                    End If


                    '==  Target is new Build 4251..
                    '==  Target is new Build 4251..
                    '==
                    ''== 4251- Fixed- Negative payments (ie more Refund ) were not being written out to Shortages.
                    '-   If IsNumeric(sPayAmountReported) AndAlso (CDec(sPayAmountReported) > 0) Then ===

                    If IsNumeric(sPayAmountReported) AndAlso _
                                    ((CDec(sPayAmountReported) <> 0) Or (CDec(sPayAmountCounted) <> 0)) Then
                        '- write "shortage" for all actual payments received..
                        sSql &= "INSERT INTO dbo.Cashup_Shortages (session_id, "
                        sSql &= " paymentType_key, paymentType_descr, "
                        sSql &= "  amount_reported, amount_counted )"
                        sSql &= "  VALUES (" & CStr(intSession_Id) & ", "
                        sSql &= "'" & gsFixSqlStr(sKey) & "', "
                        sSql &= "'" & gsFixSqlStr(sDescription) & "', "
                        sSql &= CStr(decPayAmountReported) & ", " & CStr(decPayAmountCounted)
                        sSql &= "); "
                    End If  '-amount 0.
                Next gridrow1
                '- execute-
                If sSql <> "" Then
                    '-- insert all rows..-
                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                        labHelpCashup.Text = "Insert Shortage-details Failed.."
                        '==  Target is new Build 4251..
                        Me.Close()
                        Exit Sub
                    End If  '--exec pay detail LINE-
                End If
            End If '--rows-

        Catch ex As Exception
            MsgBox("Runtime Error in Commit Session-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            sqlTransaction1.Rollback()
            labHelpCashup.Text = "Insert Shortage-details Failed.."
            '==  Target is new Build 4251..
            Me.Close()
            Exit Sub
        End Try  '- Main Try-

        '- 5.  Commit SESSION.-
        Try
            sqlTransaction1.Commit()
            MsgBox("Till-" & labCashDrawer.Text & ": Session committed ok.." & vbCrLf & _
                      " No: " & intSession_Id, MsgBoxStyle.Information)
            btnCashupCommit.Enabled = False
            Call mbRefeshTills()
            labHelpCashup.Text = "Cashup session committed ok..."
            '==  Target is new Build 4251..
            '== Me.Close()
            '- reset everything..
            Call mbTillAnalysis(labCashDrawer.Text)

        Catch ex As Exception
            MsgBox("Session commit FAILED.. ID=" & intSession_Id, MsgBoxStyle.Exclamation)
            labHelpCashup.Text = "Insert Shortage-details Failed.."
            '==  Target is new Build 4251..
            Me.Close()
            Exit Sub
        End Try
    End Sub  '-btnCashupCommit-
    '= = = = = = = = = = = = = = = = =


End Class  '--frmCashup-
'= = = = = = = = = ==  = =

'== end form ==