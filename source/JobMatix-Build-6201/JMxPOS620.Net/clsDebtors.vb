
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data

Imports System.Data.OleDb
Imports System.Math
Imports System.ComponentModel
Imports System.Threading

Public Class clsDebtors

    '-- 09-October-2019--
    '==  Class to gather and hold all current Debtors info-
    '==   For Statements and Debtors Report..
    '= = =
    '==  4201.10xx..  20October-2019-
    '==     Rewrite retrieval Query and code to speed things up..
    '== 
    '==  NB:  Before this latest version (4201.1023 ?), 
    '==        some Payment disbursements may not contain the CreditNote amount included in the payment..
    '==       This scraping/collecting of Invoices/Disbursements for Debtors/Statements
    '==              will check for these and make corrections.
    '==         (NOT IMPLEMENTD- PROBABLY NOT NEEDED)
    '--     
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = =
    '==
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==    Account Invoice Reversals.. 
    '==
    '==   MAIN THEME HERE is implementation of ACCOUNT-INVOICE-REVERSAL (Account "refund")
    '==       --  Involves creating a REFUND (onAccount) full transaction as a mirror of Original.
    '==       --  Not allowed if payments have been made towards original Invoice.
    '==       --  Not allowed if original Invoice involved DELIVERY of a Job or a Layby..
    '==       --  Reversal Transaction is accessible only from frmShowInvoice (showing original Invoice)..
    '==                 Needs NEW CLASS  "clsAccountReversal".. 
    '==       --  Transaction needs SUPERVISOR PASSWORD...
    '==
    '==       --  Here in Debtors Statements we collect all Account Refunds (Invoice Reversals)
    '==              at the start, and then chack each Invoice for a matching reversal (Refund).
    '==               AND keep that invoice aside in a separate collection for Customer..
    '=== = = = 
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==Fixes to Build 4257.0707  
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '==
    '==  Account Invoice Reversal- 
    '==      -- Payments Form needs ReversedInvoices to be filtered out..
    '==           ALSO before committing payment, check that Invoices were not changed in the meantime..
    '==             (NEEDS "gbCollectCustomerInvoices" to have option to be in a Transaction.)
    '==
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '--   SO here we need to make "mbCollectAllAccountReversals()" accessible to PUBLIC..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = =
    '==
    '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
    '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
    '== 
    '==  -- clsDebtors-  FIX crash that happens when getting invoices and NONE are on file
    '==                    (eg for invoices for a particular customer.)
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = =
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '== Fixes to Build 4267.0929  
    '==
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==
    '==  (a) POS- Reversals to Payments that included Saved Credits Notes-   
    '==        -- Credit note amounts are being treated As credits To Customer instead Of showing As debit (reversed).  
    '==          See "gbGetCreditNoteHistory" CreditNote Report, 
    '==               And CreditNote balance On Sale Screen...
    '==                AND  clsDebtors ("mbLoadCreditNoteBalances")..
    '==          IF Payment record is a REVERSAL, then "creditNotePaymentCredited" and "creditNoteAmountDebited" need reversing.
    '==
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = = = = == 


    '==   Updated.- 3519.0404..
    Private Const k_POS_MAX_TRIALDAYS As Int32 = 90

    Private mCnnSql As OleDbConnection  '=  ADODB.Connection
    Private mbIsSqlAdmin As Boolean = False

    Private msServer As String = ""

    Private msSqlVersion As String = ""
    Private msInputDBNameJobs As String = ""

    Private mbIsLoading As Boolean = False
    Private mbCloseRequested As Boolean = False

    Private msSqlDbName As String = ""
    Private mColSqlDBInfo As Collection '--  jobs DB info--
    '= Private mIntStaff_id As Integer = -1
    '= Private msStaffName As String = ""
    Private msRuntimeLogPath As String = ""

    Private msMachineName As String = "" '--local machine--
    Private msComputerName As String = "" '--client or Fat machine--
    Private mbIsThinClient As Boolean = False

    Private msAppPath As String = ""
    Private msDllversion As String = ""

    Private mSysInfo1 As clsSystemInfo

 
    '=3403.1010=-- now split server/instance..--
    Private msSqlServerComputer As String = ""
    Private msSqlServerInstance As String = ""

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""

    Private msVersionPOS As String = ""

    Private mFormWait1 As frmWait

    Private mColPrefsCustomer As Collection
    Private mImageUserLogo As Image

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1
    '=3403.1102=
    '= Private mIntRequestedCustomer_id As Integer = -1

    Private mColReportCustomers As Collection

    Private mDicCreditNoteBalances As Dictionary(Of Integer, Decimal) '-- key is customer_id-..
    '-- FOR testing, track names also..
    Private mDicCreditNoteCustNames As Dictionary(Of Integer, String) '-- key is customer_id-..

    Private mbInfoLoadedOK As Boolean = False

    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==    Account Invoice Reversals.. 
    Private mColAllAccountReversals As Collection
    '== END OF  Target is new Build 4251..
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String)

        mFormWait1 = New frmWait
        mFormWait1.waitTitle = msVersionPOS
        mFormWait1.labHdr.Text = "Debtors Statements-"
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.Show()
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

    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==    Account Invoice Reversals.. 

    '-- Collect all Reversals.
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '--   SO here we need to make "mbCollectAllAccountReversals()" PUBLIC..

    '-mbCollectAllAccountReversals-

    Private Function mbCollectAllAccountReversals(Optional ByVal bIsTransaction As Boolean = False, _
                                                    Optional ByRef oledbTransaction1 As OleDbTransaction = Nothing) As Boolean
        Dim sSql, sErrors As String
        Dim datatable1 As DataTable
        Dim intOriginal_id As Integer
        Dim colRefund As Collection

        mbCollectAllAccountReversals = False

        mColAllAccountReversals = New Collection

        sSql = " SELECT * FROM [dbo].[Invoice]"
        sSql &= " WHERE (invoice.transactionType = 'refund') "
        sSql &= "   AND (isOnAccount = 1) AND (invoice.original_id >0); "

        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        Dim bOk As Boolean
        If bIsTransaction Then
            bOk = gbGetDataTableEx(mCnnSql, datatable1, sSql, oledbTransaction1)
        Else  '-not transaction-
            bOk = gbGetDataTable(mCnnSql, datatable1, sSql)
        End If
        '==  END Target-New-Build 4259 -- (Started 17-Jul-2020)


        If Not bOk Then  '= 4259= gbGetDataTable(mCnnSql, datatable1, sSql) Then
            MsgBox("Failed Looking up AccountRefunds.. " & vbCrLf & _
                      gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        Else '--ok-
            If (datatable1.Rows.Count > 0) Then
                '-- have some-
                For Each datarow1 As DataRow In datatable1.Rows
                    '-  save each refund..
                    colRefund = New Collection
                    intOriginal_id = datarow1.Item("original_id")
                    colRefund.Add(datarow1.Item("invoice_id"), "invoice_id")
                    colRefund.Add(intOriginal_id, "original_id")

                    colRefund.Add(datarow1.Item("invoice_date"), "invoice_date")
                    colRefund.Add(datarow1.Item("customer_id"), "customer_id")
                    colRefund.Add(datarow1.Item("transactionType"), "transactionType")
                    colRefund.Add(datarow1.Item("isOnAccount"), "isOnAccount")
                    colRefund.Add(datarow1.Item("total_inc"), "total_inc")
                    '-- save refund (invoice reversal) with Original Invoice No as key.
                    mColAllAccountReversals.Add(colRefund, CStr(intOriginal_id))
                Next datarow1
            End If  '-count-
            mbCollectAllAccountReversals = True
        End If  '-get-

    End Function  '-mbCollectAllAccountReversals-

    '== END OF  Target is new Build 4251..
    '== END OF  Target is new Build 4251..

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->


    '-- Load Credit Note balances..
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==   Target-New-Build-4277 -- (Started 07-October-2020)
    '==
    '==  (a) POS- Reversals to Payments that included Saved Credits Notes-   
    '==        -- Credit note amounts are being treated As credits To Customer instead Of showing As debit (reversed).  
    '==          See "gbGetCreditNoteHistory" CreditNote Report, 
    '==               And CreditNote balance On Sale Screen...
    '==                AND  clsDebtors ("mbLoadCreditNoteBalances")..
    '==          IF Payment record is a REVERSAL, then "creditNotePaymentCredited" and "creditNoteAmountDebited" need reversing.
    '==


    Private Function mbLoadCreditNoteBalances() As Boolean

        '==         --  Debtors Report and Statements. 
        '==               Show Credit-Note Available balance and Cust. Phone No....
        '--  SO- Collect ALL Credit Note balances
        '--      so we can attach balance to customer collection.
        Dim dtCreditNotes As DataTable
        Dim decTotalCredits, decTotalDebits, decCreditNoteCreditRemaining As Decimal
        Dim decCrbal, decOldBal, decTotalCredit As Decimal
        Dim intThisCustId As Integer
        Dim sThisCustName, s1 As String

        '==   Target-New-Build-4277 -- (Started 07-October-2020)
        Dim bIsReversal As Boolean
        '==  END Target-New-Build-4277 -- (Started 07-October-2020)


        mbLoadCreditNoteBalances = False

        mDicCreditNoteBalances = New Dictionary(Of Integer, Decimal)
        mDicCreditNoteCustNames = New Dictionary(Of Integer, String)
        decTotalCredit = 0

        '- get all credit notes.. (ALL custs.).
        If Not gbGetCreditNoteHistory(mCnnSql, -1, dtCreditNotes, _
                                             decTotalCredits, decTotalDebits, decCreditNoteCreditRemaining) Then
            MsgBox("Failed Looking up credit notes.. ", MsgBoxStyle.Exclamation)

        Else  '-ok-
            If (dtCreditNotes IsNot Nothing) AndAlso (dtCreditNotes.Rows.Count > 0) Then
                Dim intTrCount As Integer = dtCreditNotes.Rows.Count
                '-- load all credit balances into collection.

                '-EACH ROW is a PAYMENT record !!-
                '-EACH ROW is a PAYMENT record !!-
                '-- DataTable is in order of Cust-name, so we can't use that as ID/Order etc
                '-- Sumarise each payment for Credit-Nore Bal, and add it to Customer dictionary.
                For Each rowCN1 As DataRow In dtCreditNotes.Rows
                    '-- get Account Custs only..
                    If (CInt(rowCN1.Item("isAccountCust")) <> 0) Then  '-is account..

                        '==   Target-New-Build-4277 -- (Started 07-October-2020)
                        bIsReversal = (rowCN1.Item("isReversal") <> 0)
                        '== END  Target-New-Build-4277 -- (Started 07-October-2020)

                        '--get customer ID from payment..
                        intThisCustId = CInt(rowCN1.Item("customer_id"))
                        sThisCustName = CStr(rowCN1.Item("customerName"))
                        decCrbal = 0
                        If (CDec(rowCN1.Item("creditNotePaymentCredited")) <> 0) Then

                            '== Target-New-Build-4277 -- (Started 07-October-2020)
                            '==  ---  decCrbal += CDec(rowCN1.Item("creditNotePaymentCredited"))
                            If bIsReversal Then
                                decCrbal -= CDec(rowCN1.Item("creditNotePaymentCredited"))
                            Else '-not-
                                decCrbal += CDec(rowCN1.Item("creditNotePaymentCredited"))
                            End If
                            '== END Target-New-Build-4277 -- (Started 07-October-2020)

                        ElseIf (CDec(rowCN1.Item("refundAsCreditNoteCredited")) <> 0) Then
                            decCrbal += CDec(rowCN1.Item("refundAsCreditNoteCredited"))
                        ElseIf (CDec(rowCN1.Item("creditNoteAmountDebited")) <> 0) Then

                            '== Target-New-Build-4277 -- (Started 07-October-2020)
                            '==  -- decCrbal -= CDec(rowCN1.Item("creditNoteAmountDebited"))
                            If bIsReversal Then
                                decCrbal += CDec(rowCN1.Item("creditNoteAmountDebited"))
                            Else  '-not-
                                decCrbal -= CDec(rowCN1.Item("creditNoteAmountDebited"))
                            End If  '-reversal-
                            '==END Target-New-Build-4277 -- (Started 07-October-2020)

                        End If
                        '--If some movement then, Create cust entry, or add to it..
                        If decCrbal <> 0 Then
                            '= decTotalCredit += decCrbal
                            If mDicCreditNoteBalances.ContainsKey(intThisCustId) Then
                                decOldBal = mDicCreditNoteBalances.Item(intThisCustId)
                                mDicCreditNoteBalances.Item(intThisCustId) = decOldBal + decCrbal
                            Else  '-new one.
                                mDicCreditNoteBalances.Add(intThisCustId, decCrbal)
                                '-testing-
                                mDicCreditNoteCustNames.Add(intThisCustId, sThisCustName)
                            End If  '-contains.
                        End If  '-zero-
                    End If  '--account-
                Next rowCN1
                '= colCreditNoteBalances.Add(decCrbal, sThisCustId)
            End If  '-nothing-
        End If  '-get-
        '--testing-  show complete list of cr-note balances..
        s1 = ""
        decTotalCredit = 0
        For Each kvpBal As KeyValuePair(Of Integer, Decimal) In mDicCreditNoteBalances
            intThisCustId = kvpBal.Key
            decCrbal = kvpBal.Value
            decTotalCredit += decCrbal
            s1 &= intThisCustId & ": " & mDicCreditNoteCustNames.Item(intThisCustId) & ": " & _
                                                                     FormatCurrency(kvpBal.Value, 2) & vbCrLf
        Next kvpBal
        '-test msg..
        'MsgBox("Testing-  " & mDicCreditNoteBalances.Count & _
        '            " Credit Note Balances: Total= " & FormatCurrency(decTotalCredit, 2) & _
        '                                                   vbCrLf & s1, MsgBoxStyle.Information)
        '-- DONE getting credit notes..
        '-- DONE getting credit notes..
        mbLoadCreditNoteBalances = True

    End Function  '-Load Credit Note balances-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- New Query to get all Debtors info..
    '==  15-Oct-2019=

    Private Function mbLoadAllAccountInvoices(ByVal bOutstandingInvoicesOnly As Boolean, _
                                               ByVal IntClosedDaysToShow As Integer, _
                                                ByVal dateCutoff As Date, _
                                                Optional ByVal intRequestedCustomer_id As Integer = -1) As Boolean
        Dim colInvoices As Collection
        Dim decTotalInvoices As Decimal
        Dim decTotalOutstanding As Decimal
        Dim sInvoicesSql As String = ""
        Dim sList As String = ""
        Dim s1, s2 As String
        Dim rdrInvoices As OleDbDataReader
        Dim cmd1 As OleDbCommand
        Dim colReportCustomers, colThisCustomer, colThisInvoice As Collection
        Dim colCustInfo, colRefunds As Collection
        Dim colThisCustomerInvoices, colThisInvoicePayments, colThisPayment As Collection
        Dim decCrbal As Decimal

        '==  Target is new Build 4251..
        '==    Account Invoice Reversals.. 
        Dim colThisCustomerReversedInvoices As Collection
        '== END OF  Target is new Build 4251..

        '- collect payments with Cr-note debits for checking..
        '= Dim colAllPaymentsWithCrNote, colCustPaymentIDs As Collection  '- key is cstr(payment Id).
        '= Dim colPayDisbs As Collection

        mbLoadAllAccountInvoices = False

        Call mbLoadCreditNoteBalances()

        '== Get ALL account sales, JOIN Payments and Customers..
        '== Get ALL account sales, JOIN Payments and Customers..

        sInvoicesSql = "SELECT lastName, firstName, companyName, customer.barcode, "
        sInvoicesSql &= " address, suburb, state, postcode, country, phone, mobile, customer.email, "
        sInvoicesSql &= " creditDays, creditLimit, doNotEmailDocuments, "
        sInvoicesSql &= " CASE customer.companyName "
        sInvoicesSql &= "   WHEN '' THEN customer.lastName + ', ' + customer.firstName + '_' + customer.barcode "
        sInvoicesSql &= "    ELSE customer.companyName  + '_' + customer.barcode "
        sInvoicesSql &= "   END  AS custShortName, "
        sInvoicesSql &= "  invoice.customer_id, invoice.invoice_id, invoice.transactionType, isOnAccount,"
        sInvoicesSql &= "  invoice.invoice_date, invoice.total_inc ,invoice.total_tax ,"
        sInvoicesSql &= "  payments.payment_id,  payments.payment_date,  payments.isReversal,"
        sInvoicesSql &= "  payments.nettAmountCredited, payments.creditNoteAmountDebited, "
        sInvoicesSql &= "  PD.invoice_id, PD.amount AS disb_amount, "
        sInvoicesSql &= "  PD.tranCode as disbTranCode, PD.sourceOfFunds "
        sInvoicesSql &= " FROM [dbo].[Invoice]"
        sInvoicesSql &= "    LEFT OUTER JOIN PaymentDisbursements PD ON (PD.invoice_id =invoice.invoice_id)"
        sInvoicesSql &= "    LEFT OUTER JOIN dbo.payments on (PD.payment_id=payments.payment_id)"
        sInvoicesSql &= "    LEFT OUTER JOIN dbo.customer ON (customer.customer_id =invoice.customer_id)"
        sInvoicesSql &= " WHERE (invoice.transactionType = 'sale')  AND (isOnAccount = 1) "
        If IsDate(dateCutoff) Then
            sInvoicesSql &= " AND (invoice_date <=  '" & Format(dateCutoff, "dd-MMM-yyyy 23:59") & "')"
        End If
        If (intRequestedCustomer_id > 0) Then  '-individual customer requested.
            sInvoicesSql &= "  AND (invoice.customer_id=" & CStr(intRequestedCustomer_id) & ") "
        End If
        sInvoicesSql &= "   ORDER BY custShortName, invoice.invoice_id;"

        cmd1 = New OleDbCommand(sInvoicesSql, mCnnSql)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            rdrInvoices = cmd1.ExecuteReader
        Catch ex As Exception
            MsgBox("Error in getting recordset for Invoices Reader: " & vbCrLf & _
                                            ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--

        colReportCustomers = New Collection
        '- per customer.
        '= colAllPaymentsWithCrNote = New Collection

        Dim intThisCustomerId, intThisInvoiceId, intRdrCount As Integer
        Dim IntCustId1, intInvId1 As Integer

        Dim intLastCustId As Integer = -1
        Dim intLastInvoiceId As Integer = -1
        Dim sName As String
        '= These per invoice.
        Dim decInvoiceTotal, decTotalTax, decAmountOutstanding As Decimal
        Dim decPaymentAmount, decPaymentTotal As Decimal
        Dim bIsReversal As Boolean
        Dim decCreditNoteAmountDebited As Decimal
        Dim colx As Collection
        Dim dateThisInvoice, dateClosedInvoicesToShow As Date

        dateClosedInvoicesToShow = DateAdd(DateInterval.Day, -IntClosedDaysToShow, Today)

        intRdrCount = 0  '-- count rows..

        '- COLLECT Customers/Invoices/Paymnt-Disbursements..
        colRefunds = New Collection  ''-will aways be empty.

        If Not (rdrInvoices Is Nothing) Then
            If rdrInvoices.HasRows Then
                While rdrInvoices.Read
                    intRdrCount += 1  '--count rdr rows..
                    IntCustId1 = CInt(rdrInvoices.Item("customer_id"))
                    intInvId1 = CInt(rdrInvoices.Item("invoice_id"))

                    If (IntCustId1 <> intLastCustId) Then
                        '-- found new customer.
                        If (intLastCustId >= 0) Then  '-not -1
                            '-- NEW CUST line also means new Invoice.
                            '-- row belongs to a new invoice..
                            '--save last invoice.
                            '--compute outstanding, if any.-
                            decAmountOutstanding = decInvoiceTotal - decPaymentTotal
                            colThisInvoice.Add(decPaymentTotal, "paymentTotalThisInvoice")
                            colThisInvoice.Add(decAmountOutstanding, "amountOutstanding")
                            colThisInvoice.Add(colThisInvoicePayments, "invoicePayments")
                            '-- IF has Outstanding, OR we are saving everything by date..
                            If (decAmountOutstanding > 0) Or (Not bOutstandingInvoicesOnly) Or _
                                     (bOutstandingInvoicesOnly AndAlso (IntClosedDaysToShow > 0) AndAlso _
                                         (dateThisInvoice >= dateClosedInvoicesToShow)) Then

                                '==  Target is new Build 4251..
                                '==    Account Invoice Reversals.. 
                                '--  If was reversed, add to reversed coll.
                                If mColAllAccountReversals.Contains(CStr(intThisInvoiceId)) Then
                                    '-- attach refund to orig invoice
                                    colThisInvoice.Add(mColAllAccountReversals.Item(CStr(intThisInvoiceId)), "refundInvoice")
                                    '-- add to reversed collection for customer..
                                    colThisCustomerReversedInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                                Else '-wasn't reversed
                                    colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                                End If
                                '= colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                                '== END OF  Target is new Build 4251..

                            End If  '-oust.

                            '- NOW can save Customer.
                            '- NOW can save Customer.

                            '--save end of last cust group..
                            '-- ONLY if there are invoices to show !!
                            '==  Target is new Build 4251..
                            If (colThisCustomerInvoices.Count > 0) Or (colThisCustomerReversedInvoices.Count > 0) Then
                                colThisCustomer.Add(colRefunds, "Refunds")  '-always empty.
                                colThisCustomer.Add(colCustInfo, "CustInfo")
                                colThisCustomer.Add(colThisCustomerInvoices, "invoices")

                                '==  Target is new Build 4251..
                                '==    Account Invoice Reversals.. 
                                colThisCustomer.Add(colThisCustomerReversedInvoices, "reversedInvoices")
                                '== END OF  Target is new Build 4251..

                                colReportCustomers.Add(colThisCustomer, CStr(intThisCustomerId))
                            End If
                        Else
                            '--must be the first rdr row in the set..
                            '-- nothing to save..
                        End If  '-was real group just passed.

                        '-- NEW CUST.. So-
                        '-     start new invoice.
                        '-     start new invoice.
                        intThisInvoiceId = intInvId1
                        '= For 4201.0727=  
                        '==  MUST Round the Invoice total to take 3 decimals down to 2.
                        decInvoiceTotal = CDec(rdrInvoices.Item("total_inc"))
                        decInvoiceTotal = Math.Round(decInvoiceTotal, 2, MidpointRounding.AwayFromZero)
                        '-Same for Tax..
                        decTotalTax = CDec(rdrInvoices.Item("total_tax"))
                        decTotalTax = Math.Round(decTotalTax, 2, MidpointRounding.AwayFromZero)
                        '=  END of  For 4201.0727=  
                        colThisInvoice = New Collection
                        colThisInvoice.Add(intThisInvoiceId, "invoice_id")
                        colThisInvoice.Add(decInvoiceTotal, "invoiceTotal")
                        colThisInvoice.Add(decTotalTax, "total_tax")
                        colThisInvoice.Add(rdrInvoices.Item("transactionType"), "transactionType")
                        colThisInvoice.Add(rdrInvoices.Item("invoice_date"), "invoice_date")
                        dateThisInvoice = CDate(rdrInvoices.Item("invoice_date"))

                        colThisInvoicePayments = New Collection
                        decPaymentTotal = 0

                        '== start new cust. group.
                        '== start new cust. group.
                        '== start new cust. group.
                        colThisCustomer = New Collection
                        colCustInfo = New Collection
                        '==    (NOT IMPLEMENTD- PROBABLY NOT NEEDED)
                        '=colAllPaymentsWithCrNote = New Collection
                        '=colCustPaymentIDs = New Collection '-- track unique pai-is'd

                        colThisCustomerInvoices = New Collection

                        '==  Target is new Build 4251..
                        '==    Account Invoice Reversals.. 
                        colThisCustomerReversedInvoices = New Collection
                        '== END OF  Target is new Build 4251..

                        intThisCustomerId = IntCustId1  '-from current (new) row.

                        colThisCustomer.Add(intThisCustomerId, "customer_id")
                        colCustInfo.Add(intThisCustomerId, "customer_id")
                        sName = CStr(rdrInvoices.Item("custShortName"))
                        colCustInfo.Add(sName, "custShortName")

                        '-- Save all customer details for statements..
                        colCustInfo.Add(rdrInvoices.Item("barcode"), "barcode")
                        colCustInfo.Add(rdrInvoices.Item("firstName"), "firstName")
                        colCustInfo.Add(rdrInvoices.Item("lastName"), "lastName")
                        colCustInfo.Add(rdrInvoices.Item("companyName"), "companyName")
                        '- make full name-
                        s1 = Trim(CStr(rdrInvoices.Item("companyName")))
                        If (s1 <> "") Then s1 &= "- "
                        s1 &= CStr(rdrInvoices.Item("firstName")) & " " & CStr(rdrInvoices.Item("lastName"))
                        colCustInfo.Add(s1, "customerFullName")
                        '--
                        colCustInfo.Add(rdrInvoices.Item("address"), "address")
                        colCustInfo.Add(rdrInvoices.Item("suburb"), "suburb")
                        colCustInfo.Add(rdrInvoices.Item("state"), "state")
                        colCustInfo.Add(rdrInvoices.Item("postcode"), "postcode")
                        colCustInfo.Add(rdrInvoices.Item("country"), "country")
                        colCustInfo.Add(rdrInvoices.Item("email"), "email")
                        colCustInfo.Add(rdrInvoices.Item("creditLimit"), "creditLimit")
                        colCustInfo.Add(rdrInvoices.Item("creditDays"), "creditDays")
                        colCustInfo.Add(rdrInvoices.Item("phone"), "phone")
                        colCustInfo.Add(rdrInvoices.Item("mobile"), "mobile")
                        '-decCrbal-
                        decCrbal = 0
                        If mDicCreditNoteBalances.ContainsKey(intThisCustomerId) Then
                            decCrbal = mDicCreditNoteBalances.Item(intThisCustomerId)
                        End If
                        '-- add to cust collection.
                        colCustInfo.Add(decCrbal, "CreditNoteBalance")
                        '= done credit note.
                        '-doNotEmailDocuments-
                        colCustInfo.Add(rdrInvoices.Item("doNotEmailDocuments"), "doNotEmailDocuments")

                        intLastCustId = intThisCustomerId
                    Else  '-not new cust-
                        '- Same cust.
                        '- Check if same invoice..
                        If (intInvId1 <> intLastInvoiceId) Then
                            '-- row belongs to a new invoice..
                            If intLastInvoiceId <> -1 Then
                                '--save last invoice.
                                '--compute outstanding, if any.-
                                decAmountOutstanding = decInvoiceTotal - decPaymentTotal
                                colThisInvoice.Add(decPaymentTotal, "paymentTotalThisInvoice")
                                colThisInvoice.Add(decAmountOutstanding, "amountOutstanding")
                                colThisInvoice.Add(colThisInvoicePayments, "invoicePayments")
                                '-- IF has Outstanding, OR we are saving Outst + stuff by date..
                                If (decAmountOutstanding > 0) Or (Not bOutstandingInvoicesOnly) Or _
                                            (bOutstandingInvoicesOnly AndAlso (IntClosedDaysToShow > 0) AndAlso _
                                                                     (dateThisInvoice >= dateClosedInvoicesToShow)) Then

                                    '==  Target is new Build 4251..
                                    '==    Account Invoice Reversals.. 
                                    '--  If was reversed, add to reversed coll.
                                    If mColAllAccountReversals.Contains(CStr(intThisInvoiceId)) Then
                                        '-- attach refund to orig invoice
                                        colThisInvoice.Add(mColAllAccountReversals.Item(CStr(intThisInvoiceId)), "refundInvoice")
                                        '-- add to reversed collection for customer..
                                        colThisCustomerReversedInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                                    Else '-wasn't reversed
                                        colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                                    End If
                                    '= colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                                    '== END OF  Target is new Build 4251..


                                End If  '-oust.
                            Else
                                '-nothing to save yet.
                            End If
                            '- start new invoice.
                            intThisInvoiceId = intInvId1
                            decPaymentTotal = 0
                            '= For 4201.0727=  
                            '==  MUST Round the Invoice total to take 3 decimals down to 2.
                            decInvoiceTotal = CDec(rdrInvoices.Item("total_inc"))
                            decInvoiceTotal = Math.Round(decInvoiceTotal, 2, MidpointRounding.AwayFromZero)
                            '-Same for Tax..
                            decTotalTax = CDec(rdrInvoices.Item("total_tax"))
                            decTotalTax = Math.Round(decTotalTax, 2, MidpointRounding.AwayFromZero)
                            colThisInvoice = New Collection
                            colThisInvoice.Add(intThisInvoiceId, "invoice_id")
                            colThisInvoice.Add(decInvoiceTotal, "invoiceTotal")
                            colThisInvoice.Add(decTotalTax, "total_tax")
                            colThisInvoice.Add(rdrInvoices.Item("transactionType"), "transactionType")
                            colThisInvoice.Add(rdrInvoices.Item("invoice_date"), "invoice_date")
                            dateThisInvoice = CDate(rdrInvoices.Item("invoice_date"))

                            colThisInvoicePayments = New Collection
                        End If  '-new invoice
                    End If  '-new cust-

                    '-process this line.. 
                    '-remember this one as the new last..
                    intLastInvoiceId = intThisInvoiceId
                    bIsReversal = False

                    '-- OK--
                    '-- process payment disb (if any) on this line.
                    '-- process payment disb (if any) on this line.
                    '-- process payment disb (if any) on this line.
                    If Not IsDBNull(rdrInvoices.Item("payment_id")) Then
                        Dim intDisbThisPaymentId As Integer = CInt(rdrInvoices.Item("payment_id"))
                        '- have payment disbursement...
                        colThisPayment = New Collection
                        colThisPayment.Add(rdrInvoices.Item("customer_id"), "customer_id")
                        colThisPayment.Add(rdrInvoices.Item("barcode"), "cust_barcode")
                        colThisPayment.Add(rdrInvoices.Item("invoice_id"), "invoice_id")
                        colThisPayment.Add(rdrInvoices.Item("payment_id"), "payment_id")
                        colThisPayment.Add(rdrInvoices.Item("payment_date"), "date")
                        colThisPayment.Add(rdrInvoices.Item("nettAmountCredited"), "paymentTotalCredited")
                        '=creditNoteAmountDebited-
                        decCreditNoteAmountDebited = CDec(rdrInvoices.Item("creditNoteAmountDebited"))
                        colThisPayment.Add(decCreditNoteAmountDebited, "creditNoteAmountDebited")
                        colThisPayment.Add(rdrInvoices.Item("disb_amount"), "amount")
                        decPaymentAmount = CDec(rdrInvoices.Item("disb_amount"))
                        colThisPayment.Add(rdrInvoices.Item("disbTranCode"), "disbTranCode")
                        colThisPayment.Add(rdrInvoices.Item("sourceOfFunds"), "sourceOfFunds")
                        If CInt(rdrInvoices.Item("isReversal")) <> 0 Then
                            bIsReversal = True
                        End If
                        colThisPayment.Add(rdrInvoices.Item("isReversal"), "isReversal")
                        colThisInvoicePayments.Add(colThisPayment)
                        '- add to current paymnt total for current invoice..
                        decPaymentTotal += IIf(bIsReversal, -decPaymentAmount, decPaymentAmount)
                        '-- IF this payment includes. cr-note wdl, then add to payment collection for later scrutiny.
                        '-- WE are only interested if the payment was made in the process of a SALE..
                        '-     As payments/disbursments made in AccountPayments do incl. the CrNote fraction if any.
                        '-- And in the case of a Sale with payment, the payment made can only ever apply to one Invoice..
                        '--    (that of the Sale.) ie to one disbursement.
                        '- NB: An invoice that is not fully paid is still 'open'.. ie more payments can come.
                        '-       But-  a payment can apply to multiple invoices, but it is closed in the
                        '-             sense that they are decided at the time of payment creation, and that's all.
                        '--                ie. however many there are, that's all there will be.
                        '== SO we want to see the payments that have onlt one invoice Disb.
                        '== WE can't know this until we've see all invoices for the customer.

                        '==  (NOT IMPLEMENTD- PROBABLY NOT NEEDED)
                        '==  (NOT IMPLEMENTD- PROBABLY NOT NEEDED)
                        '==  (NOT IMPLEMENTD- PROBABLY NOT NEEDED)
                        '    If (decCreditNoteAmountDebited > 0) And (Not bIsReversal) And _
                        '                                  (Trim(rdrInvoices.Item("sourceOfFunds")) = "") Then
                        '        colx = New Collection
                        '        colx.Add(intDisbThisPaymentId, "payment_id")
                        '        colx.Add(colThisPayment, "disbursement")
                        '        colAllPaymentsWithCrNote.Add(colx)
                        '        '--note payment_id for reference.
                        '        If Not colCustPaymentIDs.Contains(intDisbThisPaymentId) Then
                        '            colCustPaymentIDs.Add(intDisbThisPaymentId)
                        '        End If '-contains.
                        '    End If  '-decCreditNoteAmountDebited-
                    End If  '-- not null.
                End While  '-reading-


                '==   Target-New-Build-4267 -- (Started 18-Sep-2020)
                '== 
                '==  -- clsDebtors-  FIX crash that happens when getting invoices and NONE are on file
                '==                    (eg for invoices for a particular customer.)
                '==  MOVE all last Invoice stuff to here.
                '--    where we HAVE ROWS

                '-- OK.  WE've seen all rows.. (Invoices-Disbursements..)
                '--    Just save the collections that are as yet UNSAVED.
                '--compute outstanding, if any.-
                decAmountOutstanding = decInvoiceTotal - decPaymentTotal
                colThisInvoice.Add(decPaymentTotal, "paymentTotalThisInvoice")
                colThisInvoice.Add(decAmountOutstanding, "amountOutstanding")

                '--save last invoice.
                '--save last invoice.
                colThisInvoice.Add(colThisInvoicePayments, "invoicePayments")
                '--save last invoice.
                'If (decAmountOutstanding > 0) Then '= Or (Not bOutstandingInvoicesOnly) Then
                '    colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                'End If  '-outst.-
                '-- IF has Outstanding, OR we are saving Outst + stuff by date..
                If (decAmountOutstanding > 0) Or (Not bOutstandingInvoicesOnly) Or _
                            (bOutstandingInvoicesOnly AndAlso (IntClosedDaysToShow > 0) AndAlso _
                                                     (dateThisInvoice >= dateClosedInvoicesToShow)) Then

                    '==  Target is new Build 4251..
                    '==    Account Invoice Reversals.. 
                    '--  If was reversed, add to reversed coll.
                    If mColAllAccountReversals.Contains(CStr(intThisInvoiceId)) Then
                        '-- attach refund to orig invoice
                        colThisInvoice.Add(mColAllAccountReversals.Item(CStr(intThisInvoiceId)), "refundInvoice")
                        '-- add to reversed collection for customer..
                        colThisCustomerReversedInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                    Else '-wasn't reversed
                        colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                    End If
                    '= colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
                    '== END OF  Target is new Build 4251..


                End If  '-oust.

                '- NOW can save Customer.

                '- FIRST check for Credit Note misdemeanors..

                '--save end of last cust group..
                '-- ONLY if there are invoices to show !!\

                '==  Target is new Build 4251..
                If (colThisCustomerInvoices.Count > 0) Or (colThisCustomerReversedInvoices.Count > 0) Then
                    colThisCustomer.Add(colRefunds, "Refunds")  '-always empty.
                    colThisCustomer.Add(colCustInfo, "CustInfo")
                    colThisCustomer.Add(colThisCustomerInvoices, "invoices")


                    '==  Target is new Build 4251..
                    '==    Account Invoice Reversals.. 
                    colThisCustomer.Add(colThisCustomerReversedInvoices, "reversedInvoices")
                    '== END OF  Target is new Build 4251..

                    colReportCustomers.Add(colThisCustomer, CStr(intThisCustomerId))
                End If

                '==  END Target-New-Build-4267 -- (Started 18-Sep-2020)


            End If  '-has rows..
            rdrInvoices.Close()

            ''-- OK.  WE've seen all rows.. (Invoices-Disbursements..)
            ''--    Just save the collections that are as yet UNSAVED.
            ''--compute outstanding, if any.-
            'decAmountOutstanding = decInvoiceTotal - decPaymentTotal
            'colThisInvoice.Add(decPaymentTotal, "paymentTotalThisInvoice")
            'colThisInvoice.Add(decAmountOutstanding, "amountOutstanding")

            ''--save last invoice.
            ''--save last invoice.
            'colThisInvoice.Add(colThisInvoicePayments, "invoicePayments")
            ''--save last invoice.
            ''If (decAmountOutstanding > 0) Then '= Or (Not bOutstandingInvoicesOnly) Then
            ''    colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
            ''End If  '-outst.-
            ''-- IF has Outstanding, OR we are saving Outst + stuff by date..
            'If (decAmountOutstanding > 0) Or (Not bOutstandingInvoicesOnly) Or _
            '            (bOutstandingInvoicesOnly AndAlso (IntClosedDaysToShow > 0) AndAlso _
            '                                     (dateThisInvoice >= dateClosedInvoicesToShow)) Then

            '    '==  Target is new Build 4251..
            '    '==    Account Invoice Reversals.. 
            '    '--  If was reversed, add to reversed coll.
            '    If mColAllAccountReversals.Contains(CStr(intThisInvoiceId)) Then
            '        '-- attach refund to orig invoice
            '        colThisInvoice.Add(mColAllAccountReversals.Item(CStr(intThisInvoiceId)), "refundInvoice")
            '        '-- add to reversed collection for customer..
            '        colThisCustomerReversedInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
            '    Else '-wasn't reversed
            '        colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
            '    End If
            '    '= colThisCustomerInvoices.Add(colThisInvoice, CStr(intThisInvoiceId))
            '    '== END OF  Target is new Build 4251..


            'End If  '-oust.

            ''- NOW can save Customer.

            ''- FIRST check for Credit Note misdemeanors..

            ''--save end of last cust group..
            ''-- ONLY if there are invoices to show !!\

            ''==  Target is new Build 4251..
            'If (colThisCustomerInvoices.Count > 0) Or (colThisCustomerReversedInvoices.Count > 0) Then
            '    colThisCustomer.Add(colRefunds, "Refunds")  '-always empty.
            '    colThisCustomer.Add(colCustInfo, "CustInfo")
            '    colThisCustomer.Add(colThisCustomerInvoices, "invoices")


            '    '==  Target is new Build 4251..
            '    '==    Account Invoice Reversals.. 
            '    colThisCustomer.Add(colThisCustomerReversedInvoices, "reversedInvoices")
            '    '== END OF  Target is new Build 4251..

            '    colReportCustomers.Add(colThisCustomer, CStr(intThisCustomerId))
            'End If

        End If  '- rdrInvoioces is nothing-
        sList = ""
        Dim intCounter As Integer

        '-- TESTING- Show results..-
        '-- TESTING- Show results..-
        For Each colThisCustomer In colReportCustomers
            colThisCustomerInvoices = colThisCustomer.Item("invoices")
            colCustInfo = colThisCustomer.Item("CustInfo")
            intCounter = 0
            sList &= colCustInfo.Item("customerFullName") & " has " & colThisCustomerInvoices.Count & " invoices: " '- & vbCrLf
            sList &= "  CreditNoteBal: " & colCustInfo.Item("CreditNoteBalance") & ";  Invoices are-" & vbCrLf
            For Each colThisInvoice In colThisCustomerInvoices
                'If (intCounter > 5) Then
                '    sList &= vbCrLf
                '    intCounter = 0
                'End If
                sList &= "#" & CStr(colThisInvoice.Item("invoice_id")) & " " & FormatCurrency(colThisInvoice.Item("invoiceTotal"), 2)
                decPaymentTotal = colThisInvoice.Item("paymentTotalThisInvoice")
                colThisInvoicePayments = colThisInvoice.Item("invoicePayments")
                sList &= ". (" & colThisInvoicePayments.Count & " payments: " & _
                                  FormatNumber(decPaymentTotal, 2) & "); " & vbCrLf
                intCounter += 1
            Next colThisInvoice
            sList &= vbCrLf

            '==  Target is new Build 4251..
            colThisCustomerReversedInvoices = colThisCustomer.Item("reversedInvoices")

            If (colThisCustomerReversedInvoices.Count > 0) Then
                Dim colRefund As Collection
                For Each colInv1 As Collection In colThisCustomerReversedInvoices
                    colRefund = colInv1.Item("refundInvoice")
                    sList &= "** Invoice " & colInv1.Item("invoice_id") & _
                                   " was reversed by Refund #" & colRefund.Item("invoice_id") & _
                                      " on " & Format(colRefund.Item("invoice_date"), "dd-MMM-yyyy") & vbCrLf
                Next colInv1
                sList &= vbCrLf
            End If
            '== END OF Target is new Build 4251..

        Next colThisCustomer

        '-save-
        mColReportCustomers = colReportCustomers

        '-- TESTING- Show results..-
        '-- TESTING- Show results..-
        'MsgBox("Found " & intRdrCount & " Rows.." & vbCrLf & _
        '        " and " & colReportCustomers.Count & "  Customers.." & _
        '                     vbCrLf & vbCrLf & sList, MsgBoxStyle.Information)

        mbLoadAllAccountInvoices = True

    End Function  '-mbLoadAllAccountInvoices-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- New--  Constructor..

    Public Sub New(ByRef cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                       ByRef colPrefsCustomer As Collection, _
                          ByVal sVersionPOS As String, _
                          ByRef imageUserLogo As Image, _
                            ByVal intStaff_id As Integer, _
                               ByVal sStaffName As String)

        MyBase.New()

        mCnnSql = cnnSql
        '= msServer = sSqlServerName
        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo
        '= msRuntimeLogPath = strLogPath
        '-- initialise..--
        '- Get actual machine running this app process. (NOT the remote client).
        msMachineName = My.Computer.Name  '- for actual machine running this app process. (NOT the remote client).

        mColSqlDBInfo = colSqlDBInfo
        mColPrefsCustomer = colPrefsCustomer
        msVersionPOS = sVersionPOS
        mImageUserLogo = imageUserLogo

        mIntStaff_id = intStaff_id
        msStaffName = sStaffName
        '=3403.1102=
        '= mIntRequestedCustomer_id = -1  '- caller must use input property.

    End Sub  '-new-
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '- mbGetAllDebtorReportInfo-

    Public Function GetAllDebtorReportInfo(ByVal bOutstandingInvoicesOnly As Boolean, _
                                           ByVal IntDaysToShow As Integer, _
                                              ByVal dateCutoff As Date, _
                                               ByRef colAllDebtorsInfo As Collection, _
                                               Optional ByVal intRequestedCustomer_id As Integer = -1) As Boolean

        mbInfoLoadedOK = False
        GetAllDebtorReportInfo = False
        '= mIntRequestedCustomer_id = intCustomer_id  '-  -1 means all customers..


        '==  Target is new Build 4251..
        '==  Target is new Build 4251..
        '==    Account Invoice Reversals.. 
        Call mbCollectAllAccountReversals()
        '== END OF  Target is new Build 4251..



        If Not mbLoadAllAccountInvoices(bOutstandingInvoicesOnly, _
                                        IntDaysToShow, dateCutoff, _
                                            intRequestedCustomer_id) Then  '= mbReloadCustomers(IntDaysToShow, dateCutoff) Then
            MsgBox("clsDebtors: Failed to load Customer Invoice info..", MsgBoxStyle.Exclamation)
            '- dispose ?-
        Else
            mbInfoLoadedOK = True
        End If

        If mbInfoLoadedOK Then
            colAllDebtorsInfo = mColReportCustomers
            GetAllDebtorReportInfo = True
        Else
        End If

    End Function '- mbGetAllDebtorReportInfo-
    '= = = == = = = = = = = = = = = === = = =
    '-===FF->

    '==
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)

    '--   SO here we need to make "mbCollectAllAccountReversals()" PUBLIC..

    '==   CollectAllAccountReversals-

    Public Function CollectAllAccountReversals(ByRef colAllAccountReversals As Collection, _
                                               Optional ByVal bIsTransaction As Boolean = False, _
                                                    Optional ByRef oledbTransaction1 As OleDbTransaction = Nothing) As Boolean

        CollectAllAccountReversals = False
        If mbCollectAllAccountReversals(bIsTransaction, oledbTransaction1) Then
            colAllAccountReversals = mColAllAccountReversals
            CollectAllAccountReversals = True
        End If

    End Function  '-CollectAllAccountReversals-
    '= = = = = = = = = = = = = = = = = = =  == = 


End Class  '-clsDebtors-
'= = = = = = = = = = =
