
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'== Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class clsCashupPayments

    '-- Created 30-Nov-2016--
    '-  POS 3.3.3301.1130  =

    '--  To analyse cashup Payments received-
    '--    By Session, or by date..-
    '==
    '==     v3.3.3301.1212..  12-Dec-2016= ===
    '==       >> Introducing CashDrawer ID's (as per MYOB RM.).- 
    '==
    '==  v3.4.3401.0328..  28-Mar-2017===
    '==     >> 3401.327- Drop Cashout columns from payment record..
    '==     >> 3401.327- ADD EFTPOS_DR, EFTPOS_CR to Refund Options.
    '==
    '==  v3.4.3403.1109..  09-Nov-2017===
    '==     >> Update for Payment-Discounts, 
    '==            --and Account Payment Reversals...
    '==
    '==  IN PRODUCTION- 07-Feb-2019--
    '==  IN PRODUCTION- 07-Feb-2019--
    '==
    '==   Updated.- 3519.0208 08-Feb-2019= 
    '==     -- Fixes to Cashup Sessions to overcome zero payment is's in payment-no cols..-
    '==
    '==
    '==   Updated.- 3519.0217 17-Feb-2019= 
    '==     -- Fixes to Cashup/Paynents Analysis to rename current summary to "Revenue"-
    '==       and to build new summary for actual Till analysis..
    '==       and to drop "Discount on Payment" from all analyses as it is irrelevant in this context
    '==
    '==
    '==   Updated.- 3519.0218 18-Feb-2019= 
    '==     -- Fixes to Cashup/Paynents for enpty Till crash..
    '==
    '==   Updated.- 3519.0307  Started 05-March-2019= 
    '==     -- Updates to clsCashupPayments to add sub-totals to Sorted Item Details....
    '==
    '==
    '==  -- RELEASED as 3519.0501..
    '==
    '==   Updated.- 3519.0505  Started 05-May-2019= 
    '==    -- PAYMENT REPORTS-  
    '==           Add "Till Payments Analysis" report to differentiate from "Revenue Analysis" Report.
    '==    
    '=   Updated- 4221.0305=  03-Feb2020..
    '==     --  Drop CashDrawer from  ALL Tills caption..
    '==      ie THIS ok now-
    '=        s1 &= "-- Overall Totals (ALL Tills): "  & "</txt>"
    '==          BUT NOT THIS.. s1 &= "-- Overall Totals (ALL Tills): " & sCashDrawer & "</txt>"
    '= = = = = = = = = = = == == =
    '==
    '==    
    '==   New Build- == 4233.0416.  16-April-2020- 
    '==
    '==   --  Made Reports into a Child UserControl..
    '==   -- Show WaitForm while building report libes...
    '==
    '= = = = = = = = = =  = = = = = = = = = = = = = = = = = = = = == 
    '= = = = = = = = = =  = = = = = = = = = = = = = = = = = = = = == 
    '==
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251. 24-May-2020.
    '==  Target is new Build 4251. 24-May-2020.
    '== 
    '==
    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
    '==       --  Involves creating a REFUND DETAILS EXTRA Option (radioButton) to be able to refund same types as Payments..
    '==                 ie dropdown including ZipPay, bank Deposit etc..
    '==          ALSO TWO new columns to be added to Payments Table-  
    '==                      viz- "RefundOtherDetailAmount"(MONEY), and "RefundOtherDetailKey" (VARCHAR).
    '==                         to Cash/CreditNote/EftPosDr/EftPosCr already recorded in Payment record.
    '==
    '== 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '== 
    '==   Target-New-Build-4282 -- ( 22-October-2020)
    '==   Target-New-Build-4282 -- ( 22-October-2020)
    '==   Target-New-Build-4282 -- ( 22-October-2020)
    '==
    '==    New Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
    '==       CashSale REversal TRancode is "cshSaleReversal"
    '--          Include it in Reversals processing..
    '==       ALSO fix Sorted Detail totals for double negative.
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = ='= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =

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
    Private mColPayments As Collection  '-All payments Last Till Analysis.-
    Private mIntFirstPayment_id, mIntLastPayment_id As Integer

    '- List REVENUE Results of Analysis
    '=Private mColReceiptLines As Collection  '-- ie REVENUE balance..-
    '--  RENAMED-
    Private mColRevenueSummaryLines As Collection  '-- ie REVENUE balance..-

    '-- collection of amounts with payment key-
    '= Private mColTillAnalysis As Collection
    Private mColTillBalance As Collection

    '-- collection of formatted Payments (REVENUE ANALYSIS) report lines-
    Private mColReportLines As Collection

    '=3519.0505=-- collection of formatted Payments (TILL ANALYSIS) report lines-
    Private mColTillAnalysisReportLines As Collection

    Private msReportPeriod As String

    '-- collection of formatted Till Payments report lines-
    Private mColSortedDetailLines As Collection
    '-- wait form--
    Private mFormWait1 As frmWait
    Private mFrmParent As Form
    '= = = = = = = = = = = = = = = = = = = = == = 
    '-===FF->

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String)

        mFormWait1 = New frmWait
        mFormWait1.waitTitle = "POS Reports"  '=msVersionPOS
        mFormWait1.labHdr.Text = "Payments Analysis-"
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.Show(mFrmParent)
        '=mFormWait1.Show()
        DoEvents()
    End Sub '- mWaitFormOn-
    '-= = = = =  = = = = = =

    '-- kill (hide) wait form--
    Private Sub mWaitFormOff()

        mFormWait1.Hide()
        mFormWait1.Close()
        mFormWait1.Dispose()
        DoEvents()
    End Sub  '--wait--
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- set up Data1/Date2 WHERE SQL condition..-
    '-- based on the Form's  datePickers controls From/To..
    '-- format dates for SQL..-
    '=     sDate1 = VB6.Format(date1, "dd-mmm-yyyy") & " 00:00" '-min-
    '=     sDate2 = VB6.Format(date2, "dd-mmm-yyyy") & " 23:59" '--max.--


    Private Function msSetupDatesWhereCondition(ByVal strDateColumn As String, _
                                                ByVal date1 As Date, _
                                                ByVal date2 As Date, _
                                                 ByRef sWhere As String) As String
        Dim sDate1, sDate2 As String

        '-- format dates for SQL..-
        sDate1 = Format(date1, "dd-MMM-yyyy") & " 00:00"  '-min-
        sDate2 = Format(date2, "dd-MMM-yyyy") & " 23:59"  '--max.--
        If (sWhere = "") Then
            sWhere = " WHERE "
        Else
            sWhere = sWhere & " AND "
        End If
        sWhere &= " ((" & strDateColumn & ">='" & sDate1 & "') AND (" & strDateColumn & "<='" & sDate2 & "')) "

        msReportPeriod = "From: " & sDate1 & "  To: " & sDate2
        msSetupDatesWhereCondition = sWhere

    End Function  '--SetupWhereCondition-
    '= = = =  = =  == = = = = = =  = = =
    '-===FF->

    '- RESULTS of Analysis..-

    '- Last Till Results of Analysis
    '=3519.0217= -- WAS wrong and is RENAMED.
    'Public ReadOnly Property colReceiptLines As Collection
    '    Get
    '        colReceiptLines = mColReceiptLines
    '    End Get
    'End Property  '-colReceiptLines-
    '= = = = = = = = = = == = = =  =

    Public ReadOnly Property ColRevenueSummaryLines As Collection
        Get
            ColRevenueSummaryLines = mColRevenueSummaryLines
        End Get
    End Property  '-colReceiptLines-
    '= = = = = = = = = = == = = =  =



    '-- collection of amounts with payment key-
    '-- NOW renamed as TillBalance-
    'Public ReadOnly Property colTillAnalysis As Collection
    '    Get
    '        colTillAnalysis = mColTillAnalysis
    '    End Get
    'End Property  '-colReceiptLines-
    ''= = = = = = = = = = == = = =  =

    Public ReadOnly Property colTillBalance As Collection
        Get
            colTillBalance = mColTillBalance
        End Get
    End Property  '-colReceiptLines-
    '= = = = = = = = = = == = = =  =


    '-- collection of formatted Till Payments report lines-
    Public ReadOnly Property colReportLines As Collection
        Get
            colReportLines = mColReportLines
        End Get
    End Property  '-colRportLines-
    '= = = = = = = = = = = = = = = = = =

    '=3519.0505-- collection of formatted Till Payments report lines-
    Public ReadOnly Property colTillAnalysisReportLines As Collection
        Get
            colTillAnalysisReportLines = mColTillAnalysisReportLines
        End Get
    End Property  '-colRportLines-
    '= = = = = = = = = = = = = = = = = =


    '-- collection of SORTED Payments DETAIL report lines-

    Public ReadOnly Property colSortedDetailLines As Collection
        Get
            colSortedDetailLines = mColSortedDetailLines
        End Get
    End Property  '-coldetaiLines-
    '= = = = = = = = = = = = =

    '==   Updated.- 3519.0208 08-Feb-2019= 
    '--   Return the Payment_id limits..
    Public ReadOnly Property resultFirstPaymentId As Integer
        Get
            resultFirstPaymentId = mIntFirstPayment_id
        End Get
    End Property  '-first-
    '= = = = = = = = = = = = = = = = =

    Public ReadOnly Property resultLastPaymentId As Integer
        Get
            resultLastPaymentId = mIntLastPayment_id
        End Get
    End Property  '-last-
    '= = = = = = = = = = = = = = = = = = = = = = = = = == = =

    '-===FF->

    '-- Constructor- I n i t ----

    Public Sub New(ByRef cnn1 As OleDbConnection, _
                     ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                        Optional ByRef frmParent As Form = Nothing)
        MyBase.New()

        mCnnSql = cnn1  '-save-
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo

        If frmParent Is Nothing Then
            mFrmParent = New Form
        Else
            mFrmParent = frmParent
        End If

    End Sub '--init..--
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Cashup Analysis method (Till Balance or Historical Session)...--
    '== = =
    '==     v3.3.3301.615..  15-Jun-2016= ===
    '==       >>  Cashup Analysis- For re-organised payments table-
    '==              (NOW HAVE "mother" payment record back now. PLUS Details as individ. payments..
    '--                 So use PAYMENT as main cashup line anchor..)
    '==   Report now based on RM  Current Session Payments..
    '==
    '--   But if dateStart and dateEnd are supplied, then
    '--    the Report is based only on all Payments between those dates (incl. midinight to midnight.)

    Public Function cashupAnalysis(ByVal strTill_id As String, _
                                     ByVal intPreviousLastPayment As Integer, _
                                      ByVal intStartingPayment As Integer, _
                                      ByVal intEndingPayment As Integer, _
                                      Optional ByVal dateStart As Date = Nothing, _
                                      Optional ByVal dateEnd As Date = Nothing) As Boolean

        '= Dim sTerminalName As String  '=, sSummaryReport As String
        Dim sCashDrawer As String  '=, sSummaryReport As String 
        Dim sSql, sWhere, s1, s2, s3, sCashout As String
        Dim datatable1, dtDisbursements As DataTable
        Dim column1 As DataGridViewColumn
        Dim colPaymentTypes, colPaymentTypeDescriptors, colPaymentIndexes As Collection
        '== Dim col1, col2, colPayments, colInvoices As Collection
        '= Dim col1, col2, colPayments As Collection
        '= Dim col1, col2 As Collection
        Dim colPayment As Collection
        Dim colDisbursements, colList As Collection
        Dim intPayment, intCashColNo As Integer
        Dim intTypeCount, intRowCount, ix, rx, intInvoice As Integer
        '= Dim gridRow1 As DataGridViewRow
        Dim decAmount, decCashoutTotal, decTotalIncoming As Decimal
        Dim decTotalSales As Decimal = 0
        '== Dim listTerminalIDs As New List(Of String)   '--all terminal names in order.-
        Dim listPaymentTypes As New List(Of String)   '--all Totals names in order.-
        Dim listModelPaymentTotals As List(Of Decimal)  '-- Model of paymnt type totals..-terminal/Grand-
        Dim listGrandTotals As New List(Of Decimal)  '-- Collects listPaymentTotals totals..-
        Dim listPaymentTotals As List(Of Decimal)  '--paymnt type totals..- Per terminal-
        '-- indexes of fixed cash/CR amounts--
        Dim intIdxChange, intIdxCashRefund, intIdxCashOut, intIdxCRS, _
                                       intIdxCRR, intIdxNettcash, intIdxRevenue As Integer
        Dim intIdxEftPosDr, intIdxEftPosCr As Integer '=3401.328=
        '=3519.0217=  we HAVE to FIX these.
        Dim intIdxEftPosDr_refund, intIdxEftPosCr_refund As Integer '=3419.0217=
        Dim intIdxDiscOnPayment As Integer '=3403.1109===

        '-- define range of totals for Cashup Reconciliation Grid.
        Dim intIdxFirstCashupItem, intIdxLastCashupItem As Integer
        '= Dim colReceiptLines As Collection
        '= Dim prtDocs1 As clsPrintSaleDocs
        Dim listSortedPaymentDetails As List(Of String)

        '==  Target is new Build 4251. 24-May-2020.
        '==  Target is new Build 4251. 24-May-2020.
        '--   Holds a collection of DISTINCT RefundOther keys..
        '--    Used to add Totals buckets for the extra refund types
        Dim colOtherRefundDetailKeys As Collection

        cashupAnalysis = False
        mColPayments = New Collection

        '= mColTillAnalysis = New Collection
        '= mColReceiptLines = New Collection

        '==  must send back a valid object.-
        mColReportLines = New Collection
        mColTillAnalysisReportLines = New Collection

        mColSortedDetailLines = New Collection
        mColRevenueSummaryLines = New Collection
        mColTillBalance = New Collection


        mIntFirstPayment_id = -1
        mIntLastPayment_id = -1
        '= btnPrintReport.Enabled = False
        '= btnPrintTillSummary.Enabled = False
        sSql = ""
        sWhere = ""
        '--GET all Payments for Selected CASH DRAWER (by date or by Paymen-id range)..-
        '-- Make a report row for each payment, with list of related invoices..
        '--  and a column to list Payment Types/amounts.-

        '-- setup CASHUP sql-
        '==sSql = msMakeSalesListSql()

        If (dateStart <> Nothing) And (dateEnd <> Nothing) Then
            '--Wants payments in date range.-
            '-- ie. ALL TILLS for Reports.. 
            sWhere = msSetupDatesWhereCondition("payments.payment_date", dateStart, dateEnd, sWhere)
        Else  '- by cash drawer.  (CashUp..)
            sWhere = " WHERE (payments.cashDrawer='" & strTill_id & "' ) "
            If (intPreviousLastPayment > 0) Then
                '-- IF reporting on Current session-
                '--  Start AFTER last payment for previous session-
                sWhere &= " AND (payments.payment_id >" & intPreviousLastPayment & ") "
            Else
                '-- IF reporting on previous session-
                If (intStartingPayment > 0) Then
                    sWhere &= " AND (payments.payment_id >=" & intStartingPayment & ") "
                End If
                If (intEndingPayment > 0) Then
                    sWhere &= " AND (payments.payment_id <=" & intEndingPayment & ") "
                End If
            End If  '-previous-
        End If  '-date nothing-
        '== sWhere = msReportSetupWhereCondition("payments.payment_date", sWhere)

        sSql = "SELECT payments.payment_id, payments.terminal_id, payments.cashDrawer, payments.customer_id, "
        sSql &= "  payments.payment_date, payments.transactionType AS tr_type, payments.invoice_id, "
        '== sSql &= "   payments.totalAmountReceived, payments.changeGiven, payments.cashoutGiven, "
        sSql &= "  payments.isReversal, payments.discountGivenOnPayment, "
        sSql &= "  payments.totalAmountReceived, payments.changeGiven, "
        sSql &= "  payments.nettAmountCredited, "
        sSql &= " refundCashAmount, refundAsCreditNoteCredited, "
        '-  refundAsEftPosDr
        sSql &= " refundAsEftPosDr, refundAsEftPosCr, "
        sSql &= " refundOtherDetailAmount, refundOtherDetailKey, "   '==  Target is new Build 4251. 24-May-2020.
        sSql &= "   creditNotePaymentCredited,  creditNoteAmountDebited, "
        sSql &= "    PaymentType_key,  paymentType_descr, PD.amount AS PaymentAmount, customer.barcode AS cust_barcode, "
        sSql &= "    customer.firstName + ' ' + customer.lastName AS Customer, "
        sSql &= "   staff.docket_name AS staff_docket_name "
        sSql &= "  FROM dbo.payments "
        sSql &= "   LEFT OUTER JOIN PaymentDetails AS PD on (PD.payment_id=Payments.payment_id) "
        sSql &= "   LEFT JOIN Customer on (payments.customer_id=Customer.customer_id) "
        sSql &= "   LEFT JOIN staff on (payments.staff_id=staff.staff_id) "
        sSql &= sWhere
        '= sSql &= " ORDER BY payments.cashDrawer, payments.payment_id;"
        sSql &= " ORDER BY payments.payment_id;"

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '-- get the recordset (datatable)..
        If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Error in getting recordset for Payments table: " & vbCrLf & _
                                            gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If Not (datatable1 Is Nothing) AndAlso (datatable1.Rows.Count > 0) Then
                Call mWaitFormOn("Wait..  Checking Payments..")
                '-- make a list of distinct payments and payment types found...
                colPaymentTypes = New Collection
                intTypeCount = 0  '= 1   '--counts "PaymentDetails" payment types..-
                '= colInvoices = New Collection
                '= colPayments = New Collection
                colPaymentTypeDescriptors = New Collection
                '== colColumnNos = New Collection
                colPaymentIndexes = New Collection  '--track indexes for type totals.

                '==  Target is new Build 4251. 24-May-2020.
                '==  Target is new Build 4251. 24-May-2020.
                '--   Holds a collection of DISTINCT RefundOther keys..
                '--    Used to add Totals buckets for the extra refund types
                colOtherRefundDetailKeys = New Collection

                '-- make a list (sorted) of ALL payments detail items for sorted report found...
                listSortedPaymentDetails = New List(Of String)

                '-- NOW- Start/Build Model totals list..
                listModelPaymentTotals = New List(Of Decimal)
                listPaymentTypes = New List(Of String)

                '-- ALWAYS have FIRST spot FIXED for Cash (in)..
                colPaymentTypes.Add("Cash", "cash")  '--- always have a cash column (First col).-
                '== colPaymentTypes.Add(s1, s1)  '-key and data.
                colPaymentTypeDescriptors.Add("Cash in")
                intTypeCount += 1  '-- this will be the column order..
                colPaymentIndexes.Add(intTypeCount - 1, "cash")  '-same order and key as type names.
                '- make Tptals spot for Cash-in-
                listModelPaymentTotals.Add(0) '--add zero total to make type list..
                listPaymentTypes.Add("cash")

                '-- NOW make Cash+ places for Change, CashRefund, Cashout..
                '-- SO they can follow Cash-in..
                '- and Save intIdxFirstCashupItem, intIdxLastCashupItem for Cashup-
                Dim ixx As Integer = intTypeCount  '-next after type list-
                listModelPaymentTotals.Add(0) '--add zero total for Change..
                listPaymentTypes.Add("Cash Change Given")
                intIdxChange = ixx
                ixx += 1
                listModelPaymentTotals.Add(0) '--add zero total for CashRefunds..
                listPaymentTypes.Add("Cash Refund")
                intIdxCashRefund = ixx
                ixx += 1
                'listModelPaymentTotals.Add(0) '--add zero total for Cash Outs..
                'listPaymentTypes.Add("Cash Out")
                'intIdxCashOut = ixx
                'ixx += 1
                listModelPaymentTotals.Add(0) '--add zero total for Computed revenue..
                listPaymentTypes.Add("== Nett Cash Rcvd")
                intIdxNettcash = ixx
                intIdxFirstCashupItem = ixx  '-only cash amt reported to Casup Grid.
                ixx += 1

                '=3519.0217=  we HAVE to FIX these.
                '=3519.0217=  we HAVE to FIX these.
                listModelPaymentTotals.Add(0) '--add zero total for EftPOs..
                listPaymentTypes.Add("EftPos_Dr")
                colPaymentIndexes.Add(ixx, "EftPos_Dr")  '-same order and key as type names.
                intIdxEftPosDr = ixx
                ixx += 1
                '=3519.0217=  we HAVE to FIX these.
                listModelPaymentTotals.Add(0) '--add zero total for EftPOs..
                listPaymentTypes.Add("EftPos_Cr")
                colPaymentIndexes.Add(ixx, "EftPos_Cr")  '-same order and key as type names.
                intIdxEftPosCr = ixx
                ixx += 1
                '=3519.0217=  we HAVE to FIX these.
                listModelPaymentTotals.Add(0) '--add zero total for EftPOs..
                listPaymentTypes.Add("EftPosDr Refund")
                intIdxEftPosDr_refund = ixx
                ixx += 1
                '=3519.0217=  we HAVE to FIX these.
                listModelPaymentTotals.Add(0) '--add zero total for EftPOs..
                listPaymentTypes.Add("EftPosCr Refund")
                intIdxEftPosCr_refund = ixx
                ixx += 1
                '--  done first part of fixed stuff.

                '-- NOW- ADD to the a list the distinct payments and REGULAR payment types found...
                Dim sPaymentType_key As String
                Dim decRefundAmt As Decimal
                Dim strRefundOtherKey As String
                For Each row1 As DataRow In datatable1.Rows
                    If Not IsDBNull(row1.Item("PaymentType_key")) Then  '-- May not be any detail records.-
                        sPaymentType_key = row1.Item("PaymentType_key")
                        '-ignore cash items for this ID pass-
                        '--  IGNORE EFTPOS also, as we already have them.
                        If (Not ((InStr(LCase(sPaymentType_key), "cash")) > 0)) And _
                                   (LCase(sPaymentType_key) <> "eftpos_dr") And _
                                         (LCase(sPaymentType_key) <> "eftpos_cr") Then
                            If Not colPaymentTypes.Contains(sPaymentType_key) Then
                                '--  add this type..  cash will be ignored here. (spot alreday allocated).
                                '= s1 = row1.Item("PaymentType_key")
                                intTypeCount += 1  '-- this will be the column order..
                                colPaymentTypes.Add(sPaymentType_key, sPaymentType_key)  '-key and data.
                                colPaymentTypeDescriptors.Add(row1.Item("PaymentType_descr"))
                                '== colPaymentIndexes.Add(intTypeCount - 1, sPaymentType_key)  '-same order and key as type names.
                                colPaymentIndexes.Add(ixx, sPaymentType_key)  '-same order and key as type names.
                                '-- Add a totals bucket-
                                listModelPaymentTotals.Add(0) '--add zero total for Computed revenue..
                                listPaymentTypes.Add(sPaymentType_key)
                                ixx += 1
                            End If  '--contains..-
                        End If  '-cash-
                    End If '-nul-

                    '-- collect DISTINCT payment ID's -
                    If Not mColPayments.Contains(row1.Item("payment_id")) Then
                        '- save first/last- payment ids-
                        intPayment = row1.Item("payment_id")
                        If (mIntFirstPayment_id = -1) Then
                            mIntFirstPayment_id = intPayment   '-will only do it once-
                        End If
                        '--keep track of HIGHEST payment id-THIS TILL-
                        If (intPayment > mIntLastPayment_id) Then
                            mIntLastPayment_id = intPayment   '--keep track of HIGHEST payment id-THIS TILL-
                        End If
                        '-- save INVOICE info in collection..
                        colPayment = New Collection
                        ix = row1.Item("payment_id")
                        colPayment.Add(ix, "Id")
                        colPayment.Add(LCase(row1.Item("tr_type")), "tr_type")
                        colPayment.Add(row1.Item("payment_date"), "date")  '-save payment id.
                        colPayment.Add(row1.Item("isReversal"), "isReversal")  '-account payment reversal.
                        colPayment.Add(row1.Item("invoice_id"), "invoice_id")  '-save invoice_id this payment.
                        colPayment.Add(row1.Item("Customer"), "Customer")  '-save customer this payment.
                        colPayment.Add(row1.Item("Cust_barcode"), "Cust_barcode")  '-save customer this payment.
                        colPayment.Add(row1.Item("staff_docket_name"), "staff_docket_name")  '-save staff this payment.
                        colPayment.Add(row1.Item("terminal_id"), "terminal_id")  '-save terminal_id.
                        colPayment.Add(row1.Item("cashDrawer"), "cashDrawer")  '-save till_id.
                        '-discountGivenOnPayment-
                        colPayment.Add(row1.Item("discountGivenOnPayment"), "discountGivenOnPayment")  '-DISC. thispayment.
                        colPayment.Add(row1.Item("totalAmountReceived"), "totalAmountReceived")  '-save thispayment.
                        colPayment.Add(row1.Item("changeGiven"), "changeGiven")
                        '= colPayment.Add(row1.Item("cashoutGiven"), "cashoutGiven")
                        colPayment.Add(row1.Item("nettAmountCredited"), "nettAmountCredited")
                        colPayment.Add(row1.Item("refundCashAmount"), "refundCashAmount")
                        colPayment.Add(row1.Item("refundAsCreditNoteCredited"), "refundAsCreditNoteCredited")
                        '- refund as eftPos-
                        colPayment.Add(row1.Item("refundAsEftPosDr"), "refundAsEftPosDr")
                        colPayment.Add(row1.Item("refundAsEftPosCr"), "refundAsEftPosCr")

                        '==  Target is new Build 4251. 24-May-2020.
                        '==  Target is new Build 4251. 24-May-2020.
                        '--  colOtherRefundDetailKeys Holds a collection of DISTINCT RefundOther keys..
                        '--    Used to add Totals buckets for the extra refund types
                        decRefundAmt = CDec(row1.Item("refundOtherDetailAmount"))
                        strRefundOtherKey = Trim(row1.Item("refundOtherDetailKey"))
                        If (decRefundAmt <> 0) And (strRefundOtherKey <> "") Then
                            '- save key if not seen before.
                            If Not colOtherRefundDetailKeys.Contains(strRefundOtherKey) Then
                                colOtherRefundDetailKeys.Add(strRefundOtherKey, strRefundOtherKey)
                            End If
                        End If
                        colPayment.Add(decRefundAmt, "refundOtherDetailAmount")
                        colPayment.Add(strRefundOtherKey, "refundOtherDetailKey")
                        '-- done extra refund.

                        colPayment.Add(row1.Item("creditNotePaymentCredited"), "creditNotePaymentCredited")
                        colPayment.Add(row1.Item("creditNoteAmountDebited"), "creditNoteAmountDebited")
                        mColPayments.Add(colPayment, ix)
                    End If
                Next row1

                '-- we are creating a list of zero-bal type totals for Types Discovered..

                '==  Target is new Build 4251. 24-May-2020.
                '==  Target is new Build 4251. 24-May-2020.
                '--  colOtherRefundDetailKeys Holds a collection of DISTINCT RefundOther keys..
                '--    Used to add Totals buckets for the extra refund types
                '---  ADD Totals for EXtra (Other) REfund Types disc0vered..
                For Each sNewKey As String In colOtherRefundDetailKeys
                    s1 = sNewKey & "_Refund"
                    listModelPaymentTotals.Add(0) '--add zero total for Each Discovered OTHER Refund TYpe..
                    listPaymentTypes.Add(s1)
                    colPaymentIndexes.Add(ixx, s1)  '-same order and key as type names.
                    '= intIdxEftPosDr = ixx
                    ixx += 1
                Next sNewKey
                '-- Done other REfund Keys.

                ''-- add totals for Amounts in Payment Record.
                '-- indexes of fixed cash/CR amounts--
                '== Dim intIdxChange, intIdxCashRefund, intIdxCashOut, 
                '--                        intIdxCRS, intIdxCRR, intIdxNettcash, intIdxRevenue As Integer
                '-save end of cashup grid item reange-
                intIdxLastCashupItem = ixx

                '-- Make places for non-cash EXTRAS..

                '=3403.1109= -discountGivenOnPayment-
                '-- intIdxDiscOnPayment --
                '==   Updated.- 3519.0217 17-Feb-2019= 
                '==   Updated.- 3519.0217 17-Feb-2019= 
                '==   DROP "Discount on Payment" from all analyses as it is irrelevant in this context
                '==   DROP "Discount on Payment" from all analyses as it is irrelevant in this context
                'listModelPaymentTotals.Add(0) '--add zero total for EftPosDr Refund..
                'listPaymentTypes.Add("Discount on Paymnt")
                'intIdxDiscOnPayment = ixx
                'ixx += 1

                '=3401.328= EftPos REFUNDS-
                '= intIdxEftPosDr, intIdxEftPosCr As Integer '=3401.328=
                '-- DONE-  SEE ABOVE--
                '-- DONE-  SEE ABOVE--

                'listModelPaymentTotals.Add(0) '--add zero total for EftPosDr Refund..
                'listPaymentTypes.Add("EftPosDr Refund")
                'intIdxEftPosDr = ixx
                'ixx += 1
                'listModelPaymentTotals.Add(0) '--add zero total for EftPosCr Refund..
                'listPaymentTypes.Add("EftPosCr Refund")
                'intIdxEftPosCr = ixx
                'ixx += 1
                '--------
                listModelPaymentTotals.Add(0) '--add zero total for CreditNotes Saved..
                listPaymentTypes.Add("CreditNote Saved")
                intIdxCRS = ixx
                ixx += 1
                listModelPaymentTotals.Add(0) '--add zero total for CreditNotes Redeemed..
                listPaymentTypes.Add("CreditNote(s) Redeemed")
                intIdxCRR = ixx
                ixx += 1
                listModelPaymentTotals.Add(0) '--add zero total for Computed revenue..
                listPaymentTypes.Add("= Net Revenue")
                intIdxRevenue = ixx
                '= ixx += 1

                '-- initialise Grand Totals..
                listGrandTotals = listModelPaymentTotals

                '- Build grid showing payment detail in one column..--
                '- There will be one row for each invoice.
                '== NO MORE GRID= Dim cellSample As New DataGridViewTextBoxCell

                Const WIDTH_DESCR As Short = 24
                Const WIDTH_AMT As Short = 10
                '= sTerminalName = ""  '--to start-
                sCashDrawer = strTill_id  '==""
                '- For each Payment-  add a grid row, 
                '-   and find the payment details for the Payment line and put them in grid.
                Dim bIsRefund, bIsReversal As Boolean
                Dim intReversingFactor As Int16
                Dim decThisPayAmount, decTotal, decRevenue, decNettCashIn As Decimal
                Dim decTillSubTotal As Decimal  '=3519.0505..
                Dim intHeight, intPaymentRows As Integer
                Dim sTargetInvoices As String
                intRowCount = 0
                '=colReceiptLines = New Collection
                mColRevenueSummaryLines = New Collection
                '==   Updated.- 3519.0217 17-Feb-2019= 
                '==     -- Fixes to Cashup/Paynents Analysis to rename current summary to "Revenue"-
                mColRevenueSummaryLines.Add("== REVENUE Summary of Payments (REVENUE) ==" & vbCrLf & _
                                            "== REVENUE Summary of Payments (REVENUE) ==" & vbCrLf & vbCrLf)
                mColRevenueSummaryLines.Add("")
                mColRevenueSummaryLines.Add("  For Till- " & sCashDrawer & ". to " & Format(Now, "ddd dd-MMM-yyyy hh:mm tt") & vbCrLf)
                mColRevenueSummaryLines.Add("")
                '-- First Create and initialise Section Totals to Zero..
                listPaymentTotals = New List(Of Decimal)
                For ix = 0 To listModelPaymentTotals.Count - 1
                    listPaymentTotals.Add(0) '=listModelPaymentTotals(ix)
                Next ix
                '-  define tab columns- (ofsets from our marg)-
                Const k_TAB_DATE As Integer = 0
                Const k_TAB_TILL As Integer = 130
                Const k_WIDTH_TILL As Integer = 60
                Const k_TAB_DESCR As Integer = 240
                Const k_TAB_AMOUNT As Integer = 400
                '= Const k_WIDTH_DESCR As Short = 24
                Const k_WIDTH_AMT As Short = 80
                Const k_TAB_TRANCODE As Integer = 500
                Const k_TAB_CUST As Integer = 520
                Const k_TAB_STAFF As Integer = 650

                Const k_SortHdrSize As Integer = 20

                Dim sTrancode, sDate, sPaymntDate, sCust, sStaff, sDescr, sAmount, sLine As String
                Dim sPaymentDetail, sPaymentDetail2, sSortedLineDetail As String
                Dim sSortedListItem, sPid As String

                '-- Now show all payments with details..
                mFormWait1.labMsg.Text = "Wait..  Building Report Lines.."
                '-- Now show all payments with details..
                For Each colPayment In mColPayments  '=colPayments
                    intPaymentRows = 1  '-- for line height..
                    decRevenue = 0
                    decTillSubTotal = 0
                    decNettCashIn = 0
                    intPayment = colPayment.Item("id")
                    '==bIsRefund = (colPayment.Item("tr_type") = "refund")
                    sCashDrawer = colPayment.Item("cashDrawer")
                    bIsRefund = (UCase(colPayment.Item("tr_type")) = "REFUND")
                    '= 3403.1109- Account Payment Reversal-

                    '==   Target-New-Build-4282 -- (Started 12-October-2020)
                    '==
                    '==    New Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
                    '==         Done by cloning clsAccountReversal and moddying and adding Payment Reversal stuff..
                    '==       CashSale REversal TRancode is "cshSaleReversal"
                    '=
                    '= bIsReversal = (colPayment.Item("IsReversal") <> 0) And
                    '=                      (UCase(colPayment.Item("tr_type")) = "ACCOUNT")
                    bIsReversal = (colPayment.Item("IsReversal") <> 0) And
                                         ((UCase(colPayment.Item("tr_type")) = "ACCOUNT") Or
                                                    (LCase(colPayment.Item("tr_type")) = "cshsalereversal"))
                    '==  END Target-New-Build-4282 -- (Started 12-October-2020)


                    intReversingFactor = 1  '-normal-
                    If bIsReversal Then
                        intReversingFactor = -1  '- mke payment negative.
                    End If
                    '-- Check if we are seeing a new TILL--
                    '- NO MORE-
                    sPid = " P/No: " & RSet(CStr(intPayment), 6)
                    '-- ok. Add a row to the grid for this invoice (payment)..
                    '-- load payment into grid row.
                    sTrancode = Trim(UCase(colPayment.Item("tr_type")))
                    If (sTrancode = "ACCOUNT") And bIsReversal Then
                        sTrancode = "A/C (Reversal)"
                    End If
                    '= sTrancode &= IIf(bIsReversal, " (Reversal)", "")
                    sTrancode &= "; #" & colPayment.Item("invoice_id")
                    'If s1 = "ACCOUNTPAYMENT" Then
                    '    s1 = "A/C-Pmnt"
                    'End If
                    sDate = Format(CDate(colPayment.Item("date")), "dd-MMM-yy HH:mm")
                    sPaymntDate = RSet(CStr(intPayment), 6) & ".  " & Format(CDate(colPayment.Item("date")), "dd-MMM-yy HH:mm")
                    '= s1 = col1.Item("staff_docket_name") & vbCrLf & _
                    '=      col1.Item("Customer") & " [" & col1.Item("Cust_barcode") & "]"
                    sCust = colPayment.Item("Customer") & " [" & colPayment.Item("Cust_barcode") & "]"
                    sStaff = colPayment.Item("staff_docket_name")

                    sPaymentDetail = "<txt TAB=""" & k_TAB_DATE & """ fontStyle=""bold"" >"
                    sPaymentDetail &= sPaymntDate & "</txt>"
                    sPaymentDetail &= "<txt TAB=""" & k_TAB_TILL & """ fontStyle=""bold"" >"
                    sPaymentDetail &= "Till: " & sCashDrawer & "</txt>"
                    sPaymentDetail &= "<txt TAB=""" & k_TAB_TRANCODE & """ fontStyle=""bold"" >"
                    sPaymentDetail &= sTrancode & "</txt>"
                    'sPaymentDetail &= "<txt TAB=""" & k_TAB_CUST & """ >"
                    'sPaymentDetail &= sCust & "</txt>"
                    sPaymentDetail &= "<txt TAB=""" & k_TAB_STAFF & """ >"
                    sPaymentDetail &= sStaff & "</txt>"
                    '-- customer now on second line-
                    sPaymentDetail2 = "<txt TAB=""" & (k_TAB_DATE + 30) & """ >"
                    sPaymentDetail2 &= sCust & "</txt>"
                    '- for sorted report-
                    sSortedLineDetail = ";  " & sDate & "; " & VB.Left(sCust, 16) & "; " & sTrancode
                    '- NOW Read through datatable of payment details, and collect for each payment..
                    '-- (ie Search all payment detail rows for details of receipts for this payment.)
                    rx = 0
                    sTargetInvoices = ""  '= "Target Invoices : "
                    For Each row1 As DataRow In datatable1.Rows
                        If intPayment = row1.Item("payment_id") And _
                                         (Not IsDBNull(row1.Item("PaymentType_key"))) Then
                            '-- get payment type, amount-  Ignore "account" debits.--
                            s1 = row1.Item("PaymentType_key")
                            ix = colPaymentIndexes.Item(s1)  '= colColumnNos.Item(s1)    '-get col no.
                            decThisPayAmount = CDec(row1.Item("PaymentAmount"))
                            decThisPayAmount *= intReversingFactor

                            sAmount = FormatCurrency(decThisPayAmount, 2)
                            '-- cash HERE is always CASH-IN..
                            If (InStr(LCase(s1), "cash") > 0) Then
                                decNettCashIn += decThisPayAmount
                                If (LCase(s1) = "cash") Then
                                    s1 = "Cash-In"  '--fix-
                                End If
                            End If
                            sDescr = s1
                            'dgvReport.Rows(intRowCount).Cells("detail_amount").Value &= _
                            '                      LSet(s1, WIDTH_DESCR) & RSet(s2, WIDTH_AMT) & vbCrLf
                            listPaymentTotals(ix) += decThisPayAmount
                            decRevenue += decThisPayAmount
                            decTillSubTotal += decThisPayAmount

                            intPaymentRows += 1

                            '- Print the Payment Detail Line. Include Trancode if first detail.
                            sLine = "<textline>"
                            If (rx = 0) Then  '-first detail.-
                                sLine &= sPaymentDetail
                                sPaymentDetail = ""  '-- NOw is done.-
                            ElseIf (rx > 0) And (sPaymentDetail2 <> "") Then
                                sLine &= sPaymentDetail2
                                sPaymentDetail2 = ""  '-- NOw is done.-
                            End If
                            '- Descr Amt for each Detail-
                            sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >"
                            sLine &= sDescr & "</txt>"
                            '-Amount-
                            sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                            sLine &= " width = """ & k_WIDTH_AMT & """  >" & sAmount & "</txt>"
                            sLine &= "</textline>"
                            mColReportLines.Add(sLine)
                            '--Till as well..
                            mColTillAnalysisReportLines.Add(sLine)
                            rx += 1
                            '- add line to sorted detail list for sorted detail report-
                            sSortedListItem = LSet(sDescr, k_SortHdrSize) & sPid & RSet(sAmount, 13) & sSortedLineDetail

                            '=3519.0307=  Tack the amount on the end so we can retrieve it for sub-totals.
                            sSortedListItem &= vbTab & vbTab & Replace(sAmount, "$", "")  '- and strip the $ sign.-
                            listSortedPaymentDetails.Add(sSortedListItem)
                        End If
                    Next row1
                    ''-- Print Main  payment info if not detail lines yet...
                    If (sPaymentDetail <> "") Then
                        sLine = "<textline>" & sPaymentDetail & "</textline>"
                        mColReportLines.Add(sLine)
                        mColTillAnalysisReportLines.Add(sLine)
                    End If
                    'If (sPaymentDetail2 <> "") Then
                    '    sLine = "<textline>" & sPaymentDetail2 & "</textline>"
                    '    mColReportLines.Add(sLine)
                    'End If
                    '-- if this payment has Change/CASHOUT etc, add to type/amt list.--
                    '-- Show/add Amounts in Payment Record.

                    '=3403.1109= -discountGivenOnPayment-
                    '-- intIdxDiscOnPayment --
                    '==   Updated.- 3519.0217 17-Feb-2019= 
                    '==   DROP "Discount on Payment" from all analyses as it is irrelevant in this context
                    '==   DROP "Discount on Payment" from all analyses as it is irrelevant in this context
                    '==   DROP "Discount on Payment" from all analyses as it is irrelevant in this context
                    'decThisPayAmount = CDec(colPayment.Item("discountGivenOnPayment"))
                    'If (decThisPayAmount > 0) Then
                    '    decThisPayAmount *= intReversingFactor
                    '    s1 = listPaymentTypes(intIdxDiscOnPayment)
                    '    s2 = "-" & FormatCurrency(decThisPayAmount, 2)
                    '    sLine = "<textline>"
                    '    If (sPaymentDetail2 <> "") Then  '-show customer-
                    '        sLine &= sPaymentDetail2
                    '        sPaymentDetail2 = ""  '-done-
                    '    End If
                    '    '- Descr Amt for each Detail-
                    '    sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >" & s1 & "</txt>"
                    '    '-Amount-
                    '    sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                    '    sLine &= " width = """ & k_WIDTH_AMT & """  >" & s2 & "</txt>"
                    '    sLine &= "</textline>"
                    '    mColReportLines.Add(sLine)
                    '    listPaymentTotals(intIdxDiscOnPayment) -= decThisPayAmount  '- comes off revenue-
                    '    decNettCashIn -= decThisPayAmount
                    '    decRevenue -= decThisPayAmount
                    '    intPaymentRows += 1
                    '    '- add line to sorted detail list for sorted detail report-
                    '    sSortedListItem = LSet(s1, k_SortHdrSize) & sPid & RSet(s2, 13) & sSortedLineDetail
                    '    listSortedPaymentDetails.Add(sSortedListItem)
                    'End If  '->0-

                    '-- EftPos Refunds-
                    If bIsRefund Then  '-- (can't be reversal-.)
                        '= intIdxEftPosDr, intIdxEftPosCr--
                        decThisPayAmount = CDec(colPayment.Item("refundAsEftPosDr"))
                        If (decThisPayAmount > 0) Then
                            s1 = listPaymentTypes(intIdxEftPosDr_refund)
                            s2 = "-" & FormatCurrency(decThisPayAmount, 2)
                            sAmount = s2
                            sLine = "<textline>"
                            '- Descr Amt for each Detail-
                            If (sPaymentDetail2 <> "") Then  '-show customer-
                                sLine &= sPaymentDetail2
                                sPaymentDetail2 = ""  '-done-
                            End If
                            sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >" & s1 & "</txt>"
                            '-Amount-
                            sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                            sLine &= " width = """ & k_WIDTH_AMT & """  >" & s2 & "</txt>"
                            sLine &= "</textline>"
                            mColReportLines.Add(sLine)
                            mColTillAnalysisReportLines.Add(sLine)
                            listPaymentTotals(intIdxEftPosDr_refund) -= decThisPayAmount  '- comes off revenue-
                            '=decNettCashIn -= decThisPayAmount
                            decRevenue -= decThisPayAmount
                            decTillSubTotal -= decThisPayAmount
                            intPaymentRows += 1
                            '- add line to sorted detail list for sorted detail report-
                            sSortedListItem = LSet(s1, k_SortHdrSize) & sPid & RSet(s2, 13) & sSortedLineDetail
                            '=3519.0307=  Tack the amount on the end so we can retrieve it for sub-totals.
                            sSortedListItem &= vbTab & vbTab & Replace(sAmount, "$", "")  '- and strip the $ sign.-

                            listSortedPaymentDetails.Add(sSortedListItem)
                        End If '-EftPos-
                        decThisPayAmount = CDec(colPayment.Item("refundAsEftPosCr"))
                        If (decThisPayAmount > 0) Then
                            s1 = listPaymentTypes(intIdxEftPosCr_refund)
                            s2 = "-" & FormatCurrency(decThisPayAmount, 2)
                            sAmount = s2
                            sLine = "<textline>"
                            If (sPaymentDetail2 <> "") Then  '-show customer-
                                sLine &= sPaymentDetail2
                                sPaymentDetail2 = ""  '-done-
                            End If
                            '- Descr Amt for each Detail-
                            sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >" & s1 & "</txt>"
                            '-Amount-
                            sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                            sLine &= " width = """ & k_WIDTH_AMT & """  >" & s2 & "</txt>"
                            sLine &= "</textline>"
                            mColReportLines.Add(sLine)
                            mColTillAnalysisReportLines.Add(sLine)
                            listPaymentTotals(intIdxEftPosCr_refund) -= decThisPayAmount  '- comes off revenue-
                            '=decNettCashIn -= decThisPayAmount
                            decRevenue -= decThisPayAmount
                            decTillSubTotal -= decThisPayAmount

                            intPaymentRows += 1
                            '- add line to sorted detail list for sorted detail report-
                            sSortedListItem = LSet(s1, k_SortHdrSize) & sPid & RSet(s2, 13) & sSortedLineDetail

                            '=3519.0307=  Tack the amount on the end so we can retrieve it for sub-totals.
                            sSortedListItem &= vbTab & vbTab & Replace(sAmount, "$", "")  '- and strip the $ sign.-
                            listSortedPaymentDetails.Add(sSortedListItem)
                        End If '-EftPos-

                        '==  Target is new Build 4251. 24-May-2020.
                        '==  Target is new Build 4251. 24-May-2020.
                        '---  UPDATE Totals for EXtra (Other) REfund Types discovered..
                        decThisPayAmount = CDec(colPayment.Item("refundOtherDetailAmount"))
                        strRefundOtherKey = colPayment.Item("refundOtherDetailKey")
                        If (decThisPayAmount > 0) And (strRefundOtherKey <> "") Then
                            s1 = strRefundOtherKey & "_Refund"
                            ix = colPaymentIndexes.Item(s1)     '-get total no.
                            s2 = "-" & FormatCurrency(decThisPayAmount, 2)
                            sAmount = s2
                            sLine = "<textline>"
                            If (sPaymentDetail2 <> "") Then  '-show customer-
                                sLine &= sPaymentDetail2
                                sPaymentDetail2 = ""  '-done-
                            End If
                            '- Descr Amt for each Detail-
                            sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >" & s1 & "</txt>"
                            '-Amount-
                            sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                            sLine &= " width = """ & k_WIDTH_AMT & """  >" & s2 & "</txt>"
                            sLine &= "</textline>"
                            mColReportLines.Add(sLine)
                            mColTillAnalysisReportLines.Add(sLine)
                            listPaymentTotals(ix) -= decThisPayAmount  '- comes off revenue-
                            decRevenue -= decThisPayAmount
                            decTillSubTotal -= decThisPayAmount
                            intPaymentRows += 1
                            '- add line to sorted detail list for sorted detail report-
                            sSortedListItem = LSet(s1, k_SortHdrSize) & sPid & RSet(s2, 13) & sSortedLineDetail

                            '=3519.0307=  Tack the amount on the end so we can retrieve it for sub-totals.
                            sSortedListItem &= vbTab & vbTab & Replace(sAmount, "$", "")  '- and strip the $ sign.-
                            listSortedPaymentDetails.Add(sSortedListItem)
                        End If  '-REFUND other amount-
                    End If '--refund-

                    '- Change -
                    decThisPayAmount = CDec(colPayment.Item("changeGiven"))
                    If (decThisPayAmount > 0) Then
                        decThisPayAmount *= intReversingFactor
                        s1 = listPaymentTypes(intIdxChange)
                        s2 = "-" & FormatCurrency(decThisPayAmount, 2)
                        sAmount = s2
                         sLine = "<textline>"
                        If (sPaymentDetail2 <> "") Then  '-show customer-
                            sLine &= sPaymentDetail2
                            sPaymentDetail2 = ""  '-done-
                        End If
                        '- Descr Amt for each Detail-
                        sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >" & s1 & "</txt>"
                        '-Amount-
                        sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                        sLine &= " width = """ & k_WIDTH_AMT & """  >" & s2 & "</txt>"
                        sLine &= "</textline>"
                        mColReportLines.Add(sLine)
                        mColTillAnalysisReportLines.Add(sLine)

                        listPaymentTotals(intIdxChange) -= decThisPayAmount  '- comes off revenue-
                        decNettCashIn -= decThisPayAmount
                        decRevenue -= decThisPayAmount
                        decTillSubTotal -= decThisPayAmount

                        intPaymentRows += 1
                        '- add line to sorted detail list for sorted detail report-
                        sSortedListItem = LSet(s1, k_SortHdrSize) & sPid & RSet(s2, 13) & sSortedLineDetail

                        '=3519.0307=  Tack the amount on the end so we can retrieve it for sub-totals.
                        sSortedListItem &= vbTab & vbTab & Replace(sAmount, "$", "")  '- and strip the $ sign.-
                        listSortedPaymentDetails.Add(sSortedListItem)
                    End If '-change-
                    '- cash refund-
                    decThisPayAmount = CDec(colPayment.Item("refundCashAmount"))
                    If (decThisPayAmount > 0) Then
                        s1 = listPaymentTypes(intIdxCashRefund)
                        s2 = "-" & FormatCurrency(decThisPayAmount, 2)
                        sAmount = s2
                        'dgvReport.Rows(intRowCount).Cells("detail_amount").Value &= _
                        '                      LSet(s1, WIDTH_DESCR) & RSet(s2, WIDTH_AMT) & vbCrLf
                        sLine = "<textline>"
                        If (sPaymentDetail2 <> "") Then  '-show customer-
                            sLine &= sPaymentDetail2
                            sPaymentDetail2 = ""  '-done-
                        End If
                        '- Descr Amt for each Detail-
                        sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >" & s1 & "</txt>"
                        '-Amount-
                        sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                        sLine &= " width = """ & k_WIDTH_AMT & """  >" & s2 & "</txt>"
                        sLine &= "</textline>"
                        mColReportLines.Add(sLine)
                        mColTillAnalysisReportLines.Add(sLine)

                        listPaymentTotals(intIdxCashRefund) -= decThisPayAmount  '- comes off revenue-
                        decNettCashIn -= decThisPayAmount
                        decRevenue -= decThisPayAmount
                        decTillSubTotal -= decThisPayAmount

                        intPaymentRows += 1
                        '- add line to sorted detail list for sorted detail report-
                        sSortedListItem = LSet(s1, k_SortHdrSize) & sPid & RSet(s2, 13) & sSortedLineDetail

                        '=3519.0307=  Tack the amount on the end so we can retrieve it for sub-totals.
                        sSortedListItem &= vbTab & vbTab & Replace(sAmount, "$", "")  '- and strip the $ sign.-
                        listSortedPaymentDetails.Add(sSortedListItem)
                    End If '-change-

                    '-- cashout..
                    'decThisPayAmount = CDec(colPayment.Item("cashoutGiven"))
                    'If (decThisPayAmount > 0) Then
                    '    s1 = listPaymentTypes(intIdxCashOut)
                    '    s2 = "-" & FormatCurrency(decThisPayAmount, 2)
                    '    'dgvReport.Rows(intRowCount).Cells("detail_amount").Value &= _
                    '    '                      LSet(s1, WIDTH_DESCR) & RSet(s2, WIDTH_AMT) & vbCrLf
                    '    sLine = "<textline>"
                    '    '- Descr Amt for each Detail-
                    '    sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >" & s1 & "</txt>"
                    '    '-Amount-
                    '    sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                    '    sLine &= " width = """ & k_WIDTH_AMT & """  >" & s2 & "</txt>"
                    '    sLine &= "</textline>"
                    '    mColReportLines.Add(sLine)

                    '    listPaymentTotals(intIdxCashOut) -= decThisPayAmount  '- comes off revenue-
                    '    decNettCashIn -= decThisPayAmount
                    '    decRevenue -= decThisPayAmount
                    '    intPaymentRows += 1
                    'End If '-cashout

                    '- SHOW decNettCashIn - For Till Balance-
                    s1 = "= Nett Cash Rcvd:"
                    s2 = FormatCurrency(decNettCashIn, 2)
                    'dgvReport.Rows(intRowCount).Cells("detail_amount").Value &= _
                    '                      LSet(s1, WIDTH_DESCR) & RSet(s2, WIDTH_AMT) & vbCrLf
                    listPaymentTotals(intIdxNettcash) += decNettCashIn
                    intPaymentRows += 1

                    '-- Credit Note Saved-..
                    decThisPayAmount = CDec(colPayment.Item("creditNotePaymentCredited")) + _
                                            CDec(colPayment.Item("refundAsCreditNoteCredited"))
                    If (decThisPayAmount > 0) Then
                        decThisPayAmount *= intReversingFactor
                        s1 = listPaymentTypes(intIdxCRS)
                        s2 = "-" & FormatCurrency(decThisPayAmount, 2)
                        sAmount = s2
                        'dgvReport.Rows(intRowCount).Cells("detail_amount").Value &= _
                        '                      LSet(s1, WIDTH_DESCR) & RSet(s2, WIDTH_AMT) & vbCrLf
                        sLine = "<textline>"
                        If (sPaymentDetail2 <> "") Then  '-show customer-
                            sLine &= sPaymentDetail2
                            sPaymentDetail2 = ""  '-done-
                        End If
                        '- Descr Amt for each Detail-
                        sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >" & s1 & "</txt>"
                        '-Amount-
                        sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                        sLine &= " width = """ & k_WIDTH_AMT & """  >" & s2 & "</txt>"
                        sLine &= "</textline>"
                        mColReportLines.Add(sLine)
                        '-- NO Till report for Credit Note movements..

                        listPaymentTotals(intIdxCRS) -= decThisPayAmount '- comes off revenue-
                        decRevenue -= decThisPayAmount
                        '-- NO Till change for Credit Note movements..
                        intPaymentRows += 1
                        '- add line to sorted detail list for sorted detail report-
                        sSortedListItem = LSet(s1, k_SortHdrSize) & sPid & RSet(s2, 13) & sSortedLineDetail

                        '=3519.0307=  Tack the amount on the end so we can retrieve it for sub-totals.
                        sSortedListItem &= vbTab & vbTab & Replace(sAmount, "$", "")  '- and strip the $ sign.-
                        listSortedPaymentDetails.Add(sSortedListItem)
                    End If '-creditNote

                    '-- Credit Note Redeemed..
                    decThisPayAmount = CDec(colPayment.Item("creditNoteAmountDebited"))
                    If (decThisPayAmount > 0) Then
                        decThisPayAmount *= intReversingFactor
                        s1 = listPaymentTypes(intIdxCRR)
                        s2 = FormatCurrency(decThisPayAmount, 2)
                        sAmount = s2
                        'dgvReport.Rows(intRowCount).Cells("detail_amount").Value &= _
                        '                      LSet(s1, WIDTH_DESCR) & RSet(s2, WIDTH_AMT) & vbCrLf
                        sLine = "<textline>"
                        If (sPaymentDetail2 <> "") Then  '-show customer-
                            sLine &= sPaymentDetail2
                            sPaymentDetail2 = ""  '-done-
                        End If
                        '- Descr Amt for each Detail-
                        sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >" & s1 & "</txt>"
                        '-Amount-
                        sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                        sLine &= " width = """ & k_WIDTH_AMT & """  >" & s2 & "</txt>"
                        sLine &= "</textline>"
                        mColReportLines.Add(sLine)
                        '= NO TILL report for Credit Note movements.

                        listPaymentTotals(intIdxCRR) += decThisPayAmount
                        decRevenue += decThisPayAmount
                        '-- NO Till changes for Credit Note movements..
                        intPaymentRows += 1
                        '- add line to sorted detail list for sorted detail report-
                        sSortedListItem = LSet(s1, k_SortHdrSize) & sPid & RSet(s2, 13) & sSortedLineDetail

                        '=3519.0307=  Tack the amount on the end so we can retrieve it for sub-totals.
                        sSortedListItem &= vbTab & vbTab & Replace(sAmount, "$", "")  '- and strip the $ sign.-
                        listSortedPaymentDetails.Add(sSortedListItem)
                    End If '-creditNote-

                    '-- done Extra cash/creditNote stuff..-
                    '-- Show Payment Summary.

                    '-- Show revenue in revenue column..
                    '--  put revenue total into report..
                    s1 = FormatCurrency(decRevenue, 2)
                    sLine = "<textline>"
                    If (sPaymentDetail2 <> "") Then  '-show customer-
                        sLine &= sPaymentDetail2
                        sPaymentDetail2 = ""  '-done-
                    End If
                    '- Descr Amt for each Detail-
                    sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >Nett Revenue:</txt>"
                    '-Amount-
                    sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                    sLine &= " width = """ & k_WIDTH_AMT & """  >" & s1 & "</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '- Add Till SubTotal for Till analysis Report.
                    s1 = FormatCurrency(decTillSubTotal, 2)
                    sLine = "<textline>"
                    If (sPaymentDetail2 <> "") Then  '-show customer-
                        sLine &= sPaymentDetail2
                        sPaymentDetail2 = ""  '-done-
                    End If
                    '- Descr Amt for each Detail-
                    sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >Nett Till:</txt>"
                    '-Amount-
                    sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                    sLine &= " width = """ & k_WIDTH_AMT & """  >" & s1 & "</txt>"
                    sLine &= "</textline>"
                    mColTillAnalysisReportLines.Add(sLine)
                    '= = =

                    listPaymentTotals(intIdxRevenue) += decRevenue
                    '- No overall bucket for Till.. Compute Bal at end..

                    s1 = colPayment.Item("staff_docket_name") & vbCrLf & _
                                             colPayment.Item("Customer") & " [" & colPayment.Item("Cust_barcode") & "]"
                    s1 = "<drawline fontstyle=""bold"" />"
                    mColReportLines.Add(s1)
                    mColReportLines.Add("")  '--blank line under-
                    mColTillAnalysisReportLines.Add(s1)
                    mColTillAnalysisReportLines.Add("")

                    intRowCount += 1  '--next docket..-

                    mFormWait1.labMsg.Text = "Wait..  Building Report Lines: " & vbCrLf & _
                                "  Payment " & FormatNumber(intRowCount, 0) & "  of " & FormatNumber(mColPayments.Count, 0)
                    DoEvents()
                Next colPayment '-intPayment-
                '-- done all Payments..

                DoEvents()

                '-- Now Finish Off TILL T o t a l s -

                '- FIRST and LAST (ONLY) TILL-
                '- FIRST and LAST (ONLY) TILL-

                s1 = "<textline>"
                s1 &= "<txt TAB=""" & k_TAB_TILL & """ fontStyle=""bold""  >"
                If (strTill_id <> "") Then  '-till selected-
                    s1 &= "-- Overall Totals For Till: " & sCashDrawer & "</txt>"
                Else  '-all tills-
                    '= s1 &= "-- Overall Totals (ALL Tills): " & sCashDrawer & "</txt>"
                    '=4221.0305=
                    s1 &= "-- Overall Totals (ALL Tills): " & "</txt>"
                End If
                s1 &= "</textline>"
                mColReportLines.Add(s1)
                mColTillAnalysisReportLines.Add(s1)

                '-- Show listPaymentTotals  list.
                '-- Show listPaymentTotals  list.
                '-- Show listPaymentTotals  list.
                For ix = 0 To listPaymentTotals.Count - 1
                    sPaymentType_key = listPaymentTypes(ix)
                    sAmount = FormatCurrency(listPaymentTotals(ix), 2)
                    '- save till total amounts for cashup Reported Column--
                    '- listPaymentTotals(intIdxNettcash)-
                    '- USING range intIdxFirstCashupItem, intIdxLastCashupItem for Cashup-
                    '== SEE BELOW for TILL BALANCE stuff.

                    'If (ix >= intIdxFirstCashupItem) And (ix <= intIdxLastCashupItem) Then
                    '    '- only report standard pay types to Cashup-
                    '    If (InStr(LCase(sPaymentType_key), "cash") > 0) Then
                    '        '-- must report NETT cash in for cashup Till counting/comparing.
                    '        mColTillAnalysis.Add(listPaymentTotals(intIdxNettcash), "cash")  '-MUST be cash for cashup GRID.
                    '    Else '-non cash eg EFTPOS-
                    '        mColTillAnalysis.Add(listPaymentTotals(ix), sPaymentType_key)
                    '    End If '-cash-
                    'End If
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >"
                    sLine &= sPaymentType_key & "</txt>"
                    '-Amount-
                    sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                    sLine &= " width = """ & k_WIDTH_AMT & """  >" & sAmount & "</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '- Show this total in Till Report.. (if not Credit Not stuff.)
                    If (ix <> intIdxRevenue) And (ix <> intIdxCRS) And (ix <> intIdxCRR) Then
                        sLine = "<textline>"
                        sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >"
                        sLine &= sPaymentType_key & "</txt>"
                        '-Amount-
                        sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                        sLine &= " width = """ & k_WIDTH_AMT & """  >" & sAmount & "</txt>"
                        sLine &= "</textline>"
                        mColTillAnalysisReportLines.Add(sLine)
                    End If  '-till total report line.

                    '=  and show in REVENUE summary collection.-
                    mColRevenueSummaryLines.Add("<lucida>")   '-- lucida console fixed pitch.
                    mColRevenueSummaryLines.Add(LSet(sPaymentType_key, 20) & RSet(sAmount, 12))
                    If InStr(UCase(sPaymentType_key), "NETT CASH") > 0 Then
                        mColRevenueSummaryLines.Add("")   '--blank line after Cash summary..
                    End If
                Next ix

                '-- add overall total line for Till Report.
                '--  Till Total is Revenue after backing out Credit Note movements.
                decAmount = (listPaymentTotals(intIdxRevenue) - listPaymentTotals(intIdxCRR))
                '== Credit amt saved is negative.. so drop the sign.
                decAmount += Math.Abs(listPaymentTotals(intIdxCRS))
                sAmount = FormatCurrency(decAmount, 2)
                sLine = "<textline>"
                sLine &= "<txt TAB=""" & k_TAB_DESCR & """  >"
                sLine &= "Till Overall Total: </txt>"
                '-Amount-
                sLine &= "<txt TAB=""" & k_TAB_AMOUNT & """  align=""right""  "
                sLine &= " width = """ & k_WIDTH_AMT & """  >" & sAmount & "</txt>"
                sLine &= "</textline>"
                mColTillAnalysisReportLines.Add(sLine)

                '- That's all, she wrote..

                s1 = "<drawline fontstyle=""bold"" />"
                mColReportLines.Add(s1)
                mColTillAnalysisReportLines.Add(s1)
                sLine = "<textline>"
                sLine &= "<txt TAB=""" & k_TAB_TILL & """  >"
                sLine &= "   === The End ===</txt>"
                sLine &= "</textline>"
                mColReportLines.Add(sLine)
                mColTillAnalysisReportLines.Add(sLine)

                mColRevenueSummaryLines.Add("= = = = = = = = = = =  =")
                mColRevenueSummaryLines.Add("")
                intRowCount += 1  '--next section..-
                '-- and add section listPaymentTotals to Grand total list.
                For ix = 0 To listPaymentTotals.Count - 1
                    listGrandTotals(ix) += listPaymentTotals(ix)
                Next


                '-- TILL BALANCE-
                '-- TILL BALANCE-
                '-- GO through all the totals AGAIN and summarise for TILL movements.

                '= Dim decTillNettCash As Decimal = 0
                Dim decTillNettEftPosDr As Decimal = 0
                Dim decTillNettEftPosCr As Decimal = 0
                Dim intIdxTillRefund As Integer
                Dim colRefundOtherTypesDone As Collection

                mColTillBalance = New Collection
                colRefundOtherTypesDone = New Collection

                '-- first add Cash, EftPOs balances, as they are special-
                '-- Start with NETT CASH-
                '-- Then SUMMARISE EFTPOS with refunds.
                mColTillBalance.Add(listPaymentTotals(intIdxNettcash), "cash")

                '-- Refund amounts are already negative..
                decTillNettEftPosDr = listPaymentTotals(intIdxEftPosDr)
                decTillNettEftPosDr += listPaymentTotals(intIdxEftPosDr_refund)
                mColTillBalance.Add(decTillNettEftPosDr, "EftPos_Dr")

                decTillNettEftPosCr = listPaymentTotals(intIdxEftPosCr)
                decTillNettEftPosCr += listPaymentTotals(intIdxEftPosCr_refund)
                mColTillBalance.Add(decTillNettEftPosCr, "EftPos_Cr")

                '-- show all  the others..
                '--  ie Ignore cash and eftpos, as we have them..
                For ix = 0 To listPaymentTotals.Count - 1
                    sPaymentType_key = listPaymentTypes(ix)
                    sAmount = FormatCurrency(listPaymentTotals(ix), 2)
                    If (InStr(LCase(sPaymentType_key), "cash") > 0) Or _
                            (InStr(LCase(sPaymentType_key), "eftpos") > 0) Then
                        '--IGNORE as we have it.
                    Else  '--take it. eg cheque, bank etc.

                        '==  Target is new Build 4251. 24-May-2020.
                        '==  Target is new Build 4251. 24-May-2020.
                        '--  colOtherRefundDetailKeys Holds a collection of DISTINCT RefundOther keys..
                        '--    Used to add Totals buckets for the extra refund types
                        '--  NB- The Refund Types don't necessarily have matching Positive totals.. 
                        '-- FIRST take away any refunds for this Type..
                        decRefundAmt = 0
                        s1 = sPaymentType_key & "_refund"  '-to get to totals..
                        If colOtherRefundDetailKeys.Contains(sPaymentType_key) Then
                            intIdxTillRefund = colPaymentIndexes(s1)
                            decRefundAmt = listPaymentTotals(intIdxTillRefund)
                            colRefundOtherTypesDone.Add(sPaymentType_key, sPaymentType_key)  '-remember-
                        End If  '-refund
                        '= mColTillBalance.Add(listPaymentTotals(ix), sPaymentType_key)
                        mColTillBalance.Add(listPaymentTotals(ix) + decRefundAmt, sPaymentType_key)
                    End If  'type
                Next ix
                '==  Target is new Build 4251. 24-May-2020.
                '==  Target is new Build 4251. 24-May-2020.
                '-- Still on TILL BALANCE-
                '--      yes, not done TILL BALANCE yet-
                '-- GO through all the REFUND OTHER total Types and report any not matched above..
                '--  in the colRefundOtherTypesDone set..
                For Each sTypeKeyX As String In colOtherRefundDetailKeys
                    If Not colRefundOtherTypesDone.Contains(sTypeKeyX) Then
                        '-- still to report- a refund with no  matching sales in Period..
                        intIdxTillRefund = colPaymentIndexes.Item(sTypeKeyX & "_refund")
                        decRefundAmt = listPaymentTotals(intIdxTillRefund)
                        mColTillBalance.Add(decRefundAmt, sTypeKeyX)
                    End If
                Next sTypeKeyX

                '--END of TILL BALANCE-
                '--END of TILL BALANCE-
                '--END of TILL BALANCE-

                '--  G r a n d  T o t a l s -
                '--  G r a n d  T o t a l s -

                'colReceiptLines.Add("-- GRAND Totals All Terminals:" & vbCrLf)
                '-- and Show listGrandTotals  list.
                For ix = 0 To listGrandTotals.Count - 1
                    s1 = listPaymentTypes(ix)
                    s2 = FormatCurrency(listGrandTotals(ix), 2)
                    '- show in Grid..
                    'dgvReport.Rows(intRowCount).Cells("detail_amount").Value &= _
                    '                      LSet(s1, WIDTH_DESCR) & RSet(s2, WIDTH_AMT) & vbCrLf
                    ''=  and show in cash-up summary.-
                    'colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                    'colReceiptLines.Add(LSet(s1, 18) & RSet(s2, 12))
                Next
                mColRevenueSummaryLines.Add("= = = the end = = = =")
                mColRevenueSummaryLines.Add("")

                '-- List All CASHOUT below the payments received..
                decCashoutTotal = 0

                mColRevenueSummaryLines.Add("")
                '= colReceiptLines.Add("--- the end -----")
                mColRevenueSummaryLines.Add("")

                s2 = ""
                '-test receipt--
                For Each s1 In mColRevenueSummaryLines
                    s2 &= s1 & vbCrLf
                Next '-s1-
                '= MsgBox("Testing-  Summary is: " & vbCrLf & s2, MsgBoxStyle.Information)
                '= mColReceiptLines = colReceiptLines
                '= btnPrintTillSummary.Enabled = True
                '= If Not mbTillBalanceOnly Then

                '-- LAST THING..  Sort the Sorted Items list,
                '--   and make into a report..

                If (listSortedPaymentDetails.Count > 0) Then
                    Const k_SortTotalSize As Integer = k_SortHdrSize + 13
                    Dim sItem, sItemNext, sThisAmount, sTotalLine As String
                    Dim sSortedListTotalLine As String
                    Dim intCount As Integer = listSortedPaymentDetails.Count
                    Dim intPos As Integer
                    Dim decSortedSubTotal As Decimal
                    Dim decSortedFinalTotal As Decimal = 0
                    Try
                        listSortedPaymentDetails.Sort()
                    Catch ex As Exception
                        MsgBox("Error- Failed Sort Detail Items." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    End Try
                    '- make report-
                    decSortedSubTotal = 0
                    For iix As Integer = 0 To (intCount - 1)
                        sItem = listSortedPaymentDetails.Item(iix)

                        '-- get Amount from the end of the Item..
                        intPos = InStrRev(sItem, vbTab & vbTab)
                        If (intPos > 1) Then
                            '-found-

                            '==   Target-New-Build-4282 -- (Started 12-October-2020)
                            '==   Target-New-Build-4282 -- (Started 12-October-2020)
                            '==   Target-New-Build-4282 -- (Started 12-October-2020)
                            '==       CashSale REversal TRancode is "cshSaleReversal"
                            '== Amount String may be preceded by One or two minus signs..
                            '--    AND two minus will make a plus.
                            '-- RE-DO this whole block..

                            'sThisAmount = Mid(sItem, intPos + 2)  '-get past the Tabs..
                            'sItem = VB.Left(sItem, intPos - 1)  '-- drop off the amount cache off report line.
                            'If IsNumeric(sThisAmount) Then
                            '    decSortedSubTotal += CDec(sThisAmount)
                            '    decSortedFinalTotal += CDec(sThisAmount)
                            'End If

                            '- Now re-doing..
                            sThisAmount = Trim(Mid(sItem, intPos + 2))  '-get past the Tabs..
                            sItem = Trim(VB.Left(sItem, intPos - 1))  '-- drop off the amount cache off report line.
                            s1 = ""
                            If (VB.Left(sThisAmount, 1) = "-") Then
                                s1 = VB.Left(sThisAmount, 1)  '-save-
                                sThisAmount = Trim(Mid(sThisAmount, 2))  '-Drop the Minus..
                            End If  '-minus-
                            '-- if there were two minuses, then there is still one.
                            If IsNumeric(sThisAmount) Then
                                If (s1 = "-") Then  '-one or two minuses.
                                    decSortedSubTotal -= CDec(sThisAmount)
                                    decSortedFinalTotal -= CDec(sThisAmount)
                                Else '--one or no minuses.. (as before.)
                                    decSortedSubTotal += CDec(sThisAmount)
                                    decSortedFinalTotal += CDec(sThisAmount)
                                End If
                            End If
                            '== END  Target-New-Build-4282 -- (Started 12-October-2020)
                            '== END  Target-New-Build-4282 -- (Started 12-October-2020)

                        End If  '-intPos-

                        '-- Detail line-
                        sLine = "<textline>"
                        sLine &= "<txt TAB=""" & (k_TAB_DATE + 30) & """  >"
                        sLine &= sItem & "</txt>"
                        sLine &= "</textline>"
                        mColSortedDetailLines.Add(sLine)
                        '-LOOK AHEAD- Write Sub-total, and 
                        '--- draw line if new type coming up..
                        If (iix < intCount - 1) Then  '-not the end=
                            sItemNext = listSortedPaymentDetails.Item(iix + 1)
                            If UCase(Left(sItem, k_SortHdrSize)) <> UCase(Left(sItemNext, k_SortHdrSize)) Then
                                '-- show total of group just gone..
                                '=sTotalLine = " P/No: " & RSet(CStr(intPayment), 6)
                                '= sSortedListItem = LSet(sDescr, k_SortHdrSize) & sPid & RSet(sAmount, 13) & sSortedLineDetail
                                s1 = FormatCurrency(decSortedSubTotal, 2)
                                sSortedListTotalLine = LSet("== Total: ", k_SortTotalSize) & RSet(s1, 13)

                                '-- Total line-
                                sLine = "<textline>"
                                sLine &= "<txt TAB=""" & (k_TAB_DATE + 30) & """ fontStyle=""bold"" >"
                                sLine &= sSortedListTotalLine & "</txt>"
                                sLine &= "</textline>"
                                mColSortedDetailLines.Add(sLine)

                                s1 = "<drawline fontstyle=""bold"" />"
                                mColSortedDetailLines.Add(s1)
                                decSortedSubTotal = 0  '--reset-
                            End If
                        End If  '-not the end.
                    Next iix
                    '-- Show total for last group-
                    s1 = FormatCurrency(decSortedSubTotal, 2)
                    sSortedListTotalLine = LSet("== Total: ", k_SortHdrSize + 12) & RSet(s1, 13)

                    '-- Total line-
                    s1 = FormatCurrency(decSortedSubTotal, 2)
                    sSortedListTotalLine = LSet("== Total: ", k_SortHdrSize + 12) & RSet(s1, 13)
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & (k_TAB_DATE + 30) & """ fontStyle=""bold"" >"
                    sLine &= sSortedListTotalLine & "</txt>"
                    sLine &= "</textline>"
                    mColSortedDetailLines.Add(sLine)
                    s1 = "<drawline fontstyle=""bold"" />"
                    mColSortedDetailLines.Add(s1)
                    '-- Final Total-
                    s1 = FormatCurrency(decSortedFinalTotal, 2)
                    sSortedListTotalLine = LSet("== FINAL Total: ", k_SortHdrSize + 12) & RSet(s1, 13)
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & (k_TAB_DATE + 30) & """ fontStyle=""bold"" >"
                    sLine &= sSortedListTotalLine & "</txt>"
                    sLine &= "</textline>"
                    mColSortedDetailLines.Add(sLine)
                    s1 = "<drawline fontstyle=""bold"" />"
                    mColSortedDetailLines.Add(s1)

                    s1 = "<drawline fontstyle=""bold"" />"
                    mColSortedDetailLines.Add(s1)
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & (k_TAB_DATE + 30) & """  >"
                    sLine &= "====  The End ====" & "</txt>"
                    sLine &= "</textline>"
                    mColSortedDetailLines.Add(sLine)
                Else
                    '= MsgBox("No Detail Items to sort.", MsgBoxStyle.Exclamation)
                End If  '-Sorted List count-
                '= btnPrintReport.Enabled = True
                Call mWaitFormOff()
                If (listSortedPaymentDetails.Count <= 0) Then
                    MsgBox("Note- There were No Detail Items to sort.", MsgBoxStyle.Exclamation)
                End If
            Else
                '= Call mWaitFormOff()
                MsgBox("Sorting- No Payments found.. " & vbCrLf & _
                                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '= Exit Function
            End If  '--nothing-
            cashupAnalysis = True
        End If  '--get datatable-
        sSql = ""  '--all done-
        '--    bypass reload..-

    End Function  '--mbCashupAnalysis--
    '= = = = = =  ==  ==  == = =  == == =
    '-===FF->



End Class  '-clsCashupPayments-

'= = = the end = = = == = = ==
