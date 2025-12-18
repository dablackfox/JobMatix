<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildPosReports
    Inherits System.Windows.Forms.UserControl

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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Full Preview")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Grid Analysis")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Sales Invoice Listing", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2})
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Product/Profit Sales")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Customer Sales")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Sales        ", New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode4, TreeNode5})
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Revenue Analysis ")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Till Analysis (Preview)")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Till Analysis (Grid)")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Payments ", New System.Windows.Forms.TreeNode() {TreeNode7, TreeNode8, TreeNode9})
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stock on Hand")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Goods Recvd Period")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stock Barcode List")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Stock        ", New System.Windows.Forms.TreeNode() {TreeNode11, TreeNode12, TreeNode13})
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelHdr = New System.Windows.Forms.Panel()
        Me.TabControlProduct = New System.Windows.Forms.TabControl()
        Me.TabPageSalesInvoices = New System.Windows.Forms.TabPage()
        Me.panelInvoiceOptions = New System.Windows.Forms.Panel()
        Me.cboStaff = New System.Windows.Forms.ComboBox()
        Me.chkSalesSummaryOnly = New System.Windows.Forms.CheckBox()
        Me.chkShowInvoiceLines = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.chkInvoicesDiscountedOnly = New System.Windows.Forms.CheckBox()
        Me.optInvoicesCash = New System.Windows.Forms.RadioButton()
        Me.optInvoicesOnAccount = New System.Windows.Forms.RadioButton()
        Me.optInvoicesAll = New System.Windows.Forms.RadioButton()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.TabPageProductSales = New System.Windows.Forms.TabPage()
        Me.grpBoxCategory = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cboSupplierSales = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboSelectCat2 = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.optProductsNonTaxable = New System.Windows.Forms.RadioButton()
        Me.optProductsTaxable = New System.Windows.Forms.RadioButton()
        Me.optProductsAll = New System.Windows.Forms.RadioButton()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cboSelectCat1 = New System.Windows.Forms.ComboBox()
        Me.TabPageCustomerSales = New System.Windows.Forms.TabPage()
        Me.grpBoxCustomer = New System.Windows.Forms.GroupBox()
        Me.chkAllCustomers = New System.Windows.Forms.CheckBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.btnCustomerLookup = New System.Windows.Forms.Button()
        Me.labCustomerName = New System.Windows.Forms.Label()
        Me.labStaffName = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tvwReports = New System.Windows.Forms.TreeView()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.grpBoxOptions = New System.Windows.Forms.GroupBox()
        Me.TabControlOptions = New System.Windows.Forms.TabControl()
        Me.TabPageSales = New System.Windows.Forms.TabPage()
        Me.panelPeriodOpts = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.optPeriodSelect = New System.Windows.Forms.RadioButton()
        Me.optPeriod12Months = New System.Windows.Forms.RadioButton()
        Me.optperiodThisMonth = New System.Windows.Forms.RadioButton()
        Me.optPeriodToday = New System.Windows.Forms.RadioButton()
        Me.panelPeriodFromTo = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DTPickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.DTPickerTo = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TabPageStock = New System.Windows.Forms.TabPage()
        Me.panelStockReport = New System.Windows.Forms.Panel()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboSupplierStock = New System.Windows.Forms.ComboBox()
        Me.chkNoReportIfPosStock = New System.Windows.Forms.CheckBox()
        Me.chkNoReportIfNegStock = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkNoReportIfZeroStock = New System.Windows.Forms.CheckBox()
        Me.optDescription = New System.Windows.Forms.RadioButton()
        Me.optCat1Cat2Description = New System.Windows.Forms.RadioButton()
        Me.optCat1Description = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboReportPrinters = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnPrintReport = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.labExplain = New System.Windows.Forms.Label()
        Me.labReportName = New System.Windows.Forms.Label()
        Me.dgvReport = New System.Windows.Forms.DataGridView()
        Me.labDLLversion = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelHdr.SuspendLayout()
        Me.TabControlProduct.SuspendLayout()
        Me.TabPageSalesInvoices.SuspendLayout()
        Me.panelInvoiceOptions.SuspendLayout()
        Me.TabPageProductSales.SuspendLayout()
        Me.grpBoxCategory.SuspendLayout()
        Me.TabPageCustomerSales.SuspendLayout()
        Me.grpBoxCustomer.SuspendLayout()
        Me.grpBoxOptions.SuspendLayout()
        Me.TabControlOptions.SuspendLayout()
        Me.TabPageSales.SuspendLayout()
        Me.panelPeriodOpts.SuspendLayout()
        Me.panelPeriodFromTo.SuspendLayout()
        Me.TabPageStock.SuspendLayout()
        Me.panelStockReport.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelHdr
        '
        Me.panelHdr.BackColor = System.Drawing.Color.White
        Me.panelHdr.Controls.Add(Me.TabControlProduct)
        Me.panelHdr.Controls.Add(Me.labStaffName)
        Me.panelHdr.Controls.Add(Me.Label13)
        Me.panelHdr.Controls.Add(Me.Label10)
        Me.panelHdr.Controls.Add(Me.Label7)
        Me.panelHdr.Controls.Add(Me.tvwReports)
        Me.panelHdr.Controls.Add(Me.btnExit)
        Me.panelHdr.Controls.Add(Me.grpBoxOptions)
        Me.panelHdr.Controls.Add(Me.Label4)
        Me.panelHdr.Controls.Add(Me.labExplain)
        Me.panelHdr.Controls.Add(Me.labReportName)
        Me.panelHdr.Location = New System.Drawing.Point(5, 3)
        Me.panelHdr.Name = "panelHdr"
        Me.panelHdr.Size = New System.Drawing.Size(1009, 271)
        Me.panelHdr.TabIndex = 0
        '
        'TabControlProduct
        '
        Me.TabControlProduct.Controls.Add(Me.TabPageSalesInvoices)
        Me.TabControlProduct.Controls.Add(Me.TabPageProductSales)
        Me.TabControlProduct.Controls.Add(Me.TabPageCustomerSales)
        Me.TabControlProduct.Location = New System.Drawing.Point(194, 122)
        Me.TabControlProduct.Name = "TabControlProduct"
        Me.TabControlProduct.SelectedIndex = 0
        Me.TabControlProduct.Size = New System.Drawing.Size(331, 149)
        Me.TabControlProduct.TabIndex = 60
        '
        'TabPageSalesInvoices
        '
        Me.TabPageSalesInvoices.Controls.Add(Me.panelInvoiceOptions)
        Me.TabPageSalesInvoices.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSalesInvoices.Name = "TabPageSalesInvoices"
        Me.TabPageSalesInvoices.Size = New System.Drawing.Size(323, 123)
        Me.TabPageSalesInvoices.TabIndex = 2
        Me.TabPageSalesInvoices.Text = "Sales Invoices"
        Me.TabPageSalesInvoices.UseVisualStyleBackColor = True
        '
        'panelInvoiceOptions
        '
        Me.panelInvoiceOptions.BackColor = System.Drawing.Color.Transparent
        Me.panelInvoiceOptions.Controls.Add(Me.cboStaff)
        Me.panelInvoiceOptions.Controls.Add(Me.chkSalesSummaryOnly)
        Me.panelInvoiceOptions.Controls.Add(Me.chkShowInvoiceLines)
        Me.panelInvoiceOptions.Controls.Add(Me.Label18)
        Me.panelInvoiceOptions.Controls.Add(Me.chkInvoicesDiscountedOnly)
        Me.panelInvoiceOptions.Controls.Add(Me.optInvoicesCash)
        Me.panelInvoiceOptions.Controls.Add(Me.optInvoicesOnAccount)
        Me.panelInvoiceOptions.Controls.Add(Me.optInvoicesAll)
        Me.panelInvoiceOptions.Controls.Add(Me.Label17)
        Me.panelInvoiceOptions.Location = New System.Drawing.Point(4, 4)
        Me.panelInvoiceOptions.Name = "panelInvoiceOptions"
        Me.panelInvoiceOptions.Size = New System.Drawing.Size(315, 108)
        Me.panelInvoiceOptions.TabIndex = 1
        '
        'cboStaff
        '
        Me.cboStaff.BackColor = System.Drawing.Color.Lavender
        Me.cboStaff.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStaff.FormattingEnabled = True
        Me.cboStaff.Location = New System.Drawing.Point(9, 80)
        Me.cboStaff.Name = "cboStaff"
        Me.cboStaff.Size = New System.Drawing.Size(119, 19)
        Me.cboStaff.Sorted = True
        Me.cboStaff.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cboStaff, "Select Sales Invoices by Staff..")
        '
        'chkSalesSummaryOnly
        '
        Me.chkSalesSummaryOnly.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.chkSalesSummaryOnly.Location = New System.Drawing.Point(191, 37)
        Me.chkSalesSummaryOnly.Name = "chkSalesSummaryOnly"
        Me.chkSalesSummaryOnly.Size = New System.Drawing.Size(117, 20)
        Me.chkSalesSummaryOnly.TabIndex = 5
        Me.chkSalesSummaryOnly.Text = "Summary Only"
        Me.ToolTip1.SetToolTip(Me.chkSalesSummaryOnly, "Show only the Final Totals..")
        Me.chkSalesSummaryOnly.UseVisualStyleBackColor = False
        '
        'chkShowInvoiceLines
        '
        Me.chkShowInvoiceLines.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.chkShowInvoiceLines.Location = New System.Drawing.Point(191, 13)
        Me.chkShowInvoiceLines.Name = "chkShowInvoiceLines"
        Me.chkShowInvoiceLines.Size = New System.Drawing.Size(117, 20)
        Me.chkShowInvoiceLines.TabIndex = 4
        Me.chkShowInvoiceLines.Text = "Show Invoice Lines"
        Me.ToolTip1.SetToolTip(Me.chkShowInvoiceLines, "Show Invoice product detail Lines..")
        Me.chkShowInvoiceLines.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Gainsboro
        Me.Label18.Location = New System.Drawing.Point(183, 16)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(2, 75)
        Me.Label18.TabIndex = 5
        '
        'chkInvoicesDiscountedOnly
        '
        Me.chkInvoicesDiscountedOnly.Location = New System.Drawing.Point(191, 68)
        Me.chkInvoicesDiscountedOnly.Name = "chkInvoicesDiscountedOnly"
        Me.chkInvoicesDiscountedOnly.Size = New System.Drawing.Size(115, 31)
        Me.chkInvoicesDiscountedOnly.TabIndex = 6
        Me.chkInvoicesDiscountedOnly.Text = "Select Discounted" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Sales Only"
        Me.chkInvoicesDiscountedOnly.UseVisualStyleBackColor = True
        '
        'optInvoicesCash
        '
        Me.optInvoicesCash.Location = New System.Drawing.Point(85, 38)
        Me.optInvoicesCash.Name = "optInvoicesCash"
        Me.optInvoicesCash.Size = New System.Drawing.Size(93, 30)
        Me.optInvoicesCash.TabIndex = 3
        Me.optInvoicesCash.Text = "Cash Sale Invoices Only"
        Me.optInvoicesCash.UseVisualStyleBackColor = True
        '
        'optInvoicesOnAccount
        '
        Me.optInvoicesOnAccount.Location = New System.Drawing.Point(85, 5)
        Me.optInvoicesOnAccount.Name = "optInvoicesOnAccount"
        Me.optInvoicesOnAccount.Size = New System.Drawing.Size(93, 30)
        Me.optInvoicesOnAccount.TabIndex = 2
        Me.optInvoicesOnAccount.Text = "On-Account Invoices Only"
        Me.optInvoicesOnAccount.UseVisualStyleBackColor = True
        '
        'optInvoicesAll
        '
        Me.optInvoicesAll.Checked = True
        Me.optInvoicesAll.Location = New System.Drawing.Point(12, 5)
        Me.optInvoicesAll.Name = "optInvoicesAll"
        Me.optInvoicesAll.Size = New System.Drawing.Size(67, 30)
        Me.optInvoicesAll.TabIndex = 1
        Me.optInvoicesAll.TabStop = True
        Me.optInvoicesAll.Text = "All Sales Invoices"
        Me.optInvoicesAll.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label17.Location = New System.Drawing.Point(8, 62)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(50, 16)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = " -Staff-"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPageProductSales
        '
        Me.TabPageProductSales.Controls.Add(Me.grpBoxCategory)
        Me.TabPageProductSales.Location = New System.Drawing.Point(4, 22)
        Me.TabPageProductSales.Name = "TabPageProductSales"
        Me.TabPageProductSales.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageProductSales.Size = New System.Drawing.Size(323, 123)
        Me.TabPageProductSales.TabIndex = 0
        Me.TabPageProductSales.Text = "Product/Profit Sales"
        Me.TabPageProductSales.UseVisualStyleBackColor = True
        '
        'grpBoxCategory
        '
        Me.grpBoxCategory.Controls.Add(Me.Label19)
        Me.grpBoxCategory.Controls.Add(Me.cboSupplierSales)
        Me.grpBoxCategory.Controls.Add(Me.Label16)
        Me.grpBoxCategory.Controls.Add(Me.cboSelectCat2)
        Me.grpBoxCategory.Controls.Add(Me.Label15)
        Me.grpBoxCategory.Controls.Add(Me.optProductsNonTaxable)
        Me.grpBoxCategory.Controls.Add(Me.optProductsTaxable)
        Me.grpBoxCategory.Controls.Add(Me.optProductsAll)
        Me.grpBoxCategory.Controls.Add(Me.Label14)
        Me.grpBoxCategory.Controls.Add(Me.cboSelectCat1)
        Me.grpBoxCategory.Location = New System.Drawing.Point(6, 4)
        Me.grpBoxCategory.Name = "grpBoxCategory"
        Me.grpBoxCategory.Size = New System.Drawing.Size(311, 113)
        Me.grpBoxCategory.TabIndex = 2
        Me.grpBoxCategory.TabStop = False
        Me.grpBoxCategory.Text = "Product Cat1"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(3, 91)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(58, 15)
        Me.Label19.TabIndex = 12
        Me.Label19.Text = "Suppler:"
        '
        'cboSupplierSales
        '
        Me.cboSupplierSales.BackColor = System.Drawing.Color.Honeydew
        Me.cboSupplierSales.FormattingEnabled = True
        Me.cboSupplierSales.Location = New System.Drawing.Point(61, 89)
        Me.cboSupplierSales.Name = "cboSupplierSales"
        Me.cboSupplierSales.Size = New System.Drawing.Size(227, 21)
        Me.cboSupplierSales.Sorted = True
        Me.cboSupplierSales.TabIndex = 5
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 59)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(39, 15)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "Cat2:"
        '
        'cboSelectCat2
        '
        Me.cboSelectCat2.BackColor = System.Drawing.Color.Lavender
        Me.cboSelectCat2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboSelectCat2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSelectCat2.FormattingEnabled = True
        Me.cboSelectCat2.Location = New System.Drawing.Point(52, 54)
        Me.cboSelectCat2.Name = "cboSelectCat2"
        Me.cboSelectCat2.Size = New System.Drawing.Size(82, 23)
        Me.cboSelectCat2.TabIndex = 1
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(251, 12)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(48, 32)
        Me.Label15.TabIndex = 7
        Me.Label15.Text = "To Include"
        '
        'optProductsNonTaxable
        '
        Me.optProductsNonTaxable.Location = New System.Drawing.Point(157, 57)
        Me.optProductsNonTaxable.Name = "optProductsNonTaxable"
        Me.optProductsNonTaxable.Size = New System.Drawing.Size(145, 23)
        Me.optProductsNonTaxable.TabIndex = 4
        Me.optProductsNonTaxable.TabStop = True
        Me.optProductsNonTaxable.Text = "Non-Taxable Products"
        Me.optProductsNonTaxable.UseVisualStyleBackColor = True
        '
        'optProductsTaxable
        '
        Me.optProductsTaxable.Location = New System.Drawing.Point(157, 37)
        Me.optProductsTaxable.Name = "optProductsTaxable"
        Me.optProductsTaxable.Size = New System.Drawing.Size(110, 19)
        Me.optProductsTaxable.TabIndex = 3
        Me.optProductsTaxable.TabStop = True
        Me.optProductsTaxable.Text = "Taxable Products"
        Me.optProductsTaxable.UseVisualStyleBackColor = True
        '
        'optProductsAll
        '
        Me.optProductsAll.Location = New System.Drawing.Point(157, 15)
        Me.optProductsAll.Name = "optProductsAll"
        Me.optProductsAll.Size = New System.Drawing.Size(83, 19)
        Me.optProductsAll.TabIndex = 2
        Me.optProductsAll.TabStop = True
        Me.optProductsAll.Text = "All Products"
        Me.optProductsAll.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(6, 27)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(39, 15)
        Me.Label14.TabIndex = 2
        Me.Label14.Text = "Cat1:"
        '
        'cboSelectCat1
        '
        Me.cboSelectCat1.BackColor = System.Drawing.Color.Lavender
        Me.cboSelectCat1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboSelectCat1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSelectCat1.FormattingEnabled = True
        Me.cboSelectCat1.Location = New System.Drawing.Point(52, 22)
        Me.cboSelectCat1.Name = "cboSelectCat1"
        Me.cboSelectCat1.Size = New System.Drawing.Size(82, 23)
        Me.cboSelectCat1.TabIndex = 0
        '
        'TabPageCustomerSales
        '
        Me.TabPageCustomerSales.Controls.Add(Me.grpBoxCustomer)
        Me.TabPageCustomerSales.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCustomerSales.Name = "TabPageCustomerSales"
        Me.TabPageCustomerSales.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCustomerSales.Size = New System.Drawing.Size(323, 123)
        Me.TabPageCustomerSales.TabIndex = 1
        Me.TabPageCustomerSales.Text = "Customer/Product Sales"
        Me.TabPageCustomerSales.UseVisualStyleBackColor = True
        '
        'grpBoxCustomer
        '
        Me.grpBoxCustomer.Controls.Add(Me.chkAllCustomers)
        Me.grpBoxCustomer.Controls.Add(Me.Label48)
        Me.grpBoxCustomer.Controls.Add(Me.btnCustomerLookup)
        Me.grpBoxCustomer.Controls.Add(Me.labCustomerName)
        Me.grpBoxCustomer.Location = New System.Drawing.Point(6, 6)
        Me.grpBoxCustomer.Name = "grpBoxCustomer"
        Me.grpBoxCustomer.Size = New System.Drawing.Size(282, 95)
        Me.grpBoxCustomer.TabIndex = 1
        Me.grpBoxCustomer.TabStop = False
        Me.grpBoxCustomer.Text = "Customer"
        '
        'chkAllCustomers
        '
        Me.chkAllCustomers.BackColor = System.Drawing.Color.Lavender
        Me.chkAllCustomers.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCustomers.Location = New System.Drawing.Point(131, 20)
        Me.chkAllCustomers.Name = "chkAllCustomers"
        Me.chkAllCustomers.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.chkAllCustomers.Size = New System.Drawing.Size(119, 20)
        Me.chkAllCustomers.TabIndex = 12
        Me.chkAllCustomers.Text = "All Customers"
        Me.chkAllCustomers.UseVisualStyleBackColor = False
        '
        'Label48
        '
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(14, 17)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(100, 31)
        Me.Label48.TabIndex = 14
        Me.Label48.Text = "Choose Customer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Optional)"
        '
        'btnCustomerLookup
        '
        Me.btnCustomerLookup.BackColor = System.Drawing.Color.Lavender
        Me.btnCustomerLookup.Font = New System.Drawing.Font("Courier New", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomerLookup.Location = New System.Drawing.Point(207, 57)
        Me.btnCustomerLookup.Name = "btnCustomerLookup"
        Me.btnCustomerLookup.Size = New System.Drawing.Size(42, 24)
        Me.btnCustomerLookup.TabIndex = 11
        Me.btnCustomerLookup.Text = ">>"
        Me.btnCustomerLookup.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.btnCustomerLookup, "Lookup Customers...")
        Me.btnCustomerLookup.UseVisualStyleBackColor = False
        '
        'labCustomerName
        '
        Me.labCustomerName.BackColor = System.Drawing.Color.Lavender
        Me.labCustomerName.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labCustomerName.Location = New System.Drawing.Point(4, 52)
        Me.labCustomerName.Name = "labCustomerName"
        Me.labCustomerName.Size = New System.Drawing.Size(187, 29)
        Me.labCustomerName.TabIndex = 13
        Me.labCustomerName.Text = "labCreditNotesCustName"
        '
        'labStaffName
        '
        Me.labStaffName.AutoSize = True
        Me.labStaffName.Location = New System.Drawing.Point(632, 10)
        Me.labStaffName.Name = "labStaffName"
        Me.labStaffName.Size = New System.Drawing.Size(72, 13)
        Me.labStaffName.TabIndex = 59
        Me.labStaffName.Text = "labStaffName"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(583, 10)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 16)
        Me.Label13.TabIndex = 58
        Me.Label13.Text = "Staff: "
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(11, 39)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(174, 21)
        Me.Label10.TabIndex = 57
        Me.Label10.Text = "Select Report"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label7.Location = New System.Drawing.Point(194, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(303, 22)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Choose Report, set options and press Show.."
        '
        'tvwReports
        '
        Me.tvwReports.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tvwReports.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwReports.FullRowSelect = True
        Me.tvwReports.HideSelection = False
        Me.tvwReports.Location = New System.Drawing.Point(2, 62)
        Me.tvwReports.Name = "tvwReports"
        TreeNode1.Name = "Invoices_preview"
        TreeNode1.Text = "Full Preview"
        TreeNode1.ToolTipText = "Shows Details of all Invoices for Period."
        TreeNode2.Name = "invoices_grid"
        TreeNode2.Text = "Grid Analysis"
        TreeNode2.ToolTipText = "Shows a Summary Line for all Invoices for Period."
        TreeNode3.Name = "invoice_listing"
        TreeNode3.Text = "Sales Invoice Listing"
        TreeNode3.ToolTipText = "Shows all Invoices for period."
        TreeNode4.Name = "product_sales"
        TreeNode4.Text = "Product/Profit Sales"
        TreeNode4.ToolTipText = "Product Sales with profit."
        TreeNode5.Name = "customer_sales"
        TreeNode5.Text = "Customer Sales"
        TreeNode5.ToolTipText = "Sales by Customer"
        TreeNode6.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        TreeNode6.Name = "sales"
        TreeNode6.Text = "Sales        "
        TreeNode7.Name = "revenue_analysis"
        TreeNode7.Text = "Revenue Analysis "
        TreeNode7.ToolTipText = "Revenue Analysis (includes Credit-Notes movements)"
        TreeNode8.Name = "till_analysis"
        TreeNode8.Text = "Till Analysis (Preview)"
        TreeNode8.ToolTipText = "Till movement Analysis (excludes Credit Notes movements)"
        TreeNode9.Name = "till_analysis_grid"
        TreeNode9.Text = "Till Analysis (Grid)"
        TreeNode9.ToolTipText = "Till movement Analysis (excludes Credit Notes movements)"
        TreeNode10.BackColor = System.Drawing.Color.LightBlue
        TreeNode10.Name = "payments"
        TreeNode10.Text = "Payments "
        TreeNode11.Name = "stock_on_hand"
        TreeNode11.Text = "Stock on Hand"
        TreeNode11.ToolTipText = "List of Current Stock."
        TreeNode12.Name = "goods_received_by_period"
        TreeNode12.Text = "Goods Recvd Period"
        TreeNode13.Name = "stock_barcode_list"
        TreeNode13.Text = "Stock Barcode List"
        TreeNode13.ToolTipText = "Print a selection os Stock barcodes for scanning. "
        TreeNode14.BackColor = System.Drawing.Color.PaleGoldenrod
        TreeNode14.Name = "stock"
        TreeNode14.Text = "Stock        "
        Me.tvwReports.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode6, TreeNode10, TreeNode14})
        Me.tvwReports.ShowNodeToolTips = True
        Me.tvwReports.Size = New System.Drawing.Size(189, 206)
        Me.tvwReports.TabIndex = 0
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Lavender
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Location = New System.Drawing.Point(929, 11)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(63, 22)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'grpBoxOptions
        '
        Me.grpBoxOptions.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.grpBoxOptions.Controls.Add(Me.TabControlOptions)
        Me.grpBoxOptions.Controls.Add(Me.Label11)
        Me.grpBoxOptions.Controls.Add(Me.cboReportPrinters)
        Me.grpBoxOptions.Controls.Add(Me.Label1)
        Me.grpBoxOptions.Controls.Add(Me.btnPrintReport)
        Me.grpBoxOptions.Controls.Add(Me.btnRefresh)
        Me.grpBoxOptions.Location = New System.Drawing.Point(531, 40)
        Me.grpBoxOptions.Name = "grpBoxOptions"
        Me.grpBoxOptions.Size = New System.Drawing.Size(466, 226)
        Me.grpBoxOptions.TabIndex = 4
        Me.grpBoxOptions.TabStop = False
        Me.grpBoxOptions.Text = "grpBoxOptions"
        '
        'TabControlOptions
        '
        Me.TabControlOptions.Controls.Add(Me.TabPageSales)
        Me.TabControlOptions.Controls.Add(Me.TabPageStock)
        Me.TabControlOptions.Location = New System.Drawing.Point(9, 14)
        Me.TabControlOptions.Name = "TabControlOptions"
        Me.TabControlOptions.SelectedIndex = 0
        Me.TabControlOptions.Size = New System.Drawing.Size(294, 214)
        Me.TabControlOptions.TabIndex = 57
        '
        'TabPageSales
        '
        Me.TabPageSales.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPageSales.Controls.Add(Me.panelPeriodOpts)
        Me.TabPageSales.Controls.Add(Me.panelPeriodFromTo)
        Me.TabPageSales.Location = New System.Drawing.Point(4, 22)
        Me.TabPageSales.Name = "TabPageSales"
        Me.TabPageSales.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSales.Size = New System.Drawing.Size(286, 188)
        Me.TabPageSales.TabIndex = 0
        Me.TabPageSales.Text = "Sales Period"
        '
        'panelPeriodOpts
        '
        Me.panelPeriodOpts.Controls.Add(Me.Label3)
        Me.panelPeriodOpts.Controls.Add(Me.Label9)
        Me.panelPeriodOpts.Controls.Add(Me.optPeriodSelect)
        Me.panelPeriodOpts.Controls.Add(Me.optPeriod12Months)
        Me.panelPeriodOpts.Controls.Add(Me.optperiodThisMonth)
        Me.panelPeriodOpts.Controls.Add(Me.optPeriodToday)
        Me.panelPeriodOpts.Location = New System.Drawing.Point(6, 3)
        Me.panelPeriodOpts.Name = "panelPeriodOpts"
        Me.panelPeriodOpts.Size = New System.Drawing.Size(271, 73)
        Me.panelPeriodOpts.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Gainsboro
        Me.Label3.Location = New System.Drawing.Point(12, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(247, 2)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Label3"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(10, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Sales Period"
        '
        'optPeriodSelect
        '
        Me.optPeriodSelect.Location = New System.Drawing.Point(210, 34)
        Me.optPeriodSelect.Name = "optPeriodSelect"
        Me.optPeriodSelect.Size = New System.Drawing.Size(56, 34)
        Me.optPeriodSelect.TabIndex = 3
        Me.optPeriodSelect.TabStop = True
        Me.optPeriodSelect.Text = "Select Period"
        Me.optPeriodSelect.UseVisualStyleBackColor = True
        '
        'optPeriod12Months
        '
        Me.optPeriod12Months.Location = New System.Drawing.Point(139, 34)
        Me.optPeriod12Months.Name = "optPeriod12Months"
        Me.optPeriod12Months.Size = New System.Drawing.Size(63, 34)
        Me.optPeriod12Months.TabIndex = 2
        Me.optPeriod12Months.TabStop = True
        Me.optPeriod12Months.Text = "Last 12 Months"
        Me.optPeriod12Months.UseVisualStyleBackColor = True
        '
        'optperiodThisMonth
        '
        Me.optperiodThisMonth.Location = New System.Drawing.Point(80, 34)
        Me.optperiodThisMonth.Name = "optperiodThisMonth"
        Me.optperiodThisMonth.Size = New System.Drawing.Size(56, 34)
        Me.optperiodThisMonth.TabIndex = 1
        Me.optperiodThisMonth.TabStop = True
        Me.optperiodThisMonth.Text = "This Month"
        Me.optperiodThisMonth.UseVisualStyleBackColor = True
        '
        'optPeriodToday
        '
        Me.optPeriodToday.Location = New System.Drawing.Point(15, 34)
        Me.optPeriodToday.Name = "optPeriodToday"
        Me.optPeriodToday.Size = New System.Drawing.Size(56, 34)
        Me.optPeriodToday.TabIndex = 0
        Me.optPeriodToday.TabStop = True
        Me.optPeriodToday.Text = "Today Only"
        Me.optPeriodToday.UseVisualStyleBackColor = True
        '
        'panelPeriodFromTo
        '
        Me.panelPeriodFromTo.Controls.Add(Me.Label12)
        Me.panelPeriodFromTo.Controls.Add(Me.DTPickerFrom)
        Me.panelPeriodFromTo.Controls.Add(Me.DTPickerTo)
        Me.panelPeriodFromTo.Controls.Add(Me.Label5)
        Me.panelPeriodFromTo.Controls.Add(Me.Label6)
        Me.panelPeriodFromTo.Location = New System.Drawing.Point(6, 85)
        Me.panelPeriodFromTo.Name = "panelPeriodFromTo"
        Me.panelPeriodFromTo.Size = New System.Drawing.Size(271, 95)
        Me.panelPeriodFromTo.TabIndex = 18
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Gainsboro
        Me.Label12.Location = New System.Drawing.Point(14, 70)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(247, 2)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Label12"
        '
        'DTPickerFrom
        '
        Me.DTPickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPickerFrom.Location = New System.Drawing.Point(61, 30)
        Me.DTPickerFrom.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.DTPickerFrom.Name = "DTPickerFrom"
        Me.DTPickerFrom.Size = New System.Drawing.Size(88, 21)
        Me.DTPickerFrom.TabIndex = 5
        '
        'DTPickerTo
        '
        Me.DTPickerTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPickerTo.Location = New System.Drawing.Point(177, 30)
        Me.DTPickerTo.Name = "DTPickerTo"
        Me.DTPickerTo.Size = New System.Drawing.Size(88, 21)
        Me.DTPickerTo.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(19, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 35)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Period From"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(155, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(19, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "To"
        '
        'TabPageStock
        '
        Me.TabPageStock.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPageStock.Controls.Add(Me.panelStockReport)
        Me.TabPageStock.Location = New System.Drawing.Point(4, 22)
        Me.TabPageStock.Name = "TabPageStock"
        Me.TabPageStock.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageStock.Size = New System.Drawing.Size(286, 188)
        Me.TabPageStock.TabIndex = 1
        Me.TabPageStock.Text = "What's-In-Stock Options"
        '
        'panelStockReport
        '
        Me.panelStockReport.Controls.Add(Me.Label20)
        Me.panelStockReport.Controls.Add(Me.cboSupplierStock)
        Me.panelStockReport.Controls.Add(Me.chkNoReportIfPosStock)
        Me.panelStockReport.Controls.Add(Me.chkNoReportIfNegStock)
        Me.panelStockReport.Controls.Add(Me.Label8)
        Me.panelStockReport.Controls.Add(Me.Label2)
        Me.panelStockReport.Controls.Add(Me.chkNoReportIfZeroStock)
        Me.panelStockReport.Controls.Add(Me.optDescription)
        Me.panelStockReport.Controls.Add(Me.optCat1Cat2Description)
        Me.panelStockReport.Controls.Add(Me.optCat1Description)
        Me.panelStockReport.Location = New System.Drawing.Point(6, 4)
        Me.panelStockReport.Name = "panelStockReport"
        Me.panelStockReport.Size = New System.Drawing.Size(274, 176)
        Me.panelStockReport.TabIndex = 22
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(19, 149)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(58, 15)
        Me.Label20.TabIndex = 25
        Me.Label20.Text = "Suppler:"
        '
        'cboSupplierStock
        '
        Me.cboSupplierStock.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.cboSupplierStock.FormattingEnabled = True
        Me.cboSupplierStock.Location = New System.Drawing.Point(82, 148)
        Me.cboSupplierStock.Name = "cboSupplierStock"
        Me.cboSupplierStock.Size = New System.Drawing.Size(174, 21)
        Me.cboSupplierStock.Sorted = True
        Me.cboSupplierStock.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cboSupplierStock, "Choose Supplier for the stock items to report." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "NB: Only useful if Stock items ar" &
        "e exclusive to the Supplier.")
        '
        'chkNoReportIfPosStock
        '
        Me.chkNoReportIfPosStock.BackColor = System.Drawing.Color.Lavender
        Me.chkNoReportIfPosStock.Location = New System.Drawing.Point(179, 24)
        Me.chkNoReportIfPosStock.Name = "chkNoReportIfPosStock"
        Me.chkNoReportIfPosStock.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.chkNoReportIfPosStock.Size = New System.Drawing.Size(77, 30)
        Me.chkNoReportIfPosStock.TabIndex = 2
        Me.chkNoReportIfPosStock.Text = "Positive Stock"
        Me.chkNoReportIfPosStock.UseVisualStyleBackColor = False
        '
        'chkNoReportIfNegStock
        '
        Me.chkNoReportIfNegStock.BackColor = System.Drawing.Color.Lavender
        Me.chkNoReportIfNegStock.Location = New System.Drawing.Point(95, 24)
        Me.chkNoReportIfNegStock.Name = "chkNoReportIfNegStock"
        Me.chkNoReportIfNegStock.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.chkNoReportIfNegStock.Size = New System.Drawing.Size(78, 30)
        Me.chkNoReportIfNegStock.TabIndex = 1
        Me.chkNoReportIfNegStock.Text = "Negative Stock"
        Me.chkNoReportIfNegStock.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(108, 18)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "-- Don't Report:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(19, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 34)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Sort Options"
        '
        'chkNoReportIfZeroStock
        '
        Me.chkNoReportIfZeroStock.BackColor = System.Drawing.Color.Lavender
        Me.chkNoReportIfZeroStock.Location = New System.Drawing.Point(10, 24)
        Me.chkNoReportIfZeroStock.Name = "chkNoReportIfZeroStock"
        Me.chkNoReportIfZeroStock.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.chkNoReportIfZeroStock.Size = New System.Drawing.Size(66, 30)
        Me.chkNoReportIfZeroStock.TabIndex = 0
        Me.chkNoReportIfZeroStock.Text = "Zero Stock"
        Me.chkNoReportIfZeroStock.UseVisualStyleBackColor = False
        '
        'optDescription
        '
        Me.optDescription.BackColor = System.Drawing.Color.Lavender
        Me.optDescription.Location = New System.Drawing.Point(80, 116)
        Me.optDescription.Name = "optDescription"
        Me.optDescription.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.optDescription.Size = New System.Drawing.Size(142, 23)
        Me.optDescription.TabIndex = 5
        Me.optDescription.TabStop = True
        Me.optDescription.Text = "Description"
        Me.optDescription.UseVisualStyleBackColor = False
        '
        'optCat1Cat2Description
        '
        Me.optCat1Cat2Description.BackColor = System.Drawing.Color.Lavender
        Me.optCat1Cat2Description.Location = New System.Drawing.Point(80, 88)
        Me.optCat1Cat2Description.Name = "optCat1Cat2Description"
        Me.optCat1Cat2Description.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.optCat1Cat2Description.Size = New System.Drawing.Size(142, 23)
        Me.optCat1Cat2Description.TabIndex = 4
        Me.optCat1Cat2Description.TabStop = True
        Me.optCat1Cat2Description.Text = "Cat1/Cat2/Description"
        Me.optCat1Cat2Description.UseVisualStyleBackColor = False
        '
        'optCat1Description
        '
        Me.optCat1Description.BackColor = System.Drawing.Color.Lavender
        Me.optCat1Description.Location = New System.Drawing.Point(80, 60)
        Me.optCat1Description.Name = "optCat1Description"
        Me.optCat1Description.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.optCat1Description.Size = New System.Drawing.Size(142, 23)
        Me.optCat1Description.TabIndex = 3
        Me.optCat1Description.TabStop = True
        Me.optCat1Description.Text = "Cat1/Description"
        Me.optCat1Description.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Gainsboro
        Me.Label11.Location = New System.Drawing.Point(312, 99)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(132, 10)
        Me.Label11.TabIndex = 56
        '
        'cboReportPrinters
        '
        Me.cboReportPrinters.BackColor = System.Drawing.Color.Lavender
        Me.cboReportPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReportPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReportPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReportPrinters.FormattingEnabled = True
        Me.cboReportPrinters.Location = New System.Drawing.Point(309, 37)
        Me.cboReportPrinters.Name = "cboReportPrinters"
        Me.cboReportPrinters.Size = New System.Drawing.Size(151, 21)
        Me.cboReportPrinters.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(312, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "-- Report Printer --"
        '
        'btnPrintReport
        '
        Me.btnPrintReport.BackColor = System.Drawing.Color.Lavender
        Me.btnPrintReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintReport.Location = New System.Drawing.Point(355, 121)
        Me.btnPrintReport.Name = "btnPrintReport"
        Me.btnPrintReport.Size = New System.Drawing.Size(89, 30)
        Me.btnPrintReport.TabIndex = 7
        Me.btnPrintReport.Text = "Print Report"
        Me.btnPrintReport.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.Lavender
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(355, 168)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(89, 45)
        Me.btnRefresh.TabIndex = 5
        Me.btnRefresh.Text = "Refresh/Show Report"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(174, 27)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "-- POS Reports --"
        '
        'labExplain
        '
        Me.labExplain.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.labExplain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labExplain.Location = New System.Drawing.Point(197, 61)
        Me.labExplain.Name = "labExplain"
        Me.labExplain.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labExplain.Size = New System.Drawing.Size(324, 57)
        Me.labExplain.TabIndex = 14
        Me.labExplain.Text = "labExplain"
        '
        'labReportName
        '
        Me.labReportName.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.labReportName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labReportName.Location = New System.Drawing.Point(191, 37)
        Me.labReportName.Name = "labReportName"
        Me.labReportName.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labReportName.Size = New System.Drawing.Size(330, 21)
        Me.labReportName.TabIndex = 13
        Me.labReportName.Text = "labReportName"
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.BackgroundColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvReport.Location = New System.Drawing.Point(5, 280)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.ReadOnly = True
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvReport.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvReport.RowHeadersWidth = 50
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvReport.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvReport.RowTemplate.Height = 21
        Me.dgvReport.Size = New System.Drawing.Size(1007, 333)
        Me.dgvReport.TabIndex = 10
        '
        'labDLLversion
        '
        Me.labDLLversion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDLLversion.Location = New System.Drawing.Point(5, 616)
        Me.labDLLversion.Name = "labDLLversion"
        Me.labDLLversion.Size = New System.Drawing.Size(306, 13)
        Me.labDLLversion.TabIndex = 45
        Me.labDLLversion.Text = "labDLLversion"
        '
        'ucChildPosReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightYellow
        Me.Controls.Add(Me.labDLLversion)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.panelHdr)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildPosReports"
        Me.Size = New System.Drawing.Size(1017, 633)
        Me.panelHdr.ResumeLayout(False)
        Me.panelHdr.PerformLayout()
        Me.TabControlProduct.ResumeLayout(False)
        Me.TabPageSalesInvoices.ResumeLayout(False)
        Me.panelInvoiceOptions.ResumeLayout(False)
        Me.TabPageProductSales.ResumeLayout(False)
        Me.grpBoxCategory.ResumeLayout(False)
        Me.TabPageCustomerSales.ResumeLayout(False)
        Me.grpBoxCustomer.ResumeLayout(False)
        Me.grpBoxOptions.ResumeLayout(False)
        Me.TabControlOptions.ResumeLayout(False)
        Me.TabPageSales.ResumeLayout(False)
        Me.panelPeriodOpts.ResumeLayout(False)
        Me.panelPeriodOpts.PerformLayout()
        Me.panelPeriodFromTo.ResumeLayout(False)
        Me.TabPageStock.ResumeLayout(False)
        Me.panelStockReport.ResumeLayout(False)
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelHdr As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboReportPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrintReport As System.Windows.Forms.Button
    Friend WithEvents grpBoxOptions As System.Windows.Forms.GroupBox
    Friend WithEvents labReportName As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DTPickerTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPickerFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents labExplain As System.Windows.Forms.Label
    Friend WithEvents chkNoReportIfZeroStock As System.Windows.Forms.CheckBox
    Friend WithEvents tvwReports As System.Windows.Forms.TreeView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents panelPeriodOpts As System.Windows.Forms.Panel
    Friend WithEvents optPeriod12Months As System.Windows.Forms.RadioButton
    Friend WithEvents optperiodThisMonth As System.Windows.Forms.RadioButton
    Friend WithEvents optPeriodToday As System.Windows.Forms.RadioButton
    Friend WithEvents panelPeriodFromTo As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents optPeriodSelect As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents labDLLversion As System.Windows.Forms.Label
    Friend WithEvents labStaffName As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents grpBoxCustomer As System.Windows.Forms.GroupBox
    Friend WithEvents chkAllCustomers As System.Windows.Forms.CheckBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents btnCustomerLookup As System.Windows.Forms.Button
    Friend WithEvents labCustomerName As System.Windows.Forms.Label
    Friend WithEvents TabControlOptions As System.Windows.Forms.TabControl
    Friend WithEvents TabPageSales As System.Windows.Forms.TabPage
    Friend WithEvents TabPageStock As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents optDescription As System.Windows.Forms.RadioButton
    Friend WithEvents optCat1Cat2Description As System.Windows.Forms.RadioButton
    Friend WithEvents optCat1Description As System.Windows.Forms.RadioButton
    Friend WithEvents panelStockReport As System.Windows.Forms.Panel
    Friend WithEvents chkNoReportIfNegStock As System.Windows.Forms.CheckBox
    Friend WithEvents grpBoxCategory As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cboSelectCat1 As System.Windows.Forms.ComboBox
    Friend WithEvents TabControlProduct As System.Windows.Forms.TabControl
    Friend WithEvents TabPageProductSales As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCustomerSales As System.Windows.Forms.TabPage
    Friend WithEvents optProductsNonTaxable As System.Windows.Forms.RadioButton
    Friend WithEvents optProductsTaxable As System.Windows.Forms.RadioButton
    Friend WithEvents optProductsAll As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboSelectCat2 As System.Windows.Forms.ComboBox
    Friend WithEvents TabPageSalesInvoices As System.Windows.Forms.TabPage
    Friend WithEvents panelInvoiceOptions As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents chkInvoicesDiscountedOnly As System.Windows.Forms.CheckBox
    Friend WithEvents optInvoicesCash As System.Windows.Forms.RadioButton
    Friend WithEvents optInvoicesOnAccount As System.Windows.Forms.RadioButton
    Friend WithEvents optInvoicesAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents chkShowInvoiceLines As System.Windows.Forms.CheckBox
    Friend WithEvents chkSalesSummaryOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkNoReportIfPosStock As System.Windows.Forms.CheckBox
    Friend WithEvents cboStaff As ComboBox
    Friend WithEvents Label19 As Label
    Friend WithEvents cboSupplierSales As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents cboSupplierStock As ComboBox
End Class
