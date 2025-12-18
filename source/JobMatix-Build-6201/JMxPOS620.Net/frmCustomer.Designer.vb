<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomer
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
        Me.panelBanner = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.labToday = New System.Windows.Forms.Label()
        Me.labStaffName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpBoxMain = New System.Windows.Forms.GroupBox()
        Me.labDLLversion = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelBanner.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelBanner
        '
        Me.panelBanner.BackColor = System.Drawing.Color.Lavender
        Me.panelBanner.Controls.Add(Me.Label5)
        Me.panelBanner.Controls.Add(Me.labToday)
        Me.panelBanner.Controls.Add(Me.labStaffName)
        Me.panelBanner.Controls.Add(Me.Label1)
        Me.panelBanner.Location = New System.Drawing.Point(0, 0)
        Me.panelBanner.Name = "panelBanner"
        Me.panelBanner.Size = New System.Drawing.Size(1013, 29)
        Me.panelBanner.TabIndex = 22
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(13, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(165, 18)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Customer Admin (modal)"
        '
        'labToday
        '
        Me.labToday.AutoSize = True
        Me.labToday.Location = New System.Drawing.Point(218, 9)
        Me.labToday.Name = "labToday"
        Me.labToday.Size = New System.Drawing.Size(51, 13)
        Me.labToday.TabIndex = 6
        Me.labToday.Text = "labToday"
        '
        'labStaffName
        '
        Me.labStaffName.AutoSize = True
        Me.labStaffName.Location = New System.Drawing.Point(427, 10)
        Me.labStaffName.Name = "labStaffName"
        Me.labStaffName.Size = New System.Drawing.Size(72, 13)
        Me.labStaffName.TabIndex = 5
        Me.labStaffName.Text = "labStaffName"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(383, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Staff: "
        '
        'grpBoxMain
        '
        Me.grpBoxMain.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxMain.Location = New System.Drawing.Point(0, 34)
        Me.grpBoxMain.Name = "grpBoxMain"
        Me.grpBoxMain.Size = New System.Drawing.Size(1000, 646)
        Me.grpBoxMain.TabIndex = 23
        Me.grpBoxMain.TabStop = False
        Me.grpBoxMain.Text = "grpBoxMain"
        '
        'labDLLversion
        '
        Me.labDLLversion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDLLversion.Location = New System.Drawing.Point(5, 685)
        Me.labDLLversion.Name = "labDLLversion"
        Me.labDLLversion.Size = New System.Drawing.Size(306, 13)
        Me.labDLLversion.TabIndex = 44
        Me.labDLLversion.Text = "labDLLversion"
        Me.labDLLversion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(1008, 698)
        Me.Controls.Add(Me.labDLLversion)
        Me.Controls.Add(Me.grpBoxMain)
        Me.Controls.Add(Me.panelBanner)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmCustomer"
        Me.Text = "frmCustomer"
        Me.panelBanner.ResumeLayout(False)
        Me.panelBanner.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelBanner As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents labToday As System.Windows.Forms.Label
    Friend WithEvents labStaffName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpBoxMain As System.Windows.Forms.GroupBox
    Friend WithEvents labDLLversion As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
