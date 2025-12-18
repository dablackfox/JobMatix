<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildSubscription
    Inherits System.Windows.Forms.UserControl  '== System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucChildSubscription))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Subs Products")
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelSubsBanner = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.labHdrGR = New System.Windows.Forms.Label()
        Me.grpBoxSubsLookup = New System.Windows.Forms.GroupBox()
        Me.grpBoxSub = New System.Windows.Forms.GroupBox()
        Me.btnEditSub = New System.Windows.Forms.Button()
        Me.btnCancelSub = New System.Windows.Forms.Button()
        Me.grpBoxSubDetail = New System.Windows.Forms.GroupBox()
        Me.chkOkToEmailInvoices = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.dtPickerEnd = New System.Windows.Forms.DateTimePicker()
        Me.chkEndDate = New System.Windows.Forms.CheckBox()
        Me.labAction = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.labLastPeriodBilled = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.btnAddItem = New System.Windows.Forms.Button()
        Me.labInfoStarted = New System.Windows.Forms.Label()
        Me.labShowActivated = New System.Windows.Forms.Label()
        Me.btnDeleteItem = New System.Windows.Forms.Button()
        Me.chkActivateNow = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.labSubsId = New System.Windows.Forms.Label()
        Me.listViewSubsItems = New System.Windows.Forms.ListView()
        Me.ItemNo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ItemBarcode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Cat1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Description = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Price = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Qty = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Amount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.stock_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TaxCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnSaveEdit = New System.Windows.Forms.Button()
        Me.labShowPeriod = New System.Windows.Forms.Label()
        Me.btnCancelEdit = New System.Windows.Forms.Button()
        Me.dtPickerStart = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.labShowTotal = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboBillingCycle = New System.Windows.Forms.ComboBox()
        Me.labSubCustomerName = New System.Windows.Forms.Label()
        Me.txtSubCustBarcode = New System.Windows.Forms.TextBox()
        Me.labCustomerLab = New System.Windows.Forms.Label()
        Me.listSubInvoices = New System.Windows.Forms.ListBox()
        Me.btnNewSub = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labInvoiceCount = New System.Windows.Forms.Label()
        Me.frameBrowse = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmdClearSubsSearch = New System.Windows.Forms.Button()
        Me.cmdSubsSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSubsSearch = New System.Windows.Forms.TextBox()
        Me.dgvSubsList = New System.Windows.Forms.DataGridView()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.labRecCount = New System.Windows.Forms.Label()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageSubs = New System.Windows.Forms.TabPage()
        Me.TabPageInvoicing = New System.Windows.Forms.TabPage()
        Me.grpBoxBilling = New System.Windows.Forms.GroupBox()
        Me.panelBillingHdr = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.labPdfPrinter = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnPauseInvoicing = New System.Windows.Forms.Button()
        Me.panelGroupAction = New System.Windows.Forms.Panel()
        Me.btnUnMarkAll = New System.Windows.Forms.Button()
        Me.btnMarkAll = New System.Windows.Forms.Button()
        Me.btnInvoiceAllMarked = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtLogStatus = New System.Windows.Forms.TextBox()
        Me.btnRefreshBillGrid = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dgvInvoices = New System.Windows.Forms.DataGridView()
        Me.sub_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.customer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.billing_period = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.item_list = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.start_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.total_inc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prev_inv_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.due_date_period = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.invoice_now = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MarkToInvoice = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.email_invoice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.customer_id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.customer_email = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPageAnalysis = New System.Windows.Forms.TabPage()
        Me.panelPeriodReport = New System.Windows.Forms.Panel()
        Me.dgvPeriodReport = New System.Windows.Forms.DataGridView()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.panelProducts = New System.Windows.Forms.Panel()
        Me.btnRefreshAnalysis = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.clsPosTreeViewProducts = New JMxPOS330.clsPosTreeView()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.billingPeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.stock_barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.stock_description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.total_qty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.totalSellActual_inc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.panelSubsBanner.SuspendLayout()
        Me.grpBoxSubsLookup.SuspendLayout()
        Me.grpBoxSub.SuspendLayout()
        Me.grpBoxSubDetail.SuspendLayout()
        Me.frameBrowse.SuspendLayout()
        CType(Me.dgvSubsList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPageSubs.SuspendLayout()
        Me.TabPageInvoicing.SuspendLayout()
        Me.grpBoxBilling.SuspendLayout()
        Me.panelBillingHdr.SuspendLayout()
        Me.panelGroupAction.SuspendLayout()
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageAnalysis.SuspendLayout()
        Me.panelPeriodReport.SuspendLayout()
        CType(Me.dgvPeriodReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelProducts.SuspendLayout()
        Me.SuspendLayout()
        '
        'panelSubsBanner
        '
        Me.panelSubsBanner.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.panelSubsBanner.Controls.Add(Me.Label14)
        Me.panelSubsBanner.Controls.Add(Me.labHdrGR)
        Me.panelSubsBanner.Location = New System.Drawing.Point(3, 3)
        Me.panelSubsBanner.Name = "panelSubsBanner"
        Me.panelSubsBanner.Size = New System.Drawing.Size(449, 59)
        Me.panelSubsBanner.TabIndex = 5
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label14.Location = New System.Drawing.Point(126, 13)
        Me.Label14.Name = "Label14"
        Me.Label14.Padding = New System.Windows.Forms.Padding(3)
        Me.Label14.Size = New System.Drawing.Size(302, 35)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "Subscriptions Support sets up and tracks regular (recurrent) billing events for p" & _
    "eriodic services or product rentals.."
        '
        'labHdrGR
        '
        Me.labHdrGR.BackColor = System.Drawing.Color.Transparent
        Me.labHdrGR.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdrGR.ForeColor = System.Drawing.Color.Indigo
        Me.labHdrGR.Location = New System.Drawing.Point(8, 7)
        Me.labHdrGR.Name = "labHdrGR"
        Me.labHdrGR.Size = New System.Drawing.Size(112, 41)
        Me.labHdrGR.TabIndex = 6
        Me.labHdrGR.Text = "Maintain Subscriptions"
        '
        'grpBoxSubsLookup
        '
        Me.grpBoxSubsLookup.BackColor = System.Drawing.Color.MistyRose
        Me.grpBoxSubsLookup.CausesValidation = False
        Me.grpBoxSubsLookup.Controls.Add(Me.grpBoxSub)
        Me.grpBoxSubsLookup.Controls.Add(Me.frameBrowse)
        Me.grpBoxSubsLookup.Controls.Add(Me.panelSubsBanner)
        Me.grpBoxSubsLookup.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxSubsLookup.Location = New System.Drawing.Point(3, 4)
        Me.grpBoxSubsLookup.Name = "grpBoxSubsLookup"
        Me.grpBoxSubsLookup.Size = New System.Drawing.Size(986, 598)
        Me.grpBoxSubsLookup.TabIndex = 6
        Me.grpBoxSubsLookup.TabStop = False
        Me.grpBoxSubsLookup.Text = "grpBoxSubsLookup"
        '
        'grpBoxSub
        '
        Me.grpBoxSub.BackColor = System.Drawing.Color.White
        Me.grpBoxSub.CausesValidation = False
        Me.grpBoxSub.Controls.Add(Me.btnEditSub)
        Me.grpBoxSub.Controls.Add(Me.btnCancelSub)
        Me.grpBoxSub.Controls.Add(Me.grpBoxSubDetail)
        Me.grpBoxSub.Controls.Add(Me.listSubInvoices)
        Me.grpBoxSub.Controls.Add(Me.btnNewSub)
        Me.grpBoxSub.Controls.Add(Me.Label1)
        Me.grpBoxSub.Controls.Add(Me.labInvoiceCount)
        Me.grpBoxSub.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxSub.Location = New System.Drawing.Point(463, 11)
        Me.grpBoxSub.Name = "grpBoxSub"
        Me.grpBoxSub.Size = New System.Drawing.Size(516, 581)
        Me.grpBoxSub.TabIndex = 42
        Me.grpBoxSub.TabStop = False
        Me.grpBoxSub.Text = "grpBoxSub"
        '
        'btnEditSub
        '
        Me.btnEditSub.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnEditSub.CausesValidation = False
        Me.btnEditSub.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnEditSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditSub.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditSub.Location = New System.Drawing.Point(30, 19)
        Me.btnEditSub.Name = "btnEditSub"
        Me.btnEditSub.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.btnEditSub.Size = New System.Drawing.Size(101, 41)
        Me.btnEditSub.TabIndex = 0
        Me.btnEditSub.Text = "Edit Subscription"
        Me.ToolTip1.SetToolTip(Me.btnEditSub, "Edit this Subscription.." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Add/subtract Items..)")
        Me.btnEditSub.UseVisualStyleBackColor = False
        '
        'btnCancelSub
        '
        Me.btnCancelSub.BackColor = System.Drawing.Color.Pink
        Me.btnCancelSub.CausesValidation = False
        Me.btnCancelSub.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCancelSub.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelSub.Image = CType(resources.GetObject("btnCancelSub.Image"), System.Drawing.Image)
        Me.btnCancelSub.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnCancelSub.Location = New System.Drawing.Point(417, 19)
        Me.btnCancelSub.Name = "btnCancelSub"
        Me.btnCancelSub.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.btnCancelSub.Size = New System.Drawing.Size(88, 42)
        Me.btnCancelSub.TabIndex = 3
        Me.btnCancelSub.Text = "Cancel Subscription"
        Me.ToolTip1.SetToolTip(Me.btnCancelSub, "Cancel this Subscription Forever..")
        Me.btnCancelSub.UseVisualStyleBackColor = False
        '
        'grpBoxSubDetail
        '
        Me.grpBoxSubDetail.CausesValidation = False
        Me.grpBoxSubDetail.Controls.Add(Me.chkOkToEmailInvoices)
        Me.grpBoxSubDetail.Controls.Add(Me.Label15)
        Me.grpBoxSubDetail.Controls.Add(Me.dtPickerEnd)
        Me.grpBoxSubDetail.Controls.Add(Me.chkEndDate)
        Me.grpBoxSubDetail.Controls.Add(Me.labAction)
        Me.grpBoxSubDetail.Controls.Add(Me.Label11)
        Me.grpBoxSubDetail.Controls.Add(Me.labLastPeriodBilled)
        Me.grpBoxSubDetail.Controls.Add(Me.Label6)
        Me.grpBoxSubDetail.Controls.Add(Me.txtComments)
        Me.grpBoxSubDetail.Controls.Add(Me.btnAddItem)
        Me.grpBoxSubDetail.Controls.Add(Me.labInfoStarted)
        Me.grpBoxSubDetail.Controls.Add(Me.labShowActivated)
        Me.grpBoxSubDetail.Controls.Add(Me.btnDeleteItem)
        Me.grpBoxSubDetail.Controls.Add(Me.chkActivateNow)
        Me.grpBoxSubDetail.Controls.Add(Me.Label4)
        Me.grpBoxSubDetail.Controls.Add(Me.Label5)
        Me.grpBoxSubDetail.Controls.Add(Me.labSubsId)
        Me.grpBoxSubDetail.Controls.Add(Me.listViewSubsItems)
        Me.grpBoxSubDetail.Controls.Add(Me.btnSaveEdit)
        Me.grpBoxSubDetail.Controls.Add(Me.labShowPeriod)
        Me.grpBoxSubDetail.Controls.Add(Me.btnCancelEdit)
        Me.grpBoxSubDetail.Controls.Add(Me.dtPickerStart)
        Me.grpBoxSubDetail.Controls.Add(Me.Label2)
        Me.grpBoxSubDetail.Controls.Add(Me.Label12)
        Me.grpBoxSubDetail.Controls.Add(Me.labShowTotal)
        Me.grpBoxSubDetail.Controls.Add(Me.Label10)
        Me.grpBoxSubDetail.Controls.Add(Me.cboBillingCycle)
        Me.grpBoxSubDetail.Controls.Add(Me.labSubCustomerName)
        Me.grpBoxSubDetail.Controls.Add(Me.txtSubCustBarcode)
        Me.grpBoxSubDetail.Controls.Add(Me.labCustomerLab)
        Me.grpBoxSubDetail.Location = New System.Drawing.Point(4, 68)
        Me.grpBoxSubDetail.Name = "grpBoxSubDetail"
        Me.grpBoxSubDetail.Size = New System.Drawing.Size(511, 507)
        Me.grpBoxSubDetail.TabIndex = 4
        Me.grpBoxSubDetail.TabStop = False
        Me.grpBoxSubDetail.Text = "grpBoxSubDetail"
        '
        'chkOkToEmailInvoices
        '
        Me.chkOkToEmailInvoices.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkOkToEmailInvoices.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOkToEmailInvoices.Location = New System.Drawing.Point(305, 78)
        Me.chkOkToEmailInvoices.Name = "chkOkToEmailInvoices"
        Me.chkOkToEmailInvoices.Size = New System.Drawing.Size(140, 23)
        Me.chkOkToEmailInvoices.TabIndex = 66
        Me.chkOkToEmailInvoices.Text = "Ok To Email Invoices"
        Me.chkOkToEmailInvoices.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(367, 340)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(139, 19)
        Me.Label15.TabIndex = 65
        Me.Label15.Text = "Right-click on Item to Edit.."
        '
        'dtPickerEnd
        '
        Me.dtPickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPickerEnd.Location = New System.Drawing.Point(114, 150)
        Me.dtPickerEnd.Name = "dtPickerEnd"
        Me.dtPickerEnd.Size = New System.Drawing.Size(124, 20)
        Me.dtPickerEnd.TabIndex = 5
        '
        'chkEndDate
        '
        Me.chkEndDate.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkEndDate.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEndDate.Location = New System.Drawing.Point(11, 154)
        Me.chkEndDate.Name = "chkEndDate"
        Me.chkEndDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkEndDate.Size = New System.Drawing.Size(93, 16)
        Me.chkEndDate.TabIndex = 4
        Me.chkEndDate.Text = "End Date"
        Me.chkEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.chkEndDate, "Check this box if there is to be a Termination or Expiry Date")
        Me.chkEndDate.UseVisualStyleBackColor = False
        '
        'labAction
        '
        Me.labAction.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labAction.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labAction.ForeColor = System.Drawing.Color.SaddleBrown
        Me.labAction.Location = New System.Drawing.Point(302, 35)
        Me.labAction.Name = "labAction"
        Me.labAction.Size = New System.Drawing.Size(98, 27)
        Me.labAction.TabIndex = 40
        Me.labAction.Text = "labAction"
        Me.labAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(18, 219)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 20)
        Me.Label11.TabIndex = 62
        Me.Label11.Text = "Last Billing:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labLastPeriodBilled
        '
        Me.labLastPeriodBilled.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labLastPeriodBilled.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labLastPeriodBilled.Location = New System.Drawing.Point(111, 219)
        Me.labLastPeriodBilled.Name = "labLastPeriodBilled"
        Me.labLastPeriodBilled.Size = New System.Drawing.Size(221, 20)
        Me.labLastPeriodBilled.TabIndex = 61
        Me.labLastPeriodBilled.Text = "labLastPeriodBilled"
        Me.labLastPeriodBilled.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(23, 269)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 20)
        Me.Label6.TabIndex = 59
        Me.Label6.Text = "Comments"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComments.Location = New System.Drawing.Point(109, 269)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(285, 49)
        Me.txtComments.TabIndex = 7
        '
        'btnAddItem
        '
        Me.btnAddItem.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnAddItem.Location = New System.Drawing.Point(204, 336)
        Me.btnAddItem.Name = "btnAddItem"
        Me.btnAddItem.Size = New System.Drawing.Size(74, 21)
        Me.btnAddItem.TabIndex = 9
        Me.btnAddItem.Text = "Add Item"
        Me.ToolTip1.SetToolTip(Me.btnAddItem, "Add new item..")
        Me.btnAddItem.UseVisualStyleBackColor = False
        '
        'labInfoStarted
        '
        Me.labInfoStarted.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labInfoStarted.Location = New System.Drawing.Point(258, 121)
        Me.labInfoStarted.Name = "labInfoStarted"
        Me.labInfoStarted.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labInfoStarted.Size = New System.Drawing.Size(187, 59)
        Me.labInfoStarted.TabIndex = 55
        Me.labInfoStarted.Text = "Note: Billing will be started from the Start Date once it is set and activated.. " & _
    "Once activated, date can not be changed." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'labShowActivated
        '
        Me.labShowActivated.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labShowActivated.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labShowActivated.Location = New System.Drawing.Point(20, 181)
        Me.labShowActivated.Name = "labShowActivated"
        Me.labShowActivated.Size = New System.Drawing.Size(83, 31)
        Me.labShowActivated.TabIndex = 53
        Me.labShowActivated.Text = "labShowActivated"
        '
        'btnDeleteItem
        '
        Me.btnDeleteItem.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnDeleteItem.Location = New System.Drawing.Point(286, 336)
        Me.btnDeleteItem.Name = "btnDeleteItem"
        Me.btnDeleteItem.Size = New System.Drawing.Size(74, 21)
        Me.btnDeleteItem.TabIndex = 10
        Me.btnDeleteItem.Text = "Delete Item"
        Me.ToolTip1.SetToolTip(Me.btnDeleteItem, "Delete Selected Item")
        Me.btnDeleteItem.UseVisualStyleBackColor = False
        '
        'chkActivateNow
        '
        Me.chkActivateNow.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkActivateNow.Location = New System.Drawing.Point(112, 183)
        Me.chkActivateNow.Name = "chkActivateNow"
        Me.chkActivateNow.Size = New System.Drawing.Size(92, 16)
        Me.chkActivateNow.TabIndex = 6
        Me.chkActivateNow.Text = "Activate Now"
        Me.chkActivateNow.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(145, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 12)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Subs. ID: "
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 342)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(193, 14)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Subs must have billable Items.."
        '
        'labSubsId
        '
        Me.labSubsId.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labSubsId.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSubsId.Location = New System.Drawing.Point(225, 14)
        Me.labSubsId.Name = "labSubsId"
        Me.labSubsId.Size = New System.Drawing.Size(53, 15)
        Me.labSubsId.TabIndex = 45
        Me.labSubsId.Text = "labSubsId"
        Me.labSubsId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'listViewSubsItems
        '
        Me.listViewSubsItems.BackColor = System.Drawing.Color.WhiteSmoke
        Me.listViewSubsItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ItemNo, Me.ItemBarcode, Me.Cat1, Me.Description, Me.Price, Me.Qty, Me.Amount, Me.stock_id, Me.TaxCode})
        Me.listViewSubsItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewSubsItems.FullRowSelect = True
        Me.listViewSubsItems.GridLines = True
        Me.listViewSubsItems.HideSelection = False
        Me.listViewSubsItems.Location = New System.Drawing.Point(6, 359)
        Me.listViewSubsItems.MultiSelect = False
        Me.listViewSubsItems.Name = "listViewSubsItems"
        Me.listViewSubsItems.Size = New System.Drawing.Size(500, 142)
        Me.listViewSubsItems.TabIndex = 8
        Me.listViewSubsItems.TabStop = False
        Me.ToolTip1.SetToolTip(Me.listViewSubsItems, "Right-click on Line Item to Edit Price/Quantity..")
        Me.listViewSubsItems.UseCompatibleStateImageBehavior = False
        Me.listViewSubsItems.View = System.Windows.Forms.View.Details
        '
        'ItemNo
        '
        Me.ItemNo.Text = "Item#"
        Me.ItemNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ItemNo.Width = 50
        '
        'ItemBarcode
        '
        Me.ItemBarcode.Text = "Barcode"
        Me.ItemBarcode.Width = 70
        '
        'Cat1
        '
        Me.Cat1.Text = "Cat1"
        Me.Cat1.Width = 50
        '
        'Description
        '
        Me.Description.Text = "Description"
        Me.Description.Width = 130
        '
        'Price
        '
        Me.Price.Text = "Price"
        Me.Price.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Qty
        '
        Me.Qty.Text = "Qty"
        Me.Qty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Qty.Width = 40
        '
        'Amount
        '
        Me.Amount.Text = "Total_inc"
        Me.Amount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Amount.Width = 80
        '
        'stock_id
        '
        Me.stock_id.Text = "Stock_id"
        '
        'TaxCode
        '
        Me.TaxCode.Text = "Tax"
        '
        'btnSaveEdit
        '
        Me.btnSaveEdit.BackColor = System.Drawing.Color.GreenYellow
        Me.btnSaveEdit.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnSaveEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveEdit.Location = New System.Drawing.Point(424, 279)
        Me.btnSaveEdit.Name = "btnSaveEdit"
        Me.btnSaveEdit.Size = New System.Drawing.Size(67, 39)
        Me.btnSaveEdit.TabIndex = 12
        Me.btnSaveEdit.Text = "ok- Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveEdit, "Commit (save) Changes or New Item.")
        Me.btnSaveEdit.UseVisualStyleBackColor = False
        '
        'labShowPeriod
        '
        Me.labShowPeriod.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labShowPeriod.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labShowPeriod.Location = New System.Drawing.Point(21, 99)
        Me.labShowPeriod.Name = "labShowPeriod"
        Me.labShowPeriod.Size = New System.Drawing.Size(87, 16)
        Me.labShowPeriod.TabIndex = 47
        Me.labShowPeriod.Text = "labShowPeriod"
        Me.labShowPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCancelEdit
        '
        Me.btnCancelEdit.BackColor = System.Drawing.Color.Thistle
        Me.btnCancelEdit.CausesValidation = False
        Me.btnCancelEdit.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCancelEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelEdit.Location = New System.Drawing.Point(424, 221)
        Me.btnCancelEdit.Name = "btnCancelEdit"
        Me.btnCancelEdit.Size = New System.Drawing.Size(67, 39)
        Me.btnCancelEdit.TabIndex = 11
        Me.btnCancelEdit.Text = "X  Cancel"
        Me.ToolTip1.SetToolTip(Me.btnCancelEdit, "Cancel this edit or New Item.")
        Me.btnCancelEdit.UseVisualStyleBackColor = False
        '
        'dtPickerStart
        '
        Me.dtPickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtPickerStart.Location = New System.Drawing.Point(114, 123)
        Me.dtPickerStart.Name = "dtPickerStart"
        Me.dtPickerStart.Size = New System.Drawing.Size(124, 20)
        Me.dtPickerStart.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 239)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 20)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Invoice Total: "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(18, 124)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(79, 16)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "Start Date:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labShowTotal
        '
        Me.labShowTotal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labShowTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labShowTotal.Location = New System.Drawing.Point(111, 240)
        Me.labShowTotal.Name = "labShowTotal"
        Me.labShowTotal.Size = New System.Drawing.Size(95, 20)
        Me.labShowTotal.TabIndex = 41
        Me.labShowTotal.Text = "labShowTotal"
        Me.labShowTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(18, 81)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 18)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Billing Cycle"
        '
        'cboBillingCycle
        '
        Me.cboBillingCycle.BackColor = System.Drawing.Color.LavenderBlush
        Me.cboBillingCycle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBillingCycle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboBillingCycle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBillingCycle.FormattingEnabled = True
        Me.cboBillingCycle.Location = New System.Drawing.Point(114, 77)
        Me.cboBillingCycle.Name = "cboBillingCycle"
        Me.cboBillingCycle.Size = New System.Drawing.Size(124, 23)
        Me.cboBillingCycle.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cboBillingCycle, "Billing Cycle can't be changed after committing New Subscription..")
        '
        'labSubCustomerName
        '
        Me.labSubCustomerName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labSubCustomerName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSubCustomerName.Location = New System.Drawing.Point(111, 35)
        Me.labSubCustomerName.Name = "labSubCustomerName"
        Me.labSubCustomerName.Size = New System.Drawing.Size(185, 37)
        Me.labSubCustomerName.TabIndex = 44
        Me.labSubCustomerName.Text = "labSubCustomerName"
        '
        'txtSubCustBarcode
        '
        Me.txtSubCustBarcode.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSubCustBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubCustBarcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubCustBarcode.Location = New System.Drawing.Point(26, 37)
        Me.txtSubCustBarcode.Name = "txtSubCustBarcode"
        Me.txtSubCustBarcode.Size = New System.Drawing.Size(69, 21)
        Me.txtSubCustBarcode.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtSubCustBarcode, "Customer Barcode-   " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  F2 to Search." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- (Account Custs. only.)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'labCustomerLab
        '
        Me.labCustomerLab.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labCustomerLab.ForeColor = System.Drawing.Color.MediumBlue
        Me.labCustomerLab.Location = New System.Drawing.Point(18, 20)
        Me.labCustomerLab.Name = "labCustomerLab"
        Me.labCustomerLab.Size = New System.Drawing.Size(121, 16)
        Me.labCustomerLab.TabIndex = 0
        Me.labCustomerLab.Text = "Customer Barcode:"
        '
        'listSubInvoices
        '
        Me.listSubInvoices.BackColor = System.Drawing.Color.WhiteSmoke
        Me.listSubInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listSubInvoices.FormattingEnabled = True
        Me.listSubInvoices.Location = New System.Drawing.Point(284, 26)
        Me.listSubInvoices.Name = "listSubInvoices"
        Me.listSubInvoices.Size = New System.Drawing.Size(114, 39)
        Me.listSubInvoices.TabIndex = 2
        '
        'btnNewSub
        '
        Me.btnNewSub.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnNewSub.CausesValidation = False
        Me.btnNewSub.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnNewSub.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewSub.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewSub.Location = New System.Drawing.Point(152, 19)
        Me.btnNewSub.Name = "btnNewSub"
        Me.btnNewSub.Size = New System.Drawing.Size(105, 41)
        Me.btnNewSub.TabIndex = 1
        Me.btnNewSub.Text = "New Subscription"
        Me.ToolTip1.SetToolTip(Me.btnNewSub, "Create new Subscription..")
        Me.btnNewSub.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(281, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 12)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Invoices: "
        '
        'labInvoiceCount
        '
        Me.labInvoiceCount.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labInvoiceCount.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labInvoiceCount.Location = New System.Drawing.Point(360, 9)
        Me.labInvoiceCount.Name = "labInvoiceCount"
        Me.labInvoiceCount.Size = New System.Drawing.Size(44, 16)
        Me.labInvoiceCount.TabIndex = 57
        Me.labInvoiceCount.Text = "labInvoiceCount"
        Me.labInvoiceCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frameBrowse
        '
        Me.frameBrowse.BackColor = System.Drawing.Color.White
        Me.frameBrowse.Controls.Add(Me.Label22)
        Me.frameBrowse.Controls.Add(Me.Label21)
        Me.frameBrowse.Controls.Add(Me.cmdClearSubsSearch)
        Me.frameBrowse.Controls.Add(Me.cmdSubsSearch)
        Me.frameBrowse.Controls.Add(Me.Label3)
        Me.frameBrowse.Controls.Add(Me.txtSubsSearch)
        Me.frameBrowse.Controls.Add(Me.dgvSubsList)
        Me.frameBrowse.Controls.Add(Me.txtFind)
        Me.frameBrowse.Controls.Add(Me.labRecCount)
        Me.frameBrowse.Controls.Add(Me.LabFind)
        Me.frameBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameBrowse.Location = New System.Drawing.Point(3, 63)
        Me.frameBrowse.Name = "frameBrowse"
        Me.frameBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameBrowse.Size = New System.Drawing.Size(449, 526)
        Me.frameBrowse.TabIndex = 23
        Me.frameBrowse.TabStop = False
        Me.frameBrowse.Text = "FrameBrowse"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(247, 16)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(83, 13)
        Me.Label22.TabIndex = 82
        Me.Label22.Text = "Records found."
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(196, 60)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(121, 12)
        Me.Label21.TabIndex = 81
        Me.Label21.Text = "Full Text Filter (Srch):"
        '
        'cmdClearSubsSearch
        '
        Me.cmdClearSubsSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClearSubsSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearSubsSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClearSubsSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearSubsSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearSubsSearch.Location = New System.Drawing.Point(361, 34)
        Me.cmdClearSubsSearch.Name = "cmdClearSubsSearch"
        Me.cmdClearSubsSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearSubsSearch.Size = New System.Drawing.Size(53, 23)
        Me.cmdClearSubsSearch.TabIndex = 80
        Me.cmdClearSubsSearch.Text = "Clear"
        Me.cmdClearSubsSearch.UseVisualStyleBackColor = False
        '
        'cmdSubsSearch
        '
        Me.cmdSubsSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdSubsSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSubsSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSubsSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSubsSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSubsSearch.Location = New System.Drawing.Point(361, 67)
        Me.cmdSubsSearch.Name = "cmdSubsSearch"
        Me.cmdSubsSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSubsSearch.Size = New System.Drawing.Size(53, 23)
        Me.cmdSubsSearch.TabIndex = 79
        Me.cmdSubsSearch.Text = "Search"
        Me.cmdSubsSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSubsSearch.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(161, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Subscriptions List"
        '
        'txtSubsSearch
        '
        Me.txtSubsSearch.AcceptsReturn = True
        Me.txtSubsSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtSubsSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSubsSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubsSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubsSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubsSearch.Location = New System.Drawing.Point(196, 77)
        Me.txtSubsSearch.MaxLength = 0
        Me.txtSubsSearch.Name = "txtSubsSearch"
        Me.txtSubsSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubsSearch.Size = New System.Drawing.Size(150, 13)
        Me.txtSubsSearch.TabIndex = 78
        Me.txtSubsSearch.Text = "txtSubsSearch"
        '
        'dgvSubsList
        '
        Me.dgvSubsList.AllowUserToAddRows = False
        Me.dgvSubsList.AllowUserToDeleteRows = False
        Me.dgvSubsList.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvSubsList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvSubsList.ColumnHeadersHeight = 18
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSubsList.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvSubsList.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvSubsList.Location = New System.Drawing.Point(8, 97)
        Me.dgvSubsList.MultiSelect = False
        Me.dgvSubsList.Name = "dgvSubsList"
        Me.dgvSubsList.ReadOnly = True
        Me.dgvSubsList.RowHeadersWidth = 17
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvSubsList.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSubsList.RowTemplate.Height = 17
        Me.dgvSubsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSubsList.Size = New System.Drawing.Size(435, 420)
        Me.dgvSubsList.StandardTab = True
        Me.dgvSubsList.TabIndex = 4
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(11, 75)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(132, 15)
        Me.txtFind.TabIndex = 2
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(197, 16)
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
        Me.LabFind.Location = New System.Drawing.Point(8, 47)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(135, 25)
        Me.LabFind.TabIndex = 18
        Me.LabFind.Text = "LabFind"
        '
        'TabControl1
        '
        Me.TabControl1.CausesValidation = False
        Me.TabControl1.Controls.Add(Me.TabPageSubs)
        Me.TabControl1.Controls.Add(Me.TabPageInvoicing)
        Me.TabControl1.Controls.Add(Me.TabPageAnalysis)
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(3, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1006, 649)
        Me.TabControl1.TabIndex = 15
        '
        'TabPageSubs
        '
        Me.TabPageSubs.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.TabPageSubs.CausesValidation = False
        Me.TabPageSubs.Controls.Add(Me.grpBoxSubsLookup)
        Me.TabPageSubs.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageSubs.Location = New System.Drawing.Point(4, 25)
        Me.TabPageSubs.Name = "TabPageSubs"
        Me.TabPageSubs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageSubs.Size = New System.Drawing.Size(998, 620)
        Me.TabPageSubs.TabIndex = 0
        Me.TabPageSubs.Text = "Maintain Subscriptions"
        '
        'TabPageInvoicing
        '
        Me.TabPageInvoicing.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(158, Byte), Integer))
        Me.TabPageInvoicing.CausesValidation = False
        Me.TabPageInvoicing.Controls.Add(Me.grpBoxBilling)
        Me.TabPageInvoicing.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageInvoicing.Location = New System.Drawing.Point(4, 25)
        Me.TabPageInvoicing.Name = "TabPageInvoicing"
        Me.TabPageInvoicing.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageInvoicing.Size = New System.Drawing.Size(998, 620)
        Me.TabPageInvoicing.TabIndex = 1
        Me.TabPageInvoicing.Text = "-- Invoicing Log --"
        '
        'grpBoxBilling
        '
        Me.grpBoxBilling.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.grpBoxBilling.CausesValidation = False
        Me.grpBoxBilling.Controls.Add(Me.panelBillingHdr)
        Me.grpBoxBilling.Controls.Add(Me.dgvInvoices)
        Me.grpBoxBilling.Location = New System.Drawing.Point(4, 8)
        Me.grpBoxBilling.Name = "grpBoxBilling"
        Me.grpBoxBilling.Size = New System.Drawing.Size(985, 604)
        Me.grpBoxBilling.TabIndex = 6
        Me.grpBoxBilling.TabStop = False
        Me.grpBoxBilling.Text = "grpBoxBilling"
        '
        'panelBillingHdr
        '
        Me.panelBillingHdr.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelBillingHdr.CausesValidation = False
        Me.panelBillingHdr.Controls.Add(Me.Label17)
        Me.panelBillingHdr.Controls.Add(Me.labPdfPrinter)
        Me.panelBillingHdr.Controls.Add(Me.Label16)
        Me.panelBillingHdr.Controls.Add(Me.btnPauseInvoicing)
        Me.panelBillingHdr.Controls.Add(Me.panelGroupAction)
        Me.panelBillingHdr.Controls.Add(Me.Label7)
        Me.panelBillingHdr.Controls.Add(Me.txtLogStatus)
        Me.panelBillingHdr.Controls.Add(Me.btnRefreshBillGrid)
        Me.panelBillingHdr.Controls.Add(Me.Label8)
        Me.panelBillingHdr.Controls.Add(Me.Label9)
        Me.panelBillingHdr.Location = New System.Drawing.Point(3, 2)
        Me.panelBillingHdr.Name = "panelBillingHdr"
        Me.panelBillingHdr.Size = New System.Drawing.Size(976, 139)
        Me.panelBillingHdr.TabIndex = 0
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(653, 8)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(99, 61)
        Me.Label17.TabIndex = 77
        Me.Label17.Text = "Invoice a batch of Subscriptions in one invoicing run.."
        '
        'labPdfPrinter
        '
        Me.labPdfPrinter.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labPdfPrinter.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPdfPrinter.ForeColor = System.Drawing.Color.SaddleBrown
        Me.labPdfPrinter.Location = New System.Drawing.Point(432, 114)
        Me.labPdfPrinter.Name = "labPdfPrinter"
        Me.labPdfPrinter.Size = New System.Drawing.Size(210, 19)
        Me.labPdfPrinter.TabIndex = 76
        Me.labPdfPrinter.Text = "labPdfPrinter"
        Me.labPdfPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(431, 81)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(216, 35)
        Me.Label16.TabIndex = 75
        Me.Label16.Text = "Note: An Adobe (or Microsoft) PDF Printer is neded for Emailing function.. Yours " & _
    "is:"
        '
        'btnPauseInvoicing
        '
        Me.btnPauseInvoicing.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPauseInvoicing.CausesValidation = False
        Me.btnPauseInvoicing.Image = CType(resources.GetObject("btnPauseInvoicing.Image"), System.Drawing.Image)
        Me.btnPauseInvoicing.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPauseInvoicing.Location = New System.Drawing.Point(653, 71)
        Me.btnPauseInvoicing.Name = "btnPauseInvoicing"
        Me.btnPauseInvoicing.Size = New System.Drawing.Size(101, 53)
        Me.btnPauseInvoicing.TabIndex = 9
        Me.btnPauseInvoicing.Text = "Stop Invoicing"
        Me.btnPauseInvoicing.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPauseInvoicing.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnPauseInvoicing, "Pause Group Invoicing...")
        Me.btnPauseInvoicing.UseVisualStyleBackColor = False
        '
        'panelGroupAction
        '
        Me.panelGroupAction.CausesValidation = False
        Me.panelGroupAction.Controls.Add(Me.btnUnMarkAll)
        Me.panelGroupAction.Controls.Add(Me.btnMarkAll)
        Me.panelGroupAction.Controls.Add(Me.btnInvoiceAllMarked)
        Me.panelGroupAction.Location = New System.Drawing.Point(429, 4)
        Me.panelGroupAction.Name = "panelGroupAction"
        Me.panelGroupAction.Size = New System.Drawing.Size(202, 72)
        Me.panelGroupAction.TabIndex = 7
        '
        'btnUnMarkAll
        '
        Me.btnUnMarkAll.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnUnMarkAll.CausesValidation = False
        Me.btnUnMarkAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUnMarkAll.Location = New System.Drawing.Point(8, 38)
        Me.btnUnMarkAll.Name = "btnUnMarkAll"
        Me.btnUnMarkAll.Size = New System.Drawing.Size(76, 27)
        Me.btnUnMarkAll.TabIndex = 8
        Me.btnUnMarkAll.Text = "Un-Mark All"
        Me.btnUnMarkAll.UseVisualStyleBackColor = False
        '
        'btnMarkAll
        '
        Me.btnMarkAll.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMarkAll.CausesValidation = False
        Me.btnMarkAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMarkAll.Location = New System.Drawing.Point(8, 9)
        Me.btnMarkAll.Name = "btnMarkAll"
        Me.btnMarkAll.Size = New System.Drawing.Size(76, 27)
        Me.btnMarkAll.TabIndex = 7
        Me.btnMarkAll.Text = "Mark All"
        Me.ToolTip1.SetToolTip(Me.btnMarkAll, "Mark all Emails for sending now.. ")
        Me.btnMarkAll.UseVisualStyleBackColor = False
        '
        'btnInvoiceAllMarked
        '
        Me.btnInvoiceAllMarked.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnInvoiceAllMarked.CausesValidation = False
        Me.btnInvoiceAllMarked.Image = CType(resources.GetObject("btnInvoiceAllMarked.Image"), System.Drawing.Image)
        Me.btnInvoiceAllMarked.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnInvoiceAllMarked.Location = New System.Drawing.Point(101, 9)
        Me.btnInvoiceAllMarked.Name = "btnInvoiceAllMarked"
        Me.btnInvoiceAllMarked.Size = New System.Drawing.Size(92, 59)
        Me.btnInvoiceAllMarked.TabIndex = 6
        Me.btnInvoiceAllMarked.Text = "Invoice All items Marked"
        Me.btnInvoiceAllMarked.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnInvoiceAllMarked.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnInvoiceAllMarked, "Send All Selected Emails..")
        Me.btnInvoiceAllMarked.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label7.Location = New System.Drawing.Point(10, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(178, 22)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Subscription Invoicing"
        '
        'txtLogStatus
        '
        Me.txtLogStatus.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtLogStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLogStatus.CausesValidation = False
        Me.txtLogStatus.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLogStatus.HideSelection = False
        Me.txtLogStatus.Location = New System.Drawing.Point(770, 6)
        Me.txtLogStatus.Multiline = True
        Me.txtLogStatus.Name = "txtLogStatus"
        Me.txtLogStatus.ReadOnly = True
        Me.txtLogStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLogStatus.Size = New System.Drawing.Size(199, 123)
        Me.txtLogStatus.TabIndex = 4
        Me.txtLogStatus.Text = "txtLogStatus"
        '
        'btnRefreshBillGrid
        '
        Me.btnRefreshBillGrid.CausesValidation = False
        Me.btnRefreshBillGrid.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefreshBillGrid.ForeColor = System.Drawing.Color.Indigo
        Me.btnRefreshBillGrid.Location = New System.Drawing.Point(17, 92)
        Me.btnRefreshBillGrid.Name = "btnRefreshBillGrid"
        Me.btnRefreshBillGrid.Size = New System.Drawing.Size(115, 24)
        Me.btnRefreshBillGrid.TabIndex = 5
        Me.btnRefreshBillGrid.Text = "Refresh Grid"
        Me.btnRefreshBillGrid.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(14, 31)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(167, 45)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Shows Customer Subscriptiions that are due to be invoiced  now."
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Indigo
        Me.Label9.Location = New System.Drawing.Point(197, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(217, 118)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = resources.GetString("Label9.Text")
        '
        'dgvInvoices
        '
        Me.dgvInvoices.AllowUserToAddRows = False
        Me.dgvInvoices.AllowUserToDeleteRows = False
        Me.dgvInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvInvoices.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvInvoices.CausesValidation = False
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInvoices.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvInvoices.ColumnHeadersHeight = 32
        Me.dgvInvoices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.sub_id, Me.customer, Me.billing_period, Me.item_list, Me.start_date, Me.total_inc, Me.prev_inv_date, Me.due_date_period, Me.invoice_now, Me.MarkToInvoice, Me.email_invoice, Me.customer_id, Me.customer_email})
        Me.dgvInvoices.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvInvoices.Location = New System.Drawing.Point(3, 148)
        Me.dgvInvoices.MultiSelect = False
        Me.dgvInvoices.Name = "dgvInvoices"
        Me.dgvInvoices.RowHeadersWidth = 21
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvInvoices.RowsDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvInvoices.Size = New System.Drawing.Size(976, 451)
        Me.dgvInvoices.StandardTab = True
        Me.dgvInvoices.TabIndex = 0
        '
        'sub_id
        '
        Me.sub_id.FillWeight = 50.0!
        Me.sub_id.HeaderText = "Sub Id"
        Me.sub_id.Name = "sub_id"
        Me.sub_id.ReadOnly = True
        '
        'customer
        '
        Me.customer.FillWeight = 150.0!
        Me.customer.HeaderText = "Customer"
        Me.customer.Name = "customer"
        Me.customer.ReadOnly = True
        '
        'billing_period
        '
        Me.billing_period.FillWeight = 60.0!
        Me.billing_period.HeaderText = "Billing Period"
        Me.billing_period.Name = "billing_period"
        Me.billing_period.ReadOnly = True
        '
        'item_list
        '
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.item_list.DefaultCellStyle = DataGridViewCellStyle4
        Me.item_list.FillWeight = 150.0!
        Me.item_list.HeaderText = "Billable Item List"
        Me.item_list.Name = "item_list"
        Me.item_list.ReadOnly = True
        '
        'start_date
        '
        Me.start_date.FillWeight = 80.0!
        Me.start_date.HeaderText = "Original Start Date"
        Me.start_date.Name = "start_date"
        Me.start_date.ReadOnly = True
        '
        'total_inc
        '
        Me.total_inc.FillWeight = 80.0!
        Me.total_inc.HeaderText = "Invoice Total"
        Me.total_inc.Name = "total_inc"
        Me.total_inc.ReadOnly = True
        '
        'prev_inv_date
        '
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.prev_inv_date.DefaultCellStyle = DataGridViewCellStyle5
        Me.prev_inv_date.FillWeight = 80.0!
        Me.prev_inv_date.HeaderText = "Prev.Invoice"
        Me.prev_inv_date.Name = "prev_inv_date"
        Me.prev_inv_date.ReadOnly = True
        '
        'due_date_period
        '
        Me.due_date_period.FillWeight = 80.0!
        Me.due_date_period.HeaderText = "Due Date- Period"
        Me.due_date_period.Name = "due_date_period"
        Me.due_date_period.ReadOnly = True
        '
        'invoice_now
        '
        Me.invoice_now.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.Thistle
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.Padding = New System.Windows.Forms.Padding(5)
        Me.invoice_now.DefaultCellStyle = DataGridViewCellStyle6
        Me.invoice_now.FillWeight = 80.0!
        Me.invoice_now.HeaderText = "Invoice Now"
        Me.invoice_now.Name = "invoice_now"
        Me.invoice_now.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.invoice_now.Text = "Invoice Now"
        Me.invoice_now.ToolTipText = "Create and send invoice for due Period.."
        Me.invoice_now.UseColumnTextForButtonValue = True
        Me.invoice_now.Width = 70
        '
        'MarkToInvoice
        '
        Me.MarkToInvoice.FillWeight = 70.0!
        Me.MarkToInvoice.HeaderText = "Mark To Invoice"
        Me.MarkToInvoice.Name = "MarkToInvoice"
        '
        'email_invoice
        '
        Me.email_invoice.FillWeight = 70.0!
        Me.email_invoice.HeaderText = "Email Invoice"
        Me.email_invoice.Name = "email_invoice"
        '
        'customer_id
        '
        Me.customer_id.FillWeight = 50.0!
        Me.customer_id.HeaderText = "customer_id"
        Me.customer_id.Name = "customer_id"
        Me.customer_id.ReadOnly = True
        Me.customer_id.Visible = False
        '
        'customer_email
        '
        Me.customer_email.HeaderText = "Email"
        Me.customer_email.Name = "customer_email"
        Me.customer_email.ReadOnly = True
        Me.customer_email.Visible = False
        '
        'TabPageAnalysis
        '
        Me.TabPageAnalysis.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.TabPageAnalysis.Controls.Add(Me.panelPeriodReport)
        Me.TabPageAnalysis.Controls.Add(Me.panelProducts)
        Me.TabPageAnalysis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageAnalysis.Location = New System.Drawing.Point(4, 25)
        Me.TabPageAnalysis.Name = "TabPageAnalysis"
        Me.TabPageAnalysis.Size = New System.Drawing.Size(998, 620)
        Me.TabPageAnalysis.TabIndex = 2
        Me.TabPageAnalysis.Text = "Product Analysis"
        '
        'panelPeriodReport
        '
        Me.panelPeriodReport.BackColor = System.Drawing.Color.White
        Me.panelPeriodReport.Controls.Add(Me.dgvPeriodReport)
        Me.panelPeriodReport.Controls.Add(Me.Label18)
        Me.panelPeriodReport.Location = New System.Drawing.Point(402, 6)
        Me.panelPeriodReport.Name = "panelPeriodReport"
        Me.panelPeriodReport.Size = New System.Drawing.Size(583, 599)
        Me.panelPeriodReport.TabIndex = 3
        '
        'dgvPeriodReport
        '
        Me.dgvPeriodReport.AllowUserToAddRows = False
        Me.dgvPeriodReport.AllowUserToDeleteRows = False
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Gainsboro
        Me.dgvPeriodReport.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvPeriodReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPeriodReport.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPeriodReport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.dgvPeriodReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPeriodReport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.billingPeriod, Me.stock_barcode, Me.stock_description, Me.total_qty, Me.totalSellActual_inc})
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPeriodReport.DefaultCellStyle = DataGridViewCellStyle12
        Me.dgvPeriodReport.Location = New System.Drawing.Point(9, 52)
        Me.dgvPeriodReport.MultiSelect = False
        Me.dgvPeriodReport.Name = "dgvPeriodReport"
        Me.dgvPeriodReport.ReadOnly = True
        Me.dgvPeriodReport.RowHeadersWidth = 20
        Me.dgvPeriodReport.Size = New System.Drawing.Size(567, 535)
        Me.dgvPeriodReport.TabIndex = 3
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(30, 10)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(249, 40)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "Projected Income " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    by Subscription Period and Product"
        '
        'panelProducts
        '
        Me.panelProducts.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.panelProducts.Controls.Add(Me.btnRefreshAnalysis)
        Me.panelProducts.Controls.Add(Me.Label13)
        Me.panelProducts.Controls.Add(Me.clsPosTreeViewProducts)
        Me.panelProducts.Location = New System.Drawing.Point(5, 6)
        Me.panelProducts.Name = "panelProducts"
        Me.panelProducts.Size = New System.Drawing.Size(391, 599)
        Me.panelProducts.TabIndex = 2
        '
        'btnRefreshAnalysis
        '
        Me.btnRefreshAnalysis.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRefreshAnalysis.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefreshAnalysis.Location = New System.Drawing.Point(226, 8)
        Me.btnRefreshAnalysis.Name = "btnRefreshAnalysis"
        Me.btnRefreshAnalysis.Size = New System.Drawing.Size(72, 36)
        Me.btnRefreshAnalysis.TabIndex = 2
        Me.btnRefreshAnalysis.Text = "Refresh Analysis"
        Me.btnRefreshAnalysis.UseVisualStyleBackColor = False
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(13, 8)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(187, 40)
        Me.Label13.TabIndex = 1
        Me.Label13.Text = "Subscribed Products-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "     with Sub and Customer.."
        '
        'clsPosTreeViewProducts
        '
        Me.clsPosTreeViewProducts.BackColor = System.Drawing.Color.WhiteSmoke
        Me.clsPosTreeViewProducts.Font = New System.Drawing.Font("Lucida Console", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clsPosTreeViewProducts.Location = New System.Drawing.Point(8, 52)
        Me.clsPosTreeViewProducts.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.clsPosTreeViewProducts.Name = "clsPosTreeViewProducts"
        TreeNode1.Name = "nodeRoot"
        TreeNode1.NodeFont = New System.Drawing.Font("Lucida Console", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TreeNode1.Text = "Subs Products"
        Me.clsPosTreeViewProducts.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.clsPosTreeViewProducts.Size = New System.Drawing.Size(370, 535)
        Me.clsPosTreeViewProducts.TabIndex = 0
        '
        'billingPeriod
        '
        Me.billingPeriod.FillWeight = 70.0!
        Me.billingPeriod.HeaderText = "billingPeriod"
        Me.billingPeriod.Name = "billingPeriod"
        Me.billingPeriod.ReadOnly = True
        Me.billingPeriod.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'stock_barcode
        '
        Me.stock_barcode.FillWeight = 120.0!
        Me.stock_barcode.HeaderText = "stock_barcode"
        Me.stock_barcode.Name = "stock_barcode"
        Me.stock_barcode.ReadOnly = True
        Me.stock_barcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'stock_description
        '
        Me.stock_description.FillWeight = 200.0!
        Me.stock_description.HeaderText = "stock_description"
        Me.stock_description.Name = "stock_description"
        Me.stock_description.ReadOnly = True
        Me.stock_description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'total_qty
        '
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle10.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.total_qty.DefaultCellStyle = DataGridViewCellStyle10
        Me.total_qty.FillWeight = 80.0!
        Me.total_qty.HeaderText = "total_qty"
        Me.total_qty.Name = "total_qty"
        Me.total_qty.ReadOnly = True
        Me.total_qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'totalSellActual_inc
        '
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle11.Padding = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.totalSellActual_inc.DefaultCellStyle = DataGridViewCellStyle11
        Me.totalSellActual_inc.HeaderText = "totalSellActual_inc"
        Me.totalSellActual_inc.Name = "totalSellActual_inc"
        Me.totalSellActual_inc.ReadOnly = True
        Me.totalSellActual_inc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ucChildSubscription
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildSubscription"
        Me.Size = New System.Drawing.Size(1017, 658)
        Me.panelSubsBanner.ResumeLayout(False)
        Me.grpBoxSubsLookup.ResumeLayout(False)
        Me.grpBoxSub.ResumeLayout(False)
        Me.grpBoxSubDetail.ResumeLayout(False)
        Me.grpBoxSubDetail.PerformLayout()
        Me.frameBrowse.ResumeLayout(False)
        Me.frameBrowse.PerformLayout()
        CType(Me.dgvSubsList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageSubs.ResumeLayout(False)
        Me.TabPageInvoicing.ResumeLayout(False)
        Me.grpBoxBilling.ResumeLayout(False)
        Me.panelBillingHdr.ResumeLayout(False)
        Me.panelBillingHdr.PerformLayout()
        Me.panelGroupAction.ResumeLayout(False)
        CType(Me.dgvInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageAnalysis.ResumeLayout(False)
        Me.panelPeriodReport.ResumeLayout(False)
        CType(Me.dgvPeriodReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelProducts.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents panelSubsBanner As System.Windows.Forms.Panel
    Friend WithEvents labHdrGR As System.Windows.Forms.Label
    Friend WithEvents grpBoxSubsLookup As System.Windows.Forms.GroupBox
    Public WithEvents frameBrowse As System.Windows.Forms.GroupBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents cmdClearSubsSearch As System.Windows.Forms.Button
    Public WithEvents cmdSubsSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtSubsSearch As System.Windows.Forms.TextBox
    Friend WithEvents dgvSubsList As System.Windows.Forms.DataGridView
    Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents labRecCount As System.Windows.Forms.Label
    Public WithEvents LabFind As System.Windows.Forms.Label
    Friend WithEvents grpBoxSub As System.Windows.Forms.GroupBox
    Friend WithEvents labShowTotal As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents listViewSubsItems As System.Windows.Forms.ListView
    Friend WithEvents ItemNo As System.Windows.Forms.ColumnHeader
    Friend WithEvents ItemBarcode As System.Windows.Forms.ColumnHeader
    Friend WithEvents Cat1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Description As System.Windows.Forms.ColumnHeader
    Friend WithEvents Qty As System.Windows.Forms.ColumnHeader
    Friend WithEvents Price As System.Windows.Forms.ColumnHeader
    Friend WithEvents Amount As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageSubs As System.Windows.Forms.TabPage
    Friend WithEvents TabPageInvoicing As System.Windows.Forms.TabPage
    Friend WithEvents labShowPeriod As System.Windows.Forms.Label
    Friend WithEvents labSubsId As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents labShowActivated As System.Windows.Forms.Label
    Friend WithEvents btnCancelEdit As System.Windows.Forms.Button
    Friend WithEvents btnSaveEdit As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnDeleteItem As System.Windows.Forms.Button
    Friend WithEvents btnAddItem As System.Windows.Forms.Button
    Friend WithEvents btnNewSub As System.Windows.Forms.Button
    Friend WithEvents btnCancelSub As System.Windows.Forms.Button
    Friend WithEvents btnEditSub As System.Windows.Forms.Button
    Friend WithEvents grpBoxSubDetail As System.Windows.Forms.GroupBox
    Friend WithEvents labCustomerLab As System.Windows.Forms.Label
    Friend WithEvents labSubCustomerName As System.Windows.Forms.Label
    Friend WithEvents txtSubCustBarcode As System.Windows.Forms.TextBox
    Friend WithEvents dtPickerStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboBillingCycle As System.Windows.Forms.ComboBox
    Friend WithEvents stock_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkActivateNow As System.Windows.Forms.CheckBox
    Friend WithEvents labInfoStarted As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents labInvoiceCount As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents listSubInvoices As System.Windows.Forms.ListBox
    Friend WithEvents dgvInvoices As System.Windows.Forms.DataGridView
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtLogStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents labLastPeriodBilled As System.Windows.Forms.Label
    Friend WithEvents labAction As System.Windows.Forms.Label
    Friend WithEvents btnRefreshBillGrid As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents grpBoxBilling As System.Windows.Forms.GroupBox
    Friend WithEvents panelBillingHdr As System.Windows.Forms.Panel
    Friend WithEvents TaxCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents dtPickerEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkEndDate As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnPauseInvoicing As System.Windows.Forms.Button
    Friend WithEvents panelGroupAction As System.Windows.Forms.Panel
    Friend WithEvents btnUnMarkAll As System.Windows.Forms.Button
    Friend WithEvents btnMarkAll As System.Windows.Forms.Button
    Friend WithEvents btnInvoiceAllMarked As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents labPdfPrinter As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents chkOkToEmailInvoices As System.Windows.Forms.CheckBox
    Friend WithEvents sub_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents billing_period As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents item_list As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents start_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents total_inc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prev_inv_date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents due_date_period As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents invoice_now As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents MarkToInvoice As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents email_invoice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer_id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents customer_email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPageAnalysis As System.Windows.Forms.TabPage
    Friend WithEvents panelPeriodReport As System.Windows.Forms.Panel
    Friend WithEvents dgvPeriodReport As System.Windows.Forms.DataGridView
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents panelProducts As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents clsPosTreeViewProducts As JMxPOS330.clsPosTreeView
    Friend WithEvents btnRefreshAnalysis As System.Windows.Forms.Button
    Friend WithEvents billingPeriod As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents stock_barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents stock_description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents total_qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents totalSellActual_inc As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
