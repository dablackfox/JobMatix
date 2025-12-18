<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmJobMaint3
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
    Public WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents LabHdr1 As System.Windows.Forms.Label
    Public WithEvents ListViewParts As Microsoft.VisualBasic.Compatibility.VB6.ListViewArray
    Public WithEvents chkPrtDocs As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    Public WithEvents cmdAddPart As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Public WithEvents cmdDeletePart As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Public WithEvents optChargeable As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtStaffName As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJobMaint3))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabHdr1 = New System.Windows.Forms.Label()
        Me.ListViewParts = New Microsoft.VisualBasic.Compatibility.VB6.ListViewArray(Me.components)
        Me.chkPrtDocs = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.cmdAddPart = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.cmdDeletePart = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.optChargeable = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtStaffName = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.openDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.grpBoxMain = New System.Windows.Forms.GroupBox()
        Me.Picture2 = New System.Windows.Forms.PictureBox()
        Me.LabVersion = New System.Windows.Forms.Label()
        CType(Me.ListViewParts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPrtDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdAddPart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdDeletePart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optChargeable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStaffName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageList1.Images.SetKeyName(0, "unchecked")
        Me.ImageList1.Images.SetKeyName(1, "checked")
        Me.ImageList1.Images.SetKeyName(2, "alert")
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'LabHdr1
        '
        Me.LabHdr1.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.ForeColor = System.Drawing.Color.Indigo
        Me.LabHdr1.Location = New System.Drawing.Point(11, 2)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr1.Size = New System.Drawing.Size(213, 17)
        Me.LabHdr1.TabIndex = 0
        Me.LabHdr1.Text = "Job Service Record (Modal)"
        Me.LabHdr1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'openDlg1
        '
        Me.openDlg1.FileName = "OpenFileDialog1"
        '
        'grpBoxMain
        '
        Me.grpBoxMain.BackColor = System.Drawing.Color.LightSteelBlue
        Me.grpBoxMain.Location = New System.Drawing.Point(2, 21)
        Me.grpBoxMain.Name = "grpBoxMain"
        Me.grpBoxMain.Size = New System.Drawing.Size(1020, 660)
        Me.grpBoxMain.TabIndex = 69
        Me.grpBoxMain.TabStop = False
        Me.grpBoxMain.Text = "grpBoxMain"
        '
        'Picture2
        '
        Me.Picture2.Location = New System.Drawing.Point(623, 30)
        Me.Picture2.Name = "Picture2"
        Me.Picture2.Size = New System.Drawing.Size(38, 25)
        Me.Picture2.TabIndex = 70
        Me.Picture2.TabStop = False
        '
        'LabVersion
        '
        Me.LabVersion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabVersion.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabVersion.Location = New System.Drawing.Point(806, 10)
        Me.LabVersion.Name = "LabVersion"
        Me.LabVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabVersion.Size = New System.Drawing.Size(211, 10)
        Me.LabVersion.TabIndex = 71
        Me.LabVersion.Text = "LabVersion"
        Me.LabVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmJobMaint3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1029, 689)
        Me.Controls.Add(Me.LabVersion)
        Me.Controls.Add(Me.Picture2)
        Me.Controls.Add(Me.grpBoxMain)
        Me.Controls.Add(Me.LabHdr1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmJobMaint3"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Job Maintenance"
        CType(Me.ListViewParts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPrtDocs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdAddPart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdDeletePart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optChargeable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStaffName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents openDlg1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents grpBoxMain As System.Windows.Forms.GroupBox
    Friend WithEvents Picture2 As System.Windows.Forms.PictureBox
    Public WithEvents LabVersion As System.Windows.Forms.Label
#End Region
End Class