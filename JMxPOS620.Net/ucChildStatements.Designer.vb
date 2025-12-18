<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildStatements
    Inherits System.Windows.Forms.UserControl  '= System.Windows.Forms.Form

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRefreshGrid = New System.Windows.Forms.Button()
        Me.cboDaysToShow = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.optShowingAll = New System.Windows.Forms.RadioButton()
        Me.optShowingOutst = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DTPickerCutoff = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboStatementPrinters = New System.Windows.Forms.ComboBox()
        Me.btnPrintSelection = New System.Windows.Forms.Button()
        Me.dgvCustomers = New System.Windows.Forms.DataGridView()
        Me.barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomerName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.outstanding = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.emailAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkPrint = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.chkEmail = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.chkInclude = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.result = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Invoices = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.customerInfo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnDeselectAll = New System.Windows.Forms.Button()
        Me.panelSelection = New System.Windows.Forms.Panel()
        Me.btnSelectEmailOnly = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.labLoadedCount = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.labIncludedCount = New System.Windows.Forms.Label()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnDebtorsReport = New System.Windows.Forms.Button()
        Me.chkNoEmail = New System.Windows.Forms.CheckBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.panelAction = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optDebtorsReportDetail = New System.Windows.Forms.RadioButton()
        Me.optDebtorsReportSummary = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.labPdfPrinter = New System.Windows.Forms.Label()
        Me.panelOptions = New System.Windows.Forms.Panel()
        CType(Me.dgvCustomers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelSelection.SuspendLayout()
        Me.panelAction.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.panelOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.AliceBlue
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 164)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Customer Statements"
        '
        'btnRefreshGrid
        '
        Me.btnRefreshGrid.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRefreshGrid.Location = New System.Drawing.Point(108, 116)
        Me.btnRefreshGrid.Name = "btnRefreshGrid"
        Me.btnRefreshGrid.Size = New System.Drawing.Size(89, 38)
        Me.btnRefreshGrid.TabIndex = 8
        Me.btnRefreshGrid.Text = "Refresh Customer List"
        Me.btnRefreshGrid.UseVisualStyleBackColor = False
        '
        'cboDaysToShow
        '
        Me.cboDaysToShow.BackColor = System.Drawing.Color.AliceBlue
        Me.cboDaysToShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDaysToShow.FormattingEnabled = True
        Me.cboDaysToShow.Location = New System.Drawing.Point(352, 37)
        Me.cboDaysToShow.Name = "cboDaysToShow"
        Me.cboDaysToShow.Size = New System.Drawing.Size(54, 21)
        Me.cboDaysToShow.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(349, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 47)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "No of Days of closed Invoices to be included."
        '
        'optShowingAll
        '
        Me.optShowingAll.Location = New System.Drawing.Point(230, 118)
        Me.optShowingAll.Name = "optShowingAll"
        Me.optShowingAll.Size = New System.Drawing.Size(87, 17)
        Me.optShowingAll.TabIndex = 4
        Me.optShowingAll.TabStop = True
        Me.optShowingAll.Text = "All Invoices"
        Me.optShowingAll.UseVisualStyleBackColor = True
        '
        'optShowingOutst
        '
        Me.optShowingOutst.Location = New System.Drawing.Point(230, 72)
        Me.optShowingOutst.Name = "optShowingOutst"
        Me.optShowingOutst.Size = New System.Drawing.Size(96, 37)
        Me.optShowingOutst.TabIndex = 3
        Me.optShowingOutst.TabStop = True
        Me.optShowingOutst.Text = "Outstanding Invoices"
        Me.optShowingOutst.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(160, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Showing: "
        '
        'DTPickerCutoff
        '
        Me.DTPickerCutoff.Location = New System.Drawing.Point(108, 37)
        Me.DTPickerCutoff.Name = "DTPickerCutoff"
        Me.DTPickerCutoff.Size = New System.Drawing.Size(177, 21)
        Me.DTPickerCutoff.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(105, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 16)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "As at Date: "
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(379, 97)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(129, 16)
        Me.Label20.TabIndex = 60
        Me.Label20.Text = "Printer for Hard Copy:"
        '
        'cboStatementPrinters
        '
        Me.cboStatementPrinters.BackColor = System.Drawing.Color.White
        Me.cboStatementPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatementPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboStatementPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatementPrinters.FormattingEnabled = True
        Me.cboStatementPrinters.Location = New System.Drawing.Point(382, 116)
        Me.cboStatementPrinters.Name = "cboStatementPrinters"
        Me.cboStatementPrinters.Size = New System.Drawing.Size(126, 21)
        Me.cboStatementPrinters.TabIndex = 59
        '
        'btnPrintSelection
        '
        Me.btnPrintSelection.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPrintSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintSelection.Location = New System.Drawing.Point(22, 97)
        Me.btnPrintSelection.Name = "btnPrintSelection"
        Me.btnPrintSelection.Size = New System.Drawing.Size(106, 57)
        Me.btnPrintSelection.TabIndex = 0
        Me.btnPrintSelection.Text = "Print Highlighted Statement only"
        Me.ToolTip1.SetToolTip(Me.btnPrintSelection, "Print Single Statement")
        Me.btnPrintSelection.UseVisualStyleBackColor = False
        '
        'dgvCustomers
        '
        Me.dgvCustomers.AllowUserToAddRows = False
        Me.dgvCustomers.AllowUserToDeleteRows = False
        Me.dgvCustomers.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvCustomers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvCustomers.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCustomers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCustomers.ColumnHeadersHeight = 24
        Me.dgvCustomers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.barcode, Me.CustomerName, Me.outstanding, Me.emailAddress, Me.chkPrint, Me.chkEmail, Me.chkInclude, Me.result, Me.Invoices, Me.customerInfo})
        Me.dgvCustomers.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvCustomers.Location = New System.Drawing.Point(5, 235)
        Me.dgvCustomers.MultiSelect = False
        Me.dgvCustomers.Name = "dgvCustomers"
        Me.dgvCustomers.RowHeadersWidth = 21
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvCustomers.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvCustomers.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.dgvCustomers.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.dgvCustomers.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvCustomers.RowTemplate.Height = 17
        Me.dgvCustomers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCustomers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCustomers.Size = New System.Drawing.Size(997, 387)
        Me.dgvCustomers.StandardTab = True
        Me.dgvCustomers.TabIndex = 2
        '
        'barcode
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.barcode.DefaultCellStyle = DataGridViewCellStyle3
        Me.barcode.FillWeight = 51.84303!
        Me.barcode.HeaderText = "Cust. No"
        Me.barcode.Name = "barcode"
        Me.barcode.ReadOnly = True
        Me.barcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CustomerName
        '
        Me.CustomerName.FillWeight = 155.5291!
        Me.CustomerName.HeaderText = "Customer Name"
        Me.CustomerName.Name = "CustomerName"
        Me.CustomerName.ReadOnly = True
        Me.CustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'outstanding
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.outstanding.DefaultCellStyle = DataGridViewCellStyle4
        Me.outstanding.FillWeight = 82.94884!
        Me.outstanding.HeaderText = "Outstand."
        Me.outstanding.Name = "outstanding"
        Me.outstanding.ReadOnly = True
        Me.outstanding.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'emailAddress
        '
        Me.emailAddress.FillWeight = 165.8977!
        Me.emailAddress.HeaderText = "Email Address"
        Me.emailAddress.Name = "emailAddress"
        Me.emailAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'chkPrint
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.NullValue = False
        Me.chkPrint.DefaultCellStyle = DataGridViewCellStyle5
        Me.chkPrint.FillWeight = 60.0!
        Me.chkPrint.HeaderText = "Print"
        Me.chkPrint.Name = "chkPrint"
        '
        'chkEmail
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.NullValue = False
        Me.chkEmail.DefaultCellStyle = DataGridViewCellStyle6
        Me.chkEmail.FillWeight = 60.0!
        Me.chkEmail.HeaderText = "Email"
        Me.chkEmail.Name = "chkEmail"
        '
        'chkInclude
        '
        Me.chkInclude.FillWeight = 60.0!
        Me.chkInclude.HeaderText = "Mark to Send"
        Me.chkInclude.Name = "chkInclude"
        '
        'result
        '
        Me.result.FillWeight = 72.58025!
        Me.result.HeaderText = "Result"
        Me.result.Name = "result"
        Me.result.ReadOnly = True
        Me.result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Invoices
        '
        Me.Invoices.FillWeight = 10.0!
        Me.Invoices.HeaderText = "Invoices"
        Me.Invoices.Name = "Invoices"
        Me.Invoices.ReadOnly = True
        Me.Invoices.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Invoices.Visible = False
        '
        'customerInfo
        '
        Me.customerInfo.FillWeight = 10.0!
        Me.customerInfo.HeaderText = "customerInfo"
        Me.customerInfo.Name = "customerInfo"
        Me.customerInfo.ReadOnly = True
        Me.customerInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.customerInfo.Visible = False
        '
        'btnSelectAll
        '
        Me.btnSelectAll.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAll.Location = New System.Drawing.Point(110, 10)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(78, 41)
        Me.btnSelectAll.TabIndex = 0
        Me.btnSelectAll.Text = "Mark All"
        Me.btnSelectAll.UseVisualStyleBackColor = False
        '
        'btnDeselectAll
        '
        Me.btnDeselectAll.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDeselectAll.Location = New System.Drawing.Point(297, 10)
        Me.btnDeselectAll.Name = "btnDeselectAll"
        Me.btnDeselectAll.Size = New System.Drawing.Size(78, 41)
        Me.btnDeselectAll.TabIndex = 2
        Me.btnDeselectAll.Text = "Un-Mark All"
        Me.btnDeselectAll.UseVisualStyleBackColor = False
        '
        'panelSelection
        '
        Me.panelSelection.BackColor = System.Drawing.Color.White
        Me.panelSelection.Controls.Add(Me.btnSelectEmailOnly)
        Me.panelSelection.Controls.Add(Me.Label7)
        Me.panelSelection.Controls.Add(Me.labLoadedCount)
        Me.panelSelection.Controls.Add(Me.Label6)
        Me.panelSelection.Controls.Add(Me.labIncludedCount)
        Me.panelSelection.Controls.Add(Me.btnDeselectAll)
        Me.panelSelection.Controls.Add(Me.btnSelectAll)
        Me.panelSelection.Location = New System.Drawing.Point(3, 170)
        Me.panelSelection.Name = "panelSelection"
        Me.panelSelection.Size = New System.Drawing.Size(470, 60)
        Me.panelSelection.TabIndex = 1
        '
        'btnSelectEmailOnly
        '
        Me.btnSelectEmailOnly.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSelectEmailOnly.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectEmailOnly.Location = New System.Drawing.Point(206, 10)
        Me.btnSelectEmailOnly.Name = "btnSelectEmailOnly"
        Me.btnSelectEmailOnly.Size = New System.Drawing.Size(78, 41)
        Me.btnSelectEmailOnly.TabIndex = 1
        Me.btnSelectEmailOnly.Text = "Mark Email Recips. Only"
        Me.btnSelectEmailOnly.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.AliceBlue
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(5, 2, 0, 0)
        Me.Label7.Size = New System.Drawing.Size(87, 30)
        Me.Label7.TabIndex = 67
        Me.Label7.Text = "No. of Cust. Loaded: "
        '
        'labLoadedCount
        '
        Me.labLoadedCount.BackColor = System.Drawing.Color.AliceBlue
        Me.labLoadedCount.Location = New System.Drawing.Point(0, 31)
        Me.labLoadedCount.Name = "labLoadedCount"
        Me.labLoadedCount.Padding = New System.Windows.Forms.Padding(5, 2, 0, 0)
        Me.labLoadedCount.Size = New System.Drawing.Size(87, 27)
        Me.labLoadedCount.TabIndex = 66
        Me.labLoadedCount.Text = "labLoadedCount"
        Me.labLoadedCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(395, 8)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 29)
        Me.Label6.TabIndex = 65
        Me.Label6.Text = "Number Marked"
        '
        'labIncludedCount
        '
        Me.labIncludedCount.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labIncludedCount.Location = New System.Drawing.Point(395, 37)
        Me.labIncludedCount.Name = "labIncludedCount"
        Me.labIncludedCount.Size = New System.Drawing.Size(56, 17)
        Me.labIncludedCount.TabIndex = 64
        Me.labIncludedCount.Text = "labIncludedCount"
        Me.labIncludedCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'labStatus
        '
        Me.labStatus.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.Location = New System.Drawing.Point(19, 171)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labStatus.Size = New System.Drawing.Size(232, 51)
        Me.labStatus.TabIndex = 68
        Me.labStatus.Text = "labStatus"
        '
        'btnExecute
        '
        Me.btnExecute.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExecute.Location = New System.Drawing.Point(142, 97)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(109, 57)
        Me.btnExecute.TabIndex = 1
        Me.btnExecute.Text = "Execute Statements for Marked Customers" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ToolTip1.SetToolTip(Me.btnExecute, "Do all Statements")
        Me.btnExecute.UseVisualStyleBackColor = False
        '
        'btnDebtorsReport
        '
        Me.btnDebtorsReport.BackColor = System.Drawing.Color.AliceBlue
        Me.btnDebtorsReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDebtorsReport.Location = New System.Drawing.Point(277, 97)
        Me.btnDebtorsReport.Name = "btnDebtorsReport"
        Me.btnDebtorsReport.Size = New System.Drawing.Size(86, 57)
        Me.btnDebtorsReport.TabIndex = 2
        Me.btnDebtorsReport.Text = "Print Debtors Report"
        Me.ToolTip1.SetToolTip(Me.btnDebtorsReport, "Full Debtors Report")
        Me.btnDebtorsReport.UseVisualStyleBackColor = False
        '
        'chkNoEmail
        '
        Me.chkNoEmail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkNoEmail.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNoEmail.Location = New System.Drawing.Point(359, 9)
        Me.chkNoEmail.Name = "chkNoEmail"
        Me.chkNoEmail.Size = New System.Drawing.Size(77, 61)
        Me.chkNoEmail.TabIndex = 71
        Me.chkNoEmail.Text = "If Checked, then Emailing is disabled.."
        Me.ToolTip1.SetToolTip(Me.chkNoEmail, "If Checked, then Emailing is disabled..")
        Me.chkNoEmail.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.AliceBlue
        Me.btnClose.Location = New System.Drawing.Point(454, 17)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 29)
        Me.btnClose.TabIndex = 73
        Me.btnClose.Text = "Close X"
        Me.ToolTip1.SetToolTip(Me.btnClose, "Close form and Exit")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(19, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(163, 41)
        Me.Label8.TabIndex = 69
        Me.Label8.Text = "Note: An Adobe PDF Printer must be available for Emailing function.. Yours is:"
        '
        'panelAction
        '
        Me.panelAction.BackColor = System.Drawing.Color.Beige
        Me.panelAction.Controls.Add(Me.btnClose)
        Me.panelAction.Controls.Add(Me.Panel1)
        Me.panelAction.Controls.Add(Me.cboStatementPrinters)
        Me.panelAction.Controls.Add(Me.labStatus)
        Me.panelAction.Controls.Add(Me.chkNoEmail)
        Me.panelAction.Controls.Add(Me.Label20)
        Me.panelAction.Controls.Add(Me.Label9)
        Me.panelAction.Controls.Add(Me.labPdfPrinter)
        Me.panelAction.Controls.Add(Me.btnDebtorsReport)
        Me.panelAction.Controls.Add(Me.btnExecute)
        Me.panelAction.Controls.Add(Me.btnPrintSelection)
        Me.panelAction.Controls.Add(Me.Label8)
        Me.panelAction.Location = New System.Drawing.Point(479, 3)
        Me.panelAction.Name = "panelAction"
        Me.panelAction.Size = New System.Drawing.Size(523, 227)
        Me.panelAction.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.AliceBlue
        Me.Panel1.Controls.Add(Me.optDebtorsReportDetail)
        Me.Panel1.Controls.Add(Me.optDebtorsReportSummary)
        Me.Panel1.Location = New System.Drawing.Point(277, 164)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(86, 57)
        Me.Panel1.TabIndex = 72
        '
        'optDebtorsReportDetail
        '
        Me.optDebtorsReportDetail.AutoSize = True
        Me.optDebtorsReportDetail.Location = New System.Drawing.Point(11, 33)
        Me.optDebtorsReportDetail.Name = "optDebtorsReportDetail"
        Me.optDebtorsReportDetail.Size = New System.Drawing.Size(64, 17)
        Me.optDebtorsReportDetail.TabIndex = 3
        Me.optDebtorsReportDetail.TabStop = True
        Me.optDebtorsReportDetail.Text = "Detailed"
        Me.optDebtorsReportDetail.UseVisualStyleBackColor = True
        '
        'optDebtorsReportSummary
        '
        Me.optDebtorsReportSummary.AutoSize = True
        Me.optDebtorsReportSummary.Location = New System.Drawing.Point(11, 8)
        Me.optDebtorsReportSummary.Name = "optDebtorsReportSummary"
        Me.optDebtorsReportSummary.Size = New System.Drawing.Size(69, 17)
        Me.optDebtorsReportSummary.TabIndex = 0
        Me.optDebtorsReportSummary.TabStop = True
        Me.optDebtorsReportSummary.Text = "Summary"
        Me.optDebtorsReportSummary.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.Label9.Location = New System.Drawing.Point(211, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(142, 69)
        Me.Label9.TabIndex = 71
        Me.Label9.Text = "Note2: To avoid display of the Email PDF, set the Adobe PDF Printer Default prope" & _
    "rty ""View .. PDF"" to No (Cleared).."
        '
        'labPdfPrinter
        '
        Me.labPdfPrinter.Location = New System.Drawing.Point(19, 59)
        Me.labPdfPrinter.Name = "labPdfPrinter"
        Me.labPdfPrinter.Size = New System.Drawing.Size(185, 19)
        Me.labPdfPrinter.TabIndex = 70
        Me.labPdfPrinter.Text = "labPdfPrinter"
        '
        'panelOptions
        '
        Me.panelOptions.Controls.Add(Me.Label1)
        Me.panelOptions.Controls.Add(Me.Label3)
        Me.panelOptions.Controls.Add(Me.Label4)
        Me.panelOptions.Controls.Add(Me.btnRefreshGrid)
        Me.panelOptions.Controls.Add(Me.cboDaysToShow)
        Me.panelOptions.Controls.Add(Me.optShowingAll)
        Me.panelOptions.Controls.Add(Me.Label5)
        Me.panelOptions.Controls.Add(Me.optShowingOutst)
        Me.panelOptions.Controls.Add(Me.DTPickerCutoff)
        Me.panelOptions.Location = New System.Drawing.Point(3, 3)
        Me.panelOptions.Name = "panelOptions"
        Me.panelOptions.Size = New System.Drawing.Size(470, 165)
        Me.panelOptions.TabIndex = 54
        '
        'ucChildStatements
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.panelOptions)
        Me.Controls.Add(Me.panelAction)
        Me.Controls.Add(Me.panelSelection)
        Me.Controls.Add(Me.dgvCustomers)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildStatements"
        Me.Size = New System.Drawing.Size(1010, 638)
        CType(Me.dgvCustomers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelSelection.ResumeLayout(False)
        Me.panelAction.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.panelOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DTPickerCutoff As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents optShowingAll As System.Windows.Forms.RadioButton
    Friend WithEvents optShowingOutst As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cboStatementPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrintSelection As System.Windows.Forms.Button
    Friend WithEvents dgvCustomers As System.Windows.Forms.DataGridView
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnDeselectAll As System.Windows.Forms.Button
    Friend WithEvents panelSelection As System.Windows.Forms.Panel
    Friend WithEvents btnExecute As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents labIncludedCount As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents labLoadedCount As System.Windows.Forms.Label
    Friend WithEvents labStatus As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboDaysToShow As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnRefreshGrid As System.Windows.Forms.Button
    Friend WithEvents chkNoEmail As System.Windows.Forms.CheckBox
    Friend WithEvents panelAction As System.Windows.Forms.Panel
    Friend WithEvents btnDebtorsReport As System.Windows.Forms.Button
    Friend WithEvents labPdfPrinter As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents optDebtorsReportDetail As System.Windows.Forms.RadioButton
    Friend WithEvents optDebtorsReportSummary As System.Windows.Forms.RadioButton
    Friend WithEvents btnSelectEmailOnly As System.Windows.Forms.Button
    Friend WithEvents barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CustomerName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents outstanding As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents emailAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkPrint As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkEmail As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkInclude As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents result As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Invoices As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customerInfo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents panelOptions As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
