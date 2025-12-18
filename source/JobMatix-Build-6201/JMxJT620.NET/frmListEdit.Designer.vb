<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmListEdit
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
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
	Public WithEvents ListView1 As System.Windows.Forms.ListView
	Public WithEvents cmdAbort As System.Windows.Forms.Button
	Public WithEvents cmdOk As System.Windows.Forms.Button
	Public WithEvents cmdEdit As System.Windows.Forms.Button
	Public WithEvents cmdRemove As System.Windows.Forms.Button
	Public WithEvents txtEntry As System.Windows.Forms.TextBox
	Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents LabRuler As System.Windows.Forms.Label
	Public WithEvents LabAction As System.Windows.Forms.Label
	Public WithEvents LabHelp1 As System.Windows.Forms.Label
	Public WithEvents LabTable As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents labHelp2 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdAbort = New System.Windows.Forms.Button
        Me.cmdOk = New System.Windows.Forms.Button
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.cmdEdit = New System.Windows.Forms.Button
        Me.cmdRemove = New System.Windows.Forms.Button
        Me.txtEntry = New System.Windows.Forms.TextBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.LabRuler = New System.Windows.Forms.Label
        Me.LabAction = New System.Windows.Forms.Label
        Me.LabHelp1 = New System.Windows.Forms.Label
        Me.LabTable = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.labHelp2 = New System.Windows.Forms.Label
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cmdAbort
        '
        Me.cmdAbort.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbort.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdAbort.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAbort.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbort.Location = New System.Drawing.Point(611, 392)
        Me.cmdAbort.Name = "cmdAbort"
        Me.cmdAbort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAbort.Size = New System.Drawing.Size(54, 25)
        Me.cmdAbort.TabIndex = 9
        Me.cmdAbort.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.cmdAbort, "Abort this entry.")
        Me.cmdAbort.UseVisualStyleBackColor = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(544, 392)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(50, 25)
        Me.cmdOk.TabIndex = 8
        Me.cmdOk.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.cmdOk, "Save this Entry..")
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ListView1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(24, 104)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(505, 257)
        Me.ListView1.TabIndex = 12
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'cmdEdit
        '
        Me.cmdEdit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEdit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEdit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEdit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEdit.Location = New System.Drawing.Point(552, 248)
        Me.cmdEdit.Name = "cmdEdit"
        Me.cmdEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEdit.Size = New System.Drawing.Size(57, 42)
        Me.cmdEdit.TabIndex = 1
        Me.cmdEdit.Text = "Edit Item"
        Me.cmdEdit.UseVisualStyleBackColor = False
        '
        'cmdRemove
        '
        Me.cmdRemove.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRemove.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRemove.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRemove.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRemove.Location = New System.Drawing.Point(552, 306)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRemove.Size = New System.Drawing.Size(57, 25)
        Me.cmdRemove.TabIndex = 2
        Me.cmdRemove.Text = "Remove"
        Me.cmdRemove.UseVisualStyleBackColor = False
        '
        'txtEntry
        '
        Me.txtEntry.AcceptsReturn = True
        Me.txtEntry.BackColor = System.Drawing.SystemColors.Window
        Me.txtEntry.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEntry.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntry.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEntry.Location = New System.Drawing.Point(24, 392)
        Me.txtEntry.MaxLength = 0
        Me.txtEntry.Multiline = True
        Me.txtEntry.Name = "txtEntry"
        Me.txtEntry.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEntry.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEntry.Size = New System.Drawing.Size(505, 21)
        Me.txtEntry.TabIndex = 6
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(600, 528)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(65, 25)
        Me.cmdCancel.TabIndex = 11
        Me.cmdCancel.Text = "Exit"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'LabRuler
        '
        Me.LabRuler.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabRuler.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRuler.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRuler.ForeColor = System.Drawing.SystemColors.WindowText
        Me.LabRuler.Location = New System.Drawing.Point(24, 416)
        Me.LabRuler.Name = "LabRuler"
        Me.LabRuler.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRuler.Size = New System.Drawing.Size(385, 17)
        Me.LabRuler.TabIndex = 13
        Me.LabRuler.Text = "1234567890 2 4 6 8 01234567890 2 4 6 8 01234567890"
        '
        'LabAction
        '
        Me.LabAction.BackColor = System.Drawing.SystemColors.Control
        Me.LabAction.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabAction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabAction.Location = New System.Drawing.Point(24, 376)
        Me.LabAction.Name = "LabAction"
        Me.LabAction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabAction.Size = New System.Drawing.Size(505, 17)
        Me.LabAction.TabIndex = 7
        Me.LabAction.Text = "LabAction"
        '
        'LabHelp1
        '
        Me.LabHelp1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.LabHelp1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHelp1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHelp1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHelp1.Location = New System.Drawing.Point(329, 14)
        Me.LabHelp1.Name = "LabHelp1"
        Me.LabHelp1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHelp1.Size = New System.Drawing.Size(336, 71)
        Me.LabHelp1.TabIndex = 5
        Me.LabHelp1.Text = "LabHelp1"
        '
        'LabTable
        '
        Me.LabTable.BackColor = System.Drawing.SystemColors.Control
        Me.LabTable.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabTable.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabTable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabTable.Location = New System.Drawing.Point(24, 34)
        Me.LabTable.Name = "LabTable"
        Me.LabTable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabTable.Size = New System.Drawing.Size(227, 17)
        Me.LabTable.TabIndex = 4
        Me.LabTable.Text = "labTable"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(24, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(182, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Editing Reference Table:"
        '
        'labHelp2
        '
        Me.labHelp2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labHelp2.Cursor = System.Windows.Forms.Cursors.Default
        Me.labHelp2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labHelp2.Location = New System.Drawing.Point(24, 536)
        Me.labHelp2.Name = "labHelp2"
        Me.labHelp2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labHelp2.Size = New System.Drawing.Size(305, 25)
        Me.labHelp2.TabIndex = 0
        Me.labHelp2.Text = "Enter/edit List Items and press ""Finish"" to Exit"
        Me.labHelp2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(26, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(162, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Click on Column Header to sort.."
        '
        'frmListEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdAbort
        Me.ClientSize = New System.Drawing.Size(684, 574)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.cmdAbort)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.cmdEdit)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.txtEntry)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.LabRuler)
        Me.Controls.Add(Me.LabAction)
        Me.Controls.Add(Me.LabHelp1)
        Me.Controls.Add(Me.LabTable)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.labHelp2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmListEdit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "frmListEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents Label2 As System.Windows.Forms.Label
#End Region 
End Class