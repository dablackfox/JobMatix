<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildSerialLookup
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.panelHdr = New System.Windows.Forms.Panel()
        Me.txtSerialGoodsInfo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.labSerialStatus = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dgvTrail = New System.Windows.Forms.DataGridView()
        Me.TrailDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TranType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.type_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Rm_History = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.labSerial_id = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnLookup = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.txtSerialNo = New System.Windows.Forms.TextBox()
        Me.labDescription = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSerialSrch = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.frameSerialAudit = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.labResultsCount = New System.Windows.Forms.Label()
        Me.chkInstockOnly = New System.Windows.Forms.CheckBox()
        Me.txtResultsFind = New System.Windows.Forms.TextBox()
        Me.LabResultsFind = New System.Windows.Forms.Label()
        Me.dgvSerials = New System.Windows.Forms.DataGridView()
        Me.cmdSerialClear = New System.Windows.Forms.Button()
        Me.txtSerialArg = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.labSerialSearch = New System.Windows.Forms.Label()
        Me.labVerify = New System.Windows.Forms.Label()
        Me.panelHdr.SuspendLayout()
        CType(Me.dgvTrail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frameSerialAudit.SuspendLayout()
        CType(Me.dgvSerials, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(207, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Information for Stock Serial:"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Barcode: "
        '
        'labStatus
        '
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.labStatus.Location = New System.Drawing.Point(508, 7)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labStatus.Size = New System.Drawing.Size(372, 40)
        Me.labStatus.TabIndex = 3
        Me.labStatus.Text = "labStatus"
        '
        'panelHdr
        '
        Me.panelHdr.BackColor = System.Drawing.Color.Lavender
        Me.panelHdr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panelHdr.CausesValidation = False
        Me.panelHdr.Controls.Add(Me.txtSerialGoodsInfo)
        Me.panelHdr.Controls.Add(Me.Label4)
        Me.panelHdr.Controls.Add(Me.Label3)
        Me.panelHdr.Controls.Add(Me.labSerialStatus)
        Me.panelHdr.Controls.Add(Me.Label6)
        Me.panelHdr.Controls.Add(Me.Label9)
        Me.panelHdr.Controls.Add(Me.dgvTrail)
        Me.panelHdr.Controls.Add(Me.btnShow)
        Me.panelHdr.Controls.Add(Me.labSerial_id)
        Me.panelHdr.Controls.Add(Me.Label8)
        Me.panelHdr.Controls.Add(Me.btnLookup)
        Me.panelHdr.Controls.Add(Me.Label5)
        Me.panelHdr.Controls.Add(Me.txtBarcode)
        Me.panelHdr.Controls.Add(Me.txtSerialNo)
        Me.panelHdr.Controls.Add(Me.labDescription)
        Me.panelHdr.Controls.Add(Me.Label1)
        Me.panelHdr.Controls.Add(Me.Label2)
        Me.panelHdr.Location = New System.Drawing.Point(511, 76)
        Me.panelHdr.Name = "panelHdr"
        Me.panelHdr.Size = New System.Drawing.Size(468, 520)
        Me.panelHdr.TabIndex = 5
        Me.panelHdr.TabStop = True
        '
        'txtSerialGoodsInfo
        '
        Me.txtSerialGoodsInfo.BackColor = System.Drawing.Color.AliceBlue
        Me.txtSerialGoodsInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSerialGoodsInfo.CausesValidation = False
        Me.txtSerialGoodsInfo.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialGoodsInfo.Location = New System.Drawing.Point(94, 191)
        Me.txtSerialGoodsInfo.Multiline = True
        Me.txtSerialGoodsInfo.Name = "txtSerialGoodsInfo"
        Me.txtSerialGoodsInfo.Size = New System.Drawing.Size(352, 68)
        Me.txtSerialGoodsInfo.TabIndex = 16
        Me.txtSerialGoodsInfo.Text = "txtSerialGoodsInfo"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 190)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 17)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Purchased:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(148, 281)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Select Trail Row to "
        '
        'labSerialStatus
        '
        Me.labSerialStatus.BackColor = System.Drawing.Color.AliceBlue
        Me.labSerialStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSerialStatus.Location = New System.Drawing.Point(91, 163)
        Me.labSerialStatus.Name = "labSerialStatus"
        Me.labSerialStatus.Size = New System.Drawing.Size(161, 17)
        Me.labSerialStatus.TabIndex = 14
        Me.labSerialStatus.Text = "labSerialStatus"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 281)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(126, 17)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Serial History Trail"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(13, 163)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(89, 17)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Serial Status:"
        '
        'dgvTrail
        '
        Me.dgvTrail.AllowUserToAddRows = False
        Me.dgvTrail.AllowUserToDeleteRows = False
        Me.dgvTrail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTrail.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvTrail.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTrail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTrail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TrailDate, Me.TranType, Me.type_id, Me.Rm_History})
        Me.dgvTrail.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvTrail.Location = New System.Drawing.Point(10, 301)
        Me.dgvTrail.Name = "dgvTrail"
        Me.dgvTrail.ReadOnly = True
        Me.dgvTrail.RowHeadersWidth = 17
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvTrail.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTrail.RowTemplate.Height = 17
        Me.dgvTrail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTrail.Size = New System.Drawing.Size(448, 197)
        Me.dgvTrail.TabIndex = 8
        '
        'TrailDate
        '
        Me.TrailDate.FillWeight = 80.0!
        Me.TrailDate.HeaderText = "Trail Date"
        Me.TrailDate.Name = "TrailDate"
        Me.TrailDate.ReadOnly = True
        '
        'TranType
        '
        Me.TranType.FillWeight = 80.0!
        Me.TranType.HeaderText = "Trans. Type"
        Me.TranType.Name = "TranType"
        Me.TranType.ReadOnly = True
        '
        'type_id
        '
        Me.type_id.FillWeight = 60.0!
        Me.type_id.HeaderText = "Trans. Id"
        Me.type_id.Name = "type_id"
        Me.type_id.ReadOnly = True
        '
        'Rm_History
        '
        Me.Rm_History.FillWeight = 200.0!
        Me.Rm_History.HeaderText = "RM/RA History"
        Me.Rm_History.Name = "Rm_History"
        Me.Rm_History.ReadOnly = True
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(247, 274)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(81, 21)
        Me.btnShow.TabIndex = 10
        Me.btnShow.Text = "Show Invoice"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'labSerial_id
        '
        Me.labSerial_id.BackColor = System.Drawing.Color.AliceBlue
        Me.labSerial_id.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSerial_id.Location = New System.Drawing.Point(322, 51)
        Me.labSerial_id.Name = "labSerial_id"
        Me.labSerial_id.Size = New System.Drawing.Size(71, 17)
        Me.labSerial_id.TabIndex = 12
        Me.labSerial_id.Text = "labSerial_id"
        Me.labSerial_id.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(317, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 17)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "SerialAudit Id:"
        '
        'btnLookup
        '
        Me.btnLookup.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnLookup.Location = New System.Drawing.Point(220, 114)
        Me.btnLookup.Name = "btnLookup"
        Me.btnLookup.Size = New System.Drawing.Size(72, 28)
        Me.btnLookup.TabIndex = 10
        Me.btnLookup.Text = "Lookup"
        Me.ToolTip1.SetToolTip(Me.btnLookup, "Search for this Serial")
        Me.btnLookup.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 18)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Serial No:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBarcode
        '
        Me.txtBarcode.BackColor = System.Drawing.Color.AliceBlue
        Me.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBarcode.CausesValidation = False
        Me.txtBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcode.Location = New System.Drawing.Point(14, 127)
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.Size = New System.Drawing.Size(175, 20)
        Me.txtBarcode.TabIndex = 8
        Me.txtBarcode.Text = "txtBarcode"
        '
        'txtSerialNo
        '
        Me.txtSerialNo.BackColor = System.Drawing.Color.AliceBlue
        Me.txtSerialNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSerialNo.CausesValidation = False
        Me.txtSerialNo.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialNo.Location = New System.Drawing.Point(14, 50)
        Me.txtSerialNo.Name = "txtSerialNo"
        Me.txtSerialNo.Size = New System.Drawing.Size(278, 20)
        Me.txtSerialNo.TabIndex = 7
        Me.txtSerialNo.Text = "txtSerialNo"
        '
        'labDescription
        '
        Me.labDescription.BackColor = System.Drawing.Color.AliceBlue
        Me.labDescription.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDescription.Location = New System.Drawing.Point(11, 80)
        Me.labDescription.Name = "labDescription"
        Me.labDescription.Size = New System.Drawing.Size(435, 17)
        Me.labDescription.TabIndex = 6
        Me.labDescription.Text = "labDescription"
        '
        'cmdSerialSrch
        '
        Me.cmdSerialSrch.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSerialSrch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSerialSrch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSerialSrch.Location = New System.Drawing.Point(426, 75)
        Me.cmdSerialSrch.Name = "cmdSerialSrch"
        Me.cmdSerialSrch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSerialSrch.Size = New System.Drawing.Size(49, 25)
        Me.cmdSerialSrch.TabIndex = 13
        Me.cmdSerialSrch.Text = "Search"
        Me.ToolTip1.SetToolTip(Me.cmdSerialSrch, "Search/Refresh")
        Me.cmdSerialSrch.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.Location = New System.Drawing.Point(910, 15)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 24)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnOK.Location = New System.Drawing.Point(912, 46)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(59, 24)
        Me.btnOK.TabIndex = 6
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'frameSerialAudit
        '
        Me.frameSerialAudit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameSerialAudit.Controls.Add(Me.Label22)
        Me.frameSerialAudit.Controls.Add(Me.labResultsCount)
        Me.frameSerialAudit.Controls.Add(Me.chkInstockOnly)
        Me.frameSerialAudit.Controls.Add(Me.txtResultsFind)
        Me.frameSerialAudit.Controls.Add(Me.LabResultsFind)
        Me.frameSerialAudit.Controls.Add(Me.dgvSerials)
        Me.frameSerialAudit.Controls.Add(Me.cmdSerialClear)
        Me.frameSerialAudit.Controls.Add(Me.cmdSerialSrch)
        Me.frameSerialAudit.Controls.Add(Me.txtSerialArg)
        Me.frameSerialAudit.Controls.Add(Me.Label10)
        Me.frameSerialAudit.Controls.Add(Me.Label11)
        Me.frameSerialAudit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameSerialAudit.Location = New System.Drawing.Point(3, 49)
        Me.frameSerialAudit.Name = "frameSerialAudit"
        Me.frameSerialAudit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameSerialAudit.Size = New System.Drawing.Size(499, 547)
        Me.frameSerialAudit.TabIndex = 12
        Me.frameSerialAudit.TabStop = False
        Me.frameSerialAudit.Text = "FrameSerialAudit"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(437, 54)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(45, 15)
        Me.Label22.TabIndex = 84
        Me.Label22.Text = "Records."
        '
        'labResultsCount
        '
        Me.labResultsCount.BackColor = System.Drawing.Color.Transparent
        Me.labResultsCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labResultsCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labResultsCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labResultsCount.Location = New System.Drawing.Point(387, 54)
        Me.labResultsCount.Name = "labResultsCount"
        Me.labResultsCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labResultsCount.Size = New System.Drawing.Size(44, 14)
        Me.labResultsCount.TabIndex = 83
        Me.labResultsCount.Text = "labResultsCount"
        Me.labResultsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkInstockOnly
        '
        Me.chkInstockOnly.Checked = True
        Me.chkInstockOnly.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInstockOnly.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInstockOnly.Location = New System.Drawing.Point(313, 14)
        Me.chkInstockOnly.Name = "chkInstockOnly"
        Me.chkInstockOnly.Size = New System.Drawing.Size(119, 33)
        Me.chkInstockOnly.TabIndex = 21
        Me.chkInstockOnly.Text = "Show only those Serials  In stock.."
        Me.chkInstockOnly.UseVisualStyleBackColor = True
        '
        'txtResultsFind
        '
        Me.txtResultsFind.AcceptsReturn = True
        Me.txtResultsFind.BackColor = System.Drawing.Color.White
        Me.txtResultsFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResultsFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtResultsFind.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResultsFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtResultsFind.Location = New System.Drawing.Point(15, 76)
        Me.txtResultsFind.MaxLength = 0
        Me.txtResultsFind.Name = "txtResultsFind"
        Me.txtResultsFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtResultsFind.Size = New System.Drawing.Size(167, 24)
        Me.txtResultsFind.TabIndex = 19
        Me.txtResultsFind.Text = "txtResultsFind"
        '
        'LabResultsFind
        '
        Me.LabResultsFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.LabResultsFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabResultsFind.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabResultsFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabResultsFind.Location = New System.Drawing.Point(12, 46)
        Me.LabResultsFind.Name = "LabResultsFind"
        Me.LabResultsFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabResultsFind.Size = New System.Drawing.Size(170, 33)
        Me.LabResultsFind.TabIndex = 20
        Me.LabResultsFind.Text = "LabResultsFind"
        '
        'dgvSerials
        '
        Me.dgvSerials.AllowUserToAddRows = False
        Me.dgvSerials.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvSerials.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvSerials.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSerials.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvSerials.ColumnHeadersHeight = 25
        Me.dgvSerials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvSerials.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvSerials.Location = New System.Drawing.Point(6, 108)
        Me.dgvSerials.MultiSelect = False
        Me.dgvSerials.Name = "dgvSerials"
        Me.dgvSerials.ReadOnly = True
        Me.dgvSerials.RowHeadersWidth = 24
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvSerials.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvSerials.RowTemplate.Height = 17
        Me.dgvSerials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSerials.Size = New System.Drawing.Size(487, 433)
        Me.dgvSerials.StandardTab = True
        Me.dgvSerials.TabIndex = 18
        '
        'cmdSerialClear
        '
        Me.cmdSerialClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSerialClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSerialClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSerialClear.Location = New System.Drawing.Point(395, 75)
        Me.cmdSerialClear.Name = "cmdSerialClear"
        Me.cmdSerialClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSerialClear.Size = New System.Drawing.Size(25, 25)
        Me.cmdSerialClear.TabIndex = 12
        Me.cmdSerialClear.Text = "X"
        Me.cmdSerialClear.UseVisualStyleBackColor = False
        '
        'txtSerialArg
        '
        Me.txtSerialArg.AcceptsReturn = True
        Me.txtSerialArg.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSerialArg.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSerialArg.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSerialArg.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSerialArg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSerialArg.Location = New System.Drawing.Point(204, 81)
        Me.txtSerialArg.MaxLength = 0
        Me.txtSerialArg.Name = "txtSerialArg"
        Me.txtSerialArg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSerialArg.Size = New System.Drawing.Size(170, 19)
        Me.txtSerialArg.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(15, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(276, 19)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Listing of all item Serials known to the System.."
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(202, 50)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(176, 29)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "Text Search in  SerialNo, Barcode,  Cat1 && Description cols."
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Lavender
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(5, 2)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(178, 45)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Serial Lookup"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labSerialSearch
        '
        Me.labSerialSearch.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSerialSearch.Location = New System.Drawing.Point(189, 18)
        Me.labSerialSearch.Name = "labSerialSearch"
        Me.labSerialSearch.Size = New System.Drawing.Size(73, 31)
        Me.labSerialSearch.TabIndex = 15
        Me.labSerialSearch.Text = "Searching for Serials"
        '
        'labVerify
        '
        Me.labVerify.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labVerify.Location = New System.Drawing.Point(528, 56)
        Me.labVerify.Name = "labVerify"
        Me.labVerify.Size = New System.Drawing.Size(126, 15)
        Me.labVerify.TabIndex = 16
        Me.labVerify.Text = "Verifying Serial Info.."
        '
        'ucChildSerialLookup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.labVerify)
        Me.Controls.Add(Me.labSerialSearch)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.frameSerialAudit)
        Me.Controls.Add(Me.panelHdr)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.labStatus)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildSerialLookup"
        Me.Size = New System.Drawing.Size(989, 604)
        Me.panelHdr.ResumeLayout(False)
        Me.panelHdr.PerformLayout()
        CType(Me.dgvTrail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frameSerialAudit.ResumeLayout(False)
        Me.frameSerialAudit.PerformLayout()
        CType(Me.dgvSerials, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents labStatus As System.Windows.Forms.Label
    Friend WithEvents panelHdr As System.Windows.Forms.Panel
    Friend WithEvents labDescription As System.Windows.Forms.Label
    Friend WithEvents txtBarcode As System.Windows.Forms.TextBox
    Friend WithEvents txtSerialNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnLookup As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents dgvTrail As System.Windows.Forms.DataGridView
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents labSerial_id As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents labSerialStatus As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents frameSerialAudit As System.Windows.Forms.GroupBox
    Public WithEvents cmdSerialClear As System.Windows.Forms.Button
    Public WithEvents cmdSerialSrch As System.Windows.Forms.Button
    Public WithEvents txtSerialArg As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dgvSerials As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents labSerialSearch As System.Windows.Forms.Label
    Friend WithEvents labVerify As System.Windows.Forms.Label
    Friend WithEvents txtSerialGoodsInfo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkInstockOnly As System.Windows.Forms.CheckBox
    Public WithEvents txtResultsFind As System.Windows.Forms.TextBox
    Public WithEvents LabResultsFind As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents labResultsCount As System.Windows.Forms.Label
    Friend WithEvents TrailDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TranType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents type_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rm_History As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
