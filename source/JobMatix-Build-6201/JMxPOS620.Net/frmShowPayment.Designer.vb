<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowPayment
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
        Me.labPaymentDate = New System.Windows.Forms.Label()
        Me.labHdr1 = New System.Windows.Forms.Label()
        Me.labRcvdStaff = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.labCustName = New System.Windows.Forms.Label()
        Me.grpBoxPayments = New System.Windows.Forms.GroupBox()
        Me.panelReverse = New System.Windows.Forms.Panel()
        Me.btnReverseNow = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.labInvoiceList = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labPaymentTotal = New System.Windows.Forms.Label()
        Me.listPaymentDetail = New System.Windows.Forms.ListBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cboReceiptPrinters = New System.Windows.Forms.ComboBox()
        Me.btnPrintReceipt = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.labVersion = New System.Windows.Forms.Label()
        Me.labHdrReversal = New System.Windows.Forms.Label()
        Me.labCallerStaff = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.labPdfPrinter = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.labShowTill = New System.Windows.Forms.Label()
        Me.grpBoxPayments.SuspendLayout()
        Me.panelReverse.SuspendLayout()
        Me.SuspendLayout()
        '
        'labPaymentDate
        '
        Me.labPaymentDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPaymentDate.Location = New System.Drawing.Point(290, 22)
        Me.labPaymentDate.Name = "labPaymentDate"
        Me.labPaymentDate.Size = New System.Drawing.Size(117, 35)
        Me.labPaymentDate.TabIndex = 8
        Me.labPaymentDate.Text = "labPaymentDate"
        '
        'labHdr1
        '
        Me.labHdr1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr1.Location = New System.Drawing.Point(25, 20)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labHdr1.Size = New System.Drawing.Size(246, 45)
        Me.labHdr1.TabIndex = 6
        Me.labHdr1.Text = "Details for Payment # "
        Me.labHdr1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labRcvdStaff
        '
        Me.labRcvdStaff.Location = New System.Drawing.Point(481, 24)
        Me.labRcvdStaff.Name = "labRcvdStaff"
        Me.labRcvdStaff.Size = New System.Drawing.Size(100, 16)
        Me.labRcvdStaff.TabIndex = 46
        Me.labRcvdStaff.Text = "labRcvdStaff"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(428, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 12)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "Rcvd By:"
        '
        'txtCustName
        '
        Me.txtCustName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.Location = New System.Drawing.Point(29, 99)
        Me.txtCustName.Multiline = True
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(263, 59)
        Me.txtCustName.TabIndex = 44
        '
        'labCustName
        '
        Me.labCustName.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labCustName.Location = New System.Drawing.Point(30, 83)
        Me.labCustName.Name = "labCustName"
        Me.labCustName.Size = New System.Drawing.Size(183, 13)
        Me.labCustName.TabIndex = 43
        Me.labCustName.Text = "Customer:"
        '
        'grpBoxPayments
        '
        Me.grpBoxPayments.Controls.Add(Me.panelReverse)
        Me.grpBoxPayments.Controls.Add(Me.labInvoiceList)
        Me.grpBoxPayments.Controls.Add(Me.Label1)
        Me.grpBoxPayments.Controls.Add(Me.labPaymentTotal)
        Me.grpBoxPayments.Controls.Add(Me.listPaymentDetail)
        Me.grpBoxPayments.Location = New System.Drawing.Point(29, 292)
        Me.grpBoxPayments.Name = "grpBoxPayments"
        Me.grpBoxPayments.Size = New System.Drawing.Size(563, 302)
        Me.grpBoxPayments.TabIndex = 0
        Me.grpBoxPayments.TabStop = False
        Me.grpBoxPayments.Text = " - Payment Details -"
        '
        'panelReverse
        '
        Me.panelReverse.BackColor = System.Drawing.Color.Gainsboro
        Me.panelReverse.Controls.Add(Me.btnReverseNow)
        Me.panelReverse.Controls.Add(Me.Label2)
        Me.panelReverse.Location = New System.Drawing.Point(382, 20)
        Me.panelReverse.Name = "panelReverse"
        Me.panelReverse.Padding = New System.Windows.Forms.Padding(7, 7, 0, 0)
        Me.panelReverse.Size = New System.Drawing.Size(164, 267)
        Me.panelReverse.TabIndex = 50
        '
        'btnReverseNow
        '
        Me.btnReverseNow.BackColor = System.Drawing.Color.Plum
        Me.btnReverseNow.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReverseNow.Location = New System.Drawing.Point(13, 205)
        Me.btnReverseNow.Name = "btnReverseNow"
        Me.btnReverseNow.Size = New System.Drawing.Size(133, 48)
        Me.btnReverseNow.TabIndex = 1
        Me.btnReverseNow.Text = "Reverse this Payment Now"
        Me.btnReverseNow.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(5, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(137, 180)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Payment Reversal-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click the button below to Reverse out this Debtors Payment. " & _
    " " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "A debit will be created to reverse all aspects of this Payment."
        '
        'labInvoiceList
        '
        Me.labInvoiceList.BackColor = System.Drawing.Color.Gainsboro
        Me.labInvoiceList.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labInvoiceList.Location = New System.Drawing.Point(13, 165)
        Me.labInvoiceList.Name = "labInvoiceList"
        Me.labInvoiceList.Size = New System.Drawing.Size(345, 54)
        Me.labInvoiceList.TabIndex = 49
        Me.labInvoiceList.Text = "labInvoiceList"
        Me.labInvoiceList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label1.Location = New System.Drawing.Point(13, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(345, 22)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Payment Details"
        '
        'labPaymentTotal
        '
        Me.labPaymentTotal.BackColor = System.Drawing.Color.Gainsboro
        Me.labPaymentTotal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPaymentTotal.Location = New System.Drawing.Point(13, 235)
        Me.labPaymentTotal.Name = "labPaymentTotal"
        Me.labPaymentTotal.Size = New System.Drawing.Size(345, 52)
        Me.labPaymentTotal.TabIndex = 47
        Me.labPaymentTotal.Text = "labPaymentTotal"
        Me.labPaymentTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'listPaymentDetail
        '
        Me.listPaymentDetail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.listPaymentDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listPaymentDetail.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listPaymentDetail.FormattingEnabled = True
        Me.listPaymentDetail.ItemHeight = 11
        Me.listPaymentDetail.Location = New System.Drawing.Point(13, 42)
        Me.listPaymentDetail.Name = "listPaymentDetail"
        Me.listPaymentDetail.Size = New System.Drawing.Size(345, 110)
        Me.listPaymentDetail.TabIndex = 37
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(417, 73)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(94, 13)
        Me.Label21.TabIndex = 64
        Me.Label21.Text = "Available Printers:"
        '
        'cboReceiptPrinters
        '
        Me.cboReceiptPrinters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboReceiptPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReceiptPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReceiptPrinters.FormattingEnabled = True
        Me.cboReceiptPrinters.Location = New System.Drawing.Point(409, 87)
        Me.cboReceiptPrinters.Name = "cboReceiptPrinters"
        Me.cboReceiptPrinters.Size = New System.Drawing.Size(183, 21)
        Me.cboReceiptPrinters.TabIndex = 1
        '
        'btnPrintReceipt
        '
        Me.btnPrintReceipt.Location = New System.Drawing.Point(497, 119)
        Me.btnPrintReceipt.Name = "btnPrintReceipt"
        Me.btnPrintReceipt.Size = New System.Drawing.Size(95, 23)
        Me.btnPrintReceipt.TabIndex = 2
        Me.btnPrintReceipt.Text = "Print Receipt"
        Me.btnPrintReceipt.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(30, 200)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 66
        Me.Label7.Text = "Comments"
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtComments.Location = New System.Drawing.Point(29, 216)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(263, 70)
        Me.txtComments.TabIndex = 65
        '
        'labVersion
        '
        Me.labVersion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labVersion.Location = New System.Drawing.Point(10, 597)
        Me.labVersion.Name = "labVersion"
        Me.labVersion.Size = New System.Drawing.Size(237, 13)
        Me.labVersion.TabIndex = 67
        Me.labVersion.Text = "labVersion"
        '
        'labHdrReversal
        '
        Me.labHdrReversal.BackColor = System.Drawing.Color.Plum
        Me.labHdrReversal.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdrReversal.Location = New System.Drawing.Point(358, 246)
        Me.labHdrReversal.Name = "labHdrReversal"
        Me.labHdrReversal.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labHdrReversal.Size = New System.Drawing.Size(234, 43)
        Me.labHdrReversal.TabIndex = 68
        Me.labHdrReversal.Text = "Reversing Payment #"
        Me.labHdrReversal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labCallerStaff
        '
        Me.labCallerStaff.Location = New System.Drawing.Point(481, 44)
        Me.labCallerStaff.Name = "labCallerStaff"
        Me.labCallerStaff.Size = New System.Drawing.Size(100, 16)
        Me.labCallerStaff.TabIndex = 70
        Me.labCallerStaff.Text = "labCallerStaff"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(428, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 12)
        Me.Label4.TabIndex = 69
        Me.Label4.Text = "You are:"
        '
        'labPdfPrinter
        '
        Me.labPdfPrinter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPdfPrinter.ForeColor = System.Drawing.Color.SaddleBrown
        Me.labPdfPrinter.Location = New System.Drawing.Point(382, 200)
        Me.labPdfPrinter.Name = "labPdfPrinter"
        Me.labPdfPrinter.Size = New System.Drawing.Size(210, 19)
        Me.labPdfPrinter.TabIndex = 76
        Me.labPdfPrinter.Text = "labPdfPrinter"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(382, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(210, 43)
        Me.Label3.TabIndex = 75
        Me.Label3.Text = "Note: An Adobe (or Microsoft) PDF Printer must be available for Emailing function" & _
    ".. Yours is:"
        '
        'labShowTill
        '
        Me.labShowTill.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labShowTill.ForeColor = System.Drawing.Color.DarkMagenta
        Me.labShowTill.Location = New System.Drawing.Point(290, 79)
        Me.labShowTill.Name = "labShowTill"
        Me.labShowTill.Size = New System.Drawing.Size(70, 19)
        Me.labShowTill.TabIndex = 77
        Me.labShowTill.Text = "Till-"
        '
        'frmShowPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(633, 613)
        Me.Controls.Add(Me.labShowTill)
        Me.Controls.Add(Me.labPdfPrinter)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.labCallerStaff)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.labHdrReversal)
        Me.Controls.Add(Me.labVersion)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.cboReceiptPrinters)
        Me.Controls.Add(Me.btnPrintReceipt)
        Me.Controls.Add(Me.grpBoxPayments)
        Me.Controls.Add(Me.labRcvdStaff)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtCustName)
        Me.Controls.Add(Me.labCustName)
        Me.Controls.Add(Me.labPaymentDate)
        Me.Controls.Add(Me.labHdr1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmShowPayment"
        Me.Text = "frmShowPayment"
        Me.grpBoxPayments.ResumeLayout(False)
        Me.panelReverse.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents labPaymentDate As System.Windows.Forms.Label
    Friend WithEvents labHdr1 As System.Windows.Forms.Label
    Friend WithEvents labRcvdStaff As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents labCustName As System.Windows.Forms.Label
    Friend WithEvents grpBoxPayments As System.Windows.Forms.GroupBox
    Friend WithEvents labPaymentTotal As System.Windows.Forms.Label
    Friend WithEvents listPaymentDetail As System.Windows.Forms.ListBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboReceiptPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrintReceipt As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents labInvoiceList As System.Windows.Forms.Label
    Friend WithEvents labVersion As System.Windows.Forms.Label
    Friend WithEvents panelReverse As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents labHdrReversal As System.Windows.Forms.Label
    Friend WithEvents btnReverseNow As System.Windows.Forms.Button
    Friend WithEvents labCallerStaff As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents labPdfPrinter As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents labShowTill As System.Windows.Forms.Label
End Class
