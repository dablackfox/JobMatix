<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmFindPart
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
	Public WithEvents cmdSerialClear As System.Windows.Forms.Button
	Public WithEvents cmdSerialSrch As System.Windows.Forms.Button
	Public WithEvents txtSerialArg As System.Windows.Forms.TextBox
	Public WithEvents LabStatus As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents FrameSerialAudit As System.Windows.Forms.GroupBox
	Public WithEvents _OptStatus_2 As System.Windows.Forms.RadioButton
	Public WithEvents _OptStatus_1 As System.Windows.Forms.RadioButton
	Public WithEvents _OptStatus_0 As System.Windows.Forms.RadioButton
	Public WithEvents cmdJobPartClear As System.Windows.Forms.Button
	Public WithEvents cboMonths As System.Windows.Forms.ComboBox
	Public WithEvents cmdRefresh As System.Windows.Forms.Button
	Public WithEvents txtArg As System.Windows.Forms.TextBox
	Public WithEvents LabPeriod As System.Windows.Forms.Label
	Public WithEvents LabPrompt As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents FrameJobParts As System.Windows.Forms.GroupBox
	Public WithEvents cmdExit As System.Windows.Forms.Button
	Public WithEvents ListView1 As System.Windows.Forms.ListView
	Public WithEvents LabNoRecords As System.Windows.Forms.Label
	Public WithEvents LabHdr As System.Windows.Forms.Label
	Public WithEvents OptStatus As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSerialCancel = New System.Windows.Forms.Button()
        Me.FrameSerialAudit = New System.Windows.Forms.GroupBox()
        Me.cmdSerialClear = New System.Windows.Forms.Button()
        Me.cmdSerialSrch = New System.Windows.Forms.Button()
        Me.txtSerialArg = New System.Windows.Forms.TextBox()
        Me.LabStatus = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FrameJobParts = New System.Windows.Forms.GroupBox()
        Me._OptStatus_2 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_1 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_0 = New System.Windows.Forms.RadioButton()
        Me.cmdJobPartClear = New System.Windows.Forms.Button()
        Me.cboMonths = New System.Windows.Forms.ComboBox()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.txtArg = New System.Windows.Forms.TextBox()
        Me.LabPeriod = New System.Windows.Forms.Label()
        Me.LabPrompt = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.LabNoRecords = New System.Windows.Forms.Label()
        Me.LabHdr = New System.Windows.Forms.Label()
        Me.OptStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.frameListView = New System.Windows.Forms.GroupBox()
        Me.FrameSerialAudit.SuspendLayout()
        Me.FrameJobParts.SuspendLayout()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frameListView.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSerialCancel
        '
        Me.cmdSerialCancel.Location = New System.Drawing.Point(735, 122)
        Me.cmdSerialCancel.Name = "cmdSerialCancel"
        Me.cmdSerialCancel.Size = New System.Drawing.Size(54, 22)
        Me.cmdSerialCancel.TabIndex = 18
        Me.cmdSerialCancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.cmdSerialCancel, "Cancel Search")
        Me.cmdSerialCancel.UseVisualStyleBackColor = True
        '
        'FrameSerialAudit
        '
        Me.FrameSerialAudit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameSerialAudit.Controls.Add(Me.cmdSerialCancel)
        Me.FrameSerialAudit.Controls.Add(Me.cmdSerialClear)
        Me.FrameSerialAudit.Controls.Add(Me.cmdSerialSrch)
        Me.FrameSerialAudit.Controls.Add(Me.txtSerialArg)
        Me.FrameSerialAudit.Controls.Add(Me.LabStatus)
        Me.FrameSerialAudit.Controls.Add(Me.Label3)
        Me.FrameSerialAudit.Controls.Add(Me.Label2)
        Me.FrameSerialAudit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameSerialAudit.Location = New System.Drawing.Point(8, 360)
        Me.FrameSerialAudit.Name = "FrameSerialAudit"
        Me.FrameSerialAudit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameSerialAudit.Size = New System.Drawing.Size(807, 153)
        Me.FrameSerialAudit.TabIndex = 10
        Me.FrameSerialAudit.TabStop = False
        Me.FrameSerialAudit.Text = "FrameSerialAudit"
        '
        'cmdSerialClear
        '
        Me.cmdSerialClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSerialClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSerialClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSerialClear.Location = New System.Drawing.Point(282, 120)
        Me.cmdSerialClear.Name = "cmdSerialClear"
        Me.cmdSerialClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSerialClear.Size = New System.Drawing.Size(25, 25)
        Me.cmdSerialClear.TabIndex = 12
        Me.cmdSerialClear.Text = "X"
        Me.cmdSerialClear.UseVisualStyleBackColor = False
        '
        'cmdSerialSrch
        '
        Me.cmdSerialSrch.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSerialSrch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSerialSrch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSerialSrch.Location = New System.Drawing.Point(321, 120)
        Me.cmdSerialSrch.Name = "cmdSerialSrch"
        Me.cmdSerialSrch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSerialSrch.Size = New System.Drawing.Size(49, 25)
        Me.cmdSerialSrch.TabIndex = 13
        Me.cmdSerialSrch.Text = "Search"
        Me.cmdSerialSrch.UseVisualStyleBackColor = False
        '
        'txtSerialArg
        '
        Me.txtSerialArg.AcceptsReturn = True
        Me.txtSerialArg.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSerialArg.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSerialArg.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSerialArg.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialArg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSerialArg.Location = New System.Drawing.Point(27, 120)
        Me.txtSerialArg.MaxLength = 0
        Me.txtSerialArg.Name = "txtSerialArg"
        Me.txtSerialArg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSerialArg.Size = New System.Drawing.Size(241, 15)
        Me.txtSerialArg.TabIndex = 11
        '
        'LabStatus
        '
        Me.LabStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabStatus.Location = New System.Drawing.Point(383, 120)
        Me.LabStatus.Name = "LabStatus"
        Me.LabStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabStatus.Size = New System.Drawing.Size(341, 25)
        Me.LabStatus.TabIndex = 17
        Me.LabStatus.Text = "LabStatus"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(24, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(372, 33)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Listing of the Retail (or POS) Serial-Audit Table.. Includes all item Serials kno" & _
    "wn to the System.."
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(24, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(233, 41)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Text Search in  SerialNo, Barcode,  Cat1 &&  Item Description cols."
        '
        'FrameJobParts
        '
        Me.FrameJobParts.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameJobParts.Controls.Add(Me._OptStatus_2)
        Me.FrameJobParts.Controls.Add(Me._OptStatus_1)
        Me.FrameJobParts.Controls.Add(Me._OptStatus_0)
        Me.FrameJobParts.Controls.Add(Me.cmdJobPartClear)
        Me.FrameJobParts.Controls.Add(Me.cboMonths)
        Me.FrameJobParts.Controls.Add(Me.cmdRefresh)
        Me.FrameJobParts.Controls.Add(Me.txtArg)
        Me.FrameJobParts.Controls.Add(Me.LabPeriod)
        Me.FrameJobParts.Controls.Add(Me.LabPrompt)
        Me.FrameJobParts.Controls.Add(Me.Label1)
        Me.FrameJobParts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameJobParts.Location = New System.Drawing.Point(8, 64)
        Me.FrameJobParts.Name = "FrameJobParts"
        Me.FrameJobParts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameJobParts.Size = New System.Drawing.Size(807, 153)
        Me.FrameJobParts.TabIndex = 3
        Me.FrameJobParts.TabStop = False
        Me.FrameJobParts.Text = "FrameJobParts"
        '
        '_OptStatus_2
        '
        Me._OptStatus_2.BackColor = System.Drawing.Color.Gainsboro
        Me._OptStatus_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_2, CType(2, Short))
        Me._OptStatus_2.Location = New System.Drawing.Point(584, 16)
        Me._OptStatus_2.Name = "_OptStatus_2"
        Me._OptStatus_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_2.Size = New System.Drawing.Size(129, 62)
        Me._OptStatus_2.TabIndex = 21
        Me._OptStatus_2.TabStop = True
        Me._OptStatus_2.Text = "OR:  Search    DELIVERED Jobs-     Select Month Part used."
        Me._OptStatus_2.UseVisualStyleBackColor = False
        '
        '_OptStatus_1
        '
        Me._OptStatus_1.BackColor = System.Drawing.Color.Gainsboro
        Me._OptStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_1, CType(1, Short))
        Me._OptStatus_1.Location = New System.Drawing.Point(457, 64)
        Me._OptStatus_1.Name = "_OptStatus_1"
        Me._OptStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_1.Size = New System.Drawing.Size(112, 41)
        Me._OptStatus_1.TabIndex = 20
        Me._OptStatus_1.TabStop = True
        Me._OptStatus_1.Text = "Look in            All DELIVERED Jobs"
        Me._OptStatus_1.UseVisualStyleBackColor = False
        '
        '_OptStatus_0
        '
        Me._OptStatus_0.BackColor = System.Drawing.Color.Gainsboro
        Me._OptStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_0, CType(0, Short))
        Me._OptStatus_0.Location = New System.Drawing.Point(457, 16)
        Me._OptStatus_0.Name = "_OptStatus_0"
        Me._OptStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_0.Size = New System.Drawing.Size(112, 41)
        Me._OptStatus_0.TabIndex = 19
        Me._OptStatus_0.TabStop = True
        Me._OptStatus_0.Text = "Look in      Current Jobs Only"
        Me._OptStatus_0.UseVisualStyleBackColor = False
        '
        'cmdJobPartClear
        '
        Me.cmdJobPartClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdJobPartClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdJobPartClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdJobPartClear.Location = New System.Drawing.Point(296, 120)
        Me.cmdJobPartClear.Name = "cmdJobPartClear"
        Me.cmdJobPartClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdJobPartClear.Size = New System.Drawing.Size(48, 25)
        Me.cmdJobPartClear.TabIndex = 18
        Me.cmdJobPartClear.Text = "Clear"
        Me.cmdJobPartClear.UseVisualStyleBackColor = False
        '
        'cboMonths
        '
        Me.cboMonths.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cboMonths.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMonths.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMonths.Location = New System.Drawing.Point(624, 84)
        Me.cboMonths.Name = "cboMonths"
        Me.cboMonths.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMonths.Size = New System.Drawing.Size(89, 21)
        Me.cboMonths.TabIndex = 9
        Me.cboMonths.Text = "cboMonths"
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(358, 120)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(60, 25)
        Me.cmdRefresh.TabIndex = 7
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'txtArg
        '
        Me.txtArg.AcceptsReturn = True
        Me.txtArg.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtArg.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtArg.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtArg.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtArg.Location = New System.Drawing.Point(48, 120)
        Me.txtArg.MaxLength = 0
        Me.txtArg.Name = "txtArg"
        Me.txtArg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtArg.Size = New System.Drawing.Size(241, 15)
        Me.txtArg.TabIndex = 6
        '
        'LabPeriod
        '
        Me.LabPeriod.BackColor = System.Drawing.Color.Transparent
        Me.LabPeriod.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPeriod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPeriod.Location = New System.Drawing.Point(456, 112)
        Me.LabPeriod.Name = "LabPeriod"
        Me.LabPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPeriod.Size = New System.Drawing.Size(239, 33)
        Me.LabPeriod.TabIndex = 8
        Me.LabPeriod.Text = "LabPeriod"
        '
        'LabPrompt
        '
        Me.LabPrompt.BackColor = System.Drawing.Color.Transparent
        Me.LabPrompt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPrompt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPrompt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPrompt.Location = New System.Drawing.Point(48, 64)
        Me.LabPrompt.Name = "LabPrompt"
        Me.LabPrompt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPrompt.Size = New System.Drawing.Size(241, 53)
        Me.LabPrompt.TabIndex = 5
        Me.LabPrompt.Text = "Text Search in Barcode,  SerialNo,  Cat1,   Item Description,    Customer and Sta" & _
    "ffName columns.."
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(24, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(265, 33)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Search forJob Parts that were used in jobs during the selected Period."
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(824, 8)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(49, 21)
        Me.cmdExit.TabIndex = 14
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.ListView1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(15, 30)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(329, 89)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'LabNoRecords
        '
        Me.LabNoRecords.BackColor = System.Drawing.SystemColors.Control
        Me.LabNoRecords.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabNoRecords.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabNoRecords.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabNoRecords.Location = New System.Drawing.Point(12, 17)
        Me.LabNoRecords.Name = "LabNoRecords"
        Me.LabNoRecords.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabNoRecords.Size = New System.Drawing.Size(169, 17)
        Me.LabNoRecords.TabIndex = 2
        Me.LabNoRecords.Text = "LabNoRecords"
        '
        'LabHdr
        '
        Me.LabHdr.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr.Location = New System.Drawing.Point(12, 9)
        Me.LabHdr.Name = "LabHdr"
        Me.LabHdr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr.Size = New System.Drawing.Size(233, 52)
        Me.LabHdr.TabIndex = 1
        Me.LabHdr.Text = "         JobTracking-                 Search Parts used in Jobs"
        '
        'OptStatus
        '
        '
        'frameListView
        '
        Me.frameListView.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameListView.Controls.Add(Me.ListView1)
        Me.frameListView.Controls.Add(Me.LabNoRecords)
        Me.frameListView.Location = New System.Drawing.Point(8, 227)
        Me.frameListView.Name = "frameListView"
        Me.frameListView.Size = New System.Drawing.Size(422, 125)
        Me.frameListView.TabIndex = 15
        Me.frameListView.TabStop = False
        Me.frameListView.Text = "frameListView"
        '
        'FrmFindPart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(895, 573)
        Me.Controls.Add(Me.frameListView)
        Me.Controls.Add(Me.FrameSerialAudit)
        Me.Controls.Add(Me.FrameJobParts)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.LabHdr)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "FrmFindPart"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Find Job Parts"
        Me.FrameSerialAudit.ResumeLayout(False)
        Me.FrameSerialAudit.PerformLayout()
        Me.FrameJobParts.ResumeLayout(False)
        Me.FrameJobParts.PerformLayout()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frameListView.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents frameListView As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSerialCancel As System.Windows.Forms.Button
#End Region
End Class