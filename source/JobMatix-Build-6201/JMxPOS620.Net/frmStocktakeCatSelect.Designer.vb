<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStocktakeCatSelect
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
        Me.panelCatSelection = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.btnCatSelectOk = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboSelectCat1 = New System.Windows.Forms.ComboBox()
        Me.chkCat2All = New System.Windows.Forms.CheckBox()
        Me.listSelectCat2 = New System.Windows.Forms.CheckedListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.panelCatSelection.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelCatSelection
        '
        Me.panelCatSelection.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.panelCatSelection.Controls.Add(Me.btnCancel)
        Me.panelCatSelection.Controls.Add(Me.Label18)
        Me.panelCatSelection.Controls.Add(Me.btnCatSelectOk)
        Me.panelCatSelection.Controls.Add(Me.Label12)
        Me.panelCatSelection.Controls.Add(Me.Label3)
        Me.panelCatSelection.Controls.Add(Me.cboSelectCat1)
        Me.panelCatSelection.Controls.Add(Me.chkCat2All)
        Me.panelCatSelection.Controls.Add(Me.listSelectCat2)
        Me.panelCatSelection.Controls.Add(Me.Label4)
        Me.panelCatSelection.Location = New System.Drawing.Point(12, 12)
        Me.panelCatSelection.Name = "panelCatSelection"
        Me.panelCatSelection.Size = New System.Drawing.Size(462, 271)
        Me.panelCatSelection.TabIndex = 8
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.Location = New System.Drawing.Point(378, 227)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 23)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Green
        Me.Label18.Location = New System.Drawing.Point(15, 45)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(346, 13)
        Me.Label18.TabIndex = 8
        Me.Label18.Text = "Select Categories to include in this Stocktake :"
        '
        'btnCatSelectOk
        '
        Me.btnCatSelectOk.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCatSelectOk.Location = New System.Drawing.Point(378, 180)
        Me.btnCatSelectOk.Name = "btnCatSelectOk"
        Me.btnCatSelectOk.Size = New System.Drawing.Size(63, 23)
        Me.btnCatSelectOk.TabIndex = 7
        Me.btnCatSelectOk.Text = "OK"
        Me.btnCatSelectOk.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(134, 17)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "Partial Stocktake- "
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 15)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Category1: "
        '
        'cboSelectCat1
        '
        Me.cboSelectCat1.BackColor = System.Drawing.Color.Lavender
        Me.cboSelectCat1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboSelectCat1.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSelectCat1.FormattingEnabled = True
        Me.cboSelectCat1.Location = New System.Drawing.Point(11, 90)
        Me.cboSelectCat1.Name = "cboSelectCat1"
        Me.cboSelectCat1.Size = New System.Drawing.Size(95, 22)
        Me.cboSelectCat1.TabIndex = 1
        '
        'chkCat2All
        '
        Me.chkCat2All.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCat2All.Location = New System.Drawing.Point(151, 126)
        Me.chkCat2All.Name = "chkCat2All"
        Me.chkCat2All.Size = New System.Drawing.Size(73, 19)
        Me.chkCat2All.TabIndex = 2
        Me.chkCat2All.Text = "Select All"
        Me.chkCat2All.UseVisualStyleBackColor = True
        '
        'listSelectCat2
        '
        Me.listSelectCat2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.listSelectCat2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listSelectCat2.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listSelectCat2.FormattingEnabled = True
        Me.listSelectCat2.Location = New System.Drawing.Point(251, 75)
        Me.listSelectCat2.Name = "listSelectCat2"
        Me.listSelectCat2.Size = New System.Drawing.Size(110, 176)
        Me.listSelectCat2.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(153, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 35)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Category2:  Check List-"
        '
        'frmStocktakeCatSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(486, 295)
        Me.ControlBox = False
        Me.Controls.Add(Me.panelCatSelection)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmStocktakeCatSelect"
        Me.Text = "frmStocktakeCatSelect"
        Me.panelCatSelection.ResumeLayout(False)
        Me.panelCatSelection.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelCatSelection As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnCatSelectOk As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboSelectCat1 As System.Windows.Forms.ComboBox
    Friend WithEvents chkCat2All As System.Windows.Forms.CheckBox
    Friend WithEvents listSelectCat2 As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
