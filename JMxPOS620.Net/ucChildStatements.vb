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
Imports system.Threading

Public Class ucChildStatements

    '-- Customer Statements --
    '-- Customer Statements --

    '==
    '==  grh. JobMatix 3.1.3101.0316 ---  16-Mar-2015 ===
    '==   >>  Started Customer Statements... 
    '==
    '==  grh. JobMatix 3.1.3107.0906 ---  06-Sep-2015 ===
    '==   >> Saving statement PDF in DB docTable for emailing... 
    '==
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==
    '==     v3.3.3303.0114..  14-Jan-2017= ===
    '==       >> Add functionality to Debtors Payments to
    '==            Disburse Refund values to Outstanding Invoices...- 
    '==       >>  Refunds applied in Payments are shown as already disbursed credits..
    '==       >>  Outstanding Refunds are now sourced from colRefunds.
    '==             and listed separately.
    '== = = = = = = = = = = = == = = 
    '==     3403.711- 11July2017-
    '==       -- Debtors Statements. Fix Execute-all crash-
    '==       -- Check for Microsoft PDF printer also.-
    '==
    '==     3403.1009- 09-Oct-2017-
    '==      -- POS Emails now to use Server File-System to store Invoice PDF's for Email..
    '==            at \\[server]\users\public\JobMatixPOS-EmailQueue\ 
    '==                    (SystemInfo setting is :  "POS_EMAILQUEUE_SHAREPATH"
    '==               NB: (Table "DocArchive" to be DROPPED..)
    '==      --  XML Descriptor file to go with each PDF for Email sending info. 
    '==
    '==     3403.1015=  NO MORE REFUNDS in statements..
    '==     3403.1102=  Caller can request statement for single Customer..
    '==   NEW BUILD no..
    '==     3411.1119=  Fixes to wait for PDF to be created....
    '== 
    '==
    '==       3411.0110=  10-Jan-2018=
    '==          --  Get PDF preferred printer...
    '==             -(Microsoft PDF will be preferred)..
    '==
    '==   Updated.- 3519.0224  Started 22-Feb-2019= 
    '==     -- Update to Debtors Report- Add option for Summary only.... 
    '==     -- Update to Debtors Statements to Add option for Summary only.... 
    '==
    '==   Updated.- 3519.0311  Started 05-March-2019= 
    '==      >>  Statements- Make ZERO the default for Closed invoices Days-to-show..
    '==
    '==
    '==   Updated.- 3519.0319  19-March-2019= 
    '==    --  Statements- Add extra button to mean Include Email recipients only...
    '==
    '==  NEW VERSION 4.--
    '==  NEW VERSION 4.--
    '==
    '==    -- 4201.0618/0623.  11/18-June-2019-   
    '==         --  Debtors Report and Statements. 
    '==               Show Credit-Note Available balance and Cust. Phone No....
    '== NEW revision-
    '==
    '==   -- 4201.1027/1030/1031.  31-Oct-2019-  Started 27-Oct-2019-
    '==      -- STATEMENTS-- For Emailing, address customer as << FirstName LastName >>...
    '==
    '== NEW Build-
    '==
    '==   -- 4219.1106.  06-Nov-2019-  Started 06-November-2019-
    '==      -- Statements Control-  
    '==           Update to use new Debtors info class (as per Debtors Report from menu). 
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = =

    Private Const k_statementPrtSettingKey As String = "POS_StatementPrinter"

    '-- Customer DataGridView columns.--
    Private Const k_CUSTGRIDCOL_CUSTNO As Short = 0
    Private Const k_CUSTGRIDCOL_CUSTOMER_NAME As Short = 1
    '-  Private Const k_CUSTGRIDCOL_LASTNAME As Short = 3
    '=- Private Const k_CUSTGRIDCOL_COMPANY As Short = 4
    Private Const k_CUSTGRIDCOL_OUTST As Short = 2
    Private Const k_CUSTGRIDCOL_EMAILADDRESS As Short = 3
    Private Const k_CUSTGRIDCOL_CHKPRINT As Short = 4
    Private Const k_CUSTGRIDCOL_CHKEMAIL As Short = 5
    Private Const k_CUSTGRIDCOL_CHKINCLUDE As Short = 6  '--Mark to Send-
    Private Const k_CUSTGRIDCOL_RESULT As Short = 7
    Private Const k_CUSTGRIDCOL_INVOICES As Short = 8   '--collection of invoices/payments-
    Private Const k_CUSTGRIDCOL_CUSTINFO As Short = 9   '--collection of cust name/address to print-
    '== = = = = = = = = == = = = = = = = == = = = = = = = =

    Private mbIsInitialising As Boolean = True
    Private mbIsLoading As Boolean = True

    Private mFrmParent As Form
    Private mbActivated As Boolean = False   '-to activate once only.-
    Private msAppPath As String = ""
    Private msAppFullname As String = ""
    Private msStatementFilePath As String = ""

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
    '-- wait form--
    Private mFormWait1 As frmWait

    Private mColPrefsCustomer As Collection
    Private mImageUserLogo As Image

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1
    '=3403.1102=
    Private mIntRequestedCustomer_id As Integer = -1

    '==3301.428= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '==3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private msCallerStaffName As String = ""
    Private msBusinessABN As String = ""
    Private msBusinessName As String = ""
    Private msEmailTextStatement As String = "Your Statement is attached."  '-default-
    Private msEmailQueueSharePath As String = ""

    Private msStatementPrinterName As String = ""
    Private msDefaultPrinterName As String = ""
    Private msPdfPrinterName As String = ""

    '- Customer--
    Private msCustomerBarcode As String = ""
    Private mIntCustomer_id As Integer = -1
    Private mbIsAccountCust As Boolean = False

    Private msTransactionType As String = ""

    Private mDecTotalInvoices As Decimal
    Private mDecTotalOutstanding As Decimal

    Private mDecAmountPaying As Decimal  '--Header total paying today-
    Private mDecSubTotalPaying As Decimal
    Private mDecBalanceOwing As Decimal

    Private mDecPaymentTotalRcvd As Decimal
    Private mDecPaymentCashRcvd As Decimal
    Private mDecChange As Decimal
    Private mDecPaymentNettCredited As Decimal
    Private mIntCurrentPaymentTypeIndex As Integer = -1

    '= Private mColInvoices As Collection

    Private mColReportCustomers As Collection
    Private mbCloseRequested As Boolean = False

    '== Private clsWinSpec1 As New clsWinSpecial    '--3107.820 for app data pqth-

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport
    '= = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)
    '= = = = = = = = = = = = = = = = = = = = == = 


    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                            ByVal intParentHeight As Integer)

        Me.Width = intParentWidth - 11
        Me.Height = intParentHeight - 11
        '-- resize our controls..

        '=4201.0619=  
        panelAction.Left = Me.Width - panelAction.Width - 7

        dgvCustomers.Width = Me.Width - 11
        dgvCustomers.Height = Me.Height - dgvCustomers.Top - 12

        DoEvents()
        '-- resize main box and top panel-

    End Sub '-SubFormResized
    '= = = = = = = = = = = =  === =

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click....

    Public Function SubFormCloseRequest() As Boolean

        '=- Return true if ok to Close.
        If mbIsLoading Then
            MsgBox("Can't close this panel while loading grid !", MsgBoxStyle.Exclamation)
            '= Exit Function
            SubFormCloseRequest = False
        Else  '-ok-
            SubFormCloseRequest = True
        End If

        '= mbCloseRequested = True
        '= Call close_me()
    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    '--sub new-
    '--sub new-

    Public Sub New(ByRef FrmParent As Form, _
                     ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                        ByRef colPrefsCustomer As Collection, _
                          ByVal sVersionPOS As String, _
                          ByRef imageUserLogo As Image, _
                            ByVal intStaff_id As Integer, _
                               ByVal sStaffName As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        mFrmParent = FrmParent

        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName

        mColSqlDBInfo = colSqlDBInfo
        mColPrefsCustomer = colPrefsCustomer
        msVersionPOS = sVersionPOS
        mImageUserLogo = imageUserLogo

        mIntStaff_id = intStaff_id
        msStaffName = sStaffName
        '=3403.1102=
        mIntRequestedCustomer_id = -1  '- caller must use input property.


    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==

    '-- Input requested customer_id..

    WriteOnly Property requestedCustomer_id As Integer
        Set(value As Integer)
            mIntRequestedCustomer_id = value
        End Set
    End Property      '-- Input requested customer_id..
    '= = = = = = = = = = = = = = = = = == = = =
    '-===FF->

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String)

        mFormWait1 = New frmWait
        mFormWait1.waitTitle = msVersionPOS
        mFormWait1.labHdr.Text = "Debtors Statements-"
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.Show(Me)
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

    '-- load Adobe registry PDF FileName key-

    Private Function mbSetAdobeFileName(ByVal sFullFilePath As String) As Boolean

        mbSetAdobeFileName = gbSetAdobeFileName(sFullFilePath, msAppFullname)
        Exit Function
        '=3411.1118=
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

    '-- re-load Customer Grid-
    '==
    '==    -- 4201.0618.  11/18-June-2019-   
    '==         --  Debtors Report and Statements. 
    '==               Show Credit-Note Available balance and Cust. Phone No....

    'Private Function mbReloadCustomers() As Boolean
    '    Dim dataTable1 As DataTable
    '    Dim row1 As DataRow
    '    Dim gridRow1 As DataGridViewRow   '=item1 As ListViewItem
    '    Dim sSql, s1 As String
    '    Dim rx, intDays, intCustomer_id As Integer
    '    Dim intRowx, intLoaded As Integer
    '    Dim bOutstandingInvoicesOnly As Boolean
    '    Dim colInvoices As Collection
    '    Dim decTotalInvoices As Decimal
    '    Dim decTotalOutstanding As Decimal

    '    '== 3303.0114- REFUNDS 14Jan2017=
    '    Dim colRefund1, colRefunds As Collection
    '    Dim decThisAmt, decTotalRefunds As Decimal
    '    Dim decTotalAvailable As Decimal
    '    '==    -- 4201.0618.  11/18-June-2019- 
    '    Dim dicCreditNoteBalances As Dictionary(Of Integer, Decimal) '-- key is customer_id-..
    '    '-- FOR testing, track names also..
    '    Dim dicCreditNoteCustNames As Dictionary(Of Integer, String) '-- key is customer_id-..

    '    mbIsLoading = True

    '    dgvCustomers.Enabled = False
    '    dgvCustomers.SuspendLayout()

    '    Dim dateCutoff As Date = DTPickerCutoff.Value

    '    panelOptions.Enabled = False
    '    panelAction.Enabled = False
    '    panelSelection.Enabled = False

    '    btnPrintSelection.Enabled = False
    '    btnExecute.Enabled = False

    '    dgvCustomers.Enabled = False
    '    labStatus.Text = "Reloading customers.."
    '    mbReloadCustomers = False
    '    intDays = 0

    '    If IsNumeric(cboDaysToShow.SelectedItem) Then
    '        intDays = CInt(cboDaysToShow.SelectedItem)
    '    End If
    '    bOutstandingInvoicesOnly = optShowingOutst.Checked

    '    dgvCustomers.Rows.Clear()
    '    '== mColInvoices = New Collection
    '    mColReportCustomers = New Collection      '--collect all customers with their invoices.

    '    '--  load grid with Account customers..--
    '    '-- for each customer get invoices and payments collection..
    '    '-   Filter according to options..-
    '    sSql = "SELECT *, "
    '    sSql &= " CASE companyName "
    '    '= sSql &= "  WHEN '' THEN lastName + ', ' + firstName "
    '    sSql &= "  WHEN '' THEN lastName + ', ' + firstName "
    '    sSql &= "     ELSE companyName "
    '    sSql &= "  END  AS custShortName "
    '    sSql &= " FROM dbo.customer "
    '    sSql &= "  WHERE (isAccountCust=1) "
    '    '=3403.1102=
    '    '== mIntRequestedCustomer_id = -1  if no caller input property.
    '    If (mIntRequestedCustomer_id > 0) Then  '-individual customer requested.
    '        sSql &= "  AND (customer_id=" & CStr(mIntRequestedCustomer_id) & ") "
    '    End If
    '    sSql &= " ORDER BY custShortName;"

    '    If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
    '        MsgBox("Error in getting recordset for Customer table: " & vbCrLf & _
    '                                       gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
    '    Else  '-ok-
    '        If Not (dataTable1 Is Nothing) Then
    '            intRowx = 0
    '            intLoaded = 0
    '            Dim colCustInfo As Collection
    '            Dim colReportCust As Collection

    '            Call mWaitFormOn("Pls Wait. Getting Account Info.." & vbCrLf & _
    '                " This might take a minute.")

    '            '==
    '            '==    -- 4201.0618.  11/18-June-2019-   
    '            '==         --  Debtors Report and Statements. 
    '            '==               Show Credit-Note Available balance and Cust. Phone No....
    '            '--  SO- Collect ALL Credit Note balances
    '            '--      so we can attach balance to customer collection.
    '            Dim dtCreditNotes As DataTable
    '            Dim decTotalCredits, decTotalDebits, decCreditNoteCreditRemaining As Decimal
    '            Dim decCrbal, decOldBal, decTotalCredit As Decimal
    '            Dim intThisCustId As Integer
    '            Dim sThisCustName As String

    '            dicCreditNoteBalances = New Dictionary(Of Integer, Decimal)
    '            dicCreditNoteCustNames = New Dictionary(Of Integer, String)
    '            decTotalCredit = 0

    '            '- get all credit notes.. (ALL custs.).
    '            If Not gbGetCreditNoteHistory(mCnnSql, -1, dtCreditNotes, _
    '                                                 decTotalCredits, decTotalDebits, decCreditNoteCreditRemaining) Then
    '                MsgBox("Failed Looking up credit notes.. ", MsgBoxStyle.Exclamation)

    '            Else  '-ok-
    '                If (dtCreditNotes IsNot Nothing) AndAlso (dtCreditNotes.Rows.Count > 0) Then
    '                    Dim intTrCount As Integer = dtCreditNotes.Rows.Count
    '                    '-- load all credit balances into collection.

    '                    '-EACH ROW is a PAYMENT record !!-
    '                    '-EACH ROW is a PAYMENT record !!-
    '                    '-- DataTable is in order of Cust-name, so we can't use that as ID/Order etc
    '                    '-- Sumarise each payment for Credit-Nore Bal, and add it to Customer dictionary.
    '                    For Each rowCN1 As DataRow In dtCreditNotes.Rows

    '                        '-- get Account Custs only..
    '                        If (CInt(rowCN1.Item("isAccountCust")) <> 0) Then  '-is account..
    '                            '--get customer ID from payment..
    '                            intThisCustId = rowCN1.Item("customer_id")
    '                            sThisCustName = rowCN1.Item("customerName")
    '                            decCrbal = 0
    '                            If (CDec(rowCN1.Item("creditNotePaymentCredited")) <> 0) Then
    '                                decCrbal += CDec(rowCN1.Item("creditNotePaymentCredited"))
    '                            ElseIf (CDec(rowCN1.Item("refundAsCreditNoteCredited")) <> 0) Then
    '                                decCrbal += CDec(rowCN1.Item("refundAsCreditNoteCredited"))
    '                            ElseIf (CDec(rowCN1.Item("creditNoteAmountDebited")) <> 0) Then
    '                                decCrbal -= CDec(rowCN1.Item("creditNoteAmountDebited"))
    '                            End If
    '                            '--If some movement then, Create cust entry, or add to it..
    '                            If decCrbal <> 0 Then
    '                                '= decTotalCredit += decCrbal
    '                                If dicCreditNoteBalances.ContainsKey(intThisCustId) Then
    '                                    decOldBal = dicCreditNoteBalances.Item(intThisCustId)
    '                                    dicCreditNoteBalances.Item(intThisCustId) = decOldBal + decCrbal
    '                                Else  '-new one.
    '                                    dicCreditNoteBalances.Add(intThisCustId, decCrbal)
    '                                    '-testing-
    '                                    dicCreditNoteCustNames.Add(intThisCustId, sThisCustName)
    '                                End If  '-contains.
    '                            End If  '-zero-
    '                        End If  '--account-
    '                    Next rowCN1
    '                    '= colCreditNoteBalances.Add(decCrbal, sThisCustId)
    '                End If  '-nothing-
    '            End If  '-get-

    '            '--testing-  show complete list of cr-note balances..
    '            s1 = ""
    '            decTotalCredit = 0
    '            For Each kvpBal As KeyValuePair(Of Integer, Decimal) In dicCreditNoteBalances
    '                intThisCustId = kvpBal.Key
    '                decCrbal = kvpBal.Value
    '                decTotalCredit += decCrbal
    '                s1 &= intThisCustId & ": " & dicCreditNoteCustNames.Item(intThisCustId) & ": " & _
    '                                                                           FormatCurrency(kvpBal.Value, 2) & vbCrLf
    '            Next kvpBal
    '            '-test msg..
    '            'MsgBox("Testing-  " & dicCreditNoteBalances.Count & _
    '            '            " Credit Note Balances: Total= " & FormatCurrency(decTotalCredit, 2) & _
    '            '                                                   vbCrLf & s1, MsgBoxStyle.Information)
    '            '-- DONE getting credit notes..
    '            '-- DONE getting credit notes..
    '            '-- DONE getting credit notes..

    '            Dim intCustCount As Integer = dataTable1.Rows.Count
    '            '- For each Account Customer-
    '            For Each row1 In dataTable1.Rows
    '                If mbCloseRequested Then  '-stop loading grid.
    '                    Call mWaitFormOff()
    '                    mbIsLoading = False
    '                    If Not (Me.delReport Is Nothing) Then
    '                        delReport.Invoke(Me.Name, "FormClosed", "")
    '                    End If
    '                    Exit Function
    '                End If
    '                intRowx += 1
    '                labStatus.Text = "Reviewing all Accounts:  " & intRowx & "/" & intCustCount & ".."
    '                mFormWait1.labHdr.Text = "Debtors Statements- " & intRowx & "/" & intCustCount & ".."

    '                DoEvents()
    '                intCustomer_id = row1.Item("customer_id")
    '                '--get invoices this customer..-
    '                If Not gbCollectCustomerInvoices(mCnnSql, intCustomer_id, _
    '                                            bOutstandingInvoicesOnly, dateCutoff, _
    '                                             colInvoices, decTotalInvoices, decTotalOutstanding, intDays) Then
    '                    Exit For  '=failed-
    '                End If '-collect invoices.

    '                '==  3303.0114= 14Jan2017- Refunds applied in Payments are shown as already disbursed credits..
    '                '==   Outstanding Refunds are now sourced from colRefunds.
    '                '==             and listed sepatately.
    '                '-- get oustanding refunds.. if any-

    '                '=3403.1015=  NO MORE REFUNDS in statements..
    '                decTotalRefunds = 0
    '                colRefunds = New Collection '--now always empty=

    '                'If Not gbCollectCustomerRefunds(mCnnSql, intCustomer_id, _
    '                '                                     colRefunds, _
    '                '                                     decTotalRefunds, _
    '                '                                     decTotalAvailable) Then
    '                '    decTotalRefunds = 0
    '                '    colRefunds = New Collection '--empty=
    '                'End If  '--collect refunds-

    '                '-- ignore customer if no invoices..- 
    '                If ((colInvoices IsNot Nothing)) AndAlso (colInvoices.Count > 0) Then
    '                    colReportCust = New Collection
    '                    gridRow1 = New DataGridViewRow
    '                    dgvCustomers.Rows.Add(gridRow1)
    '                    '-- maybe slow it down a bit..
    '                    DoEvents()
    '                    Thread.Sleep(10)  '=Thread.Sleep(100)  '--miiliseconds

    '                    rx = dgvCustomers.Rows.Count - 1  '--last row -
    '                    With dgvCustomers.Rows(rx)
    '                        .Cells(k_CUSTGRIDCOL_CHKINCLUDE).Value = 0  '-now unchecked. was 1  '--checked-
    '                        .Cells(k_CUSTGRIDCOL_CUSTNO).Value = row1.Item("barcode")
    '                        .Cells(k_CUSTGRIDCOL_CUSTOMER_NAME).Value = row1.Item("custShortName")
    '                        .Cells(k_CUSTGRIDCOL_OUTST).Value = FormatCurrency(decTotalOutstanding, 2)
    '                        .Cells(k_CUSTGRIDCOL_EMAILADDRESS).Value = row1.Item("email")
    '                        If (row1.Item("email") = "") Or _
    '                                 chkNoEmail.Checked Or (mIntRequestedCustomer_id > 0) Then '-no email- or Show Only..
    '                            .Cells(k_CUSTGRIDCOL_CHKPRINT).Value = 1
    '                            .Cells(k_CUSTGRIDCOL_CHKEMAIL).Value = 0
    '                        Else  '-have email-
    '                            .Cells(k_CUSTGRIDCOL_CHKPRINT).Value = 0
    '                            .Cells(k_CUSTGRIDCOL_CHKEMAIL).Value = 1
    '                        End If
    '                        .Cells(k_CUSTGRIDCOL_RESULT).Value = ""
    '                        .Cells(k_CUSTGRIDCOL_INVOICES).Value = colInvoices
    '                        '-- collect Customer Info for Statement print..-
    '                        colCustInfo = New Collection
    '                        colCustInfo.Add(intCustomer_id, "customer_id")
    '                        colCustInfo.Add(row1.Item("barcode"), "barcode")
    '                        colCustInfo.Add(row1.Item("firstName"), "firstName")
    '                        colCustInfo.Add(row1.Item("lastName"), "lastName")
    '                        colCustInfo.Add(row1.Item("companyName"), "companyName")
    '                        colCustInfo.Add(row1.Item("custShortName"), "custShortName")
    '                        '- make full name-
    '                        s1 = Trim(row1.Item("companyName"))
    '                        If (s1 <> "") Then s1 &= "- "
    '                        s1 &= row1.Item("firstName") & " " & row1.Item("lastName")
    '                        colCustInfo.Add(s1, "customerFullName")

    '                        colCustInfo.Add(row1.Item("address"), "address")
    '                        colCustInfo.Add(row1.Item("suburb"), "suburb")
    '                        colCustInfo.Add(row1.Item("state"), "state")
    '                        colCustInfo.Add(row1.Item("postcode"), "postcode")
    '                        colCustInfo.Add(row1.Item("country"), "country")
    '                        colCustInfo.Add(row1.Item("email"), "email")
    '                        colCustInfo.Add(row1.Item("creditLimit"), "creditLimit")
    '                        colCustInfo.Add(row1.Item("creditDays"), "creditDays")
    '                        '=4201.0618=
    '                        '=4201.0618=
    '                        '=4201.0618=
    '                        colCustInfo.Add(row1.Item("phone"), "phone")
    '                        colCustInfo.Add(row1.Item("mobile"), "mobile")
    '                        '-decCrbal-
    '                        decCrbal = 0
    '                        If dicCreditNoteBalances.ContainsKey(intCustomer_id) Then
    '                            decCrbal = dicCreditNoteBalances.Item(intCustomer_id)
    '                        End If
    '                        '-- add to cust collection.
    '                        colCustInfo.Add(decCrbal, "CreditNoteBalance")
    '                        '= done credit note.

    '                        '-doNotEmailDocuments-
    '                        colCustInfo.Add(row1.Item("doNotEmailDocuments"), "doNotEmailDocuments")
    '                        '- save cust. info in grid row.
    '                        .Cells(k_CUSTGRIDCOL_CUSTINFO).Value = colCustInfo
    '                        '-- save for report..
    '                        colReportCust.Add(colCustInfo, "CustInfo")
    '                        colReportCust.Add(colInvoices, "Invoices")
    '                        colReportCust.Add(colRefunds, "Refunds")
    '                        mColReportCustomers.Add(colReportCust, CStr(intCustomer_id))
    '                    End With  '-dgvCustomers.Rows(rx)-
    '                    intLoaded += 1
    '                End If  '-nothing-
    '            Next row1  '--customer-
    '            Call mWaitFormOff()
    '            mbReloadCustomers = True
    '        End If  '-nothing-
    '    End If  '-get-
    '    labLoadedCount.Text = Format(intLoaded, "####0")
    '    panelOptions.Enabled = True
    '    dgvCustomers.Enabled = True
    '    btnPrintSelection.Enabled = True
    '    btnExecute.Enabled = True
    '    panelAction.Enabled = True
    '    panelSelection.Enabled = True

    '    dgvCustomers.ResumeLayout()
    '    dgvCustomers.Enabled = True

    '    labStatus.Text = "Done.."

    '    mbIsLoading = False

    'End Function  '--reLoad-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- re-load Customer Grid-
    '==
    '==    -- 4201.0618.  11/18-June-2019-   
    '==         --  Debtors Report and Statements. 
    '==               Show Credit-Note Available balance and Cust. Phone No..

    '==   -- 4219.1106.  06-Nov-2019-  Started 06-November-2019-
    '==        REWRITE to use new Debtors info class (as per Debtors Report from menu). 

    Private Function mbReloadCustomersEx() As Boolean
        '= Dim dataTable1 As DataTable
        '= Dim row1 As DataRow
        Dim gridRow1 As DataGridViewRow   '=item1 As ListViewItem
        Dim sSql, s1 As String
        Dim rx, intClosedDaysToShow, intCustomer_id As Integer
        Dim intRowx, intLoaded As Integer
        Dim bOutstandingInvoicesOnly As Boolean
        Dim colInvoice, colInvoices As Collection
        '= Dim decTotalInvoices As Decimal
        Dim decAmountOutstanding, decTotalOutstanding As Decimal

        '== 3303.0114- REFUNDS 14Jan2017=
        Dim colRefund1, colRefunds As Collection
        Dim decThisAmt, decTotalRefunds As Decimal
        Dim decTotalAvailable As Decimal
        '==    -- 4201.0618.  11/18-June-2019- 
        '= Dim dicCreditNoteBalances As Dictionary(Of Integer, Decimal) '-- key is customer_id-..
        '-- FOR testing, track names also..
        '= Dim dicCreditNoteCustNames As Dictionary(Of Integer, String) '-- key is customer_id-..

        mbIsLoading = True

        dgvCustomers.Enabled = False
        dgvCustomers.SuspendLayout()

        Dim dateCutoff As Date = DTPickerCutoff.Value

        panelOptions.Enabled = False
        panelAction.Enabled = False
        panelSelection.Enabled = False

        btnPrintSelection.Enabled = False
        btnExecute.Enabled = False

        dgvCustomers.Enabled = False
        labStatus.Text = "Reloading customers.."
        mbReloadCustomersEx = False

        intClosedDaysToShow = 0

        If IsNumeric(cboDaysToShow.SelectedItem) Then
            intClosedDaysToShow = CInt(cboDaysToShow.SelectedItem)
        End If
        bOutstandingInvoicesOnly = optShowingOutst.Checked

        dgvCustomers.Rows.Clear()
        '== mColInvoices = New Collection
        mColReportCustomers = New Collection      '--collect all customers with their invoices.

        '--  load grid with Account customers..--
        '-- for each customer get invoices and payments collection..
        '-   Filter according to options..-

        '=4201.1112=-- NEW VERSION..
        '=4201.1112=-- NEW VERSION..

        Dim clsDebtors1 As clsDebtors
        Dim intRequestedCustomer_id As Integer = -1
        intRowx = 0
        intLoaded = 0
        Dim colCustInfo As Collection
        Dim colReportCust As Collection

        clsDebtors1 = New clsDebtors(mCnnSql, msSqlDbName, _
                               mColSqlDBInfo, mColPrefsCustomer, _
                               msVersionPOS, mImageUserLogo, mIntStaff_id, msStaffName)
        '--  + intClosedDaysToShow of prev. closed invoices
        If clsDebtors1.GetAllDebtorReportInfo(bOutstandingInvoicesOnly, intClosedDaysToShow, _
                                               dateCutoff, mColReportCustomers, intRequestedCustomer_id) Then
            If (Not (mColReportCustomers Is Nothing)) AndAlso (mColReportCustomers.Count > 0) Then
 
                Call mWaitFormOn("Pls Wait. Getting Account Info.." & vbCrLf & _
                    " This might take a minute.")
                '-- save for report..
                '--  Populate the Customer Statements DataGrid.
                For Each colReportCust In mColReportCustomers
                    colCustInfo = colReportCust.Item("CustInfo")
                    colInvoices = colReportCust.Item("Invoices")
                    colRefunds = colReportCust.Item("Refunds")

                    '-- ignore customer if no invoices..- 
                    If ((colInvoices IsNot Nothing)) AndAlso (colInvoices.Count > 0) Then
                        '==colThisInvoice.Add(decAmountOutstanding, "amountOutstanding")
                        decTotalOutstanding = 0
                        '- Compute total outst. for customer.
                        For Each colInvoice In colInvoices
                            decTotalOutstanding += colInvoice.Item("amountOutstanding")
                        Next colInvoice
                        gridRow1 = New DataGridViewRow
                        dgvCustomers.Rows.Add(gridRow1)
                        '-- maybe slow it down a bit..
                        DoEvents()
                        Thread.Sleep(10)  '=Thread.Sleep(100)  '--miiliseconds

                        rx = dgvCustomers.Rows.Count - 1  '--last row -
                        With dgvCustomers.Rows(rx)
                            .Cells(k_CUSTGRIDCOL_CHKINCLUDE).Value = 0  '-now unchecked. was 1  '--checked-
                            .Cells(k_CUSTGRIDCOL_CUSTNO).Value = colCustInfo.Item("barcode") '= row1.Item("barcode")
                            .Cells(k_CUSTGRIDCOL_CUSTOMER_NAME).Value = colCustInfo.Item("custShortName") '= row1.Item("custShortName")
                            .Cells(k_CUSTGRIDCOL_OUTST).Value = FormatCurrency(decTotalOutstanding, 2)
                            .Cells(k_CUSTGRIDCOL_EMAILADDRESS).Value = colCustInfo.Item("email") '= row1.Item("email")
                            If (colCustInfo.Item("email") = "") Or _
                                     chkNoEmail.Checked Or (mIntRequestedCustomer_id > 0) Then '-no email- or Show Only..
                                .Cells(k_CUSTGRIDCOL_CHKPRINT).Value = 1
                                .Cells(k_CUSTGRIDCOL_CHKEMAIL).Value = 0
                            Else  '-have email-
                                .Cells(k_CUSTGRIDCOL_CHKPRINT).Value = 0
                                .Cells(k_CUSTGRIDCOL_CHKEMAIL).Value = 1
                            End If
                            .Cells(k_CUSTGRIDCOL_RESULT).Value = ""
                            .Cells(k_CUSTGRIDCOL_INVOICES).Value = colInvoices
                            '- save cust. info in grid row.
                            .Cells(k_CUSTGRIDCOL_CUSTINFO).Value = colCustInfo
                        End With  '-dgvCustomers.Rows(rx)-
                        intLoaded += 1
                    End If  '-invoices nothing.
                Next  '-colReportCust-
                Call mWaitFormOff()
                mbReloadCustomersEx = True
            Else
                '-nothing.-
            End If  '--nothing
        Else
            '-get info failed..-
        End If
        labLoadedCount.Text = Format(intLoaded, "####0")
        panelOptions.Enabled = True
        dgvCustomers.Enabled = True
        btnPrintSelection.Enabled = True
        btnExecute.Enabled = True
        panelAction.Enabled = True
        panelSelection.Enabled = True

        dgvCustomers.ResumeLayout()
        dgvCustomers.Enabled = True

        labStatus.Text = "Done.."
        mbIsLoading = False

    End Function '-mbReloadCustomersEx-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '--load-
    '--load-

    Private Sub frmStatements_Load(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) Handles MyBase.Load

        Dim colSystemInfo As Collection
        Dim s1 As String
        Dim clsWinInfo1 As New clsWinSpecial '=3403.711=

        panelOptions.Enabled = False
        '= MsgBox("Load Starting..")

        '= labDLLversion.Text = msVersionPOS
        '= labToday.Text = Format(Today, "ddd dd-MMM-yyyy")

        labLoadedCount.Text = ""
        labIncludedCount.Text = ""
        '== labProgressCount.Text = ""

        btnPrintSelection.Enabled = False
        '= btnPreviewSelection.Enabled = False
        btnExecute.Enabled = False
        btnDeselectAll.Enabled = False
        btnSelectAll.Enabled = False

        '== txtDays.Text = "30"

        cboDaysToShow.Enabled = False
        '- Make days a combo..  30, 60, 90.. 
        cboDaysToShow.Items.Clear()
        cboDaysToShow.Items.Add("0")
        cboDaysToShow.Items.Add("30")
        cboDaysToShow.Items.Add("60")
        cboDaysToShow.Items.Add("90")
        cboDaysToShow.Items.Add("120")
        cboDaysToShow.SelectedIndex = 0  '-default ZERO days- (3519.0311)

        '= Call CenterForm(Me)

        '-- get system Info table data.-
        '=3301.428=
        mSysInfo1 = New clsSystemInfo(mCnnSql)

        '=3301.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        msEmailTextStatement = mSysInfo1.item("POS_EMAILTEXTSTATEMENT")
        '=3301.428= End If  '-load sys info-- 

        '=3403.1009- Server Share Path for Email Queue.
        msEmailQueueSharePath = mSysInfo1.item("POS_EMAILQUEUE_SHAREPATH")


        '==3301.428= Local Settings-
        msSettingsPath = gsLocalSettingsPath() '= default Jobmatix33=
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        '-- set up paths..
        '=3403.711=
        msAppPath = clsWinInfo1.ProgramDir  '= My.Application.Info.DirectoryPath
        If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
        msAppFullname = msAppPath & My.Application.Info.AssemblyName
        If (VB.Right(LCase(msAppFullname), 4) <> ".exe") Then
            msAppFullname &= ".exe"
        End If
        '==-test-
        '== MsgBox("msAppFullname is :  " & msAppFullname, MsgBoxStyle.Information)
        '= labAppFullName.Text = msAppFullname

        '-- set up output pdf file directory..
        '== s1 = "c:\users\public"
        s1 = gsJobMatixLocalDataDir()  '== clsWinSpec1.AppDataDir
        '= If My.Computer.FileSystem.DirectoryExists(s1) Then
        msStatementFilePath = gsGetPDF_file_path()  '== s1 & "\AllDocuments"
        '= Else  '--use app dir..
        '= msStatementFilePath = msAppPath & msStatementFilePath
        '= End If  '-- exists-
        If Not My.Computer.FileSystem.DirectoryExists(msStatementFilePath) Then  '-must create..-
            My.Computer.FileSystem.CreateDirectory(msStatementFilePath)
        End If '-- exists statement dir.-
        '= msStatementFilePath &= "\"

        '-- TEMP--  No Emailing yet..-
        '-- TEMP--  No Emailing yet..-
        '-- TEMP--  No Emailing yet..- 
        '---    Until mSysInfo1.item("POS_ALLOW_EMAIL_STATEMENTS") =Y"

        '=3107.906= ok can save to email queue (docArchive table).
        chkNoEmail.Checked = True  '- No emailing-
        chkNoEmail.Enabled = False
        dgvCustomers.Columns("chkEmail").ReadOnly = True
        If mSysInfo1.contains("POS_ALLOW_EMAIL_STATEMENTS") Then
            If (UCase(mSysInfo1.item("POS_ALLOW_EMAIL_STATEMENTS")) = "Y") Then
                chkNoEmail.Checked = False   '= Yes, do emailing.-
                dgvCustomers.Columns("chkEmail").ReadOnly = False
            End If
        End If
        '= MsgBox("Form load done..", MsgBoxStyle.Information)

        btnPrintSelection.Enabled = False   '=3519.0224=--disable for now..
        labStatus.Text = "Starting.."
        optDebtorsReportSummary.Checked = True

        '- 4201.0508=
        '-- Stuff from SHOWN..
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim sName As String

        '= If mbActivated Then Exit Sub
        '= mbActivated = True

        '- get printers collection and set up combos.
        cboStatementPrinters.Items.Clear()
        labStatus.Text = "Getting Available Printers.."
        Me.BringToFront()
        DoEvents()
        Thread.Sleep(300)  '-get form visible..-

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboStatementPrinters.Items.Add(sName)
                '== See below for PDF capture..
                '    If (InStr(LCase(sName), "adobe pdf") > 0) Or _
                '          ((InStr(LCase(sName), "microsoft") > 0) And (InStr(LCase(sName), "pdf") > 0)) Then
                '        msPdfPrinterName = sName  '-save PDF printer name--
                '    End If
            Next sName
            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_statementPrtSettingKey) AndAlso _
                     (mLocalSettings1.item(k_statementPrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_statementPrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_statementPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboStatementPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboStatementPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboStatementPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
        End If '-getAvail.-   

        '=3411.0110=  Get PDF prefrred printer...
        '---(Microsoft will be preferred)..
        If Not gsGetPdfPrinterName(msPdfPrinterName) Then
            MsgBox("Error- Failed to look-up pdf printer..", MsgBoxStyle.Exclamation)
        End If  '-get-

        labPdfPrinter.Text = msPdfPrinterName
        '-- but use PDF ANYWAY..
        '-- but use PDF ANYWAY..
        '-- but use PDF ANYWAY..
        '== If (msPdfPrinterName <> "") Then  '-Enforce PDF for Statements-
        '== cboStatementPrinters.SelectedItem = msPdfPrinterName
        '== msStatementPrinterName = msPdfPrinterName
        '== cboStatementPrinters.Enabled = False
        '== End If

        '==Call mbReloadCustomers()
        '= cboDaysToShow.SelectedIndex = 0
        panelOptions.Enabled = True
        cboDaysToShow.Enabled = True

        optShowingOutst.Checked = True '- Does the first Call mbReloadCustomers()
        mbIsInitialising = False

        Call mbReloadCustomersEx()
        If (mIntRequestedCustomer_id > 0) Then
            labStatus.Text = "  Showing requested customer only.."
        End If

        btnDeselectAll.Enabled = True
        btnSelectAll.Enabled = True

        DoEvents()

    End Sub  '--load-
    '= = = = = =  == = = = 
    '-===FF->

    '--Activated-
    '--Activated-

    'Private Sub frmStatements_Activated(ByVal sender As System.Object, _
    '                          ByVal e As System.EventArgs) Handles MyBase.Activated
    '    If mbActivated Then Exit Sub
    '    mbActivated = True

    'End Sub  '--activated-
    '= = = = =  = = = = = = = =

    '--Shown-

    'Private Sub frmStatements_Shown(ByVal sender As System.Object, _
    '                              ByVal e As System.EventArgs) Handles MyBase.Shown
    'Dim colPrinters As Collection
    'Dim intDefaultPrinterIndex As Integer
    'Dim s1, sName As String

    ''= If mbActivated Then Exit Sub
    ''= mbActivated = True

    ''- get printers collection and set up combos.
    'cboStatementPrinters.Items.Clear()
    'labStatus.Text = "Getting Available Printers.."
    'Me.BringToFront()
    'DoEvents()
    'Thread.Sleep(300)  '-get form visible..-

    'If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
    '    MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
    'Else
    '    For Each sName In colPrinters
    '        cboStatementPrinters.Items.Add(sName)
    '        '== See below for PDF capture..
    '        '    If (InStr(LCase(sName), "adobe pdf") > 0) Or _
    '        '          ((InStr(LCase(sName), "microsoft") > 0) And (InStr(LCase(sName), "pdf") > 0)) Then
    '        '        msPdfPrinterName = sName  '-save PDF printer name--
    '        '    End If
    '    Next sName
    '    '-- check local settings (prefs) for printers..
    '    If mLocalSettings1.exists(k_statementPrtSettingKey) AndAlso _
    '             (mLocalSettings1.item(k_statementPrtSettingKey) <> "") Then
    '        s1 = mLocalSettings1.item(k_statementPrtSettingKey)
    '        '= gbQueryLocalSetting(gsLocalSettingsPath, k_statementPrtSettingKey, s1) AndAlso (s1 <> "") Then
    '        If colPrinters.Contains(s1) Then '--set it- 
    '            cboStatementPrinters.SelectedItem = s1
    '        Else
    '            If (msDefaultPrinterName <> "") Then cboStatementPrinters.SelectedItem = msDefaultPrinterName
    '        End If '-contains-
    '    Else  '-no prev.pref.
    '        If (msDefaultPrinterName <> "") Then cboStatementPrinters.SelectedItem = msDefaultPrinterName
    '    End If  '-query- 
    'End If '-getAvail.-   

    ''=3411.0110=  Get PDF prefrred printer...
    ''---(Microsoft will be preferred)..
    'If Not gsGetPdfPrinterName(msPdfPrinterName) Then
    '    MsgBox("Error- Failed to look-up pdf printer..", MsgBoxStyle.Exclamation)
    'End If  '-get-

    'labPdfPrinter.Text = msPdfPrinterName
    ''-- but use PDF ANYWAY..
    ''-- but use PDF ANYWAY..
    ''-- but use PDF ANYWAY..
    ''== If (msPdfPrinterName <> "") Then  '-Enforce PDF for Statements-
    ''== cboStatementPrinters.SelectedItem = msPdfPrinterName
    ''== msStatementPrinterName = msPdfPrinterName
    ''== cboStatementPrinters.Enabled = False
    ''== End If

    ''==Call mbReloadCustomers()
    ''= cboDaysToShow.SelectedIndex = 0
    'grpBoxOptions.Enabled = True
    'cboDaysToShow.Enabled = True

    'optShowingOutst.Checked = True '- Does the first Call mbReloadCustomers()
    'mbIsInitialising = False

    'Call mbReloadCustomers()
    'If (mIntRequestedCustomer_id > 0) Then
    '    labStatus.Text = "  Showing requested customer only.."
    'End If

    'btnDeselectAll.Enabled = True
    'btnSelectAll.Enabled = True

    'DoEvents()

    '= End Sub  '--SHOWN-
    '= = = = = = = = = = = 
    '-===FF->

    '-cboStatementPrinters-

    Private Sub cboStatementPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                                       ByVal e As System.EventArgs) _
                                       Handles cboStatementPrinters.SelectedIndexChanged
        If (cboStatementPrinters.SelectedIndex >= 0) Then
            msStatementPrinterName = cboStatementPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_statementPrtSettingKey, msStatementPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_statementPrtSettingKey, msStatementPrinterName) Then
                MsgBox("Failed to save Statement printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-

    End Sub  '-cboStatementPrinters-
    '= = = = = =  = = = = = = == = =


    '-cboDaysToShow-

    Private Sub cboDaysToShow_SelectedIndexChanged(ByVal sender As System.Object, _
                                                        ByVal e As System.EventArgs) _
                                                        Handles cboDaysToShow.SelectedIndexChanged
        If mbIsInitialising Then Exit Sub


    End Sub  '-cboDaysToShow-
    '= = = = = = = = = = = = =
    '-===FF->

    '--optShowingOutst_CheckedChanged-

    Private Sub optShowingOutst_CheckedChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) _
                                                 Handles optShowingOutst.CheckedChanged, optShowingAll.CheckedChanged
        Dim opt1 As RadioButton = CType(sender, RadioButton)
        Dim radioNameSelected As String = CType(sender, RadioButton).Name

        DoEvents()
        '= MsgBox(radioNameSelected, MsgBoxStyle.Information) '--Testing
        If opt1.Checked Then
            '== Call mbReloadCustomers()
        End If
        If optShowingOutst.Checked Then
            cboDaysToShow.Enabled = True
        Else
            cboDaysToShow.Enabled = False
        End If

    End Sub  '--optShowingOutst_CheckedChanged-
    '= = = = = = = = = = = = = = = = = = = = = =

    '-btnRefreshGrid-
    Private Sub btnRefreshGrid_Click(ByVal sender As System.Object, _
                                 ByVal e As System.EventArgs) Handles btnRefreshGrid.Click
        Call mbReloadCustomersEx()

    End Sub  '--btnRefreshGrid-
    '= = =  = = = = = = = = =  =
    '-===FF->

    '--de-select.

    Private Sub btnDeselectAll_Click(ByVal sender As System.Object, _
                                        ByVal e As System.EventArgs) Handles btnDeselectAll.Click
        For Each gridrow1 As DataGridViewRow In dgvCustomers.Rows
            gridrow1.Cells("chkInclude").Value = 0
            DoEvents()
        Next gridrow1
        labIncludedCount.Text = "0"

    End Sub  '-deselect-
    '= = = = = = = = =  ==

    '-- drop all the print candidates..

    '-btnSelectEmailOnly-

    Private Sub btnSelectEmailOnly_Click(sender As Object, e As EventArgs) Handles btnSelectEmailOnly.Click
        Dim intCount As Integer = 0
        For Each gridrow1 As DataGridViewRow In dgvCustomers.Rows
            If gridrow1.Cells("chkEmail").Value = 1 Then
                gridrow1.Cells("chkInclude").Value = 1   '--select if we can  email.
                intCount += 1
            Else
                gridrow1.Cells("chkInclude").Value = 0   '-del-select those for printing.
            End If
            DoEvents()
        Next gridrow1
        labIncludedCount.Text = CStr(intCount)

    End Sub   '- btnSelectEmailOnly'-
    '= = = = = = = = = = = ==== =

    '-- btnSelectAll-

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Dim intCount As Integer = 0

        For Each gridrow1 As DataGridViewRow In dgvCustomers.Rows
            intCount += 1
            gridrow1.Cells("chkInclude").Value = 1
            DoEvents()
        Next gridrow1
        labIncludedCount.Text = CStr(intCount)
    End Sub  '--select-
    '= = = = = = = =  = == 
    '-===FF->

    '=private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)

    '--  "chkInclude" column clicked..

    Private Sub dgvCustomers_CellContentClick(ByVal sender As Object, _
                                              ByVal ev As DataGridViewCellEventArgs) Handles dgvCustomers.CellContentClick
        If mbIsInitialising Then Exit Sub
        If mbIsLoading Then Exit Sub

        Dim lRow, lCol, rx As Integer
        Dim intData As Integer
        Dim intCount As Integer = 0

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        If lCol <> k_CUSTGRIDCOL_CHKINCLUDE Then
            Exit Sub   '-not the column we want.
        End If
        '--  count rows selected..
        DoEvents()
        rx = 0
        For Each gridrow1 As DataGridViewRow In dgvCustomers.Rows
            '= intData = CInt(gridrow1.Cells("chkInclude").Value)
            If rx = lRow Then
                '--currentrow clicked 
                intData = IIf(gridrow1.Cells(k_CUSTGRIDCOL_CHKINCLUDE).EditedFormattedValue = True, 1, 0)
            Else '-check other row-
                intData = CInt(gridrow1.Cells("chkInclude").Value)
            End If
            If (intData <> 0) Then  '= gridrow1.Cells("chkInclude").Value = 1 Then
                intCount += 1
            End If
            DoEvents()
            rx += 1
        Next gridrow1
        labIncludedCount.Text = CStr(intCount)

    End Sub '--CellContentClick--
    '= = = = = = = = = = = = === =

    ''==3519.0224-
    '-- TEMP DISABLED-

    'Private Sub dgvCustomers_SelectionChanged(ByVal sender As Object, _
    '                                          ByVal ev As EventArgs) Handles dgvCustomers.SelectionChanged

    '    '= btnPreviewSelection.Enabled = True
    '    If mbIsLoading Or mbIsInitialising Then Exit Sub
    '    If (dgvCustomers.SelectedRows.Count > 0) Then
    '        btnPrintSelection.Enabled = True
    '    End If
    'End Sub  '-SelectionChanged -
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Save PDF for email queue --

    '==   -- 4201.1027/1030/1031.  31-Oct-2019-  Started 27-Oct-2019-
    '==      -- STATEMENTS-- For Emailing, address customer as << FirstName LastName >>...


    Private Function mbSaveEmailStatement(ByVal sCustBarcode As String, _
                                           ByVal sCustEmail As String, _
                                           ByRef colCustomerInfo As Collection, _
                                           ByRef colInvoices As Collection, _
                                           ByRef colRefunds As Collection, _
                                           ByRef sSavedFileFullName As String) As Boolean
        Dim sPrintFileFullName, sFileTitle As String
        Dim clsPrint1 As New clsPrintSaleDocs
        Dim sCustomerName, sSubject As String
        Dim sFirstName, sLastName, sCompanyName As String
        Dim sEmailText As String = msEmailTextStatement

        mbSaveEmailStatement = False
        sFileTitle = "Statement-" & Format(DTPickerCutoff.Value, "dd-MMM-yyyy") & "_" & _
                                               "Cust-" & Trim(sCustBarcode) & ".pdf"
        '= sCustomerName = colCustomerInfo.Item("custShortName")
        sFirstName = colCustomerInfo.Item("firstName")
        sLastName = colCustomerInfo.Item("lastName")
        sCompanyName = colCustomerInfo.Item("companyName")

        '==  For Emailing, address customer as << FirstName LastName >>...
        sCustomerName = Trim(sCompanyName)
        If (sCustomerName = "") Then
            sCustomerName = sFirstName & " " & sLastName
        End If

        '-- set registry key for Adobe pdf writer..
        sPrintFileFullName = msStatementFilePath & "\" & sFileTitle
        '-- delete old file if exists.
        If My.Computer.FileSystem.FileExists(sPrintFileFullName) Then
            Try
                My.Computer.FileSystem.DeleteFile(sPrintFileFullName)
            Catch ex As Exception
                MsgBox("Failed to delete old file: " & sPrintFileFullName & vbCrLf & ex.Message)
            End Try
        End If

        '=3411.0109= Check if Microsoft PDF or Adobe.
        If (InStr(LCase(msPdfPrinterName), "adobe") > 0) Then
            If Not mbSetAdobeFileName(sPrintFileFullName) Then
                Exit Function
            End If
        Else  '-ok.. s/be microsoft pdf-
        End If

        '- this will  be blank if not pdf..
        clsPrint1.PrintToFileFullPath = sPrintFileFullName

        '== MsgBox("Ready to print PDF..", MsgBoxStyle.Information)
        '- Make PDF first..
        '== Wait for Print Completion-
        '== Wait for Print Completion-
        '== Wait for Print Completion-
        If Not clsPrint1.PrintStatement(colInvoices, colRefunds, colCustomerInfo, DTPickerCutoff.Value, _
                                              mSysInfo1, mImageUserLogo, msVersionPOS, _
                                                  msPdfPrinterName, , True) Then
            '-- coulbe adobe funny fonts error.
            Me.BringToFront()
            MsgBox("Print may have Failed..")
        Else  '- was ok.
            Me.BringToFront()
        End If
        '--3411.1117- WAIT for PDF to be made..-
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'While Not clsPrint1.PrintingCompleted
        '    DoEvents()
        '    Thread.Sleep(500)  '--milliseconds..-
        'End While
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '- wait for File to appear..
        '=3411.0110=- wait for File to appear..
        'Dim intStart, intFinish As Integer
        'intStart = CInt(VB.Timer)
        'intFinish = intStart + 20
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'While Not My.Computer.FileSystem.FileExists(sPrintFileFullName) And (CInt(VB.Timer) < intFinish)
        '    DoEvents()
        '    Thread.Sleep(500)  '--milliseconds..-
        'End While
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '-- check.-
        If Not My.Computer.FileSystem.FileExists(sPrintFileFullName) Then
            MsgBox("Error- Print file was not created..", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '-- SEND EMAIL--
        '-- save PDF in SHARED PATH..  NOT database-
        'If Not gbSaveDocumentToDB(mCnnSql, sPrintFileFullName, sFileTitle, _
        '                            "Statement as at: " & Format(DTPickerCutoff.Value, "dd-MMM-yyyy"), "PDF", _
        '                             "STATEMENT", mIntCustomer_id, -1, "Customer Statement..", sCustEmail, _
        '                                  msEmailTextStatement) Then
        sSubject = "Statement as at:" & Format(Now, "dd-MMM-yyyy hh:mm tt")
        sEmailText = Replace(sEmailText, "&&subject", "Re:" & sSubject, , , CompareMethod.Text)
        sEmailText = Replace(sEmailText, "&&greeting", "Dear " & sCustomerName, , , CompareMethod.Text)
        sEmailText = Replace(sEmailText, "&&BusinessName", msBusinessName, , , CompareMethod.Text)
        If Not gbSaveDocumentToEmailQueue(mCnnSql, sPrintFileFullName, sFileTitle, "PDF", _
                                         "STATEMENT", mIntCustomer_id, -1, -1, _
                                         sSubject, sCustomerName, _
                                          sCustEmail, _
                                          sEmailText, msEmailQueueSharePath) Then
            MsgBox("ERROR: Save PDF file to Queue has failed..", MsgBoxStyle.Exclamation)
        Else  '-  ók=
            mbSaveEmailStatement = True
            sSavedFileFullName = sPrintFileFullName  'result-
            '=MsgBox("Pls Note- The Statement PDF file: " & vbCrLf & sPrintFileFullName & vbCrLf & _
            '=       " has been saved and queued for emailing.", MsgBoxStyle.Information)
        End If  '-save-

    End Function  '-save email statement-
    '= = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- print Selected Customer Statement.--

    Private Sub btnPrintSelection_Click(ByVal sender As System.Object, _
                                           ByVal e As System.EventArgs) Handles btnPrintSelection.Click
        Dim colInvoices As Collection
        Dim colRefunds As Collection
        Dim colCustomerInfo, colReportCust As Collection
        Dim dgvRow1 As DataGridViewRow
        Dim clsPrint1 As clsPrintSaleDocs
        '= Dim sPrintFileFullName, sFileTitle As String
        Dim sCustBarcode, sCustEmail As String
        Dim bCanPrint As Boolean = False
        Dim bCanEmail As Boolean = False
        Dim bDoNotEmail As Boolean = False
        Dim sPrintFileFullName As String  '-save result-
        Dim customer_id As Integer

        panelOptions.Enabled = False

        '-- get selected row for Customer and Invoice info..

        If (dgvCustomers.SelectedRows.Count > 0) Then
            clsPrint1 = New clsPrintSaleDocs
            dgvRow1 = dgvCustomers.SelectedRows(0)
            colInvoices = dgvRow1.Cells(k_CUSTGRIDCOL_INVOICES).Value
            colCustomerInfo = dgvRow1.Cells(k_CUSTGRIDCOL_CUSTINFO).Value
            customer_id = colCustomerInfo.Item("customer_id")
            colReportCust = mColReportCustomers.Item(CStr(customer_id))
            colRefunds = colReportCust.Item("Refunds")

            sCustBarcode = colCustomerInfo.Item("barcode")
            sCustEmail = colCustomerInfo.Item("email")
            bDoNotEmail = (colCustomerInfo.Item("doNotEmailDocuments") <> 0)
            bCanPrint = (dgvRow1.Cells(k_CUSTGRIDCOL_CHKPRINT).Value <> 0)
            bCanEmail = (dgvRow1.Cells(k_CUSTGRIDCOL_CHKEMAIL).Value <> 0) And (Not bDoNotEmail)

            If bCanPrint Then
                '- hard copy-
                Dim bPreviewOnly As Boolean = True
                Call clsPrint1.PrintStatement(colInvoices, colRefunds, colCustomerInfo, DTPickerCutoff.Value, _
                                                     mSysInfo1, mImageUserLogo, msVersionPOS, _
                                                        msStatementPrinterName, bPreviewOnly)
            End If
            '- email always creates PDF..
            If bCanEmail AndAlso (msPdfPrinterName <> "") Then
                If Not chkNoEmail.Checked Then
                    '- CALL save email file-
                    If mbSaveEmailStatement(sCustBarcode, sCustEmail, colCustomerInfo, colInvoices, colRefunds, sPrintFileFullName) Then
                        MsgBox("Pls Note- The Statement PDF file: " & vbCrLf & sPrintFileFullName & vbCrLf & _
                             vbCrLf & " has been created OK, and queued for emailing.", MsgBoxStyle.Information)
                    End If  'save-
                End If  '--send- 
            End If '-- email-
        Else
            MsgBox("No row is selected!", MsgBoxStyle.Exclamation)
        End If  '--selected-
        panelOptions.Enabled = True

    End Sub  '--print selection-
    '= = = =  = = = = = = == == 
    '-===FF->

    '- Execute All--

    Private Sub btnExecute_Click(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles btnExecute.Click

        Dim colInvoices As Collection
        Dim colRefunds As Collection
        Dim colCustomerInfo, colReportCust As Collection
        Dim dgvRow1 As DataGridViewRow
        Dim clsPrint1 As clsPrintSaleDocs
        Dim sPrintFileFullName As String = ""
        Dim bCanPrint As Boolean = False
        Dim bCanEmail As Boolean = False
        Dim bDoNotEmail As Boolean = False
        Dim sCust, s1, s2, s3 As String
        Dim sCustBarcode, sCustEmail As String
        Dim colSavedFilesNames As New Collection
        Dim customer_id, intIncludeCount As Integer

        panelOptions.Enabled = False
        btnExecute.Enabled = False      '--can execute once only.-

        '-- Confirm...-
        intIncludeCount = 0
        For Each gridrow1 As DataGridViewRow In dgvCustomers.Rows
            If (gridrow1.Cells(k_CUSTGRIDCOL_CHKINCLUDE).Value <> 0) Then  '-included-
                intIncludeCount += 1
            End If
        Next gridrow1
        If (intIncludeCount > 0) Then
            If MsgBox("Execute all " & intIncludeCount & " Statements ?", _
                          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                btnExecute.Enabled = True
                Exit Sub
            End If
        End If

        clsPrint1 = New clsPrintSaleDocs
        '-- do all selected statements.-
        For Each dgvRow1 In dgvCustomers.Rows
            '=k_CUSTGRIDCOL_CHKINCLUDE()
            labStatus.Text = "Checking Row: " & dgvRow1.Index
            If (dgvRow1.Cells(k_CUSTGRIDCOL_CHKINCLUDE).Value <> 0) Then  '-included-
                colInvoices = dgvRow1.Cells(k_CUSTGRIDCOL_INVOICES).Value
                colCustomerInfo = dgvRow1.Cells(k_CUSTGRIDCOL_CUSTINFO).Value
                customer_id = colCustomerInfo.Item("customer_id")
                colReportCust = mColReportCustomers.Item(CStr(customer_id))
                colRefunds = colReportCust.Item("Refunds")

                bCanPrint = (dgvRow1.Cells(k_CUSTGRIDCOL_CHKPRINT).Value <> 0)
                bCanEmail = (dgvRow1.Cells(k_CUSTGRIDCOL_CHKEMAIL).Value <> 0)
                '- customer-
                bDoNotEmail = (colCustomerInfo.Item("doNotEmailDocuments") <> 0)
                s1 = colCustomerInfo.Item("firstName")
                s2 = colCustomerInfo.Item("lastName")
                s3 = Replace(colCustomerInfo.Item("companyName"), " ", "") '-delete spaces-
                sCust = VB.Left(s3 & "-" & s1 & "-" & s2, 8)
                sCustBarcode = colCustomerInfo.Item("barcode")
                sCustEmail = colCustomerInfo.Item("email")
                If bCanPrint Then
                    '- hard copy-
                    labStatus.Text = "Printing statement for: " & colCustomerInfo.Item("custShortName")
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

                    '== If (LCase(msStatementPrinterName) = "adobe pdf") Then
                    '== '-- set registry key for Adobe pdf writer..
                    '== If Not mbSetAdobeFileName(sPrintFileFullName) Then
                    '== Exit Sub
                    '== End If
                    '== End If  '-adobe-
                    '=3107.906= -- JUST now print to whereever..
                    Call clsPrint1.PrintStatement(colInvoices, colRefunds, colCustomerInfo, DTPickerCutoff.Value, _
                                                         mSysInfo1, mImageUserLogo, msVersionPOS, _
                                                            msStatementPrinterName, False, False)
                    labStatus.Text = "Waiting for Printer.. "
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                End If
                labStatus.Text = "Print done."

                '- email always creates PDF first..
                If (bCanEmail And (Not bDoNotEmail)) AndAlso (msPdfPrinterName <> "") Then
                    '-- make unique file name for customer.-
                    '==sPrintFileFullName = msStatementFilePath & "\Statement_" & sCust & "-" & sCustBarcode & ".pdf"
                    '-- set registry key for Adobe pdf writer..
                    '== If Not mbSetAdobeFileName(sPrintFileFullName) Then
                    '== Exit Sub
                    '==End If
                    ''== == MsgBox("Ready to print PDF..", MsgBoxStyle.Information)
                    '== Call clsPrint1.PrintStatement(colInvoices, colCustomerInfo, DTPickerCutoff.Value, _
                    '==                                       mSdSystemInfo, mImageUserLogo, msVersionPOS, _
                    '==                                           msPdfPrinterName)
                    '-- SEND EMAIL--
                    If (Not chkNoEmail.Checked) Then
                        labStatus.Text = "Saving Email statement for: " & colCustomerInfo.Item("custShortName")
                        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                        '- CALL save email file-
                        If mbSaveEmailStatement(sCustBarcode, sCustEmail, _
                                                    colCustomerInfo, colInvoices, colRefunds, sPrintFileFullName) Then
                            labStatus.Text = "Waiting for pdf file.. "
                            colSavedFilesNames.Add(sPrintFileFullName)
                            labStatus.Text = "Done."
                            '== MsgBox("Pls Note- The Statement PDF file: " & vbCrLf & sPrintFileFullName & vbCrLf & _
                            '==       " has been saved and queued for emailing.", MsgBoxStyle.Information)
                            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                        End If  'save-
                    End If  '--send-
                End If '-- email-
            End If  '--included.-
        Next  '--dgvRow1-
        '- show all file names ?-
        If (colSavedFilesNames.Count > 0) Then
            MsgBox("ok.  Saved " & colSavedFilesNames.Count & " Statement Files for emailing.", MsgBoxStyle.Information)
        End If  '-count-
        labStatus.Text = "Done this batch."

    End Sub  '--execute-
    '= = = = =  = == = == = 
    '-===FF->

    '-- Debtors Report--
    '-- Debtors Report--
    '-- Debtors Report--

    '--Print Report to 'msStatementPrinterName' ..--

    Private Sub btnDebtorsReport_Click(ByVal sender As System.Object, _
                                          ByVal e As System.EventArgs) Handles btnDebtorsReport.Click
        Dim clsPrint1 As clsPrintSaleDocs
        Dim sPrintFileFullName As String = ""
        Dim bPrintSummaryOnly As Boolean = (optDebtorsReportSummary.Checked = True)
        '=4201.1112-
        Dim bPreviewOnly As Boolean = True

        If (Not (mColReportCustomers Is Nothing)) AndAlso (mColReportCustomers.Count > 0) Then
            clsPrint1 = New clsPrintSaleDocs

            '-- now print to whereever..
            Call clsPrint1.PrintDebtorsReport(mColReportCustomers, DTPickerCutoff.Value, _
                                                 mSysInfo1, mImageUserLogo, msVersionPOS, _
                                                    msStatementPrinterName, bPrintSummaryOnly, bPreviewOnly)
        Else

        End If  '--nothing

    End Sub  '--btnDebtorsReport--
    '= = = = = = = = = = = = = = = 

    '-- Close..
    Private Sub close_me()

        '- inform parent.-
        '- Report to Parent..-
        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

        'If Not bCancel Then  '--exiting.
        '    If Not (Me.delReport Is Nothing) Then
        '        delReport.Invoke(Me.Name, "FormClosed", "")
        '    End If
        '    'End If  '-cancel-
        '    Me.Dispose()
    End Sub '--close me-
    '= = = = = = == = = = =

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        '= Call SubFormCloseRequest()

        If mbIsLoading Then
            MsgBox("Can't close this panel while loading grid !", MsgBoxStyle.Exclamation)
            Exit Sub
        Else  '-ok-
            Call close_me()
        End If

    End Sub '-close-
    '= = = = = = = = = = = =

End Class  '-frmStatements-
'= = = = = = = = = = = = = =

'== end form ==