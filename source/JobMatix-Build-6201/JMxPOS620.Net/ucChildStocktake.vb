Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
'= Imports VB6 = Microsoft.VisualBasic.Compatibility
Imports System.Reflection
Imports System.IO
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb
Imports System.Math
Imports System.ComponentModel
Imports System.Threading
'-- SqlClient needed for sqlBulkCopy class.--
Imports System.Data.SqlClient

Public Class ucChildStocktake  '= frmStocktake

    '==  NEW POS VERSION-  JMxPOS32
    '==
    '==   POS dll-  v3.2.3201.131..  31Jan2016= ===
    '==      >> Start Stocktake-.
    '==          Everyone must re-create new DB..--
    '==
    '==  JmxPOS330.dll  VERSION- 3.3.3301.501  01May2016-
    '==
    '==  3.3.3301.706  06July2016-
    '==    >>  Fixes.  also add Checkbox "Keep leading zeroes"..
    '==
    '==
    '==     v3.3.3301.813..  13-August-2016= ===
    '==       >> Fixes to Stocktake- 
    '==                (autoscrolling, single stocktake, Zero items button moved).
    '==
    '==   Updated.- 3519.0414  Started 12-April-2019= 
    '==    -- StockTake: Add code for btnStockAdmin to show Stock Form. 
    '==          And same for Lookup button.
    '==
    '==   RELEASED as 3519.0414..
    '==
    '==   Updated.- 3519.0501  Started 01-May-2019= 
    '==    -- STOCKTAKE-  "Single" Stocktake now means "Free Range". ie as per Full Stocktake,
    '==            ie., count items at random, then Commit when ready,
    '==            But NO Zeroing those items not counted.
    '==         [ now behaves more like Retailmanager "Single" stocktake.
    '==
    '==   Updated.- 4201.0507= 
    '==    -- STOCKTAKE-  New (FREE-Range) Version migrated from Build-3519.0501-
    '==   Updated.- 4201.0616= 
    '==    -- STOCKTAKE-  Implement Form Resizing-
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==
    '==
    '==  Target-New-Build-4257..
    '==  Target-New-Build-4257..  07-July-2020.
    '==
    '==   3. In Stocktake- negative qtyInStock-   
    '==           -- ADD a checkbox option-  Don't pre-load items with negative qtyInStock balance.
    '--                SO THAT we can load them if needed and can ZERO them if neede..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == = = =
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = == 

    Private Const k_ReportPrtSettingKey As String = "POS_ReportPrinter"

    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    '-- grh JobMatixPOS31  v3.1.3101.1007 -
    Private mbIsInitialising As Boolean = True

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
    Private msBusinessName As String = ""
    '== Private msAppPath As String
    '= Private msLastSqlErrorMessage As String = ""
    Private msDefaultPrinterName As String = ""
    Private msReportPrinterName As String = ""

    '--inputs--

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  POS DB info--

    Private mCnnSql As OleDbConnection   '== SqlConnection '--
    Private msStaffName As String = ""
    Private mIntStaff_id As Integer = -1

    '=3301.428= Private mSdSettings As clsStrDictionary '--  holds local job settings..
    '=3301.428= Private mSdSystemInfo As clsStrDictionary '--  holds system Info Table values at startup..
    Private mSysInfo1 As clsSystemInfo
    Private mLocalSettings1 As clsLocalSettings
    Private msSettingsPath As String = ""
    '- GST-
    Private mDecGST_percentage As Decimal = 10.0

    Private mColCategoriesTree As Collection
    Private msStockServiceChargeCat1 As String = ""

    Private mColPrefColumnsStock As Collection
    '-- For loading/sorting grids.
    '- Stock list now in dataGridView -
    Private mColPrefsResults As Collection
    Private mColPrefsUncounted As Collection

    Private mBrowseResults As clsBrowse3
    Private mBrowseUncounted As clsBrowse3

    Private mIntSelectedRowResults As Integer = -1
    Private mIntSelectedRowUncounted As Integer = -1


    '-- current stocktake happening..
    '-- current stocktake happening..
    '-- current stocktake happening..

    Private mIntStocktake_id As Integer = -1  '=NOT yet Created..-
    Private msStocktakeType As String = ""
    Private mDateCreated As DateTime

    Private msCurrentCat1 As String = ""
    Private mColCurrentCat2 As Collection
    Private msCurrentCat2List As String
    '-- single stocktake- Barcode-
    '=3519.0501= Private msSingleItemBarcode As String = ""

    Private mdateTimeCreated As DateTime

    Private mbIsFullStocktake As Boolean = False
    Private mbIsPartialStocktake As Boolean = False
    '-- FREE RANGEING-
    Private mbIsSingleStocktake As Boolean = False

    '- This set when Commit doene..
    Private mbIsCommitted As Boolean = False

    '- Cache of recently scanned barcodes and their stock Table dataRows..
    Private mColScanCache As Collection
    '-- L0 -  key=barcode, Item = collection of fld/values-
    '--    L1- barcode, stock_id, cat1, cat2, description, expected--   

    Private mColCurrentScanItemDetails As Collection

    '-- Current Result grid index..
    '--  for faster grid updating..-
    '- THESE TWO Arrays GO TOGETHER !! --
    Private maIntCurrentStock_ids() As Integer = {}
    Private maIntCurrentGridRowNos() As Integer = {}

    Private msLastSqlErrorMessage As String
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- DELEGATE for Parent Report..
    Public Delegate Sub subReportDelegate(ByVal strChildName As String, _
                                          ByVal strEvent As String, _
                                          ByVal sText As String)
    '-- This is instantiated from Mdi Parent when form is created.
    Public delReport As subReportDelegate '--    = AddressOf frmPosMainMdi.subChildReport
    '= = = = = = = =  = = = = = = = = = = = = ==
    '-===FF->

    '-mbCanStocktakeClose -

    Private Function mbCanStocktakeClose() As Boolean

        Dim bCancel As Boolean = False

        mbCanStocktakeClose = False  '-no-

        If (mIntStocktake_id > 0) Then  '-started
            If mbIsCommitted Then
                If MsgBox("This stocktake has been committed, and the stock records updated.." & vbCrLf & _
                          "This is your last chance to print the Stocktake Report," & vbCrLf & _
                           " as after exit this stocktake will no longer be accessible." & vbCrLf & vbCrLf & _
                           "OK to exit and finish with this Stocktake ?", _
                           MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = vbYes Then
                    bCancel = False  '--let it exit---
                Else '--chikened out- cancel exit-
                    bCancel = True   '--cant close yet--'--was mistake..  keep going..
                End If  '-yes/no-
            Else  '--no. it's still avlive-
                MsgBox("You can resume this stocktake later..", MsgBoxStyle.Information)
                bCancel = False  '--let it go to close---
            End If
        End If  '-stocktake-

        If Not bCancel Then
            mbCanStocktakeClose = True  '-can close-
        End If

    End Function  '-can close-
    '= = = = == = = 
    '-===FF->

    '-- CLOSING event to signal Mother Form..

    Public Event PosChildClosing(ByVal strChildName As String)
    '= = = = = = = = = = = = = = = = = = = = == = 


    '-FormResized-
    '-- Called from Mdi Mother Resize..

    Public Sub SubFormResized(ByVal intParentWidth As Integer, _
                            ByVal intParentHeight As Integer)
        Me.Width = intParentWidth  '= - 11
        Me.Height = intParentHeight  '= - 11

        '==   Updated.- 4201.0616= 
        '==    -- STOCKTAKE-  Implement Form Resizing-

        panelHdr.Width = Me.Width - panelTopBanner.Width - 5
        btnExit.Left = panelHdr.Width - btnExit.Width - 6

        panelFooter.Left = Me.Width - panelFooter.Width - 7

        TabControlMain.Width = Me.Width - panelFooter.Width - 12
        TabControlMain.Height = Me.Height - panelHdr.Height - 16
        panelFooter.Height = TabControlMain.Height

        grpBoxAuto.Width = TabControlMain.Width - 11
        grpBoxAuto.Height = TabControlMain.Height - 36
        grpBoxScanAuto.Width = grpBoxAuto.Width - 7
        labAutoInfo.Left = grpBoxAuto.Width - labAutoInfo.Width - 12
        btnAutoUndo.Left = labAutoInfo.Left

        dgvAutoCountItems.Height = grpBoxAuto.Height - grpBoxScanAuto.Height - 15
        dgvAutoCountItems.Width = grpBoxAuto.Width - labAutoInfo.Width - 21

        grpBoxManual.Width = TabControlMain.Width - 11
        grpBoxManual.Height = TabControlMain.Height - 36
        grpBoxScanManual.Width = grpBoxManual.Width - 5

        TabControlResults.Width = grpBoxManual.Width - 5
        TabControlResults.Height = (grpBoxManual.Height - grpBoxScanManual.Height - 11)

        frameBrowseResults.Width = TabControlResults.Width - 15
        frameBrowseResults.Height = TabControlResults.Height - 36

        dgvResultsList.Height = frameBrowseResults.Height - 66
        dgvResultsList.Width = frameBrowseResults.Width - 11

        frameBrowseUncounted.Height = TabControlResults.Height - 36
        frameBrowseUncounted.Width = TabControlResults.Width - 15

        dgvUncounted.Width = frameBrowseUncounted.Width - 11
        dgvUncounted.Height = frameBrowseUncounted.Height - 66

        labBottomBar.Top = panelFooter.Height - 22

        DoEvents()
        Me.Invalidate()
        Me.Update()

    End Sub '-resize.-
    '= = = = =  = == = =

    '-Form Close Request-
    '-- Called from Mdi MotherForm Tab-close Click..
    '=- Return true if ok to Close.

    Public Function SubFormCloseRequest() As Boolean

        '=- Return true if ok to Close.
        SubFormCloseRequest = mbCanStocktakeClose()
        '==Me.Close()
        '=Call close_me()
    End Function  '-close-
    '= = = = = = = = = = = == = = =
    '-===FF->

    '-- Add msg to txtReport.text--
    '-- EX frmImport-

    Private Function mbReport(ByVal sMsg As String) As Boolean

        txtReport.AppendText(sMsg & vbCrLf)
        txtReport.Focus()
        txtReport.SelectionStart = txtReport.TextLength
        txtReport.SelectionLength = 0
        '== txtReport.Select()

    End Function  '-report-
    '= = = = = = = = = = = = = = =

    '-- Numeric test..

    Private Function mbIsNumeric(ByVal sInput As String) As Boolean
        mbIsNumeric = False

        If IsNumeric(sInput) Then  '--good start-
            '-  check for "+","-" that pass the isNumeric test, but fail in Sql Server. test.
            If (InStr(sInput, "+") <= 0) AndAlso (InStr(sInput, "+") <= 0) Then
                mbIsNumeric = True
            End If
        End If  '-numeric-
    End Function  '-is numeric-
    '= = = = = = = = = = =  = = = = =
    '-===FF->

    '-BulkCopyConnect-

    Private Function mbBulkCopyConnect(ByRef cnnSqlClient As SqlConnection) As Boolean
        Dim sConnect, sSql, sSqlRM, sSqlPOS As String
        Dim sMsg, sErrorMsg, s1, s2 As String

        mbBulkCopyConnect = False
        sConnect = "Persist Security Info=False;Integrated Security=SSPI; " & _
                  "Initial Catalog=" & msSqlDbName & "; server=" & msServer & "; "

        cnnSqlClient = New SqlConnection
        '==3072== System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        txtReport.Text = "Sql bcp Connecting to Server: " & msServer & vbCrLf

        Try
            cnnSqlClient.ConnectionString = sConnect
            cnnSqlClient.Open()
            sMsg = "BulkCopy Connected ok to Sql Server.." & vbCrLf
            sMsg = sMsg & "   ConnectStr.=" & sConnect & vbCrLf
        Catch ex As Exception
            sMsg = "Failed Connect to Sql Server.." & vbCrLf
            sMsg = sMsg & "Error: " & ex.Message & vbCrLf
            sMsg = sMsg & "connect string=<" & sConnect & ">"
            s2 = sMsg & vbCrLf '== & "SQL-Provider errors are:" & vbCrLf & s1
            '== If gbDebug Then MsgBox(s2, MsgBoxStyle.Critical, "Sql Connect..")
            '== msLastSqlErrorMessage = s2
            If (gsErrorLogPath <> "") Then
                Call gbLogMsg(gsErrorLogPath, s2 & vbCrLf & "-- end of error msg.--")
            End If '--log--
            Call gbLogMsg(gsErrorLogPath, s2 & vbCrLf & "-- end of error msg.--")
            Exit Function
        End Try
        Call mbReport(sMsg)
        Call gbLogMsg(gsErrorLogPath, vbCrLf & sMsg)
        cnnSqlClient.ChangeDatabase(msSqlDbName)

        mbBulkCopyConnect = True

    End Function  '-bulk connect..-
    '= = = = = = = = = = == = =  =
    '-===FF->

    '-- Execute BULK COPY SQLClient Command..--
    '-- Execute BULK COPY SQLClient Command..--
    '-- Execute BULK COPY SQLClient Command..--
    '-- Execute SQL Command..--
    '--  IF in Transaction, RollBack in the event of failure..-

    Private Function mbExecuteSqlClient(ByRef cnnSqlClient As SqlConnection, _
                                     ByVal sSql As String, _
                                     ByVal bIsTransaction As Boolean, _
                                      ByRef sqlTran1 As SqlTransaction) As Boolean
        Dim sqlCmd1 As SqlCommand
        Dim intAffected As Integer
        Dim sMsg, sErrorMsg As String
        Dim sRollback As String = ""

        mbExecuteSqlClient = False
        Try
            sqlCmd1 = New SqlCommand(sSql, cnnSqlClient)
            If bIsTransaction Then
                sqlCmd1.Transaction = sqlTran1
            End If
            '= mCnnSql.ChangeDatabase(msSqlDbName)
            intAffected = sqlCmd1.ExecuteNonQuery()
            mbExecuteSqlClient = True   '--ok--
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
            msLastSqlErrorMessage = sErrorMsg
            '==gbExecuteCmd = False
            MsgBox(sErrorMsg, MsgBoxStyle.Exclamation)
        End Try
    End Function '-mbExecuteSql-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '=3411.0313=
    '-- Browse  table using --
    '--  Separate BROWSE33 FORM, (Includes TEXT SEARCH) and provided sWhere condition)..--
    '----  return colKeys of selected item..--
    '------return false if cancelled..--

    Private Function mbBrowseAndSearchTable(ByRef colPrefs As Collection, _
                                           ByRef sTitle As String, _
                                            ByRef sWhere As String, _
                                            ByRef colKeys As Collection, _
                                            ByRef colSelectedRow As Collection, _
                                         Optional ByVal sTableName As String = "Supplier") As Boolean
        Dim frmBrowse1 As New frmBrowse  '--File: frmBrowse33 --

        mbBrowseAndSearchTable = False
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
        'frmBrowse1.lookupSelection = True

        frmBrowse1.ShowDialog()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        If Not (frmBrowse1.selectedRow Is Nothing) Then '= frmBrowse1.cancelled Then
            '--  get selected record key..--
            colKeys = frmBrowse1.selectedKey
            colSelectedRow = frmBrowse1.selectedRow
            mbBrowseAndSearchTable = True
        End If
        frmBrowse1.Close()
        frmBrowse1.Dispose()

    End Function  '-mbBrowseAndSearchTable-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->
    '--  get current DISTINCT logged-in users...
    '--   User can have multiple sessions..--
    '--  Uses "sp_who":
    '--    in sql server 2000-  defaults to PUBLIC role..
    '--    in Sql Server 2005 needs VIEW ANY DATABASE permission
    '--      and "The public role is granted VIEW ANY DATABASE permission."
    '--        SEE:   http://msdn.microsoft.com/en-us/library/ms175892(v=sql.90).aspx   --

    Private Function mbShowLoggedInUsers(ByRef colWhichUsers As Collection, _
                                          ByRef strUserList As String) As Boolean

        Dim col1 As Collection
        Dim colAllProcesses As Collection
        Dim sLogin, sHost, sItem As String
        Dim sMsg, sDistinctUsers As String

        mbShowLoggedInUsers = False
        sDistinctUsers = ";"
        strUserList = ""
        '== ToolTip1.SetToolTip(labLoggedInUsers, "")
        If Not gbWhoUsing(mCnnSql, msSqlDbName, colAllProcesses) Then
            MsgBox("Failed to get user list.." & vbCrLf & _
                    "Sql cmd was 'exec sp_who'..  " & vbCrLf & vbCrLf & _
                    gsGetLastSqlErrorMessage() & vbCrLf, MsgBoxStyle.Exclamation)
        Else '--ok--
            sMsg = "Current users are: " & vbCrLf & vbCrLf
            colWhichUsers = New Collection
            If (colAllProcesses.Count > 0) Then
                For Each col1 In colAllProcesses
                    sLogin = Trim(col1.Item("LOGINAME"))
                    sHost = Trim(col1.Item("HOSTNAME"))
                    sItem = LCase(sHost & "!" & sLogin)
                    If Not (InStr(sDistinctUsers, sItem & ";") > 0) Then  '--new-
                        sDistinctUsers = sDistinctUsers & LCase(sItem) & ";"
                        sMsg = sMsg & sLogin & " on: " & sHost & ".." & vbCrLf
                        colWhichUsers.Add(col1)
                    End If
                Next col1 '--col1-
                '== labLoggedInUsers.Text = "Who's using JobMatix-" & vbCrLf & _
                '==                                colWhichUsers.Count & " user(s) logged in.."
                '== ToolTip1.SetToolTip(labLoggedInUsers, sMsg)
                strUserList = sMsg
            Else
                '== labLoggedInUsers.Text = vbCrLf & "No User.."
            End If  '--count.-
            Application.DoEvents()
            mbShowLoggedInUsers = True
        End If  '--who--
    End Function  '--show users.-
    '= = = = = = = = = = = = = = =
    '-===FF->


    '-- Set up Categories for PARTIAL stock take..
    '-- Builds collection of cat, with sub-collections of cat2's for each cat1.-
    '-- "SELECT DISTINCT Cat1,Cat2 FROM dbo.Stock ORDER BY Cat1,Cat2; "

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
        If (msStockServiceChargeCat1 <> "") Then  '--filter out service chards.-
            sSql &= " WHERE (cat1 <>'" & msStockServiceChargeCat1 & "') "
        End If
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

    '-- Make WHERE clause to select cat1, cat2--

    Private Function msMakeCategoryClause(ByVal strCat1 As String, _
                                           ByVal strCat2List As String, _
                                            ByRef colCurrentCat2 As Collection) As String
        Dim sWhere, sWhere2 As String

        msMakeCategoryClause = ""
        If strCat1 = "" Then Exit Function

        sWhere = " (cat1='" & strCat1 & "') "
        '-add cat2 if any selected..-
        sWhere2 = ""
        If (Not (colCurrentCat2 Is Nothing)) AndAlso (colCurrentCat2.Count > 0) Then
            For Each sCat2 As String In colCurrentCat2
                If (sWhere2 <> "") Then sWhere2 &= ", "
                sWhere2 &= "'" & gsFixSqlStr(sCat2) & "'"
            Next
            sWhere &= " AND (cat2 IN (" & sWhere2 & "))"
        End If  '-cat2-
        msMakeCategoryClause = sWhere

    End Function  '-msMakeCategoryClause-
    '= = = = = = = = = = = = = = = = =  ==

    '--- INITIALISE StockTakeItem Browser,  --
    '--      for RESULTS and UNCOUNTED Grids..--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse(ByRef browse1 As clsBrowse3, _
                                        ByVal sSelectList As String, _
                                         ByVal sWhere As String, _
                                          ByRef dgv1 As DataGridView, _
                                           ByRef labRecCount As Label, _
                                           ByRef labFind As Label, _
                                           ByRef txtFind As TextBox) As Boolean
        Dim sHostTablename As String

        mbInitialiseBrowse = False
        If browse1 Is Nothing Then
            browse1 = New clsBrowse3 '== clsBrowse22
        End If
        browse1.connection = mCnnSql
        browse1.colTables = mColSqlDBInfo
        browse1.IsSqlServer = True
        browse1.DBname = msSqlDbName

        '--  get table/prefs info for this host..--
        browse1.tableName = "StocktakeItems"  '==sHostTablename

        browse1.UserSelectList = sSelectList
        browse1.WhereCondition = sWhere

        browse1.InitialOrder1 = "cat1"
        browse1.InitialOrder2 = "cat2"
        browse1.InitialOrder3 = "description"

        browse1.DataGrid = dgv1

        '--  pass controls..--
        browse1.showRecCount = labRecCount '--updates rec. retrieval..
        browse1.showFind = labFind '--updates Sort Column display..
        browse1.showTextFind = txtFind '--updates Sort Column display..
        '= sWhere = msMakeStockFilter()  '--service or not..-
        '-- add srch args..
        '== If (sSrchWhereCond <> "") Then
        '== If (sWhere <> "") Then
        '== sWhere &= " AND "
        '= End If
        '== sWhere &= sSrchWhereCond
        '== End If
        browse1.WhereCondition = sWhere
        browse1.PreferredColumns = Nothing  '== using our SELECT-  
        browse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly

        mIntSelectedRowResults = -1
        Try
            browse1.Activate() '-- go..--
            mbInitialiseBrowse = True
        Catch ex As Exception
            MsgBox("Failed to activate Browser object." & vbCrLf & ex.Message)
        End Try
        '== txtFind.Focus()

    End Function '--init-
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- Load (both YES) result grids..=

    '-- For New Stocktake, or resuming..
    '-- dtStocktakeItems has benn loaded from StocktakeItems Table.
    '-- If "qty_counted" <=0 then load into UNCOUNTED grid, else RESULTS grid.

    '==  MUST USE clsBrowse32  !!!!  --

    Private Function mbLoadResultsGrids() As Boolean
        Dim sSqlResultsSelect, sSqlUncountedSelect, sWhere As String

        '-- load Uncounted..
        sSqlUncountedSelect = "SELECT cat1, cat2, Description, Barcode, stock_id, qty_on_record, qty_counted, qty_difference "
        sWhere = " (qty_counted <0) "  '-- "-1" if uncounted  --
        '== sSqlUncountedSelect &= "  ORDER BY cat1,cat2, description; "
        '-- BACK to TWO Grids. Grid..
        Call mbInitialiseBrowse(mBrowseUncounted, sSqlUncountedSelect, sWhere, _
                                      dgvUncounted, labUncountedCount, labUncountedFind, txtUncountedFind)
        '-- Load Results Counted..-
        sSqlResultsSelect = "SELECT cat1, cat2, Description, Barcode, stock_id, qty_on_record, qty_counted, qty_difference "
        '-- show counted items.-
        sWhere = " (qty_counted >=0) "
        Call mbInitialiseBrowse(mBrowseResults, sSqlResultsSelect, sWhere, _
                                      dgvResultsList, labResultsCount, LabResultsFind, txtResultsFind)
        If dgvResultsList.Rows.Count > 0 Then
            btnCommitAll.Enabled = True
        End If
        labCountedCount.Text = dgvResultsList.Rows.Count & "/" & dgvUncounted.Rows.Count

    End Function  '-load result.-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- Print the Results Grid as Discrepancy Report..
    '-- Print the Results Grid as Discreoancy Report..

    Private Function mbPrintReport(ByRef dgvReport As DataGridView) As Boolean

        Dim prtDocs1 As clsPrintSaleDocs
        Dim strReportHeader, strSubHeader, strSubHeader2 As String

        strReportHeader = msBusinessName & ": Stocktake #" & mIntStocktake_id & _
                            "  (" & msStocktakeType & ")  Discrep. Report- " & Format(Now, "dd-MMM-yyyy HH:mm") & "."

        strSubHeader = "  Cat1: " & msCurrentCat1 & ";  Cat2: " & msCurrentCat2List
        strSubHeader2 = "  -- Costs include GST " & CStr(mDecGST_percentage) & "%.."
        If (cboPrinters.Items.Count > 0) AndAlso (cboPrinters.SelectedIndex >= 0) Then
            '-- load prt object-
            prtDocs1 = New clsPrintSaleDocs
            prtDocs1.versionPOS = msVersionPOS
            '== prtDocs1.PrtSelectedPrinter = mPrtReceipt
            prtDocs1.PrtSelectedPrinterName = msReportPrinterName   '= msDefaultPrinterName
        Else
            MsgBox("No printer selected..", MsgBoxStyle.Exclamation)
        End If  '--have printer-

        '-- print data grid..-
        If dgvReport.Visible Then
            If (dgvReport.Rows.Count > 0) Then
                '-- go print--
                If Not prtDocs1.PrintDataGridView(strReportHeader, dgvReport, strSubHeader, strSubHeader2) Then
                    MsgBox("Failed..", MsgBoxStyle.Exclamation)
                End If
            Else
                MsgBox("No Grid report to print..", MsgBoxStyle.Exclamation)
            End If  '--rows.
        End If  '--visible-
    End Function '-print report-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- Do Bulk  Copy --
    '-- Do Bulk  Copy --

    Private Function mbDoBulkCopy(ByRef bulkCopy1 As SqlBulkCopy, _
                                   ByRef dtPOS As DataTable, _
                                   ByRef sDestTableName As String) As Boolean
        Dim sMsg As String

        mbDoBulkCopy = False
        mbReport("Bulk Copying " & CStr(dtPOS.Rows.Count) & " rows..")
        sMsg = "Bulk copying " & dtPOS.Rows.Count & " " & sDestTableName & " rows to server."
        Call mbReport(sMsg)
        Call gbLogMsg(gsErrorLogPath, sMsg)
        bulkCopy1.DestinationTableName = "dbo." & sDestTableName  '= "dbo.staff"
        Try
            ' Write from the source to the destination.
            '= bulkCopy.WriteToServer(newProducts)
            bulkCopy1.WriteToServer(dtPOS)
            sMsg = "BulkCopy- all " & sDestTableName & " rows done.."
            Call mbReport(sMsg)
            Call gbLogMsg(gsErrorLogPath, sMsg)
        Catch ex As Exception
            sMsg = "ERROR in " & sDestTableName & " table BulkCopy.. " & vbCrLf & vbCrLf & _
                                                                   ex.Message
            Call gbLogMsg(gsErrorLogPath, sMsg)
            MsgBox(sMsg, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        mbDoBulkCopy = True

    End Function  '-mbDoBulkCopy-
    '= = = = = = = = = = = =  = ==  
    '-===FF->

    '-- Bulk Copy event to notify rows copied..

    Private Sub OnSqlRowsCopied(ByVal sender As Object, ByVal args As SqlRowsCopiedEventArgs)

        mbReport("" & args.RowsCopied.ToString())

    End Sub  '--rows copied-
    '= = = = = = = = = = = = = = = =  == 

    '-mbInitialiseStocktake-

    '-- I n i t i a l i s e  Stocktake (TRANSACTION)-..
    '-  Creates a bunch of [StocktakeItems] records to track the Stocktake..  

    '-- FIRST Make new BulkCopy (sqlClient) connection.
    '--
    '--  1.  Insert new Stocktake record.. and GET IDENTITY (stocktake_id)--
    '-   2. Get recordset of Target Stock Items FROM Stock Table...
    '--            (SELECT .. 1 AS item_id, 0 AS counted, 
    '--                  cstr(intStockTakeId) AS stocktake_id,   
    '--                         qtyInStock AS expected, 0 AS difference)  
    '--         (WHERE as per Full S/T, or Partial mColCategoriesTree )-
    '--            
    '--  3.  BULK Insert Items under new Stocktake_id..
    '--          (under TRANSACTION)--
    '--  4.  COMMIT.--
    '-------------------------
    '--  Bulk Copy Stuff..  compliments frmImport..-
    '------------------------------------------

    Private Function mbInitialiseStocktake(ByVal strStocktakeType As String, _
                                               ByVal strFirstItemBarcode As String, _
                                               ByVal strCat1 As String, _
                                                ByVal strCat2List As String, _
                                                ByRef colCurrentCat2 As Collection) As Boolean
        Dim sSql, sBarcode, sErrorMsg, sMsg As String
        Dim sWhere As String
        Dim dtItems As DataTable
        Dim itemRow1 As DataRow
        Dim cnnSqlClient As SqlConnection  '--Bulk Copy Connection.--
        Dim sqlTran1 As SqlTransaction  '-can be nothing-
        Dim intStocktake_id, intAffected As Integer

        mbInitialiseStocktake = False
        If Not mbBulkCopyConnect(cnnSqlClient) Then
            MsgBox("Failed to open BulkCopy Sql Connection.. ", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        '=Dim bulkCopy1 As SqlBulkCopy = _
        '=      New SqlBulkCopy(cnnSqlClient, _
        '=            SqlBulkCopyOptions.KeepIdentity + SqlBulkCopyOptions.KeepNulls, sqlTran1)

        '- FIRST, start TRANSACTION..-
        sqlTran1 = cnnSqlClient.BeginTransaction

        '- CREATE bulkCopy- NO keeping IDENTITIES .._
        Dim bulkCopy1 As SqlBulkCopy = New SqlBulkCopy(cnnSqlClient, _
                                              SqlBulkCopyOptions.KeepNulls, sqlTran1)
        AddHandler bulkCopy1.SqlRowsCopied, AddressOf OnSqlRowsCopied

        '--1. Create NEW Stocktake Base record..
        sSql = "INSERT INTO dbo.stocktake ("
        sSql &= "  stocktake_type, cat1, cat2list, created_staff_name   "
        sSql &= ") "
        sSql &= "VALUES ( '" & gsFixSqlStr(strStocktakeType) & "', "
        sSql &= "'" & gsFixSqlStr(strCat1) & "', "
        sSql &= "'" & gsFixSqlStr(strCat2List) & "', "
        sSql &= "'" & gsFixSqlStr(msStaffName) & "' "
        sSql &= "); "
        If Not mbExecuteSqlClient(cnnSqlClient, sSql, True, sqlTran1) Then
            mbReport("Saving Stocktake record FAILED..")
            cnnSqlClient.Close()
            Exit Function
        End If  '--exec -

        '- 3. Retrieve stocktake no. (IDENTITY of record written.)-
        sSql = "SELECT CAST(IDENT_CURRENT ('dbo.stocktake') AS int);"
        Dim cmd As New SqlCommand(sSql, cnnSqlClient)
        cmd.Transaction = sqlTran1
        Try
            intStocktake_id = Convert.ToInt32(cmd.ExecuteScalar())
            mIntStocktake_id = intStocktake_id  '--added 3301.501-
        Catch ex As Exception
            MsgBox("Failed to get stocktake_id.." & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Function
        End Try
        '= MsgBox("stocktake-id is: " & intStocktake_id, MsgBoxStyle.Information)

        labStockTakeId.Text = "StockTakeId: " & intStocktake_id



        '==  Target-New-Build-4257..  07-July-2020.
        '==  Target-New-Build-4257..  07-July-2020.
        '==
        '==   3. In Stocktake- negative qtyInStock-   
        '==           -- ADD a checkbox option-  Don't pre-load items with negative qtyInStock balance.
        '==
        '--  chkDoNotPreLoadNegItems-  if checked, do not include neg. stock bals..

        '=sWhere = "WHERE (qtyInStock>0) "
        If chkDoNotPreLoadNegItems.Checked Then
            sWhere = "WHERE (qtyInStock>0) "
        Else
            '- load neg bals as well.  So we can ZERO them if needed..
            sWhere = "WHERE (qtyInStock <>0) "
        End If
        '== END  Target-New-Build-4257..  07-July-2020.



        '--ColCurrentCat2 has cat2 selections if any..
        '--         (WHERE as per Full S/T, or Partial mColCategoriesTree )-

        '== sWhere2 = ""
        If (LCase(strStocktakeType) = "single") Then
            '- Free Range loads all, just like "Full"
            '=3519.0501= msSingleItemBarcode = strFirstItemBarcode
            '=3519.0501= sWhere &= " AND (barcode='" & strFirstItemBarcode & "')"
        ElseIf (LCase(strStocktakeType) = "partial") And (strCat1 <> "") Then  '-can select cats..-
            sWhere &= " AND " & msMakeCategoryClause(strCat1, strCat2List, colCurrentCat2)
        End If
        '== sWhere &= "; "
        If (msStockServiceChargeCat1 <> "") Then  '--filter out service chards.-
            sWhere &= " AND (cat1 <>'" & msStockServiceChargeCat1 & "') "
        End If
        '==3301.706=  Ignore Non-Stockable items..-
        sWhere &= " AND (isNonStockItem=0) "  '--collect stockable items only.

        '--  get Selected stock subset for this Stocktake..
        '-- Make dataTable cols congruent with target StocktakeItems table..

        sSql = "SELECT 1 AS item_id, " & CStr(intStocktake_id) & " AS stocktake_id,  "
        sSql &= " stock_id, barcode, cat1, cat2, description, "
        sSql &= "  qtyInStock AS qty_on_record, -1 AS qty_counted, 0 AS qty_difference, "
        sSql &= " CURRENT_TIMESTAMP AS date_modified "
        sSql &= " FROM dbo.stock " & sWhere & " ORDER BY cat1, cat2, description; "
        Dim cmdSelect As New SqlCommand(sSql, cnnSqlClient)
        cmdSelect.Transaction = sqlTran1
        Dim reader1 As SqlDataReader '=== Command.ExecuteReader()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            reader1 = cmdSelect.ExecuteReader
            '--ok.  make datatable. even if empty.
            dtItems = New DataTable
            dtItems.Load(reader1)
            reader1.Close()
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            If (dtItems.Rows.Count <= 0) Then '= AndAlso (LCase(strStocktakeType) <> "single") Then  '-empty-
                sMsg = "No matching non-zero stock items on file for categories: " & vbCrLf & _
                        "Cat1: " & strCat1 & ";  Cat2: " & strCat2List & vbCrLf & vbCrLf
                If (LCase(strStocktakeType) <> "partial") Then  '-must be full-
                    sMsg = "No non-zero stock items on file." & vbCrLf & vbCrLf
                End If
                If Not MsgBox(sMsg & " Do you want to continue with this count anyway ? ", _
                          MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    sqlTran1.Rollback()
                    cnnSqlClient.Close()
                    Exit Function
                Else  '-ok. keep going.
                End If '-yes/no-
            End If
        Catch ex As Exception
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Error- failed to get stocktake Initial Stock List.." & vbCrLf & _
                           ex.Message & vbCrLf & "SQL was: " & vbCrLf & sSql, MsgBoxStyle.Exclamation)
            sqlTran1.Rollback()
            cnnSqlClient.Close()
            Exit Function
        End Try
        '= MsgBox("ok.. We loaded " & dtItems.Rows.Count & "  stock items..", MsgBoxStyle.Information)

        '--4.  BulkCopy Translated RM data (dtPOS) to Actual StockTakeItems Table..
        '-- EMPTY the actual table first..
        sSql = "DELETE FROM dbo.StocktakeItems;"
        Dim cmdExec As New SqlCommand(sSql, cnnSqlClient)
        cmdExec.Transaction = sqlTran1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            cmdExec.ExecuteNonQuery()
        Catch ex As Exception
            sqlTran1.Rollback()
            cnnSqlClient.Close()
            MsgBox("Failed to clear Table: StocktakeItems" & vbCrLf & "Error msg: " & vbCrLf & _
                       vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '== cnnSqlClient.Close()
            Exit Function
        End Try
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        '-- STAFF ready to copy.--
        If Not mbDoBulkCopy(bulkCopy1, dtItems, "StocktakeItems") Then
            sqlTran1.Rollback()
            cnnSqlClient.Close()
            Exit Function
        End If  '-copy-
        '--ok-
        sqlTran1.Commit()
        labHdrDateCreated.Text = Format(Now, "dd-MMM-yyyy HH:mm ")

        '-- Done..-
        cnnSqlClient.Close()
        MsgBox("Stocktake setup completed ok." & vbCrLf & vbCrLf & _
               "We loaded " & dtItems.Rows.Count & "  stock items.." & vbCrLf & _
               "  This new Stocktake-id is: " & intStocktake_id, MsgBoxStyle.Information)
        mbInitialiseStocktake = True

    End Function  '--mbInitialiseStocktake--
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    Private Sub mEnableScan()

        txtScanBarcode.Text = ""
        labScanCat1.Text = ""
        labScanCat2.Text = ""
        labScanDescription.Text = ""

        txtScanBarcodeManual.Text = ""
        labScanManualCat1.Text = ""
        labScanManualCat2.Text = ""
        labScanManualDescription.Text = ""

        txtScanBarcode.Enabled = True
        labCanScan.Visible = True
        labCanScan.Enabled = True
        txtScanBarcodeManual.Enabled = True

        grpBoxScanAuto.Enabled = True

        dgvAutoCountItems.Enabled = True

        Call mbReport("Ready")
        If (TabControlMain.SelectedTab.Name = "TabPageAuto") Then  '--doing auto count..-
            txtScanBarcode.Select()   '- go there.-
        Else
            txtScanBarcodeManual.Select()
        End If

        '== txtScanBarcode.Select()   '- go there.-

    End Sub  '-enable scan.-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- add to new row collection.
    Private Function mAddToCollection(ByRef colNewItem As Collection, _
                                       ByVal sName As String, _
                                        ByVal sValue As String)
        Dim col1 As New Collection
        col1.Add(sName, "name")
        col1.Add(sValue, "value")
        colNewItem.Add(col1, sName)
    End Function '--add fld-
    '= = === = =  == ==

    '-- Search Result Grids..

    '- Find in Uncounted Grid..
    '--  Reurn Row index or -1.

    Private Function mbCollectGridRowData(ByRef gridRow1 As DataGridViewRow, _
                                              ByRef colThisItem As Collection) As Boolean
        colThisItem = New Collection
        colThisItem.Add(gridRow1.Cells("barcode").Value, "barcode")
        colThisItem.Add(gridRow1.Cells("stock_id").Value, "stock_id")
        colThisItem.Add(gridRow1.Cells("cat1").Value, "cat1")
        colThisItem.Add(gridRow1.Cells("cat2").Value, "cat2")
        colThisItem.Add(gridRow1.Cells("description").Value, "description")
        colThisItem.Add(gridRow1.Cells("qty_on_record").Value, "qty_on_record")
        colThisItem.Add(gridRow1.Cells("qty_counted").Value, "qty_counted")
        colThisItem.Add(gridRow1.Cells("qty_difference").Value, "qty_difference")

    End Function  '- mIntFindInUncountedGrid-
    '= = = = = = = = = = = = = = = =

    '- Find in Results (Counted/Uncounted) Grid..
    '--  Reurn Row index or -1.

    Private Function mIntFindInResultsGrid(ByRef dgv1 As DataGridView, _
                                             ByVal sBarcode As String, _
                                                    ByRef colThisItem As Collection) As Integer
        Dim gridRow1 As DataGridViewRow
        Dim sBarcodeLc = LCase(sBarcode)
        Dim rx, intThisRow As Integer

        mIntFindInResultsGrid = -1
        intThisRow = -1
        For rx = 0 To dgv1.Rows.Count - 1
            gridRow1 = dgv1.Rows(rx)
            '==If LCase(gridRow1.Cells("barcode").Value) = sBarcodeLc Then
            If LCase(gridRow1.Cells("barcode").Value) = sBarcodeLc Then
                intThisRow = rx
                '-- Build colThisItem details from row..
                Call mbCollectGridRowData(gridRow1, colThisItem)
                Exit For
            End If
        Next rx '- gridRow1
        mIntFindInResultsGrid = intThisRow
    End Function  '- mIntFindInUncountedGrid-
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-  Update Stocktake Item Count--
    '-  AND Add item to Auto-Scanning grid-
    '--  And..  update rows/Counts on Results grids and Item Table..
    '==  NB: intAutoCount can be negative if we are UNDOING the last Auto Item.
    '--  NBB:  bManualAddToCount means add amount to existing count, else REPLACE..

    Private Function mbUpdateStocktakeItemCount(ByRef colThisItem As Collection, _
                                                ByVal intAutoCount As Integer, _
                                                   ByVal intManualCount As Integer, _
                                                   Optional ByVal bManualAddToCount As Boolean = False) As Boolean
        Dim sSql, sErrorMsg, sBarcode As String
        Dim intStock_id, intAffected, intOldCount, intThisCount, intOnRecord As Integer
        Dim intResultsGridRow, intUncountedGridRow As Integer
        Dim gridRow1 As DataGridViewRow

        mbUpdateStocktakeItemCount = False
        If (Not (colThisItem Is Nothing)) And _
                           ((intAutoCount <> 0) Or (intManualCount >= 0)) Then
            intStock_id = colThisItem.Item("stock_id")
            sBarcode = colThisItem.Item("barcode")
            intOldCount = CInt(colThisItem.Item("qty_counted"))  '-remember if it was uncounted.
            intOnRecord = CInt(colThisItem.Item("qty_on_record"))
            If (intAutoCount > 0) Then  '-- ADD to Auto counting Grid..--
                intThisCount = intAutoCount
                Try
                    gridRow1 = New DataGridViewRow
                    dgvAutoCountItems.Rows.Add(gridRow1)
                    Dim intNewCount As Integer = dgvAutoCountItems.Rows.Count
                    With dgvAutoCountItems.Rows(intNewCount - 1)
                        .Cells("cat1").Value = colThisItem.Item("cat1")
                        .Cells("cat2").Value = colThisItem.Item("cat2")
                        .Cells("description").Value = colThisItem.Item("description")
                        .Cells("barcode").Value = colThisItem.Item("barcode")
                        .Cells("stock_id").Value = intStock_id
                        .Cells("auto_counted").Value = intAutoCount
                        '-- save these.. (not visible in grid.)--
                        .Cells("qty_on_record").Value = colThisItem.Item("qty_on_record")
                        .Cells("qty_counted").Value = colThisItem.Item("qty_counted")
                        .Cells("qty_difference").Value = colThisItem.Item("qty_difference")
                        '-- number the rows.
                        '= .HeaderCell.Value = CStr(intNewCount - 1)
                    End With  '-row-
                    dgvAutoCountItems.Rows(intNewCount - 1).HeaderCell.Value = CStr(intNewCount) & "."
                    '==3301.813- Scroll to show latest item.
                    dgvAutoCountItems.FirstDisplayedScrollingRowIndex = (dgvAutoCountItems.Rows.Count - 1)
                    dgvAutoCountItems.Rows(dgvAutoCountItems.Rows.Count - 1).Selected = True
                    '= dgvAutoCountItems.Rows
                    DoEvents()
                Catch ex As Exception
                    MsgBox("ERROR adding row to scan grid.. " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
                    '= Exit Function
                End Try
            ElseIf (intAutoCount = -1) And _
                       (dgvAutoCountItems.Rows.Count > 0) Then  '-- UNDO from Auto counting Grid..--
                intThisCount = intAutoCount
                dgvAutoCountItems.Rows.RemoveAt(dgvAutoCountItems.Rows.Count - 1)
            ElseIf (intManualCount >= 0) Then
                '-- No manual counts in the Auto Grid..
                '== .Cells("man_counted").Value = intManualCount
                intThisCount = intManualCount
            End If

            '-- update Counts on Results grids..
            '-- If in uncounted grid, move to Results counted grid-
            '--        AND DELETE from. Uncounted Grid..
            '--   else just update count in results grid..
            intResultsGridRow = -1
            intUncountedGridRow = -1
            If colThisItem.Contains("ResultsGridRow") Then
                intResultsGridRow = colThisItem.Item("ResultsGridRow")
            End If
            If colThisItem.Contains("UncountedGridRow") Then
                intUncountedGridRow = colThisItem.Item("UncountedGridRow")
            End If

            Dim bRefresh As Boolean = False
            Dim intNewCountedQty, intDifference As Integer
            If (intUncountedGridRow >= 0) Then
                '==  CAN'T  ADD row to BOUND grid..
                '-  refresh below -
                bRefresh = True
                '--For Now just delete from Uncounted-
                dgvUncounted.Rows.RemoveAt(intUncountedGridRow)
                intNewCountedQty = intThisCount
                intDifference = (intNewCountedQty - intOnRecord)
                '= Else
            ElseIf (intResultsGridRow >= 0) Then
                '-- already counted and in grid... Just update to Results Counted-
                '--  Add if Auto..  Add/Replace if manual-
                With dgvResultsList.Rows(intResultsGridRow)
                    If (intAutoCount <> 0) Then  '-Auto=   
                        If (intOldCount >= 0) Then  '- counting was started'-
                            intNewCountedQty = CInt(.Cells("qty_counted").Value) + intAutoCount
                        Else '--was uncounted-
                            intNewCountedQty = CInt(.Cells("qty_counted").Value) '-- s/be 1.
                        End If
                    Else '-manual-
                        If bManualAddToCount Then
                            '--add-
                            intNewCountedQty = CInt(.Cells("qty_counted").Value) + intManualCount
                        Else  '-replace-
                            intNewCountedQty = intManualCount
                        End If
                    End If
                    .Cells("qty_counted").Value = intNewCountedQty
                    intDifference = (intNewCountedQty - intOnRecord)
                    .Cells("qty_difference").Value = intDifference
                End With
            End If  '--uncounted-

            '-- Add to StocktakeItems Table if not yet in Results grids. 
            '- check where it was in grids if any..

            If (intResultsGridRow >= 0) Or (intUncountedGridRow >= 0) Then
                '--we already have it on file.  just update counts.
                '--Already Exists. So just update Counts on Item Table row..
                sSql = "UPDATE dbo.StocktakeItems "
                If (intAutoCount <> 0) Then
                    sSql &= "SET qty_counted=" & CStr(intNewCountedQty) & ", "
                Else  '-manual-  (SAME !!) now.
                    sSql &= "SET qty_counted=" & CStr(intNewCountedQty) & ", "
                End If
                sSql &= " qty_difference= " & CStr(intDifference) '= ( " & CStr(intAutoCount) & " - qty_on_record )"
                sSql &= "  WHERE (stocktake_id=" & CStr(mIntStocktake_id) & ") "
                sSql &= "  AND  (stock_id=" & CStr(intStock_id) & "); "
                If Not gbExecuteCmd(mCnnSql, sSql, intAffected, sErrorMsg) Then
                    MsgBox("ERROR Saving Count- (updating StocktakeItems Table.)" & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                End If
            Else
                '--unknown.  may have had zero balance on file.
                '-- INSERT new Item record.
                sSql = "INSERT INTO  dbo.StocktakeItems ("
                sSql &= "  stocktake_id, Stock_id, barcode, " & _
                                  " cat1, cat2, description, qty_on_record, qty_counted, qty_difference  "
                sSql &= ") "
                sSql &= "VALUES ( " & CStr(mIntStocktake_id) & ", " & CStr(intStock_id) & ", "
                sSql &= "'" & sBarcode & "', "
                sSql &= "'" & colThisItem.Item("cat1") & "', "
                sSql &= "'" & colThisItem.Item("cat2") & "', "
                sSql &= "'" & colThisItem.Item("description") & "', "
                sSql &= CStr(colThisItem.Item("qty_on_record")) & ", "
                sSql &= CStr(intThisCount) & ", " & CStr(intOnRecord)
                sSql &= "); "
                If Not gbExecuteCmd(mCnnSql, sSql, intAffected, sErrorMsg) Then
                    mbReport("Saving StocktakeItem record FAILED..")
                    MsgBox("Saving StocktakeItem record FAILED.." & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                    '==Exit Function
                End If  '--exec -
                bRefresh = True   '- to get it into grid..
                MsgBox("New Item to be added to StocktakeItems Table..")

            End If  '--not in any grid..-
            '- if was moved or new item, refresh results..
            If bRefresh Then mBrowseResults.refresh()
            btnCommitAll.Enabled = True

            '-show counts..
            labCountedCount.Text = dgvResultsList.Rows.Count & "/" & dgvUncounted.Rows.Count
            mbUpdateStocktakeItemCount = True
        Else
            MsgBox("Can't add null item to Grid.", MsgBoxStyle.Exclamation)
        End If  '--colItem is nothing-

    End Function  '--add to grid.-
    '= = = = = = = =  = = = = = = =
    '-===FF->

    '--sub new-
    '--sub new-

    Public Sub New(ByRef FrmParent As Form, _
                     ByVal cnnSql As OleDbConnection, _
                       ByVal sSqlServerName As String, _
                        ByVal sSqlDbName As String, _
                        ByRef colSqlDBInfo As Collection, _
                          ByVal sVersionPOS As String, _
                            ByVal intStaff_id As Integer, _
                               ByVal sStaffName As String)

        '-- This call is required by the Windows Form Designer.
        InitializeComponent()

        '-- Add any initialization after the InitializeComponent() call.

        '--save -
        mFrmParent = FrmParent
        mCnnSql = cnnSql
        msServer = sSqlServerName

        msSqlDbName = sSqlDbName
        mColSqlDBInfo = colSqlDBInfo

        msVersionPOS = sVersionPOS
        mIntStaff_id = intStaff_id
        msStaffName = sStaffName

    End Sub  '--new-
    '= = = = = = = = = = = = = = = = = = = = = = = = = ==
    '-===FF->

    '-- L o a d -

    Private Sub ucChildStocktake_Load(sender As Object, ev As EventArgs) _
                                                       Handles MyBase.Load
        Dim sSql, s1, s2 As String

        TabControlMain.Enabled = False
        txtReport.Text = ""

        '-mColPrefColumnsStock-
        '--  stock--
        mColPrefColumnsStock = New Collection
        mColPrefColumnsStock.Add("description")
        mColPrefColumnsStock.Add("barcode")
        mColPrefColumnsStock.Add("brandName")
        mColPrefColumnsStock.Add("cat1")   '--fkey-
        mColPrefColumnsStock.Add("cat2")   '-fkey-
        '= mColPrefColumnsStock.Add("productPicture")
        mColPrefColumnsStock.Add("stock_id")
        '=3301.606= mColPrefsStock.Add("isServiceItem")
        mColPrefColumnsStock.Add("isNonStockItem")
        mColPrefColumnsStock.Add("track_serial")
        mColPrefColumnsStock.Add("inactive")
        mColPrefColumnsStock.Add("supplier_id")
        mColPrefColumnsStock.Add("costExTax")
        mColPrefColumnsStock.Add("goods_TaxCode")
        mColPrefColumnsStock.Add("sellExTax")
        mColPrefColumnsStock.Add("sales_TaxCode")
        mColPrefColumnsStock.Add("qtyInStock")


        panelFooter.Enabled = False
        '==panelCatSelection.Enabled = False
        btnFullOK.Enabled = False
        btnCatSelect.Enabled = False

        '= cboSelectCat1.Items.Clear()
        '= listSelectCat2.Items.Clear()

        '== cboStocktakeType.Visible = False
        '= grpBoxPrint.Enabled = False
        btnPrint.Enabled = False
        grpBoxPrint.Text = ""
        grpBoxType.Text = ""

        '== btnFullOK.Visible = False
        '= btnCatSelectOk.Visible = False

        labHdrDateCreated.Text = ""
        '== txtPartialSelection.Visible = False
        txtPartialSelection.Text = ""

        '==labPartialSelection.Left = cboStocktakeType.Left
        '= labPartialSelection.Width = 300
        '= labPartialSelection.Height = 100
        labCountedCount.Text = ""

        '- load these combos--
        labScanCat1.Text = ""
        labScanCat2.Text = ""
        labScanDescription.Text = ""
        txtScanBarcode.Text = ""
        txtScanBarcode.Enabled = False
        labCanScan.Enabled = False

        grpBoxAuto.Text = ""
        grpBoxManual.Text = ""
        grpBoxManualCount.Text = ""

        txtScanBarcodeManual.Text = ""
        labScanManualCat1.Text = ""
        labScanManualCat2.Text = ""
        labScanManualDescription.Text = ""

        txtResultsFind.Text = ""
        '==txtResultsSearch.Text = ""
        labResultsCount.Text = ""

        labUncountedCount.Text = ""
        labUncountedFind.Text = ""
        txtUncountedFind.Text = ""
        '== txtUncountedSearch.Text = ""

        dgvAutoCountItems.Rows.Clear()
        labAutoInfo.Text = "Only the immediate (running) scanning activity for the current Stocktake session is shown here.. " & _
                vbCrLf & vbCrLf & "Items are added continuously to this Read-only Grid as they are scanned and counted.." & _
                vbCrLf & vbCrLf & "To see actual status/results of counting, and to make changes, click on the Manual/Results Tab."

        grpBoxScanAuto.Enabled = False
        grpBoxManualCount.Enabled = False
        '= panelManual.Visible = False
        grpBoxScanManual.Text = ""

        frameBrowseResults.Text = ""
        frameBrowseUncounted.Text = ""

        labChooseType.Visible = False
        grpBoxType.Enabled = False
        optStocktakeType_full.Checked = False
        optStocktakeType_partial.Checked = False
        optStocktakeType_single.Checked = False

        '== cboStocktakeType.Items.Clear()
        '== cboStocktakeType.Items.Add("Full Stocktake")
        '== cboStocktakeType.Items.Add("Partial Stocktake")
        '==  cboStocktakeType.Items.Add("Single Item Stocktake")

        '== cboStocktakeType.SelectedIndex = -1
        '= cboStocktakeType.Enabled = False

        '== TabControlMain.SelectTab("TabPageManual") '--results..-
        '-- get system Info table data.-
        '=3301.501=
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        If mSysInfo1.contains("StockServiceChargeCat1") Then
            msStockServiceChargeCat1 = mSysInfo1.item("StockServiceChargeCat1")
        End If

        '-GST-  mDecGST_percentage- Default set as 10.0
        '-- key is GSTpercentage -
        If mSysInfo1.contains("GSTpercentage") Then
            s1 = mSysInfo1.item("GSTpercentage")
            If IsNumeric(s1) Then
                mDecGST_percentage = CDec(s1)
            End If
        End If  '-contains-

        '=3301.501= Call mbLoadSettings()
        msSettingsPath = gsLocalSettingsPath() '= "JobMatix33" default.
        '=3300.428= 
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)
        Dim colPrinters As Collection
        Dim intDefaultPrinterIndex As Integer
        If Not gbGetAvailablePrinters(colPrinters, msDefaultPrinterName, intDefaultPrinterIndex) Then
            MsgBox("No printers available ! ", MsgBoxStyle.Exclamation)
        Else
            For Each sName As String In colPrinters
                cboPrinters.Items.Add(sName)
                '= cboReceiptPrinters.Items.Add(sName)
            Next sName
            '-- check local settings (prefs) for printers..
            If mLocalSettings1.exists(k_ReportPrtSettingKey) AndAlso _
                         mLocalSettings1.item(k_ReportPrtSettingKey) <> "" Then
                '== gbQueryLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, s1) AndAlso (s1 <> "") Then
                s1 = mLocalSettings1.item(k_ReportPrtSettingKey)
                If colPrinters.Contains(s1) Then '--set it- 
                    cboPrinters.SelectedItem = s1
                Else
                    If (msDefaultPrinterName <> "") Then cboPrinters.SelectedItem = msDefaultPrinterName
                End If '-contains-
            Else  '-no prev.pref.
                If (msDefaultPrinterName <> "") Then cboPrinters.SelectedItem = msDefaultPrinterName
            End If  '-query- 
        End If '-getAvail.-  
        msReportPrinterName = cboPrinters.SelectedItem
        cboPrinters.Enabled = True

        btnCommitAll.Enabled = False
        '= labStaffName.Text = msStaffName
        '= labToday.Text = Format(Today, "dd-MMM-yyyy")

        '-=3519.0501=
        labFreeRange.Visible = False
        btnCountAllAsZero.Enabled = False
        chkConfirmSetZeroQty.Enabled = False
        chkConfirmSetZeroQty.Checked = False

        '= Call CenterForm(Me)
        '=4201.0507= - Main stuff FROM old Shown event..
        '=4201.0507= - Main stuff FROM old Shown event..
        '- Main stuff FROM old Shown event..
        '- Main stuff FROM old Shown event..

        '= Dim sSql, s1, s2 As String
        Dim dtStocktake As DataTable

        'If mbActivated Then Exit Sub
        'mbActivated = True
        mColScanCache = New Collection

        '-- Make sure we've got Admin status..

        '-- Make sure there's no other JobMatix Users logged in to this DB..

        MsgBox("Before you proceed with the Stocktake, " & vbCrLf & _
                 "make sure there's no other User logged in to this JobMatix DB.", MsgBoxStyle.Exclamation)

        '-- Wait loop for users to go away..

        '--load distinct cat values..
        If Not mbLoadCategories(mColCategoriesTree) Then
            '= Me.Close()
            Call close_me()
            Exit Sub
        End If

        '== MsgBox("TESTING-  Found " & mColCategoriesTree.Count & " categories.")

        '-- check for a Live uncommitted stock take..
        sSql = "SELECT * FROM dbo.Stocktake "
        sSql &= " WHERE (is_committed=0) and (is_cancelled=0) "
        sSql &= " ORDER BY stocktake_id DESC;"
        '-- get the recordset (datatable)..
        If Not gbGetDataTable(mCnnSql, dtStocktake, sSql) Then
            MsgBox("Error in getting Stocktake base table.." & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '= Me.Close()
            Call close_me()
            Exit Sub
        Else  '-ok-
            If (dtStocktake Is Nothing) Then
                MsgBox("Error-  No Stocktake table returned..", MsgBoxStyle.Exclamation)
                '= Me.Close()
                Call close_me()
                Exit Sub
            Else  '-something.-
                If (dtStocktake.Rows.Count > 0) Then
                    '-- get first one. (should only be one, and last created)....-
                    Dim datarowSt As DataRow = dtStocktake.Rows(0)
                    mIntStocktake_id = datarowSt.Item("stocktake_id")
                    mDateCreated = datarowSt.Item("date_created")
                    msStocktakeType = datarowSt.Item("stocktake_type")
                    '-get items..-
                    Dim dtCurrentStocktakeItems As DataTable
                    sSql = "SELECT * FROM dbo.StocktakeItems "
                    sSql &= " WHERE (stocktake_id =" & CStr(mIntStocktake_id) & "); "
                    '-- get the recordset (datatable)..
                    If Not gbGetDataTable(mCnnSql, dtCurrentStocktakeItems, sSql) Then
                        MsgBox("Error in getting Stocktake Items table.." & vbCrLf & _
                                                       gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                        '= Me.Close()
                        Call close_me()
                        Exit Sub
                    Else  '-ok-
                        '--load result grid..=
                        If (dtCurrentStocktakeItems Is Nothing) OrElse _
                                (dtCurrentStocktakeItems.Rows.Count <= 0) Then
                            MsgBox("An empty Stocktake was found.." & vbCrLf & _
                                     " A new Stocktake will be started.." & vbCrLf & vbCrLf & _
                                     " Please Select a Stocktake Type..", MsgBoxStyle.Information)
                            labChooseType.Visible = True
                            grpBoxType.Enabled = True
                            labChooseType.Visible = True
                            '== cboStocktakeType.Enabled = True
                            '== cboStocktakeType.Visible = True
                        Else  '- We have some items. 
                            '-- So- resuming prev. stocktake..
                            If LCase(msStocktakeType) = "full" Then
                                optStocktakeType_full.Checked = True
                                btnCountAllAsZero.Enabled = True
                                chkConfirmSetZeroQty.Enabled = True
                            ElseIf LCase(msStocktakeType) = "partial" Then
                                optStocktakeType_partial.Checked = True
                                btnCountAllAsZero.Enabled = True
                                chkConfirmSetZeroQty.Enabled = True
                            ElseIf LCase(msStocktakeType) = "single" Then
                                btnCountAllAsZero.Enabled = False
                                chkConfirmSetZeroQty.Enabled = False
                                optStocktakeType_single.Checked = True
                                labFreeRange.Visible = True
                            Else
                                MsgBox("ERROR- Invalid Stocktake type !", MsgBoxStyle.Exclamation)
                                '= Me.Close()
                                Call close_me()
                                Exit Sub
                            End If

                            '== labStocktakeType.Text = msStocktakeType
                            labStockTakeId.Text = "Stocktake ID: " & mIntStocktake_id
                            labHdr1.Text = "Resuming Last Stocktake: # " & mIntStocktake_id
                            labHdrDateCreated.Text = Format(mDateCreated, "dd-MMM-yyyy HH:mm ")
                            '-- category selection implied in curent stocktake items table.
                            msCurrentCat1 = datarowSt.Item("cat1")
                            msCurrentCat2List = datarowSt.Item("cat2List")
                            mColCurrentCat2 = New Collection

                            '-- make cat2 collection.
                            Dim asCat2() As String = Split(msCurrentCat2List, ";")
                            For Each s2 In asCat2
                                s2 = Trim(s2)
                                If s2 <> "" Then
                                    mColCurrentCat2.Add(s2, s2)  '--must have key..  so we can see it.
                                End If
                            Next

                            txtPartialSelection.Text = "Cat1: " & msCurrentCat1 & vbCrLf & vbCrLf & _
                                                        "Cat2: " & msCurrentCat2List
                            txtPartialSelection.Visible = True
                            MsgBox(labHdr1.Text, MsgBoxStyle.Information)

                            '--  Refresh Results Page grid(s)....
                            '==optShowUncounted.Checked = True  '--will call load grid..-
                            Call mbLoadResultsGrids()
                            '-- save Barcode for single item stocktake.
                            '=3519.0501= 
                            '---   NO MORE- "Single" now means FREE RANGE..
                            'If LCase(msStocktakeType) = "single" Then
                            '    '-- must be in the counted grid.
                            '    If (dgvResultsList.Rows.Count > 0) Then
                            '        msSingleItemBarcode = dgvResultsList.Rows(0).Cells("barcode").Value
                            '    Else
                            '        MsgBox("Error- no Single Item was stored in prev. run..", MsgBoxStyle.Exclamation)
                            '    End If
                            'End If  '-single-

                            '- Now can go-
                            grpBoxScanAuto.Enabled = True
                            grpBoxManualCount.Enabled = False
                            TabControlMain.Enabled = True
                            '== TabControlMain.SelectTab("TabPageResults") '--results..-
                            Call mEnableScan()
                        End If
                    End If  '-get-
                Else
                    MsgBox("There is No current stocktake on file.." & vbCrLf & _
                              " A new one will be started.." & vbCrLf & vbCrLf & _
                              " Please select a Stocktake Type..", MsgBoxStyle.Information)
                    grpBoxType.Enabled = True
                    labChooseType.Visible = True
                    '== cboStocktakeType.Enabled = True
                    '== cboStocktakeType.Visible = True

                End If
            End If  '-nothing-
        End If  '--get-

        panelFooter.Enabled = True
        '= TabControlMain.SelectTab("TabPageResults") '--results..-
        grpBoxManualCount.Visible = True

        '= listSelectCat2.Enabled = False
        TabControlMain.SelectTab("TabPageAuto") '--Start on Auto..-
        mbIsInitialising = False

    End Sub  '--load-
    '= = = = =  = = = = = == 
    '-===FF->

    '--Activated--

    'Private Sub ucChildStocktake_Activated(sender As Object, ev As EventArgs) _
    '                                            Handles MyBase.Activated
    '    If mbActivated Then Exit Sub
    '    mbActivated = True

    'End Sub  '--activated-
    '= = = = = = = = = = = = =


    'Private Sub ucChildStocktake_Shown(sender As Object, ev As EventArgs) _
    '                                                Handles MyBase.Shown
    'Dim sSql, s1, s2 As String
    'Dim dtStocktake As DataTable

    ''If mbActivated Then Exit Sub
    ''mbActivated = True
    'mColScanCache = New Collection

    ''-- Make sure we've got Admin status..

    ''-- Make sure there's no other JobMatix Users logged in to this DB..

    'MsgBox("Before you proceed with the Stocktake, " & vbCrLf & _
    '         "make sure there's no other User logged in to this JobMatix DB.", MsgBoxStyle.Exclamation)

    ''-- Wait loop for users to go away..

    ''--load distinct cat values..
    'If Not mbLoadCategories(mColCategoriesTree) Then
    '    '= Me.Close()
    '    Call close_me()
    '    Exit Sub
    'End If

    ''== MsgBox("TESTING-  Found " & mColCategoriesTree.Count & " categories.")

    ''-- check for a Live uncommitted stock take..
    'sSql = "SELECT * FROM dbo.Stocktake "
    'sSql &= " WHERE (is_committed=0) and (is_cancelled=0) "
    'sSql &= " ORDER BY stocktake_id DESC;"
    ''-- get the recordset (datatable)..
    'If Not gbGetDataTable(mCnnSql, dtStocktake, sSql) Then
    '    MsgBox("Error in getting Stocktake base table.." & vbCrLf & _
    '                                   gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
    '    '= Me.Close()
    '    Call close_me()
    '    Exit Sub
    'Else  '-ok-
    '    If (dtStocktake Is Nothing) Then
    '        MsgBox("Error-  No Stocktake table returned..", MsgBoxStyle.Exclamation)
    '        '= Me.Close()
    '        Call close_me()
    '        Exit Sub
    '    Else  '-something.-
    '        If (dtStocktake.Rows.Count > 0) Then
    '            '-- get first one. (should only be one, and last created)....-
    '            Dim datarowSt As DataRow = dtStocktake.Rows(0)
    '            mIntStocktake_id = datarowSt.Item("stocktake_id")
    '            mDateCreated = datarowSt.Item("date_created")
    '            msStocktakeType = datarowSt.Item("stocktake_type")
    '            '-get items..-
    '            Dim dtCurrentStocktakeItems As DataTable
    '            sSql = "SELECT * FROM dbo.StocktakeItems "
    '            sSql &= " WHERE (stocktake_id =" & CStr(mIntStocktake_id) & "); "
    '            '-- get the recordset (datatable)..
    '            If Not gbGetDataTable(mCnnSql, dtCurrentStocktakeItems, sSql) Then
    '                MsgBox("Error in getting Stocktake Items table.." & vbCrLf & _
    '                                               gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
    '                '= Me.Close()
    '                Call close_me()
    '                Exit Sub
    '            Else  '-ok-
    '                '--load result grid..=
    '                If (dtCurrentStocktakeItems Is Nothing) OrElse _
    '                        (dtCurrentStocktakeItems.Rows.Count <= 0) Then
    '                    MsgBox("An empty Stocktake was found.." & vbCrLf & _
    '                             " A new Stocktake will be started.." & vbCrLf & vbCrLf & _
    '                             " Please Select a Stocktake Type..", MsgBoxStyle.Information)
    '                    labChooseType.Visible = True
    '                    grpBoxType.Enabled = True
    '                    labChooseType.Visible = True
    '                    '== cboStocktakeType.Enabled = True
    '                    '== cboStocktakeType.Visible = True
    '                Else  '- We have some items. 
    '                    '-- So- resuming prev. stocktake..
    '                    If LCase(msStocktakeType) = "full" Then
    '                        optStocktakeType_full.Checked = True
    '                        btnCountAllAsZero.Enabled = True
    '                        chkConfirmSetZeroQty.Enabled = True
    '                    ElseIf LCase(msStocktakeType) = "partial" Then
    '                        optStocktakeType_partial.Checked = True
    '                        btnCountAllAsZero.Enabled = True
    '                        chkConfirmSetZeroQty.Enabled = True
    '                    ElseIf LCase(msStocktakeType) = "single" Then
    '                        btnCountAllAsZero.Enabled = False
    '                        chkConfirmSetZeroQty.Enabled = False
    '                        optStocktakeType_single.Checked = True
    '                        labFreeRange.Visible = True
    '                    Else
    '                        MsgBox("ERROR- Invalid Stocktake type !", MsgBoxStyle.Exclamation)
    '                        '= Me.Close()
    '                        Call close_me()
    '                        Exit Sub
    '                    End If

    '                    '== labStocktakeType.Text = msStocktakeType
    '                    labStockTakeId.Text = "Stocktake ID: " & mIntStocktake_id
    '                    labHdr1.Text = "Resuming Last Stocktake: # " & mIntStocktake_id
    '                    labHdrDateCreated.Text = Format(mDateCreated, "dd-MMM-yyyy HH:mm ")
    '                    '-- category selection implied in curent stocktake items table.
    '                    msCurrentCat1 = datarowSt.Item("cat1")
    '                    msCurrentCat2List = datarowSt.Item("cat2List")
    '                    mColCurrentCat2 = New Collection

    '                    '-- make cat2 collection.
    '                    Dim asCat2() As String = Split(msCurrentCat2List, ";")
    '                    For Each s2 In asCat2
    '                        s2 = Trim(s2)
    '                        If s2 <> "" Then
    '                            mColCurrentCat2.Add(s2, s2)  '--must have key..  so we can see it.
    '                        End If
    '                    Next

    '                    txtPartialSelection.Text = "Cat1: " & msCurrentCat1 & vbCrLf & vbCrLf & _
    '                                                "Cat2: " & msCurrentCat2List
    '                    txtPartialSelection.Visible = True
    '                    MsgBox(labHdr1.Text, MsgBoxStyle.Information)

    '                    '--  Refresh Results Page grid(s)....
    '                    '==optShowUncounted.Checked = True  '--will call load grid..-
    '                    Call mbLoadResultsGrids()
    '                    '-- save Barcode for single item stocktake.
    '                    '=3519.0501= 
    '                    '---   NO MORE- "Single" now means FREE RANGE..
    '                    'If LCase(msStocktakeType) = "single" Then
    '                    '    '-- must be in the counted grid.
    '                    '    If (dgvResultsList.Rows.Count > 0) Then
    '                    '        msSingleItemBarcode = dgvResultsList.Rows(0).Cells("barcode").Value
    '                    '    Else
    '                    '        MsgBox("Error- no Single Item was stored in prev. run..", MsgBoxStyle.Exclamation)
    '                    '    End If
    '                    'End If  '-single-

    '                    '- Now can go-
    '                    grpBoxScanAuto.Enabled = True
    '                    grpBoxManualCount.Enabled = False
    '                    TabControlMain.Enabled = True
    '                    '== TabControlMain.SelectTab("TabPageResults") '--results..-
    '                    Call mEnableScan()
    '                End If
    '            End If  '-get-
    '        Else
    '            MsgBox("There is No current stocktake on file.." & vbCrLf & _
    '                      " A new one will be started.." & vbCrLf & vbCrLf & _
    '                      " Please select a Stocktake Type..", MsgBoxStyle.Information)
    '            grpBoxType.Enabled = True
    '            labChooseType.Visible = True
    '            '== cboStocktakeType.Enabled = True
    '            '== cboStocktakeType.Visible = True

    '        End If
    '    End If  '-nothing-
    'End If  '--get-

    ''== TabControlMain.Enabled = True

    'panelFooter.Enabled = True
    ''= TabControlMain.SelectTab("TabPageResults") '--results..-
    'grpBoxManualCount.Visible = True

    ''= listSelectCat2.Enabled = False
    'TabControlMain.SelectTab("TabPageAuto") '--Start on Auto..-
    'mbIsInitialising = False

    '= End Sub  '-Shown.
    '= = = = =  = = = = = == 
    '-===FF->

    '- btnStockAdmin_Click-
    'Private Sub btnStockAdmin_Click(sender As Object, e As EventArgs)

    '    Dim frmStock1 As New frmStock

    '    frmStock1.StaffName = msStaffName

    '    frmStock1.SqlServer = msServer
    '    frmStock1.connectionSql = mCnnSql '--job tracking sql connenction..-
    '    frmStock1.dbInfoSql = mColSqlDBInfo  '-IS now used in stock form..

    '    frmStock1.DBname = msSqlDbName
    '    frmStock1.VersionPOS = msVersionPOS
    '    '=frmBrowse1.tableName = sTableName '--"jobs"
    '    '== frmBrowse1.IsSqlServer = True '--bIsSqlServer
    '    frmStock1.form_left = Me.Left
    '    frmStock1.form_top = Me.Top + 70

    '    frmStock1.ShowDialog()

    'End Sub   '== btnStockAdmin_Click'-
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--optStocktakeType_full_CheckedChanged-

    Private Sub optStocktakeType_full_CheckedChanged(sender As Object, ev As EventArgs) _
                                                         Handles optStocktakeType_full.CheckedChanged, _
                                                                 optStocktakeType_partial.CheckedChanged, _
                                                                 optStocktakeType_single.CheckedChanged
        Dim opt1 As RadioButton = CType(sender, RadioButton)
        Dim radioNameSelected As String = CType(sender, RadioButton).Name
        Dim sType As String = opt1.Text
        Dim col1, col2 As Collection

        If mbIsInitialising Then Exit Sub
        txtPartialSelection.Text = ""

        If opt1.Checked Then  '--filter out the unchecking change-
            Select Case LCase(VB.Left(sType, 4))
                Case "full"
                    msStocktakeType = "full"
                    mbIsFullStocktake = True
                    '- can go soon-
                    '-- After 1st scan.. load UNCOUNTED with Stock table items (ALL)..
                    '-- now can go..
                    btnCatSelect.Enabled = False
                    '= btnFullOK.Visible = True
                    btnFullOK.Enabled = True
                    btnCountAllAsZero.Enabled = True
                    chkConfirmSetZeroQty.Enabled = True
                    btnFullOK.Select()

                Case "part"
                    '= btnFullOK.Visible = False
                    btnFullOK.Enabled = False
                    mbIsFullStocktake = False

                    msStocktakeType = "partial"
                    mbIsPartialStocktake = True
                    btnCountAllAsZero.Enabled = True
                    'btnCatSelectOk.Enabled = False
                    chkConfirmSetZeroQty.Enabled = True

                    btnCatSelect.Enabled = True
                    MsgBox("Please select Categories for Stocktake..", MsgBoxStyle.Information)
                    '== panelCatSelection.Select()
                    '-- Later- make SQL WHERE clause to select from stock..
                    '-- Drop out and wait for categories.
                Case "sing"
                    '- FREE RANGING..
                    mbIsFullStocktake = False
                    btnCatSelect.Enabled = False
                    msStocktakeType = "single"
                    mbIsSingleStocktake = True
                    '= btnFullOK.Visible = False
                    btnFullOK.Enabled = True
                    btnCountAllAsZero.Enabled = False
                    chkConfirmSetZeroQty.Enabled = False
                    labFreeRange.Visible = True

                    txtPartialSelection.Text = "Stocktaking Single (Free Ranging) Items.."
                    MsgBox("Press OK and Scan items as needed.-" & vbCrLf & _
                           " Commit All when done..", MsgBoxStyle.Information)
                    '=txtScanBarcode.Select()
            End Select
        End If  '-checked-

    End Sub  '--optStocktakeType_full_CheckedChanged-
    '= = = = = = = = = = = = =  == = =  ==  = = = = ==
    '-===FF->

    '- btnCatSelect-

    Private Sub btnCatSelect_Click(sender As Object, e As EventArgs) Handles btnCatSelect.Click

        Dim frmDummy As New Form
        Dim frmSelect1 As New frmStocktakeCatSelect(frmDummy, mColCategoriesTree, msVersionPOS)

        frmSelect1.ShowDialog()

        '- get result.-
        If Not frmSelect1.isCancelled Then
            msCurrentCat1 = frmSelect1.currentCat1
            mColCurrentCat2 = frmSelect1.colCurrentCat2
            msCurrentCat2List = frmSelect1.currentCat2List

            '-- load  UNCOUNTED  Stock table items selected (cat1/cat2)..
            '--  After 1st Item scanned..
            '-- now can go..

            'grpBoxScanAuto.Enabled = True
            'grpBoxManual.Enabled = True
            'TabControlMain.Enabled = True
            'txtScanBarcode.Enabled = True
            'txtScanBarcodeManual.Enabled = True
            txtPartialSelection.Text = "Cat1: " & msCurrentCat1 & vbCrLf & vbCrLf & _
                                         "Cat2: " & msCurrentCat2List
            txtPartialSelection.Visible = True

            MsgBox("You have selected to stocktake: " & vbCrLf & _
                   "Cat1: " & msCurrentCat1 & vbCrLf & _
                   "cat2: " & msCurrentCat2List & vbCrLf & vbCrLf & _
                   "Press OK, and scan first the item to initialise this Stocktake..", MsgBoxStyle.Information)
            txtScanBarcode.Select()
            btnFullOK.Enabled = True
        End If

        frmSelect1.Close()

    End Sub  '- btnCatSelect-
    '= = = = = = = = = = = =  == = 

    '-btnFullOK_Click-
    Private Sub btnFullOK_Click(sender As Object, ev As EventArgs) Handles btnFullOK.Click

        btnFullOK.Enabled = False
        btnCatSelect.Enabled = False
        grpBoxType.Enabled = False

        grpBoxScanAuto.Enabled = True
        grpBoxScanManual.Enabled = True
        TabControlMain.Enabled = True
        txtScanBarcode.Enabled = True
        txtScanBarcodeManual.Enabled = True
        If mbIsFullStocktake Then
            MsgBox("OK.. We are stocktaking everything: " & vbCrLf & vbCrLf & _
                     "Just Scan first the item to initialise this Stocktake..", MsgBoxStyle.Information)
        End If
        txtScanBarcode.Select()

    End Sub '-btnFullOK_Click-
    '= = = = = = = = = = = = = =
    '-===FF->


    '-- SplitContainerResults_SplitterMoving-

    Private Sub SplitContainerResults_SplitterMoving(sender As Object, ev As System.Windows.Forms.SplitterCancelEventArgs)


        ' Define what happens while the splitter is moving.
        Cursor.Current = System.Windows.Forms.Cursors.NoMoveVert

    End Sub   '--moving--
    '= = = = = = = = = = =  = = = = = ==

    '-SplitContainerResults_SplitterMoved-
    '- RESIZE grids..-
    '-- Min width 260 px..--
    '=== GONE  =

    Private Sub frameBrowseResults_Resize(ByVal sender As GroupBox, _
                                          ByVal ev As System.EventArgs) Handles frameBrowseResults.Resize
        dgvResultsList.Width = frameBrowseResults.Width - 3
    End Sub  '-rsults resize..-
    '= = = = = = = = = = = = = = = = = =

    '== Private Sub frameBrowseUncounted_Resize(ByVal sender As GroupBox, _
    '==                                       ByVal ev As System.EventArgs) Handles frameBrowseUncounted.Resize
    '==     dgvUncounted.Width = frameBrowseUncounted.Width - 3
    '== '--  Refresh Results Page grids....
    '== '== MsgBox("Frame browse uncounted was resized..", MsgBoxStyle.Information)   '=Call mbLoadResultsGrids()

    '== End Sub  '- resize..-
    '= = = = = = = = = = = = = = = = = =
    '-===FF->

    '-- S c a n n i n g --

    '--btnLookupStock_Click-
    Private Sub btnLookupStock_Click(sender As Object, e As EventArgs) Handles btnLookupStock.Click
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim sBarcode, sBarcode0, sSql, s1, sErrorMsg As String
        Dim intStock_id As Integer
        '= Dim gridrow1 As DataGridViewRow
        Dim colPrefsStock As Collection = mColPrefColumnsStock  '=3301.816=

        If Not mbBrowseAndSearchTable(colPrefsStock, "Lookup Stock", "", colKeys, colSelectedRow, "stock") Then
            MsgBox("Lookup cancelled.", MsgBoxStyle.Exclamation)
        Else '-ok-
            If (Not (colKeys Is Nothing)) AndAlso (colKeys.Count > 0) Then

                intStock_id = CInt(colKeys(1))  '--save pkey as data..
                If colSelectedRow.Contains("barcode") Then
                    sBarcode0 = colSelectedRow.Item("barcode")("value")
                Else
                    MsgBox("No value in selected row for: barcode..  ", MsgBoxStyle.Information)
                    Exit Sub
                End If
                sBarcode = Replace(sBarcode0, "'", "")  ''--strip quotes-
                If (sBarcode <> "") Then
                    txtScanBarcode.Text = sBarcode
                    labScanDescription.Text = colSelectedRow("description")("value")
                End If  '-barcode-
            End If  '-keys..
        End If  '-browse

    End Sub '-btnLookupStock_Click-
    '= = = = = = =  == = ==  == = =
    '-===FF->

    '-- S c a n n i n g --
    '-- S c a n n i n g --

    Private Sub txtScanBarcode_TextChanged(sender As Object, ev As EventArgs) _
                                            Handles txtScanBarcode.TextChanged

    End Sub  '-txtScanBarcode_TextChanged-
    '= = = = = = = = = = ==  = = = = = = =

    '--txtScanBarcode_KeyPress-
    '-- ENTER on Scanned Barcode..
    '--  BOTH AUTO and MANUAL Barcode textboxes..

    Private Sub txtScanBarcode_KeyPress(ByVal eventSender As TextBox, _
                                          ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                  Handles txtScanBarcode.KeyPress, txtScanBarcodeManual.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim s1, s2, sBarcode As String
        Dim sSql, sMsg As String
        Dim colResult, colRecord, colField As Collection
        Dim datatable1 As DataTable
        '== Dim datarowThisItem As DataRow = Nothing
        Dim txtBarcode As TextBox = eventSender
        Dim bIsAutoCount As Boolean
        Dim colThisItem As Collection
        Dim intResultsGridRow, intUncountedGridRow, intItem_id, rx As Integer
        Dim bAddToCache, bAddToItemsTable As Boolean

        If (keyAscii = 13) Then '--enter-
            sBarcode = Trim(txtBarcode.Text)
            '==3301.706==
            '== Strip LEADING ZEROES Unless "Keep" is checked..
            If Not chkKeepScannedLeadZeroes.Checked Then
                While (VB.Left(sBarcode, 1) = "0") Or (VB.Left(sBarcode, 1) = " ")
                    '==    While (VB.Left(txtPartNo.Text, 1) = " ")
                    sBarcode = Mid(sBarcode, 2)
                End While
                txtBarcode.Text = sBarcode
            End If  '--checked..-

            If (sBarcode <> "") Then
                mColCurrentScanItemDetails = Nothing
                bIsAutoCount = False  '= IIf(((LCase(txtBarcode.Name) = "txtscanbarcode")), True, False)
                If (TabControlMain.SelectedTab.Name = "TabPageAuto") Then  '--doing auto count..-
                    bIsAutoCount = True
                End If
                '--have stock barcode-
                If bIsAutoCount Then
                    txtScanBarcode.Enabled = False
                    labCanScan.Enabled = False
                Else
                    txtScanBarcodeManual.Enabled = False
                End If
                '== bAddToResultsGrid = False
                intResultsGridRow = -1
                '== intUncountedGridRow = -1
                intItem_id = -1
                bAddToItemsTable = False
                '==bAddToCache = False

                txtReport.Text &= "Processing Barcode: " & sBarcode & vbCrLf
                DoEvents()

                grpBoxManualCount.Visible = True
                '-- Initialise Stocktake if not done yet..
                If (mIntStocktake_id <= 0) Then
                    If Not mbInitialiseStocktake(msStocktakeType, sBarcode, msCurrentCat1, msCurrentCat2List, mColCurrentCat2) Then
                        MsgBox("Initialise failed..", MsgBoxStyle.Exclamation)
                        '= Me.Close()
                        Call close_me()
                        Exit Sub
                    Else  '-ok-
                        '--  1st Load Results Page grid(s)....
                        '== optShowUncounted.Checked = True  '--will call load grid..-
                        Call mbLoadResultsGrids()
                    End If '-initialise.-
                End If  '-- must init-

                '-- First time after form load.. 
                '-- Refresh Result grid and Uncounted Grid..
                '= If (dgvResultsList.Rows.Count <= 0) And (dgvUncounted.Rows.Count <= 0) Then
                '==  MsgBox("Load Grids not done..", MsgBoxStyle.Information)
                '== End If
                '-- Check Barcode is same for single item stocktake.
                '=3519.0501= 
                '---   NO MORE- "Single" now means FREE RANGE..
                'If (LCase(msStocktakeType) = "single") Then
                '    If (msSingleItemBarcode <> "") Then
                '        If LCase(msSingleItemBarcode) <> LCase(sBarcode) Then
                '            MsgBox("Wrong barcode for Single Item stocktake..", MsgBoxStyle.Exclamation)
                '            Call mEnableScan()
                '            Exit Sub
                '        End If
                '    End If
                'End If  '--single-
                '-- check Cache for this barcode--
                If mColScanCache.Contains(sBarcode) Then
                    colThisItem = mColScanCache(sBarcode)
                    '==MsgBox("Testing.. found barcode in cache.. Count= " & mColScanCache.Count, MsgBoxStyle.Information)
                Else  '-no-
                    '-- wasn't in cache..  so add it.
                    If (mColScanCache.Count > 7) Then  '--drop oldest.-
                        mColScanCache.Remove(1)
                    End If
                    mColScanCache.Add(colThisItem, sBarcode)
                    '= bAddToCache = True
                End If
                If (colThisItem Is Nothing) Then '-Don't have from cache.-
                    '-- NOT in  cache.  so look up stock table.
                    '--  Look up Barcode in Uncounted Grid, and then in Results grid..
                    Dim gridRow1 As DataGridViewRow
                    intUncountedGridRow = mIntFindInResultsGrid(dgvUncounted, sBarcode, colThisItem)

                    '-- looking up results grid...
                    If (intUncountedGridRow < 0) Then  '--not in uncounted grid..
                        '--look in results Counted grid.
                        intResultsGridRow = mIntFindInResultsGrid(dgvResultsList, sBarcode, colThisItem)
                    End If  '-not in uncounted-
                    If (intResultsGridRow < 0) And (intUncountedGridRow < 0) Then
                        '-- Not in grids.  Won't be in StocktakeItems Table either..
                        '= If (intItem_id < 0) Then
                        '-- Not in Grids (Or Items Table)..  we must have missed it in bulkCopy-
                        '--  (BECAUSE of zero qty in stock.)
                        '--lookup barcode in Actual Stock Table-
                        '-- If partial s/t, must fit Selected Cats..
                        s1 = "No Stock record found for barcode: '" & sBarcode & "'" & vbCrLf
                        sSql = "SELECT * FROM [stock] WHERE (barcode='" & sBarcode & "');"
                        If LCase(msStocktakeType) = "partial" Then
                            s2 = "Selected Stocktake categories are: " & vbCrLf & _
                                  "  Cat1: " & msCurrentCat1 & vbCrLf & _
                                  "  Cat2: " & msCurrentCat2List
                            '- we get the item and then check the cat..
                            '= sSql &= " AND " & msMakeCategoryClause(msCurrentCat1, msCurrentCat2List, mColCurrentCat2)

                        End If
                        If gbGetDataTable(mCnnSql, datatable1, sSql) Then
                            If (Not (datatable1 Is Nothing)) AndAlso (datatable1.Rows.Count > 0) Then  '-have-
                                '--have a row..  Must fit ok.-
                                Dim drThisItem As DataRow = datatable1.Rows(0)
                                '-- save stock details for insertion into stocktake items.
                                colThisItem = New Collection
                                colThisItem.Add(sBarcode, "barcode")
                                colThisItem.Add(drThisItem.Item("stock_id"), "stock_id")
                                colThisItem.Add(drThisItem.Item("cat1"), "cat1")
                                colThisItem.Add(drThisItem.Item("cat2"), "cat2")
                                colThisItem.Add(drThisItem.Item("description"), "description")
                                colThisItem.Add(drThisItem.Item("qtyInStock"), "qty_on_record")
                                colThisItem.Add(0, "qty_counted")
                                colThisItem.Add(-1, "qty_difference")
                                labScanCat1.Text = Trim(colThisItem.Item("cat1"))
                                labScanCat2.Text = Trim(colThisItem.Item("cat2"))
                                labScanDescription.Text = colThisItem.Item("Description")
                                sMsg = ""
                                '- Found barcode..check that it fits-
                                If (msStockServiceChargeCat1 <> "") AndAlso _
                                              LCase(msStockServiceChargeCat1) = LCase(labScanCat1.Text) Then
                                    sMsg = "Service Charges are not counted in Stocktake."
                                    '= Call mEnableScan()
                                    '= Exit Sub
                                ElseIf (drThisItem.Item("isNonStockItem") <> 0) Then
                                    sMsg = "Non-stockable lines are not counted in Stocktake."
                                ElseIf (LCase(msStocktakeType) = "partial") Then
                                    If (LCase(labScanCat1.Text) <> LCase(msCurrentCat1)) OrElse _
                                             (Not (mColCurrentCat2.Contains(labScanCat2.Text))) Then
                                        '-- wrong cat..
                                        sMsg = "The Stock item for barcode: '" & sBarcode & "' " & vbCrLf & _
                                                 " was NOT included in this Stocktake--" & _
                                                    vbCrLf & vbCrLf & s2
                                    Else  '-ok-fits-
                                    End If
                                End If  '-partial-
                                If (sMsg <> "") Then  '-refused-
                                    MsgBox(sMsg, MsgBoxStyle.Exclamation)
                                    Call mEnableScan()
                                    Exit Sub
                                End If
                                'MsgBox("Item has been added to this Stocktake-" & vbCrLf & _
                                '       "  (May have been originally excluded because of zero balance)..", MsgBoxStyle.Information)
                                '- it fits, so add it to the StocktakeItems Table.
                                Call mbReport("New Item has been added to this Stocktake..")

                                bAddToItemsTable = True
                                '=bAddToResultsGrid = True
                            Else
                                '--not found
                                MsgBox(s1 & vbCrLf, MsgBoxStyle.Exclamation)
                                Call mEnableScan()
                                Exit Sub
                            End If  '--count-
                        Else '--not found..-
                            MsgBox("Error getting stock record:" & vbCrLf & _
                                     gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
                        End If  '-get--
                        '=End If  '-item id-
                    Else
                        '= MsgBox("Testing.. found barcode in GRID.. ", MsgBoxStyle.Information)
                    End If  '--grid row-
                End If  '- in cache-

                '-- processing (counting)..-
                If Not (colThisItem Is Nothing) Then '-have stuff.- 
                    '-- ok we have a valid item..
                    '=3301.814= -- SHOW Barcode/descr for single item stocktake.
                    '=3519.0501= 
                    '---   NO MORE- "Single" now means FREE RANGE..
                    'If (LCase(msStocktakeType) = "single") Then
                    '    txtPartialSelection.Text = "Single Item Stocktake- Barcode:" & vbCrLf & _
                    '                           sBarcode & vbCrLf & colThisItem.Item("Description")
                    'End If
                    '= If bAddToCache Then
                    '== End If
                    '-- save grid pos if any for updating..
                    colThisItem.Add(intResultsGridRow, "ResultsGridRow")
                    colThisItem.Add(intUncountedGridRow, "UncountedGridRow")

                    '- valid barcode to count.
                    If bIsAutoCount Then
                        labScanCat1.Text = colThisItem.Item("cat1")
                        labScanCat2.Text = colThisItem.Item("cat2")
                        labScanDescription.Text = colThisItem.Item("Description")
                    Else  '-manual-
                        labScanManualCat1.Text = colThisItem.Item("cat1")
                        labScanManualCat2.Text = colThisItem.Item("cat2")
                        labScanManualDescription.Text = colThisItem.Item("Description")
                    End If  '-auto-
                    DoEvents()

                    mColCurrentScanItemDetails = colThisItem  '--save for Manual Count.-
                    Call mbReport("Updating Grids.")
                    DoEvents()
                    '--If in AUTO Count mode, add to auto grid --
                    If (TabControlMain.SelectedTab.Name = "TabPageAuto") Then  '--doing auto count..-
                        Call mbUpdateStocktakeItemCount(colThisItem, 1, -1)  '-manual -1 means no manual.
                        '-- can start again..
                        Call mEnableScan()
                    Else  '-in manual mode-
                        grpBoxScanAuto.Enabled = False
                        grpBoxManualCount.Enabled = True
                        txtManualQty.Text = ""
                        '=TabPageAuto.Enabled = False   '--must complete manual box..
                        '- txtManualExpected --"qty_on_record"
                        txtManualExpected.Text = colThisItem.Item("qty_on_record")
                        txtLastManualBarcode.Text = sBarcode

                        txtManualQty.Select()
                        '- wait for commit (save0=)--
                    End If  '-auto-
                End If  '-this item..-
            End If  '-sBarcode- 
        End If  '-13- 
        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If

    End Sub  '--txtScanBarcode_KeyPress-
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Manual Counting..-
    '-- Manual Counting..-
    '-- Manual Counting..-

    Private Sub txtManualQty_TextChanged(sender As Object, e As EventArgs) _
                                             Handles txtManualQty.TextChanged

    End Sub  '-txtManualQty-
    '= = = = = = = = = = = = = = == =

    '- ENTER-  Move to Add button.

    Private Sub txtManualQty_keyPress(ByVal eventSender As TextBox, _
                                          ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                                 Handles txtManualQty.KeyPress
        Dim textBox1 As TextBox = CType(eventSender, TextBox)
        Dim controlParent As Control = textBox1.Parent
        Dim sData As String = Trim(textBox1.Text)
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)

        If (keyAscii = 13) Then '--enter-
            '--Do a  Tab-
            '--  Just use validate--
            controlParent.SelectNextControl(textBox1, True, True, True, True)
            '--and then go to ADD-
            eventArgs.Handled = True
        End If  '-13-
    End Sub '-txtManualQty_keyPress-
    '= = = = = = = = = = = = = = = = =

    '-Qty_Validating-

    Private Sub txtManualQty_Validating(ByVal sender As System.Object, _
                                              ByVal ev As CancelEventArgs) Handles txtManualQty.Validating
        Dim textBox1 As TextBox = CType(sender, TextBox)
        Dim controlParent As Control = textBox1.Parent
        Dim sData As String = Trim(textBox1.Text)

        If (sData.Length > 5) Then
            ev.Cancel = True
            MsgBox("Amount is too long..", MsgBoxStyle.Exclamation)
        ElseIf (sData = "") OrElse mbIsNumeric(sData) Then
            '--ok-
            If (sData <> "") AndAlso (CDec(sData) < 0) Then
                ev.Cancel = True
                MsgBox("Can't have negative amount.!", MsgBoxStyle.Exclamation)
            End If
        Else
            ev.Cancel = True
            MsgBox("Amount must be numeric.  !", MsgBoxStyle.Exclamation)
        End If  '--numeric-

    End Sub  '-validating.
    '= = = = = = = = = == == 
    '-===FF->

    '-UpdateManualCount-

    Private Function mbUpdateManualCount(ByVal bManualAddYesOrNo As Boolean) As Boolean
        mbUpdateManualCount = False
        If Not (mColCurrentScanItemDetails Is Nothing) Then
            If IsNumeric(Trim(txtManualQty.Text)) AndAlso _
                                         CInt((Trim(txtManualQty.Text)) >= 0) Then
                Call mbUpdateStocktakeItemCount(mColCurrentScanItemDetails, 0, CInt(txtManualQty.Text), bManualAddYesOrNo)
                grpBoxManualCount.Enabled = False
                '= TabControlMain.Enabled = True
                Call mEnableScan()
                mbUpdateManualCount = True
            Else
                MsgBox("Entry must be an integer (number only)..")
            End If
        End If
        frameBrowseResults.Enabled = True
        frameBrowseUncounted.Enabled = True
    End Function  '--update.
    '= = = = = = = = = = == == = =

    '-- Manual add to count .-

    Private Sub btnAddToCount_Click(sender As Object, e As EventArgs) Handles btnAddToCount.Click
        Call mbUpdateManualCount(True)  '--add to count-
    End Sub  '-- manual add to count .-
    '= = = = = = = = = = = = ==  =

    '--btnManual Save-
    '-- Update (Replace) Stocktake Item record.

    Private Sub btnManualSave_Click(sender As Object, ev As EventArgs) _
                                      Handles btnManualSave.Click
        Call mbUpdateManualCount(False)  '--New count count to replace-

    End Sub  '-manual Save.-
    '= = = = = = = = = = = = = ==

    Private Sub btnManualCancel_Click(sender As Object, ev As EventArgs) _
                                                     Handles btnManualCancel.Click
        grpBoxManualCount.Enabled = False
        Call mEnableScan()
        frameBrowseResults.Enabled = True
    End Sub '- manual cancel -
    '= = = = =  = = = = = = = = ==  =
    '-===FF->

    '- Undo last Auto scan line..

    Private Sub btnAutoUndo_Click(sender As Object, ev As EventArgs) Handles btnAutoUndo.Click
        Dim colThisItem As Collection
        Dim sBarcode As String
        Dim intResultsGridRow As Integer

        If (dgvAutoCountItems.Rows.Count > 0) Then  '-have some.-

            btnAutoUndo.Enabled = False   '-while we do it..
            grpBoxScanAuto.Enabled = False
            '- get stock barcode from last AUTO row.
            With dgvAutoCountItems.Rows(dgvAutoCountItems.Rows.Count - 1)
                sBarcode = .Cells("barcode").Value
            End With

            '- get stock info from Results row. 
            '==  (MUST be there as it was prev. scanned !!)
            intResultsGridRow = mIntFindInResultsGrid(dgvResultsList, sBarcode, colThisItem)
            If intResultsGridRow < 0 Then
                MsgBox("Error- Record to undo not found in Results Grid.", MsgBoxStyle.Exclamation)
                grpBoxScanAuto.Enabled = True
                Exit Sub
            End If
            '-- save grid pos if any for updating..
            colThisItem.Add(intResultsGridRow, "ResultsGridRow")
            colThisItem.Add(-1, "UncountedGridRow")

            If Not mbUpdateStocktakeItemCount(colThisItem, -1, -1) Then  '--NO manual-
                MsgBox("Failed to Undo last row.", MsgBoxStyle.Exclamation)
            End If
            If (dgvAutoCountItems.Rows.Count > 0) Then  '- STILL have some.-
                btnAutoUndo.Enabled = True
            End If
        End If  '-has rows-
        grpBoxScanAuto.Enabled = True
        Call mEnableScan()

    End Sub '-btnAutoUndo-
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--btnCountAllAsZero-

    Private Sub btnCountAllAsZero_Click(sender As Object, ev As EventArgs) Handles btnCountAllAsZero.Click
        Dim intInitialCount As Integer = dgvUncounted.Rows.Count
        Dim intFinalCount As Integer = 0
        Dim result As MsgBoxResult

        If (dgvUncounted.Rows.Count > 0) Then
            If MsgBox("Note: There are still " & intInitialCount & " uncounted items in the list. " & vbCrLf & _
           "Are you sure you want to count them all as zero stock ?", _
              MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Ok Then
                Exit Sub
            End If '- zeroise uncounted-

            '-- ok-  count them all as zero (and move into the counted grid..)
            '-- Simulate a manual movement to the Counted Results list.
            '-- Grid rows will be reducing as we go..
            Dim colThisItem As Collection
            TabControlMain.Enabled = False
            Dim gridRow1 As DataGridViewRow
            While (dgvUncounted.Rows.Count > 0)
                gridRow1 = dgvUncounted.Rows(0)  '--there must be a first row.
                '-- Build colThisItem details from row..
                Call mbCollectGridRowData(gridRow1, colThisItem)
                colThisItem.Add(0, "UncountedGridRow")
                result = MsgBoxResult.Yes '-in case no confirmation.
                If chkConfirmSetZeroQty.Checked Then
                    '-- Get confirmation for each item.
                    result = MsgBox("Is it OK to zero-qty the item: " & vbCrLf & _
                                    "  -- " & colThisItem.Item("description") & "??" & vbCrLf & vbCrLf & _
                                    "   (Replying 'Cancel' will exit the loop..)", _
                                    MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question)
                    If (result = MsgBoxResult.Cancel) Then
                        Exit While
                    ElseIf (result = MsgBoxResult.Yes) Then
                        '= Call mbUpdateStocktakeItemCount(colThisItem, 0, 0)  '--Manual Count=0 to Force Zero Stock Qty..
                        '= intFinalCount += 1
                    End If
                End If '-confirm.
                If (result = MsgBoxResult.Yes) Then
                    Call mbUpdateStocktakeItemCount(colThisItem, 0, 0)  '--Manual Count=0 to Force Zero Stock Qty..
                    intFinalCount += 1
                    Call mbReport("Updated: " & colThisItem.Item("description"))
                End If
                grpBoxManualCount.Enabled = False
                DoEvents()
                Thread.Sleep(300)  '--sleep 300 m.second for grids to adjust..
            End While  '-row count-
            MsgBox("ok. We zero-counted " & intFinalCount & " stock lines.", MsgBoxStyle.Information)
            TabControlMain.Enabled = True
            DoEvents()
        End If  '- have rows..-

    End Sub  '--btnCountAllAsZero-
    '= = = = = = = = = =  = = = =
    '-===FF->

    '-- Tab Control Events..

    '-- deselecting..
    '-- Disallow if manual mode and count not committed.

    Private Sub TabControlMain_Deselecting(sender As TabControl, ev As TabControlCancelEventArgs) _
                                                                    Handles TabControlMain.Deselecting
        If LCase(ev.TabPage.Name) = "tabpagemanual" Then
            If grpBoxManualCount.Enabled Then
                ev.Cancel = True
            End If
        End If
    End Sub  '--deselecting..
    '= = = = = = = = = = = == =  = =

    '-- selected-

    Private Sub TabControlMain_selected(sender As TabControl, ev As TabControlEventArgs) _
                                                                    Handles TabControlMain.Selected
        If mbIsInitialising Then Exit Sub

        Select Case LCase(TabControlMain.SelectedTab.Name)
            Case "tabpageauto"
                grpBoxScanAuto.Enabled = True    '-- Yes can scan now-
                labCanScan.Enabled = True
                '== MsgBox("AUTO Tab selected..", MsgBoxStyle.Information)
                txtScanBarcode.Focus()   '=Select()
                DoEvents()
            Case "tabpagemanual"
                grpBoxScanManual.Enabled = True    '-- Yes can scan now-
                '= labCanScan.Enabled = True
                '== MsgBox("MANUAL Tab selected..", MsgBoxStyle.Information)
                txtScanBarcodeManual.Focus()   '=.Select()
                DoEvents()
                '== Case "tabpageResults"
                '== grpBoxScanItem.Enabled = False   '--can't scan now-
                '== Case "tabpageuncounted"
                '== grpBoxScanItem.Enabled = False   '--can't scan now-
            Case Else
                MsgBox("No Tab selected..", MsgBoxStyle.Information)
        End Select
        '== txtScanBarcode.Select()

    End Sub  '--selected..
    '= = = = = = = = = = = = = = = = = = 
    '-===FF->

    '--  MANUAL- Results Grids --..
    '--  MANUAL- Results Grids --..


    Private Sub optShowCounted_CheckedChanged(sender As Object, ev As EventArgs)

        Call mbLoadResultsGrids()

    End Sub  '-optShowCounted_CheckedChanged-
    '= = = = = = = = = = = = = = = == = = = = 

    '- useful stuff.-
    Private Sub mShowCurrentManualGridRow(ByRef gridRow1 As DataGridViewRow, _
                                          ByRef colThisItem As Collection)
        If Not (gridRow1 Is Nothing) Then
            With gridRow1
                '-- Build colThisItem details from row..
                'colThisItem = New Collection
                'colThisItem.Add(gridRow1.Cells("barcode").Value, "barcode")
                'colThisItem.Add(gridRow1.Cells("stock_id").Value, "stock_id")
                'colThisItem.Add(gridRow1.Cells("cat1").Value, "cat1")
                'colThisItem.Add(gridRow1.Cells("cat2").Value, "cat2")
                'colThisItem.Add(gridRow1.Cells("description").Value, "description")
                'colThisItem.Add(gridRow1.Cells("qty_on_record").Value, "qty_on_record")
                'colThisItem.Add(gridRow1.Cells("qty_counted").Value, "qty_counted")
                'colThisItem.Add(gridRow1.Cells("qty_difference").Value, "qty_difference")
                '-- Build colThisItem details from row..
                Call mbCollectGridRowData(gridRow1, colThisItem)
                '- show item as current..-
                txtScanBarcodeManual.Text = colThisItem("barcode")
                labScanManualCat1.Text = colThisItem("cat1")
                labScanManualCat2.Text = colThisItem("cat2")
                labScanManualDescription.Text = colThisItem("description")
                DoEvents()
            End With '=gridrow1-
        End If '-nothing=
    End Sub '-mShowCurrentManualGridRow-
    '= = = = = = = = = = = == = = = = = 
    '-===FF->

    '-- ResultsList Browser Events..--
    '-- ResultsList Browser Events..--
    '-- ResultsList Browser Events..--

    Private Sub dgvResultsList_RowEnter(ByVal sender As Object, _
                                        ByVal ev As DataGridViewCellEventArgs) _
                                        Handles dgvResultsList.RowEnter
        Dim ix, intStock_id As Integer
        Dim s1 As String
        Dim gridRow1 As DataGridViewRow = dgvResultsList.Rows(ev.RowIndex)
        Dim colThisItem As Collection

        With gridRow1  '= dgvResultsList.Rows(ev.RowIndex)
            s1 = .Cells("stock_id").Value
            If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                intStock_id = CInt(s1)
                If (intStock_id > 0) Then  '=And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                    '==Call mbShowCustomerInfo(intCustomer_id)
                    '= show stock info..
                    '== Call mShowCurrentManualGridRow(gridRow1, colThisItem)
                    '== colThisItem.Add(ev.RowIndex, "ResultsGridRow")
                End If
            End If  '-numeric-
        End With  '-grid-
    End Sub  '-row enter.-
    '= = = = = = = = = = = = = =

    '-- cell click.--
    '--  For Manual Entry--

    Private Sub dgvResultsList_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                  ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                        Handles dgvResultsList.CellMouseClick
        Dim ix, intStock_id, intRowx As Integer
        Dim s1 As String
        Dim gridRow1 As DataGridViewRow  '= = dgvResultsList.Rows(eventArgs.RowIndex)
        Dim colThisItem As Collection

        intRowx = eventArgs.RowIndex
        If (intRowx < 0) Then Exit Sub '--in header ?-
        gridRow1 = dgvResultsList.Rows(intRowx)
        With gridRow1  '= dgvResultsList.Rows(ev.RowIndex)
            s1 = .Cells("stock_id").Value
            If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                intStock_id = CInt(s1)
                If (intStock_id > 0) Then  '=And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                    '==Call mbShowCustomerInfo(intCustomer_id)
                    '= show stock info..
                    Call mShowCurrentManualGridRow(gridRow1, colThisItem)
                    colThisItem.Add(eventArgs.RowIndex, "ResultsGridRow")
                End If
            End If  '-numeric-
        End With  '-grid-
    End Sub  '-click-
    '= = = = = = = = == = == 
    '-===FF->

    '-- cell DOUBLE click.--
    '--  For Manual Entry--

    Private Sub dgvResultsList_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvResultsList.CellMouseDoubleClick
        Dim lRow, lCol As Integer
        Dim intStock_id As Integer
        Dim colThisItem As Collection

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            If lRow < 0 Then Exit Sub '-header-

            Dim gridRow1 As DataGridViewRow = dgvResultsList.Rows(lRow)
            If (lRow >= 0) And (dgvResultsList.Rows.Count > 0) Then  '--selected a row.--
                With gridRow1
                    Dim s1 As String = .Cells("stock_id").Value
                    If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                        intStock_id = CInt(s1)
                        If (intStock_id > 0) Then  '=And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                            '= show stock info..
                            Call mShowCurrentManualGridRow(gridRow1, colThisItem)
                            colThisItem.Add(eventArgs.RowIndex, "ResultsGridRow")
                            mColCurrentScanItemDetails = colThisItem
                            '-- SET up for manual qty entry..-
                            grpBoxScanAuto.Enabled = False
                            grpBoxManualCount.Enabled = True
                            txtManualQty.Text = ""
                            txtManualExpected.Text = ""
                            '== TabControlMain.Enabled = False   '--must complete manual box..
                            txtManualQty.Select()
                            '- wait for commit (save0=)--
                            frameBrowseResults.Enabled = False
                        End If
                    End If  '-numeric-
                End With
            End If  '-selected-
        End If  '--left..-
    End Sub  '-dgvResultsList_CellMouseDBLClickEvent-
    '= = = = = = = = = = = = = = = = = = = = = =

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvResultsList_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvResultsList.Sorted
        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvResultsList.SortedColumn
        sName = currentColumn.HeaderText
        Call mBrowseResults.SortColumn(sName)
    End Sub  '--sorted-
    '= = = = = = = = =  = = =
    '-===FF->

    '-- RESULTS (Counted) STOCK Browser.. txt FIND Activity.--
    '-- RESULTS (Counted) STOCK Browser.. txt FIND Activity.--
    '--BROWSING Counted STOCK.. --

    '-- key activity---  select row to edit--
    Private Sub txtResultsFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                        Handles txtResultsFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim intRow, lCol As Integer
        Dim intStock_id As Integer
        Dim colKeys, colRowValues, colThisItem As Collection
        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If dgvResultsList.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                intRow = dgvResultsList.SelectedRows(0).Cells(0).RowIndex
                If (intRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    Call mBrowseResults.SelectRecord(intRow, colKeys, colRowValues)
                    If (intStock_id > 0) Then '= And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                        '= Call mbShowCustomerInfo(intCustomer_id)
                        Call mShowCurrentManualGridRow(dgvResultsList.SelectedRows(0), colThisItem)
                        colThisItem.Add(intRow, "ResultsGridRow")

                        '--  Start manual Edit  ??



                    End If '-stock-id-
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

    Private Sub txtResultsFind_Enter(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles txtResultsFind.Enter
        LabResultsFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        LabResultsFind.Font = VB6.FontChangeBold(LabResultsFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = = = = = = = = 

    Private Sub txtResultsFind_Leave(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtResultsFind.Leave
        LabResultsFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        LabResultsFind.Font = VB6.FontChangeBold(LabResultsFind.Font, False)

    End Sub '--Leave focus--
    '= = = = = = = = = = = = = = =  =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtResultsFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtResultsFind.TextChanged

        If mbIsInitialising Then Exit Sub
        '-- go Find..
        Call mBrowseResults.Find(txtResultsFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- Stock Browser..  Full text Search..--
    '-- Stock Browser..  Full text Search..--

    'Private Sub cmdResultsSearch_Click(ByVal sender As System.Object, _
    '                                     ByVal ev As System.EventArgs)
    '    Dim sWhere As String = ""
    '    Dim sSql As String '--search sql..-- 
    '    '= Dim s1, s2 As String
    '    Dim asColumns As Object

    '    '--  rebuild Search Columns and call makeTextSearch...-
    '    '-- arg can be multiple tokens..--
    '    '===asArgs = Split(Trim(txtSearch(index).Text))
    '    '--  now in the Interface..--
    '    '== asColumns = mRetailHost1.stockSearchColumns()
    '    asColumns = New Object() _
    '                  {"barcode", "cat1", "cat2", "description"}

    '    '-- Every Arg. must be represented in some column in the row for--
    '    '--   the row to be selected.--
    '    '-- So we need a WHERE clause that looks like:  --
    '    '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
    '    '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
    '    '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
    '    '-- (poor man's version of FULL-TEXT search..)  --
    '    sSql = gbMakeTextSearchSql(Trim(txtResultsSearch.Text), asColumns)
    '    '--add srch args if any..-
    '    If (sSql <> "") Then
    '        If (sWhere <> "") Then sWhere = sWhere + " AND "
    '        sWhere = sWhere + sSql
    '    End If
    '    '=Call mbBrowseCustomerTable(sWhere)
    '    Call mbLoadResultsGrids()

    'End Sub '-cmdResultsSearch-
    '= = = = = = = = = = = = =  =

    '== Private Sub cmdClearResultsSearch_Click(ByVal sender As System.Object, _
    '==                                         ByVal e As System.EventArgs)
    '==    txtResultsSearch.Text = ""
    '==    System.Windows.Forms.Application.DoEvents()
    '==    Call cmdResultsSearch_Click(cmdResultsSearch, New System.EventArgs())
    '== End Sub  '-ClearStockSearch-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- Start UNCOUNTED Grid Browsing..
    '-- Start Uncounted Browsing..

    Private Sub dgvUncounted_CellContentClick(sender As Object, eventArgs As DataGridViewCellEventArgs) _
                                              Handles dgvUncounted.CellContentClick

    End Sub  '--dgvUncounted_CellContentClick-
    '= = = = = = = = = = = = = = = = = = = = = ==

    Private Sub dgvUncounted_RowEnter(ByVal sender As Object, _
                                    ByVal ev As DataGridViewCellEventArgs) _
                                    Handles dgvUncounted.RowEnter
        Dim ix, intStock_id As Integer
        Dim s1 As String
        Dim gridRow1 As DataGridViewRow = dgvUncounted.Rows(ev.RowIndex)
        Dim colThisItem As Collection

        With gridRow1  '= dgvResultsList.Rows(ev.RowIndex)
            s1 = .Cells("stock_id").Value
            If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                intStock_id = CInt(s1)
                If (intStock_id > 0) Then  '=And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                    '==Call mbShowCustomerInfo(intCustomer_id)
                    '= show stock info..
                    '== Call mShowCurrentManualGridRow(gridRow1, colThisItem)
                    '= colThisItem.Add(ev.RowIndex, "UncountedGridRow")
                End If
            End If  '-numeric-
        End With  '-grid-
    End Sub  '-row enter.-
    '= = = = = = = = = = = = = =

    '-- cell click.--
    '--  For Manual Entry--

    Private Sub dgvUncounted_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                  ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                        Handles dgvUncounted.CellMouseClick
        Dim ix, intStock_id As Integer
        Dim s1 As String
        Dim colThisItem As Collection

        If (eventArgs.RowIndex < 0) Then Exit Sub '--header-
        Dim gridRow1 As DataGridViewRow = dgvUncounted.Rows(eventArgs.RowIndex)
        With gridRow1  '= dgvResultsList.Rows(ev.RowIndex)
            s1 = .Cells("stock_id").Value
            If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                intStock_id = CInt(s1)
                If (intStock_id > 0) Then  '=And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                    '==Call mbShowCustomerInfo(intCustomer_id)
                    '= show stock info..
                    Call mShowCurrentManualGridRow(gridRow1, colThisItem)
                    colThisItem.Add(eventArgs.RowIndex, "UncountedGridRow")
                End If
            End If  '-numeric-
        End With  '-grid-
    End Sub  '-click-
    '= = = = = = = = == = == 
    '-===FF->

    '-- cell DOUBLE click.--
    '--  For Manual Entry--

    Private Sub dgvUncounted_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles dgvUncounted.CellMouseDoubleClick
        Dim lRow, lCol As Integer
        Dim intStock_id As Integer
        Dim colThisItem As Collection

        If eventArgs.Button = System.Windows.Forms.MouseButtons.Left Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            If (eventArgs.RowIndex < 0) Then Exit Sub '--header-
            Dim gridRow1 As DataGridViewRow = dgvUncounted.Rows(lRow)
            If (lRow >= 0) And (dgvUncounted.Rows.Count > 0) Then  '--selected a row.--
                With gridRow1
                    Dim s1 As String = .Cells("stock_id").Value
                    If IsNumeric(s1) AndAlso (CInt(s1) > 0) Then
                        intStock_id = CInt(s1)
                        If (intStock_id > 0) Then  '=And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                            '= show stock info..
                            Call mShowCurrentManualGridRow(gridRow1, colThisItem)
                            colThisItem.Add(eventArgs.RowIndex, "UncountedGridRow")
                            mColCurrentScanItemDetails = colThisItem
                            '-- SET up for manual qty entry..-
                            grpBoxScanAuto.Enabled = False
                            grpBoxManualCount.Enabled = True
                            txtManualQty.Text = ""
                            txtManualExpected.Text = ""
                            '== TabControlMain.Enabled = False   '--must complete manual box..
                            txtManualQty.Select()
                            '- wait for commit (save0=)--
                            frameBrowseUncounted.Enabled = False
                        End If
                    End If  '-numeric-
                End With
            End If  '-selected-
        End If  '--left..-
    End Sub  '-dgvUncounted_CellMouseDBLClickEvent-
    '= = = = = = = = = = = = = = = = = = = = = =

    '--mouse activity---  select col-headers--
    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dgvUncounted_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles dgvUncounted.Sorted
        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = dgvUncounted.SortedColumn
        sName = currentColumn.HeaderText
        Call mBrowseUncounted.SortColumn(sName)
    End Sub  '--sorted-
    '= = = = = = = = =  = = =
    '-===FF->

    '-- RESULTS (Uncounted) STOCK Browser.. txt FIND Activity.--
    '-- RESULTS (Uncounted) STOCK Browser.. txt FIND Activity.--
    '--BROWSING Uncounted STOCK.. --

    '-- key activity---  select row to edit--
    Private Sub txtUncountedFind_KeyPress(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) _
                                              Handles txtUncountedFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim intRow, lCol As Integer
        Dim intStock_id As Integer
        Dim colKeys, colRowValues, colThisItem As Collection
        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If dgvUncounted.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                intRow = dgvUncounted.SelectedRows(0).Cells(0).RowIndex
                If (intRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    Call mBrowseUncounted.SelectRecord(intRow, colKeys, colRowValues)
                    If (intStock_id > 0) Then '= And (intCustomer_id <> mIntCustomer_id) Then '-- has changed..-
                        '= Call mbShowCustomerInfo(intCustomer_id)
                        Call mShowCurrentManualGridRow(dgvUncounted.SelectedRows(0), colThisItem)
                        colThisItem.Add(intRow, "UncountedGridRow")

                        '--  Start manual Edit  ??



                    End If '-stock-id-
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

    Private Sub txtUncountedFind_Enter(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles txtUncountedFind.Enter
        labUncountedFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        labUncountedFind.Font = VB6.FontChangeBold(labUncountedFind.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = = = = = = = = 

    Private Sub txtUncountedFind_Leave(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles txtUncountedFind.Leave
        labUncountedFind.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        labUncountedFind.Font = VB6.FontChangeBold(labUncountedFind.Font, False)

    End Sub '--Leave focus--
    '= = = = = = = = = = = = = = =  =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFind.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Private Sub txtUncountedFind_TextChanged(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs) Handles txtUncountedFind.TextChanged

        If mbIsInitialising Then Exit Sub
        '-- go Find..
        Call mBrowseUncounted.Find(txtUncountedFind)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '-- Stock Browser..  Full text Search..--
    '-- Stock Browser..  Full text Search..--

    'Private Sub cmdUncountedSearch_Click(ByVal sender As System.Object, _
    '                                     ByVal ev As System.EventArgs)
    '    Dim sWhere As String = ""
    '    Dim sSql As String '--search sql..-- 
    '    '= Dim s1, s2 As String
    '    Dim asColumns As Object

    '    '--  rebuild Search Columns and call makeTextSearch...-

    '    '-- arg can be multiple tokens..--
    '    '===asArgs = Split(Trim(txtSearch(index).Text))
    '    '--  now in the Interface..--
    '    '== asColumns = mRetailHost1.stockSearchColumns()
    '    asColumns = New Object() _
    '                  {"barcode", "cat1", "cat2", "description"}

    '    '-- Every Arg. must be represented in some column in the row for--
    '    '--   the row to be selected.--
    '    '-- So we need a WHERE clause that looks like:  --
    '    '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
    '    '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
    '    '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
    '    '-- (poor man's version of FULL-TEXT search..)  --
    '    sSql = gbMakeTextSearchSql(Trim(txtUncountedSearch.Text), asColumns)
    '    '--add srch args if any..-
    '    If (sSql <> "") Then
    '        If (sWhere <> "") Then sWhere = sWhere + " AND "
    '        sWhere = sWhere + sSql
    '    End If
    '    '=Call mbBrowseCustomerTable(sWhere)
    '    Call mbLoadResultsGrids()

    'End Sub '-cmdUncountedSearch-
    '= = = = = = = = = = = = =  =

    'Private Sub cmdClearUncountedSearch_Click(ByVal sender As System.Object, _
    '                                         ByVal e As System.EventArgs)
    '    txtUncountedSearch.Text = ""
    '    System.Windows.Forms.Application.DoEvents()
    '    Call cmdUncountedSearch_Click(cmdResultsSearch, New System.EventArgs())

    'End Sub  '-ClearStockSearch-
    '= = = = = = = = = = = = = = = =

    '== end Uncounted Browsing..
    '= = = = = = = = = = == = = 
    '= = = = = = = = = = = = = = = =
    '-===FF->

    Private Sub cboPrinters_SelectedIndexChanged(sender As Object, ev As EventArgs) _
                                                  Handles cboPrinters.SelectedIndexChanged

        If (cboPrinters.SelectedIndex >= 0) Then
            msReportPrinterName = cboPrinters.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_ReportPrtSettingKey, msReportPrinterName) Then
                '= gbSaveLocalSetting(gsLocalSettingsPath, k_invoicePrtSettingKey, msReportPrinterName) Then
                MsgBox("Failed to save Report printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-

    End Sub  '-cboPrinters_SelectedIndexChanged-
    '= = = = = = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- C o m m i t ---
    '-- C o m m i t ---
    '-- C o m m i t ---

    Private Sub btnCommitAll_Click(sender As Object, e As EventArgs) Handles btnCommitAll.Click

        If dgvResultsList.Rows.Count <= 0 Then
            MsgBox("Nothing counted, so nothing to commit!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If mbIsSingleStocktake Then '-Free Ranging..
            '- ok..  no Zeroing allowed..
        Else '-have a plan. must fulfill it.
            If (dgvUncounted.Rows.Count > 0) Then
                MsgBox("Note: There are still " & dgvUncounted.Rows.Count & " uncounted items in the list. " & vbCrLf & _
                          "You can mark them all to zero if needed..", _
                            MsgBoxStyle.Exclamation)
                Exit Sub
            Else  '-all counted-
                '- can go on-
            End If  '-rows-
        End If  '-Free or Plan.
        If MsgBox("NB: This commit can't be reversed.  The Stocktake will be completed.." & vbCrLf & vbCrLf & _
                  "OK to commit this stocktake ?", _
                     MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Ok Then
            Exit Sub
        End If '- commit uncounted-
        btnCommitAll.Enabled = False

        '--Press on.-
        TabControlMain.SelectedTab = TabPageManual '== "TabPageManual"  '-so we can see Results..-
        TabControlMain.Enabled = False  '--can't fiddle with it any more..

        Dim sLog As String = "Stocktake #" & mIntStocktake_id & " Commit Log.." & vbCrLf
        Dim sSql, sErrorMsg, sBarcode, sDescr As String
        Dim oleTrans1 As OleDbTransaction
        Dim intBatchMaxSize As Integer = 100
        Dim intStock_id, intQty, intNewQty As Integer
        Dim intBatchCount, intAffected, intTotalItemsUpdated As Integer
        '- Update all stock items in the Counted Results Grid.
        '-- Where the counted amt is different from the original table value..
        intTotalItemsUpdated = 0
        '- START TRANS..
        oleTrans1 = mCnnSql.BeginTransaction
        '-- Send batches of UPDATES-
        intBatchCount = 0
        sSql = ""
        For Each gridRow1 As DataGridViewRow In dgvResultsList.Rows
            If (intBatchCount >= intBatchMaxSize) Then  '--Send batch to server.
                If Not gbExecuteSql(mCnnSql, sSql, True, oleTrans1, intAffected, sErrorMsg) Then
                    MsgBox("ERROR- Sql batch Op.. UPDATE failed-" & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                    '-was rolled back.
                    Exit Sub
                End If  '--exec-
                intBatchCount = 0
                sSql = ""
            End If
            '--add next stock item to update.
            intStock_id = CInt(gridRow1.Cells("stock_id").Value)
            sBarcode = gridRow1.Cells("barcode").Value
            sDescr = gridRow1.Cells("description").Value
            intQty = CInt(gridRow1.Cells("qty_on_record").Value)
            intNewQty = CInt(gridRow1.Cells("qty_counted").Value)
            '-- update if count different from record.
            If intQty <> intNewQty Then
                sSql &= "UPDATE dbo.stock SET qtyInStock=" & CStr(intNewQty) & " WHERE stock_id=" & CStr(intStock_id) & ";" & vbCrLf
                intBatchCount += 1
                intTotalItemsUpdated += 1
                sLog &= "Updated: b/code: " & _
                    sBarcode & "; " & VB.Left(sDescr, 20) & "; OldQty/Counted: " & intQty & "/" & intNewQty & "." & vbCrLf
            End If
        Next gridRow1
        '-- last batch- (if any)-
        If (intBatchCount > 0) Then
            If Not gbExecuteSql(mCnnSql, sSql, True, oleTrans1, intAffected, sErrorMsg) Then
                MsgBox("ERROR in Sql Batch Op..- Last Batch UPDATE failed-" & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                '-was rolled back.
                Exit Sub
            End If  '--exec-
        End If
        '- Update the stocktake table to SET is_committed=1
        sSql = "UPDATE dbo.stocktake SET is_committed=1  WHERE stocktake_id=" & CStr(mIntStocktake_id) & ";" & vbCrLf
        If Not gbExecuteSql(mCnnSql, sSql, True, oleTrans1, intAffected, sErrorMsg) Then
            MsgBox("ERROR in Commit- Set Committed UPDATE failed-" & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
            '-was rolled back.
            Exit Sub
        End If  '--exec-

        '-- COMMIT TRANS.-
        Try
            oleTrans1.Commit()
            mbIsCommitted = True   '-remember-
        Catch ex As Exception
            MsgBox("ERROR- Stocktake Commit op. failed-" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            Exit Sub
        End Try

        '=MsgBox("Commit completed ok..  log is:" & vbCrLf & vbCrLf & sLog & vbCrLf)
        MsgBox("Commit completed ok.. " & vbCrLf & _
                 "  We updated  " & intTotalItemsUpdated & " stock items..", MsgBoxStyle.Information)

        '-- Build Discrepancy report datagrid from StocktakeItems Table..
        '-- IGNORE items not counted ( count = -1)
        '--  goods_taxCode is varchar(7). +GSTpercentage-
        Dim dtReport As DataTable
        Dim sGST_adjust As String = CStr((100 + mDecGST_percentage) / 100)

        sSql = "SELECT STI.cat1, STI.cat2, STI.Description, STI.Barcode, STI.stock_id, "
        sSql &= "   qty_on_record AS qty_exp, qty_counted AS counted, "
        sSql &= "     (qty_counted- qty_on_record) AS qty_diff,   "
        sSql &= "     (costExTax*" & sGST_adjust & ") AS unit_cost, "
        sSql &= "     (((qty_counted- qty_on_record)*costExTax)*" & sGST_adjust & ") AS Value_diff   "
        sSql &= " FROM dbo.StocktakeItems AS STI "
        sSql &= "   JOIN dbo.stock ON stock.stock_id=STI.stock_id "
        sSql &= "  WHERE (qty_on_record<> qty_counted) AND (qty_counted>=0) "
        sSql &= " ORDER BY STI.cat1, STI.cat2, STI.Description; "

        If Not gbGetDataTable(mCnnSql, dtReport, sSql) Then
            MsgBox("Error in getting StocktakeItems Report table.." & vbCrLf & _
                                           gsGetLastSqlErrorMessage(), MsgBoxStyle.Exclamation)
            '==Me.Close()
            Exit Sub
            '== Else  '-ok-
        End If  '-get-
        '-- ok- Dump table into Grid..
        dgvResultsList.DataSource = Nothing '--disconnect-

        If Not gbDumpTableToGrid(dtReport, dgvResultsList) Then
            MsgBox("Failed to load report grid. ", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '- Grid cols are now: 
        '-  cat1, cat2, description, barcode, stock_id, qty_exp, 
        '--                      counted, Qty_diff,  Cost_inc,  Value_diff -
        '-- adjust widths.
        With dgvResultsList
            .Columns(0).Width = 50
            .Columns(1).Width = 50
            .Columns(2).Width = 180  '--widen description column
            .Columns(3).Width = 70  '--barcode column
            .Columns(4).Width = 46  '--id column
            .Columns(5).Width = 40  '--qty column
            .Columns(6).Width = 40  '--qty column
            .Columns(7).Width = 40  '--qty column
            .Columns(8).Width = 60  '--unit cost-
            .Columns(9).Width = 80  '--Value diff. column
        End With
        '-- fix cell alignment for numeric cols..
        With dgvResultsList
            .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '-id-
            .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '--qty exp-
            .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '--qty counted-
            .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '--qty-
            .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '--amount-
            .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight  '--amount-
        End With

        '-- reformat value column, and compute overall total..
        Dim decTotalValue As Decimal = 0
        Dim decAmount As Decimal = 0
        Dim rowx As Integer = 0
        For Each row1 As DataRow In dtReport.Rows
            With dgvResultsList
                .Rows(rowx).Cells(8).Value = FormatCurrency(CDec(row1.Item("unit_cost")), 2)
                decAmount = CDec(row1.Item("Value_diff"))
                .Rows(rowx).Cells(9).Value = FormatCurrency(decAmount, 2)  '-extended cost.-
                decTotalValue += decAmount
            End With
            rowx += 1
        Next row1
        '-keep rowx-
        '- Add a new row for grand total.
        Dim gridRowReport = New DataGridViewRow  '--prepare datagrid report row..
        With dgvResultsList
            .Rows.Add(gridRowReport)
            .Rows(rowx).Cells(2).Value = "Grand Total Discrepancy Value"
            .Rows(rowx).Cells(9).Value = FormatCurrency(decTotalValue, 2)
        End With

        '- print the report from the Grid if there is a printer..-
        TabControlReport.SelectedTab = TabControlReport.TabPages("TabPagePrint")
        grpBoxPrint.Enabled = True
        btnPrint.Enabled = True
        '= Call mbPrintReport(dgvResultsList)

    End Sub  '--commit-
    '= = = = = = = = = = = = =
    '-===FF->

    '---btnPrint_Click-

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        Call mbPrintReport(dgvResultsList)

    End Sub  '---btnPrint_Click-
    '= = = = = = = = = = = = = = = =
    '-===FF->

    '- cancel--

    Private Sub cmdDestroy_Click(sender As Object, ev As EventArgs) Handles cmdDestroy.Click
        Dim intAffected As Integer
        Dim sErrorMsg As String

        If (mIntStocktake_id > 0) Then  '--have a stocktake--
            If MsgBox("Ok to abandon this stocktake run forever ?", _
                           MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Ok Then
                '-- destroy..
                '-- Ask to conifrm if more than 10 thing in the Results Grid..
                Dim intCount As Integer = dgvResultsList.Rows.Count
                If (intCount > 10) Then
                    If MsgBox("You have already counted " & intCount & " different stock items." & vbCrLf & _
                                "Sure you want to abandon this stocktake run forever ?", _
                                   MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) <> MsgBoxResult.Yes Then
                        '--no.. chickened out..
                        Exit Sub
                    End If '-confirm.
                End If '-results-
                '--  Now to destroy it..
                Dim sSql As String = "UPDATE dbo.Stocktake "
                sSql &= "SET is_cancelled=1 "
                sSql &= "  WHERE (stocktake_id=" & CStr(mIntStocktake_id) & ") "
                If Not gbExecuteCmd(mCnnSql, sSql, intAffected, sErrorMsg) Then
                    MsgBox("ERROR updating Stocktake Table." & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                Else
                    mIntStocktake_id = -1  '--gone-
                    MsgBox("This Stocktake has now been relegated to history.", MsgBoxStyle.Information)
                    '= Me.Close()
                    Call close_me()
                End If  '-exec-
            Else  '--no-
                MsgBox("You can use the X (Close) button to exit, and then resume this stocktake later. ", MsgBoxStyle.Information)
            End If '-ok -
        Else
            '=Me.Close()
            Call close_me()
        End If  '--stocktake.-
    End Sub  '--destroy/cancel..
    '= = = = = = = = = = = = = = =
    '-===FF->

    '-Exit-
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        '= Me.Close()
        Call close_me()
    End Sub  '-exit
    '= = = == ==  = =

    '-- new-  User-Control.

    Private Sub close_me()
        'Dim bCancel As Boolean = False

        'If (mIntStocktake_id > 0) Then  '-started
        '    If mbIsCommitted Then
        '        If MsgBox("This stocktake has been committed, and the stock records updated.." & vbCrLf & _
        '                  "This is your last chance to print the Stocktake Report," & vbCrLf & _
        '                   " as after exit this stocktake will no longer be accessible." & vbCrLf & vbCrLf & _
        '                   "OK to exit and finish with this Stocktake ?", _
        '                   MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = vbYes Then
        '            bCancel = False  '--let it exit---
        '        Else '--chikened out- cancel exit-
        '            bCancel = True   '--cant close yet--'--was mistake..  keep going..
        '        End If  '-yes/no-
        '    Else  '--no. it's still avlive-
        '        MsgBox("You can resume this stocktake later..", MsgBoxStyle.Information)
        '        bCancel = False  '--let it go to close---
        '    End If
        'End If  '-stocktake-
        'If bCancel Then Exit Sub '--keep alive.-

        If Not mbCanStocktakeClose() Then
            Exit Sub '--stay in..
        End If

        '- inform parent.-
        '- Report to Parent..-
        ' Raise an event get parent to close child..
        RaiseEvent PosChildClosing(Me.Name)

        '= if (this.InvokeDel != null)
        '= InvokeDel.Invoke(this.txtMsg.Text);
        'If Not (Me.delReport Is Nothing) Then
        '    delReport.Invoke(Me.Name, "FormClosed", "")
        'End If
        ''= End If  '-cancel-
        'Me.Dispose()

    End Sub  '-close me..
    '= = = = = = = = = = == 



    '--= = =Query  u n l o a d = = = = = = =
    '--= = =Query  u n l o a d = = = = = = =

    'Private Sub frmStockTake_FormClosing(ByVal eventSender As System.Object, _
    '                                         ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
    '                                            Handles Me.FormClosing
    '    Dim intCancel As Boolean = eventArgs.Cancel
    '    Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason

    '    '== Call gbLogMsg(gsRuntimeLogPath, "== JobMatixPOS Stock form is closing.." & vbCrLf & vbCrLf & _
    '    '==                                                  "= = = = = = = = = = = =" & vbCrLf & vbCrLf)
    '    Select Case intMode
    '        Case System.Windows.Forms.CloseReason.WindowsShutDown, _
    '                  System.Windows.Forms.CloseReason.TaskManagerClosing, _
    '                           System.Windows.Forms.CloseReason.FormOwnerClosing '== NOT for vb.net.. , vbFormCode
    '            intCancel = 0 '--let it go--
    '        Case System.Windows.Forms.CloseReason.UserClosing
    '            If (mIntStocktake_id > 0) Then  '-started
    '                If mbIsCommitted Then
    '                    If MsgBox("This stocktake has been committed, and the stock records updated.." & vbCrLf & _
    '                              "This is your last chance to print the Stocktake Report," & vbCrLf & _
    '                               " as after exit this stocktake will no longer be accessible." & vbCrLf & vbCrLf & _
    '                               "OK to exit and finish with this Stocktake ?", _
    '                               MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = vbYes Then
    '                        intCancel = 0 '--let it exit---
    '                    Else '--chikened out- cancel exit-
    '                        intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '                    End If  '-yes/no-
    '                Else  '--no. it's still avlive-
    '                    MsgBox("You can resume this stocktake later..", MsgBoxStyle.Information)
    '                    intCancel = 0 '--let it go---
    '                End If
    '            End If
    '            '==  intCancel = 1  '--cant close yet--'--was mistake..  keep going..
    '        Case Else
    '            intCancel = 0 '--let it go--
    '    End Select '--mode--
    '    eventArgs.Cancel = intCancel
    'End Sub '--closing--
    '= = = = = = =  = = = = = = = == 

End Class  '-ucChildStocktake-
'= = = = =  = = = = = = ==== = 

'--- end form --