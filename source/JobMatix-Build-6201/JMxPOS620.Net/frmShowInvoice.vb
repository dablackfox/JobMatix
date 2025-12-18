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
Imports System.Threading


Public Class frmShowInvoice

    '-- POS-- Show/print Sale Invoice..
    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==
    '==  grh JobMatixPOS 3.1.3101.1102 -
    '==       >> Show serial nos.
    '==       >> 3101.1104- Find printers from inside here,
    '==             and provide combos to choose..
    '==
    '==  grh. JobMatix 3.1.3101.1221 ---  21-Dec-2014 ===
    '==   >>  FrmParent is input for positioning... 
    '==   >> And fix receipt for paymnt amt alignment-
    '==
    '==  grh. JobMatix 3.1.3107.727 ---  27-Jul-2015 ===
    '==   >>  Clean up Receipt formatting.... 
    '==
    '==  grh. JobMatix 3.1.3107.820 ---  20-Aug-2015 ===
    '==   >>  If Sale just created, Print PDF and capture File... 
    '==   >> 826-  Export SetAdobeFileName to modPrintSubs..
    '==
    '==  grh. JobMatix 3.1.3107.831 ---  31-Aug-2015 ===
    '==   >>  If Sale just created, Print Anyway if Requested from COMMIT.... 
    '==
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==
    '== = = = =
    '==
    '==     v3.3.3301.622/623..  22-June-2016= ===
    '==       >> Update Invoice and PAYMENTS Table Schema-  
    '==     BACK to the ORIGINAL schema-  No IP Invoice records, and PaymentDisbursement records Re-stored.
    '==         (For Account customers, part payment with sale will record a separate Payment entry)
    '==
    '==     v3.3.3301.817..  17-August-2016= ===
    '==       >> frmShowInvoice- Add msgBox to advise Sent to printer...
    '== = = = = =
    '==
    '==     v3.3.3307.0216..  16-Feb-2017= ===
    '==       >> Account refund..  Fix labPayments message.
    '==
    '==     v3.4.3401.319-..  19-Mar-2017= ===
    '==       >> If caller has selected printer, then make it overide the current..
    '==       >>  3401.416-  Fix receipt to add Cust-barcode.
    '==       >> Added cancel (ESC) button..  (btnExit)..
    '==
    '==     v3.4.3403.507-..  07-May-2017= ===
    '==       >> Updated to handle Layby's.. (and Invoices, Quotes and Refunds !!)
    '==       >> 3403.513-  layby labels..
    '==
    '==     3403.1009- 09-Oct-2017-
    '==      -- POS Emails now to use Server File-System to store Invoice PDF's for Email..
    '==            at \\[server]\users\public\JobMatixPOS-EmailQueue\ 
    '==                    (SystemInfo setting is :  "POS_EMAILQUEUE_SHAREPATH"
    '==               NB: (Table "DocArchive" to be DROPPED..)
    '==      --  XML Descriptor file to go with each PDF for Email sending info. 
    '==
    '==     3411.0109=
    '==         --  get Microsoft PDF Printer to print To File (for emailing PDF)..
    '==         --  Show PDF Printer if any....
    '==
    '== -- Updated 3501.1024  24/29/30-Oct-2018=  
    '==     -- Show Till id (cashDrawer) on ShowInvoice/showPayment Forms... 
    '==
    '==  IN PRODUCTION- 07-Feb-2019--
    '==  IN PRODUCTION- 07-Feb-2019--
    '==
    '==   Updated.- 3519.0207 07-Feb-2019= 
    '==     -- Fixes to Show Invoice LOAD event- (No Cash Drawer column for Quotes.)-
    '==
    '==
    '==   Updated.- 3519.0211 11-Feb-2019= 
    '==     -- Fixes to print "Tax Invoice" on Receipt.-
    '==     -- Fix Crash in printing Quote..
    '==
    '==   Updated.- 3519.0221  Started 21-Feb-2019= 
    '==     -- Fixes to Various modules and forms to allow optional A4 Invoice printing on NON-account Sale... 
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==    -- Customer Admin-  Include Refunds in customer Info Tab..
    '==         ALSO- frmShowInvoice:  Show Refund amounts destinations.. (Refund as cash, CreditNore etc.)
    '==         ALSO- Ditto for printing Invoice  (clsSaleprintDocs.)
    '==    
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1117.  06-Nov-2019-  Started 06-November-2019-
    '==      -- On "frmShowInvoice"-  IF isOnAccount then 
    '==                Show "Charged to Account" in Payments Footer.
    '= = = = =  = = = = = =
    '==
    '==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
    '==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
    '==    
    '==   == 4221.0205.  05-Feb-2020- 
    '==     --  Emailing Quotes....  (frmShowInvoice.)
    '==            Email heading/text need to say "Quote"  (NOT "Invoice")...
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = ==
    '==
    '==
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
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==   2. OTHER MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
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
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = ==
    '==
    '==
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==
    '==   New BUTTON and Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
    '==         Done by cloning clsAccountReversal and moddying and adding Payment Reversal stuff..
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '== 
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = =

    Private Const k_invoicePrtSettingKey As String = "POS_InvoicePrinter"
    Private Const k_receiptPrtSettingKey As String = "POS_ReceiptPrinter"

    Private mFrmParent As Form
    Private mbActivated As Boolean = False
    '- - - - -
    Private mbIsInitialising As Boolean = False
    Private mbActive As Boolean = False
    Private mbStartingUp As Boolean
    Private msVersionPOS As String = ""

    '=  Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""
    '= Private mbIsSqlAdmin As Boolean = False

    Private msComputerName As String '--local machine--
    Private msAppPath As String
    '== Private msLastSqlErrorMessage As String = ""
    Private msAppFullname As String = ""
    Private msInvoiceFilePath As String = ""

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--
    Private mCnnSql As OleDbConnection '--
    Private mlJobId As Integer = -1
    Private mbCaptureInvoicePDF As Boolean = False
    Private mbPrintInvoiceAnyway As Boolean = False

    Private mbA4InvoiceWasRequested As Boolean = False

    Private mIntCustomer_id As Integer = -1

    Private msCallerStaffName As String = ""
    Private msBusinessABN As String = ""
    Private msBusinessName As String = ""
    Private msEmailTextInvoice As String = "Invoice Attached"  '--default value-
    Private msEmailQueueSharePath As String = ""

    Private msInvoicePrinterName As String = ""
    Private msReceiptPrinterName As String = ""
    '=3403.513=
    Private msLabelPrinterName As String = ""

    Private msDefaultPrinterName As String = ""
    Private msPdfPrinterName As String = ""

    '== Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private mImageUserLogo As Image

    '- Current INVOICE DETAILS --

    Private mIntInvoice_id As Integer = -1
    '== 4221.0205.
    '= For Quote, ID is actually copied from mIntInvoice_id..
    Private mIntSalesOrder_id As Integer = -1

    Private msTranCode As String = ""
    Private mbIsRefund As Boolean = False

    Private mbIsQuote As Boolean = False
    Private mbIsLayby As Boolean = False

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1
    Private mIntCallerStaff_id As Integer = -1

    Private mDataTableInvoice As DataTable
    Private mDataTableSaleItems As DataTable
    Private mDataTablePayments As DataTable
    Private mDataTablePaymentDetails As DataTable
    Private mDataTableDisbursements As DataTable

    Private mbIsAccountCust As Boolean
    Private mbIsOnAccount As Boolean = False

    Private mDecAmountDebitedToAccount As Decimal = 0

    Private mDecTotalPayments, mDecChange, mDecTotalContribution As Decimal
    Private mDecCreditNoteAmountCredited As Decimal = 0
    Private mDecCreditNoteAmountDebited As Decimal = 0

    '=3519.0317=
    Private mDecRefundedAsCash As Decimal = 0
    Private mDecRefundedAsCreditNote As Decimal = 0
    Private mDecRefundedAsEftPosDr As Decimal = 0
    Private mDecRefundedAsEftPosCr As Decimal = 0


    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
    Private mDecRefundedAsOther As Decimal = 0
    Private mStrRefundOtherDetailkey As String = ""
    '-also-
    Private mColRefundReceiptInfo As Collection

    Private mDateOriginalInvoice_date As DateTime '= = mDataTableInvoice.Rows(0).Item("invoice_date")

    '== END of  Target is new Build 4251..



    Private msCustBarcode As String = ""
    Private msCustomerEmail As String = ""

    '=3311.226=
    '==  Input from Commit Confirmation form..
    Dim msSelectedInvoicePrinterName As String = ""
    Dim msSelectedReceiptPrinterName As String = ""

    '== Private clsWinSpec1 As New clsWinSpecial   '--3107.820 for app data pqth-
    '= = = = = = = = = = = = = = = = = = =

    '- for quotes-
    Private sIdColumn As String
    Private sDateColumn As String

    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--properties as input parameters--
    '--properties as input parameters--
    WriteOnly Property FrmParent() As Form
        Set(ByVal value As Form)
            mFrmParent = value
        End Set
    End Property  '= parent form=
    '= = = = =  = = = = = = = ==  == 

    '-version-
    '==  WriteOnly Property versionPOS() As String
    '==     Set(ByVal value As String)
    '==         msVersionPOS = value
    '==     End Set
    '== End Property  '--version--
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
    '= = = = = = = =  = = = = = = = = = =

    WriteOnly Property connectionSql() As OleDbConnection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =

    WriteOnly Property InvoiceNo() As Integer
        Set(ByVal value As Integer)
            mIntInvoice_id = value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = 

    WriteOnly Property isQuote() As Boolean
        Set(ByVal value As Boolean)
            mbIsQuote = value
        End Set
    End Property
    '= = = = = = = = = = = = = == = = =

    WriteOnly Property islayby() As Boolean
        Set(ByVal value As Boolean)
            mbIsLayby = value
        End Set
    End Property
    '= = = = = = = = = = = = = == = = =

    '-- capture pdf when Sale id first done..

    WriteOnly Property CaptureInvoicePDF As Boolean
        Set(value As Boolean)
            mbCaptureInvoicePDF = value
        End Set
    End Property '-pdf-
    '= = = = = = = = = = = = = =  = = = == =

    '-- Print Anyway- req. when Sale is first done..

    WriteOnly Property PrintInvoiceAnyway As Boolean
        Set(value As Boolean)
            mbPrintInvoiceAnyway = value
        End Set
    End Property '-pdf-
    '= = = = = = = = = = = = = =  = = = == =

    '-- Print Anyway- req. when Sale is first done..

    WriteOnly Property A4InvoiceRequested As Boolean
        Set(value As Boolean)
            mbA4InvoicewasRequested = value
        End Set
    End Property '-pdf-
    '= = = = = = = = = = = = = =  = = = == =

    '--  printers..--
    '--  printers..--

    '=3311.226=
    '==  Input from Commit Confirmation form..
    WriteOnly Property selectedInvoicePrinterName() As String
        Set(ByVal Value As String)
            msSelectedInvoicePrinterName = Value
        End Set
    End Property '--colour.--
    '= = = = = = = =  = = = 

    WriteOnly Property selectedReceiptPrinterName() As String
        Set(ByVal Value As String)
            msSelectedReceiptPrinterName = Value
        End Set
    End Property '--receipt.--
    '= = = = = = = =  = = =

    '== WriteOnly Property LabelPrinterName() As String
    '==     Set(ByVal Value As String)
    '==         msLabelPrinterName = Value
    '==     End Set
    '== End Property '--label.--
    '= = = = = = = =  = = =

    '-- set user logo for printing..--
    WriteOnly Property UserLogo() As Image
        Set(ByVal Value As Image)
            mImageUserLogo = Value
        End Set
    End Property '--logo..--
    '= = = = = = = = = = = = = = = =

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

    '-- load Adobe registry PDF FileName key-

    Private Function mbSetAdobeFileName(ByVal sFullFilePath As String) As Boolean

        mbSetAdobeFileName = gbSetAdobeFileName(sFullFilePath, msAppFullname)
        Exit Function
        '==========all done==

        'mbSetAdobeFileName = False
        'Try
        '    My.Computer.Registry.CurrentUser.CreateSubKey("Software\Adobe\Acrobat Distiller\PrinterJobControl")
        '    '- WE are 32-bit app--
        '    If gbIsWow64() Then  '-we are on 64-bit os.--
        '        '-- for wow 64-  splwow64.exe
        '        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Adobe\Acrobat Distiller\PrinterJobControl", _
        '                                         "c:\windows\splwow64.exe", sFullFilePath)
        '    Else  '--32 bit OS-
        '        My.Computer.Registry.SetValue("HKEY_CURRENT_USER\Software\Adobe\Acrobat Distiller\PrinterJobControl", _
        '                                        msAppFullname, sFullFilePath)
        '    End If  '-wow64-
        '    mbSetAdobeFileName = True
        'Catch ex As Exception
        '    MsgBox("ERROR in setting Adobe registry value." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        'End Try

    End Function '-adobe-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-  Print or capture the invoice printout--

    Private Function mbPrintInvoice(Optional ByVal bCapturePDF As Boolean = False, _
                                   Optional ByVal sSelectedInvoicePrinterName As String = "") As Boolean
        Dim row1 As DataRow
        Dim printSaleDocs1 As New clsPrintSaleDocs
        Dim sTerms As String = mSysInfo1.item("POS_ACCOUNTTERMS") '=mSdSystemInfo.Item("POS_ACCOUNTTERMS")
        Dim sPrinterName As String
        Dim sTitle As String
        Dim sPrintFileFullName As String = ""
        Dim sCustomerName, sSubject, sInvoiceDate As String
        Dim sEmailText As String = msEmailTextInvoice

        mbPrintInvoice = False
        '- get invoice info-
        If (Not (mDataTableInvoice Is Nothing)) AndAlso (mDataTableInvoice.Rows.Count > 0) Then
            row1 = mDataTableInvoice.Rows(0)
            sCustomerName = row1.Item("firstname") & " " & row1.Item("lastname")
            If Trim(sCustomerName) = "" Then
                sCustomerName = row1.Item("companyName")
                If Trim(sCustomerName) = "" Then
                    sCustomerName = "Valued Customer " & msCustBarcode & "."
                End If
            End If
            If mbIsQuote Then
                sInvoiceDate = Format(CDate(row1.Item("salesorder_date")), "dd-MMM-yyyy")
                '-4221.0305=
                '-- Replace "Invoice" with "Quote" in email text.
                Dim iPos As Integer = InStr(LCase(sEmailText), "invoice")
                If iPos > 0 Then
                    sEmailText = VB.Left(sEmailText, iPos - 1) & "Quote" & sEmailText.Substring(iPos + 6)
                End If
            Else
                sInvoiceDate = Format(CDate(row1.Item("invoice_date")), "dd-MMM-yyyy")
            End If
        Else
            MsgBox("Nothing to print.", MsgBoxStyle.Exclamation)
            Exit Function
        End If  '-nothing-

        If bCapturePDF Then  '- From activated startup-
            sPrinterName = msPdfPrinterName
            sTitle = "Invoice-" & Trim(CStr(mIntInvoice_id)) & "_" & "Cust-" & Trim(msCustBarcode) & ".pdf"
            If mbIsQuote Then
                sTitle = "Quote-" & Trim(CStr(mIntInvoice_id)) & "_" & "Cust-" & Trim(msCustBarcode) & ".pdf"
            End If
            sPrintFileFullName = msInvoiceFilePath & "\" & sTitle
            '-- set registry key for Adobe pdf writer..
            '=3411.0109= Check if Microsoft PDF or Adobe.
            If (InStr(LCase(msPdfPrinterName), "adobe") > 0) Then
                If Not mbSetAdobeFileName(sPrintFileFullName) Then
                    Exit Function
                End If
            Else '=Microsoft or other.  use PrintToFile setting.-

            End If
            '-- delete old file if exists.
            If My.Computer.FileSystem.FileExists(sPrintFileFullName) Then
                Try
                    My.Computer.FileSystem.DeleteFile(sPrintFileFullName)
                Catch ex As Exception
                    MsgBox("Failed to delete old file: " & sPrintFileFullName & vbCrLf & ex.Message)
                End Try
            End If

        Else  '-print request-
            If (msInvoicePrinterName = "") Then
                MsgBox("No Invoice printer selected..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            sPrinterName = msInvoicePrinterName
            '=3311.226=
            If (sSelectedInvoicePrinterName <> "") Then
                sPrinterName = sSelectedInvoicePrinterName
            End If
        End If
        printSaleDocs1.PrtSelectedPrinterName = sPrinterName

        printSaleDocs1.SystemInfo = mSysInfo1  '= mSdSystemInfo
        printSaleDocs1.UserLogo = mImageUserLogo

        printSaleDocs1.DataTableInvoice = mDataTableInvoice
        printSaleDocs1.DataTableSaleItems = mDataTableSaleItems
        If Not mbIsQuote Then
            printSaleDocs1.DataTablePayments = mDataTablePayments
            printSaleDocs1.DataTablePaymentDetails = mDataTablePaymentDetails
        Else
            printSaleDocs1.isQuote = True
        End If

        printSaleDocs1.versionPOS = msVersionPOS
        If (sTerms <> "") Then
            printSaleDocs1.TermsText = sTerms
        Else
            printSaleDocs1.TermsText = "All accounts to be paid within 30 days.." & vbCrLf & "Cheers."
        End If
        '- this will  be blank if not pdf..
        printSaleDocs1.PrintToFileFullPath = sPrintFileFullName

        '=3411.0208=  -If bCapturePDF then WAIT for completion.
        If Not printSaleDocs1.PrintSalesInvoice(bCapturePDF) Then
            '-- coulbe adobe funny fonts error.
            Me.BringToFront()
            MsgBox("Print may have Failed..")
        Else  '- was ok.
        End If
        Me.BringToFront()
        '== MsgBox("Print S tarted..", MsgBoxStyle.Information)
        '== 3107.820--
        '-  Wait for endPrint Event..
        '=3411.0110- NO..
        'labStatus.Text = "printing.."
        'Application.UseWaitCursor = True
        'While Not printSaleDocs1.PrintingCompleted
        '    Thread.Sleep(100)
        'End While
        'labStatus.Text = ""
        Application.UseWaitCursor = False

        If bCapturePDF Then  '- From activated startup-
            '=  MsgBox("Note The print file: " & vbCrLf & sPrintFileFullName & vbCrLf & _
            '=           " will be saved and queued for emailing. ", MsgBoxStyle.Information)
            '=3411.0110=- wait for File to appear..
            Dim intStart, intFinish As Integer
            intStart = CInt(VB.Timer)
            intFinish = intStart + 20
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            While Not My.Computer.FileSystem.FileExists(sPrintFileFullName) And (CInt(VB.Timer) < intFinish)
                DoEvents()
                Thread.Sleep(500)  '--milliseconds..-
            End While
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            If Not My.Computer.FileSystem.FileExists(sPrintFileFullName) Then
                MsgBox("Error- Print file was not created..", MsgBoxStyle.Exclamation)
                Exit Function
            End If
            '-- save PDF ON SERVER SHARE--   NOT in database-
            'If Not gbSaveDocumentToDB(mCnnSql, sPrintFileFullName, sTitle, "Invoice No. " & mIntInvoice_id, "PDF", _
            '                             "INVOICE", mIntCustomer_id, -1, "Sale invoice..", msCustomerEmail, _
            '                                  msEmailTextInvoice) Then
            '-- Fixed- 4221.0205-
            sSubject = IIf(mbIsQuote, "Quotation- Quote No: ", "Sale- invoice No:") & _
                                                        mIntInvoice_id & "  Dated :" & sInvoiceDate
            sEmailText = Replace(sEmailText, "&&subject", "Re:" & sSubject, , , CompareMethod.Text)
            sEmailText = Replace(sEmailText, "&&greeting", "Dear " & sCustomerName, , , CompareMethod.Text)
            sEmailText = Replace(sEmailText, "&&BusinessName", msBusinessName, , , CompareMethod.Text)
            If Not gbSaveDocumentToEmailQueue(mCnnSql, sPrintFileFullName, sTitle, "PDF", _
                                             "INVOICE", mIntCustomer_id, -1, mIntInvoice_id, _
                                             sSubject, sCustomerName, _
                                              msCustomerEmail, _
                                              sEmailText, msEmailQueueSharePath) Then
                MsgBox("Save PDF file to Server Queue has failed..", MsgBoxStyle.Exclamation)
            Else  '-  ók=
                MsgBox("Pls Note- The Invoice PDF file: " & vbCrLf & sPrintFileFullName & vbCrLf & _
                      vbCrLf & " has been created OK, and queued for emailing.", MsgBoxStyle.Information)
            End If  '-save-
        Else  '=3301.817= -printed-
            If mbIsQuote Then
                MsgBox("Ok. Quote was sent to Printer: '" & sPrinterName & "'..", MsgBoxStyle.Information)
            Else
                MsgBox("Ok. Invoice was sent to Printer: '" & sPrinterName & "'..", MsgBoxStyle.Information)
            End If

        End If  '-capture-
        mbPrintInvoice = True
        '=  End If  '-print-
    End Function  '-print-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- show payment details..

    Private Function mbShowPaymentDetails(ByVal intPaymentRowNo As Integer) As Boolean
        Dim sSql, s1, sList As String
        Dim rowP1, row1 As DataRow
        Dim intPaymentId As Integer
        Dim sPayDescr, sPayAmount As String
        Dim sNettRecvd, sChangeGiven As String
        Dim dtDisbursed As DataTable
        Dim decPayAmount As Decimal

        '- get payment header rec..
        rowP1 = mDataTablePayments.Rows(intPaymentRowNo)
        intPaymentId = rowP1.Item("payment_id")
        sNettRecvd = FormatCurrency(rowP1.Item("nettAmountCredited"), 2)
        mDecChange = CDec(rowP1.Item("changeGiven"))
        '==s1 = FormatCurrency(decChange, 2)
        sChangeGiven = RSet(FormatCurrency(mDecChange, 2), 12)
        '-3401.330= refundAsCreditNoteCredited-
        mDecCreditNoteAmountCredited = CDec(rowP1.Item("creditNotePaymentCredited")) + _
                                                  CDec(rowP1.Item("refundAsCreditNoteCredited"))
        mDecCreditNoteAmountDebited = CDec(rowP1.Item("creditNoteAmountDebited"))

        '-- get payment details.-
        mDecAmountDebitedToAccount = CDec(rowP1.Item("amountDebitedToAccount"))
        listPaymentDetail.Items.Clear()
        mDecTotalPayments = 0
        labPaymentTotal.Text = ""

        '=3519.0317=
        mDecRefundedAsCash = 0
        mDecRefundedAsCreditNote = 0
        mDecRefundedAsEftPosDr = 0
        mDecRefundedAsEftPosCr = 0


        '==  Target is new Build 4251..
        '==  Target is new Build 4251..
        '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
        mDecRefundedAsOther = 0
        mStrRefundOtherDetailkey = ""
        '-also-
        mColRefundReceiptInfo = New Collection

        Dim sTag As String
        '== END of  Target is new Build 4251..


        '==   Target-New-Build-4282 -- (Started 12-October-2020)
        '==   Target-New-Build-4282 -- (Started 12-October-2020)
        '==   New BUTTON and Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
        Dim bIsPaymentReversal As Boolean = (rowP1.Item("isReversal") <> 0)

        If (Not mbIsRefund) Or (mbIsRefund And bIsPaymentReversal) Then '= New-Build-4282 (Not mbIsRefund) Then
            If bIsPaymentReversal Then
                listPaymentDetail.Items.Add("Reversed Details:")
                If (mDecCreditNoteAmountCredited <> 0) Then
                    sPayAmount = RSet(FormatCurrency(mDecCreditNoteAmountCredited, 2), 12)
                    listPaymentDetail.Items.Add(LSet("Rev.CreditNote Credited:", 24) & "|" & sPayAmount)
                End If
                If (mDecCreditNoteAmountDebited <> 0) Then
                    sPayAmount = RSet(FormatCurrency(mDecCreditNoteAmountDebited, 2), 12)
                    listPaymentDetail.Items.Add(LSet("Rev.CreditNote Debited:", 24) & "|" & sPayAmount)
                End If
            End If  '-reversal-
            '== END  Target-New-Build-4282 -- (Started 12-October-2020)


            sSql = "SELECT * FROM dbo.PaymentDetails "
            sSql &= "  WHERE (PaymentDetails.payment_id= " & CStr(intPaymentId) & ");"
            '- get  record set.-
            If Not gbGetDataTable(mCnnSql, mDataTablePaymentDetails, sSql) Then
                MsgBox("Error in getting recordset for PaymentDetails table: " & vbCrLf &
                                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '==Exit Function '--msg was displayed..
            Else
                If Not (mDataTablePaymentDetails Is Nothing) Then
                    For Each row1 In mDataTablePaymentDetails.Rows
                        s1 = CStr(row1.Item("paymentType_descr"))
                        sPayDescr = LSet(s1, 24)
                        decPayAmount = row1.Item("amount")
                        mDecTotalPayments += decPayAmount
                        sPayAmount = RSet(FormatCurrency(decPayAmount, 2), 12)
                        listPaymentDetail.Items.Add(sPayDescr & "|" & sPayAmount)
                    Next row1
                End If  '-nothing-
            End If  '-get-
            If mbIsAccountCust And mbIsOnAccount Then
                'If mbIsRefund Then
                '    '= labPaymentTotal.Text = "Refund Credited To Account: " & _
                '    '=                           FormatCurrency(mDecAmountDebitedToAccount, 2) & vbCrLf
                'Else  '-sale-
                labPaymentTotal.Text = "Invoice Debited To Account: " &
                                          FormatCurrency(mDecAmountDebitedToAccount, 2) & vbCrLf
                'End If
            End If  '- on Account-
            listPaymentDetail.Items.Add(LSet("Change Given:", 24) & "|" & sChangeGiven)
        End If  '-not refund.


        '==3301.627--  SHOW CreditNote Amounts if any.-
        '--  This can come from Refund or Sale with extra payment.
        '--   Mutually exclusive with-CreditNote Amount Debited.
        '= creditNoteAmountCredited MONEY NOT NULL DEFAULT 0,"
        '--  This amount was spent in paying for the SALE..-
        '=  creditNoteAmountDebited MONEY NOT NULL DEFAULT 0,"

        '=3519.0317=  Add stuff to show where Refund went..
        '=3519.0317=  Add stuff to show where Refund went..
        '=3519.0317=  Add stuff to show where Refund went..

        '==   Target-New-Build-4282 -- (Started 12-October-2020)
        '==   Target-New-Build-4282 -- (Started 12-October-2020)

        If (mbIsRefund And bIsPaymentReversal) Then
            ''-- Showed Reversed Details ABOVE....

            '== END  Target-New-Build-4282 -- (Started 12-October-2020)

        ElseIf mbIsRefund Then
            labPaymentsListHdr.Text = "Where the Refund amount went-"
            mDecRefundedAsCash = CDec(rowP1.Item("RefundCashAmount"))
            mDecRefundedAsCreditNote = CDec(rowP1.Item("RefundAsCreditNoteCredited"))
            mDecRefundedAsEftPosDr = CDec(rowP1.Item("RefundAsEftPosDr"))
            mDecRefundedAsEftPosCr = CDec(rowP1.Item("RefundAsEftPosCr"))

            '==  Target is new Build 4251..
            '==  Target is new Build 4251..
            '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
            mDecRefundedAsOther = CDec(rowP1.Item("RefundOtherDetailAmount"))
            mStrRefundOtherDetailkey = (rowP1.Item("RefundOtherDetailKey"))
            '== END of  Target is new Build 4251..


            '- show details..
            'If (mDecRefundedAsCash > 0) Then
            '==  Target is new Build 4251..
            s1 = RSet(FormatCurrency(mDecRefundedAsCash, 2), 12)
            '= listPaymentDetail.Items.Add(LSet("Refunded as Cash:", 24) & "|" & s1)
            sTag = LSet("Refunded as Cash:", 24)
            listPaymentDetail.Items.Add(sTag & "|" & s1)
            If (mDecRefundedAsCash > 0) Then
                mColRefundReceiptInfo.Add(LSet("Cash", 16) & "|" & s1)
            End If

            'End If '-cash-
            '- cr-note-
            '= s1 = RSet(FormatCurrency(mDecRefundedAsCreditNote, 2), 12)
            '= listPaymentDetail.Items.Add(LSet("Refunded as CreditNote:", 24) & "|" & s1)
            '- cr-note-
            '==  Target is new Build 4251..
            s1 = RSet(FormatCurrency(mDecRefundedAsCreditNote, 2), 12)
            sTag = LSet("Refunded as CreditNote:", 24)
            listPaymentDetail.Items.Add(sTag & "|" & s1)
            If (mDecRefundedAsCreditNote > 0) Then
                mColRefundReceiptInfo.Add(LSet("CreditNote:", 16) & "|" & s1)
            End If


            '-Eftpos
            '= s1 = RSet(FormatCurrency(mDecRefundedAsEftPosDr, 2), 12)
            '= listPaymentDetail.Items.Add(LSet("Refunded as EftPos Dr:", 24) & "|" & s1)
            '-Eftpos-dr
            '==  Target is new Build 4251..
            s1 = RSet(FormatCurrency(mDecRefundedAsEftPosDr, 2), 12)
            sTag = LSet("Refunded as EftPos Dr:", 24)
            listPaymentDetail.Items.Add(sTag & "|" & s1)
            If (mDecRefundedAsEftPosDr > 0) Then
                mColRefundReceiptInfo.Add(LSet("EftPos Dr", 16) & "|" & s1)
            End If


            '= s1 = RSet(FormatCurrency(mDecRefundedAsEftPosCr, 2), 12)
            '= listPaymentDetail.Items.Add(LSet("Refunded as EftPos Cr:", 24) & "|" & s1)
            '-Eftpos-cr
            '==  Target is new Build 4251..
            s1 = RSet(FormatCurrency(mDecRefundedAsEftPosCr, 2), 12)
            sTag = LSet("Refunded as EftPos Cr:", 24)
            listPaymentDetail.Items.Add(sTag & "|" & s1)
            If (mDecRefundedAsEftPosCr > 0) Then
                mColRefundReceiptInfo.Add(LSet("EftPos Cr", 16) & "|" & s1)
            End If


            '==  Target is new Build 4251..
            '==  Target is new Build 4251..
            '==   MAIN THEME HERE is implementation of EXTENDED Refund Details for REFUNDS..
            If (mDecRefundedAsOther > 0) And (mStrRefundOtherDetailkey <> "") Then
                s1 = RSet(FormatCurrency(mDecRefundedAsOther, 2), 12)
                sTag = LSet("Refunded as " & mStrRefundOtherDetailkey & ":", 24)
                listPaymentDetail.Items.Add(sTag & "|" & s1)
                mColRefundReceiptInfo.Add(LSet(mStrRefundOtherDetailkey, 16) & "|" & s1)
            End If
            '== END of  Target is new Build 4251..


        Else  '-not refund-
            If (mDecCreditNoteAmountDebited <> 0) Then
                s1 = FormatCurrency(mDecCreditNoteAmountDebited, 2)
                s1 = RSet(s1, 12)
                listPaymentDetail.Items.Add(LSet("Redeemed from CreditNote:", 24) & "|" & s1)
            End If
            mDecTotalContribution = mDecTotalPayments - mDecChange + mDecCreditNoteAmountDebited

            listPaymentDetail.Items.Add(LSet("Total:", 24) & "|" & RSet(FormatCurrency(mDecTotalContribution, 2), 12))

            labPaymentTotal.Text &= "Nett New Payment Rcvd:  " & sNettRecvd
            If (mDecCreditNoteAmountCredited <> 0) Then
                s1 = FormatCurrency(mDecCreditNoteAmountCredited, 2)
                s1 = RSet(s1, 12)
                labPaymentTotal.Text &= vbCrLf & LSet("Saved as CreditNote:", 24) & "|" & s1
            End If
        End If  '-refund.

        '=== '--  Show invoice disbursements for this payment..
        '=== sSql = "SELECT * FROM dbo.PaymentDisbursements "
        '=== sSql &= "  WHERE (payment_id= " & CStr(intPaymentId) & ");"
        '=== '- get  record set.-
        '=== If Not gbGetDataTable(mCnnSql, dtDisbursed, sSql) Then
        '===      MsgBox("Error in getting recordset for PaymentDisbursements table: " & vbCrLf & _
        '===                                gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        '=== Else
        '===   sList = ""
        '===   If Not (dtDisbursed Is Nothing) Then
        '===     For Each row1 In dtDisbursed.Rows
        '===       sList &= Format(row1.Item("invoice_id"), "  000") & ": " & _
        '===            FormatCurrency(row1.Item("amount"), 2) & "; "
        '===     Next
        '===   End If
        '===   labPaymentTotal.Text = "Payment applies to Invoices: " & vbCrLf & _
        '===                           sList & vbCrLf & labPaymentTotal.Text
        '=== End If  '--get-
    End Function  '-show-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '- l o a d --

    Private Sub frmShowInvoice_Load(ByVal sender As System.Object, _
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
        '==-test-
        '== MsgBox("msAppFullname is :  " & msAppFullname, MsgBoxStyle.Information)

        '= s1 = gsJobMatixLocalDataDir()  '== clsWinSpec1.AppDataDir
        '= msInvoiceFilePath = s1 & "\AllDocuments"
        '= If Not My.Computer.FileSystem.DirectoryExists(msInvoiceFilePath) Then  '-must create..-
        '= My.Computer.FileSystem.CreateDirectory(msInvoiceFilePath)
        '= End If '-- exists statement dir.-
        msInvoiceFilePath = gsGetPDF_file_path()
        '== msInvoiceFilePath &= "\"
        Me.Text = "Invoice Details"

        btnPrintInvoice.Enabled = False
        btnPrintReceipt.Enabled = False
        Call CenterForm(Me)
        labStatus.Text = ""
        picEmailInvoice.Enabled = False
        '=4219.1118=
        labPaymentTotal.Text = ""


        labJobNo.Text = ""
        '-- get system Info table data.-
        '=3301.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '=3301.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        msEmailTextInvoice = mSysInfo1.item("POS_EMAILTEXTINVOICE")
        '=3301.428= End If  '-load sys info--
        '=3403.1009- Server Share Path for Email Queue.
        msEmailQueueSharePath = mSysInfo1.item("POS_EMAILQUEUE_SHAREPATH")


        '- get printers collection and set up combos.
        cboInvoicePrinters.Items.Clear()
        cboReceiptPrinters.Items.Clear()
        msPdfPrinterName = ""
        '==3301.428= Local Settings-
        '=3300.428= Call mbLoadSettings()
        msSettingsPath = gsLocalSettingsPath() '= default Jobmatix33=
        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboInvoicePrinters.Items.Add(sName)
                cboReceiptPrinters.Items.Add(sName)
                '-see below for pdf printer.=
                'If (InStr(LCase(sName), "adobe pdf") > 0) Or _
                '                 ((InStr(LCase(sName), "microsoft") > 0) And (InStr(LCase(sName), "pdf") > 0)) Then
                '    msPdfPrinterName = sName  '-save PDF printer name--
                'End If
            Next sName

            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_invoicePrtSettingKey) AndAlso _
                     (mLocalSettings1.item(k_invoicePrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_invoicePrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboInvoicePrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then
                        cboInvoicePrinters.SelectedItem = msDefaultPrinterName
                    End If
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then
                    cboInvoicePrinters.SelectedItem = msDefaultPrinterName
                End If
            End If  '-query- 
            '-override from caller-
            If (msSelectedInvoicePrinterName <> "") Then
                If colPrinters.Contains(msSelectedInvoicePrinterName) Then '--set it- 
                    cboInvoicePrinters.SelectedItem = msSelectedInvoicePrinterName
                End If
            End If  '--user selected-
            '-receipt-
            If mLocalSettings1.exists(k_receiptPrtSettingKey) AndAlso _
                    (mLocalSettings1.item(k_receiptPrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_receiptPrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it-
                    cboReceiptPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then
                        cboReceiptPrinters.SelectedItem = msDefaultPrinterName
                    End If
                End If
            Else '-no pref-
                If (msDefaultPrinterName <> "") Then
                    cboReceiptPrinters.SelectedItem = msDefaultPrinterName
                End If
            End If  '-query receipt prt.--
            '-override from caller-
            If (msSelectedReceiptPrinterName <> "") Then
                If colPrinters.Contains(msSelectedReceiptPrinterName) Then '--set it- 
                    cboReceiptPrinters.SelectedItem = msSelectedReceiptPrinterName
                End If
            End If  '--user selected-
        End If '-getAvail.-  

        '=3411.0110=  Get PDF prefrred printer...
        '---(Microsoft will be preferred)..
        If Not gsGetPdfPrinterName(msPdfPrinterName) Then
            MsgBox("Error- Failed to look-up pdf printer..", MsgBoxStyle.Exclamation)
        End If  '-gety-
        labPdfPrinter.Text = msPdfPrinterName

        '=3311.226=  Override below. for auto print with input selections, if any==

        sIdColumn = "invoice_id"
        sDateColumn = "invoice_date"
        labRefundHdr.Visible = False

        If mbIsQuote Then
            labHdr1.Text = "Quotation # "
            labHdr1.ForeColor = Color.DarkOrange
            btnPrintReceipt.Enabled = False
            btnPrintInvoice.Text = "Print Quote"
            sIdColumn = "SalesOrder_id"
            sDateColumn = "SalesOrder_date"
            grpBoxPayments.Enabled = False
        ElseIf mbIsLayby Then
            labHdr1.Text = "Layby # "
            labHdr1.ForeColor = Color.DarkViolet
            btnPrintReceipt.Enabled = True
            btnPrintInvoice.Text = ""  '= "Print Quote"
            btnPrintInvoice.Enabled = False
            sIdColumn = "Layby_id"
            sDateColumn = "Layby_date_started"
            '=grpBoxPayments.Enabled = False
            'ElseIf mbIsRefund Then  '-not yet.
            '    labHdr1.Text = "(Refund) Invoice # "
        Else
            '-not quote.-
            labHdr1.Text = "Sales Invoice # "
        End If

        '- position us on top of calling form..
        If mFrmParent Is Nothing Then
            Call CenterForm(Me)
        Else
            Me.Left = mFrmParent.Left + 16
            Me.Top = mFrmParent.Top + 50
        End If

        Call gbGetUserLogo(mImageUserLogo)
        Call gbGetDllVersion(msVersionPOS)

        '=3311.226=
        '-- Move all activated stuff here 
        '--   so we can auto-print before showing form if needed...

        Dim sSql As String
        Dim ix, px As Integer
        Dim row1 As DataRow
        Dim item1 As ListViewItem
        Dim sPayDescr As String
        Dim decDiscount, decRounding As Decimal
        Dim decSubTotalTax, decDiscTax, decTotalTax As Decimal
        Dim decPayAmount, decTotalPayments As Decimal
        Dim sPayAmount As String
        '== Dim dtInvPayments As DataTable
        Dim dataTableAllPayments As DataTable

        If (mIntInvoice_id <= 0) Then
            MsgBox("No Invoice No.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        msBusinessName = mSysInfo1.item("BUSINESSNAME")

        decSubTotalTax = 0
        listViewSaleItems.Items.Clear()
        sSql = "SELECT *, Customer.barcode AS customer_barcode, customer.customer_id, "
        sSql &= "    customer.email as customer_email, staff.barcode AS staff_barcode, staff.docket_name "
        sSql &= "  FROM dbo.Invoice "
        sSql &= "   JOIN Customer on (Customer.customer_id =Invoice.customer_id) "
        sSql &= "   JOIN staff on (staff.staff_id =Invoice.staff_id) "
        sSql &= "    WHERE (invoice_id=" & CStr(mIntInvoice_id) & ");"
        If mbIsQuote Then
            '= sSql = "SELECT * "
            '== 4221.0205.
            '= For Quote, the Quote ID is actually copied from mIntInvoice_id..
            mIntSalesOrder_id = mIntInvoice_id
            '--
            sSql = "SELECT *, Customer.barcode AS customer_barcode, customer.customer_id, "
            sSql &= "    customer.email as customer_email, staff.barcode AS staff_barcode, staff.docket_name "
            sSql &= " FROM dbo.SalesOrder "
            sSql &= "   JOIN Customer on (Customer.customer_id =SalesOrder.customer_id) "
            sSql &= "   JOIN staff on (staff.staff_id =SalesOrder.staff_id) "
            sSql &= "    WHERE (SalesOrder_id=" & CStr(mIntInvoice_id) & ");"
        ElseIf mbIsLayby Then
            sSql = "SELECT *, Customer.barcode AS customer_barcode, customer.customer_id, "
            sSql &= "    customer.email as customer_email, staff.barcode AS staff_barcode, staff.docket_name "
            sSql &= " FROM dbo.layby "
            sSql &= "   JOIN Customer on (Customer.customer_id =Layby.customer_id) "
            sSql &= "   JOIN staff on (staff.staff_id =Layby.staff_id) "
            sSql &= "    WHERE (Layby_id=" & CStr(mIntInvoice_id) & ");"
        End If  '-quote-
        If Not gbGetDataTable(mCnnSql, mDataTableInvoice, sSql) Then
            MsgBox("Error in getting recordset for Invoice table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '==Exit Function '--msg was displayed..
            Me.Close()
            Exit Sub
        Else
            If Not (mDataTableInvoice Is Nothing) AndAlso (mDataTableInvoice.Rows.Count > 0) Then
                row1 = mDataTableInvoice.Rows(0)
                msTranCode = row1.Item("transactionType")
                If (LCase(msTranCode) = "refund") Then
                    labRefundHdr.Visible = True
                    mbIsRefund = True
                    labHdr1.Text = "(Refund) Invoice # "
                End If
                '=3501.1030- Get Till..
                '=3519.0207=\
                '--  -- It's not there for Quotes..
                '== labShowTill.Text = "Till- " & row1.Item("cashDrawer")
                labShowTill.Text = "Till- " & ""
                If (Not mbIsQuote) And (Not mbIsLayby) Then
                    labShowTill.Text = "Till- " & row1.Item("cashDrawer")
                End If

                msCustBarcode = row1.Item("customer_barcode")
                labCustNameLab.Text = "Customer  [" & msCustBarcode & "]"  '=3401.416=
                txtCustName.Text = row1.Item("firstname") & " " & row1.Item("lastname") & vbCrLf & _
                                   row1.Item("companyName")
                '= labInvoiceNo.Text = CStr(mIntInvoice_id)

                labHdr1.Text &= CStr(mIntInvoice_id)

                If mbIsQuote Or mbIsLayby Then
                    mbIsAccountCust = False
                    mbIsOnAccount = False
                Else
                    mbIsAccountCust = row1.Item("isAccountCust")
                    mbIsOnAccount = row1.Item("isOnAccount")
                End If
                mIntCustomer_id = row1.Item("customer_id")
                msCustomerEmail = row1.Item("customer_email")

                If Not (mbIsQuote Or mbIsLayby) Then
                    labInvoiceDate.Text = "Date: " & vbCrLf & Format(row1.Item("invoice_date"), "dd-MMM-yyyy")
                    '= txtSaleCashout.Text = FormatCurrency(row1.Item("cashout"), 2)
                ElseIf mbIsQuote Then
                    labInvoiceDate.Text = "Date: " & vbCrLf & Format(row1.Item("salesOrder_date"), "dd-MMM-yyyy")
                Else  '-layby-
                    labInvoiceDate.Text = "Date: " & vbCrLf & Format(row1.Item("Layby_date_started"), "dd-MMM-yyyy")
                End If  '-quote-
                labSaleStaff.Text = row1.Item("docket_name")

                txtSaleSubTotal.Text = FormatCurrency(row1.Item("subtotal_inc"), 2)
                decDiscount = CDec(row1.Item("discount_nett")) + CDec(row1.Item("discount_tax"))
                txtSaleDiscount.Text = FormatCurrency(decDiscount, 2)

                decDiscTax = CDec(row1.Item("discount_tax"))
                decTotalTax = CDec(row1.Item("total_tax"))
                txtDiscountTax.Text = FormatCurrency(decDiscTax, 2)
                txtTotalTax.Text = FormatCurrency(decTotalTax, 2)

                decRounding = CDec(row1.Item("rounding"))
                txtSaleRounding.Text = FormatCurrency(decRounding, 2)
                txtSaleTotal.Text = FormatCurrency(row1.Item("total_inc"), 2)

                txtComments.Text = row1.Item("comments")
                txtCustDeliveryAddress.Text = row1.Item("deliveryinstructions")
                If Not (mbIsQuote Or mbIsLayby) Then
                    '-amountDebitedToAccount-
                    '=3411.1118= GONE= mDecAmountDebitedToAccount = CDec(row1.Item("total_inc")) 
                    '=gone= CDec(row1.Item("AmountDebitedToAccount"))
                    If row1.Item("jobNumber") > 0 Then
                        labJobNo.Text = CStr(row1.Item("jobNumber"))
                    End If
                End If '-quote-
            End If  '-nothing-
        End If  '-get-
        '- end invoice hdr load..--

        '-- get details..-
        sSql = "SELECT * FROM dbo.InvoiceLine "
        sSql &= "  JOIN stock ON (stock.stock_id= invoiceLine.stock_id) "
        sSql &= "  WHERE (invoice_id=" & CStr(mIntInvoice_id) & ")"
        If mbIsQuote Then
            sSql = "SELECT * FROM dbo.SalesOrderLine "
            sSql &= "  JOIN stock ON (stock.stock_id= SalesOrderLine.stock_id) "
            sSql &= "  WHERE (SalesOrder_id=" & CStr(mIntInvoice_id) & ")"
        ElseIf mbIsLayby Then
            sSql = "SELECT * FROM dbo.LaybyLine "
            sSql &= "  JOIN stock ON (stock.stock_id= LaybyLine.stock_id) "
            sSql &= "  WHERE (Layby_id=" & CStr(mIntInvoice_id) & ")"
        End If
        sSql &= "   ORDER BY line_id;"
        If Not gbGetDataTable(mCnnSql, mDataTableSaleItems, sSql) Then
            MsgBox("Error in getting recordset for InvoiceLine table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '==Exit Function '--msg was displayed..
        Else
            If Not (mDataTableSaleItems Is Nothing) Then
                For Each row1 In mDataTableSaleItems.Rows
                    s1 = CStr(row1.Item("barcode"))
                    item1 = New ListViewItem(s1)
                    '=item1.SubItems.Add(Format(row1.Item("invoice_date"), "dd-MMM-yyyy"))
                    If Not mbIsQuote Then
                        item1.SubItems.Add(CStr(row1.Item("serialNumber")))
                    Else  '-quote-
                        item1.SubItems.Add("--")
                    End If
                    item1.SubItems.Add(CStr(row1.Item("description")))
                    item1.SubItems.Add(CStr(row1.Item("quantity")))
                    item1.SubItems.Add(FormatCurrency(row1.Item("sellActual_inc"), 2))
                    item1.SubItems.Add(FormatCurrency(row1.Item("total_inc"), 2))
                    listViewSaleItems.Items.Add(item1)
                    '-- track tax Sub total-
                    decSubTotalTax += row1.Item("total_inc") - row1.Item("total_ex")
                Next row1
            End If
        End If
        '- end invoice Lines load..--
        txtSubTotalTax.Text = FormatCurrency(decSubTotalTax, 2)

        '-- get initial payment stuff for THIS New Invoice--
        '-- get initial payment stuff for THIS New Invoice--
        If Not mbIsQuote Then
            '-- get SALE (not "Account") payments.  --

            '==   Target-New-Build-4282 -- (Started 12-October-2020)
            '==   Target-New-Build-4282 -- (Started 12-October-2020)
            '==
            '==   New BUTTON and Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
            '==         Done by cloning clsAccountReversal and moddying and adding Payment Reversal stuff..
            '=Build-4282= Dim sWhere2 As String = "((Payments.transactionType='sale') OR (Payments.transactionType='refund')) "
            Dim sWhere2 As String = "((Payments.transactionType='sale') OR " &
                                        "(Payments.transactionType='refund') OR (Payments.transactionType='CshSaleReversal')) "
            '== END Target-New-Build-4282 -- (Started 12-October-2020)


            If mbIsLayby Then
                sWhere2 = "(Payments.transactionType='layby')"
            End If
            listPayments.Items.Clear()
            decTotalPayments = 0
            sSql = "SELECT * FROM dbo.Payments "
            sSql &= "  WHERE (Payments.invoice_id=" & CStr(mIntInvoice_id) & ")"
            sSql &= "   AND  " & sWhere2
            '- get  record set.-
            If Not gbGetDataTable(mCnnSql, mDataTablePayments, sSql) Then
                MsgBox("Error in getting recordset for Payments table: " & vbCrLf & _
                                                gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Else  '-ok- Show main payment-
                '-- build list of Payments.
                If Not (mDataTablePayments Is Nothing) AndAlso (mDataTablePayments.Rows.Count > 0) Then
                    Call mbShowPaymentDetails(0)  '-show first (only) row.-
                ElseIf mbIsOnAccount Then
                    '= 4219.1117=
                    '==  Target is new Build 4251..
                    If mbIsRefund Then
                        labPaymentTotal.Text = vbCrLf & "Refunded to Customer Account."
                    Else
                        labPaymentTotal.Text = vbCrLf & "Transaction charged to Customer Account."
                    End If
                Else
                    labPaymentTotal.Text = "No Payment Found for this Transaction."
                End If
            End If  '-get-
        End If '-quote-
        '-In case we are re-displaying old invoice..-
        '-- get all ACCOUNT payments for this invoice..
        If Not mbIsQuote Then
            '-- get payments.  --
            listPayments.Items.Clear()
            decTotalPayments = 0

            '==   3301.616= Get payments info from dbo.invoice table..
            '-==           ("accountPayment" transaction type)..

            '==3301.723-  NO MORE
            '--  USE disbursements table..

            sSql = "SELECT * FROM dbo.paymentDisbursements AS PD"
            sSql &= " LEFT JOIN dbo.payments ON (PD.payment_id=payments.payment_id) "
            sSql &= "  WHERE (PD.invoice_id=" & CStr(mIntInvoice_id) & ")"
            '- get  record set.-
            If Not gbGetDataTable(mCnnSql, dataTableAllPayments, sSql) Then
                MsgBox("Error in getting recordset for Payments table: " & vbCrLf & _
                                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '==Exit Function '--msg was displayed..
            Else
                '-- build list of Payments.
                Dim sRev, sId, sAmount As String
                Dim decAmt As Decimal
                If Not (dataTableAllPayments Is Nothing) AndAlso (dataTableAllPayments.Rows.Count > 0) Then
                    For ix = 0 To (dataTableAllPayments.Rows.Count - 1)
                        row1 = dataTableAllPayments.Rows(ix)
                        s1 = Format(row1.Item("payment_date"), "dd-MMM-yyyy")
                        px = row1.Item("payment_id")

                        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
                        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
                        '--  Reformat to show Paymnt Reversals-

                        '= listPayments.Items.Add(CStr(px) & ": " & s1)
                        sId = RSet(CStr(row1.Item("payment_id")), 6)
                        sRev = IIf((row1.Item("isReversal") <> 0), " Rev", " +")
                        decAmt = CDec(row1.Item("Amount")) '- Amt disbursed to this Invoice.
                        sAmount = RSet(FormatNumber(decAmt, 2), 10)
                        listPayments.Items.Add(sId & ": " & s1 & sAmount & sRev)
                        '== END Target-New-Build-4259 -- (Started 17-Jul-2020)
                        '== END Target-New-Build-4259 -- (Started 17-Jul-2020)


                    Next  '-ix-
                End If
            End If  '--get table-

            '== If (listPayments.Items.Count > 0) Then
            '==  listPayments.SelectedIndex = 0
            '== End If
            listPayments.SelectedIndex = -1  '--force user to select.
            DoEvents()
        End If '-not quote=-

        '-- MOVE the rest to Activated, so popups are not minimised..

        ''= btnPrintInvoice.Enabled = True
        'If mbIsLayby Then
        '    btnPrintReceipt.Enabled = True
        '    btnPrintInvoice.Enabled = False
        '    If mbPrintInvoiceAnyway Then
        '        Call btnPrintReceipt_Click(New System.Object, New System.EventArgs)
        '        Me.Close()
        '    End If
        'ElseIf mbIsQuote Then
        '    btnPrintInvoice.Enabled = True
        'Else   '=If Not mbIsQuote Then  '-invoice-
        '    If mbIsOnAccount Then
        '        btnPrintInvoice.Enabled = True
        '    Else
        '        btnPrintReceipt.Enabled = True
        '    End If
        '    If mbCaptureInvoicePDF Then  '-  sale just completed-
        '        '-  print to PDF and Save file for email queue..
        '        If (msPdfPrinterName <> "") Then
        '            Call mbPrintInvoice(True)  '-capture pdf..
        '        End If
        '    End If  '- çapture -
        '    '- and print anyway if requested.
        '    If mbPrintInvoiceAnyway Then
        '        If mbIsOnAccount Then
        '            Call mbPrintInvoice(False, msSelectedInvoicePrinterName)  '-To "selected" printer..
        '        Else
        '            Call btnPrintReceipt_Click(New System.Object, New System.EventArgs)
        '        End If
        '        '=- print-anyway exits without showing form...
        '        Me.Close()
        '    End If  '-print anyway..
        '    If mbCaptureInvoicePDF Then  '-  sale just completed-  No showing the form..
        '        Me.Hide()
        '        Me.Close()
        '    End If
        'End If  '=quote-
    End Sub '--load-
    '= = = = = = = = = = = =
    '-===FF->

    '--Activated --
    Private Sub frmShowInvoice_Activated(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles MyBase.Activated
        If mbActivated Then Exit Sub '-- do once only..--
        mbActivated = True

    End Sub  '-activated-
    '= = = = = = = = = == =

    '-shown-

    Private Sub frmShowInvoice_Shown(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles MyBase.Shown

        '--  Invoice details on show.

        'If mbActivated Then Exit Sub '-- do once only..--
        'mbActivated = True

        '==   Target-New-Build-4282 -- (Started 12-October-2020)
        '==   Target-New-Build-4282 -- (Started 12-October-2020)
        '==
        '==   New BUTTON and Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
        '==         Done by cloning clsAccountReversal and moddying and adding Payment Reversal stuff..
        btnCashSaleReversal.Visible = False
        btnAccountReversal.Visible = False
        If (mbIsOnAccount And (LCase(msTranCode) = "sale")) Then
            btnAccountReversal.Visible = True
        ElseIf ((Not mbIsOnAccount) And (LCase(msTranCode) = "sale")) Then
            '-- Cash Sale.
            btnCashSaleReversal.Visible = True
            btnCashSaleReversal.Height = btnAccountReversal.Height
            btnCashSaleReversal.Top = btnAccountReversal.Top
        End If  '-onAccount-
        '== END Target-New-Build-4282 -- (Started 12-October-2020)


        '--all this move here so form is visible and popups not mionimised..
        btnPrintReceipt.Enabled = True
        btnPrintInvoice.Enabled = False

        '= btnPrintInvoice.Enabled = True
        If mbIsLayby Then
            If mbPrintInvoiceAnyway Then
                Call btnPrintReceipt_Click(New System.Object, New System.EventArgs)
                Thread.Sleep(2000)  '-stop form from ficking in and out.
                Me.Close()
                Exit Sub
            Else  'waiting for button.
                btnPrintReceipt.Enabled = True
                btnPrintInvoice.Enabled = False
            End If
        ElseIf mbIsQuote Then
            btnPrintInvoice.Enabled = True
        Else   '=If Not mbIsQuote Then  
            '-invoice-
            If mbCaptureInvoicePDF Then  '-  sale just completed-
                '-  print to PDF and Save file for email queue..
                If (msPdfPrinterName <> "") Then
                    Call mbPrintInvoice(True)  '-capture pdf..
                End If
            End If  '- çapture -
            '- and print anyway if requested.
            If mbPrintInvoiceAnyway Then
                If mbIsOnAccount Then
                    Call mbPrintInvoice(False, msSelectedInvoicePrinterName)  '-To "selected" printer..
                Else  '- not on account--
                    '-- will be a docket unless A4 requested..
                    If mbA4InvoiceWasRequested Then
                        Call mbPrintInvoice(False, msSelectedInvoicePrinterName)  '-To "selected" printer..
                    Else  '-default docket-
                        Call btnPrintReceipt_Click(New System.Object, New System.EventArgs)
                    End If
                End If
                '=- print-anyway exits without showing (holding) form...
                Thread.Sleep(2000)  '-stop form from flicking in and out.
                Me.Close()
                Exit Sub
            Else  '--needs button.
                If mbIsOnAccount Then
                    btnPrintInvoice.Enabled = True
                    btnPrintReceipt.Enabled = False
                Else
                    '= 3519.0221-  cash-sale can print A4 invoice.
                    btnPrintInvoice.Enabled = True
                    btnPrintReceipt.Enabled = True
                End If
            End If  '-print anyway..
            If mbCaptureInvoicePDF Then  '-  sale just completed-  Now is showing the form for popups....
                Me.Hide()
                Me.Close()
                Exit Sub
            End If
        End If  '=quote-

        If (msPdfPrinterName <> "") Then
            picEmailInvoice.Enabled = True
        End If

        '==
        '==  Target is new Build 4251..
        '==  Target is new Build 4251..
        '==
        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
        '==  To Fix- frmShowInvoice crashes when displaying a Quote.. 
        '==             See end of Event Shown, where "invoice_date" column is referenced without checking if Quote ot Invoice.
        '= NOT YET !! mDateOriginalInvoice_date = mDataTableInvoice.Rows(0).Item("invoice_date")
        '== END  Target-New-Build-4259 -- (Started 17-Jul-2020)

        btnAccountReversal.BackColor = Color.WhiteSmoke
        btnAccountReversal.Enabled = False
        If mbIsOnAccount And (LCase(msTranCode) = "sale") Then
            '- Make sure it's not already reversed..
            Dim sSql, sErrors As String
            Dim datatableTemp1 As DataTable
            Dim intRefund_id As Integer
            Dim dateInvoice_date As DateTime = mDataTableInvoice.Rows(0).Item("invoice_date")
            '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
            mDateOriginalInvoice_date = mDataTableInvoice.Rows(0).Item("invoice_date")
            '== END  Target-New-Build-4259 -- (Started 17-Jul-2020)


            sSql = " SELECT * FROM [dbo].[Invoice]"
            sSql &= " WHERE (invoice.transactionType = 'refund') "
            sSql &= "   AND (isOnAccount = 1) AND (invoice.original_id= " & CStr(mIntInvoice_id) & "); "
            If Not gbGetDataTable(mCnnSql, datatableTemp1, sSql) Then
                MsgBox("Failed Looking up AccountRefunds.. " & vbCrLf &
                          gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                '-- button stays disabled..
            Else '--ok-
                If (datatableTemp1.Rows.Count > 0) Then
                    '-- have a result-
                    Dim datarow1 As DataRow = datatableTemp1.Rows(0)
                    intRefund_id = datarow1.Item("invoice_id")
                    MsgBox("Note- " & vbCrLf &
                           "This Invoice was REVERSED on " &
                                 Format(CDate(datarow1.Item("invoice_date")), "dd-MMM-yyyy") & vbCrLf &
                                     "  Refund (Invoice) No is " & CStr(intRefund_id) & "..", MsgBoxStyle.Information)
                    '-- button stays disabled..
                Else
                    '- ok.. no matching refund found.

                    '==  Target is new Build 4251..
                    '--  DON'T check for payments here..  see "clsAccountReversals"..
                    'If (mDataTablePayments Is Nothing) OrElse (mDataTablePayments.Rows.Count <= 0) Then '= mDataTablePayments Then
                    '--  ok- no payments made on this.
                    '-- IF this is a newly created sale (invoice), then tone down the colour of the button.
                    If (DateDiff(DateInterval.Minute, mDateOriginalInvoice_date, Now) < 5) Then  '- less than 5 mins agoe.
                        btnAccountReversal.BackColor = Color.WhiteSmoke
                    Else '-not new-
                        btnAccountReversal.BackColor = Color.LavenderBlush
                    End If
                    btnAccountReversal.Enabled = True
                    'End If  '-payments.
                End If  '-rows count-
            End If  '-get-
        Else

            '==   Target-New-Build-4282 -- (Started 12-October-2020)
            '==   Target-New-Build-4282 -- (Started 12-October-2020)
            '==
            '==   New BUTTON and Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
            '==         Done by cloning clsAccountReversal and moddying and adding Payment Reversal stuff..
            If ((Not mbIsOnAccount) And (LCase(msTranCode) = "sale")) Then
                '-- cash sale--

                btnCashSaleReversal.BackColor = Color.WhiteSmoke
                btnCashSaleReversal.Enabled = False

                '- Make sure it's not already reversed..
                Dim sSql, sErrors As String
                Dim datatableTemp1 As DataTable
                Dim intRefund_id As Integer
                Dim dateInvoice_date As DateTime = mDataTableInvoice.Rows(0).Item("invoice_date")
                '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
                mDateOriginalInvoice_date = mDataTableInvoice.Rows(0).Item("invoice_date")
                '== END  Target-New-Build-4259 -- (Started 17-Jul-2020)


                sSql = " SELECT * FROM [dbo].[Invoice]"
                sSql &= " WHERE (invoice.transactionType = 'refund') "
                sSql &= "   AND (isOnAccount = 0) AND (invoice.original_id= " & CStr(mIntInvoice_id) & "); "
                If Not gbGetDataTable(mCnnSql, datatableTemp1, sSql) Then
                    MsgBox("Failed Looking up Cash Reversals.. " & vbCrLf &
                          gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    '-- button stays disabled..
                Else '--ok-
                    If (datatableTemp1.Rows.Count > 0) Then
                        '-- have a result-
                        Dim datarow1 As DataRow = datatableTemp1.Rows(0)
                        intRefund_id = datarow1.Item("invoice_id")
                        MsgBox("Note- " & vbCrLf &
                           "This Invoice was REVERSED on " &
                                 Format(CDate(datarow1.Item("invoice_date")), "dd-MMM-yyyy") & vbCrLf &
                                     "  Refund (Invoice) No is " & CStr(intRefund_id) & "..", MsgBoxStyle.Information)
                        '-- button stays disabled..
                    Else
                        '- ok.. no matching refund found.

                        '-- IF this is a newly created sale (invoice), then tone down the colour of the button.
                        If (DateDiff(DateInterval.Minute, mDateOriginalInvoice_date, Now) < 5) Then  '- less than 5 mins agoe.
                            btnCashSaleReversal.BackColor = Color.WhiteSmoke
                        Else '-not new-
                            btnCashSaleReversal.BackColor = Color.LightGoldenrodYellow
                        End If
                        btnCashSaleReversal.Enabled = True
                        'End If  '-payments.
                    End If  '-rows count-
                End If  '-get-
            End If  '-cash sale-
            '== END Target-New-Build-4282 -- (Started 12-October-2020) 


        End If '-account sale-
        '-- still more.
        '- NOT NEEDED - Target-New-Build-4282 -
        'If (LCase(msTranCode) = "refund") Then
        '    btnAccountReversal.Visible = False
        'End If
        '==  END Prep 4251..

        If btnPrintReceipt.Enabled Then
            btnPrintReceipt.Select()
        Else
            btnPrintInvoice.Select()
        End If

    End Sub '--activated-
    '= = = = = = = = = = = =
    '-===FF->

    '--catch printer combo selections..--
    '-- update settings.-

    Private Sub cboInvoicePrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboInvoicePrinters.SelectedIndexChanged
        '= Dim sName As String
        If (cboInvoicePrinters.SelectedIndex >= 0) Then
            msInvoicePrinterName = cboInvoicePrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_invoicePrtSettingKey, msInvoicePrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, msInvoicePrinterName) Then
                MsgBox("Failed to save invoice printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-
    End Sub '--  InvoicePrinters-
    '= = = = = = = = = = = = =  =

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

    '--  Select payment.-

    Private Sub listPayments_SelectedIndexChanged(ByVal sender As System.Object, _
                                                      ByVal e As System.EventArgs) _
                                                      Handles listPayments.SelectedIndexChanged
        Dim intPayment_id As Integer
        Dim iPos As Integer
        Dim frmShowPayment1 As frmShowPayment
        ' Get the currently selected item in the ListBox.
        Dim sCurItem As String = listPayments.SelectedItem.ToString()

        iPos = InStr(sCurItem, ":")
        If (iPos > 0) Then
            intPayment_id = CInt(Trim(VB.Left(sCurItem, (iPos - 1))))
            '= Call mbShowPaymentDetails(intPaymentRowNo)
            If (intPayment_id > 0) Then
                '-- call showPayment Form..--
                '    frmShowPayment1 = New frmShowPayment
                '    frmShowPayment1.connectionSql = mCnnSql
                '    frmShowPayment1.PaymentNo = intPayment_id
                '    '= frmShowPayment1.Settings = mSdSettings
                '    '== frmShowInvoice1.SystemInfo = mSdSystemInfo
                '    frmShowPayment1.UserLogo = mImageUserLogo
                '    frmShowPayment1.versionPOS = msVersionPOS
                '    frmShowPayment1.ShowDialog()
            End If  '-intPayment_id-
        End If  '-iPos-
    End Sub  '- listPayments_SelectedIndexChanged-
    '= = = = = = = = = = = =

    '-list- Dbl Click to select..

    Private Sub listPayments_DoubleClick(ByVal sender As System.Object, _
                                                      ByVal e As System.EventArgs) _
                                                      Handles listPayments.DoubleClick
        Dim intPayment_id As Integer
        Dim iPos As Integer
        Dim frmShowPayment1 As frmShowPayment
        ' Get the currently selected item in the ListBox.
        Dim sCurItem As String = listPayments.SelectedItem.ToString()

        iPos = InStr(sCurItem, ":")
        If (iPos > 0) Then
            intPayment_id = CInt(Trim(VB.Left(sCurItem, (iPos - 1))))
            '= Call mbShowPaymentDetails(intPaymentRowNo)
            If (intPayment_id > 0) Then
                '-- call showPayment Form..--
                frmShowPayment1 = New frmShowPayment
                frmShowPayment1.connectionSql = mCnnSql
                frmShowPayment1.PaymentNo = intPayment_id
                '= frmShowPayment1.Settings = mSdSettings
                '== frmShowInvoice1.SystemInfo = mSdSystemInfo
                frmShowPayment1.Staff_id = mIntCallerStaff_id
                frmShowPayment1.UserLogo = mImageUserLogo
                frmShowPayment1.versionPOS = msVersionPOS
                frmShowPayment1.ShowDialog()
            End If  '-intPayment_id-
        End If  '-iPos-
    End Sub '-list- Dbl Click to select..
    '= = = = = = = = = =  == = = = = = = 
    '-===FF->

    '--Print Invoice -
    '--Print Invoice -

    Private Sub btnPrintInvoice_Click(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles btnPrintInvoice.Click

        Call mbPrintInvoice()


    End Sub  '--btnPrintInvoice--
    '= = = = = = = = = = = = = = = 

    '--picEmailInvoice_Click-

    Private Sub picEmailInvoice_Click(sender As Object, e As EventArgs) Handles picEmailInvoice.Click

        Call mbPrintInvoice(True)  '-capture and email the pdf..-

    End Sub  '--picEmailInvoice_Click-
    '= = = = = = = = = = = =  = = = = =
    '-===FF->

    '- Print Receipt --
    '- Print Receipt --

    Private Sub btnPrintReceipt_Click(ByVal sender As System.Object, _
                                       ByVal e As System.EventArgs) Handles btnPrintReceipt.Click
        Dim colReceiptLines As Collection
        Dim prtDocs1 As New clsPrintSaleDocs
        Dim sABN, s1, s2, sTranType, sCompany, sCustName, sLabelCust, sBarcode As String
        Dim row1 As DataRow
        Dim rowInvoice As DataRow
        Dim bIsAccountCust As Boolean = False  '-- TEMP --

        Dim sPrinterName As String = ""
        Dim sSaleSubTotal As String = ""
        Dim sTotalTax As String = ""
        Dim sSaleRounding As String = ""
        Dim sDiscount, sSaleTotal As String

        Dim sPayDescr As String
        Dim sLaybyDate As String
        Dim sNettRecvd, sChangeGiven, sCashout As String
        Dim decDiscount, decDiscountTax, decCashout As Decimal
        Dim decTotalTax, decRounding As Decimal
        Dim decSubTotalInc, decTotalInc As Decimal
        Dim decPayAmount, decTotalPayments As Decimal

        If (msSelectedReceiptPrinterName <> "") Then  '-from Commit confirm form.
            sPrinterName = msSelectedReceiptPrinterName
        ElseIf msReceiptPrinterName = "" Then
            MsgBox("No Receipt printer selected..", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            sPrinterName = msReceiptPrinterName
        End If

        prtDocs1.PrtSelectedPrinterName = sPrinterName '= msReceiptPrinterName
        prtDocs1.SystemInfo = mSysInfo1  '== mSdSystemInfo
        prtDocs1.UserLogo = mImageUserLogo

        colReceiptLines = New Collection

        rowInvoice = mDataTableInvoice.Rows(0)
        mIntStaff_id = CInt(rowInvoice.Item("staff_id"))
        sTranType = rowInvoice.Item("transactionType")

        '- invoice info-
        '= decSubTotalEx = CDec(rowInvoice.Item("subtotal_ex"))
        decSubTotalInc = CDec(rowInvoice.Item("subtotal_inc"))
        decDiscountTax = CDec(rowInvoice.Item("discount_Tax"))  '== + CDec(rowInvoice.Item("discount_Tax"))
        decDiscount = CDec(rowInvoice.Item("discount_nett")) + decDiscountTax
        '= decCashout = CDec(rowInvoice.Item("cashout"))
        decRounding = CDec(rowInvoice.Item("rounding"))
        decTotalTax = CDec(rowInvoice.Item("total_tax"))
        decTotalInc = CDec(rowInvoice.Item("total_inc"))

        sSaleSubTotal = FormatCurrency(decSubTotalInc, 2)
        sTotalTax = FormatCurrency(decTotalTax, 2)
        sDiscount = FormatCurrency(decDiscount, 2)
        '= sCashout = FormatCurrency(decCashout, 2)
        sSaleRounding = FormatCurrency(decRounding, 2)
        sSaleTotal = FormatCurrency(decTotalInc, 2)

        '-- Format ABN for printing..-
        sABN = VB.Left(msBusinessABN, 2) & " " & Mid(msBusinessABN, 3, 3) & _
                  " " & Mid(msBusinessABN, 6, 3) & " " & Mid(msBusinessABN, 9, 3)
        colReceiptLines.Add("") '--new line..--
        colReceiptLines.Add("") '--new line..--
        colReceiptLines.Add("<bold>")
        '--colLines.Add "<bold>"
        If mbIsLayby Then
            sLaybyDate = Format(rowInvoice.Item("layby_date_started"), "ddd dd-MMM-yyyy")
            colReceiptLines.Add(UCase(sTranType) & " Receipt: " & sLaybyDate)
            colReceiptLines.Add("Layby No: LBY " & CStr(mIntInvoice_id) & ".")

        ElseIf mbIsRefund Then
            '==  Target is new Build 4251..
            '==  Target is new Build 4251..
            '-/refund-
            colReceiptLines.Add(UCase(sTranType) & " : " & _
                                 Format(rowInvoice.Item("invoice_date"), "ddd dd-MMM-yyyy"))
            colReceiptLines.Add("Refund No: JMX " & CStr(mIntInvoice_id) & ".")


        Else  '-sale/refund-
            colReceiptLines.Add(UCase(sTranType) & " Tax Invoice: " & _
                                 Format(rowInvoice.Item("invoice_date"), "ddd dd-MMM-yyyy"))
            colReceiptLines.Add("Invoice No: JMX " & CStr(mIntInvoice_id) & ".")
        End If  '-layby-
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

        colReceiptLines.Add("Served by: " & rowInvoice.Item("docket_name"))
        colReceiptLines.Add("")
        '=3401.416= -customer_barcode-
        sBarcode = Trim(rowInvoice.Item("customer_barcode"))

        colReceiptLines.Add("<bold>")
        colReceiptLines.Add("Customer [" & sBarcode & "]:")
        sCompany = Trim(rowInvoice.Item("companyName"))
        sCustName = rowInvoice.Item("firstname") & " " & rowInvoice.Item("lastname")
        If (sCompany <> "") Then
            colReceiptLines.Add(sCompany)
            sLabelCust = sCompany & "[" & sBarcode & "]"
        Else
            sLabelCust = sCustName & "[" & sBarcode & "]"
        End If
        colReceiptLines.Add(sCustName)
        colReceiptLines.Add("")

        '-- show all items..-
        Dim decQty As Decimal, intQty As Integer, decFraction As Decimal
        If Not (mDataTableSaleItems Is Nothing) And (mDataTableSaleItems.Rows.Count > 0) Then
            For Each row1 In mDataTableSaleItems.Rows
                s1 = VB.Left(row1.Item("description"), 16)   '--description-
                '-- format qty properly-
                decQty = CDec(row1.Item("quantity"))
                intQty = Int(Abs(decQty))
                decFraction = decQty - CDec(intQty)  '--see if any decimals..
                If decFraction = 0 Then '-- integer only-
                    s1 &= RSet(" x " & CStr(intQty), 9)  '--"qty"
                Else '--has fraction-
                    s1 &= RSet(" x " & Format(decQty, "  0.00"), 9)   '--"qty"
                End If
                '= s1 &= " x" & CStr(row1.Item("quantity"))   '--"qty"
                s1 &= ": " & RSet(FormatCurrency(row1.Item("total_inc"), 2), 12)   '--"extension_ex"
                colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                colReceiptLines.Add(s1)
            Next row1
        End If
        colReceiptLines.Add("")
        colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
        colReceiptLines.Add("Subtotal:  " & RSet(sSaleSubTotal, 12))
        colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
        colReceiptLines.Add("Discount:  " & RSet(sDiscount, 12))
        '= colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
        '= colReceiptLines.Add("Cashout:   " & RSet(sCashout, 10))
        colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
        colReceiptLines.Add("(incl GST  " & RSet(sTotalTax, 12) & ")")
        colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
        colReceiptLines.Add("Rounding:  " & RSet(sSaleRounding, 12))
        colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
        colReceiptLines.Add("Total:     " & RSet(sSaleTotal, 12))

        colReceiptLines.Add("")

        '-- Add list of payments for this invoice..

        '==  Target is new Build 4251..
        '==  Target is new Build 4251..
        '-sale/refund-
        If mbIsRefund Then
            colReceiptLines.Add("Refunded As:")
            For Each sLine1 As String In mColRefundReceiptInfo
                colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                colReceiptLines.Add(sLine1)
            Next sLine1
        Else '-not refund-
            colReceiptLines.Add("Payments:")
            '-- get payment and change if any..-
            If (Not bIsAccountCust) AndAlso _
                     (Not (mDataTablePayments Is Nothing)) AndAlso (mDataTablePayments.Rows.Count > 0) Then
                row1 = mDataTablePayments.Rows(0)
                sNettRecvd = FormatCurrency(row1.Item("nettAmountCredited"), 2)
                s1 = FormatCurrency(row1.Item("changeGiven"), 2)
                sChangeGiven = RSet(s1, 12)
                If Not (mDataTablePaymentDetails Is Nothing) Then
                    For Each row1 In mDataTablePaymentDetails.Rows
                        s1 = CStr(row1.Item("paymentType_descr"))
                        sPayDescr = LSet(s1 & ": ", 22)
                        decPayAmount = row1.Item("amount")
                        decTotalPayments += decPayAmount
                        '=sPayAmount = RSet(FormatCurrency(decPayAmount, 2), 12)
                        colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                        colReceiptLines.Add(sPayDescr & RSet(FormatCurrency(decPayAmount, 2), 12))
                        '==listPaymentDetail.Items.Add(sPayDescr & "|" & sPayAmount)
                    Next row1
                End If  '-nothing-
                colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                colReceiptLines.Add(LSet("Change Given  : ", 22) & sChangeGiven)
                colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
                colReceiptLines.Add("")
                colReceiptLines.Add(LSet("Nett Receivd  : ", 22) & RSet(sNettRecvd, 12))
            End If  '-payments.
        End If  '-refund/sale-


        'colReceiptLines.Add("Payments:")
        ''-- get payment and change if any..-
        'If (Not bIsAccountCust) AndAlso _
        '         (Not (mDataTablePayments Is Nothing)) AndAlso (mDataTablePayments.Rows.Count > 0) Then
        '    row1 = mDataTablePayments.Rows(0)
        '    sNettRecvd = FormatCurrency(row1.Item("nettAmountCredited"), 2)
        '    s1 = FormatCurrency(row1.Item("changeGiven"), 2)
        '    sChangeGiven = RSet(s1, 10)
        '    If Not (mDataTablePaymentDetails Is Nothing) Then
        '        For Each row1 In mDataTablePaymentDetails.Rows
        '            s1 = CStr(row1.Item("paymentType_descr"))
        '            sPayDescr = LSet(s1 & ": ", 22)
        '            decPayAmount = row1.Item("amount")
        '            decTotalPayments += decPayAmount
        '            '=sPayAmount = RSet(FormatCurrency(decPayAmount, 2), 12)
        '            colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
        '            colReceiptLines.Add(sPayDescr & RSet(FormatCurrency(decPayAmount, 2), 10))
        '            '==listPaymentDetail.Items.Add(sPayDescr & "|" & sPayAmount)
        '        Next row1
        '    End If  '-nothing-
        '    colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
        '    colReceiptLines.Add(LSet("Change Given  : ", 22) & sChangeGiven)
        '    colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
        '    colReceiptLines.Add("")
        '    colReceiptLines.Add(LSet("Nett Receivd  : ", 22) & RSet(sNettRecvd, 10))
        'End If


        colReceiptLines.Add("")
        colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.

        '=3519.0221=--DROP "Total Payments"..
        'colReceiptLines.Add(LSet("Total Payments:", 22) & _
        '                                    RSet(FormatCurrency(decTotalPayments, 2), 10))
        If (mDecCreditNoteAmountDebited <> 0) Then
            s1 = FormatCurrency(mDecCreditNoteAmountDebited, 2)
            s1 = RSet(s1, 12)
            colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
            colReceiptLines.Add(LSet("Redeemed CreditNote:", 22) & "|" & s1)
        End If

        colReceiptLines.Add("")
        If (mDecCreditNoteAmountCredited <> 0) Then
            s1 = FormatCurrency(mDecCreditNoteAmountCredited, 2)
            s1 = RSet(s1, 12)
            colReceiptLines.Add("<lucida>")   '-- lucida console fixed pitch.
            colReceiptLines.Add(LSet("Saved as CreditNote:", 22) & s1)
        End If
        colReceiptLines.Add("")

        colReceiptLines.Add("<tahoma>")
        colReceiptLines.Add("<bold>")
        colReceiptLines.Add("<big>")
        '--colLines.Add "<bold>"
        colReceiptLines.Add("Thank You.")
        colReceiptLines.Add("= = = = = = = = = = = = = = = = = = = = =") '--new line..--

        '- finish..
        prtDocs1.versionPOS = msVersionPOS
        prtDocs1.UserLogo = mImageUserLogo
        '= prtDocs1.PrtSelectedPrinterName = strPrinterName

        '-- go print--
        If Not prtDocs1.PrintDocket(colReceiptLines) Then
            MsgBox("Print Receipt Failed..", MsgBoxStyle.Exclamation)
        End If

        '- print Label for Layby.
        If mbIsLayby Then
            Dim clsPrint1 As New clsPrintSaleDocs
            Dim frmGetPrinter1 As frmGetPrinter
            Dim msPrinterName As String
            Dim intNewCount As Integer

            frmGetPrinter1 = New frmGetPrinter
            frmGetPrinter1.WhichPrinter = "label"
            frmGetPrinter1.RequestedNumberOfLabels = 2
            frmGetPrinter1.ShowDialog()
            If frmGetPrinter1.cancelled Then
                frmGetPrinter1.Close()
                MsgBox("Request was cancelled.", MsgBoxStyle.Information)
                Exit Sub
            End If
            intNewCount = frmGetPrinter1.NumberOfLabels
            msPrinterName = frmGetPrinter1.SelectedPrinterName
            frmGetPrinter1.Close()
            If (intNewCount > 0) Then
                '- do printing-
                Call clsPrint1.PrintLaybyLabels(mIntInvoice_id, intNewCount, _
                                                msPrinterName, sLabelCust, _
                                                 sLaybyDate & "; Value: " & sSaleTotal)
            End If  '-count-
        End If  '-layby.-
        Me.BringToFront()

    End Sub  '--PrintReceipt-
    '= = = = = = = = = = = = = 
    '= = = = = = = = = = = =  = = = = =
    '-===FF->

    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==

    Private Sub btnAccountReversal_Click(sender As Object, e As EventArgs) Handles btnAccountReversal.Click

        Dim clsAccountRev1 As clsAccountReversal

        If (btnAccountReversal.BackColor = Color.WhiteSmoke) Then
            If MessageBox.Show("Are you sure you want to reverse this new Sale Invoice ?", _
                                "Reverse New Sale", MessageBoxButtons.YesNo, _
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
        End If  '-color-
        '-- yes, do it..
        btnAccountReversal.Enabled = False

        clsAccountRev1 = New clsAccountReversal(mCnnSql, msVersionPOS, mImageUserLogo, mIntCallerStaff_id, _
                                                                     msCallerStaffName, msInvoicePrinterName, Me)
        If Not clsAccountRev1.CreateAccountReversal(mIntInvoice_id, mDataTableInvoice, _
                                                      mDataTableSaleItems, mDataTablePayments) Then
            MsgBox("Reversal was NOT done.", MsgBoxStyle.Exclamation)
        Else
            MsgBox("ok.. The Sale Invoice was reversed with an Account Refund.", MsgBoxStyle.Information)
        End If

    End Sub  '-reversal--
    '= = = =   == = = = 
    '-===FF->


    '==   Target-New-Build-4282 -- (Started 12-October-2020)
    '==
    '==   New BUTTON and Function to REVERSE a Cash Sale Invoice, incl. REVERSAL of assoc. Payment.
    '==         Done by cloning clsAccountReversal and moddying and adding Payment Reversal stuff..
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    Private Sub btnCashSaleReversal_Click(sender As Object, e As EventArgs) Handles btnCashSaleReversal.Click
        Dim clsCashRev1 As clsCashSaleReversal

        If (btnCashSaleReversal.BackColor = Color.WhiteSmoke) Then
            If MessageBox.Show("Are you sure you want to reverse this new Sale Invoice ?",
                                "Reverse New Sale", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If
        End If  '-color-
        '-- yes, do it..
        btnCashSaleReversal.Enabled = False

        clsCashRev1 = New clsCashSaleReversal(mCnnSql, msVersionPOS, mImageUserLogo, mIntCallerStaff_id,
                                                                     msCallerStaffName, msInvoicePrinterName, Me)
        If Not clsCashRev1.CreateCashSaleReversal(mIntInvoice_id, mDataTableInvoice,
                                                      mDataTableSaleItems, mDataTablePayments) Then
            MsgBox("Reversal was NOT done.", MsgBoxStyle.Exclamation)
        Else
            MsgBox("ok.. The Sale Invoice was reversed with a Refund Transaction.", MsgBoxStyle.Information)
        End If

    End Sub  '-btnCashSaleReversal-
    '= = = = = = = = = = =  = = = = = =


End Class  '-show invoice-

'== end form ==