<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCustHistory
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
    Public WithEvents cmdFullBrowse As System.Windows.Forms.Button
	Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents labRecCount As System.Windows.Forms.Label
	Public WithEvents LabFind As System.Windows.Forms.Label
	Public WithEvents FrameBrowse As System.Windows.Forms.GroupBox
	Public WithEvents txtCustNo As System.Windows.Forms.TextBox
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents Timer1 As System.Windows.Forms.Timer
	Public WithEvents ListViewPurchases As System.Windows.Forms.ListView
	Public WithEvents ListViewJobs As System.Windows.Forms.ListView
	Public WithEvents LabJobHistory As System.Windows.Forms.Label
	Public WithEvents LabPurchases As System.Windows.Forms.Label
	Public WithEvents frameHistory As System.Windows.Forms.GroupBox
	Public WithEvents ListViewParts As System.Windows.Forms.ListView
	Public WithEvents txtWorkHistory As System.Windows.Forms.TextBox
	Public WithEvents txtSymptoms As System.Windows.Forms.TextBox
	Public WithEvents txtGoods As System.Windows.Forms.TextBox
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents LabWorkHistory As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents FrameJobDetails As System.Windows.Forms.GroupBox
	Public WithEvents txtCustFax As System.Windows.Forms.TextBox
	Public WithEvents txtCustEmail As System.Windows.Forms.TextBox
	Public WithEvents txtCustABN As System.Windows.Forms.TextBox
	Public WithEvents txtCustAddress As System.Windows.Forms.TextBox
	Public WithEvents txtCustCompany As System.Windows.Forms.TextBox
	Public WithEvents txtCustMobile As System.Windows.Forms.TextBox
	Public WithEvents txtCustPhone As System.Windows.Forms.TextBox
	Public WithEvents txtCustName As System.Windows.Forms.TextBox
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents LabCompany As System.Windows.Forms.Label
	Public WithEvents LabMobile As System.Windows.Forms.Label
	Public WithEvents LabPhone As System.Windows.Forms.Label
	Public WithEvents LabName As System.Windows.Forms.Label
	Public WithEvents FrameCust As System.Windows.Forms.GroupBox
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents LabVersion As System.Windows.Forms.Label
	Public WithEvents labBusiness As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtCustNo = New System.Windows.Forms.TextBox
        Me.txtCustCompany = New System.Windows.Forms.TextBox
        Me.txtCustName = New System.Windows.Forms.TextBox
        Me.FrameBrowse = New System.Windows.Forms.GroupBox
        Me.DataGridViewHost = New System.Windows.Forms.DataGridView
        Me.cmdFullBrowse = New System.Windows.Forms.Button
        Me.txtFind = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.labRecCount = New System.Windows.Forms.Label
        Me.LabFind = New System.Windows.Forms.Label
        Me.cmdClose = New System.Windows.Forms.Button
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.frameHistory = New System.Windows.Forms.GroupBox
        Me.ListViewPurchases = New System.Windows.Forms.ListView
        Me.ListViewJobs = New System.Windows.Forms.ListView
        Me.LabJobHistory = New System.Windows.Forms.Label
        Me.LabPurchases = New System.Windows.Forms.Label
        Me.FrameJobDetails = New System.Windows.Forms.GroupBox
        Me.ListViewParts = New System.Windows.Forms.ListView
        Me.txtWorkHistory = New System.Windows.Forms.TextBox
        Me.txtSymptoms = New System.Windows.Forms.TextBox
        Me.txtGoods = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.LabWorkHistory = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.FrameCust = New System.Windows.Forms.GroupBox
        Me.txtCustFax = New System.Windows.Forms.TextBox
        Me.txtCustEmail = New System.Windows.Forms.TextBox
        Me.txtCustABN = New System.Windows.Forms.TextBox
        Me.txtCustAddress = New System.Windows.Forms.TextBox
        Me.txtCustMobile = New System.Windows.Forms.TextBox
        Me.txtCustPhone = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.LabCompany = New System.Windows.Forms.Label
        Me.LabMobile = New System.Windows.Forms.Label
        Me.LabPhone = New System.Windows.Forms.Label
        Me.LabName = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.LabVersion = New System.Windows.Forms.Label
        Me.labBusiness = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.FrameBrowse.SuspendLayout()
        CType(Me.DataGridViewHost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frameHistory.SuspendLayout()
        Me.FrameJobDetails.SuspendLayout()
        Me.FrameCust.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCustNo
        '
        Me.txtCustNo.AcceptsReturn = True
        Me.txtCustNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustNo.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustNo.Location = New System.Drawing.Point(116, 48)
        Me.txtCustNo.MaxLength = 0
        Me.txtCustNo.Name = "txtCustNo"
        Me.txtCustNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustNo.Size = New System.Drawing.Size(78, 14)
        Me.txtCustNo.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.txtCustNo, "Click here to lookup customer..")
        '
        'txtCustCompany
        '
        Me.txtCustCompany.AcceptsReturn = True
        Me.txtCustCompany.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustCompany.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustCompany.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustCompany.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustCompany.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustCompany.Location = New System.Drawing.Point(72, 24)
        Me.txtCustCompany.MaxLength = 0
        Me.txtCustCompany.Multiline = True
        Me.txtCustCompany.Name = "txtCustCompany"
        Me.txtCustCompany.ReadOnly = True
        Me.txtCustCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustCompany.Size = New System.Drawing.Size(257, 37)
        Me.txtCustCompany.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtCustCompany, "Click here to lookup customer..")
        '
        'txtCustName
        '
        Me.txtCustName.AcceptsReturn = True
        Me.txtCustName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustName.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustName.Location = New System.Drawing.Point(72, 64)
        Me.txtCustName.MaxLength = 0
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.ReadOnly = True
        Me.txtCustName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustName.Size = New System.Drawing.Size(257, 18)
        Me.txtCustName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtCustName, "Click here to lookup customer..")
        '
        'FrameBrowse
        '
        Me.FrameBrowse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameBrowse.Controls.Add(Me.DataGridViewHost)
        Me.FrameBrowse.Controls.Add(Me.cmdFullBrowse)
        Me.FrameBrowse.Controls.Add(Me.txtFind)
        Me.FrameBrowse.Controls.Add(Me.Label4)
        Me.FrameBrowse.Controls.Add(Me.labRecCount)
        Me.FrameBrowse.Controls.Add(Me.LabFind)
        Me.FrameBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameBrowse.Location = New System.Drawing.Point(5, 88)
        Me.FrameBrowse.Name = "FrameBrowse"
        Me.FrameBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameBrowse.Size = New System.Drawing.Size(585, 249)
        Me.FrameBrowse.TabIndex = 38
        Me.FrameBrowse.TabStop = False
        Me.FrameBrowse.Text = "FrameBrowse"
        '
        'DataGridViewHost
        '
        Me.DataGridViewHost.AllowUserToAddRows = False
        Me.DataGridViewHost.AllowUserToDeleteRows = False
        Me.DataGridViewHost.BackgroundColor = System.Drawing.Color.White
        Me.DataGridViewHost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewHost.Location = New System.Drawing.Point(11, 45)
        Me.DataGridViewHost.MultiSelect = False
        Me.DataGridViewHost.Name = "DataGridViewHost"
        Me.DataGridViewHost.ReadOnly = True
        Me.DataGridViewHost.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewHost.Size = New System.Drawing.Size(565, 195)
        Me.DataGridViewHost.StandardTab = True
        Me.DataGridViewHost.TabIndex = 47
        '
        'cmdFullBrowse
        '
        Me.cmdFullBrowse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdFullBrowse.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFullBrowse.Font = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFullBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFullBrowse.Location = New System.Drawing.Point(384, 24)
        Me.cmdFullBrowse.Name = "cmdFullBrowse"
        Me.cmdFullBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFullBrowse.Size = New System.Drawing.Size(33, 17)
        Me.cmdFullBrowse.TabIndex = 43
        Me.cmdFullBrowse.Text = ">>"
        Me.cmdFullBrowse.UseVisualStyleBackColor = False
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(104, 24)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(89, 15)
        Me.txtFind.TabIndex = 40
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(282, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(96, 17)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "Full Browse Form"
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(472, 24)
        Me.labRecCount.Name = "labRecCount"
        Me.labRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCount.Size = New System.Drawing.Size(104, 17)
        Me.labRecCount.TabIndex = 41
        Me.labRecCount.Text = "labRecCount"
        '
        'LabFind
        '
        Me.LabFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabFind.Location = New System.Drawing.Point(8, 16)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(89, 25)
        Me.LabFind.TabIndex = 39
        Me.LabFind.Text = "LabFind"
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.CausesValidation = False
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(856, 8)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(57, 17)
        Me.cmdClose.TabIndex = 29
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'frameHistory
        '
        Me.frameHistory.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameHistory.Controls.Add(Me.ListViewPurchases)
        Me.frameHistory.Controls.Add(Me.ListViewJobs)
        Me.frameHistory.Controls.Add(Me.LabJobHistory)
        Me.frameHistory.Controls.Add(Me.LabPurchases)
        Me.frameHistory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameHistory.Location = New System.Drawing.Point(8, 328)
        Me.frameHistory.Name = "frameHistory"
        Me.frameHistory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameHistory.Size = New System.Drawing.Size(585, 353)
        Me.frameHistory.TabIndex = 16
        Me.frameHistory.TabStop = False
        Me.frameHistory.Text = "frameHistory"
        '
        'ListViewPurchases
        '
        Me.ListViewPurchases.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.ListViewPurchases.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ListViewPurchases.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewPurchases.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewPurchases.FullRowSelect = True
        Me.ListViewPurchases.Location = New System.Drawing.Point(8, 184)
        Me.ListViewPurchases.Name = "ListViewPurchases"
        Me.ListViewPurchases.Size = New System.Drawing.Size(569, 161)
        Me.ListViewPurchases.TabIndex = 18
        Me.ListViewPurchases.UseCompatibleStateImageBehavior = False
        Me.ListViewPurchases.View = System.Windows.Forms.View.Details
        '
        'ListViewJobs
        '
        Me.ListViewJobs.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.ListViewJobs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ListViewJobs.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewJobs.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewJobs.FullRowSelect = True
        Me.ListViewJobs.Location = New System.Drawing.Point(8, 32)
        Me.ListViewJobs.Name = "ListViewJobs"
        Me.ListViewJobs.Size = New System.Drawing.Size(569, 121)
        Me.ListViewJobs.TabIndex = 20
        Me.ListViewJobs.UseCompatibleStateImageBehavior = False
        Me.ListViewJobs.View = System.Windows.Forms.View.Details
        '
        'LabJobHistory
        '
        Me.LabJobHistory.BackColor = System.Drawing.Color.Transparent
        Me.LabJobHistory.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabJobHistory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabJobHistory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobHistory.Location = New System.Drawing.Point(8, 16)
        Me.LabJobHistory.Name = "LabJobHistory"
        Me.LabJobHistory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabJobHistory.Size = New System.Drawing.Size(289, 17)
        Me.LabJobHistory.TabIndex = 19
        Me.LabJobHistory.Text = "Job Work History"
        '
        'LabPurchases
        '
        Me.LabPurchases.BackColor = System.Drawing.Color.Transparent
        Me.LabPurchases.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPurchases.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPurchases.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPurchases.Location = New System.Drawing.Point(8, 168)
        Me.LabPurchases.Name = "LabPurchases"
        Me.LabPurchases.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPurchases.Size = New System.Drawing.Size(337, 17)
        Me.LabPurchases.TabIndex = 17
        Me.LabPurchases.Text = "Purchase History (Retail Manager Docket Lines)"
        '
        'FrameJobDetails
        '
        Me.FrameJobDetails.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameJobDetails.Controls.Add(Me.ListViewParts)
        Me.FrameJobDetails.Controls.Add(Me.txtWorkHistory)
        Me.FrameJobDetails.Controls.Add(Me.txtSymptoms)
        Me.FrameJobDetails.Controls.Add(Me.txtGoods)
        Me.FrameJobDetails.Controls.Add(Me.Label8)
        Me.FrameJobDetails.Controls.Add(Me.LabWorkHistory)
        Me.FrameJobDetails.Controls.Add(Me.Label7)
        Me.FrameJobDetails.Controls.Add(Me.Label6)
        Me.FrameJobDetails.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameJobDetails.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameJobDetails.Location = New System.Drawing.Point(600, 72)
        Me.FrameJobDetails.Name = "FrameJobDetails"
        Me.FrameJobDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameJobDetails.Size = New System.Drawing.Size(337, 601)
        Me.FrameJobDetails.TabIndex = 15
        Me.FrameJobDetails.TabStop = False
        Me.FrameJobDetails.Text = "FrameJobDetails"
        '
        'ListViewParts
        '
        Me.ListViewParts.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ListViewParts.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewParts.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewParts.FullRowSelect = True
        Me.ListViewParts.Location = New System.Drawing.Point(16, 440)
        Me.ListViewParts.Name = "ListViewParts"
        Me.ListViewParts.Size = New System.Drawing.Size(305, 145)
        Me.ListViewParts.TabIndex = 27
        Me.ListViewParts.UseCompatibleStateImageBehavior = False
        Me.ListViewParts.View = System.Windows.Forms.View.Details
        '
        'txtWorkHistory
        '
        Me.txtWorkHistory.AcceptsReturn = True
        Me.txtWorkHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtWorkHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWorkHistory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWorkHistory.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkHistory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWorkHistory.Location = New System.Drawing.Point(16, 272)
        Me.txtWorkHistory.MaxLength = 0
        Me.txtWorkHistory.Multiline = True
        Me.txtWorkHistory.Name = "txtWorkHistory"
        Me.txtWorkHistory.ReadOnly = True
        Me.txtWorkHistory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWorkHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWorkHistory.Size = New System.Drawing.Size(297, 129)
        Me.txtWorkHistory.TabIndex = 25
        '
        'txtSymptoms
        '
        Me.txtSymptoms.AcceptsReturn = True
        Me.txtSymptoms.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtSymptoms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSymptoms.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSymptoms.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSymptoms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSymptoms.Location = New System.Drawing.Point(16, 144)
        Me.txtSymptoms.MaxLength = 0
        Me.txtSymptoms.Multiline = True
        Me.txtSymptoms.Name = "txtSymptoms"
        Me.txtSymptoms.ReadOnly = True
        Me.txtSymptoms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSymptoms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSymptoms.Size = New System.Drawing.Size(297, 97)
        Me.txtSymptoms.TabIndex = 23
        '
        'txtGoods
        '
        Me.txtGoods.AcceptsReturn = True
        Me.txtGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtGoods.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoods.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGoods.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoods.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGoods.Location = New System.Drawing.Point(16, 40)
        Me.txtGoods.MaxLength = 0
        Me.txtGoods.Multiline = True
        Me.txtGoods.Name = "txtGoods"
        Me.txtGoods.ReadOnly = True
        Me.txtGoods.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGoods.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGoods.Size = New System.Drawing.Size(297, 73)
        Me.txtGoods.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(16, 424)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(105, 17)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Items Supplied"
        '
        'LabWorkHistory
        '
        Me.LabWorkHistory.BackColor = System.Drawing.Color.Transparent
        Me.LabWorkHistory.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabWorkHistory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabWorkHistory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabWorkHistory.Location = New System.Drawing.Point(16, 256)
        Me.LabWorkHistory.Name = "LabWorkHistory"
        Me.LabWorkHistory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabWorkHistory.Size = New System.Drawing.Size(217, 17)
        Me.LabWorkHistory.TabIndex = 26
        Me.LabWorkHistory.Text = "Work History"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(16, 128)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(145, 17)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Problem and Diagnosis"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(16, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(105, 17)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Goods in Care"
        '
        'FrameCust
        '
        Me.FrameCust.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameCust.Controls.Add(Me.txtCustFax)
        Me.FrameCust.Controls.Add(Me.txtCustEmail)
        Me.FrameCust.Controls.Add(Me.txtCustABN)
        Me.FrameCust.Controls.Add(Me.txtCustAddress)
        Me.FrameCust.Controls.Add(Me.txtCustCompany)
        Me.FrameCust.Controls.Add(Me.txtCustMobile)
        Me.FrameCust.Controls.Add(Me.txtCustPhone)
        Me.FrameCust.Controls.Add(Me.txtCustName)
        Me.FrameCust.Controls.Add(Me.Label12)
        Me.FrameCust.Controls.Add(Me.Label11)
        Me.FrameCust.Controls.Add(Me.Label10)
        Me.FrameCust.Controls.Add(Me.Label9)
        Me.FrameCust.Controls.Add(Me.LabCompany)
        Me.FrameCust.Controls.Add(Me.LabMobile)
        Me.FrameCust.Controls.Add(Me.LabPhone)
        Me.FrameCust.Controls.Add(Me.LabName)
        Me.FrameCust.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameCust.Location = New System.Drawing.Point(8, 72)
        Me.FrameCust.Name = "FrameCust"
        Me.FrameCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameCust.Size = New System.Drawing.Size(585, 239)
        Me.FrameCust.TabIndex = 1
        Me.FrameCust.TabStop = False
        Me.FrameCust.Text = " Customer Details "
        '
        'txtCustFax
        '
        Me.txtCustFax.AcceptsReturn = True
        Me.txtCustFax.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustFax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustFax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustFax.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustFax.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustFax.Location = New System.Drawing.Point(408, 144)
        Me.txtCustFax.MaxLength = 0
        Me.txtCustFax.Name = "txtCustFax"
        Me.txtCustFax.ReadOnly = True
        Me.txtCustFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustFax.Size = New System.Drawing.Size(137, 15)
        Me.txtCustFax.TabIndex = 6
        Me.txtCustFax.TabStop = False
        '
        'txtCustEmail
        '
        Me.txtCustEmail.AcceptsReturn = True
        Me.txtCustEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustEmail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustEmail.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustEmail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustEmail.Location = New System.Drawing.Point(72, 176)
        Me.txtCustEmail.MaxLength = 0
        Me.txtCustEmail.Name = "txtCustEmail"
        Me.txtCustEmail.ReadOnly = True
        Me.txtCustEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustEmail.Size = New System.Drawing.Size(473, 18)
        Me.txtCustEmail.TabIndex = 8
        '
        'txtCustABN
        '
        Me.txtCustABN.AcceptsReturn = True
        Me.txtCustABN.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustABN.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustABN.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustABN.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustABN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustABN.Location = New System.Drawing.Point(408, 24)
        Me.txtCustABN.MaxLength = 0
        Me.txtCustABN.Name = "txtCustABN"
        Me.txtCustABN.ReadOnly = True
        Me.txtCustABN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustABN.Size = New System.Drawing.Size(137, 15)
        Me.txtCustABN.TabIndex = 2
        '
        'txtCustAddress
        '
        Me.txtCustAddress.AcceptsReturn = True
        Me.txtCustAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustAddress.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustAddress.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustAddress.Location = New System.Drawing.Point(72, 96)
        Me.txtCustAddress.MaxLength = 0
        Me.txtCustAddress.Multiline = True
        Me.txtCustAddress.Name = "txtCustAddress"
        Me.txtCustAddress.ReadOnly = True
        Me.txtCustAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCustAddress.Size = New System.Drawing.Size(273, 73)
        Me.txtCustAddress.TabIndex = 7
        '
        'txtCustMobile
        '
        Me.txtCustMobile.AcceptsReturn = True
        Me.txtCustMobile.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustMobile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustMobile.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustMobile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustMobile.Location = New System.Drawing.Point(408, 120)
        Me.txtCustMobile.MaxLength = 0
        Me.txtCustMobile.Name = "txtCustMobile"
        Me.txtCustMobile.ReadOnly = True
        Me.txtCustMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustMobile.Size = New System.Drawing.Size(137, 15)
        Me.txtCustMobile.TabIndex = 5
        Me.txtCustMobile.TabStop = False
        '
        'txtCustPhone
        '
        Me.txtCustPhone.AcceptsReturn = True
        Me.txtCustPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustPhone.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPhone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustPhone.Location = New System.Drawing.Point(408, 96)
        Me.txtCustPhone.MaxLength = 0
        Me.txtCustPhone.Name = "txtCustPhone"
        Me.txtCustPhone.ReadOnly = True
        Me.txtCustPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPhone.Size = New System.Drawing.Size(137, 15)
        Me.txtCustPhone.TabIndex = 4
        Me.txtCustPhone.TabStop = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(376, 144)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(33, 17)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "Fax:"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(16, 176)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(49, 17)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "Email:"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(360, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(41, 17)
        Me.Label10.TabIndex = 31
        Me.Label10.Text = "ABN"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(8, 96)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(57, 17)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "Address"
        '
        'LabCompany
        '
        Me.LabCompany.BackColor = System.Drawing.Color.Transparent
        Me.LabCompany.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabCompany.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabCompany.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabCompany.Location = New System.Drawing.Point(8, 24)
        Me.LabCompany.Name = "LabCompany"
        Me.LabCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabCompany.Size = New System.Drawing.Size(65, 17)
        Me.LabCompany.TabIndex = 13
        Me.LabCompany.Text = "Company:"
        '
        'LabMobile
        '
        Me.LabMobile.BackColor = System.Drawing.Color.Transparent
        Me.LabMobile.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabMobile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabMobile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabMobile.Location = New System.Drawing.Point(360, 120)
        Me.LabMobile.Name = "LabMobile"
        Me.LabMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabMobile.Size = New System.Drawing.Size(49, 17)
        Me.LabMobile.TabIndex = 12
        Me.LabMobile.Text = "Mobile:"
        '
        'LabPhone
        '
        Me.LabPhone.BackColor = System.Drawing.Color.Transparent
        Me.LabPhone.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPhone.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPhone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPhone.Location = New System.Drawing.Point(360, 96)
        Me.LabPhone.Name = "LabPhone"
        Me.LabPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPhone.Size = New System.Drawing.Size(49, 17)
        Me.LabPhone.TabIndex = 11
        Me.LabPhone.Text = "Phone:"
        '
        'LabName
        '
        Me.LabName.BackColor = System.Drawing.Color.Transparent
        Me.LabName.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabName.Location = New System.Drawing.Point(8, 64)
        Me.LabName.Name = "LabName"
        Me.LabName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabName.Size = New System.Drawing.Size(41, 17)
        Me.LabName.TabIndex = 10
        Me.LabName.Text = "Name:"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(200, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(3)
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(237, 35)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Enter Cust. Barcode and press ENTER,   or press F2 to Lookup Customers."
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(24, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(86, 17)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Cust Barcode:"
        '
        'LabVersion
        '
        Me.LabVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.LabVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabVersion.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabVersion.Location = New System.Drawing.Point(746, 680)
        Me.LabVersion.Name = "LabVersion"
        Me.LabVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabVersion.Size = New System.Drawing.Size(185, 9)
        Me.LabVersion.TabIndex = 34
        Me.LabVersion.Text = "LabVersion"
        '
        'labBusiness
        '
        Me.labBusiness.BackColor = System.Drawing.Color.Transparent
        Me.labBusiness.Cursor = System.Windows.Forms.Cursors.Default
        Me.labBusiness.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labBusiness.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labBusiness.Location = New System.Drawing.Point(600, 8)
        Me.labBusiness.Name = "labBusiness"
        Me.labBusiness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labBusiness.Size = New System.Drawing.Size(177, 25)
        Me.labBusiness.TabIndex = 14
        Me.labBusiness.Text = "labBusiness"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(16, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(313, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = " Customer Purchases and Job History"
        '
        'frmCustHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(943, 689)
        Me.Controls.Add(Me.FrameBrowse)
        Me.Controls.Add(Me.txtCustNo)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.frameHistory)
        Me.Controls.Add(Me.FrameJobDetails)
        Me.Controls.Add(Me.FrameCust)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LabVersion)
        Me.Controls.Add(Me.labBusiness)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmCustHistory"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "JobTracking- Customer History.."
        Me.FrameBrowse.ResumeLayout(False)
        Me.FrameBrowse.PerformLayout()
        CType(Me.DataGridViewHost, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frameHistory.ResumeLayout(False)
        Me.FrameJobDetails.ResumeLayout(False)
        Me.FrameJobDetails.PerformLayout()
        Me.FrameCust.ResumeLayout(False)
        Me.FrameCust.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridViewHost As System.Windows.Forms.DataGridView
#End Region 
End Class