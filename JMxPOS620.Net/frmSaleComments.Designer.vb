<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaleComments
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
        Me.panelComments = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtSaleDelivery = New System.Windows.Forms.TextBox()
        Me.LabSaleDelivery = New System.Windows.Forms.Label()
        Me.txtSaleComments = New System.Windows.Forms.TextBox()
        Me.labSaleComments = New System.Windows.Forms.Label()
        Me.labHdr1 = New System.Windows.Forms.Label()
        Me.panelComments.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelComments
        '
        Me.panelComments.Controls.Add(Me.btnSave)
        Me.panelComments.Controls.Add(Me.btnCancel)
        Me.panelComments.Controls.Add(Me.txtSaleDelivery)
        Me.panelComments.Controls.Add(Me.LabSaleDelivery)
        Me.panelComments.Controls.Add(Me.txtSaleComments)
        Me.panelComments.Controls.Add(Me.labSaleComments)
        Me.panelComments.Location = New System.Drawing.Point(23, 66)
        Me.panelComments.Name = "panelComments"
        Me.panelComments.Size = New System.Drawing.Size(595, 328)
        Me.panelComments.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(497, 240)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(70, 35)
        Me.btnSave.TabIndex = 39
        Me.btnSave.Text = "Save and Exit"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(497, 188)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(70, 36)
        Me.btnCancel.TabIndex = 38
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtSaleDelivery
        '
        Me.txtSaleDelivery.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSaleDelivery.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleDelivery.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleDelivery.Location = New System.Drawing.Point(31, 190)
        Me.txtSaleDelivery.Multiline = True
        Me.txtSaleDelivery.Name = "txtSaleDelivery"
        Me.txtSaleDelivery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSaleDelivery.Size = New System.Drawing.Size(427, 116)
        Me.txtSaleDelivery.TabIndex = 36
        Me.txtSaleDelivery.TabStop = False
        Me.txtSaleDelivery.Text = "txtSaleDelivery"
        '
        'LabSaleDelivery
        '
        Me.LabSaleDelivery.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSaleDelivery.Location = New System.Drawing.Point(28, 174)
        Me.LabSaleDelivery.Name = "LabSaleDelivery"
        Me.LabSaleDelivery.Size = New System.Drawing.Size(186, 13)
        Me.LabSaleDelivery.TabIndex = 37
        Me.LabSaleDelivery.Text = "DeliveryInstructions"
        '
        'txtSaleComments
        '
        Me.txtSaleComments.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSaleComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleComments.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleComments.Location = New System.Drawing.Point(31, 40)
        Me.txtSaleComments.Multiline = True
        Me.txtSaleComments.Name = "txtSaleComments"
        Me.txtSaleComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSaleComments.Size = New System.Drawing.Size(427, 116)
        Me.txtSaleComments.TabIndex = 34
        Me.txtSaleComments.TabStop = False
        Me.txtSaleComments.Text = "txtSaleComments"
        '
        'labSaleComments
        '
        Me.labSaleComments.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSaleComments.Location = New System.Drawing.Point(28, 19)
        Me.labSaleComments.Name = "labSaleComments"
        Me.labSaleComments.Size = New System.Drawing.Size(186, 13)
        Me.labSaleComments.TabIndex = 35
        Me.labSaleComments.Text = "Comments"
        '
        'labHdr1
        '
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr1.Location = New System.Drawing.Point(28, 17)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Size = New System.Drawing.Size(285, 35)
        Me.labHdr1.TabIndex = 3
        Me.labHdr1.Text = "Enter/Update Sale Comments and/or Delivery Instructions.."
        '
        'frmSaleComments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(641, 420)
        Me.Controls.Add(Me.labHdr1)
        Me.Controls.Add(Me.panelComments)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmSaleComments"
        Me.Text = "frmSaleComments"
        Me.panelComments.ResumeLayout(False)
        Me.panelComments.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelComments As System.Windows.Forms.Panel
    Friend WithEvents txtSaleDelivery As System.Windows.Forms.TextBox
    Friend WithEvents LabSaleDelivery As System.Windows.Forms.Label
    Friend WithEvents txtSaleComments As System.Windows.Forms.TextBox
    Friend WithEvents labSaleComments As System.Windows.Forms.Label
    Friend WithEvents labHdr1 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
