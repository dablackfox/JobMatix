<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPosMainMdi
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPosMainMdi))
        Me.dlg1Save = New System.Windows.Forms.SaveFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnDropdownNewSale = New JMxPOS330.clsJmxDropdownButton()
        Me.ImageList32 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnMainTill = New JMxPOS330.MyJmxButton()
        Me.picJobTracking = New System.Windows.Forms.PictureBox()
        Me.picJmxLogo = New System.Windows.Forms.PictureBox()
        Me.txtAdminStaffBarcode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnDropdownSettings = New JMxPOS330.clsJmxDropdownButton()
        Me.btnDropdownCategories = New JMxPOS330.clsJmxDropdownButton()
        Me.btnDropdownPurchases = New JMxPOS330.clsJmxDropdownButton()
        Me.btnDropdownStock = New JMxPOS330.clsJmxDropdownButton()
        Me.btnDropdownAccounts = New JMxPOS330.clsJmxDropdownButton()
        Me.btnDropdownReports = New JMxPOS330.clsJmxDropdownButton()
        Me.btnDbBackup = New System.Windows.Forms.Button()
        Me.ImageList40 = New System.Windows.Forms.ImageList(Me.components)
        Me.mContextMenuStripStockAction = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuMainStockAdmin = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainStockSerials = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainStockLabels = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainStockStocktake = New System.Windows.Forms.ToolStripMenuItem()
        Me.mContextMenuStripPurchases = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuMainPurchaseOrders = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainGoodsReceived = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainGoodsReturned = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainGoodsSuppliers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mContextMenuStripCategories = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuMainCategoriesCat1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainCategoriesCat2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainCategoriesBrands = New System.Windows.Forms.ToolStripMenuItem()
        Me.mContextMenuStripTillAction = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuMainTillLastTran = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainTillChange = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainTillBalance = New System.Windows.Forms.ToolStripMenuItem()
        Me.mContextMenuStripAccounts = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuAccountCustomers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAccountLaybys = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAccountSubs = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAccountPayments = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAccountStatements = New System.Windows.Forms.ToolStripMenuItem()
        Me.mContextMenuStripReports = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuReportsSales = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsTransLookup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsCreditNotes = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsDebtors = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsDebtorsSummary = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsDebtorsDetailed = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsDebtorsDetailed_outst = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsDebtorsDetailed_outst_30 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsDebtorsDetailed_outst_60 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsDebtorsCustomerHistory = New System.Windows.Forms.ToolStripMenuItem()
        Me.mContextMenuStripSettings = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuSettingsSetupOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettingsCashDrawers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSettingsStaff = New System.Windows.Forms.ToolStripMenuItem()
        Me.shapedPanelSignOn = New JMxPOS330.ShapedPanel()
        Me.ToolStripFile = New System.Windows.Forms.ToolStrip()
        Me.tsFileDropDownButtonFile = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsFileMenuItemNew = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsFileMenuPreferences = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReorderPaymentDetails = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsFileMenuItemDbInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsFileMenuItemExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labToday = New System.Windows.Forms.Label()
        Me.labAdminStaffName = New System.Windows.Forms.Label()
        Me.shapedPanelAdmin = New JMxPOS330.ShapedPanel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnDropdownEmailQueue = New JMxPOS330.clsJmxDropdownButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabControlMain = New JMxPOS330.clsJmxTabControl()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.tsFileMenuItemAbout = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.picJobTracking, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picJmxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mContextMenuStripStockAction.SuspendLayout()
        Me.mContextMenuStripPurchases.SuspendLayout()
        Me.mContextMenuStripCategories.SuspendLayout()
        Me.mContextMenuStripTillAction.SuspendLayout()
        Me.mContextMenuStripAccounts.SuspendLayout()
        Me.mContextMenuStripReports.SuspendLayout()
        Me.mContextMenuStripSettings.SuspendLayout()
        Me.shapedPanelSignOn.SuspendLayout()
        Me.ToolStripFile.SuspendLayout()
        Me.shapedPanelAdmin.SuspendLayout()
        Me.TabControlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnDropdownNewSale
        '
        Me.btnDropdownNewSale.BorderColor = System.Drawing.Color.White
        Me.btnDropdownNewSale.dnArrowImage = Nothing
        Me.btnDropdownNewSale.ImageIndex = 9
        Me.btnDropdownNewSale.ImageList = Me.ImageList32
        Me.btnDropdownNewSale.Location = New System.Drawing.Point(271, 3)
        Me.btnDropdownNewSale.Name = "btnDropdownNewSale"
        Me.btnDropdownNewSale.RoundRadius = 5
        Me.btnDropdownNewSale.Size = New System.Drawing.Size(56, 63)
        Me.btnDropdownNewSale.TabIndex = 2
        Me.btnDropdownNewSale.Text = "New Sale"
        Me.ToolTip1.SetToolTip(Me.btnDropdownNewSale, "New Sale (F6)")
        Me.btnDropdownNewSale.UseVisualStyleBackColor = True
        '
        'ImageList32
        '
        Me.ImageList32.ImageStream = CType(resources.GetObject("ImageList32.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList32.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList32.Images.SetKeyName(0, "DialogGroup_5846_blue.ico")
        Me.ImageList32.Images.SetKeyName(1, "base_calendar_32.png")
        Me.ImageList32.Images.SetKeyName(2, "DBSchema_12823_32x_brown.png")
        Me.ImageList32.Images.SetKeyName(3, "shopping_cart.png")
        Me.ImageList32.Images.SetKeyName(4, "hand_cart_32px.png")
        Me.ImageList32.Images.SetKeyName(5, "boxes_32px.png")
        Me.ImageList32.Images.SetKeyName(6, "accounting (2).png")
        Me.ImageList32.Images.SetKeyName(7, "reports_32px.png")
        Me.ImageList32.Images.SetKeyName(8, "gear_24px.png")
        Me.ImageList32.Images.SetKeyName(9, "newSale_32px.png")
        Me.ImageList32.Images.SetKeyName(10, "flood_mail_32px.png")
        '
        'btnMainTill
        '
        Me.btnMainTill.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.btnMainTill.BorderColor = System.Drawing.Color.LightSteelBlue
        Me.btnMainTill.CausesValidation = False
        Me.btnMainTill.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMainTill.ForeColor = System.Drawing.Color.DarkViolet
        Me.btnMainTill.Location = New System.Drawing.Point(167, 5)
        Me.btnMainTill.Name = "btnMainTill"
        Me.btnMainTill.RoundRadius = 1
        Me.btnMainTill.Size = New System.Drawing.Size(93, 23)
        Me.btnMainTill.TabIndex = 3
        Me.btnMainTill.Text = "-- Till-x --"
        Me.ToolTip1.SetToolTip(Me.btnMainTill, "Till Functions- " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   -- Show Last Transaction." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   -- Change Till." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   -- CashUp/" & _
        "Till balance." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.btnMainTill.UseVisualStyleBackColor = False
        '
        'picJobTracking
        '
        Me.picJobTracking.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.picJobTracking.Image = CType(resources.GetObject("picJobTracking.Image"), System.Drawing.Image)
        Me.picJobTracking.Location = New System.Drawing.Point(101, 54)
        Me.picJobTracking.Name = "picJobTracking"
        Me.picJobTracking.Padding = New System.Windows.Forms.Padding(4)
        Me.picJobTracking.Size = New System.Drawing.Size(40, 36)
        Me.picJobTracking.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picJobTracking.TabIndex = 67
        Me.picJobTracking.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picJobTracking, "Launch JobTrackingApplication")
        '
        'picJmxLogo
        '
        Me.picJmxLogo.Image = CType(resources.GetObject("picJmxLogo.Image"), System.Drawing.Image)
        Me.picJmxLogo.Location = New System.Drawing.Point(4, 5)
        Me.picJmxLogo.Name = "picJmxLogo"
        Me.picJmxLogo.Size = New System.Drawing.Size(119, 27)
        Me.picJmxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picJmxLogo.TabIndex = 60
        Me.picJmxLogo.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picJmxLogo, "About JobMatixPOS..")
        '
        'txtAdminStaffBarcode
        '
        Me.txtAdminStaffBarcode.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdminStaffBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdminStaffBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdminStaffBarcode.Location = New System.Drawing.Point(217, 58)
        Me.txtAdminStaffBarcode.Name = "txtAdminStaffBarcode"
        Me.txtAdminStaffBarcode.Size = New System.Drawing.Size(36, 26)
        Me.txtAdminStaffBarcode.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtAdminStaffBarcode, "Enter Staff Barcode..")
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(170, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 29)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Admin SignOn"
        Me.ToolTip1.SetToolTip(Me.Label2, "Enter Staff Barcode..")
        '
        'btnDropdownSettings
        '
        Me.btnDropdownSettings.BorderColor = System.Drawing.Color.White
        Me.btnDropdownSettings.dnArrowImage = CType(resources.GetObject("btnDropdownSettings.dnArrowImage"), System.Drawing.Image)
        Me.btnDropdownSettings.ImageIndex = 8
        Me.btnDropdownSettings.ImageList = Me.ImageList32
        Me.btnDropdownSettings.Location = New System.Drawing.Point(522, 8)
        Me.btnDropdownSettings.Name = "btnDropdownSettings"
        Me.btnDropdownSettings.RoundRadius = 5
        Me.btnDropdownSettings.Size = New System.Drawing.Size(72, 80)
        Me.btnDropdownSettings.TabIndex = 6
        Me.btnDropdownSettings.Text = "Settings"
        Me.ToolTip1.SetToolTip(Me.btnDropdownSettings, "--  Setup/Options" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Cash Drawers" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Staff")
        Me.btnDropdownSettings.UseVisualStyleBackColor = True
        '
        'btnDropdownCategories
        '
        Me.btnDropdownCategories.BorderColor = System.Drawing.Color.White
        Me.btnDropdownCategories.dnArrowImage = CType(resources.GetObject("btnDropdownCategories.dnArrowImage"), System.Drawing.Image)
        Me.btnDropdownCategories.ImageIndex = 5
        Me.btnDropdownCategories.ImageList = Me.ImageList32
        Me.btnDropdownCategories.Location = New System.Drawing.Point(176, 7)
        Me.btnDropdownCategories.Name = "btnDropdownCategories"
        Me.btnDropdownCategories.RoundRadius = 5
        Me.btnDropdownCategories.Size = New System.Drawing.Size(72, 80)
        Me.btnDropdownCategories.TabIndex = 2
        Me.btnDropdownCategories.Text = "Categories"
        Me.ToolTip1.SetToolTip(Me.btnDropdownCategories, "-- Cat1" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Cat2" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Brands")
        Me.btnDropdownCategories.UseVisualStyleBackColor = True
        '
        'btnDropdownPurchases
        '
        Me.btnDropdownPurchases.BorderColor = System.Drawing.Color.White
        Me.btnDropdownPurchases.dnArrowImage = CType(resources.GetObject("btnDropdownPurchases.dnArrowImage"), System.Drawing.Image)
        Me.btnDropdownPurchases.ImageIndex = 3
        Me.btnDropdownPurchases.ImageList = Me.ImageList32
        Me.btnDropdownPurchases.Location = New System.Drawing.Point(90, 7)
        Me.btnDropdownPurchases.Name = "btnDropdownPurchases"
        Me.btnDropdownPurchases.RoundRadius = 5
        Me.btnDropdownPurchases.Size = New System.Drawing.Size(72, 80)
        Me.btnDropdownPurchases.TabIndex = 1
        Me.btnDropdownPurchases.Text = "Purchases" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ToolTip1.SetToolTip(Me.btnDropdownPurchases, "-- Purchase Orders " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Goods Received" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Returns (RAs)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Suppliers")
        Me.btnDropdownPurchases.UseVisualStyleBackColor = True
        '
        'btnDropdownStock
        '
        Me.btnDropdownStock.BorderColor = System.Drawing.Color.White
        Me.btnDropdownStock.dnArrowImage = CType(resources.GetObject("btnDropdownStock.dnArrowImage"), System.Drawing.Image)
        Me.btnDropdownStock.ImageIndex = 4
        Me.btnDropdownStock.ImageList = Me.ImageList32
        Me.btnDropdownStock.Location = New System.Drawing.Point(4, 7)
        Me.btnDropdownStock.Name = "btnDropdownStock"
        Me.btnDropdownStock.RoundRadius = 4
        Me.btnDropdownStock.Size = New System.Drawing.Size(72, 80)
        Me.btnDropdownStock.TabIndex = 0
        Me.btnDropdownStock.Text = "Inventory"
        Me.btnDropdownStock.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDropdownStock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ToolTip1.SetToolTip(Me.btnDropdownStock, "-- Stock Admin" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "-- Stock Serials" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Stocktake" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Stock Labels.")
        Me.btnDropdownStock.UseVisualStyleBackColor = True
        '
        'btnDropdownAccounts
        '
        Me.btnDropdownAccounts.BorderColor = System.Drawing.Color.White
        Me.btnDropdownAccounts.dnArrowImage = CType(resources.GetObject("btnDropdownAccounts.dnArrowImage"), System.Drawing.Image)
        Me.btnDropdownAccounts.ImageIndex = 6
        Me.btnDropdownAccounts.ImageList = Me.ImageList32
        Me.btnDropdownAccounts.Location = New System.Drawing.Point(261, 7)
        Me.btnDropdownAccounts.Name = "btnDropdownAccounts"
        Me.btnDropdownAccounts.RoundRadius = 5
        Me.btnDropdownAccounts.Size = New System.Drawing.Size(72, 80)
        Me.btnDropdownAccounts.TabIndex = 3
        Me.btnDropdownAccounts.Text = "Accounts"
        Me.ToolTip1.SetToolTip(Me.btnDropdownAccounts, "--  Customers" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Laybys" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Subscriptions" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Payments" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Debtors Statement" & _
        "s (and Report)")
        Me.btnDropdownAccounts.UseVisualStyleBackColor = True
        '
        'btnDropdownReports
        '
        Me.btnDropdownReports.BorderColor = System.Drawing.Color.White
        Me.btnDropdownReports.dnArrowImage = CType(resources.GetObject("btnDropdownReports.dnArrowImage"), System.Drawing.Image)
        Me.btnDropdownReports.ImageIndex = 7
        Me.btnDropdownReports.ImageList = Me.ImageList32
        Me.btnDropdownReports.Location = New System.Drawing.Point(348, 7)
        Me.btnDropdownReports.Name = "btnDropdownReports"
        Me.btnDropdownReports.RoundRadius = 5
        Me.btnDropdownReports.Size = New System.Drawing.Size(72, 80)
        Me.btnDropdownReports.TabIndex = 4
        Me.btnDropdownReports.Text = "  Reports"
        Me.ToolTip1.SetToolTip(Me.btnDropdownReports, "--  Sales/Stock" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Transaction Lookup" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Credit Notes Report" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "--  Debtors Rep" & _
        "ort")
        Me.btnDropdownReports.UseVisualStyleBackColor = True
        '
        'btnDbBackup
        '
        Me.btnDbBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.btnDbBackup.Image = CType(resources.GetObject("btnDbBackup.Image"), System.Drawing.Image)
        Me.btnDbBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDbBackup.Location = New System.Drawing.Point(597, 8)
        Me.btnDbBackup.Name = "btnDbBackup"
        Me.btnDbBackup.Size = New System.Drawing.Size(72, 58)
        Me.btnDbBackup.TabIndex = 7
        Me.btnDbBackup.Text = "       Backup"
        Me.btnDbBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDbBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnDbBackup, "Backup database")
        Me.btnDbBackup.UseVisualStyleBackColor = False
        '
        'ImageList40
        '
        Me.ImageList40.ImageStream = CType(resources.GetObject("ImageList40.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList40.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList40.Images.SetKeyName(0, "fork-stock-42x36_brown.png")
        '
        'mContextMenuStripStockAction
        '
        Me.mContextMenuStripStockAction.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mContextMenuStripStockAction.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.mContextMenuStripStockAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainStockAdmin, Me.mnuMainStockSerials, Me.mnuMainStockLabels, Me.mnuMainStockStocktake})
        Me.mContextMenuStripStockAction.Name = "mContextMenuStripStockAction"
        Me.mContextMenuStripStockAction.Size = New System.Drawing.Size(159, 132)
        '
        'mnuMainStockAdmin
        '
        Me.mnuMainStockAdmin.AutoSize = False
        Me.mnuMainStockAdmin.Image = CType(resources.GetObject("mnuMainStockAdmin.Image"), System.Drawing.Image)
        Me.mnuMainStockAdmin.Name = "mnuMainStockAdmin"
        Me.mnuMainStockAdmin.Size = New System.Drawing.Size(206, 32)
        Me.mnuMainStockAdmin.Text = "Stock Admin"
        '
        'mnuMainStockSerials
        '
        Me.mnuMainStockSerials.AutoSize = False
        Me.mnuMainStockSerials.Image = CType(resources.GetObject("mnuMainStockSerials.Image"), System.Drawing.Image)
        Me.mnuMainStockSerials.Name = "mnuMainStockSerials"
        Me.mnuMainStockSerials.Size = New System.Drawing.Size(198, 32)
        Me.mnuMainStockSerials.Text = "Stock Serials"
        '
        'mnuMainStockLabels
        '
        Me.mnuMainStockLabels.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar
        Me.mnuMainStockLabels.AutoSize = False
        Me.mnuMainStockLabels.Image = CType(resources.GetObject("mnuMainStockLabels.Image"), System.Drawing.Image)
        Me.mnuMainStockLabels.Name = "mnuMainStockLabels"
        Me.mnuMainStockLabels.Size = New System.Drawing.Size(198, 32)
        Me.mnuMainStockLabels.Text = "Stock Labels"
        '
        'mnuMainStockStocktake
        '
        Me.mnuMainStockStocktake.AutoSize = False
        Me.mnuMainStockStocktake.Image = CType(resources.GetObject("mnuMainStockStocktake.Image"), System.Drawing.Image)
        Me.mnuMainStockStocktake.Name = "mnuMainStockStocktake"
        Me.mnuMainStockStocktake.Size = New System.Drawing.Size(198, 32)
        Me.mnuMainStockStocktake.Text = "Stocktake"
        '
        'mContextMenuStripPurchases
        '
        Me.mContextMenuStripPurchases.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mContextMenuStripPurchases.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.mContextMenuStripPurchases.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainPurchaseOrders, Me.mnuMainGoodsReceived, Me.mnuMainGoodsReturned, Me.mnuMainGoodsSuppliers})
        Me.mContextMenuStripPurchases.Name = "mContextMenuStripPurchases"
        Me.mContextMenuStripPurchases.Size = New System.Drawing.Size(231, 132)
        '
        'mnuMainPurchaseOrders
        '
        Me.mnuMainPurchaseOrders.AutoSize = False
        Me.mnuMainPurchaseOrders.Image = CType(resources.GetObject("mnuMainPurchaseOrders.Image"), System.Drawing.Image)
        Me.mnuMainPurchaseOrders.Name = "mnuMainPurchaseOrders"
        Me.mnuMainPurchaseOrders.Size = New System.Drawing.Size(192, 32)
        Me.mnuMainPurchaseOrders.Text = "Purchase Orders"
        '
        'mnuMainGoodsReceived
        '
        Me.mnuMainGoodsReceived.AutoSize = False
        Me.mnuMainGoodsReceived.Image = CType(resources.GetObject("mnuMainGoodsReceived.Image"), System.Drawing.Image)
        Me.mnuMainGoodsReceived.Name = "mnuMainGoodsReceived"
        Me.mnuMainGoodsReceived.Size = New System.Drawing.Size(192, 32)
        Me.mnuMainGoodsReceived.Text = "Goods Received"
        '
        'mnuMainGoodsReturned
        '
        Me.mnuMainGoodsReturned.AutoSize = False
        Me.mnuMainGoodsReturned.Image = CType(resources.GetObject("mnuMainGoodsReturned.Image"), System.Drawing.Image)
        Me.mnuMainGoodsReturned.Name = "mnuMainGoodsReturned"
        Me.mnuMainGoodsReturned.Size = New System.Drawing.Size(222, 32)
        Me.mnuMainGoodsReturned.Text = "Returns to Supplier (RAs)"
        '
        'mnuMainGoodsSuppliers
        '
        Me.mnuMainGoodsSuppliers.AutoSize = False
        Me.mnuMainGoodsSuppliers.Image = CType(resources.GetObject("mnuMainGoodsSuppliers.Image"), System.Drawing.Image)
        Me.mnuMainGoodsSuppliers.Name = "mnuMainGoodsSuppliers"
        Me.mnuMainGoodsSuppliers.Size = New System.Drawing.Size(222, 32)
        Me.mnuMainGoodsSuppliers.Text = "Suppliers"
        '
        'mContextMenuStripCategories
        '
        Me.mContextMenuStripCategories.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mContextMenuStripCategories.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.mContextMenuStripCategories.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainCategoriesCat1, Me.mnuMainCategoriesCat2, Me.mnuMainCategoriesBrands})
        Me.mContextMenuStripCategories.Name = "mContextMenuStripCategories"
        Me.mContextMenuStripCategories.Size = New System.Drawing.Size(124, 100)
        '
        'mnuMainCategoriesCat1
        '
        Me.mnuMainCategoriesCat1.AutoSize = False
        Me.mnuMainCategoriesCat1.Image = CType(resources.GetObject("mnuMainCategoriesCat1.Image"), System.Drawing.Image)
        Me.mnuMainCategoriesCat1.Name = "mnuMainCategoriesCat1"
        Me.mnuMainCategoriesCat1.Size = New System.Drawing.Size(152, 32)
        Me.mnuMainCategoriesCat1.Text = "Cat1"
        '
        'mnuMainCategoriesCat2
        '
        Me.mnuMainCategoriesCat2.AutoSize = False
        Me.mnuMainCategoriesCat2.Image = CType(resources.GetObject("mnuMainCategoriesCat2.Image"), System.Drawing.Image)
        Me.mnuMainCategoriesCat2.Name = "mnuMainCategoriesCat2"
        Me.mnuMainCategoriesCat2.Size = New System.Drawing.Size(152, 32)
        Me.mnuMainCategoriesCat2.Text = "Cat2"
        '
        'mnuMainCategoriesBrands
        '
        Me.mnuMainCategoriesBrands.AutoSize = False
        Me.mnuMainCategoriesBrands.Image = CType(resources.GetObject("mnuMainCategoriesBrands.Image"), System.Drawing.Image)
        Me.mnuMainCategoriesBrands.Name = "mnuMainCategoriesBrands"
        Me.mnuMainCategoriesBrands.Size = New System.Drawing.Size(152, 32)
        Me.mnuMainCategoriesBrands.Text = "Brands"
        '
        'mContextMenuStripTillAction
        '
        Me.mContextMenuStripTillAction.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mContextMenuStripTillAction.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.mContextMenuStripTillAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainTillLastTran, Me.mnuMainTillChange, Me.mnuMainTillBalance})
        Me.mContextMenuStripTillAction.Name = "mContextMenuStripTillAction"
        Me.mContextMenuStripTillAction.Size = New System.Drawing.Size(215, 100)
        '
        'mnuMainTillLastTran
        '
        Me.mnuMainTillLastTran.AutoSize = False
        Me.mnuMainTillLastTran.Image = CType(resources.GetObject("mnuMainTillLastTran.Image"), System.Drawing.Image)
        Me.mnuMainTillLastTran.Name = "mnuMainTillLastTran"
        Me.mnuMainTillLastTran.Size = New System.Drawing.Size(206, 32)
        Me.mnuMainTillLastTran.Text = "Show Last Transaction"
        '
        'mnuMainTillChange
        '
        Me.mnuMainTillChange.AutoSize = False
        Me.mnuMainTillChange.Image = CType(resources.GetObject("mnuMainTillChange.Image"), System.Drawing.Image)
        Me.mnuMainTillChange.Name = "mnuMainTillChange"
        Me.mnuMainTillChange.Size = New System.Drawing.Size(206, 32)
        Me.mnuMainTillChange.Text = "Change Till"
        '
        'mnuMainTillBalance
        '
        Me.mnuMainTillBalance.AutoSize = False
        Me.mnuMainTillBalance.Image = CType(resources.GetObject("mnuMainTillBalance.Image"), System.Drawing.Image)
        Me.mnuMainTillBalance.Name = "mnuMainTillBalance"
        Me.mnuMainTillBalance.Size = New System.Drawing.Size(206, 32)
        Me.mnuMainTillBalance.Text = "CashUp/Till balance"
        '
        'mContextMenuStripAccounts
        '
        Me.mContextMenuStripAccounts.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mContextMenuStripAccounts.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.mContextMenuStripAccounts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAccountCustomers, Me.mnuAccountLaybys, Me.mnuAccountSubs, Me.mnuAccountPayments, Me.mnuAccountStatements})
        Me.mContextMenuStripAccounts.Name = "mContextMenuStripAccounts"
        Me.mContextMenuStripAccounts.Size = New System.Drawing.Size(244, 164)
        '
        'mnuAccountCustomers
        '
        Me.mnuAccountCustomers.AutoSize = False
        Me.mnuAccountCustomers.Image = CType(resources.GetObject("mnuAccountCustomers.Image"), System.Drawing.Image)
        Me.mnuAccountCustomers.Name = "mnuAccountCustomers"
        Me.mnuAccountCustomers.Size = New System.Drawing.Size(180, 32)
        Me.mnuAccountCustomers.Text = "Customers"
        '
        'mnuAccountLaybys
        '
        Me.mnuAccountLaybys.AutoSize = False
        Me.mnuAccountLaybys.Image = CType(resources.GetObject("mnuAccountLaybys.Image"), System.Drawing.Image)
        Me.mnuAccountLaybys.Name = "mnuAccountLaybys"
        Me.mnuAccountLaybys.Size = New System.Drawing.Size(180, 32)
        Me.mnuAccountLaybys.Text = "Laybys"
        '
        'mnuAccountSubs
        '
        Me.mnuAccountSubs.AutoSize = False
        Me.mnuAccountSubs.Image = CType(resources.GetObject("mnuAccountSubs.Image"), System.Drawing.Image)
        Me.mnuAccountSubs.Name = "mnuAccountSubs"
        Me.mnuAccountSubs.Size = New System.Drawing.Size(180, 32)
        Me.mnuAccountSubs.Text = "Subscriptions"
        '
        'mnuAccountPayments
        '
        Me.mnuAccountPayments.AutoSize = False
        Me.mnuAccountPayments.Image = CType(resources.GetObject("mnuAccountPayments.Image"), System.Drawing.Image)
        Me.mnuAccountPayments.Name = "mnuAccountPayments"
        Me.mnuAccountPayments.Size = New System.Drawing.Size(180, 32)
        Me.mnuAccountPayments.Text = "Account Payments"
        '
        'mnuAccountStatements
        '
        Me.mnuAccountStatements.AutoSize = False
        Me.mnuAccountStatements.Image = CType(resources.GetObject("mnuAccountStatements.Image"), System.Drawing.Image)
        Me.mnuAccountStatements.Name = "mnuAccountStatements"
        Me.mnuAccountStatements.Size = New System.Drawing.Size(220, 32)
        Me.mnuAccountStatements.Text = "Debtors Statements, Report"
        '
        'mContextMenuStripReports
        '
        Me.mContextMenuStripReports.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.mContextMenuStripReports.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportsSales, Me.mnuReportsTransLookup, Me.mnuReportsCreditNotes, Me.mnuReportsDebtors})
        Me.mContextMenuStripReports.Name = "mContextMenuStripReports"
        Me.mContextMenuStripReports.Size = New System.Drawing.Size(187, 132)
        '
        'mnuReportsSales
        '
        Me.mnuReportsSales.AutoSize = False
        Me.mnuReportsSales.Image = CType(resources.GetObject("mnuReportsSales.Image"), System.Drawing.Image)
        Me.mnuReportsSales.Name = "mnuReportsSales"
        Me.mnuReportsSales.Size = New System.Drawing.Size(182, 32)
        Me.mnuReportsSales.Text = "Sales/Stock Reports"
        '
        'mnuReportsTransLookup
        '
        Me.mnuReportsTransLookup.AutoSize = False
        Me.mnuReportsTransLookup.Image = CType(resources.GetObject("mnuReportsTransLookup.Image"), System.Drawing.Image)
        Me.mnuReportsTransLookup.Name = "mnuReportsTransLookup"
        Me.mnuReportsTransLookup.Size = New System.Drawing.Size(182, 32)
        Me.mnuReportsTransLookup.Text = "Transaction Lookup"
        '
        'mnuReportsCreditNotes
        '
        Me.mnuReportsCreditNotes.AutoSize = False
        Me.mnuReportsCreditNotes.Image = CType(resources.GetObject("mnuReportsCreditNotes.Image"), System.Drawing.Image)
        Me.mnuReportsCreditNotes.Name = "mnuReportsCreditNotes"
        Me.mnuReportsCreditNotes.Size = New System.Drawing.Size(182, 32)
        Me.mnuReportsCreditNotes.Text = "Credit Notes Report"
        '
        'mnuReportsDebtors
        '
        Me.mnuReportsDebtors.AutoSize = False
        Me.mnuReportsDebtors.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportsDebtorsSummary, Me.mnuReportsDebtorsDetailed})
        Me.mnuReportsDebtors.Image = CType(resources.GetObject("mnuReportsDebtors.Image"), System.Drawing.Image)
        Me.mnuReportsDebtors.Name = "mnuReportsDebtors"
        Me.mnuReportsDebtors.Size = New System.Drawing.Size(182, 32)
        Me.mnuReportsDebtors.Text = "Debtors Report"
        '
        'mnuReportsDebtorsSummary
        '
        Me.mnuReportsDebtorsSummary.AutoSize = False
        Me.mnuReportsDebtorsSummary.Name = "mnuReportsDebtorsSummary"
        Me.mnuReportsDebtorsSummary.Size = New System.Drawing.Size(200, 32)
        Me.mnuReportsDebtorsSummary.Text = "Debtors Summary (Outst.)"
        Me.mnuReportsDebtorsSummary.ToolTipText = "Debtors Summary (Outstanding Invoices.)"
        '
        'mnuReportsDebtorsDetailed
        '
        Me.mnuReportsDebtorsDetailed.AutoSize = False
        Me.mnuReportsDebtorsDetailed.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportsDebtorsDetailed_outst, Me.mnuReportsDebtorsDetailed_outst_30, Me.mnuReportsDebtorsDetailed_outst_60, Me.mnuReportsDebtorsCustomerHistory})
        Me.mnuReportsDebtorsDetailed.Name = "mnuReportsDebtorsDetailed"
        Me.mnuReportsDebtorsDetailed.Size = New System.Drawing.Size(200, 32)
        Me.mnuReportsDebtorsDetailed.Text = "Debtors Report Detailed"
        '
        'mnuReportsDebtorsDetailed_outst
        '
        Me.mnuReportsDebtorsDetailed_outst.AutoSize = False
        Me.mnuReportsDebtorsDetailed_outst.Name = "mnuReportsDebtorsDetailed_outst"
        Me.mnuReportsDebtorsDetailed_outst.Size = New System.Drawing.Size(200, 32)
        Me.mnuReportsDebtorsDetailed_outst.Text = "Detailed (Outstanding)"
        Me.mnuReportsDebtorsDetailed_outst.ToolTipText = "DebtorsReport  Detailed (Outstanding Invoices only)"
        '
        'mnuReportsDebtorsDetailed_outst_30
        '
        Me.mnuReportsDebtorsDetailed_outst_30.AutoSize = False
        Me.mnuReportsDebtorsDetailed_outst_30.Name = "mnuReportsDebtorsDetailed_outst_30"
        Me.mnuReportsDebtorsDetailed_outst_30.Size = New System.Drawing.Size(200, 32)
        Me.mnuReportsDebtorsDetailed_outst_30.Text = "Detailed (Outst. +30 days closed)"
        Me.mnuReportsDebtorsDetailed_outst_30.ToolTipText = "Debtors Report- Detailed" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " (Outstanding  +30 days closed invoices)"
        '
        'mnuReportsDebtorsDetailed_outst_60
        '
        Me.mnuReportsDebtorsDetailed_outst_60.AutoSize = False
        Me.mnuReportsDebtorsDetailed_outst_60.Name = "mnuReportsDebtorsDetailed_outst_60"
        Me.mnuReportsDebtorsDetailed_outst_60.Size = New System.Drawing.Size(200, 32)
        Me.mnuReportsDebtorsDetailed_outst_60.Text = "Detailed (Outst. +60 days closed)"
        Me.mnuReportsDebtorsDetailed_outst_60.ToolTipText = "Debtors Report- Detailed" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " (Outstanding  +60 days closed invoices)"
        '
        'mnuReportsDebtorsCustomerHistory
        '
        Me.mnuReportsDebtorsCustomerHistory.AutoSize = False
        Me.mnuReportsDebtorsCustomerHistory.Name = "mnuReportsDebtorsCustomerHistory"
        Me.mnuReportsDebtorsCustomerHistory.Size = New System.Drawing.Size(200, 32)
        Me.mnuReportsDebtorsCustomerHistory.Text = "Report- Customer History"
        Me.mnuReportsDebtorsCustomerHistory.ToolTipText = "Debtors Report- " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Detailed History for selected Customer"
        '
        'mContextMenuStripSettings
        '
        Me.mContextMenuStripSettings.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.mContextMenuStripSettings.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSettingsSetupOptions, Me.mnuSettingsCashDrawers, Me.mnuSettingsStaff})
        Me.mContextMenuStripSettings.Name = "mContextMenuStripSettings"
        Me.mContextMenuStripSettings.Size = New System.Drawing.Size(160, 100)
        '
        'mnuSettingsSetupOptions
        '
        Me.mnuSettingsSetupOptions.AutoSize = False
        Me.mnuSettingsSetupOptions.Image = CType(resources.GetObject("mnuSettingsSetupOptions.Image"), System.Drawing.Image)
        Me.mnuSettingsSetupOptions.Name = "mnuSettingsSetupOptions"
        Me.mnuSettingsSetupOptions.Size = New System.Drawing.Size(152, 32)
        Me.mnuSettingsSetupOptions.Text = "Setup/Options"
        '
        'mnuSettingsCashDrawers
        '
        Me.mnuSettingsCashDrawers.AutoSize = False
        Me.mnuSettingsCashDrawers.Image = CType(resources.GetObject("mnuSettingsCashDrawers.Image"), System.Drawing.Image)
        Me.mnuSettingsCashDrawers.Name = "mnuSettingsCashDrawers"
        Me.mnuSettingsCashDrawers.Size = New System.Drawing.Size(152, 32)
        Me.mnuSettingsCashDrawers.Text = "Cash Drawers"
        Me.mnuSettingsCashDrawers.ToolTipText = "Setup Cash Drawer Connections"
        '
        'mnuSettingsStaff
        '
        Me.mnuSettingsStaff.AutoSize = False
        Me.mnuSettingsStaff.Image = CType(resources.GetObject("mnuSettingsStaff.Image"), System.Drawing.Image)
        Me.mnuSettingsStaff.Name = "mnuSettingsStaff"
        Me.mnuSettingsStaff.Size = New System.Drawing.Size(152, 32)
        Me.mnuSettingsStaff.Text = "Staff"
        '
        'shapedPanelSignOn
        '
        Me.shapedPanelSignOn.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.shapedPanelSignOn.BorderColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.shapedPanelSignOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.shapedPanelSignOn.Controls.Add(Me.btnDropdownNewSale)
        Me.shapedPanelSignOn.Controls.Add(Me.ToolStripFile)
        Me.shapedPanelSignOn.Controls.Add(Me.btnMainTill)
        Me.shapedPanelSignOn.Controls.Add(Me.Label1)
        Me.shapedPanelSignOn.Controls.Add(Me.picJobTracking)
        Me.shapedPanelSignOn.Controls.Add(Me.labToday)
        Me.shapedPanelSignOn.Controls.Add(Me.picJmxLogo)
        Me.shapedPanelSignOn.Controls.Add(Me.txtAdminStaffBarcode)
        Me.shapedPanelSignOn.Controls.Add(Me.labAdminStaffName)
        Me.shapedPanelSignOn.Controls.Add(Me.Label2)
        Me.shapedPanelSignOn.Edge = 5
        Me.shapedPanelSignOn.Location = New System.Drawing.Point(1, 1)
        Me.shapedPanelSignOn.Name = "shapedPanelSignOn"
        Me.shapedPanelSignOn.Size = New System.Drawing.Size(338, 96)
        Me.shapedPanelSignOn.TabIndex = 0
        Me.shapedPanelSignOn.TabStop = True
        '
        'ToolStripFile
        '
        Me.ToolStripFile.AutoSize = False
        Me.ToolStripFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.ToolStripFile.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStripFile.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripFile.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripFile.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsFileDropDownButtonFile})
        Me.ToolStripFile.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStripFile.Location = New System.Drawing.Point(10, 58)
        Me.ToolStripFile.Name = "ToolStripFile"
        Me.ToolStripFile.Size = New System.Drawing.Size(66, 31)
        Me.ToolStripFile.TabIndex = 0
        Me.ToolStripFile.Text = "ToolStripFile"
        '
        'tsFileDropDownButtonFile
        '
        Me.tsFileDropDownButtonFile.AutoSize = False
        Me.tsFileDropDownButtonFile.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.tsFileDropDownButtonFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsFileDropDownButtonFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsFileMenuItemNew, Me.tsFileMenuPreferences, Me.tsFileMenuItemDbInfo, Me.tsFileMenuItemAbout, Me.tsFileMenuItemExit})
        Me.tsFileDropDownButtonFile.Image = CType(resources.GetObject("tsFileDropDownButtonFile.Image"), System.Drawing.Image)
        Me.tsFileDropDownButtonFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsFileDropDownButtonFile.Name = "tsFileDropDownButtonFile"
        Me.tsFileDropDownButtonFile.Size = New System.Drawing.Size(47, 31)
        Me.tsFileDropDownButtonFile.Text = "File"
        Me.tsFileDropDownButtonFile.ToolTipText = "File Menu"
        '
        'tsFileMenuItemNew
        '
        Me.tsFileMenuItemNew.Name = "tsFileMenuItemNew"
        Me.tsFileMenuItemNew.Size = New System.Drawing.Size(182, 22)
        Me.tsFileMenuItemNew.Text = "New Sale"
        '
        'tsFileMenuPreferences
        '
        Me.tsFileMenuPreferences.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReorderPaymentDetails})
        Me.tsFileMenuPreferences.Name = "tsFileMenuPreferences"
        Me.tsFileMenuPreferences.Size = New System.Drawing.Size(182, 22)
        Me.tsFileMenuPreferences.Text = "Preferences"
        '
        'mnuReorderPaymentDetails
        '
        Me.mnuReorderPaymentDetails.Name = "mnuReorderPaymentDetails"
        Me.mnuReorderPaymentDetails.Size = New System.Drawing.Size(208, 22)
        Me.mnuReorderPaymentDetails.Text = "Reorder Payment Details"
        '
        'tsFileMenuItemDbInfo
        '
        Me.tsFileMenuItemDbInfo.Name = "tsFileMenuItemDbInfo"
        Me.tsFileMenuItemDbInfo.Size = New System.Drawing.Size(182, 22)
        Me.tsFileMenuItemDbInfo.Text = "DbInfo"
        '
        'tsFileMenuItemExit
        '
        Me.tsFileMenuItemExit.Name = "tsFileMenuItemExit"
        Me.tsFileMenuItemExit.Size = New System.Drawing.Size(182, 22)
        Me.tsFileMenuItemExit.Text = "Exit"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(98, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(42, 17)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "-Jobs-"
        '
        'labToday
        '
        Me.labToday.Location = New System.Drawing.Point(8, 38)
        Me.labToday.Name = "labToday"
        Me.labToday.Size = New System.Drawing.Size(94, 14)
        Me.labToday.TabIndex = 61
        Me.labToday.Text = "labToday"
        '
        'labAdminStaffName
        '
        Me.labAdminStaffName.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labAdminStaffName.Location = New System.Drawing.Point(259, 70)
        Me.labAdminStaffName.Name = "labAdminStaffName"
        Me.labAdminStaffName.Size = New System.Drawing.Size(66, 19)
        Me.labAdminStaffName.TabIndex = 5
        Me.labAdminStaffName.Text = "labAdminStaffName"
        Me.labAdminStaffName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'shapedPanelAdmin
        '
        Me.shapedPanelAdmin.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.shapedPanelAdmin.BorderColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.shapedPanelAdmin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.shapedPanelAdmin.Controls.Add(Me.Label9)
        Me.shapedPanelAdmin.Controls.Add(Me.Label8)
        Me.shapedPanelAdmin.Controls.Add(Me.btnDropdownEmailQueue)
        Me.shapedPanelAdmin.Controls.Add(Me.btnDropdownSettings)
        Me.shapedPanelAdmin.Controls.Add(Me.btnDropdownCategories)
        Me.shapedPanelAdmin.Controls.Add(Me.btnDropdownPurchases)
        Me.shapedPanelAdmin.Controls.Add(Me.btnDropdownStock)
        Me.shapedPanelAdmin.Controls.Add(Me.btnDropdownAccounts)
        Me.shapedPanelAdmin.Controls.Add(Me.btnDropdownReports)
        Me.shapedPanelAdmin.Controls.Add(Me.Label7)
        Me.shapedPanelAdmin.Controls.Add(Me.Label5)
        Me.shapedPanelAdmin.Controls.Add(Me.Label4)
        Me.shapedPanelAdmin.Controls.Add(Me.Label3)
        Me.shapedPanelAdmin.Controls.Add(Me.btnDbBackup)
        Me.shapedPanelAdmin.Edge = 5
        Me.shapedPanelAdmin.Location = New System.Drawing.Point(340, 1)
        Me.shapedPanelAdmin.Name = "shapedPanelAdmin"
        Me.shapedPanelAdmin.Size = New System.Drawing.Size(678, 96)
        Me.shapedPanelAdmin.TabIndex = 1
        Me.shapedPanelAdmin.TabStop = True
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(254, 7)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(1, 80)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "Label9"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(169, 7)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(1, 80)
        Me.Label8.TabIndex = 43
        Me.Label8.Text = "Label8"
        '
        'btnDropdownEmailQueue
        '
        Me.btnDropdownEmailQueue.BorderColor = System.Drawing.Color.White
        Me.btnDropdownEmailQueue.dnArrowImage = Nothing
        Me.btnDropdownEmailQueue.ImageIndex = 10
        Me.btnDropdownEmailQueue.ImageList = Me.ImageList32
        Me.btnDropdownEmailQueue.Location = New System.Drawing.Point(436, 7)
        Me.btnDropdownEmailQueue.Name = "btnDropdownEmailQueue"
        Me.btnDropdownEmailQueue.RoundRadius = 5
        Me.btnDropdownEmailQueue.Size = New System.Drawing.Size(72, 73)
        Me.btnDropdownEmailQueue.TabIndex = 5
        Me.btnDropdownEmailQueue.Text = "Email Queue"
        Me.btnDropdownEmailQueue.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(340, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(1, 80)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Label7"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(514, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(1, 80)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "Label5"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(427, 10)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(1, 80)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Label4"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(198, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(83, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(1, 80)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Label3"
        '
        'TabControlMain
        '
        Me.TabControlMain.CausesValidation = False
        Me.TabControlMain.CloseIcon = CType(resources.GetObject("TabControlMain.CloseIcon"), System.Drawing.Image)
        Me.TabControlMain.Controls.Add(Me.TabPage2)
        Me.TabControlMain.Controls.Add(Me.TabPage3)
        Me.TabControlMain.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        Me.TabControlMain.ItemSize = New System.Drawing.Size(140, 21)
        Me.TabControlMain.Location = New System.Drawing.Point(1, 99)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(1021, 679)
        Me.TabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControlMain.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1013, 650)
        Me.TabPage2.TabIndex = 0
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 25)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1013, 650)
        Me.TabPage3.TabIndex = 1
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'tsFileMenuItemAbout
        '
        Me.tsFileMenuItemAbout.Name = "tsFileMenuItemAbout"
        Me.tsFileMenuItemAbout.Size = New System.Drawing.Size(182, 22)
        Me.tsFileMenuItemAbout.Text = "About JobMatixPOS"
        '
        'frmPosMainMdi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(1025, 781)
        Me.Controls.Add(Me.shapedPanelSignOn)
        Me.Controls.Add(Me.shapedPanelAdmin)
        Me.Controls.Add(Me.TabControlMain)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPosMainMdi"
        Me.Text = "frmPosMain"
        CType(Me.picJobTracking, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picJmxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mContextMenuStripStockAction.ResumeLayout(False)
        Me.mContextMenuStripPurchases.ResumeLayout(False)
        Me.mContextMenuStripCategories.ResumeLayout(False)
        Me.mContextMenuStripTillAction.ResumeLayout(False)
        Me.mContextMenuStripAccounts.ResumeLayout(False)
        Me.mContextMenuStripReports.ResumeLayout(False)
        Me.mContextMenuStripSettings.ResumeLayout(False)
        Me.shapedPanelSignOn.ResumeLayout(False)
        Me.shapedPanelSignOn.PerformLayout()
        Me.ToolStripFile.ResumeLayout(False)
        Me.ToolStripFile.PerformLayout()
        Me.shapedPanelAdmin.ResumeLayout(False)
        Me.TabControlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents labAdminStaffName As System.Windows.Forms.Label
    Friend WithEvents txtAdminStaffBarcode As System.Windows.Forms.TextBox
    Friend WithEvents dlg1Save As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDbBackup As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents shapedPanelAdmin As JMxPOS330.ShapedPanel
    Friend WithEvents shapedPanelSignOn As JMxPOS330.ShapedPanel
    Friend WithEvents picJmxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents labToday As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picJobTracking As System.Windows.Forms.PictureBox
    Friend WithEvents btnMainTill As JMxPOS330.MyJmxButton
    Friend WithEvents TabControlMain As JMxPOS330.clsJmxTabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ToolStripFile As System.Windows.Forms.ToolStrip
    Friend WithEvents tsFileDropDownButtonFile As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents tsFileMenuItemNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsFileMenuItemDbInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsFileMenuItemExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tsFileMenuPreferences As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReorderPaymentDetails As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnDropdownReports As JMxPOS330.clsJmxDropdownButton
    Friend WithEvents ImageList32 As System.Windows.Forms.ImageList
    Friend WithEvents btnDropdownAccounts As JMxPOS330.clsJmxDropdownButton
    Friend WithEvents btnDropdownCategories As JMxPOS330.clsJmxDropdownButton
    Friend WithEvents btnDropdownPurchases As JMxPOS330.clsJmxDropdownButton
    Friend WithEvents btnDropdownStock As JMxPOS330.clsJmxDropdownButton
    Friend WithEvents ImageList40 As System.Windows.Forms.ImageList
    Friend WithEvents mContextMenuStripStockAction As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuMainStockAdmin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainStockSerials As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainStockStocktake As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainStockLabels As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mContextMenuStripPurchases As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuMainPurchaseOrders As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainGoodsReceived As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainGoodsReturned As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainGoodsSuppliers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mContextMenuStripCategories As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuMainCategoriesCat1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainCategoriesCat2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainCategoriesBrands As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mContextMenuStripTillAction As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuMainTillLastTran As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainTillChange As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainTillBalance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mContextMenuStripAccounts As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuAccountPayments As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccountSubs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccountCustomers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccountStatements As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAccountLaybys As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mContextMenuStripReports As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuReportsSales As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsCreditNotes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsDebtors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsDebtorsSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsDebtorsDetailed As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsDebtorsDetailed_outst As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsDebtorsDetailed_outst_30 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsDebtorsDetailed_outst_60 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsDebtorsCustomerHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsTransLookup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnDropdownSettings As JMxPOS330.clsJmxDropdownButton
    Friend WithEvents mContextMenuStripSettings As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuSettingsSetupOptions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSettingsCashDrawers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSettingsStaff As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnDropdownNewSale As JMxPOS330.clsJmxDropdownButton
    Friend WithEvents btnDropdownEmailQueue As JMxPOS330.clsJmxDropdownButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tsFileMenuItemAbout As System.Windows.Forms.ToolStripMenuItem

End Class
