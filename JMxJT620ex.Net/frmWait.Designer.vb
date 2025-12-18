<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWait
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWait))
        Me.labMsg = New System.Windows.Forms.Label()
        Me.picWorking = New System.Windows.Forms.PictureBox()
        Me.labHdr = New System.Windows.Forms.Label()
        Me.OpenDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.ClsGsPanel1 = New JMxJT350ex.clsGsPanel()
        CType(Me.picWorking, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ClsGsPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'labMsg
        '
        Me.labMsg.BackColor = System.Drawing.Color.Transparent
        Me.labMsg.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMsg.Location = New System.Drawing.Point(12, 76)
        Me.labMsg.Name = "labMsg"
        Me.labMsg.Padding = New System.Windows.Forms.Padding(4, 9, 0, 0)
        Me.labMsg.Size = New System.Drawing.Size(257, 99)
        Me.labMsg.TabIndex = 1
        Me.labMsg.Text = "labMsg"
        '
        'picWorking
        '
        Me.picWorking.Image = CType(resources.GetObject("picWorking.Image"), System.Drawing.Image)
        Me.picWorking.Location = New System.Drawing.Point(302, 144)
        Me.picWorking.Name = "picWorking"
        Me.picWorking.Size = New System.Drawing.Size(34, 31)
        Me.picWorking.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picWorking.TabIndex = 132
        Me.picWorking.TabStop = False
        '
        'labHdr
        '
        Me.labHdr.BackColor = System.Drawing.Color.Transparent
        Me.labHdr.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr.Location = New System.Drawing.Point(0, 0)
        Me.labHdr.Name = "labHdr"
        Me.labHdr.Padding = New System.Windows.Forms.Padding(12, 9, 0, 0)
        Me.labHdr.Size = New System.Drawing.Size(175, 57)
        Me.labHdr.TabIndex = 133
        Me.labHdr.Text = "labHdr"
        Me.labHdr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OpenDlg1
        '
        Me.OpenDlg1.FileName = "OpenFileDialog1"
        '
        'ClsGsPanel1
        '
        Me.ClsGsPanel1.BackColor = System.Drawing.Color.LightSkyBlue
        Me.ClsGsPanel1.BorderColor = System.Drawing.Color.White
        Me.ClsGsPanel1.Controls.Add(Me.labHdr)
        Me.ClsGsPanel1.Edge = 1
        Me.ClsGsPanel1.Location = New System.Drawing.Point(0, 0)
        Me.ClsGsPanel1.Name = "ClsGsPanel1"
        Me.ClsGsPanel1.Size = New System.Drawing.Size(350, 60)
        Me.ClsGsPanel1.TabIndex = 134
        '
        'frmWait
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(348, 212)
        Me.ControlBox = False
        Me.Controls.Add(Me.ClsGsPanel1)
        Me.Controls.Add(Me.picWorking)
        Me.Controls.Add(Me.labMsg)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmWait"
        Me.Text = "frmWait"
        CType(Me.picWorking, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ClsGsPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents labMsg As System.Windows.Forms.Label
    Friend WithEvents picWorking As System.Windows.Forms.PictureBox
    Friend WithEvents labHdr As System.Windows.Forms.Label
    Friend WithEvents OpenDlg1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ClsGsPanel1 As JMxJT350ex.clsGsPanel
End Class
