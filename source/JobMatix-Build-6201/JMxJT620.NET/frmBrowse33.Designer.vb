<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBrowse
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
                Me.mbIsInitialising=true
		'This call is required by the Windows Form Designer.
		InitializeComponent()
                Me.mbIsInitialising=false
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents txtFind As System.Windows.Forms.TextBox
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents LabFind As System.Windows.Forms.Label
	Public WithEvents Labwhere As System.Windows.Forms.Label
	Public WithEvents labTable As System.Windows.Forms.Label
	Public WithEvents LabRecCount As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdClearTextSearch = New System.Windows.Forms.Button()
        Me.cmdTextSearch = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.Labwhere = New System.Windows.Forms.Label()
        Me.labTable = New System.Windows.Forms.Label()
        Me.LabRecCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtTextSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(439, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 32)
        Me.Label5.TabIndex = 85
        Me.Label5.Text = "Full Text Search and Filter"
        Me.ToolTip1.SetToolTip(Me.Label5, "Search/Filter text columns for text fragments.." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  (No asterisks please..)")
        '
        'cmdClearTextSearch
        '
        Me.cmdClearTextSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClearTextSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearTextSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClearTextSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearTextSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearTextSearch.Location = New System.Drawing.Point(704, 72)
        Me.cmdClearTextSearch.Name = "cmdClearTextSearch"
        Me.cmdClearTextSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearTextSearch.Size = New System.Drawing.Size(65, 23)
        Me.cmdClearTextSearch.TabIndex = 6
        Me.cmdClearTextSearch.Text = "X  Clear"
        Me.ToolTip1.SetToolTip(Me.cmdClearTextSearch, "Clear Search Text and refresh grid..")
        Me.cmdClearTextSearch.UseVisualStyleBackColor = False
        '
        'cmdTextSearch
        '
        Me.cmdTextSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdTextSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTextSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdTextSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTextSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTextSearch.Location = New System.Drawing.Point(626, 72)
        Me.cmdTextSearch.Name = "cmdTextSearch"
        Me.cmdTextSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTextSearch.Size = New System.Drawing.Size(61, 23)
        Me.cmdTextSearch.TabIndex = 5
        Me.cmdTextSearch.Text = "Search"
        Me.cmdTextSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdTextSearch, "Search/Refresh customer list.")
        Me.cmdTextSearch.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 200
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(15, 87)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(240, 16)
        Me.txtFind.TabIndex = 2
        Me.txtFind.Text = "txtFind"
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.Plum
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(748, 608)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(64, 25)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.Color.Honeydew
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(660, 608)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(64, 25)
        Me.cmdOk.TabIndex = 7
        Me.cmdOk.Text = "OK"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'LabFind
        '
        Me.LabFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabFind.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabFind.Location = New System.Drawing.Point(15, 55)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(240, 29)
        Me.LabFind.TabIndex = 1
        Me.LabFind.Text = "Find xxx"
        '
        'Labwhere
        '
        Me.Labwhere.BackColor = System.Drawing.Color.Transparent
        Me.Labwhere.Cursor = System.Windows.Forms.Cursors.Default
        Me.Labwhere.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Labwhere.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Labwhere.Location = New System.Drawing.Point(17, 597)
        Me.Labwhere.Name = "Labwhere"
        Me.Labwhere.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Labwhere.Size = New System.Drawing.Size(321, 41)
        Me.Labwhere.TabIndex = 6
        Me.Labwhere.Text = "labWhere"
        '
        'labTable
        '
        Me.labTable.BackColor = System.Drawing.Color.Transparent
        Me.labTable.Cursor = System.Windows.Forms.Cursors.Default
        Me.labTable.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labTable.ForeColor = System.Drawing.Color.DarkGreen
        Me.labTable.Location = New System.Drawing.Point(16, 30)
        Me.labTable.Name = "labTable"
        Me.labTable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labTable.Size = New System.Drawing.Size(273, 21)
        Me.labTable.TabIndex = 0
        Me.labTable.Text = "labTable"
        '
        'LabRecCount
        '
        Me.LabRecCount.BackColor = System.Drawing.SystemColors.Control
        Me.LabRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRecCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRecCount.Location = New System.Drawing.Point(368, 609)
        Me.LabRecCount.Name = "LabRecCount"
        Me.LabRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRecCount.Size = New System.Drawing.Size(73, 17)
        Me.LabRecCount.TabIndex = 1
        Me.LabRecCount.Text = "labRecCount"
        Me.LabRecCount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(448, 609)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(57, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "records"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.Control
        Me.DataGridView1.Location = New System.Drawing.Point(15, 106)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(820, 488)
        Me.DataGridView1.StandardTab = True
        Me.DataGridView1.TabIndex = 3
        '
        'txtTextSearch
        '
        Me.txtTextSearch.AcceptsReturn = True
        Me.txtTextSearch.BackColor = System.Drawing.Color.Lavender
        Me.txtTextSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTextSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTextSearch.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTextSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTextSearch.Location = New System.Drawing.Point(442, 73)
        Me.txtTextSearch.MaxLength = 0
        Me.txtTextSearch.Name = "txtTextSearch"
        Me.txtTextSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTextSearch.Size = New System.Drawing.Size(164, 22)
        Me.txtTextSearch.TabIndex = 4
        Me.txtTextSearch.Text = "txtTextSearch"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Gainsboro
        Me.Label1.Location = New System.Drawing.Point(405, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 87)
        Me.Label1.TabIndex = 86
        '
        'frmBrowse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(847, 646)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmdClearTextSearch)
        Me.Controls.Add(Me.cmdTextSearch)
        Me.Controls.Add(Me.txtTextSearch)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtFind)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.LabFind)
        Me.Controls.Add(Me.Labwhere)
        Me.Controls.Add(Me.labTable)
        Me.Controls.Add(Me.LabRecCount)
        Me.Controls.Add(Me.Label2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(11, 30)
        Me.Name = "frmBrowse"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Browse Table"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents cmdClearTextSearch As System.Windows.Forms.Button
    Public WithEvents cmdTextSearch As System.Windows.Forms.Button
    Public WithEvents txtTextSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
#End Region 
End Class