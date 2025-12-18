<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmNewJob32
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
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents _chkExtras_3 As System.Windows.Forms.CheckBox
    Public WithEvents _chkExtras_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkExtras_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkExtras_0 As System.Windows.Forms.CheckBox
    Public WithEvents txtGoodsOther As System.Windows.Forms.TextBox
    Public WithEvents cboPriority As System.Windows.Forms.ComboBox
    Public WithEvents cmdCheckGoods As System.Windows.Forms.Button
    Public WithEvents txtExtrasInCare As System.Windows.Forms.TextBox
    Public WithEvents LabPriority As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents LabOtherGoods As System.Windows.Forms.Label
    Public WithEvents labGoodsHdr As System.Windows.Forms.Label
    Public WithEvents FrameGoods As System.Windows.Forms.GroupBox
    Public WithEvents _SSTabNewJob_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents _OptLogin_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptLogin_0 As System.Windows.Forms.RadioButton
    Public WithEvents _txtPassword_2 As System.Windows.Forms.TextBox
    Public WithEvents _txtUserName_2 As System.Windows.Forms.TextBox
    Public WithEvents _txtPassword_1 As System.Windows.Forms.TextBox
    Public WithEvents _txtUserName_1 As System.Windows.Forms.TextBox
    Public WithEvents _txtPassword_0 As System.Windows.Forms.TextBox
    Public WithEvents _txtUserName_0 As System.Windows.Forms.TextBox
    Public WithEvents ChkUsers As System.Windows.Forms.CheckBox
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Line1 As System.Windows.Forms.Label
    Public WithEvents FrameUsers As System.Windows.Forms.GroupBox
    Public WithEvents ChkBackupReq As System.Windows.Forms.CheckBox
    Public WithEvents ChkRecovDisk As System.Windows.Forms.CheckBox
    Public WithEvents _optQuotation_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optQuotation_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optQuotation_2 As System.Windows.Forms.RadioButton
    Public WithEvents LabInstructions As System.Windows.Forms.Label
    Public WithEvents Line2 As System.Windows.Forms.Label
    Public WithEvents LabCostLimit As System.Windows.Forms.Label
    Public WithEvents frameInstructions As System.Windows.Forms.GroupBox
    Public WithEvents _SSTabNewJob_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents SSTabNewJob As System.Windows.Forms.TabControl
    Public WithEvents chkRefreshCustomer As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrtDocs_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrtDocs_0 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrtDocs_1 As System.Windows.Forms.CheckBox
    Public WithEvents LabPrintDocs As System.Windows.Forms.Label
    Public WithEvents FramePrintOpts As System.Windows.Forms.Panel
    Public WithEvents cmdPrintAll As System.Windows.Forms.Button
    Public WithEvents picUserLogo As System.Windows.Forms.PictureBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdCancelJob As System.Windows.Forms.Button
    Public WithEvents cmdFinish As System.Windows.Forms.Button
    Public WithEvents txtRcvdName As System.Windows.Forms.TextBox
    Public WithEvents LabHelpStatus As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LabJobStatus As System.Windows.Forms.Label
    Public WithEvents LabVersion As System.Windows.Forms.Label
    Public WithEvents LabDateRcvd As System.Windows.Forms.Label
    Public WithEvents LabRcvdBy As System.Windows.Forms.Label
    Public WithEvents LabTicket As System.Windows.Forms.Label
    Public WithEvents LabHdr2 As System.Windows.Forms.Label
    Public WithEvents LabHdr1 As System.Windows.Forms.Label
    Public WithEvents OptLogin As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents chkExtras As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    Public WithEvents chkPrtDocs As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    Public WithEvents optQuotation As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtPassword As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    Public WithEvents txtUserName As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewJob32))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdCheckGoods = New System.Windows.Forms.Button()
        Me._optQuotation_0 = New System.Windows.Forms.RadioButton()
        Me._optQuotation_1 = New System.Windows.Forms.RadioButton()
        Me._optQuotation_2 = New System.Windows.Forms.RadioButton()
        Me.cmdCancelJob = New System.Windows.Forms.Button()
        Me.cmdEditSymptoms = New System.Windows.Forms.Button()
        Me.cmdGoodsClear = New System.Windows.Forms.Button()
        Me.FramePrintOpts = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboLabelPrinters = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboReceiptPrinters = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cboColourPrinters = New System.Windows.Forms.ComboBox()
        Me.NumUpDownLabels = New System.Windows.Forms.NumericUpDown()
        Me._chkPrtDocs_2 = New System.Windows.Forms.CheckBox()
        Me._chkPrtDocs_0 = New System.Windows.Forms.CheckBox()
        Me._chkPrtDocs_1 = New System.Windows.Forms.CheckBox()
        Me.LabPrintDocs = New System.Windows.Forms.Label()
        Me.picSubjectItem = New System.Windows.Forms.PictureBox()
        Me.numUpDownOnSiteDuration = New System.Windows.Forms.NumericUpDown()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.SSTabNewJob = New System.Windows.Forms.TabControl()
        Me._SSTabNewJob_TabPage0 = New System.Windows.Forms.TabPage()
        Me.FrameGoods = New System.Windows.Forms.GroupBox()
        Me.chkReturned = New System.Windows.Forms.CheckBox()
        Me.chkSystemUnderWarranty = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.labGoodsInfoRequired = New System.Windows.Forms.Label()
        Me.txtGoodsList = New System.Windows.Forms.TextBox()
        Me.labSelectPrevious = New System.Windows.Forms.Label()
        Me.cboPrevGoods = New System.Windows.Forms.ComboBox()
        Me._chkExtras_3 = New System.Windows.Forms.CheckBox()
        Me._chkExtras_2 = New System.Windows.Forms.CheckBox()
        Me._chkExtras_1 = New System.Windows.Forms.CheckBox()
        Me._chkExtras_0 = New System.Windows.Forms.CheckBox()
        Me.txtGoodsOther = New System.Windows.Forms.TextBox()
        Me.txtExtrasInCare = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabOtherGoods = New System.Windows.Forms.Label()
        Me.labGoodsHdr = New System.Windows.Forms.Label()
        Me.FrameUsers = New System.Windows.Forms.GroupBox()
        Me.labLogonRequired = New System.Windows.Forms.Label()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me._OptLogin_1 = New System.Windows.Forms.RadioButton()
        Me._OptLogin_0 = New System.Windows.Forms.RadioButton()
        Me._txtPassword_2 = New System.Windows.Forms.TextBox()
        Me._txtUserName_2 = New System.Windows.Forms.TextBox()
        Me._txtPassword_1 = New System.Windows.Forms.TextBox()
        Me._txtUserName_1 = New System.Windows.Forms.TextBox()
        Me._txtPassword_0 = New System.Windows.Forms.TextBox()
        Me._txtUserName_0 = New System.Windows.Forms.TextBox()
        Me.ChkUsers = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Line1 = New System.Windows.Forms.Label()
        Me.grpBoxItemPic = New System.Windows.Forms.GroupBox()
        Me._SSTabNewJob_TabPage1 = New System.Windows.Forms.TabPage()
        Me.frameProblem = New System.Windows.Forms.GroupBox()
        Me.cmdNext2 = New System.Windows.Forms.Button()
        Me.cmdNavBackToStart = New System.Windows.Forms.Button()
        Me.labProblemRequired = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.listSymptoms = New System.Windows.Forms.CheckedListBox()
        Me.txtProblem = New System.Windows.Forms.TextBox()
        Me.txtSymptoms = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.LabProblem = New System.Windows.Forms.Label()
        Me._SSTabNewJob_TabPage2 = New System.Windows.Forms.TabPage()
        Me.frameInstructions = New System.Windows.Forms.GroupBox()
        Me.labOnSiteDurationWarning = New System.Windows.Forms.Label()
        Me.labOnSiteDuration = New System.Windows.Forms.Label()
        Me.btnLookupStaff = New System.Windows.Forms.Button()
        Me.labOnSiteFields = New System.Windows.Forms.Label()
        Me.labOnSiteHdrP3 = New System.Windows.Forms.Label()
        Me.timeOnSite = New System.Windows.Forms.DateTimePicker()
        Me.labOnSiteTime = New System.Windows.Forms.Label()
        Me.labPriorityRequired = New System.Windows.Forms.Label()
        Me.labLine3 = New System.Windows.Forms.Label()
        Me.txtNomTech = New System.Windows.Forms.TextBox()
        Me.cmdNavBack = New System.Windows.Forms.Button()
        Me.txtTechName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.labInstructionRequired = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.datePromised = New System.Windows.Forms.DateTimePicker()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboPriority = New System.Windows.Forms.ComboBox()
        Me.LabPriority = New System.Windows.Forms.Label()
        Me.ChkBackupReq = New System.Windows.Forms.CheckBox()
        Me.ChkRecovDisk = New System.Windows.Forms.CheckBox()
        Me.LabInstructions = New System.Windows.Forms.Label()
        Me.Line2 = New System.Windows.Forms.Label()
        Me.LabCostLimit = New System.Windows.Forms.Label()
        Me.chkRefreshCustomer = New System.Windows.Forms.CheckBox()
        Me.cmdPrintAll = New System.Windows.Forms.Button()
        Me.picUserLogo = New System.Windows.Forms.PictureBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdFinish = New System.Windows.Forms.Button()
        Me.txtRcvdName = New System.Windows.Forms.TextBox()
        Me.LabHelpStatus = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabJobStatus = New System.Windows.Forms.Label()
        Me.LabVersion = New System.Windows.Forms.Label()
        Me.LabDateRcvd = New System.Windows.Forms.Label()
        Me.LabRcvdBy = New System.Windows.Forms.Label()
        Me.LabTicket = New System.Windows.Forms.Label()
        Me.LabHdr2 = New System.Windows.Forms.Label()
        Me.LabHdr1 = New System.Windows.Forms.Label()
        Me.OptLogin = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.chkExtras = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.chkPrtDocs = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.optQuotation = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtPassword = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.txtUserName = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.openDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.picSubjectMain = New System.Windows.Forms.PictureBox()
        Me.grpBoxBooking = New System.Windows.Forms.GroupBox()
        Me.chkBooking = New System.Windows.Forms.CheckBox()
        Me.panelSave = New System.Windows.Forms.Panel()
        Me.panelVersion = New System.Windows.Forms.Panel()
        Me.FramePrintOpts.SuspendLayout()
        CType(Me.NumUpDownLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSubjectItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numUpDownOnSiteDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSTabNewJob.SuspendLayout()
        Me._SSTabNewJob_TabPage0.SuspendLayout()
        Me.FrameGoods.SuspendLayout()
        Me.FrameUsers.SuspendLayout()
        Me.grpBoxItemPic.SuspendLayout()
        Me._SSTabNewJob_TabPage1.SuspendLayout()
        Me.frameProblem.SuspendLayout()
        Me._SSTabNewJob_TabPage2.SuspendLayout()
        Me.frameInstructions.SuspendLayout()
        CType(Me.picUserLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptLogin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkExtras, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPrtDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optQuotation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSubjectMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxBooking.SuspendLayout()
        Me.panelSave.SuspendLayout()
        Me.panelVersion.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCheckGoods
        '
        Me.cmdCheckGoods.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCheckGoods.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCheckGoods.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCheckGoods.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCheckGoods.Location = New System.Drawing.Point(161, 80)
        Me.cmdCheckGoods.Name = "cmdCheckGoods"
        Me.cmdCheckGoods.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCheckGoods.Size = New System.Drawing.Size(75, 21)
        Me.cmdCheckGoods.TabIndex = 2
        Me.cmdCheckGoods.Text = "Add/Update"
        Me.ToolTip1.SetToolTip(Me.cmdCheckGoods, "Add to or update Goods Received for Service.")
        Me.cmdCheckGoods.UseVisualStyleBackColor = False
        '
        '_optQuotation_0
        '
        Me._optQuotation_0.BackColor = System.Drawing.Color.Transparent
        Me._optQuotation_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optQuotation_0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optQuotation_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optQuotation.SetIndex(Me._optQuotation_0, CType(0, Short))
        Me._optQuotation_0.Location = New System.Drawing.Point(48, 69)
        Me._optQuotation_0.Name = "_optQuotation_0"
        Me._optQuotation_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optQuotation_0.Size = New System.Drawing.Size(147, 19)
        Me._optQuotation_0.TabIndex = 33
        Me._optQuotation_0.TabStop = True
        Me._optQuotation_0.Text = "Quotation Required"
        Me.ToolTip1.SetToolTip(Me._optQuotation_0, "Assessment and Quotation are required.  Minimum charge applies..")
        Me._optQuotation_0.UseVisualStyleBackColor = False
        '
        '_optQuotation_1
        '
        Me._optQuotation_1.BackColor = System.Drawing.Color.Transparent
        Me._optQuotation_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optQuotation_1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optQuotation_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optQuotation.SetIndex(Me._optQuotation_1, CType(1, Short))
        Me._optQuotation_1.Location = New System.Drawing.Point(48, 92)
        Me._optQuotation_1.Name = "_optQuotation_1"
        Me._optQuotation_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optQuotation_1.Size = New System.Drawing.Size(147, 19)
        Me._optQuotation_1.TabIndex = 34
        Me._optQuotation_1.TabStop = True
        Me._optQuotation_1.Text = "Proceed with Service"
        Me.ToolTip1.SetToolTip(Me._optQuotation_1, "Can proceed with Service as agreed..")
        Me._optQuotation_1.UseVisualStyleBackColor = False
        '
        '_optQuotation_2
        '
        Me._optQuotation_2.BackColor = System.Drawing.Color.Transparent
        Me._optQuotation_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optQuotation_2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optQuotation_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optQuotation.SetIndex(Me._optQuotation_2, CType(2, Short))
        Me._optQuotation_2.Location = New System.Drawing.Point(48, 116)
        Me._optQuotation_2.Name = "_optQuotation_2"
        Me._optQuotation_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optQuotation_2.Size = New System.Drawing.Size(155, 19)
        Me._optQuotation_2.TabIndex = 35
        Me._optQuotation_2.TabStop = True
        Me._optQuotation_2.Text = "Proceed only to Cost Limit"
        Me.ToolTip1.SetToolTip(Me._optQuotation_2, "Can proceed with Service to agreed cost limit..")
        Me._optQuotation_2.UseVisualStyleBackColor = False
        '
        'cmdCancelJob
        '
        Me.cmdCancelJob.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancelJob.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.cmdCancelJob.CausesValidation = False
        Me.cmdCancelJob.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancelJob.Font = New System.Drawing.Font("Tahoma", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelJob.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancelJob.Image = Global.JobMatix3.My.Resources.Resources.trash01
        Me.cmdCancelJob.Location = New System.Drawing.Point(727, 32)
        Me.cmdCancelJob.Name = "cmdCancelJob"
        Me.cmdCancelJob.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancelJob.Size = New System.Drawing.Size(41, 44)
        Me.cmdCancelJob.TabIndex = 51
        Me.cmdCancelJob.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCancelJob, "Cancel Job Completely")
        Me.cmdCancelJob.UseVisualStyleBackColor = False
        '
        'cmdEditSymptoms
        '
        Me.cmdEditSymptoms.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdEditSymptoms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEditSymptoms.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEditSymptoms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEditSymptoms.Location = New System.Drawing.Point(189, 61)
        Me.cmdEditSymptoms.Name = "cmdEditSymptoms"
        Me.cmdEditSymptoms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEditSymptoms.Size = New System.Drawing.Size(60, 21)
        Me.cmdEditSymptoms.TabIndex = 27
        Me.cmdEditSymptoms.Text = "Edit Ref."
        Me.ToolTip1.SetToolTip(Me.cmdEditSymptoms, "Edit Symptoms Reference Table")
        Me.cmdEditSymptoms.UseVisualStyleBackColor = False
        '
        'cmdGoodsClear
        '
        Me.cmdGoodsClear.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdGoodsClear.Location = New System.Drawing.Point(251, 80)
        Me.cmdGoodsClear.Name = "cmdGoodsClear"
        Me.cmdGoodsClear.Size = New System.Drawing.Size(49, 21)
        Me.cmdGoodsClear.TabIndex = 3
        Me.cmdGoodsClear.Text = "Clear"
        Me.ToolTip1.SetToolTip(Me.cmdGoodsClear, "Clear all Goods.")
        Me.cmdGoodsClear.UseVisualStyleBackColor = False
        '
        'FramePrintOpts
        '
        Me.FramePrintOpts.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FramePrintOpts.Controls.Add(Me.Label16)
        Me.FramePrintOpts.Controls.Add(Me.cboLabelPrinters)
        Me.FramePrintOpts.Controls.Add(Me.Label1)
        Me.FramePrintOpts.Controls.Add(Me.cboReceiptPrinters)
        Me.FramePrintOpts.Controls.Add(Me.Label21)
        Me.FramePrintOpts.Controls.Add(Me.cboColourPrinters)
        Me.FramePrintOpts.Controls.Add(Me.NumUpDownLabels)
        Me.FramePrintOpts.Controls.Add(Me._chkPrtDocs_2)
        Me.FramePrintOpts.Controls.Add(Me._chkPrtDocs_0)
        Me.FramePrintOpts.Controls.Add(Me._chkPrtDocs_1)
        Me.FramePrintOpts.Controls.Add(Me.LabPrintDocs)
        Me.FramePrintOpts.Cursor = System.Windows.Forms.Cursors.Default
        Me.FramePrintOpts.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FramePrintOpts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FramePrintOpts.Location = New System.Drawing.Point(181, 586)
        Me.FramePrintOpts.Name = "FramePrintOpts"
        Me.FramePrintOpts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FramePrintOpts.Size = New System.Drawing.Size(450, 100)
        Me.FramePrintOpts.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.FramePrintOpts, "Set number of Job Labels to be printed.")
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Linen
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(315, 53)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(117, 13)
        Me.Label16.TabIndex = 106
        Me.Label16.Text = "-- Label Printer --"
        '
        'cboLabelPrinters
        '
        Me.cboLabelPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboLabelPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLabelPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboLabelPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLabelPrinters.FormattingEnabled = True
        Me.cboLabelPrinters.Location = New System.Drawing.Point(307, 69)
        Me.cboLabelPrinters.Name = "cboLabelPrinters"
        Me.cboLabelPrinters.Size = New System.Drawing.Size(130, 21)
        Me.cboLabelPrinters.TabIndex = 105
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Linen
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(162, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 13)
        Me.Label1.TabIndex = 104
        Me.Label1.Text = "-- Docket Printer --"
        '
        'cboReceiptPrinters
        '
        Me.cboReceiptPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboReceiptPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReceiptPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReceiptPrinters.FormattingEnabled = True
        Me.cboReceiptPrinters.Location = New System.Drawing.Point(154, 69)
        Me.cboReceiptPrinters.Name = "cboReceiptPrinters"
        Me.cboReceiptPrinters.Size = New System.Drawing.Size(130, 21)
        Me.cboReceiptPrinters.TabIndex = 103
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Linen
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Blue
        Me.Label21.Location = New System.Drawing.Point(13, 53)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(117, 13)
        Me.Label21.TabIndex = 102
        Me.Label21.Text = "-- Colour Printer --"
        '
        'cboColourPrinters
        '
        Me.cboColourPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboColourPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColourPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboColourPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboColourPrinters.FormattingEnabled = True
        Me.cboColourPrinters.Location = New System.Drawing.Point(5, 69)
        Me.cboColourPrinters.Name = "cboColourPrinters"
        Me.cboColourPrinters.Size = New System.Drawing.Size(130, 21)
        Me.cboColourPrinters.TabIndex = 101
        '
        'NumUpDownLabels
        '
        Me.NumUpDownLabels.BackColor = System.Drawing.Color.LightSteelBlue
        Me.NumUpDownLabels.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.NumUpDownLabels.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumUpDownLabels.Location = New System.Drawing.Point(394, 25)
        Me.NumUpDownLabels.Maximum = New Decimal(New Integer() {21, 0, 0, 0})
        Me.NumUpDownLabels.Name = "NumUpDownLabels"
        Me.NumUpDownLabels.Size = New System.Drawing.Size(41, 16)
        Me.NumUpDownLabels.TabIndex = 51
        Me.NumUpDownLabels.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.NumUpDownLabels, "Set or overide no. of labels to be printed..")
        Me.NumUpDownLabels.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        '
        '_chkPrtDocs_2
        '
        Me._chkPrtDocs_2.BackColor = System.Drawing.Color.Transparent
        Me._chkPrtDocs_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrtDocs_2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrtDocs_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrtDocs.SetIndex(Me._chkPrtDocs_2, CType(2, Short))
        Me._chkPrtDocs_2.Location = New System.Drawing.Point(307, 20)
        Me._chkPrtDocs_2.Name = "_chkPrtDocs_2"
        Me._chkPrtDocs_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrtDocs_2.Size = New System.Drawing.Size(81, 21)
        Me._chkPrtDocs_2.TabIndex = 49
        Me._chkPrtDocs_2.Text = "Job Labels"
        Me._chkPrtDocs_2.UseVisualStyleBackColor = False
        '
        '_chkPrtDocs_0
        '
        Me._chkPrtDocs_0.BackColor = System.Drawing.Color.Transparent
        Me._chkPrtDocs_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrtDocs_0.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrtDocs_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrtDocs.SetIndex(Me._chkPrtDocs_0, CType(0, Short))
        Me._chkPrtDocs_0.Location = New System.Drawing.Point(10, 22)
        Me._chkPrtDocs_0.Name = "_chkPrtDocs_0"
        Me._chkPrtDocs_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrtDocs_0.Size = New System.Drawing.Size(120, 21)
        Me._chkPrtDocs_0.TabIndex = 48
        Me._chkPrtDocs_0.Text = "Service  Agreement"
        Me._chkPrtDocs_0.UseVisualStyleBackColor = False
        '
        '_chkPrtDocs_1
        '
        Me._chkPrtDocs_1.BackColor = System.Drawing.Color.Transparent
        Me._chkPrtDocs_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrtDocs_1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrtDocs_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrtDocs.SetIndex(Me._chkPrtDocs_1, CType(1, Short))
        Me._chkPrtDocs_1.Location = New System.Drawing.Point(154, 20)
        Me._chkPrtDocs_1.Name = "_chkPrtDocs_1"
        Me._chkPrtDocs_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrtDocs_1.Size = New System.Drawing.Size(102, 21)
        Me._chkPrtDocs_1.TabIndex = 50
        Me._chkPrtDocs_1.Text = "Receipt Docket"
        Me._chkPrtDocs_1.UseVisualStyleBackColor = False
        '
        'LabPrintDocs
        '
        Me.LabPrintDocs.BackColor = System.Drawing.Color.Transparent
        Me.LabPrintDocs.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPrintDocs.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPrintDocs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPrintDocs.Location = New System.Drawing.Point(3, 4)
        Me.LabPrintDocs.Name = "LabPrintDocs"
        Me.LabPrintDocs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPrintDocs.Size = New System.Drawing.Size(105, 15)
        Me.LabPrintDocs.TabIndex = 10
        Me.LabPrintDocs.Text = "Document Printing:"
        Me.LabPrintDocs.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'picSubjectItem
        '
        Me.picSubjectItem.Location = New System.Drawing.Point(6, 12)
        Me.picSubjectItem.Name = "picSubjectItem"
        Me.picSubjectItem.Size = New System.Drawing.Size(125, 113)
        Me.picSubjectItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSubjectItem.TabIndex = 0
        Me.picSubjectItem.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picSubjectItem, "Click to Browse for Picture of Item.")
        '
        'numUpDownOnSiteDuration
        '
        Me.numUpDownOnSiteDuration.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.numUpDownOnSiteDuration.Location = New System.Drawing.Point(456, 329)
        Me.numUpDownOnSiteDuration.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.numUpDownOnSiteDuration.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numUpDownOnSiteDuration.Name = "numUpDownOnSiteDuration"
        Me.numUpDownOnSiteDuration.Size = New System.Drawing.Size(50, 26)
        Me.numUpDownOnSiteDuration.TabIndex = 42
        Me.numUpDownOnSiteDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.numUpDownOnSiteDuration, "The Calendar event will be booked for this amount of time..")
        Me.numUpDownOnSiteDuration.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomer.Location = New System.Drawing.Point(24, 78)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Multiline = True
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReadOnly = True
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(281, 41)
        Me.txtCustomer.TabIndex = 2
        '
        'SSTabNewJob
        '
        Me.SSTabNewJob.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.SSTabNewJob.Controls.Add(Me._SSTabNewJob_TabPage0)
        Me.SSTabNewJob.Controls.Add(Me._SSTabNewJob_TabPage1)
        Me.SSTabNewJob.Controls.Add(Me._SSTabNewJob_TabPage2)
        Me.SSTabNewJob.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTabNewJob.Location = New System.Drawing.Point(24, 123)
        Me.SSTabNewJob.Name = "SSTabNewJob"
        Me.SSTabNewJob.SelectedIndex = 0
        Me.SSTabNewJob.Size = New System.Drawing.Size(744, 454)
        Me.SSTabNewJob.TabIndex = 3
        '
        '_SSTabNewJob_TabPage0
        '
        Me._SSTabNewJob_TabPage0.BackColor = System.Drawing.Color.WhiteSmoke
        Me._SSTabNewJob_TabPage0.Controls.Add(Me.FrameGoods)
        Me._SSTabNewJob_TabPage0.Controls.Add(Me.FrameUsers)
        Me._SSTabNewJob_TabPage0.Controls.Add(Me.grpBoxItemPic)
        Me._SSTabNewJob_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTabNewJob_TabPage0.Name = "_SSTabNewJob_TabPage0"
        Me._SSTabNewJob_TabPage0.Size = New System.Drawing.Size(736, 428)
        Me._SSTabNewJob_TabPage0.TabIndex = 0
        Me._SSTabNewJob_TabPage0.Text = "Goods In Care: "
        '
        'FrameGoods
        '
        Me.FrameGoods.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameGoods.Controls.Add(Me.chkReturned)
        Me.FrameGoods.Controls.Add(Me.chkSystemUnderWarranty)
        Me.FrameGoods.Controls.Add(Me.Label18)
        Me.FrameGoods.Controls.Add(Me.Label17)
        Me.FrameGoods.Controls.Add(Me.cmdGoodsClear)
        Me.FrameGoods.Controls.Add(Me.labGoodsInfoRequired)
        Me.FrameGoods.Controls.Add(Me.txtGoodsList)
        Me.FrameGoods.Controls.Add(Me.labSelectPrevious)
        Me.FrameGoods.Controls.Add(Me.cboPrevGoods)
        Me.FrameGoods.Controls.Add(Me._chkExtras_3)
        Me.FrameGoods.Controls.Add(Me._chkExtras_2)
        Me.FrameGoods.Controls.Add(Me._chkExtras_1)
        Me.FrameGoods.Controls.Add(Me._chkExtras_0)
        Me.FrameGoods.Controls.Add(Me.txtGoodsOther)
        Me.FrameGoods.Controls.Add(Me.cmdCheckGoods)
        Me.FrameGoods.Controls.Add(Me.txtExtrasInCare)
        Me.FrameGoods.Controls.Add(Me.Label7)
        Me.FrameGoods.Controls.Add(Me.LabOtherGoods)
        Me.FrameGoods.Controls.Add(Me.labGoodsHdr)
        Me.FrameGoods.Enabled = False
        Me.FrameGoods.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameGoods.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameGoods.Location = New System.Drawing.Point(6, 2)
        Me.FrameGoods.Name = "FrameGoods"
        Me.FrameGoods.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameGoods.Size = New System.Drawing.Size(720, 287)
        Me.FrameGoods.TabIndex = 4
        Me.FrameGoods.TabStop = False
        '
        'chkReturned
        '
        Me.chkReturned.BackColor = System.Drawing.Color.PaleVioletRed
        Me.chkReturned.Location = New System.Drawing.Point(624, 17)
        Me.chkReturned.Name = "chkReturned"
        Me.chkReturned.Size = New System.Drawing.Size(66, 39)
        Me.chkReturned.TabIndex = 5
        Me.chkReturned.Text = "Return Job"
        Me.chkReturned.UseVisualStyleBackColor = False
        '
        'chkSystemUnderWarranty
        '
        Me.chkSystemUnderWarranty.BackColor = System.Drawing.Color.DarkViolet
        Me.chkSystemUnderWarranty.ForeColor = System.Drawing.Color.White
        Me.chkSystemUnderWarranty.Location = New System.Drawing.Point(458, 17)
        Me.chkSystemUnderWarranty.Name = "chkSystemUnderWarranty"
        Me.chkSystemUnderWarranty.Size = New System.Drawing.Size(95, 39)
        Me.chkSystemUnderWarranty.TabIndex = 4
        Me.chkSystemUnderWarranty.Text = "System Under Warranty"
        Me.chkSystemUnderWarranty.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Gainsboro
        Me.Label18.Location = New System.Drawing.Point(16, 271)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(371, 2)
        Me.Label18.TabIndex = 103
        Me.Label18.Text = "Label18"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(19, 89)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(104, 17)
        Me.Label17.TabIndex = 102
        Me.Label17.Text = "Goods List"
        '
        'labGoodsInfoRequired
        '
        Me.labGoodsInfoRequired.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labGoodsInfoRequired.ForeColor = System.Drawing.Color.DarkMagenta
        Me.labGoodsInfoRequired.Location = New System.Drawing.Point(16, 40)
        Me.labGoodsInfoRequired.Name = "labGoodsInfoRequired"
        Me.labGoodsInfoRequired.Size = New System.Drawing.Size(137, 42)
        Me.labGoodsInfoRequired.TabIndex = 87
        Me.labGoodsInfoRequired.Text = "Info is required.."
        '
        'txtGoodsList
        '
        Me.txtGoodsList.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.txtGoodsList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsList.Location = New System.Drawing.Point(16, 107)
        Me.txtGoodsList.Multiline = True
        Me.txtGoodsList.Name = "txtGoodsList"
        Me.txtGoodsList.ReadOnly = True
        Me.txtGoodsList.Size = New System.Drawing.Size(371, 96)
        Me.txtGoodsList.TabIndex = 6
        Me.txtGoodsList.Text = "txtGoodsList"
        '
        'labSelectPrevious
        '
        Me.labSelectPrevious.BackColor = System.Drawing.Color.Transparent
        Me.labSelectPrevious.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSelectPrevious.Location = New System.Drawing.Point(158, 19)
        Me.labSelectPrevious.Name = "labSelectPrevious"
        Me.labSelectPrevious.Size = New System.Drawing.Size(208, 15)
        Me.labSelectPrevious.TabIndex = 44
        Me.labSelectPrevious.Text = "You can Select from previous Job."
        '
        'cboPrevGoods
        '
        Me.cboPrevGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.cboPrevGoods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrevGoods.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboPrevGoods.FormattingEnabled = True
        Me.cboPrevGoods.Location = New System.Drawing.Point(156, 35)
        Me.cboPrevGoods.Name = "cboPrevGoods"
        Me.cboPrevGoods.Size = New System.Drawing.Size(272, 21)
        Me.cboPrevGoods.TabIndex = 1
        '
        '_chkExtras_3
        '
        Me._chkExtras_3.BackColor = System.Drawing.Color.Transparent
        Me._chkExtras_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkExtras_3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkExtras_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExtras.SetIndex(Me._chkExtras_3, CType(3, Short))
        Me._chkExtras_3.Location = New System.Drawing.Point(141, 246)
        Me._chkExtras_3.Name = "_chkExtras_3"
        Me._chkExtras_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkExtras_3.Size = New System.Drawing.Size(57, 17)
        Me._chkExtras_3.TabIndex = 11
        Me._chkExtras_3.Text = "Cables"
        Me._chkExtras_3.UseVisualStyleBackColor = False
        '
        '_chkExtras_2
        '
        Me._chkExtras_2.BackColor = System.Drawing.Color.Transparent
        Me._chkExtras_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkExtras_2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkExtras_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExtras.SetIndex(Me._chkExtras_2, CType(2, Short))
        Me._chkExtras_2.Location = New System.Drawing.Point(200, 238)
        Me._chkExtras_2.Name = "_chkExtras_2"
        Me._chkExtras_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkExtras_2.Size = New System.Drawing.Size(66, 33)
        Me._chkExtras_2.TabIndex = 13
        Me._chkExtras_2.Text = "Power Supply"
        Me._chkExtras_2.UseVisualStyleBackColor = False
        '
        '_chkExtras_1
        '
        Me._chkExtras_1.BackColor = System.Drawing.Color.Transparent
        Me._chkExtras_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkExtras_1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkExtras_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExtras.SetIndex(Me._chkExtras_1, CType(1, Short))
        Me._chkExtras_1.Location = New System.Drawing.Point(79, 246)
        Me._chkExtras_1.Name = "_chkExtras_1"
        Me._chkExtras_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkExtras_1.Size = New System.Drawing.Size(65, 17)
        Me._chkExtras_1.TabIndex = 9
        Me._chkExtras_1.Text = "Disc(s)"
        Me._chkExtras_1.UseVisualStyleBackColor = False
        '
        '_chkExtras_0
        '
        Me._chkExtras_0.BackColor = System.Drawing.Color.Transparent
        Me._chkExtras_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkExtras_0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkExtras_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExtras.SetIndex(Me._chkExtras_0, CType(0, Short))
        Me._chkExtras_0.Location = New System.Drawing.Point(22, 246)
        Me._chkExtras_0.Name = "_chkExtras_0"
        Me._chkExtras_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkExtras_0.Size = New System.Drawing.Size(45, 17)
        Me._chkExtras_0.TabIndex = 8
        Me._chkExtras_0.Text = "Bag"
        Me._chkExtras_0.UseVisualStyleBackColor = False
        '
        'txtGoodsOther
        '
        Me.txtGoodsOther.AcceptsReturn = True
        Me.txtGoodsOther.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.txtGoodsOther.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoodsOther.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGoodsOther.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoodsOther.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGoodsOther.Location = New System.Drawing.Point(411, 107)
        Me.txtGoodsOther.MaxLength = 240
        Me.txtGoodsOther.Multiline = True
        Me.txtGoodsOther.Name = "txtGoodsOther"
        Me.txtGoodsOther.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGoodsOther.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGoodsOther.Size = New System.Drawing.Size(279, 164)
        Me.txtGoodsOther.TabIndex = 7
        '
        'txtExtrasInCare
        '
        Me.txtExtrasInCare.AcceptsReturn = True
        Me.txtExtrasInCare.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtExtrasInCare.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtExtrasInCare.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExtrasInCare.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExtrasInCare.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExtrasInCare.Location = New System.Drawing.Point(21, 209)
        Me.txtExtrasInCare.MaxLength = 0
        Me.txtExtrasInCare.Multiline = True
        Me.txtExtrasInCare.Name = "txtExtrasInCare"
        Me.txtExtrasInCare.ReadOnly = True
        Me.txtExtrasInCare.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtrasInCare.Size = New System.Drawing.Size(353, 20)
        Me.txtExtrasInCare.TabIndex = 14
        Me.txtExtrasInCare.TabStop = False
        Me.txtExtrasInCare.Text = "txtExtrasInCare"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(18, 222)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(95, 21)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Extras In Care:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabOtherGoods
        '
        Me.LabOtherGoods.BackColor = System.Drawing.Color.Transparent
        Me.LabOtherGoods.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabOtherGoods.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabOtherGoods.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabOtherGoods.Location = New System.Drawing.Point(408, 89)
        Me.LabOtherGoods.Name = "LabOtherGoods"
        Me.LabOtherGoods.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabOtherGoods.Size = New System.Drawing.Size(255, 17)
        Me.LabOtherGoods.TabIndex = 39
        Me.LabOtherGoods.Text = "Other Goods && Extras:"
        '
        'labGoodsHdr
        '
        Me.labGoodsHdr.BackColor = System.Drawing.Color.Transparent
        Me.labGoodsHdr.Cursor = System.Windows.Forms.Cursors.Default
        Me.labGoodsHdr.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labGoodsHdr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labGoodsHdr.Location = New System.Drawing.Point(13, 17)
        Me.labGoodsHdr.Name = "labGoodsHdr"
        Me.labGoodsHdr.Padding = New System.Windows.Forms.Padding(2, 1, 0, 0)
        Me.labGoodsHdr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labGoodsHdr.Size = New System.Drawing.Size(110, 23)
        Me.labGoodsHdr.TabIndex = 38
        Me.labGoodsHdr.Text = "Goods In Care: "
        '
        'FrameUsers
        '
        Me.FrameUsers.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameUsers.Controls.Add(Me.labLogonRequired)
        Me.FrameUsers.Controls.Add(Me.cmdNext)
        Me.FrameUsers.Controls.Add(Me._OptLogin_1)
        Me.FrameUsers.Controls.Add(Me._OptLogin_0)
        Me.FrameUsers.Controls.Add(Me._txtPassword_2)
        Me.FrameUsers.Controls.Add(Me._txtUserName_2)
        Me.FrameUsers.Controls.Add(Me._txtPassword_1)
        Me.FrameUsers.Controls.Add(Me._txtUserName_1)
        Me.FrameUsers.Controls.Add(Me._txtPassword_0)
        Me.FrameUsers.Controls.Add(Me._txtUserName_0)
        Me.FrameUsers.Controls.Add(Me.ChkUsers)
        Me.FrameUsers.Controls.Add(Me.Label15)
        Me.FrameUsers.Controls.Add(Me.Label14)
        Me.FrameUsers.Controls.Add(Me.Label13)
        Me.FrameUsers.Controls.Add(Me.Label12)
        Me.FrameUsers.Controls.Add(Me.Label11)
        Me.FrameUsers.Controls.Add(Me.Label5)
        Me.FrameUsers.Controls.Add(Me.Label4)
        Me.FrameUsers.Controls.Add(Me.Line1)
        Me.FrameUsers.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameUsers.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameUsers.Location = New System.Drawing.Point(158, 291)
        Me.FrameUsers.Name = "FrameUsers"
        Me.FrameUsers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameUsers.Size = New System.Drawing.Size(568, 130)
        Me.FrameUsers.TabIndex = 6
        Me.FrameUsers.TabStop = False
        Me.FrameUsers.Text = "FrameUsers"
        '
        'labLogonRequired
        '
        Me.labLogonRequired.AutoSize = True
        Me.labLogonRequired.ForeColor = System.Drawing.Color.DarkMagenta
        Me.labLogonRequired.Location = New System.Drawing.Point(13, 39)
        Me.labLogonRequired.Name = "labLogonRequired"
        Me.labLogonRequired.Size = New System.Drawing.Size(125, 13)
        Me.labLogonRequired.TabIndex = 87
        Me.labLogonRequired.Text = "Selection is Required"
        '
        'cmdNext
        '
        Me.cmdNext.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdNext.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNext.Location = New System.Drawing.Point(472, 91)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(66, 25)
        Me.cmdNext.TabIndex = 24
        Me.cmdNext.Text = "Next >>"
        Me.cmdNext.UseVisualStyleBackColor = False
        '
        '_OptLogin_1
        '
        Me._OptLogin_1.BackColor = System.Drawing.Color.Transparent
        Me._OptLogin_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptLogin_1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptLogin_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptLogin.SetIndex(Me._OptLogin_1, CType(1, Short))
        Me._OptLogin_1.Location = New System.Drawing.Point(16, 77)
        Me._OptLogin_1.Name = "_OptLogin_1"
        Me._OptLogin_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptLogin_1.Size = New System.Drawing.Size(126, 25)
        Me._OptLogin_1.TabIndex = 16
        Me._OptLogin_1.TabStop = True
        Me._OptLogin_1.Text = "No Logon Required"
        Me._OptLogin_1.UseVisualStyleBackColor = False
        '
        '_OptLogin_0
        '
        Me._OptLogin_0.BackColor = System.Drawing.Color.Transparent
        Me._OptLogin_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptLogin_0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptLogin_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptLogin.SetIndex(Me._OptLogin_0, CType(0, Short))
        Me._OptLogin_0.Location = New System.Drawing.Point(16, 57)
        Me._OptLogin_0.Name = "_OptLogin_0"
        Me._OptLogin_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptLogin_0.Size = New System.Drawing.Size(107, 17)
        Me._OptLogin_0.TabIndex = 15
        Me._OptLogin_0.TabStop = True
        Me._OptLogin_0.Text = "Logon Required"
        Me._OptLogin_0.UseVisualStyleBackColor = False
        '
        '_txtPassword_2
        '
        Me._txtPassword_2.AcceptsReturn = True
        Me._txtPassword_2.BackColor = System.Drawing.Color.White
        Me._txtPassword_2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtPassword_2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtPassword_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtPassword_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPassword.SetIndex(Me._txtPassword_2, CType(2, Short))
        Me._txtPassword_2.Location = New System.Drawing.Point(343, 104)
        Me._txtPassword_2.MaxLength = 0
        Me._txtPassword_2.Name = "_txtPassword_2"
        Me._txtPassword_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtPassword_2.Size = New System.Drawing.Size(121, 13)
        Me._txtPassword_2.TabIndex = 23
        Me._txtPassword_2.Text = "txtPassword"
        '
        '_txtUserName_2
        '
        Me._txtUserName_2.AcceptsReturn = True
        Me._txtUserName_2.BackColor = System.Drawing.Color.White
        Me._txtUserName_2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtUserName_2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtUserName_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtUserName_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserName.SetIndex(Me._txtUserName_2, CType(2, Short))
        Me._txtUserName_2.Location = New System.Drawing.Point(199, 103)
        Me._txtUserName_2.MaxLength = 0
        Me._txtUserName_2.Name = "_txtUserName_2"
        Me._txtUserName_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtUserName_2.Size = New System.Drawing.Size(89, 13)
        Me._txtUserName_2.TabIndex = 22
        Me._txtUserName_2.Text = "txtUserName"
        '
        '_txtPassword_1
        '
        Me._txtPassword_1.AcceptsReturn = True
        Me._txtPassword_1.BackColor = System.Drawing.Color.White
        Me._txtPassword_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtPassword_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtPassword_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtPassword_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPassword.SetIndex(Me._txtPassword_1, CType(1, Short))
        Me._txtPassword_1.Location = New System.Drawing.Point(343, 77)
        Me._txtPassword_1.MaxLength = 0
        Me._txtPassword_1.Name = "_txtPassword_1"
        Me._txtPassword_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtPassword_1.Size = New System.Drawing.Size(121, 13)
        Me._txtPassword_1.TabIndex = 21
        Me._txtPassword_1.Text = "txtPassword"
        '
        '_txtUserName_1
        '
        Me._txtUserName_1.AcceptsReturn = True
        Me._txtUserName_1.BackColor = System.Drawing.Color.White
        Me._txtUserName_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtUserName_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtUserName_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtUserName_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserName.SetIndex(Me._txtUserName_1, CType(1, Short))
        Me._txtUserName_1.Location = New System.Drawing.Point(199, 77)
        Me._txtUserName_1.MaxLength = 0
        Me._txtUserName_1.Name = "_txtUserName_1"
        Me._txtUserName_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtUserName_1.Size = New System.Drawing.Size(89, 13)
        Me._txtUserName_1.TabIndex = 20
        Me._txtUserName_1.Text = "txtUserName"
        '
        '_txtPassword_0
        '
        Me._txtPassword_0.AcceptsReturn = True
        Me._txtPassword_0.BackColor = System.Drawing.Color.White
        Me._txtPassword_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtPassword_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtPassword_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtPassword_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPassword.SetIndex(Me._txtPassword_0, CType(0, Short))
        Me._txtPassword_0.Location = New System.Drawing.Point(343, 27)
        Me._txtPassword_0.MaxLength = 0
        Me._txtPassword_0.Name = "_txtPassword_0"
        Me._txtPassword_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtPassword_0.Size = New System.Drawing.Size(121, 13)
        Me._txtPassword_0.TabIndex = 18
        Me._txtPassword_0.Text = "txtPassword"
        '
        '_txtUserName_0
        '
        Me._txtUserName_0.AcceptsReturn = True
        Me._txtUserName_0.BackColor = System.Drawing.Color.White
        Me._txtUserName_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtUserName_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtUserName_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtUserName_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserName.SetIndex(Me._txtUserName_0, CType(0, Short))
        Me._txtUserName_0.Location = New System.Drawing.Point(199, 27)
        Me._txtUserName_0.MaxLength = 0
        Me._txtUserName_0.Name = "_txtUserName_0"
        Me._txtUserName_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtUserName_0.Size = New System.Drawing.Size(89, 13)
        Me._txtUserName_0.TabIndex = 17
        Me._txtUserName_0.Text = "txtUserName"
        '
        'ChkUsers
        '
        Me.ChkUsers.BackColor = System.Drawing.Color.Transparent
        Me.ChkUsers.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkUsers.Enabled = False
        Me.ChkUsers.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkUsers.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkUsers.Location = New System.Drawing.Point(163, 49)
        Me.ChkUsers.Name = "ChkUsers"
        Me.ChkUsers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkUsers.Size = New System.Drawing.Size(137, 20)
        Me.ChkUsers.TabIndex = 19
        Me.ChkUsers.Text = "Multiple User Accounts"
        Me.ChkUsers.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(9, 19)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(133, 17)
        Me.Label15.TabIndex = 81
        Me.Label15.Text = "User Account Details:"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(303, 105)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(41, 17)
        Me.Label14.TabIndex = 58
        Me.Label14.Text = "Pwd3:"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(159, 103)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(41, 17)
        Me.Label13.TabIndex = 57
        Me.Label13.Text = "User3:"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(303, 77)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(41, 17)
        Me.Label12.TabIndex = 56
        Me.Label12.Text = "Pwd2:"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(159, 77)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(41, 17)
        Me.Label11.TabIndex = 55
        Me.Label11.Text = "User2:"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(303, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(41, 17)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "Pwd1:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(159, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(41, 17)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "User1:"
        '
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.Line1.Location = New System.Drawing.Point(143, 18)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(1, 99)
        Me.Line1.TabIndex = 82
        '
        'grpBoxItemPic
        '
        Me.grpBoxItemPic.BackColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.grpBoxItemPic.Controls.Add(Me.picSubjectItem)
        Me.grpBoxItemPic.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxItemPic.Location = New System.Drawing.Point(6, 291)
        Me.grpBoxItemPic.Name = "grpBoxItemPic"
        Me.grpBoxItemPic.Size = New System.Drawing.Size(141, 130)
        Me.grpBoxItemPic.TabIndex = 5
        Me.grpBoxItemPic.TabStop = False
        Me.grpBoxItemPic.Text = "grpBoxItemPic"
        '
        '_SSTabNewJob_TabPage1
        '
        Me._SSTabNewJob_TabPage1.BackColor = System.Drawing.Color.WhiteSmoke
        Me._SSTabNewJob_TabPage1.Controls.Add(Me.frameProblem)
        Me._SSTabNewJob_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTabNewJob_TabPage1.Name = "_SSTabNewJob_TabPage1"
        Me._SSTabNewJob_TabPage1.Size = New System.Drawing.Size(736, 428)
        Me._SSTabNewJob_TabPage1.TabIndex = 3
        Me._SSTabNewJob_TabPage1.Text = "Job Details"
        '
        'frameProblem
        '
        Me.frameProblem.Controls.Add(Me.cmdNext2)
        Me.frameProblem.Controls.Add(Me.cmdNavBackToStart)
        Me.frameProblem.Controls.Add(Me.labProblemRequired)
        Me.frameProblem.Controls.Add(Me.Label2)
        Me.frameProblem.Controls.Add(Me.listSymptoms)
        Me.frameProblem.Controls.Add(Me.cmdEditSymptoms)
        Me.frameProblem.Controls.Add(Me.txtProblem)
        Me.frameProblem.Controls.Add(Me.txtSymptoms)
        Me.frameProblem.Controls.Add(Me.Label10)
        Me.frameProblem.Controls.Add(Me.LabProblem)
        Me.frameProblem.Location = New System.Drawing.Point(6, 2)
        Me.frameProblem.Name = "frameProblem"
        Me.frameProblem.Size = New System.Drawing.Size(720, 423)
        Me.frameProblem.TabIndex = 25
        Me.frameProblem.TabStop = False
        Me.frameProblem.Text = "frameProblem"
        '
        'cmdNext2
        '
        Me.cmdNext2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdNext2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNext2.Location = New System.Drawing.Point(624, 380)
        Me.cmdNext2.Name = "cmdNext2"
        Me.cmdNext2.Size = New System.Drawing.Size(66, 25)
        Me.cmdNext2.TabIndex = 31
        Me.cmdNext2.Text = "Next >>"
        Me.cmdNext2.UseVisualStyleBackColor = False
        '
        'cmdNavBackToStart
        '
        Me.cmdNavBackToStart.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdNavBackToStart.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNavBackToStart.Location = New System.Drawing.Point(17, 356)
        Me.cmdNavBackToStart.Name = "cmdNavBackToStart"
        Me.cmdNavBackToStart.Size = New System.Drawing.Size(66, 25)
        Me.cmdNavBackToStart.TabIndex = 30
        Me.cmdNavBackToStart.Text = "<< Back"
        Me.cmdNavBackToStart.UseVisualStyleBackColor = False
        '
        'labProblemRequired
        '
        Me.labProblemRequired.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labProblemRequired.ForeColor = System.Drawing.Color.DarkMagenta
        Me.labProblemRequired.Location = New System.Drawing.Point(148, 14)
        Me.labProblemRequired.Name = "labProblemRequired"
        Me.labProblemRequired.Size = New System.Drawing.Size(235, 28)
        Me.labProblemRequired.TabIndex = 91
        Me.labProblemRequired.Text = "Problem symptoms, or work requested:  Something must be entered here."
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(15, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 27)
        Me.Label2.TabIndex = 90
        Me.Label2.Text = "Check problems/work with Checklist:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'listSymptoms
        '
        Me.listSymptoms.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.listSymptoms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.listSymptoms.Cursor = System.Windows.Forms.Cursors.Default
        Me.listSymptoms.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listSymptoms.ForeColor = System.Drawing.Color.MidnightBlue
        Me.listSymptoms.HorizontalScrollbar = True
        Me.listSymptoms.Location = New System.Drawing.Point(17, 88)
        Me.listSymptoms.Name = "listSymptoms"
        Me.listSymptoms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.listSymptoms.Size = New System.Drawing.Size(232, 238)
        Me.listSymptoms.Sorted = True
        Me.listSymptoms.TabIndex = 26
        '
        'txtProblem
        '
        Me.txtProblem.AcceptsReturn = True
        Me.txtProblem.BackColor = System.Drawing.Color.White
        Me.txtProblem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtProblem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProblem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProblem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProblem.Location = New System.Drawing.Point(276, 216)
        Me.txtProblem.MaxLength = 4000
        Me.txtProblem.Multiline = True
        Me.txtProblem.Name = "txtProblem"
        Me.txtProblem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProblem.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProblem.Size = New System.Drawing.Size(305, 107)
        Me.txtProblem.TabIndex = 29
        '
        'txtSymptoms
        '
        Me.txtSymptoms.AcceptsReturn = True
        Me.txtSymptoms.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtSymptoms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSymptoms.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSymptoms.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSymptoms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSymptoms.Location = New System.Drawing.Point(276, 88)
        Me.txtSymptoms.MaxLength = 0
        Me.txtSymptoms.Multiline = True
        Me.txtSymptoms.Name = "txtSymptoms"
        Me.txtSymptoms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSymptoms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSymptoms.Size = New System.Drawing.Size(305, 87)
        Me.txtSymptoms.TabIndex = 28
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(14, 25)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(104, 17)
        Me.Label10.TabIndex = 89
        Me.Label10.Text = "Job Details"
        '
        'LabProblem
        '
        Me.LabProblem.BackColor = System.Drawing.Color.Transparent
        Me.LabProblem.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabProblem.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabProblem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabProblem.Location = New System.Drawing.Point(273, 198)
        Me.LabProblem.Name = "LabProblem"
        Me.LabProblem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabProblem.Size = New System.Drawing.Size(239, 17)
        Me.LabProblem.TabIndex = 88
        Me.LabProblem.Text = "Enter any further job Info here."
        '
        '_SSTabNewJob_TabPage2
        '
        Me._SSTabNewJob_TabPage2.BackColor = System.Drawing.Color.WhiteSmoke
        Me._SSTabNewJob_TabPage2.Controls.Add(Me.frameInstructions)
        Me._SSTabNewJob_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTabNewJob_TabPage2.Name = "_SSTabNewJob_TabPage2"
        Me._SSTabNewJob_TabPage2.Size = New System.Drawing.Size(736, 428)
        Me._SSTabNewJob_TabPage2.TabIndex = 2
        Me._SSTabNewJob_TabPage2.Text = "Instructions"
        '
        'frameInstructions
        '
        Me.frameInstructions.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameInstructions.Controls.Add(Me.labOnSiteDurationWarning)
        Me.frameInstructions.Controls.Add(Me.numUpDownOnSiteDuration)
        Me.frameInstructions.Controls.Add(Me.labOnSiteDuration)
        Me.frameInstructions.Controls.Add(Me.btnLookupStaff)
        Me.frameInstructions.Controls.Add(Me.labOnSiteFields)
        Me.frameInstructions.Controls.Add(Me.labOnSiteHdrP3)
        Me.frameInstructions.Controls.Add(Me.timeOnSite)
        Me.frameInstructions.Controls.Add(Me.labOnSiteTime)
        Me.frameInstructions.Controls.Add(Me.labPriorityRequired)
        Me.frameInstructions.Controls.Add(Me.labLine3)
        Me.frameInstructions.Controls.Add(Me.txtNomTech)
        Me.frameInstructions.Controls.Add(Me.cmdNavBack)
        Me.frameInstructions.Controls.Add(Me.txtTechName)
        Me.frameInstructions.Controls.Add(Me.Label6)
        Me.frameInstructions.Controls.Add(Me.labInstructionRequired)
        Me.frameInstructions.Controls.Add(Me.Label8)
        Me.frameInstructions.Controls.Add(Me.datePromised)
        Me.frameInstructions.Controls.Add(Me.Label9)
        Me.frameInstructions.Controls.Add(Me.cboPriority)
        Me.frameInstructions.Controls.Add(Me.LabPriority)
        Me.frameInstructions.Controls.Add(Me.ChkBackupReq)
        Me.frameInstructions.Controls.Add(Me.ChkRecovDisk)
        Me.frameInstructions.Controls.Add(Me._optQuotation_0)
        Me.frameInstructions.Controls.Add(Me._optQuotation_1)
        Me.frameInstructions.Controls.Add(Me._optQuotation_2)
        Me.frameInstructions.Controls.Add(Me.LabInstructions)
        Me.frameInstructions.Controls.Add(Me.Line2)
        Me.frameInstructions.Controls.Add(Me.LabCostLimit)
        Me.frameInstructions.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameInstructions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameInstructions.Location = New System.Drawing.Point(6, 2)
        Me.frameInstructions.Name = "frameInstructions"
        Me.frameInstructions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameInstructions.Size = New System.Drawing.Size(720, 423)
        Me.frameInstructions.TabIndex = 32
        Me.frameInstructions.TabStop = False
        '
        'labOnSiteDurationWarning
        '
        Me.labOnSiteDurationWarning.Location = New System.Drawing.Point(297, 359)
        Me.labOnSiteDurationWarning.Name = "labOnSiteDurationWarning"
        Me.labOnSiteDurationWarning.Size = New System.Drawing.Size(215, 56)
        Me.labOnSiteDurationWarning.TabIndex = 94
        Me.labOnSiteDurationWarning.Text = "NB. Make sure you set the right Duration value for the Calendar,  as the (Duratio" & _
    "n) UpDown control will have been set back to its default value.."
        '
        'labOnSiteDuration
        '
        Me.labOnSiteDuration.BackColor = System.Drawing.Color.Goldenrod
        Me.labOnSiteDuration.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labOnSiteDuration.Location = New System.Drawing.Point(299, 331)
        Me.labOnSiteDuration.Name = "labOnSiteDuration"
        Me.labOnSiteDuration.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.labOnSiteDuration.Size = New System.Drawing.Size(151, 21)
        Me.labOnSiteDuration.TabIndex = 93
        Me.labOnSiteDuration.Text = "OnSite Duration (Hours)"
        Me.ToolTip1.SetToolTip(Me.labOnSiteDuration, "Estimated OnSite Job Duration (in hours)..")
        '
        'btnLookupStaff
        '
        Me.btnLookupStaff.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLookupStaff.Location = New System.Drawing.Point(624, 312)
        Me.btnLookupStaff.Name = "btnLookupStaff"
        Me.btnLookupStaff.Size = New System.Drawing.Size(63, 28)
        Me.btnLookupStaff.TabIndex = 43
        Me.btnLookupStaff.Text = "Lookup"
        Me.btnLookupStaff.UseVisualStyleBackColor = True
        '
        'labOnSiteFields
        '
        Me.labOnSiteFields.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labOnSiteFields.ForeColor = System.Drawing.Color.SaddleBrown
        Me.labOnSiteFields.Location = New System.Drawing.Point(298, 184)
        Me.labOnSiteFields.Name = "labOnSiteFields"
        Me.labOnSiteFields.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labOnSiteFields.Size = New System.Drawing.Size(187, 47)
        Me.labOnSiteFields.TabIndex = 92
        Me.labOnSiteFields.Text = "NB:  OnSite Job..   Must have Date Promised, Time booked, and Nominated Tech spec" & _
    "ified."
        '
        'labOnSiteHdrP3
        '
        Me.labOnSiteHdrP3.BackColor = System.Drawing.Color.Goldenrod
        Me.labOnSiteHdrP3.Cursor = System.Windows.Forms.Cursors.Default
        Me.labOnSiteHdrP3.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labOnSiteHdrP3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labOnSiteHdrP3.Location = New System.Drawing.Point(298, 234)
        Me.labOnSiteHdrP3.Name = "labOnSiteHdrP3"
        Me.labOnSiteHdrP3.Padding = New System.Windows.Forms.Padding(2, 1, 0, 0)
        Me.labOnSiteHdrP3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labOnSiteHdrP3.Size = New System.Drawing.Size(203, 23)
        Me.labOnSiteHdrP3.TabIndex = 91
        Me.labOnSiteHdrP3.Text = "On-Site Job"
        '
        'timeOnSite
        '
        Me.timeOnSite.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.timeOnSite.Location = New System.Drawing.Point(419, 301)
        Me.timeOnSite.Name = "timeOnSite"
        Me.timeOnSite.ShowUpDown = True
        Me.timeOnSite.Size = New System.Drawing.Size(87, 21)
        Me.timeOnSite.TabIndex = 41
        '
        'labOnSiteTime
        '
        Me.labOnSiteTime.BackColor = System.Drawing.Color.Goldenrod
        Me.labOnSiteTime.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labOnSiteTime.Location = New System.Drawing.Point(298, 301)
        Me.labOnSiteTime.Name = "labOnSiteTime"
        Me.labOnSiteTime.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.labOnSiteTime.Size = New System.Drawing.Size(119, 21)
        Me.labOnSiteTime.TabIndex = 89
        Me.labOnSiteTime.Text = "OnSite TimeBooked"
        '
        'labPriorityRequired
        '
        Me.labPriorityRequired.AutoSize = True
        Me.labPriorityRequired.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPriorityRequired.ForeColor = System.Drawing.Color.DarkMagenta
        Me.labPriorityRequired.Location = New System.Drawing.Point(47, 184)
        Me.labPriorityRequired.Name = "labPriorityRequired"
        Me.labPriorityRequired.Size = New System.Drawing.Size(170, 13)
        Me.labPriorityRequired.TabIndex = 88
        Me.labPriorityRequired.Text = "Priority Selection is Required"
        '
        'labLine3
        '
        Me.labLine3.BackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.labLine3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.labLine3.Location = New System.Drawing.Point(48, 160)
        Me.labLine3.Name = "labLine3"
        Me.labLine3.Size = New System.Drawing.Size(300, 1)
        Me.labLine3.TabIndex = 87
        Me.labLine3.Text = "Label20"
        '
        'txtNomTech
        '
        Me.txtNomTech.AcceptsReturn = True
        Me.txtNomTech.BackColor = System.Drawing.Color.White
        Me.txtNomTech.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNomTech.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNomTech.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNomTech.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNomTech.Location = New System.Drawing.Point(540, 353)
        Me.txtNomTech.MaxLength = 0
        Me.txtNomTech.Name = "txtNomTech"
        Me.txtNomTech.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNomTech.Size = New System.Drawing.Size(47, 21)
        Me.txtNomTech.TabIndex = 44
        '
        'cmdNavBack
        '
        Me.cmdNavBack.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdNavBack.Location = New System.Drawing.Point(17, 356)
        Me.cmdNavBack.Name = "cmdNavBack"
        Me.cmdNavBack.Size = New System.Drawing.Size(66, 25)
        Me.cmdNavBack.TabIndex = 45
        Me.cmdNavBack.Text = "<< Back"
        Me.cmdNavBack.UseVisualStyleBackColor = False
        '
        'txtTechName
        '
        Me.txtTechName.AcceptsReturn = True
        Me.txtTechName.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTechName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTechName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTechName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTechName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTechName.Location = New System.Drawing.Point(541, 380)
        Me.txtTechName.MaxLength = 0
        Me.txtTechName.Name = "txtTechName"
        Me.txtTechName.ReadOnly = True
        Me.txtTechName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTechName.Size = New System.Drawing.Size(146, 14)
        Me.txtTechName.TabIndex = 44
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(540, 312)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(78, 31)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "Nominated Tech/Staff:"
        '
        'labInstructionRequired
        '
        Me.labInstructionRequired.AutoSize = True
        Me.labInstructionRequired.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labInstructionRequired.ForeColor = System.Drawing.Color.DarkMagenta
        Me.labInstructionRequired.Location = New System.Drawing.Point(47, 51)
        Me.labInstructionRequired.Name = "labInstructionRequired"
        Me.labInstructionRequired.Size = New System.Drawing.Size(125, 13)
        Me.labInstructionRequired.TabIndex = 86
        Me.labInstructionRequired.Text = "Selection is Required"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(604, 352)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(57, 17)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "(Barcode)"
        '
        'datePromised
        '
        Me.datePromised.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datePromised.Location = New System.Drawing.Point(369, 271)
        Me.datePromised.Name = "datePromised"
        Me.datePromised.ShowCheckBox = True
        Me.datePromised.Size = New System.Drawing.Size(137, 21)
        Me.datePromised.TabIndex = 40
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(296, 261)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 31)
        Me.Label9.TabIndex = 84
        Me.Label9.Text = "Date Promised:"
        '
        'cboPriority
        '
        Me.cboPriority.BackColor = System.Drawing.Color.White
        Me.cboPriority.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboPriority.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPriority.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPriority.Location = New System.Drawing.Point(48, 231)
        Me.cboPriority.Name = "cboPriority"
        Me.cboPriority.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPriority.Size = New System.Drawing.Size(184, 21)
        Me.cboPriority.Sorted = True
        Me.cboPriority.TabIndex = 38
        '
        'LabPriority
        '
        Me.LabPriority.BackColor = System.Drawing.Color.Transparent
        Me.LabPriority.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPriority.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPriority.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPriority.Location = New System.Drawing.Point(47, 211)
        Me.LabPriority.Name = "LabPriority"
        Me.LabPriority.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPriority.Size = New System.Drawing.Size(101, 17)
        Me.LabPriority.TabIndex = 42
        Me.LabPriority.Text = "Job Priority:"
        '
        'ChkBackupReq
        '
        Me.ChkBackupReq.BackColor = System.Drawing.Color.Transparent
        Me.ChkBackupReq.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkBackupReq.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkBackupReq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkBackupReq.Location = New System.Drawing.Point(398, 96)
        Me.ChkBackupReq.Name = "ChkBackupReq"
        Me.ChkBackupReq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkBackupReq.Size = New System.Drawing.Size(87, 32)
        Me.ChkBackupReq.TabIndex = 36
        Me.ChkBackupReq.Text = "Data Backup Required"
        Me.ChkBackupReq.UseVisualStyleBackColor = False
        '
        'ChkRecovDisk
        '
        Me.ChkRecovDisk.BackColor = System.Drawing.Color.Transparent
        Me.ChkRecovDisk.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkRecovDisk.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkRecovDisk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkRecovDisk.Location = New System.Drawing.Point(499, 94)
        Me.ChkRecovDisk.Name = "ChkRecovDisk"
        Me.ChkRecovDisk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkRecovDisk.Size = New System.Drawing.Size(75, 34)
        Me.ChkRecovDisk.TabIndex = 37
        Me.ChkRecovDisk.Text = "Recovery Discs"
        Me.ChkRecovDisk.UseVisualStyleBackColor = False
        '
        'LabInstructions
        '
        Me.LabInstructions.BackColor = System.Drawing.Color.Transparent
        Me.LabInstructions.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabInstructions.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabInstructions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabInstructions.Location = New System.Drawing.Point(14, 26)
        Me.LabInstructions.Name = "LabInstructions"
        Me.LabInstructions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabInstructions.Size = New System.Drawing.Size(176, 16)
        Me.LabInstructions.TabIndex = 72
        Me.LabInstructions.Text = "Customer's Instructions:"
        '
        'Line2
        '
        Me.Line2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Line2.Location = New System.Drawing.Point(376, 56)
        Me.Line2.Name = "Line2"
        Me.Line2.Size = New System.Drawing.Size(1, 72)
        Me.Line2.TabIndex = 81
        '
        'LabCostLimit
        '
        Me.LabCostLimit.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.LabCostLimit.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabCostLimit.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabCostLimit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabCostLimit.Location = New System.Drawing.Point(209, 58)
        Me.LabCostLimit.Name = "LabCostLimit"
        Me.LabCostLimit.Padding = New System.Windows.Forms.Padding(3)
        Me.LabCostLimit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabCostLimit.Size = New System.Drawing.Size(139, 82)
        Me.LabCostLimit.TabIndex = 39
        Me.LabCostLimit.Text = "Will notify Customer if cost can exceed $9999.00"
        '
        'chkRefreshCustomer
        '
        Me.chkRefreshCustomer.BackColor = System.Drawing.Color.Transparent
        Me.chkRefreshCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRefreshCustomer.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRefreshCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefreshCustomer.Location = New System.Drawing.Point(107, 54)
        Me.chkRefreshCustomer.Name = "chkRefreshCustomer"
        Me.chkRefreshCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRefreshCustomer.Size = New System.Drawing.Size(159, 24)
        Me.chkRefreshCustomer.TabIndex = 1
        Me.chkRefreshCustomer.Text = "Refresh Customer Details"
        Me.chkRefreshCustomer.UseVisualStyleBackColor = False
        '
        'cmdPrintAll
        '
        Me.cmdPrintAll.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdPrintAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrintAll.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrintAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrintAll.Location = New System.Drawing.Point(7, 65)
        Me.cmdPrintAll.Name = "cmdPrintAll"
        Me.cmdPrintAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrintAll.Size = New System.Drawing.Size(53, 21)
        Me.cmdPrintAll.TabIndex = 46
        Me.cmdPrintAll.Text = "Print"
        Me.cmdPrintAll.UseVisualStyleBackColor = False
        '
        'picUserLogo
        '
        Me.picUserLogo.BackColor = System.Drawing.SystemColors.Control
        Me.picUserLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picUserLogo.Cursor = System.Windows.Forms.Cursors.Default
        Me.picUserLogo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picUserLogo.Location = New System.Drawing.Point(6, 551)
        Me.picUserLogo.Name = "picUserLogo"
        Me.picUserLogo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picUserLogo.Size = New System.Drawing.Size(16, 26)
        Me.picUserLogo.TabIndex = 7
        Me.picUserLogo.TabStop = False
        Me.picUserLogo.Visible = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancel.CausesValidation = False
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(66, 65)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(51, 21)
        Me.cmdCancel.TabIndex = 47
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdFinish
        '
        Me.cmdFinish.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdFinish.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFinish.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFinish.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFinish.Location = New System.Drawing.Point(7, 19)
        Me.cmdFinish.Name = "cmdFinish"
        Me.cmdFinish.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFinish.Size = New System.Drawing.Size(110, 26)
        Me.cmdFinish.TabIndex = 45
        Me.cmdFinish.Text = "Save && Print"
        Me.cmdFinish.UseVisualStyleBackColor = False
        '
        'txtRcvdName
        '
        Me.txtRcvdName.AcceptsReturn = True
        Me.txtRcvdName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtRcvdName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRcvdName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRcvdName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRcvdName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRcvdName.Location = New System.Drawing.Point(108, 37)
        Me.txtRcvdName.MaxLength = 0
        Me.txtRcvdName.Name = "txtRcvdName"
        Me.txtRcvdName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRcvdName.Size = New System.Drawing.Size(70, 14)
        Me.txtRcvdName.TabIndex = 0
        Me.txtRcvdName.TabStop = False
        '
        'LabHelpStatus
        '
        Me.LabHelpStatus.BackColor = System.Drawing.Color.Transparent
        Me.LabHelpStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHelpStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHelpStatus.ForeColor = System.Drawing.Color.Black
        Me.LabHelpStatus.Location = New System.Drawing.Point(474, 75)
        Me.LabHelpStatus.Name = "LabHelpStatus"
        Me.LabHelpStatus.Padding = New System.Windows.Forms.Padding(5, 3, 0, 0)
        Me.LabHelpStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHelpStatus.Size = New System.Drawing.Size(294, 45)
        Me.LabHelpStatus.TabIndex = 78
        Me.LabHelpStatus.Text = "labHelpStatus"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(25, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(76, 17)
        Me.Label3.TabIndex = 77
        Me.Label3.Text = "Customer:"
        '
        'LabJobStatus
        '
        Me.LabJobStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.LabJobStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabJobStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabJobStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobStatus.Location = New System.Drawing.Point(553, 46)
        Me.LabJobStatus.Name = "LabJobStatus"
        Me.LabJobStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabJobStatus.Size = New System.Drawing.Size(168, 25)
        Me.LabJobStatus.TabIndex = 8
        Me.LabJobStatus.Text = "LabJobStatus"
        '
        'LabVersion
        '
        Me.LabVersion.BackColor = System.Drawing.Color.Transparent
        Me.LabVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabVersion.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabVersion.Location = New System.Drawing.Point(4, 84)
        Me.LabVersion.Name = "LabVersion"
        Me.LabVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabVersion.Size = New System.Drawing.Size(143, 13)
        Me.LabVersion.TabIndex = 6
        Me.LabVersion.Text = "LabVersion"
        '
        'LabDateRcvd
        '
        Me.LabDateRcvd.AutoSize = True
        Me.LabDateRcvd.BackColor = System.Drawing.Color.Transparent
        Me.LabDateRcvd.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDateRcvd.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDateRcvd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDateRcvd.Location = New System.Drawing.Point(184, 37)
        Me.LabDateRcvd.Name = "LabDateRcvd"
        Me.LabDateRcvd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDateRcvd.Size = New System.Drawing.Size(71, 13)
        Me.LabDateRcvd.TabIndex = 5
        Me.LabDateRcvd.Text = "LabDateRcvd"
        '
        'LabRcvdBy
        '
        Me.LabRcvdBy.BackColor = System.Drawing.Color.Transparent
        Me.LabRcvdBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRcvdBy.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRcvdBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRcvdBy.Location = New System.Drawing.Point(21, 37)
        Me.LabRcvdBy.Name = "LabRcvdBy"
        Me.LabRcvdBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRcvdBy.Size = New System.Drawing.Size(91, 17)
        Me.LabRcvdBy.TabIndex = 4
        Me.LabRcvdBy.Text = " Received By:"
        '
        'LabTicket
        '
        Me.LabTicket.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.LabTicket.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabTicket.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabTicket.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabTicket.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabTicket.Location = New System.Drawing.Point(553, 3)
        Me.LabTicket.Name = "LabTicket"
        Me.LabTicket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabTicket.Size = New System.Drawing.Size(215, 28)
        Me.LabTicket.TabIndex = 3
        Me.LabTicket.Text = "labTicket"
        Me.LabTicket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabHdr2
        '
        Me.LabHdr2.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr2.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr2.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr2.Location = New System.Drawing.Point(9, 22)
        Me.LabHdr2.Name = "LabHdr2"
        Me.LabHdr2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr2.Size = New System.Drawing.Size(132, 36)
        Me.LabHdr2.TabIndex = 1
        Me.LabHdr2.Text = " "
        '
        'LabHdr1
        '
        Me.LabHdr1.AutoSize = True
        Me.LabHdr1.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr1.Location = New System.Drawing.Point(107, 3)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr1.Size = New System.Drawing.Size(211, 23)
        Me.LabHdr1.TabIndex = 0
        Me.LabHdr1.Text = "New Service Agreement"
        '
        'OptLogin
        '
        '
        'chkExtras
        '
        '
        'chkPrtDocs
        '
        '
        'optQuotation
        '
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(28, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(62, 19)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 79
        Me.PictureBox1.TabStop = False
        '
        'openDlg1
        '
        Me.openDlg1.FileName = "OpenFileDialog1"
        '
        'picSubjectMain
        '
        Me.picSubjectMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.picSubjectMain.Location = New System.Drawing.Point(285, 54)
        Me.picSubjectMain.Name = "picSubjectMain"
        Me.picSubjectMain.Size = New System.Drawing.Size(15, 17)
        Me.picSubjectMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSubjectMain.TabIndex = 104
        Me.picSubjectMain.TabStop = False
        '
        'grpBoxBooking
        '
        Me.grpBoxBooking.BackColor = System.Drawing.Color.LightYellow
        Me.grpBoxBooking.Controls.Add(Me.chkBooking)
        Me.grpBoxBooking.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxBooking.Location = New System.Drawing.Point(334, 30)
        Me.grpBoxBooking.Name = "grpBoxBooking"
        Me.grpBoxBooking.Size = New System.Drawing.Size(117, 89)
        Me.grpBoxBooking.TabIndex = 105
        Me.grpBoxBooking.TabStop = False
        Me.grpBoxBooking.Text = "grpBoxBooking"
        '
        'chkBooking
        '
        Me.chkBooking.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkBooking.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBooking.ForeColor = System.Drawing.Color.DarkBlue
        Me.chkBooking.Location = New System.Drawing.Point(7, 17)
        Me.chkBooking.Name = "chkBooking"
        Me.chkBooking.Size = New System.Drawing.Size(107, 53)
        Me.chkBooking.TabIndex = 0
        Me.chkBooking.Text = "Check Here if Booking-in only."
        Me.chkBooking.UseVisualStyleBackColor = False
        '
        'panelSave
        '
        Me.panelSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.panelSave.Controls.Add(Me.cmdFinish)
        Me.panelSave.Controls.Add(Me.cmdPrintAll)
        Me.panelSave.Controls.Add(Me.cmdCancel)
        Me.panelSave.Location = New System.Drawing.Point(637, 586)
        Me.panelSave.Name = "panelSave"
        Me.panelSave.Size = New System.Drawing.Size(131, 100)
        Me.panelSave.TabIndex = 6
        '
        'panelVersion
        '
        Me.panelVersion.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.panelVersion.Controls.Add(Me.LabHdr2)
        Me.panelVersion.Controls.Add(Me.LabVersion)
        Me.panelVersion.Location = New System.Drawing.Point(24, 586)
        Me.panelVersion.Name = "panelVersion"
        Me.panelVersion.Size = New System.Drawing.Size(151, 100)
        Me.panelVersion.TabIndex = 4
        '
        'frmNewJob32
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(785, 698)
        Me.Controls.Add(Me.panelVersion)
        Me.Controls.Add(Me.panelSave)
        Me.Controls.Add(Me.grpBoxBooking)
        Me.Controls.Add(Me.picSubjectMain)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtCustomer)
        Me.Controls.Add(Me.SSTabNewJob)
        Me.Controls.Add(Me.chkRefreshCustomer)
        Me.Controls.Add(Me.FramePrintOpts)
        Me.Controls.Add(Me.picUserLogo)
        Me.Controls.Add(Me.cmdCancelJob)
        Me.Controls.Add(Me.txtRcvdName)
        Me.Controls.Add(Me.LabHelpStatus)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LabJobStatus)
        Me.Controls.Add(Me.LabDateRcvd)
        Me.Controls.Add(Me.LabRcvdBy)
        Me.Controls.Add(Me.LabTicket)
        Me.Controls.Add(Me.LabHdr1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNewJob32"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Accept New Job"
        Me.FramePrintOpts.ResumeLayout(False)
        CType(Me.NumUpDownLabels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSubjectItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numUpDownOnSiteDuration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSTabNewJob.ResumeLayout(False)
        Me._SSTabNewJob_TabPage0.ResumeLayout(False)
        Me.FrameGoods.ResumeLayout(False)
        Me.FrameGoods.PerformLayout()
        Me.FrameUsers.ResumeLayout(False)
        Me.FrameUsers.PerformLayout()
        Me.grpBoxItemPic.ResumeLayout(False)
        Me._SSTabNewJob_TabPage1.ResumeLayout(False)
        Me.frameProblem.ResumeLayout(False)
        Me.frameProblem.PerformLayout()
        Me._SSTabNewJob_TabPage2.ResumeLayout(False)
        Me.frameInstructions.ResumeLayout(False)
        Me.frameInstructions.PerformLayout()
        CType(Me.picUserLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptLogin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkExtras, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPrtDocs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optQuotation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPassword, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSubjectMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxBooking.ResumeLayout(False)
        Me.panelSave.ResumeLayout(False)
        Me.panelVersion.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Public WithEvents txtNomTech As System.Windows.Forms.TextBox
    Public WithEvents txtTechName As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboPrevGoods As System.Windows.Forms.ComboBox
    Friend WithEvents labSelectPrevious As System.Windows.Forms.Label
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents txtGoodsList As System.Windows.Forms.TextBox
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    Friend WithEvents cmdNavBack As System.Windows.Forms.Button
    Friend WithEvents datePromised As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents _SSTabNewJob_TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents frameProblem As System.Windows.Forms.GroupBox
    Friend WithEvents labProblemRequired As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents listSymptoms As System.Windows.Forms.CheckedListBox
    Public WithEvents cmdEditSymptoms As System.Windows.Forms.Button
    Public WithEvents txtProblem As System.Windows.Forms.TextBox
    Public WithEvents txtSymptoms As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents LabProblem As System.Windows.Forms.Label
    Friend WithEvents cmdNext2 As System.Windows.Forms.Button
    Friend WithEvents cmdNavBackToStart As System.Windows.Forms.Button
    Friend WithEvents labInstructionRequired As System.Windows.Forms.Label
    Friend WithEvents labGoodsInfoRequired As System.Windows.Forms.Label
    Friend WithEvents labLogonRequired As System.Windows.Forms.Label
    Friend WithEvents labLine3 As System.Windows.Forms.Label
    Friend WithEvents labPriorityRequired As System.Windows.Forms.Label
    Friend WithEvents labOnSiteTime As System.Windows.Forms.Label
    Friend WithEvents timeOnSite As System.Windows.Forms.DateTimePicker
    Public WithEvents labOnSiteHdrP3 As System.Windows.Forms.Label
    Friend WithEvents labOnSiteFields As System.Windows.Forms.Label
    Friend WithEvents cmdGoodsClear As System.Windows.Forms.Button
    Friend WithEvents NumUpDownLabels As System.Windows.Forms.NumericUpDown
    Friend WithEvents openDlg1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents grpBoxItemPic As System.Windows.Forms.GroupBox
    Friend WithEvents picSubjectItem As System.Windows.Forms.PictureBox
    Friend WithEvents picSubjectMain As System.Windows.Forms.PictureBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboColourPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboLabelPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboReceiptPrinters As System.Windows.Forms.ComboBox
    Public WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents grpBoxBooking As System.Windows.Forms.GroupBox
    Friend WithEvents chkBooking As System.Windows.Forms.CheckBox
    Friend WithEvents panelSave As System.Windows.Forms.Panel
    Friend WithEvents panelVersion As System.Windows.Forms.Panel
    Friend WithEvents chkSystemUnderWarranty As System.Windows.Forms.CheckBox
    Friend WithEvents chkReturned As System.Windows.Forms.CheckBox
    Friend WithEvents btnLookupStaff As System.Windows.Forms.Button
    Friend WithEvents numUpDownOnSiteDuration As System.Windows.Forms.NumericUpDown
    Friend WithEvents labOnSiteDuration As System.Windows.Forms.Label
    Friend WithEvents labOnSiteDurationWarning As System.Windows.Forms.Label
#End Region
End Class