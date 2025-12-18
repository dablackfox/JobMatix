<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowInvoice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShowInvoice))
        Me.labCustNameLab = New System.Windows.Forms.Label()
        Me.labHdr1 = New System.Windows.Forms.Label()
        Me.labInvoiceDate = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.txtCustDeliveryAddress = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.listViewSaleItems = New System.Windows.Forms.ListView()
        Me.ItemBarcode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SerialNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Description = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Qty = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Price = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Amount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.listPaymentDetail = New System.Windows.Forms.ListBox()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.labSaleStaff = New System.Windows.Forms.Label()
        Me.labJobNo = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnPrintInvoice = New System.Windows.Forms.Button()
        Me.panelSaleTotals = New System.Windows.Forms.Panel()
        Me.txtTotalTax = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtDiscountTax = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtSubTotalTax = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtSaleSubTotal = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtSaleDiscount = New System.Windows.Forms.TextBox()
        Me.txtSaleRounding = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtSaleTotal = New System.Windows.Forms.TextBox()
        Me.labPaymentTotal = New System.Windows.Forms.Label()
        Me.listPayments = New System.Windows.Forms.ListBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.labPaymentsListHdr = New System.Windows.Forms.Label()
        Me.btnPrintReceipt = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboInvoicePrinters = New System.Windows.Forms.ComboBox()
        Me.cboReceiptPrinters = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.grpBoxPayments = New System.Windows.Forms.GroupBox()
        Me.labRefundHdr = New System.Windows.Forms.Label()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.labPdfPrinter = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.picEmailInvoice = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.labShowTill = New System.Windows.Forms.Label()
        Me.btnAccountReversal = New System.Windows.Forms.Button()
        Me.btnCashSaleReversal = New System.Windows.Forms.Button()
        Me.panelSaleTotals.SuspendLayout()
        Me.grpBoxPayments.SuspendLayout()
        CType(Me.picEmailInvoice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'labCustNameLab
        '
        Me.labCustNameLab.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labCustNameLab.Location = New System.Drawing.Point(16, 68)
        Me.labCustNameLab.Name = "labCustNameLab"
        Me.labCustNameLab.Size = New System.Drawing.Size(145, 13)
        Me.labCustNameLab.TabIndex = 2
        Me.labCustNameLab.Text = "Customer:"
        '
        'labHdr1
        '
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr1.Location = New System.Drawing.Point(23, 11)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Size = New System.Drawing.Size(198, 21)
        Me.labHdr1.TabIndex = 3
        Me.labHdr1.Text = "Details for Sales Invoice # "
        Me.labHdr1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labInvoiceDate
        '
        Me.labInvoiceDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labInvoiceDate.Location = New System.Drawing.Point(290, 12)
        Me.labInvoiceDate.Name = "labInvoiceDate"
        Me.labInvoiceDate.Size = New System.Drawing.Size(117, 35)
        Me.labInvoiceDate.TabIndex = 5
        Me.labInvoiceDate.Text = "labInvoiceDate"
        '
        'txtCustName
        '
        Me.txtCustName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.Location = New System.Drawing.Point(18, 83)
        Me.txtCustName.Multiline = True
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.ReadOnly = True
        Me.txtCustName.Size = New System.Drawing.Size(242, 34)
        Me.txtCustName.TabIndex = 7
        Me.txtCustName.TabStop = False
        '
        'txtCustDeliveryAddress
        '
        Me.txtCustDeliveryAddress.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustDeliveryAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustDeliveryAddress.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustDeliveryAddress.Location = New System.Drawing.Point(538, 219)
        Me.txtCustDeliveryAddress.Multiline = True
        Me.txtCustDeliveryAddress.Name = "txtCustDeliveryAddress"
        Me.txtCustDeliveryAddress.ReadOnly = True
        Me.txtCustDeliveryAddress.Size = New System.Drawing.Size(179, 53)
        Me.txtCustDeliveryAddress.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(536, 203)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Deivery Address:"
        '
        'listViewSaleItems
        '
        Me.listViewSaleItems.BackColor = System.Drawing.Color.FloralWhite
        Me.listViewSaleItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ItemBarcode, Me.SerialNo, Me.Description, Me.Qty, Me.Price, Me.Amount})
        Me.listViewSaleItems.FullRowSelect = True
        Me.listViewSaleItems.GridLines = True
        Me.listViewSaleItems.Location = New System.Drawing.Point(19, 146)
        Me.listViewSaleItems.Name = "listViewSaleItems"
        Me.listViewSaleItems.Size = New System.Drawing.Size(501, 247)
        Me.listViewSaleItems.TabIndex = 35
        Me.listViewSaleItems.TabStop = False
        Me.listViewSaleItems.UseCompatibleStateImageBehavior = False
        Me.listViewSaleItems.View = System.Windows.Forms.View.Details
        '
        'ItemBarcode
        '
        Me.ItemBarcode.Text = "ItemBarcode"
        Me.ItemBarcode.Width = 90
        '
        'SerialNo
        '
        Me.SerialNo.Text = "Serial No"
        Me.SerialNo.Width = 70
        '
        'Description
        '
        Me.Description.Text = "Description"
        Me.Description.Width = 140
        '
        'Qty
        '
        Me.Qty.Text = "Qty"
        Me.Qty.Width = 40
        '
        'Price
        '
        Me.Price.Text = "Price"
        '
        'Amount
        '
        Me.Amount.Text = "Ext.Amount"
        Me.Amount.Width = 80
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(15, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 17)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Items Invoiced"
        '
        'listPaymentDetail
        '
        Me.listPaymentDetail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.listPaymentDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listPaymentDetail.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listPaymentDetail.FormattingEnabled = True
        Me.listPaymentDetail.ItemHeight = 11
        Me.listPaymentDetail.Location = New System.Drawing.Point(211, 41)
        Me.listPaymentDetail.Name = "listPaymentDetail"
        Me.listPaymentDetail.Size = New System.Drawing.Size(283, 121)
        Me.listPaymentDetail.TabIndex = 37
        Me.listPaymentDetail.TabStop = False
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtComments.Location = New System.Drawing.Point(538, 302)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(276, 90)
        Me.txtComments.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(535, 286)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Comments"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(291, 83)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Sold By:"
        '
        'labSaleStaff
        '
        Me.labSaleStaff.Location = New System.Drawing.Point(341, 83)
        Me.labSaleStaff.Name = "labSaleStaff"
        Me.labSaleStaff.Size = New System.Drawing.Size(81, 16)
        Me.labSaleStaff.TabIndex = 42
        Me.labSaleStaff.Text = "labSaleStaff"
        '
        'labJobNo
        '
        Me.labJobNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJobNo.Location = New System.Drawing.Point(355, 105)
        Me.labJobNo.Name = "labJobNo"
        Me.labJobNo.Size = New System.Drawing.Size(52, 17)
        Me.labJobNo.TabIndex = 43
        Me.labJobNo.Text = "labJobNo"
        Me.labJobNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Firebrick
        Me.Label9.Location = New System.Drawing.Point(291, 105)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 17)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "Job No:"
        '
        'btnPrintInvoice
        '
        Me.btnPrintInvoice.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPrintInvoice.Location = New System.Drawing.Point(557, 94)
        Me.btnPrintInvoice.Name = "btnPrintInvoice"
        Me.btnPrintInvoice.Size = New System.Drawing.Size(84, 23)
        Me.btnPrintInvoice.TabIndex = 2
        Me.btnPrintInvoice.Text = "Print Invoice"
        Me.ToolTip1.SetToolTip(Me.btnPrintInvoice, "Prints A4 Invoice..")
        Me.btnPrintInvoice.UseVisualStyleBackColor = False
        '
        'panelSaleTotals
        '
        Me.panelSaleTotals.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelSaleTotals.Controls.Add(Me.txtTotalTax)
        Me.panelSaleTotals.Controls.Add(Me.Label19)
        Me.panelSaleTotals.Controls.Add(Me.Label18)
        Me.panelSaleTotals.Controls.Add(Me.txtDiscountTax)
        Me.panelSaleTotals.Controls.Add(Me.Label10)
        Me.panelSaleTotals.Controls.Add(Me.txtSubTotalTax)
        Me.panelSaleTotals.Controls.Add(Me.Label17)
        Me.panelSaleTotals.Controls.Add(Me.txtSaleSubTotal)
        Me.panelSaleTotals.Controls.Add(Me.Label13)
        Me.panelSaleTotals.Controls.Add(Me.Label14)
        Me.panelSaleTotals.Controls.Add(Me.txtSaleDiscount)
        Me.panelSaleTotals.Controls.Add(Me.txtSaleRounding)
        Me.panelSaleTotals.Controls.Add(Me.Label15)
        Me.panelSaleTotals.Controls.Add(Me.Label16)
        Me.panelSaleTotals.Controls.Add(Me.txtSaleTotal)
        Me.panelSaleTotals.Location = New System.Drawing.Point(538, 420)
        Me.panelSaleTotals.Name = "panelSaleTotals"
        Me.panelSaleTotals.Size = New System.Drawing.Size(276, 252)
        Me.panelSaleTotals.TabIndex = 46
        '
        'txtTotalTax
        '
        Me.txtTotalTax.BackColor = System.Drawing.Color.LightGray
        Me.txtTotalTax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalTax.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalTax.Location = New System.Drawing.Point(123, 126)
        Me.txtTotalTax.Name = "txtTotalTax"
        Me.txtTotalTax.ReadOnly = True
        Me.txtTotalTax.Size = New System.Drawing.Size(84, 15)
        Me.txtTotalTax.TabIndex = 36
        Me.txtTotalTax.TabStop = False
        Me.txtTotalTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(60, 127)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(58, 14)
        Me.Label19.TabIndex = 35
        Me.Label19.Text = "Nett Tax:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.Color.Gray
        Me.Label18.Location = New System.Drawing.Point(38, 107)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(82, 14)
        Me.Label18.TabIndex = 34
        Me.Label18.Text = "(Disc. Tax)"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDiscountTax
        '
        Me.txtDiscountTax.BackColor = System.Drawing.Color.LightGray
        Me.txtDiscountTax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDiscountTax.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscountTax.ForeColor = System.Drawing.Color.Gray
        Me.txtDiscountTax.Location = New System.Drawing.Point(123, 106)
        Me.txtDiscountTax.Name = "txtDiscountTax"
        Me.txtDiscountTax.ReadOnly = True
        Me.txtDiscountTax.Size = New System.Drawing.Size(84, 15)
        Me.txtDiscountTax.TabIndex = 33
        Me.txtDiscountTax.TabStop = False
        Me.txtDiscountTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.Gray
        Me.Label10.Location = New System.Drawing.Point(38, 41)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 14)
        Me.Label10.TabIndex = 32
        Me.Label10.Text = "(Includes Tax)"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSubTotalTax
        '
        Me.txtSubTotalTax.BackColor = System.Drawing.Color.LightGray
        Me.txtSubTotalTax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSubTotalTax.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotalTax.ForeColor = System.Drawing.Color.Gray
        Me.txtSubTotalTax.Location = New System.Drawing.Point(123, 40)
        Me.txtSubTotalTax.Name = "txtSubTotalTax"
        Me.txtSubTotalTax.ReadOnly = True
        Me.txtSubTotalTax.Size = New System.Drawing.Size(84, 15)
        Me.txtSubTotalTax.TabIndex = 31
        Me.txtSubTotalTax.TabStop = False
        Me.txtSubTotalTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(10, 12)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(107, 13)
        Me.Label17.TabIndex = 51
        Me.Label17.Text = "Invoice Summary"
        '
        'txtSaleSubTotal
        '
        Me.txtSaleSubTotal.BackColor = System.Drawing.Color.LightGray
        Me.txtSaleSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleSubTotal.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleSubTotal.Location = New System.Drawing.Point(123, 61)
        Me.txtSaleSubTotal.Name = "txtSaleSubTotal"
        Me.txtSaleSubTotal.ReadOnly = True
        Me.txtSaleSubTotal.Size = New System.Drawing.Size(84, 15)
        Me.txtSaleSubTotal.TabIndex = 25
        Me.txtSaleSubTotal.TabStop = False
        Me.txtSaleSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(60, 62)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(58, 14)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "Sub-total:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(60, 84)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(58, 14)
        Me.Label14.TabIndex = 16
        Me.Label14.Text = "Discount:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSaleDiscount
        '
        Me.txtSaleDiscount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaleDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleDiscount.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleDiscount.Location = New System.Drawing.Point(123, 85)
        Me.txtSaleDiscount.Name = "txtSaleDiscount"
        Me.txtSaleDiscount.Size = New System.Drawing.Size(84, 15)
        Me.txtSaleDiscount.TabIndex = 26
        Me.txtSaleDiscount.TabStop = False
        Me.txtSaleDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSaleRounding
        '
        Me.txtSaleRounding.BackColor = System.Drawing.Color.LightGray
        Me.txtSaleRounding.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleRounding.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleRounding.Location = New System.Drawing.Point(123, 171)
        Me.txtSaleRounding.Name = "txtSaleRounding"
        Me.txtSaleRounding.ReadOnly = True
        Me.txtSaleRounding.Size = New System.Drawing.Size(84, 15)
        Me.txtSaleRounding.TabIndex = 27
        Me.txtSaleRounding.TabStop = False
        Me.txtSaleRounding.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(60, 171)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(58, 14)
        Me.Label15.TabIndex = 20
        Me.Label15.Text = "Rounding:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(35, 211)
        Me.Label16.Margin = New System.Windows.Forms.Padding(0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(85, 14)
        Me.Label16.TabIndex = 21
        Me.Label16.Text = "Invoice Total:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSaleTotal
        '
        Me.txtSaleTotal.BackColor = System.Drawing.Color.LightGray
        Me.txtSaleTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleTotal.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleTotal.Location = New System.Drawing.Point(123, 210)
        Me.txtSaleTotal.Name = "txtSaleTotal"
        Me.txtSaleTotal.ReadOnly = True
        Me.txtSaleTotal.Size = New System.Drawing.Size(84, 15)
        Me.txtSaleTotal.TabIndex = 28
        Me.txtSaleTotal.TabStop = False
        Me.txtSaleTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labPaymentTotal
        '
        Me.labPaymentTotal.BackColor = System.Drawing.Color.Gainsboro
        Me.labPaymentTotal.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPaymentTotal.Location = New System.Drawing.Point(208, 179)
        Me.labPaymentTotal.Name = "labPaymentTotal"
        Me.labPaymentTotal.Size = New System.Drawing.Size(286, 63)
        Me.labPaymentTotal.TabIndex = 47
        Me.labPaymentTotal.Text = "labPaymentTotal"
        Me.labPaymentTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'listPayments
        '
        Me.listPayments.Font = New System.Drawing.Font("Lucida Console", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listPayments.FormattingEnabled = True
        Me.listPayments.ItemHeight = 9
        Me.listPayments.Location = New System.Drawing.Point(8, 58)
        Me.listPayments.Name = "listPayments"
        Me.listPayments.ScrollAlwaysVisible = True
        Me.listPayments.Size = New System.Drawing.Size(194, 184)
        Me.listPayments.TabIndex = 48
        Me.listPayments.TabStop = False
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(7, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 26)
        Me.Label11.TabIndex = 49
        Me.Label11.Text = "Payments ID's  for this Invoice-"
        '
        'labPaymentsListHdr
        '
        Me.labPaymentsListHdr.Location = New System.Drawing.Point(199, 23)
        Me.labPaymentsListHdr.Name = "labPaymentsListHdr"
        Me.labPaymentsListHdr.Size = New System.Drawing.Size(207, 13)
        Me.labPaymentsListHdr.TabIndex = 50
        Me.labPaymentsListHdr.Text = "Payment made with this Invoice-"
        '
        'btnPrintReceipt
        '
        Me.btnPrintReceipt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPrintReceipt.Location = New System.Drawing.Point(696, 94)
        Me.btnPrintReceipt.Name = "btnPrintReceipt"
        Me.btnPrintReceipt.Size = New System.Drawing.Size(84, 23)
        Me.btnPrintReceipt.TabIndex = 3
        Me.btnPrintReceipt.Text = "Print Receipt"
        Me.ToolTip1.SetToolTip(Me.btnPrintReceipt, "Prints as docket format..")
        Me.btnPrintReceipt.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(557, 49)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(67, 13)
        Me.Label20.TabIndex = 57
        Me.Label20.Text = "A4  Printers:"
        '
        'cboInvoicePrinters
        '
        Me.cboInvoicePrinters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboInvoicePrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvoicePrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboInvoicePrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvoicePrinters.FormattingEnabled = True
        Me.cboInvoicePrinters.Location = New System.Drawing.Point(553, 65)
        Me.cboInvoicePrinters.Name = "cboInvoicePrinters"
        Me.cboInvoicePrinters.Size = New System.Drawing.Size(133, 21)
        Me.cboInvoicePrinters.TabIndex = 0
        '
        'cboReceiptPrinters
        '
        Me.cboReceiptPrinters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboReceiptPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReceiptPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReceiptPrinters.FormattingEnabled = True
        Me.cboReceiptPrinters.Location = New System.Drawing.Point(692, 65)
        Me.cboReceiptPrinters.Name = "cboReceiptPrinters"
        Me.cboReceiptPrinters.Size = New System.Drawing.Size(122, 21)
        Me.cboReceiptPrinters.TabIndex = 1
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(693, 49)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(88, 13)
        Me.Label21.TabIndex = 59
        Me.Label21.Text = "Docket Printers::"
        '
        'grpBoxPayments
        '
        Me.grpBoxPayments.Controls.Add(Me.listPayments)
        Me.grpBoxPayments.Controls.Add(Me.Label11)
        Me.grpBoxPayments.Controls.Add(Me.labPaymentTotal)
        Me.grpBoxPayments.Controls.Add(Me.listPaymentDetail)
        Me.grpBoxPayments.Controls.Add(Me.labPaymentsListHdr)
        Me.grpBoxPayments.Location = New System.Drawing.Point(16, 414)
        Me.grpBoxPayments.Name = "grpBoxPayments"
        Me.grpBoxPayments.Size = New System.Drawing.Size(504, 258)
        Me.grpBoxPayments.TabIndex = 60
        Me.grpBoxPayments.TabStop = False
        Me.grpBoxPayments.Text = "Payment Details"
        '
        'labRefundHdr
        '
        Me.labRefundHdr.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRefundHdr.ForeColor = System.Drawing.Color.Tomato
        Me.labRefundHdr.Location = New System.Drawing.Point(30, 40)
        Me.labRefundHdr.Name = "labRefundHdr"
        Me.labRefundHdr.Size = New System.Drawing.Size(97, 19)
        Me.labRefundHdr.TabIndex = 61
        Me.labRefundHdr.Text = "R e f u n d "
        '
        'labStatus
        '
        Me.labStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.ForeColor = System.Drawing.Color.Firebrick
        Me.labStatus.Location = New System.Drawing.Point(449, 14)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Padding = New System.Windows.Forms.Padding(3, 1, 0, 0)
        Me.labStatus.Size = New System.Drawing.Size(229, 17)
        Me.labStatus.TabIndex = 62
        Me.labStatus.Text = "labStatus"
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnExit.CausesValidation = False
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Location = New System.Drawing.Point(757, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(57, 22)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'labPdfPrinter
        '
        Me.labPdfPrinter.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labPdfPrinter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPdfPrinter.ForeColor = System.Drawing.Color.SaddleBrown
        Me.labPdfPrinter.Location = New System.Drawing.Point(537, 173)
        Me.labPdfPrinter.Name = "labPdfPrinter"
        Me.labPdfPrinter.Size = New System.Drawing.Size(210, 19)
        Me.labPdfPrinter.TabIndex = 74
        Me.labPdfPrinter.Text = "labPdfPrinter"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(537, 130)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(210, 43)
        Me.Label1.TabIndex = 73
        Me.Label1.Text = "Note: An Adobe (or Microsoft) PDF Printer must be available for Emailing function" &
    ".. Yours is:"
        '
        'picEmailInvoice
        '
        Me.picEmailInvoice.BackColor = System.Drawing.Color.WhiteSmoke
        Me.picEmailInvoice.Image = CType(resources.GetObject("picEmailInvoice.Image"), System.Drawing.Image)
        Me.picEmailInvoice.Location = New System.Drawing.Point(758, 169)
        Me.picEmailInvoice.Name = "picEmailInvoice"
        Me.picEmailInvoice.Size = New System.Drawing.Size(29, 25)
        Me.picEmailInvoice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picEmailInvoice.TabIndex = 75
        Me.picEmailInvoice.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picEmailInvoice, "Email this Invoice.." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(NB- may be a duplicate)..")
        '
        'labShowTill
        '
        Me.labShowTill.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labShowTill.ForeColor = System.Drawing.Color.DarkMagenta
        Me.labShowTill.Location = New System.Drawing.Point(290, 54)
        Me.labShowTill.Name = "labShowTill"
        Me.labShowTill.Size = New System.Drawing.Size(70, 19)
        Me.labShowTill.TabIndex = 76
        Me.labShowTill.Text = "Till-"
        '
        'btnAccountReversal
        '
        Me.btnAccountReversal.BackColor = System.Drawing.Color.LavenderBlush
        Me.btnAccountReversal.Enabled = False
        Me.btnAccountReversal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAccountReversal.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAccountReversal.Location = New System.Drawing.Point(734, 216)
        Me.btnAccountReversal.Name = "btnAccountReversal"
        Me.btnAccountReversal.Size = New System.Drawing.Size(80, 40)
        Me.btnAccountReversal.TabIndex = 78
        Me.btnAccountReversal.Text = "Reverse this Account Sale"
        Me.btnAccountReversal.UseVisualStyleBackColor = False
        '
        'btnCashSaleReversal
        '
        Me.btnCashSaleReversal.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.btnCashSaleReversal.Enabled = False
        Me.btnCashSaleReversal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCashSaleReversal.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCashSaleReversal.Location = New System.Drawing.Point(734, 262)
        Me.btnCashSaleReversal.Name = "btnCashSaleReversal"
        Me.btnCashSaleReversal.Size = New System.Drawing.Size(80, 24)
        Me.btnCashSaleReversal.TabIndex = 80
        Me.btnCashSaleReversal.Text = "Reverse this Cash Sale"
        Me.btnCashSaleReversal.UseVisualStyleBackColor = False
        '
        'frmShowInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnExit
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(850, 688)
        Me.Controls.Add(Me.btnCashSaleReversal)
        Me.Controls.Add(Me.btnAccountReversal)
        Me.Controls.Add(Me.labShowTill)
        Me.Controls.Add(Me.picEmailInvoice)
        Me.Controls.Add(Me.labPdfPrinter)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.labStatus)
        Me.Controls.Add(Me.labRefundHdr)
        Me.Controls.Add(Me.grpBoxPayments)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.cboReceiptPrinters)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.cboInvoicePrinters)
        Me.Controls.Add(Me.btnPrintReceipt)
        Me.Controls.Add(Me.panelSaleTotals)
        Me.Controls.Add(Me.btnPrintInvoice)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.labJobNo)
        Me.Controls.Add(Me.labSaleStaff)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtComments)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.listViewSaleItems)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCustDeliveryAddress)
        Me.Controls.Add(Me.txtCustName)
        Me.Controls.Add(Me.labInvoiceDate)
        Me.Controls.Add(Me.labHdr1)
        Me.Controls.Add(Me.labCustNameLab)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmShowInvoice"
        Me.Text = "frmShowInvoice"
        Me.panelSaleTotals.ResumeLayout(False)
        Me.panelSaleTotals.PerformLayout()
        Me.grpBoxPayments.ResumeLayout(False)
        CType(Me.picEmailInvoice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents labCustNameLab As System.Windows.Forms.Label
    Friend WithEvents labHdr1 As System.Windows.Forms.Label
    Friend WithEvents labInvoiceDate As System.Windows.Forms.Label
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustDeliveryAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents listViewSaleItems As System.Windows.Forms.ListView
    Friend WithEvents ItemBarcode As System.Windows.Forms.ColumnHeader
    Friend WithEvents Description As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Amount As System.Windows.Forms.ColumnHeader
    Friend WithEvents Qty As System.Windows.Forms.ColumnHeader
    Friend WithEvents listPaymentDetail As System.Windows.Forms.ListBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents labSaleStaff As System.Windows.Forms.Label
    Friend WithEvents labJobNo As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnPrintInvoice As System.Windows.Forms.Button
    Friend WithEvents panelSaleTotals As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtSubTotalTax As System.Windows.Forms.TextBox
    Friend WithEvents txtSaleSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtSaleDiscount As System.Windows.Forms.TextBox
    Friend WithEvents txtSaleRounding As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtSaleTotal As System.Windows.Forms.TextBox
    Friend WithEvents labPaymentTotal As System.Windows.Forms.Label
    Friend WithEvents Price As System.Windows.Forms.ColumnHeader
    Friend WithEvents listPayments As System.Windows.Forms.ListBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents labPaymentsListHdr As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtDiscountTax As System.Windows.Forms.TextBox
    Friend WithEvents txtTotalTax As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnPrintReceipt As System.Windows.Forms.Button
    Friend WithEvents SerialNo As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cboInvoicePrinters As System.Windows.Forms.ComboBox
    Friend WithEvents cboReceiptPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents grpBoxPayments As System.Windows.Forms.GroupBox
    Friend WithEvents labRefundHdr As System.Windows.Forms.Label
    Friend WithEvents labStatus As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents labPdfPrinter As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picEmailInvoice As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents labShowTill As System.Windows.Forms.Label
    Friend WithEvents btnAccountReversal As System.Windows.Forms.Button
    Friend WithEvents btnCashSaleReversal As Button
End Class
