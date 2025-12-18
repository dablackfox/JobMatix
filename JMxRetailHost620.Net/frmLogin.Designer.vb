<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLogin
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
	Public dlg1Open As System.Windows.Forms.OpenFileDialog
	Public WithEvents cmdBrowse As System.Windows.Forms.Button
	Public WithEvents txtServer As System.Windows.Forms.TextBox
	Public WithEvents txtUserName As System.Windows.Forms.TextBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents txtPassword As System.Windows.Forms.TextBox
    Public WithEvents LabTitle As System.Windows.Forms.Label
    Public WithEvents labServer As System.Windows.Forms.Label
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.dlg1Open = New System.Windows.Forms.OpenFileDialog
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.txtServer = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.LabTitle = New System.Windows.Forms.Label
        Me.labServer = New System.Windows.Forms.Label
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.labStatus = New System.Windows.Forms.Label
        Me.cmdOK = New System.Windows.Forms.Button
        Me.listServers = New System.Windows.Forms.ListBox
        Me.labPassword = New System.Windows.Forms.Label
        Me.labUsername = New System.Windows.Forms.Label
        Me.labServerSearch = New System.Windows.Forms.Label
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdBrowse
        '
        Me.cmdBrowse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBrowse.Location = New System.Drawing.Point(247, 64)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBrowse.Size = New System.Drawing.Size(58, 23)
        Me.cmdBrowse.TabIndex = 3
        Me.cmdBrowse.Text = "Browse..."
        Me.cmdBrowse.UseVisualStyleBackColor = False
        '
        'txtServer
        '
        Me.txtServer.AcceptsReturn = True
        Me.txtServer.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.txtServer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtServer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServer.Location = New System.Drawing.Point(30, 87)
        Me.txtServer.MaxLength = 0
        Me.txtServer.Multiline = True
        Me.txtServer.Name = "txtServer"
        Me.txtServer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServer.Size = New System.Drawing.Size(275, 33)
        Me.txtServer.TabIndex = 1
        '
        'txtUserName
        '
        Me.txtUserName.AcceptsReturn = True
        Me.txtUserName.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUserName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserName.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserName.Location = New System.Drawing.Point(30, 160)
        Me.txtUserName.MaxLength = 0
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserName.Size = New System.Drawing.Size(191, 13)
        Me.txtUserName.TabIndex = 4
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(382, 230)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(52, 24)
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'txtPassword
        '
        Me.txtPassword.AcceptsReturn = True
        Me.txtPassword.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPassword.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPassword.Location = New System.Drawing.Point(30, 204)
        Me.txtPassword.MaxLength = 0
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPassword.Size = New System.Drawing.Size(191, 13)
        Me.txtPassword.TabIndex = 5
        '
        'LabTitle
        '
        Me.LabTitle.BackColor = System.Drawing.Color.Transparent
        Me.LabTitle.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabTitle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabTitle.ForeColor = System.Drawing.Color.Blue
        Me.LabTitle.Location = New System.Drawing.Point(13, 11)
        Me.LabTitle.Name = "LabTitle"
        Me.LabTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabTitle.Size = New System.Drawing.Size(214, 33)
        Me.LabTitle.TabIndex = 9
        Me.LabTitle.Text = "LabTitle"
        '
        'labServer
        '
        Me.labServer.BackColor = System.Drawing.Color.Transparent
        Me.labServer.Cursor = System.Windows.Forms.Cursors.Default
        Me.labServer.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labServer.ForeColor = System.Drawing.Color.Blue
        Me.labServer.Location = New System.Drawing.Point(27, 69)
        Me.labServer.Name = "labServer"
        Me.labServer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labServer.Size = New System.Drawing.Size(194, 18)
        Me.labServer.TabIndex = 7
        Me.labServer.Text = "Server"
        '
        'labStatus
        '
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.ForeColor = System.Drawing.Color.Firebrick
        Me.labStatus.Location = New System.Drawing.Point(244, 5)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Size = New System.Drawing.Size(190, 53)
        Me.labStatus.TabIndex = 10
        Me.labStatus.Text = "labStatus"
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdOK.Location = New System.Drawing.Point(299, 232)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(52, 22)
        Me.cmdOK.TabIndex = 6
        Me.cmdOK.Text = "Login"
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'listServers
        '
        Me.listServers.BackColor = System.Drawing.Color.Lavender
        Me.listServers.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listServers.FormattingEnabled = True
        Me.listServers.Location = New System.Drawing.Point(57, 238)
        Me.listServers.Name = "listServers"
        Me.listServers.ScrollAlwaysVisible = True
        Me.listServers.Size = New System.Drawing.Size(170, 39)
        Me.listServers.TabIndex = 11
        '
        'labPassword
        '
        Me.labPassword.AutoSize = True
        Me.labPassword.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPassword.ForeColor = System.Drawing.Color.Blue
        Me.labPassword.Location = New System.Drawing.Point(29, 187)
        Me.labPassword.Name = "labPassword"
        Me.labPassword.Size = New System.Drawing.Size(61, 13)
        Me.labPassword.TabIndex = 12
        Me.labPassword.Text = "Password"
        '
        'labUsername
        '
        Me.labUsername.AutoSize = True
        Me.labUsername.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labUsername.ForeColor = System.Drawing.Color.Blue
        Me.labUsername.Location = New System.Drawing.Point(27, 144)
        Me.labUsername.Name = "labUsername"
        Me.labUsername.Size = New System.Drawing.Size(65, 13)
        Me.labUsername.TabIndex = 13
        Me.labUsername.Text = "UserName"
        '
        'labServerSearch
        '
        Me.labServerSearch.Location = New System.Drawing.Point(320, 67)
        Me.labServerSearch.Name = "labServerSearch"
        Me.labServerSearch.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labServerSearch.Size = New System.Drawing.Size(161, 113)
        Me.labServerSearch.TabIndex = 14
        Me.labServerSearch.Text = "labServerSearch"
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(487, 287)
        Me.ControlBox = False
        Me.Controls.Add(Me.labServerSearch)
        Me.Controls.Add(Me.labUsername)
        Me.Controls.Add(Me.labPassword)
        Me.Controls.Add(Me.listServers)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.labStatus)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Controls.Add(Me.txtServer)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.LabTitle)
        Me.Controls.Add(Me.labServer)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(189, 232)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogin"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " SQL Login"
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents labStatus As System.Windows.Forms.Label
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents listServers As System.Windows.Forms.ListBox
    Friend WithEvents labPassword As System.Windows.Forms.Label
    Friend WithEvents labUsername As System.Windows.Forms.Label
    Friend WithEvents labServerSearch As System.Windows.Forms.Label
#End Region
End Class