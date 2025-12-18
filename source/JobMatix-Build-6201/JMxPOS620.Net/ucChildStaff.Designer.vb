<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildStaff
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpBoxStaff = New System.Windows.Forms.GroupBox()
        Me.panelStaffDetail = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPosition = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDocketName = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.panelStaffHdr = New System.Windows.Forms.Panel()
        Me.BarcodeLabel = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.labStaffName = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.panelBanner = New System.Windows.Forms.Panel()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.labGettingData = New System.Windows.Forms.Label()
        Me.labHdr1 = New System.Windows.Forms.Label()
        Me.FrameBrowse = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmdClearStaffSearch = New System.Windows.Forms.Button()
        Me.cmdStaffSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtStaffSearch = New System.Windows.Forms.TextBox()
        Me.dgvStaffList = New System.Windows.Forms.DataGridView()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.labRecCount = New System.Windows.Forms.Label()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpBoxStaff.SuspendLayout()
        Me.panelStaffDetail.SuspendLayout()
        Me.panelStaffHdr.SuspendLayout()
        Me.panelBanner.SuspendLayout()
        Me.FrameBrowse.SuspendLayout()
        CType(Me.dgvStaffList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpBoxStaff
        '
        Me.grpBoxStaff.BackColor = System.Drawing.Color.White
        Me.grpBoxStaff.Controls.Add(Me.panelStaffDetail)
        Me.grpBoxStaff.Controls.Add(Me.panelStaffHdr)
        Me.grpBoxStaff.Controls.Add(Me.panelBanner)
        Me.grpBoxStaff.Controls.Add(Me.FrameBrowse)
        Me.grpBoxStaff.Location = New System.Drawing.Point(6, 5)
        Me.grpBoxStaff.Name = "grpBoxStaff"
        Me.grpBoxStaff.Size = New System.Drawing.Size(990, 631)
        Me.grpBoxStaff.TabIndex = 0
        Me.grpBoxStaff.TabStop = False
        Me.grpBoxStaff.Text = "grpBoxSupplier"
        '
        'panelStaffDetail
        '
        Me.panelStaffDetail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelStaffDetail.Controls.Add(Me.Label1)
        Me.panelStaffDetail.Controls.Add(Me.txtPosition)
        Me.panelStaffDetail.Controls.Add(Me.txtAddress)
        Me.panelStaffDetail.Controls.Add(Me.Label4)
        Me.panelStaffDetail.Controls.Add(Me.Label6)
        Me.panelStaffDetail.Controls.Add(Me.txtDocketName)
        Me.panelStaffDetail.Controls.Add(Me.txtEmail)
        Me.panelStaffDetail.Controls.Add(Me.Label29)
        Me.panelStaffDetail.Controls.Add(Me.txtMobile)
        Me.panelStaffDetail.Controls.Add(Me.Label28)
        Me.panelStaffDetail.Controls.Add(Me.txtPhone)
        Me.panelStaffDetail.Controls.Add(Me.Label26)
        Me.panelStaffDetail.Controls.Add(Me.btnEdit)
        Me.panelStaffDetail.Location = New System.Drawing.Point(653, 141)
        Me.panelStaffDetail.Name = "panelStaffDetail"
        Me.panelStaffDetail.Size = New System.Drawing.Size(327, 479)
        Me.panelStaffDetail.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 114)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 92
        Me.Label1.Text = "Position"
        '
        'txtPosition
        '
        Me.txtPosition.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPosition.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPosition.Location = New System.Drawing.Point(17, 131)
        Me.txtPosition.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtPosition.MaxLength = 50
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.ReadOnly = True
        Me.txtPosition.Size = New System.Drawing.Size(296, 19)
        Me.txtPosition.TabIndex = 3
        Me.txtPosition.Text = "txtPosition"
        Me.txtPosition.WordWrap = False
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAddress.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(16, 263)
        Me.txtAddress.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtAddress.MaxLength = 200
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(296, 65)
        Me.txtAddress.TabIndex = 6
        Me.txtAddress.Text = "txtAddress"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 246)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 13)
        Me.Label4.TabIndex = 90
        Me.Label4.Text = "Address"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(13, 61)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 13)
        Me.Label6.TabIndex = 84
        Me.Label6.Text = "Docket Name"
        '
        'txtDocketName
        '
        Me.txtDocketName.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtDocketName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtDocketName.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocketName.Location = New System.Drawing.Point(16, 78)
        Me.txtDocketName.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtDocketName.MaxLength = 50
        Me.txtDocketName.Name = "txtDocketName"
        Me.txtDocketName.ReadOnly = True
        Me.txtDocketName.Size = New System.Drawing.Size(296, 19)
        Me.txtDocketName.TabIndex = 2
        Me.txtDocketName.Text = "txtDocketName"
        Me.txtDocketName.WordWrap = False
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtEmail.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(16, 349)
        Me.txtEmail.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtEmail.MaxLength = 200
        Me.txtEmail.Multiline = True
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReadOnly = True
        Me.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtEmail.Size = New System.Drawing.Size(296, 37)
        Me.txtEmail.TabIndex = 7
        Me.txtEmail.Text = "txtEmail"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(13, 332)
        Me.Label29.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(61, 13)
        Me.Label29.TabIndex = 82
        Me.Label29.Text = "Email"
        '
        'txtMobile
        '
        Me.txtMobile.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMobile.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.Location = New System.Drawing.Point(16, 217)
        Me.txtMobile.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtMobile.MaxLength = 20
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.ReadOnly = True
        Me.txtMobile.Size = New System.Drawing.Size(220, 19)
        Me.txtMobile.TabIndex = 5
        Me.txtMobile.Text = "txtMobile"
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(13, 200)
        Me.Label28.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(65, 13)
        Me.Label28.TabIndex = 80
        Me.Label28.Text = "Mobile"
        '
        'txtPhone
        '
        Me.txtPhone.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPhone.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.Location = New System.Drawing.Point(16, 177)
        Me.txtPhone.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtPhone.MaxLength = 20
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.ReadOnly = True
        Me.txtPhone.Size = New System.Drawing.Size(220, 19)
        Me.txtPhone.TabIndex = 4
        Me.txtPhone.Text = "txtPhone"
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(13, 160)
        Me.Label26.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(65, 13)
        Me.Label26.TabIndex = 76
        Me.Label26.Text = "Phone"
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Location = New System.Drawing.Point(232, 20)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(67, 33)
        Me.btnEdit.TabIndex = 0
        Me.btnEdit.Text = "Edit"
        Me.ToolTip1.SetToolTip(Me.btnEdit, "Edit Supplier Details.")
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'panelStaffHdr
        '
        Me.panelStaffHdr.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelStaffHdr.Controls.Add(Me.BarcodeLabel)
        Me.panelStaffHdr.Controls.Add(Me.btnNew)
        Me.panelStaffHdr.Controls.Add(Me.txtBarcode)
        Me.panelStaffHdr.Controls.Add(Me.labStaffName)
        Me.panelStaffHdr.Controls.Add(Me.Label14)
        Me.panelStaffHdr.Location = New System.Drawing.Point(653, 62)
        Me.panelStaffHdr.Name = "panelStaffHdr"
        Me.panelStaffHdr.Size = New System.Drawing.Size(327, 75)
        Me.panelStaffHdr.TabIndex = 25
        '
        'BarcodeLabel
        '
        Me.BarcodeLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarcodeLabel.ForeColor = System.Drawing.Color.Gray
        Me.BarcodeLabel.Location = New System.Drawing.Point(109, 5)
        Me.BarcodeLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.BarcodeLabel.Name = "BarcodeLabel"
        Me.BarcodeLabel.Size = New System.Drawing.Size(54, 17)
        Me.BarcodeLabel.TabIndex = 25
        Me.BarcodeLabel.Text = "Barcode"
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.LavenderBlush
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNew.Location = New System.Drawing.Point(232, 6)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(67, 28)
        Me.btnNew.TabIndex = 7
        Me.btnNew.Text = "New"
        Me.ToolTip1.SetToolTip(Me.btnNew, "Create New Supplier Record..")
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'txtBarcode
        '
        Me.txtBarcode.BackColor = System.Drawing.Color.LightGray
        Me.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtBarcode.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcode.Location = New System.Drawing.Point(110, 20)
        Me.txtBarcode.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtBarcode.MaxLength = 15
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.ReadOnly = True
        Me.txtBarcode.Size = New System.Drawing.Size(53, 19)
        Me.txtBarcode.TabIndex = 24
        Me.txtBarcode.TabStop = False
        '
        'labStaffName
        '
        Me.labStaffName.Font = New System.Drawing.Font("Lucida Sans Unicode", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStaffName.Location = New System.Drawing.Point(3, 43)
        Me.labStaffName.Name = "labStaffName"
        Me.labStaffName.Size = New System.Drawing.Size(309, 26)
        Me.labStaffName.TabIndex = 22
        Me.labStaffName.Text = "labStaffName"
        Me.labStaffName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 18)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Staff Member: "
        '
        'panelBanner
        '
        Me.panelBanner.BackColor = System.Drawing.Color.AliceBlue
        Me.panelBanner.Controls.Add(Me.btnExit)
        Me.panelBanner.Controls.Add(Me.labGettingData)
        Me.panelBanner.Controls.Add(Me.labHdr1)
        Me.panelBanner.Location = New System.Drawing.Point(3, 4)
        Me.panelBanner.Name = "panelBanner"
        Me.panelBanner.Size = New System.Drawing.Size(977, 50)
        Me.panelBanner.TabIndex = 23
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnExit.CausesValidation = False
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Location = New System.Drawing.Point(882, 11)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(66, 28)
        Me.btnExit.TabIndex = 46
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'labGettingData
        '
        Me.labGettingData.BackColor = System.Drawing.Color.Transparent
        Me.labGettingData.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labGettingData.ForeColor = System.Drawing.Color.DarkOrange
        Me.labGettingData.Location = New System.Drawing.Point(419, 9)
        Me.labGettingData.Name = "labGettingData"
        Me.labGettingData.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labGettingData.Size = New System.Drawing.Size(114, 33)
        Me.labGettingData.TabIndex = 2
        Me.labGettingData.Text = "Wait-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Getting Data.."
        '
        'labHdr1
        '
        Me.labHdr1.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHdr1.ForeColor = System.Drawing.Color.MediumBlue
        Me.labHdr1.Location = New System.Drawing.Point(18, 13)
        Me.labHdr1.Name = "labHdr1"
        Me.labHdr1.Size = New System.Drawing.Size(395, 23)
        Me.labHdr1.TabIndex = 0
        Me.labHdr1.Text = "S t a f f    A d m i n"
        '
        'FrameBrowse
        '
        Me.FrameBrowse.BackColor = System.Drawing.Color.White
        Me.FrameBrowse.Controls.Add(Me.Label22)
        Me.FrameBrowse.Controls.Add(Me.Label21)
        Me.FrameBrowse.Controls.Add(Me.cmdClearStaffSearch)
        Me.FrameBrowse.Controls.Add(Me.cmdStaffSearch)
        Me.FrameBrowse.Controls.Add(Me.Label3)
        Me.FrameBrowse.Controls.Add(Me.txtStaffSearch)
        Me.FrameBrowse.Controls.Add(Me.dgvStaffList)
        Me.FrameBrowse.Controls.Add(Me.txtFind)
        Me.FrameBrowse.Controls.Add(Me.labRecCount)
        Me.FrameBrowse.Controls.Add(Me.LabFind)
        Me.FrameBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameBrowse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameBrowse.Location = New System.Drawing.Point(3, 55)
        Me.FrameBrowse.Name = "FrameBrowse"
        Me.FrameBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameBrowse.Size = New System.Drawing.Size(644, 567)
        Me.FrameBrowse.TabIndex = 1
        Me.FrameBrowse.TabStop = False
        Me.FrameBrowse.Text = "FrameBrowse"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(259, 16)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(83, 13)
        Me.Label22.TabIndex = 82
        Me.Label22.Text = "Records found."
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(310, 50)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(121, 12)
        Me.Label21.TabIndex = 81
        Me.Label21.Text = "Full Text Filter (Srch):"
        '
        'cmdClearStaffSearch
        '
        Me.cmdClearStaffSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClearStaffSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearStaffSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClearStaffSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearStaffSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearStaffSearch.Location = New System.Drawing.Point(564, 29)
        Me.cmdClearStaffSearch.Name = "cmdClearStaffSearch"
        Me.cmdClearStaffSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearStaffSearch.Size = New System.Drawing.Size(68, 23)
        Me.cmdClearStaffSearch.TabIndex = 80
        Me.cmdClearStaffSearch.Text = "Clear"
        Me.cmdClearStaffSearch.UseVisualStyleBackColor = False
        '
        'cmdStaffSearch
        '
        Me.cmdStaffSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdStaffSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdStaffSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdStaffSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStaffSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdStaffSearch.Location = New System.Drawing.Point(564, 59)
        Me.cmdStaffSearch.Name = "cmdStaffSearch"
        Me.cmdStaffSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdStaffSearch.Size = New System.Drawing.Size(68, 23)
        Me.cmdStaffSearch.TabIndex = 79
        Me.cmdStaffSearch.Text = "Search"
        Me.cmdStaffSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdStaffSearch.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Staff Members"
        '
        'txtStaffSearch
        '
        Me.txtStaffSearch.AcceptsReturn = True
        Me.txtStaffSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtStaffSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStaffSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStaffSearch.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStaffSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStaffSearch.Location = New System.Drawing.Point(312, 67)
        Me.txtStaffSearch.MaxLength = 0
        Me.txtStaffSearch.Name = "txtStaffSearch"
        Me.txtStaffSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStaffSearch.Size = New System.Drawing.Size(234, 19)
        Me.txtStaffSearch.TabIndex = 78
        Me.txtStaffSearch.Text = "txtStaffSearch"
        '
        'dgvStaffList
        '
        Me.dgvStaffList.AllowUserToAddRows = False
        Me.dgvStaffList.AllowUserToDeleteRows = False
        Me.dgvStaffList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvStaffList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvStaffList.ColumnHeadersHeight = 18
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvStaffList.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvStaffList.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvStaffList.Location = New System.Drawing.Point(6, 91)
        Me.dgvStaffList.MultiSelect = False
        Me.dgvStaffList.Name = "dgvStaffList"
        Me.dgvStaffList.ReadOnly = True
        Me.dgvStaffList.RowHeadersWidth = 17
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvStaffList.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvStaffList.RowTemplate.Height = 17
        Me.dgvStaffList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvStaffList.Size = New System.Drawing.Size(626, 470)
        Me.dgvStaffList.StandardTab = True
        Me.dgvStaffList.TabIndex = 4
        '
        'txtFind
        '
        Me.txtFind.AcceptsReturn = True
        Me.txtFind.BackColor = System.Drawing.Color.Gainsboro
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFind.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFind.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFind.Location = New System.Drawing.Point(6, 67)
        Me.txtFind.MaxLength = 0
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(136, 14)
        Me.txtFind.TabIndex = 2
        '
        'labRecCount
        '
        Me.labRecCount.BackColor = System.Drawing.Color.Transparent
        Me.labRecCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.labRecCount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labRecCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labRecCount.Location = New System.Drawing.Point(209, 14)
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
        Me.LabFind.Location = New System.Drawing.Point(7, 40)
        Me.LabFind.Name = "LabFind"
        Me.LabFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabFind.Size = New System.Drawing.Size(135, 25)
        Me.LabFind.TabIndex = 18
        Me.LabFind.Text = "LabFind"
        '
        'ucChildStaff
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.Controls.Add(Me.grpBoxStaff)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildStaff"
        Me.Size = New System.Drawing.Size(1011, 650)
        Me.grpBoxStaff.ResumeLayout(False)
        Me.panelStaffDetail.ResumeLayout(False)
        Me.panelStaffDetail.PerformLayout()
        Me.panelStaffHdr.ResumeLayout(False)
        Me.panelStaffHdr.PerformLayout()
        Me.panelBanner.ResumeLayout(False)
        Me.FrameBrowse.ResumeLayout(False)
        Me.FrameBrowse.PerformLayout()
        CType(Me.dgvStaffList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpBoxStaff As GroupBox
    Public WithEvents FrameBrowse As GroupBox
    Public WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Public WithEvents cmdClearStaffSearch As Button
    Public WithEvents cmdStaffSearch As Button
    Friend WithEvents Label3 As Label
    Public WithEvents txtStaffSearch As TextBox
    Friend WithEvents dgvStaffList As DataGridView
    Public WithEvents txtFind As TextBox
    Public WithEvents labRecCount As Label
    Public WithEvents LabFind As Label
    Friend WithEvents panelBanner As Panel
    Friend WithEvents labGettingData As Label
    Friend WithEvents labHdr1 As Label
    Friend WithEvents panelStaffHdr As Panel
    Friend WithEvents labStaffName As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents panelStaffDetail As Panel
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents BarcodeLabel As Label
    Friend WithEvents txtBarcode As TextBox
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents txtMobile As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtDocketName As TextBox
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPosition As TextBox
End Class
