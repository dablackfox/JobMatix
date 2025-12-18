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
Imports system.data.OleDb
Imports System.Math
Imports System.ComponentModel

Public Class frmShowPayment


    '-- POS-- Show/print payment record..
    '==
    '==  grh JobMatixPOS 3.1.3101.1219 -
    '==       >> Clone form from showInvoice..
    '== 
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==
    '==     3403.1031- 31-Oct-2017-
    '==        >> AccountPayment - frmShowPayment upgraded to support eMailing Receipt...  
    '==        >> AccountPayment - REVERSAL implmented here...  
    '==
    '==       3411.0113=  13-Jan-2018=
    '==          --  Get PDF preferred printer...
    '==             -(Microsoft PDF will be preferred)..
    '==
    '==       3411.0314=  14-Mar-2018=
    '==          --  Fix Customer name spacing for email....
    '==
    '==
    '==       3411.0422=  22-Apr-2018=
    '==          --  Fix to No Staffid exit.....
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== -- Updated 3501.1030  24/29/30-Oct-2018=  
    '==     -- Show Till id (cashDrawer) on ShowInvoice/showPayment Forms... 
    '==
    '==
    '== -- Updated 4201.1007 07-Oct-2019=  
    '==     -- Show Discount on Payment in List of payments.... 
    '==
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1130.  06-Nov-2019-  Started 06-November-2019-
    '==      --  On frmShowPayment, For non-account (ie, for Cash sales), 
    '==            the Payment record column "nettAmountCredited" does NOT include creditNoteWDl amount..
    '==              So add this Cr-noteWdl amt in to show total contriburion to payment for the Sale.
    '==
    '= = = = = = = = = = = = = = = = = =  == = =  = = = = == = = =
    '== 
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '==
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==
    '==     -- ALSO- Fix Terminal_id missing in REFUND for  clsCshSaleReversal, clsAccountReversal, 
    '==                     and frmShowPayment (for Payment Reversal.).
    '==
    '== msComputerName = My.Computer.Name
    '= = = = = = = = == = = = = = === = = = = = = = = = = 


    Private Const k_receiptPrtSettingKey As String = "POS_ReceiptPrinter"

    Private mFrmParent As Form
    Private mbActivated As Boolean = False
    '- - - - -
    Private mbIsInitialising As Boolean = False
    Private mbActive As Boolean = False
    Private mbStartingUp As Boolean
    Private msVersionPOS As String = ""

    Private msCurrentUserName As String = ""
    '= Private msCurrentUserNT As String = ""
    '= Private mbIsSqlAdmin As Boolean = False
    '=msCurrentUserName=

    Private msComputerName As String '--local machine--
    Private msAppPath As String
    '== Private msLastSqlErrorMessage As String = ""
    Private msAppFullname As String = ""
    Private msInvoiceFilePath As String = ""

    Private msEmailQueueSharePath As String = ""
    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--
    Private mCnnSql As OleDbConnection '--
    Private mlJobId As Integer = -1

    Private msBusinessABN As String = ""
    Private msBusinessName As String = ""
    Private msEmailTextReceipt As String = ""

    '= Private msInvoicePrinterName As String = ""
    Private msReceiptPrinterName As String = ""
    '== Private msLabelPrinterName As String = ""
    Private msDefaultPrinterName As String = ""

    Private msPdfPrinterName As String = ""  '=3401.1031=
    Private msSelectedReceiptPrinterName As String = ""
    Private mbCaptureReceiptPDF As Boolean = False
    Private mbCanPrintReceipt As Boolean = True

    '== Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '==3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private mImageUserLogo As Image

    '- Current PAYMENT DETAILS --

    Private mIntPayment_id As Integer = -1
    Private msTranCode As String = ""
    Private mbPaymentReversalRequested As Boolean = False

    Private mIntOriginalPayment_id As Integer = -1

    Private msOriginalStaffName As String = ""
    Private mIntOriginalStaff_id As Integer = -1
    '- current-
    Private msCallerStaffName As String = ""
    Private mIntCallerStaff_id As Integer = -1

    Private msCustBarcode As String = ""
    Private mIntCustomer_id As Integer = -1
    Private msCustomerName As String = ""
    Private msCustomerEmail As String = ""

    '== Private mDataTableInvoice As DataTable
    '== Private mDataTableSaleItems As DataTable
    Private mDataTablePayments As DataTable
    Private mDataTablePaymentDetails As DataTable
    Private mDataTableDisbursements As DataTable

    Private mbIsReversal As Boolean = False

    '= = = = = = = = = = = = = = = = = = =

    '- for quotes-
    '== Private sIdColumn As String
    '== Private sDateColumn As String

    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--properties as input parameters--
    WriteOnly Property FrmParent() As Form
        Set(ByVal value As Form)
            mFrmParent = value
        End Set
    End Property  '= parent form=
    '= = = = =  = = = = = = = ==  == 

    '-version-
    WriteOnly Property versionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
        End Set
    End Property  '--version--
    '= = = = = = = = = = = = = = = = = = == 

    '--This Staff Id now comes from caller..--

    WriteOnly Property Staff_id() As Integer
        Set(ByVal Value As Integer)
            mIntCallerStaff_id = Value
        End Set
    End Property '--id--
    '= = = = = = = =  = = = = = = = = = =

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msCallerStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = = = = = = = == = = = = =

    WriteOnly Property connectionSql() As OleDbConnection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =

    WriteOnly Property sqlDbname As String
        Set(value As String)
            msSqlDbName = value
        End Set
    End Property
    '= = = = = = = = = = = = = 

    WriteOnly Property PaymentNo() As Integer
        Set(ByVal value As Integer)
            mIntPayment_id = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = 

    WriteOnly Property PaymentReversalRequested As Boolean
        Set(value As Boolean)
            mbPaymentReversalRequested = value
        End Set
    End Property  '-reversal-
    '= = = = = = = = = = = = = == = = =


    '-- set user logo for printing..--
    WriteOnly Property UserLogo() As Image
        Set(ByVal Value As Image)

            mImageUserLogo = Value
        End Set
    End Property '--logo..--
    '= = = = = = = = = = = = = = = = =

    WriteOnly Property selectedReceiptPrinterName() As String
        Set(ByVal Value As String)
            msSelectedReceiptPrinterName = Value
        End Set
    End Property '--receipt.--
    '= = = = = = = =  = = =

    '-- capture pdf for email when Payment is first done..

    WriteOnly Property CaptureReceiptPDF As Boolean
        Set(value As Boolean)
            mbCaptureReceiptPDF = value
        End Set
    End Property '-pdf-
    '= = = = = = = = = = = = = =  = = = == =

    '-- Print Anyway- req. when Sale id first done..

    WriteOnly Property CanPrintReceipt As Boolean
        Set(value As Boolean)
            mbCanPrintReceipt = value
        End Set
    End Property '-pdf-
    '= = = = = = = = = = = = = =  = = = == =


    '== WriteOnly Property Settings() As clsStrDictionary
    '==     Set(ByVal value As clsStrDictionary)
    '==         mSdSettings = value
    '==     End Set
    '== End Property
    '= = = = = = = = = = = = = = = = = =

    '== WriteOnly Property SystemInfo() As clsStrDictionary
    '==     Set(ByVal value As clsStrDictionary)
    '==         mSdSystemInfo = value
    '==     End Set
    '== End Property '-systeminfo-
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

    '-- Build receipt Lines Collection--
    '==   -- 4219.1130.  06-Nov-2019-  Started 06-November-2019-
    '==      --  On frmShowPayment, For non-account (ie, for Cash sales), 
    '==            the Payment record column "nettAmountCredited" does NOT include creditNoteWDl amount..
    '==              So add this Cr-noteWdl amt in to show total contriburion to payment for the Sale.

    Private Function mBuildReceipt(ByRef rowPayment As DataRow, _
                                   ByRef colReceiptLines As Collection) As Boolean
        '= Dim colReceiptLines As Collection
        '= Dim prtDocs1 As New clsPrintSaleDocs
        Dim sABN, s1, s2 As String
        Dim row1 As DataRow
        '= Dim rowPayment As DataRow
        '= Dim bIsAccountCust As Boolean = False  '-- TEMP --
        Dim sPayDescr As String
        Dim sNettRecvd, sChangeGiven As String
        '== Dim decSubTotalInc, decTotalInc As Decimal
        Dim decPayAmount, decTotalPayments As Decimal
        Dim decCreditNoteRedeemed, decCreditNoteSaved As Decimal
        Dim decNettAmountCredited As Decimal = 0
        Dim bIsReversal As Boolean = False
        Dim sTrancode As String
        Dim bIsAccountPayment As Boolean = False

        '-colReceiptLines is ready to receive..
        mBuildReceipt = False
        '-4219.1130=
        If rowPayment IsNot Nothing Then
            sTrancode = LCase(rowPayment.Item("transactionType"))
            If (sTrancode = "account") Then
                bIsAccountPayment = True
            End If
        End If
        Try
            mIntOriginalStaff_id = CInt(rowPayment.Item("staff_id"))
            bIsReversal = IIf((rowPayment.Item("IsReversal") = 0), False, True)

            '-- Format ABN for printing..-
            sABN = VB.Left(msBusinessABN, 2) & " " & Mid(msBusinessABN, 3, 3) & _
                      " " & Mid(msBusinessABN, 6, 3) & " " & Mid(msBusinessABN, 9, 3)
            colReceiptLines.Add("") '--new line..--
            colReceiptLines.Add("") '--new line..--
            colReceiptLines.Add("<bold>")
            If bIsAccountPayment Then
                colReceiptLines.Add("- Account Payment Receipt - ")
            Else  '-cash sale-
                colReceiptLines.Add("- Payment Receipt - ")
            End If
            If bIsReversal Then
                colReceiptLines.Add("<bold>")
                colReceiptLines.Add("- R E V E R S A L - ")
            End If
            colReceiptLines.Add("<bold>")
            colReceiptLines.Add(Format(rowPayment.Item("payment_date"), "ddd dd-MMM-yyyy"))
            colReceiptLines.Add("Receipt No: " & CStr(mIntPayment_id) & ".")
            colReceiptLines.Add("") '--new line..--
            colReceiptLines.Add("<big>")
            colReceiptLines.Add("<bold>")
            If (msBusinessName <> "") Then
                colReceiptLines.Add(msBusinessName) '--"Precise PCs"
            Else
                colReceiptLines.Add("-JobMatix POS -")
            End If
            colReceiptLines.Add("<bold>")
            '====colLines.Add "<ul>"
            colReceiptLines.Add("ABN: " & sABN & ".")
            colReceiptLines.Add("Served by: " & msOriginalStaffName)
            colReceiptLines.Add("")
            colReceiptLines.Add("<bold>")
            colReceiptLines.Add("Customer:")
            colReceiptLines.Add("Account No: " & rowPayment.Item("customer_barcode") & ".")
            colReceiptLines.Add(rowPayment.Item("firstname") & " " & rowPayment.Item("lastname"))
            colReceiptLines.Add("")

            '-- Add list of payments details..
            colReceiptLines.Add("Payments:")
            '-- get payment and change if any..-
            If (Not (mDataTablePayments Is Nothing)) AndAlso (mDataTablePayments.Rows.Count > 0) Then
                row1 = mDataTablePayments.Rows(0)
                decCreditNoteRedeemed = CDec(row1.Item("creditNoteAmountDebited"))
                decCreditNoteSaved = CDec(row1.Item("creditNotePaymentCredited"))
                '=4219.1130=
                decNettAmountCredited = CDec(row1.Item("nettAmountCredited"))

                sNettRecvd = FormatCurrency(row1.Item("nettAmountCredited"), 2)  '-- for account pymnt, includeds CrNote wdl.
                If Not bIsAccountPayment Then  '-cash sale=
                    sNettRecvd = FormatCurrency((decNettAmountCredited + decCreditNoteRedeemed), 2)
                End If
                s1 = FormatCurrency(row1.Item("changeGiven"), 2)
                sChangeGiven = RSet(s1, 12)
                colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                colReceiptLines.Add("(Change Given:" & sChangeGiven & ")")
                If decCreditNoteRedeemed > 0 Then
                    s1 = LSet("CreditNoteRedeemed:", 20)
                    s2 = RSet(FormatCurrency(decCreditNoteRedeemed, 2), 12)
                    colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                    colReceiptLines.Add(s1 & s2)
                End If
                '- show variable details.
                If Not (mDataTablePaymentDetails Is Nothing) Then
                    For Each row1 In mDataTablePaymentDetails.Rows
                        s1 = CStr(row1.Item("paymentType_key"))
                        sPayDescr = LSet(s1 & ":", 20)
                        decPayAmount = row1.Item("amount")
                        decTotalPayments += decPayAmount
                        '=sPayAmount = RSet(FormatCurrency(decPayAmount, 2), 12)
                        colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                        colReceiptLines.Add(sPayDescr & RSet(FormatCurrency(decPayAmount, 2), 12))
                        '==listPaymentDetail.Items.Add(sPayDescr & "|" & sPayAmount)
                    Next row1
                    colReceiptLines.Add("")
                End If  '-nothing-
            End If
            colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
            colReceiptLines.Add(LSet("Credited to Invoice:", 20) & RSet(sNettRecvd, 12))
            colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
            colReceiptLines.Add(LSet("Total New Paymnts:", 20) & RSet(FormatCurrency(decTotalPayments, 2), 12))
            If decCreditNoteSaved > 0 Then
                s1 = LSet("CreditNote Saved:", 20)
                s2 = RSet(FormatCurrency(decCreditNoteSaved, 2), 12)
                colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                colReceiptLines.Add(s1 & s2)
            End If
            colReceiptLines.Add("")
            colReceiptLines.Add(labInvoiceList.Text)
            colReceiptLines.Add("")
            colReceiptLines.Add("<tahoma>")
            colReceiptLines.Add("<bold>")
            colReceiptLines.Add("<big>")
            '--colLines.Add "<bold>"
            colReceiptLines.Add("Thank You.")
            colReceiptLines.Add("= = = = = = = = = = = = = = = = = = = = =") '--new line..--
            mBuildReceipt = True
        Catch ex As Exception
            MsgBox("Error-in building Receipt lines.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try

    End Function  '-mbuildReceipt-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- show payment details..
    '==   -- 4219.1130.  06-Nov-2019-  Started 06-November-2019-
    '==      --  On frmShowPayment, For non-account (ie, for Cash sales), 
    '==            the Payment record column "nettAmountCredited" does NOT include creditNoteWDl amount..
    '==              So add this Cr-noteWdl amt in to show total contriburion to payment for the Sale.

    Private Function mbShowPaymentDetails(ByVal intPaymentRowNo As Integer) As Boolean
        Dim decTotalPayments, decPayAmount, decChange As Decimal
        Dim decCreditNoteRedeemed, decCreditNoteSaved As Decimal
        Dim decTotalDiscountOnPayment As Decimal = 0
        Dim decNettAmountCredited As Decimal = 0

        Dim sSql, s1, sList As String
        Dim sTrancode As String
        Dim rowP1, row1 As DataRow
        Dim intPaymentId, intInvoice_id As Integer
        Dim sPayDescr, sPayAmount As String
        Dim sNettRecvd, sChangeGiven As String
        '= Dim dtDisbursed As DataTable
        Dim bIsAccountPayment As Boolean = False

        '- get payment header rec..
        rowP1 = mDataTablePayments.Rows(intPaymentRowNo)
        intPaymentId = rowP1.Item("payment_id")
        intInvoice_id = rowP1.Item("invoice_id")
        sTrancode = LCase(rowP1.Item("transactionType"))
        If (sTrancode = "account") Then
            bIsAccountPayment = True
        End If

        decChange = CDec(rowP1.Item("changeGiven"))
        '=s1 = FormatCurrency(decChange, 2)
        sChangeGiven = RSet(FormatCurrency(decChange, 2), 12)
        '=3403.1031=
        decCreditNoteRedeemed = CDec(rowP1.Item("creditNoteAmountDebited"))
        decCreditNoteSaved = CDec(rowP1.Item("creditNotePaymentCredited"))
        decNettAmountCredited = CDec(rowP1.Item("nettAmountCredited"))

        '-- account only-
        sNettRecvd = FormatCurrency(rowP1.Item("nettAmountCredited"), 2)
        If Not bIsAccountPayment Then
            sNettRecvd = FormatCurrency(decNettAmountCredited + decCreditNoteRedeemed)
        End If

        '-- get payment details.-
        listPaymentDetail.Items.Clear()
        decTotalPayments = 0
        If (decChange > 0) Then
            listPaymentDetail.Items.Add(LSet("(Change Given:", 24) & "|" & sChangeGiven & ")")
            listPaymentDetail.Items.Add("")
        End If

        '=4201.1007=
        decTotalDiscountOnPayment = rowP1.Item("discountGivenOnPayment")
        If (decTotalDiscountOnPayment > 0) Then
            s1 = RSet(FormatCurrency(decTotalDiscountOnPayment, 2), 12)
            listPaymentDetail.Items.Add(LSet("(Discount On Payment:", 24) & "|" & s1 & ")")
        End If

        '=3403.1031-  Show Credit note eedeemed.=
        If decCreditNoteRedeemed > 0 Then
            decTotalPayments += decCreditNoteRedeemed
            s1 = RSet(FormatCurrency(decCreditNoteRedeemed, 2), 12)
            listPaymentDetail.Items.Add(LSet("(Credit Note Redeemed:", 24) & "|" & s1 & ")")
        End If  '-credit note-

        sSql = "SELECT * FROM dbo.PaymentDetails "
        sSql &= "  WHERE (PaymentDetails.payment_id= " & CStr(intPaymentId) & ");"
        '- get  record set.-
        If Not gbGetDataTable(mCnnSql, mDataTablePaymentDetails, sSql) Then
            MsgBox("Error in getting recordset for PaymentDetails table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '==Exit Function '--msg was displayed..
        Else
            If Not (mDataTablePaymentDetails Is Nothing) Then
                For Each row1 In mDataTablePaymentDetails.Rows
                    s1 = CStr(row1.Item("paymentType_descr"))
                    sPayDescr = LSet(s1, 24)
                    decPayAmount = row1.Item("amount")
                    decTotalPayments += decPayAmount
                    sPayAmount = RSet(FormatCurrency(decPayAmount, 2), 12)
                    listPaymentDetail.Items.Add(sPayDescr & "|" & sPayAmount)
                Next row1
            End If  '-nothing-
        End If
        '--bottom box-
        labPaymentTotal.Text = "Total Payment:  " & sNettRecvd
        If (decCreditNoteSaved > 0) Then
            s1 = FormatCurrency(decCreditNoteSaved, 2)
            labPaymentTotal.Text &= vbCrLf & "Saved as Credit Note::  " & s1
        End If

        '--  Show invoice disbursements for this payment..
        sSql = "SELECT * FROM dbo.PaymentDisbursements "
        sSql &= "  WHERE (payment_id= " & CStr(intPaymentId) & ");"
        '- get  record set.-
        If Not gbGetDataTable(mCnnSql, mDataTableDisbursements, sSql) Then
            MsgBox("Error in getting recordset for PaymentDisbursements table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '==Exit Function '--msg was displayed..
        Else
            sList = ""
            If Not (mDataTableDisbursements Is Nothing) Then
                For Each row1 In mDataTableDisbursements.Rows
                    sList &= Format(row1.Item("invoice_id"), "  000") & ": " & _
                               FormatCurrency(row1.Item("amount"), 2) & "; "
                Next
            End If
            If bIsAccountPayment Then
                labInvoiceList.Text = "Payment applies to Invoices: " & vbCrLf & sList
            Else
                '-- cash sale- 
                labInvoiceList.Text = "Payment for Invoice No: " & vbCrLf & _
                                 "#" & intInvoice_id & ":" & FormatCurrency(decNettAmountCredited + decCreditNoteRedeemed)
            End If
        End If  '--get-
    End Function  '-show-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '- l o a d --

    Private Sub frmShowPayment_Load(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles MyBase.Load
        Dim colSystemInfo As Collection
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim s1, sName As String

        msAppPath = My.Application.Info.DirectoryPath
        If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
        msAppFullname = msAppPath & My.Application.Info.AssemblyName
        If (VB.Right(LCase(msAppFullname), 4) <> ".exe") Then
            msAppFullname &= ".exe"
        End If
        msCurrentUserName = gsGetCurrentUser()

        '==-test-
        '== MsgBox("msAppFullname is :  " & msAppFullname, MsgBoxStyle.Information)
        msInvoiceFilePath = gsGetPDF_file_path()

        '=3403.1106=
        'If (mIntCallerStaff_id <= 0) Then
        '    MsgBox("Error- No Staff Id provided..", MsgBoxStyle.Exclamation)
        '    Me.Hide()  '=Me.Close()
        '    Exit Sub
        'End If

        '==
        '==   Target-New-Build-4282 --  (22-October-2020)
        '==   Target-New-Build-4282 --  (22-October-2020)
        '==
        '==     -Fix Terminal_id missing in REFUND for  clsCshSaleReversal, clsAccountReversal, 
        '==                     and frmShowPayment (for Payment Reversal.).
        '==
        msComputerName = My.Computer.Name
        '== END Target-New-Build-4282 --  (22-October-2020)




        labCallerStaff.Text = msCallerStaffName

        '= btnPrintInvoice.Enabled = False
        btnPrintReceipt.Enabled = False
        '==  Call CenterForm(Me)
        grpBoxPayments.Text = ""
        '-reversals-
        labHdrReversal.Visible = False
        panelReverse.Visible = False
        Me.Text = "Payment Details"

        '= labJobNo.Text = ""
        '-- get system Info table data.-
        '=3301.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '=3301.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        '=3301.428= End If  '-load sys info--

        '=3411=-- temp for receipt-
        msEmailTextReceipt = vbCrLf & "&&subject" & vbCrLf & "&&greeting" & vbCrLf & _
                    "Please find attached your Receipt as per above." & vbCrLf & _
                    "Thank You." & vbCrLf & "&&BusinessName"


        '=3403.1031- Server Share Path for Email Queue.
        msEmailQueueSharePath = mSysInfo1.item("POS_EMAILQUEUE_SHAREPATH")

        '==3301.428= Local Settings-
        msSettingsPath = gsLocalSettingsPath() '= default Jobmatix33=
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        '- get printers collection and set up combos.
        '== cboInvoicePrinters.Items.Clear()
        cboReceiptPrinters.Items.Clear()
        msPdfPrinterName = ""

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                '== cboInvoicePrinters.Items.Add(sName)
                cboReceiptPrinters.Items.Add(sName)
                ''=3403.1031=
                'If (InStr(LCase(sName), "adobe pdf") > 0) Or _
                ' ((InStr(LCase(sName), "microsoft") > 0) And (InStr(LCase(sName), "pdf") > 0)) Then
                '    msPdfPrinterName = sName  '-save PDF printer name--
                'End If
            Next sName

            '-- check local settings (prefs) for printers..
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
            '-override from caller-
            If (msSelectedReceiptPrinterName <> "") Then
                If colPrinters.Contains(msSelectedReceiptPrinterName) Then '--set it- 
                    cboReceiptPrinters.SelectedItem = msSelectedReceiptPrinterName
                End If
            End If  '--user selected-

        End If '-getAvail.- 

        '==
        '==  3411.0113=  13-Jan-2018=
        '==          --  Get PDF preferred printer...
        '==             -(Microsoft PDF will be preferred)..
        '==
        '---(Microsoft will be preferred)..

        If Not gsGetPdfPrinterName(msPdfPrinterName) Then
            MsgBox("Error- Failed to look-up pdf printer..", MsgBoxStyle.Exclamation)
        End If  '-get-

        labPdfPrinter.Text = msPdfPrinterName


        '- position us on top of calling form..
        If mFrmParent Is Nothing Then
            Call CenterForm(Me)
        Else
            Me.Left = mFrmParent.Left + 16
            Me.Top = mFrmParent.Top + 50
        End If
        labVersion.Text = msVersionPOS

        '==3403.1031=  
        '--  DO setup stuff here so we can capture PDF..
        '--    and if no printing then exit with out showing the form..

        Dim sSql As String
        Dim ix, intStaff_id As Integer
        Dim row1 As DataRow
        '=Dim item1 As ListViewItem
        '= Dim sPayDescr As String
        Dim decPayAmount, decTotalPayments As Decimal

        If mIntPayment_id <= 0 Then
            MsgBox("Show Receipt- No Payment No.", MsgBoxStyle.Exclamation)
            Me.Close()
            Exit Sub
        End If
        labHdr1.Text = "Details for Payment No: " & CStr(mIntPayment_id)
        labHdrReversal.Text &= CStr(mIntPayment_id) & " ?"
        '-- get payments.  --
        '-  SHOULD only be one.-
        '== listPayments.Items.Clear()
        decTotalPayments = 0
        sSql = "SELECT *, staff.barcode AS staff_barcode, customer.barcode AS customer_barcode, "
        sSql &= " customer.email as customer_email, staff.docket_name "
        sSql &= " FROM dbo.Payments "
        sSql &= "   JOIN Customer on (Customer.customer_id =payments.customer_id) "
        sSql &= "   JOIN staff on (staff.staff_id =payments.staff_id) "
        sSql &= "  WHERE (Payments.payment_id=" & CStr(mIntPayment_id) & ")"
        '- get  record set.-
        If Not gbGetDataTable(mCnnSql, mDataTablePayments, sSql) Then
            MsgBox("Error in getting recordset for Payments table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Me.Close()
            Exit Sub '--msg was displayed..
        Else
            '-- build list of Payments.
            If Not (mDataTablePayments Is Nothing) AndAlso (mDataTablePayments.Rows.Count > 0) Then
                '==For ix = 0 To (mDataTablePayments.Rows.Count - 1)
                row1 = mDataTablePayments.Rows(0)
                '== listPayments.Items.Add(CStr(ix + 1) & ": " & s1)
                '== Next  '-ix-
            Else  '-payment not found.-
                MsgBox("Payment no: " & mIntPayment_id & " not found ! ", MsgBoxStyle.Exclamation)
                Me.Close()
                Exit Sub '--msg was displayed..
            End If
        End If  '--get table-
        '==If listPayments.Items.Count > 0 Then
        '==listPayments.SelectedIndex = 0
        '==End If
        DoEvents()

        mIntCustomer_id = row1.Item("customer_id")
        '-- Show payment main details==
        labPaymentDate.Text = Format(row1.Item("payment_date"), "dd-MMM-yyyy HH:mm")
        '=3501.1030- Get Till..
        labShowTill.Text = "Till- " & row1.Item("cashDrawer")

        mbIsReversal = IIf((row1.Item("IsReversal") = 0), False, True)
        '- originalPayment_id-

        intStaff_id = row1.Item("Staff_id")
        mIntOriginalStaff_id = intStaff_id
        msOriginalStaffName = row1.Item("docket_name")
        labRcvdStaff.Text = row1.Item("docket_name")

        msCustBarcode = row1.Item("customer_barcode")
        labCustName.Text = "Customer [" & msCustBarcode & "]:"
        msCustomerEmail = row1.Item("customer_email")

        txtCustName.Text = row1.Item("firstname") & " " & row1.Item("lastname") & vbCrLf & _
                     row1.Item("companyName")
        msCustomerName = txtCustName.Text

        txtCustName.Text &= vbCrLf & "Account No: " & row1.Item("customer_barcode")
        txtComments.Text = row1.Item("comments")
        If mbIsReversal Then
            mIntOriginalPayment_id = row1.Item("originalPayment_id")
            txtComments.Text = "-- R E V E R S A L of Payment # " & mIntOriginalPayment_id & _
                                                                        vbCrLf & txtComments.Text
        End If

        '== NB-  All this had to go tp Activated-
        '--   So its not minimised for msgboxes..

    End Sub '--load-
    '= = = = = = = = = = = =
    '-===FF->

    '--Activated --

    Private Sub frmShowPayment_Activated(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub '-- do once only..--
        mbActivated = True

    End Sub '--Activated --
    '= = = = = = = = = == =

    '-- S h o w n ..-

    Private Sub frmShowPayment_Shown(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles MyBase.Shown
        '-get supplied Payment no..  
        '-- retrieve Payment details.
        '= If mbActivated Then Exit Sub '-- do once only..--
        '= mbActivated = True

        If (mIntCallerStaff_id <= 0) Then
            MsgBox("Error- No Staff Id provided..", MsgBoxStyle.Exclamation)
            Me.Close()
            Exit Sub
        End If

        'labHdr1.Text = "Details for Payment No: " & CStr(mIntPayment_id)

        '==        Call mbShowPaymentDetails(0)  '-should only be one row..
        Dim row1 As DataRow = mDataTablePayments.Rows(0)

        '-- All this came back here so the popups are not minimised..
        '-- All this came back here so the popups are not minimised..
        '-- All this came back here so the popups are not minimised..

        '=3411.0113= Jan-2018=
        '- -- show payment details..  ready for printing..
        Call mbShowPaymentDetails(0)  '-should only be one row..

        '-- capture PDF if email needed..
        If mbCaptureReceiptPDF And (msPdfPrinterName <> "") Then
            Dim sPrinterName, sTitle, sPrintFileFullName As String
            Dim prtDocs1 As clsPrintSaleDocs
            Dim colReceiptLines As Collection
            Dim rowPayment As DataRow
            Dim sSubject, sEmailText, sReceipteDate As String

            sReceipteDate = Format(row1.Item("payment_date"), "dd-MMM-yyyy")

            sPrinterName = msPdfPrinterName
            sTitle = "PaymntReceipt-" & Trim(CStr(mIntPayment_id)) & "_" & "Cust-" & Trim(msCustBarcode) & ".pdf"
            sPrintFileFullName = msInvoiceFilePath & "\" & sTitle

            '-- set registry key for Adobe pdf writer..
            '=3411.0109= Check if Microsoft PDF or Adobe.
            If (InStr(LCase(msPdfPrinterName), "adobe") > 0) Then
                '-- set registry key for Adobe pdf writer..
                If Not gbSetAdobeFileName(sPrintFileFullName, msAppFullname) Then
                    MsgBox("Failed to set Adobe File Name.", MsgBoxStyle.Exclamation)
                    '= mbSetAdobeFileName(sPrintFileFullName) Then
                    Exit Sub
                End If
            Else '=Microsoft or other.  use PrintToFile setting.-
                '-ok=
            End If  '-adobe-
            '-- delete old file if exists.
            If My.Computer.FileSystem.FileExists(sPrintFileFullName) Then
                Try
                    My.Computer.FileSystem.DeleteFile(sPrintFileFullName)
                Catch ex As Exception
                    MsgBox("Failed to delete old file: " & sPrintFileFullName & vbCrLf & ex.Message)
                End Try
            End If

            '-- set registry key for Adobe pdf writer..
            'If Not gbSetAdobeFileName(sPrintFileFullName, msAppFullname) Then
            '    MsgBox("Failed to set Adobe File Name.", MsgBoxStyle.Exclamation)
            'Else  '-ok-
            prtDocs1 = New clsPrintSaleDocs
            prtDocs1.PrtSelectedPrinterName = sPrinterName
            prtDocs1.SystemInfo = mSysInfo1 '= mSdSystemInfo
            prtDocs1.UserLogo = mImageUserLogo

            colReceiptLines = New Collection
            rowPayment = mDataTablePayments.Rows(0)
            If Not mBuildReceipt(rowPayment, colReceiptLines) Then
                MsgBox("Error- Failed to build receipt lines collection..", MsgBoxStyle.Exclamation)
                Me.Close()
                Exit Sub
            End If
            '- finish.. make print file..
            prtDocs1.versionPOS = msVersionPOS
            prtDocs1.UserLogo = mImageUserLogo
            '= prtDocs1.PrtSelectedPrinterName = strPrinterName
            '- this will  be blank if not pdf..
            prtDocs1.PrintToFileFullPath = sPrintFileFullName

            '-- go print--
            If Not prtDocs1.PrintDocket(colReceiptLines, True) Then  '- wait for completion.-
                MsgBox("Print Failed..", MsgBoxStyle.Exclamation)
                '= Me.Close()
                '= Exit Sub
            End If
            Me.BringToFront()
            If Not My.Computer.FileSystem.FileExists(sPrintFileFullName) Then
                MsgBox("Error- Print file was not created..", MsgBoxStyle.Exclamation)
                Me.Close()
                Exit Sub
            End If

            '-temp- =3403.1031= GONE=
            '    MsgBox("Testing- the File: " & vbCrLf & strFileFullPath & vbCrLf & _
            '        "Will be stored at: " & vbCrLf & strEmailQueueSharePath, MsgBoxStyle.Information)

            '-- move PDF file to Server..
            sSubject = "Receipt- Payment No:" & mIntPayment_id & "  Dated :" & sReceipteDate

            sEmailText = msEmailTextReceipt '=3411.0127=

            sEmailText = Replace(sEmailText, "&&subject", "Re:" & sSubject, , , CompareMethod.Text)
            sEmailText = Replace(sEmailText, "&&greeting", "Dear " & msCustomerName, , , CompareMethod.Text)
            sEmailText = Replace(sEmailText, "&&BusinessName", msBusinessName, , , CompareMethod.Text)
            If Not gbSaveDocumentToEmailQueue(mCnnSql, sPrintFileFullName, sTitle, "PDF", _
                                             "RECEIPT", mIntCustomer_id, -1, mIntPayment_id, _
                                             sSubject, msCustomerName, _
                                              msCustomerEmail, _
                                              sEmailText, msEmailQueueSharePath) Then
                MsgBox("Save PDF file to Server has failed..", MsgBoxStyle.Exclamation)
            Else  '-  ók=
                MsgBox("Pls Note- The Invoice PDF file: " & vbCrLf & sPrintFileFullName & vbCrLf & _
                      " has been saved and queued for emailing.", MsgBoxStyle.Information)
            End If  '-save-
            '=End If  '-set name-
        End If  '--pdf-

        If mbPaymentReversalRequested Then
            labHdrReversal.Visible = True
            panelReverse.Visible = True
        End If

        If Not mbCanPrintReceipt Then
            Me.Close()
            Exit Sub
        End If

        btnPrintReceipt.Enabled = True

    End Sub '--SHOWN.. Was Activated --
    '= = = = = = = = = =  =  = = = = =

    Private Sub cboReceiptPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                     ByVal e As System.EventArgs) _
                                                        Handles cboReceiptPrinters.SelectedIndexChanged
        '= Dim sName As String
        If (cboReceiptPrinters.SelectedIndex >= 0) Then
            msReceiptPrinterName = cboReceiptPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_receiptPrtSettingKey, msReceiptPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, msReceiptPrinterName) Then
                MsgBox("Failed to save Receipt printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '-ReceiptPrinters-
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '--btnPrintReceipt-
    '--btnPrintReceipt-

    Private Sub btnPrintReceipt_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles btnPrintReceipt.Click

        Dim colReceiptLines As Collection
        Dim prtDocs1 As New clsPrintSaleDocs
        '= Dim sABN, s1 As String
        '=Dim row1 As DataRow
        Dim rowPayment As DataRow
        'Dim bIsAccountCust As Boolean = False  '-- TEMP --

        'Dim sPayDescr As String
        'Dim sNettRecvd, sChangeGiven As String
        ''== Dim decSubTotalInc, decTotalInc As Decimal
        'Dim decPayAmount, decTotalPayments As Decimal

        If msReceiptPrinterName = "" Then
            MsgBox("No Receipt printer selected..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        prtDocs1.PrtSelectedPrinterName = msReceiptPrinterName
        prtDocs1.SystemInfo = mSysInfo1 '= mSdSystemInfo
        prtDocs1.UserLogo = mImageUserLogo

        colReceiptLines = New Collection

        rowPayment = mDataTablePayments.Rows(0)

        If Not mBuildReceipt(rowPayment, colReceiptLines) Then
            MsgBox("Error- Failed to build receipt lines collection..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        '- finish..
        prtDocs1.versionPOS = msVersionPOS
        prtDocs1.UserLogo = mImageUserLogo
        '= prtDocs1.PrtSelectedPrinterName = strPrinterName

        '-- go print--
        If Not prtDocs1.PrintDocket(colReceiptLines) Then
            MsgBox("Print Failed..", MsgBoxStyle.Exclamation)
        End If


    End Sub  '--btnPrintReceipt-
    '= = = = = = = = = = = = === =
    '-===FF->

    '-- btnReverseNow--
    '-- btnReverseNow--

    Private Sub btnReverseNow_Click(sender As Object, e As EventArgs) Handles btnReverseNow.Click

        If (MsgBox("Are you sure you want to reverse this payment ?" & vbCrLf & _
                     "  NB: A Reversal can't be undone..", _
                 MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes) Then

            Exit Sub
        End If

        '- ok. make the reversal--
        '-- We just need to copy out all the original Payment details..
        '--   with the isReversal flag set..
        btnReverseNow.Enabled = False

        Dim sqlTransaction1 As OleDbTransaction
        Dim rowOriginal As DataRow

        Dim sSql, sFieldList, sValues, sComments As String
        Dim v2 As Object
        '= Dim bIsCredit As Boolean = (LCase(msTransactionType) = "refund")
        '== NB AccountPayments, the Payments Table has -1 in primary InvoiceNo=
        '=    See Disbursements table for related invoices for the Payment...
        Dim intInvoice_id As Integer = -1
        Dim intOriginal_id, intRevPayment_Id, intID As Integer
        Dim datarow1 As DataRow
        Dim sPayAmount, sDescription, sKey As String
        Dim decAmount, decInvoiceTotal, decTotalTax As Decimal

        mCnnSql.ChangeDatabase(msSqlDbName) '--USE our db..-

        If mDataTablePayments.Rows.Count > 0 Then
            rowOriginal = mDataTablePayments.Rows(0)
        Else
            MsgBox("Error !  No Payment Row found to reverse..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        intOriginal_id = rowOriginal.Item("payment_id")
        intInvoice_id = rowOriginal.Item("invoice_id")
        sComments = "Reversing Original Payment No: " & intOriginal_id & ".."

        '-- copy out all the original Payment details..
        Dim decPaymentTotalRcvd As Decimal = rowOriginal.Item("totalAmountReceived")
        Dim decTotalDiscount As Decimal = rowOriginal.Item("discountGivenOnPayment")
        Dim decChangeAsCash As Decimal = rowOriginal.Item("changeGiven")
        Dim decPaymentNettCredited As Decimal = rowOriginal.Item("nettAmountCredited")
        Dim decChangeAsCredit As Decimal = rowOriginal.Item("creditNotePaymentCredited")
        Dim decCreditNoteCreditApplying As Decimal = rowOriginal.Item("creditNoteAmountDebited")

        '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
        sqlTransaction1 = mCnnSql.BeginTransaction

        '= labHelp.Text = "Invoice #" & intInvoice_id & ":  Saving Payment record.."
        sSql = "INSERT INTO dbo.payments ("
        sSql &= "  staff_id, customer_id, invoice_id, "
        sSql &= " transactionType, isReversal, originalPayment_id, totalAmountReceived, "
        sSql &= " discountGivenOnPayment, changeGiven, nettAmountCredited, "
        sSql &= " creditNotePaymentCredited, creditNoteAmountDebited, "
        sSql &= "    terminal_id, cashDrawer, currentWindowsUserName, comments "
        sSql &= ") "
        sSql &= "VALUES ( "
        sSql &= CStr(mIntCallerStaff_id) & ", " & CStr(mIntCustomer_id) & ", " & CStr(intInvoice_id) & ", "
        sSql &= "'Account', 1, " & CStr(intOriginal_id) & ", " & CStr(decPaymentTotalRcvd) & ", "
        sSql &= CStr(decTotalDiscount) & ", "
        sSql &= CStr(decChangeAsCash) & ", " & CStr(decPaymentNettCredited) & ", "
        '=3403.1017- Debtor can have Credit Note..
        sSql &= CStr(decChangeAsCredit) & ", " & CStr(decCreditNoteCreditApplying) & ", "
        sSql &= "'" & msComputerName & "', "
        sSql &= "'" & gsGetCurrentCashDrawer() & "', '" & gsFixSqlStr(msCurrentUserName) & "', "
        sSql &= "'AccountPayment: " & gsFixSqlStr(txtComments.Text) & "'"
        sSql &= "); "
        '-- Save-
        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
            '= labHelp.Text = "Saving Payment Record FAILED.."
            Exit Sub
        End If  '--exec invoice-

        '- 2. Retrieve Payment No. (IDENTITY of Reversed Payment record written.)-
        sSql = "SELECT CAST(IDENT_CURRENT ('dbo.payments') AS int);"
        If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
            intRevPayment_Id = intID
            '-- update invoice display later..-
        Else
            MsgBox("Failed to retrieve Reversal Payment No..", MsgBoxStyle.Exclamation)
            Exit Sub
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
                    Exit Sub
                End If  '--exec INSERT pay detail LINE-
            Next datarow1
        End If  '-details- nothing.-
        '- 4. INSERT Payment-Disbursement Child Table Rows for all Invoices.-
        '--       covered in this payment....
        If Not (mDataTableDisbursements Is Nothing) Then
            Dim intDisbInvoice_id As Integer
            Dim sTrancode As String
            For Each datarow1 In mDataTableDisbursements.Rows
                'sList &= Format(row1.Item("invoice_id"), "  000") & ": " & _
                '           FormatCurrency(row1.Item("amount"), 2) & "; "
                intDisbInvoice_id = datarow1.Item("invoice_id")
                sTrancode = datarow1.Item("tranCode")
                decAmount = datarow1.Item("amount")
                '-- sav disb. detail line-
                sSql = "INSERT INTO dbo.paymentDisbursements ("
                sSql &= "  payment_id, invoice_id, tranCode, sourceOfFunds,  "
                sSql &= "  amount )"
                sSql &= "  VALUES (" & CStr(intRevPayment_Id) & ", " & CStr(intDisbInvoice_id) & ", "
                sSql &= "'" & sTrancode & "', '" & gsFixSqlStr(sTrancode) & "', "
                sSql &= CStr(decAmount)
                sSql &= "); "
                '-- insert this row..-
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    '=  labHelp.Text = "Insert Pay-disbursement Failed.."
                    Exit Sub
                End If  '--exec pay disb. LINE-
            Next datarow1
        End If  '-disb. nothing-.

        '- that's all..
        '- 5. -- Commit TRANSACTION.---
        Try
            sqlTransaction1.Commit()
            MsgBox("REVERSAL Transaction committed ok.." & vbCrLf & _
                      "   Payment No: " & intRevPayment_Id, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Transaction commit FAILED.. " & intInvoice_id, MsgBoxStyle.Exclamation)
        End Try
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    End Sub '- btnReverseNow-
    '= = = = = = = = = = = = = == =  =

End Class  '- frmShowPayment-
'= = = = = = =  = = = = = = =

'== end form=