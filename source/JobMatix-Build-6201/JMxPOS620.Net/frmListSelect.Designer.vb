<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmListSelect
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
        Me.labHdr1 = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.labHdr2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'labHdr1
        '
        Me.labHdr1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr1.Location = New System.Drawing.Point(24, 13)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Size = New System.Drawing.Size(523, 63)
        Me.labHdr1.TabIndex = 0
        Me.labHdr1.Text = "labHdr1"
        '
        'ListView1
        '
        Me.ListView1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(27, 111)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(640, 289)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(528, 420)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(57, 22)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.Location = New System.Drawing.Point(605, 417)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(56, 24)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'labHdr2
        '
        Me.labHdr2.Location = New System.Drawing.Point(27, 89)
        Me.labHdr2.Name = "labHdr2"
        Me.labHdr2.Size = New System.Drawing.Size(422, 19)
        Me.labHdr2.TabIndex = 4
        Me.labHdr2.Text = "Select item and press OK..."
        '
        'frmListSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(690, 459)
        Me.Controls.Add(Me.labHdr2)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.labHdr1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmListSelect"
        Me.Text = "frmListSelect"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents labHdr1 As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents labHdr2 As System.Windows.Forms.Label
End Class
