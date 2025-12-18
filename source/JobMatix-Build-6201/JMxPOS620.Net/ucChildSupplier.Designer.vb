<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucChildSupplier
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.grpBoxSupplier = New System.Windows.Forms.GroupBox()
        Me.panelSupplierDetail = New System.Windows.Forms.Panel()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtWebsite = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtContactName = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.btnLookupGoods = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.panelSupplierHdr = New System.Windows.Forms.Panel()
        Me.BarcodeLabel = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.txtBarcode = New System.Windows.Forms.TextBox()
        Me.labSupplierName = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.panelBanner = New System.Windows.Forms.Panel()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.labGettingData = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.FrameBrowse = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmdClearSupplierSearch = New System.Windows.Forms.Button()
        Me.cmdSupplierSearch = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSupplierSearch = New System.Windows.Forms.TextBox()
        Me.dgvSupplierList = New System.Windows.Forms.DataGridView()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.labRecCount = New System.Windows.Forms.Label()
        Me.LabFind = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grpBoxSupplier.SuspendLayout()
        Me.panelSupplierDetail.SuspendLayout()
        Me.panelSupplierHdr.SuspendLayout()
        Me.panelBanner.SuspendLayout()
        Me.FrameBrowse.SuspendLayout()
        CType(Me.dgvSupplierList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpBoxSupplier
        '
        Me.grpBoxSupplier.BackColor = System.Drawing.Color.White
        Me.grpBoxSupplier.Controls.Add(Me.panelSupplierDetail)
        Me.grpBoxSupplier.Controls.Add(Me.panelSupplierHdr)
        Me.grpBoxSupplier.Controls.Add(Me.panelBanner)
        Me.grpBoxSupplier.Controls.Add(Me.FrameBrowse)
        Me.grpBoxSupplier.Location = New System.Drawing.Point(6, 5)
        Me.grpBoxSupplier.Name = "grpBoxSupplier"
        Me.grpBoxSupplier.Size = New System.Drawing.Size(990, 631)
        Me.grpBoxSupplier.TabIndex = 0
        Me.grpBoxSupplier.TabStop = False
        Me.grpBoxSupplier.Text = "grpBoxSupplier"
        '
        'panelSupplierDetail
        '
        Me.panelSupplierDetail.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelSupplierDetail.Controls.Add(Me.txtAddress)
        Me.panelSupplierDetail.Controls.Add(Me.Label4)
        Me.panelSupplierDetail.Controls.Add(Me.txtComments)
        Me.panelSupplierDetail.Controls.Add(Me.Label2)
        Me.panelSupplierDetail.Controls.Add(Me.txtWebsite)
        Me.panelSupplierDetail.Controls.Add(Me.Label1)
        Me.panelSupplierDetail.Controls.Add(Me.Label6)
        Me.panelSupplierDetail.Controls.Add(Me.txtContactName)
        Me.panelSupplierDetail.Controls.Add(Me.txtEmail)
        Me.panelSupplierDetail.Controls.Add(Me.Label29)
        Me.panelSupplierDetail.Controls.Add(Me.txtFax)
        Me.panelSupplierDetail.Controls.Add(Me.Label28)
        Me.panelSupplierDetail.Controls.Add(Me.txtPhone)
        Me.panelSupplierDetail.Controls.Add(Me.Label26)
        Me.panelSupplierDetail.Controls.Add(Me.btnLookupGoods)
        Me.panelSupplierDetail.Controls.Add(Me.btnEdit)
        Me.panelSupplierDetail.Location = New System.Drawing.Point(653, 141)
        Me.panelSupplierDetail.Name = "panelSupplierDetail"
        Me.panelSupplierDetail.Size = New System.Drawing.Size(327, 479)
        Me.panelSupplierDetail.TabIndex = 26
        '
        'txtAddress
        '
        Me.txtAddress.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAddress.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(16, 180)
        Me.txtAddress.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtAddress.MaxLength = 200
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAddress.Size = New System.Drawing.Size(296, 65)
        Me.txtAddress.TabIndex = 5
        Me.txtAddress.Text = "txtAddress"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 163)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 13)
        Me.Label4.TabIndex = 90
        Me.Label4.Text = "Address"
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtComments.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.Location = New System.Drawing.Point(16, 392)
        Me.txtComments.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtComments.MaxLength = 200
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(296, 77)
        Me.txtComments.TabIndex = 8
        Me.txtComments.Text = "txtComments"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 375)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 88
        Me.Label2.Text = "Comments"
        '
        'txtWebsite
        '
        Me.txtWebsite.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtWebsite.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtWebsite.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWebsite.Location = New System.Drawing.Point(16, 327)
        Me.txtWebsite.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtWebsite.MaxLength = 200
        Me.txtWebsite.Multiline = True
        Me.txtWebsite.Name = "txtWebsite"
        Me.txtWebsite.ReadOnly = True
        Me.txtWebsite.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtWebsite.Size = New System.Drawing.Size(296, 37)
        Me.txtWebsite.TabIndex = 7
        Me.txtWebsite.Text = "txtWebsite"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 310)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 86
        Me.Label1.Text = "Website"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(13, 67)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 13)
        Me.Label6.TabIndex = 84
        Me.Label6.Text = "Contact Name"
        '
        'txtContactName
        '
        Me.txtContactName.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtContactName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtContactName.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContactName.Location = New System.Drawing.Point(16, 84)
        Me.txtContactName.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtContactName.MaxLength = 50
        Me.txtContactName.Name = "txtContactName"
        Me.txtContactName.ReadOnly = True
        Me.txtContactName.Size = New System.Drawing.Size(296, 19)
        Me.txtContactName.TabIndex = 2
        Me.txtContactName.Text = "txtContactName"
        Me.txtContactName.WordWrap = False
        '
        'txtEmail
        '
        Me.txtEmail.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtEmail.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(16, 266)
        Me.txtEmail.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtEmail.MaxLength = 200
        Me.txtEmail.Multiline = True
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.ReadOnly = True
        Me.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtEmail.Size = New System.Drawing.Size(296, 37)
        Me.txtEmail.TabIndex = 6
        Me.txtEmail.Text = "txtEmail"
        '
        'Label29
        '
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(13, 249)
        Me.Label29.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(61, 13)
        Me.Label29.TabIndex = 82
        Me.Label29.Text = "Email"
        '
        'txtFax
        '
        Me.txtFax.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFax.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.Location = New System.Drawing.Point(172, 133)
        Me.txtFax.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtFax.MaxLength = 20
        Me.txtFax.Name = "txtFax"
        Me.txtFax.ReadOnly = True
        Me.txtFax.Size = New System.Drawing.Size(148, 19)
        Me.txtFax.TabIndex = 4
        Me.txtFax.Text = "txtFax"
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(169, 116)
        Me.Label28.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(65, 13)
        Me.Label28.TabIndex = 80
        Me.Label28.Text = "Fax"
        '
        'txtPhone
        '
        Me.txtPhone.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPhone.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.Location = New System.Drawing.Point(16, 133)
        Me.txtPhone.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtPhone.MaxLength = 20
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.ReadOnly = True
        Me.txtPhone.Size = New System.Drawing.Size(148, 19)
        Me.txtPhone.TabIndex = 3
        Me.txtPhone.Text = "txtPhone"
        '
        'Label26
        '
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(13, 116)
        Me.Label26.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(65, 13)
        Me.Label26.TabIndex = 76
        Me.Label26.Text = "Phone"
        '
        'btnLookupGoods
        '
        Me.btnLookupGoods.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.btnLookupGoods.CausesValidation = False
        Me.btnLookupGoods.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLookupGoods.Location = New System.Drawing.Point(83, 20)
        Me.btnLookupGoods.Name = "btnLookupGoods"
        Me.btnLookupGoods.Size = New System.Drawing.Size(89, 33)
        Me.btnLookupGoods.TabIndex = 0
        Me.btnLookupGoods.Text = "Lookup Goods"
        Me.ToolTip1.SetToolTip(Me.btnLookupGoods, "Lookup all Goods Received from this Supplier..")
        Me.btnLookupGoods.UseVisualStyleBackColor = False
        '
        'btnEdit
        '
        Me.btnEdit.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEdit.Location = New System.Drawing.Point(232, 20)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(67, 33)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "Edit"
        Me.ToolTip1.SetToolTip(Me.btnEdit, "Edit Supplier Details.")
        Me.btnEdit.UseVisualStyleBackColor = False
        '
        'panelSupplierHdr
        '
        Me.panelSupplierHdr.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panelSupplierHdr.Controls.Add(Me.BarcodeLabel)
        Me.panelSupplierHdr.Controls.Add(Me.btnNew)
        Me.panelSupplierHdr.Controls.Add(Me.txtBarcode)
        Me.panelSupplierHdr.Controls.Add(Me.labSupplierName)
        Me.panelSupplierHdr.Controls.Add(Me.Label14)
        Me.panelSupplierHdr.Location = New System.Drawing.Point(653, 62)
        Me.panelSupplierHdr.Name = "panelSupplierHdr"
        Me.panelSupplierHdr.Size = New System.Drawing.Size(327, 75)
        Me.panelSupplierHdr.TabIndex = 25
        '
        'BarcodeLabel
        '
        Me.BarcodeLabel.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarcodeLabel.ForeColor = System.Drawing.Color.Gray
        Me.BarcodeLabel.Location = New System.Drawing.Point(80, 9)
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
        Me.txtBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarcode.Location = New System.Drawing.Point(81, 24)
        Me.txtBarcode.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtBarcode.MaxLength = 15
        Me.txtBarcode.Name = "txtBarcode"
        Me.txtBarcode.ReadOnly = True
        Me.txtBarcode.Size = New System.Drawing.Size(107, 13)
        Me.txtBarcode.TabIndex = 24
        Me.txtBarcode.TabStop = False
        '
        'labSupplierName
        '
        Me.labSupplierName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labSupplierName.Location = New System.Drawing.Point(3, 41)
        Me.labSupplierName.Name = "labSupplierName"
        Me.labSupplierName.Size = New System.Drawing.Size(309, 26)
        Me.labSupplierName.TabIndex = 22
        Me.labSupplierName.Text = "labSupplierName"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(3, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(62, 18)
        Me.Label14.TabIndex = 23
        Me.Label14.Text = "Supplier: "
        '
        'panelBanner
        '
        Me.panelBanner.BackColor = System.Drawing.Color.White
        Me.panelBanner.Controls.Add(Me.btnExit)
        Me.panelBanner.Controls.Add(Me.labGettingData)
        Me.panelBanner.Controls.Add(Me.Label5)
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
        Me.labGettingData.Location = New System.Drawing.Point(262, 3)
        Me.labGettingData.Name = "labGettingData"
        Me.labGettingData.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labGettingData.Size = New System.Drawing.Size(114, 33)
        Me.labGettingData.TabIndex = 2
        Me.labGettingData.Text = "Wait-" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Getting Data.."
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label5.Location = New System.Drawing.Point(18, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(228, 23)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "S u p p l i e r   A d m i n"
        '
        'FrameBrowse
        '
        Me.FrameBrowse.BackColor = System.Drawing.Color.White
        Me.FrameBrowse.Controls.Add(Me.Label22)
        Me.FrameBrowse.Controls.Add(Me.Label21)
        Me.FrameBrowse.Controls.Add(Me.cmdClearSupplierSearch)
        Me.FrameBrowse.Controls.Add(Me.cmdSupplierSearch)
        Me.FrameBrowse.Controls.Add(Me.Label3)
        Me.FrameBrowse.Controls.Add(Me.txtSupplierSearch)
        Me.FrameBrowse.Controls.Add(Me.dgvSupplierList)
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
        'cmdClearSupplierSearch
        '
        Me.cmdClearSupplierSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdClearSupplierSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClearSupplierSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdClearSupplierSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClearSupplierSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClearSupplierSearch.Location = New System.Drawing.Point(564, 29)
        Me.cmdClearSupplierSearch.Name = "cmdClearSupplierSearch"
        Me.cmdClearSupplierSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClearSupplierSearch.Size = New System.Drawing.Size(68, 23)
        Me.cmdClearSupplierSearch.TabIndex = 80
        Me.cmdClearSupplierSearch.Text = "Clear"
        Me.cmdClearSupplierSearch.UseVisualStyleBackColor = False
        '
        'cmdSupplierSearch
        '
        Me.cmdSupplierSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdSupplierSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSupplierSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSupplierSearch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSupplierSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSupplierSearch.Location = New System.Drawing.Point(564, 59)
        Me.cmdSupplierSearch.Name = "cmdSupplierSearch"
        Me.cmdSupplierSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSupplierSearch.Size = New System.Drawing.Size(68, 23)
        Me.cmdSupplierSearch.TabIndex = 79
        Me.cmdSupplierSearch.Text = "Search"
        Me.cmdSupplierSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSupplierSearch.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Supplier List"
        '
        'txtSupplierSearch
        '
        Me.txtSupplierSearch.AcceptsReturn = True
        Me.txtSupplierSearch.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSupplierSearch.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSupplierSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierSearch.Font = New System.Drawing.Font("Lucida Sans Unicode", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSupplierSearch.Location = New System.Drawing.Point(312, 67)
        Me.txtSupplierSearch.MaxLength = 0
        Me.txtSupplierSearch.Name = "txtSupplierSearch"
        Me.txtSupplierSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierSearch.Size = New System.Drawing.Size(234, 19)
        Me.txtSupplierSearch.TabIndex = 78
        Me.txtSupplierSearch.Text = "txtSupplierSearch"
        '
        'dgvSupplierList
        '
        Me.dgvSupplierList.AllowUserToAddRows = False
        Me.dgvSupplierList.AllowUserToDeleteRows = False
        Me.dgvSupplierList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.dgvSupplierList.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvSupplierList.ColumnHeadersHeight = 18
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvSupplierList.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvSupplierList.GridColor = System.Drawing.SystemColors.ControlLight
        Me.dgvSupplierList.Location = New System.Drawing.Point(6, 91)
        Me.dgvSupplierList.MultiSelect = False
        Me.dgvSupplierList.Name = "dgvSupplierList"
        Me.dgvSupplierList.ReadOnly = True
        Me.dgvSupplierList.RowHeadersWidth = 17
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        Me.dgvSupplierList.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvSupplierList.RowTemplate.Height = 17
        Me.dgvSupplierList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSupplierList.Size = New System.Drawing.Size(626, 470)
        Me.dgvSupplierList.StandardTab = True
        Me.dgvSupplierList.TabIndex = 4
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
        'ucChildSupplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.Controls.Add(Me.grpBoxSupplier)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ucChildSupplier"
        Me.Size = New System.Drawing.Size(1011, 650)
        Me.grpBoxSupplier.ResumeLayout(False)
        Me.panelSupplierDetail.ResumeLayout(False)
        Me.panelSupplierDetail.PerformLayout()
        Me.panelSupplierHdr.ResumeLayout(False)
        Me.panelSupplierHdr.PerformLayout()
        Me.panelBanner.ResumeLayout(False)
        Me.FrameBrowse.ResumeLayout(False)
        Me.FrameBrowse.PerformLayout()
        CType(Me.dgvSupplierList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents grpBoxSupplier As GroupBox
    Public WithEvents FrameBrowse As GroupBox
    Public WithEvents Label22 As Label
    Friend WithEvents Label21 As Label
    Public WithEvents cmdClearSupplierSearch As Button
    Public WithEvents cmdSupplierSearch As Button
    Friend WithEvents Label3 As Label
    Public WithEvents txtSupplierSearch As TextBox
    Friend WithEvents dgvSupplierList As DataGridView
    Public WithEvents txtFind As TextBox
    Public WithEvents labRecCount As Label
    Public WithEvents LabFind As Label
    Friend WithEvents panelBanner As Panel
    Friend WithEvents labGettingData As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents panelSupplierHdr As Panel
    Friend WithEvents labSupplierName As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents panelSupplierDetail As Panel
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnNew As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents BarcodeLabel As Label
    Friend WithEvents txtBarcode As TextBox
    Friend WithEvents btnLookupGoods As Button
    Friend WithEvents txtPhone As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents txtFax As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtContactName As TextBox
    Friend WithEvents txtWebsite As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtComments As TextBox
    Friend WithEvents Label2 As Label
End Class
