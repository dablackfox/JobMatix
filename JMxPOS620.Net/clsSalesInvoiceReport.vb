
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

Public Class clsSalesInvoiceReport

    '--02-Apr-2020..  
    '==  This is a rewrite of the Sales Invoice Listing..
    '--    NO more Payments details now..  Invoice data only..
    '--    NO more Payments details now..  Invoice data only..
    '--    NO more Payments details now..  Invoice data only..
    '--    NO more Payments details now..  Invoice data only..
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==   Updates to 4233.0421  Started 24-April-2020= 
    '==   Updates to 4233.0421  Started 24-April-2020= 
    '==
    '==  Target is new Build 4234..
    '==  Target is new Build 4234..
    '==
    '==    frmPOS3Reports-  STILL a CHILD UserControl-
    '==      --  For Sales Invoice Report-  ADD PROFIT on Invoice For BOTH GRID and preview Versions.
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==
    '== DEV PREP Updates to 4251.0606  Started 17-June-2020= 
    '== DEV PREP Updates to 4251.0606  Started 17-June-2020= 
    '==
    '==  Target-New-Build-4253..
    '==  Target-New-Build-4253..
    '==  -- Sales Report- Show Nett sales after Refunds;  Show Producr Sales, then Gross Profit After Discount.
    '==  -- ALSO- Add function get totals section from last report run..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==
    '==     POS Sales-  Fix DivideByZero Runtime Error in Sales Invoice Report.
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = = = = == 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Private mFrmParent As Form

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection '--
    Private msSqlDbName As String = ""
    '- SHAPE cnn for us here only- (GoodsReceived.)
    Private mCnnShape As OleDbConnection   '=  ADODB.Connection

    Private msWhereCondition As String = ""
    '= Private mbShowInvoiceLines As Boolean = False

    Private mColReportLines As Collection  '-- standard report to preview/print.
    Private mColGridReport As Collection   '-- condensed for Grid- 1 Row per invoice.

    '==  Target-New-Build-4253..
    Private mIntStartOfTotalsLines As Integer = -1   '--StartOfTotalsLines in mColReportLines
    Private mIntStartOfTotalsGridLines As Integer = -1   '--StartOfTotalsLines in mColGridReport

    '-- wait form--
    Private mFormWait1 As frmWait

    '= = = = = = == =  =
    '-===FF->

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String)

        mFormWait1 = New frmWait
        mFormWait1.waitTitle = "POS Reports"  '=msVersionPOS
        mFormWait1.labHdr.Text = "Sales Invoices-"
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.Show(mFrmParent)
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

    '-mbMakeSalesInvoiceReport=

    Private Function mbMakeSalesInvoiceReport(ByVal sWhereCondition As String, _
                                               ByVal bShowInvoiceLines As Boolean) As Boolean
        '= Dim dtInvoices, dtInvoiceLines As DataTable
        Dim sServer, sConnect, sErrors As String
        Dim sShapeSql As String
        Dim intRecordsAffected, ix As Integer
        '= Dim colInvoice, colInvoices, colInvoiceLines, colInvLine, colPayDetails As Collection
        Dim colGridInvoice As Collection

        mbMakeSalesInvoiceReport = False
        mColReportLines = New Collection
        mColGridReport = New Collection

        sServer = mCnnSql.DataSource
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mCnnShape = New OleDbConnection '=  ADODB.Connection
        sConnect = "Provider=MSDataShape; Server=" & sServer & "; Trusted_Connection=true; Integrated Security=SSPI; "
        sConnect = sConnect & "; Data Provider=SQLOLEDB; Data Source=" & sServer & "; "
        '=== sConnect = sConnect + "; Password=" + msSqlPwd + "; User ID=" + msSqlUid
        If gbConnectSql(mCnnShape, sConnect) Then
            '--FrameReport.Enabled = True   '--show report options frame..--
            '--FrameStatus.Enabled = True
        Else
            MsgBox("Login to sql SHAPE dataSource has failed." & vbCrLf, MsgBoxStyle.Exclamation)
            '====FrameReport.Enabled = False
            '= Me.Hide()
            '==End
            Exit Function
        End If '--connected-
        If Not gbExecuteCmd(mCnnShape, "USE " & msSqlDbName & vbCrLf, intRecordsAffected, sErrors) Then
            MsgBox(vbCrLf & "Failed USE for SHAPE connection to DATABASE: '" & _
                                            msSqlDbName & "'.." & vbCrLf & sErrors, MsgBoxStyle.Critical)
        End If '--use-
        '= mCnnShape.CommandTimeout = 10 '-- 10 sec cmd timeout..-
        '= mCnnShape.CursorLocation = ADODB.CursorLocationEnum.adUseClient


        '-- YES Shaping..
        '= mCnnShape = mCnnSql
        sShapeSql = ""

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        sShapeSql = " SHAPE {"
        sShapeSql &= " SELECT  *, Invoice.invoice_id, Invoice.invoice_id AS invoice_id2, staff.docket_name, "
        sShapeSql &= "  payments.invoice_id AS paym_invoice_id, "
        sShapeSql &= "  payments.payment_id, payment_date, payments.transactionType AS payment_tranType, "
        sShapeSql &= "  payments.payment_id AS payment_id2, "
        sShapeSql &= "  payments.nettAmountCredited, payments.amountDebitedToAccount, "
        sShapeSql &= "  payments.changeGiven, payments.refundCashAmount,  "
        sShapeSql &= "  refundAsCreditNoteCredited, refundAsEftPosDr, refundAsEftPosCr,  "
        sShapeSql &= "    creditNoteAmountDebited, creditNotePaymentCredited, "
        sShapeSql &= "    customer.firstName + ' ' + customer.lastName AS Customer, customer.barcode AS cust_barcode "
        sShapeSql &= "  FROM dbo.Invoice "
        sShapeSql &= "  Left outer JOIN [dbo].[Payments]  on (payments.invoice_id =invoice.invoice_id) "
        sShapeSql &= "  LEFT OUTER JOIN staff on (staff.staff_id=invoice.staff_id) "
        sShapeSql &= "  LEFT OUTER JOIN Customer on (invoice.customer_id=Customer.customer_id) "
        sShapeSql &= sWhereCondition
        sShapeSql &= " ORDER BY invoice.invoice_id "
        sShapeSql &= "  } "
        '--  Get child r-sets for Invoice lines..
        sShapeSql &= " APPEND  ("
        sShapeSql &= " { SELECT *, stock.barcode, stock.cat1, stock.description "
        sShapeSql &= "     FROM dbo.InvoiceLine "
        sShapeSql &= "     LEFT OUTER JOIN  dbo.stock ON (stock.stock_id=InvoiceLine.stock_id) }"  '--end of SELECT..
        sShapeSql &= "     RELATE Invoice_id2 to Invoice_id) AS rsInvoiceLines "
        '--  Get child r-sets for Payment Details...
        '=sShapeSql &= "  , "
        '== payment_id, paymentType_key, amount  "
        sShapeSql &= ", ({ SELECT * FROM dbo.PaymentDetails } "
        sShapeSql &= "   RELATE payment_id2 to payment_id)  "
        sShapeSql &= "      AS rsPaymentDetails  "
        '--  end of child r-sets for Payment Details...
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '-- start retrieval-
        Dim cmd1 As OleDbCommand
        Dim rdrInvoices, rdrInvoiceLines, rdrPayDetails As OleDbDataReader
        Dim sCustomerBarcode, sCustomerName As String
        Dim sInvoiceId As String
        Dim colInvoice, colInvoices, colInvoiceLines, colInvLine, colPayDetails As Collection
        Dim sWaitMsg As String = "Pls Wait. Collecting the Sales Invoices.." & vbCrLf & _
                         " This might take a minute-  "

        Call mWaitFormOn("Pls Wait. connecting to Sales Invoices.." & vbCrLf & _
                         " This might take a minute.")
        '-- get the main INVOICES recordset (datatable)..
        'If Not gbGetDataTable(mCnnSql, dtInvoices, sSql) Then
        '    Call mWaitFormOff()
        '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    MsgBox("Error in getting recordset for Invoices table: " & vbCrLf & _
        '                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        '    Exit Function
        'Else
        'End If  '--get Invoice datatable-
        'If (dtInvoices Is Nothing) OrElse (dtInvoices.Rows.Count <= 0) Then
        '    '-nothing
        '    Call mWaitFormOff()
        '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '    MsgBox("No Invoice found.. " & vbCrLf & _
        '                gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        '    Exit Function
        'End If  '-nothing--

        Try
            cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
            rdrInvoices = cmd1.ExecuteReader
        Catch ex As Exception
            Call mWaitFormOff()
            MsgBox("Error getting Invoices/Items recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Exit Function
        End Try
        '-- check it all-
        Call mWaitFormOff()
        '= MsgBox("Found " & dtInvoices.Rows.Count & " Invoice records.. ", MsgBoxStyle.Information)

        colInvoices = New Collection
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '= Collect all invoices..
        Call mWaitFormOn(sWaitMsg)
        Try  '--main try Invoices-
            If rdrInvoices.HasRows Then
                Dim intRowx As Integer = 0
                '--for dropping duplicated due to multiple payments.
                Dim intLastInvoiceId As Integer = -1  '--for dropping duplicated due to multiple payments.

                Do While rdrInvoices.Read
                    '-- get Invoices..
                    '= dataRow1 = dtInvoices.Rows(intRowx)
                    sCustomerBarcode = rdrInvoices.Item("cust_barcode")
                    sCustomerName = rdrInvoices.Item("Customer")
                    ix = rdrInvoices.Item("invoice_id")
                    If (ix <= intLastInvoiceId) Then
                        '-drop redundant row.
                        Continue Do
                    End If
                    intLastInvoiceId = ix '- new invoice row..

                    '-- save docket info into a sub-collection..
                    '--     for this docket
                    colInvoice = New Collection
  

                    sInvoiceId = CStr(ix)  '- for collection key.
                    colInvoice.Add(ix, "Id")

                    '-TESTING-
                    '= MsgBox("Got Invoice # " & sInvoiceId, MsgBoxStyle.Information)

                    colInvoice.Add(UCase(rdrInvoices.Item("transactionType")), "tr_type")
                    colInvoice.Add(rdrInvoices.Item("subtotal_inc"), "subtotal_inc")

                    '==  Target-New-Build-4253..
                    '==  -- Sales Report- Show Nett sales after Refunds;  Show Producr Sales, then Gross Profit After Discount.
                    colInvoice.Add(rdrInvoices.Item("subtotal_ex_non_taxable"), "subtotal_ex_non_taxable")
                    colInvoice.Add(rdrInvoices.Item("subtotal_ex_taxable"), "subtotal_ex_taxable")
                    colInvoice.Add(rdrInvoices.Item("subtotal_tax"), "subtotal_tax")

                    colInvoice.Add(rdrInvoices.Item("total_inc"), "total_inc")
                    colInvoice.Add(rdrInvoices.Item("total_tax"), "total_tax")
                    colInvoice.Add(rdrInvoices.Item("docket_name"), "docket_name")  '-staff-
                    colInvoice.Add(rdrInvoices.Item("Customer"), "Customer")  '-save customer this invoice.
                    colInvoice.Add(rdrInvoices.Item("cust_barcode"), "cust_barcode")  '-save customer barcde.
                    colInvoice.Add(rdrInvoices.Item("invoice_date"), "date")  '-save Date this invoice.
                    colInvoice.Add(rdrInvoices.Item("cashDrawer"), "cashDrawer")  '-save till_id.
                    '=3301.817= col1.Add(row1.Item("amtCharged"), "amtCharged")  '-save amt Charged this invoice.
                    colInvoice.Add(rdrInvoices.Item("isOnAccount"), "isOnAccount")  '-save whether Charged this invoice.
                    colInvoice.Add(rdrInvoices.Item("discount_nett"), "discount_nett")  '-save discount this invoice.
                    colInvoice.Add(rdrInvoices.Item("discount_tax"), "discount_tax")
                    '=colInvoice.Add(dataRow1.Item("cashout"), "cashout")
                    colInvoice.Add(rdrInvoices.Item("rounding"), "rounding")

                    '-- Payment record used only to show payment for CASH SALE transaction.
                    '-- Capture payment details as well.
                    colPayDetails = New Collection
                    If IsDBNull(rdrInvoices.Item("payment_id")) Then
                        colInvoice.Add(-1, "payment_id")
                    Else  '-have payment-
                        colInvoice.Add(rdrInvoices.Item("payment_id"), "payment_id")
                        colInvoice.Add(rdrInvoices.Item("creditNoteAmountDebited"), "creditNoteAmountDebited")
                        colInvoice.Add(rdrInvoices.Item("creditNotePaymentCredited"), "creditNotePaymentCredited")
                        colInvoice.Add(rdrInvoices.Item("changeGiven"), "changeGiven")
                        colInvoice.Add(rdrInvoices.Item("refundCashAmount"), "refundCashAmount")
                        colInvoice.Add(rdrInvoices.Item("refundAsCreditNoteCredited"), "refundAsCreditNoteCredited")
                        colInvoice.Add(rdrInvoices.Item("refundAsEftPosDr"), "refundAsEftPosDr")
                        colInvoice.Add(rdrInvoices.Item("refundAsEftPosCr"), "refundAsEftPosCr")
                        '-- Capture payment details as well.
                        If TypeOf rdrInvoices.Item("rsPaymentDetails") Is IDataReader Then
                            rdrPayDetails = rdrInvoices.Item("rsPaymentDetails")
                            If rdrPayDetails.HasRows Then
                                Dim colDetail As Collection
                                Do While rdrPayDetails.Read
                                    colDetail = New Collection
                                    colDetail.Add(rdrPayDetails.Item("payment_id"), "payment_id")
                                    '-paymentType_key-
                                    colDetail.Add(rdrPayDetails.Item("paymentType_key"), "paymentType_key")
                                    colDetail.Add(rdrPayDetails.Item("amount"), "amount")
                                    colPayDetails.Add(colDetail)
                                Loop  '-While rdrPayDetails.Read-
                            End If '--has rows..
                        End If '-typeOf-
                    End If
                    colInvoice.Add(colPayDetails, "PaymentDetails")

                    '-- Get all invoice lines THIS invoice...
                    '-- Save as a collection in the invoice collection..
                    colInvoiceLines = New Collection
                    If TypeOf rdrInvoices.Item("rsInvoiceLines") Is IDataReader Then
                        rdrInvoiceLines = rdrInvoices.Item("rsInvoiceLines")
                        If rdrInvoiceLines.HasRows Then
                            Do While rdrInvoiceLines.Read
                                '-- next invoiceLine.
                                colInvLine = New Collection
                                colInvLine.Add(rdrInvoiceLines.Item("barcode"), "barcode")
                                colInvLine.Add(rdrInvoiceLines.Item("description"), "description")
                                colInvLine.Add(rdrInvoiceLines.Item("quantity"), "quantity")
                                colInvLine.Add(rdrInvoiceLines.Item("total_inc"), "total_inc")

                                '==  Target is new Build 4234..
                                '==  Target is new Build 4234..

                                '= Save gross profit from all lines to compute Invoice profit.
                                colInvLine.Add(rdrInvoiceLines.Item("gross_profit"), "gross_profit")
                                '==  END OF-  PREP FOR- Updates to 4233.0421  Started 24-April-2020= 
                                '==  END OF-  PREP FOR- Updates to 4233.0421  Started 24-April-2020= 

                                colInvoiceLines.Add(colInvLine)
                            Loop  '-While rdrInvoiceLines.Read-
                        End If  '-rdrInvoiceLines.HasRows-
                        rdrInvoiceLines.Close()
                    End If  '-TypeOf rdrInvoices-
                    colInvoice.Add(colInvoiceLines, "InvoiceLines")
                    colInvoices.Add(colInvoice, sInvoiceId)
                    intRowx += 1
                    mFormWait1.labMsg.Text = sWaitMsg & intRowx
                    DoEvents()
                Loop  '-rdrInvoices.Read-
            End If  '-rdrInvoices.HasRows-
            Call mWaitFormOff()
        Catch ex As Exception
            '-error-
            Call mWaitFormOff()
            MsgBox("Error Loading Invoices collection....." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try  '--main try Invoices-
        '=rdrInvoices.Close()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '= MsgBox("Loaded " & colInvoices.Count & " Invoices.. ", MsgBoxStyle.Information)

        '-- Build report..
        '- NOW- Build report list showing-
        '--    (1) invoiceNo/Cust and (2) Product Items list as extra Rows..--
        '-  define tab columns- (ofsets from our marg)-
        Const k_TAB_TRANCODE As Integer = 0  '=60
        Const k_TAB_INVNO As Integer = 80
        Const k_WIDTH_INVNO As Short = 50
        '= Const k_TAB_CUST As Integer = 180
        Const k_TAB_DATE As Integer = 140  '-Date + Invoice Total.
        Const k_TAB_CUST As Integer = 300  '- Cust and Items underneath.

        Const k_TAB_INV_AMT As Integer = 140  '--Invoice DEtails overlap Cist.
          '- detail lines-
        '- Const k_TAB_BARCODE As Integer = 200
        Const k_TAB_DESCR As Integer = 320  '= 300  '--Description [barcode]
        Const k_TAB_QTY As Integer = 500
        Const k_WIDTH_QTY As Short = 50
        Const k_TAB_LINE_AMT As Integer = 560
        Const k_WIDTH_AMT As Short = 80
        Const k_TAB_STAFF As Integer = 680

        '- For EACH docket (Invoice) in turn-  add an invoice Row. 
        '-   and find the payments for the docket and add them (as extra rows).
        Dim bIsRefund As Boolean
        Dim decAmount, decAmtCharged As Decimal
        Dim decSubtotal_inc, decTotalTax, decSubTotalTax As Decimal
        Dim decDiscNett, decDiscTax, decDisc_inc As Decimal
        Dim decCashout, decRounding As Decimal
        Dim decInvoiceTotal, decItemQty As Decimal
        Dim decInvoiceAmtPaid As Decimal
        Dim decChangeGiven As Decimal
        Dim decCreditNoteAmountDebited, decCreditNotePaymentCredited As Decimal
        Dim decRefundCash, decRefundCreditNote, decRefundEftPosDr, decRefundEftPosCr As Decimal


        '==  Updates to 4233.0421  Started 24-April-2020= 
        '==  Target is new Build 4234..
        Dim decGP As Decimal
        Dim decTotalLine_gross_profit As Decimal
        Dim decInvoiceNettProfit As Decimal
        Dim decFinalTotalNettProfit As Decimal = 0
        '==  END OF-  PREP FOR- Updates to 4233.0421  Started 24-April-2020= 

        Dim decTotalSubTotal As Decimal = 0
        Dim decTotalDiscount As Decimal = 0
        '= Dim decTotalCashout As Decimal = 0
        Dim decTotalRounding As Decimal = 0

        Dim decTotalInvoiced As Decimal = 0
        Dim decTotalTaxInvoiced As Decimal = 0
        Dim decTotalProductSales As Decimal = 0

        '==  Target-New-Build-4253..
        '==  -- Sales Report- Show Nett sales after Refunds;  Show Product Sales, then Gross Profit After Discount.
        Dim decTotalRefundsProductSales As Decimal = 0
        Dim decProductAmount As Decimal
        Dim decTotalSubtotalTax As Decimal = 0
        Dim decTotalRefundSubtotalTax As Decimal = 0

        Dim decTotalDiscountTaxSales As Decimal = 0
        Dim decTotalDiscountTaxRefunds As Decimal = 0

        Dim decTotalInvoicePaid As Decimal = 0

        Dim decTotalRefunds As Decimal = 0
        Dim decTotalRefundsSubTotal As Decimal = 0
        Dim decTotalRefundsTax As Decimal = 0
        Dim decTotalRefundsDiscount As Decimal = 0

        Dim decTotalAmtCharged As Decimal = 0
        '-- THESE TWO below are only for Payment related to the CASH Sales..
        Dim decTotalCreditNoteDebited As Decimal = 0
        Dim decTotalCreditPaymentCredited As Decimal = 0

        Dim bIsOnAccount As Boolean
        Dim sInvoiceDetail, sLine, sDate, sInvNo, sCust, sInvAmounts, sPaidAmounts, sStaff As String
        Dim sBarcode, sDescr, sQty, sExt As String
        Dim date1, dateInvoice As DateTime
        Dim intInvoice As Integer
        Dim s1, s2, s3 As String
        Dim sInvoiceTotalLine As String
        Dim sTranType As String

        Dim intCount As Integer = 0
        Dim rowx As Integer = 0  '--count all rows added.

        Dim sWaitMsg2 As String = "Pls Wait. Building Invoice Report.." & vbCrLf & _
                 " This might take a minute as well.."
        Call mWaitFormOn(sWaitMsg2)

        Try  '-- all Invoices..
            '- For EACH docket (Invoice) in turn-  add an invoice Row. 
            For Each colInvoice In colInvoices  '-Each Invoice/docket-
                intInvoice = colInvoice.Item("id")
                dateInvoice = CDate(colInvoice.Item("date"))
                sTranType = colInvoice.Item("tr_type")

                bIsRefund = (LCase(colInvoice.Item("tr_type")) = "refund")
                '=3301.817=decAmtCharged = CDec(col1.Item("amtCharged"))
                bIsOnAccount = IIf(((colInvoice.Item("isOnAccount")) <> 0), True, False)

                decDiscNett = CDec(colInvoice.Item("discount_nett"))
                decDiscTax = CDec(colInvoice.Item("discount_tax"))
                decDisc_inc = (decDiscNett + decDiscTax)
                '= decCashout = CDec(colInvoice.Item("cashout"))
                decRounding = CDec(colInvoice.Item("rounding"))
                decTotalTax = CDec(colInvoice.Item("total_tax"))

                decSubtotal_inc = CDec(colInvoice.Item("subtotal_inc"))
                decInvoiceTotal = CDec(colInvoice.Item("total_inc"))

                '== decTotal = IIf(bIsRefund, -CDec(col1.Item("total")), CDec(col1.Item("total")))
                decInvoiceAmtPaid = 0  '-temp-
                decAmtCharged = 0
                '-- set sign on on invoice total..--

                '==  Target-New-Build-4253..
                '==  -- Sales Report- Show Nett sales after Refunds;  Show Producr Sales, then Gross Profit After Discount.
                decProductAmount = CDec(colInvoice.Item("subtotal_ex_taxable")) + _
                                                           CDec(colInvoice.Item("subtotal_ex_non_taxable"))
                decSubTotalTax = CDec(colInvoice.Item("subtotal_tax"))

                '--  and add amtCharged to pay-type totals.-
                If bIsRefund Then
                    '=decTotalDiscount -= decDisc_inc
                    decTotalRefundsDiscount += decDisc_inc
                    '= decTotalCashout -= decCashout
                    '=decTotalRounding -= decRounding
                    '= decTotalTaxInvoiced -= decTotalTax
                    decTotalRefundsTax += decTotalTax
                    '=decTotalInvoiced -= decInvoiceTotal
                    decTotalRefunds += decInvoiceTotal
                    decTotalRefundsSubTotal += decSubtotal_inc
                    decTotalRefundSubtotalTax += decSubTotalTax
                    '==  Target-New-Build-4253..
                    decTotalRefundsProductSales += decProductAmount
                    decTotalDiscountTaxRefunds += decDiscTax
                Else '-not refund-
                    decTotalSubTotal += decSubtotal_inc
                    decTotalDiscount += decDisc_inc
                    '= decTotalCashout += decCashout
                    decTotalRounding += decRounding
                    decTotalTaxInvoiced += decTotalTax
                    decTotalInvoiced += decInvoiceTotal
                    If bIsOnAccount Then
                        decAmtCharged = decInvoiceTotal
                        decTotalAmtCharged += decInvoiceTotal
                        sTranType = "ACCOUNT " & sTranType
                    Else   '-cash sale=
                        decInvoiceAmtPaid = decInvoiceTotal
                        decTotalInvoicePaid += decInvoiceTotal
                        sTranType = "CASH " & sTranType
                    End If
                    '==  Target-New-Build-4253..
                    decTotalProductSales += decProductAmount
                    decTotalSubtotalTax += decSubTotalTax
                    decTotalDiscountTaxSales += decDiscTax
                End If
                '=3301.817=
                '=3519.0112=

                '-  Payments are now only those for cash sales.. !!..
                decChangeGiven = 0
                decCreditNotePaymentCredited = 0
                decCreditNoteAmountDebited = 0
                If colInvoice.Contains("payment_id") AndAlso (colInvoice.Item("payment_id") > 0) Then
                    '-have a payment record for this invoice..
                    If colInvoice.Contains("creditNoteAmountDebited") Then
                        decCreditNoteAmountDebited = colInvoice.Item("creditNoteAmountDebited")
                    End If
                    If colInvoice.Contains("creditNotePaymentCredited") Then
                        decCreditNotePaymentCredited = colInvoice.Item("creditNotePaymentCredited")
                    End If
                    If colInvoice.Contains("changeGiven") Then
                        decChangeGiven = colInvoice.Item("changeGiven")
                    End If
                End If  '-payment-
 
                'decCreditNoteAmountDebited = CDec(colInvoice.Item("creditNoteAmountDebited"))
                decTotalCreditNoteDebited += decCreditNoteAmountDebited
                decTotalCreditPaymentCredited += decCreditNotePaymentCredited

                '- collection stuff for Invoice Grid Row.
                colGridInvoice = New Collection

                '-- Add a "header" line for each invoice..

                '-- start Invoice Report Line..
                '-  FIRST- ask for Min page space..
                '--   need min lines for invoice..
                colInvoiceLines = colInvoice.Item("InvoiceLines")
                s1 = "<MinPageRoom Lines= """ & CStr(colInvoiceLines.Count + 3) & """ />"
                mColReportLines.Add(s1)

                '-- start Invoice Report Line..
                sInvNo = Format(intInvoice, "  000") & " "  ''--make trailing space.
                sDate = Format(colInvoice.Item("date"), "dd-MMM-yyyy hh:mm")
                sCust = colInvoice.Item("Customer") & "  [" & colInvoice.Item("Cust_barcode") & "]"
                sStaff = colInvoice.Item("docket_name")

                sInvoiceDetail = "<textline>"
                sInvoiceDetail &= "<txt  TAB=""" & k_TAB_TRANCODE & """ >" & sTranType & "</txt> "
                sInvoiceDetail &= "<txt  TAB=""" & k_TAB_INVNO & """   align=""right""  " & _
                                      " width = """ & k_WIDTH_INVNO & """  >" & sInvNo & "</txt>"
                sInvoiceDetail &= "<txt  TAB=""" & k_TAB_DATE & """ >" & sDate & _
                                      "  [Till: " & colInvoice.Item("cashDrawer") & "].</txt>"
                sInvoiceDetail &= "<txt TAB=""" & k_TAB_CUST & """ > =" & sCust & "</txt>"
                '=            "<txt TAB=""" & k_TAB_CUST & """ >" & sCust & "</txt>"
                sInvoiceDetail &= "<txt TAB=""" & k_TAB_STAFF & """ >" & sStaff & "</txt>"
                sInvoiceDetail &= "</textline>"
                mColReportLines.Add(sInvoiceDetail)

                '- 1st four Grid Columns.
                colGridInvoice.Add(sTranType)
                colGridInvoice.Add(sInvNo)
                colGridInvoice.Add(sDate)
                colGridInvoice.Add(sCust)

                '- Read through COLLECTION of InvoiceLines for current Invoice, 
                '--   and SHOW Lines for THIS invoice..
                colInvoiceLines = colInvoice.Item("InvoiceLines")
                If bShowInvoiceLines Then
                    For Each colInvLine In colInvoiceLines
                        '-- show this Item line.
                        sBarcode = colInvLine.Item("barcode")
                        sDescr = colInvLine.Item("description") & " [" & sBarcode & "]"
                        '=sQty = colInvLine.Item("quantity")
                        '= sExt = CStr(colInvLine.Item("total_inc"))
                        decItemQty = CDec(colInvLine.Item("quantity"))
                        '-- if no fractions, don't show decimals.
                        If decItemQty = Int(decItemQty) Then
                            '- is whole
                            sQty = "x" & CStr(CInt(decItemQty))
                        Else  '-leave it make decimals-
                            sQty = "x" & CStr(decItemQty)
                        End If
                        sExt = FormatCurrency(colInvLine.Item("total_inc"), 2)
                        sLine = "<textline>"  '=  & sInvoiceDetail & "</textline>"
                        sLine &= "<txt TAB=""" & k_TAB_DESCR & """ >      -- " & sDescr & "</txt>"
                        sLine &= "<txt TAB=""" & k_TAB_QTY & """ align=""right""  " & _
                                     " width = """ & k_WIDTH_QTY & """  >" & sQty & "</txt>"
                        sLine &= "<txt TAB=""" & k_TAB_LINE_AMT & """   align=""right""  " & _
                                      " width = """ & k_WIDTH_AMT & """  >" & sExt & "</txt>"
                        sLine &= "</textline>"
                        mColReportLines.Add(sLine)
                    Next colInvLine
                End If '-show lines-


                '==   Updates to 4233.0421  Started 24-April-2020= 
                 '==  Target is new Build 4234..
                '--   Accum Gross profit Amounts..
                decTotalLine_gross_profit = 0
                For Each colInvLine In colInvoiceLines
                    decGP = CDec(colInvLine.Item("gross_profit"))
                    If bIsRefund Then
                        decTotalLine_gross_profit -= decGP  '=CDec(colInvLine.Item("gross_profit"))
                    Else  '-not refund.
                        decTotalLine_gross_profit += decGP  '=CDec(colInvLine.Item("gross_profit"))
                    End If
                Next colInvLine
                '==  END OF-  PREP FOR- Updates to 4233.0421  Started 24-April-2020= 

                '-- done all item lines..

                '-- summary this invoice.--
                '-- new line for Inv Amounts.-
                sInvAmounts = " SubTotal_inc: " & FormatCurrency(decSubtotal_inc)
                sInvAmounts &= ".  (Tax: " & FormatCurrency(decTotalTax) & ");"


                '== Updates to 4233.0421  Started 24-April-2020= 
                '==  Target is new Build 4234..
                '--_gross_profit-
                sInvAmounts &= " Gr-profit: " & FormatNumber(decTotalLine_gross_profit, 2) & ";"
                '==  Target-New-Build-4253..
                '==  Target-New-Build-4253..  IF refund, then Discount "increases" profit.
                If bIsRefund Then
                    decInvoiceNettProfit = decTotalLine_gross_profit + decDiscNett
                Else
                    '-not refund-
                    decInvoiceNettProfit = decTotalLine_gross_profit - decDiscNett
                End If
                decFinalTotalNettProfit += decInvoiceNettProfit
                '==  END OF-  PREP FOR- Updates to 4233.0421  Started 24-April-2020= 

                If (decDisc_inc <> 0) Then
                    sInvAmounts &= "Discount: " & FormatCurrency(decDisc_inc) & ";"
                End If
                '- Rounding..
                If (decRounding <> 0) Then
                    sInvAmounts &= "; Rounding: " & FormatCurrency(decRounding)
                End If '- Rounding..

                '==  Updates to 4233.0421  Started 24-April-2020= 
                '==  Target is new Build 4234..
                '--Nett_profit-
                sInvAmounts &= ";Nett-Profit: " & FormatCurrency(decInvoiceNettProfit)
                '==  END OF PREP FOR- Target is new Build 4234..
                '==  END OF PREP FOR- Target is new Build 4234..

                '-print sub-total-
                sLine = "<textline>"
                sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >" & sInvAmounts & "..</txt>"
                sLine &= "</textline>"
                mColReportLines.Add(sLine)
                '--discount.-
                'If (decDisc_inc <> 0) Then
                '    sInvAmounts = "Discount: " & FormatCurrency(decDisc_inc)
                '    '-print discount-
                '    sLine = "<textline>"
                '    sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >" & sInvAmounts & "..</txt>"
                '    sLine &= "</textline>"
                '    mColReportLines.Add(sLine)
                'End If
                'If (decCashout <> 0) Then
                '    sInvAmounts &= "; Cashout: " & FormatCurrency(decCashout)
                'End If
                'If (decRounding <> 0) Then
                '    sInvAmounts = "Rounding: " & FormatCurrency(decRounding)
                '    '-print Rounding-
                '    sLine = "<textline>"
                '    sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >" & sInvAmounts & "..</txt>"
                '    sLine &= "</textline>"
                '    mColReportLines.Add(sLine)
                'End If

                sInvAmounts = FormatCurrency(decInvoiceTotal)
                sInvoiceTotalLine = sInvAmounts '-SAVE to print below with payments.

                '- Show Payment info if NOT onAccount..
                sPaidAmounts = ""
                '- refund treated sep.
                If bIsRefund Then
                    '- decRefundCash, decRefundCreditNote, decRefundEftPosDr, decRefundEftPosCr 
                    If colInvoice.Contains("payment_id") AndAlso (colInvoice.Item("payment_id") > 0) Then
                        '- have payment record.
                        decRefundCash = colInvoice.Item("refundCashAmount")
                        decRefundCreditNote = colInvoice.Item("refundAsCreditNoteCredited")
                        decRefundEftPosDr = colInvoice.Item("refundAsEftPosDr")
                        decRefundEftPosCr = colInvoice.Item("refundAsEftPosCr")
                        sPaidAmounts = " REFUND As: Cash: " & FormatCurrency(decRefundCash, 2) & "; "
                        sPaidAmounts &= IIf((decRefundCreditNote <> 0), _
                                             " Cr-Note: " & FormatCurrency(decRefundCreditNote, 2) & "; ", "")
                        sPaidAmounts &= IIf((decRefundEftPosDr <> 0), _
                                              " EftPos Dr: " & FormatCurrency(decRefundEftPosDr, 2) & "; ", "")
                        sPaidAmounts &= IIf((decRefundEftPosCr <> 0), _
                                              " EftPos Cr: " & FormatCurrency(decRefundEftPosCr, 2) & "; ", "")
                        '-- UPDATE TOTALs..
                        '= decTotalInvoicePaid -= (decInvoiceAmtPaid)
                        '-- Disbursement not relevant fpr REFUND.
                        '= decTotalRefunds += decInvoiceTotal
                        '=decTotalCreditPaymentCredited += decRefundCreditNote
                    End If
                Else '-not refund-
                    If (Not bIsOnAccount) Then
                        sPaidAmounts = "Paid: " '=  & FormatCurrency(decInvoiceAmtPaid) & ";  ("
                        '-  Find/show cash/chq etc payment details this payment). ??
                        'If (dtPaymentDetails IsNot Nothing) AndAlso (dtPaymentDetails.Rows.Count > 0) Then
                        '    For Each rowDetail As DataRow In dtPaymentDetails.Rows
                        '        If (rowDetail.Item("payment_id") = colInvoice.Item("payment_id")) Then
                        '            sPaidAmounts &= rowDetail.Item("paymentType_key") & ": "
                        '            sPaidAmounts &= FormatCurrency(rowDetail.Item("amount"), 2) & ";  "
                        '        End If  '-this pay.
                        '    Next rowDetail
                        'End If
                        colPayDetails = colInvoice.Item("PaymentDetails")
                        For Each colDetail As Collection In colPayDetails
                            sPaidAmounts &= colDetail.Item("paymentType_key") & ": "
                            sPaidAmounts &= FormatCurrency(colDetail.Item("amount"), 2) & ";  "
                        Next colDetail
                        If (decCreditNoteAmountDebited > 0) Then
                            sPaidAmounts &= " CrNote-Redmd: " & FormatCurrency(decCreditNoteAmountDebited) & "; "
                        End If
                        If (decChangeGiven > 0) Then
                            sPaidAmounts &= "; Change: " & FormatCurrency(decChangeGiven) & "; "
                        End If
                        '-- should be mutually exclusive...
                        If (decCreditNotePaymentCredited > 0) Then
                            sPaidAmounts &= "; CrNote-Credited: " & FormatCurrency(decCreditNotePaymentCredited) & "; "
                        End If
                        sPaidAmounts &= ")"
                        '= decTotalInvoicePaid += (decInvoiceAmtPaid - decCreditNotePaymentCredited)
                        '= decTotalInvoicePaid += (decInvoiceAmtPaid)
                    Else  '-charged-
                        sPaidAmounts = " Charged: " & FormatCurrency(decAmtCharged) & ". "
                        '- show Account payments to  date ?
                        'If (decInvoiceAmtPaid > 0) Then
                        '    sPaidAmounts &= " (A/c paymnts on day this Inv.: " & FormatCurrency(decInvoiceAmtPaid) & ").."
                        'End If
                        '= decTotalAmtCharged += decAmtCharged
                    End If
                End If  '-refund.

                '=4201.0707=  Just one line for INVOICE total PLUS paid amts..
                sLine = "<textline>"
                sLine &= "<txt TAB=""" & k_TAB_TRANCODE & """ >---- ----- -----</txt>"  '--Flag last line of invoice.
                sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >" & "Invoice TOTAL: " & _
                                                         sInvoiceTotalLine & " [" & sPaidAmounts & "]..</txt>"
                sLine &= "</textline>"
                mColReportLines.Add(sLine)

                '--Show invoice total in Grid,
                colGridInvoice.Add(sInvoiceTotalLine) '= & " [" & sPaidAmounts & "]..")

                '==  Updates to 4233.0421  Started 24-April-2020= 
                '==  Target is new Build 4234..
                '--Show Nett Invoice profit in Grid,
                s1 = FormatNumber(decInvoiceNettProfit, 2)
                colGridInvoice.Add(s1)
                s1 = FormatNumber(0, 2) & "%"  '---Profit rate.
                colGridInvoice.Add(s1)
                '== END OF- PREP FOR- Updates to 4233.0421  Started 24-April-2020= 

                '--Show PaidAmounts as next col. in Grid,
                colGridInvoice.Add(" [" & sPaidAmounts & "]..")
                '--Show Staff in Grid,
                colGridInvoice.Add(sStaff)

                '= listPaymentTotals(listPaymentTotals.Count - 1) += decTotal '= CDec(col1("total"))
                intCount += 1  '--next docket..-

                '-- Blank Line..
                sLine = "<textline>"
                sLine &= "</textline>"
                mColReportLines.Add(sLine)

                '= s1 = "<drawline />"
                '- <VERTICALGAP_HALFLINE
                '=s1 = "<VERTICALGAP_HALFLINE />"
                '= mColReportLines.Add(s1)
                mFormWait1.labMsg.Text = sWaitMsg2 & intCount
                '-Save info for Grid this Invoice.
                mColGridReport.Add(colGridInvoice)

            Next colInvoice
            Call mWaitFormOff()

        Catch ex As Exception
            Call mWaitFormOff()
            MsgBox("Runtime Error building Sales Invoice list.." & vbCrLf & _
                    "Failed on Invoice # " & intInvoice & vbCrLf & _
                    "Report may not be correct.  Error is:" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            '= Exit Function
        End Try

        '-- All invoices done...
        DoEvents()

        '--  F i n a l  T o t a l s -

        Const k_TAB_ITEM_TOTAL As Integer = 440  '= 300  
        Const k_WIDTH_ITEM_TOTAL As Short = 120

        s1 = "<drawline />"
        mColReportLines.Add(s1)

        '--   need min 12 lines for totals..
        '=s1 = "<MinPageRoom Lines= ""26"" />"

        '==  Target-New-Build-4253..
        s1 = "<MinPageRoom Lines= ""40"" />"
        mColReportLines.Add(s1)

        '-- separate invoice lines from Final Totals.
        colGridInvoice = New Collection
        colGridInvoice.Add("--")
        mColGridReport.Add(colGridInvoice)

        '==  Target-New-Build-4253..
        '--  Rember start of totals.. For Totals-only query..
        mIntStartOfTotalsLines = mColReportLines.Count  '--StartOfTotalsLines in mColReportLines
        mIntStartOfTotalsGridLines = mColGridReport.Count  '--StartOfTotalsLines in mColGridReport


        '==  Target-New-Build-4253..
        '==  Target-New-Build-4253..
        '= -- REALLY ?  decTotalProductSales = decTotalInvoiced + decTotalDiscount - decTotalRounding - decTotalRefundsSubTotal

        sLine = "<textline>"
        sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >" & "OVERALL TOTALS for Period-" & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >-----</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)  '--blank line.

        '- make a grid total line.
        colGridInvoice = New Collection
        colGridInvoice.Add("--")
        colGridInvoice.Add("--")
        colGridInvoice.Add("=OVERALL TOTALS for Period=")  '-- in date column..
        mColGridReport.Add(colGridInvoice)

        '= sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  " & _
        '=   " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFreightCost_ex, 2) & "</txt>"



        '-- Total Sub-totals..
        sInvAmounts = FormatCurrency(decTotalSubTotal)
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >" & _
                                 "1. -- Sub-total (inc Tax " & FormatNumber(decTotalSubtotalTax, 2) & "): " & "</txt>"
        sLine &= "<txt TAB=""" & k_TAB_ITEM_TOTAL & """   align=""right""  " & _
                                " width = """ & k_WIDTH_ITEM_TOTAL & """ >" & sInvAmounts & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-- SubTotal for Grid.
        '- make a grid total line.
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("1. -- Sales Sub-total: ")  '-- in cust column..
        colGridInvoice.Add(sInvAmounts)  '-- in total column..
        mColGridReport.Add(colGridInvoice)


        '-Discount-
        '-- incluse decTotalDiscountTaxSales-
        sInvAmounts = FormatCurrency(decTotalDiscount)
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >" & _
                                  "2. -- Discount (Tax: " & FormatNumber(decTotalDiscountTaxSales, 2) & ")" & "</txt>"
        sLine &= "<txt TAB=""" & k_TAB_ITEM_TOTAL & """   align=""right""  " & _
                                   " width = """ & k_WIDTH_ITEM_TOTAL & """ >" & sInvAmounts & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-- Discount Total for Grid.
        '- make a grid total line.
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("2. -- Discount: ")  '-- in cust column..
        colGridInvoice.Add(sInvAmounts)  '-- in total column..
        mColGridReport.Add(colGridInvoice)


        '= Tax--
        sInvAmounts = "3. -- Nett Tax: " & FormatCurrency(decTotalTaxInvoiced) & "."
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >" & sInvAmounts & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-- TAX for Grid.
        '- make a grid total line.
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        '=colGridInvoice.Add("-- Discount: ")  '-- in cust column..
        colGridInvoice.Add(sInvAmounts)  '-- Tax..
        mColGridReport.Add(colGridInvoice)


        sInvAmounts = FormatCurrency(decTotalInvoiced)
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & k_TAB_INV_AMT & """ >" & "4. -- Total Sales Invoices: " & "</txt>"
        sLine &= "<txt TAB=""" & k_TAB_ITEM_TOTAL & """   align=""right""  " & _
                           " width = """ & k_WIDTH_ITEM_TOTAL & """ >" & sInvAmounts & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & (k_TAB_INV_AMT + 4) & """ >  (ie. Sales, before Refunds)..</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-Sales for Grid..
        '- make a grid total line.
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("4. -- Total Sales Invoices: ")  '-- in cust column..
        colGridInvoice.Add(sInvAmounts)  '-- Tax..
        mColGridReport.Add(colGridInvoice)


        '==  Target-New-Build-4253..
        '==  Target-New-Build-4253..
        '---  Show Paid/Charged on one line..
        '-- Paid/Charged-
        sInvAmounts = "    [Paid: " & FormatCurrency(decTotalInvoicePaid, 2) & "; "
        sInvAmounts &= " Charged: " & FormatCurrency(decTotalAmtCharged, 2) & "]"
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & (k_TAB_INV_AMT + 4) & """ >" & sInvAmounts & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-Paid/Charged for Grid..
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        '=  colGridInvoice.Add("-- REFUNDS: ")  '-- in cust column..
        colGridInvoice.Add(sInvAmounts)
        mColGridReport.Add(colGridInvoice)

        ''--invoices Paid.
        'sPaidAmounts = FormatCurrency(decTotalInvoicePaid, 2)
        'sLine = "<textline>"
        'sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & LSet("5. -- Invoices Paid: ", 24) & "</txt>"
        'sLine &= "<txt  TAB=""" & k_TAB_DESCR & """   align=""right""  " & _
        '                              " width = """ & k_WIDTH_ITEM_TOTAL & """ > " & sPaidAmounts & "</txt>"
        'sLine &= "</textline>"
        'mColReportLines.Add(sLine)

        ''-Invoices Paid for Grid..
        ''- make a grid total line.
        'colGridInvoice = New Collection
        'colGridInvoice.Add("--") '-skip column
        'colGridInvoice.Add("--")
        'colGridInvoice.Add("")
        'colGridInvoice.Add("5. -- Invoices Paid: ")  '-- in cust column..
        'colGridInvoice.Add(sPaidAmounts)  '-- Cash Sales..
        'mColGridReport.Add(colGridInvoice)


        'sPaidAmounts = FormatCurrency(decTotalAmtCharged, 2)
        ''= sPaidAmounts &= ";  C/Note redeemed.: " & FormatCurrency(decTotalCreditNoteDebited, 2) & ";"
        'sLine = "<textline>"
        'sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & LSet("6. -- Invoices Charged: ", 24) & "</txt>"
        'sLine &= "<txt  TAB=""" & k_TAB_DESCR & """   align=""right""  " & _
        '                           " width = """ & k_WIDTH_ITEM_TOTAL & """ > " & sPaidAmounts & "</txt>"
        'sLine &= "</textline>"
        'mColReportLines.Add(sLine)

        ''-Invoices Charged for Grid..
        ''- make a grid total line.
        'colGridInvoice = New Collection
        'colGridInvoice.Add("--") '-skip column
        'colGridInvoice.Add("--")
        'colGridInvoice.Add("")
        'colGridInvoice.Add("6. -- Invoices Charged: ")  '-- in cust column..
        'colGridInvoice.Add(sPaidAmounts)
        'mColGridReport.Add(colGridInvoice)

        '== END of  Target-New-Build-4253.. paid/charged.
        '== END of  Target-New-Build-4253.. paid/charged.
        '== END of  Target-New-Build-4253.. paid/charged.



        sLine = "<textline>"
        '=sLine &= "<txt>----</txt>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & "-------" & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)  '--sep. line.

        '- make a gridSep. line.
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("---------")  '-- in cust column..
        '== colGridInvoice.Add(sInvAmounts)
        mColGridReport.Add(colGridInvoice)


        '-- REfunds-
        sPaidAmounts = FormatCurrency(decTotalRefunds, 2)
        '= sPaidAmounts &= ";  C/Note redeemed.: " & FormatCurrency(decTotalCreditNoteDebited, 2) & ";"
        sLine = "<textline>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & "5. -- REFUNDS: " & "</txt>"
        sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL & """   align=""right""  " & _
                                   " width = """ & k_WIDTH_ITEM_TOTAL & """ > " & sPaidAmounts & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-REFUNDS for Grid..
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("5. -- REFUNDS: ")  '-- in cust column..
        colGridInvoice.Add(sPaidAmounts)
        mColGridReport.Add(colGridInvoice)

        '-- REFUNDS Sub-total-
        sInvAmounts = " [SubTotal: " & FormatCurrency(decTotalRefundsSubTotal, 2) & "; "
        '-decTotalRefundSubtotalTax - decTotalDiscountTaxRefunds-
        sInvAmounts &= " (Tax: " & FormatCurrency(decTotalRefundSubtotalTax, 2) & "); "
        sInvAmounts &= " Discount: " & FormatCurrency(decTotalRefundsDiscount, 2) & _
                            " (tax " & FormatNumber(decTotalDiscountTaxRefunds, 2) & ");"
        sInvAmounts &= " NettTax: " & FormatCurrency(decTotalRefundsTax, 2) & "] "
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & (k_TAB_INV_AMT + 4) & """ >" & sInvAmounts & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-REFUNDS SUBTOTAL for Grid..
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        '=  colGridInvoice.Add("-- REFUNDS: ")  '-- in cust column..
        colGridInvoice.Add(sInvAmounts)
        mColGridReport.Add(colGridInvoice)




        '==  Target-New-Build-4253..
        '==  Target-New-Build-4253..
        '==  -- Sales Report- Show Nett sales after Refunds; check we show Profit After Discount.

        Dim decNettSalesTotal As Decimal = decTotalInvoiced - decTotalRefunds
        Dim decNettSalesSubTotal As Decimal = decTotalSubTotal - decTotalRefundsSubTotal
        Dim decNettSalesTax As Decimal = decTotalTaxInvoiced - decTotalRefundsTax
        Dim decNettSalesDiscount As Decimal = decTotalDiscount - decTotalRefundsDiscount
        '- get discount nett of tax  ("ex")
        Dim decNettDiscountTax As Decimal = decTotalDiscountTaxSales - decTotalDiscountTaxRefunds
        Dim decNettDiscount_ex As Decimal = decNettSalesDiscount - decNettDiscountTax

        '-- SHOW Nett Sales with breakdown..
        sLine = "<textline>"
        '=sLine &= "<txt>----</txt>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & "-------" & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)  '--sep. line.

        '- make a gridSep. line.
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("---------")  '-- in cust column..
        '== colGridInvoice.Add(sInvAmounts)
        mColGridReport.Add(colGridInvoice)

        '-- Nett Sales-
        sPaidAmounts = FormatCurrency(decNettSalesTotal, 2)
        '= sPaidAmounts &= ";  C/Note redeemed.: " & FormatCurrency(decTotalCreditNoteDebited, 2) & ";"
        sLine = "<textline>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & "6. -- NETT SALES: " & "</txt>"
        sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL & """   align=""right""  " & _
                                   " width = """ & k_WIDTH_ITEM_TOTAL & """ > " & sPaidAmounts & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-NETT SALES for Grid..
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("6. -- NETT SALES: ")  '-- in cust column..
        colGridInvoice.Add(sPaidAmounts)
        mColGridReport.Add(colGridInvoice)

        '-- NETT SALES Sub-total-
        sInvAmounts = " [SubTotal: " & FormatCurrency(decNettSalesSubTotal, 2) & "; "
        sInvAmounts &= " (Tax: " & FormatNumber((decTotalSubtotalTax - decTotalRefundSubtotalTax), 2) & ")  "
        sInvAmounts &= " Discount: " & FormatCurrency(decNettSalesDiscount, 2) & _
                                          " (tax " & FormatNumber(decNettDiscountTax, 2) & ");" '- "; "
        sInvAmounts &= " NettTax: " & FormatCurrency(decNettSalesTax, 2) & "]; "
        sLine = "<textline>"
        sLine &= "<txt TAB=""" & (k_TAB_INV_AMT + 4) & """ >" & sInvAmounts & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-NETT SALES SUBTOTAL for Grid..
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        '=  colGridInvoice.Add("-- REFUNDS: ")  '-- in cust column..
        colGridInvoice.Add(sInvAmounts)
        mColGridReport.Add(colGridInvoice)

        '==  END of  Target-New-Build-4253..
        '==  END of  Target-New-Build-4253..
        '==  END of  Target-New-Build-4253..




        '==  Updates to 4233.0421  Started 24-April-2020= 
        '==  Target is new Build 4234..
        '--   SHOW TOTAL Gross profit Amount..
        sLine = "<textline>"
        '=sLine &= "<txt>----</txt>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & "-------" & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)  '--sep. line.

        '- make a gridSep. line.
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("---------")  '-- in cust column..
        '== colGridInvoice.Add(sInvAmounts)
        mColGridReport.Add(colGridInvoice)

        ''=decFinalTotalNettProfit=
        'Dim sTotalProfit As String = FormatCurrency(decFinalTotalNettProfit, 2)
        'sLine = "<textline>"
        'sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & LSet("11. -- Total Profit: ", 24) & "</txt>"
        'sLine &= "<txt  TAB=""" & k_TAB_DESCR & """   align=""right""  " & _
        '                           " width = """ & k_WIDTH_ITEM_TOTAL & """ > " & sTotalProfit & "</txt>"
        'sLine &= "</textline>"
        'mColReportLines.Add(sLine)

        ''--Total profit for Grid.
        'colGridInvoice = New Collection
        'colGridInvoice.Add("--") '-skip column
        'colGridInvoice.Add("--")
        'colGridInvoice.Add("")
        'colGridInvoice.Add("-- Total Profit: ")  '-- in cust column..
        'colGridInvoice.Add(sTotalProfit)
        'mColGridReport.Add(colGridInvoice)
        '==  END OF PREP FOR- Target is new Build 4234..


        '==  Target-New-Build-4253..
        '==  Target-New-Build-4253..

        s1 = "<drawline />"
        mColReportLines.Add(s1)

        '--NETT Product Sales  (ex tax)..
        Dim decNettProductSales As Decimal = decTotalProductSales - decTotalRefundsProductSales

        sLine = "<textline>"
        'sLine &= " <txt> -- Nett product Sales (ex tax.) Before Disc. After Refunds: " & _
        '                                                          FormatCurrency(decTotalProductSales, 2) & ".</txt>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & "7. -- Nett Prod. Sales (ex): " & "</txt>"
        sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL & """   align=""right""  " & _
                                   " width = """ & k_WIDTH_ITEM_TOTAL & """ > " & FormatCurrency(decNettProductSales, 2) & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '-Product Sales for Grid..
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("-- ")
        colGridInvoice.Add("7. -- Nett Prod. Sales (ex tax..) Before Disc. After Refunds:")  '-- in cust column..
        colGridInvoice.Add(FormatCurrency(decNettProductSales, 2))
        mColGridReport.Add(colGridInvoice)
        '= = = = = =

        '--Gross Profit from Sales.. Before discount..

        '=decFinalTotalNettProfit has been decounted.=
        '-- add back discount.
        '-- AND compute Rate of profit..
        Dim decFinalProfitBeforeDiscount As Decimal = (decFinalTotalNettProfit + decNettDiscount_ex)

        '==   Target-New-Build-4277 -- (Started 07-October-2020)
        '==     POS Sales-  Fix DivideByZero Runtime Error in Sales Invoice Report.
        Dim decProfitRate1 As Decimal = 0   '= (decFinalProfitBeforeDiscount / decNettProductSales) * 100
        If (decNettProductSales > 0) Then
            decProfitRate1 = (decFinalProfitBeforeDiscount / decNettProductSales) * 100
        End If
        '== END  Target-New-Build-4277 -- (Started 07-October-2020)

        '-- 
        Dim sTotalProfit As String = FormatCurrency((decFinalTotalNettProfit + decNettDiscount_ex), 2)
        Dim sFinalProfitRate As String = FormatNumber(decProfitRate1, 2)
        sLine = "<textline>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & _
                                       "8. -- Gross Profit Pre Discount (" & sFinalProfitRate & "%):</txt>"
        sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL & """   align=""right""  " & _
                                   " width = """ & k_WIDTH_ITEM_TOTAL & """ > " & sTotalProfit & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '--Total profit for Grid.
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("8.-- Gross Profit before Discount (" & sFinalProfitRate & "%):")  '-- in cust column..
        colGridInvoice.Add(sTotalProfit)
        mColGridReport.Add(colGridInvoice)


        '-- Nett Trading profit..
        '-- Nett Trading profit..
        '-- Nett Trading profit..

        '==   Target-New-Build-4277 -- (Started 07-October-2020)
        '==     POS Sales-  Fix DivideByZero Runtime Error in Sales Invoice Report.
        '==decProfitRate1 = (decFinalTotalNettProfit / decNettProductSales) * 100
        decProfitRate1 = 0  '=(decFinalTotalNettProfit / decNettProductSales) * 100
        If (decNettProductSales > 0) Then
            decProfitRate1 = (decFinalTotalNettProfit / decNettProductSales) * 100
        End If
        '== END  Target-New-Build-4277 -- (Started 07-October-2020)

        sFinalProfitRate = FormatNumber(decProfitRate1, 2)

        '=decFinalTotalNettProfit=
        sTotalProfit = FormatCurrency(decFinalTotalNettProfit, 2)
        sLine = "<textline>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & _
                          "9. -- Nett Profit after Discount_ex [" & _
                                        FormatCurrency(decNettDiscount_ex, 2) & "] " & _
                                                    "(" & sFinalProfitRate & "%):</txt>"
        sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL & """   align=""right""  " & _
                                   " width = """ & k_WIDTH_ITEM_TOTAL & """ > " & sTotalProfit & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '--Total profit for Grid.
        colGridInvoice = New Collection
        colGridInvoice.Add("--") '-skip column
        colGridInvoice.Add("--")
        colGridInvoice.Add("")
        colGridInvoice.Add("9.-- Nett Profit after Discount_ex [" & _
                                       FormatCurrency(decNettDiscount_ex, 2) & "] (" & _
                                           sFinalProfitRate & "%):")  '-- in cust column..
        colGridInvoice.Add(sTotalProfit)
        mColGridReport.Add(colGridInvoice)


        '==  END of  Target-New-Build-4253..
        '==  END of  Target-New-Build-4253..


        s1 = "<drawline />"
        mColReportLines.Add(s1)

        s1 = "Please Note: This is a Sales Invoice List only.. "
        sLine = "<textline>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & s1 & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        s1 = "  For full Payment/Till details, see Payments Report for the Period, "
        sLine = "<textline>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & s1 & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        s1 = "  or the Cashup Till Listing for the session."
        sLine = "<textline>"
        sLine &= "<txt  TAB=""" & k_TAB_INV_AMT & """ > " & s1 & "</txt>"
        sLine &= "</textline>"
        mColReportLines.Add(sLine)

        '= btnPrintReport.Enabled = True

        '- make a grid total line.
        colGridInvoice = New Collection
        colGridInvoice.Add("==")
        colGridInvoice.Add("==")
        colGridInvoice.Add("=== the end ====")  '-- in date column..
        mColGridReport.Add(colGridInvoice)


        '-- show report preview.
        '--load report info and show--
        s1 = "<drawline fontstyle=""bold"" />"
        mColReportLines.Add(s1)

        mbMakeSalesInvoiceReport = True

    End Function  '--MakeSalesInvoiceReport-
    '= = = = = = = = = = ==  == = ==
    '-===FF->

    '-- new--constructor..
    '-- new--constructor..

    Public Sub New(ByRef frmParent As Form, _
                   ByRef cnn1 As OleDbConnection, _
                   ByVal sSqlDbName As String)

        MyBase.New()
        mFrmParent = frmParent

        mCnnSql = cnn1

        msSqlDbName = sSqlDbName
        '-msWhereCondition = sWhereCondition

    End Sub  '-new-
    '= = = = = = == =  =

    '-GetSalesInvoiceReport-

    Public Function GetSalesInvoiceReport(ByVal sWhereCondition As String, _
                                             ByVal bShowInvoiceLines As Boolean, _
                                             ByRef colReportLines As Collection, _
                                             ByRef colGridReport As Collection) As Boolean

        GetSalesInvoiceReport = False
        If mbMakeSalesInvoiceReport(sWhereCondition, bShowInvoiceLines) Then
            '-ok-
            colReportLines = mColReportLines
            colGridReport = mColGridReport

            GetSalesInvoiceReport = True
        End If


    End Function  '-GetSalesInvoiceReport-
    '= = = = = = = = = = = == = = = = = =


    '-- get totals section from last report run..

    Public Function GetSalesInvoiceTotalsReport(ByRef colReportLines As Collection, _
                                                  ByRef colGridReport As Collection) As Boolean
        GetSalesInvoiceTotalsReport = False

        If (mIntStartOfTotalsLines <= 0) Or (mIntStartOfTotalsGridLines <= 0) Then
            MsgBox("Must run full report first..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        '-  ok- make new collections for totals..
        '-  ok- make new collections for totals..
        Dim intIdx As Integer
        colReportLines = New Collection
        colGridReport = New Collection

        For intIdx = mIntStartOfTotalsLines To mColReportLines.Count
            colReportLines.Add(mColReportLines.Item(intIdx))
        Next intIdx

        For intIdx = mIntStartOfTotalsGridLines To mColGridReport.Count
            colGridReport.Add(mColGridReport.Item(intIdx))
        Next intIdx

        GetSalesInvoiceTotalsReport = True

    End Function  '-GetSalesInvoiceTotalsReport-
    '= = = = = = = = = = == = == ======= =
    '-===FF->




End Class  '-clsSalesInvoiceReport-
'= = = = = = = = = ==  ==== = = = == 
