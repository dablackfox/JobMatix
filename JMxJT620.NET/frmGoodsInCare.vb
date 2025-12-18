Option Strict Off
Option Explicit On
Imports VB6 = Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Friend Class frmGoodsInCare
	Inherits System.Windows.Forms.Form
    'UPGRADE_NOTE: DefInt A-Z statement was removed. Variables were explicitly declared as type Short. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="92AFD3E3-440D-4D49-A8BF-580D74A8C9F2"'
	'= = = = ===
	
    '==  EDIT old/new GoodsReceived Item..---

    '-- grh- =25-Nov-2011=  UPGRADED to VB.NET..--
    '== grh= 07-Mar-2012==  Location set by calling form..
    '==             DO NOT call centerForm...

    '== grh= 16-Jan-2013  ==  New Form for TESTING mods to GoodsEdit form.
    '==
    '== grh- 10-Mar-2013- Build 3073.310--
    '==   >> MUST set Form.CancelButton to NONE..
    '==       AND cmdCancel.DialogREsult to NONE..
    '==   >>  Fixes to stop crashing when returning from ListEdit   -
    '==            when no Brand selected in drop-down..
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
    '== 
    '==  grh. JobMatix 3.1.3101 ---  16-Sep-2014 ===
    '==   >>  Now Using ADO.net 
    '==   >>  AND:  System.Data.OleDb  (dropped sqlClient).. 
    '==          (.net oleDb provider needed for Jet OleDb driver).
    '==
    '==  >> 11Feb2016-  3203.211-  GoodsIncare Entry Form- 
    '==                      SubClass DataGridView to change ENTER key to TAB..
    '==
    '==
    '= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =


    Const K_TEXTBOXCOUNT As Short = 4

    '-- Sub-classing dataGridView-
    '-- Sub-classing dataGridView-
    '-- Sub-classing dataGridView-
    Friend WithEvents dgvGoods As clsMyDataGridView
    Friend WithEvents GoodsType As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Brand As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Model As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Serial As System.Windows.Forms.DataGridViewTextBoxColumn
    Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()

    '-- end of sub- dgv..-

    Private mbActivated As Boolean = False
    Private mbCancelled As Boolean = False
    Private mColInitialValues As Collection

    Private mCnnJobs As OleDbConnection   '== ADODB.Connection
	
    '== Private mColGoodsTypes As Collection
    '== Private mColBrands As Collection
	
    Private mColResultGoodsInCare As Collection
	
    Private mbDataChanged As Boolean = False
    Private mbEditingNewItem As Boolean = False
    Dim mItemEditing As System.Windows.Forms.ListViewItem

    '--  mandated location..
    Private mIntFormTop As Integer = -1
    Private mIntFormLeft As Integer = -1

    '-- Hold all current COMBO goods-types and Brands.-
    '--  For inclusion checking---

    '== Private mColRefGoodsTypes As Collection
    '== Private mColRefBrands As Collection

    '--Extra Types and Brands added to combo.
    '--   from current Job (history).
    '--  Just to advise user..
    Private mColAddedGoodsTypes As Collection
    Private mColAddedBrands As Collection

    Private mbIsInitialising As Boolean = False
    Private mbChecklistLoading As Boolean = False

    '= = = = = = = = = = =

    '--properties as input parameters--
    WriteOnly Property connection() As OleDbConnection   '== ADODB.Connection
        Set(ByVal Value As OleDbConnection)

            mCnnJobs = Value
        End Set
    End Property
    '- - - - - - - - - -

    '-- INPUT properties..--
	
	WriteOnly Property initialValues() As Collection
		Set(ByVal Value As Collection)
			
			If Not (Value Is Nothing) Then
				mColInitialValues = Value
			End If '--nothing--
			
		End Set
	End Property '--initial.--
	'= = = = = = = = = = = = =
	
	'--goods..-
    '== WriteOnly Property GoodsList() As Collection
    '== 	Set(ByVal Value As Collection)

    '== 		If Not (Value Is Nothing) Then
    '== 			mColGoods = Value
    '== 		End If '--nothing--

    '== 	End Set
    '== End Property '--initial.--
    '= = = = = = = = = = = = =

    '--Brands..-
    '== WriteOnly Property BrandsList() As Collection
    '==     Set(ByVal Value As Collection)
    '==         If Not (Value Is Nothing) Then
    '==             mColBrands = Value
    '==         End If '--nothing--
    '==     End Set
    '== End Property '--initial.--
    '= = = = = = = = = = = = =

    '--Mandated location..-
    WriteOnly Property MandatedFormTop() As Integer
        Set(ByVal Value As Integer)
            If Not (Value < 0) Then
                mIntFormTop = Value
            End If '--nothing--
        End Set
    End Property '--initial.--
    '= = = = = = = = = = = = =

    WriteOnly Property MandatedFormLeft() As Integer
        Set(ByVal Value As Integer)
            If Not (Value < 0) Then
                mIntFormLeft = Value
            End If '--nothing--
        End Set
    End Property '--initial.--
    '= = = = = = = = = = = = =


    '-- results..--
    ReadOnly Property result() As Collection
        Get
            result = mColResultGoodsInCare
        End Get
    End Property '--result.-

    ReadOnly Property cancelled() As Boolean
        Get
            cancelled = mbCancelled
        End Get
    End Property
    '= = = = = = = = = ==
    '-===FF->

    Private Sub mSetDataChanged()

        mbDataChanged = True
        '==cmdPrintAll.Enabled = False
        '==If mbAmending Then cmdFinish.Enabled = True
        '==cmdCancel.Text = "Cancel"
        cmdFinish.Visible = True

    End Sub '--data changed.-
    '= = = = = = = =  = = =


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

    '--Saving and restoring Grid values..

    '-- S a v e to Result collection.-

    Private Function mbSaveDataGrid() As Boolean
        Dim gx, ix As Integer
        Dim colGoodsItem As Collection
        '== Dim colResultGoodsInCare As Collection
        Dim row1 As DataGridViewRow
        '--  FIXES-  10Mar2013--
        Dim sType As String
        Dim sBrand As String
        Dim sModel, sSerial As String

        mColResultGoodsInCare = New Collection

        If (dgvGoods.Rows.Count > 0) Then
            For ix = 0 To (dgvGoods.Rows.Count - 1)
                row1 = dgvGoods.Rows(ix)
                If Not row1.IsNewRow Then '--not the last empty row.-
                    colGoodsItem = New Collection
                    sType = IIf(IsNothing(dgvGoods.Rows(ix).Cells(0).Value), "", _
                                                                 dgvGoods.Rows(ix).Cells(0).Value)
                    sBrand = IIf(IsNothing(dgvGoods.Rows(ix).Cells(1).Value), "", _
                                                                  dgvGoods.Rows(ix).Cells(1).Value)
                    sModel = IIf(IsNothing(dgvGoods.Rows(ix).Cells(2).Value), "", _
                                                                  dgvGoods.Rows(ix).Cells(2).Value)
                    sSerial = IIf(IsNothing(dgvGoods.Rows(ix).Cells(3).Value), "", _
                                                                  dgvGoods.Rows(ix).Cells(3).Value)
                    colGoodsItem.Add(sType, "type")
                    colGoodsItem.Add(sBrand, "brand")
                    colGoodsItem.Add(sModel, "model")
                    colGoodsItem.Add(sSerial, "serialno")
                    mColResultGoodsInCare.Add(colGoodsItem)
                End If  '--new-
            Next ix
        End If
    End Function  '--  save  --
    '= = = = = = = = = = = = = = =
    '= = = = = = = =  = = =
    '-===FF->

    '-- R e s t o r e  from input collection--

    Private Function mbRestoreDataGrid(ByRef colInitialValues As Collection) As Boolean
        Dim gx, ix, rx As Integer
        Dim colItem1 As Collection
        Dim sReport As String = ""
        Dim sType, sBrand, sModel, sSerialNo As String
        Dim columnGoodsType As DataGridViewComboBoxColumn = dgvGoods.Columns(0)
        Dim columnBrand As DataGridViewComboBoxColumn = dgvGoods.Columns(1)
        '== Dim colResultGoodsInCare As Collection
        Dim row1 As DataGridViewRow
        Dim v1 As Object

        dgvGoods.Rows.Clear()  '--clear grid data.-
        mColAddedGoodsTypes = New Collection
        mColAddedBrands = New Collection
        mbChecklistLoading = True

        If Not (colInitialValues Is Nothing) Then
            '--1st check for unauthorised  types/brands..-
            '--  can't load GoodsType/Brand that is not if in combo items..-
            rx = 0
            For Each colItem1 In colInitialValues
                '--get values.-
                sType = colItem1.Item("Type")
                sBrand = colItem1.Item("Brand")
                If Not columnGoodsType.Items.Contains(sType) Then  '--must add.-
                    columnGoodsType.Items.Add(sType)
                    mColAddedGoodsTypes.Add(sType)
                End If
                If Not columnBrand.Items.Contains(sBrand) Then  '--must add.-
                    columnBrand.Items.Add(sBrand)
                    mColAddedBrands.Add(sBrand)
                End If
            Next colItem1
            '--  now load data..
            rx = 0
            For Each colItem1 In colInitialValues
                '--get values.-
                sType = colItem1.Item("Type")
                sBrand = colItem1.Item("Brand")
                sModel = colItem1.Item("Model")
                sSerialNo = colItem1.Item("SerialNo")
                row1 = New DataGridViewRow
                dgvGoods.Rows.Add(row1)
                dgvGoods.Rows(rx).Cells(0).Value = colItem1.Item("Type")
                dgvGoods.Rows(rx).Cells(1).Value = colItem1.Item("Brand")
                dgvGoods.Rows(rx).Cells(2).Value = colItem1.Item("Model")
                dgvGoods.Rows(rx).Cells(3).Value = colItem1.Item("SerialNo")
                rx += 1
            Next colItem1 '--col1..-
        End If  '--initial-
        Call mbNumberGridRows(dgvGoods)

        '--report any extras..-
        If (mColAddedGoodsTypes.Count > 0) Then
            sReport = "Extra Goods Type(s):  "
            For Each v1 In mColAddedGoodsTypes
                sReport = sReport & CStr(v1) & "; "
            Next v1
            '== sReport = sReport & vbCrLf
        End If
        If (mColAddedBrands.Count > 0) Then
            If (sReport <> "") Then sReport = sReport & vbCrLf
            sReport = sReport & "Extra Brands(s):  "
            For Each v1 In mColAddedBrands
                sReport = sReport & CStr(v1) & "; "
            Next v1
            '--sReport = sReport & vbCrLf
        End If
        txtExtraDefs.Text = ""
        If (sReport <> "") Then  '--report--
            txtExtraDefs.Text = "NB: Job has unlisted GoodsType(s) or Brands(s)" & _
                                                                     vbCrLf & sReport
            '== MsgBox("Job has unofficial GoodsType(s) or Brands(s)" & vbCrLf & vbCrLf & _
            '=     sReport, MsgBoxStyle.Information)
        End If
        mbChecklistLoading = False
    End Function  '--  restore  --
    '= = = = = = = =  = = =
    '-===FF->

    '--  build COMBO Box of BRANDS available for jobs..--

    Private Function mbBuildBrands() As Boolean
        Dim sSql As String
        Dim iCount, L1 As Integer
        Dim s1 As String
        Dim rs1 As DataTable  '= As ADODB.Recordset
        Dim columnBrand As DataGridViewComboBoxColumn = dgvGoods.Columns(1)

        mbBuildBrands = False
        On Error GoTo BuildBrands_Error
        iCount = 0

        columnBrand.Items.Clear()
        columnBrand.AutoComplete = True
        columnBrand.Sorted = True

        sSql = "Select * from [JobBrands] "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
            MsgBox("Failed to get jobTasks recordset..", MsgBoxStyle.Exclamation)
        Else ''-ok-
            '= mColBrands = New Collection
            '--build combo box of task types available..-
            If Not (rs1 Is Nothing) Then
                '==If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                For Each datarow1 As DataRow In rs1.Rows
                    '--add to list box for job..
                    iCount = iCount + 1
                    '==cboBrand.AddItem Left(rs1("BrandName"), 24)
                    '== NEW ==
                    s1 = Trim(VB6.Left(datarow1.Item("BrandName"), 24))
                    '-- don't add duplicates..-
                    If Not columnBrand.Items.Contains(s1) Then
                        columnBrand.Items.Add(s1)
                    End If  '--add-
                Next datarow1
                '== While (Not rs1.EOF) '---And (cx < 100)
                '==   rs1.MoveNext()
                '= End While '-eof-
                '== rs1.Close()
                mbBuildBrands = True
            End If '--rs nothing-
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If '--get rs-
        '==cboBrand.Visible = False
        rs1 = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        Exit Function

BuildBrands_Error:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        L1 = Err().Number
        MsgBox("VB Runtime Error in 'Build Brands' function.." & vbCrLf & _
                                  "Error is " & L1 & " = " & ErrorToString(L1))
        Exit Function
    End Function '--build.==
    '= = = = = = =  =  = =
    '---===FF->

    '-- load list box of ref table of GOODS TYPES...-
    Private Function mbLoadRefGoods() As Boolean
        Dim sSql As String
        Dim s1 As String
        Dim rs1 As DataTable  '== ADODB.Recordset
        Dim ix, L1 As Integer
        Dim columnGoodsType As DataGridViewComboBoxColumn = dgvGoods.Columns(0)

        mbLoadRefGoods = False
        On Error GoTo LoadGoods_Error

        columnGoodsType.Items.Clear()
        columnGoodsType.AutoComplete = True
        columnGoodsType.Sorted = True

        sSql = "Select * from [GoodsTypes] "
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        If Not gbGetDataTable(mCnnJobs, rs1, sSql) Then
            MsgBox("Failed to get GoodsTypes recordset..", MsgBoxStyle.Exclamation)
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        Else '--build list box list of GoodsTypes..-
            If Not (rs1 Is Nothing) Then
                '==If rs1.BOF And (Not rs1.EOF) Then rs1.MoveFirst()
                For Each dataRow1 As DataRow In rs1.Rows
                    '--add to list box for job..
                    '== NEW ==
                    s1 = Trim(VB6.Left(dataRow1.Item("GoodsTypeDescription"), 24))
                    '-- don't add duplicates..-
                    If Not columnGoodsType.Items.Contains(s1) Then
                        columnGoodsType.Items.Add(s1)
                    End If  '--contains..-
                Next dataRow1
                '== While (Not rs1.EOF) '---And (cx < 100)
                '==   rs1.MoveNext()
                '= End While '-eof-
                '== rs1.Close()
                mbLoadRefGoods = True
            End If '--rs nothing-
        End If ''--get rs-
        rs1 = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        Exit Function

LoadGoods_Error:
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default '--in case Browser failed--
        L1 = Err().Number
        MsgBox("VB Runtime Error in 'mbLoadRefGoods' function.." & vbCrLf & _
                                  "Error is " & L1 & " = " & ErrorToString(L1))
        Exit Function
    End Function '--Load Goods..-
    '= = = = = = = = = = = = = = = = = 
    '---===FF->

    '--  NB: USER property vars HAVE ALREADY BEEN SET..--
    '--  NB: USER property vars HAVE ALREADY BEEN SET..--

    Private Sub mbOriginal_frmGoodsEdit_Load()
        Dim ix As Short

        dgvGoods_old.Visible = False  '--hide original dgv model..

        '=3203.211-- CREATE sub-classed dataGridView Goods..
        '- load custom datagridview..
        dgvGoods = New clsMyDataGridView
        Me.GoodsType = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Brand = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Model = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Serial = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvGoods, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvGoods --
        '
        Me.dgvGoods.BackgroundColor = System.Drawing.Color.Thistle
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = _
                  New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]

        Me.dgvGoods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvGoods.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvGoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGoods.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() _
                                     {Me.GoodsType, Me.Brand, Me.Model, Me.Serial})
        Me.dgvGoods.Location = New System.Drawing.Point(16, 91)
        Me.dgvGoods.Name = "dgvGoods"
        Me.dgvGoods.Size = New System.Drawing.Size(503, 168)
        Me.dgvGoods.TabIndex = 16
        '
        '--GoodsType--
        '
        Me.GoodsType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.GoodsType.HeaderText = "Goods Type"
        Me.GoodsType.Name = "GoodsType"
        Me.GoodsType.Width = 130
        '
        '--Brand--
        '
        Me.Brand.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Brand.HeaderText = "Brand"
        Me.Brand.Name = "Brand"
        Me.Brand.Width = 120
        '
        '--Model--
        '
        Me.Model.HeaderText = "Model"
        Me.Model.Name = "Model"
        '
        '--Serial--
        '
        Me.Serial.HeaderText = "Serial No."
        Me.Serial.Name = "Serial"
        '
        '--frameGoods--
        Me.FrameGoods.Controls.Add(Me.dgvGoods)
        Me.ResumeLayout(False)

        '-- done building new dataGridView..

        '--disable boxes so we can change..
        For ix = 0 To (K_TEXTBOXCOUNT - 1)
            '==GoodsInCare= txtGoods(ix).Enabled = False
        Next ix

        Me.Text = "Goods In Care"
        '=dataGridView= LabGoodsHdr.Text = ""

        '== Call CenterForm(Me)
        If (mIntFormTop >= 0) Then
            Me.Top = mIntFormTop
        End If
        If (mIntFormLeft >= 0) Then
            Me.Left = mIntFormLeft
        End If

    End Sub '--orig load--
    '= = = = = = = = =

    '-- Load.--

    'UPGRADE_WARNING: Form event frmGoodsEdit.Activate has a new behavior. Click for more:
    '---   'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
 
    Private Sub frmGoodsIncare_Load(ByVal eventSender As System.Object, _
                                       ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim ix, rx As Short
        Dim v1 As Object
        Dim colItem1 As Collection
        Dim item1 As System.Windows.Forms.ListViewItem
        '== Dim row1 As DataGridViewRow

        Call mbOriginal_frmGoodsEdit_Load()
        '== mColResults = New Collection
        cmdFinish.Visible = False

        dgvGoods.EditMode = DataGridViewEditMode.EditOnEnter

        '-- set inital values of flds.--
        '== cboTypes.Items.Clear()
        '== cboBrands.Items.Clear()

        If Not mbLoadRefGoods() Then MsgBox("No Goods types loaded.", MsgBoxStyle.Exclamation)
        If Not mbBuildBrands() Then MsgBox("No Goods Brands loaded.", MsgBoxStyle.Exclamation)

        '== MsgBox("Load combos done..", MsgBoxStyle.Information)  '--test--

        '--load original data..-
        Call mbRestoreDataGrid(mColInitialValues)

        cmdDeleteGoods.Enabled = False
        '= cmdEditGoods.Enabled = False

        '--re-enable--
        For ix = 0 To (K_TEXTBOXCOUNT - 1)
            '==GoodsInCare= txtGoods(ix).Enabled = True
        Next ix

        txtExtraDefs.Text = ""

    End Sub '--load--
    '= = = = = = = = ==

    '--activated-

    Private Sub frmGoodsIncare_Activated(ByVal eventSender As System.Object, _
                                            ByVal eventArgs As System.EventArgs) Handles MyBase.Activated

        If mbActivated Then Exit Sub
        mbActivated = True

        dgvGoods.Select()

    End Sub  '--activated-
    '= = = = = = = = = = = = = = = = =
    '-===FF->

    '--  g r i d  e v e n t s ..-
    '--  g r i d  e v e n t s ..-

    '-- cell change.--

    Private Sub dgvGoods_CellValueChanged(ByVal eventSender As System.Object, _
                                                      ByVal eventArgs As DataGridViewCellEventArgs) _
                                                            Handles dgvGoods.CellValueChanged
        Dim lRow, lCol As Integer
        Dim sStatus, sText As String

        If mbIsInitialising Or mbChecklistLoading Then Exit Sub
        lCol = eventArgs.ColumnIndex
        lRow = eventArgs.RowIndex
        If (lRow >= 0) Then '== And (dgvChecklist.Rows.Count > 0) Then  '--selected a row.--
            '== If (lCol = k_GRIDCOL_STATUS) Then  '--status-
            '==sStatus = dgvChecklist.Rows(lRow).Cells(lCol).Value
            '== '== ?????? ==    Call mbSetDataGridIcon(lRow, sStatus, dgvChecklist)

            '== End If  '--status-
            Call mSetDataChanged()  '== mbDataChanged = True
        End If  '--row-
    End Sub '= CellValueChangedEvent--
    '= = = = = = = = = = = = = = = = = 


    '--  Change selected row..

    Private Sub dgvGoods_SelectionChanged(ByVal eventSender As System.Object, _
                                                  ByVal eventArgs As System.EventArgs) _
                                                        Handles dgvGoods.SelectionChanged
        Dim lRow As Integer
        '== lRow = eventArgs.RowIndex
        If mbIsInitialising Then Exit Sub
        If dgvGoods.CurrentCell Is Nothing Then Exit Sub

        lRow = dgvGoods.CurrentCell.RowIndex

        If (lRow >= 0) Then '--ok--
            cmdDeleteGoods.Enabled = True
            '== mLngSelectedRow = lRow
            '== '--  get stock id and show checklist.--
            '== Call mbSelectEditItem(lRow, False)
        End If '--row--
    End Sub '--click--
    '= = = = = = = = = =

    '-===FF->

    '--  finished editing item.

    '=dataGridView= Private Sub cmdOK_Click(ByVal sender As System.Object, _
    '=dataGridView=                               ByVal e As System.EventArgs)
    '=dataGridView= Dim item1 As System.Windows.Forms.ListViewItem

    '=dataGridView=     If mbEditingNewItem Then
    '=dataGridView= '-- add new row to listview--
    '=dataGridView=         item1 = ListViewGoods.Items.Add(cboTypes.Text)   '--1st column.-
    '=dataGridView=         item1.UseItemStyleForSubItems = True
    '=dataGridView=         item1.BackColor = Color.Gainsboro

    '=dataGridView=         item1.SubItems.Add(cboBrands.Text) '-- sBrand
    '=dataGridView=         item1.SubItems.Add(txtModel.Text) '-sModel
    '=dataGridView=         item1.SubItems.Add(txtSerial.Text) '--sSerialNo
    '=dataGridView=     Else '--editing.--
    '=dataGridView=         item1 = mItemEditing
    '=dataGridView=         item1.Text = cboTypes.Text '--1st column.-
    '=dataGridView=         item1.SubItems(1).Text = cboBrands.Text
    '=dataGridView=         item1.SubItems(2).Text = txtModel.Text
    '=dataGridView=         item1.SubItems(3).Text = txtSerial.Text
    '=dataGridView=     End If
    '=dataGridView= '-- clear edit form.-
    '=dataGridView=     cboTypes.Text = ""
    '=dataGridView=     cboBrands.Text = ""
    '=dataGridView=     txtModel.Text = ""
    '=dataGridView=     txtSerial.Text = ""

    '=dataGridView=     frameEdit.Enabled = False
    '=dataGridView=     ListViewGoods.Enabled = True
    '=dataGridView=     cmdAddGoods.Enabled = True

    '=dataGridView=     cmdOK.Visible = False
    '=dataGridView=     cmdAbortEdit.Visible = False

    '=dataGridView=     cmdFinish.Enabled = True
    '=dataGridView=     cmdCancel.Enabled = True
    '=dataGridView=     LabGoodsHdr.Text = ""

    '=dataGridView=     ListViewGoods.Enabled = True
    '=dataGridView=     FrameGoods.Enabled = True

    '=dataGridView=     ListViewGoods.Select()

    '=dataGridView= End Sub '===ok==
    '= = = = = = = = ==
    '-===FF->

    '-- Abort edit.-

    '=dataGridView= Private Sub cmdAbortEdit_Click(ByVal sender As System.Object, _
    '=dataGridView=                                      ByVal e As System.EventArgs)

    '=dataGridView=     frameEdit.Enabled = False
    '=dataGridView=     cmdOK.Visible = False
    '=dataGridView=     cmdAbortEdit.Visible = False

    '=dataGridView= '-- clear edit form.-
    '=dataGridView=     cboTypes.Text = ""
    '=dataGridView=     cboBrands.Text = ""
    '=dataGridView=     txtModel.Text = ""
    '=dataGridView=     txtSerial.Text = ""

    '=dataGridView=     LabGoodsHdr.Text = ""
    '=dataGridView=     ListViewGoods.Enabled = True
    '=dataGridView=     FrameGoods.Enabled = True
    '=dataGridView=     cmdAddGoods.Enabled = True

    '=dataGridView=     cmdFinish.Enabled = True
    '=dataGridView=     cmdCancel.Enabled = True
    '=dataGridView=     ListViewGoods.Select()

    '=dataGridView= End Sub  '--abort--
    '= = = = = = = = ==
    '-===FF->

    '== NEW ==
    '-- cmd ADD to Goods list..--

    '=dataGridView= Private Sub cmdAddGoods_Click(ByVal eventSender As System.Object, _
    '=dataGridView=                     ByVal eventArgs As System.EventArgs) Handles cmdAddGoods.Click
    '== Dim ix, gx, lCount As Integer
    '== Dim item1 As System.Windows.Forms.ListViewItem

    '=dataGridView=     cmdDeleteGoods.Enabled = False
    '=dataGridView=    If mbGoodsEdit(True) Then
    '=dataGridView= '== 3053.0 =  FrameUsers.Enabled = True '--only if orig., or no prev. username.
    '=dataGridView= '== cboPriority.Enabled = True
    '=dataGridView=         Call mSetDataChanged() '=== mbDataChanged = True
    '=dataGridView= '===If mbAmending Then cmdFinish.Enabled = True
    '=dataGridView=     End If
    '=dataGridView= End Sub '--cmd goods..-
    '=== = = = = = = ==

    '--edit selected Goods Item..-
    '=dataGridView= Private Sub cmdEditGoods_Click(ByVal eventSender As System.Object, _
    '=dataGridView=                                 ByVal eventArgs As System.EventArgs) Handles cmdEditGoods.Click

    '=dataGridView=     cmdDeleteGoods.Enabled = False
    '=dataGridView=    Call mbGoodsEdit(False) '--edit selected item..--
    '=dataGridView=     Call mSetDataChanged() '=== mbDataChanged = True
    '=dataGridView= '===If mbAmending Then cmdFinish.Enabled = True

    '=dataGridView= End Sub '--edit..-
    '= = = = = = = =  =

    '--Delete selected item..-
    Private Sub cmdDeleteGoods_Click(ByVal eventSender As System.Object, _
                                         ByVal eventArgs As System.EventArgs) _
                                                  Handles cmdDeleteGoods.Click
        Dim row1 As DataGridViewRow

        If (dgvGoods.SelectedRows.Count > 0) Then
            row1 = dgvGoods.SelectedRows(0)
            If MsgBox("Delete this Item:" & vbCrLf & CStr(row1.Cells(0).Value) & vbCrLf & vbCrLf & _
                     "From Goods in Care ?", _
                       MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1 + _
                                             MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Try
                    dgvGoods.Rows.Remove(dgvGoods.SelectedRows(0))
                Catch ex As Exception
                    MsgBox("Can't delete that row!" & vbCrLf & ex.ToString, MsgBoxStyle.Exclamation)
                    Exit Sub
                End Try
                Call mSetDataChanged() '=== mbDataChanged = True
                Call mbNumberGridRows(dgvGoods)
            Else '--no-
                Exit Sub
            End If '-yes/no..-
        Else
            MsgBox("No row selected.." & vbCrLf & _
                        "(NB: Row header must be selected..)", MsgBoxStyle.Information)
        End If

    End Sub '--delete--
    '= = = = = == = = = =
    '-===FF->

    '=dataGridView= Private Sub listViewGoods_Click(ByVal eventSender As System.Object, _
    '=dataGridView=                              ByVal eventArgs As System.EventArgs) Handles ListViewGoods.Click
    '=dataGridView=     cmdDeleteGoods.Enabled = True
    '=dataGridView=     cmdEditGoods.Enabled = True
    '=dataGridView= End Sub '--listViewGoods_Click--
    '= = = = =  =

    '=dataGridView= Private Sub listViewGoods_DoubleClick(ByVal eventSender As System.Object, _
    '=dataGridView=                                           ByVal eventArgs As System.EventArgs) Handles ListViewGoods.DoubleClick
    '=dataGridView=     Call mbGoodsEdit(False) '--edit selected item..--

    '=dataGridView= End Sub '--listViewGoods_dblClick--
    '= = = = =  =
    '-===FF->

    '-- create new definition..-
    '-- create new definition..-

    Private Sub CmdCreateGoodsType_Click(ByVal eventSender As System.Object, _
                                             ByVal eventArgs As System.EventArgs) Handles CmdCreateGoodsType.Click
        Dim frmListEdit1 As frmListEdit

        '== Load(frmListEdit)
        frmListEdit1 = New frmListEdit

        frmListEdit1.maxLength = 24
        frmListEdit1.tableName = "GoodsTypes"
        frmListEdit1.DescrColumn = "GoodsTypeDescription"
        frmListEdit1.IdColumn = "GoodsType_Id"
        frmListEdit1.PrimaryKeyColName = "GoodsType_Id"
        frmListEdit1.Top = Me.Top + 200
        frmListEdit1.Left = dgvGoods.Left + 400  '== ListViewGoods.Left + 400
        frmListEdit1.connection = mCnnJobs
        frmListEdit1.deletionsOK = True
        '--MsgBox "Calling edit for: " + sTableName, vbInformation
        frmListEdit1.ShowDialog()
        '== LISTEDIT WRONG= If Not frmListEdit1.cancelled Then '--update..-

        '-- save clear grid before reloading Ref combos..
        '--  else grid data may be not found in new combo list..
        Call mbSaveDataGrid()  '--send result back..
        dgvGoods.Rows.Clear()
        '--load goods list..-
        Call mbLoadRefGoods()

        '--MUST also load Brands list..-
        '--  Restore will re-add ferals, and report..
        Call mbBuildBrands()

        '-restore grid data will update the Goods combo if needed..
        Call mbRestoreDataGrid(mColResultGoodsInCare)

        '====If (ListGoods.ListCount <= 0) Then cmdCheckGoods.Enabled = False
        '== LISTEDIT WRONG= End If '--cancelled..-
        frmListEdit1.Close()
    End Sub '--edit..--
    '= = = = = = = = = =

    '--  Edit Brands Reference..-
    '--  Edit Brands Reference..-

    Private Sub cmdEditBrands_Click(ByVal eventSender As System.Object, _
                                        ByVal eventArgs As System.EventArgs) Handles cmdEditBrands.Click
        Dim frmListEdit1 As frmListEdit

        frmListEdit1 = New frmListEdit
        frmListEdit1.maxLength = 24
        frmListEdit1.tableName = "JobBrands"
        frmListEdit1.DescrColumn = "BrandName"
        frmListEdit1.IdColumn = "Brand_Id"
        frmListEdit1.PrimaryKeyColName = "Brand_Id"
        frmListEdit1.Top = Me.Top + 200
        frmListEdit1.Left = dgvGoods.Left + 400  '==  ListViewGoods.Left + 400
        frmListEdit1.connection = mCnnJobs
        frmListEdit1.deletionsOK = True
        '--MsgBox "Calling edit for: " + sTableName, vbInformation
        frmListEdit1.ShowDialog()
        '== LISTEDIT WRONG= If Not frmListEdit1.cancelled Then '--update..-

        '-- save clear grid before reloading Ref combos..
        '--  else grid data may be not found in new combo list..
        Call mbSaveDataGrid()  '--send result back..
        dgvGoods.Rows.Clear()
        '--load Brands list..-
        Call mbBuildBrands()

        '-- MUST also re-load goods list..-
        '--  Restore will re-add ferals, and report..
        Call mbLoadRefGoods()

        '-restore grid data will update the Goods combo if needed..
        Call mbRestoreDataGrid(mColResultGoodsInCare)

        '====If (cboBrand.ListCount <= 0) Then cboBrand.Enabled = False
        '== LISTEDIT WRONG= End If '--cancelled..-
        frmListEdit1.Close()

    End Sub '--  Edit Brands Reference..-
    '= = = = = = = =  = =  ==
    '-===FF->


    '--  Lookup Goods Type List..-
    '--  Lookup Goods Type List..-
    '--  Lookup Goods Type List..-

    '== Private Sub cmdLookupType_Click(ByVal eventSender As System.Object, _
    '==                                    ByVal eventArgs As System.EventArgs) Handles cmdLookupType.Click

    '==     VB6.SetCancel(cmdCancel, False)
    '==     ListGoods.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(cmdLookupType.Top) - 300)
    '==     ListGoods.Left = cmdLookupType.Left
    '==     ListGoods.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) - VB6.PixelsToTwipsY(cmdLookupType.Top) - 480)

    '==     ListGoods.Visible = True
    '==     ListGoods.Focus()

    '== End Sub '--lookup..-
    '= = = = = = = = ====

    '--   Goods Type List..-
    'UPGRADE_WARNING: Event listGoods.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    '== Private Sub listGoods_SelectedIndexChanged(ByVal eventSender As System.Object, _
    '==                                               ByVal eventArgs As System.EventArgs) Handles ListGoods.SelectedIndexChanged

    '== End Sub '--goods-click--
    '= = = = = = = = = ===

    '--   Goods Type List..-
    '== Private Sub listGoods_DoubleClick(ByVal eventSender As System.Object, _
    '==                                      ByVal eventArgs As System.EventArgs) Handles ListGoods.DoubleClick

    '== '--save selected item.--
    '==     If ListGoods.SelectedIndex >= 0 Then '--selected..
    '==         txtGoods(0).Text = VB6.GetItemString(ListGoods, ListGoods.SelectedIndex)
    '==         VB6.SetCancel(cmdCancel, True) '--let it go.-
    '==         ListGoods.Visible = False
    '==         txtGoods(1).Focus() '--go to Brand.-
    '==     Else '--??-
    '==         ListGoods.Visible = False
    '==     End If '--index-

    '== End Sub '--goods-click--
    '= = = = = = = = = ===

    '== Private Sub listGoods_KeyPress(ByVal eventSender As System.Object, _
    '==                                    ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles ListGoods.KeyPress
    '== Dim keyAscii As Short = Asc(EventArgs.KeyChar)

    '==     If (keyAscii = 13) Then '-enter--
    '==         Call listGoods_DoubleClick(ListGoods, New System.EventArgs())
    '==         keyAscii = 0
    '==     ElseIf (keyAscii = 27) Then  '--ESC--
    '==         VB6.SetCancel(cmdCancel, True) '--let it go.-
    '==         ListGoods.Visible = False
    '==         keyAscii = 0
    '==     End If

    '==     eventArgs.KeyChar = Chr(keyAscii)
    '==     If keyAscii = 0 Then
    '==         eventArgs.Handled = True
    '==     End If
    '== End Sub '--key press..--
    '= = = = = = = = = == =

    Private Sub txtGoods_Enter(ByVal eventSender As System.Object, _
                                  ByVal eventArgs As System.EventArgs)

        '==GoodsInCare= Handles txtGoods.Enter
        Dim index As Short
        '==GoodsInCare= index = txtGoods.GetIndex(eventSender)

        '==GoodsInCare= txtGoods(index).SelectionStart = 0
        '==GoodsInCare= txtGoods(index).SelectionLength = Len(txtGoods(index).Text)

    End Sub '--goFocus..-
    '= = = = = = = = = =


    'UPGRADE_WARNING: Event txtgoods.TextChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    '==GoodsInCare= Private Sub txtgoods_TextChanged(ByVal eventSender As System.Object, _
    '==GoodsInCare=                                      ByVal eventArgs As System.EventArgs) Handles txtGoods.TextChanged
    '==GoodsInCare= Dim index As Short = txtGoods.GetIndex(eventSender)

    '==GoodsInCare=     mbDataChanged = True

    '==GoodsInCare= End Sub '--change..-
    '= = = = = = ==


    Private Sub txtGoods_KeyPress(ByVal eventSender As System.Object, _
                                     ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs)

        '-- etc..-  
        '==GoodsInCare= Handles txtGoods.KeyPress
        Dim keyAscii As Short = Asc(eventArgs.KeyChar)
        Dim index As Short
        '==GoodsInCare= index = txtGoods.GetIndex(eventSender)

        If (keyAscii = 13) Then '-enter--
            If index < 3 Then
                '==GoodsInCare= txtGoods(index + 1).Focus()
            Else
                cmdFinish.Focus()
            End If
            keyAscii = 0
        End If

        eventArgs.KeyChar = Chr(keyAscii)
        If keyAscii = 0 Then
            eventArgs.Handled = True
        End If
    End Sub '--key press..--
    '= = = = = = = = = == =

    '--  BRAND Stuff..-
    '--  BRAND Stuff..-
    '--  BRAND Stuff..-


    '--  Lookup BRAND List..-

    '== Private Sub cmdLookupBrand_Click(ByVal eventSender As System.Object, _
    '==                                      ByVal eventArgs As System.EventArgs) Handles cmdLookupBrand.Click

    '==    VB6.SetCancel(cmdCancel, False)
    '==     ListBrands.Top = cmdLookupBrand.Top
    '==     ListBrands.Left = cmdLookupBrand.Left
    '==     ListBrands.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) - VB6.PixelsToTwipsY(cmdLookupBrand.Top) - 480)

    '==     cmdLookupType.Enabled = False
    '==     ListBrands.Visible = True
    '==     ListBrands.Focus()

    '== End Sub '--lookup..-
    '= = = = = = = = ====

    '--   Brand List..-
    'UPGRADE_WARNING: Event listBrands.SelectedIndexChanged may fire when form is initialized. Click for more: 
    'ms-help://MS.VSExpressCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'

    '==GoodsInCare= Private Sub listBrands_SelectedIndexChanged(ByVal eventSender As System.Object, _
    '==GoodsInCare=                                                ByVal eventArgs As System.EventArgs) Handles ListBrands.SelectedIndexChanged

    '==GoodsInCare= End Sub '--goods-click--
    '= = = = = = = = = ===

    '--   Brand List..-
    '== Private Sub listBrands_DoubleClick(ByVal eventSender As System.Object, _
    '==                                       ByVal eventArgs As System.EventArgs) Handles ListBrands.DoubleClick

    '== '--save selected item.--
    '==     If ListBrands.SelectedIndex >= 0 Then '--selected..
    '==         txtGoods(1).Text = VB6.GetItemString(ListBrands, ListBrands.SelectedIndex)
    '==         VB6.SetCancel(cmdCancel, True) '--let it go.-
    '==         ListBrands.Visible = False
    '==         cmdLookupType.Enabled = True
    '==         txtGoods(2).Focus() '--go to Model.-
    '==     Else '--??-
    '==         ListBrands.Visible = False
    '==     End If '--index-

    '== End Sub '--goods-click--
    '= = = = = = = = = ===

    '== Private Sub listBrands_KeyPress(ByVal eventSender As System.Object, _
    '==                                     ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles ListBrands.KeyPress
    '== Dim keyAscii As Short = Asc(EventArgs.KeyChar)

    '==     If (keyAscii = 13) Then '-enter--
    '==         Call listBrands_DoubleClick(ListBrands, New System.EventArgs())
    '==         keyAscii = 0
    '==     ElseIf (keyAscii = 27) Then  '--ESC--
    '==         VB6.SetCancel(cmdCancel, True) '--let it go.-
    '==         ListBrands.Visible = False
    '==         cmdLookupType.Enabled = True
    '==         keyAscii = 0
    '==     End If

    '==     eventArgs.KeyChar = Chr(keyAscii)
    '==     If keyAscii = 0 Then
    '==         eventArgs.Handled = True
    '==     End If
    '== End Sub '--key press..--
    '= = = = = = = = = == =

    '-- ok--
    '-- ok--

    Private Sub cmdFinish_Click(ByVal eventSender As System.Object, _
                                 ByVal eventArgs As System.EventArgs) Handles cmdFinish.Click
        Dim qx, ix As Integer
        Dim row1 As DataGridViewRow

        '==mColCollectGoodsInCare = colResultGoodsInCare
        qx = 0
        '--CHECK that every row has both GoodsType AND Brand..
        If (dgvGoods.Rows.Count > 0) Then
            For ix = 0 To (dgvGoods.Rows.Count - 1)
                row1 = dgvGoods.Rows(ix)
                If Not row1.IsNewRow Then '--not the last empty row.-
                    If (CStr(row1.Cells(0).Value) = "") Or _
                                        (CStr(row1.Cells(1).Value) = "") Then
                        qx += 1  '--count incomplete rows..-
                    End If  '--empty cell--
                End If  '--new row..-
            Next ix
        End If
        If (qx > 0) Then
            MsgBox("Grid data has " & qx & " incomplete row(s) " & _
                      vbCrLf & vbCrLf & _
                       "(Both GoodsType and Brand are required..", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        '--ok.. store result grid for caller..-
        Call mbSaveDataGrid()  '--send result back..

        Me.Hide()
    End Sub '--ok--
    '= = = = == = =

    Private Sub cmdCancel_Click(ByVal eventSender As System.Object, _
                                    ByVal eventArgs As System.EventArgs) Handles cmdCancel.Click

        If mbDataChanged Then
            If MsgBox("Abandon changes ?", _
               MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Question, _
                                                 "Goods In Care") = MsgBoxResult.Yes Then
                mbCancelled = True
                Me.Hide()
            End If '--yes..-
        Else '-no change..-
            mbCancelled = True
            Me.Hide()
        End If
    End Sub '--cancel-
    '= = = = = == ==

    '== end form ===

End Class