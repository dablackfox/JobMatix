
Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'== Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
'== Imports system.data.sqlclient
Imports System.Data.OleDb
Imports System.ComponentModel
Imports System.Threading

Public Class ucChildSubscription

    '-- grh= 20-December-2017-
    '--  started Subscriptions.. (cloned from Goods Lookup)...

    '-- 3411.1126-  Created to Show/Create/Maint Subscriptions..
    '==    -- 3411.1223  And Show/Release Subs for invoicing..
    '==
    '==    -- 3411.0224=  24-Feb-2018=
    '==        -- Fixes to Subscriptions..  
    '==            Add Context menu to Edit Line Items (Price/Qty)... 
    '==            Add Sub. EndDate (optional).
    '==
    '==   >> 3411.0304=  04-Mar-2018=
    '==      -- More Fixes to Browsing..  Drop default selection
    '==           And Fix Index 8. crash.
    '==
    '==   >> 3411.0404=  04-Apr-2018=
    '==      -- Fixes to Cancel Sub (was bad column name.)
    '==
    '==
    '==   >> 3519.0214=  14-Feb-2019=
    '==      -- Fixes to crash in Select Sub from from ENTER on Find text box.
    '==
    '==   Updated.- 3519.0307  Started 05-March-2019= 
    '==     -- Update to Subscriptions Invoicing Log to add Mark/Unmark checkbox to Subs rows, 
    '==            and to implement the Multiple Invoice-all-Marked function....
    '==             And to use frmBrowse33 for lookups..
    '==
    '==
    '==   Updated.- 3519.0319  Started 14-March-2019= 
    '==    --  Subscriptions..  Add button to show StockAdmin form.
    '==
    '==   Updated.- 3519.0325  Started 25-March-2019= 
    '==    -- If No email address for customer, Invoice anyway, and report "Not Emailed".
    '==        
    '==
    '==   Updated.- 3519.0331  Started 30-March-2019= 
    '==    -- Subscriptions.. When Reporting Subs/Custs with no email, 
    '==                 DON'T show the actual list in the MsgBox popup. .
    '==    
    '==
    '== - - - - RELEASED as 3519.0414 --
    '==  
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==    NEW VERSION 4.2  FIRST Version 4.
    '==
    '==    First New Build- 4201.0416 -
    '==    New Build- 4201.0416 -  Started 16-April-2019.
    '==
    '==   Updated.- 4201.0428- 
    '==     --for TDI Admin forms are now in Tabs inside Main Form...
    '==    -- 4201.0426. TDI Child forms now converted to UserControls.... 
    '==  
    '==    -- 4201.0618/0623.  11/18-June-2019-   
    '==         --  Subscriptions-  Fix crash happening after cancelling a Customer Lookup.. 
    '==
    '==
    '== NEW revision-
    '==
    '==    -- 4201.0929.  Started 19-September-2019-
    '==        -- Subscriptions-  Lock Invoicing loop (higher level transaction),
    '==                   and the SubscriptionInvoices Table to keep out competitors.
    '==        -- Add column to Subscriptions Table: "OkToEmailInvoices bit default 0"
    '==        -- Report any Subs that are termination in this cycle..

    '== NEW revision-
    '==
    '==    -- 4201.1007.  07-October-2019-
    '==          --  4201.1007-  Catch Enter Key on Subs srch text- Execute the Search. 
    '=== = = = = 
    '==   Updates to 4233.0421  Started 24-April-2020= 
    '==   Updates to 4233.0421  Started 24-April-2020= 
    '==
    '==  Target is new Build 4234..
    '==  Target is new Build 4234..
    '==
    '==   Subscriptions- Add new TreeView subclass, and add an "analysis" panel to Subs Form to show Product/Sub-Cust
    '==               Query Result..
    '==
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = === = = = = = =
    '==
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==
    '==   -- Subscriptions.
    '==        (1)  Add filter to Product Analysis to screen out non-activated and cancelled Subs.  (Martin email 11-Aug-2020)
    '==        (2)  Add code to invoicing to show list of checked items about to be invoiced to get confirmation.. 
    '==                 (Check/Fix why unchecked items get invoiced !!)
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    '-listview columns-
    Private Const k_ListViewIndexItemNo As Integer = 0
    Private Const k_ListViewIndexItemBarcode As Integer = 1
    Private Const k_ListViewIndexCat1 As Integer = 2
    Private Const k_ListViewIndexDescription As Integer = 3
    Private Const k_ListViewIndexPrice As Integer = 4
    Private Const k_ListViewIndexQty As Integer = 5
    Private Const k_ListViewIndexAmount As Integer = 6
    Private Const k_ListViewIndexStockId As Integer = 7
    Private Const k_ListViewIndexTaxCode As Integer = 8

    '- row depth-
    Private Const k_SUBS_LIST_ROW_HEIGHT As Integer = 34  '--pixels.

    '== 3411.0224=  24-Feb-2018=
    '--- For suppressing default popup menu..-
    '--- For suppressing default popup menu..-
    Private Declare Function LockWindowUpdate Lib "user32" (ByVal hwndLock As Integer) As Integer
    '= = = = = = = = = = = = = = = = = = = = = = = =


    Private mbActivated As Boolean = False
    Private mbIsLoading As Boolean = True

    Private mIntFormDesignHeight As Integer '-- starting dimensions..-
    Private mIntFormDesignWidth As Integer '-- starting dimensions..-

    '--inputs--
    Private msVersionPOS As String = ""
    Private msComputerName As String = ""
    Private msAppFullname As String = ""
    Private msAppPath As String = ""

    Private msServer As String
    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection '--

    '- SHAPE cnn for us here only-
    '--  needed for Invoicing Log..
    Private mCnnShape As OleDbConnection   '=  ADODB.Connection

    Private mSysInfo1 As clsSystemInfo
    Private mDecGST_rate As Decimal = 10D    '--default. value-  get from setup.
    Private mIntStaff_id As Integer = -1
    Private msStaffName As String = ""

    Private msCurrentUserName As String = ""  '=3301.1211= 11ec2016=
    Private msBusinessABN As String = ""
    Private msBusinessName As String = ""
    Private msEmailTextInvoice As String = "Invoice Attached"  '--default value-
    Private msEmailQueueSharePath As String = ""


    Private msDefaultPrinterName As String = ""
    Private msPdfPrinterName As String = ""
    Private mbAllowEmailInvoices As Boolean = False

    '- Stock list now in dataGridView -
    Private mColPrefsSubs As Collection
    Private mColPrefsStock As Collection
    Private mColPrefsCustomer As Collection

    Private mImageUserLogo As Image

    Private mBrowse1 As clsBrowse3
    Private mLngSelectedRow As Integer = -1

    Private mIntSubscription_id As Integer = -1
    '= = = = = = = = = = = = = = = = = = = = = =

    '--Editing Sub..
    '--  If current sub changed..
    Private mbDataModified As Boolean = False
    Private mbStartDateChanged As Boolean = False
    Private mbActivateSubNow As Boolean = False
    Private mbItemsModified As Boolean = False

    Private mbHasEndDate As Boolean = False
    Private mDateTerminationDate As Date
    Private mbIsTerminated As Boolean = False

    '-- Subs Items Listview..

    Private mIntListViewIndexStockId As Integer = 7
    Private mIntListViewIndexQty As Integer = 4

    '- New Sub customer..

    Private mIntNewSubCustomer_id As Integer = -1
    Private msNewSubCustomerBarcode As String = ""
    Private msNewSubCustomerName As String = ""

    '-  Maint panel- Current Subscription on Show--
    Private mDataTableSubscription As DataTable
    Private mDataTableSubInvoices As DataTable

    '-  Invoicing Log panel- Subscription in Grid for Invoicing..-
    '-- all ACTIVATED Subs
    Private mColSubsInvoiceLog As Collection

    '-- Get subs that need to be invoiced..
    Dim mColSubsToInvoice As Collection  '--collects them for loading the grid..

    '=3411.0224- Context menu for Editing Line Items.
    '--  Popup menu for Right click on parts..-
    '--  Popup menu for Right click on parts..-

    '--  Popup menu for Right click on parts..-
    Private mContextMenuPartInfo As ContextMenu
    Private WithEvents mnuEditPrice As New MenuItem("Edit Line Price.")
    Private WithEvents mnuEditQuantity As New MenuItem("Edit Line Quantity.")
    '= Private WithEvents mnuCopyPartSerialNo As New MenuItem("Copy Part SerialNo.")
    '= Private WithEvents mnuPartMenuSep1 As New MenuItem("-")
    '= Private WithEvents mnuDeletePart As New MenuItem("Delete item.")

    '==3519.0307  05-March-2019=
    '--  Group Invoicing..
    '--  Group Invoicing..
    '--  Group Invoicing..

    Private mIntMarkedGroupCount As Integer = 0
    Private mbGroupInvoicingInProgress As Boolean = False

    Private mbGroupCancelRequested As Boolean = False
    '= Private msPdfPrinterName As String = ""
    Private mbGroupInvoicingEnabled As Boolean = False
    '= Private msEmailTextInvoice As String = "Invoice  attached."
    Private msInvoiceFilePath As String = ""

    '-- wait form--
    Private mFormWait1 As frmWait

    '= = = = = = = = = = = = = = = = = = = = = = = = = = == = =

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport
    '= = = = = = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- P r o p e r t i e s --
    '-- P r o p e r t i e s --

    WriteOnly Property VersionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
            '= labVersion.Text = msVersionPOS
        End Set
    End Property
    '= = = = = = = = = = = = = = 

    'WriteOnly Property SupplierName() As String
    '    Set(ByVal value As String)
    '        labSupplier.Text = value
    '    End Set
    'End Property
    '= = = = = = = = = = = = = = 

    WriteOnly Property SqlServer() As String
        Set(ByVal Value As String)
            msServer = Value
        End Set
    End Property '--server-
    '= = = = = =  = = =  = =

    WriteOnly Property ComputerName() As String
        Set(ByVal Value As String)
            msComputerName = Value
        End Set
    End Property '--this computer-
    '= = = = = =  = = =  = = = = = =

    WriteOnly Property connectionSql() As OleDbConnection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =

    WriteOnly Property dbInfoSql() As Collection
        Set(ByVal Value As Collection)
            mColSqlDBInfo = Value
        End Set
    End Property '--info sql jobs..--
    '= = = = = = =  = = = = =

    WriteOnly Property DBname() As String
        Set(ByVal Value As String)
            msSqlDbName = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = 

    WriteOnly Property staff_id() As Integer
        Set(ByVal Value As Integer)
            mIntStaff_id = Value
        End Set
    End Property  '-staff_id-
    '= = = = = = = = = = = = = = = = = 

    WriteOnly Property staffNname() As String
        Set(ByVal Value As String)
            msStaffName = Value
            '= labStaffName.Text = Value
        End Set
    End Property
    '= = = = = = = = = = = = = = = = = 

    '-- set user logo for printing..--
    WriteOnly Property UserLogo() As Image
        Set(ByVal Value As Image)
            mImageUserLogo = Value
        End Set
    End Property '--logo..--
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)
    '= = = = = = = = = = = = = = = = = = = = == = 


    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                            ByVal intParentHeight As Integer)

        'Me.Width = intParentWidth - 11
        'Me.Height = intParentHeight  '= - 11
        ''-- resize our controls..
        'DoEvents()
        ''-- resize main box and top panel-
        'TabControl1.Height = Me.Height - 11 '= (Me.Height - 120)
        'TabControl1.Width = (Me.Width - 12)

        ''= btnStockAdmin.Left = panelGoodsBanner.Width - btnStockAdmin.Width - 5
        'grpBoxSubsLookup.Width = TabPageSubs.Width - 7 '= 17
        'grpBoxSubsLookup.Height = TabPageSubs.Height - 11

        'grpBoxSub.Left = grpBoxSubsLookup.Width - grpBoxSub.Width - 3
        'grpBoxSub.Height = grpBoxSubsLookup.Height - 17

        'grpBoxSubDetail.Height = grpBoxSub.Height - 80

        'listViewSubsItems.Width = grpBoxSubDetail.Width - 13
        'listViewSubsItems.Height = grpBoxSubDetail.Height - listViewSubsItems.Top - 11  '= 411

        'frameBrowse.Width = grpBoxSubsLookup.Width - grpBoxSub.Width - 17
        'frameBrowse.Height = grpBoxSubsLookup.Height - frameBrowse.Top - 12

        'dgvSubsList.Height = frameBrowse.Height - dgvSubsList.Top - 18
        'dgvSubsList.Width = frameBrowse.Width - 17

        ''-invoice tab-
        'panelBillingHdr.Width = TabControl1.Width - 7
        'txtLogStatus.Left = panelBillingHdr.Width - txtLogStatus.Width - 25  '--add for scroll bar width ?..

        'grpBoxBilling.Width = TabPageInvoicing.Width - 7
        'grpBoxBilling.Height = TabPageInvoicing.Height - 11

        'panelBillingHdr.Width = grpBoxBilling.Width - 8

        'dgvInvoices.Width = panelBillingHdr.Width  '= TabControl1.Width - 17
        'dgvInvoices.Height = grpBoxBilling.Height - panelBillingHdr.Height - 12

        ''-- Analysis Page..
        'Dim intAnalysisWidth = TabPageAnalysis.Width - 12
        'panelProducts.Width = intAnalysisWidth \ 3   '-- one third for the tree.
        'panelProducts.Height = TabPageAnalysis.Height - 12
        'clsPosTreeViewProducts.Width = panelProducts.Width - 5
        'clsPosTreeViewProducts.Height = panelProducts.Height - clsPosTreeViewProducts.Top - 11

        'panelPeriodReport.Left = panelProducts.Left + panelProducts.Width + 7
        'panelPeriodReport.Width = intAnalysisWidth - panelProducts.Width - 11
        'panelPeriodReport.Height = TabPageAnalysis.Height - 12
        'dgvPeriodReport.Width = panelPeriodReport.Width - 12
        'dgvPeriodReport.Height = panelPeriodReport.Height - dgvPeriodReport.Top - 7


    End Sub '-SubFormResized
    '= = = = = = = = = = = =  === =

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click..
    '=- Return true if ok to Close.

    Public Function SubFormCloseRequest() As Boolean

        SubFormCloseRequest = True
        '==Me.Close()
        '= Call close_me()
    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String)

        mFormWait1 = New frmWait
        mFormWait1.waitTitle = msVersionPOS
        mFormWait1.labHdr.Text = "JobMatixPOS.."
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

    '-- Add msg to txtReport.text--

    Private Function mbReport(ByVal sMsg As String) As Boolean

        txtLogStatus.AppendText(sMsg & vbCrLf)
        '= txtLogStatus.Focus()
        txtLogStatus.SelectionStart = txtLogStatus.TextLength + 1
        txtLogStatus.SelectionLength = 0
        '== txtReport.Select()

    End Function  '-report-
    '= = = = = = = = = = = = = = =

    '-- de-code the billing period..

    '- input is "nW xxxx"   or "nM xxxx"
    '-- Output intWeeks is >0  OR intMonths is >0  

    Private Function mbDecodeBillingPeriod(ByVal strBillingPeriod As String, _
                                              ByRef intWeeks As Integer, _
                                              ByRef intMonths As Integer) As Boolean
        mbDecodeBillingPeriod = False
        Dim sPeriod, s1, s2 As String

        intWeeks = -1
        intMonths = -1

        Dim sParts() As String = Split(strBillingPeriod)
        If (sParts.Length > 0) Then
            sPeriod = UCase(Trim(sParts(0)))  '--eg 1W or 12M  --
            If sPeriod.Length >= 2 Then
                s2 = VB.Right(sPeriod, 1)
                If (s2 = "W") Or (s2 = "M") Then  '-ok-n
                    '- get length of period..
                    s1 = VB.Left(sPeriod, (sPeriod.Length - 1))
                    If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                        mbDecodeBillingPeriod = True
                        If (s2 = "W") Then '-weeks-
                            intWeeks = CInt(s1)
                        Else  '-- months--
                            intMonths = CInt(s1)
                        End If
                    End If  '-numeric-
                End If  '-W-M-
            End If  '->2-
        End If  '-parts.
    End Function '--mbDecodeBillingPeriod-
    '= = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Browse CUSTOMER or STOCK Lookup table-  using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    '=3519.0309= NOW using frmBrowse33.. (class if frmBrowse)

    Private Function mbBrowseTable33(ByRef colPrefs As Collection, _
                                       ByRef sTitle As String, _
                                      ByRef sWhere As String, _
                                      ByRef colKeys As Collection, _
                                      ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Customer") As Boolean
        '= Dim frmBrowse1 As New frmBrowsePOS
        Dim frmBrowse1 As frmBrowse

        frmBrowse1 = New frmBrowse

        mbBrowseTable33 = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        'If bHideEditButtons Then  '=3403.715- Default has changed-
        '    frmBrowse1.lookupSelection = True
        '    frmBrowse1.HideEditButtons = True
        'Else '--need to edit..
        '    frmBrowse1.lookupSelection = False
        '    frmBrowse1.HideEditButtons = False
        'End If
        '= frmBrowse1.lookupSelection = True

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If (frmBrowse1.selectedKey IsNot Nothing) AndAlso (frmBrowse1.selectedKey.Count > 0) AndAlso _
                    (frmBrowse1.selectedRow IsNot Nothing) AndAlso (frmBrowse1.selectedRow.Count > 0) Then
            '--  get selected record key..--
            '= MsgBox("TESTING- Looking up Customers.. GOT RESULT", MsgBoxStyle.Information)
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseTable33 = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()
    End Function '--browse.--
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-- Browse CUSTOMER or STOCK Lookup table-  using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    'Private Function mbBrowseTable(ByRef colPrefs As Collection, _
    '                            ByRef sTitle As String, _
    '                              ByRef sWhere As String, _
    '                              ByRef colKeys As Collection, _
    '                              ByRef colSelectedRow As Collection, _
    '                                 Optional ByVal sTableName As String = "Customer", _
    '                                 Optional ByVal bHideEditButtons As Boolean = False) As Boolean
    '    Dim frmBrowse1 As New frmBrowsePOS

    '    mbBrowseTable = False
    '    frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
    '    frmBrowse1.colTables = mColSqlDBInfo
    '    frmBrowse1.DBname = msSqlDbName
    '    frmBrowse1.tableName = sTableName '--"jobs"
    '    frmBrowse1.IsSqlServer = True '--bIsSqlServer

    '    '--- set WHERE condition for jobStatus..--
    '    frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
    '    frmBrowse1.PreferredColumns = colPrefs
    '    frmBrowse1.Title = sTitle
    '    If bHideEditButtons Then  '=3403.715- Default has changed-
    '        frmBrowse1.lookupSelection = True
    '        frmBrowse1.HideEditButtons = True
    '    Else '--need to edit..
    '        frmBrowse1.lookupSelection = False
    '        frmBrowse1.HideEditButtons = False
    '    End If
    '    frmBrowse1.lookupSelection = True

    '    frmBrowse1.ShowDialog()
    '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
    '    If Not frmBrowse1.cancelled Then
    '        '--  get selected record key..--
    '        colKeys = frmBrowse1.selectedKey
    '        colSelectedRow = frmBrowse1.selectedRow
    '        mbBrowseTable = True
    '    End If
    '    frmBrowse1.Close()
    '    frmBrowse1.Dispose()
    'End Function '--browse.--
    '= = = = = = = = = = = = = = =
    '-===FF->


    '=3519.0307=
    '--  Mark all for Group Invoicing..
    '--  Can Mark only if Can Email to Cust.

    Private Function mIntMarkAllInvoices() As Integer
        Dim intMarkedCount = 0
        Dim sEmail As String
        mIntMarkAllInvoices = 0

        If (dgvInvoices.Rows.Count > 0) Then  '--has row -
            For Each datagridrow1 As DataGridViewRow In dgvInvoices.Rows
                With datagridrow1
                    sEmail = Trim(.Cells("customer_email").Value)
                    'If (sEmail <> "") AndAlso (InStr(sEmail, "@") > 1) Then  '-has email-
                    '=3519.0325= Mark all, even if no Email..
                    .Cells("MarkToInvoice").Value = True '==   Target-New-Build-4262 -- 1  '--check-
                    intMarkedCount += 1
                    'End If  '-email-
                End With
            Next datagridrow1
            mIntMarkAllInvoices = intMarkedCount
            '== Target-New-Build-4262 -- (Started 14-Aug-2020)
            dgvInvoices.EndEdit()
            '== END Target-New-Build-4262 -- (Started 14-Aug-2020)

        End If  '-count-
    End Function  '-mark all-
    '= = = = = = = = = = == = = 

    '-Un-mark all-

    Private Function mIntUnMarkAll() As Integer
        Dim intMarkedCount = 0
        mIntUnMarkAll = 0

        If (dgvInvoices.Rows.Count > 0) Then  '--has row -
            For Each datagridrow1 As DataGridViewRow In dgvInvoices.Rows
                With datagridrow1
                    .Cells("MarkToInvoice").Value = False '==   Target-New-Build-4262 -- '--UN-check-
                    intMarkedCount += 1
                End With
            Next datagridrow1
            mIntUnMarkAll = intMarkedCount
            '== Target-New-Build-4262 -- (Started 14-Aug-2020)
            dgvInvoices.EndEdit()
            '== END Target-New-Build-4262 -- (Started 14-Aug-2020)
        End If  '-count-
    End Function  '-Un-mark all-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '==   Updates to 4233.0421  Started 24-April-2020= 
    '==   Updates to 4233.0421  Started 24-April-2020= 
    Private nodeFont1 As New Font("Lucida Console", 8)
    '==
    '==  Target is new Build 4234..
    '==  Target is new Build 4234..

    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==
    '==   -- Subscriptions.
    '==       Add filter to Product Analysis to screen out non-activated and cancelled Subs.  (Martin email 11-Aug-2020)


    Private Function mbRefreshSubsTree() As Boolean
        Dim sServer, sConnect, sErrors As String
        Dim sShapeSql, sSql, s1, s2, sAmt, sIdList As String
        Dim intRecordsAffected, ix As Integer
        Dim dtStockIDs As DataTable
        '=  Dim colGridInvoice As Collection
        Dim rdrSubsLines, rdrProducts, rdrInvoices As OleDbDataReader
        Dim cmd1 As OleDbCommand

        '-- For the period Report-
        Dim colPeriodReport As Collection
        Dim colPeriodBarcodeLists As Collection '-collection of sorted lists.

        Dim periodList As SortedList(Of String, String)
        Dim barcodeList As SortedList(Of String, String)
        Dim sPeriod As String
        Dim colPeriod, colProduct As Collection


        mbRefreshSubsTree = False

        '-- FIRST get list of DISTINCT Stock-ids used in the Subs-Line table..
        sSql = " SELECT DISTINCT stock_id  "  '=Descr not unique..=, stock_description "
        sSql &= "  FROM dbo.SubscriptionLine AS SL "
        '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
        '--     FILTER for Acrive only..
        sSql &= " JOIN subscription AS SUB ON (SL.subscription_id = SUB.subscription_id) "
        sSql &= "   WHERE (isCancelled=0) AND (isActivated <>0)  "
        sSql &= "    ORDER BY stock_id; "

        If Not gbGetDataTable(mCnnSql, dtStockIDs, sSql) Then
            MsgBox("Error in getting recordset for Subscription Lines table: " & vbCrLf & _
                                 gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function
        ElseIf ((dtStockIDs Is Nothing)) OrElse (dtStockIDs.Rows.Count <= 0) Then
            MsgBox("No Subscription Lines data", MsgBoxStyle.Exclamation)
            Exit Function
        Else
            '-ok=
        End If  '--get-
        '-- make a list of all stock Id's in subs lines.. 
        sIdList = ""
        For Each datarow1 As DataRow In dtStockIDs.Rows
            If sIdList <> "" Then
                sIdList &= ", "
            End If
            sIdList &= datarow1.Item("stock_id")
        Next
        dtStockIDs.Dispose()

        '-Testing-
        '= MsgBox("DISTINCT stock_ids are: " & vbCrLf & sIdList)

        sServer = mCnnSql.DataSource
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mCnnShape = New OleDbConnection '=  ADODB.Connection
        sConnect = "Provider=MSDataShape; Server=" & sServer & "; Trusted_Connection=true; Integrated Security=SSPI; "
        sConnect = sConnect & "; Data Provider=SQLOLEDB; Data Source=" & sServer & "; "
        '=== sConnect = sConnect + "; Password=" + msSqlPwd + "; User ID=" + msSqlUid
        If gbConnectSql(mCnnShape, sConnect) Then
            '--FrameReport.Enabled = True   '--show report options frame..--
            '--FrameStatus.Enabled = True
            '-OK-
        Else
            MsgBox(" 'mbRefreshSubsTree'- Login to sql SHAPE dataSource has failed." & vbCrLf, MsgBoxStyle.Exclamation)
            '====FrameReport.Enabled = False
            '= Me.Hide()
            '==End
            Exit Function
        End If '--connected-
        If Not gbExecuteCmd(mCnnShape, "USE " & msSqlDbName & vbCrLf, intRecordsAffected, sErrors) Then
            MsgBox(vbCrLf & "Failed USE for SHAPE connection to DATABASE: '" & _
                                            msSqlDbName & "'.." & vbCrLf & sErrors, MsgBoxStyle.Critical)
        End If '--use-

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '-- get distinct PRODUCTS..  Defines be DISTINCT id-list
        sShapeSql = " SHAPE {"
        sShapeSql &= " SELECT  stock_id, stock.barcode AS stock_barcode, stock.description AS stock_description "
        sShapeSql &= "    FROM dbo.stock "
        sShapeSql &= "  WHERE stock_id IN (" & sIdList & ") "
        sShapeSql &= "    ORDER BY stock_description "
        sShapeSql &= " }"  '--end of main SELECT..-
        '-- APPEND a child..
        '--1. subs lines-
        sShapeSql &= " APPEND ( { SELECT *, SUB.billingPeriod, SL.stock_id AS SL_stock_id,  "
        sShapeSql &= "  stock.barcode AS stock_barcode, stock.description AS stock_description,  "
        sShapeSql &= " customer.barcode AS customer_barcode, "
        sShapeSql &= "  customerName = CASE companyName  "
        sShapeSql &= "     WHEN '' THEN (customer.lastname + '.' + customer.firstname)"
        sShapeSql &= "     WHEN 'n/a' THEN (customer.lastname + '.' + customer.firstname)"
        sShapeSql &= "     ELSE companyName "
        sShapeSql &= " END  "  '--case- Include barcode in Name.
        sShapeSql &= "  FROM SubscriptionLine AS SL  "
        sShapeSql &= "    JOIN stock ON (SL.stock_id = stock.stock_id) "
        sShapeSql &= "    JOIN subscription AS SUB ON (SL.subscription_id = SUB.subscription_id) "
        sShapeSql &= "    JOIN customer ON (customer.customer_id=SUB.customer_id) "
        sShapeSql &= "   ORDER BY customerName } "
        sShapeSql &= "     RELATE stock_id TO SL_stock_id "
        sShapeSql &= "   ) AS rsSubsLines "

        '-- start retrieval-
        Call mWaitFormOn("Pls Wait. Getting all Subscription product Info.." & vbCrLf & _
                           "   This might take a moment.")
        Try
            '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
            cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
            cmd1.CommandTimeout = 10   '-seconds-
            rdrProducts = cmd1.ExecuteReader
        Catch ex As Exception
            Call mWaitFormOff()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Error getting Subscription records SHAPED recordset..." & vbCrLf & _
                   ex.Message & vbCrLf & vbCrLf & _
                     "Pls report error to supplier.", MsgBoxStyle.Exclamation)
            '= dgvGoodsList.Enabled = True
            Exit Function
        End Try
        Call mWaitFormOff()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '-  load products to Tree..
        '==// Suppress repainting the TreeView until all the objects have been created.
        clsPosTreeViewProducts.BeginUpdate()
        '== get Root Node.
        Dim nodeRoot As TreeNode = clsPosTreeViewProducts.Nodes("nodeRoot")
        nodeRoot.Nodes.Clear()

        '-- period Report for Grid analysis of billingPeriod projections.
        colPeriodReport = New Collection
        colPeriodBarcodeLists = New Collection

        periodList = New SortedList(Of String, String)

        Dim sDescription, sBarcode As String
        Dim colSubsLines, colSubInfo As Collection

        If rdrProducts.HasRows Then
            Dim node1 As TreeNode

            '-- read through the distince product list.
            Do While rdrProducts.Read
                sDescription = rdrProducts.Item("stock_description")
                sBarcode = rdrProducts.Item("stock_barcode")
                node1 = nodeRoot.Nodes.Add(sBarcode, _
                                           LSet(sDescription, 24) & " [" & sBarcode & "]")  '-barcode is Key..
                node1.NodeFont = nodeFont1
                colSubsLines = New Collection

                '-- Read all Subs from Child rdr rsSubs, and make a collection to store in Node Tag.
                '-- ie all the subs lines that have this product.
                '== Also, Sort into Periods..
                If TypeOf rdrProducts.Item("rsSubsLines") Is IDataReader Then
                    rdrSubsLines = rdrProducts.Item("rsSubsLines")
                    If rdrSubsLines.HasRows Then
                        Do While rdrSubsLines.Read
                            colSubInfo = New Collection
                            '-billingPeriod-
                            sPeriod = UCase(rdrSubsLines.Item("billingPeriod"))
                            colSubInfo.Add(sPeriod, "billingPeriod")
                            colSubInfo.Add(rdrSubsLines.Item("customer_barcode"), "customer_barcode")
                            colSubInfo.Add(rdrSubsLines.Item("customerName"), "customerName")
                            colSubInfo.Add(rdrSubsLines.Item("subscription_id"), "subscription_id")
                            colSubInfo.Add(rdrSubsLines.Item("stock_barcode"), "stock_barcode")
                            colSubInfo.Add(rdrSubsLines.Item("stock_description"), "stock_description")
                            colSubInfo.Add(rdrSubsLines.Item("stock_id"), "stock_id")
                            colSubInfo.Add(rdrSubsLines.Item("sellActual_inc"), "sellActual_inc")
                            colSubInfo.Add(rdrSubsLines.Item("quantity"), "quantity")
                            colSubsLines.Add(colSubInfo)

                            '-- Save this Subs Line in Period Analysis..
                            If colPeriodReport.Contains(sPeriod) Then
                                colPeriod = colPeriodReport.Item(sPeriod)
                                If colPeriod.Contains(sBarcode) Then
                                    colProduct = colPeriod.Item(sBarcode)
                                Else
                                    '-- make sub-sub collection for New product.
                                    colProduct = New Collection
                                    colPeriod.Add(colProduct, sBarcode)
                                    '--save new barcode insoted list this period.
                                    barcodeList = colPeriodBarcodeLists(sPeriod)
                                    barcodeList.Add(sBarcode, sBarcode)
                                End If
                            Else
                                '-make sub-collection for period.
                                colPeriod = New Collection
                                colPeriodReport.Add(colPeriod, sPeriod)
                                '- save key also
                                periodList.Add(sPeriod, sPeriod)  '-is key AND value. (to retrieve keys.)
                                barcodeList = New SortedList(Of String, String)
                                '-- collect sorted barcode lists for Period.
                                colPeriodBarcodeLists.Add(barcodeList, sPeriod)
                                '-- make sub-sub collection for product.
                                colProduct = New Collection
                                colPeriod.Add(colProduct, sBarcode)
                                barcodeList.Add(sBarcode, sBarcode)
                            End If
                            '-  now save this SubsLine in the appropriate sorted spot.
                            colProduct.Add(colSubInfo)
                        Loop  '-read-
                    End If  '-has rows-
                    rdrSubsLines.Close()
                End If  '-rdr type=
                '--save-
                node1.Tag = colSubsLines
                '- add subs nodes to product Node..
                If (colSubsLines.Count > 0) Then
                    For Each colSubInfo In colSubsLines
                        sAmt = FormatCurrency(colSubInfo.Item("sellActual_inc"), 2)
                        s1 = "Sub#" & LSet(CStr(colSubInfo.Item("subscription_id")), 5)
                        s1 &= ":" & LSet(VB.Left(colSubInfo.Item("billingPeriod"), 3), 4)
                        s1 &= RSet(sAmt, 8)
                        s1 &= "; " & LSet(VB.Left(colSubInfo.Item("customerName"), 24), 24) & _
                                                              " [" & colSubInfo.Item("customer_barcode") & "]"
                        '= s1 &= " (" & FormatCurrency(colSubInfo.Item("sellActual_inc"), 2) & ");"
                        node1.Nodes.Add(s1)
                    Next colSubInfo
                End If
                '= node1 .Expand 
            Loop  '-While rdrProducts.Read-
        End If '-rdrProducts.HasRows-
        rdrProducts.Close()
        nodeRoot.Expand()

        clsPosTreeViewProducts.EndUpdate()

        Dim sReport As String = ""   '-Display for testing..
        '= listPeriodReport.Items.Clear()
        dgvPeriodReport.Rows.Clear()

        '==  NOW show period/product analysis in Grid.  (listBox for testing..)
        Dim decPeriodTotal, decProductTotal, decSubSell, decLineExt As Decimal
        Dim decPeriodQtyTotal, decProductQty, decLineQty As Decimal
        Dim gridRow1 As DataGridViewRow
        Dim intRowX As Integer

        For Each sPeriod In periodList.Keys
            colPeriod = colPeriodReport.Item(sPeriod)
            decPeriodTotal = 0
            decPeriodQtyTotal = 0
            barcodeList = colPeriodBarcodeLists(sPeriod)
            For Each sBarcode In barcodeList.Keys '= colProduct In colPeriod
                colProduct = colPeriod.Item(sBarcode)
                decProductTotal = 0
                decProductQty = 0
                For Each colSubInfo In colProduct
                    sDescription = colSubInfo.Item("stock_description")
                    sReport = sPeriod & "- " & colSubInfo.Item("stock_barcode") & " Sub:" & _
                                                       LSet(colSubInfo.Item("subscription_id"), 5) & " " & _
                                                       sDescription & vbCrLf
                    '= listPeriodReport.Items.Add(sReport)
                    decSubSell = CDec(colSubInfo.Item("sellActual_inc"))  '-sellActual_inc.
                    decLineQty = CDec(colSubInfo.Item("quantity"))  '-Qty this Line..
                    decLineExt = decSubSell * decLineQty

                    decProductQty += decLineQty
                    decPeriodQtyTotal += decLineQty

                    decProductTotal += decLineExt
                    decPeriodTotal += decLineExt
                Next colSubInfo

                '-- Add a Grid Row for this Barcode..
                intRowX = dgvPeriodReport.Rows.Add()
                gridRow1 = dgvPeriodReport.Rows(intRowX)
                With gridRow1
                    .Cells("billingPeriod").Value = sPeriod
                    .Cells("stock_barcode").Value = sBarcode
                    .Cells("stock_description").Value = sDescription
                    .Cells("total_qty").Value = FormatNumber(decProductQty, 2)
                    .Cells("totalSellActual_inc").Value = FormatNumber(decProductTotal, 2)
                End With
                '= listPeriodReport.Items.Add("-- Product Total: " & FormatCurrency(decProductTotal, 2) & "--")
            Next sBarcode '=colProduct

            '--add a grid row for period Total..
            '= listPeriodReport.Items.Add("==Period Total:  " & FormatCurrency(decPeriodTotal, 2) & "   ====")

            intRowX = dgvPeriodReport.Rows.Add()
            gridRow1 = dgvPeriodReport.Rows(intRowX)
            With gridRow1
                .Cells("stock_barcode").Value = "-- Period Total: "   '=& FormatCurrency(decProductTotal, 2) & "--")
                .Cells("total_qty").Value = FormatNumber(decPeriodQtyTotal, 2)
                .Cells("totalSellActual_inc").Value = FormatCurrency(decPeriodTotal, 2)
            End With

            '- Blank row After Period Total.
            intRowX = dgvPeriodReport.Rows.Add()
            gridRow1 = dgvPeriodReport.Rows(intRowX)
            gridRow1.Cells("totalSellActual_inc").Value = "-------"

        Next sPeriod
        '= MsgBox("Period Report is :" & vbCrLf & sReport)
        '--add a grid row for== the end=..
        intRowX = dgvPeriodReport.Rows.Add()
        gridRow1 = dgvPeriodReport.Rows(intRowX)
        With gridRow1
            .Cells("stock_barcode").Value = "== The End == "   '=& FormatCurrency(decProductTotal, 2) & "--")
            '== .Cells("totalSellActual_inc").Value = FormatCurrency(decPeriodTotal, 2)
        End With

    End Function '-mbRefreshSubsTree-
    '= = = = = = = = = = = = == = = = = = = =
    '= = = = = = = = = = = = = = =
    '-===FF->
    '==  END OF PREP FOR- Target 4234 - Updates to 4233.0421  Started 24-April-2020= 
    '==  END OF PREP FOR- Target 4234 - Updates to 4233.0421  Started 24-April-2020= 
    '==  END OF PREP FOR- Target 4234 - Updates to 4233.0421  Started 24-April-2020= 



    '--- INITIALISE Goods Browser.for SUBSCRIPTIONS Lookup--

    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(ByVal sTableName As String, _
                                        Optional ByVal sSrchWhereCond As String = "") As Boolean
        Dim sSelectSql As String
        Dim sWhere As String = ""
        Dim colWidthWeights As Collection

        If (mBrowse1 Is Nothing) Then
            mBrowse1 = New clsBrowse3 '== clsBrowse22
        End If

        mBrowse1.connection = mCnnSql  '= mRetailHost1.connection
        mBrowse1.colTables = mColSqlDBInfo '= mRetailHost1.colTables 
        mBrowse1.IsSqlServer = True   '= mRetailHost1.IsSqlServer
        mBrowse1.DBname = msSqlDbName  '= mRetailHost1.DBname

        '--  get table/prefs info for this host..--
        mBrowse1.tableName = sTableName  '= "customer"  '==sHostTablename
        mBrowse1.DataGrid = dgvSubsList

        '--needs a join..
        sSelectSql = "SELECT subscription_id, "
        sSelectSql &= "  customerName = CASE companyName  "
        sSelectSql &= "     WHEN '' THEN (customer.firstname + ' ' + customer.lastname)"
        sSelectSql &= "     WHEN 'n/a' THEN (customer.firstname + ' ' + customer.lastname)"
        sSelectSql &= "     ELSE companyName "
        sSelectSql &= "   END, "
        sSelectSql &= " billingPeriod, isActivated, start_date, termination_date AS end_date, "
        sSelectSql &= " customer.barcode AS cust_barcode, subscription.customer_id "
        sSelectSql &= " FROM subscription "
        sSelectSql &= "  JOIN customer ON (customer.customer_id=subscription.customer_id) "
        '== not allowed here..sSelectSql &= "  WHERE (subscription.isCancelled=0) "

        sWhere = " (subscription.isCancelled=0) "
        mBrowse1.UserSelectList = sSelectSql
        mBrowse1.InitialOrder1 = "subscription_id"
        mBrowse1.InitialOrderIsDescending = False

        '-- add col width weights for seven cols.
        colWidthWeights = New Collection
        colWidthWeights.Add(50)  '-subs-id-
        colWidthWeights.Add(120)  'cust name-
        colWidthWeights.Add(50)  '-bill period-
        colWidthWeights.Add(60)  '-isAcivated-
        colWidthWeights.Add(100)  '-start date-
        colWidthWeights.Add(100)  '-end date-
        colWidthWeights.Add(80)  '-cust barcode-
        colWidthWeights.Add(50)  '-cust-id-
        mBrowse1.colColumnWidthWeights = colWidthWeights

        '--  pass controls..--
        mBrowse1.showRecCount = labRecCount '--updates rec. retrieval..
        mBrowse1.showFind = LabFind '--updates Sort Column display..
        mBrowse1.showTextFind = txtFind '--updates Sort Column display..
        '= sWhere = msMakeStockFilter()  '--service or not..-
        '-- add srch args..
        If (sSrchWhereCond <> "") Then
            If (sWhere <> "") Then
                sWhere &= " AND "
            End If
            sWhere &= sSrchWhereCond
        End If
        mBrowse1.WhereCondition = sWhere
        mBrowse1.PreferredColumns = mColPrefsSubs '== mColPrefs
        mBrowse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly
        frameBrowse.Enabled = True

        mLngSelectedRow = -1
        Try
            mBrowse1.Activate() '-- go..--
        Catch ex As Exception
            MsgBox("Browser Activate failed.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        '== txtFind.Focus()
        '-- set row height to make checkboxes visible.
        For Each row1 As DataGridViewRow In dgvSubsList.Rows
            row1.Height = k_SUBS_LIST_ROW_HEIGHT
            row1.MinimumHeight = k_SUBS_LIST_ROW_HEIGHT
        Next 'row1

        '=3411.0304=
        '= dgvSubsList.ClearSelection()  '==Now in clsBrowse34 -

        System.Windows.Forms.Application.DoEvents()

        dgvSubsList.Select()

    End Function '--init-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--
    '--   IN-HOUSE browser version.--

    Private Function mbBrowseSubsTable(Optional ByRef sSrchWhereCond As String = "") As Boolean
        Dim sWhere As String = ""

        Call mbInitialiseBrowse("Subscription", sSrchWhereCond)

        'If (mBrowse1 Is Nothing) Then
        '    Call mbInitialiseBrowse("Subscription")
        'Else
        '    sWhere = " (subscription.isCancelled=0) "
        '    '-- add srch args..
        '    If (sSrchWhereCond <> "") Then
        '        If sWhere <> "" Then
        '            sWhere &= "AND "
        '        End If
        '        sWhere &= sSrchWhereCond
        '    End If
        '    mBrowse1.WhereCondition = sWhere '-- sWhere -
        '    '== mBrowse1.refresh()
        '    '==3103-203==
        '    mBrowse1.Activate()  '==3103-203==
        'End If

        ''-- set row height to make checkboxes visible.
        'For Each row1 As DataGridViewRow In dgvSubsList.Rows
        '    row1.Height = 60
        '    row1.MinimumHeight = 60
        'Next 'row1
        'txtFind.Focus()
        System.Windows.Forms.Application.DoEvents()
    End Function  ''--mbBrowseSubsTable--
    '= = = = = =  = == =
    '-===FF->

    Private Sub mSubClearShowSub()

        labSubsId.Text = ""
        labAction.Text = ""

        '= txtSubCustBarcode.Text = ""
        labShowActivated.Text = ""
        'labShowCustName.Text = ""
        'labShowEnd.Text = ""
        'labShowStart.Text = ""
        'labShowPeriod.Text = ""
        labShowTotal.Text = ""
        txtComments.Text = ""

        btnAddItem.Enabled = False
        btnDeleteItem.Enabled = False

        txtSubCustBarcode.Text = ""
        labSubCustomerName.Text = ""

        grpBoxSubDetail.Enabled = False

        listViewSubsItems.Items.Clear()

        labLastPeriodBilled.Text = ""

    End Sub '-clear-
    '= = = = = =  = == =

    '-mbCheckNewSub-

    Private Function mbCheckSaveOk() As Boolean
        Dim intDays As Integer

        If txtSubCustBarcode.Enabled Then '-- NEW-=  grpBoxSubDetail.Enabled Then
            mbCheckSaveOk = False
            intDays = DateDiff(DateInterval.Day, Today, dtPickerStart.Value)
            If (cboBillingCycle.SelectedIndex >= 0) And (intDays >= 0) And _
                                                                 (listViewSubsItems.Items.Count > 0) Then
                btnSaveEdit.Enabled = True
                mbCheckSaveOk = True
            Else
                btnSaveEdit.Enabled = False
            End If '-check-
        Else  '--edit-
            If mbDataModified AndAlso (listViewSubsItems.Items.Count > 0) Then
                btnSaveEdit.Enabled = True
                mbCheckSaveOk = True
            End If
        End If  '- new /edit-
    End Function  '-mbCheckNewSub-
    '= = = = = = = = = = = = = = = = = == 
    '-===FF->

    '--  mSubShowTotalSubvalue-

    Private Sub mSubShowTotalSubvalue()
        Dim decTotal As Decimal = 0

        If (listViewSubsItems.Items.Count > 0) Then
            For Each item1 As ListViewItem In listViewSubsItems.Items
                If IsNumeric(item1.SubItems(k_ListViewIndexAmount).Text) Then
                    decTotal += CDec((item1.SubItems(k_ListViewIndexAmount).Text))
                End If  '-numeric-
            Next item1
        End If  '-count-
        labShowTotal.Text = FormatCurrency(decTotal, 2)

    End Sub '- mSubShowTotalSubvalue-
    '= = = = = = = = = = = = = = = = = == 
    '-===FF->

    '- get all line items from listView..
    '-- Buils Sql INSERT list..

    Private Function msMakeItemsInsertSql(intSubscription_id As Integer) As String
        Dim sSql As String = ""
        Dim decAmount As Decimal

        If (listViewSubsItems.Items.Count > 0) Then
            For Each item1 As ListViewItem In listViewSubsItems.Items
                sSql &= "INSERT INTO dbo.SubscriptionLine "
                sSql &= "  ( Subscription_id, stock_id, stock_barcode, stock_description, sellActual_inc, quantity) "
                sSql &= "  VALUES( " & CStr(intSubscription_id) & ","
                '- stock_id, stock_barcode, description, sellActual_ins, quantity) "
                '= With listViewSubsItems
                sSql &= item1.SubItems(k_ListViewIndexStockId).Text & ", "   '- stock_id-
                sSql &= "'" & item1.SubItems(k_ListViewIndexItemBarcode).Text & "', "  '-barcode-
                sSql &= "'" & gsFixSqlStr(item1.SubItems(k_ListViewIndexDescription).Text) & "', "    '-"Description"-

                '-  Can't have commas in the texted Amount fld..
                decAmount = CDec(item1.SubItems(k_ListViewIndexPrice).Text)
                '= sSql &= item1.SubItems(k_ListViewIndexPrice).Text & ", "    '-"sellActual_inc"-
                sSql &= CStr(decAmount) & ", "    '-"sellActual_inc"-
                sSql &= item1.SubItems(k_ListViewIndexQty).Text & "); " & vbCrLf  '-"quantity"-
                '= End With
            Next item1
        End If
        msMakeItemsInsertSql = sSql

    End Function '-msMakeItemsInsertSql-
    '= = = = = = = == = = = = = = = == 
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

    '-- Execute SQL Command..--
    '-- Execute SQL Command..--
    '-- Always IN Transaction, 
    '--   NO RollBack in the event of failure..-

    Private Function mbExecSqlCmd(ByRef cnnSql As OleDbConnection, _
                                     ByVal sSql As String, _
                                      ByRef sqlTran1 As OleDbTransaction) As Boolean
        Dim sqlCmd1 As OleDbCommand
        Dim intAffected As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbExecSqlCmd = False
        Try
            sqlCmd1 = New OleDbCommand(sSql, cnnSql)
            sqlCmd1.Transaction = sqlTran1
            '= mCnnSql.ChangeDatabase(msSqlDbName)
            intAffected = sqlCmd1.ExecuteNonQuery()
            mbExecSqlCmd = True   '--ok--
            '== MsgBox("Sql exec ok. " & intAffected & " records affected..", MsgBoxStyle.Information)
        Catch ex As Exception
            '= lAffected = lError
            'If bIsTransaction Then
            '    sqlTran1.Rollback()
            sRollback = vbCrLf & "RollBack NOT executed here.." & vbCrLf
            'End If
            sMsg = "ERROR: " & ex.Message & vbCrLf & "=="
            sErrorMsg = "mbExecSqlCmd: Error in Executing Sql: " & vbCrLf & _
                      sMsg & vbCrLf & "SQL was:" & vbCrLf & sSql & vbCrLf & sRollback & _
                       "--- end of error msg.--" & vbCrLf
            '== If gbDebug Then MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsErrorLogPath, sErrorMsg)
            '= msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecSqlCmd-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Invoice Log..  Refresh Subs. Info.
    '==
    '==   Updated.- 3519.0325  Started 25-March-2019= 
    '==    -- If No email address for customer, Invoice anyway, and report "Not Emailed".
    '==        

    Private Function mbRefreshInvoiceLog() As Boolean
        Dim sSql, sShapeSql, s1, sErrorMsg, sList, sEmailsMissing As String
        Dim sSubsTerminating As String = ""
        '= Dim dataTable1 As DataTable
        '= Dim goodsrow1 As DataRow
        Dim rdrSubs, rdrItems, rdrInvoices As OleDbDataReader
        Dim cmd1 As OleDbCommand

        mbRefreshInvoiceLog = False
        '-- get all subscriptions (incl Lines/Invoices). that have an invoicing event due..

        sShapeSql = " SHAPE {  "
        sShapeSql &= "  SELECT subscription_id, "
        sShapeSql &= "  customerName = CASE companyName  "
        sShapeSql &= "     WHEN '' THEN (customer.firstname + ' ' + customer.lastname)"
        sShapeSql &= "     WHEN 'n/a' THEN (customer.firstname + ' ' + customer.lastname)"
        sShapeSql &= "     ELSE companyName "
        sShapeSql &= "   END, "
        sShapeSql &= "    billingPeriod, OkToEmailInvoices, start_date, termination_date,  "
        sShapeSql &= "    customer.barcode AS cust_barcode, customer.email AS cust_email, subscription.customer_id "
        sShapeSql &= " FROM subscription "
        sShapeSql &= "  JOIN customer ON (customer.customer_id=subscription.customer_id) "
        sShapeSql &= "    WHERE  (subscription.start_date IS NOT NULL)  "
        sShapeSql &= "         AND (isActivated<>0)  AND (isCancelled=0) "
        sShapeSql &= "          ORDER BY start_date ASC "
        sShapeSql &= " }"  '--end of main SELECT..-
        '-- APPEND two children..
        '--1. subs lines-
        sShapeSql &= " APPEND ( { SELECT * FROM SubscriptionLine AS SL  "
        sShapeSql &= "          JOIN stock ON (stock.stock_id=SL.stock_id) } "
        '== sShapeSql &= "   WHERE (SL.subscription_id=Subscription.subscription_id)  } "
        sShapeSql &= "     RELATE subscription_id TO subscription_id) AS rsSubsItems, "
        '-- 2. invoiced lines-
        sShapeSql &= " ( { SELECT SI.subscription_id, SI.invoice_id, "
        sShapeSql &= "    invoice_period_start_date, invoice_period_end_date, invoice_date, total_inc "
        sShapeSql &= "   FROM SubscriptionInvoice AS SI  "
        sShapeSql &= "   JOIN invoice ON (SI.invoice_id=invoice.invoice_id) ORDER BY invoice_period_end_date DESC } "
        '= sShapeSql &= "   WHERE (SI.subscription_id=Subscription.subscription_id)  } "
        sShapeSql &= "     RELATE subscription_id TO subscription_id) AS rsSubsInvoices "  '-end of APPEND-
        '-end of APPEND-

        '-- start retrieval-
        Call mWaitFormOn("Pls Wait. Getting all Subscriptions Info.." & vbCrLf & _
                           "   This might take a moment.")
        Try
            '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
            cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
            cmd1.CommandTimeout = 10   '-seconds-
            rdrSubs = cmd1.ExecuteReader
        Catch ex As Exception
            Call mWaitFormOff()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Error getting Subscription records SHAPED recordset..." & vbCrLf & _
                   ex.Message & vbCrLf & vbCrLf & _
                     "Another User may be doing Subscriptions Invoicing.." & vbCrLf & _
                     "Try again in a little while.", MsgBoxStyle.Exclamation)
            '= dgvGoodsList.Enabled = True
            Exit Function
        End Try
        Call mWaitFormOff()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Dim intSubs_id As Integer = 0
        Dim colSub, colItems, colInvoices, col1 As Collection
        Dim sEmail, sName As String
        Dim bOkToEmailInvoices As Boolean

        sEmailsMissing = ""
        mColSubsInvoiceLog = New Collection

        If rdrSubs.HasRows Then
            Do While rdrSubs.Read
                '-- bypass if no email address.
                intSubs_id = rdrSubs.Item("subscription_id")
                sEmail = Trim(rdrSubs.Item("cust_email"))
                sName = Trim(rdrSubs.Item("customerName"))
                '=4201.0929=
                bOkToEmailInvoices = False
                If (CInt(rdrSubs.Item("OkToEmailInvoices")) <> 0) Then
                    bOkToEmailInvoices = True
                End If

                '=4201.0929=
                If bOkToEmailInvoices And ((sEmail = "") OrElse ((InStr(sEmail, "@") <= 0))) Then
                    'MsgBox("Customer " & sName & _
                    '       " is missing a valid email address, and can't be invoiced. !!!.", MsgBoxStyle.Exclamation)
                    sEmailsMissing &= "-- The Customer " & sName & " (Subs.Id # " & intSubs_id & ") " & _
                           " has no valid email address.. Invoice will be created on account, but not emailed.." & vbCrLf
                    '= 3519.0325= Invoice it ANYWAY..  Continue Do  '=Next rdr ite,.
                End If
                '-- ok..  take this sub.
                colSub = New Collection
                colInvoices = New Collection
                colItems = New Collection    '--for each sub..
                colSub.Add(rdrSubs.Item("subscription_id"), "subscription_id")
                colSub.Add(rdrSubs.Item("billingPeriod"), "billingPeriod")
                '=4201.0929=
                colSub.Add(bOkToEmailInvoices, "OkToEmailInvoices")
                colSub.Add(rdrSubs.Item("start_date"), "start_date")
                colSub.Add(rdrSubs.Item("termination_date"), "termination_date")
                colSub.Add(rdrSubs.Item("customer_id"), "customer_id")
                colSub.Add(rdrSubs.Item("cust_barcode"), "cust_barcode")
                colSub.Add(rdrSubs.Item("cust_email"), "cust_email")
                colSub.Add(rdrSubs.Item("customerName"), "customerName")

                '-- pick up line items.
                If TypeOf rdrSubs.Item("rsSubsItems") Is IDataReader Then
                    '-- has items list.
                    rdrItems = rdrSubs.Item("rsSubsItems")
                    If rdrItems.HasRows Then
                        Do While rdrItems.Read
                            col1 = New Collection    '--for each sub..
                            col1.Add(rdrItems.Item("stock_id"), "stock_id")
                            col1.Add(rdrItems.Item("stock_barcode"), "stock_barcode")
                            col1.Add(rdrItems.Item("cat1"), "cat1")
                            col1.Add(rdrItems.Item("description"), "description")
                            '= sCreateSql &= "  goods_taxCode nvarChar(7) NOT NULL DEFAULT '',"
                            '= sCreateSql &= "  costExTax MONEY NOT NULL DEFAULT 0,"
                            col1.Add(rdrItems.Item("goods_taxCode"), "goods_taxCode")
                            col1.Add(rdrItems.Item("costExTax"), "costExTax")
                            col1.Add(rdrItems.Item("quantity"), "quantity")
                            col1.Add(rdrItems.Item("Sales_taxCode"), "Sales_taxCode")
                            col1.Add(rdrItems.Item("sellExTax"), "sellExTax")
                            '=3411.0224=- sellActual from listView.
                            col1.Add(rdrItems.Item("sellActual_inc"), "sellActual_inc")

                            colItems.Add(col1)
                        Loop  '-rdrItems-
                    End If  '-rdrItems.HasRows-
                    rdrItems.Close()
                End If  '-rdr type=
                '-- pick up sub invoices.
                If TypeOf rdrSubs.Item("rsSubsInvoices") Is IDataReader Then
                    '- has invoices-
                    rdrInvoices = rdrSubs.Item("rsSubsInvoices")
                    If rdrInvoices.HasRows Then
                        Do While rdrInvoices.Read
                            col1 = New Collection    '--for each sub..
                            col1.Add(rdrInvoices.Item("invoice_id"), "invoice_id")
                            col1.Add(rdrInvoices.Item("subscription_id"), "subscription_id")
                            col1.Add(rdrInvoices.Item("invoice_period_start_date"), "invoice_period_start_date")
                            col1.Add(rdrInvoices.Item("invoice_period_end_date"), "invoice_period_end_date")
                            colInvoices.Add(col1)
                        Loop '-invoices-
                    End If  '-has rows-
                    rdrInvoices.Close()
                End If  '-rdr type-

                colSub.Add(colItems, "lineItems")
                colSub.Add(colInvoices, "invoices")
                mColSubsInvoiceLog.Add(colSub, CStr(intSubs_id))
            Loop '-rdrSubs-
        End If   ' subs- has rows.
        rdrSubs.Close()

        dgvInvoices.Rows.Clear()

        '-- Get subs that need to be invoiced..
        mColSubsToInvoice = New Collection  '--collects them for loading the grid..
        Dim colSubx As Collection
        Dim sPrevPeriodStart, sPrevPeriodEnd As String
        Dim intdays As Integer
        '- For temination date-
        Dim bHasTerminatingDate, bIsTerminated As Boolean
        Dim dateTerminatingDate As Date

        '-- First, get all subs that are DUE to be invoiced, 
        '--   and as yet have no invoices sent.
        If (mColSubsInvoiceLog.Count > 0) Then
            For Each col1 In mColSubsInvoiceLog
                bHasTerminatingDate = False
                bIsTerminated = False
                If Not IsDBNull(col1.Item("termination_date")) Then
                    bHasTerminatingDate = True
                    dateTerminatingDate = CDate(col1.Item("termination_date"))
                    '- check if terminated.
                    '- NOT YET-
                    'If (DateDiff(DateInterval.Day, Today, dateTerminatingDate) <= 0) Then
                    '    mbIsTerminated = True
                    'End If
                End If  '-has end date.

                colInvoices = (col1.Item("invoices"))
                '-check if invoices-
                sPrevPeriodStart = "" : sPrevPeriodEnd = ""
                If (colInvoices.Count <= 0) Then
                    '--check start date--
                    intdays = CInt(DateDiff("d", CDate(col1.Item("start_date")), Today))
                    '-testing-
                    'MsgBox("Sub: " & col1.Item("Subscription_id") & vbCrLf & _
                    '         "Start_date: " & Format(CDate(col1.Item("start_date")), "dd-MMM-yyyy") & vbCrLf & _
                    '         "Today: " & Format(CDate(Today), "dd-MMM-yyyy") & vbCrLf & _
                    '          "Int Days: " & intdays, MsgBoxStyle.Information)

                    '-- CAN'T be terminated if not billed yet !!!
                    If (intdays >= 0) Then
                        '= And (Not bIsTerminated) Then '= DateDiff("d", CDate(col1.Item("start_date")), Today) >= 0 Then  
                        '- due to start-
                        colSubx = New Collection
                        colSubx.Add(col1.Item("Subscription_id"), "Subscription_id")
                        colSubx.Add(col1.Item("start_date"), "new_bill_period_start_date")
                        colSubx.Add(sPrevPeriodStart, "PrevPeriodStart")
                        colSubx.Add(sPrevPeriodEnd, "PrevPeriodEnd")
                        '=4201.0929= Save Term.date if any.
                        colSubx.Add(bHasTerminatingDate, "HasTerminationDate")
                        If bHasTerminatingDate Then
                            colSubx.Add(dateTerminatingDate, "termination_date")
                        End If
                        mColSubsToInvoice.Add(colSubx, CStr(col1.Item("Subscription_id")))
                    End If  '-date diff-
                Else  '-has prev.invoices..
                    '-- check if invoicing due again now..
                    '--- ie  check for invoicing repeats needed..  FIND latest invoiced Period !! -
                     '- BUT !!  SQL Query HAS alreday ordered invoices BY: "invoice_period_end_date"DESC-
                    Dim colInv1 As Collection = colInvoices.Item(1)  '-first is latest-
                    Dim datePrevStart As Date = CDate(colInv1.Item("invoice_period_start_date"))
                    sPrevPeriodStart = Format(datePrevStart, "dd-MMM-yyyy")
                    Dim datePrevPeriodEnd As Date = CDate(colInv1.Item("invoice_period_end_date"))
                    sPrevPeriodEnd = Format(datePrevPeriodEnd, "dd-MMM-yyyy")
                    '=4201.1003-- It's terminated if last bill period included term.date.
                    If bHasTerminatingDate Then
                        If (DateDiff("d", dateTerminatingDate, datePrevPeriodEnd) >= 0) Then
                            bIsTerminated = True
                        End If  '-termin.
                    End If  '-has date-
                    If (DateDiff("d", datePrevPeriodEnd, Today) >= 0) And _
                                         (Not bIsTerminated) Then  '- Is due to start-
                        colSubx = New Collection
                        colSubx.Add(colInv1.Item("Subscription_id"), "Subscription_id")
                        colSubx.Add(DateAdd("d", 1, datePrevPeriodEnd), "new_bill_period_start_date")  '-new billing from next day..
                        colSubx.Add(sPrevPeriodStart, "PrevPeriodStart")
                        colSubx.Add(sPrevPeriodEnd, "PrevPeriodEnd")
                        '=4201.0929= Save Term.date if any.
                        colSubx.Add(bHasTerminatingDate, "HasTerminationDate")
                        If bHasTerminatingDate Then
                            colSubx.Add(dateTerminatingDate, "termination_date")
                        End If
                        mColSubsToInvoice.Add(colSubx, CStr(col1.Item("Subscription_id")))
                    End If  '-diff-
                End If  '-invoice count-
            Next col1
        End If  '-count-

        '-- Load data Grid.. dgvInvoices -
        sSubsTerminating = ""
        '--  Guided by "mColSubsToInvoice"--
        txtLogStatus.Text &= vbCrLf & "Loading Invoicing Grid." & vbCrLf

        If (mColSubsToInvoice.Count > 0) Then
            Dim sBillPeriod As String
            Dim intWeeks, intMonths As Integer   '-- period type and length-
            Dim sDueDate, sNewPeriodEndDate, sNewPeriodInfo As String
            Dim dateDueDate, dateNewPeriodEndDate As Date
            Dim dateNextPeriodAfterThisPeriodEndDate As Date
            Dim rx, intSubId1, intCustomer_id As Integer
            Dim gridRow1 As DataGridViewRow
            '-- For line extensions..
            '-- Compute Invoice totals..
            Dim sGoodsTaxCode As String
            Dim decCostExTax, decCostTaxAmount, decCostInc As Decimal
            Dim sSalesTaxCode As String
            Dim decSellExTax, decItemQty As Decimal
            '-- Compute Invoice totals, and
            '-   update these for INSERTING the line items..
            Dim decSellExLineTotal As Decimal
            Dim decSellExLineTotal_taxable, decSellExLineTotal_non_taxable As Decimal
            Dim decSellTaxAmount, decSellIncTax As Decimal
            Dim decLineTotalEx, decLineTotalTax, decExtension, decGrossProfit As Decimal
            Dim decInvoiceTotal As Decimal
            Dim colBillItems As Collection '-make new collection of extended items for Invoice..-
            Dim sCustomerName As String

            For Each colBillSub As Collection In mColSubsToInvoice
                decInvoiceTotal = 0
                colBillItems = New Collection
                intSubId1 = colBillSub.Item("Subscription_id")
                sPrevPeriodStart = colBillSub.Item("PrevPeriodStart")
                sPrevPeriodEnd = colBillSub.Item("PrevPeriodEnd")
                dateDueDate = colBillSub.Item("new_bill_period_start_date")
                sDueDate = Format(colBillSub.Item("new_bill_period_start_date"), "dd-MMM-yyyy")
                '-get main subscription INFO-
                colSub = mColSubsInvoiceLog.Item(CStr(intSubId1))  '-get main subscription-
                intCustomer_id = colSub.Item("customer_id")
                colItems = colSub.Item("lineItems")
                sNewPeriodEndDate = ""
                sBillPeriod = colSub.Item("billingPeriod")
                If mbDecodeBillingPeriod(sBillPeriod, intWeeks, intMonths) Then
                    If intWeeks > 0 Then
                        dateNewPeriodEndDate = DateAdd(DateInterval.Day, (intWeeks) * 7, dateDueDate)
                        '-- compute following period also so we know if termination now.
                        dateNextPeriodAfterThisPeriodEndDate = DateAdd(DateInterval.Day, (intWeeks) * 7, dateNewPeriodEndDate)
                    Else '-months-
                        dateNewPeriodEndDate = DateAdd(DateInterval.Month, intMonths, dateDueDate)
                        '-- compute following period also so we know if termination now.
                        dateNextPeriodAfterThisPeriodEndDate = DateAdd(DateInterval.Month, intMonths, dateNewPeriodEndDate)
                    End If
                    '- go back one day to get end-day of the period..
                    dateNewPeriodEndDate = DateAdd(DateInterval.Day, -1, dateNewPeriodEndDate)
                    sNewPeriodEndDate = Format(dateNewPeriodEndDate, "dd-MMM-yyyy")
                Else
                    '-invalid period ?-
                    MsgBox("Warning- Subscription no. " & intSubId1 & vbCrLf & _
                            "can't be billed due to invalid billing period. (" & sBillPeriod & ")..", MsgBoxStyle.Exclamation)
                    Continue For  '-goto next..
                End If
                '-- show period to be billed in Grid..
                sNewPeriodInfo = "From " & sDueDate & " To " & sNewPeriodEndDate
                '- update -colBillSub-
                colBillSub.Add(dateNewPeriodEndDate, "new_bill_period_end_date")  '-new billing from next day..
                sCustomerName = colSub.Item("customerName") & " (" & colSub.Item("cust_email") & ")"
                '-4201.0929=  Check if terminating this cycle.
                If colBillSub.Item("HasTerminationDate") Then
                    Dim dateTerm As Date = colBillSub.Item("termination_date")
                    If DateDiff("d", dateTerm, dateNextPeriodAfterThisPeriodEndDate) > 0 Then
                        '-temination falls before the end of next period..  so this is the last.
                        sSubsTerminating &= "Subs.No: " & intSubId1 & "; " & sCustomerName & vbCrLf
                    End If
                End If  '-terminates.

                gridRow1 = New DataGridViewRow
                dgvInvoices.Rows.Add(gridRow1)
                rx = dgvInvoices.Rows.Count - 1  '--last row -
                With dgvInvoices.Rows(rx)
                    .Height = 60
                    .MinimumHeight = 60
                    .Cells("customer").Value = colSub.Item("customerName") & vbCrLf & _
                                                 colSub.Item("cust_email")
                    .Cells("customer_email").Value = colSub.Item("cust_email")
                    .Cells("sub_id").Value = CStr(intSubId1)
                    .Cells("billing_period").Value = colSub.Item("billingPeriod")
                    '-4201.0929= Can Email Invoices."OkToEmailInvoices"..
                    If (colSub.Item("OkToEmailInvoices")) Then
                        .Cells("email_invoice").Value = "Yes"
                    Else  '-false-
                        .Cells("email_invoice").Value = "No"
                    End If

                    '- show line items..
                    s1 = ""
                    For Each col1 In colItems
                        If s1 <> "" Then s1 &= vbCrLf
                        s1 &= col1.Item("description")

                        '-compute/save line extensions for Invoice..
                        '-- Need to compute price/ext.. (from sell-ex and Qty)..
                        '- sales_taxCode, sellExTax --
                        sSalesTaxCode = UCase(col1.Item("Sales_taxCode"))
                        decItemQty = CDec(col1.Item("quantity"))  '--rrp ex tax-
                        decSellExTax = CDec(col1.Item("sellExTax"))  '--rrp ex tax-
                        '= 3411.0224= Actual price is now saved from ListView Item.
                        decSellIncTax = CDec(col1.Item("sellActual_inc"))  '--actual inc. tax-

                        '-- get GST rate for this tax code..
                        '-mDecGST_rate-
                        '- compute rrp incl. -
                        'If (sSalesTaxCode = "GST") Then
                        '    decSellTaxAmount = ((decSellExTax * mDecGST_rate / 100))
                        'Else
                        '    decSellTaxAmount = 0
                        'End If
                        'decSellIncTax = decSellExTax + decSellTaxAmount

                        '= 3411.0224= Get Tax from RRP-
                        If (sSalesTaxCode = "GST") Then
                            decSellTaxAmount = (decSellIncTax / 11)
                        Else
                            decSellTaxAmount = 0
                        End If
                        decSellExTax = decSellIncTax - decSellTaxAmount

                        '-- extension-
                        decExtension = decSellIncTax * decItemQty
                        '-also-
                        decSellExLineTotal = decSellExTax * decItemQty
                        '-- taxable/non-taxable.
                        decSellExLineTotal_non_taxable = 0
                        decSellExLineTotal_taxable = 0
                        If (sSalesTaxCode = "GST") Then
                            decSellExLineTotal_taxable = decSellExLineTotal
                        Else
                            decSellExLineTotal_non_taxable = decSellExLineTotal
                        End If
                        '-save these.
                        col1.Add(decSellExLineTotal_taxable, "SellExLineTotal_taxable")
                        col1.Add(decSellExLineTotal_non_taxable, "SellExLineTotal_non_taxable")
                        col1.Add(decCostInc, "CostIncTax")
                        col1.Add(decSellTaxAmount, "SellTaxAmount")
                        col1.Add(decSellIncTax, "SellIncTax")
                        '-- total-ex
                        decLineTotalEx = decSellExTax * decItemQty
                        col1.Add(decSellIncTax, "LineTotalEx")
                        '-- total-tax --
                        decLineTotalTax = decSellTaxAmount * decItemQty
                        col1.Add(decSellIncTax, "LineTotalTax")
                        '--total_inc--
                        col1.Add(decExtension, "LineExtension")  '--total_inc--
                        decGrossProfit = (decSellExTax - decCostExTax) * decItemQty '= CDec(sQty)
                        col1.Add(decGrossProfit, "GrossProfit")  '--gp--
                        '--our total for grid..
                        decInvoiceTotal += decExtension
                        '- accumulate items for Invoice..
                        colBillItems.Add(col1)
                    Next col1
                    .Cells("item_list").Value = s1
                    .Cells("total_inc").Value = FormatCurrency(decInvoiceTotal, 2)

                    '-  adjust row height if needed..


                    If sPrevPeriodStart <> "" Then
                        .Cells("prev_inv_date").Value = sPrevPeriodStart & vbCrLf & _
                                                         " to " & sPrevPeriodEnd
                    End If
                    .Cells("due_date_period").Value = sNewPeriodInfo
                    '=MarkToInvoice-
                    .Cells("MarkToInvoice").Value = False '==   Target-New-Build-4262 --   '--uncheck all..
                    .Cells("customer_id").Value = intCustomer_id
                End With  '-row rx-
                colBillSub.Add(colBillItems, "BillLineItems")
            Next colBillSub
        End If  '-count-

        '-- set row height to make Cell Button visible.
        'For Each row1 As DataGridViewRow In dgvInvoices.Rows
        '    row1.Height = 42
        '    row1.MinimumHeight = 42
        'Next 'row1

        If (sEmailsMissing <> "") Then
            s1 = "Please Note-" & vbCrLf & "Some Subs that need it are missing Email addresses." & vbCrLf & _
                          "--  If you proceed, the Subs. that are due will be invoiced anyway, but not emailed." & vbCrLf & _
                            " (NB: This msg and list is logged to the JobMatix Runtime Log..)" & vbCrLf
            '==                             vbCrLf & vbCrLf & sEmailsMissing & vbCrLf & "== t h e  e n d =="
            MsgBox(s1, MsgBoxStyle.Exclamation)
            txtLogStatus.Text &= vbCrLf & sEmailsMissing & vbCrLf & vbCrLf & sEmailsMissing & vbCrLf & "== t h e  e n d =="
            Call gbLogMsg(gsRuntimeLogPath, s1)
        End If

        '-4201.0929=
        '-- Report terminating subs if any..
        If (sSubsTerminating <> "") Then
            s1 = "Please Note-" & vbCrLf & _
                  "The following subscriptions will terminate with this billing cycle-" & vbCrLf & _
                  " (NB: This msg and list is logged to the JobMatix Runtime Log..)" & vbCrLf & vbCrLf & _
                      sSubsTerminating & vbCrLf & "== e n d of list ==" & vbCrLf
            Call gbLogMsg(gsRuntimeLogPath, "Please Note-" & vbCrLf & s1 & vbCrLf)
            MsgBox(s1 & vbCrLf, MsgBoxStyle.Exclamation)
        End If 'termination.

        Call mbReport("Refresh Done.." & vbCrLf) '=  "Switch to Maintenance panel to change subscription properties."

        mbRefreshInvoiceLog = True

    End Function  '-mbRefreshInvoiceLog-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Setup New Sub Customer-

    Private Function mbSetupNewSubCustomer(ByRef colSelectedRow As Collection) As Boolean
        Dim s1, sSql As String
        Dim int1 As Integer
        Dim dtPayments As DataTable
        Dim decCredits As Decimal = 0
        Dim decDebits As Decimal = 0

        mIntNewSubCustomer_id = CInt(colSelectedRow.Item("customer_id")("value"))

        '==If colSelectedRow.Contains("barcode") Then
        msNewSubCustomerBarcode = colSelectedRow.Item("barcode")("value")
        txtSubCustBarcode.Text = msNewSubCustomerBarcode
        '= msSaleCustomerBarcode = Trim(mTxtSaleCustBarcode.Text)
        '==End If
        If colSelectedRow.Contains("companyName") Then
            labSubCustomerName.Text = colSelectedRow.Item("companyName")("value")
            '= txtCustName.Text = col1.Item("value")
        End If
        If labSubCustomerName.Text <> "" Then labSubCustomerName.Text &= vbCrLf
        If colSelectedRow.Contains("firstName") Then
            labSubCustomerName.Text &= colSelectedRow.Item("firstName")("value") & " "
            '=txtCustName.Text = col1.Item("value")
        End If
        If colSelectedRow.Contains("lastName") Then
            labSubCustomerName.Text &= colSelectedRow.Item("lastName")("value")
        End If
        '-save for RESTORE..-
        msNewSubCustomerName = labSubCustomerName.Text

        cboBillingCycle.Enabled = True
        cboBillingCycle.Select()

    End Function '-mbSetupNewSubCustomer-
    '= = = = = = = = = = = = = = = = = == 
    '-===FF->

    '-  Show Subscription Info-
    Private mbLoadingSub As Boolean = False

    Private Function mbShowSubscriptionInfo(ByVal intSubscription_id As Integer) As Boolean
        '-  get goods info..

        Dim sSql, s1, sErrorMsg, sList As String
        Dim dataTable1 As DataTable
        Dim subrow1 As DataRow
        Dim rdrItems As OleDbDataReader
        Dim cmd1 As OleDbCommand

        grpBoxSubDetail.Enabled = False
        '--lookup goods-id-
        mbStartDateChanged = False
        mbActivateSubNow = False
        mbDataModified = False
        mbItemsModified = False

        mbHasEndDate = False
        mbIsTerminated = False

        txtSubCustBarcode.Enabled = False
        cboBillingCycle.Enabled = False

        mIntSubscription_id = intSubscription_id
        '--  get recordset as collection for SELECT..--
        sSql = "SELECT *, "
        sSql &= "  customerName = CASE companyName  "
        sSql &= "     WHEN '' THEN (customer.firstname + ' ' + customer.lastname)"
        sSql &= "     WHEN 'n/a' THEN (customer.firstname + ' ' + customer.lastname)"
        sSql &= "     ELSE companyName "
        sSql &= "   END, customer.barcode as cust_barcode "
        sSql &= " FROM dbo.Subscription "
        sSql &= "  JOIN customer ON (customer.customer_id=subscription.customer_id) "
        sSql &= " WHERE (subscription_id=" & CStr(intSubscription_id) & ");"

        If gbGetDataTable(mCnnSql, mDataTableSubscription, sSql) AndAlso _
                               (Not (mDataTableSubscription Is Nothing)) AndAlso (mDataTableSubscription.Rows.Count > 0) Then

            subrow1 = mDataTableSubscription.Rows(0)
            '= not there..= labGoodsTotal.Text = FormatCurrency(CDec(goodsrow1.Item("total_inc")), 2)
        Else
            mbLoadingSub = False
            Exit Function
        End If  '--get-
        sList = ""

        mbLoadingSub = True
        labSubsId.Text = CStr(intSubscription_id)
        txtSubCustBarcode.Text = subrow1.Item("cust_barcode")
        labSubCustomerName.Text = subrow1.Item("customerName")
        dtPickerStart.Value = CDate(subrow1.Item("start_date"))

        '=3411.0225-
        '--  End Date-
        If Not IsDBNull(subrow1.Item("termination_date")) Then
            '- Have end date.-
            mbHasEndDate = True
            mDateTerminationDate = CDate(subrow1.Item("termination_date"))
            '- check if terminated.
            If (DateDiff(DateInterval.Day, Today, mDateTerminationDate) <= 0) Then
                mbIsTerminated = True
            End If
            dtPickerEnd.Value = CDate(subrow1.Item("termination_date"))
            chkEndDate.Checked = True
            chkEndDate.Enabled = False
            dtPickerEnd.Visible = True
        Else
            chkEndDate.Checked = False
            chkEndDate.Enabled = True
            dtPickerEnd.Visible = False
        End If  '-null term date-

        If (CInt(subrow1.Item("isActivated")) <> 0) Then '= Not IsDBNull(subrow1.Item("start_date")) Then
            chkActivateNow.Enabled = False
            chkActivateNow.Checked = True
            chkActivateNow.Text = "Activated."
            labShowActivated.Text = "Activated."
            dtPickerStart.Enabled = False
            '==dtPickerStart.Value = CDate(subrow1.Item("start_date"))
        Else  '-not activated yet.
            chkActivateNow.Enabled = True
            chkActivateNow.Checked = False
            chkActivateNow.Text = "Activate Now."
            labShowActivated.Text = "NOT Activated."
        End If

        labShowPeriod.Text = Trim(subrow1.Item("billingPeriod"))
        '-cboBillingCycle.Items
        '- show full name-
        Dim sSplit() As String
        For Each item1 As String In cboBillingCycle.Items
            sSplit = Split(Trim(item1))
            If UCase(Trim(sSplit(0))) = UCase(labShowPeriod.Text) Then
                '-found-
                cboBillingCycle.SelectedItem = item1
                Exit For
            End If
        Next item1

        '-- ok To Email Invoices.
        '==    -- 4201.0929.  Started 19-September-2019-
        '==        -- Add column to Subscriptions Table: "OkToEmailInvoices bit default 0"
        chkOkToEmailInvoices.Checked = False
        If CInt(subrow1.Item("OkToEmailInvoices") <> 0) Then
            chkOkToEmailInvoices.Checked = True
        End If

        txtComments.Text = subrow1.Item("comments")

        '-- get all line items this sub...
        listViewSubsItems.Items.Clear()
        listViewSubsItems.SelectedItems.Clear()

        sSql = " SELECT *, stock.barcode, stock.cat1, stock.description "
        sSql &= "        FROM dbo.SubscriptionLine AS SL "
        sSql &= "          JOIN stock ON (stock.stock_id=SL.stock_id) "
        sSql &= "        WHERE (SL.subscription_id=" & CStr(intSubscription_id) & ") ORDER BY SL.line_id "

        '-- start retrieval-
        Try
            '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
            cmd1 = New OleDbCommand(sSql, mCnnSql)
            rdrItems = cmd1.ExecuteReader
        Catch ex As Exception
            MsgBox("Error getting Subscription Items recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            dgvSubsList.Enabled = True
            mbLoadingSub = False
            Exit Function
        End Try

        Dim listItem1 As ListViewItem
        '=Dim rdrSerials As OleDbDataReader
        Dim intStock_id As Integer
        Dim intItemNo As Integer = 0
        '=Dim colItemSerials As Collection
        Dim sSalesTaxCode As String
        Dim decSellExTax, decSellTaxAmount, decSellIncTax, decExtension As Decimal
        Dim intItemQty As Integer '=Decimal
        '==  load items listview..
        '= mColAllItemSerials = New Collection

        If rdrItems.HasRows Then
            Do While rdrItems.Read
                intStock_id = CInt(rdrItems.Item("stock_id"))
                '= intLineNo = CInt(rdrItems.Item("line_id"))  '--item line no in goods.Lines
                intItemNo += 1
                listItem1 = New ListViewItem(CStr(intItemNo))  '-- number lines from 1..
                s1 = rdrItems.Item("barcode")
                listItem1.SubItems.Add(s1)  '-barcode
                listItem1.SubItems.Add(rdrItems.Item("cat1"))
                listItem1.SubItems.Add(rdrItems.Item("description"))
                intItemQty = CInt(rdrItems.Item("quantity"))
                '=3411.0224= Actual Price is saved in Sibs.Line.
                decSellIncTax = CDec(rdrItems.Item("sellActual_inc"))

                listItem1.SubItems.Add(FormatCurrency(decSellIncTax, 2))
                listItem1.SubItems.Add(CStr(intItemQty))

                '-- Subscriptions ALWAYS use latest prices.
                '-- Need to compute price/ext.. (fom sell-ex and Qty)..
                '- sales_taxCode, sellExTax --
                sSalesTaxCode = UCase(rdrItems.Item("Sales_taxCode"))
                decSellExTax = CDec(rdrItems.Item("sellExTax"))  '--rrp ex tax-

                '-- get GST rate for this tax code..
                '-mDecGST_rate-
                '- compute rrp incl. -
                '==     v3.4.3403.0731 = 31Jul2017=
                '==      -- clsPOS34Sale-  Fix TAX code choice if setting up line. (was using GoodsTax code-)..
                If (sSalesTaxCode = "GST") Then '= (sGoodsTaxcode = "GST") Then
                    decSellTaxAmount = (decSellIncTax / 11)  '= ((decSellExTax * mDecGST_rate / 100))
                Else
                    decSellTaxAmount = 0
                End If
                '= decSellIncTax = decSellExTax + decSellTaxAmount
                '-- extension-
                decExtension = decSellIncTax * intItemQty
                '= listItem1.SubItems.Add(FormatCurrency(decSellIncTax, 2))
                listItem1.SubItems.Add(FormatCurrency(decExtension, 2))
                listItem1.SubItems.Add(CStr(intStock_id))
                listItem1.SubItems.Add(sSalesTaxCode)

                listViewSubsItems.Items.Add(listItem1)
            Loop '-items-
        End If
        rdrItems.Close()
        mSubShowTotalSubvalue()
        listViewSubsItems.SelectedItems.Clear()
        listSubInvoices.Items.Clear()
        btnDeleteItem.Enabled = False

        '-- get any invoices charged against this sub..
        '--  List them in the list box..
        '-- Show last period billed..
        labLastPeriodBilled.Text = ""

        labInvoiceCount.Text = ""
        mDataTableSubInvoices = Nothing
        '= chkActivateNow.Enabled = False
        sSql = "SELECT *, invoice.invoice_date, invoice.total_inc "
        '== sSql &= "    invoice_period_start_date, invoice_period_end_date, invoice_date, total_inc "
        sSql &= " FROM dbo.SubscriptionInvoice AS SI "
        sSql &= "  JOIN Invoice ON invoice.invoice_id= SI.invoice_id "
        sSql &= " WHERE (Subscription_id=" & CStr(intSubscription_id) & ") "
        sSql &= "  ORDER BY invoice_period_end_date DESC;"
        If gbGetDataTable(mCnnSql, mDataTableSubInvoices, sSql) Then
            If (Not (mDataTableSubInvoices Is Nothing)) AndAlso (mDataTableSubInvoices.Rows.Count > 0) Then
                labInvoiceCount.Text = CStr(mDataTableSubInvoices.Rows.Count)
                With mDataTableSubInvoices.Rows(0)
                    labLastPeriodBilled.Text = Format(.Item("invoice_period_start_date"), "dd-MMM-yyyy") & " to " & _
                                                    Format(.Item("invoice_period_end_date"), "dd-MMM-yyyy")
                End With
                '- save lastest period billed-
                With listSubInvoices
                    For Each datarow1 As DataRow In mDataTableSubInvoices.Rows
                        s1 = (RSet(CStr(datarow1.Item("invoice_id")), 4) & ". ")
                        s1 &= Format(datarow1.Item("invoice_date"), "dd-MMM-yyyy")
                        .Items.Add(s1)
                    Next datarow1
                End With
            Else
                '- invoices
            End If  '-nothing..
        Else  '-Failed-
            '==chkActivateNow.Enabled = True   '- can change date until invoicing started.
            MsgBox("Failed to get Subs. Invoices.", MsgBoxStyle.Exclamation)
        End If  '--get-
        '-- can't change start date if invoicing has happened.
        '-- ie if activated..
        'If listSubInvoices.Items.Count > 0 Then
        '    dtPickerStart.Enabled = False
        'Else
        '    dtPickerStart.Enabled = True  '- can still change-
        'End If
        If Not mbIsTerminated Then
            btnEditSub.Enabled = True
        End If
        btnCancelSub.Enabled = True

        If (listViewSubsItems.Items.Count > 0) Then
            '== NO!  do not move focus from Goods..  listViewGoodsItems.Items(0).Focused = True
            '== listViewSubsItems.Items(0).Selected = True
        End If
        mbLoadingSub = False

    End Function  '-mbShowSubscriptionInfo-
    '= = = = = = = = = = = = = ==  = = == =
    '-===FF->

    '-- load-

    Private Sub frmSubscription_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim s1 As String

        '--  Subs--
        labShowTotal.Text = ""
        '= labShowCustName.Text = ""
        msCurrentUserName = gsGetCurrentUser()
        '= labToday.Text = Format(Today, "ddd dd-MMM-yyyy")

        mIntFormDesignHeight = Me.Height '--save starting dimensions..-
        mIntFormDesignWidth = Me.Width '--save starting dimensions..-

        msAppPath = My.Application.Info.DirectoryPath
        If VB.Right(msAppPath, 1) <> "\" Then msAppPath = msAppPath & "\"
        msAppFullname = msAppPath & My.Application.Info.AssemblyName
        If (VB.Right(LCase(msAppFullname), 4) <> ".exe") Then
            msAppFullname &= ".exe"
        End If

        mColPrefsSubs = New Collection
        mColPrefsSubs.Add("subscription_id")
        mColPrefsSubs.Add("customer_id")
        mColPrefsSubs.Add("staff_id")
        mColPrefsSubs.Add("billingPeriod")
        mColPrefsSubs.Add("start_date")
        mColPrefsSubs.Add("end_date")


        '-- Customer --
        mColPrefsCustomer = New Collection
        mColPrefsCustomer.Add("barcode")
        mColPrefsCustomer.Add("lastname")
        mColPrefsCustomer.Add("firstname")
        mColPrefsCustomer.Add("companyName")
        mColPrefsCustomer.Add("isAccountCust")
        mColPrefsCustomer.Add("phone")
        mColPrefsCustomer.Add("mobile")
        mColPrefsCustomer.Add("email")
        mColPrefsCustomer.Add("customer_id")
        mColPrefsCustomer.Add("creditLimit")
        mColPrefsCustomer.Add("pricingGrade")
        mColPrefsCustomer.Add("inactive")
        mColPrefsCustomer.Add("address")
        '==mColPrefsCustomer.Add("addr2")
        '=mColPrefsCustomer.Add("addr3")
        mColPrefsCustomer.Add("suburb")
        '= mColPrefsCustomer.Add("date_modified")
        '= mColPrefsCustomer.Add("comments")

        '--  stock--
        mColPrefsStock = New Collection
        mColPrefsStock.Add("cat1")   '--fkey-
        mColPrefsStock.Add("cat2")   '-fkey-
        mColPrefsStock.Add("description")
        mColPrefsStock.Add("barcode")
        mColPrefsStock.Add("brandName")
        mColPrefsStock.Add("sales_taxCode")
        mColPrefsStock.Add("sellExTax")
        mColPrefsStock.Add("stock_id")

        labAction.Text = ""
        grpBoxBilling.Text = ""
        frameBrowse.Text = ""
        grpBoxSubsLookup.Text = ""
        grpBoxSub.Text = ""
        grpBoxSubDetail.Text = ""
        grpBoxSubDetail.Text = "Selected Sub."
        labInvoiceCount.Text = ""
        listSubInvoices.Items.Clear()

        grpBoxSubDetail.Enabled = False
        grpBoxSubDetail.Enabled = False

        btnAddItem.Enabled = False
        btnDeleteItem.Enabled = False
        btnCancelSub.Enabled = False

        txtLogStatus.Text = "Starting up.." & vbCrLf & "-- The grid below shows all Subscriptions due to be invoiced now.. " & _
                              " Invoices can be released Individually, or as a marked Group... " & vbCrLf & _
                              "-- To issue and send invoice, click on the relevant ""Invoice Now"" button, " & _
                                             " or mark (check) the Subs to be invoiced as a group."
        labLastPeriodBilled.Text = ""

        cboBillingCycle.Items.Clear()
        cboBillingCycle.Items.Add("1W Weekly")
        cboBillingCycle.Items.Add("1M Monthly")
        cboBillingCycle.Items.Add("3M Quarterly")
        cboBillingCycle.Items.Add("6M Half-Yearly")
        cboBillingCycle.Items.Add("12M Yearly")
        cboBillingCycle.SelectedIndex = -1

        dtPickerStart.CustomFormat = "dd-MMM-yyyy"
        '-- Lucida Sans'-
        dtPickerStart.Font = New Font("Lucida Sans", 9)
        dtPickerEnd.CustomFormat = "dd-MMM-yyyy"
        dtPickerEnd.Value = CDate("31/12/2100")
        dtPickerEnd.Font = New Font("Lucida Sans", 9)

        Call mSubClearShowSub()
        txtSubsSearch.Text = ""

        mSysInfo1 = New clsSystemInfo(mCnnSql)
        If mSysInfo1.contains("GSTPercentage") Then
            s1 = mSysInfo1.item("GSTPercentage")
            '-mdecGST_percentage
            If IsNumeric(s1) Then
                mDecGST_rate = CDec(s1)
            End If
        End If

        '-Set Column names in ListView, as they get lost--
        '--    when done in the designer..
        listViewSubsItems.Items.Clear()
        listViewSubsItems.Columns(0).Name = "ItemNo"
        listViewSubsItems.Columns(1).Name = "ItemBarcode"
        listViewSubsItems.Columns(2).Name = "Cat1"
        listViewSubsItems.Columns(3).Name = "Description"
        listViewSubsItems.Columns(5).Name = "Price"
        listViewSubsItems.Columns(4).Name = "Qty"
        listViewSubsItems.Columns(6).Name = "Amount"
        listViewSubsItems.Columns(7).Name = "stock_id"
        '== = = 

        '- find PDF printer for emailing invoice..

        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            'For Each sName As String In colPrinters
            '     If (InStr(LCase(sName), "adobe pdf") > 0) Or _
            '             ((InStr(LCase(sName), "microsoft") > 0) And (InStr(LCase(sName), "pdf") > 0)) Then
            '        msPdfPrinterName = sName  '-save PDF printer name--
            '    End If
            'Next sName
        End If  '- no printers-
        '-  FROM Show Invoice-
        '=3411.0110=  Get PDF prefrred printer...
        '---(Microsoft will be preferred)..
        If Not gsGetPdfPrinterName(msPdfPrinterName) Then
            MsgBox("Error- Failed to look-up pdf printer..", MsgBoxStyle.Exclamation)
        End If  '-gety-
        labPdfPrinter.Text = msPdfPrinterName

        If (msPdfPrinterName = "") Then
            MsgBox("Please Note: " & vbCrLf & "No PDF printer was found on this system." & vbCrLf & _
                    "Invoices created here can not be stored for emailing).." & vbCrLf & vbCrLf & _
                    "  (ALSO-  Bulk invoicing function will be disabled.)", MsgBoxStyle.Information)
            mbGroupInvoicingEnabled = False
        Else  '-pdf ok..
            mbGroupInvoicingEnabled = True
        End If
        '=3301.607= Check if we can save to email queue (docArchive table).
        mbAllowEmailInvoices = False
        If mSysInfo1.contains("POS_ALLOW_EMAIL_INVOICES") Then
            If (UCase(mSysInfo1.item("POS_ALLOW_EMAIL_INVOICES")) = "Y") Then
                mbAllowEmailInvoices = True    '= Yes, do emailing.-
            End If
        End If
        '= = = = = = = = = = = = = = = = = = = = = =

        '=3301.428= If gbLoadsystemInfo(mCnnSql, colSystemInfo, mSdSystemInfo) Then '-- get all system info..--
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-
        '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        msEmailTextInvoice = mSysInfo1.item("POS_EMAILTEXTINVOICE")
        '=3301.428= End If  '-load sys info--
        '=3403.1009- Server Share Path for Email Queue.
        msEmailQueueSharePath = mSysInfo1.item("POS_EMAILQUEUE_SHAREPATH")
        msInvoiceFilePath = gsGetPDF_file_path()

        '= = = = = = = = = = =

        '=3411.0224- Context menu-
        '--  Popup menu for Right click on parts..-
        mContextMenuPartInfo = New ContextMenu
        mnuEditPrice.Name = "EditLinePrice"
        mContextMenuPartInfo.MenuItems.Add(mnuEditPrice)
        mnuEditQuantity.Name = "EditLineQuantity"
        mContextMenuPartInfo.MenuItems.Add(mnuEditQuantity)
        'mnuCopyPartSerialNo.Name = "CopyPartSerialNo"
        'mContextMenuPartInfo.MenuItems.Add(mnuCopyPartSerialNo)
        'mnuPartMenuSep1.Name = "PartMenuSep1"
        'mContextMenuPartInfo.MenuItems.Add(mnuPartMenuSep1)
        'mnuDeletePart.Name = "DeletePart"
        'mContextMenuPartInfo.MenuItems.Add(mnuDeletePart)

        '=3519.0307--
        panelGroupAction.Enabled = False
        '=btnCancelInvoicing.Enabled = False
        btnPauseInvoicing.Enabled = False
        TabControl1.SelectedTab = TabControl1.TabPages("TabPageInvoicing")

        '= Me.Text = "POS Subscriptions- " & labVersion.Text
        '= Call CenterForm(Me)

        '= 4201.0428=
        '--  Stuff from SHOWN..
        '--  Stuff from SHOWN..
        '--  Stuff from SHOWN..

        Dim sConnect, sErrors As String
        Dim intRecordsAffected As Integer

        '-- do sub at startup only..
        'If mbActivated Then Exit Sub
        'mbActivated = True

        '-- setup sql SHAPE connection for Invoicing Log..--
        '-- setup sql SHAPE connection for Invoicing Log..--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mCnnShape = New OleDbConnection '=  ADODB.Connection
        sConnect = "Provider=MSDataShape; Server=" & msServer & "; Trusted_Connection=true; Integrated Security=SSPI; "
        sConnect = sConnect & "; Data Provider=SQLOLEDB; Data Source=" & msServer & "; "
        '=== sConnect = sConnect + "; Password=" + msSqlPwd + "; User ID=" + msSqlUid
        If gbConnectSql(mCnnShape, sConnect) Then
            '--FrameReport.Enabled = True   '--show report options frame..--
            '--FrameStatus.Enabled = True
        Else
            MsgBox("Login to sql SHAPE dataSource has failed." & vbCrLf, MsgBoxStyle.Exclamation)
            '====FrameReport.Enabled = False
            Me.Hide()
            '==End
        End If '--connected-
        If Not gbExecuteCmd(mCnnShape, "USE " & msSqlDbName & vbCrLf, intRecordsAffected, sErrors) Then
            MsgBox(vbCrLf & "Failed USE for SHAPE connection to DATABASE: '" & _
                                            msSqlDbName & "'.." & vbCrLf & sErrors, MsgBoxStyle.Critical)
        End If '--use-
        '= mCnnShape.CommandTimeout = 10 '-- 10 sec cmd timeout..-
        '= mCnnShape.CursorLocation = ADODB.CursorLocationEnum.adUseClient

        '-- get initial subscriptions list
        Try
            '== Call mbInitialiseBrowse("Subscription")
            Call mbBrowseSubsTable()
        Catch ex As Exception
            MsgBox("Error in activating browser object.." & vbCrLf & ex.Message, MsgBoxStyle.Information)
            '= Me.Close()
            Call close_me()
            Exit Sub
        End Try

        '--load collection of active subs..
        If Not mbRefreshInvoiceLog() Then
            '= Me.Close()
            Call close_me()
            Exit Sub
        End If
        Call mIntUnMarkAll()

        '-- test-
        '- show collection..-
        Dim sInfo As String = ""
        Dim colItems, colInvoices As Collection
        For Each colSub As Collection In mColSubsInvoiceLog
            sInfo &= "Sub-id: " & colSub.Item("subscription_id")
            colItems = colSub.Item("lineItems")
            colInvoices = colSub.Item("Invoices")
            sInfo &= ";  Has " & colItems.Count & " items;  " & colInvoices.Count & " invoices.." & vbCrLf
        Next colSub

        'MsgBox("TEST Invoice Log- Found " & mColSubsInvoiceLog.Count & " active subs." & vbCrLf & _
        '                      "Contents: " & vbCrLf & sInfo, MsgBoxStyle.Information)
        mbIsLoading = False

        panelGroupAction.Enabled = True
        txtLogStatus.Select()  '--focus-

        '==
        '==  Target is new Build 4234..
        '==  Target is new Build 4234..
        mbRefreshSubsTree()
        '==  END OF PREP FOR- Target 4234 - Updates to 4233.0421  Started 24-April-2020= 
        '==  END OF PREP FOR- Target 4234 - Updates to 4233.0421  Started 24-April-2020= 


        Call mbReport("Startup completed..")


    End Sub  '-load-
    '= = = = = ==  ==  == =
    '-===FF->

    '-- form resized --

    Private Sub frmSubscription_Resize(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Resize

        '=If (Me.WindowState <> System.Windows.Forms.FormWindowState.Minimized) Then

        '--  cant make smaller than original..-

        TabControl1.Height = Me.Height - 11 '= (Me.Height - 120)
        TabControl1.Width = (Me.Width - 12)

        '= btnStockAdmin.Left = panelGoodsBanner.Width - btnStockAdmin.Width - 5
        grpBoxSubsLookup.Width = TabPageSubs.Width - 7 '= 17
        grpBoxSubsLookup.Height = TabPageSubs.Height - 11

        grpBoxSub.Left = grpBoxSubsLookup.Width - grpBoxSub.Width - 3
        grpBoxSub.Height = grpBoxSubsLookup.Height - 17

        grpBoxSubDetail.Height = grpBoxSub.Height - 80

        listViewSubsItems.Width = grpBoxSubDetail.Width - 13
        listViewSubsItems.Height = grpBoxSubDetail.Height - listViewSubsItems.Top - 11  '= 411

        frameBrowse.Width = grpBoxSubsLookup.Width - grpBoxSub.Width - 17
        frameBrowse.Height = grpBoxSubsLookup.Height - frameBrowse.Top - 12

        dgvSubsList.Height = frameBrowse.Height - dgvSubsList.Top - 18
        dgvSubsList.Width = frameBrowse.Width - 17

        '-invoice tab-
        panelBillingHdr.Width = TabControl1.Width - 7
        txtLogStatus.Left = panelBillingHdr.Width - txtLogStatus.Width - 25  '--add for scroll bar width ?..

        grpBoxBilling.Width = TabPageInvoicing.Width - 7
        grpBoxBilling.Height = TabPageInvoicing.Height - 11

        panelBillingHdr.Width = grpBoxBilling.Width - 8

        dgvInvoices.Width = panelBillingHdr.Width  '= TabControl1.Width - 17
        dgvInvoices.Height = grpBoxBilling.Height - panelBillingHdr.Height - 12

        '-- Analysis Page..
        Dim intAnalysisWidth = TabPageAnalysis.Width - 12
        panelProducts.Width = (intAnalysisWidth \ 3) + 60   '-- one third for the tree.
        panelProducts.Height = TabPageAnalysis.Height - 12
        clsPosTreeViewProducts.Width = panelProducts.Width - 12
        clsPosTreeViewProducts.Height = panelProducts.Height - clsPosTreeViewProducts.Top - 11

        panelPeriodReport.Left = panelProducts.Left + panelProducts.Width + 7
        panelPeriodReport.Width = intAnalysisWidth - panelProducts.Width - 11
        panelPeriodReport.Height = TabPageAnalysis.Height - 12
        dgvPeriodReport.Width = panelPeriodReport.Width - 12
        dgvPeriodReport.Height = panelPeriodReport.Height - dgvPeriodReport.Top - 7

        '= labVersion.Top = Me.Height - 55
        '= End If  '-minimised.-
    End Sub  '-re-sized=-
    '= = = = = = = = = = = = = = = = = = = = =

    '-btnRefreshAnalysis_Click-

    Private Sub btnRefreshAnalysis_Click(sender As Object, e As EventArgs) Handles btnRefreshAnalysis.Click

        mbRefreshSubsTree()

    End Sub  '-btnRefreshAnalysis_Click-
    '= = = = = ==  =
    '-===FF->

    '--BROWSING Customer.. --

    '--  D at a  G r i d  E v e n t s..--
    '--  D at a  G r i d  E v e n t s..--

    Private Sub dgvSubsList_SelectionChanged(ByVal sender As Object, _
                                               ByVal ev As EventArgs) Handles dgvSubsList.SelectionChanged
        Dim ix, intRow, intSubs_id As Integer
        Dim s1 As String

        If mbIsLoading Then
            Exit Sub
        End If
        If (dgvSubsList.Rows.Count > 0) Then
            intRow = dgvSubsList.CurrentRow.Index   '= ev.RowIndex
            If (intRow >= 0) Then
                With dgvSubsList.Rows(intRow)
                    s1 = .Cells(0).Value
                    If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                        intSubs_id = CInt(s1)
                        If (intSubs_id > 0) And (intSubs_id <> mIntSubscription_id) Then '-- has changed..-
                            Call mbShowSubscriptionInfo(intSubs_id)
                        End If
                    End If
                End With
            End If  '-intRow-
        End If  '-count-
        '=lCol = ev.ColumnIndex
    End Sub  '-SelectionChanged-
    '= = = = = = = = = === 
    '-===FF->

    '--  Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvSubsList_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvSubsList.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvSubsList.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowse1.SortColumn(sName)

        '-- set row height to make checkboxes visible.
        For Each row1 As DataGridViewRow In dgvSubsList.Rows
            row1.Height = k_SUBS_LIST_ROW_HEIGHT
            row1.MinimumHeight = k_SUBS_LIST_ROW_HEIGHT
        Next 'row1
        System.Windows.Forms.Application.DoEvents()

    End Sub
    '= = = = = = = = =  = = =
    '= = = = = = = = =  = = =
    '-===FF->

    '-- cell click.--
    '-- cell click.--

    Private Sub dgvSubsList_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvSubsList.CellMouseClick
        Dim lRow, lCol As Integer
        Dim intSubs_id As Integer
        Dim colKeys, colRowValues As Collection

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (dgvSubsList.Rows.Count > 0) Then  '--selected a row.--
                mLngSelectedRow = lRow
                Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                intSubs_id = colRowValues.Item("subscription_id")("value")

                labSubCustomerName.Text = colRowValues.Item("customerName")("value")
                Call mbShowSubscriptionInfo(intSubs_id)
                'intCustomer_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                'If (intCustomer_id > 0) And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                '    Call mbShowCustomerInfo(intCustomer_id)
                'End If
            End If  '-selected-
        End If  '--left..-
    End Sub  '-CellMouseClickEvent-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  
    '-- select row to edit--
    '-- select row to edit--

    'Private Sub dgvGoodsList_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
    '                                                  ByVal eventArgs As DataGridViewCellMouseEventArgs) _
    '                                                        Handles dgvGoodsList.CellMouseDoubleClick
    '    Dim lRow As Integer
    '    Dim intGoods_id As Integer
    '    Dim colKeys, colRowValues As Collection

    '    '== lCol = eventArgs.ColumnIndex
    '    lRow = eventArgs.RowIndex

    '    If (lRow >= 0) Then '--ok--
    '        mLngSelectedRow = lRow
    '        '--  get stock id and start edit.--
    '        If (lRow >= 0) And (dgvGoodsList.Rows.Count > 0) Then  '--selected a row.--
    '            mLngSelectedRow = lRow
    '            Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
    '            intGoods_id = colRowValues.Item("goods_id")("value")
    '            Call mbShowGoodsInfo(intGoods_id)

    '        End If  '-selected-
    '    End If '--row--
    'End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '-- SUBS Browser.. txt FIND Activity.--
    '-- SUBS Browser.. txt FIND Activity.--

    '-- key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                                           Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        Dim intStaff_id, intSubs_id As Integer
        Dim colKeys, colRowValues As Collection

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If dgvSubsList.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = dgvSubsList.SelectedRows(0).Cells(0).RowIndex
                If (lRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow
                    '= Call mbSelectStockRow(mLngSelectedRow)
                    Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
                    intSubs_id = colRowValues.Item("subscription_id")("value")
                    Call mbShowSubscriptionInfo(intSubs_id)
                    'intCustomer_id = colKeys.Item(1)  '-- PKEY=  CInt(item1.Text) '--1st column has to be id..--
                    'If (intCustomer_id > 0) And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                    '    Call mbShowCustomerInfo(intCustomer_id)
                    'End If
                End If '--row--
                iKeyAscii = 0 '--processed--
            End If '--count--

            eventArgs.KeyChar = Chr(iKeyAscii)
            If iKeyAscii = 0 Then
                eventArgs.Handled = True
            End If
        End If  '--Enter-
    End Sub '--click--
    '= = = = = = = = = = =

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Private Sub txtFind_Enter(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles txtFind.Enter
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Private Sub txtFind_Leave(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtFind.Leave
        LabFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabFind.Font = VB6.FontChangeBold(LabFind.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtFind.TextChanged
        If mbIsLoading Then
            Exit Sub
        End If

        Call mBrowse1.Find(txtFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '--added 07-Oct-2019=
    '=4201.1007-  Catch Enter Key on Subs srch text-

    Private Sub txtSubsSearch_keyPress(ByVal sender As System.Object, _
                                         ByVal EventArgs As KeyPressEventArgs) Handles txtSubsSearch.KeyPress
        Dim keyAscii As Short = Asc(EventArgs.KeyChar)
        Dim e2 As New EventArgs
        If keyAscii = 13 Then '--enter-
            Call cmdSubsSearch_Click(cmdSubsSearch, e2)
            keyAscii = 0 '--processed..-
        End If  '13-
        EventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            EventArgs.Handled = True
        End If
    End Sub  '-keypress.
    '= = = = = = = = == 

    '--  Browser..  Full text Search..--
    '--  Browser..  Full text Search..--

    Private Sub cmdSubsSearch_Click(ByVal sender As System.Object, _
                                         ByVal e As System.EventArgs) Handles cmdSubsSearch.Click
        Dim sWhere As String = ""
        Dim sSql As String '--search sql..-- 
        '= Dim s1, s2 As String
        Dim asColumns As Object

        '--  rebuild Search Columns and call makeTextSearch...-
        btnCancelSub.Enabled = False

        '-- arg can be multiple tokens..--
        '===asArgs = Split(Trim(txtSearch(index).Text))
        '--  now in the Interface..--
        '== asColumns = mRetailHost1.stockSearchColumns()
        asColumns = New Object() _
                      {"subscription_id", "firstName", "lastName", "companyName"}

        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(txtSubsSearch.Text), asColumns)
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere + " AND "
            sWhere = sWhere + sSql
        End If
        Call mbBrowseSubsTable(sWhere)

    End Sub '-cmdCustomerSearch-
    '= = = = = = = = = = = = =  =

    Private Sub cmdClearSubsSearch_Click(ByVal sender As System.Object, _
                                             ByVal e As System.EventArgs) Handles cmdClearSubsSearch.Click
        txtSubsSearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdSubsSearch_Click(cmdSubsSearch, New System.EventArgs())

    End Sub  '-ClearCustSearch-
    '= = = = = = = = = = = = = = = =

    '==  END of SUBS BROWSING..--
    '==  END of SUBS BROWSING..--
    '-===FF->

    '-- Edit etc  Actions--
    '-- Edit etc  Actions--
    '-- Edit etc  Actions--

    '- CANCEL sub forever.

    Private Sub btnCancelSub_Click(sender As Object, e As EventArgs) Handles btnCancelSub.Click
        Dim sSql, sUpdateList As String

        If MsgBox("Sure you want to CANCEL this sub for " & vbCrLf & _
                    labSubCustomerName.Text & vbCrLf & _
                    " forever ??", _
                     MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Ok Then
            Exit Sub
        End If

        sUpdateList = "isCancelled=1, "
        sUpdateList &= " cancelled_staff_id= " & CStr(mIntStaff_id) & ", "
        sUpdateList &= " date_cancelled = CURRENT_TIMESTAMP, "
        sUpdateList &= " date_updated = CURRENT_TIMESTAMP "
        '--- finish update--
        sSql = "UPDATE dbo.Subscription SET " & sUpdateList
        sSql &= "  WHERE subscription_id= " & CStr(mIntSubscription_id) & "; "
        If Not mbExecuteSql(mCnnSql, sSql, False, Nothing) Then
            MsgBox("Cancelling Subscription FAILED..", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            MsgBox("Subscription was cancelled..", MsgBoxStyle.Exclamation)

        End If  '--exec invoice-
        frameBrowse.Enabled = True
        btnNewSub.Enabled = True
        btnEditSub.Enabled = False
        btnCancelSub.Enabled = False

        Call mbRefreshInvoiceLog()
        Call mbBrowseSubsTable()

    End Sub '-cancel sub-
    '= = = = = = = = == =  =

    '-- btnEditSub_Click --

    Private Sub btnEditSub_Click(sender As Object, e As EventArgs) Handles btnEditSub.Click

        btnSaveEdit.Enabled = False
        frameBrowse.Enabled = False
        grpBoxSubDetail.Enabled = True
        '=dtPickerStart.Enabled = True

        '= grpBoxShowSub.Enabled = False
        btnNewSub.Enabled = False
        btnEditSub.Enabled = False

        If (listViewSubsItems.Items.Count > 0) Then
            listViewSubsItems.Items(0).Selected = True
            btnDeleteItem.Enabled = True
        End If
        labAction.Text = "Editing.."
        labAction.ForeColor = Color.SaddleBrown

        btnAddItem.Enabled = True

    End Sub  '-btnEditSub_Click-
    '= = = = = = = = = = ==  == 
    '-===FF->

    '-- NEW Sub  Actions--
    '-- NEW Sub  Actions--

    '-btnNewSub_Click-

    Private Sub btnNewSub_Click(sender As Object, e As EventArgs) Handles btnNewSub.Click

        btnCancelSub.Enabled = False

        frameBrowse.Enabled = False
        btnNewSub.Enabled = False
        btnEditSub.Enabled = False
        Call mSubClearShowSub()

        grpBoxSubDetail.Enabled = True
        dtPickerStart.Enabled = True

        listViewSubsItems.Items.Clear()
        '= btnAddItem.Enabled = True
        '= btnDeleteItem.Enabled = False
        dtPickerStart.Enabled = False
        dtPickerEnd.Enabled = False
        dtPickerEnd.Visible = False
        chkEndDate.Checked = False
        chkEndDate.Enabled = False
        dtPickerEnd.Value = CDate("31/12/2100")

        btnSaveEdit.Enabled = False
        mIntSubscription_id = -1

        txtSubCustBarcode.Enabled = True
        labAction.Text = "New Sub.."
        labAction.ForeColor = Color.DarkBlue

        labShowActivated.Text = ""
        labLastPeriodBilled.Text = ""
        labShowTotal.Text = ""

        chkActivateNow.Checked = False
        chkActivateNow.Enabled = False
        chkActivateNow.Text = "Activate Now."

        txtComments.Text = ""
        btnCancelEdit.Enabled = True

        txtSubCustBarcode.Select()

    End Sub '-btnNewSub_Click-
    '= = = = = = = = = = = = =

    '--NEW Subs.  Select Customer..
    '--NEW Subs.  Select Customer..
    '--NEW Subs.  Select Customer..

    '-- CUSTOMER  Enter Pressed --

    Private Sub txtSubCustBarcode_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                    Handles txtSubCustBarcode.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1 As String
        Dim sSql, sEmail, sBarcode As String
        Dim colResult, colRecord As Collection
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent

        If keyAscii = 13 Then '--enter-
            s1 = Trim(txtSubCustBarcode.Text)
            If (s1 <> "") Then  '--have barcode-
                '--lookup barcode-
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [customer] WHERE (barcode='" & s1 & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    sBarcode = s1
                    colRecord = colResult.Item(1)
                    '== MUST be ACCOUNT customer..
                    '== MUST be ACCOUNT customer..
                    '= MsgBox("Test- 'isAccountCust' value is: [" & _
                    '=                     CStr(colRecord.Item("isAccountCust")("value")) & "]", MsgBoxStyle.Information)
                    If (colRecord.Item("isAccountCust")("value") <> True) Then
                        MsgBox("Not an Account Customer.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    '=3519.0309=-- Must have email address.
                    sEmail = Trim(colRecord.Item("email")("value"))
                    If (sEmail = "") OrElse ((InStr(sEmail, "@") <= 0)) Then
                        MsgBox("NB: Customer (" & sBarcode & ") has no valid email address..", MsgBoxStyle.Exclamation)
                        '= MsgBox("That Customer is missing a valid email address !!!.", MsgBoxStyle.Exclamation)
                        '= Exit Sub
                    End If
                    Call mbSetupNewSubCustomer(colRecord)
                Else '--not found..-
                    MsgBox("No Customer found for barcode: " & s1, MsgBoxStyle.Exclamation)
                End If  '-get--
            Else  '- no barcode-
                '-- allow to pass, but not cust or trans. can go-
                '--  Just use validate--
                controlParent.SelectNextControl(textBox1, True, True, True, True)
            End If  '--have barcode-
            keyAscii = 0
        End If  '--key ascii-
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If

    End Sub  '--txtNewSubCustBarcode_KeyPress-
    '= = = = = = = = = =  ==  = = = = = = == 
    '-===FF->

    '-- Customer Search (F2)..--
    '-- Cust Barcode TEXTBOX- Catch F2 on Barcode --
    '--- check for F2 for Cust Lookup--

    Public Sub txtSubCustBarcode_KeyDown(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.Windows.Forms.KeyEventArgs) _
                                                 Handles txtSubCustBarcode.KeyDown

        Dim KeyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        Dim txtBarcode As TextBox = CType(eventSender, System.Windows.Forms.TextBox)
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sSql, s1, sWhere, sEmail, sBarcode As String
        Dim colResult, colRecord As Collection

        txtBarcode = CType(eventSender, System.Windows.Forms.TextBox)  '-save for combo event..-
        '=MsgBox("TESTING- Looking up Customers..", MsgBoxStyle.Information)
        If (KeyCode = System.Windows.Forms.Keys.F2) And _
                                ((Not eventArgs.Shift) And (Not eventArgs.Control)) Then '--lookup Customer--
            '== MUST be ACCOUNT customer..
            '== AND (customer.isAccountCust=1)
            sWhere = "(customer.isAccountCust=1)"
            If Not mbBrowseTable33(mColPrefsCustomer, "Lookup Customers", sWhere, colKeys, colSelectedRow, "Customer") Then
                MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
            Else  '--selected
                labSubCustomerName.Text = ""
                If (Not (colKeys Is Nothing)) AndAlso _
                                (colSelectedRow IsNot Nothing) AndAlso (colSelectedRow.Count > 0) Then
                    '=3519.0309=-- Must have email address.
                    '= MsgBox("TESTING- Looking up Customers.. GOT RESULT", MsgBoxStyle.Information)
                    sEmail = Trim(colSelectedRow.Item("email")("value"))
                    sBarcode = Trim(colSelectedRow.Item("barcode")("value"))
                    '= MsgBox("TESTING- Looking up Customers.. GOT RESULT: " & sEmail & "; " & sBarcode, MsgBoxStyle.Information)
                    If (sEmail = "") OrElse ((InStr(sEmail, "@") <= 0)) Then
                        MsgBox("NB: Customer (" & sBarcode & ") has no valid email address..", MsgBoxStyle.Exclamation)
                        '= Exit Sub
                    End If
                    Call mbSetupNewSubCustomer(colSelectedRow)
                    '=3301.710= mPanelOptTranType.Focus()   '== mDgvSaleItems.Select()   '--focus-
                End If
            End If  '-browse-
        End If  '-F2-
    End Sub  '== txtNewSubCustBarcode_KeyDown-
    '= = = = = = = = = = == = = = = = = = = = 
    '-===FF->

    '-- Customer barcode TEXTBOX- Validating --
    '==   Cust Barcode- Must catch "Validating" event for TAB key. .- 

    Public Sub txtSubCustBarcode_Validating(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As CancelEventArgs) Handles txtSubCustBarcode.Validating
        Dim sBarcode As String
        Dim sSql, sEmail As String
        Dim colResult, colRecord As Collection

        '= Call mClsSale1.txtSaleCustBarcode_Validating(eventSender, eventArgs)
        If (Trim(txtSubCustBarcode.Text) = "") Then
            eventArgs.Cancel = True
            MsgBox("Must Have customer barcode", MsgBoxStyle.Exclamation)
        Else
            '- validate/lookup if not done yet..
            If (msNewSubCustomerBarcode = "") OrElse _
                   (msNewSubCustomerBarcode <> Trim(txtSubCustBarcode.Text)) Then  '-cust not set up--
                '--lookup -
                sBarcode = Trim(txtSubCustBarcode.Text)
                '--  get recordset as collection for SELECT..--
                sSql = "SELECT * FROM [customer] WHERE (barcode='" & sBarcode & "');"
                If gbGetRecordCollection(mCnnSql, sSql, colResult) AndAlso _
                                       (Not (colResult Is Nothing)) AndAlso (colResult.Count > 0) Then
                    '--have a row..-
                    colRecord = colResult.Item(1)
                    '== MUST be ACCOUNT customer..
                    If (colRecord.Item("isAccountCust")("value") <> True) Then
                        MsgBox("Not an Account Customer.", MsgBoxStyle.Exclamation)
                        eventArgs.Cancel = True
                        Exit Sub
                    End If
                    sEmail = (colRecord.Item("email")("value"))
                    If (sEmail = "") OrElse ((InStr(sEmail, "@") <= 0)) Then
                        MsgBox("NB: Customer (" & sBarcode & ") has no valid email address..", MsgBoxStyle.Exclamation)
                    End If
                    Call mbSetupNewSubCustomer(colRecord)
                    '== mPanelOptTranType.Focus()   '==  '==mDgvSaleItems.Select()   '--focus-
                Else '--not found..-
                    eventArgs.Cancel = True
                    MsgBox("No Customer found for barcode: " & sBarcode, MsgBoxStyle.Exclamation)
                End If  '-get--
            End If  '-set up-
        End If  '-text-
    End Sub  '--vailidating-
    '= = = = = = = = = = = = = = = 
    '-===FF->

    '-cboBillingCycle_SelectedIndexChanged-

    Private Sub cboBillingCycle_SelectedIndexChanged(sender As Object, e As EventArgs) _
                                                     Handles cboBillingCycle.SelectedIndexChanged

        labShowPeriod.Text = cboBillingCycle.SelectedItem
        '--IF Is new sub..  
        '--   check if we can Save now..
        If txtSubCustBarcode.Enabled Then '-- NEW- (UCase(VB.Left(labAction.Text, 1)) = "N") Then
            '- Is New sub.
            dtPickerStart.Value = Today
            dtPickerStart.Enabled = True

            btnAddItem.Enabled = True
            btnDeleteItem.Enabled = False

            chkEndDate.Enabled = True
            chkActivateNow.Enabled = True
        Else   '=If (UCase(VB.Left(labAction.Text, 1)) = "E") Then
            '-editing
        End If  '-new

    End Sub  '--cboBillingCycle_SelectedIndexChanged-
    '= = = = = = = = = 

    '-- ok To Email Invoices.
    '==    -- 4201.0929.  Started 19-September-2019-
    '==        -- Add column to Subscriptions Table: "OkToEmailInvoices bit default 0"

    Private Sub chkOkToEmailInvoices_CheckedChanged(sender As Object, ev As EventArgs) _
                                                     Handles chkOkToEmailInvoices.CheckedChanged
        mbDataModified = True
        Call mbCheckSaveOk()

    End Sub  '-chkOkToEmailInvoices-
    '= = = = = = = =  = = = = = = =

    '-- dtPickerStart_ValueChanged-

    Private Sub dtPickerStart_ValueChanged(sender As Object, e As EventArgs) Handles dtPickerStart.ValueChanged

        mbStartDateChanged = True
        mbDataModified = True
        Call mbCheckSaveOk()

    End Sub  '- dtPickerStart_ValueChanged-
    '= = = = = = = = = = = = = = = = = = 


    '--chkEndDate_CheckedChanged-

    Private Sub chkEndDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkEndDate.CheckedChanged
        If chkEndDate.Checked Then
            '-wants it-
            dtPickerEnd.Visible = True
            dtPickerEnd.Enabled = True
        Else
            dtPickerEnd.Visible = False
        End If
        mbDataModified = True
        Call mbCheckSaveOk()

    End Sub '-chkEndDate_CheckedChanged-
    '= = = = = = = = = = = == = == = = 

    Private Sub dtPickerEnd_ValueChanged(sender As Object, e As EventArgs) Handles dtPickerEnd.ValueChanged

        '= mbStartDateChanged = True
        mbDataModified = True
        Call mbCheckSaveOk()

    End Sub  '- dtPickerStart_ValueChanged-
    '= = = = = = = = = = = = = = = = = = 

    '-chkSetStartDate-

    Private Sub chkActivateNow_CheckedChanged(sender As Object, e As EventArgs) _
                                                    Handles chkActivateNow.CheckedChanged
        If chkActivateNow.Checked Then
            '= dtPickerStart.Enabled = True
            mbActivateSubNow = True
        Else
            '= dtPickerStart.Enabled = False
            mbActivateSubNow = False
        End If
        mbDataModified = True
        Call mbCheckSaveOk()
    End Sub  '-chkSetStartDate-
    '= = = = = = = = = = = =  = = = =

    '-comments-
    Private Sub txtComments_TextChanged(sender As Object, e As EventArgs) Handles txtComments.TextChanged

        If mbLoadingSub Then Exit Sub

        mbDataModified = True
        Call mbCheckSaveOk()

    End Sub '-comments-
    '= = = = = = = = = = =
    '-===FF->

    '--listView_Click--

    Private Sub listViewSubsItems_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles listViewSubsItems.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim intItemNo As Integer
        Dim colItemSerials As Collection
        '--  update serials info display if selection has moved..--
        '= txtSerialsList.Text = ""
        Dim selectedItems As ListView.SelectedListViewItemCollection = listViewSubsItems.SelectedItems
        If (selectedItems IsNot Nothing) AndAlso (selectedItems.Count > 0) Then
            item1 = selectedItems(0)
        Else
            Exit Sub
        End If

        '= item1 = listViewGoodsItems.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else  '- get selected serials if any.
            intItemNo = CInt(item1.Text)
            'If (mColAllItemSerials IsNot Nothing) Then
            '    If mColAllItemSerials.Contains(item1.Text) Then  '-item no.
            '        colItemSerials = mColAllItemSerials.Item(item1.Text)
            '        For Each sSerial As String In colItemSerials
            '            txtSerialsList.Text &= sSerial & vbCrLf
            '        Next sSerial
            '    End If
            'End If  '-nothing-
        End If '--selected..-
    End Sub '--listViewSubs_Click--
    '= = = = = = = =  =

    '-listViewSubeItems_SelectedIndexChanged-

    Private Sub listViewSubsItems_SelectedIndexChanged(sender As Object, _
                                                       ev As EventArgs) Handles listViewSubsItems.SelectedIndexChanged
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim intItemNo As Integer
        Dim colItemSerials As Collection
        '--  --
        '= txtSerialsList.Text = ""
        Dim selectedItems As ListView.SelectedListViewItemCollection = listViewSubsItems.SelectedItems
        If (selectedItems IsNot Nothing) AndAlso (selectedItems.Count > 0) Then
            item1 = selectedItems(0)
        Else
            btnDeleteItem.Enabled = False
            Exit Sub
        End If

        '= item1 = listViewGoodsItems.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else  '- get selected serials if any.
            intItemNo = CInt(item1.Text)
            'If (mColAllItemSerials IsNot Nothing) Then
            '    If mColAllItemSerials.Contains(item1.Text) Then  '-item no.
            '        colItemSerials = mColAllItemSerials.Item(item1.Text)
            '        For Each sSerial As String In colItemSerials
            '            txtSerialsList.Text &= sSerial & vbCrLf
            '        Next sSerial
            '    End If

            'End If  '-nothing-
            btnDeleteItem.Enabled = True
        End If '--selected..-

    End Sub '-listViewItems_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '=For Context menu.
    Private mItemSelectedPart As System.Windows.Forms.ListViewItem

    '--  menu stuff for edit part info.-
    '- Subscription Quantities are always INTEGERs..
    '- Subscription Quantities are always INTEGERs..

    '-- Edit Price of selected part..-
    Public Sub mnuEditPrice_Click(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) _
                                        Handles mnuEditPrice.Click
        Dim s1 As String = ""
        Dim sTaxCode, sQty, sPrice As String
        Dim decNewPrice, decExtension As Decimal
        Dim intQty, xPos, yPos As Integer

        If Not (mItemSelectedPart Is Nothing) Then
            '- set input position.
            yPos = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) \ 2)
            xPos = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) \ 3) * 2

            '-- get Price and Qty..
            sQty = Trim(mItemSelectedPart.SubItems(k_ListViewIndexQty).Text) '-- Price is -9th columns..-.
            If IsNumeric(sQty) Then
                intQty = CInt(sQty)
            Else
                intQty = 0
            End If
            sTaxCode = Trim(mItemSelectedPart.SubItems(k_ListViewIndexTaxCode).Text) '-- Price is -9th columns..-.
            s1 = Trim(mItemSelectedPart.SubItems(k_ListViewIndexPrice).Text) '-- Price is -5th columns..-.
            sPrice = "0.00"
            If IsNumeric(s1) Then
                sPrice = FormatCurrency(CDec(s1), 2)
            End If
            '--  Show/Edit in InputBox..
            sPrice = InputBox("Tax Code is: " & sTaxCode & vbCrLf & vbCrLf & _
                                "Enter Price (incl Tax)", "New Item Price", sPrice, xPos, yPos)
            If (Trim(sPrice) = "") Then
                Exit Sub
            ElseIf IsNumeric(sPrice) Then
                '-- save new price..
                mItemSelectedPart.SubItems(k_ListViewIndexPrice).Text = FormatCurrency(CDec(sPrice))
                '-re-compute Extension..
                decExtension = CDec(sPrice) * intQty
                mItemSelectedPart.SubItems(k_ListViewIndexAmount).Text = FormatCurrency(CDec(decExtension))
                mbItemsModified = True
                mbDataModified = True
                Call mSubShowTotalSubvalue()
                Call mbCheckSaveOk()
            Else
                MsgBox("Price must be numeric ! ", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If '-nothing-
    End Sub '--part Price
    '= = = = = = = = = = =
    '-===FF->

    '-- Edit Qty of selected part..-
    '- Subscription Quantities are always INTEGERs..

    Public Sub mnuEditQuantity_Click(ByVal eventSender As System.Object, _
                                              ByVal eventArgs As System.EventArgs) _
                                        Handles mnuEditQuantity.Click
        Dim s1 As String = ""
        Dim sTaxCode, sQty, sPrice As String
        Dim decPrice, decNewQty, decExtension As Decimal
        Dim intQty, xPos, yPos As Integer

        If Not (mItemSelectedPart Is Nothing) Then
            '- set input position.
            yPos = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) \ 2)
            xPos = ((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) \ 3) * 2

            '-- get Price and Qty..
            sQty = Trim(mItemSelectedPart.SubItems(k_ListViewIndexQty).Text) '-- Price is -9th columns..-.
            If IsNumeric(sQty) Then
                intQty = CInt(sQty)
            Else
                intQty = 0
            End If
            sQty = intQty.ToString
            '= sTaxCode = Trim(mItemSelectedPart.SubItems(k_ListViewIndexTaxCode).Text) '-- Price is -9th columns..-.
            s1 = Trim(mItemSelectedPart.SubItems(k_ListViewIndexPrice).Text) '-- Price is -5th columns..-.
            sPrice = "0.00"
            If IsNumeric(s1) Then
                sPrice = FormatCurrency(CDec(s1), 2)
            Else
                MsgBox("Item Price is invalid ", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            decPrice = CDec(sPrice)

            '--  Show/Edit in InputBox..
            sQty = InputBox("Enter Quantity" & vbCrLf, "New Item Quantity", sQty, xPos, yPos)
            If (Trim(sQty) = "") Then
                Exit Sub
            ElseIf IsNumeric(sQty) Then
                If Integer.TryParse(sQty, intQty) Then
                Else
                    MsgBox("Qty must be numeric, and an integer ! ", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                '-- save new Qty..
                mItemSelectedPart.SubItems(k_ListViewIndexQty).Text = sQty
                '-re-compute Extension..
                decExtension = CDec(sPrice) * CInt(sQty)
                mItemSelectedPart.SubItems(k_ListViewIndexAmount).Text = FormatCurrency(CDec(decExtension))
                mbItemsModified = True
                mbDataModified = True
                Call mSubShowTotalSubvalue()
                Call mbCheckSaveOk()
            Else
                MsgBox("Qty must be numeric, and an integer ! ", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If '-nothing-

    End Sub '- mnuEditQuantity-
    '= = = = = = = = = = = == =
    '-===FF->

    '--mouse activity---
    '-- Popup COPY Part info menu..--
    '-- Popup COPY Part info menu..--

    Private Sub listViewSubsItems_MouseDown(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.Windows.Forms.MouseEventArgs) _
                                     Handles listViewSubsItems.MouseDown
        Dim iButton As Short = eventArgs.Button \ &H100000
        Dim iShift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        '=Dim idxView As Short = listViewSubsItems.GetIndex(eventSender)
        Dim lRow, ix, lCol As Integer
        Dim sName As String
        Dim j, i, k As Integer
        Dim index As Short
        Dim item1 As System.Windows.Forms.ListViewItem

        item1 = listViewSubsItems.FocusedItem
        If Not (item1 Is Nothing) Then '--item selected.. -
            If iButton = 1 Then '--left --
                '-- MsgBox "Left Mouse clicked on row: " & index & "..", vbInformation
            ElseIf iButton = 2 Then  '--right..-
                '-- Avoid the 'disabled' gray text by locking updates
                LockWindowUpdate(listViewSubsItems.Handle.ToInt32)
                '---- A disabled TextBox will not display a context menu
                listViewSubsItems.Enabled = False
                '--- Give the previous line time to complete
                System.Windows.Forms.Application.DoEvents()
                '-- Display our own context menu
                mItemSelectedPart = item1 '--pass item to mnu routine..-

                '= mContextMenuPartInfo.Show(CType(eventSender, Control), listViewSubsItems.Location)
                mContextMenuPartInfo.Show(CType(eventSender, Control), New System.Drawing.Point(440, 20))

                ' Enable the control again
                listViewSubsItems.Enabled = True
                '-- Unlock updates
                LockWindowUpdate(0)
                '--End If  '--yes..-
            End If '--button..-
        End If '--nothing--
    End Sub '--mouse..-
    '= = = = = = = = =  =
    '-===FF->

    '- listViewSubsItems  --ADD/DELETE
    '- listViewSubsItems  --ADD/DELETE
    '- listViewSubsItems  --ADD/DELETE

    '- btn A d d  I t e m-
    '- btn A d d  I t e m-

    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click

        Dim sCat1, sDescr, sBarcode As String
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sSql, s1 As String
        '== Dim colResult, colRecord As Collection
        Dim intStock_id As Integer
        Dim listItem1 As ListViewItem
        '=Dim rdrSerials As OleDbDataReader
        Dim intItemNo As Integer = 0
        '=Dim colItemSerials As Collection
        Dim sSalesTaxCode As String
        Dim decSellExTax, decSellTaxAmount, decSellIncTax, decExtension As Decimal
        Dim intItemQty As Integer
        '==  load items listview..

        If (listViewSubsItems.Items.Count > 0) Then
            intItemNo = listViewSubsItems.Items.Count  '-next item no.
        End If
        '-- show browser to lookup stock..

        If Not mbBrowseTable33(mColPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, "stock") Then
            MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
        Else  '--selected
            If (Not (colSelectedRow Is Nothing)) AndAlso (colSelectedRow.Count > 0) Then
                intStock_id = CInt(colSelectedRow("stock_id")("value"))
                sBarcode = colSelectedRow("barcode")("value")
                sCat1 = colSelectedRow("cat1")("value")
                sDescr = colSelectedRow("description")("value")
                sSalesTaxCode = colSelectedRow("sales_taxCode")("value")
                decSellExTax = CDec(colSelectedRow("sellExTax")("value"))  '--rrp ex tax-

                intItemQty = 1  '= TEMP ==   CDec(rdrItems.Item("quantity"))

                '-- See Context Menu for INPUTBOX to ASK for Quantity..

                If (sSalesTaxCode = "GST") Then '= (sGoodsTaxcode = "GST") Then
                    decSellTaxAmount = ((decSellExTax * mDecGST_rate / 100))
                Else
                    decSellTaxAmount = 0
                End If
                decSellIncTax = decSellExTax + decSellTaxAmount
                '-- extension-
                decExtension = decSellIncTax * intItemQty

                listItem1 = New ListViewItem(CStr(intItemNo))  '-- number lines from 1..
                listItem1.SubItems.Add(sBarcode)  '-barcode
                listItem1.SubItems.Add(sCat1)
                listItem1.SubItems.Add(sDescr)
                listItem1.SubItems.Add(FormatCurrency(decSellIncTax, 2))
                listItem1.SubItems.Add(CStr(intItemQty))
                listItem1.SubItems.Add(FormatCurrency(decExtension, 2))
                listItem1.SubItems.Add(CStr(intStock_id))
                '==3411.0304=
                listItem1.SubItems.Add(CStr(sSalesTaxCode))

                listViewSubsItems.Items.Add(listItem1)
                mbItemsModified = True
                mbDataModified = True
                Call mSubShowTotalSubvalue()
                Call mbCheckSaveOk()
            End If  '-nothing.-
        End If '--lookup-
        mSubShowTotalSubvalue()

    End Sub  '- btnAddItem-
    '= = = = = = = = = = == = = =
    '-===FF->

    '-btnDeleteItem-

    Private Sub btnDeleteItem_Click(sender As Object, e As EventArgs) Handles btnDeleteItem.Click

        '= Dim idxView As Short = cmdDeletePart.GetIndex(eventSender)
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngError As Integer
        Dim sCat1, sDescr, sBarcode As String

        Try
            '==3041.0= Call listViewParts_DoubleClick(ListViewParts.Item(idxView), New System.EventArgs())
            item1 = listViewSubsItems.FocusedItem '--   .ListIndex
            If (item1 Is Nothing) Then
                MsgBox("Nothing selected", MsgBoxStyle.Exclamation)
            Else '-ok-
                '-k_ListViewIndexCat1-
                sCat1 = item1.SubItems(k_ListViewIndexCat1).Text '-- ListTasks.List(idx)
                sDescr = item1.SubItems(k_ListViewIndexDescription).Text '-- ListTasks.List(idx)
                sBarcode = item1.SubItems(k_ListViewIndexItemBarcode).Text
                If MsgBox("Do you want to DELETE this item from the Subscription:" & vbCrLf & vbCrLf & _
               "Descr:  " & sCat1 & "- " & sDescr & vbCrLf & "Barcode: " & sBarcode & vbCrLf & vbCrLf, _
                 MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    listViewSubsItems.Items.RemoveAt(item1.Index) '--REMOVE from view..--
                    mbItemsModified = True
                End If  '-yes-
                System.Windows.Forms.Application.DoEvents()
            End If  '--nothing..-
            mbItemsModified = True
            mbDataModified = True
            Call mbCheckSaveOk()
            Call mSubShowTotalSubvalue()

        Catch ex As Exception
            MsgBox("Runtime Error in cmdDeletePart sub.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

    End Sub  '-btnDeleteItem-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- EditSub-NewSub--
    '--  RESULTS..

    '-cancel Edit or NEW item..

    Private Sub btnCancelEdit_Click(sender As Object, e As EventArgs) Handles btnCancelEdit.Click

        If btnNewSub.Enabled Then '==NEW= mIntSubscription_id <= 0 Then '= grpBoxSubDetail.Enabled Then  '-new-
            '--cancel new sub..
            Call mSubClearShowSub()

        Else  '-cancel edit existing.-
            Call mSubClearShowSub()
        End If  '-new-
        labAction.Text = ""

        frameBrowse.Enabled = True
        Call mbBrowseSubsTable()
        btnNewSub.Enabled = True

    End Sub  '-cancel Edit or NEW item..
    '= = = = = = = = = = = = = == =
    '-===FF->

    '-btnSaveEdit_Click-

    Private Sub btnSaveEdit_Click(sender As Object, e As EventArgs) Handles btnSaveEdit.Click
        Dim sqlTransaction1 As OleDbTransaction
        Dim sSql As String
        Dim intSubscription_id, intID As Integer
        Dim sPeriodItem() As String = Split(Trim(cboBillingCycle.SelectedItem))
        Dim sPeriod As String

        If (listViewSubsItems.Items.Count <= 0) Then
            btnSaveEdit.Enabled = False
            MsgBox("Subscription must have at least one stock item..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If (mIntSubscription_id <= 0) Then  '-new-
            sPeriod = sPeriodItem(0)  '--1M, 3M part etc..
            '--Save new sub..
            sSql = "INSERT INTO dbo.Subscription "
            sSql &= "  (customer_id, staff_id, billingPeriod, terminal_id, start_date,"
            If chkEndDate.Checked Then
                sSql &= " termination_date, "
            End If
            If chkActivateNow.Checked Then
                sSql &= " isActivated, "
            End If
            '-4201.0929= Can Email Invoices."OkToEmailInvoices"..
            If chkOkToEmailInvoices.Checked Then
                sSql &= " OkToEmailInvoices, "
            End If  '-email.

            sSql &= " comments "
            sSql &= " )"

            '--values list..
            sSql &= " VALUES ( " & CStr(mIntNewSubCustomer_id) & ", " & CStr(mIntStaff_id) & ", "
            sSql &= " '" & sPeriod & "', "
            sSql &= " '" & gsFixSqlStr(msComputerName) & "', "
            sSql &= " '" & Format(dtPickerStart.Value, "dd-MMM-yyyy") & "', "
            If chkEndDate.Checked Then
                sSql &= " '" & Format(dtPickerEnd.Value, "dd-MMM-yyyy") & "', "
            End If
            If chkActivateNow.Checked Then
                sSql &= " 1, "
            End If
            '-4201.0929= Can Email Invoices."OkToEmailInvoices"..
            If chkOkToEmailInvoices.Checked Then
                sSql &= " 1, "
            End If  '-email.

            '= sSql &= " '" & Format(dtPickerEnd.Value, "dd-MMM-yyyy") & "'); "
            sSql &= " '" & gsFixSqlStr(txtComments.Text) & "' "
            sSql &= " ); "

            '--Start the INSERT transaction-
            mCnnSql.ChangeDatabase(msSqlDbName) '--USE our db..-
            sqlTransaction1 = mCnnSql.BeginTransaction
            Try  '--Main try-
                '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
                '-- Create NEW subs. record..
                '== sqlTransaction1 = mCnnSql.BeginTransaction
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    MsgBox("Saving New Subscription FAILED..", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If  '--exec invoice-
                '- 3. Retrieve Invoice No. (IDENTITY of Invoice record written.)-
                '-- get id..
                '=3411.0129=  sSql = "SELECT CAST(IDENT_CURRENT ('dbo.Subscription') AS int);"
                '-SCOPE_IDENTITY-
                sSql = "SELECT CAST(SCOPE_IDENTITY () AS int);"
                If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
                    intSubscription_id = intID
                    '-- update invoice display later..-
                Else
                    MsgBox("Failed to retrieve latest Subscription No..", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
                '-- insert contents of list view..
                sSql = msMakeItemsInsertSql(intSubscription_id)
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    MsgBox("Saving New Subscription LINES FAILED..", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If  '--exec subs.-
            Catch ex As Exception
                MsgBox("Error writing new Subscription-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                sqlTransaction1.Rollback()
                Exit Sub
            End Try
            '-- Commit transaction..
            Try
                sqlTransaction1.Commit()
                MsgBox("New Subscription Transaction committed ok.." & vbCrLf & _
                         "Subscription No (Id) is:  " & CStr(intSubscription_id), MsgBoxStyle.Information)
            Catch ex As Exception
                Try
                    sqlTransaction1.Rollback()
                    MsgBox("Subsciption Transaction commit FAILED.. rollback completed.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    '=MsgBox("Transaction rolback completed.. " & vbCrLf & ex.Message, MsgBoxStyle.Information)
                Catch ex2 As Exception
                    MsgBox("Subs.Transaction commit FAILED.. and Rollback FAILED.. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
                Exit Sub
            End Try  '-commit-
            '-- Clear all new frame items..
            '-- below--  Exit Sub  '--all done.

        Else  '- Editing.-Save changes in existing sub. .-
            '-- UPDATING --
            '-- Can only modify start date, activated status, Line Items List, and comments..
            If Not mbDataModified Then
                MsgBox("Nothing modified.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
            '--Start transaction-
            mCnnSql.ChangeDatabase(msSqlDbName) '--USE our db..-
            sqlTransaction1 = mCnnSql.BeginTransaction
            Try  '--Update Main try-
                '-- if Item list changed then DELETE all, 
                '--   (Delete existing subs. child lines)..
                '-- and then INSERT ListView Contents.
                If mbItemsModified Then
                    sSql = "DELETE FROM dbo.SubscriptionLine "
                    sSql &= "  WHERE subscription_id= " & CStr(mIntSubscription_id) & "; "
                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                        MsgBox("DELETING old Subscription lines FAILED..", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If  '--exec invoice-
                    '-- and INSERT ListView Contents.
                    sSql = msMakeItemsInsertSql(mIntSubscription_id)
                    If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                        MsgBox("UPDATE- INSERTING New Subscription LINES FAILED..", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If  '--exec subs.-
                End If  '-modified-
                '- main update-
                Dim sUpdateList As String = ""
                If chkActivateNow.Enabled Then
                    If chkActivateNow.Checked Then
                        '-- Activating now..
                        sUpdateList &= " isActivated =1, "
                    End If
                End If
                '-4201.0929= Can Email Invoices."OkToEmailInvoices"..
                If chkOkToEmailInvoices.Checked Then
                    sUpdateList &= " OkToEmailInvoices =1, "
                Else '-no-
                    sUpdateList &= " OkToEmailInvoices =0, "
                End If
                '= = =

                '-- start date-
                If mbStartDateChanged Then
                    sUpdateList &= " start_date = '" & Format(dtPickerStart.Value, "dd-MMM-yyyy") & "', "
                End If '- comments-
                '-- Termin. date-
                If chkEndDate.Checked Then
                    sUpdateList &= " termination_date = '" & Format(dtPickerEnd.Value, "dd-MMM-yyyy") & "', "
                End If '- comments-
                sUpdateList &= " comments= '" & gsFixSqlStr(txtComments.Text) & "', "
                sUpdateList &= " date_updated = CURRENT_TIMESTAMP "
                '--- finish update--
                sSql = "UPDATE dbo.Subscription SET " & sUpdateList
                sSql &= "  WHERE subscription_id= " & CStr(mIntSubscription_id) & "; "
                If Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
                    MsgBox("DELETING old Subscription lines FAILED..", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If  '--exec invoice-
            Catch ex As Exception
                MsgBox("Error UPDATING Subscription-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                sqlTransaction1.Rollback()
                Exit Sub
            End Try

            '-- Commit DELETE/INSERT/UPDATE transaction.
            Try
                sqlTransaction1.Commit()
                MsgBox("UPDATE Subscription Transaction committed ok.." & vbCrLf & _
                         "Subscription No. was:  " & mIntSubscription_id, MsgBoxStyle.Information)
            Catch ex As Exception
                Try
                    sqlTransaction1.Rollback()
                    MsgBox("Subsciption UPDATE Transaction commit FAILED.. rollback completed.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    '=MsgBox("Transaction rolback completed.. " & vbCrLf & ex.Message, MsgBoxStyle.Information)
                Catch ex2 As Exception
                    MsgBox("Subs.Transaction commit FAILED.. and Rollback FAILED.. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
                Exit Sub
            End Try  '-commit-

            '-- ckearr the details frame info.
        End If  '-new-

        Call mSubClearShowSub()
        labAction.Text = ""

        frameBrowse.Enabled = True
        btnNewSub.Enabled = True
        btnEditSub.Enabled = False
        btnCancelSub.Enabled = False

        Call mbRefreshInvoiceLog()
        Call mbBrowseSubsTable()

    End Sub  '-btnSaveEdit_Click-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Billing Log Events....
    '-- Billing Log Events....
    '-- Billing Log Events....

    '-- btnRefreshBillGrid-

    Private Sub btnRefreshBillGrid_Click(sender As Object, e As EventArgs) Handles btnRefreshBillGrid.Click

        '--refresh invoicing grid..
        Call mbRefreshInvoiceLog()

    End Sub  '--btnRefreshBillGrid-
    '= = = = = = = = = = = = = = =
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- G r o u p  Invoicing-
    '-- G r o u p  Invoicing-
    '-- G r o u p  Invoicing-

    '--  Print (PDF) and Queue Invoice PDF for ONE item--
    '--  Functionality taken from frmShowInvoice..

    Private Function mbPrintAndQueueInvoice(ByVal intInvoice_id As Integer) As Boolean
        Dim datatableInvoice, dataTableSaleItems As DataTable
        Dim dataTablePayments, dataTablePaymentDetails As DataTable

        Dim sSql As String
        Dim ix, px As Integer
        Dim datarow1 As DataRow
        Dim sCustBarcode, sCustName, sCustomerEmail As String
        Dim bCapturePDF As Boolean = True
        Dim intCustomer_id As Integer

        mbPrintAndQueueInvoice = False

        sSql = "SELECT *, Customer.barcode AS customer_barcode, customer.customer_id, "
        sSql &= "    customer.email as customer_email, staff.barcode AS staff_barcode, staff.docket_name "
        sSql &= "  FROM dbo.Invoice "
        sSql &= "   JOIN Customer on (Customer.customer_id =Invoice.customer_id) "
        sSql &= "   JOIN staff on (staff.staff_id =Invoice.staff_id) "
        sSql &= "    WHERE (invoice_id=" & CStr(intInvoice_id) & ");"

        If Not gbGetDataTable(mCnnSql, datatableInvoice, sSql) Then
            MsgBox("Error in getting recordset for Invoice table: " & vbCrLf & _
                                gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '==Exit Function '--msg was displayed..
            '= Me.Close()
            Exit Function
        Else  '-ok-
            If (datatableInvoice Is Nothing) OrElse (datatableInvoice.Rows.Count <= 0 > 0) Then
                MsgBox("Error in getting recordset for Invoice table: " & vbCrLf & _
                    "Invoice record not found", MsgBoxStyle.Exclamation)
                Exit Function
            End If
        End If  '-get-   
        '-  ok- Have invoice-
        datarow1 = datatableInvoice.Rows(0)

        intCustomer_id = datarow1.Item("customer_id")
        sCustomerEmail = datarow1.Item("customer_email")
        sCustBarcode = datarow1.Item("customer_barcode")
        '= labCustNameLab.Text = "Customer  [" & sCustBarcode & "]"  '=3401.416=
        sCustName = datarow1.Item("firstname") & " " & datarow1.Item("lastname") & vbCrLf & _
                           datarow1.Item("companyName")
        '-- get Line details..-
        sSql = "SELECT * FROM dbo.InvoiceLine "
        sSql &= "  JOIN stock ON (stock.stock_id= invoiceLine.stock_id) "
        sSql &= "  WHERE (invoice_id=" & CStr(intInvoice_id) & ")"

        '- get line items..
        sSql &= "   ORDER BY line_id;"
        If Not gbGetDataTable(mCnnSql, dataTableSaleItems, sSql) Then
            MsgBox("Error in getting recordset for InvoiceLine table: " & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            Exit Function '--msg was displayed..
        Else
            If (dataTableSaleItems Is Nothing) Then
                MsgBox("Error in getting recordset for InvoiceLine table: " & vbCrLf & _
                               "  No datatable was returned..", MsgBoxStyle.Exclamation)
                Exit Function '--msg was displayed..
            End If
        End If
        '-- No payments, as this invoice was created just now for subscriptions.
        dataTablePayments = New DataTable ''- will be empty..
        dataTablePaymentDetails = New DataTable ''- will be empty..

        Dim printSaleDocs1 As clsPrintSaleDocs
        Dim sTerms As String = mSysInfo1.item("POS_ACCOUNTTERMS") '=mSdSystemInfo.Item("POS_ACCOUNTTERMS")
        Dim sPrinterName As String
        Dim sTitle As String
        Dim sPrintFileFullName As String = ""
        Dim sCustomerName, sSubject, sInvoiceDate As String
        Dim sEmailText As String = msEmailTextInvoice

        '-- Capture PDF-
        sPrinterName = msPdfPrinterName
        sTitle = "Invoice-" & Trim(CStr(intInvoice_id)) & "_" & "Cust-" & Trim(sCustBarcode) & ".pdf"
        sPrintFileFullName = msInvoiceFilePath & "\" & sTitle
        '-- set registry key for Adobe pdf writer..
        '=3411.0109= Check if Microsoft PDF or Adobe.
        '= mbSetAdobeFileName = gbSetAdobeFileName(sFullFilePath, msAppFullname)

        If (InStr(LCase(msPdfPrinterName), "adobe") > 0) Then
            If Not gbSetAdobeFileName(sPrintFileFullName, msAppFullname) Then
                '= mbSetAdobeFileName(sPrintFileFullName) Then
                Exit Function
            End If
        Else '=Microsoft or other.  use PrintToFile setting.-
            '- not adobe-
        End If
        '-- delete old file if exists.
        If My.Computer.FileSystem.FileExists(sPrintFileFullName) Then
            Try
                My.Computer.FileSystem.DeleteFile(sPrintFileFullName)
            Catch ex As Exception
                MsgBox("Failed to delete old file: " & sPrintFileFullName & vbCrLf & ex.Message)
            End Try
        End If

        printSaleDocs1 = New clsPrintSaleDocs
        printSaleDocs1.PrtSelectedPrinterName = sPrinterName

        printSaleDocs1.SystemInfo = mSysInfo1  '= mSdSystemInfo
        printSaleDocs1.UserLogo = mImageUserLogo

        printSaleDocs1.DataTableInvoice = datatableInvoice
        printSaleDocs1.DataTableSaleItems = dataTableSaleItems
        printSaleDocs1.DataTablePayments = dataTablePayments
        printSaleDocs1.DataTablePaymentDetails = dataTablePaymentDetails

        printSaleDocs1.versionPOS = msVersionPOS
        If (sTerms <> "") Then
            printSaleDocs1.TermsText = sTerms
        Else
            printSaleDocs1.TermsText = "All accounts to be paid within 30 days.." & vbCrLf & "Thank You."
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
        '== Me.BringToFront()
        '-If bCapturePDF then Save to Email Queue...
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
        sSubject = "Subscription- invoice No:" & intInvoice_id & "  Dated :" & sInvoiceDate
        sEmailText = Replace(sEmailText, "&&subject", "Re:" & sSubject, , , CompareMethod.Text)
        sEmailText = Replace(sEmailText, "&&greeting", "Dear " & sCustomerName, , , CompareMethod.Text)
        sEmailText = Replace(sEmailText, "&&BusinessName", msBusinessName, , , CompareMethod.Text)
        If Not gbSaveDocumentToEmailQueue(mCnnSql, sPrintFileFullName, sTitle, "PDF", _
                                         "INVOICE", intCustomer_id, -1, intInvoice_id, _
                                         sSubject, sCustomerName, _
                                          sCustomerEmail, _
                                          sEmailText, msEmailQueueSharePath) Then
            MsgBox("Save PDF file to Server Queue has failed..", MsgBoxStyle.Exclamation)
        Else  '-  ók=
            mbPrintAndQueueInvoice = True
            'MsgBox("Pls Note- The Invoice PDF file: " & vbCrLf & sPrintFileFullName & vbCrLf & _
            '      vbCrLf & " has been created OK, and queued for emailing.", MsgBoxStyle.Information)
        End If  '-save-
    End Function  '-mbPrintAndQueueInvoice-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- G r o u p  Invoicing-
    '-- G r o u p  Invoicing-
    '-- G r o u p  Invoicing-

    '--  CREATE/Commit Invoice for ONE item--
    '---   (Single, or as next in Group-

    Private Function mbInvoiceOneSub(ByVal intRowIndex As Integer, _
                                     ByRef sqlMainTransaction As OleDbTransaction, _
                                     ByRef intCreatedInvoice_id As Integer) As Boolean

        mbInvoiceOneSub = False
        intCreatedInvoice_id = -1

        '-intRowIndex is the Selected Grid Row to Invoice..-

        Dim intSubscription_id, intCustomer_id As Integer
        '-Dim value As Object = dgvInvoices.Rows(cellEvent.RowIndex).Cells(cellEvent.ColumnIndex).Value
        Dim sCustName, sBillPeriod As String
        Dim sCustomerBarcode, sCustomerEmail As String
        Dim colSub As Collection  '--main collection sub., 
        Dim colBillSub As Collection  '--main collection sub., and from Billable Collection..
        Dim colItems As Collection    '-- items in this invoice..
        '- Invoice stuff.
        '=Dim sqlTransaction1 As OleDbTransaction
        Dim sMainTable, sLineTable, sBarcode As String
        Dim sSql, sValues, sQty, sAudit_id, sStock_id, sSerialNo As String
        Dim v2 As Object
        '= Dim bIsCredit As Boolean = (LCase(msTransactionType) = "refund")
        Dim bCanPrint, bCanEmail, bIsOnAccount As Boolean
        Dim intInvoice_id, intInvoiceLine_id, intID As Integer
        Dim sSaleDeliveryInstr, sComments, sNewPeriodInfo As String
        '- Invoice sub-totals--
        Dim decSubTotalEx_non_taxable As Decimal = 0
        Dim decSubTotalEx_taxable As Decimal = 0
        Dim decTotalTax As Decimal = 0
        Dim decSubTotal As Decimal = 0
        Dim decDiscountNett As Decimal = 0
        Dim decDiscountTax As Decimal = 0
        Dim decRounding As Decimal = 0
        Dim decTotalEx As Decimal = 0
        Dim decNettTax As Decimal = 0
        Dim decInvoiceTotal As Decimal = 0

        bIsOnAccount = True   '--has to be..-
        bCanEmail = False
        bCanPrint = True

        With dgvInvoices.Rows(intRowIndex)
            intSubscription_id = CInt(.Cells("sub_id").Value)
            intCustomer_id = CInt(.Cells("customer_id").Value)
            sCustName = .Cells("customer").Value
            sCustomerEmail = .Cells("customer_email").Value
            sBillPeriod = .Cells("billing_period").Value
            '= .Cells("due_date_period").Value = sNewPeriodInfo
            sNewPeriodInfo = .Cells("due_date_period").Value
        End With
        sSaleDeliveryInstr = "To Subscription No: " & intSubscription_id & vbCrLf & _
                                "For Period: " & sNewPeriodInfo
        sComments = sSaleDeliveryInstr

        If bIsOnAccount And (sCustomerEmail <> "") And _
               (msPdfPrinterName <> "") And (mbAllowEmailInvoices) Then
            bCanEmail = True   '= dlgQueryCommit1.chkEmail.Checked = True
            '= dlgQueryCommit1.labEmail.Text = msCustomerEmail
        End If

        '-test-
        '== FIX-
        'If (MsgBox("This will Invoice Subscription: " & intSubscription_id & vbCrLf & _
        '           "Cust: " & sCustName, _
        '            MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Ok) Then
        '    mbInvoiceButtonLocked = False
        '    Exit Function
        'End If

        '-- Create Invoice Record..  
        '--  Put Subs-Info into INVOICE  record DeliveryInstructions Column..
        colSub = mColSubsInvoiceLog.Item(CStr(intSubscription_id))  '-get main subscription-
        colBillSub = mColSubsToInvoice.Item(CStr(intSubscription_id))
        colItems = colSub.Item("lineItems")
        Dim colBillItems As Collection '-make new collection of extended items for Invoice..-
        colBillItems = colBillSub.Item("BillLineItems")
        '-- make Subs Invoice Ibfo--
        '  -sSaleDeliveryInstr, sComments 
        '-- Compute Invoice totals..
        Dim sGoodsTaxCode As String
        Dim decCostExTax, decCostTaxAmount, decCostInc As Decimal
        Dim sSalesTaxCode As String
        Dim decSellExTax, decItemQty As Decimal
        '-- Compute Invoice totals, and
        '-   update these for INSERTING the line items..
        Dim decSellExLineTotal As Decimal
        Dim decSellExLineTotal_taxable, decSellExLineTotal_non_taxable As Decimal
        Dim decSellTaxAmount, decSellIncTax As Decimal
        Dim decLineTotalEx, decLineTotalTax, decExtension, decGrossProfit As Decimal
        '-- first, update line items collection with tax and extension..

        '-- Lines sell amounts are all PRE-prepared..

        For Each col1 As Collection In colBillItems
            '-- Subscriptions ALWAYS use latest prices.
            '-- Line Items Include COST..
            sGoodsTaxCode = Trim(UCase(col1.Item("Goods_taxCode")))
            decCostExTax = CDec(col1.Item("costExTax"))  '--cost ex tax-
            decCostInc = decCostExTax   '--case no tax..
            '- compute cost incl. -
            If (sGoodsTaxCode = "GST") Then
                decCostTaxAmount = ((decCostExTax * mDecGST_rate / 100))
            Else
                decCostTaxAmount = 0
            End If
            decCostInc = decCostExTax + decCostTaxAmount

            '-- update invoice totals..
            decSubTotalEx_taxable = CDec(col1.Item("SellExLineTotal_taxable"))
            decSubTotalEx_non_taxable = CDec(col1.Item("SellExLineTotal_non_taxable"))

            decTotalTax += CDec(col1.Item("SellTaxAmount"))   '=decSellTaxAmount
            decSubTotal += CDec(col1.Item("SellIncTax"))   '= decSellIncTax
            '- NO DISCOUNT here..
            '= Dim decDiscountNett As Decimal = 0
            '= Dim decDiscountTax As Decimal = 0
            '==Dim DecRounding As Decimal = 0
            decTotalEx += decSubTotalEx_taxable + decSubTotalEx_non_taxable '= decSellExTaxSubTotal
            decNettTax += CDec(col1.Item("SellTaxAmount"))   '== decSellTaxAmount
            decInvoiceTotal += CDec(col1.Item("LineExtension"))   '= decExtension
        Next col1
        '-- Lines are now all prepared..

        sMainTable = "invoice"
        sLineTable = "invoiceLine"

        '- 1. BEGIN (SQL) TRANSACTION. (cnn.BeginTrans) -
        '-- Uses CALLERs Transaction..
        '=sqlTransaction1 = mCnnSql.BeginTransaction

        '- 2. INSERT Main Invoice record -
        sSql = "INSERT INTO dbo." & sMainTable & " ("
        sSql &= "  staff_id, customer_id, transactionType,  "
        sValues = "VALUES ( " & CStr(mIntStaff_id) & ", " & _
                      CStr(intCustomer_id) & ", 'SALE', "

        sSql &= " JobNumber, delivered_layby_id, terminal_id, cashDrawer, currentWindowsUserName,  "
        sSql &= " isOnAccount, "
        sValues &= CStr(-1) & ", " & CStr(-1) & ", '" & msComputerName & "', "
        sValues &= "'" & gsGetCurrentCashDrawer() & "', '" & msCurrentUserName & "', "
        sValues &= IIf(bIsOnAccount, "1, ", "0, ")

        sSql &= " subtotal_ex_non_taxable, subtotal_ex_taxable, "
        sValues &= CStr(decSubTotalEx_non_taxable) & ", " & CStr(decSubTotalEx_taxable) & ", "

        sSql &= " subtotal_tax, subtotal_inc, discount_nett, discount_tax, "
        sValues &= CStr(decTotalTax) & ", " & CStr(decSubTotal) & ", " & _
                                CStr(decDiscountNett) & ", " & CStr(decDiscountTax) & ", "
        'If Not mbIsQuote Then
        '    sSql &= "  cashOut, "
        '    sValues &= CStr(mDecCashout) & ", "
        'End If
        sSql &= " rounding, total_ex, total_tax, total_inc, "
        sValues &= CStr(decRounding) & ", "
        sValues &= CStr(decTotalEx) & ", " & CStr(decNettTax) & ", " & CStr(decInvoiceTotal) & ", "

        '-- Now Finish-
        sSql &= " comments, deliveryInstructions  "
        sSql &= ") "
        sValues &= " '" & gsFixSqlStr(sComments) & "', '" & gsFixSqlStr(sSaleDeliveryInstr) & "' "
        sValues &= "); "

        If Not mbExecSqlCmd(mCnnSql, sSql & sValues, sqlMainTransaction) Then
            '=mbExecuteSql(mCnnSql, sSql & sValues, True, sqlTransaction1) Then
            txtLogStatus.Text &= "Saving Subs invoice FAILED.." & vbCrLf
            '= mbInvoiceButtonLocked = False
            Exit Function
        End If  '--exec invoice-

        '- 3. Retrieve Invoice No. (IDENTITY of Invoice record written.)-
        sSql = "SELECT CAST(IDENT_CURRENT ('dbo." & sMainTable & "') AS int);"
        If gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlMainTransaction, intID) Then
            '= gbGetSqlScalarIntegerValue_Trans(mCnnSql, sSql, True, sqlTransaction1, intID) Then
            intInvoice_id = intID
            '-- update invoice display later..-
        Else
            MsgBox("Failed to retrieve latest invoice No..", MsgBoxStyle.Exclamation)
            '= mbInvoiceButtonLocked = False
            Exit Function
        End If
        txtLogStatus.Text &= "Saving Invoice #" & intInvoice_id & " details.." & vbCrLf
        DoEvents()

        '- 4. FOR EACH Invoice Line- INSERT Invoice-Line.. -
        '--    And -> NO NEED to UPDATE Stock Record as these are just reg. Subscriptions-
        '= Dim decGrossProfit As Decimal
        '-- BUILD list if inserts..
        sSql = ""
        For Each col1 As Collection In colItems
            sBarcode = col1.Item("stock_barcode")  '= .Cells(k_GRIDCOL_BARCODE).Value
            sQty = CStr(col1.Item("quantity"))  '= .Cells(k_GRIDCOL_QTY).Value
            sStock_id = CStr(col1.Item("stock_id"))
            '=3403.717=- decCost_ex, decSellActual_ex, decGrossProfit As Decimal
            '= decCostExTax = col1.Item("costExTax") '= CDec(.Cells(k_GRIDCOL_COST_EX).Value)
            '= decCostInc = col1.Item("CostIncTax") '= CDec(.Cells(k_GRIDCOL_SELLACTUAL_EX).Value)

            decSellExTax = col1.Item("sellExTax") '= CDec(.Cells(k_GRIDCOL_SELLACTUAL_EX).Value)
            decGrossProfit = (decSellExTax - decCostExTax) * CDec(sQty)
            '-- GET serial audit id for serial status update...
            sAudit_id = "-1" '= .Cells(k_GRIDCOL_SERIAL_AUDIT_ID).Value
            '- check serial entered if needed..
            sSerialNo = "" '=  .Cells(k_GRIDCOL_SERIALNO).Value
            sSql &= "INSERT INTO dbo." & sLineTable & " ("

            sSql &= " invoice_id, stock_id,  SerialNumber, SerialAudit_id, "
            sValues = "VALUES ( " & CStr(intInvoice_id) & ", " & sStock_id & ", " & _
                         "'" & gsFixSqlStr(sSerialNo) & "', " & sAudit_id & ", "
            sSql &= " description, cost_ex, cost_inc, "
            sSql &= " sell_ex, sales_taxCode, sales_taxPercentage, "
            sSql &= " sell_inc, sellActual_ex, sellActual_Tax, sellActual_inc, "
            sSql &= " quantity, total_ex, total_tax, total_inc "
            '=If Not mbIsQuote Then
            sSql &= ", gross_profit  "
            '= End If
            sSql &= ") "
            '-- There's no discount, so sell-actuall is the same as RRP..
            sValues &= "'" & gsFixSqlStr(col1.Item("description")) & "', " & _
                  CStr(col1.Item("costExTax")) & ", " & _
                   CStr(col1.Item("CostIncTax")) & ", " & _
                   CStr(col1.Item("sellExTax")) & ", " & _
                   "'" & gsFixSqlStr(col1.Item("Sales_taxCode")) & "', " & CStr(mDecGST_rate) & ", "
            '-RRP-
            sValues &= CStr(col1.Item("SellIncTax")) & ", "
            '--Actual-
            sValues &= CStr(col1.Item("sellExTax")) & ", " & _
                 CStr(col1.Item("SellTaxAmount")) & ", " & _
                 CStr(col1.Item("SellIncTax")) & ", " & _
                    sQty & ", " & CStr(col1.Item("LineTotalEx")) & ", " & _
                     CStr(col1.Item("LineTotalTax")) & ", " & _
                    CStr(col1.Item("LineExtension"))
            sValues &= ", " & CStr(col1.Item("GrossProfit"))
            'If Not mbIsQuote Then
            '    sValues &= ", " & CStr(decGrossProfit)
            'End If
            sValues &= "); "  '= & vbCrLf
            sSql &= sValues & vbCrLf

            '-- insert this row..-
            'If Not mbExecuteSql(mCnnSql, sSql & sValues, True, sqlTransaction1) Then
            '    txtLogStatus.Text &= "Insert Failed.."
            '    Exit Sub
            'End If  '--exec invoice LINE-
            ''=3403.430=  Layby doesn't actuall leave stock..-
        Next col1  '--Line item-
        '-- NOW DO the insert for all the invoice lines rows..-
        If Not mbExecSqlCmd(mCnnSql, sSql, sqlMainTransaction) Then
            '= Not mbExecuteSql(mCnnSql, sSql, True, sqlTransaction1) Then
            txtLogStatus.Text &= "Insert Subs. Invoice Lines Failed.." & vbCrLf
            '= mbInvoiceButtonLocked = False
            Exit Function
        End If  '--exec invoice LINE-

        '== Now Insert ONE Subscription Invoice line to remember the Sub Invoice.....
        sSql = "INSERT INTO dbo.SubscriptionInvoice ("
        sSql &= " subscription_id, invoice_id, invoice_period_start_date,  invoice_period_end_date) "
        sValues = "VALUES ( " & CStr(intSubscription_id) & ", " & CStr(intInvoice_id) & ", " & _
                             "'" & Format(colBillSub.Item("new_bill_period_start_date"), "dd-MMM-yyyy") & "', " & _
                                       "'" & Format(colBillSub.Item("new_bill_period_end_date"), "dd-MMM-yyyy") & "' "
        sValues &= "); "
        '-- NOW DO the insert for the Sub. invoice lines rows..-
        If Not mbExecSqlCmd(mCnnSql, sSql & sValues, sqlMainTransaction) Then
            '= mbExecuteSql(mCnnSql, sSql & sValues, True, sqlTransaction1) Then
            txtLogStatus.Text &= "Insert Subs. Invoice Lines Failed.." & vbCrLf
            '= mbInvoiceButtonLocked = False
            Exit Function
        End If  '--exec Sub invoice -
        '= txtLogStatus.Text &= "Committing SQL Transaction.." & vbCrLf
        txtLogStatus.Text &= "Done invoice No: " & CStr(intInvoice_id) & vbCrLf

        '- 9.  Commit TRANSACTION.-
        '-- NOT HERE..  Caller must COMMIT..
        'Try
        '    sqlTransaction1.Commit()
        '    'MsgBox("Subs. sale Transaction committed ok.." & vbCrLf & _
        '    '         sMainTable & " No: " & intInvoice_id, MsgBoxStyle.Information)
        intCreatedInvoice_id = intInvoice_id
        mbInvoiceOneSub = True
        '    txtLogStatus.Text &= "Committed ok.." & vbCrLf
        'Catch ex As Exception
        '    Try
        '        sqlTransaction1.Rollback()
        '        MsgBox("Transaction commit FAILED.. rollback completed.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        '        '=MsgBox("Transaction rolback completed.. " & vbCrLf & ex.Message, MsgBoxStyle.Information)
        '    Catch ex2 As Exception
        '        MsgBox("Transaction commit FAILED.. and Rollback FAILED.. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        '    End Try
        '    '= mbInvoiceButtonLocked = False
        '    Exit Function
        'End Try  '-commit-
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '=txtLogStatus.Text &= "Done.."

    End Function  '-one invoice-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--LockAndCheckIfSubsChanged-
    '--  Locks SubsInvoices Table and Checks If Subs Changed..
    '-- Must be called with Active Main Transaction.
    '--   AND Caller must Commit or Rollback main Transaction.


    Private Function mbLockAndCheckIfSubsChanged(ByRef sqlMainTransaction As OleDbTransaction) As Boolean

        Dim bInvoicingAborted As Boolean = False
        Dim sMsg, s1 As String

        mbLockAndCheckIfSubsChanged = False

        Dim dtSubsInvoices As DataTable
        Dim sSqlSubsInvoices As String   '= = "SELECT * FROM Person.Person WITH (TABLOCKX, HOLDLOCK);"
        '=  WAITFOR DELAY '00:01:00' ---Wait a minute!
        Dim rdr1 As OleDbDataReader

        sSqlSubsInvoices = " SELECT subscription_id, invoice_id, "
        sSqlSubsInvoices &= "    invoice_period_start_date, invoice_period_end_date "
        sSqlSubsInvoices &= " FROM SubscriptionInvoice WITH (TABLOCKX, HOLDLOCK) "
        sSqlSubsInvoices &= "    ORDER BY subscription_id, invoice_period_end_date DESC ; "

        '-- use a command +reader  for timeout..
        Dim cmd1 As New OleDbCommand(sSqlSubsInvoices, mCnnSql, sqlMainTransaction)
        cmd1.CommandTimeout = 10  '-seconds.
        Try
            rdr1 = cmd1.ExecuteReader
        Catch ex As Exception
            MsgBox("Error- Failed to get Subs-Invoices." & vbCrLf & _
             ex.Message & vbCrLf & _
             "Subscription Invoicing may be in use by another user." & vbCrLf & _
                 "Try again later..", MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        '-- load datatable..
        dtSubsInvoices = New DataTable
        Try
            dtSubsInvoices.Load(rdr1)
        Catch ex As Exception
            MsgBox("Error in loading Datatable." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
        '--ok.. Table is now locked.
        rdr1.Close()

        '-- Compare last invoice-period end-date for each Sub. with what's in the Grid,
        '-    to make sure no one has snuck in and done it while we were mucking around.
        '-- Capture the first (latest) invoiced end-date for each Sub..
        Dim colSubsGridDates, colSubsLatestDates, col1 As Collection
        Dim intLastSubId As Integer = -1
        Dim intSub1_id, intPos1, rx As Integer
        Dim dLastEndDate As Date
        Dim strLastEndDate As String
        Dim sId As String

        colSubsLatestDates = New Collection
        For Each dtRow1 As DataRow In dtSubsInvoices.Rows
            If (dtRow1.Item("subscription_id") <> intLastSubId) Then
                '- start of new sub.
                colSubsLatestDates.Add(dtRow1.Item("invoice_period_end_date"), CStr(dtRow1.Item("subscription_id")))
                '--rember we got this sub.
                intLastSubId = dtRow1.Item("subscription_id")
            End If  '-last-
        Next dtRow1
        '=.Cells("prev_inv_date").Value = sPrevPeriodStart & vbCrLf & " to " & sPrevPeriodEnd
        '- check all marked subs to see if more invoicing was done...
        colSubsGridDates = New Collection
        rx = 0
        While (rx <= (dgvInvoices.Rows.Count - 1))
            strLastEndDate = ""  '-no prev. invoice.
            With dgvInvoices.Rows(rx)
                intSub1_id = CInt(.Cells("sub_id").Value)
                s1 = .Cells("prev_inv_date").Value
                intPos1 = InStr(LCase(s1), "to")
                If (intPos1 > 0) Then  '-have date.
                    strLastEndDate = Trim(Mid(s1, intPos1 + 2))
                    If IsDate(strLastEndDate) Then
                        col1 = New Collection
                        col1.Add(CStr(intSub1_id), "key")
                        col1.Add(CDate(strLastEndDate), "value")
                        colSubsGridDates.Add(col1, CStr(intSub1_id))
                    End If
                End If '-pos-
            End With
            rx += 1  '--next row.
        End While '-rx-

        '-- check that latest dates are no later than current grid dates..
        '--  So we know that no invoicing was sneaked in.

        '- testing- show all..
        sMsg = ""
        For Each col1 In colSubsGridDates
            sId = col1.Item("key")
            sMsg &= "Sub: " & sId & ".  Date=" & Format(col1.Item("value"), "dd-MMM-yyyy")
            '-- get latest invoice date.
            If colSubsLatestDates.Contains(sId) Then
                sMsg &= ";  Latest Inv endDate=" & Format(colSubsLatestDates.Item(sId), "dd-MMM-yyyy") & vbCrLf
            End If
        Next col1

        '-- TESTING- Abort here..  Caller will Rollback.
        '== MsgBox(sMsg, MsgBoxStyle.Information)
        '=sqlMainTransaction.Rollback()
        '= Exit Function
        '========= == = = = = ===== = = = == = 

        '--Now Check dates to discover any subterranean invoicing..
        '-  NOTE-  Counts are not relevent, as latest Invoice dates collection will
        '--          contain heaps that are not in the grid..

        'If (colSubsGridDates.Count <> colSubsLatestDates.Count) Then
        '    '--Subs invoices have changed..  Report and abort..
        '    bInvoicingAborted = True
        'Else '-count ok.
        '-check all dates..
        Dim dateGrid, dateLatest As Date
        For Each col1 In colSubsGridDates
            sId = col1.Item("key")
            dateGrid = col1.Item("value")
            '-- get latest invoice date.
            If colSubsLatestDates.Contains(sId) Then
                dateLatest = colSubsLatestDates.Item(sId)
                '== dates should be the same day.
                If DateDiff("d", dateGrid, dateLatest) <> 0 Then
                    bInvoicingAborted = True
                End If
            End If
        Next col1
        'End If  '-count-

        If bInvoicingAborted Then
            MsgBox("Error-  The Subs. Invoicing list has been changed by another task..." & vbCrLf & _
                           "This Invoicing run will be aborted..", MsgBoxStyle.Exclamation)
        Else  '-all clear-
            mbLockAndCheckIfSubsChanged = True
            '--  ok--
            '-- Caller must Commit or Rollback main Transaction.
        End If

        '-- done checking concurrency..

    End Function  '--LockAndCheckIfSubsChanged-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  INVOICE Now Cell BUTTON..
    '--  INVOICE Now Cell BUTTON..

    Private mbInvoiceButtonLocked As Boolean = False  '-serialises entry..
    Private objLockInvoice As New Object

    '-dgvInvoices_CellContentClick-

    Private Sub dgvInvoices_CellContentClick(ByVal sender As Object, _
                                                ByVal cellEvent As DataGridViewCellEventArgs) _
                                                     Handles dgvInvoices.CellContentClick
        Dim intSubscription_id, intCustomer_id As Integer
        ''-Dim value As Object = dgvInvoices.Rows(cellEvent.RowIndex).Cells(cellEvent.ColumnIndex).Value
        Dim sCustName, sBillPeriod As String
        Dim sCustomerBarcode, sCustomerEmail As String
        Dim bCanPrint, bCanEmail, bIsOnAccount As Boolean
        Dim intInvoice_id, intInvoiceLine_id, intID As Integer
        Dim sSaleDeliveryInstr, sComments, sNewPeriodInfo As String
        Dim bInvoicingAborted As Boolean = False
        Dim sqlMainTransaction As OleDbTransaction

        If TypeOf dgvInvoices.Columns(cellEvent.ColumnIndex) _
             Is DataGridViewButtonColumn _
             AndAlso Not cellEvent.RowIndex = -1 Then
            '--ok..  check its the Send Invoice "button..
        Else
            Exit Sub
        End If     '= End If  '--not header-
        If (dgvInvoices.Columns(cellEvent.ColumnIndex).Name = "invoice_now") Then
            '-ok-
        Else  '-wrong button..
            MsgBox("Unknown button..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '-invoice now-

        '-don't let it click again..
        '--3519.0319-
        '-- LOCK the buttons.-
        SyncLock objLockInvoice
            If mbInvoiceButtonLocked Then
                MsgBox("Still working !!")
                Exit Sub
            End If
            '-lock it now-
            mbInvoiceButtonLocked = True
        End SyncLock

        bIsOnAccount = True   '--has to be..-
        bCanEmail = False
        bCanPrint = True

        With dgvInvoices.Rows(cellEvent.RowIndex)
            intSubscription_id = CInt(.Cells("sub_id").Value)
            intCustomer_id = CInt(.Cells("customer_id").Value)
            sCustName = .Cells("customer").Value
            sCustomerEmail = .Cells("customer_email").Value
            sBillPeriod = .Cells("billing_period").Value
            '= .Cells("due_date_period").Value = sNewPeriodInfo
            sNewPeriodInfo = .Cells("due_date_period").Value
        End With
        sSaleDeliveryInstr = "To Subscription No: " & intSubscription_id & vbCrLf & _
                                "For Period: " & sNewPeriodInfo
        sComments = sSaleDeliveryInstr

        If bIsOnAccount And (sCustomerEmail <> "") And _
               (msPdfPrinterName <> "") And (mbAllowEmailInvoices) Then
            bCanEmail = True   '= dlgQueryCommit1.chkEmail.Checked = True
            '= dlgQueryCommit1.labEmail.Text = msCustomerEmail
        End If
        '-test-
        '== FIX-
        If (MsgBox("This will Invoice Subscription: " & intSubscription_id & vbCrLf & _
                   "Cust: " & sCustName, _
                    MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) <> MsgBoxResult.Ok) Then
            mbInvoiceButtonLocked = False
            Exit Sub
        End If
        ''-- Create Invoice Record..  
        txtLogStatus.Text &= "ok- Now Invoicing Subscription: " & intSubscription_id & vbCrLf & "For:  " & sCustName & vbCrLf

        '- 1. BEGIN MAIN TRANSACTION. (cnn.BeginTrans) -
        sqlMainTransaction = mCnnSql.BeginTransaction
        '-- NOW-  check for concurrency..
        If mbLockAndCheckIfSubsChanged(sqlMainTransaction) Then
            '--ok.. IS now Locked.. Press on.
            If Not mbInvoiceOneSub(cellEvent.RowIndex, sqlMainTransaction, intInvoice_id) Then
                bInvoicingAborted = True
                txtLogStatus.Text &= "!! FAILED to Invoice Subscription: " & intSubscription_id & vbCrLf & _
                                                                                        "For:  " & sCustName & vbCrLf
                MsgBox("Failed to Create Invoice.")
                '= mbInvoiceButtonLocked = False
                '= Exit Sub
            End If
        Else  '-can't lock..
            bInvoicingAborted = True
        End If  '-check-

        If bInvoicingAborted Then
            sqlMainTransaction.Rollback()
            MsgBox("Single Invoicing ended WITH ERRORS. " & vbCrLf & _
                                 "And no Subs were invoiced.", MsgBoxStyle.Exclamation)
            txtLogStatus.Text &= vbCrLf & "Single Invoicing ended WITH ERRORS. " & vbCrLf & _
                                 "And no Subs were invoiced." & vbCrLf & "= = = = " & vbCrLf
            mbInvoiceButtonLocked = False
            Exit Sub
        Else '-ok 
            Try
                sqlMainTransaction.Commit()
                txtLogStatus.Text &= vbCrLf & "Group Invoicing ended. " & vbCrLf & _
                                     "And one Subs was invoiced." & vbCrLf & "= = = = " & vbCrLf
            Catch ex As Exception
                MsgBox("ERROR in Main Commit for group invoicing." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                bInvoicingAborted = True
                mbInvoiceButtonLocked = False
                Exit Sub
            End Try
        End If

        '--  Show/Print invoice..-

        Dim frmShowInvoice1 As frmShowInvoice
 
        frmShowInvoice1 = New frmShowInvoice
        frmShowInvoice1.connectionSql = mCnnSql
        frmShowInvoice1.InvoiceNo = intInvoice_id
        frmShowInvoice1.isQuote = False  '= bIsQuote
        frmShowInvoice1.islayby = False  '= bIsLayby

        frmShowInvoice1.Staff_id = mIntStaff_id
        '=If bCaptureInvoicePDF Then
        frmShowInvoice1.CaptureInvoicePDF = bCanEmail  '= bCaptureInvoicePDF  '--capture pdf for email..
        frmShowInvoice1.PrintInvoiceAnyway = False  '=SHOW Invoice= bCanPrint  '=  bPrintInvoiceAnyway 
        '= End If
        frmShowInvoice1.UserLogo = mImageUserLogo
        '== ADDED 3401.319=
        '= frmShowInvoice1.selectedInvoicePrinterName = sSelectedInvoicePrinterName
        '== frmShowInvoice1.selectedReceiptPrinterName = sSelectedReceiptPrinterName
        frmShowInvoice1.ShowDialog()

        '== done..=
        MsgBox("Invoice Done. Refreshing grid..", MsgBoxStyle.Information)

        mbInvoiceButtonLocked = False   '-unlock the button..
        '--refresh invoicing grid..
        Call mbRefreshInvoiceLog()

    End Sub '-dgvInvoices_CellContentClick-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Mark All..

    Private Sub btnMarkAll_Click(sender As Object, e As EventArgs) Handles btnMarkAll.Click
        Dim intCount As Integer = mIntMarkAllInvoices()
        '- done-
        '-test-
        '= MsgBox("testing- Marked " & intCount & " Invoice Rows..", MsgBoxStyle.Information)

    End Sub  '-- Mark All..
    '= = = = = = = = = = = = ==

    '--Un- Mark All..
    Private Sub btnUnMarkAll_Click(sender As Object, e As EventArgs) Handles btnUnMarkAll.Click
        Call mIntUnMarkAll()

    End Sub  '-- Mark All..
    '= = = = = = = = = = = = = =
    '-===FF->

    '--btn I n v o i c e  A l l  M a r k e d-
    '--btn I n v o i c e  A l l  M a r k e d-
    '--btn I n v o i c e  A l l  M a r k e d-
    '--btnInvoiceAllMarked-

    '==
    '==    -- 4201.0929.  Started 19-September-2019-
    '==        -- Subscriptions-  Lock Invoicing loop (with higher level transaction),
    '==                   and the SubscriptionInvoices Table to keep out competitors.
    '==        --  DO one loop to do invoicing, and then another loop to do PDF's for email.

    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
    '==         (2)  Add code to invoicing to show list of checked items about to be invoiced to get confirmation.. 
    '==                 (Check/Fix why unchecked items get invoiced !!)
    '==


    Private Sub btnInvoiceAllMarked_Click(sender As Object, e As EventArgs) Handles btnInvoiceAllMarked.Click
        '= Dim frmShowInvoice1 As frmShowInvoice
        Dim intInvoicedCount As Integer = 0
        Dim sMsg, s1 As String
        Dim colInvoiceIDs As Collection
        Dim bInvoicingAborted As Boolean = False
        Dim sqlMainTransaction As OleDbTransaction

        '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
        Dim sList, sListX As String
        Dim bChecked As Boolean '=   = CType(DataGridView1.CurrentCell.Value, Boolean)
        '== END Target-New-Build-4262 -- (Started 14-Aug-2020)


        mIntMarkedGroupCount = 0
        '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
        sList = ""
        sListX = ""
        dgvInvoices.EndEdit()
        '== END Target-New-Build-4262 -- (Started 14-Aug-2020)


        If (dgvInvoices.Rows.Count > 0) Then  '-have some.
            For Each gridRow1 As DataGridViewRow In dgvInvoices.Rows
                '-- check if Marked.
                '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
                bChecked = CType(gridRow1.Cells("MarkToInvoice").Value, Boolean)
                If bChecked Then  '= (CInt(gridRow1.Cells("MarkToInvoice").Value) <> 0) Then  '- checked, so count it-
                    mIntMarkedGroupCount += 1

                    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
                    sList &= CStr(CInt(gridRow1.Cells("sub_id").Value)) & "; "
                    '== END Target-New-Build-4262 -- (Started 14-Aug-2020)
                Else  '-not checked..
                    sListX &= CStr(CInt(gridRow1.Cells("sub_id").Value)) & "; "
                End If
            Next gridRow1
        End If '-count-

        If (mIntMarkedGroupCount > 0) Then
            If (MsgBox("Important Note-" & vbCrLf & _
                       " You are about to Invoice " & mIntMarkedGroupCount & " Subscriptions.. " & vbCrLf & _
                       "      (Subscription Ids to Invoice are " & sList & " )" & vbCrLf & _
                       "      (--Not to be Invoiced Ids are " & sListX & " )" & vbCrLf & _
                       " These will be committed as debits to the customer as they go.." & vbCrLf & vbCrLf & _
                     " Are you sure it's ok to do this invoicing run now? ", _
                     MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Ok) Then
                Exit Sub
            End If
            'txtLogStatus.Text &= vbCrLf & "= = = = = =  =" & vbCrLf & vbCrLf & _
            '                      "Now Group Invoicing " & mIntMarkedGroupCount & "  subs.." & vbCrLf
            panelGroupAction.Enabled = False
            dgvInvoices.Enabled = False
            DoEvents()
            mbGroupCancelRequested = False
            mbGroupInvoicingInProgress = True
            btnPauseInvoicing.Enabled = True
            txtLogStatus.Select()
            Call mbReport(vbCrLf & "= = = = = =  =" & vbCrLf & vbCrLf & _
                       "Now Group Invoicing " & mIntMarkedGroupCount & "  subs..")

            Dim intSubscription_id, intInvoice_id, intCustomer_id As Integer
            Dim sCustomer, sCustomerEmail As String
            Dim rx As Integer = 0

            '-- Start (BEGIN) top level Transaction..
            '-- Read (SELECT with TABLOCKX) the SubscriptionInvoices Table..
            '-- For each Row in Grid, 
            '--     Check with SELECTed invoice datatable if sub has been invoiced while we were not looking.
            '== if any has changed, then ABORT (ROLLBACK Top Transaction, Report to user, and EXIT)

            '- 1. BEGIN MAIN TRANSACTION. (cnn.BeginTrans) -
            '--  All invoices are created under this MAIN Transaction..
            sqlMainTransaction = mCnnSql.BeginTransaction

            '-- NOW-  check for concurrency..
            If mbLockAndCheckIfSubsChanged(sqlMainTransaction) Then
                '--ok.. IS now Locked.. Press on.
            Else
                bInvoicingAborted = True
            End If  '-check-

            '-- done checking concurrency..
            '-- done checking concurrency..

            colInvoiceIDs = New Collection
            '- check all rows and do invoices...
            '-- Do PDF's after invoices are all done..
            rx = 0
            If Not bInvoicingAborted Then
                While (rx <= (dgvInvoices.Rows.Count - 1)) And (Not mbGroupCancelRequested)
                    '-- check if Marked.
                    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
                    bChecked = CType(dgvInvoices.Rows(rx).Cells("MarkToInvoice").Value, Boolean)
                    '==  END Target-New-Build-4262 -- (Started 14-Aug-2020)

                    If bChecked Then  '=(CInt(dgvInvoices.Rows(rx).Cells("MarkToInvoice").Value) <> 0) Then  '- checked.. do it-
                        With dgvInvoices.Rows(rx)
                            intSubscription_id = CInt(.Cells("sub_id").Value)
                            intCustomer_id = CInt(.Cells("customer_id").Value)
                            sCustomer = .Cells("customer").Value
                            sCustomerEmail = Trim(.Cells("customer_email").Value)
                        End With
                        '-- Create Invoice Record..  
                        If Not mbInvoiceOneSub(rx, sqlMainTransaction, intInvoice_id) Then
                            MsgBox("Failed to Create Invoice.")
                            bInvoicingAborted = True
                            Exit While
                        End If
                        '- save invoice id for printing PDF.
                        colInvoiceIDs.Add(intInvoice_id, CStr(intSubscription_id))
                        txtLogStatus.Select()
                        Call mbReport("= = = = ")
                        '--done this invoice..
                        intInvoicedCount += 1
                        '=NOT YET..  dgvInvoices.Rows(rx).Cells("MarkToInvoice").Value = 0  '--uncheck.
                    End If  '-marked-
                    rx += 1  '--next row.
                End While  '--rx-
            End If  '-aborted.
            If mbGroupCancelRequested Then
                bInvoicingAborted = True
            End If

            '- DO MAIN Commit or roll-back before printing..
            '- DO MAIN Commit or roll-back before printing..
            '- DO MAIN Commit or roll-back before printing..
            If Not bInvoicingAborted Then
                Try
                    sqlMainTransaction.Commit()
                    txtLogStatus.Text &= vbCrLf & "Group Invoicing ended ok. " & vbCrLf & _
                                         "And " & intInvoicedCount & " Subs were invoiced." & vbCrLf & "= = = = " & vbCrLf
                Catch ex As Exception
                    MsgBox("ERROR in Main Commit for group invoicing." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    bInvoicingAborted = True
                End Try
            Else  '-aborted-
                sqlMainTransaction.Rollback()
                txtLogStatus.Text &= vbCrLf & "Group Invoicing ended WITH ERRORS. " & vbCrLf & _
                                     "And No Sub was invoiced." & vbCrLf & "= = = = " & vbCrLf
            End If  '-aborted..

            '= Print Pdf's if not aborted.
            If Not bInvoicingAborted Then
                '- check all rows.. Again..  for printing.
                Dim intPrintInvoiceId As Integer
                Dim sCanEmail As String
                rx = 0
                While (rx <= (dgvInvoices.Rows.Count - 1)) And (Not mbGroupCancelRequested)
                    '-- check if Marked.
                    '==   Target-New-Build-4262 -- (Started 14-Aug-2020)
                    bChecked = CType(dgvInvoices.Rows(rx).Cells("MarkToInvoice").Value, Boolean)
                    If bChecked Then  '= (CInt(dgvInvoices.Rows(rx).Cells("MarkToInvoice").Value) <> 0) Then  '- checked.. do it-
                        '-- Create PDF and Send to Email..
                        With dgvInvoices.Rows(rx)
                            intSubscription_id = CInt(.Cells("sub_id").Value)
                            intCustomer_id = CInt(.Cells("customer_id").Value)
                            sCustomer = .Cells("customer").Value
                            sCustomerEmail = Trim(.Cells("customer_email").Value)
                            sCanEmail = UCase(Trim(.Cells("email_invoice").Value))
                        End With
                        '-- get saved invoice id to print.
                        intPrintInvoiceId = colInvoiceIDs(CStr(intSubscription_id))
                        '=3519.0325- No pdf if no email address.
                        If (VB.Left(sCanEmail, 1) = "Y") Then
                            If (sCustomerEmail = "") OrElse ((InStr(sCustomerEmail, "@") <= 0)) Then
                                '-no valid email.
                                txtLogStatus.Text &= vbCrLf & "Invoiced ok, but no Email for: " & vbCrLf & sCustomer & vbCrLf
                            ElseIf Not mbPrintAndQueueInvoice(intPrintInvoiceId) Then
                                txtLogStatus.Text &= "FAILED to queue Invoice PDF file for: " & vbCrLf & sCustomer & vbCrLf
                            Else
                                txtLogStatus.Text &= "-- Done- the Invoice PDF file for: " & vbCrLf & sCustomer & vbCrLf & _
                                      vbCrLf & " has been created OK, and queued for emailing." & vbCrLf
                            End If
                        Else
                            '- no to emailing.
                            'txtLogStatus.Text &= "-- Done- the Invoice for: " & vbCrLf & sCustomer & vbCrLf & _
                            '      vbCrLf & " has been created OK, (NO emailing for this sub)." & vbCrLf
                        End If '-can email-
                        dgvInvoices.Rows(rx).Cells("MarkToInvoice").Value = False '==   Target-New-Build-4262 -- '= 0  '--uncheck.
                    End If  '-marked..
                    rx += 1  '--next row.
                End While  '-rx-
            End If  '-not aborted-
            If mbGroupCancelRequested Then
                MsgBox("Invoicing was stopped by operator.", MsgBoxStyle.Exclamation)
                txtLogStatus.Text &= vbCrLf & "Invoicing Stopped on Request." & vbCrLf & _
                                     "And " & intInvoicedCount & " SubsInvoices were Rolled Back.." & vbCrLf & "= = = = " & vbCrLf
            End If  '-cancelled-
        Else
            MsgBox("Nothing marked..", MsgBoxStyle.Exclamation)
        End If  '-count-

        btnPauseInvoicing.Enabled = False
        '--refresh invoicing grid..
        Call mbRefreshInvoiceLog()

        panelGroupAction.Enabled = True
        dgvInvoices.Enabled = True
        mbGroupInvoicingInProgress = False
        txtLogStatus.Select()
        Call mbReport("Group Invoicing is exiting..")

    End Sub  '-btnInvoiceAllMarked-
    '= = = = = =  = = = = = = == = = = = = = = = =

    '-btnCancelInvoicing-

    'Private Sub btnCancelInvoicing_Click(sender As Object, e As EventArgs)

    'End Sub  '-btnCancelInvoicing--
    '= = = = = = == = = = = = = = =

    '-- btnPauseInvoicing=

    Private Sub btnPauseInvoicing_Click(sender As Object, e As EventArgs) Handles btnPauseInvoicing.Click

        mbGroupCancelRequested = True
    End Sub  '-- btnPauseInvoicing-
    '= = = = = = = = =  = = = = = = 
    '-===FF->

    '-closing-
    '-closing-

    '- close-

    Private Sub close_me()
        Dim bCancel As Boolean = False '= = EventArgs.Cancel
  
        '- inform parent.-
        '- Report to Parent..-
        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

        '    If Not bCancel Then  '--exiting.
        '        If Not (Me.delReport Is Nothing) Then
        '            delReport.Invoke(Me.Name, "FormClosed", "")
        '        End If
        '    End If  '-cancel-
        '    Me.Dispose()
    End Sub '--close me-
    '= = = = = = == = = = =

    '-closing-

    'Private Sub me_formclosing(sender As Object, ev As EventArgs) Handles Me.FormClosing

    '    '- clost mCnnShape if not nothing.
    'End Sub  '-closing-
    '= = = = = = == = = =


End Class  '- frmSubscription-
'= = = = = = = = = = = = = = = 


'== end form ==