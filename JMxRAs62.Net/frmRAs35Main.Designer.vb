<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRAs35Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRAs35Main))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.panelHdr = New System.Windows.Forms.Panel()
        Me.picPOS_logo = New System.Windows.Forms.PictureBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtJetDBName = New System.Windows.Forms.TextBox()
        Me.labStatus = New System.Windows.Forms.Label()
        Me.txtStaff = New System.Windows.Forms.TextBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabBusiness = New System.Windows.Forms.Label()
        Me.labRetailDB = New System.Windows.Forms.Label()
        Me.LabServer = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabToday = New System.Windows.Forms.Label()
        Me.LabHdr0 = New System.Windows.Forms.Label()
        Me.LabAdmin = New System.Windows.Forms.Label()
        Me.frameRAsTab = New System.Windows.Forms.GroupBox()
        Me.TabControlRAs = New System.Windows.Forms.TabControl()
        Me.TabPageRAsTree = New System.Windows.Forms.TabPage()
        Me.frameRAsTree = New System.Windows.Forms.GroupBox()
        Me._OptRATreeSort_3 = New System.Windows.Forms.RadioButton()
        Me._OptRATreeSort_2 = New System.Windows.Forms.RadioButton()
        Me.cmdRefreshRAsTree = New System.Windows.Forms.Button()
        Me._OptRATreeSort_1 = New System.Windows.Forms.RadioButton()
        Me._OptRATreeSort_0 = New System.Windows.Forms.RadioButton()
        Me.ChkAutoRefreshRAs = New System.Windows.Forms.CheckBox()
        Me.tvwRAs = New System.Windows.Forms.TreeView()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.LabTreeStatusRAs = New System.Windows.Forms.Label()
        Me.TabPageRAsGrid = New System.Windows.Forms.TabPage()
        Me.FrameBrowseRAs = New System.Windows.Forms.GroupBox()
        Me.DataGridViewRAs = New System.Windows.Forms.DataGridView()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtFindRAs = New System.Windows.Forms.TextBox()
        Me.txtRASearch = New System.Windows.Forms.TextBox()
        Me.cmdClearRASearch = New System.Windows.Forms.Button()
        Me.cmdRASearch = New System.Windows.Forms.Button()
        Me.ToolbarRAsGrid = New System.Windows.Forms.ToolStrip()
        Me._ToolbarRAs_ButtonQueued = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarRAs_ButtonRequested = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarRAs_ButtonGranted = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarRAs_ButtonShipped = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarRAs_ButtonCompleted = New System.Windows.Forms.ToolStripButton()
        Me._ToolbarRAs_ButtonAll_RAs = New System.Windows.Forms.ToolStripButton()
        Me.LabRATitle = New System.Windows.Forms.Label()
        Me.LabFindRAs = New System.Windows.Forms.Label()
        Me.labRecCountRAs = New System.Windows.Forms.Label()
        Me.LabRASearch = New System.Windows.Forms.Label()
        Me.TabPageRAsSuppliers = New System.Windows.Forms.TabPage()
        Me.frameRA_suppliers = New System.Windows.Forms.GroupBox()
        Me.panelRA_supplierInfo = New System.Windows.Forms.Panel()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.cboRAs_A4Printers = New System.Windows.Forms.ComboBox()
        Me.btnRAsUpdateGroupSent = New System.Windows.Forms.Button()
        Me.txtRA_supplierName = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.chkSelectAllRAsGranted = New System.Windows.Forms.CheckBox()
        Me.txtFindSupplier = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.labRecCountSupplier = New System.Windows.Forms.Label()
        Me.chkShowGrantedRAsOnly = New System.Windows.Forms.CheckBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.labFindSupplier = New System.Windows.Forms.Label()
        Me.listViewSupplierRAs = New System.Windows.Forms.ListView()
        Me.listViewRA_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listViewRA_status = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listViewRA_SupplierRMA_No = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listViewRA_SerialNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listViewRM_ItemCat1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listViewRM_ItemDescription = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.listViewRM_ItemSupplierCode = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.dgvRA_suppliers = New System.Windows.Forms.DataGridView()
        Me.FrameRADetails = New System.Windows.Forms.GroupBox()
        Me.btnRA_attachments = New System.Windows.Forms.Button()
        Me.txtFreightTrackNo = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtRACustomerContact = New System.Windows.Forms.TextBox()
        Me.txtRACat1 = New System.Windows.Forms.TextBox()
        Me.txtRASupplierRANo = New System.Windows.Forms.TextBox()
        Me.cmdViewRecordRAs = New System.Windows.Forms.Button()
        Me.txtRAItem = New System.Windows.Forms.TextBox()
        Me.txtRAProblem = New System.Windows.Forms.TextBox()
        Me.txtRACreated = New System.Windows.Forms.TextBox()
        Me.txtRAUpdated = New System.Windows.Forms.TextBox()
        Me.txtRASerialNo = New System.Windows.Forms.TextBox()
        Me.txtRAProdBarcode = New System.Windows.Forms.TextBox()
        Me.txtRASupplier = New System.Windows.Forms.TextBox()
        Me.txtRACustomerName = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.LabRAStatusFriendly = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.LabRA_id = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.labRASupplier = New System.Windows.Forms.Label()
        Me.labJobMatixRAs = New System.Windows.Forms.Label()
        Me.cmdNewRA = New System.Windows.Forms.Button()
        Me.LabShowSearchRAs = New System.Windows.Forms.Label()
        Me.labVersion = New System.Windows.Forms.Label()
        Me.Picture2 = New System.Windows.Forms.PictureBox()
        Me.TimerRAs = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelHdr.SuspendLayout()
        CType(Me.picPOS_logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.frameRAsTab.SuspendLayout()
        Me.TabControlRAs.SuspendLayout()
        Me.TabPageRAsTree.SuspendLayout()
        Me.frameRAsTree.SuspendLayout()
        Me.TabPageRAsGrid.SuspendLayout()
        Me.FrameBrowseRAs.SuspendLayout()
        CType(Me.DataGridViewRAs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolbarRAsGrid.SuspendLayout()
        Me.TabPageRAsSuppliers.SuspendLayout()
        Me.frameRA_suppliers.SuspendLayout()
        Me.panelRA_supplierInfo.SuspendLayout()
        CType(Me.dgvRA_suppliers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FrameRADetails.SuspendLayout()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'panelHdr
        '
        Me.panelHdr.Controls.Add(Me.picPOS_logo)
        Me.panelHdr.Controls.Add(Me.Label18)
        Me.panelHdr.Controls.Add(Me.txtJetDBName)
        Me.panelHdr.Controls.Add(Me.labStatus)
        Me.panelHdr.Controls.Add(Me.txtStaff)
        Me.panelHdr.Controls.Add(Me.cmdExit)
        Me.panelHdr.Controls.Add(Me.Label3)
        Me.panelHdr.Controls.Add(Me.LabBusiness)
        Me.panelHdr.Controls.Add(Me.labRetailDB)
        Me.panelHdr.Controls.Add(Me.LabServer)
        Me.panelHdr.Controls.Add(Me.Label7)
        Me.panelHdr.Controls.Add(Me.LabToday)
        Me.panelHdr.Controls.Add(Me.LabHdr0)
        Me.panelHdr.Controls.Add(Me.LabAdmin)
        Me.panelHdr.Location = New System.Drawing.Point(3, 0)
        Me.panelHdr.Name = "panelHdr"
        Me.panelHdr.Size = New System.Drawing.Size(989, 81)
        Me.panelHdr.TabIndex = 1
        '
        'picPOS_logo
        '
        Me.picPOS_logo.Image = CType(resources.GetObject("picPOS_logo.Image"), System.Drawing.Image)
        Me.picPOS_logo.Location = New System.Drawing.Point(138, 15)
        Me.picPOS_logo.Name = "picPOS_logo"
        Me.picPOS_logo.Size = New System.Drawing.Size(34, 25)
        Me.picPOS_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPOS_logo.TabIndex = 135
        Me.picPOS_logo.TabStop = False
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Firebrick
        Me.Label18.Location = New System.Drawing.Point(5, 43)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(116, 19)
        Me.Label18.TabIndex = 134
        Me.Label18.Text = "Stock Returns"
        '
        'txtJetDBName
        '
        Me.txtJetDBName.AcceptsReturn = True
        Me.txtJetDBName.BackColor = System.Drawing.Color.FromArgb(CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.txtJetDBName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtJetDBName.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.txtJetDBName.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJetDBName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtJetDBName.Location = New System.Drawing.Point(436, 42)
        Me.txtJetDBName.MaxLength = 0
        Me.txtJetDBName.Multiline = True
        Me.txtJetDBName.Name = "txtJetDBName"
        Me.txtJetDBName.ReadOnly = True
        Me.txtJetDBName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJetDBName.Size = New System.Drawing.Size(338, 29)
        Me.txtJetDBName.TabIndex = 133
        '
        'labStatus
        '
        Me.labStatus.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.labStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.labStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labStatus.Location = New System.Drawing.Point(776, 41)
        Me.labStatus.Name = "labStatus"
        Me.labStatus.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.labStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labStatus.Size = New System.Drawing.Size(117, 29)
        Me.labStatus.TabIndex = 132
        Me.labStatus.Text = "labStatus"
        '
        'txtStaff
        '
        Me.txtStaff.AcceptsReturn = True
        Me.txtStaff.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtStaff.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStaff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStaff.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStaff.Location = New System.Drawing.Point(801, 23)
        Me.txtStaff.MaxLength = 0
        Me.txtStaff.Name = "txtStaff"
        Me.txtStaff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStaff.Size = New System.Drawing.Size(89, 14)
        Me.txtStaff.TabIndex = 131
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(931, 8)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(49, 20)
        Me.cmdExit.TabIndex = 122
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(801, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(42, 17)
        Me.Label3.TabIndex = 130
        Me.Label3.Text = "Staff:"
        '
        'LabBusiness
        '
        Me.LabBusiness.BackColor = System.Drawing.Color.Transparent
        Me.LabBusiness.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabBusiness.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabBusiness.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabBusiness.Location = New System.Drawing.Point(193, 12)
        Me.LabBusiness.Name = "LabBusiness"
        Me.LabBusiness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabBusiness.Size = New System.Drawing.Size(214, 14)
        Me.LabBusiness.TabIndex = 129
        Me.LabBusiness.Text = "labBusiness"
        '
        'labRetailDB
        '
        Me.labRetailDB.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labRetailDB.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRetailDB.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRetailDB.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRetailDB.Location = New System.Drawing.Point(434, 15)
        Me.labRetailDB.Name = "labRetailDB"
        Me.labRetailDB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRetailDB.Size = New System.Drawing.Size(145, 24)
        Me.labRetailDB.TabIndex = 128
        Me.labRetailDB.Text = "Retail Manager Database-"
        Me.labRetailDB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabServer
        '
        Me.LabServer.BackColor = System.Drawing.Color.Transparent
        Me.LabServer.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabServer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabServer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabServer.Location = New System.Drawing.Point(193, 43)
        Me.LabServer.Name = "LabServer"
        Me.LabServer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabServer.Size = New System.Drawing.Size(229, 29)
        Me.LabServer.TabIndex = 127
        Me.LabServer.Text = "LabServer"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 6.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(193, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(145, 17)
        Me.Label7.TabIndex = 126
        Me.Label7.Text = "Sql Server (Jobs Database):"
        '
        'LabToday
        '
        Me.LabToday.BackColor = System.Drawing.Color.Transparent
        Me.LabToday.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabToday.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabToday.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabToday.Location = New System.Drawing.Point(605, 16)
        Me.LabToday.Name = "LabToday"
        Me.LabToday.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabToday.Size = New System.Drawing.Size(166, 17)
        Me.LabToday.TabIndex = 125
        Me.LabToday.Text = "LabToday"
        Me.LabToday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabHdr0
        '
        Me.LabHdr0.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr0.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr0.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr0.Location = New System.Drawing.Point(4, 15)
        Me.LabHdr0.Name = "LabHdr0"
        Me.LabHdr0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr0.Size = New System.Drawing.Size(129, 25)
        Me.LabHdr0.TabIndex = 124
        Me.LabHdr0.Text = "JobMatix RA's"
        '
        'LabAdmin
        '
        Me.LabAdmin.BackColor = System.Drawing.Color.Transparent
        Me.LabAdmin.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabAdmin.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabAdmin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabAdmin.Location = New System.Drawing.Point(897, 48)
        Me.LabAdmin.Name = "LabAdmin"
        Me.LabAdmin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabAdmin.Size = New System.Drawing.Size(75, 17)
        Me.LabAdmin.TabIndex = 123
        Me.LabAdmin.Text = "LabAdmin"
        '
        'frameRAsTab
        '
        Me.frameRAsTab.BackColor = System.Drawing.Color.LavenderBlush
        Me.frameRAsTab.CausesValidation = False
        Me.frameRAsTab.Controls.Add(Me.TabControlRAs)
        Me.frameRAsTab.Controls.Add(Me.FrameRADetails)
        Me.frameRAsTab.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameRAsTab.Location = New System.Drawing.Point(3, 123)
        Me.frameRAsTab.Name = "frameRAsTab"
        Me.frameRAsTab.Size = New System.Drawing.Size(989, 567)
        Me.frameRAsTab.TabIndex = 2
        Me.frameRAsTab.TabStop = False
        Me.frameRAsTab.Text = "frameRAsTab"
        '
        'TabControlRAs
        '
        Me.TabControlRAs.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.TabControlRAs.CausesValidation = False
        Me.TabControlRAs.Controls.Add(Me.TabPageRAsTree)
        Me.TabControlRAs.Controls.Add(Me.TabPageRAsGrid)
        Me.TabControlRAs.Controls.Add(Me.TabPageRAsSuppliers)
        Me.TabControlRAs.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlRAs.Location = New System.Drawing.Point(3, 9)
        Me.TabControlRAs.Name = "TabControlRAs"
        Me.TabControlRAs.SelectedIndex = 0
        Me.TabControlRAs.Size = New System.Drawing.Size(644, 546)
        Me.TabControlRAs.TabIndex = 83
        '
        'TabPageRAsTree
        '
        Me.TabPageRAsTree.Controls.Add(Me.frameRAsTree)
        Me.TabPageRAsTree.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageRAsTree.ForeColor = System.Drawing.Color.Firebrick
        Me.TabPageRAsTree.Location = New System.Drawing.Point(4, 26)
        Me.TabPageRAsTree.Name = "TabPageRAsTree"
        Me.TabPageRAsTree.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageRAsTree.Size = New System.Drawing.Size(636, 516)
        Me.TabPageRAsTree.TabIndex = 0
        Me.TabPageRAsTree.Text = "Active RAs Tree"
        Me.TabPageRAsTree.UseVisualStyleBackColor = True
        '
        'frameRAsTree
        '
        Me.frameRAsTree.BackColor = System.Drawing.Color.White
        Me.frameRAsTree.Controls.Add(Me._OptRATreeSort_3)
        Me.frameRAsTree.Controls.Add(Me._OptRATreeSort_2)
        Me.frameRAsTree.Controls.Add(Me.cmdRefreshRAsTree)
        Me.frameRAsTree.Controls.Add(Me._OptRATreeSort_1)
        Me.frameRAsTree.Controls.Add(Me._OptRATreeSort_0)
        Me.frameRAsTree.Controls.Add(Me.ChkAutoRefreshRAs)
        Me.frameRAsTree.Controls.Add(Me.tvwRAs)
        Me.frameRAsTree.Controls.Add(Me.Label27)
        Me.frameRAsTree.Controls.Add(Me.LabTreeStatusRAs)
        Me.frameRAsTree.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frameRAsTree.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frameRAsTree.Location = New System.Drawing.Point(3, 3)
        Me.frameRAsTree.Name = "frameRAsTree"
        Me.frameRAsTree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frameRAsTree.Size = New System.Drawing.Size(625, 475)
        Me.frameRAsTree.TabIndex = 79
        Me.frameRAsTree.TabStop = False
        Me.frameRAsTree.Text = "frameRAsTree"
        '
        '_OptRATreeSort_3
        '
        Me._OptRATreeSort_3.BackColor = System.Drawing.Color.Transparent
        Me._OptRATreeSort_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRATreeSort_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._OptRATreeSort_3.Location = New System.Drawing.Point(504, 32)
        Me._OptRATreeSort_3.Name = "_OptRATreeSort_3"
        Me._OptRATreeSort_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRATreeSort_3.Size = New System.Drawing.Size(105, 20)
        Me._OptRATreeSort_3.TabIndex = 62
        Me._OptRATreeSort_3.TabStop = True
        Me._OptRATreeSort_3.Text = "DateUpdated"
        Me._OptRATreeSort_3.UseVisualStyleBackColor = False
        '
        '_OptRATreeSort_2
        '
        Me._OptRATreeSort_2.BackColor = System.Drawing.Color.Transparent
        Me._OptRATreeSort_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRATreeSort_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._OptRATreeSort_2.Location = New System.Drawing.Point(432, 32)
        Me._OptRATreeSort_2.Name = "_OptRATreeSort_2"
        Me._OptRATreeSort_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRATreeSort_2.Size = New System.Drawing.Size(83, 20)
        Me._OptRATreeSort_2.TabIndex = 61
        Me._OptRATreeSort_2.TabStop = True
        Me._OptRATreeSort_2.Text = "Supplier"
        Me._OptRATreeSort_2.UseVisualStyleBackColor = False
        '
        'cmdRefreshRAsTree
        '
        Me.cmdRefreshRAsTree.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdRefreshRAsTree.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefreshRAsTree.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefreshRAsTree.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefreshRAsTree.Location = New System.Drawing.Point(128, 32)
        Me.cmdRefreshRAsTree.Name = "cmdRefreshRAsTree"
        Me.cmdRefreshRAsTree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefreshRAsTree.Size = New System.Drawing.Size(57, 17)
        Me.cmdRefreshRAsTree.TabIndex = 60
        Me.cmdRefreshRAsTree.Text = "Refresh"
        Me.cmdRefreshRAsTree.UseVisualStyleBackColor = False
        '
        '_OptRATreeSort_1
        '
        Me._OptRATreeSort_1.BackColor = System.Drawing.Color.Transparent
        Me._OptRATreeSort_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRATreeSort_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._OptRATreeSort_1.Location = New System.Drawing.Point(367, 32)
        Me._OptRATreeSort_1.Name = "_OptRATreeSort_1"
        Me._OptRATreeSort_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRATreeSort_1.Size = New System.Drawing.Size(65, 20)
        Me._OptRATreeSort_1.TabIndex = 59
        Me._OptRATreeSort_1.TabStop = True
        Me._OptRATreeSort_1.Text = "Cat1"
        Me._OptRATreeSort_1.UseVisualStyleBackColor = False
        '
        '_OptRATreeSort_0
        '
        Me._OptRATreeSort_0.BackColor = System.Drawing.Color.Transparent
        Me._OptRATreeSort_0.Checked = True
        Me._OptRATreeSort_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRATreeSort_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._OptRATreeSort_0.Location = New System.Drawing.Point(296, 31)
        Me._OptRATreeSort_0.Name = "_OptRATreeSort_0"
        Me._OptRATreeSort_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRATreeSort_0.Size = New System.Drawing.Size(70, 20)
        Me._OptRATreeSort_0.TabIndex = 58
        Me._OptRATreeSort_0.TabStop = True
        Me._OptRATreeSort_0.Text = "RA_Id"
        Me._OptRATreeSort_0.UseVisualStyleBackColor = False
        '
        'ChkAutoRefreshRAs
        '
        Me.ChkAutoRefreshRAs.BackColor = System.Drawing.Color.Transparent
        Me.ChkAutoRefreshRAs.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkAutoRefreshRAs.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAutoRefreshRAs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkAutoRefreshRAs.Location = New System.Drawing.Point(16, 16)
        Me.ChkAutoRefreshRAs.Name = "ChkAutoRefreshRAs"
        Me.ChkAutoRefreshRAs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkAutoRefreshRAs.Size = New System.Drawing.Size(65, 33)
        Me.ChkAutoRefreshRAs.TabIndex = 57
        Me.ChkAutoRefreshRAs.Text = "Auto Refresh"
        Me.ChkAutoRefreshRAs.UseVisualStyleBackColor = False
        '
        'tvwRAs
        '
        Me.tvwRAs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tvwRAs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tvwRAs.CausesValidation = False
        Me.tvwRAs.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvwRAs.HideSelection = False
        Me.tvwRAs.Indent = 19
        Me.tvwRAs.Location = New System.Drawing.Point(8, 56)
        Me.tvwRAs.Name = "tvwRAs"
        Me.tvwRAs.Size = New System.Drawing.Size(609, 84)
        Me.tvwRAs.TabIndex = 63
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(293, 16)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(316, 17)
        Me.Label27.TabIndex = 65
        Me.Label27.Text = "-- Sort RA Items By:"
        '
        'LabTreeStatusRAs
        '
        Me.LabTreeStatusRAs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabTreeStatusRAs.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabTreeStatusRAs.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabTreeStatusRAs.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabTreeStatusRAs.Location = New System.Drawing.Point(367, 158)
        Me.LabTreeStatusRAs.Name = "LabTreeStatusRAs"
        Me.LabTreeStatusRAs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabTreeStatusRAs.Size = New System.Drawing.Size(161, 17)
        Me.LabTreeStatusRAs.TabIndex = 64
        Me.LabTreeStatusRAs.Text = "LabTreeStatusRAs"
        Me.LabTreeStatusRAs.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TabPageRAsGrid
        '
        Me.TabPageRAsGrid.Controls.Add(Me.FrameBrowseRAs)
        Me.TabPageRAsGrid.Location = New System.Drawing.Point(4, 26)
        Me.TabPageRAsGrid.Name = "TabPageRAsGrid"
        Me.TabPageRAsGrid.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageRAsGrid.Size = New System.Drawing.Size(636, 516)
        Me.TabPageRAsGrid.TabIndex = 1
        Me.TabPageRAsGrid.Text = "RA's Search Grid"
        Me.TabPageRAsGrid.UseVisualStyleBackColor = True
        '
        'FrameBrowseRAs
        '
        Me.FrameBrowseRAs.BackColor = System.Drawing.Color.White
        Me.FrameBrowseRAs.Controls.Add(Me.DataGridViewRAs)
        Me.FrameBrowseRAs.Controls.Add(Me.Label28)
        Me.FrameBrowseRAs.Controls.Add(Me.txtFindRAs)
        Me.FrameBrowseRAs.Controls.Add(Me.txtRASearch)
        Me.FrameBrowseRAs.Controls.Add(Me.cmdClearRASearch)
        Me.FrameBrowseRAs.Controls.Add(Me.cmdRASearch)
        Me.FrameBrowseRAs.Controls.Add(Me.ToolbarRAsGrid)
        Me.FrameBrowseRAs.Controls.Add(Me.LabRATitle)
        Me.FrameBrowseRAs.Controls.Add(Me.LabFindRAs)
        Me.FrameBrowseRAs.Controls.Add(Me.labRecCountRAs)
        Me.FrameBrowseRAs.Controls.Add(Me.LabRASearch)
        Me.FrameBrowseRAs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameBrowseRAs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameBrowseRAs.Location = New System.Drawing.Point(3, 3)
        Me.FrameBrowseRAs.Name = "FrameBrowseRAs"
        Me.FrameBrowseRAs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameBrowseRAs.Size = New System.Drawing.Size(625, 507)
        Me.FrameBrowseRAs.TabIndex = 80
        Me.FrameBrowseRAs.TabStop = False
        Me.FrameBrowseRAs.Text = "FrameBrowseRAs"
        '
        'DataGridViewRAs
        '
        Me.DataGridViewRAs.AllowUserToAddRows = False
        Me.DataGridViewRAs.AllowUserToDeleteRows = False
        Me.DataGridViewRAs.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridViewRAs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewRAs.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewRAs.GridColor = System.Drawing.SystemColors.ControlLight
        Me.DataGridViewRAs.Location = New System.Drawing.Point(10, 107)
        Me.DataGridViewRAs.MultiSelect = False
        Me.DataGridViewRAs.Name = "DataGridViewRAs"
        Me.DataGridViewRAs.ReadOnly = True
        Me.DataGridViewRAs.RowTemplate.Height = 19
        Me.DataGridViewRAs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewRAs.Size = New System.Drawing.Size(605, 72)
        Me.DataGridViewRAs.TabIndex = 33
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(10, 24)
        Me.Label28.MaximumSize = New System.Drawing.Size(100, 32)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(100, 28)
        Me.Label28.TabIndex = 32
        Me.Label28.Text = "Browse RAs on RA Status"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtFindRAs
        '
        Me.txtFindRAs.AcceptsReturn = True
        Me.txtFindRAs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFindRAs.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFindRAs.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindRAs.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFindRAs.Location = New System.Drawing.Point(153, 80)
        Me.txtFindRAs.MaxLength = 0
        Me.txtFindRAs.Name = "txtFindRAs"
        Me.txtFindRAs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFindRAs.Size = New System.Drawing.Size(97, 22)
        Me.txtFindRAs.TabIndex = 22
        '
        'txtRASearch
        '
        Me.txtRASearch.AcceptsReturn = True
        Me.txtRASearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtRASearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRASearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRASearch.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRASearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRASearch.Location = New System.Drawing.Point(359, 73)
        Me.txtRASearch.MaxLength = 32
        Me.txtRASearch.Name = "txtRASearch"
        Me.txtRASearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRASearch.Size = New System.Drawing.Size(96, 14)
        Me.txtRASearch.TabIndex = 23
        Me.txtRASearch.Text = "txtRASearch"
        '
        'cmdClearRASearch
        '
        Me.cmdClearRASearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClearRASearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearRASearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearRASearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearRASearch.Location = New System.Drawing.Point(547, 68)
        Me.cmdClearRASearch.Name = "cmdClearRASearch"
        Me.cmdClearRASearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearRASearch.Size = New System.Drawing.Size(61, 21)
        Me.cmdClearRASearch.TabIndex = 25
        Me.cmdClearRASearch.Text = "Clear"
        Me.cmdClearRASearch.UseVisualStyleBackColor = False
        '
        'cmdRASearch
        '
        Me.cmdRASearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdRASearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRASearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRASearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRASearch.Image = CType(resources.GetObject("cmdRASearch.Image"), System.Drawing.Image)
        Me.cmdRASearch.Location = New System.Drawing.Point(466, 64)
        Me.cmdRASearch.Name = "cmdRASearch"
        Me.cmdRASearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRASearch.Size = New System.Drawing.Size(65, 25)
        Me.cmdRASearch.TabIndex = 24
        Me.cmdRASearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRASearch.UseVisualStyleBackColor = False
        '
        'ToolbarRAsGrid
        '
        Me.ToolbarRAsGrid.CanOverflow = False
        Me.ToolbarRAsGrid.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me._ToolbarRAs_ButtonQueued, Me._ToolbarRAs_ButtonRequested, Me._ToolbarRAs_ButtonGranted, Me._ToolbarRAs_ButtonShipped, Me._ToolbarRAs_ButtonCompleted, Me._ToolbarRAs_ButtonAll_RAs})
        Me.ToolbarRAsGrid.Location = New System.Drawing.Point(3, 17)
        Me.ToolbarRAsGrid.Name = "ToolbarRAsGrid"
        Me.ToolbarRAsGrid.Padding = New System.Windows.Forms.Padding(120, 0, 1, 0)
        Me.ToolbarRAsGrid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ToolbarRAsGrid.Size = New System.Drawing.Size(619, 40)
        Me.ToolbarRAsGrid.TabIndex = 26
        '
        '_ToolbarRAs_ButtonQueued
        '
        Me._ToolbarRAs_ButtonQueued.AutoSize = False
        Me._ToolbarRAs_ButtonQueued.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarRAs_ButtonQueued.Name = "_ToolbarRAs_ButtonQueued"
        Me._ToolbarRAs_ButtonQueued.Size = New System.Drawing.Size(60, 37)
        Me._ToolbarRAs_ButtonQueued.Tag = "queued"
        Me._ToolbarRAs_ButtonQueued.Text = "Queued"
        Me._ToolbarRAs_ButtonQueued.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarRAs_ButtonQueued.ToolTipText = "RA's waiting to be requested.."
        '
        '_ToolbarRAs_ButtonRequested
        '
        Me._ToolbarRAs_ButtonRequested.AutoSize = False
        Me._ToolbarRAs_ButtonRequested.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarRAs_ButtonRequested.Name = "_ToolbarRAs_ButtonRequested"
        Me._ToolbarRAs_ButtonRequested.Size = New System.Drawing.Size(60, 37)
        Me._ToolbarRAs_ButtonRequested.Tag = "requested"
        Me._ToolbarRAs_ButtonRequested.Text = "Requested"
        Me._ToolbarRAs_ButtonRequested.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarRAs_ButtonRequested.ToolTipText = "RMA's requested.."
        '
        '_ToolbarRAs_ButtonGranted
        '
        Me._ToolbarRAs_ButtonGranted.AutoSize = False
        Me._ToolbarRAs_ButtonGranted.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarRAs_ButtonGranted.Name = "_ToolbarRAs_ButtonGranted"
        Me._ToolbarRAs_ButtonGranted.Size = New System.Drawing.Size(60, 37)
        Me._ToolbarRAs_ButtonGranted.Tag = "granted"
        Me._ToolbarRAs_ButtonGranted.Text = "Granted"
        Me._ToolbarRAs_ButtonGranted.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarRAs_ButtonGranted.ToolTipText = "RMA Granted-  not yet shipped.."
        '
        '_ToolbarRAs_ButtonShipped
        '
        Me._ToolbarRAs_ButtonShipped.AutoSize = False
        Me._ToolbarRAs_ButtonShipped.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarRAs_ButtonShipped.Name = "_ToolbarRAs_ButtonShipped"
        Me._ToolbarRAs_ButtonShipped.Size = New System.Drawing.Size(60, 37)
        Me._ToolbarRAs_ButtonShipped.Tag = "shipped"
        Me._ToolbarRAs_ButtonShipped.Text = "Shipped"
        Me._ToolbarRAs_ButtonShipped.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarRAs_ButtonShipped.ToolTipText = "RA's with Goods shipped to Supplier.."
        '
        '_ToolbarRAs_ButtonCompleted
        '
        Me._ToolbarRAs_ButtonCompleted.AutoSize = False
        Me._ToolbarRAs_ButtonCompleted.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarRAs_ButtonCompleted.Name = "_ToolbarRAs_ButtonCompleted"
        Me._ToolbarRAs_ButtonCompleted.Size = New System.Drawing.Size(60, 37)
        Me._ToolbarRAs_ButtonCompleted.Tag = "completed"
        Me._ToolbarRAs_ButtonCompleted.Text = "Completed"
        Me._ToolbarRAs_ButtonCompleted.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarRAs_ButtonCompleted.ToolTipText = "RA's Completed or Refused"
        '
        '_ToolbarRAs_ButtonAll_RAs
        '
        Me._ToolbarRAs_ButtonAll_RAs.AutoSize = False
        Me._ToolbarRAs_ButtonAll_RAs.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me._ToolbarRAs_ButtonAll_RAs.Name = "_ToolbarRAs_ButtonAll_RAs"
        Me._ToolbarRAs_ButtonAll_RAs.Size = New System.Drawing.Size(60, 37)
        Me._ToolbarRAs_ButtonAll_RAs.Tag = "viewall"
        Me._ToolbarRAs_ButtonAll_RAs.Text = "All RAs"
        Me._ToolbarRAs_ButtonAll_RAs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me._ToolbarRAs_ButtonAll_RAs.ToolTipText = "Show All RAs"
        '
        'LabRATitle
        '
        Me.LabRATitle.BackColor = System.Drawing.SystemColors.Control
        Me.LabRATitle.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRATitle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRATitle.Location = New System.Drawing.Point(8, 16)
        Me.LabRATitle.Name = "LabRATitle"
        Me.LabRATitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRATitle.Size = New System.Drawing.Size(89, 25)
        Me.LabRATitle.TabIndex = 31
        Me.LabRATitle.Text = "LabRATitle"
        '
        'LabFindRAs
        '
        Me.LabFindRAs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabFindRAs.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabFindRAs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabFindRAs.Location = New System.Drawing.Point(6, 68)
        Me.LabFindRAs.Name = "LabFindRAs"
        Me.LabFindRAs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFindRAs.Size = New System.Drawing.Size(139, 33)
        Me.LabFindRAs.TabIndex = 29
        Me.LabFindRAs.Text = "LabFindRAs"
        '
        'labRecCountRAs
        '
        Me.labRecCountRAs.BackColor = System.Drawing.Color.Transparent
        Me.labRecCountRAs.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCountRAs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCountRAs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCountRAs.Location = New System.Drawing.Point(8, 191)
        Me.labRecCountRAs.Name = "labRecCountRAs"
        Me.labRecCountRAs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCountRAs.Size = New System.Drawing.Size(80, 17)
        Me.labRecCountRAs.TabIndex = 28
        Me.labRecCountRAs.Text = "labRecCountRAs"
        '
        'LabRASearch
        '
        Me.LabRASearch.BackColor = System.Drawing.Color.Transparent
        Me.LabRASearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRASearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRASearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRASearch.Location = New System.Drawing.Point(356, 58)
        Me.LabRASearch.Name = "LabRASearch"
        Me.LabRASearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRASearch.Size = New System.Drawing.Size(89, 19)
        Me.LabRASearch.TabIndex = 27
        Me.LabRASearch.Text = "Full Text Search:"
        Me.LabRASearch.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TabPageRAsSuppliers
        '
        Me.TabPageRAsSuppliers.Controls.Add(Me.frameRA_suppliers)
        Me.TabPageRAsSuppliers.Location = New System.Drawing.Point(4, 26)
        Me.TabPageRAsSuppliers.Name = "TabPageRAsSuppliers"
        Me.TabPageRAsSuppliers.Size = New System.Drawing.Size(636, 516)
        Me.TabPageRAsSuppliers.TabIndex = 2
        Me.TabPageRAsSuppliers.Text = "Packaged Returns"
        Me.TabPageRAsSuppliers.UseVisualStyleBackColor = True
        '
        'frameRA_suppliers
        '
        Me.frameRA_suppliers.BackColor = System.Drawing.Color.White
        Me.frameRA_suppliers.Controls.Add(Me.panelRA_supplierInfo)
        Me.frameRA_suppliers.Controls.Add(Me.txtFindSupplier)
        Me.frameRA_suppliers.Controls.Add(Me.Label38)
        Me.frameRA_suppliers.Controls.Add(Me.labRecCountSupplier)
        Me.frameRA_suppliers.Controls.Add(Me.chkShowGrantedRAsOnly)
        Me.frameRA_suppliers.Controls.Add(Me.Label40)
        Me.frameRA_suppliers.Controls.Add(Me.labFindSupplier)
        Me.frameRA_suppliers.Controls.Add(Me.listViewSupplierRAs)
        Me.frameRA_suppliers.Controls.Add(Me.dgvRA_suppliers)
        Me.frameRA_suppliers.Location = New System.Drawing.Point(3, 3)
        Me.frameRA_suppliers.Name = "frameRA_suppliers"
        Me.frameRA_suppliers.Size = New System.Drawing.Size(625, 489)
        Me.frameRA_suppliers.TabIndex = 82
        Me.frameRA_suppliers.TabStop = False
        Me.frameRA_suppliers.Text = "frameRA_suppliers"
        '
        'panelRA_supplierInfo
        '
        Me.panelRA_supplierInfo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelRA_supplierInfo.Controls.Add(Me.Label42)
        Me.panelRA_supplierInfo.Controls.Add(Me.cboRAs_A4Printers)
        Me.panelRA_supplierInfo.Controls.Add(Me.btnRAsUpdateGroupSent)
        Me.panelRA_supplierInfo.Controls.Add(Me.txtRA_supplierName)
        Me.panelRA_supplierInfo.Controls.Add(Me.Label41)
        Me.panelRA_supplierInfo.Controls.Add(Me.Label39)
        Me.panelRA_supplierInfo.Controls.Add(Me.chkSelectAllRAsGranted)
        Me.panelRA_supplierInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelRA_supplierInfo.Location = New System.Drawing.Point(298, 12)
        Me.panelRA_supplierInfo.Name = "panelRA_supplierInfo"
        Me.panelRA_supplierInfo.Size = New System.Drawing.Size(321, 121)
        Me.panelRA_supplierInfo.TabIndex = 93
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.Linen
        Me.Label42.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.Blue
        Me.Label42.Location = New System.Drawing.Point(106, 76)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(98, 13)
        Me.Label42.TabIndex = 102
        Me.Label42.Text = "-- A4 Printer --"
        '
        'cboRAs_A4Printers
        '
        Me.cboRAs_A4Printers.BackColor = System.Drawing.Color.Gainsboro
        Me.cboRAs_A4Printers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRAs_A4Printers.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboRAs_A4Printers.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRAs_A4Printers.FormattingEnabled = True
        Me.cboRAs_A4Printers.Location = New System.Drawing.Point(98, 91)
        Me.cboRAs_A4Printers.Name = "cboRAs_A4Printers"
        Me.cboRAs_A4Printers.Size = New System.Drawing.Size(106, 21)
        Me.cboRAs_A4Printers.TabIndex = 101
        '
        'btnRAsUpdateGroupSent
        '
        Me.btnRAsUpdateGroupSent.BackColor = System.Drawing.Color.LavenderBlush
        Me.btnRAsUpdateGroupSent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRAsUpdateGroupSent.Location = New System.Drawing.Point(209, 65)
        Me.btnRAsUpdateGroupSent.Name = "btnRAsUpdateGroupSent"
        Me.btnRAsUpdateGroupSent.Size = New System.Drawing.Size(109, 46)
        Me.btnRAsUpdateGroupSent.TabIndex = 9
        Me.btnRAsUpdateGroupSent.Text = "Go- Print Group Label && Update as 'Sent' "
        Me.btnRAsUpdateGroupSent.UseVisualStyleBackColor = False
        '
        'txtRA_supplierName
        '
        Me.txtRA_supplierName.BackColor = System.Drawing.Color.LightYellow
        Me.txtRA_supplierName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRA_supplierName.Location = New System.Drawing.Point(69, 9)
        Me.txtRA_supplierName.Multiline = True
        Me.txtRA_supplierName.Name = "txtRA_supplierName"
        Me.txtRA_supplierName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRA_supplierName.Size = New System.Drawing.Size(249, 50)
        Me.txtRA_supplierName.TabIndex = 8
        '
        'Label41
        '
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(4, 62)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(92, 29)
        Me.Label41.TabIndex = 7
        Me.Label41.Text = "Check RA's to send  in Package"
        '
        'Label39
        '
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(5, 10)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(64, 17)
        Me.Label39.TabIndex = 5
        Me.Label39.Text = "Supplier:"
        '
        'chkSelectAllRAsGranted
        '
        Me.chkSelectAllRAsGranted.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkSelectAllRAsGranted.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSelectAllRAsGranted.Location = New System.Drawing.Point(8, 91)
        Me.chkSelectAllRAsGranted.Name = "chkSelectAllRAsGranted"
        Me.chkSelectAllRAsGranted.Size = New System.Drawing.Size(81, 20)
        Me.chkSelectAllRAsGranted.TabIndex = 3
        Me.chkSelectAllRAsGranted.Text = "(Check All )"
        Me.chkSelectAllRAsGranted.UseVisualStyleBackColor = False
        '
        'txtFindSupplier
        '
        Me.txtFindSupplier.AcceptsReturn = True
        Me.txtFindSupplier.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFindSupplier.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFindSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFindSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindSupplier.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFindSupplier.Location = New System.Drawing.Point(113, 109)
        Me.txtFindSupplier.MaxLength = 0
        Me.txtFindSupplier.Name = "txtFindSupplier"
        Me.txtFindSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFindSupplier.Size = New System.Drawing.Size(89, 14)
        Me.txtFindSupplier.TabIndex = 0
        Me.txtFindSupplier.Text = "txtFindSupplier"
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Firebrick
        Me.Label38.Location = New System.Drawing.Point(10, 21)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(139, 31)
        Me.Label38.TabIndex = 92
        Me.Label38.Text = "Browsing all Suppliers   (with RA's).."
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labRecCountSupplier
        '
        Me.labRecCountSupplier.BackColor = System.Drawing.Color.Transparent
        Me.labRecCountSupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCountSupplier.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCountSupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCountSupplier.Location = New System.Drawing.Point(208, 109)
        Me.labRecCountSupplier.Name = "labRecCountSupplier"
        Me.labRecCountSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRecCountSupplier.Size = New System.Drawing.Size(61, 14)
        Me.labRecCountSupplier.TabIndex = 91
        Me.labRecCountSupplier.Text = "labRecCountSupplier"
        Me.labRecCountSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkShowGrantedRAsOnly
        '
        Me.chkShowGrantedRAsOnly.BackColor = System.Drawing.Color.WhiteSmoke
        Me.chkShowGrantedRAsOnly.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowGrantedRAsOnly.Location = New System.Drawing.Point(183, 65)
        Me.chkShowGrantedRAsOnly.Name = "chkShowGrantedRAsOnly"
        Me.chkShowGrantedRAsOnly.Size = New System.Drawing.Size(94, 30)
        Me.chkShowGrantedRAsOnly.TabIndex = 2
        Me.chkShowGrantedRAsOnly.Text = "Show only 'Granted' RAs"
        Me.chkShowGrantedRAsOnly.UseVisualStyleBackColor = False
        '
        'Label40
        '
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(162, 7)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(115, 56)
        Me.Label40.TabIndex = 6
        Me.Label40.Text = "Select Supplier and a set of RA's for Shipping Package"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labFindSupplier
        '
        Me.labFindSupplier.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labFindSupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.labFindSupplier.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labFindSupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labFindSupplier.Location = New System.Drawing.Point(10, 100)
        Me.labFindSupplier.Name = "labFindSupplier"
        Me.labFindSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labFindSupplier.Size = New System.Drawing.Size(97, 27)
        Me.labFindSupplier.TabIndex = 89
        Me.labFindSupplier.Text = "labFindSupplier"
        '
        'listViewSupplierRAs
        '
        Me.listViewSupplierRAs.BackColor = System.Drawing.Color.WhiteSmoke
        Me.listViewSupplierRAs.CheckBoxes = True
        Me.listViewSupplierRAs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.listViewRA_id, Me.listViewRA_status, Me.listViewRA_SupplierRMA_No, Me.listViewRA_SerialNumber, Me.listViewRM_ItemCat1, Me.listViewRM_ItemDescription, Me.listViewRM_ItemSupplierCode})
        Me.listViewSupplierRAs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listViewSupplierRAs.FullRowSelect = True
        Me.listViewSupplierRAs.GridLines = True
        Me.listViewSupplierRAs.Location = New System.Drawing.Point(297, 136)
        Me.listViewSupplierRAs.MultiSelect = False
        Me.listViewSupplierRAs.Name = "listViewSupplierRAs"
        Me.listViewSupplierRAs.Size = New System.Drawing.Size(360, 334)
        Me.listViewSupplierRAs.TabIndex = 4
        Me.listViewSupplierRAs.UseCompatibleStateImageBehavior = False
        Me.listViewSupplierRAs.View = System.Windows.Forms.View.Details
        '
        'listViewRA_id
        '
        Me.listViewRA_id.Text = "RA_id"
        Me.listViewRA_id.Width = 50
        '
        'listViewRA_status
        '
        Me.listViewRA_status.Text = "Status"
        '
        'listViewRA_SupplierRMA_No
        '
        Me.listViewRA_SupplierRMA_No.Text = "Supplier RMA"
        Me.listViewRA_SupplierRMA_No.Width = 80
        '
        'listViewRA_SerialNumber
        '
        Me.listViewRA_SerialNumber.Text = "Serial No"
        Me.listViewRA_SerialNumber.Width = 80
        '
        'listViewRM_ItemCat1
        '
        Me.listViewRM_ItemCat1.Text = "Cat1"
        '
        'listViewRM_ItemDescription
        '
        Me.listViewRM_ItemDescription.Text = "Description"
        Me.listViewRM_ItemDescription.Width = 120
        '
        'listViewRM_ItemSupplierCode
        '
        Me.listViewRM_ItemSupplierCode.Text = "Supplier Code"
        Me.listViewRM_ItemSupplierCode.Width = 80
        '
        'dgvRA_suppliers
        '
        Me.dgvRA_suppliers.AllowUserToAddRows = False
        Me.dgvRA_suppliers.AllowUserToDeleteRows = False
        Me.dgvRA_suppliers.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvRA_suppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRA_suppliers.GridColor = System.Drawing.SystemColors.Control
        Me.dgvRA_suppliers.Location = New System.Drawing.Point(6, 136)
        Me.dgvRA_suppliers.MultiSelect = False
        Me.dgvRA_suppliers.Name = "dgvRA_suppliers"
        Me.dgvRA_suppliers.ReadOnly = True
        Me.dgvRA_suppliers.RowTemplate.Height = 17
        Me.dgvRA_suppliers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRA_suppliers.Size = New System.Drawing.Size(270, 334)
        Me.dgvRA_suppliers.StandardTab = True
        Me.dgvRA_suppliers.TabIndex = 1
        '
        'FrameRADetails
        '
        Me.FrameRADetails.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameRADetails.Controls.Add(Me.btnRA_attachments)
        Me.FrameRADetails.Controls.Add(Me.txtFreightTrackNo)
        Me.FrameRADetails.Controls.Add(Me.Label29)
        Me.FrameRADetails.Controls.Add(Me.txtRACustomerContact)
        Me.FrameRADetails.Controls.Add(Me.txtRACat1)
        Me.FrameRADetails.Controls.Add(Me.txtRASupplierRANo)
        Me.FrameRADetails.Controls.Add(Me.cmdViewRecordRAs)
        Me.FrameRADetails.Controls.Add(Me.txtRAItem)
        Me.FrameRADetails.Controls.Add(Me.txtRAProblem)
        Me.FrameRADetails.Controls.Add(Me.txtRACreated)
        Me.FrameRADetails.Controls.Add(Me.txtRAUpdated)
        Me.FrameRADetails.Controls.Add(Me.txtRASerialNo)
        Me.FrameRADetails.Controls.Add(Me.txtRAProdBarcode)
        Me.FrameRADetails.Controls.Add(Me.txtRASupplier)
        Me.FrameRADetails.Controls.Add(Me.txtRACustomerName)
        Me.FrameRADetails.Controls.Add(Me.Label30)
        Me.FrameRADetails.Controls.Add(Me.LabRAStatusFriendly)
        Me.FrameRADetails.Controls.Add(Me.Label31)
        Me.FrameRADetails.Controls.Add(Me.LabRA_id)
        Me.FrameRADetails.Controls.Add(Me.Label32)
        Me.FrameRADetails.Controls.Add(Me.Label33)
        Me.FrameRADetails.Controls.Add(Me.Label34)
        Me.FrameRADetails.Controls.Add(Me.Label35)
        Me.FrameRADetails.Controls.Add(Me.Label36)
        Me.FrameRADetails.Controls.Add(Me.Label37)
        Me.FrameRADetails.Controls.Add(Me.labRASupplier)
        Me.FrameRADetails.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameRADetails.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameRADetails.Location = New System.Drawing.Point(657, 8)
        Me.FrameRADetails.Name = "FrameRADetails"
        Me.FrameRADetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameRADetails.Size = New System.Drawing.Size(329, 553)
        Me.FrameRADetails.TabIndex = 81
        Me.FrameRADetails.TabStop = False
        Me.FrameRADetails.Text = "FrameRADetails"
        '
        'btnRA_attachments
        '
        Me.btnRA_attachments.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnRA_attachments.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRA_attachments.Location = New System.Drawing.Point(242, 452)
        Me.btnRA_attachments.Name = "btnRA_attachments"
        Me.btnRA_attachments.Size = New System.Drawing.Size(78, 41)
        Me.btnRA_attachments.TabIndex = 58
        Me.btnRA_attachments.Text = "Attachments"
        Me.btnRA_attachments.UseVisualStyleBackColor = False
        '
        'txtFreightTrackNo
        '
        Me.txtFreightTrackNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtFreightTrackNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFreightTrackNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFreightTrackNo.Location = New System.Drawing.Point(103, 425)
        Me.txtFreightTrackNo.Multiline = True
        Me.txtFreightTrackNo.Name = "txtFreightTrackNo"
        Me.txtFreightTrackNo.ReadOnly = True
        Me.txtFreightTrackNo.Size = New System.Drawing.Size(209, 19)
        Me.txtFreightTrackNo.TabIndex = 57
        Me.txtFreightTrackNo.Text = "txtFreightTrackNo"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(13, 419)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(84, 29)
        Me.Label29.TabIndex = 56
        Me.Label29.Text = "Freight.  Tracking No:"
        '
        'txtRACustomerContact
        '
        Me.txtRACustomerContact.AcceptsReturn = True
        Me.txtRACustomerContact.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRACustomerContact.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRACustomerContact.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRACustomerContact.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRACustomerContact.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRACustomerContact.Location = New System.Drawing.Point(15, 502)
        Me.txtRACustomerContact.MaxLength = 0
        Me.txtRACustomerContact.Multiline = True
        Me.txtRACustomerContact.Name = "txtRACustomerContact"
        Me.txtRACustomerContact.ReadOnly = True
        Me.txtRACustomerContact.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRACustomerContact.Size = New System.Drawing.Size(218, 35)
        Me.txtRACustomerContact.TabIndex = 44
        Me.txtRACustomerContact.Text = "txtRACustomerContact" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txtRACat1
        '
        Me.txtRACat1.AcceptsReturn = True
        Me.txtRACat1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtRACat1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRACat1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRACat1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRACat1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRACat1.Location = New System.Drawing.Point(16, 24)
        Me.txtRACat1.MaxLength = 0
        Me.txtRACat1.Name = "txtRACat1"
        Me.txtRACat1.ReadOnly = True
        Me.txtRACat1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRACat1.Size = New System.Drawing.Size(121, 24)
        Me.txtRACat1.TabIndex = 43
        Me.txtRACat1.Text = "txtRACat1"
        '
        'txtRASupplierRANo
        '
        Me.txtRASupplierRANo.AcceptsReturn = True
        Me.txtRASupplierRANo.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRASupplierRANo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRASupplierRANo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRASupplierRANo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRASupplierRANo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRASupplierRANo.Location = New System.Drawing.Point(103, 387)
        Me.txtRASupplierRANo.MaxLength = 0
        Me.txtRASupplierRANo.Multiline = True
        Me.txtRASupplierRANo.Name = "txtRASupplierRANo"
        Me.txtRASupplierRANo.ReadOnly = True
        Me.txtRASupplierRANo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRASupplierRANo.Size = New System.Drawing.Size(210, 19)
        Me.txtRASupplierRANo.TabIndex = 42
        Me.txtRASupplierRANo.Text = "txtRASupplierRANo"
        '
        'cmdViewRecordRAs
        '
        Me.cmdViewRecordRAs.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.cmdViewRecordRAs.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdViewRecordRAs.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdViewRecordRAs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdViewRecordRAs.Location = New System.Drawing.Point(239, 498)
        Me.cmdViewRecordRAs.Name = "cmdViewRecordRAs"
        Me.cmdViewRecordRAs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdViewRecordRAs.Size = New System.Drawing.Size(81, 41)
        Me.cmdViewRecordRAs.TabIndex = 41
        Me.cmdViewRecordRAs.Text = "View or Update"
        Me.cmdViewRecordRAs.UseVisualStyleBackColor = False
        '
        'txtRAItem
        '
        Me.txtRAItem.AcceptsReturn = True
        Me.txtRAItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRAItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRAItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRAItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRAItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRAItem.Location = New System.Drawing.Point(16, 83)
        Me.txtRAItem.MaxLength = 0
        Me.txtRAItem.Multiline = True
        Me.txtRAItem.Name = "txtRAItem"
        Me.txtRAItem.ReadOnly = True
        Me.txtRAItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRAItem.Size = New System.Drawing.Size(297, 33)
        Me.txtRAItem.TabIndex = 40
        Me.txtRAItem.Text = "txtRAItem"
        '
        'txtRAProblem
        '
        Me.txtRAProblem.AcceptsReturn = True
        Me.txtRAProblem.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRAProblem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRAProblem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRAProblem.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRAProblem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRAProblem.Location = New System.Drawing.Point(16, 269)
        Me.txtRAProblem.MaxLength = 0
        Me.txtRAProblem.Multiline = True
        Me.txtRAProblem.Name = "txtRAProblem"
        Me.txtRAProblem.ReadOnly = True
        Me.txtRAProblem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRAProblem.Size = New System.Drawing.Size(297, 42)
        Me.txtRAProblem.TabIndex = 39
        Me.txtRAProblem.Text = "txtRAProblem"
        '
        'txtRACreated
        '
        Me.txtRACreated.AcceptsReturn = True
        Me.txtRACreated.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRACreated.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRACreated.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRACreated.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRACreated.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRACreated.Location = New System.Drawing.Point(16, 141)
        Me.txtRACreated.MaxLength = 0
        Me.txtRACreated.Multiline = True
        Me.txtRACreated.Name = "txtRACreated"
        Me.txtRACreated.ReadOnly = True
        Me.txtRACreated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRACreated.Size = New System.Drawing.Size(136, 17)
        Me.txtRACreated.TabIndex = 38
        Me.txtRACreated.Text = "txtRACreated"
        '
        'txtRAUpdated
        '
        Me.txtRAUpdated.AcceptsReturn = True
        Me.txtRAUpdated.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRAUpdated.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRAUpdated.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRAUpdated.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRAUpdated.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRAUpdated.Location = New System.Drawing.Point(176, 141)
        Me.txtRAUpdated.MaxLength = 0
        Me.txtRAUpdated.Multiline = True
        Me.txtRAUpdated.Name = "txtRAUpdated"
        Me.txtRAUpdated.ReadOnly = True
        Me.txtRAUpdated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRAUpdated.Size = New System.Drawing.Size(136, 17)
        Me.txtRAUpdated.TabIndex = 37
        Me.txtRAUpdated.Text = "txtRAUpdated"
        '
        'txtRASerialNo
        '
        Me.txtRASerialNo.AcceptsReturn = True
        Me.txtRASerialNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRASerialNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRASerialNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRASerialNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRASerialNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRASerialNo.Location = New System.Drawing.Point(16, 182)
        Me.txtRASerialNo.MaxLength = 0
        Me.txtRASerialNo.Name = "txtRASerialNo"
        Me.txtRASerialNo.ReadOnly = True
        Me.txtRASerialNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRASerialNo.Size = New System.Drawing.Size(297, 15)
        Me.txtRASerialNo.TabIndex = 36
        Me.txtRASerialNo.Text = "txtRASerialNo"
        '
        'txtRAProdBarcode
        '
        Me.txtRAProdBarcode.AcceptsReturn = True
        Me.txtRAProdBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRAProdBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRAProdBarcode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRAProdBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRAProdBarcode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRAProdBarcode.Location = New System.Drawing.Point(16, 226)
        Me.txtRAProdBarcode.MaxLength = 0
        Me.txtRAProdBarcode.Name = "txtRAProdBarcode"
        Me.txtRAProdBarcode.ReadOnly = True
        Me.txtRAProdBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRAProdBarcode.Size = New System.Drawing.Size(297, 15)
        Me.txtRAProdBarcode.TabIndex = 35
        Me.txtRAProdBarcode.Text = "txtRAProdBarcode"
        '
        'txtRASupplier
        '
        Me.txtRASupplier.AcceptsReturn = True
        Me.txtRASupplier.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRASupplier.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRASupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRASupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRASupplier.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRASupplier.Location = New System.Drawing.Point(16, 334)
        Me.txtRASupplier.MaxLength = 0
        Me.txtRASupplier.Multiline = True
        Me.txtRASupplier.Name = "txtRASupplier"
        Me.txtRASupplier.ReadOnly = True
        Me.txtRASupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRASupplier.Size = New System.Drawing.Size(297, 43)
        Me.txtRASupplier.TabIndex = 34
        Me.txtRASupplier.Text = "txtRASupplier"
        '
        'txtRACustomerName
        '
        Me.txtRACustomerName.AcceptsReturn = True
        Me.txtRACustomerName.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRACustomerName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRACustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRACustomerName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRACustomerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRACustomerName.Location = New System.Drawing.Point(15, 471)
        Me.txtRACustomerName.MaxLength = 0
        Me.txtRACustomerName.Multiline = True
        Me.txtRACustomerName.Name = "txtRACustomerName"
        Me.txtRACustomerName.ReadOnly = True
        Me.txtRACustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRACustomerName.Size = New System.Drawing.Size(218, 31)
        Me.txtRACustomerName.TabIndex = 33
        Me.txtRACustomerName.Text = "txtRACustomer" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(16, 456)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(136, 17)
        Me.Label30.TabIndex = 55
        Me.Label30.Text = "Customer Name:"
        '
        'LabRAStatusFriendly
        '
        Me.LabRAStatusFriendly.BackColor = System.Drawing.Color.Transparent
        Me.LabRAStatusFriendly.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRAStatusFriendly.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRAStatusFriendly.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRAStatusFriendly.Location = New System.Drawing.Point(168, 56)
        Me.LabRAStatusFriendly.Name = "LabRAStatusFriendly"
        Me.LabRAStatusFriendly.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRAStatusFriendly.Size = New System.Drawing.Size(153, 25)
        Me.LabRAStatusFriendly.TabIndex = 54
        Me.LabRAStatusFriendly.Text = "LabRAStatusFriendly"
        Me.LabRAStatusFriendly.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(13, 379)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(84, 34)
        Me.Label31.TabIndex = 53
        Me.Label31.Text = "Supplier    RA Number"
        '
        'LabRA_id
        '
        Me.LabRA_id.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.LabRA_id.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRA_id.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRA_id.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRA_id.Location = New System.Drawing.Point(178, 10)
        Me.LabRA_id.Name = "LabRA_id"
        Me.LabRA_id.Padding = New System.Windows.Forms.Padding(5, 5, 0, 0)
        Me.LabRA_id.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRA_id.Size = New System.Drawing.Size(134, 39)
        Me.LabRA_id.TabIndex = 52
        Me.LabRA_id.Text = "LabRA_id"
        Me.LabRA_id.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(16, 67)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(80, 17)
        Me.Label32.TabIndex = 51
        Me.Label32.Text = "Return Item:"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.Transparent
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(16, 253)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(177, 17)
        Me.Label33.TabIndex = 50
        Me.Label33.Text = "Problem Description:"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.Transparent
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(16, 125)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(57, 17)
        Me.Label34.TabIndex = 49
        Me.Label34.Text = "Created:"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.Transparent
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(176, 125)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(57, 17)
        Me.Label35.TabIndex = 48
        Me.Label35.Text = "Updated:"
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.Color.Transparent
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(16, 168)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(145, 17)
        Me.Label36.TabIndex = 47
        Me.Label36.Text = "Serial Number:"
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(16, 209)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(153, 17)
        Me.Label37.TabIndex = 46
        Me.Label37.Text = "Product Barcode:"
        '
        'labRASupplier
        '
        Me.labRASupplier.BackColor = System.Drawing.Color.Transparent
        Me.labRASupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRASupplier.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRASupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRASupplier.Location = New System.Drawing.Point(16, 318)
        Me.labRASupplier.Name = "labRASupplier"
        Me.labRASupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labRASupplier.Size = New System.Drawing.Size(297, 17)
        Me.labRASupplier.TabIndex = 45
        Me.labRASupplier.Text = "Supplier: <  >"
        '
        'labJobMatixRAs
        '
        Me.labJobMatixRAs.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJobMatixRAs.ForeColor = System.Drawing.Color.Firebrick
        Me.labJobMatixRAs.Location = New System.Drawing.Point(660, 92)
        Me.labJobMatixRAs.Name = "labJobMatixRAs"
        Me.labJobMatixRAs.Size = New System.Drawing.Size(127, 24)
        Me.labJobMatixRAs.TabIndex = 72
        Me.labJobMatixRAs.Text = "JobMatix RA's "
        Me.labJobMatixRAs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdNewRA
        '
        Me.cmdNewRA.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNewRA.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNewRA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdNewRA.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNewRA.Image = CType(resources.GetObject("cmdNewRA.Image"), System.Drawing.Image)
        Me.cmdNewRA.Location = New System.Drawing.Point(896, 87)
        Me.cmdNewRA.Name = "cmdNewRA"
        Me.cmdNewRA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNewRA.Size = New System.Drawing.Size(83, 33)
        Me.cmdNewRA.TabIndex = 71
        Me.cmdNewRA.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdNewRA, "Create a new RA record for an Item to be returned to Supplier..")
        Me.cmdNewRA.UseVisualStyleBackColor = False
        '
        'LabShowSearchRAs
        '
        Me.LabShowSearchRAs.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.LabShowSearchRAs.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabShowSearchRAs.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabShowSearchRAs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabShowSearchRAs.Location = New System.Drawing.Point(4, 87)
        Me.LabShowSearchRAs.Name = "LabShowSearchRAs"
        Me.LabShowSearchRAs.Padding = New System.Windows.Forms.Padding(30, 0, 30, 0)
        Me.LabShowSearchRAs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabShowSearchRAs.Size = New System.Drawing.Size(637, 33)
        Me.LabShowSearchRAs.TabIndex = 70
        Me.LabShowSearchRAs.Text = "Showing Active RAs"
        Me.LabShowSearchRAs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labVersion
        '
        Me.labVersion.Location = New System.Drawing.Point(11, 711)
        Me.labVersion.Name = "labVersion"
        Me.labVersion.Size = New System.Drawing.Size(226, 18)
        Me.labVersion.TabIndex = 73
        Me.labVersion.Text = "labVersion"
        '
        'Picture2
        '
        Me.Picture2.BackColor = System.Drawing.SystemColors.Control
        Me.Picture2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Picture2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Picture2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Picture2.Location = New System.Drawing.Point(483, 702)
        Me.Picture2.Name = "Picture2"
        Me.Picture2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture2.Size = New System.Drawing.Size(33, 17)
        Me.Picture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture2.TabIndex = 74
        Me.Picture2.TabStop = False
        '
        'TimerRAs
        '
        Me.TimerRAs.Interval = 1000
        '
        'frmRAs35Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(999, 724)
        Me.Controls.Add(Me.Picture2)
        Me.Controls.Add(Me.labVersion)
        Me.Controls.Add(Me.cmdNewRA)
        Me.Controls.Add(Me.labJobMatixRAs)
        Me.Controls.Add(Me.LabShowSearchRAs)
        Me.Controls.Add(Me.frameRAsTab)
        Me.Controls.Add(Me.panelHdr)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmRAs35Main"
        Me.Text = "frmRAs35Main"
        Me.panelHdr.ResumeLayout(False)
        Me.panelHdr.PerformLayout()
        CType(Me.picPOS_logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.frameRAsTab.ResumeLayout(False)
        Me.TabControlRAs.ResumeLayout(False)
        Me.TabPageRAsTree.ResumeLayout(False)
        Me.frameRAsTree.ResumeLayout(False)
        Me.TabPageRAsGrid.ResumeLayout(False)
        Me.FrameBrowseRAs.ResumeLayout(False)
        Me.FrameBrowseRAs.PerformLayout()
        CType(Me.DataGridViewRAs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolbarRAsGrid.ResumeLayout(False)
        Me.ToolbarRAsGrid.PerformLayout()
        Me.TabPageRAsSuppliers.ResumeLayout(False)
        Me.frameRA_suppliers.ResumeLayout(False)
        Me.frameRA_suppliers.PerformLayout()
        Me.panelRA_supplierInfo.ResumeLayout(False)
        Me.panelRA_supplierInfo.PerformLayout()
        CType(Me.dgvRA_suppliers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FrameRADetails.ResumeLayout(False)
        Me.FrameRADetails.PerformLayout()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents panelHdr As System.Windows.Forms.Panel
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents txtJetDBName As System.Windows.Forms.TextBox
    Public WithEvents labStatus As System.Windows.Forms.Label
    Public WithEvents txtStaff As System.Windows.Forms.TextBox
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LabBusiness As System.Windows.Forms.Label
    Public WithEvents labRetailDB As System.Windows.Forms.Label
    Public WithEvents LabServer As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents LabToday As System.Windows.Forms.Label
    Public WithEvents LabHdr0 As System.Windows.Forms.Label
    Public WithEvents LabAdmin As System.Windows.Forms.Label
    Friend WithEvents frameRAsTab As System.Windows.Forms.GroupBox
    Friend WithEvents TabControlRAs As System.Windows.Forms.TabControl
    Friend WithEvents TabPageRAsTree As System.Windows.Forms.TabPage
    Public WithEvents frameRAsTree As System.Windows.Forms.GroupBox
    Public WithEvents _OptRATreeSort_3 As System.Windows.Forms.RadioButton
    Public WithEvents _OptRATreeSort_2 As System.Windows.Forms.RadioButton
    Public WithEvents cmdRefreshRAsTree As System.Windows.Forms.Button
    Public WithEvents _OptRATreeSort_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptRATreeSort_0 As System.Windows.Forms.RadioButton
    Public WithEvents ChkAutoRefreshRAs As System.Windows.Forms.CheckBox
    Public WithEvents tvwRAs As System.Windows.Forms.TreeView
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents LabTreeStatusRAs As System.Windows.Forms.Label
    Friend WithEvents TabPageRAsGrid As System.Windows.Forms.TabPage
    Public WithEvents FrameBrowseRAs As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridViewRAs As System.Windows.Forms.DataGridView
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents txtFindRAs As System.Windows.Forms.TextBox
    Public WithEvents txtRASearch As System.Windows.Forms.TextBox
    Public WithEvents cmdClearRASearch As System.Windows.Forms.Button
    Public WithEvents cmdRASearch As System.Windows.Forms.Button
    Public WithEvents ToolbarRAsGrid As System.Windows.Forms.ToolStrip
    Public WithEvents _ToolbarRAs_ButtonQueued As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarRAs_ButtonRequested As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarRAs_ButtonGranted As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarRAs_ButtonShipped As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarRAs_ButtonCompleted As System.Windows.Forms.ToolStripButton
    Public WithEvents _ToolbarRAs_ButtonAll_RAs As System.Windows.Forms.ToolStripButton
    Public WithEvents LabRATitle As System.Windows.Forms.Label
    Public WithEvents LabFindRAs As System.Windows.Forms.Label
    Public WithEvents labRecCountRAs As System.Windows.Forms.Label
    Public WithEvents LabRASearch As System.Windows.Forms.Label
    Friend WithEvents TabPageRAsSuppliers As System.Windows.Forms.TabPage
    Friend WithEvents frameRA_suppliers As System.Windows.Forms.GroupBox
    Friend WithEvents panelRA_supplierInfo As System.Windows.Forms.Panel
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents cboRAs_A4Printers As System.Windows.Forms.ComboBox
    Friend WithEvents btnRAsUpdateGroupSent As System.Windows.Forms.Button
    Friend WithEvents txtRA_supplierName As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents chkSelectAllRAsGranted As System.Windows.Forms.CheckBox
    Public WithEvents txtFindSupplier As System.Windows.Forms.TextBox
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents labRecCountSupplier As System.Windows.Forms.Label
    Friend WithEvents chkShowGrantedRAsOnly As System.Windows.Forms.CheckBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents labFindSupplier As System.Windows.Forms.Label
    Friend WithEvents listViewSupplierRAs As System.Windows.Forms.ListView
    Friend WithEvents listViewRA_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents listViewRA_status As System.Windows.Forms.ColumnHeader
    Friend WithEvents listViewRA_SupplierRMA_No As System.Windows.Forms.ColumnHeader
    Friend WithEvents listViewRA_SerialNumber As System.Windows.Forms.ColumnHeader
    Friend WithEvents listViewRM_ItemCat1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents listViewRM_ItemDescription As System.Windows.Forms.ColumnHeader
    Friend WithEvents listViewRM_ItemSupplierCode As System.Windows.Forms.ColumnHeader
    Friend WithEvents dgvRA_suppliers As System.Windows.Forms.DataGridView
    Public WithEvents FrameRADetails As System.Windows.Forms.GroupBox
    Friend WithEvents btnRA_attachments As System.Windows.Forms.Button
    Friend WithEvents txtFreightTrackNo As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents txtRACustomerContact As System.Windows.Forms.TextBox
    Public WithEvents txtRACat1 As System.Windows.Forms.TextBox
    Public WithEvents txtRASupplierRANo As System.Windows.Forms.TextBox
    Public WithEvents cmdViewRecordRAs As System.Windows.Forms.Button
    Public WithEvents txtRAItem As System.Windows.Forms.TextBox
    Public WithEvents txtRAProblem As System.Windows.Forms.TextBox
    Public WithEvents txtRACreated As System.Windows.Forms.TextBox
    Public WithEvents txtRAUpdated As System.Windows.Forms.TextBox
    Public WithEvents txtRASerialNo As System.Windows.Forms.TextBox
    Public WithEvents txtRAProdBarcode As System.Windows.Forms.TextBox
    Public WithEvents txtRASupplier As System.Windows.Forms.TextBox
    Public WithEvents txtRACustomerName As System.Windows.Forms.TextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents LabRAStatusFriendly As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents LabRA_id As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents labRASupplier As System.Windows.Forms.Label
    Friend WithEvents labJobMatixRAs As System.Windows.Forms.Label
    Public WithEvents cmdNewRA As System.Windows.Forms.Button
    Public WithEvents LabShowSearchRAs As System.Windows.Forms.Label
    Friend WithEvents labVersion As System.Windows.Forms.Label
    Public WithEvents Picture2 As System.Windows.Forms.PictureBox
    Friend WithEvents TimerRAs As System.Windows.Forms.Timer
    Friend WithEvents picPOS_logo As System.Windows.Forms.PictureBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
