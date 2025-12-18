Option Strict Off
Option Explicit On
Imports System.Data
Imports System.Data.OleDb

Friend Class frmModelEdit
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefLng A-Z statement was removed. Variables were explicitly declared as type Integer. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = = = = = =
	
	'-- EDIT Service Models ( son of  Edit Flex Grid..)--
	
	'--  created 23-April-2011=  - For JobMatix V2.2== with Multiple Service Models..-
	'--  created 19-June-2011=  - re-do using mBrowse class for stock...-
	
    '== grh- 12-Nov-2011== now USING clsRetailHost--

    '== grh- 08-Dec-2011== now VB.NET version..--
    '== grh- 27-Feb-2012== Build 3031==  Use Retail-Host StockTableColumnNameCat1/2 ..--
    '---                                 to build stock browser WHERE clause.. 
    '===   ALSO.  Drop MSHFlexGrid..  now using .Net  DataGridView..
    '==                 BOTH for Stock Browser AND for Checklist..
    '==
    '== grh- 07-Jun-2012== Build 3061.0== 
    '==    Re-vamp to show checklists as Service-Stock is browsed..
    '==
    '== grh- 26-Jul-2012== Build 3067.0== 
    '==    >> Add HelpProvider..
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '== 
    '==  grh. JobMatix 3.1.3101 ---  18-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider is needed for Jet OleDb driver).
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =

    '= = = = = = = = = =
    '-===FF->

	Const K_FINDACTIVEBG As Integer = &HC0FFFF
    Const K_FINDINACTIVEBG As Integer = &HC0C0C0 '--grey.--

    '== This is not actually refernced..
    '--  Max size is set at design time..
    '--  To suit ServiceModelChecklist  Descr column width..-
    Const K_MAX_TASK_TEXTSIZE As Integer = 80
	
    Private mbIsInitialising As Boolean = False '-- set on/of by form-init-component..

    Private mbActive As Boolean = False  '-- stops activate being re-entered..-
    '== Private mbAmending As Boolean '-- amend existing  job...-
    Private mbChecklistLoading As Boolean = False '-- set on/of by form-init-component..

    Private mCnnJobs As OleDbConnection  '== ADODB.Connection '--SQL jobs connection --
	Private mColSqlDBInfo As Collection
	
	'== Private mCnnJet  As ADODB.connection    '--  Retail Manager Jet connection..--
	'== Private mColJetDBInfo As Collection
	
	Private mColFields As Collection '-- stock record for caller..--
	
    Private msServiceChargeCat1 As String = "SERVCE" '--our default.-
    Private msServiceChargeCat2 As String = ""

    '- 3031--
    Private msStockTableColumnNameCat1 As String = ""
    Private msStockTableColumnNameCat2 As String = ""

    Private mbCancelled As Boolean = False
    Private mbServiceCharge As Boolean = False
	
	Private mColItemFields As Collection '-serial no. lookup..-
	Private msSerialNo As String
	
    Private msLogPath As String = ""
    Private mbIsSqlAdmin As Boolean = False
	
    Private mbGridDataChanged As Boolean = False
    Private mlStock_Id As Integer = -1 '--current item.-
	
	Private mBrowse1 As clsBrowse3 '== clsBrowse22
    '== Private mColPrefs As Collection
    Private mLngSelectedRow As Integer = -1
	
	Private mRetailHost1 As _clsRetailHost
	'= = = = = = = = = = = = = = = = = =
	
	'--  Input Properties..-
	'--  Input Properties..-
	
    WriteOnly Property connectionSql() As OleDbConnection  '== ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property '--cnn sql..--
	'= = = = = = =  = = = = =
	WriteOnly Property dbInfoSql() As Collection
		Set(ByVal Value As Collection)
			
			mColSqlDBInfo = Value
			
		End Set
	End Property '--info sql jobs..--
	'= = = = = = =  = = = = =
	
	'== Property Let connectionJet(cnn1 As ADODB.connection)
	'==   Set mCnnJet = cnn1
	'== End Property  '--cnn jet..--
	'= = = = = = =  = = = = =
	'== Property Let dbInfoJet(dbinfo As Collection)
	'==   Set mColJetDBInfo = dbinfo
	'== End Property  '--info jet..--
	'= = = = = = = = = = = = = = =
	
	WriteOnly Property retailHost() As _clsRetailHost
		Set(ByVal Value As _clsRetailHost)
			
			mRetailHost1 = Value
		End Set
	End Property '-host..-
	'= = = = = = = = = = = = = = = = =
	
	
	'= = = = = = = = = = = =
	
	WriteOnly Property ServiceChargeCat1() As String
		Set(ByVal Value As String)
			msServiceChargeCat1 = Value
		End Set
	End Property
	'= = = = = = =
	WriteOnly Property ServiceChargeCat2() As String
		Set(ByVal Value As String)
			msServiceChargeCat2 = Value
		End Set
	End Property
	'= = = = = = =
	
	WriteOnly Property logPath() As String
		Set(ByVal Value As String)
			msLogPath = Value
		End Set
	End Property
	'= = = = = = = = = = = =
	
	'-- results..--
	
	ReadOnly Property cancelled() As Boolean
		Get
			
			cancelled = mbCancelled
		End Get
	End Property '--cancelled--
	'= = = = = = = = = = =  ==
	'-===FF->
	
	'--convert numeric data for sorted display..-
	
	Private Function msFormat(ByVal v1 As Object, ByVal vType As Object, ByVal lSize As Integer) As String
        '== Dim sResult As String
        '== Dim sType As String '--sql type--

        msFormat = gsFormat(v1, vType, lSize)
        '== sResult = CStr(v1) '--for strings..-
        '==  sType = UCase(gsGetSqlType(vType, lSize))
        '== If (sType = "MONEY") Or (sType = "SAMLLMONEY") Then '--currency..-
        '== sResult = New String(" ", 9)
        '== sResult = RSet(FormatCurrency(v1, 2), Len(sResult))
        '== ElseIf gbIsNumericType(sType) Then 
        '== sResult = New String(" ", 5)
        '== sResult = RSet(VB6.Format(v1, "####0"), Len(sResult))
        '== ElseIf gbIsDate(sType) Then
        '== sResult = VB6.Format(CDate(v1), "yyyy-mm-dd")
        '== End If
        '== msFormat = sResult
    End Function '--convert--
	'= = = = = = = = = = = =
	
    '== Private Function mbNumberGridRows(ByRef fg1 As AxMSHierarchicalFlexGridLib.AxMSHFlexGrid) As Boolean
    '== Dim ix As Short
    '== 	For ix = fg1.FixedRows To fg1.Rows - 1
    '== 		fg1.set_TextArray(miFgi(ix, 0), ix)
    '== 	Next 

    '== End Function '--number.-
    '= = = = = = = = = = = = 

    '--  show Grid row numbers..-

    Private Function mbNumberGridRows(ByRef dgv1 As DataGridView) As Boolean
        Dim rx As Integer

        If dgv1.RowCount > 0 Then
            For rx = 0 To (dgv1.RowCount - 1)
                dgv1.Rows(rx).HeaderCell.Value = (rx + 1).ToString  '== CStr(rx + 1)
            Next rx
        End If

    End Function  '-- NumberGridRows --
    '= = = = = = = = = = 
    '= = = = = = = = = = = =
    '-===FF->

    '-- lookup  RM table..--
    '-- lookup  RM table..--
    '== Private Function mbRMLookup(sSql As String, _
    ''==                         colFields As Collection) As Boolean
    '==       Dim colFld As Collection  '--"name"=, "value"-
    '==       Dim fld1 As ADODB.Field
    '==       Dim s1 As String
    '==       Dim sName As String
    '==       Dim rs1 As ADODB.Recordset

    '==   mbRMLookup = False

    '==   '--sSql = "Select * from [staff] WHERE staff_id=" + CStr(lngStaffId)
    '==   Screen.MousePointer = vbHourglass
    '==   If Not gbGetRst(mCnnJet, rs1, sSql) Then
    '==           MsgBox "Failed to get recordset for SQL:" + vbCrLf + sSql + vbCrLf, vbExclamation
    '==           Screen.MousePointer = vbDefault
    '==           Exit Function
    '==   End If
    '==      '--txtMessages.Text = ""
    '==   If Not (rs1 Is Nothing) Then
    '==          If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst
    '==          If (Not rs1.EOF) Then   '---And (cx < 100)
    '==             '--return complete row..-
    '==             Set colFields = New Collection
    '==             For Each fld1 In rs1.Fields
    '==                Set colFld = New Collection
    '==                colFld.Add LCase(fld1.Name), "name"
    '==                If Not IsNull(fld1.Value) Then
    '==                   colFld.Add fld1.Value, "value"
    '==                Else  '--null-
    '==                   colFld.Add "", "value"
    '==                End If
    '==                colFields.Add colFld, LCase(fld1.Name)
    '==             Next fld1
    '==             mbRMLookup = True
    '==          Else  '--not found-
    '==          End If  '-eof-
    '==   End If  '--rs-
    '==   Set rs1 = Nothing

    '==   Screen.MousePointer = vbDefault '--in case Browser failed--

    '== End Function '--RM lookup..--
    '= = = = = = = = = = = = =
    '-===FF->

    '-- lookup RM Stock to get record given long ID..--
    '-- lookup RM Stock to get record given long ID..--

    Private Function mbLookupStockId(ByRef lngStockId As Integer, _
                                      ByRef colRecord As Collection) As Boolean
  
        mbLookupStockId = False
        If mRetailHost1.stockGetStockRecord("", lngStockId, colRecord) Then
            mbLookupStockId = True
        Else
            MsgBox("Failed to get Stock recordset..", MsgBoxStyle.Exclamation)
        End If
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
    End Function '--get stock-
    '= = = = = = =  =  = =
    '-===FF->

    '-save model..-
    '-save model..-

    '== Private Function mbSaveServiceModel(ByRef fgCheckList As AxMSHierarchicalFlexGridLib.AxMSHFlexGrid, _
    '==                                      ByRef Stock_id As Integer) As Boolean

    Private Function mbSaveServiceModel(ByRef dgv1 As DataGridView, _
                                         ByRef Stock_id As Integer) As Boolean
        Dim sSql As String
        Dim sContent, sInserts As String
        Dim sDescr, sErrors As String
        Dim ix As Short
        Dim lngAffected As Integer
        Dim sqlTran1 As OleDbTransaction
        Dim bIsTransaction As Boolean = True

        mbSaveServiceModel = False
        '--  don't save an all-blank list..-- ???  --
        sContent = ""
        '--  replace all items for this stock_id with new list..--
        sSql = " DELETE FROM [ServiceModelCheckLists] WHERE " & "(ModelCheckList_RMStockId=" & CStr(Stock_id) & "); " & vbCrLf
        sInserts = ""
        '==FlexGrid== For ix = 1 To fgCheckList.Rows - 1
        For ix = 0 To dgv1.RowCount - 1
            '==FlexGrid== sDescr = Trim(fgCheckList.get_TextArray(miFgi(ix, 1))) '-- description..
            sDescr = Trim(dgv1.Rows(ix).Cells(0).Value) '-- description..

            If (sDescr = "--") Then sDescr = ""
            If sDescr <> "" Then '-- drop blank lines..--
                sContent = sContent & sDescr
                sInserts = sInserts & "INSERT INTO [ServiceModelCheckLists] " & _
                                                              " (ModelCheckList_RMStockId, ModelCheckListTaskDescription ) "
                sInserts = sInserts & " VALUES ( " & CStr(Stock_id) & ", '" & gsFixSqlStr(sDescr) & "' );" & vbCrLf
            End If '-blank.-
        Next ix

        If (sContent <> "") Then
            sSql = sSql & sInserts
        Else
            MsgBox("Note: Old model will be deleted, but blank model won't be saved..", MsgBoxStyle.Information)
        End If
        '--  Apply DELETE and INSERTS..--
        '== mCnnJobs.BeginTrans()
        sqlTran1 = mCnnJobs.BeginTransaction

        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbExecuteSql(mCnnJobs, sSql, bIsTransaction, sqlTran1, lngAffected, sErrors) Then
            '==If Not gbExecuteCmd(mCnnJobs, sSql, lngaffected, sErrors) Then
            '=mCnnJobs.RollbackTrans() '-- DONE-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            MsgBox("Failed to DELETE/INSERT DB Checklist items.." & vbCrLf & _
                          sErrors & vbCrLf, MsgBoxStyle.Exclamation)
            Exit Function
        Else '--ok--
            '= mCnnJobs.CommitTrans()
            sqlTran1.Commit()
            If gbDebug Then MsgBox("Checklist record deletes/inserts completed ok..", MsgBoxStyle.Information)
        End If '--exec.-
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Function '-save model..-
    '= = = = = = = = = = = =
    '-===FF->

    '--  GRID VERSION- Retrieve model for current stock item..--

    Private Function mbLoadServiceModelEx(ByRef colRecord As Collection) As Boolean
        '==Dim item1 As ListItem
        Dim lngMinGrid, lngRows, rx As Integer
        Dim lngErr, scrx, lngCount As Integer
        Dim sCat1, sCat2 As String
        Dim sDescr As String
        Dim sBarcode, sSell As String
        Dim sSql As String
        Dim rs1 As DataTable '= ADODB.Recordset
        Dim row1 As DataGridViewRow

        mbLoadServiceModelEx = False
        '-- show current item, and go to edit..
        lngMinGrid = 6
        '==FlexGrid== fgCheckList.Clear()
        mbChecklistLoading = True
        dgvChecklist.Rows.Clear()

        If (colRecord Is Nothing) Then
            MsgBox("Nothing selected", MsgBoxStyle.Exclamation)
        Else '-ok-
            sCat1 = colRecord.Item("cat1")("value") '-- item1.Text       '-- ListTasks.List(idx)
            sCat2 = colRecord.Item("cat2")("value") '--item1.SubItems(1)      '-- ListTasks.List(idx)
            sDescr = colRecord.Item("description")("value") '-- item1.SubItems(2)      '-- ListTasks.List(idx)
            sBarcode = colRecord.Item("barcode")("value") '-- item1.SubItems(4)
            sSell = FormatCurrency(CDec(colRecord.Item("sell")("value")), 2) '-- item1.SubItems(5)
            scrx = colRecord.Item("stock_id")("value") '-- CLng(item1.Tag)    '--get sstock id...-
            If (scrx > 0) Then
                LabDescription(0).Enabled = True
                LabDescription(1).Enabled = True
                LabDescription(0).Text = "Stock_id:  " & scrx & vbCrLf & "Prod. Barcode: " & sBarcode

                LabDescription(1).Text = sDescr & vbCrLf & "Sell Price:  " & sSell & " (ex gst.)"
                '== 3061.0 ==  dgvChecklist.Enabled = True    '== fgCheckList.Enabled = True

                mlStock_Id = scrx
                '--  Retrieve model (if any) for this stock id--
                '--   and load grid.--
                lngCount = 0
                sSql = "Select * FROM [ServiceModelCheckLists] WHERE (ModelCheckList_RMStockId= " & CStr(mlStock_Id) & "); "
                If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
                    MsgBox("Failed to get ModelCheckList recordset.." & vbCrLf, MsgBoxStyle.Exclamation)
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                    mbChecklistLoading = False
                    Exit Function
                Else '--build checklist ..-
                    If Not (rs1 Is Nothing) Then
                        If rs1.Rows.Count <= 0 Then  '== (rs1.BOF And rs1.EOF) Then '--empty.. no model..-
                            '--no model defined..-
                            '--else  If no model..  just make  blank data rows..
                            lngRows = lngMinGrid + 1
                            For rx = 1 To lngRows
                                row1 = New DataGridViewRow
                                dgvChecklist.Rows.Add(row1)
                            Next rx
                        Else '--have some.-
                            '== rs1.MoveLast()
                            '== rs1.MoveFirst()
                            '-- load fgCheckList FLEXGRID from rs1..--
                            If rs1.Rows.Count >= lngMinGrid Then
                                '== fgCheckList.Rows = rs1.RecordCount + 1
                                lngRows = rs1.Rows.Count + 1
                            Else
                                '==fgCheckList.Rows = lngMinGrid + 1 '--show a complete grid..-
                                lngRows = lngMinGrid + 1 '--show a complete grid..- 
                            End If
                            '==fgCheckList.FixedRows = 1
                            For rx = 1 To lngRows
                                row1 = New DataGridViewRow
                                dgvChecklist.Rows.Add(row1)
                            Next rx
                            For Each dataRow1 As DataRow In rs1.Rows
                                '--add to list for job..
                                '-load item.-
                                dgvChecklist.Rows(lngCount).Cells(0).Value = Trim(dataRow1.Item("ModelCheckListTaskDescription").Value)
                                lngCount = lngCount + 1
                            Next dataRow1
                            '== While (Not rs1.EOF) '---And (cx < 100)
                            '==  rs1.MoveNext()
                            '= End While '-eof-
                        End If '--empty..-
                        '== rs1.Close()
                    End If '--rs nothing-
                End If '--get rs-
                mbLoadServiceModelEx = True '--found model or blank new model..-
                '== 3061.0 ==  dgvChecklist.Enabled = True
                '-- If last row is not empty, then add 1 empty row at the end..-

                '--  SHOULD NOT BE EMPTY..==
                '==FlexGrid== If (Trim(fgCheckList.get_TextArray(miFgi(lngCount, 1))) <> "") And _
                '==FlexGrid==                (Trim(fgCheckList.get_TextArray(miFgi(lngCount, 1))) <> "--") Then
                '==FlexGrid== fgCheckList.AddItem("" & Chr(9) & "", lngCount + 1)
                '==FlexGrid== End If

                Call mbNumberGridRows(dgvChecklist)
                mbGridDataChanged = False
            End If '--scrx-
            System.Windows.Forms.Application.DoEvents()
        End If '--item1 nothing..
        mbChecklistLoading = False
        '==End If  '--error..-
    End Function '-load model..-
    '= = = = = = = = = = = =
    '-===FF->

    '--  GOT Function KEY..
    '--- check for F2 for STOCK Lookup--
    '-- JobMatix---- USE built-in browse object..--

    Private Function mbInitialiseBrowse() As Boolean

        Dim cx, j, i, k, fx As Integer
        Dim lngId As Integer
        Dim s1, s2 As String
        '--Dim lngActualSize As Long
        Dim lngControl As Integer
        Dim AltDown, ShiftDown, CtrlDown As Integer
        Dim colPrefs As Collection
        Dim colSelectedRow As Collection
        Dim sBarcode As String
        Dim colRecord As Collection '--full cust record..-
        Dim sHostTablename As String
        Dim sWhere As String

        '-- show full frame..-
        '== 3061.0 ==  frame doesn't change--
        '== FrameBrowse.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(dgvChecklist.Top) - _
        '==                                                              VB6.PixelsToTwipsY(FrameBrowse.Top) - 60)
        '== DataGridView1.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(FrameBrowse.Height) - _
        '==                                             VB6.PixelsToTwipsY(DataGridView1.Top) - 120)

        mBrowse1.connection = mRetailHost1.connection
        mBrowse1.colTables = mRetailHost1.colTables
        mBrowse1.IsSqlServer = mRetailHost1.IsSqlServer
        mBrowse1.DBname = mRetailHost1.DBname

        '--  get table/prefs info for this host..--
        If Not mRetailHost1.browseGetPrefColumns("stock", sHostTablename, colPrefs) Then
            MsgBox("Can't translate table name to host table..", MsgBoxStyle.Exclamation)
        End If
        mBrowse1.tableName = sHostTablename

        '= mBrowse1.FlexGrid = MSHFlexGrid1
        mBrowse1.DataGrid = DataGridView1

        '==FlexGrid== mBrowse1.ArrowUp = PicArrowUp
        '==FlexGrid== mBrowse1.ArrowDown = PicArrowDown

        '--  pass controls..--
        mBrowse1.showRecCount = labRecCount '--updates rec. retrieval..
        mBrowse1.showFind = LabFind '--updates Sort Column display..
        mBrowse1.showTextFind = txtFind '--updates Sort Column display..
        sWhere = ""
        If (msStockTableColumnNameCat1 <> "") AndAlso (msServiceChargeCat1 <> "") Then
            sWhere = " (" & msStockTableColumnNameCat1 & "='" & msServiceChargeCat1 & "') "
            If (msStockTableColumnNameCat2 <> "") AndAlso (msServiceChargeCat2 <> "") Then
                sWhere = sWhere & " AND (" & msStockTableColumnNameCat2 & "='" & msServiceChargeCat2 & "') "
            End If
        End If
        mBrowse1.WhereCondition = sWhere '--" (LEFT(jobStatus,2)<='30')"  '--not completed..--
        mBrowse1.PreferredColumns = colPrefs '== mColPrefs
        mBrowse1.ShowPreferredColumnsOnly = True '==  ShowPreferredColumnsOnly
        FrameBrowse.Enabled = True

        mLngSelectedRow = -1

        mBrowse1.Activate() '-- go..--

        '==3061.0=  txtFind.Focus()
        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(0)
            DataGridView1.Select()
        Else
            MsgBox("No service items found..", MsgBoxStyle.Exclamation)
            Me.Close()
            Exit Function
        End If
    End Function '--init-
    '= = = =  = =  = =
    '-===FF->

    '-- L o a d --
    '-- L o a d --

    Sub frmModelEdit_Load(ByVal eventSender As System.Object, _
                             ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Dim i As Short
        Dim s1 As String
        '== mbActive = False
        '= mbIsSqlAdmin = False
        '== msLogPath = ""

           '-- Label rows and columns.
        '--  row nos..--
        '== Call mbNumberGridRows(fgCheckList)

        '== Call mbNumberGridRows(dgvChecklist)

        '==FlexGrid== fgCheckList.set_TextArray(miFgi(0, 1), "Task Name/Description..")

        '== msServiceChargeCat1 = "SERVCE" '--our default.-
        '== msServiceChargeCat2 = ""
        With DataGridView1.RowTemplate
            .DefaultCellStyle.BackColor = Color.Gainsboro
            .Height = 21
            .MinimumHeight = 20
        End With
        DataGridView1.ColumnHeadersHeight = 18

        With dgvChecklist.RowTemplate
            '== .DefaultCellStyle.BackColor = Color.Bisque
            .Height = 21
            .MinimumHeight = 20
        End With
        dgvChecklist.Enabled = False

        mbGridDataChanged = False
        cmdSave.Enabled = False
        cmdInsert.Enabled = False
        cmdDelete.Enabled = False
        cmdLookup.Enabled = False
        LabEdit.Enabled = False

        mlStock_Id = -1
        '-- Initialize edit box (so it loads now).

        LabExplain.Text = "Service Checklists:  Any Retail-Host Service Item can have an associated Task-Checklist for JobTracking.." & _
                            "  A copy of the Model Checklist will be attached to any Job that includes such a Service Item.." & _
                            vbCrLf & vbCrLf & " -- Choose a Service Stock Item from the Retail-Host stock list," & _
                                        " then edit and Save the Service Model Checklist in the data grid beneath.."
  
        '===FrameProduct.Top = FrameBrowse.Top
        '====FrameProduct.Left = FrameBrowse.Left

        LabFind.Text = ""
        labRecCount.Text = ""
        FrameBrowse.Text = ""  '== "Browsing Service items (RM Stock Table).."
        labFrameBrowse.Text = "Browsing Service items (RM Stock Table).."
        '===FrameProduct.Caption = ""
        frameModelEdit.Text = ""
        '==cmdLookup.Top = FrameProduct.Top
        labEditHelp.Text = "Double-Click on selected cell, or press F2 to edit cell data.."
        labEditHelp.Visible = False

        '== 3067.0 ==
        s1 = gsGetHelpFileName()
        If (s1 <> "") Then
            HelpProvider1.HelpNamespace = s1
            HelpProvider1.SetHelpNavigator(Me, HelpNavigator.Topic)
            HelpProvider1.SetHelpKeyword(Me, "JT3-ModelEdit3.htm")
        End If

        Call CenterForm(Me)

    End Sub '--load-
    '= = = = = = = = =

    '-- A c t i v a t e --
    '-- A c t i v a t e --

    'UPGRADE_WARNING: Form event frmModelEdit.Activate has a new behavior. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'

    Sub frmModelEdit_Activated(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles MyBase.Activated
        Dim idx, lngCount As Integer
        Dim bUpgradeNeeded As Boolean
        Dim col1 As Collection
        Dim sMsg As String
        Dim s1 As String
        Dim item1 As System.Windows.Forms.ListViewItem

        If mbActive Then Exit Sub
        mBrowse1 = New clsBrowse3 '== clsBrowse22

        Me.Text = "Editing Model Checklists.."
        LabHdr.Text = "JobMatix-" & vbCrLf & " Service Model Checklists"

        '===LabDescription.Caption = IIf(mbIsSqlAdmin, "admin. user.", "Normal user..")
        System.Windows.Forms.Application.DoEvents()

        msStockTableColumnNameCat1 = mRetailHost1.StockTableColumnNameCat1
        msStockTableColumnNameCat2 = mRetailHost1.StockTableColumnNameCat2

        s1 = "Note- Service items are identified by Retail-Host Stock categories: " & vbCrLf & _
                                                      "   Cat1= '" & msServiceChargeCat1 & "';  and Cat2="
        If (msStockTableColumnNameCat2 = "") Or (msServiceChargeCat2 = "") Then
            s1 = s1 & " (any).."
        Else
            s1 = s1 & " '" & msServiceChargeCat2 & "'.."
        End If
        LabDefinition.Text = s1

        '== fgCheckList.Enabled = False
        dgvChecklist.Enabled = False

        mbActive = True

        Call mbInitialiseBrowse()

    End Sub '--Activate..--
    '= = = = = = = = = = =

    '-- Lookup Stock..--
    '-- Lookup Stock..--
    '--  give up any model being edited..-

    Private Sub cmdLookup_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdLookup.Click
        Dim lRow As Integer

        If cmdSave.Enabled Then '--  data not saved..--
            '--ask if they want to abandon changes to current grid..--
            If mbGridDataChanged Then
                If Not MsgBox("Abandon changes to model:" & vbCrLf & vbCrLf & LabDescription(1).Text & vbCrLf, _
                             MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    '== fgCheckList.Focus() '--no.. go back to grid..-
                    dgvChecklist.Focus() '--no.. go back to grid..-

                    Exit Sub
                End If
            End If
        End If
        '--  give up any model being edited..-

        LabEdit.Enabled = False
        labEditHelp.Visible = False

        '--  clear grid
        '==LabStockHdr.Enabled = True
        cmdSave.Enabled = False
        cmdInsert.Enabled = False
        cmdDelete.Enabled = False

        '===LabDescription.Caption = ""
        '==3061.0 = LabDescription(0).Enabled = False
        '==3061.0 = LabDescription(1).Enabled = False
        '--  clear Grid..-
        '==3061.0 = dgvChecklist.Rows.Clear()

        dgvChecklist.Enabled = False

        '==FrameProduct.Visible = False
        frameModelEdit.Enabled = False
        FrameBrowse.Visible = True
        FrameBrowse.Enabled = True
        '= FrameBrowse.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(dgvChecklist.Top) - VB6.PixelsToTwipsY(FrameBrowse.Top) - 60)
        '== DataGridView1.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(FrameBrowse.Height) - VB6.PixelsToTwipsY(DataGridView1.Top) - 120)

        '==3061.0 = mBrowse1.refresh()
        '==3061.0 = txtFind.Focus()
        cmdLookup.Enabled = False
        If Not DataGridView1.CurrentCell Is Nothing Then
            lRow = DataGridView1.CurrentCell.RowIndex
            If (lRow >= 0) Then '--ok--
                mLngSelectedRow = lRow
                '--  get stock id and show checklist.--
                Call mbSelectEditItem(lRow, False)
            End If '--row--
        End If
        DataGridView1.Select()
        System.Windows.Forms.Application.DoEvents()
    End Sub '-- Lookup..--
    '= = = = = = = = = = = = = = =
    '-===FF->

    '--Edit current item.--
    '--Edit current item.--

    '==== Private Sub cmdEdit_Click()

    Private Function mbStartEdit() As Integer

        FrameBrowse.Enabled = False
        frameModelEdit.Enabled = True
        '==3061.0=FrameBrowse.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(LabDescription(0).Top) _
        '=    - VB6.PixelsToTwipsY(FrameBrowse.Top) - 360)

        dgvChecklist.Enabled = True
        cmdInsert.Enabled = True
        cmdLookup.Enabled = True
        cmdLookup.Text = "Cancel"
        ToolTip1.SetToolTip(cmdLookup, "Cancel editing for this Checklist.")

        '====LabStockHdr.Enabled = False
        LabEdit.Enabled = True
        labEditHelp.Visible = True

        '== fgCheckList.Enabled = True
        '== fgCheckList.Focus()
        dgvChecklist.Enabled = True
        If dgvChecklist.RowCount > 0 Then
            cmdDelete.Enabled = True
            '= dgvChecklist.CurrentRow 
        End If

        dgvChecklist.Focus()

    End Function '--edit--
    '= = = = = = = = = = =

    '-- get stock grid row and start checklist edit..

    Private Function mbSelectEditItem(ByVal lngRowNo As Integer, _
                                       Optional ByVal bStartEdit As Boolean = True) As Boolean
        Dim lCol, lngId As Integer
        Dim sBarcode As String
        Dim colSelectedRow As Collection
        Dim colRecord As Collection
        Dim colKeys As Collection

        Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colSelectedRow)

        If colSelectedRow.Count() > 0 Then
            sBarcode = colSelectedRow.Item("barcode")("value")
            lngId = CInt(colSelectedRow.Item("stock_id")("value"))
            If Not mbLookupStockId(lngId, colRecord) Then
                MsgBox("Failed to retrieve stock record (Id " & lngId & ") " & vbCrLf & _
                                             " for Barcode: '" & sBarcode & "'..", MsgBoxStyle.Exclamation)
            Else '--ok--
                '--set up stock details.-
                If mbLoadServiceModelEx(colRecord) Then
                    '-- show half frame..-
                    If bStartEdit Then Call mbStartEdit()
                End If
            End If
        Else
            If gbDebug Then MsgBox("No selection made..", MsgBoxStyle.Information)
        End If '--got row..--
    End Function  '--select..--
    '= = = = = = = = = =  = == 

    Private Sub cmdEdit_Click(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles cmdEdit.Click
        Dim lngRowNo As Integer

        '==Call MSHFlexgrid1_dblClick(MSHFlexGrid1, New System.EventArgs())
        If DataGridView1.SelectedRows.Count > 0 Then
            '--  use 1st selected row only.
            lngRowNo = DataGridView1.SelectedRows(0).Cells(0).RowIndex
            If (lngRowNo >= 0) Then '--ok row--
                '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                mLngSelectedRow = lngRowNo
                Call mbSelectEditItem(lngRowNo)
            End If  '--row-
        End If  '--count.-

    End Sub
    '= = = = = = = = = = = =
    '-===FF->

    '--BROWSING STOCK.. --

    '--  F l e x G r i d  E v e n t s..--
    '--  F l e x G r i d  E v e n t s..--

    '--  Browser MOUSE Events..--
    '--  Browser MOUSE Events..--

    '--mouse activity---  select col-headers--
    '-- set new sort column--
    '==== Private Sub MSHFlexgrid1_MouseUpEvent(ByVal eventSender As System.Object, _
    '====                                ByVal eventArgs As AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_MouseUpEvent)

    '==== Dim lRow, lCol As Integer
    '==== Dim sName As String
    '==== Dim j, i, k As Integer

    '====     If eventArgs.button = 1 Then '--left --
    '====         lRow = MSHFlexGrid1.MouseRow
    '====         lCol = MSHFlexGrid1.MouseCol
    '====         If (lRow > 0) And (MSHFlexGrid1.Rows > 1) Then '--NOT header row--
    '==== '=== STUFFS UP PgUp/Dn SCROLLING  ===  cmdViewRecord.SetFocus
    '====         ElseIf lRow = 0 And (MSHFlexGrid1.Rows > 1) Then  '--in header row--
    '==== '--MsgBox "Left click on col :" & lCol
    '====             sName = Trim(MSHFlexGrid1.get_TextMatrix(0, lCol)) '--get new column name--
    '====             Call mBrowse1.SortColumn(sName)
    '====             txtFind.Focus()
    '====         End If '--row 0--
    '====     End If '--left--
    '==== End Sub '--mouse up--
    '= = = = = = = = = =

    '--  Catch sorted event so we can highlight correct column..--

    Private Sub dataGridView1_Sorted(ByVal sender As Object, _
                                      ByVal e As System.EventArgs) Handles DataGridView1.Sorted

        Dim sName As String
        '-- get new sort column..--

        Dim currentColumn As DataGridViewColumn = DataGridView1.SortedColumn
        '==Dim direction As ListSortDirection
        sName = currentColumn.HeaderText

        Call mBrowse1.SortColumn(sName)

        '==  Me.DataGridView1.FirstDisplayedCell = Me.DataGridView1.CurrentCell

    End Sub
    '= = = = = = = = =  = = =
    '-===FF->

    '-- cell click.--
    '-- cell click.--

    Private Sub DataGridView1_CellMouseClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridView1.CellMouseClick
        Dim lRow, lCol As Integer
        Dim sName As String
        '==Dim i, j, k As Long

        If eventArgs.Button = 1 Then '--left --
            lCol = eventArgs.ColumnIndex
            lRow = eventArgs.RowIndex
            '==If (lRow = 0) And (DataGridView1.Rows.Count > 1) Then  '--hader clicked-
            If (lRow >= 0) And (DataGridView1.Rows.Count > 0) Then  '--selected a row.--
                '== cmdOk.Enabled = True
                mLngSelectedRow = lRow
                '== Call mBrowse1.SelectRecord(mLngSelectedRow, colKeys, colRowValues)
            End If
        End If  '--left..-
    End Sub
    '= = = = = = = = = = = =
    '-===FF->

    '--mouse activity---  
    '-- select row to edit--
    '-- select row to edit--

    '== Private Sub MSHFlexgrid1_dblClick(ByVal eventSender As System.Object, _
    '==                                        ByVal eventArgs As System.EventArgs)
    Private Sub DataGridView1_CellMouseDblClickEvent(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellMouseEventArgs) _
                                                            Handles DataGridView1.CellMouseDoubleClick
        Dim lRow As Integer

        '== lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex

        If (lRow >= 0) Then '--ok--
            mLngSelectedRow = lRow
            '--  get stock id and start edit.--
            Call mbSelectEditItem(lRow)

        End If '--row--
    End Sub '--click--
    '= = = = = = = = = =

    '--  Change selected row..

    Private Sub DataGridView1_SelectionChanged(ByVal eventSender As System.Object, _
                                                  ByVal eventArgs As System.EventArgs) _
                                                        Handles DataGridView1.SelectionChanged
        Dim lRow As Integer
        '== lRow = eventArgs.RowIndex
        If mbIsInitialising Then Exit Sub
        If DataGridView1.CurrentCell Is Nothing Then Exit Sub

        lRow = DataGridView1.CurrentCell.RowIndex

        If (lRow >= 0) Then '--ok--
            mLngSelectedRow = lRow
            '--  get stock id and show checklist.--
            Call mbSelectEditItem(lRow, False)
        End If '--row--
    End Sub '--click--
    '= = = = = = = = = =
    '-===FF->

    '-- STOCK Browser.. txt FIND Activity.--
    '-- STOCK Browser.. txt FIND Activity.--
    '--BROWSING STOCK.. --

    '-- key activity---  select row to edit--
    Private Sub txtFind_KeyPress(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles txtFind.KeyPress
        Dim iKeyAscii As Short = Asc(eventArgs.KeyChar)
        Dim lRow, lCol As Integer
        '= lRow = MSHFlexGrid1.Row
        '= lCol = MSHFlexGrid1.Col

        '--MsgBox "KeyPress <" & iKeyAscii & "> on Row: " & lRow & ", col :" & lCol
        If iKeyAscii = System.Windows.Forms.Keys.Return Then
            If DataGridView1.SelectedRows.Count > 0 Then
                '--  use 1st selected row only.
                lRow = DataGridView1.SelectedRows(0).Cells(0).RowIndex
                If (lRow >= 0) Then '--ok row--
                    '--MsgBox "ENTER key on Row: " & lRow & ", col :" & lCol
                    mLngSelectedRow = lRow
                    '==Call MSHFlexgrid1_KeyPressEvent(MSHFlexGrid1, _
                    '==                New AxMSHierarchicalFlexGridLib.DMSHFlexGridEvents_KeyPressEvent(iKeyAscii))
                    Call mbSelectEditItem(lRow)

                End If '--row--
                iKeyAscii = 0 '--processed--
            End If '--enter--

            eventArgs.KeyChar = Chr(iKeyAscii)
            If iKeyAscii = 0 Then
                eventArgs.Handled = True
            End If
        End If  '--count-
    End Sub '--click--
    '= = = = = = = = = = =
    '-===FF->

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
        Dim lRow As Integer

        If mbIsInitialising Then Exit Sub
        Call mBrowse1.Find(txtFind)  '-go to nearest row..-
        If Not DataGridView1.CurrentCell Is Nothing Then
            lRow = DataGridView1.CurrentCell.RowIndex
            If (lRow >= 0) Then '--ok--
                mLngSelectedRow = lRow
                '--  get stock id and show checklist.--
                Call mbSelectEditItem(lRow, False)
            End If '--row--
        End If
    End Sub '--txtfind_change--
    '= = = = = = = = = = = =
    '-===FF->

    '-- Stock Listview..--  ==  ??? ==

    Private Sub xxxx_listViewService_gotfocus()

        If cmdSave.Enabled Then '--  data not saved..--
            '--ask if they want to abandon changes to current grid..--
            If mbGridDataChanged Then
                If Not MsgBox("Abandon changes to model:" & vbCrLf & vbCrLf & _
                                LabDescription(1).Text & vbCrLf, _
                                  MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    '== fgCheckList.Focus() '--no.. go back to grid..-
                    dgvChecklist.Focus() '--no.. go back to grid..-

                    Exit Sub
                End If
            End If
        End If
        '--  give up any model being edited..-

        LabEdit.Enabled = False
        '--  clear grid
        '==LabStockHdr.Enabled = True
        cmdSave.Enabled = False
        cmdInsert.Enabled = False
        cmdDelete.Enabled = False

        '===LabDescription.Caption = ""
        LabDescription(0).Enabled = False
        LabDescription(1).Enabled = False
        '--  clear Grid..-
        '== fgCheckList.Enabled = False
        dgvChecklist.Enabled = False

        System.Windows.Forms.Application.DoEvents()

    End Sub '--got focus..-
    '= = = = = = = = = = =
    '-===FF->

    '-- C H E C K L I S I -  D a t a G r i d --
    '---         Editing events..--

    '-- cell click.--
    '-- cell click.--

    '== Private Sub dgvChecklist_CellValueChangedEvent(ByVal eventSender As System.Object, _
    '==                                                   ByVal eventArgs As DataGridViewCellEventArgs) _
    '==                                                         Handles dgvChecklist.CellValueChanged
    Private Sub dgvChecklist_CurrentCellDirtyStateChanged(ByVal sender As Object, _
                                                           ByVal eventargs As EventArgs) _
                                                       Handles dgvChecklist.CurrentCellDirtyStateChanged
        Dim lRow, lCol As Integer
        Dim sText As String

        If mbIsInitialising Or mbChecklistLoading Then Exit Sub
        '== lCol = eventargs.ColumnIndex
        lRow = dgvChecklist.CurrentCell.RowIndex   '== eventargs.RowIndex
        If (lRow >= 0) And (dgvChecklist.Rows.Count > 0) Then  '--selected a row.--
            mbGridDataChanged = True
            cmdSave.Enabled = True
            cmdLookup.Enabled = True
            cmdLookup.Text = "Cancel"
            ToolTip1.SetToolTip(cmdLookup, "Cancel editing for this Checklist.")

            sText = dgvChecklist.Rows(lRow).Cells(lCol).Value
            '== MsgBox("New value of row: " & lRow & "  is: " & sText, MsgBoxStyle.Information)
        End If  '--row-
    End Sub '= CellValueChangedEvent--
    '= = = = = = = = = = = = = = = = = 
    '-===FF->

    '-- Grid Command Buttons..--
    '-- Grid Command Buttons..--

    Private Sub cmdSave_Click(ByVal eventSender As System.Object, _
                               ByVal eventArgs As System.EventArgs) Handles cmdSave.Click

        '== Call mbSaveServiceModel(fgCheckList, mlStock_Id)
        Call mbSaveServiceModel(dgvChecklist, mlStock_Id)

        cmdSave.Enabled = False
        cmdLookup.Text = "Exit"
        ToolTip1.SetToolTip(cmdLookup, "Exit editing for this Checklist.")

    End Sub '--save--
    '= = = = = = = =

    Private Sub cmdInsert_Click(ByVal eventSender As System.Object, _
                                   ByVal eventArgs As System.EventArgs) Handles cmdInsert.Click
        Dim lngRow As Integer

        If (dgvChecklist.SelectedRows.Count > 0) Then
            lngRow = dgvChecklist.SelectedRows(0).Cells(0).RowIndex
            If (lngRow) >= 0 And (lngRow < dgvChecklist.RowCount) Then
                dgvChecklist.Rows.Insert(lngRow, 1)
                Call mbNumberGridRows(dgvChecklist)
            End If '--row.-
        Else
            MsgBox("No row selected.." & vbCrLf & "(NB: Row header must be selected..)", MsgBoxStyle.Information)
        End If  '--count..-
    End Sub '--save--
    '= = = = = = = =

    Private Sub cmdDelete_Click(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs) Handles cmdDelete.Click
        '=Dim lngRow As Integer

        '== lngRow = fgCheckList.Row
        '== If (lngRow) > 0 And (lngRow < (fgCheckList.Rows - 1)) Then
        '== fgCheckList.RemoveItem(lngRow)
        '== Call mbNumberGridRows(fgCheckList)
        '== End If '--row.-

        If (dgvChecklist.SelectedRows.Count > 0) Then
            Try
                dgvChecklist.Rows.Remove(dgvChecklist.SelectedRows(0))
                Call mbNumberGridRows(dgvChecklist)
            Catch ex As Exception
                MsgBox("Can't delete that row!" & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
            End Try
        Else
            MsgBox("No row selected.." & vbCrLf & "(NB: Row header must be selected..)", MsgBoxStyle.Information)
        End If
    End Sub '--save--
    '= = = = = = = =
    '-===FF->

    Private Sub cmdExit_Click(ByVal eventSender As System.Object, _
                                ByVal eventArgs As System.EventArgs) Handles cmdExit.Click

        If cmdSave.Enabled Then
            If MsgBox("Abandon changes to model:" & vbCrLf & LabDescription(1).Text & vbCrLf, _
                     MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                cmdSave.Enabled = False '-- bypass form-closing query.-
                Me.Close() '--yes..-
            End If
        Else '--nothing to save..-
            Me.Close()
        End If '--saved..--
    End Sub '--exit--
    '= = = = = = = =

    '--uses QueryUnload--
    Private Sub frmModelEdit_FormClosing(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) _
                                               Handles Me.FormClosing
        Dim intCancel As Boolean = eventArgs.Cancel
        Dim intMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
        Dim ans As Short

        'UPGRADE_ISSUE: Constant vbFormCode was not upgraded. Click for more: 
        'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="55B59875-9A95-4B71-9D6A-7C294BF7139D"'
        Select Case intMode
            Case System.Windows.Forms.CloseReason.WindowsShutDown, _
                  System.Windows.Forms.CloseReason.TaskManagerClosing, _
                         System.Windows.Forms.CloseReason.FormOwnerClosing   '==, vbFormCode
                intCancel = 0 '--let it go--
            Case System.Windows.Forms.CloseReason.UserClosing
                '--Call cmdExit_Click
                ans = MsgBoxResult.Yes
                If cmdSave.Enabled Then
                    ans = MsgBox("Abandon changes to model:" & vbCrLf & vbCrLf & _
                      LabDescription(1).Text & vbCrLf, MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question)
                End If '-enabled..-
                If ans <> MsgBoxResult.Yes Then
                    intCancel = 1 '--cant close yet--
                Else '--yes--
                    intCancel = 0 '--let it go--
                End If
            Case Else
                intCancel = 0 '--let it go--
        End Select '--mode--
        eventArgs.Cancel = intCancel
    End Sub '--queryUnload--
    '= = = = = = = = = =  =

    '=== end form.===

End Class