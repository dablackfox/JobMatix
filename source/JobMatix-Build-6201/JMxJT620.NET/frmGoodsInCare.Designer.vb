<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGoodsInCare
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
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdFinish As System.Windows.Forms.Button
    Public WithEvents Label5 As System.Windows.Forms.Label
    '== Public WithEvents txtGoods As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdCreateGoodsType = New System.Windows.Forms.Button()
        Me.cmdFinish = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.FrameGoods = New System.Windows.Forms.GroupBox()
        Me.txtExtraDefs = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvGoods_old = New System.Windows.Forms.DataGridView()
        Me.GoodsType_old = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Brand_old = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Model_old = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Serial_old = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cmdDeleteGoods = New System.Windows.Forms.Button()
        Me.cmdEditBrands = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.FrameGoods.SuspendLayout()
        CType(Me.dgvGoods_old, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdCreateGoodsType
        '
        Me.CmdCreateGoodsType.BackColor = System.Drawing.Color.WhiteSmoke
        Me.CmdCreateGoodsType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdCreateGoodsType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdCreateGoodsType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdCreateGoodsType.Location = New System.Drawing.Point(136, 339)
        Me.CmdCreateGoodsType.Name = "CmdCreateGoodsType"
        Me.CmdCreateGoodsType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdCreateGoodsType.Size = New System.Drawing.Size(78, 21)
        Me.CmdCreateGoodsType.TabIndex = 15
        Me.CmdCreateGoodsType.Text = "Goods Types"
        Me.ToolTip1.SetToolTip(Me.CmdCreateGoodsType, "Edit Reference Table of Standard Goods Types..")
        Me.CmdCreateGoodsType.UseVisualStyleBackColor = False
        '
        'cmdFinish
        '
        Me.cmdFinish.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdFinish.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFinish.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFinish.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFinish.Location = New System.Drawing.Point(390, 334)
        Me.cmdFinish.Name = "cmdFinish"
        Me.cmdFinish.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFinish.Size = New System.Drawing.Size(56, 26)
        Me.cmdFinish.TabIndex = 17
        Me.cmdFinish.Text = "Finish"
        Me.ToolTip1.SetToolTip(Me.cmdFinish, "All Items Completed")
        Me.cmdFinish.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(463, 334)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(56, 26)
        Me.cmdCancel.TabIndex = 18
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(13, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(287, 51)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Select Type of Goods and Brand from the standard Lookup Tables (drop-downs).  Ent" & _
    "er Model info,  and Serial Number where available."
        '
        'FrameGoods
        '
        Me.FrameGoods.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameGoods.Controls.Add(Me.txtExtraDefs)
        Me.FrameGoods.Controls.Add(Me.cmdCancel)
        Me.FrameGoods.Controls.Add(Me.Label1)
        Me.FrameGoods.Controls.Add(Me.cmdFinish)
        Me.FrameGoods.Controls.Add(Me.dgvGoods_old)
        Me.FrameGoods.Controls.Add(Me.cmdDeleteGoods)
        Me.FrameGoods.Controls.Add(Me.Label5)
        Me.FrameGoods.Controls.Add(Me.cmdEditBrands)
        Me.FrameGoods.Controls.Add(Me.CmdCreateGoodsType)
        Me.FrameGoods.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameGoods.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameGoods.Location = New System.Drawing.Point(21, 47)
        Me.FrameGoods.Name = "FrameGoods"
        Me.FrameGoods.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameGoods.Size = New System.Drawing.Size(551, 377)
        Me.FrameGoods.TabIndex = 25
        Me.FrameGoods.TabStop = False
        '
        'txtExtraDefs
        '
        Me.txtExtraDefs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtExtraDefs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtExtraDefs.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExtraDefs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtExtraDefs.Location = New System.Drawing.Point(19, 265)
        Me.txtExtraDefs.Multiline = True
        Me.txtExtraDefs.Name = "txtExtraDefs"
        Me.txtExtraDefs.Size = New System.Drawing.Size(288, 62)
        Me.txtExtraDefs.TabIndex = 40
        Me.txtExtraDefs.Text = "txtExtraDefs..   NB: DataGridView above is a MODEL only..  subclassed Grid built " & _
    "at Runtime."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 343)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Edit Reference Table: "
        '
        'dgvGoods_old
        '
        Me.dgvGoods_old.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvGoods_old.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Desktop
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvGoods_old.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvGoods_old.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvGoods_old.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GoodsType_old, Me.Brand_old, Me.Model_old, Me.Serial_old})
        Me.dgvGoods_old.Location = New System.Drawing.Point(16, 91)
        Me.dgvGoods_old.Name = "dgvGoods_old"
        Me.dgvGoods_old.Size = New System.Drawing.Size(503, 168)
        Me.dgvGoods_old.TabIndex = 16
        '
        'GoodsType_old
        '
        Me.GoodsType_old.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.GoodsType_old.HeaderText = "Goods Type"
        Me.GoodsType_old.Name = "GoodsType_old"
        '
        'Brand_old
        '
        Me.Brand_old.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.Brand_old.HeaderText = "Brand"
        Me.Brand_old.Name = "Brand_old"
        '
        'Model_old
        '
        Me.Model_old.HeaderText = "Model"
        Me.Model_old.Name = "Model_old"
        '
        'Serial_old
        '
        Me.Serial_old.HeaderText = "Serial No."
        Me.Serial_old.Name = "Serial_old"
        '
        'cmdDeleteGoods
        '
        Me.cmdDeleteGoods.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdDeleteGoods.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeleteGoods.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDeleteGoods.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeleteGoods.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeleteGoods.Location = New System.Drawing.Point(451, 56)
        Me.cmdDeleteGoods.Name = "cmdDeleteGoods"
        Me.cmdDeleteGoods.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeleteGoods.Size = New System.Drawing.Size(57, 26)
        Me.cmdDeleteGoods.TabIndex = 7
        Me.cmdDeleteGoods.Text = "Delete"
        Me.cmdDeleteGoods.UseVisualStyleBackColor = False
        '
        'cmdEditBrands
        '
        Me.cmdEditBrands.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdEditBrands.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEditBrands.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEditBrands.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEditBrands.Location = New System.Drawing.Point(223, 339)
        Me.cmdEditBrands.Name = "cmdEditBrands"
        Me.cmdEditBrands.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEditBrands.Size = New System.Drawing.Size(78, 21)
        Me.cmdEditBrands.TabIndex = 16
        Me.cmdEditBrands.Text = "Brands"
        Me.cmdEditBrands.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(27, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(227, 17)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Goods In Care-  Add/Update"
        '
        'frmGoodsInCare
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(591, 447)
        Me.ControlBox = False
        Me.Controls.Add(Me.FrameGoods)
        Me.Controls.Add(Me.Label6)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGoodsInCare"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "frmGoodsInCare"
        Me.FrameGoods.ResumeLayout(False)
        Me.FrameGoods.PerformLayout()
        CType(Me.dgvGoods_old, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents FrameGoods As System.Windows.Forms.GroupBox
    Public WithEvents cmdDeleteGoods As System.Windows.Forms.Button
    Public WithEvents CmdCreateGoodsType As System.Windows.Forms.Button
    Public WithEvents cmdEditBrands As System.Windows.Forms.Button
    Public WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvGoods_old As System.Windows.Forms.DataGridView
    Friend WithEvents txtExtraDefs As System.Windows.Forms.TextBox
    Friend WithEvents GoodsType_old As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Brand_old As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Model_old As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Serial_old As System.Windows.Forms.DataGridViewTextBoxColumn
#End Region
End Class