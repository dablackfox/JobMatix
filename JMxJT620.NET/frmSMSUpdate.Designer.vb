<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSMSUpdate
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
    Public WithEvents txtSmsPassword As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSMSUpdate))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdFinish = New System.Windows.Forms.Button()
        Me.btnOnSiteSave = New System.Windows.Forms.Button()
        Me.btnSaveExchange = New System.Windows.Forms.Button()
        Me.txtSmsPassword = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me._txtSmsPassword_0 = New System.Windows.Forms.TextBox()
        Me._txtSmsPassword_1 = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageSMS = New System.Windows.Forms.TabPage()
        Me.grpBoxReminders = New System.Windows.Forms.GroupBox()
        Me.cboOnSiteMinsBefore = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboOnSiteWakeUp = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.FrameSMS = New System.Windows.Forms.GroupBox()
        Me.txtNewSMS = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboSmsKeys = New System.Windows.Forms.ComboBox()
        Me.txtNewKey = New System.Windows.Forms.TextBox()
        Me.txtSMS = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Line2 = New System.Windows.Forms.Label()
        Me.LabText = New System.Windows.Forms.Label()
        Me.TabPageSMTP = New System.Windows.Forms.TabPage()
        Me.labAdminOnly = New System.Windows.Forms.Label()
        Me.frameSMSGateway = New System.Windows.Forms.GroupBox()
        Me.optSmsGatewayDirect = New System.Windows.Forms.RadioButton()
        Me.optSmsGatewayGlobal = New System.Windows.Forms.RadioButton()
        Me.optSmsGatewayBroadcast = New System.Windows.Forms.RadioButton()
        Me.optSmsGatewayBoss = New System.Windows.Forms.RadioButton()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtGatewayURL = New System.Windows.Forms.TextBox()
        Me.txtSmsUsername = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.LabConfirm = New System.Windows.Forms.Label()
        Me.frameSMTPSettings = New System.Windows.Forms.GroupBox()
        Me.chkHostUsesSSL = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtSMTPHostPort = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSMTPServer = New System.Windows.Forms.TextBox()
        Me.txtSMTPUsername = New System.Windows.Forms.TextBox()
        Me.txtSMTPPassword1 = New System.Windows.Forms.TextBox()
        Me.txtSMTPPassword2 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.labSMTPConfirm = New System.Windows.Forms.Label()
        Me.TabPageExchange = New System.Windows.Forms.TabPage()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.labAdminOnly2 = New System.Windows.Forms.Label()
        Me.grpBoxExchange = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtExchangeMailbox = New System.Windows.Forms.TextBox()
        Me.txtExchangePassword1 = New System.Windows.Forms.TextBox()
        Me.txtExchangePassword2 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.labExchangeConfirm = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.LabHdr1 = New System.Windows.Forms.Label()
        CType(Me.txtSmsPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPageSMS.SuspendLayout()
        Me.grpBoxReminders.SuspendLayout()
        Me.FrameSMS.SuspendLayout()
        Me.TabPageSMTP.SuspendLayout()
        Me.frameSMSGateway.SuspendLayout()
        Me.frameSMTPSettings.SuspendLayout()
        Me.TabPageExchange.SuspendLayout()
        Me.grpBoxExchange.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.Color.Lavender
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Location = New System.Drawing.Point(553, 299)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(68, 21)
        Me.cmdAdd.TabIndex = 16
        Me.cmdAdd.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add this message to SMS ref. List..")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.Color.Lavender
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Location = New System.Drawing.Point(87, 299)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(82, 21)
        Me.cmdDelete.TabIndex = 12
        Me.cmdDelete.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete this Standard Msg from Reference Table..")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.Color.Lavender
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Location = New System.Drawing.Point(197, 299)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(81, 21)
        Me.cmdUpdate.TabIndex = 13
        Me.cmdUpdate.Text = "Update"
        Me.ToolTip1.SetToolTip(Me.cmdUpdate, "Update the text of this Standard Msg in the Reference Table..")
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdFinish
        '
        Me.cmdFinish.BackColor = System.Drawing.Color.Lavender
        Me.cmdFinish.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFinish.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFinish.Location = New System.Drawing.Point(563, 447)
        Me.cmdFinish.Name = "cmdFinish"
        Me.cmdFinish.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFinish.Size = New System.Drawing.Size(87, 23)
        Me.cmdFinish.TabIndex = 27
        Me.cmdFinish.Text = "Finish"
        Me.ToolTip1.SetToolTip(Me.cmdFinish, "Save credentials and Exit")
        Me.cmdFinish.UseVisualStyleBackColor = False
        '
        'btnOnSiteSave
        '
        Me.btnOnSiteSave.BackColor = System.Drawing.Color.Lavender
        Me.btnOnSiteSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnOnSiteSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOnSiteSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnOnSiteSave.Location = New System.Drawing.Point(550, 67)
        Me.btnOnSiteSave.Name = "btnOnSiteSave"
        Me.btnOnSiteSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnOnSiteSave.Size = New System.Drawing.Size(68, 25)
        Me.btnOnSiteSave.TabIndex = 27
        Me.btnOnSiteSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnOnSiteSave, "Save on-site Reminder Time changes..")
        Me.btnOnSiteSave.UseVisualStyleBackColor = False
        '
        'btnSaveExchange
        '
        Me.btnSaveExchange.BackColor = System.Drawing.Color.LavenderBlush
        Me.btnSaveExchange.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSaveExchange.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveExchange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSaveExchange.Location = New System.Drawing.Point(563, 447)
        Me.btnSaveExchange.Name = "btnSaveExchange"
        Me.btnSaveExchange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnSaveExchange.Size = New System.Drawing.Size(87, 23)
        Me.btnSaveExchange.TabIndex = 28
        Me.btnSaveExchange.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveExchange, "Save credentials and Exit")
        Me.btnSaveExchange.UseVisualStyleBackColor = False
        '
        'txtSmsPassword
        '
        '
        '_txtSmsPassword_0
        '
        Me._txtSmsPassword_0.AcceptsReturn = True
        Me._txtSmsPassword_0.BackColor = System.Drawing.Color.White
        Me._txtSmsPassword_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtSmsPassword_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtSmsPassword_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtSmsPassword_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._txtSmsPassword_0.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSmsPassword.SetIndex(Me._txtSmsPassword_0, CType(0, Short))
        Me._txtSmsPassword_0.Location = New System.Drawing.Point(23, 335)
        Me._txtSmsPassword_0.MaxLength = 64
        Me._txtSmsPassword_0.Name = "_txtSmsPassword_0"
        Me._txtSmsPassword_0.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me._txtSmsPassword_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtSmsPassword_0.Size = New System.Drawing.Size(232, 21)
        Me._txtSmsPassword_0.TabIndex = 19
        '
        '_txtSmsPassword_1
        '
        Me._txtSmsPassword_1.AcceptsReturn = True
        Me._txtSmsPassword_1.BackColor = System.Drawing.Color.White
        Me._txtSmsPassword_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._txtSmsPassword_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtSmsPassword_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtSmsPassword_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me._txtSmsPassword_1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSmsPassword.SetIndex(Me._txtSmsPassword_1, CType(1, Short))
        Me._txtSmsPassword_1.Location = New System.Drawing.Point(23, 389)
        Me._txtSmsPassword_1.MaxLength = 64
        Me._txtSmsPassword_1.Name = "_txtSmsPassword_1"
        Me._txtSmsPassword_1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me._txtSmsPassword_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtSmsPassword_1.Size = New System.Drawing.Size(232, 21)
        Me._txtSmsPassword_1.TabIndex = 20
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageSMS)
        Me.TabControl1.Controls.Add(Me.TabPageSMTP)
        Me.TabControl1.Controls.Add(Me.TabPageExchange)
        Me.TabControl1.Location = New System.Drawing.Point(17, 49)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(670, 515)
        Me.TabControl1.TabIndex = 2
        '
        'TabPageSMS
        '
        Me.TabPageSMS.BackColor = System.Drawing.Color.White
        Me.TabPageSMS.Controls.Add(Me.grpBoxReminders)
        Me.TabPageSMS.Controls.Add(Me.FrameSMS)
        Me.TabPageSMS.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSMS.Name = "TabPageSMS"
        Me.TabPageSMS.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSMS.Size = New System.Drawing.Size(662, 489)
        Me.TabPageSMS.TabIndex = 0
        Me.TabPageSMS.Text = "SMS Messages"
        '
        'grpBoxReminders
        '
        Me.grpBoxReminders.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpBoxReminders.Controls.Add(Me.btnOnSiteSave)
        Me.grpBoxReminders.Controls.Add(Me.cboOnSiteMinsBefore)
        Me.grpBoxReminders.Controls.Add(Me.Label16)
        Me.grpBoxReminders.Controls.Add(Me.cboOnSiteWakeUp)
        Me.grpBoxReminders.Controls.Add(Me.Label15)
        Me.grpBoxReminders.Controls.Add(Me.Label14)
        Me.grpBoxReminders.Location = New System.Drawing.Point(6, 351)
        Me.grpBoxReminders.Name = "grpBoxReminders"
        Me.grpBoxReminders.Size = New System.Drawing.Size(649, 132)
        Me.grpBoxReminders.TabIndex = 2
        Me.grpBoxReminders.TabStop = False
        Me.grpBoxReminders.Text = "grpBoxReminders"
        '
        'cboOnSiteMinsBefore
        '
        Me.cboOnSiteMinsBefore.BackColor = System.Drawing.Color.Lavender
        Me.cboOnSiteMinsBefore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOnSiteMinsBefore.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboOnSiteMinsBefore.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOnSiteMinsBefore.FormattingEnabled = True
        Me.cboOnSiteMinsBefore.Location = New System.Drawing.Point(454, 43)
        Me.cboOnSiteMinsBefore.Name = "cboOnSiteMinsBefore"
        Me.cboOnSiteMinsBefore.Size = New System.Drawing.Size(57, 20)
        Me.cboOnSiteMinsBefore.TabIndex = 26
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(344, 43)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(106, 45)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "In-time Reminder (No of Mins before On-Site due)-"
        '
        'cboOnSiteWakeUp
        '
        Me.cboOnSiteWakeUp.BackColor = System.Drawing.Color.Lavender
        Me.cboOnSiteWakeUp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOnSiteWakeUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboOnSiteWakeUp.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOnSiteWakeUp.FormattingEnabled = True
        Me.cboOnSiteWakeUp.Location = New System.Drawing.Point(257, 43)
        Me.cboOnSiteWakeUp.Name = "cboOnSiteWakeUp"
        Me.cboOnSiteWakeUp.Size = New System.Drawing.Size(63, 20)
        Me.cboOnSiteWakeUp.TabIndex = 24
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(163, 43)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(92, 45)
        Me.Label15.TabIndex = 23
        Me.Label15.Text = "WakeUp Time (First On-Site reminder of day)-  "
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(6, 31)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(149, 40)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "Staff On-Site Reminders- SMS Message Times"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrameSMS
        '
        Me.FrameSMS.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameSMS.Controls.Add(Me.txtNewSMS)
        Me.FrameSMS.Controls.Add(Me.Label3)
        Me.FrameSMS.Controls.Add(Me.Label4)
        Me.FrameSMS.Controls.Add(Me.Label2)
        Me.FrameSMS.Controls.Add(Me.Label1)
        Me.FrameSMS.Controls.Add(Me.cboSmsKeys)
        Me.FrameSMS.Controls.Add(Me.txtNewKey)
        Me.FrameSMS.Controls.Add(Me.cmdAdd)
        Me.FrameSMS.Controls.Add(Me.txtSMS)
        Me.FrameSMS.Controls.Add(Me.cmdDelete)
        Me.FrameSMS.Controls.Add(Me.cmdUpdate)
        Me.FrameSMS.Controls.Add(Me.Label27)
        Me.FrameSMS.Controls.Add(Me.Label28)
        Me.FrameSMS.Controls.Add(Me.Line2)
        Me.FrameSMS.Controls.Add(Me.LabText)
        Me.FrameSMS.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameSMS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameSMS.Location = New System.Drawing.Point(3, 3)
        Me.FrameSMS.Name = "FrameSMS"
        Me.FrameSMS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameSMS.Size = New System.Drawing.Size(653, 342)
        Me.FrameSMS.TabIndex = 1
        Me.FrameSMS.TabStop = False
        '
        'txtNewSMS
        '
        Me.txtNewSMS.AcceptsReturn = True
        Me.txtNewSMS.BackColor = System.Drawing.Color.White
        Me.txtNewSMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewSMS.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNewSMS.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewSMS.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNewSMS.Location = New System.Drawing.Point(362, 172)
        Me.txtNewSMS.MaxLength = 144
        Me.txtNewSMS.Multiline = True
        Me.txtNewSMS.Name = "txtNewSMS"
        Me.txtNewSMS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNewSMS.Size = New System.Drawing.Size(259, 113)
        Me.txtNewSMS.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(491, 152)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 15)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "(Max. 144 characters)"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(359, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(76, 17)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Text to Send:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(165, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 15)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "(Max. 144 characters)"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(16, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(134, 25)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Edit SMS Messages"
        '
        'cboSmsKeys
        '
        Me.cboSmsKeys.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cboSmsKeys.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSmsKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSmsKeys.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSmsKeys.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSmsKeys.Location = New System.Drawing.Point(35, 97)
        Me.cboSmsKeys.Name = "cboSmsKeys"
        Me.cboSmsKeys.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSmsKeys.Size = New System.Drawing.Size(245, 21)
        Me.cboSmsKeys.TabIndex = 10
        '
        'txtNewKey
        '
        Me.txtNewKey.AcceptsReturn = True
        Me.txtNewKey.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtNewKey.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNewKey.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNewKey.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewKey.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNewKey.Location = New System.Drawing.Point(362, 97)
        Me.txtNewKey.MaxLength = 32
        Me.txtNewKey.Name = "txtNewKey"
        Me.txtNewKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNewKey.Size = New System.Drawing.Size(245, 14)
        Me.txtNewKey.TabIndex = 14
        '
        'txtSMS
        '
        Me.txtSMS.AcceptsReturn = True
        Me.txtSMS.BackColor = System.Drawing.Color.White
        Me.txtSMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMS.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMS.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMS.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMS.Location = New System.Drawing.Point(35, 172)
        Me.txtSMS.MaxLength = 144
        Me.txtSMS.Multiline = True
        Me.txtSMS.Name = "txtSMS"
        Me.txtSMS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMS.Size = New System.Drawing.Size(245, 113)
        Me.txtSMS.TabIndex = 11
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(35, 78)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(182, 18)
        Me.Label27.TabIndex = 19
        Me.Label27.Text = "Select Pre-Defined Message:"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(359, 78)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(130, 17)
        Me.Label28.TabIndex = 18
        Me.Label28.Text = "Define New Message:"
        '
        'Line2
        '
        Me.Line2.BackColor = System.Drawing.SystemColors.WindowText
        Me.Line2.Location = New System.Drawing.Point(324, 40)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(1, 275)
        Me.Line2.TabIndex = 21
        '
        'LabText
        '
        Me.LabText.BackColor = System.Drawing.Color.Transparent
        Me.LabText.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabText.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabText.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabText.Location = New System.Drawing.Point(35, 152)
        Me.LabText.Name = "LabText"
        Me.LabText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabText.Size = New System.Drawing.Size(76, 17)
        Me.LabText.TabIndex = 17
        Me.LabText.Text = "Text to Send:"
        '
        'TabPageSMTP
        '
        Me.TabPageSMTP.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPageSMTP.Controls.Add(Me.labAdminOnly)
        Me.TabPageSMTP.Controls.Add(Me.frameSMSGateway)
        Me.TabPageSMTP.Controls.Add(Me.frameSMTPSettings)
        Me.TabPageSMTP.Controls.Add(Me.cmdFinish)
        Me.TabPageSMTP.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSMTP.Name = "TabPageSMTP"
        Me.TabPageSMTP.Size = New System.Drawing.Size(662, 489)
        Me.TabPageSMTP.TabIndex = 2
        Me.TabPageSMTP.Text = "SMS and SMTP (Mail) Settings"
        Me.TabPageSMTP.UseVisualStyleBackColor = True
        '
        'labAdminOnly
        '
        Me.labAdminOnly.BackColor = System.Drawing.Color.Gainsboro
        Me.labAdminOnly.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labAdminOnly.ForeColor = System.Drawing.Color.Red
        Me.labAdminOnly.Location = New System.Drawing.Point(26, 447)
        Me.labAdminOnly.Name = "labAdminOnly"
        Me.labAdminOnly.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labAdminOnly.Size = New System.Drawing.Size(281, 21)
        Me.labAdminOnly.TabIndex = 28
        Me.labAdminOnly.Text = "Settings on this page for Admin access only.."
        '
        'frameSMSGateway
        '
        Me.frameSMSGateway.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameSMSGateway.Controls.Add(Me.optSmsGatewayDirect)
        Me.frameSMSGateway.Controls.Add(Me.optSmsGatewayGlobal)
        Me.frameSMSGateway.Controls.Add(Me.optSmsGatewayBroadcast)
        Me.frameSMSGateway.Controls.Add(Me.optSmsGatewayBoss)
        Me.frameSMSGateway.Controls.Add(Me.Label13)
        Me.frameSMSGateway.Controls.Add(Me.Label5)
        Me.frameSMSGateway.Controls.Add(Me.txtGatewayURL)
        Me.frameSMSGateway.Controls.Add(Me.txtSmsUsername)
        Me.frameSMSGateway.Controls.Add(Me._txtSmsPassword_0)
        Me.frameSMSGateway.Controls.Add(Me._txtSmsPassword_1)
        Me.frameSMSGateway.Controls.Add(Me.Label30)
        Me.frameSMSGateway.Controls.Add(Me.Label48)
        Me.frameSMSGateway.Controls.Add(Me.Label49)
        Me.frameSMSGateway.Controls.Add(Me.Label50)
        Me.frameSMSGateway.Controls.Add(Me.LabConfirm)
        Me.frameSMSGateway.Location = New System.Drawing.Point(3, 3)
        Me.frameSMSGateway.Name = "frameSMSGateway"
        Me.frameSMSGateway.Size = New System.Drawing.Size(321, 427)
        Me.frameSMSGateway.TabIndex = 23
        Me.frameSMSGateway.TabStop = False
        Me.frameSMSGateway.Text = "frameSMSGateway"
        '
        'optSmsGatewayDirect
        '
        Me.optSmsGatewayDirect.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.optSmsGatewayDirect.Location = New System.Drawing.Point(127, 146)
        Me.optSmsGatewayDirect.Name = "optSmsGatewayDirect"
        Me.optSmsGatewayDirect.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.optSmsGatewayDirect.Size = New System.Drawing.Size(109, 21)
        Me.optSmsGatewayDirect.TabIndex = 31
        Me.optSmsGatewayDirect.TabStop = True
        Me.optSmsGatewayDirect.Text = "directSMS"
        Me.optSmsGatewayDirect.UseVisualStyleBackColor = False
        '
        'optSmsGatewayGlobal
        '
        Me.optSmsGatewayGlobal.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.optSmsGatewayGlobal.Location = New System.Drawing.Point(30, 146)
        Me.optSmsGatewayGlobal.Name = "optSmsGatewayGlobal"
        Me.optSmsGatewayGlobal.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.optSmsGatewayGlobal.Size = New System.Drawing.Size(91, 21)
        Me.optSmsGatewayGlobal.TabIndex = 30
        Me.optSmsGatewayGlobal.TabStop = True
        Me.optSmsGatewayGlobal.Text = "smsGlobal"
        Me.optSmsGatewayGlobal.UseVisualStyleBackColor = False
        '
        'optSmsGatewayBroadcast
        '
        Me.optSmsGatewayBroadcast.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.optSmsGatewayBroadcast.Location = New System.Drawing.Point(127, 119)
        Me.optSmsGatewayBroadcast.Name = "optSmsGatewayBroadcast"
        Me.optSmsGatewayBroadcast.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.optSmsGatewayBroadcast.Size = New System.Drawing.Size(109, 21)
        Me.optSmsGatewayBroadcast.TabIndex = 29
        Me.optSmsGatewayBroadcast.TabStop = True
        Me.optSmsGatewayBroadcast.Text = "smsBroadcast"
        Me.optSmsGatewayBroadcast.UseVisualStyleBackColor = False
        '
        'optSmsGatewayBoss
        '
        Me.optSmsGatewayBoss.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.optSmsGatewayBoss.Location = New System.Drawing.Point(30, 120)
        Me.optSmsGatewayBoss.Name = "optSmsGatewayBoss"
        Me.optSmsGatewayBoss.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.optSmsGatewayBoss.Size = New System.Drawing.Size(91, 21)
        Me.optSmsGatewayBoss.TabIndex = 0
        Me.optSmsGatewayBoss.TabStop = True
        Me.optSmsGatewayBoss.Text = "smsBoss"
        Me.optSmsGatewayBoss.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(15, 38)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(291, 53)
        Me.Label13.TabIndex = 28
        Me.Label13.Text = resources.GetString("Label13.Text")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(147, 14)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "SMS Gateway Settings"
        '
        'txtGatewayURL
        '
        Me.txtGatewayURL.AcceptsReturn = True
        Me.txtGatewayURL.BackColor = System.Drawing.Color.LightGray
        Me.txtGatewayURL.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGatewayURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGatewayURL.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGatewayURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGatewayURL.Location = New System.Drawing.Point(26, 174)
        Me.txtGatewayURL.MaxLength = 0
        Me.txtGatewayURL.Multiline = True
        Me.txtGatewayURL.Name = "txtGatewayURL"
        Me.txtGatewayURL.ReadOnly = True
        Me.txtGatewayURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGatewayURL.Size = New System.Drawing.Size(280, 49)
        Me.txtGatewayURL.TabIndex = 17
        '
        'txtSmsUsername
        '
        Me.txtSmsUsername.AcceptsReturn = True
        Me.txtSmsUsername.BackColor = System.Drawing.Color.White
        Me.txtSmsUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSmsUsername.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSmsUsername.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSmsUsername.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSmsUsername.Location = New System.Drawing.Point(23, 277)
        Me.txtSmsUsername.MaxLength = 64
        Me.txtSmsUsername.Name = "txtSmsUsername"
        Me.txtSmsUsername.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSmsUsername.Size = New System.Drawing.Size(283, 21)
        Me.txtSmsUsername.TabIndex = 18
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(23, 99)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(153, 18)
        Me.Label30.TabIndex = 25
        Me.Label30.Text = "SMS Gateway Host:"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.Transparent
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(23, 238)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(166, 18)
        Me.Label48.TabIndex = 24
        Me.Label48.Text = "SMS Gateway Credentials:"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(23, 261)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(129, 17)
        Me.Label49.TabIndex = 23
        Me.Label49.Text = "Gateway Username"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.Transparent
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(23, 315)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(137, 17)
        Me.Label50.TabIndex = 22
        Me.Label50.Text = "Gateway Password"
        '
        'LabConfirm
        '
        Me.LabConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabConfirm.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabConfirm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabConfirm.Location = New System.Drawing.Point(23, 373)
        Me.LabConfirm.Name = "LabConfirm"
        Me.LabConfirm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabConfirm.Size = New System.Drawing.Size(153, 17)
        Me.LabConfirm.TabIndex = 21
        Me.LabConfirm.Text = "Confirm Password"
        '
        'frameSMTPSettings
        '
        Me.frameSMTPSettings.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameSMTPSettings.Controls.Add(Me.chkHostUsesSSL)
        Me.frameSMTPSettings.Controls.Add(Me.Label12)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPHostPort)
        Me.frameSMTPSettings.Controls.Add(Me.Label11)
        Me.frameSMTPSettings.Controls.Add(Me.Label6)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPServer)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPUsername)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPPassword1)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPPassword2)
        Me.frameSMTPSettings.Controls.Add(Me.Label7)
        Me.frameSMTPSettings.Controls.Add(Me.Label8)
        Me.frameSMTPSettings.Controls.Add(Me.Label9)
        Me.frameSMTPSettings.Controls.Add(Me.Label10)
        Me.frameSMTPSettings.Controls.Add(Me.labSMTPConfirm)
        Me.frameSMTPSettings.Location = New System.Drawing.Point(329, 3)
        Me.frameSMTPSettings.Name = "frameSMTPSettings"
        Me.frameSMTPSettings.Size = New System.Drawing.Size(321, 427)
        Me.frameSMTPSettings.TabIndex = 4
        Me.frameSMTPSettings.TabStop = False
        Me.frameSMTPSettings.Text = "frameSMTPSettings"
        '
        'chkHostUsesSSL
        '
        Me.chkHostUsesSSL.Location = New System.Drawing.Point(132, 149)
        Me.chkHostUsesSSL.Name = "chkHostUsesSSL"
        Me.chkHostUsesSSL.Size = New System.Drawing.Size(102, 22)
        Me.chkHostUsesSSL.TabIndex = 23
        Me.chkHostUsesSSL.Text = "Host Uses SSL"
        Me.chkHostUsesSSL.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(20, 151)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(31, 13)
        Me.Label12.TabIndex = 29
        Me.Label12.Text = "Port:"
        '
        'txtSMTPHostPort
        '
        Me.txtSMTPHostPort.AcceptsReturn = True
        Me.txtSMTPHostPort.BackColor = System.Drawing.Color.White
        Me.txtSMTPHostPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPHostPort.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPHostPort.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPHostPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPHostPort.Location = New System.Drawing.Point(60, 149)
        Me.txtSMTPHostPort.MaxLength = 0
        Me.txtSMTPHostPort.Multiline = True
        Me.txtSMTPHostPort.Name = "txtSMTPHostPort"
        Me.txtSMTPHostPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPHostPort.Size = New System.Drawing.Size(51, 25)
        Me.txtSMTPHostPort.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(20, 38)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(285, 43)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "For sending Emails:  These settings identify the Mail Host name and mailbox crede" & _
    "ntials that your business uses as a Mail server. "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(23, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(135, 14)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "SMTP (Mail) Settings"
        '
        'txtSMTPServer
        '
        Me.txtSMTPServer.AcceptsReturn = True
        Me.txtSMTPServer.BackColor = System.Drawing.Color.White
        Me.txtSMTPServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPServer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPServer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPServer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPServer.Location = New System.Drawing.Point(23, 115)
        Me.txtSMTPServer.MaxLength = 0
        Me.txtSMTPServer.Multiline = True
        Me.txtSMTPServer.Name = "txtSMTPServer"
        Me.txtSMTPServer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPServer.Size = New System.Drawing.Size(282, 25)
        Me.txtSMTPServer.TabIndex = 21
        '
        'txtSMTPUsername
        '
        Me.txtSMTPUsername.AcceptsReturn = True
        Me.txtSMTPUsername.BackColor = System.Drawing.Color.White
        Me.txtSMTPUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPUsername.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPUsername.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPUsername.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPUsername.Location = New System.Drawing.Point(23, 277)
        Me.txtSMTPUsername.MaxLength = 64
        Me.txtSMTPUsername.Name = "txtSMTPUsername"
        Me.txtSMTPUsername.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPUsername.Size = New System.Drawing.Size(282, 21)
        Me.txtSMTPUsername.TabIndex = 24
        '
        'txtSMTPPassword1
        '
        Me.txtSMTPPassword1.AcceptsReturn = True
        Me.txtSMTPPassword1.BackColor = System.Drawing.Color.White
        Me.txtSMTPPassword1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPPassword1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPPassword1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPPassword1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPPassword1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSMTPPassword1.Location = New System.Drawing.Point(23, 335)
        Me.txtSMTPPassword1.MaxLength = 64
        Me.txtSMTPPassword1.Name = "txtSMTPPassword1"
        Me.txtSMTPPassword1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSMTPPassword1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPPassword1.Size = New System.Drawing.Size(230, 21)
        Me.txtSMTPPassword1.TabIndex = 25
        '
        'txtSMTPPassword2
        '
        Me.txtSMTPPassword2.AcceptsReturn = True
        Me.txtSMTPPassword2.BackColor = System.Drawing.Color.White
        Me.txtSMTPPassword2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPPassword2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPPassword2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPPassword2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPPassword2.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSMTPPassword2.Location = New System.Drawing.Point(23, 389)
        Me.txtSMTPPassword2.MaxLength = 64
        Me.txtSMTPPassword2.Name = "txtSMTPPassword2"
        Me.txtSMTPPassword2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSMTPPassword2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPPassword2.Size = New System.Drawing.Size(230, 21)
        Me.txtSMTPPassword2.TabIndex = 26
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(20, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(156, 18)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "SMTP (Mail) Host Name"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(20, 238)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(113, 18)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "SMTP Credentials:"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(23, 261)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(153, 17)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "SMTP (Mailbox) Username"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(20, 315)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(137, 17)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "SMTP (Mailbox) Password"
        '
        'labSMTPConfirm
        '
        Me.labSMTPConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labSMTPConfirm.Cursor = System.Windows.Forms.Cursors.Default
        Me.labSMTPConfirm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labSMTPConfirm.Location = New System.Drawing.Point(23, 373)
        Me.labSMTPConfirm.Name = "labSMTPConfirm"
        Me.labSMTPConfirm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labSMTPConfirm.Size = New System.Drawing.Size(129, 13)
        Me.labSMTPConfirm.TabIndex = 21
        Me.labSMTPConfirm.Text = "Confirm Password"
        '
        'TabPageExchange
        '
        Me.TabPageExchange.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPageExchange.Controls.Add(Me.Label17)
        Me.TabPageExchange.Controls.Add(Me.btnSaveExchange)
        Me.TabPageExchange.Controls.Add(Me.labAdminOnly2)
        Me.TabPageExchange.Controls.Add(Me.grpBoxExchange)
        Me.TabPageExchange.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExchange.Name = "TabPageExchange"
        Me.TabPageExchange.Size = New System.Drawing.Size(662, 489)
        Me.TabPageExchange.TabIndex = 3
        Me.TabPageExchange.Text = "Exchange Calendar"
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label17.Location = New System.Drawing.Point(39, 24)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(118, 86)
        Me.Label17.TabIndex = 30
        Me.Label17.Text = "Exchange Server ON-SITE Job (Calendar) Settings"
        '
        'labAdminOnly2
        '
        Me.labAdminOnly2.BackColor = System.Drawing.Color.Gainsboro
        Me.labAdminOnly2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labAdminOnly2.ForeColor = System.Drawing.Color.Red
        Me.labAdminOnly2.Location = New System.Drawing.Point(39, 369)
        Me.labAdminOnly2.Name = "labAdminOnly2"
        Me.labAdminOnly2.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labAdminOnly2.Size = New System.Drawing.Size(118, 52)
        Me.labAdminOnly2.TabIndex = 29
        Me.labAdminOnly2.Text = "Settings on this page for Admin access only.."
        '
        'grpBoxExchange
        '
        Me.grpBoxExchange.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpBoxExchange.Controls.Add(Me.Label18)
        Me.grpBoxExchange.Controls.Add(Me.Label19)
        Me.grpBoxExchange.Controls.Add(Me.txtExchangeMailbox)
        Me.grpBoxExchange.Controls.Add(Me.txtExchangePassword1)
        Me.grpBoxExchange.Controls.Add(Me.txtExchangePassword2)
        Me.grpBoxExchange.Controls.Add(Me.Label21)
        Me.grpBoxExchange.Controls.Add(Me.Label22)
        Me.grpBoxExchange.Controls.Add(Me.Label23)
        Me.grpBoxExchange.Controls.Add(Me.labExchangeConfirm)
        Me.grpBoxExchange.Location = New System.Drawing.Point(243, 3)
        Me.grpBoxExchange.Name = "grpBoxExchange"
        Me.grpBoxExchange.Size = New System.Drawing.Size(407, 427)
        Me.grpBoxExchange.TabIndex = 5
        Me.grpBoxExchange.TabStop = False
        Me.grpBoxExchange.Text = "Exchange Calendar"
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(20, 38)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(275, 133)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = resources.GetString("Label18.Text")
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(23, 21)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(261, 14)
        Me.Label19.TabIndex = 26
        Me.Label19.Text = "Exchange Server (Calendar) Settings"
        '
        'txtExchangeMailbox
        '
        Me.txtExchangeMailbox.AcceptsReturn = True
        Me.txtExchangeMailbox.BackColor = System.Drawing.Color.White
        Me.txtExchangeMailbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExchangeMailbox.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExchangeMailbox.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExchangeMailbox.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExchangeMailbox.Location = New System.Drawing.Point(23, 234)
        Me.txtExchangeMailbox.MaxLength = 64
        Me.txtExchangeMailbox.Name = "txtExchangeMailbox"
        Me.txtExchangeMailbox.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExchangeMailbox.Size = New System.Drawing.Size(336, 21)
        Me.txtExchangeMailbox.TabIndex = 24
        '
        'txtExchangePassword1
        '
        Me.txtExchangePassword1.AcceptsReturn = True
        Me.txtExchangePassword1.BackColor = System.Drawing.Color.White
        Me.txtExchangePassword1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExchangePassword1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExchangePassword1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExchangePassword1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExchangePassword1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtExchangePassword1.Location = New System.Drawing.Point(23, 295)
        Me.txtExchangePassword1.MaxLength = 64
        Me.txtExchangePassword1.Name = "txtExchangePassword1"
        Me.txtExchangePassword1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtExchangePassword1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExchangePassword1.Size = New System.Drawing.Size(296, 21)
        Me.txtExchangePassword1.TabIndex = 25
        '
        'txtExchangePassword2
        '
        Me.txtExchangePassword2.AcceptsReturn = True
        Me.txtExchangePassword2.BackColor = System.Drawing.Color.White
        Me.txtExchangePassword2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExchangePassword2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExchangePassword2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExchangePassword2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExchangePassword2.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtExchangePassword2.Location = New System.Drawing.Point(23, 354)
        Me.txtExchangePassword2.MaxLength = 64
        Me.txtExchangePassword2.Name = "txtExchangePassword2"
        Me.txtExchangePassword2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtExchangePassword2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExchangePassword2.Size = New System.Drawing.Size(296, 21)
        Me.txtExchangePassword2.TabIndex = 26
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(20, 197)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(245, 19)
        Me.Label21.TabIndex = 24
        Me.Label21.Text = "Exchange Mailbox Credentials:"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(23, 218)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(336, 17)
        Me.Label22.TabIndex = 23
        Me.Label22.Text = "EXCHANGE Mailbox Name  (eg myusername@outlook.com)"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(20, 275)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(233, 17)
        Me.Label23.TabIndex = 22
        Me.Label23.Text = "EXCHANGE (Mailbox) Password"
        '
        'labExchangeConfirm
        '
        Me.labExchangeConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labExchangeConfirm.Cursor = System.Windows.Forms.Cursors.Default
        Me.labExchangeConfirm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labExchangeConfirm.Location = New System.Drawing.Point(23, 338)
        Me.labExchangeConfirm.Name = "labExchangeConfirm"
        Me.labExchangeConfirm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labExchangeConfirm.Size = New System.Drawing.Size(129, 13)
        Me.labExchangeConfirm.TabIndex = 21
        Me.labExchangeConfirm.Text = "Confirm Password"
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(623, 20)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(57, 23)
        Me.cmdClose.TabIndex = 28
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'LabHdr1
        '
        Me.LabHdr1.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LabHdr1.Location = New System.Drawing.Point(18, 12)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr1.Size = New System.Drawing.Size(467, 31)
        Me.LabHdr1.TabIndex = 100
        Me.LabHdr1.Text = "JobMatix-     SMS, SMTP and MS Exchange configuration Settings"
        '
        'frmSMSUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(703, 568)
        Me.Controls.Add(Me.LabHdr1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.TabControl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmSMSUpdate"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "frmSMSUpdate"
        CType(Me.txtSmsPassword, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageSMS.ResumeLayout(False)
        Me.grpBoxReminders.ResumeLayout(False)
        Me.FrameSMS.ResumeLayout(False)
        Me.FrameSMS.PerformLayout()
        Me.TabPageSMTP.ResumeLayout(False)
        Me.frameSMSGateway.ResumeLayout(False)
        Me.frameSMSGateway.PerformLayout()
        Me.frameSMTPSettings.ResumeLayout(False)
        Me.frameSMTPSettings.PerformLayout()
        Me.TabPageExchange.ResumeLayout(False)
        Me.grpBoxExchange.ResumeLayout(False)
        Me.grpBoxExchange.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageSMS As System.Windows.Forms.TabPage
    Public WithEvents FrameSMS As System.Windows.Forms.GroupBox
    Public WithEvents txtNewSMS As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cboSmsKeys As System.Windows.Forms.ComboBox
    Public WithEvents txtNewKey As System.Windows.Forms.TextBox
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents txtSMS As System.Windows.Forms.TextBox
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Line2 As System.Windows.Forms.Label
    Public WithEvents LabText As System.Windows.Forms.Label
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdFinish As System.Windows.Forms.Button
    Friend WithEvents TabPageSMTP As System.Windows.Forms.TabPage
    Friend WithEvents frameSMTPSettings As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtSMTPServer As System.Windows.Forms.TextBox
    Public WithEvents txtSMTPUsername As System.Windows.Forms.TextBox
    Public WithEvents txtSMTPPassword1 As System.Windows.Forms.TextBox
    Public WithEvents txtSMTPPassword2 As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents labSMTPConfirm As System.Windows.Forms.Label
    Public WithEvents LabHdr1 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents txtSMTPHostPort As System.Windows.Forms.TextBox
    Friend WithEvents chkHostUsesSSL As System.Windows.Forms.CheckBox
    Friend WithEvents frameSMSGateway As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtGatewayURL As System.Windows.Forms.TextBox
    Public WithEvents txtSmsUsername As System.Windows.Forms.TextBox
    Public WithEvents _txtSmsPassword_0 As System.Windows.Forms.TextBox
    Public WithEvents _txtSmsPassword_1 As System.Windows.Forms.TextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents Label49 As System.Windows.Forms.Label
    Public WithEvents Label50 As System.Windows.Forms.Label
    Public WithEvents LabConfirm As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents labAdminOnly As System.Windows.Forms.Label
    Friend WithEvents grpBoxReminders As System.Windows.Forms.GroupBox
    Public WithEvents btnOnSiteSave As System.Windows.Forms.Button
    Friend WithEvents cboOnSiteMinsBefore As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboOnSiteWakeUp As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents optSmsGatewayGlobal As System.Windows.Forms.RadioButton
    Friend WithEvents optSmsGatewayBroadcast As System.Windows.Forms.RadioButton
    Friend WithEvents optSmsGatewayBoss As System.Windows.Forms.RadioButton
    Friend WithEvents optSmsGatewayDirect As System.Windows.Forms.RadioButton
    Friend WithEvents TabPageExchange As System.Windows.Forms.TabPage
    Friend WithEvents grpBoxExchange As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents txtExchangeMailbox As System.Windows.Forms.TextBox
    Public WithEvents txtExchangePassword1 As System.Windows.Forms.TextBox
    Public WithEvents txtExchangePassword2 As System.Windows.Forms.TextBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents labExchangeConfirm As System.Windows.Forms.Label
    Friend WithEvents labAdminOnly2 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents btnSaveExchange As System.Windows.Forms.Button
#End Region
End Class