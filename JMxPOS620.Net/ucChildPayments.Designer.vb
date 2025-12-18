<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildPayments
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelBanner = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.labMainTitle = New System.Windows.Forms.Label()
        Me.labStart1 = New System.Windows.Forms.Label()
        Me.panelHdr = New System.Windows.Forms.Panel()
        Me.labReversedInvoices = New System.Windows.Forms.Label()
        Me.cboStatementPrinters = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.btnStatement = New System.Windows.Forms.Button()
        Me.btnReverse = New System.Windows.Forms.Button()
        Me.btnNewPayment = New System.Windows.Forms.Button()
        Me.labHelp = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnCancel2 = New System.Windows.Forms.Button()
        Me.txtInitialOwing = New System.Windows.Forms.TextBox()
        Me.labCreditNoteAvail = New System.Windows.Forms.Label()
        Me.labCreditLimit = New System.Windows.Forms.Label()
        Me.txtCustMobile = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCustEmail = New System.Windows.Forms.TextBox()
        Me.txtCustPhone = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabSaleCustSrch = New System.Windows.Forms.Label()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.LabSaleCust = New System.Windows.Forms.Label()
        Me.txtCustBarcode = New System.Windows.Forms.TextBox()
        Me.txtCreditNotesApplied = New System.Windows.Forms.TextBox()
        Me.labApplyingCreditNotes = New System.Windows.Forms.Label()
        Me.LabSalePayments = New System.Windows.Forms.Label()
        Me.dgvPaymentDetails = New System.Windows.Forms.DataGridView()
        Me.PaymentType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaymentType_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.panelPayment = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.labTotalApplying = New System.Windows.Forms.Label()
        Me.txtDiscountAllowed = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnCommit = New System.Windows.Forms.Button()
        Me.labComments = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.grpBoxRefundType = New System.Windows.Forms.GroupBox()
        Me.optRefundCredit = New System.Windows.Forms.RadioButton()
        Me.optRefundCash = New System.Windows.Forms.RadioButton()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBalanceOwing = New System.Windows.Forms.TextBox()
        Me.labTotalOwing = New System.Windows.Forms.Label()
        Me.txtSaleSubTotal = New System.Windows.Forms.TextBox()
        Me.LabCreditAvail = New System.Windows.Forms.Label()
        Me.labChange = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.LabPaymentBalance = New System.Windows.Forms.Label()
        Me.txtChange = New System.Windows.Forms.TextBox()
        Me.txtPaymentBalance = New System.Windows.Forms.TextBox()
        Me.grpBoxPayment = New System.Windows.Forms.GroupBox()
        Me.dgvInvoices = New JMxPOS330.clsDgvGoods()
        Me.invoice_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invoice_no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.inv_total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TaxTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prev_paid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.outstanding = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.discount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.payable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.paying_now = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.new_balance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.panelPaymentList = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.listPayments = New System.Windows.Forms.ListBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelBanner.SuspendLayout()
        Me.panelHdr.SuspendLayout()
        CType(Me.dgvPaymentDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelPayment.SuspendLayout()
        Me.grpBoxRefundType.SuspendLayout()
        Me.grpBoxPayment.SuspendLayout()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelPaymentList.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelBanner
        '
        Me.panelBanner.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.panelBanner.Controls.Add(Me.Label6)
        Me.panelBanner.Controls.Add(Me.labMainTitle)
        Me.panelBanner.Location = New System.Drawing.Point(6, 6)
        Me.panelBanner.Name = "panelBanner"
        Me.panelBanner.Size = New System.Drawing.Size(118, 140)
        Me.panelBanner.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(5, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 74)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Use this form to apply Payments Received to Debtors Invoices.."
        '
        'labMainTitle
        '
        Me.labMainTitle.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMainTitle.Location = New System.Drawing.Point(7, 6)
        Me.labMainTitle.Name = "labMainTitle"
        Me.labMainTitle.Size = New System.Drawing.Size(78, 41)
        Me.labMainTitle.TabIndex = 0
        Me.labMainTitle.Text = "Account Payments"
        '
        'labStart1
        '
        Me.labStart1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStart1.Location = New System.Drawing.Point(666, 30)
        Me.labStart1.Name = "labStart1"
        Me.labStart1.Size = New System.Drawing.Size(194, 44)
        Me.labStart1.TabIndex = 6
        Me.labStart1.Text = "For a New payment, enter the Amount Paying Now for each Invoice.  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Then enter ov" & _
    "erall Payment details.."
        '
        'panelHdr
        '
        Me.panelHdr.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelHdr.CausesValidation = False
        Me.panelHdr.Controls.Add(Me.labReversedInvoices)
        Me.panelHdr.Controls.Add(Me.cboStatementPrinters)
        Me.panelHdr.Controls.Add(Me.Label20)
        Me.panelHdr.Controls.Add(Me.labStart1)
        Me.panelHdr.Controls.Add(Me.btnStatement)
        Me.panelHdr.Controls.Add(Me.btnReverse)
        Me.panelHdr.Controls.Add(Me.btnNewPayment)
        Me.panelHdr.Controls.Add(Me.labHelp)
        Me.panelHdr.Controls.Add(Me.Label12)
        Me.panelHdr.Controls.Add(Me.Label7)
        Me.panelHdr.Controls.Add(Me.btnCancel2)
        Me.panelHdr.Controls.Add(Me.txtInitialOwing)
        Me.panelHdr.Controls.Add(Me.labCreditNoteAvail)
        Me.panelHdr.Controls.Add(Me.labCreditLimit)
        Me.panelHdr.Controls.Add(Me.txtCustMobile)
        Me.panelHdr.Controls.Add(Me.Label5)
        Me.panelHdr.Controls.Add(Me.txtCustEmail)
        Me.panelHdr.Controls.Add(Me.txtCustPhone)
        Me.panelHdr.Controls.Add(Me.Label3)
        Me.panelHdr.Controls.Add(Me.Label2)
        Me.panelHdr.Controls.Add(Me.LabSaleCustSrch)
        Me.panelHdr.Controls.Add(Me.txtCustName)
        Me.panelHdr.Controls.Add(Me.LabSaleCust)
        Me.panelHdr.Controls.Add(Me.txtCustBarcode)
        Me.panelHdr.Location = New System.Drawing.Point(128, 6)
        Me.panelHdr.Name = "panelHdr"
        Me.panelHdr.Size = New System.Drawing.Size(874, 140)
        Me.panelHdr.TabIndex = 0
        '
        'labReversedInvoices
        '
        Me.labReversedInvoices.BackColor = System.Drawing.Color.LightYellow
        Me.labReversedInvoices.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.labReversedInvoices.Location = New System.Drawing.Point(452, 103)
        Me.labReversedInvoices.Name = "labReversedInvoices"
        Me.labReversedInvoices.Size = New System.Drawing.Size(80, 30)
        Me.labReversedInvoices.TabIndex = 63
        Me.labReversedInvoices.Text = "labReversedInvoices"
        Me.labReversedInvoices.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboStatementPrinters
        '
        Me.cboStatementPrinters.BackColor = System.Drawing.Color.White
        Me.cboStatementPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatementPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboStatementPrinters.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatementPrinters.FormattingEnabled = True
        Me.cboStatementPrinters.Location = New System.Drawing.Point(493, 79)
        Me.cboStatementPrinters.Name = "cboStatementPrinters"
        Me.cboStatementPrinters.Size = New System.Drawing.Size(150, 21)
        Me.cboStatementPrinters.TabIndex = 3
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(490, 60)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(129, 16)
        Me.Label20.TabIndex = 62
        Me.Label20.Text = "Printer for Hard Copy:"
        '
        'btnStatement
        '
        Me.btnStatement.BackColor = System.Drawing.Color.AliceBlue
        Me.btnStatement.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnStatement.Location = New System.Drawing.Point(543, 107)
        Me.btnStatement.Name = "btnStatement"
        Me.btnStatement.Size = New System.Drawing.Size(99, 27)
        Me.btnStatement.TabIndex = 4
        Me.btnStatement.Text = "Show Statement"
        Me.btnStatement.UseVisualStyleBackColor = False
        '
        'btnReverse
        '
        Me.btnReverse.BackColor = System.Drawing.Color.Gainsboro
        Me.btnReverse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReverse.ForeColor = System.Drawing.Color.Black
        Me.btnReverse.Location = New System.Drawing.Point(582, 6)
        Me.btnReverse.Name = "btnReverse"
        Me.btnReverse.Size = New System.Drawing.Size(60, 43)
        Me.btnReverse.TabIndex = 2
        Me.btnReverse.Text = "Reverse Payment"
        Me.ToolTip1.SetToolTip(Me.btnReverse, "Reverse out a previous Account Payment")
        Me.btnReverse.UseVisualStyleBackColor = False
        '
        'btnNewPayment
        '
        Me.btnNewPayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnNewPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewPayment.Location = New System.Drawing.Point(493, 7)
        Me.btnNewPayment.Name = "btnNewPayment"
        Me.btnNewPayment.Size = New System.Drawing.Size(60, 43)
        Me.btnNewPayment.TabIndex = 1
        Me.btnNewPayment.Text = "New Payment"
        Me.btnNewPayment.UseVisualStyleBackColor = False
        '
        'labHelp
        '
        Me.labHelp.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.labHelp.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHelp.ForeColor = System.Drawing.Color.Blue
        Me.labHelp.Location = New System.Drawing.Point(665, 74)
        Me.labHelp.Name = "labHelp"
        Me.labHelp.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labHelp.Size = New System.Drawing.Size(202, 60)
        Me.labHelp.TabIndex = 42
        Me.labHelp.Text = "labHelp"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DarkGray
        Me.Label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label12.ForeColor = System.Drawing.Color.LightGray
        Me.Label12.Location = New System.Drawing.Point(654, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(5, 115)
        Me.Label12.TabIndex = 54
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(661, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(119, 12)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Initial Outstanding:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancel2
        '
        Me.btnCancel2.BackColor = System.Drawing.Color.Thistle
        Me.btnCancel2.CausesValidation = False
        Me.btnCancel2.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCancel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel2.Location = New System.Drawing.Point(386, 101)
        Me.btnCancel2.Name = "btnCancel2"
        Me.btnCancel2.Size = New System.Drawing.Size(54, 34)
        Me.btnCancel2.TabIndex = 5
        Me.btnCancel2.TabStop = False
        Me.btnCancel2.Text = "Cancel"
        Me.btnCancel2.UseVisualStyleBackColor = False
        '
        'txtInitialOwing
        '
        Me.txtInitialOwing.BackColor = System.Drawing.Color.Lavender
        Me.txtInitialOwing.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInitialOwing.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInitialOwing.Location = New System.Drawing.Point(786, 9)
        Me.txtInitialOwing.Name = "txtInitialOwing"
        Me.txtInitialOwing.ReadOnly = True
        Me.txtInitialOwing.Size = New System.Drawing.Size(74, 16)
        Me.txtInitialOwing.TabIndex = 4
        Me.txtInitialOwing.TabStop = False
        Me.txtInitialOwing.Text = "0.00"
        Me.txtInitialOwing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labCreditNoteAvail
        '
        Me.labCreditNoteAvail.Location = New System.Drawing.Point(383, 51)
        Me.labCreditNoteAvail.Name = "labCreditNoteAvail"
        Me.labCreditNoteAvail.Size = New System.Drawing.Size(91, 38)
        Me.labCreditNoteAvail.TabIndex = 50
        Me.labCreditNoteAvail.Text = "labCreditNoteAvail"
        '
        'labCreditLimit
        '
        Me.labCreditLimit.Location = New System.Drawing.Point(381, 26)
        Me.labCreditLimit.Name = "labCreditLimit"
        Me.labCreditLimit.Size = New System.Drawing.Size(67, 14)
        Me.labCreditLimit.TabIndex = 49
        Me.labCreditLimit.Text = "0"
        Me.labCreditLimit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustMobile
        '
        Me.txtCustMobile.BackColor = System.Drawing.Color.Lavender
        Me.txtCustMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustMobile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustMobile.Location = New System.Drawing.Point(237, 84)
        Me.txtCustMobile.Multiline = True
        Me.txtCustMobile.Name = "txtCustMobile"
        Me.txtCustMobile.Size = New System.Drawing.Size(140, 16)
        Me.txtCustMobile.TabIndex = 5
        Me.txtCustMobile.TabStop = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(381, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 14)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Credit Limit:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustEmail
        '
        Me.txtCustEmail.BackColor = System.Drawing.Color.Lavender
        Me.txtCustEmail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustEmail.Location = New System.Drawing.Point(97, 106)
        Me.txtCustEmail.Multiline = True
        Me.txtCustEmail.Name = "txtCustEmail"
        Me.txtCustEmail.Size = New System.Drawing.Size(280, 16)
        Me.txtCustEmail.TabIndex = 6
        Me.txtCustEmail.TabStop = False
        '
        'txtCustPhone
        '
        Me.txtCustPhone.BackColor = System.Drawing.Color.Lavender
        Me.txtCustPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustPhone.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPhone.Location = New System.Drawing.Point(97, 84)
        Me.txtCustPhone.Multiline = True
        Me.txtCustPhone.Name = "txtCustPhone"
        Me.txtCustPhone.Size = New System.Drawing.Size(128, 16)
        Me.txtCustPhone.TabIndex = 4
        Me.txtCustPhone.TabStop = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 109)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Email:"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Phone:"
        '
        'LabSaleCustSrch
        '
        Me.LabSaleCustSrch.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSaleCustSrch.Location = New System.Drawing.Point(27, 47)
        Me.LabSaleCustSrch.Name = "LabSaleCustSrch"
        Me.LabSaleCustSrch.Size = New System.Drawing.Size(108, 32)
        Me.LabSaleCustSrch.TabIndex = 38
        Me.LabSaleCustSrch.Text = "F2 to Search Cust." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Shift-F2 Srch Invoices."
        '
        'txtCustName
        '
        Me.txtCustName.BackColor = System.Drawing.Color.Lavender
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.Location = New System.Drawing.Point(97, 15)
        Me.txtCustName.Multiline = True
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.Size = New System.Drawing.Size(280, 34)
        Me.txtCustName.TabIndex = 3
        Me.txtCustName.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtCustName, "Enter Customer No. (Barcode)..")
        '
        'LabSaleCust
        '
        Me.LabSaleCust.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSaleCust.Location = New System.Drawing.Point(22, 11)
        Me.LabSaleCust.Name = "LabSaleCust"
        Me.LabSaleCust.Size = New System.Drawing.Size(67, 13)
        Me.LabSaleCust.TabIndex = 35
        Me.LabSaleCust.Text = "Customer:"
        '
        'txtCustBarcode
        '
        Me.txtCustBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustBarcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustBarcode.Location = New System.Drawing.Point(25, 26)
        Me.txtCustBarcode.Name = "txtCustBarcode"
        Me.txtCustBarcode.Size = New System.Drawing.Size(55, 21)
        Me.txtCustBarcode.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtCustBarcode, "Customer No. (Barcode)..")
        '
        'txtCreditNotesApplied
        '
        Me.txtCreditNotesApplied.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCreditNotesApplied.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditNotesApplied.Enabled = False
        Me.txtCreditNotesApplied.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditNotesApplied.Location = New System.Drawing.Point(12, 39)
        Me.txtCreditNotesApplied.MaxLength = 10
        Me.txtCreditNotesApplied.Name = "txtCreditNotesApplied"
        Me.txtCreditNotesApplied.Size = New System.Drawing.Size(95, 23)
        Me.txtCreditNotesApplied.TabIndex = 5
        Me.txtCreditNotesApplied.Text = "0.00"
        Me.txtCreditNotesApplied.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labApplyingCreditNotes
        '
        Me.labApplyingCreditNotes.Enabled = False
        Me.labApplyingCreditNotes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labApplyingCreditNotes.Location = New System.Drawing.Point(21, 4)
        Me.labApplyingCreditNotes.Name = "labApplyingCreditNotes"
        Me.labApplyingCreditNotes.Size = New System.Drawing.Size(91, 32)
        Me.labApplyingCreditNotes.TabIndex = 51
        Me.labApplyingCreditNotes.Text = "Credit Note Amount to use:"
        Me.labApplyingCreditNotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolTip1.SetToolTip(Me.labApplyingCreditNotes, "Optionally apply any available Credit Nore balance towards payong Invoices. ")
        '
        'LabSalePayments
        '
        Me.LabSalePayments.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSalePayments.ImageAlign = System.Drawing.ContentAlignment.TopRight
        Me.LabSalePayments.Location = New System.Drawing.Point(346, 12)
        Me.LabSalePayments.Name = "LabSalePayments"
        Me.LabSalePayments.Size = New System.Drawing.Size(94, 33)
        Me.LabSalePayments.TabIndex = 7
        Me.LabSalePayments.Text = "-- This Payment Details --"
        Me.LabSalePayments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgvPaymentDetails
        '
        Me.dgvPaymentDetails.AllowUserToAddRows = False
        Me.dgvPaymentDetails.AllowUserToDeleteRows = False
        Me.dgvPaymentDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPaymentDetails.BackgroundColor = System.Drawing.Color.LavenderBlush
        Me.dgvPaymentDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPaymentDetails.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPaymentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPaymentDetails.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PaymentType, Me.Amount, Me.PaymentType_id})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPaymentDetails.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPaymentDetails.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvPaymentDetails.Location = New System.Drawing.Point(122, 12)
        Me.dgvPaymentDetails.Name = "dgvPaymentDetails"
        Me.dgvPaymentDetails.RowHeadersWidth = 20
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvPaymentDetails.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvPaymentDetails.RowTemplate.Height = 17
        Me.dgvPaymentDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPaymentDetails.Size = New System.Drawing.Size(210, 217)
        Me.dgvPaymentDetails.StandardTab = True
        Me.dgvPaymentDetails.TabIndex = 6
        '
        'PaymentType
        '
        Me.PaymentType.HeaderText = "PaymentType"
        Me.PaymentType.Name = "PaymentType"
        Me.PaymentType.ReadOnly = True
        Me.PaymentType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Amount
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        Me.Amount.DefaultCellStyle = DataGridViewCellStyle2
        Me.Amount.FillWeight = 50.0!
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        Me.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PaymentType_id
        '
        Me.PaymentType_id.HeaderText = "PaymentType_id"
        Me.PaymentType_id.Name = "PaymentType_id"
        Me.PaymentType_id.ReadOnly = True
        Me.PaymentType_id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PaymentType_id.Visible = False
        '
        'panelPayment
        '
        Me.panelPayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.panelPayment.Controls.Add(Me.Label11)
        Me.panelPayment.Controls.Add(Me.labTotalApplying)
        Me.panelPayment.Controls.Add(Me.txtDiscountAllowed)
        Me.panelPayment.Controls.Add(Me.Label10)
        Me.panelPayment.Controls.Add(Me.Label9)
        Me.panelPayment.Controls.Add(Me.dgvPaymentDetails)
        Me.panelPayment.Controls.Add(Me.btnCancel)
        Me.panelPayment.Controls.Add(Me.btnCommit)
        Me.panelPayment.Controls.Add(Me.LabSalePayments)
        Me.panelPayment.Controls.Add(Me.labComments)
        Me.panelPayment.Controls.Add(Me.txtComments)
        Me.panelPayment.Controls.Add(Me.grpBoxRefundType)
        Me.panelPayment.Controls.Add(Me.txtSubTotal)
        Me.panelPayment.Controls.Add(Me.Label4)
        Me.panelPayment.Controls.Add(Me.txtBalanceOwing)
        Me.panelPayment.Controls.Add(Me.labTotalOwing)
        Me.panelPayment.Controls.Add(Me.labApplyingCreditNotes)
        Me.panelPayment.Controls.Add(Me.txtCreditNotesApplied)
        Me.panelPayment.Controls.Add(Me.txtSaleSubTotal)
        Me.panelPayment.Controls.Add(Me.LabCreditAvail)
        Me.panelPayment.Controls.Add(Me.labChange)
        Me.panelPayment.Controls.Add(Me.Label17)
        Me.panelPayment.Controls.Add(Me.LabPaymentBalance)
        Me.panelPayment.Controls.Add(Me.txtChange)
        Me.panelPayment.Controls.Add(Me.txtPaymentBalance)
        Me.panelPayment.Location = New System.Drawing.Point(201, 391)
        Me.panelPayment.Name = "panelPayment"
        Me.panelPayment.Size = New System.Drawing.Size(802, 240)
        Me.panelPayment.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(10, 72)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(106, 20)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "Payment Details"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labTotalApplying
        '
        Me.labTotalApplying.Location = New System.Drawing.Point(345, 57)
        Me.labTotalApplying.Name = "labTotalApplying"
        Me.labTotalApplying.Size = New System.Drawing.Size(105, 40)
        Me.labTotalApplying.TabIndex = 55
        Me.labTotalApplying.Text = "labTotalApplying"
        '
        'txtDiscountAllowed
        '
        Me.txtDiscountAllowed.BackColor = System.Drawing.Color.LightGray
        Me.txtDiscountAllowed.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDiscountAllowed.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscountAllowed.Location = New System.Drawing.Point(687, 32)
        Me.txtDiscountAllowed.Name = "txtDiscountAllowed"
        Me.txtDiscountAllowed.ReadOnly = True
        Me.txtDiscountAllowed.Size = New System.Drawing.Size(104, 14)
        Me.txtDiscountAllowed.TabIndex = 14
        Me.txtDiscountAllowed.TabStop = False
        Me.txtDiscountAllowed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(604, 33)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 14)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "Discount:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(13, 106)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 119)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "Enter Credit-Note amount to use if any.." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Then enter details of new payment (if" & _
    " any).."
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Thistle
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(625, 192)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(71, 33)
        Me.btnCancel.TabIndex = 18
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnCommit
        '
        Me.btnCommit.BackColor = System.Drawing.Color.YellowGreen
        Me.btnCommit.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCommit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCommit.Location = New System.Drawing.Point(708, 192)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(77, 33)
        Me.btnCommit.TabIndex = 19
        Me.btnCommit.Text = "ok- Commit"
        Me.btnCommit.UseVisualStyleBackColor = False
        '
        'labComments
        '
        Me.labComments.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labComments.Location = New System.Drawing.Point(349, 179)
        Me.labComments.Name = "labComments"
        Me.labComments.Size = New System.Drawing.Size(57, 15)
        Me.labComments.TabIndex = 39
        Me.labComments.Text = "Comments"
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.White
        Me.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtComments.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.Location = New System.Drawing.Point(348, 197)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(228, 28)
        Me.txtComments.TabIndex = 11
        '
        'grpBoxRefundType
        '
        Me.grpBoxRefundType.Controls.Add(Me.optRefundCredit)
        Me.grpBoxRefundType.Controls.Add(Me.optRefundCash)
        Me.grpBoxRefundType.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxRefundType.Location = New System.Drawing.Point(466, 69)
        Me.grpBoxRefundType.Name = "grpBoxRefundType"
        Me.grpBoxRefundType.Size = New System.Drawing.Size(116, 61)
        Me.grpBoxRefundType.TabIndex = 46
        Me.grpBoxRefundType.TabStop = False
        Me.grpBoxRefundType.Text = "ChangeType"
        '
        'optRefundCredit
        '
        Me.optRefundCredit.Checked = True
        Me.optRefundCredit.Location = New System.Drawing.Point(14, 37)
        Me.optRefundCredit.Name = "optRefundCredit"
        Me.optRefundCredit.Size = New System.Drawing.Size(93, 15)
        Me.optRefundCredit.TabIndex = 1
        Me.optRefundCredit.TabStop = True
        Me.optRefundCredit.Text = "Credit Note"
        Me.ToolTip1.SetToolTip(Me.optRefundCredit, "Credit Note")
        Me.optRefundCredit.UseVisualStyleBackColor = True
        '
        'optRefundCash
        '
        Me.optRefundCash.Location = New System.Drawing.Point(14, 16)
        Me.optRefundCash.Name = "optRefundCash"
        Me.optRefundCash.Size = New System.Drawing.Size(56, 15)
        Me.optRefundCash.TabIndex = 0
        Me.optRefundCash.Text = "Cash"
        Me.optRefundCash.UseVisualStyleBackColor = True
        '
        'txtSubTotal
        '
        Me.txtSubTotal.BackColor = System.Drawing.Color.LightGray
        Me.txtSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubTotal.Location = New System.Drawing.Point(687, 71)
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.Size = New System.Drawing.Size(104, 14)
        Me.txtSubTotal.TabIndex = 15
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(608, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 33)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "SubTotal Paying Now:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBalanceOwing
        '
        Me.txtBalanceOwing.BackColor = System.Drawing.Color.LightGray
        Me.txtBalanceOwing.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBalanceOwing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalanceOwing.Location = New System.Drawing.Point(687, 133)
        Me.txtBalanceOwing.Name = "txtBalanceOwing"
        Me.txtBalanceOwing.ReadOnly = True
        Me.txtBalanceOwing.Size = New System.Drawing.Size(104, 14)
        Me.txtBalanceOwing.TabIndex = 17
        Me.txtBalanceOwing.TabStop = False
        Me.txtBalanceOwing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labTotalOwing
        '
        Me.labTotalOwing.Location = New System.Drawing.Point(604, 134)
        Me.labTotalOwing.Name = "labTotalOwing"
        Me.labTotalOwing.Size = New System.Drawing.Size(77, 14)
        Me.labTotalOwing.TabIndex = 42
        Me.labTotalOwing.Text = "Still Owing:"
        Me.labTotalOwing.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSaleSubTotal
        '
        Me.txtSaleSubTotal.BackColor = System.Drawing.Color.LightGray
        Me.txtSaleSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSaleSubTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSaleSubTotal.Location = New System.Drawing.Point(687, 104)
        Me.txtSaleSubTotal.Name = "txtSaleSubTotal"
        Me.txtSaleSubTotal.ReadOnly = True
        Me.txtSaleSubTotal.Size = New System.Drawing.Size(104, 14)
        Me.txtSaleSubTotal.TabIndex = 16
        Me.txtSaleSubTotal.TabStop = False
        Me.txtSaleSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabCreditAvail
        '
        Me.LabCreditAvail.Location = New System.Drawing.Point(604, 105)
        Me.LabCreditAvail.Name = "LabCreditAvail"
        Me.LabCreditAvail.Size = New System.Drawing.Size(77, 14)
        Me.LabCreditAvail.TabIndex = 40
        Me.LabCreditAvail.Text = "CreditAvail:"
        Me.LabCreditAvail.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'labChange
        '
        Me.labChange.BackColor = System.Drawing.Color.Transparent
        Me.labChange.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labChange.Location = New System.Drawing.Point(470, 133)
        Me.labChange.Name = "labChange"
        Me.labChange.Size = New System.Drawing.Size(85, 20)
        Me.labChange.TabIndex = 37
        Me.labChange.Text = "Change:"
        Me.labChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.DarkGray
        Me.Label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label17.ForeColor = System.Drawing.Color.LightGray
        Me.Label17.Location = New System.Drawing.Point(591, 21)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(5, 197)
        Me.Label17.TabIndex = 22
        '
        'LabPaymentBalance
        '
        Me.LabPaymentBalance.BackColor = System.Drawing.Color.Transparent
        Me.LabPaymentBalance.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPaymentBalance.Location = New System.Drawing.Point(470, 13)
        Me.LabPaymentBalance.Name = "LabPaymentBalance"
        Me.LabPaymentBalance.Size = New System.Drawing.Size(106, 15)
        Me.LabPaymentBalance.TabIndex = 12
        Me.LabPaymentBalance.Text = "Balance:"
        Me.LabPaymentBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtChange
        '
        Me.txtChange.BackColor = System.Drawing.Color.LightCyan
        Me.txtChange.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtChange.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChange.Location = New System.Drawing.Point(470, 156)
        Me.txtChange.Name = "txtChange"
        Me.txtChange.ReadOnly = True
        Me.txtChange.Size = New System.Drawing.Size(100, 17)
        Me.txtChange.TabIndex = 14
        Me.txtChange.TabStop = False
        Me.txtChange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPaymentBalance
        '
        Me.txtPaymentBalance.BackColor = System.Drawing.Color.Thistle
        Me.txtPaymentBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPaymentBalance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentBalance.Location = New System.Drawing.Point(469, 33)
        Me.txtPaymentBalance.Name = "txtPaymentBalance"
        Me.txtPaymentBalance.ReadOnly = True
        Me.txtPaymentBalance.Size = New System.Drawing.Size(95, 15)
        Me.txtPaymentBalance.TabIndex = 13
        Me.txtPaymentBalance.TabStop = False
        Me.txtPaymentBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grpBoxPayment
        '
        Me.grpBoxPayment.CausesValidation = False
        Me.grpBoxPayment.Controls.Add(Me.dgvInvoices)
        Me.grpBoxPayment.Controls.Add(Me.panelBanner)
        Me.grpBoxPayment.Controls.Add(Me.panelPaymentList)
        Me.grpBoxPayment.Controls.Add(Me.panelHdr)
        Me.grpBoxPayment.Controls.Add(Me.panelPayment)
        Me.grpBoxPayment.Location = New System.Drawing.Point(1, 2)
        Me.grpBoxPayment.Name = "grpBoxPayment"
        Me.grpBoxPayment.Size = New System.Drawing.Size(1010, 641)
        Me.grpBoxPayment.TabIndex = 1
        Me.grpBoxPayment.TabStop = False
        Me.grpBoxPayment.Text = "grpBoxPayment"
        '
        'dgvInvoices
        '
        Me.dgvInvoices.AllowUserToAddRows = False
        Me.dgvInvoices.AllowUserToDeleteRows = False
        Me.dgvInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvInvoices.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInvoices.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInvoices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.invoice_date, Me.invoice_no, Me.inv_total, Me.TaxTotal, Me.prev_paid, Me.outstanding, Me.discount, Me.payable, Me.paying_now, Me.new_balance})
        Me.dgvInvoices.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvInvoices.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvInvoices.lastEditableColumn = 8
        Me.dgvInvoices.Location = New System.Drawing.Point(6, 152)
        Me.dgvInvoices.MultiSelect = False
        Me.dgvInvoices.Name = "dgvInvoices"
        DataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black
        Me.dgvInvoices.RowsDefaultCellStyle = DataGridViewCellStyle15
        Me.dgvInvoices.Size = New System.Drawing.Size(998, 233)
        Me.dgvInvoices.StandardTab = True
        Me.dgvInvoices.TabIndex = 1
        '
        'invoice_date
        '
        Me.invoice_date.FillWeight = 70.0!
        Me.invoice_date.HeaderText = "Invoice Date"
        Me.invoice_date.Name = "invoice_date"
        Me.invoice_date.ReadOnly = True
        Me.invoice_date.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'invoice_no
        '
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.invoice_no.DefaultCellStyle = DataGridViewCellStyle6
        Me.invoice_no.FillWeight = 40.0!
        Me.invoice_no.HeaderText = "Invoice No"
        Me.invoice_no.Name = "invoice_no"
        Me.invoice_no.ReadOnly = True
        Me.invoice_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'inv_total
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.inv_total.DefaultCellStyle = DataGridViewCellStyle7
        Me.inv_total.FillWeight = 80.0!
        Me.inv_total.HeaderText = "Inv Total"
        Me.inv_total.Name = "inv_total"
        Me.inv_total.ReadOnly = True
        Me.inv_total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TaxTotal
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.TaxTotal.DefaultCellStyle = DataGridViewCellStyle8
        Me.TaxTotal.FillWeight = 50.0!
        Me.TaxTotal.HeaderText = "Tax Total"
        Me.TaxTotal.Name = "TaxTotal"
        Me.TaxTotal.ReadOnly = True
        Me.TaxTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'prev_paid
        '
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.prev_paid.DefaultCellStyle = DataGridViewCellStyle9
        Me.prev_paid.FillWeight = 120.0!
        Me.prev_paid.HeaderText = "Prev.Payments"
        Me.prev_paid.Name = "prev_paid"
        Me.prev_paid.ReadOnly = True
        Me.prev_paid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'outstanding
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.outstanding.DefaultCellStyle = DataGridViewCellStyle10
        Me.outstanding.FillWeight = 80.0!
        Me.outstanding.HeaderText = "Outstanding"
        Me.outstanding.Name = "outstanding"
        Me.outstanding.ReadOnly = True
        Me.outstanding.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'discount
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black
        Me.discount.DefaultCellStyle = DataGridViewCellStyle11
        Me.discount.FillWeight = 70.0!
        Me.discount.HeaderText = "Discount"
        Me.discount.MaxInputLength = 11
        Me.discount.Name = "discount"
        Me.discount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'payable
        '
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.payable.DefaultCellStyle = DataGridViewCellStyle12
        Me.payable.FillWeight = 70.0!
        Me.payable.HeaderText = "Payable"
        Me.payable.Name = "payable"
        Me.payable.ReadOnly = True
        Me.payable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'paying_now
        '
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle13.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText
        Me.paying_now.DefaultCellStyle = DataGridViewCellStyle13
        Me.paying_now.FillWeight = 80.0!
        Me.paying_now.HeaderText = "Paying Now"
        Me.paying_now.MaxInputLength = 11
        Me.paying_now.Name = "paying_now"
        Me.paying_now.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'new_balance
        '
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.new_balance.DefaultCellStyle = DataGridViewCellStyle14
        Me.new_balance.FillWeight = 90.0!
        Me.new_balance.HeaderText = "New Balance"
        Me.new_balance.Name = "new_balance"
        Me.new_balance.ReadOnly = True
        Me.new_balance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'panelPaymentList
        '
        Me.panelPaymentList.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.panelPaymentList.CausesValidation = False
        Me.panelPaymentList.Controls.Add(Me.Label8)
        Me.panelPaymentList.Controls.Add(Me.listPayments)
        Me.panelPaymentList.Location = New System.Drawing.Point(6, 391)
        Me.panelPaymentList.Name = "panelPaymentList"
        Me.panelPaymentList.Size = New System.Drawing.Size(192, 240)
        Me.panelPaymentList.TabIndex = 47
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(7, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(164, 14)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "Previous Account Payments"
        '
        'listPayments
        '
        Me.listPayments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listPayments.CausesValidation = False
        Me.listPayments.Font = New System.Drawing.Font("Lucida Console", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listPayments.FormattingEnabled = True
        Me.listPayments.ItemHeight = 9
        Me.listPayments.Location = New System.Drawing.Point(5, 32)
        Me.listPayments.Name = "listPayments"
        Me.listPayments.ScrollAlwaysVisible = True
        Me.listPayments.Size = New System.Drawing.Size(181, 198)
        Me.listPayments.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.listPayments, "Double-click on selected payment to view details.")
        '
        'ucChildPayments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.CausesValidation = False
        Me.Controls.Add(Me.grpBoxPayment)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildPayments"
        Me.Size = New System.Drawing.Size(1013, 655)
        Me.panelBanner.ResumeLayout(False)
        Me.panelHdr.ResumeLayout(False)
        Me.panelHdr.PerformLayout()
        CType(Me.dgvPaymentDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelPayment.ResumeLayout(False)
        Me.panelPayment.PerformLayout()
        Me.grpBoxRefundType.ResumeLayout(False)
        Me.grpBoxPayment.ResumeLayout(False)
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelPaymentList.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelBanner As System.Windows.Forms.Panel
    Friend WithEvents labMainTitle As System.Windows.Forms.Label
    Friend WithEvents panelHdr As System.Windows.Forms.Panel
    Friend WithEvents LabSaleCustSrch As System.Windows.Forms.Label
    Friend WithEvents txtCustName As System.Windows.Forms.TextBox
    Friend WithEvents LabSaleCust As System.Windows.Forms.Label
    Friend WithEvents txtCustBarcode As System.Windows.Forms.TextBox
    Friend WithEvents panelPayment As System.Windows.Forms.Panel
    Friend WithEvents LabSalePayments As System.Windows.Forms.Label
    Friend WithEvents dgvPaymentDetails As System.Windows.Forms.DataGridView
    Friend WithEvents labChange As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents LabPaymentBalance As System.Windows.Forms.Label
    Friend WithEvents txtChange As System.Windows.Forms.TextBox
    Friend WithEvents txtPaymentBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents labComments As System.Windows.Forms.Label
    Friend WithEvents labHelp As System.Windows.Forms.Label
    Friend WithEvents btnCommit As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCustEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtCustPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtBalanceOwing As System.Windows.Forms.TextBox
    Friend WithEvents labTotalOwing As System.Windows.Forms.Label
    Friend WithEvents txtSaleSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents LabCreditAvail As System.Windows.Forms.Label
    Friend WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtInitialOwing As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCustMobile As System.Windows.Forms.TextBox
    Friend WithEvents grpBoxPayment As System.Windows.Forms.GroupBox
    Friend WithEvents panelPaymentList As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents labStart1 As System.Windows.Forms.Label
    Friend WithEvents listPayments As System.Windows.Forms.ListBox
    Friend WithEvents labCreditLimit As System.Windows.Forms.Label
    Friend WithEvents txtCreditNotesApplied As System.Windows.Forms.TextBox
    Friend WithEvents labApplyingCreditNotes As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents labCreditNoteAvail As System.Windows.Forms.Label
    Friend WithEvents grpBoxRefundType As System.Windows.Forms.GroupBox
    Friend WithEvents optRefundCredit As System.Windows.Forms.RadioButton
    Friend WithEvents optRefundCash As System.Windows.Forms.RadioButton
    Friend WithEvents btnCancel2 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnReverse As System.Windows.Forms.Button
    Friend WithEvents btnNewPayment As System.Windows.Forms.Button
    Friend WithEvents dgvInvoices As JMxPOS330.clsDgvGoods
    Friend WithEvents labTotalApplying As System.Windows.Forms.Label
    Friend WithEvents txtDiscountAllowed As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnStatement As System.Windows.Forms.Button
    Friend WithEvents cboStatementPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents invoice_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invoice_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents inv_total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TaxTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prev_paid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents outstanding As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents discount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents payable As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents paying_now As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents new_balance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentType_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents labReversedInvoices As System.Windows.Forms.Label
End Class
