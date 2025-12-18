<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmNotifyCust
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
        Me.mbIsInitialising = True
        'This call is required by the Windows Form Designer.
		InitializeComponent()
        '-- grh-
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
	Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents txtReason As System.Windows.Forms.TextBox
	Public WithEvents txtCustomerMobile As System.Windows.Forms.TextBox
	Public WithEvents txtCustomerPhone As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LabHdr As System.Windows.Forms.Label
	Public WithEvents OptNotified As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents OptNotifyChoice As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    '==3043.0= Public WithEvents txtSmsPassword As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSend = New System.Windows.Forms.Button()
        Me.cmdEditReference = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.txtCustomerMobile = New System.Windows.Forms.TextBox()
        Me.txtCustomerPhone = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabHdr = New System.Windows.Forms.Label()
        Me.OptNotified = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me._OptNotified_2 = New System.Windows.Forms.RadioButton()
        Me._OptNotified_1 = New System.Windows.Forms.RadioButton()
        Me._OptNotified_0 = New System.Windows.Forms.RadioButton()
        Me.OptNotifyChoice = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.FramePhoneResult = New System.Windows.Forms.GroupBox()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.LabPhoneResult = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.FrameSMS = New System.Windows.Forms.GroupBox()
        Me.cboSmsKeys = New System.Windows.Forms.ComboBox()
        Me.ChkJobNo = New System.Windows.Forms.CheckBox()
        Me.txtSMS = New System.Windows.Forms.TextBox()
        Me.txtSmsDest = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabText = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LabStatus = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.frameEmail = New System.Windows.Forms.GroupBox()
        Me.labEmailTimer = New System.Windows.Forms.Label()
        Me.labSMTPHost = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmdCancelEmail = New System.Windows.Forms.Button()
        Me.labEmailStatus = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtEmailFrom = New System.Windows.Forms.TextBox()
        Me.cmdSendEmail = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtEmailTo = New System.Windows.Forms.TextBox()
        Me.txtEmailSubject = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtEmailText = New System.Windows.Forms.TextBox()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.txtNotifications = New System.Windows.Forms.TextBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        CType(Me.OptNotified, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptNotifyChoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.FramePhoneResult.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.FrameSMS.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.frameEmail.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSend
        '
        Me.cmdSend.BackColor = System.Drawing.Color.Lavender
        Me.cmdSend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSend.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSend.Location = New System.Drawing.Point(534, 192)
        Me.cmdSend.Name = "cmdSend"
        Me.cmdSend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSend.Size = New System.Drawing.Size(89, 32)
        Me.cmdSend.TabIndex = 12
        Me.cmdSend.Text = "Send SMS"
        Me.ToolTip1.SetToolTip(Me.cmdSend, "Send the text message displayed..")
        Me.cmdSend.UseVisualStyleBackColor = False
        '
        'cmdEditReference
        '
        Me.cmdEditReference.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdEditReference.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdEditReference.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEditReference.Location = New System.Drawing.Point(593, 119)
        Me.cmdEditReference.Name = "cmdEditReference"
        Me.cmdEditReference.Size = New System.Drawing.Size(107, 21)
        Me.cmdEditReference.TabIndex = 13
        Me.cmdEditReference.Text = "Edit Reference"
        Me.ToolTip1.SetToolTip(Me.cmdEditReference, "Edit Table of pre-defined  messages and SMS/SMTP Settings.")
        Me.cmdEditReference.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(525, 560)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(71, 29)
        Me.cmdCancel.TabIndex = 14
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReason.Location = New System.Drawing.Point(12, 113)
        Me.txtReason.MaxLength = 24
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(483, 27)
        Me.txtReason.TabIndex = 3
        '
        'txtCustomerMobile
        '
        Me.txtCustomerMobile.AcceptsReturn = True
        Me.txtCustomerMobile.BackColor = System.Drawing.Color.White
        Me.txtCustomerMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustomerMobile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerMobile.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerMobile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerMobile.Location = New System.Drawing.Point(555, 46)
        Me.txtCustomerMobile.MaxLength = 0
        Me.txtCustomerMobile.Name = "txtCustomerMobile"
        Me.txtCustomerMobile.ReadOnly = True
        Me.txtCustomerMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerMobile.Size = New System.Drawing.Size(145, 15)
        Me.txtCustomerMobile.TabIndex = 2
        '
        'txtCustomerPhone
        '
        Me.txtCustomerPhone.AcceptsReturn = True
        Me.txtCustomerPhone.BackColor = System.Drawing.Color.White
        Me.txtCustomerPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustomerPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerPhone.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerPhone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerPhone.Location = New System.Drawing.Point(555, 10)
        Me.txtCustomerPhone.MaxLength = 0
        Me.txtCustomerPhone.Name = "txtCustomerPhone"
        Me.txtCustomerPhone.ReadOnly = True
        Me.txtCustomerPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerPhone.Size = New System.Drawing.Size(145, 15)
        Me.txtCustomerPhone.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(12, 96)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(145, 17)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Reason   for Notify:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(501, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(57, 17)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Mobile:"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(501, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(48, 17)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Phone:"
        '
        'LabHdr
        '
        Me.LabHdr.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabHdr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr.Location = New System.Drawing.Point(12, 28)
        Me.LabHdr.Name = "LabHdr"
        Me.LabHdr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr.Size = New System.Drawing.Size(481, 61)
        Me.LabHdr.TabIndex = 5
        Me.LabHdr.Text = "LabHdr"
        '
        'OptNotified
        '
        '
        '_OptNotified_2
        '
        Me._OptNotified_2.BackColor = System.Drawing.Color.Transparent
        Me._OptNotified_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptNotified_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptNotified.SetIndex(Me._OptNotified_2, CType(2, Short))
        Me._OptNotified_2.Location = New System.Drawing.Point(40, 124)
        Me._OptNotified_2.Name = "_OptNotified_2"
        Me._OptNotified_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptNotified_2.Size = New System.Drawing.Size(186, 38)
        Me._OptNotified_2.TabIndex = 6
        Me._OptNotified_2.TabStop = True
        Me._OptNotified_2.Text = "Customer Unreachable"
        Me._OptNotified_2.UseVisualStyleBackColor = False
        '
        '_OptNotified_1
        '
        Me._OptNotified_1.BackColor = System.Drawing.Color.Transparent
        Me._OptNotified_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptNotified_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptNotified.SetIndex(Me._OptNotified_1, CType(1, Short))
        Me._OptNotified_1.Location = New System.Drawing.Point(40, 88)
        Me._OptNotified_1.Name = "_OptNotified_1"
        Me._OptNotified_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptNotified_1.Size = New System.Drawing.Size(214, 39)
        Me._OptNotified_1.TabIndex = 5
        Me._OptNotified_1.TabStop = True
        Me._OptNotified_1.Text = "Customer Unreachable - Left Message"
        Me._OptNotified_1.UseVisualStyleBackColor = False
        '
        '_OptNotified_0
        '
        Me._OptNotified_0.BackColor = System.Drawing.Color.Transparent
        Me._OptNotified_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptNotified_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptNotified.SetIndex(Me._OptNotified_0, CType(0, Short))
        Me._OptNotified_0.Location = New System.Drawing.Point(40, 55)
        Me._OptNotified_0.Name = "_OptNotified_0"
        Me._OptNotified_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptNotified_0.Size = New System.Drawing.Size(214, 38)
        Me._OptNotified_0.TabIndex = 4
        Me._OptNotified_0.TabStop = True
        Me._OptNotified_0.Text = "Customer Notified OKAY"
        Me._OptNotified_0.UseVisualStyleBackColor = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(11, 151)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(696, 399)
        Me.TabControl1.TabIndex = 46
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage1.Controls.Add(Me.FramePhoneResult)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(688, 373)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Telephone"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'FramePhoneResult
        '
        Me.FramePhoneResult.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FramePhoneResult.Controls.Add(Me.cmdOK)
        Me.FramePhoneResult.Controls.Add(Me._OptNotified_2)
        Me.FramePhoneResult.Controls.Add(Me._OptNotified_1)
        Me.FramePhoneResult.Controls.Add(Me._OptNotified_0)
        Me.FramePhoneResult.Controls.Add(Me.LabPhoneResult)
        Me.FramePhoneResult.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FramePhoneResult.Location = New System.Drawing.Point(12, 10)
        Me.FramePhoneResult.Name = "FramePhoneResult"
        Me.FramePhoneResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FramePhoneResult.Size = New System.Drawing.Size(434, 221)
        Me.FramePhoneResult.TabIndex = 1
        Me.FramePhoneResult.TabStop = False
        Me.FramePhoneResult.Text = "FramePhoneResult"
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.Color.AliceBlue
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(309, 170)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(88, 27)
        Me.cmdOK.TabIndex = 7
        Me.cmdOK.Text = "Save Result"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'LabPhoneResult
        '
        Me.LabPhoneResult.BackColor = System.Drawing.Color.Transparent
        Me.LabPhoneResult.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPhoneResult.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPhoneResult.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPhoneResult.Location = New System.Drawing.Point(16, 31)
        Me.LabPhoneResult.Name = "LabPhoneResult"
        Me.LabPhoneResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPhoneResult.Size = New System.Drawing.Size(189, 18)
        Me.LabPhoneResult.TabIndex = 6
        Me.LabPhoneResult.Text = "Telephoned Customer Result:"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage2.Controls.Add(Me.FrameSMS)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(688, 373)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "SMS"
        '
        'FrameSMS
        '
        Me.FrameSMS.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameSMS.Controls.Add(Me.cboSmsKeys)
        Me.FrameSMS.Controls.Add(Me.ChkJobNo)
        Me.FrameSMS.Controls.Add(Me.txtSMS)
        Me.FrameSMS.Controls.Add(Me.txtSmsDest)
        Me.FrameSMS.Controls.Add(Me.cmdSend)
        Me.FrameSMS.Controls.Add(Me.Label5)
        Me.FrameSMS.Controls.Add(Me.LabText)
        Me.FrameSMS.Controls.Add(Me.Label9)
        Me.FrameSMS.Controls.Add(Me.LabStatus)
        Me.FrameSMS.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameSMS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameSMS.Location = New System.Drawing.Point(6, -2)
        Me.FrameSMS.Name = "FrameSMS"
        Me.FrameSMS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameSMS.Size = New System.Drawing.Size(673, 366)
        Me.FrameSMS.TabIndex = 19
        Me.FrameSMS.TabStop = False
        '
        'cboSmsKeys
        '
        Me.cboSmsKeys.BackColor = System.Drawing.Color.White
        Me.cboSmsKeys.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSmsKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSmsKeys.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSmsKeys.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSmsKeys.Location = New System.Drawing.Point(202, 51)
        Me.cboSmsKeys.Name = "cboSmsKeys"
        Me.cboSmsKeys.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSmsKeys.Size = New System.Drawing.Size(292, 20)
        Me.cboSmsKeys.TabIndex = 8
        '
        'ChkJobNo
        '
        Me.ChkJobNo.BackColor = System.Drawing.Color.Transparent
        Me.ChkJobNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkJobNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkJobNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkJobNo.Location = New System.Drawing.Point(526, 90)
        Me.ChkJobNo.Name = "ChkJobNo"
        Me.ChkJobNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkJobNo.Size = New System.Drawing.Size(97, 32)
        Me.ChkJobNo.TabIndex = 10
        Me.ChkJobNo.Text = "Include JobNo in Message."
        Me.ChkJobNo.UseVisualStyleBackColor = False
        '
        'txtSMS
        '
        Me.txtSMS.AcceptsReturn = True
        Me.txtSMS.BackColor = System.Drawing.SystemColors.Window
        Me.txtSMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMS.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMS.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMS.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMS.Location = New System.Drawing.Point(202, 90)
        Me.txtSMS.MaxLength = 144
        Me.txtSMS.Multiline = True
        Me.txtSMS.Name = "txtSMS"
        Me.txtSMS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMS.Size = New System.Drawing.Size(292, 96)
        Me.txtSMS.TabIndex = 9
        '
        'txtSmsDest
        '
        Me.txtSmsDest.AcceptsReturn = True
        Me.txtSmsDest.BackColor = System.Drawing.Color.White
        Me.txtSmsDest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSmsDest.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSmsDest.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSmsDest.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSmsDest.Location = New System.Drawing.Point(202, 202)
        Me.txtSmsDest.MaxLength = 0
        Me.txtSmsDest.Name = "txtSmsDest"
        Me.txtSmsDest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSmsDest.Size = New System.Drawing.Size(220, 21)
        Me.txtSmsDest.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(17, 54)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(179, 13)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Select Pre-defined Message:"
        '
        'LabText
        '
        Me.LabText.BackColor = System.Drawing.Color.Transparent
        Me.LabText.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabText.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabText.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabText.Location = New System.Drawing.Point(98, 89)
        Me.LabText.Name = "LabText"
        Me.LabText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabText.Size = New System.Drawing.Size(84, 21)
        Me.LabText.TabIndex = 39
        Me.LabText.Text = "Text to Send:"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(20, 205)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(162, 17)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "Destination Phone Number:"
        '
        'LabStatus
        '
        Me.LabStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabStatus.Location = New System.Drawing.Point(20, 248)
        Me.LabStatus.Name = "LabStatus"
        Me.LabStatus.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.LabStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabStatus.Size = New System.Drawing.Size(474, 100)
        Me.LabStatus.TabIndex = 33
        Me.LabStatus.Text = "LabStatus"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.frameEmail)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(688, 373)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Email"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'frameEmail
        '
        Me.frameEmail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameEmail.Controls.Add(Me.labEmailTimer)
        Me.frameEmail.Controls.Add(Me.labSMTPHost)
        Me.frameEmail.Controls.Add(Me.Label8)
        Me.frameEmail.Controls.Add(Me.cmdCancelEmail)
        Me.frameEmail.Controls.Add(Me.labEmailStatus)
        Me.frameEmail.Controls.Add(Me.Label2)
        Me.frameEmail.Controls.Add(Me.txtEmailFrom)
        Me.frameEmail.Controls.Add(Me.cmdSendEmail)
        Me.frameEmail.Controls.Add(Me.Label7)
        Me.frameEmail.Controls.Add(Me.Label6)
        Me.frameEmail.Controls.Add(Me.txtEmailTo)
        Me.frameEmail.Controls.Add(Me.txtEmailSubject)
        Me.frameEmail.Controls.Add(Me.Label1)
        Me.frameEmail.Controls.Add(Me.txtEmailText)
        Me.frameEmail.Location = New System.Drawing.Point(1, 0)
        Me.frameEmail.Name = "frameEmail"
        Me.frameEmail.Size = New System.Drawing.Size(687, 366)
        Me.frameEmail.TabIndex = 0
        Me.frameEmail.TabStop = False
        Me.frameEmail.Text = "frameEmail"
        '
        'labEmailTimer
        '
        Me.labEmailTimer.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labEmailTimer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.labEmailTimer.Location = New System.Drawing.Point(513, 281)
        Me.labEmailTimer.Name = "labEmailTimer"
        Me.labEmailTimer.Size = New System.Drawing.Size(92, 13)
        Me.labEmailTimer.TabIndex = 53
        Me.labEmailTimer.Text = "labEmailTimer"
        '
        'labSMTPHost
        '
        Me.labSMTPHost.BackColor = System.Drawing.Color.Gainsboro
        Me.labSMTPHost.Location = New System.Drawing.Point(499, 42)
        Me.labSMTPHost.Name = "labSMTPHost"
        Me.labSMTPHost.Size = New System.Drawing.Size(174, 37)
        Me.labSMTPHost.TabIndex = 52
        Me.labSMTPHost.Text = "labSMTPHost"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(500, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(58, 13)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "SMTP Host"
        '
        'cmdCancelEmail
        '
        Me.cmdCancelEmail.BackColor = System.Drawing.Color.Lavender
        Me.cmdCancelEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancelEmail.Location = New System.Drawing.Point(602, 303)
        Me.cmdCancelEmail.Name = "cmdCancelEmail"
        Me.cmdCancelEmail.Size = New System.Drawing.Size(71, 39)
        Me.cmdCancelEmail.TabIndex = 21
        Me.cmdCancelEmail.Text = "Cancel Email"
        Me.cmdCancelEmail.UseVisualStyleBackColor = False
        '
        'labEmailStatus
        '
        Me.labEmailStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labEmailStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.labEmailStatus.Location = New System.Drawing.Point(485, 104)
        Me.labEmailStatus.Name = "labEmailStatus"
        Me.labEmailStatus.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labEmailStatus.Size = New System.Drawing.Size(193, 152)
        Me.labEmailStatus.TabIndex = 49
        Me.labEmailStatus.Text = "labEmailStatus"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(39, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(144, 17)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Emailing From"
        '
        'txtEmailFrom
        '
        Me.txtEmailFrom.AcceptsReturn = True
        Me.txtEmailFrom.BackColor = System.Drawing.Color.LightGray
        Me.txtEmailFrom.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtEmailFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmailFrom.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmailFrom.Location = New System.Drawing.Point(42, 42)
        Me.txtEmailFrom.MaxLength = 144
        Me.txtEmailFrom.Multiline = True
        Me.txtEmailFrom.Name = "txtEmailFrom"
        Me.txtEmailFrom.ReadOnly = True
        Me.txtEmailFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmailFrom.Size = New System.Drawing.Size(426, 24)
        Me.txtEmailFrom.TabIndex = 16
        '
        'cmdSendEmail
        '
        Me.cmdSendEmail.BackColor = System.Drawing.Color.Lavender
        Me.cmdSendEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSendEmail.Location = New System.Drawing.Point(516, 303)
        Me.cmdSendEmail.Name = "cmdSendEmail"
        Me.cmdSendEmail.Size = New System.Drawing.Size(71, 39)
        Me.cmdSendEmail.TabIndex = 20
        Me.cmdSendEmail.Text = "Send Email"
        Me.cmdSendEmail.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(39, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(144, 17)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Email To"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(39, 137)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(144, 17)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Subject"
        '
        'txtEmailTo
        '
        Me.txtEmailTo.AcceptsReturn = True
        Me.txtEmailTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmailTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmailTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmailTo.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmailTo.Location = New System.Drawing.Point(42, 102)
        Me.txtEmailTo.MaxLength = 144
        Me.txtEmailTo.Multiline = True
        Me.txtEmailTo.Name = "txtEmailTo"
        Me.txtEmailTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmailTo.Size = New System.Drawing.Size(426, 24)
        Me.txtEmailTo.TabIndex = 17
        '
        'txtEmailSubject
        '
        Me.txtEmailSubject.AcceptsReturn = True
        Me.txtEmailSubject.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmailSubject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmailSubject.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmailSubject.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailSubject.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmailSubject.Location = New System.Drawing.Point(42, 155)
        Me.txtEmailSubject.MaxLength = 144
        Me.txtEmailSubject.Multiline = True
        Me.txtEmailSubject.Name = "txtEmailSubject"
        Me.txtEmailSubject.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmailSubject.Size = New System.Drawing.Size(426, 26)
        Me.txtEmailSubject.TabIndex = 18
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(39, 194)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(144, 17)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Email Text to Send:"
        '
        'txtEmailText
        '
        Me.txtEmailText.AcceptsReturn = True
        Me.txtEmailText.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmailText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmailText.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmailText.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailText.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmailText.Location = New System.Drawing.Point(42, 213)
        Me.txtEmailText.MaxLength = 4000
        Me.txtEmailText.Multiline = True
        Me.txtEmailText.Name = "txtEmailText"
        Me.txtEmailText.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmailText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEmailText.Size = New System.Drawing.Size(426, 129)
        Me.txtEmailText.TabIndex = 19
        '
        'TabPage4
        '
        Me.TabPage4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPage4.Controls.Add(Me.txtNotifications)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(688, 373)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Previous Contact"
        '
        'txtNotifications
        '
        Me.txtNotifications.AcceptsReturn = True
        Me.txtNotifications.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNotifications.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotifications.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNotifications.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotifications.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNotifications.Location = New System.Drawing.Point(3, 3)
        Me.txtNotifications.MaxLength = 0
        Me.txtNotifications.Multiline = True
        Me.txtNotifications.Name = "txtNotifications"
        Me.txtNotifications.ReadOnly = True
        Me.txtNotifications.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNotifications.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotifications.Size = New System.Drawing.Size(682, 365)
        Me.txtNotifications.TabIndex = 10
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClose.Location = New System.Drawing.Point(622, 560)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(72, 29)
        Me.cmdClose.TabIndex = 15
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(12, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(126, 14)
        Me.Label10.TabIndex = 48
        Me.Label10.Text = "Notifying Customer"
        '
        'frmNotifyCust
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(719, 600)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cmdEditReference)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.txtReason)
        Me.Controls.Add(Me.txtCustomerMobile)
        Me.Controls.Add(Me.txtCustomerPhone)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LabHdr)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmNotifyCust"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "frmNotifyCust"
        CType(Me.OptNotified, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptNotifyChoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.FramePhoneResult.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.FrameSMS.ResumeLayout(False)
        Me.FrameSMS.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.frameEmail.ResumeLayout(False)
        Me.frameEmail.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents FramePhoneResult As System.Windows.Forms.GroupBox
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents _OptNotified_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptNotified_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptNotified_0 As System.Windows.Forms.RadioButton
    Public WithEvents LabPhoneResult As System.Windows.Forms.Label
    Public WithEvents FrameSMS As System.Windows.Forms.GroupBox
    Public WithEvents cboSmsKeys As System.Windows.Forms.ComboBox
    Public WithEvents ChkJobNo As System.Windows.Forms.CheckBox
    Public WithEvents txtSMS As System.Windows.Forms.TextBox
    Public WithEvents txtSmsDest As System.Windows.Forms.TextBox
    Public WithEvents cmdSend As System.Windows.Forms.Button
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents LabText As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents LabStatus As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents txtNotifications As System.Windows.Forms.TextBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdEditReference As System.Windows.Forms.Button
    Friend WithEvents frameEmail As System.Windows.Forms.GroupBox
    Public WithEvents txtEmailSubject As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtEmailText As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtEmailTo As System.Windows.Forms.TextBox
    Friend WithEvents cmdSendEmail As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtEmailFrom As System.Windows.Forms.TextBox
    Friend WithEvents labEmailStatus As System.Windows.Forms.Label
    Friend WithEvents cmdCancelEmail As System.Windows.Forms.Button
    Friend WithEvents labSMTPHost As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents labEmailTimer As System.Windows.Forms.Label
#End Region 
End Class