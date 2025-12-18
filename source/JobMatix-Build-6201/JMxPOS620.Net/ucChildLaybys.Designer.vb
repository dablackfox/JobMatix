<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildLaybys
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelHdr = New System.Windows.Forms.Panel()
        Me.chkShowCompleted = New System.Windows.Forms.CheckBox()
        Me.labSubHdr = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvLaybys = New System.Windows.Forms.DataGridView()
        Me.laybyCustomer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.laybyAvailCredit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LabyId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.laybyDateStarted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LaybyIsDelivered = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.laybyTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.laybyCustomerId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.labyDateDelivered = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.laybyItems = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.laybyDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.panelLayby = New System.Windows.Forms.Panel()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtDateDelivered = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.panelFooter = New System.Windows.Forms.Panel()
        Me.btnPrintLabel = New System.Windows.Forms.Button()
        Me.btnCancelLayby = New System.Windows.Forms.Button()
        Me.txtLaybyItems = New System.Windows.Forms.TextBox()
        Me.txtLaybyTotal = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtLaybyDate = New System.Windows.Forms.TextBox()
        Me.txtLaybyId = New System.Windows.Forms.TextBox()
        Me.txtLaybyCustomer = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.labHdrInfo = New System.Windows.Forms.Label()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.TabPageCustomerLaybys = New System.Windows.Forms.TabPage()
        Me.TabPageLaybyStock = New System.Windows.Forms.TabPage()
        Me.frameBrowse = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmdClearStockSearch = New System.Windows.Forms.Button()
        Me.cmdStockSearch = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtStockSearch = New System.Windows.Forms.TextBox()
        Me.dgvStockList = New System.Windows.Forms.DataGridView()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.labRecCount = New System.Windows.Forms.Label()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.panelHdr.SuspendLayout()
        CType(Me.dgvLaybys, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelLayby.SuspendLayout()
        Me.panelFooter.SuspendLayout()
        Me.TabControlMain.SuspendLayout()
        Me.TabPageCustomerLaybys.SuspendLayout()
        Me.TabPageLaybyStock.SuspendLayout()
        Me.frameBrowse.SuspendLayout()
        CType(Me.dgvStockList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelHdr
        '
        Me.panelHdr.BackColor = System.Drawing.Color.MediumPurple
        Me.panelHdr.Controls.Add(Me.chkShowCompleted)
        Me.panelHdr.Controls.Add(Me.labSubHdr)
        Me.panelHdr.Controls.Add(Me.Label1)
        Me.panelHdr.Location = New System.Drawing.Point(0, 0)
        Me.panelHdr.Name = "panelHdr"
        Me.panelHdr.Size = New System.Drawing.Size(1001, 53)
        Me.panelHdr.TabIndex = 0
        '
        'chkShowCompleted
        '
        Me.chkShowCompleted.BackColor = System.Drawing.Color.Thistle
        Me.chkShowCompleted.CausesValidation = False
        Me.chkShowCompleted.Location = New System.Drawing.Point(544, 18)
        Me.chkShowCompleted.Name = "chkShowCompleted"
        Me.chkShowCompleted.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.chkShowCompleted.Size = New System.Drawing.Size(161, 21)
        Me.chkShowCompleted.TabIndex = 2
        Me.chkShowCompleted.Text = "Include Completed Laybys"
        Me.chkShowCompleted.UseVisualStyleBackColor = False
        '
        'labSubHdr
        '
        Me.labSubHdr.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSubHdr.ForeColor = System.Drawing.Color.Yellow
        Me.labSubHdr.Location = New System.Drawing.Point(124, 20)
        Me.labSubHdr.Name = "labSubHdr"
        Me.labSubHdr.Size = New System.Drawing.Size(416, 20)
        Me.labSubHdr.TabIndex = 1
        Me.labSubHdr.Text = "View of Layby's currently active.."
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(14, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Layby's"
        '
        'dgvLaybys
        '
        Me.dgvLaybys.AllowUserToAddRows = False
        Me.dgvLaybys.AllowUserToDeleteRows = False
        Me.dgvLaybys.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvLaybys.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvLaybys.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvLaybys.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvLaybys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLaybys.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.laybyCustomer, Me.laybyAvailCredit, Me.LabyId, Me.laybyDateStarted, Me.LaybyIsDelivered, Me.laybyTotal, Me.laybyCustomerId, Me.labyDateDelivered, Me.laybyItems, Me.laybyDiscount})
        Me.dgvLaybys.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvLaybys.Location = New System.Drawing.Point(6, 6)
        Me.dgvLaybys.MultiSelect = False
        Me.dgvLaybys.Name = "dgvLaybys"
        Me.dgvLaybys.ReadOnly = True
        Me.dgvLaybys.RowHeadersWidth = 33
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvLaybys.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvLaybys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLaybys.Size = New System.Drawing.Size(608, 503)
        Me.dgvLaybys.StandardTab = True
        Me.dgvLaybys.TabIndex = 2
        '
        'laybyCustomer
        '
        Me.laybyCustomer.FillWeight = 80.0!
        Me.laybyCustomer.HeaderText = "Customer"
        Me.laybyCustomer.Name = "laybyCustomer"
        Me.laybyCustomer.ReadOnly = True
        '
        'laybyAvailCredit
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.laybyAvailCredit.DefaultCellStyle = DataGridViewCellStyle2
        Me.laybyAvailCredit.FillWeight = 57.90609!
        Me.laybyAvailCredit.HeaderText = "Available Credit"
        Me.laybyAvailCredit.Name = "laybyAvailCredit"
        Me.laybyAvailCredit.ReadOnly = True
        '
        'LabyId
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.LabyId.DefaultCellStyle = DataGridViewCellStyle3
        Me.LabyId.FillWeight = 43.42957!
        Me.LabyId.HeaderText = "Layby Id"
        Me.LabyId.Name = "LabyId"
        Me.LabyId.ReadOnly = True
        '
        'laybyDateStarted
        '
        Me.laybyDateStarted.FillWeight = 80.0!
        Me.laybyDateStarted.HeaderText = "Date Started"
        Me.laybyDateStarted.Name = "laybyDateStarted"
        Me.laybyDateStarted.ReadOnly = True
        '
        'LaybyIsDelivered
        '
        Me.LaybyIsDelivered.FillWeight = 20.0!
        Me.LaybyIsDelivered.HeaderText = "Delivered"
        Me.LaybyIsDelivered.Name = "LaybyIsDelivered"
        Me.LaybyIsDelivered.ReadOnly = True
        '
        'laybyTotal
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.laybyTotal.DefaultCellStyle = DataGridViewCellStyle4
        Me.laybyTotal.FillWeight = 70.0!
        Me.laybyTotal.HeaderText = "Layby Total Amt"
        Me.laybyTotal.Name = "laybyTotal"
        Me.laybyTotal.ReadOnly = True
        '
        'laybyCustomerId
        '
        Me.laybyCustomerId.HeaderText = "laybyCustomerId"
        Me.laybyCustomerId.Name = "laybyCustomerId"
        Me.laybyCustomerId.ReadOnly = True
        Me.laybyCustomerId.Visible = False
        '
        'labyDateDelivered
        '
        Me.labyDateDelivered.HeaderText = "Date delivered"
        Me.labyDateDelivered.Name = "labyDateDelivered"
        Me.labyDateDelivered.ReadOnly = True
        Me.labyDateDelivered.Visible = False
        '
        'laybyItems
        '
        Me.laybyItems.HeaderText = "laybyItems"
        Me.laybyItems.Name = "laybyItems"
        Me.laybyItems.ReadOnly = True
        Me.laybyItems.Visible = False
        '
        'laybyDiscount
        '
        Me.laybyDiscount.HeaderText = "Discount"
        Me.laybyDiscount.Name = "laybyDiscount"
        Me.laybyDiscount.ReadOnly = True
        Me.laybyDiscount.Visible = False
        '
        'btnOK
        '
        Me.btnOK.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnOK.CausesValidation = False
        Me.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOK.Location = New System.Drawing.Point(226, 23)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(60, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(226, 54)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(60, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'panelLayby
        '
        Me.panelLayby.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.panelLayby.CausesValidation = False
        Me.panelLayby.Controls.Add(Me.txtDiscount)
        Me.panelLayby.Controls.Add(Me.Label9)
        Me.panelLayby.Controls.Add(Me.txtDateDelivered)
        Me.panelLayby.Controls.Add(Me.Label2)
        Me.panelLayby.Controls.Add(Me.panelFooter)
        Me.panelLayby.Controls.Add(Me.txtLaybyItems)
        Me.panelLayby.Controls.Add(Me.txtLaybyTotal)
        Me.panelLayby.Controls.Add(Me.Label8)
        Me.panelLayby.Controls.Add(Me.Label7)
        Me.panelLayby.Controls.Add(Me.txtLaybyDate)
        Me.panelLayby.Controls.Add(Me.txtLaybyId)
        Me.panelLayby.Controls.Add(Me.txtLaybyCustomer)
        Me.panelLayby.Controls.Add(Me.Label6)
        Me.panelLayby.Controls.Add(Me.Label5)
        Me.panelLayby.Controls.Add(Me.Label4)
        Me.panelLayby.Controls.Add(Me.Label3)
        Me.panelLayby.Location = New System.Drawing.Point(623, 37)
        Me.panelLayby.Name = "panelLayby"
        Me.panelLayby.Size = New System.Drawing.Size(360, 484)
        Me.panelLayby.TabIndex = 5
        '
        'txtDiscount
        '
        Me.txtDiscount.BackColor = System.Drawing.Color.Lavender
        Me.txtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDiscount.Location = New System.Drawing.Point(170, 29)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.ReadOnly = True
        Me.txtDiscount.Size = New System.Drawing.Size(68, 13)
        Me.txtDiscount.TabIndex = 1
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(182, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 22)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Discount"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDateDelivered
        '
        Me.txtDateDelivered.BackColor = System.Drawing.Color.Lavender
        Me.txtDateDelivered.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDateDelivered.Location = New System.Drawing.Point(150, 100)
        Me.txtDateDelivered.Name = "txtDateDelivered"
        Me.txtDateDelivered.ReadOnly = True
        Me.txtDateDelivered.Size = New System.Drawing.Size(97, 13)
        Me.txtDateDelivered.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(147, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 22)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Date Delivered"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panelFooter
        '
        Me.panelFooter.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.panelFooter.CausesValidation = False
        Me.panelFooter.Controls.Add(Me.btnPrintLabel)
        Me.panelFooter.Controls.Add(Me.btnCancel)
        Me.panelFooter.Controls.Add(Me.btnCancelLayby)
        Me.panelFooter.Controls.Add(Me.btnOK)
        Me.panelFooter.Location = New System.Drawing.Point(17, 371)
        Me.panelFooter.Name = "panelFooter"
        Me.panelFooter.Size = New System.Drawing.Size(321, 101)
        Me.panelFooter.TabIndex = 7
        '
        'btnPrintLabel
        '
        Me.btnPrintLabel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPrintLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintLabel.Location = New System.Drawing.Point(116, 23)
        Me.btnPrintLabel.Name = "btnPrintLabel"
        Me.btnPrintLabel.Size = New System.Drawing.Size(72, 54)
        Me.btnPrintLabel.TabIndex = 1
        Me.btnPrintLabel.Text = "Print Label"
        Me.btnPrintLabel.UseVisualStyleBackColor = False
        '
        'btnCancelLayby
        '
        Me.btnCancelLayby.BackColor = System.Drawing.Color.LavenderBlush
        Me.btnCancelLayby.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelLayby.Location = New System.Drawing.Point(19, 23)
        Me.btnCancelLayby.Name = "btnCancelLayby"
        Me.btnCancelLayby.Size = New System.Drawing.Size(72, 54)
        Me.btnCancelLayby.TabIndex = 0
        Me.btnCancelLayby.Text = "Cancel this Layby Forever"
        Me.btnCancelLayby.UseVisualStyleBackColor = False
        '
        'txtLaybyItems
        '
        Me.txtLaybyItems.BackColor = System.Drawing.Color.Lavender
        Me.txtLaybyItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLaybyItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLaybyItems.Location = New System.Drawing.Point(17, 144)
        Me.txtLaybyItems.Multiline = True
        Me.txtLaybyItems.Name = "txtLaybyItems"
        Me.txtLaybyItems.ReadOnly = True
        Me.txtLaybyItems.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLaybyItems.Size = New System.Drawing.Size(321, 221)
        Me.txtLaybyItems.TabIndex = 6
        '
        'txtLaybyTotal
        '
        Me.txtLaybyTotal.BackColor = System.Drawing.Color.Lavender
        Me.txtLaybyTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLaybyTotal.Location = New System.Drawing.Point(254, 29)
        Me.txtLaybyTotal.Name = "txtLaybyTotal"
        Me.txtLaybyTotal.ReadOnly = True
        Me.txtLaybyTotal.Size = New System.Drawing.Size(74, 13)
        Me.txtLaybyTotal.TabIndex = 2
        Me.txtLaybyTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(261, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 22)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Total Value"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(14, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 14)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Selected Layby"
        '
        'txtLaybyDate
        '
        Me.txtLaybyDate.BackColor = System.Drawing.Color.Lavender
        Me.txtLaybyDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLaybyDate.Location = New System.Drawing.Point(20, 100)
        Me.txtLaybyDate.Name = "txtLaybyDate"
        Me.txtLaybyDate.ReadOnly = True
        Me.txtLaybyDate.Size = New System.Drawing.Size(97, 13)
        Me.txtLaybyDate.TabIndex = 4
        '
        'txtLaybyId
        '
        Me.txtLaybyId.BackColor = System.Drawing.Color.Lavender
        Me.txtLaybyId.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLaybyId.Location = New System.Drawing.Point(117, 29)
        Me.txtLaybyId.Name = "txtLaybyId"
        Me.txtLaybyId.ReadOnly = True
        Me.txtLaybyId.Size = New System.Drawing.Size(40, 13)
        Me.txtLaybyId.TabIndex = 0
        Me.txtLaybyId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtLaybyCustomer
        '
        Me.txtLaybyCustomer.BackColor = System.Drawing.Color.Lavender
        Me.txtLaybyCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLaybyCustomer.Location = New System.Drawing.Point(20, 57)
        Me.txtLaybyCustomer.Name = "txtLaybyCustomer"
        Me.txtLaybyCustomer.Size = New System.Drawing.Size(295, 13)
        Me.txtLaybyCustomer.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 22)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Date Started"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(108, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 19)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "LaybyId"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 41)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 17)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Customer"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 125)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 18)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Items Reserved"
        '
        'labHdrInfo
        '
        Me.labHdrInfo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labHdrInfo.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdrInfo.ForeColor = System.Drawing.Color.Black
        Me.labHdrInfo.Location = New System.Drawing.Point(620, 7)
        Me.labHdrInfo.Name = "labHdrInfo"
        Me.labHdrInfo.Padding = New System.Windows.Forms.Padding(4, 3, 0, 0)
        Me.labHdrInfo.Size = New System.Drawing.Size(363, 27)
        Me.labHdrInfo.TabIndex = 6
        Me.labHdrInfo.Text = "Select the Layby from Grid to view details.."
        '
        'TabControlMain
        '
        Me.TabControlMain.CausesValidation = False
        Me.TabControlMain.Controls.Add(Me.TabPageCustomerLaybys)
        Me.TabControlMain.Controls.Add(Me.TabPageLaybyStock)
        Me.TabControlMain.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlMain.Location = New System.Drawing.Point(3, 56)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(998, 554)
        Me.TabControlMain.TabIndex = 7
        '
        'TabPageCustomerLaybys
        '
        Me.TabPageCustomerLaybys.CausesValidation = False
        Me.TabPageCustomerLaybys.Controls.Add(Me.panelLayby)
        Me.TabPageCustomerLaybys.Controls.Add(Me.dgvLaybys)
        Me.TabPageCustomerLaybys.Controls.Add(Me.labHdrInfo)
        Me.TabPageCustomerLaybys.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageCustomerLaybys.Location = New System.Drawing.Point(4, 23)
        Me.TabPageCustomerLaybys.Name = "TabPageCustomerLaybys"
        Me.TabPageCustomerLaybys.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCustomerLaybys.Size = New System.Drawing.Size(990, 527)
        Me.TabPageCustomerLaybys.TabIndex = 0
        Me.TabPageCustomerLaybys.Text = "Customers-Laybys"
        Me.TabPageCustomerLaybys.UseVisualStyleBackColor = True
        '
        'TabPageLaybyStock
        '
        Me.TabPageLaybyStock.Controls.Add(Me.frameBrowse)
        Me.TabPageLaybyStock.Location = New System.Drawing.Point(4, 23)
        Me.TabPageLaybyStock.Name = "TabPageLaybyStock"
        Me.TabPageLaybyStock.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageLaybyStock.Size = New System.Drawing.Size(990, 527)
        Me.TabPageLaybyStock.TabIndex = 1
        Me.TabPageLaybyStock.Text = "Stock Items on Layby"
        Me.TabPageLaybyStock.UseVisualStyleBackColor = True
        '
        'frameBrowse
        '
        Me.frameBrowse.BackColor = System.Drawing.Color.White
        Me.frameBrowse.CausesValidation = False
        Me.frameBrowse.Controls.Add(Me.Label11)
        Me.frameBrowse.Controls.Add(Me.Label22)
        Me.frameBrowse.Controls.Add(Me.Label21)
        Me.frameBrowse.Controls.Add(Me.cmdClearStockSearch)
        Me.frameBrowse.Controls.Add(Me.cmdStockSearch)
        Me.frameBrowse.Controls.Add(Me.Label10)
        Me.frameBrowse.Controls.Add(Me.txtStockSearch)
        Me.frameBrowse.Controls.Add(Me.dgvStockList)
        Me.frameBrowse.Controls.Add(Me.txtFind)
        Me.frameBrowse.Controls.Add(Me.labRecCount)
        Me.frameBrowse.Controls.Add(Me.LabFind)
        Me.frameBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameBrowse.Location = New System.Drawing.Point(3, 3)
        Me.frameBrowse.Name = "frameBrowse"
        Me.frameBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameBrowse.Size = New System.Drawing.Size(981, 511)
        Me.frameBrowse.TabIndex = 22
        Me.frameBrowse.TabStop = False
        Me.frameBrowse.Text = "FrameBrowse"
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(481, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(178, 68)
        Me.Label11.TabIndex = 83
        Me.Label11.Text = "These items are all counted as being in-stock in Stock Admin, but are notionally " & _
    "reserved for the various Layby's.."
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(345, 13)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(83, 15)
        Me.Label22.TabIndex = 82
        Me.Label22.Text = "Records found."
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(173, 63)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(121, 12)
        Me.Label21.TabIndex = 81
        Me.Label21.Text = "Full Text Filter (Srch):"
        '
        'cmdClearStockSearch
        '
        Me.cmdClearStockSearch.BackColor = System.Drawing.Color.LavenderBlush
        Me.cmdClearStockSearch.CausesValidation = False
        Me.cmdClearStockSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearStockSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClearStockSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearStockSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearStockSearch.Location = New System.Drawing.Point(375, 48)
        Me.cmdClearStockSearch.Name = "cmdClearStockSearch"
        Me.cmdClearStockSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearStockSearch.Size = New System.Drawing.Size(53, 23)
        Me.cmdClearStockSearch.TabIndex = 80
        Me.cmdClearStockSearch.Text = "Clear"
        Me.cmdClearStockSearch.UseVisualStyleBackColor = False
        '
        'cmdStockSearch
        '
        Me.cmdStockSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.cmdStockSearch.CausesValidation = False
        Me.cmdStockSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdStockSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdStockSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStockSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdStockSearch.Location = New System.Drawing.Point(375, 83)
        Me.cmdStockSearch.Name = "cmdStockSearch"
        Me.cmdStockSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdStockSearch.Size = New System.Drawing.Size(53, 23)
        Me.cmdStockSearch.TabIndex = 79
        Me.cmdStockSearch.Text = "Search"
        Me.cmdStockSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdStockSearch.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(134, 20)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Layby Stock List"
        '
        'txtStockSearch
        '
        Me.txtStockSearch.AcceptsReturn = True
        Me.txtStockSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtStockSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStockSearch.CausesValidation = False
        Me.txtStockSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStockSearch.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStockSearch.Location = New System.Drawing.Point(167, 80)
        Me.txtStockSearch.MaxLength = 0
        Me.txtStockSearch.Name = "txtStockSearch"
        Me.txtStockSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStockSearch.Size = New System.Drawing.Size(195, 26)
        Me.txtStockSearch.TabIndex = 78
        Me.txtStockSearch.Text = "txtStockSearch"
        '
        'dgvStockList
        '
        Me.dgvStockList.AllowUserToAddRows = False
        Me.dgvStockList.AllowUserToDeleteRows = False
        Me.dgvStockList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvStockList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvStockList.ColumnHeadersHeight = 18
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvStockList.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvStockList.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvStockList.Location = New System.Drawing.Point(4, 110)
        Me.dgvStockList.MultiSelect = False
        Me.dgvStockList.Name = "dgvStockList"
        Me.dgvStockList.ReadOnly = True
        Me.dgvStockList.RowHeadersWidth = 17
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvStockList.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvStockList.RowTemplate.Height = 19
        Me.dgvStockList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvStockList.Size = New System.Drawing.Size(971, 391)
        Me.dgvStockList.StandardTab = True
        Me.dgvStockList.TabIndex = 4
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.Gainsboro
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.CausesValidation = False
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(6, 87)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(136, 19)
        Me.txtFind.TabIndex = 2
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(298, 13)
        Me.labRecCount.Name = "labRecCount"
        Me.labRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCount.Size = New System.Drawing.Size(44, 17)
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
        Me.LabFind.Location = New System.Drawing.Point(7, 60)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(135, 25)
        Me.LabFind.TabIndex = 18
        Me.LabFind.Text = "LabFind"
        '
        'ucChildLaybys
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LavenderBlush
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CausesValidation = False
        Me.Controls.Add(Me.TabControlMain)
        Me.Controls.Add(Me.panelHdr)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildLaybys"
        Me.Size = New System.Drawing.Size(1004, 624)
        Me.panelHdr.ResumeLayout(False)
        CType(Me.dgvLaybys, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelLayby.ResumeLayout(False)
        Me.panelLayby.PerformLayout()
        Me.panelFooter.ResumeLayout(False)
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPageCustomerLaybys.ResumeLayout(False)
        Me.TabPageLaybyStock.ResumeLayout(False)
        Me.frameBrowse.ResumeLayout(False)
        Me.frameBrowse.PerformLayout()
        CType(Me.dgvStockList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelHdr As System.Windows.Forms.Panel
    Friend WithEvents labSubHdr As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvLaybys As System.Windows.Forms.DataGridView
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents panelLayby As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtLaybyDate As System.Windows.Forms.TextBox
    Friend WithEvents txtLaybyId As System.Windows.Forms.TextBox
    Friend WithEvents txtLaybyCustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents panelFooter As System.Windows.Forms.Panel
    Friend WithEvents txtLaybyItems As System.Windows.Forms.TextBox
    Friend WithEvents txtLaybyTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnCancelLayby As System.Windows.Forms.Button
    Friend WithEvents btnPrintLabel As System.Windows.Forms.Button
    Friend WithEvents labHdrInfo As System.Windows.Forms.Label
    Friend WithEvents chkShowCompleted As System.Windows.Forms.CheckBox
    Friend WithEvents txtDateDelivered As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents laybyCustomer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents laybyAvailCredit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LabyId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents laybyDateStarted As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LaybyIsDelivered As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents laybyTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents laybyCustomerId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents labyDateDelivered As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents laybyItems As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents laybyDiscount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPageCustomerLaybys As System.Windows.Forms.TabPage
    Friend WithEvents TabPageLaybyStock As System.Windows.Forms.TabPage
    Public WithEvents frameBrowse As System.Windows.Forms.GroupBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents cmdClearStockSearch As System.Windows.Forms.Button
    Public WithEvents cmdStockSearch As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents txtStockSearch As System.Windows.Forms.TextBox
    Friend WithEvents dgvStockList As System.Windows.Forms.DataGridView
    Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents labRecCount As System.Windows.Forms.Label
    Public WithEvents LabFind As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
