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
Imports System.ComponentModel

Public Class ucChildPayments

    '-- Account Payments.-- 03-Dec-2104..

    '- grh JobMatixPOS 3.1.3101.1214--
    '--  14-Dec-2014==
    '=3301.428=  cleaned up sysinfo redundant stuff.e
    '==
    '==     v3.3.3301.602..  02-June-2016= ===
    '==       >>  Fixes to INSERT "accountpayment" records 
    '==         for Payment into Invoice table (accountPayments)...
    '==
    '==   v.3301.611-  Add accountPayment Invoice line for each invoice being paid.
    '==     and drop the "mother" payment record..
    '==
    '== = = = =
    '==
    '==     v3.3.3301.622..  22-June-2016= ===
    '==       >> Update Invoice and PAYMENTS Table Schema-  
    '==     BACK to the ORIGINAL schema-  No IP Invoice records, and PaymentDisbursement records Re-stored.
    '==         (For Account customers, part payment with sale records a separate Payment entry)
    '==
    '==     v3.3.3303.0109..  09-Jan-2017= ===
    '==       >> Add functionality to Debtors Payments to
    '==            Disburse Refund values to Outstanding Invoices...- 
    '==          NB: Refunds must be done as a  separate run (commit) from money payments.
    '==
    '==
    '==     v3.3.3307.0202..  02-Feb-2017= ===
    '==       >> Debtor Payments.. Accept Sales InvoiceNo as lookup to find Customer..
    '==                  (Shift-F2 on CustNo to Search Invoices-
    '== = = = = = = = = = = = = = =
    '==
    '==     3403.711- 11July2017-
    '==      --  Fix Commit crash-
    '==              (was looking to insert "terminal_id" into payment details..) 
    '==
    '==     3403.1015- 15-Oct-2017-
    '==      -- MAJOR SHIFT..
    '==        >> All Customers can have credit Notes (Account and non-a/c custs).
    '==        >>  For Sales to Account Cust-  only onAccount invoices will go to Debtors.
    '==             --  So account cust can have normal Cash Sales
    '==             --  'chkOnAccount' Checkbox (Charge to Account) added to sale Payments Panel. 
    '==                    (User must check this to make on-account sale..).
    '==        >>  On-Account sales can have partial Debtors payment with it..       
    '==        >>  Refunds now the same for Account Custs and non-a/c custs..   
    '==             -- So Account Payments can draw on CreditNotes for payment of invoices..  
    '==             --    and Table "paymentRefundDetails" is dropped. 
    '==        >>  DROP all references to Refunds and Table "paymentRefundDetails"
    '==        >>  CreditNote balance replaces Avail-Refunds..
    '==        >>  Overpayments can be given as Change, or saved as Crdit Note.
    '==        >> Account Payments can have discount given on invoices..  
    '==              -- Discount is saved as PaymentDisbursement row..(trancode="discount")
    '==
    '==       3411.0110=  10-Jan-2018=
    '==          --  Get PDF preferred printer...
    '==             -(Microsoft PDF will be preferred)..
    '==
    '==     Updated 3519.0321=  21-March-2019=
    '==         -- Full Text Srch (frmBrowse33) now used for Cust Srch. 
    '==                   (in mbBrowseAndSearchCustomers STOLEN from clsPOS34Sale...)..
    '==
    '==   Updated.- 3519.0414  Started 12-April-2019= 
    '==    -- SALES-  Make sure that discount/discountTax are rounded to 2 decimals.. 
    '==         AND in Account Payments
    '==                  round off invoice OUTSTANDING amount to 2 digits when lodaing grid..
    '==
    '== - - - - RELEASED as 3519.0414 --
    '==  
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==
    '==    First New Build- 4201.0416 -
    '==    New Build- 4201.0416 -  Started 16-April-2019.
    '==   Updated.- 4201.0416- 
    '==     --for TDI Admin forms in Tabs inside Main Form...
    '==    -- 4201.0426. TDI Child forms now converted to UserControls.... 
    '==    -- 4201.0509. For Show Statement, go straight to PrintStatement..... 
    '==  
    '==
    '= = == = =  = = = === 
    '==
    '== NEW revision-
    '==    -- 4201.0707.  Started 05-July-2019-
    '==       -- Add Local Preference for Re-ordering of payment Details.
    '==       -- Payments- Catch ENTER on PaymentDetails Grid (As per Sales). Load balance into Grid. .
    '==       -- On entering PayingNow Grid cell, preload with Amount owing.. 
    '==       -- Commit-Confirm dialog:  Make Email/Print options UNCHECKED to start.. 
    '== = = =
    '==
    '== NEW revision-
    '==
    '==    -- 4201.0929/930/1002.  02-Oct-2019-  -
    '==        -- Debtors Payments- Allow Reversing a Payment even when nothing is outstanding..
    '==        -- Debtors Payments- Allow user to Discount full Outst. balance of Invoice...
    '==
    '==
    '== NEW revision-
    '== NEW revision-
    '==
    '==    -- 4201.1007.  07-Oct-2019-  
    '==        -- Debtors Payments- Fix problem with CreditNote as change showing when CreditNote is Input...
    '==
    '= = = = = = = = = = = = = = == = = = = = = = = = = = = = = = = = = = = = = = =  = = =
    '==
    '==  Target is new Build 4251..
    '==  Target is new Build 4251..
    '==
    '==   In ucChildPayments-  ListBox of previous payments doesn't have/show full Paymnent-ID..
    '==                               Results in wrong payment being show when clicked on..
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==Fixes to Build 4257.0707  
    '==
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
    '==
    '==  Account Invoice Reversal- 
    '==      -- If Account payments have been made to the Invoice 
    '==           - User should be able to reverse the Account Invoice if all the Account Payments are reversed first..
    '==      -- Payments Form needs ReversedInvoices to be filtered out..
    '==           ALSO before committing payment, check that Invoices were not changed in the meantime..
    '==             (NEEDS "gbCollectCustomerInvoices" to have option to be in a Transaction.)
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == =
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '== Fixes to Build 4259.0730  
    '==
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==
    '==   Customer Account Payments-  
    '==        -- Payments Grid- (Stewart 24Aug2020) Fix problem with Payments Amount being displayed in in the 
    '==               detail descr. label column..  
    '==               (In dgvSalePaymentDetails_KeyDown, check for CurrentCell being readonly if ENTER key pressed..)
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '= = = = = = = = = = = = = = == = = = = = = = = = = = = = = = = = = = = = = = =  = = =

    Private Const k_statementPrtSettingKey As String = "POS_StatementPrinter"

    '-- INVOICES DataGridView columns.--
    Private Const k_INVGRIDCOL_INV_DATE As Short = 0
    Private Const k_INVGRIDCOL_INV_NO As Short = 1
    Private Const k_INVGRIDCOL_INV_TOTAL As Short = 2
    Private Const k_INVGRIDCOL_TAX_TOTAL As Short = 3
    Private Const k_INVGRIDCOL_PREV_PAID As Short = 4
    Private Const k_INVGRIDCOL_OUTSTANDING As Short = 5
    Private Const k_INVGRIDCOL_DISCOUNT As Short = 6
    Private Const k_INVGRIDCOL_PAYABLE As Short = 7
    Private Const k_INVGRIDCOL_PAYING_NOW As Short = 8
    Private Const k_INVGRIDCOL_NEW_BAL As Short = 9

    '-- PAYMENTS DataGridView columns.--
    Private Const k_PAYGRIDCOL_PAYMENTTYPE_DESCR As Short = 0
    Private Const k_PAYGRIDCOL_AMOUNT As Short = 1
    Private Const k_PAYGRIDCOL_PAYMENTTYPE_ID As Short = 2

    '-- grh JobMatixPOS31  v3.1.3101.1007 -
    Private mbIsInitialising As Boolean = True
    Private mbIsLoadingCustomer As Boolean = False

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
    Private msPdfPrinterName As String = ""
    Private msDefaultprinterName As String = ""

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    '= Private mlJobId As Integer = -1
    Private mColPrefsCustomer As Collection
    Private mImageUserLogo As Image
    Private mbAllowEmailInvoices As Boolean = False

    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    '=3301.428= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    '- Customer--
    Private msCustomerBarcode As String = ""
    Private mIntCustomer_id As Integer = -1
    Private mbIsAccountCust As Boolean = False
    Private msCustomerEmail As String = ""

    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '== -- Payments Form needs ReversedInvoices to be filtered out..
    Private mColAllAccountReversals As Collection
    '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)

    Private mColInvoices As Collection
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '== -- Payments Form needs ReversedInvoices to be filtered out..
    Private mColReversedInvoices As Collection
    '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)

    '- Prev.payments=
    Private mDtPrevPayments As DataTable
    '-- headers with sql types..
    Private mColSqlColumnTypes As Collection

    Private msTransactionType As String = ""

    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
    '== -- Payments Form needs ReversedInvoices to be filtered out..
    '-- Before deducting Reversed Invoices..
    Private mDecGrossTotalInvoices As Decimal
    Private mDecGrossTotalOutstanding As Decimal
    '-  After deducting Reversed Invoices
    Private mDecTotalInvoices As Decimal
    Private mDecTotalOutstanding As Decimal
    '== END   Target-New-Build 4259 -- (Started 17-Jul-2020)

    '=3403.1015=
    Private mDecCreditNoteCreditAvailable As Decimal = 0
    Private mDecCreditNoteCreditApplying As Decimal = 0   '-Credit Note amy applying today-

    '=3043.1015= Private mDecRefundApplying As Decimal  '-Refund amts applying today-
    '=3043.1015= Private mColRefundsUsed As Collection  '--refunds or partsof applied..
    '--  each sub-collection has invoice_id (refund),  amount (amount used in this payment pass).


    Private mDecAmountPayingNow As Decimal  '--Header total USER paying today-
    '= Private mDecAmountPaying As Decimal  '--Header total paying/applying today-
    Private mDecSubTotalPaying As Decimal
    Private mDecBalanceOwing As Decimal
    Private mDecTotalDiscount As Decimal
    '=4201.0707-
    Private mDecPaymentBalance As Decimal = 0

    Private mDecPaymentTotalRcvd As Decimal  '-EXcludes any incoming CreditNote if any.
    Private mDecPaymentTotalAllInputs As Decimal  '-EXcludes any incoming CreditNote if any.
    Private mDecPaymentCashRcvd As Decimal
    Private mDecChangeAsCash As Decimal

    Private mDecChangeAsCredit As Decimal
    Private mDecPaymentNettCredited As Decimal
    Private mIntCurrentPaymentTypeIndex As Integer = -1

    Private mIntLastPaymentRowNoEntered As Integer = -1  '-- track last used for Stew..
    '= = = = = = = = = = = = = = = = = = = = = = = = = =

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport

    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--sub new-
    '--sub new-

    Public Sub New(ByRef FrmParent As Form, _
                     ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                        ByRef colPrefsCustomer As Collection, _
                          ByVal sSettingsPath As String, _
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

        msSettingsPath = sSettingsPath
        msVersionPOS = sVersionPOS

        mImageUserLogo = imageUserLogo

        mIntStaff_id = intStaff_id
        msStaffName = sStaffName

    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)
    '= = = = = = = = = = = = = = = = = = = = == = 

    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                            ByVal intParentHeight As Integer)

        Me.Width = intParentWidth - 11
        Me.Height = intParentHeight  '= - 11
        '-- resize our controls..
        DoEvents()
        '-- resize main box and top panel-

        grpBoxPayment.Width = Me.Width - 7
        grpBoxPayment.Height = Me.Height - 7

        panelHdr.Left = grpBoxPayment.Width - panelHdr.Width - 3

        panelPayment.Top = grpBoxPayment.Height - panelPayment.Height - 7
        panelPayment.Left = grpBoxPayment.Width - panelPayment.Width - 7
        panelPaymentList.Top = panelPayment.Top

        dgvInvoices.Width = grpBoxPayment.Width - 11
        dgvInvoices.Height = grpBoxPayment.Height - panelHdr.Height - panelPayment.Height - 20

    End Sub '-SubFormResized
    '= = = = = = = = = = = =  === =

    '-Form Close Request-
    '-- Called from MdiMotherForm Tab-close Click..
    '- Return true if ok to Close.

    Public Function SubFormCloseRequest() As Boolean

        SubFormCloseRequest = True
        '==Me.Close()
        '= Call close_me()
    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    '-- Numeric test..

    Private Function mbIsNumeric(ByVal sInput As String) As Boolean
        mbIsNumeric = False

        If IsNumeric(sInput) Then  '--good start-
            '-  check for "+","-" that pass the isNumeric test, but fail in Sql Server. test.
            If (InStr(sInput, "+") <= 0) AndAlso (InStr(sInput, "-") <= 0) Then
                mbIsNumeric = True
            End If
        End If  '-numeric-
    End Function  '-is numeric-
    '= = = = = = = = = = =  = = = = =


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
    '-- Browse  table using --
    '--  Separate BROWSE33 FORM, (Includes TEXT SEARCH) and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    '== SPECIAL mbBrowseAndSearchTable for Customer Table.
    '==
    '==   Updated.- 3519.0317  Started 14-March-2019= 
    '==       -- Looking up Customers- make special Sql-Select for Browser 
    '==              to make a column of [lastName, firstName] in browser Grid.  

    '=3519.0321=  STOLEN from clsPOS34Sale..

    Private Function mbBrowseAndSearchCustomers(ByRef colPrefs As Collection, _
                                       ByRef sTitle As String, _
                                        ByRef sWhere As String, _
                                        ByRef colKeys As Collection, _
                                        ByRef colSelectedRow As Collection) As Boolean

        Dim frmBrowse1 As New frmBrowse  '--File: frmBrowse33 --
        Dim sSelectSql As String

        mbBrowseAndSearchCustomers = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = "Customer"  '=sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle

        '== make Special Select for Customer last/First Name combo.
        sSelectSql = " SELECT CASE WHEN (lastName='' ) THEN firstName "
        sSelectSql &= "  ELSE  lastName + ', ' + firstName "
        sSelectSql &= " END  AS CustomerName "
        '- add prefs.
        For Each sField As String In colPrefs
            'If (LCase(sField) <> "lastname") And (LCase(sField) <> "firstname") Then  '-we already have these.
            sSelectSql &= ", " & sField
            'End If
        Next sField
        sSelectSql &= " FROM dbo.customer "
        frmBrowse1.UserSelectList = sSelectSql
        '-test-
        '=  MsgBox("Select Sql is:  " & sSelectSql, MsgBoxStyle.Information)

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not (frmBrowse1.selectedRow Is Nothing) Then '= frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseAndSearchCustomers = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()

    End Function '-mbBrowseAndSearchCustomers
    '= = = = = = = = = = 
    '-===FF->


    '-- Browse Jobs or Parts table using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseTable(ByRef colPrefs As Collection, _
                                    ByRef sTitle As String, _
                                      ByRef sWhere As String, _
                                      ByRef colKeys As Collection, _
                                      ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Customer") As Boolean
        Dim frmBrowse1 As New frmBrowsePOS

        mbBrowseTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer
        '=3403.711=
        frmBrowse1.HideEditButtons = True
        frmBrowse1.lookupSelection = True

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle

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

    '-- load Payments list box.-
    '-- load Payments list box.-
    '== ACCOUNT payments only..
    '-- SAVE datatable for Reversals Lookup..

    Private Function mbLoadPaymentsList(ByVal intCustomerId As Integer) As Boolean
        '-  load list of invoices for this cust..--
        '= Dim dataTable1 As DataTable
        Dim row1 As DataRow
        '= Dim sItem1 As String
        Dim sSql, s1, sRev As String
        Dim sId As String
        Dim sDate, sAmount As String

        listPayments.Enabled = True
        listPayments.Items.Clear()

        sSql = "SELECT payment_id, payment_date, nettAmountCredited, "
        sSql &= "   transactionType, isReversal, originalPayment_id, "
        sSql &= "   staff.docket_name, invoice_id  "
        sSql &= " FROM dbo.payments  "
        sSql &= "    JOIN staff ON (staff.staff_id =payments.staff_id)  "
        sSql &= " WHERE (Customer_id=" & CStr(intCustomerId) & ") AND (transactionType LIKE '%account%')"
        sSql &= " ORDER BY payment_id DESC;"
        If Not gbGetDataTable(mCnnSql, mDtPrevPayments, sSql) Then
            MsgBox("Error in getting recordset for payments table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        Else
            If Not (mDtPrevPayments Is Nothing) Then
                Dim decAmt As Decimal
                For Each row1 In mDtPrevPayments.Rows
                    '==  Target is new Build 4251..
                    '==
                    '==  ListBox of previous payments doesn't have/show full Paymnent-ID..
                    '==       Results in wrong payment being show when clicked on..
                    '=sId = RSet(CStr(row1.Item("payment_id")), 4)
                    sId = RSet(CStr(row1.Item("payment_id")), 6)

                    sDate = LSet(Format(row1.Item("payment_date"), "dd-MMM-yy"), 10)
                    decAmt = CDec(row1.Item("nettAmountCredited"))
                    '=3403.1017- Filter out zero payment records.
                    sRev = IIf((row1.Item("isReversal") <> 0), " Rev", " +")
                    '=4201.1007--  Amount may be zero if was all DISCOUNT !!
                    'If decAmt > 0 Then
                    sAmount = RSet(FormatNumber(decAmt, 2), 10)
                    listPayments.Items.Add(sId & ": " & sDate & sAmount & sRev)
                    '= End If
                    '==3303.0111-
                    '--  Check for any REFUNDS disbursed with this payment-
                    '-- dbo.paymentRefundDetails -
                Next row1
            End If
            '- build collection money col-types for listSelect form
            mColSqlColumnTypes = New Collection
            mColSqlColumnTypes.Add("money", "nettAmountCredited")
        End If
        '- end invoices load..--
    End Function  '--mbLoadpaymentsList--
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--show/print Payment/receipt-

    Private Function mbShowPayment(ByVal intPayment_Id As Integer, _
                                   Optional ByVal sSelectedReceiptPrinterName As String = "", _
                                   Optional ByVal bCaptureReceiptPDF As Boolean = False, _
                                   Optional ByVal bCanPrintReceipt As Boolean = True) As Boolean
        Dim frmShowPayment1 As frmShowPayment

        frmShowPayment1 = New frmShowPayment
        frmShowPayment1.connectionSql = mCnnSql
        frmShowPayment1.sqlDbname = msSqlDbName
        frmShowPayment1.PaymentNo = intPayment_Id
        '== frmShowPayment1.Settings = mSdSettings
        '== frmShowInvoice1.SystemInfo = mSdSystemInfo
        frmShowPayment1.UserLogo = mImageUserLogo
        frmShowPayment1.versionPOS = msVersionPOS
        frmShowPayment1.selectedReceiptPrinterName = sSelectedReceiptPrinterName
        frmShowPayment1.CaptureReceiptPDF = bCaptureReceiptPDF
        frmShowPayment1.CanPrintReceipt = bCanPrintReceipt
        '-- current staff_id and msStaffName-
        frmShowPayment1.Staff_id = mIntStaff_id
        frmShowPayment1.StaffName = msStaffName

        frmShowPayment1.ShowDialog()

    End Function  '--show payment receipt..-
    '= = = = = =  = = = = = = ==  == = = ==
    '-===FF->

    '-- clear Payment..

    Private Function mbClearTransaction() As Boolean

        btnNewPayment.Enabled = False
        btnReverse.Enabled = False
        btnStatement.Enabled = False
        '= mDecAmountPaying = 0
        mDecBalanceOwing = 0
        '= txtAmtPaying.Text = ""
        txtBalanceOwing.Text = ""
        txtInitialOwing.Text = ""
        txtDiscountAllowed.Text = ""
        txtSubTotal.Text = ""

        txtCreditNotesApplied.Text = ""
        txtCreditNotesApplied.Enabled = False

        labTotalApplying.Text = ""

        labCreditNoteAvail.Text = ""
        LabCreditAvail.Text = ""

        btnCommit.Enabled = False
        mIntCurrentPaymentTypeIndex = -1

        mDecTotalInvoices = 0
        mDecTotalOutstanding = 0
        mDecPaymentTotalAllInputs = 0
        mDecTotalDiscount = 0

        mDecSubTotalPaying = 0

        dgvInvoices.Rows.Clear()
        dgvInvoices.Enabled = False

        '-- clear payments.-
        dgvPaymentDetails.Enabled = False
        dgvPaymentDetails.ClearSelection()
        For Each row1 As DataGridViewRow In dgvPaymentDetails.Rows
            row1.Cells(k_PAYGRIDCOL_AMOUNT).Value = "0.00"
        Next
        '=4201.0707=
        dgvPaymentDetails.CurrentCell = dgvPaymentDetails.Rows(0).Cells(1)  '-1st amount.

        LabSalePayments.Text = "-- This Payment Details --"
        '= panelPaymentHdr.Enabled = False

        txtPaymentBalance.Text = ""
        LabPaymentBalance.Text = "Balance:"

        txtComments.Text = ""
        txtChange.Text = ""
        '= btnCustomerInfo.Enabled = False

        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
        '= labReversedInvoices.Text = "Reversed Invoices"
        ToolTip1.SetToolTip(labReversedInvoices, "")
        labReversedInvoices.Text = ""
        '== END  Target-New-Build-4259 -- (Started 17-Jul-2020)


        labHelp.ForeColor = Color.Blue
        labHelp.Text = "Select customer."

    End Function  '--ClearTrans -
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-  mbUpdateTransactionTotal --
    '-  mbUpdateTransactionTotal --
    '-- Updated 3303.0111 (11Jan2017) for Applying Refund values to Outstanding Invoices-
    '=  -- 4201.0929/930/1002.  02-Oct-2019-  -
    '==        -- Debtors Payments- Allow Reversing a Payment even when nothing is outstanding..
    '==        -- Debtors Payments- Allow user to Discount full Outst. balance of Invoice...


    Private Function mbUpdateTransactionTotal() As Boolean
        Dim row1 As DataGridViewRow
        Dim decAmount, decAmount2, decDiscount As Decimal
        Dim decOver As Decimal
        Dim s1 As String
        '== Dim intCents1, intCentsRounding As Integer

        mDecSubTotalPaying = 0  '--Disbursements now-
        mDecBalanceOwing = 0
        mDecTotalDiscount = 0
        mDecPaymentBalance = 0

        '= mDecTotalTax = 0
        '== dgvPaymentDetails.Enabled = False
        btnCommit.Enabled = False

        '==  get all paying-now invoice contributions..
        For Each row1 In dgvInvoices.Rows
            decDiscount = 0
            If (Trim(row1.Cells(k_INVGRIDCOL_DISCOUNT).Value) <> "") Then
                decDiscount = CDec(row1.Cells(k_INVGRIDCOL_DISCOUNT).Value)
                mDecTotalDiscount += decDiscount
            End If
            If (Trim(row1.Cells(k_INVGRIDCOL_PAYING_NOW).Value) <> "") Then
                decAmount = CDec(row1.Cells(k_INVGRIDCOL_PAYING_NOW).Value)
                mDecSubTotalPaying += decAmount
            End If
            decAmount2 = CDec(row1.Cells(k_INVGRIDCOL_NEW_BAL).Value)
            mDecBalanceOwing += decAmount2
        Next row1  '-invoices-

        '--TEST==
        '== txtComments.Text &= "UpdTr- SubTotal: " & CStr(mDecSubTotalPaying) & vbCrLf

        txtDiscountAllowed.Text = FormatCurrency(mDecTotalDiscount, 2)
        txtSubTotal.Text = FormatCurrency(mDecSubTotalPaying, 2)
        txtBalanceOwing.Text = FormatCurrency(mDecBalanceOwing, 2)

        'If (mDecSubTotalPaying <> mDecAmountPaying) Then
        '    labHelp.Text = "Invoice line Pay Amounts need to balance to amount paying."
        '    labHelp.ForeColor = Color.Red
        'Else
        '    labHelp.ForeColor = Color.Blue
        '    labHelp.Text = "Adjust details for invoices if needed-" & vbCrLf & " And Enter payment details.."
        '    '== dgvPaymentDetails.Enabled = True
        'End If
        labHelp.ForeColor = Color.Blue
        labHelp.Text = "Enter amount paying for each invoice as needed..  " & vbCrLf & " Use TAB to Enter payment details.."

        '--  get actual payment type details.-
        '--dvg payments--
        mDecPaymentTotalRcvd = 0  '= excludes any mDecCreditNoteCreditApplying-- 
        mDecPaymentTotalAllInputs = 0
        mDecPaymentCashRcvd = 0
        mDecChangeAsCash = 0
        mDecChangeAsCredit = 0
        mDecPaymentNettCredited = 0

        For Each row1 In dgvPaymentDetails.Rows
            '== dgvPaymentDetails.Rows(rx).Cells(k_PAYGRIDCOL_AMOUNT).Value()
            decAmount = CDec(row1.Cells(k_PAYGRIDCOL_AMOUNT).Value)
            mDecPaymentTotalRcvd += decAmount
            s1 = row1.Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value
            If (InStr(LCase(s1), "cash") > 0) Then
                mDecPaymentCashRcvd += decAmount
            End If
        Next  '-row--
        LabSalePayments.Text = "-- Pay Detail Total: " & FormatCurrency(mDecPaymentTotalRcvd, 2)
        mDecPaymentTotalAllInputs = mDecPaymentTotalRcvd + mDecCreditNoteCreditApplying

        grpBoxRefundType.Enabled = False
        '- grpBoxRefundType and -
        '--  optRefundCash/CreditNote- decides on overpayment.
        labTotalApplying.Text = "Total Payment: " & vbCrLf & FormatCurrency(mDecPaymentTotalAllInputs, 2)

        labChange.Enabled = False
        labChange.Text = "Change:"
        txtChange.Text = "0"

        decOver = (mDecPaymentTotalAllInputs - mDecSubTotalPaying)  '-3303.0111
        If ((decOver > 0) And (mDecCreditNoteCreditApplying > 0)) Then
            labHelp.Text = "Payment is over-paid.." & vbCrLf & _
                                 "Not permitted with CreditNote input.."
            'mDecChangeAsCredit = decOver
            'labChange.Enabled = True
            'labChange.Text = "CreditNote:"
            'txtChange.Text = FormatCurrency(mDecChangeAsCredit, 2)
            'mDecPaymentNettCredited = mDecPaymentTotalAllInputs - mDecChangeAsCredit
        ElseIf (decOver > 0) And (mDecPaymentCashRcvd < decOver) Then
            labHelp.Text = "Payment is over-paid.." & vbCrLf & _
                                "Balance will be saved as CreditNote.."
            mDecChangeAsCredit = decOver
            labChange.Enabled = True
            labChange.Text = "CreditNote:"
            txtChange.Text = FormatCurrency(mDecChangeAsCredit, 2)
            mDecPaymentNettCredited = mDecPaymentTotalAllInputs - mDecChangeAsCredit
        ElseIf (decOver > 0) And (mDecPaymentCashRcvd >= decOver) Then '= can be cash change.
            '=4201.1002.. ElseIf (decOver > 0) And (mDecPaymentCashRcvd > decOver) Then '= can be cash change.
            '-- can choose changeCash/credit-
            grpBoxRefundType.Enabled = True
            If optRefundCash.Checked Then
                mDecChangeAsCash = decOver
                labChange.Enabled = True
                labChange.Text = "Change:"
                txtChange.Text = FormatCurrency(mDecChangeAsCash, 2)
                mDecPaymentNettCredited = mDecPaymentTotalAllInputs - mDecChangeAsCash
            Else '-overpay. saved as credit note.
                mDecChangeAsCredit = decOver
                labChange.Enabled = True
                labChange.Text = "CreditNote:"
                txtChange.Text = FormatCurrency(mDecChangeAsCredit, 2)
                mDecPaymentNettCredited = mDecPaymentTotalAllInputs - mDecChangeAsCredit
            End If '-cash/credit-
        ElseIf (decOver <= 0) Then
            mDecPaymentNettCredited = mDecPaymentTotalAllInputs
        Else
            MsgBox("Error- Undefined balance condition..", MsgBoxStyle.Exclamation)
        End If  '-decOver-
        '==mDecPaymentBalance = decOver
        '=4201.1007- mDecPaymentBalance is amount still to be paid//
        '--   to cover mDecSubTotalPaying (what the user/cust intends to pay.)
        mDecPaymentBalance = mDecSubTotalPaying - mDecPaymentTotalAllInputs
        '-- THIS will be negative if it is OVERPAID..

        txtPaymentBalance.Text = _
                  FormatCurrency(decOver, 2)
        If (decOver = 0) Then
            LabPaymentBalance.Text = "Balanced"
        ElseIf (decOver < 0) Then
            LabPaymentBalance.Text = "Still needed:"
        Else
            LabPaymentBalance.Text = "Overpaid"
        End If
        '-mDecPaymentTotalRcvd- is sum of paying now dgv details-
        '==  -- Debtors Payments- Allow user to Discount full Outst. balance of Invoice...
        If (((mDecSubTotalPaying > 0) Or (mDecTotalDiscount > 0)) AndAlso _
                        (mDecPaymentTotalAllInputs >= mDecSubTotalPaying)) And _
                        (Not ((decOver > 0) And (mDecCreditNoteCreditApplying > 0))) Then
            btnCommit.Enabled = True
        End If
    End Function '-  mbUpdateTransactionTotal --
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Setup Account Customer Detail-
    '==    -- 4201.0929/930/1002.  02-Oct-2019-  -
    '==    -- Debtors Payments- Allow Reversing a Payment even when nothing is outstanding..
    '==    -- Debtors Payments- Allow user to Discount full Outst. balance of Invoice...

    Private Function mbSetupCustomer(ByRef colSelectedRow As Collection) As Boolean
        Dim s1, sSql, sPayList As String
        Dim int1, intHeight, intInvoice_id As Integer
        '== Dim dtInvoices, dtDisbursements As DataTable
        '= Dim colDisbursements As Collection
        Dim colInvoices, colInvoice1 As Collection
        Dim col1, col2, colPayList As Collection
        '= Dim bIsRefund As Boolean
        Dim decInvoiceTotal, decTotalTax, decPaymentAmount As Decimal
        Dim decPaymentTotal, decAmountOutstanding As Decimal

        mbIsLoadingCustomer = True

        btnCommit.Enabled = False
        mbSetupCustomer = False
        mDecTotalOutstanding = 0
        mDecAmountPayingNow = 0
        labCreditNoteAvail.Text = ""
        '= btnContinue.Enabled = False
        '= dgvInvoices.Enabled = False

        txtCustBarcode.Text = colSelectedRow.Item("barcode")("value")
        msCustomerBarcode = Trim(txtCustBarcode.Text)

        If colSelectedRow.Contains("companyName") Then
            txtCustName.Text = colSelectedRow.Item("companyName")("value")
            '= txtCustName.Text = col1.Item("value")
        End If
        If txtCustName.Text <> "" Then txtCustName.Text &= vbCrLf
        If colSelectedRow.Contains("firstName") Then
            txtCustName.Text &= colSelectedRow.Item("firstName")("value") & " "
            '=txtCustName.Text = col1.Item("value")
        End If
        If colSelectedRow.Contains("lastName") Then
            txtCustName.Text &= colSelectedRow.Item("lastName")("value")
            '=txtCustName.Text = col1.Item("value")
        End If
        txtCustPhone.Text = colSelectedRow.Item("phone")("value")
        txtCustMobile.Text = colSelectedRow.Item("mobile")("value")
        txtCustEmail.Text = Trim(colSelectedRow.Item("email")("value"))
        labCreditLimit.Text = Format(CDec(colSelectedRow.Item("creditlimit")("value")), "    0.00")

        msCustomerEmail = txtCustEmail.Text

        DoEvents()

        int1 = CInt(colSelectedRow.Item("isAccountCust")("value"))
        If (int1 <> 0) Then
            mbIsAccountCust = True
        Else '-zero-
            mbIsAccountCust = False
            '=done before= mTxtSaleCashout.Enabled = True
            MsgBox("Customer " & txtCustName.Text & " is NOT an account customer ! ", MsgBoxStyle.Exclamation)
            mIntCustomer_id = -1
            msCustomerBarcode = ""
            txtCustBarcode.Text = ""
            mbIsLoadingCustomer = False
            Exit Function
        End If
        mIntCustomer_id = CInt(colSelectedRow.Item("customer_id")("value"))


        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '== -- Payments Form needs ReversedInvoices to be filtered out..
        '=mColAllAccountReversals-
        Dim sReversedInfo As String = ""
        Dim clsDebtors1 As clsDebtors
        Dim listInvoicesReversals As New List(Of Integer)
        clsDebtors1 = New clsDebtors(mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                         mColPrefsCustomer, msVersionPOS, mImageUserLogo, mIntStaff_id, msStaffName)
        If Not clsDebtors1.CollectAllAccountReversals(mColAllAccountReversals) Then
            '- error..  take it as none..
            Cursor = Cursors.Default
            mbIsLoadingCustomer = False
            MsgBox("ERROR- Payment session is being abandoned !", MsgBoxStyle.Exclamation)
            Call close_me()
            Exit Function
            '=mColAllAccountReversals = New Collection
        Else '==ok-
            '--test-
            s1 = ""
            For Each colRefund As Collection In mColAllAccountReversals
                listInvoicesReversals.Add(colRefund.Item("original_id"))
                s1 &= "Inv.No: " & colRefund.Item("original_id") & _
                           " Amt: " & FormatNumber(colRefund.Item("total_inc"), 2) & vbCrLf
            Next
            '= MsgBox("Found " & mColAllAccountReversals.Count & " account invoices reversed.." & vbCrLf & s1, MsgBoxStyle.Information)
        End If
        ToolTip1.SetToolTip(labReversedInvoices, "")
        labReversedInvoices.Text = ""

        '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)
        '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)


        '-  load list of invoices for this cust..--
        '== Call mbLoadInvoiceList(mIntSaleCustomer_id)

        '-- clear grid.-
        Call mbClearTransaction()
        labHelp.Text = " Loading and checking Customer Invoices."

        '--Get all invoices for this customer-
        '--  get all PaymentDisbursements (JOIN Payments) for Customer..
        '-- Apply disb. amounts to invoices..
        '--   Collect invoices not fully paid..
        Cursor = Cursors.WaitCursor
        '-- Load Grid with all Outstanding Invoices for this customer --

        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '=  If Not gbCollectCustomerInvoices(mCnnSql, mIntCustomer_id, True, DateTime.Today, _
        '=                                     colInvoices, mDecTotalInvoices, mDecTotalOutstanding) Then
        mDecTotalInvoices = 0
        mDecTotalOutstanding = 0  '--Will be computed.

        If Not gbCollectCustomerInvoices(mCnnSql, mIntCustomer_id, True, DateTime.Today, _
                                             colInvoices, mDecGrossTotalInvoices, mDecGrossTotalOutstanding) Then
            '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)
            Cursor = Cursors.Default
            Call close_me()
            mbIsLoadingCustomer = False
            Exit Function
        End If
        If (colInvoices Is Nothing) OrElse (colInvoices.Count <= 0) Then
            Cursor = Cursors.Default
            MsgBox("No outstanding invoice found for this Customer ", MsgBoxStyle.Exclamation)
            '=- Debtors Payments- Allow Reversing a Payment even when nothing is outstanding..
            'mIntCustomer_id = -1
            'msCustomerBarcode = ""
            'txtCustBarcode.Text = ""
            'txtCustBarcode.Select()
            'mbIsLoadingCustomer = False
            'Exit Function
        End If
        Cursor = Cursors.Default

        '-Save for Statement print..
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '== -- Payments Form needs ReversedInvoices to be filtered out..
        mColReversedInvoices = New Collection
        '==mColInvoices = colInvoices
        mColInvoices = New Collection
        '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)

        '-- Load Grid with all Outstanding Invoices for this customer --
        Dim intCount As Integer = 0
        Dim gridRow1 As DataGridViewRow
        Cursor = Cursors.WaitCursor

        For Each colInvoice1 In colInvoices
            '=decPaymentTotal = 0
            intInvoice_id = colInvoice1.Item("invoice_id")

            '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
            '== -- Payments Form needs ReversedInvoices to be filtered out..
            If listInvoicesReversals.Contains(intInvoice_id) Then
                '-- Was reversed..
                mColReversedInvoices.Add(colInvoice1)
                sReversedInfo &= "Invoice # " & intInvoice_id & "; " & _
                        Format(colInvoice1.Item("invoice_date"), "dd-MMM-yyyy") & "; " & _
                                     FormatNumber(CDec(colInvoice1.Item("InvoiceTotal")), 2) & "." & vbCrLf
                Continue For   '--next invoice.
            Else
                '--alive=  Show in grid..
                mColInvoices.Add(colInvoice1)
            End If
            '== END Target-New-Build 4259 -- (Started 17-Jul-2020)

            '== bIsRefund = (UCase(datarow1.Item("transactiontype")) = "REFUND")
            decInvoiceTotal = CDec(colInvoice1.Item("InvoiceTotal"))
            decTotalTax = CDec(colInvoice1.Item("Total_Tax"))
            colPayList = colInvoice1.Item("invoicePayments")
            sPayList = colInvoice1.Item("invoicePaymentList")
            '--paymentTotalThisInvoice-
            decPaymentTotal = colInvoice1.Item("paymentTotalThisInvoice")
            '-decAmountOutstanding-
            decAmountOutstanding = colInvoice1.Item("amountOutstanding")
            '=3519.0414- truncate excess decimals (we only want whole cents-)
            '= eg. mDecComputeAmountExTax = Decimal.Truncate((decGrossAmount * (100 / (100 + mDecGST_rate))) * 100) / 100
            decAmountOutstanding = (Decimal.Truncate(decAmountOutstanding * 100) / 100)

            '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
            '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
            mDecTotalOutstanding += decAmountOutstanding
            mDecTotalInvoices += decInvoiceTotal
            '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)


            '--load grid with outstanding invoices-
            Dim fontLucida As New Font("Lucida Console", 7)
            '-- 3303.0109= DO NOT show Refunds (are shown separately below)..
            '==  BUT now- NO MORE REFUNDS here in Payments !!
            If (decAmountOutstanding <> 0) And (LCase(colInvoice1.Item("transactionType")) <> "refund") Then  '-he must pay..-
                gridRow1 = New DataGridViewRow  '--prepare datagrid report row..
                dgvInvoices.Rows.Add(gridRow1)
                '-- load invoice no into grid row.
                With dgvInvoices.Rows(intCount)
                    .Cells(k_INVGRIDCOL_INV_DATE).Value = _
                                              Format(colInvoice1.Item("invoice_date"), "dd-MMM-yyyy")
                    .Cells(k_INVGRIDCOL_INV_NO).Value = intInvoice_id
                    .Cells(k_INVGRIDCOL_INV_TOTAL).Value = FormatNumber(decInvoiceTotal, 2)
                    .Cells(k_INVGRIDCOL_TAX_TOTAL).Value = FormatNumber(decTotalTax, 2)
                    .Cells(k_INVGRIDCOL_PREV_PAID).Value = sPayList
                    '- make paylist fixed pitch..-
                    .Cells(k_INVGRIDCOL_PREV_PAID).Style.Font = fontLucida

                    .Cells(k_INVGRIDCOL_OUTSTANDING).Value = FormatNumber(decAmountOutstanding, 2)
                    .Cells(k_INVGRIDCOL_PAYABLE).Value = FormatNumber(decAmountOutstanding, 2)  '-new-
                    .Cells(k_INVGRIDCOL_NEW_BAL).Value = FormatNumber(decAmountOutstanding, 2)
                    '== mDecTotalInvoices += decInvoiceTotal
                    '== mDecTotalOutstanding += decAmountOutstanding

                    .Cells(k_INVGRIDCOL_INV_DATE).Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                    '-make white bg for paying col.
                    .Cells(k_INVGRIDCOL_PAYING_NOW).Style.BackColor = Color.White
                    '-and discount col.
                    .Cells(k_INVGRIDCOL_DISCOUNT).Style.BackColor = Color.White

                    intHeight = dgvInvoices.Rows(intCount).Height
                    '-- make row deeper if more than one payment.
                    If (colPayList.Count > 1) Then
                        .Height = intHeight + ((intHeight \ 2) * colPayList.Count)
                    End If
                End With
                intCount += 1  '-count grid rows..-
            End If  '-outstanding-
            DoEvents()  '--give it some time.-
        Next  '-colInvoice1-
        Cursor = Cursors.Default

        Call mbLoadPaymentsList(mIntCustomer_id)

        msTransactionType = "accountPayment"

        txtInitialOwing.Text = FormatCurrency(mDecTotalOutstanding, 2)

        '= labHelp.Text = " Enter Amount being paid."
        '== not here..=  txtCustBarcode.Enabled = False

        If (mDecTotalOutstanding > 0) Then
            '= txtAmtPaying.Enabled = True
            '= LabAmountPaying.Enabled = True
        ElseIf (mDecTotalOutstanding < 0) Then
            MsgBox("Account is in credit..", MsgBoxStyle.Information)
        Else
            '=MsgBox("There is no outstanding amount for this customer.", MsgBoxStyle.Information)
        End If

        '-- get oustanding refunds.. if any- 
        '-- 3403.1031- NO MORE refunds here..

        '=3403.1015=
        mDecCreditNoteCreditApplying = 0
        '--  get any CreditNote balance if any..
        '- get all credit notes.. (as selected).
        Dim dtPayments As DataTable
        Dim decCredits As Decimal = 0
        Dim decDebits As Decimal = 0

        If Not gbGetCreditNoteHistory(mCnnSql, mIntCustomer_id, dtPayments, _
                                            decCredits, decDebits, mDecCreditNoteCreditAvailable) Then
            MsgBox("Failed Looking up credit notes.. ", MsgBoxStyle.Exclamation)
            '= Exit Function
        Else  '-ok-
            If (dtPayments IsNot Nothing) AndAlso (dtPayments.Rows.Count > 0) Then
                Dim intTrCount As Integer = dtPayments.Rows.Count
                '= MsgBox("Found " & intTrCount & " credit note transactionss..", MsgBoxStyle.Information)
            Else
                '= MsgBox("No credit note transaction found..", MsgBoxStyle.Information)
            End If  '-dt nothing-
        End If  '--get-
        labCreditNoteAvail.Text = "Credit Note Bal." & vbCrLf & _
                                    "Available:" & vbCrLf & FormatCurrency(mDecCreditNoteCreditAvailable, 2)

        If (mDecTotalOutstanding > 0) Then  '-have debits.
            dgvInvoices.Enabled = False
            '-  must enter some amt to pay first..
            dgvPaymentDetails.Enabled = False
            '= txtAmtPaying.Enabled = True
            '= txtAmtPaying.Select()
            '-- Show original TotalRefunds- and now Available..

            '==          NB: Refunds must be done as a  separate run (commit) from money payments.
            '==          NB: Refunds must be done as a  separate run (commit) from money payments.
            '==          NB: Refunds must be done as a  separate run (commit) from money payments.
            '-- NO MORE !!!

            '-no avail refunds- OR NOT being done now.
            '= mColRefundsUsed = New Collection  '-make emoty-
            '= mDecRefundApplying = 0
            txtCreditNotesApplied.Enabled = False  '=ReadOnly = True
            '= txtAmtPaying.Enabled = True
            dgvPaymentDetails.Enabled = True

            '- mIntLastPaymentRowNoEntered -
            Dim rx As Integer = mIntLastPaymentRowNoEntered
            If (mIntLastPaymentRowNoEntered < 0) Then
                rx = 0  '--first time-
            End If
            '=  no=   dgvPaymentDetails.CurrentCell = dgvPaymentDetails.Rows(rx).Cells(1)
            '=4201.0707- Always start with Payment Detail at the top.
            dgvPaymentDetails.CurrentCell = dgvPaymentDetails.Rows(0).Cells(1)
            '= panelPaymentHdr.Enabled = True

            '=3403.1016=  Set up credit note avail if any.. (in place of Refunds)..
            If (mDecCreditNoteCreditAvailable > 0) Then
                '=4201.1007=
                '=  TOO EARLY to decide to use credit not !!!
                '=  TOO EARLY to decide to use credit not !!!
                '=  TOO EARLY to decide to use credit not !!!
                'If (mDecCreditNoteCreditAvailable >= mDecTotalOutstanding) Then
                '    mDecCreditNoteCreditApplying = mDecTotalOutstanding
                'Else  '--not enough to cover outstanding..
                '    '--use up what we've got.
                '    mDecCreditNoteCreditApplying = mDecCreditNoteCreditAvailable
                'End If
                'txtCreditNotesApplied.Text = FormatCurrency(mDecCreditNoteCreditApplying, 2)
                txtCreditNotesApplied.Text = "0.00"
                txtCreditNotesApplied.Enabled = True
                '= txtCreditNoteApplied.Text = txtRefundsApplied1.Text
                labApplyingCreditNotes.Enabled = True
                '=txtCreditNotesApplied.Select()
            Else  '-no credit note avail.
                txtCreditNotesApplied.Text = ""
                '= txtCreditNoteApplied.Text = ""
                '-  must enter some amt to pay first..
                txtCreditNotesApplied.Text = "0.00"
                '= txtTotalApplying.Text = txtInitialOwing.Text
                '= dgvPaymentDetails.Select()   '=txtAmtPaying.Select()
            End If '--avail-
            '-- 'validate' should distribute..-
            '- compute balances etc..
            btnNewPayment.Enabled = True
            btnReverse.Enabled = True
            btnStatement.Enabled = True

            '= btnCustomerInfo.Enabled = True
            labHelp.Text = " Choose Transaction.."

            btnNewPayment.Select()
        Else
            '-- no debits..  no action..
            '-  can reverse a payment if any.
            If listPayments.Items.Count > 0 Then
                btnReverse.Enabled = True
            End If
        End If  '--outst.-
        '= End If '--collect-
        '== Call mbUpdateTransactionTotal()

        '--testing- show reversals..
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '== -- Payments Form needs ReversedInvoices to be filtered out..
        'MsgBox("NB: This customer has " & mColReversedInvoices.Count & _
        '                " Reversed Invoices in this period.." & vbCrLf & sReversedInfo, MsgBoxStyle.Information)
        If (sReversedInfo <> "") Then
            labReversedInvoices.Text = "Reversed Invoices"
            ToolTip1.SetToolTip(labReversedInvoices, sReversedInfo)
        End If  '-info-

        '"Gross outstanding is: " & FormatNumber(mDecGrossTotalOutstanding, 2) & vbCrLf & _
        '"Adjusted outstanding is: " & FormatNumber(mDecTotalOutstanding, 2), MsgBoxStyle.Information)
        '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)

        mbIsLoadingCustomer = False  '-release Roger.
        mbSetupCustomer = True

    End Function  '--SetupCustomer--
    '= = = = = = =
    '-===FF->

    '--load--
    '--load--

    Private Sub frmPayments_Load(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles MyBase.Load
        Dim s1 As String
        '= txtAmtPaying.Enabled = False
        '= LabAmountPaying.Enabled = False
        labCreditNoteAvail.Text = ""
        grpBoxRefundType.Enabled = False
        '= btnContinue.Enabled = False
        dgvInvoices.Enabled = False

        grpBoxPayment.Text = ""
        msComputerName = My.Computer.Name
        msCurrentUserName = gsGetCurrentUser()

        btnNewPayment.Enabled = False
        btnReverse.Enabled = False
        '-- show Statement-
        btnStatement.Enabled = False

        '= panelPaymentHdr.Enabled = False
        '= msSettingsPath = gsLocalSettingsPath() '= default Jobmatix33=
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        '- set white BG for paying col..
        '== dgvInvoices.Columns(k_INVGRIDCOL_PAYING_NOW).DefaultCellStyle.BackColor = Color.White
        '- position on top of calling form..
        'If mFrmParent Is Nothing Then
        '    Call CenterForm(Me)
        'Else
        '    Me.Left = mFrmParent.Left + 16
        '    Me.Top = mFrmParent.Top + 33
        'End If

        '= dgvInvoices_old.Visible = False

        dgvInvoices.Width = panelHdr.Width
        '- fix grid headers-
        With dgvInvoices
            .Columns("inv_total").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("TaxTotal").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("prev_paid").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("outstanding").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("discount").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("discount").DefaultCellStyle.BackColor = Color.White

            .Columns("payable").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("paying_now").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .Columns("paying_now").DefaultCellStyle.BackColor = Color.White

            .Columns("new_balance").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        'labInstructions.Text = "The data grid below shows those invoices with payment still outstanding." & _
        '                     "The current payment amount is initially applied to oldest invoices first."

        '-- get system Info table data.-
        '=3403.1031=
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        '= Call mbRefreshSystemInfo()

        '=3301.607= Check if we can save to email queue (docArchive table).
        mbAllowEmailInvoices = False
        If mSysInfo1.contains("POS_ALLOW_EMAIL_INVOICES") Then
            If (UCase(mSysInfo1.item("POS_ALLOW_EMAIL_INVOICES")) = "Y") Then
                mbAllowEmailInvoices = True    '= Yes, do emailing.-
            End If
        End If
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName As String In colPrinters
                '-See below for pdf..
                'If (InStr(LCase(sName), "adobe pdf") > 0) Or _
                '         ((InStr(LCase(sName), "microsoft") > 0) And (InStr(LCase(sName), "pdf") > 0)) Then
                '    msPdfPrinterName = sName  '-save PDF printer name--
                'End If
            Next sName
        End If  '- no printers-

        '=3411.0110=  Get PDF prefrred printer...
        '---(Microsoft will be preferred)..
        If Not gsGetPdfPrinterName(msPdfPrinterName) Then
            MsgBox("Error- Failed to look-up pdf printer..", MsgBoxStyle.Exclamation)
        End If  '-gety-

        If (msPdfPrinterName = "") Then
            MsgBox("Please Note: " & vbCrLf & "No PDF printer is installed on this system." & vbCrLf & _
                    "Invoices created here can not be stored for emailing)..", MsgBoxStyle.Information)
        End If

        '= = = = = = = = = = = = = = = = = = = = = =

        '=4201.0426=--  All stuff from Activated...
        '=4201.0426=--  All stuff from Activated...
        '=4201.0426=--  All stuff from Activated...
        Dim colPaymentTypes, col1 As Collection
        Dim rx As Integer
        '= Dim s1 As String
        Dim row1 As DataGridViewRow

        If mbActivated Then Exit Sub
        mbActivated = True

        '=- labDLLversion.Text = msVersionPOS

        '= labStaffName.Text = msStaffName

        '- load payment types..-
        '==sSql = "SELECT * from [paymentTypes];"
        dgvPaymentDetails.Rows.Clear()

        '=--3101.1206=
        '==  4201.0707. 
        '= colPaymentTypes = gColPaymentTypes()  '--3101.1206= Get collection of types.

        '==    -- 4201.0707.  Started 05-July-2019-
        '==       -- Add Local Preference for Re-ordering of payment Details.
        Dim clsPayTypes1 As clsPaymentTypes
        clsPayTypes1 = New clsPaymentTypes(msSettingsPath)
        '-test-
        '= MsgBox("Settings path: " & vbCrLf & msSettingsPath, MsgBoxStyle.Information)
        colPaymentTypes = clsPayTypes1.getColPaymentTypes()
        '- end of new bit..
        rx = 0
        For Each col1 In colPaymentTypes
            row1 = New DataGridViewRow
            dgvPaymentDetails.Rows.Add(row1)
            With dgvPaymentDetails.Rows(rx)
                .Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value = col1("description")
                .Cells(k_PAYGRIDCOL_AMOUNT).Value = "0.00"
                .Cells(k_PAYGRIDCOL_AMOUNT).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value = col1("key")
            End With
            rx += 1
        Next col1
        DoEvents()

        txtCustBarcode.Select()
        labHelp.ForeColor = Color.Blue
        labHelp.Text = "Select customer."

        '=4201.0426=--  MORE stuff for Statement Print....
        '=4201.0426=--  MORE stuff for Statement Print....
        '=4201.0426=-- GET printers AGAIN for statement printing.....

        If Not gbGetAvailablePrinters(colPrinters, msDefaultprinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName As String In colPrinters
                cboStatementPrinters.Items.Add(sName)
                '== See above for PDF capture..
             Next sName
            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_statementPrtSettingKey) AndAlso _
                     (mLocalSettings1.item(k_statementPrtSettingKey) <> "") Then
                s1 = mLocalSettings1.item(k_statementPrtSettingKey)
                '= gbQueryLocalSetting(gsLocalSettingsPath, k_statementPrtSettingKey, s1) AndAlso (s1 <> "") Then
                If colPrinters.Contains(s1) Then '--set it- 
                    cboStatementPrinters.SelectedItem = s1
                Else
                    If (msDefaultprinterName <> "") Then cboStatementPrinters.SelectedItem = msDefaultprinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultprinterName <> "") Then cboStatementPrinters.SelectedItem = msDefaultprinterName
            End If  '-query- 
        End If '-getAvail.- 


        '==   Target-New-Build-4259 -- (Started 17-Jul-2020)
        labReversedInvoices.Text = ""
        ToolTip1.SetToolTip(labReversedInvoices, "")
        '== END  Target-New-Build-4259 -- (Started 17-Jul-2020)

        mbIsInitialising = False

    End Sub  '--load--
    '= = = = = = = = = = = = = 
    '= = = = = = = = = = = = = 
    '-===FF->

    '--Activated--
    '--Activated--

    'Private Sub frmPayments_Activated(ByVal sender As System.Object, _
    '                            ByVal e As System.EventArgs) Handles MyBase.Activated
    '= Dim sSql As String
    'Dim colPaymentTypes, col1 As Collection
    'Dim rx As Integer
    ''= Dim s1 As String
    'Dim row1 As DataGridViewRow

    'If mbActivated Then Exit Sub
    'mbActivated = True

    ''=- labDLLversion.Text = msVersionPOS

    ''= labStaffName.Text = msStaffName

    ''- load payment types..-
    ''==sSql = "SELECT * from [paymentTypes];"
    'dgvPaymentDetails.Rows.Clear()

    ''=--3101.1206=
    'colPaymentTypes = gColPaymentTypes()  '--3101.1206= Get collection of types.
    'rx = 0
    'For Each col1 In colPaymentTypes
    '    row1 = New DataGridViewRow
    '    dgvPaymentDetails.Rows.Add(row1)
    '    With dgvPaymentDetails.Rows(rx)
    '        .Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value = col1("description")
    '        .Cells(k_PAYGRIDCOL_AMOUNT).Value = "0.00"
    '        .Cells(k_PAYGRIDCOL_AMOUNT).Style.Alignment = DataGridViewContentAlignment.MiddleRight
    '        .Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value = col1("key")
    '    End With
    '    rx += 1
    'Next col1
    'DoEvents()

    'txtCustBarcode.Select()
    'labHelp.ForeColor = Color.Blue
    'labHelp.Text = "Select customer."

    'mbIsInitialising = False

    '= End Sub  '--Activated--
    '= = = = = = = = = = = = = 
    '-===FF->

    '--Show CustomerInfo--

    'Private Sub btnCustomerInfo_Click(ByVal sender As System.Object, _
    '                                 ByVal e As System.EventArgs)
    '    Dim frmCust1 As New frmCustomer

    '    frmCust1.StaffName = msStaffName

    '    frmCust1.SqlServer = msServer
    '    frmCust1.connectionSql = mCnnSql '--job tracking sql connenction..-
    '    frmCust1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..

    '    frmCust1.DBname = msSqlDbName
    '    frmCust1.VersionPOS = msVersionPOS
    '    frmCust1.form_left = Me.Left
    '    frmCust1.form_top = Me.Top + 30
    '    frmCust1.customer_id = mIntCustomer_id  '--this customer..-

    '    frmCust1.ShowDialog()

    'End Sub '--CustomerInfo-
    '= = = =  = = = = = = = = =

    '-listPayments-

    Private Sub listPayments_SelectedIndexChanged(ByVal sender As System.Object, _
                                                  ByVal e As System.EventArgs) _
                                                    Handles listPayments.SelectedIndexChanged


    End Sub  '-listPayments_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = = = =  = =

    Private Sub listPayments_DoubleClick(ByVal sender As System.Object, _
                                                   ByVal e As System.EventArgs) _
                                               Handles listPayments.DoubleClick

        Dim sItem As String = Trim(listPayments.SelectedItem)
        Dim iPos, ix As Integer

        iPos = InStr(sItem, ":")
        If (iPos > 0) Then
            ix = CStr(Trim(VB.Left(sItem, iPos - 1)))  '-get payment no.-
            Call mbShowPayment(ix)
        End If

    End Sub  '-listPayments_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = = = =  = =
    '-===FF->

    '- Customer barcode entry--
    '- Customer barcode entry--
    '-- ENTER for TEST-

    Private Sub txtCustBarcode_Enter(ByVal sender As Object, _
                                    ByVal e As System.EventArgs) Handles txtCustBarcode.Enter

        If dgvInvoices.Rows.Count > 0 Then  '-have payment in progress..
            MsgBox("Use Cancel button to discard current payment if not wanted..", MsgBoxStyle.Exclamation)
            btnCancel.Select()
        End If  '--count-

    End Sub '-enter-
    '= = = =  = = = = = = = = = =


    Public Sub txtCustBarcode_TextChanged(ByVal sender As System.Object, _
                                                ByVal e As System.EventArgs) _
                                                Handles txtCustBarcode.TextChanged

    End Sub  '--txtCustBarcode_TextChanged--
    '= = = = = = = = = = = = = = =  = = = 

    '-- CUSTOMER  Enter Pressed --

    Public Sub txtCustBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                          Handles txtCustBarcode.KeyPress

        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim sSql As String
        Dim colResult, colRecord As Collection

        If keyAscii = 13 Then '--enter-
            s1 = Trim(txtCustBarcode.Text)
            If (s1 <> "") Then  '--have barcode-
                '--lookup barcode-
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [customer] WHERE (barcode='" & s1 & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    If Not mbSetupCustomer(colRecord) Then
                        Exit Sub
                    End If
                    '= txtAmtPaying.Select()
                    '= dgvInvoices.Select()   '--focus-
                Else '--not found..-
                    MsgBox("No Customer found for barcode: " & s1, MsgBoxStyle.Exclamation)
                End If  '-get--
            End If  '--have barcode-
            keyAscii = 0
        End If  '--key ascii-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub  '--CUST keypress=
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-- Customer Search (F2)..--
    '-- Grid TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for Cust Lookup--
    '--  and SHIFT-F2 to lookup Invoice No.

    Public Sub txtCustBarcode_KeyDown(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                          Handles txtCustBarcode.KeyDown

        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim colKeys As Collection
        Dim colSelectedRow As Collection

        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup Customer--
            If Not mbBrowseAndSearchCustomers(mColPrefsCustomer, "Lookup Customers", "", colKeys, colSelectedRow) Then
                '=If Not mbBrowseTable(mColPrefsCustomer, "Lookup Customers", "", colKeys, colSelectedRow, "Customer") Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else  '--selected
                txtCustName.Text = ""
                If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                    If Not mbSetupCustomer(colSelectedRow) Then
                        Exit Sub
                    End If
                    '= dgvInvoices.Select()   '--focus-
                    '= txtAmtPaying.Select()
                End If
            End If  '-browse-
            '= End With '-frmsale-
        ElseIf (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup INVOICES--
            '--  and SHIFT-F2 to lookup Invoice No.
            Dim frmBrowse1 As New frmBrowsePOS
            Dim sSql, sBarcode As String
            Dim colPrefs, colRecord, colResult As New Collection
            colPrefs.Add("invoice_id")

            frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
            frmBrowse1.colTables = mColSqlDBInfo
            frmBrowse1.DBname = msSqlDbName
            frmBrowse1.tableName = "invoice"
            frmBrowse1.IsSqlServer = True '--bIsSqlServer

            sSql = "SELECT invoice_id, invoice_date, total_inc, "
            sSql &= "   customer.companyname AS cust_company, (customer.lastname + ', ' + customer.firstname) AS cust_name, "
            sSql &= "    customer.barcode, customer.customer_id "
            sSql &= "  FROM dbo.invoice "
            sSql &= "   JOIN customer ON customer.customer_id=invoice.customer_id "
            frmBrowse1.UserSelectList = sSql
            '- WHERE (isAccountCust=1)=
            frmBrowse1.WhereCondition = " (isAccountCust=1) "
            frmBrowse1.PreferredColumns = colPrefs
            frmBrowse1.Title = "Lookup Sales Invoices"
            frmBrowse1.HideEditButtons = True
            frmBrowse1.lookupSelection = True

            frmBrowse1.ShowDialog()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
            If Not frmBrowse1.cancelled Then
                '--  get selected record key..--
                colKeys = frmBrowse1.selectedKey
                colSelectedRow = frmBrowse1.selectedRow
                txtCustName.Text = ""
                If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                    '-- get barcode, and then get cust record..
                    sBarcode = colSelectedRow.Item("barcode")("value")
                    '--  get recordset as collection for SELECT..--
                    sSql = "SELECT * FROM [customer] WHERE (barcode='" & sBarcode & "');"
                    If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                           (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                        '--have a row..-
                        colRecord = colResult.Item(1)
                        If Not mbSetupCustomer(colRecord) Then
                            Exit Sub
                        End If
                        '= txtAmtPaying.Select()
                        '= dgvInvoices.Select()
                    End If  '--fet collection-
                End If '-nothing-
            End If  '-cancelled-
            frmBrowse1.Close()
            frmBrowse1.Dispose()
        End If '--keycode 

    End Sub  '--key down-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '--txtCustBarcode_Validating-
    '-  In case TAB was used..

    Public Sub txtCustBarcode_Validating(ByVal sender As System.Object, _
                                               ByVal ev As CancelEventArgs) _
                                                Handles txtCustBarcode.Validating
        Dim text1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(text1.Text)
        Dim sBarcode As String
        Dim sSql As String
        Dim colResult, colRecord As Collection

        If (sData = "") Then
            ev.Cancel = True
            MsgBox("Cust Barcode needed !", MsgBoxStyle.Exclamation)
        Else  '--lookup barcode
            sBarcode = Trim(sData)
            If LCase(msCustomerBarcode) = LCase(sBarcode) Then
                Exit Sub  '-TAB ok-
            End If
            '--lookup barcode-
            '--  get recordset as collection for SELECT..--
            sSql = "SELECT * FROM [customer] WHERE (barcode='" & sBarcode & "');"
            If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                   (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                '--have a row..-
                colRecord = colResult.Item(1)
                If Not mbSetupCustomer(colRecord) Then
                    Exit Sub
                End If
                '== txtAmtPaying.Select()
                '==DgvSaleItems.Select()   '--focus-
            Else '--not found..-
                ev.Cancel = True
                MsgBox("No Customer found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
            End If  '-get--
        End If  '-data-
    End Sub  '--txtCustBarcode_Validating-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-'- txtCustBarcode_Validated--

    Public Sub txtCustBarcode_Validated(ByVal sender As System.Object, _
                                               ByVal ev As System.EventArgs) Handles txtCustBarcode.Validated
        txtCustBarcode.Enabled = False
        '= txtAmtPaying.Select()
        '= dgvInvoices.Select()
    End Sub  '- txtCustBarcode_Validated-
    '= = = = = = = = = = = = = == = ==  =

    '-- new payment-
    Private Sub btnNewPayment_Click(sender As Object, e As EventArgs) Handles btnNewPayment.Click

        btnNewPayment.Enabled = False
        btnReverse.Enabled = False
        btnStatement.Enabled = False

        dgvInvoices.Enabled = True

        dgvInvoices.CurrentCell = dgvInvoices.Rows(0).Cells(k_INVGRIDCOL_PAYING_NOW)
        labHelp.Text = " Enter amounts paying for each Invoice."

        Call mbUpdateTransactionTotal()  '--initial setup-

        dgvInvoices.Select()

    End Sub  '-new-
    '= = = = = = == =  =

    '-- show Statement-
    '=4201.0509=- Go straight to PrintStatement (Preview only.)
    '- --- Go straight to PrintStatement (Preview only.)

    Private Sub btnStatement_Click(sender As Object, e As EventArgs) Handles btnStatement.Click

        '- Go straight to PrintStatement (Preview only.)
        Dim sSql, s1 As String
        Dim dataTable1 As DataTable
        Dim row1 As DataRow
        Dim bPreviewOnly As Boolean = True
        Dim clsPrint1 As clsPrintSaleDocs

        '-  Get CustInfo for PrintStatement....-
        sSql = "SELECT *, "
        sSql &= " CASE companyName "
        '= sSql &= "  WHEN '' THEN lastName + ', ' + firstName "
        sSql &= "  WHEN '' THEN lastName + ', ' + firstName "
        sSql &= "     ELSE companyName "
        sSql &= "  END  AS custShortName "
        sSql &= " FROM dbo.customer "
        sSql &= "  WHERE (Customer_id =" & CStr(mIntCustomer_id) & ");"

        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            MsgBox("Error in getting recordset for Customer table: " & vbCrLf & _
                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Sub
        Else  '-ok=
            If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then
                row1 = dataTable1.Rows(0)
            Else
                MsgBox("Error- No data returned for Customer table: ", MsgBoxStyle.Exclamation)
                Exit Sub
            End If  '-nothing-
        End If  '-get-

        '--make custInfo stuff for PrintStatement.
        '-- collect Customer Info for Statement print..-
        Dim colCustInfo As New Collection
        colCustInfo.Add(mIntCustomer_id, "customer_id")
        colCustInfo.Add(row1.Item("barcode"), "barcode")
        colCustInfo.Add(row1.Item("firstName"), "firstName")
        colCustInfo.Add(row1.Item("lastName"), "lastName")
        colCustInfo.Add(row1.Item("companyName"), "companyName")
        colCustInfo.Add(row1.Item("custShortName"), "custShortName")
        '- make full name-
        s1 = Trim(row1.Item("companyName"))
        If (s1 <> "") Then s1 &= "- "
        s1 &= row1.Item("firstName") & " " & row1.Item("lastName")
        colCustInfo.Add(s1, "customerFullName")

        colCustInfo.Add(row1.Item("address"), "address")
        colCustInfo.Add(row1.Item("suburb"), "suburb")
        colCustInfo.Add(row1.Item("state"), "state")
        colCustInfo.Add(row1.Item("postcode"), "postcode")
        colCustInfo.Add(row1.Item("country"), "country")
        colCustInfo.Add(row1.Item("email"), "email")
        colCustInfo.Add(row1.Item("creditLimit"), "creditLimit")
        colCustInfo.Add(row1.Item("creditDays"), "creditDays")
        '-doNotEmailDocuments-
        colCustInfo.Add(row1.Item("doNotEmailDocuments"), "doNotEmailDocuments")
        '=4201.0626-
        '-- add to cust collection.
        colCustInfo.Add(mDecCreditNoteCreditAvailable, "CreditNoteBalance")

        clsPrint1 = New clsPrintSaleDocs

        Dim colRefunds As New Collection  '-always empty-
        Dim sStatementPrinterName As String = cboStatementPrinters.SelectedItem

        Call clsPrint1.PrintStatement(mColInvoices, colRefunds, colCustInfo, Today, _
                                     mSysInfo1, mImageUserLogo, msVersionPOS, _
                                        sStatementPrinterName, bPreviewOnly)

        'Dim frmStatements1 As ucChildStatements
        'Dim frmDummy As New Form
        'frmStatements1 = New ucChildStatements(frmDummy, mCnnSql, msSqlDbName, mColSqlDBInfo, _
        '                                 mColPrefsCustomer, msVersionPOS, mImageUserLogo, mIntStaff_id, msStaffName)
        ''--request for one customer..
        'frmStatements1.requestedCustomer_id = mIntCustomer_id
        'frmStatements1.ShowDialog()
        '-done-

        Call mbClearTransaction()
        txtCustBarcode.Text = ""
        txtCustName.Text = ""
        txtCustPhone.Text = ""
        txtCustMobile.Text = ""
        txtCustEmail.Text = ""

        '== Call mbLoadPaymentsList(mIntCustomer_id)
        listPayments.Items.Clear()

        txtCustBarcode.Enabled = True
        DoEvents()
        txtCustBarcode.Select()

    End Sub  '-- show Statement-
    '= = = = = = = = = = = = == = == = == 
    '-===FF->

    '--Reverse a payment..

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click

        Dim frmListSel1 As frmListSelect
        Dim intRowx, intPaymentNo As Integer

        btnNewPayment.Enabled = False
        btnReverse.Enabled = False
        intPaymentNo = -1
        '--show all payments.
        frmListSel1 = New frmListSelect
        frmListSel1.inData = mDtPrevPayments
        frmListSel1.hdrText = "Select Payment to be Reversed.."
        '-nettAmountCredited- etc..
        frmListSel1.SqlHeaderTypes = mColSqlColumnTypes

        frmListSel1.ShowDialog()
        If Not frmListSel1.cancelled Then
            intRowx = frmListSel1.selectedRow
            If (intRowx >= 0) Then
                intPaymentNo = mDtPrevPayments.Rows(intRowx).Item(0)
            End If
            frmListSel1.Close()
            '== MsgBox("Selected Payment No: " & intPaymentNo & "..", MsgBoxStyle.Information)

            '-- check that's it not a reversal..
            If (intPaymentNo > 0) AndAlso (Not mDtPrevPayments.Rows(intRowx).Item("isReversal")) Then
                '-- call showPayment with REVERSAL option..
                Dim frmShowPayment1 As frmShowPayment

                frmShowPayment1 = New frmShowPayment
                frmShowPayment1.connectionSql = mCnnSql
                frmShowPayment1.sqlDbname = msSqlDbName
                frmShowPayment1.PaymentNo = intPaymentNo
                '== frmShowPayment1.Settings = mSdSettings
                '== frmShowInvoice1.SystemInfo = mSdSystemInfo
                frmShowPayment1.UserLogo = mImageUserLogo
                frmShowPayment1.versionPOS = msVersionPOS
                '= frmShowPayment1.selectedReceiptPrinterName = sSelectedReceiptPrinterName
                frmShowPayment1.CaptureReceiptPDF = False   '= bCaptureReceiptPDF
                '= frmShowPayment1.CanPrintReceipt = bCanPrintReceipt
                frmShowPayment1.PaymentReversalRequested = True
                '-- current staff_id and msStaffName-
                frmShowPayment1.Staff_id = mIntStaff_id
                frmShowPayment1.StaffName = msStaffName

                frmShowPayment1.ShowDialog()

            Else
                MsgBox("Selected Payment might be a Reversal: ", MsgBoxStyle.Exclamation)
            End If  '-payment No.-
        Else '--cancelled-
            frmListSel1.Close()
        End If

        Call mbClearTransaction()
        txtCustBarcode.Text = ""
        txtCustName.Text = ""
        txtCustPhone.Text = ""
        txtCustMobile.Text = ""
        txtCustEmail.Text = ""

        '== Call mbLoadPaymentsList(mIntCustomer_id)
        listPayments.Items.Clear()

        txtCustBarcode.Enabled = True
        DoEvents()
        txtCustBarcode.Select()

    End Sub  'reverse..
    '= = = = = = =  = = =
    '-===FF->

    '== Invoices paying..

    'Private Sub dgvInvoices_RowEnter(ByVal sender As Object, _
    '                                        ByVal ev As DataGridViewCellEventArgs) Handles dgvInvoices.RowEnter
    '    Dim intRow = ev.RowIndex
    '    Dim intCol = ev.ColumnIndex

    '    If mbIsInitialising Or mbIsLoadingCustomer Then Exit Sub

    '    '= CRASHES--  dgvInvoices.CurrentCell = dgvInvoices.Rows(intRow).Cells(k_INVGRIDCOL_DISCOUNT)
    'End Sub  '-row enter.
    '= = = = = = = = = =


    '-- Enter a Cell.. If Paying now, then load up with amt owing on invoice...

    Private Sub dgvInvoices_CellEnter(ByVal sender As Object, _
                                            ByVal ev As DataGridViewCellEventArgs) _
                                                         Handles dgvInvoices.CellEnter
        Dim intRow = ev.RowIndex
        Dim intCol = ev.ColumnIndex
        Dim sData, s1, sColumnName As String
        Dim decOwing, decDiscount, decPaying As Decimal

        sColumnName = LCase(dgvInvoices.Columns(intCol).Name)
        If (sColumnName = "paying_now") Then
            With dgvInvoices.Rows(ev.RowIndex)
                sData = Trim(.Cells(intCol).value)
                If (sData = "") Then
                    decOwing = CDec(.Cells(k_INVGRIDCOL_PAYABLE).Value)
                    .Cells(k_INVGRIDCOL_PAYING_NOW).Value = FormatNumber(decOwing, 2)
                End If  '-empty-
            End With
        End If  '- col.name-

    End Sub  '-cell enter-
    '= = = = = = = = === = = =
    '-===FF->

    '-- invoice/paying validation..\
    '--- Invoices-- C e l l  V a l i d a t i n g--=  
    '= 3403.1028== CAN give discount..

    Private Sub dgvInvoices_CellValidating(ByVal sender As Object, _
                                                  ByVal ev As DataGridViewCellValidatingEventArgs) _
                                                   Handles dgvInvoices.CellValidating
        '-- check all amounts..
        '--  check total against amount Paying.

        Dim lRow, lCol As Integer
        Dim sData, s1, sColumnName As String
        Dim decOwing, decDiscount, decPaying As Decimal

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        dgvInvoices.Rows(ev.RowIndex).ErrorText = Nothing
        '==dgvInvoices.Rows(ev.RowIndex).Cells(lCol).ErrorText = Nothing
        sColumnName = LCase(dgvInvoices.Columns(lCol).Name)
        sData = Trim(ev.FormattedValue.ToString)

        If (sColumnName = "discount") Or (sColumnName = "paying_now") Then
            If (sData = "") OrElse mbIsNumeric(sData) Then
                '-ok-
            Else
                ev.Cancel = True
                dgvInvoices.Rows(ev.RowIndex).ErrorText = "Amount must be numeric. "
                MsgBox("Amount must be numeric.  !", MsgBoxStyle.Exclamation)
                Exit Sub
            End If  '-numeric-
        Else
            Exit Sub   '-nothing to do-
        End If  '-name-
        decOwing = CDec(dgvInvoices.Rows(ev.RowIndex).Cells(k_INVGRIDCOL_OUTSTANDING).Value)
        If (sColumnName = "discount") Then
            If (sData = "") Then
                decDiscount = 0
            Else
                decDiscount = CDec(sData)
            End If
            If (decDiscount > decOwing) Then
                ev.Cancel = True
                dgvInvoices.Rows(ev.RowIndex).ErrorText = "Amount too big. "
                MsgBox("Discount can't be more than what's owing on invoice !", MsgBoxStyle.Exclamation)
                Exit Sub
            ElseIf (decDiscount < decOwing) Then
                '-ok=
            End If
        ElseIf (sColumnName = "paying_now") Then
            '=If (sData = "") OrElse IsNumeric(sData) Then
            If (sData = "") Then
                decPaying = 0
            Else
                decPaying = CDec(sData)
            End If
            '= decOwing = CDec(dgvInvoices.Rows(ev.RowIndex).Cells(k_INVGRIDCOL_OUTSTANDING).Value)
            decDiscount = 0
            With dgvInvoices.Rows(ev.RowIndex)
                s1 = Trim(.Cells(k_INVGRIDCOL_DISCOUNT).Value)
                If (s1 <> "") AndAlso mbIsNumeric(s1) Then
                    decDiscount = CDec(s1)
                End If  '-discount-
            End With
            '= decDiscount = CDec(dgvInvoices.Rows(ev.RowIndex).Cells(k_INVGRIDCOL_DISCOUNT).Value)
            If (decPaying > (decOwing - decDiscount)) Then
                ev.Cancel = True
                dgvInvoices.Rows(ev.RowIndex).ErrorText = "Amount too big. "
                MsgBox("Amount can't be more than what's owing on invoice (less discount) !", MsgBoxStyle.Exclamation)
                Exit Sub
            ElseIf (decDiscount > 0) AndAlso (decPaying < (decOwing - decDiscount)) Then
                ev.Cancel = True
                dgvInvoices.Rows(ev.RowIndex).ErrorText = "Discount must clear the invoice.. "
                MsgBox("When discounting, payment must clear the invoice. !", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            '--check overall in CellValidated Event below.--
            '=End If  '-numeric-
        End If  '-column-
    End Sub  '--dgvInvoices_CellValidating-
    '= = = = = = = = = = = =
    '-===FF->

    '- validated.

    Private Sub dgvInvoices_CellValidated(ByVal sender As Object, _
                                                ByVal ev As DataGridViewCellEventArgs) _
                                                 Handles dgvInvoices.CellValidated
        Dim lRow, lCol As Integer
        Dim sData, s1 As String
        Dim decOwing, decDiscount, decPayable, decPaying As Decimal
        Dim decNewBal As Decimal

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        decDiscount = 0
        decOwing = 0
        dgvInvoices.Rows(ev.RowIndex).ErrorText = Nothing
        If (LCase(dgvInvoices.Columns(lCol).Name) = "discount") Then
            '- format discount and compute Payable..
            With dgvInvoices.Rows(ev.RowIndex)
                s1 = Trim(.Cells(k_INVGRIDCOL_OUTSTANDING).Value)
                If (s1 <> "") Then
                    decOwing = CDec(.Cells(k_INVGRIDCOL_OUTSTANDING).Value)
                End If
                s1 = Trim(.Cells(k_INVGRIDCOL_DISCOUNT).Value)
                If (s1 <> "") Then
                    decDiscount = CDec(.Cells(k_INVGRIDCOL_DISCOUNT).Value)
                    .Cells(k_INVGRIDCOL_DISCOUNT).Value = FormatCurrency(decDiscount, 2)
                End If
                decPayable = decOwing - decDiscount
                .Cells(k_INVGRIDCOL_PAYABLE).Value = FormatCurrency(decPayable, 2)
                '--load PayingNow with new default value.
                .Cells(k_INVGRIDCOL_PAYING_NOW).Value = FormatCurrency(decPayable, 2)
            End With
        ElseIf (LCase(dgvInvoices.Columns(lCol).Name) = "paying_now") Then
            decDiscount = 0
            With dgvInvoices.Rows(ev.RowIndex)
                s1 = Trim(.Cells(k_INVGRIDCOL_DISCOUNT).Value)
                If (s1 <> "") AndAlso IsNumeric(s1) Then
                    decDiscount = CDec(s1)
                End If
                decOwing = CDec(.Cells(k_INVGRIDCOL_OUTSTANDING).Value)
                s1 = Trim(.Cells(k_INVGRIDCOL_PAYING_NOW).Value)
                decPaying = 0
                If (s1 <> "") AndAlso IsNumeric(s1) Then
                    decPaying = CDec(.Cells(k_INVGRIDCOL_PAYING_NOW).Value)
                End If
                decNewBal = decOwing - decPaying - decDiscount
                If decPaying <> 0 Then
                    .Cells(k_INVGRIDCOL_PAYING_NOW).Value = FormatCurrency(decPaying, 2)
                Else
                    .Cells(k_INVGRIDCOL_PAYING_NOW).Value = ""
                End If
                .Cells(k_INVGRIDCOL_NEW_BAL).Value = FormatCurrency(decNewBal, 2)
            End With
            '-- check overall balance of invoice paying cols.
            '-- set help red if not equal to amount paying..

            Call mbUpdateTransactionTotal()
        End If  '-paying-now-

    End Sub '-dgvInvoices_CellValidated-
    '= = = =  = = = = = == = = = = = = 

    '==  CellEndEdit..--
    '-- CALLED from ACTUAL event sub on Sale Form !!--

    Public Sub dgvinvoices_CellEndEdit(ByVal sender As Object, _
                                         ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
                                           Handles dgvInvoices.CellEndEdit

        ' Clear the row error in case the user presses ESC.   
        dgvInvoices.Rows(e.RowIndex).ErrorText = String.Empty

    End Sub  '-CellEndEdit-
    '= = = = = = = = = = == =
    '-===FF->


    '-- Credit Note-  Amount Applying.

    '- txtRefundsApplied1_TextChanged-

    Private Sub txtCreditNotesApplied_TextChanged(sender As Object, ev As EventArgs) _
                                                        Handles txtCreditNotesApplied.TextChanged

    End Sub  '- txtRefundsApplied1_TextChanged-
    '= = = = = = = = = = = = = = = = = = = == = 

    '-txtRefundsApplied1_Keypress-

    Public Sub txtCreditNotesApplied_Keypress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                    Handles txtCreditNotesApplied.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim control1 As Control = CType(eventSender, TextBox).Parent

        If keyAscii = 13 Then '--enter-
            control1.SelectNextControl(txtCreditNotesApplied, True, True, False, True)
            keyAscii = 0
            eventArgs.Handled = True
        End If  '-13-

    End Sub '-txtRefundsApplied1_Keypress-
    '= = = = = = = = = = = == = = = =  =

    '-txtRefundsApplied1_validating-

    Public Sub txtCreditNotesApplied_validating(ByVal sender As System.Object, _
                                               ByVal ev As CancelEventArgs) _
                                           Handles txtCreditNotesApplied.Validating
        Dim text1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(text1.Text)
        Dim decAmt As Decimal

        If (sData = "") Then  '-ok-
            text1.Text = "0.00"
            mDecCreditNoteCreditApplying = 0
        ElseIf IsNumeric(sData) AndAlso (CDec(sData) >= 0) Then
            '--ok-
            decAmt = CDec(sData)  '==no refunds any more=  + mDecRefundApplying
            '= text1.Text = FormatCurrency(CDec(sData), 2)
            '-- check if over-paying..-
            If (decAmt > mDecCreditNoteCreditAvailable) Then
                ev.Cancel = True
                labHelp.Text = "Exceeds Credit Note Available.!"
                labHelp.ForeColor = Color.Red
                MsgBox("Exceeds Credit Note Available. !!", MsgBoxStyle.Exclamation)
            Else
                '--ok, allocate to invoices (oldest first..)
                '- reset the paying column values..
                labHelp.Text = ""
            End If  '--over- 
        Else
            ev.Cancel = True
            MsgBox("Amount must be numeric, and non-negative. !", MsgBoxStyle.Exclamation)
        End If  '--numeric-

    End Sub  '-txtRefundsApplied1_validating-
    '= = = = = = = = = = = = == = = = = = = =

    '-  Credit Note- txtRefundsApplied1_Validated-

    Public Sub txtCreditNotesApplied_Validated(ByVal sender As System.Object, _
                                               ByVal ev As System.EventArgs) _
                                           Handles txtCreditNotesApplied.Validated
        Dim text1 As TextBox = CType(sender, TextBox)
        Dim sData As String = Trim(text1.Text)

        text1.Text = FormatCurrency(CDec(sData), 2)

        mDecCreditNoteCreditApplying = CDec(text1.Text)
        Call mbUpdateTransactionTotal()

    End Sub  '- txtRefundsApplied1_Validated-
    '= = = = = = == = == = = = = = = = = = =
    '-===FF->

    '-- payment Details..--
    '--- P a y m e n t s --
    '--- P a y m e n t s --

    '=4201.0707- catch enter and F2 etc and pass to keydown.

    Private Sub dgvPaymentDetails_PreviewKeyDown(sender As DataGridView, ev As PreviewKeyDownEventArgs) _
                                                                    Handles dgvPaymentDetails.PreviewKeyDown
        If (ev.KeyCode = Keys.Enter) Or _
                   (ev.KeyCode = Keys.Back) Or (ev.KeyCode = Keys.Right) Then
            ev.IsInputKey = True
        End If
    End Sub  '-dgvPaymentDetails_PreviewKeyDown-
    '= = = = = = =  = = = = = = = = = = = = = = = = 

    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==
    '==   Customer Account Payments-  
    '==        -- Payments Grid- (Stewart 24Aug2020) Fix problem with Payments Amount being displayed in in the 
    '==               detail descr. label column..  
    '==               (In dgvSalePaymentDetails_KeyDown, check for CurrentCell being readonly if ENTER key pressed..)
    '==


    Private Sub dgvSalePaymentDetails_KeyDown(sender As DataGridView, _
                                                ev As KeyEventArgs) Handles dgvPaymentDetails.KeyDown

        Dim bEnterHandled As Boolean = False

        '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
        Dim cellCurrent As DataGridViewCell = dgvPaymentDetails.CurrentCell
        '== END Target-New-Build-4262 -- (Started 14-Aug-2020)


        If (ev.KeyData = Keys.Enter) Then
            '= MsgBox("Enter pressed..", MsgBoxStyle.Information)
            '= ev.Handled = True

            Dim intRow As Integer = dgvPaymentDetails.CurrentCell.RowIndex
            Dim intColumn As Integer = dgvPaymentDetails.CurrentCell.ColumnIndex
            Dim sData As String

            bEnterHandled = False

            '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
            If cellCurrent.ReadOnly Then  '-must be in descr/label col..
                '= MsgBox("You have pressed ENTER in col: " & cellCurrent.ColumnIndex, MsgBoxStyle.Information)  '--TEST-
                ev.Handled = False   '-let the control have it.
                Exit Sub
            End If  '--readonly=
            '== END  Target-New-Build-4262 -- (Started 14-Aug-2020)

            If btnCommit.Enabled Then
                btnCommit.Select()
                bEnterHandled = True
            ElseIf (mDecPaymentBalance > 0) Then  '-not balanced- Load balance into current cell.
                '=sData = FormatNumber(Math.Abs(mDecPaymentBalance), 2) '= FormatCurrency(mDecPaymentBalance, 2)
                sData = FormatNumber(mDecPaymentBalance, 2)
                dgvPaymentDetails.CurrentCell.Value = sData
                Call mbUpdateTransactionTotal()
            End If
        ElseIf (ev.KeyData = Keys.Back) Or _
                      (ev.KeyData = Keys.Right) Or (ev.KeyData = Keys.F2) Then
            '- start edit..
            '-- Let the control see it..
            '= If (ev.KeyData <> Keys.F2) Then SendKeys.Send("{F2}") '=Force start of editing with F2..
        End If
        ev.Handled = bEnterHandled

    End Sub  '--key down.
    '= = = = = = = = = = = = = 
    '-===FF->

    '--- Payments-- C e l l  V a l i d a t i n g--=  

    Private Sub dgvPaymentDetails_CellValidating(ByVal sender As Object, _
                                                 ByVal ev As DataGridViewCellValidatingEventArgs) _
                                                  Handles dgvPaymentDetails.CellValidating
        If mbIsInitialising Then Exit Sub

        Dim lRow, lCol As Integer
        Dim sData, s1 As String

        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        dgvPaymentDetails.Rows(ev.RowIndex).ErrorText = Nothing
        '=dgvPaymentDetails.Rows(ev.RowIndex).Cells(lCol).ErrorText = Nothing
        If LCase(dgvPaymentDetails.Columns(lCol).Name) = "amount" Then
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

    '- Amount Validated-

    Private Sub dgvPaymentDetails_CellValidated(ByVal sender As Object, _
                                                       ByVal ev As DataGridViewCellEventArgs) _
                                                           Handles dgvPaymentDetails.CellValidated
        If mbIsInitialising Then Exit Sub
        Dim lRow, lCol As Integer
        Dim sData, s1 As String
        lRow = ev.RowIndex
        lCol = ev.ColumnIndex
        Dim decAmount As Decimal

        If LCase(dgvPaymentDetails.Columns(lCol).Name) = "amount" Then
            sData = Trim(dgvPaymentDetails.Rows(ev.RowIndex).Cells(lCol).Value)
            If (sData = "") Then
                sData = "0.00"
            End If
            If (sData <> "") AndAlso IsNumeric(sData) Then
                dgvPaymentDetails.Rows(ev.RowIndex).Cells(lCol).Value = Format(CDec(sData), "  0.00")
            End If
            ' Clear any error messages that may have been set in cell validation.
            '== dgvPaymentDetails.Rows(ev.RowIndex).ErrorText = Nothing
            '--TEST==
            '== txtComments.Text &= "PmtValidated- SubT: " & CStr(mDecSubTotalPaying) & vbCrLf

            '- sum all-
            'mDecPaymentTotalRcvd = 0
            'For Each row1 As DataGridViewRow In dgvPaymentDetails.Rows
            '    '== dgvPaymentDetails.Rows(rx).Cells(k_PAYGRIDCOL_AMOUNT).Value()
            '    decAmount = CDec(row1.Cells(k_PAYGRIDCOL_AMOUNT).Value)
            '    mDecPaymentTotalRcvd += decAmount
            '    s1 = row1.Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value
            '    If (InStr(LCase(s1), "cash") > 0) Then
            '        mDecPaymentCashRcvd += decAmount
            '    End If
            'Next  '-row--
            LabSalePayments.Text = "-- Pay Detail Total: " & FormatCurrency(mDecPaymentTotalRcvd, 2)
            mDecPaymentTotalAllInputs = mDecPaymentTotalRcvd + mDecCreditNoteCreditApplying
            '- NOT HERE - Call mbUpdateTransactionTotal()
            '- YES.. HERE - Call mbUpdateTransactionTotal()
            Call mbUpdateTransactionTotal()

            '-- save total-
            '= txtAmtPaying.Text = FormatCurrency(mDecPaymentTotalRcvd, 2)
            mDecAmountPayingNow = mDecPaymentTotalRcvd
            '= mDecAmountPaying = mDecPaymentTotalAllInputs
            '= txtTotalApplying.Text = FormatCurrency(mDecAmountPaying, 2)

            mIntLastPaymentRowNoEntered = lRow  '--save for Stew-
            '= btnContinue.Enabled = True
        End If '-amount-
        '--thats all-

    End Sub  '-validated-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->



    '-txtCreditNoteApplied_TextChanged-

    'Private Sub txtCreditNoteApplied_TextChanged(sender As Object, e As EventArgs) _
    '                                                           Handles txtCreditNoteApplied.TextChanged

    'End Sub '-txtCreditNoteApplied_TextChanged-
    ''= = = = = = = = = = = == = = = = = = == = =

    ''--AmountPaying_Validating-

    'Public Sub txtCreditNoteApplied_Validating(ByVal sender As System.Object, _
    '                                           ByVal ev As CancelEventArgs) _
    '                                            Handles txtCreditNoteApplied.Validating

    'End Sub '-txtCreditNoteApplied-
    ''= = = = = = = = = = == = = = = = 


    'Public Sub txtCreditNoteApplied_Validated(ByVal sender As System.Object, _
    '                                           ByVal ev As System.EventArgs) _
    '                                        Handles txtCreditNoteApplied.Validated

    'End Sub '- txtCreditNoteApplied_Validated-
    '= = = = = = = = = = == = = = = = = = == = 
    '-===FF->

    '--payments-

    '- optRefundCash_CheckedChanged-

    Private Sub optRefundCash_CheckedChanged(sender As Object, e As EventArgs) _
                                                        Handles optRefundCash.CheckedChanged, _
                                                        optRefundCredit.CheckedChanged
        If mbIsInitialising Then Exit Sub
        Dim opt1 As RadioButton = CType(sender, RadioButton)

        '- update invoice total--
        If opt1.Checked Then    '-just do it once..-
            Call mbUpdateTransactionTotal()
        End If

    End Sub  '-optRefundCash_CheckedChanged-
    '== = = = = = =  = = = = = = = = == = = =


    '-cancel-

    Private Sub btnCancel_Click(ByVal sender As System.Object, _
                                     ByVal e As System.EventArgs) _
                                    Handles btnCancel.Click, btnCancel2.Click

        Call mbClearTransaction()

        txtCustBarcode.Text = ""
        txtCustName.Text = ""
        txtCustPhone.Text = ""
        txtCustMobile.Text = ""
        txtCustEmail.Text = ""
        listPayments.Items.Clear()

        txtCustBarcode.Enabled = True
        DoEvents()
        txtCustBarcode.Select()

    End Sub  '-cancel-
    '= = = = =  = = = = = = =
    '-===FF->

    '--Commit-
    '--Commit- payment and all details..

    Private Sub btnCommit_Click(ByVal sender As System.Object, _
                                  ByVal e As System.EventArgs) Handles btnCommit.Click

        Dim sqlTransaction1 As OleDbTransaction
        '== Dim sMainTable, sLineTable As String
        Dim sSql, sFieldList, sValues As String
        Dim v2 As Object
        Dim bIsCredit As Boolean = (LCase(msTransactionType) = "refund")
        '== NB AccountPayments, the Payments Table has -1 in primary InvoiceNo=
        '=    See Disbursements table for related invoices for the Payment...
        Dim intInvoice_id As Integer = -1
        Dim intPayment_Id, intID As Integer
        Dim row1 As DataGridViewRow
        Dim sPayAmount, sDescription, sKey As String
        Dim decAmount, decInvoiceTotal, decTotalTax As Decimal
        '- save list of IP infos for payment refs..
        Dim listAccountPaymentInvoice_id As New List(Of Integer)
        Dim listOriginalInvoice_id As New List(Of Integer)
        Dim listOriginalInvoiceAmount As New List(Of Decimal)
        Dim dlgQueryCommit1 As dlgQueryCommit
        Dim sSelectedReceiptPrinterName As String = ""
        Dim bCanPrint, bCanEmail As Boolean

        mCnnSql.ChangeDatabase(msSqlDbName) '--USE our db..-

        '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
        '= NOT YET.. sqlTransaction1 = mCnnSql.BeginTransaction

        '==3301.611-  Add accountPayment Invoice line for each invoice being paid.
        '==     and drop the "mother" payment record..
        '==
        '==3301.622/623=
        '==  NO MORE IP records..-
        '- 2.   NO  --  INSERT accountPayment INVOICE Table Rows for all Invoices -.-
        '--        that are covered in this payment....
        '--  Check all rows in dgvInvoices..--

        If (dgvInvoices.Rows.Count > 0) Then
            '==  get all paying-now invoice contributions..
            '-- we need to compute Tax portion of payment credit.
            '= Dim decTaxCredit As Decimal
            '= Dim decSubTotalExCredit As Decimal
            '= colAccountPaymentInvoice_id = New Collection '--save invoice id's for allocation of cash..

            '==3301.622/623=
            '==  NO MORE IP records..-
            '--  Just pick up First invoice being paid..
            intInvoice_id = CInt(dgvInvoices.Rows(0).Cells(k_INVGRIDCOL_INV_NO).Value)

        Else '-none !!-
            MsgBox("No invoices were paid !", MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '-dgv rows count.-

        '=3403.1031=-- Confirm Commit..
        dlgQueryCommit1 = New dlgQueryCommit
        dlgQueryCommit1.labEmail.Text = ""
        If (msCustomerEmail <> "") And (msPdfPrinterName <> "") And (mbAllowEmailInvoices) Then
            dlgQueryCommit1.chkEmail.Enabled = True
            dlgQueryCommit1.chkEmail.Checked = False
            dlgQueryCommit1.labEmail.Text = msCustomerEmail
        Else '-no emai-
            dlgQueryCommit1.chkEmail.Enabled = False
            dlgQueryCommit1.chkEmail.Checked = False
        End If  '-email-
        dlgQueryCommit1.Text = "Committing Payment."
        dlgQueryCommit1.labMessage.Text = "Cash Change is : " & FormatCurrency(mDecChangeAsCash, 2) & ".."
        If (mDecChangeAsCredit > 0) Then
            dlgQueryCommit1.labMessage.Text &= vbCrLf & _
                          "Saved as CreditNote: " & FormatCurrency(mDecChangeAsCredit, 2) & ".."
        End If
        dlgQueryCommit1.labQuestion.Text = "OK to commit this Payment ?"
        dlgQueryCommit1.labDocType.Text = "receipt"

        '--  Make "auto" printing easy to flow on..
        dlgQueryCommit1.chkPrint.Checked = False   '=  NO- not for Stewart.. True
        '-show-
        dlgQueryCommit1.ShowDialog()
        If dlgQueryCommit1.DialogResult = DialogResult.Cancel Then
            Exit Sub
        Else  '-ok-
            '-save selections.
            '= sSelectedInvoicePrinterName = dlgQueryCommit1.selectedInvoicePrinter
            sSelectedReceiptPrinterName = dlgQueryCommit1.selectedReceiptPrinter
        End If
        '-ok=
        '--  Get checkbox results..
        '= MsgBox("TEST-  " & vbCrLf & "Email checked=" & dlgQueryCommit1.chkEmail.Checked & vbCrLf & _
        '=                 "Print checked= " & dlgQueryCommit1.chkPrint.Checked, MsgBoxStyle.Information)
        '- save print preferences..
        bCanPrint = dlgQueryCommit1.chkPrint.Checked
        bCanEmail = dlgQueryCommit1.chkEmail.Checked

        '==3301.611- ==  We NEED Payment record to hold details together for Pay Event. ==
        '- 2. INSERT Account Payment Record.. -
        '--  3403.1030-  Add DISCOUNT..
        '-
        '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
        '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
        '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
        sqlTransaction1 = mCnnSql.BeginTransaction


        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '==   Target-New-Build 4259 -- (Started 17-Jul-2020)
        '== -- Payments Form needs ReversedInvoices to be filtered out..

        '--IN TRANSACTION.. CHECK that nothing has been changed from OUTSIDE,
        '--  during this payments processing..

        Dim colInvoicesTrans, colInvoice1Trans As Collection
        Dim decGrossTotalInvoicesTrans As Decimal = 0
        Dim decGrossTotalOutstandingTrans As Decimal = 0
        '-  After deducting Reversed Invoices
        Dim decTotalInvoicesTrans As Decimal = 0
        Dim decTotalOutstandingTrans As Decimal = 0

        '= We need latest mColAllAccountReversals-
        Dim clsDebtors1 As clsDebtors
        Dim colAllAccountReversalsTrans As Collection
        Dim listInvoicesReversals As New List(Of Integer)
        Dim colReversedInvoicesTrans, colLiveInvoicesTrans As Collection
        Dim intInvNo As Integer

        clsDebtors1 = New clsDebtors(mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                         mColPrefsCustomer, msVersionPOS, mImageUserLogo, mIntStaff_id, msStaffName)
        If Not clsDebtors1.CollectAllAccountReversals(colAllAccountReversalsTrans, True, sqlTransaction1) Then
            '- error..  take it as fatal.
            sqlTransaction1.Rollback()
            Cursor = Cursors.Default
            mbIsLoadingCustomer = False
            MsgBox("ERROR- Payment session is being abandoned !", MsgBoxStyle.Exclamation)
            Call close_me()
            Exit Sub
        Else '==ok-
            '= s1 = ""  '= for test.
            For Each colRefund As Collection In mColAllAccountReversals
                listInvoicesReversals.Add(colRefund.Item("original_id"))
                's1 &= "Inv.No: " & colRefund.Item("original_id") & _
                '           " Amt: " & FormatNumber(colRefund.Item("total_inc"), 2) & vbCrLf
            Next
            '=MsgBox("Found " & mColAllAccountReversals.Count & " account invoices reversed.." & vbCrLf & s1, MsgBoxStyle.Information)
        End If
        '-- get all Invoices again and check none has been reversed or paid while we were busy with them.

        If Not gbCollectCustomerInvoices(mCnnSql, mIntCustomer_id, True, DateTime.Today, _
                                             colInvoicesTrans, decGrossTotalInvoicesTrans, decGrossTotalOutstandingTrans, , _
                                                                                                     True, sqlTransaction1) Then
            sqlTransaction1.Rollback()
            Cursor = Cursors.Default
            mbIsLoadingCustomer = False
            MsgBox("Payment session is being abandoned !", MsgBoxStyle.Exclamation)
            Call close_me()
            Exit Sub
        End If  '--collect-
        '== ok..  Match with list we started off with..
        '== First-  split off any Reversed invoices from latest list.
        colReversedInvoicesTrans = New Collection
        colLiveInvoicesTrans = New Collection
        For Each colInvoice1Trans In colInvoicesTrans
            If listInvoicesReversals.Contains(intInvoice_id) Then
                '-- Was reversed..
                colReversedInvoicesTrans.Add(colInvoice1Trans)
                Continue For   '--net invoice.
            Else
                '--alive=  collect as not reversed...
                intInvNo = CInt(colInvoice1Trans.Item("invoice_id"))
                colLiveInvoicesTrans.Add(colInvoice1Trans, CStr(intInvNo))
            End If
        Next colInvoice1Trans

        '-- Check that none of our invoices has been reversed or paid while we were busy with them.
        Dim decAmountOutstanding As Decimal
        Dim sProblem As String = ""

        For Each colGridInvoice1 As Collection In mColInvoices
            intInvNo = CInt(colGridInvoice1.Item("invoice_id"))
            decAmountOutstanding = CDec(colGridInvoice1.Item("amountOutstanding"))  '-original-
            If Not colLiveInvoicesTrans.Contains(CStr(intInvNo)) Then
                sProblem = "Invoice being processed is no longer active.."
            Else
                '-- still alive.. check is payments are made from outside..
                colInvoice1Trans = colLiveInvoicesTrans.Item(CStr(intInvNo))
                If (CDec(colInvoice1Trans.Item("amountOutstanding")) <> decAmountOutstanding) Then
                    sProblem = "Invoice Payments made from another Process..."
                End If
            End If  '-contains.
            If (sProblem <> "") Then
                sqlTransaction1.Rollback()
                Cursor = Cursors.Default
                mbIsLoadingCustomer = False
                MsgBox("Problem- Invoice No: " & intInvNo & vbCrLf & "    seems have been changed from another process.." & vbCrLf & _
                       sProblem & vbCrLf & _
                         "Nothing will be committed.." & _
                           "Payment session is being abandoned !", MsgBoxStyle.Exclamation)
                Call close_me()
                Exit Sub
            End If  '--problem-
        Next colGridInvoice1
        '--OK. all still alive..  and no outstandings have changed..

        '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)
        '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)
        '== END  Target-New-Build 4259 -- (Started 17-Jul-2020)



        labHelp.Text = "Invoice #" & intInvoice_id & ":  Saving Payment record.."
        sSql = "INSERT INTO dbo.payments ("
        sSql &= "  staff_id, customer_id, invoice_id, "
        sSql &= " transactionType, totalAmountReceived, "
        sSql &= " discountGivenOnPayment, changeGiven, nettAmountCredited, "
        sSql &= " creditNotePaymentCredited, creditNoteAmountDebited, "
        sSql &= "    terminal_id, cashDrawer, currentWindowsUserName, comments "
        sSql &= ") "
        sSql &= "VALUES ( "
        sSql &= CStr(mIntStaff_id) & ", " & CStr(mIntCustomer_id) & ", " & CStr(intInvoice_id) & ", "
        sSql &= "'Account', " & CStr(mDecPaymentTotalRcvd) & ", "
        sSql &= CStr(mDecTotalDiscount) & ", "
        sSql &= CStr(mDecChangeAsCash) & ", " & CStr(mDecPaymentNettCredited) & ", "
        '=3403.1017- Debtor can have Credit Note..
        sSql &= CStr(mDecChangeAsCredit) & ", " & CStr(mDecCreditNoteCreditApplying) & ", "
        sSql &= "'" & msComputerName & "', "
        sSql &= "'" & gsGetCurrentCashDrawer() & "', '" & gsFixSqlStr(msCurrentUserName) & "', "
        sSql &= "'AccountPayment: " & gsFixSqlStr(txtComments.Text) & "'"
        sSql &= "); "
        '-- Save-
        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
            labHelp.Text = "Saving Payment Record FAILED.."
            Exit Sub
        End If  '--exec invoice-

        '- 6. Retrieve Payment No. (IDENTITY of Payment record written.)-
        '= 3411.0129= -sSql = "SELECT CAST(IDENT_CURRENT ('dbo.payments') AS int);"
        '= 3411.0129= -SCOPE_IDENTITY-
        sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
        If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
            intPayment_Id = intID
            '-- update invoice display later..-
        Else
            MsgBox("Failed to retrieve latest Payment No..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        labHelp.Text = "Saving Payment details.."
        DoEvents()

        '- 3. FOR EACH Payment Detail: INSERT Payment Detail Row.-
        '- -
        If (dgvPaymentDetails.Rows.Count > 0) Then
            '--As we iterate through the payment-type list,
            '-   allocate payment types among accountPayment (IP) invoices as per RM.
            '-- This arbitrary allocation is what RM seems to do..
            '--    as in reality the total payment relates collectively to all invoices being paid.
            '-listOriginalInvoice_id-
            '- listOriginalInvoiceAmount-

            '== 3301.623==  NO MORE allocation here !! SEE Disbursements Table !!

            '== 3301.623== Dim intInvoiceListIndex As Integer = 0   '--track through IP items.
            '== 3301.623== Dim decCurrentInvoiceBalance As Decimal = listOriginalInvoiceAmount(0)  '-abal- starting balance.-
            '== 3301.623== Dim decCurrentPaymentBalance As Decimal  '-pBal-
            '== 3301.623== Dim intCurrentInvoice_id As Integer
            Dim decPaymentInsertAmount As Decimal  '-pBal-
            Dim sComments As String
            For Each row1 In dgvPaymentDetails.Rows
                sPayAmount = row1.Cells(k_PAYGRIDCOL_AMOUNT).Value
                sKey = Trim(row1.Cells(k_PAYGRIDCOL_PAYMENTTYPE_ID).Value)
                sDescription = row1.Cells(k_PAYGRIDCOL_PAYMENTTYPE_DESCR).Value
                sComments = "Account Payment: "
                If IsNumeric(sPayAmount) AndAlso (CDec(sPayAmount) > 0) Then
                    '-- If Cash line, then deduct change, if any..
                    '--  Detail lines must show nett receipts.
                    If (InStr(LCase(sKey), "cash") > 0) Then
                        '--cash paid line.-
                        sComments &= "TOTAL Cash tendered: " & FormatCurrency(CDec(sPayAmount), 2)
                        If (mDecChangeAsCash > 0) Then
                            sPayAmount = CStr(CDec(sPayAmount) - mDecChangeAsCash)
                        End If
                        sComments &= " Change: " & FormatCurrency(mDecChangeAsCash, 2) & "."
                    End If '-cash-
                    decPaymentInsertAmount = CDec(sPayAmount)
                    '-- allocate to current IP item..
                    'decCurrentPaymentBalance = CDec(sPayAmount)
                    ''== 3301.623== While (decCurrentPaymentBalance > 0) And (intInvoiceListIndex <= (listOriginalInvoice_id.Count - 1))
                    'intCurrentInvoice_id = listOriginalInvoice_id(intInvoiceListIndex)
                    'If (decCurrentPaymentBalance <= decCurrentInvoiceBalance) Then
                    '    '- allocate rest of payment to current invoice-
                    '    decPaymentInsertAmount = decCurrentPaymentBalance
                    '    decCurrentPaymentBalance = 0
                    'Else '- allocate PART of current payment to FILL current invoice-
                    '    decPaymentInsertAmount = decCurrentInvoiceBalance
                    '    decCurrentInvoiceBalance = 0
                    '    decCurrentPaymentBalance -= decPaymentInsertAmount  '-used this bit.--
                    'End If
                    '-- save detail (payment) line-
                    '== sSql = "INSERT INTO dbo.paymentDetails ("
                    sSql = "INSERT INTO dbo.paymentdetails ("
                    sSql &= "  payment_id,  paymentType_key, paymentType_descr, "
                    sSql &= "  amount, comments )"
                    sSql &= "  VALUES (" & CStr(intPayment_Id) & ", "
                    sSql &= "'" & gsFixSqlStr(sKey) & "', "
                    sSql &= "'" & gsFixSqlStr(sDescription) & "', "
                    sSql &= CStr(decPaymentInsertAmount) & ", '" & sComments & "' "
                    sSql &= "); "
                    '-- insert this row..-
                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                        labHelp.Text = "Insert Payment record Failed.."
                        Exit Sub
                    End If  '--exec INSERT pay detail LINE-
                    ''-- Jump to next IP invoice item if current "invoice" is "full"
                    'If (decCurrentInvoiceBalance <= 0) Then
                    '    intInvoiceListIndex += 1
                    '    If (intInvoiceListIndex <= (listOriginalInvoice_id.Count - 1)) Then
                    '        decCurrentInvoiceBalance = listOriginalInvoiceAmount(intInvoiceListIndex)
                    '    Else
                    '        '-- will drop out at the top of While-
                    '    End If
                    'End If
                    '== 3301.623== End While '-THIS PaymentBalance-

                End If  '-numeric-
            Next row1  '-dgvPaymentDetails.Rows-
        End If  '--row count.-

        '- 4. INSERT Payment-Disbursement Child Table Rows for all Invoices.-
        '--       covered in this payment....
        '--  Check all rows in dgvInvoices..--
        If dgvInvoices.Rows.Count > 0 Then
            Dim sSource As String = "Payment"  '= "No Refund Used."
            Dim decDiscount As Decimal
            'If (mColRefundsUsed.Count > 0) Then
            '    '==  get all REFUND invoice contributions..
            '    For Each colRefund As Collection In mColRefundsUsed
            '        sSource &= CStr(colRefund.Item("invoice_id")) & "; "
            '    Next colRefund
            'Else  '-paying now-
            '    '==  get paying-now id..
            '    sSource = "Payment # " & CStr(intPayment_Id) & "."
            'End If
            For Each row1 In dgvInvoices.Rows
                If (Trim(row1.Cells(k_INVGRIDCOL_PAYING_NOW).Value) <> "") Then
                    sSource = "Payment"
                    decAmount = CDec(row1.Cells(k_INVGRIDCOL_PAYING_NOW).Value)
                    intInvoice_id = CInt(row1.Cells(k_INVGRIDCOL_INV_NO).Value)
                    '-decInvoiceTotal, decTotalTax-
                    decInvoiceTotal = CDec(row1.Cells(k_INVGRIDCOL_INV_TOTAL).Value)
                    decTotalTax = CDec(row1.Cells(k_INVGRIDCOL_TAX_TOTAL).Value)
                    If (decAmount > 0) Then
                        '-- sav disb. detail line-
                        sSql = "INSERT INTO dbo.paymentDisbursements ("
                        sSql &= "  payment_id, invoice_id, tranCode, sourceOfFunds,  "
                        sSql &= "  amount )"
                        sSql &= "  VALUES (" & CStr(intPayment_Id) & ", " & CStr(intInvoice_id) & ", "
                        sSql &= "'Payment', '" & gsFixSqlStr(sSource) & "', "
                        sSql &= CStr(decAmount)
                        sSql &= "); "
                        '-- insert this row..-
                        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                            labHelp.Text = "Insert Pay-disbursement Failed.."
                            Exit Sub
                        End If  '--exec pay disb. LINE-
                        '==3301.622=
                        '==  NO MORE IP records..-
                    End If  '- >0 -
                End If  '-- amount-
                '=3403.1030=  Add discount record if needed.. decDiscount-
                If IsNumeric(row1.Cells(k_INVGRIDCOL_DISCOUNT).Value) AndAlso _
                                      (CDec(row1.Cells(k_INVGRIDCOL_DISCOUNT).Value) > 0) Then
                    sSource = "Discount"
                    decDiscount = CDec(row1.Cells(k_INVGRIDCOL_DISCOUNT).Value)
                    '-- sav disb. detail line-
                    sSql = "INSERT INTO dbo.paymentDisbursements ("
                    sSql &= "  payment_id, invoice_id, tranCode, sourceOfFunds,  "
                    sSql &= "  amount )"
                    sSql &= "  VALUES (" & CStr(intPayment_Id) & ", " & CStr(intInvoice_id) & ", "
                    sSql &= "'Discount', '" & gsFixSqlStr(sSource) & "', "
                    sSql &= CStr(decDiscount)
                    sSql &= "); "
                    '-- insert this row..-
                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                        labHelp.Text = "Insert Discount-disbursement Failed.."
                        Exit Sub
                    End If  '--exec discount disb. LINE-
                End If  '-discount-
            Next row1  '-invoices-
        End If  '-count-

        '== mColRefundsUsed --
        '= 3303.0111 =
        '-- 4.1 - INSERT  dbo.paymentRefundDetails  rows for any REFUND credits applied..

        '-  NB:  These Payment DETAIL Records only to keep track of applied Refund balance..
        '--    ALL Credits Disbursed to Debtors invoices
        '--      have been recorded in the main Payment Disbursements above..

        '=3403.1015= REFUNF STUFF ALL GONE =
        'If (mColRefundsUsed.Count > 0) Then
        '    For Each colRefund As Collection In mColRefundsUsed
        '        '-- save REFUND disb. detail line-
        '        sSql = "INSERT INTO dbo.paymentRefundDetails ("
        '        sSql &= "  payment_id, refundInvoice_id,  "
        '        sSql &= "  amountDisbursed )"
        '        sSql &= "  VALUES (" & CStr(intPayment_Id) & ", " & CStr(colRefund.Item("invoice_id")) & ", "
        '        sSql &= CStr(colRefund.Item("amount"))
        '        sSql &= "); "
        '        '-- insert this row..-
        '        If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
        '            labHelp.Text = "Insert paymentRefundDetails Failed.."
        '            Exit Sub
        '        End If  '--exec pay REFUND disb. LINE-
        '    Next  '-colRefund-
        'End If  '--- mColRefunds Used.Count-

        '- 5. -- Commit TRANSACTION.---
        Try
            sqlTransaction1.Commit()
            MsgBox(" Transaction committed ok.." & vbCrLf & _
                      "   Payment No: " & intPayment_Id, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Transaction commit FAILED.. " & intInvoice_id, MsgBoxStyle.Exclamation)
        End Try
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        labHelp.Text = ""

        '--  Print invoice..-
        '= 3403.1031= Show/print receipt  --

        Call mbShowPayment(intPayment_Id, sSelectedReceiptPrinterName, bCanEmail, bCanPrint)

        '-- clear grid.-
        Call mbClearTransaction()

        txtCustBarcode.Text = ""
        txtCustName.Text = ""
        txtCustPhone.Text = ""
        txtCustMobile.Text = ""
        txtCustEmail.Text = ""

        '== Call mbLoadPaymentsList(mIntCustomer_id)
        listPayments.Items.Clear()

        txtCustBarcode.Enabled = True
        DoEvents()
        txtCustBarcode.Select()

    End Sub '--Commit-
    '= = = = = = = = = = = 
    '-- show Statement-
    '-===FF->

    '- close-

    Private Sub close_me()
        '= Dim bCancel As Boolean = False '= = EventArgs.Cancel
        '= Dim intMode As System.Windows.Forms.CloseReason = EventArgs.CloseReason

        '- inform parent.-
        '- Report to Parent..-

        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

        'If Not bCancel Then  '--exiting.
        '     If Not (Me.delReport Is Nothing) Then
        '        delReport.Invoke(Me.Name, "FormClosed", "")
        '    End If
        'End If  '-cancel-
        'Me.Dispose()
    End Sub '--close me-
    '= = = = = = == = = = =


End Class  '-frmPayments-
'= = = = = = = = = = = = = 

'== end form ==