
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

Public Class ucChildPosReports
    Inherits UserControl

    '-- Form to print POS Reports..-
    '==
    '==  JobMatix POS3---  10-Sep-2014 ===
    '==   >>  Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient)..
    '==
    '==  JobMatix POS3- 3103.0419--  19-Apr-2015 ===
    '==   >>  "No report" option for zero stock..- 
    '==
    '==  JobMatix POS3- 3107.0710--  10-Jul-2015 ===
    '==   >>  TreeView for Reports Selection...- 
    '==
    '==  JobMatix POS3- 3107.0713--  13-Jul-2015 ===
    '==   >>  MIGRATED to vs-2013 -==...- 
    '==
    '== = =
    '==     v3.3.3301.428..  28-Apr-2016= ===
    '==       >>  Big cleanup of LocalSettings and SysInfo using classes..
    '==
    '== = =
    '==     v3.3.3301.615..  15-Jun-2016= ===
    '==       >>  Cashup Analysis- For re-organised payments table-
    '==         (YES HAVE "mother" payment record. PLUS Details as individ. payment types..
    '--                 So use Payment. as main cashup line anchor..)
    '== = =
    '==     v3.3.3301.701..  01-Jul-2016= ===
    '==         >>  Tidying up cash-up 
    '==
    '==     v3.3.3301.817..  17-Aug-2016= ===
    '==         >>  Tidying up Invoice list (dropping antChargedToAccount)- 
    '==
    '==     v3.3.3301.1229..  29-Dec-2016= ===
    '==       >> Now uses clsCashupPayments for Payments analysis.
    '==       >>  Uses clsReportPrinter for Payments Report (NOT the Grid !)..- 
    '==       >>      and now for all reports.
    '==
    '==     v3.3.3301.0102..  02-Jan-2017= ===
    '==       >> Rewriting Sales/Stock reports to drop Grid..- 
    '==  
    '==
    '==     v3.3.3307.0202/5/11..  15-Feb-2017= ===
    '==       >>  Product Sales and Stock Repott can still use Grid..
    '==       >>  Add Sales by Customer-  Remove Till Balance Stuff...
    '==       >>  21Feb2017=  Fixed Cust sales SQL for Compatiblity Level 2000 (80)...
    '==       >>  27Feb2017=  Fixed Invoice Report SQL
    '==                           (ORDER BY desription) for Compatiblity Level 2000 (80)...
    '= = = = = == = = ==
    '==
    '==  v3.4.3403.0627..  27-Jun-2017===
    '==     >> Drop Cashout columns...
    '==
    '==   3403.717- 17July2017-
    '==      -- Product Sales Report- Show cost-ex, sell_ex columns..
    '==      -- Sales Screen.  Fix errors with Hold, and changing Item barcode..
    '==
    '==
    '==     3403.0917- 15/16/17-Sep-2017-
    '==      -- POS Reports- Fix Invoice Report for NULL Invoice lines..
    '==                Add Final totals for Discount..
    '==
    '==      -- 3411.0131=  Fixes to Stock REport...  Add Ext_Cost_ex col. Total.
    '==                  AND-   Fix to Print Grid to reduce width.. 
    '==
    '== -- Updated 3501.1024  24-Oct-2018=  
    '==     -- Fix Crash in Sales Invoice Report due to Null Payment info....
    '==
    '=== = = = = = =  = = =
    '==
    '==
    '== -- NEW BUILD 3519.0112  12-Jan-2019=  
    '==     -- Fix Sales Invoice Report to show ALL payments (Disbursements) for invoice.....
    '==
    '==   Updated.- 3519.0127 27-Jan-2019= 
    '==     -- Fixes to Stock Sales Report for Stewart-
    '==           '  ie Select Category to Report, and report on Average Sell Price..
    '==
    '==
    '==   Updated.- 3519.0303  Started 02-March-2019= 
    '==     -- New class clsWhatsInStock to redo this report as Standard Report with Cat1 SUBTOTALS...
    '==            (No longer a grid report.)
    '==     -- Product Report- Add option to report on Taxable/Non-taxable....
    '==           and overall profit Rate at the bottom right.
    '==
    '==   Updated.- 3519.0404  Started 30-March-2019= 
    '==    -- Reports-  Product Sales Fix- Must Total TotalCost_ex column (NOT cost_ex.)
    '==                 ALSO DON'T use PrintDataGridView Function- 
    '==                 INSTEAD, Convert to user Standard clsReportPrinter.
    '==
    '==  --Last RELEASED as 3519.0501..
    '==
    '==   Updated.- 3519.0505  Started 05-May-2019= 
    '==    -- PAYMENT REPORTS-  
    '==        Add "Till Payments Analysis" report to differentiate from "Revenue Analysis" Report.
    '==   
    '== - NEW VERSIOnn.
    '==
    '==    -- 4201.0611.  11-June-2019-   
    '==        --   New Report "Goods REceived in Period..
    '==        --   Add Cat2 to Stock Sales Cat selection..
    '==
    '==    -- 4201.0707.  07-July-2019-   
    '==        Sales Invoice List Report runtime error.-
    '==            Needs cstr(Id) as collection index--  MsgBox("Possible error showing payments for Invoice # "
    '==          ALSO reduce no.of lines for each invoice.
    '==
    '==    -- 4201.0831.  31-August-2019-   
    '==        Fix Product Sales Report by Category.-
    '==
    '== NEW Revision-
    '==   -- 4219.1214.  10-Dec-2019-  Started 10-Dec-2019-
    '==       -- POS Reports.. 
    '==            Invoice-Listing- Add TabPage/Panel witrh options to select on-account or cash sales.. 
    '==
    '= = = = = = = = = = = == == =
    '==
    '==   BUILDING for New BUILD 4221..  Started  in DEVEL 27-12-2019..-
    '==    
    '==   == 4221.0206.  06-Feb-2020- 
    '==   --  Reports- 03-Feb-2020..  
    '==            Till Anaysis- Add functions to show the Report in DataGrid also...
    '==
    '= = = = = = = = = = = == == =
    '==
    '==   PREP FOR- Updates to 4221.0207  Started 24-March-2020= 
    '==   PREP FOR- Updates to 4221.0207  Started 24-March-2020= 
    '==    
    '==   == 4233.0416.  16-April-2020- 
    '==
    '==   --  Making Reports into a Child UserControl..
    '==   -- For Sales Invoice Report-  Link to new class for re-write..
    '==   -- FIXed dateAdd for prev 12-months..  (- 1 year PLUS ONE DAY..)..
    '= = = = = = = = =  = = = = = = = ==  == = == = = = = = = = = 
    '==
    '==  - Updates to 4233.0421  Started 24-April-2020= 
    '==
    '==  Target is new Build 4234..
    '==  Target is new Build 4234..
    '==
    '==   ucChildPOSReports-  STILL a CHILD UserControl-
    '==       --  For Sales Invoice Report-  ADD PROFIT on Invoice For BOTH GRID and preview Versions.
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = == = = = = = =
    '==
    '==
    '== DEV PREP Updates to 4251.0606  Started 17-June-2020= 
    '== DEV PREP Updates to 4251.0606  Started 17-June-2020= 
    '==
    '==  Target-New-Build-4253..
    '==  Target-New-Build-4253..
    '==  -- Sales Report- Show Nett sales after Refunds;  Show Product Sales, then Gross Profit After Discount.
    '==  -- ALSO- Add function get totals section from last report run..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==  Target-New-Build-4257..
    '==  Target-New-Build-4257..  07-July-2020.
    '==
    '==   1. MAIN REASON is to Print list of Stock Table Items with barcode in Barcode Font..
    '==            -- ADDS NEW CLASS clsStockBarcodeList..
    '==            --  Also adds extra checkbox to Report Stock Options "Don't include stock with postive qtyInStock." 
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==   Target-New-Build-4282 --  (22-October-2020)
    '==
    '==  -- For POS Sales Reports-  Incorporate  Dropdown to select Staff Sales...
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '=='= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 
    '==
    '==
    '== Fixes to Build 4282.1025  
    '==
    '==   Target-New-Revision-4282.1102 --  (02-November-2020)
    '==   Target-New-Revision-4282.1102 --  (02-November-2020)
    '==     -- Fix PosReports (Load Event) to get rid of "ClearwaterJT" DB name out of Staff Quuery..
    '==
    '==
    '= = = =  = = = = = = = = = = = = = = = = = = = = = = == = = = = = = = = = = == = = = == = = = = = = = = = = = = = 

    '== Target 6201- Updating 12-July-2021..
    '==    Updates For Target OpenSource version Build 6201...
    '==  Add features to Product Sales and What's In Stock to filter for a particular stock Supplier..
    '= = = = =


    Private Const k_reportPrtSettingKey As String = "POS_ReportPrinter"
    Private Const k_receiptPrtSettingKey As String = "POS_ReceiptPrinter"

    Private mbActivated As Boolean = False

    Private mFrmParent1 As Form

    '- - - - -
    Private mbIsInitialising As Boolean = False
    Private mbActive As Boolean = False
    Private mbStartingUp As Boolean = True
    Private msVersionPOS As String = ""

    '==3301.428= 
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""

    Private mSysInfo1 As clsSystemInfo

    Private msDefaultPrinterName As String = ""
    Private msReportPrinterName As String = ""

    Private msCurrentUserName As String = ""
    Private msCurrentUserNT As String = ""

    Private msComputerName As String '--local machine--
    Private msAppPath As String
    Private msLastSqlErrorMessage As String = ""

    Private mIntFormDesignHeight As Integer   '-- starting dimensions..-
    Private mIntFormDesignWidth As Integer    '-- starting dimensions..-


    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    Private msStaffName As String = ""

    '== Private mCnnSql As ADODB.Connection '--
    Private mCnnSql As OleDbConnection '--

    '- SHAPE cnn for us here only- (GoodsReceived.)
    Private mCnnShape As OleDbConnection   '=  ADODB.Connection

    Private msBusinessName As String = ""
    Private msBusinessABN As String = ""

    Private msColourPrinterName As String = ""
    Private msReceiptPrinterName As String = ""

    Private msReportName As String = ""
    Private msReportPeriod As String = ""  '-- for Report print Header..

    '== Private mColReceiptLines As Collection   '- For cashup Summary--
    '== Private mbTillBalanceOnly As Boolean = False

    Private mColReportLines As Collection   '- For Payments Summary--
    Private clsCashupPayments1 As clsCashupPayments

    Private clsReportPrint1 As clsReportPrinter
    Private mColPrefsCustomer As Collection

    Private mColCategoriesTree As Collection

    '=3519.0404==  
    '-- To Print Grid with product sales.
    Private msReportColumnHdr1 As String = ""  '- " Cat1/Cat2  Description    Barcode  "
    Private msReportColumnHdr2 As String = ""

    Private msCurrentNodeName As String = ""

    '-- Loading Grid.
    Private mbIsLoading As Boolean = False
    '-- wait form--
    Private mFormWait1 As frmWait

    '= = = = = = = = = = = = = = = = = = = = =

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport

    '= = = = = = = = = = = = = = = = = = = = == = 
    '-===FF->

    '-version-
    WriteOnly Property versionPOS() As String
        Set(ByVal value As String)
            msVersionPOS = value
        End Set
    End Property  '--version--
    '= = = = = = = = = = = = = = = = = = == 

    '--This Staff Id now comes from caller..--

    WriteOnly Property StaffName() As String
        Set(ByVal Value As String)
            msStaffName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = = = = = = = = =

    WriteOnly Property connectionSql() As OleDbConnection
        Set(ByVal Value As OleDbConnection)

            mCnnSql = Value
        End Set
    End Property '--cnn sql..--
    '= = = = = = =  = = = = =

    WriteOnly Property DBname() As String
        Set(ByVal Value As String)
            msSqlDbName = Value
        End Set
    End Property  '--dbname--
    '= = = = = = = = = = = = = = = = = = = = =

    WriteOnly Property ColSqlDBInfo() As Collection
        Set(ByVal Value As Collection)
            mColSqlDBInfo = Value
        End Set
    End Property  '--db info.--
    '= = = = = = = = = = = = = = = = = = = = =

    WriteOnly Property BusinessName() As String
        Set(ByVal Value As String)
            msBusinessName = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = = = = = = = = =

    WriteOnly Property BusinessABN() As String
        Set(ByVal Value As String)
            msBusinessABN = Value
        End Set
    End Property '--name--
    '= = = = = = = =  = = = = = = = = = =

    '== 3301.704=
    '== 3307.0215=
    'WriteOnly Property TillBalanceOnly As Boolean
    '    Set(ByVal Value As Boolean)
    '        mbTillBalanceOnly = Value
    '    End Set
    'End Property '--TillBalanceOnly--
    '= = = = = = = =  = = = = = = = = = =
    '-===FF->

    '-- NOW CHILD.--
    '--sub new-
    '--sub new-

    Public Sub New(ByRef frmParent As Form)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        mFrmParent1 = frmParent   '-save.

    End Sub  '- New-
    '= = = = = = = == = = 
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
        'panelAction.Left = Me.Width - panelAction.Width - 7

        'dgvCustomers.Width = Me.Width - 11
        'dgvCustomers.Height = Me.Height - dgvCustomers.Top - 12

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

    '-- show WAIT form NON-MODAL so it sits there
    '--    while we go on..

    Private Sub mWaitFormOn(ByVal sMsg As String)

        mFormWait1 = New frmWait
        mFormWait1.waitTitle = "POS Reports"  '=msVersionPOS
        mFormWait1.labHdr.Text = "Sales Invoices-"
        mFormWait1.labMsg.Text = sMsg
        mFormWait1.Show(mFrmParent1)
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
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
            Call gbLogMsg(gsRuntimeLogPath, sErrorMsg)
            '= msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- Set up Categories for PARTIAL stock take..
    '-- Builds collection of cat, with sub-collections of cat2's for each cat1.-
    '-- "SELECT DISTINCT Cat1,Cat2 FROM dbo.Stock ORDER BY Cat1,Cat2; "

    '=3519.0128--  COPIED from StockTaking..


    Private Function mbLoadCategories(ByRef colCategoriesTree As Collection) As Boolean
        Dim sSql As String
        Dim s1 As String
        Dim i, j, k, lCount, ix As Integer
        Dim lngCurrentChassis As Integer
        '== Dim rs1 As ADODB.Recordset
        Dim dataTable1 As DataTable
        Dim dr1 As DataRow
        Dim colCat2, col1 As Collection
        Dim colItem As Collection
        Dim v1 As Object
        Dim sCat1, sLastCat1, sCat2 As String

        mbLoadCategories = False
        lngCurrentChassis = -1
        sSql = "SELECT DISTINCT Cat1,Cat2 FROM dbo.Stock "
        '-- service are charges not stocktaked..
        'If (msStockServiceChargeCat1 <> "") Then  '--filter out service chards.-
        '    sSql &= " WHERE (cat1 <>'" & msStockServiceChargeCat1 & "') "
        'End If
        sSql &= " ORDER BY Cat1,Cat2;"
        '== Screen.MousePointer = vbHourglass
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '==If Not gbGetRst(mCnnJet, rs1, sSql) Then
        If Not gbGetDataTable(mCnnSql, dataTable1, sSql) Then
            '=  mRetailHost1.stockGetDistinctCategoryList(colDistinctCat1Cat2) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get Cat1/Cat2 recordset.." + vbCrLf, MsgBoxStyle.Exclamation)
            Exit Function
        Else   '--ok--
            '--fill COMBO box with record fields--
            '== If Not (rs1.BOF And rs1.EOF) Then   '--ok.. not empty--
            If (Not (dataTable1 Is Nothing)) AndAlso (dataTable1.Rows.Count > 0) Then   '--ok.. not empty--
                '== rs1.MoveFirst()
                lCount = 0
                colCategoriesTree = New Collection
                sLastCat1 = "" '--  to start--
                colCat2 = New Collection
                '-- look through cat1-cat2 distinct list-
                '-- pick out cat1's and take assoc. cat2's -
                For ix = 0 To (dataTable1.Rows.Count - 1)
                    dr1 = dataTable1.Rows(ix)
                    sCat1 = UCase(dr1.Item("cat1"))
                    If (sCat1 <> sLastCat1) Then
                        '-- save current cat1 family.
                        If (sLastCat1 <> "") Then '- not the start.
                            col1 = New Collection
                            col1.Add(sLastCat1, "cat1Name")
                            col1.Add(colCat2, "cat2Children")
                            colCategoriesTree.Add(col1, sLastCat1)
                        End If '-not start-
                        colCat2 = New Collection
                        sLastCat1 = sCat1
                    End If  '--cat1 different.-
                    '-- add cat2 to current collection.-
                    sCat2 = UCase(dr1.Item("cat2"))
                    colCat2.Add(sCat2, sCat2)  '-must have key the same so we can use "contains"..
                Next ix
                '-- save last lot..
                If (sLastCat1 <> "") And (colCat2.Count > 0) Then '- not the start.
                    col1 = New Collection
                    col1.Add(sLastCat1, "cat1Name")
                    col1.Add(colCat2, "cat2Children")
                    colCategoriesTree.Add(col1, sLastCat1)
                End If '-not start-
                mbLoadCategories = True
            Else
                MsgBox("No Cat1/Cat2 stock items found..", vbExclamation)
            End If  '--not empty--
            '== cboChassis.ListIndex = lngCurrentChassis
            '==  cboChassis.SelectedIndex = lngCurrentChassis
        End If  '-got rs--
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function  '--mbLoadCategories-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->



    '-- Browse Selected table using --
    '--  Separate DROWSE FORM, and provided sWhere condition..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseTable(ByRef colPrefs As Collection, _
                                    ByRef sTitle As String, _
                                      ByRef sWhere As String, _
                                      ByRef colKeys As Collection, _
                                      ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Customer", _
                                         Optional ByVal bLookupOnly As Boolean = False) As Boolean
        Dim frmBrowse1 As New frmBrowsePOS

        mbBrowseTable = False
        frmBrowse1.connection = mCnnSql '--job tracking sql connenction..-
        frmBrowse1.colTables = mColSqlDBInfo
        frmBrowse1.DBname = msSqlDbName
        frmBrowse1.tableName = sTableName '--"jobs"
        frmBrowse1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        frmBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        frmBrowse1.PreferredColumns = colPrefs
        frmBrowse1.Title = sTitle
        '=3101.1207== add staff.-
        '= frmBrowse1.StaffId = mIntStaff_id
        frmBrowse1.StaffName = msStaffName
        frmBrowse1.versionPOS = msVersionPOS
        If bLookupOnly Then
            frmBrowse1.HideEditButtons = True
            frmBrowse1.lookupSelection = True
        Else '- can edit records-
            frmBrowse1.HideEditButtons = False
            frmBrowse1.lookupSelection = False
        End If
        '--go-
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

    '-- set up Data1/Date2 WHERE SQL condition..-
    '-- based on the Form's  datePickers controls From/To..

    Private Function msReportSetupWhereCondition(ByVal strDateColumn As String, _
                                                 ByRef sWhere As String) As String
        Dim sDate1, sDate2 As String

        '-- format dates for SQL..-
        sDate1 = Format(DTPickerFrom.Value, "dd-MMM-yyyy") & " 00:00"  '-min-
        sDate2 = Format(DTPickerTo.Value, "dd-MMM-yyyy") & " 23:59"  '--max.--
        If (sWhere = "") Then
            sWhere = " WHERE "
        Else
            sWhere = sWhere & " AND "
        End If
        sWhere = sWhere & " ((" & strDateColumn & ">='" & sDate1 & "') AND (" & strDateColumn & "<='" & sDate2 & "')) "

        msReportPeriod = "From: " & sDate1 & "  To: " & sDate2
        msReportSetupWhereCondition = sWhere

    End Function  '--SetupWhereCondition-
    '= = = =  = =  == = = = = = =  = = =

    '- make Sql for CASHUP..-

    Private Function msMakeSalesListSql() As String
        Dim sSql, sWhere As String

        sWhere = ""  '==  " WHERE (invoice.terminal_id='" & msComputerName & "' )"
        '-- All w/s included for invoice list..-
        sWhere = msReportSetupWhereCondition("invoice.invoice_date", sWhere)

        '--GET all Payments for today..-
        '-- Make a grid row for the payments for each invoice..

        sSql = "SELECT invoice.invoice_id, invoice.invoice_date,  invoice.transactionType AS tr_type,  "
        '=3301.817= sSql &= "      invoice.amountDebitedToAccount as amtCharged, "
        sSql &= " isOnAccount, total_inc AS InvTotal, "
        sSql &= "  invoice.discount_nett, invoice.discount_tax, 0 AS cashout, invoice.rounding,  "
        sSql &= "    PaymentType_key, PaymentDetails.amount AS PaymentAmount, "
        sSql &= "    firstName + ' ' + lastName AS Customer "
        sSql &= "  FROM dbo.invoice "
        sSql &= "  LEFT OUTER JOIN payments on (invoice.invoice_id=Payments.invoice_id) "
        sSql &= "  LEFT OUTER JOIN PaymentDetails on (paymentDetails.payment_id=Payments.payment_id) "
        sSql &= "  LEFT OUTER JOIN Customer on (invoice.customer_id=Customer.customer_id) "
        sSql &= sWhere
        sSql &= " ORDER BY invoice.invoice_id;"

        msMakeSalesListSql = sSql

    End Function  '-- make sql--
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Dump DataTable into DataGridView..
    '-- Tfr all columns/rows from Table to Grid..

    Private Function mbDumpTableToGrid(ByRef datatable1 As DataTable, _
                                       ByRef dgv1 As DataGridView) As Boolean
        Dim intCount, ix, rowx As Integer
        Dim datacol1 As DataColumn
        Dim row1 As DataRow

        Dim column1 As DataGridViewColumn
        Dim gridRow1 As DataGridViewRow

        mbDumpTableToGrid = False
        dgv1.DataSource = Nothing
        dgv1.Rows.Clear()
        dgv1.Columns.Clear()
        '-- FIRST- Build Grid Columns and headers..
        Dim cellSample As New DataGridViewTextBoxCell  '-- to make a text-box type column-

        '-- Build Grid columns.-
        For Each datacol1 In datatable1.Columns
            column1 = New DataGridViewColumn
            column1.CellTemplate = cellSample  '-- makes text-box type column-
            column1.HeaderText = datacol1.ColumnName    '
            '== column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '= column1.Width = 50
            dgv1.Columns.Add(column1)
        Next datacol1

        '- add data rows from datatable..
        rowx = 0
        Try
            For Each row1 In datatable1.Rows
                '-- Add a row to the grid for each Payment..
                gridRow1 = New DataGridViewRow  '--prepare datagrid report row..
                dgv1.Rows.Add(gridRow1)
                '-- get cell values into grid..--
                '=3519.0404-  Check data Type..
                For ix = 0 To datatable1.Columns.Count - 1
                    If datatable1.Columns(ix).DataType Is GetType(System.Decimal) Then
                        dgv1.Rows(rowx).Cells(ix).Value = FormatNumber(row1.Item(ix), 2)
                    ElseIf datatable1.Columns(ix).DataType IsNot GetType(System.String) Then
                        dgv1.Rows(rowx).Cells(ix).Value = CStr(row1.Item(ix))
                    Else  '-is string
                        dgv1.Rows(rowx).Cells(ix).Value = CStr(row1.Item(ix))
                    End If  'type-
                Next ix
                '=3411.0202 - Number the rows..
                dgv1.Rows(rowx).HeaderCell.Value = CStr(rowx + 1)  '- Format(rowx + 1, "   00")
                rowx += 1
            Next row1
            mbDumpTableToGrid = True
        Catch ex As Exception
            MsgBox("Error loading datagrid.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
        '-- done-
        '= If mDataTableCurrent.Columns(msCurrentOrder).DataType Is GetType(DateTime) Then


    End Function '--dump--
    '= = = = = = = = = =  =
    '-===FF->

    '-- Main Report Builder.--
    '-- Build report according to Radio Button (opt) selected..

    Private Function mbSetupReport() As Boolean
        Dim intCount, ix, rowx, intInvoice As Integer
        Dim sSql, sWhere, s1, s2, s3 As String
        Dim datatable1 As DataTable

        '= Dim colPaymentTypes As Collection
        '= Dim col1, colInvoices As Collection
        '= Dim colInvoice, colInvoices As Collection
        Dim column1 As DataGridViewColumn
        Dim gridRow1 As DataGridViewRow
        '= Dim decTotalSales As Decimal = 0
        Dim decTotalCost_ex As Decimal = 0
        Dim decTotalSales_ex As Decimal = 0
        Dim decTotalSales_inc As Decimal = 0
        Dim decTotalProfit As Decimal = 0
        '= Dim listPaymentTotals As New List(Of Decimal)  '--paymnt type totals..-
        Dim bPreviewOnly As Boolean = True
        Dim dateStart, dateEnd As Date

        dgvReport.DataSource = Nothing
        dgvReport.Rows.Clear()
        dgvReport.Columns.Clear()
        btnPrintReport.Enabled = False
        sSql = ""
        clsReportPrint1 = New clsReportPrinter

        mColReportLines = New Collection

        Select Case LCase(msReportName)
            Case "" : Exit Function

                '-- Option Cashup analysis -
            Case "optreportrevenueanalysis", "optreporttillanalysis"
                '==labReportName.Text = "Cashup Analysis- For w/s: " & msComputerName
                Dim bIsRevenueAnalysis As Boolean = (LCase(msReportName) = "optreportrevenueanalysis")

                dgvReport.Visible = True
                '= Call mbCashupAnalysis()
                clsCashupPayments1 = New clsCashupPayments(mCnnSql, msSqlDbName, mColSqlDBInfo, mFrmParent1)
                dateStart = DTPickerFrom.Value
                dateEnd = DTPickerTo.Value

                '-- use Cashup code, but with DATE parameters.
                If clsCashupPayments1.cashupAnalysis("", -1, -1, -1, dateStart, dateEnd) Then
                    mColReportLines = clsCashupPayments1.colReportLines
                    '-- show report preview.
                    '--load report info and show--
                    s1 = "<drawline fontstyle=""bold"" />"
                    mColReportLines.Add(s1)

                    If bIsRevenueAnalysis Then
                        '=4233= --  Give option to abort..
                        If (mColReportLines.Count > 500) Then
                            Dim msgResult As MsgBoxResult = _
                                  MsgBox("Please Note-  This Report has " & FormatNumber(mColReportLines.Count, 0) & " lines.." & vbCrLf & _
                                         "Bypass the Preview and print on " & msReportPrinterName & " ??", _
                                                           MsgBoxStyle.YesNoCancel, MsgBoxStyle.DefaultButton1)
                            If (msgResult = MsgBoxResult.Yes) Then
                                bPreviewOnly = False
                            ElseIf (msgResult = MsgBoxResult.Cancel) Then
                                Exit Function   '- no more..
                            Else
                                '- show preview..
                            End If '-bypass preview..
                        End If  '-reportlines-
                        Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS, _
                                          clsCashupPayments1.colReportLines, msReportPrinterName, _
                                         msBusinessName, "POS PAYMENTS Report-", _
                                         Color.DarkBlue, _
                                        "All REVENUE for Period: " & vbCrLf & _
                                            "From: " & Format(dateStart, "ddd dd-MMM-yyyy") & vbCrLf & _
                                        "  To: " & Format(dateEnd, "ddd dd-MMM-yyyy"), _
                                        "(Calendar periods are midnight am to midnight pm.) ", _
                                          "  -Payment- Till -        Description" & Space(48) & _
                                                " Invoice -  Customer-.       Staff  ")
                    Else
                        '--Till-
                        Dim colTillReportLines As Collection = clsCashupPayments1.colTillAnalysisReportLines
                        s1 = "<drawline fontstyle=""bold"" />"
                        colTillReportLines.Add(s1)

                        '=4221=  TILL Analysis now optionally in GRID--
                        If (InStr(LCase(msCurrentNodeName), "grid") > 0) Then
                            '=Show report in Grid..
                            '=4221=  TILL Analysis now in GRID--
                            Dim clsReportToGrid1 As clsReportToGrid
                            Dim colTabStops As Collection
                            '-- These will be converted into Grid Column indexes.
                            colTabStops = New Collection
                            colTabStops.Add(0)
                            colTabStops.Add(130)
                            colTabStops.Add(240)
                            colTabStops.Add(400)
                            colTabStops.Add(500)
                            colTabStops.Add(650)

                            '-- Grid Headings.
                            Dim asHeadings As String() = {"Payment", "Till", "Item", "Amount$", "Invoice", "Staff/Customer"}
                            '--Translate back from Standard Report Markup, and Dump into Grid..
                            clsReportToGrid1 = New clsReportToGrid
                            clsReportToGrid1.DecodeStandardReport(colTillReportLines, colTabStops, asHeadings, dgvReport)

                            '-- Back-shading for alternate payment Groups
                            '--  First row for each Payment will have "Till:"  in the text of second column...
                            Dim bShadingOn As Boolean = True
                            With dgvReport
                                For intRowx As Integer = 0 To .Rows.Count - 1
                                    s1 = LCase(.Rows(intRowx).Cells(1).Value)
                                    If InStr(s1, "till:") > 0 Then
                                        '-start of a paymnt.
                                        '-- flip shading.
                                        If bShadingOn Then
                                            bShadingOn = False
                                        Else  '-was off.
                                            bShadingOn = True
                                        End If
                                    End If
                                    If bShadingOn Then
                                        For Each cell1 As DataGridViewCell In .Rows(intRowx).Cells
                                            cell1.Style.BackColor = Color.WhiteSmoke
                                        Next
                                    End If
                                Next intRowx
                            End With  '-dgvReport.
                            dgvReport.Columns(0).Width = 180   '--expand date column
                            '=4221= END of-  TILL Analysis now in GRID--
                            '=4221= END of-  TILL Analysis now in GRID--
                        Else  '--Preview-
                            '-- No.. do as preview..
                            If (colTillReportLines.Count > 500) Then
                                Dim msgResult As MsgBoxResult = _
                                      MsgBox("Please Note-  This Report has " & _
                                             FormatNumber(colTillReportLines.Count, 0) & " lines.." & vbCrLf & _
                                             "Bypass the Preview and print on " & msReportPrinterName & " ??", _
                                                               MsgBoxStyle.YesNoCancel, MsgBoxStyle.DefaultButton1)
                                If (msgResult = MsgBoxResult.Yes) Then
                                    bPreviewOnly = False
                                ElseIf (msgResult = MsgBoxResult.Cancel) Then
                                    Exit Function   '- no more..
                                Else
                                    '- show preview..
                                End If '-bypass preview..
                            End If  '-reportlines-
                            Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS, _
                                                                          colTillReportLines, msReportPrinterName, _
                                                                         msBusinessName, "POS PAYMENTS Report-", _
                                                                         Color.DarkBlue, _
                                                                        "All TILL Payments for Period: " & vbCrLf & _
                                                                            "From: " & Format(dateStart, "ddd dd-MMM-yyyy") & vbCrLf & _
                                                                        "  To: " & Format(dateEnd, "ddd dd-MMM-yyyy"), _
                                                                        "(Calendar periods are midnight am to midnight pm.) ", _
                                                                          "  -Payment- Till -        Description" & Space(48) & _
                                                                                " Invoice -  Customer-.       Staff  ")
                        End If  '-Preview/Grid.--
                    End If  '-revenue/till-
                    System.Windows.Forms.Application.DoEvents()
                End If '-analysis.
                Exit Function

                '-- Option Invoice simple listing -
            Case "optreportinvoicelisting"
                '-- Option Invoice simple listing -
                '-- Option Invoice simple listing -
                '= labReportName.Text = "Sales Invoice Listing-  For all w/s.."
                labReportName.Text = "Sales Invoices (All w/s) for selected period. "
                labExplain.Text = _
                    "Lists all Sales Invoices & Refunds with payments + Line-Items for selected period."
                'Dim dtInvoices, dtInvoiceLines As DataTable
                'Dim dtPaymentDetails As DataTable  '--to hold cash/chq details of payments.
                'Dim listPaymentIDs As List(Of Integer)
                'Dim sInvoiceId As String

                dateStart = DTPickerFrom.Value
                dateEnd = DTPickerTo.Value

                '--GET all Sales for today..-
                '-- Make a report Line for the docket Listing..
                '--   followed by grid rows for Line Items for that docket.
                '--
                '== -- NEW BUILD 3519.0112  12-Jan-2019=  
                '==     -- Fix Sales Invoice Report to show ALL payments (Disbursements) for invoice.....

                '= sSql = msMakeSalesListSql()
                sWhere = ""  '==  " WHERE (invoice.terminal_id='" & msComputerName & "' )"
                '-- All w/s included for invoice list..-
                sWhere = msReportSetupWhereCondition("invoice.invoice_date", sWhere)

                '==
                '= 4219.1214= = FIX FOR 4219.1130=
                '= 4219.1214= = FIX FOR 4219.1130=
                '==  Add Options to Sales Invoce Listing.
                '==
                If (optInvoicesCash.Checked Or optInvoicesOnAccount.Checked) Then
                    If sWhere <> "" Then
                        sWhere &= " AND "
                    End If
                    If optInvoicesCash.Checked Then
                        sWhere &= " (invoice.isOnAccount=0) "
                    Else  '--on account-
                        sWhere &= " (invoice.isOnAccount<>0) "
                    End If
                End If '-opts-
                '--Discounts only ?=
                If chkInvoicesDiscountedOnly.Checked Then
                    If sWhere <> "" Then
                        sWhere &= " AND "
                    End If
                    sWhere &= " (invoice.discount_nett<>0) "
                End If '-discounts.
                '= end of == 4219.1214= = FIX FOR 4219.1130=


                '==
                '==   Target-New-Build-4282 --  (22-October-2020)
                '==   Target-New-Build-4282 --  (22-October-2020)
                '==
                '==  -- For POS Sales Reports-  Incorporate  Dropdown to select Staff Sales...
                '==
                '-- Select for Staff if op. selected..
                Dim sStaffSelection As String = "- All Staff -"  '-default.

                If (cboStaff.SelectedIndex > 0) Then  '-not "ALL"..
                    '-  get staff id for item.
                    Dim sId As String
                    s1 = Trim(cboStaff.SelectedItem)
                    ix = InStr(s1, ".")
                    If (ix > 1) Then  '-have something.

                        '==   Target-New-Revision-4282.1102 --  (02-November-2020)
                        '== sId = Trim(VB.Left(s1, ix - 1))
                        '== sStaffSelection = Trim(Mid(s1, ix + 1))
                        sId = Trim(Mid(s1, ix + 1))
                        sStaffSelection = Trim(VB.Left(s1, ix - 1))
                        '== END  Target-New-Revision-4282.1102 --  (02-November-2020)

                        If (sWhere <> "") Then
                            sWhere &= " AND "
                        End If
                        sWhere &= " (invoice.staff_id= " & sId & ") "
                    End If  '-ix-
                End If  '-selected index-
                '==  END Target-New-Build-4282 --  (22-October-2020)
                '==  END Target-New-Build-4282 --  (22-October-2020)


                '==
                '==   PREP FOR- Updates to 4221.0207  Started 24-March-2020= 
                '==   PREP FOR- Updates to 4221.0207  Started 24-March-2020= 
                '==   PREP FOR- Updates to 4221.0207  Started 24-March-2020= 
                '==   -- For Sales Invoice Report-  Link to new class for re-write..
                '==
                Dim colGridReport As Collection
                Dim clsSalesReport1 As clsSalesInvoiceReport
                Dim bShowInvoiceLines As Boolean = chkShowInvoiceLines.Checked

                clsSalesReport1 = New clsSalesInvoiceReport(mFrmParent1, mCnnSql, msSqlDbName)

                If clsSalesReport1.GetSalesInvoiceReport(sWhere, bShowInvoiceLines, mColReportLines, colGridReport) Then
                    btnPrintReport.Enabled = True


                    '==  Target-New-Build-4253..
                    '==  Target-New-Build-4253..
                    '- IF Totals only, replace collections with totals.
                    If chkSalesSummaryOnly.Checked Then
                        Call clsSalesReport1.GetSalesInvoiceTotalsReport(mColReportLines, colGridReport)
                    End If


                    If (LCase(msCurrentNodeName) = "invoices_preview") Then
                        '-- show report preview.
                        '--load report info and show--
                        s1 = "<drawline fontstyle=""bold"" />"
                        mColReportLines.Add(s1)
                        '= bPreviewOnly = False     '- force printing.
                        '-- Confirm preview..
                        If (mColReportLines.Count > 500) Then
                            Dim msgResult As MsgBoxResult = _
                                  MsgBox("Please Note-  This Report has " & _
                                         FormatNumber(mColReportLines.Count, 0) & " lines.." & vbCrLf & _
                                         "Bypass the Preview and print on " & msReportPrinterName & " ??", _
                                                           MsgBoxStyle.YesNoCancel, MsgBoxStyle.DefaultButton1)
                            If (msgResult = MsgBoxResult.Yes) Then
                                bPreviewOnly = False
                            ElseIf (msgResult = MsgBoxResult.Cancel) Then
                                Exit Function   '- no more..
                            Else
                                '- show preview..
                                bPreviewOnly = True
                            End If '-bypass preview..
                        End If  '-reportlines-
                        Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS,
                                                                 mColReportLines, msReportPrinterName,
                                                                 msBusinessName, "POS Sales Invoices- " & sStaffSelection & "..",
                                                                 Color.DarkBlue,
                                                                "Sales Invoices for Period: " & vbCrLf &
                                                                   "From: " & Format(dateStart, "ddd dd-MMM-yyyy") & vbCrLf &
                                                                 "    To:   " & Format(dateEnd, "ddd dd-MMM-yyyy"),
                                                                 "(Calendar periods are midnight am to midnight pm.) ",
                                     "  - Invoice -  Date/Till-   Customer   " &
                                     Space(30) & "Item Description    [Barcode]                        Quantity          Amount           -Staff-")
                        Exit Function
                    End If  '-preview-

                    '-- Send REport to Grid..

                    '-- now Dump into GRID ..
                    Dim intGridRowX As Integer = -1
                    Dim cellAny As DataGridViewTextBoxCell
                    Dim intCellNo As Integer

                    If (colGridReport Is Nothing) OrElse (colGridReport.Count <= 0) Then
                        Exit Function
                    End If
                    dgvReport.Rows.Clear()
                    dgvReport.Columns.Clear()

                    ''-- Grid Headings.
                    '=Dim asHeadings As String() = {"Trancode", "InvoiceNo", "Date", "Customer", "Invoice-total", "Payment", "Staff"}

                    '==    Updates to 4233.0421  Started 24-April-2020= 
                    '==
                    '==  Target is new Build 4234..
                    '==  Target is new Build 4234..
                    '-- Grid Headings.
                    Dim asHeadings As String() = {"Trancode", "InvoiceNo", "Date", _
                                                 "Customer", "Invoice-total", "Nett Profit", "Rate", "Payment", "Staff"}
                    '== END OF-  PREP FOR- Updates to 4233.0421  Started 24-April-2020= 


                    '-create cols..
                    cellAny = New DataGridViewTextBoxCell
                    cellAny.Style.BackColor = Color.White
                    '= Dim column1 As DataGridViewColumn

                    '- make columns-
                    Try
                        For Each sHeading As String In asHeadings
                            column1 = New DataGridViewColumn
                            s1 = sHeading
                            '= dgvReport.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight  '--cost
                            With column1
                                .HeaderText = s1
                                .Name = s1
                            End With
                            column1.CellTemplate = cellAny
                            'If (InStr(LCase(s1), "invoiceno") > 0) Then
                            '    column1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                            '    column1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '--amount.
                            'End If
                            dgvReport.Columns.Add(column1)
                        Next sHeading
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Exit Function
                    End Try
                    '-- invoice no and total..
                    dgvReport.Columns(1).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvReport.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvReport.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvReport.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    '--widths.
                    'dgvReport.Columns(2).Width = 180   '--expand date column
                    'dgvReport.Columns(3).Width = 220   '--expand cust-name column
                    'dgvReport.Columns(4).Width = 100   '-- totals column
                    'dgvReport.Columns(5).Width = 240   '--expand Payments column

                    '==  Updates to 4233.0421  Started 24-April-2020= 
                    '==
                    '==  Target is new Build 4234..
                    '==  Target is new Build 4234..

                    dgvReport.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvReport.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvReport.Columns(6).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvReport.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    '--widths.
                    dgvReport.Columns(2).Width = 180   '--expand date column
                    dgvReport.Columns(3).Width = 220   '--expand cust-name column
                    dgvReport.Columns(4).Width = 100   '-- totals column
                    dgvReport.Columns(7).Width = 240   '--expand Payments column
                    '== END OF-  PREP FOR- Updates to 4233.0421  Started 24-April-2020= 
                    '== END OF-  PREP FOR- Updates to 4233.0421  Started 24-April-2020= 

                    '- Add rows and data..

                    Call mWaitFormOn("Wait..  Loading dataGrid..")
                    For Each colInvoiceLine As Collection In colGridReport
                        gridRow1 = New DataGridViewRow
                        dgvReport.Rows.Add(gridRow1)
                        intGridRowX = dgvReport.Rows.Count - 1  '--last row -
                        With dgvReport.Rows(intGridRowX)
                            intCellNo = 0
                            For Each sItem As String In colInvoiceLine
                                .Cells(intCellNo).Value = sItem
                                intCellNo += 1
                            Next
                            '- number the rows..
                            .HeaderCell.Value = Trim(CStr(intGridRowX + 1))
                            mFormWait1.labMsg.Text = "Wait..  Loading dataGrid- " & CStr(intGridRowX) & ".."
                            DoEvents()
                            '-- .Cells(k_CUSTGRIDCOL_CUSTNO).Value = row1.Item("barcode")
                        End With
                    Next colInvoiceLine

                    '= END -- TESTING TILL in GRID--

                    '= End If  '--get datatable-
                    sSql = ""  '--all done-
                    '-- E n d  of Dockets/Lines report.-
                    Call mWaitFormOff()
                    Exit Function
                    '-- NEXT= CUSTOMER Sales -
                Else  ''-failed
                    Exit Function
                End If  '-get report-


                '==  END of  PREP FOR- Updates to 4221.0207  Started 24-March-2020= 
                '==  END of    PREP FOR- Updates to 4221.0207  Started 24-March-2020= 

                '-- THE REST of Sales Report stuff IS NOW REDUNDANT..
                '-- THE REST of Sales Report stuff IS NOW REDUNDANT..
                '-- THE REST of Sales Report stuff IS NOW REDUNDANT..


                '-- NEXT= CUSTOMER Sales -

            Case "optreportcustomersales"
                '-- CUSTOMER sales= 3307.0215=
                '-- CUSTOMER sales= 3307.0215=
                '-  define tab columns- (ofsets from our marg)-
                Const k_TAB_CUST_CUST As Integer = 0
                Const k_TAB_CUST_CAT1 As Integer = 25
                '= Const k_TAB_CUST_CAT2 As Integer = 120
                Const k_TAB_CUST_DESCR As Integer = 80
                Const k_TAB_CUST_TYPE As Integer = 300   '-- transaction.
                Const k_TAB_CUST_DATE As Integer = 350  '-invoice No. and date-
                Const k_TAB_CUST_QTY As Integer = 520   '-- transaction.
                Const k_TAB_CUST_AMOUNT As Integer = 560
                Const k_WIDTH_CUST_QTY As Short = 30
                Const k_WIDTH_CUST_AMT As Short = 80
                '= Const k_TAB_TRANCODE As Integer = 420
                Const k_TAB_CUST_STAFF As Integer = 660

                Dim dtInvoiceLines As DataTable
                Dim sCustomer, sLastCustomer As String
                Dim intStock_id As Integer = -1
                Dim sLine, sCat1, sCat2, sDescr, sDate, sType, sQty, sAmount, sStaff As String
                Dim decSalesInc, decCustTotal As Decimal
                Dim decFinalTotal As Decimal = 0

                '-- LIST and Total all sales-lines by CUSTOMER/ product.
                '-- Then compute Customer and a Grand total.
                dateStart = DTPickerFrom.Value
                dateEnd = DTPickerTo.Value
                sWhere = ""
                '-- make Date condition..
                sWhere = msReportSetupWhereCondition("invoice.invoice_date", sWhere)
                '-- setup sql-
                '= labReportName.Text = "Report: Product Sales"
                '= labExplain.Text = "Shows all Items Sold by Product for the selected Period."
                If mIntLookupCustomer_id > 0 Then
                    If (sWhere <> "") Then
                        sWhere &= " AND "
                    End If
                    sWhere &= " (invoice.customer_id =" & CStr(mIntLookupCustomer_id) & ")"
                End If
                sSql = "SELECT  invoice.transactionType, invoice.invoice_id, invoice.invoice_date, "
                sSql &= "  stock.cat1  AS Category1, stock.cat2 AS Category2,  "
                sSql &= "  stock.description,  stock.barcode,  "
                sSql &= "  IL.stock_id,  "
                sSql &= "  Qty= (CASE WHEN (invoice.transactionType ='Refund') "
                sSql &= "             THEN (-CONVERT(INT,IL.quantity)) "
                sSql &= "              ELSE CONVERT(INT,IL.quantity) END), "
                sSql &= "  Sales_inc= (CASE WHEN (invoice.transactionType ='Refund') "
                sSql &= "           THEN (-IL.total_inc) "
                sSql &= "           ELSE IL.total_inc END), "
                sSql &= "  customerName = CASE companyName  "
                sSql &= "     WHEN '' THEN (customer.lastname + '.' + customer.firstname)"
                sSql &= "     WHEN 'n/a' THEN (customer.lastname + '.' + customer.firstname)"
                sSql &= "     ELSE companyName "
                sSql &= " END + '.[' + customer.barcode + ']', "  '--case- Include barcode in Name.
                sSql &= "  staff.docket_name "
                sSql &= "  FROM dbo.invoiceLine AS IL "
                sSql &= "   LEFT OUTER JOIN invoice on (IL.invoice_id=invoice.invoice_id) "
                sSql &= "   LEFT OUTER JOIN stock on (IL.stock_id=stock.stock_id) "
                sSql &= "   LEFT OUTER JOIN customer on (invoice.customer_id=customer.customer_id) "
                sSql &= "   LEFT OUTER JOIN staff on (invoice.staff_id=staff.staff_id) "
                sSql &= sWhere
                '==sSql &= " GROUP BY IL.stock_id, invoice.transactionType "
                '== sSql &= " WITH ROLLUP "
                '==sSql &= " ORDER BY description;"
                sSql &= " ORDER BY customerName, Category1, Category2, stock.description, IL.stock_id;"

                '- get Query result and load datagrid..-
                If Not gbGetDataTable(mCnnSql, dtInvoiceLines, sSql) Then
                    MsgBox("Error in SELECT for InvoiceLine table: " & vbCrLf & _
                                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    Exit Function
                End If  '--get-

                '--get ok-
                If ((dtInvoiceLines Is Nothing)) OrElse (dtInvoiceLines.Rows.Count <= 0) Then
                    MsgBox("No Sales found.. " & vbCrLf & _
                                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    Exit Function
                End If
                '--  RE- Grid..
                '== DON'T USE GRID- 
                '-- Use our Report printer Class-
                mColReportLines = New Collection
                sCustomer = ""
                sLastCustomer = ""

                For Each drInvoiceLine As DataRow In dtInvoiceLines.Rows

                    sCustomer = Trim(drInvoiceLine.Item("customerName"))
                    If (LCase(sCustomer) <> LCase(sLastCustomer)) Then
                        '-new or first customer-
                        If (sLastCustomer = "") Then  '- previous-
                            '-- First trans. of report-

                        Else '--just passed a completed customer-
                            '- print cust totals..
                            '= "Totals for Customer: " & sLastCustomer
                            sAmount = FormatCurrency(decCustTotal, 2)
                            sLine = "<textline>"
                            sLine &= "<txt TAB=""" & k_TAB_CUST_CUST & """  >Total for Cust:" & sLastCustomer & "</txt>"
                            sLine &= "<txt TAB=""" & k_TAB_CUST_AMOUNT & """  align=""right""  fontstyle=""bold""  "
                            sLine &= " width = """ & k_WIDTH_CUST_AMT & """  >" & sAmount & "</txt>"
                            sLine &= "</textline>"
                            mColReportLines.Add(sLine)

                            '= mColReportLines.Add(sLine)
                            s1 = "<drawline fontstyle=""bold"" />"
                            mColReportLines.Add(s1)
                            '-- done with last cust.-

                        End If  '-last cust-
                        '--new  customer-
                        sLine = "<textline>"
                        sLine &= "<txt TAB=""" & k_TAB_CUST_CUST & """  >" & sCustomer & "</txt>"
                        sLine &= "</textline>"
                        mColReportLines.Add(sLine)

                        sLastCustomer = sCustomer
                        '- reset totals.
                        decCustTotal = 0
                    End If  '--different customer-

                    '-- get details this transaction.
                    sDate = " Inv #" & RSet(drInvoiceLine.Item("invoice_id"), 6)
                    sDate &= ".  " & Format(CDate(drInvoiceLine.Item("invoice_date")), "dd-MMM-yyyy")
                    sType = drInvoiceLine.Item("transactionType") '-invoice.transactionType-
                    sCat1 = drInvoiceLine.Item("category1")
                    sCat2 = drInvoiceLine.Item("category2")
                    sDescr = VB.Left(drInvoiceLine.Item("description"), 32)
                    '= sType = drInvoiceLine.Item("transactionType")
                    sStaff = drInvoiceLine.Item("docket_name")
                    decSalesInc = CDec(drInvoiceLine.Item("sales_inc"))
                    decCustTotal += decSalesInc
                    decFinalTotal += decSalesInc
                    sQty = drInvoiceLine.Item("Qty")
                    sAmount = FormatCurrency(decSalesInc, 2)
                    '-- format transaction-
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_CUST_CAT1 & """  >" & sCat1 & "</txt>"
                    '= sLine &= "<txt TAB=""" & k_TAB_CUST_CAT2 & """  >" & sCat2 & "</txt>"
                    sLine &= "<txt TAB=""" & k_TAB_CUST_DESCR & """  >" & sDescr & "</txt>"
                    sLine &= "<txt TAB=""" & k_TAB_CUST_DATE & """  >" & sDate & "</txt>"
                    sLine &= "<txt TAB=""" & k_TAB_CUST_TYPE & """  >" & sType & "</txt>"
                    '=sLine &= "<txt TAB=""" & k_TAB_CUST_QTY & """  >" & sQty & "</txt>"
                    sLine &= "<txt TAB=""" & k_TAB_CUST_QTY & """  align=""right""  "
                    sLine &= " width = """ & k_WIDTH_CUST_QTY & """  >" & sQty & "</txt>"

                    sLine &= "<txt TAB=""" & k_TAB_CUST_AMOUNT & """  align=""right""  "
                    sLine &= " width = """ & k_WIDTH_CUST_AMT & """  >" & sAmount & "</txt>"
                    sLine &= "<txt TAB=""" & k_TAB_CUST_STAFF & """  >" & sStaff & "</txt>"
                    '- finish the line-
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                Next drInvoiceLine
                '-------- all rows done-----------

                '- Finish Last Customer totals..
                sAmount = FormatCurrency(decCustTotal, 2)
                sLine = "<textline>"
                sLine &= "<txt TAB=""" & k_TAB_CUST_CUST & """  >Total for Cust:" & sCustomer & "</txt>"
                sLine &= "<txt TAB=""" & k_TAB_CUST_AMOUNT & """  align=""right""  fontstyle=""bold""  "
                sLine &= " width = """ & k_WIDTH_CUST_AMT & """  >" & sAmount & "</txt>"
                sLine &= "</textline>"
                mColReportLines.Add(sLine)
                s1 = "<drawline fontstyle=""bold"" />"
                mColReportLines.Add(s1)
                s1 = "<drawline/>"
                mColReportLines.Add(s1)

                '-- Final total..
                sAmount = FormatCurrency(decFinalTotal, 2)
                sLine = "<textline>"
                sLine &= "<txt TAB=""" & k_TAB_CUST_CUST & """  >Final Total (all Customers):</txt>"
                sLine &= "<txt TAB=""" & k_TAB_CUST_AMOUNT & """  align=""right""  fontstyle=""bold""  "
                sLine &= " width = """ & k_WIDTH_CUST_AMT & """  >" & sAmount & "</txt>"
                sLine &= "</textline>"
                mColReportLines.Add(sLine)

                s1 = "<drawline fontstyle=""bold"" />"
                mColReportLines.Add(s1)
                bPreviewOnly = True
                '-- Confirm preview..
                If (mColReportLines.Count > 500) Then
                    Dim msgResult As MsgBoxResult = _
                          MsgBox("Please Note-  This Report has " & _
                                 FormatNumber(mColReportLines.Count, 0) & " lines.." & vbCrLf & _
                                 "Bypass the Preview and print on " & msReportPrinterName & " ??", _
                                                   MsgBoxStyle.YesNoCancel, MsgBoxStyle.DefaultButton1)
                    If (msgResult = MsgBoxResult.Yes) Then
                        bPreviewOnly = False
                    ElseIf (msgResult = MsgBoxResult.Cancel) Then
                        Exit Function   '- no more..
                    Else
                        '- show preview..
                        bPreviewOnly = True
                    End If '-bypass preview..
                End If  '-reportlines-
                Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS, _
                                                          mColReportLines, msReportPrinterName, _
                                                          msBusinessName, "CUSTOMER SALES History Report-", _
                                                           Color.DarkGreen, _
                                                           "Customer Sales Invoices for Period: " & vbCrLf & _
                                                                  "From: " & Format(dateStart, "ddd dd-MMM-yyyy") & vbCrLf & _
                                                                   "    To:   " & Format(dateEnd, "ddd dd-MMM-yyyy"), _
                                                            " By Product Line.. ", _
                                                            "  Customer  Description" & Space(42) & _
                                                                " Invoice " & Space(54) & " Qty        Amount-       Staff  ")
                '== END Customer Sales=
                '== END Customer Sales=
                '== END Customer Sales=

            Case "optreportproductsales"
                '-- Summarise sales by product.
                '-- Summarise sales by product.
                '-- Summarise sales by product.
                '-- Then compute a Grand total.

                '==
                '==   Updated.- 3519.0127 27-Jan-2019= 
                '==     -- Fixes to Stock Sales Report for Stewart-
                '==           '  ie Select Category to Report, and report on Average Sell Price..
                '==

                '==   Updated.- 3519.0404  Started 30-March-2019= 
                '==    -- Reports-  Product Sales Fix- Must Total TotalCost_ex column (NOT cost_ex.)
                '==                 ALSO DON'T use PrintDataGridView Function- 
                '==                 INSTEAD, Convert to user Standard clsReportPrinter.

                mColReportLines = New Collection  '- For Report printer..

                dateStart = DTPickerFrom.Value
                dateEnd = DTPickerTo.Value

                sWhere = ""
                '-- make Date condition..
                sWhere = msReportSetupWhereCondition("invoice.invoice_date", sWhere)

                '-- Add category if selected.
                s1 = cboSelectCat1.SelectedItem
                If (LCase(s1) <> "-all-") And (LCase(s1) <> "<n/a>") Then
                    sWhere &= " AND (cat1='" & s1 & "') "
                    '=4201.0613-
                    '-- Add Cat2 if seleceted..
                    s2 = cboSelectCat2.SelectedItem
                    If (LCase(s2) <> "-all-") And (LCase(s2) <> "<n/a>") Then
                        sWhere &= " AND (cat2='" & s2 & "') "
                    End If
                End If  '-cat1-

                '=3519.0303=
                '-- -- Add Taxable/non-taxable Options..
                If optProductsNonTaxable.Checked Or optProductsTaxable.Checked Then
                    '-- selection needed
                    If sWhere <> "" Then
                        sWhere &= " AND "
                    End If
                    If optProductsNonTaxable.Checked Then
                        sWhere &= "(IL.sales_taxCode<>'GST')"
                    Else  '-wants taxable only.
                        sWhere &= "(IL.sales_taxCode='GST')"
                    End If  '--nonTaxablr.
                End If  '-tax/nonTax-

                '== Target 6201- Updating 12-July-2021..
                '==    Updates For Target OpenSource version Build 6201...
                '==  Add features to Product Sales and What's In Stock to filter for a particular stock Supplier..
                '= = = = =

                '-- Select for Supplier if op. selected..
                Dim sSupplierSelection As String = "- All Suppliers -"  '-default.

                If (cboSupplierSales.SelectedIndex > 0) Then  '-not "ALL"..
                    '-  get staff id for item.
                    Dim sId As String
                    s1 = Trim(cboSupplierSales.SelectedItem)
                    ix = InStr(s1, ".")
                    If (ix > 1) Then  '-have something.
                        sId = Trim(Mid(s1, ix + 1))
                        sSupplierSelection = Trim(VB.Left(s1, ix - 1))
                        If (sWhere <> "") Then
                            sWhere &= " AND "
                        End If
                        sWhere &= " (stock.supplier_id= " & sId & ") "
                    End If  '-ix-
                End If  '-selected index-
                '== END Target 6201- Updating 12-July-2021..
                '==    Updates For Target OpenSource version Build 6201...
                '==  Add features to Product Sales and What's In Stock to filter for a particular stock Supplier..
                '= = = = =

                '-- setup sql-
                '== Target 6201- Updating 12-July-2021..
                labReportName.Text = "Product Sales: " & sSupplierSelection
                '== END Target 6201- Updating 12-July-2021..
                labExplain.Text = "Shows all Items Sold by Product/Category for the selected Period." &
                                     "  (You can Select specific Category if needed.)"

                '==   Updated.- 3519.0127 27-Jan-2019= 
                '--  First get all Invoice lines for the period..
                '--  We need to do this so we can make Qty negative for Refunds..
                '--  Otherwise the GROUP BY gets too complicated..

                ' --  PART-I  Select required Invoice Lines into TEMP table, 
                '--    with Transaction type joined..  
                '= use ClearwaterJT_jmpos

                '-- DROP temp table..-
                sSql = "IF OBJECT_ID('tempdb.dbo.#StockSalesReportLines', 'U') IS NOT NULL "
                sSql &= "    DROP TABLE #StockSalesReportLines; "
                '= GO()
                If Not mbExecuteSql(mCnnSql, sSql, False, Nothing) Then
                    MsgBox("Error in dropping Temporary Table.")
                End If

                '--  First get all Invoice lines for the period..
                sSql = "SELECT  stock.barcode, stock.cat1,    stock.cat2,  "
                sSql &= "  stock.description,  invoice.transactionType,  IL.stock_id, "
                sSql &= "    IL.cost_ex,"
                sSql &= "    IL.sales_taxCode,  "
                sSql &= "    IL.sellActual_ex, "
                sSql &= "    IL.sellActual_Tax, "
                sSql &= "    IL.sellActual_inc, "
                sSql &= "    (CASE WHEN (transactionType ='Refund') "
                sSql &= "              THEN -IL.quantity "
                sSql &= "              ELSE IL.quantity END) AS Qty"
                '-- ==  CHECK for ZERO total_ex !!
                '-   -- gp Rate-- DONE in PART II..
                '-- 
                sSql &= "    INTO #StockSalesReportLines           "
                '-- '-from-
                sSql &= "    FROM dbo.invoiceLine AS IL "
                sSql &= "     INNER JOIN invoice  on (IL.invoice_id=invoice.invoice_id) "
                sSql &= "     INNER JOIN stock on (IL.stock_id=stock.stock_id) "
                sSql &= sWhere
                '-- '== sSql &= " WITH ROLLUP "
                '--  '==sSql &= " ORDER BY description;"
                sSql &= "  ORDER BY  IL.stock_id;"
                '-- test
                'GO()
                '- get Query result int TEMP Table..-
                If Not mbExecuteSql(mCnnSql, sSql, False, Nothing) Then
                    MsgBox("Error in Creating Temporary Stock Sales Table.")
                End If

                '--INTO was  ok-
                '--  THE end of part I--
                '-- select * from #StockSalesReportLines;

                '--NOW- Just Summarise all selected Invoice LINES by stock_id..-
                '-- Uses GROUP BY clause to summarise sales by product (stock)..
                '-  http://msdn.microsoft.com/en-au/library/ms177673.aspx  

                sSql = "SELECT  " '=  invoice.transactionType AS tr_type, "
                sSql &= "  MIN(cat1) AS Cat1, MIN(cat2) AS Cat2,  MIN(description) AS Description,  "
                sSql &= "  MIN(barcode) AS Barcode,  "
                sSql &= "  stock_id, "
                sSql &= "   MIN (cost_ex) as cost_ex,"
                sSql &= "   MIN (sales_taxCode) as sales_taxCode,"
                sSql &= "   SUM (Qty) as Qty,"
                sSql &= "    SUM (cost_ex *qty) as totalCost_ex, "

                '--  THiS Averaage need to be RE-computed in the VB code
                '--   ie working over the Query results.-
                '--  As the query AVG does not tale into account the Line Quantity..
                sSql &= "   AVG (SellActual_inc) as SellActual_avg, "
                '-- 
                sSql &= "  SUM (SellActual_ex *qty) as totalActual_ex, "
                '-- SUM (grossprofit) as grossProfit,
                '-- gross_profit is :  (IL.sellActual_ex -IL.cost_ex)*IL.quantity )
                sSql &= "  SUM ((sellActual_ex -cost_ex)*qty ) AS gross_profit,"
                '- Sales_inc
                sSql &= "  SUM (sellActual_inc *qty) as totalActual_inc, "
                '-- gp Rate--
                '-- gross_profit is :  (IL.sellActual_ex -IL.cost_ex)*IL.quantity )
                '==  CHECK for ZERO total_ex !!
                sSql &= " CASE WHEN (SUM((SellActual_ex *qty))=0) THEN 0 "
                sSql &= "  ELSE SUM((sellActual_ex -cost_ex)*Qty )/SUM((SellActual_ex *qty)) *100 "
                sSql &= " END "
                sSql &= " AS gpRate "
                '-from-
                sSql &= "   FROM #StockSalesReportLines"
                '== sSql &= sWhere
                sSql &= " GROUP BY stock_id "
                '== sSql &= " WITH ROLLUP "
                sSql &= " ORDER BY Cat1, Cat2, Description, stock_id;"

                '- get Query result and load datagrid..-
                If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
                    MsgBox("Error in SELECT from TEMP InvoiceLine table: " & vbCrLf & _
                                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    Exit Function
                End If  '--get-

                '--get ok-
                If ((datatable1 Is Nothing)) OrElse (datatable1.Rows.Count <= 0) Then
                    MsgBox("No Sales found.. " & vbCrLf & _
                                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    Exit Function
                End If

                '-- DUMP the datatable into the Grid..
                '-- DUMP the datatable into the Grid..
                '-- DUMP the datatable into the Grid..
                '-- DUMP the datatable into the Grid..

                '== DON'T USE BOUND GRID- dgvReport.DataSource = datatable1

                '- WE use UNBOUND GRID..-
                '-- Transfer datatable to grid.
                If Not mbDumpTableToGrid(datatable1, dgvReport) Then
                    Exit Function
                End If '-dump-
                dgvReport.Columns(0).Width = 70   '--shrink cat1 column
                dgvReport.Columns(1).Width = 70   '--cat2
                dgvReport.Columns(2).Width = 140  '--widen description column
                dgvReport.Columns(3).Width = 80    '--barcode column
                dgvReport.Columns(4).Width = 40    '--Stock_id column
                dgvReport.Columns(4).Visible = False  '- HIDE Stock_id column
                dgvReport.Columns(5).Width = 70    '--Cost column
                dgvReport.Columns(6).Width = 40    '--taxCode
                dgvReport.Columns(6).Visible = False   '-HIDE-taxCode
                dgvReport.Columns(7).Width = 40    '--Qty column
                dgvReport.Columns(8).Width = 70    '--TotalCost_ex column
                '=3519.0404= dgvReport.Columns(8).Visible = False   '-HIDE-TotalCost_ex column
                dgvReport.Columns(9).Width = 90    '--Sell Actual_AVG
                dgvReport.Columns(10).Width = 90    '--total actual_ex column
                dgvReport.Columns(11).Width = 70    '--gross profit column
                dgvReport.Columns(12).Width = 90    '--total actual_inc column
                dgvReport.Columns(13).Width = 60    '--gp rate column

                '--  THE end of part II--

                ' -- Part III.. vb.net
                ' --   get distinct Stock_id's from last Query.. 
                ' --    (there is one row per stock Id..) 
                ' -- Search TEMP TABLE #StockSalesReportLines (Stock_id by Stock_id)..
                ' --  ie For each stock id get line sales 
                '-    and compute average sell price over all lines and quantities.
                '==   The  plug result into AVG price column of GRID.

                '-FIRST, get a reduced data table of the selected invoice ines from the TEMP tab..
                Dim dtSellPrices As DataTable

                sSql = "SELECT  " '=  invoice.transactionType AS tr_type, "
                sSql &= " stock_id, Qty, sellActual_inc "
                sSql &= "   FROM #StockSalesReportLines ORDER by stock_id "
                '- get Query result and load datagrid..-
                If Not gbGetDataTable(mCnnSql, dtSellPrices, sSql) Then
                    MsgBox("Error in SELECT Prices from TEMP InvoiceLine table: " & vbCrLf & _
                                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    Exit Function
                End If  '--get-
                '--get ok-
                If ((dtSellPrices Is Nothing)) OrElse (dtSellPrices.Rows.Count <= 0) Then
                    MsgBox("No Sales found in temp table... " & vbCrLf & _
                                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                    '-- nothing.  
                Else  '-ok.
                    '--have line rows..
                    Dim decTotalPrices, decAveragePrice As Decimal
                    Dim intTotalQty, intStockID, rowThis As Integer
                    '-- For each report row, find all the Inv. lines, 
                    '- and compute average price for that stick-id.
                    '--  Report datatable1 must match Grid row for row...-
                    rowThis = 0
                    For Each rowReport As DataRow In datatable1.Rows
                        decTotalPrices = 0
                        intTotalQty = 0
                        intStockID = rowReport.Item("stock_id")
                        For Each rowSaleLine As DataRow In dtSellPrices.Rows
                            If rowSaleLine.Item("stock_id") = intStockID Then
                                decTotalPrices += CDec(rowSaleLine.Item("sellActual_inc")) * CInt(rowSaleLine.Item("Qty"))
                                intTotalQty += CInt(rowSaleLine.Item("Qty"))  '- to compute final average.
                            End If
                        Next rowSaleLine
                        If (intTotalQty > 0) Then
                            decAveragePrice = decTotalPrices / intTotalQty
                            '-Replace old price grid.
                            '-- format $ Amount-
                            dgvReport.Rows(rowThis).Cells(9).Value = FormatCurrency(decAveragePrice, 2)
                        End If
                        rowThis += 1
                    Next rowReport
                End If  '-nothing.
                '-  ok..  done with the average sold price.

                '-- fix Hdr alignments.-
                '=dgvReport.Columns(4).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight '-stock_id
                dgvReport.Columns(5).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight  '--cost
                dgvReport.Columns(7).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight '-qty- 
                dgvReport.Columns(8).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight '-total Cost- 
                dgvReport.Columns(9).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight  '--sell actual AVG-
                dgvReport.Columns(10).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight  '-total actual-ex-
                dgvReport.Columns(11).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight  '--gpt
                dgvReport.Columns(12).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight  '-total-actual-inc-
                dgvReport.Columns(13).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight  '-gpRate-

                '-- fix content alignments.-
                '=dgvReport.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight '-stock_id
                dgvReport.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '--cost
                dgvReport.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight '-qty-
                dgvReport.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight '-total Cost-
                dgvReport.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '-Sell-actual-AVG
                dgvReport.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '-total actual-ex-
                dgvReport.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '-gp-
                dgvReport.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '-total sales-inc-
                dgvReport.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '-gprate-

                '- Grid should now have fourteen columns-
                '-  Stock_id, Description, Qty and $-value.
                '--  Compute overall total sales.

                ' -- Part IV.. vb.net

                '--  COMPUTE overall totals in vb.net..   
                '--  Dump into DataGrid.. 

                '= decTotalSales = 0
                decTotalCost_ex = 0
                decTotalSales_ex = 0
                decTotalSales_inc = 0
                decTotalProfit = 0

                Dim decAmount As Decimal = 0
                rowx = 0
                For Each row1 As DataRow In datatable1.Rows
                    '-cost_ex-
                    '=3519.0404= 
                    decAmount = CDec(row1.Item("cost_ex"))
                    dgvReport.Rows(rowx).Cells(5).Value = FormatNumber(decAmount, 2)
                    '-totalCost_ex-
                    decAmount = CDec(row1.Item("totalCost_ex"))
                    decTotalCost_ex += decAmount
                    '-- reformat $ Amount-
                    dgvReport.Rows(rowx).Cells(8).Value = FormatNumber(decAmount, 2)

                    '- reformat Qty if whole numbet-
                    decAmount = CDec(row1.Item("Qty"))
                    If decAmount = Math.Truncate(decAmount) Then  '- is integer-
                        dgvReport.Rows(rowx).Cells(7).Value = CStr(CInt(decAmount))
                    End If

                    '- SalesActual_ex -
                    decAmount = CDec(row1.Item("totalActual_ex"))
                    decTotalSales_ex += decAmount
                    '-- reformat $ Amount-
                    dgvReport.Rows(rowx).Cells(10).Value = FormatNumber(decAmount, 2)

                    '-profit-
                    decAmount = CDec(row1.Item("gross_profit"))
                    decTotalProfit += decAmount
                    '-- reformat $ Amount-
                    dgvReport.Rows(rowx).Cells(11).Value = FormatNumber(decAmount, 2)

                    '- sales_inc-
                    decAmount = CDec(row1.Item("totalActual_inc"))
                    decTotalSales_inc += decAmount
                    '-- reformat $ Amount-
                    dgvReport.Rows(rowx).Cells(12).Value = FormatNumber(decAmount, 2)
                    '-profit Rate-
                    decAmount = CDec(row1.Item("gpRate"))
                    '-- reformat %..-
                    dgvReport.Rows(rowx).Cells(13).Value = Format(decAmount, "  0.00") & "%"
                    rowx += 1
                Next row1  '-datatable-
                '-- make row for overall total.
                Try
                    '- Add a new row for grand total.
                    gridRow1 = New DataGridViewRow  '--prepare datagrid report row..
                    dgvReport.Rows.Add(gridRow1)
                    dgvReport.Rows(rowx).Cells(2).Value = "-- Total Sales/Profit- "
                    dgvReport.Rows(rowx).Cells(8).Value = FormatCurrency(decTotalCost_ex, 2)
                    dgvReport.Rows(rowx).Cells(10).Value = FormatCurrency(decTotalSales_ex, 2)
                    dgvReport.Rows(rowx).Cells(11).Value = FormatCurrency(decTotalProfit, 2)
                    dgvReport.Rows(rowx).Cells(12).Value = FormatCurrency(decTotalSales_inc, 2)

                    '--  Add overall profit..
                    Dim decOverallProfitRate As Decimal
                    decOverallProfitRate = (decTotalProfit / decTotalSales_ex) * 100
                    dgvReport.Rows(rowx).Cells(13).Value = FormatNumber(decOverallProfitRate, 2) & "%"
                Catch ex As Exception
                    MsgBox("Error updating report datagrid.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                End Try
                DoEvents()
                btnPrintReport.Enabled = True
                '= End If  '--nothing-

                '=3519.0404..
                '--   Build Report Lines to print Grid via Report printer..
                '--   Build Report Lines to print Grid via Report printer..
                Const k_FIRST_AMT_GRIDCOL As Integer = 3  '--grid col index of 1st col after Descr ( Barcode ).

                Dim colColumnLefts As New Collection
                Dim colColumnWidths As New Collection

                Dim intTmpWidth, intColumnCharWidth As Integer
                '=Dim intLeftPos As Integer = 0
                Dim intTotalGridWidth As Integer = 0
                Dim intPrintWidth As Integer = 760
                '-- TWO column Header Lines.  Each Grid row will produce TWO report Lines.
                Dim sDataLine As String
                Dim idx As Integer

                msReportColumnHdr1 = "Cat1  Cat2   Description ----   "
                msReportColumnHdr2 = ""

                intTotalGridWidth = 0
                For Each dgvGridCol1 As DataGridViewColumn In dgvReport.Columns
                    intTotalGridWidth += dgvGridCol1.Width
                Next
                Dim intLeftPos As Integer = 0 '=16  '= intLeftMargin + intRowHdrWidth

                '--  Get column widths and positions..---
                For Each GridCol1 As DataGridViewColumn In dgvReport.Columns
                    '=intTmpWidth = CInt(Math.Floor(GridCol1.Width / mIntTotalWidth * mIntTotalWidth * (ev.MarginBounds.Width / mIntTotalWidth)))
                    intTmpWidth = _
                        CInt(Math.Floor(GridCol1.Width / intTotalGridWidth * intTotalGridWidth * (intPrintWidth / intTotalGridWidth)))
                    '-- Save width and height of headers
                    colColumnLefts.Add(intLeftPos)
                    colColumnWidths.Add(intTmpWidth)
                    intLeftPos += intTmpWidth
                Next GridCol1

                '- get Data line widths and lefts..
                intLeftPos = 16 '-start again at the left..

                '-- Build width coll.  manually..
                '-- Barcode, stock_id, cost_ex, sTaxcode, Qty, totalCost_ex, 
                '--           sellActualAvg, totalActual_ex, gross_profit, totalActual_inc, gp_rate
                Dim aIntWidths() As Integer = {120, 30, 70, 30, 40, 70, 70, 80, 70, 80, 60}
                'For Each intTmpWidth In aIntWidths
                '    colDataColumnLefts.Add(intLeftPos)
                '    colDataColumnWidths.Add(intTmpWidth)
                '    intLeftPos += intTmpWidth
                'Next intTmpWidth

                '== Build 2nd header line from Grid Columns Headers. (AFTER barcode)

                Dim aStrHeadings() As String = {"Barcode", "_Id", "CostEx", "sTax", "Qty", _
                                                       "TotCostEx", "SellAvg", "TotActualEx", "grPofit", "TotActualInc", "gpRate"}
                '-- use array..
                For cx As Integer = 0 To (aIntWidths.Length - 1)
                    intTmpWidth = aIntWidths(0)
                    intColumnCharWidth = intTmpWidth \ 8  '== say average 8px per char 8-point.
                    s1 = aStrHeadings(cx)  '= dgvReport.Columns(cx + k_FIRST_AMT_GRIDCOL).HeaderText
                    If cx = 0 Then  '-barcode-
                        msReportColumnHdr2 &= LSet(s1, intColumnCharWidth)
                    Else '-not first-
                        msReportColumnHdr2 &= RSet(s1, intColumnCharWidth)
                    End If
                Next cx

                '-- build data lines from Grid data..
                '- 1st line has cat1/Cat2/Description/ Barcode..
                '-- 2nd line has grid amount columns..
                rowx = 0
                For Each gridRowX As DataGridViewRow In dgvReport.Rows
                    '-"<MINPAGEROOM"-  keep item lines together.
                    mColReportLines.Add("<MINPAGEROOM LINES= ""3""  />")
                    '- 1st line has cat1/Cat2/Description..
                    sDataLine = "<textline>"
                    With gridRowX
                        sDataLine &= "<txt TAB=""" & colColumnLefts(1) & """  align=""left"" "
                        sDataLine &= " width = """ & colColumnWidths(1) & """  >" & .Cells(0).Value & "</txt>"

                        sDataLine &= "<txt TAB=""" & colColumnLefts(2) & """  align=""left"" "
                        sDataLine &= " width = """ & colColumnWidths(2) & """  >" & .Cells(1).Value & "</txt>"
                        '- description-
                        sDataLine &= "<txt TAB=""" & colColumnLefts(3) & """  align=""left"" "
                        sDataLine &= " width = """ & colColumnWidths(3) & """  >" & .Cells(2).Value & " -----------</txt>"

                        '=sDataLine &= "<txt TAB=""" & k_TAB_CUST_AMOUNT & """  align=""right""  fontstyle=""bold""  "
                        '=sDataLine &= " width = """ & k_WIDTH_CUST_AMT & """  >" & sAmount & "</txt>"
                    End With
                    sDataLine &= "</textline>"
                    mColReportLines.Add(sDataLine)

                    '-- 2nd line has grid amount columns..
                    sDataLine = "<textline>"
                    With gridRowX
                        idx = 0
                        intLeftPos = 0 '= 16
                        For cx As Integer = 0 To (aIntWidths.Length - 1) '=(k_FIRST_AMT_GRIDCOL) To (dgvReport.Columns.Count - 1)
                            idx += 1
                            intTmpWidth = aIntWidths(cx)
                            If idx = 1 Then '-barcode
                                sDataLine &= "<txt TAB=""" & intLeftPos & """  align=""left"" "
                                sDataLine &= " width = """ & intTmpWidth & """  >" & .Cells(cx + k_FIRST_AMT_GRIDCOL).Value & "</txt>"
                            Else
                                sDataLine &= "<txt TAB=""" & intLeftPos & """  align=""right"" "
                                sDataLine &= " width = """ & intTmpWidth & """  >" & .Cells(cx + k_FIRST_AMT_GRIDCOL).Value & "</txt>"
                            End If
                            intLeftPos += intTmpWidth
                        Next cx
                    End With
                    sDataLine &= "</textline>"
                    mColReportLines.Add(sDataLine)
                    '-VERTICALGAP_HALFLINE-
                    mColReportLines.Add("<VERTICALGAP_HALFLINE />")

                Next gridRowX  '--next data line.

                '-- This Will be printed when print Button is Pressed.

                '-- E n d  of product Sales.-

                '- Option Stock on hand--
                '-  Show all stock items and current stock holdings.
            Case "optreportstock"
                '==Dim sWhere As String = ""

                '=3519.-0303-- New version--  has own class.
                '-- SEE BELOW ..-
                '-- SEE BELOW ..-
                '-- SEE BELOW ..-


                'DoEvents()
                'btnPrintReport.Enabled = True '--ca print the grid..
                '-- E n d OLDSTYLE  of S t o c k  R e p o r t.-
                '-- E n d OLDSTYLE  of S t o c k  R e p o r t.-
                '-- E n d OLDSTYLE  of S t o c k  R e p o r t.-

                '--  nEW VERSION..

                '-  NOW IS CHILD--  4221.0416-

                Dim clsWhatsInStock1 As clsWhatsInStock

                clsWhatsInStock1 = New clsWhatsInStock(mCnnSql, msSqlDbName, mColSqlDBInfo, msVersionPOS, msStaffName)

                Dim colStockReportLines As Collection
                If clsWhatsInStock1.BuildReport(mFrmParent1, colStockReportLines) Then
                    '-show report..
                    s1 = "<drawline fontstyle=""bold"" />"
                    colStockReportLines.Add(s1)
                    bPreviewOnly = True
                    '-- Confirm preview..
                    If (colStockReportLines.Count > 500) Then
                        Dim msgResult As MsgBoxResult = _
                              MsgBox("Please Note-  This Report has " & _
                                     FormatNumber(colStockReportLines.Count, 0) & " lines.." & vbCrLf & _
                                     "Bypass the Preview and print on " & msReportPrinterName & " ??", _
                                                       MsgBoxStyle.YesNoCancel, MsgBoxStyle.DefaultButton1)
                        If (msgResult = MsgBoxResult.Yes) Then
                            bPreviewOnly = False
                        ElseIf (msgResult = MsgBoxResult.Cancel) Then
                            Exit Function   '- no more..
                        Else
                            '- show preview..
                            bPreviewOnly = True
                        End If '-bypass preview..
                    End If  '-reportlines-
                    '== Target 6201- Updating 14-July-2021..
                    Dim sStockSupplierSelected As String = "- All Suppliers-"
                    If cboSupplierStock.SelectedIndex > 0 Then
                        sStockSupplierSelected = cboSupplierStock.SelectedItem
                    End If
                    '== END Target 6201- Updating 14-July-2021..
                    Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS,
                                                                  colStockReportLines, msReportPrinterName,
                                                                  msBusinessName, "What's In Stock Report-",
                                                                   Color.SaddleBrown,
                                                                   "Stock List: " & sStockSupplierSelected & vbCrLf &
                                                                           " As at:   " & Format(Now, "ddd dd-MMM-yyyy"),
                                                                    " By Product Line.. ",
                                                                    "  Item Description" & Space(32) &
                                                                        " Cat1/Cat2 " & Space(16) & "    Barcode  " & Space(16) &
                                                                        "   Cost-Ex   Sell_Ex " & Space(10) & "   Qty         Ext-Cost      Ext-Sell  ")
                Else
                    MsgBox("No Stock Report..", MsgBoxStyle.Exclamation)
                End If  '-clsWhatsInStock1-

            Case "optreportprintstockbarcodes"
                '==  Target-New-Build-4257..  07-July-2020.
                '==  Target-New-Build-4257..  07-July-2020.
                '==  Target-New-Build-4257..  07-July-2020.

                '=Dim s1 As String
                Dim sItemBarcodeFontName As String = "IDAutomationHC39M"  '-default
                Dim intItemBarcodeFontSize As Integer = 9  '-default-

                Dim bShowNegativeStock As Boolean = (Not chkNoReportIfNegStock.Checked)
                Dim bShowZeroStock As Boolean = (Not chkNoReportIfZeroStock.Checked)
                Dim bShowPositiveStock As Boolean = (Not chkNoReportIfPosStock.Checked)

                Dim clsStockList1 As clsStockBarcodeList

                If mSysInfo1.contains("POS_BARCODEFONTNAME") Then
                    sItemBarcodeFontName = mSysInfo1.item("POS_BARCODEFONTNAME")
                End If
                s1 = mSysInfo1.item("POS_BARCODEFONTSIZE")
                If (s1 <> "") AndAlso (IsNumeric(s1)) Then
                    Dim L1 As Integer = CInt(s1)
                    If (L1 > 3) And (L1 < 36) Then
                        intItemBarcodeFontSize = L1
                    End If
                End If
                clsStockList1 = New clsStockBarcodeList(mCnnSql, msSqlDbName, mColSqlDBInfo, msVersionPOS, msStaffName)

                Call clsStockList1.printStockBarcodeList(bShowNegativeStock, bShowZeroStock, bShowPositiveStock, _
                                                          msReportPrinterName, sItemBarcodeFontName, intItemBarcodeFontSize)
                Exit Function

                '==END  Target-New-Build-4257..  07-July-2020.


            Case "optreportgoodsreceivedbyperiod"
                '-- goods received by period--
                '-- goods received by period--
                '-- goods received by period--
                '-  define tab columns- (ofsets from our marg)-
                Const k_TAB_SUPPLIER_NAME As Integer = 0
                Const k_TAB_GOODS_INVOICE_NO As Integer = 0  '-invoice No. and date-
                '--  ALSO is the column for Item Barcode.
                Const k_TAB_GOODS_ID As Integer = 120  '-GoodsId and Item-Description under.-
                Const k_TAB_GOODS_INVDATE As Integer = 160  '-invoice No. and date-
                Const k_TAB_GOODS_RECDATE As Integer = 240  '-invoice No. and date-

                Const k_TAB_GOODS_STAFF As Integer = 660

                Const k_TAB_ITEM_BARCODE As Integer = 0
                '= Const k_TAB_CUST_CAT2 As Integer = 120
                Const k_TAB_ITEM_DESCR As Integer = 120
                Const k_TAB_ITEM_UNIT_COST As Integer = 330
                Const k_TAB_ITEM_QTY As Integer = 390   '-- transaction.
                Const k_TAB_ITEM_TAX_CODE As Integer = 440
                Const k_TAB_ITEM_TOTAL_EX As Integer = 480
                Const k_TAB_ITEM_TOTAL_TAX As Integer = 560
                Const k_TAB_ITEM_TOTAL_INC As Integer = 640
                '-rhs=760.

                '--Widths for Right justified amounts.
                Const k_WIDTH_ITEM_UNIT_COST As Short = 60
                Const k_WIDTH_ITEM_TOTAL As Short = 80
                Const k_WIDTH_ITEM_QTY As Short = 40
                '= Const k_TAB_TRANCODE As Integer = 420

                Dim sConnect, sServer, sErrors, sShapeSql As String
                Dim intRecordsAffected, intGoods_id As Integer
                Dim rdrGoods, rdrItems As OleDbDataReader
                Dim cmd1 As OleDbCommand
                Dim sSupplierName, sSupplierBarcode, sSupplierInvoiceNo As String
                Dim sItemBarcode, sItemDescription As String
                Dim sGoodsTaxCode, sFreightTaxCode As String
                Dim decItemQty, decItemCost_ex, decTotalTax, decTotalCost_inc As Decimal
                Dim decFreightCost_ex, decFreightTax, decFreightCost_inc As Decimal

                '--Goods Invoice Total.
                Dim decGoodsTotal_ex, decGoodsTotalTax, decGoodsTotal_inc As Decimal
                '--Supplier Total.
                Dim decSupplierTotal_ex, decSupplierTotalTax, decSupplierTotal_inc As Decimal
                '--Goods Final Totals.
                Dim decFinalTotalFreightNT_ex As Decimal = 0  '--NON-Taxable.
                Dim decFinalTotalFreight_ex, decFinalTotalFreightTax, decFinalTotalFreight_inc As Decimal
                Dim decFinalTotalItemsNT_ex As Decimal = 0  '-NON-Taxable..
                Dim decFinalTotalItems_ex, decFinalTotalItemsTax, decFinalTotalItems_inc As Decimal

                Dim sLastSupplierName As String = ""

                dateStart = DTPickerFrom.Value
                dateEnd = DTPickerTo.Value

                decSupplierTotal_ex = 0 : decSupplierTotalTax = 0 : decSupplierTotal_inc = 0
                '-finals-
                decFinalTotalFreight_ex = 0 : decFinalTotalFreightTax = 0 : decFinalTotalFreight_inc = 0
                decFinalTotalItems_ex = 0 : decFinalTotalItemsTax = 0 : decFinalTotalItems_inc = 0

                '= sSql = msMakeSalesListSql()
                sWhere = ""  '==  " WHERE (invoice.terminal_id='" & msComputerName & "' )"
                '-- All suppliers included..  period is decider-
                sWhere = msReportSetupWhereCondition("GoodsReceived.goods_date", sWhere)

                '-- setup sql SHAPE connection for reports..--
                '-- setup sql SHAPE connection for reports..--

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

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

                '-- ok--  get all goodsreceived for selected period.
                '- Tack barcode onto end of SupplierName to maki unique.

                sShapeSql = " SHAPE {SELECT *, staff.docket_name, "
                sShapeSql &= "  supplier.supplierName + ' (' + supplier.barcode + ')' AS supplierNameEx , "
                sShapeSql &= "    supplier.barcode AS supplier_barcode  "
                sShapeSql &= " FROM [GoodsReceived] "
                sShapeSql &= "  JOIN supplier ON GoodsReceived.supplier_id=supplier.supplier_id "
                sShapeSql &= "    JOIN staff ON GoodsReceived.staff_id=staff.staff_id "
                sShapeSql &= sWhere
                sShapeSql &= " ORDER BY supplierNameEx, GoodsReceived.goods_id } "
                sShapeSql &= " APPEND (  "
                sShapeSql &= " { SELECT *, stock.barcode, cat1, description "
                sShapeSql &= "        FROM dbo.GoodsReceivedLine AS GRL "
                sShapeSql &= "          JOIN stock ON (stock.stock_id=GRL.stock_id) "
                sShapeSql &= "}  "
                sShapeSql &= "   AS rsGoodsItems RELATE goods_id TO goods_id)"

                '-- start retrieval-
                Try
                    '=adapterReport = New OleDbDataAdapter(sShapeSql, mCnnShape)
                    cmd1 = New OleDbCommand(sShapeSql, mCnnShape)
                    rdrGoods = cmd1.ExecuteReader
                Catch ex As Exception
                    MsgBox("Error getting Goods/Items recordset..." & vbCrLf & ex.Message, MsgBoxStyle.Information)
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    '= dgvGoodsList.Enabled = True
                    Exit Function
                End Try
                '-- check it all-

                '- test results-
                Dim sResults As String = "Results are:" & vbCrLf
                Dim sLine As String

                '-- Use our Report printer Class-
                mColReportLines = New Collection

                Try  '-main try GoodsREceived.
                    If rdrGoods.HasRows Then
                        Do While rdrGoods.Read
                            sSupplierBarcode = rdrGoods.Item("supplier_barcode")
                            sSupplierName = rdrGoods.Item("supplierNameEx")
                            '-IF NEW supplier name, then Show Previous Supplier Totals.
                            If (sSupplierName <> sLastSupplierName) Then
                                '-- first or new supplier..
                                If (sLastSupplierName <> "") Then
                                    '-- show supplier totals.
                                    '--total Line..
                                    sLine = "<textline>"
                                    sLine &= "<txt TAB=""" & k_TAB_GOODS_INVDATE & """  fontstyle=""bold""  >" & _
                                                                                "Total for " & sLastSupplierName & "</txt>"
                                    '-item total-ex.
                                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  fontstyle=""bold""  " & _
                                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decSupplierTotal_ex, 2) & "</txt>"
                                    '-item total-tax.
                                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""  fontstyle=""bold""   " & _
                                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decSupplierTotalTax, 2) & "</txt>"
                                    '-item total-INC.
                                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  fontstyle=""bold""  " & _
                                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decSupplierTotal_inc, 2) & "</txt>"
                                    sLine &= "</textline>"
                                    mColReportLines.Add(sLine)
                                    s1 = "<drawline/>"
                                    mColReportLines.Add(s1)
                                    s1 = ""
                                    mColReportLines.Add(s1)
                                End If  '-show-
                                '- save next supplier.
                                sLastSupplierName = sSupplierName
                                decSupplierTotal_ex = 0
                                decSupplierTotalTax = 0
                                decSupplierTotal_inc = 0
                            End If

                            '- Back to current Goods invoice.

                            intGoods_id = rdrGoods.Item("goods_id")
                            sSupplierInvoiceNo = rdrGoods.Item("invoice_no")
                            decGoodsTotal_ex = CDec(rdrGoods.Item("total_ex"))
                            decGoodsTotalTax = CDec(rdrGoods.Item("total_tax"))
                            decGoodsTotal_inc = CDec(rdrGoods.Item("total_inc"))
                            '-
                            '- Freight on this invoice. (already included in Goods Totals.)
                            sFreightTaxCode = CStr(rdrGoods.Item("freight_taxCode"))
                            decFreightCost_ex = CDec(rdrGoods.Item("freight_ex"))
                            decFreightTax = CDec(rdrGoods.Item("freight_tax"))
                            decFreightCost_inc = CDec(rdrGoods.Item("freight_inc"))

                            '- suppplier totals..
                            decSupplierTotal_ex += decGoodsTotal_ex
                            decSupplierTotalTax += decGoodsTotalTax
                            decSupplierTotal_inc += decGoodsTotal_inc
                            '--Final Totals-
                            '- ITEMS totals..
                            'decFinalTotalItems_ex += decGoodsTotal_ex
                            'decFinalTotalItemsTax += decGoodsTotalTax
                            'decFinalTotalItems_inc += decGoodsTotal_inc
                            '--Final Totals-
                            '- FREIGHT totals..
                            If UCase(sFreightTaxCode) <> "GST" Then
                                '--free-
                                decFinalTotalFreightNT_ex += decFreightCost_ex
                            Else  '-taxable-
                                decFinalTotalFreight_ex += decFreightCost_ex
                                decFinalTotalFreightTax += decFreightTax
                                decFinalTotalFreight_inc += decFreightCost_inc
                            End If

                            sResults &= "Supplier: " & sSupplierName & vbCrLf '= " [" & sSupplierBarcode & "];" & vbCrLf
                            sResults &= "    .. Invoice-No: " & sSupplierInvoiceNo & ";  Goods-Id: " & intGoods_id & vbCrLf
                            '- Supplier Name Line.
                            sLine = "<textline>"
                            sLine &= "<txt TAB=""" & k_TAB_SUPPLIER_NAME & """  >" & sSupplierName & "</txt>"
                            sLine &= "</textline>"
                            mColReportLines.Add(sLine)
                            '-- Goods Invoice Line..
                            sLine = "<textline>"
                            sLine &= "<txt TAB=""" & k_TAB_GOODS_INVOICE_NO & """  fontstyle=""bold""  >" & sSupplierInvoiceNo & "</txt>"
                            sLine &= "<txt TAB=""" & k_TAB_GOODS_ID & """  >" & intGoods_id & ".</txt>"
                            '-- Dates..
                            '- rdrGoods.Item("goods_date")
                            '- rdrGoods.Item("invoice_date")
                            sLine &= "<txt TAB=""" & k_TAB_GOODS_INVDATE & """  >" & _
                                                    Format(rdrGoods.Item("invoice_date"), "dd-MMM-yyyy") & ".</txt>"
                            sLine &= "<txt TAB=""" & k_TAB_GOODS_RECDATE & """  >" & _
                                                    Format(rdrGoods.Item("goods_date"), "dd-MMM-yyyy") & ".</txt>"
                            '-- SHOW FREIGHT here on this line, if any..
                            'If (decFreightCost_ex <> 0) Then
                            sLine &= "<txt TAB=""" & k_TAB_ITEM_UNIT_COST & """  >FREIGHT:</txt>"
                            '-item total-ex.
                            sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  " & _
                                      " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFreightCost_ex, 2) & "</txt>"
                            '-item total-tax.
                            sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""  " & _
                                      " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFreightTax, 2) & "</txt>"
                            '-item total-INC.
                            sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  " & _
                                      " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFreightCost_inc, 2) & "</txt>"
                            'End If
                            sLine &= "</textline>"
                            mColReportLines.Add(sLine)

                            '-get items for this Goods Invoice..
                            If TypeOf rdrGoods.Item("rsGoodsItems") Is IDataReader Then
                                '-- has serials list.
                                rdrItems = rdrGoods.Item("rsGoodsItems")
                                If rdrItems.HasRows Then
                                    Do While rdrItems.Read
                                        sItemBarcode = rdrItems.Item("barcode")
                                        sItemDescription = rdrItems.Item("description")
                                        decItemCost_ex = CDec(rdrItems.Item("cost_ex"))
                                        sGoodsTaxCode = rdrItems.Item("goods_taxCode")
                                        decItemQty = CDec(rdrItems.Item("quantity"))
                                        decTotalCost_ex = CDec(rdrItems.Item("total_ex"))
                                        decTotalTax = CDec(rdrItems.Item("total_tax"))
                                        decTotalCost_inc = CDec(rdrItems.Item("total_inc"))

                                        '--have to add to final Totals here 
                                        '==  so as to separate Taxable-Non-Taxable..
                                        If (UCase(sGoodsTaxCode) <> "GST") Then
                                            '-free- 
                                            decFinalTotalItemsNT_ex += decTotalCost_ex
                                        Else  '-taxable-  
                                            decFinalTotalItems_ex += decTotalCost_ex
                                            decFinalTotalItemsTax += decTotalTax
                                            decFinalTotalItems_inc += decTotalCost_inc
                                        End If
                                        sResults &= "  -- Item: " & sItemBarcode & "; " & sItemDescription & vbCrLf
                                        sResults &= "    .. Cost_ex: " & FormatNumber(decItemCost_ex, 2) & "; " & _
                                                                 " Total_inc: " & FormatNumber(decTotalCost_inc, 2) & vbCrLf
                                        '--Item Line..
                                        sLine = "<textline>"
                                        sLine &= "<txt TAB=""" & k_TAB_ITEM_BARCODE & """  >" & sItemBarcode & "</txt>"
                                        sLine &= "<txt TAB=""" & k_TAB_ITEM_DESCR & """  >" & sItemDescription & "</txt>"
                                        '=sLine &= "<txt TAB=""" & k_TAB_ITEM_UNIT_COST & """  >" & decItemCost_ex & "</txt>"
                                        sLine &= "<txt  TAB=""" & k_TAB_ITEM_UNIT_COST & """   align=""right""  " & _
                                                  " width = """ & k_WIDTH_ITEM_UNIT_COST & """  >" & FormatNumber(decItemCost_ex, 2) & "</txt>"
                                        '-qty-
                                        sLine &= "<txt  TAB=""" & k_TAB_ITEM_QTY & """   align=""right""  " & _
                                                  " width = """ & k_WIDTH_ITEM_QTY & """  >" & CInt(decItemQty) & "</txt>"
                                        sLine &= "<txt TAB=""" & k_TAB_ITEM_TAX_CODE & """  >" & sGoodsTaxCode & "</txt>"
                                        '-item total-ex.
                                        sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  " & _
                                                  " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decTotalCost_ex, 2) & "</txt>"
                                        '-item total-tax.
                                        sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""  " & _
                                                  " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decTotalTax, 2) & "</txt>"
                                        '-item total-INC.
                                        sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  " & _
                                                  " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decTotalCost_inc, 2) & "</txt>"
                                        sLine &= "</textline>"
                                        mColReportLines.Add(sLine)
                                    Loop  '-read-
                                    rdrItems.Close()
                                    sResults &= vbCrLf
                                End If  '-items rows..
                            End If '-rdr type..

                            '- show Goods Totals. and draw line.
                            '--GOODS Line..
                            sLine = "<textline>"
                            sLine &= "<txt TAB=""" & k_TAB_GOODS_INVDATE & """  fontstyle=""bold""  >" & _
                                                                        "Total for Invoice " & sSupplierInvoiceNo & "</txt>"
                            '-item total-ex.
                            sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  fontstyle=""bold""  " & _
                                      " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decGoodsTotal_ex, 2) & "</txt>"
                            '-item total-tax.
                            sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""  fontstyle=""bold""   " & _
                                      " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decGoodsTotalTax, 2) & "</txt>"
                            '-item total-INC.
                            sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  fontstyle=""bold""  " & _
                                      " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decGoodsTotal_inc, 2) & "</txt>"
                            sLine &= "</textline>"
                            mColReportLines.Add(sLine)
                            's1 = "<drawline/>"
                            'mColReportLines.Add(s1)
                            's1 = "<drawline/>"
                            'mColReportLines.Add(s1)
                            s1 = ""
                            mColReportLines.Add(s1)
                            '= mColReportLines.Add(s1)
                        Loop '-goods read-

                        rdrGoods.Close()

                        '-- Show last Supplier Totals if last supplier not shown yet.
                        If (sLastSupplierName <> "") Then
                            '-- show supplier totals.
                            '--total Line..
                            sLine = "<textline>"
                            sLine &= "<txt TAB=""" & k_TAB_GOODS_INVDATE & """  fontstyle=""bold""  >" & _
                                                                        "Total for " & sLastSupplierName & "</txt>"
                            '-item total-ex.
                            sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  fontstyle=""bold""  " & _
                                      " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decSupplierTotal_ex, 2) & "</txt>"
                            '-item total-tax.
                            sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""  fontstyle=""bold""   " & _
                                      " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decSupplierTotalTax, 2) & "</txt>"
                            '-item total-INC.
                            sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  fontstyle=""bold""  " & _
                                      " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decSupplierTotal_inc, 2) & "</txt>"
                            sLine &= "</textline>"
                            mColReportLines.Add(sLine)
                            s1 = "<drawline/>"
                            mColReportLines.Add(s1)
                            s1 = ""
                            mColReportLines.Add(s1)
                        End If  '-show-
                    End If '-rdrGoods.HasRows-

                    '-- FINAL TOTALS..
                    '-  FIRST- ask for Min page space..
                    '--   need min 10 lines for invoicve..
                    s1 = "<MinPageRoom Lines= ""15"" />"
                    mColReportLines.Add(s1)

                    '-   Final Line.
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_GOODS_INVDATE & """  >FINAL TOTALS:</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '--Total I T E M S..
                    '--Total I T E M S..
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_GOODS_RECDATE & """  >TOTAL Goods Items:</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '--T a x a b l e-
                    '--T a x a b l e-
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_ITEM_UNIT_COST & """  fontstyle=""bold""  >" & _
                                                                "Taxable:</txt>"
                    '-item total-ex.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""   " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalItems_ex, 2) & "</txt>"
                    '-item total-tax.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""   " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalItemsTax, 2) & "</txt>"
                    '-item total-INC.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  fontstyle=""bold""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalItems_inc, 2) & "</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '--NON T a x a b l e-
                    '-- NON T a x a b l e-
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_ITEM_UNIT_COST & """  fontstyle=""bold""  >" & _
                                                                "NON-Taxable:</txt>"
                    '-item total-ex.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalItemsNT_ex, 2) & "</txt>"
                    '-item total-tax.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""   " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >0.00</txt>"
                    '-item total-INC.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  fontstyle=""bold""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalItemsNT_ex, 2) & "</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '--SUM of the above two Items Totals.
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_ITEM_UNIT_COST & """  fontstyle=""bold""  >" & _
                                                                "TOTAL:</txt>"
                    '-item total-ex.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  fontstyle=""bold""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & _
                                                 FormatNumber(decFinalTotalItems_ex + decFinalTotalItemsNT_ex, 2) & "</txt>"
                    '-item total-tax.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""  fontstyle=""bold""   " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalItemsTax, 2) & "</txt>"
                    '-item total-INC.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  fontstyle=""bold""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & _
                                                FormatNumber(decFinalTotalItems_inc + decFinalTotalItemsNT_ex, 2) & "</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)
                    sLine = ""
                    mColReportLines.Add(sLine)

                    '--FREIGHT-
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_GOODS_RECDATE & """  >TOTAL Freight:</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '-- Freight FINAL Totals..
                    '-- Freight FINAL Totals..

                    '-- Freight TAXABLE-
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_ITEM_UNIT_COST & """  fontstyle=""bold""  >" & _
                                                                "Taxable:</txt>"
                    '-Freight total-ex.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""    " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalFreight_ex, 2) & "</txt>"
                    '-Freight total-tax.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""    " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalFreightTax, 2) & "</txt>"
                    '-Freight total-INC.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  fontstyle=""bold""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalFreight_inc, 2) & "</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '-- Freight NON-Taxable-
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_ITEM_UNIT_COST & """  fontstyle=""bold""  >" & _
                                                                "NON-Taxable:</txt>"
                    '-Freight total-ex.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  fontstyle=""bold""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalFreightNT_ex, 2) & "</txt>"
                    '-Freight total-tax.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""  fontstyle=""bold""   " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >0.00</txt>"
                    '-Freight total-INC.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  fontstyle=""bold""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalFreightNT_ex, 2) & "</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '-- Freight TOTALS-
                    '-Freight total-.
                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_ITEM_UNIT_COST & """  fontstyle=""bold""  >" & _
                                                                "TOTAL:</txt>"
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_EX & """   align=""right""  fontstyle=""bold""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & _
                                                 FormatNumber(decFinalTotalFreight_ex + decFinalTotalFreightNT_ex, 2) & "</txt>"
                    '-Freight total-tax.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_TAX & """   align=""right""  fontstyle=""bold""   " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & FormatNumber(decFinalTotalFreightTax, 2) & "</txt>"
                    '-Freight total-INC.
                    sLine &= "<txt  TAB=""" & k_TAB_ITEM_TOTAL_INC & """   align=""right""  fontstyle=""bold""  " & _
                              " width = """ & k_WIDTH_ITEM_TOTAL & """  >" & _
                                                FormatNumber(decFinalTotalFreight_inc + decFinalTotalFreightNT_ex, 2) & "</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    '-- That's all folks..
                    s1 = "<drawline  fontstyle=""bold"" />"
                    mColReportLines.Add(s1)

                    sLine = "<textline>"
                    sLine &= "<txt TAB=""" & k_TAB_GOODS_INVDATE & """  >   == The End ==</txt>"
                    sLine &= "</textline>"
                    mColReportLines.Add(sLine)

                    sResults &= "==  the end =="
                Catch ex As Exception
                    MsgBox("Error in Main GoodsReceived Report passage.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    Exit Function
                End Try '-main try GoodsREceived.

                '- was test- MsgBox(sResults, MsgBoxStyle.Information)
                '-- print the Report..
                s1 = "<drawline fontstyle=""bold"" />"
                mColReportLines.Add(s1)
                bPreviewOnly = True
                '-- Confirm preview..
                If (mColReportLines.Count > 500) Then
                    Dim msgResult As MsgBoxResult = _
                          MsgBox("Please Note-  This Report has " & _
                                 FormatNumber(mColReportLines.Count, 0) & " lines.." & vbCrLf & _
                                 "Bypass the Preview and print on " & msReportPrinterName & " ??", _
                                                   MsgBoxStyle.YesNoCancel, MsgBoxStyle.DefaultButton1)
                    If (msgResult = MsgBoxResult.Yes) Then
                        bPreviewOnly = False
                    ElseIf (msgResult = MsgBoxResult.Cancel) Then
                        Exit Function   '- no more..
                    Else
                        '- show preview..
                        bPreviewOnly = True
                    End If '-bypass preview..
                End If  '-reportlines-
                Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS, _
                                                          mColReportLines, msReportPrinterName, _
                                                          msBusinessName, "Goods Received Period Report-", _
                                                           Color.DarkGoldenrod, _
                                                           "Goods Received for Period: " & vbCrLf & _
                                                                  "From: " & Format(dateStart, "ddd dd-MMM-yyyy") & vbCrLf & _
                                                                   "    To:   " & Format(dateEnd, "ddd dd-MMM-yyyy"), _
                                                            " By Supplier/Goods Invoice.. ", _
                                                            "  Supplier/Invoice  Goods-Id      Inv.date       Rec.Date" & Space(13) & _
                                                                " Cost_ex " & Space(14) & " Qty  " & Space(24) & " Total_ex-        Tax           Total_inc")
                '-- When all done..
                '-- When all done..
                mCnnShape.Close()

            Case Else
                MsgBox("No such Report! ", MsgBoxStyle.Information)  '-: Exit Function
        End Select

    End Function  '-mbSetupReport-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    Private Function mbClearReport() As Boolean
        dgvReport.DataSource = Nothing
        dgvReport.Rows.Clear()
        dgvReport.Columns.Clear()

    End Function  '---clear-
    '= = = = = = = = = = = =

    '-- Tree Node selected-

    Private Function mbSelectReport(ByVal sNodeName As String) As Boolean

        chkNoReportIfZeroStock.Enabled = False
        panelPeriodOpts.Enabled = False
        '= mColReceiptLines = Nothing
        '= btnPrintSummary.Enabled = False
        grpBoxCustomer.Enabled = False
        grpBoxCategory.Enabled = False
        TabControlProduct.Enabled = False
        panelInvoiceOptions.Enabled = False  '=4219.1214= = FIX FOR 4219.1130=
        '==   == 4221.0206.  06-Feb-2020- 
        msCurrentNodeName = sNodeName
        btnRefresh.Enabled = False

        Select Case LCase(sNodeName)
            Case "invoices_preview", "invoices_grid"
                '=4219.1214= = FIX FOR 4219.1130=
                '==  Add Options to Sales Invoice Listing.
                TabControlProduct.Enabled = True '= FIX FOR 4219.1130=
                TabControlProduct.SelectedTab = TabControlProduct.TabPages("TabPageSalesInvoices")
                panelStockReport.Enabled = False
                panelInvoiceOptions.Enabled = True  ''=4219.1214= == FIX FOR 4219.1130=
                grpBoxCategory.Enabled = False  ''=4219.1214= == FIX FOR 4219.1130=

                TabControlOptions.SelectedTab = TabControlOptions.TabPages("TabPageSales")
                msReportName = "optReportInvoiceListing"
                labReportName.Text = "Sales Invoices (All w/s) for selected period. "
                labExplain.Text = "Previews list all Sales Invoices & Refunds for Selected Period."
                chkShowInvoiceLines.Enabled = True
                chkShowInvoiceLines.Checked = False
                If (LCase(sNodeName) = "invoices_grid") Then
                    labExplain.Text = "Grid display all Sales Invoices & Refunds for Selected Period."
                    chkShowInvoiceLines.Enabled = False
                End If
                panelPeriodOpts.Enabled = True
                Call mbClearReport()
                btnRefresh.Enabled = True

            Case "product_sales"
                TabControlProduct.Enabled = True
                TabControlProduct.SelectedTab = TabControlProduct.TabPages("TabPageProductSales")
                optProductsAll.Checked = True

                panelStockReport.Enabled = False
                grpBoxCategory.Enabled = True
                TabControlOptions.SelectedTab = TabControlOptions.TabPages("TabPageSales")
                msReportName = "optReportProductSales"
                labReportName.Text = "Report: Product Sales"
                labExplain.Text = "Shows Sales summarised by Product (Category) for selected Period." & _
                                         "  (User can select specific Cat1 if needed.)"
                panelPeriodOpts.Enabled = True
                Call mbClearReport()
                btnRefresh.Enabled = True

            Case "customer_sales"
                TabControlProduct.Enabled = True
                TabControlProduct.SelectedTab = TabControlProduct.TabPages("TabPageCustomerSales")
                panelStockReport.Enabled = False
                TabControlOptions.SelectedTab = TabControlOptions.TabPages("TabPageSales")
                msReportName = "optReportCustomerSales"
                labReportName.Text = "Report: Customer Sales"
                labExplain.Text = "Shows Sales summarised by Customer/Product for selected Period."
                panelPeriodOpts.Enabled = True
                grpBoxCustomer.Enabled = True
                Call mbClearReport()
                chkAllCustomers.Checked = True
                btnRefresh.Enabled = True

            Case "revenue_analysis"
                panelStockReport.Enabled = False
                TabControlOptions.SelectedTab = TabControlOptions.TabPages("TabPageSales")
                msReportName = "optReportRevenueAnalysis"
                labReportName.Text = "Payments (Revenue) Analysis- All Tills. "
                labExplain.Text = "Lists and Summarises all REVENUE received (both Sales and Account Payments) " & _
                                      "with Type Totals, for the selected Period."
                panelPeriodOpts.Enabled = True
                Call mbClearReport()
                btnRefresh.Enabled = True

            Case "till_analysis", "till_analysis_grid"
                panelStockReport.Enabled = False
                TabControlOptions.SelectedTab = TabControlOptions.TabPages("TabPageSales")
                msReportName = "optReportTillAnalysis"
                labReportName.Text = "Till Payments Analysis-All Tills. "
                labExplain.Text = "Lists and Summarises all TILL Movements (both Sales and Account Payments) " & _
                                      "with Type Totals, for the selected Period."
                panelPeriodOpts.Enabled = True
                Call mbClearReport()
                btnRefresh.Enabled = True

            Case "stock_on_hand", "stock_barcode_list"
                '- select Stock Tab page..
                panelPeriodOpts.Enabled = False
                panelStockReport.Enabled = True
                TabControlOptions.SelectedTab = TabControlOptions.TabPages("TabPageStock")

                chkNoReportIfZeroStock.Enabled = True
                chkNoReportIfZeroStock.Checked = True    '-- excludes zero stock if true.

                chkNoReportIfNegStock.Enabled = True
                chkNoReportIfNegStock.Checked = True   '-- excludes Neg. stock if true.

                msReportName = "optReportStock"

                '==  Target-New-Build-4257..  07-July-2020.
                '==  Target-New-Build-4257..  07-July-2020.
                If LCase(sNodeName) = "stock_barcode_list" Then
                    msReportName = "optReportPrintStockBarcodes"
                    chkNoReportIfNegStock.Checked = False
                    chkNoReportIfPosStock.Visible = True
                    chkNoReportIfPosStock.Enabled = True
                    chkNoReportIfPosStock.Checked = True

                    labReportName.Text = "Report: Stock Barcode List."
                    labExplain.Text = "Stock Barcode List as of: " & Format(Now, "ddd dd-MMM-yyyy hh:mm tt") & "."

                Else  '-stock report.
                    chkNoReportIfPosStock.Visible = False
                    labReportName.Text = "Report: Stock on Hand."
                    labExplain.Text = "Stock on hand by Product as of: " & Format(Now, "ddd dd-MMM-yyyy hh:mm tt") & "."
                End If
                '== END Target-New-Build-4257..  07-July-2020.


                Call mbClearReport()
                btnRefresh.Enabled = True

            Case "goods_received_by_period"
                '=4201.0611=
                panelStockReport.Enabled = False
                TabControlOptions.SelectedTab = TabControlOptions.TabPages("TabPageSales")
                msReportName = "optReportGoodsReceivedByPeriod"
                labReportName.Text = "All Goods Received for selected period. "
                labExplain.Text = "Lists all Goods Received Invoices with stock items for selected Period."
                panelPeriodOpts.Enabled = True
                Call mbClearReport()
                btnRefresh.Enabled = True

            Case Else
                '-- must have clicked on a root..-
                labReportName.Text = ""
                labExplain.Text = ""
                btnRefresh.Enabled = False
        End Select

    End Function  '-mbSelectReport-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- Form Load Event -

    Private Sub frmPOS3Reports_Load(ByVal sender As Object, _
                                  ByVal e As EventArgs) Handles MyBase.Load
        '== Dim ix, intDefault As Integer
        '= Dim s1 As String
        Dim pd1 As New PrintDocument()
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        Dim s1, sName As String
        Dim fontRoot As New Font("Lucida Sans", 11, FontStyle.Regular)

        mIntFormDesignHeight = Me.Height    '-- starting dimensions..-
        mIntFormDesignWidth = Me.Width     '-- starting dimensions..-

        btnPrintReport.Enabled = False
        '== btnPrintSummary.Enabled = False
        grpBoxOptions.Text = "Report Options"
        '== grpBoxPrinter.Text = ""
        labReportName.Text = ""
        labExplain.Text = ""
        labCustomerName.Text = ""
        grpBoxCustomer.Enabled = False

        msComputerName = My.Computer.Name

        msDefaultPrinterName = ""  '== Printer.DeviceName '--  save name of original default printer..-
        cboReportPrinters.Items.Clear()
        '= cboReceiptPrinters.Items.Clear()
        '= Private mSysInfo1 As clsSystemInfo
        '=3411.202=
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        msBusinessName = mSysInfo1.item("BUSINESSNAME")

        panelPeriodOpts.Enabled = False
        panelPeriodFromTo.Enabled = False

        '=3300.428= Call mbLoadSettings()
        msSettingsPath = gsLocalSettingsPath() '= "JobMatix33" default.
        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName In colPrinters
                cboReportPrinters.Items.Add(sName)
                '= cboReceiptPrinters.Items.Add(sName)
            Next sName
            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_reportPrtSettingKey) AndAlso _
                         mLocalSettings1.item(k_reportPrtSettingKey) <> "" Then
                '== gbQueryLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, s1) AndAlso (s1 <> "") Then
                s1 = mLocalSettings1.item(k_reportPrtSettingKey)
                If colPrinters.Contains(s1) Then '--set it- 
                    cboReportPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboReportPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboReportPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
            '-receipt-
            'If mLocalSettings1.exists(k_receiptPrtSettingKey) AndAlso _
            '        (mLocalSettings1.item(k_receiptPrtSettingKey) <> "") Then
            '    s1 = mLocalSettings1.item(k_receiptPrtSettingKey)
            '    '= gbQueryLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, s1) AndAlso (s1 <> "") Then
            '    If colPrinters.Contains(s1) Then '--set it-
            '        cboReceiptPrinters.SelectedItem = s1
            '    Else
            '        If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
            '    End If
            'Else '-no pref-
            '    If (msDefaultPrinterName <> "") Then cboReceiptPrinters.SelectedItem = msDefaultPrinterName
            'End If  '-query receipt prt.--

        End If '-getAvail.-  
        msReportPrinterName = cboReportPrinters.SelectedItem
        cboReportPrinters.Enabled = True

        chkNoReportIfZeroStock.Checked = True '-Excludes zero stock if checked..
        chkNoReportIfNegStock.Checked = True

        '==  Target-New-Build-4257..  07-July-2020.
        '==  Target-New-Build-4257..  07-July-2020.
        chkNoReportIfPosStock.Visible = False
        chkNoReportIfPosStock.Checked = False
        '== END Target-New-Build-4257..  07-July-2020.

        optCat1Description.Checked = True    '=3411.0201=

        '-- set root fonts..
        Dim nodex, nodeResult() As TreeNode
        nodeResult = tvwReports.Nodes.Find("sales", True)
        If (nodeResult.Length > 0) Then  '--found..-
            nodex = nodeResult(0)  '==.Item(sKey)
            If Not (nodex Is Nothing) Then
                nodex.NodeFont = fontRoot
            End If
        End If '--length-
        nodeResult = tvwReports.Nodes.Find("payments", True)
        If (nodeResult.Length > 0) Then  '--found..-
            nodex = nodeResult(0)  '==.Item(sKey)
            If Not (nodex Is Nothing) Then
                nodex.NodeFont = fontRoot
            End If
        End If '--length-
        nodeResult = tvwReports.Nodes.Find("stock", True)
        If (nodeResult.Length > 0) Then  '--found..-
            nodex = nodeResult(0)  '==.Item(sKey)
            If Not (nodex Is Nothing) Then
                nodex.NodeFont = fontRoot
            End If
        End If '--length-
        labStaffName.Text = msStaffName
        labDLLversion.Text = msVersionPOS
        Me.Text = "Point of Sale Reports-"
        '-- Customer --

        mColPrefsCustomer = New Collection
        mColPrefsCustomer.Add("barcode")
        mColPrefsCustomer.Add("lastname")
        mColPrefsCustomer.Add("firstname")
        mColPrefsCustomer.Add("companyName")
        mColPrefsCustomer.Add("phone")
        mColPrefsCustomer.Add("mobile")
        mColPrefsCustomer.Add("isAccountCust")
        mColPrefsCustomer.Add("creditLimit")
        mColPrefsCustomer.Add("pricingGrade")
        mColPrefsCustomer.Add("openedStaff_id")
        mColPrefsCustomer.Add("openedStaffname")
        mColPrefsCustomer.Add("inactive")
        mColPrefsCustomer.Add("address")
        '==mColPrefsCustomer.Add("addr2")
        '=mColPrefsCustomer.Add("addr3")
        mColPrefsCustomer.Add("suburb")
        mColPrefsCustomer.Add("email")
        mColPrefsCustomer.Add("customer_id")
        mColPrefsCustomer.Add("date_modified")
        mColPrefsCustomer.Add("comments")

        '=3519.0128=--load distinct cat values..
        If Not mbLoadCategories(mColCategoriesTree) Then
            '= Me.Close()
            Exit Sub
        End If
        '- load combo-
        cboSelectCat1.Items.Clear()
        cboSelectCat2.Items.Clear()

        cboSelectCat1.Items.Add("-All-")
        Dim col1, col2 As Collection
        For Each col1 In mColCategoriesTree
            '= MsgBox(col1.Item(1))
            cboSelectCat1.Items.Add(col1.Item("cat1name"))
            col2 = col1.Item("cat2children")  '-test=
        Next col1
        cboSelectCat1.SelectedIndex = 0   '--All is default.

        '-- Relevant Cat2 items are loaded when Cat1 selected.
        cboSelectCat2.Items.Add("-All-")
        cboSelectCat2.SelectedIndex = 0   '--All is default.

        grpBoxCategory.Enabled = False

        TabControlProduct.Enabled = False


        '==
        '==   Target-New-Build-4282 --  (22-October-2020)
        '==   Target-New-Build-4282 --  (22-October-2020)
        '==
        '==  -- For POS Sales Reports-  Incorporate  Dropdown to select Staff Sales...
        '==
        '--  Get list of staff who have sales in last three years..
        '--     Load Staff Combo for selection..
        Dim dtStaffList As DataTable
        Dim sSqlStaff As String
        Dim sId As String

        sSqlStaff = "SELECT staff_id, barcode, docket_name FROM staff "
        sSqlStaff &= "  WHERE staff_id IN "
        sSqlStaff &= " (SELECT DISTINCT [staff_id] "

        '==   Target-New-Revision-4282.1102 --  (02-November-2020)
        '==     -- Fix PosReports (Load Event) to get rid of "ClearwaterJT" DB name out of Staff Quuery..
        '---sSqlStaff &= " FROM [ClearwaterJT_jmpos].[dbo].[Invoice] "
        sSqlStaff &= " FROM [dbo].[Invoice] "
        '== END  Target-New-Revision-4282.1102 --  (02-November-2020)
        sSqlStaff &= "    WHERE (DATEDIFF ( month ,invoice_date , getdate() ) <=36) );"

        cboStaff.Items.Clear()
        cboStaff.Items.Add(" All Staff-")

        If Not gbGetDataTable(mCnnSql, dtStaffList, sSqlStaff) Then
            MsgBox("Error in SELECT Staff Invoice table: " & vbCrLf &
                                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        Else
            '-ok-
        End If
        '-- load staff combo.
        If (dtStaffList IsNot Nothing) Then
            For Each datarowStaff As DataRow In dtStaffList.Rows
                sId = RSet(datarowStaff.Item("staff_id"), 3)
                '==   Target-New-Revision-4282.1102 --  (02-November-2020)
                '== cboStaff.Items.Add(sId & ". " & datarowStaff.Item("docket_name"))
                s1 = Trim(Replace(datarowStaff.Item("docket_name"), ".", " "))  '-Can't have dots in name.
                If (s1 <> "") Then
                    cboStaff.Items.Add(LSet(s1, 8) & ". " & sId)
                End If
                '== END  Target-New-Revision-4282.1102 --  (02-November-2020)

            Next datarowStaff
        End If  '-nothing-
        cboStaff.SelectedIndex = 0  '-- -All- is default..
        '== END  Target-New-Build-4282 --  (22-October-2020)
        '== END  Target-New-Build-4282 --  (22-October-2020)


        '== Target 6201- Updating 12-July-2021..
        '==    Updates For Target OpenSource version Build 6201...
        '==  Add features to Product Sales and What's In Stock to filter for a particular stock Supplier..
        '= = = = =

        '--  Get list of supplier who have sales in last three years..
        '--     Load supplier sales Combo for selection..
        '==  ALSO-  Load combo sor Supplier Stock Report.

        Dim dtSupplierList As DataTable
        Dim sSqlSupplier As String
        '=Dim sId As String

        sSqlSupplier = "SELECT supplier_id, barcode, supplierName FROM supplier "
        sSqlSupplier &= "  WHERE supplier_id IN "
        sSqlSupplier &= " (SELECT DISTINCT [supplier_id] "

        sSqlSupplier &= " FROM [dbo].[InvoiceLine] "
        sSqlSupplier &= "  JOIN [dbo].[Invoice] ON (invoice.invoice_id=invoiceLine.invoice_id) "
        '== END  Target-New-Revision-4282.1102 --  (02-November-2020)
        sSqlSupplier &= "    WHERE (DATEDIFF ( month, invoice_date , getdate() ) <=36) );"

        cboSupplierSales.Items.Clear()
        cboSupplierSales.Items.Add(" All Suppliers-")

        cboSupplierStock.Items.Clear()
        cboSupplierStock.Items.Add(" All Suppliers-")

        If Not gbGetDataTable(mCnnSql, dtSupplierList, sSqlSupplier) Then
            MsgBox("Error in SELECT Supplier Invoice table: " & vbCrLf &
                                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
        Else
            '-ok-
        End If
        '-- load supplier sales combo.
        If (dtSupplierList IsNot Nothing) Then
            For Each datarowSupplier As DataRow In dtSupplierList.Rows
                sId = RSet(datarowSupplier.Item("supplier_id"), 3)
                '== cboStaff.Items.Add(sId & ". " & datarowStaff.Item("docket_name"))
                s1 = Trim(Replace(datarowSupplier.Item("supplierName"), ".", " "))  '-Can't have dots in name.
                If (s1 <> "") And (datarowSupplier.Item("supplier_id") > 0) Then
                    cboSupplierSales.Items.Add(VB.Left(s1, 32) & ". " & sId)
                    cboSupplierStock.Items.Add(VB.Left(s1, 32) & ". " & sId)
                End If
            Next datarowSupplier
        End If  '-nothing-
        cboSupplierSales.SelectedIndex = 0  '-- -All- is default..
        cboSupplierStock.SelectedIndex = 0  '-- -All- is default..



        '== END Target 6201- Updating 12-July-2021..
        '==    Updates For Target OpenSource version Build 6201...
        '==  Add features to Product Sales and What's In Stock to filter for a particular stock Supplier..
        '= = = = =





        '= Call CenterForm(Me)
        mbStartingUp = False

    End Sub  '--load--
    '= = = = = = = = =
    '-===FF->

    '--Form Activated Event --

    'Private Sub frmPOS3Reports_Activated(ByVal sender As System.Object, _
    '                                        ByVal e As System.EventArgs) Handles MyBase.Activated

    '    If mbActivated Then Exit Sub '-- do once only..--
    '    mbActivated = True

    '    'If mbTillBalanceOnly Then

    '    '    '--THIS for PROD.. --
    '    '    '-- Till balance is always for TODAY (since midnight.).
    '    '    optPeriodToday.Checked = True

    '    '    '== Call mbCashupAnalysis()
    '    '    If btnPrintSummary.Enabled Then
    '    '        Call btnPrintSummary_Click(New Button, New EventArgs)
    '    '    End If
    '    '    Me.Close()
    '    '    Exit Sub
    '    'End If

    '    '= mbStartingUp = False
    'End Sub  '-Activated.-
    '= = = = = = = = = = =  =

    'Private Sub frmPOS3Reports_Shown(ByVal sender As System.Object, _
    '                                    ByVal e As System.EventArgs) Handles MyBase.Shown

    '    mbStartingUp = False

    'End Sub  '--shown-
    '= = = = = = =  = == =
    '-===FF->

    Private Sub frmPOS3Reports_resize(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles Me.Resize

        '--  cant make smaller than original..-
        If (Me.Height < mIntFormDesignHeight) Then Me.Height = mIntFormDesignHeight
        If (Me.Width < mIntFormDesignWidth) Then Me.Width = mIntFormDesignWidth

        panelHdr.Width = Me.Width - 9
        btnExit.Left = panelHdr.Width - btnExit.Width - 7

        dgvReport.Width = panelHdr.Width
        dgvReport.Height = Me.Height - panelHdr.Height - 30
        labDLLversion.Top = dgvReport.Top + dgvReport.Height + 3  '= Me.Height - 20

        '= btnExit.Top = Me.Height - 58
        '= btnExit.Left = Me.Width - 100

    End Sub  '-resize.-
    '= = = = = = = = = = = = = ==
    '-===FF->

    '--TreeView Node Click--
    '-- TreeView Node Click--

    Private Sub tvwReports_NodeClick(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.Windows.Forms.TreeNodeMouseClickEventArgs) _
                                            Handles tvwReports.NodeMouseClick
        Dim nodeX As System.Windows.Forms.TreeNode = eventArgs.Node
        Dim sKey, sCnn As String
        Dim s1 As String
        '== Dim sStatus As String
        Dim lngJobId As Integer
        '== Dim bToNotify As Boolean
        '= Call mbSelectReport(sKey)

    End Sub '--node click..
    '= = = = = = = = = = = =

    '==== Catch selection by Arrow keys..-

    Private Sub tvwReports_AfterSelect(ByVal sender As System.Object, _
                                          ByVal EventArgs As System.Windows.Forms.TreeViewEventArgs) _
                                             Handles tvwReports.AfterSelect
        Dim nodeX As System.Windows.Forms.TreeNode = EventArgs.Node
        Dim sKey, sCnn As String
        Dim s1 As String
        ' Vary the response depending on which TreeViewAction
        ' triggered the event. 
        Select Case (EventArgs.Action)
            Case TreeViewAction.ByKeyboard
                '== MessageBox.Show("You like the keyboard!")
            Case TreeViewAction.ByMouse
                '== MessageBox.Show("You like the mouse!")
        End Select
        sKey = LCase(nodeX.Name)
        Call mbSelectReport(sKey)
        '--MsgBox sKey + vbCrLf + "was clicked..", vbInformation

    End Sub  '-tvwReports_AfterSelect-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- R e p o r t  O p t i o n s  -

    '==  Target-New-Build-4253..
    '==  Target-New-Build-4253..

    '-chkSalesSummaryOnly is exclusive with ShowInvoice Lines.

    Private Sub chkSalesSummaryOnly_CheckedChanged(sender As Object, e As EventArgs) _
                                                 Handles chkSalesSummaryOnly.CheckedChanged, _
                                                              chkInvoicesDiscountedOnly.CheckedChanged, _
                                                                 chkShowInvoiceLines.CheckedChanged
        Dim chkBox1 As CheckBox = CType(sender, CheckBox)

        If chkBox1.Checked Then
            If (LCase(chkBox1.Name) = "chksalessummaryonly") Then
                chkShowInvoiceLines.Checked = False   '-can't have this as well.

            ElseIf (LCase(chkBox1.Name) = "chkshowinvoicelines") Then
                chkSalesSummaryOnly.Checked = False
            End If  '-summary-
        End If  '-checked..

    End Sub '-chkSalesSummaryOnly_CheckedChanged-
    '= = = = = = = = = = == = =  = = = = = = = = 
    '-===FF->


    '-- Option Sales/cashup Analysis/Listing -
    '-- Option Product Sales (Period) -
    '-- Option Stock--
    'Private Sub optReportSales_CheckedChanged(ByVal sender As Object, _
    'ByVal e As EventArgs)
    '    Dim opt1 As RadioButton = CType(sender, RadioButton)

    '    If mbStartingUp Then Exit Sub
    '    If opt1.Checked Then
    '        msReportName = opt1.Name
    '        '== Call mbSetupReport()
    '    End If
    'End Sub '-SalesToday-
    '= = = = = = = = = = = =

    '-- R e p o r t  O p t i o n s  -
    '--  Choosing Cat1/Cat2...

    '-- cboSelectCat1_SelectedIndexChanged-

    Private Sub cboSelectCat1_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                       Handles cboSelectCat1.SelectedIndexChanged
        Dim colCat2List As Collection

        '-- reload Cat2  list..-
        cboSelectCat2.Items.Clear()
        cboSelectCat2.Items.Add("-All-")

        If (cboSelectCat1.SelectedIndex >= 0) Then
            Dim sCat1 As String = cboSelectCat1.SelectedItem
            If sCat1 <> "-All-" Then
                If mColCategoriesTree.Contains(sCat1) Then
                    colCat2List = mColCategoriesTree(sCat1)("cat2Children")
                    For Each s1 As String In colCat2List
                        cboSelectCat2.Items.Add(s1)
                    Next '-s1-
                    '=4201.0831=
                    If cboSelectCat2.Items.Count > 0 Then
                        cboSelectCat2.Enabled = True
                        cboSelectCat2.SelectedIndex = 0
                    End If
                Else
                    '--not in collection-
                    MsgBox("Error..  can't match that. ", MsgBoxStyle.Exclamation)
                End If  '-contains-
            End If  '-all-
        End If  '-index-
    End Sub  '-cboSelectCat1-
    '= = = = = = = = = = = = = = = =  

    '-- Cat2-

    Private Sub listSelectCat2_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                     Handles cboSelectCat2.SelectedIndexChanged
        '= btnCatSelectOk.Enabled = True
    End Sub '-listSelectCat2-
    '= = = = = = = = = = = = = ==  = =
    '-===FF->


    '-- R e p o r t  O p t i o n s  -
    '--  Choosing Customer..

    Private mIntLookupCustomer_id As Integer = -1

    '-- btnCustomerLookup-

    Private Sub btnCustomerLookup_Click(sender As Object, e As EventArgs) _
                                                   Handles btnCustomerLookup.Click
        Dim colPrefs, colKeys As Collection
        Dim colSelectedRow As Collection
        Dim intCustomer_id As Integer
        Dim sCustomer As String
        Dim sName As String

        intCustomer_id = -1
        mIntLookupCustomer_id = -1
        sCustomer = ""
        '=3301.816=
        colPrefs = mColPrefsCustomer   '== New Collection

        If Not mbBrowseTable(colPrefs, "Lookup Customer", "", colKeys, colSelectedRow, "customer", True) Then
            MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
        Else '-ok-
            If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then
                '== MsgBox("Selected : " & colKeys(1))
                intCustomer_id = CInt(colKeys(1))  '--save fkey as data..
                mIntLookupCustomer_id = intCustomer_id
                sName = colSelectedRow.Item("companyName")("value")
                If (sName = "") Then
                    sName = colSelectedRow.Item("lastName")("value") & ", " & colSelectedRow.Item("firstName")("value")
                End If
                labCustomerName.Text = sName
            End If  '--keys- 
        End If  '--browse-

    End Sub '-btnCustomerLookup-
    '= = = = = = = = = = = = = = =

    '-chkAllCustomers-

    Private Sub chkAllCustomers_CheckedChanged(sender As Object, e As EventArgs) _
                                                                 Handles chkAllCustomers.CheckedChanged
        If chkAllCustomers.Checked Then
            labCustomerName.Text = ""
            mIntLookupCustomer_id = -1
        End If
    End Sub  '-chkAllCustomers-
    '= = = = = = = = = = = = == 
    '-===FF->

    '-- CHECKBOX today only--

    Private Sub chkToday_CheckedChanged(ByVal sender As Object, _
    ByVal e As EventArgs)

        '== If chkToday.Checked Then
        '== DTPickerFrom.Value = Today
        '== DTPickerTo.Value = Today
        '== End If
    End Sub  '--today--
    '= = = = = = = = = = = = =


    Private Sub optPeriodToday_CheckedChanged(sender As Object, e As EventArgs) _
                                             Handles optPeriodToday.CheckedChanged, optperiodThisMonth.CheckedChanged, _
                                                 optPeriod12Months.CheckedChanged, optPeriodSelect.CheckedChanged
        panelPeriodFromTo.Enabled = False
        If optPeriodToday.Checked Then
            DTPickerFrom.Value = Today
            DTPickerTo.Value = Today

        ElseIf optperiodThisMonth.Checked Then
            DTPickerFrom.Value = DateAdd("d", -(Today.Day - 1), Today) '--start at 1st day of this month..-
            DTPickerTo.Value = Today.Date                                 '-- end date is today.-
        ElseIf optPeriod12Months.Checked Then
            '=4233.0421=  DTPickerFrom.Value = DateAdd("d", -(366), Today) 
            '==  Minus 1 year plus 1 day..
            DTPickerFrom.Value = DateAdd("d", +(1), _
                           DateAdd("yyyy", -(1), Today)) '--start at 1st day of a year ago.-
            '--started at 1st day of a year ago.- PLUS one day.
            DTPickerTo.Value = Today.Date    '-- end date is today.-
        ElseIf optPeriodSelect.Checked Then
            panelPeriodFromTo.Enabled = True
        Else  '-nothing

        End If
    End Sub '-optPeriods--
    '= = = = = = = = = = = = 


    Private Sub btnRefresh_Click(ByVal sender As Object, _
                                    ByVal e As EventArgs) Handles btnRefresh.Click

        If (msReportName <> "") Then
            Call mbSetupReport()  '-set up and run analysis..
        End If
 
    End Sub '-refresh-
    '= = =  = = = == == =
    '-===FF->


    Private Sub cboReportPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
                                               ByVal e As System.EventArgs) Handles cboReportPrinters.SelectedIndexChanged

        If (cboReportPrinters.SelectedIndex >= 0) Then
            msReportPrinterName = cboReportPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_reportPrtSettingKey, msReportPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, msReportPrinterName) Then
                MsgBox("Failed to save invoice printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-

    End Sub  '- selected index-
    '= = = = = = = = = = = = = = =

    'Private Sub cboReceiptPrinters_SelectedIndexChanged(ByVal sender As System.Object, _
    '                                                ByVal e As System.EventArgs)

    '    '= Dim sName As String
    '    If (cboReceiptPrinters.SelectedIndex >= 0) Then
    '        msReceiptPrinterName = cboReceiptPrinters.SelectedItem
    '        If Not mLocalSettings1.SaveSetting(k_receiptPrtSettingKey, msReceiptPrinterName) Then
    '            '= gbSaveLocalSetting(gsLocalSettingsPath, k_receiptPrtSettingKey, msReceiptPrinterName) Then
    '            MsgBox("Failed to save Receipt printer setting.", MsgBoxStyle.Information)
    '        End If
    '    End If '-index-
    'End Sub '-ReceiptPrinters-
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '-- PRINT --
    '-- PRINT --
    '==   Updated.- 3519.0404  Started 30-March-2019= 
    '==    -- Reports- DON'T use PrintDataGridView Function- 
    '==                 INSTEAD, Convert to user Standard clsReportPrinter.

    Private Sub btnPrintReport_Click(ByVal sender As Object,
                                        ByVal e As EventArgs) Handles btnPrintReport.Click
        Dim prtDocs1 As clsPrintSaleDocs

        If (cboReportPrinters.Items.Count > 0) AndAlso (cboReportPrinters.SelectedIndex >= 0) Then
            '-- load prt object-
            prtDocs1 = New clsPrintSaleDocs
            prtDocs1.versionPOS = msVersionPOS
            '== prtDocs1.PrtSelectedPrinter = mPrtReceipt
            prtDocs1.PrtSelectedPrinterName = msReportPrinterName   '= msDefaultPrinterName
            prtDocs1.SystemInfo = mSysInfo1
        Else
            MsgBox("No printer selected..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If  '--have printer-

        '-- print data grid..-
        If dgvReport.Visible Then
            If (dgvReport.Rows.Count > 0) Then
                If (mColReportLines IsNot Nothing) Then
                    '- report printer-
                    mColReportLines.Add("<drawline fontstyle=""bold"" />")
                    Dim bPreviewOnly As Boolean = True
                    '-- Confirm preview..
                    If (mColReportLines.Count > 500) Then
                        Dim msgResult As MsgBoxResult =
                              MsgBox("Please Note-  This Report has " &
                                     FormatNumber(mColReportLines.Count, 0) & " lines.." & vbCrLf &
                                     "Bypass the Preview and print on " & msReportPrinterName & " ??",
                                                       MsgBoxStyle.YesNoCancel, MsgBoxStyle.DefaultButton1)
                        If (msgResult = MsgBoxResult.Yes) Then
                            bPreviewOnly = False
                        ElseIf (msgResult = MsgBoxResult.Cancel) Then
                            Exit Sub   '- no more..
                        Else
                            '- show preview..
                            bPreviewOnly = True
                        End If '-bypass preview..
                    End If  '-reportlines-

                    Call clsReportPrint1.PrintStandardReport(bPreviewOnly, msVersionPOS,
                                              mColReportLines, msReportPrinterName,
                                              msBusinessName, labReportName.Text,
                                               Color.SaddleBrown,
                                               "Stock Sales " & vbCrLf &
                                                       msReportPeriod,
                                                " By Product Line.. ", msReportColumnHdr1, msReportColumnHdr2)
                    '                                "  Item Description" & Space(32) & _
                    '                                    " Cat1/Cat2 " & Space(16) & "    Barcode  " & Space(16) & _
                    '                                    "   Cost-Ex   Sell_Ex " & Space(10) & "   Qty         Ext-Cost      Ext-Sell  ")
                Else '-print grid-
                    '-- go grid-print--
                    If Not prtDocs1.PrintDataGridView(labReportName.Text & ": " & msReportPeriod, dgvReport, labExplain.Text) Then
                        MsgBox("Failed..", MsgBoxStyle.Exclamation)
                    End If
                End If
            Else
                MsgBox("No Grid report to print..", MsgBoxStyle.Exclamation)
            End If  '--rows.

        End If  '--visible-
    End Sub  '--print-
    '= = = = = = = = = = = = = == 


    'Private Sub btnPrintSummary_Click(sender As Object, e As EventArgs)

    '    MsgBox("No summary available.", MsgBoxStyle.Exclamation)

    '    '    Dim prtDocs1 As clsPrintSaleDocs
    '    '    If (cboReportPrinters.Items.Count > 0) AndAlso (cboReportPrinters.SelectedIndex >= 0) Then
    '    '        '-- load prt object-
    '    '        prtDocs1 = New clsPrintSaleDocs
    '    '        prtDocs1.versionPOS = msVersionPOS
    '    '        prtDocs1.PrtSelectedPrinterName = msReceiptPrinterName   '= msDefaultPrinterName
    '    '    Else
    '    '        MsgBox("No printer selected..", MsgBoxStyle.Exclamation)
    '    '    End If  '--have printer-
    '    '    If (Not (mColReceiptLines Is Nothing)) AndAlso (mColReceiptLines.Count > 0) Then
    '    '        '-- go print--
    '    '        If Not prtDocs1.PrintDocket(mColReceiptLines) Then
    '    '            MsgBox("Print Failed..", MsgBoxStyle.Exclamation)
    '    '        End If
    '    '    End If  '-nothing-
    'End Sub  '-print summary-
    '= = = = = = =  = = = = = = =

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


    Private Sub btnExit_Click(ByVal sender As Object, _
                                         ByVal e As EventArgs) Handles btnExit.Click
        '= Me.Close()
        Call close_me()
    End Sub  '-exit--
    '= = = = = = = = = = 


End Class  '- frmPOS3Reports-

'=== end form ==