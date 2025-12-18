<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCashup
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelBanner = New System.Windows.Forms.Panel()
        Me.labCurrentTill = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.labThisComputer = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.labStaffName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labMainTitle = New System.Windows.Forms.Label()
        Me.labCashDrawer = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnCashupCommit = New System.Windows.Forms.Button()
        Me.labDLLversion = New System.Windows.Forms.Label()
        Me.btnCashupCancel = New System.Windows.Forms.Button()
        Me.txtCashupComments = New System.Windows.Forms.TextBox()
        Me.panelCashup = New System.Windows.Forms.Panel()
        Me.panelTotals = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTotalCounted = New System.Windows.Forms.TextBox()
        Me.txtTotalDifference = New System.Windows.Forms.TextBox()
        Me.txtTotalReported = New System.Windows.Forms.TextBox()
        Me.labExplain = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.labComments = New System.Windows.Forms.Label()
        Me.labHelpCashup = New System.Windows.Forms.Label()
        Me.LabSalePayments = New System.Windows.Forms.Label()
        Me.dgvPaymentDetails = New System.Windows.Forms.DataGridView()
        Me.PaymentType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.amount_reported = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.amount_counted = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.amount_difference = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaymentType_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.validated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.TabPageTillBalance = New System.Windows.Forms.TabPage()
        Me.panelTillbalance = New System.Windows.Forms.Panel()
        Me.btnPrintRevenueSummary = New System.Windows.Forms.Button()
        Me.btnPrintSortedDetail = New System.Windows.Forms.Button()
        Me.btnPrintTillListing = New System.Windows.Forms.Button()
        Me.txtTillSummary = New System.Windows.Forms.TextBox()
        Me.labToPrint = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnPrintTillSummary = New System.Windows.Forms.Button()
        Me.TabPageEndOfDay = New System.Windows.Forms.TabPage()
        Me.TabPageCashUpSessions = New System.Windows.Forms.TabPage()
        Me.panelReports = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.labRecCount = New System.Windows.Forms.Label()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.dgvSessions = New System.Windows.Forms.DataGridView()
        Me.grpBoxPrintCmds = New System.Windows.Forms.GroupBox()
        Me.listViewSession = New System.Windows.Forms.ListView()
        Me.ColumnKey = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnReported = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnCounted = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnDiff = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnSessionReport = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtSessionComment = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboCashDrawers = New System.Windows.Forms.ComboBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpBoxPrinters = New System.Windows.Forms.GroupBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cboReceiptPrinters = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cboReportPrinters = New System.Windows.Forms.ComboBox()
        Me.grpBoxTerminal = New System.Windows.Forms.GroupBox()
        Me.labNotYOurTill = New System.Windows.Forms.Label()
        Me.labIsYourTill = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.labRefreshing = New System.Windows.Forms.Label()
        Me.panelBanner.SuspendLayout()
        Me.panelCashup.SuspendLayout()
        Me.panelTotals.SuspendLayout()
        CType(Me.dgvPaymentDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlMain.SuspendLayout()
        Me.TabPageTillBalance.SuspendLayout()
        Me.panelTillbalance.SuspendLayout()
        Me.TabPageEndOfDay.SuspendLayout()
        Me.TabPageCashUpSessions.SuspendLayout()
        Me.panelReports.SuspendLayout()
        CType(Me.dgvSessions, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxPrintCmds.SuspendLayout()
        Me.grpBoxPrinters.SuspendLayout()
        Me.grpBoxTerminal.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelBanner
        '
        Me.panelBanner.BackColor = System.Drawing.Color.Thistle
        Me.panelBanner.Controls.Add(Me.labCurrentTill)
        Me.panelBanner.Controls.Add(Me.Label14)
        Me.panelBanner.Controls.Add(Me.labThisComputer)
        Me.panelBanner.Controls.Add(Me.Label7)
        Me.panelBanner.Controls.Add(Me.labStaffName)
        Me.panelBanner.Controls.Add(Me.Label1)
        Me.panelBanner.Controls.Add(Me.labMainTitle)
        Me.panelBanner.Location = New System.Drawing.Point(-2, 1)
        Me.panelBanner.Name = "panelBanner"
        Me.panelBanner.Size = New System.Drawing.Size(830, 55)
        Me.panelBanner.TabIndex = 1
        '
        'labCurrentTill
        '
        Me.labCurrentTill.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labCurrentTill.Location = New System.Drawing.Point(476, 34)
        Me.labCurrentTill.Name = "labCurrentTill"
        Me.labCurrentTill.Size = New System.Drawing.Size(81, 20)
        Me.labCurrentTill.TabIndex = 8
        Me.labCurrentTill.Text = "labCurrentTill"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(473, 17)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 14)
        Me.Label14.TabIndex = 7
        Me.Label14.Text = "Your Till is:"
        '
        'labThisComputer
        '
        Me.labThisComputer.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labThisComputer.Location = New System.Drawing.Point(292, 34)
        Me.labThisComputer.Name = "labThisComputer"
        Me.labThisComputer.Size = New System.Drawing.Size(173, 20)
        Me.labThisComputer.TabIndex = 6
        Me.labThisComputer.Text = "labThisComputer"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(289, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(121, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "This Workstation is: "
        '
        'labStaffName
        '
        Me.labStaffName.AutoSize = True
        Me.labStaffName.Location = New System.Drawing.Point(646, 32)
        Me.labStaffName.Name = "labStaffName"
        Me.labStaffName.Size = New System.Drawing.Size(72, 13)
        Me.labStaffName.TabIndex = 4
        Me.labStaffName.Text = "labStaffName"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(646, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 19)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Staff: "
        '
        'labMainTitle
        '
        Me.labMainTitle.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMainTitle.Location = New System.Drawing.Point(11, 12)
        Me.labMainTitle.Name = "labMainTitle"
        Me.labMainTitle.Size = New System.Drawing.Size(273, 33)
        Me.labMainTitle.TabIndex = 0
        Me.labMainTitle.Text = "Till Balance and Daily Cash-up"
        '
        'labCashDrawer
        '
        Me.labCashDrawer.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labCashDrawer.Location = New System.Drawing.Point(66, 58)
        Me.labCashDrawer.Name = "labCashDrawer"
        Me.labCashDrawer.Size = New System.Drawing.Size(65, 21)
        Me.labCashDrawer.TabIndex = 7
        Me.labCashDrawer.Text = "labCashDrawer"
        Me.labCashDrawer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(13, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(140, 37)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Reporting on Till (CashDrawer):"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Indigo
        Me.Label6.Location = New System.Drawing.Point(12, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(133, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "End of Session- "
        '
        'btnCashupCommit
        '
        Me.btnCashupCommit.BackColor = System.Drawing.Color.Lavender
        Me.btnCashupCommit.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCashupCommit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCashupCommit.Location = New System.Drawing.Point(690, 344)
        Me.btnCashupCommit.Name = "btnCashupCommit"
        Me.btnCashupCommit.Size = New System.Drawing.Size(77, 33)
        Me.btnCashupCommit.TabIndex = 3
        Me.btnCashupCommit.Text = "ok- Commit"
        Me.btnCashupCommit.UseVisualStyleBackColor = False
        '
        'labDLLversion
        '
        Me.labDLLversion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDLLversion.Location = New System.Drawing.Point(7, 598)
        Me.labDLLversion.Name = "labDLLversion"
        Me.labDLLversion.Size = New System.Drawing.Size(297, 13)
        Me.labDLLversion.TabIndex = 50
        Me.labDLLversion.Text = "labDLLversion"
        '
        'btnCashupCancel
        '
        Me.btnCashupCancel.BackColor = System.Drawing.Color.Lavender
        Me.btnCashupCancel.CausesValidation = False
        Me.btnCashupCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCashupCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCashupCancel.Location = New System.Drawing.Point(603, 344)
        Me.btnCashupCancel.Name = "btnCashupCancel"
        Me.btnCashupCancel.Size = New System.Drawing.Size(71, 33)
        Me.btnCashupCancel.TabIndex = 2
        Me.btnCashupCancel.Text = "Cancel"
        Me.btnCashupCancel.UseVisualStyleBackColor = False
        '
        'txtCashupComments
        '
        Me.txtCashupComments.BackColor = System.Drawing.SystemColors.Window
        Me.txtCashupComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCashupComments.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashupComments.Location = New System.Drawing.Point(519, 192)
        Me.txtCashupComments.Multiline = True
        Me.txtCashupComments.Name = "txtCashupComments"
        Me.txtCashupComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCashupComments.Size = New System.Drawing.Size(251, 127)
        Me.txtCashupComments.TabIndex = 1
        '
        'panelCashup
        '
        Me.panelCashup.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelCashup.Controls.Add(Me.panelTotals)
        Me.panelCashup.Controls.Add(Me.labExplain)
        Me.panelCashup.Controls.Add(Me.Label2)
        Me.panelCashup.Controls.Add(Me.labComments)
        Me.panelCashup.Controls.Add(Me.btnCashupCommit)
        Me.panelCashup.Controls.Add(Me.Label6)
        Me.panelCashup.Controls.Add(Me.labHelpCashup)
        Me.panelCashup.Controls.Add(Me.btnCashupCancel)
        Me.panelCashup.Controls.Add(Me.LabSalePayments)
        Me.panelCashup.Controls.Add(Me.txtCashupComments)
        Me.panelCashup.Controls.Add(Me.dgvPaymentDetails)
        Me.panelCashup.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelCashup.Location = New System.Drawing.Point(6, 6)
        Me.panelCashup.Name = "panelCashup"
        Me.panelCashup.Size = New System.Drawing.Size(790, 386)
        Me.panelCashup.TabIndex = 51
        '
        'panelTotals
        '
        Me.panelTotals.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.panelTotals.Controls.Add(Me.Label4)
        Me.panelTotals.Controls.Add(Me.txtTotalCounted)
        Me.panelTotals.Controls.Add(Me.txtTotalDifference)
        Me.panelTotals.Controls.Add(Me.txtTotalReported)
        Me.panelTotals.Location = New System.Drawing.Point(14, 325)
        Me.panelTotals.Name = "panelTotals"
        Me.panelTotals.Size = New System.Drawing.Size(480, 42)
        Me.panelTotals.TabIndex = 53
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(28, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 14)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Totals:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTotalCounted
        '
        Me.txtTotalCounted.BackColor = System.Drawing.Color.LightGray
        Me.txtTotalCounted.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalCounted.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalCounted.Location = New System.Drawing.Point(266, 15)
        Me.txtTotalCounted.Name = "txtTotalCounted"
        Me.txtTotalCounted.ReadOnly = True
        Me.txtTotalCounted.Size = New System.Drawing.Size(89, 14)
        Me.txtTotalCounted.TabIndex = 52
        Me.txtTotalCounted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalDifference
        '
        Me.txtTotalDifference.BackColor = System.Drawing.Color.Thistle
        Me.txtTotalDifference.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalDifference.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDifference.Location = New System.Drawing.Point(377, 14)
        Me.txtTotalDifference.Name = "txtTotalDifference"
        Me.txtTotalDifference.ReadOnly = True
        Me.txtTotalDifference.Size = New System.Drawing.Size(89, 14)
        Me.txtTotalDifference.TabIndex = 13
        Me.txtTotalDifference.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalReported
        '
        Me.txtTotalReported.BackColor = System.Drawing.Color.LightGray
        Me.txtTotalReported.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTotalReported.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalReported.Location = New System.Drawing.Point(157, 14)
        Me.txtTotalReported.Name = "txtTotalReported"
        Me.txtTotalReported.ReadOnly = True
        Me.txtTotalReported.Size = New System.Drawing.Size(89, 14)
        Me.txtTotalReported.TabIndex = 15
        Me.txtTotalReported.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labExplain
        '
        Me.labExplain.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labExplain.ForeColor = System.Drawing.Color.Maroon
        Me.labExplain.Location = New System.Drawing.Point(600, 171)
        Me.labExplain.Name = "labExplain"
        Me.labExplain.Size = New System.Drawing.Size(128, 18)
        Me.labExplain.TabIndex = 51
        Me.labExplain.Text = "Comment needed ! "
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(175, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(301, 20)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Enter counts for all Payment details for the session."
        '
        'labComments
        '
        Me.labComments.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labComments.Location = New System.Drawing.Point(527, 171)
        Me.labComments.Name = "labComments"
        Me.labComments.Size = New System.Drawing.Size(57, 18)
        Me.labComments.TabIndex = 48
        Me.labComments.Text = "Comments"
        '
        'labHelpCashup
        '
        Me.labHelpCashup.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.labHelpCashup.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHelpCashup.ForeColor = System.Drawing.Color.Blue
        Me.labHelpCashup.Location = New System.Drawing.Point(523, 59)
        Me.labHelpCashup.Name = "labHelpCashup"
        Me.labHelpCashup.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labHelpCashup.Size = New System.Drawing.Size(247, 87)
        Me.labHelpCashup.TabIndex = 42
        Me.labHelpCashup.Text = "labHelpCashup"
        '
        'LabSalePayments
        '
        Me.LabSalePayments.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSalePayments.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.LabSalePayments.Location = New System.Drawing.Point(498, 8)
        Me.LabSalePayments.Name = "LabSalePayments"
        Me.LabSalePayments.Size = New System.Drawing.Size(240, 35)
        Me.LabSalePayments.TabIndex = 7
        Me.LabSalePayments.Text = "--  NB:   Committing this Cashup will cliose the current Session for Selected Til" & _
    "l --"
        Me.LabSalePayments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgvPaymentDetails
        '
        Me.dgvPaymentDetails.AllowUserToAddRows = False
        Me.dgvPaymentDetails.AllowUserToDeleteRows = False
        Me.dgvPaymentDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPaymentDetails.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPaymentDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPaymentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPaymentDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PaymentType, Me.amount_reported, Me.amount_counted, Me.amount_difference, Me.PaymentType_id, Me.validated})
        Me.dgvPaymentDetails.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvPaymentDetails.Location = New System.Drawing.Point(14, 59)
        Me.dgvPaymentDetails.MultiSelect = False
        Me.dgvPaymentDetails.Name = "dgvPaymentDetails"
        Me.dgvPaymentDetails.RowHeadersWidth = 20
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvPaymentDetails.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvPaymentDetails.RowTemplate.Height = 17
        Me.dgvPaymentDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvPaymentDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvPaymentDetails.Size = New System.Drawing.Size(480, 260)
        Me.dgvPaymentDetails.StandardTab = True
        Me.dgvPaymentDetails.TabIndex = 0
        '
        'PaymentType
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PaymentType.DefaultCellStyle = DataGridViewCellStyle2
        Me.PaymentType.FillWeight = 130.0!
        Me.PaymentType.HeaderText = "PaymentType"
        Me.PaymentType.Name = "PaymentType"
        Me.PaymentType.ReadOnly = True
        '
        'amount_reported
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        Me.amount_reported.DefaultCellStyle = DataGridViewCellStyle3
        Me.amount_reported.HeaderText = "Amount POS Reported"
        Me.amount_reported.Name = "amount_reported"
        Me.amount_reported.ReadOnly = True
        '
        'amount_counted
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black
        Me.amount_counted.DefaultCellStyle = DataGridViewCellStyle4
        Me.amount_counted.HeaderText = "Amount Counted"
        Me.amount_counted.MaxInputLength = 10
        Me.amount_counted.Name = "amount_counted"
        '
        'amount_difference
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.amount_difference.DefaultCellStyle = DataGridViewCellStyle5
        Me.amount_difference.HeaderText = "Difference"
        Me.amount_difference.Name = "amount_difference"
        Me.amount_difference.ReadOnly = True
        '
        'PaymentType_id
        '
        Me.PaymentType_id.HeaderText = "PaymentType_id"
        Me.PaymentType_id.Name = "PaymentType_id"
        Me.PaymentType_id.ReadOnly = True
        Me.PaymentType_id.Visible = False
        '
        'validated
        '
        Me.validated.HeaderText = "validated"
        Me.validated.Name = "validated"
        Me.validated.ReadOnly = True
        Me.validated.Visible = False
        '
        'TabControlMain
        '
        Me.TabControlMain.CausesValidation = False
        Me.TabControlMain.Controls.Add(Me.TabPageTillBalance)
        Me.TabControlMain.Controls.Add(Me.TabPageEndOfDay)
        Me.TabControlMain.Controls.Add(Me.TabPageCashUpSessions)
        Me.TabControlMain.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlMain.Location = New System.Drawing.Point(-2, 152)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(830, 443)
        Me.TabControlMain.TabIndex = 52
        '
        'TabPageTillBalance
        '
        Me.TabPageTillBalance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPageTillBalance.CausesValidation = False
        Me.TabPageTillBalance.Controls.Add(Me.panelTillbalance)
        Me.TabPageTillBalance.Location = New System.Drawing.Point(4, 25)
        Me.TabPageTillBalance.Name = "TabPageTillBalance"
        Me.TabPageTillBalance.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTillBalance.Size = New System.Drawing.Size(822, 414)
        Me.TabPageTillBalance.TabIndex = 0
        Me.TabPageTillBalance.Text = "Till Balance"
        '
        'panelTillbalance
        '
        Me.panelTillbalance.Controls.Add(Me.btnPrintRevenueSummary)
        Me.panelTillbalance.Controls.Add(Me.btnPrintSortedDetail)
        Me.panelTillbalance.Controls.Add(Me.btnPrintTillListing)
        Me.panelTillbalance.Controls.Add(Me.txtTillSummary)
        Me.panelTillbalance.Controls.Add(Me.labToPrint)
        Me.panelTillbalance.Controls.Add(Me.Label5)
        Me.panelTillbalance.Controls.Add(Me.Label3)
        Me.panelTillbalance.Controls.Add(Me.btnPrintTillSummary)
        Me.panelTillbalance.Location = New System.Drawing.Point(6, 6)
        Me.panelTillbalance.Name = "panelTillbalance"
        Me.panelTillbalance.Size = New System.Drawing.Size(810, 402)
        Me.panelTillbalance.TabIndex = 56
        '
        'btnPrintRevenueSummary
        '
        Me.btnPrintRevenueSummary.BackColor = System.Drawing.Color.Lavender
        Me.btnPrintRevenueSummary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintRevenueSummary.Location = New System.Drawing.Point(604, 191)
        Me.btnPrintRevenueSummary.Name = "btnPrintRevenueSummary"
        Me.btnPrintRevenueSummary.Size = New System.Drawing.Size(132, 44)
        Me.btnPrintRevenueSummary.TabIndex = 2
        Me.btnPrintRevenueSummary.Text = "Print Revenue Summary"
        Me.ToolTip1.SetToolTip(Me.btnPrintRevenueSummary, "Prints Till Revenue Summary on selected Report Printer..")
        Me.btnPrintRevenueSummary.UseVisualStyleBackColor = False
        '
        'btnPrintSortedDetail
        '
        Me.btnPrintSortedDetail.BackColor = System.Drawing.Color.Lavender
        Me.btnPrintSortedDetail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintSortedDetail.Location = New System.Drawing.Point(604, 328)
        Me.btnPrintSortedDetail.Name = "btnPrintSortedDetail"
        Me.btnPrintSortedDetail.Size = New System.Drawing.Size(132, 45)
        Me.btnPrintSortedDetail.TabIndex = 4
        Me.btnPrintSortedDetail.Text = "Print Sorted Detail"
        Me.ToolTip1.SetToolTip(Me.btnPrintSortedDetail, "Prints Sorted Payment Details on selected Report Printer..")
        Me.btnPrintSortedDetail.UseVisualStyleBackColor = False
        '
        'btnPrintTillListing
        '
        Me.btnPrintTillListing.BackColor = System.Drawing.Color.Lavender
        Me.btnPrintTillListing.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintTillListing.Location = New System.Drawing.Point(604, 258)
        Me.btnPrintTillListing.Name = "btnPrintTillListing"
        Me.btnPrintTillListing.Size = New System.Drawing.Size(132, 44)
        Me.btnPrintTillListing.TabIndex = 3
        Me.btnPrintTillListing.Text = "Print Payments Listing (Selected Till)"
        Me.ToolTip1.SetToolTip(Me.btnPrintTillListing, "Prints Till Payments Received Listing on selected Report Printer..")
        Me.btnPrintTillListing.UseVisualStyleBackColor = False
        '
        'txtTillSummary
        '
        Me.txtTillSummary.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtTillSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTillSummary.Font = New System.Drawing.Font("Lucida Console", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTillSummary.Location = New System.Drawing.Point(14, 59)
        Me.txtTillSummary.Multiline = True
        Me.txtTillSummary.Name = "txtTillSummary"
        Me.txtTillSummary.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtTillSummary.Size = New System.Drawing.Size(533, 314)
        Me.txtTillSummary.TabIndex = 0
        '
        'labToPrint
        '
        Me.labToPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labToPrint.Location = New System.Drawing.Point(573, 59)
        Me.labToPrint.Name = "labToPrint"
        Me.labToPrint.Size = New System.Drawing.Size(163, 45)
        Me.labToPrint.TabIndex = 55
        Me.labToPrint.Text = "To Print the Till balance Summary- Select a Receipt Printer and press Print Summa" & _
    "ry."
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(12, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(120, 20)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "Till Balance"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(173, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(313, 20)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Till Balance Detail in Currently open Session.."
        '
        'btnPrintTillSummary
        '
        Me.btnPrintTillSummary.BackColor = System.Drawing.Color.Lavender
        Me.btnPrintTillSummary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrintTillSummary.Location = New System.Drawing.Point(604, 125)
        Me.btnPrintTillSummary.Name = "btnPrintTillSummary"
        Me.btnPrintTillSummary.Size = New System.Drawing.Size(132, 49)
        Me.btnPrintTillSummary.TabIndex = 1
        Me.btnPrintTillSummary.Text = "Print Till Balance"
        Me.ToolTip1.SetToolTip(Me.btnPrintTillSummary, "Prints Till Balance Summary on selected Receipt Printer..")
        Me.btnPrintTillSummary.UseVisualStyleBackColor = False
        '
        'TabPageEndOfDay
        '
        Me.TabPageEndOfDay.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPageEndOfDay.CausesValidation = False
        Me.TabPageEndOfDay.Controls.Add(Me.panelCashup)
        Me.TabPageEndOfDay.Location = New System.Drawing.Point(4, 25)
        Me.TabPageEndOfDay.Name = "TabPageEndOfDay"
        Me.TabPageEndOfDay.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEndOfDay.Size = New System.Drawing.Size(822, 414)
        Me.TabPageEndOfDay.TabIndex = 1
        Me.TabPageEndOfDay.Text = "End of Day Cashup"
        '
        'TabPageCashUpSessions
        '
        Me.TabPageCashUpSessions.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TabPageCashUpSessions.Controls.Add(Me.panelReports)
        Me.TabPageCashUpSessions.Location = New System.Drawing.Point(4, 25)
        Me.TabPageCashUpSessions.Name = "TabPageCashUpSessions"
        Me.TabPageCashUpSessions.Size = New System.Drawing.Size(822, 414)
        Me.TabPageCashUpSessions.TabIndex = 2
        Me.TabPageCashUpSessions.Text = "CashUp Sessions/Reports"
        '
        'panelReports
        '
        Me.panelReports.Controls.Add(Me.Label22)
        Me.panelReports.Controls.Add(Me.labRecCount)
        Me.panelReports.Controls.Add(Me.txtFind)
        Me.panelReports.Controls.Add(Me.LabFind)
        Me.panelReports.Controls.Add(Me.dgvSessions)
        Me.panelReports.Controls.Add(Me.grpBoxPrintCmds)
        Me.panelReports.Controls.Add(Me.Label8)
        Me.panelReports.Controls.Add(Me.Label9)
        Me.panelReports.Location = New System.Drawing.Point(6, 6)
        Me.panelReports.Name = "panelReports"
        Me.panelReports.Size = New System.Drawing.Size(813, 405)
        Me.panelReports.TabIndex = 54
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(450, 82)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(83, 17)
        Me.Label22.TabIndex = 84
        Me.Label22.Text = "Records found."
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(400, 82)
        Me.labRecCount.Name = "labRecCount"
        Me.labRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCount.Size = New System.Drawing.Size(44, 17)
        Me.labRecCount.TabIndex = 83
        Me.labRecCount.Text = "labRecCount"
        Me.labRecCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(10, 77)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(136, 15)
        Me.txtFind.TabIndex = 58
        '
        'LabFind
        '
        Me.LabFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.LabFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabFind.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabFind.Location = New System.Drawing.Point(11, 50)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(135, 25)
        Me.LabFind.TabIndex = 59
        Me.LabFind.Text = "LabFind"
        '
        'dgvSessions
        '
        Me.dgvSessions.AllowUserToAddRows = False
        Me.dgvSessions.AllowUserToDeleteRows = False
        Me.dgvSessions.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvSessions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvSessions.ColumnHeadersHeight = 18
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSessions.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvSessions.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvSessions.Location = New System.Drawing.Point(14, 99)
        Me.dgvSessions.MultiSelect = False
        Me.dgvSessions.Name = "dgvSessions"
        Me.dgvSessions.ReadOnly = True
        Me.dgvSessions.RowHeadersWidth = 17
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvSessions.RowsDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvSessions.RowTemplate.Height = 19
        Me.dgvSessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSessions.Size = New System.Drawing.Size(533, 293)
        Me.dgvSessions.TabIndex = 57
        '
        'grpBoxPrintCmds
        '
        Me.grpBoxPrintCmds.Controls.Add(Me.listViewSession)
        Me.grpBoxPrintCmds.Controls.Add(Me.btnSessionReport)
        Me.grpBoxPrintCmds.Controls.Add(Me.Label11)
        Me.grpBoxPrintCmds.Controls.Add(Me.txtSessionComment)
        Me.grpBoxPrintCmds.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxPrintCmds.Location = New System.Drawing.Point(558, 23)
        Me.grpBoxPrintCmds.Name = "grpBoxPrintCmds"
        Me.grpBoxPrintCmds.Size = New System.Drawing.Size(252, 369)
        Me.grpBoxPrintCmds.TabIndex = 56
        Me.grpBoxPrintCmds.TabStop = False
        Me.grpBoxPrintCmds.Text = "grpBoxPrintCmds"
        '
        'listViewSession
        '
        Me.listViewSession.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listViewSession.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnKey, Me.ColumnReported, Me.ColumnCounted, Me.ColumnDiff})
        Me.listViewSession.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewSession.GridLines = True
        Me.listViewSession.Location = New System.Drawing.Point(6, 76)
        Me.listViewSession.MultiSelect = False
        Me.listViewSession.Name = "listViewSession"
        Me.listViewSession.Size = New System.Drawing.Size(240, 144)
        Me.listViewSession.TabIndex = 85
        Me.listViewSession.UseCompatibleStateImageBehavior = False
        Me.listViewSession.View = System.Windows.Forms.View.Details
        '
        'ColumnKey
        '
        Me.ColumnKey.Text = "PaymentType"
        '
        'ColumnReported
        '
        Me.ColumnReported.Text = "Reported"
        Me.ColumnReported.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ColumnCounted
        '
        Me.ColumnCounted.Text = "Counted"
        Me.ColumnCounted.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ColumnDiff
        '
        Me.ColumnDiff.Text = "Difference"
        Me.ColumnDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnSessionReport
        '
        Me.btnSessionReport.BackColor = System.Drawing.Color.Lavender
        Me.btnSessionReport.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSessionReport.Location = New System.Drawing.Point(133, 21)
        Me.btnSessionReport.Name = "btnSessionReport"
        Me.btnSessionReport.Size = New System.Drawing.Size(105, 46)
        Me.btnSessionReport.TabIndex = 54
        Me.btnSessionReport.Text = "Print Session Report"
        Me.ToolTip1.SetToolTip(Me.btnSessionReport, "Prints Listing of Payments for the selected Session")
        Me.btnSessionReport.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(18, 238)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(116, 28)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "Comment from Selected Session"
        '
        'txtSessionComment
        '
        Me.txtSessionComment.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.txtSessionComment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSessionComment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSessionComment.Location = New System.Drawing.Point(21, 269)
        Me.txtSessionComment.Multiline = True
        Me.txtSessionComment.Name = "txtSessionComment"
        Me.txtSessionComment.ReadOnly = True
        Me.txtSessionComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSessionComment.Size = New System.Drawing.Size(217, 85)
        Me.txtSessionComment.TabIndex = 54
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(175, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(312, 33)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Lists all completed Cashup Sessions for selected Drawer (Till)."
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Firebrick
        Me.Label9.Location = New System.Drawing.Point(12, 23)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(157, 20)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Cashup History"
        '
        'cboCashDrawers
        '
        Me.cboCashDrawers.BackColor = System.Drawing.Color.Lavender
        Me.cboCashDrawers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCashDrawers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboCashDrawers.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCashDrawers.FormattingEnabled = True
        Me.cboCashDrawers.Location = New System.Drawing.Point(196, 17)
        Me.cboCashDrawers.Name = "cboCashDrawers"
        Me.cboCashDrawers.Size = New System.Drawing.Size(78, 24)
        Me.cboCashDrawers.TabIndex = 8
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(289, 17)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(67, 21)
        Me.btnRefresh.TabIndex = 53
        Me.btnRefresh.Text = "Refresh All"
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Re-compute Till balances and reload session info..")
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'grpBoxPrinters
        '
        Me.grpBoxPrinters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpBoxPrinters.Controls.Add(Me.Label21)
        Me.grpBoxPrinters.Controls.Add(Me.cboReceiptPrinters)
        Me.grpBoxPrinters.Controls.Add(Me.Label20)
        Me.grpBoxPrinters.Controls.Add(Me.cboReportPrinters)
        Me.grpBoxPrinters.Location = New System.Drawing.Point(500, 62)
        Me.grpBoxPrinters.Name = "grpBoxPrinters"
        Me.grpBoxPrinters.Size = New System.Drawing.Size(328, 89)
        Me.grpBoxPrinters.TabIndex = 54
        Me.grpBoxPrinters.TabStop = False
        Me.grpBoxPrinters.Text = "grpBoxPrinters"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(189, 25)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(87, 13)
        Me.Label21.TabIndex = 63
        Me.Label21.Text = "Receipt Printers:"
        '
        'cboReceiptPrinters
        '
        Me.cboReceiptPrinters.BackColor = System.Drawing.Color.Lavender
        Me.cboReceiptPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReceiptPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReceiptPrinters.FormattingEnabled = True
        Me.cboReceiptPrinters.Location = New System.Drawing.Point(181, 41)
        Me.cboReceiptPrinters.Name = "cboReceiptPrinters"
        Me.cboReceiptPrinters.Size = New System.Drawing.Size(137, 21)
        Me.cboReceiptPrinters.TabIndex = 62
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(22, 25)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(84, 13)
        Me.Label20.TabIndex = 61
        Me.Label20.Text = "Report Printers:"
        '
        'cboReportPrinters
        '
        Me.cboReportPrinters.BackColor = System.Drawing.Color.Lavender
        Me.cboReportPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReportPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReportPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReportPrinters.FormattingEnabled = True
        Me.cboReportPrinters.Location = New System.Drawing.Point(17, 41)
        Me.cboReportPrinters.Name = "cboReportPrinters"
        Me.cboReportPrinters.Size = New System.Drawing.Size(137, 21)
        Me.cboReportPrinters.TabIndex = 60
        '
        'grpBoxTerminal
        '
        Me.grpBoxTerminal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpBoxTerminal.Controls.Add(Me.labNotYOurTill)
        Me.grpBoxTerminal.Controls.Add(Me.labIsYourTill)
        Me.grpBoxTerminal.Controls.Add(Me.Label12)
        Me.grpBoxTerminal.Controls.Add(Me.labRefreshing)
        Me.grpBoxTerminal.Controls.Add(Me.cboCashDrawers)
        Me.grpBoxTerminal.Controls.Add(Me.Label10)
        Me.grpBoxTerminal.Controls.Add(Me.btnRefresh)
        Me.grpBoxTerminal.Controls.Add(Me.labCashDrawer)
        Me.grpBoxTerminal.Location = New System.Drawing.Point(8, 61)
        Me.grpBoxTerminal.Name = "grpBoxTerminal"
        Me.grpBoxTerminal.Size = New System.Drawing.Size(486, 89)
        Me.grpBoxTerminal.TabIndex = 55
        Me.grpBoxTerminal.TabStop = False
        Me.grpBoxTerminal.Text = "grpBoxTerminal"
        '
        'labNotYOurTill
        '
        Me.labNotYOurTill.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labNotYOurTill.ForeColor = System.Drawing.Color.IndianRed
        Me.labNotYOurTill.Location = New System.Drawing.Point(215, 59)
        Me.labNotYOurTill.Name = "labNotYOurTill"
        Me.labNotYOurTill.Size = New System.Drawing.Size(90, 21)
        Me.labNotYOurTill.TabIndex = 57
        Me.labNotYOurTill.Text = "(Not YourTill)"
        Me.labNotYOurTill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labIsYourTill
        '
        Me.labIsYourTill.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labIsYourTill.ForeColor = System.Drawing.Color.ForestGreen
        Me.labIsYourTill.Location = New System.Drawing.Point(140, 58)
        Me.labIsYourTill.Name = "labIsYourTill"
        Me.labIsYourTill.Size = New System.Drawing.Size(69, 21)
        Me.labIsYourTill.TabIndex = 56
        Me.labIsYourTill.Text = "(YourTill)"
        Me.labIsYourTill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.DarkViolet
        Me.Label12.Location = New System.Drawing.Point(10, 57)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 21)
        Me.Label12.TabIndex = 55
        Me.Label12.Text = "Till : "
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'labRefreshing
        '
        Me.labRefreshing.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(157, Byte), Integer))
        Me.labRefreshing.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRefreshing.Location = New System.Drawing.Point(394, 58)
        Me.labRefreshing.Name = "labRefreshing"
        Me.labRefreshing.Size = New System.Drawing.Size(74, 21)
        Me.labRefreshing.TabIndex = 54
        Me.labRefreshing.Text = "Refreshing"
        Me.labRefreshing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmCashup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(834, 610)
        Me.Controls.Add(Me.grpBoxTerminal)
        Me.Controls.Add(Me.grpBoxPrinters)
        Me.Controls.Add(Me.TabControlMain)
        Me.Controls.Add(Me.labDLLversion)
        Me.Controls.Add(Me.panelBanner)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmCashup"
        Me.Text = "frmCashup"
        Me.panelBanner.ResumeLayout(False)
        Me.panelBanner.PerformLayout()
        Me.panelCashup.ResumeLayout(False)
        Me.panelCashup.PerformLayout()
        Me.panelTotals.ResumeLayout(False)
        Me.panelTotals.PerformLayout()
        CType(Me.dgvPaymentDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPageTillBalance.ResumeLayout(False)
        Me.panelTillbalance.ResumeLayout(False)
        Me.panelTillbalance.PerformLayout()
        Me.TabPageEndOfDay.ResumeLayout(False)
        Me.TabPageCashUpSessions.ResumeLayout(False)
        Me.panelReports.ResumeLayout(False)
        Me.panelReports.PerformLayout()
        CType(Me.dgvSessions, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxPrintCmds.ResumeLayout(False)
        Me.grpBoxPrintCmds.PerformLayout()
        Me.grpBoxPrinters.ResumeLayout(False)
        Me.grpBoxPrinters.PerformLayout()
        Me.grpBoxTerminal.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelBanner As System.Windows.Forms.Panel
    Friend WithEvents labCashDrawer As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents labStaffName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents labMainTitle As System.Windows.Forms.Label
    Friend WithEvents btnCashupCommit As System.Windows.Forms.Button
    Friend WithEvents labDLLversion As System.Windows.Forms.Label
    Friend WithEvents btnCashupCancel As System.Windows.Forms.Button
    Friend WithEvents txtCashupComments As System.Windows.Forms.TextBox
    Friend WithEvents panelCashup As System.Windows.Forms.Panel
    Friend WithEvents txtTotalReported As System.Windows.Forms.TextBox
    Friend WithEvents labHelpCashup As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LabSalePayments As System.Windows.Forms.Label
    Friend WithEvents dgvPaymentDetails As System.Windows.Forms.DataGridView
    Friend WithEvents txtTotalDifference As System.Windows.Forms.TextBox
    Friend WithEvents labComments As System.Windows.Forms.Label
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPageTillBalance As System.Windows.Forms.TabPage
    Friend WithEvents TabPageEndOfDay As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCashUpSessions As System.Windows.Forms.TabPage
    Friend WithEvents cboCashDrawers As System.Windows.Forms.ComboBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpBoxPrinters As System.Windows.Forms.GroupBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboReceiptPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents cboReportPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents grpBoxTerminal As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents labRefreshing As System.Windows.Forms.Label
    Friend WithEvents btnPrintTillSummary As System.Windows.Forms.Button
    Friend WithEvents txtTillSummary As System.Windows.Forms.TextBox
    Friend WithEvents labToPrint As System.Windows.Forms.Label
    Friend WithEvents labThisComputer As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents panelTillbalance As System.Windows.Forms.Panel
    Friend WithEvents panelReports As System.Windows.Forms.Panel
    Friend WithEvents grpBoxPrintCmds As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtSessionComment As System.Windows.Forms.TextBox
    Friend WithEvents labExplain As System.Windows.Forms.Label
    Friend WithEvents txtTotalCounted As System.Windows.Forms.TextBox
    Friend WithEvents panelTotals As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents PaymentType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents amount_reported As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents amount_counted As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents amount_difference As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentType_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents validated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents labCurrentTill As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnPrintTillListing As System.Windows.Forms.Button
    Friend WithEvents labNotYOurTill As System.Windows.Forms.Label
    Friend WithEvents labIsYourTill As System.Windows.Forms.Label
    Friend WithEvents dgvSessions As System.Windows.Forms.DataGridView
    Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents LabFind As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents labRecCount As System.Windows.Forms.Label
    Friend WithEvents listViewSession As System.Windows.Forms.ListView
    Friend WithEvents ColumnKey As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnReported As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnCounted As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnDiff As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnSessionReport As System.Windows.Forms.Button
    Friend WithEvents btnPrintSortedDetail As System.Windows.Forms.Button
    Friend WithEvents btnPrintRevenueSummary As System.Windows.Forms.Button
End Class
