<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildGoodsRecvd
    Inherits System.Windows.Forms.UserControl  '= System.Windows.Forms.Form

    '-- Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucChildGoodsRecvd))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpBoxGoods = New System.Windows.Forms.GroupBox()
        Me.btnLookupGoods = New System.Windows.Forms.Button()
        Me.labCreatingPDF = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.TabControlInvoice = New System.Windows.Forms.TabControl()
        Me.TabPageInvoice = New System.Windows.Forms.TabPage()
        Me.panelInvoiceNo = New System.Windows.Forms.Panel()
        Me.btnGoodsContinue = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSupplierInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtPickerInvoiceDate = New System.Windows.Forms.DateTimePicker()
        Me.TabPagePrint = New System.Windows.Forms.TabPage()
        Me.panelPrinting = New System.Windows.Forms.Panel()
        Me.picEmailPO = New System.Windows.Forms.PictureBox()
        Me.labPdfPrinter = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboInvoicePrinters = New System.Windows.Forms.ComboBox()
        Me.btnCancelPO = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.panelStockLineEntry = New System.Windows.Forms.Panel()
        Me.txtStockItemSupplierCode = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.labStockLinePrompt = New System.Windows.Forms.Label()
        Me.btnStockLineOk = New System.Windows.Forms.Button()
        Me.txtStockItemDescription = New System.Windows.Forms.TextBox()
        Me.txtStockItemBarcode = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.clsDgvGoodsItems = New JMxPOS330.clsDgvGoods()
        Me.barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.supplierCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cat1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cat2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cost_ex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tax_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cost_inc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sell_ex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SalesTaxCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sell_inc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.extension_inc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.serialNos = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.serialNoList = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.stock_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.track_serial = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cost_tax = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cost_tax_extended = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.po_line_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.original_cost_ex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.original_cost_inc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.new_suppliercode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.panelGoodsFooter = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.labHelp2 = New System.Windows.Forms.Label()
        Me.labHelp = New System.Windows.Forms.Label()
        Me.txtGoodsDeliveryAddress = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnGoodsCancel = New System.Windows.Forms.Button()
        Me.btnGoodsCommit = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtGoodsComments = New System.Windows.Forms.TextBox()
        Me.panelGoodsTotals = New System.Windows.Forms.Panel()
        Me.chkCloseForBackorders = New System.Windows.Forms.CheckBox()
        Me.labDueDate = New System.Windows.Forms.Label()
        Me.DTPickerDueDate = New System.Windows.Forms.DateTimePicker()
        Me.txtGoodsTotal = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.labDiscountTax = New System.Windows.Forms.Label()
        Me.txtGoodsDiscountAnalysis = New System.Windows.Forms.TextBox()
        Me.labDiscount = New System.Windows.Forms.Label()
        Me.txtGoodsDiscount = New System.Windows.Forms.TextBox()
        Me.chkFreightIsIncl = New System.Windows.Forms.CheckBox()
        Me.txtFreightTax = New System.Windows.Forms.TextBox()
        Me.labFreightTax = New System.Windows.Forms.Label()
        Me.labFreight = New System.Windows.Forms.Label()
        Me.txtGoodsFreight = New System.Windows.Forms.TextBox()
        Me.txtGoodsTotalTax = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtGoodsSubTotal = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.labExpected = New System.Windows.Forms.Label()
        Me.txtTotalExpected = New System.Windows.Forms.TextBox()
        Me.txtGoodsNettTax = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.picGoodsItem = New System.Windows.Forms.PictureBox()
        Me.panelGoodsBanner = New System.Windows.Forms.Panel()
        Me.labHdrPO = New System.Windows.Forms.Label()
        Me.labHdrGR = New System.Windows.Forms.Label()
        Me.panelGoodsHdr = New System.Windows.Forms.Panel()
        Me.chkLoadPO = New System.Windows.Forms.CheckBox()
        Me.panelIncludes = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkIncludeClosedForBO = New System.Windows.Forms.CheckBox()
        Me.chkIncludeCompletedOrders = New System.Windows.Forms.CheckBox()
        Me.chkPriceIncludesTax = New System.Windows.Forms.CheckBox()
        Me.btnNewBlankPO = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnNewStandardOrder = New System.Windows.Forms.Button()
        Me.btnGoodsCancel2 = New System.Windows.Forms.Button()
        Me.txtOrderNoSuffix = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnViewPO = New System.Windows.Forms.Button()
        Me.labPO_id = New System.Windows.Forms.Label()
        Me.btnLoadPO = New System.Windows.Forms.Button()
        Me.LabSaleCustSrch = New System.Windows.Forms.Label()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.txtSupplierBarcode = New System.Windows.Forms.TextBox()
        Me.labHdrFormType = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpBoxGoods.SuspendLayout()
        Me.TabControlInvoice.SuspendLayout()
        Me.TabPageInvoice.SuspendLayout()
        Me.panelInvoiceNo.SuspendLayout()
        Me.TabPagePrint.SuspendLayout()
        Me.panelPrinting.SuspendLayout()
        CType(Me.picEmailPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelStockLineEntry.SuspendLayout()
        CType(Me.clsDgvGoodsItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelGoodsFooter.SuspendLayout()
        Me.panelGoodsTotals.SuspendLayout()
        CType(Me.picGoodsItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelGoodsBanner.SuspendLayout()
        Me.panelGoodsHdr.SuspendLayout()
        Me.panelIncludes.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpBoxGoods
        '
        Me.grpBoxGoods.BackColor = System.Drawing.Color.Snow
        Me.grpBoxGoods.CausesValidation = False
        Me.grpBoxGoods.Controls.Add(Me.btnLookupGoods)
        Me.grpBoxGoods.Controls.Add(Me.labCreatingPDF)
        Me.grpBoxGoods.Controls.Add(Me.btnExit)
        Me.grpBoxGoods.Controls.Add(Me.TabControlInvoice)
        Me.grpBoxGoods.Controls.Add(Me.panelStockLineEntry)
        Me.grpBoxGoods.Controls.Add(Me.clsDgvGoodsItems)
        Me.grpBoxGoods.Controls.Add(Me.panelGoodsFooter)
        Me.grpBoxGoods.Controls.Add(Me.panelGoodsBanner)
        Me.grpBoxGoods.Controls.Add(Me.panelGoodsHdr)
        Me.grpBoxGoods.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxGoods.Location = New System.Drawing.Point(0, 3)
        Me.grpBoxGoods.Name = "grpBoxGoods"
        Me.grpBoxGoods.Size = New System.Drawing.Size(1012, 625)
        Me.grpBoxGoods.TabIndex = 0
        Me.grpBoxGoods.TabStop = False
        Me.grpBoxGoods.Text = "grpBoxGoods"
        '
        'btnLookupGoods
        '
        Me.btnLookupGoods.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.btnLookupGoods.CausesValidation = False
        Me.btnLookupGoods.Location = New System.Drawing.Point(931, 109)
        Me.btnLookupGoods.Name = "btnLookupGoods"
        Me.btnLookupGoods.Size = New System.Drawing.Size(73, 48)
        Me.btnLookupGoods.TabIndex = 200
        Me.btnLookupGoods.Text = "Lookup Goods"
        Me.ToolTip1.SetToolTip(Me.btnLookupGoods, "Lookup previous Goods Received..")
        Me.btnLookupGoods.UseVisualStyleBackColor = False
        '
        'labCreatingPDF
        '
        Me.labCreatingPDF.BackColor = System.Drawing.Color.LavenderBlush
        Me.labCreatingPDF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.labCreatingPDF.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labCreatingPDF.Location = New System.Drawing.Point(931, 48)
        Me.labCreatingPDF.Name = "labCreatingPDF"
        Me.labCreatingPDF.Size = New System.Drawing.Size(74, 39)
        Me.labCreatingPDF.TabIndex = 203
        Me.labCreatingPDF.Text = "Creating PDF..."
        Me.labCreatingPDF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Lavender
        Me.btnExit.CausesValidation = False
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Location = New System.Drawing.Point(946, 11)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(47, 28)
        Me.btnExit.TabIndex = 202
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'TabControlInvoice
        '
        Me.TabControlInvoice.Controls.Add(Me.TabPageInvoice)
        Me.TabControlInvoice.Controls.Add(Me.TabPagePrint)
        Me.TabControlInvoice.Location = New System.Drawing.Point(667, 6)
        Me.TabControlInvoice.Name = "TabControlInvoice"
        Me.TabControlInvoice.SelectedIndex = 0
        Me.TabControlInvoice.Size = New System.Drawing.Size(254, 161)
        Me.TabControlInvoice.TabIndex = 1
        '
        'TabPageInvoice
        '
        Me.TabPageInvoice.Controls.Add(Me.panelInvoiceNo)
        Me.TabPageInvoice.Location = New System.Drawing.Point(4, 22)
        Me.TabPageInvoice.Name = "TabPageInvoice"
        Me.TabPageInvoice.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageInvoice.Size = New System.Drawing.Size(246, 135)
        Me.TabPageInvoice.TabIndex = 0
        Me.TabPageInvoice.Text = "Supplier Invoice"
        Me.TabPageInvoice.UseVisualStyleBackColor = True
        '
        'panelInvoiceNo
        '
        Me.panelInvoiceNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelInvoiceNo.Controls.Add(Me.btnGoodsContinue)
        Me.panelInvoiceNo.Controls.Add(Me.Label6)
        Me.panelInvoiceNo.Controls.Add(Me.txtSupplierInvoiceNo)
        Me.panelInvoiceNo.Controls.Add(Me.Label4)
        Me.panelInvoiceNo.Controls.Add(Me.Label5)
        Me.panelInvoiceNo.Controls.Add(Me.dtPickerInvoiceDate)
        Me.panelInvoiceNo.Location = New System.Drawing.Point(3, 3)
        Me.panelInvoiceNo.Name = "panelInvoiceNo"
        Me.panelInvoiceNo.Size = New System.Drawing.Size(240, 126)
        Me.panelInvoiceNo.TabIndex = 9
        '
        'btnGoodsContinue
        '
        Me.btnGoodsContinue.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.btnGoodsContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoodsContinue.Location = New System.Drawing.Point(81, 96)
        Me.btnGoodsContinue.Name = "btnGoodsContinue"
        Me.btnGoodsContinue.Size = New System.Drawing.Size(61, 26)
        Me.btnGoodsContinue.TabIndex = 2
        Me.btnGoodsContinue.Text = "Continue"
        Me.btnGoodsContinue.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(162, 29)
        Me.Label6.TabIndex = 113
        Me.Label6.Text = "Supplier Invoice  Info- You must Set these before proceeding.."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSupplierInvoiceNo
        '
        Me.txtSupplierInvoiceNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSupplierInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierInvoiceNo.CausesValidation = False
        Me.txtSupplierInvoiceNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierInvoiceNo.Location = New System.Drawing.Point(80, 35)
        Me.txtSupplierInvoiceNo.MaxLength = 20
        Me.txtSupplierInvoiceNo.Name = "txtSupplierInvoiceNo"
        Me.txtSupplierInvoiceNo.Size = New System.Drawing.Size(105, 21)
        Me.txtSupplierInvoiceNo.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(14, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 19)
        Me.Label4.TabIndex = 109
        Me.Label4.Text = "Inv.Date: "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(7, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 26)
        Me.Label5.TabIndex = 111
        Me.Label5.Text = "Invoice No. (mandatory)"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'dtPickerInvoiceDate
        '
        Me.dtPickerInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPickerInvoiceDate.Location = New System.Drawing.Point(80, 64)
        Me.dtPickerInvoiceDate.Name = "dtPickerInvoiceDate"
        Me.dtPickerInvoiceDate.Size = New System.Drawing.Size(104, 20)
        Me.dtPickerInvoiceDate.TabIndex = 1
        '
        'TabPagePrint
        '
        Me.TabPagePrint.Controls.Add(Me.panelPrinting)
        Me.TabPagePrint.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePrint.Name = "TabPagePrint"
        Me.TabPagePrint.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePrint.Size = New System.Drawing.Size(246, 135)
        Me.TabPagePrint.TabIndex = 1
        Me.TabPagePrint.Text = "PO Print"
        Me.TabPagePrint.UseVisualStyleBackColor = True
        '
        'panelPrinting
        '
        Me.panelPrinting.CausesValidation = False
        Me.panelPrinting.Controls.Add(Me.picEmailPO)
        Me.panelPrinting.Controls.Add(Me.labPdfPrinter)
        Me.panelPrinting.Controls.Add(Me.Label14)
        Me.panelPrinting.Controls.Add(Me.Label1)
        Me.panelPrinting.Controls.Add(Me.cboInvoicePrinters)
        Me.panelPrinting.Controls.Add(Me.btnCancelPO)
        Me.panelPrinting.Controls.Add(Me.btnPrint)
        Me.panelPrinting.Location = New System.Drawing.Point(6, 4)
        Me.panelPrinting.Name = "panelPrinting"
        Me.panelPrinting.Size = New System.Drawing.Size(237, 125)
        Me.panelPrinting.TabIndex = 11
        '
        'picEmailPO
        '
        Me.picEmailPO.BackColor = System.Drawing.Color.WhiteSmoke
        Me.picEmailPO.Image = CType(resources.GetObject("picEmailPO.Image"), System.Drawing.Image)
        Me.picEmailPO.Location = New System.Drawing.Point(200, 92)
        Me.picEmailPO.Name = "picEmailPO"
        Me.picEmailPO.Size = New System.Drawing.Size(29, 25)
        Me.picEmailPO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picEmailPO.TabIndex = 78
        Me.picEmailPO.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picEmailPO, "Email this Purchase Order." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Make sure it's not a duplicate)..")
        '
        'labPdfPrinter
        '
        Me.labPdfPrinter.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labPdfPrinter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPdfPrinter.ForeColor = System.Drawing.Color.SaddleBrown
        Me.labPdfPrinter.Location = New System.Drawing.Point(3, 103)
        Me.labPdfPrinter.Name = "labPdfPrinter"
        Me.labPdfPrinter.Size = New System.Drawing.Size(181, 19)
        Me.labPdfPrinter.TabIndex = 77
        Me.labPdfPrinter.Text = "labPdfPrinter"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(7, 73)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(222, 31)
        Me.Label14.TabIndex = 76
        Me.Label14.Text = "NB: A PDF Printer (Adobe or Microsoft) must be available for Emailing.. Yours is:" & _
    ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Available Printers:"
        '
        'cboInvoicePrinters
        '
        Me.cboInvoicePrinters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboInvoicePrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvoicePrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboInvoicePrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvoicePrinters.FormattingEnabled = True
        Me.cboInvoicePrinters.Location = New System.Drawing.Point(13, 48)
        Me.cboInvoicePrinters.Name = "cboInvoicePrinters"
        Me.cboInvoicePrinters.Size = New System.Drawing.Size(216, 21)
        Me.cboInvoicePrinters.TabIndex = 58
        '
        'btnCancelPO
        '
        Me.btnCancelPO.CausesValidation = False
        Me.btnCancelPO.Location = New System.Drawing.Point(155, 5)
        Me.btnCancelPO.Name = "btnCancelPO"
        Me.btnCancelPO.Size = New System.Drawing.Size(68, 25)
        Me.btnCancelPO.TabIndex = 1
        Me.btnCancelPO.Text = "Cancel PO"
        Me.ToolTip1.SetToolTip(Me.btnCancelPO, "Cancel this PO forever..")
        Me.btnCancelPO.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.CausesValidation = False
        Me.btnPrint.Location = New System.Drawing.Point(15, 8)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(79, 22)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print PO"
        Me.ToolTip1.SetToolTip(Me.btnPrint, "Print this PO.")
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'panelStockLineEntry
        '
        Me.panelStockLineEntry.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.panelStockLineEntry.Controls.Add(Me.txtStockItemSupplierCode)
        Me.panelStockLineEntry.Controls.Add(Me.Label13)
        Me.panelStockLineEntry.Controls.Add(Me.Label16)
        Me.panelStockLineEntry.Controls.Add(Me.labStockLinePrompt)
        Me.panelStockLineEntry.Controls.Add(Me.btnStockLineOk)
        Me.panelStockLineEntry.Controls.Add(Me.txtStockItemDescription)
        Me.panelStockLineEntry.Controls.Add(Me.txtStockItemBarcode)
        Me.panelStockLineEntry.Controls.Add(Me.Label22)
        Me.panelStockLineEntry.Location = New System.Drawing.Point(2, 172)
        Me.panelStockLineEntry.Name = "panelStockLineEntry"
        Me.panelStockLineEntry.Size = New System.Drawing.Size(1003, 62)
        Me.panelStockLineEntry.TabIndex = 2
        Me.panelStockLineEntry.TabStop = True
        '
        'txtStockItemSupplierCode
        '
        Me.txtStockItemSupplierCode.BackColor = System.Drawing.Color.White
        Me.txtStockItemSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStockItemSupplierCode.Font = New System.Drawing.Font("Lucida Sans Unicode", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockItemSupplierCode.Location = New System.Drawing.Point(206, 34)
        Me.txtStockItemSupplierCode.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtStockItemSupplierCode.MaxLength = 40
        Me.txtStockItemSupplierCode.Name = "txtStockItemSupplierCode"
        Me.txtStockItemSupplierCode.Size = New System.Drawing.Size(141, 21)
        Me.txtStockItemSupplierCode.TabIndex = 1
        Me.txtStockItemSupplierCode.Text = "txtStockItemSupplierCode"
        Me.ToolTip1.SetToolTip(Me.txtStockItemSupplierCode, "Enter   Supplier code if any..")
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(203, 13)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 15)
        Me.Label13.TabIndex = 42
        Me.Label13.Text = "Supplier-Code"
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.Color.Navy
        Me.Label16.Location = New System.Drawing.Point(885, 8)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(95, 44)
        Me.Label16.TabIndex = 40
        Me.Label16.Text = "Press F5 in Serials column to start New Item."
        '
        'labStockLinePrompt
        '
        Me.labStockLinePrompt.Location = New System.Drawing.Point(729, 4)
        Me.labStockLinePrompt.Name = "labStockLinePrompt"
        Me.labStockLinePrompt.Size = New System.Drawing.Size(141, 53)
        Me.labStockLinePrompt.TabIndex = 39
        Me.labStockLinePrompt.Text = "Scan or Enter the Stock Barcode..  Then finish off the Cost, Quantity and Serials" & _
    " in the Data Grid.."
        '
        'btnStockLineOk
        '
        Me.btnStockLineOk.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnStockLineOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStockLineOk.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStockLineOk.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnStockLineOk.Location = New System.Drawing.Point(643, 15)
        Me.btnStockLineOk.Name = "btnStockLineOk"
        Me.btnStockLineOk.Size = New System.Drawing.Size(70, 37)
        Me.btnStockLineOk.TabIndex = 3
        Me.btnStockLineOk.Text = "Add Item to Grid"
        Me.btnStockLineOk.UseVisualStyleBackColor = False
        '
        'txtStockItemDescription
        '
        Me.txtStockItemDescription.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtStockItemDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStockItemDescription.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockItemDescription.Location = New System.Drawing.Point(365, 33)
        Me.txtStockItemDescription.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtStockItemDescription.MaxLength = 60
        Me.txtStockItemDescription.Name = "txtStockItemDescription"
        Me.txtStockItemDescription.ReadOnly = True
        Me.txtStockItemDescription.Size = New System.Drawing.Size(267, 19)
        Me.txtStockItemDescription.TabIndex = 2
        Me.txtStockItemDescription.TabStop = False
        Me.txtStockItemDescription.Text = "txtStockItemDescription"
        '
        'txtStockItemBarcode
        '
        Me.txtStockItemBarcode.BackColor = System.Drawing.Color.White
        Me.txtStockItemBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStockItemBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockItemBarcode.Location = New System.Drawing.Point(20, 33)
        Me.txtStockItemBarcode.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtStockItemBarcode.MaxLength = 40
        Me.txtStockItemBarcode.Name = "txtStockItemBarcode"
        Me.txtStockItemBarcode.Size = New System.Drawing.Size(159, 21)
        Me.txtStockItemBarcode.TabIndex = 0
        Me.txtStockItemBarcode.Text = "txtStockItemBarcode"
        Me.ToolTip1.SetToolTip(Me.txtStockItemBarcode, "Enter or Scan Stock Barcode- (F2 to Lookup Stocklist, or F5 to Create New Item wi" & _
        "th Auto Barcode).")
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(17, 13)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(101, 15)
        Me.Label22.TabIndex = 29
        Me.Label22.Text = "Item Barcode: "
        '
        'clsDgvGoodsItems
        '
        Me.clsDgvGoodsItems.AllowUserToAddRows = False
        Me.clsDgvGoodsItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.clsDgvGoodsItems.BackgroundColor = System.Drawing.Color.Snow
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.clsDgvGoodsItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.clsDgvGoodsItems.ColumnHeadersHeight = 22
        Me.clsDgvGoodsItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.barcode, Me.supplierCode, Me.Cat1, Me.Cat2, Me.Description, Me.cost_ex, Me.tax_code, Me.cost_inc, Me.sell_ex, Me.SalesTaxCode, Me.sell_inc, Me.qty, Me.extension_inc, Me.serialNos, Me.serialNoList, Me.stock_id, Me.track_serial, Me.cost_tax, Me.cost_tax_extended, Me.po_line_id, Me.original_cost_ex, Me.original_cost_inc, Me.new_suppliercode})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.clsDgvGoodsItems.DefaultCellStyle = DataGridViewCellStyle8
        Me.clsDgvGoodsItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.clsDgvGoodsItems.GridColor = System.Drawing.SystemColors.ControlLight
        Me.clsDgvGoodsItems.lastEditableColumn = 5
        Me.clsDgvGoodsItems.Location = New System.Drawing.Point(2, 240)
        Me.clsDgvGoodsItems.MultiSelect = False
        Me.clsDgvGoodsItems.Name = "clsDgvGoodsItems"
        Me.clsDgvGoodsItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.clsDgvGoodsItems.Size = New System.Drawing.Size(1003, 170)
        Me.clsDgvGoodsItems.StandardTab = True
        Me.clsDgvGoodsItems.TabIndex = 3
        '
        'barcode
        '
        Me.barcode.FillWeight = 47.17342!
        Me.barcode.HeaderText = "Barcode"
        Me.barcode.MaxInputLength = 40
        Me.barcode.MinimumWidth = 20
        Me.barcode.Name = "barcode"
        Me.barcode.ReadOnly = True
        '
        'supplierCode
        '
        Me.supplierCode.FillWeight = 26.95624!
        Me.supplierCode.HeaderText = "Suppl.Code"
        Me.supplierCode.MinimumWidth = 20
        Me.supplierCode.Name = "supplierCode"
        Me.supplierCode.ReadOnly = True
        '
        'Cat1
        '
        Me.Cat1.FillWeight = 20.0!
        Me.Cat1.HeaderText = "Cat1"
        Me.Cat1.MaxInputLength = 6
        Me.Cat1.Name = "Cat1"
        Me.Cat1.ReadOnly = True
        '
        'Cat2
        '
        Me.Cat2.FillWeight = 20.0!
        Me.Cat2.HeaderText = "Cat2"
        Me.Cat2.MaxInputLength = 6
        Me.Cat2.Name = "Cat2"
        Me.Cat2.ReadOnly = True
        '
        'Description
        '
        Me.Description.FillWeight = 80.0!
        Me.Description.HeaderText = "Description"
        Me.Description.MaxInputLength = 50
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        '
        'cost_ex
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.cost_ex.DefaultCellStyle = DataGridViewCellStyle2
        Me.cost_ex.FillWeight = 26.6252!
        Me.cost_ex.HeaderText = "Cost-Ex"
        Me.cost_ex.MaxInputLength = 12
        Me.cost_ex.Name = "cost_ex"
        '
        'tax_code
        '
        Me.tax_code.FillWeight = 16.84765!
        Me.tax_code.HeaderText = "Tax-code"
        Me.tax_code.MaxInputLength = 6
        Me.tax_code.Name = "tax_code"
        Me.tax_code.ReadOnly = True
        '
        'cost_inc
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.cost_inc.DefaultCellStyle = DataGridViewCellStyle3
        Me.cost_inc.FillWeight = 26.6252!
        Me.cost_inc.HeaderText = "Cost-inc"
        Me.cost_inc.MaxInputLength = 12
        Me.cost_inc.Name = "cost_inc"
        Me.cost_inc.ReadOnly = True
        '
        'sell_ex
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.sell_ex.DefaultCellStyle = DataGridViewCellStyle4
        Me.sell_ex.FillWeight = 36.0!
        Me.sell_ex.HeaderText = "Sell-ex"
        Me.sell_ex.MaxInputLength = 12
        Me.sell_ex.Name = "sell_ex"
        '
        'SalesTaxCode
        '
        Me.SalesTaxCode.FillWeight = 20.0!
        Me.SalesTaxCode.HeaderText = "STax"
        Me.SalesTaxCode.Name = "SalesTaxCode"
        Me.SalesTaxCode.ReadOnly = True
        '
        'sell_inc
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.sell_inc.DefaultCellStyle = DataGridViewCellStyle5
        Me.sell_inc.FillWeight = 36.0!
        Me.sell_inc.HeaderText = "Sell_inc"
        Me.sell_inc.MaxInputLength = 12
        Me.sell_inc.Name = "sell_inc"
        '
        'qty
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.qty.DefaultCellStyle = DataGridViewCellStyle6
        Me.qty.FillWeight = 13.47812!
        Me.qty.HeaderText = "Qty"
        Me.qty.MaxInputLength = 6
        Me.qty.Name = "qty"
        '
        'extension_inc
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.extension_inc.DefaultCellStyle = DataGridViewCellStyle7
        Me.extension_inc.FillWeight = 33.28149!
        Me.extension_inc.HeaderText = "Extension_ex"
        Me.extension_inc.MaxInputLength = 12
        Me.extension_inc.Name = "extension_inc"
        Me.extension_inc.ReadOnly = True
        '
        'serialNos
        '
        Me.serialNos.FillWeight = 24.26062!
        Me.serialNos.HeaderText = "Serials"
        Me.serialNos.Name = "serialNos"
        Me.serialNos.ReadOnly = True
        '
        'serialNoList
        '
        Me.serialNoList.HeaderText = "serialNoList"
        Me.serialNoList.Name = "serialNoList"
        Me.serialNoList.ReadOnly = True
        Me.serialNoList.Visible = False
        '
        'stock_id
        '
        Me.stock_id.HeaderText = "stock_id"
        Me.stock_id.Name = "stock_id"
        Me.stock_id.ReadOnly = True
        Me.stock_id.Visible = False
        '
        'track_serial
        '
        Me.track_serial.HeaderText = "track_serial"
        Me.track_serial.Name = "track_serial"
        Me.track_serial.ReadOnly = True
        Me.track_serial.Visible = False
        '
        'cost_tax
        '
        Me.cost_tax.HeaderText = "cost_tax"
        Me.cost_tax.Name = "cost_tax"
        Me.cost_tax.ReadOnly = True
        Me.cost_tax.Visible = False
        '
        'cost_tax_extended
        '
        Me.cost_tax_extended.HeaderText = "cost_tax_extended"
        Me.cost_tax_extended.Name = "cost_tax_extended"
        Me.cost_tax_extended.ReadOnly = True
        Me.cost_tax_extended.Visible = False
        '
        'po_line_id
        '
        Me.po_line_id.HeaderText = "po_line_id"
        Me.po_line_id.Name = "po_line_id"
        Me.po_line_id.ReadOnly = True
        Me.po_line_id.Visible = False
        '
        'original_cost_ex
        '
        Me.original_cost_ex.HeaderText = "original_cost_ex"
        Me.original_cost_ex.Name = "original_cost_ex"
        Me.original_cost_ex.ReadOnly = True
        Me.original_cost_ex.Visible = False
        '
        'original_cost_inc
        '
        Me.original_cost_inc.HeaderText = "original_cost_inc"
        Me.original_cost_inc.Name = "original_cost_inc"
        Me.original_cost_inc.ReadOnly = True
        Me.original_cost_inc.Visible = False
        '
        'new_suppliercode
        '
        Me.new_suppliercode.HeaderText = "new_suppliercode"
        Me.new_suppliercode.Name = "new_suppliercode"
        Me.new_suppliercode.ReadOnly = True
        Me.new_suppliercode.Visible = False
        '
        'panelGoodsFooter
        '
        Me.panelGoodsFooter.BackColor = System.Drawing.Color.Linen
        Me.panelGoodsFooter.CausesValidation = False
        Me.panelGoodsFooter.Controls.Add(Me.Label12)
        Me.panelGoodsFooter.Controls.Add(Me.Label9)
        Me.panelGoodsFooter.Controls.Add(Me.labHelp2)
        Me.panelGoodsFooter.Controls.Add(Me.labHelp)
        Me.panelGoodsFooter.Controls.Add(Me.txtGoodsDeliveryAddress)
        Me.panelGoodsFooter.Controls.Add(Me.Label7)
        Me.panelGoodsFooter.Controls.Add(Me.btnGoodsCancel)
        Me.panelGoodsFooter.Controls.Add(Me.btnGoodsCommit)
        Me.panelGoodsFooter.Controls.Add(Me.Label24)
        Me.panelGoodsFooter.Controls.Add(Me.txtGoodsComments)
        Me.panelGoodsFooter.Controls.Add(Me.panelGoodsTotals)
        Me.panelGoodsFooter.Controls.Add(Me.Label30)
        Me.panelGoodsFooter.Controls.Add(Me.picGoodsItem)
        Me.panelGoodsFooter.Location = New System.Drawing.Point(2, 414)
        Me.panelGoodsFooter.Name = "panelGoodsFooter"
        Me.panelGoodsFooter.Size = New System.Drawing.Size(1005, 203)
        Me.panelGoodsFooter.TabIndex = 4
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(401, 32)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(77, 13)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "Selected Item-"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(202, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(123, 16)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "Delivery Address"
        '
        'labHelp2
        '
        Me.labHelp2.BackColor = System.Drawing.Color.Snow
        Me.labHelp2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHelp2.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.labHelp2.Location = New System.Drawing.Point(237, 139)
        Me.labHelp2.Name = "labHelp2"
        Me.labHelp2.Size = New System.Drawing.Size(199, 20)
        Me.labHelp2.TabIndex = 37
        Me.labHelp2.Text = "labHelp2"
        '
        'labHelp
        '
        Me.labHelp.BackColor = System.Drawing.Color.Snow
        Me.labHelp.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHelp.Location = New System.Drawing.Point(14, 132)
        Me.labHelp.Name = "labHelp"
        Me.labHelp.Size = New System.Drawing.Size(205, 20)
        Me.labHelp.TabIndex = 38
        Me.labHelp.Text = "labHelp"
        '
        'txtGoodsDeliveryAddress
        '
        Me.txtGoodsDeliveryAddress.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtGoodsDeliveryAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsDeliveryAddress.CausesValidation = False
        Me.txtGoodsDeliveryAddress.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsDeliveryAddress.Location = New System.Drawing.Point(205, 63)
        Me.txtGoodsDeliveryAddress.MaxLength = 500
        Me.txtGoodsDeliveryAddress.Multiline = True
        Me.txtGoodsDeliveryAddress.Name = "txtGoodsDeliveryAddress"
        Me.txtGoodsDeliveryAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGoodsDeliveryAddress.Size = New System.Drawing.Size(178, 60)
        Me.txtGoodsDeliveryAddress.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(14, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.Label7.Size = New System.Drawing.Size(386, 34)
        Me.Label7.TabIndex = 36
        Me.Label7.Text = "Note-  To delete an item, FULLY select the item row and press the DELETE key.."
        '
        'btnGoodsCancel
        '
        Me.btnGoodsCancel.BackColor = System.Drawing.Color.Thistle
        Me.btnGoodsCancel.CausesValidation = False
        Me.btnGoodsCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoodsCancel.Location = New System.Drawing.Point(918, 99)
        Me.btnGoodsCancel.Name = "btnGoodsCancel"
        Me.btnGoodsCancel.Size = New System.Drawing.Size(73, 33)
        Me.btnGoodsCancel.TabIndex = 10
        Me.btnGoodsCancel.Text = "Cancel"
        Me.btnGoodsCancel.UseVisualStyleBackColor = False
        '
        'btnGoodsCommit
        '
        Me.btnGoodsCommit.BackColor = System.Drawing.Color.Lavender
        Me.btnGoodsCommit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoodsCommit.Location = New System.Drawing.Point(920, 150)
        Me.btnGoodsCommit.Name = "btnGoodsCommit"
        Me.btnGoodsCommit.Size = New System.Drawing.Size(73, 33)
        Me.btnGoodsCommit.TabIndex = 11
        Me.btnGoodsCommit.Text = "Commit"
        Me.btnGoodsCommit.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(14, 48)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(70, 16)
        Me.Label24.TabIndex = 35
        Me.Label24.Text = "Comments"
        '
        'txtGoodsComments
        '
        Me.txtGoodsComments.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtGoodsComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsComments.CausesValidation = False
        Me.txtGoodsComments.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsComments.Location = New System.Drawing.Point(17, 63)
        Me.txtGoodsComments.Multiline = True
        Me.txtGoodsComments.Name = "txtGoodsComments"
        Me.txtGoodsComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGoodsComments.Size = New System.Drawing.Size(175, 60)
        Me.txtGoodsComments.TabIndex = 12
        '
        'panelGoodsTotals
        '
        Me.panelGoodsTotals.Controls.Add(Me.chkCloseForBackorders)
        Me.panelGoodsTotals.Controls.Add(Me.labDueDate)
        Me.panelGoodsTotals.Controls.Add(Me.DTPickerDueDate)
        Me.panelGoodsTotals.Controls.Add(Me.txtGoodsTotal)
        Me.panelGoodsTotals.Controls.Add(Me.Label2)
        Me.panelGoodsTotals.Controls.Add(Me.labDiscountTax)
        Me.panelGoodsTotals.Controls.Add(Me.txtGoodsDiscountAnalysis)
        Me.panelGoodsTotals.Controls.Add(Me.labDiscount)
        Me.panelGoodsTotals.Controls.Add(Me.txtGoodsDiscount)
        Me.panelGoodsTotals.Controls.Add(Me.chkFreightIsIncl)
        Me.panelGoodsTotals.Controls.Add(Me.txtFreightTax)
        Me.panelGoodsTotals.Controls.Add(Me.labFreightTax)
        Me.panelGoodsTotals.Controls.Add(Me.labFreight)
        Me.panelGoodsTotals.Controls.Add(Me.txtGoodsFreight)
        Me.panelGoodsTotals.Controls.Add(Me.txtGoodsTotalTax)
        Me.panelGoodsTotals.Controls.Add(Me.Label10)
        Me.panelGoodsTotals.Controls.Add(Me.txtGoodsSubTotal)
        Me.panelGoodsTotals.Controls.Add(Me.Label25)
        Me.panelGoodsTotals.Controls.Add(Me.labExpected)
        Me.panelGoodsTotals.Controls.Add(Me.txtTotalExpected)
        Me.panelGoodsTotals.Controls.Add(Me.txtGoodsNettTax)
        Me.panelGoodsTotals.Controls.Add(Me.Label27)
        Me.panelGoodsTotals.Location = New System.Drawing.Point(513, 3)
        Me.panelGoodsTotals.Name = "panelGoodsTotals"
        Me.panelGoodsTotals.Size = New System.Drawing.Size(391, 197)
        Me.panelGoodsTotals.TabIndex = 0
        '
        'chkCloseForBackorders
        '
        Me.chkCloseForBackorders.Location = New System.Drawing.Point(18, 76)
        Me.chkCloseForBackorders.Name = "chkCloseForBackorders"
        Me.chkCloseForBackorders.Size = New System.Drawing.Size(110, 37)
        Me.chkCloseForBackorders.TabIndex = 1
        Me.chkCloseForBackorders.Text = "Mark as Closed For Back-orders"
        Me.ToolTip1.SetToolTip(Me.chkCloseForBackorders, "No Back Orders for this PO..")
        Me.chkCloseForBackorders.UseVisualStyleBackColor = True
        '
        'labDueDate
        '
        Me.labDueDate.BackColor = System.Drawing.Color.Gainsboro
        Me.labDueDate.Location = New System.Drawing.Point(18, 129)
        Me.labDueDate.Name = "labDueDate"
        Me.labDueDate.Size = New System.Drawing.Size(110, 28)
        Me.labDueDate.TabIndex = 122
        Me.labDueDate.Text = "Date Delivery Due: "
        Me.labDueDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'DTPickerDueDate
        '
        Me.DTPickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPickerDueDate.Location = New System.Drawing.Point(21, 160)
        Me.DTPickerDueDate.Name = "DTPickerDueDate"
        Me.DTPickerDueDate.Size = New System.Drawing.Size(107, 20)
        Me.DTPickerDueDate.TabIndex = 2
        '
        'txtGoodsTotal
        '
        Me.txtGoodsTotal.BackColor = System.Drawing.Color.LightGray
        Me.txtGoodsTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsTotal.Location = New System.Drawing.Point(255, 171)
        Me.txtGoodsTotal.Name = "txtGoodsTotal"
        Me.txtGoodsTotal.ReadOnly = True
        Me.txtGoodsTotal.Size = New System.Drawing.Size(116, 14)
        Me.txtGoodsTotal.TabIndex = 8
        Me.txtGoodsTotal.TabStop = False
        Me.txtGoodsTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(188, 171)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 14)
        Me.Label2.TabIndex = 120
        Me.Label2.Text = "Inv. Total:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'labDiscountTax
        '
        Me.labDiscountTax.ForeColor = System.Drawing.Color.Gray
        Me.labDiscountTax.Location = New System.Drawing.Point(172, 129)
        Me.labDiscountTax.Name = "labDiscountTax"
        Me.labDiscountTax.Size = New System.Drawing.Size(79, 14)
        Me.labDiscountTax.TabIndex = 118
        Me.labDiscountTax.Text = "(-Items/Tax)"
        Me.labDiscountTax.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGoodsDiscountAnalysis
        '
        Me.txtGoodsDiscountAnalysis.BackColor = System.Drawing.Color.LightGray
        Me.txtGoodsDiscountAnalysis.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsDiscountAnalysis.Font = New System.Drawing.Font("Verdana", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsDiscountAnalysis.ForeColor = System.Drawing.Color.Gray
        Me.txtGoodsDiscountAnalysis.Location = New System.Drawing.Point(254, 128)
        Me.txtGoodsDiscountAnalysis.Name = "txtGoodsDiscountAnalysis"
        Me.txtGoodsDiscountAnalysis.ReadOnly = True
        Me.txtGoodsDiscountAnalysis.Size = New System.Drawing.Size(117, 12)
        Me.txtGoodsDiscountAnalysis.TabIndex = 6
        Me.txtGoodsDiscountAnalysis.TabStop = False
        Me.txtGoodsDiscountAnalysis.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labDiscount
        '
        Me.labDiscount.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDiscount.Location = New System.Drawing.Point(190, 101)
        Me.labDiscount.Name = "labDiscount"
        Me.labDiscount.Size = New System.Drawing.Size(58, 12)
        Me.labDiscount.TabIndex = 115
        Me.labDiscount.Text = "Discount:"
        Me.labDiscount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGoodsDiscount
        '
        Me.txtGoodsDiscount.BackColor = System.Drawing.SystemColors.Window
        Me.txtGoodsDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsDiscount.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsDiscount.Location = New System.Drawing.Point(254, 98)
        Me.txtGoodsDiscount.MaxLength = 11
        Me.txtGoodsDiscount.Name = "txtGoodsDiscount"
        Me.txtGoodsDiscount.Size = New System.Drawing.Size(117, 22)
        Me.txtGoodsDiscount.TabIndex = 5
        Me.txtGoodsDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkFreightIsIncl
        '
        Me.chkFreightIsIncl.BackColor = System.Drawing.Color.Bisque
        Me.chkFreightIsIncl.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFreightIsIncl.Location = New System.Drawing.Point(241, 52)
        Me.chkFreightIsIncl.Name = "chkFreightIsIncl"
        Me.chkFreightIsIncl.Size = New System.Drawing.Size(44, 28)
        Me.chkFreightIsIncl.TabIndex = 3
        Me.chkFreightIsIncl.Text = "Incl. Tax:"
        Me.chkFreightIsIncl.UseVisualStyleBackColor = False
        '
        'txtFreightTax
        '
        Me.txtFreightTax.BackColor = System.Drawing.Color.LightGray
        Me.txtFreightTax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFreightTax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFreightTax.ForeColor = System.Drawing.Color.Gray
        Me.txtFreightTax.Location = New System.Drawing.Point(255, 79)
        Me.txtFreightTax.Name = "txtFreightTax"
        Me.txtFreightTax.ReadOnly = True
        Me.txtFreightTax.Size = New System.Drawing.Size(116, 13)
        Me.txtFreightTax.TabIndex = 4
        Me.txtFreightTax.TabStop = False
        Me.txtFreightTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labFreightTax
        '
        Me.labFreightTax.ForeColor = System.Drawing.Color.Gray
        Me.labFreightTax.Location = New System.Drawing.Point(167, 79)
        Me.labFreightTax.Name = "labFreightTax"
        Me.labFreightTax.Size = New System.Drawing.Size(82, 14)
        Me.labFreightTax.TabIndex = 113
        Me.labFreightTax.Text = "Freight Tax:"
        Me.labFreightTax.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'labFreight
        '
        Me.labFreight.BackColor = System.Drawing.Color.Transparent
        Me.labFreight.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labFreight.Location = New System.Drawing.Point(181, 56)
        Me.labFreight.Name = "labFreight"
        Me.labFreight.Size = New System.Drawing.Size(56, 14)
        Me.labFreight.TabIndex = 111
        Me.labFreight.Text = "Freight:"
        Me.labFreight.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGoodsFreight
        '
        Me.txtGoodsFreight.BackColor = System.Drawing.SystemColors.Window
        Me.txtGoodsFreight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoodsFreight.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsFreight.Location = New System.Drawing.Point(290, 56)
        Me.txtGoodsFreight.MaxLength = 11
        Me.txtGoodsFreight.Name = "txtGoodsFreight"
        Me.txtGoodsFreight.Size = New System.Drawing.Size(79, 21)
        Me.txtGoodsFreight.TabIndex = 4
        Me.txtGoodsFreight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGoodsTotalTax
        '
        Me.txtGoodsTotalTax.BackColor = System.Drawing.Color.LightGray
        Me.txtGoodsTotalTax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsTotalTax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsTotalTax.ForeColor = System.Drawing.Color.Gray
        Me.txtGoodsTotalTax.Location = New System.Drawing.Point(257, 16)
        Me.txtGoodsTotalTax.Name = "txtGoodsTotalTax"
        Me.txtGoodsTotalTax.ReadOnly = True
        Me.txtGoodsTotalTax.Size = New System.Drawing.Size(114, 14)
        Me.txtGoodsTotalTax.TabIndex = 0
        Me.txtGoodsTotalTax.TabStop = False
        Me.txtGoodsTotalTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.Gray
        Me.Label10.Location = New System.Drawing.Point(169, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 14)
        Me.Label10.TabIndex = 109
        Me.Label10.Text = "(Includes Tax)"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGoodsSubTotal
        '
        Me.txtGoodsSubTotal.BackColor = System.Drawing.Color.LightGray
        Me.txtGoodsSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsSubTotal.Location = New System.Drawing.Point(257, 36)
        Me.txtGoodsSubTotal.Name = "txtGoodsSubTotal"
        Me.txtGoodsSubTotal.ReadOnly = True
        Me.txtGoodsSubTotal.Size = New System.Drawing.Size(114, 14)
        Me.txtGoodsSubTotal.TabIndex = 1
        Me.txtGoodsSubTotal.TabStop = False
        Me.txtGoodsSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(173, 36)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(82, 14)
        Me.Label25.TabIndex = 14
        Me.Label25.Text = "Sub-total (incl):"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'labExpected
        '
        Me.labExpected.BackColor = System.Drawing.Color.Gainsboro
        Me.labExpected.Location = New System.Drawing.Point(13, 9)
        Me.labExpected.Name = "labExpected"
        Me.labExpected.Padding = New System.Windows.Forms.Padding(3, 1, 0, 0)
        Me.labExpected.Size = New System.Drawing.Size(130, 41)
        Me.labExpected.TabIndex = 16
        Me.labExpected.Text = "-- Total Expected (Supplier Invoice Total):"
        Me.labExpected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalExpected
        '
        Me.txtTotalExpected.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalExpected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalExpected.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalExpected.Location = New System.Drawing.Point(16, 51)
        Me.txtTotalExpected.MaxLength = 11
        Me.txtTotalExpected.Name = "txtTotalExpected"
        Me.txtTotalExpected.Size = New System.Drawing.Size(127, 20)
        Me.txtTotalExpected.TabIndex = 0
        Me.txtTotalExpected.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGoodsNettTax
        '
        Me.txtGoodsNettTax.BackColor = System.Drawing.Color.LightGray
        Me.txtGoodsNettTax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsNettTax.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsNettTax.Location = New System.Drawing.Point(254, 149)
        Me.txtGoodsNettTax.Name = "txtGoodsNettTax"
        Me.txtGoodsNettTax.ReadOnly = True
        Me.txtGoodsNettTax.Size = New System.Drawing.Size(117, 14)
        Me.txtGoodsNettTax.TabIndex = 7
        Me.txtGoodsNettTax.TabStop = False
        Me.txtGoodsNettTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(191, 149)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(58, 14)
        Me.Label27.TabIndex = 20
        Me.Label27.Text = "Total Tax:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.DarkGray
        Me.Label30.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label30.ForeColor = System.Drawing.Color.LightGray
        Me.Label30.Location = New System.Drawing.Point(499, 13)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(7, 119)
        Me.Label30.TabIndex = 22
        '
        'picGoodsItem
        '
        Me.picGoodsItem.BackColor = System.Drawing.Color.WhiteSmoke
        Me.picGoodsItem.Location = New System.Drawing.Point(404, 48)
        Me.picGoodsItem.Name = "picGoodsItem"
        Me.picGoodsItem.Size = New System.Drawing.Size(80, 77)
        Me.picGoodsItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picGoodsItem.TabIndex = 27
        Me.picGoodsItem.TabStop = False
        '
        'panelGoodsBanner
        '
        Me.panelGoodsBanner.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.panelGoodsBanner.CausesValidation = False
        Me.panelGoodsBanner.Controls.Add(Me.labHdrPO)
        Me.panelGoodsBanner.Controls.Add(Me.labHdrGR)
        Me.panelGoodsBanner.Location = New System.Drawing.Point(2, 3)
        Me.panelGoodsBanner.Name = "panelGoodsBanner"
        Me.panelGoodsBanner.Size = New System.Drawing.Size(84, 164)
        Me.panelGoodsBanner.TabIndex = 200
        '
        'labHdrPO
        '
        Me.labHdrPO.BackColor = System.Drawing.Color.Transparent
        Me.labHdrPO.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdrPO.Location = New System.Drawing.Point(12, 22)
        Me.labHdrPO.Name = "labHdrPO"
        Me.labHdrPO.Size = New System.Drawing.Size(63, 28)
        Me.labHdrPO.TabIndex = 7
        Me.labHdrPO.Text = "Purchase Order"
        '
        'labHdrGR
        '
        Me.labHdrGR.BackColor = System.Drawing.Color.Transparent
        Me.labHdrGR.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdrGR.Location = New System.Drawing.Point(13, 61)
        Me.labHdrGR.Name = "labHdrGR"
        Me.labHdrGR.Size = New System.Drawing.Size(62, 38)
        Me.labHdrGR.TabIndex = 6
        Me.labHdrGR.Text = "Goods Received"
        '
        'panelGoodsHdr
        '
        Me.panelGoodsHdr.BackColor = System.Drawing.Color.Snow
        Me.panelGoodsHdr.CausesValidation = False
        Me.panelGoodsHdr.Controls.Add(Me.chkLoadPO)
        Me.panelGoodsHdr.Controls.Add(Me.panelIncludes)
        Me.panelGoodsHdr.Controls.Add(Me.chkPriceIncludesTax)
        Me.panelGoodsHdr.Controls.Add(Me.btnNewBlankPO)
        Me.panelGoodsHdr.Controls.Add(Me.Label8)
        Me.panelGoodsHdr.Controls.Add(Me.btnNewStandardOrder)
        Me.panelGoodsHdr.Controls.Add(Me.btnGoodsCancel2)
        Me.panelGoodsHdr.Controls.Add(Me.txtOrderNoSuffix)
        Me.panelGoodsHdr.Controls.Add(Me.Label3)
        Me.panelGoodsHdr.Controls.Add(Me.btnViewPO)
        Me.panelGoodsHdr.Controls.Add(Me.labPO_id)
        Me.panelGoodsHdr.Controls.Add(Me.btnLoadPO)
        Me.panelGoodsHdr.Controls.Add(Me.LabSaleCustSrch)
        Me.panelGoodsHdr.Controls.Add(Me.txtSupplierName)
        Me.panelGoodsHdr.Controls.Add(Me.txtSupplierBarcode)
        Me.panelGoodsHdr.Controls.Add(Me.labHdrFormType)
        Me.panelGoodsHdr.Controls.Add(Me.Label21)
        Me.panelGoodsHdr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelGoodsHdr.Location = New System.Drawing.Point(88, 3)
        Me.panelGoodsHdr.Name = "panelGoodsHdr"
        Me.panelGoodsHdr.Size = New System.Drawing.Size(570, 163)
        Me.panelGoodsHdr.TabIndex = 0
        '
        'chkLoadPO
        '
        Me.chkLoadPO.BackColor = System.Drawing.Color.Lavender
        Me.chkLoadPO.Location = New System.Drawing.Point(373, 22)
        Me.chkLoadPO.Name = "chkLoadPO"
        Me.chkLoadPO.Size = New System.Drawing.Size(68, 37)
        Me.chkLoadPO.TabIndex = 4
        Me.chkLoadPO.Text = "Select, Load PO"
        Me.ToolTip1.SetToolTip(Me.chkLoadPO, "Select and Load Purchase Order for this Delivery")
        Me.chkLoadPO.UseVisualStyleBackColor = False
        '
        'panelIncludes
        '
        Me.panelIncludes.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelIncludes.Controls.Add(Me.Label11)
        Me.panelIncludes.Controls.Add(Me.chkIncludeClosedForBO)
        Me.panelIncludes.Controls.Add(Me.chkIncludeCompletedOrders)
        Me.panelIncludes.Location = New System.Drawing.Point(117, 100)
        Me.panelIncludes.Name = "panelIncludes"
        Me.panelIncludes.Size = New System.Drawing.Size(181, 60)
        Me.panelIncludes.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 6)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(168, 14)
        Me.Label11.TabIndex = 117
        Me.Label11.Text = "Selecting PO- Include even if:"
        '
        'chkIncludeClosedForBO
        '
        Me.chkIncludeClosedForBO.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkIncludeClosedForBO.Location = New System.Drawing.Point(92, 25)
        Me.chkIncludeClosedForBO.Name = "chkIncludeClosedForBO"
        Me.chkIncludeClosedForBO.Size = New System.Drawing.Size(80, 33)
        Me.chkIncludeClosedForBO.TabIndex = 116
        Me.chkIncludeClosedForBO.Text = "PO Closed For B/O"
        Me.chkIncludeClosedForBO.UseVisualStyleBackColor = False
        '
        'chkIncludeCompletedOrders
        '
        Me.chkIncludeCompletedOrders.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkIncludeCompletedOrders.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludeCompletedOrders.Location = New System.Drawing.Point(7, 23)
        Me.chkIncludeCompletedOrders.Name = "chkIncludeCompletedOrders"
        Me.chkIncludeCompletedOrders.Size = New System.Drawing.Size(78, 33)
        Me.chkIncludeCompletedOrders.TabIndex = 115
        Me.chkIncludeCompletedOrders.Text = "PO is Completed"
        Me.chkIncludeCompletedOrders.UseVisualStyleBackColor = False
        '
        'chkPriceIncludesTax
        '
        Me.chkPriceIncludesTax.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkPriceIncludesTax.Location = New System.Drawing.Point(373, 119)
        Me.chkPriceIncludesTax.Name = "chkPriceIncludesTax"
        Me.chkPriceIncludesTax.Size = New System.Drawing.Size(81, 35)
        Me.chkPriceIncludesTax.TabIndex = 6
        Me.chkPriceIncludesTax.Text = "Prices-  include Tax"
        Me.chkPriceIncludesTax.UseVisualStyleBackColor = False
        '
        'btnNewBlankPO
        '
        Me.btnNewBlankPO.BackColor = System.Drawing.Color.NavajoWhite
        Me.btnNewBlankPO.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.btnNewBlankPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewBlankPO.Location = New System.Drawing.Point(272, 26)
        Me.btnNewBlankPO.Name = "btnNewBlankPO"
        Me.btnNewBlankPO.Size = New System.Drawing.Size(90, 28)
        Me.btnNewBlankPO.TabIndex = 3
        Me.btnNewBlankPO.Text = "New Blank PO"
        Me.ToolTip1.SetToolTip(Me.btnNewBlankPO, "Start a new blank Purchase Order")
        Me.btnNewBlankPO.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(304, 108)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 21)
        Me.Label8.TabIndex = 113
        Me.Label8.Text = "PO ID."
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnNewStandardOrder
        '
        Me.btnNewStandardOrder.BackColor = System.Drawing.Color.NavajoWhite
        Me.btnNewStandardOrder.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.btnNewStandardOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewStandardOrder.Location = New System.Drawing.Point(173, 26)
        Me.btnNewStandardOrder.Name = "btnNewStandardOrder"
        Me.btnNewStandardOrder.Size = New System.Drawing.Size(88, 28)
        Me.btnNewStandardOrder.TabIndex = 2
        Me.btnNewStandardOrder.Text = "New Std Order"
        Me.ToolTip1.SetToolTip(Me.btnNewStandardOrder, "Load PO with standard stock  shop order (based on re-order levels)..")
        Me.btnNewStandardOrder.UseVisualStyleBackColor = False
        '
        'btnGoodsCancel2
        '
        Me.btnGoodsCancel2.BackColor = System.Drawing.Color.Thistle
        Me.btnGoodsCancel2.CausesValidation = False
        Me.btnGoodsCancel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGoodsCancel2.Location = New System.Drawing.Point(494, 112)
        Me.btnGoodsCancel2.Name = "btnGoodsCancel2"
        Me.btnGoodsCancel2.Size = New System.Drawing.Size(65, 34)
        Me.btnGoodsCancel2.TabIndex = 30
        Me.btnGoodsCancel2.TabStop = False
        Me.btnGoodsCancel2.Text = "Cancel "
        Me.ToolTip1.SetToolTip(Me.btnGoodsCancel2, "Cancel this transaction..")
        Me.btnGoodsCancel2.UseVisualStyleBackColor = False
        '
        'txtOrderNoSuffix
        '
        Me.txtOrderNoSuffix.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtOrderNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrderNoSuffix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrderNoSuffix.Location = New System.Drawing.Point(450, 30)
        Me.txtOrderNoSuffix.MaxLength = 15
        Me.txtOrderNoSuffix.Name = "txtOrderNoSuffix"
        Me.txtOrderNoSuffix.Size = New System.Drawing.Size(109, 21)
        Me.txtOrderNoSuffix.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(454, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 18)
        Me.Label3.TabIndex = 107
        Me.Label3.Text = "Our OrderNo Suffix:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnViewPO
        '
        Me.btnViewPO.BackColor = System.Drawing.Color.NavajoWhite
        Me.btnViewPO.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.btnViewPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewPO.Location = New System.Drawing.Point(15, 102)
        Me.btnViewPO.Name = "btnViewPO"
        Me.btnViewPO.Size = New System.Drawing.Size(96, 28)
        Me.btnViewPO.TabIndex = 4
        Me.btnViewPO.Text = "View PO"
        Me.ToolTip1.SetToolTip(Me.btnViewPO, "View/print an existing PO..")
        Me.btnViewPO.UseVisualStyleBackColor = False
        '
        'labPO_id
        '
        Me.labPO_id.BackColor = System.Drawing.Color.PeachPuff
        Me.labPO_id.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPO_id.Location = New System.Drawing.Point(304, 131)
        Me.labPO_id.Name = "labPO_id"
        Me.labPO_id.Padding = New System.Windows.Forms.Padding(3, 3, 2, 0)
        Me.labPO_id.Size = New System.Drawing.Size(41, 23)
        Me.labPO_id.TabIndex = 107
        Me.labPO_id.Text = "labPO_id"
        Me.labPO_id.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnLoadPO
        '
        Me.btnLoadPO.BackColor = System.Drawing.Color.Lavender
        Me.btnLoadPO.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray
        Me.btnLoadPO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadPO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadPO.Location = New System.Drawing.Point(373, 59)
        Me.btnLoadPO.Name = "btnLoadPO"
        Me.btnLoadPO.Size = New System.Drawing.Size(25, 37)
        Me.btnLoadPO.TabIndex = 5
        Me.btnLoadPO.Text = ">>"
        Me.ToolTip1.SetToolTip(Me.btnLoadPO, "Select and Load Purchase Order for this Delivery")
        Me.btnLoadPO.UseVisualStyleBackColor = False
        '
        'LabSaleCustSrch
        '
        Me.LabSaleCustSrch.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSaleCustSrch.Location = New System.Drawing.Point(11, 54)
        Me.LabSaleCustSrch.Name = "LabSaleCustSrch"
        Me.LabSaleCustSrch.Size = New System.Drawing.Size(66, 41)
        Me.LabSaleCustSrch.TabIndex = 105
        Me.LabSaleCustSrch.Text = "F2 to Search;" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "F5 New Supplier."
        '
        'txtSupplierName
        '
        Me.txtSupplierName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSupplierName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.Location = New System.Drawing.Point(79, 58)
        Me.txtSupplierName.Multiline = True
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.ReadOnly = True
        Me.txtSupplierName.Size = New System.Drawing.Size(284, 37)
        Me.txtSupplierName.TabIndex = 104
        Me.txtSupplierName.TabStop = False
        '
        'txtSupplierBarcode
        '
        Me.txtSupplierBarcode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSupplierBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierBarcode.Location = New System.Drawing.Point(80, 33)
        Me.txtSupplierBarcode.Name = "txtSupplierBarcode"
        Me.txtSupplierBarcode.Size = New System.Drawing.Size(77, 21)
        Me.txtSupplierBarcode.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtSupplierBarcode, "Supplier Barcode")
        '
        'labHdrFormType
        '
        Me.labHdrFormType.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdrFormType.Location = New System.Drawing.Point(11, 7)
        Me.labHdrFormType.Name = "labHdrFormType"
        Me.labHdrFormType.Size = New System.Drawing.Size(133, 21)
        Me.labHdrFormType.TabIndex = 28
        Me.labHdrFormType.Text = "Supplier Invoice"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(21, 36)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(56, 13)
        Me.Label21.TabIndex = 1
        Me.Label21.Text = "Supplier:"
        '
        'ucChildGoodsRecvd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CausesValidation = False
        Me.Controls.Add(Me.grpBoxGoods)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildGoodsRecvd"
        Me.Size = New System.Drawing.Size(1022, 633)
        Me.grpBoxGoods.ResumeLayout(False)
        Me.TabControlInvoice.ResumeLayout(False)
        Me.TabPageInvoice.ResumeLayout(False)
        Me.panelInvoiceNo.ResumeLayout(False)
        Me.panelInvoiceNo.PerformLayout()
        Me.TabPagePrint.ResumeLayout(False)
        Me.panelPrinting.ResumeLayout(False)
        Me.panelPrinting.PerformLayout()
        CType(Me.picEmailPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelStockLineEntry.ResumeLayout(False)
        Me.panelStockLineEntry.PerformLayout()
        CType(Me.clsDgvGoodsItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelGoodsFooter.ResumeLayout(False)
        Me.panelGoodsFooter.PerformLayout()
        Me.panelGoodsTotals.ResumeLayout(False)
        Me.panelGoodsTotals.PerformLayout()
        CType(Me.picGoodsItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelGoodsBanner.ResumeLayout(False)
        Me.panelGoodsHdr.ResumeLayout(False)
        Me.panelGoodsHdr.PerformLayout()
        Me.panelIncludes.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpBoxGoods As System.Windows.Forms.GroupBox
    Friend WithEvents panelGoodsFooter As System.Windows.Forms.Panel
    Friend WithEvents txtGoodsComments As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents panelGoodsBanner As System.Windows.Forms.Panel
    Friend WithEvents panelGoodsHdr As System.Windows.Forms.Panel
    Friend WithEvents labHdrFormType As System.Windows.Forms.Label
    Friend WithEvents picGoodsItem As System.Windows.Forms.PictureBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplierBarcode As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents labHelp As System.Windows.Forms.Label
    Friend WithEvents labHelp2 As System.Windows.Forms.Label
    Friend WithEvents btnGoodsCancel As System.Windows.Forms.Button
    Friend WithEvents btnGoodsCommit As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtOrderNoSuffix As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtPickerInvoiceDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents panelGoodsTotals As System.Windows.Forms.Panel
    Friend WithEvents labDiscountTax As System.Windows.Forms.Label
    Friend WithEvents txtGoodsDiscountAnalysis As System.Windows.Forms.TextBox
    Friend WithEvents labDiscount As System.Windows.Forms.Label
    Friend WithEvents txtGoodsDiscount As System.Windows.Forms.TextBox
    Friend WithEvents chkFreightIsIncl As System.Windows.Forms.CheckBox
    Friend WithEvents txtFreightTax As System.Windows.Forms.TextBox
    Friend WithEvents labFreightTax As System.Windows.Forms.Label
    Friend WithEvents labFreight As System.Windows.Forms.Label
    Friend WithEvents txtGoodsFreight As System.Windows.Forms.TextBox
    Friend WithEvents txtGoodsTotalTax As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtGoodsSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents labExpected As System.Windows.Forms.Label
    Friend WithEvents txtTotalExpected As System.Windows.Forms.TextBox
    Friend WithEvents txtGoodsNettTax As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtGoodsTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents panelInvoiceNo As System.Windows.Forms.Panel
    Friend WithEvents LabSaleCustSrch As System.Windows.Forms.Label
    Friend WithEvents labHdrGR As System.Windows.Forms.Label
    Friend WithEvents labHdrPO As System.Windows.Forms.Label
    Friend WithEvents btnLoadPO As System.Windows.Forms.Button
    Friend WithEvents btnViewPO As System.Windows.Forms.Button
    Friend WithEvents labPO_id As System.Windows.Forms.Label
    Friend WithEvents btnGoodsCancel2 As System.Windows.Forms.Button
    Friend WithEvents chkPriceIncludesTax As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnNewStandardOrder As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents labDueDate As System.Windows.Forms.Label
    Friend WithEvents DTPickerDueDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtGoodsDeliveryAddress As System.Windows.Forms.TextBox
    Friend WithEvents chkCloseForBackorders As System.Windows.Forms.CheckBox
    Friend WithEvents panelIncludes As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkIncludeClosedForBO As System.Windows.Forms.CheckBox
    Friend WithEvents chkIncludeCompletedOrders As System.Windows.Forms.CheckBox
    Friend WithEvents panelPrinting As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboInvoicePrinters As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancelPO As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnNewBlankPO As System.Windows.Forms.Button
    Friend WithEvents clsDgvGoodsItems As JMxPOS330.clsDgvGoods
    Friend WithEvents btnLookupGoods As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents picEmailPO As System.Windows.Forms.PictureBox
    Friend WithEvents labPdfPrinter As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents chkLoadPO As System.Windows.Forms.CheckBox
    Friend WithEvents btnGoodsContinue As System.Windows.Forms.Button
    Friend WithEvents panelStockLineEntry As System.Windows.Forms.Panel
    Friend WithEvents btnStockLineOk As System.Windows.Forms.Button
    Friend WithEvents txtStockItemDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtStockItemBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents labStockLinePrompt As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TabControlInvoice As System.Windows.Forms.TabControl
    Friend WithEvents TabPageInvoice As System.Windows.Forms.TabPage
    Friend WithEvents TabPagePrint As System.Windows.Forms.TabPage
    Friend WithEvents txtStockItemSupplierCode As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents supplierCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cat1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cat2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cost_ex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tax_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cost_inc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sell_ex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SalesTaxCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sell_inc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents extension_inc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents serialNos As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents serialNoList As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents stock_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents track_serial As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cost_tax As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cost_tax_extended As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents po_line_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents original_cost_ex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents original_cost_inc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents new_suppliercode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents labCreatingPDF As System.Windows.Forms.Label
End Class
