<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowCustomerTags
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
        Me.panelTagSelect = New System.Windows.Forms.Panel()
        Me.btnSaveCustList = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.checkedListRefTags = New System.Windows.Forms.CheckedListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.txtCustomer_id = New System.Windows.Forms.TextBox()
        Me.BarcodeLabel = New System.Windows.Forms.Label()
        Me.txtCustBarcode = New System.Windows.Forms.TextBox()
        Me.panelTagList = New System.Windows.Forms.Panel()
        Me.panelTagSelect.SuspendLayout()
        Me.panelTagList.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelTagSelect
        '
        Me.panelTagSelect.Controls.Add(Me.panelTagList)
        Me.panelTagSelect.Controls.Add(Me.btnSaveCustList)
        Me.panelTagSelect.Controls.Add(Me.btnCancel)
        Me.panelTagSelect.Controls.Add(Me.Label5)
        Me.panelTagSelect.Location = New System.Drawing.Point(39, 155)
        Me.panelTagSelect.Name = "panelTagSelect"
        Me.panelTagSelect.Size = New System.Drawing.Size(470, 450)
        Me.panelTagSelect.TabIndex = 14
        '
        'btnSaveCustList
        '
        Me.btnSaveCustList.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveCustList.Location = New System.Drawing.Point(361, 383)
        Me.btnSaveCustList.Name = "btnSaveCustList"
        Me.btnSaveCustList.Size = New System.Drawing.Size(57, 32)
        Me.btnSaveCustList.TabIndex = 17
        Me.btnSaveCustList.Text = "Save"
        Me.btnSaveCustList.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.LavenderBlush
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(361, 303)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(57, 28)
        Me.btnCancel.TabIndex = 16
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'checkedListRefTags
        '
        Me.checkedListRefTags.BackColor = System.Drawing.Color.WhiteSmoke
        Me.checkedListRefTags.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.checkedListRefTags.CheckOnClick = True
        Me.checkedListRefTags.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.checkedListRefTags.FormattingEnabled = True
        Me.checkedListRefTags.Location = New System.Drawing.Point(12, 8)
        Me.checkedListRefTags.Name = "checkedListRefTags"
        Me.checkedListRefTags.Size = New System.Drawing.Size(281, 280)
        Me.checkedListRefTags.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(23, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.Label5.Size = New System.Drawing.Size(312, 83)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Below is the full reference list of available customer-related TAGS..." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Tick a" & _
    "ll the items that will apply to the selected Customer." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Press ""Save"" when don" & _
    "e.."
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(202, 19)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Customer Tags Update.."
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label4.Location = New System.Drawing.Point(309, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(200, 125)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "This form is shown from Customer Admin to view/edit Current List of Tags for a  C" & _
    "ustomer:." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Go To the CustomerAdmin to change the Reference List of availabl" & _
    "e TAGS.."
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(43, 60)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(116, 19)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Customer:"
        '
        'txtCustName
        '
        Me.txtCustName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustName.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.Location = New System.Drawing.Point(39, 108)
        Me.txtCustName.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtCustName.MaxLength = 50
        Me.txtCustName.Multiline = True
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.ReadOnly = True
        Me.txtCustName.Size = New System.Drawing.Size(254, 40)
        Me.txtCustName.TabIndex = 24
        Me.txtCustName.TabStop = False
        Me.txtCustName.Text = "txtCustName"
        '
        'txtCustomer_id
        '
        Me.txtCustomer_id.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer_id.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustomer_id.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer_id.Location = New System.Drawing.Point(237, 84)
        Me.txtCustomer_id.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtCustomer_id.Name = "txtCustomer_id"
        Me.txtCustomer_id.ReadOnly = True
        Me.txtCustomer_id.Size = New System.Drawing.Size(56, 17)
        Me.txtCustomer_id.TabIndex = 25
        Me.txtCustomer_id.TabStop = False
        '
        'BarcodeLabel
        '
        Me.BarcodeLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarcodeLabel.ForeColor = System.Drawing.Color.Gray
        Me.BarcodeLabel.Location = New System.Drawing.Point(43, 85)
        Me.BarcodeLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.BarcodeLabel.Name = "BarcodeLabel"
        Me.BarcodeLabel.Size = New System.Drawing.Size(56, 20)
        Me.BarcodeLabel.TabIndex = 26
        Me.BarcodeLabel.Text = "Barcode"
        '
        'txtCustBarcode
        '
        Me.txtCustBarcode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustBarcode.Location = New System.Drawing.Point(104, 83)
        Me.txtCustBarcode.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtCustBarcode.MaxLength = 15
        Me.txtCustBarcode.Name = "txtCustBarcode"
        Me.txtCustBarcode.ReadOnly = True
        Me.txtCustBarcode.Size = New System.Drawing.Size(53, 19)
        Me.txtCustBarcode.TabIndex = 23
        Me.txtCustBarcode.TabStop = False
        Me.txtCustBarcode.Text = "txtCustBarcode"
        '
        'panelTagList
        '
        Me.panelTagList.BackColor = System.Drawing.Color.GhostWhite
        Me.panelTagList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panelTagList.Controls.Add(Me.checkedListRefTags)
        Me.panelTagList.Location = New System.Drawing.Point(24, 115)
        Me.panelTagList.Name = "panelTagList"
        Me.panelTagList.Size = New System.Drawing.Size(311, 300)
        Me.panelTagList.TabIndex = 18
        '
        'frmShowCustomerTags
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(538, 622)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtCustName)
        Me.Controls.Add(Me.txtCustomer_id)
        Me.Controls.Add(Me.BarcodeLabel)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCustBarcode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.panelTagSelect)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmShowCustomerTags"
        Me.Text = "frmShowCustomerTags"
        Me.panelTagSelect.ResumeLayout(False)
        Me.panelTagList.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents panelTagSelect As System.Windows.Forms.Panel
    Friend WithEvents checkedListRefTags As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSaveCustList As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer_id As System.Windows.Forms.TextBox
    Friend WithEvents BarcodeLabel As System.Windows.Forms.Label
    Friend WithEvents txtCustBarcode As System.Windows.Forms.TextBox
    Friend WithEvents panelTagList As System.Windows.Forms.Panel
End Class
