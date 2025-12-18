<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmQuoteJobs
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
	Public WithEvents txtChassisCat2 As System.Windows.Forms.TextBox
	Public WithEvents txtChassisCat1 As System.Windows.Forms.TextBox
	Public WithEvents cmdRestoreChassisDefs As System.Windows.Forms.Button
	Public WithEvents cboChassis As System.Windows.Forms.ComboBox
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents LabSplitMsg As System.Windows.Forms.Label
	Public WithEvents FrameChassis As System.Windows.Forms.GroupBox
	Public WithEvents cmdReprint As System.Windows.Forms.Button
	Public WithEvents cmdCopyComment As System.Windows.Forms.Button
	Public WithEvents txtNominTech As System.Windows.Forms.TextBox
	Public WithEvents txtComments As System.Windows.Forms.TextBox
	Public WithEvents ListViewJob As System.Windows.Forms.ListView
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_0 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_1 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_2 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_3 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_4 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_5 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_6 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_7 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_8 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_9 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_10 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_11 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_12 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_13 As System.Windows.Forms.Label
	Public WithEvents _LabJobSeq_14 As System.Windows.Forms.Label
	Public WithEvents LabJobInfo As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents FrameBuild As System.Windows.Forms.GroupBox
	Public WithEvents Picture1 As System.Windows.Forms.PictureBox
	Public WithEvents Timer1 As System.Windows.Forms.Timer
	Public WithEvents ListViewQuote As System.Windows.Forms.ListView
	Public WithEvents LabPoolInfo As System.Windows.Forms.Label
	Public WithEvents LabSequence As System.Windows.Forms.Label
	Public WithEvents FrameQuote As System.Windows.Forms.GroupBox
    Public WithEvents txtNoJobs As System.Windows.Forms.TextBox
	Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdQuoteOK As System.Windows.Forms.Button
    Public WithEvents LabVersion As System.Windows.Forms.Label
	Public WithEvents LabStatus As System.Windows.Forms.Label
	Public WithEvents LabSpin As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents LabOrderDetail As System.Windows.Forms.Label
	Public WithEvents LabHdr2 As System.Windows.Forms.Label
	Public WithEvents LabHdr1 As System.Windows.Forms.Label
	Public WithEvents LabJobSeq As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuoteJobs))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdReprint = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdAddAll = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.chkBuildOneJobAnyway = New System.Windows.Forms.CheckBox()
        Me.FrameChassis = New System.Windows.Forms.GroupBox()
        Me.txtChassisCat2 = New System.Windows.Forms.TextBox()
        Me.txtChassisCat1 = New System.Windows.Forms.TextBox()
        Me.cmdRestoreChassisDefs = New System.Windows.Forms.Button()
        Me.cboChassis = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LabSplitMsg = New System.Windows.Forms.Label()
        Me.FrameBuild = New System.Windows.Forms.GroupBox()
        Me.txtNomTechBarcode = New System.Windows.Forms.TextBox()
        Me.cmdCopyComment = New System.Windows.Forms.Button()
        Me.txtNominTech = New System.Windows.Forms.TextBox()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.ListViewJob = New System.Windows.Forms.ListView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._LabJobSeq_0 = New System.Windows.Forms.Label()
        Me._LabJobSeq_1 = New System.Windows.Forms.Label()
        Me._LabJobSeq_2 = New System.Windows.Forms.Label()
        Me._LabJobSeq_3 = New System.Windows.Forms.Label()
        Me._LabJobSeq_4 = New System.Windows.Forms.Label()
        Me._LabJobSeq_5 = New System.Windows.Forms.Label()
        Me._LabJobSeq_6 = New System.Windows.Forms.Label()
        Me._LabJobSeq_7 = New System.Windows.Forms.Label()
        Me._LabJobSeq_8 = New System.Windows.Forms.Label()
        Me._LabJobSeq_9 = New System.Windows.Forms.Label()
        Me._LabJobSeq_10 = New System.Windows.Forms.Label()
        Me._LabJobSeq_11 = New System.Windows.Forms.Label()
        Me._LabJobSeq_12 = New System.Windows.Forms.Label()
        Me._LabJobSeq_13 = New System.Windows.Forms.Label()
        Me._LabJobSeq_14 = New System.Windows.Forms.Label()
        Me.LabJobInfo = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Picture1 = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.FrameQuote = New System.Windows.Forms.GroupBox()
        Me.ListViewQuote = New System.Windows.Forms.ListView()
        Me.LabPoolInfo = New System.Windows.Forms.Label()
        Me.LabSequence = New System.Windows.Forms.Label()
        Me.txtNoJobs = New System.Windows.Forms.TextBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdQuoteOK = New System.Windows.Forms.Button()
        Me.LabVersion = New System.Windows.Forms.Label()
        Me.LabStatus = New System.Windows.Forms.Label()
        Me.LabSpin = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabOrderDetail = New System.Windows.Forms.Label()
        Me.LabHdr2 = New System.Windows.Forms.Label()
        Me.LabHdr1 = New System.Windows.Forms.Label()
        Me.LabJobSeq = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label11 = New System.Windows.Forms.Label()
        Me.labCustSearch = New System.Windows.Forms.Label()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.FrameChassis.SuspendLayout()
        Me.FrameBuild.SuspendLayout()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FrameQuote.SuspendLayout()
        CType(Me.LabJobSeq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdReprint
        '
        Me.cmdReprint.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdReprint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReprint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReprint.Location = New System.Drawing.Point(808, 664)
        Me.cmdReprint.Name = "cmdReprint"
        Me.cmdReprint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReprint.Size = New System.Drawing.Size(57, 25)
        Me.cmdReprint.TabIndex = 27
        Me.cmdReprint.Text = "Print"
        Me.ToolTip1.SetToolTip(Me.cmdReprint, "Print/Re-print all Job sheets for this Quote..")
        Me.cmdReprint.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(640, 664)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(141, 25)
        Me.cmdSave.TabIndex = 26
        Me.cmdSave.Text = "  Save  (Create Jobs)"
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Create or update JobTracking Job records for this Quote.. ")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdAddAll
        '
        Me.cmdAddAll.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdAddAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAddAll.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAddAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAddAll.Location = New System.Drawing.Point(446, 92)
        Me.cmdAddAll.Name = "cmdAddAll"
        Me.cmdAddAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAddAll.Size = New System.Drawing.Size(49, 27)
        Me.cmdAddAll.TabIndex = 17
        Me.cmdAddAll.Text = ">>>"
        Me.ToolTip1.SetToolTip(Me.cmdAddAll, "Auto-Allocate All Parts to Jobs")
        Me.cmdAddAll.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Location = New System.Drawing.Point(456, 150)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(39, 27)
        Me.cmdAdd.TabIndex = 18
        Me.cmdAdd.Text = ">"
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Allocate Selected Part to Current Job")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdRemove
        '
        Me.cmdRemove.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdRemove.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRemove.Font = New System.Drawing.Font("Comic Sans MS", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRemove.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRemove.Location = New System.Drawing.Point(456, 198)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRemove.Size = New System.Drawing.Size(39, 27)
        Me.cmdRemove.TabIndex = 19
        Me.cmdRemove.Text = "<"
        Me.cmdRemove.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.cmdRemove, "Remove selected part from job")
        Me.cmdRemove.UseVisualStyleBackColor = False
        '
        'chkBuildOneJobAnyway
        '
        Me.chkBuildOneJobAnyway.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBuildOneJobAnyway.ForeColor = System.Drawing.Color.DarkMagenta
        Me.chkBuildOneJobAnyway.Location = New System.Drawing.Point(268, 196)
        Me.chkBuildOneJobAnyway.Name = "chkBuildOneJobAnyway"
        Me.chkBuildOneJobAnyway.Size = New System.Drawing.Size(105, 33)
        Me.chkBuildOneJobAnyway.TabIndex = 49
        Me.chkBuildOneJobAnyway.Text = "Build One Job (Anyway)"
        Me.ToolTip1.SetToolTip(Me.chkBuildOneJobAnyway, "No chassis comonent to build on..  But go ahead and Build One Job (Anyway)")
        Me.chkBuildOneJobAnyway.UseVisualStyleBackColor = True
        '
        'FrameChassis
        '
        Me.FrameChassis.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.FrameChassis.Controls.Add(Me.txtChassisCat2)
        Me.FrameChassis.Controls.Add(Me.txtChassisCat1)
        Me.FrameChassis.Controls.Add(Me.cmdRestoreChassisDefs)
        Me.FrameChassis.Controls.Add(Me.cboChassis)
        Me.FrameChassis.Controls.Add(Me.Label7)
        Me.FrameChassis.Controls.Add(Me.Label6)
        Me.FrameChassis.Controls.Add(Me.Label8)
        Me.FrameChassis.Controls.Add(Me.Label10)
        Me.FrameChassis.Controls.Add(Me.Label9)
        Me.FrameChassis.Controls.Add(Me.LabSplitMsg)
        Me.FrameChassis.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameChassis.Location = New System.Drawing.Point(522, 13)
        Me.FrameChassis.Name = "FrameChassis"
        Me.FrameChassis.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameChassis.Size = New System.Drawing.Size(439, 183)
        Me.FrameChassis.TabIndex = 46
        Me.FrameChassis.TabStop = False
        Me.FrameChassis.Text = "Chassis"
        '
        'txtChassisCat2
        '
        Me.txtChassisCat2.AcceptsReturn = True
        Me.txtChassisCat2.BackColor = System.Drawing.Color.Gainsboro
        Me.txtChassisCat2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChassisCat2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChassisCat2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChassisCat2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChassisCat2.Location = New System.Drawing.Point(344, 149)
        Me.txtChassisCat2.MaxLength = 0
        Me.txtChassisCat2.Name = "txtChassisCat2"
        Me.txtChassisCat2.ReadOnly = True
        Me.txtChassisCat2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChassisCat2.Size = New System.Drawing.Size(57, 15)
        Me.txtChassisCat2.TabIndex = 56
        Me.txtChassisCat2.TabStop = False
        '
        'txtChassisCat1
        '
        Me.txtChassisCat1.AcceptsReturn = True
        Me.txtChassisCat1.BackColor = System.Drawing.Color.Gainsboro
        Me.txtChassisCat1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChassisCat1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChassisCat1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChassisCat1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChassisCat1.Location = New System.Drawing.Point(261, 149)
        Me.txtChassisCat1.MaxLength = 0
        Me.txtChassisCat1.Name = "txtChassisCat1"
        Me.txtChassisCat1.ReadOnly = True
        Me.txtChassisCat1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChassisCat1.Size = New System.Drawing.Size(57, 15)
        Me.txtChassisCat1.TabIndex = 55
        Me.txtChassisCat1.TabStop = False
        '
        'cmdRestoreChassisDefs
        '
        Me.cmdRestoreChassisDefs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdRestoreChassisDefs.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRestoreChassisDefs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRestoreChassisDefs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRestoreChassisDefs.Location = New System.Drawing.Point(376, 84)
        Me.cmdRestoreChassisDefs.Name = "cmdRestoreChassisDefs"
        Me.cmdRestoreChassisDefs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRestoreChassisDefs.Size = New System.Drawing.Size(25, 21)
        Me.cmdRestoreChassisDefs.TabIndex = 51
        Me.cmdRestoreChassisDefs.Text = "<>"
        Me.cmdRestoreChassisDefs.UseVisualStyleBackColor = False
        '
        'cboChassis
        '
        Me.cboChassis.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboChassis.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboChassis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChassis.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboChassis.Location = New System.Drawing.Point(264, 44)
        Me.cboChassis.Name = "cboChassis"
        Me.cboChassis.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboChassis.Size = New System.Drawing.Size(137, 21)
        Me.cboChassis.TabIndex = 49
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(349, 129)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(41, 17)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "Cat2"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(258, 129)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(41, 17)
        Me.Label6.TabIndex = 53
        Me.Label6.Text = "Cat1"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(258, 112)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(97, 17)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Current Settings:"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(261, 88)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(89, 17)
        Me.Label10.TabIndex = 50
        Me.Label10.Text = "Restore Defaults"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Comic Sans MS", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(261, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(145, 17)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "Change Chassis Component"
        '
        'LabSplitMsg
        '
        Me.LabSplitMsg.BackColor = System.Drawing.Color.Transparent
        Me.LabSplitMsg.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabSplitMsg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabSplitMsg.Location = New System.Drawing.Point(13, 25)
        Me.LabSplitMsg.Name = "LabSplitMsg"
        Me.LabSplitMsg.Padding = New System.Windows.Forms.Padding(3)
        Me.LabSplitMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabSplitMsg.Size = New System.Drawing.Size(214, 104)
        Me.LabSplitMsg.TabIndex = 47
        Me.LabSplitMsg.Text = "The splitting of a Quote into multiple Jobs is based on the Category (Cat1/Cat2) " & _
    "of the Stock Part selected as the foundation, or base, Component."
        '
        'FrameBuild
        '
        Me.FrameBuild.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameBuild.Controls.Add(Me.txtNomTechBarcode)
        Me.FrameBuild.Controls.Add(Me.cmdCopyComment)
        Me.FrameBuild.Controls.Add(Me.txtNominTech)
        Me.FrameBuild.Controls.Add(Me.txtComments)
        Me.FrameBuild.Controls.Add(Me.ListViewJob)
        Me.FrameBuild.Controls.Add(Me.Label2)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_0)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_1)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_2)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_3)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_4)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_5)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_6)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_7)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_8)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_9)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_10)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_11)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_12)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_13)
        Me.FrameBuild.Controls.Add(Me._LabJobSeq_14)
        Me.FrameBuild.Controls.Add(Me.LabJobInfo)
        Me.FrameBuild.Controls.Add(Me.Label3)
        Me.FrameBuild.Controls.Add(Me.Label5)
        Me.FrameBuild.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameBuild.Location = New System.Drawing.Point(522, 237)
        Me.FrameBuild.Name = "FrameBuild"
        Me.FrameBuild.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameBuild.Size = New System.Drawing.Size(439, 411)
        Me.FrameBuild.TabIndex = 19
        Me.FrameBuild.TabStop = False
        Me.FrameBuild.Text = "FrameBuild"
        '
        'txtNomTechBarcode
        '
        Me.txtNomTechBarcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNomTechBarcode.Location = New System.Drawing.Point(315, 72)
        Me.txtNomTechBarcode.Name = "txtNomTechBarcode"
        Me.txtNomTechBarcode.Size = New System.Drawing.Size(31, 21)
        Me.txtNomTechBarcode.TabIndex = 20
        '
        'cmdCopyComment
        '
        Me.cmdCopyComment.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCopyComment.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCopyComment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCopyComment.Location = New System.Drawing.Point(16, 126)
        Me.cmdCopyComment.Name = "cmdCopyComment"
        Me.cmdCopyComment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCopyComment.Size = New System.Drawing.Size(97, 21)
        Me.cmdCopyComment.TabIndex = 57
        Me.cmdCopyComment.Text = "Copy to all Jobs"
        Me.cmdCopyComment.UseVisualStyleBackColor = False
        '
        'txtNominTech
        '
        Me.txtNominTech.AcceptsReturn = True
        Me.txtNominTech.BackColor = System.Drawing.SystemColors.Window
        Me.txtNominTech.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNominTech.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNominTech.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNominTech.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNominTech.Location = New System.Drawing.Point(358, 72)
        Me.txtNominTech.MaxLength = 0
        Me.txtNominTech.Name = "txtNominTech"
        Me.txtNominTech.ReadOnly = True
        Me.txtNominTech.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNominTech.Size = New System.Drawing.Size(67, 14)
        Me.txtNominTech.TabIndex = 21
        '
        'txtComments
        '
        Me.txtComments.AcceptsReturn = True
        Me.txtComments.BackColor = System.Drawing.SystemColors.Window
        Me.txtComments.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComments.Location = New System.Drawing.Point(128, 97)
        Me.txtComments.MaxLength = 0
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(305, 49)
        Me.txtComments.TabIndex = 22
        '
        'ListViewJob
        '
        Me.ListViewJob.BackColor = System.Drawing.SystemColors.Window
        Me.ListViewJob.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewJob.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewJob.FullRowSelect = True
        Me.ListViewJob.GridLines = True
        Me.ListViewJob.Location = New System.Drawing.Point(8, 152)
        Me.ListViewJob.Name = "ListViewJob"
        Me.ListViewJob.Size = New System.Drawing.Size(425, 246)
        Me.ListViewJob.TabIndex = 22
        Me.ListViewJob.UseCompatibleStateImageBehavior = False
        Me.ListViewJob.View = System.Windows.Forms.View.Details
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(24, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(177, 17)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "JOBS: View Job by Sequence No:"
        '
        '_LabJobSeq_0
        '
        Me._LabJobSeq_0.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_0.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_0, CType(0, Short))
        Me._LabJobSeq_0.Location = New System.Drawing.Point(24, 35)
        Me._LabJobSeq_0.Name = "_LabJobSeq_0"
        Me._LabJobSeq_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_0.Size = New System.Drawing.Size(19, 25)
        Me._LabJobSeq_0.TabIndex = 43
        Me._LabJobSeq_0.Text = "1"
        Me._LabJobSeq_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_1
        '
        Me._LabJobSeq_1.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_1, CType(1, Short))
        Me._LabJobSeq_1.Location = New System.Drawing.Point(48, 35)
        Me._LabJobSeq_1.Name = "_LabJobSeq_1"
        Me._LabJobSeq_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_1.Size = New System.Drawing.Size(19, 25)
        Me._LabJobSeq_1.TabIndex = 42
        Me._LabJobSeq_1.Text = "2"
        Me._LabJobSeq_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_2
        '
        Me._LabJobSeq_2.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_2.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_2, CType(2, Short))
        Me._LabJobSeq_2.Location = New System.Drawing.Point(72, 35)
        Me._LabJobSeq_2.Name = "_LabJobSeq_2"
        Me._LabJobSeq_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_2.Size = New System.Drawing.Size(19, 25)
        Me._LabJobSeq_2.TabIndex = 41
        Me._LabJobSeq_2.Text = "3"
        Me._LabJobSeq_2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_3
        '
        Me._LabJobSeq_3.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_3, CType(3, Short))
        Me._LabJobSeq_3.Location = New System.Drawing.Point(96, 35)
        Me._LabJobSeq_3.Name = "_LabJobSeq_3"
        Me._LabJobSeq_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_3.Size = New System.Drawing.Size(19, 25)
        Me._LabJobSeq_3.TabIndex = 40
        Me._LabJobSeq_3.Text = "4"
        Me._LabJobSeq_3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_4
        '
        Me._LabJobSeq_4.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_4, CType(4, Short))
        Me._LabJobSeq_4.Location = New System.Drawing.Point(120, 35)
        Me._LabJobSeq_4.Name = "_LabJobSeq_4"
        Me._LabJobSeq_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_4.Size = New System.Drawing.Size(19, 25)
        Me._LabJobSeq_4.TabIndex = 39
        Me._LabJobSeq_4.Text = "5"
        Me._LabJobSeq_4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_5
        '
        Me._LabJobSeq_5.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_5.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_5, CType(5, Short))
        Me._LabJobSeq_5.Location = New System.Drawing.Point(144, 35)
        Me._LabJobSeq_5.Name = "_LabJobSeq_5"
        Me._LabJobSeq_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_5.Size = New System.Drawing.Size(19, 25)
        Me._LabJobSeq_5.TabIndex = 38
        Me._LabJobSeq_5.Text = "6"
        Me._LabJobSeq_5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_6
        '
        Me._LabJobSeq_6.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_6.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_6, CType(6, Short))
        Me._LabJobSeq_6.Location = New System.Drawing.Point(168, 35)
        Me._LabJobSeq_6.Name = "_LabJobSeq_6"
        Me._LabJobSeq_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_6.Size = New System.Drawing.Size(19, 25)
        Me._LabJobSeq_6.TabIndex = 37
        Me._LabJobSeq_6.Text = "7"
        Me._LabJobSeq_6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_7
        '
        Me._LabJobSeq_7.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_7.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_7, CType(7, Short))
        Me._LabJobSeq_7.Location = New System.Drawing.Point(192, 35)
        Me._LabJobSeq_7.Name = "_LabJobSeq_7"
        Me._LabJobSeq_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_7.Size = New System.Drawing.Size(19, 25)
        Me._LabJobSeq_7.TabIndex = 36
        Me._LabJobSeq_7.Text = "8"
        Me._LabJobSeq_7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_8
        '
        Me._LabJobSeq_8.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_8.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_8, CType(8, Short))
        Me._LabJobSeq_8.Location = New System.Drawing.Point(216, 35)
        Me._LabJobSeq_8.Name = "_LabJobSeq_8"
        Me._LabJobSeq_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_8.Size = New System.Drawing.Size(19, 25)
        Me._LabJobSeq_8.TabIndex = 35
        Me._LabJobSeq_8.Text = "9"
        Me._LabJobSeq_8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_9
        '
        Me._LabJobSeq_9.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_9.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_9, CType(9, Short))
        Me._LabJobSeq_9.Location = New System.Drawing.Point(240, 35)
        Me._LabJobSeq_9.Name = "_LabJobSeq_9"
        Me._LabJobSeq_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_9.Size = New System.Drawing.Size(27, 25)
        Me._LabJobSeq_9.TabIndex = 34
        Me._LabJobSeq_9.Text = "10"
        Me._LabJobSeq_9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_10
        '
        Me._LabJobSeq_10.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_10.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_10, CType(10, Short))
        Me._LabJobSeq_10.Location = New System.Drawing.Point(269, 35)
        Me._LabJobSeq_10.Name = "_LabJobSeq_10"
        Me._LabJobSeq_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_10.Size = New System.Drawing.Size(27, 25)
        Me._LabJobSeq_10.TabIndex = 33
        Me._LabJobSeq_10.Text = "11"
        Me._LabJobSeq_10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_11
        '
        Me._LabJobSeq_11.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_11.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_11, CType(11, Short))
        Me._LabJobSeq_11.Location = New System.Drawing.Point(298, 35)
        Me._LabJobSeq_11.Name = "_LabJobSeq_11"
        Me._LabJobSeq_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_11.Size = New System.Drawing.Size(27, 25)
        Me._LabJobSeq_11.TabIndex = 32
        Me._LabJobSeq_11.Text = "12"
        Me._LabJobSeq_11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_12
        '
        Me._LabJobSeq_12.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_12.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_12, CType(12, Short))
        Me._LabJobSeq_12.Location = New System.Drawing.Point(328, 35)
        Me._LabJobSeq_12.Name = "_LabJobSeq_12"
        Me._LabJobSeq_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_12.Size = New System.Drawing.Size(27, 25)
        Me._LabJobSeq_12.TabIndex = 31
        Me._LabJobSeq_12.Text = "13"
        Me._LabJobSeq_12.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_13
        '
        Me._LabJobSeq_13.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_13.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_13, CType(13, Short))
        Me._LabJobSeq_13.Location = New System.Drawing.Point(358, 35)
        Me._LabJobSeq_13.Name = "_LabJobSeq_13"
        Me._LabJobSeq_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_13.Size = New System.Drawing.Size(27, 25)
        Me._LabJobSeq_13.TabIndex = 30
        Me._LabJobSeq_13.Text = "14"
        Me._LabJobSeq_13.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_LabJobSeq_14
        '
        Me._LabJobSeq_14.BackColor = System.Drawing.SystemColors.Control
        Me._LabJobSeq_14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._LabJobSeq_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabJobSeq_14.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabJobSeq_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobSeq.SetIndex(Me._LabJobSeq_14, CType(14, Short))
        Me._LabJobSeq_14.Location = New System.Drawing.Point(388, 35)
        Me._LabJobSeq_14.Name = "_LabJobSeq_14"
        Me._LabJobSeq_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabJobSeq_14.Size = New System.Drawing.Size(27, 25)
        Me._LabJobSeq_14.TabIndex = 28
        Me._LabJobSeq_14.Text = "15"
        Me._LabJobSeq_14.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabJobInfo
        '
        Me.LabJobInfo.BackColor = System.Drawing.Color.Transparent
        Me.LabJobInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabJobInfo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabJobInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobInfo.Location = New System.Drawing.Point(16, 65)
        Me.LabJobInfo.Name = "LabJobInfo"
        Me.LabJobInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabJobInfo.Size = New System.Drawing.Size(177, 25)
        Me.LabJobInfo.TabIndex = 25
        Me.LabJobInfo.Text = "LabJobInfo"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(202, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(104, 17)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Nom.Tech.this Job:"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(9, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(117, 27)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Special Comments for Building this Job:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Picture1
        '
        Me.Picture1.BackColor = System.Drawing.SystemColors.Control
        Me.Picture1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Picture1.Image = CType(resources.GetObject("Picture1.Image"), System.Drawing.Image)
        Me.Picture1.Location = New System.Drawing.Point(592, 328)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture1.Size = New System.Drawing.Size(120, 82)
        Me.Picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture1.TabIndex = 18
        Me.Picture1.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 300
        '
        'FrameQuote
        '
        Me.FrameQuote.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameQuote.Controls.Add(Me.cmdRemove)
        Me.FrameQuote.Controls.Add(Me.cmdAdd)
        Me.FrameQuote.Controls.Add(Me.cmdAddAll)
        Me.FrameQuote.Controls.Add(Me.ListViewQuote)
        Me.FrameQuote.Controls.Add(Me.LabPoolInfo)
        Me.FrameQuote.Controls.Add(Me.LabSequence)
        Me.FrameQuote.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameQuote.Location = New System.Drawing.Point(12, 237)
        Me.FrameQuote.Name = "FrameQuote"
        Me.FrameQuote.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameQuote.Size = New System.Drawing.Size(504, 412)
        Me.FrameQuote.TabIndex = 12
        Me.FrameQuote.TabStop = False
        Me.FrameQuote.Text = "FrameQuote"
        '
        'ListViewQuote
        '
        Me.ListViewQuote.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ListViewQuote.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewQuote.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewQuote.FullRowSelect = True
        Me.ListViewQuote.Location = New System.Drawing.Point(6, 34)
        Me.ListViewQuote.Name = "ListViewQuote"
        Me.ListViewQuote.Size = New System.Drawing.Size(434, 370)
        Me.ListViewQuote.TabIndex = 13
        Me.ListViewQuote.UseCompatibleStateImageBehavior = False
        Me.ListViewQuote.View = System.Windows.Forms.View.Details
        '
        'LabPoolInfo
        '
        Me.LabPoolInfo.BackColor = System.Drawing.Color.Transparent
        Me.LabPoolInfo.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPoolInfo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPoolInfo.Location = New System.Drawing.Point(192, 17)
        Me.LabPoolInfo.Name = "LabPoolInfo"
        Me.LabPoolInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPoolInfo.Size = New System.Drawing.Size(137, 17)
        Me.LabPoolInfo.TabIndex = 16
        Me.LabPoolInfo.Text = "LabPoolInfo"
        '
        'LabSequence
        '
        Me.LabSequence.BackColor = System.Drawing.Color.Transparent
        Me.LabSequence.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabSequence.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabSequence.Location = New System.Drawing.Point(8, 17)
        Me.LabSequence.Name = "LabSequence"
        Me.LabSequence.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabSequence.Size = New System.Drawing.Size(146, 17)
        Me.LabSequence.TabIndex = 14
        Me.LabSequence.Text = "Quotation Pool (Item List)"
        '
        'txtNoJobs
        '
        Me.txtNoJobs.AcceptsReturn = True
        Me.txtNoJobs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNoJobs.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNoJobs.Font = New System.Drawing.Font("Courier New", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoJobs.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNoJobs.Location = New System.Drawing.Point(362, 664)
        Me.txtNoJobs.MaxLength = 0
        Me.txtNoJobs.Name = "txtNoJobs"
        Me.txtNoJobs.ReadOnly = True
        Me.txtNoJobs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNoJobs.Size = New System.Drawing.Size(25, 24)
        Me.txtNoJobs.TabIndex = 7
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancel.CausesValidation = False
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(888, 664)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(57, 25)
        Me.cmdCancel.TabIndex = 29
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdQuoteOK
        '
        Me.cmdQuoteOK.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdQuoteOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdQuoteOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdQuoteOK.Location = New System.Drawing.Point(399, 205)
        Me.cmdQuoteOK.Name = "cmdQuoteOK"
        Me.cmdQuoteOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdQuoteOK.Size = New System.Drawing.Size(60, 25)
        Me.cmdQuoteOK.TabIndex = 0
        Me.cmdQuoteOK.Text = "Proceed"
        Me.cmdQuoteOK.UseVisualStyleBackColor = False
        '
        'LabVersion
        '
        Me.LabVersion.BackColor = System.Drawing.Color.Transparent
        Me.LabVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabVersion.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabVersion.Location = New System.Drawing.Point(16, 688)
        Me.LabVersion.Name = "LabVersion"
        Me.LabVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabVersion.Size = New System.Drawing.Size(243, 11)
        Me.LabVersion.TabIndex = 45
        Me.LabVersion.Text = "LabVersion"
        '
        'LabStatus
        '
        Me.LabStatus.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabStatus.Font = New System.Drawing.Font("Comic Sans MS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabStatus.Location = New System.Drawing.Point(265, 158)
        Me.LabStatus.Name = "LabStatus"
        Me.LabStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabStatus.Size = New System.Drawing.Size(187, 33)
        Me.LabStatus.TabIndex = 17
        Me.LabStatus.Text = "LabStatus"
        '
        'LabSpin
        '
        Me.LabSpin.BackColor = System.Drawing.SystemColors.Control
        Me.LabSpin.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabSpin.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSpin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabSpin.Location = New System.Drawing.Point(488, 296)
        Me.LabSpin.Name = "LabSpin"
        Me.LabSpin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabSpin.Size = New System.Drawing.Size(17, 17)
        Me.LabSpin.TabIndex = 15
        Me.LabSpin.Text = "|"
        Me.LabSpin.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Gainsboro
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(400, 662)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(113, 27)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Systems (Jobs) to be built from this Quote"
        '
        'LabOrderDetail
        '
        Me.LabOrderDetail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabOrderDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabOrderDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabOrderDetail.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabOrderDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabOrderDetail.Location = New System.Drawing.Point(12, 91)
        Me.LabOrderDetail.Name = "LabOrderDetail"
        Me.LabOrderDetail.Padding = New System.Windows.Forms.Padding(5, 5, 0, 0)
        Me.LabOrderDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabOrderDetail.Size = New System.Drawing.Size(233, 139)
        Me.LabOrderDetail.TabIndex = 5
        Me.LabOrderDetail.Text = "LabOrderDetail"
        '
        'LabHdr2
        '
        Me.LabHdr2.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr2.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr2.Location = New System.Drawing.Point(265, 13)
        Me.LabHdr2.Name = "LabHdr2"
        Me.LabHdr2.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.LabHdr2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr2.Size = New System.Drawing.Size(244, 88)
        Me.LabHdr2.TabIndex = 4
        Me.LabHdr2.Text = "LabHdr2"
        '
        'LabHdr1
        '
        Me.LabHdr1.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr1.Location = New System.Drawing.Point(16, 13)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr1.Size = New System.Drawing.Size(185, 41)
        Me.LabHdr1.TabIndex = 1
        Me.LabHdr1.Text = "labHdr1"
        '
        'LabJobSeq
        '
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(267, 108)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 13)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "Customer-"
        '
        'labCustSearch
        '
        Me.labCustSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labCustSearch.Location = New System.Drawing.Point(265, 121)
        Me.labCustSearch.Name = "labCustSearch"
        Me.labCustSearch.Size = New System.Drawing.Size(191, 30)
        Me.labCustSearch.TabIndex = 48
        Me.labCustSearch.Text = "labCustSearch"
        '
        'frmQuoteJobs
        '
        Me.AcceptButton = Me.cmdQuoteOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(976, 700)
        Me.Controls.Add(Me.chkBuildOneJobAnyway)
        Me.Controls.Add(Me.labCustSearch)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.FrameChassis)
        Me.Controls.Add(Me.cmdReprint)
        Me.Controls.Add(Me.FrameBuild)
        Me.Controls.Add(Me.Picture1)
        Me.Controls.Add(Me.FrameQuote)
        Me.Controls.Add(Me.txtNoJobs)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdQuoteOK)
        Me.Controls.Add(Me.LabVersion)
        Me.Controls.Add(Me.LabStatus)
        Me.Controls.Add(Me.LabSpin)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabOrderDetail)
        Me.Controls.Add(Me.LabHdr2)
        Me.Controls.Add(Me.LabHdr1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmQuoteJobs"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "frmBuildQuotedJobs"
        Me.FrameChassis.ResumeLayout(False)
        Me.FrameChassis.PerformLayout()
        Me.FrameBuild.ResumeLayout(False)
        Me.FrameBuild.PerformLayout()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FrameQuote.ResumeLayout(False)
        CType(Me.LabJobSeq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents labCustSearch As System.Windows.Forms.Label
    Public WithEvents cmdRemove As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdAddAll As System.Windows.Forms.Button
    Friend WithEvents txtNomTechBarcode As System.Windows.Forms.TextBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents chkBuildOneJobAnyway As System.Windows.Forms.CheckBox
#End Region 
End Class