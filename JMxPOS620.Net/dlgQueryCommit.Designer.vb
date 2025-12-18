<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dlgQueryCommit
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.labQuestion = New System.Windows.Forms.Label()
        Me.labMessage = New System.Windows.Forms.Label()
        Me.chkEmail = New System.Windows.Forms.CheckBox()
        Me.chkPrint = New System.Windows.Forms.CheckBox()
        Me.grpBoxDocAction = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panelDocOpts = New System.Windows.Forms.Panel()
        Me.optConfirmDocket = New System.Windows.Forms.RadioButton()
        Me.optConfirmA4 = New System.Windows.Forms.RadioButton()
        Me.labDocType = New System.Windows.Forms.Label()
        Me.labEmail = New System.Windows.Forms.Label()
        Me.panelPrinters = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cboReceiptPrinters = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboInvoicePrinters = New System.Windows.Forms.ComboBox()
        Me.labPdfPrinter = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.grpBoxDocAction.SuspendLayout()
        Me.panelDocOpts.SuspendLayout()
        Me.panelPrinters.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(485, 307)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'labQuestion
        '
        Me.labQuestion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labQuestion.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labQuestion.ForeColor = System.Drawing.Color.Firebrick
        Me.labQuestion.Location = New System.Drawing.Point(29, 102)
        Me.labQuestion.Name = "labQuestion"
        Me.labQuestion.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labQuestion.Size = New System.Drawing.Size(360, 60)
        Me.labQuestion.TabIndex = 1
        Me.labQuestion.Text = "labQuestion"
        Me.labQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labMessage
        '
        Me.labMessage.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labMessage.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMessage.Location = New System.Drawing.Point(29, 23)
        Me.labMessage.Name = "labMessage"
        Me.labMessage.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.labMessage.Size = New System.Drawing.Size(360, 55)
        Me.labMessage.TabIndex = 2
        Me.labMessage.Text = "labMessage"
        '
        'chkEmail
        '
        Me.chkEmail.BackColor = System.Drawing.Color.LavenderBlush
        Me.chkEmail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmail.Location = New System.Drawing.Point(21, 21)
        Me.chkEmail.Name = "chkEmail"
        Me.chkEmail.Size = New System.Drawing.Size(71, 33)
        Me.chkEmail.TabIndex = 4
        Me.chkEmail.Text = "Email"
        Me.chkEmail.UseVisualStyleBackColor = False
        '
        'chkPrint
        '
        Me.chkPrint.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrint.Location = New System.Drawing.Point(111, 21)
        Me.chkPrint.Name = "chkPrint"
        Me.chkPrint.Size = New System.Drawing.Size(61, 33)
        Me.chkPrint.TabIndex = 5
        Me.chkPrint.Text = "Print"
        Me.chkPrint.UseVisualStyleBackColor = False
        '
        'grpBoxDocAction
        '
        Me.grpBoxDocAction.Controls.Add(Me.Label1)
        Me.grpBoxDocAction.Controls.Add(Me.panelDocOpts)
        Me.grpBoxDocAction.Controls.Add(Me.labDocType)
        Me.grpBoxDocAction.Controls.Add(Me.labEmail)
        Me.grpBoxDocAction.Controls.Add(Me.chkPrint)
        Me.grpBoxDocAction.Controls.Add(Me.chkEmail)
        Me.grpBoxDocAction.Location = New System.Drawing.Point(32, 174)
        Me.grpBoxDocAction.Name = "grpBoxDocAction"
        Me.grpBoxDocAction.Size = New System.Drawing.Size(357, 159)
        Me.grpBoxDocAction.TabIndex = 7
        Me.grpBoxDocAction.TabStop = False
        Me.grpBoxDocAction.Text = "Check Document Action"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(28, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 35)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Be sure to select the correct printer dropdown."
        '
        'panelDocOpts
        '
        Me.panelDocOpts.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelDocOpts.Controls.Add(Me.optConfirmDocket)
        Me.panelDocOpts.Controls.Add(Me.optConfirmA4)
        Me.panelDocOpts.Location = New System.Drawing.Point(174, 62)
        Me.panelDocOpts.Name = "panelDocOpts"
        Me.panelDocOpts.Size = New System.Drawing.Size(168, 43)
        Me.panelDocOpts.TabIndex = 8
        '
        'optConfirmDocket
        '
        Me.optConfirmDocket.AutoSize = True
        Me.optConfirmDocket.Location = New System.Drawing.Point(94, 13)
        Me.optConfirmDocket.Name = "optConfirmDocket"
        Me.optConfirmDocket.Size = New System.Drawing.Size(58, 17)
        Me.optConfirmDocket.TabIndex = 1
        Me.optConfirmDocket.TabStop = True
        Me.optConfirmDocket.Text = "Docket"
        Me.optConfirmDocket.UseVisualStyleBackColor = True
        '
        'optConfirmA4
        '
        Me.optConfirmA4.AutoSize = True
        Me.optConfirmA4.Location = New System.Drawing.Point(12, 13)
        Me.optConfirmA4.Name = "optConfirmA4"
        Me.optConfirmA4.Size = New System.Drawing.Size(76, 17)
        Me.optConfirmA4.TabIndex = 0
        Me.optConfirmA4.TabStop = True
        Me.optConfirmA4.Text = "A4 Invoice"
        Me.optConfirmA4.UseVisualStyleBackColor = True
        '
        'labDocType
        '
        Me.labDocType.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDocType.Location = New System.Drawing.Point(185, 28)
        Me.labDocType.Name = "labDocType"
        Me.labDocType.Size = New System.Drawing.Size(157, 26)
        Me.labDocType.TabIndex = 7
        Me.labDocType.Text = "labDocType"
        Me.labDocType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labEmail
        '
        Me.labEmail.Location = New System.Drawing.Point(18, 122)
        Me.labEmail.Name = "labEmail"
        Me.labEmail.Size = New System.Drawing.Size(295, 15)
        Me.labEmail.TabIndex = 6
        Me.labEmail.Text = "labEmail"
        '
        'panelPrinters
        '
        Me.panelPrinters.Controls.Add(Me.Label21)
        Me.panelPrinters.Controls.Add(Me.cboReceiptPrinters)
        Me.panelPrinters.Controls.Add(Me.Label20)
        Me.panelPrinters.Controls.Add(Me.cboInvoicePrinters)
        Me.panelPrinters.Location = New System.Drawing.Point(419, 13)
        Me.panelPrinters.Name = "panelPrinters"
        Me.panelPrinters.Size = New System.Drawing.Size(209, 122)
        Me.panelPrinters.TabIndex = 8
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(23, 61)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(92, 13)
        Me.Label21.TabIndex = 63
        Me.Label21.Text = "Docket Printer:"
        '
        'cboReceiptPrinters
        '
        Me.cboReceiptPrinters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboReceiptPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReceiptPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReceiptPrinters.FormattingEnabled = True
        Me.cboReceiptPrinters.Location = New System.Drawing.Point(15, 77)
        Me.cboReceiptPrinters.Name = "cboReceiptPrinters"
        Me.cboReceiptPrinters.Size = New System.Drawing.Size(182, 21)
        Me.cboReceiptPrinters.TabIndex = 62
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(22, 10)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(112, 13)
        Me.Label20.TabIndex = 61
        Me.Label20.Text = "A4 Invoice Printer:"
        '
        'cboInvoicePrinters
        '
        Me.cboInvoicePrinters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboInvoicePrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvoicePrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboInvoicePrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvoicePrinters.FormattingEnabled = True
        Me.cboInvoicePrinters.Location = New System.Drawing.Point(17, 26)
        Me.cboInvoicePrinters.Name = "cboInvoicePrinters"
        Me.cboInvoicePrinters.Size = New System.Drawing.Size(180, 21)
        Me.cboInvoicePrinters.TabIndex = 60
        '
        'labPdfPrinter
        '
        Me.labPdfPrinter.Location = New System.Drawing.Point(416, 195)
        Me.labPdfPrinter.Name = "labPdfPrinter"
        Me.labPdfPrinter.Size = New System.Drawing.Size(200, 19)
        Me.labPdfPrinter.TabIndex = 72
        Me.labPdfPrinter.Text = "labPdfPrinter"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(416, 145)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(185, 43)
        Me.Label8.TabIndex = 71
        Me.Label8.Text = "Note: An Adobe (or Microsoft) PDF Printer must be available for Emailing function" & _
    ".. Yours is:"
        '
        'dlgQueryCommit
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(643, 355)
        Me.Controls.Add(Me.labPdfPrinter)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.panelPrinters)
        Me.Controls.Add(Me.grpBoxDocAction)
        Me.Controls.Add(Me.labMessage)
        Me.Controls.Add(Me.labQuestion)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "dlgQueryCommit"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "dlgQueryCommit"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.grpBoxDocAction.ResumeLayout(False)
        Me.panelDocOpts.ResumeLayout(False)
        Me.panelDocOpts.PerformLayout()
        Me.panelPrinters.ResumeLayout(False)
        Me.panelPrinters.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents labQuestion As System.Windows.Forms.Label
    Friend WithEvents labMessage As System.Windows.Forms.Label
    Friend WithEvents chkEmail As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrint As System.Windows.Forms.CheckBox
    Friend WithEvents grpBoxDocAction As System.Windows.Forms.GroupBox
    Friend WithEvents labEmail As System.Windows.Forms.Label
    Friend WithEvents panelPrinters As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboReceiptPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cboInvoicePrinters As System.Windows.Forms.ComboBox
    Friend WithEvents labDocType As System.Windows.Forms.Label
    Friend WithEvents labPdfPrinter As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents panelDocOpts As System.Windows.Forms.Panel
    Friend WithEvents optConfirmDocket As System.Windows.Forms.RadioButton
    Friend WithEvents optConfirmA4 As System.Windows.Forms.RadioButton

End Class
