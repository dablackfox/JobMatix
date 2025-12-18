<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStock
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.openDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.labDLLversion = New System.Windows.Forms.Label()
        Me.timerGrid = New System.Windows.Forms.Timer(Me.components)
        Me.grpBoxMain = New System.Windows.Forms.GroupBox()
        Me.SuspendLayout()
        '
        'openDlg1
        '
        Me.openDlg1.FileName = "OpenFileDialog1"
        '
        'labDLLversion
        '
        Me.labDLLversion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDLLversion.Location = New System.Drawing.Point(12, 699)
        Me.labDLLversion.Name = "labDLLversion"
        Me.labDLLversion.Size = New System.Drawing.Size(306, 13)
        Me.labDLLversion.TabIndex = 43
        Me.labDLLversion.Text = "labDLLversion"
        '
        'grpBoxMain
        '
        Me.grpBoxMain.BackColor = System.Drawing.Color.LightSteelBlue
        Me.grpBoxMain.Location = New System.Drawing.Point(1, 1)
        Me.grpBoxMain.Name = "grpBoxMain"
        Me.grpBoxMain.Size = New System.Drawing.Size(988, 689)
        Me.grpBoxMain.TabIndex = 44
        Me.grpBoxMain.TabStop = False
        Me.grpBoxMain.Text = "grpBoxMain"
        '
        'frmStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 721)
        Me.Controls.Add(Me.labDLLversion)
        Me.Controls.Add(Me.grpBoxMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmStock"
        Me.Text = "frmStock"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents openDlg1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents labDLLversion As System.Windows.Forms.Label
    Friend WithEvents timerGrid As System.Windows.Forms.Timer
    Friend WithEvents grpBoxMain As System.Windows.Forms.GroupBox
End Class
