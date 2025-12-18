<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildStocktake
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucChildStocktake))
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelTopBanner = New System.Windows.Forms.Panel()
        Me.panelHdr = New System.Windows.Forms.Panel()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.txtPartialSelection = New System.Windows.Forms.TextBox()
        Me.TabControlReport = New System.Windows.Forms.TabControl()
        Me.TabPageReport = New System.Windows.Forms.TabPage()
        Me.txtReport = New System.Windows.Forms.TextBox()
        Me.TabPagePrint = New System.Windows.Forms.TabPage()
        Me.grpBoxPrint = New System.Windows.Forms.GroupBox()
        Me.labCountedCount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cboPrinters = New System.Windows.Forms.ComboBox()
        Me.btnCatSelect = New System.Windows.Forms.Button()
        Me.chkKeepScannedLeadZeroes = New System.Windows.Forms.CheckBox()
        Me.grpBoxType = New System.Windows.Forms.GroupBox()
        Me.optStocktakeType_full = New System.Windows.Forms.RadioButton()
        Me.optStocktakeType_partial = New System.Windows.Forms.RadioButton()
        Me.optStocktakeType_single = New System.Windows.Forms.RadioButton()
        Me.labChooseType = New System.Windows.Forms.Label()
        Me.btnFullOK = New System.Windows.Forms.Button()
        Me.labStockTakeId = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labHdrDateCreated = New System.Windows.Forms.Label()
        Me.labHdr1 = New System.Windows.Forms.Label()
        Me.panelFooter = New System.Windows.Forms.Panel()
        Me.labBottomBar = New System.Windows.Forms.Label()
        Me.chkConfirmSetZeroQty = New System.Windows.Forms.CheckBox()
        Me.labFreeRange = New System.Windows.Forms.Label()
        Me.btnCountAllAsZero = New System.Windows.Forms.Button()
        Me.cmdDestroy = New System.Windows.Forms.Button()
        Me.btnCommitAll = New System.Windows.Forms.Button()
        Me.labCanScan = New System.Windows.Forms.Label()
        Me.labScanDescription = New System.Windows.Forms.Label()
        Me.labScanCat2 = New System.Windows.Forms.Label()
        Me.labScanCat1 = New System.Windows.Forms.Label()
        Me.txtScanBarcode = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.frameBrowseResults = New System.Windows.Forms.GroupBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.dgvResultsList = New System.Windows.Forms.DataGridView()
        Me.txtResultsFind = New System.Windows.Forms.TextBox()
        Me.labResultsCount = New System.Windows.Forms.Label()
        Me.LabResultsFind = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnManualSave = New System.Windows.Forms.Button()
        Me.btnManualCancel = New System.Windows.Forms.Button()
        Me.txtManualExpected = New System.Windows.Forms.TextBox()
        Me.txtManualQty = New System.Windows.Forms.TextBox()
        Me.btnAutoUndo = New System.Windows.Forms.Button()
        Me.txtScanBarcodeManual = New System.Windows.Forms.TextBox()
        Me.btnAddToCount = New System.Windows.Forms.Button()
        Me.grpBoxScanAuto = New System.Windows.Forms.GroupBox()
        Me.labExplainScanning = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnLookupStock = New System.Windows.Forms.Button()
        Me.TabPageManual = New System.Windows.Forms.TabPage()
        Me.grpBoxManual = New System.Windows.Forms.GroupBox()
        Me.grpBoxScanManual = New System.Windows.Forms.GroupBox()
        Me.labScanManualCat2 = New System.Windows.Forms.Label()
        Me.labScanManualCat1 = New System.Windows.Forms.Label()
        Me.labScanManualDescription = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.labExplainManual = New System.Windows.Forms.Label()
        Me.grpBoxManualCount = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtLastManualBarcode = New System.Windows.Forms.TextBox()
        Me.picmanualWaiting = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TabControlResults = New System.Windows.Forms.TabControl()
        Me.TabPageCounted = New System.Windows.Forms.TabPage()
        Me.TabPageUncounted = New System.Windows.Forms.TabPage()
        Me.frameBrowseUncounted = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dgvUncounted = New System.Windows.Forms.DataGridView()
        Me.txtUncountedFind = New System.Windows.Forms.TextBox()
        Me.labUncountedCount = New System.Windows.Forms.Label()
        Me.labUncountedFind = New System.Windows.Forms.Label()
        Me.TabPageAuto = New System.Windows.Forms.TabPage()
        Me.grpBoxAuto = New System.Windows.Forms.GroupBox()
        Me.labAutoInfo = New System.Windows.Forms.Label()
        Me.dgvAutoCountItems = New System.Windows.Forms.DataGridView()
        Me.Cat1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cat2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Stock_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Auto_Counted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.qty_on_record = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.qty_counted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.qty_difference = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.chkDoNotPreLoadNegItems = New System.Windows.Forms.CheckBox()
        Me.panelHdr.SuspendLayout()
        Me.TabControlReport.SuspendLayout()
        Me.TabPageReport.SuspendLayout()
        Me.TabPagePrint.SuspendLayout()
        Me.grpBoxPrint.SuspendLayout()
        Me.grpBoxType.SuspendLayout()
        Me.panelFooter.SuspendLayout()
        Me.frameBrowseResults.SuspendLayout()
        CType(Me.dgvResultsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxScanAuto.SuspendLayout()
        Me.TabPageManual.SuspendLayout()
        Me.grpBoxManual.SuspendLayout()
        Me.grpBoxScanManual.SuspendLayout()
        Me.grpBoxManualCount.SuspendLayout()
        CType(Me.picmanualWaiting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlResults.SuspendLayout()
        Me.TabPageCounted.SuspendLayout()
        Me.TabPageUncounted.SuspendLayout()
        Me.frameBrowseUncounted.SuspendLayout()
        CType(Me.dgvUncounted, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageAuto.SuspendLayout()
        Me.grpBoxAuto.SuspendLayout()
        CType(Me.dgvAutoCountItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelTopBanner
        '
        Me.panelTopBanner.BackColor = System.Drawing.Color.Goldenrod
        Me.panelTopBanner.Location = New System.Drawing.Point(0, 1)
        Me.panelTopBanner.Name = "panelTopBanner"
        Me.panelTopBanner.Size = New System.Drawing.Size(27, 129)
        Me.panelTopBanner.TabIndex = 4
        '
        'panelHdr
        '
        Me.panelHdr.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelHdr.Controls.Add(Me.chkDoNotPreLoadNegItems)
        Me.panelHdr.Controls.Add(Me.btnExit)
        Me.panelHdr.Controls.Add(Me.txtPartialSelection)
        Me.panelHdr.Controls.Add(Me.TabControlReport)
        Me.panelHdr.Controls.Add(Me.btnCatSelect)
        Me.panelHdr.Controls.Add(Me.chkKeepScannedLeadZeroes)
        Me.panelHdr.Controls.Add(Me.grpBoxType)
        Me.panelHdr.Controls.Add(Me.btnFullOK)
        Me.panelHdr.Controls.Add(Me.labStockTakeId)
        Me.panelHdr.Controls.Add(Me.Label2)
        Me.panelHdr.Controls.Add(Me.Label1)
        Me.panelHdr.Controls.Add(Me.labHdrDateCreated)
        Me.panelHdr.Controls.Add(Me.labHdr1)
        Me.panelHdr.Location = New System.Drawing.Point(26, 2)
        Me.panelHdr.Name = "panelHdr"
        Me.panelHdr.Size = New System.Drawing.Size(978, 130)
        Me.panelHdr.TabIndex = 6
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Lavender
        Me.btnExit.Location = New System.Drawing.Point(914, 10)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(59, 34)
        Me.btnExit.TabIndex = 59
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'txtPartialSelection
        '
        Me.txtPartialSelection.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtPartialSelection.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPartialSelection.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartialSelection.Location = New System.Drawing.Point(431, 17)
        Me.txtPartialSelection.Multiline = True
        Me.txtPartialSelection.Name = "txtPartialSelection"
        Me.txtPartialSelection.ReadOnly = True
        Me.txtPartialSelection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPartialSelection.Size = New System.Drawing.Size(204, 63)
        Me.txtPartialSelection.TabIndex = 16
        '
        'TabControlReport
        '
        Me.TabControlReport.Controls.Add(Me.TabPageReport)
        Me.TabControlReport.Controls.Add(Me.TabPagePrint)
        Me.TabControlReport.Location = New System.Drawing.Point(671, 7)
        Me.TabControlReport.Name = "TabControlReport"
        Me.TabControlReport.SelectedIndex = 0
        Me.TabControlReport.Size = New System.Drawing.Size(224, 119)
        Me.TabControlReport.TabIndex = 58
        '
        'TabPageReport
        '
        Me.TabPageReport.Controls.Add(Me.txtReport)
        Me.TabPageReport.Location = New System.Drawing.Point(4, 22)
        Me.TabPageReport.Name = "TabPageReport"
        Me.TabPageReport.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageReport.Size = New System.Drawing.Size(216, 93)
        Me.TabPageReport.TabIndex = 0
        Me.TabPageReport.Text = "Reporting"
        Me.TabPageReport.UseVisualStyleBackColor = True
        '
        'txtReport
        '
        Me.txtReport.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.txtReport.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReport.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.txtReport.Location = New System.Drawing.Point(6, 5)
        Me.txtReport.Multiline = True
        Me.txtReport.Name = "txtReport"
        Me.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReport.Size = New System.Drawing.Size(204, 81)
        Me.txtReport.TabIndex = 8
        Me.txtReport.Text = "txtReport"
        '
        'TabPagePrint
        '
        Me.TabPagePrint.Controls.Add(Me.grpBoxPrint)
        Me.TabPagePrint.Location = New System.Drawing.Point(4, 22)
        Me.TabPagePrint.Name = "TabPagePrint"
        Me.TabPagePrint.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPagePrint.Size = New System.Drawing.Size(216, 93)
        Me.TabPagePrint.TabIndex = 1
        Me.TabPagePrint.Text = "Print"
        Me.TabPagePrint.UseVisualStyleBackColor = True
        '
        'grpBoxPrint
        '
        Me.grpBoxPrint.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpBoxPrint.Controls.Add(Me.labCountedCount)
        Me.grpBoxPrint.Controls.Add(Me.Label5)
        Me.grpBoxPrint.Controls.Add(Me.btnPrint)
        Me.grpBoxPrint.Controls.Add(Me.Label17)
        Me.grpBoxPrint.Controls.Add(Me.cboPrinters)
        Me.grpBoxPrint.Location = New System.Drawing.Point(1, 1)
        Me.grpBoxPrint.Name = "grpBoxPrint"
        Me.grpBoxPrint.Size = New System.Drawing.Size(208, 95)
        Me.grpBoxPrint.TabIndex = 57
        Me.grpBoxPrint.TabStop = False
        Me.grpBoxPrint.Text = "grpBoxPrint"
        '
        'labCountedCount
        '
        Me.labCountedCount.Location = New System.Drawing.Point(124, 69)
        Me.labCountedCount.Name = "labCountedCount"
        Me.labCountedCount.Size = New System.Drawing.Size(72, 18)
        Me.labCountedCount.TabIndex = 60
        Me.labCountedCount.Text = "labCountedCount"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 57)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 29)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "Stock Lines Counted/Uncounted: "
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.Lavender
        Me.btnPrint.CausesValidation = False
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Location = New System.Drawing.Point(147, 15)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(54, 37)
        Me.btnPrint.TabIndex = 58
        Me.btnPrint.Text = "Print Report"
        Me.ToolTip1.SetToolTip(Me.btnPrint, "Print Report")
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(6, 17)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(112, 13)
        Me.Label17.TabIndex = 57
        Me.Label17.Text = "- Select Printer -"
        '
        'cboPrinters
        '
        Me.cboPrinters.BackColor = System.Drawing.Color.Lavender
        Me.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPrinters.FormattingEnabled = True
        Me.cboPrinters.Location = New System.Drawing.Point(7, 31)
        Me.cboPrinters.Name = "cboPrinters"
        Me.cboPrinters.Size = New System.Drawing.Size(134, 21)
        Me.cboPrinters.TabIndex = 56
        '
        'btnCatSelect
        '
        Me.btnCatSelect.BackColor = System.Drawing.Color.Lavender
        Me.btnCatSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCatSelect.Location = New System.Drawing.Point(471, 92)
        Me.btnCatSelect.Name = "btnCatSelect"
        Me.btnCatSelect.Size = New System.Drawing.Size(106, 23)
        Me.btnCatSelect.TabIndex = 14
        Me.btnCatSelect.Text = "Select Categories."
        Me.ToolTip1.SetToolTip(Me.btnCatSelect, "Select Categories")
        Me.btnCatSelect.UseVisualStyleBackColor = False
        '
        'chkKeepScannedLeadZeroes
        '
        Me.chkKeepScannedLeadZeroes.BackColor = System.Drawing.Color.Lavender
        Me.chkKeepScannedLeadZeroes.Location = New System.Drawing.Point(15, 85)
        Me.chkKeepScannedLeadZeroes.Name = "chkKeepScannedLeadZeroes"
        Me.chkKeepScannedLeadZeroes.Size = New System.Drawing.Size(105, 33)
        Me.chkKeepScannedLeadZeroes.TabIndex = 16
        Me.chkKeepScannedLeadZeroes.Text = " Scannng- Keep Leading Zeroes"
        Me.chkKeepScannedLeadZeroes.UseVisualStyleBackColor = False
        '
        'grpBoxType
        '
        Me.grpBoxType.Controls.Add(Me.optStocktakeType_full)
        Me.grpBoxType.Controls.Add(Me.optStocktakeType_partial)
        Me.grpBoxType.Controls.Add(Me.optStocktakeType_single)
        Me.grpBoxType.Controls.Add(Me.labChooseType)
        Me.grpBoxType.Location = New System.Drawing.Point(294, 11)
        Me.grpBoxType.Name = "grpBoxType"
        Me.grpBoxType.Size = New System.Drawing.Size(125, 107)
        Me.grpBoxType.TabIndex = 13
        Me.grpBoxType.TabStop = False
        Me.grpBoxType.Text = "grpBoxType"
        '
        'optStocktakeType_full
        '
        Me.optStocktakeType_full.BackColor = System.Drawing.Color.Lavender
        Me.optStocktakeType_full.Location = New System.Drawing.Point(6, 27)
        Me.optStocktakeType_full.Name = "optStocktakeType_full"
        Me.optStocktakeType_full.Size = New System.Drawing.Size(110, 20)
        Me.optStocktakeType_full.TabIndex = 2
        Me.optStocktakeType_full.TabStop = True
        Me.optStocktakeType_full.Text = "Full Stocktake"
        Me.ToolTip1.SetToolTip(Me.optStocktakeType_full, "Stocktake All Stock..")
        Me.optStocktakeType_full.UseVisualStyleBackColor = False
        '
        'optStocktakeType_partial
        '
        Me.optStocktakeType_partial.BackColor = System.Drawing.Color.Lavender
        Me.optStocktakeType_partial.Location = New System.Drawing.Point(6, 49)
        Me.optStocktakeType_partial.Name = "optStocktakeType_partial"
        Me.optStocktakeType_partial.Size = New System.Drawing.Size(110, 20)
        Me.optStocktakeType_partial.TabIndex = 3
        Me.optStocktakeType_partial.TabStop = True
        Me.optStocktakeType_partial.Text = "Partial Stocktake"
        Me.ToolTip1.SetToolTip(Me.optStocktakeType_partial, "Stocktake selected category only..")
        Me.optStocktakeType_partial.UseVisualStyleBackColor = False
        '
        'optStocktakeType_single
        '
        Me.optStocktakeType_single.BackColor = System.Drawing.Color.Lavender
        Me.optStocktakeType_single.Location = New System.Drawing.Point(6, 71)
        Me.optStocktakeType_single.Name = "optStocktakeType_single"
        Me.optStocktakeType_single.Size = New System.Drawing.Size(110, 30)
        Me.optStocktakeType_single.TabIndex = 4
        Me.optStocktakeType_single.TabStop = True
        Me.optStocktakeType_single.Text = "Single (Free) Stocktake"
        Me.ToolTip1.SetToolTip(Me.optStocktakeType_single, "Free Range Stocktake select items as needed...  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- No zeroing of uncounted item" & _
        "s.")
        Me.optStocktakeType_single.UseVisualStyleBackColor = False
        '
        'labChooseType
        '
        Me.labChooseType.AutoSize = True
        Me.labChooseType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labChooseType.ForeColor = System.Drawing.Color.Green
        Me.labChooseType.Location = New System.Drawing.Point(6, 11)
        Me.labChooseType.Name = "labChooseType"
        Me.labChooseType.Size = New System.Drawing.Size(82, 13)
        Me.labChooseType.TabIndex = 6
        Me.labChooseType.Text = "Choose Type:"
        '
        'btnFullOK
        '
        Me.btnFullOK.BackColor = System.Drawing.Color.Lavender
        Me.btnFullOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFullOK.Location = New System.Drawing.Point(586, 92)
        Me.btnFullOK.Name = "btnFullOK"
        Me.btnFullOK.Size = New System.Drawing.Size(49, 23)
        Me.btnFullOK.TabIndex = 15
        Me.btnFullOK.Text = "OK"
        Me.ToolTip1.SetToolTip(Me.btnFullOK, "Proceed to StockTake")
        Me.btnFullOK.UseVisualStyleBackColor = False
        '
        'labStockTakeId
        '
        Me.labStockTakeId.Location = New System.Drawing.Point(9, 41)
        Me.labStockTakeId.Name = "labStockTakeId"
        Me.labStockTakeId.Size = New System.Drawing.Size(111, 13)
        Me.labStockTakeId.TabIndex = 9
        Me.labStockTakeId.Text = "Stocktake Id"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(226, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 34)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Stocktake Type:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Created: "
        '
        'labHdrDateCreated
        '
        Me.labHdrDateCreated.AutoSize = True
        Me.labHdrDateCreated.Location = New System.Drawing.Point(68, 62)
        Me.labHdrDateCreated.Name = "labHdrDateCreated"
        Me.labHdrDateCreated.Size = New System.Drawing.Size(100, 13)
        Me.labHdrDateCreated.TabIndex = 1
        Me.labHdrDateCreated.Text = "labHdrDateCreated"
        '
        'labHdr1
        '
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle))
        Me.labHdr1.ForeColor = System.Drawing.Color.Goldenrod
        Me.labHdr1.Location = New System.Drawing.Point(3, 3)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Size = New System.Drawing.Size(273, 31)
        Me.labHdr1.TabIndex = 0
        Me.labHdr1.Text = "New Stocktake"
        Me.labHdr1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panelFooter
        '
        Me.panelFooter.BackColor = System.Drawing.Color.White
        Me.panelFooter.Controls.Add(Me.labBottomBar)
        Me.panelFooter.Controls.Add(Me.chkConfirmSetZeroQty)
        Me.panelFooter.Controls.Add(Me.labFreeRange)
        Me.panelFooter.Controls.Add(Me.btnCountAllAsZero)
        Me.panelFooter.Controls.Add(Me.cmdDestroy)
        Me.panelFooter.Controls.Add(Me.btnCommitAll)
        Me.panelFooter.Location = New System.Drawing.Point(876, 134)
        Me.panelFooter.Name = "panelFooter"
        Me.panelFooter.Size = New System.Drawing.Size(128, 504)
        Me.panelFooter.TabIndex = 7
        '
        'labBottomBar
        '
        Me.labBottomBar.BackColor = System.Drawing.Color.Gainsboro
        Me.labBottomBar.Location = New System.Drawing.Point(11, 479)
        Me.labBottomBar.Name = "labBottomBar"
        Me.labBottomBar.Size = New System.Drawing.Size(106, 10)
        Me.labBottomBar.TabIndex = 29
        '
        'chkConfirmSetZeroQty
        '
        Me.chkConfirmSetZeroQty.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkConfirmSetZeroQty.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConfirmSetZeroQty.Location = New System.Drawing.Point(11, 299)
        Me.chkConfirmSetZeroQty.Name = "chkConfirmSetZeroQty"
        Me.chkConfirmSetZeroQty.Size = New System.Drawing.Size(112, 29)
        Me.chkConfirmSetZeroQty.TabIndex = 28
        Me.chkConfirmSetZeroQty.Text = "Confirm Each Setting Zero Stock"
        Me.chkConfirmSetZeroQty.UseVisualStyleBackColor = False
        '
        'labFreeRange
        '
        Me.labFreeRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.labFreeRange.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labFreeRange.ForeColor = System.Drawing.Color.Navy
        Me.labFreeRange.Location = New System.Drawing.Point(4, 7)
        Me.labFreeRange.Name = "labFreeRange"
        Me.labFreeRange.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labFreeRange.Size = New System.Drawing.Size(119, 148)
        Me.labFreeRange.TabIndex = 27
        Me.labFreeRange.Text = "Single (Free Range) Counting-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  -- Scan/Count items as needed." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  -- Commit wh" & _
    "en done. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  --  NO zeroing of uncounted items. "
        '
        'btnCountAllAsZero
        '
        Me.btnCountAllAsZero.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCountAllAsZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCountAllAsZero.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCountAllAsZero.ForeColor = System.Drawing.Color.Firebrick
        Me.btnCountAllAsZero.Location = New System.Drawing.Point(4, 170)
        Me.btnCountAllAsZero.Name = "btnCountAllAsZero"
        Me.btnCountAllAsZero.Size = New System.Drawing.Size(119, 112)
        Me.btnCountAllAsZero.TabIndex = 26
        Me.btnCountAllAsZero.Text = "If no stock found for remaining items, Click here to count them all as Zero.."
        Me.btnCountAllAsZero.UseVisualStyleBackColor = False
        '
        'cmdDestroy
        '
        Me.cmdDestroy.BackColor = System.Drawing.Color.LavenderBlush
        Me.cmdDestroy.CausesValidation = False
        Me.cmdDestroy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDestroy.Location = New System.Drawing.Point(31, 367)
        Me.cmdDestroy.Name = "cmdDestroy"
        Me.cmdDestroy.Size = New System.Drawing.Size(64, 30)
        Me.cmdDestroy.TabIndex = 7
        Me.cmdDestroy.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.cmdDestroy, "Cancel this Stocktake run forever.")
        Me.cmdDestroy.UseVisualStyleBackColor = False
        '
        'btnCommitAll
        '
        Me.btnCommitAll.BackColor = System.Drawing.Color.Lavender
        Me.btnCommitAll.CausesValidation = False
        Me.btnCommitAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCommitAll.Location = New System.Drawing.Point(31, 415)
        Me.btnCommitAll.Name = "btnCommitAll"
        Me.btnCommitAll.Size = New System.Drawing.Size(64, 41)
        Me.btnCommitAll.TabIndex = 6
        Me.btnCommitAll.Text = "Commit All"
        Me.btnCommitAll.UseVisualStyleBackColor = False
        '
        'labCanScan
        '
        Me.labCanScan.BackColor = System.Drawing.Color.Chartreuse
        Me.labCanScan.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labCanScan.Location = New System.Drawing.Point(164, 24)
        Me.labCanScan.Name = "labCanScan"
        Me.labCanScan.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.labCanScan.Size = New System.Drawing.Size(86, 23)
        Me.labCanScan.TabIndex = 5
        Me.labCanScan.Text = "Scan.."
        Me.labCanScan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labScanDescription
        '
        Me.labScanDescription.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labScanDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labScanDescription.ForeColor = System.Drawing.Color.MediumBlue
        Me.labScanDescription.Location = New System.Drawing.Point(208, 83)
        Me.labScanDescription.Name = "labScanDescription"
        Me.labScanDescription.Size = New System.Drawing.Size(244, 14)
        Me.labScanDescription.TabIndex = 4
        Me.labScanDescription.Text = "labScanDescription"
        '
        'labScanCat2
        '
        Me.labScanCat2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labScanCat2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labScanCat2.ForeColor = System.Drawing.Color.MediumBlue
        Me.labScanCat2.Location = New System.Drawing.Point(142, 83)
        Me.labScanCat2.Name = "labScanCat2"
        Me.labScanCat2.Size = New System.Drawing.Size(61, 15)
        Me.labScanCat2.TabIndex = 3
        Me.labScanCat2.Text = "labScanCat2"
        '
        'labScanCat1
        '
        Me.labScanCat1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labScanCat1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labScanCat1.ForeColor = System.Drawing.Color.MediumBlue
        Me.labScanCat1.Location = New System.Drawing.Point(80, 83)
        Me.labScanCat1.Name = "labScanCat1"
        Me.labScanCat1.Size = New System.Drawing.Size(58, 15)
        Me.labScanCat1.TabIndex = 2
        Me.labScanCat1.Text = "labScanCat1"
        '
        'txtScanBarcode
        '
        Me.txtScanBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtScanBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScanBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanBarcode.ForeColor = System.Drawing.Color.MediumBlue
        Me.txtScanBarcode.Location = New System.Drawing.Point(83, 51)
        Me.txtScanBarcode.MaxLength = 40
        Me.txtScanBarcode.Name = "txtScanBarcode"
        Me.txtScanBarcode.Size = New System.Drawing.Size(276, 23)
        Me.txtScanBarcode.TabIndex = 1
        Me.txtScanBarcode.Text = "txtScanBarcode"
        Me.ToolTip1.SetToolTip(Me.txtScanBarcode, "Scan or [Enter] Stock Barcode..")
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(15, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 22)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Barcode:"
        '
        'frameBrowseResults
        '
        Me.frameBrowseResults.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameBrowseResults.Controls.Add(Me.Label24)
        Me.frameBrowseResults.Controls.Add(Me.Label22)
        Me.frameBrowseResults.Controls.Add(Me.dgvResultsList)
        Me.frameBrowseResults.Controls.Add(Me.txtResultsFind)
        Me.frameBrowseResults.Controls.Add(Me.labResultsCount)
        Me.frameBrowseResults.Controls.Add(Me.LabResultsFind)
        Me.frameBrowseResults.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameBrowseResults.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameBrowseResults.Location = New System.Drawing.Point(3, 3)
        Me.frameBrowseResults.Name = "frameBrowseResults"
        Me.frameBrowseResults.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameBrowseResults.Size = New System.Drawing.Size(814, 295)
        Me.frameBrowseResults.TabIndex = 22
        Me.frameBrowseResults.TabStop = False
        Me.frameBrowseResults.Text = "FrameBrowseResults"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(8, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(135, 23)
        Me.Label24.TabIndex = 83
        Me.Label24.Text = "Stock Counted"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(687, 30)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(45, 15)
        Me.Label22.TabIndex = 82
        Me.Label22.Text = "Records."
        '
        'dgvResultsList
        '
        Me.dgvResultsList.AllowUserToAddRows = False
        Me.dgvResultsList.AllowUserToDeleteRows = False
        Me.dgvResultsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvResultsList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dgvResultsList.ColumnHeadersHeight = 18
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvResultsList.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvResultsList.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvResultsList.Location = New System.Drawing.Point(6, 64)
        Me.dgvResultsList.MultiSelect = False
        Me.dgvResultsList.Name = "dgvResultsList"
        Me.dgvResultsList.ReadOnly = True
        Me.dgvResultsList.RowHeadersWidth = 17
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvResultsList.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvResultsList.RowTemplate.Height = 19
        Me.dgvResultsList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvResultsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvResultsList.Size = New System.Drawing.Size(797, 214)
        Me.dgvResultsList.TabIndex = 4
        '
        'txtResultsFind
        '
        Me.txtResultsFind.AcceptsReturn = True
        Me.txtResultsFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtResultsFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtResultsFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtResultsFind.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResultsFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtResultsFind.Location = New System.Drawing.Point(232, 45)
        Me.txtResultsFind.MaxLength = 0
        Me.txtResultsFind.Name = "txtResultsFind"
        Me.txtResultsFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtResultsFind.Size = New System.Drawing.Size(133, 13)
        Me.txtResultsFind.TabIndex = 2
        Me.txtResultsFind.Text = "txtResultsFind"
        '
        'labResultsCount
        '
        Me.labResultsCount.BackColor = System.Drawing.Color.Transparent
        Me.labResultsCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labResultsCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labResultsCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labResultsCount.Location = New System.Drawing.Point(682, 13)
        Me.labResultsCount.Name = "labResultsCount"
        Me.labResultsCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labResultsCount.Size = New System.Drawing.Size(44, 14)
        Me.labResultsCount.TabIndex = 19
        Me.labResultsCount.Text = "labResultsCount"
        Me.labResultsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabResultsFind
        '
        Me.LabResultsFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.LabResultsFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabResultsFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabResultsFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabResultsFind.Location = New System.Drawing.Point(229, 15)
        Me.LabResultsFind.Name = "LabResultsFind"
        Me.LabResultsFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabResultsFind.Size = New System.Drawing.Size(132, 25)
        Me.LabResultsFind.TabIndex = 18
        Me.LabResultsFind.Text = "LabResultsFind"
        '
        'btnManualSave
        '
        Me.btnManualSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnManualSave.Location = New System.Drawing.Point(246, 26)
        Me.btnManualSave.Name = "btnManualSave"
        Me.btnManualSave.Size = New System.Drawing.Size(49, 23)
        Me.btnManualSave.TabIndex = 5
        Me.btnManualSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnManualSave, "Replace the total count for the count for this item.")
        Me.btnManualSave.UseVisualStyleBackColor = True
        '
        'btnManualCancel
        '
        Me.btnManualCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnManualCancel.Location = New System.Drawing.Point(246, 55)
        Me.btnManualCancel.Name = "btnManualCancel"
        Me.btnManualCancel.Size = New System.Drawing.Size(49, 23)
        Me.btnManualCancel.TabIndex = 6
        Me.btnManualCancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.btnManualCancel, "Cancel Manual Count for this Item")
        Me.btnManualCancel.UseVisualStyleBackColor = True
        '
        'txtManualExpected
        '
        Me.txtManualExpected.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtManualExpected.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtManualExpected.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualExpected.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.txtManualExpected.Location = New System.Drawing.Point(112, 56)
        Me.txtManualExpected.MaxLength = 5
        Me.txtManualExpected.Name = "txtManualExpected"
        Me.txtManualExpected.ReadOnly = True
        Me.txtManualExpected.Size = New System.Drawing.Size(54, 14)
        Me.txtManualExpected.TabIndex = 9
        Me.txtManualExpected.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtManualExpected, "Add to Count")
        '
        'txtManualQty
        '
        Me.txtManualQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtManualQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtManualQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManualQty.Location = New System.Drawing.Point(112, 30)
        Me.txtManualQty.MaxLength = 5
        Me.txtManualQty.Name = "txtManualQty"
        Me.txtManualQty.Size = New System.Drawing.Size(54, 14)
        Me.txtManualQty.TabIndex = 3
        Me.txtManualQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtManualQty, "Add to Count")
        '
        'btnAutoUndo
        '
        Me.btnAutoUndo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnAutoUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAutoUndo.Location = New System.Drawing.Point(698, 144)
        Me.btnAutoUndo.Name = "btnAutoUndo"
        Me.btnAutoUndo.Size = New System.Drawing.Size(88, 23)
        Me.btnAutoUndo.TabIndex = 12
        Me.btnAutoUndo.Text = "Undo"
        Me.ToolTip1.SetToolTip(Me.btnAutoUndo, "Undo last Auto-counted line..")
        Me.btnAutoUndo.UseVisualStyleBackColor = False
        '
        'txtScanBarcodeManual
        '
        Me.txtScanBarcodeManual.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtScanBarcodeManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScanBarcodeManual.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScanBarcodeManual.ForeColor = System.Drawing.Color.Indigo
        Me.txtScanBarcodeManual.Location = New System.Drawing.Point(83, 54)
        Me.txtScanBarcodeManual.MaxLength = 40
        Me.txtScanBarcodeManual.Name = "txtScanBarcodeManual"
        Me.txtScanBarcodeManual.Size = New System.Drawing.Size(276, 23)
        Me.txtScanBarcodeManual.TabIndex = 16
        Me.txtScanBarcodeManual.Text = "txtScanBarcodeManual"
        Me.ToolTip1.SetToolTip(Me.txtScanBarcodeManual, "Scan or [Enter] Stock Barcode..")
        '
        'btnAddToCount
        '
        Me.btnAddToCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddToCount.Location = New System.Drawing.Point(185, 26)
        Me.btnAddToCount.Name = "btnAddToCount"
        Me.btnAddToCount.Size = New System.Drawing.Size(49, 23)
        Me.btnAddToCount.TabIndex = 4
        Me.btnAddToCount.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.btnAddToCount, "Add to the total count for for this item.")
        Me.btnAddToCount.UseVisualStyleBackColor = True
        '
        'grpBoxScanAuto
        '
        Me.grpBoxScanAuto.Controls.Add(Me.labExplainScanning)
        Me.grpBoxScanAuto.Controls.Add(Me.Label9)
        Me.grpBoxScanAuto.Controls.Add(Me.btnLookupStock)
        Me.grpBoxScanAuto.Controls.Add(Me.labScanCat2)
        Me.grpBoxScanAuto.Controls.Add(Me.labScanCat1)
        Me.grpBoxScanAuto.Controls.Add(Me.labScanDescription)
        Me.grpBoxScanAuto.Controls.Add(Me.labCanScan)
        Me.grpBoxScanAuto.Controls.Add(Me.Label6)
        Me.grpBoxScanAuto.Controls.Add(Me.txtScanBarcode)
        Me.grpBoxScanAuto.Location = New System.Drawing.Point(4, 7)
        Me.grpBoxScanAuto.Name = "grpBoxScanAuto"
        Me.grpBoxScanAuto.Size = New System.Drawing.Size(813, 112)
        Me.grpBoxScanAuto.TabIndex = 11
        Me.grpBoxScanAuto.TabStop = False
        Me.grpBoxScanAuto.Text = "Scanning Barcodes"
        '
        'labExplainScanning
        '
        Me.labExplainScanning.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labExplainScanning.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labExplainScanning.Location = New System.Drawing.Point(527, 16)
        Me.labExplainScanning.Name = "labExplainScanning"
        Me.labExplainScanning.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.labExplainScanning.Size = New System.Drawing.Size(266, 79)
        Me.labExplainScanning.TabIndex = 12
        Me.labExplainScanning.Text = "Auto-counting as you scan Items barcodes..  Make sure the Focus is on the Barcode" & _
    " textbox before scanning the next item..     [Enter] key is needed if keyboardin" & _
    "g code.."
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label9.Location = New System.Drawing.Point(14, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(130, 19)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Auto Counting"
        '
        'btnLookupStock
        '
        Me.btnLookupStock.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnLookupStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLookupStock.Location = New System.Drawing.Point(376, 49)
        Me.btnLookupStock.Name = "btnLookupStock"
        Me.btnLookupStock.Size = New System.Drawing.Size(76, 25)
        Me.btnLookupStock.TabIndex = 6
        Me.btnLookupStock.TabStop = False
        Me.btnLookupStock.Text = "Lookup >>"
        Me.btnLookupStock.UseVisualStyleBackColor = False
        '
        'TabPageManual
        '
        Me.TabPageManual.BackColor = System.Drawing.Color.Maroon
        Me.TabPageManual.Controls.Add(Me.grpBoxManual)
        Me.TabPageManual.Location = New System.Drawing.Point(4, 28)
        Me.TabPageManual.Name = "TabPageManual"
        Me.TabPageManual.Size = New System.Drawing.Size(863, 472)
        Me.TabPageManual.TabIndex = 3
        Me.TabPageManual.Text = "Results && Manual Count."
        '
        'grpBoxManual
        '
        Me.grpBoxManual.BackColor = System.Drawing.Color.White
        Me.grpBoxManual.Controls.Add(Me.grpBoxScanManual)
        Me.grpBoxManual.Controls.Add(Me.TabControlResults)
        Me.grpBoxManual.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxManual.Location = New System.Drawing.Point(1, 1)
        Me.grpBoxManual.Name = "grpBoxManual"
        Me.grpBoxManual.Size = New System.Drawing.Size(852, 460)
        Me.grpBoxManual.TabIndex = 15
        Me.grpBoxManual.TabStop = False
        Me.grpBoxManual.Text = "grpBoxManual"
        '
        'grpBoxScanManual
        '
        Me.grpBoxScanManual.Controls.Add(Me.labScanManualCat2)
        Me.grpBoxScanManual.Controls.Add(Me.labScanManualCat1)
        Me.grpBoxScanManual.Controls.Add(Me.labScanManualDescription)
        Me.grpBoxScanManual.Controls.Add(Me.Label21)
        Me.grpBoxScanManual.Controls.Add(Me.txtScanBarcodeManual)
        Me.grpBoxScanManual.Controls.Add(Me.Label16)
        Me.grpBoxScanManual.Controls.Add(Me.labExplainManual)
        Me.grpBoxScanManual.Controls.Add(Me.grpBoxManualCount)
        Me.grpBoxScanManual.Location = New System.Drawing.Point(4, 7)
        Me.grpBoxScanManual.Name = "grpBoxScanManual"
        Me.grpBoxScanManual.Size = New System.Drawing.Size(831, 112)
        Me.grpBoxScanManual.TabIndex = 24
        Me.grpBoxScanManual.TabStop = False
        Me.grpBoxScanManual.Text = "grpBoxScanManual"
        '
        'labScanManualCat2
        '
        Me.labScanManualCat2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labScanManualCat2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labScanManualCat2.ForeColor = System.Drawing.Color.Indigo
        Me.labScanManualCat2.Location = New System.Drawing.Point(142, 83)
        Me.labScanManualCat2.Name = "labScanManualCat2"
        Me.labScanManualCat2.Size = New System.Drawing.Size(61, 15)
        Me.labScanManualCat2.TabIndex = 18
        Me.labScanManualCat2.Text = "labScanManualCat2"
        '
        'labScanManualCat1
        '
        Me.labScanManualCat1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labScanManualCat1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labScanManualCat1.ForeColor = System.Drawing.Color.Indigo
        Me.labScanManualCat1.Location = New System.Drawing.Point(80, 83)
        Me.labScanManualCat1.Name = "labScanManualCat1"
        Me.labScanManualCat1.Size = New System.Drawing.Size(58, 15)
        Me.labScanManualCat1.TabIndex = 17
        Me.labScanManualCat1.Text = "labScanManualCat1"
        '
        'labScanManualDescription
        '
        Me.labScanManualDescription.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labScanManualDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labScanManualDescription.ForeColor = System.Drawing.Color.Indigo
        Me.labScanManualDescription.Location = New System.Drawing.Point(208, 83)
        Me.labScanManualDescription.Name = "labScanManualDescription"
        Me.labScanManualDescription.Size = New System.Drawing.Size(269, 14)
        Me.labScanManualDescription.TabIndex = 19
        Me.labScanManualDescription.Text = "labScanManualDescription"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(15, 54)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(59, 22)
        Me.Label21.TabIndex = 15
        Me.Label21.Text = "Barcode:"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.DarkViolet
        Me.Label16.Location = New System.Drawing.Point(14, 22)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(152, 19)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "Manual Counting"
        '
        'labExplainManual
        '
        Me.labExplainManual.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labExplainManual.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labExplainManual.Location = New System.Drawing.Point(191, 13)
        Me.labExplainManual.Name = "labExplainManual"
        Me.labExplainManual.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.labExplainManual.Size = New System.Drawing.Size(292, 31)
        Me.labExplainManual.TabIndex = 14
        Me.labExplainManual.Text = "Manual Entry- Scan the product Barcode. or double-click on Item in Grid..  Then e" & _
    "nter the qty counted."
        '
        'grpBoxManualCount
        '
        Me.grpBoxManualCount.Controls.Add(Me.Label3)
        Me.grpBoxManualCount.Controls.Add(Me.txtLastManualBarcode)
        Me.grpBoxManualCount.Controls.Add(Me.btnAddToCount)
        Me.grpBoxManualCount.Controls.Add(Me.txtManualQty)
        Me.grpBoxManualCount.Controls.Add(Me.txtManualExpected)
        Me.grpBoxManualCount.Controls.Add(Me.picmanualWaiting)
        Me.grpBoxManualCount.Controls.Add(Me.Label10)
        Me.grpBoxManualCount.Controls.Add(Me.btnManualCancel)
        Me.grpBoxManualCount.Controls.Add(Me.btnManualSave)
        Me.grpBoxManualCount.Controls.Add(Me.Label7)
        Me.grpBoxManualCount.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxManualCount.Location = New System.Drawing.Point(516, 8)
        Me.grpBoxManualCount.Name = "grpBoxManualCount"
        Me.grpBoxManualCount.Size = New System.Drawing.Size(306, 101)
        Me.grpBoxManualCount.TabIndex = 12
        Me.grpBoxManualCount.TabStop = False
        Me.grpBoxManualCount.Text = "Manual Count Entry"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 16)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Last scan:"
        '
        'txtLastManualBarcode
        '
        Me.txtLastManualBarcode.BackColor = System.Drawing.SystemColors.MenuBar
        Me.txtLastManualBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLastManualBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastManualBarcode.Location = New System.Drawing.Point(70, 77)
        Me.txtLastManualBarcode.Name = "txtLastManualBarcode"
        Me.txtLastManualBarcode.ReadOnly = True
        Me.txtLastManualBarcode.Size = New System.Drawing.Size(164, 15)
        Me.txtLastManualBarcode.TabIndex = 12
        Me.txtLastManualBarcode.TabStop = False
        '
        'picmanualWaiting
        '
        Me.picmanualWaiting.BackColor = System.Drawing.Color.Transparent
        Me.picmanualWaiting.Image = CType(resources.GetObject("picmanualWaiting.Image"), System.Drawing.Image)
        Me.picmanualWaiting.Location = New System.Drawing.Point(11, 25)
        Me.picmanualWaiting.Name = "picmanualWaiting"
        Me.picmanualWaiting.Size = New System.Drawing.Size(20, 20)
        Me.picmanualWaiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picmanualWaiting.TabIndex = 11
        Me.picmanualWaiting.TabStop = False
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label10.Location = New System.Drawing.Point(54, 56)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 21)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Expected:"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Indigo
        Me.Label7.Location = New System.Drawing.Point(47, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 29)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Enter qty counted:"
        '
        'TabControlResults
        '
        Me.TabControlResults.Controls.Add(Me.TabPageCounted)
        Me.TabControlResults.Controls.Add(Me.TabPageUncounted)
        Me.TabControlResults.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlResults.Location = New System.Drawing.Point(5, 121)
        Me.TabControlResults.Name = "TabControlResults"
        Me.TabControlResults.SelectedIndex = 0
        Me.TabControlResults.Size = New System.Drawing.Size(844, 333)
        Me.TabControlResults.TabIndex = 23
        '
        'TabPageCounted
        '
        Me.TabPageCounted.BackColor = System.Drawing.Color.Thistle
        Me.TabPageCounted.Controls.Add(Me.frameBrowseResults)
        Me.TabPageCounted.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageCounted.Location = New System.Drawing.Point(4, 25)
        Me.TabPageCounted.Name = "TabPageCounted"
        Me.TabPageCounted.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCounted.Size = New System.Drawing.Size(836, 304)
        Me.TabPageCounted.TabIndex = 0
        Me.TabPageCounted.Text = "Stock Lines Counted"
        '
        'TabPageUncounted
        '
        Me.TabPageUncounted.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.TabPageUncounted.Controls.Add(Me.frameBrowseUncounted)
        Me.TabPageUncounted.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageUncounted.Location = New System.Drawing.Point(4, 25)
        Me.TabPageUncounted.Name = "TabPageUncounted"
        Me.TabPageUncounted.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageUncounted.Size = New System.Drawing.Size(836, 304)
        Me.TabPageUncounted.TabIndex = 1
        Me.TabPageUncounted.Text = "Stock Lines Uncounted"
        '
        'frameBrowseUncounted
        '
        Me.frameBrowseUncounted.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameBrowseUncounted.Controls.Add(Me.Label15)
        Me.frameBrowseUncounted.Controls.Add(Me.Label11)
        Me.frameBrowseUncounted.Controls.Add(Me.Label14)
        Me.frameBrowseUncounted.Controls.Add(Me.dgvUncounted)
        Me.frameBrowseUncounted.Controls.Add(Me.txtUncountedFind)
        Me.frameBrowseUncounted.Controls.Add(Me.labUncountedCount)
        Me.frameBrowseUncounted.Controls.Add(Me.labUncountedFind)
        Me.frameBrowseUncounted.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameBrowseUncounted.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameBrowseUncounted.Location = New System.Drawing.Point(3, 3)
        Me.frameBrowseUncounted.Name = "frameBrowseUncounted"
        Me.frameBrowseUncounted.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameBrowseUncounted.Size = New System.Drawing.Size(803, 295)
        Me.frameBrowseUncounted.TabIndex = 23
        Me.frameBrowseUncounted.TabStop = False
        Me.frameBrowseUncounted.Text = "frameBrowseUncounted"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(412, 16)
        Me.Label15.Name = "Label15"
        Me.Label15.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.Label15.Size = New System.Drawing.Size(264, 42)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "This Grid shows Stock on file but not yet counted..   Items will disappear from t" & _
    "his grid into the Results grid as they are counted."
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Khaki
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(135, 23)
        Me.Label11.TabIndex = 83
        Me.Label11.Text = "Still UnCounted"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(687, 30)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(45, 15)
        Me.Label14.TabIndex = 82
        Me.Label14.Text = "Records."
        '
        'dgvUncounted
        '
        Me.dgvUncounted.AllowUserToAddRows = False
        Me.dgvUncounted.AllowUserToDeleteRows = False
        Me.dgvUncounted.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvUncounted.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(237, Byte), Integer))
        Me.dgvUncounted.ColumnHeadersHeight = 18
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUncounted.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvUncounted.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvUncounted.Location = New System.Drawing.Point(6, 64)
        Me.dgvUncounted.MultiSelect = False
        Me.dgvUncounted.Name = "dgvUncounted"
        Me.dgvUncounted.ReadOnly = True
        Me.dgvUncounted.RowHeadersWidth = 17
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvUncounted.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvUncounted.RowTemplate.Height = 19
        Me.dgvUncounted.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvUncounted.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUncounted.Size = New System.Drawing.Size(774, 208)
        Me.dgvUncounted.TabIndex = 4
        '
        'txtUncountedFind
        '
        Me.txtUncountedFind.AcceptsReturn = True
        Me.txtUncountedFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtUncountedFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUncountedFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUncountedFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUncountedFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUncountedFind.Location = New System.Drawing.Point(232, 45)
        Me.txtUncountedFind.MaxLength = 0
        Me.txtUncountedFind.Name = "txtUncountedFind"
        Me.txtUncountedFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUncountedFind.Size = New System.Drawing.Size(133, 13)
        Me.txtUncountedFind.TabIndex = 2
        Me.txtUncountedFind.Text = "txtUncountedFind"
        '
        'labUncountedCount
        '
        Me.labUncountedCount.BackColor = System.Drawing.Color.Transparent
        Me.labUncountedCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labUncountedCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labUncountedCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labUncountedCount.Location = New System.Drawing.Point(682, 13)
        Me.labUncountedCount.Name = "labUncountedCount"
        Me.labUncountedCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labUncountedCount.Size = New System.Drawing.Size(44, 13)
        Me.labUncountedCount.TabIndex = 19
        Me.labUncountedCount.Text = "labUncountedCount"
        Me.labUncountedCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'labUncountedFind
        '
        Me.labUncountedFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.labUncountedFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.labUncountedFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labUncountedFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labUncountedFind.Location = New System.Drawing.Point(229, 15)
        Me.labUncountedFind.Name = "labUncountedFind"
        Me.labUncountedFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labUncountedFind.Size = New System.Drawing.Size(132, 25)
        Me.labUncountedFind.TabIndex = 18
        Me.labUncountedFind.Text = "labUncountedFind"
        '
        'TabPageAuto
        '
        Me.TabPageAuto.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.TabPageAuto.Controls.Add(Me.grpBoxAuto)
        Me.TabPageAuto.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageAuto.Location = New System.Drawing.Point(4, 28)
        Me.TabPageAuto.Name = "TabPageAuto"
        Me.TabPageAuto.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAuto.Size = New System.Drawing.Size(863, 472)
        Me.TabPageAuto.TabIndex = 0
        Me.TabPageAuto.Text = "Auto Counting"
        '
        'grpBoxAuto
        '
        Me.grpBoxAuto.BackColor = System.Drawing.Color.White
        Me.grpBoxAuto.Controls.Add(Me.grpBoxScanAuto)
        Me.grpBoxAuto.Controls.Add(Me.labAutoInfo)
        Me.grpBoxAuto.Controls.Add(Me.btnAutoUndo)
        Me.grpBoxAuto.Controls.Add(Me.dgvAutoCountItems)
        Me.grpBoxAuto.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxAuto.Location = New System.Drawing.Point(1, 1)
        Me.grpBoxAuto.Name = "grpBoxAuto"
        Me.grpBoxAuto.Size = New System.Drawing.Size(831, 463)
        Me.grpBoxAuto.TabIndex = 8
        Me.grpBoxAuto.TabStop = False
        Me.grpBoxAuto.Text = "grpBoxAuto"
        '
        'labAutoInfo
        '
        Me.labAutoInfo.BackColor = System.Drawing.Color.Khaki
        Me.labAutoInfo.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labAutoInfo.Location = New System.Drawing.Point(696, 186)
        Me.labAutoInfo.Name = "labAutoInfo"
        Me.labAutoInfo.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.labAutoInfo.Size = New System.Drawing.Size(121, 271)
        Me.labAutoInfo.TabIndex = 10
        Me.labAutoInfo.Text = "Only the immediate Counting History is Shown here..     Items are added to the Re" & _
    "ad-only Grid as they are scanned and counted...-"
        '
        'dgvAutoCountItems
        '
        Me.dgvAutoCountItems.AllowUserToAddRows = False
        Me.dgvAutoCountItems.AllowUserToDeleteRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvAutoCountItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvAutoCountItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvAutoCountItems.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.GhostWhite
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvAutoCountItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvAutoCountItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Cat1, Me.Cat2, Me.Description, Me.Barcode, Me.Stock_id, Me.Auto_Counted, Me.qty_on_record, Me.qty_counted, Me.qty_difference})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAutoCountItems.DefaultCellStyle = DataGridViewCellStyle9
        Me.dgvAutoCountItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvAutoCountItems.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvAutoCountItems.Location = New System.Drawing.Point(2, 125)
        Me.dgvAutoCountItems.MultiSelect = False
        Me.dgvAutoCountItems.Name = "dgvAutoCountItems"
        Me.dgvAutoCountItems.ReadOnly = True
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAutoCountItems.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.dgvAutoCountItems.RowHeadersWidth = 45
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvAutoCountItems.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.dgvAutoCountItems.RowTemplate.Height = 17
        Me.dgvAutoCountItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAutoCountItems.Size = New System.Drawing.Size(680, 332)
        Me.dgvAutoCountItems.TabIndex = 5
        '
        'Cat1
        '
        Me.Cat1.FillWeight = 40.0!
        Me.Cat1.HeaderText = "Cat1"
        Me.Cat1.Name = "Cat1"
        Me.Cat1.ReadOnly = True
        '
        'Cat2
        '
        Me.Cat2.FillWeight = 40.0!
        Me.Cat2.HeaderText = "Cat2"
        Me.Cat2.Name = "Cat2"
        Me.Cat2.ReadOnly = True
        '
        'Description
        '
        Me.Description.FillWeight = 150.0!
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        '
        'Barcode
        '
        Me.Barcode.FillWeight = 80.0!
        Me.Barcode.HeaderText = "Barcode"
        Me.Barcode.Name = "Barcode"
        Me.Barcode.ReadOnly = True
        '
        'Stock_id
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Stock_id.DefaultCellStyle = DataGridViewCellStyle7
        Me.Stock_id.FillWeight = 30.0!
        Me.Stock_id.HeaderText = "Stock_id"
        Me.Stock_id.Name = "Stock_id"
        Me.Stock_id.ReadOnly = True
        '
        'Auto_Counted
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Auto_Counted.DefaultCellStyle = DataGridViewCellStyle8
        Me.Auto_Counted.FillWeight = 40.0!
        Me.Auto_Counted.HeaderText = "AutoCount"
        Me.Auto_Counted.Name = "Auto_Counted"
        Me.Auto_Counted.ReadOnly = True
        '
        'qty_on_record
        '
        Me.qty_on_record.HeaderText = "qty_on_record"
        Me.qty_on_record.Name = "qty_on_record"
        Me.qty_on_record.ReadOnly = True
        Me.qty_on_record.Visible = False
        '
        'qty_counted
        '
        Me.qty_counted.HeaderText = "qty_counted"
        Me.qty_counted.Name = "qty_counted"
        Me.qty_counted.ReadOnly = True
        Me.qty_counted.Visible = False
        '
        'qty_difference
        '
        Me.qty_difference.HeaderText = "qty_difference"
        Me.qty_difference.Name = "qty_difference"
        Me.qty_difference.ReadOnly = True
        Me.qty_difference.Visible = False
        '
        'TabControlMain
        '
        Me.TabControlMain.Controls.Add(Me.TabPageAuto)
        Me.TabControlMain.Controls.Add(Me.TabPageManual)
        Me.TabControlMain.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlMain.Location = New System.Drawing.Point(3, 134)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(871, 504)
        Me.TabControlMain.TabIndex = 10
        '
        'chkDoNotPreLoadNegItems
        '
        Me.chkDoNotPreLoadNegItems.BackColor = System.Drawing.Color.Lavender
        Me.chkDoNotPreLoadNegItems.Location = New System.Drawing.Point(131, 85)
        Me.chkDoNotPreLoadNegItems.Name = "chkDoNotPreLoadNegItems"
        Me.chkDoNotPreLoadNegItems.Size = New System.Drawing.Size(105, 33)
        Me.chkDoNotPreLoadNegItems.TabIndex = 17
        Me.chkDoNotPreLoadNegItems.Text = "Do Not PreLoad Negative Items"
        Me.ToolTip1.SetToolTip(Me.chkDoNotPreLoadNegItems, "Do Not PreLoad Stock Items with Negative Qty in Stock..")
        Me.chkDoNotPreLoadNegItems.UseVisualStyleBackColor = False
        '
        'ucChildStocktake
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.TabControlMain)
        Me.Controls.Add(Me.panelFooter)
        Me.Controls.Add(Me.panelHdr)
        Me.Controls.Add(Me.panelTopBanner)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildStocktake"
        Me.Size = New System.Drawing.Size(1008, 646)
        Me.panelHdr.ResumeLayout(False)
        Me.panelHdr.PerformLayout()
        Me.TabControlReport.ResumeLayout(False)
        Me.TabPageReport.ResumeLayout(False)
        Me.TabPageReport.PerformLayout()
        Me.TabPagePrint.ResumeLayout(False)
        Me.grpBoxPrint.ResumeLayout(False)
        Me.grpBoxType.ResumeLayout(False)
        Me.grpBoxType.PerformLayout()
        Me.panelFooter.ResumeLayout(False)
        Me.frameBrowseResults.ResumeLayout(False)
        Me.frameBrowseResults.PerformLayout()
        CType(Me.dgvResultsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxScanAuto.ResumeLayout(False)
        Me.grpBoxScanAuto.PerformLayout()
        Me.TabPageManual.ResumeLayout(False)
        Me.grpBoxManual.ResumeLayout(False)
        Me.grpBoxScanManual.ResumeLayout(False)
        Me.grpBoxScanManual.PerformLayout()
        Me.grpBoxManualCount.ResumeLayout(False)
        Me.grpBoxManualCount.PerformLayout()
        CType(Me.picmanualWaiting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlResults.ResumeLayout(False)
        Me.TabPageCounted.ResumeLayout(False)
        Me.TabPageUncounted.ResumeLayout(False)
        Me.frameBrowseUncounted.ResumeLayout(False)
        Me.frameBrowseUncounted.PerformLayout()
        CType(Me.dgvUncounted, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageAuto.ResumeLayout(False)
        Me.grpBoxAuto.ResumeLayout(False)
        CType(Me.dgvAutoCountItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelTopBanner As System.Windows.Forms.Panel
    Friend WithEvents panelHdr As System.Windows.Forms.Panel
    Friend WithEvents panelFooter As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents labHdrDateCreated As System.Windows.Forms.Label
    Friend WithEvents labHdr1 As System.Windows.Forms.Label
    Friend WithEvents txtScanBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents labScanDescription As System.Windows.Forms.Label
    Friend WithEvents labScanCat2 As System.Windows.Forms.Label
    Friend WithEvents labScanCat1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents labChooseType As System.Windows.Forms.Label
    Friend WithEvents btnCommitAll As System.Windows.Forms.Button
    Public WithEvents frameBrowseResults As System.Windows.Forms.GroupBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents dgvResultsList As System.Windows.Forms.DataGridView
    Public WithEvents txtResultsFind As System.Windows.Forms.TextBox
    Public WithEvents labResultsCount As System.Windows.Forms.Label
    Public WithEvents LabResultsFind As System.Windows.Forms.Label
    Friend WithEvents txtReport As System.Windows.Forms.TextBox
    Friend WithEvents labStockTakeId As System.Windows.Forms.Label
    Friend WithEvents labCanScan As System.Windows.Forms.Label
    Friend WithEvents grpBoxScanAuto As System.Windows.Forms.GroupBox
    Friend WithEvents labExplainScanning As System.Windows.Forms.Label
    Friend WithEvents btnLookupStock As System.Windows.Forms.Button
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TabPageManual As System.Windows.Forms.TabPage
    Friend WithEvents grpBoxManual As System.Windows.Forms.GroupBox
    Friend WithEvents labExplainManual As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents grpBoxManualCount As System.Windows.Forms.GroupBox
    Friend WithEvents txtManualQty As System.Windows.Forms.TextBox
    Friend WithEvents txtManualExpected As System.Windows.Forms.TextBox
    Friend WithEvents picmanualWaiting As System.Windows.Forms.PictureBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnManualCancel As System.Windows.Forms.Button
    Friend WithEvents btnManualSave As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TabPageAuto As System.Windows.Forms.TabPage
    Friend WithEvents grpBoxAuto As System.Windows.Forms.GroupBox
    Friend WithEvents btnAutoUndo As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents labAutoInfo As System.Windows.Forms.Label
    Friend WithEvents dgvAutoCountItems As System.Windows.Forms.DataGridView
    Friend WithEvents Cat1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cat2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Stock_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Auto_Counted As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents qty_on_record As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents qty_counted As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents qty_difference As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents TabControlResults As System.Windows.Forms.TabControl
    Friend WithEvents TabPageCounted As System.Windows.Forms.TabPage
    Friend WithEvents TabPageUncounted As System.Windows.Forms.TabPage
    Public WithEvents frameBrowseUncounted As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dgvUncounted As System.Windows.Forms.DataGridView
    Public WithEvents txtUncountedFind As System.Windows.Forms.TextBox
    Public WithEvents labUncountedCount As System.Windows.Forms.Label
    Public WithEvents labUncountedFind As System.Windows.Forms.Label
    Friend WithEvents cmdDestroy As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents grpBoxScanManual As System.Windows.Forms.GroupBox
    Friend WithEvents labScanManualCat2 As System.Windows.Forms.Label
    Friend WithEvents labScanManualCat1 As System.Windows.Forms.Label
    Friend WithEvents labScanManualDescription As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtScanBarcodeManual As System.Windows.Forms.TextBox
    Friend WithEvents cboPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents optStocktakeType_full As System.Windows.Forms.RadioButton
    Friend WithEvents optStocktakeType_partial As System.Windows.Forms.RadioButton
    Friend WithEvents optStocktakeType_single As System.Windows.Forms.RadioButton
    Friend WithEvents btnFullOK As System.Windows.Forms.Button
    Friend WithEvents grpBoxPrint As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents grpBoxType As System.Windows.Forms.GroupBox
    Friend WithEvents btnCountAllAsZero As System.Windows.Forms.Button
    Friend WithEvents labCountedCount As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkKeepScannedLeadZeroes As System.Windows.Forms.CheckBox
    Friend WithEvents btnAddToCount As System.Windows.Forms.Button
    Friend WithEvents btnCatSelect As System.Windows.Forms.Button
    Friend WithEvents txtPartialSelection As System.Windows.Forms.TextBox
    Friend WithEvents labFreeRange As System.Windows.Forms.Label
    Friend WithEvents TabControlReport As System.Windows.Forms.TabControl
    Friend WithEvents TabPageReport As System.Windows.Forms.TabPage
    Friend WithEvents TabPagePrint As System.Windows.Forms.TabPage
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLastManualBarcode As System.Windows.Forms.TextBox
    Friend WithEvents chkConfirmSetZeroQty As System.Windows.Forms.CheckBox
    Friend WithEvents labBottomBar As System.Windows.Forms.Label
    Friend WithEvents chkDoNotPreLoadNegItems As System.Windows.Forms.CheckBox
End Class
