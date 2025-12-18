
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


Public Class clsCashSaleReversal

    '==
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
    '== DEV PREP Updates to 4234.0505  Started 12-May-2020= 
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
    '==       --  Transaction needs SUPERVISOR PASSWORD...
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==
    '==
    '==  Target-New-Build-4257..
    '==  Target-New-Build-4257..  07-July-2020.
    '==
    '==   -- Allow Account-Invoice Reversal with WARNING for Delivered-Job Invoices..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==Fixes to Build 4257.0707  
    '==
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==
    '== A.  POS System  30-Jul-2020
    '==
    '== (a) To Fix- frmShowInvoice crashes when displaying a Quote.. 
    '==             See end of Event Shown, where "invoice_date" column is referenced without checking if Quote ot Invoice.
    '== (b) Account Invoice Reversal- 
    '==      -- If Account payments have been made to the Invoice 
    '==           - User should be able to reverse the Account Invoice if all the Account Payments are reversed first..
    '== 
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '==
    '== Fixes to Build 4277.1010  
    '==
    '==  NEW CLASS (cloned) to do Cash Sale Reversal..
    '==  NEW CLASS (cloned) to do Cash Sale Reversal..
    '==  NEW CLASS (cloned) to do Cash Sale Reversal..
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '==   Target-New-Build-4282 -- (Started 12-October-2020)
    '==   Target-New-Build-4282 -- (Started 12-October-2020)
    '==   Target-New-Build-4282 -- (Started 12-October-2020)
    '==
    '==  A.  New Class to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
    '==         Done by cloning clsAccountReversal and moddying and adding Payment Reversal stuff..
    '==
    '==
    '==     -- ALSO- Fix Terminal_id missing in clsCshSaleReversal, clsAccountReversal, 
    '==                     and frmShowPayment (for Payment Reversal.).
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 


    Private mCnnSql As OleDbConnection  '=  ADODB.Connection
    Private mbIsSqlAdmin As Boolean = False

    '= Private msServer As String = ""
    Private msSqlVersion As String = ""
    '= Private msInputDBNameJobs As String = ""
    Private mFrmParent As Form

    Private mbIsLoading As Boolean = False
    '= Private mbCloseRequested As Boolean = False

    '= Private msSqlDbName As String = ""
    '= Private mColSqlDBInfo As Collection '--  jobs DB info--
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
    '= Private msSqlServerComputer As String = ""
    '= Private msSqlServerInstance As String = ""

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""

    Private msVersionPOS As String = ""

    Private mFormWait1 As frmWait

    Private mColPrefsCustomer As Collection
    Private mImageUserLogo As Image

    Private msCallerStaffName As String = ""
    Private mIntCallerStaff_id As Integer = -1
    '=3403.1102=

    Private mbInfoLoadedOK As Boolean = False
    '= = = = = = = = = = = = = = = = = = = = = = = = =

    '- Current INVOICE DETAILS --
    '-Original-
    Private mIntOriginalInvoice_id As Integer = -1

    Private mIntRefundInvoice_id As Integer = -1
    '== 4221.0205.
    '= For Quote, ID is actually copied from mIntInvoice_id..
    Private mIntSalesOrder_id As Integer = -1

    Private msTranCode As String = ""
    Private mbIsRefund As Boolean = False

    Private mbIsQuote As Boolean = False
    Private mbIsLayby As Boolean = False


    Private mDataTableOriginalInvoice As DataTable
    Private mDataTableOriginalSaleItems As DataTable
    Private mDataTableOriginalPayments As DataTable
    Private mDataTablePaymentDetails As DataTable
    Private mDataTableDisbursements As DataTable

    Private mbIsAccountCust As Boolean
    Private mbIsOnAccount As Boolean = False

    Private mDecAmountDebitedToAccount As Decimal = 0

    'Private mDecTotalPayments, mDecChange, mDecTotalContribution As Decimal
    'Private mDecCreditNoteAmountCredited As Decimal = 0
    'Private mDecCreditNoteAmountDebited As Decimal = 0

    ''=3519.0317=
    'Private mDecRefundedAsCash As Decimal = 0
    'Private mDecRefundedAsCreditNote As Decimal = 0
    'Private mDecRefundedAsEftPosDr As Decimal = 0
    'Private mDecRefundedAsEftPosCr As Decimal = 0


    Private msCustBarcode As String = ""
    Private msCustomerEmail As String = ""

    '=3311.226=
    '==  Input from Commit Confirmation form..
    Dim msSelectedInvoicePrinterName As String = ""
    Dim msSelectedReceiptPrinterName As String = ""

    '= = = = = = = = = = = = = == = = = = = = ==
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


    '-- New--  Constructor..

    Public Sub New(ByRef cnnSql As OleDbConnection, _
                          ByVal sVersionPOS As String, _
                          ByRef imageUserLogo As Image, _
                            ByVal intStaff_id As Integer, _
                              ByVal sStaffName As String, _
                              ByVal sInvoicePrinterName As String, _
                               Optional ByRef frmParent As Form = Nothing)

        MyBase.New()

        mCnnSql = cnnSql
        '= msServer = sSqlServerName
        '-- initialise..--
        '- Get actual machine running this app process. (NOT the remote client).
        msMachineName = My.Computer.Name  '- for actual machine running this app process. (NOT the remote client).
        msVersionPOS = sVersionPOS
        mImageUserLogo = imageUserLogo

        mIntCallerStaff_id = intStaff_id
        msCallerStaffName = sStaffName
        msSelectedInvoicePrinterName = sInvoicePrinterName

        '= mIntRequestedCustomer_id = -1  '- caller must use input property.
        If frmParent Is Nothing Then
            mFrmParent = New Form
        Else
            mFrmParent = frmParent
        End If

        msCurrentUserName = gsGetCurrentUser()

        '-- get staffname if not supplied..
        If (intStaff_id > 0) And (Trim(sStaffName) = "") Then
            Dim dataTable1 As DataTable
            Dim datarow1 As DataRow
            Dim sSqlSelect As String = "SELECT docket_name FROM staff WHERE (staff_id=" & intStaff_id & "); "
            If Not gbGetDataTable(mCnnSql, dataTable1, sSqlSelect) Then
                '-error-
                MsgBox("Error in getting recordset for Staff table: " & vbCrLf & _
                         gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '=Exit Sub
            Else
                '--ok-
                If (dataTable1 IsNot Nothing) AndAlso (dataTable1.Rows.Count > 0) Then
                    '-- have some..
                    datarow1 = dataTable1.Rows(0)
                    msCallerStaffName = datarow1.Item("docket_name")
                End If
            End If  'get-
        End If  '-staff-
        '==
        '==     -- ALSO- Fix Terminal_id missing in clsCshSaleReversal, clsAccountReversal, 
        '==                     and frmShowPayment (for Payment Reversal.).
        '==
        msComputerName = My.Computer.Name

    End Sub  '-new-
    '= = = = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- MAIN THING..

    '-- Create Reversal-
    '-- Create Reversal- as mirror image of Original Invoice..

    '--  Create REVERSAL of the Payment also..
    '--  Create REVERSAL of the Payment also..

    Public Function CreateCashSaleReversal(ByVal intOriginalInvoice_id As Integer, _
                                          ByRef dtOriginalInvoice As DataTable, _
                                          ByRef dtOriginalSaleItems As DataTable, _
                                          ByRef dtOriginalPayments As DataTable) As Boolean
        Dim sSqlSelect, sOriginalTranCode As String
        Dim sMsg As String
        Dim rowInvoice1 As DataRow
        Dim sqlTransaction1 As OleDbTransaction
        Dim intCount, intID, intCustomer_id As Integer
        Dim listOfNonStockItems As List(Of Integer)
        Dim dataTable1 As DataTable

        CreateCashSaleReversal = False

        mIntOriginalInvoice_id = intOriginalInvoice_id
        msCurrentUserName = gsGetCurrentUser()

        If (dtOriginalInvoice Is Nothing) OrElse (dtOriginalInvoice.Rows.Count <= 0) Then
            MsgBox("This Cash Sale Invoice No: " & intOriginalInvoice_id & _
                      " has no datatable data.. it cannot be reversed..", MsgBoxStyle.Exclamation)
            Exit Function
        End If
        mDataTableOriginalInvoice = dtOriginalInvoice

        mDataTableOriginalSaleItems = dtOriginalSaleItems
        mDataTableOriginalPayments = dtOriginalPayments

        '-  get original invoice data row..
        rowInvoice1 = mDataTableOriginalInvoice.Rows(0)
        intCustomer_id = rowInvoice1.Item("Customer_id")

        sOriginalTranCode = rowInvoice1.Item("transactionType")
        If (LCase(sOriginalTranCode) <> "sale") Then
            MsgBox("This original Invoice No: " & intOriginalInvoice_id & _
              " is not a Sale transaction..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '--MUST BE Cash sale invoice.. (NOT Account.)
        If (rowInvoice1.Item("isOnAccount") <> 0) Then
            MsgBox("This original Invoice No: " & intOriginalInvoice_id & _
              " is 'On Account'- it's not a Cash Sale transaction..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        sMsg = ""
        '--  Check that invoice was not For Layby or a JobNumber..
        If CInt((rowInvoice1.Item("delivered_layby_id")) > 0) Then
            sMsg = "This invoice was delivering layby No: " & rowInvoice1.Item("delivered_layby_id")
        End If
        If CInt((rowInvoice1.Item("jobNumber")) > 0) Then
            sMsg = "This invoice was delivering Job No: " & rowInvoice1.Item("jobNumber")
        End If


        '==  Target-New-Build-4257..  07-July-2020.
        '==  Target-New-Build-4257..  07-July-2020.
        '==
        '==   -- Allow Account-Invoice Reversal with WARNING for Delivered-Job Invoices..

        If (sMsg <> "") Then
            '= MsgBox("Reversal not allowed-" & vbCrLf & sMsg, MsgBoxStyle.Exclamation)
            If MsgBox("IMPORANT NOTE-" & vbCrLf & sMsg & vbCrLf & vbCrLf & _
                      "Are you sure you want to REVERSE this Invoice ??", _
                         MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) <> MsgBoxResult.Yes Then
                Exit Function
            End If
        End If

        '== END  Target-New-Build-4257..  07-July-2020.
        
        '-- Confirm that they really want to do this..
        If MessageBox.Show("Please note-" & _
                           "You are about to reverse a Cash Sale Invoice with " & _
                                       mDataTableOriginalSaleItems.Rows.Count & " or more items.." & vbCrLf & vbCrLf & _
                                       "Before proceeding, make sure these items have not been delivered, " & vbCrLf & _
                                       "    or if so, then they are being received back in.." & vbCrLf & _
                                        "   This transaction will update stock records to reverse the Sale." & vbCrLf & vbCrLf & _
                                       "Do you want to proceed with this Reversal of Invoice # " & intOriginalInvoice_id & " ??", _
                            "Reversal of Cash Sale", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                  MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
            Exit Function
        End If

        '-- Confirm that they KNOW they really want to do this..
        '-- Confirm that they KNOW they really want to do this..
        If MessageBox.Show("NB-" & _
                           "This reversal of a Cash Sale Invoice can't itself be reversed.. " & _
                                       "Are you sure you want to Reverse the Sale Invoice # " & intOriginalInvoice_id & " ??", _
                            "Confirming Reversal of Cash Sale", _
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
                                  MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
            Exit Function
        End If

        '-- make a list of all Stock ID's that are non-stock items..
        listOfNonStockItems = New List(Of Integer)(1000)

        sSqlSelect = "SELECT stock_id from dbo.stock WHERE (isNonStockItem<>0); "
        If Not gbGetDataTable(mCnnSql, dataTable1, sSqlSelect) Then
            '-error-
            MsgBox("Error in getting recordset for Stock ID's table: " & vbCrLf & _
                     gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        Else
            '--ok-
            If (dataTable1 IsNot Nothing) AndAlso (dataTable1.Rows.Count > 0) Then
                '-- have some..
                For Each datarow2 As DataRow In dataTable1.Rows
                    listOfNonStockItems.Add(datarow2.Item("stock_id"))
                Next datarow2
            End If
        End If

        '--1. Start a TRANSACTION so we can lock out other attempts..
        '--1. Start a TRANSACTION so we can lock out other attempts..
        '--1. Start a TRANSACTION so we can lock out other attempts..
        '--1. Start a Transaction so we can lock out other attempts..

        sqlTransaction1 = mCnnSql.BeginTransaction

        '- Check that CASH SALE Invoice is not already reversed..  Set up locking for the Trans.
        '- Check that CASH SALE Invoice is not already reversed..  Set up locking for the Trans.
        '- Check that CASH SALE Invoice is not already reversed..  Set up locking for the Trans.

        sSqlSelect = "SELECT count(*) FROM dbo.invoice  WITH (TABLOCKX, HOLDLOCK)  "
        sSqlSelect &= "  WHERE (transactionType='refund') AND " & _
                         "(isOnAccount =0) AND (original_id=" & intOriginalInvoice_id & "); "
        If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSqlSelect, True, sqlTransaction1, intCount) Then
            If (intCount > 0) Then
                '-- already refunded.
                sqlTransaction1.Rollback()
                MsgBox("This Invoice No: " & intOriginalInvoice_id & _
                  " has already been reversed..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            '-- ok..  keep going..-
        Else
            '--  rollback was done.
            MsgBox("Failed to retrieve count of possible previous refunds...", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '- Check if payments have been made since we were called..

        '- NB-  Must read the DB payments disbursements to get latest info.
        Dim dtTempPayments As DataTable
        '--  USE disbursements table..


        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
        '-- Check Instead for Unreversed-Payments to this invoice..

        'sSqlSelect = "SELECT * FROM dbo.paymentDisbursements AS PD"
        'sSqlSelect &= " LEFT JOIN dbo.payments ON (PD.payment_id=payments.payment_id) "
        'sSqlSelect &= "  WHERE (PD.invoice_id=" & CStr(intOriginalInvoice_id) & ")"


        '--  DON'T   NEED THIS --
        '--  DON'T   NEED THIS --
        '--  DON'T   NEED THIS --

        '-- Check  for UNREVERSED-Payments to this invoice..
        '-- ie. Check that there is no payment to this invoice that hasn't been Reversed..
        '--   If any rows are returned,  it's fail..
        '--   (The subQuery selects ALL Payment REVERSALS)


        'sSqlSelect = "SELECT * FROM dbo.paymentDisbursements AS PD"
        'sSqlSelect &= " LEFT JOIN dbo.Payments P1 ON (PD.Payment_id=P1.Payment_id) "
        'sSqlSelect &= "  WHERE (PD.invoice_id=" & CStr(intOriginalInvoice_id) & ") "
        'sSqlSelect &= "    AND (P1.isReversal = 0)  "
        'sSqlSelect &= "    AND (P1.Payment_id NOT IN  "
        'sSqlSelect &= "        (SELECT originalPayment_id FROM Payments P2 WHERE P2.isReversal <>0) ) "

        ''== END  Target-New-Build-4259 -- (Started 17-Jul-2020)

        ''- get  record set.-
        'If Not gbGetDataTableEx(mCnnSql, dtTempPayments, sSqlSelect, sqlTransaction1) Then
        '    sqlTransaction1.Rollback()
        '    MsgBox("Error in getting recordset for Payments table: " & vbCrLf & _
        '                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        '    Exit Function '--msg was displayed..
        'Else
        '    '-- check list of Payments.
        'End If  '--get table-
        'If (dtTempPayments IsNot Nothing) AndAlso (dtTempPayments.Rows.Count > 0) Then
        '    Dim intUnrevId As Integer
        '    intUnrevId = dtTempPayments.Rows(0).Item("payment_id")
        '    sqlTransaction1.Rollback()
        '    MsgBox("This account Invoice No: " & intOriginalInvoice_id & _
        '              " has UNREVERSED Payment (" & intUnrevId & ") made on it, and cannot be reversed..", MsgBoxStyle.Exclamation)
        '    Exit Function
        'End If
        '-- Done payments..

        '--  END DON'T NEED THIS --
        '--  END DON'T NEED THIS --
        '--  END DON'T NEED THIS --







        '--  INSERT new Refund Invoice Record..  Use all money values from Original Invoice.
        '--  INSERT new Refund Invoice Record..  Use all money values from Original Invoice.

        Dim sSqlInsert, sSqlUpdate As String
        Dim sValues As String = ""
        Dim sComments As String = _
                "This is a REVERSAL (Refund) of Cash Sale Invoice No: " & CStr(intOriginalInvoice_id) & ".."
        Dim intRefundInvoice_id As Integer

        Try   '--Main INSERT Try..

            '- 2. INSERT Main REFUND Invoice record -
            '- 2. INSERT Main REFUND Invoice record -
            '- 2. INSERT Main REFUND Invoice record -

            sSqlInsert = "INSERT INTO dbo.Invoice ("
            sSqlInsert &= "  staff_id, customer_id, transactionType,  "

            sValues = "VALUES ( " & CStr(mIntCallerStaff_id) & ", " & _
                                          CStr(intCustomer_id) & ", 'refund', "
            sSqlInsert &= "  terminal_id, cashDrawer, currentWindowsUserName,  "
            sSqlInsert &= " isOnAccount, original_id, "
            sValues &= "'" & msComputerName & "', "
            sValues &= "'" & gsGetCurrentCashDrawer() & "', '" & msCurrentUserName & "', "
            sValues &= "0, " & CStr(intOriginalInvoice_id) & ", "   '-isOnAccount,  OriginalInvoice.

            '-- Transaction values sre from the ORIGINAL Invoice..
            sSqlInsert &= " subtotal_ex_non_taxable, subtotal_ex_taxable, "
            sValues &= CStr(rowInvoice1.Item("subtotal_ex_non_taxable")) & ", " & _
                                              CStr(rowInvoice1.Item("subtotal_ex_taxable")) & ", "
            '-rowInvoice1.Item("")-
            sSqlInsert &= " subtotal_tax, subtotal_inc, discount_nett, discount_tax, "
            sValues &= CStr(rowInvoice1.Item("subtotal_tax")) & ", " & _
                             CStr(rowInvoice1.Item("subtotal_inc")) & ", " & _
                                    CStr(rowInvoice1.Item("discount_nett")) & ", " & _
                                            CStr(rowInvoice1.Item("discount_tax")) & ", "
            sSqlInsert &= " rounding, total_ex, total_tax, total_inc, "
            sValues &= CStr(rowInvoice1.Item("rounding")) & ", "
            sValues &= CStr(rowInvoice1.Item("total_ex")) & ", " & _
                              CStr(rowInvoice1.Item("total_tax")) & ", " & _
                                     CStr(rowInvoice1.Item("total_inc")) & ", "
            '-- Now Finish-
            sSqlInsert &= " comments "
            sSqlInsert &= ") "
            sValues &= " '" & gsFixSqlStr(sComments) & "' "
            sValues &= "); "

            If Not mbExecuteSql(mCnnSql, sSqlInsert & sValues, True, sqlTransaction1) Then
                '= mLabSaleHelp2.Text = "Saving " & msTransactionType & " FAILED.."
                '= mbCommitLocked = False
                Exit Function
            End If  '--exec invoice-

            '- 3. Retrieve Invoice No. (IDENTITY of Invoice record written.)-
            '= sSql = "SELECT CAST(SCOPE_IDENTITY ('dbo." & sMainTable & "') AS int);"
            sSqlSelect = "SELECT CAST(SCOPE_IDENTITY () AS int);"
            If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSqlSelect, True, sqlTransaction1, intID) Then
                intRefundInvoice_id = intID
                '-- update invoice display later..-
                mIntRefundInvoice_id = intRefundInvoice_id
            Else
                MsgBox("Failed to retrieve latest invoice/Refund No..", MsgBoxStyle.Exclamation)
                '= mbCommitLocked = False
                Exit Function
            End If

            '- 4. FOR EACH Invoice Line- INSERT Invoice-Line.. -
            '--                     And ->  UPDATE Stock Record with +/- Qty. (IF NOT Service/labour) -

            '--  Insert all Line Items from Original.. GET Stock Table Info for Item. 
            '--           and Update Stock (is a Refund, so ADD to stock if not nonStockItem)
            '--             and update Serial Audit AND  TRAIL if original LINE item has serialNumber and SerialAudit_id..
            '--       note- k_GRIDCOL_ISSERVICEITEM is from Stock Table-  "isNonStockItem"

            Dim intAudit_id, intStock_id, intInvoiceLine_id As Integer
            Dim sSerialNo, sQty As String

            For Each itemRow1 As DataRow In mDataTableOriginalSaleItems.Rows
                intStock_id = CInt(itemRow1.Item("stock_id"))
                intAudit_id = CInt(itemRow1.Item("SerialAudit_id"))
                '- check serial it was..
                sSerialNo = Trim(itemRow1.Item("SerialNumber"))
                sQty = CStr(itemRow1.Item("quantity"))

                sSqlInsert = "INSERT INTO dbo.InvoiceLine ("
                sSqlInsert &= " invoice_id, stock_id,  SerialNumber, SerialAudit_id, "

                sValues = "VALUES ( " & CStr(intRefundInvoice_id) & ", " & CStr(intStock_id) & ", " & _
                                                        "'" & gsFixSqlStr(sSerialNo) & "', " & CStr(intAudit_id) & ", "
                sSqlInsert &= " description, cost_ex, cost_inc, "
                sSqlInsert &= " sell_ex, sales_taxCode, sales_taxPercentage, "
                sSqlInsert &= " sell_inc, sellActual_ex, sellActual_Tax, sellActual_inc, "
                sSqlInsert &= " quantity, total_ex, total_tax, total_inc, gross_profit  "
                sSqlInsert &= ") "

                sValues &= "'" & gsFixSqlStr(itemRow1.Item("description")) & "', "
                sValues &= CStr(itemRow1.Item("cost_ex")) & ", "
                sValues &= CStr(itemRow1.Item("cost_inc")) & ", "
                sValues &= CStr(itemRow1.Item("sell_ex")) & ", "
                sValues &= "'" & gsFixSqlStr(itemRow1.Item("sales_taxCode")) & "', "
                sValues &= CStr(itemRow1.Item("sales_taxPercentage")) & ", "
                sValues &= CStr(itemRow1.Item("sell_inc")) & ", "
                sValues &= CStr(itemRow1.Item("sellActual_ex")) & ", "
                sValues &= CStr(itemRow1.Item("sellActual_tax")) & ", "
                sValues &= CStr(itemRow1.Item("sellActual_inc")) & ", "
                sValues &= CStr(itemRow1.Item("quantity")) & ", "
                sValues &= CStr(itemRow1.Item("total_ex")) & ", "
                sValues &= CStr(itemRow1.Item("total_tax")) & ", "
                sValues &= CStr(itemRow1.Item("total_inc"))
                sValues &= ", " & CStr(itemRow1.Item("gross_profit"))
                sValues &= "); "
                '-- insert this row..-
                If Not mbExecuteSql(mCnnSql, sSqlInsert & sValues, True, sqlTransaction1) Then
                    '= mLabSaleHelp2.Text = "Insert Failed.."
                    '= mbCommitLocked = False
                    Exit Function
                End If  '--exec invoice LINE-

                '--  Bring stuff back into stock..
                '-- get invoice Line_id--
                '-get ID of last line inserted.. (For Serial-Audit-Trail).
                '= sSql = "SELECT CAST(SCOPE_IDENTITY ('dbo.InvoiceLine') AS int);"
                sSqlSelect = "SELECT CAST(SCOPE_IDENTITY () AS int);"
                If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSqlSelect, True, sqlTransaction1, intID) Then
                    intInvoiceLine_id = intID  '-- this goes into serialAuditTrail below..-
                Else
                    MsgBox("Failed to read latest Invoice LINE-ID No..", MsgBoxStyle.Exclamation)
                    '=mbCommitLocked = False
                    Exit Function
                End If
                '--  update stock on hand if not service..-
                If Not listOfNonStockItems.Contains(intStock_id) Then
                    '--not a NON-Stock item..
                    '-- BRING back into stock.. (It's a refund.)
                    sSqlUpdate = "UPDATE dbo.stock SET "
                    sSqlUpdate &= " qtyInStock=(qtyInStock + " & sQty & ")"
                    sSqlUpdate &= "  WHERE (stock_id = " & CStr(intStock_id) & " );"
                    If Not mbExecuteSql(mCnnSql, sSqlUpdate, True, sqlTransaction1) Then
                        '=mLabSaleHelp2.Text = "Update Stock Failed.."
                        '= mbCommitLocked = False
                        Exit Function
                    End If  '--exec Update stock qty-

                    '--   IF SERIALized ->  UPDATE SerialAudit status -> SOLD. -
                    '--            And ->  INSERT SerialAuditTrail record.. (SALE/REFUND (INSTOCK)).-
                    '--  Bring Serial back in if it is a serial..
                    If (intAudit_id > 0) And (sSerialNo <> "") Then
                        sSqlUpdate = "UPDATE dbo.serialAudit  "
                        '= sSqlUpdate &= IIf(bIsCredit, "SET status='INSTOCK', isInStock=1", "SET status='SOLD', isInStock=0")
                        sSqlUpdate &= " SET status='INSTOCK', isInStock=1"
                        sSqlUpdate &= "  WHERE (serial_id=" & CStr(intAudit_id) & ");"
                        If Not mbExecuteSql(mCnnSql, sSqlUpdate, True, sqlTransaction1) Then
                            '=mLabSaleHelp2.Text = "Update SerialAudit Failed.."
                            '=mbCommitLocked = False
                            Exit Function
                        End If  '--exec Update serial status-
                        '--ok.. insert SerialAuditTrail record.-
                        '- This is the "transaction" trail for this serial.-
                        sSqlInsert = "INSERT INTO dbo.SerialAuditTrail ("
                        sSqlInsert &= " stock_id, SerialAudit_id, "
                        sSqlInsert &= "  tran_type, type_id, type_line_id, movement"
                        sSqlInsert &= ") "
                        sSqlInsert &= "VALUES ( "
                        sSqlInsert &= CStr(intStock_id) & ", " & CStr(intAudit_id) & ", "
                        sSqlInsert &= "'refund', "
                        sSqlInsert &= CStr(intRefundInvoice_id) & ", " & CStr(intInvoiceLine_id) & ", 1"
                        sSqlInsert &= "); "
                        '-- insert this TRAIL rec..-
                        If Not mbExecuteSql(mCnnSql, sSqlInsert, True, sqlTransaction1) Then
                            '= mLabSaleHelp2.Text = "Saving Serial Audit TRAIL record FAILED.."
                            '= mbCommitLocked = False
                            Exit Function
                        End If  '--exec serial-
                    End If  '-serial-
                End If  '-non-stock..

            Next itemRow1

            '-- Done all invoice lines..

            '-- BEFORE Committing..
            '--  UPDATE original Invoice COMMENTS to Show it was reversed....

            sComments = "NB: This Sale Invoice has been REVERSED by Refund #" & intRefundInvoice_id & ".." & vbCrLf
            sComments &= " By " & msCallerStaffName & "  On " & Format(Now, "dd-MMM-yyyy HH:mm ") & vbCrLf & vbCrLf

            sSqlUpdate = "UPDATE dbo.Invoice "
            sSqlUpdate &= "  SET comments ='" & gsFixSqlStr(sComments) & "' + comments "
            sSqlUpdate &= " WHERE invoice_id =" & intOriginalInvoice_id & ";"

            If Not mbExecuteSql(mCnnSql, sSqlUpdate, True, sqlTransaction1) Then
                '=mLabSaleHelp2.Text = "Update SerialAudit Failed.."
                '=mbCommitLocked = False
                Exit Function
            End If  '--exec Update serial status-

        Catch ex As Exception
            sqlTransaction1.Rollback()
            MsgBox("Error building SQL Refund Transaction..(" & msCallerStaffName & ")" & _
                    " Rollback was done.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try '--Main INSERT Try..


        '-- REVERSE the Payment that is associated with this Cash Sale..
        '-- REVERSE the Payment that is associated with this Cash Sale..
        '-- REVERSE the Payment that is associated with this Cash Sale..

        '--  FROM frmShowPayment-

                '- ok. make the reversal--
        '-- We just need to copy out all the original Payment details..
        '--   with the isReversal flag set..
        '= btnReverseNow.Enabled = False

        '= Dim sqlTransaction1 As OleDbTransaction
        Dim rowOriginalPayment As DataRow

        Dim sSql, sFieldList As String  '=, sValues, sComments As String
        Dim v2 As Object
        '= Dim bIsCredit As Boolean = (LCase(msTransactionType) = "refund")
        '== NB AccountPayments, the Payments Table has -1 in primary InvoiceNo=
        '=    See Disbursements table for related invoices for the Payment...
        Dim intInvoice_id As Integer = -1
        Dim intOriginalPayment_id, intRevPayment_Id As Integer '=, intID As Integer
        Dim datarow1 As DataRow
        Dim sPayAmount, sDescription, sKey As String
        Dim decAmount, decInvoiceTotal, decTotalTax As Decimal

        '= mCnnSql.ChangeDatabase(msSqlDbName) '--USE our db..-

        If (mDataTableOriginalPayments.Rows.Count > 0) Then
            rowOriginalPayment = mDataTableOriginalPayments.Rows(0)
        Else
            MsgBox("Error !  No Payment Row found to reverse..", MsgBoxStyle.Exclamation)
            Exit Function 
        End If
        intOriginalPayment_id = rowOriginalPayment.Item("payment_id")
        intInvoice_id = rowOriginalPayment.Item("invoice_id")
        sComments = "Reversing Original Payment No: " & intOriginalPayment_id & ".."

        '-- GET these--
        '-- GET these--
        '-- GET these--
        '==   Private mDataTablePaymentDetails As DataTable
        '=  Private mDataTableDisbursements As DataTable --

        sSql = "SELECT * FROM dbo.PaymentDetails "
        sSql &= "  WHERE (PaymentDetails.payment_id= " & CStr(intOriginalPayment_id) & ");"
        '- get  record set.-
        If Not gbGetDataTableEx(mCnnSql, mDataTablePaymentDetails, sSql, sqlTransaction1) Then
            sqlTransaction1.Rollback()
            MsgBox("Error in getting data for PaymentDetails table: " & vbCrLf &
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function '--msg was displayed..
        Else
            If Not (mDataTablePaymentDetails Is Nothing) Then
                '--ok-
            Else '-error-
                sqlTransaction1.Rollback()
                MsgBox("Failed to get Payment Details data..", MsgBoxStyle.Exclamation)
                Exit Function
            End If  '-nothing-
        End If  '-get-

        '--  Show invoice disbursements for this payment..
        sSql = "SELECT * FROM dbo.PaymentDisbursements "
        sSql &= "  WHERE (payment_id= " & CStr(intOriginalPayment_id) & ");"
        '- get  record set.-
        If Not gbGetDataTableEx(mCnnSql, mDataTableDisbursements, sSql, sqlTransaction1) Then
            sqlTransaction1.Rollback()
            MsgBox("Error in getting data for PaymentDisbursements table: " & vbCrLf &
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function '--msg was displayed..
        Else
        End If  '--get-

        '-- copy out all the original Payment details..
        Dim decPaymentTotalRcvd As Decimal '= = rowOriginalPayment.Item("totalAmountReceived")
        Dim decTotalDiscount As Decimal '= = rowOriginalPayment.Item("discountGivenOnPayment")
        Dim decChangeAsCash As Decimal '= = rowOriginalPayment.Item("changeGiven")
        Dim decPaymentNettCredited As Decimal '= = rowOriginalPayment.Item("nettAmountCredited")
        Dim decChangeAsCredit As Decimal '= = rowOriginalPayment.Item("creditNotePaymentCredited")
        Dim decCreditNoteCreditApplying As Decimal '= = rowOriginalPayment.Item("creditNoteAmountDebited")
        With rowOriginalPayment
            decPaymentTotalRcvd = .Item("totalAmountReceived")
            decTotalDiscount = .Item("discountGivenOnPayment")
            decChangeAsCash = .Item("changeGiven")
            decPaymentNettCredited = .Item("nettAmountCredited")
            decChangeAsCredit = .Item("creditNotePaymentCredited")
            decCreditNoteCreditApplying = .Item("creditNoteAmountDebited")
        End With

        '-  DONE ABOVE..  1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
        '== sqlTransaction1 = mCnnSql.BeginTransaction

        '= labHelp.Text = "Invoice #" & intInvoice_id & ":  Saving Payment record.."
        sSql = "INSERT INTO dbo.payments ("
        sSql &= "  staff_id, customer_id, invoice_id, "
        sSql &= " transactionType, isReversal, originalPayment_id, totalAmountReceived, "
        sSql &= " discountGivenOnPayment, changeGiven, nettAmountCredited, "
        sSql &= " creditNotePaymentCredited, creditNoteAmountDebited, "
        sSql &= "    terminal_id, cashDrawer, currentWindowsUserName, comments "
        sSql &= ") "
        sSql &= "VALUES ( "
        sSql &= CStr(mIntCallerStaff_id) & ", " & CStr(intCustomer_id) & ", " & CStr(mIntRefundInvoice_id) & ", "
        sSql &= "'CshSaleReversal', 1, " & CStr(intOriginalPayment_id) & ", " & CStr(decPaymentTotalRcvd) & ", "
        sSql &= CStr(decTotalDiscount) & ", "
        sSql &= CStr(decChangeAsCash) & ", " & CStr(decPaymentNettCredited) & ", "
        '=3403.1017- Debtor can have Credit Note..
        sSql &= CStr(decChangeAsCredit) & ", " & CStr(decCreditNoteCreditApplying) & ", "
        sSql &= "'" & msComputerName & "', "
        sSql &= "'" & gsGetCurrentCashDrawer() & "', '" & gsFixSqlStr(msCurrentUserName) & "', "
        sSql &= "'Cash Payment: Reversal '"
        sSql &= "); "
        '-- Save-
        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
            '= labHelp.Text = "Saving Payment Record FAILED.."
            Exit Function 
        End If  '--exec invoice-

        '- 2. Retrieve Payment No. (IDENTITY of Reversed Payment record written.)-
        sSql = "SELECT CAST(IDENT_CURRENT ('dbo.payments') AS int);"
        If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
            intRevPayment_Id = intID
            '-- update invoice display later..-
        Else
            MsgBox("Failed to retrieve Reversal Payment No..", MsgBoxStyle.Exclamation)
            Exit Function 
        End If
        '= labHelp.Text = "Saving Payment details.."
        DoEvents()

        '- 3. FOR EACH Payment Detail: INSERT Payment Detail Row.-
        If Not (mDataTablePaymentDetails Is Nothing) Then
            For Each datarow1 In mDataTablePaymentDetails.Rows
                '-- save detail (payment) line-
                '=  sPayAmount, sDescription, sKey --
                sKey = datarow1.Item("paymentType_key")
                sDescription = datarow1.Item("paymentType_descr")

                sSql = "INSERT INTO dbo.paymentdetails ("
                sSql &= "  payment_id,  paymentType_key, paymentType_descr, "
                sSql &= "  amount, comments )"
                sSql &= "  VALUES (" & CStr(intRevPayment_Id) & ", "
                sSql &= "'" & gsFixSqlStr(sKey) & "', "
                sSql &= "'" & gsFixSqlStr(sDescription) & "', "
                sSql &= CStr(datarow1.Item("amount")) & ", '" & sComments & "' "
                sSql &= "); "
                '-- insert this row..-
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    '=labHelp.Text = "Insert Payment record Failed.."
                    Exit Function 
                End If  '--exec INSERT pay detail LINE-
            Next datarow1
        End If  '-details- nothing.-
        '- 4. INSERT Payment-Disbursement Child Table Rows for all Invoices.-
        '--       covered in this payment....
        If Not (mDataTableDisbursements Is Nothing) Then
            '= Dim intDisbInvoice_id As Integer
            Dim sTrancode As String
            For Each datarow1 In mDataTableDisbursements.Rows
                'sList &= Format(row1.Item("invoice_id"), "  000") & ": " & _
                '           FormatCurrency(row1.Item("amount"), 2) & "; "
                '= intDisbInvoice_id = datarow1.Item("invoice_id")
                sTrancode = datarow1.Item("tranCode")
                decAmount = datarow1.Item("amount")
                '-- sav disb. detail line-
                sSql = "INSERT INTO dbo.paymentDisbursements ("
                sSql &= "  payment_id, invoice_id, tranCode, sourceOfFunds,  "
                sSql &= "  amount )"
                sSql &= "  VALUES (" & CStr(intRevPayment_Id) & ", " & CStr(mIntRefundInvoice_id) & ", "
                sSql &= "'" & sTrancode & "', '" & gsFixSqlStr(sTrancode) & "', "
                sSql &= CStr(decAmount)
                sSql &= "); "
                '-- insert this row..-
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    '=  labHelp.Text = "Insert Pay-disbursement Failed.."
                    Exit Function 
                End If  '--exec pay disb. LINE-
            Next datarow1
        End If  '-disb. nothing-.

        '- that's all (for the payment)...
        '- 5. -- Commit TRANSACTION.---






        '-- Commit..

        '- 9.  Commit TRANSACTION.-
        Try
            sqlTransaction1.Commit()
            '= MsgBox(mLabSaleTranType.Text & " Transaction committed ok.." & vbCrLf & _
            '=         sMainTable & " No: " & intInvoice_id, MsgBoxStyle.Information)
        Catch ex As Exception
            Try
                sqlTransaction1.Rollback()
                MsgBox("Transaction committing REFUND FAILED.. rollback completed.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                '=MsgBox("Transaction rolback completed.. " & vbCrLf & ex.Message, MsgBoxStyle.Information)
            Catch ex2 As Exception
                MsgBox("Transaction committing REFUND FAILED.. and Rollback FAILED.. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            End Try
            '=mbCommitLocked = False
            Exit Function
        End Try  '-commit-
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '-- Refund done..
        CreateCashSaleReversal = True

        MsgBox("ok.. The Reversal REFUND Transaction was committed.." & vbCrLf & _
               " Refund No is: " & intRefundInvoice_id, MsgBoxStyle.Information)

        '-- Show created refund..

        Dim frmShowInvoice1 As frmShowInvoice

        frmShowInvoice1 = New frmShowInvoice

        frmShowInvoice1.connectionSql = mCnnSql
        frmShowInvoice1.InvoiceNo = intRefundInvoice_id
        frmShowInvoice1.isQuote = False
        frmShowInvoice1.islayby = False
        '-- can use main signon- mIntMainStaff_id -
        'If (mIntSaleStaff_id <= 0) Then
        '    frmShowInvoice1.Staff_id = mIntMainStaff_id  '- no current sale login.-
        'Else  '-ok- current sale.
        frmShowInvoice1.Staff_id = mIntCallerStaff_id
        'End If
        '=If bCaptureInvoicePDF Then
        '=frmShowInvoice1.CaptureInvoicePDF = bCaptureInvoicePDF  '--capture pdf for email..
        '= frmShowInvoice1.PrintInvoiceAnyway = bPrintInvoiceAnyway  '--if checked..
        frmShowInvoice1.A4InvoiceRequested = True
        '= End If
        frmShowInvoice1.UserLogo = mImageUserLogo
        '== ADDED 3401.319=
        frmShowInvoice1.selectedInvoicePrinterName = msSelectedInvoicePrinterName
        frmShowInvoice1.selectedReceiptPrinterName = msSelectedReceiptPrinterName

        frmShowInvoice1.ShowDialog()

        '-- All done..


    End Function  '-CreateAccountReversal-
    '= = = = = = = = = = = = == = = = = 


End Class  '-clsCashSaleReversal-
'= = = = =  = = = = = = = = = = =
