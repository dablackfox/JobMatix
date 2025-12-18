<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmNewRA
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
    Public WithEvents cmdPrintShippingLabel As System.Windows.Forms.Button
    Public WithEvents txtSupplierRMA As System.Windows.Forms.TextBox
    Public WithEvents txtRAStatusFriendly As System.Windows.Forms.TextBox
    Public WithEvents txtRAStatusOrig As System.Windows.Forms.TextBox
    Public WithEvents cmdCancelRARecord As System.Windows.Forms.Button
    Public WithEvents chkTfrToStock As System.Windows.Forms.CheckBox
    Public WithEvents cmdNewSupplier As System.Windows.Forms.Button
    Public WithEvents txtJobNo As System.Windows.Forms.TextBox
    Public WithEvents txtSymptoms As System.Windows.Forms.TextBox
    Public WithEvents txtInvoiceInfo As System.Windows.Forms.TextBox
    Public WithEvents txtSupplierInfo As System.Windows.Forms.TextBox
    Public WithEvents LabJobNo As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents FrameSupplier As System.Windows.Forms.GroupBox
    Public WithEvents cmdSelectInvoice As System.Windows.Forms.Button
    Public WithEvents Chk12Mths As System.Windows.Forms.CheckBox
    Public WithEvents ListViewGoods As System.Windows.Forms.ListView
    Public WithEvents LabSelectInvoice As System.Windows.Forms.Label
    Public WithEvents FrameInvoiceList As System.Windows.Forms.GroupBox
    Public WithEvents Picture2 As System.Windows.Forms.PictureBox
    Public WithEvents txtCustNo As System.Windows.Forms.TextBox
    Public WithEvents txtCustMobile As System.Windows.Forms.TextBox
    Public WithEvents txtCustPhone As System.Windows.Forms.TextBox
    Public WithEvents txtCustName As System.Windows.Forms.TextBox
    Public WithEvents txtCustCompany As System.Windows.Forms.TextBox
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents labUserPrompt0 As System.Windows.Forms.Label
    Public WithEvents labUserPrompt1 As System.Windows.Forms.Label
    Public WithEvents txtRequestNotes As System.Windows.Forms.TextBox
    Public WithEvents txtRequestNotesHistory As System.Windows.Forms.TextBox
    Public WithEvents chkRMARequested As System.Windows.Forms.CheckBox
    Public WithEvents txtRMAReceived As System.Windows.Forms.TextBox
    Public WithEvents _OptRMAResult_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptRMAResult_1 As System.Windows.Forms.RadioButton
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents LabDateRMARequested As System.Windows.Forms.Label
    Public WithEvents LabDateRMAResult As System.Windows.Forms.Label
    Public WithEvents LabRMAREceived As System.Windows.Forms.Label
    Public WithEvents LabRMAResult As System.Windows.Forms.Label
    Public WithEvents FrameRMARequest As System.Windows.Forms.GroupBox
    Public WithEvents cmdNewSupplier2 As System.Windows.Forms.Button
    Public WithEvents labUserPrompt2 As System.Windows.Forms.Label
    Public WithEvents chkGoodsSent As System.Windows.Forms.CheckBox
    Public WithEvents txtCourierBarcode As System.Windows.Forms.TextBox
    Public WithEvents txtResultComments As System.Windows.Forms.TextBox
    Public WithEvents LabCourierBarcode As System.Windows.Forms.Label
    Public WithEvents LabDateGoodsSent As System.Windows.Forms.Label
    Public WithEvents LabDateGoodsResult As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents LabResultHdr As System.Windows.Forms.Label
    Public WithEvents LabGoodsResult As System.Windows.Forms.Label
    Public WithEvents FrameGoods As System.Windows.Forms.GroupBox
    Public WithEvents cmdReOpen As System.Windows.Forms.Button
    Public WithEvents txtUpdatedName As System.Windows.Forms.TextBox
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdPrintRAForm As System.Windows.Forms.Button
    Public WithEvents txtItemDescription As System.Windows.Forms.TextBox
    Public WithEvents txtItemBarcode As System.Windows.Forms.TextBox
    Public WithEvents txtItemSerial As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents LabItemSerial As System.Windows.Forms.Label
    Public WithEvents labProdBarcode As System.Windows.Forms.Label
    Public WithEvents FrameItem As System.Windows.Forms.GroupBox
    Public WithEvents Picture1 As System.Windows.Forms.PictureBox
    Public WithEvents txtCreatedName As System.Windows.Forms.TextBox
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents LabPrevStaff As System.Windows.Forms.Label
    Public WithEvents LabPrevUpdate As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents LabToday As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents LabOurRANumber As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LabVersion As System.Windows.Forms.Label
    Public WithEvents LabHdr2 As System.Windows.Forms.Label
    Public WithEvents LabHd3 As System.Windows.Forms.Label
    Public WithEvents LabDateCreated As System.Windows.Forms.Label
    Public WithEvents LabRcvdBy As System.Windows.Forms.Label
    Public WithEvents LabHdr1 As System.Windows.Forms.Label
    '== Public WithEvents OptGoodsResult As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptRMAResult As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    '== Public WithEvents txtRequestNotes As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    '== Public WithEvents txtRequestNotesHistory As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewRA))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdPrintShippingLabel = New System.Windows.Forms.Button()
        Me.chkTfrToStock = New System.Windows.Forms.CheckBox()
        Me.cmdNewSupplier = New System.Windows.Forms.Button()
        Me.txtCustNo = New System.Windows.Forms.TextBox()
        Me.txtCustName = New System.Windows.Forms.TextBox()
        Me.txtCustCompany = New System.Windows.Forms.TextBox()
        Me.cmdNewSupplier2 = New System.Windows.Forms.Button()
        Me.cmdReOpen = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdPrintRAForm = New System.Windows.Forms.Button()
        Me.btnSaveAttachment = New System.Windows.Forms.Button()
        Me.txtNewComment = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnViewDoc = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.picSubjectItem = New System.Windows.Forms.PictureBox()
        Me.btnPrintItemLabel = New System.Windows.Forms.Button()
        Me.cmdCancelRARecord = New System.Windows.Forms.Button()
        Me.cboPrinters = New System.Windows.Forms.ComboBox()
        Me.cboItemLabelPrinters = New System.Windows.Forms.ComboBox()
        Me.btnUpdateSupplierAddress = New System.Windows.Forms.Button()
        Me.txtSupplierRMA = New System.Windows.Forms.TextBox()
        Me.txtRAStatusFriendly = New System.Windows.Forms.TextBox()
        Me.txtRAStatusOrig = New System.Windows.Forms.TextBox()
        Me.FrameSupplier = New System.Windows.Forms.GroupBox()
        Me.txtSymptoms = New System.Windows.Forms.TextBox()
        Me.txtInvoiceInfo = New System.Windows.Forms.TextBox()
        Me.txtSupplierInfo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCustMobile = New System.Windows.Forms.TextBox()
        Me.txtCustPhone = New System.Windows.Forms.TextBox()
        Me.optOrigin_stock = New System.Windows.Forms.RadioButton()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.optOrigin_counter = New System.Windows.Forms.RadioButton()
        Me.txtJobNo = New System.Windows.Forms.TextBox()
        Me.LabJobNo = New System.Windows.Forms.Label()
        Me.optOrigin_job = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.FrameInvoiceList = New System.Windows.Forms.GroupBox()
        Me.cmdSelectInvoice = New System.Windows.Forms.Button()
        Me.Chk12Mths = New System.Windows.Forms.CheckBox()
        Me.ListViewGoods = New System.Windows.Forms.ListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.LabSelectInvoice = New System.Windows.Forms.Label()
        Me.grpBoxOrigin = New System.Windows.Forms.GroupBox()
        Me.panelOrigin = New System.Windows.Forms.Panel()
        Me.labOrigin = New System.Windows.Forms.Label()
        Me.txtItemSaleInvoiceDate = New System.Windows.Forms.TextBox()
        Me.txtItemSaleInvoiceNo = New System.Windows.Forms.TextBox()
        Me.labItemSaleHdr = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Picture2 = New System.Windows.Forms.PictureBox()
        Me.grpBoxItemPic = New System.Windows.Forms.GroupBox()
        Me.labUserPrompt0 = New System.Windows.Forms.Label()
        Me.labUserPrompt1 = New System.Windows.Forms.Label()
        Me.FrameRMARequest = New System.Windows.Forms.GroupBox()
        Me.labStep2 = New System.Windows.Forms.Label()
        Me.labStep1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkRMARequested = New System.Windows.Forms.CheckBox()
        Me.txtRMAReceived = New System.Windows.Forms.TextBox()
        Me._OptRMAResult_0 = New System.Windows.Forms.RadioButton()
        Me._OptRMAResult_1 = New System.Windows.Forms.RadioButton()
        Me.LabDateRMARequested = New System.Windows.Forms.Label()
        Me.LabDateRMAResult = New System.Windows.Forms.Label()
        Me.LabRMAREceived = New System.Windows.Forms.Label()
        Me.LabRMAResult = New System.Windows.Forms.Label()
        Me.txtRequestNotes = New System.Windows.Forms.TextBox()
        Me.txtRequestNotesHistory = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.labUserPrompt2 = New System.Windows.Forms.Label()
        Me.FrameGoods = New System.Windows.Forms.GroupBox()
        Me.labStep4 = New System.Windows.Forms.Label()
        Me.labStep3 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cboGoodsResult = New System.Windows.Forms.ComboBox()
        Me.chkGoodsSent = New System.Windows.Forms.CheckBox()
        Me.txtCourierBarcode = New System.Windows.Forms.TextBox()
        Me.txtResultComments = New System.Windows.Forms.TextBox()
        Me.LabCourierBarcode = New System.Windows.Forms.Label()
        Me.LabDateGoodsSent = New System.Windows.Forms.Label()
        Me.LabDateGoodsResult = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabResultHdr = New System.Windows.Forms.Label()
        Me.LabGoodsResult = New System.Windows.Forms.Label()
        Me.txtUpdatedName = New System.Windows.Forms.TextBox()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.FrameItem = New System.Windows.Forms.GroupBox()
        Me.labStartHere = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkKeepScannedLeadZeroes = New System.Windows.Forms.CheckBox()
        Me.txtItemDescription = New System.Windows.Forms.TextBox()
        Me.txtItemBarcode = New System.Windows.Forms.TextBox()
        Me.txtItemSerial = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.LabItemSerial = New System.Windows.Forms.Label()
        Me.labProdBarcode = New System.Windows.Forms.Label()
        Me.Picture1 = New System.Windows.Forms.PictureBox()
        Me.txtCreatedName = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.LabPrevStaff = New System.Windows.Forms.Label()
        Me.LabPrevUpdate = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabToday = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LabOurRANumber = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabVersion = New System.Windows.Forms.Label()
        Me.LabHdr2 = New System.Windows.Forms.Label()
        Me.LabHd3 = New System.Windows.Forms.Label()
        Me.LabDateCreated = New System.Windows.Forms.Label()
        Me.LabRcvdBy = New System.Windows.Forms.Label()
        Me.LabHdr1 = New System.Windows.Forms.Label()
        Me.OptRMAResult = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Label9 = New System.Windows.Forms.Label()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SSTabMain = New System.Windows.Forms.TabControl()
        Me.MainTabPage_progress = New System.Windows.Forms.TabPage()
        Me.grpBoxNotes = New System.Windows.Forms.GroupBox()
        Me.mainTabPage_attachments = New System.Windows.Forms.TabPage()
        Me.grpBoxItem = New System.Windows.Forms.GroupBox()
        Me.picMsExcel = New System.Windows.Forms.PictureBox()
        Me.picMsWord = New System.Windows.Forms.PictureBox()
        Me.picPDF = New System.Windows.Forms.PictureBox()
        Me.txtComments = New System.Windows.Forms.TextBox()
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
        Me.openDlg1 = New System.Windows.Forms.OpenFileDialog()
        Me.picSubjectMain = New System.Windows.Forms.PictureBox()
        Me.Label23 = New System.Windows.Forms.Label()
        CType(Me.picSubjectItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FrameSupplier.SuspendLayout()
        Me.FrameInvoiceList.SuspendLayout()
        Me.grpBoxOrigin.SuspendLayout()
        Me.panelOrigin.SuspendLayout()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxItemPic.SuspendLayout()
        Me.FrameRMARequest.SuspendLayout()
        Me.FrameGoods.SuspendLayout()
        Me.FrameItem.SuspendLayout()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptRMAResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SSTabMain.SuspendLayout()
        Me.MainTabPage_progress.SuspendLayout()
        Me.grpBoxNotes.SuspendLayout()
        Me.mainTabPage_attachments.SuspendLayout()
        Me.grpBoxItem.SuspendLayout()
        CType(Me.picMsExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picMsWord, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpBoxAddNew.SuspendLayout()
        CType(Me.picSubjectMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdPrintShippingLabel
        '
        Me.cmdPrintShippingLabel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdPrintShippingLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrintShippingLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPrintShippingLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrintShippingLabel.Location = New System.Drawing.Point(434, 681)
        Me.cmdPrintShippingLabel.Name = "cmdPrintShippingLabel"
        Me.cmdPrintShippingLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrintShippingLabel.Size = New System.Drawing.Size(101, 31)
        Me.cmdPrintShippingLabel.TabIndex = 35
        Me.cmdPrintShippingLabel.Text = "Print Ship.Label"
        Me.ToolTip1.SetToolTip(Me.cmdPrintShippingLabel, "Print A4 shipping label for Goods Despatch..")
        Me.cmdPrintShippingLabel.UseVisualStyleBackColor = False
        '
        'chkTfrToStock
        '
        Me.chkTfrToStock.BackColor = System.Drawing.Color.Transparent
        Me.chkTfrToStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTfrToStock.Enabled = False
        Me.chkTfrToStock.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTfrToStock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTfrToStock.Location = New System.Drawing.Point(243, 93)
        Me.chkTfrToStock.Name = "chkTfrToStock"
        Me.chkTfrToStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTfrToStock.Size = New System.Drawing.Size(67, 26)
        Me.chkTfrToStock.TabIndex = 102
        Me.chkTfrToStock.Text = "Tfr to Stock"
        Me.ToolTip1.SetToolTip(Me.chkTfrToStock, "Mark this item as now being an RA stock item..")
        Me.chkTfrToStock.UseVisualStyleBackColor = False
        '
        'cmdNewSupplier
        '
        Me.cmdNewSupplier.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdNewSupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNewSupplier.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNewSupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNewSupplier.Location = New System.Drawing.Point(256, 179)
        Me.cmdNewSupplier.Name = "cmdNewSupplier"
        Me.cmdNewSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNewSupplier.Size = New System.Drawing.Size(105, 20)
        Me.cmdNewSupplier.TabIndex = 6
        Me.cmdNewSupplier.Text = "Change Supplier"
        Me.ToolTip1.SetToolTip(Me.cmdNewSupplier, "Return Goods to Different Supplier")
        Me.cmdNewSupplier.UseVisualStyleBackColor = False
        '
        'txtCustNo
        '
        Me.txtCustNo.AcceptsReturn = True
        Me.txtCustNo.BackColor = System.Drawing.Color.White
        Me.txtCustNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustNo.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustNo.Location = New System.Drawing.Point(81, 55)
        Me.txtCustNo.MaxLength = 0
        Me.txtCustNo.Name = "txtCustNo"
        Me.txtCustNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustNo.Size = New System.Drawing.Size(50, 21)
        Me.txtCustNo.TabIndex = 101
        Me.ToolTip1.SetToolTip(Me.txtCustNo, "Click here to lookup customer..")
        '
        'txtCustName
        '
        Me.txtCustName.AcceptsReturn = True
        Me.txtCustName.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtCustName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustName.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustName.Location = New System.Drawing.Point(11, 129)
        Me.txtCustName.MaxLength = 0
        Me.txtCustName.Name = "txtCustName"
        Me.txtCustName.ReadOnly = True
        Me.txtCustName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustName.Size = New System.Drawing.Size(226, 13)
        Me.txtCustName.TabIndex = 11
        Me.txtCustName.Text = "txtCustName"
        Me.ToolTip1.SetToolTip(Me.txtCustName, "Click here to lookup customer..")
        '
        'txtCustCompany
        '
        Me.txtCustCompany.AcceptsReturn = True
        Me.txtCustCompany.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtCustCompany.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustCompany.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustCompany.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustCompany.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustCompany.Location = New System.Drawing.Point(11, 99)
        Me.txtCustCompany.MaxLength = 0
        Me.txtCustCompany.Multiline = True
        Me.txtCustCompany.Name = "txtCustCompany"
        Me.txtCustCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustCompany.Size = New System.Drawing.Size(226, 26)
        Me.txtCustCompany.TabIndex = 10
        Me.txtCustCompany.Text = "txtCustCompany" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.ToolTip1.SetToolTip(Me.txtCustCompany, "Click here to lookup customer..")
        '
        'cmdNewSupplier2
        '
        Me.cmdNewSupplier2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdNewSupplier2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNewSupplier2.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNewSupplier2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNewSupplier2.Location = New System.Drawing.Point(115, 10)
        Me.cmdNewSupplier2.Name = "cmdNewSupplier2"
        Me.cmdNewSupplier2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNewSupplier2.Size = New System.Drawing.Size(110, 24)
        Me.cmdNewSupplier2.TabIndex = 96
        Me.cmdNewSupplier2.Text = "Change Supplier"
        Me.ToolTip1.SetToolTip(Me.cmdNewSupplier2, "Return Goods to Different Supplier")
        Me.cmdNewSupplier2.UseVisualStyleBackColor = False
        '
        'cmdReOpen
        '
        Me.cmdReOpen.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdReOpen.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReOpen.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReOpen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReOpen.Location = New System.Drawing.Point(11, 39)
        Me.cmdReOpen.Name = "cmdReOpen"
        Me.cmdReOpen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReOpen.Size = New System.Drawing.Size(73, 25)
        Me.cmdReOpen.TabIndex = 20
        Me.cmdReOpen.Text = "Re- Open"
        Me.ToolTip1.SetToolTip(Me.cmdReOpen, "RE-OPen this RA, and set the Status back to RMA-Granted..")
        Me.cmdReOpen.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(904, 638)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(73, 33)
        Me.cmdSave.TabIndex = 37
        Me.cmdSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Changes..")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdPrintRAForm
        '
        Me.cmdPrintRAForm.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdPrintRAForm.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrintRAForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdPrintRAForm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrintRAForm.Location = New System.Drawing.Point(434, 639)
        Me.cmdPrintRAForm.Name = "cmdPrintRAForm"
        Me.cmdPrintRAForm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrintRAForm.Size = New System.Drawing.Size(101, 31)
        Me.cmdPrintRAForm.TabIndex = 34
        Me.cmdPrintRAForm.Text = "Print  RA Form"
        Me.ToolTip1.SetToolTip(Me.cmdPrintRAForm, "Print A4 RA Details and Delivery Form..")
        Me.cmdPrintRAForm.UseVisualStyleBackColor = False
        '
        'btnSaveAttachment
        '
        Me.btnSaveAttachment.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnSaveAttachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSaveAttachment.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSaveAttachment.Location = New System.Drawing.Point(454, 113)
        Me.btnSaveAttachment.Name = "btnSaveAttachment"
        Me.btnSaveAttachment.Size = New System.Drawing.Size(57, 26)
        Me.btnSaveAttachment.TabIndex = 2
        Me.btnSaveAttachment.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.btnSaveAttachment, "Save to Database")
        Me.btnSaveAttachment.UseVisualStyleBackColor = False
        '
        'txtNewComment
        '
        Me.txtNewComment.BackColor = System.Drawing.Color.White
        Me.txtNewComment.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewComment.Location = New System.Drawing.Point(172, 68)
        Me.txtNewComment.Multiline = True
        Me.txtNewComment.Name = "txtNewComment"
        Me.txtNewComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNewComment.Size = New System.Drawing.Size(255, 73)
        Me.txtNewComment.TabIndex = 1
        Me.txtNewComment.Text = "txtNewComment"
        Me.ToolTip1.SetToolTip(Me.txtNewComment, "ust Have Comment for New Attachment")
        '
        'btnBrowse
        '
        Me.btnBrowse.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowse.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowse.Location = New System.Drawing.Point(454, 31)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(57, 26)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "Browse"
        Me.ToolTip1.SetToolTip(Me.btnBrowse, "Browse for new File to Attach")
        Me.btnBrowse.UseVisualStyleBackColor = False
        '
        'btnViewDoc
        '
        Me.btnViewDoc.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnViewDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewDoc.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnViewDoc.Location = New System.Drawing.Point(467, 171)
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
        Me.btnDelete.Location = New System.Drawing.Point(467, 227)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(53, 23)
        Me.btnDelete.TabIndex = 16
        Me.btnDelete.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.btnDelete, "Delete this document permantly. File System to View..")
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'picSubjectItem
        '
        Me.picSubjectItem.Location = New System.Drawing.Point(8, 14)
        Me.picSubjectItem.Name = "picSubjectItem"
        Me.picSubjectItem.Size = New System.Drawing.Size(53, 49)
        Me.picSubjectItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSubjectItem.TabIndex = 0
        Me.picSubjectItem.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picSubjectItem, "Click to Browse and save Picture of Item.")
        '
        'btnPrintItemLabel
        '
        Me.btnPrintItemLabel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnPrintItemLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnPrintItemLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrintItemLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnPrintItemLabel.Location = New System.Drawing.Point(784, 674)
        Me.btnPrintItemLabel.Name = "btnPrintItemLabel"
        Me.btnPrintItemLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnPrintItemLabel.Size = New System.Drawing.Size(70, 36)
        Me.btnPrintItemLabel.TabIndex = 104
        Me.btnPrintItemLabel.Text = "Print Item Label"
        Me.ToolTip1.SetToolTip(Me.btnPrintItemLabel, "Print Item (RA Part) sticky label for Goods Despatch..")
        Me.btnPrintItemLabel.UseVisualStyleBackColor = False
        '
        'cmdCancelRARecord
        '
        Me.cmdCancelRARecord.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancelRARecord.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancelRARecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancelRARecord.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancelRARecord.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancelRARecord.Location = New System.Drawing.Point(861, 45)
        Me.cmdCancelRARecord.Name = "cmdCancelRARecord"
        Me.cmdCancelRARecord.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancelRARecord.Size = New System.Drawing.Size(127, 26)
        Me.cmdCancelRARecord.TabIndex = 39
        Me.cmdCancelRARecord.Text = "Cancel RA Forever"
        Me.ToolTip1.SetToolTip(Me.cmdCancelRARecord, "Cancel this RA record permenantly..")
        Me.cmdCancelRARecord.UseVisualStyleBackColor = False
        '
        'cboPrinters
        '
        Me.cboPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboPrinters.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPrinters.FormattingEnabled = True
        Me.cboPrinters.Location = New System.Drawing.Point(543, 656)
        Me.cboPrinters.Name = "cboPrinters"
        Me.cboPrinters.Size = New System.Drawing.Size(106, 20)
        Me.cboPrinters.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.cboPrinters, "A4 printer for RA Forms and Shipping Label.")
        '
        'cboItemLabelPrinters
        '
        Me.cboItemLabelPrinters.BackColor = System.Drawing.Color.Gainsboro
        Me.cboItemLabelPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemLabelPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboItemLabelPrinters.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboItemLabelPrinters.FormattingEnabled = True
        Me.cboItemLabelPrinters.Location = New System.Drawing.Point(671, 656)
        Me.cboItemLabelPrinters.Name = "cboItemLabelPrinters"
        Me.cboItemLabelPrinters.Size = New System.Drawing.Size(106, 20)
        Me.cboItemLabelPrinters.TabIndex = 105
        Me.ToolTip1.SetToolTip(Me.cboItemLabelPrinters, "Print Sticky Labels for Jobs/RA's ")
        '
        'btnUpdateSupplierAddress
        '
        Me.btnUpdateSupplierAddress.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnUpdateSupplierAddress.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnUpdateSupplierAddress.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateSupplierAddress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnUpdateSupplierAddress.Location = New System.Drawing.Point(115, 40)
        Me.btnUpdateSupplierAddress.Name = "btnUpdateSupplierAddress"
        Me.btnUpdateSupplierAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnUpdateSupplierAddress.Size = New System.Drawing.Size(110, 24)
        Me.btnUpdateSupplierAddress.TabIndex = 74
        Me.btnUpdateSupplierAddress.Text = "Update Suppl.Addr."
        Me.ToolTip1.SetToolTip(Me.btnUpdateSupplierAddress, "Update Supplier Address Details from Retail DB.")
        Me.btnUpdateSupplierAddress.UseVisualStyleBackColor = False
        '
        'txtSupplierRMA
        '
        Me.txtSupplierRMA.AcceptsReturn = True
        Me.txtSupplierRMA.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSupplierRMA.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSupplierRMA.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierRMA.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierRMA.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSupplierRMA.Location = New System.Drawing.Point(779, 85)
        Me.txtSupplierRMA.MaxLength = 0
        Me.txtSupplierRMA.Name = "txtSupplierRMA"
        Me.txtSupplierRMA.ReadOnly = True
        Me.txtSupplierRMA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierRMA.Size = New System.Drawing.Size(209, 17)
        Me.txtSupplierRMA.TabIndex = 95
        Me.txtSupplierRMA.TabStop = False
        Me.txtSupplierRMA.Text = "txtSupplierRMA"
        '
        'txtRAStatusFriendly
        '
        Me.txtRAStatusFriendly.AcceptsReturn = True
        Me.txtRAStatusFriendly.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.txtRAStatusFriendly.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRAStatusFriendly.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRAStatusFriendly.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRAStatusFriendly.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRAStatusFriendly.Location = New System.Drawing.Point(412, 77)
        Me.txtRAStatusFriendly.MaxLength = 0
        Me.txtRAStatusFriendly.Name = "txtRAStatusFriendly"
        Me.txtRAStatusFriendly.ReadOnly = True
        Me.txtRAStatusFriendly.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRAStatusFriendly.Size = New System.Drawing.Size(209, 17)
        Me.txtRAStatusFriendly.TabIndex = 94
        Me.txtRAStatusFriendly.Text = "txtRAStatusFriendly"
        '
        'txtRAStatusOrig
        '
        Me.txtRAStatusOrig.AcceptsReturn = True
        Me.txtRAStatusOrig.BackColor = System.Drawing.Color.FromArgb(CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.txtRAStatusOrig.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRAStatusOrig.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRAStatusOrig.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRAStatusOrig.Location = New System.Drawing.Point(484, 59)
        Me.txtRAStatusOrig.MaxLength = 0
        Me.txtRAStatusOrig.Name = "txtRAStatusOrig"
        Me.txtRAStatusOrig.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRAStatusOrig.Size = New System.Drawing.Size(137, 14)
        Me.txtRAStatusOrig.TabIndex = 93
        Me.txtRAStatusOrig.TabStop = False
        Me.txtRAStatusOrig.Text = "txtRAStatusOrig"
        '
        'FrameSupplier
        '
        Me.FrameSupplier.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameSupplier.Controls.Add(Me.cmdNewSupplier)
        Me.FrameSupplier.Controls.Add(Me.txtSymptoms)
        Me.FrameSupplier.Controls.Add(Me.txtInvoiceInfo)
        Me.FrameSupplier.Controls.Add(Me.txtSupplierInfo)
        Me.FrameSupplier.Controls.Add(Me.Label5)
        Me.FrameSupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameSupplier.Location = New System.Drawing.Point(16, 293)
        Me.FrameSupplier.Name = "FrameSupplier"
        Me.FrameSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameSupplier.Size = New System.Drawing.Size(385, 266)
        Me.FrameSupplier.TabIndex = 81
        Me.FrameSupplier.TabStop = False
        Me.FrameSupplier.Text = "FrameSupplier"
        '
        'txtSymptoms
        '
        Me.txtSymptoms.AcceptsReturn = True
        Me.txtSymptoms.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.txtSymptoms.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSymptoms.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSymptoms.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSymptoms.Location = New System.Drawing.Point(8, 212)
        Me.txtSymptoms.MaxLength = 500
        Me.txtSymptoms.Multiline = True
        Me.txtSymptoms.Name = "txtSymptoms"
        Me.txtSymptoms.ReadOnly = True
        Me.txtSymptoms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSymptoms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSymptoms.Size = New System.Drawing.Size(366, 47)
        Me.txtSymptoms.TabIndex = 7
        '
        'txtInvoiceInfo
        '
        Me.txtInvoiceInfo.AcceptsReturn = True
        Me.txtInvoiceInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtInvoiceInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInvoiceInfo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvoiceInfo.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceInfo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInvoiceInfo.Location = New System.Drawing.Point(8, 20)
        Me.txtInvoiceInfo.MaxLength = 0
        Me.txtInvoiceInfo.Multiline = True
        Me.txtInvoiceInfo.Name = "txtInvoiceInfo"
        Me.txtInvoiceInfo.ReadOnly = True
        Me.txtInvoiceInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvoiceInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInvoiceInfo.Size = New System.Drawing.Size(353, 59)
        Me.txtInvoiceInfo.TabIndex = 4
        Me.txtInvoiceInfo.Text = "txtInvoiceInfo"
        '
        'txtSupplierInfo
        '
        Me.txtSupplierInfo.AcceptsReturn = True
        Me.txtSupplierInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtSupplierInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSupplierInfo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierInfo.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierInfo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSupplierInfo.Location = New System.Drawing.Point(8, 90)
        Me.txtSupplierInfo.MaxLength = 0
        Me.txtSupplierInfo.Multiline = True
        Me.txtSupplierInfo.Name = "txtSupplierInfo"
        Me.txtSupplierInfo.ReadOnly = True
        Me.txtSupplierInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSupplierInfo.Size = New System.Drawing.Size(353, 89)
        Me.txtSupplierInfo.TabIndex = 5
        Me.txtSupplierInfo.Text = "txtSupplierInfo" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 196)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(161, 17)
        Me.Label5.TabIndex = 83
        Me.Label5.Text = "*  Symptoms/ Dysfunction"
        '
        'txtCustMobile
        '
        Me.txtCustMobile.AcceptsReturn = True
        Me.txtCustMobile.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtCustMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustMobile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustMobile.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustMobile.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustMobile.Location = New System.Drawing.Point(144, 150)
        Me.txtCustMobile.MaxLength = 0
        Me.txtCustMobile.Name = "txtCustMobile"
        Me.txtCustMobile.ReadOnly = True
        Me.txtCustMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustMobile.Size = New System.Drawing.Size(93, 13)
        Me.txtCustMobile.TabIndex = 13
        Me.txtCustMobile.TabStop = False
        Me.txtCustMobile.Text = "txtCustMobile"
        '
        'txtCustPhone
        '
        Me.txtCustPhone.AcceptsReturn = True
        Me.txtCustPhone.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtCustPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCustPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustPhone.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPhone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustPhone.Location = New System.Drawing.Point(15, 150)
        Me.txtCustPhone.MaxLength = 0
        Me.txtCustPhone.Name = "txtCustPhone"
        Me.txtCustPhone.ReadOnly = True
        Me.txtCustPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPhone.Size = New System.Drawing.Size(116, 13)
        Me.txtCustPhone.TabIndex = 12
        Me.txtCustPhone.TabStop = False
        Me.txtCustPhone.Text = "txtCustPhone"
        '
        'optOrigin_stock
        '
        Me.optOrigin_stock.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.optOrigin_stock.Location = New System.Drawing.Point(163, 7)
        Me.optOrigin_stock.Name = "optOrigin_stock"
        Me.optOrigin_stock.Size = New System.Drawing.Size(57, 19)
        Me.optOrigin_stock.TabIndex = 101
        Me.optOrigin_stock.TabStop = True
        Me.optOrigin_stock.Text = "Stock"
        Me.optOrigin_stock.UseVisualStyleBackColor = False
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(139, 55)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(75, 24)
        Me.Label19.TabIndex = 103
        Me.Label19.Text = "Cust. Barcode-  F2 to Lookup.."
        '
        'optOrigin_counter
        '
        Me.optOrigin_counter.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.optOrigin_counter.Location = New System.Drawing.Point(83, 7)
        Me.optOrigin_counter.Name = "optOrigin_counter"
        Me.optOrigin_counter.Size = New System.Drawing.Size(67, 19)
        Me.optOrigin_counter.TabIndex = 100
        Me.optOrigin_counter.TabStop = True
        Me.optOrigin_counter.Text = "Counter"
        Me.optOrigin_counter.UseVisualStyleBackColor = False
        '
        'txtJobNo
        '
        Me.txtJobNo.AcceptsReturn = True
        Me.txtJobNo.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.txtJobNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJobNo.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtJobNo.Location = New System.Drawing.Point(82, 31)
        Me.txtJobNo.MaxLength = 0
        Me.txtJobNo.Name = "txtJobNo"
        Me.txtJobNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJobNo.Size = New System.Drawing.Size(49, 20)
        Me.txtJobNo.TabIndex = 9
        Me.txtJobNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabJobNo
        '
        Me.LabJobNo.BackColor = System.Drawing.Color.Transparent
        Me.LabJobNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabJobNo.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabJobNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabJobNo.Location = New System.Drawing.Point(35, 36)
        Me.LabJobNo.Name = "LabJobNo"
        Me.LabJobNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabJobNo.Size = New System.Drawing.Size(41, 13)
        Me.LabJobNo.TabIndex = 85
        Me.LabJobNo.Text = "JobNo :"
        '
        'optOrigin_job
        '
        Me.optOrigin_job.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.optOrigin_job.Location = New System.Drawing.Point(36, 7)
        Me.optOrigin_job.Name = "optOrigin_job"
        Me.optOrigin_job.Size = New System.Drawing.Size(57, 19)
        Me.optOrigin_job.TabIndex = 99
        Me.optOrigin_job.TabStop = True
        Me.optOrigin_job.Text = "Job"
        Me.optOrigin_job.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(10, 61)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(65, 17)
        Me.Label8.TabIndex = 102
        Me.Label8.Text = "Customer"
        '
        'FrameInvoiceList
        '
        Me.FrameInvoiceList.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameInvoiceList.Controls.Add(Me.cmdSelectInvoice)
        Me.FrameInvoiceList.Controls.Add(Me.Chk12Mths)
        Me.FrameInvoiceList.Controls.Add(Me.ListViewGoods)
        Me.FrameInvoiceList.Controls.Add(Me.LabSelectInvoice)
        Me.FrameInvoiceList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameInvoiceList.Location = New System.Drawing.Point(31, 292)
        Me.FrameInvoiceList.Name = "FrameInvoiceList"
        Me.FrameInvoiceList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameInvoiceList.Size = New System.Drawing.Size(377, 325)
        Me.FrameInvoiceList.TabIndex = 76
        Me.FrameInvoiceList.TabStop = False
        Me.FrameInvoiceList.Text = "FrameInvoiceList"
        '
        'cmdSelectInvoice
        '
        Me.cmdSelectInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelectInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelectInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelectInvoice.Location = New System.Drawing.Point(296, 296)
        Me.cmdSelectInvoice.Name = "cmdSelectInvoice"
        Me.cmdSelectInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelectInvoice.Size = New System.Drawing.Size(57, 23)
        Me.cmdSelectInvoice.TabIndex = 80
        Me.cmdSelectInvoice.Text = "Select"
        Me.cmdSelectInvoice.UseVisualStyleBackColor = False
        '
        'Chk12Mths
        '
        Me.Chk12Mths.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Chk12Mths.Cursor = System.Windows.Forms.Cursors.Default
        Me.Chk12Mths.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Chk12Mths.Location = New System.Drawing.Point(8, 288)
        Me.Chk12Mths.Name = "Chk12Mths"
        Me.Chk12Mths.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Chk12Mths.Size = New System.Drawing.Size(89, 25)
        Me.Chk12Mths.TabIndex = 78
        Me.Chk12Mths.Text = "Show Last      12 Mths only"
        Me.Chk12Mths.UseVisualStyleBackColor = False
        '
        'ListViewGoods
        '
        Me.ListViewGoods.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ListViewGoods.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewGoods.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ListViewGoods.FullRowSelect = True
        Me.ListViewGoods.GridLines = True
        Me.ListViewGoods.HideSelection = False
        Me.ListViewGoods.Location = New System.Drawing.Point(8, 22)
        Me.ListViewGoods.Name = "ListViewGoods"
        Me.ListViewGoods.Size = New System.Drawing.Size(365, 257)
        Me.ListViewGoods.SmallImageList = Me.ImageList1
        Me.ListViewGoods.TabIndex = 77
        Me.ListViewGoods.UseCompatibleStateImageBehavior = False
        Me.ListViewGoods.View = System.Windows.Forms.View.Details
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageList1.Images.SetKeyName(0, "ArrowUp")
        Me.ImageList1.Images.SetKeyName(1, "ArrowDown")
        '
        'LabSelectInvoice
        '
        Me.LabSelectInvoice.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LabSelectInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabSelectInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabSelectInvoice.Location = New System.Drawing.Point(112, 288)
        Me.LabSelectInvoice.Name = "LabSelectInvoice"
        Me.LabSelectInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabSelectInvoice.Size = New System.Drawing.Size(137, 25)
        Me.LabSelectInvoice.TabIndex = 79
        Me.LabSelectInvoice.Text = "Select Suppliers Invoice    for this RMA"
        '
        'grpBoxOrigin
        '
        Me.grpBoxOrigin.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpBoxOrigin.Controls.Add(Me.panelOrigin)
        Me.grpBoxOrigin.Controls.Add(Me.txtItemSaleInvoiceDate)
        Me.grpBoxOrigin.Controls.Add(Me.txtItemSaleInvoiceNo)
        Me.grpBoxOrigin.Controls.Add(Me.labItemSaleHdr)
        Me.grpBoxOrigin.Controls.Add(Me.Label16)
        Me.grpBoxOrigin.Controls.Add(Me.txtCustMobile)
        Me.grpBoxOrigin.Controls.Add(Me.chkTfrToStock)
        Me.grpBoxOrigin.Controls.Add(Me.txtCustPhone)
        Me.grpBoxOrigin.Controls.Add(Me.txtCustName)
        Me.grpBoxOrigin.Controls.Add(Me.txtCustCompany)
        Me.grpBoxOrigin.Location = New System.Drawing.Point(16, 560)
        Me.grpBoxOrigin.Name = "grpBoxOrigin"
        Me.grpBoxOrigin.Size = New System.Drawing.Size(385, 169)
        Me.grpBoxOrigin.TabIndex = 82
        Me.grpBoxOrigin.TabStop = False
        Me.grpBoxOrigin.Text = "Item Source:"
        '
        'panelOrigin
        '
        Me.panelOrigin.Controls.Add(Me.optOrigin_stock)
        Me.panelOrigin.Controls.Add(Me.Label19)
        Me.panelOrigin.Controls.Add(Me.optOrigin_counter)
        Me.panelOrigin.Controls.Add(Me.txtJobNo)
        Me.panelOrigin.Controls.Add(Me.txtCustNo)
        Me.panelOrigin.Controls.Add(Me.labOrigin)
        Me.panelOrigin.Controls.Add(Me.Label8)
        Me.panelOrigin.Controls.Add(Me.LabJobNo)
        Me.panelOrigin.Controls.Add(Me.optOrigin_job)
        Me.panelOrigin.Location = New System.Drawing.Point(3, 8)
        Me.panelOrigin.Name = "panelOrigin"
        Me.panelOrigin.Size = New System.Drawing.Size(225, 85)
        Me.panelOrigin.TabIndex = 109
        '
        'labOrigin
        '
        Me.labOrigin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.labOrigin.Location = New System.Drawing.Point(11, 12)
        Me.labOrigin.Name = "labOrigin"
        Me.labOrigin.Size = New System.Drawing.Size(18, 33)
        Me.labOrigin.TabIndex = 104
        '
        'txtItemSaleInvoiceDate
        '
        Me.txtItemSaleInvoiceDate.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.txtItemSaleInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtItemSaleInvoiceDate.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemSaleInvoiceDate.Location = New System.Drawing.Point(290, 60)
        Me.txtItemSaleInvoiceDate.Name = "txtItemSaleInvoiceDate"
        Me.txtItemSaleInvoiceDate.ReadOnly = True
        Me.txtItemSaleInvoiceDate.Size = New System.Drawing.Size(87, 14)
        Me.txtItemSaleInvoiceDate.TabIndex = 108
        Me.txtItemSaleInvoiceDate.Text = "txtItemSaleInvoiceDate"
        '
        'txtItemSaleInvoiceNo
        '
        Me.txtItemSaleInvoiceNo.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.txtItemSaleInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtItemSaleInvoiceNo.Font = New System.Drawing.Font("Lucida Sans", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemSaleInvoiceNo.Location = New System.Drawing.Point(238, 60)
        Me.txtItemSaleInvoiceNo.Name = "txtItemSaleInvoiceNo"
        Me.txtItemSaleInvoiceNo.ReadOnly = True
        Me.txtItemSaleInvoiceNo.Size = New System.Drawing.Size(47, 14)
        Me.txtItemSaleInvoiceNo.TabIndex = 107
        Me.txtItemSaleInvoiceNo.Text = "txtItemSaleInvoiceNo"
        Me.txtItemSaleInvoiceNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'labItemSaleHdr
        '
        Me.labItemSaleHdr.BackColor = System.Drawing.Color.LavenderBlush
        Me.labItemSaleHdr.Location = New System.Drawing.Point(237, 40)
        Me.labItemSaleHdr.Name = "labItemSaleHdr"
        Me.labItemSaleHdr.Size = New System.Drawing.Size(134, 17)
        Me.labItemSaleHdr.TabIndex = 106
        Me.labItemSaleHdr.Text = "labItemSaleHdr"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Lavender
        Me.Label16.Location = New System.Drawing.Point(237, 17)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(127, 17)
        Me.Label16.TabIndex = 105
        Me.Label16.Text = "Item Sale Info-"
        '
        'Picture2
        '
        Me.Picture2.BackColor = System.Drawing.SystemColors.Control
        Me.Picture2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Picture2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Picture2.Location = New System.Drawing.Point(374, 658)
        Me.Picture2.Name = "Picture2"
        Me.Picture2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture2.Size = New System.Drawing.Size(49, 33)
        Me.Picture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture2.TabIndex = 75
        Me.Picture2.TabStop = False
        '
        'grpBoxItemPic
        '
        Me.grpBoxItemPic.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grpBoxItemPic.Controls.Add(Me.picSubjectItem)
        Me.grpBoxItemPic.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxItemPic.Location = New System.Drawing.Point(299, 16)
        Me.grpBoxItemPic.Name = "grpBoxItemPic"
        Me.grpBoxItemPic.Size = New System.Drawing.Size(72, 70)
        Me.grpBoxItemPic.TabIndex = 100
        Me.grpBoxItemPic.TabStop = False
        Me.grpBoxItemPic.Text = "grpBoxItemPic"
        '
        'labUserPrompt0
        '
        Me.labUserPrompt0.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.labUserPrompt0.Cursor = System.Windows.Forms.Cursors.Default
        Me.labUserPrompt0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labUserPrompt0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labUserPrompt0.Location = New System.Drawing.Point(5, 3)
        Me.labUserPrompt0.Name = "labUserPrompt0"
        Me.labUserPrompt0.Padding = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.labUserPrompt0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labUserPrompt0.Size = New System.Drawing.Size(105, 72)
        Me.labUserPrompt0.TabIndex = 100
        Me.labUserPrompt0.Text = "labUserPrompt0"
        '
        'labUserPrompt1
        '
        Me.labUserPrompt1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labUserPrompt1.Cursor = System.Windows.Forms.Cursors.Default
        Me.labUserPrompt1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labUserPrompt1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labUserPrompt1.Location = New System.Drawing.Point(116, 3)
        Me.labUserPrompt1.Name = "labUserPrompt1"
        Me.labUserPrompt1.Padding = New System.Windows.Forms.Padding(3)
        Me.labUserPrompt1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labUserPrompt1.Size = New System.Drawing.Size(91, 72)
        Me.labUserPrompt1.TabIndex = 73
        Me.labUserPrompt1.Text = "labUserPrompt1"
        '
        'FrameRMARequest
        '
        Me.FrameRMARequest.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameRMARequest.Controls.Add(Me.labStep2)
        Me.FrameRMARequest.Controls.Add(Me.labStep1)
        Me.FrameRMARequest.Controls.Add(Me.Label10)
        Me.FrameRMARequest.Controls.Add(Me.chkRMARequested)
        Me.FrameRMARequest.Controls.Add(Me.txtRMAReceived)
        Me.FrameRMARequest.Controls.Add(Me._OptRMAResult_0)
        Me.FrameRMARequest.Controls.Add(Me._OptRMAResult_1)
        Me.FrameRMARequest.Controls.Add(Me.LabDateRMARequested)
        Me.FrameRMARequest.Controls.Add(Me.LabDateRMAResult)
        Me.FrameRMARequest.Controls.Add(Me.LabRMAREceived)
        Me.FrameRMARequest.Controls.Add(Me.LabRMAResult)
        Me.FrameRMARequest.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameRMARequest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameRMARequest.Location = New System.Drawing.Point(2, 79)
        Me.FrameRMARequest.Name = "FrameRMARequest"
        Me.FrameRMARequest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameRMARequest.Size = New System.Drawing.Size(324, 181)
        Me.FrameRMARequest.TabIndex = 75
        Me.FrameRMARequest.TabStop = False
        Me.FrameRMARequest.Text = "Requesting RMA.."
        '
        'labStep2
        '
        Me.labStep2.BackColor = System.Drawing.Color.LawnGreen
        Me.labStep2.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStep2.Location = New System.Drawing.Point(11, 77)
        Me.labStep2.Name = "labStep2"
        Me.labStep2.Size = New System.Drawing.Size(27, 25)
        Me.labStep2.TabIndex = 75
        Me.labStep2.Text = "2"
        Me.labStep2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labStep1
        '
        Me.labStep1.BackColor = System.Drawing.Color.LawnGreen
        Me.labStep1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStep1.Location = New System.Drawing.Point(11, 19)
        Me.labStep1.Name = "labStep1"
        Me.labStep1.Size = New System.Drawing.Size(27, 25)
        Me.labStep1.TabIndex = 74
        Me.labStep1.Text = "1"
        Me.labStep1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Gainsboro
        Me.Label10.Location = New System.Drawing.Point(13, 59)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(285, 7)
        Me.Label10.TabIndex = 73
        '
        'chkRMARequested
        '
        Me.chkRMARequested.BackColor = System.Drawing.Color.Transparent
        Me.chkRMARequested.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRMARequested.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRMARequested.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRMARequested.Location = New System.Drawing.Point(47, 15)
        Me.chkRMARequested.Name = "chkRMARequested"
        Me.chkRMARequested.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRMARequested.Size = New System.Drawing.Size(79, 42)
        Me.chkRMARequested.TabIndex = 14
        Me.chkRMARequested.Text = "RMA Requested"
        Me.chkRMARequested.UseVisualStyleBackColor = False
        '
        'txtRMAReceived
        '
        Me.txtRMAReceived.AcceptsReturn = True
        Me.txtRMAReceived.BackColor = System.Drawing.Color.AliceBlue
        Me.txtRMAReceived.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRMAReceived.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRMAReceived.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRMAReceived.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRMAReceived.Location = New System.Drawing.Point(66, 147)
        Me.txtRMAReceived.MaxLength = 24
        Me.txtRMAReceived.Name = "txtRMAReceived"
        Me.txtRMAReceived.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRMAReceived.Size = New System.Drawing.Size(233, 21)
        Me.txtRMAReceived.TabIndex = 18
        Me.txtRMAReceived.Text = "txtRMAReceived"
        '
        '_OptRMAResult_0
        '
        Me._OptRMAResult_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me._OptRMAResult_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRMAResult_0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptRMAResult_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptRMAResult.SetIndex(Me._OptRMAResult_0, CType(0, Short))
        Me._OptRMAResult_0.Location = New System.Drawing.Point(126, 77)
        Me._OptRMAResult_0.Name = "_OptRMAResult_0"
        Me._OptRMAResult_0.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me._OptRMAResult_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRMAResult_0.Size = New System.Drawing.Size(78, 25)
        Me._OptRMAResult_0.TabIndex = 16
        Me._OptRMAResult_0.TabStop = True
        Me._OptRMAResult_0.Text = "Granted"
        Me._OptRMAResult_0.UseVisualStyleBackColor = False
        '
        '_OptRMAResult_1
        '
        Me._OptRMAResult_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me._OptRMAResult_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRMAResult_1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptRMAResult_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptRMAResult.SetIndex(Me._OptRMAResult_1, CType(1, Short))
        Me._OptRMAResult_1.Location = New System.Drawing.Point(206, 77)
        Me._OptRMAResult_1.Name = "_OptRMAResult_1"
        Me._OptRMAResult_1.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me._OptRMAResult_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRMAResult_1.Size = New System.Drawing.Size(81, 25)
        Me._OptRMAResult_1.TabIndex = 17
        Me._OptRMAResult_1.TabStop = True
        Me._OptRMAResult_1.Text = "Refused"
        Me._OptRMAResult_1.UseVisualStyleBackColor = False
        '
        'LabDateRMARequested
        '
        Me.LabDateRMARequested.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.LabDateRMARequested.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDateRMARequested.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDateRMARequested.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDateRMARequested.Location = New System.Drawing.Point(132, 33)
        Me.LabDateRMARequested.Name = "LabDateRMARequested"
        Me.LabDateRMARequested.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDateRMARequested.Size = New System.Drawing.Size(166, 18)
        Me.LabDateRMARequested.TabIndex = 72
        Me.LabDateRMARequested.Text = "LabDateRMARequested"
        '
        'LabDateRMAResult
        '
        Me.LabDateRMAResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.LabDateRMAResult.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDateRMAResult.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDateRMAResult.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDateRMAResult.Location = New System.Drawing.Point(139, 105)
        Me.LabDateRMAResult.Name = "LabDateRMAResult"
        Me.LabDateRMAResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDateRMAResult.Size = New System.Drawing.Size(160, 19)
        Me.LabDateRMAResult.TabIndex = 71
        Me.LabDateRMAResult.Text = "LabDateRMAResult"
        '
        'LabRMAREceived
        '
        Me.LabRMAREceived.BackColor = System.Drawing.Color.Transparent
        Me.LabRMAREceived.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRMAREceived.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRMAREceived.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRMAREceived.Location = New System.Drawing.Point(60, 131)
        Me.LabRMAREceived.Name = "LabRMAREceived"
        Me.LabRMAREceived.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRMAREceived.Size = New System.Drawing.Size(239, 15)
        Me.LabRMAREceived.TabIndex = 70
        Me.LabRMAREceived.Text = "Enter RMA Received from Supplier"
        '
        'LabRMAResult
        '
        Me.LabRMAResult.BackColor = System.Drawing.Color.Transparent
        Me.LabRMAResult.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRMAResult.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRMAResult.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRMAResult.Location = New System.Drawing.Point(44, 77)
        Me.LabRMAResult.Name = "LabRMAResult"
        Me.LabRMAResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRMAResult.Size = New System.Drawing.Size(73, 29)
        Me.LabRMAResult.TabIndex = 69
        Me.LabRMAResult.Text = "RMA Request Result"
        Me.LabRMAResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRequestNotes
        '
        Me.txtRequestNotes.AcceptsReturn = True
        Me.txtRequestNotes.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.txtRequestNotes.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRequestNotes.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestNotes.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRequestNotes.Location = New System.Drawing.Point(10, 294)
        Me.txtRequestNotes.MaxLength = 0
        Me.txtRequestNotes.Multiline = True
        Me.txtRequestNotes.Name = "txtRequestNotes"
        Me.txtRequestNotes.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRequestNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRequestNotes.Size = New System.Drawing.Size(224, 183)
        Me.txtRequestNotes.TabIndex = 19
        Me.txtRequestNotes.Text = "txtRequestNotes"
        '
        'txtRequestNotesHistory
        '
        Me.txtRequestNotesHistory.AcceptsReturn = True
        Me.txtRequestNotesHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtRequestNotesHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRequestNotesHistory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRequestNotesHistory.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestNotesHistory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRequestNotesHistory.Location = New System.Drawing.Point(10, 99)
        Me.txtRequestNotesHistory.MaxLength = 0
        Me.txtRequestNotesHistory.Multiline = True
        Me.txtRequestNotesHistory.Name = "txtRequestNotesHistory"
        Me.txtRequestNotesHistory.ReadOnly = True
        Me.txtRequestNotesHistory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRequestNotesHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRequestNotesHistory.Size = New System.Drawing.Size(221, 151)
        Me.txtRequestNotesHistory.TabIndex = 15
        Me.txtRequestNotesHistory.Text = "txtRequestNotesHistory" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(7, 277)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(166, 17)
        Me.Label15.TabIndex = 89
        Me.Label15.Text = "Add New Note to Request File-"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(8, 82)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(136, 15)
        Me.Label12.TabIndex = 88
        Me.Label12.Text = "Previous Notes-"
        '
        'labUserPrompt2
        '
        Me.labUserPrompt2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.labUserPrompt2.Cursor = System.Windows.Forms.Cursors.Default
        Me.labUserPrompt2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labUserPrompt2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labUserPrompt2.Location = New System.Drawing.Point(211, 3)
        Me.labUserPrompt2.Name = "labUserPrompt2"
        Me.labUserPrompt2.Padding = New System.Windows.Forms.Padding(3)
        Me.labUserPrompt2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labUserPrompt2.Size = New System.Drawing.Size(91, 72)
        Me.labUserPrompt2.TabIndex = 74
        Me.labUserPrompt2.Text = "labUserPrompt2"
        '
        'FrameGoods
        '
        Me.FrameGoods.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameGoods.Controls.Add(Me.labStep4)
        Me.FrameGoods.Controls.Add(Me.labStep3)
        Me.FrameGoods.Controls.Add(Me.Label24)
        Me.FrameGoods.Controls.Add(Me.cboGoodsResult)
        Me.FrameGoods.Controls.Add(Me.chkGoodsSent)
        Me.FrameGoods.Controls.Add(Me.txtCourierBarcode)
        Me.FrameGoods.Controls.Add(Me.txtResultComments)
        Me.FrameGoods.Controls.Add(Me.LabCourierBarcode)
        Me.FrameGoods.Controls.Add(Me.LabDateGoodsSent)
        Me.FrameGoods.Controls.Add(Me.LabDateGoodsResult)
        Me.FrameGoods.Controls.Add(Me.Label7)
        Me.FrameGoods.Controls.Add(Me.LabResultHdr)
        Me.FrameGoods.Controls.Add(Me.LabGoodsResult)
        Me.FrameGoods.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameGoods.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameGoods.Location = New System.Drawing.Point(2, 264)
        Me.FrameGoods.Name = "FrameGoods"
        Me.FrameGoods.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameGoods.Size = New System.Drawing.Size(324, 233)
        Me.FrameGoods.TabIndex = 61
        Me.FrameGoods.TabStop = False
        Me.FrameGoods.Text = "Goods Progress"
        '
        'labStep4
        '
        Me.labStep4.BackColor = System.Drawing.Color.LawnGreen
        Me.labStep4.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStep4.Location = New System.Drawing.Point(11, 128)
        Me.labStep4.Name = "labStep4"
        Me.labStep4.Size = New System.Drawing.Size(27, 25)
        Me.labStep4.TabIndex = 77
        Me.labStep4.Text = "4"
        Me.labStep4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'labStep3
        '
        Me.labStep3.BackColor = System.Drawing.Color.LawnGreen
        Me.labStep3.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStep3.Location = New System.Drawing.Point(11, 26)
        Me.labStep3.Name = "labStep3"
        Me.labStep3.Size = New System.Drawing.Size(27, 25)
        Me.labStep3.TabIndex = 76
        Me.labStep3.Text = "3"
        Me.labStep3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Gainsboro
        Me.Label24.Location = New System.Drawing.Point(14, 100)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(285, 7)
        Me.Label24.TabIndex = 74
        '
        'cboGoodsResult
        '
        Me.cboGoodsResult.BackColor = System.Drawing.Color.Lavender
        Me.cboGoodsResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGoodsResult.FormattingEnabled = True
        Me.cboGoodsResult.Location = New System.Drawing.Point(182, 122)
        Me.cboGoodsResult.Name = "cboGoodsResult"
        Me.cboGoodsResult.Size = New System.Drawing.Size(121, 21)
        Me.cboGoodsResult.TabIndex = 68
        '
        'chkGoodsSent
        '
        Me.chkGoodsSent.BackColor = System.Drawing.Color.Transparent
        Me.chkGoodsSent.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGoodsSent.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGoodsSent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGoodsSent.Location = New System.Drawing.Point(49, 21)
        Me.chkGoodsSent.Name = "chkGoodsSent"
        Me.chkGoodsSent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGoodsSent.Size = New System.Drawing.Size(91, 25)
        Me.chkGoodsSent.TabIndex = 21
        Me.chkGoodsSent.Text = "Goods Sent"
        Me.chkGoodsSent.UseVisualStyleBackColor = False
        '
        'txtCourierBarcode
        '
        Me.txtCourierBarcode.AcceptsReturn = True
        Me.txtCourierBarcode.BackColor = System.Drawing.Color.AliceBlue
        Me.txtCourierBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCourierBarcode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCourierBarcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCourierBarcode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCourierBarcode.Location = New System.Drawing.Point(66, 64)
        Me.txtCourierBarcode.MaxLength = 31
        Me.txtCourierBarcode.Name = "txtCourierBarcode"
        Me.txtCourierBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCourierBarcode.Size = New System.Drawing.Size(233, 21)
        Me.txtCourierBarcode.TabIndex = 22
        Me.txtCourierBarcode.Text = "txtCourierBarcode"
        '
        'txtResultComments
        '
        Me.txtResultComments.AcceptsReturn = True
        Me.txtResultComments.BackColor = System.Drawing.Color.AliceBlue
        Me.txtResultComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResultComments.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtResultComments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResultComments.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtResultComments.Location = New System.Drawing.Point(66, 200)
        Me.txtResultComments.MaxLength = 63
        Me.txtResultComments.Name = "txtResultComments"
        Me.txtResultComments.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtResultComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtResultComments.Size = New System.Drawing.Size(233, 21)
        Me.txtResultComments.TabIndex = 29
        Me.txtResultComments.Text = "txtResultComments"
        '
        'LabCourierBarcode
        '
        Me.LabCourierBarcode.BackColor = System.Drawing.Color.Transparent
        Me.LabCourierBarcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabCourierBarcode.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabCourierBarcode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabCourierBarcode.Location = New System.Drawing.Point(65, 49)
        Me.LabCourierBarcode.Name = "LabCourierBarcode"
        Me.LabCourierBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabCourierBarcode.Size = New System.Drawing.Size(145, 17)
        Me.LabCourierBarcode.TabIndex = 67
        Me.LabCourierBarcode.Text = "Freight Tracking No:"
        '
        'LabDateGoodsSent
        '
        Me.LabDateGoodsSent.BackColor = System.Drawing.Color.Gainsboro
        Me.LabDateGoodsSent.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDateGoodsSent.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDateGoodsSent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDateGoodsSent.Location = New System.Drawing.Point(145, 25)
        Me.LabDateGoodsSent.Name = "LabDateGoodsSent"
        Me.LabDateGoodsSent.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.LabDateGoodsSent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDateGoodsSent.Size = New System.Drawing.Size(158, 18)
        Me.LabDateGoodsSent.TabIndex = 66
        Me.LabDateGoodsSent.Text = "LabDateGoodsSent"
        '
        'LabDateGoodsResult
        '
        Me.LabDateGoodsResult.BackColor = System.Drawing.Color.Gainsboro
        Me.LabDateGoodsResult.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDateGoodsResult.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDateGoodsResult.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDateGoodsResult.Location = New System.Drawing.Point(186, 156)
        Me.LabDateGoodsResult.Name = "LabDateGoodsResult"
        Me.LabDateGoodsResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDateGoodsResult.Size = New System.Drawing.Size(117, 17)
        Me.LabDateGoodsResult.TabIndex = 65
        Me.LabDateGoodsResult.Text = "LabDateGoodsResult"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(64, 182)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(187, 15)
        Me.Label7.TabIndex = 64
        Me.Label7.Text = "ENTER Replacement Serial No: "
        '
        'LabResultHdr
        '
        Me.LabResultHdr.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.LabResultHdr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabResultHdr.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabResultHdr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabResultHdr.Location = New System.Drawing.Point(45, 122)
        Me.LabResultHdr.Name = "LabResultHdr"
        Me.LabResultHdr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabResultHdr.Size = New System.Drawing.Size(130, 22)
        Me.LabResultHdr.TabIndex = 63
        Me.LabResultHdr.Text = "Select Goods Result"
        Me.LabResultHdr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabGoodsResult
        '
        Me.LabGoodsResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LabGoodsResult.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabGoodsResult.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabGoodsResult.ForeColor = System.Drawing.Color.White
        Me.LabGoodsResult.Location = New System.Drawing.Point(65, 156)
        Me.LabGoodsResult.Name = "LabGoodsResult"
        Me.LabGoodsResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabGoodsResult.Size = New System.Drawing.Size(115, 14)
        Me.LabGoodsResult.TabIndex = 62
        Me.LabGoodsResult.Text = "LabGoodsResult"
        '
        'txtUpdatedName
        '
        Me.txtUpdatedName.AcceptsReturn = True
        Me.txtUpdatedName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtUpdatedName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUpdatedName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUpdatedName.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUpdatedName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUpdatedName.Location = New System.Drawing.Point(293, 83)
        Me.txtUpdatedName.MaxLength = 0
        Me.txtUpdatedName.Name = "txtUpdatedName"
        Me.txtUpdatedName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUpdatedName.Size = New System.Drawing.Size(81, 15)
        Me.txtUpdatedName.TabIndex = 52
        Me.txtUpdatedName.TabStop = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmdCancel.CausesValidation = False
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(904, 678)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 32)
        Me.cmdCancel.TabIndex = 39
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'FrameItem
        '
        Me.FrameItem.BackColor = System.Drawing.Color.WhiteSmoke
        Me.FrameItem.Controls.Add(Me.labStartHere)
        Me.FrameItem.Controls.Add(Me.Label13)
        Me.FrameItem.Controls.Add(Me.chkKeepScannedLeadZeroes)
        Me.FrameItem.Controls.Add(Me.grpBoxItemPic)
        Me.FrameItem.Controls.Add(Me.txtItemDescription)
        Me.FrameItem.Controls.Add(Me.txtItemBarcode)
        Me.FrameItem.Controls.Add(Me.txtItemSerial)
        Me.FrameItem.Controls.Add(Me.Label11)
        Me.FrameItem.Controls.Add(Me.LabItemSerial)
        Me.FrameItem.Controls.Add(Me.labProdBarcode)
        Me.FrameItem.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FrameItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FrameItem.Location = New System.Drawing.Point(16, 99)
        Me.FrameItem.Name = "FrameItem"
        Me.FrameItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FrameItem.Size = New System.Drawing.Size(385, 190)
        Me.FrameItem.TabIndex = 42
        Me.FrameItem.TabStop = False
        Me.FrameItem.Text = "Item Details"
        '
        'labStartHere
        '
        Me.labStartHere.BackColor = System.Drawing.Color.LawnGreen
        Me.labStartHere.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labStartHere.Location = New System.Drawing.Point(8, 17)
        Me.labStartHere.Name = "labStartHere"
        Me.labStartHere.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.labStartHere.Size = New System.Drawing.Size(61, 35)
        Me.labStartHere.TabIndex = 57
        Me.labStartHere.Text = "Start Here >"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(215, 73)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(84, 13)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "(F2 to Lookup)"
        '
        'chkKeepScannedLeadZeroes
        '
        Me.chkKeepScannedLeadZeroes.Font = New System.Drawing.Font("Tahoma", 6.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkKeepScannedLeadZeroes.Location = New System.Drawing.Point(252, 85)
        Me.chkKeepScannedLeadZeroes.Name = "chkKeepScannedLeadZeroes"
        Me.chkKeepScannedLeadZeroes.Size = New System.Drawing.Size(102, 36)
        Me.chkKeepScannedLeadZeroes.TabIndex = 55
        Me.chkKeepScannedLeadZeroes.Text = "Keep Scanned Leading Zeroes"
        Me.chkKeepScannedLeadZeroes.UseVisualStyleBackColor = True
        '
        'txtItemDescription
        '
        Me.txtItemDescription.AcceptsReturn = True
        Me.txtItemDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.txtItemDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtItemDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemDescription.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDescription.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemDescription.Location = New System.Drawing.Point(16, 133)
        Me.txtItemDescription.MaxLength = 0
        Me.txtItemDescription.Multiline = True
        Me.txtItemDescription.Name = "txtItemDescription"
        Me.txtItemDescription.ReadOnly = True
        Me.txtItemDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtItemDescription.Size = New System.Drawing.Size(345, 47)
        Me.txtItemDescription.TabIndex = 2
        Me.txtItemDescription.Text = "txtItemDescription"
        '
        'txtItemBarcode
        '
        Me.txtItemBarcode.AcceptsReturn = True
        Me.txtItemBarcode.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtItemBarcode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemBarcode.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemBarcode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemBarcode.Location = New System.Drawing.Point(18, 90)
        Me.txtItemBarcode.MaxLength = 0
        Me.txtItemBarcode.Name = "txtItemBarcode"
        Me.txtItemBarcode.ReadOnly = True
        Me.txtItemBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemBarcode.Size = New System.Drawing.Size(219, 20)
        Me.txtItemBarcode.TabIndex = 1
        Me.txtItemBarcode.Text = "txtItemBarcode"
        '
        'txtItemSerial
        '
        Me.txtItemSerial.AcceptsReturn = True
        Me.txtItemSerial.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.txtItemSerial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemSerial.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemSerial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemSerial.Location = New System.Drawing.Point(76, 32)
        Me.txtItemSerial.MaxLength = 40
        Me.txtItemSerial.Name = "txtItemSerial"
        Me.txtItemSerial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemSerial.Size = New System.Drawing.Size(212, 20)
        Me.txtItemSerial.TabIndex = 0
        Me.txtItemSerial.Text = "txtItemSerial"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(16, 117)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(81, 17)
        Me.Label11.TabIndex = 54
        Me.Label11.Text = "Description:"
        '
        'LabItemSerial
        '
        Me.LabItemSerial.BackColor = System.Drawing.Color.Transparent
        Me.LabItemSerial.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabItemSerial.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabItemSerial.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabItemSerial.Location = New System.Drawing.Point(76, 16)
        Me.LabItemSerial.Name = "LabItemSerial"
        Me.LabItemSerial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabItemSerial.Size = New System.Drawing.Size(177, 17)
        Me.LabItemSerial.TabIndex = 49
        Me.LabItemSerial.Text = "Scan or ENTER Item Serial No:"
        '
        'labProdBarcode
        '
        Me.labProdBarcode.BackColor = System.Drawing.Color.Transparent
        Me.labProdBarcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.labProdBarcode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labProdBarcode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.labProdBarcode.Location = New System.Drawing.Point(16, 71)
        Me.labProdBarcode.Name = "labProdBarcode"
        Me.labProdBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.labProdBarcode.Size = New System.Drawing.Size(202, 17)
        Me.labProdBarcode.TabIndex = 45
        Me.labProdBarcode.Text = "No Serial ?  Scan Product Barcode-"
        '
        'Picture1
        '
        Me.Picture1.BackColor = System.Drawing.Color.FromArgb(CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.Picture1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Picture1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Picture1.Image = CType(resources.GetObject("Picture1.Image"), System.Drawing.Image)
        Me.Picture1.Location = New System.Drawing.Point(461, 244)
        Me.Picture1.Name = "Picture1"
        Me.Picture1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Picture1.Size = New System.Drawing.Size(134, 93)
        Me.Picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.Picture1.TabIndex = 40
        Me.Picture1.TabStop = False
        '
        'txtCreatedName
        '
        Me.txtCreatedName.AcceptsReturn = True
        Me.txtCreatedName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCreatedName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCreatedName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreatedName.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreatedName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCreatedName.Location = New System.Drawing.Point(72, 62)
        Me.txtCreatedName.MaxLength = 0
        Me.txtCreatedName.Name = "txtCreatedName"
        Me.txtCreatedName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreatedName.Size = New System.Drawing.Size(97, 15)
        Me.txtCreatedName.TabIndex = 36
        Me.txtCreatedName.TabStop = False
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(16, 8)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(105, 25)
        Me.Label18.TabIndex = 97
        Me.Label18.Text = "JobMatix"
        '
        'LabPrevStaff
        '
        Me.LabPrevStaff.BackColor = System.Drawing.Color.Transparent
        Me.LabPrevStaff.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPrevStaff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPrevStaff.Location = New System.Drawing.Point(293, 59)
        Me.LabPrevStaff.Name = "LabPrevStaff"
        Me.LabPrevStaff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPrevStaff.Size = New System.Drawing.Size(97, 17)
        Me.LabPrevStaff.TabIndex = 59
        Me.LabPrevStaff.Text = "LabPrevStaff"
        '
        'LabPrevUpdate
        '
        Me.LabPrevUpdate.BackColor = System.Drawing.Color.Transparent
        Me.LabPrevUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabPrevUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabPrevUpdate.Location = New System.Drawing.Point(293, 45)
        Me.LabPrevUpdate.Name = "LabPrevUpdate"
        Me.LabPrevUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabPrevUpdate.Size = New System.Drawing.Size(97, 17)
        Me.LabPrevUpdate.TabIndex = 58
        Me.LabPrevUpdate.Text = "LabPrevUpdate"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(221, 45)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(66, 33)
        Me.Label14.TabIndex = 57
        Me.Label14.Text = "Updated:   By:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(42, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(25, 17)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "By:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabToday
        '
        Me.LabToday.BackColor = System.Drawing.Color.Transparent
        Me.LabToday.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabToday.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabToday.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabToday.Location = New System.Drawing.Point(421, 27)
        Me.LabToday.Name = "LabToday"
        Me.LabToday.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabToday.Size = New System.Drawing.Size(153, 17)
        Me.LabToday.TabIndex = 53
        Me.LabToday.Text = "LabToday"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(776, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(81, 31)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "Supplier's RMA No:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(411, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(73, 17)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "RA Status:"
        '
        'LabOurRANumber
        '
        Me.LabOurRANumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.LabOurRANumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabOurRANumber.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabOurRANumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabOurRANumber.Location = New System.Drawing.Point(859, 9)
        Me.LabOurRANumber.Name = "LabOurRANumber"
        Me.LabOurRANumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabOurRANumber.Size = New System.Drawing.Size(129, 33)
        Me.LabOurRANumber.TabIndex = 47
        Me.LabOurRANumber.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(772, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(73, 33)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Home RA Number:"
        '
        'LabVersion
        '
        Me.LabVersion.BackColor = System.Drawing.Color.Transparent
        Me.LabVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabVersion.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabVersion.Location = New System.Drawing.Point(740, 714)
        Me.LabVersion.Name = "LabVersion"
        Me.LabVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabVersion.Size = New System.Drawing.Size(251, 11)
        Me.LabVersion.TabIndex = 44
        Me.LabVersion.Text = "LabVersion"
        Me.LabVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabHdr2
        '
        Me.LabHdr2.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr2.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr2.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHdr2.Location = New System.Drawing.Point(419, 718)
        Me.LabHdr2.Name = "LabHdr2"
        Me.LabHdr2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr2.Size = New System.Drawing.Size(313, 11)
        Me.LabHdr2.TabIndex = 43
        Me.LabHdr2.Text = "labHdr2"
        '
        'LabHd3
        '
        Me.LabHd3.BackColor = System.Drawing.Color.Transparent
        Me.LabHd3.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHd3.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHd3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabHd3.Location = New System.Drawing.Point(356, 10)
        Me.LabHd3.Name = "LabHd3"
        Me.LabHd3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHd3.Size = New System.Drawing.Size(225, 17)
        Me.LabHd3.TabIndex = 41
        Me.LabHd3.Text = " Return Merchandise Authorisation"
        '
        'LabDateCreated
        '
        Me.LabDateCreated.BackColor = System.Drawing.Color.Transparent
        Me.LabDateCreated.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabDateCreated.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDateCreated.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabDateCreated.Location = New System.Drawing.Point(72, 45)
        Me.LabDateCreated.Name = "LabDateCreated"
        Me.LabDateCreated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabDateCreated.Size = New System.Drawing.Size(113, 17)
        Me.LabDateCreated.TabIndex = 38
        Me.LabDateCreated.Text = "LabDateCreated"
        '
        'LabRcvdBy
        '
        Me.LabRcvdBy.BackColor = System.Drawing.Color.Transparent
        Me.LabRcvdBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabRcvdBy.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabRcvdBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LabRcvdBy.Location = New System.Drawing.Point(16, 45)
        Me.LabRcvdBy.Name = "LabRcvdBy"
        Me.LabRcvdBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabRcvdBy.Size = New System.Drawing.Size(57, 17)
        Me.LabRcvdBy.TabIndex = 33
        Me.LabRcvdBy.Text = "Created:"
        '
        'LabHdr1
        '
        Me.LabHdr1.BackColor = System.Drawing.Color.Transparent
        Me.LabHdr1.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabHdr1.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabHdr1.ForeColor = System.Drawing.Color.DarkBlue
        Me.LabHdr1.Location = New System.Drawing.Point(130, 8)
        Me.LabHdr1.Name = "LabHdr1"
        Me.LabHdr1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabHdr1.Size = New System.Drawing.Size(215, 29)
        Me.LabHdr1.TabIndex = 31
        Me.LabHdr1.Text = "New RA Record"
        '
        'OptRMAResult
        '
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(229, 82)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 18)
        Me.Label9.TabIndex = 98
        Me.Label9.Text = "You are:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Linen
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Blue
        Me.Label21.Location = New System.Drawing.Point(551, 640)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(98, 13)
        Me.Label21.TabIndex = 100
        Me.Label21.Text = "-- A4 Printer --"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Gainsboro
        Me.Label2.Location = New System.Drawing.Point(548, 707)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(230, 3)
        Me.Label2.TabIndex = 101
        '
        'SSTabMain
        '
        Me.SSTabMain.Controls.Add(Me.MainTabPage_progress)
        Me.SSTabMain.Controls.Add(Me.mainTabPage_attachments)
        Me.SSTabMain.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTabMain.Location = New System.Drawing.Point(411, 104)
        Me.SSTabMain.Name = "SSTabMain"
        Me.SSTabMain.SelectedIndex = 0
        Me.SSTabMain.Size = New System.Drawing.Size(579, 528)
        Me.SSTabMain.TabIndex = 102
        '
        'MainTabPage_progress
        '
        Me.MainTabPage_progress.BackColor = System.Drawing.Color.LightPink
        Me.MainTabPage_progress.Controls.Add(Me.grpBoxNotes)
        Me.MainTabPage_progress.Controls.Add(Me.FrameRMARequest)
        Me.MainTabPage_progress.Controls.Add(Me.labUserPrompt0)
        Me.MainTabPage_progress.Controls.Add(Me.FrameGoods)
        Me.MainTabPage_progress.Controls.Add(Me.labUserPrompt1)
        Me.MainTabPage_progress.Controls.Add(Me.labUserPrompt2)
        Me.MainTabPage_progress.Location = New System.Drawing.Point(4, 25)
        Me.MainTabPage_progress.Name = "MainTabPage_progress"
        Me.MainTabPage_progress.Padding = New System.Windows.Forms.Padding(3)
        Me.MainTabPage_progress.Size = New System.Drawing.Size(571, 499)
        Me.MainTabPage_progress.TabIndex = 0
        Me.MainTabPage_progress.Text = "RMA Progress"
        '
        'grpBoxNotes
        '
        Me.grpBoxNotes.BackColor = System.Drawing.Color.LightYellow
        Me.grpBoxNotes.Controls.Add(Me.cmdNewSupplier2)
        Me.grpBoxNotes.Controls.Add(Me.btnUpdateSupplierAddress)
        Me.grpBoxNotes.Controls.Add(Me.txtRequestNotes)
        Me.grpBoxNotes.Controls.Add(Me.txtRequestNotesHistory)
        Me.grpBoxNotes.Controls.Add(Me.Label15)
        Me.grpBoxNotes.Controls.Add(Me.cmdReOpen)
        Me.grpBoxNotes.Controls.Add(Me.Label12)
        Me.grpBoxNotes.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxNotes.Location = New System.Drawing.Point(329, 3)
        Me.grpBoxNotes.Name = "grpBoxNotes"
        Me.grpBoxNotes.Size = New System.Drawing.Size(239, 492)
        Me.grpBoxNotes.TabIndex = 101
        Me.grpBoxNotes.TabStop = False
        Me.grpBoxNotes.Text = "grpBoxNotes"
        '
        'mainTabPage_attachments
        '
        Me.mainTabPage_attachments.BackColor = System.Drawing.Color.White
        Me.mainTabPage_attachments.Controls.Add(Me.grpBoxItem)
        Me.mainTabPage_attachments.Controls.Add(Me.grpBoxAddNew)
        Me.mainTabPage_attachments.Location = New System.Drawing.Point(4, 25)
        Me.mainTabPage_attachments.Name = "mainTabPage_attachments"
        Me.mainTabPage_attachments.Padding = New System.Windows.Forms.Padding(3)
        Me.mainTabPage_attachments.Size = New System.Drawing.Size(571, 499)
        Me.mainTabPage_attachments.TabIndex = 1
        Me.mainTabPage_attachments.Text = "Attachments"
        '
        'grpBoxItem
        '
        Me.grpBoxItem.Controls.Add(Me.picMsExcel)
        Me.grpBoxItem.Controls.Add(Me.picMsWord)
        Me.grpBoxItem.Controls.Add(Me.picPDF)
        Me.grpBoxItem.Controls.Add(Me.txtComments)
        Me.grpBoxItem.Controls.Add(Me.btnViewDoc)
        Me.grpBoxItem.Controls.Add(Me.btnDelete)
        Me.grpBoxItem.Controls.Add(Me.picProduct)
        Me.grpBoxItem.Controls.Add(Me.lvwDocs)
        Me.grpBoxItem.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxItem.Location = New System.Drawing.Point(8, 181)
        Me.grpBoxItem.Name = "grpBoxItem"
        Me.grpBoxItem.Size = New System.Drawing.Size(538, 273)
        Me.grpBoxItem.TabIndex = 21
        Me.grpBoxItem.TabStop = False
        Me.grpBoxItem.Text = "grpBoxItem"
        '
        'picMsExcel
        '
        Me.picMsExcel.Image = CType(resources.GetObject("picMsExcel.Image"), System.Drawing.Image)
        Me.picMsExcel.Location = New System.Drawing.Point(7, 223)
        Me.picMsExcel.Name = "picMsExcel"
        Me.picMsExcel.Size = New System.Drawing.Size(53, 53)
        Me.picMsExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMsExcel.TabIndex = 29
        Me.picMsExcel.TabStop = False
        '
        'picMsWord
        '
        Me.picMsWord.Image = CType(resources.GetObject("picMsWord.Image"), System.Drawing.Image)
        Me.picMsWord.Location = New System.Drawing.Point(68, 188)
        Me.picMsWord.Name = "picMsWord"
        Me.picMsWord.Size = New System.Drawing.Size(53, 53)
        Me.picMsWord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picMsWord.TabIndex = 28
        Me.picMsWord.TabStop = False
        '
        'picPDF
        '
        Me.picPDF.Image = CType(resources.GetObject("picPDF.Image"), System.Drawing.Image)
        Me.picPDF.Location = New System.Drawing.Point(5, 167)
        Me.picPDF.Name = "picPDF"
        Me.picPDF.Size = New System.Drawing.Size(51, 51)
        Me.picPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPDF.TabIndex = 23
        Me.picPDF.TabStop = False
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer), CType(CType(244, Byte), Integer))
        Me.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtComments.Font = New System.Drawing.Font("Lucida Sans", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComments.Location = New System.Drawing.Point(188, 171)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ReadOnly = True
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(261, 80)
        Me.txtComments.TabIndex = 17
        Me.txtComments.Text = "txtComments"
        '
        'picProduct
        '
        Me.picProduct.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.picProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picProduct.Location = New System.Drawing.Point(85, 171)
        Me.picProduct.Name = "picProduct"
        Me.picProduct.Size = New System.Drawing.Size(92, 92)
        Me.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picProduct.TabIndex = 13
        Me.picProduct.TabStop = False
        '
        'lvwDocs
        '
        Me.lvwDocs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.doc_id, Me.doc_date_created, Me.doc_file_title, Me.doc_file_size, Me.doc_staff})
        Me.lvwDocs.Font = New System.Drawing.Font("Lucida Sans", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvwDocs.FullRowSelect = True
        Me.lvwDocs.GridLines = True
        Me.lvwDocs.HideSelection = False
        Me.lvwDocs.Location = New System.Drawing.Point(6, 24)
        Me.lvwDocs.MultiSelect = False
        Me.lvwDocs.Name = "lvwDocs"
        Me.lvwDocs.Size = New System.Drawing.Size(514, 129)
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
        Me.doc_file_title.Width = 200
        '
        'doc_file_size
        '
        Me.doc_file_size.Text = "File Size"
        Me.doc_file_size.Width = 100
        '
        'doc_staff
        '
        Me.doc_staff.Text = "Staff"
        '
        'grpBoxAddNew
        '
        Me.grpBoxAddNew.Controls.Add(Me.txtNewFileName)
        Me.grpBoxAddNew.Controls.Add(Me.Label20)
        Me.grpBoxAddNew.Controls.Add(Me.labHelp)
        Me.grpBoxAddNew.Controls.Add(Me.Label22)
        Me.grpBoxAddNew.Controls.Add(Me.btnSaveAttachment)
        Me.grpBoxAddNew.Controls.Add(Me.txtNewComment)
        Me.grpBoxAddNew.Controls.Add(Me.btnBrowse)
        Me.grpBoxAddNew.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpBoxAddNew.Location = New System.Drawing.Point(8, 6)
        Me.grpBoxAddNew.Name = "grpBoxAddNew"
        Me.grpBoxAddNew.Size = New System.Drawing.Size(538, 157)
        Me.grpBoxAddNew.TabIndex = 20
        Me.grpBoxAddNew.TabStop = False
        Me.grpBoxAddNew.Text = "Add New Attachment"
        '
        'txtNewFileName
        '
        Me.txtNewFileName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNewFileName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNewFileName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewFileName.Location = New System.Drawing.Point(172, 31)
        Me.txtNewFileName.Multiline = True
        Me.txtNewFileName.Name = "txtNewFileName"
        Me.txtNewFileName.ReadOnly = True
        Me.txtNewFileName.Size = New System.Drawing.Size(255, 31)
        Me.txtNewFileName.TabIndex = 23
        Me.txtNewFileName.Text = "txtNewFileName"
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(172, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(74, 13)
        Me.Label20.TabIndex = 22
        Me.Label20.Text = "File to attach:"
        '
        'labHelp
        '
        Me.labHelp.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labHelp.Location = New System.Drawing.Point(6, 22)
        Me.labHelp.Name = "labHelp"
        Me.labHelp.Padding = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.labHelp.Size = New System.Drawing.Size(149, 104)
        Me.labHelp.TabIndex = 20
        Me.labHelp.Text = "To add an Attachment,, browse to the file to be attached, and Open.  Then Enter s" & _
    "ome comment, and Press Save.."
        '
        'Label22
        '
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(444, 68)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(76, 42)
        Me.Label22.TabIndex = 20
        Me.Label22.Text = "Must Have Comment for New Doc."
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'openDlg1
        '
        Me.openDlg1.FileName = "OpenFileDialog1"
        '
        'picSubjectMain
        '
        Me.picSubjectMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.picSubjectMain.Location = New System.Drawing.Point(658, 5)
        Me.picSubjectMain.Name = "picSubjectMain"
        Me.picSubjectMain.Size = New System.Drawing.Size(88, 90)
        Me.picSubjectMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picSubjectMain.TabIndex = 103
        Me.picSubjectMain.TabStop = False
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Linen
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Blue
        Me.Label23.Location = New System.Drawing.Point(668, 640)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(108, 13)
        Me.Label23.TabIndex = 106
        Me.Label23.Text = "-- Label Printer --"
        '
        'frmNewRA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(996, 731)
        Me.Controls.Add(Me.FrameSupplier)
        Me.Controls.Add(Me.grpBoxOrigin)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.cboItemLabelPrinters)
        Me.Controls.Add(Me.btnPrintItemLabel)
        Me.Controls.Add(Me.picSubjectMain)
        Me.Controls.Add(Me.FrameInvoiceList)
        Me.Controls.Add(Me.SSTabMain)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.cboPrinters)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmdPrintShippingLabel)
        Me.Controls.Add(Me.txtSupplierRMA)
        Me.Controls.Add(Me.txtRAStatusFriendly)
        Me.Controls.Add(Me.txtRAStatusOrig)
        Me.Controls.Add(Me.cmdCancelRARecord)
        Me.Controls.Add(Me.Picture1)
        Me.Controls.Add(Me.Picture2)
        Me.Controls.Add(Me.txtUpdatedName)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.cmdPrintRAForm)
        Me.Controls.Add(Me.FrameItem)
        Me.Controls.Add(Me.txtCreatedName)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.LabPrevStaff)
        Me.Controls.Add(Me.LabPrevUpdate)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabToday)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LabOurRANumber)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LabVersion)
        Me.Controls.Add(Me.LabHdr2)
        Me.Controls.Add(Me.LabHd3)
        Me.Controls.Add(Me.LabDateCreated)
        Me.Controls.Add(Me.LabRcvdBy)
        Me.Controls.Add(Me.LabHdr1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmNewRA"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "frmNewRA"
        CType(Me.picSubjectItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FrameSupplier.ResumeLayout(False)
        Me.FrameSupplier.PerformLayout()
        Me.FrameInvoiceList.ResumeLayout(False)
        Me.grpBoxOrigin.ResumeLayout(False)
        Me.grpBoxOrigin.PerformLayout()
        Me.panelOrigin.ResumeLayout(False)
        Me.panelOrigin.PerformLayout()
        CType(Me.Picture2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxItemPic.ResumeLayout(False)
        Me.FrameRMARequest.ResumeLayout(False)
        Me.FrameRMARequest.PerformLayout()
        Me.FrameGoods.ResumeLayout(False)
        Me.FrameGoods.PerformLayout()
        Me.FrameItem.ResumeLayout(False)
        Me.FrameItem.PerformLayout()
        CType(Me.Picture1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptRMAResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SSTabMain.ResumeLayout(False)
        Me.MainTabPage_progress.ResumeLayout(False)
        Me.grpBoxNotes.ResumeLayout(False)
        Me.grpBoxNotes.PerformLayout()
        Me.mainTabPage_attachments.ResumeLayout(False)
        Me.grpBoxItem.ResumeLayout(False)
        Me.grpBoxItem.PerformLayout()
        CType(Me.picMsExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picMsWord, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPDF, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpBoxAddNew.ResumeLayout(False)
        Me.grpBoxAddNew.PerformLayout()
        CType(Me.picSubjectMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents chkKeepScannedLeadZeroes As System.Windows.Forms.CheckBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cboPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents SSTabMain As System.Windows.Forms.TabControl
    Friend WithEvents MainTabPage_progress As System.Windows.Forms.TabPage
    Friend WithEvents mainTabPage_attachments As System.Windows.Forms.TabPage
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
    Friend WithEvents grpBoxItemPic As System.Windows.Forms.GroupBox
    Friend WithEvents picSubjectItem As System.Windows.Forms.PictureBox
    Friend WithEvents picSubjectMain As System.Windows.Forms.PictureBox
    Friend WithEvents doc_file_size As System.Windows.Forms.ColumnHeader
    Public WithEvents btnPrintItemLabel As System.Windows.Forms.Button
    Friend WithEvents cboItemLabelPrinters As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents btnUpdateSupplierAddress As System.Windows.Forms.Button
    Friend WithEvents picMsExcel As System.Windows.Forms.PictureBox
    Friend WithEvents picMsWord As System.Windows.Forms.PictureBox
    Friend WithEvents labStartHere As System.Windows.Forms.Label
    Friend WithEvents optOrigin_job As System.Windows.Forms.RadioButton
    Friend WithEvents optOrigin_counter As System.Windows.Forms.RadioButton
    Friend WithEvents optOrigin_stock As System.Windows.Forms.RadioButton
    Friend WithEvents grpBoxOrigin As System.Windows.Forms.GroupBox
    Friend WithEvents labOrigin As System.Windows.Forms.Label
    Friend WithEvents cboGoodsResult As System.Windows.Forms.ComboBox
    Friend WithEvents grpBoxNotes As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents labStep2 As System.Windows.Forms.Label
    Friend WithEvents labStep1 As System.Windows.Forms.Label
    Friend WithEvents labStep4 As System.Windows.Forms.Label
    Friend WithEvents labStep3 As System.Windows.Forms.Label
    Public WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents txtItemSaleInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents labItemSaleHdr As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtItemSaleInvoiceDate As System.Windows.Forms.TextBox
    Friend WithEvents panelOrigin As System.Windows.Forms.Panel
#End Region
End Class