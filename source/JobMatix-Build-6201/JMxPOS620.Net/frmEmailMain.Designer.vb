<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmailMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmailMain))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.labVersion = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabBusiness = New System.Windows.Forms.Label()
        Me.LabToday = New System.Windows.Forms.Label()
        Me.LabServer = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtStaff = New System.Windows.Forms.TextBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtQueuePath = New System.Windows.Forms.TextBox()
        Me.panelEmailHdr = New System.Windows.Forms.Panel()
        Me.labSMTPHost = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.panelEmailGrid = New System.Windows.Forms.Panel()
        Me.txtReport = New System.Windows.Forms.TextBox()
        Me.chkDeleteWhenSent = New System.Windows.Forms.CheckBox()
        Me.btnUnselectAll = New System.Windows.Forms.Button()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.btnSendAllMarked = New System.Windows.Forms.Button()
        Me.btnPauseSending = New System.Windows.Forms.Button()
        Me.chkConfirmSends = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.labEmailTimer = New System.Windows.Forms.Label()
        Me.dgvEmailList = New System.Windows.Forms.DataGridView()
        Me.doc_email_target = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.doc_date_created = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.doc_subject = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.doc_email_text = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.delete_email = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.view_doc = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.doc_send = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.doc_markToSend = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.doc_file_title = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.xml_file_path = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.doc_recipient = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCancelSend = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.grpBoxMain = New System.Windows.Forms.GroupBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelEmailHdr.SuspendLayout()
        Me.panelEmailGrid.SuspendLayout()
        CType(Me.dgvEmailList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'labVersion
        '
        Me.labVersion.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labVersion.Location = New System.Drawing.Point(698, 686)
        Me.labVersion.Name = "labVersion"
        Me.labVersion.Size = New System.Drawing.Size(172, 11)
        Me.labVersion.TabIndex = 109
        Me.labVersion.Text = "labVersion"
        Me.labVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 6.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(495, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(168, 21)
        Me.Label7.TabIndex = 111
        Me.Label7.Text = "Sql Server (POS Database):"
        '
        'LabBusiness
        '
        Me.LabBusiness.BackColor = System.Drawing.Color.Transparent
        Me.LabBusiness.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabBusiness.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabBusiness.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabBusiness.Location = New System.Drawing.Point(13, 59)
        Me.LabBusiness.Name = "LabBusiness"
        Me.LabBusiness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabBusiness.Size = New System.Drawing.Size(194, 17)
        Me.LabBusiness.TabIndex = 113
        Me.LabBusiness.Text = "labBusiness"
        '
        'LabToday
        '
        Me.LabToday.BackColor = System.Drawing.Color.Transparent
        Me.LabToday.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabToday.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabToday.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabToday.Location = New System.Drawing.Point(211, 8)
        Me.LabToday.Name = "LabToday"
        Me.LabToday.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabToday.Size = New System.Drawing.Size(148, 17)
        Me.LabToday.TabIndex = 110
        Me.LabToday.Text = "LabToday"
        Me.LabToday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabServer
        '
        Me.LabServer.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabServer.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabServer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabServer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabServer.Location = New System.Drawing.Point(494, 56)
        Me.LabServer.Name = "LabServer"
        Me.LabServer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabServer.Size = New System.Drawing.Size(244, 37)
        Me.LabServer.TabIndex = 112
        Me.LabServer.Text = "LabServer"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(10, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(42, 17)
        Me.Label3.TabIndex = 114
        Me.Label3.Text = "Staff:"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label1.Location = New System.Drawing.Point(18, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 47)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "JobMatix POS Email Agent"
        '
        'txtStaff
        '
        Me.txtStaff.AcceptsReturn = True
        Me.txtStaff.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtStaff.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStaff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStaff.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStaff.Location = New System.Drawing.Point(62, 83)
        Me.txtStaff.MaxLength = 0
        Me.txtStaff.Name = "txtStaff"
        Me.txtStaff.ReadOnly = True
        Me.txtStaff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStaff.Size = New System.Drawing.Size(89, 14)
        Me.txtStaff.TabIndex = 115
        Me.txtStaff.TabStop = False
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(929, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(52, 26)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label5.Location = New System.Drawing.Point(211, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(118, 17)
        Me.Label5.TabIndex = 116
        Me.Label5.Text = "Email Queue Path:"
        '
        'txtQueuePath
        '
        Me.txtQueuePath.AcceptsReturn = True
        Me.txtQueuePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtQueuePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtQueuePath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQueuePath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtQueuePath.Location = New System.Drawing.Point(214, 50)
        Me.txtQueuePath.MaxLength = 0
        Me.txtQueuePath.Multiline = True
        Me.txtQueuePath.Name = "txtQueuePath"
        Me.txtQueuePath.ReadOnly = True
        Me.txtQueuePath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQueuePath.Size = New System.Drawing.Size(267, 43)
        Me.txtQueuePath.TabIndex = 117
        Me.txtQueuePath.TabStop = False
        '
        'panelEmailHdr
        '
        Me.panelEmailHdr.Controls.Add(Me.txtQueuePath)
        Me.panelEmailHdr.Controls.Add(Me.Label5)
        Me.panelEmailHdr.Controls.Add(Me.cmdClose)
        Me.panelEmailHdr.Controls.Add(Me.txtStaff)
        Me.panelEmailHdr.Controls.Add(Me.Label1)
        Me.panelEmailHdr.Controls.Add(Me.Label3)
        Me.panelEmailHdr.Controls.Add(Me.labSMTPHost)
        Me.panelEmailHdr.Controls.Add(Me.Label2)
        Me.panelEmailHdr.Controls.Add(Me.LabServer)
        Me.panelEmailHdr.Controls.Add(Me.LabToday)
        Me.panelEmailHdr.Controls.Add(Me.LabBusiness)
        Me.panelEmailHdr.Controls.Add(Me.Label7)
        Me.panelEmailHdr.Location = New System.Drawing.Point(6, 0)
        Me.panelEmailHdr.Name = "panelEmailHdr"
        Me.panelEmailHdr.Size = New System.Drawing.Size(991, 106)
        Me.panelEmailHdr.TabIndex = 0
        '
        'labSMTPHost
        '
        Me.labSMTPHost.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labSMTPHost.Location = New System.Drawing.Point(757, 56)
        Me.labSMTPHost.Name = "labSMTPHost"
        Me.labSMTPHost.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labSMTPHost.Size = New System.Drawing.Size(223, 37)
        Me.labSMTPHost.TabIndex = 0
        Me.labSMTPHost.Text = "labSMTPHost"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(760, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "SMTP (Mail) Host"
        '
        'panelEmailGrid
        '
        Me.panelEmailGrid.BackColor = System.Drawing.Color.Lavender
        Me.panelEmailGrid.Controls.Add(Me.txtReport)
        Me.panelEmailGrid.Controls.Add(Me.chkDeleteWhenSent)
        Me.panelEmailGrid.Controls.Add(Me.btnUnselectAll)
        Me.panelEmailGrid.Controls.Add(Me.btnSelectAll)
        Me.panelEmailGrid.Controls.Add(Me.btnSendAllMarked)
        Me.panelEmailGrid.Controls.Add(Me.btnPauseSending)
        Me.panelEmailGrid.Controls.Add(Me.chkConfirmSends)
        Me.panelEmailGrid.Controls.Add(Me.Label4)
        Me.panelEmailGrid.Controls.Add(Me.labEmailTimer)
        Me.panelEmailGrid.Controls.Add(Me.dgvEmailList)
        Me.panelEmailGrid.Controls.Add(Me.btnCancelSend)
        Me.panelEmailGrid.Controls.Add(Me.btnRefresh)
        Me.panelEmailGrid.Controls.Add(Me.labStatus)
        Me.panelEmailGrid.Location = New System.Drawing.Point(5, 12)
        Me.panelEmailGrid.Name = "panelEmailGrid"
        Me.panelEmailGrid.Size = New System.Drawing.Size(978, 561)
        Me.panelEmailGrid.TabIndex = 0
        '
        'txtReport
        '
        Me.txtReport.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtReport.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtReport.Location = New System.Drawing.Point(745, 5)
        Me.txtReport.Multiline = True
        Me.txtReport.Name = "txtReport"
        Me.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtReport.Size = New System.Drawing.Size(221, 98)
        Me.txtReport.TabIndex = 8
        Me.txtReport.Text = "txtReport"
        '
        'chkDeleteWhenSent
        '
        Me.chkDeleteWhenSent.Checked = True
        Me.chkDeleteWhenSent.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDeleteWhenSent.Location = New System.Drawing.Point(255, 53)
        Me.chkDeleteWhenSent.Name = "chkDeleteWhenSent"
        Me.chkDeleteWhenSent.Size = New System.Drawing.Size(119, 45)
        Me.chkDeleteWhenSent.TabIndex = 2
        Me.chkDeleteWhenSent.Text = "Drop from Queue when Sent (without asking).."
        Me.chkDeleteWhenSent.UseVisualStyleBackColor = True
        '
        'btnUnselectAll
        '
        Me.btnUnselectAll.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnUnselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUnselectAll.Location = New System.Drawing.Point(404, 75)
        Me.btnUnselectAll.Name = "btnUnselectAll"
        Me.btnUnselectAll.Size = New System.Drawing.Size(89, 27)
        Me.btnUnselectAll.TabIndex = 4
        Me.btnUnselectAll.Text = "Un-Mark All"
        Me.btnUnselectAll.UseVisualStyleBackColor = False
        '
        'btnSelectAll
        '
        Me.btnSelectAll.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectAll.Location = New System.Drawing.Point(404, 42)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(89, 27)
        Me.btnSelectAll.TabIndex = 3
        Me.btnSelectAll.Text = "Mark All"
        Me.ToolTip1.SetToolTip(Me.btnSelectAll, "Mark all Emails for sending now.. ")
        Me.btnSelectAll.UseVisualStyleBackColor = False
        '
        'btnSendAllMarked
        '
        Me.btnSendAllMarked.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSendAllMarked.Image = CType(resources.GetObject("btnSendAllMarked.Image"), System.Drawing.Image)
        Me.btnSendAllMarked.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSendAllMarked.Location = New System.Drawing.Point(515, 64)
        Me.btnSendAllMarked.Name = "btnSendAllMarked"
        Me.btnSendAllMarked.Size = New System.Drawing.Size(103, 38)
        Me.btnSendAllMarked.TabIndex = 5
        Me.btnSendAllMarked.Text = "Send All items Marked"
        Me.btnSendAllMarked.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnSendAllMarked, "Send All Selected Emails..")
        Me.btnSendAllMarked.UseVisualStyleBackColor = False
        '
        'btnPauseSending
        '
        Me.btnPauseSending.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPauseSending.Image = CType(resources.GetObject("btnPauseSending.Image"), System.Drawing.Image)
        Me.btnPauseSending.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPauseSending.Location = New System.Drawing.Point(647, 64)
        Me.btnPauseSending.Name = "btnPauseSending"
        Me.btnPauseSending.Size = New System.Drawing.Size(78, 38)
        Me.btnPauseSending.TabIndex = 7
        Me.btnPauseSending.Text = "Pause   Sending"
        Me.btnPauseSending.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnPauseSending, "Pause sending Emails..")
        Me.btnPauseSending.UseVisualStyleBackColor = False
        '
        'chkConfirmSends
        '
        Me.chkConfirmSends.Location = New System.Drawing.Point(255, 12)
        Me.chkConfirmSends.Name = "chkConfirmSends"
        Me.chkConfirmSends.Size = New System.Drawing.Size(119, 35)
        Me.chkConfirmSends.TabIndex = 1
        Me.chkConfirmSends.Text = "Confirm All Manual Send Requests"
        Me.chkConfirmSends.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(188, 60)
        Me.Label4.TabIndex = 129
        Me.Label4.Text = "DataGrid shows all Emails and Attachments (eg Invoice pdf's) in the Queue for sen" & _
    "ding to Customers.."
        '
        'labEmailTimer
        '
        Me.labEmailTimer.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labEmailTimer.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labEmailTimer.ForeColor = System.Drawing.Color.Blue
        Me.labEmailTimer.Location = New System.Drawing.Point(519, 28)
        Me.labEmailTimer.Name = "labEmailTimer"
        Me.labEmailTimer.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labEmailTimer.Size = New System.Drawing.Size(99, 24)
        Me.labEmailTimer.TabIndex = 128
        Me.labEmailTimer.Text = "labEmailTimer"
        '
        'dgvEmailList
        '
        Me.dgvEmailList.AllowUserToAddRows = False
        Me.dgvEmailList.AllowUserToDeleteRows = False
        Me.dgvEmailList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvEmailList.BackgroundColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvEmailList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvEmailList.ColumnHeadersHeight = 33
        Me.dgvEmailList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.doc_email_target, Me.doc_date_created, Me.doc_subject, Me.doc_email_text, Me.delete_email, Me.view_doc, Me.doc_send, Me.doc_markToSend, Me.doc_file_title, Me.xml_file_path, Me.doc_recipient})
        Me.dgvEmailList.Location = New System.Drawing.Point(3, 110)
        Me.dgvEmailList.MultiSelect = False
        Me.dgvEmailList.Name = "dgvEmailList"
        Me.dgvEmailList.RowHeadersWidth = 20
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvEmailList.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvEmailList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvEmailList.Size = New System.Drawing.Size(972, 441)
        Me.dgvEmailList.StandardTab = True
        Me.dgvEmailList.TabIndex = 3
        '
        'doc_email_target
        '
        Me.doc_email_target.HeaderText = "Email Target"
        Me.doc_email_target.Name = "doc_email_target"
        '
        'doc_date_created
        '
        Me.doc_date_created.HeaderText = "Date Created"
        Me.doc_date_created.Name = "doc_date_created"
        '
        'doc_subject
        '
        Me.doc_subject.FillWeight = 150.0!
        Me.doc_subject.HeaderText = "Subject"
        Me.doc_subject.Name = "doc_subject"
        '
        'doc_email_text
        '
        Me.doc_email_text.FillWeight = 200.0!
        Me.doc_email_text.HeaderText = "Email Text"
        Me.doc_email_text.Name = "doc_email_text"
        '
        'delete_email
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(5, 11, 5, 11)
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.delete_email.DefaultCellStyle = DataGridViewCellStyle2
        Me.delete_email.FillWeight = 70.0!
        Me.delete_email.HeaderText = "Delete Email"
        Me.delete_email.Name = "delete_email"
        Me.delete_email.Text = "X Delete Email"
        Me.delete_email.UseColumnTextForButtonValue = True
        '
        'view_doc
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(5, 11, 5, 11)
        Me.view_doc.DefaultCellStyle = DataGridViewCellStyle3
        Me.view_doc.FillWeight = 70.0!
        Me.view_doc.HeaderText = "View pdf"
        Me.view_doc.Name = "view_doc"
        Me.view_doc.Text = "View pdf"
        Me.view_doc.UseColumnTextForButtonValue = True
        '
        'doc_send
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Padding = New System.Windows.Forms.Padding(5, 11, 5, 11)
        Me.doc_send.DefaultCellStyle = DataGridViewCellStyle4
        Me.doc_send.FillWeight = 70.0!
        Me.doc_send.HeaderText = "Send"
        Me.doc_send.Name = "doc_send"
        Me.doc_send.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.doc_send.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.doc_send.Text = "Send This Email"
        Me.doc_send.UseColumnTextForButtonValue = True
        '
        'doc_markToSend
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.NullValue = False
        DataGridViewCellStyle5.Padding = New System.Windows.Forms.Padding(5)
        Me.doc_markToSend.DefaultCellStyle = DataGridViewCellStyle5
        Me.doc_markToSend.FillWeight = 70.0!
        Me.doc_markToSend.HeaderText = "Mark to Send"
        Me.doc_markToSend.Name = "doc_markToSend"
        '
        'doc_file_title
        '
        Me.doc_file_title.HeaderText = "doc_file_title"
        Me.doc_file_title.Name = "doc_file_title"
        Me.doc_file_title.Visible = False
        '
        'xml_file_path
        '
        Me.xml_file_path.HeaderText = "xml_file_path"
        Me.xml_file_path.Name = "xml_file_path"
        Me.xml_file_path.Visible = False
        '
        'doc_recipient
        '
        Me.doc_recipient.HeaderText = "Recipient"
        Me.doc_recipient.Name = "doc_recipient"
        Me.doc_recipient.Visible = False
        '
        'btnCancelSend
        '
        Me.btnCancelSend.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancelSend.Image = CType(resources.GetObject("btnCancelSend.Image"), System.Drawing.Image)
        Me.btnCancelSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelSend.Location = New System.Drawing.Point(647, 14)
        Me.btnCancelSend.Name = "btnCancelSend"
        Me.btnCancelSend.Size = New System.Drawing.Size(78, 38)
        Me.btnCancelSend.TabIndex = 6
        Me.btnCancelSend.Text = "Cancel  Sending"
        Me.btnCancelSend.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnCancelSend, "Cancel current Send..")
        Me.btnCancelSend.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRefresh.Location = New System.Drawing.Point(17, 73)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(142, 29)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "Refresh Email List"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'labStatus
        '
        Me.labStatus.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.labStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labStatus.Location = New System.Drawing.Point(406, 5)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.labStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labStatus.Size = New System.Drawing.Size(96, 18)
        Me.labStatus.TabIndex = 118
        Me.labStatus.Text = "labStatus"
        '
        'grpBoxMain
        '
        Me.grpBoxMain.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.grpBoxMain.Controls.Add(Me.panelEmailGrid)
        Me.grpBoxMain.Location = New System.Drawing.Point(6, 106)
        Me.grpBoxMain.Name = "grpBoxMain"
        Me.grpBoxMain.Size = New System.Drawing.Size(991, 583)
        Me.grpBoxMain.TabIndex = 0
        Me.grpBoxMain.TabStop = False
        Me.grpBoxMain.Text = "grpBoxMain"
        '
        'frmEmailMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1001, 700)
        Me.Controls.Add(Me.panelEmailHdr)
        Me.Controls.Add(Me.labVersion)
        Me.Controls.Add(Me.grpBoxMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmEmailMain"
        Me.Text = "frmEmailMain"
        Me.panelEmailHdr.ResumeLayout(False)
        Me.panelEmailHdr.PerformLayout()
        Me.panelEmailGrid.ResumeLayout(False)
        Me.panelEmailGrid.PerformLayout()
        CType(Me.dgvEmailList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents labVersion As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents LabBusiness As System.Windows.Forms.Label
    Public WithEvents LabToday As System.Windows.Forms.Label
    Public WithEvents LabServer As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtStaff As System.Windows.Forms.TextBox
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtQueuePath As System.Windows.Forms.TextBox
    Friend WithEvents panelEmailHdr As System.Windows.Forms.Panel
    Friend WithEvents panelEmailGrid As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents labEmailTimer As System.Windows.Forms.Label
    Friend WithEvents dgvEmailList As System.Windows.Forms.DataGridView
    Friend WithEvents btnCancelSend As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents labSMTPHost As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents labStatus As System.Windows.Forms.Label
    Friend WithEvents grpBoxMain As System.Windows.Forms.GroupBox
    Friend WithEvents chkConfirmSends As System.Windows.Forms.CheckBox
    Friend WithEvents btnSendAllMarked As System.Windows.Forms.Button
    Friend WithEvents btnPauseSending As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnUnselectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents chkDeleteWhenSent As System.Windows.Forms.CheckBox
    Friend WithEvents doc_email_target As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents doc_date_created As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents doc_subject As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents doc_email_text As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents delete_email As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents view_doc As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents doc_send As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents doc_markToSend As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents doc_file_title As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents xml_file_path As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents doc_recipient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtReport As System.Windows.Forms.TextBox
End Class
