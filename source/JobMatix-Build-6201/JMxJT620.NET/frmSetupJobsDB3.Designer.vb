<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSetupJobsDB
#Region "Windows Form Designer generated code "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
        Me.mbIsInitialising = True
        'This call is required by the Windows Form Designer.
		InitializeComponent()
        '== see Form LOAD event..--   Me.mbIsInitialising = False
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
	Public dlg1Font As System.Windows.Forms.FontDialog
	Public WithEvents _optRetailHost_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optRetailHost_0 As System.Windows.Forms.RadioButton
	Public WithEvents txtState As System.Windows.Forms.TextBox
	Public WithEvents txtPhone As System.Windows.Forms.TextBox
	Public WithEvents txtPostCode As System.Windows.Forms.TextBox
	Public WithEvents cboState As System.Windows.Forms.ComboBox
	Public WithEvents txtFullName As System.Windows.Forms.TextBox
	Public WithEvents txtAddress1 As System.Windows.Forms.TextBox
	Public WithEvents txtAddress2 As System.Windows.Forms.TextBox
	Public WithEvents labRetailHost As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents LabWarnPostcode As System.Windows.Forms.Label
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents frameBusiness As System.Windows.Forms.GroupBox
	Public WithEvents txtUserLicence As System.Windows.Forms.TextBox
	Public WithEvents txtShortName As System.Windows.Forms.TextBox
	Public WithEvents txtABN As System.Windows.Forms.TextBox
	Public WithEvents txtNewDBName As System.Windows.Forms.TextBox
	Public WithEvents Label45 As System.Windows.Forms.Label
	Public WithEvents labShortName As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents LabDatabaseName As System.Windows.Forms.Label
	Public WithEvents frameABN As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents _txtPriority_2 As System.Windows.Forms.TextBox
	Public WithEvents _txtPriority_1 As System.Windows.Forms.TextBox
	Public WithEvents _txtPriority_0 As System.Windows.Forms.TextBox
	Public WithEvents _txtLabourRates_4 As System.Windows.Forms.TextBox
	Public WithEvents _txtLabourRates_0 As System.Windows.Forms.TextBox
	Public WithEvents _txtLabourRates_1 As System.Windows.Forms.TextBox
	Public WithEvents _txtLabourRates_2 As System.Windows.Forms.TextBox
	Public WithEvents _txtLabourRates_3 As System.Windows.Forms.TextBox
	Public WithEvents txtGSTPercentage As System.Windows.Forms.TextBox
    Public WithEvents Label34 As System.Windows.Forms.Label
	Public WithEvents Label33 As System.Windows.Forms.Label
	Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
	Public WithEvents Label22 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents frameLabour As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents _txtChassisCat_1 As System.Windows.Forms.TextBox
	Public WithEvents _txtChassisCat_0 As System.Windows.Forms.TextBox
	Public WithEvents _txtServiceCategory_1 As System.Windows.Forms.TextBox
	Public WithEvents _txtServiceCategory_0 As System.Windows.Forms.TextBox
	Public WithEvents Line1 As System.Windows.Forms.Label
    Public WithEvents labChassisCatPrompt1 As System.Windows.Forms.Label
    Public WithEvents labChassisCatPrompt0 As System.Windows.Forms.Label
    Public WithEvents LabHelpChassis As System.Windows.Forms.Label
    Public WithEvents LabChassisCats As System.Windows.Forms.Label
    Public WithEvents LabHelpServiceCat As System.Windows.Forms.Label
    Public WithEvents labServiceCatPrompt1 As System.Windows.Forms.Label
    Public WithEvents labServiceCatPrompt0 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents frameCategories As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents cmdRestoreTerms As System.Windows.Forms.Button
    Public WithEvents cmdBarcodeFont As System.Windows.Forms.Button
    Public WithEvents txtBarcodeFontSize As System.Windows.Forms.TextBox
    Public WithEvents txtBarcodeFontName As System.Windows.Forms.TextBox
    Public WithEvents _txtUserTexts_0 As System.Windows.Forms.TextBox
    Public WithEvents _txtUserTexts_1 As System.Windows.Forms.TextBox
    Public WithEvents _txtUserTexts_2 As System.Windows.Forms.TextBox
    Public WithEvents _txtUserTexts_3 As System.Windows.Forms.TextBox
    Public WithEvents LabHelpBarcode As System.Windows.Forms.Label
    Public WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents frameTexts As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents cmdBrowsePath As System.Windows.Forms.Button
    Public WithEvents _txtServerBackupFolder_1 As System.Windows.Forms.TextBox
    Public WithEvents _txtServerBackupFolder_0 As System.Windows.Forms.TextBox
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents LabHelpBackupPath As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents frameBackupPath As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents LabHdr3 As System.Windows.Forms.Label
    Public WithEvents labHdr2 As System.Windows.Forms.Label
    Public WithEvents LabVersion As System.Windows.Forms.Label
    Public WithEvents labServer As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents labStatus As System.Windows.Forms.Label
    Public WithEvents LabHdr1 As System.Windows.Forms.Label
    Public WithEvents optRetailHost As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtChassisCat As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    Public WithEvents txtLabourRates As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    Public WithEvents txtPriority As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    Public WithEvents txtServerBackupFolder As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    Public WithEvents txtServiceCategory As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    Public WithEvents txtUserTexts As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSetupJobsDB))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdRestoreTerms = New System.Windows.Forms.Button()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.labPOSWarning = New System.Windows.Forms.Label()
        Me.btnP3Browse = New System.Windows.Forms.Button()
        Me.btnP2Browse = New System.Windows.Forms.Button()
        Me.btnP1Browse = New System.Windows.Forms.Button()
        Me.btnOnSiteP1Browse = New System.Windows.Forms.Button()
        Me.btnOnSiteP2Browse = New System.Windows.Forms.Button()
        Me.btnOnSiteP3Browse = New System.Windows.Forms.Button()
        Me.optBusinessTypeComputer = New System.Windows.Forms.RadioButton()
        Me.optBusinessTypeOther = New System.Windows.Forms.RadioButton()
        Me.dlg1Font = New System.Windows.Forms.FontDialog()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.frameBusiness = New System.Windows.Forms.GroupBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.labHostName = New System.Windows.Forms.Label()
        Me._optRetailHost_1 = New System.Windows.Forms.RadioButton()
        Me._optRetailHost_0 = New System.Windows.Forms.RadioButton()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtPostCode = New System.Windows.Forms.TextBox()
        Me.cboState = New System.Windows.Forms.ComboBox()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.txtAddress1 = New System.Windows.Forms.TextBox()
        Me.txtAddress2 = New System.Windows.Forms.TextBox()
        Me.labRetailHost = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LabWarnPostcode = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.frameABN = New System.Windows.Forms.GroupBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.txtUserLicence = New System.Windows.Forms.TextBox()
        Me.txtShortName = New System.Windows.Forms.TextBox()
        Me.txtABN = New System.Windows.Forms.TextBox()
        Me.txtNewDBName = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.labShortName = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LabDatabaseName = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.frameCategories = New System.Windows.Forms.GroupBox()
        Me._txtChassisCat_1 = New System.Windows.Forms.TextBox()
        Me._txtChassisCat_0 = New System.Windows.Forms.TextBox()
        Me._txtServiceCategory_1 = New System.Windows.Forms.TextBox()
        Me._txtServiceCategory_0 = New System.Windows.Forms.TextBox()
        Me.Line1 = New System.Windows.Forms.Label()
        Me.labChassisCatPrompt1 = New System.Windows.Forms.Label()
        Me.labChassisCatPrompt0 = New System.Windows.Forms.Label()
        Me.LabHelpChassis = New System.Windows.Forms.Label()
        Me.LabChassisCats = New System.Windows.Forms.Label()
        Me.LabHelpServiceCat = New System.Windows.Forms.Label()
        Me.labServiceCatPrompt1 = New System.Windows.Forms.Label()
        Me.labServiceCatPrompt0 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.frameLabour = New System.Windows.Forms.GroupBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.TabControlLabourRates = New System.Windows.Forms.TabControl()
        Me.TabPageWorkshop = New System.Windows.Forms.TabPage()
        Me.panelHourlyRateDefsWorkshop = New System.Windows.Forms.Panel()
        Me.labP3Rate = New System.Windows.Forms.Label()
        Me.labP3Description = New System.Windows.Forms.Label()
        Me.txtLabourP3Barcode = New System.Windows.Forms.TextBox()
        Me.labP2Rate = New System.Windows.Forms.Label()
        Me.labP2Description = New System.Windows.Forms.Label()
        Me.txtLabourP2Barcode = New System.Windows.Forms.TextBox()
        Me.labP1Rate = New System.Windows.Forms.Label()
        Me.labP1Description = New System.Windows.Forms.Label()
        Me.txtLabourP1Barcode = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TabPageOnSite = New System.Windows.Forms.TabPage()
        Me.panelHourlyRateDefsOnSite = New System.Windows.Forms.Panel()
        Me.txtLabourOnSiteP3Barcode = New System.Windows.Forms.TextBox()
        Me.labOnSiteP3Rate = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.labOnSiteP3Description = New System.Windows.Forms.Label()
        Me.txtLabourOnSiteP2Barcode = New System.Windows.Forms.TextBox()
        Me.labOnSiteP2Rate = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.labOnSiteP2Description = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtLabourOnSiteP1Barcode = New System.Windows.Forms.TextBox()
        Me.labOnSiteP1Rate = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.labOnSiteP1Description = New System.Windows.Forms.Label()
        Me.panelOldLabourRates = New System.Windows.Forms.Panel()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me._txtLabourRates_0 = New System.Windows.Forms.TextBox()
        Me._txtPriority_2 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me._txtPriority_1 = New System.Windows.Forms.TextBox()
        Me._txtLabourRates_2 = New System.Windows.Forms.TextBox()
        Me._txtPriority_0 = New System.Windows.Forms.TextBox()
        Me._txtLabourRates_1 = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chkNoEnforceMinCharge = New System.Windows.Forms.CheckBox()
        Me._txtLabourRates_4 = New System.Windows.Forms.TextBox()
        Me._txtLabourRates_3 = New System.Windows.Forms.TextBox()
        Me.txtGSTPercentage = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.frameTexts = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmdBarcodeFont = New System.Windows.Forms.Button()
        Me.txtBarcodeFontSize = New System.Windows.Forms.TextBox()
        Me.txtBarcodeFontName = New System.Windows.Forms.TextBox()
        Me._txtUserTexts_0 = New System.Windows.Forms.TextBox()
        Me._txtUserTexts_1 = New System.Windows.Forms.TextBox()
        Me._txtUserTexts_2 = New System.Windows.Forms.TextBox()
        Me._txtUserTexts_3 = New System.Windows.Forms.TextBox()
        Me.LabHelpBarcode = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me.frameBackupPath = New System.Windows.Forms.GroupBox()
        Me.panelBackupPaths = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmdBrowsePath = New System.Windows.Forms.Button()
        Me._txtServerBackupFolder_1 = New System.Windows.Forms.TextBox()
        Me._txtServerBackupFolder_0 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.LabHelpBackupPath = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.LabHdr3 = New System.Windows.Forms.Label()
        Me.labHdr2 = New System.Windows.Forms.Label()
        Me.LabVersion = New System.Windows.Forms.Label()
        Me.labServer = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.LabHdr1 = New System.Windows.Forms.Label()
        Me.optRetailHost = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtChassisCat = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.txtLabourRates = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.txtPriority = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.txtServerBackupFolder = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.txtServiceCategory = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.txtUserTexts = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.frameBusiness.SuspendLayout()
        Me.frameABN.SuspendLayout()
        Me._SSTab1_TabPage2.SuspendLayout()
        Me.frameCategories.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.frameLabour.SuspendLayout()
        Me.TabControlLabourRates.SuspendLayout()
        Me.TabPageWorkshop.SuspendLayout()
        Me.panelHourlyRateDefsWorkshop.SuspendLayout()
        Me.TabPageOnSite.SuspendLayout()
        Me.panelHourlyRateDefsOnSite.SuspendLayout()
        Me.panelOldLabourRates.SuspendLayout()
        Me._SSTab1_TabPage3.SuspendLayout()
        Me.frameTexts.SuspendLayout()
        Me._SSTab1_TabPage4.SuspendLayout()
        Me.frameBackupPath.SuspendLayout()
        Me.panelBackupPaths.SuspendLayout()
        CType(Me.optRetailHost, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtChassisCat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLabourRates, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPriority, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServerBackupFolder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtServiceCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUserTexts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdRestoreTerms
        '
        Me.cmdRestoreTerms.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.cmdRestoreTerms.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRestoreTerms.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRestoreTerms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRestoreTerms.Location = New System.Drawing.Point(511, 19)
        Me.cmdRestoreTerms.Name = "cmdRestoreTerms"
        Me.cmdRestoreTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRestoreTerms.Size = New System.Drawing.Size(73, 23)
        Me.cmdRestoreTerms.TabIndex = 100
        Me.cmdRestoreTerms.Text = "Restore"
        Me.ToolTip1.SetToolTip(Me.cmdRestoreTerms, "Restore standard Terms text..")
        Me.cmdRestoreTerms.UseVisualStyleBackColor = False
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.Enabled = False
        Me.cmdOK.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(485, 641)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(92, 26)
        Me.cmdOK.TabIndex = 37
        Me.cmdOK.Text = "OK- Create"
        Me.ToolTip1.SetToolTip(Me.cmdOK, "Save details and exit..")
        Me.cmdOK.UseVisualStyleBackColor = False
        '
        'labPOSWarning
        '
        Me.labPOSWarning.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labPOSWarning.ForeColor = System.Drawing.Color.DarkBlue
        Me.labPOSWarning.Location = New System.Drawing.Point(439, 230)
        Me.labPOSWarning.Name = "labPOSWarning"
        Me.labPOSWarning.Size = New System.Drawing.Size(205, 65)
        Me.labPOSWarning.TabIndex = 103
        Me.labPOSWarning.Text = "Please Note: As of Build 4267, The JobMatix POS Retail system is at Beta++ level." & _
    " Use with regular DB Backups..  Check online for later versions of the POS dll.." & _
    ""
        Me.ToolTip1.SetToolTip(Me.labPOSWarning, "Use to explore the POS functionality.")
        '
        'btnP3Browse
        '
        Me.btnP3Browse.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnP3Browse.Location = New System.Drawing.Point(288, 185)
        Me.btnP3Browse.Name = "btnP3Browse"
        Me.btnP3Browse.Size = New System.Drawing.Size(68, 21)
        Me.btnP3Browse.TabIndex = 84
        Me.btnP3Browse.Text = "Browse.."
        Me.ToolTip1.SetToolTip(Me.btnP3Browse, "Lookup Retail Stock Table and select W'shop P3 Labour item..")
        Me.btnP3Browse.UseVisualStyleBackColor = True
        '
        'btnP2Browse
        '
        Me.btnP2Browse.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnP2Browse.Location = New System.Drawing.Point(288, 125)
        Me.btnP2Browse.Name = "btnP2Browse"
        Me.btnP2Browse.Size = New System.Drawing.Size(68, 21)
        Me.btnP2Browse.TabIndex = 82
        Me.btnP2Browse.Text = "Browse.."
        Me.ToolTip1.SetToolTip(Me.btnP2Browse, "Lookup Retail Stock Table and select W'shop P2 Labour item..")
        Me.btnP2Browse.UseVisualStyleBackColor = True
        '
        'btnP1Browse
        '
        Me.btnP1Browse.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnP1Browse.Location = New System.Drawing.Point(288, 73)
        Me.btnP1Browse.Name = "btnP1Browse"
        Me.btnP1Browse.Size = New System.Drawing.Size(68, 21)
        Me.btnP1Browse.TabIndex = 80
        Me.btnP1Browse.Text = "Browse.."
        Me.ToolTip1.SetToolTip(Me.btnP1Browse, "Lookup Retail Stock Table and select W'shop P1 Labour item..")
        Me.btnP1Browse.UseVisualStyleBackColor = True
        '
        'btnOnSiteP1Browse
        '
        Me.btnOnSiteP1Browse.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOnSiteP1Browse.Location = New System.Drawing.Point(288, 73)
        Me.btnOnSiteP1Browse.Name = "btnOnSiteP1Browse"
        Me.btnOnSiteP1Browse.Size = New System.Drawing.Size(68, 21)
        Me.btnOnSiteP1Browse.TabIndex = 86
        Me.btnOnSiteP1Browse.Text = "Browse.."
        Me.ToolTip1.SetToolTip(Me.btnOnSiteP1Browse, "Lookup Retail Stock Table and select On-site P1 Labour item..")
        Me.btnOnSiteP1Browse.UseVisualStyleBackColor = True
        '
        'btnOnSiteP2Browse
        '
        Me.btnOnSiteP2Browse.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOnSiteP2Browse.Location = New System.Drawing.Point(288, 130)
        Me.btnOnSiteP2Browse.Name = "btnOnSiteP2Browse"
        Me.btnOnSiteP2Browse.Size = New System.Drawing.Size(68, 21)
        Me.btnOnSiteP2Browse.TabIndex = 99
        Me.btnOnSiteP2Browse.Text = "Browse.."
        Me.ToolTip1.SetToolTip(Me.btnOnSiteP2Browse, "Lookup Retail Stock Table and select On-site P2 Labour item..")
        Me.btnOnSiteP2Browse.UseVisualStyleBackColor = True
        '
        'btnOnSiteP3Browse
        '
        Me.btnOnSiteP3Browse.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOnSiteP3Browse.Location = New System.Drawing.Point(288, 186)
        Me.btnOnSiteP3Browse.Name = "btnOnSiteP3Browse"
        Me.btnOnSiteP3Browse.Size = New System.Drawing.Size(68, 21)
        Me.btnOnSiteP3Browse.TabIndex = 104
        Me.btnOnSiteP3Browse.Text = "Browse.."
        Me.ToolTip1.SetToolTip(Me.btnOnSiteP3Browse, "Lookup Retail Stock Table and select On-site P3 Labour item..")
        Me.btnOnSiteP3Browse.UseVisualStyleBackColor = True
        '
        'optBusinessTypeComputer
        '
        Me.optBusinessTypeComputer.BackColor = System.Drawing.Color.Lavender
        Me.optBusinessTypeComputer.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBusinessTypeComputer.Location = New System.Drawing.Point(445, 72)
        Me.optBusinessTypeComputer.Name = "optBusinessTypeComputer"
        Me.optBusinessTypeComputer.Size = New System.Drawing.Size(79, 22)
        Me.optBusinessTypeComputer.TabIndex = 13
        Me.optBusinessTypeComputer.TabStop = True
        Me.optBusinessTypeComputer.Text = "Computer"
        Me.ToolTip1.SetToolTip(Me.optBusinessTypeComputer, "This will set up Reference Tables for Goods, Brands, Tasks etc suitable for a Com" & _
        "puter Shop/IT Service business.")
        Me.optBusinessTypeComputer.UseVisualStyleBackColor = False
        '
        'optBusinessTypeOther
        '
        Me.optBusinessTypeOther.BackColor = System.Drawing.Color.Lavender
        Me.optBusinessTypeOther.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBusinessTypeOther.Location = New System.Drawing.Point(530, 72)
        Me.optBusinessTypeOther.Name = "optBusinessTypeOther"
        Me.optBusinessTypeOther.Size = New System.Drawing.Size(79, 22)
        Me.optBusinessTypeOther.TabIndex = 14
        Me.optBusinessTypeOther.TabStop = True
        Me.optBusinessTypeOther.Text = "Other"
        Me.ToolTip1.SetToolTip(Me.optBusinessTypeOther, "This will set up dummy Reference Tables..  To be completed later by the Business." & _
        "")
        Me.optBusinessTypeOther.UseVisualStyleBackColor = False
        '
        'SSTab1
        '
        Me.SSTab1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(16, 77)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(685, 550)
        Me.SSTab1.TabIndex = 0
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.BackColor = System.Drawing.Color.White
        Me._SSTab1_TabPage0.Controls.Add(Me.frameBusiness)
        Me._SSTab1_TabPage0.Controls.Add(Me.frameABN)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(677, 524)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Business Details"
        '
        'frameBusiness
        '
        Me.frameBusiness.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameBusiness.Controls.Add(Me.txtEmail)
        Me.frameBusiness.Controls.Add(Me.Label30)
        Me.frameBusiness.Controls.Add(Me.labPOSWarning)
        Me.frameBusiness.Controls.Add(Me.labHostName)
        Me.frameBusiness.Controls.Add(Me._optRetailHost_1)
        Me.frameBusiness.Controls.Add(Me._optRetailHost_0)
        Me.frameBusiness.Controls.Add(Me.txtState)
        Me.frameBusiness.Controls.Add(Me.txtPhone)
        Me.frameBusiness.Controls.Add(Me.txtPostCode)
        Me.frameBusiness.Controls.Add(Me.cboState)
        Me.frameBusiness.Controls.Add(Me.txtFullName)
        Me.frameBusiness.Controls.Add(Me.txtAddress1)
        Me.frameBusiness.Controls.Add(Me.txtAddress2)
        Me.frameBusiness.Controls.Add(Me.labRetailHost)
        Me.frameBusiness.Controls.Add(Me.Label5)
        Me.frameBusiness.Controls.Add(Me.LabWarnPostcode)
        Me.frameBusiness.Controls.Add(Me.Label18)
        Me.frameBusiness.Controls.Add(Me.Label20)
        Me.frameBusiness.Controls.Add(Me.Label2)
        Me.frameBusiness.Controls.Add(Me.Label3)
        Me.frameBusiness.Controls.Add(Me.Label4)
        Me.frameBusiness.Controls.Add(Me.Label19)
        Me.frameBusiness.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameBusiness.Location = New System.Drawing.Point(3, 3)
        Me.frameBusiness.Name = "frameBusiness"
        Me.frameBusiness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameBusiness.Size = New System.Drawing.Size(669, 303)
        Me.frameBusiness.TabIndex = 43
        Me.frameBusiness.TabStop = False
        Me.frameBusiness.Text = "frameBusiness"
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.BackColor = System.Drawing.Color.Lavender
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmail.Location = New System.Drawing.Point(80, 195)
        Me.txtEmail.MaxLength = 127
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail.Size = New System.Drawing.Size(526, 14)
        Me.txtEmail.TabIndex = 8
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(16, 192)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(62, 17)
        Me.Label30.TabIndex = 105
        Me.Label30.Text = "Email:"
        '
        'labHostName
        '
        Me.labHostName.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHostName.Location = New System.Drawing.Point(56, 282)
        Me.labHostName.Name = "labHostName"
        Me.labHostName.Size = New System.Drawing.Size(184, 17)
        Me.labHostName.TabIndex = 102
        '
        '_optRetailHost_1
        '
        Me._optRetailHost_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me._optRetailHost_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRetailHost_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRetailHost_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRetailHost.SetIndex(Me._optRetailHost_1, CType(1, Short))
        Me._optRetailHost_1.Location = New System.Drawing.Point(313, 233)
        Me._optRetailHost_1.Name = "_optRetailHost_1"
        Me._optRetailHost_1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me._optRetailHost_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRetailHost_1.Size = New System.Drawing.Size(111, 47)
        Me._optRetailHost_1.TabIndex = 10
        Me._optRetailHost_1.TabStop = True
        Me._optRetailHost_1.Tag = "JobMatixPOS"
        Me._optRetailHost_1.Text = "JobMatixPOS PointOfSale"
        Me._optRetailHost_1.UseVisualStyleBackColor = False
        '
        '_optRetailHost_0
        '
        Me._optRetailHost_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me._optRetailHost_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRetailHost_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRetailHost_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRetailHost.SetIndex(Me._optRetailHost_0, CType(0, Short))
        Me._optRetailHost_0.Location = New System.Drawing.Point(198, 233)
        Me._optRetailHost_0.Name = "_optRetailHost_0"
        Me._optRetailHost_0.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me._optRetailHost_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRetailHost_0.Size = New System.Drawing.Size(114, 47)
        Me._optRetailHost_0.TabIndex = 9
        Me._optRetailHost_0.TabStop = True
        Me._optRetailHost_0.Tag = "RetailManager"
        Me._optRetailHost_0.Text = "MYOB Retail-Manager"
        Me._optRetailHost_0.UseVisualStyleBackColor = False
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.BackColor = System.Drawing.Color.Gainsboro
        Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtState.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtState.Location = New System.Drawing.Point(154, 123)
        Me.txtState.MaxLength = 4
        Me.txtState.Name = "txtState"
        Me.txtState.ReadOnly = True
        Me.txtState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtState.Size = New System.Drawing.Size(49, 15)
        Me.txtState.TabIndex = 5
        Me.txtState.TabStop = False
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.Color.Lavender
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPhone.Location = New System.Drawing.Point(80, 167)
        Me.txtPhone.MaxLength = 24
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(244, 14)
        Me.txtPhone.TabIndex = 7
        '
        'txtPostCode
        '
        Me.txtPostCode.AcceptsReturn = True
        Me.txtPostCode.BackColor = System.Drawing.Color.Lavender
        Me.txtPostCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPostCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPostCode.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPostCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPostCode.Location = New System.Drawing.Point(269, 123)
        Me.txtPostCode.MaxLength = 4
        Me.txtPostCode.Name = "txtPostCode"
        Me.txtPostCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPostCode.Size = New System.Drawing.Size(49, 15)
        Me.txtPostCode.TabIndex = 6
        '
        'cboState
        '
        Me.cboState.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.cboState.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboState.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboState.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboState.Location = New System.Drawing.Point(80, 121)
        Me.cboState.Name = "cboState"
        Me.cboState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboState.Size = New System.Drawing.Size(73, 21)
        Me.cboState.TabIndex = 4
        '
        'txtFullName
        '
        Me.txtFullName.AcceptsReturn = True
        Me.txtFullName.BackColor = System.Drawing.Color.Lavender
        Me.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFullName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFullName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFullName.Location = New System.Drawing.Point(80, 31)
        Me.txtFullName.MaxLength = 50
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFullName.Size = New System.Drawing.Size(337, 14)
        Me.txtFullName.TabIndex = 1
        '
        'txtAddress1
        '
        Me.txtAddress1.AcceptsReturn = True
        Me.txtAddress1.BackColor = System.Drawing.Color.Lavender
        Me.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAddress1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddress1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddress1.Location = New System.Drawing.Point(80, 61)
        Me.txtAddress1.MaxLength = 50
        Me.txtAddress1.Name = "txtAddress1"
        Me.txtAddress1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress1.Size = New System.Drawing.Size(337, 14)
        Me.txtAddress1.TabIndex = 2
        '
        'txtAddress2
        '
        Me.txtAddress2.AcceptsReturn = True
        Me.txtAddress2.BackColor = System.Drawing.Color.Lavender
        Me.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAddress2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddress2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddress2.Location = New System.Drawing.Point(80, 89)
        Me.txtAddress2.MaxLength = 50
        Me.txtAddress2.Name = "txtAddress2"
        Me.txtAddress2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress2.Size = New System.Drawing.Size(337, 14)
        Me.txtAddress2.TabIndex = 3
        '
        'labRetailHost
        '
        Me.labRetailHost.BackColor = System.Drawing.Color.Transparent
        Me.labRetailHost.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRetailHost.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRetailHost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRetailHost.Location = New System.Drawing.Point(75, 233)
        Me.labRetailHost.Name = "labRetailHost"
        Me.labRetailHost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRetailHost.Size = New System.Drawing.Size(117, 49)
        Me.labRetailHost.TabIndex = 101
        Me.labRetailHost.Text = "Selected Stock/POS (Retail) System that will be used:"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(432, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(174, 92)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "These details will identfy your business on the Customer Service Agreement, the J" & _
    "ob Delivery Form, and all Dockets.."
        '
        'LabWarnPostcode
        '
        Me.LabWarnPostcode.BackColor = System.Drawing.Color.Transparent
        Me.LabWarnPostcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabWarnPostcode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabWarnPostcode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.LabWarnPostcode.Location = New System.Drawing.Point(370, 121)
        Me.LabWarnPostcode.Name = "LabWarnPostcode"
        Me.LabWarnPostcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabWarnPostcode.Size = New System.Drawing.Size(274, 41)
        Me.LabWarnPostcode.TabIndex = 50
        Me.LabWarnPostcode.Text = "State and Postcode can't be changed later."
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(15, 152)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(62, 29)
        Me.Label18.TabIndex = 49
        Me.Label18.Text = "Business Phone:"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(204, 123)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(63, 17)
        Me.Label20.TabIndex = 48
        Me.Label20.Text = "PostCode:"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(15, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(53, 30)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Business Name"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(16, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Address-1"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(15, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(62, 25)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Address-2"
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(33, 121)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(41, 17)
        Me.Label19.TabIndex = 44
        Me.Label19.Text = "State:"
        '
        'frameABN
        '
        Me.frameABN.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameABN.Controls.Add(Me.Label48)
        Me.frameABN.Controls.Add(Me.optBusinessTypeOther)
        Me.frameABN.Controls.Add(Me.optBusinessTypeComputer)
        Me.frameABN.Controls.Add(Me.txtUserLicence)
        Me.frameABN.Controls.Add(Me.txtShortName)
        Me.frameABN.Controls.Add(Me.txtABN)
        Me.frameABN.Controls.Add(Me.txtNewDBName)
        Me.frameABN.Controls.Add(Me.Label45)
        Me.frameABN.Controls.Add(Me.labShortName)
        Me.frameABN.Controls.Add(Me.Label7)
        Me.frameABN.Controls.Add(Me.Label8)
        Me.frameABN.Controls.Add(Me.LabDatabaseName)
        Me.frameABN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameABN.Location = New System.Drawing.Point(5, 307)
        Me.frameABN.Name = "frameABN"
        Me.frameABN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameABN.Size = New System.Drawing.Size(669, 210)
        Me.frameABN.TabIndex = 59
        Me.frameABN.TabStop = False
        Me.frameABN.Text = "frameABN"
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.Transparent
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(444, 53)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(160, 16)
        Me.Label48.TabIndex = 90
        Me.Label48.Text = "Type of User Business"
        '
        'txtUserLicence
        '
        Me.txtUserLicence.AcceptsReturn = True
        Me.txtUserLicence.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtUserLicence.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUserLicence.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserLicence.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserLicence.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserLicence.Location = New System.Drawing.Point(159, 181)
        Me.txtUserLicence.MaxLength = 32
        Me.txtUserLicence.Name = "txtUserLicence"
        Me.txtUserLicence.ReadOnly = True
        Me.txtUserLicence.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserLicence.Size = New System.Drawing.Size(393, 14)
        Me.txtUserLicence.TabIndex = 16
        '
        'txtShortName
        '
        Me.txtShortName.AcceptsReturn = True
        Me.txtShortName.BackColor = System.Drawing.Color.Lavender
        Me.txtShortName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtShortName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShortName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtShortName.Location = New System.Drawing.Point(159, 43)
        Me.txtShortName.MaxLength = 24
        Me.txtShortName.Name = "txtShortName"
        Me.txtShortName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShortName.Size = New System.Drawing.Size(242, 14)
        Me.txtShortName.TabIndex = 11
        '
        'txtABN
        '
        Me.txtABN.AcceptsReturn = True
        Me.txtABN.BackColor = System.Drawing.Color.Lavender
        Me.txtABN.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtABN.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtABN.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtABN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtABN.Location = New System.Drawing.Point(159, 76)
        Me.txtABN.MaxLength = 20
        Me.txtABN.Name = "txtABN"
        Me.txtABN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtABN.Size = New System.Drawing.Size(242, 14)
        Me.txtABN.TabIndex = 12
        '
        'txtNewDBName
        '
        Me.txtNewDBName.AcceptsReturn = True
        Me.txtNewDBName.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtNewDBName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNewDBName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNewDBName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewDBName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNewDBName.Location = New System.Drawing.Point(159, 128)
        Me.txtNewDBName.MaxLength = 0
        Me.txtNewDBName.Name = "txtNewDBName"
        Me.txtNewDBName.ReadOnly = True
        Me.txtNewDBName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNewDBName.Size = New System.Drawing.Size(393, 14)
        Me.txtNewDBName.TabIndex = 15
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.Transparent
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(156, 163)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(315, 15)
        Me.Label45.TabIndex = 87
        Me.Label45.Text = "JobMatix End-user Licence (when available).."
        '
        'labShortName
        '
        Me.labShortName.BackColor = System.Drawing.Color.Transparent
        Me.labShortName.Cursor = System.Windows.Forms.Cursors.Default
        Me.labShortName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labShortName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labShortName.Location = New System.Drawing.Point(8, 22)
        Me.labShortName.Margin = New System.Windows.Forms.Padding(1, 0, 13, 0)
        Me.labShortName.Name = "labShortName"
        Me.labShortName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labShortName.Size = New System.Drawing.Size(145, 47)
        Me.labShortName.TabIndex = 63
        Me.labShortName.Text = "Business Short-Name   (Letters && digits only): (Can't be changed later)"
        Me.labShortName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(8, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(145, 17)
        Me.Label7.TabIndex = 62
        Me.Label7.Text = "Business ABN: (11 digits)"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(6, 110)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(108, 85)
        Me.Label8.TabIndex = 61
        Me.Label8.Text = "These Details will identify this JobTracking Database and End-user Licence."
        '
        'LabDatabaseName
        '
        Me.LabDatabaseName.BackColor = System.Drawing.Color.Transparent
        Me.LabDatabaseName.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDatabaseName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDatabaseName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDatabaseName.Location = New System.Drawing.Point(156, 110)
        Me.LabDatabaseName.Name = "LabDatabaseName"
        Me.LabDatabaseName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDatabaseName.Size = New System.Drawing.Size(368, 15)
        Me.LabDatabaseName.TabIndex = 60
        Me.LabDatabaseName.Text = "The JobMatix Database will be created with this DB Name:"
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.BackColor = System.Drawing.Color.WhiteSmoke
        Me._SSTab1_TabPage2.Controls.Add(Me.frameCategories)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(677, 524)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "Stock Category Defs."
        '
        'frameCategories
        '
        Me.frameCategories.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameCategories.Controls.Add(Me._txtChassisCat_1)
        Me.frameCategories.Controls.Add(Me._txtChassisCat_0)
        Me.frameCategories.Controls.Add(Me._txtServiceCategory_1)
        Me.frameCategories.Controls.Add(Me._txtServiceCategory_0)
        Me.frameCategories.Controls.Add(Me.Line1)
        Me.frameCategories.Controls.Add(Me.labChassisCatPrompt1)
        Me.frameCategories.Controls.Add(Me.labChassisCatPrompt0)
        Me.frameCategories.Controls.Add(Me.LabHelpChassis)
        Me.frameCategories.Controls.Add(Me.LabChassisCats)
        Me.frameCategories.Controls.Add(Me.LabHelpServiceCat)
        Me.frameCategories.Controls.Add(Me.labServiceCatPrompt1)
        Me.frameCategories.Controls.Add(Me.labServiceCatPrompt0)
        Me.frameCategories.Controls.Add(Me.Label37)
        Me.frameCategories.Enabled = False
        Me.frameCategories.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameCategories.Location = New System.Drawing.Point(3, 3)
        Me.frameCategories.Name = "frameCategories"
        Me.frameCategories.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameCategories.Size = New System.Drawing.Size(666, 506)
        Me.frameCategories.TabIndex = 78
        Me.frameCategories.TabStop = False
        Me.frameCategories.Text = "frameCategories"
        '
        '_txtChassisCat_1
        '
        Me._txtChassisCat_1.AcceptsReturn = True
        Me._txtChassisCat_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtChassisCat_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtChassisCat_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtChassisCat_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtChassisCat_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChassisCat.SetIndex(Me._txtChassisCat_1, CType(1, Short))
        Me._txtChassisCat_1.Location = New System.Drawing.Point(27, 360)
        Me._txtChassisCat_1.MaxLength = 6
        Me._txtChassisCat_1.Name = "_txtChassisCat_1"
        Me._txtChassisCat_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtChassisCat_1.Size = New System.Drawing.Size(245, 14)
        Me._txtChassisCat_1.TabIndex = 26
        '
        '_txtChassisCat_0
        '
        Me._txtChassisCat_0.AcceptsReturn = True
        Me._txtChassisCat_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtChassisCat_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtChassisCat_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtChassisCat_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtChassisCat_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChassisCat.SetIndex(Me._txtChassisCat_0, CType(0, Short))
        Me._txtChassisCat_0.Location = New System.Drawing.Point(27, 308)
        Me._txtChassisCat_0.MaxLength = 6
        Me._txtChassisCat_0.Name = "_txtChassisCat_0"
        Me._txtChassisCat_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtChassisCat_0.Size = New System.Drawing.Size(245, 14)
        Me._txtChassisCat_0.TabIndex = 25
        '
        '_txtServiceCategory_1
        '
        Me._txtServiceCategory_1.AcceptsReturn = True
        Me._txtServiceCategory_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtServiceCategory_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtServiceCategory_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtServiceCategory_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtServiceCategory_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServiceCategory.SetIndex(Me._txtServiceCategory_1, CType(1, Short))
        Me._txtServiceCategory_1.Location = New System.Drawing.Point(27, 156)
        Me._txtServiceCategory_1.MaxLength = 6
        Me._txtServiceCategory_1.Name = "_txtServiceCategory_1"
        Me._txtServiceCategory_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtServiceCategory_1.Size = New System.Drawing.Size(245, 14)
        Me._txtServiceCategory_1.TabIndex = 24
        '
        '_txtServiceCategory_0
        '
        Me._txtServiceCategory_0.AcceptsReturn = True
        Me._txtServiceCategory_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtServiceCategory_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtServiceCategory_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtServiceCategory_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtServiceCategory_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServiceCategory.SetIndex(Me._txtServiceCategory_0, CType(0, Short))
        Me._txtServiceCategory_0.Location = New System.Drawing.Point(27, 101)
        Me._txtServiceCategory_0.MaxLength = 6
        Me._txtServiceCategory_0.Name = "_txtServiceCategory_0"
        Me._txtServiceCategory_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtServiceCategory_0.Size = New System.Drawing.Size(245, 14)
        Me._txtServiceCategory_0.TabIndex = 23
        '
        'Line1
        '
        Me.Line1.BackColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.Line1.Location = New System.Drawing.Point(24, 191)
        Me.Line1.Name = "Line1"
        Me.Line1.Size = New System.Drawing.Size(104, 1)
        Me.Line1.TabIndex = 27
        '
        'labChassisCatPrompt1
        '
        Me.labChassisCatPrompt1.BackColor = System.Drawing.Color.Transparent
        Me.labChassisCatPrompt1.Cursor = System.Windows.Forms.Cursors.Default
        Me.labChassisCatPrompt1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labChassisCatPrompt1.Location = New System.Drawing.Point(24, 340)
        Me.labChassisCatPrompt1.Name = "labChassisCatPrompt1"
        Me.labChassisCatPrompt1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labChassisCatPrompt1.Size = New System.Drawing.Size(243, 17)
        Me.labChassisCatPrompt1.TabIndex = 86
        Me.labChassisCatPrompt1.Text = "Cat2"
        '
        'labChassisCatPrompt0
        '
        Me.labChassisCatPrompt0.BackColor = System.Drawing.Color.Transparent
        Me.labChassisCatPrompt0.Cursor = System.Windows.Forms.Cursors.Default
        Me.labChassisCatPrompt0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labChassisCatPrompt0.Location = New System.Drawing.Point(24, 288)
        Me.labChassisCatPrompt0.Name = "labChassisCatPrompt0"
        Me.labChassisCatPrompt0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labChassisCatPrompt0.Size = New System.Drawing.Size(243, 17)
        Me.labChassisCatPrompt0.TabIndex = 85
        Me.labChassisCatPrompt0.Text = "Cat1"
        '
        'LabHelpChassis
        '
        Me.LabHelpChassis.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.LabHelpChassis.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHelpChassis.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHelpChassis.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHelpChassis.Location = New System.Drawing.Point(296, 221)
        Me.LabHelpChassis.Name = "LabHelpChassis"
        Me.LabHelpChassis.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHelpChassis.Size = New System.Drawing.Size(292, 62)
        Me.LabHelpChassis.TabIndex = 84
        Me.LabHelpChassis.Text = "LabHelpChassis"
        '
        'LabChassisCats
        '
        Me.LabChassisCats.BackColor = System.Drawing.Color.Transparent
        Me.LabChassisCats.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabChassisCats.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabChassisCats.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabChassisCats.Location = New System.Drawing.Point(24, 221)
        Me.LabChassisCats.Name = "LabChassisCats"
        Me.LabChassisCats.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabChassisCats.Size = New System.Drawing.Size(174, 49)
        Me.LabChassisCats.TabIndex = 83
        Me.LabChassisCats.Text = "Chassis Category Definitions as set up in the  Retail Manager Stock Table."
        '
        'LabHelpServiceCat
        '
        Me.LabHelpServiceCat.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.LabHelpServiceCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHelpServiceCat.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHelpServiceCat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHelpServiceCat.Location = New System.Drawing.Point(296, 32)
        Me.LabHelpServiceCat.Name = "LabHelpServiceCat"
        Me.LabHelpServiceCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHelpServiceCat.Size = New System.Drawing.Size(274, 65)
        Me.LabHelpServiceCat.TabIndex = 82
        Me.LabHelpServiceCat.Text = "LabHelpServiceCat"
        '
        'labServiceCatPrompt1
        '
        Me.labServiceCatPrompt1.BackColor = System.Drawing.Color.Transparent
        Me.labServiceCatPrompt1.Cursor = System.Windows.Forms.Cursors.Default
        Me.labServiceCatPrompt1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labServiceCatPrompt1.Location = New System.Drawing.Point(24, 136)
        Me.labServiceCatPrompt1.Name = "labServiceCatPrompt1"
        Me.labServiceCatPrompt1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labServiceCatPrompt1.Size = New System.Drawing.Size(33, 17)
        Me.labServiceCatPrompt1.TabIndex = 81
        Me.labServiceCatPrompt1.Text = "Cat2"
        '
        'labServiceCatPrompt0
        '
        Me.labServiceCatPrompt0.BackColor = System.Drawing.Color.Transparent
        Me.labServiceCatPrompt0.Cursor = System.Windows.Forms.Cursors.Default
        Me.labServiceCatPrompt0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labServiceCatPrompt0.Location = New System.Drawing.Point(24, 81)
        Me.labServiceCatPrompt0.Name = "labServiceCatPrompt0"
        Me.labServiceCatPrompt0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labServiceCatPrompt0.Size = New System.Drawing.Size(243, 17)
        Me.labServiceCatPrompt0.TabIndex = 80
        Me.labServiceCatPrompt0.Text = "Cat1"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(24, 32)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(174, 49)
        Me.Label37.TabIndex = 79
        Me.Label37.Text = "Service Charge Category Definitions as set up in the  Retail Host Stock Table."
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.BackColor = System.Drawing.Color.WhiteSmoke
        Me._SSTab1_TabPage1.Controls.Add(Me.frameLabour)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(677, 524)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "GST and Labour Rates"
        '
        'frameLabour
        '
        Me.frameLabour.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameLabour.Controls.Add(Me.Label35)
        Me.frameLabour.Controls.Add(Me.Label36)
        Me.frameLabour.Controls.Add(Me.TabControlLabourRates)
        Me.frameLabour.Controls.Add(Me.panelOldLabourRates)
        Me.frameLabour.Controls.Add(Me.chkNoEnforceMinCharge)
        Me.frameLabour.Controls.Add(Me._txtLabourRates_4)
        Me.frameLabour.Controls.Add(Me._txtLabourRates_3)
        Me.frameLabour.Controls.Add(Me.txtGSTPercentage)
        Me.frameLabour.Controls.Add(Me.Label26)
        Me.frameLabour.Controls.Add(Me.Label22)
        Me.frameLabour.Controls.Add(Me.Label12)
        Me.frameLabour.Controls.Add(Me.Label11)
        Me.frameLabour.Controls.Add(Me.Label21)
        Me.frameLabour.Controls.Add(Me.Label39)
        Me.frameLabour.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameLabour.Location = New System.Drawing.Point(3, 3)
        Me.frameLabour.Name = "frameLabour"
        Me.frameLabour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameLabour.Size = New System.Drawing.Size(671, 518)
        Me.frameLabour.TabIndex = 51
        Me.frameLabour.TabStop = False
        Me.frameLabour.Text = " frameLabour"
        '
        'Label35
        '
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(253, 84)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(164, 13)
        Me.Label35.TabIndex = 96
        Me.Label35.Text = "(Not available at Create Time..)"
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(24, 84)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(276, 17)
        Me.Label36.TabIndex = 77
        Me.Label36.Text = "Labour Hourly Rates - Retail barcodes "
        '
        'TabControlLabourRates
        '
        Me.TabControlLabourRates.Controls.Add(Me.TabPageWorkshop)
        Me.TabControlLabourRates.Controls.Add(Me.TabPageOnSite)
        Me.TabControlLabourRates.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlLabourRates.Location = New System.Drawing.Point(20, 152)
        Me.TabControlLabourRates.Name = "TabControlLabourRates"
        Me.TabControlLabourRates.SelectedIndex = 0
        Me.TabControlLabourRates.Size = New System.Drawing.Size(397, 288)
        Me.TabControlLabourRates.TabIndex = 76
        '
        'TabPageWorkshop
        '
        Me.TabPageWorkshop.BackColor = System.Drawing.Color.White
        Me.TabPageWorkshop.Controls.Add(Me.panelHourlyRateDefsWorkshop)
        Me.TabPageWorkshop.Location = New System.Drawing.Point(4, 22)
        Me.TabPageWorkshop.Name = "TabPageWorkshop"
        Me.TabPageWorkshop.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageWorkshop.Size = New System.Drawing.Size(389, 262)
        Me.TabPageWorkshop.TabIndex = 0
        Me.TabPageWorkshop.Text = "Labour- Workshop"
        '
        'panelHourlyRateDefsWorkshop
        '
        Me.panelHourlyRateDefsWorkshop.BackColor = System.Drawing.Color.LightYellow
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.labP3Rate)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.labP3Description)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.btnP3Browse)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.txtLabourP3Barcode)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.labP2Rate)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.labP2Description)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.btnP2Browse)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.txtLabourP2Barcode)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.labP1Rate)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.labP1Description)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.btnP1Browse)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.txtLabourP1Barcode)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.Label43)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.Label42)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.Label41)
        Me.panelHourlyRateDefsWorkshop.Controls.Add(Me.Label10)
        Me.panelHourlyRateDefsWorkshop.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelHourlyRateDefsWorkshop.Location = New System.Drawing.Point(3, 3)
        Me.panelHourlyRateDefsWorkshop.Name = "panelHourlyRateDefsWorkshop"
        Me.panelHourlyRateDefsWorkshop.Size = New System.Drawing.Size(383, 253)
        Me.panelHourlyRateDefsWorkshop.TabIndex = 15
        '
        'labP3Rate
        '
        Me.labP3Rate.Location = New System.Drawing.Point(317, 211)
        Me.labP3Rate.Name = "labP3Rate"
        Me.labP3Rate.Size = New System.Drawing.Size(51, 12)
        Me.labP3Rate.TabIndex = 91
        Me.labP3Rate.Text = "labP3Rate"
        '
        'labP3Description
        '
        Me.labP3Description.Location = New System.Drawing.Point(101, 211)
        Me.labP3Description.Name = "labP3Description"
        Me.labP3Description.Size = New System.Drawing.Size(192, 13)
        Me.labP3Description.TabIndex = 90
        Me.labP3Description.Text = "labP3Description"
        '
        'txtLabourP3Barcode
        '
        Me.txtLabourP3Barcode.BackColor = System.Drawing.Color.Lavender
        Me.txtLabourP3Barcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabourP3Barcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabourP3Barcode.Location = New System.Drawing.Point(137, 188)
        Me.txtLabourP3Barcode.Name = "txtLabourP3Barcode"
        Me.txtLabourP3Barcode.Size = New System.Drawing.Size(126, 20)
        Me.txtLabourP3Barcode.TabIndex = 83
        Me.txtLabourP3Barcode.Text = "txtLabourP3Barcode"
        '
        'labP2Rate
        '
        Me.labP2Rate.Location = New System.Drawing.Point(317, 152)
        Me.labP2Rate.Name = "labP2Rate"
        Me.labP2Rate.Size = New System.Drawing.Size(51, 12)
        Me.labP2Rate.TabIndex = 87
        Me.labP2Rate.Text = "labP2Rate"
        '
        'labP2Description
        '
        Me.labP2Description.Location = New System.Drawing.Point(101, 152)
        Me.labP2Description.Name = "labP2Description"
        Me.labP2Description.Size = New System.Drawing.Size(192, 13)
        Me.labP2Description.TabIndex = 86
        Me.labP2Description.Text = "labP2Description"
        '
        'txtLabourP2Barcode
        '
        Me.txtLabourP2Barcode.BackColor = System.Drawing.Color.Lavender
        Me.txtLabourP2Barcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabourP2Barcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabourP2Barcode.Location = New System.Drawing.Point(137, 128)
        Me.txtLabourP2Barcode.Name = "txtLabourP2Barcode"
        Me.txtLabourP2Barcode.Size = New System.Drawing.Size(126, 20)
        Me.txtLabourP2Barcode.TabIndex = 81
        Me.txtLabourP2Barcode.Text = "txtLabourP2Barcode"
        '
        'labP1Rate
        '
        Me.labP1Rate.Location = New System.Drawing.Point(317, 98)
        Me.labP1Rate.Name = "labP1Rate"
        Me.labP1Rate.Size = New System.Drawing.Size(51, 12)
        Me.labP1Rate.TabIndex = 83
        Me.labP1Rate.Text = "labP1Rate"
        '
        'labP1Description
        '
        Me.labP1Description.Location = New System.Drawing.Point(101, 98)
        Me.labP1Description.Name = "labP1Description"
        Me.labP1Description.Size = New System.Drawing.Size(192, 13)
        Me.labP1Description.TabIndex = 82
        Me.labP1Description.Text = "labP1Description"
        '
        'txtLabourP1Barcode
        '
        Me.txtLabourP1Barcode.BackColor = System.Drawing.Color.Lavender
        Me.txtLabourP1Barcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabourP1Barcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabourP1Barcode.Location = New System.Drawing.Point(137, 76)
        Me.txtLabourP1Barcode.Name = "txtLabourP1Barcode"
        Me.txtLabourP1Barcode.Size = New System.Drawing.Size(126, 20)
        Me.txtLabourP1Barcode.TabIndex = 79
        Me.txtLabourP1Barcode.Text = "txtLabourP1Barcode"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.Transparent
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(10, 189)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(120, 17)
        Me.Label43.TabIndex = 78
        Me.Label43.Text = "Priority 3- Barcode:"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(10, 133)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(120, 15)
        Me.Label42.TabIndex = 77
        Me.Label42.Text = "Priority 2-  barcode:"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(10, 76)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(120, 14)
        Me.Label41.TabIndex = 76
        Me.Label41.Text = "Priority 1-  Barcode: "
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(10, 19)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(238, 34)
        Me.Label10.TabIndex = 57
        Me.Label10.Text = "Workshop- Labour Hourly Rates - Browse and save Retail barcodes.."
        '
        'TabPageOnSite
        '
        Me.TabPageOnSite.BackColor = System.Drawing.Color.White
        Me.TabPageOnSite.Controls.Add(Me.panelHourlyRateDefsOnSite)
        Me.TabPageOnSite.Location = New System.Drawing.Point(4, 22)
        Me.TabPageOnSite.Name = "TabPageOnSite"
        Me.TabPageOnSite.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageOnSite.Size = New System.Drawing.Size(389, 262)
        Me.TabPageOnSite.TabIndex = 1
        Me.TabPageOnSite.Text = "Labour- OnSite"
        '
        'panelHourlyRateDefsOnSite
        '
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.txtLabourOnSiteP3Barcode)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.labOnSiteP3Rate)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.Label52)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.btnOnSiteP3Browse)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.labOnSiteP3Description)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.txtLabourOnSiteP2Barcode)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.labOnSiteP2Rate)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.Label49)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.btnOnSiteP2Browse)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.labOnSiteP2Description)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.Label44)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.txtLabourOnSiteP1Barcode)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.labOnSiteP1Rate)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.Label31)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.btnOnSiteP1Browse)
        Me.panelHourlyRateDefsOnSite.Controls.Add(Me.labOnSiteP1Description)
        Me.panelHourlyRateDefsOnSite.Location = New System.Drawing.Point(3, 3)
        Me.panelHourlyRateDefsOnSite.Name = "panelHourlyRateDefsOnSite"
        Me.panelHourlyRateDefsOnSite.Size = New System.Drawing.Size(380, 253)
        Me.panelHourlyRateDefsOnSite.TabIndex = 0
        '
        'txtLabourOnSiteP3Barcode
        '
        Me.txtLabourOnSiteP3Barcode.BackColor = System.Drawing.Color.Lavender
        Me.txtLabourOnSiteP3Barcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabourOnSiteP3Barcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabourOnSiteP3Barcode.Location = New System.Drawing.Point(137, 189)
        Me.txtLabourOnSiteP3Barcode.Name = "txtLabourOnSiteP3Barcode"
        Me.txtLabourOnSiteP3Barcode.Size = New System.Drawing.Size(126, 20)
        Me.txtLabourOnSiteP3Barcode.TabIndex = 103
        Me.txtLabourOnSiteP3Barcode.Text = "txtLabourOnSiteP3Barcode"
        '
        'labOnSiteP3Rate
        '
        Me.labOnSiteP3Rate.Location = New System.Drawing.Point(319, 212)
        Me.labOnSiteP3Rate.Name = "labOnSiteP3Rate"
        Me.labOnSiteP3Rate.Size = New System.Drawing.Size(51, 15)
        Me.labOnSiteP3Rate.TabIndex = 106
        Me.labOnSiteP3Rate.Text = "labOnSiteP3Rate"
        '
        'Label52
        '
        Me.Label52.BackColor = System.Drawing.Color.Transparent
        Me.Label52.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label52.Location = New System.Drawing.Point(10, 189)
        Me.Label52.Name = "Label52"
        Me.Label52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label52.Size = New System.Drawing.Size(105, 34)
        Me.Label52.TabIndex = 102
        Me.Label52.Text = "OnSite Priority 3- B'code:"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'labOnSiteP3Description
        '
        Me.labOnSiteP3Description.Location = New System.Drawing.Point(121, 212)
        Me.labOnSiteP3Description.Name = "labOnSiteP3Description"
        Me.labOnSiteP3Description.Size = New System.Drawing.Size(192, 13)
        Me.labOnSiteP3Description.TabIndex = 105
        Me.labOnSiteP3Description.Text = "labOnSiteP3Description"
        '
        'txtLabourOnSiteP2Barcode
        '
        Me.txtLabourOnSiteP2Barcode.BackColor = System.Drawing.Color.Lavender
        Me.txtLabourOnSiteP2Barcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabourOnSiteP2Barcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabourOnSiteP2Barcode.Location = New System.Drawing.Point(137, 133)
        Me.txtLabourOnSiteP2Barcode.Name = "txtLabourOnSiteP2Barcode"
        Me.txtLabourOnSiteP2Barcode.Size = New System.Drawing.Size(126, 20)
        Me.txtLabourOnSiteP2Barcode.TabIndex = 98
        Me.txtLabourOnSiteP2Barcode.Text = "txtLabourOnSiteP2Barcode"
        '
        'labOnSiteP2Rate
        '
        Me.labOnSiteP2Rate.Location = New System.Drawing.Point(319, 156)
        Me.labOnSiteP2Rate.Name = "labOnSiteP2Rate"
        Me.labOnSiteP2Rate.Size = New System.Drawing.Size(51, 15)
        Me.labOnSiteP2Rate.TabIndex = 101
        Me.labOnSiteP2Rate.Text = "labOnSiteP2Rate"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(10, 133)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(105, 34)
        Me.Label49.TabIndex = 97
        Me.Label49.Text = "OnSite Priority 2- B'code:"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'labOnSiteP2Description
        '
        Me.labOnSiteP2Description.Location = New System.Drawing.Point(121, 156)
        Me.labOnSiteP2Description.Name = "labOnSiteP2Description"
        Me.labOnSiteP2Description.Size = New System.Drawing.Size(192, 13)
        Me.labOnSiteP2Description.TabIndex = 100
        Me.labOnSiteP2Description.Text = "labOnSiteP2Description"
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(10, 19)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(238, 34)
        Me.Label44.TabIndex = 96
        Me.Label44.Text = "On-Site Jobs- Labour Hourly Rates - Browse and save Retail barcodes.."
        '
        'txtLabourOnSiteP1Barcode
        '
        Me.txtLabourOnSiteP1Barcode.BackColor = System.Drawing.Color.Lavender
        Me.txtLabourOnSiteP1Barcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLabourOnSiteP1Barcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabourOnSiteP1Barcode.Location = New System.Drawing.Point(137, 76)
        Me.txtLabourOnSiteP1Barcode.Name = "txtLabourOnSiteP1Barcode"
        Me.txtLabourOnSiteP1Barcode.Size = New System.Drawing.Size(126, 20)
        Me.txtLabourOnSiteP1Barcode.TabIndex = 85
        Me.txtLabourOnSiteP1Barcode.Text = "txtLabourOnSiteP1Barcode"
        '
        'labOnSiteP1Rate
        '
        Me.labOnSiteP1Rate.Location = New System.Drawing.Point(319, 99)
        Me.labOnSiteP1Rate.Name = "labOnSiteP1Rate"
        Me.labOnSiteP1Rate.Size = New System.Drawing.Size(51, 15)
        Me.labOnSiteP1Rate.TabIndex = 95
        Me.labOnSiteP1Rate.Text = "labOnSiteP1Rate"
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(10, 76)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(105, 34)
        Me.Label31.TabIndex = 80
        Me.Label31.Text = "OnSite Priority 1- B'code:"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'labOnSiteP1Description
        '
        Me.labOnSiteP1Description.Location = New System.Drawing.Point(121, 99)
        Me.labOnSiteP1Description.Name = "labOnSiteP1Description"
        Me.labOnSiteP1Description.Size = New System.Drawing.Size(192, 13)
        Me.labOnSiteP1Description.TabIndex = 94
        Me.labOnSiteP1Description.Text = "labOnSiteP1Description"
        '
        'panelOldLabourRates
        '
        Me.panelOldLabourRates.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.panelOldLabourRates.Controls.Add(Me.Label40)
        Me.panelOldLabourRates.Controls.Add(Me.Label38)
        Me.panelOldLabourRates.Controls.Add(Me._txtLabourRates_0)
        Me.panelOldLabourRates.Controls.Add(Me._txtPriority_2)
        Me.panelOldLabourRates.Controls.Add(Me.Label14)
        Me.panelOldLabourRates.Controls.Add(Me._txtPriority_1)
        Me.panelOldLabourRates.Controls.Add(Me._txtLabourRates_2)
        Me.panelOldLabourRates.Controls.Add(Me._txtPriority_0)
        Me.panelOldLabourRates.Controls.Add(Me._txtLabourRates_1)
        Me.panelOldLabourRates.Controls.Add(Me.Label32)
        Me.panelOldLabourRates.Controls.Add(Me.Label34)
        Me.panelOldLabourRates.Controls.Add(Me.Label15)
        Me.panelOldLabourRates.Controls.Add(Me.Label33)
        Me.panelOldLabourRates.Controls.Add(Me.Label16)
        Me.panelOldLabourRates.Location = New System.Drawing.Point(434, 154)
        Me.panelOldLabourRates.Name = "panelOldLabourRates"
        Me.panelOldLabourRates.Size = New System.Drawing.Size(225, 286)
        Me.panelOldLabourRates.TabIndex = 75
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(12, 67)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(133, 33)
        Me.Label40.TabIndex = 75
        Me.Label40.Text = "Labour Hourly Rates  $         Priority Descriptors"
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(10, 12)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(209, 55)
        Me.Label38.TabIndex = 74
        Me.Label38.Text = "Old Hourly Rate defs..  These are deprecated, and can't now be changed..  Use the" & _
    " panel to left to set up Retail Barcodes for Labour Rates."
        '
        '_txtLabourRates_0
        '
        Me._txtLabourRates_0.AcceptsReturn = True
        Me._txtLabourRates_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtLabourRates_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtLabourRates_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtLabourRates_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtLabourRates_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLabourRates.SetIndex(Me._txtLabourRates_0, CType(0, Short))
        Me._txtLabourRates_0.Location = New System.Drawing.Point(80, 124)
        Me._txtLabourRates_0.MaxLength = 7
        Me._txtLabourRates_0.Name = "_txtLabourRates_0"
        Me._txtLabourRates_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtLabourRates_0.Size = New System.Drawing.Size(76, 14)
        Me._txtLabourRates_0.TabIndex = 15
        '
        '_txtPriority_2
        '
        Me._txtPriority_2.AcceptsReturn = True
        Me._txtPriority_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtPriority_2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtPriority_2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtPriority_2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtPriority_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPriority.SetIndex(Me._txtPriority_2, CType(2, Short))
        Me._txtPriority_2.Location = New System.Drawing.Point(80, 253)
        Me._txtPriority_2.MaxLength = 24
        Me._txtPriority_2.Name = "_txtPriority_2"
        Me._txtPriority_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtPriority_2.Size = New System.Drawing.Size(139, 14)
        Me._txtPriority_2.TabIndex = 20
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(9, 113)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(68, 37)
        Me.Label14.TabIndex = 56
        Me.Label14.Text = "Priority 1.    $"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_txtPriority_1
        '
        Me._txtPriority_1.AcceptsReturn = True
        Me._txtPriority_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtPriority_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtPriority_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtPriority_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtPriority_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPriority.SetIndex(Me._txtPriority_1, CType(1, Short))
        Me._txtPriority_1.Location = New System.Drawing.Point(83, 200)
        Me._txtPriority_1.MaxLength = 24
        Me._txtPriority_1.Name = "_txtPriority_1"
        Me._txtPriority_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtPriority_1.Size = New System.Drawing.Size(139, 14)
        Me._txtPriority_1.TabIndex = 18
        '
        '_txtLabourRates_2
        '
        Me._txtLabourRates_2.AcceptsReturn = True
        Me._txtLabourRates_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtLabourRates_2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtLabourRates_2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtLabourRates_2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtLabourRates_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLabourRates.SetIndex(Me._txtLabourRates_2, CType(2, Short))
        Me._txtLabourRates_2.Location = New System.Drawing.Point(75, 233)
        Me._txtLabourRates_2.MaxLength = 7
        Me._txtLabourRates_2.Name = "_txtLabourRates_2"
        Me._txtLabourRates_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtLabourRates_2.Size = New System.Drawing.Size(65, 14)
        Me._txtLabourRates_2.TabIndex = 19
        '
        '_txtPriority_0
        '
        Me._txtPriority_0.AcceptsReturn = True
        Me._txtPriority_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtPriority_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtPriority_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtPriority_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtPriority_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPriority.SetIndex(Me._txtPriority_0, CType(0, Short))
        Me._txtPriority_0.Location = New System.Drawing.Point(80, 144)
        Me._txtPriority_0.MaxLength = 24
        Me._txtPriority_0.Name = "_txtPriority_0"
        Me._txtPriority_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtPriority_0.Size = New System.Drawing.Size(139, 14)
        Me._txtPriority_0.TabIndex = 16
        '
        '_txtLabourRates_1
        '
        Me._txtLabourRates_1.AcceptsReturn = True
        Me._txtLabourRates_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtLabourRates_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtLabourRates_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtLabourRates_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtLabourRates_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLabourRates.SetIndex(Me._txtLabourRates_1, CType(1, Short))
        Me._txtLabourRates_1.Location = New System.Drawing.Point(80, 182)
        Me._txtLabourRates_1.MaxLength = 7
        Me._txtLabourRates_1.Name = "_txtLabourRates_1"
        Me._txtLabourRates_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtLabourRates_1.Size = New System.Drawing.Size(65, 14)
        Me._txtLabourRates_1.TabIndex = 17
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(77, 108)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(76, 18)
        Me.Label32.TabIndex = 69
        Me.Label32.Text = "Hourly Rate"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(80, 217)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(73, 17)
        Me.Label34.TabIndex = 71
        Me.Label34.Text = "Hourly Rate"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(12, 169)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(65, 33)
        Me.Label15.TabIndex = 55
        Me.Label15.Text = "Priority 2.   $"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(80, 166)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(73, 13)
        Me.Label33.TabIndex = 70
        Me.Label33.Text = "Hourly Rate"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(12, 218)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(65, 29)
        Me.Label16.TabIndex = 54
        Me.Label16.Text = "Priority 3.   $"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkNoEnforceMinCharge
        '
        Me.chkNoEnforceMinCharge.BackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.chkNoEnforceMinCharge.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNoEnforceMinCharge.Location = New System.Drawing.Point(166, 471)
        Me.chkNoEnforceMinCharge.Name = "chkNoEnforceMinCharge"
        Me.chkNoEnforceMinCharge.Size = New System.Drawing.Size(150, 34)
        Me.chkNoEnforceMinCharge.TabIndex = 74
        Me.chkNoEnforceMinCharge.Text = "Do Not Enforce (Min will be used for printing only)"
        Me.chkNoEnforceMinCharge.UseVisualStyleBackColor = False
        '
        '_txtLabourRates_4
        '
        Me._txtLabourRates_4.AcceptsReturn = True
        Me._txtLabourRates_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtLabourRates_4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtLabourRates_4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtLabourRates_4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtLabourRates_4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLabourRates.SetIndex(Me._txtLabourRates_4, CType(4, Short))
        Me._txtLabourRates_4.Location = New System.Drawing.Point(504, 481)
        Me._txtLabourRates_4.MaxLength = 7
        Me._txtLabourRates_4.Name = "_txtLabourRates_4"
        Me._txtLabourRates_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtLabourRates_4.Size = New System.Drawing.Size(65, 14)
        Me._txtLabourRates_4.TabIndex = 17
        '
        '_txtLabourRates_3
        '
        Me._txtLabourRates_3.AcceptsReturn = True
        Me._txtLabourRates_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtLabourRates_3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtLabourRates_3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtLabourRates_3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtLabourRates_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLabourRates.SetIndex(Me._txtLabourRates_3, CType(3, Short))
        Me._txtLabourRates_3.Location = New System.Drawing.Point(95, 481)
        Me._txtLabourRates_3.MaxLength = 7
        Me._txtLabourRates_3.Name = "_txtLabourRates_3"
        Me._txtLabourRates_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtLabourRates_3.Size = New System.Drawing.Size(65, 14)
        Me._txtLabourRates_3.TabIndex = 16
        '
        'txtGSTPercentage
        '
        Me.txtGSTPercentage.AcceptsReturn = True
        Me.txtGSTPercentage.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtGSTPercentage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtGSTPercentage.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTPercentage.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTPercentage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGSTPercentage.Location = New System.Drawing.Point(134, 54)
        Me.txtGSTPercentage.MaxLength = 6
        Me.txtGSTPercentage.Name = "txtGSTPercentage"
        Me.txtGSTPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTPercentage.Size = New System.Drawing.Size(49, 14)
        Me.txtGSTPercentage.TabIndex = 14
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(15, 23)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(145, 17)
        Me.Label26.TabIndex = 67
        Me.Label26.Text = "GST and Labour Rates "
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(361, 472)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(137, 33)
        Me.Label22.TabIndex = 64
        Me.Label22.Text = "Service Notification Cost Limit"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(361, 19)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(219, 49)
        Me.Label12.TabIndex = 58
        Me.Label12.Text = "These Items used for Job Costing, && for Printing Terms and Conditions and Receip" & _
    "ts"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(27, 471)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(64, 33)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Minimum Charge:  $"
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(24, 54)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(104, 14)
        Me.Label21.TabIndex = 52
        Me.Label21.Text = "GST Percentage"
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(24, 103)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(383, 46)
        Me.Label39.TabIndex = 75
        Me.Label39.Text = resources.GetString("Label39.Text")
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.BackColor = System.Drawing.Color.WhiteSmoke
        Me._SSTab1_TabPage3.Controls.Add(Me.frameTexts)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(677, 524)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "DocumentTexts"
        '
        'frameTexts
        '
        Me.frameTexts.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameTexts.Controls.Add(Me.Label17)
        Me.frameTexts.Controls.Add(Me.cmdRestoreTerms)
        Me.frameTexts.Controls.Add(Me.cmdBarcodeFont)
        Me.frameTexts.Controls.Add(Me.txtBarcodeFontSize)
        Me.frameTexts.Controls.Add(Me.txtBarcodeFontName)
        Me.frameTexts.Controls.Add(Me._txtUserTexts_0)
        Me.frameTexts.Controls.Add(Me._txtUserTexts_1)
        Me.frameTexts.Controls.Add(Me._txtUserTexts_2)
        Me.frameTexts.Controls.Add(Me._txtUserTexts_3)
        Me.frameTexts.Controls.Add(Me.LabHelpBarcode)
        Me.frameTexts.Controls.Add(Me.Label47)
        Me.frameTexts.Controls.Add(Me.Label46)
        Me.frameTexts.Controls.Add(Me.Label1)
        Me.frameTexts.Controls.Add(Me.Label23)
        Me.frameTexts.Controls.Add(Me.Label24)
        Me.frameTexts.Controls.Add(Me.Label25)
        Me.frameTexts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameTexts.Location = New System.Drawing.Point(3, 3)
        Me.frameTexts.Name = "frameTexts"
        Me.frameTexts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameTexts.Size = New System.Drawing.Size(666, 513)
        Me.frameTexts.TabIndex = 74
        Me.frameTexts.TabStop = False
        Me.frameTexts.Text = "frameTexts"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(24, 368)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(230, 17)
        Me.Label17.TabIndex = 102
        Me.Label17.Text = "Service Charges Info text"
        '
        'cmdBarcodeFont
        '
        Me.cmdBarcodeFont.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBarcodeFont.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBarcodeFont.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBarcodeFont.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBarcodeFont.Location = New System.Drawing.Point(497, 364)
        Me.cmdBarcodeFont.Name = "cmdBarcodeFont"
        Me.cmdBarcodeFont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBarcodeFont.Size = New System.Drawing.Size(58, 19)
        Me.cmdBarcodeFont.TabIndex = 32
        Me.cmdBarcodeFont.Text = "Browse"
        Me.cmdBarcodeFont.UseVisualStyleBackColor = False
        '
        'txtBarcodeFontSize
        '
        Me.txtBarcodeFontSize.AcceptsReturn = True
        Me.txtBarcodeFontSize.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtBarcodeFontSize.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBarcodeFontSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBarcodeFontSize.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcodeFontSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBarcodeFontSize.Location = New System.Drawing.Point(525, 385)
        Me.txtBarcodeFontSize.MaxLength = 0
        Me.txtBarcodeFontSize.Name = "txtBarcodeFontSize"
        Me.txtBarcodeFontSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBarcodeFontSize.Size = New System.Drawing.Size(47, 14)
        Me.txtBarcodeFontSize.TabIndex = 33
        '
        'txtBarcodeFontName
        '
        Me.txtBarcodeFontName.AcceptsReturn = True
        Me.txtBarcodeFontName.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtBarcodeFontName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBarcodeFontName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBarcodeFontName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcodeFontName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBarcodeFontName.Location = New System.Drawing.Point(335, 385)
        Me.txtBarcodeFontName.MaxLength = 50
        Me.txtBarcodeFontName.Name = "txtBarcodeFontName"
        Me.txtBarcodeFontName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBarcodeFontName.Size = New System.Drawing.Size(220, 14)
        Me.txtBarcodeFontName.TabIndex = 31
        '
        '_txtUserTexts_0
        '
        Me._txtUserTexts_0.AcceptsReturn = True
        Me._txtUserTexts_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me._txtUserTexts_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtUserTexts_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtUserTexts_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtUserTexts_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserTexts.SetIndex(Me._txtUserTexts_0, CType(0, Short))
        Me._txtUserTexts_0.Location = New System.Drawing.Point(24, 50)
        Me._txtUserTexts_0.MaxLength = 2400
        Me._txtUserTexts_0.Multiline = True
        Me._txtUserTexts_0.Name = "_txtUserTexts_0"
        Me._txtUserTexts_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtUserTexts_0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me._txtUserTexts_0.Size = New System.Drawing.Size(560, 169)
        Me._txtUserTexts_0.TabIndex = 27
        '
        '_txtUserTexts_1
        '
        Me._txtUserTexts_1.AcceptsReturn = True
        Me._txtUserTexts_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me._txtUserTexts_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtUserTexts_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtUserTexts_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtUserTexts_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserTexts.SetIndex(Me._txtUserTexts_1, CType(1, Short))
        Me._txtUserTexts_1.Location = New System.Drawing.Point(24, 260)
        Me._txtUserTexts_1.MaxLength = 2000
        Me._txtUserTexts_1.Multiline = True
        Me._txtUserTexts_1.Name = "_txtUserTexts_1"
        Me._txtUserTexts_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtUserTexts_1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me._txtUserTexts_1.Size = New System.Drawing.Size(277, 82)
        Me._txtUserTexts_1.TabIndex = 28
        '
        '_txtUserTexts_2
        '
        Me._txtUserTexts_2.AcceptsReturn = True
        Me._txtUserTexts_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me._txtUserTexts_2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtUserTexts_2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtUserTexts_2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtUserTexts_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserTexts.SetIndex(Me._txtUserTexts_2, CType(2, Short))
        Me._txtUserTexts_2.Location = New System.Drawing.Point(332, 260)
        Me._txtUserTexts_2.MaxLength = 2000
        Me._txtUserTexts_2.Multiline = True
        Me._txtUserTexts_2.Name = "_txtUserTexts_2"
        Me._txtUserTexts_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtUserTexts_2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me._txtUserTexts_2.Size = New System.Drawing.Size(288, 65)
        Me._txtUserTexts_2.TabIndex = 29
        '
        '_txtUserTexts_3
        '
        Me._txtUserTexts_3.AcceptsReturn = True
        Me._txtUserTexts_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me._txtUserTexts_3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtUserTexts_3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtUserTexts_3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtUserTexts_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserTexts.SetIndex(Me._txtUserTexts_3, CType(3, Short))
        Me._txtUserTexts_3.Location = New System.Drawing.Point(24, 387)
        Me._txtUserTexts_3.MaxLength = 2000
        Me._txtUserTexts_3.Multiline = True
        Me._txtUserTexts_3.Name = "_txtUserTexts_3"
        Me._txtUserTexts_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtUserTexts_3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me._txtUserTexts_3.Size = New System.Drawing.Size(277, 94)
        Me._txtUserTexts_3.TabIndex = 30
        '
        'LabHelpBarcode
        '
        Me.LabHelpBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.LabHelpBarcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHelpBarcode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHelpBarcode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHelpBarcode.Location = New System.Drawing.Point(334, 409)
        Me.LabHelpBarcode.Name = "LabHelpBarcode"
        Me.LabHelpBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHelpBarcode.Size = New System.Drawing.Size(274, 47)
        Me.LabHelpBarcode.TabIndex = 92
        Me.LabHelpBarcode.Text = "LabHelpBarcode"
        '
        'Label47
        '
        Me.Label47.BackColor = System.Drawing.SystemColors.Control
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label47.Location = New System.Drawing.Point(561, 366)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(59, 17)
        Me.Label47.TabIndex = 90
        Me.Label47.Text = "Size (Pts)"
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(332, 366)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(73, 17)
        Me.Label46.TabIndex = 89
        Me.Label46.Text = "Font Name"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(332, 345)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(179, 17)
        Me.Label1.TabIndex = 88
        Me.Label1.Text = "Barcode Font (if available)"
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(24, 18)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(169, 33)
        Me.Label23.TabIndex = 77
        Me.Label23.Text = "Terms and Conditions (Service Agreement)"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(24, 244)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(230, 17)
        Me.Label24.TabIndex = 76
        Me.Label24.Text = "New-Job Docket Footnote"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(332, 244)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(229, 17)
        Me.Label25.TabIndex = 75
        Me.Label25.Text = "Delivery Docket Footnote"
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.BackColor = System.Drawing.Color.WhiteSmoke
        Me._SSTab1_TabPage4.Controls.Add(Me.frameBackupPath)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(677, 524)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "DB Backup Path"
        '
        'frameBackupPath
        '
        Me.frameBackupPath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameBackupPath.Controls.Add(Me.panelBackupPaths)
        Me.frameBackupPath.Controls.Add(Me.Label28)
        Me.frameBackupPath.Controls.Add(Me.LabHelpBackupPath)
        Me.frameBackupPath.Controls.Add(Me.Label27)
        Me.frameBackupPath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameBackupPath.Location = New System.Drawing.Point(3, 3)
        Me.frameBackupPath.Name = "frameBackupPath"
        Me.frameBackupPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameBackupPath.Size = New System.Drawing.Size(666, 513)
        Me.frameBackupPath.TabIndex = 93
        Me.frameBackupPath.TabStop = False
        Me.frameBackupPath.Text = "frameBackupPath"
        '
        'panelBackupPaths
        '
        Me.panelBackupPaths.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.panelBackupPaths.Controls.Add(Me.Label13)
        Me.panelBackupPaths.Controls.Add(Me.cmdBrowsePath)
        Me.panelBackupPaths.Controls.Add(Me._txtServerBackupFolder_1)
        Me.panelBackupPaths.Controls.Add(Me._txtServerBackupFolder_0)
        Me.panelBackupPaths.Controls.Add(Me.Label6)
        Me.panelBackupPaths.Location = New System.Drawing.Point(27, 142)
        Me.panelBackupPaths.Name = "panelBackupPaths"
        Me.panelBackupPaths.Size = New System.Drawing.Size(619, 173)
        Me.panelBackupPaths.TabIndex = 99
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(12, 107)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(571, 17)
        Me.Label13.TabIndex = 95
        Me.Label13.Text = "B.  SAME Backup Folder-   Server SHARE Path:  (eg  ""\\ServerName\Backup-Share\"" )" & _
    ""
        '
        'cmdBrowsePath
        '
        Me.cmdBrowsePath.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdBrowsePath.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBrowsePath.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBrowsePath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBrowsePath.Location = New System.Drawing.Point(542, 41)
        Me.cmdBrowsePath.Name = "cmdBrowsePath"
        Me.cmdBrowsePath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBrowsePath.Size = New System.Drawing.Size(57, 23)
        Me.cmdBrowsePath.TabIndex = 35
        Me.cmdBrowsePath.Text = "Browse"
        Me.cmdBrowsePath.UseVisualStyleBackColor = False
        '
        '_txtServerBackupFolder_1
        '
        Me._txtServerBackupFolder_1.AcceptsReturn = True
        Me._txtServerBackupFolder_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtServerBackupFolder_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtServerBackupFolder_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtServerBackupFolder_1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtServerBackupFolder_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServerBackupFolder.SetIndex(Me._txtServerBackupFolder_1, CType(1, Short))
        Me._txtServerBackupFolder_1.Location = New System.Drawing.Point(12, 129)
        Me._txtServerBackupFolder_1.MaxLength = 0
        Me._txtServerBackupFolder_1.Name = "_txtServerBackupFolder_1"
        Me._txtServerBackupFolder_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtServerBackupFolder_1.Size = New System.Drawing.Size(515, 14)
        Me._txtServerBackupFolder_1.TabIndex = 36
        '
        '_txtServerBackupFolder_0
        '
        Me._txtServerBackupFolder_0.AcceptsReturn = True
        Me._txtServerBackupFolder_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me._txtServerBackupFolder_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._txtServerBackupFolder_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._txtServerBackupFolder_0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._txtServerBackupFolder_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServerBackupFolder.SetIndex(Me._txtServerBackupFolder_0, CType(0, Short))
        Me._txtServerBackupFolder_0.Location = New System.Drawing.Point(15, 50)
        Me._txtServerBackupFolder_0.MaxLength = 0
        Me._txtServerBackupFolder_0.Name = "_txtServerBackupFolder_0"
        Me._txtServerBackupFolder_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._txtServerBackupFolder_0.Size = New System.Drawing.Size(512, 14)
        Me._txtServerBackupFolder_0.TabIndex = 34
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(508, 17)
        Me.Label6.TabIndex = 94
        Me.Label6.Text = "A.  Backup Folder-   Server Local Path:  (eg  ""c:\JobMatix-Backups\"" )"
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Indigo
        Me.Label28.Location = New System.Drawing.Point(39, 351)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(248, 63)
        Me.Label28.TabIndex = 98
        Me.Label28.Text = "Note:  These settings are enabled only when you are running this on the Server ma" & _
    "chine.."
        '
        'LabHelpBackupPath
        '
        Me.LabHelpBackupPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.LabHelpBackupPath.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHelpBackupPath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHelpBackupPath.Location = New System.Drawing.Point(243, 38)
        Me.LabHelpBackupPath.Name = "LabHelpBackupPath"
        Me.LabHelpBackupPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHelpBackupPath.Size = New System.Drawing.Size(270, 81)
        Me.LabHelpBackupPath.TabIndex = 97
        Me.LabHelpBackupPath.Text = "LabHelpBackupPath"
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(24, 38)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(186, 33)
        Me.Label27.TabIndex = 96
        Me.Label27.Text = "JobMatix Backup File -Destination Path-"
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancel.CausesValidation = False
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(605, 641)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(92, 26)
        Me.cmdCancel.TabIndex = 38
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'LabHdr3
        '
        Me.LabHdr3.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.LabHdr3.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LabHdr3.Location = New System.Drawing.Point(16, 57)
        Me.LabHdr3.Name = "LabHdr3"
        Me.LabHdr3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr3.Size = New System.Drawing.Size(313, 17)
        Me.LabHdr3.TabIndex = 99
        Me.LabHdr3.Text = "JobMatix Database configuration Settings"
        '
        'labHdr2
        '
        Me.labHdr2.BackColor = System.Drawing.Color.Transparent
        Me.labHdr2.Cursor = System.Windows.Forms.Cursors.Default
        Me.labHdr2.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr2.ForeColor = System.Drawing.Color.Black
        Me.labHdr2.Location = New System.Drawing.Point(128, 8)
        Me.labHdr2.Name = "labHdr2"
        Me.labHdr2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labHdr2.Size = New System.Drawing.Size(201, 41)
        Me.labHdr2.TabIndex = 91
        Me.labHdr2.Text = "Creating New Database"
        '
        'LabVersion
        '
        Me.LabVersion.BackColor = System.Drawing.Color.Transparent
        Me.LabVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabVersion.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabVersion.Location = New System.Drawing.Point(16, 668)
        Me.LabVersion.Name = "LabVersion"
        Me.LabVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabVersion.Size = New System.Drawing.Size(249, 11)
        Me.LabVersion.TabIndex = 42
        Me.LabVersion.Text = "LabVersion"
        '
        'labServer
        '
        Me.labServer.BackColor = System.Drawing.Color.Transparent
        Me.labServer.Cursor = System.Windows.Forms.Cursors.Default
        Me.labServer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labServer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labServer.Location = New System.Drawing.Point(432, 8)
        Me.labServer.Name = "labServer"
        Me.labServer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labServer.Size = New System.Drawing.Size(237, 14)
        Me.labServer.TabIndex = 41
        Me.labServer.Text = "LabServer"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(355, 8)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(71, 17)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "Sql Server: "
        '
        'labStatus
        '
        Me.labStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labStatus.Location = New System.Drawing.Point(16, 638)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labStatus.Size = New System.Drawing.Size(353, 17)
        Me.labStatus.TabIndex = 39
        Me.labStatus.Text = "LabStatus"
        '
        'LabHdr1
        '
        Me.LabHdr1.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.ForeColor = System.Drawing.Color.Black
        Me.LabHdr1.Location = New System.Drawing.Point(16, 1)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr1.Size = New System.Drawing.Size(66, 41)
        Me.LabHdr1.TabIndex = 36
        Me.LabHdr1.Text = "JobMatix Setup- "
        Me.LabHdr1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtChassisCat
        '
        '
        'txtLabourRates
        '
        '
        'txtPriority
        '
        '
        'txtServerBackupFolder
        '
        '
        'txtServiceCategory
        '
        '
        'txtUserTexts
        '
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(420, 34)
        Me.Label29.Name = "Label29"
        Me.Label29.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label29.Size = New System.Drawing.Size(277, 40)
        Me.Label29.TabIndex = 100
        Me.Label29.Text = "Note:  Business and ABN info is the minimum required for initial JobMatix databas" & _
    "e Setup.."
        '
        'frmSetupJobsDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(713, 683)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.SSTab1)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.LabHdr3)
        Me.Controls.Add(Me.labHdr2)
        Me.Controls.Add(Me.LabVersion)
        Me.Controls.Add(Me.labServer)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.labStatus)
        Me.Controls.Add(Me.LabHdr1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(189, 232)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetupJobsDB"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "JobMatix- Setup Jobs DB"
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.frameBusiness.ResumeLayout(False)
        Me.frameBusiness.PerformLayout()
        Me.frameABN.ResumeLayout(False)
        Me.frameABN.PerformLayout()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        Me.frameCategories.ResumeLayout(False)
        Me.frameCategories.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.frameLabour.ResumeLayout(False)
        Me.frameLabour.PerformLayout()
        Me.TabControlLabourRates.ResumeLayout(False)
        Me.TabPageWorkshop.ResumeLayout(False)
        Me.panelHourlyRateDefsWorkshop.ResumeLayout(False)
        Me.panelHourlyRateDefsWorkshop.PerformLayout()
        Me.TabPageOnSite.ResumeLayout(False)
        Me.panelHourlyRateDefsOnSite.ResumeLayout(False)
        Me.panelHourlyRateDefsOnSite.PerformLayout()
        Me.panelOldLabourRates.ResumeLayout(False)
        Me.panelOldLabourRates.PerformLayout()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        Me.frameTexts.ResumeLayout(False)
        Me.frameTexts.PerformLayout()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        Me.frameBackupPath.ResumeLayout(False)
        Me.panelBackupPaths.ResumeLayout(False)
        Me.panelBackupPaths.PerformLayout()
        CType(Me.optRetailHost, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtChassisCat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLabourRates, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPriority, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServerBackupFolder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtServiceCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUserTexts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents labHostName As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents labPOSWarning As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents txtEmail As System.Windows.Forms.TextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents chkNoEnforceMinCharge As System.Windows.Forms.CheckBox
    Friend WithEvents panelHourlyRateDefsWorkshop As System.Windows.Forms.Panel
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents panelOldLabourRates As System.Windows.Forms.Panel
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents labP1Description As System.Windows.Forms.Label
    Friend WithEvents btnP1Browse As System.Windows.Forms.Button
    Public WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtLabourP1Barcode As System.Windows.Forms.TextBox
    Friend WithEvents labP3Rate As System.Windows.Forms.Label
    Friend WithEvents labP3Description As System.Windows.Forms.Label
    Friend WithEvents btnP3Browse As System.Windows.Forms.Button
    Friend WithEvents txtLabourP3Barcode As System.Windows.Forms.TextBox
    Friend WithEvents labP2Rate As System.Windows.Forms.Label
    Friend WithEvents labP2Description As System.Windows.Forms.Label
    Friend WithEvents btnP2Browse As System.Windows.Forms.Button
    Friend WithEvents txtLabourP2Barcode As System.Windows.Forms.TextBox
    Friend WithEvents labP1Rate As System.Windows.Forms.Label
    Friend WithEvents labOnSiteP1Rate As System.Windows.Forms.Label
    Friend WithEvents labOnSiteP1Description As System.Windows.Forms.Label
    Friend WithEvents btnOnSiteP1Browse As System.Windows.Forms.Button
    Friend WithEvents txtLabourOnSiteP1Barcode As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents TabControlLabourRates As System.Windows.Forms.TabControl
    Friend WithEvents TabPageWorkshop As System.Windows.Forms.TabPage
    Friend WithEvents TabPageOnSite As System.Windows.Forms.TabPage
    Public WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents panelHourlyRateDefsOnSite As System.Windows.Forms.Panel
    Public WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtLabourOnSiteP3Barcode As System.Windows.Forms.TextBox
    Friend WithEvents labOnSiteP3Rate As System.Windows.Forms.Label
    Public WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents btnOnSiteP3Browse As System.Windows.Forms.Button
    Friend WithEvents labOnSiteP3Description As System.Windows.Forms.Label
    Friend WithEvents txtLabourOnSiteP2Barcode As System.Windows.Forms.TextBox
    Friend WithEvents labOnSiteP2Rate As System.Windows.Forms.Label
    Public WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents btnOnSiteP2Browse As System.Windows.Forms.Button
    Friend WithEvents labOnSiteP2Description As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents optBusinessTypeOther As System.Windows.Forms.RadioButton
    Friend WithEvents optBusinessTypeComputer As System.Windows.Forms.RadioButton
    Friend WithEvents panelBackupPaths As System.Windows.Forms.Panel
#End Region 
End Class