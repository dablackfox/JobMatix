<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewJobBase
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
        Me.grpBoxMain = New System.Windows.Forms.GroupBox()
        Me.panelBanner = New System.Windows.Forms.Panel()
        Me.picUserLogo = New System.Windows.Forms.PictureBox()
        Me.LabHdr1 = New System.Windows.Forms.Label()
        Me.panelBanner.SuspendLayout()
        CType(Me.picUserLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpBoxMain
        '
        Me.grpBoxMain.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.grpBoxMain.Location = New System.Drawing.Point(3, 32)
        Me.grpBoxMain.Name = "grpBoxMain"
        Me.grpBoxMain.Padding = New System.Windows.Forms.Padding(0)
        Me.grpBoxMain.Size = New System.Drawing.Size(982, 642)
        Me.grpBoxMain.TabIndex = 0
        Me.grpBoxMain.TabStop = False
        Me.grpBoxMain.Text = "grpBoxMain"
        '
        'panelBanner
        '
        Me.panelBanner.Controls.Add(Me.picUserLogo)
        Me.panelBanner.Controls.Add(Me.LabHdr1)
        Me.panelBanner.Location = New System.Drawing.Point(0, 2)
        Me.panelBanner.Name = "panelBanner"
        Me.panelBanner.Size = New System.Drawing.Size(1008, 28)
        Me.panelBanner.TabIndex = 1
        '
        'picUserLogo
        '
        Me.picUserLogo.BackColor = System.Drawing.SystemColors.Control
        Me.picUserLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picUserLogo.Cursor = System.Windows.Forms.Cursors.Default
        Me.picUserLogo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picUserLogo.Location = New System.Drawing.Point(840, 5)
        Me.picUserLogo.Name = "picUserLogo"
        Me.picUserLogo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picUserLogo.Size = New System.Drawing.Size(16, 20)
        Me.picUserLogo.TabIndex = 8
        Me.picUserLogo.TabStop = False
        Me.picUserLogo.Visible = False
        '
        'LabHdr1
        '
        Me.LabHdr1.AutoSize = True
        Me.LabHdr1.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr1.Location = New System.Drawing.Point(10, 7)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr1.Size = New System.Drawing.Size(230, 14)
        Me.LabHdr1.TabIndex = 1
        Me.LabHdr1.Text = "New/Amend Service Agreement (Modal)"
        '
        'frmNewJobBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(991, 681)
        Me.Controls.Add(Me.panelBanner)
        Me.Controls.Add(Me.grpBoxMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmNewJobBase"
        Me.Text = "frmNewJobBase"
        Me.panelBanner.ResumeLayout(False)
        Me.panelBanner.PerformLayout()
        CType(Me.picUserLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpBoxMain As System.Windows.Forms.GroupBox
    Friend WithEvents panelBanner As System.Windows.Forms.Panel
    Public WithEvents LabHdr1 As System.Windows.Forms.Label
    Public WithEvents picUserLogo As System.Windows.Forms.PictureBox
End Class
