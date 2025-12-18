<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGoodsSerials
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labStockBarcode = New System.Windows.Forms.Label()
        Me.labStockDescription = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dgvSerials = New System.Windows.Forms.DataGridView()
        Me.SeraiNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdSaveExit = New System.Windows.Forms.Button()
        Me.labIntro = New System.Windows.Forms.Label()
        Me.labMsg = New System.Windows.Forms.Label()
        Me.labStockId = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSerialsOnFile = New System.Windows.Forms.TextBox()
        Me.labShowCurrent = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.labQty = New System.Windows.Forms.Label()
        CType(Me.dgvSerials, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(168, 27)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Goods Received- "
        '
        'labStockBarcode
        '
        Me.labStockBarcode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labStockBarcode.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStockBarcode.Location = New System.Drawing.Point(195, 95)
        Me.labStockBarcode.Name = "labStockBarcode"
        Me.labStockBarcode.Size = New System.Drawing.Size(278, 19)
        Me.labStockBarcode.TabIndex = 1
        Me.labStockBarcode.Text = "labStockBarcode"
        '
        'labStockDescription
        '
        Me.labStockDescription.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labStockDescription.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStockDescription.Location = New System.Drawing.Point(45, 114)
        Me.labStockDescription.Name = "labStockDescription"
        Me.labStockDescription.Size = New System.Drawing.Size(428, 19)
        Me.labStockDescription.TabIndex = 2
        Me.labStockDescription.Text = "labStockDescription"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(25, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Stock Item:"
        '
        'dgvSerials
        '
        Me.dgvSerials.AllowUserToAddRows = False
        Me.dgvSerials.AllowUserToDeleteRows = False
        Me.dgvSerials.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvSerials.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSerials.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSerials.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvSerials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSerials.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SeraiNo})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSerials.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvSerials.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvSerials.Location = New System.Drawing.Point(293, 228)
        Me.dgvSerials.MultiSelect = False
        Me.dgvSerials.Name = "dgvSerials"
        Me.dgvSerials.RowTemplate.Height = 17
        Me.dgvSerials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSerials.Size = New System.Drawing.Size(292, 247)
        Me.dgvSerials.StandardTab = True
        Me.dgvSerials.TabIndex = 4
        '
        'SeraiNo
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SeraiNo.DefaultCellStyle = DataGridViewCellStyle2
        Me.SeraiNo.HeaderText = "Serial No."
        Me.SeraiNo.Name = "SeraiNo"
        Me.SeraiNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.Thistle
        Me.cmdCancel.CausesValidation = False
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(432, 552)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(57, 32)
        Me.cmdCancel.TabIndex = 91
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdSaveExit
        '
        Me.cmdSaveExit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdSaveExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSaveExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveExit.Location = New System.Drawing.Point(528, 552)
        Me.cmdSaveExit.Name = "cmdSaveExit"
        Me.cmdSaveExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSaveExit.Size = New System.Drawing.Size(57, 32)
        Me.cmdSaveExit.TabIndex = 93
        Me.cmdSaveExit.Text = "OK"
        Me.cmdSaveExit.UseVisualStyleBackColor = False
        '
        'labIntro
        '
        Me.labIntro.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labIntro.Location = New System.Drawing.Point(292, 152)
        Me.labIntro.Name = "labIntro"
        Me.labIntro.Size = New System.Drawing.Size(293, 73)
        Me.labIntro.TabIndex = 94
        Me.labIntro.Text = "Enter all serial numbers arriving for this stock item line.."
        '
        'labMsg
        '
        Me.labMsg.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labMsg.Location = New System.Drawing.Point(290, 493)
        Me.labMsg.Name = "labMsg"
        Me.labMsg.Size = New System.Drawing.Size(295, 45)
        Me.labMsg.TabIndex = 95
        Me.labMsg.Text = "labMsg"
        '
        'labStockId
        '
        Me.labStockId.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labStockId.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStockId.Location = New System.Drawing.Point(76, 95)
        Me.labStockId.Name = "labStockId"
        Me.labStockId.Size = New System.Drawing.Size(34, 19)
        Me.labStockId.TabIndex = 96
        Me.labStockId.Text = "labStockId"
        Me.labStockId.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(45, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 19)
        Me.Label4.TabIndex = 97
        Me.Label4.Text = "ID: "
        '
        'txtSerialsOnFile
        '
        Me.txtSerialsOnFile.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSerialsOnFile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSerialsOnFile.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialsOnFile.Location = New System.Drawing.Point(42, 228)
        Me.txtSerialsOnFile.Multiline = True
        Me.txtSerialsOnFile.Name = "txtSerialsOnFile"
        Me.txtSerialsOnFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSerialsOnFile.Size = New System.Drawing.Size(229, 356)
        Me.txtSerialsOnFile.TabIndex = 98
        '
        'labShowCurrent
        '
        Me.labShowCurrent.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labShowCurrent.Location = New System.Drawing.Point(39, 188)
        Me.labShowCurrent.Name = "labShowCurrent"
        Me.labShowCurrent.Size = New System.Drawing.Size(164, 28)
        Me.labShowCurrent.TabIndex = 99
        Me.labShowCurrent.Text = "Item Serials currently on File in System:"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(131, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 19)
        Me.Label5.TabIndex = 100
        Me.Label5.Text = "Barcode: "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(24, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(247, 27)
        Me.Label6.TabIndex = 101
        Me.Label6.Text = "   Record Item Serial Numbers"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(45, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 19)
        Me.Label7.TabIndex = 103
        Me.Label7.Text = "Qty: "
        '
        'labQty
        '
        Me.labQty.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labQty.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labQty.Location = New System.Drawing.Point(86, 136)
        Me.labQty.Name = "labQty"
        Me.labQty.Size = New System.Drawing.Size(44, 19)
        Me.labQty.TabIndex = 102
        Me.labQty.Text = "labQty"
        Me.labQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmGoodsSerials
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(648, 606)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.labQty)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.labShowCurrent)
        Me.Controls.Add(Me.txtSerialsOnFile)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.labStockId)
        Me.Controls.Add(Me.labMsg)
        Me.Controls.Add(Me.labIntro)
        Me.Controls.Add(Me.cmdSaveExit)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.dgvSerials)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.labStockDescription)
        Me.Controls.Add(Me.labStockBarcode)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmGoodsSerials"
        Me.Text = "frmGoodsSerials"
        CType(Me.dgvSerials, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents labStockBarcode As System.Windows.Forms.Label
    Friend WithEvents labStockDescription As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dgvSerials As System.Windows.Forms.DataGridView
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdSaveExit As System.Windows.Forms.Button
    Friend WithEvents labIntro As System.Windows.Forms.Label
    Friend WithEvents labMsg As System.Windows.Forms.Label
    Friend WithEvents labStockId As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSerialsOnFile As System.Windows.Forms.TextBox
    Friend WithEvents labShowCurrent As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SeraiNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents labQty As System.Windows.Forms.Label
End Class
