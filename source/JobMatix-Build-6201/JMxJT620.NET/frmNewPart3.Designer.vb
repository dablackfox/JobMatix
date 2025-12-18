<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmNewPart
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents txtSellInclGST As System.Windows.Forms.TextBox
	Public WithEvents txtDescription As System.Windows.Forms.TextBox
	Public WithEvents ChkAllowRenaming As System.Windows.Forms.CheckBox
	Public WithEvents LabVerify As System.Windows.Forms.Label
	Public WithEvents PicVerify As System.Windows.Forms.Panel
	Public WithEvents txtSerialNo As System.Windows.Forms.TextBox
	Public WithEvents cboQty As System.Windows.Forms.ComboBox
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdOk As System.Windows.Forms.Button
	Public WithEvents cmdLookup As System.Windows.Forms.Button
	Public WithEvents chkWarranty As System.Windows.Forms.CheckBox
	Public WithEvents txtPartNo As System.Windows.Forms.TextBox
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents LabChangePrice As System.Windows.Forms.Label
	Public WithEvents LabEnterSerialNo As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents LabDescription As System.Windows.Forms.Label
	Public WithEvents LabScanProduct As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSelect = New System.Windows.Forms.Button()
        Me.cmdClearStockSearch = New System.Windows.Forms.Button()
        Me.cmdStockSearch = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSellInclGST = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.ChkAllowRenaming = New System.Windows.Forms.CheckBox()
        Me.PicVerify = New System.Windows.Forms.Panel()
        Me.LabVerify = New System.Windows.Forms.Label()
        Me.txtSerialNo = New System.Windows.Forms.TextBox()
        Me.cboQty = New System.Windows.Forms.ComboBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdLookup = New System.Windows.Forms.Button()
        Me.chkWarranty = New System.Windows.Forms.CheckBox()
        Me.txtPartNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabChangePrice = New System.Windows.Forms.Label()
        Me.LabEnterSerialNo = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabDescription = New System.Windows.Forms.Label()
        Me.LabScanProduct = New System.Windows.Forms.Label()
        Me.LabHdr1 = New System.Windows.Forms.Label()
        Me.FrameBrowse = New System.Windows.Forms.GroupBox()
        Me.txtStockSearch = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.labRecCount = New System.Windows.Forms.Label()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkKeepScannedLeadZeroes = New System.Windows.Forms.CheckBox()
        Me.PicVerify.SuspendLayout()
        Me.FrameBrowse.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSelect
        '
        Me.cmdSelect.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSelect.Font = New System.Drawing.Font("Lucida Sans", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelect.Location = New System.Drawing.Point(477, 111)
        Me.cmdSelect.Name = "cmdSelect"
        Me.cmdSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelect.Size = New System.Drawing.Size(53, 21)
        Me.cmdSelect.TabIndex = 3
        Me.cmdSelect.Text = "OK"
        Me.ToolTip1.SetToolTip(Me.cmdSelect, "Select current item..")
        Me.cmdSelect.UseVisualStyleBackColor = False
        '
        'cmdClearStockSearch
        '
        Me.cmdClearStockSearch.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdClearStockSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearStockSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClearStockSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearStockSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearStockSearch.Location = New System.Drawing.Point(477, 22)
        Me.cmdClearStockSearch.Name = "cmdClearStockSearch"
        Me.cmdClearStockSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearStockSearch.Size = New System.Drawing.Size(53, 23)
        Me.cmdClearStockSearch.TabIndex = 80
        Me.cmdClearStockSearch.Text = "Clear"
        Me.ToolTip1.SetToolTip(Me.cmdClearStockSearch, "Clear Search Text and refresh grid..")
        Me.cmdClearStockSearch.UseVisualStyleBackColor = False
        '
        'cmdStockSearch
        '
        Me.cmdStockSearch.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdStockSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdStockSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdStockSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStockSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdStockSearch.Location = New System.Drawing.Point(410, 22)
        Me.cmdStockSearch.Name = "cmdStockSearch"
        Me.cmdStockSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdStockSearch.Size = New System.Drawing.Size(53, 23)
        Me.cmdStockSearch.TabIndex = 79
        Me.cmdStockSearch.Text = "Search"
        Me.cmdStockSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdStockSearch, "Search/Refresh customer list.")
        Me.cmdStockSearch.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(248, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 12)
        Me.Label5.TabIndex = 81
        Me.Label5.Text = "Full Text Search"
        Me.ToolTip1.SetToolTip(Me.Label5, "Search stock items for text fragments..  No asterisks please..")
        '
        'txtSellInclGST
        '
        Me.txtSellInclGST.AcceptsReturn = True
        Me.txtSellInclGST.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSellInclGST.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSellInclGST.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSellInclGST.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellInclGST.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSellInclGST.Location = New System.Drawing.Point(99, 488)
        Me.txtSellInclGST.MaxLength = 9
        Me.txtSellInclGST.Name = "txtSellInclGST"
        Me.txtSellInclGST.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSellInclGST.Size = New System.Drawing.Size(81, 15)
        Me.txtSellInclGST.TabIndex = 9
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = True
        Me.txtDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescription.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescription.Location = New System.Drawing.Point(23, 446)
        Me.txtDescription.MaxLength = 50
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescription.Size = New System.Drawing.Size(369, 14)
        Me.txtDescription.TabIndex = 8
        '
        'ChkAllowRenaming
        '
        Me.ChkAllowRenaming.BackColor = System.Drawing.Color.Transparent
        Me.ChkAllowRenaming.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkAllowRenaming.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAllowRenaming.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkAllowRenaming.Location = New System.Drawing.Point(23, 391)
        Me.ChkAllowRenaming.Name = "ChkAllowRenaming"
        Me.ChkAllowRenaming.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkAllowRenaming.Size = New System.Drawing.Size(137, 28)
        Me.ChkAllowRenaming.TabIndex = 7
        Me.ChkAllowRenaming.Text = "Description and Selling Price can be changed"
        Me.ChkAllowRenaming.UseVisualStyleBackColor = False
        '
        'PicVerify
        '
        Me.PicVerify.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.PicVerify.Controls.Add(Me.LabVerify)
        Me.PicVerify.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicVerify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PicVerify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PicVerify.Location = New System.Drawing.Point(411, 314)
        Me.PicVerify.Name = "PicVerify"
        Me.PicVerify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PicVerify.Size = New System.Drawing.Size(165, 146)
        Me.PicVerify.TabIndex = 13
        Me.PicVerify.TabStop = True
        '
        'LabVerify
        '
        Me.LabVerify.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabVerify.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabVerify.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVerify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabVerify.Location = New System.Drawing.Point(8, 14)
        Me.LabVerify.Name = "LabVerify"
        Me.LabVerify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabVerify.Size = New System.Drawing.Size(148, 124)
        Me.LabVerify.TabIndex = 14
        Me.LabVerify.Text = "LabVerify"
        '
        'txtSerialNo
        '
        Me.txtSerialNo.AcceptsReturn = True
        Me.txtSerialNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.txtSerialNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSerialNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSerialNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSerialNo.Location = New System.Drawing.Point(134, 315)
        Me.txtSerialNo.MaxLength = 40
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSerialNo.Size = New System.Drawing.Size(258, 22)
        Me.txtSerialNo.TabIndex = 5
        '
        'cboQty
        '
        Me.cboQty.BackColor = System.Drawing.SystemColors.Window
        Me.cboQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboQty.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboQty.Location = New System.Drawing.Point(336, 490)
        Me.cboQty.MaxLength = 3
        Me.cboQty.Name = "cboQty"
        Me.cboQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboQty.Size = New System.Drawing.Size(57, 22)
        Me.cboQty.TabIndex = 10
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdCancel.CausesValidation = False
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(525, 487)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(54, 25)
        Me.cmdCancel.TabIndex = 12
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(434, 487)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(56, 25)
        Me.cmdOk.TabIndex = 11
        Me.cmdOk.Text = "Finish"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'cmdLookup
        '
        Me.cmdLookup.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdLookup.CausesValidation = False
        Me.cmdLookup.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLookup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdLookup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLookup.Location = New System.Drawing.Point(375, 66)
        Me.cmdLookup.Name = "cmdLookup"
        Me.cmdLookup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLookup.Size = New System.Drawing.Size(112, 28)
        Me.cmdLookup.TabIndex = 1
        Me.cmdLookup.Text = "Full Size Lookup.."
        Me.cmdLookup.UseVisualStyleBackColor = False
        '
        'chkWarranty
        '
        Me.chkWarranty.BackColor = System.Drawing.Color.Transparent
        Me.chkWarranty.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWarranty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWarranty.Location = New System.Drawing.Point(24, 343)
        Me.chkWarranty.Name = "chkWarranty"
        Me.chkWarranty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWarranty.Size = New System.Drawing.Size(97, 25)
        Me.chkWarranty.TabIndex = 6
        Me.chkWarranty.Text = "Warranty Part"
        Me.chkWarranty.UseVisualStyleBackColor = False
        '
        'txtPartNo
        '
        Me.txtPartNo.AcceptsReturn = True
        Me.txtPartNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.txtPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNo.CausesValidation = False
        Me.txtPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartNo.Location = New System.Drawing.Point(23, 71)
        Me.txtPartNo.MaxLength = 15
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartNo.Size = New System.Drawing.Size(325, 22)
        Me.txtPartNo.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(23, 482)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(82, 29)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Selling Price (INCL. GST)"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(21, 431)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(100, 17)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Item Description"
        '
        'LabChangePrice
        '
        Me.LabChangePrice.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabChangePrice.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabChangePrice.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabChangePrice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabChangePrice.Location = New System.Drawing.Point(184, 374)
        Me.LabChangePrice.Name = "LabChangePrice"
        Me.LabChangePrice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabChangePrice.Size = New System.Drawing.Size(165, 46)
        Me.LabChangePrice.TabIndex = 16
        Me.LabChangePrice.Text = "NOTE:  The Description and Price data can be changed now for this Item.."
        '
        'LabEnterSerialNo
        '
        Me.LabEnterSerialNo.BackColor = System.Drawing.Color.Transparent
        Me.LabEnterSerialNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabEnterSerialNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabEnterSerialNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabEnterSerialNo.Location = New System.Drawing.Point(20, 309)
        Me.LabEnterSerialNo.Name = "LabEnterSerialNo"
        Me.LabEnterSerialNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabEnterSerialNo.Size = New System.Drawing.Size(108, 27)
        Me.LabEnterSerialNo.TabIndex = 12
        Me.LabEnterSerialNo.Text = "LabEnterSerialNo"
        Me.LabEnterSerialNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(281, 482)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(49, 33)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Quantity Supplied"
        '
        'LabDescription
        '
        Me.LabDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.LabDescription.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDescription.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDescription.Location = New System.Drawing.Point(23, 98)
        Me.LabDescription.Name = "LabDescription"
        Me.LabDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDescription.Size = New System.Drawing.Size(326, 41)
        Me.LabDescription.TabIndex = 8
        Me.LabDescription.Text = "LabDescription"
        '
        'LabScanProduct
        '
        Me.LabScanProduct.BackColor = System.Drawing.Color.Transparent
        Me.LabScanProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabScanProduct.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabScanProduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabScanProduct.Location = New System.Drawing.Point(266, 13)
        Me.LabScanProduct.Name = "LabScanProduct"
        Me.LabScanProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabScanProduct.Size = New System.Drawing.Size(310, 41)
        Me.LabScanProduct.TabIndex = 5
        Me.LabScanProduct.Text = "Scan in the Product Barcode (or key-in and press ENTER)..      Double-Click,  or " & _
    "Press F2,   to lookup the Retail Host stock table.."
        '
        'LabHdr1
        '
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 11.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.Location = New System.Drawing.Point(23, 9)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.Size = New System.Drawing.Size(206, 31)
        Me.LabHdr1.TabIndex = 19
        Me.LabHdr1.Text = "labHdr1"
        '
        'FrameBrowse
        '
        Me.FrameBrowse.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.FrameBrowse.Controls.Add(Me.Label5)
        Me.FrameBrowse.Controls.Add(Me.cmdClearStockSearch)
        Me.FrameBrowse.Controls.Add(Me.cmdStockSearch)
        Me.FrameBrowse.Controls.Add(Me.txtStockSearch)
        Me.FrameBrowse.Controls.Add(Me.DataGridView1)
        Me.FrameBrowse.Controls.Add(Me.cmdSelect)
        Me.FrameBrowse.Controls.Add(Me.txtFind)
        Me.FrameBrowse.Controls.Add(Me.labRecCount)
        Me.FrameBrowse.Controls.Add(Me.LabFind)
        Me.FrameBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameBrowse.Location = New System.Drawing.Point(24, 148)
        Me.FrameBrowse.Name = "FrameBrowse"
        Me.FrameBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameBrowse.Size = New System.Drawing.Size(555, 139)
        Me.FrameBrowse.TabIndex = 20
        Me.FrameBrowse.TabStop = False
        Me.FrameBrowse.Text = "FrameBrowse"
        '
        'txtStockSearch
        '
        Me.txtStockSearch.AcceptsReturn = True
        Me.txtStockSearch.BackColor = System.Drawing.Color.Gainsboro
        Me.txtStockSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStockSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStockSearch.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStockSearch.Location = New System.Drawing.Point(249, 33)
        Me.txtStockSearch.MaxLength = 0
        Me.txtStockSearch.Name = "txtStockSearch"
        Me.txtStockSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStockSearch.Size = New System.Drawing.Size(151, 13)
        Me.txtStockSearch.TabIndex = 78
        Me.txtStockSearch.Text = "txtStockSearch"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeight = 18
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.GridColor = System.Drawing.SystemColors.ControlLight
        Me.DataGridView1.Location = New System.Drawing.Point(10, 51)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.RowTemplate.Height = 19
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(533, 54)
        Me.DataGridView1.TabIndex = 4
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.Gainsboro
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(100, 32)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(89, 15)
        Me.txtFind.TabIndex = 2
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(8, 115)
        Me.labRecCount.Name = "labRecCount"
        Me.labRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCount.Size = New System.Drawing.Size(121, 17)
        Me.labRecCount.TabIndex = 19
        Me.labRecCount.Text = "labRecCount"
        '
        'LabFind
        '
        Me.LabFind.BackColor = System.Drawing.Color.LightGray
        Me.LabFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabFind.Location = New System.Drawing.Point(8, 24)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(89, 25)
        Me.LabFind.TabIndex = 18
        Me.LabFind.Text = "LabFind"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(23, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(237, 14)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Scan Product Barcode  (F2 to search)"
        '
        'chkKeepScannedLeadZeroes
        '
        Me.chkKeepScannedLeadZeroes.Font = New System.Drawing.Font("Tahoma", 6.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkKeepScannedLeadZeroes.Location = New System.Drawing.Point(375, 100)
        Me.chkKeepScannedLeadZeroes.Name = "chkKeepScannedLeadZeroes"
        Me.chkKeepScannedLeadZeroes.Size = New System.Drawing.Size(112, 28)
        Me.chkKeepScannedLeadZeroes.TabIndex = 22
        Me.chkKeepScannedLeadZeroes.Text = "Keep scanned Leading Zeroes.."
        Me.chkKeepScannedLeadZeroes.UseVisualStyleBackColor = True
        '
        'frmNewPart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(611, 536)
        Me.Controls.Add(Me.chkKeepScannedLeadZeroes)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.FrameBrowse)
        Me.Controls.Add(Me.LabHdr1)
        Me.Controls.Add(Me.txtSellInclGST)
        Me.Controls.Add(Me.txtDescription)
        Me.Controls.Add(Me.ChkAllowRenaming)
        Me.Controls.Add(Me.PicVerify)
        Me.Controls.Add(Me.txtSerialNo)
        Me.Controls.Add(Me.cboQty)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.cmdLookup)
        Me.Controls.Add(Me.chkWarranty)
        Me.Controls.Add(Me.txtPartNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabChangePrice)
        Me.Controls.Add(Me.LabEnterSerialNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LabDescription)
        Me.Controls.Add(Me.LabScanProduct)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNewPart"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "JobMatix- Add New Item."
        Me.PicVerify.ResumeLayout(False)
        Me.FrameBrowse.ResumeLayout(False)
        Me.FrameBrowse.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabHdr1 As System.Windows.Forms.Label
    Public WithEvents FrameBrowse As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Public WithEvents cmdSelect As System.Windows.Forms.Button
    Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents labRecCount As System.Windows.Forms.Label
    Public WithEvents LabFind As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkKeepScannedLeadZeroes As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents cmdClearStockSearch As System.Windows.Forms.Button
    Public WithEvents cmdStockSearch As System.Windows.Forms.Button
    Public WithEvents txtStockSearch As System.Windows.Forms.TextBox
#End Region 
End Class