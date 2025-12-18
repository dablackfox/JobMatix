<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGetPrinter
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
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboLabelPrinters = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboReceiptPrinters = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cboColourPrinters = New System.Windows.Forms.ComboBox()
        Me.NumUpDownLabels = New System.Windows.Forms.NumericUpDown()
        Me.panelPrtColour = New System.Windows.Forms.Panel()
        Me.panelPrtReceipt = New System.Windows.Forms.Panel()
        Me.panelPrtLabel = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        CType(Me.NumUpDownLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelPrtColour.SuspendLayout()
        Me.panelPrtReceipt.SuspendLayout()
        Me.panelPrtLabel.SuspendLayout()
        Me.SuspendLayout()
        '
        'labHdr1
        '
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr1.Location = New System.Drawing.Point(24, 12)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Size = New System.Drawing.Size(243, 28)
        Me.labHdr1.TabIndex = 0
        Me.labHdr1.Text = "Select Printer"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Linen
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(21, 50)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(135, 13)
        Me.Label16.TabIndex = 106
        Me.Label16.Text = "-- Label Printer --"
        '
        'cboLabelPrinters
        '
        Me.cboLabelPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboLabelPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLabelPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboLabelPrinters.Font = New System.Drawing.Font("Lucida Sans", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLabelPrinters.FormattingEnabled = True
        Me.cboLabelPrinters.Location = New System.Drawing.Point(10, 66)
        Me.cboLabelPrinters.Name = "cboLabelPrinters"
        Me.cboLabelPrinters.Size = New System.Drawing.Size(173, 20)
        Me.cboLabelPrinters.TabIndex = 105
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Linen
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(25, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(187, 13)
        Me.Label1.TabIndex = 104
        Me.Label1.Text = "-- Docket (Receipt) Printer --"
        '
        'cboReceiptPrinters
        '
        Me.cboReceiptPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboReceiptPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReceiptPrinters.Font = New System.Drawing.Font("Lucida Sans", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReceiptPrinters.FormattingEnabled = True
        Me.cboReceiptPrinters.Location = New System.Drawing.Point(28, 54)
        Me.cboReceiptPrinters.Name = "cboReceiptPrinters"
        Me.cboReceiptPrinters.Size = New System.Drawing.Size(173, 20)
        Me.cboReceiptPrinters.TabIndex = 103
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Linen
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Blue
        Me.Label21.Location = New System.Drawing.Point(25, 41)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(158, 13)
        Me.Label21.TabIndex = 102
        Me.Label21.Text = "-- Colour Printer --"
        '
        'cboColourPrinters
        '
        Me.cboColourPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboColourPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColourPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboColourPrinters.Font = New System.Drawing.Font("Lucida Sans", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboColourPrinters.FormattingEnabled = True
        Me.cboColourPrinters.Location = New System.Drawing.Point(28, 57)
        Me.cboColourPrinters.Name = "cboColourPrinters"
        Me.cboColourPrinters.Size = New System.Drawing.Size(173, 20)
        Me.cboColourPrinters.TabIndex = 101
        '
        'NumUpDownLabels
        '
        Me.NumUpDownLabels.BackColor = System.Drawing.Color.LightSteelBlue
        Me.NumUpDownLabels.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumUpDownLabels.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumUpDownLabels.Location = New System.Drawing.Point(135, 12)
        Me.NumUpDownLabels.Maximum = New Decimal(New Integer() {21, 0, 0, 0})
        Me.NumUpDownLabels.Name = "NumUpDownLabels"
        Me.NumUpDownLabels.Size = New System.Drawing.Size(48, 17)
        Me.NumUpDownLabels.TabIndex = 51
        Me.NumUpDownLabels.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.NumUpDownLabels.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        '
        'panelPrtColour
        '
        Me.panelPrtColour.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelPrtColour.Controls.Add(Me.cboColourPrinters)
        Me.panelPrtColour.Controls.Add(Me.Label21)
        Me.panelPrtColour.Location = New System.Drawing.Point(28, 43)
        Me.panelPrtColour.Name = "panelPrtColour"
        Me.panelPrtColour.Size = New System.Drawing.Size(236, 101)
        Me.panelPrtColour.TabIndex = 0
        '
        'panelPrtReceipt
        '
        Me.panelPrtReceipt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelPrtReceipt.Controls.Add(Me.cboReceiptPrinters)
        Me.panelPrtReceipt.Controls.Add(Me.Label1)
        Me.panelPrtReceipt.Location = New System.Drawing.Point(28, 148)
        Me.panelPrtReceipt.Name = "panelPrtReceipt"
        Me.panelPrtReceipt.Size = New System.Drawing.Size(236, 25)
        Me.panelPrtReceipt.TabIndex = 1
        '
        'panelPrtLabel
        '
        Me.panelPrtLabel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelPrtLabel.Controls.Add(Me.Label2)
        Me.panelPrtLabel.Controls.Add(Me.Label16)
        Me.panelPrtLabel.Controls.Add(Me.cboLabelPrinters)
        Me.panelPrtLabel.Controls.Add(Me.NumUpDownLabels)
        Me.panelPrtLabel.Location = New System.Drawing.Point(28, 178)
        Me.panelPrtLabel.Name = "panelPrtLabel"
        Me.panelPrtLabel.Size = New System.Drawing.Size(236, 27)
        Me.panelPrtLabel.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(55, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 107
        Me.Label2.Text = "No.of Labels"
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnOK.Location = New System.Drawing.Point(123, 212)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(57, 22)
        Me.btnOK.TabIndex = 14
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(207, 212)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(57, 22)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'frmGetPrinter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(304, 274)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.panelPrtLabel)
        Me.Controls.Add(Me.panelPrtReceipt)
        Me.Controls.Add(Me.panelPrtColour)
        Me.Controls.Add(Me.labHdr1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmGetPrinter"
        Me.Text = "frmGetPrinter"
        CType(Me.NumUpDownLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelPrtColour.ResumeLayout(False)
        Me.panelPrtReceipt.ResumeLayout(False)
        Me.panelPrtLabel.ResumeLayout(False)
        Me.panelPrtLabel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents labHdr1 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboLabelPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboReceiptPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboColourPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents NumUpDownLabels As System.Windows.Forms.NumericUpDown
    Friend WithEvents panelPrtColour As System.Windows.Forms.Panel
    Friend WithEvents panelPrtReceipt As System.Windows.Forms.Panel
    Friend WithEvents panelPrtLabel As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
