<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class ucChildJobMaint
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
    Public WithEvents PicSkipped As System.Windows.Forms.PictureBox
	Public WithEvents PicQueried As System.Windows.Forms.PictureBox
	Public WithEvents PicChecked As System.Windows.Forms.PictureBox
    Public WithEvents chkMyJob As System.Windows.Forms.CheckBox
	Public WithEvents txtNomTech As System.Windows.Forms.TextBox
    Public WithEvents PictureExtraPrint As System.Windows.Forms.PictureBox
    Public WithEvents Picture2 As System.Windows.Forms.PictureBox
    Public WithEvents txtWorkDetails As System.Windows.Forms.TextBox
    Public WithEvents txtWorkHistory As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents FrameWorkRecord As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPageWork As System.Windows.Forms.TabPage
    Public WithEvents _cmdDeletePart_1 As System.Windows.Forms.Button
    Public WithEvents _cmdAddPart_1 As System.Windows.Forms.Button
    Public WithEvents _ListViewParts_1 As System.Windows.Forms.ListView
    Public WithEvents LabChecklist As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents FrameServiceItems As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPageAttachments As System.Windows.Forms.TabPage
    Public WithEvents PictureExtra As System.Windows.Forms.PictureBox
    Public WithEvents _cmdDeletePart_0 As System.Windows.Forms.Button
    Public WithEvents _cmdAddPart_0 As System.Windows.Forms.Button
    Public WithEvents _ListViewParts_0 As System.Windows.Forms.ListView
    Public WithEvents LabPriceUpdated As System.Windows.Forms.Label
    Public WithEvents LabExtra As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents FrameStockItems As System.Windows.Forms.GroupBox
    Public WithEvents txtStaffSessions As System.Windows.Forms.TextBox
    Public WithEvents CmdCreateTask As System.Windows.Forms.Button
    Public WithEvents cmdDeleteTask As System.Windows.Forms.Button
    Public WithEvents cmdAddTask As System.Windows.Forms.Button
    Public WithEvents ListTaskTypes As System.Windows.Forms.CheckedListBox
    Public WithEvents ListViewTasks As System.Windows.Forms.ListView
    Public WithEvents LabTotalTime As System.Windows.Forms.Label
    Public WithEvents labNCMsg As System.Windows.Forms.Label
    Public WithEvents LabStaffSessions As System.Windows.Forms.Label
    Public WithEvents LabTasksDone As System.Windows.Forms.Label
    Public WithEvents FrameOtherTasks As System.Windows.Forms.GroupBox
    Public WithEvents txtNotification As System.Windows.Forms.TextBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents FrameNotified As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPageNotif As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents _optChargeable_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optChargeable_0 As System.Windows.Forms.RadioButton
    Public WithEvents cboTimeSelectHours As System.Windows.Forms.ComboBox
    Public WithEvents LabelTimeSelect As System.Windows.Forms.Label
    Public WithEvents FrameSessionTime As System.Windows.Forms.GroupBox
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents Picture1 As System.Windows.Forms.PictureBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents LabVersion As System.Windows.Forms.Label
    Public WithEvents LabTicket As System.Windows.Forms.Label
    Public WithEvents LabHdr1 As System.Windows.Forms.Label
    Public WithEvents ListViewParts As Microsoft.VisualBasic.Compatibility.VB6.ListViewArray
    Public WithEvents chkPrtDocs As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    Public WithEvents cmdAddPart As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Public WithEvents cmdDeletePart As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Public WithEvents optChargeable As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtStaffName As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucChildJobMaint))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me._cmdDeletePart_1 = New System.Windows.Forms.Button()
        Me._cmdDeletePart_0 = New System.Windows.Forms.Button()
        Me.CmdCreateTask = New System.Windows.Forms.Button()
        Me.cmdAddTask = New System.Windows.Forms.Button()
        Me._SSTab1_TabPageWork = New System.Windows.Forms.TabPage()
        Me.ssTabWork = New System.Windows.Forms.TabControl()
        Me.TabWorkPage_notes = New System.Windows.Forms.TabPage()
        Me.FrameWorkRecord = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDiagnosis = New System.Windows.Forms.TextBox()
        Me.txtWorkDetails = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabComments = New System.Windows.Forms.Label()
        Me.txtWorkHistory = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabWorkPage_service = New System.Windows.Forms.TabPage()
        Me.FrameServiceItems = New System.Windows.Forms.GroupBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.dgvChecklist = New System.Windows.Forms.DataGridView()
        Me.Symbol = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Comments = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateUpdated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Staff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrevStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrevComments = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me._cmdAddPart_1 = New System.Windows.Forms.Button()
        Me._ListViewParts_1 = New System.Windows.Forms.ListView()
        Me.LabChecklist = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabWorkPage_stock = New System.Windows.Forms.TabPage()
        Me.FrameStockItems = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.PictureExtra = New System.Windows.Forms.PictureBox()
        Me._cmdAddPart_0 = New System.Windows.Forms.Button()
        Me._ListViewParts_0 = New System.Windows.Forms.ListView()
        Me.LabPriceUpdated = New System.Windows.Forms.Label()
        Me.LabExtra = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TabWorkPage_Tasks = New System.Windows.Forms.TabPage()
        Me.FrameOtherTasks = New System.Windows.Forms.GroupBox()
        Me.cmdClearSessionTimes = New System.Windows.Forms.Button()
        Me.txtStaffSessions = New System.Windows.Forms.TextBox()
        Me.cmdDeleteTask = New System.Windows.Forms.Button()
        Me.ListTaskTypes = New System.Windows.Forms.CheckedListBox()
        Me.ListViewTasks = New System.Windows.Forms.ListView()
        Me.LabTotalTime = New System.Windows.Forms.Label()
        Me.labNCMsg = New System.Windows.Forms.Label()
        Me.LabStaffSessions = New System.Windows.Forms.Label()
        Me.LabTasksDone = New System.Windows.Forms.Label()
        Me._SSTab1_TabPageAttachments = New System.Windows.Forms.TabPage()
        Me.grpBoxItem = New System.Windows.Forms.GroupBox()
        Me.picMsExcel = New System.Windows.Forms.PictureBox()
        Me.picMsWord = New System.Windows.Forms.PictureBox()
        Me.picPDF = New System.Windows.Forms.PictureBox()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.btnViewDoc = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.picProduct = New System.Windows.Forms.PictureBox()
        Me.lvwDocs = New System.Windows.Forms.ListView()
        Me.doc_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.doc_date_created = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.doc_file_title = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.doc_file_size = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.doc_staff = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.grpBoxAddNew = New System.Windows.Forms.GroupBox()
        Me.txtNewFileName = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.labHelp = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.btnSaveAttachment = New System.Windows.Forms.Button()
        Me.txtNewComment = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me._SSTab1_TabPageNotif = New System.Windows.Forms.TabPage()
        Me.FrameNotified = New System.Windows.Forms.GroupBox()
        Me.txtNotification = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmdFinish = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.labTaskNeeded = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me._chkPrtDocs_1 = New System.Windows.Forms.CheckBox()
        Me._chkPrtDocs_0 = New System.Windows.Forms.CheckBox()
        Me._chkPrtDocs_Report = New System.Windows.Forms.CheckBox()
        Me.cboTimeSelectTenths = New System.Windows.Forms.ComboBox()
        Me.cboTimeSelectHours = New System.Windows.Forms.ComboBox()
        Me.cboReceiptPrinters = New System.Windows.Forms.ComboBox()
        Me.cboColourPrinters = New System.Windows.Forms.ComboBox()
        Me.chkPrintItemBarcodes = New System.Windows.Forms.CheckBox()
        Me.PicSkipped = New System.Windows.Forms.PictureBox()
        Me.PicQueried = New System.Windows.Forms.PictureBox()
        Me.PicChecked = New System.Windows.Forms.PictureBox()
        Me.chkMyJob = New System.Windows.Forms.CheckBox()
        Me.txtNomTech = New System.Windows.Forms.TextBox()
        Me.PictureExtraPrint = New System.Windows.Forms.PictureBox()
        Me.Picture2 = New System.Windows.Forms.PictureBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPageJob = New System.Windows.Forms.TabPage()
        Me.frameProblem = New System.Windows.Forms.GroupBox()
        Me.labPriority = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ChkRecovDisk = New System.Windows.Forms.CheckBox()
        Me.ChkBackupReq = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSymptoms = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.labInstructions = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtGoods = New System.Windows.Forms.TextBox()
        Me.frameJobDates = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.LabJobReturned = New System.Windows.Forms.Label()
        Me.chkReturned = New System.Windows.Forms.CheckBox()
        Me._txtStaffName_1 = New System.Windows.Forms.TextBox()
        Me._txtStaffName_0 = New System.Windows.Forms.TextBox()
        Me.labDeliveredBy = New System.Windows.Forms.Label()
        Me.labDateUpdated = New System.Windows.Forms.Label()
        Me.LabRcvdStaff = New System.Windows.Forms.Label()
        Me.labDateCreated = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.FrameQuotation = New System.Windows.Forms.GroupBox()
        Me.ListViewExtraParts = New System.Windows.Forms.ListView()
        Me.ListViewQuote = New System.Windows.Forms.ListView()
        Me.LabReconciliation = New System.Windows.Forms.Label()
        Me.LabExtraParts = New System.Windows.Forms.Label()
        Me.LabQuotedParts = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.FrameSessionTime = New System.Windows.Forms.GroupBox()
        Me.labMinCharge = New System.Windows.Forms.Label()
        Me.labResultHours = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.labFullCost = New System.Windows.Forms.Label()
        Me._optChargeable_1 = New System.Windows.Forms.RadioButton()
        Me._optChargeable_0 = New System.Windows.Forms.RadioButton()
        Me.LabelTimeSelect = New System.Windows.Forms.Label()
        Me.LabVersion = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Picture1 = New System.Windows.Forms.PictureBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.LabTicket = New System.Windows.Forms.Label()
        Me.LabHdr1 = New System.Windows.Forms.Label()
        Me.ListViewParts = New Microsoft.VisualBasic.Compatibility.VB6.ListViewArray(Me.components)
        Me.chkPrtDocs = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.cmdAddPart = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.cmdDeletePart = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.optChargeable = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtStaffName = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.labJobMatix = New System.Windows.Forms.Label()
        Me.labJobStatus = New System.Windows.Forms.Label()
        Me.FramePrintOpts = New System.Windows.Forms.GroupBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.optCompleted = New System.Windows.Forms.RadioButton()
        Me.optQA = New System.Windows.Forms.RadioButton()
        Me.optSuspend = New System.Windows.Forms.RadioButton()
        Me.optSaveExit = New System.Windows.Forms.RadioButton()
        Me.cmdCustHistory = New System.Windows.Forms.Button()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.openDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.labWarrantyJob = New System.Windows.Forms.Label()
        Me.picSubjectMain = New System.Windows.Forms.PictureBox()
        Me._SSTab1_TabPageWork.SuspendLayout()
        Me.ssTabWork.SuspendLayout()
        Me.TabWorkPage_notes.SuspendLayout()
        Me.FrameWorkRecord.SuspendLayout()
        Me.TabWorkPage_service.SuspendLayout()
        Me.FrameServiceItems.SuspendLayout()
        CType(Me.dgvChecklist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabWorkPage_stock.SuspendLayout()
        Me.FrameStockItems.SuspendLayout()
        CType(Me.PictureExtra, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabWorkPage_Tasks.SuspendLayout()
        Me.FrameOtherTasks.SuspendLayout()
        Me._SSTab1_TabPageAttachments.SuspendLayout()
        Me.grpBoxItem.SuspendLayout()
        CType(Me.picMsExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMsWord, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxAddNew.SuspendLayout()
        Me._SSTab1_TabPageNotif.SuspendLayout()
        Me.FrameNotified.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicSkipped, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicQueried, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicChecked, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureExtraPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPageJob.SuspendLayout()
        Me.frameProblem.SuspendLayout()
        Me.frameJobDates.SuspendLayout()
        Me.FrameQuotation.SuspendLayout()
        Me.FrameSessionTime.SuspendLayout()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ListViewParts, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkPrtDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdAddPart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdDeletePart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optChargeable, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtStaffName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FramePrintOpts.SuspendLayout()
        CType(Me.picSubjectMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        '_cmdDeletePart_1
        '
        Me._cmdDeletePart_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._cmdDeletePart_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdDeletePart_1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdDeletePart_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeletePart.SetIndex(Me._cmdDeletePart_1, CType(1, Short))
        Me._cmdDeletePart_1.Location = New System.Drawing.Point(352, 12)
        Me._cmdDeletePart_1.Name = "_cmdDeletePart_1"
        Me._cmdDeletePart_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdDeletePart_1.Size = New System.Drawing.Size(57, 23)
        Me._cmdDeletePart_1.TabIndex = 18
        Me._cmdDeletePart_1.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me._cmdDeletePart_1, " DELETE selected item.")
        Me._cmdDeletePart_1.UseVisualStyleBackColor = False
        '
        '_cmdDeletePart_0
        '
        Me._cmdDeletePart_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._cmdDeletePart_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdDeletePart_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeletePart.SetIndex(Me._cmdDeletePart_0, CType(0, Short))
        Me._cmdDeletePart_0.Location = New System.Drawing.Point(352, 16)
        Me._cmdDeletePart_0.Name = "_cmdDeletePart_0"
        Me._cmdDeletePart_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdDeletePart_0.Size = New System.Drawing.Size(57, 23)
        Me._cmdDeletePart_0.TabIndex = 22
        Me._cmdDeletePart_0.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me._cmdDeletePart_0, " DELETE selected item.")
        Me._cmdDeletePart_0.UseVisualStyleBackColor = False
        '
        'CmdCreateTask
        '
        Me.CmdCreateTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.CmdCreateTask.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdCreateTask.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdCreateTask.Location = New System.Drawing.Point(455, 29)
        Me.CmdCreateTask.Name = "CmdCreateTask"
        Me.CmdCreateTask.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdCreateTask.Size = New System.Drawing.Size(100, 21)
        Me.CmdCreateTask.TabIndex = 2
        Me.CmdCreateTask.Text = "Edit Tasks Ref.."
        Me.ToolTip1.SetToolTip(Me.CmdCreateTask, "Create new Task-type Definition.")
        Me.CmdCreateTask.UseVisualStyleBackColor = False
        '
        'cmdAddTask
        '
        Me.cmdAddTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.cmdAddTask.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAddTask.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAddTask.Location = New System.Drawing.Point(176, 29)
        Me.cmdAddTask.Name = "cmdAddTask"
        Me.cmdAddTask.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAddTask.Size = New System.Drawing.Size(70, 21)
        Me.cmdAddTask.TabIndex = 0
        Me.cmdAddTask.Text = "Add Task"
        Me.ToolTip1.SetToolTip(Me.cmdAddTask, "Add standard task to this job.")
        Me.cmdAddTask.UseVisualStyleBackColor = False
        '
        '_SSTab1_TabPageWork
        '
        Me._SSTab1_TabPageWork.Controls.Add(Me.ssTabWork)
        Me._SSTab1_TabPageWork.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPageWork.Name = "_SSTab1_TabPageWork"
        Me._SSTab1_TabPageWork.Size = New System.Drawing.Size(760, 506)
        Me._SSTab1_TabPageWork.TabIndex = 0
        Me._SSTab1_TabPageWork.Text = "Work Record"
        Me.ToolTip1.SetToolTip(Me._SSTab1_TabPageWork, "Job Work details and History")
        Me._SSTab1_TabPageWork.UseVisualStyleBackColor = True
        '
        'ssTabWork
        '
        Me.ssTabWork.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.ssTabWork.Controls.Add(Me.TabWorkPage_notes)
        Me.ssTabWork.Controls.Add(Me.TabWorkPage_service)
        Me.ssTabWork.Controls.Add(Me.TabWorkPage_stock)
        Me.ssTabWork.Controls.Add(Me.TabWorkPage_Tasks)
        Me.ssTabWork.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ssTabWork.Location = New System.Drawing.Point(2, 4)
        Me.ssTabWork.Margin = New System.Windows.Forms.Padding(2)
        Me.ssTabWork.Name = "ssTabWork"
        Me.ssTabWork.SelectedIndex = 0
        Me.ssTabWork.Size = New System.Drawing.Size(752, 490)
        Me.ssTabWork.TabIndex = 80
        '
        'TabWorkPage_notes
        '
        Me.TabWorkPage_notes.Controls.Add(Me.FrameWorkRecord)
        Me.TabWorkPage_notes.Location = New System.Drawing.Point(4, 26)
        Me.TabWorkPage_notes.Name = "TabWorkPage_notes"
        Me.TabWorkPage_notes.Padding = New System.Windows.Forms.Padding(3)
        Me.TabWorkPage_notes.Size = New System.Drawing.Size(744, 460)
        Me.TabWorkPage_notes.TabIndex = 0
        Me.TabWorkPage_notes.Text = "Work Notes"
        Me.TabWorkPage_notes.UseVisualStyleBackColor = True
        '
        'FrameWorkRecord
        '
        Me.FrameWorkRecord.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameWorkRecord.Controls.Add(Me.Label7)
        Me.FrameWorkRecord.Controls.Add(Me.txtDiagnosis)
        Me.FrameWorkRecord.Controls.Add(Me.txtWorkDetails)
        Me.FrameWorkRecord.Controls.Add(Me.Label1)
        Me.FrameWorkRecord.Controls.Add(Me.LabComments)
        Me.FrameWorkRecord.Controls.Add(Me.txtWorkHistory)
        Me.FrameWorkRecord.Controls.Add(Me.Label3)
        Me.FrameWorkRecord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameWorkRecord.Location = New System.Drawing.Point(2, 3)
        Me.FrameWorkRecord.Name = "FrameWorkRecord"
        Me.FrameWorkRecord.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameWorkRecord.Size = New System.Drawing.Size(736, 451)
        Me.FrameWorkRecord.TabIndex = 95
        Me.FrameWorkRecord.TabStop = False
        Me.FrameWorkRecord.Text = "FrameWorkRecord"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(14, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.Label7.Size = New System.Drawing.Size(256, 73)
        Me.Label7.TabIndex = 107
        Me.Label7.Text = "Update the notes on diagnosis and work done as the job progresses.." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Note that" &
    " for JobMatixPOS shops, the Diagnosis and Job-Report text will be printed on the" &
    " Sales Invoice for the Job."
        '
        'txtDiagnosis
        '
        Me.txtDiagnosis.AcceptsReturn = True
        Me.txtDiagnosis.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtDiagnosis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiagnosis.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDiagnosis.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiagnosis.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDiagnosis.Location = New System.Drawing.Point(402, 36)
        Me.txtDiagnosis.MaxLength = 548
        Me.txtDiagnosis.Multiline = True
        Me.txtDiagnosis.Name = "txtDiagnosis"
        Me.txtDiagnosis.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDiagnosis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDiagnosis.Size = New System.Drawing.Size(321, 119)
        Me.txtDiagnosis.TabIndex = 14
        Me.txtDiagnosis.Text = "txtDiagnosis"
        '
        'txtWorkDetails
        '
        Me.txtWorkDetails.AcceptsReturn = True
        Me.txtWorkDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtWorkDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWorkDetails.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWorkDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkDetails.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWorkDetails.Location = New System.Drawing.Point(402, 190)
        Me.txtWorkDetails.MaxLength = 3000
        Me.txtWorkDetails.Multiline = True
        Me.txtWorkDetails.Name = "txtWorkDetails"
        Me.txtWorkDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWorkDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWorkDetails.Size = New System.Drawing.Size(321, 255)
        Me.txtWorkDetails.TabIndex = 15
        Me.txtWorkDetails.Text = "txtWorkDetails"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(401, 172)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(284, 15)
        Me.Label1.TabIndex = 100
        Me.Label1.Text = "Add Work Details here..   (This Session)"
        '
        'LabComments
        '
        Me.LabComments.BackColor = System.Drawing.Color.Transparent
        Me.LabComments.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabComments.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabComments.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabComments.Location = New System.Drawing.Point(401, 18)
        Me.LabComments.Name = "LabComments"
        Me.LabComments.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabComments.Size = New System.Drawing.Size(227, 15)
        Me.LabComments.TabIndex = 106
        Me.LabComments.Text = "Diagnosis and Customer Job Report:"
        '
        'txtWorkHistory
        '
        Me.txtWorkHistory.AcceptsReturn = True
        Me.txtWorkHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtWorkHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWorkHistory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWorkHistory.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkHistory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWorkHistory.Location = New System.Drawing.Point(11, 120)
        Me.txtWorkHistory.MaxLength = 0
        Me.txtWorkHistory.Multiline = True
        Me.txtWorkHistory.Name = "txtWorkHistory"
        Me.txtWorkHistory.ReadOnly = True
        Me.txtWorkHistory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWorkHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWorkHistory.Size = New System.Drawing.Size(366, 325)
        Me.txtWorkHistory.TabIndex = 16
        Me.txtWorkHistory.Text = "txtWorkHistory"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(211, 14)
        Me.Label3.TabIndex = 98
        Me.Label3.Text = "Previous Work History to date.."
        '
        'TabWorkPage_service
        '
        Me.TabWorkPage_service.Controls.Add(Me.FrameServiceItems)
        Me.TabWorkPage_service.Location = New System.Drawing.Point(4, 26)
        Me.TabWorkPage_service.Name = "TabWorkPage_service"
        Me.TabWorkPage_service.Padding = New System.Windows.Forms.Padding(3)
        Me.TabWorkPage_service.Size = New System.Drawing.Size(744, 460)
        Me.TabWorkPage_service.TabIndex = 1
        Me.TabWorkPage_service.Text = "Service Items"
        Me.TabWorkPage_service.UseVisualStyleBackColor = True
        '
        'FrameServiceItems
        '
        Me.FrameServiceItems.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameServiceItems.Controls.Add(Me.Label23)
        Me.FrameServiceItems.Controls.Add(Me.dgvChecklist)
        Me.FrameServiceItems.Controls.Add(Me._cmdDeletePart_1)
        Me.FrameServiceItems.Controls.Add(Me._cmdAddPart_1)
        Me.FrameServiceItems.Controls.Add(Me._ListViewParts_1)
        Me.FrameServiceItems.Controls.Add(Me.LabChecklist)
        Me.FrameServiceItems.Controls.Add(Me.Label4)
        Me.FrameServiceItems.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameServiceItems.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameServiceItems.Location = New System.Drawing.Point(3, 2)
        Me.FrameServiceItems.Margin = New System.Windows.Forms.Padding(2)
        Me.FrameServiceItems.Name = "FrameServiceItems"
        Me.FrameServiceItems.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameServiceItems.Size = New System.Drawing.Size(736, 439)
        Me.FrameServiceItems.TabIndex = 87
        Me.FrameServiceItems.TabStop = False
        Me.FrameServiceItems.Text = "FrameServiceItems"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Font = New System.Drawing.Font("Comic Sans MS", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(426, 16)
        Me.Label23.Name = "Label23"
        Me.Label23.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label23.Size = New System.Drawing.Size(233, 17)
        Me.Label23.TabIndex = 108
        Me.Label23.Text = "NB: Cost And Sell Prices include Tax."
        '
        'dgvChecklist
        '
        Me.dgvChecklist.AllowUserToAddRows = False
        Me.dgvChecklist.AllowUserToDeleteRows = False
        Me.dgvChecklist.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvChecklist.ColumnHeadersHeight = 25
        Me.dgvChecklist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Symbol, Me.Description, Me.Status, Me.Comments, Me.DateUpdated, Me.Staff, Me.PrevStatus, Me.PrevComments})
        Me.dgvChecklist.GridColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvChecklist.Location = New System.Drawing.Point(8, 214)
        Me.dgvChecklist.MultiSelect = False
        Me.dgvChecklist.Name = "dgvChecklist"
        Me.dgvChecklist.Size = New System.Drawing.Size(722, 208)
        Me.dgvChecklist.TabIndex = 20
        '
        'Symbol
        '
        Me.Symbol.HeaderText = "Status"
        Me.Symbol.Name = "Symbol"
        Me.Symbol.ReadOnly = True
        Me.Symbol.Width = 38
        '
        'Description
        '
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Description.DefaultCellStyle = DataGridViewCellStyle1
        Me.Description.HeaderText = "Task Description"
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        Me.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Status
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Status.DefaultCellStyle = DataGridViewCellStyle2
        Me.Status.HeaderText = "Task Status"
        Me.Status.Name = "Status"
        Me.Status.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Status.ToolTipText = "Space or Dbl-click to change status.."
        Me.Status.Width = 80
        '
        'Comments
        '
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Comments.DefaultCellStyle = DataGridViewCellStyle3
        Me.Comments.HeaderText = "Your Comments"
        Me.Comments.Name = "Comments"
        Me.Comments.Width = 130
        '
        'DateUpdated
        '
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateUpdated.DefaultCellStyle = DataGridViewCellStyle4
        Me.DateUpdated.HeaderText = "Date Updated"
        Me.DateUpdated.Name = "DateUpdated"
        Me.DateUpdated.ReadOnly = True
        '
        'Staff
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Staff.DefaultCellStyle = DataGridViewCellStyle5
        Me.Staff.HeaderText = "Staff"
        Me.Staff.Name = "Staff"
        Me.Staff.ReadOnly = True
        '
        'PrevStatus
        '
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrevStatus.DefaultCellStyle = DataGridViewCellStyle6
        Me.PrevStatus.HeaderText = "Prev. Status"
        Me.PrevStatus.Name = "PrevStatus"
        Me.PrevStatus.ReadOnly = True
        '
        'PrevComments
        '
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrevComments.DefaultCellStyle = DataGridViewCellStyle7
        Me.PrevComments.HeaderText = "Prev. Comments"
        Me.PrevComments.Name = "PrevComments"
        Me.PrevComments.ReadOnly = True
        '
        '_cmdAddPart_1
        '
        Me._cmdAddPart_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._cmdAddPart_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdAddPart_1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdAddPart_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAddPart.SetIndex(Me._cmdAddPart_1, CType(1, Short))
        Me._cmdAddPart_1.Location = New System.Drawing.Point(208, 12)
        Me._cmdAddPart_1.Name = "_cmdAddPart_1"
        Me._cmdAddPart_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdAddPart_1.Size = New System.Drawing.Size(122, 23)
        Me._cmdAddPart_1.TabIndex = 17
        Me._cmdAddPart_1.Text = "Add Service Charge"
        Me._cmdAddPart_1.UseVisualStyleBackColor = False
        '
        '_ListViewParts_1
        '
        Me._ListViewParts_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me._ListViewParts_1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ListViewParts_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me._ListViewParts_1.FullRowSelect = True
        Me._ListViewParts_1.GridLines = True
        Me._ListViewParts_1.HideSelection = False
        Me.ListViewParts.SetIndex(Me._ListViewParts_1, CType(1, Short))
        Me._ListViewParts_1.Location = New System.Drawing.Point(8, 40)
        Me._ListViewParts_1.MultiSelect = False
        Me._ListViewParts_1.Name = "_ListViewParts_1"
        Me._ListViewParts_1.Size = New System.Drawing.Size(722, 139)
        Me._ListViewParts_1.TabIndex = 19
        Me._ListViewParts_1.UseCompatibleStateImageBehavior = False
        Me._ListViewParts_1.View = System.Windows.Forms.View.Details
        '
        'LabChecklist
        '
        Me.LabChecklist.BackColor = System.Drawing.Color.Transparent
        Me.LabChecklist.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabChecklist.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabChecklist.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabChecklist.Location = New System.Drawing.Point(9, 196)
        Me.LabChecklist.Name = "LabChecklist"
        Me.LabChecklist.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabChecklist.Size = New System.Drawing.Size(473, 13)
        Me.LabChecklist.TabIndex = 92
        Me.LabChecklist.Text = "Service Checklist"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(153, 17)
        Me.Label4.TabIndex = 88
        Me.Label4.Text = "Service Charge Items"
        '
        'TabWorkPage_stock
        '
        Me.TabWorkPage_stock.Controls.Add(Me.FrameStockItems)
        Me.TabWorkPage_stock.Location = New System.Drawing.Point(4, 26)
        Me.TabWorkPage_stock.Name = "TabWorkPage_stock"
        Me.TabWorkPage_stock.Size = New System.Drawing.Size(744, 460)
        Me.TabWorkPage_stock.TabIndex = 2
        Me.TabWorkPage_stock.Text = "Stock Items"
        Me.TabWorkPage_stock.UseVisualStyleBackColor = True
        '
        'FrameStockItems
        '
        Me.FrameStockItems.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameStockItems.Controls.Add(Me.Label19)
        Me.FrameStockItems.Controls.Add(Me.PictureExtra)
        Me.FrameStockItems.Controls.Add(Me._cmdDeletePart_0)
        Me.FrameStockItems.Controls.Add(Me._cmdAddPart_0)
        Me.FrameStockItems.Controls.Add(Me._ListViewParts_0)
        Me.FrameStockItems.Controls.Add(Me.LabPriceUpdated)
        Me.FrameStockItems.Controls.Add(Me.LabExtra)
        Me.FrameStockItems.Controls.Add(Me.Label11)
        Me.FrameStockItems.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameStockItems.Location = New System.Drawing.Point(3, 2)
        Me.FrameStockItems.Name = "FrameStockItems"
        Me.FrameStockItems.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameStockItems.Size = New System.Drawing.Size(733, 441)
        Me.FrameStockItems.TabIndex = 80
        Me.FrameStockItems.TabStop = False
        Me.FrameStockItems.Text = "FrameStockItems"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Lavender
        Me.Label19.Font = New System.Drawing.Font("Comic Sans MS", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(450, 20)
        Me.Label19.Name = "Label19"
        Me.Label19.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label19.Size = New System.Drawing.Size(233, 17)
        Me.Label19.TabIndex = 107
        Me.Label19.Text = "NB: Cost And Sell Prices include Tax."
        '
        'PictureExtra
        '
        Me.PictureExtra.BackColor = System.Drawing.Color.Transparent
        Me.PictureExtra.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureExtra.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PictureExtra.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PictureExtra.Image = CType(resources.GetObject("PictureExtra.Image"), System.Drawing.Image)
        Me.PictureExtra.Location = New System.Drawing.Point(352, 412)
        Me.PictureExtra.Name = "PictureExtra"
        Me.PictureExtra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PictureExtra.Size = New System.Drawing.Size(16, 16)
        Me.PictureExtra.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureExtra.TabIndex = 85
        Me.PictureExtra.TabStop = False
        '
        '_cmdAddPart_0
        '
        Me._cmdAddPart_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._cmdAddPart_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdAddPart_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAddPart.SetIndex(Me._cmdAddPart_0, CType(0, Short))
        Me._cmdAddPart_0.Location = New System.Drawing.Point(208, 16)
        Me._cmdAddPart_0.Name = "_cmdAddPart_0"
        Me._cmdAddPart_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdAddPart_0.Size = New System.Drawing.Size(122, 23)
        Me._cmdAddPart_0.TabIndex = 21
        Me._cmdAddPart_0.Text = "Add Stock Part"
        Me._cmdAddPart_0.UseVisualStyleBackColor = False
        '
        '_ListViewParts_0
        '
        Me._ListViewParts_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me._ListViewParts_0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ListViewParts_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._ListViewParts_0.FullRowSelect = True
        Me._ListViewParts_0.GridLines = True
        Me._ListViewParts_0.HideSelection = False
        Me.ListViewParts.SetIndex(Me._ListViewParts_0, CType(0, Short))
        Me._ListViewParts_0.Location = New System.Drawing.Point(8, 48)
        Me._ListViewParts_0.MultiSelect = False
        Me._ListViewParts_0.Name = "_ListViewParts_0"
        Me._ListViewParts_0.Size = New System.Drawing.Size(719, 350)
        Me._ListViewParts_0.TabIndex = 23
        Me._ListViewParts_0.UseCompatibleStateImageBehavior = False
        Me._ListViewParts_0.View = System.Windows.Forms.View.Details
        '
        'LabPriceUpdated
        '
        Me.LabPriceUpdated.BackColor = System.Drawing.Color.Transparent
        Me.LabPriceUpdated.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPriceUpdated.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPriceUpdated.Location = New System.Drawing.Point(24, 412)
        Me.LabPriceUpdated.Name = "LabPriceUpdated"
        Me.LabPriceUpdated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPriceUpdated.Size = New System.Drawing.Size(110, 17)
        Me.LabPriceUpdated.TabIndex = 106
        Me.LabPriceUpdated.Text = "(*) Price Updated.."
        '
        'LabExtra
        '
        Me.LabExtra.BackColor = System.Drawing.Color.Transparent
        Me.LabExtra.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabExtra.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabExtra.Location = New System.Drawing.Point(384, 412)
        Me.LabExtra.Name = "LabExtra"
        Me.LabExtra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabExtra.Size = New System.Drawing.Size(108, 17)
        Me.LabExtra.TabIndex = 86
        Me.LabExtra.Text = "Extra to Quote"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(16, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(137, 17)
        Me.Label11.TabIndex = 81
        Me.Label11.Text = "Stock Items Supplied:"
        '
        'TabWorkPage_Tasks
        '
        Me.TabWorkPage_Tasks.Controls.Add(Me.FrameOtherTasks)
        Me.TabWorkPage_Tasks.Location = New System.Drawing.Point(4, 26)
        Me.TabWorkPage_Tasks.Name = "TabWorkPage_Tasks"
        Me.TabWorkPage_Tasks.Size = New System.Drawing.Size(744, 460)
        Me.TabWorkPage_Tasks.TabIndex = 3
        Me.TabWorkPage_Tasks.Text = "Tasks/Labour"
        Me.TabWorkPage_Tasks.UseVisualStyleBackColor = True
        '
        'FrameOtherTasks
        '
        Me.FrameOtherTasks.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameOtherTasks.Controls.Add(Me.cmdClearSessionTimes)
        Me.FrameOtherTasks.Controls.Add(Me.txtStaffSessions)
        Me.FrameOtherTasks.Controls.Add(Me.CmdCreateTask)
        Me.FrameOtherTasks.Controls.Add(Me.cmdDeleteTask)
        Me.FrameOtherTasks.Controls.Add(Me.cmdAddTask)
        Me.FrameOtherTasks.Controls.Add(Me.ListTaskTypes)
        Me.FrameOtherTasks.Controls.Add(Me.ListViewTasks)
        Me.FrameOtherTasks.Controls.Add(Me.LabTotalTime)
        Me.FrameOtherTasks.Controls.Add(Me.labNCMsg)
        Me.FrameOtherTasks.Controls.Add(Me.LabStaffSessions)
        Me.FrameOtherTasks.Controls.Add(Me.LabTasksDone)
        Me.FrameOtherTasks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameOtherTasks.Location = New System.Drawing.Point(3, 6)
        Me.FrameOtherTasks.Name = "FrameOtherTasks"
        Me.FrameOtherTasks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameOtherTasks.Size = New System.Drawing.Size(719, 393)
        Me.FrameOtherTasks.TabIndex = 0
        Me.FrameOtherTasks.TabStop = False
        Me.FrameOtherTasks.Text = "FrameOtherTasks"
        '
        'cmdClearSessionTimes
        '
        Me.cmdClearSessionTimes.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.cmdClearSessionTimes.Location = New System.Drawing.Point(217, 266)
        Me.cmdClearSessionTimes.Name = "cmdClearSessionTimes"
        Me.cmdClearSessionTimes.Size = New System.Drawing.Size(52, 23)
        Me.cmdClearSessionTimes.TabIndex = 6
        Me.cmdClearSessionTimes.Text = "Clear"
        Me.ToolTip1.SetToolTip(Me.cmdClearSessionTimes, "Clear all session Hours.  (Admin only). Warning- all previous session times will " &
        "be erased.")
        Me.cmdClearSessionTimes.UseVisualStyleBackColor = False
        '
        'txtStaffSessions
        '
        Me.txtStaffSessions.AcceptsReturn = True
        Me.txtStaffSessions.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtStaffSessions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStaffSessions.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStaffSessions.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStaffSessions.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStaffSessions.Location = New System.Drawing.Point(16, 288)
        Me.txtStaffSessions.MaxLength = 0
        Me.txtStaffSessions.Multiline = True
        Me.txtStaffSessions.Name = "txtStaffSessions"
        Me.txtStaffSessions.ReadOnly = True
        Me.txtStaffSessions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStaffSessions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtStaffSessions.Size = New System.Drawing.Size(253, 93)
        Me.txtStaffSessions.TabIndex = 29
        Me.txtStaffSessions.Text = "txtStaffSessions"
        '
        'cmdDeleteTask
        '
        Me.cmdDeleteTask.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.cmdDeleteTask.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeleteTask.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeleteTask.Location = New System.Drawing.Point(283, 29)
        Me.cmdDeleteTask.Name = "cmdDeleteTask"
        Me.cmdDeleteTask.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeleteTask.Size = New System.Drawing.Size(70, 21)
        Me.cmdDeleteTask.TabIndex = 1
        Me.cmdDeleteTask.Text = "Delete"
        Me.cmdDeleteTask.UseVisualStyleBackColor = False
        '
        'ListTaskTypes
        '
        Me.ListTaskTypes.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ListTaskTypes.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListTaskTypes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListTaskTypes.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListTaskTypes.Items.AddRange(New Object() {"ListTaskTypes"})
        Me.ListTaskTypes.Location = New System.Drawing.Point(200, 64)
        Me.ListTaskTypes.Name = "ListTaskTypes"
        Me.ListTaskTypes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ListTaskTypes.Size = New System.Drawing.Size(209, 154)
        Me.ListTaskTypes.Sorted = True
        Me.ListTaskTypes.TabIndex = 3
        Me.ListTaskTypes.Visible = False
        '
        'ListViewTasks
        '
        Me.ListViewTasks.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ListViewTasks.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewTasks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewTasks.FullRowSelect = True
        Me.ListViewTasks.HideSelection = False
        Me.ListViewTasks.Location = New System.Drawing.Point(16, 56)
        Me.ListViewTasks.Name = "ListViewTasks"
        Me.ListViewTasks.Size = New System.Drawing.Size(680, 191)
        Me.ListViewTasks.TabIndex = 5
        Me.ListViewTasks.UseCompatibleStateImageBehavior = False
        Me.ListViewTasks.View = System.Windows.Forms.View.Details
        '
        'LabTotalTime
        '
        Me.LabTotalTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.LabTotalTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabTotalTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabTotalTime.Location = New System.Drawing.Point(335, 324)
        Me.LabTotalTime.Name = "LabTotalTime"
        Me.LabTotalTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabTotalTime.Size = New System.Drawing.Size(281, 57)
        Me.LabTotalTime.TabIndex = 31
        Me.LabTotalTime.Text = "LabTotalTime"
        '
        'labNCMsg
        '
        Me.labNCMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.labNCMsg.Cursor = System.Windows.Forms.Cursors.Default
        Me.labNCMsg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labNCMsg.Location = New System.Drawing.Point(335, 266)
        Me.labNCMsg.Name = "labNCMsg"
        Me.labNCMsg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labNCMsg.Size = New System.Drawing.Size(281, 41)
        Me.labNCMsg.TabIndex = 30
        Me.labNCMsg.Text = "labNCMsg"
        '
        'LabStaffSessions
        '
        Me.LabStaffSessions.BackColor = System.Drawing.Color.Transparent
        Me.LabStaffSessions.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabStaffSessions.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabStaffSessions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabStaffSessions.Location = New System.Drawing.Point(16, 272)
        Me.LabStaffSessions.Name = "LabStaffSessions"
        Me.LabStaffSessions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabStaffSessions.Size = New System.Drawing.Size(198, 17)
        Me.LabStaffSessions.TabIndex = 76
        Me.LabStaffSessions.Text = "--Labour: Session Times This Job:"
        '
        'LabTasksDone
        '
        Me.LabTasksDone.BackColor = System.Drawing.Color.Transparent
        Me.LabTasksDone.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabTasksDone.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabTasksDone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabTasksDone.Location = New System.Drawing.Point(8, 24)
        Me.LabTasksDone.Name = "LabTasksDone"
        Me.LabTasksDone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabTasksDone.Size = New System.Drawing.Size(142, 17)
        Me.LabTasksDone.TabIndex = 70
        Me.LabTasksDone.Text = "Set Tasks and labour"
        '
        '_SSTab1_TabPageAttachments
        '
        Me._SSTab1_TabPageAttachments.BackColor = System.Drawing.Color.WhiteSmoke
        Me._SSTab1_TabPageAttachments.Controls.Add(Me.grpBoxItem)
        Me._SSTab1_TabPageAttachments.Controls.Add(Me.grpBoxAddNew)
        Me._SSTab1_TabPageAttachments.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPageAttachments.Name = "_SSTab1_TabPageAttachments"
        Me._SSTab1_TabPageAttachments.Size = New System.Drawing.Size(760, 506)
        Me._SSTab1_TabPageAttachments.TabIndex = 1
        Me._SSTab1_TabPageAttachments.Text = "Job Attachments"
        Me.ToolTip1.SetToolTip(Me._SSTab1_TabPageAttachments, "Service items Suppl'd")
        Me._SSTab1_TabPageAttachments.ToolTipText = "Service items Supplied"
        '
        'grpBoxItem
        '
        Me.grpBoxItem.BackColor = System.Drawing.Color.White
        Me.grpBoxItem.Controls.Add(Me.picMsExcel)
        Me.grpBoxItem.Controls.Add(Me.picMsWord)
        Me.grpBoxItem.Controls.Add(Me.picPDF)
        Me.grpBoxItem.Controls.Add(Me.txtComments)
        Me.grpBoxItem.Controls.Add(Me.btnViewDoc)
        Me.grpBoxItem.Controls.Add(Me.btnDelete)
        Me.grpBoxItem.Controls.Add(Me.picProduct)
        Me.grpBoxItem.Controls.Add(Me.lvwDocs)
        Me.grpBoxItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxItem.Location = New System.Drawing.Point(15, 181)
        Me.grpBoxItem.Name = "grpBoxItem"
        Me.grpBoxItem.Size = New System.Drawing.Size(740, 308)
        Me.grpBoxItem.TabIndex = 22
        Me.grpBoxItem.TabStop = False
        Me.grpBoxItem.Text = "grpBoxItem"
        '
        'picMsExcel
        '
        Me.picMsExcel.Image = CType(resources.GetObject("picMsExcel.Image"), System.Drawing.Image)
        Me.picMsExcel.Location = New System.Drawing.Point(11, 211)
        Me.picMsExcel.Name = "picMsExcel"
        Me.picMsExcel.Size = New System.Drawing.Size(49, 44)
        Me.picMsExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMsExcel.TabIndex = 25
        Me.picMsExcel.TabStop = False
        '
        'picMsWord
        '
        Me.picMsWord.Image = CType(resources.GetObject("picMsWord.Image"), System.Drawing.Image)
        Me.picMsWord.Location = New System.Drawing.Point(86, 160)
        Me.picMsWord.Name = "picMsWord"
        Me.picMsWord.Size = New System.Drawing.Size(43, 42)
        Me.picMsWord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMsWord.TabIndex = 24
        Me.picMsWord.TabStop = False
        '
        'picPDF
        '
        Me.picPDF.Image = CType(resources.GetObject("picPDF.Image"), System.Drawing.Image)
        Me.picPDF.Location = New System.Drawing.Point(8, 158)
        Me.picPDF.Name = "picPDF"
        Me.picPDF.Size = New System.Drawing.Size(44, 44)
        Me.picPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPDF.TabIndex = 23
        Me.picPDF.TabStop = False
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtComments.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.Location = New System.Drawing.Point(342, 163)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(289, 119)
        Me.txtComments.TabIndex = 17
        Me.txtComments.Text = "txtComments"
        '
        'btnViewDoc
        '
        Me.btnViewDoc.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnViewDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewDoc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewDoc.Location = New System.Drawing.Point(671, 169)
        Me.btnViewDoc.Name = "btnViewDoc"
        Me.btnViewDoc.Size = New System.Drawing.Size(53, 23)
        Me.btnViewDoc.TabIndex = 15
        Me.btnViewDoc.Text = "View"
        Me.ToolTip1.SetToolTip(Me.btnViewDoc, "Launches app to View Doc..")
        Me.btnViewDoc.UseVisualStyleBackColor = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDelete.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Location = New System.Drawing.Point(671, 215)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(53, 23)
        Me.btnDelete.TabIndex = 16
        Me.btnDelete.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this document permantly. File System to View..")
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'picProduct
        '
        Me.picProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.picProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picProduct.Location = New System.Drawing.Point(165, 163)
        Me.picProduct.Name = "picProduct"
        Me.picProduct.Size = New System.Drawing.Size(92, 92)
        Me.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picProduct.TabIndex = 13
        Me.picProduct.TabStop = False
        '
        'lvwDocs
        '
        Me.lvwDocs.CheckBoxes = True
        Me.lvwDocs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.doc_id, Me.doc_date_created, Me.doc_file_title, Me.doc_file_size, Me.doc_staff})
        Me.lvwDocs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwDocs.FullRowSelect = True
        Me.lvwDocs.GridLines = True
        Me.lvwDocs.HideSelection = False
        Me.lvwDocs.Location = New System.Drawing.Point(7, 24)
        Me.lvwDocs.MultiSelect = False
        Me.lvwDocs.Name = "lvwDocs"
        Me.lvwDocs.Size = New System.Drawing.Size(720, 124)
        Me.lvwDocs.TabIndex = 14
        Me.lvwDocs.UseCompatibleStateImageBehavior = False
        Me.lvwDocs.View = System.Windows.Forms.View.Details
        '
        'doc_id
        '
        Me.doc_id.Text = "Doc Id"
        '
        'doc_date_created
        '
        Me.doc_date_created.Text = "Created"
        Me.doc_date_created.Width = 100
        '
        'doc_file_title
        '
        Me.doc_file_title.Text = "File Title"
        Me.doc_file_title.Width = 260
        '
        'doc_file_size
        '
        Me.doc_file_size.Text = "File Size"
        Me.doc_file_size.Width = 100
        '
        'doc_staff
        '
        Me.doc_staff.Text = "Staff"
        Me.doc_staff.Width = 100
        '
        'grpBoxAddNew
        '
        Me.grpBoxAddNew.BackColor = System.Drawing.Color.White
        Me.grpBoxAddNew.Controls.Add(Me.txtNewFileName)
        Me.grpBoxAddNew.Controls.Add(Me.Label20)
        Me.grpBoxAddNew.Controls.Add(Me.labHelp)
        Me.grpBoxAddNew.Controls.Add(Me.Label22)
        Me.grpBoxAddNew.Controls.Add(Me.btnSaveAttachment)
        Me.grpBoxAddNew.Controls.Add(Me.txtNewComment)
        Me.grpBoxAddNew.Controls.Add(Me.btnBrowse)
        Me.grpBoxAddNew.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxAddNew.Location = New System.Drawing.Point(14, 6)
        Me.grpBoxAddNew.Name = "grpBoxAddNew"
        Me.grpBoxAddNew.Size = New System.Drawing.Size(741, 157)
        Me.grpBoxAddNew.TabIndex = 21
        Me.grpBoxAddNew.TabStop = False
        Me.grpBoxAddNew.Text = "Add New Attachment"
        '
        'txtNewFileName
        '
        Me.txtNewFileName.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtNewFileName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNewFileName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewFileName.Location = New System.Drawing.Point(216, 31)
        Me.txtNewFileName.Multiline = True
        Me.txtNewFileName.Name = "txtNewFileName"
        Me.txtNewFileName.ReadOnly = True
        Me.txtNewFileName.Size = New System.Drawing.Size(325, 31)
        Me.txtNewFileName.TabIndex = 23
        Me.txtNewFileName.Text = "txtNewFileName"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(216, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(74, 13)
        Me.Label20.TabIndex = 22
        Me.Label20.Text = "File to attach:"
        '
        'labHelp
        '
        Me.labHelp.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHelp.Location = New System.Drawing.Point(22, 17)
        Me.labHelp.Name = "labHelp"
        Me.labHelp.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labHelp.Size = New System.Drawing.Size(142, 122)
        Me.labHelp.TabIndex = 20
        Me.labHelp.Text = "To add an Attachment,, browse to the file to be attached, and Open.  Then Enter s" &
    "ome comment, and Press Save.."
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(602, 68)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(81, 42)
        Me.Label22.TabIndex = 20
        Me.Label22.Text = "Must Have Some Comment for New Doc."
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSaveAttachment
        '
        Me.btnSaveAttachment.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveAttachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveAttachment.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveAttachment.Location = New System.Drawing.Point(626, 113)
        Me.btnSaveAttachment.Name = "btnSaveAttachment"
        Me.btnSaveAttachment.Size = New System.Drawing.Size(57, 26)
        Me.btnSaveAttachment.TabIndex = 2
        Me.btnSaveAttachment.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveAttachment, "Save to Database")
        Me.btnSaveAttachment.UseVisualStyleBackColor = False
        '
        'txtNewComment
        '
        Me.txtNewComment.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.txtNewComment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNewComment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewComment.Location = New System.Drawing.Point(216, 68)
        Me.txtNewComment.Multiline = True
        Me.txtNewComment.Name = "txtNewComment"
        Me.txtNewComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNewComment.Size = New System.Drawing.Size(325, 73)
        Me.txtNewComment.TabIndex = 1
        Me.txtNewComment.Text = "txtNewComment"
        Me.ToolTip1.SetToolTip(Me.txtNewComment, "ust Have Comment for New Attachment")
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(626, 31)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(57, 26)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "Browse"
        Me.ToolTip1.SetToolTip(Me.btnBrowse, "Browse for new File to Attach")
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        '_SSTab1_TabPageNotif
        '
        Me._SSTab1_TabPageNotif.Controls.Add(Me.FrameNotified)
        Me._SSTab1_TabPageNotif.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPageNotif.Name = "_SSTab1_TabPageNotif"
        Me._SSTab1_TabPageNotif.Size = New System.Drawing.Size(760, 506)
        Me._SSTab1_TabPageNotif.TabIndex = 4
        Me._SSTab1_TabPageNotif.Text = " Cust. Notified"
        Me.ToolTip1.SetToolTip(Me._SSTab1_TabPageNotif, " Customer Notifications..")
        Me._SSTab1_TabPageNotif.UseVisualStyleBackColor = True
        '
        'FrameNotified
        '
        Me.FrameNotified.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameNotified.Controls.Add(Me.txtNotification)
        Me.FrameNotified.Controls.Add(Me.Label12)
        Me.FrameNotified.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameNotified.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameNotified.Location = New System.Drawing.Point(5, 3)
        Me.FrameNotified.Name = "FrameNotified"
        Me.FrameNotified.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameNotified.Size = New System.Drawing.Size(745, 486)
        Me.FrameNotified.TabIndex = 102
        Me.FrameNotified.TabStop = False
        Me.FrameNotified.Text = "FrameNotified"
        '
        'txtNotification
        '
        Me.txtNotification.AcceptsReturn = True
        Me.txtNotification.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtNotification.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNotification.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNotification.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNotification.Location = New System.Drawing.Point(16, 55)
        Me.txtNotification.MaxLength = 0
        Me.txtNotification.Multiline = True
        Me.txtNotification.Name = "txtNotification"
        Me.txtNotification.ReadOnly = True
        Me.txtNotification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNotification.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotification.Size = New System.Drawing.Size(720, 407)
        Me.txtNotification.TabIndex = 32
        Me.txtNotification.TabStop = False
        Me.txtNotification.Text = "txtNotification"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(13, 25)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(207, 17)
        Me.Label12.TabIndex = 103
        Me.Label12.Text = " Customer Notifications History"
        '
        'cmdFinish
        '
        Me.cmdFinish.BackColor = System.Drawing.Color.LawnGreen
        Me.cmdFinish.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdFinish.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFinish.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFinish.Location = New System.Drawing.Point(116, 261)
        Me.cmdFinish.Name = "cmdFinish"
        Me.cmdFinish.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFinish.Size = New System.Drawing.Size(92, 33)
        Me.cmdFinish.TabIndex = 40
        Me.cmdFinish.Text = "Finish"
        Me.ToolTip1.SetToolTip(Me.cmdFinish, "Finish as selected..")
        Me.cmdFinish.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.Thistle
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(117, 300)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(92, 33)
        Me.cmdCancel.TabIndex = 41
        Me.cmdCancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.cmdCancel, "Abandon changes and exit..")
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'labTaskNeeded
        '
        Me.labTaskNeeded.BackColor = System.Drawing.SystemColors.Highlight
        Me.labTaskNeeded.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labTaskNeeded.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.labTaskNeeded.Location = New System.Drawing.Point(117, 194)
        Me.labTaskNeeded.Name = "labTaskNeeded"
        Me.labTaskNeeded.Size = New System.Drawing.Size(69, 17)
        Me.labTaskNeeded.TabIndex = 79
        Me.labTaskNeeded.Text = "-No Tasks-"
        Me.labTaskNeeded.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ToolTip1.SetToolTip(Me.labTaskNeeded, "Note: A Task or Sevice item is needed for Job to be completed..")
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.JobMatix3.My.Resources.Resources.Printer_icon_thD0MUHEMG
        Me.PictureBox1.Location = New System.Drawing.Point(169, 99)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(29, 29)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 85
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Select Print options as needed...")
        '
        '_chkPrtDocs_1
        '
        Me._chkPrtDocs_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._chkPrtDocs_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrtDocs_1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrtDocs_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._chkPrtDocs_1.Location = New System.Drawing.Point(15, 108)
        Me._chkPrtDocs_1.Name = "_chkPrtDocs_1"
        Me._chkPrtDocs_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrtDocs_1.Size = New System.Drawing.Size(106, 23)
        Me._chkPrtDocs_1.TabIndex = 4
        Me._chkPrtDocs_1.Text = "Delivery Docket"
        Me.ToolTip1.SetToolTip(Me._chkPrtDocs_1, "Prints Job Info on Delivery Docket/Receipt.")
        Me._chkPrtDocs_1.UseVisualStyleBackColor = False
        '
        '_chkPrtDocs_0
        '
        Me._chkPrtDocs_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._chkPrtDocs_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrtDocs_0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrtDocs_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._chkPrtDocs_0.Location = New System.Drawing.Point(10, 18)
        Me._chkPrtDocs_0.Name = "_chkPrtDocs_0"
        Me._chkPrtDocs_0.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me._chkPrtDocs_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrtDocs_0.Size = New System.Drawing.Size(66, 41)
        Me._chkPrtDocs_0.TabIndex = 0
        Me._chkPrtDocs_0.Text = "Service  Record"
        Me.ToolTip1.SetToolTip(Me._chkPrtDocs_0, "Prints Service Record with all item barcodes and job info ready for Job Sale/Deli" &
        "very.")
        Me._chkPrtDocs_0.UseVisualStyleBackColor = False
        '
        '_chkPrtDocs_Report
        '
        Me._chkPrtDocs_Report.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me._chkPrtDocs_Report.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrtDocs_Report.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrtDocs_Report.ForeColor = System.Drawing.SystemColors.ControlText
        Me._chkPrtDocs_Report.Location = New System.Drawing.Point(152, 18)
        Me._chkPrtDocs_Report.Name = "_chkPrtDocs_Report"
        Me._chkPrtDocs_Report.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me._chkPrtDocs_Report.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrtDocs_Report.Size = New System.Drawing.Size(62, 41)
        Me._chkPrtDocs_Report.TabIndex = 2
        Me._chkPrtDocs_Report.Text = "Cust. Report"
        Me.ToolTip1.SetToolTip(Me._chkPrtDocs_Report, "Prints Service Report for Customer with all items and work notes  and pictures (i" &
        "f any).")
        Me._chkPrtDocs_Report.UseVisualStyleBackColor = False
        '
        'cboTimeSelectTenths
        '
        Me.cboTimeSelectTenths.BackColor = System.Drawing.SystemColors.Window
        Me.cboTimeSelectTenths.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTimeSelectTenths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTimeSelectTenths.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboTimeSelectTenths.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTimeSelectTenths.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTimeSelectTenths.Items.AddRange(New Object() {"cboTimeSelect"})
        Me.cboTimeSelectTenths.Location = New System.Drawing.Point(64, 42)
        Me.cboTimeSelectTenths.Name = "cboTimeSelectTenths"
        Me.cboTimeSelectTenths.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTimeSelectTenths.Size = New System.Drawing.Size(54, 19)
        Me.cboTimeSelectTenths.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.cboTimeSelectTenths, "Select Tenths of an Hour..")
        '
        'cboTimeSelectHours
        '
        Me.cboTimeSelectHours.BackColor = System.Drawing.SystemColors.Window
        Me.cboTimeSelectHours.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTimeSelectHours.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTimeSelectHours.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboTimeSelectHours.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTimeSelectHours.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTimeSelectHours.Items.AddRange(New Object() {"cboTimeSelect"})
        Me.cboTimeSelectHours.Location = New System.Drawing.Point(15, 42)
        Me.cboTimeSelectHours.Name = "cboTimeSelectHours"
        Me.cboTimeSelectHours.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTimeSelectHours.Size = New System.Drawing.Size(42, 19)
        Me.cboTimeSelectHours.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.cboTimeSelectHours, "Select Full Hours")
        '
        'cboReceiptPrinters
        '
        Me.cboReceiptPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboReceiptPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReceiptPrinters.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReceiptPrinters.FormattingEnabled = True
        Me.cboReceiptPrinters.Location = New System.Drawing.Point(14, 132)
        Me.cboReceiptPrinters.Name = "cboReceiptPrinters"
        Me.cboReceiptPrinters.Size = New System.Drawing.Size(197, 21)
        Me.cboReceiptPrinters.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cboReceiptPrinters, "Docket Printer")
        '
        'cboColourPrinters
        '
        Me.cboColourPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboColourPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColourPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboColourPrinters.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboColourPrinters.FormattingEnabled = True
        Me.cboColourPrinters.Location = New System.Drawing.Point(14, 67)
        Me.cboColourPrinters.Name = "cboColourPrinters"
        Me.cboColourPrinters.Size = New System.Drawing.Size(197, 21)
        Me.cboColourPrinters.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cboColourPrinters, "-- A4 Colour Printer --")
        '
        'chkPrintItemBarcodes
        '
        Me.chkPrintItemBarcodes.BackColor = System.Drawing.Color.Snow
        Me.chkPrintItemBarcodes.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintItemBarcodes.Location = New System.Drawing.Point(83, 19)
        Me.chkPrintItemBarcodes.Name = "chkPrintItemBarcodes"
        Me.chkPrintItemBarcodes.Size = New System.Drawing.Size(63, 40)
        Me.chkPrintItemBarcodes.TabIndex = 1
        Me.chkPrintItemBarcodes.Text = "Incl.Item Barcodes"
        Me.ToolTip1.SetToolTip(Me.chkPrintItemBarcodes, "Include Item Barcode List in Service Record Printout.")
        Me.chkPrintItemBarcodes.UseVisualStyleBackColor = False
        '
        'PicSkipped
        '
        Me.PicSkipped.BackColor = System.Drawing.SystemColors.Control
        Me.PicSkipped.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PicSkipped.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicSkipped.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PicSkipped.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PicSkipped.Image = CType(resources.GetObject("PicSkipped.Image"), System.Drawing.Image)
        Me.PicSkipped.Location = New System.Drawing.Point(465, 5)
        Me.PicSkipped.Name = "PicSkipped"
        Me.PicSkipped.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PicSkipped.Size = New System.Drawing.Size(25, 25)
        Me.PicSkipped.TabIndex = 68
        Me.PicSkipped.TabStop = False
        Me.PicSkipped.Visible = False
        '
        'PicQueried
        '
        Me.PicQueried.BackColor = System.Drawing.SystemColors.Control
        Me.PicQueried.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PicQueried.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicQueried.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PicQueried.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PicQueried.Image = CType(resources.GetObject("PicQueried.Image"), System.Drawing.Image)
        Me.PicQueried.Location = New System.Drawing.Point(465, 35)
        Me.PicQueried.Name = "PicQueried"
        Me.PicQueried.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PicQueried.Size = New System.Drawing.Size(25, 25)
        Me.PicQueried.TabIndex = 67
        Me.PicQueried.TabStop = False
        Me.PicQueried.Visible = False
        '
        'PicChecked
        '
        Me.PicChecked.BackColor = System.Drawing.SystemColors.Control
        Me.PicChecked.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PicChecked.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicChecked.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PicChecked.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PicChecked.Image = CType(resources.GetObject("PicChecked.Image"), System.Drawing.Image)
        Me.PicChecked.Location = New System.Drawing.Point(503, 32)
        Me.PicChecked.Name = "PicChecked"
        Me.PicChecked.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PicChecked.Size = New System.Drawing.Size(25, 25)
        Me.PicChecked.TabIndex = 65
        Me.PicChecked.TabStop = False
        Me.PicChecked.Visible = False
        '
        'chkMyJob
        '
        Me.chkMyJob.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkMyJob.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMyJob.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMyJob.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMyJob.Location = New System.Drawing.Point(15, 75)
        Me.chkMyJob.Name = "chkMyJob"
        Me.chkMyJob.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMyJob.Size = New System.Drawing.Size(109, 15)
        Me.chkMyJob.TabIndex = 4
        Me.chkMyJob.Text = "Make it My Job."
        Me.chkMyJob.UseVisualStyleBackColor = False
        '
        'txtNomTech
        '
        Me.txtNomTech.AcceptsReturn = True
        Me.txtNomTech.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNomTech.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNomTech.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNomTech.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNomTech.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNomTech.Location = New System.Drawing.Point(117, 58)
        Me.txtNomTech.MaxLength = 0
        Me.txtNomTech.Name = "txtNomTech"
        Me.txtNomTech.ReadOnly = True
        Me.txtNomTech.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNomTech.Size = New System.Drawing.Size(95, 15)
        Me.txtNomTech.TabIndex = 3
        Me.txtNomTech.TabStop = False
        Me.txtNomTech.Text = "txtNomTech"
        '
        'PictureExtraPrint
        '
        Me.PictureExtraPrint.BackColor = System.Drawing.Color.Transparent
        Me.PictureExtraPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureExtraPrint.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PictureExtraPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PictureExtraPrint.Image = CType(resources.GetObject("PictureExtraPrint.Image"), System.Drawing.Image)
        Me.PictureExtraPrint.Location = New System.Drawing.Point(496, 5)
        Me.PictureExtraPrint.Name = "PictureExtraPrint"
        Me.PictureExtraPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PictureExtraPrint.Size = New System.Drawing.Size(16, 16)
        Me.PictureExtraPrint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureExtraPrint.TabIndex = 57
        Me.PictureExtraPrint.TabStop = False
        '
        'Picture2
        '
        Me.Picture2.BackColor = System.Drawing.SystemColors.Control
        Me.Picture2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Picture2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Picture2.Location = New System.Drawing.Point(667, 5)
        Me.Picture2.Name = "Picture2"
        Me.Picture2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture2.Size = New System.Drawing.Size(33, 25)
        Me.Picture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture2.TabIndex = 56
        Me.Picture2.TabStop = False
        '
        'SSTab1
        '
        Me.SSTab1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPageJob)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPageWork)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPageAttachments)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPageNotif)
        Me.SSTab1.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(7, 103)
        Me.SSTab1.Margin = New System.Windows.Forms.Padding(2)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 1
        Me.SSTab1.Size = New System.Drawing.Size(768, 532)
        Me.SSTab1.TabIndex = 54
        '
        '_SSTab1_TabPageJob
        '
        Me._SSTab1_TabPageJob.Controls.Add(Me.frameProblem)
        Me._SSTab1_TabPageJob.Controls.Add(Me.frameJobDates)
        Me._SSTab1_TabPageJob.Controls.Add(Me.FrameQuotation)
        Me._SSTab1_TabPageJob.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPageJob.Name = "_SSTab1_TabPageJob"
        Me._SSTab1_TabPageJob.Size = New System.Drawing.Size(760, 506)
        Me._SSTab1_TabPageJob.TabIndex = 5
        Me._SSTab1_TabPageJob.Text = "Job Requirements"
        Me._SSTab1_TabPageJob.UseVisualStyleBackColor = True
        '
        'frameProblem
        '
        Me.frameProblem.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameProblem.Controls.Add(Me.labPriority)
        Me.frameProblem.Controls.Add(Me.Label10)
        Me.frameProblem.Controls.Add(Me.ChkRecovDisk)
        Me.frameProblem.Controls.Add(Me.ChkBackupReq)
        Me.frameProblem.Controls.Add(Me.Label9)
        Me.frameProblem.Controls.Add(Me.txtSymptoms)
        Me.frameProblem.Controls.Add(Me.Label8)
        Me.frameProblem.Controls.Add(Me.labInstructions)
        Me.frameProblem.Controls.Add(Me.Label2)
        Me.frameProblem.Controls.Add(Me.txtGoods)
        Me.frameProblem.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameProblem.Location = New System.Drawing.Point(6, 94)
        Me.frameProblem.Name = "frameProblem"
        Me.frameProblem.Size = New System.Drawing.Size(713, 394)
        Me.frameProblem.TabIndex = 43
        Me.frameProblem.TabStop = False
        Me.frameProblem.Text = "frameProblem"
        '
        'labPriority
        '
        Me.labPriority.AutoSize = True
        Me.labPriority.Location = New System.Drawing.Point(103, 17)
        Me.labPriority.Name = "labPriority"
        Me.labPriority.Size = New System.Drawing.Size(59, 14)
        Me.labPriority.TabIndex = 114
        Me.labPriority.Text = "labPriority"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(18, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 113
        Me.Label10.Text = "Job Priority:"
        '
        'ChkRecovDisk
        '
        Me.ChkRecovDisk.Enabled = False
        Me.ChkRecovDisk.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkRecovDisk.Location = New System.Drawing.Point(437, 225)
        Me.ChkRecovDisk.Name = "ChkRecovDisk"
        Me.ChkRecovDisk.Size = New System.Drawing.Size(109, 17)
        Me.ChkRecovDisk.TabIndex = 11
        Me.ChkRecovDisk.Text = "Recovery Disk"
        Me.ChkRecovDisk.UseVisualStyleBackColor = True
        '
        'ChkBackupReq
        '
        Me.ChkBackupReq.Enabled = False
        Me.ChkBackupReq.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkBackupReq.Location = New System.Drawing.Point(437, 202)
        Me.ChkBackupReq.Name = "ChkBackupReq"
        Me.ChkBackupReq.Size = New System.Drawing.Size(132, 17)
        Me.ChkBackupReq.TabIndex = 10
        Me.ChkBackupReq.Text = "Data Backup Req'd"
        Me.ChkBackupReq.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(17, 154)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(216, 13)
        Me.Label9.TabIndex = 110
        Me.Label9.Text = "Problems reported/ Work requested:"
        '
        'txtSymptoms
        '
        Me.txtSymptoms.AcceptsReturn = True
        Me.txtSymptoms.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtSymptoms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSymptoms.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSymptoms.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSymptoms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSymptoms.Location = New System.Drawing.Point(18, 173)
        Me.txtSymptoms.MaxLength = 548
        Me.txtSymptoms.Multiline = True
        Me.txtSymptoms.Name = "txtSymptoms"
        Me.txtSymptoms.ReadOnly = True
        Me.txtSymptoms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSymptoms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSymptoms.Size = New System.Drawing.Size(381, 183)
        Me.txtSymptoms.TabIndex = 9
        Me.txtSymptoms.Text = "txtSymptoms"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(436, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(143, 16)
        Me.Label8.TabIndex = 108
        Me.Label8.Text = "Cust. Instructions:"
        '
        'labInstructions
        '
        Me.labInstructions.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.labInstructions.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labInstructions.Location = New System.Drawing.Point(434, 57)
        Me.labInstructions.Name = "labInstructions"
        Me.labInstructions.Padding = New System.Windows.Forms.Padding(5)
        Me.labInstructions.Size = New System.Drawing.Size(231, 81)
        Me.labInstructions.TabIndex = 107
        Me.labInstructions.Text = "labInstructions"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(18, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(143, 16)
        Me.Label2.TabIndex = 104
        Me.Label2.Text = "Goods in Care:"
        '
        'txtGoods
        '
        Me.txtGoods.AcceptsReturn = True
        Me.txtGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtGoods.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGoods.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGoods.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoods.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGoods.Location = New System.Drawing.Point(18, 57)
        Me.txtGoods.MaxLength = 548
        Me.txtGoods.Multiline = True
        Me.txtGoods.Name = "txtGoods"
        Me.txtGoods.ReadOnly = True
        Me.txtGoods.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGoods.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtGoods.Size = New System.Drawing.Size(387, 81)
        Me.txtGoods.TabIndex = 8
        Me.txtGoods.Text = "txtGoods"
        '
        'frameJobDates
        '
        Me.frameJobDates.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameJobDates.Controls.Add(Me.Label15)
        Me.frameJobDates.Controls.Add(Me.LabJobReturned)
        Me.frameJobDates.Controls.Add(Me.chkReturned)
        Me.frameJobDates.Controls.Add(Me._txtStaffName_1)
        Me.frameJobDates.Controls.Add(Me._txtStaffName_0)
        Me.frameJobDates.Controls.Add(Me.labDeliveredBy)
        Me.frameJobDates.Controls.Add(Me.labDateUpdated)
        Me.frameJobDates.Controls.Add(Me.LabRcvdStaff)
        Me.frameJobDates.Controls.Add(Me.labDateCreated)
        Me.frameJobDates.Controls.Add(Me.Label13)
        Me.frameJobDates.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameJobDates.Location = New System.Drawing.Point(5, 3)
        Me.frameJobDates.Name = "frameJobDates"
        Me.frameJobDates.Size = New System.Drawing.Size(750, 47)
        Me.frameJobDates.TabIndex = 44
        Me.frameJobDates.TabStop = False
        Me.frameJobDates.Text = "frameJobDates"
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(174, 13)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(44, 29)
        Me.Label15.TabIndex = 109
        Me.Label15.Text = "Last Update:"
        '
        'LabJobReturned
        '
        Me.LabJobReturned.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.LabJobReturned.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabJobReturned.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabJobReturned.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobReturned.Location = New System.Drawing.Point(605, 11)
        Me.LabJobReturned.Name = "LabJobReturned"
        Me.LabJobReturned.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabJobReturned.Size = New System.Drawing.Size(65, 31)
        Me.LabJobReturned.TabIndex = 7
        Me.LabJobReturned.Text = "Job Returned"
        Me.LabJobReturned.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkReturned
        '
        Me.chkReturned.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.chkReturned.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkReturned.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReturned.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReturned.Location = New System.Drawing.Point(587, 11)
        Me.chkReturned.Name = "chkReturned"
        Me.chkReturned.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkReturned.Size = New System.Drawing.Size(17, 29)
        Me.chkReturned.TabIndex = 107
        Me.chkReturned.UseVisualStyleBackColor = False
        '
        '_txtStaffName_1
        '
        Me._txtStaffName_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtStaffName_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtStaffName_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtStaffName_1.Location = New System.Drawing.Point(362, 27)
        Me._txtStaffName_1.Name = "_txtStaffName_1"
        Me._txtStaffName_1.Size = New System.Drawing.Size(160, 14)
        Me._txtStaffName_1.TabIndex = 6
        Me._txtStaffName_1.Text = "_txtStaffName_1"
        '
        '_txtStaffName_0
        '
        Me._txtStaffName_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtStaffName_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtStaffName_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtStaffName_0.Location = New System.Drawing.Point(224, 27)
        Me._txtStaffName_0.Name = "_txtStaffName_0"
        Me._txtStaffName_0.Size = New System.Drawing.Size(113, 14)
        Me._txtStaffName_0.TabIndex = 5
        Me._txtStaffName_0.Text = "_txtStaffName_0"
        '
        'labDeliveredBy
        '
        Me.labDeliveredBy.Location = New System.Drawing.Point(359, 13)
        Me.labDeliveredBy.Name = "labDeliveredBy"
        Me.labDeliveredBy.Size = New System.Drawing.Size(100, 11)
        Me.labDeliveredBy.TabIndex = 76
        Me.labDeliveredBy.Text = "labDeliveredBy"
        '
        'labDateUpdated
        '
        Me.labDateUpdated.Location = New System.Drawing.Point(221, 13)
        Me.labDateUpdated.Name = "labDateUpdated"
        Me.labDateUpdated.Size = New System.Drawing.Size(100, 11)
        Me.labDateUpdated.TabIndex = 75
        Me.labDateUpdated.Text = "labDateUpdated"
        '
        'LabRcvdStaff
        '
        Me.LabRcvdStaff.AutoSize = True
        Me.LabRcvdStaff.BackColor = System.Drawing.Color.Transparent
        Me.LabRcvdStaff.Location = New System.Drawing.Point(59, 27)
        Me.LabRcvdStaff.Name = "LabRcvdStaff"
        Me.LabRcvdStaff.Size = New System.Drawing.Size(72, 13)
        Me.LabRcvdStaff.TabIndex = 74
        Me.LabRcvdStaff.Text = "LabRcvdStaff"
        '
        'labDateCreated
        '
        Me.labDateCreated.Location = New System.Drawing.Point(59, 13)
        Me.labDateCreated.Name = "labDateCreated"
        Me.labDateCreated.Size = New System.Drawing.Size(91, 13)
        Me.labDateCreated.TabIndex = 73
        Me.labDateCreated.Text = "labDateCreated"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(8, 13)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 13)
        Me.Label13.TabIndex = 72
        Me.Label13.Text = "Created:"
        '
        'FrameQuotation
        '
        Me.FrameQuotation.BackColor = System.Drawing.Color.Snow
        Me.FrameQuotation.Controls.Add(Me.ListViewExtraParts)
        Me.FrameQuotation.Controls.Add(Me.ListViewQuote)
        Me.FrameQuotation.Controls.Add(Me.LabReconciliation)
        Me.FrameQuotation.Controls.Add(Me.LabExtraParts)
        Me.FrameQuotation.Controls.Add(Me.LabQuotedParts)
        Me.FrameQuotation.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameQuotation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameQuotation.Location = New System.Drawing.Point(5, 54)
        Me.FrameQuotation.Name = "FrameQuotation"
        Me.FrameQuotation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameQuotation.Size = New System.Drawing.Size(750, 440)
        Me.FrameQuotation.TabIndex = 42
        Me.FrameQuotation.TabStop = False
        Me.FrameQuotation.Text = "FrameQuotation"
        '
        'ListViewExtraParts
        '
        Me.ListViewExtraParts.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ListViewExtraParts.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewExtraParts.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewExtraParts.GridLines = True
        Me.ListViewExtraParts.Location = New System.Drawing.Point(11, 240)
        Me.ListViewExtraParts.Name = "ListViewExtraParts"
        Me.ListViewExtraParts.Size = New System.Drawing.Size(733, 112)
        Me.ListViewExtraParts.TabIndex = 13
        Me.ListViewExtraParts.TabStop = False
        Me.ListViewExtraParts.UseCompatibleStateImageBehavior = False
        Me.ListViewExtraParts.View = System.Windows.Forms.View.Details
        '
        'ListViewQuote
        '
        Me.ListViewQuote.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ListViewQuote.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewQuote.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewQuote.GridLines = True
        Me.ListViewQuote.Location = New System.Drawing.Point(15, 30)
        Me.ListViewQuote.Name = "ListViewQuote"
        Me.ListViewQuote.Size = New System.Drawing.Size(729, 177)
        Me.ListViewQuote.TabIndex = 12
        Me.ListViewQuote.TabStop = False
        Me.ListViewQuote.UseCompatibleStateImageBehavior = False
        Me.ListViewQuote.View = System.Windows.Forms.View.Details
        '
        'LabReconciliation
        '
        Me.LabReconciliation.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabReconciliation.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabReconciliation.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabReconciliation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabReconciliation.Location = New System.Drawing.Point(13, 365)
        Me.LabReconciliation.Name = "LabReconciliation"
        Me.LabReconciliation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabReconciliation.Size = New System.Drawing.Size(731, 73)
        Me.LabReconciliation.TabIndex = 53
        Me.LabReconciliation.Text = "LabReconciliation"
        '
        'LabExtraParts
        '
        Me.LabExtraParts.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabExtraParts.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabExtraParts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabExtraParts.Location = New System.Drawing.Point(12, 220)
        Me.LabExtraParts.Name = "LabExtraParts"
        Me.LabExtraParts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabExtraParts.Size = New System.Drawing.Size(195, 17)
        Me.LabExtraParts.TabIndex = 52
        Me.LabExtraParts.Text = "Parts Supplied Extra to Quote:"
        '
        'LabQuotedParts
        '
        Me.LabQuotedParts.BackColor = System.Drawing.Color.Transparent
        Me.LabQuotedParts.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabQuotedParts.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabQuotedParts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabQuotedParts.Location = New System.Drawing.Point(16, 14)
        Me.LabQuotedParts.Name = "LabQuotedParts"
        Me.LabQuotedParts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabQuotedParts.Size = New System.Drawing.Size(321, 14)
        Me.LabQuotedParts.TabIndex = 50
        Me.LabQuotedParts.Text = "Quoted Parts (Red Flagged Items still to be Supplied..)"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageList1.Images.SetKeyName(0, "unchecked")
        Me.ImageList1.Images.SetKeyName(1, "checked")
        Me.ImageList1.Images.SetKeyName(2, "alert")
        '
        'FrameSessionTime
        '
        Me.FrameSessionTime.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameSessionTime.Controls.Add(Me.labMinCharge)
        Me.FrameSessionTime.Controls.Add(Me.labResultHours)
        Me.FrameSessionTime.Controls.Add(Me.cboTimeSelectTenths)
        Me.FrameSessionTime.Controls.Add(Me.Label5)
        Me.FrameSessionTime.Controls.Add(Me.labFullCost)
        Me.FrameSessionTime.Controls.Add(Me._optChargeable_1)
        Me.FrameSessionTime.Controls.Add(Me._optChargeable_0)
        Me.FrameSessionTime.Controls.Add(Me.cboTimeSelectHours)
        Me.FrameSessionTime.Controls.Add(Me.LabelTimeSelect)
        Me.FrameSessionTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameSessionTime.Location = New System.Drawing.Point(778, 102)
        Me.FrameSessionTime.Name = "FrameSessionTime"
        Me.FrameSessionTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameSessionTime.Size = New System.Drawing.Size(224, 176)
        Me.FrameSessionTime.TabIndex = 27
        Me.FrameSessionTime.TabStop = False
        Me.FrameSessionTime.Text = "FrameSessionTime"
        '
        'labMinCharge
        '
        Me.labMinCharge.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMinCharge.ForeColor = System.Drawing.Color.DarkMagenta
        Me.labMinCharge.Location = New System.Drawing.Point(130, 42)
        Me.labMinCharge.Name = "labMinCharge"
        Me.labMinCharge.Size = New System.Drawing.Size(69, 31)
        Me.labMinCharge.TabIndex = 108
        Me.labMinCharge.Text = "Min. Charge is in force."
        '
        'labResultHours
        '
        Me.labResultHours.AutoSize = True
        Me.labResultHours.Location = New System.Drawing.Point(127, 18)
        Me.labResultHours.Name = "labResultHours"
        Me.labResultHours.Size = New System.Drawing.Size(79, 13)
        Me.labResultHours.TabIndex = 107
        Me.labResultHours.Text = "labResultHours"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 120)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(59, 34)
        Me.Label5.TabIndex = 106
        Me.Label5.Text = "Job  Cost Summary: "
        '
        'labFullCost
        '
        Me.labFullCost.BackColor = System.Drawing.Color.Transparent
        Me.labFullCost.Cursor = System.Windows.Forms.Cursors.Default
        Me.labFullCost.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labFullCost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labFullCost.Location = New System.Drawing.Point(66, 117)
        Me.labFullCost.Name = "labFullCost"
        Me.labFullCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labFullCost.Size = New System.Drawing.Size(151, 53)
        Me.labFullCost.TabIndex = 40
        Me.labFullCost.Text = "labFullCost"
        '
        '_optChargeable_1
        '
        Me._optChargeable_1.BackColor = System.Drawing.Color.Lavender
        Me._optChargeable_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optChargeable_1.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optChargeable_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optChargeable.SetIndex(Me._optChargeable_1, CType(1, Short))
        Me._optChargeable_1.Location = New System.Drawing.Point(16, 80)
        Me._optChargeable_1.Name = "_optChargeable_1"
        Me._optChargeable_1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me._optChargeable_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optChargeable_1.Size = New System.Drawing.Size(93, 25)
        Me._optChargeable_1.TabIndex = 36
        Me._optChargeable_1.TabStop = True
        Me._optChargeable_1.Text = "No Charge"
        Me._optChargeable_1.UseVisualStyleBackColor = False
        '
        '_optChargeable_0
        '
        Me._optChargeable_0.BackColor = System.Drawing.Color.Lavender
        Me._optChargeable_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optChargeable_0.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optChargeable_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optChargeable.SetIndex(Me._optChargeable_0, CType(0, Short))
        Me._optChargeable_0.Location = New System.Drawing.Point(110, 80)
        Me._optChargeable_0.Name = "_optChargeable_0"
        Me._optChargeable_0.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me._optChargeable_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optChargeable_0.Size = New System.Drawing.Size(107, 25)
        Me._optChargeable_0.TabIndex = 37
        Me._optChargeable_0.TabStop = True
        Me._optChargeable_0.Text = "Charge To Cust"
        Me._optChargeable_0.UseVisualStyleBackColor = False
        '
        'LabelTimeSelect
        '
        Me.LabelTimeSelect.BackColor = System.Drawing.Color.Transparent
        Me.LabelTimeSelect.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelTimeSelect.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTimeSelect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabelTimeSelect.Location = New System.Drawing.Point(13, 16)
        Me.LabelTimeSelect.Name = "LabelTimeSelect"
        Me.LabelTimeSelect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelTimeSelect.Size = New System.Drawing.Size(108, 23)
        Me.LabelTimeSelect.TabIndex = 38
        Me.LabelTimeSelect.Text = "Time this Session"
        Me.LabelTimeSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabVersion
        '
        Me.LabVersion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabVersion.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabVersion.Location = New System.Drawing.Point(14, 338)
        Me.LabVersion.Name = "LabVersion"
        Me.LabVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabVersion.Size = New System.Drawing.Size(160, 10)
        Me.LabVersion.TabIndex = 40
        Me.LabVersion.Text = "LabVersion"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 400
        '
        'Picture1
        '
        Me.Picture1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Picture1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Picture1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Picture1.Location = New System.Drawing.Point(392, 4)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture1.Size = New System.Drawing.Size(32, 25)
        Me.Picture1.TabIndex = 3
        Me.Picture1.TabStop = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(12, 57)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(99, 17)
        Me.Label14.TabIndex = 60
        Me.Label14.Text = "Job assigned to:"
        '
        'LabTicket
        '
        Me.LabTicket.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.LabTicket.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabTicket.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabTicket.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabTicket.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabTicket.Location = New System.Drawing.Point(763, 2)
        Me.LabTicket.Name = "LabTicket"
        Me.LabTicket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabTicket.Size = New System.Drawing.Size(237, 31)
        Me.LabTicket.TabIndex = 2
        Me.LabTicket.Text = "labTicket"
        Me.LabTicket.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabHdr1
        '
        Me.LabHdr1.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.ForeColor = System.Drawing.Color.Indigo
        Me.LabHdr1.Location = New System.Drawing.Point(11, 7)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr1.Size = New System.Drawing.Size(265, 19)
        Me.LabHdr1.TabIndex = 0
        Me.LabHdr1.Text = "Job Service Record"
        Me.LabHdr1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ListViewParts
        '
        '
        'cmdAddPart
        '
        '
        'cmdDeletePart
        '
        '
        'optChargeable
        '
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustomer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.Location = New System.Drawing.Point(81, 32)
        Me.txtCustomer.Multiline = True
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReadOnly = True
        Me.txtCustomer.Size = New System.Drawing.Size(292, 21)
        Me.txtCustomer.TabIndex = 0
        Me.txtCustomer.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 14)
        Me.Label6.TabIndex = 70
        Me.Label6.Text = "Customer:"
        '
        'labJobMatix
        '
        Me.labJobMatix.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJobMatix.Location = New System.Drawing.Point(286, 3)
        Me.labJobMatix.Name = "labJobMatix"
        Me.labJobMatix.Size = New System.Drawing.Size(101, 27)
        Me.labJobMatix.TabIndex = 74
        Me.labJobMatix.Text = "labJobMatix"
        '
        'labJobStatus
        '
        Me.labJobStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labJobStatus.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJobStatus.ForeColor = System.Drawing.Color.Sienna
        Me.labJobStatus.Location = New System.Drawing.Point(763, 36)
        Me.labJobStatus.Name = "labJobStatus"
        Me.labJobStatus.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labJobStatus.Size = New System.Drawing.Size(196, 30)
        Me.labJobStatus.TabIndex = 75
        Me.labJobStatus.Text = "labJobStatus"
        '
        'FramePrintOpts
        '
        Me.FramePrintOpts.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FramePrintOpts.Controls.Add(Me.chkPrintItemBarcodes)
        Me.FramePrintOpts.Controls.Add(Me.Label24)
        Me.FramePrintOpts.Controls.Add(Me._chkPrtDocs_Report)
        Me.FramePrintOpts.Controls.Add(Me.cboReceiptPrinters)
        Me.FramePrintOpts.Controls.Add(Me.cboColourPrinters)
        Me.FramePrintOpts.Controls.Add(Me.Label17)
        Me.FramePrintOpts.Controls.Add(Me.PictureBox1)
        Me.FramePrintOpts.Controls.Add(Me.optCompleted)
        Me.FramePrintOpts.Controls.Add(Me.LabVersion)
        Me.FramePrintOpts.Controls.Add(Me.optQA)
        Me.FramePrintOpts.Controls.Add(Me.optSuspend)
        Me.FramePrintOpts.Controls.Add(Me.optSaveExit)
        Me.FramePrintOpts.Controls.Add(Me.labTaskNeeded)
        Me.FramePrintOpts.Controls.Add(Me.cmdCancel)
        Me.FramePrintOpts.Controls.Add(Me.cmdFinish)
        Me.FramePrintOpts.Controls.Add(Me._chkPrtDocs_1)
        Me.FramePrintOpts.Controls.Add(Me._chkPrtDocs_0)
        Me.FramePrintOpts.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FramePrintOpts.Location = New System.Drawing.Point(778, 281)
        Me.FramePrintOpts.Name = "FramePrintOpts"
        Me.FramePrintOpts.Size = New System.Drawing.Size(224, 353)
        Me.FramePrintOpts.TabIndex = 78
        Me.FramePrintOpts.TabStop = False
        Me.FramePrintOpts.Text = "Change Job Status as needed:"
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(114, 218)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(96, 33)
        Me.Label24.TabIndex = 107
        Me.Label24.Text = "Reset Job Status as needed:"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(13, 170)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(198, 7)
        Me.Label17.TabIndex = 86
        '
        'optCompleted
        '
        Me.optCompleted.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.optCompleted.Location = New System.Drawing.Point(14, 300)
        Me.optCompleted.Name = "optCompleted"
        Me.optCompleted.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.optCompleted.Size = New System.Drawing.Size(77, 33)
        Me.optCompleted.TabIndex = 39
        Me.optCompleted.TabStop = True
        Me.optCompleted.Text = "Mark as Completed"
        Me.optCompleted.UseVisualStyleBackColor = False
        '
        'optQA
        '
        Me.optQA.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.optQA.Location = New System.Drawing.Point(14, 264)
        Me.optQA.Name = "optQA"
        Me.optQA.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.optQA.Size = New System.Drawing.Size(77, 33)
        Me.optQA.TabIndex = 38
        Me.optQA.TabStop = True
        Me.optQA.Text = "Mark          for QA"
        Me.optQA.UseVisualStyleBackColor = False
        '
        'optSuspend
        '
        Me.optSuspend.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.optSuspend.Location = New System.Drawing.Point(14, 229)
        Me.optSuspend.Name = "optSuspend"
        Me.optSuspend.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.optSuspend.Size = New System.Drawing.Size(77, 33)
        Me.optSuspend.TabIndex = 37
        Me.optSuspend.TabStop = True
        Me.optSuspend.Text = "Suspend"
        Me.optSuspend.UseVisualStyleBackColor = False
        '
        'optSaveExit
        '
        Me.optSaveExit.BackColor = System.Drawing.Color.FromArgb(CType(CType(213, Byte), Integer), CType(CType(207, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.optSaveExit.Location = New System.Drawing.Point(14, 194)
        Me.optSaveExit.Name = "optSaveExit"
        Me.optSaveExit.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.optSaveExit.Size = New System.Drawing.Size(77, 33)
        Me.optSaveExit.TabIndex = 36
        Me.optSaveExit.TabStop = True
        Me.optSaveExit.Text = "Started.."
        Me.optSaveExit.UseVisualStyleBackColor = False
        '
        'cmdCustHistory
        '
        Me.cmdCustHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCustHistory.Location = New System.Drawing.Point(378, 32)
        Me.cmdCustHistory.Name = "cmdCustHistory"
        Me.cmdCustHistory.Size = New System.Drawing.Size(55, 23)
        Me.cmdCustHistory.TabIndex = 1
        Me.cmdCustHistory.Text = "History"
        Me.cmdCustHistory.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.Label16.Location = New System.Drawing.Point(551, 68)
        Me.Label16.Name = "Label16"
        Me.Label16.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.Label16.Size = New System.Drawing.Size(452, 29)
        Me.Label16.TabIndex = 79
        Me.Label16.Text = "Use this form to update the Job Service record with parts, labour and notes as th" &
    "e Job progresses..  NB:  Database is not updated until the Save/Finish cmd is ex" &
    "ecuted."
        '
        'openDlg1
        '
        Me.openDlg1.FileName = "OpenFileDialog1"
        '
        'labWarrantyJob
        '
        Me.labWarrantyJob.BackColor = System.Drawing.Color.DarkViolet
        Me.labWarrantyJob.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labWarrantyJob.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.labWarrantyJob.Location = New System.Drawing.Point(240, 58)
        Me.labWarrantyJob.Name = "labWarrantyJob"
        Me.labWarrantyJob.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.labWarrantyJob.Size = New System.Drawing.Size(69, 37)
        Me.labWarrantyJob.TabIndex = 87
        Me.labWarrantyJob.Text = "Warranty Job"
        Me.labWarrantyJob.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picSubjectMain
        '
        Me.picSubjectMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.picSubjectMain.Location = New System.Drawing.Point(566, 1)
        Me.picSubjectMain.Name = "picSubjectMain"
        Me.picSubjectMain.Size = New System.Drawing.Size(65, 60)
        Me.picSubjectMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSubjectMain.TabIndex = 105
        Me.picSubjectMain.TabStop = False
        '
        'ucChildJobMaint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.picSubjectMain)
        Me.Controls.Add(Me.labWarrantyJob)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.cmdCustHistory)
        Me.Controls.Add(Me.FramePrintOpts)
        Me.Controls.Add(Me.labJobStatus)
        Me.Controls.Add(Me.labJobMatix)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtCustomer)
        Me.Controls.Add(Me.PicSkipped)
        Me.Controls.Add(Me.PicQueried)
        Me.Controls.Add(Me.PicChecked)
        Me.Controls.Add(Me.chkMyJob)
        Me.Controls.Add(Me.txtNomTech)
        Me.Controls.Add(Me.PictureExtraPrint)
        Me.Controls.Add(Me.Picture2)
        Me.Controls.Add(Me.SSTab1)
        Me.Controls.Add(Me.FrameSessionTime)
        Me.Controls.Add(Me.Picture1)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.LabTicket)
        Me.Controls.Add(Me.LabHdr1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "ucChildJobMaint"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Size = New System.Drawing.Size(1010, 644)
        Me._SSTab1_TabPageWork.ResumeLayout(False)
        Me.ssTabWork.ResumeLayout(False)
        Me.TabWorkPage_notes.ResumeLayout(False)
        Me.FrameWorkRecord.ResumeLayout(False)
        Me.FrameWorkRecord.PerformLayout()
        Me.TabWorkPage_service.ResumeLayout(False)
        Me.FrameServiceItems.ResumeLayout(False)
        CType(Me.dgvChecklist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabWorkPage_stock.ResumeLayout(False)
        Me.FrameStockItems.ResumeLayout(False)
        Me.FrameStockItems.PerformLayout()
        CType(Me.PictureExtra, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabWorkPage_Tasks.ResumeLayout(False)
        Me.FrameOtherTasks.ResumeLayout(False)
        Me.FrameOtherTasks.PerformLayout()
        Me._SSTab1_TabPageAttachments.ResumeLayout(False)
        Me.grpBoxItem.ResumeLayout(False)
        Me.grpBoxItem.PerformLayout()
        CType(Me.picMsExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMsWord, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPDF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxAddNew.ResumeLayout(False)
        Me.grpBoxAddNew.PerformLayout()
        Me._SSTab1_TabPageNotif.ResumeLayout(False)
        Me.FrameNotified.ResumeLayout(False)
        Me.FrameNotified.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicSkipped, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicQueried, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicChecked, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureExtraPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPageJob.ResumeLayout(False)
        Me.frameProblem.ResumeLayout(False)
        Me.frameProblem.PerformLayout()
        Me.frameJobDates.ResumeLayout(False)
        Me.frameJobDates.PerformLayout()
        Me.FrameQuotation.ResumeLayout(False)
        Me.FrameSessionTime.ResumeLayout(False)
        Me.FrameSessionTime.PerformLayout()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ListViewParts, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkPrtDocs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdAddPart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdDeletePart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optChargeable, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtStaffName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FramePrintOpts.ResumeLayout(False)
        CType(Me.picSubjectMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvChecklist As System.Windows.Forms.DataGridView
    Friend WithEvents _SSTab1_TabPageJob As System.Windows.Forms.TabPage
    Public WithEvents FrameQuotation As System.Windows.Forms.GroupBox
    Public WithEvents ListViewExtraParts As System.Windows.Forms.ListView
    Public WithEvents ListViewQuote As System.Windows.Forms.ListView
    Public WithEvents LabReconciliation As System.Windows.Forms.Label
    Public WithEvents LabExtraParts As System.Windows.Forms.Label
    Public WithEvents LabQuotedParts As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents labFullCost As System.Windows.Forms.Label
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents frameProblem As System.Windows.Forms.GroupBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtGoods As System.Windows.Forms.TextBox
    Public WithEvents txtDiagnosis As System.Windows.Forms.TextBox
    Public WithEvents LabComments As System.Windows.Forms.Label
    Friend WithEvents labInstructions As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtSymptoms As System.Windows.Forms.TextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ChkRecovDisk As System.Windows.Forms.CheckBox
    Friend WithEvents ChkBackupReq As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents labPriority As System.Windows.Forms.Label
    Friend WithEvents labJobMatix As System.Windows.Forms.Label
    Friend WithEvents labJobStatus As System.Windows.Forms.Label
    Friend WithEvents frameJobDates As System.Windows.Forms.GroupBox
    Friend WithEvents labDeliveredBy As System.Windows.Forms.Label
    Friend WithEvents labDateUpdated As System.Windows.Forms.Label
    Friend WithEvents LabRcvdStaff As System.Windows.Forms.Label
    Friend WithEvents labDateCreated As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents _txtStaffName_1 As System.Windows.Forms.TextBox
    Friend WithEvents _txtStaffName_0 As System.Windows.Forms.TextBox
    Public WithEvents LabJobReturned As System.Windows.Forms.Label
    Public WithEvents chkReturned As System.Windows.Forms.CheckBox
    Friend WithEvents FramePrintOpts As System.Windows.Forms.GroupBox
    Public WithEvents _chkPrtDocs_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrtDocs_0 As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdFinish As System.Windows.Forms.Button
    Friend WithEvents cmdCustHistory As System.Windows.Forms.Button
    Friend WithEvents Symbol As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Comments As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DateUpdated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Staff As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrevStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PrevComments As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents labTaskNeeded As System.Windows.Forms.Label
    Friend WithEvents optSaveExit As System.Windows.Forms.RadioButton
    Friend WithEvents optSuspend As System.Windows.Forms.RadioButton
    Friend WithEvents optQA As System.Windows.Forms.RadioButton
    Friend WithEvents optCompleted As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents cmdClearSessionTimes As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents ssTabWork As System.Windows.Forms.TabControl
    Friend WithEvents TabWorkPage_notes As System.Windows.Forms.TabPage
    Friend WithEvents TabWorkPage_service As System.Windows.Forms.TabPage
    Friend WithEvents TabWorkPage_stock As System.Windows.Forms.TabPage
    Friend WithEvents TabWorkPage_Tasks As System.Windows.Forms.TabPage
    Friend WithEvents grpBoxAddNew As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewFileName As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents labHelp As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents btnSaveAttachment As System.Windows.Forms.Button
    Friend WithEvents txtNewComment As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents grpBoxItem As System.Windows.Forms.GroupBox
    Friend WithEvents picPDF As System.Windows.Forms.PictureBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents btnViewDoc As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents picProduct As System.Windows.Forms.PictureBox
    Friend WithEvents lvwDocs As System.Windows.Forms.ListView
    Friend WithEvents doc_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents doc_date_created As System.Windows.Forms.ColumnHeader
    Friend WithEvents doc_file_title As System.Windows.Forms.ColumnHeader
    Friend WithEvents doc_staff As System.Windows.Forms.ColumnHeader
    Friend WithEvents openDlg1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cboColourPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents cboReceiptPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents doc_file_size As System.Windows.Forms.ColumnHeader
    Public WithEvents _chkPrtDocs_Report As System.Windows.Forms.CheckBox
    Friend WithEvents labWarrantyJob As System.Windows.Forms.Label
    Friend WithEvents picSubjectMain As System.Windows.Forms.PictureBox
    Public WithEvents cboTimeSelectTenths As System.Windows.Forms.ComboBox
    Friend WithEvents labResultHours As System.Windows.Forms.Label
    Friend WithEvents labMinCharge As System.Windows.Forms.Label
    Friend WithEvents picMsExcel As System.Windows.Forms.PictureBox
    Friend WithEvents picMsWord As System.Windows.Forms.PictureBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents chkPrintItemBarcodes As CheckBox
#End Region
End Class