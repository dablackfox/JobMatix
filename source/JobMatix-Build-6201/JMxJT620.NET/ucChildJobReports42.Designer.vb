<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class ucChildJobReports42
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
    Public WithEvents _optReport_6 As System.Windows.Forms.RadioButton
    Public WithEvents ChkWarranty As System.Windows.Forms.CheckBox
    Public WithEvents _optReport_5 As System.Windows.Forms.RadioButton
    Public WithEvents _optReport_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optReport_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optReport_4 As System.Windows.Forms.RadioButton
    Public WithEvents _optReport_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optReport_0 As System.Windows.Forms.RadioButton
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents labChooseReport As System.Windows.Forms.Label
    Public WithEvents Line3 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Line2 As System.Windows.Forms.Label
    Public WithEvents Line1 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FrameReportType As System.Windows.Forms.GroupBox
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents cboPeriod As System.Windows.Forms.ComboBox
    Public WithEvents CboMonths As System.Windows.Forms.ComboBox
    Public WithEvents LabSelectMonth As System.Windows.Forms.Label
    Public WithEvents LabSelectPeriod As System.Windows.Forms.Label
    Public WithEvents FramePeriod As System.Windows.Forms.GroupBox
    Public WithEvents LabInclude As System.Windows.Forms.Label
    Public WithEvents FrameStatus As System.Windows.Forms.GroupBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents LabHdr0 As System.Windows.Forms.Label
    Public WithEvents LabDBName As System.Windows.Forms.Label
    Public WithEvents LabADmin As System.Windows.Forms.Label
    Public WithEvents LabReportName As System.Windows.Forms.Label
    Public WithEvents LabReportStatus As System.Windows.Forms.Label
    Public WithEvents LabHdr1 As System.Windows.Forms.Label
    Public WithEvents OptJobStatus As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optReport As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cboPrinters = New System.Windows.Forms.ComboBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.FrameReportType = New System.Windows.Forms.GroupBox()
        Me._optReport_6 = New System.Windows.Forms.RadioButton()
        Me.txtTestResults = New System.Windows.Forms.TextBox()
        Me.ChkWarranty = New System.Windows.Forms.CheckBox()
        Me._optReport_5 = New System.Windows.Forms.RadioButton()
        Me._optReport_3 = New System.Windows.Forms.RadioButton()
        Me._optReport_1 = New System.Windows.Forms.RadioButton()
        Me._optReport_4 = New System.Windows.Forms.RadioButton()
        Me._optReport_2 = New System.Windows.Forms.RadioButton()
        Me._optReport_0 = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.labChooseReport = New System.Windows.Forms.Label()
        Me.Line3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Line2 = New System.Windows.Forms.Label()
        Me.Line1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FramePeriod = New System.Windows.Forms.GroupBox()
        Me.panelDtPickers = New System.Windows.Forms.Panel()
        Me.labPickDates = New System.Windows.Forms.Label()
        Me.DTPickerTo = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DTPickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboPeriod = New System.Windows.Forms.ComboBox()
        Me.CboMonths = New System.Windows.Forms.ComboBox()
        Me.LabSelectMonth = New System.Windows.Forms.Label()
        Me.LabSelectPeriod = New System.Windows.Forms.Label()
        Me.FrameStatus = New System.Windows.Forms.GroupBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.panelStatusFiller1 = New System.Windows.Forms.Panel()
        Me.chkExcludeNilLabourJobs = New System.Windows.Forms.CheckBox()
        Me.optJobStatusDelivered = New System.Windows.Forms.RadioButton()
        Me.optJobStatusCompleted = New System.Windows.Forms.RadioButton()
        Me.optJobStatusCurrent = New System.Windows.Forms.RadioButton()
        Me.LabInclude = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabHdr0 = New System.Windows.Forms.Label()
        Me.LabDBName = New System.Windows.Forms.Label()
        Me.LabADmin = New System.Windows.Forms.Label()
        Me.LabReportName = New System.Windows.Forms.Label()
        Me.LabReportStatus = New System.Windows.Forms.Label()
        Me.LabHdr1 = New System.Windows.Forms.Label()
        Me.OptJobStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optReport = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GrpBoxPrinter = New System.Windows.Forms.GroupBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.grpBoxReportsMain = New System.Windows.Forms.GroupBox()
        Me.FrameReportType.SuspendLayout()
        Me.FramePeriod.SuspendLayout()
        Me.panelDtPickers.SuspendLayout()
        Me.FrameStatus.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.panelStatusFiller1.SuspendLayout()
        CType(Me.OptJobStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpBoxPrinter.SuspendLayout()
        Me.grpBoxReportsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdRefresh.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(48, 98)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(90, 46)
        Me.cmdRefresh.TabIndex = 1
        Me.cmdRefresh.Text = "Preview  >>"
        Me.ToolTip1.SetToolTip(Me.cmdRefresh, "Show Report..")
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'cboPrinters
        '
        Me.cboPrinters.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPrinters.FormattingEnabled = True
        Me.cboPrinters.Location = New System.Drawing.Point(14, 47)
        Me.cboPrinters.Name = "cboPrinters"
        Me.cboPrinters.Size = New System.Drawing.Size(215, 21)
        Me.cboPrinters.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cboPrinters, "A4 printer for RA Forms and Shipping Label.")
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(49, 174)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(90, 46)
        Me.cmdPrint.TabIndex = 2
        Me.cmdPrint.Text = "Print"
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Show Report..")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'FrameReportType
        '
        Me.FrameReportType.BackColor = System.Drawing.Color.Transparent
        Me.FrameReportType.Controls.Add(Me._optReport_6)
        Me.FrameReportType.Controls.Add(Me.txtTestResults)
        Me.FrameReportType.Controls.Add(Me.ChkWarranty)
        Me.FrameReportType.Controls.Add(Me._optReport_5)
        Me.FrameReportType.Controls.Add(Me._optReport_3)
        Me.FrameReportType.Controls.Add(Me._optReport_1)
        Me.FrameReportType.Controls.Add(Me._optReport_4)
        Me.FrameReportType.Controls.Add(Me._optReport_2)
        Me.FrameReportType.Controls.Add(Me._optReport_0)
        Me.FrameReportType.Controls.Add(Me.Label9)
        Me.FrameReportType.Controls.Add(Me.labChooseReport)
        Me.FrameReportType.Controls.Add(Me.Line3)
        Me.FrameReportType.Controls.Add(Me.Label8)
        Me.FrameReportType.Controls.Add(Me.Line2)
        Me.FrameReportType.Controls.Add(Me.Line1)
        Me.FrameReportType.Controls.Add(Me.Label6)
        Me.FrameReportType.Controls.Add(Me.Label5)
        Me.FrameReportType.Controls.Add(Me.Label4)
        Me.FrameReportType.Controls.Add(Me.Label3)
        Me.FrameReportType.Controls.Add(Me.Label2)
        Me.FrameReportType.Controls.Add(Me.Label1)
        Me.FrameReportType.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameReportType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameReportType.Location = New System.Drawing.Point(32, 68)
        Me.FrameReportType.Name = "FrameReportType"
        Me.FrameReportType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameReportType.Size = New System.Drawing.Size(293, 530)
        Me.FrameReportType.TabIndex = 3
        Me.FrameReportType.TabStop = False
        '
        '_optReport_6
        '
        Me._optReport_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me._optReport_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_6.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.optReport.SetIndex(Me._optReport_6, CType(6, Short))
        Me._optReport_6.Location = New System.Drawing.Point(24, 437)
        Me._optReport_6.Name = "_optReport_6"
        Me._optReport_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_6.Size = New System.Drawing.Size(145, 25)
        Me._optReport_6.TabIndex = 7
        Me._optReport_6.TabStop = True
        Me._optReport_6.Text = "Jobs/Session times"
        Me._optReport_6.UseVisualStyleBackColor = False
        '
        'txtTestResults
        '
        Me.txtTestResults.Font = New System.Drawing.Font("Lucida Console", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTestResults.Location = New System.Drawing.Point(24, 475)
        Me.txtTestResults.Multiline = True
        Me.txtTestResults.Name = "txtTestResults"
        Me.txtTestResults.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtTestResults.Size = New System.Drawing.Size(227, 41)
        Me.txtTestResults.TabIndex = 8
        '
        'ChkWarranty
        '
        Me.ChkWarranty.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ChkWarranty.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkWarranty.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkWarranty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkWarranty.Location = New System.Drawing.Point(167, 193)
        Me.ChkWarranty.Name = "ChkWarranty"
        Me.ChkWarranty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkWarranty.Size = New System.Drawing.Size(80, 29)
        Me.ChkWarranty.TabIndex = 4
        Me.ChkWarranty.Text = "Warranty Parts Only"
        Me.ChkWarranty.UseVisualStyleBackColor = False
        '
        '_optReport_5
        '
        Me._optReport_5.BackColor = System.Drawing.Color.WhiteSmoke
        Me._optReport_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_5.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optReport.SetIndex(Me._optReport_5, CType(5, Short))
        Me._optReport_5.Location = New System.Drawing.Point(24, 330)
        Me._optReport_5.Name = "_optReport_5"
        Me._optReport_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_5.Size = New System.Drawing.Size(113, 25)
        Me._optReport_5.TabIndex = 6
        Me._optReport_5.TabStop = True
        Me._optReport_5.Text = "Detailed Report"
        Me._optReport_5.UseVisualStyleBackColor = False
        '
        '_optReport_3
        '
        Me._optReport_3.BackColor = System.Drawing.Color.WhiteSmoke
        Me._optReport_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_3.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optReport.SetIndex(Me._optReport_3, CType(3, Short))
        Me._optReport_3.Location = New System.Drawing.Point(24, 197)
        Me._optReport_3.Name = "_optReport_3"
        Me._optReport_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_3.Size = New System.Drawing.Size(113, 25)
        Me._optReport_3.TabIndex = 3
        Me._optReport_3.TabStop = True
        Me._optReport_3.Text = "Detailed Report"
        Me._optReport_3.UseVisualStyleBackColor = False
        '
        '_optReport_1
        '
        Me._optReport_1.BackColor = System.Drawing.Color.WhiteSmoke
        Me._optReport_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_1.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optReport.SetIndex(Me._optReport_1, CType(1, Short))
        Me._optReport_1.Location = New System.Drawing.Point(24, 96)
        Me._optReport_1.Name = "_optReport_1"
        Me._optReport_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_1.Size = New System.Drawing.Size(113, 25)
        Me._optReport_1.TabIndex = 1
        Me._optReport_1.TabStop = True
        Me._optReport_1.Text = "Detailed Report"
        Me._optReport_1.UseVisualStyleBackColor = False
        '
        '_optReport_4
        '
        Me._optReport_4.BackColor = System.Drawing.Color.WhiteSmoke
        Me._optReport_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_4.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optReport.SetIndex(Me._optReport_4, CType(4, Short))
        Me._optReport_4.Location = New System.Drawing.Point(24, 306)
        Me._optReport_4.Name = "_optReport_4"
        Me._optReport_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_4.Size = New System.Drawing.Size(113, 25)
        Me._optReport_4.TabIndex = 5
        Me._optReport_4.TabStop = True
        Me._optReport_4.Text = "Summary Report"
        Me._optReport_4.UseVisualStyleBackColor = False
        '
        '_optReport_2
        '
        Me._optReport_2.BackColor = System.Drawing.Color.WhiteSmoke
        Me._optReport_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_2.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optReport.SetIndex(Me._optReport_2, CType(2, Short))
        Me._optReport_2.Location = New System.Drawing.Point(24, 173)
        Me._optReport_2.Name = "_optReport_2"
        Me._optReport_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_2.Size = New System.Drawing.Size(113, 25)
        Me._optReport_2.TabIndex = 2
        Me._optReport_2.TabStop = True
        Me._optReport_2.Text = "Summary Report"
        Me._optReport_2.UseVisualStyleBackColor = False
        '
        '_optReport_0
        '
        Me._optReport_0.BackColor = System.Drawing.Color.WhiteSmoke
        Me._optReport_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optReport_0.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optReport_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optReport.SetIndex(Me._optReport_0, CType(0, Short))
        Me._optReport_0.Location = New System.Drawing.Point(24, 72)
        Me._optReport_0.Name = "_optReport_0"
        Me._optReport_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optReport_0.Size = New System.Drawing.Size(113, 25)
        Me._optReport_0.TabIndex = 0
        Me._optReport_0.TabStop = True
        Me._optReport_0.Text = "Summary Report"
        Me._optReport_0.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(164, 389)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(113, 44)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Lists all session times logged against Jobs in selected Period."
        '
        'labChooseReport
        '
        Me.labChooseReport.BackColor = System.Drawing.Color.Transparent
        Me.labChooseReport.Cursor = System.Windows.Forms.Cursors.Default
        Me.labChooseReport.Font = New System.Drawing.Font("Tahoma", 12.0!, CType((System.Drawing.FontStyle.Italic Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labChooseReport.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.labChooseReport.Location = New System.Drawing.Point(16, 16)
        Me.labChooseReport.Name = "labChooseReport"
        Me.labChooseReport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labChooseReport.Size = New System.Drawing.Size(177, 25)
        Me.labChooseReport.TabIndex = 38
        Me.labChooseReport.Text = "Choose Report Type:"
        '
        'Line3
        '
        Me.Line3.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Line3.Location = New System.Drawing.Point(16, 130)
        Me.Line3.Name = "Line3"
        Me.Line3.Size = New System.Drawing.Size(240, 1)
        Me.Line3.TabIndex = 40
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(24, 397)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(134, 33)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Daily Staff Timesheets-Report"
        '
        'Line2
        '
        Me.Line2.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Line2.Location = New System.Drawing.Point(16, 370)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(240, 1)
        Me.Line2.TabIndex = 41
        '
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Line1.Location = New System.Drawing.Point(16, 239)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(240, 1)
        Me.Line1.TabIndex = 42
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(164, 265)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(113, 48)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Lists all Staff with Jobs completed in the selected Period."
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(164, 141)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(113, 52)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Lists all Parts used inJobs  for selected Job Status"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(164, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(105, 57)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Lists all Jobs and attached parts for selected Job Status"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(16, 266)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(99, 33)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Staff/Jobs Report"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(16, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(113, 17)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Parts Report"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(24, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(101, 17)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Jobs Report"
        '
        'FramePeriod
        '
        Me.FramePeriod.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FramePeriod.Controls.Add(Me.panelDtPickers)
        Me.FramePeriod.Controls.Add(Me.cboPeriod)
        Me.FramePeriod.Controls.Add(Me.CboMonths)
        Me.FramePeriod.Controls.Add(Me.LabSelectMonth)
        Me.FramePeriod.Controls.Add(Me.LabSelectPeriod)
        Me.FramePeriod.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FramePeriod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FramePeriod.Location = New System.Drawing.Point(390, 337)
        Me.FramePeriod.Name = "FramePeriod"
        Me.FramePeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FramePeriod.Size = New System.Drawing.Size(289, 261)
        Me.FramePeriod.TabIndex = 5
        Me.FramePeriod.TabStop = False
        Me.FramePeriod.Text = "Reporting Period"
        '
        'panelDtPickers
        '
        Me.panelDtPickers.Controls.Add(Me.labPickDates)
        Me.panelDtPickers.Controls.Add(Me.DTPickerTo)
        Me.panelDtPickers.Controls.Add(Me.Label10)
        Me.panelDtPickers.Controls.Add(Me.DTPickerFrom)
        Me.panelDtPickers.Controls.Add(Me.Label11)
        Me.panelDtPickers.Location = New System.Drawing.Point(19, 105)
        Me.panelDtPickers.Name = "panelDtPickers"
        Me.panelDtPickers.Size = New System.Drawing.Size(239, 96)
        Me.panelDtPickers.TabIndex = 2
        '
        'labPickDates
        '
        Me.labPickDates.BackColor = System.Drawing.Color.Transparent
        Me.labPickDates.Cursor = System.Windows.Forms.Cursors.Default
        Me.labPickDates.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPickDates.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labPickDates.Location = New System.Drawing.Point(12, 19)
        Me.labPickDates.Name = "labPickDates"
        Me.labPickDates.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labPickDates.Size = New System.Drawing.Size(146, 17)
        Me.labPickDates.TabIndex = 33
        Me.labPickDates.Text = "Pick Period Start/End date.."
        '
        'DTPickerTo
        '
        Me.DTPickerTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPickerTo.Location = New System.Drawing.Point(128, 62)
        Me.DTPickerTo.Name = "DTPickerTo"
        Me.DTPickerTo.Size = New System.Drawing.Size(97, 21)
        Me.DTPickerTo.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(17, 42)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(79, 15)
        Me.Label10.TabIndex = 31
        Me.Label10.Text = "Period From"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DTPickerFrom
        '
        Me.DTPickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPickerFrom.Location = New System.Drawing.Point(20, 61)
        Me.DTPickerFrom.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DTPickerFrom.Name = "DTPickerFrom"
        Me.DTPickerFrom.Size = New System.Drawing.Size(97, 21)
        Me.DTPickerFrom.TabIndex = 0
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(126, 43)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(29, 13)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "To"
        '
        'cboPeriod
        '
        Me.cboPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cboPeriod.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.cboPeriod.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPeriod.Location = New System.Drawing.Point(19, 50)
        Me.cboPeriod.Name = "cboPeriod"
        Me.cboPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPeriod.Size = New System.Drawing.Size(129, 21)
        Me.cboPeriod.TabIndex = 0
        '
        'CboMonths
        '
        Me.CboMonths.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.CboMonths.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboMonths.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboMonths.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboMonths.Location = New System.Drawing.Point(159, 50)
        Me.CboMonths.Name = "CboMonths"
        Me.CboMonths.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboMonths.Size = New System.Drawing.Size(91, 21)
        Me.CboMonths.TabIndex = 1
        '
        'LabSelectMonth
        '
        Me.LabSelectMonth.BackColor = System.Drawing.Color.Transparent
        Me.LabSelectMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabSelectMonth.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSelectMonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabSelectMonth.Location = New System.Drawing.Point(164, 30)
        Me.LabSelectMonth.Name = "LabSelectMonth"
        Me.LabSelectMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabSelectMonth.Size = New System.Drawing.Size(80, 17)
        Me.LabSelectMonth.TabIndex = 28
        Me.LabSelectMonth.Text = "- Month -"
        '
        'LabSelectPeriod
        '
        Me.LabSelectPeriod.BackColor = System.Drawing.Color.Transparent
        Me.LabSelectPeriod.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabSelectPeriod.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSelectPeriod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabSelectPeriod.Location = New System.Drawing.Point(22, 30)
        Me.LabSelectPeriod.Name = "LabSelectPeriod"
        Me.LabSelectPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabSelectPeriod.Size = New System.Drawing.Size(97, 17)
        Me.LabSelectPeriod.TabIndex = 15
        Me.LabSelectPeriod.Text = "-Select Period-"
        '
        'FrameStatus
        '
        Me.FrameStatus.BackColor = System.Drawing.Color.Transparent
        Me.FrameStatus.Controls.Add(Me.Panel2)
        Me.FrameStatus.Controls.Add(Me.Panel1)
        Me.FrameStatus.Controls.Add(Me.panelStatusFiller1)
        Me.FrameStatus.Controls.Add(Me.optJobStatusDelivered)
        Me.FrameStatus.Controls.Add(Me.optJobStatusCompleted)
        Me.FrameStatus.Controls.Add(Me.optJobStatusCurrent)
        Me.FrameStatus.Controls.Add(Me.LabInclude)
        Me.FrameStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameStatus.Location = New System.Drawing.Point(390, 116)
        Me.FrameStatus.Name = "FrameStatus"
        Me.FrameStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameStatus.Size = New System.Drawing.Size(447, 172)
        Me.FrameStatus.TabIndex = 4
        Me.FrameStatus.TabStop = False
        Me.FrameStatus.Text = "FrameStatus"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Location = New System.Drawing.Point(301, 112)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(129, 44)
        Me.Panel2.TabIndex = 20
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Silver
        Me.Label13.Location = New System.Drawing.Point(8, 18)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(112, 8)
        Me.Label13.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Location = New System.Drawing.Point(19, 112)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(129, 44)
        Me.Panel1.TabIndex = 19
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Silver
        Me.Label12.Location = New System.Drawing.Point(7, 17)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(112, 8)
        Me.Label12.TabIndex = 0
        '
        'panelStatusFiller1
        '
        Me.panelStatusFiller1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelStatusFiller1.Controls.Add(Me.chkExcludeNilLabourJobs)
        Me.panelStatusFiller1.Location = New System.Drawing.Point(160, 112)
        Me.panelStatusFiller1.Name = "panelStatusFiller1"
        Me.panelStatusFiller1.Size = New System.Drawing.Size(129, 44)
        Me.panelStatusFiller1.TabIndex = 18
        '
        'chkExcludeNilLabourJobs
        '
        Me.chkExcludeNilLabourJobs.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExcludeNilLabourJobs.Location = New System.Drawing.Point(6, 4)
        Me.chkExcludeNilLabourJobs.Name = "chkExcludeNilLabourJobs"
        Me.chkExcludeNilLabourJobs.Size = New System.Drawing.Size(117, 29)
        Me.chkExcludeNilLabourJobs.TabIndex = 2
        Me.chkExcludeNilLabourJobs.Text = "Exclude Jobs with no Labour value.."
        Me.chkExcludeNilLabourJobs.UseVisualStyleBackColor = True
        '
        'optJobStatusDelivered
        '
        Me.optJobStatusDelivered.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optJobStatusDelivered.Cursor = System.Windows.Forms.Cursors.Default
        Me.optJobStatusDelivered.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optJobStatusDelivered.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optJobStatusDelivered.Location = New System.Drawing.Point(301, 52)
        Me.optJobStatusDelivered.Name = "optJobStatusDelivered"
        Me.optJobStatusDelivered.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.optJobStatusDelivered.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optJobStatusDelivered.Size = New System.Drawing.Size(129, 60)
        Me.optJobStatusDelivered.TabIndex = 17
        Me.optJobStatusDelivered.TabStop = True
        Me.optJobStatusDelivered.Text = "Delivered Jobs (Select Period)"
        Me.optJobStatusDelivered.UseVisualStyleBackColor = False
        '
        'optJobStatusCompleted
        '
        Me.optJobStatusCompleted.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optJobStatusCompleted.Cursor = System.Windows.Forms.Cursors.Default
        Me.optJobStatusCompleted.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optJobStatusCompleted.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optJobStatusCompleted.Location = New System.Drawing.Point(160, 52)
        Me.optJobStatusCompleted.Name = "optJobStatusCompleted"
        Me.optJobStatusCompleted.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.optJobStatusCompleted.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optJobStatusCompleted.Size = New System.Drawing.Size(129, 60)
        Me.optJobStatusCompleted.TabIndex = 1
        Me.optJobStatusCompleted.TabStop = True
        Me.optJobStatusCompleted.Text = "Jobs Completed in period.. (Whether delivered or not..)"
        Me.optJobStatusCompleted.UseVisualStyleBackColor = False
        '
        'optJobStatusCurrent
        '
        Me.optJobStatusCurrent.BackColor = System.Drawing.Color.WhiteSmoke
        Me.optJobStatusCurrent.Cursor = System.Windows.Forms.Cursors.Default
        Me.optJobStatusCurrent.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optJobStatusCurrent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optJobStatusCurrent.Location = New System.Drawing.Point(19, 52)
        Me.optJobStatusCurrent.Name = "optJobStatusCurrent"
        Me.optJobStatusCurrent.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.optJobStatusCurrent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optJobStatusCurrent.Size = New System.Drawing.Size(129, 60)
        Me.optJobStatusCurrent.TabIndex = 0
        Me.optJobStatusCurrent.TabStop = True
        Me.optJobStatusCurrent.Text = "All Current Jobs (On-Bench and Awaiting Delivery)"
        Me.optJobStatusCurrent.UseVisualStyleBackColor = False
        '
        'LabInclude
        '
        Me.LabInclude.BackColor = System.Drawing.Color.Transparent
        Me.LabInclude.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabInclude.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabInclude.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabInclude.Location = New System.Drawing.Point(16, 18)
        Me.LabInclude.Name = "LabInclude"
        Me.LabInclude.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabInclude.Size = New System.Drawing.Size(192, 30)
        Me.LabInclude.TabIndex = 14
        Me.LabInclude.Text = "Include only those Job records with Selected Job Status "
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(717, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(97, 17)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "Database"
        '
        'LabHdr0
        '
        Me.LabHdr0.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr0.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr0.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr0.ForeColor = System.Drawing.Color.DarkGray
        Me.LabHdr0.Location = New System.Drawing.Point(17, 24)
        Me.LabHdr0.Name = "LabHdr0"
        Me.LabHdr0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr0.Size = New System.Drawing.Size(262, 41)
        Me.LabHdr0.TabIndex = 23
        Me.LabHdr0.Text = "JobMatix-  Job Reports"
        '
        'LabDBName
        '
        Me.LabDBName.BackColor = System.Drawing.Color.Transparent
        Me.LabDBName.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDBName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDBName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDBName.Location = New System.Drawing.Point(717, 55)
        Me.LabDBName.Name = "LabDBName"
        Me.LabDBName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDBName.Size = New System.Drawing.Size(245, 22)
        Me.LabDBName.TabIndex = 21
        Me.LabDBName.Text = "labDBname"
        '
        'LabADmin
        '
        Me.LabADmin.BackColor = System.Drawing.Color.Transparent
        Me.LabADmin.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabADmin.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabADmin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabADmin.Location = New System.Drawing.Point(856, 32)
        Me.LabADmin.Name = "LabADmin"
        Me.LabADmin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabADmin.Size = New System.Drawing.Size(91, 17)
        Me.LabADmin.TabIndex = 20
        Me.LabADmin.Text = "LabAdmin"
        '
        'LabReportName
        '
        Me.LabReportName.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabReportName.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabReportName.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabReportName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabReportName.Location = New System.Drawing.Point(389, 17)
        Me.LabReportName.Name = "LabReportName"
        Me.LabReportName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabReportName.Size = New System.Drawing.Size(302, 45)
        Me.LabReportName.TabIndex = 16
        Me.LabReportName.Text = "LabReportName"
        Me.LabReportName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabReportStatus
        '
        Me.LabReportStatus.BackColor = System.Drawing.Color.Transparent
        Me.LabReportStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabReportStatus.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabReportStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabReportStatus.Location = New System.Drawing.Point(387, 291)
        Me.LabReportStatus.Name = "LabReportStatus"
        Me.LabReportStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabReportStatus.Size = New System.Drawing.Size(331, 33)
        Me.LabReportStatus.TabIndex = 13
        Me.LabReportStatus.Text = "LabReportStatus"
        '
        'LabHdr1
        '
        Me.LabHdr1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabHdr1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr1.Location = New System.Drawing.Point(387, 80)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr1.Size = New System.Drawing.Size(442, 25)
        Me.LabHdr1.TabIndex = 12
        Me.LabHdr1.Text = "LabHdr1"
        '
        'optReport
        '
        '
        'GrpBoxPrinter
        '
        Me.GrpBoxPrinter.Controls.Add(Me.cmdPrint)
        Me.GrpBoxPrinter.Controls.Add(Me.Label21)
        Me.GrpBoxPrinter.Controls.Add(Me.cboPrinters)
        Me.GrpBoxPrinter.Controls.Add(Me.cmdRefresh)
        Me.GrpBoxPrinter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBoxPrinter.Location = New System.Drawing.Point(691, 334)
        Me.GrpBoxPrinter.Name = "GrpBoxPrinter"
        Me.GrpBoxPrinter.Size = New System.Drawing.Size(252, 264)
        Me.GrpBoxPrinter.TabIndex = 6
        Me.GrpBoxPrinter.TabStop = False
        Me.GrpBoxPrinter.Text = "Printer"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Linen
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Blue
        Me.Label21.Location = New System.Drawing.Point(22, 31)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(98, 13)
        Me.Label21.TabIndex = 102
        Me.Label21.Text = "-- A4 Printer --"
        '
        'grpBoxReportsMain
        '
        Me.grpBoxReportsMain.BackColor = System.Drawing.Color.White
        Me.grpBoxReportsMain.Controls.Add(Me.LabHdr0)
        Me.grpBoxReportsMain.Controls.Add(Me.GrpBoxPrinter)
        Me.grpBoxReportsMain.Controls.Add(Me.LabReportName)
        Me.grpBoxReportsMain.Controls.Add(Me.FramePeriod)
        Me.grpBoxReportsMain.Controls.Add(Me.FrameReportType)
        Me.grpBoxReportsMain.Controls.Add(Me.LabReportStatus)
        Me.grpBoxReportsMain.Controls.Add(Me.FrameStatus)
        Me.grpBoxReportsMain.Controls.Add(Me.LabADmin)
        Me.grpBoxReportsMain.Controls.Add(Me.LabHdr1)
        Me.grpBoxReportsMain.Controls.Add(Me.LabDBName)
        Me.grpBoxReportsMain.Controls.Add(Me.Label7)
        Me.grpBoxReportsMain.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxReportsMain.Location = New System.Drawing.Point(4, 5)
        Me.grpBoxReportsMain.Name = "grpBoxReportsMain"
        Me.grpBoxReportsMain.Size = New System.Drawing.Size(981, 612)
        Me.grpBoxReportsMain.TabIndex = 36
        Me.grpBoxReportsMain.TabStop = False
        Me.grpBoxReportsMain.Text = "grpBoxReportsMain"
        '
        'ucChildJobReports42
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.Controls.Add(Me.grpBoxReportsMain)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "ucChildJobReports42"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Size = New System.Drawing.Size(1000, 638)
        Me.FrameReportType.ResumeLayout(False)
        Me.FrameReportType.PerformLayout()
        Me.FramePeriod.ResumeLayout(False)
        Me.panelDtPickers.ResumeLayout(False)
        Me.FrameStatus.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.panelStatusFiller1.ResumeLayout(False)
        CType(Me.OptJobStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpBoxPrinter.ResumeLayout(False)
        Me.grpBoxReportsMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTestResults As System.Windows.Forms.TextBox
    Public WithEvents optJobStatusDelivered As System.Windows.Forms.RadioButton
    Public WithEvents optJobStatusCompleted As System.Windows.Forms.RadioButton
    Public WithEvents optJobStatusCurrent As System.Windows.Forms.RadioButton
    Friend WithEvents GrpBoxPrinter As System.Windows.Forms.GroupBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents panelDtPickers As System.Windows.Forms.Panel
    Friend WithEvents DTPickerTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DTPickerFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents labPickDates As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents panelStatusFiller1 As System.Windows.Forms.Panel
    Friend WithEvents chkExcludeNilLabourJobs As System.Windows.Forms.CheckBox
    Friend WithEvents grpBoxReportsMain As GroupBox
#End Region
End Class