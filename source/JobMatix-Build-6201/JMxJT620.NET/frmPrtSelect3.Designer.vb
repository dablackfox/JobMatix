<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrtSelect
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
	Public WithEvents cboPrtSelect As System.Windows.Forms.ComboBox
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdOk As System.Windows.Forms.Button
	Public WithEvents LabMsg As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cboPrtSelect = New System.Windows.Forms.ComboBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdOk = New System.Windows.Forms.Button
        Me.LabMsg = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboReceiptPrtSelect = New System.Windows.Forms.ComboBox
        Me.cboLabelPrtSelect = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtColourPrinter = New System.Windows.Forms.TextBox
        Me.txtReceiptPrinter = New System.Windows.Forms.TextBox
        Me.txtLabelPrinter = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'cboPrtSelect
        '
        Me.cboPrtSelect.BackColor = System.Drawing.SystemColors.Window
        Me.cboPrtSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPrtSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrtSelect.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPrtSelect.Location = New System.Drawing.Point(56, 104)
        Me.cboPrtSelect.Name = "cboPrtSelect"
        Me.cboPrtSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPrtSelect.Size = New System.Drawing.Size(262, 21)
        Me.cboPrtSelect.TabIndex = 3
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(374, 321)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(49, 23)
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(297, 321)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(49, 23)
        Me.cmdOk.TabIndex = 1
        Me.cmdOk.Text = "OK"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'LabMsg
        '
        Me.LabMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabMsg.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabMsg.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabMsg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabMsg.Location = New System.Drawing.Point(34, 15)
        Me.LabMsg.Name = "LabMsg"
        Me.LabMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabMsg.Size = New System.Drawing.Size(280, 23)
        Me.LabMsg.TabIndex = 0
        Me.LabMsg.Text = "Change JobMatix Printer assignments"
        Me.LabMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(59, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Colour/A4 Printer"
        '
        'cboReceiptPrtSelect
        '
        Me.cboReceiptPrtSelect.BackColor = System.Drawing.SystemColors.Window
        Me.cboReceiptPrtSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboReceiptPrtSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrtSelect.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboReceiptPrtSelect.Location = New System.Drawing.Point(56, 180)
        Me.cboReceiptPrtSelect.Name = "cboReceiptPrtSelect"
        Me.cboReceiptPrtSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboReceiptPrtSelect.Size = New System.Drawing.Size(262, 21)
        Me.cboReceiptPrtSelect.TabIndex = 5
        '
        'cboLabelPrtSelect
        '
        Me.cboLabelPrtSelect.BackColor = System.Drawing.SystemColors.Window
        Me.cboLabelPrtSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLabelPrtSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLabelPrtSelect.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLabelPrtSelect.Location = New System.Drawing.Point(56, 257)
        Me.cboLabelPrtSelect.Name = "cboLabelPrtSelect"
        Me.cboLabelPrtSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLabelPrtSelect.Size = New System.Drawing.Size(262, 21)
        Me.cboLabelPrtSelect.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(59, 150)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Receipt Printer"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(59, 221)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Label Printer"
        '
        'txtColourPrinter
        '
        Me.txtColourPrinter.BackColor = System.Drawing.Color.Gainsboro
        Me.txtColourPrinter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtColourPrinter.Location = New System.Drawing.Point(56, 90)
        Me.txtColourPrinter.Name = "txtColourPrinter"
        Me.txtColourPrinter.ReadOnly = True
        Me.txtColourPrinter.Size = New System.Drawing.Size(258, 14)
        Me.txtColourPrinter.TabIndex = 9
        '
        'txtReceiptPrinter
        '
        Me.txtReceiptPrinter.BackColor = System.Drawing.Color.Gainsboro
        Me.txtReceiptPrinter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtReceiptPrinter.Location = New System.Drawing.Point(56, 166)
        Me.txtReceiptPrinter.Name = "txtReceiptPrinter"
        Me.txtReceiptPrinter.ReadOnly = True
        Me.txtReceiptPrinter.Size = New System.Drawing.Size(262, 14)
        Me.txtReceiptPrinter.TabIndex = 10
        '
        'txtLabelPrinter
        '
        Me.txtLabelPrinter.BackColor = System.Drawing.Color.Gainsboro
        Me.txtLabelPrinter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabelPrinter.Location = New System.Drawing.Point(56, 237)
        Me.txtLabelPrinter.Name = "txtLabelPrinter"
        Me.txtLabelPrinter.ReadOnly = True
        Me.txtLabelPrinter.Size = New System.Drawing.Size(262, 14)
        Me.txtLabelPrinter.TabIndex = 11
        '
        'frmPrtSelect
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(457, 369)
        Me.Controls.Add(Me.txtLabelPrinter)
        Me.Controls.Add(Me.txtReceiptPrinter)
        Me.Controls.Add(Me.txtColourPrinter)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboLabelPrtSelect)
        Me.Controls.Add(Me.cboReceiptPrtSelect)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboPrtSelect)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.LabMsg)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmPrtSelect"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "frmPrtSelect"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cboReceiptPrtSelect As System.Windows.Forms.ComboBox
    Public WithEvents cboLabelPrtSelect As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtColourPrinter As System.Windows.Forms.TextBox
    Friend WithEvents txtReceiptPrinter As System.Windows.Forms.TextBox
    Friend WithEvents txtLabelPrinter As System.Windows.Forms.TextBox
#End Region 
End Class