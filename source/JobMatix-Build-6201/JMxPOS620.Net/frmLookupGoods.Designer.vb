<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLookupGoods
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelGoodsBanner = New System.Windows.Forms.Panel()
        Me.labStaffName = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.labHdrGR = New System.Windows.Forms.Label()
        Me.labToday = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.grpBoxGoodsLookup = New System.Windows.Forms.GroupBox()
        Me.grpBoxInvoice = New System.Windows.Forms.GroupBox()
        Me.panelPrinting = New System.Windows.Forms.Panel()
        Me.labReceivedBy = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.labGoodsTotal = New System.Windows.Forms.Label()
        Me.labItemsHdr = New System.Windows.Forms.Label()
        Me.cboInvoicePrinters = New System.Windows.Forms.ComboBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.txtSerialsList = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.listViewGoodsItems = New System.Windows.Forms.ListView()
        Me.ItemNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ItemBarcode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Cat1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Description = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cost_ex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.taxCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cost_inc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Qty = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.total_ex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.total_inc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FrameBrowse = New System.Windows.Forms.GroupBox()
        Me.labSupplier = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmdClearGoodsSearch = New System.Windows.Forms.Button()
        Me.cmdGoodsSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtGoodsSearch = New System.Windows.Forms.TextBox()
        Me.dgvGoodsList = New System.Windows.Forms.DataGridView()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.labRecCount = New System.Windows.Forms.Label()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.labVersion = New System.Windows.Forms.Label()
        Me.panelGoodsBanner.SuspendLayout()
        Me.grpBoxGoodsLookup.SuspendLayout()
        Me.grpBoxInvoice.SuspendLayout()
        Me.panelPrinting.SuspendLayout()
        Me.FrameBrowse.SuspendLayout()
        CType(Me.dgvGoodsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelGoodsBanner
        '
        Me.panelGoodsBanner.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.panelGoodsBanner.Controls.Add(Me.labStaffName)
        Me.panelGoodsBanner.Controls.Add(Me.Label13)
        Me.panelGoodsBanner.Controls.Add(Me.labHdrGR)
        Me.panelGoodsBanner.Controls.Add(Me.labToday)
        Me.panelGoodsBanner.Controls.Add(Me.Label20)
        Me.panelGoodsBanner.Location = New System.Drawing.Point(3, 1)
        Me.panelGoodsBanner.Name = "panelGoodsBanner"
        Me.panelGoodsBanner.Size = New System.Drawing.Size(943, 45)
        Me.panelGoodsBanner.TabIndex = 4
        '
        'labStaffName
        '
        Me.labStaffName.Location = New System.Drawing.Point(378, 25)
        Me.labStaffName.Name = "labStaffName"
        Me.labStaffName.Size = New System.Drawing.Size(101, 13)
        Me.labStaffName.TabIndex = 9
        Me.labStaffName.Text = "labStaffName"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Location = New System.Drawing.Point(378, 6)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 22)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Staff: "
        '
        'labHdrGR
        '
        Me.labHdrGR.BackColor = System.Drawing.Color.Transparent
        Me.labHdrGR.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdrGR.Location = New System.Drawing.Point(172, 14)
        Me.labHdrGR.Name = "labHdrGR"
        Me.labHdrGR.Size = New System.Drawing.Size(181, 23)
        Me.labHdrGR.TabIndex = 6
        Me.labHdrGR.Text = "Lookup Goods Received"
        '
        'labToday
        '
        Me.labToday.AutoSize = True
        Me.labToday.Location = New System.Drawing.Point(513, 24)
        Me.labToday.Name = "labToday"
        Me.labToday.Size = New System.Drawing.Size(51, 13)
        Me.labToday.TabIndex = 4
        Me.labToday.Text = "labToday"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(12, 9)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(160, 23)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "JobMatix POS-  "
        '
        'grpBoxGoodsLookup
        '
        Me.grpBoxGoodsLookup.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.grpBoxGoodsLookup.Controls.Add(Me.grpBoxInvoice)
        Me.grpBoxGoodsLookup.Controls.Add(Me.FrameBrowse)
        Me.grpBoxGoodsLookup.Location = New System.Drawing.Point(3, 47)
        Me.grpBoxGoodsLookup.Name = "grpBoxGoodsLookup"
        Me.grpBoxGoodsLookup.Size = New System.Drawing.Size(943, 647)
        Me.grpBoxGoodsLookup.TabIndex = 5
        Me.grpBoxGoodsLookup.TabStop = False
        Me.grpBoxGoodsLookup.Text = "grpBoxGoodsLookup"
        '
        'grpBoxInvoice
        '
        Me.grpBoxInvoice.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.grpBoxInvoice.Controls.Add(Me.panelPrinting)
        Me.grpBoxInvoice.Controls.Add(Me.txtSerialsList)
        Me.grpBoxInvoice.Controls.Add(Me.Label1)
        Me.grpBoxInvoice.Controls.Add(Me.listViewGoodsItems)
        Me.grpBoxInvoice.Location = New System.Drawing.Point(4, 322)
        Me.grpBoxInvoice.Name = "grpBoxInvoice"
        Me.grpBoxInvoice.Size = New System.Drawing.Size(933, 319)
        Me.grpBoxInvoice.TabIndex = 41
        Me.grpBoxInvoice.TabStop = False
        Me.grpBoxInvoice.Text = "grpBoxInvoice"
        '
        'panelPrinting
        '
        Me.panelPrinting.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelPrinting.CausesValidation = False
        Me.panelPrinting.Controls.Add(Me.labReceivedBy)
        Me.panelPrinting.Controls.Add(Me.Label4)
        Me.panelPrinting.Controls.Add(Me.Label2)
        Me.panelPrinting.Controls.Add(Me.labGoodsTotal)
        Me.panelPrinting.Controls.Add(Me.labItemsHdr)
        Me.panelPrinting.Controls.Add(Me.cboInvoicePrinters)
        Me.panelPrinting.Controls.Add(Me.btnPrint)
        Me.panelPrinting.Location = New System.Drawing.Point(3, 16)
        Me.panelPrinting.Name = "panelPrinting"
        Me.panelPrinting.Size = New System.Drawing.Size(924, 53)
        Me.panelPrinting.TabIndex = 42
        '
        'labReceivedBy
        '
        Me.labReceivedBy.Location = New System.Drawing.Point(165, 26)
        Me.labReceivedBy.Name = "labReceivedBy"
        Me.labReceivedBy.Size = New System.Drawing.Size(104, 21)
        Me.labReceivedBy.TabIndex = 61
        Me.labReceivedBy.Text = "labReceivedBy"
        Me.labReceivedBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(162, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 13)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Received By:"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(570, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(108, 13)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "Available Printers:"
        '
        'labGoodsTotal
        '
        Me.labGoodsTotal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labGoodsTotal.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labGoodsTotal.Location = New System.Drawing.Point(356, 5)
        Me.labGoodsTotal.Name = "labGoodsTotal"
        Me.labGoodsTotal.Size = New System.Drawing.Size(171, 40)
        Me.labGoodsTotal.TabIndex = 41
        Me.labGoodsTotal.Text = "labGoodsTotal"
        Me.labGoodsTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labItemsHdr
        '
        Me.labItemsHdr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labItemsHdr.Location = New System.Drawing.Point(23, 5)
        Me.labItemsHdr.Name = "labItemsHdr"
        Me.labItemsHdr.Size = New System.Drawing.Size(153, 40)
        Me.labItemsHdr.TabIndex = 38
        Me.labItemsHdr.Text = "Items Invoiced"
        Me.labItemsHdr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboInvoicePrinters
        '
        Me.cboInvoicePrinters.BackColor = System.Drawing.Color.Lavender
        Me.cboInvoicePrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvoicePrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboInvoicePrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvoicePrinters.FormattingEnabled = True
        Me.cboInvoicePrinters.Location = New System.Drawing.Point(570, 24)
        Me.cboInvoicePrinters.Name = "cboInvoicePrinters"
        Me.cboInvoicePrinters.Size = New System.Drawing.Size(171, 21)
        Me.cboInvoicePrinters.TabIndex = 58
        '
        'btnPrint
        '
        Me.btnPrint.CausesValidation = False
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(780, 17)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(126, 28)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print Goods Invoice"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'txtSerialsList
        '
        Me.txtSerialsList.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSerialsList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSerialsList.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialsList.Location = New System.Drawing.Point(769, 108)
        Me.txtSerialsList.Multiline = True
        Me.txtSerialsList.Name = "txtSerialsList"
        Me.txtSerialsList.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSerialsList.Size = New System.Drawing.Size(161, 205)
        Me.txtSerialsList.TabIndex = 39
        Me.txtSerialsList.Text = "txtSerialsList"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(772, 77)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 28)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Serials recorded for Selected Item."
        '
        'listViewGoodsItems
        '
        Me.listViewGoodsItems.BackColor = System.Drawing.Color.WhiteSmoke
        Me.listViewGoodsItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ItemNo, Me.ItemBarcode, Me.Cat1, Me.Description, Me.cost_ex, Me.taxCode, Me.cost_inc, Me.Qty, Me.total_ex, Me.total_inc})
        Me.listViewGoodsItems.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewGoodsItems.FullRowSelect = True
        Me.listViewGoodsItems.GridLines = True
        Me.listViewGoodsItems.HideSelection = False
        Me.listViewGoodsItems.Location = New System.Drawing.Point(3, 72)
        Me.listViewGoodsItems.MultiSelect = False
        Me.listViewGoodsItems.Name = "listViewGoodsItems"
        Me.listViewGoodsItems.Size = New System.Drawing.Size(755, 241)
        Me.listViewGoodsItems.TabIndex = 37
        Me.listViewGoodsItems.TabStop = False
        Me.listViewGoodsItems.UseCompatibleStateImageBehavior = False
        Me.listViewGoodsItems.View = System.Windows.Forms.View.Details
        '
        'ItemNo
        '
        Me.ItemNo.Text = "Item#"
        Me.ItemNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ItemNo.Width = 40
        '
        'ItemBarcode
        '
        Me.ItemBarcode.Text = "ItemBarcode"
        Me.ItemBarcode.Width = 100
        '
        'Cat1
        '
        Me.Cat1.Text = "Cat1"
        Me.Cat1.Width = 40
        '
        'Description
        '
        Me.Description.Text = "Description"
        Me.Description.Width = 170
        '
        'cost_ex
        '
        Me.cost_ex.Text = "cost_ex"
        Me.cost_ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'taxCode
        '
        Me.taxCode.Text = "taxCode"
        Me.taxCode.Width = 55
        '
        'cost_inc
        '
        Me.cost_inc.Text = "Cost_inc"
        Me.cost_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.cost_inc.Width = 70
        '
        'Qty
        '
        Me.Qty.Text = "Qty"
        Me.Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Qty.Width = 50
        '
        'total_ex
        '
        Me.total_ex.Text = "total_ex"
        Me.total_ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.total_ex.Width = 80
        '
        'total_inc
        '
        Me.total_inc.Text = "total_inc"
        Me.total_inc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.total_inc.Width = 80
        '
        'FrameBrowse
        '
        Me.FrameBrowse.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.FrameBrowse.Controls.Add(Me.labSupplier)
        Me.FrameBrowse.Controls.Add(Me.Label22)
        Me.FrameBrowse.Controls.Add(Me.Label21)
        Me.FrameBrowse.Controls.Add(Me.cmdClearGoodsSearch)
        Me.FrameBrowse.Controls.Add(Me.cmdGoodsSearch)
        Me.FrameBrowse.Controls.Add(Me.Label3)
        Me.FrameBrowse.Controls.Add(Me.txtGoodsSearch)
        Me.FrameBrowse.Controls.Add(Me.dgvGoodsList)
        Me.FrameBrowse.Controls.Add(Me.txtFind)
        Me.FrameBrowse.Controls.Add(Me.labRecCount)
        Me.FrameBrowse.Controls.Add(Me.LabFind)
        Me.FrameBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameBrowse.Location = New System.Drawing.Point(7, 8)
        Me.FrameBrowse.Name = "FrameBrowse"
        Me.FrameBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameBrowse.Size = New System.Drawing.Size(930, 306)
        Me.FrameBrowse.TabIndex = 22
        Me.FrameBrowse.TabStop = False
        Me.FrameBrowse.Text = "FrameBrowse"
        '
        'labSupplier
        '
        Me.labSupplier.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSupplier.Location = New System.Drawing.Point(205, 14)
        Me.labSupplier.Name = "labSupplier"
        Me.labSupplier.Size = New System.Drawing.Size(220, 32)
        Me.labSupplier.TabIndex = 83
        Me.labSupplier.Text = "labSupplier"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(792, 14)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(83, 13)
        Me.Label22.TabIndex = 82
        Me.Label22.Text = "Records found."
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(522, 27)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(121, 12)
        Me.Label21.TabIndex = 81
        Me.Label21.Text = "Full Text Filter (Srch):"
        '
        'cmdClearGoodsSearch
        '
        Me.cmdClearGoodsSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClearGoodsSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearGoodsSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClearGoodsSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearGoodsSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearGoodsSearch.Location = New System.Drawing.Point(731, 34)
        Me.cmdClearGoodsSearch.Name = "cmdClearGoodsSearch"
        Me.cmdClearGoodsSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearGoodsSearch.Size = New System.Drawing.Size(53, 23)
        Me.cmdClearGoodsSearch.TabIndex = 80
        Me.cmdClearGoodsSearch.Text = "Clear"
        Me.cmdClearGoodsSearch.UseVisualStyleBackColor = False
        '
        'cmdGoodsSearch
        '
        Me.cmdGoodsSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdGoodsSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGoodsSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdGoodsSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGoodsSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGoodsSearch.Location = New System.Drawing.Point(803, 34)
        Me.cmdGoodsSearch.Name = "cmdGoodsSearch"
        Me.cmdGoodsSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGoodsSearch.Size = New System.Drawing.Size(53, 23)
        Me.cmdGoodsSearch.TabIndex = 79
        Me.cmdGoodsSearch.Text = "Search"
        Me.cmdGoodsSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdGoodsSearch.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(190, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Goods Received Invoices-"
        '
        'txtGoodsSearch
        '
        Me.txtGoodsSearch.AcceptsReturn = True
        Me.txtGoodsSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtGoodsSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGoodsSearch.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGoodsSearch.Location = New System.Drawing.Point(525, 40)
        Me.txtGoodsSearch.MaxLength = 0
        Me.txtGoodsSearch.Name = "txtGoodsSearch"
        Me.txtGoodsSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGoodsSearch.Size = New System.Drawing.Size(168, 19)
        Me.txtGoodsSearch.TabIndex = 78
        Me.txtGoodsSearch.Text = "txtGoodsSearch"
        '
        'dgvGoodsList
        '
        Me.dgvGoodsList.AllowUserToAddRows = False
        Me.dgvGoodsList.AllowUserToDeleteRows = False
        Me.dgvGoodsList.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvGoodsList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvGoodsList.ColumnHeadersHeight = 18
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvGoodsList.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvGoodsList.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvGoodsList.Location = New System.Drawing.Point(11, 70)
        Me.dgvGoodsList.MultiSelect = False
        Me.dgvGoodsList.Name = "dgvGoodsList"
        Me.dgvGoodsList.ReadOnly = True
        Me.dgvGoodsList.RowHeadersWidth = 17
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvGoodsList.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvGoodsList.RowTemplate.Height = 17
        Me.dgvGoodsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvGoodsList.Size = New System.Drawing.Size(913, 219)
        Me.dgvGoodsList.StandardTab = True
        Me.dgvGoodsList.TabIndex = 4
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(150, 50)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(100, 15)
        Me.txtFind.TabIndex = 2
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(742, 14)
        Me.labRecCount.Name = "labRecCount"
        Me.labRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCount.Size = New System.Drawing.Size(44, 15)
        Me.labRecCount.TabIndex = 19
        Me.labRecCount.Text = "labRecCount"
        Me.labRecCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabFind
        '
        Me.LabFind.BackColor = System.Drawing.Color.LightGray
        Me.LabFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabFind.Location = New System.Drawing.Point(9, 40)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(135, 25)
        Me.LabFind.TabIndex = 18
        Me.LabFind.Text = "LabFind"
        '
        'labVersion
        '
        Me.labVersion.AutoSize = True
        Me.labVersion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labVersion.Location = New System.Drawing.Point(17, 697)
        Me.labVersion.Name = "labVersion"
        Me.labVersion.Size = New System.Drawing.Size(51, 12)
        Me.labVersion.TabIndex = 13
        Me.labVersion.Text = "labVersion"
        '
        'frmLookupGoods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(950, 708)
        Me.Controls.Add(Me.labVersion)
        Me.Controls.Add(Me.grpBoxGoodsLookup)
        Me.Controls.Add(Me.panelGoodsBanner)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmLookupGoods"
        Me.Text = "frmLookupGoods"
        Me.panelGoodsBanner.ResumeLayout(False)
        Me.panelGoodsBanner.PerformLayout()
        Me.grpBoxGoodsLookup.ResumeLayout(False)
        Me.grpBoxInvoice.ResumeLayout(False)
        Me.grpBoxInvoice.PerformLayout()
        Me.panelPrinting.ResumeLayout(False)
        Me.panelPrinting.PerformLayout()
        Me.FrameBrowse.ResumeLayout(False)
        Me.FrameBrowse.PerformLayout()
        CType(Me.dgvGoodsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents panelGoodsBanner As System.Windows.Forms.Panel
    Friend WithEvents labStaffName As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents labHdrGR As System.Windows.Forms.Label
    Friend WithEvents labToday As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents grpBoxGoodsLookup As System.Windows.Forms.GroupBox
    Public WithEvents FrameBrowse As System.Windows.Forms.GroupBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents cmdClearGoodsSearch As System.Windows.Forms.Button
    Public WithEvents cmdGoodsSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtGoodsSearch As System.Windows.Forms.TextBox
    Friend WithEvents dgvGoodsList As System.Windows.Forms.DataGridView
    Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents labRecCount As System.Windows.Forms.Label
    Public WithEvents LabFind As System.Windows.Forms.Label
    Friend WithEvents labItemsHdr As System.Windows.Forms.Label
    Friend WithEvents listViewGoodsItems As System.Windows.Forms.ListView
    Friend WithEvents ItemBarcode As System.Windows.Forms.ColumnHeader
    Friend WithEvents Description As System.Windows.Forms.ColumnHeader
    Friend WithEvents Qty As System.Windows.Forms.ColumnHeader
    Friend WithEvents cost_inc As System.Windows.Forms.ColumnHeader
    Friend WithEvents total_ex As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSerialsList As System.Windows.Forms.TextBox
    Friend WithEvents grpBoxInvoice As System.Windows.Forms.GroupBox
    Friend WithEvents Cat1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents labVersion As System.Windows.Forms.Label
    Friend WithEvents labGoodsTotal As System.Windows.Forms.Label
    Friend WithEvents labSupplier As System.Windows.Forms.Label
    Friend WithEvents ItemNo As System.Windows.Forms.ColumnHeader
    Friend WithEvents panelPrinting As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboInvoicePrinters As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents taxCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents cost_ex As System.Windows.Forms.ColumnHeader
    Friend WithEvents total_inc As System.Windows.Forms.ColumnHeader
    Friend WithEvents labReceivedBy As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
