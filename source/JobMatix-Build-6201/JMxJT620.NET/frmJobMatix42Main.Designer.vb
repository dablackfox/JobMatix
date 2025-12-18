<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmJobMatix42Main
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        Me.mbIsInitialising = True
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        '-- grh-
        Me.mbIsInitialising = False
        Me.mlResultsTop = frameJobsTab.Top
        Me.mlResultsLeft = frameJobsTab.Left
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
    Public WithEvents cmdSignoff As System.Windows.Forms.Button
    Public WithEvents txtDetailsHdr As System.Windows.Forms.TextBox
    Public WithEvents toolbarJobView As System.Windows.Forms.ToolStrip
    Public WithEvents txtFindCust As System.Windows.Forms.TextBox
    Public WithEvents listViewCustJobs As System.Windows.Forms.ListView
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents labJobHistory As System.Windows.Forms.Label
    Public WithEvents labRecCountCust As System.Windows.Forms.Label
    Public WithEvents labFindCust As System.Windows.Forms.Label
    Public WithEvents frameCustomers As System.Windows.Forms.GroupBox
    Public WithEvents cboPriority As System.Windows.Forms.ComboBox
    Public WithEvents cmdChangePriority As System.Windows.Forms.Button
    Public WithEvents rtfJobDetails As System.Windows.Forms.RichTextBox
    Public WithEvents LabDetailsJobNo As System.Windows.Forms.Label
    Public WithEvents LabDetailPriority As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents LabDetailStatus As System.Windows.Forms.Label
    Public WithEvents LabDetailStatus2 As System.Windows.Forms.Label
    Public WithEvents labLabStatus As System.Windows.Forms.Label
    Public WithEvents FrameJobDetails As System.Windows.Forms.GroupBox
    Public WithEvents txtSearch As System.Windows.Forms.TextBox
    Public WithEvents cmdClearSearch As System.Windows.Forms.Button
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents txtFind As System.Windows.Forms.TextBox
    Public WithEvents Picture3 As System.Windows.Forms.PictureBox
    Public WithEvents _ToolbarJobs_Button1 As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarJobs_Button2 As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarJobs_Button3 As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarJobs_Button4 As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarJobs_Button5 As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarJobs_Button6 As System.Windows.Forms.ToolStripButton
    Public WithEvents ToolbarJobs As System.Windows.Forms.ToolStrip
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents LabSearch As System.Windows.Forms.Label
    Public WithEvents LabFind As System.Windows.Forms.Label
    Public WithEvents labRecCount As System.Windows.Forms.Label
    Public WithEvents _LabSearchJobs_0 As System.Windows.Forms.Label
    Public WithEvents LabTitle As System.Windows.Forms.Label
    Public WithEvents FrameBrowse As System.Windows.Forms.GroupBox
    Public WithEvents labJobsExplorer As System.Windows.Forms.Label
    Public WithEvents FrameJobsTree2 As System.Windows.Forms.GroupBox
    Public WithEvents FrameJobsTree As System.Windows.Forms.GroupBox
    Public WithEvents frameJobsTab As System.Windows.Forms.GroupBox
    Public WithEvents ImageListTree As System.Windows.Forms.ImageList
    Public WithEvents Picture2 As System.Windows.Forms.PictureBox
    Public WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents Picture1 As System.Windows.Forms.PictureBox
    Public WithEvents ListResults As System.Windows.Forms.ListBox
    Public dlg1Save As System.Windows.Forms.SaveFileDialog
    Public WithEvents ListNames As System.Windows.Forms.ListBox
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents LabStaffName2 As System.Windows.Forms.Label
    Public WithEvents LabBusinessId As System.Windows.Forms.Label
    Public WithEvents LabBusiness As System.Windows.Forms.Label
    Public WithEvents LabToday As System.Windows.Forms.Label
    Public WithEvents LabSearchJobs As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptJobsOrder As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents mnuPrinters As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuFileSep050 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuAutoSignOffOptions As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuPreferences As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuFileSep070 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuDatabase As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuNewJob As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuJobsSep028 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuNew As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuJobs As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuShowJobParts As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuJobsSep035 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuFindPart As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuJobParts As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuJobsSep050 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuSerialAudit As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuParts As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuCustomerHistory As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuCustomers As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuGoodsAcceptedTypes As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuBrands As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuProblemSymptoms As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuServiceTaskTypes As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuEditRefTables As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuModelCheckList As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuReference As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuResetJetPath As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAdminSep0 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuBackupJobsDB As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAdminSep1 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuAdminSep13 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuAdminDatabase As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAdminSep10 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuSetupInfo As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAdminSep111 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuSMSUpdate As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuSetup As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAdminSep11 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuShowSystemInfo As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAdminSep12 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuUpdSysInfo As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuSystemInfo As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAdminSep15 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuShowUsers As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAdminSep17 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents mnuAddNewUser As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuUsers As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents mnuAdmin As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJobMatix42Main))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdChangePriority = New System.Windows.Forms.Button()
        Me.cmdClearSearch = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdCustSearch = New System.Windows.Forms.Button()
        Me.cmdClearCustSearch = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmdBuildQuote = New System.Windows.Forms.Button()
        Me.picJobDetailReturned = New System.Windows.Forms.PictureBox()
        Me.chkShowCompany1st = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.chkHideWaitList = New System.Windows.Forms.CheckBox()
        Me.labReminderStatus = New System.Windows.Forms.Label()
        Me.cmdSignoff = New System.Windows.Forms.Button()
        Me.frameJobsTab = New System.Windows.Forms.GroupBox()
        Me.TabControlJobTracking = New System.Windows.Forms.TabControl()
        Me.TabPageJobsTree = New System.Windows.Forms.TabPage()
        Me.FrameJobsTree = New System.Windows.Forms.GroupBox()
        Me.frameLegend = New System.Windows.Forms.GroupBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.picWaitlisted = New System.Windows.Forms.PictureBox()
        Me.picWarranty = New System.Windows.Forms.PictureBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabTreeStatus = New System.Windows.Forms.Label()
        Me.PictureReturned = New System.Windows.Forms.PictureBox()
        Me.PicturePriority3 = New System.Windows.Forms.PictureBox()
        Me.LabDeliveredOrder = New System.Windows.Forms.Label()
        Me.FrameJobsTree2 = New System.Windows.Forms.GroupBox()
        Me.tvwJobs = New JobMatix3.clsMyTreeView()
        Me.ImageListTree = New System.Windows.Forms.ImageList(Me.components)
        Me.labJobsFilter = New System.Windows.Forms.Label()
        Me.cboJobsFilter = New System.Windows.Forms.ComboBox()
        Me.panelReminder = New System.Windows.Forms.Panel()
        Me.labOnSiteReminder = New System.Windows.Forms.Label()
        Me.cmdDismissReminder = New System.Windows.Forms.Button()
        Me.labReminder = New System.Windows.Forms.Label()
        Me.cboJobsOrder = New System.Windows.Forms.ComboBox()
        Me.LabJobsOrder = New System.Windows.Forms.Label()
        Me.cmdRefreshJobsTree = New System.Windows.Forms.Button()
        Me.labJobsExplorer = New System.Windows.Forms.Label()
        Me.TabPageOnsite = New System.Windows.Forms.TabPage()
        Me.frameOnSite = New System.Windows.Forms.GroupBox()
        Me.cmdRefreshOnSite = New System.Windows.Forms.Button()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.labRecCountOnSite = New System.Windows.Forms.Label()
        Me.dataGridViewOnSite = New System.Windows.Forms.DataGridView()
        Me.datePromised = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Customer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.techName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.jobNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.JobStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPageJobsGrid = New System.Windows.Forms.TabPage()
        Me.FrameBrowse = New System.Windows.Forms.GroupBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.DataGridViewJobs = New System.Windows.Forms.DataGridView()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.ToolbarJobs = New System.Windows.Forms.ToolStrip()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me._ToolbarJobs_Button1 = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarJobs_Button2 = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarJobs_Button3 = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarJobs_ButtonQA = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarJobs_Button4 = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarJobs_Button5 = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarJobs_Button6 = New System.Windows.Forms.ToolStripButton()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.LabSearch = New System.Windows.Forms.Label()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.labRecCount = New System.Windows.Forms.Label()
        Me._LabSearchJobs_0 = New System.Windows.Forms.Label()
        Me.LabTitle = New System.Windows.Forms.Label()
        Me.TabPageCustomers = New System.Windows.Forms.TabPage()
        Me.frameCustomers = New System.Windows.Forms.GroupBox()
        Me.btnCustNewCustomer = New System.Windows.Forms.Button()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtCustSearch = New System.Windows.Forms.TextBox()
        Me.DataGridViewCust = New System.Windows.Forms.DataGridView()
        Me.txtFindCust = New System.Windows.Forms.TextBox()
        Me.listViewCustJobs = New System.Windows.Forms.ListView()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.labJobHistory = New System.Windows.Forms.Label()
        Me.labRecCountCust = New System.Windows.Forms.Label()
        Me.labFindCust = New System.Windows.Forms.Label()
        Me.FrameJobDetails = New System.Windows.Forms.GroupBox()
        Me.labDetailWarrantyJob = New System.Windows.Forms.Label()
        Me.picAttachments = New System.Windows.Forms.PictureBox()
        Me.labDetailOnSiteJob = New System.Windows.Forms.Label()
        Me.labDetailJobDue = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.labDetailDateUpdated = New System.Windows.Forms.Label()
        Me.labDetailUpdatedDescription = New System.Windows.Forms.Label()
        Me.labDetailTech = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.labDetailDatePromised = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.labDetailDateCreated = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LabDetailsJobNo = New System.Windows.Forms.Label()
        Me.ToolStripJobAction = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCheckIn = New System.Windows.Forms.ToolStripButton()
        Me.btnAmend = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUpdate = New System.Windows.Forms.ToolStripButton()
        Me.btnReturnToQueue = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnReOpen = New System.Windows.Forms.ToolStripButton()
        Me.btnDeliver = New System.Windows.Forms.ToolStripButton()
        Me.btnDetailNotify = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnStopPress = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.LabDetailStatus2 = New System.Windows.Forms.Label()
        Me.cboPriority = New System.Windows.Forms.ComboBox()
        Me.LabDetailStatus = New System.Windows.Forms.Label()
        Me.rtfJobDetails = New System.Windows.Forms.RichTextBox()
        Me.labLabStatus = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabDetailPriority = New System.Windows.Forms.Label()
        Me.frameMainCmds = New System.Windows.Forms.GroupBox()
        Me.labMainCustTags = New System.Windows.Forms.Label()
        Me.toolbarNewJob2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAcceptJob = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnOnSiteJob = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHistory = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtDetailsHdr = New System.Windows.Forms.TextBox()
        Me.picNoCustRecord = New System.Windows.Forms.PictureBox()
        Me.labEmptyJobPanel = New System.Windows.Forms.Label()
        Me.toolbarJobView = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripDropDownGridFont = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripMenuItemFontSize_8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemFontSize_9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemFontSize_10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me._toolbarJobView_ButtonBackup = New System.Windows.Forms.ToolStripButton()
        Me.Picture3 = New System.Windows.Forms.PictureBox()
        Me.frameQuotes = New System.Windows.Forms.GroupBox()
        Me.labLoadingQuotes = New System.Windows.Forms.Label()
        Me.chkRecentQuotes = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.labQuoteCount = New System.Windows.Forms.Label()
        Me.cmdRefreshQuotes = New System.Windows.Forms.Button()
        Me.chkNewQuotes = New System.Windows.Forms.CheckBox()
        Me.LabQuotesHdr = New System.Windows.Forms.Label()
        Me.ListViewSalesOrders = New System.Windows.Forms.ListView()
        Me.frameQuoteDetails = New System.Windows.Forms.GroupBox()
        Me.labQuoteCanBuild = New System.Windows.Forms.Label()
        Me.labJoblist = New System.Windows.Forms.Label()
        Me.labOrderDetail = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labQuoteOrderNo = New System.Windows.Forms.Label()
        Me.ListViewQuote = New System.Windows.Forms.ListView()
        Me.Picture2 = New System.Windows.Forms.PictureBox()
        Me.Picture1 = New System.Windows.Forms.PictureBox()
        Me.ListResults = New System.Windows.Forms.ListBox()
        Me.dlg1Save = New System.Windows.Forms.SaveFileDialog()
        Me.ListNames = New System.Windows.Forms.ListBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabStaffName2 = New System.Windows.Forms.Label()
        Me.LabBusinessId = New System.Windows.Forms.Label()
        Me.LabBusiness = New System.Windows.Forms.Label()
        Me.LabToday = New System.Windows.Forms.Label()
        Me.LabSearchJobs = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptJobsOrder = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.MainMenu1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPrinters = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPrinterAssignments = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileSep050 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPreferences = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoSignOffOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStaySignedOn = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLongSignOff = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShortSignOff = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDontShowNotifyReminder = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStartupMaximised = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGridFontSizes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGridFont_8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGridFont_9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGridFont_10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFileSep070 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuFileRetailManagerDb = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDatabase = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuJobMatix = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBackupJobTrackingDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSqlServer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWhoUsing = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDbRetailManager = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuJobs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewJob = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuJobsSep028 = New System.Windows.Forms.ToolStripSeparator()
        Me.NewOnSiteJobToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuParts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuJobParts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFindPart = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuJobsSep035 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuShowJobParts = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuJobsSep050 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSerialAudit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCustomers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCustomerHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReference = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditRefTables = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuGoodsAcceptedTypes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBrands = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProblemSymptoms = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuServiceTaskTypes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuModelCheckList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdmin = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuResetJetPath = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdminSep0 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAdminDatabase = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBackupJobsDB = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdminSep1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAdminSep13 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAdminSep10 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSetup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSetupInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdminSep111 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSMSUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEnableSMSReminders = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoAssignOrphanJobsOnUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdminSep11 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSystemInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShowSystemInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdminSep12 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuUpdSysInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdminSep15 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuUsers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuShowUsers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAdminSep17 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAddNewUser = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutJobMatix32ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.labVersion = New System.Windows.Forms.Label()
        Me.labStaffLabel = New System.Windows.Forms.Label()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.panelSignOn = New System.Windows.Forms.Panel()
        Me.labStaffTimeRemaining = New System.Windows.Forms.Label()
        Me.txtStaffBarcode = New System.Windows.Forms.TextBox()
        Me.BackgroundWorkerSearch = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorkerGetSchema = New System.ComponentModel.BackgroundWorker()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.TabPageJobTracking = New System.Windows.Forms.TabPage()
        Me.TabPageQuoteJobs = New System.Windows.Forms.TabPage()
        Me.frameQuoteCustomer = New System.Windows.Forms.GroupBox()
        Me.txtQuoteDetailsHdr = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.BackgroundWorkerSqlConnect = New System.ComponentModel.BackgroundWorker()
        Me.ToolTipMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnLaunchRAs = New System.Windows.Forms.Button()
        Me.openDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.TimerRAs = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundWorkerExchange201 = New System.ComponentModel.BackgroundWorker()
        Me.timerExchange2 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.picJobDetailReturned, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frameJobsTab.SuspendLayout()
        Me.TabControlJobTracking.SuspendLayout()
        Me.TabPageJobsTree.SuspendLayout()
        Me.FrameJobsTree.SuspendLayout()
        Me.frameLegend.SuspendLayout()
        CType(Me.picWaitlisted, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picWarranty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureReturned, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicturePriority3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FrameJobsTree2.SuspendLayout()
        Me.panelReminder.SuspendLayout()
        Me.TabPageOnsite.SuspendLayout()
        Me.frameOnSite.SuspendLayout()
        CType(Me.dataGridViewOnSite, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageJobsGrid.SuspendLayout()
        Me.FrameBrowse.SuspendLayout()
        CType(Me.DataGridViewJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolbarJobs.SuspendLayout()
        Me.TabPageCustomers.SuspendLayout()
        Me.frameCustomers.SuspendLayout()
        CType(Me.DataGridViewCust, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FrameJobDetails.SuspendLayout()
        CType(Me.picAttachments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripJobAction.SuspendLayout()
        Me.frameMainCmds.SuspendLayout()
        Me.toolbarNewJob2.SuspendLayout()
        CType(Me.picNoCustRecord, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.toolbarJobView.SuspendLayout()
        CType(Me.Picture3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frameQuotes.SuspendLayout()
        Me.frameQuoteDetails.SuspendLayout()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LabSearchJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptJobsOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MainMenu1.SuspendLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelSignOn.SuspendLayout()
        Me.TabControlMain.SuspendLayout()
        Me.TabPageJobTracking.SuspendLayout()
        Me.TabPageQuoteJobs.SuspendLayout()
        Me.frameQuoteCustomer.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 5000
        Me.ToolTip1.InitialDelay = 300
        Me.ToolTip1.ReshowDelay = 100
        Me.ToolTip1.ShowAlways = True
        '
        'cmdChangePriority
        '
        Me.cmdChangePriority.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdChangePriority.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdChangePriority.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdChangePriority.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdChangePriority.Location = New System.Drawing.Point(184, 69)
        Me.cmdChangePriority.Name = "cmdChangePriority"
        Me.cmdChangePriority.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdChangePriority.Size = New System.Drawing.Size(26, 17)
        Me.cmdChangePriority.TabIndex = 49
        Me.cmdChangePriority.Text = ">"
        Me.ToolTip1.SetToolTip(Me.cmdChangePriority, "Change Job Priority.")
        Me.cmdChangePriority.UseVisualStyleBackColor = False
        '
        'cmdClearSearch
        '
        Me.cmdClearSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClearSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearSearch.Location = New System.Drawing.Point(462, 66)
        Me.cmdClearSearch.Name = "cmdClearSearch"
        Me.cmdClearSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearSearch.Size = New System.Drawing.Size(65, 21)
        Me.cmdClearSearch.TabIndex = 42
        Me.cmdClearSearch.Text = "Clear"
        Me.ToolTip1.SetToolTip(Me.cmdClearSearch, "Clear Search Text and Refresh Grid.")
        Me.cmdClearSearch.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Location = New System.Drawing.Point(462, 93)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(65, 21)
        Me.cmdSearch.TabIndex = 41
        Me.cmdSearch.Text = "Search"
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search/Refresh Jobs Grid.")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdCustSearch
        '
        Me.cmdCustSearch.BackColor = System.Drawing.Color.Lavender
        Me.cmdCustSearch.CausesValidation = False
        Me.cmdCustSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCustSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCustSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCustSearch.Location = New System.Drawing.Point(405, 51)
        Me.cmdCustSearch.Name = "cmdCustSearch"
        Me.cmdCustSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCustSearch.Size = New System.Drawing.Size(65, 21)
        Me.cmdCustSearch.TabIndex = 3
        Me.cmdCustSearch.Text = "Search"
        Me.cmdCustSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCustSearch, "Search/Refresh customer list.")
        Me.cmdCustSearch.UseVisualStyleBackColor = False
        '
        'cmdClearCustSearch
        '
        Me.cmdClearCustSearch.BackColor = System.Drawing.Color.Lavender
        Me.cmdClearCustSearch.CausesValidation = False
        Me.cmdClearCustSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearCustSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearCustSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearCustSearch.Location = New System.Drawing.Point(405, 25)
        Me.cmdClearCustSearch.Name = "cmdClearCustSearch"
        Me.cmdClearCustSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearCustSearch.Size = New System.Drawing.Size(65, 21)
        Me.cmdClearCustSearch.TabIndex = 2
        Me.cmdClearCustSearch.Text = "X Clear"
        Me.ToolTip1.SetToolTip(Me.cmdClearCustSearch, "Clear Search Text and refresh grid..")
        Me.cmdClearCustSearch.UseVisualStyleBackColor = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label16.Location = New System.Drawing.Point(115, 43)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(61, 15)
        Me.Label16.TabIndex = 36
        Me.Label16.Text = "Attention"
        Me.ToolTip1.SetToolTip(Me.Label16, "Stop Press or other update pending..")
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(240, 35)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(41, 17)
        Me.Label17.TabIndex = 38
        Me.Label17.Text = "Priority"
        Me.ToolTip1.SetToolTip(Me.Label17, "Stop Press or other update pending..")
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(343, 36)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(66, 18)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Return Job"
        Me.ToolTip1.SetToolTip(Me.Label18, "Stop Press or other update pending..")
        '
        'cmdBuildQuote
        '
        Me.cmdBuildQuote.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(204, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdBuildQuote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdBuildQuote.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBuildQuote.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBuildQuote.Image = CType(resources.GetObject("cmdBuildQuote.Image"), System.Drawing.Image)
        Me.cmdBuildQuote.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.cmdBuildQuote.Location = New System.Drawing.Point(328, 15)
        Me.cmdBuildQuote.Name = "cmdBuildQuote"
        Me.cmdBuildQuote.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBuildQuote.Size = New System.Drawing.Size(107, 37)
        Me.cmdBuildQuote.TabIndex = 80
        Me.cmdBuildQuote.Text = "Go to Quotes"
        Me.cmdBuildQuote.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.cmdBuildQuote, "Create Jobs to Build from Quotation")
        Me.cmdBuildQuote.UseVisualStyleBackColor = False
        '
        'picJobDetailReturned
        '
        Me.picJobDetailReturned.BackColor = System.Drawing.Color.White
        Me.picJobDetailReturned.Cursor = System.Windows.Forms.Cursors.Default
        Me.picJobDetailReturned.ForeColor = System.Drawing.SystemColors.ControlText
        Me.picJobDetailReturned.Image = CType(resources.GetObject("picJobDetailReturned.Image"), System.Drawing.Image)
        Me.picJobDetailReturned.Location = New System.Drawing.Point(152, 22)
        Me.picJobDetailReturned.Name = "picJobDetailReturned"
        Me.picJobDetailReturned.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.picJobDetailReturned.Size = New System.Drawing.Size(16, 16)
        Me.picJobDetailReturned.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picJobDetailReturned.TabIndex = 70
        Me.picJobDetailReturned.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picJobDetailReturned, "Job Returned")
        '
        'chkShowCompany1st
        '
        Me.chkShowCompany1st.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkShowCompany1st.CausesValidation = False
        Me.chkShowCompany1st.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowCompany1st.Location = New System.Drawing.Point(382, 11)
        Me.chkShowCompany1st.Name = "chkShowCompany1st"
        Me.chkShowCompany1st.Size = New System.Drawing.Size(81, 32)
        Me.chkShowCompany1st.TabIndex = 91
        Me.chkShowCompany1st.Text = "Company  Name 1st"
        Me.ToolTip1.SetToolTip(Me.chkShowCompany1st, "Check this box to show the Company name in front of the Customer's  name on Job N" &
        "ode text.")
        Me.ToolTipMain.SetToolTip(Me.chkShowCompany1st, "Check this box to show the Company name in front of the Customer's  name on Job N" &
        "ode text.")
        Me.chkShowCompany1st.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(289, 36)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(47, 17)
        Me.Label25.TabIndex = 45
        Me.Label25.Text = "Wty Job"
        Me.ToolTip1.SetToolTip(Me.Label25, "Stop Press or other update pending..")
        '
        'chkHideWaitList
        '
        Me.chkHideWaitList.BackColor = System.Drawing.Color.LavenderBlush
        Me.chkHideWaitList.CausesValidation = False
        Me.chkHideWaitList.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHideWaitList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHideWaitList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHideWaitList.Location = New System.Drawing.Point(196, 11)
        Me.chkHideWaitList.Name = "chkHideWaitList"
        Me.chkHideWaitList.Padding = New System.Windows.Forms.Padding(2, 0, 0, 0)
        Me.chkHideWaitList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHideWaitList.Size = New System.Drawing.Size(79, 34)
        Me.chkHideWaitList.TabIndex = 89
        Me.chkHideWaitList.Text = "Hide WaitList"
        Me.ToolTip1.SetToolTip(Me.chkHideWaitList, "Show myJobs only in Explorer Tree..")
        Me.ToolTipMain.SetToolTip(Me.chkHideWaitList, "Show myJobs only in Explorer Tree..")
        Me.chkHideWaitList.UseVisualStyleBackColor = False
        '
        'labReminderStatus
        '
        Me.labReminderStatus.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labReminderStatus.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.labReminderStatus.Location = New System.Drawing.Point(846, 34)
        Me.labReminderStatus.Name = "labReminderStatus"
        Me.labReminderStatus.Size = New System.Drawing.Size(212, 36)
        Me.labReminderStatus.TabIndex = 111
        Me.labReminderStatus.Text = "labReminderStatus"
        Me.ToolTipMain.SetToolTip(Me.labReminderStatus, "Shows Staff SMS Reminder BG Task status-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "If errors occur,  check Gateway Provide" &
        "r Name and credentials," & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " and then check for sufficient available credit." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Then " &
        "exit and restart JobMatix..")
        '
        'cmdSignoff
        '
        Me.cmdSignoff.BackColor = System.Drawing.Color.LavenderBlush
        Me.cmdSignoff.CausesValidation = False
        Me.cmdSignoff.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSignoff.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSignoff.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSignoff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSignoff.Location = New System.Drawing.Point(133, 6)
        Me.cmdSignoff.Name = "cmdSignoff"
        Me.cmdSignoff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSignoff.Size = New System.Drawing.Size(46, 44)
        Me.cmdSignoff.TabIndex = 1
        Me.cmdSignoff.Text = "Sign Off"
        Me.ToolTipMain.SetToolTip(Me.cmdSignoff, "RM Staff Sign Off")
        Me.cmdSignoff.UseVisualStyleBackColor = False
        '
        'frameJobsTab
        '
        Me.frameJobsTab.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameJobsTab.CausesValidation = False
        Me.frameJobsTab.Controls.Add(Me.TabControlJobTracking)
        Me.frameJobsTab.Controls.Add(Me.FrameJobDetails)
        Me.frameJobsTab.Controls.Add(Me.frameMainCmds)
        Me.frameJobsTab.Controls.Add(Me.labEmptyJobPanel)
        Me.frameJobsTab.Controls.Add(Me.toolbarJobView)
        Me.frameJobsTab.Controls.Add(Me.Picture3)
        Me.frameJobsTab.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameJobsTab.Location = New System.Drawing.Point(3, 3)
        Me.frameJobsTab.Name = "frameJobsTab"
        Me.frameJobsTab.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameJobsTab.Size = New System.Drawing.Size(1056, 607)
        Me.frameJobsTab.TabIndex = 17
        Me.frameJobsTab.TabStop = False
        Me.frameJobsTab.Text = "frameJobsTab"
        Me.frameJobsTab.Visible = False
        '
        'TabControlJobTracking
        '
        Me.TabControlJobTracking.Controls.Add(Me.TabPageJobsTree)
        Me.TabControlJobTracking.Controls.Add(Me.TabPageOnsite)
        Me.TabControlJobTracking.Controls.Add(Me.TabPageJobsGrid)
        Me.TabControlJobTracking.Controls.Add(Me.TabPageCustomers)
        Me.TabControlJobTracking.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlJobTracking.Location = New System.Drawing.Point(3, 3)
        Me.TabControlJobTracking.Name = "TabControlJobTracking"
        Me.TabControlJobTracking.SelectedIndex = 0
        Me.TabControlJobTracking.Size = New System.Drawing.Size(559, 441)
        Me.TabControlJobTracking.TabIndex = 97
        '
        'TabPageJobsTree
        '
        Me.TabPageJobsTree.BackColor = System.Drawing.Color.White
        Me.TabPageJobsTree.Controls.Add(Me.FrameJobsTree)
        Me.TabPageJobsTree.Location = New System.Drawing.Point(4, 25)
        Me.TabPageJobsTree.Name = "TabPageJobsTree"
        Me.TabPageJobsTree.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageJobsTree.Size = New System.Drawing.Size(551, 412)
        Me.TabPageJobsTree.TabIndex = 0
        Me.TabPageJobsTree.Text = "- Active Jobs Tree -"
        '
        'FrameJobsTree
        '
        Me.FrameJobsTree.BackColor = System.Drawing.Color.White
        Me.FrameJobsTree.CausesValidation = False
        Me.FrameJobsTree.Controls.Add(Me.frameLegend)
        Me.FrameJobsTree.Controls.Add(Me.FrameJobsTree2)
        Me.FrameJobsTree.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameJobsTree.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameJobsTree.Location = New System.Drawing.Point(2, 2)
        Me.FrameJobsTree.Name = "FrameJobsTree"
        Me.FrameJobsTree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameJobsTree.Size = New System.Drawing.Size(542, 395)
        Me.FrameJobsTree.TabIndex = 18
        Me.FrameJobsTree.TabStop = False
        Me.FrameJobsTree.Text = "frameJobsTree"
        '
        'frameLegend
        '
        Me.frameLegend.BackColor = System.Drawing.Color.White
        Me.frameLegend.Controls.Add(Me.Label26)
        Me.frameLegend.Controls.Add(Me.picWaitlisted)
        Me.frameLegend.Controls.Add(Me.Label25)
        Me.frameLegend.Controls.Add(Me.picWarranty)
        Me.frameLegend.Controls.Add(Me.Label11)
        Me.frameLegend.Controls.Add(Me.Label2)
        Me.frameLegend.Controls.Add(Me.LabTreeStatus)
        Me.frameLegend.Controls.Add(Me.Label18)
        Me.frameLegend.Controls.Add(Me.PictureReturned)
        Me.frameLegend.Controls.Add(Me.Label17)
        Me.frameLegend.Controls.Add(Me.PicturePriority3)
        Me.frameLegend.Controls.Add(Me.Label16)
        Me.frameLegend.Controls.Add(Me.LabDeliveredOrder)
        Me.frameLegend.Location = New System.Drawing.Point(8, 104)
        Me.frameLegend.Name = "frameLegend"
        Me.frameLegend.Size = New System.Drawing.Size(528, 59)
        Me.frameLegend.TabIndex = 36
        Me.frameLegend.TabStop = False
        Me.frameLegend.Text = "frameLegend"
        '
        'Label26
        '
        Me.Label26.Location = New System.Drawing.Point(186, 35)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(46, 11)
        Me.Label26.TabIndex = 47
        Me.Label26.Text = "Wait-list"
        '
        'picWaitlisted
        '
        Me.picWaitlisted.Image = CType(resources.GetObject("picWaitlisted.Image"), System.Drawing.Image)
        Me.picWaitlisted.Location = New System.Drawing.Point(195, 11)
        Me.picWaitlisted.Name = "picWaitlisted"
        Me.picWaitlisted.Size = New System.Drawing.Size(17, 19)
        Me.picWaitlisted.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picWaitlisted.TabIndex = 46
        Me.picWaitlisted.TabStop = False
        '
        'picWarranty
        '
        Me.picWarranty.Image = CType(resources.GetObject("picWarranty.Image"), System.Drawing.Image)
        Me.picWarranty.Location = New System.Drawing.Point(299, 13)
        Me.picWarranty.Name = "picWarranty"
        Me.picWarranty.Size = New System.Drawing.Size(22, 20)
        Me.picWarranty.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picWarranty.TabIndex = 44
        Me.picWarranty.TabStop = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(117, 26)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 13)
        Me.Label11.TabIndex = 43
        Me.Label11.Text = "Overdue"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkOrange
        Me.Label2.Location = New System.Drawing.Point(117, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Job Due"
        '
        'LabTreeStatus
        '
        Me.LabTreeStatus.BackColor = System.Drawing.Color.Transparent
        Me.LabTreeStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabTreeStatus.Font = New System.Drawing.Font("Tahoma", 7.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabTreeStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabTreeStatus.Location = New System.Drawing.Point(413, 9)
        Me.LabTreeStatus.Name = "LabTreeStatus"
        Me.LabTreeStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabTreeStatus.Size = New System.Drawing.Size(79, 43)
        Me.LabTreeStatus.TabIndex = 41
        Me.LabTreeStatus.Text = "LabTreeStatus"
        '
        'PictureReturned
        '
        Me.PictureReturned.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.PictureReturned.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureReturned.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PictureReturned.Image = CType(resources.GetObject("PictureReturned.Image"), System.Drawing.Image)
        Me.PictureReturned.Location = New System.Drawing.Point(353, 16)
        Me.PictureReturned.Name = "PictureReturned"
        Me.PictureReturned.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PictureReturned.Size = New System.Drawing.Size(16, 16)
        Me.PictureReturned.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureReturned.TabIndex = 39
        Me.PictureReturned.TabStop = False
        '
        'PicturePriority3
        '
        Me.PicturePriority3.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.PicturePriority3.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicturePriority3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PicturePriority3.Image = CType(resources.GetObject("PicturePriority3.Image"), System.Drawing.Image)
        Me.PicturePriority3.Location = New System.Drawing.Point(243, 14)
        Me.PicturePriority3.Name = "PicturePriority3"
        Me.PicturePriority3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PicturePriority3.Size = New System.Drawing.Size(16, 16)
        Me.PicturePriority3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PicturePriority3.TabIndex = 37
        Me.PicturePriority3.TabStop = False
        '
        'LabDeliveredOrder
        '
        Me.LabDeliveredOrder.BackColor = System.Drawing.Color.Transparent
        Me.LabDeliveredOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDeliveredOrder.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDeliveredOrder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDeliveredOrder.Location = New System.Drawing.Point(6, 11)
        Me.LabDeliveredOrder.Name = "LabDeliveredOrder"
        Me.LabDeliveredOrder.Padding = New System.Windows.Forms.Padding(3)
        Me.LabDeliveredOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDeliveredOrder.Size = New System.Drawing.Size(105, 45)
        Me.LabDeliveredOrder.TabIndex = 34
        Me.LabDeliveredOrder.Text = "Note: Days in custody.."
        '
        'FrameJobsTree2
        '
        Me.FrameJobsTree2.BackColor = System.Drawing.Color.White
        Me.FrameJobsTree2.CausesValidation = False
        Me.FrameJobsTree2.Controls.Add(Me.tvwJobs)
        Me.FrameJobsTree2.Controls.Add(Me.labJobsFilter)
        Me.FrameJobsTree2.Controls.Add(Me.cboJobsFilter)
        Me.FrameJobsTree2.Controls.Add(Me.chkHideWaitList)
        Me.FrameJobsTree2.Controls.Add(Me.panelReminder)
        Me.FrameJobsTree2.Controls.Add(Me.chkShowCompany1st)
        Me.FrameJobsTree2.Controls.Add(Me.cboJobsOrder)
        Me.FrameJobsTree2.Controls.Add(Me.LabJobsOrder)
        Me.FrameJobsTree2.Controls.Add(Me.cmdRefreshJobsTree)
        Me.FrameJobsTree2.Controls.Add(Me.labJobsExplorer)
        Me.FrameJobsTree2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameJobsTree2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameJobsTree2.Location = New System.Drawing.Point(8, 8)
        Me.FrameJobsTree2.Name = "FrameJobsTree2"
        Me.FrameJobsTree2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameJobsTree2.Size = New System.Drawing.Size(528, 94)
        Me.FrameJobsTree2.TabIndex = 19
        Me.FrameJobsTree2.TabStop = False
        Me.FrameJobsTree2.Text = "FrameJobsTree2"
        '
        'tvwJobs
        '
        Me.tvwJobs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tvwJobs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tvwJobs.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwJobs.ImageIndex = 0
        Me.tvwJobs.ImageList = Me.ImageListTree
        Me.tvwJobs.Location = New System.Drawing.Point(6, 53)
        Me.tvwJobs.Name = "tvwJobs"
        Me.tvwJobs.SelectedImageIndex = 0
        Me.tvwJobs.Size = New System.Drawing.Size(95, 29)
        Me.tvwJobs.TabIndex = 20
        '
        'ImageListTree
        '
        Me.ImageListTree.ImageStream = CType(resources.GetObject("ImageListTree.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListTree.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageListTree.Images.SetKeyName(0, "Blank.ico")
        Me.ImageListTree.Images.SetKeyName(1, "viewall")
        Me.ImageListTree.Images.SetKeyName(2, "returned")
        Me.ImageListTree.Images.SetKeyName(3, "hourglass_wide")
        Me.ImageListTree.Images.SetKeyName(4, "hourglass")
        Me.ImageListTree.Images.SetKeyName(5, "alert_original")
        Me.ImageListTree.Images.SetKeyName(6, "alert_p3")
        Me.ImageListTree.Images.SetKeyName(7, "alert_p2")
        Me.ImageListTree.Images.SetKeyName(8, "alert_p2_pink")
        Me.ImageListTree.Images.SetKeyName(9, "alert_wty")
        '
        'labJobsFilter
        '
        Me.labJobsFilter.BackColor = System.Drawing.Color.Transparent
        Me.labJobsFilter.Cursor = System.Windows.Forms.Cursors.Default
        Me.labJobsFilter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJobsFilter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labJobsFilter.Location = New System.Drawing.Point(74, 7)
        Me.labJobsFilter.Name = "labJobsFilter"
        Me.labJobsFilter.Padding = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.labJobsFilter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labJobsFilter.Size = New System.Drawing.Size(70, 14)
        Me.labJobsFilter.TabIndex = 94
        Me.labJobsFilter.Text = "Jobs Filter:"
        Me.labJobsFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboJobsFilter
        '
        Me.cboJobsFilter.BackColor = System.Drawing.Color.MistyRose
        Me.cboJobsFilter.CausesValidation = False
        Me.cboJobsFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboJobsFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboJobsFilter.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboJobsFilter.FormattingEnabled = True
        Me.cboJobsFilter.Items.AddRange(New Object() {"My Jobs", "All Jobs", "Unclaimed Jobs", "Wait-Listed Jobs"})
        Me.cboJobsFilter.Location = New System.Drawing.Point(76, 22)
        Me.cboJobsFilter.Name = "cboJobsFilter"
        Me.cboJobsFilter.Size = New System.Drawing.Size(111, 22)
        Me.cboJobsFilter.TabIndex = 93
        '
        'panelReminder
        '
        Me.panelReminder.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.panelReminder.Controls.Add(Me.labOnSiteReminder)
        Me.panelReminder.Controls.Add(Me.cmdDismissReminder)
        Me.panelReminder.Controls.Add(Me.labReminder)
        Me.panelReminder.Location = New System.Drawing.Point(225, 50)
        Me.panelReminder.Name = "panelReminder"
        Me.panelReminder.Size = New System.Drawing.Size(270, 38)
        Me.panelReminder.TabIndex = 92
        '
        'labOnSiteReminder
        '
        Me.labOnSiteReminder.AutoSize = True
        Me.labOnSiteReminder.BackColor = System.Drawing.Color.Transparent
        Me.labOnSiteReminder.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labOnSiteReminder.Location = New System.Drawing.Point(97, 13)
        Me.labOnSiteReminder.Name = "labOnSiteReminder"
        Me.labOnSiteReminder.Size = New System.Drawing.Size(116, 13)
        Me.labOnSiteReminder.TabIndex = 2
        Me.labOnSiteReminder.Text = "labOnSiteReminder"
        '
        'cmdDismissReminder
        '
        Me.cmdDismissReminder.FlatAppearance.BorderSize = 0
        Me.cmdDismissReminder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdDismissReminder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.cmdDismissReminder.Location = New System.Drawing.Point(209, 9)
        Me.cmdDismissReminder.Name = "cmdDismissReminder"
        Me.cmdDismissReminder.Size = New System.Drawing.Size(58, 20)
        Me.cmdDismissReminder.TabIndex = 1
        Me.cmdDismissReminder.Text = "Dismiss"
        Me.cmdDismissReminder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdDismissReminder.UseVisualStyleBackColor = True
        '
        'labReminder
        '
        Me.labReminder.AutoSize = True
        Me.labReminder.Location = New System.Drawing.Point(5, 13)
        Me.labReminder.Name = "labReminder"
        Me.labReminder.Size = New System.Drawing.Size(66, 13)
        Me.labReminder.TabIndex = 0
        Me.labReminder.Text = "labReminder"
        '
        'cboJobsOrder
        '
        Me.cboJobsOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.cboJobsOrder.CausesValidation = False
        Me.cboJobsOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboJobsOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboJobsOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboJobsOrder.FormattingEnabled = True
        Me.cboJobsOrder.Items.AddRange(New Object() {"Job No.", "Job Priority", "Customer"})
        Me.cboJobsOrder.Location = New System.Drawing.Point(278, 22)
        Me.cboJobsOrder.Name = "cboJobsOrder"
        Me.cboJobsOrder.Size = New System.Drawing.Size(96, 22)
        Me.cboJobsOrder.TabIndex = 90
        '
        'LabJobsOrder
        '
        Me.LabJobsOrder.BackColor = System.Drawing.Color.Transparent
        Me.LabJobsOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabJobsOrder.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabJobsOrder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobsOrder.Location = New System.Drawing.Point(275, 7)
        Me.LabJobsOrder.Name = "LabJobsOrder"
        Me.LabJobsOrder.Padding = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.LabJobsOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabJobsOrder.Size = New System.Drawing.Size(70, 14)
        Me.LabJobsOrder.TabIndex = 89
        Me.LabJobsOrder.Text = "Order By:"
        '
        'cmdRefreshJobsTree
        '
        Me.cmdRefreshJobsTree.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdRefreshJobsTree.CausesValidation = False
        Me.cmdRefreshJobsTree.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefreshJobsTree.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefreshJobsTree.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefreshJobsTree.Location = New System.Drawing.Point(466, 16)
        Me.cmdRefreshJobsTree.Name = "cmdRefreshJobsTree"
        Me.cmdRefreshJobsTree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefreshJobsTree.Size = New System.Drawing.Size(56, 21)
        Me.cmdRefreshJobsTree.TabIndex = 92
        Me.cmdRefreshJobsTree.Text = "Refresh"
        Me.cmdRefreshJobsTree.UseVisualStyleBackColor = False
        '
        'labJobsExplorer
        '
        Me.labJobsExplorer.BackColor = System.Drawing.Color.Transparent
        Me.labJobsExplorer.Cursor = System.Windows.Forms.Cursors.Default
        Me.labJobsExplorer.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJobsExplorer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.labJobsExplorer.Location = New System.Drawing.Point(0, 11)
        Me.labJobsExplorer.Name = "labJobsExplorer"
        Me.labJobsExplorer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labJobsExplorer.Size = New System.Drawing.Size(70, 41)
        Me.labJobsExplorer.TabIndex = 86
        Me.labJobsExplorer.Text = " Jobs Explorer"
        Me.labJobsExplorer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPageOnsite
        '
        Me.TabPageOnsite.BackColor = System.Drawing.Color.SandyBrown
        Me.TabPageOnsite.Controls.Add(Me.frameOnSite)
        Me.TabPageOnsite.Location = New System.Drawing.Point(4, 25)
        Me.TabPageOnsite.Name = "TabPageOnsite"
        Me.TabPageOnsite.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageOnsite.Size = New System.Drawing.Size(551, 412)
        Me.TabPageOnsite.TabIndex = 1
        Me.TabPageOnsite.Text = "- On-Site Jobs -"
        '
        'frameOnSite
        '
        Me.frameOnSite.BackColor = System.Drawing.Color.White
        Me.frameOnSite.CausesValidation = False
        Me.frameOnSite.Controls.Add(Me.cmdRefreshOnSite)
        Me.frameOnSite.Controls.Add(Me.Label13)
        Me.frameOnSite.Controls.Add(Me.Label12)
        Me.frameOnSite.Controls.Add(Me.labRecCountOnSite)
        Me.frameOnSite.Controls.Add(Me.dataGridViewOnSite)
        Me.frameOnSite.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameOnSite.Location = New System.Drawing.Point(2, 0)
        Me.frameOnSite.Name = "frameOnSite"
        Me.frameOnSite.Size = New System.Drawing.Size(542, 397)
        Me.frameOnSite.TabIndex = 96
        Me.frameOnSite.TabStop = False
        Me.frameOnSite.Text = "frameOnSite"
        '
        'cmdRefreshOnSite
        '
        Me.cmdRefreshOnSite.CausesValidation = False
        Me.cmdRefreshOnSite.Location = New System.Drawing.Point(463, 10)
        Me.cmdRefreshOnSite.Name = "cmdRefreshOnSite"
        Me.cmdRefreshOnSite.Size = New System.Drawing.Size(65, 21)
        Me.cmdRefreshOnSite.TabIndex = 78
        Me.cmdRefreshOnSite.Text = "Refresh"
        Me.cmdRefreshOnSite.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.White
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label13.Location = New System.Drawing.Point(180, 9)
        Me.Label13.Name = "Label13"
        Me.Label13.Padding = New System.Windows.Forms.Padding(3, 2, 0, 0)
        Me.Label13.Size = New System.Drawing.Size(208, 44)
        Me.Label13.TabIndex = 77
        Me.Label13.Text = "Shows all current On-Site Jobs-  (not yet delivered or cancelled).. (Jobs are sho" &
    "wn from latest down to oldest.)"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Khaki
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(4, 10)
        Me.Label12.Name = "Label12"
        Me.Label12.Padding = New System.Windows.Forms.Padding(6, 7, 0, 0)
        Me.Label12.Size = New System.Drawing.Size(171, 40)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "On-Site Job Schedule"
        '
        'labRecCountOnSite
        '
        Me.labRecCountOnSite.BackColor = System.Drawing.Color.Transparent
        Me.labRecCountOnSite.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCountOnSite.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCountOnSite.Location = New System.Drawing.Point(432, 36)
        Me.labRecCountOnSite.Name = "labRecCountOnSite"
        Me.labRecCountOnSite.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCountOnSite.Size = New System.Drawing.Size(101, 14)
        Me.labRecCountOnSite.TabIndex = 76
        Me.labRecCountOnSite.Text = "labRecCountOnSite"
        '
        'dataGridViewOnSite
        '
        Me.dataGridViewOnSite.AllowUserToAddRows = False
        Me.dataGridViewOnSite.AllowUserToDeleteRows = False
        Me.dataGridViewOnSite.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dataGridViewOnSite.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dataGridViewOnSite.CausesValidation = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dataGridViewOnSite.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dataGridViewOnSite.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridViewOnSite.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.datePromised, Me.Customer, Me.techName, Me.jobNo, Me.JobStatus})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dataGridViewOnSite.DefaultCellStyle = DataGridViewCellStyle2
        Me.dataGridViewOnSite.GridColor = System.Drawing.Color.Gainsboro
        Me.dataGridViewOnSite.Location = New System.Drawing.Point(4, 53)
        Me.dataGridViewOnSite.MultiSelect = False
        Me.dataGridViewOnSite.Name = "dataGridViewOnSite"
        Me.dataGridViewOnSite.ReadOnly = True
        Me.dataGridViewOnSite.RowHeadersWidth = 33
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dataGridViewOnSite.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dataGridViewOnSite.RowTemplate.Height = 27
        Me.dataGridViewOnSite.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dataGridViewOnSite.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataGridViewOnSite.Size = New System.Drawing.Size(532, 178)
        Me.dataGridViewOnSite.StandardTab = True
        Me.dataGridViewOnSite.TabIndex = 1
        '
        'datePromised
        '
        Me.datePromised.HeaderText = "Date Promised"
        Me.datePromised.Name = "datePromised"
        Me.datePromised.ReadOnly = True
        '
        'Customer
        '
        Me.Customer.FillWeight = 200.0!
        Me.Customer.HeaderText = "Customer"
        Me.Customer.Name = "Customer"
        Me.Customer.ReadOnly = True
        '
        'techName
        '
        Me.techName.HeaderText = "Tech Name"
        Me.techName.Name = "techName"
        Me.techName.ReadOnly = True
        '
        'jobNo
        '
        Me.jobNo.HeaderText = "Job No"
        Me.jobNo.Name = "jobNo"
        Me.jobNo.ReadOnly = True
        '
        'JobStatus
        '
        Me.JobStatus.FillWeight = 140.0!
        Me.JobStatus.HeaderText = "Job Status"
        Me.JobStatus.Name = "JobStatus"
        Me.JobStatus.ReadOnly = True
        '
        'TabPageJobsGrid
        '
        Me.TabPageJobsGrid.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TabPageJobsGrid.Controls.Add(Me.FrameBrowse)
        Me.TabPageJobsGrid.Location = New System.Drawing.Point(4, 25)
        Me.TabPageJobsGrid.Name = "TabPageJobsGrid"
        Me.TabPageJobsGrid.Size = New System.Drawing.Size(551, 412)
        Me.TabPageJobsGrid.TabIndex = 2
        Me.TabPageJobsGrid.Text = "- Search all Jobs -"
        '
        'FrameBrowse
        '
        Me.FrameBrowse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameBrowse.CausesValidation = False
        Me.FrameBrowse.Controls.Add(Me.Label15)
        Me.FrameBrowse.Controls.Add(Me.Label14)
        Me.FrameBrowse.Controls.Add(Me.DataGridViewJobs)
        Me.FrameBrowse.Controls.Add(Me.txtSearch)
        Me.FrameBrowse.Controls.Add(Me.cmdClearSearch)
        Me.FrameBrowse.Controls.Add(Me.cmdSearch)
        Me.FrameBrowse.Controls.Add(Me.txtFind)
        Me.FrameBrowse.Controls.Add(Me.ToolbarJobs)
        Me.FrameBrowse.Controls.Add(Me.Label23)
        Me.FrameBrowse.Controls.Add(Me.LabSearch)
        Me.FrameBrowse.Controls.Add(Me.LabFind)
        Me.FrameBrowse.Controls.Add(Me.labRecCount)
        Me.FrameBrowse.Controls.Add(Me._LabSearchJobs_0)
        Me.FrameBrowse.Controls.Add(Me.LabTitle)
        Me.FrameBrowse.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameBrowse.Location = New System.Drawing.Point(2, 2)
        Me.FrameBrowse.Name = "FrameBrowse"
        Me.FrameBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameBrowse.Size = New System.Drawing.Size(545, 209)
        Me.FrameBrowse.TabIndex = 35
        Me.FrameBrowse.TabStop = False
        Me.FrameBrowse.Text = "FrameBrowse"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(316, 59)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(113, 28)
        Me.Label15.TabIndex = 93
        Me.Label15.Text = "Select Jobs with Full Text Search:"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Gainsboro
        Me.Label14.Location = New System.Drawing.Point(253, 62)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(10, 51)
        Me.Label14.TabIndex = 92
        '
        'DataGridViewJobs
        '
        Me.DataGridViewJobs.AllowUserToAddRows = False
        Me.DataGridViewJobs.AllowUserToDeleteRows = False
        Me.DataGridViewJobs.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridViewJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewJobs.GridColor = System.Drawing.SystemColors.Control
        Me.DataGridViewJobs.Location = New System.Drawing.Point(11, 120)
        Me.DataGridViewJobs.Name = "DataGridViewJobs"
        Me.DataGridViewJobs.ReadOnly = True
        Me.DataGridViewJobs.RowTemplate.Height = 17
        Me.DataGridViewJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewJobs.Size = New System.Drawing.Size(528, 72)
        Me.DataGridViewJobs.StandardTab = True
        Me.DataGridViewJobs.TabIndex = 91
        '
        'txtSearch
        '
        Me.txtSearch.AcceptsReturn = True
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearch.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSearch.Location = New System.Drawing.Point(313, 88)
        Me.txtSearch.MaxLength = 0
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearch.Size = New System.Drawing.Size(143, 26)
        Me.txtSearch.TabIndex = 40
        Me.txtSearch.Text = "txtSearch"
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(120, 93)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(105, 21)
        Me.txtFind.TabIndex = 37
        '
        'ToolbarJobs
        '
        Me.ToolbarJobs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ToolbarJobs.CanOverflow = False
        Me.ToolbarJobs.ImageList = Me.ImageList1
        Me.ToolbarJobs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me._ToolbarJobs_Button1, Me._ToolbarJobs_Button2, Me._ToolbarJobs_Button3, Me._ToolbarJobs_ButtonQA, Me._ToolbarJobs_Button4, Me._ToolbarJobs_Button5, Me._ToolbarJobs_Button6})
        Me.ToolbarJobs.Location = New System.Drawing.Point(3, 16)
        Me.ToolbarJobs.Name = "ToolbarJobs"
        Me.ToolbarJobs.Size = New System.Drawing.Size(539, 38)
        Me.ToolbarJobs.TabIndex = 42
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageList1.Images.SetKeyName(0, "queued")
        Me.ImageList1.Images.SetKeyName(1, "suspended")
        Me.ImageList1.Images.SetKeyName(2, "started")
        Me.ImageList1.Images.SetKeyName(3, "notify")
        Me.ImageList1.Images.SetKeyName(4, "deliver")
        Me.ImageList1.Images.SetKeyName(5, "viewall")
        Me.ImageList1.Images.SetKeyName(6, "alert_red")
        Me.ImageList1.Images.SetKeyName(7, "alert_p3")
        Me.ImageList1.Images.SetKeyName(8, "alert_p2")
        Me.ImageList1.Images.SetKeyName(9, "hourglass")
        Me.ImageList1.Images.SetKeyName(10, "returned")
        Me.ImageList1.Images.SetKeyName(11, "alert_original")
        Me.ImageList1.Images.SetKeyName(12, "ArrowUp")
        Me.ImageList1.Images.SetKeyName(13, "ArrowDown")
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.ToolStripLabel1.Size = New System.Drawing.Size(143, 35)
        Me.ToolStripLabel1.Text = "Select Jobs by Status-"
        '
        '_ToolbarJobs_Button1
        '
        Me._ToolbarJobs_Button1.CheckOnClick = True
        Me._ToolbarJobs_Button1.ImageKey = "queued"
        Me._ToolbarJobs_Button1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarJobs_Button1.Name = "_ToolbarJobs_Button1"
        Me._ToolbarJobs_Button1.Size = New System.Drawing.Size(53, 35)
        Me._ToolbarJobs_Button1.Tag = "queued"
        Me._ToolbarJobs_Button1.Text = "Queued"
        Me._ToolbarJobs_Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarJobs_Button1.ToolTipText = "Job Waiting to be Started.."
        '
        '_ToolbarJobs_Button2
        '
        Me._ToolbarJobs_Button2.CheckOnClick = True
        Me._ToolbarJobs_Button2.ImageKey = "suspended"
        Me._ToolbarJobs_Button2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarJobs_Button2.Name = "_ToolbarJobs_Button2"
        Me._ToolbarJobs_Button2.Size = New System.Drawing.Size(69, 35)
        Me._ToolbarJobs_Button2.Tag = "suspended"
        Me._ToolbarJobs_Button2.Text = "Suspended"
        Me._ToolbarJobs_Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarJobs_Button2.ToolTipText = "Suspended Jobs.."
        '
        '_ToolbarJobs_Button3
        '
        Me._ToolbarJobs_Button3.CheckOnClick = True
        Me._ToolbarJobs_Button3.ImageKey = "started"
        Me._ToolbarJobs_Button3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarJobs_Button3.Name = "_ToolbarJobs_Button3"
        Me._ToolbarJobs_Button3.Size = New System.Drawing.Size(48, 35)
        Me._ToolbarJobs_Button3.Tag = "started"
        Me._ToolbarJobs_Button3.Text = "Started"
        Me._ToolbarJobs_Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarJobs_Button3.ToolTipText = "Jobs In Progress"
        '
        '_ToolbarJobs_ButtonQA
        '
        Me._ToolbarJobs_ButtonQA.Image = CType(resources.GetObject("_ToolbarJobs_ButtonQA.Image"), System.Drawing.Image)
        Me._ToolbarJobs_ButtonQA.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._ToolbarJobs_ButtonQA.Name = "_ToolbarJobs_ButtonQA"
        Me._ToolbarJobs_ButtonQA.Size = New System.Drawing.Size(28, 35)
        Me._ToolbarJobs_ButtonQA.Tag = "qa"
        Me._ToolbarJobs_ButtonQA.Text = "QA"
        Me._ToolbarJobs_ButtonQA.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarJobs_ButtonQA.ToolTipText = "Jobs in Qualiity Assurance"
        '
        '_ToolbarJobs_Button4
        '
        Me._ToolbarJobs_Button4.CheckOnClick = True
        Me._ToolbarJobs_Button4.ImageKey = "notify"
        Me._ToolbarJobs_Button4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarJobs_Button4.Name = "_ToolbarJobs_Button4"
        Me._ToolbarJobs_Button4.Size = New System.Drawing.Size(44, 35)
        Me._ToolbarJobs_Button4.Tag = "completed"
        Me._ToolbarJobs_Button4.Text = "Notify"
        Me._ToolbarJobs_Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarJobs_Button4.ToolTipText = "Completed Jobs-  Customer still to be Notified"
        '
        '_ToolbarJobs_Button5
        '
        Me._ToolbarJobs_Button5.CheckOnClick = True
        Me._ToolbarJobs_Button5.ImageKey = "deliver"
        Me._ToolbarJobs_Button5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarJobs_Button5.Name = "_ToolbarJobs_Button5"
        Me._ToolbarJobs_Button5.Size = New System.Drawing.Size(47, 35)
        Me._ToolbarJobs_Button5.Tag = "deliverable"
        Me._ToolbarJobs_Button5.Text = "Deliver"
        Me._ToolbarJobs_Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarJobs_Button5.ToolTipText = "All Jobs Ready to be Delivered"
        '
        '_ToolbarJobs_Button6
        '
        Me._ToolbarJobs_Button6.CheckOnClick = True
        Me._ToolbarJobs_Button6.ImageKey = "viewall"
        Me._ToolbarJobs_Button6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarJobs_Button6.Name = "_ToolbarJobs_Button6"
        Me._ToolbarJobs_Button6.Size = New System.Drawing.Size(51, 35)
        Me._ToolbarJobs_Button6.Tag = "viewall"
        Me._ToolbarJobs_Button6.Text = "All Jobs"
        Me._ToolbarJobs_Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarJobs_Button6.ToolTipText = "Show All Jobs"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(8, 16)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(133, 17)
        Me.Label23.TabIndex = 90
        Me.Label23.Text = " Searching all Jobs.."
        '
        'LabSearch
        '
        Me.LabSearch.BackColor = System.Drawing.Color.Transparent
        Me.LabSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabSearch.Font = New System.Drawing.Font("Tahoma", 6.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabSearch.Location = New System.Drawing.Point(432, 32)
        Me.LabSearch.Name = "LabSearch"
        Me.LabSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabSearch.Size = New System.Drawing.Size(81, 17)
        Me.LabSearch.TabIndex = 47
        Me.LabSearch.Text = "Text Search:"
        '
        'LabFind
        '
        Me.LabFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.LabFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabFind.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabFind.Location = New System.Drawing.Point(9, 81)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(105, 33)
        Me.LabFind.TabIndex = 46
        Me.LabFind.Text = "LabFind"
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(8, 192)
        Me.labRecCount.Name = "labRecCount"
        Me.labRecCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCount.Size = New System.Drawing.Size(115, 17)
        Me.labRecCount.TabIndex = 45
        Me.labRecCount.Text = "labRecCount"
        '
        '_LabSearchJobs_0
        '
        Me._LabSearchJobs_0.BackColor = System.Drawing.Color.Transparent
        Me._LabSearchJobs_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._LabSearchJobs_0.Font = New System.Drawing.Font("Tahoma", 6.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._LabSearchJobs_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabSearchJobs.SetIndex(Me._LabSearchJobs_0, CType(0, Short))
        Me._LabSearchJobs_0.Location = New System.Drawing.Point(128, 36)
        Me._LabSearchJobs_0.Name = "_LabSearchJobs_0"
        Me._LabSearchJobs_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LabSearchJobs_0.Size = New System.Drawing.Size(113, 17)
        Me._LabSearchJobs_0.TabIndex = 44
        Me._LabSearchJobs_0.Text = "Select Jobs by Status"
        '
        'LabTitle
        '
        Me.LabTitle.BackColor = System.Drawing.Color.Transparent
        Me.LabTitle.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabTitle.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabTitle.ForeColor = System.Drawing.Color.SaddleBrown
        Me.LabTitle.Location = New System.Drawing.Point(8, 53)
        Me.LabTitle.Name = "LabTitle"
        Me.LabTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabTitle.Size = New System.Drawing.Size(112, 28)
        Me.LabTitle.TabIndex = 43
        Me.LabTitle.Text = "Browse through Selected Jobs-"
        '
        'TabPageCustomers
        '
        Me.TabPageCustomers.BackColor = System.Drawing.Color.PaleGreen
        Me.TabPageCustomers.Controls.Add(Me.frameCustomers)
        Me.TabPageCustomers.Location = New System.Drawing.Point(4, 25)
        Me.TabPageCustomers.Name = "TabPageCustomers"
        Me.TabPageCustomers.Size = New System.Drawing.Size(551, 412)
        Me.TabPageCustomers.TabIndex = 3
        Me.TabPageCustomers.Text = "- Customers -"
        '
        'frameCustomers
        '
        Me.frameCustomers.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameCustomers.CausesValidation = False
        Me.frameCustomers.Controls.Add(Me.btnCustNewCustomer)
        Me.frameCustomers.Controls.Add(Me.Label68)
        Me.frameCustomers.Controls.Add(Me.Label24)
        Me.frameCustomers.Controls.Add(Me.cmdClearCustSearch)
        Me.frameCustomers.Controls.Add(Me.cmdCustSearch)
        Me.frameCustomers.Controls.Add(Me.txtCustSearch)
        Me.frameCustomers.Controls.Add(Me.DataGridViewCust)
        Me.frameCustomers.Controls.Add(Me.txtFindCust)
        Me.frameCustomers.Controls.Add(Me.listViewCustJobs)
        Me.frameCustomers.Controls.Add(Me.Label21)
        Me.frameCustomers.Controls.Add(Me.labJobHistory)
        Me.frameCustomers.Controls.Add(Me.labRecCountCust)
        Me.frameCustomers.Controls.Add(Me.labFindCust)
        Me.frameCustomers.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameCustomers.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameCustomers.Location = New System.Drawing.Point(2, 2)
        Me.frameCustomers.Name = "frameCustomers"
        Me.frameCustomers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameCustomers.Size = New System.Drawing.Size(545, 371)
        Me.frameCustomers.TabIndex = 71
        Me.frameCustomers.TabStop = False
        Me.frameCustomers.Text = "frameCustomers"
        '
        'btnCustNewCustomer
        '
        Me.btnCustNewCustomer.BackColor = System.Drawing.Color.LavenderBlush
        Me.btnCustNewCustomer.Location = New System.Drawing.Point(477, 34)
        Me.btnCustNewCustomer.Name = "btnCustNewCustomer"
        Me.btnCustNewCustomer.Size = New System.Drawing.Size(64, 38)
        Me.btnCustNewCustomer.TabIndex = 4
        Me.btnCustNewCustomer.Text = "New Customer"
        Me.btnCustNewCustomer.UseVisualStyleBackColor = False
        '
        'Label68
        '
        Me.Label68.BackColor = System.Drawing.Color.Gainsboro
        Me.Label68.Location = New System.Drawing.Point(242, 13)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(10, 55)
        Me.Label68.TabIndex = 90
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Green
        Me.Label24.Location = New System.Drawing.Point(260, 12)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(137, 32)
        Me.Label24.TabIndex = 89
        Me.Label24.Text = "Select Customers with Full Text Search:"
        '
        'txtCustSearch
        '
        Me.txtCustSearch.AcceptsReturn = True
        Me.txtCustSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustSearch.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustSearch.Location = New System.Drawing.Point(261, 44)
        Me.txtCustSearch.MaxLength = 0
        Me.txtCustSearch.Name = "txtCustSearch"
        Me.txtCustSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustSearch.Size = New System.Drawing.Size(125, 26)
        Me.txtCustSearch.TabIndex = 1
        Me.txtCustSearch.Text = "txtCustSearch"
        '
        'DataGridViewCust
        '
        Me.DataGridViewCust.AllowUserToAddRows = False
        Me.DataGridViewCust.AllowUserToDeleteRows = False
        Me.DataGridViewCust.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridViewCust.CausesValidation = False
        Me.DataGridViewCust.ColumnHeadersHeight = 33
        Me.DataGridViewCust.GridColor = System.Drawing.SystemColors.ControlLight
        Me.DataGridViewCust.Location = New System.Drawing.Point(8, 74)
        Me.DataGridViewCust.MultiSelect = False
        Me.DataGridViewCust.Name = "DataGridViewCust"
        Me.DataGridViewCust.ReadOnly = True
        Me.DataGridViewCust.RowHeadersWidth = 30
        Me.DataGridViewCust.RowTemplate.Height = 17
        Me.DataGridViewCust.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewCust.Size = New System.Drawing.Size(531, 47)
        Me.DataGridViewCust.StandardTab = True
        Me.DataGridViewCust.TabIndex = 5
        '
        'txtFindCust
        '
        Me.txtFindCust.AcceptsReturn = True
        Me.txtFindCust.BackColor = System.Drawing.Color.Lavender
        Me.txtFindCust.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFindCust.CausesValidation = False
        Me.txtFindCust.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFindCust.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindCust.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFindCust.Location = New System.Drawing.Point(117, 56)
        Me.txtFindCust.MaxLength = 0
        Me.txtFindCust.Name = "txtFindCust"
        Me.txtFindCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFindCust.Size = New System.Drawing.Size(102, 15)
        Me.txtFindCust.TabIndex = 0
        '
        'listViewCustJobs
        '
        Me.listViewCustJobs.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.listViewCustJobs.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.listViewCustJobs.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewCustJobs.ForeColor = System.Drawing.SystemColors.WindowText
        Me.listViewCustJobs.FullRowSelect = True
        Me.listViewCustJobs.GridLines = True
        Me.listViewCustJobs.Location = New System.Drawing.Point(8, 160)
        Me.listViewCustJobs.MultiSelect = False
        Me.listViewCustJobs.Name = "listViewCustJobs"
        Me.listViewCustJobs.Size = New System.Drawing.Size(526, 38)
        Me.listViewCustJobs.TabIndex = 6
        Me.listViewCustJobs.UseCompatibleStateImageBehavior = False
        Me.listViewCustJobs.View = System.Windows.Forms.View.Details
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label21.Location = New System.Drawing.Point(1, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(232, 17)
        Me.Label21.TabIndex = 88
        Me.Label21.Text = "  Browse through Selected Customers.."
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labJobHistory
        '
        Me.labJobHistory.BackColor = System.Drawing.SystemColors.Control
        Me.labJobHistory.Cursor = System.Windows.Forms.Cursors.Default
        Me.labJobHistory.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJobHistory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labJobHistory.Location = New System.Drawing.Point(8, 132)
        Me.labJobHistory.Name = "labJobHistory"
        Me.labJobHistory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labJobHistory.Size = New System.Drawing.Size(289, 17)
        Me.labJobHistory.TabIndex = 87
        Me.labJobHistory.Text = "Customer Job Work History"
        '
        'labRecCountCust
        '
        Me.labRecCountCust.BackColor = System.Drawing.Color.Transparent
        Me.labRecCountCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCountCust.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCountCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCountCust.Location = New System.Drawing.Point(434, 8)
        Me.labRecCountCust.Name = "labRecCountCust"
        Me.labRecCountCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCountCust.Size = New System.Drawing.Size(99, 15)
        Me.labRecCountCust.TabIndex = 75
        Me.labRecCountCust.Text = "labRecCountCust"
        Me.labRecCountCust.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'labFindCust
        '
        Me.labFindCust.BackColor = System.Drawing.Color.Gainsboro
        Me.labFindCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.labFindCust.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labFindCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labFindCust.Location = New System.Drawing.Point(6, 37)
        Me.labFindCust.Name = "labFindCust"
        Me.labFindCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labFindCust.Size = New System.Drawing.Size(106, 34)
        Me.labFindCust.TabIndex = 73
        Me.labFindCust.Text = "labFindCust"
        '
        'FrameJobDetails
        '
        Me.FrameJobDetails.BackColor = System.Drawing.Color.White
        Me.FrameJobDetails.CausesValidation = False
        Me.FrameJobDetails.Controls.Add(Me.labDetailWarrantyJob)
        Me.FrameJobDetails.Controls.Add(Me.picAttachments)
        Me.FrameJobDetails.Controls.Add(Me.labDetailOnSiteJob)
        Me.FrameJobDetails.Controls.Add(Me.labDetailJobDue)
        Me.FrameJobDetails.Controls.Add(Me.Label10)
        Me.FrameJobDetails.Controls.Add(Me.labDetailDateUpdated)
        Me.FrameJobDetails.Controls.Add(Me.labDetailUpdatedDescription)
        Me.FrameJobDetails.Controls.Add(Me.labDetailTech)
        Me.FrameJobDetails.Controls.Add(Me.Label9)
        Me.FrameJobDetails.Controls.Add(Me.labDetailDatePromised)
        Me.FrameJobDetails.Controls.Add(Me.Label8)
        Me.FrameJobDetails.Controls.Add(Me.labDetailDateCreated)
        Me.FrameJobDetails.Controls.Add(Me.Label7)
        Me.FrameJobDetails.Controls.Add(Me.Label6)
        Me.FrameJobDetails.Controls.Add(Me.picJobDetailReturned)
        Me.FrameJobDetails.Controls.Add(Me.LabDetailsJobNo)
        Me.FrameJobDetails.Controls.Add(Me.ToolStripJobAction)
        Me.FrameJobDetails.Controls.Add(Me.LabDetailStatus2)
        Me.FrameJobDetails.Controls.Add(Me.cboPriority)
        Me.FrameJobDetails.Controls.Add(Me.LabDetailStatus)
        Me.FrameJobDetails.Controls.Add(Me.rtfJobDetails)
        Me.FrameJobDetails.Controls.Add(Me.labLabStatus)
        Me.FrameJobDetails.Controls.Add(Me.Label5)
        Me.FrameJobDetails.Controls.Add(Me.LabDetailPriority)
        Me.FrameJobDetails.Controls.Add(Me.cmdChangePriority)
        Me.FrameJobDetails.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameJobDetails.Location = New System.Drawing.Point(572, 145)
        Me.FrameJobDetails.Name = "FrameJobDetails"
        Me.FrameJobDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameJobDetails.Size = New System.Drawing.Size(478, 331)
        Me.FrameJobDetails.TabIndex = 48
        Me.FrameJobDetails.TabStop = False
        Me.FrameJobDetails.Text = "FrameJobDetails"
        '
        'labDetailWarrantyJob
        '
        Me.labDetailWarrantyJob.BackColor = System.Drawing.Color.DarkViolet
        Me.labDetailWarrantyJob.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDetailWarrantyJob.ForeColor = System.Drawing.Color.Yellow
        Me.labDetailWarrantyJob.Location = New System.Drawing.Point(233, 169)
        Me.labDetailWarrantyJob.Name = "labDetailWarrantyJob"
        Me.labDetailWarrantyJob.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.labDetailWarrantyJob.Size = New System.Drawing.Size(69, 39)
        Me.labDetailWarrantyJob.TabIndex = 86
        Me.labDetailWarrantyJob.Text = "Warranty Job"
        Me.labDetailWarrantyJob.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'picAttachments
        '
        Me.picAttachments.BackColor = System.Drawing.Color.WhiteSmoke
        Me.picAttachments.Image = CType(resources.GetObject("picAttachments.Image"), System.Drawing.Image)
        Me.picAttachments.Location = New System.Drawing.Point(332, 165)
        Me.picAttachments.Name = "picAttachments"
        Me.picAttachments.Padding = New System.Windows.Forms.Padding(5, 5, 0, 0)
        Me.picAttachments.Size = New System.Drawing.Size(36, 43)
        Me.picAttachments.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picAttachments.TabIndex = 85
        Me.picAttachments.TabStop = False
        Me.ToolTipMain.SetToolTip(Me.picAttachments, "View/Add Attachment Docs this Job")
        '
        'labDetailOnSiteJob
        '
        Me.labDetailOnSiteJob.BackColor = System.Drawing.Color.Goldenrod
        Me.labDetailOnSiteJob.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDetailOnSiteJob.Location = New System.Drawing.Point(174, 18)
        Me.labDetailOnSiteJob.Name = "labDetailOnSiteJob"
        Me.labDetailOnSiteJob.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.labDetailOnSiteJob.Size = New System.Drawing.Size(85, 23)
        Me.labDetailOnSiteJob.TabIndex = 83
        Me.labDetailOnSiteJob.Text = "On-Site Job"
        '
        'labDetailJobDue
        '
        Me.labDetailJobDue.AutoSize = True
        Me.labDetailJobDue.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDetailJobDue.Location = New System.Drawing.Point(226, 113)
        Me.labDetailJobDue.Name = "labDetailJobDue"
        Me.labDetailJobDue.Size = New System.Drawing.Size(106, 14)
        Me.labDetailJobDue.TabIndex = 82
        Me.labDetailJobDue.Text = "labDetailJobDue"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(8, 194)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(138, 16)
        Me.Label10.TabIndex = 81
        Me.Label10.Text = "Service Details Follow:"
        '
        'labDetailDateUpdated
        '
        Me.labDetailDateUpdated.Location = New System.Drawing.Point(81, 152)
        Me.labDetailDateUpdated.Name = "labDetailDateUpdated"
        Me.labDetailDateUpdated.Size = New System.Drawing.Size(257, 13)
        Me.labDetailDateUpdated.TabIndex = 80
        Me.labDetailDateUpdated.Text = "labDetailDateUpdated"
        '
        'labDetailUpdatedDescription
        '
        Me.labDetailUpdatedDescription.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDetailUpdatedDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.labDetailUpdatedDescription.Location = New System.Drawing.Point(8, 139)
        Me.labDetailUpdatedDescription.Name = "labDetailUpdatedDescription"
        Me.labDetailUpdatedDescription.Size = New System.Drawing.Size(68, 29)
        Me.labDetailUpdatedDescription.TabIndex = 79
        Me.labDetailUpdatedDescription.Text = "Updated:"
        '
        'labDetailTech
        '
        Me.labDetailTech.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labDetailTech.Location = New System.Drawing.Point(284, 34)
        Me.labDetailTech.Name = "labDetailTech"
        Me.labDetailTech.Size = New System.Drawing.Size(87, 13)
        Me.labDetailTech.TabIndex = 78
        Me.labDetailTech.Text = "labDetailTech"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(281, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(41, 17)
        Me.Label9.TabIndex = 77
        Me.Label9.Text = "Tech:"
        '
        'labDetailDatePromised
        '
        Me.labDetailDatePromised.Location = New System.Drawing.Point(81, 115)
        Me.labDetailDatePromised.Name = "labDetailDatePromised"
        Me.labDetailDatePromised.Size = New System.Drawing.Size(139, 13)
        Me.labDetailDatePromised.TabIndex = 76
        Me.labDetailDatePromised.Text = "labDetailDatePromised"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Firebrick
        Me.Label8.Location = New System.Drawing.Point(8, 115)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 19)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "Promised:"
        '
        'labDetailDateCreated
        '
        Me.labDetailDateCreated.Location = New System.Drawing.Point(81, 96)
        Me.labDetailDateCreated.Name = "labDetailDateCreated"
        Me.labDetailDateCreated.Size = New System.Drawing.Size(248, 13)
        Me.labDetailDateCreated.TabIndex = 74
        Me.labDetailDateCreated.Text = "labDetailDateCreated"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(8, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(55, 19)
        Me.Label7.TabIndex = 73
        Me.Label7.Text = "Created:"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(8, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(61, 23)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "JobNo:"
        '
        'LabDetailsJobNo
        '
        Me.LabDetailsJobNo.BackColor = System.Drawing.Color.Transparent
        Me.LabDetailsJobNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDetailsJobNo.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDetailsJobNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.LabDetailsJobNo.Location = New System.Drawing.Point(69, 17)
        Me.LabDetailsJobNo.Name = "LabDetailsJobNo"
        Me.LabDetailsJobNo.Padding = New System.Windows.Forms.Padding(2, 3, 2, 2)
        Me.LabDetailsJobNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDetailsJobNo.Size = New System.Drawing.Size(77, 24)
        Me.LabDetailsJobNo.TabIndex = 69
        Me.LabDetailsJobNo.Text = "LabDetailsJobNo"
        Me.LabDetailsJobNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripJobAction
        '
        Me.ToolStripJobAction.AutoSize = False
        Me.ToolStripJobAction.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStripJobAction.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripJobAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel2, Me.ToolStripSeparator2, Me.btnCheckIn, Me.btnAmend, Me.ToolStripSeparator3, Me.btnUpdate, Me.btnReturnToQueue, Me.ToolStripSeparator4, Me.btnReOpen, Me.btnDeliver, Me.btnDetailNotify, Me.ToolStripSeparator12, Me.btnStopPress, Me.ToolStripSeparator1})
        Me.ToolStripJobAction.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ToolStripJobAction.Location = New System.Drawing.Point(388, 0)
        Me.ToolStripJobAction.Name = "ToolStripJobAction"
        Me.ToolStripJobAction.Size = New System.Drawing.Size(81, 223)
        Me.ToolStripJobAction.TabIndex = 70
        Me.ToolStripJobAction.Text = "ToolStrip1"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.ToolStripLabel2.Size = New System.Drawing.Size(79, 15)
        Me.ToolStripLabel2.Text = "Job Actions"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(79, 6)
        '
        'btnCheckIn
        '
        Me.btnCheckIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnCheckIn.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnCheckIn.Image = CType(resources.GetObject("btnCheckIn.Image"), System.Drawing.Image)
        Me.btnCheckIn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCheckIn.Name = "btnCheckIn"
        Me.btnCheckIn.Size = New System.Drawing.Size(79, 17)
        Me.btnCheckIn.Tag = "CheckIn"
        Me.btnCheckIn.Text = "Check-in"
        Me.btnCheckIn.ToolTipText = "Booked Job:  Checking-in.."
        '
        'btnAmend
        '
        Me.btnAmend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnAmend.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnAmend.Image = CType(resources.GetObject("btnAmend.Image"), System.Drawing.Image)
        Me.btnAmend.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAmend.Name = "btnAmend"
        Me.btnAmend.Size = New System.Drawing.Size(79, 17)
        Me.btnAmend.Tag = "Amend"
        Me.btnAmend.Text = "Amend"
        Me.btnAmend.ToolTipText = "Amend and/or print Service Agreement Details."
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(79, 6)
        '
        'btnUpdate
        '
        Me.btnUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnUpdate.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnUpdate.Image = CType(resources.GetObject("btnUpdate.Image"), System.Drawing.Image)
        Me.btnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(79, 17)
        Me.btnUpdate.Tag = "Update"
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.ToolTipText = "Start or Continue Service update on Job."
        '
        'btnReturnToQueue
        '
        Me.btnReturnToQueue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnReturnToQueue.Image = CType(resources.GetObject("btnReturnToQueue.Image"), System.Drawing.Image)
        Me.btnReturnToQueue.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReturnToQueue.Name = "btnReturnToQueue"
        Me.btnReturnToQueue.Size = New System.Drawing.Size(79, 19)
        Me.btnReturnToQueue.Tag = "ReturnToQueue"
        Me.btnReturnToQueue.Text = "Queue"
        Me.btnReturnToQueue.ToolTipText = "Return Job to Input Queue"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(79, 6)
        '
        'btnReOpen
        '
        Me.btnReOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnReOpen.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnReOpen.Image = CType(resources.GetObject("btnReOpen.Image"), System.Drawing.Image)
        Me.btnReOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReOpen.Name = "btnReOpen"
        Me.btnReOpen.Size = New System.Drawing.Size(79, 17)
        Me.btnReOpen.Tag = "ReOpen"
        Me.btnReOpen.Text = "Re-Open"
        Me.btnReOpen.ToolTipText = "Re-open Completed Job for further Service.."
        '
        'btnDeliver
        '
        Me.btnDeliver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnDeliver.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnDeliver.Image = CType(resources.GetObject("btnDeliver.Image"), System.Drawing.Image)
        Me.btnDeliver.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDeliver.Name = "btnDeliver"
        Me.btnDeliver.Size = New System.Drawing.Size(79, 17)
        Me.btnDeliver.Tag = "Deliver"
        Me.btnDeliver.Text = "Deliver"
        Me.btnDeliver.ToolTipText = " Delivery to Customer Recorded and Printed.."
        '
        'btnDetailNotify
        '
        Me.btnDetailNotify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnDetailNotify.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnDetailNotify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDetailNotify.Name = "btnDetailNotify"
        Me.btnDetailNotify.Size = New System.Drawing.Size(79, 17)
        Me.btnDetailNotify.Tag = "notify"
        Me.btnDetailNotify.Text = "Notify"
        Me.btnDetailNotify.ToolTipText = "Notify Customer"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(79, 6)
        '
        'btnStopPress
        '
        Me.btnStopPress.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnStopPress.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnStopPress.Image = CType(resources.GetObject("btnStopPress.Image"), System.Drawing.Image)
        Me.btnStopPress.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnStopPress.Name = "btnStopPress"
        Me.btnStopPress.Size = New System.Drawing.Size(79, 17)
        Me.btnStopPress.Tag = "StopPress"
        Me.btnStopPress.Text = "Stop Press"
        Me.btnStopPress.ToolTipText = "Stop Press: Add message to Work History.."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(79, 6)
        '
        'LabDetailStatus2
        '
        Me.LabDetailStatus2.AutoSize = True
        Me.LabDetailStatus2.BackColor = System.Drawing.Color.Transparent
        Me.LabDetailStatus2.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDetailStatus2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDetailStatus2.Location = New System.Drawing.Point(181, 51)
        Me.LabDetailStatus2.Name = "LabDetailStatus2"
        Me.LabDetailStatus2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDetailStatus2.Size = New System.Drawing.Size(88, 13)
        Me.LabDetailStatus2.TabIndex = 60
        Me.LabDetailStatus2.Text = "LabDetailStatus2"
        '
        'cboPriority
        '
        Me.cboPriority.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.cboPriority.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPriority.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboPriority.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPriority.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPriority.Location = New System.Drawing.Point(213, 66)
        Me.cboPriority.Name = "cboPriority"
        Me.cboPriority.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPriority.Size = New System.Drawing.Size(137, 21)
        Me.cboPriority.Sorted = True
        Me.cboPriority.TabIndex = 50
        '
        'LabDetailStatus
        '
        Me.LabDetailStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.LabDetailStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDetailStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDetailStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDetailStatus.Location = New System.Drawing.Point(81, 51)
        Me.LabDetailStatus.Name = "LabDetailStatus"
        Me.LabDetailStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDetailStatus.Size = New System.Drawing.Size(97, 13)
        Me.LabDetailStatus.TabIndex = 61
        Me.LabDetailStatus.Text = "LabDetailStatus"
        '
        'rtfJobDetails
        '
        Me.rtfJobDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.rtfJobDetails.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtfJobDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rtfJobDetails.HideSelection = False
        Me.rtfJobDetails.Location = New System.Drawing.Point(9, 214)
        Me.rtfJobDetails.Name = "rtfJobDetails"
        Me.rtfJobDetails.ReadOnly = True
        Me.rtfJobDetails.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtfJobDetails.Size = New System.Drawing.Size(435, 67)
        Me.rtfJobDetails.TabIndex = 58
        Me.rtfJobDetails.Text = ""
        '
        'labLabStatus
        '
        Me.labLabStatus.BackColor = System.Drawing.Color.Transparent
        Me.labLabStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.labLabStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labLabStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.labLabStatus.Location = New System.Drawing.Point(8, 51)
        Me.labLabStatus.Name = "labLabStatus"
        Me.labLabStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labLabStatus.Size = New System.Drawing.Size(50, 17)
        Me.labLabStatus.TabIndex = 59
        Me.labLabStatus.Text = "Status:"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(8, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(55, 17)
        Me.Label5.TabIndex = 63
        Me.Label5.Text = "Priority:"
        '
        'LabDetailPriority
        '
        Me.LabDetailPriority.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.LabDetailPriority.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDetailPriority.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDetailPriority.Location = New System.Drawing.Point(81, 71)
        Me.LabDetailPriority.Name = "LabDetailPriority"
        Me.LabDetailPriority.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDetailPriority.Size = New System.Drawing.Size(97, 13)
        Me.LabDetailPriority.TabIndex = 64
        Me.LabDetailPriority.Text = "LabDetailPriority"
        '
        'frameMainCmds
        '
        Me.frameMainCmds.BackColor = System.Drawing.Color.White
        Me.frameMainCmds.Controls.Add(Me.labMainCustTags)
        Me.frameMainCmds.Controls.Add(Me.toolbarNewJob2)
        Me.frameMainCmds.Controls.Add(Me.txtDetailsHdr)
        Me.frameMainCmds.Controls.Add(Me.picNoCustRecord)
        Me.frameMainCmds.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameMainCmds.Location = New System.Drawing.Point(568, 45)
        Me.frameMainCmds.Name = "frameMainCmds"
        Me.frameMainCmds.Size = New System.Drawing.Size(482, 102)
        Me.frameMainCmds.TabIndex = 95
        Me.frameMainCmds.TabStop = False
        Me.frameMainCmds.Text = "Customer"
        '
        'labMainCustTags
        '
        Me.labMainCustTags.BackColor = System.Drawing.Color.Snow
        Me.labMainCustTags.Font = New System.Drawing.Font("Lucida Sans Unicode", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labMainCustTags.ForeColor = System.Drawing.Color.Blue
        Me.labMainCustTags.Location = New System.Drawing.Point(250, 40)
        Me.labMainCustTags.Name = "labMainCustTags"
        Me.labMainCustTags.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.labMainCustTags.Size = New System.Drawing.Size(220, 53)
        Me.labMainCustTags.TabIndex = 89
        Me.labMainCustTags.Text = "labMainCustTags"
        '
        'toolbarNewJob2
        '
        Me.toolbarNewJob2.AutoSize = False
        Me.toolbarNewJob2.Dock = System.Windows.Forms.DockStyle.None
        Me.toolbarNewJob2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.toolbarNewJob2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator11, Me.btnAcceptJob, Me.ToolStripSeparator16, Me.ToolStripSeparator5, Me.btnOnSiteJob, Me.ToolStripSeparator13, Me.btnHistory, Me.ToolStripSeparator7})
        Me.toolbarNewJob2.Location = New System.Drawing.Point(89, 8)
        Me.toolbarNewJob2.Name = "toolbarNewJob2"
        Me.toolbarNewJob2.Size = New System.Drawing.Size(382, 29)
        Me.toolbarNewJob2.TabIndex = 0
        Me.toolbarNewJob2.Text = "ToolStrip1"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(6, 29)
        '
        'btnAcceptJob
        '
        Me.btnAcceptJob.AutoSize = False
        Me.btnAcceptJob.BackColor = System.Drawing.Color.LightSteelBlue
        Me.btnAcceptJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnAcceptJob.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAcceptJob.ForeColor = System.Drawing.Color.Black
        Me.btnAcceptJob.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAcceptJob.Name = "btnAcceptJob"
        Me.btnAcceptJob.Size = New System.Drawing.Size(123, 25)
        Me.btnAcceptJob.Tag = "acceptjob"
        Me.btnAcceptJob.Text = "New Workshop Job"
        Me.btnAcceptJob.ToolTipText = "Create new Workshop Job-  Accept now or Book-in."
        '
        'ToolStripSeparator16
        '
        Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
        Me.ToolStripSeparator16.Size = New System.Drawing.Size(6, 29)
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.AutoSize = False
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(7, 25)
        '
        'btnOnSiteJob
        '
        Me.btnOnSiteJob.AutoSize = False
        Me.btnOnSiteJob.BackColor = System.Drawing.Color.Goldenrod
        Me.btnOnSiteJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnOnSiteJob.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnOnSiteJob.Image = CType(resources.GetObject("btnOnSiteJob.Image"), System.Drawing.Image)
        Me.btnOnSiteJob.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOnSiteJob.Name = "btnOnSiteJob"
        Me.btnOnSiteJob.Size = New System.Drawing.Size(123, 25)
        Me.btnOnSiteJob.Tag = "OnSiteJob"
        Me.btnOnSiteJob.Text = "New On-Site Job"
        Me.btnOnSiteJob.ToolTipText = "Create and schedule a new OnSite Job Booking"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.AutoSize = False
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(9, 25)
        '
        'btnHistory
        '
        Me.btnHistory.BackColor = System.Drawing.Color.Transparent
        Me.btnHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnHistory.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.btnHistory.Image = CType(resources.GetObject("btnHistory.Image"), System.Drawing.Image)
        Me.btnHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(48, 26)
        Me.btnHistory.Tag = "history"
        Me.btnHistory.Text = " History"
        Me.btnHistory.ToolTipText = " Customer Job and Purchases History"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.AutoSize = False
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(13, 25)
        '
        'txtDetailsHdr
        '
        Me.txtDetailsHdr.AcceptsReturn = True
        Me.txtDetailsHdr.BackColor = System.Drawing.Color.White
        Me.txtDetailsHdr.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDetailsHdr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDetailsHdr.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDetailsHdr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDetailsHdr.Location = New System.Drawing.Point(8, 40)
        Me.txtDetailsHdr.MaxLength = 0
        Me.txtDetailsHdr.Multiline = True
        Me.txtDetailsHdr.Name = "txtDetailsHdr"
        Me.txtDetailsHdr.ReadOnly = True
        Me.txtDetailsHdr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDetailsHdr.Size = New System.Drawing.Size(233, 56)
        Me.txtDetailsHdr.TabIndex = 83
        Me.txtDetailsHdr.Text = "txtDetailsHdr"
        '
        'picNoCustRecord
        '
        Me.picNoCustRecord.Image = CType(resources.GetObject("picNoCustRecord.Image"), System.Drawing.Image)
        Me.picNoCustRecord.Location = New System.Drawing.Point(12, 19)
        Me.picNoCustRecord.Name = "picNoCustRecord"
        Me.picNoCustRecord.Size = New System.Drawing.Size(16, 16)
        Me.picNoCustRecord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picNoCustRecord.TabIndex = 86
        Me.picNoCustRecord.TabStop = False
        '
        'labEmptyJobPanel
        '
        Me.labEmptyJobPanel.AutoSize = True
        Me.labEmptyJobPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labEmptyJobPanel.Location = New System.Drawing.Point(675, 182)
        Me.labEmptyJobPanel.Name = "labEmptyJobPanel"
        Me.labEmptyJobPanel.Size = New System.Drawing.Size(103, 14)
        Me.labEmptyJobPanel.TabIndex = 90
        Me.labEmptyJobPanel.Text = "No Job selected.."
        '
        'toolbarJobView
        '
        Me.toolbarJobView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.toolbarJobView.AutoSize = False
        Me.toolbarJobView.CanOverflow = False
        Me.toolbarJobView.Dock = System.Windows.Forms.DockStyle.None
        Me.toolbarJobView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator18, Me.ToolStripSeparator9, Me.ToolStripDropDownGridFont, Me.ToolStripSeparator14, Me.ToolStripSeparator15, Me._toolbarJobView_ButtonBackup})
        Me.toolbarJobView.Location = New System.Drawing.Point(568, 14)
        Me.toolbarJobView.Name = "toolbarJobView"
        Me.toolbarJobView.Size = New System.Drawing.Size(478, 25)
        Me.toolbarJobView.TabIndex = 77
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.AutoSize = False
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(21, 25)
        '
        'ToolStripDropDownGridFont
        '
        Me.ToolStripDropDownGridFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownGridFont.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemFontSize_8, Me.ToolStripMenuItemFontSize_9, Me.ToolStripMenuItemFontSize_10})
        Me.ToolStripDropDownGridFont.Image = CType(resources.GetObject("ToolStripDropDownGridFont.Image"), System.Drawing.Image)
        Me.ToolStripDropDownGridFont.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownGridFont.Name = "ToolStripDropDownGridFont"
        Me.ToolStripDropDownGridFont.Size = New System.Drawing.Size(29, 22)
        Me.ToolStripDropDownGridFont.ToolTipText = "Grid Text Size"
        '
        'ToolStripMenuItemFontSize_8
        '
        Me.ToolStripMenuItemFontSize_8.Name = "ToolStripMenuItemFontSize_8"
        Me.ToolStripMenuItemFontSize_8.Size = New System.Drawing.Size(97, 22)
        Me.ToolStripMenuItemFontSize_8.Tag = "8"
        Me.ToolStripMenuItemFontSize_8.Text = "8pt"
        '
        'ToolStripMenuItemFontSize_9
        '
        Me.ToolStripMenuItemFontSize_9.Name = "ToolStripMenuItemFontSize_9"
        Me.ToolStripMenuItemFontSize_9.Size = New System.Drawing.Size(97, 22)
        Me.ToolStripMenuItemFontSize_9.Tag = "9"
        Me.ToolStripMenuItemFontSize_9.Text = "9pt"
        '
        'ToolStripMenuItemFontSize_10
        '
        Me.ToolStripMenuItemFontSize_10.Name = "ToolStripMenuItemFontSize_10"
        Me.ToolStripMenuItemFontSize_10.Size = New System.Drawing.Size(97, 22)
        Me.ToolStripMenuItemFontSize_10.Tag = "10"
        Me.ToolStripMenuItemFontSize_10.Text = "10pt"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSeparator14.AutoSize = False
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(17, 25)
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.AutoSize = False
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(17, 25)
        '
        '_toolbarJobView_ButtonBackup
        '
        Me._toolbarJobView_ButtonBackup.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me._toolbarJobView_ButtonBackup.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._toolbarJobView_ButtonBackup.Image = CType(resources.GetObject("_toolbarJobView_ButtonBackup.Image"), System.Drawing.Image)
        Me._toolbarJobView_ButtonBackup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me._toolbarJobView_ButtonBackup.Name = "_toolbarJobView_ButtonBackup"
        Me._toolbarJobView_ButtonBackup.Size = New System.Drawing.Size(66, 22)
        Me._toolbarJobView_ButtonBackup.Tag = "backup"
        Me._toolbarJobView_ButtonBackup.Text = "Backup"
        '
        'Picture3
        '
        Me.Picture3.BackColor = System.Drawing.SystemColors.Control
        Me.Picture3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Picture3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Picture3.Image = CType(resources.GetObject("Picture3.Image"), System.Drawing.Image)
        Me.Picture3.Location = New System.Drawing.Point(420, 460)
        Me.Picture3.Name = "Picture3"
        Me.Picture3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture3.Size = New System.Drawing.Size(138, 97)
        Me.Picture3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture3.TabIndex = 36
        Me.Picture3.TabStop = False
        Me.Picture3.Visible = False
        '
        'frameQuotes
        '
        Me.frameQuotes.CausesValidation = False
        Me.frameQuotes.Controls.Add(Me.labLoadingQuotes)
        Me.frameQuotes.Controls.Add(Me.chkRecentQuotes)
        Me.frameQuotes.Controls.Add(Me.Label4)
        Me.frameQuotes.Controls.Add(Me.labQuoteCount)
        Me.frameQuotes.Controls.Add(Me.cmdRefreshQuotes)
        Me.frameQuotes.Controls.Add(Me.chkNewQuotes)
        Me.frameQuotes.Controls.Add(Me.LabQuotesHdr)
        Me.frameQuotes.Controls.Add(Me.ListViewSalesOrders)
        Me.frameQuotes.Location = New System.Drawing.Point(9, 6)
        Me.frameQuotes.Name = "frameQuotes"
        Me.frameQuotes.Size = New System.Drawing.Size(507, 543)
        Me.frameQuotes.TabIndex = 91
        Me.frameQuotes.TabStop = False
        Me.frameQuotes.Text = "frameQuotes"
        '
        'labLoadingQuotes
        '
        Me.labLoadingQuotes.Location = New System.Drawing.Point(343, 17)
        Me.labLoadingQuotes.Name = "labLoadingQuotes"
        Me.labLoadingQuotes.Size = New System.Drawing.Size(156, 16)
        Me.labLoadingQuotes.TabIndex = 11
        Me.labLoadingQuotes.Text = "labLoadingQuotes"
        Me.labLoadingQuotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkRecentQuotes
        '
        Me.chkRecentQuotes.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRecentQuotes.Location = New System.Drawing.Point(186, 48)
        Me.chkRecentQuotes.Name = "chkRecentQuotes"
        Me.chkRecentQuotes.Size = New System.Drawing.Size(144, 23)
        Me.chkRecentQuotes.TabIndex = 10
        Me.chkRecentQuotes.Text = "Show last 60 days only"
        Me.chkRecentQuotes.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(55, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "records.."
        '
        'labQuoteCount
        '
        Me.labQuoteCount.Location = New System.Drawing.Point(14, 71)
        Me.labQuoteCount.Name = "labQuoteCount"
        Me.labQuoteCount.Size = New System.Drawing.Size(39, 12)
        Me.labQuoteCount.TabIndex = 8
        Me.labQuoteCount.Text = "labQuoteCount"
        Me.labQuoteCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdRefreshQuotes
        '
        Me.cmdRefreshQuotes.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefreshQuotes.Location = New System.Drawing.Point(442, 46)
        Me.cmdRefreshQuotes.Name = "cmdRefreshQuotes"
        Me.cmdRefreshQuotes.Size = New System.Drawing.Size(57, 23)
        Me.cmdRefreshQuotes.TabIndex = 7
        Me.cmdRefreshQuotes.Text = "Refresh"
        Me.cmdRefreshQuotes.UseVisualStyleBackColor = True
        '
        'chkNewQuotes
        '
        Me.chkNewQuotes.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNewQuotes.Location = New System.Drawing.Point(186, 22)
        Me.chkNewQuotes.Name = "chkNewQuotes"
        Me.chkNewQuotes.Size = New System.Drawing.Size(160, 26)
        Me.chkNewQuotes.TabIndex = 6
        Me.chkNewQuotes.Text = "Show Unbuilt Quotes only"
        Me.chkNewQuotes.UseVisualStyleBackColor = True
        '
        'LabQuotesHdr
        '
        Me.LabQuotesHdr.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabQuotesHdr.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabQuotesHdr.Location = New System.Drawing.Point(9, 25)
        Me.LabQuotesHdr.Name = "LabQuotesHdr"
        Me.LabQuotesHdr.Padding = New System.Windows.Forms.Padding(3)
        Me.LabQuotesHdr.Size = New System.Drawing.Size(154, 31)
        Me.LabQuotesHdr.TabIndex = 5
        Me.LabQuotesHdr.Text = "RetailManager Quotes"
        '
        'ListViewSalesOrders
        '
        Me.ListViewSalesOrders.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.ListViewSalesOrders.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ListViewSalesOrders.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewSalesOrders.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewSalesOrders.FullRowSelect = True
        Me.ListViewSalesOrders.GridLines = True
        Me.ListViewSalesOrders.HideSelection = False
        Me.ListViewSalesOrders.Location = New System.Drawing.Point(9, 92)
        Me.ListViewSalesOrders.MultiSelect = False
        Me.ListViewSalesOrders.Name = "ListViewSalesOrders"
        Me.ListViewSalesOrders.Size = New System.Drawing.Size(493, 37)
        Me.ListViewSalesOrders.SmallImageList = Me.ImageList1
        Me.ListViewSalesOrders.TabIndex = 4
        Me.ListViewSalesOrders.UseCompatibleStateImageBehavior = False
        Me.ListViewSalesOrders.View = System.Windows.Forms.View.Details
        '
        'frameQuoteDetails
        '
        Me.frameQuoteDetails.Controls.Add(Me.labQuoteCanBuild)
        Me.frameQuoteDetails.Controls.Add(Me.labJoblist)
        Me.frameQuoteDetails.Controls.Add(Me.labOrderDetail)
        Me.frameQuoteDetails.Controls.Add(Me.Label1)
        Me.frameQuoteDetails.Controls.Add(Me.labQuoteOrderNo)
        Me.frameQuoteDetails.Controls.Add(Me.ListViewQuote)
        Me.frameQuoteDetails.Controls.Add(Me.cmdBuildQuote)
        Me.frameQuoteDetails.Location = New System.Drawing.Point(525, 111)
        Me.frameQuoteDetails.Name = "frameQuoteDetails"
        Me.frameQuoteDetails.Size = New System.Drawing.Size(469, 212)
        Me.frameQuoteDetails.TabIndex = 92
        Me.frameQuoteDetails.TabStop = False
        Me.frameQuoteDetails.Text = "frameQuoteDetails"
        '
        'labQuoteCanBuild
        '
        Me.labQuoteCanBuild.Location = New System.Drawing.Point(203, 22)
        Me.labQuoteCanBuild.Name = "labQuoteCanBuild"
        Me.labQuoteCanBuild.Size = New System.Drawing.Size(119, 17)
        Me.labQuoteCanBuild.TabIndex = 86
        Me.labQuoteCanBuild.Text = "Can Build"
        '
        'labJoblist
        '
        Me.labJoblist.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJoblist.Location = New System.Drawing.Point(228, 55)
        Me.labJoblist.Name = "labJoblist"
        Me.labJoblist.Size = New System.Drawing.Size(207, 23)
        Me.labJoblist.TabIndex = 85
        Me.labJoblist.Text = "labJoblist"
        Me.labJoblist.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'labOrderDetail
        '
        Me.labOrderDetail.Location = New System.Drawing.Point(12, 41)
        Me.labOrderDetail.Name = "labOrderDetail"
        Me.labOrderDetail.Padding = New System.Windows.Forms.Padding(3)
        Me.labOrderDetail.Size = New System.Drawing.Size(210, 34)
        Me.labOrderDetail.TabIndex = 84
        Me.labOrderDetail.Text = "labOrderDetail"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 13)
        Me.Label1.TabIndex = 83
        Me.Label1.Text = "Quote (Order) No:"
        '
        'labQuoteOrderNo
        '
        Me.labQuoteOrderNo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labQuoteOrderNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labQuoteOrderNo.Location = New System.Drawing.Point(117, 22)
        Me.labQuoteOrderNo.Name = "labQuoteOrderNo"
        Me.labQuoteOrderNo.Size = New System.Drawing.Size(76, 14)
        Me.labQuoteOrderNo.TabIndex = 82
        Me.labQuoteOrderNo.Text = "labQuoteOrderNo"
        '
        'ListViewQuote
        '
        Me.ListViewQuote.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ListViewQuote.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewQuote.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewQuote.FullRowSelect = True
        Me.ListViewQuote.Location = New System.Drawing.Point(8, 81)
        Me.ListViewQuote.MultiSelect = False
        Me.ListViewQuote.Name = "ListViewQuote"
        Me.ListViewQuote.Size = New System.Drawing.Size(436, 50)
        Me.ListViewQuote.TabIndex = 81
        Me.ListViewQuote.UseCompatibleStateImageBehavior = False
        Me.ListViewQuote.View = System.Windows.Forms.View.Details
        '
        'Picture2
        '
        Me.Picture2.BackColor = System.Drawing.SystemColors.Control
        Me.Picture2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Picture2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Picture2.Location = New System.Drawing.Point(446, 713)
        Me.Picture2.Name = "Picture2"
        Me.Picture2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture2.Size = New System.Drawing.Size(33, 17)
        Me.Picture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture2.TabIndex = 14
        Me.Picture2.TabStop = False
        '
        'Picture1
        '
        Me.Picture1.BackColor = System.Drawing.SystemColors.Control
        Me.Picture1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Picture1.Image = CType(resources.GetObject("Picture1.Image"), System.Drawing.Image)
        Me.Picture1.Location = New System.Drawing.Point(650, 370)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture1.Size = New System.Drawing.Size(310, 212)
        Me.Picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture1.TabIndex = 11
        Me.Picture1.TabStop = False
        '
        'ListResults
        '
        Me.ListResults.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.ListResults.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListResults.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListResults.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListResults.ItemHeight = 14
        Me.ListResults.Location = New System.Drawing.Point(363, 718)
        Me.ListResults.Name = "ListResults"
        Me.ListResults.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ListResults.Size = New System.Drawing.Size(41, 18)
        Me.ListResults.TabIndex = 3
        Me.ListResults.TabStop = False
        '
        'ListNames
        '
        Me.ListNames.BackColor = System.Drawing.SystemColors.Window
        Me.ListNames.Cursor = System.Windows.Forms.Cursors.Default
        Me.ListNames.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListNames.Location = New System.Drawing.Point(486, 718)
        Me.ListNames.Name = "ListNames"
        Me.ListNames.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ListNames.Size = New System.Drawing.Size(41, 17)
        Me.ListNames.Sorted = True
        Me.ListNames.TabIndex = 2
        Me.ListNames.TabStop = False
        Me.ListNames.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'LabStaffName2
        '
        Me.LabStaffName2.BackColor = System.Drawing.Color.Transparent
        Me.LabStaffName2.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabStaffName2.Font = New System.Drawing.Font("Lucida Sans Unicode", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabStaffName2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabStaffName2.Location = New System.Drawing.Point(5, 30)
        Me.LabStaffName2.Name = "LabStaffName2"
        Me.LabStaffName2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabStaffName2.Size = New System.Drawing.Size(84, 23)
        Me.LabStaffName2.TabIndex = 93
        Me.LabStaffName2.Text = "LabStaffName2"
        '
        'LabBusinessId
        '
        Me.LabBusinessId.BackColor = System.Drawing.Color.Transparent
        Me.LabBusinessId.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabBusinessId.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabBusinessId.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabBusinessId.Location = New System.Drawing.Point(551, 724)
        Me.LabBusinessId.Name = "LabBusinessId"
        Me.LabBusinessId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabBusinessId.Size = New System.Drawing.Size(257, 13)
        Me.LabBusinessId.TabIndex = 15
        Me.LabBusinessId.Text = "LabBusinessId"
        '
        'LabBusiness
        '
        Me.LabBusiness.BackColor = System.Drawing.Color.Transparent
        Me.LabBusiness.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabBusiness.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabBusiness.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabBusiness.Location = New System.Drawing.Point(8, 724)
        Me.LabBusiness.Name = "LabBusiness"
        Me.LabBusiness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabBusiness.Size = New System.Drawing.Size(537, 13)
        Me.LabBusiness.TabIndex = 5
        Me.LabBusiness.Text = "labBusiness"
        '
        'LabToday
        '
        Me.LabToday.BackColor = System.Drawing.Color.Transparent
        Me.LabToday.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabToday.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabToday.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabToday.Location = New System.Drawing.Point(149, 35)
        Me.LabToday.Name = "LabToday"
        Me.LabToday.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabToday.Size = New System.Drawing.Size(95, 32)
        Me.LabToday.TabIndex = 1
        Me.LabToday.Text = "LabToday"
        Me.LabToday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MainMenu1
        '
        Me.MainMenu1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuDatabase, Me.mnuJobs, Me.mnuParts, Me.mnuCustomers, Me.mnuReference, Me.mnuAdmin, Me.mnuAbout})
        Me.MainMenu1.Location = New System.Drawing.Point(0, 0)
        Me.MainMenu1.Name = "MainMenu1"
        Me.MainMenu1.ShowItemToolTips = True
        Me.MainMenu1.Size = New System.Drawing.Size(1070, 24)
        Me.MainMenu1.TabIndex = 100
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPrinters, Me.mnuFileSep050, Me.mnuPreferences, Me.mnuFileSep070, Me.mnuFileRetailManagerDb, Me.ToolStripSeparator17, Me.mnuExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(36, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuPrinters
        '
        Me.mnuPrinters.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPrinterAssignments})
        Me.mnuPrinters.Name = "mnuPrinters"
        Me.mnuPrinters.Size = New System.Drawing.Size(151, 22)
        Me.mnuPrinters.Text = "Printers"
        '
        'mnuPrinterAssignments
        '
        Me.mnuPrinterAssignments.Name = "mnuPrinterAssignments"
        Me.mnuPrinterAssignments.Size = New System.Drawing.Size(182, 22)
        Me.mnuPrinterAssignments.Text = "Printer Assignments"
        '
        'mnuFileSep050
        '
        Me.mnuFileSep050.Name = "mnuFileSep050"
        Me.mnuFileSep050.Size = New System.Drawing.Size(148, 6)
        '
        'mnuPreferences
        '
        Me.mnuPreferences.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAutoSignOffOptions, Me.mnuDontShowNotifyReminder, Me.mnuStartupMaximised, Me.mnuGridFontSizes})
        Me.mnuPreferences.Name = "mnuPreferences"
        Me.mnuPreferences.Size = New System.Drawing.Size(151, 22)
        Me.mnuPreferences.Text = "Preferences"
        '
        'mnuAutoSignOffOptions
        '
        Me.mnuAutoSignOffOptions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuStaySignedOn, Me.mnuLongSignOff, Me.mnuShortSignOff})
        Me.mnuAutoSignOffOptions.Name = "mnuAutoSignOffOptions"
        Me.mnuAutoSignOffOptions.Size = New System.Drawing.Size(250, 22)
        Me.mnuAutoSignOffOptions.Text = "Auto SignOff Options"
        '
        'mnuStaySignedOn
        '
        Me.mnuStaySignedOn.Name = "mnuStaySignedOn"
        Me.mnuStaySignedOn.Size = New System.Drawing.Size(203, 22)
        Me.mnuStaySignedOn.Text = "Stay Signed On"
        '
        'mnuLongSignOff
        '
        Me.mnuLongSignOff.Name = "mnuLongSignOff"
        Me.mnuLongSignOff.Size = New System.Drawing.Size(203, 22)
        Me.mnuLongSignOff.Text = "Long SignOff (5 mins)"
        '
        'mnuShortSignOff
        '
        Me.mnuShortSignOff.Name = "mnuShortSignOff"
        Me.mnuShortSignOff.Size = New System.Drawing.Size(203, 22)
        Me.mnuShortSignOff.Text = "Short SignOff (30 secs)"
        '
        'mnuDontShowNotifyReminder
        '
        Me.mnuDontShowNotifyReminder.Checked = True
        Me.mnuDontShowNotifyReminder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuDontShowNotifyReminder.Name = "mnuDontShowNotifyReminder"
        Me.mnuDontShowNotifyReminder.Size = New System.Drawing.Size(250, 22)
        Me.mnuDontShowNotifyReminder.Text = "Don't show Notify Reminder"
        '
        'mnuStartupMaximised
        '
        Me.mnuStartupMaximised.Name = "mnuStartupMaximised"
        Me.mnuStartupMaximised.Size = New System.Drawing.Size(250, 22)
        Me.mnuStartupMaximised.Text = "Full-screen at Startup "
        '
        'mnuGridFontSizes
        '
        Me.mnuGridFontSizes.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuGridFont_8, Me.mnuGridFont_9, Me.mnuGridFont_10})
        Me.mnuGridFontSizes.Name = "mnuGridFontSizes"
        Me.mnuGridFontSizes.Size = New System.Drawing.Size(250, 22)
        Me.mnuGridFontSizes.Text = "Font Sizes (TreeView, DataGrid)"
        '
        'mnuGridFont_8
        '
        Me.mnuGridFont_8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuGridFont_8.Name = "mnuGridFont_8"
        Me.mnuGridFont_8.Size = New System.Drawing.Size(100, 22)
        Me.mnuGridFont_8.Tag = "8"
        Me.mnuGridFont_8.Text = "8pt"
        '
        'mnuGridFont_9
        '
        Me.mnuGridFont_9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuGridFont_9.Name = "mnuGridFont_9"
        Me.mnuGridFont_9.Size = New System.Drawing.Size(100, 22)
        Me.mnuGridFont_9.Tag = "9"
        Me.mnuGridFont_9.Text = "9pt"
        '
        'mnuGridFont_10
        '
        Me.mnuGridFont_10.Name = "mnuGridFont_10"
        Me.mnuGridFont_10.Size = New System.Drawing.Size(100, 22)
        Me.mnuGridFont_10.Tag = "10"
        Me.mnuGridFont_10.Text = "10pt"
        '
        'mnuFileSep070
        '
        Me.mnuFileSep070.Name = "mnuFileSep070"
        Me.mnuFileSep070.Size = New System.Drawing.Size(148, 6)
        '
        'mnuFileRetailManagerDb
        '
        Me.mnuFileRetailManagerDb.Name = "mnuFileRetailManagerDb"
        Me.mnuFileRetailManagerDb.Size = New System.Drawing.Size(151, 22)
        Me.mnuFileRetailManagerDb.Text = "Retail Host DB"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(148, 6)
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(151, 22)
        Me.mnuExit.Text = "E&xit"
        '
        'mnuDatabase
        '
        Me.mnuDatabase.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuJobMatix, Me.mnuDbRetailManager})
        Me.mnuDatabase.Name = "mnuDatabase"
        Me.mnuDatabase.Size = New System.Drawing.Size(69, 20)
        Me.mnuDatabase.Text = "Database"
        Me.mnuDatabase.ToolTipText = "See SQL Server and RetailManager DB Info.."
        '
        'mnuJobMatix
        '
        Me.mnuJobMatix.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBackupJobTrackingDB, Me.mnuSqlServer, Me.mnuWhoUsing})
        Me.mnuJobMatix.Name = "mnuJobMatix"
        Me.mnuJobMatix.Size = New System.Drawing.Size(181, 22)
        Me.mnuJobMatix.Text = "JobMatix/Sql Server"
        '
        'mnuBackupJobTrackingDB
        '
        Me.mnuBackupJobTrackingDB.Name = "mnuBackupJobTrackingDB"
        Me.mnuBackupJobTrackingDB.Size = New System.Drawing.Size(183, 22)
        Me.mnuBackupJobTrackingDB.Text = "JobMatix DB Backup"
        '
        'mnuSqlServer
        '
        Me.mnuSqlServer.Name = "mnuSqlServer"
        Me.mnuSqlServer.Size = New System.Drawing.Size(183, 22)
        Me.mnuSqlServer.Text = "Sql Server Info"
        '
        'mnuWhoUsing
        '
        Me.mnuWhoUsing.Name = "mnuWhoUsing"
        Me.mnuWhoUsing.Size = New System.Drawing.Size(183, 22)
        Me.mnuWhoUsing.Text = "Who using JobMatix"
        '
        'mnuDbRetailManager
        '
        Me.mnuDbRetailManager.Name = "mnuDbRetailManager"
        Me.mnuDbRetailManager.Size = New System.Drawing.Size(181, 22)
        Me.mnuDbRetailManager.Text = "Retail Host DB"
        '
        'mnuJobs
        '
        Me.mnuJobs.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNew})
        Me.mnuJobs.Name = "mnuJobs"
        Me.mnuJobs.Size = New System.Drawing.Size(43, 20)
        Me.mnuJobs.Text = "Jobs"
        '
        'mnuNew
        '
        Me.mnuNew.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewJob, Me.mnuJobsSep028, Me.NewOnSiteJobToolStripMenuItem})
        Me.mnuNew.Name = "mnuNew"
        Me.mnuNew.Size = New System.Drawing.Size(99, 22)
        Me.mnuNew.Text = "New"
        '
        'mnuNewJob
        '
        Me.mnuNewJob.Name = "mnuNewJob"
        Me.mnuNewJob.Size = New System.Drawing.Size(181, 22)
        Me.mnuNewJob.Text = "New Workshop Job"
        '
        'mnuJobsSep028
        '
        Me.mnuJobsSep028.Name = "mnuJobsSep028"
        Me.mnuJobsSep028.Size = New System.Drawing.Size(178, 6)
        '
        'NewOnSiteJobToolStripMenuItem
        '
        Me.NewOnSiteJobToolStripMenuItem.Name = "NewOnSiteJobToolStripMenuItem"
        Me.NewOnSiteJobToolStripMenuItem.Size = New System.Drawing.Size(181, 22)
        Me.NewOnSiteJobToolStripMenuItem.Text = "New On-Site Job"
        '
        'mnuParts
        '
        Me.mnuParts.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuJobParts, Me.mnuJobsSep050, Me.mnuSerialAudit})
        Me.mnuParts.Name = "mnuParts"
        Me.mnuParts.Size = New System.Drawing.Size(46, 20)
        Me.mnuParts.Text = "Parts"
        Me.mnuParts.ToolTipText = "Jobs Parts"
        '
        'mnuJobParts
        '
        Me.mnuJobParts.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFindPart, Me.mnuJobsSep035, Me.mnuShowJobParts})
        Me.mnuJobParts.Name = "mnuJobParts"
        Me.mnuJobParts.Size = New System.Drawing.Size(157, 22)
        Me.mnuJobParts.Text = "Job Parts"
        '
        'mnuFindPart
        '
        Me.mnuFindPart.Name = "mnuFindPart"
        Me.mnuFindPart.Size = New System.Drawing.Size(195, 22)
        Me.mnuFindPart.Text = "Text Search Job Parts"
        '
        'mnuJobsSep035
        '
        Me.mnuJobsSep035.Name = "mnuJobsSep035"
        Me.mnuJobsSep035.Size = New System.Drawing.Size(192, 6)
        '
        'mnuShowJobParts
        '
        Me.mnuShowJobParts.Name = "mnuShowJobParts"
        Me.mnuShowJobParts.Size = New System.Drawing.Size(195, 22)
        Me.mnuShowJobParts.Text = "Browse All Job Parts"
        '
        'mnuJobsSep050
        '
        Me.mnuJobsSep050.Name = "mnuJobsSep050"
        Me.mnuJobsSep050.Size = New System.Drawing.Size(154, 6)
        '
        'mnuSerialAudit
        '
        Me.mnuSerialAudit.Name = "mnuSerialAudit"
        Me.mnuSerialAudit.Size = New System.Drawing.Size(157, 22)
        Me.mnuSerialAudit.Text = "Serial Audit List"
        '
        'mnuCustomers
        '
        Me.mnuCustomers.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCustomerHistory})
        Me.mnuCustomers.Name = "mnuCustomers"
        Me.mnuCustomers.Size = New System.Drawing.Size(76, 20)
        Me.mnuCustomers.Text = "Customers"
        '
        'mnuCustomerHistory
        '
        Me.mnuCustomerHistory.Name = "mnuCustomerHistory"
        Me.mnuCustomerHistory.Size = New System.Drawing.Size(167, 22)
        Me.mnuCustomerHistory.Text = "Customer History"
        '
        'mnuReference
        '
        Me.mnuReference.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditRefTables, Me.mnuModelCheckList})
        Me.mnuReference.Name = "mnuReference"
        Me.mnuReference.Size = New System.Drawing.Size(75, 20)
        Me.mnuReference.Text = "Reference"
        '
        'mnuEditRefTables
        '
        Me.mnuEditRefTables.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuGoodsAcceptedTypes, Me.mnuBrands, Me.mnuProblemSymptoms, Me.mnuServiceTaskTypes})
        Me.mnuEditRefTables.Name = "mnuEditRefTables"
        Me.mnuEditRefTables.Size = New System.Drawing.Size(234, 22)
        Me.mnuEditRefTables.Text = "Edit Ref.Tables"
        '
        'mnuGoodsAcceptedTypes
        '
        Me.mnuGoodsAcceptedTypes.Name = "mnuGoodsAcceptedTypes"
        Me.mnuGoodsAcceptedTypes.Size = New System.Drawing.Size(194, 22)
        Me.mnuGoodsAcceptedTypes.Text = "GoodsAcceptedTypes"
        '
        'mnuBrands
        '
        Me.mnuBrands.Name = "mnuBrands"
        Me.mnuBrands.Size = New System.Drawing.Size(194, 22)
        Me.mnuBrands.Text = "Brands"
        '
        'mnuProblemSymptoms
        '
        Me.mnuProblemSymptoms.Name = "mnuProblemSymptoms"
        Me.mnuProblemSymptoms.Size = New System.Drawing.Size(194, 22)
        Me.mnuProblemSymptoms.Text = "ProblemSymptoms"
        '
        'mnuServiceTaskTypes
        '
        Me.mnuServiceTaskTypes.Name = "mnuServiceTaskTypes"
        Me.mnuServiceTaskTypes.Size = New System.Drawing.Size(194, 22)
        Me.mnuServiceTaskTypes.Text = "ServiceTaskTypes"
        '
        'mnuModelCheckList
        '
        Me.mnuModelCheckList.Name = "mnuModelCheckList"
        Me.mnuModelCheckList.Size = New System.Drawing.Size(234, 22)
        Me.mnuModelCheckList.Text = "Edit Service Model CheckLists"
        '
        'mnuAdmin
        '
        Me.mnuAdmin.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuResetJetPath, Me.mnuAdminSep0, Me.mnuAdminDatabase, Me.mnuAdminSep10, Me.mnuSetup, Me.mnuAdminSep11, Me.mnuSystemInfo, Me.mnuAdminSep15, Me.mnuUsers})
        Me.mnuAdmin.Name = "mnuAdmin"
        Me.mnuAdmin.Size = New System.Drawing.Size(53, 20)
        Me.mnuAdmin.Text = "Admin"
        Me.mnuAdmin.ToolTipText = "Admin (JobTracking)"
        '
        'mnuResetJetPath
        '
        Me.mnuResetJetPath.Name = "mnuResetJetPath"
        Me.mnuResetJetPath.Size = New System.Drawing.Size(215, 22)
        Me.mnuResetJetPath.Text = "Reset Retail Host DB Path"
        '
        'mnuAdminSep0
        '
        Me.mnuAdminSep0.Name = "mnuAdminSep0"
        Me.mnuAdminSep0.Size = New System.Drawing.Size(212, 6)
        '
        'mnuAdminDatabase
        '
        Me.mnuAdminDatabase.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBackupJobsDB, Me.mnuAdminSep1, Me.mnuAdminSep13})
        Me.mnuAdminDatabase.Name = "mnuAdminDatabase"
        Me.mnuAdminDatabase.Size = New System.Drawing.Size(215, 22)
        Me.mnuAdminDatabase.Text = "Database"
        '
        'mnuBackupJobsDB
        '
        Me.mnuBackupJobsDB.Name = "mnuBackupJobsDB"
        Me.mnuBackupJobsDB.Size = New System.Drawing.Size(160, 22)
        Me.mnuBackupJobsDB.Text = "Backup Jobs DB"
        '
        'mnuAdminSep1
        '
        Me.mnuAdminSep1.Name = "mnuAdminSep1"
        Me.mnuAdminSep1.Size = New System.Drawing.Size(157, 6)
        '
        'mnuAdminSep13
        '
        Me.mnuAdminSep13.Name = "mnuAdminSep13"
        Me.mnuAdminSep13.Size = New System.Drawing.Size(157, 6)
        '
        'mnuAdminSep10
        '
        Me.mnuAdminSep10.Name = "mnuAdminSep10"
        Me.mnuAdminSep10.Size = New System.Drawing.Size(212, 6)
        '
        'mnuSetup
        '
        Me.mnuSetup.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSetupInfo, Me.mnuAdminSep111, Me.mnuSMSUpdate, Me.mnuEnableSMSReminders, Me.mnuAutoAssignOrphanJobsOnUpdate})
        Me.mnuSetup.Name = "mnuSetup"
        Me.mnuSetup.Size = New System.Drawing.Size(215, 22)
        Me.mnuSetup.Text = "Setup"
        '
        'mnuSetupInfo
        '
        Me.mnuSetupInfo.Name = "mnuSetupInfo"
        Me.mnuSetupInfo.Size = New System.Drawing.Size(301, 22)
        Me.mnuSetupInfo.Text = "JobMatix Settings"
        '
        'mnuAdminSep111
        '
        Me.mnuAdminSep111.Name = "mnuAdminSep111"
        Me.mnuAdminSep111.Size = New System.Drawing.Size(298, 6)
        '
        'mnuSMSUpdate
        '
        Me.mnuSMSUpdate.Name = "mnuSMSUpdate"
        Me.mnuSMSUpdate.Size = New System.Drawing.Size(301, 22)
        Me.mnuSMSUpdate.Text = "SMS Texts, SMTP and Exchange Settings"
        '
        'mnuEnableSMSReminders
        '
        Me.mnuEnableSMSReminders.CheckOnClick = True
        Me.mnuEnableSMSReminders.Name = "mnuEnableSMSReminders"
        Me.mnuEnableSMSReminders.Size = New System.Drawing.Size(301, 22)
        Me.mnuEnableSMSReminders.Text = "Enable SMS  Reminders to Staff"
        '
        'mnuAutoAssignOrphanJobsOnUpdate
        '
        Me.mnuAutoAssignOrphanJobsOnUpdate.CheckOnClick = True
        Me.mnuAutoAssignOrphanJobsOnUpdate.Name = "mnuAutoAssignOrphanJobsOnUpdate"
        Me.mnuAutoAssignOrphanJobsOnUpdate.Size = New System.Drawing.Size(301, 22)
        Me.mnuAutoAssignOrphanJobsOnUpdate.Text = "Auto Assign Orphan Jobs On Update"
        '
        'mnuAdminSep11
        '
        Me.mnuAdminSep11.Name = "mnuAdminSep11"
        Me.mnuAdminSep11.Size = New System.Drawing.Size(212, 6)
        '
        'mnuSystemInfo
        '
        Me.mnuSystemInfo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShowSystemInfo, Me.mnuAdminSep12, Me.mnuUpdSysInfo})
        Me.mnuSystemInfo.Name = "mnuSystemInfo"
        Me.mnuSystemInfo.Size = New System.Drawing.Size(215, 22)
        Me.mnuSystemInfo.Text = "View System Info"
        '
        'mnuShowSystemInfo
        '
        Me.mnuShowSystemInfo.Name = "mnuShowSystemInfo"
        Me.mnuShowSystemInfo.Size = New System.Drawing.Size(227, 22)
        Me.mnuShowSystemInfo.Text = "View/Edit SystemInfo Table"
        '
        'mnuAdminSep12
        '
        Me.mnuAdminSep12.Name = "mnuAdminSep12"
        Me.mnuAdminSep12.Size = New System.Drawing.Size(224, 6)
        '
        'mnuUpdSysInfo
        '
        Me.mnuUpdSysInfo.Name = "mnuUpdSysInfo"
        Me.mnuUpdSysInfo.Size = New System.Drawing.Size(227, 22)
        Me.mnuUpdSysInfo.Text = "New SysInfo Entry"
        '
        'mnuAdminSep15
        '
        Me.mnuAdminSep15.Name = "mnuAdminSep15"
        Me.mnuAdminSep15.Size = New System.Drawing.Size(212, 6)
        '
        'mnuUsers
        '
        Me.mnuUsers.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShowUsers, Me.mnuAdminSep17, Me.mnuAddNewUser})
        Me.mnuUsers.Name = "mnuUsers"
        Me.mnuUsers.Size = New System.Drawing.Size(215, 22)
        Me.mnuUsers.Text = "Users"
        '
        'mnuShowUsers
        '
        Me.mnuShowUsers.Name = "mnuShowUsers"
        Me.mnuShowUsers.Size = New System.Drawing.Size(335, 22)
        Me.mnuShowUsers.Text = "Show Windows SQL JobTracking Users"
        '
        'mnuAdminSep17
        '
        Me.mnuAdminSep17.Name = "mnuAdminSep17"
        Me.mnuAdminSep17.Size = New System.Drawing.Size(332, 6)
        '
        'mnuAddNewUser
        '
        Me.mnuAddNewUser.Name = "mnuAddNewUser"
        Me.mnuAddNewUser.Size = New System.Drawing.Size(335, 22)
        Me.mnuAddNewUser.Text = "Add Windows User to  SQL JobTracking  Logins"
        '
        'mnuAbout
        '
        Me.mnuAbout.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutJobMatix32ToolStripMenuItem})
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(53, 20)
        Me.mnuAbout.Text = "About"
        '
        'AboutJobMatix32ToolStripMenuItem
        '
        Me.AboutJobMatix32ToolStripMenuItem.Name = "AboutJobMatix32ToolStripMenuItem"
        Me.AboutJobMatix32ToolStripMenuItem.Size = New System.Drawing.Size(159, 22)
        Me.AboutJobMatix32ToolStripMenuItem.Text = "About JobMatix"
        '
        'labVersion
        '
        Me.labVersion.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labVersion.Location = New System.Drawing.Point(880, 722)
        Me.labVersion.Name = "labVersion"
        Me.labVersion.Size = New System.Drawing.Size(172, 11)
        Me.labVersion.TabIndex = 101
        Me.labVersion.Text = "labVersion"
        Me.labVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'labStaffLabel
        '
        Me.labStaffLabel.BackColor = System.Drawing.Color.White
        Me.labStaffLabel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStaffLabel.Location = New System.Drawing.Point(6, 8)
        Me.labStaffLabel.Name = "labStaffLabel"
        Me.labStaffLabel.Size = New System.Drawing.Size(39, 15)
        Me.labStaffLabel.TabIndex = 102
        Me.labStaffLabel.Text = "Staff:"
        '
        'picLogo
        '
        Me.picLogo.Image = CType(resources.GetObject("picLogo.Image"), System.Drawing.Image)
        Me.picLogo.Location = New System.Drawing.Point(9, 34)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(28, 28)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picLogo.TabIndex = 103
        Me.picLogo.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(43, 34)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(95, 28)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 104
        Me.PictureBox1.TabStop = False
        '
        'labStatus
        '
        Me.labStatus.BackColor = System.Drawing.Color.Transparent
        Me.labStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labStatus.Location = New System.Drawing.Point(255, 25)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.labStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labStatus.Size = New System.Drawing.Size(318, 49)
        Me.labStatus.TabIndex = 106
        Me.labStatus.Text = "labStatus"
        '
        'panelSignOn
        '
        Me.panelSignOn.BackColor = System.Drawing.Color.White
        Me.panelSignOn.Controls.Add(Me.labStaffTimeRemaining)
        Me.panelSignOn.Controls.Add(Me.txtStaffBarcode)
        Me.panelSignOn.Controls.Add(Me.labStaffLabel)
        Me.panelSignOn.Controls.Add(Me.LabStaffName2)
        Me.panelSignOn.Controls.Add(Me.cmdSignoff)
        Me.panelSignOn.Location = New System.Drawing.Point(584, 22)
        Me.panelSignOn.Name = "panelSignOn"
        Me.panelSignOn.Size = New System.Drawing.Size(185, 53)
        Me.panelSignOn.TabIndex = 0
        '
        'labStaffTimeRemaining
        '
        Me.labStaffTimeRemaining.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStaffTimeRemaining.ForeColor = System.Drawing.SystemColors.Desktop
        Me.labStaffTimeRemaining.Location = New System.Drawing.Point(95, 36)
        Me.labStaffTimeRemaining.Name = "labStaffTimeRemaining"
        Me.labStaffTimeRemaining.Size = New System.Drawing.Size(32, 12)
        Me.labStaffTimeRemaining.TabIndex = 103
        Me.labStaffTimeRemaining.Text = "labStaffTimeRemaining"
        Me.labStaffTimeRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtStaffBarcode
        '
        Me.txtStaffBarcode.AcceptsReturn = True
        Me.txtStaffBarcode.BackColor = System.Drawing.Color.White
        Me.txtStaffBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStaffBarcode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStaffBarcode.Enabled = False
        Me.txtStaffBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStaffBarcode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStaffBarcode.Location = New System.Drawing.Point(52, 6)
        Me.txtStaffBarcode.MaxLength = 4
        Me.txtStaffBarcode.Name = "txtStaffBarcode"
        Me.txtStaffBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStaffBarcode.Size = New System.Drawing.Size(62, 21)
        Me.txtStaffBarcode.TabIndex = 0
        Me.txtStaffBarcode.Text = "txtStaffBarcode"
        '
        'BackgroundWorkerSearch
        '
        '
        'BackgroundWorkerGetSchema
        '
        '
        'TabControlMain
        '
        Me.TabControlMain.CausesValidation = False
        Me.TabControlMain.Controls.Add(Me.TabPageJobTracking)
        Me.TabControlMain.Controls.Add(Me.TabPageQuoteJobs)
        Me.TabControlMain.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlMain.Location = New System.Drawing.Point(1, 75)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(1072, 644)
        Me.TabControlMain.TabIndex = 3
        '
        'TabPageJobTracking
        '
        Me.TabPageJobTracking.BackColor = System.Drawing.Color.White
        Me.TabPageJobTracking.Controls.Add(Me.frameJobsTab)
        Me.TabPageJobTracking.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageJobTracking.Location = New System.Drawing.Point(4, 28)
        Me.TabPageJobTracking.Name = "TabPageJobTracking"
        Me.TabPageJobTracking.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageJobTracking.Size = New System.Drawing.Size(1064, 612)
        Me.TabPageJobTracking.TabIndex = 0
        Me.TabPageJobTracking.Text = "- Job Tracking -"
        Me.TabPageJobTracking.UseVisualStyleBackColor = True
        '
        'TabPageQuoteJobs
        '
        Me.TabPageQuoteJobs.BackColor = System.Drawing.Color.White
        Me.TabPageQuoteJobs.Controls.Add(Me.frameQuoteCustomer)
        Me.TabPageQuoteJobs.Controls.Add(Me.frameQuotes)
        Me.TabPageQuoteJobs.Controls.Add(Me.frameQuoteDetails)
        Me.TabPageQuoteJobs.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageQuoteJobs.Location = New System.Drawing.Point(4, 28)
        Me.TabPageQuoteJobs.Name = "TabPageQuoteJobs"
        Me.TabPageQuoteJobs.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageQuoteJobs.Size = New System.Drawing.Size(1064, 612)
        Me.TabPageQuoteJobs.TabIndex = 1
        Me.TabPageQuoteJobs.Text = "Quotes to Build"
        Me.TabPageQuoteJobs.UseVisualStyleBackColor = True
        '
        'frameQuoteCustomer
        '
        Me.frameQuoteCustomer.Controls.Add(Me.txtQuoteDetailsHdr)
        Me.frameQuoteCustomer.Controls.Add(Me.Label3)
        Me.frameQuoteCustomer.Location = New System.Drawing.Point(525, 6)
        Me.frameQuoteCustomer.Name = "frameQuoteCustomer"
        Me.frameQuoteCustomer.Size = New System.Drawing.Size(459, 97)
        Me.frameQuoteCustomer.TabIndex = 93
        Me.frameQuoteCustomer.TabStop = False
        Me.frameQuoteCustomer.Text = "frameQuoteCustomer"
        '
        'txtQuoteDetailsHdr
        '
        Me.txtQuoteDetailsHdr.AcceptsReturn = True
        Me.txtQuoteDetailsHdr.BackColor = System.Drawing.Color.White
        Me.txtQuoteDetailsHdr.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtQuoteDetailsHdr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQuoteDetailsHdr.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuoteDetailsHdr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtQuoteDetailsHdr.Location = New System.Drawing.Point(11, 39)
        Me.txtQuoteDetailsHdr.MaxLength = 0
        Me.txtQuoteDetailsHdr.Multiline = True
        Me.txtQuoteDetailsHdr.Name = "txtQuoteDetailsHdr"
        Me.txtQuoteDetailsHdr.ReadOnly = True
        Me.txtQuoteDetailsHdr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQuoteDetailsHdr.Size = New System.Drawing.Size(428, 52)
        Me.txtQuoteDetailsHdr.TabIndex = 89
        Me.txtQuoteDetailsHdr.Text = "txtQuoteDetailsHdr"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(77, Byte), Integer), CType(CType(77, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(21, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(172, 23)
        Me.Label3.TabIndex = 88
        Me.Label3.Text = "Quote Customer"
        '
        'BackgroundWorkerSqlConnect
        '
        '
        'ToolTipMain
        '
        Me.ToolTipMain.AutomaticDelay = 300
        Me.ToolTipMain.AutoPopDelay = 7000
        Me.ToolTipMain.InitialDelay = 300
        Me.ToolTipMain.ReshowDelay = 60
        Me.ToolTipMain.ShowAlways = True
        '
        'btnLaunchRAs
        '
        Me.btnLaunchRAs.CausesValidation = False
        Me.btnLaunchRAs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLaunchRAs.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLaunchRAs.ForeColor = System.Drawing.Color.Firebrick
        Me.btnLaunchRAs.Location = New System.Drawing.Point(782, 40)
        Me.btnLaunchRAs.Name = "btnLaunchRAs"
        Me.btnLaunchRAs.Size = New System.Drawing.Size(52, 33)
        Me.btnLaunchRAs.TabIndex = 2
        Me.btnLaunchRAs.Text = "RA's"
        Me.ToolTipMain.SetToolTip(Me.btnLaunchRAs, "Launch RAs (Returns) Process..")
        Me.btnLaunchRAs.UseVisualStyleBackColor = True
        '
        'openDlg1
        '
        Me.openDlg1.FileName = "OpenFileDialog1"
        '
        'TimerRAs
        '
        Me.TimerRAs.Interval = 1000
        '
        'BackgroundWorkerExchange201
        '
        Me.BackgroundWorkerExchange201.WorkerReportsProgress = True
        '
        'timerExchange2
        '
        Me.timerExchange2.Interval = 120000
        '
        'frmJobMatix42Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1070, 733)
        Me.Controls.Add(Me.btnLaunchRAs)
        Me.Controls.Add(Me.labReminderStatus)
        Me.Controls.Add(Me.TabControlMain)
        Me.Controls.Add(Me.panelSignOn)
        Me.Controls.Add(Me.labStatus)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.picLogo)
        Me.Controls.Add(Me.labVersion)
        Me.Controls.Add(Me.Picture2)
        Me.Controls.Add(Me.Picture1)
        Me.Controls.Add(Me.ListResults)
        Me.Controls.Add(Me.ListNames)
        Me.Controls.Add(Me.LabBusinessId)
        Me.Controls.Add(Me.LabBusiness)
        Me.Controls.Add(Me.LabToday)
        Me.Controls.Add(Me.MainMenu1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(11, 49)
        Me.Name = "frmJobMatix42Main"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "-JobTracking-"
        CType(Me.picJobDetailReturned, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frameJobsTab.ResumeLayout(False)
        Me.frameJobsTab.PerformLayout()
        Me.TabControlJobTracking.ResumeLayout(False)
        Me.TabPageJobsTree.ResumeLayout(False)
        Me.FrameJobsTree.ResumeLayout(False)
        Me.frameLegend.ResumeLayout(False)
        Me.frameLegend.PerformLayout()
        CType(Me.picWaitlisted, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picWarranty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureReturned, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicturePriority3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FrameJobsTree2.ResumeLayout(False)
        Me.panelReminder.ResumeLayout(False)
        Me.panelReminder.PerformLayout()
        Me.TabPageOnsite.ResumeLayout(False)
        Me.frameOnSite.ResumeLayout(False)
        CType(Me.dataGridViewOnSite, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageJobsGrid.ResumeLayout(False)
        Me.FrameBrowse.ResumeLayout(False)
        Me.FrameBrowse.PerformLayout()
        CType(Me.DataGridViewJobs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolbarJobs.ResumeLayout(False)
        Me.ToolbarJobs.PerformLayout()
        Me.TabPageCustomers.ResumeLayout(False)
        Me.frameCustomers.ResumeLayout(False)
        Me.frameCustomers.PerformLayout()
        CType(Me.DataGridViewCust, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FrameJobDetails.ResumeLayout(False)
        Me.FrameJobDetails.PerformLayout()
        CType(Me.picAttachments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripJobAction.ResumeLayout(False)
        Me.ToolStripJobAction.PerformLayout()
        Me.frameMainCmds.ResumeLayout(False)
        Me.frameMainCmds.PerformLayout()
        Me.toolbarNewJob2.ResumeLayout(False)
        Me.toolbarNewJob2.PerformLayout()
        CType(Me.picNoCustRecord, System.ComponentModel.ISupportInitialize).EndInit()
        Me.toolbarJobView.ResumeLayout(False)
        Me.toolbarJobView.PerformLayout()
        CType(Me.Picture3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frameQuotes.ResumeLayout(False)
        Me.frameQuoteDetails.ResumeLayout(False)
        Me.frameQuoteDetails.PerformLayout()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LabSearchJobs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptJobsOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MainMenu1.ResumeLayout(False)
        Me.MainMenu1.PerformLayout()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelSignOn.ResumeLayout(False)
        Me.panelSignOn.PerformLayout()
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPageJobTracking.ResumeLayout(False)
        Me.TabPageQuoteJobs.ResumeLayout(False)
        Me.frameQuoteCustomer.ResumeLayout(False)
        Me.frameQuoteCustomer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents labEmptyJobPanel As System.Windows.Forms.Label
    Friend WithEvents ToolStripJobAction As System.Windows.Forms.ToolStrip
    Friend WithEvents btnStopPress As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCheckIn As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAmend As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnReOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDeliver As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DataGridViewJobs As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewCust As System.Windows.Forms.DataGridView
    Friend WithEvents labVersion As System.Windows.Forms.Label
    Friend WithEvents labStaffLabel As System.Windows.Forms.Label
    Public WithEvents txtCustSearch As System.Windows.Forms.TextBox
    Public WithEvents cmdCustSearch As System.Windows.Forms.Button
    Public WithEvents cmdClearCustSearch As System.Windows.Forms.Button
    Friend WithEvents picLogo As System.Windows.Forms.PictureBox
    Friend WithEvents frameLegend As System.Windows.Forms.GroupBox
    Public WithEvents LabDeliveredOrder As System.Windows.Forms.Label
    Public WithEvents LabTreeStatus As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents PictureReturned As System.Windows.Forms.PictureBox
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents PicturePriority3 As System.Windows.Forms.PictureBox
    Public WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboJobsOrder As System.Windows.Forms.ComboBox
    Public WithEvents LabJobsOrder As System.Windows.Forms.Label
    Public WithEvents cmdRefreshJobsTree As System.Windows.Forms.Button
    Friend WithEvents _ToolbarJobs_ButtonQA As System.Windows.Forms.ToolStripButton
    Friend WithEvents frameQuotes As System.Windows.Forms.GroupBox
    Public WithEvents ListViewSalesOrders As System.Windows.Forms.ListView
    Friend WithEvents frameQuoteDetails As System.Windows.Forms.GroupBox
    Public WithEvents cmdBuildQuote As System.Windows.Forms.Button
    Public WithEvents ListViewQuote As System.Windows.Forms.ListView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents labQuoteOrderNo As System.Windows.Forms.Label
    Friend WithEvents LabQuotesHdr As System.Windows.Forms.Label
    Friend WithEvents labOrderDetail As System.Windows.Forms.Label
    Friend WithEvents chkNewQuotes As System.Windows.Forms.CheckBox
    Friend WithEvents cmdRefreshQuotes As System.Windows.Forms.Button
    Friend WithEvents labQuoteCount As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkRecentQuotes As System.Windows.Forms.CheckBox
    Friend WithEvents labLoadingQuotes As System.Windows.Forms.Label
    Friend WithEvents labJoblist As System.Windows.Forms.Label
    Friend WithEvents mnuGridFontSizes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGridFont_8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGridFont_9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuGridFont_10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripDropDownGridFont As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItemFontSize_8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemFontSize_9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemFontSize_10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnDetailNotify As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents _toolbarJobView_ButtonBackup As System.Windows.Forms.ToolStripButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents mnuPrinterAssignments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents picNoCustRecord As System.Windows.Forms.PictureBox
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents mnuStartupMaximised As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents labQuoteCanBuild As System.Windows.Forms.Label
    Public WithEvents picJobDetailReturned As System.Windows.Forms.PictureBox
    Friend WithEvents frameMainCmds As System.Windows.Forms.GroupBox
    Friend WithEvents toolbarNewJob2 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAcceptJob As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
    Public WithEvents labStatus As System.Windows.Forms.Label
    Friend WithEvents mnuFileRetailManagerDb As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkShowCompany1st As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents labDetailDateCreated As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents labDetailDatePromised As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents labDetailTech As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents labDetailDateUpdated As System.Windows.Forms.Label
    Friend WithEvents labDetailUpdatedDescription As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents mnuJobMatix As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDbRetailManager As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBackupJobTrackingDB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSqlServer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuWhoUsing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents labDetailJobDue As System.Windows.Forms.Label
    Friend WithEvents btnOnSiteJob As System.Windows.Forms.ToolStripButton
    Friend WithEvents frameOnSite As System.Windows.Forms.GroupBox
    Friend WithEvents dataGridViewOnSite As System.Windows.Forms.DataGridView
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents labRecCountOnSite As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents labDetailOnSiteJob As System.Windows.Forms.Label
    Friend WithEvents cmdRefreshOnSite As System.Windows.Forms.Button
    Friend WithEvents panelSignOn As System.Windows.Forms.Panel
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents panelReminder As System.Windows.Forms.Panel
    Friend WithEvents cmdDismissReminder As System.Windows.Forms.Button
    Friend WithEvents labReminder As System.Windows.Forms.Label
    Friend WithEvents mnuDontShowNotifyReminder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundWorkerSearch As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorkerGetSchema As System.ComponentModel.BackgroundWorker
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPageJobTracking As System.Windows.Forms.TabPage
    Friend WithEvents TabPageQuoteJobs As System.Windows.Forms.TabPage
    Friend WithEvents frameQuoteCustomer As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtQuoteDetailsHdr As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorkerSqlConnect As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolTipMain As System.Windows.Forms.ToolTip
    Friend WithEvents openDlg1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents picAttachments As System.Windows.Forms.PictureBox
    Friend WithEvents labDetailWarrantyJob As System.Windows.Forms.Label
    Friend WithEvents NewOnSiteJobToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutJobMatix32ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnReturnToQueue As System.Windows.Forms.ToolStripButton
    Friend WithEvents picWarranty As System.Windows.Forms.PictureBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents picWaitlisted As System.Windows.Forms.PictureBox
    Public WithEvents chkHideWaitList As System.Windows.Forms.CheckBox
    Friend WithEvents TimerRAs As System.Windows.Forms.Timer
    Friend WithEvents labOnSiteReminder As System.Windows.Forms.Label
    Friend WithEvents labReminderStatus As System.Windows.Forms.Label
    Friend WithEvents mnuEnableSMSReminders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAutoAssignOrphanJobsOnUpdate As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents labJobsFilter As System.Windows.Forms.Label
    Friend WithEvents cboJobsFilter As System.Windows.Forms.ComboBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents btnLaunchRAs As System.Windows.Forms.Button
    Friend WithEvents btnCustNewCustomer As System.Windows.Forms.Button
    Public WithEvents txtStaffBarcode As System.Windows.Forms.TextBox
    Friend WithEvents mnuStaySignedOn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLongSignOff As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShortSignOff As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents labStaffTimeRemaining As System.Windows.Forms.Label
    Public WithEvents tvwJobs As JobMatix3.clsMyTreeView
    Friend WithEvents BackgroundWorkerExchange201 As System.ComponentModel.BackgroundWorker
    Friend WithEvents datePromised As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Customer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents techName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents jobNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents JobStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControlJobTracking As System.Windows.Forms.TabControl
    Friend WithEvents TabPageJobsTree As System.Windows.Forms.TabPage
    Friend WithEvents TabPageOnsite As System.Windows.Forms.TabPage
    Friend WithEvents TabPageJobsGrid As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCustomers As System.Windows.Forms.TabPage
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents timerExchange2 As System.Windows.Forms.Timer
    Friend WithEvents labMainCustTags As System.Windows.Forms.Label
#End Region
End Class