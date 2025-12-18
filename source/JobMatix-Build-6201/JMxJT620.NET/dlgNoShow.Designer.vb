<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgNoShow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.labMainTitle = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.labMessage = New System.Windows.Forms.Label()
        Me.chkDoNotShow = New System.Windows.Forms.CheckBox()
        Me.labSubTitle = New System.Windows.Forms.Label()
        Me.RichTextInfo = New System.Windows.Forms.RichTextBox()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.labViewWhatsNew = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.OK_Button.Location = New System.Drawing.Point(388, 291)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(53, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Close"
        '
        'labMainTitle
        '
        Me.labMainTitle.AutoSize = True
        Me.labMainTitle.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMainTitle.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.labMainTitle.Location = New System.Drawing.Point(96, 17)
        Me.labMainTitle.Name = "labMainTitle"
        Me.labMainTitle.Size = New System.Drawing.Size(133, 17)
        Me.labMainTitle.TabIndex = 1
        Me.labMainTitle.Text = "JobMatix Message"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.JobMatix3.My.Resources.Resources.PawPrint1
        Me.PictureBox1.Location = New System.Drawing.Point(16, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(39, 36)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'labMessage
        '
        Me.labMessage.BackColor = System.Drawing.Color.Transparent
        Me.labMessage.Location = New System.Drawing.Point(12, 76)
        Me.labMessage.Name = "labMessage"
        Me.labMessage.Size = New System.Drawing.Size(410, 32)
        Me.labMessage.TabIndex = 3
        Me.labMessage.Text = "labMessage"
        '
        'chkDoNotShow
        '
        Me.chkDoNotShow.Location = New System.Drawing.Point(19, 291)
        Me.chkDoNotShow.Name = "chkDoNotShow"
        Me.chkDoNotShow.Size = New System.Drawing.Size(118, 31)
        Me.chkDoNotShow.TabIndex = 4
        Me.chkDoNotShow.Text = "Do not show this message again."
        Me.chkDoNotShow.UseVisualStyleBackColor = True
        '
        'labSubTitle
        '
        Me.labSubTitle.BackColor = System.Drawing.Color.Transparent
        Me.labSubTitle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSubTitle.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.labSubTitle.Location = New System.Drawing.Point(96, 47)
        Me.labSubTitle.Name = "labSubTitle"
        Me.labSubTitle.Size = New System.Drawing.Size(283, 13)
        Me.labSubTitle.TabIndex = 5
        Me.labSubTitle.Text = "labSubTitle"
        '
        'RichTextInfo
        '
        Me.RichTextInfo.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.RichTextInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextInfo.Location = New System.Drawing.Point(15, 111)
        Me.RichTextInfo.Name = "RichTextInfo"
        Me.RichTextInfo.ReadOnly = True
        Me.RichTextInfo.Size = New System.Drawing.Size(424, 29)
        Me.RichTextInfo.TabIndex = 6
        Me.RichTextInfo.Text = ""
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(14, 157)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(427, 116)
        Me.WebBrowser1.TabIndex = 7
        '
        'labViewWhatsNew
        '
        Me.labViewWhatsNew.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labViewWhatsNew.Location = New System.Drawing.Point(188, 292)
        Me.labViewWhatsNew.Name = "labViewWhatsNew"
        Me.labViewWhatsNew.Size = New System.Drawing.Size(166, 33)
        Me.labViewWhatsNew.TabIndex = 8
        Me.labViewWhatsNew.Text = "You can view this message any time from the main menu ""About"" item."
        '
        'dlgNoShow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.CancelButton = Me.OK_Button
        Me.ClientSize = New System.Drawing.Size(458, 334)
        Me.Controls.Add(Me.labViewWhatsNew)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.RichTextInfo)
        Me.Controls.Add(Me.labSubTitle)
        Me.Controls.Add(Me.chkDoNotShow)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.labMessage)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.labMainTitle)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgNoShow"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "JobMatix3"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents labMainTitle As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents labMessage As System.Windows.Forms.Label
    Friend WithEvents chkDoNotShow As System.Windows.Forms.CheckBox
    Friend WithEvents labSubTitle As System.Windows.Forms.Label
    Friend WithEvents RichTextInfo As System.Windows.Forms.RichTextBox
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents labViewWhatsNew As System.Windows.Forms.Label

End Class
