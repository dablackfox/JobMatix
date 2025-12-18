Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Imports System.Drawing.Printing
Imports System.Windows.Forms.Application
Imports System.Data
Imports System.Data.OleDb

Public Class clsRAsMain33

    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
    '==
    '==  NEW CLASS 3.3.3311.309-  09-Mar-2016=
    '==
    '==    >> Class derived from frmRAsMain33 -
    '==      --   RAs Main Form Controls now grafted onto JobMatix Main Screen
    '==              All RAs Main suport code exported into this class.
    '==
    '==    >> All systemInfo work now via Class clsSystemInfo ..
    '==    >> All localSettings work now via Class clsLocalSettings..
    '==
    '==   grh- 3311.420=
    '==         >>   Exit Do (print loop) if print failed-
    '==         >>  make sure a printer is sekected if no previous..
    '==
    '==  -- 3327.0119- 19-Jan-2017-
    '==         >>-- Fixes ERROR in clsRAsMain33- SQL error when NO RAs on file. (empty list)..-- 
    '==         >>-- Fix Package Supplier Browse bug when no RA's..-- 
    '==
    '== = = = =
    '==
    '==  -- 3357.0205- 05-Feb-2017-
    '==         >>-- Update to go with Updated POS 3307==-- 
    '==             (Supplier GoodsReturns Update Function to be called from RAs.)...
    '==         >>--RAs- (modCreateJobs) Add column RM_SerialAudit_id" "to RAItems Table ==-- 
    '==               and Expand RM_ItemBarcode to 40 chars.
    '==         >>--RAs-  IF Retail System is JobMatix POS, then call PO-GoodsReturned when Goods Sent. ==-- 
    '==
    '==  -- 3401.0319- 19-Mar-2017-
    '==         >>--Tree Items.. expand SupplierName & ItemDescr using the extra form width.==-- 
    '==   v3.4.3403.0608 -- 08-Jun2017= x-
    '==         --  Refresh Supplier RAs Listview after "Sending" a package.
    '==   v3.4.3403.0711 -- 11Jul2017= - FIX UP for release.
    '==         -- Fix RA's main problem with ViewRA showing the wrong RA..-
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    Const K_STATUS_GOODSSENT As String = "50-GoodsSentToSupplier"

    '--  STUFF for TreeView Backcolour..--= = = = = = = = =
    '--  STUFF for TreeView Backcolour..--= = = = = = = ==
    Private Declare Function SendMessage Lib "user32" _
                          Alias "SendMessageA" (ByVal hwnd As Integer, _
                                                ByVal wMsg As Integer, _
                                                 ByVal wParam As Integer, _
                                                     ByVal lParam As Integer) As Integer

    Private Declare Function InvalidateRect Lib "user32" (ByVal hwnd As Integer, _
                                                         ByVal lpRect As Integer, _
                                                          ByVal bErase As Integer) As Integer

    Private Declare Function UpdateWindow Lib "user32" (ByVal hwnd As Integer) As Integer

    Private Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hwnd As Integer, _
                                                                               ByVal nIndex As Integer) As Integer

    Private Declare Function SetWindowLong Lib "user32" _
                                             Alias "SetWindowLongA" (ByVal hwnd As Integer, _
                                                                   ByVal nIndex As Integer, _
                                                                    ByVal dwNewLong As Integer) As Integer

    Private Const GWL_STYLE As Short = -16
    Private Const TVM_SETBKCOLOR As Short = 4381
    Private Const TVM_GETBKCOLOR As Short = 4383
    Private Const TVS_HASLINES As Short = 2
    '-- redraw--
    Private Const WM_SETREDRAW As Integer = &HBS
    '= = = = = = = = = = = = = = = = = = = = ==  =

    Private Const K_SAVESETTINGSPATH As String = "localRAsSettings.txt"
    Private Const k_RA_PrtSettingKey As String = "RA_PRTCOLOUR"


    Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    Const K_RTFLINEBREAK As String = "\par "
    Const k_MAXPRINTCOLS As Short = 12 '--max columns we can print from grid..-
    '= = = = = = = = =  = = = =
    Private mbIsInitialising As Boolean = True

    Private mFrmParent As Form  '--mother form (JobMatix Main)-
    '-- Controls are on Parent Form.
    Private mLabStatus As Label  '--main screen..

    Private mFrameRAsTab As GroupBox
    Private mLabShowSearchRAs As Label
    '== Private mToolStripRAs As ToolStrip
    Private mTabControlRAs As TabControl

    '-- Grouped in Boxes..
    Private mFrameRAsTree As GroupBox
    Private mTvwRAs As System.Windows.Forms.TreeView
    Private mChkAutoRefreshRAs As CheckBox
    Private mCmdRefreshRAsTree As Button
    Private m_OptRATreeSort_0 As RadioButton
    Private m_OptRATreeSort_1 As RadioButton
    Private m_OptRATreeSort_2 As RadioButton
    Private m_OptRATreeSort_3 As RadioButton
    Private mLabTreeStatusRAs As Label

    Private mFrameBrowseRAs As GroupBox
    Private mToolbarRAsGrid As ToolStrip
    Private mDataGridViewRAs As DataGridView
    Private mLabFindRAs As Label
    Private mTxtFindRAs As TextBox
    Private mTxtRASearch As TextBox
    Private mCmdRASearch As Button
    Private mCmdClearRASearch As Button
    Private mLabRecCountRAs As Label

    '-- Suppliers Frame-
    Private mFrameRA_suppliers As GroupBox
    Private mDgvRA_suppliers As DataGridView
    Private mListViewSupplierRAs As ListView
    '== Private mChkShowGrantedRAsOnly As CheckBox
    Private mLabFindSupplier As Label
    Private mTxtFindSupplier As TextBox
    Private mLabRecCountSupplier As Label
    Private mChkShowGrantedRAsOnly As CheckBox

    '-- supplier info panel-
    Private mPanelRA_supplierInfo As Panel
    Private mChkSelectAllRAsGranted As CheckBox
    '= Private mLabRA_supplierName As Label
    Private mTxtRA_supplierName As TextBox
    Private mBtnRAsUpdateGroupSent As Button
    Private mCboRAs_A4Printers As ComboBox

    Private mFrameRADetails As GroupBox
    Private mTxtRACat1 As TextBox
    Private mLabRA_id As Label
    Private mLabRAStatusFriendly As Label
    Private mTxtRAItem As TextBox
    Private mTxtRACreated As TextBox
    Private mTxtRAUpdated As TextBox
    Private mTxtRASerialNo, mTxtRAProdBarcode, mTxtRAProblem As TextBox
    Private mLabRASupplier As Label
    Private mTxtRASupplier, mTxtRASupplierRANo, mTxtFreightTrackNo As TextBox
    Private mTxtRACustomerName, mTxtRACustomerContact As TextBox
    Private mBtnRA_attachments, mCmdViewRecordRAs As Button

    Private mbStartupDone As Boolean = False

    '= Private mColQuote As Collection
    '= Private mColQuoteLines As Collection '--of collections (lines..)==

    Private mCnnSql As OleDbConnection '== ADODB.Connection
    '== Private mCnnShape As ADODB.Connection

    Private msServer As String
    Private msSqlVersion As String
    Private msInputDBNameJobs As String

    Private msSqlDbName As String
    Private mColSqlDBInfo As Collection '--  jobs DB info--

    Dim msComputerName As String '--local machine--
    Dim msAppPath As String
    '====Private msSaveJetPath As String
    Private msInstallPath As String
    Private msJobMatixVersion As String = "JobMatix"

    '-- Results box position..-
    Private mlResultsTop As Integer = 84
    Private mlResultsLeft As Integer = 8

    '== Private mlFormDesignHeight As Integer = 760  '-- starting dimensions..-
    '= Private mlFormDesignWidth As Integer = 998   '-- starting dimensions..-

    Private mLngSelectedRow As Integer '--selected browse row..-
    Private mLngSelectedRowRAs As Integer '--selected browse row..-
    Private mLngSelectedRowSupplier As Integer

    Private mLngRAsTreeBGColour As Integer
    Private mLngGridBGColourRAs As Integer

    '==3311.305=Private mSdSettings As clsStrDictionary   '==  Scripting.Dictionary
    '==3311.305=Private mSdSystemInfo As clsStrDictionary   '==   Scripting.Dictionary
    '==3311.305=Private mColSystemInfo As Collection
    Private msSettingsPath As String = ""
    '==3311.305=
    Private mLocalSettings1 As clsLocalSettings
    '=3311.305=
    Private mSysInfo1 As clsSystemInfo

    '--staff--
    Private mIntStaff_Id As Integer = -1
    Private msStaffBarcode As String = ""
    Private msStaffName As String = ""
    Private mlStaffTimeout As Integer = 1 '--Not used in this class now...--

    Private msCurrentUser As String
    Private mbIsSqlAdmin As Boolean

    '--  Jet-- RM --
    Private msJetPathInfoKey As String '--  eg- "RM_JetPath_computername" --

    Private msJetDbName As String
    Private msJetUid As String '--jet-
    Private msJetPwd As String '--jet-
    Private msSaveJetPath As String

    '-- Multi-Retail-Host--
    Private mRetailHost1 As _clsRetailHost
    Private msRetailHostname As String = ""
    Private msProviderCode As String '--RM  or PBPOS..--
    '= = = = = = = = = = = = = = = = = = = = = = = = =  =
    Private mBrowseRAs1 As clsBrowse3 '== clsBrowse22
    Private mBrowseSupplier1 As clsBrowse3 '== clsBrowse22

    Private msWhereRAsExist As String = "" '--WHERE for suppliers browse..-

    Private mColPrefsRAs As Collection '--  all statuses..-
    Private mColPrefsSupplier As Collection '--  all statuses..-
    Private mColSelectedSupplierRecord As Collection '--name/address+  all info..-
    Private msSupplierName As String = ""
    Private msSupplierAddressInfo As String = ""
    Private msSupplierMainPhone As String = ""
    Private msSupplierMainFax As String = ""
    Private msSupplierMainEmail As String = ""

    '---  printers..--

    Private miPrtIndex As Short = -1
    Private msDefaultPrinterName As String = ""
    '== Private msColourPrtName As String = ""
    Private msColourPrinterName As String = ""
    Private msReceiptPrtName As String = ""
    Private msLabelPrtName As String = ""

    Private mButtonCurrentBrowseRAs As System.Windows.Forms.ToolStripButton
    Private mButtonCurrentBrowseSupplier As System.Windows.Forms.ToolStripButton

    Private mlRAId As Integer = -1

    Private mDateOldest As Date
    '= = = = = = = = = = = = = = =

    Private msServiceChargeCat1 As String
    Private msServiceChargeCat2 As String

    '= = = = = = = = =  = = = = =

    '--  Business Info-
    '--  Business Info-
    Private msBusinessABN As String
    Private msBusinessName As String
    Private msBusinessAddress1 As String
    Private msBusinessAddress2 As String
    Private msBusinessShortName As String
    Private msBusinessPhone As String
    Private msBusinessPostCode As String
    Private msBusinessState As String
    '-- for printing..
    Private mColBusiness As Collection

    Private mdDateCreated As Date
    Private msLicenceKey As String
    Private mbLicenceOK As Boolean
    Private msGSTPercentage As String
    Private mCurGSTPercentage As Decimal

    Private mImageUserLogo As Image
    '--Barcodes..-
    Private msItemBarcodeFontName As String
    Private mlItemBarcodeFontSize As Integer

    Private mbCancelled As Boolean
    Private mbOK As Boolean

    Private msLog As String = ""
    '== = = = = = = = = =  = = = = == = =

    Private mDataGridViewCellStyleHdr As DataGridViewCellStyle
    Private mDataGridViewCellStyleData As DataGridViewCellStyle

    Private mNodeActiveRoot, mNodeCompletedRoot As System.Windows.Forms.TreeNode
    '== Private mNodeCancelledRoot As System.Windows.Forms.TreeNode

    '- Hold all RA's for selected supplier..
    Private mDtSupplierRAs As DataTable
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  change treeView Backcolour..--

    Private Sub mSetTreeViewColour(ByRef TreeView1 As System.Windows.Forms.TreeView, _
                                     ByRef lngColour As Integer)
        Dim lngStyle As Integer

        Call SendMessage(TreeView1.Handle.ToInt32, TVM_SETBKCOLOR, 0, lngColour) '====  ByVal RGB(255, 0, 0))  'Change the backgroundcolor to red.

        ' Now reset the style so that the tree lines appear properly
        lngStyle = GetWindowLong(TreeView1.Handle.ToInt32, GWL_STYLE)
        Call SetWindowLong(TreeView1.Handle.ToInt32, GWL_STYLE, lngStyle - TVS_HASLINES)
        Call SetWindowLong(TreeView1.Handle.ToInt32, GWL_STYLE, lngStyle)

    End Sub '--set.-
    '= = = = = = = =

    '--get day of week--
    Private Function msDayOfWeek(ByRef date1 As Date) As String
        Dim sDay As String

        Select Case DatePart(Microsoft.VisualBasic.DateInterval.Weekday, date1)
            Case 1 : sDay = "Sunday"
            Case 2 : sDay = "Monday"
            Case 3 : sDay = "Tuesday"
            Case 4 : sDay = "Wednesday"
            Case 5 : sDay = "Thursday"
            Case 6 : sDay = "Friday"
            Case 7 : sDay = "Saturday"
        End Select

        msDayOfWeek = sDay
    End Function '--day--
    '= = = = = = =  =  = =


    '--= 3301.221-=
    '-- Return Local RAs  SettingsPath-

    Private Function msLocalRAsSettingsPath() As String

        '== gsLocalSettingsPath = gsJobMatixLocalDataDir() & "\" & K_POS_SETTINGS_PATH
        msLocalRAsSettingsPath = gsJobMatixLocalDataDir("JobMatix34") & "\" & K_SAVESETTINGSPATH

    End Function '-- gsLocalSettingsPath --
    '= = = = =  = = = = == = = = = = = = = 
    '-===FF->


    '-- Is RetailManager--

    Private Function mbIsRetailManager() As Boolean

        mbIsRetailManager = (LCase(msRetailHostname) = "retailmanager")
    End Function  '- mbIsRetailManager--
    '= = = = = = = = = = = = = = = = == 

    '-- Is RetailManager--

    Private Function mbIsJobmatixPOS() As Boolean

        mbIsJobmatixPOS = (LCase(msRetailHostname) = "jobmatixpos")
    End Function  '- mbIsRetailManager--
    '= = = = = = = = = = = = = = = = == 
    '-===FF->

    '--convert variant array into collection -

    Private Function mbMakeCollection(ByRef array1 As Object, _
                                      ByRef colResult As Collection) As Boolean
        Dim ix, lngLower As Integer
        Dim lngError As Integer

        mbMakeCollection = False
        On Error Resume Next
        lngLower = LBound(array1)
        lngError = Err().Number
        On Error GoTo 0
        If lngError <> 0 Then '--error-
            MsgBox("No array to convert..", MsgBoxStyle.Information)
            Exit Function
        End If '--error..-
        colResult = New Collection
        For ix = lngLower To UBound(array1)
            colResult.Add(array1(ix))
        Next ix

        mbMakeCollection = True

    End Function '-end make.--
    '= = = = = = = = = = = = =
    '= = = = = = = = = = = = = =
    '-===FF->

    '-- test sql server user condition.-
    '----  the SELECT statemen1 provided should return a single value.-

    Private Function mbTestSqlUser(ByRef cnnSQL As OleDbConnection, _
                                    ByVal strSelectQuery As String) As Boolean

        '=3101= Dim rs1 As ADODB.Recordset
        '=3101= Dim vResult As Object

        mbTestSqlUser = gbTestSqlUser(cnnSQL, strSelectQuery)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function '-test--
    '= = = = = = = = = = =
    '-===FF->

    '-- L o a d  ANY JobTracking R e c o r d..-
    '-- L o a d  ANY JobTracking R e c o r d..-

    Private Function mbGetJobTrackingRecord(ByVal sSql As String, _
                                               ByRef ColJobFields As Collection) As Boolean
        Dim RsJob As DataTable '= ADODB.Recordset
        '= Dim fld1 As ADODB.Field
        Dim colFld As Collection
        Dim sName As String

        mbGetJobTrackingRecord = False
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnSql, RsJob, sSql) Then
            MsgBox("Failed to get JOB recordset.." & vbCrLf & "Job Record may be in use..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            '--Me.Hide
            Exit Function
        End If
        '--txtMessages.Text = ""
        ColJobFields = New Collection
        If (Not (RsJob Is Nothing)) AndAlso (RsJob.Rows.Count > 0) Then
            '== If RsJob.BOF And (Not RsJob.EOF) Then
            '== RsJob.MoveFirst()
            '== End If
            Dim dataRow1 As DataRow = RsJob.Rows(0)  '--first row-
            '== If (Not RsJob.EOF) Then '---And (cx < 100)
            '--return complete row..-
            For Each column1 As DataColumn In RsJob.Columns '== fld1 In RsJob.Fields
                sName = column1.ColumnName
                colFld = New Collection
                colFld.Add(LCase(sName), "name")
                colFld.Add(dataRow1.Item(sName), "value")
                ColJobFields.Add(colFld, LCase(sName))
            Next column1 '= fld1
            mbGetJobTrackingRecord = True
            '== Else '--not found-
            '== End If '-eof-
            '== RsJob.Close()
        End If '--rs-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        RsJob = Nothing
    End Function '--get job.-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '--  get value of 1st rst item for SELECT..--

    Private Function mbGetSelectValue(ByVal sSql As String, _
                                     ByRef vResult As Object) As Boolean
        '==3101= Dim rs1 As ADODB.Recordset

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        mbGetSelectValue = gbGetSelectValue(mCnnSql, sSql, vResult)

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        '==3101= rs1 = Nothing
    End Function '--getSElect..-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--Initialise RAs Tree--
    '--Initialise RAs Tree--
    '----  Build all Root nodes..--

    Private Function mbInitialiseRAsTreeView(ByRef tvwRAs As System.Windows.Forms.TreeView) As Boolean
        Dim nodeX, nodeY As System.Windows.Forms.TreeNode
        Dim fontBold As New Font("Lucida Console", 8, FontStyle.Bold)

        '= LabTreeStatusRAs.Text = "Clearing RAs Tree.."
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '==FormWaitOn "Clearing Jobs Tree.."
        System.Windows.Forms.Application.DoEvents()
        tvwRAs.Nodes.Clear()
        '==FormWaitOff

        '= LabTreeStatusRAs.Text = "Building RAs Root Nodes.."
        '==FormWaitOn "Building Tree Root Nodes.."
        System.Windows.Forms.Application.DoEvents()
        '==FormWaitOff
        '==='--  add new roots to treeView for RA basics.--
        '== nodeX = tvwRAs.Nodes.Add("ActiveRoot", "Active Returns (RAs)-") '--1st/next --
        '== nodeX.NodeFont = fontBold
        '== nodeX.Expand()
        '==Set nodeX = tvwJobs.Nodes.Add(, tvwNext, "QuotesRoot", "Active QUOTE Jobs") '--1st/next --
        '== nodeX = tvwRAs.Nodes.Add("CompletedRoot", "Completed Returns-") '--1st/next --
        '== nodeX.NodeFont = fontBold

        '==='--  add new roots to treeView for job types.--
        mNodeActiveRoot = tvwRAs.Nodes.Add("ActiveRoot", "Active Returns (RAs)-", "viewall") '--1st/next --
        mNodeActiveRoot.NodeFont = fontBold
        mNodeActiveRoot.Expand()
        '= mNodeActiveRoot.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)

        mNodeCompletedRoot = tvwRAs.Nodes.Add("CompletedRoot", "Completed Returns-", "viewall") '--1st/next --
        mNodeCompletedRoot.NodeFont = fontBold '== VB6.FontChangeBold(nodeX.NodeFont, True)
        '== mNodeDeliveredRoot.BackColor = System.Drawing.ColorTranslator.FromOle(mLngJobsTreeBGColour)

        '--Roots for Current RAs==
        nodeY = tvwRAs.Nodes.Find("ActiveRoot", True)(0).Nodes.Add("Queued", "Queued")
        nodeY.NodeFont = fontBold  '= VB6.FontChangeBold(nodeY.NodeFont, True)
        nodeY.Expand()
        '==nodeY.Image = "queued"
        nodeY = tvwRAs.Nodes.Find("ActiveRoot", True)(0).Nodes.Add("Requested", "Requested")
        nodeY.NodeFont = fontBold  '=  VB6.FontChangeBold(nodeY.NodeFont, True)
        '==nodeY.Image = "started"
        nodeY = tvwRAs.Nodes.Find("ActiveRoot", True)(0).Nodes.Add("Granted", "Granted")
        nodeY.NodeFont = fontBold  '=  VB6.FontChangeBold(nodeY.NodeFont, True)
        '==nodeY.Image = "notify"
        nodeY = tvwRAs.Nodes.Find("ActiveRoot", True)(0).Nodes.Add("Shipped", "Shipped")
        nodeY.NodeFont = fontBold  '=  VB6.FontChangeBold(nodeY.NodeFont, True)
        '==nodeY.Image = "deliver"
        nodeY = tvwRAs.Nodes.Find("CompletedRoot", True)(0).Nodes.Add("CompletedOK", "Completed OK..")
        nodeY.NodeFont = fontBold  '=  VB6.FontChangeBold(nodeY.NodeFont, True)
        nodeY = tvwRAs.Nodes.Find("CompletedRoot", True)(0).Nodes.Add("Refused", "RA Refused..")
        nodeY.NodeFont = fontBold  '=  VB6.FontChangeBold(nodeY.NodeFont, True)
        nodeY = tvwRAs.Nodes.Find("CompletedRoot", True)(0).Nodes.Add("Cancelled", "RA Cancelled..")
        nodeY.NodeFont = fontBold  '=  VB6.FontChangeBold(nodeY.NodeFont, True)

        '--Root for Delivered SERVICE Jobs==
        '===Set nodeY = tvwRAs.Nodes.Add("DeliveredRoot", tvwChild, "ServiceDelivered", "Service Jobs")
        '--Root for Delivered QUOTE Jobs==
        '===Set nodeY = tvwRAs.Nodes.Add("DeliveredRoot", tvwChild, "QuotesDelivered", "Quote Jobs")
        '--  colour all root nodes as per control BG..-
        For Each nodeX In tvwRAs.Nodes
            nodeX.BackColor = System.Drawing.ColorTranslator.FromOle(mLngRAsTreeBGColour) '--grey-ish.-- &HE4E4E4
        Next nodeX '--node..-
        '= LabTreeStatusRAs.Text = "done.."
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function '--init tree..-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    Private Sub mbSaveExpansionRecursive(ByVal n As TreeNode, _
                                       ByRef colNodesExpanded As Collection)
        '==System.Diagnostics.Debug.WriteLine(n.Text)
        '==MessageBox.Show(n.Text)
        Dim nodeX As TreeNode
        For Each nodeX In n.Nodes
            '== PrintRecursive(aNode)
            If nodeX.IsExpanded Then
                colNodesExpanded.Add(nodeX)
            End If
            mbSaveExpansionRecursive(nodeX, colNodesExpanded)
        Next
    End Sub
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    '--  Refresh TreeView of RAs..-
    '--  Refresh TreeView of RAs..-
    '--  Refresh TreeView of RAs..-

    Private Function mbRefreshRAsTreeView(ByRef tvwRAs As System.Windows.Forms.TreeView, _
                                            Optional ByVal bClearAll As Boolean = True) As Boolean

        Dim rs1 As DataTable '= New ADODB.Recordset
        Dim nodeParent As System.Windows.Forms.TreeNode
        Dim nodeX, nodeY As System.Windows.Forms.TreeNode
        Dim nodeResult() As System.Windows.Forms.TreeNode
        '== Dim avDelivered() As Object
        '== Dim asExpanded() As String '--node key names..-
        Dim colNodesExpanded As Collection  '--of nodes..--
        '== Dim intMonth, intYear As Short
        Dim sSql As String
        Dim sOrder As String
        Dim s1 As String
        Dim sSelectedNodeKey As String
        Dim L1, lngRAs, ix, L2 As Integer
        Dim lngCount As Integer
        Dim lngReq, lngQueued, lngGranted As Integer
        Dim lngRefused, lngShipped, lngCompleted, lngCancelled As Integer
        Dim lngExpanded As Integer
        Dim sKey, sCat1 As String
        Dim sSupplier As String
        Dim sDate As String
        Dim sCaption As String
        Dim sStatus, sRAId As String

        mbRefreshRAsTreeView = False
        On Error GoTo RefreshRAs_Error

        lngExpanded = 0
        sSelectedNodeKey = ""
        '--  save settings..--
        nodeX = tvwRAs.SelectedNode
        If Not (nodeX Is Nothing) Then '--was a selection..-
            sSelectedNodeKey = nodeX.Name
        End If
        '-save expansions..-
        '== Erase asExpanded
        '== If tvwRAs.Nodes.Count > 0 Then
        '== For Each nodeX In tvwRAs.Nodes
        '== If nodeX.IsExpanded Then '--add to saved list.-
        '== lngExpanded = lngExpanded + 1 '--count collected nodes..-
        '== ReDim Preserve asExpanded(lngExpanded)
        '== asExpanded(lngExpanded) = nodeX.Name
        '== End If
        '== Next nodeX '---nodex.-
        '== End If '--count-

        '-- save expansion condition.
        '--  HAS TO BE recursive..--
        colNodesExpanded = New Collection
        If tvwRAs.Nodes.Count > 0 Then
            For Each nodeX In tvwRAs.Nodes
                If nodeX.IsExpanded Then '--add to saved list.-
                    '= L1 = aNodesExpanded.Length
                    '== ReDim Preserve aNodesExpanded(L1 + 1)
                    colNodesExpanded.Add(nodeX)
                End If
                '--  do recursion for its chlidren..-
                mbSaveExpansionRecursive(nodeX, colNodesExpanded)
            Next nodeX '---nodex.-
            '--Initialise Jobs TreeView.-
        End If '--count-

        '--Initialise RAs TreeView.-
        '==  Just clear parent nodes..--  Call mbInitialiseRAsTreeView(tvwRAs)

        If bClearAll Then
            tvwRAs.BeginUpdate()
            For Each nodeParent In mNodeActiveRoot.Nodes
                nodeParent.Nodes.Clear()
            Next nodeParent
            For Each nodeParent In mNodeCompletedRoot.Nodes
                nodeParent.Nodes.Clear()
            Next nodeParent
        End If  '--clear all-
        '-- CURRENT RA's..--

        '--  ADD Date Condition to COMPLETED RAs..--
        sOrder = " RA_Id ASC  "
        If (m_OptRATreeSort_1.Checked = True) Then '--sort by CAT1--
            sOrder = " RM_ItemCat1 ASC, RM_Supplier ASC, RA_Id ASC "
        ElseIf (m_OptRATreeSort_2.Checked = True) Then  '--sort by supplier--
            sOrder = " RM_Supplier ASC, RM_ItemCat1 ASC, RA_Id ASC "
        ElseIf (m_OptRATreeSort_3.Checked = True) Then  '--sort by date--
            sOrder = " RA_DateUpdated ASC, RM_Supplier ASC, RM_ItemCat1 ASC "
        End If

        sSql = " SELECT RA_Id, RA_DateCreated, RA_Status, RM_Supplier, RM_ItemCat1, RM_ItemDescription, RA_DateUpdated "
        sSql = sSql & " FROM dbo.RAItems "
        sSql = sSql & "  WHERE ((LEFT(RA_Status,2)<70) OR (LEFT(RA_Status,2)>=70)) " '--  AND DATE..=====
        sSql = sSql & "    ORDER BY  " & sOrder
        lngRAs = 0
        lngQueued = 0 : lngReq = 0 : lngGranted = 0 : lngShipped = 0 : lngCompleted = 0
        lngRefused = 0 : lngCancelled = 0
        mLabTreeStatusRAs.Text = "Building current RAs.."

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Application.DoEvents()
        '--MsgBox "Getting jobs recordset..  sql is:" + vbCrLf + sSql, vbInformation '--test--
        If Not gbGetDataTable(mCnnSql, rs1, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get RA's recordset.." & vbCrLf & gsGetLastSqlErrorMessage() &
                    "Possible connection failure.." & vbCrLf & vbCrLf & _
                 "Re-Launch Ra's from JobMatix when connection is restored..", MsgBoxStyle.Exclamation)
            '== Me.Close()
            Exit Function
        Else '--build session records.. --
            '==Set colReportLines = New Collection   '--  all session lines..-
            If Not (rs1 Is Nothing) Then
                If Not (rs1.Rows.Count > 0) Then  '=rs1.BOF And rs1.EOF Then
                    If gbDebug Then MsgBox("No current RA's found.", MsgBoxStyle.Information)
                Else '-show RAs..-
                    '= rs1.MoveFirst()
                    For Each dataRow1 As DataRow In rs1.Rows
                        lngRAs = lngRAs + 1
                        sRAId = CStr(dataRow1.Item("RA_Id"))
                        sStatus = dataRow1.Item("RA_Status")
                        sKey = "RA-" & sRAId
                        sCat1 = LSet(dataRow1.Item("RM_ItemCat1"), 7)
                        '==sCat1 = rs1("RM_ItemCat1")
                        sSupplier = Space(32)
                        sSupplier = LSet(VB.Left(dataRow1.Item("RM_Supplier"), 32), Len(sSupplier))
                        '=====sSupplier = Trim(Left(rs1("RM_Supplier"), 28))
                        sDate = VB6.Format(CDate(dataRow1.Item("RA_DateUpdated")), "dd-mmm-yyyy")
                        sCaption = "RA:" & VB6.Format(CInt(dataRow1.Item("ra_id")), " 000") & "- " & sCat1
                        '--  align..-
                        '====While (Picture2.TextWidth(sCaption) < 1600)
                        '====       sCaption = sCaption & " "
                        '====Wend
                        sCaption = sCaption & sSupplier
                        '-- align.--
                        '====While (Picture2.TextWidth(sCaption) < 4440):  sCaption = sCaption & " ": Wend
                        '====sCaption = sCaption & "[" & Left(rs1("RM_ItemDescription"), 16) & "]"
                        '--align--
                        '====While (Picture2.TextWidth(sCaption) < 6564):  sCaption = sCaption & " ": Wend
                        s1 = Space(30)
                        s1 = LSet("[" & VB.Left(dataRow1.Item("RM_ItemDescription"), 28) & "]", Len(s1))
                        sCaption = sCaption & s1 & "  " & sDate
                        If (VB.Left(sStatus, 2) = "10") Then '--created..-
                            nodeY = tvwRAs.Nodes.Find("Queued", True)(0).Nodes.Add(sKey, sCaption)
                            lngQueued = lngQueued + 1
                            lngCount = lngQueued
                        ElseIf (VB.Left(sStatus, 2) = "20") Then
                            nodeY = tvwRAs.Nodes.Find("Requested", True)(0).Nodes.Add(sKey, sCaption)
                            lngReq = lngReq + 1
                            lngCount = lngReq
                        ElseIf (VB.Left(sStatus, 2) = "30") Then  '-- completed..  not delivered..--
                            nodeY = tvwRAs.Nodes.Find("Granted", True)(0).Nodes.Add(sKey, sCaption)
                            lngGranted = lngGranted + 1
                            lngCount = lngGranted
                        ElseIf (VB.Left(sStatus, 2) = "50") Then
                            nodeY = tvwRAs.Nodes.Find("Shipped", True)(0).Nodes.Add(sKey, sCaption)
                            lngShipped = lngShipped + 1
                            lngCount = lngShipped
                        ElseIf (VB.Left(sStatus, 2) = "70") Then  '-- completed..
                            nodeY = tvwRAs.Nodes.Find("CompletedOK", True)(0).Nodes.Add(sKey, sCaption)
                            lngCompleted = lngCompleted + 1
                            lngCount = lngCompleted
                        ElseIf (VB.Left(sStatus, 2) = "95") Then
                            nodeY = tvwRAs.Nodes.Find("Refused", True)(0).Nodes.Add(sKey, sCaption)
                            lngRefused = lngRefused + 1
                            lngCount = lngRefused
                        Else '--been notified.-
                            nodeY = tvwRAs.Nodes.Find("Cancelled", True)(0).Nodes.Add(sKey, sCaption)
                            lngCancelled = lngCancelled + 1
                            lngCount = lngCancelled
                        End If '--service status..-
                        If (lngCount Mod 2) = 0 Then '--even.-
                            nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(mLngRAsTreeBGColour) '---&HC0C0C0  '--grey-ish.--
                        Else
                            nodeY.BackColor = System.Drawing.ColorTranslator.FromOle(&HE0E0E0)
                        End If
                    Next dataRow1
                    '== While (Not rs1.EOF) '---
                    '==  rs1.MoveNext()
                    '== End While '-eof service..-
                End If '--empty-
                '== rs1.Close()
                '==sTest = "Found Sessions: " + vbCrLf
            End If '--nothing.-
        End If '--get
        '== '--  TRY and restore current (saved ) expansion...
        '== If (lngExpanded > 0) Then
        '== For ix = 1 To lngExpanded
        '== nodeX = tvwRAs.Nodes.Item(asExpanded(ix))
        '== nodeX.Expand()
        '== Next ix
        '= End If
        '--  TRY and restore current (saved ) expansion...
        L1 = colNodesExpanded.Count
        If (L1 > 0) Then
            For Each nodeX In colNodesExpanded
                nodeX.Expand()
            Next
        Else
            '--nothing expanded..  expand active root.
            nodeX = tvwRAs.Nodes("ActiveRoot")
            nodeX.Expand()
        End If

        '--  set up selected node again.--
        '== If sSelectedNodeKey <> "" Then
        '== For Each nodeX In tvwRAs.Nodes
        '== If LCase(nodeX.Name) = LCase(sSelectedNodeKey) Then
        '== tvwRAs.SelectedNode = nodeX
        '== Exit For '--done.-
        '== End If
        '== Next nodeX
        '== End If '==selectedNode.-

        If (sSelectedNodeKey <> "") Then
            nodeResult = tvwRAs.Nodes.Find(sSelectedNodeKey, True)  '==.Item(sKey)
            If (nodeResult.Length > 0) Then  '--found..-
                nodeX = nodeResult(0)  '==.Item(sKey)
                If Not (nodeX Is Nothing) Then
                    tvwRAs.SelectedNode = nodeX
                End If
            End If '--length-
        End If
        If bClearAll Then tvwRAs.EndUpdate()

        mLabTreeStatusRAs.Text = ""
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Exit Function

RefreshRAs_Error:
        L1 = Err().Number
        MsgBox("Runtime Error in Refresh RAs Tree function.." & vbCrLf & _
                   "RA-Id is: " & sRAId & vbCrLf & "Error is " & L1 & " = " & ErrorToString(L1))
    End Function '--refresh RAs--
    '= = = = = = = = = = = = =  ==
    '-===FF->

    '-- Show RA Info for current RA Row..--
    '-- Show RA Info for current RA Row..--

    Private Function mbShowRAInfo(ByVal lngRAId As Integer) As Boolean

        Dim s1 As String
        Dim sSql As String
        Dim sInvoiceDate, sComments As String
        '== Dim sStaff, sDescr As String
        Dim sStatus, sStatus2 As String
        '== Dim sPriority As String
        Dim sSupplCode As String
        Dim sCustomerCompany As String
        Dim sCustomerName As String
        '== Dim rs1 As ADODB.Recordset
        Dim colRAFields As Collection

        '======labStatus.Visible = False
        '====FrameDetails.Visible = False
        mFrameRADetails.Visible = True
        sSql = "SELECT * from [RAItems]  "
        sSql = sSql & " WHERE (RA_id=" & CStr(lngRAId) & ")  " & vbCrLf
        '--If mbGetJobRecord(lngJobId, mColJobFields(tabIndex)) Then
        If mbGetJobTrackingRecord(sSql, colRAFields) Then
            mlRAId = lngRAId
            mFrameRADetails.Text = "" '-- "Home RA No: " & lngRAId
            mLabRA_id.Text = " RA# " & lngRAId
            sStatus = colRAFields.Item("RA_Status")("value")
            '=====txtRAStatusOrig.Text = sStatus
            sStatus2 = ""
            Select Case VB.Left(sStatus, 2)
                '==Case "10": sStatus2 = "CREATED"
                Case "10" : sStatus2 = "QUEUED"
                Case "20" : sStatus2 = "REQUESTED"
                Case "30" : sStatus2 = "GRANTED"
                Case "50" : sStatus2 = "SHIPPED"
                Case "70" : sStatus2 = "RECEIVED"
                Case "95" : sStatus2 = "RMA REFUSED"
                Case "97" : sStatus2 = "CANCELLED"
            End Select
            '==txtRAStatusFriendly.Text = "[" & sStatus2 & "]"
            mLabRAStatusFriendly.Text = "[" & sStatus2 & "]"
            mTxtRACat1.Text = Trim(colRAFields.Item("RM_ItemCat1")("value"))
            sSupplCode = colRAFields.Item("RM_ItemSupplierCode")("value")
            '===txtRAItem = " (Suppl.Code:" & IIf((sSupplCode <> ""), sSupplCode, "--") & " )" & vbCrLf & _
            ''===                 Trim(colRAFields("RM_ItemDescription")("value")) & vbCrLf
            mTxtRAItem.Text = Trim(colRAFields.Item("RM_ItemDescription")("value"))
            mTxtRAProblem.Text = colRAFields.Item("RA_Symptoms")("value")
            '==                               "NOTES:  " & colRAFields("RA_RMA_RequestNotes")("value")
            mTxtRACreated.Text = VB6.Format(CDate(colRAFields.Item("RA_DateCreated")("value")), "dd-mmm-yyyy") & _
                                                                   " (" & colRAFields.Item("RM_StaffNameCreated")("value") & ")"
            mTxtRAUpdated.Text = ""
            If Not IsDBNull(colRAFields.Item("RA_DateUpdated")("value")) Then
                mTxtRAUpdated.Text = VB6.Format(CDate(colRAFields.Item("RA_DateUpdated")("value")), "dd-mmm-yyyy") & _
                                                                      " (" & colRAFields.Item("RM_StaffNameUpdated")("value") & ")"
            End If '--updated.-
            mTxtRASerialNo.Text = colRAFields.Item("RA_SerialNumber")("value")
            mTxtRAProdBarcode.Text = colRAFields.Item("RM_ItemBarcode")("value")

            sInvoiceDate = "--"
            If Not IsDBNull(colRAFields.Item("RM_InvoiceDate")("value")) Then
                sInvoiceDate = VB6.Format(CDate(colRAFields.Item("RM_InvoiceDate")("value")), "dd-mmm-yyyy")
            End If '--updated.-

            mlabRASupplier.Text = "Supplier Info: [Tel: " & colRAFields.Item("RM_Supplier_Main_Phone")("value") & "]"
            mTxtRASupplier.Text = colRAFields.Item("RM_Supplier")("value") & vbCrLf & _
                                 "Invoice No: " & colRAFields.Item("RM_InvoiceNo")("value") & vbCrLf & _
                                 "Invoice Date: " & sInvoiceDate
            '===txtInvoiceInfo.Text = "Invoice No: " + colRAFields("RM_InvoiceNo")("value") + vbCrLf + _
            ''===            "  Date: " + Format(CDate(colRAFields("RM_InvoiceDate")("value"))) + vbCrLf + _
            ''===                 "Order No: " + colRAFields("RM_orderNo")("value")
            mTxtFreightTrackNo.Text = colRAFields.Item("RA_CourierBarcode")("value")
            sCustomerCompany = colRAFields.Item("RA_CustomerCompany")("value")
            sCustomerName = colRAFields.Item("RA_CustomerName")("value")
            If (sCustomerName = ",") Then sCustomerName = ""
            If (sCustomerName <> "") Then
                If (sCustomerCompany <> "") And (sCustomerCompany <> "--") Then
                    s1 = sCustomerName & vbCrLf & " [" & sCustomerCompany & "]"
                Else
                    s1 = sCustomerName
                End If
            Else
                s1 = sCustomerCompany
            End If '--cust name.-
            mTxtRACustomerName.Text = s1
            mTxtRACustomerContact.Text = colRAFields.Item("RA_CustomerPhone")("value") & vbCrLf & _
                                                                       colRAFields.Item("RA_CustomerMobile")("value")
            mTxtRASupplierRANo.Text = colRAFields.Item("RA_SupplierRMA_No")("value")
            '== Doesn't like this DoEvents.  Reentrancy PROBLEM- 
            '== System.Windows.Forms.Application.DoEvents()
        End If '--get..-
    End Function '-RA info..-
    '= = = = = = = = = = =  =  = =  =
    '-===FF->

    '-- BROWSE RAs using Browse Class and our local FlexGrid..-
    '-- BROWSE RAs using Browse Class and our local FlexGrid..-

    '-- BROWSE RAs.. Show ONLY (No wait..) using Browse Class..
    '---  show recordset, and enable Grid..--

    Private Function mbShowRAsBrowse(ByRef colPrefs As Collection, _
                                       ByRef sTitle As String, ByRef sWhere As String) As Boolean
        '== Dim colRowValues As Collection

        mbShowRAsBrowse = False
        mlStaffTimeout = -1 '--disable timer..-
        '=====  Load frmBrowse  '--from original maint..--
        '-- MUST start complete new browse instance..-
        '===If Not (mBrowseRAs Is Nothing) Then Set mBrowseRAs = Nothing
        '===Set mBrowseRAs = New clsBrowse

        '-- - --- MUST load/unload each time--
        mBrowseRAs1.connection = mCnnSql '--job tracking sql connenction..-
        mBrowseRAs1.colTables = mColSqlDBInfo
        mBrowseRAs1.DBname = msSqlDbName
        mBrowseRAs1.tableName = "RAItems"
        mBrowseRAs1.IsSqlServer = True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        mBrowseRAs1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        mBrowseRAs1.PreferredColumns = colPrefs
        mBrowseRAs1.ShowPreferredColumnsOnly = True
        '== mBrowse1.Title = sTitle

        '--  pass controls..--
        '== mBrowseRAs.FlexGrid = MSHFlexGridRAs
        mBrowseRAs1.DataGrid = mDataGridViewRAs

        mBrowseRAs1.showRecCount = mLabRecCountRAs '--updates rec. retrieval..
        '===mBrowseRAs.showWhere = LabWhereRAs        '--updates Sort Order..
        mBrowseRAs1.showFind = mLabFindRAs '--updates Sort Column display..
        mBrowseRAs1.showTextFind = mTxtFindRAs '--updates Sort Column display..
        mFrameBrowseRAs.Enabled = True
        '=  mLabRATitle.Text = sTitle
        mLngSelectedRowRAs = -1

        On Error GoTo BrowseRAs_Activate_Error
        mBrowseRAs1.Activate() '-- go..--
        On Error GoTo 0
        '=====  txtFind.SetFocus
        mTxtFindRAs.Focus()
        mlStaffTimeout = 0 '--reset timer..-
        mbShowRAsBrowse = True
        Exit Function

BrowseRAs_Activate_Error:
        MsgBox("Possible Sql Connection Failure.." & vbCrLf & _
                  "JobMatix will attempt to renew Connection.." & vbCrLf, _
                       MsgBoxStyle.Exclamation, "mbShowRAsBrowse")
        '= Me.Close()
    End Function '-- Show RA Browse..--
    '= = = = = = = = = = = = = = 
    '-===FF->

    Private Function msAddSupplierItem(ByVal sAddressInfo As String, _
                                      ByRef colFields As Collection, _
                                         ByVal sColName As String) As String
        Dim s1 As String
        If Not IsDBNull(colFields.Item(sColName)("value")) Then
            s1 = colFields.Item(sColName)("value")
            If (s1 <> "") Then
                msAddSupplierItem = sAddressInfo & s1 & vbCrLf  '==3203.213= max db colsize..(Plus quotes ?)
            Else
                msAddSupplierItem = sAddressInfo  '--no change-
            End If
        End If
    End Function '--add-
    '= = = = = = = = = = =

    '-- Set up Supplier Info text from ColRecord..--
    '--   Fudged from form frmNewRA for Printing GROUP Shipping Label.. ..--

    Private Function mbSetUpSupplier(ByRef colSupplierRecord As Collection) As Integer
        Dim sSurburb As String = ""  '=v1 As Object

        msSupplierAddressInfo = ""
        msSupplierName = colSupplierRecord("supplier")("value")
        '-- build supplier composite address info..-
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colSupplierRecord, "Main_Addr1")
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colSupplierRecord, "Main_Addr2")
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colSupplierRecord, "Main_Addr3")

        If Not IsDBNull(colSupplierRecord("Main_suburb")("value")) Then
            sSurburb = colSupplierRecord("Main_suburb")("value")
        End If
        If Not IsDBNull(colSupplierRecord("Main_state")("value")) Then
            sSurburb &= " " & colSupplierRecord("Main_state")("value")
        End If
        If Not IsDBNull(colSupplierRecord("Main_postcode")("value")) Then
            sSurburb &= " " & colSupplierRecord("Main_postcode")("value")
        End If
        If (Trim(sSurburb) <> "") Then
            msSupplierAddressInfo &= Trim(sSurburb) & vbCrLf
        End If
        '= msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colSupplierRecord, "Main_suburb")
        '= msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colSupplierRecord, "Main_state")
        '= msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colSupplierRecord, "Main_postcode")
        msSupplierAddressInfo = msAddSupplierItem(msSupplierAddressInfo, colSupplierRecord, "Main_country")

        msSupplierMainPhone = colSupplierRecord.Item("Main_Phone")("value")
        msSupplierMainFax = colSupplierRecord.Item("Main_fax")("value")
        msSupplierMainEmail = colSupplierRecord.Item("Main_email")("value")

        mTxtRA_supplierName.Text = msSupplierName & vbCrLf & msSupplierAddressInfo

    End Function '--SetUpSupplier-
    '= = = = = = =  =
    '-===FF->

    '-- BROWSE Suppliers.. Show ONLY (No wait..) using Browse Class..
    '---  show recordset, and enable Grid..-
    '-- MUST go via RetailHost..-

    Private Function mbShowSupplierBrowse(ByRef colPrefs As Collection, _
                                            ByRef sTitle As String, _
                                               ByRef sWhere As String) As Boolean
        '== Dim colRowValues As Collection

        mbShowSupplierBrowse = False
        mlStaffTimeout = -1 '--disable timer..-
        '=====  Load frmBrowse  '--from original maint..--
        '-- MUST start complete new browse instance..-
        '===If Not (mBrowseRAs Is Nothing) Then Set mBrowseRAs = Nothing
        '===Set mBrowseRAs = New clsBrowse

        '-- - --- MUST load/unload each time--
        mBrowseSupplier1.connection = mRetailHost1.connection  '== mCnnSql '--job tracking sql connection..-
        mBrowseSupplier1.colTables = mRetailHost1.colTables   '=mColSqlDBInfo
        mBrowseSupplier1.DBname = mRetailHost1.DBname  '=msSqlDbName
        mBrowseSupplier1.tableName = "supplier"
        mBrowseSupplier1.IsSqlServer = mRetailHost1.IsSqlServer  '=True '--bIsSqlServer

        '--- set WHERE condition for jobStatus..--
        mBrowseSupplier1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        mBrowseSupplier1.PreferredColumns = colPrefs
        mBrowseSupplier1.ShowPreferredColumnsOnly = True
        '== mBrowse1.Title = sTitle

        '--  pass controls..--
        '== mBrowseRAs.FlexGrid = MSHFlexGridRAs
        mBrowseSupplier1.DataGrid = mDgvRA_suppliers

        mBrowseSupplier1.showRecCount = mLabRecCountSupplier
        '--updates rec. retrieval..
        '===mBrowseRAs.showWhere = LabWhereRAs        '--updates Sort Order..
        mBrowseSupplier1.showFind = mLabFindSupplier  '--updates Sort Column display..
        mBrowseSupplier1.showTextFind = mTxtFindSupplier  '--updates Sort Column display..
        mFrameRA_suppliers.Enabled = True
        '=  mLabRATitle.Text = sTitle
        mLngSelectedRowSupplier = -1

        On Error GoTo BrowseSupplier_Activate_Error
        mBrowseSupplier1.Activate() '-- go..--
        On Error GoTo 0
        '=====  txtFind.SetFocus
        mTxtFindSupplier.Focus()
        mlStaffTimeout = 0 '--reset timer..-
        mbShowSupplierBrowse = True
        Exit Function

BrowseSupplier_Activate_Error:
        MsgBox("Possible Sql Connection Failure.." & vbCrLf & _
                  "JobMatix will attempt to renew Connection.." & vbCrLf, _
                       MsgBoxStyle.Exclamation, "mbShowSupplierBrowse")
        '= Me.Close()
    End Function '-- Show Supplier Browse..--
    '= = = = = = = = = = = = = = 
    '-===FF->

    '-- refresh Supplier Grid (Only want suppliers with RA's)
    '==  -- 3327.0119- 19-Jan-2017-
    '==         >>-- Fixes ERROR in clsRAsMain33- SQL error when NO RAs on file. (empty list)..-- 

    Private Function mbRefreshSuppliersGrid() As Boolean
        Dim sSql, sList As String
        Dim datatable1 As DataTable

        '-- First get DISTINCT suppliers who have RA;s.=
        msWhereRAsExist = " supplier_id IN ("
        sList = ""
        sSql = " SELECT DISTINCT RM_supplierId "
        sSql = sSql & " FROM dbo.RAItems "
        '== sSql = sSql & "  WHERE (RM_supplierId=" & CStr(intSupplier_id) & ") "
        If mChkShowGrantedRAsOnly.Checked Then
            sSql = sSql & " WHERE (LEFT(RA_Status,2)='30') "  '--granted-
        End If
        sSql = sSql & "  ORDER BY RM_supplierId; "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Application.DoEvents()
        '--MsgBox "Getting jobs recordset..  sql is:" + vbCrLf + sSql, vbInformation '--test--
        If Not gbGetDataTable(mCnnSql, datatable1, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get DISTINCT Suppliers RA's recordset.." & vbCrLf & _
                    gsGetLastSqlErrorMessage() & vbCrLf & vbCrLf, MsgBoxStyle.Exclamation)
            '== Me.Close()
            Exit Function
        Else '--ok. build Supplier ID list.
            '-- used to select suppliers for Browser Grid.
            If (Not (datatable1 Is Nothing)) Then
                For Each datarow1 As DataRow In datatable1.Rows
                    If (sList <> "") Then
                        sList &= ", "
                    End If
                    sList &= CStr(datarow1.Item("RM_supplierId"))
                Next
            End If '-nothing-
            If (sList <> "") Then
                msWhereRAsExist &= sList & ")"
            Else  '- no RAs-  Just get empty recordset to show cols.
                msWhereRAsExist = " supplier_id = -1"
            End If
            '--call browser start-
            Call mbShowSupplierBrowse(mColPrefsSupplier, "Browse Suppliers", msWhereRAsExist)
        End If  '--get-

    End Function  '--refresh suppliers.-
    '= = = = = = = = = = == = = = = == =
    '-===FF->

    '--  Get all RA's for selected Supplier--
    '= -- 3357.0207=  Add Columns needed for POS GoodsReturned fld. collection..

    Private Function mbShowSupplierRAs(ByVal intSupplier_id As Integer) As Boolean
        Dim sSql, s1 As String
        '= Dim rs1 As DataTable '= New ADODB.Recordset
        '==Dim L1, lngRAs, ix, L2 As Integer
        Dim lngCount As Integer
        Dim lngReq, lngQueued, lngGranted As Integer
        Dim sKey, sCat1 As String
        Dim sDate, sStatus, sRAId As String

        mbShowSupplierRAs = False
        mListViewSupplierRAs.Items.Clear()

        sSql = " SELECT RA_Id, RA_SerialNumber, RM_SerialAudit_id, RA_SupplierRMA_No, "
        sSql &= "   RM_InvoiceNo, RM_StockId, RM_ItemBarcode, RM_ItemSupplierCode, "
        sSql &= "  RA_symptoms, RA_RMA_RequestNotes,"
        sSql &= "   RA_DateCreated, RA_Status, RM_ItemCat1, RM_ItemDescription, RA_DateUpdated "
        sSql &= " FROM dbo.RAItems "
        sSql &= "  WHERE (RM_supplierId=" & CStr(intSupplier_id) & ") "
        If mChkShowGrantedRAsOnly.Checked Then
            sSql = sSql & " AND (LEFT(RA_Status,2)='30') "  '--granted-
        End If
        sSql = sSql & "  ORDER BY RA_Id; "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        System.Windows.Forms.Application.DoEvents()
        '--MsgBox "Getting jobs recordset..  sql is:" + vbCrLf + sSql, vbInformation '--test--

        '-- get r-set as  static datatable for later use-
        If Not gbGetDataTable(mCnnSql, mDtSupplierRAs, sSql) Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to get RA's recordset.." & vbCrLf & _
                    gsGetLastSqlErrorMessage() & vbCrLf & vbCrLf, MsgBoxStyle.Exclamation)
            '== Me.Close()
            Exit Function
        Else '--build listView rows.. records.
            If (mDtSupplierRAs IsNot Nothing) AndAlso (mDtSupplierRAs.Rows.Count > 0) Then
                Dim item1 As ListViewItem
                Dim sub1 As ListViewItem.ListViewSubItem
                Dim dataRow1 As DataRow
                Dim rowindex1 As Integer
                '= mListViewSupplierRAs.Items.Clear()
                For rowindex1 = 0 To (mDtSupplierRAs.Rows.Count - 1)
                    '== Each dataRow1 As DataRow In mDtSupplierRAs.Rows
                    dataRow1 = mDtSupplierRAs.Rows(rowindex1)
                    s1 = Trim(dataRow1.Item("ra_id")) '--1st column.-
                    item1 = mListViewSupplierRAs.Items.Add(s1)   '--1st column.-
                    '-These don't work
                    '= item1.SubItems("listViewRA_SerialNumber").Text = Trim(dataRow1.Item("RA_SerialNumber"))
                    '- only this works-
                    sub1 = item1.SubItems.Add(Trim(dataRow1.Item("RA_Status")))
                    sub1.Name = "RA_Status"
                    sub1 = item1.SubItems.Add(Trim(dataRow1.Item("RA_SupplierRMA_No")))
                    sub1.Name = "RA_SupplierRMA_No"
                    sub1 = item1.SubItems.Add(Trim(dataRow1.Item("RA_SerialNumber")))
                    sub1.Name = "RA_SerialNumber"
                    sub1 = item1.SubItems.Add(Trim(dataRow1.Item("RM_ItemCat1")))
                    sub1.Name = "RM_ItemCat1"
                    sub1 = item1.SubItems.Add(Trim(dataRow1.Item("RM_ItemDescription")))
                    sub1.Name = "RM_ItemDescription"
                    sub1 = item1.SubItems.Add(Trim(dataRow1.Item("RM_ItemSupplierCode")))
                    sub1.Name = "RM_ItemSupplierCode"
                    '-- save row index for later GoodsSent..
                    sub1 = item1.SubItems.Add(rowindex1)
                    sub1.Name = "dtrowx"
                    '=item1.SubItems.Add(Format(dataRow1.Item("doc_date_inserted"), "dd-MMM-yyyy")) '--2nd column.-
                    System.Windows.Forms.Application.DoEvents()
                Next rowindex1 '= dataRow1
                '=3403.608=  Select first item
                mListViewSupplierRAs.Items(0).Focused = True
                mListViewSupplierRAs.Items(0).Selected = True
                mbShowSupplierRAs = True
            End If  '-nothing-
        End If  '-get-
    End Function  '-mbGetSupplierRAs-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '-- new --

    Public Sub New(ByRef frmParent As Form, _
                   ByRef imageUserLogo As Image, _
                   ByRef labStatus As Label, _
                    ByVal sServer As String, _
                    ByVal sJobMatixVersion As String, _
                     ByVal cnnSql As OleDbConnection, _
                      ByVal sSqlDbName As String, _
                      ByRef colSqlDBInfo As Collection, _
                       ByVal sStaffBarcode As String, _
                        ByVal bLicenceOK As Boolean, _
                         ByRef retailHost As _clsRetailHost)
        Dim s1, s2 As String
        Dim L1 As Integer

        mbIsInitialising = True
        '--save  input..-
        mFrmParent = frmParent
        mImageUserLogo = imageUserLogo
        mLabStatus = labStatus
        msServer = sServer
        msJobMatixVersion = sJobMatixVersion
        mCnnSql = cnnSql
        msSqlDbName = sSqlDbName

        mColSqlDBInfo = colSqlDBInfo
        msStaffBarcode = sStaffBarcode
        mbLicenceOK = bLicenceOK
        mRetailHost1 = retailHost  '-save host-

        '-- Save addresses of Main Controls..-
        '-- Find all needed Controls on Main Form (RAs Tab)...
        '--   and save as Module vars..
        Dim controlsArray() As Control
        mLabStatus.Text = "Saving RA Main Controls info.." & vbCrLf

        '== Private mFrameRAsTree As GroupBox
        '- get group Boxes first.
        controlsArray = mFrmParent.Controls.Find("frameRAsTab", True)  '--searches all children.. 
        If (controlsArray.Length > 0) Then
            mFrameRAsTab = controlsArray(0)
        End If
        '-- RAs red lable -
        '-mLabShowSearchRAs-
        controlsArray = mFrmParent.Controls.Find("LabShowSearchRAs", True)  '--searches all children.. 
        If (controlsArray.Length > 0) Then
            mLabShowSearchRAs = controlsArray(0)
        End If
        '- mToolStripRAs -
        '= NOW TabControlRAs-
        controlsArray = mFrmParent.Controls.Find("TabControlRAs", True)  '--searches all children.. 
        If (controlsArray.Length > 0) Then
            mTabControlRAs = controlsArray(0)
        End If

        '-- Active RAs Tree.-
        controlsArray = mFrmParent.Controls.Find("frameRAsTree", True)  '--searches all children.. 
        If (controlsArray.Length > 0) Then
            mFrameRAsTree = controlsArray(0)
            '-- get frame contents.-
            For Each Control1 As Control In mFrameRAsTree.Controls
                Select Case LCase(Control1.Name)
                    '= Case "grpboxaddnew" : mGrpBoxAddNew = Control1
                    Case "tvwras" : mTvwRAs = Control1
                    Case "chkautorefreshras" : mChkAutoRefreshRAs = Control1
                    Case "cmdrefreshrastree" : mCmdRefreshRAsTree = Control1
                    Case "_optratreesort_0" : m_OptRATreeSort_0 = Control1
                    Case "_optratreesort_1" : m_OptRATreeSort_1 = Control1
                    Case "_optratreesort_2" : m_OptRATreeSort_2 = Control1
                    Case "_optratreesort_3" : m_OptRATreeSort_3 = Control1
                    Case "labtreestatusras" : mLabTreeStatusRAs = Control1
                    Case Else
                        '-- Don't need this control.-
                End Select
            Next  '--control1-
        End If
        '== mFrameBrowseRAs As GroupBox-
        controlsArray = mFrmParent.Controls.Find("frameBrowseRAs", True)  '--searches all children.. 
        If (controlsArray.Length > 0) Then
            mFrameBrowseRAs = controlsArray(0)
            '-- get frame contents.-
            For Each Control1 As Control In mFrameBrowseRAs.Controls
                Select Case LCase(Control1.Name)
                    Case "toolbarrasgrid" : mToolbarRAsGrid = Control1
                    Case "datagridviewras" : mDataGridViewRAs = Control1
                    Case "labfindras" : mLabFindRAs = Control1
                    Case "txtfindras" : mTxtFindRAs = Control1
                    Case "txtrasearch" : mTxtRASearch = Control1
                    Case "cmdrasearch " : mCmdRASearch = Control1
                    Case "cmdclearrasearch" : mCmdClearRASearch = Control1
                        '- mLabRecCountRAs
                    Case "labreccountras" : mLabRecCountRAs = Control1
                    Case Else
                        '-- Don't need this control.-
                End Select
            Next Control1
        End If  '-length-

        '-- Suppliers Frame-
        '=Private mFrameRA_suppliers As GroupBox
        '=Private mDgvRA_suppliers As DataGridView
        '= Private mListViewSupplierRAs As ListView
        '-- chkShowGrantedRAsOnly -
        controlsArray = mFrmParent.Controls.Find("frameRA_suppliers", True)  '--searches all children.. 
        If (controlsArray.Length > 0) Then
            mFrameRA_suppliers = controlsArray(0)
            '-- get frame contents.-
            For Each Control1 As Control In mFrameRA_suppliers.Controls
                Select Case LCase(Control1.Name)
                    Case "dgvra_suppliers" : mDgvRA_suppliers = Control1
                    Case "listviewsupplierras" : mListViewSupplierRAs = Control1
                        '==Case "chkshowgrantedrasonly" : mChkShowGrantedRAsOnly = Control1
                        '--  more controls for suppliers..
                    Case "labfindsupplier" : mLabFindSupplier = Control1
                    Case "txtfindsupplier" : mTxtFindSupplier = Control1
                    Case "labreccountsupplier" : mLabRecCountSupplier = Control1
                    Case "chkshowgrantedrasonly" : mChkShowGrantedRAsOnly = Control1
                    Case Else
                        '-- Don't need this control.-
                End Select
            Next Control1
        End If  '-length-
        '-- supplier info panel-
        '=Private mPanelRA_supplierInfo As Panel
        '=Private mChkShowGrantedRAsOnly As CheckBox
        '=Private mChkSelectAllRAsGranted As CheckBox
        '=Private mLabRA_supplierName As Label
        controlsArray = mFrmParent.Controls.Find("panelRA_supplierInfo", True)  '--searches all children.. 
        If (controlsArray.Length > 0) Then
            mPanelRA_supplierInfo = controlsArray(0)
            '-- get panel contents.-
            For Each Control1 As Control In mPanelRA_supplierInfo.Controls
                Select Case LCase(Control1.Name)
                    '= Case "chkshowgrantedrasonly" : mChkShowGrantedRAsOnly = Control1
                    Case "chkselectallrasgranted" : mChkSelectAllRAsGranted = Control1
                    Case "txtra_suppliername" : mTxtRA_supplierName = Control1
                        '- mBtnRAsUpdateGroupSent-
                    Case "btnrasupdategroupsent" : mBtnRAsUpdateGroupSent = Control1
                        '-mCboRAs_A4Printers-
                    Case "cboras_a4printers" : mCboRAs_A4Printers = Control1
                    Case Else
                        '-- Don't need this control.-
                End Select
            Next Control1
        End If '-length-

        '== Private mFrameRADetails As GroupBox
        controlsArray = mFrmParent.Controls.Find("frameRADetails", True)  '--searches all children.. 
        If (controlsArray.Length > 0) Then
            mFrameRADetails = controlsArray(0)
            '-- get frame contents.-
            For Each Control1 As Control In mFrameRADetails.Controls
                Select Case LCase(Control1.Name)
                    Case "txtracat1" : mTxtRACat1 = Control1
                    Case "labra_id" : mLabRA_id = Control1
                    Case "labrastatusfriendly" : mLabRAStatusFriendly = Control1
                    Case "txtraitem" : mTxtRAItem = Control1
                    Case "txtracreated" : mTxtRACreated = Control1
                    Case "txtraupdated" : mTxtRAUpdated = Control1
                    Case "txtraserialno" : mTxtRASerialNo = Control1
                    Case "txtraprodbarcode" : mTxtRAProdBarcode = Control1
                    Case "txtraproblem" : mTxtRAProblem = Control1
                        '- mLabRASupplier
                    Case "labrasupplier" : mLabRASupplier = Control1
                    Case "txtrasupplier" : mTxtRASupplier = Control1
                    Case "txtrasupplierrano" : mTxtRASupplierRANo = Control1
                    Case "txtfreighttrackno" : mTxtFreightTrackNo = Control1
                    Case "txtracustomername" : mTxtRACustomerName = Control1
                    Case "txtracustomercontact" : mTxtRACustomerContact = Control1
                    Case "btnra_attachments" : mBtnRA_attachments = Control1
                    Case "cmdviewrecordras" : mCmdViewRecordRAs = Control1
                    Case Else
                        '-- Don't need this control.-
                End Select
            Next Control1
        End If  '-length-

        '-- From frmRAsMain Load  -
        '-- FORCE datagridView cell style to stick..-
        mDataGridViewCellStyleHdr = New DataGridViewCellStyle
        mDataGridViewCellStyleData = New DataGridViewCellStyle

        mDataGridViewCellStyleHdr.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        mDataGridViewCellStyleHdr.BackColor = System.Drawing.Color.Gainsboro
        mDataGridViewCellStyleHdr.Font = _
            New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, _
                                          System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        mDataGridViewCellStyleHdr.ForeColor = System.Drawing.SystemColors.WindowText
        mDataGridViewCellStyleHdr.SelectionBackColor = System.Drawing.SystemColors.Highlight
        mDataGridViewCellStyleHdr.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        mDataGridViewCellStyleHdr.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        mDataGridViewRAs.ColumnHeadersDefaultCellStyle = mDataGridViewCellStyleHdr
        mDataGridViewRAs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        mDataGridViewCellStyleData.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        mDataGridViewCellStyleData.BackColor = System.Drawing.SystemColors.Window
        mDataGridViewCellStyleData.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        mDataGridViewCellStyleData.ForeColor = System.Drawing.SystemColors.ControlText
        mDataGridViewCellStyleData.SelectionBackColor = System.Drawing.SystemColors.Highlight
        mDataGridViewCellStyleData.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        mDataGridViewCellStyleData.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        mDataGridViewRAs.DefaultCellStyle = mDataGridViewCellStyleData

        '---   RAs ---
        Call mbMakeCollection(New Object() {"RA_id", "RA_Status", "RA_SerialNumber AS SerialNumber", _
                                              "RA_SupplierRMA_No AS SupplierRMA_No", _
                                               "RM_Supplier AS Supplier", "RM_ItemCat1 AS Cat1", _
                                                  "RM_ItemDescription AS Description", _
                                                    "RM_ItemBarcode AS Barcode", _
                                                      "RA_DateUpdated AS DateUpdated"}, mColPrefsRAs)
        '-- start with RA Tree.
        '=mframeRAsTab.Visible = True
        '== mFrameBrowseRAs.Visible = False
        '== mFrameRA_suppliers.Visible = False

        mFrameRAsTree.Visible = True
        mFrameRADetails.Visible = True
        mBrowseRAs1 = New clsBrowse3
        mBrowseSupplier1 = New clsBrowse3
        Dim sHostTableName As String
        Call mRetailHost1.browseGetPrefColumns("supplier", sHostTableName, mColPrefsSupplier)

        mChkShowGrantedRAsOnly.Checked = True
        mTxtRA_supplierName.Text = ""

        '--WHERE clause for suppliers browse..-  
        '--  Pick only suppliers with RAs..
        '- can't work across databases-
        '== msWhereRAsExist = "EXISTS (SELECT RA_id FROM dbo.RAitems " & _
        '==                       " WHERE (RAitems.RM_SupplierId = " & sHostTableName & ".supplier_id) )"
        msSettingsPath = msLocalRAsSettingsPath() '= gsLocalJobsSettingsPath("JobMatix33") '= msAppPath & K_SAVESETTINGSPATH
        '=3311.305= Load up Settings.
        mLocalSettings1 = New clsLocalSettings(msSettingsPath)

        '==3311= SysInfo. use class instance.
        mSysInfo1 = New clsSystemInfo(mCnnSql)
        '-- Business Info..
        msBusinessABN = mSysInfo1.item("BUSINESSABN")
        msBusinessABN = Replace(msBusinessABN, " ", "") '--strip blanks..-

        '== msBusinessUser = mSdSystemInfo.Item("BUSINESSUSERNAME")
        msBusinessName = mSysInfo1.item("BUSINESSNAME")
        msBusinessAddress1 = mSysInfo1.item("BUSINESSADDRESS1")
        msBusinessAddress2 = mSysInfo1.item("BUSINESSADDRESS2")
        msBusinessShortName = mSysInfo1.item("BUSINESSSHORTNAME")
        msBusinessState = mSysInfo1.item("BUSINESSSTATE")
        msBusinessPostCode = mSysInfo1.item("BUSINESSPOSTCODE")
        msBusinessPhone = mSysInfo1.item("BUSINESSPHONE")

        msItemBarcodeFontName = mSysInfo1.item("ITEMBARCODEFONTNAME")
        s1 = mSysInfo1.item("ITEMBARCODEFONTSIZE")
        If IsNumeric(s1) Then
            L1 = CInt(s1)
            If (L1 > 3) And (L1 < 36) Then
                mlItemBarcodeFontSize = L1
            End If
        End If
        '-Make biz collection for Printing.
        '--load business info..-
        mColBusiness = New Collection
        mColBusiness.Add(msBusinessName, "BusinessName")
        mColBusiness.Add("", "BusinessShortName")
        mColBusiness.Add(msBusinessAddress1, "BusinessAddress1")
        mColBusiness.Add(msBusinessAddress2, "BusinessAddress2")
        mColBusiness.Add("", "BusinessState")
        mColBusiness.Add("", "BusinessPostcode")

        '= msColourPrinterName = mCboRAs_A4Printers.SelectedItem
        '= 3357.0206=
        If mSysInfo1.exists("RETAILHOSTNAME") Then
            msRetailHostname = mSysInfo1.item("RETAILHOSTNAME")
        End If

        '- MUST override Main prt setting (was from jobs setting)-
        '-mLocalSettings1.SaveSetting(k_RA_PrtSettingKey, msColourPrinterName)
        msColourPrinterName = mLocalSettings1.item(k_RA_PrtSettingKey)  '-"RA_PRTCOLOUR"-

        '=3311.319- select current printer if any.
        If (mCboRAs_A4Printers.Items.Count > 0) Then
            If (msColourPrinterName <> "") Then
                '-- find is in combo-
                For Each s1 In mCboRAs_A4Printers.Items
                    If (LCase(msColourPrinterName) = LCase(s1)) Then
                        mCboRAs_A4Printers.SelectedItem = s1
                    End If
                Next s1
            Else  '--no previous-
                mCboRAs_A4Printers.SelectedIndex = 0   '-case-
            End If
         End If

        mLngRAsTreeBGColour = &HF8F8F8 '== &HF0F0F0
        Call mSetTreeViewColour(mTvwRAs, mLngRAsTreeBGColour) '===== RGB(&HB0, &HC4, &HDE))  '--lt steel blue.-
        mTvwRAs.HotTracking = False

        mLabStatus.Text &= "Initialising RA TreeView.." & vbCrLf
        Call mbInitialiseRAsTreeView(mTvwRAs)

        mLabStatus.Text &= "Refreshing RA TreeView.."
        Call mbRefreshRAsTreeView(mTvwRAs)
        mbIsInitialising = False

    End Sub  '-new--
    '= = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '-staffSignedOn-
    '-staffSignedOn-

    Public Sub staffSignedOn(ByVal intStaff_id As Integer, _
                              ByVal sStaffBarcode As String, _
                               ByVal sStaffName As String)
        mIntStaff_Id = intStaff_id
        msStaffBarcode = sStaffBarcode
        msStaffName = sStaffName
    End Sub  '-staffSignedOn-
    '= = = =  = = = = = = = 

    '-- PUBLIC-  called from Control Events on JobMatix Main form.

    '== timer interrupt ==

    Public Sub TimerRAs_Tick(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) '= Handles TimerRAs.Tick

        '== Dim dtime1, dTime2 As Date
        '== Dim ix, lDuration As Integer
        Dim tabIndex_Renamed As Short
        '== Dim s1 As String
        '== Dim sBarcode As String
        '== Dim form1 As Form
        Dim lRow, lCol As Integer
        Dim lngRAId As Integer
        Dim colRowValues As Collection
        Dim colKeys As Collection
        Dim colRecord As Collection
        Dim nodeX As System.Windows.Forms.TreeNode
        Dim item1 As System.Windows.Forms.ListViewItem
        '==Dim cellCurrent As DataGridViewCell

        '--signon can only be idle for 300 secs..--
        '==If (mlStaffTimeout < 0) Then '--SUSPENDED timing out..--
        '== Else '--timer active..-
        '--not MAIN timeout..-
        If ((mlStaffTimeout Mod 60) = 0) Then '--refresh time..-
            mlStaffTimeout = mlStaffTimeout + 1 '--make sure we don't repeat it immediately...--
            If Not (mBrowseRAs1 Is Nothing) Then
                '==  lRow = MSHFlexGridRAs.Row '--save current RAs row..-
                lRow = -1
                If mDataGridViewRAs.CurrentCell IsNot Nothing Then lRow = mDataGridViewRAs.CurrentCell.RowIndex
                Call mBrowseRAs1.refresh()
                If (lRow >= 0) And (lRow < mDataGridViewRAs.RowCount) Then
                    mDataGridViewRAs.CurrentCell = mDataGridViewRAs.Rows(lRow).Cells(0)
                End If '--row-
            End If '--nothing..--
            If (mChkAutoRefreshRAs.CheckState = 1) Then Call mbRefreshRAsTreeView(mTvwRAs)
        Else '--no timeouts..-
            '==tabIndex = SSTabMain.Tab
            '==If (tabIndex <= K_MAXJOBTABS) Then   '--showing jobs..-
            If mFrameRAsTab.Visible Then '--showing RA's..-
                '==If FrameDetails.Visible Then FrameDetails.Visible = False
                '==If MSHFlexGridRAs.Enabled Then
                tabIndex_Renamed = 1
                If mFrameBrowseRAs.Visible Then
                    '--show current RA..-
                    If (mDataGridViewRAs.SelectedRows.Count > 0) Then
                        '--  use 1st selected row only.
                        lRow = mDataGridViewRAs.SelectedRows(0).Cells(0).RowIndex
                        If (lRow >= 0) Then '--NOT in header row--
                            mLngSelectedRowRAs = lRow
                            Call mBrowseRAs1.SelectRecord(mLngSelectedRowRAs, colKeys, colRowValues)
                            If Not (colKeys Is Nothing) Then
                                '==MsgBox "Nothing selected.", vbExclamation
                                If colKeys.Count() > 0 Then '--we have selection..-
                                    '-- Passing JOB-NO to get details....--
                                    lngRAId = CInt(colKeys.Item(1))
                                    '== If (lngRAId <> mlRAId) Or (tabIndex <> miLastTabNo) Then '--different panel or row..-
                                    If (lngRAId <> mlRAId) Then '--different panel or row..-
                                        Call mbShowRAInfo(lngRAId)
                                    End If
                                End If '--keys.-
                            End If '--nothing.-
                        Else '--no row..-
                            '===Call mbClearDetailsFrame    '--clear details..-
                        End If '--row 0--
                    End If  '--sel cpont.-
                End If '--frame visible..-
            End If '--jobs/RAs.-
            '== miLastTabNo = tabIndex  '--remenber this tab..-
        End If '--timeout..-
        '== End If '--active-
        nodeX = Nothing
        item1 = Nothing
        colKeys = Nothing
        colRowValues = Nothing
        colRecord = Nothing
    End Sub '--timer--
    '= = = = =  = = =
    '-===FF->

    '-- RAs Tree and Grid..
    '-- RAs Tree and Grid..

    Public Sub tvwRAs_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        '==Handles tvwRAs.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim nodeX As System.Windows.Forms.TreeNode

        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            nodeX = mTvwRAs.SelectedNode
            If Not (nodeX Is Nothing) Then
                '== Call cmdViewRecordRAs_Click(mCmdViewRecordRAs, New System.EventArgs())
                Call cmdViewRecordRAs_Click(New Button, New System.EventArgs) '= mbViewRecordRAs_Click()
            End If
            iKeyAscii = 0 '--done..-
        End If
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = =  = = =

    Public Sub tvwRAs_DoubleClick(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) '= Handles tvwRAs.DoubleClick
        Dim nodeX As System.Windows.Forms.TreeNode
        nodeX = mTvwRAs.SelectedNode
        If Not (nodeX Is Nothing) Then
            '== Call cmdViewRecordRAs_Click(mCmdViewRecordRAs, New System.EventArgs())
            Call cmdViewRecordRAs_Click(New Button, New System.EventArgs) '=mbViewRecordRAs_Click()
        End If
    End Sub '--keypress..-
    '= = = = = =  = = =

    '-- TreeView Node Click--

    Public Sub tvwRAs_NodeClick(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.Windows.Forms.TreeNodeMouseClickEventArgs) _
                                       '==  Handles tvwRAs.NodeMouseClick
        Dim nodeX As System.Windows.Forms.TreeNode = eventArgs.Node
        Dim sKey As String
        Dim s1 As String
        '== Dim sStatus As String
        '== Dim k, i, j,
        Dim lngRAId As Integer
        '== Dim bToNotify As Boolean

        sKey = LCase(nodeX.Name)
        '--MsgBox sKey + vbCrLf + "was clicked..", vbInformation
        If VB.Left(sKey, 3) = "ra-" Then
            '--lngJobId = CLng(Mid(sKey, 5)) '--bypass "job-" --
            s1 = Mid(sKey, 4)
            If IsNumeric(s1) Then
                lngRAId = CInt(s1)
                Call mbShowRAInfo(lngRAId)
                mCmdViewRecordRAs.Enabled = True
            End If '--numeric.-
            If My.Computer.Keyboard.ShiftKeyDown Then
                '= MsgBox("Shift key is down..", MsgBoxStyle.Information)
                nodeX.ForeColor = Color.Firebrick
            Else
                nodeX.ForeColor = Color.Black
            End If
        Else
            mFrameRADetails.Visible = False
        End If
        mlStaffTimeout = 0 '--restart timing out..  normal--
    End Sub '--node click..
    '= = = = = = = = = = = =
    '-===FF->

    Public Sub cmdRefreshRAsTree_Click(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) '= Handles cmdRefreshRAsTree.Click

        Call mbRefreshRAsTreeView(mTvwRAs)
        mlStaffTimeout = 0 '--restart timing out..  normal--
        mTvwRAs.Focus()

    End Sub '--refresh.-
    '= = = = = = = = = =

    '-- SORT option..--

    Public Sub OptRATreeSort_CheckedChanged(ByVal eventSender As System.Object, _
                                                 ByVal eventArgs As System.EventArgs)
        '== Handles OptRATreeSort.CheckedChanged
        If eventSender.Checked Then
            '= Dim index As Short = OptRATreeSort.GetIndex(eventSender)

            Call mbRefreshRAsTreeView(mTvwRAs)
            '= mlStaffTimeout = 0 '--restart timing out..  normal--
        End If
    End Sub '--sort-
    '= = = = = = = =
    '-- end of tree stuff..
    '-===FF->

    '= RAS Search Grid Events..
    '== Called form ACTUAL event handler in main form..

    '-- RAs Grid..  Mouse Activity..--
    '-- RAs Grid..  Mouse Activity..--

    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '-- set new sort column--
    '--  Catch sorted event so we can highlight correct column..--

    Public Sub dataGridViewRAs_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs)  '= Handles mDataGridViewRAs.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = mDataGridViewRAs.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowseRAs1.SortColumn(sName)

    End Sub  '--sorted..-
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  select row to SHOW--

    Public Sub DataGridViewRAs_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As DataGridViewCellMouseEventArgs)
        '= Handles  '= mDataGridViewRAs.CellMouseClick
        Dim intRow, lngRAId As Integer
        Dim colRowValues As Collection
        Dim colKeys As Collection

        '==MsgBox("CellMouseClick event.", MsgBoxStyle.Information)   '== TEST ==
        If eventArgs.Button = MouseButtons.Left Then '--left --
            '=lCol = eventArgs.ColumnIndex
            intRow = eventArgs.RowIndex
            If (intRow >= 0) Then '--ok row--
                '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
                If (mLngSelectedRowRAs <> intRow) Then  '-row changed.
                    mLngSelectedRowRAs = intRow
                    Call mBrowseRAs1.SelectRecord(mLngSelectedRowRAs, colKeys, colRowValues)
                    If Not (colKeys Is Nothing) Then
                        '==MsgBox "Nothing selected.", vbExclamation
                        If colKeys.Count() > 0 Then '--we have selection..-
                            '-- Passing RA-NO to get details....--
                            lngRAId = CInt(colKeys.Item(1))
                            If (lngRAId <> mlRAId) Then '--different panel or row..-
                                Call mbShowRAInfo(lngRAId)
                                mCmdViewRecordRAs.Enabled = True
                            End If
                        End If '--keys.-
                    End If '--nothing.-
                End If '-row changed.
            End If '--row--
        End If  '--button-
    End Sub '--click--
    '= = = = = = = = = =

    '-- Enter Row..  update RA detail..-

    Public Sub dataGridViewRAs_RowEnter(ByVal sender As Object, _
                                         ByVal EventArgs As DataGridViewCellEventArgs) '= Handles DataGridViewRAs.RowEnter
        Dim intRow, lngRAId As Integer
        Dim colRowValues As Collection
        Dim colKeys As Collection

        '== MsgBox("RowEnter event.", MsgBoxStyle.Information)   '== TEST ==
        '=lCol = EventArgs.ColumnIndex
        intRow = EventArgs.RowIndex
        If (intRow >= 0) Then '--ok row--
            If (mLngSelectedRowRAs <> intRow) Then  '-row changed.
                mLngSelectedRowRAs = intRow
                Call mBrowseRAs1.SelectRecord(mLngSelectedRowRAs, colKeys, colRowValues)
                If Not (colKeys Is Nothing) Then
                    '==MsgBox "Nothing selected.", vbExclamation
                    If colKeys.Count() > 0 Then '--we have selection..-
                        '-- Passing RA-NO to get details....--
                        lngRAId = CInt(colKeys.Item(1))
                        If (lngRAId <> mlRAId) Then '--different panel or row..-
                            Call mbShowRAInfo(lngRAId)
                            mCmdViewRecordRAs.Enabled = True
                        End If
                    End If '--keys.-
                End If '--nothing.-
            End If  '-changed-
        End If '--ok row--
    End Sub  '--row enter-
    '= = = = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  select row to edit--

    Public Sub DataGridViewRAs_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                ByVal eventArgs As DataGridViewCellMouseEventArgs)
        '== Handles '= mDataGridViewRAs.CellMouseDoubleClick
        Dim lRow, lCol As Integer
        lRow = eventArgs.RowIndex
        If lRow >= 0 Then '--ok row--
            '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
            mLngSelectedRowRAs = lRow
            Call cmdViewRecordRAs_Click(New Button, New System.EventArgs) '=Call mbViewRecordRAs_Click()
            '--Call cmdExit_Click
        End If '--row--
    End Sub '--DBL click--
    '= = = = = = = = = =

    '--key activity---  select row to edit--

    Public Sub DataGridViewRAs_KeyUp(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As KeyEventArgs) '= Handles DataGridViewRAs.KeyUp

        Dim lRow, lCol As Integer

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        '== If eventArgs.keyAscii = System.Windows.Forms.Keys.Return Then
        If eventArgs.Control AndAlso (eventArgs.KeyCode = Keys.Enter) Then
            If lRow >= 0 Then '--ok row--
                '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                mLngSelectedRowRAs = lRow
                Call cmdViewRecordRAs_Click(New Button, New System.EventArgs) '=Call mbViewRecordRAs_Click()
                '--Call cmdExit_Click
            End If '--row--
            '== eventArgs.keyAscii = 0 '--processed--
            eventArgs.Handled = True '--processed--
        End If '--enter--
    End Sub '--key-up--
    '= = = = = = = = = = =
    '-===FF->

    '-- RAs Browser.. txt FIND Activity.--
    '-- RAs Browser.. txt FIND Activity.--

    '--BROWSING RAs.. --

    '--key activity---  select row to edit--
    Public Sub txtFindRAs_KeyPress(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        '== Handles mTxtFindRAs.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If mDataGridViewRAs.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = mDataGridViewRAs.SelectedRows(0).Cells(0).RowIndex
                If lRow >= 0 Then '--OK row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRowRAs = lRow
                    '=== Call mBrowse1.SelectRecord(lRow)
                    '======mbBrowseCompleted = True
                    '===If (mButtonCurrentBrowseRAs.Key = "viewall") Then Call cmdViewRecordRAs_Click
                    '--Call cmdExit_Click
                End If '--row--
                iKeyAscii = 0 '--processed--
            End If  '--sel-count.-
        End If '--enter--
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--click--
    '= = = = = = = = = = =

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Public Sub txtFindRAs_Enter(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) '== Handles txtFindRAs.Enter
        mLabFindRAs.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        mLabFindRAs.Font = VB6.FontChangeBold(mLabFindRAs.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Public Sub txtFindRAs_Leave(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) '= Handles txtFindRAs.Leave
        mLabFindRAs.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        mLabFindRAs.Font = VB6.FontChangeBold(mLabFindRAs.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    Public Sub txtFindRAs_TextChanged(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs)  '= Handles txtFindRAs.TextChanged

        Call mBrowseRAs1.Find(mTxtFindRAs)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '== RA's Grid/Search events..

    '-- ToolbarRAs for RA Browse..--
    '---- just sets up browser..

    Public Sub ToolbarRAsGrid_ButtonClick(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs)
        '= Handles _ToolbarRAs_ButtonQueued.Click, _ToolbarRAs_ButtonRequested.Click, _
        '= _ToolbarRAs_ButtonGranted.Click, _ToolbarRAs_ButtonShipped.Click, _
        '= _ToolbarRAs_ButtonCompleted.Click, _ToolbarRAs_ButtonAll_RAs.Click

        Dim Button As System.Windows.Forms.ToolStripItem = CType(eventSender, System.Windows.Forms.ToolStripItem)
        Dim sWhere As String
        Dim sTitle As String
        Dim colPrefs As Collection
        Dim cx As Integer
        Dim table1 As Collection
        Dim column1 As Collection
        Dim sSql As String '--search sql..--
        Dim s1, s2 As String
        Dim asColumns() As String

        '== If msStaffName = "" Then Exit Sub  '--was signed off..-
        If Button Is Nothing Then Exit Sub
        mButtonCurrentBrowseRAs = Button '--save button so we can repeat browse..

        '--unclick all six buttons..-

        CType(mToolbarRAsGrid.Items.Item("_toolbarRAs_ButtonQueued"), ToolStripButton).Checked = False
        CType(mToolbarRAsGrid.Items.Item("_toolbarRAs_ButtonRequested"), ToolStripButton).Checked = False
        CType(mToolbarRAsGrid.Items.Item("_toolbarRAs_ButtonGranted"), ToolStripButton).Checked = False
        CType(mToolbarRAsGrid.Items.Item("_toolbarRAs_ButtonShipped"), ToolStripButton).Checked = False
        CType(mToolbarRAsGrid.Items.Item("_toolbarRAs_ButtonCompleted"), ToolStripButton).Checked = False
        CType(mToolbarRAsGrid.Items.Item("_toolbarRAs_ButtonAll_RAs"), ToolStripButton).Checked = False

        '-- check this button.- 
        CType(Button, ToolStripButton).Checked = True

        colPrefs = mColPrefsRAs '--default..-
        Select Case LCase(Button.Tag)
            Case "queued"
                sWhere = " (LEFT(RA_Status,2)<='10') " '--created..-
                sTitle = "RA's   Queued "
            Case "requested"
                sWhere = " (LEFT(RA_Status,2)='20')  " '--requested..-
                sTitle = "RMA's   Requested."
            Case "granted"
                sWhere = " (LEFT(RA_Status,2)='30')  " '--granted..-
                sTitle = "RMA's   Granted."
            Case "shipped"
                sWhere = " (LEFT(RA_Status,2)='50')  " '--shipped..-
                sTitle = "RMA's-   Goods shipped.."
            Case "completed"
                sWhere = " (LEFT(RA_Status,2)>='70')  " '--done ok, or refused..-
                sTitle = "RMA's    Completed."
            Case "viewall"
                sWhere = "" '-- ALL RA's  ---  "(LEFT(RA_Status,2)<='10')"   '--created..-
                sTitle = "All RA's"
            Case Else
        End Select
        '--  add search argument if any.-
        '--TEXT SEARCH request--
        '--TEXT SEARCH request--
        '-- build query to srch all text cols..--
        '=======sSearchArg = Trim(txtSearch.Text)    '==UCase(request.Form("selTextSearchArg"))
        sSql = "" '---"SELECT * FROM " + msProductTableName + " WHERE "
        s1 = "" : s2 = "" '--result-
        '-- arg can be multiple tokens..--
        '===asArgs = Split(Trim(txtSearch(index).Text))
        table1 = mColSqlDBInfo.Item("RAItems")
        cx = 0 : Erase asColumns
        '-- get list of text-type columns..-
        For Each column1 In table1.Item("FIELDS")
            '===If gbIsText(gsGetSqlType(column1.Type, column1.DefinedSize)) Then  '-- is text col..-
            If gbIsText(column1.Item("TYPE_NAME")) Then '-- is text col..-
                ReDim Preserve asColumns(cx)
                asColumns(cx) = column1.Item("NAME")
                cx = cx + 1
            End If
        Next column1 '--
        '-- Every Arg. must be represented in some column in the row for--
        '--   the row to be selected.--
        '-- So we need a WHERE clause that looks like:  --
        '---- ((col-x LIKE arg-1) OR (col-y LIKE arg-1) OR .... (col-z LIKE arg-1))
        '----   AND ((col-x LIKE arg-2) OR (col-y LIKE arg-2) OR .... (col-z LIKE arg-2))
        '----   ....  AND  ((col-x LIKE arg-N) OR (col-y LIKE arg-N) OR .... (col-z LIKE arg-N))
        '-- (poor man's version of FULL-TEXT search..)  --
        sSql = gbMakeTextSearchSql(Trim(mTxtRASearch.Text), asColumns)
        '--add srch args if any..-
        If (sSql <> "") Then
            If (sWhere <> "") Then sWhere = sWhere & " AND "
            sWhere = sWhere & sSql
        End If
        Call mbShowRAsBrowse(colPrefs, sTitle, sWhere)
        mCmdViewRecordRAs.Enabled = True

    End Sub '--toolbar RAs.--
    '= = = = = = = = =
    '-===FF->

    '-- RA Search..-

    Public Sub txtRASearch_Enter(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As System.EventArgs)  '= Handles txtRASearch.Enter
        mTxtRASearch.SelectionStart = 0
        mTxtRASearch.SelectionLength = Len(mTxtRASearch.Text)
    End Sub '--got focus..-
    '= = = = = = = = = = =

    Public Sub txtRASearch_KeyPress(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        '= Handles txtRASearch.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)

        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            Call cmdRASearch_Click(mCmdRASearch, New System.EventArgs())
            iKeyAscii = 0 '--done..-
        End If '--CR-
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--keypress..-
    '= = = = = = = =

    '-- Clear--
    Public Sub cmdClearRASearch_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs)  '= Handles cmdClearRASearch.Click
        mTxtRASearch.Text = ""
        System.Windows.Forms.Application.DoEvents()
        Call cmdRASearch_Click(mCmdRASearch, New System.EventArgs)
    End Sub '--clear--
    '= = = = = = =
    '-===FF->

    '-- Search add some more crteria --
    '--  to the current Browse parameters..-

    Public Sub cmdRASearch_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) '= Handles cmdRASearch.Click


        If mButtonCurrentBrowseRAs Is Nothing Then
            '==MsgBox "Please Select a status filter first.", vbInformation
        Else
            Call ToolbarRAsGrid_ButtonClick(mButtonCurrentBrowseRAs, New System.EventArgs()) '--refresh browse grid..-
        End If

        '======= cmdViewRecordRAs.Enabled = False
    End Sub '--search..-
    '= = = = = = = = == =
    '-===FF->

    '== RAS Supplier Grid events..

    '=3311.313'-- RA Suppliers Data Grid.
    '--    R A  S U P P L I E R  --  BROWSING..
    '--    R A  S U P P L I E R  --  BROWSING..
    '--    R A  S U P P L I E R  --  BROWSING..

    '--  F l e x G r i d  E v e n t s..--
    '--  F l e x G r i d  E v e n t s..--
    '--mouse activity---  select col-headers--
    '-- set new sort column--

    '--  Catch sorted event so we can highlight correct column..--

    Public Sub dgvRA_suppliers_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs)  '= Handles dgvRA_suppliers.Sorted

        Dim sName As String
        '-- get new sort column..--
        Dim currentColumn As DataGridViewColumn = mDgvRA_suppliers.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText
        Call mBrowseSupplier1.SortColumn(sName)
        '==  Me.DataGridView1.FirstDisplayedCell = Me.DataGridView1.CurrentCell
    End Sub  '--sorted-
    '= = = = = = = = =  = = =
    '-===FF->

    '-- Enter Row..  update RA list for supplier...-

    '== THIS causes re-entrancy problems. ????  --

    Public Sub dgvRA_suppliers_RowEnter(ByVal sender As Object, _
                                         ByVal EventArgs As DataGridViewCellEventArgs)   '== Handles dgvRA_suppliers.RowEnter
        Dim intSupplier_Id, intRow, lngRAId As Integer
        Dim colRowValues As Collection
        Dim colKeys, colRecord As Collection
        Dim sBarcode, sName, svalue As String

        '== MsgBox("RowEnter event.", MsgBoxStyle.Information)   '== TEST ==
        intRow = EventArgs.RowIndex
        If (intRow >= 0) Then '--ok row--
            mLngSelectedRowSupplier = intRow
            Call mBrowseSupplier1.SelectRecord(mLngSelectedRowSupplier, colKeys, colRowValues)
            If Not (colRowValues Is Nothing) Then
                If (colRowValues.Count > 0) Then
                    '= sName = CStr(colRowValues.Item(1)("name"))
                    '== svalue = CStr(colRowValues.Item(1)("value"))
                    sBarcode = CStr(colRowValues.Item("barcode")("value"))
                    If Not mRetailHost1.supplierGetSupplierRecord(sBarcode, -1, colRecord) Then
                        MsgBox("Failed to retrieve supplier record ( " & _
                                                    "Barcode = " & sBarcode & ") ..", MsgBoxStyle.Exclamation)
                    Else '--ok--
                        '--set up supplier details.-
                        mColSelectedSupplierRecord = colRecord  '--save supplier address for shipping label-
                        '= mLabRA_supplierName.Text = colRecord("supplier")("value")
                        intSupplier_Id = CInt(colRecord("supplier_id")("value"))
                        Call mbSetUpSupplier(colRecord)
                        '-- AND get all RAs for this supplier-
                        Call mbShowSupplierRAs(intSupplier_Id)
                        mChkSelectAllRAsGranted.Checked = False '--uncheck all-
                    End If '--get customer..-
                End If  '--count-
            End If '--nothing.-
        End If '--ok row--
    End Sub  '--row enter-
    '= = = = = = = =  == = = =
    '-===FF->

    '--mouse activity---  select row to SHOW--
    '--mouse activity---  select row to SHOW--

    Public Sub dgvRA_suppliers_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                               ByVal eventArgs As DataGridViewCellMouseEventArgs)
        '==  Handles dgvRA_suppliers.CellMouseClick
        Dim intSupplier_Id, lRow, lCol, lngError As Integer
        Dim colRowValues As Collection
        Dim sBarcode As String
        Dim colKeys As Collection
        Dim colRecord As Collection
        Dim sName, sValue As String

        On Error GoTo dgvRA_suppliers_Click_Error
        If (eventArgs.Button = MouseButtons.Left) Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            If (lRow >= 0) Then '--NOT in header row--
                '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
                mLngSelectedRowSupplier = lRow
                Call mBrowseSupplier1.SelectRecord(mLngSelectedRowSupplier, colKeys, colRowValues)
                If Not (colRowValues Is Nothing) Then
                    If (colRowValues.Count > 0) Then
                        '= sName = CStr(colRowValues.Item(1)("name"))
                        '= sValue = CStr(colRowValues.Item(1)("value"))
                        sBarcode = CStr(colRowValues.Item("barcode")("value"))
                        If Not mRetailHost1.supplierGetSupplierRecord(sBarcode, -1, colRecord) Then
                            MsgBox("Failed to retrieve supplier record ( " & _
                                                       sName & "= " & sValue & ") ..", MsgBoxStyle.Exclamation)
                        Else '--ok--
                            '--set up supplier details.-
                            mColSelectedSupplierRecord = colRecord  '--save supplier address for shipping label-
                            '= mLabRA_supplierName.Text = colRecord("supplier")("value")
                            intSupplier_Id = CInt(colRecord("supplier_id")("value"))
                            Call mbSetUpSupplier(colRecord)
                            '-- AND get all RAs for this supplier-
                            Call mbShowSupplierRAs(intSupplier_Id)
                            mChkSelectAllRAsGranted.Checked = False '--uncheck all-

                        End If '--get customer..-
                    End If  '--count-
                End If '--nothing.-
            End If '--row--
        End If  '--button.-
        Exit Sub

dgvRA_suppliers_Click_Error:
        lngError = Err.Number()
        MsgBox("Runtime Error in JobMatix dgvRA_supplier_Click (" & lRow & "/" & lCol & ") sub.." & vbCrLf & _
                "Error is " & lngError & " = " & ErrorToString(lngError))

    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '--mouse activity---  select row to edit--

    Public Sub dgvRA_suppliers_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                      ByVal eventArgs As DataGridViewCellMouseEventArgs)
        '= Handles dgvRA_suppliers.CellMouseDoubleClick
        Dim lCol, lRow, lngId As Integer
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim sBarcode As String

        lRow = eventArgs.RowIndex
        If lRow >= 0 Then '--ok row--
            '--MsgBox "dbl click on Row: " & lRow & ", col :" & lCol
            mLngSelectedRowSupplier = lRow
            Call mBrowseSupplier1.SelectRecord(mLngSelectedRowSupplier, colKeys, colSelectedRow)

            If colSelectedRow.Count() > 0 Then
                sBarcode = colSelectedRow.Item("barcode")("value")
                lngId = CInt(colSelectedRow.Item("Supplier_id")("value"))
                '==If Not mbLookupCustomerId(lngId, colRecord) Then
                If Not mRetailHost1.supplierGetSupplierRecord(sBarcode, lngId, colRecord) Then '--not found..--
                    MsgBox("Failed to retrieve supplier record (Id " & lngId & ") " & vbCrLf & " for Barcode: '" & sBarcode & "'..", MsgBoxStyle.Exclamation)
                Else '--ok--
                    '--set up customer details.-
                    '== =    ????  ==  Call mbSetupCustomer(colRecord)


                    '-- Get all jobs for this customer, and load the ListviewCustJobs.--

                    '==FrameBrowse.Visible = False
                End If
            Else
                If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
            End If '--got row..--
        End If '--row--
    End Sub '--dbl-click--
    '= = = = = = = = = =
    '-===FF->

    '--key activity---  select row to edit--
    '--  CATCH  << CTL-ENTER >>  ---

    Public Sub dgvRA_suppliers_KeyUp(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As KeyEventArgs) '= Handles dgvRA_suppliers.KeyUp

        Dim lCol, lRow, lngId As Integer
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim sBarcode As String

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        '== If eventArgs.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
        If eventArgs.Control AndAlso (eventArgs.KeyCode = Keys.Enter) Then
            If mDgvRA_suppliers.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = mDgvRA_suppliers.SelectedRows(0).Cells(0).RowIndex
                If lRow >= 0 Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRowSupplier = lRow
                    Call mBrowseSupplier1.SelectRecord(mLngSelectedRowSupplier, colKeys, colSelectedRow)
                    If colSelectedRow.Count() > 0 Then
                        '= sName = CStr(colSelectedRow.Item(1)("name"))
                        '= sValue = CStr(colSelectedRow.Item(1)("value"))
                        sBarcode = CStr(colSelectedRow.Item("barcode")("value"))
                        If Not mRetailHost1.supplierGetSupplierRecord(sBarcode, -1, colRecord) Then
                            MsgBox("Failed to retrieve supplier record ( " & sBarcode & ") ..", MsgBoxStyle.Exclamation)
                        Else '--ok--
                            '--set up customer details.-
                            '-- Get all jobs for this customer, and load the ListviewCustJobs.--
                            '=   Call mbSetupSupplierInfo(colRecord, True)

                        End If '--get customer..-
                    Else
                        If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                    End If '--got row..--
                End If '--row--
                eventArgs.Handled = True '--processed--
            End If  '--count-
            '== ElseIf eventArgs.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Escape) Then
        End If '--enter--
    End Sub '--click--
    '= = = = = = = = = = =
    '-===FF->

    '--BROWSING SUPPLIERS.. --

    '--SUPP key activity---  select row to edit--
    Public Sub txtFindSupplier_KeyPress(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)
        '= Handles txtFindSupplier.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        Dim colKeys As Collection
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim sBarcode As String

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            lRow = mDgvRA_suppliers.CurrentRow.Cells(0).RowIndex
            If lRow >= 0 Then '--ok row--
                '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                mLngSelectedRowSupplier = lRow
                Call mBrowseSupplier1.SelectRecord(mLngSelectedRowSupplier, colKeys, colSelectedRow)
                If colSelectedRow.Count() > 0 Then
                    '= sName = CStr(colSelectedRow.Item(1)("name"))
                    '- sValue = CStr(colSelectedRow.Item(1)("value"))
                    sBarcode = CStr(colSelectedRow.Item("barcode")("value"))
                    If Not mRetailHost1.supplierGetSupplierRecord(sBarcode, -1, colRecord) Then
                        MsgBox("Failed to retrieve supplier record ( " & sBarcode & ") ..", MsgBoxStyle.Exclamation)
                    Else '--ok--
                        '--set up customer details.-
                        '-- Get all jobs for this customer, and load the ListviewCustJobs.--
                        '==   Call mbSetupSupplierInfo(colRecord, True)
                    End If '--get supplier..-
                Else
                    If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
                End If '--got row..--

            End If '--row--
            iKeyAscii = 0 '--processed--
        End If '--enter--
        eventArgs.KeyChar = Chr(iKeyAscii)
        If iKeyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--click--
    '= = = = = = = = = = =

    '-- Highlight FIND entry.--
    '-- Highlight FIND entry.--

    Public Sub txtFindSupplier_Enter(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.EventArgs)  '= Handles txtFindSupplier.Enter
        mLabFindSupplier.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDACTIVEBG)
        mLabFindSupplier.Font = VB6.FontChangeBold(mLabFindSupplier.Font, True)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    Public Sub txtFindSupplier_Leave(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) '== Handles txtFindSupplier.Leave

        mLabFindSupplier.BackColor = System.Drawing.ColorTranslator.FromOle(K_FINDINACTIVEBG)
        mLabFindSupplier.Font = VB6.FontChangeBold(mLabFindSupplier.Font, False)

    End Sub '--gotfocus--
    '= = = = = = = = = =

    '--BROWSING..  catch Find text box changes..-

    'UPGRADE_WARNING: Event txtFindCust.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    Public Sub txtFindSupplier_TextChanged(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs)  '=  Handles txtFindSupplier.TextChanged

        Call mBrowseSupplier1.Find(mTxtFindSupplier)
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =

    Public Sub chkShowGrantedRAsOnly_CheckedChanged(sender As Object, _
                                              ev As EventArgs) '== Handles chkShowGrantedRAsOnly.CheckedChanged
        If mbIsInitialising Then Exit Sub

        '-- btnRAsUpdateGroupSent only available is Granted selected..
        If mChkShowGrantedRAsOnly.Checked Then
            mBtnRAsUpdateGroupSent.Enabled = True
        Else
            mBtnRAsUpdateGroupSent.Enabled = False
        End If
        Call mbRefreshSuppliersGrid()

    End Sub  '-chkShowGrantedRAsOnly_CheckedChanged-
    '= = = = = = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--  Supplier.RA's  listview--
    '--  Show selected RA detail..--

    '--listViewJobs_Click--

    Public Sub listViewSupplierRAs_Click(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) '== Handles listViewSupplierRAs.Click
        Dim item1 As System.Windows.Forms.ListViewItem
        Dim lngJobId As Integer

        '--  update quote info display if selection has moved..--
        item1 = mListViewSupplierRAs.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            '= lngJobId = CInt(item1.Text) '--1st column has to be job_id..--
            '==    Call mbShowJobInfoRTF(lngJobId)
        End If '--selected..-

    End Sub '--listViewJobs_Click--
    '= = = = = = = =  =

    '---If any item is  selected, then show RA details..--

    Public Sub listViewSupplierRAs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '== Handles listViewSupplierRAs.SelectedIndexChanged

        Dim listItems As ListView.SelectedListViewItemCollection = mListViewSupplierRAs.SelectedItems

        If (listItems.Count > 0) Then  '--have selection..-
            Dim item1 As ListViewItem = listItems(0)   '--first selected.-
            '=Dim intRA_id As Integer = CInt(item1.Text)
            If IsNumeric(item1.Text) Then
                Dim intRA_id As Integer = CInt(item1.Text)
                Call mbShowRAInfo(intRA_id)
            End If
        End If  '-count-
    End Sub  '-index changed.-
    '= = = = = = = = = = = = 
    '-===FF->

    Public Sub listViewSupplierRAs_dblClick(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) '= Handles listViewSupplierRAs.DoubleClick
        Dim item1 As System.Windows.Forms.ListViewItem
        item1 = mListViewSupplierRAs.FocusedItem
        If (item1 Is Nothing) Then '--no selection..-
            Exit Sub
        Else
            '= Call cmdViewRecord_Click()
        End If
    End Sub
    '= = = = = = = = = 

    Public Sub chkSelectAllRAsGranted_CheckedChanged(eventSender As Object, _
                                                  eventArgs As EventArgs) '= Handles chkSelectAllRAsGranted.CheckedChanged
        Dim checkbox1 As CheckBox = CType(eventSender, CheckBox)
        '-- if checked, then check all RAS in ListView..
        '--   Else uncheck them all.
        If checkbox1.Checked Then  '--check all-
            For Each item1 As ListViewItem In mListViewSupplierRAs.Items
                item1.Checked = True
            Next item1
        Else  '-uncheck all-
            For Each item1 As ListViewItem In mListViewSupplierRAs.Items
                item1.Checked = False
            Next item1
        End If  '--checked-
    End Sub  '-- chkSelectAllRAsGranted-
    '= = = = = = = = = = = = =  = = = = = 
    '-===FF->

    '- Print/Update Group-
    '==  3357.0207
    '--    ALSO- Build GoodsSent collection for POS.

    Public Sub btnRAsUpdateGroupSent_Click(eventSender As Object, _
                                              eventArgs As EventArgs)  '= Handles btnRAsUpdateGroupSent.Click
        '- count checked items-
        Dim colRAItems, col1 As Collection
        Dim sTrackingNo As String = ""
        Dim intRa_id, intAffected, intDtRowIndex As Integer
        Dim intCount As Integer = 0
        '--  Build collection of RA listView of items info for printing Group Shipping Label.
        '--  AND- Build collection of RA items info for POS Returns..
        Dim colPOS_RAItems, colPOS_Item As Collection
        Dim dt1 As DataRow

        colRAItems = New Collection
        colPOS_RAItems = New Collection

        For Each item1 As ListViewItem In mListViewSupplierRAs.Items
            If item1.Checked Then
                col1 = New Collection
                intRa_id = CInt(item1.Text)
                col1.Add(intRa_id, "ra_id")
                col1.Add(item1.SubItems("RA_SerialNumber").Text, "RA_SerialNumber")
                col1.Add(item1.SubItems("RM_ItemCat1").Text, "RM_ItemCat1")
                col1.Add(item1.SubItems("RM_ItemDescription").Text, "RM_ItemDescription")

                col1.Add(item1.SubItems("RM_ItemSupplierCode").Text, "RM_ItemSupplierCode")
                col1.Add(item1.SubItems("RA_SupplierRMA_No").Text, "RA_SupplierRMA_No")
                colRAItems.Add(col1, intRa_id)

                '==  3357.0207
                '- Build POS RA Item Info-
                colPOS_Item = New Collection
                intDtRowIndex = CInt(item1.SubItems("dtrowx").Text)
                '-  get static dt row for this item.. 
                '--  (from saved datatable mDtSupplierRAs).
                dt1 = mDtSupplierRAs.Rows(intDtRowIndex)
                colPOS_Item.Add(intRa_id, "RA_id")
                colPOS_Item.Add(item1.SubItems("RA_SupplierRMA_No").Text, "RA_SupplierRMA_No")
                colPOS_Item.Add(dt1.Item("RM_InvoiceNo"), "RM_InvoiceNo")
                colPOS_Item.Add(dt1.Item("RM_stockid"), "RM_stockid")
                colPOS_Item.Add(dt1.Item("RM_ItemBarcode"), "RM_ItemBarcode")
                colPOS_Item.Add(item1.SubItems("RM_ItemDescription").Text, "RM_ItemDescription")
                colPOS_Item.Add(dt1.Item("RM_SerialAudit_id"), "RM_SerialAudit_id")
                colPOS_Item.Add(item1.SubItems("RA_SerialNumber").Text, "RA_SerialNumber")
                colPOS_Item.Add(1, "quantity")
                colPOS_Item.Add(VB.Left(dt1.Item("RA_Symptoms"), 500), "RA_Symptoms")
                colPOS_Item.Add(VB.Right(dt1.Item("RA_RMA_RequestNotes"), 2040), "RA_RMA_RequestNotes")
                colPOS_RAItems.Add(colPOS_Item)

                intCount += 1
            End If
        Next item1
        If (intCount > 0) Then
            MsgBox("You have selected " & intCount & " RA's for shipping to Supplier." & _
                   "The program will now ask for a Courier Tracking Number, " & _
                       " print out a Group Shipping Label, " & _
                         "followed by a Packing Slip listing all items.." & vbCrLf & vbCrLf & _
                       "When confirmation is given, all included RAs will be updated with the Tracking No, " & _
                          "and marked with a status of 'Sent'..", MsgBoxStyle.Information)
            sTrackingNo = Trim(InputBox("Please enter or scan the Courier Tracking Number" & vbCrLf & _
                                  "For this group (package) of RA's.. "))
            If (sTrackingNo = "") Then
                MsgBox("No action taken..", MsgBoxStyle.Information)
                Exit Sub
            End If
            '-- confirm ok to print Shipping label..
            If Not (MsgBox("OK to print Group Shipping Label ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + MsgBoxStyle.Question) = MsgBoxResult.Yes) Then
                Exit Sub
            End If  '-ok-
            '--PRINT Shipping label with List of items from "colItems".
            Dim s1 As String = ""
            '-- test-
            For Each col1 In colRAItems
                s1 &= "RA_id=" & col1("ra_id") & ";  SerialNo=" & col1("RA_SerialNumber") & "; "
                s1 &= "Cat1=" & col1("RM_ItemCat1") & ";  Description=" & col1("RM_ItemDescription") & "; "
                s1 &= "SupplierCode=" & col1("RM_ItemSupplierCode") & ";  RMA_No=" & col1("RA_SupplierRMA_No") & "; "
                s1 &= vbCrLf & vbCrLf
            Next col1
            '= MsgBox("TESTING- Package items are: " & vbCrLf & vbCrLf & s1, MsgBoxStyle.Information)

            '-- print label with item list..
            Dim prtDocs1 As New clsPrintRAs

            '== prtDocs1.PrtSelectedPrinter = mPrtColour
            prtDocs1.PrtSelectedPrinterName = msColourPrinterName

            prtDocs1.Version = msJobMatixVersion
            prtDocs1.UserLogo = mImageUserLogo '=Picture2.Image
            '-Group of RA's--
            prtDocs1.RA_No = -1 '  mlRA_id
            prtDocs1.SupplierRMA = ""  '=txtSupplierRMA.Text

            s1 = VB6.Format(CDate(DateTime.Now), "dd-mmm-yyyy hh:mm")  '--now..-
            prtDocs1.HeaderDate = "Prepared by:  " & msStaffName & Space(4) & s1

            prtDocs1.Business = mColBusiness

            '== prtDocs1.RAStatus = msCurrentStatus

            prtDocs1.Supplier = msSupplierName
            prtDocs1.SupplierAddressInfo = msSupplierAddressInfo
            prtDocs1.SupplierMainPhone = msSupplierMainPhone

            '-- PUT in a Loop to print until happy..--
            '-- PUT in a Loop to print until happy..--
            '-- PUT in a Loop to print until happy..--
            Dim bDone As Boolean = False
            Do Until bDone
                '--  go-
                If prtDocs1.PrintShippingLabel(colRAItems) Then
                    If MsgBox("A Group Shipping Label has been sent to the printer:" & vbCrLf & _
                              msColourPrinterName & ".." & vbCrLf & _
                                "Ok to update all included RA's as 'Sent' ", _
                               MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                        '-- Accumulate UPDATEs for all RA's as SENT..
                        '= LabDateGoodsSent.Text = VB6.Format(CDate(DateTime.Now), "dd-mmm-yyyy hh:mm")
                        '= msActionUpdate = " RA_DateGoodsSentBack='" & LabDateGoodsSent.Text & "', " & _
                        '=                        " RA_Status='" & K_STATUS_GOODSSENT & "'"
                        '= msActionUpdate = msActionUpdate & ", RA_CourierBarcode='" & msFixSqlStr(sCourier) & "' "
                        Dim sDate As String = VB6.Format(CDate(DateTime.Now), "dd-MMM-yyyy hh:mm")
                        Dim sSqlUpdate, sErrorMsg As String
                        Dim sIdList As String = ""

                        Dim intSupplier_Id As Integer = CInt(mColSelectedSupplierRecord("supplier_id")("value"))

                        For Each col1 In colRAItems
                            If sIdList <> "" Then
                                sIdList &= ", "
                            End If
                            sIdList &= CStr(col1("ra_id"))
                        Next col1
                        sSqlUpdate = "UPDATE dbo.RAItems SET "
                        sSqlUpdate &= " RA_DateGoodsSentBack='" & sDate & "'"
                        sSqlUpdate &= ", RA_CourierBarcode='" & gsFixSqlStr(sTrackingNo) & "' "
                        sSqlUpdate &= ", RA_Status='" & K_STATUS_GOODSSENT & "'"
                        sSqlUpdate &= ", RM_StaffNameUpdated='" & msStaffName & "'"
                        sSqlUpdate &= ", RA_DateUpdated=CURRENT_TIMESTAMP "
                        sSqlUpdate &= "    WHERE (ra_id IN (" & sIdList & ") ); "

                        '- IF JobMatixPOS, then get POS to do RA update-
                        '-- in same transaction as Returns/Serials updating..
                        If mbIsJobmatixPOS() Then
                            Dim JMx31POS1 As JMxPOS330.clsJMxPOS31
                            '-mColSelectedSupplierRecord-
                            '=Dim intSupplier_Id As Integer = CInt(mColSelectedSupplierRecord("supplier_id")("value"))

                            JMx31POS1 = New JMxPOS330.clsJMxPOS31(mCnnSql, msServer, msSqlDbName, mColSqlDBInfo, gsRuntimeLogPath)
                            '-- do Update for all..
                            If Not JMx31POS1.POS_GoodsReturned(mIntStaff_Id, msStaffName, _
                                                                intSupplier_Id, "From JobMatixRAs..", _
                                                                          colPOS_RAItems, sSqlUpdate) Then
                                MsgBox("Failed to update POS Returns and RA status..", MsgBoxStyle.Exclamation)
                                Exit Sub
                            Else  '-ok-
                                '-- That's all..
                                bDone = True
                                MsgBox("POS Returns and RA Updates were completed ok.. ", MsgBoxStyle.Information)
                            End If  'POS-
                        Else  '-not POS.  is RM-
                            If Not gbExecuteCmd(mCnnSql, sSqlUpdate, intAffected, sErrorMsg) Then
                                MsgBox("ERROR in updating RA's.." & vbCrLf & sErrorMsg, MsgBoxStyle.Exclamation)
                                Exit Sub
                            Else  '-ok-
                                MsgBox("RA's update completed." & vbCrLf & _
                                       " and " & intAffected & " items were affected.", MsgBoxStyle.Information)
                                '-- That's all..
                                bDone = True
                            End If  '- exec-
                        End If  '-JobmatixPOS-
                        '3403.608- refresh-
                        Call mbShowSupplierRAs(intSupplier_Id)
                        '-done-
                    Else '-no to update=
                        If MsgBox("Print the group Label again ?" & vbCrLf, _
                              MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            '-- recycle to re-print..
                        Else  '-no update-  No reprint. 
                            bDone = True   '--just exit..
                        End If '-print again..
                    End If  '-yes-
                Else  '--failed-
                    '==3311.420=
                    Exit Do
                End If  '--printed=
            Loop
            prtDocs1 = Nothing
        Else
            MsgBox("No RA's have been selected (checked) for shipping..", MsgBoxStyle.Exclamation)
        End If  '--intCount-
    End Sub  '-btnRAsUpdateGroupSent-
    '= = = = = = = = = = = = = = = = 
    '-===FF->

    Public Sub cboRAs_A4Printers_SelectedIndexChanged(sender As Object, ev As EventArgs)
        '==  Handles cboRAs_A4Printers.SelectedIndexChanged

        If (mCboRAs_A4Printers.SelectedIndex >= 0) Then
            msColourPrinterName = mCboRAs_A4Printers.SelectedItem
            If Not mLocalSettings1.SaveSetting(k_RA_PrtSettingKey, msColourPrinterName) Then
                '= MsgBox("Failed to save RA printer setting.", MsgBoxStyle.Information)
            End If
        End If '-index-

    End Sub  '-cboRAs_A4Printers-
    '= = = = = = = = = = = = = =
    '-===FF->


    '--  END OF  -  R A  S U P P L I E R  --  BROWSING..
    '--  END OF  -  R A  S U P P L I E R  --  BROWSING..
    '--  END OF  -  R A  S U P P L I E R  --  BROWSING..
    '= = = = = = = = = = = =


    '== D O N E   RAS Supplier Grid events..
    '== D O N E   RAS Supplier Grid events..
    '-===FF->

    '==RA Toolbar- Switch between Tree and Grid etc..
    '== Now RAs Main TAB CONTROL..

    Public Sub TabControlRAs_selected(sender As TabControl, ev As TabControlEventArgs) '=Handles TabControlRAs.Selected

        Select Case LCase(mTabControlRAs.SelectedTab.Name)
            Case "tabpagerastree"
                mLabShowSearchRAs.Text = "Showing Active RA's Tree.."
                mLabShowSearchRAs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft  '--left--

            Case "tabpagerasgrid"
                mLabShowSearchRAs.Text = "Searching all RA's.."
                mLabShowSearchRAs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                If mButtonCurrentBrowseRAs Is Nothing Then    '--startup.. show all RAs..-
                    Call ToolbarRAsGrid_ButtonClick(mToolbarRAsGrid.Items("_toolbarRAs_ButtonAll_RAs"), New System.EventArgs()) '--refresh browse grid..-
                End If
                If Not mDataGridViewRAs.Enabled Then
                    '==  ????    Call cmdRASearch_Click(cmdRASearch, New System.EventArgs())
                Else
                    mTxtRASearch.Focus()
                End If
            Case "tabpagerassuppliers"
                mLabShowSearchRAs.Text = "Showing Suppliers"
                mLabShowSearchRAs.TextAlign = System.Drawing.ContentAlignment.MiddleRight

                If mButtonCurrentBrowseSupplier Is Nothing Then
                    '==Call mbShowSupplierBrowse(mColPrefsSupplier, "Browse Suppliers", msWhereRAsExist)
                    Call mbRefreshSuppliersGrid()
                End If
        End Select
    End Sub  '-- TabControlRAs_selected-
    '= = = = = = = = = = = = = = = = = = =
    '-===FF->

    '--RA Tree-
    '--RAs Search Grid..- 
    '-- Suppliers-

    '== G O N E ===

    '== G O N E ===
    '== G O N E ===
    '== G O N E ===

    Public Sub ToolStripButtonRAs_Click(sender As Object, ev As EventArgs)
        '== Handles ToolStripButtonRAsTree.Click, _
        '== ToolStripButtonSearchRAs.Click, _
        '== ToolStripButtonSuppliers.Click()

        Dim button1 As ToolStripButton = CType(sender, ToolStripButton)

        '--unclick all buttons..-
    End Sub  '-ToolStripButtonRAsTree_Click-
    '= = = = = = = = = = = = = =  = = = = = = =
    '-===FF->

    '-- RAs External Form calls..
    '-- RAs External Form calls..
    '-- RAs External Form calls..

    '-- R A 's -----
    '-- R A 's -----

    Private Sub mnuCreateNewRA_Click()
        Dim frmNewRA1 As frmNewRA

        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        If Not mbLicenceOK Then
            MsgBox("No valid licence found for this JobMatix site.." & vbCrLf & _
                   "(See JobMatix website for Licence details..) ", MsgBoxStyle.Exclamation)
        End If

        frmNewRA1 = New frmNewRA

        '=== SOON ===  frmNewRA.connectionSql = mCnnSql
        '== frmNewRA.connectionJet = mCnnJet
        frmNewRA1.connectionSql = mCnnSql
        '==3357.0206=
        frmNewRA1.server = msServer
        frmNewRA1.dbName = msSqlDbName

        frmNewRA1.dbInfoSql = mColSqlDBInfo
        '== frmNewRA.dbInfoJet = mColJetDBInfo
        frmNewRA1.retailHost = mRetailHost1

        frmNewRA1.V20_Only = False '= mbV20_Only  '--if no jobless RAs.--
        frmNewRA1.LocalSettingsPath = msLocalRAsSettingsPath() '=msAppPath & K_SAVESETTINGSPATH

        '== NOT USED  == frmNewRA.prtReceipt = mPrtReceipt
        '======= If Not mbLicenceOk Then MsgBox "Product does not have a valid Licence..", vbExclamation
        frmNewRA1.BusinessABN = msBusinessABN
        frmNewRA1.BusinessName = msBusinessName
        frmNewRA1.BusinessAddress1 = msBusinessAddress1
        frmNewRA1.BusinessAddress2 = msBusinessAddress2

        '===frmNewJob2.LicenceOk = mbLicenceOk
        frmNewRA1.StaffId = mIntStaff_Id
        frmNewRA1.StaffName = msStaffName
        frmNewRA1.ShowDialog()
        '=3101=  VB6.ShowForm(frmNewRA1, VB6.FormShowConstants.Modal, Me) '==vbModeless, Me '--vbModal
        '==Unload frmNewRA
        Call mBrowseRAs1.refresh()
        Call mbRefreshRAsTreeView(mTvwRAs)

        mlStaffTimeout = 0 '--start timing out..--

    End Sub '--create-
    '= = = = = = = = = = = = =

    '--  new RA command.-

    Public Sub cmdNewRA_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) '= Handles cmdNewRA.Click

        Call mnuCreateNewRA_Click()
    End Sub '--new-
    '= = = = = = = = = = = =
    '-===FF->

    '-- Update RA --
    '-- Update RA --
    '--  ACTUAL RA updateCOMMAND.--
    '--  ACTUAL RA updateCOMMAND.--
    '==   v3.4.3403.0711 -- 11Jul2017= - FIX UP for release.
    '==         -- Fix RA's main problem with ViewRA showing the wrong RA..-

    Public Sub cmdViewRecordRAs_Click(ByVal eventSender As System.Object, _
                                          ByVal eventArgs As System.EventArgs)  '= Handles cmdViewRecordRAs.Click
        Dim lRow, lCol As Integer
        Dim colRowValues As Collection
        Dim colKeys As Collection
        Dim nodeX As System.Windows.Forms.TreeNode
        Dim sKey As String
        Dim lngRAId As Integer
        Dim frmNewRA1 As frmNewRA

        '-- Get current selected Row.--
        '== lRow = MSHFlexGridRAs.Row
        '== lCol = MSHFlexGridRAs.Col
        If (msStaffName = "") Then Exit Sub '--was signed off..-
        mlStaffTimeout = -1 '--SUSPEND timing out..--
        lngRAId = -1
        If mFrameBrowseRAs.Visible Then
            If (mDataGridViewRAs.SelectedRows.Count > 0) Then
                '--  use 1st selected row only.
                lRow = mDataGridViewRAs.SelectedRows(0).Cells(0).RowIndex
                If lRow >= 0 Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRowRAs = lRow
                    '--  call Maint to service job.--
                    Call mBrowseRAs1.SelectRecord(mLngSelectedRowRAs, colKeys, colRowValues)
                    If colKeys Is Nothing Then
                        MsgBox("Nothing selected.", MsgBoxStyle.Exclamation)
                    Else '--ok
                        '--  get selected record key..--
                        '--Set colKeys = frmBrowse.selectedKey
                        If colKeys.Count() <= 0 Then
                            MsgBox("Selection is empty..", MsgBoxStyle.Information)
                        Else
                            '-- Load RA form..-.--
                            lngRAId = CInt(colRowValues.Item("RA_Id")("value"))
                        End If '--empty-
                    End If '--nothing..--
                End If '--lRow.-
            End If  '--sel count.-
        ElseIf (LCase(mTabControlRAs.SelectedTab.Name) = "tabpagerassuppliers") Then
            '-3403.711- showing suppliers and Supplier RA's.
            '-- Check if any RA is selected in ListViewRA's for selected supplier.
            Dim listItems As ListView.SelectedListViewItemCollection = mListViewSupplierRAs.SelectedItems
            If (listItems.Count > 0) Then  '--have selection..-
                Dim item1 As ListViewItem = listItems(0)   '--first selected.-
                '=Dim intRA_id As Integer = CInt(item1.Text)
                If IsNumeric(item1.Text) Then
                    'Dim intRA_id As Integer = CInt(item1.Text)
                    'Call mbShowRAInfo(intRA_id)
                    lngRAId = CInt(item1.Text)
                End If
            End If  '-count-
        Else '--must be Tree.  Get current node..-.--
            '--get current node..-
            nodeX = mTvwRAs.SelectedNode
            If nodeX Is Nothing Then
                Beep()
            Else
                sKey = LCase(nodeX.Name)
                If VB.Left(sKey, 3) = "ra-" Then
                    lngRAId = CInt(Mid(sKey, 4)) '--bypass "ra-" --
                End If '--key/ra..-
            End If '--nothing.-
        End If '--tree..-

        If (lngRAId > 0) Then '--something..--
            frmNewRA1 = New frmNewRA

            '==frmNewRA.connectionJet = mCnnJet
            frmNewRA1.connectionSql = mCnnSql
            '==3357.0206=
            frmNewRA1.server = msServer
            frmNewRA1.dbName = msSqlDbName

            frmNewRA1.V20_Only = False '= mbV20_Only  '--if no jobless RAs.--

            frmNewRA1.dbInfoSql = mColSqlDBInfo
            '==        frmNewRA.dbInfoJet = mColJetDBInfo
            frmNewRA1.retailHost = mRetailHost1
            frmNewRA1.LocalSettingsPath = msLocalRAsSettingsPath() '= gsLocalJobsSettingsPath() 
            '======= If Not mbLicenceOk Then MsgBox "Product does not have a valid Licence..", vbExclamation
            frmNewRA1.BusinessABN = msBusinessABN
            frmNewRA1.BusinessName = msBusinessName
            frmNewRA1.BusinessAddress1 = msBusinessAddress1
            frmNewRA1.BusinessAddress2 = msBusinessAddress2
            '===frmNewJob2.LicenceOk = mbLicenceOk
            frmNewRA1.StaffId = mIntStaff_Id
            frmNewRA1.StaffName = msStaffName
            frmNewRA1.RA_Id = lngRAId '--updating this record..-
            frmNewRA1.ShowDialog()

            '=3101=  VB6.ShowForm(frmNewRA1, VB6.FormShowConstants.Modal, Me) '== vbModeless, Me  '-- vbModal
            '===Unload frmNewRA
            Call mBrowseRAs1.refresh()
            Call mbRefreshRAsTreeView(mTvwRAs)
        End If '--id..--
        mlStaffTimeout = 0 '--NOW is timing out..--
    End Sub '--viewRecord--
    '= = = = = = = = = = = =
    '-===FF->

    '-- 3119.1217- Show Attachments --
    '-- 3119.1217- Show Attachments --
    '-- 3119.1217- Show Attachments --

    '== 3301.221-  TEMP bypass Attachments.-

    Public Sub btnRA_Attachments_Click(sender As Object, _
                                      ev As EventArgs)  '= Handles btnAttachments.Click
        Dim frmDocs1 As frmAttachments

        If (mlRAId > 0) Then '-have RA..-
            mlStaffTimeout = -1 '--SUSPEND timing out..--
            frmDocs1 = New frmAttachments(mFrmParent, mCnnSql, msSqlDbName, mColSqlDBInfo, _
                                         "RA", mlRAId, mTxtRASupplier.Text, mIntStaff_Id, msStaffName, msJobMatixVersion)
            frmDocs1.ShowDialog()
            mlStaffTimeout = 0 '--NOW is timing out..--
        End If  '-id-

    End Sub  '-btnAttachments_Click--
    '= = = = = = = = = = = = = = =  =





End Class  '-clsRAsMain33-
'= = = = = = = = = = = = = = =
