<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEdit
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.panelEdit = New System.Windows.Forms.Panel()
        Me.chkModel = New System.Windows.Forms.CheckBox()
        Me.DTPickerModel = New System.Windows.Forms.DateTimePicker()
        Me.picImage1 = New System.Windows.Forms.PictureBox()
        Me.txtModelData = New System.Windows.Forms.TextBox()
        Me.labModelCap = New System.Windows.Forms.Label()
        Me.cmdModelData = New System.Windows.Forms.Button()
        Me.labModelJoin = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUnfinished = New System.Windows.Forms.TextBox()
        Me.cmdSaveExit = New System.Windows.Forms.Button()
        Me.labVersion = New System.Windows.Forms.Label()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.labTitle = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpBoxMainFooter = New System.Windows.Forms.GroupBox()
        Me.labKeyInfo = New System.Windows.Forms.Label()
        Me.OpenDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.panelEdit.SuspendLayout()
        CType(Me.picImage1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxMainFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelEdit
        '
        Me.panelEdit.AutoScroll = True
        Me.panelEdit.AutoScrollMargin = New System.Drawing.Size(5, 5)
        Me.panelEdit.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.panelEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelEdit.Controls.Add(Me.chkModel)
        Me.panelEdit.Controls.Add(Me.DTPickerModel)
        Me.panelEdit.Controls.Add(Me.picImage1)
        Me.panelEdit.Controls.Add(Me.txtModelData)
        Me.panelEdit.Controls.Add(Me.labModelCap)
        Me.panelEdit.Controls.Add(Me.cmdModelData)
        Me.panelEdit.Controls.Add(Me.labModelJoin)
        Me.panelEdit.Location = New System.Drawing.Point(12, 45)
        Me.panelEdit.Name = "panelEdit"
        Me.panelEdit.Size = New System.Drawing.Size(697, 413)
        Me.panelEdit.TabIndex = 7
        '
        'chkModel
        '
        Me.chkModel.AutoSize = True
        Me.chkModel.Location = New System.Drawing.Point(439, 34)
        Me.chkModel.Name = "chkModel"
        Me.chkModel.Size = New System.Drawing.Size(70, 17)
        Me.chkModel.TabIndex = 9
        Me.chkModel.Text = "chkModel"
        Me.chkModel.UseVisualStyleBackColor = True
        '
        'DTPickerModel
        '
        Me.DTPickerModel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPickerModel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPickerModel.Location = New System.Drawing.Point(367, 34)
        Me.DTPickerModel.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.DTPickerModel.Name = "DTPickerModel"
        Me.DTPickerModel.ShowUpDown = True
        Me.DTPickerModel.Size = New System.Drawing.Size(62, 21)
        Me.DTPickerModel.TabIndex = 8
        '
        'picImage1
        '
        Me.picImage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.picImage1.Location = New System.Drawing.Point(518, 15)
        Me.picImage1.Name = "picImage1"
        Me.picImage1.Size = New System.Drawing.Size(146, 132)
        Me.picImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picImage1.TabIndex = 0
        Me.picImage1.TabStop = False
        '
        'txtModelData
        '
        Me.txtModelData.BackColor = System.Drawing.Color.White
        Me.txtModelData.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtModelData.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModelData.Location = New System.Drawing.Point(127, 33)
        Me.txtModelData.Name = "txtModelData"
        Me.txtModelData.Size = New System.Drawing.Size(91, 14)
        Me.txtModelData.TabIndex = 1
        Me.txtModelData.Text = "txtModelData"
        '
        'labModelCap
        '
        Me.labModelCap.BackColor = System.Drawing.Color.Transparent
        Me.labModelCap.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labModelCap.Location = New System.Drawing.Point(18, 31)
        Me.labModelCap.Name = "labModelCap"
        Me.labModelCap.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.labModelCap.Size = New System.Drawing.Size(103, 17)
        Me.labModelCap.TabIndex = 0
        Me.labModelCap.Text = "labModelCap"
        '
        'cmdModelData
        '
        Me.cmdModelData.Location = New System.Drawing.Point(306, 29)
        Me.cmdModelData.Name = "cmdModelData"
        Me.cmdModelData.Size = New System.Drawing.Size(58, 23)
        Me.cmdModelData.TabIndex = 5
        Me.cmdModelData.Text = "Lookup.."
        Me.cmdModelData.UseVisualStyleBackColor = True
        '
        'labModelJoin
        '
        Me.labModelJoin.BackColor = System.Drawing.Color.LemonChiffon
        Me.labModelJoin.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labModelJoin.Location = New System.Drawing.Point(221, 35)
        Me.labModelJoin.Name = "labModelJoin"
        Me.labModelJoin.Size = New System.Drawing.Size(82, 13)
        Me.labModelJoin.TabIndex = 2
        Me.labModelJoin.Text = "labModelJoin"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(246, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Data Still needed."
        '
        'txtUnfinished
        '
        Me.txtUnfinished.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtUnfinished.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUnfinished.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnfinished.Location = New System.Drawing.Point(239, 37)
        Me.txtUnfinished.Multiline = True
        Me.txtUnfinished.Name = "txtUnfinished"
        Me.txtUnfinished.Size = New System.Drawing.Size(203, 30)
        Me.txtUnfinished.TabIndex = 10
        '
        'cmdSaveExit
        '
        Me.cmdSaveExit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdSaveExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSaveExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveExit.Location = New System.Drawing.Point(602, 40)
        Me.cmdSaveExit.Name = "cmdSaveExit"
        Me.cmdSaveExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSaveExit.Size = New System.Drawing.Size(63, 22)
        Me.cmdSaveExit.TabIndex = 92
        Me.cmdSaveExit.Text = "Save/Exit"
        Me.ToolTip1.SetToolTip(Me.cmdSaveExit, "Save Changes and Exit")
        Me.cmdSaveExit.UseVisualStyleBackColor = False
        '
        'labVersion
        '
        Me.labVersion.AutoSize = True
        Me.labVersion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labVersion.Location = New System.Drawing.Point(6, 58)
        Me.labVersion.Name = "labVersion"
        Me.labVersion.Size = New System.Drawing.Size(51, 12)
        Me.labVersion.TabIndex = 8
        Me.labVersion.Text = "labVersion"
        '
        'labStatus
        '
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.ForeColor = System.Drawing.Color.Firebrick
        Me.labStatus.Location = New System.Drawing.Point(433, 9)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Size = New System.Drawing.Size(181, 20)
        Me.labStatus.TabIndex = 7
        Me.labStatus.Text = "labStatus"
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(464, 40)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(57, 22)
        Me.cmdCancel.TabIndex = 90
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(539, 40)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(57, 22)
        Me.cmdSave.TabIndex = 91
        Me.cmdSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Changes")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'labTitle
        '
        Me.labTitle.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labTitle.Location = New System.Drawing.Point(17, 8)
        Me.labTitle.Name = "labTitle"
        Me.labTitle.Size = New System.Drawing.Size(352, 33)
        Me.labTitle.TabIndex = 10
        Me.labTitle.Text = "labTitle"
        '
        'grpBoxMainFooter
        '
        Me.grpBoxMainFooter.Controls.Add(Me.Label1)
        Me.grpBoxMainFooter.Controls.Add(Me.labKeyInfo)
        Me.grpBoxMainFooter.Controls.Add(Me.txtUnfinished)
        Me.grpBoxMainFooter.Controls.Add(Me.labVersion)
        Me.grpBoxMainFooter.Controls.Add(Me.cmdSaveExit)
        Me.grpBoxMainFooter.Controls.Add(Me.cmdSave)
        Me.grpBoxMainFooter.Controls.Add(Me.cmdCancel)
        Me.grpBoxMainFooter.Location = New System.Drawing.Point(12, 458)
        Me.grpBoxMainFooter.Name = "grpBoxMainFooter"
        Me.grpBoxMainFooter.Size = New System.Drawing.Size(697, 73)
        Me.grpBoxMainFooter.TabIndex = 11
        Me.grpBoxMainFooter.TabStop = False
        Me.grpBoxMainFooter.Text = "grpBoxMainFooter"
        '
        'labKeyInfo
        '
        Me.labKeyInfo.BackColor = System.Drawing.Color.LavenderBlush
        Me.labKeyInfo.Location = New System.Drawing.Point(106, 13)
        Me.labKeyInfo.Name = "labKeyInfo"
        Me.labKeyInfo.Padding = New System.Windows.Forms.Padding(3, 2, 0, 0)
        Me.labKeyInfo.Size = New System.Drawing.Size(94, 49)
        Me.labKeyInfo.TabIndex = 10
        Me.labKeyInfo.Text = "* PrimaryKey or AutoGen Field-"
        Me.labKeyInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OpenDlg1
        '
        Me.OpenDlg1.FileName = "OpenFileDialog1"
        '
        'frmEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(721, 534)
        Me.Controls.Add(Me.grpBoxMainFooter)
        Me.Controls.Add(Me.panelEdit)
        Me.Controls.Add(Me.labTitle)
        Me.Controls.Add(Me.labStatus)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmEdit"
        Me.Text = "frmEdit"
        Me.panelEdit.ResumeLayout(False)
        Me.panelEdit.PerformLayout()
        CType(Me.picImage1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxMainFooter.ResumeLayout(False)
        Me.grpBoxMainFooter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents labVersion As System.Windows.Forms.Label
    Friend WithEvents labStatus As System.Windows.Forms.Label
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents labTitle As System.Windows.Forms.Label
    Public WithEvents cmdSaveExit As System.Windows.Forms.Button
    Friend WithEvents txtModelData As System.Windows.Forms.TextBox
    Friend WithEvents labModelCap As System.Windows.Forms.Label
    Friend WithEvents labModelJoin As System.Windows.Forms.Label
    Friend WithEvents cmdModelData As System.Windows.Forms.Button
    Friend WithEvents picImage1 As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents panelEdit As System.Windows.Forms.Panel
    Friend WithEvents grpBoxMainFooter As System.Windows.Forms.GroupBox
    Friend WithEvents OpenDlg1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents DTPickerModel As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkModel As System.Windows.Forms.CheckBox
    Friend WithEvents labKeyInfo As System.Windows.Forms.Label
    Friend WithEvents txtUnfinished As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
