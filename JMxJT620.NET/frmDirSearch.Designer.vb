<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmDirSearch
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
	Public WithEvents txtDirPath As System.Windows.Forms.TextBox
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdOK As System.Windows.Forms.Button
	Public WithEvents txtSearch As System.Windows.Forms.TextBox
	Public WithEvents dirList As Microsoft.VisualBasic.Compatibility.VB6.DirListBox
	Public WithEvents drvList As Microsoft.VisualBasic.Compatibility.VB6.DriveListBox
	Public WithEvents filList As Microsoft.VisualBasic.Compatibility.VB6.FileListBox
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmDirSearch))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.txtDirPath = New System.Windows.Forms.TextBox
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.cmdOK = New System.Windows.Forms.Button
		Me.txtSearch = New System.Windows.Forms.TextBox
		Me.dirList = New Microsoft.VisualBasic.Compatibility.VB6.DirListBox
		Me.drvList = New Microsoft.VisualBasic.Compatibility.VB6.DriveListBox
		Me.filList = New Microsoft.VisualBasic.Compatibility.VB6.FileListBox
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		Me.Text = "frmDirSearch"
		Me.ClientSize = New System.Drawing.Size(439, 415)
		Me.Location = New System.Drawing.Point(4, 23)
		Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmDirSearch"
		Me.txtDirPath.AutoSize = False
		Me.txtDirPath.BackColor = System.Drawing.Color.FromARGB(255, 255, 192)
		Me.txtDirPath.Size = New System.Drawing.Size(289, 23)
		Me.txtDirPath.Location = New System.Drawing.Point(16, 56)
		Me.txtDirPath.ReadOnly = True
		Me.txtDirPath.TabIndex = 8
		Me.txtDirPath.AcceptsReturn = True
		Me.txtDirPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtDirPath.CausesValidation = True
		Me.txtDirPath.Enabled = True
		Me.txtDirPath.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtDirPath.HideSelection = True
		Me.txtDirPath.Maxlength = 0
		Me.txtDirPath.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtDirPath.MultiLine = False
		Me.txtDirPath.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtDirPath.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtDirPath.TabStop = True
		Me.txtDirPath.Visible = True
		Me.txtDirPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtDirPath.Name = "txtDirPath"
		Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCancel.Text = "Cancel"
		Me.cmdCancel.Size = New System.Drawing.Size(49, 19)
		Me.cmdCancel.Location = New System.Drawing.Point(360, 384)
		Me.cmdCancel.TabIndex = 7
		Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancel.CausesValidation = True
		Me.cmdCancel.Enabled = True
		Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancel.TabStop = True
		Me.cmdCancel.Name = "cmdCancel"
		Me.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdOK.Text = "OK"
		Me.cmdOK.Size = New System.Drawing.Size(49, 19)
		Me.cmdOK.Location = New System.Drawing.Point(272, 384)
		Me.cmdOK.TabIndex = 6
		Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
		Me.cmdOK.CausesValidation = True
		Me.cmdOK.Enabled = True
		Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdOK.TabStop = True
		Me.cmdOK.Name = "cmdOK"
		Me.txtSearch.AutoSize = False
		Me.txtSearch.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtSearch.Size = New System.Drawing.Size(81, 23)
		Me.txtSearch.Location = New System.Drawing.Point(328, 32)
		Me.txtSearch.TabIndex = 3
		Me.txtSearch.AcceptsReturn = True
		Me.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtSearch.BackColor = System.Drawing.SystemColors.Window
		Me.txtSearch.CausesValidation = True
		Me.txtSearch.Enabled = True
		Me.txtSearch.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtSearch.HideSelection = True
		Me.txtSearch.ReadOnly = False
		Me.txtSearch.Maxlength = 0
		Me.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtSearch.MultiLine = False
		Me.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtSearch.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtSearch.TabStop = True
		Me.txtSearch.Visible = True
		Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtSearch.Name = "txtSearch"
		Me.dirList.BackColor = System.Drawing.Color.FromARGB(255, 255, 192)
		Me.dirList.Size = New System.Drawing.Size(393, 186)
		Me.dirList.Location = New System.Drawing.Point(16, 80)
		Me.dirList.TabIndex = 2
		Me.dirList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.dirList.CausesValidation = True
		Me.dirList.Enabled = True
		Me.dirList.ForeColor = System.Drawing.SystemColors.WindowText
		Me.dirList.Cursor = System.Windows.Forms.Cursors.Default
		Me.dirList.TabStop = True
		Me.dirList.Visible = True
		Me.dirList.Name = "dirList"
		Me.drvList.Size = New System.Drawing.Size(241, 21)
		Me.drvList.Location = New System.Drawing.Point(16, 32)
		Me.drvList.TabIndex = 1
		Me.drvList.BackColor = System.Drawing.SystemColors.Window
		Me.drvList.CausesValidation = True
		Me.drvList.Enabled = True
		Me.drvList.ForeColor = System.Drawing.SystemColors.WindowText
		Me.drvList.Cursor = System.Windows.Forms.Cursors.Default
		Me.drvList.TabStop = True
		Me.drvList.Visible = True
		Me.drvList.Name = "drvList"
		Me.filList.BackColor = System.Drawing.Color.FromARGB(208, 208, 208)
		Me.filList.Size = New System.Drawing.Size(401, 97)
		Me.filList.Location = New System.Drawing.Point(16, 272)
		Me.filList.TabIndex = 0
		Me.filList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.filList.Archive = True
		Me.filList.CausesValidation = True
		Me.filList.Enabled = True
		Me.filList.ForeColor = System.Drawing.SystemColors.WindowText
		Me.filList.Hidden = False
		Me.filList.Cursor = System.Windows.Forms.Cursors.Default
		Me.filList.SelectionMode = System.Windows.Forms.SelectionMode.One
		Me.filList.Normal = True
		Me.filList.Pattern = "*.*"
		Me.filList.ReadOnly = True
		Me.filList.System = False
		Me.filList.TabStop = True
		Me.filList.TopIndex = 0
		Me.filList.Visible = True
		Me.filList.Name = "filList"
		Me.Label2.Text = "Directory Search"
		Me.Label2.Font = New System.Drawing.Font("Tahoma", 9!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(121, 17)
		Me.Label2.Location = New System.Drawing.Point(16, 8)
		Me.Label2.TabIndex = 5
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Enabled = True
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.UseMnemonic = True
		Me.Label2.Visible = True
		Me.Label2.AutoSize = False
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label2.Name = "Label2"
		Me.Label1.Text = "File Criteria:"
		Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.Size = New System.Drawing.Size(81, 17)
		Me.Label1.Location = New System.Drawing.Point(328, 16)
		Me.Label1.TabIndex = 4
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = False
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.Controls.Add(txtDirPath)
		Me.Controls.Add(cmdCancel)
		Me.Controls.Add(cmdOK)
		Me.Controls.Add(txtSearch)
		Me.Controls.Add(dirList)
		Me.Controls.Add(drvList)
		Me.Controls.Add(filList)
		Me.Controls.Add(Label2)
		Me.Controls.Add(Label1)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class