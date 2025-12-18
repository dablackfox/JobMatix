<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPOS34Setup
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
        Me.grpBoxAdmin = New System.Windows.Forms.GroupBox()
        Me.ShapedPanelAdminHdr = New JMxPOS330.ShapedPanel()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.cboReceiptPrinters = New System.Windows.Forms.ComboBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.labStaffName = New System.Windows.Forms.Label()
        Me.cboReportPrinters = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageOptions = New System.Windows.Forms.TabPage()
        Me.panelPOS_Setup = New System.Windows.Forms.Panel()
        Me.panelPricingGrades = New System.Windows.Forms.Panel()
        Me.btnSaveGrades = New System.Windows.Forms.Button()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.txtPriceGrade4 = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtPriceGrade3 = New System.Windows.Forms.TextBox()
        Me.txtPriceGrade1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPriceGrade2 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.grpBoxTerms = New System.Windows.Forms.GroupBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtAccountNo = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtBSB2 = New System.Windows.Forms.TextBox()
        Me.txtBSB1 = New System.Windows.Forms.TextBox()
        Me.txtAccountName = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.btnSaveTerms = New System.Windows.Forms.Button()
        Me.txtPOS_Terms = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.panelLabourItems = New System.Windows.Forms.Panel()
        Me.txtLabourDescr_pr3 = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.labLabourPrice_pr2 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.labLabourPrice_pr3 = New System.Windows.Forms.Label()
        Me.txtLabourDescr_pr2 = New System.Windows.Forms.TextBox()
        Me.btnSaveLabour_pr1 = New System.Windows.Forms.Button()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.labLabourPrice_pr1 = New System.Windows.Forms.Label()
        Me.txtLabourDescr_pr1 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.btnSaveFontSize = New System.Windows.Forms.Button()
        Me.txtStockBarcodeFontSize = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnSaveFontName = New System.Windows.Forms.Button()
        Me.txtStockBarcodeFontName = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnSaveGST = New System.Windows.Forms.Button()
        Me.txtGST_percentage = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSaveMargin = New System.Windows.Forms.Button()
        Me.txtSellMargin = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.TabPageMailSetup = New System.Windows.Forms.TabPage()
        Me.grpBoxEmailOptions = New System.Windows.Forms.GroupBox()
        Me.chkAllowEmailStatements = New System.Windows.Forms.CheckBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.chkAllowEmailInvoices = New System.Windows.Forms.CheckBox()
        Me.btnSaveEmailTexts = New System.Windows.Forms.Button()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.txtEmailTextPO = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtEmailTextStatement = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtEmailTextInvoice = New System.Windows.Forms.TextBox()
        Me.frameSMTPSettings = New System.Windows.Forms.GroupBox()
        Me.labJobTrackingSharing = New System.Windows.Forms.Label()
        Me.btnSaveSMTP = New System.Windows.Forms.Button()
        Me.chkHostUsesSSL = New System.Windows.Forms.CheckBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtSMTPHostPort = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtSMTPServer = New System.Windows.Forms.TextBox()
        Me.txtSMTPUsername = New System.Windows.Forms.TextBox()
        Me.txtSMTPPassword1 = New System.Windows.Forms.TextBox()
        Me.txtSMTPPassword2 = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.labSMTPConfirm = New System.Windows.Forms.Label()
        Me.TabPageServerShare = New System.Windows.Forms.TabPage()
        Me.frameBackupPath = New System.Windows.Forms.GroupBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.cmdSaveServerPath = New System.Windows.Forms.Button()
        Me.cmdBrowseServerPath = New System.Windows.Forms.Button()
        Me.txtServerBackupFolderNetworkPath = New System.Windows.Forms.TextBox()
        Me.txtServerBackupFolderLocal = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.LabHelpBackupPath = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.labDLLversion = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.grpBoxAdmin.SuspendLayout()
        Me.ShapedPanelAdminHdr.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageOptions.SuspendLayout()
        Me.panelPOS_Setup.SuspendLayout()
        Me.panelPricingGrades.SuspendLayout()
        Me.grpBoxTerms.SuspendLayout()
        Me.panelLabourItems.SuspendLayout()
        Me.TabPageMailSetup.SuspendLayout()
        Me.grpBoxEmailOptions.SuspendLayout()
        Me.frameSMTPSettings.SuspendLayout()
        Me.TabPageServerShare.SuspendLayout()
        Me.frameBackupPath.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpBoxAdmin
        '
        Me.grpBoxAdmin.BackColor = System.Drawing.Color.White
        Me.grpBoxAdmin.Controls.Add(Me.ShapedPanelAdminHdr)
        Me.grpBoxAdmin.Controls.Add(Me.TabControl1)
        Me.grpBoxAdmin.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxAdmin.Location = New System.Drawing.Point(6, 3)
        Me.grpBoxAdmin.Name = "grpBoxAdmin"
        Me.grpBoxAdmin.Size = New System.Drawing.Size(887, 627)
        Me.grpBoxAdmin.TabIndex = 0
        Me.grpBoxAdmin.TabStop = False
        Me.grpBoxAdmin.Text = "grpBoxAdmin"
        '
        'ShapedPanelAdminHdr
        '
        Me.ShapedPanelAdminHdr.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.ShapedPanelAdminHdr.BorderColor = System.Drawing.Color.Khaki
        Me.ShapedPanelAdminHdr.Controls.Add(Me.Label53)
        Me.ShapedPanelAdminHdr.Controls.Add(Me.cboReceiptPrinters)
        Me.ShapedPanelAdminHdr.Controls.Add(Me.btnExit)
        Me.ShapedPanelAdminHdr.Controls.Add(Me.labStaffName)
        Me.ShapedPanelAdminHdr.Controls.Add(Me.cboReportPrinters)
        Me.ShapedPanelAdminHdr.Controls.Add(Me.Label13)
        Me.ShapedPanelAdminHdr.Controls.Add(Me.Label52)
        Me.ShapedPanelAdminHdr.Controls.Add(Me.Label5)
        Me.ShapedPanelAdminHdr.Edge = 20
        Me.ShapedPanelAdminHdr.Location = New System.Drawing.Point(6, 9)
        Me.ShapedPanelAdminHdr.Name = "ShapedPanelAdminHdr"
        Me.ShapedPanelAdminHdr.Size = New System.Drawing.Size(871, 56)
        Me.ShapedPanelAdminHdr.TabIndex = 8
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(634, 10)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(78, 13)
        Me.Label53.TabIndex = 61
        Me.Label53.Text = "Receipt Printer"
        '
        'cboReceiptPrinters
        '
        Me.cboReceiptPrinters.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cboReceiptPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReceiptPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReceiptPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReceiptPrinters.FormattingEnabled = True
        Me.cboReceiptPrinters.Location = New System.Drawing.Point(626, 26)
        Me.cboReceiptPrinters.Name = "cboReceiptPrinters"
        Me.cboReceiptPrinters.Size = New System.Drawing.Size(129, 21)
        Me.cboReceiptPrinters.TabIndex = 60
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnExit.CausesValidation = False
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.Location = New System.Drawing.Point(788, 15)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(61, 27)
        Me.btnExit.TabIndex = 58
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'labStaffName
        '
        Me.labStaffName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStaffName.Location = New System.Drawing.Point(287, 29)
        Me.labStaffName.Name = "labStaffName"
        Me.labStaffName.Size = New System.Drawing.Size(125, 13)
        Me.labStaffName.TabIndex = 6
        Me.labStaffName.Text = "labStaffName"
        '
        'cboReportPrinters
        '
        Me.cboReportPrinters.BackColor = System.Drawing.Color.Lavender
        Me.cboReportPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReportPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboReportPrinters.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReportPrinters.FormattingEnabled = True
        Me.cboReportPrinters.Location = New System.Drawing.Point(451, 27)
        Me.cboReportPrinters.Name = "cboReportPrinters"
        Me.cboReportPrinters.Size = New System.Drawing.Size(151, 21)
        Me.cboReportPrinters.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(287, 10)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 22)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "Staff: "
        '
        'Label52
        '
        Me.Label52.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(449, 13)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(108, 13)
        Me.Label52.TabIndex = 57
        Me.Label52.Text = "Report Printer :"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.Label5.Location = New System.Drawing.Point(25, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(244, 29)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "POS Setup and Options"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageOptions)
        Me.TabControl1.Controls.Add(Me.TabPageMailSetup)
        Me.TabControl1.Controls.Add(Me.TabPageServerShare)
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(2, 67)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(875, 554)
        Me.TabControl1.TabIndex = 7
        '
        'TabPageOptions
        '
        Me.TabPageOptions.Controls.Add(Me.panelPOS_Setup)
        Me.TabPageOptions.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageOptions.Location = New System.Drawing.Point(4, 25)
        Me.TabPageOptions.Name = "TabPageOptions"
        Me.TabPageOptions.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageOptions.Size = New System.Drawing.Size(867, 525)
        Me.TabPageOptions.TabIndex = 1
        Me.TabPageOptions.Text = "POS Options/Setup"
        Me.TabPageOptions.UseVisualStyleBackColor = True
        '
        'panelPOS_Setup
        '
        Me.panelPOS_Setup.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.panelPOS_Setup.Controls.Add(Me.panelPricingGrades)
        Me.panelPOS_Setup.Controls.Add(Me.Label4)
        Me.panelPOS_Setup.Controls.Add(Me.Label34)
        Me.panelPOS_Setup.Controls.Add(Me.grpBoxTerms)
        Me.panelPOS_Setup.Controls.Add(Me.Label24)
        Me.panelPOS_Setup.Controls.Add(Me.panelLabourItems)
        Me.panelPOS_Setup.Controls.Add(Me.Label18)
        Me.panelPOS_Setup.Controls.Add(Me.Label17)
        Me.panelPOS_Setup.Controls.Add(Me.btnSaveFontSize)
        Me.panelPOS_Setup.Controls.Add(Me.txtStockBarcodeFontSize)
        Me.panelPOS_Setup.Controls.Add(Me.Label16)
        Me.panelPOS_Setup.Controls.Add(Me.btnSaveFontName)
        Me.panelPOS_Setup.Controls.Add(Me.txtStockBarcodeFontName)
        Me.panelPOS_Setup.Controls.Add(Me.Label15)
        Me.panelPOS_Setup.Controls.Add(Me.Label14)
        Me.panelPOS_Setup.Controls.Add(Me.btnSaveGST)
        Me.panelPOS_Setup.Controls.Add(Me.txtGST_percentage)
        Me.panelPOS_Setup.Controls.Add(Me.Label2)
        Me.panelPOS_Setup.Controls.Add(Me.btnSaveMargin)
        Me.panelPOS_Setup.Controls.Add(Me.txtSellMargin)
        Me.panelPOS_Setup.Controls.Add(Me.Label1)
        Me.panelPOS_Setup.Controls.Add(Me.Label29)
        Me.panelPOS_Setup.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelPOS_Setup.Location = New System.Drawing.Point(3, 3)
        Me.panelPOS_Setup.Name = "panelPOS_Setup"
        Me.panelPOS_Setup.Size = New System.Drawing.Size(854, 516)
        Me.panelPOS_Setup.TabIndex = 0
        '
        'panelPricingGrades
        '
        Me.panelPricingGrades.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelPricingGrades.Controls.Add(Me.btnSaveGrades)
        Me.panelPricingGrades.Controls.Add(Me.Label46)
        Me.panelPricingGrades.Controls.Add(Me.Label47)
        Me.panelPricingGrades.Controls.Add(Me.txtPriceGrade4)
        Me.panelPricingGrades.Controls.Add(Me.Label25)
        Me.panelPricingGrades.Controls.Add(Me.Label32)
        Me.panelPricingGrades.Controls.Add(Me.txtPriceGrade3)
        Me.panelPricingGrades.Controls.Add(Me.txtPriceGrade1)
        Me.panelPricingGrades.Controls.Add(Me.Label7)
        Me.panelPricingGrades.Controls.Add(Me.Label11)
        Me.panelPricingGrades.Controls.Add(Me.Label6)
        Me.panelPricingGrades.Controls.Add(Me.Label8)
        Me.panelPricingGrades.Controls.Add(Me.Label12)
        Me.panelPricingGrades.Controls.Add(Me.Label10)
        Me.panelPricingGrades.Controls.Add(Me.txtPriceGrade2)
        Me.panelPricingGrades.Location = New System.Drawing.Point(19, 152)
        Me.panelPricingGrades.Name = "panelPricingGrades"
        Me.panelPricingGrades.Size = New System.Drawing.Size(390, 107)
        Me.panelPricingGrades.TabIndex = 4
        '
        'btnSaveGrades
        '
        Me.btnSaveGrades.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveGrades.Location = New System.Drawing.Point(326, 65)
        Me.btnSaveGrades.Name = "btnSaveGrades"
        Me.btnSaveGrades.Size = New System.Drawing.Size(51, 21)
        Me.btnSaveGrades.TabIndex = 4
        Me.btnSaveGrades.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveGrades, "Save GST Percentage")
        Me.btnSaveGrades.UseVisualStyleBackColor = False
        '
        'Label46
        '
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(301, 72)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(22, 14)
        Me.Label46.TabIndex = 56
        Me.Label46.Text = "%"
        '
        'Label47
        '
        Me.Label47.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(169, 70)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(66, 14)
        Me.Label47.TabIndex = 55
        Me.Label47.Text = "D. Grade-4:"
        '
        'txtPriceGrade4
        '
        Me.txtPriceGrade4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceGrade4.Location = New System.Drawing.Point(238, 69)
        Me.txtPriceGrade4.Name = "txtPriceGrade4"
        Me.txtPriceGrade4.Size = New System.Drawing.Size(59, 21)
        Me.txtPriceGrade4.TabIndex = 3
        Me.txtPriceGrade4.Text = "txtPriceGrade4"
        Me.txtPriceGrade4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(301, 39)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(22, 14)
        Me.Label25.TabIndex = 53
        Me.Label25.Text = "%"
        '
        'Label32
        '
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(169, 39)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(66, 14)
        Me.Label32.TabIndex = 52
        Me.Label32.Text = "C. Grade-3:"
        '
        'txtPriceGrade3
        '
        Me.txtPriceGrade3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceGrade3.Location = New System.Drawing.Point(238, 36)
        Me.txtPriceGrade3.Name = "txtPriceGrade3"
        Me.txtPriceGrade3.Size = New System.Drawing.Size(59, 21)
        Me.txtPriceGrade3.TabIndex = 2
        Me.txtPriceGrade3.Text = "txtPriceGrade3"
        Me.txtPriceGrade3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPriceGrade1
        '
        Me.txtPriceGrade1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceGrade1.Location = New System.Drawing.Point(80, 36)
        Me.txtPriceGrade1.Name = "txtPriceGrade1"
        Me.txtPriceGrade1.Size = New System.Drawing.Size(56, 21)
        Me.txtPriceGrade1.TabIndex = 0
        Me.txtPriceGrade1.Text = "txtPriceGrade1"
        Me.txtPriceGrade1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(236, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 14)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "(Grade-0 =RRP (default))  "
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(141, 72)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(20, 14)
        Me.Label11.TabIndex = 50
        Me.Label11.Text = "%"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(221, 14)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Customer Pricing Grades (Cost+ x%)"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(9, 39)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(66, 14)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "A. Grade-1:"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(9, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(66, 14)
        Me.Label12.TabIndex = 49
        Me.Label12.Text = "B. Grade-2:"
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(141, 39)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 14)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "%"
        '
        'txtPriceGrade2
        '
        Me.txtPriceGrade2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPriceGrade2.Location = New System.Drawing.Point(80, 70)
        Me.txtPriceGrade2.Name = "txtPriceGrade2"
        Me.txtPriceGrade2.Size = New System.Drawing.Size(59, 21)
        Me.txtPriceGrade2.TabIndex = 1
        Me.txtPriceGrade2.Text = "txtPriceGrade2"
        Me.txtPriceGrade2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Gainsboro
        Me.Label4.Location = New System.Drawing.Point(16, 366)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(393, 3)
        Me.Label4.TabIndex = 42
        '
        'Label34
        '
        Me.Label34.Location = New System.Drawing.Point(33, 98)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(154, 30)
        Me.Label34.TabIndex = 41
        Me.Label34.Text = "(Applied to Cost Ex Tax to get  SellPrice ExTax.)"
        '
        'grpBoxTerms
        '
        Me.grpBoxTerms.Controls.Add(Me.Label33)
        Me.grpBoxTerms.Controls.Add(Me.txtAccountNo)
        Me.grpBoxTerms.Controls.Add(Me.Label30)
        Me.grpBoxTerms.Controls.Add(Me.txtBSB2)
        Me.grpBoxTerms.Controls.Add(Me.txtBSB1)
        Me.grpBoxTerms.Controls.Add(Me.txtAccountName)
        Me.grpBoxTerms.Controls.Add(Me.txtBankName)
        Me.grpBoxTerms.Controls.Add(Me.Label28)
        Me.grpBoxTerms.Controls.Add(Me.Label27)
        Me.grpBoxTerms.Controls.Add(Me.Label31)
        Me.grpBoxTerms.Controls.Add(Me.btnSaveTerms)
        Me.grpBoxTerms.Controls.Add(Me.txtPOS_Terms)
        Me.grpBoxTerms.Controls.Add(Me.Label22)
        Me.grpBoxTerms.Controls.Add(Me.Label3)
        Me.grpBoxTerms.Controls.Add(Me.Label9)
        Me.grpBoxTerms.Location = New System.Drawing.Point(456, 47)
        Me.grpBoxTerms.Name = "grpBoxTerms"
        Me.grpBoxTerms.Size = New System.Drawing.Size(380, 457)
        Me.grpBoxTerms.TabIndex = 10
        Me.grpBoxTerms.TabStop = False
        Me.grpBoxTerms.Text = "POS Account Terms"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(24, 335)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(331, 14)
        Me.Label33.TabIndex = 48
        Me.Label33.Text = "Note: ALL Terms and Account info fields must have values."
        '
        'txtAccountNo
        '
        Me.txtAccountNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountNo.Location = New System.Drawing.Point(124, 303)
        Me.txtAccountNo.Name = "txtAccountNo"
        Me.txtAccountNo.Size = New System.Drawing.Size(170, 21)
        Me.txtAccountNo.TabIndex = 46
        Me.txtAccountNo.Text = "txtAccountNo"
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(170, 271)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(12, 19)
        Me.Label30.TabIndex = 37
        Me.Label30.Text = "-"
        '
        'txtBSB2
        '
        Me.txtBSB2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBSB2.Location = New System.Drawing.Point(186, 270)
        Me.txtBSB2.MaxLength = 3
        Me.txtBSB2.Name = "txtBSB2"
        Me.txtBSB2.Size = New System.Drawing.Size(39, 21)
        Me.txtBSB2.TabIndex = 45
        Me.txtBSB2.Text = "txtBSB2"
        '
        'txtBSB1
        '
        Me.txtBSB1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBSB1.Location = New System.Drawing.Point(127, 270)
        Me.txtBSB1.MaxLength = 3
        Me.txtBSB1.Name = "txtBSB1"
        Me.txtBSB1.Size = New System.Drawing.Size(37, 20)
        Me.txtBSB1.TabIndex = 44
        Me.txtBSB1.Text = "txtBSB1"
        '
        'txtAccountName
        '
        Me.txtAccountName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountName.Location = New System.Drawing.Point(112, 233)
        Me.txtAccountName.Name = "txtAccountName"
        Me.txtAccountName.Size = New System.Drawing.Size(253, 21)
        Me.txtAccountName.TabIndex = 43
        Me.txtAccountName.Text = "txtAccountName"
        '
        'txtBankName
        '
        Me.txtBankName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.Location = New System.Drawing.Point(112, 202)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.Size = New System.Drawing.Size(253, 21)
        Me.txtBankName.TabIndex = 42
        Me.txtBankName.Text = "txtBankName"
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(59, 272)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(59, 13)
        Me.Label28.TabIndex = 32
        Me.Label28.Text = "BSB:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(24, 303)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(94, 13)
        Me.Label27.TabIndex = 31
        Me.Label27.Text = "Account Number:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(24, 38)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(115, 13)
        Me.Label31.TabIndex = 2
        Me.Label31.Text = "Account Terms"
        '
        'btnSaveTerms
        '
        Me.btnSaveTerms.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveTerms.Location = New System.Drawing.Point(291, 363)
        Me.btnSaveTerms.Name = "btnSaveTerms"
        Me.btnSaveTerms.Size = New System.Drawing.Size(74, 21)
        Me.btnSaveTerms.TabIndex = 47
        Me.btnSaveTerms.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveTerms, "Save Account Terms text.")
        Me.btnSaveTerms.UseVisualStyleBackColor = False
        '
        'txtPOS_Terms
        '
        Me.txtPOS_Terms.Location = New System.Drawing.Point(27, 63)
        Me.txtPOS_Terms.Multiline = True
        Me.txtPOS_Terms.Name = "txtPOS_Terms"
        Me.txtPOS_Terms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPOS_Terms.Size = New System.Drawing.Size(274, 75)
        Me.txtPOS_Terms.TabIndex = 41
        Me.txtPOS_Terms.Text = "txtPOS_Terms"
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(24, 233)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(82, 13)
        Me.Label22.TabIndex = 30
        Me.Label22.Text = "Account Name: "
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 175)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(289, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Business Bank Account Details for Debtor Remittances"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(24, 204)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 13)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Bank Name: "
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Gainsboro
        Me.Label24.Location = New System.Drawing.Point(431, 39)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(10, 465)
        Me.Label24.TabIndex = 31
        '
        'panelLabourItems
        '
        Me.panelLabourItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.panelLabourItems.Controls.Add(Me.txtLabourDescr_pr3)
        Me.panelLabourItems.Controls.Add(Me.Label20)
        Me.panelLabourItems.Controls.Add(Me.labLabourPrice_pr2)
        Me.panelLabourItems.Controls.Add(Me.Label19)
        Me.panelLabourItems.Controls.Add(Me.labLabourPrice_pr3)
        Me.panelLabourItems.Controls.Add(Me.txtLabourDescr_pr2)
        Me.panelLabourItems.Controls.Add(Me.btnSaveLabour_pr1)
        Me.panelLabourItems.Controls.Add(Me.Label23)
        Me.panelLabourItems.Controls.Add(Me.Label26)
        Me.panelLabourItems.Controls.Add(Me.labLabourPrice_pr1)
        Me.panelLabourItems.Controls.Add(Me.txtLabourDescr_pr1)
        Me.panelLabourItems.Controls.Add(Me.Label21)
        Me.panelLabourItems.Enabled = False
        Me.panelLabourItems.Location = New System.Drawing.Point(19, 378)
        Me.panelLabourItems.Name = "panelLabourItems"
        Me.panelLabourItems.Size = New System.Drawing.Size(390, 128)
        Me.panelLabourItems.TabIndex = 9
        '
        'txtLabourDescr_pr3
        '
        Me.txtLabourDescr_pr3.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtLabourDescr_pr3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabourDescr_pr3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabourDescr_pr3.Location = New System.Drawing.Point(68, 112)
        Me.txtLabourDescr_pr3.Name = "txtLabourDescr_pr3"
        Me.txtLabourDescr_pr3.ReadOnly = True
        Me.txtLabourDescr_pr3.Size = New System.Drawing.Size(63, 14)
        Me.txtLabourDescr_pr3.TabIndex = 31
        Me.txtLabourDescr_pr3.Text = "txtLabourDescr_pr3"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(11, 44)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(235, 17)
        Me.Label20.TabIndex = 31
        Me.Label20.Text = "Stock Id's for Labour- Priorities 1,2,3:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'labLabourPrice_pr2
        '
        Me.labLabourPrice_pr2.BackColor = System.Drawing.Color.MistyRose
        Me.labLabourPrice_pr2.Location = New System.Drawing.Point(161, 88)
        Me.labLabourPrice_pr2.Name = "labLabourPrice_pr2"
        Me.labLabourPrice_pr2.Size = New System.Drawing.Size(56, 17)
        Me.labLabourPrice_pr2.TabIndex = 30
        Me.labLabourPrice_pr2.Text = "labLabourPrice_pr2"
        Me.labLabourPrice_pr2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.labLabourPrice_pr2, "Hourly rate Priority-2-  Click to Change.")
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(11, 12)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(266, 45)
        Me.Label19.TabIndex = 30
        Me.Label19.Text = "Jobs Labour Charges as POS Stock Items-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "THESE NOW DONE IN JOB-TRACKING !!"
        '
        'labLabourPrice_pr3
        '
        Me.labLabourPrice_pr3.BackColor = System.Drawing.Color.MistyRose
        Me.labLabourPrice_pr3.Location = New System.Drawing.Point(161, 110)
        Me.labLabourPrice_pr3.Name = "labLabourPrice_pr3"
        Me.labLabourPrice_pr3.Size = New System.Drawing.Size(56, 17)
        Me.labLabourPrice_pr3.TabIndex = 32
        Me.labLabourPrice_pr3.Text = "labLabourPrice_pr3"
        Me.labLabourPrice_pr3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.labLabourPrice_pr3, "Hourly rate Priority-3-  Click to Change.")
        '
        'txtLabourDescr_pr2
        '
        Me.txtLabourDescr_pr2.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtLabourDescr_pr2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabourDescr_pr2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabourDescr_pr2.Location = New System.Drawing.Point(76, 92)
        Me.txtLabourDescr_pr2.Name = "txtLabourDescr_pr2"
        Me.txtLabourDescr_pr2.ReadOnly = True
        Me.txtLabourDescr_pr2.Size = New System.Drawing.Size(63, 14)
        Me.txtLabourDescr_pr2.TabIndex = 29
        Me.txtLabourDescr_pr2.Text = "txtLabourDescr_pr2"
        '
        'btnSaveLabour_pr1
        '
        Me.btnSaveLabour_pr1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveLabour_pr1.Location = New System.Drawing.Point(225, 103)
        Me.btnSaveLabour_pr1.Name = "btnSaveLabour_pr1"
        Me.btnSaveLabour_pr1.Size = New System.Drawing.Size(49, 21)
        Me.btnSaveLabour_pr1.TabIndex = 33
        Me.btnSaveLabour_pr1.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveLabour_pr1, "Save Font Size")
        Me.btnSaveLabour_pr1.UseVisualStyleBackColor = False
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(17, 92)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(53, 17)
        Me.Label23.TabIndex = 38
        Me.Label23.Text = "Priority-2:"
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(17, 109)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(53, 17)
        Me.Label26.TabIndex = 41
        Me.Label26.Text = "Priority-3:"
        '
        'labLabourPrice_pr1
        '
        Me.labLabourPrice_pr1.BackColor = System.Drawing.Color.MistyRose
        Me.labLabourPrice_pr1.Location = New System.Drawing.Point(161, 69)
        Me.labLabourPrice_pr1.Name = "labLabourPrice_pr1"
        Me.labLabourPrice_pr1.Size = New System.Drawing.Size(56, 17)
        Me.labLabourPrice_pr1.TabIndex = 28
        Me.labLabourPrice_pr1.Text = "labLabourPrice_pr1"
        Me.labLabourPrice_pr1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.labLabourPrice_pr1, "Hourly rate Priority-1-  Click to Change.")
        '
        'txtLabourDescr_pr1
        '
        Me.txtLabourDescr_pr1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtLabourDescr_pr1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLabourDescr_pr1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLabourDescr_pr1.Location = New System.Drawing.Point(76, 71)
        Me.txtLabourDescr_pr1.Name = "txtLabourDescr_pr1"
        Me.txtLabourDescr_pr1.ReadOnly = True
        Me.txtLabourDescr_pr1.Size = New System.Drawing.Size(63, 14)
        Me.txtLabourDescr_pr1.TabIndex = 27
        Me.txtLabourDescr_pr1.Text = "txtLabourDescr_pr1"
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(17, 71)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(53, 17)
        Me.Label21.TabIndex = 32
        Me.Label21.Text = "Priority-1:"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(24, 282)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(70, 13)
        Me.Label18.TabIndex = 28
        Me.Label18.Text = "Stock Labels-"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Gainsboro
        Me.Label17.Location = New System.Drawing.Point(17, 262)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(393, 3)
        Me.Label17.TabIndex = 27
        '
        'btnSaveFontSize
        '
        Me.btnSaveFontSize.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveFontSize.Location = New System.Drawing.Point(268, 332)
        Me.btnSaveFontSize.Name = "btnSaveFontSize"
        Me.btnSaveFontSize.Size = New System.Drawing.Size(51, 21)
        Me.btnSaveFontSize.TabIndex = 8
        Me.btnSaveFontSize.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveFontSize, "Save Font Size")
        Me.btnSaveFontSize.UseVisualStyleBackColor = False
        '
        'txtStockBarcodeFontSize
        '
        Me.txtStockBarcodeFontSize.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockBarcodeFontSize.Location = New System.Drawing.Point(165, 333)
        Me.txtStockBarcodeFontSize.Name = "txtStockBarcodeFontSize"
        Me.txtStockBarcodeFontSize.Size = New System.Drawing.Size(59, 21)
        Me.txtStockBarcodeFontSize.TabIndex = 7
        Me.txtStockBarcodeFontSize.Text = "txtStockBarcodeFontSize"
        Me.txtStockBarcodeFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(24, 336)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(135, 21)
        Me.Label16.TabIndex = 24
        Me.Label16.Text = "Stock Barcode FontSize:"
        '
        'btnSaveFontName
        '
        Me.btnSaveFontName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveFontName.Location = New System.Drawing.Point(356, 296)
        Me.btnSaveFontName.Name = "btnSaveFontName"
        Me.btnSaveFontName.Size = New System.Drawing.Size(51, 21)
        Me.btnSaveFontName.TabIndex = 6
        Me.btnSaveFontName.Text = "Save "
        Me.ToolTip1.SetToolTip(Me.btnSaveFontName, "Save Font Name")
        Me.btnSaveFontName.UseVisualStyleBackColor = False
        '
        'txtStockBarcodeFontName
        '
        Me.txtStockBarcodeFontName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockBarcodeFontName.Location = New System.Drawing.Point(165, 298)
        Me.txtStockBarcodeFontName.Name = "txtStockBarcodeFontName"
        Me.txtStockBarcodeFontName.Size = New System.Drawing.Size(154, 20)
        Me.txtStockBarcodeFontName.TabIndex = 5
        Me.txtStockBarcodeFontName.Text = "txtStockBarcodeFontName"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(24, 301)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(135, 21)
        Me.Label15.TabIndex = 21
        Me.Label15.Text = "Stock Barcode FontName:"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Gainsboro
        Me.Label14.Location = New System.Drawing.Point(28, 132)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(382, 3)
        Me.Label14.TabIndex = 20
        '
        'btnSaveGST
        '
        Me.btnSaveGST.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveGST.Location = New System.Drawing.Point(356, 71)
        Me.btnSaveGST.Name = "btnSaveGST"
        Me.btnSaveGST.Size = New System.Drawing.Size(51, 21)
        Me.btnSaveGST.TabIndex = 3
        Me.btnSaveGST.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveGST, "Save GST Percentage")
        Me.btnSaveGST.UseVisualStyleBackColor = False
        '
        'txtGST_percentage
        '
        Me.txtGST_percentage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGST_percentage.Location = New System.Drawing.Point(272, 70)
        Me.txtGST_percentage.Name = "txtGST_percentage"
        Me.txtGST_percentage.Size = New System.Drawing.Size(59, 21)
        Me.txtGST_percentage.TabIndex = 2
        Me.txtGST_percentage.Text = "txtGST_percentage"
        Me.txtGST_percentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(226, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 14)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "GST Percentage (%):  "
        '
        'btnSaveMargin
        '
        Me.btnSaveMargin.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveMargin.Location = New System.Drawing.Point(147, 71)
        Me.btnSaveMargin.Name = "btnSaveMargin"
        Me.btnSaveMargin.Size = New System.Drawing.Size(49, 21)
        Me.btnSaveMargin.TabIndex = 1
        Me.btnSaveMargin.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveMargin, "Save Sell Margin.")
        Me.btnSaveMargin.UseVisualStyleBackColor = False
        '
        'txtSellMargin
        '
        Me.txtSellMargin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSellMargin.Location = New System.Drawing.Point(77, 72)
        Me.txtSellMargin.Name = "txtSellMargin"
        Me.txtSellMargin.Size = New System.Drawing.Size(59, 21)
        Me.txtSellMargin.TabIndex = 0
        Me.txtSellMargin.Text = "txtSellMargin"
        Me.txtSellMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(27, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "RRP Selling Margin (%):  "
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(17, 14)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(128, 14)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "POS Options/Setup"
        '
        'TabPageMailSetup
        '
        Me.TabPageMailSetup.BackColor = System.Drawing.Color.White
        Me.TabPageMailSetup.Controls.Add(Me.grpBoxEmailOptions)
        Me.TabPageMailSetup.Controls.Add(Me.frameSMTPSettings)
        Me.TabPageMailSetup.Location = New System.Drawing.Point(4, 25)
        Me.TabPageMailSetup.Name = "TabPageMailSetup"
        Me.TabPageMailSetup.Size = New System.Drawing.Size(867, 525)
        Me.TabPageMailSetup.TabIndex = 2
        Me.TabPageMailSetup.Text = "POS Mail Setup"
        '
        'grpBoxEmailOptions
        '
        Me.grpBoxEmailOptions.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.grpBoxEmailOptions.Controls.Add(Me.chkAllowEmailStatements)
        Me.grpBoxEmailOptions.Controls.Add(Me.Label48)
        Me.grpBoxEmailOptions.Controls.Add(Me.chkAllowEmailInvoices)
        Me.grpBoxEmailOptions.Controls.Add(Me.btnSaveEmailTexts)
        Me.grpBoxEmailOptions.Controls.Add(Me.Label45)
        Me.grpBoxEmailOptions.Controls.Add(Me.txtEmailTextPO)
        Me.grpBoxEmailOptions.Controls.Add(Me.Label44)
        Me.grpBoxEmailOptions.Controls.Add(Me.txtEmailTextStatement)
        Me.grpBoxEmailOptions.Controls.Add(Me.Label43)
        Me.grpBoxEmailOptions.Controls.Add(Me.Label42)
        Me.grpBoxEmailOptions.Controls.Add(Me.txtEmailTextInvoice)
        Me.grpBoxEmailOptions.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxEmailOptions.Location = New System.Drawing.Point(451, 13)
        Me.grpBoxEmailOptions.Name = "grpBoxEmailOptions"
        Me.grpBoxEmailOptions.Size = New System.Drawing.Size(398, 494)
        Me.grpBoxEmailOptions.TabIndex = 6
        Me.grpBoxEmailOptions.TabStop = False
        Me.grpBoxEmailOptions.Text = "Email text Options"
        '
        'chkAllowEmailStatements
        '
        Me.chkAllowEmailStatements.Location = New System.Drawing.Point(191, 46)
        Me.chkAllowEmailStatements.Name = "chkAllowEmailStatements"
        Me.chkAllowEmailStatements.Size = New System.Drawing.Size(176, 22)
        Me.chkAllowEmailStatements.TabIndex = 1
        Me.chkAllowEmailStatements.Text = "Allow Emailing Statements"
        Me.chkAllowEmailStatements.UseVisualStyleBackColor = True
        '
        'Label48
        '
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(16, 18)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(169, 13)
        Me.Label48.TabIndex = 51
        Me.Label48.Text = "Email Options and Texts"
        '
        'chkAllowEmailInvoices
        '
        Me.chkAllowEmailInvoices.Location = New System.Drawing.Point(20, 46)
        Me.chkAllowEmailInvoices.Name = "chkAllowEmailInvoices"
        Me.chkAllowEmailInvoices.Size = New System.Drawing.Size(155, 22)
        Me.chkAllowEmailInvoices.TabIndex = 0
        Me.chkAllowEmailInvoices.Text = "Allow Emailing Invoices"
        Me.chkAllowEmailInvoices.UseVisualStyleBackColor = True
        '
        'btnSaveEmailTexts
        '
        Me.btnSaveEmailTexts.Location = New System.Drawing.Point(306, 446)
        Me.btnSaveEmailTexts.Name = "btnSaveEmailTexts"
        Me.btnSaveEmailTexts.Size = New System.Drawing.Size(61, 30)
        Me.btnSaveEmailTexts.TabIndex = 49
        Me.btnSaveEmailTexts.Text = "Save"
        Me.btnSaveEmailTexts.UseVisualStyleBackColor = True
        '
        'Label45
        '
        Me.Label45.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(16, 333)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(134, 13)
        Me.Label45.TabIndex = 47
        Me.Label45.Text = "Purchase Order"
        '
        'txtEmailTextPO
        '
        Me.txtEmailTextPO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailTextPO.Location = New System.Drawing.Point(19, 353)
        Me.txtEmailTextPO.Multiline = True
        Me.txtEmailTextPO.Name = "txtEmailTextPO"
        Me.txtEmailTextPO.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEmailTextPO.Size = New System.Drawing.Size(348, 71)
        Me.txtEmailTextPO.TabIndex = 48
        Me.txtEmailTextPO.Text = "txtEmailTextPO"
        '
        'Label44
        '
        Me.Label44.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(18, 220)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(134, 13)
        Me.Label44.TabIndex = 45
        Me.Label44.Text = "Customer Statement"
        '
        'txtEmailTextStatement
        '
        Me.txtEmailTextStatement.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailTextStatement.Location = New System.Drawing.Point(21, 241)
        Me.txtEmailTextStatement.Multiline = True
        Me.txtEmailTextStatement.Name = "txtEmailTextStatement"
        Me.txtEmailTextStatement.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEmailTextStatement.Size = New System.Drawing.Size(348, 71)
        Me.txtEmailTextStatement.TabIndex = 46
        Me.txtEmailTextStatement.Text = "txtEmailTextStatement"
        '
        'Label43
        '
        Me.Label43.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(16, 84)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(94, 13)
        Me.Label43.TabIndex = 44
        Me.Label43.Text = "Email Texts"
        '
        'Label42
        '
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(16, 110)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(109, 13)
        Me.Label42.TabIndex = 42
        Me.Label42.Text = "Customer Invoice"
        '
        'txtEmailTextInvoice
        '
        Me.txtEmailTextInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailTextInvoice.Location = New System.Drawing.Point(19, 133)
        Me.txtEmailTextInvoice.Multiline = True
        Me.txtEmailTextInvoice.Name = "txtEmailTextInvoice"
        Me.txtEmailTextInvoice.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtEmailTextInvoice.Size = New System.Drawing.Size(348, 71)
        Me.txtEmailTextInvoice.TabIndex = 43
        Me.txtEmailTextInvoice.Text = "txtEmailTextInvoice"
        '
        'frameSMTPSettings
        '
        Me.frameSMTPSettings.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameSMTPSettings.Controls.Add(Me.labJobTrackingSharing)
        Me.frameSMTPSettings.Controls.Add(Me.btnSaveSMTP)
        Me.frameSMTPSettings.Controls.Add(Me.chkHostUsesSSL)
        Me.frameSMTPSettings.Controls.Add(Me.Label35)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPHostPort)
        Me.frameSMTPSettings.Controls.Add(Me.Label36)
        Me.frameSMTPSettings.Controls.Add(Me.Label37)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPServer)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPUsername)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPPassword1)
        Me.frameSMTPSettings.Controls.Add(Me.txtSMTPPassword2)
        Me.frameSMTPSettings.Controls.Add(Me.Label38)
        Me.frameSMTPSettings.Controls.Add(Me.Label39)
        Me.frameSMTPSettings.Controls.Add(Me.Label40)
        Me.frameSMTPSettings.Controls.Add(Me.Label41)
        Me.frameSMTPSettings.Controls.Add(Me.labSMTPConfirm)
        Me.frameSMTPSettings.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameSMTPSettings.Location = New System.Drawing.Point(16, 13)
        Me.frameSMTPSettings.Name = "frameSMTPSettings"
        Me.frameSMTPSettings.Size = New System.Drawing.Size(401, 494)
        Me.frameSMTPSettings.TabIndex = 5
        Me.frameSMTPSettings.TabStop = False
        Me.frameSMTPSettings.Text = "frameSMTPSettings"
        '
        'labJobTrackingSharing
        '
        Me.labJobTrackingSharing.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJobTrackingSharing.ForeColor = System.Drawing.Color.DarkMagenta
        Me.labJobTrackingSharing.Location = New System.Drawing.Point(22, 105)
        Me.labJobTrackingSharing.Name = "labJobTrackingSharing"
        Me.labJobTrackingSharing.Size = New System.Drawing.Size(257, 32)
        Me.labJobTrackingSharing.TabIndex = 31
        Me.labJobTrackingSharing.Text = "Note: These settings are shared with the JobMatix Job-tracking SMTP mail Function" & _
    "s."
        '
        'btnSaveSMTP
        '
        Me.btnSaveSMTP.Location = New System.Drawing.Point(294, 446)
        Me.btnSaveSMTP.Name = "btnSaveSMTP"
        Me.btnSaveSMTP.Size = New System.Drawing.Size(61, 30)
        Me.btnSaveSMTP.TabIndex = 30
        Me.btnSaveSMTP.Text = "Save"
        Me.btnSaveSMTP.UseVisualStyleBackColor = True
        '
        'chkHostUsesSSL
        '
        Me.chkHostUsesSSL.Location = New System.Drawing.Point(132, 202)
        Me.chkHostUsesSSL.Name = "chkHostUsesSSL"
        Me.chkHostUsesSSL.Size = New System.Drawing.Size(130, 22)
        Me.chkHostUsesSSL.TabIndex = 23
        Me.chkHostUsesSSL.Text = "Host Uses SSL"
        Me.chkHostUsesSSL.UseVisualStyleBackColor = True
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(20, 204)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(34, 14)
        Me.Label35.TabIndex = 29
        Me.Label35.Text = "Port:"
        '
        'txtSMTPHostPort
        '
        Me.txtSMTPHostPort.AcceptsReturn = True
        Me.txtSMTPHostPort.BackColor = System.Drawing.Color.White
        Me.txtSMTPHostPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPHostPort.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPHostPort.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPHostPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPHostPort.Location = New System.Drawing.Point(60, 202)
        Me.txtSMTPHostPort.MaxLength = 0
        Me.txtSMTPHostPort.Multiline = True
        Me.txtSMTPHostPort.Name = "txtSMTPHostPort"
        Me.txtSMTPHostPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPHostPort.Size = New System.Drawing.Size(51, 25)
        Me.txtSMTPHostPort.TabIndex = 22
        '
        'Label36
        '
        Me.Label36.Location = New System.Drawing.Point(20, 55)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(335, 43)
        Me.Label36.TabIndex = 27
        Me.Label36.Text = "For sending Emails:  These settings identify the Mail Host name and mailbox crede" & _
    "ntials that your business uses as a Mail server. "
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(23, 33)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(135, 14)
        Me.Label37.TabIndex = 26
        Me.Label37.Text = "SMTP (Mail) Settings"
        '
        'txtSMTPServer
        '
        Me.txtSMTPServer.AcceptsReturn = True
        Me.txtSMTPServer.BackColor = System.Drawing.Color.White
        Me.txtSMTPServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPServer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPServer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPServer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPServer.Location = New System.Drawing.Point(23, 168)
        Me.txtSMTPServer.MaxLength = 0
        Me.txtSMTPServer.Multiline = True
        Me.txtSMTPServer.Name = "txtSMTPServer"
        Me.txtSMTPServer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPServer.Size = New System.Drawing.Size(333, 25)
        Me.txtSMTPServer.TabIndex = 21
        '
        'txtSMTPUsername
        '
        Me.txtSMTPUsername.AcceptsReturn = True
        Me.txtSMTPUsername.BackColor = System.Drawing.Color.White
        Me.txtSMTPUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPUsername.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPUsername.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPUsername.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPUsername.Location = New System.Drawing.Point(23, 299)
        Me.txtSMTPUsername.MaxLength = 64
        Me.txtSMTPUsername.Name = "txtSMTPUsername"
        Me.txtSMTPUsername.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPUsername.Size = New System.Drawing.Size(333, 21)
        Me.txtSMTPUsername.TabIndex = 24
        '
        'txtSMTPPassword1
        '
        Me.txtSMTPPassword1.AcceptsReturn = True
        Me.txtSMTPPassword1.BackColor = System.Drawing.Color.White
        Me.txtSMTPPassword1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPPassword1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPPassword1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPPassword1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPPassword1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSMTPPassword1.Location = New System.Drawing.Point(23, 357)
        Me.txtSMTPPassword1.MaxLength = 64
        Me.txtSMTPPassword1.Name = "txtSMTPPassword1"
        Me.txtSMTPPassword1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSMTPPassword1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPPassword1.Size = New System.Drawing.Size(211, 21)
        Me.txtSMTPPassword1.TabIndex = 25
        '
        'txtSMTPPassword2
        '
        Me.txtSMTPPassword2.AcceptsReturn = True
        Me.txtSMTPPassword2.BackColor = System.Drawing.Color.White
        Me.txtSMTPPassword2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSMTPPassword2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSMTPPassword2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSMTPPassword2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSMTPPassword2.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSMTPPassword2.Location = New System.Drawing.Point(23, 411)
        Me.txtSMTPPassword2.MaxLength = 64
        Me.txtSMTPPassword2.Name = "txtSMTPPassword2"
        Me.txtSMTPPassword2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSMTPPassword2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSMTPPassword2.Size = New System.Drawing.Size(211, 21)
        Me.txtSMTPPassword2.TabIndex = 26
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(20, 147)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(156, 18)
        Me.Label38.TabIndex = 25
        Me.Label38.Text = "SMTP (Mail) Host Name"
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(20, 260)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(113, 18)
        Me.Label39.TabIndex = 24
        Me.Label39.Text = "SMTP Credentials:"
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(23, 283)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(153, 17)
        Me.Label40.TabIndex = 23
        Me.Label40.Text = "SMTP (Mailbox) Username"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(20, 337)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(214, 17)
        Me.Label41.TabIndex = 22
        Me.Label41.Text = "SMTP (Mailbox) Password"
        '
        'labSMTPConfirm
        '
        Me.labSMTPConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labSMTPConfirm.Cursor = System.Windows.Forms.Cursors.Default
        Me.labSMTPConfirm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labSMTPConfirm.Location = New System.Drawing.Point(23, 395)
        Me.labSMTPConfirm.Name = "labSMTPConfirm"
        Me.labSMTPConfirm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labSMTPConfirm.Size = New System.Drawing.Size(211, 13)
        Me.labSMTPConfirm.TabIndex = 21
        Me.labSMTPConfirm.Text = "Confirm Password"
        '
        'TabPageServerShare
        '
        Me.TabPageServerShare.Controls.Add(Me.frameBackupPath)
        Me.TabPageServerShare.Location = New System.Drawing.Point(4, 25)
        Me.TabPageServerShare.Name = "TabPageServerShare"
        Me.TabPageServerShare.Size = New System.Drawing.Size(867, 525)
        Me.TabPageServerShare.TabIndex = 3
        Me.TabPageServerShare.Text = "Server Share Path"
        Me.TabPageServerShare.UseVisualStyleBackColor = True
        '
        'frameBackupPath
        '
        Me.frameBackupPath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.frameBackupPath.Controls.Add(Me.Label55)
        Me.frameBackupPath.Controls.Add(Me.cmdSaveServerPath)
        Me.frameBackupPath.Controls.Add(Me.cmdBrowseServerPath)
        Me.frameBackupPath.Controls.Add(Me.txtServerBackupFolderNetworkPath)
        Me.frameBackupPath.Controls.Add(Me.txtServerBackupFolderLocal)
        Me.frameBackupPath.Controls.Add(Me.Label49)
        Me.frameBackupPath.Controls.Add(Me.LabHelpBackupPath)
        Me.frameBackupPath.Controls.Add(Me.Label50)
        Me.frameBackupPath.Controls.Add(Me.Label51)
        Me.frameBackupPath.Controls.Add(Me.Label54)
        Me.frameBackupPath.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameBackupPath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameBackupPath.Location = New System.Drawing.Point(3, 3)
        Me.frameBackupPath.Name = "frameBackupPath"
        Me.frameBackupPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameBackupPath.Size = New System.Drawing.Size(861, 505)
        Me.frameBackupPath.TabIndex = 94
        Me.frameBackupPath.TabStop = False
        Me.frameBackupPath.Text = "frameBackupPath"
        '
        'Label55
        '
        Me.Label55.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.Color.DarkMagenta
        Me.Label55.Location = New System.Drawing.Point(539, 40)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(195, 58)
        Me.Label55.TabIndex = 99
        Me.Label55.Text = "Note: These settings are shared with the JobMatix Job-tracking Backup Functions."
        '
        'cmdSaveServerPath
        '
        Me.cmdSaveServerPath.BackColor = System.Drawing.Color.GreenYellow
        Me.cmdSaveServerPath.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSaveServerPath.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveServerPath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveServerPath.Location = New System.Drawing.Point(542, 384)
        Me.cmdSaveServerPath.Name = "cmdSaveServerPath"
        Me.cmdSaveServerPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSaveServerPath.Size = New System.Drawing.Size(80, 30)
        Me.cmdSaveServerPath.TabIndex = 3
        Me.cmdSaveServerPath.Text = "S a v e"
        Me.ToolTip1.SetToolTip(Me.cmdSaveServerPath, "Save new values..")
        Me.cmdSaveServerPath.UseVisualStyleBackColor = False
        '
        'cmdBrowseServerPath
        '
        Me.cmdBrowseServerPath.BackColor = System.Drawing.Color.Gainsboro
        Me.cmdBrowseServerPath.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBrowseServerPath.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBrowseServerPath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBrowseServerPath.Location = New System.Drawing.Point(542, 156)
        Me.cmdBrowseServerPath.Name = "cmdBrowseServerPath"
        Me.cmdBrowseServerPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBrowseServerPath.Size = New System.Drawing.Size(80, 30)
        Me.cmdBrowseServerPath.TabIndex = 1
        Me.cmdBrowseServerPath.Text = "Browse"
        Me.cmdBrowseServerPath.UseVisualStyleBackColor = False
        '
        'txtServerBackupFolderNetworkPath
        '
        Me.txtServerBackupFolderNetworkPath.AcceptsReturn = True
        Me.txtServerBackupFolderNetworkPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtServerBackupFolderNetworkPath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtServerBackupFolderNetworkPath.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServerBackupFolderNetworkPath.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServerBackupFolderNetworkPath.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServerBackupFolderNetworkPath.Location = New System.Drawing.Point(24, 304)
        Me.txtServerBackupFolderNetworkPath.MaxLength = 0
        Me.txtServerBackupFolderNetworkPath.Name = "txtServerBackupFolderNetworkPath"
        Me.txtServerBackupFolderNetworkPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServerBackupFolderNetworkPath.Size = New System.Drawing.Size(598, 19)
        Me.txtServerBackupFolderNetworkPath.TabIndex = 2
        Me.txtServerBackupFolderNetworkPath.Text = "txtServerBackupFolderNetworkPath"
        '
        'txtServerBackupFolderLocal
        '
        Me.txtServerBackupFolderLocal.AcceptsReturn = True
        Me.txtServerBackupFolderLocal.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtServerBackupFolderLocal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtServerBackupFolderLocal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServerBackupFolderLocal.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServerBackupFolderLocal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServerBackupFolderLocal.Location = New System.Drawing.Point(24, 192)
        Me.txtServerBackupFolderLocal.MaxLength = 0
        Me.txtServerBackupFolderLocal.Name = "txtServerBackupFolderLocal"
        Me.txtServerBackupFolderLocal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServerBackupFolderLocal.Size = New System.Drawing.Size(598, 19)
        Me.txtServerBackupFolderLocal.TabIndex = 0
        Me.txtServerBackupFolderLocal.Text = "txtServerBackupFolderLocal"
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.Transparent
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.Color.Indigo
        Me.Label49.Location = New System.Drawing.Point(32, 384)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(183, 58)
        Me.Label49.TabIndex = 98
        Me.Label49.Text = "Note:  This frame is enabled only when you are running this on the SQL Server mac" & _
    "hine.."
        '
        'LabHelpBackupPath
        '
        Me.LabHelpBackupPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.LabHelpBackupPath.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHelpBackupPath.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHelpBackupPath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHelpBackupPath.Location = New System.Drawing.Point(221, 38)
        Me.LabHelpBackupPath.Name = "LabHelpBackupPath"
        Me.LabHelpBackupPath.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.LabHelpBackupPath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHelpBackupPath.Size = New System.Drawing.Size(296, 92)
        Me.LabHelpBackupPath.TabIndex = 97
        Me.LabHelpBackupPath.Text = "LabHelpBackupPath"
        '
        'Label50
        '
        Me.Label50.BackColor = System.Drawing.Color.Transparent
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(24, 38)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(160, 81)
        Me.Label50.TabIndex = 96
        Me.Label50.Text = "JobMatix Backup File -Destination Path-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(and Network Shared folder.)"
        '
        'Label51
        '
        Me.Label51.BackColor = System.Drawing.Color.Transparent
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(24, 264)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(575, 37)
        Me.Label51.TabIndex = 95
        Me.Label51.Text = "B.  SAME Backup Folder-   Server SHARE Path:  " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "                (eg  ""\\ServerNam" & _
    "e\Backup-Share\"" )"
        '
        'Label54
        '
        Me.Label54.BackColor = System.Drawing.Color.Transparent
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label54.Location = New System.Drawing.Point(21, 172)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(412, 17)
        Me.Label54.TabIndex = 94
        Me.Label54.Text = "A.  Backup Folder-   Server Local Path:  (eg  ""c:\JobMatix-Backups\"" )"
        '
        'labDLLversion
        '
        Me.labDLLversion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labDLLversion.Location = New System.Drawing.Point(3, 633)
        Me.labDLLversion.Name = "labDLLversion"
        Me.labDLLversion.Size = New System.Drawing.Size(306, 13)
        Me.labDLLversion.TabIndex = 44
        Me.labDLLversion.Text = "labDLLversion"
        '
        'frmPOS34Setup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(898, 652)
        Me.Controls.Add(Me.labDLLversion)
        Me.Controls.Add(Me.grpBoxAdmin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmPOS34Setup"
        Me.Text = "frmPOS31Admin"
        Me.grpBoxAdmin.ResumeLayout(False)
        Me.ShapedPanelAdminHdr.ResumeLayout(False)
        Me.ShapedPanelAdminHdr.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageOptions.ResumeLayout(False)
        Me.panelPOS_Setup.ResumeLayout(False)
        Me.panelPOS_Setup.PerformLayout()
        Me.panelPricingGrades.ResumeLayout(False)
        Me.panelPricingGrades.PerformLayout()
        Me.grpBoxTerms.ResumeLayout(False)
        Me.grpBoxTerms.PerformLayout()
        Me.panelLabourItems.ResumeLayout(False)
        Me.panelLabourItems.PerformLayout()
        Me.TabPageMailSetup.ResumeLayout(False)
        Me.grpBoxEmailOptions.ResumeLayout(False)
        Me.grpBoxEmailOptions.PerformLayout()
        Me.frameSMTPSettings.ResumeLayout(False)
        Me.frameSMTPSettings.PerformLayout()
        Me.TabPageServerShare.ResumeLayout(False)
        Me.frameBackupPath.ResumeLayout(False)
        Me.frameBackupPath.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpBoxAdmin As System.Windows.Forms.GroupBox
    Friend WithEvents panelPOS_Setup As System.Windows.Forms.Panel
    Friend WithEvents btnSaveTerms As System.Windows.Forms.Button
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtPOS_Terms As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSellMargin As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveMargin As System.Windows.Forms.Button
    Friend WithEvents btnSaveGST As System.Windows.Forms.Button
    Friend WithEvents txtGST_percentage As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents labStaffName As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents labDLLversion As System.Windows.Forms.Label
    Friend WithEvents btnSaveFontName As System.Windows.Forms.Button
    Friend WithEvents txtStockBarcodeFontName As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnSaveFontSize As System.Windows.Forms.Button
    Friend WithEvents txtStockBarcodeFontSize As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtLabourDescr_pr1 As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveLabour_pr1 As System.Windows.Forms.Button
    Friend WithEvents labLabourPrice_pr1 As System.Windows.Forms.Label
    Friend WithEvents labLabourPrice_pr3 As System.Windows.Forms.Label
    Friend WithEvents txtLabourDescr_pr3 As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents labLabourPrice_pr2 As System.Windows.Forms.Label
    Friend WithEvents txtLabourDescr_pr2 As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents panelLabourItems As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageOptions As System.Windows.Forms.TabPage
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents grpBoxTerms As System.Windows.Forms.GroupBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtBSB2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBSB1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAccountName As System.Windows.Forms.TextBox
    Friend WithEvents txtAccountNo As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents TabPageMailSetup As System.Windows.Forms.TabPage
    Friend WithEvents frameSMTPSettings As System.Windows.Forms.GroupBox
    Friend WithEvents chkHostUsesSSL As System.Windows.Forms.CheckBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents txtSMTPHostPort As System.Windows.Forms.TextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents txtSMTPServer As System.Windows.Forms.TextBox
    Public WithEvents txtSMTPUsername As System.Windows.Forms.TextBox
    Public WithEvents txtSMTPPassword1 As System.Windows.Forms.TextBox
    Public WithEvents txtSMTPPassword2 As System.Windows.Forms.TextBox
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents labSMTPConfirm As System.Windows.Forms.Label
    Friend WithEvents btnSaveSMTP As System.Windows.Forms.Button
    Friend WithEvents labJobTrackingSharing As System.Windows.Forms.Label
    Friend WithEvents grpBoxEmailOptions As System.Windows.Forms.GroupBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtEmailTextInvoice As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtEmailTextStatement As System.Windows.Forms.TextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents txtEmailTextPO As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveEmailTexts As System.Windows.Forms.Button
    Friend WithEvents cboReportPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents ShapedPanelAdminHdr As JMxPOS330.ShapedPanel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents cboReceiptPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents panelPricingGrades As System.Windows.Forms.Panel
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents txtPriceGrade4 As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtPriceGrade3 As System.Windows.Forms.TextBox
    Friend WithEvents txtPriceGrade1 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPriceGrade2 As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveGrades As System.Windows.Forms.Button
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents chkAllowEmailInvoices As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowEmailStatements As System.Windows.Forms.CheckBox
    Friend WithEvents TabPageServerShare As System.Windows.Forms.TabPage
    Public WithEvents frameBackupPath As System.Windows.Forms.GroupBox
    Public WithEvents cmdBrowseServerPath As System.Windows.Forms.Button
    Public WithEvents txtServerBackupFolderNetworkPath As System.Windows.Forms.TextBox
    Public WithEvents txtServerBackupFolderLocal As System.Windows.Forms.TextBox
    Public WithEvents Label49 As System.Windows.Forms.Label
    Public WithEvents LabHelpBackupPath As System.Windows.Forms.Label
    Public WithEvents Label50 As System.Windows.Forms.Label
    Public WithEvents Label51 As System.Windows.Forms.Label
    Public WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Public WithEvents cmdSaveServerPath As System.Windows.Forms.Button
    Friend WithEvents Label55 As System.Windows.Forms.Label
End Class
