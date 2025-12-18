<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStartup
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
        Me.grpBoxDBs = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnSelectDB = New System.Windows.Forms.Button()
        Me.ListDBs = New System.Windows.Forms.ListBox()
        Me.grpBoxAdminExisting = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnMigrate = New System.Windows.Forms.Button()
        Me.btnRestore = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCreateRM = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labJobmatixVersion = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.labChoose = New System.Windows.Forms.Label()
        Me.labSqlVersion = New System.Windows.Forms.Label()
        Me.grpBoxAdminNew = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnCreatePOS = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageNew = New System.Windows.Forms.TabPage()
        Me.TabPageExisting = New System.Windows.Forms.TabPage()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.grpBoxDBs.SuspendLayout()
        Me.grpBoxAdminExisting.SuspendLayout()
        Me.grpBoxAdminNew.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageNew.SuspendLayout()
        Me.TabPageExisting.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpBoxDBs
        '
        Me.grpBoxDBs.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.grpBoxDBs.Controls.Add(Me.Label5)
        Me.grpBoxDBs.Controls.Add(Me.btnSelectDB)
        Me.grpBoxDBs.Controls.Add(Me.ListDBs)
        Me.grpBoxDBs.Location = New System.Drawing.Point(383, 6)
        Me.grpBoxDBs.Name = "grpBoxDBs"
        Me.grpBoxDBs.Size = New System.Drawing.Size(307, 219)
        Me.grpBoxDBs.TabIndex = 0
        Me.grpBoxDBs.TabStop = False
        Me.grpBoxDBs.Text = "grpBoxDBs"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(181, 14)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Current JobMatix Databases"
        '
        'btnSelectDB
        '
        Me.btnSelectDB.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSelectDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSelectDB.Location = New System.Drawing.Point(181, 172)
        Me.btnSelectDB.Name = "btnSelectDB"
        Me.btnSelectDB.Size = New System.Drawing.Size(103, 29)
        Me.btnSelectDB.TabIndex = 1
        Me.btnSelectDB.Text = "Open"
        Me.btnSelectDB.UseVisualStyleBackColor = False
        '
        'ListDBs
        '
        Me.ListDBs.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ListDBs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListDBs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListDBs.FormattingEnabled = True
        Me.ListDBs.Location = New System.Drawing.Point(9, 55)
        Me.ListDBs.Name = "ListDBs"
        Me.ListDBs.ScrollAlwaysVisible = True
        Me.ListDBs.Size = New System.Drawing.Size(275, 91)
        Me.ListDBs.TabIndex = 0
        '
        'grpBoxAdminExisting
        '
        Me.grpBoxAdminExisting.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.grpBoxAdminExisting.Controls.Add(Me.Label19)
        Me.grpBoxAdminExisting.Controls.Add(Me.Label11)
        Me.grpBoxAdminExisting.Controls.Add(Me.Label10)
        Me.grpBoxAdminExisting.Controls.Add(Me.Label9)
        Me.grpBoxAdminExisting.Controls.Add(Me.Label7)
        Me.grpBoxAdminExisting.Controls.Add(Me.Label6)
        Me.grpBoxAdminExisting.Controls.Add(Me.Label3)
        Me.grpBoxAdminExisting.Controls.Add(Me.btnMigrate)
        Me.grpBoxAdminExisting.Controls.Add(Me.btnRestore)
        Me.grpBoxAdminExisting.Location = New System.Drawing.Point(11, 232)
        Me.grpBoxAdminExisting.Name = "grpBoxAdminExisting"
        Me.grpBoxAdminExisting.Size = New System.Drawing.Size(679, 252)
        Me.grpBoxAdminExisting.TabIndex = 1
        Me.grpBoxAdminExisting.TabStop = False
        Me.grpBoxAdminExisting.Text = "grpBoxAdminExisting"
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(30, 201)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(236, 29)
        Me.Label19.TabIndex = 15
        Me.Label19.Text = "NB: This is enabled only when running on the Sql Server machine.."
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(25, 75)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(242, 22)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Restore JobMatixPOS. DB from Backup"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.LightGray
        Me.Label10.Location = New System.Drawing.Point(306, 75)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 155)
        Me.Label10.TabIndex = 13
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label9.Location = New System.Drawing.Point(24, 29)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(292, 18)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Admin- Options for existing JobMatix Users"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(345, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(235, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Upgrade (migrate) to JobMatixPOS.:"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(345, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(193, 94)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Restore and upgrade existing Jobmatix Database and migrate the DB to JobMatixPOS." &
    "." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "You will be importing current Supplier and Stock data from MYOB RetailManager" &
    " Database.."
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(25, 125)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(121, 67)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Restore a JobMatix database from a Jobmatix Sql Server Backup file.."
        '
        'btnMigrate
        '
        Me.btnMigrate.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMigrate.Location = New System.Drawing.Point(553, 125)
        Me.btnMigrate.Name = "btnMigrate"
        Me.btnMigrate.Size = New System.Drawing.Size(120, 73)
        Me.btnMigrate.TabIndex = 5
        Me.btnMigrate.Text = "Migrate JobMatix (RM) to JobMatix POS"
        Me.btnMigrate.UseVisualStyleBackColor = False
        '
        'btnRestore
        '
        Me.btnRestore.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnRestore.Location = New System.Drawing.Point(152, 125)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(120, 73)
        Me.btnRestore.TabIndex = 3
        Me.btnRestore.Text = "Restore Database from Backup"
        Me.btnRestore.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.LightGray
        Me.Label4.Location = New System.Drawing.Point(238, 161)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 228)
        Me.Label4.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(22, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(200, 52)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "NB:  The Stock/POS system must be either MYOB RetailManager or JobMatixPOS" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCreateRM
        '
        Me.btnCreateRM.BackColor = System.Drawing.Color.Lavender
        Me.btnCreateRM.Location = New System.Drawing.Point(25, 272)
        Me.btnCreateRM.Name = "btnCreateRM"
        Me.btnCreateRM.Size = New System.Drawing.Size(171, 88)
        Me.btnCreateRM.TabIndex = 2
        Me.btnCreateRM.Text = "New JobTracking Database (with MYOB RetailManager as Retail System)"
        Me.btnCreateRM.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(30, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(243, 27)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "JobMatix Startup Options"
        '
        'labJobmatixVersion
        '
        Me.labJobmatixVersion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labJobmatixVersion.Location = New System.Drawing.Point(9, 594)
        Me.labJobmatixVersion.Name = "labJobmatixVersion"
        Me.labJobmatixVersion.Size = New System.Drawing.Size(258, 17)
        Me.labJobmatixVersion.TabIndex = 3
        Me.labJobmatixVersion.Text = "labJobmatixVersion"
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancel.CausesValidation = False
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Location = New System.Drawing.Point(629, 18)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(83, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Cancel Startup")
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'labChoose
        '
        Me.labChoose.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.labChoose.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labChoose.Location = New System.Drawing.Point(8, 90)
        Me.labChoose.Name = "labChoose"
        Me.labChoose.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labChoose.Size = New System.Drawing.Size(369, 135)
        Me.labChoose.TabIndex = 4
        Me.labChoose.Text = "Choose from:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  - Select a JobMatix database to open, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  - Restore from a Job" &
    "Matix Database Backup, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "    - or Migrate JobMatix from MYOB RM to JobMatixPOS" &
    ""
        Me.labChoose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'labSqlVersion
        '
        Me.labSqlVersion.BackColor = System.Drawing.Color.WhiteSmoke
        Me.labSqlVersion.ForeColor = System.Drawing.Color.DarkBlue
        Me.labSqlVersion.Location = New System.Drawing.Point(293, 13)
        Me.labSqlVersion.Name = "labSqlVersion"
        Me.labSqlVersion.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labSqlVersion.Size = New System.Drawing.Size(278, 54)
        Me.labSqlVersion.TabIndex = 6
        Me.labSqlVersion.Text = "labSqlVersion"
        '
        'grpBoxAdminNew
        '
        Me.grpBoxAdminNew.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpBoxAdminNew.Controls.Add(Me.Label16)
        Me.grpBoxAdminNew.Controls.Add(Me.Label17)
        Me.grpBoxAdminNew.Controls.Add(Me.Label15)
        Me.grpBoxAdminNew.Controls.Add(Me.Label14)
        Me.grpBoxAdminNew.Controls.Add(Me.Label13)
        Me.grpBoxAdminNew.Controls.Add(Me.Label12)
        Me.grpBoxAdminNew.Controls.Add(Me.btnCreatePOS)
        Me.grpBoxAdminNew.Controls.Add(Me.Label8)
        Me.grpBoxAdminNew.Controls.Add(Me.Label2)
        Me.grpBoxAdminNew.Controls.Add(Me.btnCreateRM)
        Me.grpBoxAdminNew.Controls.Add(Me.Label4)
        Me.grpBoxAdminNew.Location = New System.Drawing.Point(3, 3)
        Me.grpBoxAdminNew.Name = "grpBoxAdminNew"
        Me.grpBoxAdminNew.Size = New System.Drawing.Size(684, 481)
        Me.grpBoxAdminNew.TabIndex = 7
        Me.grpBoxAdminNew.TabStop = False
        Me.grpBoxAdminNew.Text = "grpBoxAdminNew"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(304, 161)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 14)
        Me.Label16.TabIndex = 16
        Me.Label16.Text = "All New Users"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(308, 197)
        Me.Label17.Name = "Label17"
        Me.Label17.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.Label17.Size = New System.Drawing.Size(180, 60)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "Start new JobTracking Database (with JobMatix POS as Retail/Stock System)"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(22, 161)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(174, 14)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "MYOB Retail Manager Users"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(26, 197)
        Me.Label14.Name = "Label14"
        Me.Label14.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.Label14.Size = New System.Drawing.Size(180, 60)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "Start a new JobTracking Database (and continue with MYOB RetailManager as Retail " &
    "Stock system)"
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(304, 373)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(308, 91)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "MYOB RetailManager Users:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " After creating new DB, You can import Initial (late" &
    "st) Supplier and Stock data from your current MYOB RetailManager Database"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 10.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(20, 57)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(260, 27)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Create a new JobMatix Database.. "
        '
        'btnCreatePOS
        '
        Me.btnCreatePOS.BackColor = System.Drawing.Color.Lavender
        Me.btnCreatePOS.Location = New System.Drawing.Point(307, 272)
        Me.btnCreatePOS.Name = "btnCreatePOS"
        Me.btnCreatePOS.Size = New System.Drawing.Size(171, 88)
        Me.btnCreatePOS.TabIndex = 10
        Me.btnCreatePOS.Text = "New JobTracking Database (with JobMatix POS as Retail System)"
        Me.btnCreatePOS.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label8.Location = New System.Drawing.Point(22, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(226, 23)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "New JobMatix Users"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageNew)
        Me.TabControl1.Controls.Add(Me.TabPageExisting)
        Me.TabControl1.Location = New System.Drawing.Point(12, 75)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(704, 516)
        Me.TabControl1.TabIndex = 8
        '
        'TabPageNew
        '
        Me.TabPageNew.BackColor = System.Drawing.Color.White
        Me.TabPageNew.Controls.Add(Me.grpBoxAdminNew)
        Me.TabPageNew.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageNew.Location = New System.Drawing.Point(4, 22)
        Me.TabPageNew.Name = "TabPageNew"
        Me.TabPageNew.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageNew.Size = New System.Drawing.Size(696, 490)
        Me.TabPageNew.TabIndex = 0
        Me.TabPageNew.Text = "New Users"
        '
        'TabPageExisting
        '
        Me.TabPageExisting.BackColor = System.Drawing.Color.White
        Me.TabPageExisting.Controls.Add(Me.Label18)
        Me.TabPageExisting.Controls.Add(Me.grpBoxDBs)
        Me.TabPageExisting.Controls.Add(Me.grpBoxAdminExisting)
        Me.TabPageExisting.Controls.Add(Me.labChoose)
        Me.TabPageExisting.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPageExisting.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExisting.Name = "TabPageExisting"
        Me.TabPageExisting.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageExisting.Size = New System.Drawing.Size(696, 490)
        Me.TabPageExisting.TabIndex = 1
        Me.TabPageExisting.Text = "Existing Users"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label18.Location = New System.Drawing.Point(8, 6)
        Me.Label18.Name = "Label18"
        Me.Label18.Padding = New System.Windows.Forms.Padding(7, 7, 0, 0)
        Me.Label18.Size = New System.Drawing.Size(369, 78)
        Me.Label18.TabIndex = 15
        Me.Label18.Text = "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "  For Existing JobMatix Users"
        '
        'frmStartup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(728, 611)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.labSqlVersion)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.labJobmatixVersion)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmStartup"
        Me.Text = "frmStartup"
        Me.grpBoxDBs.ResumeLayout(False)
        Me.grpBoxDBs.PerformLayout()
        Me.grpBoxAdminExisting.ResumeLayout(False)
        Me.grpBoxAdminNew.ResumeLayout(False)
        Me.grpBoxAdminNew.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageNew.ResumeLayout(False)
        Me.TabPageExisting.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpBoxDBs As System.Windows.Forms.GroupBox
    Friend WithEvents btnSelectDB As System.Windows.Forms.Button
    Friend WithEvents ListDBs As System.Windows.Forms.ListBox
    Friend WithEvents grpBoxAdminExisting As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCreateRM As System.Windows.Forms.Button
    Friend WithEvents labJobmatixVersion As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnRestore As System.Windows.Forms.Button
    Friend WithEvents labChoose As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents labSqlVersion As System.Windows.Forms.Label
    Friend WithEvents btnMigrate As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents grpBoxAdminNew As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnCreatePOS As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageNew As System.Windows.Forms.TabPage
    Friend WithEvents TabPageExisting As System.Windows.Forms.TabPage
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
End Class
