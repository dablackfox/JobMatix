<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockLabels
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvStockItems = New System.Windows.Forms.DataGridView()
        Me.Barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cat1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cat2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sell_inc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Stock_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.isServiceItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboLabelPrinters = New System.Windows.Forms.ComboBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.labHelp = New System.Windows.Forms.Label()
        Me.panelStockLineEntry = New System.Windows.Forms.Panel()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.labStockLinePrompt = New System.Windows.Forms.Label()
        Me.btnStockLineOk = New System.Windows.Forms.Button()
        Me.txtStockItemDescription = New System.Windows.Forms.TextBox()
        Me.txtStockItemBarcode = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        CType(Me.dgvStockItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelStockLineEntry.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(198, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Printing Stock Labels"
        '
        'dgvStockItems
        '
        Me.dgvStockItems.AllowUserToAddRows = False
        Me.dgvStockItems.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.dgvStockItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvStockItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvStockItems.BackgroundColor = System.Drawing.Color.Lavender
        Me.dgvStockItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvStockItems.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvStockItems.ColumnHeadersHeight = 22
        Me.dgvStockItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Barcode, Me.Qty, Me.Cat1, Me.Cat2, Me.Description, Me.Sell_inc, Me.Stock_id, Me.isServiceItem})
        Me.dgvStockItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvStockItems.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvStockItems.Location = New System.Drawing.Point(12, 155)
        Me.dgvStockItems.MultiSelect = False
        Me.dgvStockItems.Name = "dgvStockItems"
        Me.dgvStockItems.ReadOnly = True
        Me.dgvStockItems.RowHeadersWidth = 50
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvStockItems.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvStockItems.RowTemplate.Height = 19
        Me.dgvStockItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvStockItems.Size = New System.Drawing.Size(767, 343)
        Me.dgvStockItems.TabIndex = 22
        '
        'Barcode
        '
        Me.Barcode.HeaderText = "Barcode"
        Me.Barcode.Name = "Barcode"
        Me.Barcode.ReadOnly = True
        '
        'Qty
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Qty.DefaultCellStyle = DataGridViewCellStyle3
        Me.Qty.FillWeight = 50.0!
        Me.Qty.HeaderText = "Qty"
        Me.Qty.Name = "Qty"
        Me.Qty.ReadOnly = True
        '
        'Cat1
        '
        Me.Cat1.FillWeight = 50.0!
        Me.Cat1.HeaderText = "Cat1"
        Me.Cat1.Name = "Cat1"
        Me.Cat1.ReadOnly = True
        '
        'Cat2
        '
        Me.Cat2.FillWeight = 50.0!
        Me.Cat2.HeaderText = "Cat2"
        Me.Cat2.Name = "Cat2"
        Me.Cat2.ReadOnly = True
        '
        'Description
        '
        Me.Description.FillWeight = 240.0!
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        '
        'Sell_inc
        '
        Me.Sell_inc.FillWeight = 80.0!
        Me.Sell_inc.HeaderText = "RRP"
        Me.Sell_inc.Name = "Sell_inc"
        Me.Sell_inc.ReadOnly = True
        Me.Sell_inc.ToolTipText = "Normal Sell Inc Tax"
        '
        'Stock_id
        '
        Me.Stock_id.HeaderText = "Stock_id"
        Me.Stock_id.Name = "Stock_id"
        Me.Stock_id.ReadOnly = True
        Me.Stock_id.Visible = False
        '
        'isServiceItem
        '
        Me.isServiceItem.HeaderText = "isServiceItem"
        Me.isServiceItem.Name = "isServiceItem"
        Me.isServiceItem.ReadOnly = True
        Me.isServiceItem.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPrint.CausesValidation = False
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Location = New System.Drawing.Point(682, 36)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(92, 28)
        Me.btnPrint.TabIndex = 23
        Me.btnPrint.Text = "Print All"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(401, 25)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(94, 13)
        Me.Label20.TabIndex = 59
        Me.Label20.Text = "Available Printers:"
        '
        'cboLabelPrinters
        '
        Me.cboLabelPrinters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboLabelPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLabelPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboLabelPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLabelPrinters.FormattingEnabled = True
        Me.cboLabelPrinters.Location = New System.Drawing.Point(404, 41)
        Me.cboLabelPrinters.Name = "cboLabelPrinters"
        Me.cboLabelPrinters.Size = New System.Drawing.Size(237, 21)
        Me.cboLabelPrinters.TabIndex = 58
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(676, 520)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(92, 28)
        Me.btnCancel.TabIndex = 60
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'labHelp
        '
        Me.labHelp.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labHelp.Location = New System.Drawing.Point(16, 501)
        Me.labHelp.Name = "labHelp"
        Me.labHelp.Padding = New System.Windows.Forms.Padding(5, 5, 0, 0)
        Me.labHelp.Size = New System.Drawing.Size(255, 65)
        Me.labHelp.TabIndex = 61
        Me.labHelp.Text = "Scan stock barcodes to print (F2 to lookup)." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "To delete a row, fully select the" & _
    " row (click on the Cat1 cell) and press DEL."
        '
        'panelStockLineEntry
        '
        Me.panelStockLineEntry.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.panelStockLineEntry.Controls.Add(Me.txtQuantity)
        Me.panelStockLineEntry.Controls.Add(Me.Label2)
        Me.panelStockLineEntry.Controls.Add(Me.Label16)
        Me.panelStockLineEntry.Controls.Add(Me.labStockLinePrompt)
        Me.panelStockLineEntry.Controls.Add(Me.btnStockLineOk)
        Me.panelStockLineEntry.Controls.Add(Me.txtStockItemDescription)
        Me.panelStockLineEntry.Controls.Add(Me.txtStockItemBarcode)
        Me.panelStockLineEntry.Controls.Add(Me.Label22)
        Me.panelStockLineEntry.Location = New System.Drawing.Point(12, 79)
        Me.panelStockLineEntry.Name = "panelStockLineEntry"
        Me.panelStockLineEntry.Size = New System.Drawing.Size(767, 73)
        Me.panelStockLineEntry.TabIndex = 63
        Me.panelStockLineEntry.TabStop = True
        '
        'txtQuantity
        '
        Me.txtQuantity.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantity.Location = New System.Drawing.Point(466, 30)
        Me.txtQuantity.MaxLength = 3
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(46, 27)
        Me.txtQuantity.TabIndex = 2
        Me.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(464, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 15)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Quantity: "
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
        Me.labStockLinePrompt.Location = New System.Drawing.Point(621, 8)
        Me.labStockLinePrompt.Name = "labStockLinePrompt"
        Me.labStockLinePrompt.Size = New System.Drawing.Size(141, 57)
        Me.labStockLinePrompt.TabIndex = 39
        Me.labStockLinePrompt.Text = "Scan or Enter the Stock Barcode..  Select the Quantity, and Add to item to DataGr" & _
    "id.."
        '
        'btnStockLineOk
        '
        Me.btnStockLineOk.BackColor = System.Drawing.Color.Lavender
        Me.btnStockLineOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStockLineOk.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnStockLineOk.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnStockLineOk.Location = New System.Drawing.Point(544, 19)
        Me.btnStockLineOk.Name = "btnStockLineOk"
        Me.btnStockLineOk.Size = New System.Drawing.Size(70, 37)
        Me.btnStockLineOk.TabIndex = 3
        Me.btnStockLineOk.Text = "Add Item to Grid"
        Me.btnStockLineOk.UseVisualStyleBackColor = False
        '
        'txtStockItemDescription
        '
        Me.txtStockItemDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtStockItemDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStockItemDescription.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockItemDescription.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.txtStockItemDescription.Location = New System.Drawing.Point(184, 37)
        Me.txtStockItemDescription.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtStockItemDescription.MaxLength = 60
        Me.txtStockItemDescription.Name = "txtStockItemDescription"
        Me.txtStockItemDescription.ReadOnly = True
        Me.txtStockItemDescription.Size = New System.Drawing.Size(248, 19)
        Me.txtStockItemDescription.TabIndex = 1
        Me.txtStockItemDescription.TabStop = False
        Me.txtStockItemDescription.Text = "txtStockItemDescription"
        '
        'txtStockItemBarcode
        '
        Me.txtStockItemBarcode.BackColor = System.Drawing.Color.White
        Me.txtStockItemBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStockItemBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockItemBarcode.Location = New System.Drawing.Point(13, 37)
        Me.txtStockItemBarcode.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtStockItemBarcode.MaxLength = 40
        Me.txtStockItemBarcode.Name = "txtStockItemBarcode"
        Me.txtStockItemBarcode.Size = New System.Drawing.Size(159, 21)
        Me.txtStockItemBarcode.TabIndex = 0
        Me.txtStockItemBarcode.Text = "txtStockItemBarcode"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(10, 15)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(101, 15)
        Me.Label22.TabIndex = 29
        Me.Label22.Text = "Item Barcode: "
        '
        'frmStockLabels
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(791, 572)
        Me.Controls.Add(Me.panelStockLineEntry)
        Me.Controls.Add(Me.labHelp)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.cboLabelPrinters)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.dgvStockItems)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmStockLabels"
        Me.Text = "frmStockLabels"
        CType(Me.dgvStockItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelStockLineEntry.ResumeLayout(False)
        Me.panelStockLineEntry.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvStockItems As System.Windows.Forms.DataGridView
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cboLabelPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents labHelp As System.Windows.Forms.Label
    Friend WithEvents panelStockLineEntry As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents labStockLinePrompt As System.Windows.Forms.Label
    Friend WithEvents btnStockLineOk As System.Windows.Forms.Button
    Friend WithEvents txtStockItemDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtStockItemBarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cat1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cat2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sell_inc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Stock_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents isServiceItem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
End Class
