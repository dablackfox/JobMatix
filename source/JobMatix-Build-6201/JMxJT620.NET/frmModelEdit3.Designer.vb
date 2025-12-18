<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmModelEdit
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
        Me.mbIsInitialising = True
        'This call is required by the Windows Form Designer.
		InitializeComponent()
        Me.mbIsInitialising = False
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
	Public WithEvents cmdEdit As System.Windows.Forms.Button
	Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents labRecCount As System.Windows.Forms.Label
	Public WithEvents LabFind As System.Windows.Forms.Label
	Public WithEvents FrameBrowse As System.Windows.Forms.GroupBox
    Public WithEvents cmdLookup As System.Windows.Forms.Button
	Public WithEvents LabExplain As System.Windows.Forms.Label
    Public WithEvents cmdDelete As System.Windows.Forms.Button
	Public WithEvents cmdInsert As System.Windows.Forms.Button
	Public WithEvents cmdSave As System.Windows.Forms.Button
	Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents _LabDescription_1 As System.Windows.Forms.Label
	Public WithEvents _LabDescription_0 As System.Windows.Forms.Label
	Public WithEvents LabDefinition As System.Windows.Forms.Label
	Public WithEvents LabEdit As System.Windows.Forms.Label
	Public WithEvents LabHdr As System.Windows.Forms.Label
	Public WithEvents LabDescription As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdInsert = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.cmdLookup = New System.Windows.Forms.Button
        Me.FrameBrowse = New System.Windows.Forms.GroupBox
        Me.labFrameBrowse = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.txtFind = New System.Windows.Forms.TextBox
        Me.labRecCount = New System.Windows.Forms.Label
        Me.LabFind = New System.Windows.Forms.Label
        Me.LabExplain = New System.Windows.Forms.Label
        Me.cmdExit = New System.Windows.Forms.Button
        Me._LabDescription_1 = New System.Windows.Forms.Label
        Me._LabDescription_0 = New System.Windows.Forms.Label
        Me.LabDefinition = New System.Windows.Forms.Label
        Me.LabEdit = New System.Windows.Forms.Label
        Me.LabHdr = New System.Windows.Forms.Label
        Me.LabDescription = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.dgvChecklist = New System.Windows.Forms.DataGridView
        Me.TaskDescription = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.frameModelEdit = New System.Windows.Forms.GroupBox
        Me.labEditHelp = New System.Windows.Forms.Label
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        Me.FrameBrowse.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabDescription, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvChecklist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frameModelEdit.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Location = New System.Drawing.Point(438, 22)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(49, 21)
        Me.cmdDelete.TabIndex = 3
        Me.cmdDelete.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "DElete selected task from list..")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdInsert
        '
        Me.cmdInsert.BackColor = System.Drawing.SystemColors.Control
        Me.cmdInsert.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdInsert.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdInsert.Location = New System.Drawing.Point(384, 22)
        Me.cmdInsert.Name = "cmdInsert"
        Me.cmdInsert.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdInsert.Size = New System.Drawing.Size(49, 21)
        Me.cmdInsert.TabIndex = 2
        Me.cmdInsert.Text = "Insert"
        Me.ToolTip1.SetToolTip(Me.cmdInsert, "Insert new line above current item..")
        Me.cmdInsert.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(518, 22)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(49, 21)
        Me.cmdSave.TabIndex = 1
        Me.cmdSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save changes to  this Model..")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdEdit
        '
        Me.cmdEdit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEdit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEdit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEdit.Location = New System.Drawing.Point(566, 222)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEdit.Size = New System.Drawing.Size(56, 21)
        Me.cmdEdit.TabIndex = 16
        Me.cmdEdit.Text = "Select"
        Me.ToolTip1.SetToolTip(Me.cmdEdit, "Edit the Task Checklist for the selected Service Item.")
        Me.cmdEdit.UseVisualStyleBackColor = False
        '
        'cmdLookup
        '
        Me.cmdLookup.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLookup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLookup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLookup.Location = New System.Drawing.Point(573, 21)
        Me.cmdLookup.Name = "cmdLookup"
        Me.cmdLookup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLookup.Size = New System.Drawing.Size(49, 21)
        Me.cmdLookup.TabIndex = 11
        Me.cmdLookup.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.cmdLookup, "Abort editing for this Checklist.")
        Me.cmdLookup.UseVisualStyleBackColor = False
        '
        'FrameBrowse
        '
        Me.FrameBrowse.BackColor = System.Drawing.Color.FromArgb(CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.FrameBrowse.Controls.Add(Me.labFrameBrowse)
        Me.FrameBrowse.Controls.Add(Me.DataGridView1)
        Me.FrameBrowse.Controls.Add(Me.cmdEdit)
        Me.FrameBrowse.Controls.Add(Me.txtFind)
        Me.FrameBrowse.Controls.Add(Me.labRecCount)
        Me.FrameBrowse.Controls.Add(Me.LabFind)
        Me.FrameBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameBrowse.Location = New System.Drawing.Point(19, 94)
        Me.FrameBrowse.Name = "FrameBrowse"
        Me.FrameBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameBrowse.Size = New System.Drawing.Size(637, 247)
        Me.FrameBrowse.TabIndex = 14
        Me.FrameBrowse.TabStop = False
        Me.FrameBrowse.Text = "FrameBrowse"
        '
        'labFrameBrowse
        '
        Me.labFrameBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labFrameBrowse.Location = New System.Drawing.Point(8, 14)
        Me.labFrameBrowse.Name = "labFrameBrowse"
        Me.labFrameBrowse.Size = New System.Drawing.Size(250, 11)
        Me.labFrameBrowse.TabIndex = 22
        Me.labFrameBrowse.Text = "labFrameBrowse"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.GridColor = System.Drawing.Color.Gainsboro
        Me.DataGridView1.Location = New System.Drawing.Point(8, 58)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(616, 160)
        Me.DataGridView1.StandardTab = True
        Me.DataGridView1.TabIndex = 21
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(104, 39)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(89, 15)
        Me.txtFind.TabIndex = 15
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(8, 224)
        Me.labRecCount.Name = "labRecCount"
        Me.labRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCount.Size = New System.Drawing.Size(121, 17)
        Me.labRecCount.TabIndex = 19
        Me.labRecCount.Text = "labRecCount"
        '
        'LabFind
        '
        Me.LabFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabFind.Location = New System.Drawing.Point(8, 31)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(89, 25)
        Me.LabFind.TabIndex = 18
        Me.LabFind.Text = "LabFind"
        '
        'LabExplain
        '
        Me.LabExplain.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.LabExplain.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabExplain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabExplain.Location = New System.Drawing.Point(249, 5)
        Me.LabExplain.Name = "LabExplain"
        Me.LabExplain.Padding = New System.Windows.Forms.Padding(3)
        Me.LabExplain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabExplain.Size = New System.Drawing.Size(405, 86)
        Me.LabExplain.TabIndex = 8
        Me.LabExplain.Text = "LabExplain"
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(605, 637)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(49, 21)
        Me.cmdExit.TabIndex = 4
        Me.cmdExit.Text = "Close"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        '_LabDescription_1
        '
        Me._LabDescription_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me._LabDescription_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabDescription_1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabDescription_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDescription.SetIndex(Me._LabDescription_1, CType(1, Short))
        Me._LabDescription_1.Location = New System.Drawing.Point(8, 142)
        Me._LabDescription_1.Name = "_LabDescription_1"
        Me._LabDescription_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabDescription_1.Size = New System.Drawing.Size(199, 88)
        Me._LabDescription_1.TabIndex = 22
        Me._LabDescription_1.Text = "LabDescription(1)"
        '
        '_LabDescription_0
        '
        Me._LabDescription_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me._LabDescription_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabDescription_0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabDescription_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDescription.SetIndex(Me._LabDescription_0, CType(0, Short))
        Me._LabDescription_0.Location = New System.Drawing.Point(8, 70)
        Me._LabDescription_0.Name = "_LabDescription_0"
        Me._LabDescription_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabDescription_0.Size = New System.Drawing.Size(199, 60)
        Me._LabDescription_0.TabIndex = 21
        Me._LabDescription_0.Text = "LabDescription(0)"
        '
        'LabDefinition
        '
        Me.LabDefinition.BackColor = System.Drawing.Color.Transparent
        Me.LabDefinition.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDefinition.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDefinition.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDefinition.Location = New System.Drawing.Point(17, 629)
        Me.LabDefinition.Name = "LabDefinition"
        Me.LabDefinition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDefinition.Size = New System.Drawing.Size(448, 29)
        Me.LabDefinition.TabIndex = 10
        Me.LabDefinition.Text = "LabDefinition"
        '
        'LabEdit
        '
        Me.LabEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.LabEdit.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabEdit.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabEdit.ForeColor = System.Drawing.Color.Maroon
        Me.LabEdit.Location = New System.Drawing.Point(8, 19)
        Me.LabEdit.Name = "LabEdit"
        Me.LabEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabEdit.Size = New System.Drawing.Size(165, 27)
        Me.LabEdit.TabIndex = 9
        Me.LabEdit.Text = " -Editing Service Model -"
        Me.LabEdit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabHdr
        '
        Me.LabHdr.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr.Location = New System.Drawing.Point(19, 8)
        Me.LabHdr.Name = "LabHdr"
        Me.LabHdr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr.Size = New System.Drawing.Size(224, 41)
        Me.LabHdr.TabIndex = 6
        Me.LabHdr.Text = "JobMatix-  Editing Service Models.."
        '
        'dgvChecklist
        '
        Me.dgvChecklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChecklist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TaskDescription})
        Me.dgvChecklist.GridColor = System.Drawing.Color.Gainsboro
        Me.dgvChecklist.Location = New System.Drawing.Point(215, 57)
        Me.dgvChecklist.MultiSelect = False
        Me.dgvChecklist.Name = "dgvChecklist"
        Me.dgvChecklist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvChecklist.Size = New System.Drawing.Size(409, 211)
        Me.dgvChecklist.StandardTab = True
        Me.dgvChecklist.TabIndex = 23
        '
        'TaskDescription
        '
        Me.TaskDescription.HeaderText = "Task Name/Description"
        Me.TaskDescription.MaxInputLength = 80
        Me.TaskDescription.Name = "TaskDescription"
        Me.TaskDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TaskDescription.Width = 360
        '
        'frameModelEdit
        '
        Me.frameModelEdit.Controls.Add(Me.labEditHelp)
        Me.frameModelEdit.Controls.Add(Me.LabEdit)
        Me.frameModelEdit.Controls.Add(Me.dgvChecklist)
        Me.frameModelEdit.Controls.Add(Me.cmdSave)
        Me.frameModelEdit.Controls.Add(Me.cmdInsert)
        Me.frameModelEdit.Controls.Add(Me._LabDescription_1)
        Me.frameModelEdit.Controls.Add(Me.cmdLookup)
        Me.frameModelEdit.Controls.Add(Me._LabDescription_0)
        Me.frameModelEdit.Controls.Add(Me.cmdDelete)
        Me.frameModelEdit.Location = New System.Drawing.Point(19, 347)
        Me.frameModelEdit.Name = "frameModelEdit"
        Me.frameModelEdit.Size = New System.Drawing.Size(635, 277)
        Me.frameModelEdit.TabIndex = 24
        Me.frameModelEdit.TabStop = False
        Me.frameModelEdit.Text = "frameModelEdit"
        '
        'labEditHelp
        '
        Me.labEditHelp.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.labEditHelp.Location = New System.Drawing.Point(217, 17)
        Me.labEditHelp.Name = "labEditHelp"
        Me.labEditHelp.Padding = New System.Windows.Forms.Padding(3)
        Me.labEditHelp.Size = New System.Drawing.Size(161, 35)
        Me.labEditHelp.TabIndex = 24
        Me.labEditHelp.Text = "labEditHelp"
        '
        'frmModelEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(682, 667)
        Me.Controls.Add(Me.LabExplain)
        Me.Controls.Add(Me.frameModelEdit)
        Me.Controls.Add(Me.FrameBrowse)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.LabDefinition)
        Me.Controls.Add(Me.LabHdr)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmModelEdit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "frmModelEdit"
        Me.FrameBrowse.ResumeLayout(False)
        Me.FrameBrowse.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabDescription, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvChecklist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frameModelEdit.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvChecklist As System.Windows.Forms.DataGridView
    Friend WithEvents labFrameBrowse As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents frameModelEdit As System.Windows.Forms.GroupBox
    Friend WithEvents labEditHelp As System.Windows.Forms.Label
    Friend WithEvents TaskDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
#End Region
End Class